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
using System.IO;
using System.Windows.Forms;

namespace APKINFO
{
    /// <summary>
    /// 日志工具类
    /// </summary>
    public class LogUtils
    {
        
        public static void WriteLine(string msg)
        {
            string path = Application.StartupPath + "log\\log" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            msg = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff ") + msg + "\r\n";
            File.AppendAllText(path, msg);
         }
    }
}
