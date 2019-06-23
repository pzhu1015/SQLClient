using AForge.Controls;
using Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FaceDetection
{
    /// <summary>
    /// 请到http://www.arcsoft.com.cn/ai/arcface.html注册应用你以获取免费AppKey, TraceKey
    /// </summary>
    public class AIFace
    {
        public event CaputurePictureEventHandler CaputurePicture;
        public string FaceAppKey { get; set; } = "E4MgeCvb119u379KTANDjnWL2u7xWsF9QDsUDtNrztUW";
        public string FaceTraceKey { get; set; } = "21GVdPnC3cFmaukVCSCvGGAxRpqens4AAHfc4sneCuxz";
        public string FaceDetectKey { get; set; } = "21GVdPnC3cFmaukVCSCvGGB5bE6oBDQQeLCBerB5LWN4";
        public string FaceMatchKey { get; set; } = "21GVdPnC3cFmaukVCSCvGGBaEq9Ukh5w6iSdhezfEa2Z";
        public string FaceAgeKey { get; set; } = "21GVdPnC3cFmaukVCSCvGGBpZdfq5zHXJvPzGQCd7B7a";
        public string FaceGenderKey { get; set; } = "21GVdPnC3cFmaukVCSCvGGBwj2w3AeaoafNJQWh48i3F";
        public int FaceScale { get; set; } = 16;
        public int FaceNumber { get; set; } = 1;
        public VideoSourcePlayer VideoPlayer { get; set; }
        public List<FaceItem> Faces { get; set; }
        public bool IsDetected { get; set; } = false;
        public FaceResult FaceResult
        {
            get
            {
                return faceResult;
            }

            set
            {
                faceResult = value;
            }
        }

        /// <summary>
        /// 缓存大小
        /// </summary>
        private const int BufferSize = 40 * 1024 * 1024;
        /// <summary>
        /// 人脸跟踪的缓存
        /// </summary>
        private byte[] faceTraceBuffer = new byte[BufferSize];
        /// <summary>
        /// 人脸检测的缓存
        /// </summary>
        private byte[] faceDetectBuffer = new byte[BufferSize];
        /// <summary>
        /// 人脸比对的缓存
        /// </summary>
        private byte[] faceMatchBuffer = new byte[BufferSize];
        /// <summary>
        /// 年龄识别的缓存
        /// </summary>
        private byte[] faceAgeBuffer = new byte[BufferSize];
        /// <summary>
        /// 性别识别的缓存
        /// </summary>
        private byte[] faceGenderBuffer = new byte[BufferSize];
        /// <summary>
        /// 人脸跟踪的引擎
        /// </summary>
        private IntPtr faceTraceEnginer = IntPtr.Zero;
        /// <summary>
        /// 人脸检测的引擎
        /// </summary>
        private IntPtr faceDetectEnginer = IntPtr.Zero;
        /// <summary>
        /// 人脸比对的引擎
        /// </summary>
        private IntPtr faceMatchEngine = IntPtr.Zero;
        /// <summary>
        /// 年龄识别的引擎
        /// </summary>
        private IntPtr faceAgeEngine = IntPtr.Zero;
        /// <summary>
        /// 性别识别的引擎
        /// </summary>
        private IntPtr faceGenderEngine = IntPtr.Zero;

        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        private FaceResult faceResult = new FaceResult();

        private bool registerClicked = false;

        private byte[] registerFeatureData = null;

        private Camera camera = null;

        private double rateW, rateH;

        public AIFace(
            string faceAppKey, 
            string faceTraceKey, 
            string faceDetectKey, 
            string faceMatchKey, 
            string faceAgeKey, 
            string faceGenderKey, 
            int faceScale, 
            int faceNum,
            VideoSourcePlayer videoplayer)
        {
            this.FaceAppKey = faceAppKey;
            this.FaceTraceKey = faceTraceKey;
            this.FaceDetectKey = faceDetectKey;
            this.FaceMatchKey = faceMatchKey;
            this.FaceAgeKey = faceAgeKey;
            this.FaceGenderKey = faceGenderKey;
            this.FaceScale = faceScale;
            this.FaceNumber = faceNum;
            this.VideoPlayer = videoplayer;
        }

        public AIFace(VideoSourcePlayer videoplayer)
        {
            this.VideoPlayer = videoplayer;
        }

        private void OnCaputurePicture(CapturePictureEventArgs e)
        {
            if (this.CaputurePicture != null)
            {
                this.CaputurePicture(this, e);
            }
        }

        /// <summary>
        /// 初始化人脸识别
        /// </summary>
        /// <returns></returns>
        public ErrorCode Initialize()
        {
            ErrorCode initResult = ErrorCode.Unknown;
            try
            {
                initResult = (ErrorCode)Trace.Init(this.FaceAppKey, this.FaceTraceKey, faceTraceBuffer, BufferSize, out faceTraceEnginer, (int)OrientPriority.Only0, this.FaceScale, this.FaceNumber);
                if (initResult != ErrorCode.Ok)
                {
                    //MessageBox.Show("初始化人脸跟踪引擎失败，错误代码为：" + initResult);
                    return initResult;
                }

                initResult = (ErrorCode)Detect.Init(this.FaceAppKey, this.FaceDetectKey, faceDetectBuffer, BufferSize, out faceDetectEnginer, (int)OrientPriority.Only0, this.FaceScale, this.FaceNumber);
                if (initResult != ErrorCode.Ok)
                {
                    //MessageBox.Show("初始化人脸检测引擎失败，错误代码为：" + initResult);
                    return initResult;
                }

                initResult = (ErrorCode)Match.Init(this.FaceAppKey, this.FaceMatchKey, faceMatchBuffer, BufferSize, out faceMatchEngine);
                if (initResult != ErrorCode.Ok)
                {
                    //MessageBox.Show("初始化人脸比对引擎失败，错误代码为：" + initResult);
                    return initResult;
                }

                initResult = (ErrorCode)Age.Init(this.FaceAppKey, this.FaceAgeKey, faceAgeBuffer, BufferSize, out faceAgeEngine);
                if (initResult != ErrorCode.Ok)
                {
                    //MessageBox.Show("初始化年龄识别引擎失败，错误代码为：" + initResult);
                    return initResult;
                }

                initResult = (ErrorCode)Gender.Init(this.FaceAppKey, this.FaceGenderKey, faceGenderBuffer, BufferSize, out faceGenderEngine);
                if (initResult != ErrorCode.Ok)
                {
                    //MessageBox.Show("初始化性别识别引擎失败，错误代码为：" + initResult);
                    return initResult;
                }
                return initResult;
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return initResult;
            }
        }

        /// <summary>
        /// 启动摄像头,开始识别人脸
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            this.camera = new Camera();
            if (!this.camera.HasVideoDevice)
            {
                //MessageBox.Show("没有检测到摄像头");
                return false;
            }

            this.VideoPlayer.VideoSource = camera.VideoSource;
            this.VideoPlayer.Start();

            rateH = 1.0 * this.VideoPlayer.Height / this.camera.FrameHeight;
            rateW = 1.0 * this.VideoPlayer.Width / this.camera.FrameWidth;
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                while (!cancellationTokenSource.IsCancellationRequested)
                {
                    #region 200毫秒左右
                    MatchFrame();
                    #endregion
                }
            }, cancellationTokenSource.Token);
            return true;
        }

        public bool Stop()
        {
            if (this.camera.HasVideoDevice)
            {
                this.cancellationTokenSource.Cancel();
                Thread.Sleep(500);
                this.VideoPlayer.Stop();

                if (this.faceMatchEngine != IntPtr.Zero)
                {
                    Match.Close(this.faceMatchEngine);
                }
                if (this.faceTraceEnginer != IntPtr.Zero)
                {
                    Trace.Close(this.faceTraceEnginer);
                }
                if (this.faceDetectEnginer != IntPtr.Zero)
                {
                    Detect.Close(this.faceDetectEnginer);
                }
                if (this.faceAgeEngine != IntPtr.Zero)
                {
                    Age.Close(this.faceAgeEngine);
                }
                if (this.faceGenderEngine != IntPtr.Zero)
                {
                    Gender.Close(this.faceGenderEngine);
                }
            }
            return true;
        }

        public bool CapturePicture()
        {
            this.registerClicked = true;
            return true;
        }

        private void MatchFrame()
        {
            #region 获取图片 1毫秒
            var bitmap = this.VideoPlayer.GetCurrentVideoFrame();
            #endregion

            Stopwatch sw = new Stopwatch();
            sw.Start();
            #region 图片转换 0.7-2微妙
            var bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var imageData = new ImageData
            {
                u32PixelArrayFormat = 513,//Rgb24,
                i32Width = bitmap.Width,
                i32Height = bitmap.Height,
                pi32Pitch = new int[4],
                ppu8Plane = new IntPtr[4]
            };
            imageData.pi32Pitch[0] = bmpData.Stride;
            imageData.ppu8Plane[0] = bmpData.Scan0;

            sw.Stop();
            faceResult.Score = sw.ElapsedTicks;


            #endregion

            try
            {
                #region 人脸检测 5-8毫秒
                DetectResult pDetectResult = new DetectResult();
                var ret = (ErrorCode)Detect.Detection(faceDetectEnginer, ref imageData, out pDetectResult.lfaceOrient);
                if (ret != ErrorCode.Ok)
                    return;
                var detectResult = (DetectResult)Marshal.PtrToStructure(pDetectResult.lfaceOrient, typeof(DetectResult));
                if (detectResult.nFace == 0)
                    return;
                var faceRect = (FaceRect)Marshal.PtrToStructure(detectResult.rcFace, typeof(FaceRect));
                faceResult.Rectangle = new Rectangle((int)(faceRect.left * rateW), (int)(faceRect.top * rateH), (int)((faceRect.right - faceRect.left) * rateW), (int)((faceRect.bottom - faceRect.top) * rateH));
                faceResult.RealRetangle = new Rectangle(faceRect.left, faceRect.top, (faceRect.right - faceRect.left), (faceRect.bottom - faceRect.top));
                var faceOrient = (int)Marshal.PtrToStructure(detectResult.lfaceOrient, typeof(int));
                #endregion


                #region 性别识别基本准确 年龄识别误差太大，没什么应用场景
                //ExtraFaceInput faceInput = new ExtraFaceInput()
                //{
                //    lFaceNumber = facesDetect.nFace,
                //    pFaceRectArray = Marshal.AllocHGlobal(Marshal.SizeOf(faceRect)),
                //    pFaceOrientArray = Marshal.AllocHGlobal(Marshal.SizeOf(faceOrient))
                //};
                //Marshal.StructureToPtr(faceRect, faceInput.pFaceRectArray, false);
                //Marshal.StructureToPtr(faceOrient, faceInput.pFaceOrientArray, false);

                //var ageResult = Face.Age.ASAE_FSDK_AgeEstimation_Preview(faceAgeEngine, ref imageData, ref faceInput, out var pAgeRes);
                //var ages = pAgeRes.pResult.ToStructArray<int>(pAgeRes.lFaceNumber);
                //var genderResult = Face.Gender.ASGE_FSDK_GenderEstimation_Preview(faceGenderEngine, ref imageData, ref faceInput, out var pGenderRes);
                //var genders = pGenderRes.pResult.ToStructArray<int>(pGenderRes.lFaceNumber);
                //faceResult.Age = ages[0];
                //faceResult.Gender = genders[0];

                //Marshal.FreeHGlobal(faceInput.pFaceOrientArray);
                //Marshal.FreeHGlobal(faceInput.pFaceRectArray);
                #endregion

                #region 获取人脸特征 160-180毫秒
                var faceFeatureInput = new FaceFeatureInput
                {
                    rcFace = faceRect,
                    lOrient = faceOrient
                };

                var faceModel = new FaceModel();
                ret = (ErrorCode)Match.ExtractFeature(faceMatchEngine, ref imageData, ref faceFeatureInput, out faceModel);
                #endregion

                if (ret == ErrorCode.Ok)
                {
                    if (registerClicked)
                    {
                        this.registerFeatureData = new byte[faceModel.lFeatureSize];
                        Marshal.Copy(faceModel.pbFeature, registerFeatureData, 0, faceModel.lFeatureSize);
                    }

                    #region 人脸识别（100张人脸） 17-20毫秒
                    bool isdetected = false;
                    foreach (var item in this.Faces.OrderByDescending(i => i.OrderId))
                    {
                        var fm = item.FaceModel;
                        float score = 0;
                        Match.FacePairMatch(faceMatchEngine, ref fm, ref faceModel, out score);
                        if (score > 0.5)
                        {
                            item.OrderId = DateTime.Now.Ticks;
                            faceResult.ID = item.FaceID;
                            isdetected = true;
                            break;
                        }
                    }
                    this.IsDetected = isdetected;
                    #endregion

                }

            }
            finally
            {
                bitmap.UnlockBits(bmpData);
                if (registerClicked)
                {
                    this.OnCaputurePicture(new CapturePictureEventArgs(bitmap, this.faceResult.RealRetangle, this.registerFeatureData));
                    registerClicked = false;
                }
                else
                {
                    bitmap.Dispose();
                }
            }
        }


        public bool MatchBitmap(Bitmap bitmap, FaceModel fm)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            #region 图片转换 0.7-2微妙
            var bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            var imageData = new ImageData
            {
                u32PixelArrayFormat = 513,//Rgb24,
                i32Width = bitmap.Width,
                i32Height = bitmap.Height,
                pi32Pitch = new int[4],
                ppu8Plane = new IntPtr[4]
            };
            imageData.pi32Pitch[0] = bmpData.Stride;
            imageData.ppu8Plane[0] = bmpData.Scan0;

            sw.Stop();
            faceResult.Score = sw.ElapsedTicks;

            #endregion

            try
            {
                #region 人脸检测 5-8毫秒
                DetectResult pDetectResult = new DetectResult();
                var ret = (ErrorCode)Detect.Detection(faceDetectEnginer, ref imageData, out pDetectResult.lfaceOrient);
                if (ret != ErrorCode.Ok)
                    return false;
                var detectResult = (DetectResult)Marshal.PtrToStructure(pDetectResult.lfaceOrient, typeof(DetectResult));
                if (detectResult.nFace == 0)
                    return false;
                var faceRect = (FaceRect)Marshal.PtrToStructure(detectResult.rcFace, typeof(FaceRect));
                faceResult.Rectangle = new Rectangle((int)(faceRect.left * rateW), (int)(faceRect.top * rateH), (int)((faceRect.right - faceRect.left) * rateW), (int)((faceRect.bottom - faceRect.top) * rateH));
                faceResult.RealRetangle = new Rectangle(faceRect.left, faceRect.top, (faceRect.right - faceRect.left), (faceRect.bottom - faceRect.top));
                var faceOrient = (int)Marshal.PtrToStructure(detectResult.lfaceOrient, typeof(int));
                #endregion

                #region 获取人脸特征 160-180毫秒
                var faceFeatureInput = new FaceFeatureInput
                {
                    rcFace = faceRect,
                    lOrient = faceOrient
                };

                var faceModel = new FaceModel();
                ret = (ErrorCode)Match.ExtractFeature(faceMatchEngine, ref imageData, ref faceFeatureInput, out faceModel);
                #endregion

                if (ret == ErrorCode.Ok)
                {
                    if (registerClicked)
                    {
                        this.registerFeatureData = new byte[faceModel.lFeatureSize];
                        Marshal.Copy(faceModel.pbFeature, registerFeatureData, 0, faceModel.lFeatureSize);
                    }

                    #region 人脸识别（100张人脸） 17-20毫秒
                    float score = 0;
                    Match.FacePairMatch(faceMatchEngine, ref fm, ref faceModel, out score);
                    if (score > 0.5)
                    {
                        return true;
                    }
                    #endregion
                }
            }
            finally
            {
                bitmap.UnlockBits(bmpData);
            }
            return false;
        }

    }

    public delegate void CaputurePictureEventHandler(object sender, CapturePictureEventArgs e);
    public class CapturePictureEventArgs : EventArgs
    {
        private Bitmap bitmap;
        private Rectangle rectangle;
        private byte[] faceResult;
        public CapturePictureEventArgs(Bitmap bitmap, Rectangle rectangle, byte[] faceResult)
            :
            base()
        {
            this.bitmap = bitmap;
            this.rectangle = rectangle;
            this.faceResult = faceResult;
        }

        public Bitmap Bitmap
        {
            get
            {
                return bitmap;
            }

            set
            {
                bitmap = value;
            }
        }

        public Rectangle Rectangle
        {
            get
            {
                return rectangle;
            }

            set
            {
                rectangle = value;
            }
        }

        public byte[] FaceResult
        {
            get
            {
                return faceResult;
            }

            set
            {
                faceResult = value;
            }
        }
    }

}
