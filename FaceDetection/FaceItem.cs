using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaceDetection
{
    public class FaceItem
    {
        /// <summary>
        /// 用于排序
        /// </summary>
        public long OrderId { get; set; }
        /// <summary>
        /// Face唯一标识
        /// </summary>
        public string FaceID { get; set; }
        /// <summary>
        /// Face唯一标识对应的口令
        /// </summary>
        public string FacePassword { get; set; }
        /// <summary>
        /// 人脸模型
        /// </summary>
        public FaceModel FaceModel { get; set; }
    }
}
