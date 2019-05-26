using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FaceDetection
{
    public class Gender
    {
        [DllImport(@"lib\libarcsoft_fsdk_gender_estimation.dll", EntryPoint = "ASGE_FSDK_InitGenderEngine", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Init(string appId, string sdkKey, byte[] buffer, int bufferSize, out IntPtr engine);
        [DllImport(@"lib\libarcsoft_fsdk_gender_estimation.dll", EntryPoint = "ASGE_FSDK_GenderEstimation_StaticImage", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int EstimationStatic(IntPtr engine, ref ImageData imgData, ref EstimationInput estimationInputInput, out EstimationResult pGenderResult);
        [DllImport(@"lib\libarcsoft_fsdk_gender_estimation.dll", EntryPoint = "ASGE_FSDK_GenderEstimation_Preview", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int EstimationPreview(IntPtr engine, ref ImageData imgData, ref EstimationInput estimationInputInput, out EstimationResult pGenderResesult);
        [DllImport(@"lib\libarcsoft_fsdk_gender_estimation.dll", EntryPoint = "ASGE_FSDK_UninitGenderEngine", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Close(IntPtr engine);
    }
}
