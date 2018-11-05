namespace APKINFO.UI
{
    partial class AssetsConfigForm
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
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.txtKeys = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.colNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colKeys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDel = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv1
            // 
            this.dgv1.AllowUserToAddRows = false;
            this.dgv1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colNum,
            this.colFileName,
            this.colKeys,
            this.colDel});
            this.dgv1.GridColor = System.Drawing.SystemColors.Control;
            this.dgv1.Location = new System.Drawing.Point(12, 12);
            this.dgv1.MultiSelect = false;
            this.dgv1.Name = "dgv1";
            this.dgv1.ReadOnly = true;
            this.dgv1.RowHeadersVisible = false;
            this.dgv1.RowTemplate.Height = 23;
            this.dgv1.Size = new System.Drawing.Size(738, 215);
            this.dgv1.TabIndex = 2;
            this.dgv1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtKeys);
            this.groupBox1.Controls.Add(this.txtFileName);
            this.groupBox1.Location = new System.Drawing.Point(12, 246);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(741, 115);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "新增配置";
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(179, 24);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(297, 21);
            this.txtFileName.TabIndex = 0;
            // 
            // txtKeys
            // 
            this.txtKeys.Location = new System.Drawing.Point(179, 51);
            this.txtKeys.Name = "txtKeys";
            this.txtKeys.Size = new System.Drawing.Size(545, 21);
            this.txtKeys.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Assets目录下的配置文件名称:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "读取该配置文件的参数:";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(179, 78);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(92, 22);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Maroon;
            this.label3.Location = new System.Drawing.Point(509, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(215, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "多个用逗号隔开;不填表示读取全部配置";
            // 
            // colNum
            // 
            this.colNum.HeaderText = "序号";
            this.colNum.Name = "colNum";
            this.colNum.ReadOnly = true;
            this.colNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colNum.Width = 40;
            // 
            // colFileName
            // 
            this.colFileName.HeaderText = "配置文件名称(包括后缀)";
            this.colFileName.Name = "colFileName";
            this.colFileName.ReadOnly = true;
            this.colFileName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colFileName.Width = 200;
            // 
            // colKeys
            // 
            this.colKeys.HeaderText = "读取该配置文件的参数(多个参数用逗号隔开)";
            this.colKeys.Name = "colKeys";
            this.colKeys.ReadOnly = true;
            this.colKeys.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.colKeys.Width = 400;
            // 
            // colDel
            // 
            this.colDel.HeaderText = "删除";
            this.colDel.Name = "colDel";
            this.colDel.ReadOnly = true;
            this.colDel.Width = 60;
            // 
            // AssetsConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 379);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AssetsConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "可以设置读取APK的assets目录下的配置文件   注意:只能读取以key=value形式的配置文件)";
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKeys;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colKeys;
        private System.Windows.Forms.DataGridViewButtonColumn colDel;
    }
}