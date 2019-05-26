using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FaceDetection
{
    /// <summary>
    /// 人脸特征
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FaceModel
    {
        public IntPtr pbFeature;
        [MarshalAs(UnmanagedType.I4)]
        public int lFeatureSize;
    }
}
