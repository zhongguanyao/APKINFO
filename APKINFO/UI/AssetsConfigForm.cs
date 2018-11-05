/***********************************************
 * 作者: ZhongGuanYao
 * 邮箱: 598115778@qq.com
 * 博客: https://blog.csdn.net/xiangxinzijiwonen
 *       https://github.com/zhongguanyao
 * 日期: 2018-10-27
 * **********************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using APKINFO.Entity;
using APKINFO.Utils;

namespace APKINFO.UI
{
    /// <summary>
    /// 用户配置界面(读取Assets目录下配置文件参数)
    /// </summary>
    public partial class AssetsConfigForm : Form
    {
        public AssetsConfigForm()
        {
            InitializeComponent();

            // 读取配置
            mConfigLst = ConfigUtils.ReadUserConfig();
            refreshRow();
        }


        List<AssetsConfig> mConfigLst;

        /// <summary>
        /// 刷新列表
        /// </summary>
        private void refreshRow() {
            dgv1.Rows.Clear();
            if (mConfigLst == null || mConfigLst.Count <= 0) return;

            dgv1.RowTemplate.Height = 30;
            foreach (AssetsConfig config in mConfigLst)
            {
                int index = dgv1.Rows.Add();
                dgv1.Rows[index].Cells["colNum"].Value = index + 1;
                dgv1.Rows[index].Cells["colFileName"].Value = config.FileName;
                dgv1.Rows[index].Cells["colKeys"].Value = config.Keys;
                dgv1.Rows[index].Cells["colDel"].Value = "删除";
            }
        }


        /// <summary>
        /// 新增配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFileName.Text.Trim())) {
                MessageBox.Show("请输入配置文件名称");
                txtFileName.Focus();
                return;
            }

            AssetsConfig config = new AssetsConfig();
            config.FileName = txtFileName.Text.Trim();
            if (!string.IsNullOrEmpty(txtKeys.Text.Trim()))
            {
                config.Keys = txtKeys.Text.Trim();
            }

            if (mConfigLst == null) mConfigLst = new List<AssetsConfig>();
            mConfigLst.Add(config);

            refreshRow();
            ConfigUtils.SaveUserConfig(mConfigLst);

            txtFileName.Text = "";
            txtKeys.Text = "";
        }

        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 3) {
                DialogResult dr = MessageBox.Show("确定要删除第 " + (e.RowIndex + 1) + " 行配置吗？", "提示", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.OK) {
                    mConfigLst.RemoveAt(e.RowIndex);
                    refreshRow();
                    ConfigUtils.SaveUserConfig(mConfigLst);
                }
            }
        }
    }
}
