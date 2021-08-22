using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace MotivationButtons
{
    public partial class Optimization
    {
        public List<Order> selectedCandidates   = new List<Order>();
        public List<Order> sampleOrder          = new List<Order>();


        public List<float> percentageList      = new List<float>();
        public double[] goldenCoeffIndexes      = new double[8];

        public int totalTopPercentWorkerAmount  = 0;
        public int totalSampleOrder             = 0;
        public int totalSelectedOrder           = 0;
        public int topCandidatePercentage       = 0;
        public int iterationCounter             = 1;
        public int goldenIteration              = 0;
        public int percentileLength             = 30;

        public class Order : Optimization
        {
            public int keyID { get; set; }
            public string nameSurname { get; set; }
            public string workStatus { get; set; }
            public double finalScore { get; set; }
            public float percentage { get; set; }

            public Order(int keyID, string nameSurname, string workStatus, double finalScore)
            {
                this.keyID = keyID;
                this.nameSurname = nameSurname;
                this.finalScore = finalScore;
                this.workStatus = workStatus;
            }
        }

        public void IterativePermutation()
        {
            Console.WriteLine("----------FINAL CALCULATION----------");

            for (double coeffIndex1 = 1; coeffIndex1 < 2.5; coeffIndex1 +=0.25)
            {
                for (double coeffIndex2 = 1; coeffIndex2 < 2.5; coeffIndex2 += 0.25) 
                {
                    for (double coeffIndex3 = 1; coeffIndex3 < 2.5; coeffIndex3 += 0.25)
                    {
                        for (double coeffIndex4 = 1; coeffIndex4 < 2.5; coeffIndex4 += 0.25)
                        {
                            for (double coeffIndex5 = 1; coeffIndex5 < 2.5; coeffIndex5 += 0.25)
                            {
                                CalculateFinalScores(coeffIndex1, coeffIndex2, coeffIndex3,
                                                        coeffIndex4, coeffIndex5, false);

                                //Console.WriteLine("Iteration: " + iterationCounter);
                                iterationCounter++;                                         
                            }
                        }
                    }
                }
            }       
            PrintTopCandidates();
        }

        private void PrintTopCandidates()
        {
            FileStream fileStream = new FileStream("C:\\Users\\Ege\\Documents\\GitHub\\MotivationButtons\\Result.txt", FileMode.Create);
            //FileStream fileStream = new FileStream(".\\Result.txt", FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            Console.SetOut(streamWriter);

            //CalculateFinalScores(goldenCoeffIndexes[0], goldenCoeffIndexes[1], goldenCoeffIndexes[2],
            //                     goldenCoeffIndexes[3], goldenCoeffIndexes[4], false);
            CalculateFinalScores(goldenCoeffIndexes[0], goldenCoeffIndexes[1], goldenCoeffIndexes[2], 
                                 goldenCoeffIndexes[3], goldenCoeffIndexes[4], true);

            for (int candidateIterator = 0; candidateIterator < totalSelectedOrder; candidateIterator++)
            {
                Console.WriteLine(    "Name, Surname: "     + selectedCandidates[candidateIterator].nameSurname 
                                    + " Score: "            + (float)selectedCandidates[candidateIterator].finalScore 
                                    + " Percentage: "       + selectedCandidates[candidateIterator].percentage);
                }
            streamWriter.Close();
            MessageBox.Show("calculations are completed","Update!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CalculateFinalScores(double coeffIndex1, double coeffIndex2, double coeffIndex3, 
                                          double coeffIndex4, double coeffIndex5, bool isFinalAnalysis)
        {
            for (int candidateIterator = 0; candidateIterator < totalCandidate; candidateIterator++)
            {
                double tempScore = 0.0;
                for (int mbIterator = 0; mbIterator < (int)MotivationButtonInfo.MotivationButtonLast; mbIterator++)
                {
                    if ((int)diffNormalizedMBScoreArr[0][mbNameIndex] == mbIterator)
                    {
                        tempScore += normalizedMBScoreArr[candidateIterator][mbIterator] + coeffIndex1;
                    }
                    else if ((int)diffNormalizedMBScoreArr[1][mbNameIndex] == mbIterator)
                    {
                        tempScore += normalizedMBScoreArr[candidateIterator][mbIterator] + coeffIndex2;
                    }
                    else if ((int)diffNormalizedMBScoreArr[2][mbNameIndex] == mbIterator)
                    {
                        tempScore += normalizedMBScoreArr[candidateIterator][mbIterator] + coeffIndex3;
                    }
                    else if ((int)diffNormalizedMBScoreArr[3][mbNameIndex] == mbIterator)
                    {
                        tempScore += normalizedMBScoreArr[candidateIterator][mbIterator] + coeffIndex4;
                    }
                    else if ((int)diffNormalizedMBScoreArr[4][mbNameIndex] == mbIterator)
                    {
                        tempScore += normalizedMBScoreArr[candidateIterator][mbIterator] + coeffIndex5;
                    }
                    else
                    {
                        for (int i = 0; i < (int)MotivationButtonInfo.MotivationButtonLast; i++)
                        {
                            if ((int)diffNormalizedMBScoreArr[i][mbNameIndex] == mbIterator)
                            {
                                tempScore += normalizedMBScoreArr[candidateIterator][mbIterator];
                                break;
                            }
                        }
                    }
                }
                if(isFinalAnalysis == false)
                {
                    if (iterationCounter == 1 && candidateArr[candidateIterator][workingStatusColumn] != WorkingStatusArr[(int)WorkingStatus.Candidate]) //create non-candidate array
                    {
                        var candidate = new Order(candidateIterator, candidateArr[candidateIterator][nameIndex], candidateArr[candidateIterator][workingStatusColumn], tempScore);
                        sampleOrder.Add(candidate);

                        sampleOrder[totalSampleOrder].finalScore = tempScore;
                        totalSampleOrder++; //increment total worker
                    }
                    else if(iterationCounter > 1 && candidateIterator < totalSampleOrder) //update scores depending on new temp scores...
                    {
                        sampleOrder[candidateIterator].finalScore = tempScore;
                    }
                }
                else if (isFinalAnalysis == true)
                {
                    if (candidateArr[candidateIterator][workingStatusColumn] == WorkingStatusArr[(int)WorkingStatus.Candidate]) //pick only candidates
                    {
                        var candidate = new Order(candidateIterator, candidateArr[candidateIterator][nameIndex], candidateArr[candidateIterator][workingStatusColumn], tempScore);
                        selectedCandidates.Add(candidate);

                        selectedCandidates[totalSelectedOrder].finalScore = tempScore;   
                        totalSelectedOrder++;
                    }
                }
            }
            if (isFinalAnalysis == false)
            {
                sampleOrder = sampleOrder.OrderByDescending(o => o.finalScore).ToList();

                int tempTopTotalWorkStatusScore = 0;
                topCandidatePercentage = (int)((double)totalSampleOrder * 0.3); 

                for (int candidateIterator = 0; candidateIterator < topCandidatePercentage; candidateIterator++)
                {
                    if (sampleOrder[candidateIterator].workStatus == WorkingStatusArr[(int)WorkingStatus.Working])
                    {
                        tempTopTotalWorkStatusScore++;
                    }
                }
                if (totalTopPercentWorkerAmount < tempTopTotalWorkStatusScore)
                {
                    goldenCoeffIndexes[0] = coeffIndex1;
                    goldenCoeffIndexes[1] = coeffIndex2;
                    goldenCoeffIndexes[2] = coeffIndex3;
                    goldenCoeffIndexes[3] = coeffIndex4;
                    goldenCoeffIndexes[4] = coeffIndex5;

                    goldenIteration = iterationCounter;
                    totalTopPercentWorkerAmount = tempTopTotalWorkStatusScore;
                }
                //Console.WriteLine("Total Found Workers: " + tempTopTotalWorkStatusScore);
            }
            else if (isFinalAnalysis == true)
            {
                selectedCandidates = selectedCandidates.OrderByDescending(o => o.finalScore).ToList();
                CreatePercentileList(percentileLength);
            }
        }

        public void CreatePercentileList(int topLimit)
        {
            if (topLimit <= totalSampleOrder)
            {
                int workerCounter = 0;
                for (int sampleIterator = 0; sampleIterator < topLimit; sampleIterator++)
                {
                    if (sampleOrder[sampleIterator].workStatus == WorkingStatusArr[(int)WorkingStatus.Working])
                    {
                        workerCounter++;
                    }
                }
                float tempPercentage = ((float)workerCounter / (float)topLimit) * 100;
                Math.Round(tempPercentage, 2);
                percentageList.Add(tempPercentage);

                Console.WriteLine(tempPercentage.ToString());

                int newLimit = topLimit + 30;
                CreatePercentileList(newLimit);
            }
        }
    }
}