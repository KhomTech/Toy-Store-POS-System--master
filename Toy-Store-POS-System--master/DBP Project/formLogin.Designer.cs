namespace DBP_Project
{
    partial class formLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formLogin));
            this.textPassword = new System.Windows.Forms.TextBox();
            this.textUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonBackLogin = new System.Windows.Forms.Button();
            this.buttonLoginDone = new System.Windows.Forms.Button();
            this.checkShowPassword = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textPassword
            // 
            this.textPassword.Font = new System.Drawing.Font("Comic Sans MS", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textPassword.Location = new System.Drawing.Point(578, 403);
            this.textPassword.Name = "textPassword";
            this.textPassword.PasswordChar = '*';
            this.textPassword.Size = new System.Drawing.Size(224, 53);
            this.textPassword.TabIndex = 7;
            // 
            // textUsername
            // 
            this.textUsername.Font = new System.Drawing.Font("Comic Sans MS", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textUsername.Location = new System.Drawing.Point(578, 330);
            this.textUsername.Name = "textUsername";
            this.textUsername.Size = new System.Drawing.Size(224, 53);
            this.textUsername.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(367, 406);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 45);
            this.label2.TabIndex = 5;
            this.label2.Text = "Password :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(367, 335);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 45);
            this.label1.TabIndex = 4;
            this.label1.Text = "Username :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Berlin Sans FB", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(473, 218);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(341, 99);
            this.label3.TabIndex = 8;
            this.label3.Text = "LOG IN ";
            // 
            // buttonBackLogin
            // 
            this.buttonBackLogin.BackColor = System.Drawing.Color.Transparent;
            this.buttonBackLogin.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBackLogin.ForeColor = System.Drawing.Color.Black;
            this.buttonBackLogin.Image = global::DBP_Project.Properties.Resources.bback;
            this.buttonBackLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonBackLogin.Location = new System.Drawing.Point(12, 698);
            this.buttonBackLogin.Name = "buttonBackLogin";
            this.buttonBackLogin.Size = new System.Drawing.Size(135, 45);
            this.buttonBackLogin.TabIndex = 35;
            this.buttonBackLogin.Text = "BACK";
            this.buttonBackLogin.UseVisualStyleBackColor = false;
            this.buttonBackLogin.Click += new System.EventHandler(this.buttonBackLogin_Click);
            // 
            // buttonLoginDone
            // 
            this.buttonLoginDone.BackColor = System.Drawing.Color.Salmon;
            this.buttonLoginDone.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLoginDone.ForeColor = System.Drawing.Color.Snow;
            this.buttonLoginDone.Image = global::DBP_Project.Properties.Resources.login;
            this.buttonLoginDone.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLoginDone.Location = new System.Drawing.Point(578, 510);
            this.buttonLoginDone.Name = "buttonLoginDone";
            this.buttonLoginDone.Size = new System.Drawing.Size(141, 50);
            this.buttonLoginDone.TabIndex = 1;
            this.buttonLoginDone.Text = "LOG IN";
            this.buttonLoginDone.UseVisualStyleBackColor = false;
            this.buttonLoginDone.Click += new System.EventHandler(this.buttonLoginDone_Click);
            // 
            // checkShowPassword
            // 
            this.checkShowPassword.AutoSize = true;
            this.checkShowPassword.BackColor = System.Drawing.Color.Transparent;
            this.checkShowPassword.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkShowPassword.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkShowPassword.Location = new System.Drawing.Point(578, 471);
            this.checkShowPassword.Name = "checkShowPassword";
            this.checkShowPassword.Size = new System.Drawing.Size(184, 33);
            this.checkShowPassword.TabIndex = 37;
            this.checkShowPassword.Text = "Show Password";
            this.checkShowPassword.UseVisualStyleBackColor = false;
            this.checkShowPassword.CheckedChanged += new System.EventHandler(this.checkShowPassword_CheckedChanged);
            // 
            // formLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::DBP_Project.Properties.Resources.bbg1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1182, 753);
            this.Controls.Add(this.buttonLoginDone);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonBackLogin);
            this.Controls.Add(this.checkShowPassword);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.textUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Toy Store";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formLogin_FormClosing);
            this.Load += new System.EventHandler(this.formLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textPassword;
        private System.Windows.Forms.TextBox textUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonBackLogin;
        private System.Windows.Forms.Button buttonLoginDone;
        private System.Windows.Forms.CheckBox checkShowPassword;
    }
}