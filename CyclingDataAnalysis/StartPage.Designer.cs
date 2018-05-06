namespace CyclingDataAnalysis
{
    partial class StartPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.browseFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.viewFile2 = new System.Windows.Forms.LinkLabel();
            this.sDate2 = new System.Windows.Forms.Label();
            this.sDate = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ViewFile = new System.Windows.Forms.LinkLabel();
            this.mfile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // browseFile
            // 
            this.browseFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.browseFile.Location = new System.Drawing.Point(520, 265);
            this.browseFile.Name = "browseFile";
            this.browseFile.Size = new System.Drawing.Size(200, 64);
            this.browseFile.TabIndex = 0;
            this.browseFile.Text = "Single File Data Analysis";
            this.browseFile.UseVisualStyleBackColor = true;
            this.browseFile.Click += new System.EventHandler(this.browseFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cambria", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(65, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(309, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cycling Data Analysis";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(205, 425);
            this.monthCalendar1.MinimumSize = new System.Drawing.Size(100, 0);
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 3;
            this.monthCalendar1.Visible = false;
            this.monthCalendar1.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.viewFile2);
            this.panel1.Controls.Add(this.sDate2);
            this.panel1.Controls.Add(this.sDate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.ViewFile);
            this.panel1.Location = new System.Drawing.Point(457, 425);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(324, 148);
            this.panel1.TabIndex = 5;
            this.panel1.Visible = false;
            // 
            // viewFile2
            // 
            this.viewFile2.AutoSize = true;
            this.viewFile2.Location = new System.Drawing.Point(17, 126);
            this.viewFile2.Name = "viewFile2";
            this.viewFile2.Size = new System.Drawing.Size(55, 13);
            this.viewFile2.TabIndex = 9;
            this.viewFile2.TabStop = true;
            this.viewFile2.Text = "linkLabel1";
            this.viewFile2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.viewFile2_LinkClicked);
            // 
            // sDate2
            // 
            this.sDate2.AutoSize = true;
            this.sDate2.Location = new System.Drawing.Point(37, 99);
            this.sDate2.Name = "sDate2";
            this.sDate2.Size = new System.Drawing.Size(35, 13);
            this.sDate2.TabIndex = 8;
            this.sDate2.Text = "label4";
            // 
            // sDate
            // 
            this.sDate.AutoSize = true;
            this.sDate.Location = new System.Drawing.Point(37, 34);
            this.sDate.Name = "sDate";
            this.sDate.Size = new System.Drawing.Size(73, 13);
            this.sDate.TabIndex = 7;
            this.sDate.Text = "Select A Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(183, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Time Summary";
            // 
            // ViewFile
            // 
            this.ViewFile.AutoSize = true;
            this.ViewFile.Location = new System.Drawing.Point(17, 63);
            this.ViewFile.Name = "ViewFile";
            this.ViewFile.Size = new System.Drawing.Size(89, 13);
            this.ViewFile.TabIndex = 5;
            this.ViewFile.TabStop = true;
            this.ViewFile.Text = "Sirf The Calendar";
            this.ViewFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.sDate_LinkClicked);
            // 
            // mfile
            // 
            this.mfile.Location = new System.Drawing.Point(520, 166);
            this.mfile.Name = "mfile";
            this.mfile.Size = new System.Drawing.Size(200, 64);
            this.mfile.TabIndex = 6;
            this.mfile.Text = "Multiple Files Comparison";
            this.mfile.UseVisualStyleBackColor = true;
            this.mfile.Click += new System.EventHandler(this.mfile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(493, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(254, 22);
            this.label2.TabIndex = 7;
            this.label2.Text = "Select any of the option below:";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(68, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(292, 117);
            this.label4.TabIndex = 8;
            this.label4.Text = "Welcome to Cycling Data Analysis Application based on the college assignment of T" +
    "he British College for Semester B, Level 06.";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // StartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(833, 439);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mfile);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.browseFile);
            this.Name = "StartPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cycling Data Analysis";
            this.Load += new System.EventHandler(this.StartPage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button browseFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MonthCalendar monthCalendar1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel ViewFile;
        private System.Windows.Forms.Label sDate;
        private System.Windows.Forms.LinkLabel viewFile2;
        private System.Windows.Forms.Label sDate2;
        private System.Windows.Forms.Button mfile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
    }
}