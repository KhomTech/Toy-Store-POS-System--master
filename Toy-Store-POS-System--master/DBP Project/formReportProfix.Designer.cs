namespace DBP_Project
{
    partial class formReportProfit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formReportProfit));
            this.buttonBackLogin = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewProfit = new System.Windows.Forms.DataGridView();
            this.buttonFind = new System.Windows.Forms.Button();
            this.comboCategory = new System.Windows.Forms.ComboBox();
            this.comboYear = new System.Windows.Forms.ComboBox();
            this.comboMonth = new System.Windows.Forms.ComboBox();
            this.comboDay = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProfit)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonBackLogin
            // 
            this.buttonBackLogin.BackColor = System.Drawing.Color.White;
            this.buttonBackLogin.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBackLogin.ForeColor = System.Drawing.Color.Black;
            this.buttonBackLogin.Image = global::DBP_Project.Properties.Resources.bback;
            this.buttonBackLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonBackLogin.Location = new System.Drawing.Point(12, 708);
            this.buttonBackLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonBackLogin.Name = "buttonBackLogin";
            this.buttonBackLogin.Size = new System.Drawing.Size(130, 40);
            this.buttonBackLogin.TabIndex = 82;
            this.buttonBackLogin.Text = "BACK";
            this.buttonBackLogin.UseVisualStyleBackColor = false;
            this.buttonBackLogin.Click += new System.EventHandler(this.buttonBackLogin_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Berlin Sans FB", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Snow;
            this.label1.Location = new System.Drawing.Point(512, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(265, 99);
            this.label1.TabIndex = 76;
            this.label1.Text = "Profit";
            // 
            // dataGridViewProfit
            // 
            this.dataGridViewProfit.AllowUserToAddRows = false;
            this.dataGridViewProfit.AllowUserToDeleteRows = false;
            this.dataGridViewProfit.AllowUserToResizeColumns = false;
            this.dataGridViewProfit.AllowUserToResizeRows = false;
            this.dataGridViewProfit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewProfit.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewProfit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProfit.Location = new System.Drawing.Point(123, 188);
            this.dataGridViewProfit.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridViewProfit.Name = "dataGridViewProfit";
            this.dataGridViewProfit.ReadOnly = true;
            this.dataGridViewProfit.RowHeadersVisible = false;
            this.dataGridViewProfit.RowHeadersWidth = 51;
            this.dataGridViewProfit.Size = new System.Drawing.Size(966, 506);
            this.dataGridViewProfit.TabIndex = 83;
            // 
            // buttonFind
            // 
            this.buttonFind.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonFind.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFind.ForeColor = System.Drawing.Color.Snow;
            this.buttonFind.Image = global::DBP_Project.Properties.Resources.search;
            this.buttonFind.Location = new System.Drawing.Point(877, 134);
            this.buttonFind.Margin = new System.Windows.Forms.Padding(4);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(130, 40);
            this.buttonFind.TabIndex = 93;
            this.buttonFind.UseVisualStyleBackColor = false;
            this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // comboCategory
            // 
            this.comboCategory.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboCategory.FormattingEnabled = true;
            this.comboCategory.Location = new System.Drawing.Point(624, 134);
            this.comboCategory.Margin = new System.Windows.Forms.Padding(4);
            this.comboCategory.Name = "comboCategory";
            this.comboCategory.Size = new System.Drawing.Size(160, 36);
            this.comboCategory.TabIndex = 92;
            // 
            // comboYear
            // 
            this.comboYear.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboYear.FormattingEnabled = true;
            this.comboYear.Location = new System.Drawing.Point(475, 134);
            this.comboYear.Margin = new System.Windows.Forms.Padding(4);
            this.comboYear.Name = "comboYear";
            this.comboYear.Size = new System.Drawing.Size(119, 36);
            this.comboYear.TabIndex = 91;
            // 
            // comboMonth
            // 
            this.comboMonth.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboMonth.FormattingEnabled = true;
            this.comboMonth.Location = new System.Drawing.Point(307, 134);
            this.comboMonth.Margin = new System.Windows.Forms.Padding(4);
            this.comboMonth.Name = "comboMonth";
            this.comboMonth.Size = new System.Drawing.Size(160, 36);
            this.comboMonth.TabIndex = 90;
            // 
            // comboDay
            // 
            this.comboDay.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboDay.FormattingEnabled = true;
            this.comboDay.Location = new System.Drawing.Point(213, 134);
            this.comboDay.Margin = new System.Windows.Forms.Padding(4);
            this.comboDay.Name = "comboDay";
            this.comboDay.Size = new System.Drawing.Size(85, 36);
            this.comboDay.TabIndex = 89;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1182, 115);
            this.panel1.TabIndex = 94;
            // 
            // formReportProfit
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::DBP_Project.Properties.Resources.bbg3;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonFind);
            this.Controls.Add(this.comboCategory);
            this.Controls.Add(this.comboYear);
            this.Controls.Add(this.comboMonth);
            this.Controls.Add(this.comboDay);
            this.Controls.Add(this.dataGridViewProfit);
            this.Controls.Add(this.buttonBackLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formReportProfit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Toy Store";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formReportProfit_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProfit)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonBackLogin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewProfit;
        private System.Windows.Forms.Button buttonFind;
        private System.Windows.Forms.ComboBox comboCategory;
        private System.Windows.Forms.ComboBox comboYear;
        private System.Windows.Forms.ComboBox comboMonth;
        private System.Windows.Forms.ComboBox comboDay;
        private System.Windows.Forms.Panel panel1;
    }
}