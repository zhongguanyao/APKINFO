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

namespace APKINFO.Entity
{
    /// <summary>
    /// 签名配置
    /// </summary>
    public class KeystoreConfig
    {
        public string KeystoreFilePath { get; set; }
        public string Password { get; set; }
        public string Aliaskey { get; set; }
        public string Aliaspwd { get; set; }
    }
}
