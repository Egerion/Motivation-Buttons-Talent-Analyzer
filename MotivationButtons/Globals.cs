using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MotivationButtons
{
    public partial class DataAggregation
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

        [Flags]
        public enum Gender
        {
           Female,
           Male,
           Else
        }

        public string[] GenderArr =
        {
            "Kadın",
            "Erkek",
            "Diğer"
        };

        public string[] CandidateStatusArr =
        {
            "Not Selected",
            "Selected",
            "Not Defined by User"
        };

        //column indexes
        public const int startColumn                                = 4;
        public const int startRow                                   = 7;
        public const int mbScoreStartIndex                          = 96  - startColumn;
        public const int mbScoreStopIndex                           = 111 - startColumn;
        public const int genderColumn                               = 117 - startColumn;
        public const int totalMBScore                               = 180;
        public const int workingStatusColumn                        = 129 - startColumn;
        public const int ageColumn                                  = 128 - startColumn;
        public const int jobCountColumn                             = 123 - startColumn;

        //array indexes 
        public const int minScoreIndex                              = 0;
        public const int maxScoreIndex                              = 1;
        public const int nameIndex                                  = 0;
        public const int mbNameIndex                                = 0;

        //global variables
        public List<List<string>>   masterDataArr                   = new List<List<string>>();             /* i = candidates, j = all excel rows* (starting from names...)*/
        public List<List<string>>   trainDataPersonArr              = new List<List<string>>();             /* ONLY ELIGIBLE CANDIDATES i = candidates, j = all excel rows* (starting from names...)*/
        public List<List<double>>   mbScoreArr                      = new List<List<double>>();             /* i = candidates, j = motivation buttons scores*/
        public List<List<int>>      mbMinMaxScoreArr                = new List<List<int>>();                /* i = motivation buttons, j = 0: min score, j = 1: max score*/
        public List<List<List<double>>> normMBScoresArr             = new List<List<List<double>>>();
        public List<List<double>>   diffNormalizedMBScoreArr        = new List<List<double>>();             /* i = candidates, j = 0: motivation button types j = 1: scores*/
        public List<List<double>>   normPersonMBScoreArr            = new List<List<double>>();
        public List<List<double>>   normCandidateMBScoreArr         = new List<List<double>>();

        public string               excelPath;
        public int                  totalTrainDataPerson;
        public string[]             mbNames                         = Enum.GetNames(typeof(MotivationButtonInfo));
        public string[]             candidateWorkStatus             = Enum.GetNames(typeof(WorkingStatus));

        public double[]             goldenCoeffIndexes              = new double[15];

        public int                  totalTopPercentWorkerAmount     = 0;
        public int                  topTrainingDataPercentage       = 0;
        public int                  iterationCounter                = 1;
        public int                  goldenIteration                 = 0;
        public double               eliminationPercentile           = 0.0;
    }
}