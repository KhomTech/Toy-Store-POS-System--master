namespace DBP_Project
{
    partial class formTaxinvoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formTaxinvoice));
            this.buttonBackLogin = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lblVatAmount = new System.Windows.Forms.Label();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvInvoiceItems = new System.Windows.Forms.DataGridView();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCustomerInvoiceNo = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCustomerID = new System.Windows.Forms.Label();
            this.lblCustomerAddress = new System.Windows.Forms.Label();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblCompanyTaxID = new System.Windows.Forms.Label();
            this.lblComapanyAddress = new System.Windows.Forms.Label();
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoiceItems)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonBackLogin
            // 
            this.buttonBackLogin.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBackLogin.Location = new System.Drawing.Point(12, 717);
            this.buttonBackLogin.Name = "buttonBackLogin";
            this.buttonBackLogin.Size = new System.Drawing.Size(71, 32);
            this.buttonBackLogin.TabIndex = 101;
            this.buttonBackLogin.Text = "BACK";
            this.buttonBackLogin.UseVisualStyleBackColor = true;
            this.buttonBackLogin.Click += new System.EventHandler(this.buttonBackLogin_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(16, 645);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 25);
            this.label13.TabIndex = 97;
            this.label13.Text = "Sign :";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalAmount.Location = new System.Drawing.Point(691, 685);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(144, 25);
            this.lblTotalAmount.TabIndex = 96;
            this.lblTotalAmount.Text = "label5";
            // 
            // lblVatAmount
            // 
            this.lblVatAmount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblVatAmount.Location = new System.Drawing.Point(691, 644);
            this.lblVatAmount.Name = "lblVatAmount";
            this.lblVatAmount.Size = new System.Drawing.Size(144, 25);
            this.lblVatAmount.TabIndex = 95;
            this.lblVatAmount.Text = "label5";
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSubTotal.Location = new System.Drawing.Point(691, 603);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(144, 25);
            this.lblSubTotal.TabIndex = 94;
            this.lblSubTotal.Text = "label5";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(545, 686);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(140, 25);
            this.label12.TabIndex = 93;
            this.label12.Text = "Total Amount :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(559, 645);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(126, 25);
            this.label11.TabIndex = 92;
            this.label11.Text = "Vat Amount :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(590, 604);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 25);
            this.label7.TabIndex = 91;
            this.label7.Text = "Subtotal :";
            // 
            // dgvInvoiceItems
            // 
            this.dgvInvoiceItems.AllowUserToAddRows = false;
            this.dgvInvoiceItems.AllowUserToDeleteRows = false;
            this.dgvInvoiceItems.AllowUserToResizeColumns = false;
            this.dgvInvoiceItems.AllowUserToResizeRows = false;
            this.dgvInvoiceItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInvoiceItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvInvoiceItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvoiceItems.Location = new System.Drawing.Point(21, 283);
            this.dgvInvoiceItems.Name = "dgvInvoiceItems";
            this.dgvInvoiceItems.ReadOnly = true;
            this.dgvInvoiceItems.RowHeadersVisible = false;
            this.dgvInvoiceItems.RowHeadersWidth = 51;
            this.dgvInvoiceItems.RowTemplate.Height = 24;
            this.dgvInvoiceItems.Size = new System.Drawing.Size(814, 308);
            this.dgvInvoiceItems.TabIndex = 90;
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblInvoiceDate.Location = new System.Drawing.Point(725, 67);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(110, 23);
            this.lblInvoiceDate.TabIndex = 89;
            this.lblInvoiceDate.Text = "label5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(655, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 25);
            this.label6.TabIndex = 88;
            this.label6.Text = "Date :";
            // 
            // lblCustomerInvoiceNo
            // 
            this.lblCustomerInvoiceNo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCustomerInvoiceNo.Location = new System.Drawing.Point(654, 243);
            this.lblCustomerInvoiceNo.Name = "lblCustomerInvoiceNo";
            this.lblCustomerInvoiceNo.Size = new System.Drawing.Size(181, 25);
            this.lblCustomerInvoiceNo.TabIndex = 87;
            this.lblCustomerInvoiceNo.Text = "label5";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(462, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 25);
            this.label5.TabIndex = 86;
            this.label5.Text = "Invoice No. :";
            // 
            // lblCustomerID
            // 
            this.lblCustomerID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCustomerID.Location = new System.Drawing.Point(654, 208);
            this.lblCustomerID.Name = "lblCustomerID";
            this.lblCustomerID.Size = new System.Drawing.Size(181, 25);
            this.lblCustomerID.TabIndex = 85;
            this.lblCustomerID.Text = "label5";
            // 
            // lblCustomerAddress
            // 
            this.lblCustomerAddress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCustomerAddress.Location = new System.Drawing.Point(654, 145);
            this.lblCustomerAddress.Name = "lblCustomerAddress";
            this.lblCustomerAddress.Size = new System.Drawing.Size(181, 63);
            this.lblCustomerAddress.TabIndex = 84;
            this.lblCustomerAddress.Text = "label5";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCustomerName.Location = new System.Drawing.Point(654, 109);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(161, 23);
            this.lblCustomerName.TabIndex = 83;
            this.lblCustomerName.Text = "label5";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(462, 209);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 25);
            this.label8.TabIndex = 82;
            this.label8.Text = "Tax ID :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(462, 145);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(186, 25);
            this.label9.TabIndex = 81;
            this.label9.Text = "Customer Address :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(462, 110);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(165, 25);
            this.label10.TabIndex = 80;
            this.label10.Text = "Customer Name :";
            // 
            // lblCompanyTaxID
            // 
            this.lblCompanyTaxID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCompanyTaxID.Location = new System.Drawing.Point(208, 240);
            this.lblCompanyTaxID.Name = "lblCompanyTaxID";
            this.lblCompanyTaxID.Size = new System.Drawing.Size(181, 25);
            this.lblCompanyTaxID.TabIndex = 79;
            this.lblCompanyTaxID.Text = "label5";
            // 
            // lblComapanyAddress
            // 
            this.lblComapanyAddress.AllowDrop = true;
            this.lblComapanyAddress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblComapanyAddress.Location = new System.Drawing.Point(208, 147);
            this.lblComapanyAddress.Name = "lblComapanyAddress";
            this.lblComapanyAddress.Size = new System.Drawing.Size(181, 72);
            this.lblComapanyAddress.TabIndex = 78;
            this.lblComapanyAddress.Text = "label5";
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCompanyName.Location = new System.Drawing.Point(208, 109);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(161, 23);
            this.lblCompanyName.TabIndex = 77;
            this.lblCompanyName.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 241);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 25);
            this.label4.TabIndex = 76;
            this.label4.Text = "Tax ID :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 25);
            this.label3.TabIndex = 75;
            this.label3.Text = "Company Address :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 25);
            this.label2.TabIndex = 74;
            this.label2.Text = "Company Name :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(250, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(370, 38);
            this.label1.TabIndex = 73;
            this.label1.Text = "ใบกำกับภาษี (Tax invoice)";
            // 
            // buttonPrint
            // 
            this.buttonPrint.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrint.Image = global::DBP_Project.Properties.Resources.print;
            this.buttonPrint.Location = new System.Drawing.Point(738, 717);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(107, 32);
            this.buttonPrint.TabIndex = 99;
            this.buttonPrint.UseVisualStyleBackColor = true;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // formTaxinvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 761);
            this.Controls.Add(this.buttonBackLogin);
            this.Controls.Add(this.buttonPrint);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.lblVatAmount);
            this.Controls.Add(this.lblSubTotal);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dgvInvoiceItems);
            this.Controls.Add(this.lblInvoiceDate);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblCustomerInvoiceNo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblCustomerID);
            this.Controls.Add(this.lblCustomerAddress);
            this.Controls.Add(this.lblCustomerName);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblCompanyTaxID);
            this.Controls.Add(this.lblComapanyAddress);
            this.Controls.Add(this.lblCompanyName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formTaxinvoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tax invoice";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formTaxinvoice_FormClosing);
            this.Load += new System.EventHandler(this.formTaxinvoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoiceItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBackLogin;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblVatAmount;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvInvoiceItems;
        private System.Windows.Forms.Label lblInvoiceDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCustomerInvoiceNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCustomerID;
        private System.Windows.Forms.Label lblCustomerAddress;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblCompanyTaxID;
        private System.Windows.Forms.Label lblComapanyAddress;
        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}