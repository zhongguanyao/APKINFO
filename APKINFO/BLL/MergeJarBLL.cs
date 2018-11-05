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
using System.IO;
using APKINFO.Utils;
using APKINFO.Entity;
using APKINFO.Interface;

namespace APKINFO.BLL
{
    /// <summary>
    /// 业务逻辑类-打入Jar包到APK
    /// </summary>
    public class MergeJarBLL
    {

        /// <summary>
        /// 反编译
        /// </summary>
        /// <param name="apkFilePath"></param>
        /// <param name="logView"></param>
        /// <returns></returns>
        public static string DecompileApk(string apkFilePath, ILog logView)
        {
            logView.Log("↓↓↓↓↓↓ 反编译-开始 ↓↓↓↓↓↓");

            string res = DecompileBLL.DecompileApk(apkFilePath);

            if (res == null)
            {
                logView.Log(">>>>>>反编译失败");
                return res;
            } else {
                logView.Log(">>>>>>反编译成功：" + res);
            }

            logView.Log("↑↑↑↑↑↑ 反编译-结束 ↑↑↑↑↑↑");
            logView.BlankLine();

            return res;

        }


        /// <summary>
        /// Jar文件转换成Dex文件
        /// </summary>
        /// <param name="jarPath">Jar文件或目录</param>
        /// <param name="logView"></param>
        /// <returns>返回生成dex文件的路径,为null时表示转换dex文件失败</returns>
        public static string Jar2Dex(string jarPath, ILog logView)
        {
            string javaPath = PathUtils.GetPath(Constants.PATH_JAVA);// java.exe
            string dexToolPath = PathUtils.GetPath(Constants.PATH_DX);// dx.jar
            if (!File.Exists(javaPath))
            {
                logView.Log(">>>>>>Jar文件转换成Dex文件失败: " + javaPath + "文件未生成！");
                return null;
            }
            if (!File.Exists(dexToolPath))
            {
                logView.Log(">>>>>>Jar文件转换成Dex文件失败: " + dexToolPath + "文件未生成！");
                return null;
            }


            string dexFilePath = PathUtils.GetPath(Constants.DIR_DEX);
            if (!Directory.Exists(dexFilePath)) {
                Directory.CreateDirectory(dexFilePath);
            }
            FileUtils.ClearDir(dexFilePath);
            // 生成的dex文件路径
            dexFilePath = PathUtils.GetPath(Constants.PATH_CLASSDEX);
   
            string args = javaPath + " -jar -Xms1024m -Xmx1024m " + dexToolPath + " --dex --output=" + dexFilePath;

            logView.Log("↓↓↓↓↓↓ Jar文件转换成Dex文件-开始 ↓↓↓↓↓↓");
            if (Directory.Exists(jarPath))
            {
                string[] files = Directory.GetFiles(jarPath);
                if (files != null && files.Length > 0)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (files[i].EndsWith(".jar"))
                        {
                            args += " " + files[i];
                        }
                    }
                }
            } else if (File.Exists(jarPath) && jarPath.EndsWith(".jar"))
            {
                args += " " + jarPath;
            }

            string[] ret = CmdUtils.ExecCmdAndWaitForExit(args, logView);
            if (ret == null || ret.Length < 2 || !string.IsNullOrEmpty(ret[1])) {
                logView.Log(">>>>>>Jar文件转换成Dex文件失败: 执行CMD失败" );
                return null;
            }

            logView.Log("↑↑↑↑↑↑ Jar文件转换成Dex文件-结束 ↑↑↑↑↑↑");
            logView.BlankLine();

            if (File.Exists(dexFilePath))
            {
                logView.Log("已生成dex文件：" + dexFilePath);
            } else {
                logView.Log("没生成dex文件：" + dexFilePath);
            }
            logView.BlankLine();

