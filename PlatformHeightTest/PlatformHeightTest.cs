using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Threading;


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
        #endregion

        private void PlatformHeightTest_Load(object sender, EventArgs e)
        {
            Button.CheckForIllegalCrossThreadCalls = false;
            CenterToScreen();
            comboBox1.SelectedIndex = 0;

            try
            {
                startWatchHeight(v2kPathFolder);
                FilePath.Text = v2kPathFolder;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + "請手動添加測高資料夾位址", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
                FilePath.Text = fbd.SelectedPath;
            }
        }//choose folder to watch

        private void startWatchHeight(string watchFolder)
        {
            watcher = new FileSystemWatcher
            {
                Path = watchFolder,
                NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = "*.csv",
                IncludeSubdirectories = false
            };
            watcher.Changed += _Watch_Changed;
            watcher.Error += _Watch_Error;
            watcher.EnableRaisingEvents = true;
        } //start watch heightTest folder

        private void StopWatchBtn_Click(object sender, EventArgs e)
        {
            if (watcher != null)
            {
                watcher.EnableRaisingEvents = false;
                watcher.Dispose();
            }
        }//stop the watcher 

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
            LastUpdateTime.Text = "最後更新:" + dirInfo.LastWriteTime;
            Thread.Sleep(1000);
            lastData = File.ReadLines(e.FullPath).Last();

            data = results(lastData);
            if (data != null)
            {
                decimal min, max, tolerance;
                try
                {
                    min = Convert.ToDecimal(getMaxMin(data, "Min"));
                    max = Convert.ToDecimal(getMaxMin(data, "Max"));
                    tolerance = max - min;
                    label2.Text = "Max: " + max.ToString() + " mm";
                    label3.Text = "Min: " + min.ToString() + " mm";
                    label4.Text = "Tolerance: " + tolerance.ToString() + " mm";
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
                        }
                    }
                }
            }
        }

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

        private void dataChanged()
        {
            if (data != null)
                foreach (Control bt in this.Controls)
                {
                    if (bt is Button)
                    {
                        if (bt.Name != "WatchPathBtn" && bt.Name != "StopWatchBtn")
                        {
                            int index = int.Parse(bt.Name);
                            bt.Text = setValueToBtn(data[way[index] - 1]);
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

        #endregion
    }
}
