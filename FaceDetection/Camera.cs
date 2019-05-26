using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaceDetection
{
    public class Camera
    {
        /// <summary>
        /// 是否有摄像头
        /// </summary>
        public bool HasVideoDevice { get; set; }
        /// <summary>
        /// 视频源
        /// </summary>
        public VideoCaptureDevice VideoSource { get; set; }
        /// <summary>
        /// 视频图片的宽度
        /// </summary>
        public int FrameWidth { get; set; }
        /// <summary>
        /// 视频图片的高度
        /// </summary>
        public int FrameHeight { get; set; }
        /// <summary>
        /// 视频图片的字节数
        /// </summary>
        public int ByteCount { get; set; }

        public Camera()
        {
            var videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count == 0)//没有检测到摄像头
            {
                HasVideoDevice = false;
                return;
            }

            VideoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);//连接第一个摄像头
            var videoResolution = VideoSource.VideoCapabilities.First(ii => ii.FrameSize.Width == VideoSource.VideoCapabilities.Max(jj => jj.FrameSize.Width)); //获取摄像头最高的分辨率

            FrameWidth = videoResolution.FrameSize.Width;
            FrameHeight = videoResolution.FrameSize.Height;
            ByteCount = videoResolution.BitCount / 8;
            VideoSource.VideoResolution = videoResolution;
            HasVideoDevice = true;
        }
    }
}
