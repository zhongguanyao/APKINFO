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
using APKINFO.Utils;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;

namespace APKINFO.BLL
{
    /// <summary>
    /// 业务逻辑类-查看APK信息
    /// </summary>
    public class BrowseApkInfoBLL
    {

        #region 读取APK信息

        /// <summary>
        /// 读取APK信息
        /// </summary>
        /// <param name="apkFilePath"></param>
        /// <returns></returns>
        public static ApkInfo ReadApkInfo(string apkFilePath)
        {
            if (String.IsNullOrEmpty(apkFilePath))
                return null;

            string aaptPath = PathUtils.GetPath(Constants.PATH_AAPT);
            var startInfo = new ProcessStartInfo(aaptPath);
            string args = string.Format("dump badging \"{0}\"", apkFilePath);
            startInfo.Arguments = args;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;
            startInfo.StandardOutputEncoding = Encoding.UTF8;
            
            // 读取命令行返回的所有txt文本和字符编码
            List<string> lineLst = new List<string>();
            using (var process = Process.Start(startInfo))
            {
                var sr = process.StandardOutput;
                while (!sr.EndOfStream)
                {
                    lineLst.Add(sr.ReadLine());
                }
                process.WaitForExit();
            }


            ApkInfo apkInfo = new ApkInfo();
            apkInfo.ApkFilePath = apkFilePath;
            apkInfo.CurrentFileName = Path.GetFileName(apkFilePath);
            apkInfo.AllInfoList = lineLst;

            // 解析相关的配置参数
            ParseTxtFile(ref apkInfo);
            ParseIcon(ref apkInfo);
            ParseAssetsConfig(ref apkInfo);

            return apkInfo;
        }