            return dexFilePath;
        }



        /// <summary>
        /// 将Dex文件转换为Smali文件
        /// </summary>
        /// <param name="decompileDir"></param>
        /// <param name="logView"></param>
        /// <returns></returns>
        public static bool Dex2Smali(string decompileDir, ILog logView)
        {
            if (string.IsNullOrEmpty(decompileDir) || !Directory.Exists(decompileDir))
            {
                logView.Log(">>>>>>将Dex文件转换为Smali文件失败: " + decompileDir + "目录不存在！");
                return false;
            }

            string dexFilePath = PathUtils.GetPath(Constants.PATH_CLASSDEX);
            if (!File.Exists(dexFilePath))
            {
                logView.Log(">>>>>>将Dex文件转换为Smali文件失败: " + dexFilePath + "文件未生成！");
                return false;
            }

            string javaPath = PathUtils.GetPath(Constants.PATH_JAVA);// java.exe
            string smaliTool = PathUtils.GetPath(Constants.PATH_BAKSMALI);// baksmali.jar
            if (!File.Exists(javaPath))
            {
                logView.Log(">>>>>>将Dex文件转换为Smali文件失败: " + javaPath + "不存在！");
                return false;
            }
            if (!File.Exists(smaliTool))
            {
                logView.Log(">>>>>>将Dex文件转换为Smali文件失败: " + smaliTool + "不存在！");
                return false;
            }


            // 生成smali位置,是Apk反编译目录下的smali目录,用于覆盖apk的smali文件
            string targetDir = PathUtils.JoinPath(decompileDir, Constants.DIR_SMALI);

            logView.Log("↓↓↓↓↓↓ 将Dex文件转换为Smali文件-开始 ↓↓↓↓↓↓");

            string args = javaPath + " -jar " + smaliTool + " -o " + targetDir + " " + dexFilePath;
            string[] ret = CmdUtils.ExecCmdAndWaitForExit(args, logView);
            if (ret == null || ret.Length < 2 || !string.IsNullOrEmpty(ret[1]))
            {
                logView.Log(">>>>>>将Dex文件转换为Smali文件失败: 执行CMD失败");
                return false;
            }

            logView.Log("↑↑↑↑↑↑ 将Dex文件转换为Smali文件-结束 ↑↑↑↑↑↑");
            logView.BlankLine();

            return true;
        }



        /// <summary>
        /// 回编译
        /// </summary>
        /// <param name="decompileDir"></param>
        /// <param name="logView"></param>
        /// <returns></returns>
        public static bool RecompileApk(string decompileDir, ILog logView)
        {
            if (string.IsNullOrEmpty(decompileDir) || !Directory.Exists(decompileDir))
            {
                logView.Log(">>>>>>回编译失败: " + decompileDir + "目录不存在！");
                return false;
            }

            string apktoolPath = PathUtils.GetPath(Constants.PATH_APKTOOL);
            if (!File.Exists(apktoolPath))
            {
                LogUtils.WriteLine(">>>>>>回编译失败: " + apktoolPath + "不存在！");
                return false;
            }

            string tempApkPath = PathUtils.GetPath(Constants.PATH_TEMPAPK);
            if (File.Exists(tempApkPath))
            {
                File.Delete(tempApkPath);
            }

            logView.Log("↓↓↓↓↓↓ 回编译-开始 ↓↓↓↓↓↓");

            string args = apktoolPath + " b " + decompileDir + " -o " + tempApkPath;
            string[] ret = CmdUtils.ExecCmdAndWaitForExit(args, logView);
            if (ret == null || ret.Length < 2 || !string.IsNullOrEmpty(ret[1]))
            {
                logView.Log(">>>>>>回编译失败: 执行CMD失败");
                return false;
            }

            logView.Log("↑↑↑↑↑↑ 回编译-结束 ↑↑↑↑↑↑");
            logView.BlankLine();

            return true;
        }


        /// <summary>
        /// 签名
        /// </summary>
        /// <param name="config"></param>
        /// <param name="logView"></param>
        /// <returns></returns>
        public static bool SignApk(KeystoreConfig config, ILog logView)
        {
            string tempApkPath = PathUtils.GetPath(Constants.PATH_TEMPAPK);
            if (!File.Exists(tempApkPath))
            {
                logView.Log(">>>>>>签名失败: " + tempApkPath + "未生成！");
                return false;
            }

            string jarsignerPath = PathUtils.GetPath(Constants.PATH_JARSIGNER);
            if (!File.Exists(jarsignerPath))
            {
                logView.Log(">>>>>>签名失败: " + jarsignerPath + "不存在！");
                return false;
            }

            logView.Log("↓↓↓↓↓↓ 签名-开始 ↓↓↓↓↓↓");

            string args = jarsignerPath + " -digestalg SHA1 -sigalg SHA1withRSA -keystore " + config.KeystoreFilePath + " -storepass " + config.Password + " -keypass " + config.Aliaspwd + " " + tempApkPath + " " + config.Aliaskey;
            string[] ret = CmdUtils.ExecCmdAndWaitForExit(args, logView);
            if (ret == null || ret.Length < 2 || !string.IsNullOrEmpty(ret[1]))
            {
                logView.Log(">>>>>>签名失败: 执行CMD失败");
                return false;
            }

            logView.Log("↑↑↑↑↑↑ 签名-结束 ↑↑↑↑↑↑");
            logView.BlankLine();

            return true;
        }


        /// <summary>
        /// 对齐优化
        /// </summary>
        /// <param name="newApkPath"></param>
        /// <param name="logView"></param>
        /// <returns></returns>
        public static bool AlignApk(string newApkPath, ILog logView)
        {
            string tempApkPath = PathUtils.GetPath(Constants.PATH_TEMPAPK);
            if (!File.Exists(tempApkPath))
            {
                logView.Log(">>>>>>对齐优化失败: " + tempApkPath + "未生成！");
                return false;
            }

            string zipalignPath = PathUtils.GetPath(Constants.PATH_ZIPALIGN);
            if (!File.Exists(zipalignPath))
            {
                logView.Log(">>>>>>对齐优化失败: " + zipalignPath + "不存在！");
                return false;
            }

            logView.Log("↓↓↓↓↓↓ 对齐优化-开始 ↓↓↓↓↓↓");

            string args = zipalignPath + " -f 4 " + tempApkPath + " " + newApkPath;
            string[] ret = CmdUtils.ExecCmdAndWaitForExit(args, logView);
            if (ret == null || ret.Length < 2 || !string.IsNullOrEmpty(ret[1]))
            {
                logView.Log(">>>>>>对齐优化失败: 执行CMD失败");
                return false;
            }

            logView.Log("↑↑↑↑↑↑ 对齐优化-结束 ↑↑↑↑↑↑");
            logView.BlankLine();
            logView.Log("导出apk为: " + newApkPath);
            logView.BlankLine();

            return true;
        }


     

        
    }
}
