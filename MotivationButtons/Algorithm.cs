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
        //debug console 
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

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
        public List<Order> candidateOrder = new List<Order>();

        public void SelectTopCandidates()
        {
            AllocConsole();

            for (double coeffIndex1 = 0; coeffIndex1 < 2.5; coeffIndex1+=0.5) //fractional incrementation...
            {
                for (double coeffIndex2 = 0; coeffIndex2 < 2.5; coeffIndex2 += 0.5)
                {
                    for (double coeffIndex3 = 0; coeffIndex3 < 2.5; coeffIndex3 += 0.5)
                    {
                        CalculateTopCandidates(coeffIndex1, coeffIndex2, coeffIndex3);
                        Console.WriteLine("Iteration: " + iterationCounter);
                        iterationCounter++;
                    }
                }
            }     
        }

        public void CalculateTopCandidates(double coeffIndex1, double coeffIndex2, double coeffIndex3)
        {
            int topCandidateIndex = (int)(totalCandidate * 0.3); //choose top 30%

            for (int candidateIterator = 0; candidateIterator < totalCandidate; candidateIterator++)
            {
                double tempScore = 0;
                for (int mbIterator = 0; mbIterator < (int)MotivationButtonInfo.BeingStressed; mbIterator++)
                {
                    tempScore += diffNormalizedMBScoreArr[mbIterator][1] * normalizedMBScoreArr[candidateIterator][mbIterator];

                    //pick top 3 motivation buttons...
                    if ((int)diffNormalizedMBScoreArr[0][0] == mbIterator)
                    {
                        diffNormalizedMBScoreArr[mbIterator][1] += diffNormalizedMBScoreArr[mbIterator][1] + coeffIndex1; //increment the iteration

                        //MessageBox.Show(diffNormalizedMBScoreArr[mbIterator][1].ToString());
                    }
                    else if ((int)diffNormalizedMBScoreArr[1][0] == mbIterator)
                    {
                        diffNormalizedMBScoreArr[mbIterator][1] += diffNormalizedMBScoreArr[mbIterator][1] + coeffIndex2;
                    }
                    else if ((int)diffNormalizedMBScoreArr[2][0] == mbIterator)
                    {
                        diffNormalizedMBScoreArr[mbIterator][1] += diffNormalizedMBScoreArr[mbIterator][1] + coeffIndex3;
                    }
                }
                //only pick if a candidate is not "candidate"
                if (Int32.Parse(candidateArr[candidateIterator][workingStatusColumn]) != (int)WorkingStatus.Candidate)
                {
                    var candidate = new Order(candidateIterator, candidateArr[candidateIterator][0], candidateArr[candidateIterator][workingStatusColumn], tempScore);
                    candidateOrder.Add(candidate);
                }
                //debug
                //MessageBox.Show(candidateOrder[candidateIterator].nameSurname);
                //MessageBox.Show(candidateOrder[candidateIterator].finalScore.ToString());
            }
            candidateOrder = candidateOrder.OrderBy(o => o.finalScore).ToList();

            for (int candidateIterator = 0; candidateIterator < topCandidateIndex; candidateIterator++)
            {
                Console.WriteLine(candidateOrder[candidateIterator].nameSurname);
                Console.WriteLine(candidateOrder[candidateIterator].workStatus);
                Console.WriteLine(candidateOrder[candidateIterator].finalScore);

                //MessageBox.Show(candidateOrder[candidateIterator].nameSurname);
                //MessageBox.Show(candidateOrder[candidateIterator].workStatus);
                //MessageBox.Show(candidateOrder[candidateIterator].finalScore.ToString());
            }
        }
    }
}