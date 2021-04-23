using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Button = System.Windows.Forms.Button;

namespace PlatformHeightTest
{
    public partial class PlatformHeightTest : Form
    {
        public PlatformHeightTest()
        {
            InitializeComponent();
            createButtons();
            shapeinfo.Init9PointSquare();
        }

        #region Announce

        private FileSystemWatcher watcher;
        private const string v2kPathFolder = @".\HeightRecognition";
        private Button[] btns;
        private double[] data;
        private int[] way;//Z+S
        private string lastData = "";
        private Color maxColor = Color.Red;
        private Color minColor = Color.LimeGreen;
        private Color nGColor = Color.Red;
        private Color oKColor = Color.LimeGreen;
        private Color initColor = Color.White;
        private bool extendForm = false; private bool extendForm2 = false;
        private Size unExtend = new Size(480, 425);
        private Size extend = new Size(840, 425);
        private Size extend2 = new Size(0, 200);
        private decimal catchOffsetHeight;
        private bool paintType; private double[] sortAryForColor;

        public class ShapeInfo
        {
            public int PointCnt { get; set; }
            public int ShapeLength { get; set; }
            public int ShapeWidth { get; set; }

            public void Init9PointSquare()
            {
                PointCnt = 9;
                ShapeLength = 3;
                ShapeWidth = 3;
            }

            public void testRec()
            {
                PointCnt = 10;
                ShapeLength = 2;
                ShapeWidth = 5;
            }

            public void singlePoint()
            {
                PointCnt = 1;
                ShapeLength = 1;
                ShapeWidth = 1;
            }
        }
        public class OffsetInfo
        {
            public int offsetNum = 5;
            public int ImageIndex { get; set; }
            public int HeightIndex { get; set; }
            public double[] XOffset { get; set; }
            public double[] YOffset { get; set; }
            public decimal[] ZOffset { get; set; }
            public void Init()
            {
                XOffset = new double[offsetNum];
                YOffset = new double[offsetNum];
                ZOffset = new decimal[offsetNum];
            }
            public void Reverse()
            {
                XOffset.Reverse();
                YOffset.Reverse();
                ZOffset.Reverse();
            }
        }
        #region NPOI Excel
        public class XlsxFormat
        {
            public string sheetOfCTQ { get; set; }
            public string startColumn { get; set; }
            public int startRow { get; set; }
            public string endColumn { get; set; }
            public int endRow { get; set; }

            public void HeightTestFormat()
            {
                sheetOfCTQ = "工作表1";
                startColumn = "B";
                startRow = 56;
                endColumn = "D";
                endRow = 60;
            }
            public void OffsetFormat()
            {
                sheetOfCTQ = "工作表1";
                startColumn = "B";
                startRow = 69;
                endColumn = "D";
                endRow = 73;
            }
        }
        #endregion NPOI Excel

        private ShapeInfo shapeinfo = new ShapeInfo();
        private OffsetInfo offsetInfo = new OffsetInfo();
        #endregion Announce

