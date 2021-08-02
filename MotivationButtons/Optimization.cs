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
        const int minScore = 180;

        //global variables
        public List<List<string>> candidateArr = new List<List<string>>();
        public List<List<int>> mbArr = new List<List<int>>();
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
                    candidateArr[i][0] = "Removed Candidate";
                }
            }
        }

        public void FindMaxMinMB()
        {
            int number;
            for (int i = 0; i < totalCandidate; i++)
            {
                for (int columns = 96 - 4; columns < 110 - 3; columns++)
                {
                    if (Int32.TryParse(candidateArr[i][columns], out number))
                    {
                        mbArr.Add(new List<int>());
                        mbArr[i].Add(number);

                        MessageBox.Show(number.ToString());
                    }
                }
            }

            int smallInt = 0;
            int smallIntIndex = 0;

            //for (int i = 0; rows < totalCandidate; rows++)
            //{
            //    for(int j = 0; j < 14; j++)
            //    {
            //        if(mbArr[i][j] < smallInt)
            //        {
            //            smallInt = mbArr[i][j];
            //            smallIntIndex = j;
            //        }
            //    }


            //    MessageBox.Show(smallInt.ToString());
            //    MessageBox.Show(smallIntIndex.ToString());
            //}

            //int min = mbArr[0].Min();
            //int max = mbArr[0].Max();
        }


    }
}
