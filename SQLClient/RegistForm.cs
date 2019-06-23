using FaceDetection;
using Helper;
using SQLDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SQLClient
{
    public partial class RegistForm : Form
    {
        private int moveDelt = 0;
        private AIFace aiFace;
        public RegistForm()
        {
            InitializeComponent();
            this.dgvUser.AutoGenerateColumns = false;
            this.GoHomePage();
        }

        private void StartAIFace()
        {
            try
            {
                if (this.aiFace == null)
                {
                    DataTable dt = this.dgvUser.DataSource as DataTable;
                    this.aiFace = new AIFace(this.vpFace);
                    this.aiFace.Faces = this.LoadFaces(dt);
                    this.aiFace.CaputurePicture += AiFace_CaputurePicture;
                    ErrorCode rslt = this.aiFace.Initialize();
                    if (rslt != ErrorCode.Ok)
                    {
                        this.aiFace.Stop();
                        return;
                    }
                    
                }
                this.aiFace.Start();
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
                if (this.aiFace != null)
                {
                    this.aiFace.Stop();
                    this.aiFace = null;
                }
            }
        }

        private FaceModel GetModel(byte [] faceresult_value)
        {
            FaceModel faceModel = new FaceModel();
            faceModel.lFeatureSize = faceresult_value.Length;
            faceModel.pbFeature = Marshal.AllocHGlobal(faceresult_value.Length);
            Marshal.Copy(faceresult_value, 0, faceModel.pbFeature, faceresult_value.Length);
            return faceModel;
        }

        private List<FaceItem> LoadFaces(DataTable dt)
        {
            List<FaceItem> items = new List<FaceItem>();
            if (dt == null) return items;
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
                        item.FaceModel = this.GetModel(faceresult_value);
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
            g.DrawLines(pen, new Point[] { pstart1, pend1, pstart2, pend2});

            //right, top
            pstart1 = new Point(r.X + r.Width, r.Y);
            pend1 = new Point(r.X + r.Width - DL, r.Y);
            pstart2 = new Point(r.X + r.Width, r.Y);
            pend2 = new Point(r.X + r.Width, r.Y + DL);
            g.DrawLines(pen, new Point[] { pstart1, pend1, pstart2, pend2 });

            //left, bottom
            pstart1 = new Point(r.X, r.Y + r.Height);
            pend1 = new Point(r.X , r.Y + r.Height - DL);
            pstart2 = new Point(r.X, r.Y + r.Height);
            pend2 = new Point(r.X + DL, r.Y + r.Height);
            g.DrawLines(pen, new Point[] { pstart1, pend1, pstart2, pend2 });

            //right, bottom
            pstart1 = new Point(r.X + r.Width, r.Y + r.Height);
            pend1 = new Point(r.X + r.Width - DL, r.Y + r.Height);
            pstart2 = new Point(r.X + r.Width, r.Y + r.Height);
            pend2 = new Point(r.X + r.Width, r.Y + + r.Height - DL);
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

        private void RegistForm_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = null;
                string error = "";
                ConnectInfo.LoadUser(out dt, out error);
                if (dt != null)
                {
                    this.dgvUser.DataSource = dt;
                }
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

        private void AiFace_CaputurePicture(object sender, CapturePictureEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                CaptureForm captureForm = new CaptureForm();
                captureForm.CaptureBitmap = e.Bitmap;
                if (captureForm.ShowDialog() == DialogResult.OK)
                {
                    Bitmap bitmap = ImageHelper.ResizeImage(captureForm.UserHead, this.pbFace.Width, this.pbFace.Height, 0);
                    this.pbFace.Image = bitmap;
                    if (this.aiFace.MatchBitmap(bitmap, this.GetModel(e.FaceResult)))
                    {
                        this.pbFace.Tag = e.FaceResult;
                    }
                    else
                    {
                        MessageBox.Show("错误，截取头像识别率低，请重新拍照截取头像!!!");
                    }
                }
            }));
        }

        private void RegistForm_FormClosing(object sender, FormClosingEventArgs e)
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
                e.Graphics.DrawString(fs.ID, this.Font, Brushes.White, fs.Rectangle.Left, fs.Rectangle.Top - 20);
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            if (this.aiFace != null)
            {
                this.aiFace.CapturePicture();
            }
        }

        private void btnRegist_Click(object sender, EventArgs e)
        {
            string error = "";
            byte[] face = null;
            byte[] faceresult = null;
            if (this.pbFace.Tag != null)
            {
                face = BytesHelper.ImageToByte(this.pbFace.Image);
                faceresult = this.pbFace.Tag as byte[];
            }
            bool rslt = ConnectInfo.Regist(
                this.txtUser.Text, 
                this.txtPassword.Text, 
                this.txtPhone.Text,
                this.txtEmail.Text,
                this.txtAge.Text,
                this.cmbGender.Text,
                face, 
                faceresult, 
                out error);
            if (rslt)
            {
                DataTable dt = null;
                error = "";
                ConnectInfo.LoadUser(out dt, out error);
                if (dt != null)
                {
                    this.dgvUser.DataSource = dt;
                }
                this.aiFace.Faces = this.LoadFaces(dt);
            }
            else
            {
                MessageBox.Show(error);
                return;
            }
            this.pbFace.Image = null;
            this.pbFace.Tag = null;
            this.txtUser.Text = "";
            this.txtPassword.Text = "";
            this.txtPhone.Text = "";
            this.txtEmail.Text = "";
            this.txtAge.Text = "";
            this.cmbGender.Text = "";
            this.GoHomePage();
        }

        private void dgvUser_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void dgvUser_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
                e.ThrowException = false;
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataTable dt = this.dgvUser.DataSource as DataTable;
                if (dt.Rows[e.RowIndex]["face"] != null)
                {
                    object face_value = dt.Rows[e.RowIndex]["face"];
                    if (face_value != null)
                    {
                        byte[] face = face_value as byte[];
                        if (face != null)
                        {
                            this.picHead.Image = BytesHelper.ByteToImage(face);
                        }
                        else
                        {
                            this.picHead.Image = null;
                        }
                    }
                    else
                    {
                        this.picHead.Image = null;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.nFRight.SelectedPageIndex = 1;
            this.nFLeft.SelectedPageIndex = 1;
            this.StartAIFace();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.nFRight.SelectedPageIndex = 2;
            this.nFLeft.SelectedPageIndex = 1;
            this.StartAIFace();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this.GoHomePage();   
        }

        private void btnCancelRegist_Click(object sender, EventArgs e)
        {
            this.GoHomePage();
        }

        private void GoHomePage()
        {
            this.nFLeft.SelectedPageIndex = 0;
            this.nFRight.SelectedPageIndex = 0;
            if (this.aiFace != null)
            {
                this.aiFace.Stop();
                this.aiFace = null;
            }
        }

        private void btnCancelUpdate_Click(object sender, EventArgs e)
        {
            this.GoHomePage();
        }
    }
}
