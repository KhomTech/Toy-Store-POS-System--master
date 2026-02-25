namespace DBP_Project
{
    partial class formPurchase
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formPurchase));
            this.flowLayoutPanelCart = new System.Windows.Forms.FlowLayoutPanel();
            this.comboPayment = new System.Windows.Forms.ComboBox();
            this.labelAddress = new System.Windows.Forms.Label();
            this.labelMemberName = new System.Windows.Forms.Label();
            this.labelEmail = new System.Windows.Forms.Label();
            this.labelTotalPrice = new System.Windows.Forms.Label();
            this.buttoPlaceOrder = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanelCart
            // 
            this.flowLayoutPanelCart.AutoScroll = true;
            this.flowLayoutPanelCart.Location = new System.Drawing.Point(28, 17);
            this.flowLayoutPanelCart.Name = "flowLayoutPanelCart";
            this.flowLayoutPanelCart.Size = new System.Drawing.Size(553, 529);
            this.flowLayoutPanelCart.TabIndex = 6;
            // 
            // comboPayment
            // 
            this.comboPayment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboPayment.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboPayment.FormattingEnabled = true;
            this.comboPayment.Items.AddRange(new object[] {
            "เก็บเงินปลายทาง",
            "คิวอาร์พร้อมเพย์",
            "โอนผ่านบัญชีธนาคาร",
            "บัตรเครดิต"});
            this.comboPayment.Location = new System.Drawing.Point(623, 340);
            this.comboPayment.Name = "comboPayment";
            this.comboPayment.Size = new System.Drawing.Size(265, 36);
            this.comboPayment.TabIndex = 37;
            // 
            // labelAddress
            // 
            this.labelAddress.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelAddress.Font = new System.Drawing.Font("Comic Sans MS", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAddress.Location = new System.Drawing.Point(626, 17);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(308, 127);
            this.labelAddress.TabIndex = 38;
            this.labelAddress.Text = "Addresss";
            // 
            // labelMemberName
            // 
            this.labelMemberName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelMemberName.Font = new System.Drawing.Font("Comic Sans MS", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMemberName.Location = new System.Drawing.Point(626, 170);
            this.labelMemberName.Name = "labelMemberName";
            this.labelMemberName.Size = new System.Drawing.Size(308, 45);
            this.labelMemberName.TabIndex = 39;
            this.labelMemberName.Text = "Name";
            // 
            // labelEmail
            // 
            this.labelEmail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelEmail.Font = new System.Drawing.Font("Comic Sans MS", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEmail.Location = new System.Drawing.Point(626, 242);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(308, 41);
            this.labelEmail.TabIndex = 40;
            this.labelEmail.Text = "Email";
            // 
            // labelTotalPrice
            // 
            this.labelTotalPrice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelTotalPrice.Font = new System.Drawing.Font("Comic Sans MS", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTotalPrice.Location = new System.Drawing.Point(623, 424);
            this.labelTotalPrice.Name = "labelTotalPrice";
            this.labelTotalPrice.Size = new System.Drawing.Size(457, 41);
            this.labelTotalPrice.TabIndex = 42;
            // 
            // buttoPlaceOrder
            // 
            this.buttoPlaceOrder.BackColor = System.Drawing.Color.DarkGreen;
            this.buttoPlaceOrder.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttoPlaceOrder.ForeColor = System.Drawing.Color.Snow;
            this.buttoPlaceOrder.Location = new System.Drawing.Point(1027, 708);
            this.buttoPlaceOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttoPlaceOrder.Name = "buttoPlaceOrder";
            this.buttoPlaceOrder.Size = new System.Drawing.Size(143, 40);
            this.buttoPlaceOrder.TabIndex = 43;
            this.buttoPlaceOrder.Text = "Place order";
            this.buttoPlaceOrder.UseVisualStyleBackColor = false;
            this.buttoPlaceOrder.Click += new System.EventHandler(this.buttonPlaceOrder_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.BackColor = System.Drawing.Color.Transparent;
            this.buttonBack.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBack.ForeColor = System.Drawing.Color.Black;
            this.buttonBack.Image = global::DBP_Project.Properties.Resources.bback;
            this.buttonBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonBack.Location = new System.Drawing.Point(12, 708);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(130, 40);
            this.buttonBack.TabIndex = 44;
            this.buttonBack.Text = "BACK";
            this.buttonBack.UseVisualStyleBackColor = false;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1182, 121);
            this.panel1.TabIndex = 45;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Berlin Sans FB", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(407, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(435, 99);
            this.label1.TabIndex = 46;
            this.label1.Text = "Check Out";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.flowLayoutPanelCart);
            this.panel2.Controls.Add(this.comboPayment);
            this.panel2.Controls.Add(this.labelEmail);
            this.panel2.Controls.Add(this.labelTotalPrice);
            this.panel2.Controls.Add(this.labelAddress);
            this.panel2.Controls.Add(this.labelMemberName);
            this.panel2.Location = new System.Drawing.Point(12, 127);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1158, 554);
            this.panel2.TabIndex = 46;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(621, 308);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 29);
            this.label2.TabIndex = 47;
            this.label2.Text = "Payment Methods";
            // 
            // formPurchase
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::DBP_Project.Properties.Resources.bbg3;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttoPlaceOrder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formPurchase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Toy Store";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formPurchase_FormClosing);
            this.Load += new System.EventHandler(this.formPurchase_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCart;
        private System.Windows.Forms.ComboBox comboPayment;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.Label labelMemberName;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.Label labelTotalPrice;
        private System.Windows.Forms.Button buttoPlaceOrder;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
    }
}