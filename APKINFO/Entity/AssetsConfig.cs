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

namespace APKINFO.Entity
{
    /// <summary>
    /// 用户配置
    /// </summary>
    public class AssetsConfig
    {
        /// <summary>
        /// 指定读取APK中assets目录的配置文件，不能为空
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 指定读取该配置文件的配置参数(多个用逗号隔开)，为空表示读取全部参数
        /// </summary>
        public string Keys { get; set; }
    }
}
