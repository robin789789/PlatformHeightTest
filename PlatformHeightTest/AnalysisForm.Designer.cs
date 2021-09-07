
namespace PlatformHeightTest
{
    partial class AnalysisForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.loadBtn = new System.Windows.Forms.Button();
            this.resultsListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.filterRowNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.filterColNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.GenerateBtn = new System.Windows.Forms.Button();
            this.HeightTestChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label4 = new System.Windows.Forms.Label();
            this.MulityLinenumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.RefreshBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.modeCB = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.filterRowNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterColNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightTestChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MulityLinenumericUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "逗號分隔檔案|*.csv";
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(22, 27);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(99, 31);
            this.loadBtn.TabIndex = 0;
            this.loadBtn.Text = "Load CSV";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // resultsListBox
            // 
            this.resultsListBox.FormattingEnabled = true;
            this.resultsListBox.HorizontalScrollbar = true;
            this.resultsListBox.ItemHeight = 15;
            this.resultsListBox.Location = new System.Drawing.Point(22, 104);
            this.resultsListBox.Name = "resultsListBox";
            this.resultsListBox.ScrollAlwaysVisible = true;
            this.resultsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.resultsListBox.Size = new System.Drawing.Size(582, 454);
            this.resultsListBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter:";
            // 
            // filterRowNumericUpDown
            // 
            this.filterRowNumericUpDown.Location = new System.Drawing.Point(122, 21);
            this.filterRowNumericUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.filterRowNumericUpDown.Name = "filterRowNumericUpDown";
            this.filterRowNumericUpDown.Size = new System.Drawing.Size(53, 25);
            this.filterRowNumericUpDown.TabIndex = 3;
            this.filterRowNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // filterColNumericUpDown
            // 
            this.filterColNumericUpDown.Location = new System.Drawing.Point(122, 55);
            this.filterColNumericUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.filterColNumericUpDown.Name = "filterColNumericUpDown";
            this.filterColNumericUpDown.Size = new System.Drawing.Size(53, 25);
            this.filterColNumericUpDown.TabIndex = 3;
            this.filterColNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Row";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Column";
            // 
            // GenerateBtn
            // 
            this.GenerateBtn.Location = new System.Drawing.Point(1327, 28);
            this.GenerateBtn.Name = "GenerateBtn";
            this.GenerateBtn.Size = new System.Drawing.Size(122, 65);
            this.GenerateBtn.TabIndex = 6;
            this.GenerateBtn.Text = "Generate";
            this.GenerateBtn.UseVisualStyleBackColor = true;
            this.GenerateBtn.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // HeightTestChart
            // 
            chartArea3.Name = "ChartArea1";
            this.HeightTestChart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.HeightTestChart.Legends.Add(legend3);
            this.HeightTestChart.Location = new System.Drawing.Point(638, 104);
            this.HeightTestChart.Name = "HeightTestChart";
            this.HeightTestChart.Size = new System.Drawing.Size(811, 454);
            this.HeightTestChart.TabIndex = 7;
            this.HeightTestChart.Text = "chart1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(347, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "MulityLines:";
            // 
            // MulityLinenumericUpDown
            // 
            this.MulityLinenumericUpDown.Location = new System.Drawing.Point(434, 71);
            this.MulityLinenumericUpDown.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.MulityLinenumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MulityLinenumericUpDown.Name = "MulityLinenumericUpDown";
            this.MulityLinenumericUpDown.Size = new System.Drawing.Size(51, 25);
            this.MulityLinenumericUpDown.TabIndex = 9;
            this.MulityLinenumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.Location = new System.Drawing.Point(22, 64);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(99, 32);
            this.RefreshBtn.TabIndex = 10;
            this.RefreshBtn.Text = "Refresh";
            this.RefreshBtn.UseVisualStyleBackColor = true;
            this.RefreshBtn.Click += new System.EventHandler(this.RefreshBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.filterRowNumericUpDown);
            this.groupBox1.Controls.Add(this.filterColNumericUpDown);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(141, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 91);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GeneralSetting";
            // 
            // modeCB
            // 
            this.modeCB.FormattingEnabled = true;
            this.modeCB.Location = new System.Drawing.Point(350, 27);
            this.modeCB.Name = "modeCB";
            this.modeCB.Size = new System.Drawing.Size(192, 23);
            this.modeCB.TabIndex = 12;
            this.modeCB.SelectedIndexChanged += new System.EventHandler(this.modeCB_SelectedIndexChanged);
            // 
            // AnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1461, 573);
            this.Controls.Add(this.modeCB);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.RefreshBtn);
            this.Controls.Add(this.MulityLinenumericUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.HeightTestChart);
            this.Controls.Add(this.GenerateBtn);
            this.Controls.Add(this.resultsListBox);
            this.Controls.Add(this.loadBtn);
            this.Name = "AnalysisForm";
            this.Text = "DataVisualization";
            this.Load += new System.EventHandler(this.AnalysisForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.filterRowNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterColNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightTestChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MulityLinenumericUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.ListBox resultsListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown filterRowNumericUpDown;
        private System.Windows.Forms.NumericUpDown filterColNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button GenerateBtn;
        private System.Windows.Forms.DataVisualization.Charting.Chart HeightTestChart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown MulityLinenumericUpDown;
        private System.Windows.Forms.Button RefreshBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox modeCB;
    }
}