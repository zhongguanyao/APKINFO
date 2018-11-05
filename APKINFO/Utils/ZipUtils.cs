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
using Ionic.Zip;
using APKINFO.Interface;

namespace APKINFO.Utils
{
    /// <summary>
    /// 解压文件操作
    /// </summary>
    public class ZipUtils
    {

        /// <summary>
        /// 解压指定的文件
        /// </summary>
        /// <param name="zipPath">压缩文件</param>
        /// <param name="outPath">解压指定目录</param>
        /// <param name="unZipFileName">解压文件里指定的文件路径</param>
        public static void UnZip(string zipPath, string outPath, string unZipFileName)
        {
            try
            {
                using (ZipFile zip = ZipFile.Read(zipPath))
                {
                    foreach (ZipEntry entry in zip.Entries)
                    {
                        if (String.Equals(entry.FileName, unZipFileName))
                        {
                            entry.Extract(outPath, ExtractExistingFileAction.OverwriteSilently);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteLine("UnZip Fail. Exception Occurred :" + ex.Message);
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
                Console.ReadKey();
            }
        }


        /// <summary>
        /// 删除APK文件中META-INF目录下的签名文件
        /// </summary>
        /// <param name="zipPath"></param>
        /// <param name="logView"></param>
        public static void RemoveApkCertFile(string zipPath, ILog logView)
        {
            try
            {
                using (ZipFile zip = ZipFile.Read(zipPath))
                {
                    List<ZipEntry> entries = new List<ZipEntry>();

                    foreach (ZipEntry entry in zip.Entries)
                    {
                        if (string.IsNullOrEmpty(entry.FileName))
                            continue;

                        if (!entry.FileName.StartsWith("META-INF/"))
                            continue;

                        if (entry.FileName.EndsWith(".MF")
                            || entry.FileName.EndsWith(".SF")
                            || entry.FileName.EndsWith(".RSA")
                            || entry.FileName.EndsWith(".DSA"))
                        {

                            entries.Add(entry);
                            logView.Log("删除->" + entry.FileName);
                        }
                    }

                    if (entries.Count > 0)
                    {
                        zip.RemoveEntries(entries);
                    }
                    zip.Save();
                }
            }
            catch (Exception ex)
            {
                logView.Log("RemoveApkCertFile Exception : " + ex.Message);
                LogUtils.WriteLine("RemoveApkCertFile Exception : " + ex.Message);
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
                Console.ReadKey();
            }
        }


        /// <summary>
        /// 解压APK文件中META-INF目录下的.RSA签名文件
        /// </summary>
        /// <param name="zipPath"></param>
        /// <param name="outPath"></param>
        /// <param name="logView"></param>
        public static void UnZipApkCertRSAFile(string zipPath, string outPath, ILog logView)
        {

            try
            {
                using (ZipFile zip = ZipFile.Read(zipPath))
                {
                    List<ZipEntry> entries = new List<ZipEntry>();

                    foreach (ZipEntry entry in zip.Entries)
                    {
                        if (string.IsNullOrEmpty(entry.FileName))
                            continue;

                        if (!entry.FileName.StartsWith("META-INF/"))
                            continue;

                        if (entry.FileName.EndsWith(".RSA"))
                        {
                            entry.Extract(outPath, ExtractExistingFileAction.OverwriteSilently);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logView.Log("UnZipApkCertRSAFile Exception : " + ex.Message);
                LogUtils.WriteLine("UnZipApkCertRSAFile Exception : " + ex.Message);
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
                Console.ReadKey();
            }
        }



        /// <summary>
        /// 整个文件解压
        /// </summary>
        /// <param name="zipPath"></param>
        /// <param name="outPath"></param>
        public static void UnZip(string zipPath, string outPath)
        {
            try
            {
                using (ZipFile zip = ZipFile.Read(zipPath))
                {
                    foreach (ZipEntry entry in zip.Entries)
                    {
                        entry.Extract(outPath, ExtractExistingFileAction.OverwriteSilently);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteLine("UnZip Exception : " + ex.Message);
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
                Console.ReadKey();
            }
        }

    }
}
