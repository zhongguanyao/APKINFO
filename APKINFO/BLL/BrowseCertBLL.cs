/***********************************************
 * 作者: ZhongGuanYao
 * 邮箱: 598115778@qq.com
 * 博客: https://blog.csdn.net/xiangxinzijiwonen
 *       https://github.com/zhongguanyao
 * 日期: 2018-11-01
 * **********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using APKINFO.Utils;
using APKINFO.Entity;
using System.IO;
using APKINFO.Interface;
using System.Diagnostics;

namespace APKINFO.BLL
{
    /// <summary>
    /// 业务逻辑类-查看签名
    /// </summary>
    public class BrowseCertBLL
    {

        /// <summary>
        /// 查看签名信息
        /// </summary>
        /// <param name="apkFilePath"></param>
        /// <param name="logView"></param>
        /// <returns></returns>
        public static string BrowseCert(string apkFilePath, ILog logView) {

            if (string.IsNullOrEmpty(apkFilePath) || !File.Exists(apkFilePath)) {
                logView.Log("Apk文件不存在!");
                return null;
            }

            // 解压文件存放的临时目录
            string tempDir = PathUtils.GetPath(Constants.DIR_CERT);
            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }
            FileUtils.ClearDir(tempDir);

            // 解压APK文件中META-INF目录下的.RSA签名文件
            ZipUtils.UnZipApkCertRSAFile(apkFilePath, tempDir, logView);

            tempDir = PathUtils.JoinPath(tempDir, "META-INF");
            if (!Directory.Exists(tempDir)) {
                logView.Log(">>>>>>查看签名信息失败: 解压APK文件中META-INF目录下的.RSA签名文件失败,META-INF目录未解压出来!");
                return null;
            }
                
            string[] files = Directory.GetFiles(tempDir);
            if (files == null || files.Length != 1 || !".RSA".Equals(Path.GetExtension(files[0])))
            {
                logView.Log(">>>>>>查看签名信息失败: 解压APK文件中META-INF目录下的.RSA签名文件失败!");
                return null;
            }

            string keyToolPath = PathUtils.GetPath(Constants.PATH_KEYTOOL);
            if (!File.Exists(keyToolPath)) {
                logView.Log(">>>>>>重新签名失败: " + keyToolPath + "不存在！");
                return null;
            }

            var startInfo = new ProcessStartInfo(keyToolPath);
            string args = "-printcert -file " + files[0];
            startInfo.Arguments = args;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;

            StringBuilder sb = new StringBuilder();
            using (var process = Process.Start(startInfo))
            {
                var sr = process.StandardOutput;
                while (!sr.EndOfStream)
                {
                    sb.AppendLine(sr.ReadLine());
                }
                process.WaitForExit();
            }

            return sb.ToString();

        }


    }
}
