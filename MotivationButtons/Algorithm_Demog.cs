using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MotivationButtons
{
    public partial class DataAggregation
    {
        public class ListData : DataAggregation
        {
            public ListData(int keyID, string nameSurname, string workStatus,
                                string gender, int age, double finalScore)
            {
                this.keyID = keyID;
                this.nameSurname = nameSurname;
                this.workStatus = workStatus;
                this.gender = gender;
                this.age = age;
                this.finalScore = finalScore;
            }
            public int keyID { get; set; }
            public string nameSurname { get; set; }
            public string workStatus { get; set; }
            public double percentage { get; set; }
            public string gender { get; set; }
            public int age { get; set; }
            public int jobCount { get; set; }
            public double finalScore { get; set; }
            public string selectionStatus { get; set; }
        }

        public bool finalIteration = false;

        public double[] percentileListArr = new double[5];

        public List<ListData> trainingList = new List<ListData>();
        public int totalTrainingData = 0;
        public int personTypeIndex = 0;

        public List<ListData> candidateList = new List<ListData>();
        public int totalCandidateData = 0;
        public int candidateTypeIndex = 1;

        public int favorAvgAge;
        public int favorJobCount;
        public string favorGender;

        public int endStep = 10000;
        public int stepCounter = 0;

        private void PrintTopCandidatesDo()
        {
            string filename = Path.GetFileName(this.excelPath);
            //FileStream fileStream = new FileStream("C:\\Users\\Ege\\Documents\\GitHub\\MotivationButtons\\" + filename + "_Results.txt", FileMode.Create);
            FileStream fileStream = new FileStream(".\\Out\\" + filename + "_Results.txt", FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            Console.SetOut(streamWriter);

            finalIteration = true;

            CalculateFinalScoresDo(goldenCoeffIndexes[0], goldenCoeffIndexes[1], goldenCoeffIndexes[2],
                                   goldenCoeffIndexes[3], goldenCoeffIndexes[4], goldenCoeffIndexes[5],
                                   goldenCoeffIndexes[6], false);

            CalculateFinalScoresDo(goldenCoeffIndexes[0], goldenCoeffIndexes[1], goldenCoeffIndexes[2],
                                   goldenCoeffIndexes[3], goldenCoeffIndexes[4], goldenCoeffIndexes[5],
                                   goldenCoeffIndexes[6], true);

            Console.WriteLine("---Candidates---");
            for (int candidateIterator = 0; candidateIterator < totalCandidateData; candidateIterator++)
            {   
                Console.WriteLine(  "Candidate: " + candidateList[candidateIterator].nameSurname + 
                                    " Percentage: "+ candidateList[candidateIterator].percentage * 100);
            }
            Console.WriteLine("---Golden Parameters---");
            Console.WriteLine(  "Golden Iteration: " + goldenIteration.ToString() + " Final Iteration: "  + iterationCounter.ToString() +" Max Found Worker: " + totalTopPercentWorkerAmount.ToString());
            Console.WriteLine(  "Golden Coeffs "  +
                                " C1: " + goldenCoeffIndexes[0] + 
                                " C2: " + goldenCoeffIndexes[1] + 
                                " C3: " + goldenCoeffIndexes[2] + 
                                " C4: " + goldenCoeffIndexes[3] + 
                                " C5: " + goldenCoeffIndexes[4] + 
                                " C6: " + goldenCoeffIndexes[5] + 
                                " C7: " + goldenCoeffIndexes[6]);

            streamWriter.Close();
            MessageBox.Show("calculations are completed","Update!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CreateBiasParameters()
        {
            int tempAvgAge = 0;
            int tempMaleCounter = 0;
            int tempWorkerCounter = 0;
            int tempFemaleCounter = 0;
            int tempTotalJobCount = 0;

            for (int sampleIterator = 0; sampleIterator < totalTrainDataPerson; sampleIterator++)
            {
                if(trainDataPersonArr[sampleIterator][workingStatusColumn] != WorkingStatusArr[(int)WorkingStatus.Candidate]) 
                {
                    if (trainDataPersonArr[sampleIterator][genderColumn] == GenderArr[(int)Gender.Male])
                    {
                        tempMaleCounter++;
                    }
                    else
                    {
                        tempFemaleCounter++;
                    }
                    if (trainDataPersonArr[sampleIterator][workingStatusColumn] == WorkingStatusArr[(int)WorkingStatus.Working])
                    {
                        tempAvgAge += Int32.Parse(trainDataPersonArr[sampleIterator][ageColumn]);
                        tempTotalJobCount += Int32.Parse(trainDataPersonArr[sampleIterator][jobCountColumn]);
                        tempWorkerCounter++;

                    }
                }
            }
            if (tempMaleCounter >= tempFemaleCounter)
            {
                favorGender = GenderArr[(int)Gender.Male];
            }
            else
            {
                favorGender = GenderArr[(int)Gender.Female];
            }
            favorAvgAge = tempAvgAge / tempWorkerCounter;
            favorJobCount = tempTotalJobCount / tempWorkerCounter;
        }

        public void IterativePermutationDo()
        {
            CreateListDataArrays();
            CreateBiasParameters();

            for (double coeffIndex1 = -0.60; coeffIndex1 <= 1.60; coeffIndex1 += 0.20)
                for (double coeffIndex2 = -0.60; coeffIndex2 <= 1.60; coeffIndex2 += 0.20)
                    for (double coeffIndex3 = -0.60; coeffIndex3 <= 1.60; coeffIndex3 += 0.20)
                        for (double coeffIndex4 = -0.60; coeffIndex4 <= 1.60; coeffIndex4 += 0.20)
                            for (double coeffIndex5 = -0.60; coeffIndex5 <= 1.60; coeffIndex5 += 0.20)
                                for (double coeffIndex6 = -0.60; coeffIndex6 <= 1.60; coeffIndex6 += 0.20)
                                    for (double coeffIndex7 = -0.60; coeffIndex7 <= 1.60; coeffIndex7 += 0.20)                                     
                                    {
                                        if (finalIteration == true)
                                            goto Finish;
                                        CalculateFinalScoresDo(coeffIndex1, coeffIndex2, coeffIndex3,
                                                               coeffIndex4, coeffIndex5, coeffIndex6,
                                                               coeffIndex7, false);
                                        iterationCounter++;
                                    }
            Finish:
            PrintTopCandidatesDo();
        }

        private void CreateListDataArrays()
        {
            for(int personIterator = 0; personIterator < totalTrainDataPerson; personIterator++)
            {
                if (trainDataPersonArr[personIterator][workingStatusColumn] != WorkingStatusArr[(int)WorkingStatus.Candidate])
                {
                    var oData = new ListData(
                                personIterator,
                                trainDataPersonArr[personIterator][nameIndex],
                                trainDataPersonArr[personIterator][workingStatusColumn],
                                trainDataPersonArr[personIterator][genderColumn],
                                Int32.Parse(trainDataPersonArr[personIterator][ageColumn]),
                                0.0
                                );
                    trainingList.Add(oData);
                    trainingList[totalTrainingData].jobCount = Int32.Parse(trainDataPersonArr[personIterator][jobCountColumn]);

                    totalTrainingData++;
                }
                else
                {
                    var oData = new ListData(
                                personIterator,
                                trainDataPersonArr[personIterator][nameIndex],
                                trainDataPersonArr[personIterator][workingStatusColumn],
                                trainDataPersonArr[personIterator][genderColumn],
                                Int32.Parse(trainDataPersonArr[personIterator][ageColumn]),
                                0.0
                                );
                    candidateList.Add(oData);

                    totalCandidateData++;
                }
            }
        }

        private void CreatePercentileList(int percentileCealing, int iterationCounter)
        {
            var sortedTrainingList = trainingList.OrderByDescending(score => score.finalScore).ToList();
            if (iterationCounter == 5)
            {
                percentileCealing = totalTrainingData;
            }
            if(iterationCounter <= 5)
            {
                int tempWorkerCounter = 0;
                for(int personIterator = 0; personIterator < percentileCealing; personIterator++)
                {
                    if(sortedTrainingList[personIterator].workStatus == WorkingStatusArr[(int)WorkingStatus.Working])
                    {
                        tempWorkerCounter++;
                    }
                }
                double tempPercentage = Math.Round((double)tempWorkerCounter / (double)percentileCealing, 2);
                percentileListArr[iterationCounter - 1] = tempPercentage;
                Console.WriteLine("Percentile: " + tempPercentage * 100 + "% " + " found workers: " + tempWorkerCounter + " over: " + percentileCealing);

                iterationCounter++;
                CreatePercentileList(percentileCealing + topTrainingDataPercentage, iterationCounter);
            }
        }

        private void ApplyPercentileListToCandidates()
        {
            var sortedCandidateList = candidateList.OrderByDescending(score => score.finalScore).ToList();
            for(int candidateIterator = 0; candidateIterator < totalCandidateData; candidateIterator++)
            {
                if(candidateIterator <= (int)((double)totalCandidateData * 0.20))
                {
                    candidateList[candidateIterator].percentage = percentileListArr[0];
                }
                else if(candidateIterator <= (int)((double)totalCandidateData * 0.40))
                {
                    candidateList[candidateIterator].percentage = percentileListArr[1];
                }
                else if (candidateIterator <= (int)((double)totalCandidateData * 0.60))
                {
                    candidateList[candidateIterator].percentage = percentileListArr[2];
                }
                else if (candidateIterator <= (int)((double)totalCandidateData * 0.80))
                {
                    candidateList[candidateIterator].percentage = percentileListArr[3];
                }
                else
                {
                    candidateList[candidateIterator].percentage = percentileListArr[4];
                }
                if(eliminationPercentile > 0.0)
                {
                    if(candidateList[candidateIterator].percentage <= eliminationPercentile)
                    {
                        candidateList[candidateIterator].selectionStatus = CandidateStatusArr[0];
                    }
                    else
                    {
                        candidateList[candidateIterator].selectionStatus = CandidateStatusArr[1];
                    }
                   
                }
                else
                {
                    candidateList[candidateIterator].selectionStatus = CandidateStatusArr[2];
                }
            }
        }

        private void CalculateFinalScoresDo(double coeffIndex1, double coeffIndex2, double coeffIndex3,
                                            double coeffIndex4, double coeffIndex5, double coeffIndex6,
                                            double coeffIndex7, bool isFinalAnalysis)
        {
            int dataType = 0;
            int totalLoop = 0;
            if(isFinalAnalysis == true)
            {
                dataType = candidateTypeIndex;
                totalLoop = totalCandidateData;
            }
            else if(isFinalAnalysis == false)
            {
                dataType = personTypeIndex;
                totalLoop = totalTrainingData;
            }
            for (int sampleIterator = 0; sampleIterator < totalLoop; sampleIterator++)
            {
                double tempScore = 0.0;
                for (int mbIterator = 0; mbIterator < (int)MotivationButtonInfo.MotivationButtonLast; mbIterator++)
                {
                    if ((int)diffNormalizedMBScoreArr[0][mbNameIndex] == mbIterator)
                    {
                        tempScore += normMBScoresArr[dataType][sampleIterator][mbIterator] + coeffIndex1;
                    }
                    else if ((int)diffNormalizedMBScoreArr[1][mbNameIndex] == mbIterator)
                    {
                        tempScore += normMBScoresArr[dataType][sampleIterator][mbIterator] + coeffIndex2;
                    }
                    else if ((int)diffNormalizedMBScoreArr[2][mbNameIndex] == mbIterator)
                    {
                        tempScore += normMBScoresArr[dataType][sampleIterator][mbIterator] + coeffIndex3;
                    }
                    else if ((int)diffNormalizedMBScoreArr[3][mbNameIndex] == mbIterator)
                    {
                        tempScore += normMBScoresArr[dataType][sampleIterator][mbIterator] + coeffIndex4;
                    }
                    else if ((int)diffNormalizedMBScoreArr[4][mbNameIndex] == mbIterator)
                    {
                        tempScore += normMBScoresArr[dataType][sampleIterator][mbIterator] + coeffIndex5;
                    }
                }
                if (isFinalAnalysis == true)
                {
                    candidateList[sampleIterator].finalScore = tempScore;
                }
                else if(isFinalAnalysis == false)
                {
                    if(trainingList[sampleIterator].jobCount >= favorJobCount)
                    {
                        tempScore += coeffIndex6;
                    }
                    if (trainingList[sampleIterator].age >= favorAvgAge)
                    {
                        tempScore += coeffIndex7;
                    }
                    trainingList[sampleIterator].finalScore = tempScore;
                }
            }
            if (isFinalAnalysis == true)
            {
                Console.WriteLine("---Percentiles---");
                CreatePercentileList(topTrainingDataPercentage, 1);
                ApplyPercentileListToCandidates();
            }
            else if (isFinalAnalysis == false)
            {
                var sortedTrainingList = trainingList.OrderByDescending(score => score.finalScore).ToList();

                topTrainingDataPercentage = (int)((double)totalTrainingData * 0.20);
                int tempWorkerCounter = 0;
                for (int sampleIterator = 0; sampleIterator < topTrainingDataPercentage; sampleIterator++)
                {
                    if (sortedTrainingList[sampleIterator].workStatus == WorkingStatusArr[(int)WorkingStatus.Working])
                    {
                        tempWorkerCounter++;
                    }
                }
                if (totalTopPercentWorkerAmount < tempWorkerCounter)
                {
                    goldenCoeffIndexes[0] = coeffIndex1;
                    goldenCoeffIndexes[1] = coeffIndex2;
                    goldenCoeffIndexes[2] = coeffIndex3;
                    goldenCoeffIndexes[3] = coeffIndex4;
                    goldenCoeffIndexes[4] = coeffIndex5;
                    goldenCoeffIndexes[5] = coeffIndex6;
                    goldenCoeffIndexes[6] = coeffIndex7;

                    goldenIteration = iterationCounter;
                    totalTopPercentWorkerAmount = tempWorkerCounter;

                    stepCounter = 0;
                }
                if(stepCounter >= endStep)
                {
                    finalIteration = true;
                }
                stepCounter++;
            }
        }
    }
}