        private void PlatformHeightTest_Load(object sender, EventArgs e)
        {
            Timer.Enabled = true;
            initFlowDirectionCB();
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            Button.CheckForIllegalCrossThreadCalls = false;

            #region UI setting

            this.Size = unExtend;
            CenterToScreen();
            createButtons();
            PathTypeCB.SelectedIndex = 0;
            OKpictureBox.Visible = false;
            NGpictureBox.Visible = false;
            ExtendBtn.MouseHover += btn_MouseHover;
            ExtendBtn.MouseLeave += btn_MouseLeave;
            SetBtnStyle(ExtendBtn);
            initListView(AllListView);

            #endregion UI setting

            try
            {
                startWatchHeight(v2kPathFolder);
                this.Text = "HeightTest " + "Path:" + v2kPathFolder;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + "請手動添加測高資料夾位址", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #region tooltip

            toolTip.ToolTipIcon = ToolTipIcon.Info;
            toolTip.ForeColor = Color.Blue;
            toolTip.BackColor = Color.Gray;
            toolTip.AutoPopDelay = 4000;
            toolTip.ToolTipTitle = "Tips";

            toolTip.RemoveAll();
            tips(paintType);

            #endregion tooltip
        }

        private void createButtons()
        {
            this.flowLayoutPanel1.Controls.Clear();
            btns = new Button[shapeinfo.PointCnt];
            int size = newSize(shapeinfo.ShapeWidth);
            if (shapeinfo.ShapeLength > shapeinfo.ShapeWidth)
            { size = newSize(shapeinfo.ShapeLength); }

            for (int i = 0; i < btns.Length; i++)
            {
                btns[i] = new Button
                {
                    Name = i.ToString(),
                    Size = new Size(size, size),
                    Margin = new Padding(0, 0, 0, 0),
                    //Font = new Font("新細明體", 8,FontStyle.Regular)
                };
                btns[i].Click += eachPointBtn_Click;
                if ((i + 1) % shapeinfo.ShapeWidth == 0)
                {
                    this.flowLayoutPanel1.SetFlowBreak(btns[i], true);
                }
                this.flowLayoutPanel1.Controls.Add(btns[i]);
            }
        }

        private void eachPointBtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            MessageBox.Show(button.Text);
        }

        private int newSize(int sideLengthOfSquare)
        {
            int result = 0;
            switch (sideLengthOfSquare)
            {
                case 1:
                    result = 250;
                    break;

                case 2:
                    result = 120;
                    break;

                case 3:
                    result = 80;
                    break;

                case 4:
                    result = 60;

                    break;

                case 5:
                    result = 45;

                    break;

                case 6:
                    result = 40;

                    break;

                case 7:
                    result = 35;

                    break;

                case 8:
                    result = 30;

                    break;

                case 9:
                    result = 25;

                    break;

                case 10:
                    result = 24;

                    break;

                case 11:
                    result = 22;

                    break;

                case 12:
                    result = 20;

                    break;

                default:
                    result = 24;

                    break;
            }
            return result;
        }

        private void PathTypeCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            way = new int[shapeinfo.PointCnt];
            way = ZS(PathTypeCB.Text);

            if (watcher != null)
            {
                if (data != null)
                    if (data.Length >= shapeinfo.PointCnt)
                        dataChanged();
            }
            else
            {
                foreach (var bt in btns)
                {
                    bt.Text = "第 " + way[int.Parse(bt.Name)].ToString() + " 點";
                    bt.BackColor = initColor;
                }
            }
        }

        private void GenerateBtn_Click(object sender, EventArgs e)
        {
            shapeinfo.ShapeLength = Convert.ToInt32(LengthNUD.Value);
            shapeinfo.ShapeWidth = Convert.ToInt32(WidthNUD.Value);
            shapeinfo.PointCnt = shapeinfo.ShapeLength * shapeinfo.ShapeWidth;
            way = ZS(PathTypeCB.Text);
            createButtons();
            tips(paintType);
            foreach (var bt in btns)
            {
                bt.Text = "第 " + way[int.Parse(bt.Name)].ToString() + " 點";
                bt.BackColor = initColor;
            }
        }

        #region Xlms Export

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            //setExcel();
            XlsxFormat format = new XlsxFormat();
            format.HeightTestFormat();

