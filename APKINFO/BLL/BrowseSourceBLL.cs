/***********************************************
 * 作者: ZhongGuanYao
 * 邮箱: 598115778@qq.com
 * 博客: https://blog.csdn.net/xiangxinzijiwonen
 *       https://github.com/zhongguanyao
 * 日期: 2018-10-29
 * **********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APKINFO.Utils;
using APKINFO.Entity;
using System.IO;

namespace APKINFO.BLL
{
    /// <summary>
    /// 业务逻辑类-查看源码
    /// </summary>
    public class BrowseSourceBLL
    {
        /// <summary>
        /// 用jadx-gui.bat打开文件
        /// </summary>
        /// <param name="filePath">以.jar/.dex/.apk为后缀的文件路径</param>
        public static void OpenJadxGUI(string filePath) {

            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return;

            string jadxPath = PathUtils.GetPath(Constants.PATH_JADXGUI);
            if (!File.Exists(jadxPath))
            {
                LogUtils.WriteLine(jadxPath + "不存在！");
                return;
            }

            string cmdArgs = jadxPath + " " + filePath;
            CmdUtils.ExecCmd(cmdArgs);
        }



    }
}
