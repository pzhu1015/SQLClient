using FaceDetection;
using Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLClient
{
    public partial class RegistForm : Form
    {
        public RegistForm()
        {
            InitializeComponent();
        }

        private AIFace aiFace;
        private void RegistForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.aiFace == null)
                {
                    this.aiFace = new AIFace(this.vpFace);
                    this.aiFace.Faces = this.LoadFaces();
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

        public static Bitmap KiCut(Bitmap b, int StartX, int StartY, int iWidth, int iHeight)
        {
            if (b == null)
            {
                return null;
            }

            int w = b.Width;
            int h = b.Height;

            if (StartX >= w || StartY >= h)
            {
                return null;
            }

            if (StartX + iWidth > w)
            {
                iWidth = w - StartX;
            }

            if (StartY + iHeight > h)
            {
                iHeight = h - StartY;
            }

            try
            {
                Bitmap bmpOut = new Bitmap(iWidth, iHeight, PixelFormat.Format24bppRgb);

                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();

                return bmpOut;
            }
            catch
            {
                return null;
            }
        }


        private void AiFace_CaputurePicture(object sender, CapturePictureEventArgs e)
        {
            //this.pbFace.Image = RegistForm.KiCut(e.Bitmap, e.Rectangle.X, e.Rectangle.Y, e.Rectangle.Width, e.Rectangle.Height);
            Bitmap bitmap = new Bitmap(e.Rectangle.Width + 80, e.Rectangle.Height + 160);
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawImage(e.Bitmap, 0, 0, new Rectangle(e.Rectangle.Left - 40, e.Rectangle.Top - 80, e.Rectangle.Width + 80, e.Rectangle.Height + 160), GraphicsUnit.Pixel);
            this.pbFace.Image = bitmap;
        }

        private List<FaceItem> LoadFaces()
        {
            List<FaceItem> items = new List<FaceItem>();
            return items;
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
                e.Graphics.DrawRectangle(Pens.White, fs.Rectangle);
                e.Graphics.DrawString(fs.ID, this.Font, Brushes.White, fs.Rectangle.Left, fs.Rectangle.Top - 20);
            }
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            this.aiFace.CapturePicture();
        }
    }
}
