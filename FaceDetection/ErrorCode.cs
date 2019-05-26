using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaceDetection
{
    /// <summary>
    /// 错误代码
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// 正确
        /// </summary>
        Ok = 0,

        /// <summary>
        /// 通用错误类型
        /// </summary>
        BasicBase = 0x0001,

        /// <summary>
        /// 错误原因不明
        /// </summary>
        Unknown = BasicBase,

        /// <summary>
        /// 无效的参数
        /// </summary>
        InvalidParam = BasicBase + 1,

        /// <summary>
        /// 引擎不支持
        /// </summary>
        Unsupported = BasicBase + 2,

        /// <summary>
        /// 内存不足
        /// </summary>
        NoMemory = BasicBase + 3,

        /// <summary>
        /// 状态错误
        /// </summary>
        BadState = BasicBase + 4,

        /// <summary>
        /// 用户取消相关操作
        /// </summary>
        UserCancel = BasicBase + 5,

        /// <summary>
        /// 操作时间过期
        /// </summary>
        Expired = BasicBase + 6,

        /// <summary>
        /// 用户暂停操作
        /// </summary>
        UserPause = BasicBase + 7,

        /// <summary>
        /// 缓冲上溢
        /// </summary>
        BufferOverflow = BasicBase + 8,

        /// <summary>
        /// 缓冲下溢
        /// </summary>
        BufferUnderflow = BasicBase + 9,

        /// <summary>
        /// 存贮空间不足
        /// </summary>
        NoDiskspace = BasicBase + 10,

        /// <summary>
        /// 组件不存在
        /// </summary>
        ComponentNotExist = BasicBase + 11,

        /// <summary>
        /// 全局数据不存在
        /// </summary>
        GlobalDataNotExist = BasicBase + 12,

        /// <summary>
        /// Free SDK通用错误类型
        /// </summary>
        SdkBase = 0x7000,

        /// <summary>
        /// 无效的App Id
        /// </summary>
        InvalidAppId = SdkBase + 1,

        /// <summary>
        /// 无效的SDK key
        /// </summary>
        InvalidSdkId = SdkBase + 2,

        /// <summary>
        /// AppId和SDKKey不匹配
        /// </summary>
        InvalidIdPair = SdkBase + 3,

        /// <summary>
        /// SDKKey 和使用的SDK 不匹配
        /// </summary>
        MismatchIdAndSdk = SdkBase + 4,

        /// <summary>
        /// 系统版本不被当前SDK所支持
        /// </summary>
        SystemVersionUnsupported = SdkBase + 5,

        /// <summary>
        /// SDK有效期过期，需要重新下载更新
        /// </summary>
        LicenceExpired = SdkBase + 6,

        /// <summary>
        /// Face Recognition错误类型
        /// </summary>
        FaceRecognitionBase = 0x12000,

        /// <summary>
        /// 无效的输入内存
        /// </summary>
        InvalidMemoryInfo = FaceRecognitionBase + 1,

        /// <summary>
        /// 无效的输入图像参数
        /// </summary>
        InvalidImageInfo = FaceRecognitionBase + 2,

        /// <summary>
        /// 无效的脸部信息
        /// </summary>
        InvalidFaceInfo = FaceRecognitionBase + 3,

        /// <summary>
        /// 当前设备无GPU可用
        /// </summary>
        NoGpuAvailable = FaceRecognitionBase + 4,

        /// <summary>
        /// 待比较的两个人脸特征的版本不一致
        /// </summary>
        MismatchedFeatureLevel = FaceRecognitionBase + 5
    }
}
