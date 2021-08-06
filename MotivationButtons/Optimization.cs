using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotivationButtons
{
    public class Optimization
    {
        [Flags]
        public enum MotivationButtonInfo
        {
            Routine,
            ObjectiveOrianted,
            BeingResponsible,
            BeingOpportuniscit,
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
            BeingStressed
        }

        //constants
        public const int minScore = 180;
        public const string removedCandidate = "Candidate Removed";

        //global variables
        public List<List<string>>candidateArr 
                                = new List<List<string>>();

        public List<List<int>>  mbScoreArr
                                = new List<List<int>>();
        
        public List<List<int>>  mbRoutineArr,
                                mbObjectiveOriantedArr,
                                mbBeingResponsibleArr,
                                mbBeingOpportuniscitArr,
                                mbBeingFamilyGuyArr,
                                mbSocialContactArr,
                                mbBeingAppreciatedArr,
                                mbBeingSafeArr,
                                mbBeingActiveArr,
                                mbBeingFairArr,
                                mbBeingAnalyticalArr,
                                mbBeingSincereArr,
                                mbBeingTeamMateArr,
                                mbBeingConvincingArr,
                                mbBeingStressedArr

                                = new List<List<int>>();

        public string excelPath;
        public int totalCandidate;

        public void CandidateDataInit()
        {

        }

        public void LoadCandidateData(string path)
        {
            int rows = 7;
            int columns = 4;
            int candidateIterator = 0;

            this.excelPath = path;
            Excel excel = new Excel(this.excelPath, 1);

            while (excel.ReadCell(rows, columns) != null)
            {
                candidateArr.Add(new List<string>());
                while (excel.ReadCell(rows, columns) != null)
                {
                    candidateArr[candidateIterator].Add(excel.ReadCell(rows, columns));
                    columns++;
                }
                columns = 4;
                candidateIterator++;
                rows++;
            }
            totalCandidate = candidateIterator;
            excel.TerminateExcel();
        }

        public void RemoveImproperCandidateData()
        {
            int number;
            for (int i = 0; i < totalCandidate; i++)
            {
                int scoreCounter = 0;
                for (int j = 1; j < 92; j++)
                {
                    if (Int32.TryParse(candidateArr[i][j], out number))
                    {
                        scoreCounter += number;
                    }
                }
                if (scoreCounter < minScore)
                {
                    candidateArr[i][0] = removedCandidate;
                }
            }
        }

        public void FindMaxMinMB()
        {
            int tempNumber; 

            //gather motivation buttons scores
            for (int i = 0; i < totalCandidate; i++)
            {
                for (int columns = 96 - 4; columns < 110 - 3; columns++)
                {
                    if (Int32.TryParse(candidateArr[i][columns], out tempNumber))
                    {
                        mbScoreArr.Add(new List<int>());
                        mbScoreArr[i].Add(tempNumber);

                        MessageBox.Show(tempNumber.ToString());
                    }
                }
            }

            for(int i = 0; i < totalCandidate; i++)
            {
                for(int mbIterator = 0; mbIterator < (int)MotivationButtonInfo.BeingStressed; mbIterator++)
                {
                    switch (mbIterator)
                    {
                        case (int)MotivationButtonInfo.Routine:
                            mbRoutineArr.Add(new List<int>());
                            mbRoutineArr[i].Add(mbScoreArr[i][mbIterator]);
                            break;
                        case (int)MotivationButtonInfo.ObjectiveOrianted:
                            mbObjectiveOriantedArr.Add(new List<int>());
                            mbObjectiveOriantedArr[i].Add(mbScoreArr[i][mbIterator]);
                            break;
                        case (int)MotivationButtonInfo.BeingResponsible:
                            mbBeingResponsibleArr.Add(new List<int>());
                            mbBeingResponsibleArr[i].Add(mbScoreArr[i][mbIterator]);
                            break;
                        case (int)MotivationButtonInfo.BeingOpportuniscit:
                            mbBeingOpportuniscitArr.Add(new List<int>());
                            mbBeingOpportuniscitArr[i].Add(mbScoreArr[i][mbIterator]);
                            break;
                        case (int)MotivationButtonInfo.BeingFamilyGuy:
                            mbBeingFamilyGuyArr.Add(new List<int>());
                            mbBeingFamilyGuyArr[i].Add(mbScoreArr[i][mbIterator]);
                            break;
                        case (int)MotivationButtonInfo.SocialContact:
                            mbSocialContactArr.Add(new List<int>());
                            mbSocialContactArr[i].Add(mbScoreArr[i][mbIterator]);
                            break;
                        case (int)MotivationButtonInfo.BeingAppreciated:
                            mbBeingAppreciatedArr.Add(new List<int>());
                            mbBeingAppreciatedArr[i].Add(mbScoreArr[i][mbIterator]);
                            break;
                        case (int)MotivationButtonInfo.BeingSafe:
                            mbBeingSafeArr.Add(new List<int>());
                            mbBeingSafeArr[i].Add(mbScoreArr[i][mbIterator]);
                            break;
                        case (int)MotivationButtonInfo.BeingActive:
                            mbBeingActiveArr.Add(new List<int>());
                            mbBeingActiveArr[i].Add(mbScoreArr[i][mbIterator]);
                            break;
                        case (int)MotivationButtonInfo.BeingFair:
                            mbBeingFairArr.Add(new List<int>());
                            mbBeingFairArr[i].Add(mbScoreArr[i][mbIterator]);
                            break;
                        case (int)MotivationButtonInfo.BeingAnalytical:
                            mbBeingAnalyticalArr.Add(new List<int>());
                            mbBeingAnalyticalArr[i].Add(mbScoreArr[i][mbIterator]);
                            break;
                        case (int)MotivationButtonInfo.BeingSincere:
                            mbBeingSincereArr.Add(new List<int>());
                            mbBeingSincereArr[i].Add(mbScoreArr[i][mbIterator]);
                            break;
                        case (int)MotivationButtonInfo.BeingTeamMate:
                            mbBeingTeamMateArr.Add(new List<int>());
                            mbBeingTeamMateArr[i].Add(mbScoreArr[i][mbIterator]);
                            break;
                        case (int)MotivationButtonInfo.BeingConvincing:
                            mbBeingConvincingArr.Add(new List<int>());
                            mbBeingConvincingArr[i].Add(mbScoreArr[i][mbIterator]);
                            break;
                        case (int)MotivationButtonInfo.BeingStressed:
                            mbBeingStressedArr.Add(new List<int>());
                            mbBeingStressedArr[i].Add(mbScoreArr[i][mbIterator]);
                            break;
                        default:
                            break;
                    }
                }
            }

            //mbMinScoreArr[i].Add(mbScoreArr[i].Min());
            //mbMaxScoreArr[i].Add(mbScoreArr[i].Max());
        }
    }
}