        /// <summary>
        /// 解析TXT文件
        /// </summary>
        /// <param name="apkInfo"></param>
        private static void ParseTxtFile(ref ApkInfo apkInfo)
        {
            if (apkInfo == null) return;

            string s;

            // 遍历解析各行
            foreach (string line in apkInfo.AllInfoList)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                // 游戏名称
                if (line.StartsWith("application-label:"))
                {
                    string[] ss = line.Split(new char[] { ':' });
                    if (ss == null || ss.Length <= 1) continue;
                    apkInfo.AppName = ss[1].Trim(new char[] { '\'' });
                    continue;
                }

                //package: name='com.test.sdk' versionCode='1' versionName='1.0.0' platformBuildVersionName='6.0-2438415'
                if (line.Contains("package: name=") && line.Contains("versionCode=") && line.Contains("versionName="))
                {
                    //string[] ss = info.Split(new char[]{' '});
                    string[] ss = Regex.Split(line, "\\s+", RegexOptions.IgnoreCase);
                    if (ss == null || ss.Length < 4) continue;

                    // 包名
                    string[] pp = ss[1].Split(new char[] { '=' });
                    if (pp == null || pp.Length < 2) continue;
                    apkInfo.PackageName = pp[1].Trim(new char[] { '\'' });

                    // 版本号
                    pp = ss[2].Split(new char[] { '=' });
                    if (pp == null || pp.Length < 2) continue;
                    apkInfo.VersionCode = pp[1].Trim(new char[] { '\'' });

                    // 版本名称
                    pp = ss[3].Split(new char[] { '=' });
                    if (pp == null || pp.Length < 2) continue;
                    apkInfo.VersionName = pp[1].Trim(new char[] { '\'' });

                    continue;
                }

                // 最小SDK版本号
                if (line.StartsWith("sdkVersion:"))
                {
                    string[] ss = line.Split(new char[] { ':' });
                    if (ss == null || ss.Length <= 1) continue;
                    apkInfo.MinSdkVersion = ss[1].Trim(new char[] { '\'' });
                    continue;
                }

                // 最大SDK版本号
                if (line.StartsWith("maxSdkVersion:"))
                {
                    string[] ss = line.Split(new char[] { ':' });
                    if (ss == null || ss.Length <= 1) continue;
                    apkInfo.MaxSdkVersion = ss[1].Trim(new char[] { '\'' });
                    continue;
                }

                // 目标版本号
                if (line.StartsWith("targetSdkVersion:"))
                {
                    string[] ss = line.Split(new char[] { ':' });
                    if (ss == null || ss.Length <= 1) continue;
                    apkInfo.TargetSdkVersion = ss[1].Trim(new char[] { '\'' });
                    continue;
                }


                if (line.StartsWith("densities:"))
                {
                    string[] ss = line.Split(new char[] { ':' });
                    if (ss == null || ss.Length <= 1) continue;
                    apkInfo.Densities = ss[1].Replace('\'', ' ').Trim();
                    continue;
                }

                if (line.StartsWith("supports-screens:"))
                {
                    string[] ss = line.Split(new char[] { ':' });
                    if (ss == null || ss.Length <= 1) continue;
                    apkInfo.SupportsScreens = ss[1].Replace('\'', ' ').Trim();
                    continue;
                }

                if (line.StartsWith("uses-permission:"))
                {
                    string[] ss = line.Split(new char[] { ':' });
                    if (ss == null || ss.Length <= 1) continue;
                    if (!string.IsNullOrEmpty(apkInfo.UsesPermission))
                    {
                        apkInfo.UsesPermission += "\r\n";
                    }
                    apkInfo.UsesPermission += ss[1].Replace('\'', ' ');
                    continue;
                }


                if (line.StartsWith("uses-feature:"))
                {
                    string[] ss = line.Split(new char[] { ':' });
                    if (ss == null || ss.Length <= 1) continue;
                    if (!string.IsNullOrEmpty(apkInfo.UsesFeature))
                    {
                        apkInfo.UsesFeature += "\r\n";
                    } 
                    apkInfo.UsesFeature += ss[1].Replace('\'', ' ');
                    continue;
                }

                // icon文件集合
                //application-icon-120:'res/drawable-ldpi-v4/ic_launcher.png'
                //application-icon-160:'res/drawable-mdpi-v4/ic_launcher.png'
                //application-icon-240:'res/drawable-hdpi-v4/ic_launcher.png'
                //application-icon-320:'res/drawable-xhdpi-v4/ic_launcher.png'
                //application-icon-480:'res/drawable-xxhdpi/ic_launcher.png'
                //application-icon-640:'res/drawable-xxxhdpi/ic_launcher.png'
                if (line.StartsWith("application-icon"))
                {
                    if (apkInfo.IconDic == null)
                        apkInfo.IconDic = new Dictionary<string, string>();

                    string[] ss = line.Split(new char[] { ':' });
                    if (ss == null || ss.Length <= 1) continue;
                    if (!apkInfo.IconDic.ContainsKey(ss[0]))
                    {
                        apkInfo.IconDic.Add(ss[0], ss[1].Trim(new char[] { '\'' }));
                    }
                    continue;
                }

            }
        }


        /// <summary>
        /// 解压并读取icon图片
        /// </summary>
        /// <param name="apkInfo"></param>
        private static void ParseIcon(ref ApkInfo apkInfo)
        {
            if (apkInfo == null 
                || string.IsNullOrEmpty(apkInfo.ApkFilePath)
                || apkInfo.IconDic == null 
                || apkInfo.IconDic.Count <= 0) return;

            string icon = null;
            int i = 0;
            // 在多张尺寸的ICON里选择尺寸为240
            foreach (KeyValuePair<string, string> kv in apkInfo.IconDic)
            {
                if (kv.Key.Contains("240"))
                {
                    icon = kv.Value;
                    apkInfo.IconName = Path.GetFileName(icon);
                    break;
                }
                if (i == apkInfo.IconDic.Count - 1)
                {
                    icon = kv.Value;
                    apkInfo.IconName = Path.GetFileName(icon);
                    break;
                }
                i++;
            }

            // 指定解压目录
            string unZipPath = PathUtils.GetPath(Constants.DIR_TEMP_ICON);
            // 清空解压目录
            FileUtils.ClearDir(unZipPath);

            // 解压指定的icon图片文件
            ZipUtils.UnZip(apkInfo.ApkFilePath, unZipPath, icon);

            // 读取解压出来的图片文件
            string iconPath = PathUtils.JoinPath(unZipPath, icon);

            apkInfo.Icon = new Bitmap(iconPath);
        }

