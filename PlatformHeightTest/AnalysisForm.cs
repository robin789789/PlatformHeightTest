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

        #region Announce

        private Dictionary<int, decimal[]> indexDataDict;//selectIndex and data
        private int rowFilter, columnFilter;
        private const string ignoreStr = "Date,Time,Require Amount,Measuring Amount";
        private const string ignoreData = "NaN";
        private string path = string.Empty;
        private bool isPositive = true;
        private bool isLine = true;
        private static Dictionary<string, bool> member;
        public static Dictionary<string, bool> DisplayMember
        {
            get;
            set;
        }
        private enum generateModeEnum
        {
            SingleReverse,
            SingleZtypePath,
            SingleStypePath,
            MultiSelectWithReverse,
            MultiSelect
        }

        private generateModeEnum generateMode;

        #endregion

        #region UIEvent

        private void AnalysisForm_Load(object sender, EventArgs e)
        {
            resultsListBox.Items.Clear();
            HeightTestChart.ChartAreas[0].AxisY.IsStartedFromZero = false;
            HeightTestChart.ChartAreas[0].AxisX.IsStartedFromZero = false;
            HeightTestChart.ChartAreas[0].AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            initModeComboBox();
            chartZoom();
        }

        private void initModeComboBox()
        {
            modeCB.DropDownStyle = ComboBoxStyle.DropDownList;
            modeCB.Items.AddRange(Enum.GetNames(typeof(generateModeEnum)));
            modeCB.SelectedIndex = modeCB.Items.Count - 1;
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Select the HeightTest.csv";
            openFileDialog1.InitialDirectory = ".\\";
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                readCSV(openFileDialog1.FileName);
            }
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            resultsListBox.Items.Clear();
            readCSV(path);
        }

        private void modeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (modeCB.SelectedIndex != -1)
                generateMode = (generateModeEnum)modeCB.SelectedIndex;
            resultsListBox.SelectionMode = SelectionMode.One;

            if (generateMode == generateModeEnum.MultiSelect || generateMode == generateModeEnum.MultiSelectWithReverse)
            {
                resultsListBox.SelectionMode = SelectionMode.MultiExtended;
            }
            if (generateMode == generateModeEnum.MultiSelectWithReverse)
            {
                MessageBox.Show("Choose the positve lines first.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void generateBtn_Click(object sender, EventArgs e)
        {
            columnFilter = Convert.ToInt32(filterColNumericUpDown.Value);
            rowFilter = Convert.ToInt32(filterRowNumericUpDown.Value);

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

                case generateModeEnum.MultiSelectWithReverse:
                    if (isPositive)
                    {
                        multiselectWithReverseGenerate(isPositive);
                        isPositive = MessageBox.Show("Choose the negative lines then click the Generate button.", "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel;
                    }
                    else
                    {
                        multiselectWithReverseGenerate(isPositive);
                        isPositive = true;
                    }
                    break;

                default:
                    break;
            }
            HeightTestChart.ChartAreas[0].RecalculateAxesScale();
        }

        private void lineToPointBtn_Click(object sender, EventArgs e)
        {
            if (isLine)
            {
                foreach (var item in HeightTestChart.Series)
                {
                    item.ChartType = SeriesChartType.Point;
                }
                isLine = false;
            }
            else
            {
                foreach (var item in HeightTestChart.Series)
                {
                    item.ChartType = SeriesChartType.FastLine;
                }
                isLine = true;
            }
        }


        #endregion

        private void readCSV(string path)
        {
            bool tryAgain = true;
            while (tryAgain)
            {
                try
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
                    tryAgain = false;
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    {
                        tryAgain = true;
                    }
                    else
                    {
                        tryAgain = false;
                    }
                }
            }
        }

        private bool resultsValidCheck(object item)
        {
            if (item == null)
            {
                return false;
            }
            bool isIgnore = item.ToString().Contains(ignoreStr) || item.ToString().Contains(ignoreData);
            return !isIgnore;
        }

        private Series newSeries(string name)
        {
            Series series = new Series
            {
                Name = name,
                ChartType = SeriesChartType.FastLine,
                BorderWidth = 4,
                IsVisibleInLegend = true,
                IsValueShownAsLabel = true
            };
            return series;
        }

        private void multiselectGenerate()
        {
            var results = resultsListBox.SelectedItems;

            if (results != null && results.Count <= MulityLinenumericUpDown.Value)
            {
                HeightTestChart.Series.Clear();
                indexDataDict = new Dictionary<int, decimal[]>();

                foreach (var item in results)
                {
                    if (resultsValidCheck(item))
                    {
                        var buf = item.ToString().Split(',');
                        decimal[] buf2 = new decimal[buf.Length - columnFilter];
                        var series = newSeries(buf[0] + buf[1]);

                        for (int i = columnFilter; i < buf.Length; i++)
                        {
                            buf2[i - columnFilter] = Convert.ToDecimal(buf[i]);
                            series.Points.AddXY(i - columnFilter, Convert.ToDecimal(buf[i]));
                        }
                        if (!indexDataDict.TryGetValue(resultsListBox.Items.IndexOf(item), out var foo))
                        {
                            indexDataDict.Add(resultsListBox.Items.IndexOf(item), buf2);
                            HeightTestChart.Series.Add(series);
                        }
                        else
                        {
                            MessageBox.Show(buf[0] + buf[1] + " is already exist.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        private void multiselectWithReverseGenerate(bool isPositive)
        {
            var results = resultsListBox.SelectedItems;

            if (results != null)
            {
                if (isPositive)
                {
                    HeightTestChart.Series.Clear();
                    indexDataDict = new Dictionary<int, decimal[]>();

                    foreach (var item in results)
                    {
                        if (resultsValidCheck(item))
                        {
                            var buf = item.ToString().Split(',');
                            decimal[] buf2 = new decimal[buf.Length - columnFilter];
                            var series = newSeries(buf[1] + "Positive");

                            for (int i = columnFilter; i < buf.Length; i++)
                            {
                                buf2[i - columnFilter] = Convert.ToDecimal(buf[i]);
                                series.Points.AddXY(i - columnFilter, Convert.ToDecimal(buf[i]));
                            }

                            if (!indexDataDict.TryGetValue(resultsListBox.Items.IndexOf(item), out var foo))
                            {
                                indexDataDict.Add(resultsListBox.Items.IndexOf(item), buf2);
                                HeightTestChart.Series.Add(series);
                            }
                            else
                            {
                                MessageBox.Show(buf[1] + "Positive" + " is already exist.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in results)
                    {
                        if (resultsValidCheck(item))
                        {
                            var buf = item.ToString().Split(',');
                            decimal[] buf2 = new decimal[buf.Length - columnFilter];
                            var series = newSeries(buf[1] + "Negative");

                            for (int i = columnFilter; i < buf.Length; i++)
                            {
                                buf2[i - columnFilter] = Convert.ToDecimal(buf[i]);
                                series.Points.AddXY((buf.Length - 1) - i, Convert.ToDecimal(buf[i]));
                            }

                            // indexDataDict.Add(resultsListBox.Items.IndexOf(item), buf2);
                            try
                            {
                                HeightTestChart.Series.Add(series);
                            }
                            catch 
                            {
                                MessageBox.Show(buf[1] + "Negative" + " is already exist.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }                    
                        }
                    }
                }
            }
        }

        private void singleReverseGenerate()
        {
            var result = resultsListBox.SelectedItem;

            if (resultsValidCheck(result))
            {
                HeightTestChart.Series.Clear();
                indexDataDict = new Dictionary<int, decimal[]>();
                var buf = result.ToString().Split(',');
                decimal[] buf2 = new decimal[buf.Length - columnFilter];
                var series = newSeries(buf[0] + buf[1]);

                for (int i = columnFilter; i < buf.Length; i++)
                {
                    buf2[i - columnFilter] = Convert.ToDecimal(buf[i]);
                }
                Array.Reverse(buf2);
                for (int i = columnFilter; i < buf.Length; i++)
                {
                    series.Points.AddXY(i - columnFilter, buf2[i - columnFilter]);
                }

                indexDataDict.Add(resultsListBox.Items.IndexOf(result), buf2);
                HeightTestChart.Series.Add(series);
            }
        }

        private void singleSandZtypeGenerate(generateModeEnum mode)
        {
            var result = resultsListBox.SelectedItem;
            int xCount = Convert.ToInt32(xCountNumericUD.Value);
            int yCount = Convert.ToInt32(yCountNumericUD.Value);

            if (resultsValidCheck(result))
            {
                var buf = result.ToString().Split(',');
                if (xCount * yCount == buf.Length - columnFilter)
                {
                    HeightTestChart.Series.Clear();
                    indexDataDict = new Dictionary<int, decimal[]>();
                    decimal[] buf2 = new decimal[buf.Length - columnFilter];
                    Series[] series = new Series[yCount];

                    for (int i = columnFilter; i < buf.Length; i++)
                    {
                        buf2[i - columnFilter] = Convert.ToDecimal(buf[i]);
                    }
                    for (int i = 0; i < series.Length; i++)
                    {
                        series[i] = newSeries("Row: " + (i + 1).ToString());
                        HeightTestChart.Series.Add(series[i]);
                    }

                    for (int i = 0; i < yCount; i += 2)//odd row
                    {
                        for (int j = 0; j < xCount; j++)
                        {
                            series[i].Points.AddXY(j, buf2[i * xCount + j]);
                        }
                    }
                    if (mode == generateModeEnum.SingleZtypePath)
                    {
                        for (int i = 1; i < yCount; i += 2)//even row
                        {
                            for (int j = 0; j < xCount; j++)
                            {
                                series[i].Points.AddXY(j, buf2[i * xCount + j]);
                            }
                        }
                    }
                    else if (mode == generateModeEnum.SingleStypePath)
                    {
                        for (int i = 1; i < yCount; i += 2)//even row
                        {
                            for (int j = 0; j < xCount; j++)
                            {
                                series[i].Points.AddXY((xCount - 1) - j, buf2[i * xCount + j]);
                            }
                        }
                    }
                    //indexDataDict.Add(resultsListBox.Items.IndexOf(result), buf2);
                }
                else
                {
                    MessageBox.Show("X counts * Y counts not equal to the measuring points.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #region chartZoom

        private void chartZoom()
        {
            HeightTestChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            HeightTestChart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            HeightTestChart.MouseWheel += HeightTestChart_MouseWheel;
            HeightTestChart.MouseLeave += HeightTestChart_MouseLeave;
            HeightTestChart.MouseEnter += HeightTestChart_MouseEnter;
        }

        private void HeightTestChart_MouseLeave(object sender, EventArgs e)
        {
            if (HeightTestChart.Focused) HeightTestChart.Parent.Focus();
        }

        private void HeightTestChart_MouseWheel(object sender, MouseEventArgs e)
        {
            var chart = (Chart)sender;
            var xAxis = chart.ChartAreas[0].AxisX;
            var yAxis = chart.ChartAreas[0].AxisY;

            try
            {
                if (e.Delta < 0) // Scrolled down.
                {
                    xAxis.ScaleView.ZoomReset();
                    yAxis.ScaleView.ZoomReset();
                }
                else if (e.Delta > 0) // Scrolled up.
                {
                    var xMin = xAxis.ScaleView.ViewMinimum;
                    var xMax = xAxis.ScaleView.ViewMaximum;
                    var yMin = yAxis.ScaleView.ViewMinimum;
                    var yMax = yAxis.ScaleView.ViewMaximum;

                    var posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    var posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                    var posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                    var posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;

                    xAxis.ScaleView.Zoom(posXStart, posXFinish);
                    yAxis.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { }
        }

        private void HeightTestChart_MouseEnter(object sender, EventArgs e)
        {
            if (!HeightTestChart.Focused) HeightTestChart.Focus();
        }

        #endregion

        public void SeriesDisplay(string seriesName, bool isEnabled)
        {
            HeightTestChart.Series[seriesName].Enabled = isEnabled;
        }

        private void displayForm_Click(object sender, EventArgs e)
        {
            SeriesDisplayForm seriesDisplayForm = new SeriesDisplayForm();
            member = new Dictionary<string, bool>();
            foreach (var item in HeightTestChart.Series)
            {
                member.Add(item.Name, item.Enabled);
            }
            seriesDisplayForm.GenerateCheckListBox(member);
            seriesDisplayForm.ShowDialog();
            foreach (var item in DisplayMember)
            {
                SeriesDisplay(item.Key,item.Value);
            }
        }
    }
}