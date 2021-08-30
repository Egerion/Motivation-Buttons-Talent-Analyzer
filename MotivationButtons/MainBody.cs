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
        public DataAggregation optimizationObj = new DataAggregation();

        public MainBody()
        { 
            InitializeComponent();
            LoadInitialSettings();
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            try { } catch { }
        }

        private void LoadInitialSettings()
        {
            ComboBox_TresholdPercentage.SelectedIndex = 0;
            TextBox_MaxStep.Text = (10000).ToString();
            TextBox_IterationStart.Text = optimizationObj.startStep.ToString();
            TextBox_IterationStep.Text = optimizationObj.iterationStep.ToString();
            TextBox_IterationEnd.Text = optimizationObj.endStep.ToString();
        }

        private void Button_LoadExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "Excel File |*.xlsx";
            openFileDialog.FilterIndex = 2;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Label_CurrentStatus.Text = "working...";
                PictureBox_Progress.Visible = true;
                if (CheckBox_DebugMode.Checked)
                {
                    optimizationObj.ExecuteDebugMode();
                }
                optimizationObj.GetExcelData(openFileDialog.FileName);
                optimizationObj.RemoveImproperTrainDataPerson();
                optimizationObj.NormalizeMotivationButtons();
                optimizationObj.IterativePermutationDo();
                LoadCandidateResults();
                //PictureBox_Progress.Image = global::MotivationButtons.Properties.Resources.Green_Tick;
                PictureBox_Progress.Visible = false;
                Label_CurrentStatus.Text = "Idle";
            }
        }
        private void LoadCandidateResults()
        {
            for (int candidateIterator = 0; candidateIterator < optimizationObj.totalCandidateData; candidateIterator++)
            {
                ListBox_Candidates.Items.Add(optimizationObj.candidateList[candidateIterator].nameSurname);
                ListBox_CandidatePercentile.Items.Add(optimizationObj.candidateList[candidateIterator].percentage);
                ListBox_CandidateStatus.Items.Add(optimizationObj.candidateList[candidateIterator].selectionStatus);
            }
        }

        private void ComboBox_TresholdPercentage_SelectedIndexChanged(object sender, EventArgs e)
        {
            double oValue = 0.0;
            switch (ComboBox_TresholdPercentage.SelectedIndex)
            {
                case 0:
                    oValue = 0.8;
                    break;
                case 1:
                    oValue = 0.6;
                    break;
                case 2:
                    oValue = 0.4;
                    break;
                case 3:
                    oValue = 0.2;
                    break;
                default:
                    break;
            }
            optimizationObj.eliminationPercentile = oValue;
        }

        private void TextBox_MaxStep_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(TextBox_MaxStep.Text, "[^0-9]"))
            {
                MessageBox.Show
                    ("Please enter only numbers.");
                TextBox_MaxStep.Text = TextBox_MaxStep.Text.Remove(TextBox_MaxStep.Text.Length - 1);
            }
            else
            {
                int EnteredAmount;
                if (!int.TryParse(TextBox_MaxStep.Text, out EnteredAmount))
                {
                    optimizationObj.endAfterStep = EnteredAmount;
                }
                else
                {
                    optimizationObj.endAfterStep = 10000;
                }
            }
        }

        private void CheckBox_DebugMode_CheckedChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(TextBox_MaxStep.Text, "[^0-9]"))
            {
                MessageBox.Show
                    ("Please enter only numbers.");
                TextBox_MaxStep.Text = TextBox_MaxStep.Text.Remove(TextBox_MaxStep.Text.Length - 1);
            }
            else
            {
                int EnteredAmount;
                if (!int.TryParse(TextBox_MaxStep.Text, out EnteredAmount))
                {
                    optimizationObj.endAfterStep = EnteredAmount;
                }
                else
                {
                    optimizationObj.endAfterStep = 10000;
                }
            }
        }

        private void TextBox_IterationStart_TextChanged(object sender, EventArgs e)
        {
            double EnteredAmount;
            if (!double.TryParse(TextBox_IterationStart.Text, out EnteredAmount))
            {
                optimizationObj.startStep = EnteredAmount;
            }
            else
            {
                optimizationObj.startStep = -0.60;
            }       
        }

        private void TextBox_IterationStep_TextChanged(object sender, EventArgs e)
        {
            double EnteredAmount;
            if (!double.TryParse(TextBox_IterationStep.Text, out EnteredAmount))
            {
                optimizationObj.iterationStep = EnteredAmount;
            }
            else
            {
                optimizationObj.iterationStep = 0.20;
            }
        }

        private void TextBox_IterationEnd_TextChanged(object sender, EventArgs e)
        {
            double EnteredAmount;
            if (!double.TryParse(TextBox_IterationEnd.Text, out EnteredAmount))
            {
                optimizationObj.endStep = EnteredAmount;
            }
            else
            {
                optimizationObj.endStep = 1.60;
            }
        }
    }
}
