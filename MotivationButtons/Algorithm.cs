using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace MotivationButtons
{
    public partial class Optimization
    {
        public int iterationCounter = 1;

        public class Order : Optimization
        {
            public int keyID { get; set; }
            public string nameSurname { get; set; }
            public string workStatus { get; set; }
            public double finalScore { get; set; }

            public Order(int keyID, string nameSurname, string workStatus, double finalScore)
            {
                this.keyID = keyID;
                this.nameSurname = nameSurname;
                this.finalScore = finalScore;
                this.workStatus = workStatus;
            }
        }
        public List<Order> selectedCandidates = new List<Order>();
        public List<Order> candidateOrder = new List<Order>();

        public double[] maxCoeffIndexes = new double [3];
        public int totalWorkStatusScore = 0;

        public int totalCandidateOrder = 0;
        public int totalSelectedOrder = 0;
        public int topCandidatePercentage = 0;

        public void SelectTopCandidates()
        {
            Console.WriteLine("----------FINAL CALCULATION----------");

            for (double coeffIndex1 = 0; coeffIndex1 < 2.5; coeffIndex1+=0.5) //fractional incrementation...
            {
                for (double coeffIndex2 = 0; coeffIndex2 < 2.5; coeffIndex2 += 0.5)
                {
                    for (double coeffIndex3 = 0; coeffIndex3 < 2.5; coeffIndex3 += 0.5)
                    {
                        CalculateTopCandidates(coeffIndex1, coeffIndex2, coeffIndex3, false);

                        Console.WriteLine("Iteration: " + iterationCounter);
                        iterationCounter++;
                    }
                }
            }
            //final adjustment
            PrintTopCandidates();
        }

        public void PrintTopCandidates()
        {
            CalculateTopCandidates(maxCoeffIndexes[0], maxCoeffIndexes[1], maxCoeffIndexes[2], true);

            for (int candidateIterator = 0; candidateIterator < totalSelectedOrder; candidateIterator++)
            {
                Console.WriteLine("Name Surname: "  + selectedCandidates[candidateIterator].nameSurname);
                Console.WriteLine("Score: "         + selectedCandidates[candidateIterator].finalScore);
                Console.WriteLine("Work Status: "   + selectedCandidates[candidateIterator].workStatus);
            }
        }

        public void CalculateTopCandidates(double coeffIndex1, double coeffIndex2, double coeffIndex3, bool isFinalAnalysis)
        {
            for (int candidateIterator = 0; candidateIterator < totalCandidate; candidateIterator++)
            {
                double tempScore = 0;
                for (int mbIterator = 0; mbIterator < (int)MotivationButtonInfo.MotivationButtonLast; mbIterator++)
                {
                    //pick top 3 motivation buttons...
                    if ((int)diffNormalizedMBScoreArr[0][0] == mbIterator)
                    {
                        tempScore += (diffNormalizedMBScoreArr[0][1] +  coeffIndex1) * normalizedMBScoreArr[candidateIterator][mbIterator];
                    }
                    else if ((int)diffNormalizedMBScoreArr[1][0] == mbIterator)
                    {
                        tempScore += (diffNormalizedMBScoreArr[1][1] + coeffIndex2) * normalizedMBScoreArr[candidateIterator][mbIterator];
                    }
                    else if ((int)diffNormalizedMBScoreArr[2][0] == mbIterator)
                    {
                        tempScore += (diffNormalizedMBScoreArr[2][1] +  coeffIndex3) * normalizedMBScoreArr[candidateIterator][mbIterator];
                    }
                    else
                    {
                        for(int i = 0; i < (int)MotivationButtonInfo.MotivationButtonLast; i++)
                        {
                            if((int)diffNormalizedMBScoreArr[i][0] == mbIterator)
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
                        var candidate = new Order(candidateIterator, candidateArr[candidateIterator][0], candidateArr[candidateIterator][workingStatusColumn], tempScore);
                        candidateOrder.Add(candidate);

                        candidateOrder[totalCandidateOrder].finalScore = tempScore;
                        totalCandidateOrder++;
                    }
                }
                else if (isFinalAnalysis == true)
                {
                    if (candidateArr[candidateIterator][workingStatusColumn] == WorkingStatusArr[(int)WorkingStatus.Candidate]) //pick only candidates
                    {
                        var candidate = new Order(candidateIterator, candidateArr[candidateIterator][0], candidateArr[candidateIterator][workingStatusColumn], tempScore);
                        selectedCandidates.Add(candidate);

                        selectedCandidates[totalSelectedOrder].finalScore = tempScore;
                        totalSelectedOrder++;
                    }
                }
            }

            if (isFinalAnalysis == false)
            {
                candidateOrder = candidateOrder.OrderByDescending(o => o.finalScore).ToList();
                topCandidatePercentage = (int)(totalCandidateOrder * 0.3); //choose top 30%
                int tempTotalWorkStatusScore = 0;
                for (int candidateIterator = 0; candidateIterator < topCandidatePercentage; candidateIterator++)
                {
                    if (candidateOrder[candidateIterator].workStatus == WorkingStatusArr[1])
                    {
                        tempTotalWorkStatusScore++;
                    }
                }
                if (totalWorkStatusScore < tempTotalWorkStatusScore)
                {
                    totalWorkStatusScore = tempTotalWorkStatusScore;
                    maxCoeffIndexes[0] = coeffIndex1;
                    maxCoeffIndexes[1] = coeffIndex2;
                    maxCoeffIndexes[2] = coeffIndex3;
                }
            }
            else if (isFinalAnalysis == true)
            {
                selectedCandidates = selectedCandidates.OrderByDescending(o => o.finalScore).ToList();
            }
        }
    }
}