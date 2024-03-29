﻿using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PlatformHeightTest
{
    public partial class PlatformHeightTest : Form
    {
        public PlatformHeightTest()
        {
            InitializeComponent();
            createButtons(this.flowLayoutPanel1);
            shapeinfo.Init9PointSquare();
        }

        #region Announce

        private FileSystemWatcher watcher;
        private const string v2kPathFolder = @".\HeightRecognition";
        private Button[] btns;
        private double[] data;
        private int[] way;//Z+S
        private string lastData = string.Empty;
        private Color maxColor = Color.Red; private Color minColor = Color.LimeGreen;
        private Color nGColor = Color.Red; private Color oKColor = Color.LimeGreen; private Color initColor = Color.White;
        private bool extendForm = false; private bool extendForm2 = false;
        private Size unExtend = new Size(480, 425); private Size extend = new Size(840, 425); private Size extend2 = new Size(0, 200);
        private decimal catchOffsetHeight;
        private bool paintType; private double[] sortAryForColor;
        private Thread threadForForm;
        private static AnalysisForm analysisform = null;
        #region Class

        private class ExtendFormUI
        {
            private static object thisLock = new object();
            public Form form;
            public int IntervalOfScroll = 10;
            public Size ExtendSize1 = new Size(480, 425);
            public Size ExtendSize2 = new Size(840, 425);

            public delegate void Thread_Helper(Form form, int size);

            public void ExtendThreadHeight(Form form, int height)
            {
                if (form.InvokeRequired)
                {
                    Thread_Helper helper = new Thread_Helper(ExtendThreadHeight);
                    form.Invoke(helper, form, height);
                }
                else
                {
                    var sizeHeight = new Size(0, height);
                    form.Size = ExtendSize2 + sizeHeight;
                }
            }

            public void ExtendThreadWidth(Form form, int width)
            {
                if (form.InvokeRequired)
                {
                    Thread_Helper helper = new Thread_Helper(ExtendThreadWidth);
                    form.Invoke(helper, form, width);
                }
                else
                {
                    var sizeWidth = new Size(width, 0);
                    form.Size = ExtendSize1 + sizeWidth;
                }
            }

            public void ExtendHeightSlowly()
            {
                lock (thisLock)
                {
                    for (int i = 0; i <= 20; i++)
                    {
                        ExtendThreadHeight(form, i * 10);
                        Thread.Sleep(IntervalOfScroll);
                    }
                }
            }

            public void UnExtendHeightSlowly()
            {
                lock (thisLock)
                {
                    for (int i = 20; i >= 0; i--)
                    {
                        ExtendThreadHeight(form, i * 10);
                        Thread.Sleep(IntervalOfScroll);
                    }
                }
            }

            public void ExtendWidthSlowly()
            {
                lock (thisLock)
                {
                    for (int i = 0; i <= 36; i++)
                    {
                        ExtendThreadWidth(form, i * 10);
                        Thread.Sleep(IntervalOfScroll);
                    }
                }
            }

            public void UnExtendWidthSlowly()
            {
                lock (thisLock)
                {
                    for (int i = 36; i >= 0; i--)
                    {
                        ExtendThreadWidth(form, i * 10);
                        Thread.Sleep(IntervalOfScroll);
                    }
                }
            }
        }

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
            public string ImagePath { get; set; }
            public string HeightPath { get; set; }
            public int offsetNum = 5;//data quantities
            public int ImageIndex { get; set; }
            public int HeightIndex { get; set; }
            public double[] XOffset { get; set; }
            public double[] YOffset { get; set; }
            public decimal[] ZOffset { get; set; }
            public bool IsDataExist { get; set; }
            public decimal SpecOfAllOffset { get; set; }

            public void Init()
            {
                XOffset = new double[offsetNum];
                YOffset = new double[offsetNum];
                ZOffset = new decimal[offsetNum];
            }

            public void Reverse()
            {
                Array.Reverse(XOffset);
                Array.Reverse(YOffset);
                Array.Reverse(ZOffset);
            }

            public void GetPath()
            {
                string date = DateTime.Now.ToString("yyyyMMdd");
                HeightPath = @".\HeightRecognition\HeightRecognition_" + date + ".csv";
                ImagePath = @".\ImageInspection\ImageInspection_" + date + ".csv";
            }
        }

        public class XlsxFormat
        {
            [CategoryAttribute("Xlsx工作表名稱"), DefaultValueAttribute(true)]
            public string SheetOfCTQ { get; set; }

            [CategoryAttribute("Xlsx起始行列(左上)"), DefaultValueAttribute(true)]
            public string StartColumn { get; set; }

            [CategoryAttribute("Xlsx起始行列(左上)"), DefaultValueAttribute(true)]
            public int StartRow { get; set; }

            [CategoryAttribute("Xlsx結束行列(右下)"), DefaultValueAttribute(true)]
            public string EndColumn { get; set; }

            [CategoryAttribute("Xlsx結束行列(右下)"), DefaultValueAttribute(true)]
            public int EndRow { get; set; }

            [CategoryAttribute("Xlsx數值位數格式"), DefaultValueAttribute(true)]
            public string DotFormat { get; set; }

            public void HeightTestFormat()
            {
                SheetOfCTQ = "工作表1";
                StartColumn = "B";
                StartRow = 56;
                EndColumn = "D";
                EndRow = 60;
                DotFormat = "0.000";
            }

            public void OffsetFormat()
            {
                SheetOfCTQ = "工作表1";
                StartColumn = "B";
                StartRow = 69;
                EndColumn = "D";
                EndRow = 73;
                DotFormat = "0.0000";
            }

            public void FAEHeightTestFormat()
            {
                SheetOfCTQ = "Machine Base + Conveyor";
                StartColumn = "M";
                StartRow = 13;
                EndColumn = "O";
                EndRow = 17;
                DotFormat = "0.000";
            }

            public void FAEOffsetFormat()
            {
                SheetOfCTQ = "Machine Base + Conveyor";
                StartColumn = "D";
                StartRow = 24;
                EndColumn = "F";
                EndRow = 28;
                DotFormat = "0.0000";
            }
        }

        public class Subtract2Point
        {
            public bool IsStartSubtract { get; set; }
            public bool IsFirstPointClick { get; set; }
            public bool IsSecondPointClick { get; set; }
            public string FirstPoint { get; set; }
            public string SecondPoint { get; set; }

            public void InitSub()
            {
                IsStartSubtract = false; IsFirstPointClick = false; IsSecondPointClick = false;
                FirstPoint = string.Empty; SecondPoint = string.Empty;
            }

            public bool GetSubtractValue(Button btn)
            {
                if (IsStartSubtract)
                {
                    if (!IsFirstPointClick)
                    {
                        IsFirstPointClick = true;
                        FirstPoint = btn.Text;
                        btn.FlatAppearance.BorderColor = Color.Red;
                        return false;
                    }
                    else
                    {
                        IsSecondPointClick = true;
                        SecondPoint = btn.Text;
                        btn.FlatAppearance.BorderColor = Color.Red;
                        IsStartSubtract = false;
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show(btn.Text);
                    return false;
                }
            }

            public void StartSubtract()
            {
                try
                {
                    var a = Convert.ToDecimal(FirstPoint.Replace(" mm", ""));
                    var b = Convert.ToDecimal(SecondPoint.Replace(" mm", ""));

                    MessageBox.Show("Result: " + (a - b).ToString() + " mm", "Subtract", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InitSub();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion Class

        private ShapeInfo shapeinfo = new ShapeInfo();
        private OffsetInfo offsetInfo = new OffsetInfo();
        private XlsxFormat heightTestXlsx = new XlsxFormat();
        private XlsxFormat offsetXlsx = new XlsxFormat();
        private XlsxFormat fAEheightTestXlsx = new XlsxFormat();
        private XlsxFormat fAeoffsetXlsx = new XlsxFormat();
        private ExtendFormUI extendFormUI = new ExtendFormUI();
        private Subtract2Point subtract2Point = new Subtract2Point();

        #endregion Announce

        #region MainUI events

        private void PlatformHeightTest_Load(object sender, EventArgs e)
        {
            Button.CheckForIllegalCrossThreadCalls = false;
            offsetInfo.GetPath();
            heightTestXlsx.HeightTestFormat();
            fAEheightTestXlsx.FAEHeightTestFormat();
            offsetXlsx.OffsetFormat();
            fAeoffsetXlsx.FAEOffsetFormat();

            #region UI setting

            this.Size = unExtend;
            CenterToScreen();
            createButtons(this.flowLayoutPanel1);
            SetBtnStyle(ExtendBtn);
            initFlowDirectionCB();
            initListView(AllListView);
            initListView(OffsetListView);
            initTooltip(toolTip);

            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            PathTypeCB.SelectedIndex = 0;

            ExtendBtn.MouseHover += btn_MouseHover;
            ExtendBtn.MouseLeave += btn_MouseLeave;
            ExportBtn.MouseMove += ExportBtn_MouseMove;
            ExportBtn2.MouseMove += ExportBtn_MouseMove;

            OKpictureBox.Visible = false;
            NGpictureBox.Visible = false;
            Timer.Enabled = true;
            tips(paintType);

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
        }

        private void ExportBtn_MouseMove(object sender, MouseEventArgs e)
        {
            var bt = (Button)sender;
            Setting.Tag = bt.Tag;
            ExportBtn3.Tag = bt.Tag;
            Setting2.Tag = bt.Tag;
        }

        private void Setting_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            string type = item.Tag.ToString();
            PropertyGridForm propertyGridForm = new PropertyGridForm();

            switch (type)
            {
                case "Height":
                    propertyGridForm.SetPropertyGridForm(heightTestXlsx);
                    break;

                case "Offset":
                    propertyGridForm.SetPropertyGridForm(offsetXlsx);
                    break;
            }
            propertyGridForm.ShowDialog();
        }

        private void Setting2_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            string type = item.Tag.ToString();
            PropertyGridForm propertyGridForm = new PropertyGridForm();
            switch (type)
            {
                case "Height":
                    propertyGridForm.SetPropertyGridForm(fAEheightTestXlsx);
                    break;

                case "Offset":
                    propertyGridForm.SetPropertyGridForm(fAeoffsetXlsx);
                    break;
            }
            propertyGridForm.ShowDialog();
        }

        private void eachPointBtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (subtract2Point.GetSubtractValue(button))
            {
                subtract2Point.IsStartSubtract = false;
                subtract2Point.StartSubtract();
                foreach (var item in btns)
                {
                    item.FlatAppearance.BorderColor = Color.LightGray;
                }
            }
            SubtractBtnColor();
        }

        private void SubtractBtn_Click(object sender, EventArgs e)
        {
            subtract2Point.InitSub();
            subtract2Point.IsStartSubtract = true;
            SubtractBtnColor();
        }

        private void SubtractBtnColor()
        {
            if (subtract2Point.IsStartSubtract)
            { SubtractBtn.BackColor = oKColor; }
            else
            { SubtractBtn.BackColor = Color.FromArgb(0, 255, 255, 255); }
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
            createButtons(this.flowLayoutPanel1);
            tips(paintType);
            foreach (var bt in btns)
            {
                bt.Text = "第 " + way[int.Parse(bt.Name)].ToString() + " 點";
                bt.BackColor = initColor;
            }
        }

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

        private void Extend2Btn_Click(object sender, EventArgs e)
        {
            extendFormUI.form = this;
            if (!extendForm2)
            {
                threadForForm = new Thread(extendFormUI.ExtendHeightSlowly);
                threadForForm.Start();

                #region init

                shapeinfo.singlePoint();
                createButtons(this.flowLayoutPanel1);
                foreach (var bt in btns)
                {
                    bt.Text = "第 " + way[int.Parse(bt.Name)].ToString() + " 點";
                    bt.BackColor = initColor;
                }
                foreach (Control con in offsetPanel.Controls)
                {
                    con.Enabled = false;
                }
                CatchBtn.Enabled = true;
                offsetInfo.SpecOfAllOffset = OffsetSpecNum.Value;

                #endregion init

                Extend2Btn.Text = "▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲▲";
            }
            else
            {
                threadForForm = new Thread(extendFormUI.UnExtendHeightSlowly);
                threadForForm.Start();
                // this.Size -= extend2;
                Extend2Btn.Text = "▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼";
            }
            extendForm2 = !extendForm2;
        }

        #endregion MainUI events

        #region Xlms Export

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            if (OKListView.Items.Count == 5)
            {
                double[,] result = new double[5, 3];
                for (int i = 0; i < OKListView.Items.Count; i++)
                {
                    result[i, 0] = double.Parse(OKListView.Items[i].SubItems[1].Text);
                    result[i, 1] = double.Parse(OKListView.Items[i].SubItems[2].Text);
                    result[i, 2] = double.Parse(OKListView.Items[i].SubItems[3].Text);
                }
                npoiSetExcel(heightTestXlsx, result);
                //setExcel(format,result);
            }
            else
            {
                MessageBox.Show("At least five records are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportBtn3_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem)sender;
            string type = item.Tag.ToString();
            switch (type)
            {
                case "Height":
                    {
                        if (OKListView.Items.Count == 5)
                        {
                            double[,] result = new double[5, 3];
                            for (int i = 0; i < OKListView.Items.Count; i++)
                            {
                                result[i, 0] = double.Parse(OKListView.Items[i].SubItems[1].Text);
                                result[i, 1] = double.Parse(OKListView.Items[i].SubItems[2].Text);
                                result[i, 2] = double.Parse(OKListView.Items[i].SubItems[3].Text);
                            }
                            npoiSetExcel(fAEheightTestXlsx, result);
                        }
                        else
                        {
                            MessageBox.Show("At least five records are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    break;

                case "Offset":
                    {
                        if (offsetInfo.IsDataExist)
                        {
                            double[,] result = new double[offsetInfo.offsetNum, 3];
                            for (int i = 0; i < offsetInfo.offsetNum; i++)
                            {
                                result[i, 0] = offsetInfo.XOffset[i];
                                result[i, 1] = offsetInfo.YOffset[i];
                                result[i, 2] = Convert.ToDouble(catchOffsetHeight - Convert.ToDecimal(offsetInfo.ZOffset[i]));
                            }
                            npoiSetExcel(fAeoffsetXlsx, result);
                        }
                    }
                    break;
            }
        }

        private void setExcel(XlsxFormat xlsxFormat, double[,] result)
        {
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
                string leftTop = xlsxFormat.StartColumn + xlsxFormat.StartRow.ToString();
                string rightBottm = xlsxFormat.EndColumn + xlsxFormat.EndRow.ToString();
                Microsoft.Office.Interop.Excel.Workbook Wbook = App.Workbooks.Open(dialog.FileName);
                System.IO.FileInfo xlsAttribute = new FileInfo(dialog.FileName);
                xlsAttribute.Attributes = FileAttributes.Normal;
                Microsoft.Office.Interop.Excel.Worksheet Wsheet = (Microsoft.Office.Interop.Excel.Worksheet)Wbook.Sheets[xlsxFormat.SheetOfCTQ];
                Microsoft.Office.Interop.Excel.Range aRangeChange = Wsheet.get_Range(leftTop, rightBottm);
                aRangeChange.Value2 = result;
                aRangeChange.NumberFormat = "0.000";

                Wbook.Save();
                Wbook.Close();
                App.Quit();
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

                            cellStyle.DataFormat = format.GetFormat(xlsxformat.DotFormat);
                            font.FontName = "Calibri";
                            font.FontHeightInPoints = 12;
                            cellStyle.SetFont(font);
                            cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                            cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                            cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                            cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

                            #endregion setting style

                            string sheetName = xlsxformat.SheetOfCTQ;
                            ISheet sheet = templateWorkbook.GetSheet(sheetName) ?? templateWorkbook.CreateSheet(sheetName);

                            int _startCloumn = NumberFromExcelColumn(xlsxformat.StartColumn) - 1;//from [0] start first
                            int _endColumn = NumberFromExcelColumn(xlsxformat.EndColumn) - 1;
                            int cloumnQTY = _endColumn - _startCloumn;
                            int rowQTY = xlsxformat.EndRow - xlsxformat.StartRow;
                            for (int i = 0; i < rowQTY + 1; i++)
                            {
                                IRow dataRow = sheet.GetRow(i + xlsxformat.StartRow - 1) ?? sheet.CreateRow(i + xlsxformat.StartRow - 1);//-1 cause from [0] start first
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
                            MessageBox.Show("Done!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        #region FileWatch_watchChanged

        private void WatchPathBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                //RootFolder = Environment.SpecialFolder.DesktopDirectory
            };
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                if (null != watcher)
                { watcher.Dispose(); }
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

                    if (whichTolerence / 1000 <= Convert.ToDecimal(SpecNumericUpDown.Value * 2))
                    {
                        var item = new ListViewItem(index.ToString());
                        item.SubItems.AddRange(new string[4] { whichMax.ToString(), whichMin.ToString(), whichTolerence.ToString(), "O" });
                        item.ForeColor = oKColor;
                        AllListView.Items.Add(item);
                    }
                    else
                    {
                        var item = new ListViewItem(index.ToString());
                        item.SubItems.AddRange(new string[4] { whichMax.ToString(), whichMin.ToString(), whichTolerence.ToString(), "X" });
                        item.ForeColor = nGColor;
                        AllListView.Items.Add(item);
                    }
                    if (edgeTolerance / 1000 <= Convert.ToDecimal(SpecNumericUpDown.Value))
                    {
                        OKpictureBox.Visible = true;
                        NGpictureBox.Visible = false;
                    }
                    else
                    {
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

        #endregion FileWatch_watchChanged

        #region Function Button[],DataChanged,ZS,MaxMin,BtnSize

        private void createButtons(FlowLayoutPanel flowLayoutPanel)
        {
            flowLayoutPanel.Controls.Clear();
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
                btns[i].FlatStyle = FlatStyle.Flat;
                btns[i].FlatAppearance.BorderColor = Color.LightGray;
                btns[i].Click += eachPointBtn_Click;
                if ((i + 1) % shapeinfo.ShapeWidth == 0)
                {
                    flowLayoutPanel.SetFlowBreak(btns[i], true);
                }
                flowLayoutPanel.Controls.Add(btns[i]);
            }
        }

        private double[] results(string latestData)//最後行轉九點高度
        {
            double[] results;
            string[] buf = latestData.Split(',');
            if (buf.Length == 5 + shapeinfo.PointCnt)
            {
                results = new double[buf.Length - 5];

                for (int i = 5; i < buf.Length; i++)
                {
                    if (double.TryParse(buf[i], out double result))
                    {
                        results[i - 5] = result;
                    }
                    else
                    {
                        MessageBox.Show("Data Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
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

        #endregion Function Button[],DataChanged,ZS,MaxMin,BtnSize

        #region Extend

        private static object lock0 = new object();

        private void ExtendBtn_Click(object sender, EventArgs e)
        {
            extendFormUI.form = this;
            lock (lock0)
            {
                if (!extendForm)
                {
                    extendForm = true;
                    threadForForm = new Thread(extendFormUI.ExtendWidthSlowly);
                    threadForForm.Start();
                }
                else
                {
                    extendForm = false;
                    if (!extendForm2)
                    {
                        threadForForm = new Thread(extendFormUI.UnExtendWidthSlowly);
                        threadForForm.Start();
                    }
                    else
                    {
                        extendForm2 = false;
                        threadForForm = new Thread(extendFormUI.UnExtendHeightSlowly);
                        threadForForm.Start();
                        threadForForm = new Thread(extendFormUI.UnExtendWidthSlowly);
                        threadForForm.Start();
                    }
                }
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

        #region AllListView + OKListview

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

        #endregion AllListView + OKListview

        #region Gradient Colors

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

        #endregion Gradient Colors

        #region Tips

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

        private void initTooltip(ToolTip toolTip)
        {
            toolTip.ToolTipIcon = ToolTipIcon.Info;
            toolTip.ForeColor = Color.Blue;
            toolTip.BackColor = Color.Gray;
            toolTip.AutoPopDelay = 4000;
            toolTip.ToolTipTitle = "Tips";
            toolTip.RemoveAll();
        }

        #endregion Tips

        #region Offset checking and export to xlsx

        private void CatchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var strRead = File.ReadLines(offsetInfo.HeightPath).ToList();
                var strRead2 = File.ReadLines(offsetInfo.ImagePath).ToList();
                offsetInfo.HeightIndex = strRead.Count;
                offsetInfo.ImageIndex = strRead2.Count;
                if (strRead.Count > 2)
                {
                    var tempStr = strRead[strRead.Count - 1].Split(',');
                    try
                    {
                        catchOffsetHeight = Convert.ToDecimal(tempStr[5]);
                        OffsetLB.Text = tempStr[5].ToString();

                        foreach (Control con in offsetPanel.Controls)
                        {
                            con.Enabled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("測高資料不足。","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportBtn2_Click(object sender, EventArgs e)
        {
            if (offsetInfo.IsDataExist)
            {
                double[,] result = new double[offsetInfo.offsetNum, 3];
                for (int i = 0; i < offsetInfo.offsetNum; i++)
                {
                    result[i, 0] = offsetInfo.XOffset[i];
                    result[i, 1] = offsetInfo.YOffset[i];
                    result[i, 2] = Convert.ToDouble(catchOffsetHeight - Convert.ToDecimal(offsetInfo.ZOffset[i]));
                }
                npoiSetExcel(offsetXlsx, result);
            }
        }

        private void GetOffsetBtn_Click(object sender, EventArgs e)
        {
            offsetInfo.Init();
            offsetInfo.IsDataExist = false;
            try
            {
                var heightStr = File.ReadLines(offsetInfo.HeightPath).ToList();
                var imageStr = File.ReadLines(offsetInfo.ImagePath).ToList();
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
                        offsetInfo.IsDataExist = true;
                        offsetInfo.Reverse();
                    }
                    else
                    {
                        MessageBox.Show("At least need " + offsetInfo.offsetNum.ToString() + " records.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    if (!isHeightOK)
                    {
                        MessageBox.Show("Insufficient HeightRecognition Records." + Environment.NewLine + "Only " + heightDatasNum.ToString() + " records were caught.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    if (!isImageOK)
                    {
                        MessageBox.Show("Insufficient ImageInspection Records." + Environment.NewLine + "Only " + imageDatasNum.ToString() + " records were caught.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (offsetInfo.IsDataExist)
            {
                OffsetListView.Items.Clear();
                for (int i = 0; i < offsetInfo.offsetNum; i++)
                {
                    var item = new ListViewItem((i + 1).ToString());
                    item.SubItems.AddRange(new string[4] { offsetInfo.XOffset[i].ToString(), offsetInfo.YOffset[i].ToString(), offsetInfo.ZOffset[i].ToString(), (catchOffsetHeight - Convert.ToDecimal(offsetInfo.ZOffset[i])).ToString() });
                    item.UseItemStyleForSubItems = false;
                    OffsetListView.Items.Add(item);
                }

                #region checking NG data

                foreach (ListViewItem item in OffsetListView.Items)
                {
                    for (int i = 0; i < item.SubItems.Count; i++)
                    {
                        if (i != 3)
                        {
                            if (Convert.ToDecimal(item.SubItems[i].Text) * 1000 > offsetInfo.SpecOfAllOffset)
                            {
                                item.SubItems[i].ForeColor = nGColor;
                            }
                            else
                            {
                                item.SubItems[i].ForeColor = oKColor;
                            }
                        }
                    }
                }

                #endregion checking NG data
            }
        }

        private void OffsetSpecNum_ValueChanged(object sender, EventArgs e)
        {
            offsetInfo.SpecOfAllOffset = OffsetSpecNum.Value * 2;
        }

        #endregion Offset checking and export to xlsx

        #region Open csv

        private void ImageInsectionBtn_Click(object sender, EventArgs e)
        {
            openCSVbyNpad(offsetInfo.ImagePath);
        }

        private void HeightRecognitionBtn_Click(object sender, EventArgs e)
        {
            openCSVbyNpad(offsetInfo.HeightPath);
        }

        private void openCSVbyNpad(string path)
        {
            if (File.Exists(path))
                Process.Start("notepad", path);
            else
                MessageBox.Show("File doesn't exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion Open csv

        #region Analysis Form

        public void AnalysisBtn_Click(object sender, EventArgs e)
        {
            if (analysisform == null)
            {
                analysisform = new AnalysisForm();
                analysisform.Show();
                analysisform.Disposed += Analysisform_Disposed; ;
            }
            else
            {
                analysisform.WindowState = FormWindowState.Normal;
                analysisform.BringToFront();
            }
        }

        private void Analysisform_Disposed(object sender, EventArgs e)
        {
            analysisform = null;
        }

        #endregion
    }
}