/***********************************************
 * 作者: ZhongGuanYao
 * 邮箱: 598115778@qq.com
 * 博客: https://blog.csdn.net/xiangxinzijiwonen
 *       https://github.com/zhongguanyao
 * 日期: 2018-11-06
 * **********************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace APKINFO.UI
{
    /// <summary>
    /// 密码输入框
    /// </summary>
    public partial class EnterPwdForm : Form
    {
        public EnterPwdForm()
        {
            InitializeComponent();
        }

        public EnterPwdForm(string title)
        {
            InitializeComponent();

            this.Text = title;
        }

        private string mPwd;
        public string Pwd { get { return mPwd; } }






        private void btnOK_Click(object sender, EventArgs e)
        {
            Confirm();
        }

        private void txtPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) {
                Confirm();
            }
        }

        private void Confirm() {
            if (string.IsNullOrEmpty(txtPwd.Text.Trim())) {
                lbMsg.Text = "请输入密码！";
                txtPwd.Focus();
                return;
            }

            mPwd = txtPwd.Text.Trim();

            this.DialogResult = DialogResult.OK;
            this.Dispose();
            this.Close();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        

        
    }
}
