namespace PlatformHeightTest
{
    partial class PlatformHeightTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlatformHeightTest));
            this.FilePath = new System.Windows.Forms.Label();
            this.WatchPathBtn = new System.Windows.Forms.Button();
            this.LastUpdateTime = new System.Windows.Forms.Label();
            this.StopWatchBtn = new System.Windows.Forms.Button();
            this.checkWatch = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.OKpictureBox = new System.Windows.Forms.PictureBox();
            this.NGpictureBox = new System.Windows.Forms.PictureBox();
            this.SpecNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.OKpictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NGpictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpecNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // FilePath
            // 
            this.FilePath.AutoSize = true;
            this.FilePath.Font = new System.Drawing.Font("微軟正黑體", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.FilePath.ForeColor = System.Drawing.Color.Red;
            this.FilePath.Location = new System.Drawing.Point(8, 46);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(46, 22);
            this.FilePath.TabIndex = 1;
            this.FilePath.Text = "path";
            // 
            // WatchPathBtn
            // 
            this.WatchPathBtn.Location = new System.Drawing.Point(9, 10);
            this.WatchPathBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.WatchPathBtn.Name = "WatchPathBtn";
            this.WatchPathBtn.Size = new System.Drawing.Size(141, 32);
            this.WatchPathBtn.TabIndex = 2;
            this.WatchPathBtn.Text = "Watch";
            this.WatchPathBtn.UseVisualStyleBackColor = true;
            this.WatchPathBtn.Click += new System.EventHandler(this.WatchPathBtn_Click);
            // 
            // LastUpdateTime
            // 
            this.LastUpdateTime.AutoSize = true;
            this.LastUpdateTime.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.LastUpdateTime.Location = new System.Drawing.Point(8, 109);
            this.LastUpdateTime.Name = "LastUpdateTime";
            this.LastUpdateTime.Size = new System.Drawing.Size(93, 19);
            this.LastUpdateTime.TabIndex = 5;
            this.LastUpdateTime.Text = "update time";
            // 
            // StopWatchBtn
            // 
            this.StopWatchBtn.Location = new System.Drawing.Point(197, 11);
            this.StopWatchBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.StopWatchBtn.Name = "StopWatchBtn";
            this.StopWatchBtn.Size = new System.Drawing.Size(141, 32);
            this.StopWatchBtn.TabIndex = 6;
            this.StopWatchBtn.Text = "Stop Watch";
            this.StopWatchBtn.UseVisualStyleBackColor = true;
            this.StopWatchBtn.Click += new System.EventHandler(this.StopWatchBtn_Click);
            // 
            // checkWatch
            // 
            this.checkWatch.AutoSize = true;
            this.checkWatch.Font = new System.Drawing.Font("微軟正黑體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.checkWatch.Location = new System.Drawing.Point(367, 16);
            this.checkWatch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.checkWatch.Name = "checkWatch";
            this.checkWatch.Size = new System.Drawing.Size(132, 23);
            this.checkWatch.TabIndex = 7;
            this.checkWatch.Text = "check Watcher";
            this.checkWatch.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Z",
            "S"});
            this.comboBox1.Location = new System.Drawing.Point(12, 74);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(55, 23);
            this.comboBox1.TabIndex = 3;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(362, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 25);
            this.label2.TabIndex = 10;
            this.label2.Text = "Max:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(362, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 25);
            this.label3.TabIndex = 11;
            this.label3.Text = "Min:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(362, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 25);
            this.label4.TabIndex = 12;
            this.label4.Text = "Tolerance:";
            this.label4.TextChanged += new System.EventHandler(this.label4_TextChanged);
            // 
            // OKpictureBox
            // 
            this.OKpictureBox.Image = ((System.Drawing.Image)(resources.GetObject("OKpictureBox.Image")));
            this.OKpictureBox.Location = new System.Drawing.Point(367, 213);
            this.OKpictureBox.Name = "OKpictureBox";
            this.OKpictureBox.Size = new System.Drawing.Size(200, 200);
            this.OKpictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.OKpictureBox.TabIndex = 13;
            this.OKpictureBox.TabStop = false;
            // 
            // NGpictureBox
            // 
            this.NGpictureBox.Image = ((System.Drawing.Image)(resources.GetObject("NGpictureBox.Image")));
            this.NGpictureBox.Location = new System.Drawing.Point(367, 213);
            this.NGpictureBox.Name = "NGpictureBox";
            this.NGpictureBox.Size = new System.Drawing.Size(200, 200);
            this.NGpictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.NGpictureBox.TabIndex = 14;
            this.NGpictureBox.TabStop = false;
            // 
            // SpecNumericUpDown
            // 
            this.SpecNumericUpDown.DecimalPlaces = 4;
            this.SpecNumericUpDown.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.SpecNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.SpecNumericUpDown.Location = new System.Drawing.Point(433, 55);
            this.SpecNumericUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SpecNumericUpDown.Name = "SpecNumericUpDown";
            this.SpecNumericUpDown.Size = new System.Drawing.Size(72, 25);
            this.SpecNumericUpDown.TabIndex = 15;
            this.SpecNumericUpDown.Value = new decimal(new int[] {
            50,
            0,
            0,
            196608});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(362, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 25);
            this.label1.TabIndex = 16;
            this.label1.Text = "Spec:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(512, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 25);
            this.label5.TabIndex = 17;
            this.label5.Text = "mm";
            // 
            // PlatformHeightTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(611, 480);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SpecNumericUpDown);
            this.Controls.Add(this.NGpictureBox);
            this.Controls.Add(this.OKpictureBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checkWatch);
            this.Controls.Add(this.StopWatchBtn);
            this.Controls.Add(this.LastUpdateTime);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.WatchPathBtn);
            this.Controls.Add(this.FilePath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PlatformHeightTest";
            this.Text = "HeightTest";
            this.Load += new System.EventHandler(this.PlatformHeightTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.OKpictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NGpictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SpecNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label FilePath;
        private System.Windows.Forms.Button WatchPathBtn;
        private System.Windows.Forms.Label LastUpdateTime;
        private System.Windows.Forms.Button StopWatchBtn;
        private System.Windows.Forms.CheckBox checkWatch;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox OKpictureBox;
        private System.Windows.Forms.PictureBox NGpictureBox;
        private System.Windows.Forms.NumericUpDown SpecNumericUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
    }
}