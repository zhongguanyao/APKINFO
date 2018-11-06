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
using System.Windows.Forms;

namespace APKINFO.Entity
{
    /// <summary>
    /// 常量类
    /// </summary>
    public class Constants
    {
        public readonly static string PATH_ROOT = Application.StartupPath;
        public const string PATH_JADXGUI = "jadx-0.7.1\\bin\\jadx-gui.bat";
        public const string PATH_AAPT = "win\\aapt.exe";
        public const string PATH_JARSIGNER = "win\\jre\\bin\\jarsigner.exe";
        public const string PATH_ZIPALIGN = "win\\zipalign.exe";
        public const string PATH_APKTOOL = "win\\apktool.bat";
        public const string PATH_JAVA = "win\\jre\\bin\\java.exe";
        public const string PATH_DX = "win\\lib\\dx.jar";
        public const string PATH_CLASSDEX = "temp\\dex\\classes.dex";
        public const string PATH_BAKSMALI = "win\\baksmali.jar";
        public const string PATH_TEMPAPK = "temp\\temp.apk";
        public const string PATH_KEYTOOL = "win\\jre\\bin\\keytool.exe";


        public const string DIR_CONFIG = "config";
        public const string DIR_TEMP_ICON = "temp\\icon";
        public const string DIR_TEMP_ASSETS = "temp\\assets";
        public const string DIR_TEMP_RESIGNAPK = "temp\\resignApk";
        public const string DIR_TEMP_DECOMPLIE = "temp\\decompile";
        public const string DIR_SMALI = "smali";
        public const string DIR_DEX = "temp\\dex";
        public const string DIR_CERT = "temp\\cert";

        public const string FILENAME_USER_CONFIG = "userconfig.xml";
        public const string FILENAME_KEYSTORE = "keystore.xml";


        /// <summary>
        /// 查看签名类型——APK文件
        /// </summary>
        public const int TYPE_APK_FILE = 0;
        /// <summary>
        /// 查看签名类型——签名库文件
        /// </summary>
        public const int TYPE_CERT_FILE= 1;
    }
}
