using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace FaceDetection
{
    public class FaceResult
    {
        public int NotMatchedCount { get; set; }
        public string ID { get; set; }
        public float Score { get; set; }
        public Rectangle Rectangle { get; set; }
        public Rectangle RealRetangle { get; set; }
        public int Age { get; set; }
        /// <summary>
        /// 0：男，1：女，其他：未知
        /// </summary>
        public int Gender { get; set; }
        public override string ToString()
        {
            string ret = "";
            if (!string.IsNullOrEmpty(ID))
                ret = ID + "，";
            ret += Age + "岁";
            if (Gender == 0)
                ret += "，男";
            else if (Gender == 1)
                ret += "，女";

            return ret + "," + Score;
        }
    }
}
