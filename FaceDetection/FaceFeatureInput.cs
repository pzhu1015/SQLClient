using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FaceDetection
{
    /// <summary>
    /// 获取人脸特征的输入参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FaceFeatureInput
    {
        public FaceRect rcFace;
        public int lOrient;
    }
}
