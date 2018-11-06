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
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using APKINFO.Utils;
using APKINFO.Entity;
using APKINFO.BLL;

namespace APKINFO.UI
{
    /// <summary>
    /// APK信息窗体
    /// </summary>
    public partial class ApkInfoForm : Form
    {
        public ApkInfoForm()
        {
            InitializeComponent();
        }

        public ApkInfoForm(ApkInfo apkInfo)
        {
            InitializeComponent();

            mApkInfo = apkInfo;
            refreshUI();
        }

        ApkInfo mApkInfo;

        /// <summary>
        /// 刷新UI
        /// </summary>
        public void refreshUI()
        {
            if (mApkInfo == null) return;

            txtAppName.Text = mApkInfo.AppName;
            txtVersionCode.Text = mApkInfo.VersionCode;
            txtVersionName.Text = mApkInfo.VersionName;
            txtPackageName.Text = mApkInfo.PackageName;
            txtMaxVersion.Text = mApkInfo.MaxSdkVersion;
            txtTargetVersion.Text = mApkInfo.TargetSdkVersion;
            txtMinSdkVersion.Text = mApkInfo.MinSdkVersion;
            txtYsdkconf.Text = mApkInfo.AssetsConfig;
            txtDensities.Text = mApkInfo.Densities;
            txtSupportsScreens.Text = mApkInfo.SupportsScreens;
            txtUsesPermission.Text = mApkInfo.UsesPermission;
            txtUsesFeature.Text = mApkInfo.UsesFeature;
            this.Text = "APKINFO-V1.0  " + mApkInfo.CurrentFileName;
            txtIconName.Text = mApkInfo.IconName;
            picIcon.BackgroundImage = mApkInfo.Icon;

            bool visible = (mApkInfo != null && !string.IsNullOrEmpty(mApkInfo.ApkFilePath));
            btnBrowseCertificate.Visible = (mApkInfo.AllInfoList != null);
            btnDecompile.Visible = visible;
            btnBrowseSource.Visible = visible;
            btnResignApk.Visible = visible;
        }



        #region 按钮事件处理

        /// <summary>
        /// 查看源码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowseSource_Click(object sender, EventArgs e)
        {
            if (mApkInfo == null) return;

            BrowseSourceBLL.OpenJadxGUI(mApkInfo.ApkFilePath);
            this.Dispose();
            this.Close();
        }


        /// <summary>
        /// 查看签名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowseCertificate_Click(object sender, EventArgs e)
        {
            if (mApkInfo == null) return;

            BrowseCertForm form = new BrowseCertForm(mApkInfo.ApkFilePath, Constants.TYPE_APK_FILE);
            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.Dispose();
                this.Close();
            }
        }

        /// <summary>
        /// 重签名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnResignApk_Click(object sender, EventArgs e)
        {
            if (mApkInfo == null) return;

            ResignApkForm form = new ResignApkForm();
            form.ApkFilePath = mApkInfo.ApkFilePath;
            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK) {
                this.Dispose();
                this.Close();
            }
        }

        /// <summary>
        /// 反编译
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDecompile_Click(object sender, EventArgs e)
        {
            if (mApkInfo == null) return;

            ThreadStart threadStart = new ThreadStart(delegate()
            {
                string decompileDir = DecompileBLL.DecompileApk(mApkInfo.ApkFilePath);
                closeMsgForm();
                if (!string.IsNullOrEmpty(decompileDir))
                {
                    FileUtils.OpenDir(decompileDir);
                }
            });
            Thread thread = new Thread(threadStart);
            thread.Start();

            showMsgForm();
        }

        /// <summary>
        /// 打入Jar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMergeJar_Click(object sender, EventArgs e)
        {
            if (mApkInfo == null) return;

            MergeJarForm form = new MergeJarForm();
            form.ApkFilePath = mApkInfo.ApkFilePath;
            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                this.Dispose();
                this.Close();
            }
        }

        /// <summary>
        /// 导出TXT文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llbExportTxtFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (mApkInfo == null) return;

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string txtFilePath = dialog.SelectedPath + "\\apkInfo" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".txt";
                BrowseApkInfoBLL.ExportTxtFile(txtFilePath, mApkInfo.AllInfoList);
            }
        }

        
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 设置读取文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void llSetAssetsFile_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AssetsConfigForm form = new AssetsConfigForm();
            form.ShowDialog();
        }


        #endregion


        #region 提示框

        public MessageForm sMessageForm = null;

        public delegate void CloseFormDelegate();

        /// <summary>
        /// 打开提示框
        /// </summary>
        public void showMsgForm()
        {
            sMessageForm = new MessageForm();
            sMessageForm.StartPosition = FormStartPosition.CenterParent;
            sMessageForm.ShowDialog();
        }


        /// <summary>
        /// 关闭提示框
        /// </summary>
        public void closeMsgForm()
        {
            if (sMessageForm == null) return;

            if (sMessageForm.InvokeRequired)
            {
                CloseFormDelegate pro = new CloseFormDelegate(closeMsgForm);
                sMessageForm.Invoke(pro);
            } else
            {
                sMessageForm.Close();
                sMessageForm.Dispose();
                this.Close();
                this.Dispose();
            }
        }

        #endregion

        
       

    }
}
