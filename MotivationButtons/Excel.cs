using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using EXCEL = Microsoft.Office.Interop.Excel;

namespace MotivationButtons
{
    public partial class DataAggregation
    {
        //debug console 
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        public void ExecuteDebugMode()
        {
            AllocConsole();
        }

        public void GetExcelData(string fullPath)
        {
            this.excelPath = fullPath;
            EXCEL.Application excelapp = new EXCEL.Application();
            excelapp.Visible = false;
            EXCEL.Workbook excelappworkbook = excelapp.Workbooks.Open(
                fullPath,
                Type.Missing, Type.Missing, true, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing);
            EXCEL.Worksheet excelworksheet = (EXCEL.Worksheet)excelappworkbook.Worksheets.get_Item(1);
            EXCEL.Range excelcells = excelworksheet.UsedRange;


            object[,] worksheetValuesArray = excelcells.get_Value(Type.Missing);

            int dataIterator = 0;
            for (int row = startRow; row < (worksheetValuesArray.GetLength(0) + 1); row++)
            {
                masterDataArr.Add(new List<string>());
                for (int column = startColumn; column < (worksheetValuesArray.GetLength(1) + 1); column++)
                {
                    masterDataArr[dataIterator].Add(Convert.ToString(worksheetValuesArray[row, column]));
                }
                dataIterator++;
            }
            totalTrainDataPerson = dataIterator;
            excelappworkbook.Close(false, Type.Missing, Type.Missing);
            excelapp.Quit();
            TerminateExcel(excelapp);
        }
        [DllImport("user32.dll")]
        static extern int GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);
        System.Diagnostics.Process GetExcelProcess(EXCEL.Application excelApp)
        {
            int id;
            GetWindowThreadProcessId(excelApp.Hwnd, out id);
            return System.Diagnostics.Process.GetProcessById(id);
        }
        public void TerminateExcel(EXCEL.Application myExcelApp)
        {
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
            foreach (System.Diagnostics.Process p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName) && p.Id == GetExcelProcess(myExcelApp).Id)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch { }
                }
            }
        }
    }
}