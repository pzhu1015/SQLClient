using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FaceDetection
{
    /// <summary>
    /// 人脸跟踪的结果
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TraceResult
    {
        [MarshalAs(UnmanagedType.I4)]
        public int nFace;
        [MarshalAs(UnmanagedType.I4)]
        public int lfaceOrient;
        public IntPtr rcFace;
    }
}
