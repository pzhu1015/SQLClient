using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace FaceDetection
{
    public class Match
    {
        [DllImport(@"lib\libarcsoft_fsdk_face_recognition.dll", EntryPoint = "AFR_FSDK_InitialEngine", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Init(string appId, string sdkKey, byte[] buffer, int bufferSize, out IntPtr engine);
        [DllImport(@"lib\libarcsoft_fsdk_face_recognition.dll", EntryPoint = "AFR_FSDK_ExtractFRFeature", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int ExtractFeature(IntPtr engine, ref ImageData imageData, ref FaceFeatureInput faceFeatureInput, out FaceModel pFaceModels);
        [DllImport(@"lib\libarcsoft_fsdk_face_recognition.dll", EntryPoint = "AFR_FSDK_FacePairMatching", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int FacePairMatch(IntPtr engine, ref FaceModel faceModel1, ref FaceModel faceModel2, out float score);
        [DllImport(@"lib\libarcsoft_fsdk_face_recognition.dll", EntryPoint = "AFR_FSDK_UninitialEngine", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int Close(IntPtr engine);
    }
}
