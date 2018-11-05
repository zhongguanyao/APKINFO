using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace APKINFO
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public class FileUtils
    {

        /// <summary>
        /// 打开目录
        /// </summary>
        /// <param name="dirPath"></param>
        public static void OpenDir(string dirPath)
        {
            if (string.IsNullOrEmpty(dirPath))
                return;

            if (Directory.Exists(dirPath) == false)
            {
                Directory.CreateDirectory(dirPath);
            }
            System.Diagnostics.Process.Start(dirPath);
        }

        /// <summary>
        /// 打开文件位置
        /// </summary>
        /// <param name="filePath"></param>
        public static bool OpenFile(string filePath)
        {

            if (!File.Exists(filePath))
            {
                return false;
            }
            string args = string.Format("/Select, {0}", filePath);

            ProcessStartInfo pfi = new ProcessStartInfo("Explorer.exe", args);
            System.Diagnostics.Process.Start(pfi);
            return true;

        }


        /// <summary>
        /// 清空指定目录
        /// </summary>
        /// <param name="dirPath"></param>
        public static void ClearDir(string dirPath)
        {
            if (Directory.Exists(dirPath))
            {
                foreach (string content in Directory.GetFileSystemEntries(dirPath))
                {
                    if (Directory.Exists(content))
                    {
                        Directory.Delete(content, true);
                    } else if (File.Exists(content))
                    {
                        File.Delete(content);
                    }
                }
            }
        }

        /// <summary>
        /// 判断TXT文件是否有数据可读
        /// </summary>
        /// <param name="txtFilePath"></param>
        /// <returns></returns>
        public static bool HasContent(string txtFilePath)
        {
            string[] lines = File.ReadAllLines(txtFilePath);
            if (lines == null || lines.Length <= 0) return false;
            return true;
        }






        /// <summary>
        /// 拷贝整个目录（包括目录下的子孙目录和子孙文件）
        /// </summary>
        /// <param name="sourceDirPath"></param>
        /// <param name="saveDirPath"></param>
        public static void CopyDirectory(string sourceDirPath, string saveDirPath)
        {
            if (!Directory.Exists(sourceDirPath)) return;

            if (!Directory.Exists(saveDirPath))
            {
                Directory.CreateDirectory(saveDirPath);
            }
            string[] files = Directory.GetFiles(sourceDirPath);
            foreach (string file in files)
            {
                string pFilePath = saveDirPath + "\\" + Path.GetFileName(file);
                File.Copy(file, pFilePath, true);
            }

            string[] dirs = Directory.GetDirectories(sourceDirPath);
            foreach (string dir in dirs)
            {
                CopyDirectory(dir, saveDirPath + "\\" + Path.GetFileName(dir));
            }
        }

        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <param name="oldFilePath"></param>
        /// <param name="newFilePath"></param>
        public static void RenameFile(string oldFilePath, string newFilePath)
        {
            if (string.IsNullOrEmpty(oldFilePath)
                || !File.Exists(oldFilePath)
                || string.IsNullOrEmpty(newFilePath))
                return;

            new FileInfo(oldFilePath).MoveTo(newFilePath);
        }



        /// <summary>
        /// 找出目录下所有.smali文件
        /// </summary>
        /// <param name="sourceDirPath"></param>
        /// <param name="smaliFiles"></param>
        public static void ListSmaliFiles(string sourceDirPath, ref List<string> smaliFiles)
        {
            if (Directory.Exists(sourceDirPath))
            {

                string[] files = Directory.GetFiles(sourceDirPath);
                if (files != null && files.Length > 0)
                {
                    foreach (string file in files)
                    {
                        if (file.EndsWith(".smali"))
                        {
                            smaliFiles.Add(file);
                        }
                    }
                }

                string[] dirs = Directory.GetDirectories(sourceDirPath);
                if (dirs != null && dirs.Length > 0)
                {
                    foreach (string dir in dirs)
                    {
                        ListSmaliFiles(dir, ref smaliFiles);
                    }
                }
            }
        }


        /// <summary>
        /// 读取smali文件下的方法数量
        /// </summary>
        /// <param name="smaliFilePath">smali文件路径</param>
        /// <param name="methodNames">出现过的方法名称集合</param>
        /// <returns></returns>
        public static int GetSmaliFuncCount(string smaliFilePath, ref List<string> methodNames)
        {
            if (string.IsNullOrEmpty(smaliFilePath) || !File.Exists(smaliFilePath)) return 0;

            string[] lines = File.ReadAllLines(smaliFilePath);
            if (lines == null || lines.Length <= 0 || !lines[0].StartsWith(".class"))
                return 0;

            string[] ss = lines[0].Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
            string className = ss[ss.Length - 1];

            int cnt = 0;
            string methodName;
            for (int i = 0; i < lines.Length; i++)
            {
                methodName = null;

                if (lines[i].StartsWith(".method"))
                {
                    ss = lines[i].Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
                    methodName = className + "->" + ss[ss.Length - 1];
                } else if (lines[i].Trim().StartsWith("invoke-"))
                {
                    ss = lines[i].Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
                    methodName = ss[ss.Length - 1];
                }

                if (methodName != null && !methodNames.Contains(methodName))
                {
                    methodNames.Add(methodName);
                    cnt++;
                }
            }

            return cnt;
        }


        /// <summary>
        /// 替换指定文件内容的字符串
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="oldVal"></param>
        /// <param name="newVal"></param>
        public static void ReplaceContent(string filePath, string oldVal, string newVal)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return;
            if (String.Equals(oldVal, newVal)) return;

            string content = File.ReadAllText(filePath);
            content = content.Replace(oldVal, newVal);
            File.WriteAllText(filePath, content);
        }
    }
}
