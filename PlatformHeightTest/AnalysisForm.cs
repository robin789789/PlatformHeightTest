using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private Dictionary<int,decimal[]> datas;//selectIndex and data
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

            if (resultsListBox.Items != null)
            {
                resultsListBox.SelectedIndex = resultsListBox.Items.Count - 1;
            }
        }

        private enum generateModeEnum
        {
            SingleReverse,
            SingleZtypePath,
            SingleStypePath,
            MultiSelect
        }

        private generateModeEnum generateMode;

        private void initModeComboBox()
        {
            modeCB.DropDownStyle = ComboBoxStyle.DropDownList;
            modeCB.Items.AddRange(Enum.GetNames(typeof(generateModeEnum)));
            modeCB.SelectedIndex = modeCB.Items.Count - 1;
        }

        private void AnalysisForm_Load(object sender, EventArgs e)
        {
            resultsListBox.Items.Clear();
            HeightTestChart.ChartAreas[0].AxisY.IsStartedFromZero = false;
            initModeComboBox();
        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            switch (generateMode)
            {
                case generateModeEnum.SingleReverse:
                    singleReverseGenerate();
                    break;

                case generateModeEnum.SingleZtypePath:
                    singleSandZtypeGenerate(generateMode);
                    break;

                case generateModeEnum.SingleStypePath:
                    singleSandZtypeGenerate(generateMode);
                    break;

                case generateModeEnum.MultiSelect:
                    multiselectGenerate();
                    break;

                default:
                    break;
            }
        }

        private void multiselectGenerate()
        {
            var results = resultsListBox.SelectedItems;

            if (results != null && results.Count <= MulityLinenumericUpDown.Value)
            {
                HeightTestChart.Series.Clear();
                datas = new Dictionary<int, decimal[]>();
                int rowFilter = Convert.ToInt32(filterColNumericUpDown.Value);

                foreach (var item in results)
                {
                    if (resultsIndexCheck(item))
                    {
                        var buf = item.ToString().Split(',');
                        int totalNums = buf.Length - rowFilter;
                        decimal[] buf2 = new decimal[totalNums];
                        var series = newSeries(buf[0] + buf[1]);

                        for (int i = rowFilter; i < buf.Length; i++)
                        {
                            buf2[i - rowFilter] = Convert.ToDecimal(buf[i]);
                            series.Points.AddXY(i - rowFilter, Convert.ToDecimal(buf[i]));
                        }
                        datas.Add(resultsListBox.Items.IndexOf(item), buf2);
                        HeightTestChart.Series.Add(series);
                    }
                }
            }
        }

        private void singleReverseGenerate()
        {
            var result = resultsListBox.SelectedItem;

            if (result != null)
            {
                HeightTestChart.Series.Clear();
                datas = new Dictionary<int, decimal[]>();
                int rowFilter = Convert.ToInt32(filterColNumericUpDown.Value);
                if (resultsIndexCheck(result))
                {
                    var buf = result.ToString().Split(',');
                    int totalNums = buf.Length - rowFilter;
                    decimal[] buf2 = new decimal[totalNums];
                    var series = newSeries(buf[0] + buf[1]);

                    for (int i = rowFilter; i < buf.Length; i++)
                    {
                        buf2[i - rowFilter] = Convert.ToDecimal(buf[i]);
                    }
                    Array.Reverse(buf2);
                    for (int i = rowFilter; i < buf.Length; i++)
                    {
                        series.Points.AddXY(i - rowFilter, buf2[i - rowFilter]);
                    }

                    datas.Add(resultsListBox.Items.IndexOf(result), buf2);
                    HeightTestChart.Series.Add(series);
                }
            }
        }

        private void singleSandZtypeGenerate(generateModeEnum mode)
        {

        }

        private Series newSeries(string name)
        {
            Series series = new Series
            {
                Name = name,
                ChartType = SeriesChartType.FastLine,
                BorderWidth = 4
            };
            return series;
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

        private void modeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modeCB.SelectedIndex != -1)
                generateMode = (generateModeEnum)modeCB.SelectedIndex;
            resultsListBox.SelectionMode = SelectionMode.One;

            if (generateMode == generateModeEnum.MultiSelect)
            {
                resultsListBox.SelectionMode = SelectionMode.MultiExtended;
            }
        }
    }
}