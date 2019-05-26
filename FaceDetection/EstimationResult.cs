using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FaceDetection
{
    /// <summary>
    /// 性别和年龄评估的结果
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct EstimationResult
    {
        public IntPtr pResult;
        public int lFaceNumber;
    }
}
