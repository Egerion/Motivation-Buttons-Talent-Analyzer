using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace MotivationButtons
{
    public partial class DataAggregation
    {
        public void RemoveImproperTrainDataPerson()
        {
            int number;
            int tempScoreCounter = 0;
            int newPersonIterator = 0;

            for (int personIterator = 0; personIterator < totalTrainDataPerson; personIterator++)
            {
                for (int dataIterator = 2; dataIterator < mbScoreStartIndex; dataIterator++)
                {
                    if (Int32.TryParse(masterDataArr[personIterator][dataIterator], out number))
                    {
                        tempScoreCounter += number;                                  
                    }
                }
                if (tempScoreCounter == totalMBScore)
                {
                    trainDataPersonArr.Add(new List<string>());
                    for (int dataIterator = 0; dataIterator <= workingStatusColumn; dataIterator++)
                    {                 
                        trainDataPersonArr[newPersonIterator].Add(masterDataArr[personIterator][dataIterator]);
                    }
                    newPersonIterator++;
                }
                else
                {
                    //debugging candidate removed...
                    MessageBox.Show("Person: " + masterDataArr[personIterator][0] + " removed due to incomplete test result. Score: " + tempScoreCounter.ToString());
                }
                tempScoreCounter = 0; //reset the counter
            }
            if(newPersonIterator > 0)
            {
                totalTrainDataPerson = newPersonIterator; //update the candidate size!
            }
        }

        public void NormalizeMotivationButtons()
        {
            Console.WriteLine("---min-max motivation buttons scores---");
            int tempNumber;

            //gather motivation buttons scores
            for (int personIterator = 0; personIterator < totalTrainDataPerson; personIterator++)
            {
                mbScoreArr.Add(new List<double>());
                for (int mbIterator = mbScoreStartIndex; mbIterator < mbScoreStopIndex; mbIterator++)
                {
                    if (Int32.TryParse(trainDataPersonArr[personIterator][mbIterator], out tempNumber))
                    {
                        mbScoreArr[personIterator].Add(tempNumber);
                    }
                }
            }

            //find and store min max for all motivation buttons...
            for (int mbIterator = 0; mbIterator < (int)MotivationButtonInfo.MotivationButtonLast; mbIterator++)
            {
                int tempMin = 100;
                int tempMax = 0;
                int temValue = 0;

                mbMinMaxScoreArr.Add(new List<int>());
                for (int personIterator = 0; personIterator < totalTrainDataPerson; personIterator++)
                {
                    temValue = (int)mbScoreArr[personIterator][mbIterator];
                    if (temValue <= tempMin)
                    {
                        tempMin = temValue;
                    }
                    if (temValue >= tempMax)
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

            Console.WriteLine("---normalized motivation buttons scores---");
            for (int mbIterator = 0; mbIterator < (int)MotivationButtonInfo.MotivationButtonLast; mbIterator++)
            {
                int tempPersonIterator          = 0;
                int tempCandidateIterator       = 0;
                int tempWorkingCounter          = 0;
                double tempWorkingScore         = 0.0;
                int tempNotWorkingCounter       = 0;
                double tempNotWorkingScore      = 0.0;

                for (int personIterator = 0; personIterator < totalTrainDataPerson; personIterator++)
                {
                    if (trainDataPersonArr[personIterator][workingStatusColumn] != WorkingStatusArr[(int)WorkingStatus.Candidate])
                    {

                        double tempNormalizedScore = (mbScoreArr[personIterator][mbIterator] - mbMinMaxScoreArr[mbIterator][minScoreIndex]) / (mbMinMaxScoreArr[mbIterator][maxScoreIndex] - mbMinMaxScoreArr[mbIterator][minScoreIndex]);
                        Math.Round(tempNormalizedScore, 2);

                        normMBScoresArr.Add(new List<List<double>>());
                        normMBScoresArr[personTypeIndex].Add(new List<double>());
                        normMBScoresArr[personTypeIndex][tempPersonIterator].Add(tempNormalizedScore);

                        Console.WriteLine("Person: " + trainDataPersonArr[personIterator][nameIndex] + "MB: " + mbNames[mbIterator] + " score: " + tempNormalizedScore);

                        tempPersonIterator++;

                        if (trainDataPersonArr[personIterator][workingStatusColumn] == WorkingStatusArr[(int)WorkingStatus.Working])
                        {
                            tempWorkingScore += tempNormalizedScore;
                            tempWorkingCounter++;
                        }
                        else if (trainDataPersonArr[personIterator][workingStatusColumn] == WorkingStatusArr[(int)WorkingStatus.NotWorking])
                        {
                            tempNotWorkingScore += tempNormalizedScore;
                            tempNotWorkingCounter++;
                        }
                    }
                    else
                    {
                        double tempNormalizedScore = (mbScoreArr[personIterator][mbIterator] - mbMinMaxScoreArr[mbIterator][minScoreIndex]) / (mbMinMaxScoreArr[mbIterator][maxScoreIndex] - mbMinMaxScoreArr[mbIterator][minScoreIndex]);
                        Math.Round(tempNormalizedScore, 2);

                        normMBScoresArr.Add(new List<List<double>>());
                        normMBScoresArr[candidateTypeIndex].Add(new List<double>());
                        normMBScoresArr[candidateTypeIndex][tempCandidateIterator].Add(tempNormalizedScore);

                        Console.WriteLine("Candidate: " + trainDataPersonArr[personIterator][nameIndex] + " with score: " + tempNormalizedScore);

                        tempCandidateIterator++;
                    }
                }
                diffNormalizedMBScoreArr.Add(new List<double>());
                diffNormalizedMBScoreArr[mbIterator].Add(mbIterator);

                double tempScore = (tempWorkingScore / tempWorkingCounter) - (tempNotWorkingScore / tempNotWorkingCounter);
                if (tempScore < 0)
                {
                    for (int personIterator = 0; personIterator < tempWorkingCounter + tempNotWorkingCounter - 1; personIterator++)
                    {
                        normMBScoresArr[personTypeIndex][personIterator][mbIterator] = (1 - normMBScoresArr[personTypeIndex][personIterator][mbIterator]);
                    }
                    for (int candidateIterators = 0; candidateIterators < tempCandidateIterator; candidateIterators++)
                    {
                        normMBScoresArr[candidateTypeIndex][candidateIterators][mbIterator] = (1 - normMBScoresArr[candidateTypeIndex][candidateIterators][mbIterator]);
                    }
                }
                diffNormalizedMBScoreArr[mbIterator].Add(Math.Round(Math.Abs(tempScore), 4));
            }
            diffNormalizedMBScoreArr = diffNormalizedMBScoreArr.OrderByDescending(y => y[1]).ToList();
            //debugging
            Console.WriteLine("---diff-normalized---");
            for (int i = 0; i < (int)MotivationButtonInfo.MotivationButtonLast; i++)
            {
                Console.WriteLine("Motivation Button: " + mbNames[(int)diffNormalizedMBScoreArr[i][0]] + " " + diffNormalizedMBScoreArr[i][mbNameIndex].ToString());
                Console.WriteLine("With diff-normalized score: " + diffNormalizedMBScoreArr[i][1].ToString());
            }
        }
    }
}
