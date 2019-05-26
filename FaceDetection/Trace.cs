using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FaceDetection
{
    public class Trace
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="sdkKey"></param>
        /// <param name="buffer"></param>
        /// <param name="bufferSize"></param>
        /// <param name="engine"></param>
        /// <param name="orientPriority"></param>
        /// <param name="scale">最小人脸尺寸有效值范围[2,16] 推荐值 16。该尺寸是人脸相对于所在图片的长边的占比。例如，如果用户想检测到的最小人脸尺寸是图片长度的 1/8，那么这个 nScale 就应该设置为8</param>
        /// <param name="faceNumber">用户期望引擎最多能检测出的人脸数有效值范围[1,20]</param>
        /// <returns></returns>        
        [DllImport(@"lib\libarcsoft_fsdk_face_tracking.dll", EntryPoint = "AFT_FSDK_InitialFaceEngine", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Init(string appId, string sdkKey, byte[] buffer, int bufferSize, out IntPtr engine, int orientPriority, int scale, int faceNumber);
        [DllImport(@"lib\libarcsoft_fsdk_face_tracking.dll", EntryPoint = "AFT_FSDK_FaceFeatureDetect", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Detection(IntPtr engine, ref ImageData imgData, out IntPtr pTraceResult);
        [DllImport(@"lib\libarcsoft_fsdk_face_tracking.dll", EntryPoint = "AFT_FSDK_UninitialFaceEngine", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Close(IntPtr engine);
    }
}
