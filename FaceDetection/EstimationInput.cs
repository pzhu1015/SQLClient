using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FaceDetection
{
    /// <summary>
    /// 性别和年龄评估的输入参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EstimationInput
    {
        public IntPtr pFaceRectArray;
        public IntPtr pFaceOrientArray;
        public int lFaceNumber;
    }
}
