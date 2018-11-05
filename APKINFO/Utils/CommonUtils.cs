using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using Ionic.Zip;
using System.Text.RegularExpressions;

namespace APKINFO
{
    /// <summary>
    /// 常用工具类
    /// </summary>
    public class CommonUtils
    {

        /// <summary>
        /// 字符串列表通过逗号隔开转换为字符串(列表中的空串会被剔除)
        /// </summary>
        /// <param name="strLst"></param>
        /// <returns></returns>
        public static string ToStr(List<string> strLst)
        {
            if (strLst == null || strLst.Count <= 0) return null;

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < strLst.Count; i++)
            {
                if (string.IsNullOrEmpty(strLst[i])) continue;
                builder.Append(strLst[i]);
                if (i != strLst.Count - 1)
                {
                    builder.Append(",");
                }
            }
            return builder.ToString();
        }


        /// <summary>
        /// 通过逗号（包含中英文逗号）隔开的字符串转换成列表(列表中的空串会被剔除)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static List<string> ToList(string str)
        {
            if (string.IsNullOrEmpty(str)) return null;

            string[] ss = str.Split(new char[] { ',', '，'}, StringSplitOptions.RemoveEmptyEntries);
            return ss.ToList<string>();
        }


    }
}
