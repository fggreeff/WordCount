namespace WordCountApplication
{
    partial class WordCount
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
            this.btnWordCount = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnWordCountTwo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnWordCount
            // 
            this.btnWordCount.Location = new System.Drawing.Point(12, 12);
            this.btnWordCount.Name = "btnWordCount";
            this.btnWordCount.Size = new System.Drawing.Size(151, 23);
            this.btnWordCount.TabIndex = 1;
            this.btnWordCount.Text = "Word Count 1";
            this.btnWordCount.UseVisualStyleBackColor = true;
            this.btnWordCount.Click += new System.EventHandler(this.btnWordCount_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(12, 137);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(480, 211);
            this.txtOutput.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(416, 361);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnWordCountTwo
            // 
            this.btnWordCountTwo.Location = new System.Drawing.Point(12, 41);
            this.btnWordCountTwo.Name = "btnWordCountTwo";
            this.btnWordCountTwo.Size = new System.Drawing.Size(151, 23);
            this.btnWordCountTwo.TabIndex = 4;
            this.btnWordCountTwo.Text = "Word Count 2";
            this.btnWordCountTwo.UseVisualStyleBackColor = true;
            this.btnWordCountTwo.Click += new System.EventHandler(this.btnWordCountTwo_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(189, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Using stream reader, read whole file and process it";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(189, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Using file stream, process line by line in file";
            // 
            // WordCount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 396);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnWordCountTwo);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnWordCount);
            this.Name = "WordCount";
            this.Text = "Count words from text";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnWordCount;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnWordCountTwo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

