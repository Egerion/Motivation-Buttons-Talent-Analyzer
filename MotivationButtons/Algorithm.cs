using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MotivationButtons
{
    public partial class Optimization
    {
        public List<Order> selectedCandidates   = new List<Order>();
        public List<Order> candidateOrder       = new List<Order>();

        public double[] maxCoeffIndexes         = new double[8];

        public int totalCandidateWorkers        = 0;
        public int totalCandidateOrder          = 0;
        public int totalSelectedOrder           = 0;
        public int topCandidatePercentage       = 0;
        public int iterationCounter             = 1;

        public class Order : Optimization
        {
            public int keyID { get; set; }
            public string nameSurname { get; set; }
            public string workStatus { get; set; }
            public double finalScore { get; set; }
            public double topPercentage { get; set; }

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

            for (double coeffIndex1 = 1.5; coeffIndex1 < 4.0; coeffIndex1 +=0.5)
            {
                for (double coeffIndex2 = 1.5; coeffIndex2 < 4.0; coeffIndex2 += 0.5) 
                {
                    for (double coeffIndex3 = 1.5; coeffIndex3 < 4.0; coeffIndex3 += 0.5)
                    {
                      //  for (double coeffIndex4 = 1.5; coeffIndex4 < 4.0; coeffIndex4 += 0.5)
                      //  {
                       //     for (double coeffIndex5 = 1.5; coeffIndex5 < 4.0; coeffIndex5 += 0.5)
                        //    {
                                CalculateFinalScores(coeffIndex1, coeffIndex2, coeffIndex3, /*coeffIndex4, coeffIndex5,*/ false);

                                Console.WriteLine("Iteration: " + iterationCounter);
                                iterationCounter++;
                        //    }
                       // }
                    }
                }
            }       
            PrintTopCandidates();
        }

        private void PrintTopCandidates()
        {
            FileStream fileStream = new FileStream("C:\\Users\\egede\\Documents\\GitHub\\MotivationButtons\\Result.txt", FileMode.Create);
            //FileStream fileStream = new FileStream(".\\Result.txt", FileMode.Create);
            StreamWriter streamWriter = new StreamWriter(fileStream);
            Console.SetOut(streamWriter);

            CalculateFinalScores(maxCoeffIndexes[0], maxCoeffIndexes[1], maxCoeffIndexes[2], /*maxCoeffIndexes[3], maxCoeffIndexes[4],*/ true);
             
            for (int candidateIterator = 0; candidateIterator < totalSelectedOrder; candidateIterator++)
            {
                Console.WriteLine("Name Surname: "  + selectedCandidates[candidateIterator].nameSurname);
                Console.WriteLine("Score: "         + selectedCandidates[candidateIterator].finalScore);
                Console.WriteLine("Work Status: "   + selectedCandidates[candidateIterator].workStatus);
                //Console.WriteLine("Top Percentage: "+ selectedCandidates[candidateIterator].topPercentage); TODO
            }

            streamWriter.Close();
            MessageBox.Show("Calculation are done!","JB", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CalculateFinalScores(double coeffIndex1, double coeffIndex2, double coeffIndex3, /*double coeffIndex4, double coeffIndex5,*/ bool isFinalAnalysis)
        {
            for (int candidateIterator = 0; candidateIterator < totalCandidate; candidateIterator++)
            {
                double tempScore = 0;
                for (int mbIterator = 0; mbIterator < (int)MotivationButtonInfo.MotivationButtonLast; mbIterator++)
                {
                    if ((int)diffNormalizedMBScoreArr[0][mbNameIndex] == mbIterator)
                    {
                        tempScore += (diffNormalizedMBScoreArr[0][1] +  coeffIndex1) * normalizedMBScoreArr[candidateIterator][mbIterator];
                    }
                    else if ((int)diffNormalizedMBScoreArr[1][mbNameIndex] == mbIterator)
                    {
                        tempScore += (diffNormalizedMBScoreArr[1][1] + coeffIndex2) * normalizedMBScoreArr[candidateIterator][mbIterator];
                    }
                    else if ((int)diffNormalizedMBScoreArr[2][mbNameIndex] == mbIterator)
                    {
                        tempScore += (diffNormalizedMBScoreArr[2][1] +  coeffIndex3) * normalizedMBScoreArr[candidateIterator][mbIterator];
                    }
                    //else if ((int)diffNormalizedMBScoreArr[3][mbNameIndex] == mbIterator)
                    //{
                    //    tempScore += (diffNormalizedMBScoreArr[3][1] + coeffIndex4) * normalizedMBScoreArr[candidateIterator][mbIterator];
                    //}
                    //else if ((int)diffNormalizedMBScoreArr[4][mbNameIndex] == mbIterator)
                    //{
                    //    tempScore += (diffNormalizedMBScoreArr[4][1] + coeffIndex5) * normalizedMBScoreArr[candidateIterator][mbIterator];
                    //}
                    //else if ((int)diffNormalizedMBScoreArr[5][mbNameIndex] == mbIterator)
                    //{
                    //    tempScore += (diffNormalizedMBScoreArr[5][1] + coeffIndex6) * normalizedMBScoreArr[candidateIterator][mbIterator];
                    //}
                    //else if ((int)diffNormalizedMBScoreArr[6][mbNameIndex] == mbIterator)
                    //{
                    //    tempScore += (diffNormalizedMBScoreArr[6][1] + coeffIndex7) * normalizedMBScoreArr[candidateIterator][mbIterator];
                    //}
                    //else if ((int)diffNormalizedMBScoreArr[7][mbNameIndex] == mbIterator)
                    //{
                    //    tempScore += (diffNormalizedMBScoreArr[7][1] + coeffIndex8) * normalizedMBScoreArr[candidateIterator][mbIterator];
                    //}
                    else
                    {
                        for(int i = 0; i < (int)MotivationButtonInfo.MotivationButtonLast; i++)
                        {
                            if((int)diffNormalizedMBScoreArr[i][mbNameIndex] == mbIterator)
                            {
                                tempScore += diffNormalizedMBScoreArr[i][1] * normalizedMBScoreArr[candidateIterator][mbIterator];
                                break;
                            }
                        }
                    }
                }
                if(isFinalAnalysis == false)
                {
                    if (iterationCounter == 1 && candidateArr[candidateIterator][workingStatusColumn] != WorkingStatusArr[(int)WorkingStatus.Candidate]) //pick non candidates
                    {
                        var candidate = new Order(candidateIterator, candidateArr[candidateIterator][nameIndex], candidateArr[candidateIterator][workingStatusColumn], tempScore);
                        candidateOrder.Add(candidate);

                        candidateOrder[totalCandidateOrder].finalScore = tempScore;
                        totalCandidateOrder++;
                    }
                    else if(iterationCounter > 1 && candidateIterator < totalCandidateOrder)
                    {
                        candidateOrder[candidateIterator].finalScore = tempScore;
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
                candidateOrder = candidateOrder.OrderByDescending(o => o.finalScore).ToList();
                topCandidatePercentage = (int)(totalCandidateOrder * 0.3); 
                int tempTotalWorkStatusScore = 0;
                for (int candidateIterator = 0; candidateIterator < topCandidatePercentage; candidateIterator++)
                {
                    if (candidateOrder[candidateIterator].workStatus == WorkingStatusArr[(int)WorkingStatus.Working])
                    {
                        tempTotalWorkStatusScore++;
                    }
                }
                if (totalCandidateWorkers < tempTotalWorkStatusScore)
                {
                    maxCoeffIndexes[0] = coeffIndex1;
                    maxCoeffIndexes[1] = coeffIndex2;
                    maxCoeffIndexes[2] = coeffIndex3;
                    //maxCoeffIndexes[3] = coeffIndex4;
                    //maxCoeffIndexes[4] = coeffIndex5;

                    totalCandidateWorkers = tempTotalWorkStatusScore;
                }

                Console.WriteLine("Total Workers: " + totalCandidateWorkers.ToString());
                Console.WriteLine("Total Candidates: " + topCandidatePercentage.ToString());
                Console.WriteLine("Coeff. list: " + coeffIndex1.ToString() + " " + coeffIndex2.ToString() + " " + coeffIndex3.ToString());
            }
            else if (isFinalAnalysis == true)
            {
                selectedCandidates = selectedCandidates.OrderByDescending(o => o.finalScore).ToList();
            }
        }
    }
}