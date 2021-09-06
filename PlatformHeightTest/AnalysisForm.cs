using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PlatformHeightTest
{
    public partial class AnalysisForm : Form
    {
        public AnalysisForm()
        {
            InitializeComponent();
        }
        private string path = string.Empty;
        private void loadBtn_Click(object sender, EventArgs e)
        {         
            openFileDialog1.Title = "Select file";
            openFileDialog1.InitialDirectory = ".\\";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                readCSV(openFileDialog1.FileName);
            }
        }
        private void readCSV(string path)
        {
            var strRead = File.ReadLines(path).ToList();
            
            foreach (var item in strRead)
            {
                resultsListBox.Items.Add(item);
            }

            if (resultsListBox.Items!=null)
            {
                resultsListBox.SelectedIndex = resultsListBox.Items.Count - 1;
            }
        }
       
        private void AnalysisForm_Load(object sender, EventArgs e)
        {
            resultsListBox.Items.Clear();
            HeightTestChart.ChartAreas[0].AxisY.IsStartedFromZero = false;
        }
        List<decimal[]> datas;
        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            var results = resultsListBox.SelectedItems;

            if (results != null && results.Count <= MulityLinenumericUpDown.Value)
            {
                HeightTestChart.Series.Clear();
                datas = new List<decimal[]>();
                int rowFilter = Convert.ToInt32(filterColNumericUpDown.Value);

                foreach (var item in results)
                {
                    if (resultsIndexCheck(item))
                    {
                        var buf = item.ToString().Split(',');
                        int totalNums = buf.Length - rowFilter;
                        decimal[] buf2 = new decimal[totalNums];
                        Series series = new Series
                        {
                            Name = buf[0] + buf[1],
                            ChartType = SeriesChartType.FastLine,
                            BorderWidth = 4
                        };

                        for (int i = rowFilter; i < buf.Length; i++)
                        {
                            buf2[i - rowFilter] = Convert.ToDecimal(buf[i]);
                            series.Points.AddXY(i - rowFilter, Convert.ToDecimal(buf[i]));
                        }                   
                        datas.Add(buf2);
                        HeightTestChart.Series.Add(series);
                    }
                }
            }
        }

        private bool resultsIndexCheck(object item)
        {
            var isOKint = resultsListBox.Items.IndexOf(item);
            if (isOKint == -1 || isOKint < Convert.ToInt32(filterRowNumericUpDown.Value))
                return false;
            else
                return true;
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            resultsListBox.Items.Clear();
            readCSV(path);
        }
    }
}
