
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
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
            ((System.ComponentModel.ISupportInitialize)(this.filterRowNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filterColNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HeightTestChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MulityLinenumericUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xCountNumericUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yCountNumericUD)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "逗號分隔檔案|*.csv";
            // 
            // loadBtn
            // 
            this.loadBtn.Location = new System.Drawing.Point(25, 32);
            this.loadBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(111, 37);
            this.loadBtn.TabIndex = 0;
            this.loadBtn.Text = "Load CSV";
            this.loadBtn.UseVisualStyleBackColor = true;
            this.loadBtn.Click += new System.EventHandler(this.loadBtn_Click);
            // 
            // resultsListBox
            // 
            this.resultsListBox.FormattingEnabled = true;
            this.resultsListBox.HorizontalScrollbar = true;
            this.resultsListBox.ItemHeight = 18;
            this.resultsListBox.Location = new System.Drawing.Point(25, 125);
            this.resultsListBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.resultsListBox.Name = "resultsListBox";
            this.resultsListBox.ScrollAlwaysVisible = true;
            this.resultsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.resultsListBox.Size = new System.Drawing.Size(654, 544);
            this.resultsListBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Filter:";
            // 
            // filterRowNumericUpDown
            // 
            this.filterRowNumericUpDown.Enabled = false;
            this.filterRowNumericUpDown.Location = new System.Drawing.Point(137, 25);
            this.filterRowNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.filterRowNumericUpDown.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.filterRowNumericUpDown.Name = "filterRowNumericUpDown";
            this.filterRowNumericUpDown.Size = new System.Drawing.Size(60, 29);
            this.filterRowNumericUpDown.TabIndex = 3;
            this.filterRowNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // filterColNumericUpDown
            // 
            this.filterColNumericUpDown.Location = new System.Drawing.Point(137, 66);
            this.filterColNumericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.filterColNumericUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.filterColNumericUpDown.Name = "filterColNumericUpDown";
            this.filterColNumericUpDown.Size = new System.Drawing.Size(60, 29);
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
            this.label2.Location = new System.Drawing.Point(72, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Row";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(72, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Column";
            // 
            // GenerateBtn
            // 
            this.GenerateBtn.Location = new System.Drawing.Point(1493, 34);
            this.GenerateBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GenerateBtn.Name = "GenerateBtn";
            this.GenerateBtn.Size = new System.Drawing.Size(137, 78);
            this.GenerateBtn.TabIndex = 6;
            this.GenerateBtn.Text = "Generate";
            this.GenerateBtn.UseVisualStyleBackColor = true;
            this.GenerateBtn.Click += new System.EventHandler(this.GenerateBtn_Click);
            // 
            // HeightTestChart
            // 
            chartArea1.Name = "ChartArea1";
            this.HeightTestChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.HeightTestChart.Legends.Add(legend1);
            this.HeightTestChart.Location = new System.Drawing.Point(718, 125);
            this.HeightTestChart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.HeightTestChart.Name = "HeightTestChart";
            this.HeightTestChart.Size = new System.Drawing.Size(912, 545);
            this.HeightTestChart.TabIndex = 7;
            this.HeightTestChart.Text = "chart1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(390, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "MulityLines:";
            // 
            // MulityLinenumericUpDown
            // 
            this.MulityLinenumericUpDown.Location = new System.Drawing.Point(488, 85);
            this.MulityLinenumericUpDown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.MulityLinenumericUpDown.Size = new System.Drawing.Size(57, 29);
            this.MulityLinenumericUpDown.TabIndex = 9;
            this.MulityLinenumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // RefreshBtn
            // 
            this.RefreshBtn.Location = new System.Drawing.Point(25, 77);
            this.RefreshBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RefreshBtn.Name = "RefreshBtn";
            this.RefreshBtn.Size = new System.Drawing.Size(111, 38);
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
            this.groupBox1.Location = new System.Drawing.Point(159, 8);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(225, 109);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "GeneralSetting";
            // 
            // modeCB
            // 
            this.modeCB.FormattingEnabled = true;
            this.modeCB.Location = new System.Drawing.Point(394, 32);
            this.modeCB.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.modeCB.Name = "modeCB";
            this.modeCB.Size = new System.Drawing.Size(216, 26);
            this.modeCB.TabIndex = 12;
            this.modeCB.SelectedIndexChanged += new System.EventHandler(this.modeCB_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(740, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 18);
            this.label5.TabIndex = 13;
            this.label5.Text = "Axis:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(740, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 18);
            this.label6.TabIndex = 13;
            this.label6.Text = "Axis:";
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(791, 30);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(49, 26);
            this.comboBox1.TabIndex = 14;
            this.comboBox1.Text = "X";
            // 
            // comboBox2
            // 
            this.comboBox2.Enabled = false;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(791, 70);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(49, 26);
            this.comboBox2.TabIndex = 14;
            this.comboBox2.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(846, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 18);
            this.label7.TabIndex = 13;
            this.label7.Text = "Count:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(846, 74);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 18);
            this.label8.TabIndex = 13;
            this.label8.Text = "Count:";
            // 
            // xCountNumericUD
            // 
            this.xCountNumericUD.Location = new System.Drawing.Point(899, 29);
            this.xCountNumericUD.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.xCountNumericUD.Name = "xCountNumericUD";
            this.xCountNumericUD.Size = new System.Drawing.Size(64, 29);
            this.xCountNumericUD.TabIndex = 15;
            this.xCountNumericUD.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // yCountNumericUD
            // 
            this.yCountNumericUD.Location = new System.Drawing.Point(899, 69);
            this.yCountNumericUD.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.yCountNumericUD.Name = "yCountNumericUD";
            this.yCountNumericUD.Size = new System.Drawing.Size(64, 29);
            this.yCountNumericUD.TabIndex = 15;
            this.yCountNumericUD.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // AnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1644, 688);
            this.Controls.Add(this.yCountNumericUD);
            this.Controls.Add(this.xCountNumericUD);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.modeCB);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.RefreshBtn);
            this.Controls.Add(this.MulityLinenumericUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.HeightTestChart);
            this.Controls.Add(this.GenerateBtn);
            this.Controls.Add(this.resultsListBox);
            this.Controls.Add(this.loadBtn);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AnalysisForm";
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
    }
}