using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FaceDetection
{
    /// <summary>
    /// 人脸跟踪、检测、性别年龄评估和获取人脸信息的输入参数
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ImageData
    {
        public uint u32PixelArrayFormat;
        public int i32Width;
        public int i32Height;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public IntPtr[] ppu8Plane;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I4)]
        public int[] pi32Pitch;
    }
}
