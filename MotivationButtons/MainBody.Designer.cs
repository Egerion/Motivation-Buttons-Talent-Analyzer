
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
            this.SidePanel = new System.Windows.Forms.Panel();
            this.Button_LoadExcel = new System.Windows.Forms.Button();
            this.ListBox_Candidates = new System.Windows.Forms.ListBox();
            this.SidePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SidePanel
            // 
            this.SidePanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.SidePanel.Controls.Add(this.Button_LoadExcel);
            this.SidePanel.Location = new System.Drawing.Point(-1, -1);
            this.SidePanel.Name = "SidePanel";
            this.SidePanel.Size = new System.Drawing.Size(290, 724);
            this.SidePanel.TabIndex = 0;
            // 
            // Button_LoadExcel
            // 
            this.Button_LoadExcel.Location = new System.Drawing.Point(81, 68);
            this.Button_LoadExcel.Name = "Button_LoadExcel";
            this.Button_LoadExcel.Size = new System.Drawing.Size(112, 34);
            this.Button_LoadExcel.TabIndex = 0;
            this.Button_LoadExcel.Text = "Load Excel";
            this.Button_LoadExcel.UseVisualStyleBackColor = true;
            this.Button_LoadExcel.Click += new System.EventHandler(this.Button_LoadExcel_Click);
            // 
            // ListBox_Candidates
            // 
            this.ListBox_Candidates.FormattingEnabled = true;
            this.ListBox_Candidates.ItemHeight = 25;
            this.ListBox_Candidates.Location = new System.Drawing.Point(315, 67);
            this.ListBox_Candidates.Name = "ListBox_Candidates";
            this.ListBox_Candidates.Size = new System.Drawing.Size(336, 629);
            this.ListBox_Candidates.TabIndex = 1;
            // 
            // MainBody
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1262, 723);
            this.Controls.Add(this.ListBox_Candidates);
            this.Controls.Add(this.SidePanel);
            this.Name = "MainBody";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Motivation Buttons";
            this.SidePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel SidePanel;
        private System.Windows.Forms.Button Button_LoadExcel;
        public System.Windows.Forms.ListBox ListBox_Candidates;
    }
}

