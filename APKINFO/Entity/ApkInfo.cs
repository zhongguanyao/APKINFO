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
using System.Drawing;

namespace APKINFO.Entity
{
    /// <summary>
    /// APK信息
    /// </summary>
    public class ApkInfo
    {
        public string ApkFilePath { get; set; }

        public string AppName { get; set; }

        public string VersionCode { get; set; }

        public string VersionName { get; set; }

        public string PackageName { get; set; }

        public string MinSdkVersion { get; set; }

        public string MaxSdkVersion { get; set; }

        public string TargetSdkVersion { get; set; }

        public string AssetsConfig { get; set; }

        public string Densities { get; set; }

        public string SupportsScreens { get; set; }

        public string UsesPermission { get; set; }

        public string UsesFeature { get; set; }

        /// <summary>
        /// APK文件名
        /// </summary>
        public string CurrentFileName { get; set; }

        /// <summary>
        /// icon文件集合
        /// </summary>
        public Dictionary<string, string> IconDic { get; set; }
        /// <summary>
        /// icon文件名
        /// </summary>
        public string IconName { get; set; }
        /// <summary>
        /// icon对象
        /// </summary>
        public Bitmap Icon { get; set; }
        

        /// <summary>
        /// 未解析的全部信息
        /// </summary>
        public List<string> AllInfoList { get; set; }


    }
}
