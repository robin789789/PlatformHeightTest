
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
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
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.xCountNumericUD = new System.Windows.Forms.NumericUpDown();
            this.yCountNumericUD = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lineToPointBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.filterRowNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterColNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightTestChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MulityLinenumericUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xCountNumericUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yCountNumericUD)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "逗號分隔檔案|*.csv";
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(22, 19);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(99, 36);
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
            this.filterRowNumericUpDown.Enabled = false;
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
            this.GenerateBtn.Location = new System.Drawing.Point(834, 51);
            this.GenerateBtn.Name = "GenerateBtn";
            this.GenerateBtn.Size = new System.Drawing.Size(251, 34);
            this.GenerateBtn.TabIndex = 6;
            this.GenerateBtn.Text = "Generate";
            this.GenerateBtn.UseVisualStyleBackColor = true;
            this.GenerateBtn.Click += new System.EventHandler(this.generateBtn_Click);
            // 
            // HeightTestChart
            // 
            chartArea2.Name = "ChartArea1";
            this.HeightTestChart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.HeightTestChart.Legends.Add(legend2);
            this.HeightTestChart.Location = new System.Drawing.Point(638, 104);
            this.HeightTestChart.Name = "HeightTestChart";
            this.HeightTestChart.Size = new System.Drawing.Size(811, 454);
            this.HeightTestChart.TabIndex = 7;
            this.HeightTestChart.Text = "chart1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "MulityLines:";
            // 
            // MulityLinenumericUpDown
            // 
            this.MulityLinenumericUpDown.Location = new System.Drawing.Point(103, 29);
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
            this.RefreshBtn.Location = new System.Drawing.Point(22, 60);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(99, 38);
            this.RefreshBtn.TabIndex = 10;
            this.RefreshBtn.Text = "Refresh";
            this.RefreshBtn.UseVisualStyleBackColor = true;
            this.RefreshBtn.Click += new System.EventHandler(this.refreshBtn_Click);
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
            this.modeCB.Location = new System.Drawing.Point(893, 22);
            this.modeCB.Name = "modeCB";
            this.modeCB.Size = new System.Drawing.Size(192, 23);
            this.modeCB.TabIndex = 12;
            this.modeCB.SelectedIndexChanged += new System.EventHandler(this.modeCB_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "Axis:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Axis:";
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(51, 20);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(44, 23);
            this.comboBox1.TabIndex = 14;
            this.comboBox1.Text = "X";
            // 
            // comboBox2
            // 
            this.comboBox2.Enabled = false;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(51, 53);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(44, 23);
            this.comboBox2.TabIndex = 14;
            this.comboBox2.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(100, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "Count:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(100, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 15);
            this.label8.TabIndex = 13;
            this.label8.Text = "Count:";
            // 
            // xCountNumericUD
            // 
            this.xCountNumericUD.Location = new System.Drawing.Point(151, 19);
            this.xCountNumericUD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xCountNumericUD.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.xCountNumericUD.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.xCountNumericUD.Name = "xCountNumericUD";
            this.xCountNumericUD.Size = new System.Drawing.Size(57, 25);
            this.xCountNumericUD.TabIndex = 15;
            this.xCountNumericUD.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // yCountNumericUD
            // 
            this.yCountNumericUD.Location = new System.Drawing.Point(151, 52);
            this.yCountNumericUD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.yCountNumericUD.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.yCountNumericUD.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.yCountNumericUD.Name = "yCountNumericUD";
            this.yCountNumericUD.Size = new System.Drawing.Size(57, 25);
            this.yCountNumericUD.TabIndex = 15;
            this.yCountNumericUD.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(831, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 15);
            this.label9.TabIndex = 16;
            this.label9.Text = "Mode:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.yCountNumericUD);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.xCountNumericUD);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Location = new System.Drawing.Point(584, 7);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 91);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "ZandS Path Setting";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.MulityLinenumericUpDown);
            this.groupBox3.Location = new System.Drawing.Point(360, 7);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 91);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "MulitySelect Setting";
            // 
            // lineToPointBtn
            // 
            this.lineToPointBtn.Location = new System.Drawing.Point(1123, 21);
            this.lineToPointBtn.Name = "lineToPointBtn";
            this.lineToPointBtn.Size = new System.Drawing.Size(131, 23);
            this.lineToPointBtn.TabIndex = 19;
            this.lineToPointBtn.Text = "Line→Point";
            this.lineToPointBtn.UseVisualStyleBackColor = true;
            this.lineToPointBtn.Click += new System.EventHandler(this.lineToPointBtn_Click);
            // 
            // AnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1461, 573);
            this.Controls.Add(this.lineToPointBtn);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.modeCB);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.RefreshBtn);
            this.Controls.Add(this.HeightTestChart);
            this.Controls.Add(this.GenerateBtn);
            this.Controls.Add(this.resultsListBox);
            this.Controls.Add(this.loadBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AnalysisForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DataVisualization";
            this.Load += new System.EventHandler(this.AnalysisForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.filterRowNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterColNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightTestChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MulityLinenumericUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xCountNumericUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yCountNumericUD)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown xCountNumericUD;
        private System.Windows.Forms.NumericUpDown yCountNumericUD;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button lineToPointBtn;
    }
}