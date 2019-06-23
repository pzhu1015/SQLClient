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
using System.Runtime.InteropServices;
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

        private int moveDelt = 0;
        private bool isDetected = false;
        private AIFace aiFace;
        private void LoginForm_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = null;
                string error = "";
                ConnectInfo.LoadUser(out dt, out error);
                if (this.aiFace == null)
                {
                    this.aiFace = new AIFace(this.vpFace);
                    this.aiFace.Faces = this.LoadFaces(dt);
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

        private List<FaceItem> LoadFaces(DataTable dt)
        {
            List<FaceItem> items = new List<FaceItem>();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["faceResult"] != null)
                {
                    byte[] faceresult_value = dr["faceResult"] as byte[];
                    if (faceresult_value != null)
                    {
                        FaceItem item = new FaceItem();
                        item.OrderId = DateTime.Now.Ticks;
                        item.FaceID = dr[0].ToString();
                        FaceModel faceModel = new FaceModel();
                        faceModel.lFeatureSize = faceresult_value.Length;
                        faceModel.pbFeature = Marshal.AllocHGlobal(faceresult_value.Length);
                        Marshal.Copy(faceresult_value, 0, faceModel.pbFeature, faceresult_value.Length);
                        item.FaceModel = faceModel;
                        items.Add(item);
                    }
                }
            }
            return items;
        }

        private void DrawDetectRectangle(Graphics g, Rectangle r)
        {
            int DL = r.Width / 2 - 50;
            if (DL < 20) DL = 20;
            Pen pen = new Pen(Color.White, 2);
            Point pstart1, pend1, pstart2, pend2;
            //left, top
            pstart1 = new Point(r.X, r.Y);
            pend1 = new Point(r.X + DL, r.Y);
            pstart2 = new Point(r.X, r.Y);
            pend2 = new Point(r.X, r.Y + DL);
            g.DrawLines(pen, new Point[] { pstart1, pend1, pstart2, pend2 });

            //right, top
            pstart1 = new Point(r.X + r.Width, r.Y);
            pend1 = new Point(r.X + r.Width - DL, r.Y);
            pstart2 = new Point(r.X + r.Width, r.Y);
            pend2 = new Point(r.X + r.Width, r.Y + DL);
            g.DrawLines(pen, new Point[] { pstart1, pend1, pstart2, pend2 });

            //left, bottom
            pstart1 = new Point(r.X, r.Y + r.Height);
            pend1 = new Point(r.X, r.Y + r.Height - DL);
            pstart2 = new Point(r.X, r.Y + r.Height);
            pend2 = new Point(r.X + DL, r.Y + r.Height);
            g.DrawLines(pen, new Point[] { pstart1, pend1, pstart2, pend2 });

            //right, bottom
            pstart1 = new Point(r.X + r.Width, r.Y + r.Height);
            pend1 = new Point(r.X + r.Width - DL, r.Y + r.Height);
            pstart2 = new Point(r.X + r.Width, r.Y + r.Height);
            pend2 = new Point(r.X + r.Width, r.Y + +r.Height - DL);
            g.DrawLines(pen, new Point[] { pstart1, pend1, pstart2, pend2 });

            Point detectStart = new Point(r.X, r.Y + this.moveDelt);
            Point detectEnd = new Point(r.X + r.Width, r.Y + this.moveDelt);
            g.DrawLine(new Pen(Color.White, 1), detectStart, detectEnd);
            this.moveDelt += 4;
            if (this.moveDelt >= r.Height)
            {
                this.moveDelt = 0;
            }
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
                FaceResult fs = this.aiFace.FaceResult;
                this.DrawDetectRectangle(e.Graphics, fs.Rectangle);
                if (this.aiFace.IsDetected)
                {
                    e.Graphics.DrawString(fs.ID, this.Font, Brushes.White, fs.Rectangle.Left, fs.Rectangle.Top - 20);
                    Image successImage = Resources.detect_success_64;
                    e.Graphics.DrawImage(successImage, new Point(this.vpFace.Width - successImage.Width, 0));
                }
                else
                {
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
