namespace SQLClient
{
    partial class LoginForm
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
            this.components = new System.ComponentModel.Container();
            this.vpFace = new AForge.Controls.VideoSourcePlayer();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gpLogin = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tipMsg = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.gpLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // vpFace
            // 
            this.vpFace.BackgroundImage = global::SQLClient.Properties.Resources.camara;
            this.vpFace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.vpFace.BorderColor = System.Drawing.Color.Transparent;
            this.vpFace.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vpFace.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.vpFace.Location = new System.Drawing.Point(3, 17);
            this.vpFace.Name = "vpFace";
            this.vpFace.Size = new System.Drawing.Size(325, 225);
            this.vpFace.TabIndex = 0;
            this.vpFace.VideoSource = null;
            this.vpFace.Paint += new System.Windows.Forms.PaintEventHandler(this.vpFace_Paint);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(59, 17);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(263, 21);
            this.txtUser.TabIndex = 3;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(59, 44);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(263, 21);
            this.txtPassword.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.vpFace);
            this.groupBox1.Location = new System.Drawing.Point(5, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(331, 245);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "登录请看向摄像头";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "用户名:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "密码:";
            // 
            // gpLogin
            // 
            this.gpLogin.Controls.Add(this.btnCancel);
            this.gpLogin.Controls.Add(this.txtUser);
            this.gpLogin.Controls.Add(this.label1);
            this.gpLogin.Controls.Add(this.btnLogin);
            this.gpLogin.Controls.Add(this.txtPassword);
            this.gpLogin.Controls.Add(this.label2);
            this.gpLogin.Location = new System.Drawing.Point(7, 248);
            this.gpLogin.Name = "gpLogin";
            this.gpLogin.Size = new System.Drawing.Size(328, 109);
            this.gpLogin.TabIndex = 10;
            this.gpLogin.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(154, 74);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(247, 74);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 8;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tipMsg
            // 
            this.tipMsg.AutoPopDelay = 5000;
            this.tipMsg.ForeColor = System.Drawing.Color.Red;
            this.tipMsg.InitialDelay = 100;
            this.tipMsg.IsBalloon = true;
            this.tipMsg.ReshowDelay = 100;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(341, 362);
            this.Controls.Add(this.gpLogin);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "登录";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.gpLogin.ResumeLayout(false);
            this.gpLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AForge.Controls.VideoSourcePlayer vpFace;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox gpLogin;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.ToolTip tipMsg;
    }
}