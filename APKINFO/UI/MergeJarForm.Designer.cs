namespace APKINFO.UI
{
    partial class MergeJarForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtJarFilePath = new System.Windows.Forms.TextBox();
            this.txtApkFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtKeystoreFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtAliaskey = new System.Windows.Forms.TextBox();
            this.txtAliaspwd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtNewFileDir = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNewFileName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnExe = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.btnNewFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtJarFilePath
            // 
            this.txtJarFilePath.Location = new System.Drawing.Point(118, 62);
            this.txtJarFilePath.Name = "txtJarFilePath";
            this.txtJarFilePath.Size = new System.Drawing.Size(528, 21);
            this.txtJarFilePath.TabIndex = 1;
            // 
            // txtApkFilePath
            // 
            this.txtApkFilePath.Location = new System.Drawing.Point(118, 35);
            this.txtApkFilePath.Name = "txtApkFilePath";
            this.txtApkFilePath.Size = new System.Drawing.Size(528, 21);
            this.txtApkFilePath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Jar包文件或目录:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "APK文件:";
            // 
            // txtKeystoreFile
            // 
            this.txtKeystoreFile.Location = new System.Drawing.Point(118, 176);
            this.txtKeystoreFile.Name = "txtKeystoreFile";
            this.txtKeystoreFile.Size = new System.Drawing.Size(530, 21);
            this.txtKeystoreFile.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "签名库文件:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(118, 203);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(218, 21);
            this.txtPassword.TabIndex = 5;
            // 
            // txtAliaskey
            // 
            this.txtAliaskey.Location = new System.Drawing.Point(118, 230);
            this.txtAliaskey.Name = "txtAliaskey";
            this.txtAliaskey.Size = new System.Drawing.Size(218, 21);
            this.txtAliaskey.TabIndex = 6;
            // 
            // txtAliaspwd
            // 
            this.txtAliaspwd.Location = new System.Drawing.Point(118, 257);
            this.txtAliaspwd.Name = "txtAliaspwd";
            this.txtAliaspwd.Size = new System.Drawing.Size(218, 21);
            this.txtAliaspwd.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(77, 206);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "密码:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(77, 233);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "别名:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(53, 260);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "别名密码:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(29, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "导出目录位置:";
            // 
            // txtNewFileDir
            // 
            this.txtNewFileDir.Location = new System.Drawing.Point(118, 107);
            this.txtNewFileDir.Name = "txtNewFileDir";
            this.txtNewFileDir.Size = new System.Drawing.Size(528, 21);
            this.txtNewFileDir.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(29, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "导出文件名称:";
            // 
            // txtNewFileName
            // 
            this.txtNewFileName.Location = new System.Drawing.Point(118, 134);
            this.txtNewFileName.Name = "txtNewFileName";
            this.txtNewFileName.Size = new System.Drawing.Size(467, 21);
            this.txtNewFileName.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(30, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(485, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "输入文件或目录路径操作提示：1.光标停留在输入框 -> 2.将文件或目录拖放到本窗体即可";
            // 
            // btnExe
            // 
            this.btnExe.Location = new System.Drawing.Point(556, 244);
            this.btnExe.Name = "btnExe";
            this.btnExe.Size = new System.Drawing.Size(92, 34);
            this.btnExe.TabIndex = 8;
            this.btnExe.Text = "开始";
            this.btnExe.UseVisualStyleBackColor = true;
            this.btnExe.Click += new System.EventHandler(this.btnExe_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(7, 284);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(639, 175);
            this.txtLog.TabIndex = 9;
            this.txtLog.Text = "";
            // 
            // btnNewFile
            // 
            this.btnNewFile.Location = new System.Drawing.Point(591, 132);
            this.btnNewFile.Name = "btnNewFile";
            this.btnNewFile.Size = new System.Drawing.Size(57, 23);
            this.btnNewFile.TabIndex = 10;
            this.btnNewFile.Text = "打开";
            this.btnNewFile.UseVisualStyleBackColor = true;
            this.btnNewFile.Visible = false;
            this.btnNewFile.Click += new System.EventHandler(this.btnNewFile_Click);
            // 
            // MergeJarForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 474);
            this.Controls.Add(this.btnNewFile);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnExe);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtNewFileName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtNewFileDir);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtAliaspwd);
            this.Controls.Add(this.txtAliaskey);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtKeystoreFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtJarFilePath);
            this.Controls.Add(this.txtApkFilePath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MergeJarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "打入Jar包";
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MergeJarForm_DragEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtJarFilePath;
        private System.Windows.Forms.TextBox txtApkFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtKeystoreFile;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtAliaskey;
        private System.Windows.Forms.TextBox txtAliaspwd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNewFileDir;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNewFileName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnExe;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Button btnNewFile;
    }
}