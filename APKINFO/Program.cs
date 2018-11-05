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
using System.Windows.Forms;
using System.IO;
using System.Threading;
using APKINFO;
using APKINFO.Utils;
using APKINFO.BLL;
using APKINFO.Entity;
using APKINFO.UI;

namespace APKINFO
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string filePath;

            // 读取文件路径
            if (args != null && args.Length > 0)
            {
                filePath = args[0];
            } else
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "打开（.apk|.jar|.dex）文件";
                ofd.Filter = "apk jar dex|*.apk;*.jar;*.dex";
                DialogResult res = ofd.ShowDialog();
                if (String.IsNullOrEmpty(ofd.FileName)) return;

                filePath = ofd.FileName;
            }

            // 处理文件
            if (!File.Exists(filePath))
            {
                MessageBox.Show("文件不存在：" + filePath);
                return;
            }

            string vExtension = Path.GetExtension(filePath);
            if (!".jar".Equals(vExtension)
                && !".dex".Equals(vExtension)
                && !".apk".Equals(vExtension)
                )
            {
                MessageBox.Show("文件格式错误（正确格式：.jar、.dex、.apk）");
                return;
            }


            if (".jar".Equals(vExtension))
            {
                // 查看源码
                BrowseSourceBLL.OpenJadxGUI(filePath);
                return;
            }

            if (".dex".Equals(vExtension))
            {
                // 查看源码
                BrowseSourceBLL.OpenJadxGUI(filePath);
                return;
            }


            if (".apk".Equals(vExtension))
            {
                // 查看APK信息
                ApkInfo apkInfo = BrowseApkInfoBLL.ReadApkInfo(filePath);
                ApkInfoForm apkInfoForm = new ApkInfoForm(apkInfo);
                apkInfoForm.ShowDialog();
            }

            //Application.Run(new Form1());
        }
    }
}
