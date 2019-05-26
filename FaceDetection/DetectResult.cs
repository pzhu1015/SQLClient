using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FaceDetection
{
    /// <summary>
    /// 人脸检测的结果
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DetectResult
    {
        [MarshalAs(UnmanagedType.I4)]
        public int nFace;
        public IntPtr rcFace;
        public IntPtr lfaceOrient;
    }
}
