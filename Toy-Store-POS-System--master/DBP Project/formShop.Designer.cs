namespace DBP_Project
{
    partial class formShop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formShop));
            this.flowLayoutPanelProducts = new System.Windows.Forms.FlowLayoutPanel();
            this.comboCategory = new System.Windows.Forms.ComboBox();
            this.textFind = new System.Windows.Forms.TextBox();
            this.sidebar = new System.Windows.Forms.Panel();
            this.buttonManageProduct = new System.Windows.Forms.Button();
            this.buttonLogOut = new System.Windows.Forms.Button();
            this.buttonManageCus = new System.Windows.Forms.Button();
            this.buttonManageEmp = new System.Windows.Forms.Button();
            this.buttonOrder = new System.Windows.Forms.Button();
            this.buttonReportProfix = new System.Windows.Forms.Button();
            this.buttonReportSell = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnMenubar = new System.Windows.Forms.PictureBox();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonViewCart = new System.Windows.Forms.Button();
            this.buttonFind = new System.Windows.Forms.Button();
            this.sidebar.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMenubar)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanelProducts
            // 
            this.flowLayoutPanelProducts.AutoScroll = true;
            this.flowLayoutPanelProducts.BackColor = System.Drawing.Color.White;
            this.flowLayoutPanelProducts.Location = new System.Drawing.Point(278, 166);
            this.flowLayoutPanelProducts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanelProducts.Name = "flowLayoutPanelProducts";
            this.flowLayoutPanelProducts.Size = new System.Drawing.Size(875, 505);
            this.flowLayoutPanelProducts.TabIndex = 4;
            // 
            // comboCategory
            // 
            this.comboCategory.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboCategory.FormattingEnabled = true;
            this.comboCategory.Location = new System.Drawing.Point(915, 124);
            this.comboCategory.Name = "comboCategory";
            this.comboCategory.Size = new System.Drawing.Size(121, 36);
            this.comboCategory.TabIndex = 51;
            // 
            // textFind
            // 
            this.textFind.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textFind.Location = new System.Drawing.Point(278, 123);
            this.textFind.Name = "textFind";
            this.textFind.Size = new System.Drawing.Size(395, 35);
            this.textFind.TabIndex = 82;
            // 
            // sidebar
            // 
            this.sidebar.BackColor = System.Drawing.Color.DodgerBlue;
            this.sidebar.Controls.Add(this.buttonManageProduct);
            this.sidebar.Controls.Add(this.buttonLogOut);
            this.sidebar.Controls.Add(this.buttonManageCus);
            this.sidebar.Controls.Add(this.buttonManageEmp);
            this.sidebar.Controls.Add(this.buttonOrder);
            this.sidebar.Controls.Add(this.buttonReportProfix);
            this.sidebar.Controls.Add(this.buttonReportSell);
            this.sidebar.Location = new System.Drawing.Point(-6, 116);
            this.sidebar.Name = "sidebar";
            this.sidebar.Size = new System.Drawing.Size(267, 680);
            this.sidebar.TabIndex = 50;
            // 
            // buttonManageProduct
            // 
            this.buttonManageProduct.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonManageProduct.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonManageProduct.ForeColor = System.Drawing.Color.Snow;
            this.buttonManageProduct.Image = global::DBP_Project.Properties.Resources.mmanage;
            this.buttonManageProduct.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonManageProduct.Location = new System.Drawing.Point(6, 0);
            this.buttonManageProduct.Name = "buttonManageProduct";
            this.buttonManageProduct.Size = new System.Drawing.Size(258, 71);
            this.buttonManageProduct.TabIndex = 44;
            this.buttonManageProduct.Text = "Manage product";
            this.buttonManageProduct.UseVisualStyleBackColor = false;
            this.buttonManageProduct.Click += new System.EventHandler(this.buttonManageProduct_Click);
            // 
            // buttonLogOut
            // 
            this.buttonLogOut.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonLogOut.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLogOut.ForeColor = System.Drawing.Color.Snow;
            this.buttonLogOut.Image = global::DBP_Project.Properties.Resources.llogout;
            this.buttonLogOut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLogOut.Location = new System.Drawing.Point(5, 568);
            this.buttonLogOut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonLogOut.Name = "buttonLogOut";
            this.buttonLogOut.Size = new System.Drawing.Size(261, 71);
            this.buttonLogOut.TabIndex = 8;
            this.buttonLogOut.Text = "LogOut";
            this.buttonLogOut.UseVisualStyleBackColor = false;
            this.buttonLogOut.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // buttonManageCus
            // 
            this.buttonManageCus.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonManageCus.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonManageCus.ForeColor = System.Drawing.Color.Snow;
            this.buttonManageCus.Image = global::DBP_Project.Properties.Resources.mmanage;
            this.buttonManageCus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonManageCus.Location = new System.Drawing.Point(5, 73);
            this.buttonManageCus.Name = "buttonManageCus";
            this.buttonManageCus.Size = new System.Drawing.Size(261, 71);
            this.buttonManageCus.TabIndex = 45;
            this.buttonManageCus.Text = "Manage customer";
            this.buttonManageCus.UseVisualStyleBackColor = false;
            this.buttonManageCus.Click += new System.EventHandler(this.buttonManageCus_Click);
            // 
            // buttonManageEmp
            // 
            this.buttonManageEmp.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonManageEmp.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonManageEmp.ForeColor = System.Drawing.Color.Snow;
            this.buttonManageEmp.Image = global::DBP_Project.Properties.Resources.mmanage;
            this.buttonManageEmp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonManageEmp.Location = new System.Drawing.Point(5, 146);
            this.buttonManageEmp.Name = "buttonManageEmp";
            this.buttonManageEmp.Size = new System.Drawing.Size(261, 71);
            this.buttonManageEmp.TabIndex = 47;
            this.buttonManageEmp.Text = "Manage employee";
            this.buttonManageEmp.UseVisualStyleBackColor = false;
            this.buttonManageEmp.Click += new System.EventHandler(this.buttonManageEmp_Click);
            // 
            // buttonOrder
            // 
            this.buttonOrder.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonOrder.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOrder.ForeColor = System.Drawing.Color.Snow;
            this.buttonOrder.Image = global::DBP_Project.Properties.Resources.oorder;
            this.buttonOrder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonOrder.Location = new System.Drawing.Point(5, 365);
            this.buttonOrder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonOrder.Name = "buttonOrder";
            this.buttonOrder.Size = new System.Drawing.Size(261, 71);
            this.buttonOrder.TabIndex = 10;
            this.buttonOrder.Text = "Order";
            this.buttonOrder.UseVisualStyleBackColor = false;
            this.buttonOrder.Click += new System.EventHandler(this.buttonOrder_Click);
            // 
            // buttonReportProfix
            // 
            this.buttonReportProfix.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonReportProfix.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReportProfix.ForeColor = System.Drawing.Color.Snow;
            this.buttonReportProfix.Image = global::DBP_Project.Properties.Resources.rreport;
            this.buttonReportProfix.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonReportProfix.Location = new System.Drawing.Point(5, 292);
            this.buttonReportProfix.Name = "buttonReportProfix";
            this.buttonReportProfix.Size = new System.Drawing.Size(261, 71);
            this.buttonReportProfix.TabIndex = 49;
            this.buttonReportProfix.Text = "Report profit";
            this.buttonReportProfix.UseVisualStyleBackColor = false;
            this.buttonReportProfix.Click += new System.EventHandler(this.buttonReportProfix_Click);
            // 
            // buttonReportSell
            // 
            this.buttonReportSell.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonReportSell.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonReportSell.ForeColor = System.Drawing.Color.Snow;
            this.buttonReportSell.Image = global::DBP_Project.Properties.Resources.rreport;
            this.buttonReportSell.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonReportSell.Location = new System.Drawing.Point(5, 219);
            this.buttonReportSell.Name = "buttonReportSell";
            this.buttonReportSell.Size = new System.Drawing.Size(261, 71);
            this.buttonReportSell.TabIndex = 48;
            this.buttonReportSell.Text = "Report sell";
            this.buttonReportSell.UseVisualStyleBackColor = false;
            this.buttonReportSell.Click += new System.EventHandler(this.buttonReportSell_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.btnMenubar);
            this.panel2.Location = new System.Drawing.Point(0, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1208, 117);
            this.panel2.TabIndex = 83;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DBP_Project.Properties.Resources.Gemini_Generated_Image_vair05vair05vair;
            this.pictureBox1.Location = new System.Drawing.Point(135, -156);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(530, 429);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 52;
            this.pictureBox1.TabStop = false;
            // 
            // btnMenubar
            // 
            this.btnMenubar.Image = global::DBP_Project.Properties.Resources.menubar;
            this.btnMenubar.Location = new System.Drawing.Point(29, 11);
            this.btnMenubar.Name = "btnMenubar";
            this.btnMenubar.Size = new System.Drawing.Size(106, 82);
            this.btnMenubar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnMenubar.TabIndex = 51;
            this.btnMenubar.TabStop = false;
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonPrevious.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPrevious.ForeColor = System.Drawing.Color.Snow;
            this.buttonPrevious.Location = new System.Drawing.Point(278, 684);
            this.buttonPrevious.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(130, 40);
            this.buttonPrevious.TabIndex = 7;
            this.buttonPrevious.Text = "<<";
            this.buttonPrevious.UseVisualStyleBackColor = false;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.BackColor = System.Drawing.Color.DodgerBlue;
            this.buttonNext.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNext.ForeColor = System.Drawing.Color.Snow;
            this.buttonNext.Location = new System.Drawing.Point(1023, 684);
            this.buttonNext.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(130, 40);
            this.buttonNext.TabIndex = 6;
            this.buttonNext.Text = ">>";
            this.buttonNext.UseVisualStyleBackColor = false;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(789, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 29);
            this.label2.TabIndex = 84;
            this.label2.Text = "CATEGORIES";
            // 
            // buttonViewCart
            // 
            this.buttonViewCart.BackColor = System.Drawing.Color.Lime;
            this.buttonViewCart.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonViewCart.ForeColor = System.Drawing.Color.Snow;
            this.buttonViewCart.Image = global::DBP_Project.Properties.Resources.cart;
            this.buttonViewCart.Location = new System.Drawing.Point(1074, 120);
            this.buttonViewCart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonViewCart.Name = "buttonViewCart";
            this.buttonViewCart.Size = new System.Drawing.Size(79, 42);
            this.buttonViewCart.TabIndex = 5;
            this.buttonViewCart.UseVisualStyleBackColor = false;
            this.buttonViewCart.Click += new System.EventHandler(this.buttonViewCart_Click);
            // 
            // buttonFind
            // 
            this.buttonFind.BackColor = System.Drawing.Color.Salmon;
            this.buttonFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonFind.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonFind.ForeColor = System.Drawing.Color.Snow;
            this.buttonFind.Image = global::DBP_Project.Properties.Resources.search;
            this.buttonFind.Location = new System.Drawing.Point(671, 122);
            this.buttonFind.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(79, 35);
            this.buttonFind.TabIndex = 9;
            this.buttonFind.UseVisualStyleBackColor = false;
            this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
            // 
            // formShop
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BackgroundImage = global::DBP_Project.Properties.Resources.bbg3;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonViewCart);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.sidebar);
            this.Controls.Add(this.comboCategory);
            this.Controls.Add(this.buttonFind);
            this.Controls.Add(this.textFind);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.flowLayoutPanelProducts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formShop";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Toy Store";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formShop_FormClosing);
            this.Load += new System.EventHandler(this.formShop_Load);
            this.sidebar.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMenubar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelProducts;
        private System.Windows.Forms.Button buttonFind;
        private System.Windows.Forms.ComboBox comboCategory;
        private System.Windows.Forms.TextBox textFind;
        private System.Windows.Forms.Panel sidebar;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonManageProduct;
        private System.Windows.Forms.Button buttonLogOut;
        private System.Windows.Forms.Button buttonManageCus;
        private System.Windows.Forms.Button buttonViewCart;
        private System.Windows.Forms.Button buttonManageEmp;
        private System.Windows.Forms.Button buttonOrder;
        private System.Windows.Forms.Button buttonReportProfix;
        private System.Windows.Forms.Button buttonReportSell;
        private System.Windows.Forms.PictureBox btnMenubar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}