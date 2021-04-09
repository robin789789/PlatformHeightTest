namespace PlatformHeightTest
{
    partial class PlatformHeightChartForm
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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.Chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // Chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.Chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.Chart1.Legends.Add(legend1);
            this.Chart1.Location = new System.Drawing.Point(12, 12);
            this.Chart1.Name = "Chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series2.Legend = "Legend1";
            series2.Name = "Series2";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series3";
            series4.ChartArea = "ChartArea1";
            series4.Legend = "Legend1";
            series4.Name = "Series4";
            this.Chart1.Series.Add(series1);
            this.Chart1.Series.Add(series2);
            this.Chart1.Series.Add(series3);
            this.Chart1.Series.Add(series4);
            this.Chart1.Size = new System.Drawing.Size(1051, 709);
            this.Chart1.TabIndex = 0;
            this.Chart1.Text = "chart1";
            // 
            // PlatformHeightChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 733);
            this.Controls.Add(this.Chart1);
            this.Name = "PlatformHeightChartForm";
            this.Text = "PlatformHeightChartForm";
            this.Load += new System.EventHandler(this.PlatformHeightChartForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart Chart1;
    }
}