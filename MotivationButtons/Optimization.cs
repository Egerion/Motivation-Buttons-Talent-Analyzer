using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MotivationButtons
{
    public partial class Optimization
    {
        //debug console 
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [Flags]
        public enum MotivationButtonInfo
        {
            Routine,
            ObjectiveOrianted,
            BeingResponsible,
            BeingOpportunistic,
            BeingFamilyGuy,
            SocialContact,
            BeingAppreciated,
            BeingSafe,
            BeingActive,
            BeingFair,
            BeingAnalytical,
            BeingSincere,
            BeingTeamMate,
            BeingConvincing,
            BeingStressed,
            MotivationButtonLast
        }

        [Flags]
        public enum WorkingStatus
        {
            Working, 
            NotWorking,
            Candidate,
            HighPerformance, 
            WorkingStatusLast
        }

        public string[] WorkingStatusArr = { 
            "Çalışan", 
            "Ayrıldı", 
            "Aday",
            "Yüksek Performans"
        };

        public const int mbScoreStartIndex = 1;
        public const int mbScoreStopIndex = 16;
        public const int totalMBScore = 180;
        public const int startColumn = 4;
        public const int startRow = 7;
        public const int minScoreIndex = 0;
        public const int maxScoreIndex = 1;
        public const int nameIndex = 0;
        public const int mbNameIndex = 0;
        public const int workingStatusColumn = 34;
        public const int finalMBQuestionRowIndex = mbScoreStopIndex;

        //public const int                mbScoreStartIndex           = 96  - 4;
        //public const int                mbScoreStopIndex            = 110 - 3;
        //public const int                totalMBScore                = 180;
        //public const int                startColumn                 = 4;
        //public const int                startRow                    = 7;
        //public const int                minScoreIndex               = 0;
        //public const int                maxScoreIndex               = 1;
        //public       int                workingStatusColumn         = 129 - 4;
        //public const int                finalMBQuestionRowIndex     = 92;

        //global variables
        public List<List<string>>       masterDataArr               = new List<List<string>>();     /* i = candidates, j = all excel rows* (starting from names...)*/
        public List<List<string>>       candidateArr                = new List<List<string>>();     /* ONLY ELIGIBLE CANDIDATES i = candidates, j = all excel rows* (starting from names...)*/ 
        public List<List<double>>       mbScoreArr                  = new List<List<double>>();     /* i = candidates, j = motivation buttons scores*/
        public List<List<double>>       normalizedMBScoreArr        = new List<List<double>>();     /* i = candidates, j = normalized motivation buttons scores*/ 
        public List<List<int>>          mbMinMaxScoreArr            = new List<List<int>>();        /* i = motivation buttons, j = 0: min score, j = 1: max score*/
        public List<List<double>>       diffNormalizedMBScoreArr    = new List<List<double>>();     /* i = candidates, j = 0: motivation button types j = 1: scores*/

        public string                   excelPath;
        public int                      totalCandidate;
        public string[]                 mbNames                     = Enum.GetNames(typeof(MotivationButtonInfo));
        public string[]                 candidateWorkStatus         = Enum.GetNames(typeof(WorkingStatus));

        public void ExecuteDebugMode()
        {
            AllocConsole();
        }

        //functions
        public void LoadCandidateData(string path)
        {
            int rows = startRow;
            int columns = startColumn;
            int candidateIterator = 0;

            this.excelPath = path;
            Excel excel = new Excel(this.excelPath, 1);

            while (excel.ReadCell(rows, columns) != null)
            {
                masterDataArr.Add(new List<string>());
                while (excel.ReadCell(rows, columns) != null)
                {
                    masterDataArr[candidateIterator].Add(excel.ReadCell(rows, columns));
                    columns++;
                    if (columns == 5)
                    {
                        columns = 96; //skip redundant rows
                    }
                }
                //if(columns != 131)
                //{
                //    MessageBox.Show("Data set structure is looking wrong!", "Wrong Data Set", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //}
                columns = startColumn;
                candidateIterator++;
                rows++;
            }
            totalCandidate = candidateIterator;
            excel.TerminateExcel();
        }

        public void RemoveImproperCandidateData()
        {
            int number;
            int tempScoreCounter = 0;
            int newCandidateIterator = 0;

            for (int candidateIterator = 0; candidateIterator < totalCandidate; candidateIterator++)
            {
                for (int dataIterator = 0; dataIterator < finalMBQuestionRowIndex; dataIterator++)
                {
                    if (Int32.TryParse(masterDataArr[candidateIterator][dataIterator], out number))
                    {
                        tempScoreCounter += number;
                    }
                }
                if (tempScoreCounter == totalMBScore)
                {
                    candidateArr.Add(new List<string>());
                    for (int dataIterator = 0; dataIterator <= workingStatusColumn; dataIterator++)
                    {                 
                        candidateArr[newCandidateIterator].Add(masterDataArr[candidateIterator][dataIterator]);
                    }
                    newCandidateIterator++;
                }
                else
                {
                    //debugging candidate removed...
                    MessageBox.Show("Candidate: " + masterDataArr[candidateIterator][0] + " removed due to incomplete test result...");
                }
                tempScoreCounter = 0; //reset the counter
            }
            if(newCandidateIterator > 0)
            {
                totalCandidate = newCandidateIterator; //update the candidate size!
            }
        }

        public void NormalizeMotivationButtons()
        {
            Console.WriteLine("----------MIN-MAX MOTIVATION BUTTON SCORES----------");
            int tempNumber;

            //gather motivation buttons scores
            for (int candidateIterator = 0; candidateIterator < totalCandidate; candidateIterator++)
            {
                mbScoreArr.Add(new List<double>());
                for (int mbIterator = mbScoreStartIndex; mbIterator < mbScoreStopIndex; mbIterator++)
                {
                    if (Int32.TryParse(candidateArr[candidateIterator][mbIterator], out tempNumber))
                    {                   
                        mbScoreArr[candidateIterator].Add(tempNumber);
                    }
                }
            }

            //find and store min max for all motivation buttons...
            for (int mbIterator = 0; mbIterator < (int)MotivationButtonInfo.MotivationButtonLast; mbIterator++)
            {
                int tempMin     = 100;
                int tempMax     = 0;
                int temValue    = 0;

                mbMinMaxScoreArr.Add(new List<int>());
                for (int candidateIterator = 0; candidateIterator < totalCandidate; candidateIterator++)
                {
                    temValue = (int)mbScoreArr[candidateIterator][mbIterator];
                    if(temValue <= tempMin)
                    {
                        tempMin = temValue;
                    }
                    if(temValue >= tempMax)
                    {
                        tempMax = temValue;
                    }
                }
                mbMinMaxScoreArr[mbIterator].Add(tempMin);
                mbMinMaxScoreArr[mbIterator].Add(tempMax);

                //debugging the min max of motivation buttons...
                Console.WriteLine("Min MB: " + mbNames[mbIterator] + ": " + mbMinMaxScoreArr[mbIterator][minScoreIndex].ToString());
                Console.WriteLine("Max MB: " + mbNames[mbIterator] + ": " + mbMinMaxScoreArr[mbIterator][maxScoreIndex].ToString());
            }

            Console.WriteLine("----------NORMALIZED MOTIVATION BUTTON SCORES----------");

            //normalize the motivation buttons for all candidates...
            for (int mbIterator = 0; mbIterator < (int)MotivationButtonInfo.MotivationButtonLast; mbIterator++)
            {               
                for (int candidateIterator = 0; candidateIterator < totalCandidate; candidateIterator++)
                {
                    normalizedMBScoreArr.Add(new List<double>());
                    normalizedMBScoreArr[candidateIterator].Add(((mbScoreArr[candidateIterator][mbIterator] - mbMinMaxScoreArr[mbIterator][minScoreIndex]) / (mbMinMaxScoreArr[mbIterator][maxScoreIndex] - mbMinMaxScoreArr[mbIterator][minScoreIndex])));
                    //debugging the normalized values for all candidates...
                    Console.WriteLine("candidate: " + candidateArr[candidateIterator][0] + " normalized: " + mbNames[mbIterator] + ": " + normalizedMBScoreArr[candidateIterator][mbIterator].ToString());
                }
            }
        }

        public void ApplyCandidateCurrentWorkingStatus()
        {
            Console.WriteLine("----------DIFF NORMALIZED MOTIVATION BUTTON SCORES----------");

            for (int mbIterator = 0; mbIterator < (int)MotivationButtonInfo.MotivationButtonLast; mbIterator++)
            {
                int tempWorkingCounter = 0;
                double tempWorkingScore = 0.0;
                int tempNotWorkingCounter = 0;
                double tempNotWorkingScore = 0.0;
              
                for (int candidateIterator = 0; candidateIterator < totalCandidate; candidateIterator++)
                {
                    if (candidateArr[candidateIterator][workingStatusColumn] == WorkingStatusArr[(int)WorkingStatus.Working])
                    {
                        tempWorkingScore += normalizedMBScoreArr[candidateIterator][mbIterator];
                        tempWorkingCounter++;
                    }
                    else if (candidateArr[candidateIterator][workingStatusColumn] == WorkingStatusArr[(int)WorkingStatus.NotWorking])
                    {
                        tempNotWorkingScore += normalizedMBScoreArr[candidateIterator][mbIterator];
                        tempNotWorkingCounter++;
                    }

                }
                diffNormalizedMBScoreArr.Add(new List<double>());
                diffNormalizedMBScoreArr[mbIterator].Add(mbIterator);

                double tempScore = (tempWorkingScore / tempWorkingCounter) - (tempNotWorkingScore / tempNotWorkingCounter);
                if (tempScore < 0)
                {
                    for (int candidateIterators = 0; candidateIterators < totalCandidate; candidateIterators++)
                    {
                        normalizedMBScoreArr[candidateIterators][mbIterator] = (1 - normalizedMBScoreArr[candidateIterators][mbIterator]);
                    }
                }
               diffNormalizedMBScoreArr[mbIterator].Add(Math.Abs(tempScore)); //this can be an 1d array in the future TODO!         
            }
            //sorting the list!
            diffNormalizedMBScoreArr = diffNormalizedMBScoreArr.OrderByDescending(y => y[1]).ToList(); //order the array with respect to final score!
            //debug
            for (int i = 0; i < (int)MotivationButtonInfo.MotivationButtonLast; i++)
            {
                Console.WriteLine("Motivation Button: " + mbNames[(int)diffNormalizedMBScoreArr[i][0]] + " " + diffNormalizedMBScoreArr[i][mbNameIndex].ToString());
                Console.WriteLine("With diff-normalized score: " + diffNormalizedMBScoreArr[i][1].ToString());
            }
        }
    }
}
