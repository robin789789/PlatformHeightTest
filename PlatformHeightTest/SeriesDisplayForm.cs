using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlatformHeightTest
{
    public partial class SeriesDisplayForm : Form
    {
        public SeriesDisplayForm()
        {
            InitializeComponent();
            checkedListBox1.MultiColumn = true;
        }
        public void GenerateCheckListBox(Dictionary<string, bool> members)
        {
            foreach (var item in members)
            {
                checkedListBox1.Items.Add(item.Key, item.Value);
            }
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            AnalysisForm.DisplayMember = new Dictionary<string, bool>();

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                AnalysisForm.DisplayMember[checkedListBox1.Items[i].ToString()] = checkedListBox1.GetItemChecked(i);
            }
            this.Close();
        }

        private void AllCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, AllCheckBox.Checked);
            }
        }

        private void OddEvenCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
            if (OddEvenCheckBox.Checked)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i += 2)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 1; i < checkedListBox1.Items.Count; i += 2)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
            }
        }

        private void SeriesDisplayForm_Load(object sender, EventArgs e)
        {
            this.FormClosing += SeriesDisplayForm_FormClosing;
        }

        private void SeriesDisplayForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            AnalysisForm.DisplayMember = new Dictionary<string, bool>();

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                AnalysisForm.DisplayMember[checkedListBox1.Items[i].ToString()] = checkedListBox1.GetItemChecked(i);
            }
        }
    }
}
