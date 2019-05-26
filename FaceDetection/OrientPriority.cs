using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaceDetection
{
    /// <summary>
    /// 脸部角度的检测范围
    /// </summary>
    public enum OrientPriority
    {
        /// <summary>
        /// 检测 0 度（±45 度）方向
        /// </summary>
        Only0 = 0x1,

        /// <summary>
        /// 检测 90 度（±45 度）方向
        /// </summary>
        Only90 = 0x2,

        /// <summary>
        /// 检测 270 度（±45 度）方向
        /// </summary>
        Only270 = 0x3,

        /// <summary>
        /// 检测 180 度（±45 度）方向
        /// </summary>
        Only180 = 0x4,

        /// <summary>
        /// 检测 0， 90， 180， 270 四个方向,0 度更优先
        /// </summary>
        Ext0 = 0x5
    }

}
