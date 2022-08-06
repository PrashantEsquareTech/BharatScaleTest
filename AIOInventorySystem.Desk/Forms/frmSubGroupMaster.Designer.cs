namespace AIOInventorySystem.Desk.Forms
{
    partial class frmSubGroupMaster
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtsubgroupname = new System.Windows.Forms.TextBox();
            this.txtdesc = new System.Windows.Forms.TextBox();
            this.cmbgroup = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.tblltInfo = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnaddgroup = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.dtgvList = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubGroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GroupName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblltMain.SuspendLayout();
            this.grpInfo.SuspendLayout();
            this.tblltInfo.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sub Group Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 56);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "Description:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtsubgroupname
            // 
            this.txtsubgroupname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtsubgroupname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsubgroupname.Location = new System.Drawing.Point(123, 2);
            this.txtsubgroupname.Margin = new System.Windows.Forms.Padding(2);
            this.txtsubgroupname.Name = "txtsubgroupname";
            this.txtsubgroupname.Size = new System.Drawing.Size(285, 25);
            this.txtsubgroupname.TabIndex = 1;
            // 
            // txtdesc
            // 
            this.txtdesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtdesc.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdesc.Location = new System.Drawing.Point(123, 56);
            this.txtdesc.Margin = new System.Windows.Forms.Padding(2);
            this.txtdesc.Multiline = true;
            this.txtdesc.Name = "txtdesc";
            this.tblltInfo.SetRowSpan(this.txtdesc, 2);
            this.txtdesc.Size = new System.Drawing.Size(285, 50);
            this.txtdesc.TabIndex = 4;
            // 
            // cmbgroup
            // 
            this.cmbgroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbgroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbgroup.FormattingEnabled = true;
            this.cmbgroup.Location = new System.Drawing.Point(123, 29);
            this.cmbgroup.Margin = new System.Windows.Forms.Padding(2);
            this.cmbgroup.Name = "cmbgroup";
            this.cmbgroup.Size = new System.Drawing.Size(285, 25);
            this.cmbgroup.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 23);
            this.label1.TabIndex = 13;
            this.label1.Text = "Group Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.grpInfo, 0, 1);
            this.tblltMain.Controls.Add(this.label8, 0, 0);
            this.tblltMain.Controls.Add(this.grpList, 0, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tblltMain.Size = new System.Drawing.Size(464, 482);
            this.tblltMain.TabIndex = 15;
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.tblltInfo);
            this.grpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInfo.Location = new System.Drawing.Point(3, 41);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(458, 172);
            this.grpInfo.TabIndex = 0;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Info";
            // 
            // tblltInfo
            // 
            this.tblltInfo.ColumnCount = 3;
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.82927F));
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.07982F));
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.090908F));
            this.tblltInfo.Controls.Add(this.tblltButtons, 0, 5);
            this.tblltInfo.Controls.Add(this.label2, 0, 0);
            this.tblltInfo.Controls.Add(this.label1, 0, 1);
            this.tblltInfo.Controls.Add(this.label3, 0, 2);
            this.tblltInfo.Controls.Add(this.btnaddgroup, 2, 1);
            this.tblltInfo.Controls.Add(this.txtsubgroupname, 1, 0);
            this.tblltInfo.Controls.Add(this.txtdesc, 1, 2);
            this.tblltInfo.Controls.Add(this.cmbgroup, 1, 1);
            this.tblltInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltInfo.Location = new System.Drawing.Point(3, 21);
            this.tblltInfo.Name = "tblltInfo";
            this.tblltInfo.RowCount = 6;
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.8F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.8F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.8F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.8F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.8F));
            this.tblltInfo.Size = new System.Drawing.Size(452, 148);
            this.tblltInfo.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 6;
            this.tblltInfo.SetColumnSpan(this.tblltButtons, 3);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.Controls.Add(this.btnnew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnAdd, 2, 0);
            this.tblltButtons.Controls.Add(this.btnDelete, 3, 0);
            this.tblltButtons.Controls.Add(this.btnClose, 4, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 116);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(452, 32);
            this.tblltButtons.TabIndex = 5;
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(77, 2);
            this.btnnew.Margin = new System.Windows.Forms.Padding(2);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(71, 28);
            this.btnnew.TabIndex = 6;
            this.btnnew.Text = "New";
            this.btnnew.UseVisualStyleBackColor = false;
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(152, 2);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(71, 28);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(227, 2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(71, 28);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(302, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(71, 28);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnaddgroup
            // 
            this.btnaddgroup.BackgroundImage = global::AIOInventorySystem.Desk.Properties.Resources.newbutton;
            this.btnaddgroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnaddgroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnaddgroup.FlatAppearance.BorderSize = 0;
            this.btnaddgroup.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnaddgroup.Location = new System.Drawing.Point(412, 29);
            this.btnaddgroup.Margin = new System.Windows.Forms.Padding(2);
            this.btnaddgroup.Name = "btnaddgroup";
            this.btnaddgroup.Size = new System.Drawing.Size(38, 23);
            this.btnaddgroup.TabIndex = 3;
            this.btnaddgroup.UseVisualStyleBackColor = true;
            this.btnaddgroup.Click += new System.EventHandler(this.btnaddgroup_Click);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(464, 38);
            this.label8.TabIndex = 38;
            this.label8.Text = "Sub Group Information";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.dtgvList);
            this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpList.Location = new System.Drawing.Point(2, 218);
            this.grpList.Margin = new System.Windows.Forms.Padding(2);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(460, 262);
            this.grpList.TabIndex = 9;
            this.grpList.TabStop = false;
            this.grpList.Text = "All Sub Group Information";
            // 
            // dtgvList
            // 
            this.dtgvList.AllowUserToAddRows = false;
            this.dtgvList.AllowUserToDeleteRows = false;
            this.dtgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvList.BackgroundColor = System.Drawing.Color.White;
            this.dtgvList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dtgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.SubGroupName,
            this.GroupId,
            this.GroupName,
            this.Description});
            this.dtgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgvList.Location = new System.Drawing.Point(3, 21);
            this.dtgvList.Name = "dtgvList";
            this.dtgvList.ReadOnly = true;
            this.dtgvList.RowHeadersWidth = 15;
            this.dtgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvList.Size = new System.Drawing.Size(454, 238);
            this.dtgvList.TabIndex = 9;
            this.dtgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvList_CellClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // SubGroupName
            // 
            this.SubGroupName.HeaderText = "Sub Group Name";
            this.SubGroupName.Name = "SubGroupName";
            this.SubGroupName.ReadOnly = true;
            // 
            // GroupId
            // 
            this.GroupId.HeaderText = "Group Id";
            this.GroupId.Name = "GroupId";
            this.GroupId.ReadOnly = true;
            this.GroupId.Visible = false;
            // 
            // GroupName
            // 
            this.GroupName.HeaderText = "Group Name";
            this.GroupName.Name = "GroupName";
            this.GroupName.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // frmSubGroupMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(464, 482);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "frmSubGroupMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sub Group Master";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSubGroupMaster_KeyDown);
            this.tblltMain.ResumeLayout(false);
            this.grpInfo.ResumeLayout(false);
            this.tblltInfo.ResumeLayout(false);
            this.tblltInfo.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.grpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtsubgroupname;
        private System.Windows.Forms.TextBox txtdesc;
        private System.Windows.Forms.ComboBox cmbgroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnaddgroup;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TableLayoutPanel tblltInfo;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.DataGridView dtgvList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubGroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;

    }
}