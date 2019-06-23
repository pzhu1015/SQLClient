using Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SQLClient
{
    public enum CursorDirection
    {
        eMove,           
        eLeft,           
        eRight,          
        eTop,            
        eBottom,         
        eLeftTop,        
        eRightBottom,    
        eRightTop,       
        eLeftBottom      
    }
    public partial class CaptureForm : Form
    {
        private int AnchorSize = 8;
        private Bitmap captureBitmap;
        private Rectangle cutRectangle = Rectangle.Empty;
        private Bitmap userHead = null;
        private bool startMove = false;
        private Point currentPos = new Point(0, 0);
        private CursorDirection currentCursor;
        public CaptureForm()
        {
            InitializeComponent();
            this.cutRectangle = new Rectangle((this.pnImage.Width - 120) / 2, (this.pnImage.Height - 140) / 2, 120, 140);
            this.userHead = null;
        }


        public Bitmap CaptureBitmap
        {
            get
            {
                return captureBitmap;
            }
            set
            {
                captureBitmap = ImageHelper.ResizeImage(value, this.pnImage.Width, this.pnImage.Height, 0);
                this.pnImage.BackgroundImage = this.captureBitmap;
            }
        }

        public Bitmap UserHead
        {
            get
            {
                return userHead;
            }

            set
            {
                userHead = value;
            }
        }

        private void DrawRectangles(int cX, int cY, int cW, int cH, Point point)
        {
            if (this.OutOfRange(cX, cY, cW, cH)) return;

            this.cutRectangle.X = cX;
            this.cutRectangle.Y = cY;
            this.cutRectangle.Width = cW;
            this.cutRectangle.Height = cH;
            Pen pen = new Pen(Color.Red, 1);
            Graphics gBuffer = Graphics.FromHwnd(this.pnImage.Handle);
            try
            {
                Bitmap bitmap = new Bitmap(this.pnImage.ClientSize.Width, this.pnImage.ClientSize.Height);
                using (Graphics gMemoryBuffer = Graphics.FromImage(bitmap))
                {
                    gMemoryBuffer.Clear(this.pnImage.BackColor);
                    gMemoryBuffer.Transform = new System.Drawing.Drawing2D.Matrix();
                    gMemoryBuffer.DrawImage(this.captureBitmap, 0, 0);
                    if (cY >= 30)
                    {
                        gMemoryBuffer.DrawString($"坐标:[{cX}, {cY}] 大小: [{cW}, {cH}]", this.Font, Brushes.Red, cX, cY - 30);
                    }
                    else
                    {
                        gMemoryBuffer.DrawString($"坐标:[{cX}, {cY}] 大小: [{cW}, {cH}]", this.Font, Brushes.Red, cX, cY + cH + 10);
                    }
                    gMemoryBuffer.DrawRectangle(new Pen(Color.Red, 1), this.cutRectangle);

                    //first line
                    gMemoryBuffer.DrawRectangle(pen, cX - AnchorSize / 2, cY - AnchorSize / 2, AnchorSize, AnchorSize);
                    gMemoryBuffer.DrawRectangle(pen, (cX + cW / 2) - AnchorSize / 2, cY - AnchorSize / 2, AnchorSize, AnchorSize);
                    gMemoryBuffer.DrawRectangle(pen, (cX + cW) - AnchorSize / 2, cY - AnchorSize / 2, AnchorSize, AnchorSize);

                    //second line
                    gMemoryBuffer.DrawRectangle(pen, cX - AnchorSize / 2, (cY + cH / 2) - AnchorSize / 2, AnchorSize, AnchorSize);
                    gMemoryBuffer.DrawRectangle(pen, (cX + cW) - AnchorSize / 2, (cY + cH / 2) - AnchorSize / 2, AnchorSize, AnchorSize);

                    //third line
                    gMemoryBuffer.DrawRectangle(pen, cX - AnchorSize / 2, (cY + cH) - AnchorSize / 2, AnchorSize, AnchorSize);
                    gMemoryBuffer.DrawRectangle(pen, (cX + cW / 2) - AnchorSize / 2, (cY + cH) - AnchorSize / 2, AnchorSize, AnchorSize);
                    gMemoryBuffer.DrawRectangle(pen, (cX + cW) - AnchorSize / 2, (cY + cH) - AnchorSize / 2, AnchorSize, AnchorSize);

                    //gBuffer.Clear(this.pnImage.BackColor);
                    gBuffer.DrawImage(bitmap, 0, 0);
                    this.currentPos = point;
                }
            }
            catch(Exception ex)
            {
                LogHelper.Error(ex);
                gBuffer.Dispose();
            }
        }

        #region Detect Cursor Position
        private bool InCutRectangle(int cX, int cY, int cW, int cH, MouseEventArgs e)
        {
            return (e.X > (cX - AnchorSize / 2) && e.X < (cX + cW + AnchorSize / 2) &&
                e.Y > (cY - AnchorSize / 2) && e.Y < (cY + cH + AnchorSize / 2));
        }

        private bool InCutRectangleLeft(int cX, int cY, int cW, int cH, MouseEventArgs e)
        {
            return (e.X > (cX - AnchorSize / 2) && e.X < (cX + AnchorSize / 2) &&
                e.Y > (cY + AnchorSize / 2) && e.Y < (cY + cH - AnchorSize / 2));
        }

        private bool InCutRectangleRight(int cX, int cY, int cW, int cH, MouseEventArgs e)
        {
            return (e.X > (cX + cW - AnchorSize / 2) && e.X < (cX + cW + AnchorSize / 2) &&
                e.Y > (cY + AnchorSize / 2) && e.Y < (cY + cH - AnchorSize / 2));
        }

        private bool InCutRectangleTop(int cX, int cY, int cW, int cH, MouseEventArgs e)
        {
            return (e.X > (cX + AnchorSize / 2) && e.X < (cX + cW - AnchorSize / 2) &&
                e.Y > (cY - AnchorSize / 2) && e.Y < (cY + AnchorSize / 2));
        }

        private bool InCutRectangleBottom(int cX, int cY, int cW, int cH, MouseEventArgs e)
        {
            return (e.X > (cX + AnchorSize / 2) && e.X < (cX + cW - AnchorSize / 2) &&
                e.Y > (cY + cH - AnchorSize / 2) && e.Y < (cY + cH + AnchorSize / 2));
        }

        private bool InCutRectangleLeftOrBottom(int cX, int cY, int cW, int cH, MouseEventArgs e)
        {
            return (e.X > (cX - AnchorSize / 2) && e.X < (cX + AnchorSize / 2) &&
                e.Y > (cY + cH - AnchorSize / 2) && e.Y < (cY + cH + AnchorSize / 2));
        }

        private bool InCutRectangleRightOrBottom(int cX, int cY, int cW, int cH, MouseEventArgs e)
        {
            return (e.X > (cX + cW - AnchorSize / 2) && e.X < (cX + cW + AnchorSize / 2) &&
                e.Y > (cY + cH - AnchorSize / 2) && e.Y < (cY + cH + AnchorSize / 2));
        }

        private bool InCutRectangleRightOrTop(int cX, int cY, int cW, int cH, MouseEventArgs e)
        {
            return (e.X > (cX + cW - AnchorSize / 2) && e.X < (cX + cW + AnchorSize / 2) &&
                e.Y > (cY - AnchorSize / 2) && e.Y < (cY + AnchorSize / 2));
        }

        private bool InCutRectangleLeftOrTop(int cX, int cY, int cW, int cH, MouseEventArgs e)
        {
            return (e.X > (cX - AnchorSize / 2) && e.X < (cX + AnchorSize / 2) &&
                e.Y > (cY - AnchorSize / 2) && e.Y < (cY + AnchorSize / 2));
        }

        #endregion

        #region Resize Retangle
        private void ReSizeRightBottom(int cX, int cY, int cW, int cH, MouseEventArgs e)
        {
            int dX = e.X - this.currentPos.X;
            int dY = e.Y - this.currentPos.Y;
            cW += dX;
            cH += dY;
            this.DrawRectangles(cX, cY, cW, cH, e.Location);
        }

        private void ReSizeLeftBottom(int cX, int cY, int cW, int cH, MouseEventArgs e)
        {
            int dX = e.X - this.currentPos.X;
            int dY = e.Y - this.currentPos.Y;
            cX += dX;
            cW -= dX;
            cH += dY;
            this.DrawRectangles(cX, cY, cW, cH, e.Location);
        }

        private void ReSizeRightTop(int cX, int cY, int cW, int cH, MouseEventArgs e)
        {
            int dX = e.X - this.currentPos.X;
            int dY = e.Y - this.currentPos.Y;
            cY += dY;
            cW += dX;
            cH -= dY;
            this.DrawRectangles(cX, cY, cW, cH, e.Location);
        }

        private void ReSizeLeftTop(int cX, int cY, int cW, int cH, MouseEventArgs e)
        {
            int dX = e.X - this.currentPos.X;
            int dY = e.Y - this.currentPos.Y;
            cX += dX;
            cY += dY;
            cW -= dX;
            cH -= dY;
            this.DrawRectangles(cX, cY, cW, cH, e.Location);
        }

        private void ReSizeRight(int cX, int cY, int cW, int cH, MouseEventArgs e)
        {
            int dX = e.X - this.currentPos.X;
            cW += dX;
            this.DrawRectangles(cX, cY, cW, cH, e.Location);
        }

        private void ReSizeLeft(int cX, int cY, int cW, int cH, MouseEventArgs e)
        {
            int dX = e.X - this.currentPos.X;
            cX += dX;
            cW -= dX;
            this.DrawRectangles(cX, cY, cW, cH, e.Location);
        }

        private void ReSizeBottom(int cX, int cY, int cW, int cH, MouseEventArgs e)
        {
            int dY = e.Y - this.currentPos.Y;
            cH += dY;
            this.cutRectangle.Height = cH;
            this.DrawRectangles(cX, cY, cW, cH, e.Location);
        }

        private void ReSizeTop(int cX, int cY, int cW, int cH, MouseEventArgs e)
        {
            int dY = e.Y - this.currentPos.Y;
            cY += dY;
            cH -= dY;
            this.DrawRectangles(cX, cY, cW, cH, e.Location);
        }

        private void MoveRectangle(int cX, int cY, int cW, int cH, MouseEventArgs e)
        {
            int dX = e.X - this.currentPos.X;
            int dY = e.Y - this.currentPos.Y;
            cX += dX;
            cY += dY;
            this.DrawRectangles(cX, cY, cW, cH, e.Location);
        }

        private bool OutOfRange(int cX, int cY, int cW, int cH)
        {
            return (cX < 0 || cY < 0 || cX > (this.pnImage.Width - cW) || cY > (this.pnImage.Height - cH));
        }

        #endregion

        private void CaptureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.OK)
            {
                this.userHead = ImageHelper.Cut(this.captureBitmap, this.cutRectangle.X, this.cutRectangle.Y, this.cutRectangle.Width, this.cutRectangle.Height);
            }
        }

        private void CaptureForm_Load(object sender, EventArgs e)
        {
            int cX = this.cutRectangle.X;
            int cY = this.cutRectangle.Y;
            int cW = this.cutRectangle.Width;
            int cH = this.cutRectangle.Height;
            this.DrawRectangles(cX, cY, cW, cH, new Point(0, 0));
        }

        private void pnImage_MouseMove(object sender, MouseEventArgs e)
        {
            int cX = this.cutRectangle.X;
            int cY = this.cutRectangle.Y;
            int cW = this.cutRectangle.Width;
            int cH = this.cutRectangle.Height;
            
            if (this.startMove)
            {
                if (this.currentCursor == CursorDirection.eTop)
                {
                    this.ReSizeTop(cX, cY, cW, cH, e);
                }
                else if (this.currentCursor == CursorDirection.eBottom)
                {
                    this.ReSizeBottom(cX, cY, cW, cH, e);
                }
                else if (this.currentCursor == CursorDirection.eLeft)
                {
                    this.ReSizeLeft(cX, cY, cW, cH, e);
                }
                else if (this.currentCursor == CursorDirection.eRight)
                {
                    this.ReSizeRight(cX, cY, cW, cH, e);
                }
                else if (this.currentCursor == CursorDirection.eLeftTop)
                {
                    this.ReSizeLeftTop(cX, cY, cW, cH, e);
                }
                else if (this.currentCursor == CursorDirection.eRightTop)
                {
                    this.ReSizeRightTop(cX, cY, cW, cH, e);
                }
                else if (this.currentCursor == CursorDirection.eLeftBottom)
                {
                    this.ReSizeLeftBottom(cX, cY, cW, cH, e);
                }
                else if (this.currentCursor == CursorDirection.eRightBottom)
                {
                    this.ReSizeRightBottom(cX, cY, cW, cH, e);
                }
                else
                {
                    this.MoveRectangle(cX, cY, cW, cH, e);
                }
            }
            else
            {
                if (this.InCutRectangle(cX, cY, cW, cH, e))
                {
                    if (this.InCutRectangleTop(cX, cY, cW, cH, e))
                    {
                        this.Cursor = Cursors.SizeNS;
                    }
                    else if (this.InCutRectangleBottom(cX, cY, cW, cH, e))
                    {
                        this.Cursor = Cursors.SizeNS;
                    }
                    else if (this.InCutRectangleLeft(cX, cY, cW, cH, e))
                    {
                        this.Cursor = Cursors.SizeWE;
                    }
                    else if (this.InCutRectangleRight(cX, cY, cW, cH, e))
                    {
                        this.Cursor = Cursors.SizeWE;
                    }
                    else if (this.InCutRectangleLeftOrTop(cX, cY, cW, cH, e))
                    {
                        this.Cursor = Cursors.SizeNWSE;
                    }
                    else if (this.InCutRectangleRightOrTop(cX, cY, cW, cH, e))
                    {
                        this.Cursor = Cursors.SizeNESW;
                    }
                    else if (this.InCutRectangleLeftOrBottom(cX, cY, cW, cH, e))
                    {
                        this.Cursor = Cursors.SizeNESW;
                    }
                    else if (this.InCutRectangleRightOrBottom(cX, cY, cW, cH, e))
                    {
                        this.Cursor = Cursors.SizeNWSE;
                    }
                    else
                    {
                        this.Cursor = Cursors.SizeAll;
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }

        

        private void pnImage_MouseDown(object sender, MouseEventArgs e)
        {
            int cX = this.cutRectangle.X;
            int cY = this.cutRectangle.Y;
            int cW = this.cutRectangle.Width;
            int cH = this.cutRectangle.Height;
            if (e.Button == MouseButtons.Left)
            {
                if (this.InCutRectangle(cX, cY, cW, cH, e))
                {
                    if (this.InCutRectangleTop(cX, cY, cW, cH, e))
                    {
                        this.currentCursor = CursorDirection.eTop;
                    }
                    else if (this.InCutRectangleBottom(cX, cY, cW, cH, e))
                    {
                        this.currentCursor = CursorDirection.eBottom;
                    }
                    else if (this.InCutRectangleLeft(cX, cY, cW, cH, e))
                    {
                        this.currentCursor = CursorDirection.eLeft;
                    }
                    else if (this.InCutRectangleRight(cX, cY, cW, cH, e))
                    {
                        this.currentCursor = CursorDirection.eRight;
                    }
                    else if (this.InCutRectangleLeftOrTop(cX, cY, cW, cH, e))
                    {
                        this.currentCursor = CursorDirection.eLeftTop;
                    }
                    else if (this.InCutRectangleRightOrTop(cX, cY, cW, cH, e))
                    {
                        this.currentCursor = CursorDirection.eRightTop;
                    }
                    else if (this.InCutRectangleLeftOrBottom(cX, cY, cW, cH, e))
                    {
                        this.currentCursor = CursorDirection.eLeftBottom;
                    }
                    else if (this.InCutRectangleRightOrBottom(cX, cY, cW, cH, e))
                    {
                        this.currentCursor = CursorDirection.eRightBottom;
                    }
                    else
                    {
                        this.currentCursor = CursorDirection.eMove;
                    }
                    this.currentPos = e.Location;
                    this.startMove = true;
                }
            }
        }

        private void pnImage_MouseUp(object sender, MouseEventArgs e)
        {
            int cX = this.cutRectangle.X;
            int cY = this.cutRectangle.Y;
            int cW = this.cutRectangle.Width;
            int cH = this.cutRectangle.Height;
            if (e.Button == MouseButtons.Left)
            {
                if (this.InCutRectangle(cX, cY, cW, cH, e))
                {
                    this.startMove = false;
                }
            }
        }

        private void pnImage_Paint(object sender, PaintEventArgs e)
        {
            int cX = this.cutRectangle.X;
            int cY = this.cutRectangle.Y;
            int cW = this.cutRectangle.Width;
            int cH = this.cutRectangle.Height;
            this.DrawRectangles(cX, cY, cW, cH, this.currentPos);
        }
    }
}
