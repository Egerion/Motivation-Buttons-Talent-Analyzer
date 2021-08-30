
namespace MotivationButtons
{
    partial class MainBody
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainBody));
            this.SidePanel = new System.Windows.Forms.Panel();
            this.TextBox_IterationEnd = new System.Windows.Forms.TextBox();
            this.Label_IterationEnd = new System.Windows.Forms.Label();
            this.TextBox_IterationStep = new System.Windows.Forms.TextBox();
            this.Label_IterationStep = new System.Windows.Forms.Label();
            this.Label_IterationStart = new System.Windows.Forms.Label();
            this.Label_IterationSettings = new System.Windows.Forms.Label();
            this.TextBox_IterationStart = new System.Windows.Forms.TextBox();
            this.Label_MaxStep = new System.Windows.Forms.Label();
            this.TextBox_MaxStep = new System.Windows.Forms.TextBox();
            this.Label_TresholdPercentage = new System.Windows.Forms.Label();
            this.ComboBox_TresholdPercentage = new System.Windows.Forms.ComboBox();
            this.Label_Settings = new System.Windows.Forms.Label();
            this.CheckBox_DebugMode = new System.Windows.Forms.CheckBox();
            this.Button_LoadExcel = new System.Windows.Forms.Button();
            this.Label_CurrentStatus = new System.Windows.Forms.Label();
            this.Label_Status = new System.Windows.Forms.Label();
            this.ListBox_Candidates = new System.Windows.Forms.ListBox();
            this.PictureBox_Progress = new System.Windows.Forms.PictureBox();
            this.Label_Candidates = new System.Windows.Forms.Label();
            this.ListBox_CandidatePercentile = new System.Windows.Forms.ListBox();
            this.Label_CandidatePercentiles = new System.Windows.Forms.Label();
            this.ListBox_CandidateStatus = new System.Windows.Forms.ListBox();
            this.Label_CandidateStatus = new System.Windows.Forms.Label();
            this.SidePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Progress)).BeginInit();
            this.SuspendLayout();
            // 
            // SidePanel
            // 
            this.SidePanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.SidePanel.BackgroundImage = global::MotivationButtons.Properties.Resources.grey;
            this.SidePanel.Controls.Add(this.TextBox_IterationEnd);
            this.SidePanel.Controls.Add(this.Label_IterationEnd);
            this.SidePanel.Controls.Add(this.TextBox_IterationStep);
            this.SidePanel.Controls.Add(this.Label_IterationStep);
            this.SidePanel.Controls.Add(this.Label_IterationStart);
            this.SidePanel.Controls.Add(this.Label_IterationSettings);
            this.SidePanel.Controls.Add(this.TextBox_IterationStart);
            this.SidePanel.Controls.Add(this.Label_MaxStep);
            this.SidePanel.Controls.Add(this.TextBox_MaxStep);
            this.SidePanel.Controls.Add(this.Label_TresholdPercentage);
            this.SidePanel.Controls.Add(this.ComboBox_TresholdPercentage);
            this.SidePanel.Controls.Add(this.Label_Settings);
            this.SidePanel.Controls.Add(this.CheckBox_DebugMode);
            this.SidePanel.Controls.Add(this.Button_LoadExcel);
            this.SidePanel.Location = new System.Drawing.Point(-1, 63);
            this.SidePanel.Name = "SidePanel";
            this.SidePanel.Size = new System.Drawing.Size(344, 629);
            this.SidePanel.TabIndex = 0;
            // 
            // TextBox_IterationEnd
            // 
            this.TextBox_IterationEnd.Location = new System.Drawing.Point(146, 315);
            this.TextBox_IterationEnd.Name = "TextBox_IterationEnd";
            this.TextBox_IterationEnd.Size = new System.Drawing.Size(93, 31);
            this.TextBox_IterationEnd.TabIndex = 14;
            this.TextBox_IterationEnd.TextChanged += new System.EventHandler(this.TextBox_IterationEnd_TextChanged);
            // 
            // Label_IterationEnd
            // 
            this.Label_IterationEnd.AutoSize = true;
            this.Label_IterationEnd.Location = new System.Drawing.Point(94, 318);
            this.Label_IterationEnd.Name = "Label_IterationEnd";
            this.Label_IterationEnd.Size = new System.Drawing.Size(42, 25);
            this.Label_IterationEnd.TabIndex = 13;
            this.Label_IterationEnd.Text = "End";
            // 
            // TextBox_IterationStep
            // 
            this.TextBox_IterationStep.Location = new System.Drawing.Point(146, 277);
            this.TextBox_IterationStep.Name = "TextBox_IterationStep";
            this.TextBox_IterationStep.Size = new System.Drawing.Size(93, 31);
            this.TextBox_IterationStep.TabIndex = 12;
            this.TextBox_IterationStep.TextChanged += new System.EventHandler(this.TextBox_IterationStep_TextChanged);
            // 
            // Label_IterationStep
            // 
            this.Label_IterationStep.AutoSize = true;
            this.Label_IterationStep.Location = new System.Drawing.Point(93, 280);
            this.Label_IterationStep.Name = "Label_IterationStep";
            this.Label_IterationStep.Size = new System.Drawing.Size(47, 25);
            this.Label_IterationStep.TabIndex = 11;
            this.Label_IterationStep.Text = "Step";
            // 
            // Label_IterationStart
            // 
            this.Label_IterationStart.AutoSize = true;
            this.Label_IterationStart.Location = new System.Drawing.Point(93, 243);
            this.Label_IterationStart.Name = "Label_IterationStart";
            this.Label_IterationStart.Size = new System.Drawing.Size(48, 25);
            this.Label_IterationStart.TabIndex = 10;
            this.Label_IterationStart.Text = "Start";
            // 
            // Label_IterationSettings
            // 
            this.Label_IterationSettings.AutoSize = true;
            this.Label_IterationSettings.Location = new System.Drawing.Point(92, 191);
            this.Label_IterationSettings.Name = "Label_IterationSettings";
            this.Label_IterationSettings.Size = new System.Drawing.Size(147, 25);
            this.Label_IterationSettings.TabIndex = 9;
            this.Label_IterationSettings.Text = "Iteration Settings";
            // 
            // TextBox_IterationStart
            // 
            this.TextBox_IterationStart.Location = new System.Drawing.Point(146, 240);
            this.TextBox_IterationStart.Name = "TextBox_IterationStart";
            this.TextBox_IterationStart.Size = new System.Drawing.Size(93, 31);
            this.TextBox_IterationStart.TabIndex = 8;
            this.TextBox_IterationStart.TextChanged += new System.EventHandler(this.TextBox_IterationStart_TextChanged);
            // 
            // Label_MaxStep
            // 
            this.Label_MaxStep.AutoSize = true;
            this.Label_MaxStep.Location = new System.Drawing.Point(120, 109);
            this.Label_MaxStep.Name = "Label_MaxStep";
            this.Label_MaxStep.Size = new System.Drawing.Size(85, 25);
            this.Label_MaxStep.TabIndex = 7;
            this.Label_MaxStep.Text = "Max Step";
            // 
            // TextBox_MaxStep
            // 
            this.TextBox_MaxStep.Location = new System.Drawing.Point(83, 137);
            this.TextBox_MaxStep.Name = "TextBox_MaxStep";
            this.TextBox_MaxStep.Size = new System.Drawing.Size(169, 31);
            this.TextBox_MaxStep.TabIndex = 6;
            this.TextBox_MaxStep.TextChanged += new System.EventHandler(this.TextBox_MaxStep_TextChanged);
            // 
            // Label_TresholdPercentage
            // 
            this.Label_TresholdPercentage.AutoSize = true;
            this.Label_TresholdPercentage.Location = new System.Drawing.Point(83, 38);
            this.Label_TresholdPercentage.Name = "Label_TresholdPercentage";
            this.Label_TresholdPercentage.Size = new System.Drawing.Size(169, 25);
            this.Label_TresholdPercentage.TabIndex = 5;
            this.Label_TresholdPercentage.Text = "Treshold Percentage";
            // 
            // ComboBox_TresholdPercentage
            // 
            this.ComboBox_TresholdPercentage.FormattingEnabled = true;
            this.ComboBox_TresholdPercentage.Items.AddRange(new object[] {
            "20%",
            "40%",
            "60%",
            "80%"});
            this.ComboBox_TresholdPercentage.Location = new System.Drawing.Point(83, 66);
            this.ComboBox_TresholdPercentage.Name = "ComboBox_TresholdPercentage";
            this.ComboBox_TresholdPercentage.Size = new System.Drawing.Size(169, 33);
            this.ComboBox_TresholdPercentage.TabIndex = 4;
            this.ComboBox_TresholdPercentage.SelectedIndexChanged += new System.EventHandler(this.ComboBox_TresholdPercentage_SelectedIndexChanged);
            // 
            // Label_Settings
            // 
            this.Label_Settings.AutoSize = true;
            this.Label_Settings.Location = new System.Drawing.Point(129, 0);
            this.Label_Settings.Name = "Label_Settings";
            this.Label_Settings.Size = new System.Drawing.Size(76, 25);
            this.Label_Settings.TabIndex = 3;
            this.Label_Settings.Text = "Settings";
            // 
            // CheckBox_DebugMode
            // 
            this.CheckBox_DebugMode.AutoSize = true;
            this.CheckBox_DebugMode.Location = new System.Drawing.Point(71, 425);
            this.CheckBox_DebugMode.Name = "CheckBox_DebugMode";
            this.CheckBox_DebugMode.Size = new System.Drawing.Size(196, 29);
            this.CheckBox_DebugMode.TabIndex = 1;
            this.CheckBox_DebugMode.Text = "Enable DebugMode";
            this.CheckBox_DebugMode.UseVisualStyleBackColor = true;
            this.CheckBox_DebugMode.CheckedChanged += new System.EventHandler(this.CheckBox_DebugMode_CheckedChanged);
            // 
            // Button_LoadExcel
            // 
            this.Button_LoadExcel.Location = new System.Drawing.Point(110, 369);
            this.Button_LoadExcel.Name = "Button_LoadExcel";
            this.Button_LoadExcel.Size = new System.Drawing.Size(112, 34);
            this.Button_LoadExcel.TabIndex = 0;
            this.Button_LoadExcel.Text = "Load Excel";
            this.Button_LoadExcel.UseVisualStyleBackColor = true;
            this.Button_LoadExcel.Click += new System.EventHandler(this.Button_LoadExcel_Click);
            // 
            // Label_CurrentStatus
            // 
            this.Label_CurrentStatus.AutoSize = true;
            this.Label_CurrentStatus.Location = new System.Drawing.Point(70, 35);
            this.Label_CurrentStatus.Name = "Label_CurrentStatus";
            this.Label_CurrentStatus.Size = new System.Drawing.Size(41, 25);
            this.Label_CurrentStatus.TabIndex = 4;
            this.Label_CurrentStatus.Text = "Idle";
            // 
            // Label_Status
            // 
            this.Label_Status.AutoSize = true;
            this.Label_Status.Location = new System.Drawing.Point(12, 35);
            this.Label_Status.Name = "Label_Status";
            this.Label_Status.Size = new System.Drawing.Size(64, 25);
            this.Label_Status.TabIndex = 3;
            this.Label_Status.Text = "Status:";
            // 
            // ListBox_Candidates
            // 
            this.ListBox_Candidates.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListBox_Candidates.FormattingEnabled = true;
            this.ListBox_Candidates.ItemHeight = 25;
            this.ListBox_Candidates.Location = new System.Drawing.Point(349, 63);
            this.ListBox_Candidates.Name = "ListBox_Candidates";
            this.ListBox_Candidates.Size = new System.Drawing.Size(272, 625);
            this.ListBox_Candidates.TabIndex = 1;
            // 
            // PictureBox_Progress
            // 
            this.PictureBox_Progress.Image = global::MotivationButtons.Properties.Resources.RedDot_Update;
            this.PictureBox_Progress.Location = new System.Drawing.Point(159, 38);
            this.PictureBox_Progress.Name = "PictureBox_Progress";
            this.PictureBox_Progress.Size = new System.Drawing.Size(22, 22);
            this.PictureBox_Progress.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBox_Progress.TabIndex = 5;
            this.PictureBox_Progress.TabStop = false;
            this.PictureBox_Progress.Visible = false;
            // 
            // Label_Candidates
            // 
            this.Label_Candidates.AutoSize = true;
            this.Label_Candidates.Location = new System.Drawing.Point(349, 32);
            this.Label_Candidates.Name = "Label_Candidates";
            this.Label_Candidates.Size = new System.Drawing.Size(100, 25);
            this.Label_Candidates.TabIndex = 6;
            this.Label_Candidates.Text = "Candidates";
            // 
            // ListBox_CandidatePercentile
            // 
            this.ListBox_CandidatePercentile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListBox_CandidatePercentile.FormattingEnabled = true;
            this.ListBox_CandidatePercentile.ItemHeight = 25;
            this.ListBox_CandidatePercentile.Location = new System.Drawing.Point(611, 63);
            this.ListBox_CandidatePercentile.Name = "ListBox_CandidatePercentile";
            this.ListBox_CandidatePercentile.Size = new System.Drawing.Size(171, 625);
            this.ListBox_CandidatePercentile.TabIndex = 7;
            // 
            // Label_CandidatePercentiles
            // 
            this.Label_CandidatePercentiles.AutoSize = true;
            this.Label_CandidatePercentiles.Location = new System.Drawing.Point(611, 35);
            this.Label_CandidatePercentiles.Name = "Label_CandidatePercentiles";
            this.Label_CandidatePercentiles.Size = new System.Drawing.Size(86, 25);
            this.Label_CandidatePercentiles.TabIndex = 8;
            this.Label_CandidatePercentiles.Text = "Percentile";
            // 
            // ListBox_CandidateStatus
            // 
            this.ListBox_CandidateStatus.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ListBox_CandidateStatus.FormattingEnabled = true;
            this.ListBox_CandidateStatus.ItemHeight = 25;
            this.ListBox_CandidateStatus.Location = new System.Drawing.Point(778, 63);
            this.ListBox_CandidateStatus.Name = "ListBox_CandidateStatus";
            this.ListBox_CandidateStatus.Size = new System.Drawing.Size(171, 625);
            this.ListBox_CandidateStatus.TabIndex = 9;
            // 
            // Label_CandidateStatus
            // 
            this.Label_CandidateStatus.AutoSize = true;
            this.Label_CandidateStatus.Location = new System.Drawing.Point(778, 32);
            this.Label_CandidateStatus.Name = "Label_CandidateStatus";
            this.Label_CandidateStatus.Size = new System.Drawing.Size(60, 25);
            this.Label_CandidateStatus.TabIndex = 10;
            this.Label_CandidateStatus.Text = "Status";
            // 
            // MainBody
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(945, 694);
            this.Controls.Add(this.Label_CandidateStatus);
            this.Controls.Add(this.ListBox_CandidateStatus);
            this.Controls.Add(this.Label_CandidatePercentiles);
            this.Controls.Add(this.ListBox_CandidatePercentile);
            this.Controls.Add(this.Label_Candidates);
            this.Controls.Add(this.PictureBox_Progress);
            this.Controls.Add(this.Label_CurrentStatus);
            this.Controls.Add(this.ListBox_Candidates);
            this.Controls.Add(this.Label_Status);
            this.Controls.Add(this.SidePanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainBody";
            this.Opacity = 0.95D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Motivation Buttons - Recruiter Assistant v1.0.0.0";
            this.SidePanel.ResumeLayout(false);
            this.SidePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Progress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel SidePanel;
        private System.Windows.Forms.Button Button_LoadExcel;
        public System.Windows.Forms.ListBox ListBox_Candidates;
        private System.Windows.Forms.CheckBox CheckBox_DebugMode;
        private System.Windows.Forms.Label Label_CurrentStatus;
        private System.Windows.Forms.Label Label_Status;
        private System.Windows.Forms.Label Label_Settings;
        private System.Windows.Forms.PictureBox PictureBox_Progress;
        private System.Windows.Forms.Label Label_Candidates;
        private System.Windows.Forms.Label Label_TresholdPercentage;
        private System.Windows.Forms.ComboBox ComboBox_TresholdPercentage;
        private System.Windows.Forms.Label Label_MaxStep;
        private System.Windows.Forms.TextBox TextBox_MaxStep;
        private System.Windows.Forms.ListBox ListBox_CandidatePercentile;
        private System.Windows.Forms.Label Label_CandidatePercentiles;
        private System.Windows.Forms.ListBox ListBox_CandidateStatus;
        private System.Windows.Forms.Label Label_CandidateStatus;
        private System.Windows.Forms.TextBox TextBox_IterationEnd;
        private System.Windows.Forms.Label Label_IterationEnd;
        private System.Windows.Forms.TextBox TextBox_IterationStep;
        private System.Windows.Forms.Label Label_IterationStep;
        private System.Windows.Forms.Label Label_IterationStart;
        private System.Windows.Forms.Label Label_IterationSettings;
        private System.Windows.Forms.TextBox TextBox_IterationStart;
    }
}

