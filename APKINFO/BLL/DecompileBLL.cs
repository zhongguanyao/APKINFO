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
    /// 业务逻辑类-反编译APK
    /// </summary>
    public class DecompileBLL
    {

        /// <summary>
        /// 反编译
        /// </summary>
        /// <param name="apkFilePath"></param>
        /// <returns>返回保存反编译信息的目录,为空表示反编译失败</returns>
        public static string DecompileApk(string apkFilePath)
        {
            if (string.IsNullOrEmpty(apkFilePath) 
                || !File.Exists(apkFilePath)) return null;

            string apktoolPath = PathUtils.GetPath(Constants.PATH_APKTOOL);
            if (!File.Exists(apktoolPath))
            {
                LogUtils.WriteLine(apktoolPath + "不存在！");
                return null;
            }

            string decompileDir = PathUtils.GetPath(Constants.DIR_TEMP_DECOMPLIE);
            if (!Directory.Exists(decompileDir))
            {
                Directory.CreateDirectory(decompileDir);
            }
            FileUtils.ClearDir(decompileDir);
            decompileDir = PathUtils.JoinPath(decompileDir, DateTime.Now.ToString("yyyyMMdd_HHmmss"));

            string cmdArgs = apktoolPath + " d " + apkFilePath + " -o " + decompileDir;// -o后面的目录不能已存在
            string[] res = CmdUtils.ExecCmdAndWaitForExit(cmdArgs);
            if (res == null || res.Length < 2 || !string.IsNullOrEmpty(res[1]))
                return null;

            return decompileDir;
        }

    }
}
