using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using EXCEL = Microsoft.Office.Interop.Excel;

namespace MotivationButtons
{
    public class Excel : Form
    {
        string path;

        EXCEL.Application excelApp = new EXCEL.Application();
        Workbook workBook; 
        Worksheet workSheet;

        //open excell
        public Excel(string Path, int Sheet)
        {
            this.path = Path;
            workBook = excelApp.Workbooks.Open(this.path);
            workSheet = workBook.Worksheets[Sheet];
        }

        //read cell
        public string ReadCell(int i, int j)
        {
            //++i;
            //++j;

            if (workSheet.Cells[i, j].Value2 != null)
            {
                return workSheet.Cells[i, j].Value2.ToString();
            }
            else
            {
                return null;
            }

        }

        //terminates the excell process
        [DllImport("user32.dll")]
        static extern int GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);
        System.Diagnostics.Process GetExcelProcess(EXCEL.Application excelApp)
        {
            int id;
            GetWindowThreadProcessId(excelApp.Hwnd, out id);
            return System.Diagnostics.Process.GetProcessById(id);
        }
        public void TerminateExcel()
        {
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
            foreach (System.Diagnostics.Process p in process)
            {
                if (!string.IsNullOrEmpty(p.ProcessName) && p.Id == GetExcelProcess(excelApp).Id)
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
