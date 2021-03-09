using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Button = System.Windows.Forms.Button;
using Point = System.Drawing.Point;

namespace PlatformHeightTest
{
    public partial class PlatformHeightTest : Form
    {
        public PlatformHeightTest()
        {
            InitializeComponent();
            createButtons();
        }

        #region Announce

        private FileSystemWatcher watcher;
        private const string v2kPathFolder = @".\HeightRecognition";
        private double[] data = new double[9];
        private int[] way = new int[9];//Z+S
        private string lastData = "";
        private Color MaxColor = Color.Red;
        private Color MinColor = Color.LimeGreen;
        private Color NGColor = Color.Red;
        private Color OKColor = Color.LimeGreen;
        private Color InitColor = Color.White;
        private bool extendForm = false;
        private Size unExtend = new Size(480, 425);
        private Size extend = new Size(840, 425);

        #region NPOI Excel

        private string sheetOfCTQ = "工作表1";
        private string startColumn = "B";
        private int startRow = 56;
        private string endColumn = "D";
        private int endRow = 60;

        #endregion NPOI Excel

        #endregion Announce

        private void PlatformHeightTest_Load(object sender, EventArgs e)
        {
            Button.CheckForIllegalCrossThreadCalls = false;
            this.Size = unExtend;
            CenterToScreen();
            comboBox1.SelectedIndex = 0;
            OKpictureBox.Visible = false;
            NGpictureBox.Visible = false;
            ExtendBtn.MouseHover += btn_MouseHover;
            ExtendBtn.MouseLeave += btn_MouseLeave;
            SetBtnStyle(ExtendBtn);
            initListView(AllListView);
            try
            {
                startWatchHeight(v2kPathFolder);
                FilePath.Text = "Path:" + v2kPathFolder;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + "請手動添加測高資料夾位址", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void createButtons()
        {
            Button[] bt = new Button[9];
            int size = 80;
            int[] location = new int[2] { 10, 120 };
            int gap = 5;

            for (int i = 0; i < bt.Length; i++)
            {
                bt[i] = new Button();
                bt[i].Name = i.ToString();
                bt[i].Size = new Size(size, size);
                if (i < 3)
                    bt[i].Location = new Point(location[0] + i * (gap + size), location[1]);
                if (i >= 3 && i < 6)
                    bt[i].Location = new Point(location[0] + (i - 3) * (gap + size), location[1] + size + gap);
                if (i >= 6)
                    bt[i].Location = new Point(location[0] + (i - 6) * (gap + size), location[1] + 2 * (size + gap));
                this.Controls.Add(bt[i]);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            way = ZS(comboBox1.Text);

            if (watcher != null)
            {
                dataChanged();
            }
            else
            {
                foreach (Control bt in this.Controls)
                {
                    if (bt is Button)
                    {
                        if (bt.Name != "WatchPathBtn" && bt.Name != "StopWatchBtn")
                        {
                            bt.Text = "第 " + way[int.Parse(bt.Name)].ToString() + " 點";
                            bt.BackColor = InitColor;
                        }
                    }
                }
            }
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            //setExcel();
            npoiSetExcel();
        }

        private void setExcel()
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
                    string leftTop = startColumn + startRow.ToString();
                    string rightBottm = endColumn + endRow.ToString();
                    Microsoft.Office.Interop.Excel.Workbook Wbook = App.Workbooks.Open(dialog.FileName);
                    System.IO.FileInfo xlsAttribute = new FileInfo(dialog.FileName);
                    xlsAttribute.Attributes = FileAttributes.Normal;
                    Microsoft.Office.Interop.Excel.Worksheet Wsheet = (Microsoft.Office.Interop.Excel.Worksheet)Wbook.Sheets[sheetOfCTQ];
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

        private void npoiSetExcel()
        {
            if (OKListView.Items.Count == 5)
            {
                OpenFileDialog dialog = new OpenFileDialog()
                {
                    RestoreDirectory = true,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    Filter = "Excel Work|*.xlsx",
                    Title = "Open 檢核表.xlsx File"
                };
                dialog.ShowDialog();

                double[,] result = new double[5, 3];
                for (int i = 0; i < OKListView.Items.Count; i++)
                {
                    result[i, 0] = double.Parse(OKListView.Items[i].SubItems[1].Text);
                    result[i, 1] = double.Parse(OKListView.Items[i].SubItems[2].Text);
                    result[i, 2] = double.Parse(OKListView.Items[i].SubItems[3].Text);
                }

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

                                string sheetName = sheetOfCTQ;
                                ISheet sheet = templateWorkbook.GetSheet(sheetName) ?? templateWorkbook.CreateSheet(sheetName);

                                int _startCloumn = NumberFromExcelColumn(startColumn) - 1;//from [0] start first
                                int _endColumn = NumberFromExcelColumn(endColumn) - 1;
                                int cloumnQTY = _endColumn - _startCloumn;
                                int rowQTY = endRow - startRow;
                                for (int i = 0; i < rowQTY + 1; i++)
                                {
                                    IRow dataRow = sheet.GetRow(i + startRow - 1) ?? sheet.CreateRow(i + startRow - 1);//-1 cause from [0] start first
                                    for (int j = 0; j < cloumnQTY + 1; j++)
                                    {
                                        ICell cell = dataRow.GetCell(j + _startCloumn) ?? dataRow.CreateCell(j + _startCloumn);
                                        cell.SetCellValue(result[i, j]);
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
            else
            {
                MessageBox.Show("At least five records are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                FilePath.Text = "Path:" + fbd.SelectedPath;
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
            Thread.Sleep(500);
            lastData = File.ReadLines(e.FullPath).Last();

            data = results(lastData);
            if (data != null)
            {
                decimal min, max, tolerance, ninePointTolerance, ninePointMax, ninePointMin;
                int maxIndex = -1, minIndex = -1;
                try
                {
                    min = Convert.ToDecimal(getMaxMin(data, "Min"));
                    max = Convert.ToDecimal(getMaxMin(data, "Max"));
                    maxIndex = getAllMaxMin(data, "Max");
                    minIndex = getAllMaxMin(data, "Min");
                    tolerance = max - min;//4pt
                    ninePointMax = Convert.ToDecimal(data[maxIndex]);
                    ninePointMin = Convert.ToDecimal(data[minIndex]);
                    ninePointTolerance = (ninePointMax - ninePointMin) * 1000;//9pt um*1000
                    label2.Text = "Corner Max: " + max.ToString() + " mm";
                    label3.Text = "Corner Min: " + min.ToString() + " mm";
                    label4.Text = "Tolerance: " + tolerance.ToString() + " mm";

                    int index = AllListView.Items.Count + 1;
                    if (ninePointTolerance <= Convert.ToDecimal(SpecNumericUpDown.Value))
                    {
                        var item = new ListViewItem(index.ToString());
                        item.SubItems.AddRange(new string[4] { ninePointMax.ToString(), ninePointMin.ToString(), ninePointTolerance.ToString(), "O" });
                        item.ForeColor = OKColor;
                        AllListView.Items.Add(item);
                        OKpictureBox.Visible = true;
                        NGpictureBox.Visible = false;
                    }
                    else
                    {
                        var item = new ListViewItem(index.ToString());
                        item.SubItems.AddRange(new string[4] { ninePointMax.ToString(), ninePointMin.ToString(), ninePointTolerance.ToString(), "X" });
                        item.ForeColor = NGColor;
                        AllListView.Items.Add(item);
                        OKpictureBox.Visible = false;
                        NGpictureBox.Visible = true;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Data Error.");
                }

                foreach (Control bt in this.Controls)
                {
                    if (bt is Button)
                    {
                        if (bt.Name != "WatchPathBtn" && bt.Name != "StopWatchBtn")
                        {
                            int index = int.Parse(bt.Name);
                            bt.Text = setValueToBtn(data[way[index] - 1]);
                            if (bt.Name == (way[maxIndex] - 1).ToString())
                            { bt.BackColor = MaxColor; }
                            else if (bt.Name == (way[minIndex] - 1).ToString())
                            { bt.BackColor = MinColor; }
                            else
                            { bt.BackColor = InitColor; }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("測高數據異常，請確認為九點測高", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (buf.Length == 14)
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
                MessageBox.Show("Out of 9 points", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                foreach (Control bt in this.Controls)
                {
                    if (bt is Button)
                    {
                        if (bt.Name != "WatchPathBtn" && bt.Name != "StopWatchBtn")
                        {
                            int index = int.Parse(bt.Name);
                            bt.Text = setValueToBtn(data[way[index] - 1]);
                            if (bt.Name == (way[maxIndex] - 1).ToString())
                            { bt.BackColor = MaxColor; }
                            else if (bt.Name == (way[minIndex] - 1).ToString())
                            { bt.BackColor = MinColor; }
                            else
                            { bt.BackColor = InitColor; }
                        }
                    }
                }
        }

        private int[] ZS(string type)
        {
            if (type == "Z")
            {
                int[] results = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                return results;
            }
            if (type == "S")
            {
                int[] results = new int[9] { 1, 2, 3, 6, 5, 4, 7, 8, 9 };
                return results;
            }
            return null;
        }

        private double getMaxMin(double[] datastring, string MaxorMin)
        {
            double answer = 0, temp = 0;
            double[] sortarray = new double[4];

            sortarray[0] = datastring[0];
            sortarray[1] = datastring[2];
            sortarray[2] = datastring[6];
            sortarray[3] = datastring[8];// edge 4 points
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
            double[] sortarray = new double[9];

            for (int i = 0; i < 9; i++)
            {
                sortarray[i] = datastring[i];
            }
            Array.Sort(sortarray);

            if (MaxorMin == "Min")
            {
                temp = sortarray[0];
                for (int i = 0; i < 9; i++)
                {
                    if (temp == datastring[i])
                        answer = i;
                }
            }
            else if (MaxorMin == "Max")
            {
                temp = sortarray[sortarray.Length - 1];
                for (int i = 0; i < 9; i++)
                {
                    if (temp == datastring[i])
                        answer = i;
                }
            }
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
                if (okItem.ForeColor == OKColor)
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
                    if (okItem.ForeColor == OKColor && loop < 5)
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
    }
}