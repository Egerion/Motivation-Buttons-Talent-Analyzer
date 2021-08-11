using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MotivationButtons
{
    public partial class MainBody : Form
    {
        public Optimization optimizationObj = new Optimization();

        public MainBody()
        { 
            InitializeComponent();
        }

        public void LoadCandidateNames()
        {
            for (int i = 0; i < optimizationObj.totalCandidate; i++)
            {
                ListBox_Candidates.Items.Add(optimizationObj.candidateArr[i][0]);
                ListBox_Candidates.SelectedIndex = 0;  
            }
        }

        private void Button_LoadExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "Excel File |*.xlsx";
            openFileDialog.FilterIndex = 2;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                optimizationObj.LoadCandidateData(openFileDialog.FileName);
                optimizationObj.RemoveImproperCandidateData();
                //System.Threading.Tasks.Task.Run(() => LoadCandidateNames());
                LoadCandidateNames();
                optimizationObj.NormalizeMotivationButtons();
                optimizationObj.ApplyCandidateCurrentWorkingStatus();
                optimizationObj.SelectTopCandidates();


            }
        }
    }
}
