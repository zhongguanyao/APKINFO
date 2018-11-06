/***********************************************
 * 作者: ZhongGuanYao
 * 邮箱: 598115778@qq.com
 * 博客: https://blog.csdn.net/xiangxinzijiwonen
 *       https://github.com/zhongguanyao
 * 日期: 2018-11-01
 * **********************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using APKINFO.Entity;
using System.Threading;
using APKINFO.Utils;
using APKINFO.BLL;
using APKINFO.Interface;

namespace APKINFO.UI
{
    /// <summary>
    /// 查看签名
    /// </summary>
    public partial class BrowseCertForm : Form, ILog
    {

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="filePath">.apk或.keystore或.jks文件路径</param>
        /// <param name="type">类型（Constant常量）</param>
        public BrowseCertForm(string filePath, int type)
        {
            InitializeComponent();

            mFilePath = filePath;
            mType = type;

            if (mType == Constants.TYPE_APK_FILE) {
                this.Text = "查看APK签名信息";
                string msg = BrowseCertBLL.BrowseCert(mFilePath, this);
                Log(msg);

            } else if (mType == Constants.TYPE_CERT_FILE) {
                this.Text = "查看签名库信息";
            }
        }

        


        private string mFilePath;
        private int mType;

        public delegate void LogDelegate(string msg);





        #region 实现ILog接口
        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="msg"></param>
        public void Log(string msg)
        {
            if (txtLog.InvokeRequired)
            {
                LogDelegate ld = new LogDelegate(Log);
                txtLog.Invoke(ld, msg);
            } else
            {
                txtLog.Text += "\r\n" + msg;

                txtLog.Focus();//获取焦点
                txtLog.Select(txtLog.TextLength, 0);//光标定位到文本最后
                txtLog.ScrollToCaret();//滚动到光标处
            }
        }
        /// <summary>
        /// 空行
        /// </summary>
        public void BlankLine()
        {
            Log(null);
        }

        #endregion

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
            this.Dispose();
        }



        private void BrowseCertForm_Shown(object sender, EventArgs e)
        {
            if (mType == Constants.TYPE_CERT_FILE)
            {
                Console.WriteLine("文件路径 " + mFilePath);
                EnterPwdForm form = new EnterPwdForm();
                DialogResult dr = form.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    Console.WriteLine("密码 " + form.Pwd);
                    string msg = BrowseCertBLL.BrowseCert(mFilePath, form.Pwd, this);
                    Console.WriteLine("msg " + msg);
                    Log(msg);

                } else
                {
                    this.Dispose();
                    this.Close();
                }
            }
        }
   
    }
}
