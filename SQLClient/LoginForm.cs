using FaceDetection;
using Helper;
using SQLClient.Properties;
using SQLDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLClient
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private bool isDetected = false;
        private AIFace aiFace;
        private void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.aiFace == null)
                {
                    this.aiFace = new AIFace(this.vpFace);
                    this.aiFace.Faces = this.LoadFaces();
                    ErrorCode rslt = this.aiFace.Initialize();
                    if (rslt != ErrorCode.Ok)
                    {
                        this.aiFace.Stop();
                        return;
                    }
                }
                this.aiFace.Start();
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                if (this.aiFace != null)
                {
                    this.aiFace.Stop();
                    this.aiFace = null;
                }
            }
        }

        private List<FaceItem> LoadFaces()
        {
            List<FaceItem> items = new List<FaceItem>();
            return items;
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.aiFace != null)
            {
                this.aiFace.Stop();
            }
        }

        private void vpFace_Paint(object sender, PaintEventArgs e)
        {
            if (this.aiFace != null)
            {
                if (!this.isDetected)
                {
                    FaceResult fs = this.aiFace.FaceResult;
                    fs.ID = "admin";
                    e.Graphics.DrawRectangle(Pens.White, fs.Rectangle);
                    e.Graphics.DrawString(fs.ID, this.Font, Brushes.White, fs.Rectangle.Left, fs.Rectangle.Top - 20);
                    Image successImage = Resources.detect_success_64;
                    e.Graphics.DrawImage(successImage, new PointF(this.vpFace.Width - successImage.Width, 0));
                }
                else
                {
                    FaceResult fs = this.aiFace.FaceResult;
                    e.Graphics.DrawRectangle(Pens.ForestGreen, fs.Rectangle);
                    e.Graphics.DrawString("正在识别中...", this.Font, Brushes.ForestGreen, fs.Rectangle.Left, fs.Rectangle.Top - 20);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string error;
                bool rslt = ConnectInfo.Login(this.txtUser.Text, this.txtPassword.Text, out error);
                if (!rslt)
                {
                    this.tipMsg.Show(error, this.txtUser);
                }
                else
                {
                    string args = $"-user {this.txtUser.Text} -password {this.txtPassword.Text}";
                    string fileName = Application.ExecutablePath;
                    Process proc = Process.Start(fileName, args);
                    proc.WaitForInputIdle();
                    Application.Exit();
                }

            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
            }
        }
    }
}
