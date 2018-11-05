/***********************************************
 * 作者: ZhongGuanYao
 * 邮箱: 598115778@qq.com
 * 博客: https://blog.csdn.net/xiangxinzijiwonen
 *       https://github.com/zhongguanyao
 * 日期: 2018-10-27
 * **********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APKINFO.Entity;

namespace APKINFO.Utils
{
    /// <summary>
    /// 文件路径工具类
    /// </summary>
    public class PathUtils
    {
        public static string GetPath(string path)
        {
            return Constants.PATH_ROOT + "\\" + path;
        }

        public static string JoinPath(string path1, string path2)
        {
            return path1 + "\\" + path2;
        }

    }
}