        /// <summary>
        /// 根据用户配置读取assets目录下的文件参数
        /// </summary>
        /// <param name="apkInfo"></param>
        private static void ParseAssetsConfig(ref ApkInfo apkInfo)
        {
            if (apkInfo == null || string.IsNullOrEmpty(apkInfo.ApkFilePath)) return;

            List<AssetsConfig> configList = ConfigUtils.ReadUserConfig();

            if (configList == null || configList.Count <= 0) return;

            StringBuilder sb = new StringBuilder();

            // 根据用户配置，遍历解压指定文件，并根据配置读取指定参数
            foreach (AssetsConfig config in configList)
            {
                if (string.IsNullOrEmpty(config.FileName)) continue;

                // 指定解压目录
                string unZipPath = PathUtils.GetPath(Constants.DIR_TEMP_ASSETS);
                // 清空解压目录
                FileUtils.ClearDir(unZipPath);

                // 解压指定的文件
                ZipUtils.UnZip(apkInfo.ApkFilePath, unZipPath, "assets/" + config.FileName);
                // 读取解压出来的文件
                string configFilePath = PathUtils.JoinPath(unZipPath, "assets/" + config.FileName);

                if (!File.Exists(configFilePath) || !FileUtils.HasContent(configFilePath))
                {
                    sb.AppendLine(config.FileName + "文件读取失败");
                    continue;
                }


                // 以下是读取配置文件中指定的参数

                // 用户没有配置读取指定参数时，表示读取整个配置文件
                List<string> keyList = CommonUtils.ToList(config.Keys);
                if (keyList == null)
                {
                    sb.AppendLine(File.ReadAllText(configFilePath));
                    continue;
                }

                // 用户配置读取指定参数
                Dictionary<string, bool> keyDic = new Dictionary<string, bool>();
                foreach (string key in keyList)
                {
                    if (keyDic.ContainsKey(key)) continue;
                    keyDic.Add(key, true);// 将参数放置到字典里，true表示未找到参数，false表示未找到参数
                }

                string[] lines = File.ReadAllLines(configFilePath);
                foreach (string line in lines)
                {
                    if (string.IsNullOrEmpty(line))
                        continue;

                    string[] ss = line.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    if (ss != null && ss.Length > 0 && keyDic.ContainsKey(ss[0]))
                    {
                        sb.AppendLine(line);
                        keyDic[ss[0]] = false;// 已找到参数
                    }
                }

                // 如果参数未找到则提示
                foreach (KeyValuePair<string, bool> kv in keyDic)
                {
                    if (kv.Value)
                    {
                        sb.AppendLine(kv.Key + "参数未找到");
                    }
                }

            }

            apkInfo.AssetsConfig = sb.ToString();
        }


        #endregion





        /// <summary>
        /// 导出TXT文件
        /// </summary>
        /// <param name="txtPath"></param>
        /// <param name="infoList"></param>
        public static void ExportTxtFile(string txtPath, List<string> infoList)
        {

            try
            {
                FileStream stream = System.IO.File.Create(txtPath);

                if (infoList != null && infoList.Count > 0)
                {
                    using (StreamWriter sw = new StreamWriter(stream))
                    {
                        foreach (string line in infoList)
                        {
                            sw.WriteLine(line);
                        }
                        sw.Flush();
                    }
                }

                stream.Close();
                stream.Dispose();
            }
            catch (Exception ex)
            {
                LogUtils.WriteLine("ExportTxtFile Exception: " + ex.Message);
            }
        }
    }
}
