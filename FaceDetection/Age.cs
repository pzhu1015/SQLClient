using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FaceDetection
{
    public class Age
    {
        [DllImport(@"lib\libarcsoft_fsdk_age_estimation.dll", EntryPoint = "ASAE_FSDK_InitAgeEngine", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Init(string appId, string sdkKey, byte[] buffer, int bufferSize, out IntPtr engine);
        [DllImport(@"lib\libarcsoft_fsdk_age_estimation.dll", EntryPoint = "ASAE_FSDK_AgeEstimation_StaticImage", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int EstimationStatic(IntPtr engine, ref ImageData imgData, ref EstimationInput estimationInputInput, out EstimationResult pAgeResult);
        [DllImport(@"lib\libarcsoft_fsdk_age_estimation.dll", EntryPoint = "ASAE_FSDK_AgeEstimation_Preview", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int EstimationPreview(IntPtr engine, ref ImageData imgData, ref EstimationInput estimationInputInput, out EstimationResult pAgeResult);
        [DllImport(@"lib\libarcsoft_fsdk_age_estimation.dll", EntryPoint = "ASAE_FSDK_UninitAgeEngine", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Close(IntPtr engine);
    }
}
