/***********************************************
 * 作者: ZhongGuanYao
 * 邮箱: 598115778@qq.com
 * 博客: https://blog.csdn.net/xiangxinzijiwonen
 *       https://github.com/zhongguanyao
 * 日期: 2018-10-29
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
    /// 重签名窗体
    /// </summary>
    public partial class ResignApkForm : Form, ILog
    {
        public ResignApkForm()
        {
            InitializeComponent();

            // 读取上次保存的签名信息
            KeystoreConfig config = ConfigUtils.ReadKeystoreConfig();
            if (config != null)
            {
                txtKeystoreFile.Text = config.KeystoreFilePath;
                txtPassword.Text = config.Password;
                txtAliaskey.Text = config.Aliaskey;
                txtAliaspwd.Text = config.Aliaspwd;
            }
        }

        public delegate void UpdateUIDelegate();
        public delegate void LogDelegate(string msg);

        /// <summary>
        /// 获取或设置APK文件路径
        /// </summary>
        public string ApkFilePath
        {
            get { return txtApkFilePath.Text.Trim(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    txtApkFilePath.Text = "";
                    txtNewFileDir.Text = "";
                    txtApkFilePath.Focus();
                }
                else {
                    if (!value.EndsWith(".apk"))
                    {
                        ApkFilePath = "";
                        txtNewFileDir.Text = "";
                        Log("\r\n请输入.apk文件路径!!!!!\r\n");
                        txtApkFilePath.Focus();
                    }
                    else
                    {
                        txtApkFilePath.Text = value;
                        string fileName = Path.GetFileName(value);
                        txtNewFileName.Text = fileName.Substring(0, fileName.Length - 4) + "_ResignApk_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".apk";
                        txtNewFileDir.Text = Path.GetDirectoryName(value);
                    }
                }
            }
        }



        /// <summary>
        /// 检查表单
        /// </summary>
        /// <returns></returns>
        private bool VerifyForm()
        {
            if (string.IsNullOrEmpty(ApkFilePath))
            {
                Log("\r\n母包APK文件为空!!!!!\r\n");
                txtApkFilePath.Focus();
                return false;
            }
            if (!File.Exists(ApkFilePath))
            {
                Log("\r\n母包APK文件不存在!!!!!\r\n");
                txtApkFilePath.Focus();
                return false;
            }


            if (string.IsNullOrEmpty(txtNewFileName.Text.Trim())) {
                Log("\r\n导出文件名称不能为空!!!!!\r\n");
                txtNewFileName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtNewFileDir.Text.Trim()))
            {
                Log("\r\n导出目录位置不能为空!!!!!\r\n");
                txtNewFileDir.Focus();
                return false;
            }


            if (string.IsNullOrEmpty(txtKeystoreFile.Text.Trim()))
            {
                Log("\r\n签名库文件不能为空!!!!!\r\n");
                txtKeystoreFile.Focus();
                return false;
            }
            if (!File.Exists(txtKeystoreFile.Text.Trim()))
            {
                Log("\r\n签名库文件不存在!!!!!\r\n");
                txtKeystoreFile.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                Log("\r\n密码不能为空!!!!!\r\n");
                txtPassword.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtAliaskey.Text.Trim()))
            {
                Log("\r\n别名不能为空!!!!!\r\n");
                txtAliaskey.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtAliaspwd.Text.Trim()))
            {
                Log("\r\n别名密码不能为空!!!!!\r\n");
                txtAliaspwd.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// 设置控件状态
        /// </summary>
        /// <param name="enabled"></param>
        private void SetViewEnabled(bool enabled) {

            txtApkFilePath.Enabled = enabled;
            txtNewFileDir.Enabled = enabled;
            txtNewFileName.Enabled = enabled;

            txtKeystoreFile.Enabled = enabled;
            txtPassword.Enabled = enabled;
            txtAliaskey.Enabled = enabled;
            txtAliaspwd.Enabled = enabled;

            btnNewFile.Enabled = enabled;
            btnExe.Enabled = enabled;
        }

        /// <summary>
        /// 读取输入的签名信息
        /// </summary>
        /// <returns></returns>
        private KeystoreConfig GetKeystoreConfig()
        {
            KeystoreConfig config = new KeystoreConfig();
            config.KeystoreFilePath = txtKeystoreFile.Text.Trim();
            config.Password = txtPassword.Text.Trim();
            config.Aliaskey = txtAliaskey.Text.Trim();
            config.Aliaspwd = txtAliaspwd.Text.Trim();

            return config;
        }


        /// <summary>
        /// 重签名处理
        /// </summary>
        private void ReignApk()
        {
            ThreadStart threadStart = new ThreadStart(delegate()
            {
                BlankLine();
                Log("================== 重签名-开始 ==================");
                BlankLine();

                string newFileDir = txtNewFileDir.Text.Trim();
                if (!Directory.Exists(newFileDir))
                {
                    Directory.CreateDirectory(newFileDir);
                }
                string newApkFilePath = PathUtils.JoinPath(newFileDir, txtNewFileName.Text.Trim());

                KeystoreConfig config = GetKeystoreConfig();
                // 将签名信息保存到配置
                ConfigUtils.SaveKeystoreConfig(config);

                // 重签名
                ResignApkBLL.ResignApk(ApkFilePath, newApkFilePath, config, this);

                BlankLine();
                Log("================== 重签名-结束 ==================");
                BlankLine();
                BlankLine();

                // 更新UI
                UpdateUI();
            });
            Thread thread = new Thread(threadStart);
            thread.Start();
        }


        /// <summary>
        /// 线程执行完毕,更新UI
        /// </summary>
        private void UpdateUI()
        {
            if (btnNewFile.InvokeRequired)
            {
                UpdateUIDelegate ud = new UpdateUIDelegate(UpdateUI);
                btnNewFile.Invoke(ud);
            } else
            {
                btnNewFile.Enabled = true;
                btnExe.Text = "关闭";
                btnExe.Enabled = true;

                string path = PathUtils.JoinPath(txtNewFileDir.Text.Trim(), txtNewFileName.Text.Trim());
                if (File.Exists(path))
                {
                    btnNewFile.Visible = true;
                    btnNewFile.Enabled = true;
                    btnNewFile.Focus();
                } else
                {
                    btnNewFile.Visible = false;
                    btnExe.Focus();
                }
            }
        }

     

        #region 控件事件处理

        /// <summary>
        /// 开始或关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExe_Click(object sender, EventArgs e)
        {
            if (btnExe.Text.Equals("关闭"))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
                this.Dispose();
                return;
            }

            // 检查表单
            if (VerifyForm() == false) return;

            // 设置控件状态
            SetViewEnabled(false);

            // 开始重签名
            ReignApk();
        }


        /// <summary>
        /// 拖放事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResignApkForm_DragEnter(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();

            if (txtApkFilePath.Focused && txtApkFilePath.Enabled)
            {
                ApkFilePath = path;

            } else if (txtNewFileDir.Focused && txtNewFileDir.Enabled)
            {
                txtNewFileDir.Text = path;

            } else if (txtKeystoreFile.Focused && txtKeystoreFile.Enabled)
            {
                txtKeystoreFile.Text = path;
            }
        }


        /// <summary>
        /// 打开文件位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewFile_Click(object sender, EventArgs e)
        {
            string path = PathUtils.JoinPath(txtNewFileDir.Text.Trim(), txtNewFileName.Text.Trim());

            if (FileUtils.OpenFile(path) == false)
            {
                BlankLine();
                Log(path + " 文件未生成!!!");
                BlankLine();
            }
        }

        #endregion


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
   
    }
}
