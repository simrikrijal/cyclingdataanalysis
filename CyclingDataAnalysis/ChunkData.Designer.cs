namespace CyclingDataAnalysis
{
    partial class ChunkData
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
            this.OK = new System.Windows.Forms.Button();
            this.chunk = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.inputValue = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(253, 189);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 1;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // chunk
            // 
            this.chunk.AutoSize = true;
            this.chunk.Location = new System.Drawing.Point(111, 136);
            this.chunk.Name = "chunk";
            this.chunk.Size = new System.Drawing.Size(35, 13);
            this.chunk.TabIndex = 4;
            this.chunk.Text = "label1";
            this.chunk.Click += new System.EventHandler(this.chunk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // inputValue
            // 
            this.inputValue.Location = new System.Drawing.Point(253, 129);
            this.inputValue.Name = "inputValue";
            this.inputValue.Size = new System.Drawing.Size(169, 20);
            this.inputValue.TabIndex = 7;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.comboBox1.Location = new System.Drawing.Point(253, 88);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.Visible = false;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // ChunkData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 256);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.inputValue);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chunk);
            this.Controls.Add(this.OK);
            this.Name = "ChunkData";
            this.Text = "ChunkData";
            this.Load += new System.EventHandler(this.ChunkData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Label chunk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox inputValue;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}