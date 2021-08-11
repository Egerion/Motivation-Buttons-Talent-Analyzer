using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MotivationButtons
{
    public partial class Optimization
    {
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
            BeingStressed = 14
        }

        [Flags]
        public enum WorkingStatus
        {
            HighPerformance, //0
            Working, //1
            Candidate,  //2
            NotWorking //3
        }

        //constant paramters, which subject to change in the future...
        public const int                mbScoreStartIndex           = 96  - 4;
        public const int                mbScoreStopIndex            = 110 - 3;
        public const int                totalMBScore                = 180;
        public const int                startColumn                 = 4;
        public const int                startRow                    = 7;
        public const int                minScoreIndex               = 0;
        public const int                maxScoreIndex               = 1;
        public       int                workingStatusColumn         = 126;
        public const int                finalMBQuestionRowIndex     = 92;

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
                }
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
            for(int mbIterator = 0; mbIterator < (int)MotivationButtonInfo.BeingStressed; mbIterator++)
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
                //MessageBox.Show("Min MB: " + mbNames[mbIterator] + ": " + mbMinMaxScoreArr[mbIterator][minScoreIndex].ToString());
                //MessageBox.Show("Max MB: " + mbNames[mbIterator] + ": " + mbMinMaxScoreArr[mbIterator][maxScoreIndex].ToString());
            }

            //normalize the motivation buttons for all candidates...
            for (int mbIterator = 0; mbIterator < (int)MotivationButtonInfo.BeingStressed; mbIterator++)
            {               
                for (int candidateIterator = 0; candidateIterator < totalCandidate; candidateIterator++)
                {
                    normalizedMBScoreArr.Add(new List<double>());
                    normalizedMBScoreArr[candidateIterator].Add(((mbScoreArr[candidateIterator][mbIterator] / mbMinMaxScoreArr[mbIterator][minScoreIndex]) / (mbMinMaxScoreArr[mbIterator][maxScoreIndex] - mbMinMaxScoreArr[mbIterator][minScoreIndex])));
                    //debugging the normalized values for all candidates...
                    //MessageBox.Show("candidate: " + candidateArr[candidateIterator][0] + " normalized: " + mbNames[mbIterator] + ": " + normalizedMBScoreArr[candidateIterator][mbIterator].ToString());
                }
            }
        }

        public void ApplyCandidateCurrentWorkingStatus()
        {
            for (int mbIterator = 0; mbIterator < (int)MotivationButtonInfo.BeingStressed; mbIterator++)
            {
                double tempWorkingScore = 0.0;
                double tempNotWorkingScore = 0.0;

                for (int candidateIterator = 0; candidateIterator < totalCandidate; candidateIterator++)
                {
                   if (    Int32.Parse(candidateArr[candidateIterator][workingStatusColumn]) == (int)WorkingStatus.HighPerformance 
                        || Int32.Parse(candidateArr[candidateIterator][workingStatusColumn]) == (int)WorkingStatus.Working)
                    {
                        tempWorkingScore += normalizedMBScoreArr[candidateIterator][mbIterator];
                        //debugging
                        //MessageBox.Show("Candidate: " + candidateArr[candidateIterator][0] + " work status is: " + candidateWorkStatus[0]);
                    }
                    else if(Int32.Parse(candidateArr[candidateIterator][workingStatusColumn]) == (int)WorkingStatus.NotWorking)
                    {
                        tempNotWorkingScore += normalizedMBScoreArr[candidateIterator][mbIterator];
                        //debugging
                        //MessageBox.Show("Candidate: " + candidateArr[candidateIterator][0] + " work status is: " + candidateWorkStatus[1]);
                    }
                }
                diffNormalizedMBScoreArr.Add(new List<double>());
                diffNormalizedMBScoreArr[mbIterator].Add(mbIterator);
                diffNormalizedMBScoreArr[mbIterator].Add((tempWorkingScore/totalCandidate) - (tempNotWorkingScore/totalCandidate)); //this can be an 1d array in the future TODO!         
            }

            //sorting the list!
            diffNormalizedMBScoreArr = diffNormalizedMBScoreArr.AsEnumerable().Select(x => x.OrderBy(y => (double)y).ToList()).OrderByDescending(z => z[0]).ToList();

            //debug
            //for (int i = 0; i < 14; i++)
            //{
            //    MessageBox.Show(diffNormalizedMBScoreArr[i][1].ToString());
            //    MessageBox.Show(diffNormalizedMBScoreArr[i][0].ToString());
            //}
        }

        public void init()
        {

        }
    }
}
