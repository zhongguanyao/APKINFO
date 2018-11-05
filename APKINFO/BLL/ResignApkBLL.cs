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
using APKINFO.Entity;
using System.IO;
using APKINFO.Utils;
using System.Diagnostics;
using APKINFO.Interface;

namespace APKINFO.BLL
{
    /// <summary>
    /// 业务逻辑类-重签名APK
    /// </summary>
    public class ResignApkBLL
    {
        /// <summary>
        /// 重签名
        /// </summary>
        /// <param name="apkFilePath"></param>
        /// <param name="newApkPath"></param>
        /// <param name="config"></param>
        /// <param name="logView"></param>
        /// <returns></returns>
        public static bool ResignApk(string apkFilePath, string newApkPath, KeystoreConfig config, ILog logView)
        {
            if (string.IsNullOrEmpty(apkFilePath) || !File.Exists(apkFilePath))
            {
                logView.Log(">>>>>>重新签名失败: " + apkFilePath + "不存在！");
                return false;
            }

            string tempDir = PathUtils.GetPath(Constants.DIR_TEMP_RESIGNAPK);
            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }
            FileUtils.ClearDir(tempDir);
            string tempApkFilePath = PathUtils.JoinPath(tempDir, "temp.apk");

            // 拷贝一份APK作为临时文件，在该临时文件上操作
            FileInfo fi = new FileInfo(apkFilePath);
            if (fi.Exists)
            {
                fi.CopyTo(tempApkFilePath, true);
            }

            logView.BlankLine();
            logView.Log("删除APK文件中META-INF目录下的签名文件");
            ZipUtils.RemoveApkCertFile(tempApkFilePath, logView);

            logView.BlankLine();
            logView.Log("↓↓↓↓↓↓ 重签名-开始 ↓↓↓↓↓↓");

            string jarsignerPath = PathUtils.GetPath(Constants.PATH_JARSIGNER);
            string args = jarsignerPath + " -digestalg SHA1 -sigalg SHA1withRSA -keystore " + config.KeystoreFilePath + " -storepass " + config.Password + " -keypass " + config.Aliaspwd + " " + tempApkFilePath + " " + config.Aliaskey;
            string[] ret = CmdUtils.ExecCmdAndWaitForExit(args, logView);
            if (ret == null || ret.Length < 2 || !string.IsNullOrEmpty(ret[1]))
            {
                logView.Log(">>>>>>重新签名失败: 执行CMD失败");
                return false;
            }
            logView.Log("↑↑↑↑↑↑ 重签名-结束 ↑↑↑↑↑↑");


            logView.BlankLine();
            logView.Log("↓↓↓↓↓↓ 对齐优化-开始 ↓↓↓↓↓↓");
            string zipalignPath = PathUtils.GetPath(Constants.PATH_ZIPALIGN);
            args = zipalignPath + " -f 4 " + tempApkFilePath + " " + newApkPath;

            ret = CmdUtils.ExecCmdAndWaitForExit(args, logView);
            if (ret == null || ret.Length < 2 || !string.IsNullOrEmpty(ret[1]))
            {
                logView.Log(">>>>>>对齐优化失败: 执行CMD失败");
                return false;
            }
            logView.Log("↑↑↑↑↑↑ 对齐优化-结束 ↑↑↑↑↑↑");
            logView.BlankLine();


            if (!File.Exists(newApkPath)) {
                logView.Log(">>>>>>重新签名失败: " + newApkPath + "导出的新apk文件不存在！");
                return false;
            }
            
            logView.Log("导出apk为: " + newApkPath);

            return true;
        }
    

    }
}
