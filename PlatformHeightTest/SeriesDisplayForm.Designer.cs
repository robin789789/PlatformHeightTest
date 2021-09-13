
namespace PlatformHeightTest
{
    partial class SeriesDisplayForm
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
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.OKBtn = new System.Windows.Forms.Button();
            this.AllCheckBox = new System.Windows.Forms.CheckBox();
            this.OddEvenCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.ColumnWidth = 140;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(12, 68);
            this.checkedListBox1.MultiColumn = true;
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(388, 344);
            this.checkedListBox1.TabIndex = 0;
            // 
            // OKBtn
            // 
            this.OKBtn.Location = new System.Drawing.Point(216, 12);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(187, 50);
            this.OKBtn.TabIndex = 1;
            this.OKBtn.Text = "OK";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // AllCheckBox
            // 
            this.AllCheckBox.AutoSize = true;
            this.AllCheckBox.Location = new System.Drawing.Point(37, 29);
            this.AllCheckBox.Name = "AllCheckBox";
            this.AllCheckBox.Size = new System.Drawing.Size(47, 19);
            this.AllCheckBox.TabIndex = 2;
            this.AllCheckBox.Text = "All";
            this.AllCheckBox.UseVisualStyleBackColor = true;
            this.AllCheckBox.CheckedChanged += new System.EventHandler(this.AllCheckBox_CheckedChanged);
            // 
            // OddEvenCheckBox
            // 
            this.OddEvenCheckBox.AutoSize = true;
            this.OddEvenCheckBox.Location = new System.Drawing.Point(107, 29);
            this.OddEvenCheckBox.Name = "OddEvenCheckBox";
            this.OddEvenCheckBox.Size = new System.Drawing.Size(86, 19);
            this.OddEvenCheckBox.TabIndex = 2;
            this.OddEvenCheckBox.Text = "Odd/Even";
            this.OddEvenCheckBox.UseVisualStyleBackColor = true;
            this.OddEvenCheckBox.CheckedChanged += new System.EventHandler(this.OddEvenCheckBox_CheckedChanged);
            // 
            // SeriesDisplayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 423);
            this.Controls.Add(this.OddEvenCheckBox);
            this.Controls.Add(this.AllCheckBox);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.checkedListBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SeriesDisplayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DisplaySeries";
            this.Load += new System.EventHandler(this.SeriesDisplayForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.CheckBox AllCheckBox;
        private System.Windows.Forms.CheckBox OddEvenCheckBox;
    }
}