            if (OKListView.Items.Count == 5)
            {
                double[,] result = new double[5, 3];
                for (int i = 0; i < OKListView.Items.Count; i++)
                {
                    result[i, 0] = double.Parse(OKListView.Items[i].SubItems[1].Text);
                    result[i, 1] = double.Parse(OKListView.Items[i].SubItems[2].Text);
                    result[i, 2] = double.Parse(OKListView.Items[i].SubItems[3].Text);
                }
                npoiSetExcel(format, result);
            }
            else
            {
                MessageBox.Show("At least five records are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setExcel(XlsxFormat xlsxFormat)
        {
            if (OKListView.Items.Count == 5)
            {
                double[,] result = new double[5, 2];
                for (int i = 0; i < OKListView.Items.Count; i++)
                {
                    result[i, 0] = double.Parse(OKListView.Items[i].SubItems[1].Text);
                    result[i, 1] = double.Parse(OKListView.Items[i].SubItems[2].Text);
                }

                OpenFileDialog dialog = new OpenFileDialog()
                {
                    RestoreDirectory = true,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    Filter = "Excel Work|*.xlsx",
                    Title = "Open 檢核表.xlsx File"
                };
                dialog.ShowDialog();
                Microsoft.Office.Interop.Excel.Application App = new Microsoft.Office.Interop.Excel.Application();

                if (!string.IsNullOrEmpty(dialog.FileName))
                {
                    string leftTop = xlsxFormat.startColumn + xlsxFormat.startRow.ToString();
                    string rightBottm = xlsxFormat.endColumn + xlsxFormat.endRow.ToString();
                    Microsoft.Office.Interop.Excel.Workbook Wbook = App.Workbooks.Open(dialog.FileName);
                    System.IO.FileInfo xlsAttribute = new FileInfo(dialog.FileName);
                    xlsAttribute.Attributes = FileAttributes.Normal;
                    Microsoft.Office.Interop.Excel.Worksheet Wsheet = (Microsoft.Office.Interop.Excel.Worksheet)Wbook.Sheets[xlsxFormat.sheetOfCTQ];
                    Microsoft.Office.Interop.Excel.Range aRangeChange = Wsheet.get_Range(leftTop, rightBottm);
                    aRangeChange.Value2 = result;
                    aRangeChange.NumberFormat = "0.000";

                    Wbook.Save();
                    Wbook.Close();
                    App.Quit();
                }
            }
            else
            {
                MessageBox.Show("Data null.");
            }
        }

        private void npoiSetExcel(XlsxFormat xlsxformat, double[,] data)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                RestoreDirectory = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = "Excel Work|*.xlsx",
                Title = "Open 檢核表.xlsx File"
            };
            dialog.ShowDialog();

            if (!string.IsNullOrEmpty(dialog.FileName))
            {
                IWorkbook templateWorkbook;
                try
                {
                    if (isOpen(dialog.FileName))
                    {
                        if (MessageBox.Show("Writing to the" + dialog.SafeFileName + "?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            using (FileStream fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read))
                            {
                                templateWorkbook = new XSSFWorkbook(fs);
                            }

                            #region setting style

                            XSSFCellStyle cellStyle = (XSSFCellStyle)templateWorkbook.CreateCellStyle();
                            XSSFDataFormat format = (XSSFDataFormat)templateWorkbook.CreateDataFormat();
                            XSSFFont font = (XSSFFont)templateWorkbook.CreateFont();

                            cellStyle.DataFormat = format.GetFormat("0.000");
                            font.FontName = "Calibri";
                            font.FontHeightInPoints = 12;
                            cellStyle.SetFont(font);
                            cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                            cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                            cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                            cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

                            #endregion setting style

                            string sheetName = xlsxformat.sheetOfCTQ;
                            ISheet sheet = templateWorkbook.GetSheet(sheetName) ?? templateWorkbook.CreateSheet(sheetName);

                            int _startCloumn = NumberFromExcelColumn(xlsxformat.startColumn) - 1;//from [0] start first
                            int _endColumn = NumberFromExcelColumn(xlsxformat.endColumn) - 1;
                            int cloumnQTY = _endColumn - _startCloumn;
                            int rowQTY = xlsxformat.endRow - xlsxformat.startRow;
                            for (int i = 0; i < rowQTY + 1; i++)
                            {
                                IRow dataRow = sheet.GetRow(i + xlsxformat.startRow - 1) ?? sheet.CreateRow(i + xlsxformat.startRow - 1);//-1 cause from [0] start first
                                for (int j = 0; j < cloumnQTY + 1; j++)
                                {
                                    ICell cell = dataRow.GetCell(j + _startCloumn) ?? dataRow.CreateCell(j + _startCloumn);
                                    cell.SetCellValue(data[i, j]);
                                    cell.CellStyle = cellStyle;
                                }
                            }

                            sheet.ForceFormulaRecalculation = true;///強制重新計算公式

                            using (FileStream fs = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write))
                            {
                                templateWorkbook.Write(fs);
                                fs.Close();
                                templateWorkbook.Close();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (ex is FileNotFoundException)
                    {
                        MessageBox.Show("請確認NPOI資料夾位於mcconf內，或是缺乏Dll", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private bool isOpen(string filename)
        {
            IWorkbook templateWorkbook;
            bool isOpen = true;
            while (isOpen)
            {
                try
                {
                    using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read))
                    {
                        templateWorkbook = new XSSFWorkbook(fs);
                        isOpen = false;
                        fs.Close();
                        templateWorkbook.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (ex is FileNotFoundException)
                    {
                        MessageBox.Show("請確認NPOI資料夾位於mcconf內，或是缺乏Dll", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return true;
                    }
                }
            }
            return true;
        }

        #endregion Xlms Export

        #region FileWatch

        private void WatchPathBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                RootFolder = Environment.SpecialFolder.DesktopDirectory
            };
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                startWatchHeight(fbd.SelectedPath);
                this.Text = "HeightTest " + "Path:" + fbd.SelectedPath;
            }
        }//choose folder to watch

        private void StopWatchBtn_Click(object sender, EventArgs e)
        {
            if (watcher != null)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
            }
        }//stop the watcher

        private void startWatchHeight(string watchFolder)
        {
            watcher = new FileSystemWatcher
            {
                Path = watchFolder,
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = "*.csv",
                IncludeSubdirectories = false
            };
            watcher.Created += _Watch_Changed;
            watcher.Changed += _Watch_Changed;
            watcher.Error += _Watch_Error;
            watcher.EnableRaisingEvents = true;
        } //start watch heightTest folder

        private void _Watch_Changed(object sender, FileSystemEventArgs e)//if change heighttest
        {
            var dirInfo = new DirectoryInfo(e.FullPath);
            if (checkWatch.Checked)
            {
                MessageBox.Show("被更改內容的檔名為" + e.Name
                    + Environment.NewLine
                    + "檔案所在位址為" + e.FullPath.Replace(e.Name, "")
                    , "數據更新通知", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            LastUpdateTime.Text = "Latest updated:" + dirInfo.LastWriteTime;
            Thread.Sleep(100);
            lastData = File.ReadLines(e.FullPath).Last();
            data = new double[shapeinfo.PointCnt];
            data = results(lastData);
            if (data != null)
            {
                decimal edgeMin, edgeMax, edgeTolerance, allPointTolerance, allPointMax, allPointMin;
                int maxIndex = -1, minIndex = -1;
                try
                {
                    edgeMin = Convert.ToDecimal(getMaxMin(data, "Min"));
                    edgeMax = Convert.ToDecimal(getMaxMin(data, "Max"));
                    maxIndex = getAllMaxMin(data, "Max");
                    minIndex = getAllMaxMin(data, "Min");
                    edgeTolerance = (edgeMax - edgeMin) * 1000;//4pt
                    allPointMax = Convert.ToDecimal(data[maxIndex]);
                    allPointMin = Convert.ToDecimal(data[minIndex]);
                    allPointTolerance = (allPointMax - allPointMin) * 1000;//9pt um*1000
                    label2.Text = "Corner Max: " + edgeMax.ToString() + " mm";
                    label3.Text = "Corner Min: " + edgeMin.ToString() + " mm";
                    label4.Text = "Tolerance: " + (edgeTolerance / 1000).ToString() + " mm";

                    int index = AllListView.Items.Count + 1;
                    decimal whichTolerence = 0, whichMax = 0, whichMin = 0;
                    if (null != ModeCB.SelectedItem)
                    {
                        switch (ModeCB.SelectedItem.ToString())
                        {
                            case "Edge Points":
                                whichTolerence = edgeTolerance;
                                whichMax = edgeMax;
                                whichMin = edgeMin;
                                break;

                            case "All Points":
                                whichTolerence = allPointTolerance;
                                whichMax = allPointMax;
                                whichMin = allPointMin;
                                break;
                        }
                    }
                    else
                    {
                        whichTolerence = allPointTolerance;
                        whichMax = allPointMax;
                        whichMin = allPointMin;
                    }

                    if (whichTolerence / 1000 <= Convert.ToDecimal(SpecNumericUpDown.Value))
                    {
                        var item = new ListViewItem(index.ToString());
                        item.SubItems.AddRange(new string[4] { whichMax.ToString(), whichMin.ToString(), whichTolerence.ToString(), "O" });
                        item.ForeColor = oKColor;
                        AllListView.Items.Add(item);
                        OKpictureBox.Visible = true;
                        NGpictureBox.Visible = false;
                    }
                    else
                    {
                        var item = new ListViewItem(index.ToString());
                        item.SubItems.AddRange(new string[4] { whichMax.ToString(), whichMin.ToString(), whichTolerence.ToString(), "X" });
                        item.ForeColor = nGColor;
                        AllListView.Items.Add(item);
                        OKpictureBox.Visible = false;
                        NGpictureBox.Visible = true;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Data Error.");
                }

                if (paintType)
                {
                    foreach (var bt in btns)
                    {
                        int index = int.Parse(bt.Name);
                        bt.Text = setValueToBtn(data[way[index] - 1]);
                        if (bt.Name == (way[maxIndex] - 1).ToString())
                        { bt.BackColor = maxColor; }
                        else if (bt.Name == (way[minIndex] - 1).ToString())
                        { bt.BackColor = minColor; }
                        else
                        { bt.BackColor = initColor; }
                    }
                }
                else
                {
                    var temp = sortAryForColor.ToList();
                    colorSort(out int[] colorInt);
                    foreach (var bt in btns)
                    {
                        int index = int.Parse(bt.Name);
                        bt.Text = setValueToBtn(data[way[index] - 1]);
                        int i = temp.IndexOf(Convert.ToDouble(bt.Text.Replace(" mm", "")));
                        bt.BackColor = Color.FromArgb(colorInt[i], 255, colorInt[i]);
                    }
                }
            }
            else
            {
                MessageBox.Show("測高數據異常，請確認為" + shapeinfo.PointCnt.ToString() + "點測高", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _Watch_Error(object sender, ErrorEventArgs e)
        {
            MessageBox.Show("請重新選擇測高資料夾.", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }//exception

        #endregion FileWatch

        #region Function

        private double[] results(string latestData)//最後行轉九點高度
        {
            double[] results;
            string[] buf = latestData.Split(',');
            if (buf.Length == 5 + shapeinfo.PointCnt)
            {
                results = new double[buf.Length - 5];

                for (int i = 5; i < buf.Length; i++)
                {
                    results[i - 5] = double.Parse(buf[i]);
                }
                return results;
            }
            else
            {
                MessageBox.Show("Out of " + shapeinfo.PointCnt.ToString() + " points", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private string setValueToBtn(double r)
        {
            int _mm = 1;
            string _scale = " mm";
            return (r * _mm).ToString() + _scale;
        }

        private void dataChanged()
        {
            int maxIndex = getAllMaxMin(data, "Max");
            int minIndex = getAllMaxMin(data, "Min");

            if (data != null)
                if (paintType)
                {
                    foreach (var bt in btns)
                    {
                        int index = int.Parse(bt.Name);
                        bt.Text = setValueToBtn(data[way[index] - 1]);
                        if (bt.Name == (way[maxIndex] - 1).ToString())
                        { bt.BackColor = maxColor; }
                        else if (bt.Name == (way[minIndex] - 1).ToString())
                        { bt.BackColor = minColor; }
                        else
                        { bt.BackColor = initColor; }
                    }
                }
                else
                {
                    var temp = sortAryForColor.ToList();
                    colorSort(out int[] colorInt);
                    foreach (var bt in btns)
                    {
                        int index = int.Parse(bt.Name);
                        bt.Text = setValueToBtn(data[way[index] - 1]);
                        int i = temp.IndexOf(Convert.ToDouble(bt.Text.Replace(" mm", "")));
                        bt.BackColor = Color.FromArgb(colorInt[i], 255, colorInt[i]);
                    }
                }
        }

        private int[] ZS(string type)// todo
        {
            int[] results = new int[shapeinfo.PointCnt];
            int row, column; int totalCnt = 0;

            row = shapeinfo.ShapeWidth;
            column = shapeinfo.ShapeLength;

            if (type == "Z")
            {
                for (int i = 1; i <= row; i++)
                {
                    for (int j = 0; j < column; j++)
                    {
                        totalCnt += 1;
                        results[totalCnt - 1] = totalCnt;
                    }
                }
                return results;
            }
            if (type == "S")
            {
                for (int i = 1; i <= column; i++)
                {
                    for (int j = 0; j < row; j++)
                    {
                        totalCnt += 1;
                        if (i % 2 != 0)
                        {
                            results[totalCnt - 1] = totalCnt;
                        }
                        else
                        {
                            results[totalCnt - 1] = (i * row) - j;
                        }
                    }
                }
                return results;
            }
            return null;
        }

        private double getMaxMin(double[] datastring, string MaxorMin)
        {
            double answer = 0, temp = 0;
            double[] sortarray = new double[4];

            sortarray[0] = datastring[0];
            sortarray[1] = datastring[shapeinfo.ShapeWidth - 1];
            sortarray[2] = datastring[shapeinfo.PointCnt - shapeinfo.ShapeWidth];
            sortarray[3] = datastring[shapeinfo.PointCnt - 1];// edge 4 points
            Array.Sort(sortarray);

            if (MaxorMin == "Min")
            {
                temp = sortarray[0];
                answer = temp;
            }
            else if (MaxorMin == "Max")
            {
                temp = sortarray[sortarray.Length - 1];
                answer = temp;
            }
            return answer;
        }

        private int getAllMaxMin(double[] datastring, string MaxorMin)
        {
            int answer = 0;
            double temp = 0;
            double[] sortarray = new double[datastring.Length];
            sortAryForColor = new double[datastring.Length];

            datastring.CopyTo(sortarray, 0);
            Array.Sort(sortarray);

            sortAryForColor = sortarray;
            var list = datastring.ToList();

            if (MaxorMin == "Min")
            {
                temp = sortarray[0];
            }
            else if (MaxorMin == "Max")
            {
                temp = sortarray[sortarray.Length - 1];
            }
            answer = list.IndexOf(temp);
            return answer;
        }

        public static int NumberFromExcelColumn(string column)
        {
            int retVal = 0;
            string col = column.ToUpper();
            for (int iChar = col.Length - 1; iChar >= 0; iChar--)
            {
                char colPiece = col[iChar];
                int colNum = colPiece - 64;
                retVal = retVal + colNum * (int)Math.Pow(26, col.Length - (iChar + 1));
            }
            return retVal;
        }

        #endregion Function

        #region Extend

        private void ExtendBtn_Click(object sender, EventArgs e)
        {
            if (!extendForm)
            {
                extendForm = true;
                this.Size = extend;
            }
            else
            {
                extendForm = false;
                this.Size = unExtend;
                extendForm2 = false;
            }
        }

        private void SetBtnStyle(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;//樣式
            btn.ForeColor = Color.Transparent;//前景
            btn.BackColor = Color.Transparent;//去背景
            btn.FlatAppearance.BorderSize = 1;//去邊線
            btn.FlatAppearance.MouseOverBackColor = Color.OrangeRed;//滑鼠經過
            btn.FlatAppearance.MouseDownBackColor = Color.Transparent;//滑鼠按下
        }

        private void btn_MouseHover(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.FlatAppearance.BorderSize = 1;
        }

        private void btn_MouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.FlatAppearance.BorderSize = 0;
        }

        #endregion Extend

        #region ListView

        private void initListView(ListView listView)
        {
            if (listView.Items.Count > 0)
                listView.Items.Clear();
            listView.View = View.Details;
            listView.GridLines = true;
            listView.LabelEdit = false;
            listView.FullRowSelect = true;
        }

        private void SelectBtn_Click(object sender, EventArgs e)
        {
            OKListView.Items.Clear();
            int i = 0;
            foreach (ListViewItem okItem in AllListView.Items)
            {
                if (okItem.ForeColor == oKColor)
                {
                    if (i < 5)
                    {
                        i++;
                    }
                    else
                    { i = 5; }
                }
            }
            if (i != 0)
            {
                ListViewItem[] listViewItems = new ListViewItem[i];
                int loop = 0;
                foreach (ListViewItem okItem in AllListView.Items)
                {
                    if (okItem.ForeColor == oKColor && loop < 5)
                    {
                        listViewItems[loop] = (ListViewItem)okItem.Clone();
                        loop++;
                    }
                }
                OKListView.Items.AddRange(listViewItems);
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (AllListView.SelectedItems.Count != 0)
            {
                foreach (ListViewItem item in this.AllListView.SelectedItems)
                {
                    if (item.Selected)
                    {
                        item.Remove();
                    }
                }
            }
        }

        private void CopyBtn_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, clipboardString());
        }

        private string clipboardString()
        {
            string paste = "";
            if (OKListView.Items.Count == 5)
            {
                for (int i = 0; i < OKListView.Items.Count; i++)
                {
                    string max = OKListView.Items[i].SubItems[1].Text;
                    string min = OKListView.Items[i].SubItems[2].Text;
                    string tolerance = OKListView.Items[i].SubItems[3].Text;
                    paste += max + "\t" + min + "\t" + tolerance + "\r\n";
                }
            }
            else
            {
                MessageBox.Show("Data null.");
            }
            return paste;
        }

        #endregion ListView

        #region ColorRGB

        private void colorSort(out int[] percentage)
        {
            int rangeInColor = 230;
            int colorOffset = 0;
            var max = sortAryForColor[shapeinfo.PointCnt - 1];
            var min = sortAryForColor[0];
            var tolerence = max - min;
            percentage = new int[shapeinfo.PointCnt];
            if (tolerence != 0)
            {
                for (int i = 0; i < shapeinfo.PointCnt; i++)
                {
                    var percent = (sortAryForColor[i] - min) / tolerence;
                    if ((percent * rangeInColor).ToString().Contains('.'))
                    {
                        string[] temp = (percent * rangeInColor).ToString().Split('.');
                        percentage[i] = int.Parse(temp[0]) + colorOffset;
                    }
                    else
                    {
                        percentage[i] = int.Parse((percent * rangeInColor).ToString()) + colorOffset;
                    }
                }
            }
        }

        private void PaintTypeCkb_CheckedChanged(object sender, EventArgs e)
        {
            paintType = !PaintTypeCkb.Checked;
            if (watcher != null)
            {
                if (data != null)
                    dataChanged();
            }
            tips(paintType);
        }

        #endregion ColorRGB

        #region tips

        private void tips(bool paintType)
        {
            foreach (var bt in btns)
            {
                if (!paintType)
                {
                    toolTip.SetToolTip(bt, "顏色越深，單點高度越高。" + Environment.NewLine + "顏色越淺，單點高度越低。");
                }
                else
                {
                    toolTip.SetToolTip(bt, "紅色為平面最低點。" + Environment.NewLine + "綠色為平面最高點。");
                }
            }
        }

        #endregion tips

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeNow.Text = DateTime.Now.ToString();
        }

        private void FlowDirectionCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FlowDirectionCB.SelectedIndex != -1)
                flowLayoutPanel1.FlowDirection = (FlowDirection)FlowDirectionCB.SelectedIndex;
        }

        private void initFlowDirectionCB()
        {
            FlowDirectionCB.DropDownStyle = ComboBoxStyle.DropDownList;
            FlowDirectionCB.Items.AddRange(Enum.GetNames(typeof(FlowDirection)));
        }

        private void ExtendLabel_Click(object sender, EventArgs e)
        {
            if (!extendForm2)
            {
                this.Size = extend + extend2;
                shapeinfo.singlePoint();
                createButtons();
                foreach (var bt in btns)
                {
                    bt.Text = "第 " + way[int.Parse(bt.Name)].ToString() + " 點";
                    bt.BackColor = initColor;
                }
            }
            else
            {
                this.Size -= extend2;
            }
            extendForm2 = !extendForm2;
        }

        private void CatchBtn_Click(object sender, EventArgs e)
        {
            var path = pathOfRecognition();
            try
            {
                var strRead = File.ReadLines(path[0]).ToList();
                var strRead2 = File.ReadLines(path[1]).ToList();
                offsetInfo.HeightIndex = strRead.Count;
                offsetInfo.ImageIndex = strRead2.Count;
                if (strRead.Count > 2)
                {
                    var tempStr = strRead[strRead.Count - 1].Split(',');
                    try
                    {
                        catchOffsetHeight = Convert.ToDecimal(tempStr[5]);
                        OffsetLB.Text = tempStr[5].ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string[] pathOfRecognition()
        {
            string[] path = new string[2];
            string date = DateTime.Now.ToString("yyyyMMd");
            path[0] = @".\HeightRecognition\HeightRecognition_" + date + ".csv";
            path[1] = @".\ImageInspection\ImageInspection_" + date + ".csv";
            return path;
        }

        private void ExportBtn2_Click(object sender, EventArgs e)
        {
            XlsxFormat format = new XlsxFormat();
            format.OffsetFormat();

            double[,] result = new double[5, 3];
            for (int i = 0; i < offsetInfo.offsetNum; i++)
            {
                result[i, 0] = offsetInfo.XOffset[i];
                result[i, 1] = offsetInfo.YOffset[i];
                result[i, 2] = Convert.ToDouble(catchOffsetHeight - Convert.ToDecimal(offsetInfo.ZOffset[i]));
            }
            npoiSetExcel(format, result);
        }

        private void GetOffsetBtn_Click(object sender, EventArgs e)
        {
            var path = pathOfRecognition();
            offsetInfo.Init();
            bool isReadOk = false;
            try
            {
                var heightStr = File.ReadLines(path[0]).ToList();
                var imageStr = File.ReadLines(path[1]).ToList();
                int heightDatasNum = heightStr.Count - offsetInfo.HeightIndex;
                int imageDatasNum = imageStr.Count - offsetInfo.ImageIndex;
                bool isHeightOK = heightDatasNum >= offsetInfo.offsetNum;
                bool isImageOK = imageDatasNum >= offsetInfo.offsetNum;
                if (isHeightOK && isImageOK)
                {
                    if (heightStr.Count >= (2 + offsetInfo.offsetNum) && imageStr.Count >= (2 + offsetInfo.offsetNum))
                    {
                        for (int i = 0; i < offsetInfo.offsetNum; i++)
                        {
                            var temp = heightStr[heightStr.Count - 1 - i].Split(',');
                            var temp2 = imageStr[imageStr.Count - 1 - i].Split(',');
                            offsetInfo.ZOffset[i] = Convert.ToDecimal(temp[5]);
                            offsetInfo.XOffset[i] = Convert.ToDouble(temp2[7]);
                            offsetInfo.YOffset[i] = Convert.ToDouble(temp2[8]);
                        }
                        isReadOk = true;
                    }
                    else
                    {
                        MessageBox.Show("Data is not enough.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    if (!isHeightOK)
                    {
                        MessageBox.Show("HeightRecognition Data is not enough." + Environment.NewLine + "Only " + heightDatasNum.ToString() + " records were caught.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    if (!isImageOK)
                    {
                        MessageBox.Show("ImageInspection Data is not enough." + Environment.NewLine + "Only " + imageDatasNum.ToString() + " records were caught.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (isReadOk)
            {
                OffsetListView.Items.Clear();
                for (int i = 0; i < offsetInfo.offsetNum; i++)
                {
                    var item = new ListViewItem((i+1).ToString());
                    item.SubItems.AddRange(new string[4] { offsetInfo.XOffset[i].ToString(), offsetInfo.YOffset[i].ToString(), offsetInfo.ZOffset[i].ToString(), (catchOffsetHeight - Convert.ToDecimal(offsetInfo.ZOffset[i])).ToString() });
                    item.ForeColor = oKColor;
                    OffsetListView.Items.Add(item);
                }            
            }
        }
    }
}