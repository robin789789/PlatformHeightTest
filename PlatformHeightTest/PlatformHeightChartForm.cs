using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PlatformHeightTest
{
    public partial class PlatformHeightChartForm : Form
    {
        public PlatformHeightChartForm()
        {
            InitializeComponent();
        }

        private void PlatformHeightChartForm_Load(object sender, EventArgs e)
        {
            s();
        }
        private void s()
        {
            DateTime a = DateTime.Now;
            DateTime b = DateTime.Now;

            List<String> xValue = new List<string>();

            xValue.Add(a.ToString());
            xValue.Add(b.ToString());

            string[] titleArr = { "活動1", "活動2" };
            double[] yValues = { 269.23, 94 };
            double[] yValues2 = { 120.14, 116 };

            Chart1.Legends.Add("Legends1"); //圖例集合

            //設定 Chart
            Title title = new Title();
            title.Text = "統計圖";
            title.Alignment = ContentAlignment.MiddleCenter;
            title.Font = new System.Drawing.Font("Trebuchet MS", 14F, FontStyle.Bold);
            Chart1.Titles.Add(title);

            //設定 ChartArea----------------------------------------------------------------------

            //-----------------------------設定3D------------------------------//
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = true; //3D效果
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.IsClustered = true; //並排顯示
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Rotation = 40; //垂直角度
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.Inclination = 50; //水平角度
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.PointDepth = 10; //數據條厚度
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.WallWidth = 0; //外牆寬度
            Chart1.ChartAreas["ChartArea1"].Area3DStyle.LightStyle = LightStyle.Realistic; //光源
            //-----------------------------設定3D------------------------------//

            Chart1.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(240, 240, 240); //背景色
            Chart1.ChartAreas["ChartArea1"].AxisX.Enabled = AxisEnabled.True;
            Chart1.ChartAreas["ChartArea1"].AxisX2.Enabled = AxisEnabled.False; //隱藏 X2 標示
            Chart1.ChartAreas["ChartArea1"].AxisY2.Enabled = AxisEnabled.False; //隱藏 Y2 標示
            Chart1.ChartAreas["ChartArea1"].AxisY2.MajorGrid.Enabled = false;   //隱藏 Y2 軸線
            Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.FromArgb(150, 150, 150);//X 軸線顏色
            Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.FromArgb(150, 150, 150);//Y 軸線顏色


            Chart1.ChartAreas["ChartArea1"].AxisY.LabelStyle.Format = "#.###";//設定小數點

            //設定 Legends------------------------------------------------------------------------              
            Chart1.Legends["Legends1"].DockedToChartArea = "ChartArea1"; //顯示在圖表內
            //Chart1.Legends["Legends1"].Docking = Docking.Bottom; //自訂顯示位置
            Chart1.Legends["Legends1"].BackColor = Color.FromArgb(235, 235, 235); //背景色
            //斜線背景
            Chart1.Legends["Legends1"].BackHatchStyle = ChartHatchStyle.DarkDownwardDiagonal;
            Chart1.Legends["Legends1"].BorderWidth = 1;
            Chart1.Legends["Legends1"].BorderColor = Color.FromArgb(200, 200, 200);

            //設定 Series-----------------------------------------------------------------------
            //Chart1.Series["Series1"].ChartType = SeriesChartType.Line; //直條圖(Column),折線圖(Line),橫條圖(Bar)
            //Chart1.Series["Series2"].ChartType = SeriesChartType.Line; //直條圖(Column),折線圖(Line),橫條圖(Bar)
            //Chart1.Series["Series1"].ChartType = SeriesChartType.Bar; //橫條圖

            Chart1.Series["Series1"].Points.DataBindXY(xValue, yValues);//Series1的XY數值放入圖中

            Chart1.Series["Series1"].Legend = "Legends1";
            Chart1.Series["Series1"].LegendText = titleArr[0];
            Chart1.Series["Series1"].LabelFormat = "#.###"; //小數點
            Chart1.Series["Series1"].MarkerSize = 8; //Label 範圍大小
            Chart1.Series["Series1"].LabelForeColor = Color.FromArgb(0, 90, 255); //字體顏色
            //字體設定
            Chart1.Series["Series1"].Font = new System.Drawing.Font("Trebuchet MS", 10, System.Drawing.FontStyle.Bold);
            //Label 背景色
            Chart1.Series["Series1"].LabelBackColor = Color.FromArgb(150, 255, 255, 255);
            Chart1.Series["Series1"].Color = Color.FromArgb(240, 65, 140, 240); //背景色
            Chart1.Series["Series1"].IsValueShownAsLabel = true; // Show data points labels

            Chart1.Series["Series2"].Points.DataBindXY(xValue, yValues2);

            Chart1.Series["Series2"].Legend = "Legends1";
            Chart1.Series["Series2"].LegendText = titleArr[1];
            Chart1.Series["Series2"].LabelFormat = "#.###"; //小數點
            Chart1.Series["Series2"].MarkerSize = 8; //Label 範圍大小
            Chart1.Series["Series2"].LabelForeColor = Color.FromArgb(255, 103, 0);
            Chart1.Series["Series2"].Font = new System.Drawing.Font("Trebuchet MS", 10, FontStyle.Bold);
            Chart1.Series["Series2"].LabelBackColor = Color.FromArgb(150, 255, 255, 255);
            Chart1.Series["Series2"].Color = Color.FromArgb(240, 252, 180, 65); //背景色
            Chart1.Series["Series2"].IsValueShownAsLabel = true; //顯示數據

            Chart1.Series["Series3"].Color = Color.FromArgb(22, 24, 18, 65); //背景色

            Chart1.ChartAreas[0].AxisX.Interval = 1;   //設置X軸坐標的間隔為1
            Chart1.ChartAreas[0].AxisX.IntervalOffset = 1;  //設置X軸坐標偏移為1
            Chart1.ChartAreas[0].AxisX.LabelStyle.IsStaggered = true;   //設置是否交錯顯示,比如數據多的時間分成兩行來顯示
        }
    }
}
