using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Helper
{
    public class BytesHelper
    {
        //将image转换成byte[]数据
        public static byte[] ImageToByte(Image _image)
        {
            MemoryStream ms = new MemoryStream();
            _image.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }
        //将byte[]数据转换成image
        public static Image ByteToImage(byte[] myByte)
        {
            MemoryStream ms = new MemoryStream(myByte);
            Image _Image = Image.FromStream(ms);
            return _Image;
        }
    }
}
