using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FaceDetection
{
    /// <summary>
    /// 人脸在图片中的位置
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FaceRect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
}
