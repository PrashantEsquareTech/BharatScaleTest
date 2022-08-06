namespace AIOInventorySystem.Desk.Forms
{
    partial class frmStaffInfo
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
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.tblltInfo = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStaffid = new System.Windows.Forms.TextBox();
            this.txtStaffName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAdharNo = new System.Windows.Forms.TextBox();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.tblltList = new System.Windows.Forms.TableLayoutPanel();
            this.lblTotalStaff = new System.Windows.Forms.Label();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.Gvstaffinfo = new System.Windows.Forms.DataGridView();
            this.label15 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbstaffname = new System.Windows.Forms.ComboBox();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.StaffIdg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StaffName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MobileNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AdharCardNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpInfo.SuspendLayout();
            this.tblltInfo.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.grpList.SuspendLayout();
            this.tblltList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gvstaffinfo)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.tblltInfo);
            this.grpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInfo.ForeColor = System.Drawing.Color.Black;
            this.grpInfo.Location = new System.Drawing.Point(3, 37);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(355, 302);
            this.grpInfo.TabIndex = 0;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Salesman Information";
            // 
            // tblltInfo
            // 
            this.tblltInfo.ColumnCount = 2;
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tblltInfo.Controls.Add(this.label8, 0, 0);
            this.tblltInfo.Controls.Add(this.txtAddress, 1, 2);
            this.tblltInfo.Controls.Add(this.label4, 0, 2);
            this.tblltInfo.Controls.Add(this.label3, 0, 1);
            this.tblltInfo.Controls.Add(this.txtStaffid, 1, 0);
            this.tblltInfo.Controls.Add(this.txtStaffName, 1, 1);
            this.tblltInfo.Controls.Add(this.label6, 0, 5);
            this.tblltInfo.Controls.Add(this.label5, 0, 4);
            this.tblltInfo.Controls.Add(this.txtAdharNo, 1, 5);
            this.tblltInfo.Controls.Add(this.txtMobileNo, 1, 4);
            this.tblltInfo.Controls.Add(this.tblltButtons, 0, 7);
            this.tblltInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltInfo.Location = new System.Drawing.Point(3, 21);
            this.tblltInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltInfo.Name = "tblltInfo";
            this.tblltInfo.RowCount = 8;
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltInfo.Size = new System.Drawing.Size(349, 278);
            this.tblltInfo.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(2, 2);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 23);
            this.label8.TabIndex = 27;
            this.label8.Text = "Salesman ID:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAddress
            // 
            this.txtAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(89, 56);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(2);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.tblltInfo.SetRowSpan(this.txtAddress, 2);
            this.txtAddress.Size = new System.Drawing.Size(258, 50);
            this.txtAddress.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(2, 56);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 23);
            this.label4.TabIndex = 21;
            this.label4.Text = "Address:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(2, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 23);
            this.label3.TabIndex = 23;
            this.label3.Text = "Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStaffid
            // 
            this.txtStaffid.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtStaffid.Enabled = false;
            this.txtStaffid.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStaffid.Location = new System.Drawing.Point(89, 2);
            this.txtStaffid.Margin = new System.Windows.Forms.Padding(2);
            this.txtStaffid.Name = "txtStaffid";
            this.txtStaffid.ReadOnly = true;
            this.txtStaffid.Size = new System.Drawing.Size(120, 25);
            this.txtStaffid.TabIndex = 0;
            this.txtStaffid.TabStop = false;
            // 
            // txtStaffName
            // 
            this.txtStaffName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStaffName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStaffName.Location = new System.Drawing.Point(89, 29);
            this.txtStaffName.Margin = new System.Windows.Forms.Padding(2);
            this.txtStaffName.Name = "txtStaffName";
            this.txtStaffName.Size = new System.Drawing.Size(258, 25);
            this.txtStaffName.TabIndex = 1;
            this.txtStaffName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStaffName_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(2, 137);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 23);
            this.label6.TabIndex = 19;
            this.label6.Text = "Email:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(2, 110);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 23);
            this.label5.TabIndex = 20;
            this.label5.Text = "Mobile No:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAdharNo
            // 
            this.txtAdharNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAdharNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdharNo.Location = new System.Drawing.Point(89, 137);
            this.txtAdharNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtAdharNo.Name = "txtAdharNo";
            this.txtAdharNo.Size = new System.Drawing.Size(258, 25);
            this.txtAdharNo.TabIndex = 4;
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtMobileNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobileNo.Location = new System.Drawing.Point(89, 110);
            this.txtMobileNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(200, 25);
            this.txtMobileNo.TabIndex = 3;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 6;
            this.tblltInfo.SetColumnSpan(this.tblltButtons, 5);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.5F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.5F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.5F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.5F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltButtons.Controls.Add(this.btnNew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnSave, 2, 0);
            this.tblltButtons.Controls.Add(this.btnDelete, 3, 0);
            this.tblltButtons.Controls.Add(this.btnClose, 4, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 237);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(349, 41);
            this.tblltButtons.TabIndex = 5;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(47, 2);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(60, 37);
            this.btnNew.TabIndex = 6;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnnew_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(111, 2);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 37);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(175, 2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 37);
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
            this.btnClose.Location = new System.Drawing.Point(239, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 37);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.tblltList);
            this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpList.ForeColor = System.Drawing.Color.Black;
            this.grpList.Location = new System.Drawing.Point(364, 37);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(437, 302);
            this.grpList.TabIndex = 9;
            this.grpList.TabStop = false;
            this.grpList.Text = "Salesman List";
            // 
            // tblltList
            // 
            this.tblltList.ColumnCount = 3;
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltList.Controls.Add(this.lblTotalStaff, 1, 0);
            this.tblltList.Controls.Add(this.btnGetAll, 2, 1);
            this.tblltList.Controls.Add(this.Gvstaffinfo, 0, 2);
            this.tblltList.Controls.Add(this.label15, 0, 0);
            this.tblltList.Controls.Add(this.label7, 0, 1);
            this.tblltList.Controls.Add(this.cmbstaffname, 1, 1);
            this.tblltList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltList.Location = new System.Drawing.Point(3, 21);
            this.tblltList.Margin = new System.Windows.Forms.Padding(0);
            this.tblltList.Name = "tblltList";
            this.tblltList.RowCount = 3;
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tblltList.Size = new System.Drawing.Size(431, 278);
            this.tblltList.TabIndex = 9;
            // 
            // lblTotalStaff
            // 
            this.lblTotalStaff.AutoSize = true;
            this.lblTotalStaff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalStaff.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalStaff.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalStaff.Location = new System.Drawing.Point(131, 2);
            this.lblTotalStaff.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotalStaff.Name = "lblTotalStaff";
            this.lblTotalStaff.Size = new System.Drawing.Size(211, 23);
            this.lblTotalStaff.TabIndex = 52;
            this.lblTotalStaff.Text = "0";
            this.lblTotalStaff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnGetAll
            // 
            this.btnGetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnGetAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGetAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAll.ForeColor = System.Drawing.Color.White;
            this.btnGetAll.Location = new System.Drawing.Point(346, 28);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(83, 25);
            this.btnGetAll.TabIndex = 10;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // Gvstaffinfo
            // 
            this.Gvstaffinfo.AllowUserToAddRows = false;
            this.Gvstaffinfo.AllowUserToDeleteRows = false;
            this.Gvstaffinfo.AllowUserToOrderColumns = true;
            this.Gvstaffinfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Gvstaffinfo.BackgroundColor = System.Drawing.Color.White;
            this.Gvstaffinfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Gvstaffinfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Gvstaffinfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StaffIdg,
            this.StaffName,
            this.Address,
            this.MobileNo,
            this.AdharCardNo});
            this.tblltList.SetColumnSpan(this.Gvstaffinfo, 3);
            this.Gvstaffinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gvstaffinfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.Gvstaffinfo.Location = new System.Drawing.Point(3, 57);
            this.Gvstaffinfo.Name = "Gvstaffinfo";
            this.Gvstaffinfo.ReadOnly = true;
            this.Gvstaffinfo.RowHeadersWidth = 15;
            this.Gvstaffinfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Gvstaffinfo.Size = new System.Drawing.Size(425, 218);
            this.Gvstaffinfo.TabIndex = 17;
            this.Gvstaffinfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Gvstaffinfo_CellClick);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label15.Location = new System.Drawing.Point(2, 2);
            this.label15.Margin = new System.Windows.Forms.Padding(2);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(125, 23);
            this.label15.TabIndex = 51;
            this.label15.Text = "Total Salesman:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(2, 29);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 23);
            this.label7.TabIndex = 11;
            this.label7.Text = "Salesman Name:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbstaffname
            // 
            this.cmbstaffname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbstaffname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbstaffname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbstaffname.FormattingEnabled = true;
            this.cmbstaffname.Location = new System.Drawing.Point(131, 29);
            this.cmbstaffname.Margin = new System.Windows.Forms.Padding(2);
            this.cmbstaffname.Name = "cmbstaffname";
            this.cmbstaffname.Size = new System.Drawing.Size(211, 25);
            this.cmbstaffname.TabIndex = 9;
            this.cmbstaffname.SelectedIndexChanged += new System.EventHandler(this.cmbstaffname_SelectedIndexChanged);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 2;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tblltMain.Controls.Add(this.grpInfo, 0, 1);
            this.tblltMain.Controls.Add(this.grpList, 1, 1);
            this.tblltMain.Controls.Add(this.label2, 0, 0);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 2;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tblltMain.Size = new System.Drawing.Size(804, 342);
            this.tblltMain.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label2, 2);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(804, 34);
            this.label2.TabIndex = 38;
            this.label2.Text = "Salesman Information";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StaffIdg
            // 
            this.StaffIdg.HeaderText = "Staff ID";
            this.StaffIdg.Name = "StaffIdg";
            this.StaffIdg.ReadOnly = true;
            this.StaffIdg.Visible = false;
            // 
            // StaffName
            // 
            this.StaffName.FillWeight = 97.99494F;
            this.StaffName.HeaderText = "Salesman Name";
            this.StaffName.Name = "StaffName";
            this.StaffName.ReadOnly = true;
            // 
            // Address
            // 
            this.Address.FillWeight = 122.1289F;
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            // 
            // MobileNo
            // 
            this.MobileNo.FillWeight = 78.35335F;
            this.MobileNo.HeaderText = "Mobile No";
            this.MobileNo.Name = "MobileNo";
            this.MobileNo.ReadOnly = true;
            // 
            // AdharCardNo
            // 
            this.AdharCardNo.FillWeight = 101.5228F;
            this.AdharCardNo.HeaderText = "Adhar Card No";
            this.AdharCardNo.Name = "AdharCardNo";
            this.AdharCardNo.ReadOnly = true;
            // 
            // frmStaffInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(804, 342);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmStaffInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Salesman Information";
            this.grpInfo.ResumeLayout(false);
            this.tblltInfo.ResumeLayout(false);
            this.tblltInfo.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.grpList.ResumeLayout(false);
            this.tblltList.ResumeLayout(false);
            this.tblltList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gvstaffinfo)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.TableLayoutPanel tblltInfo;
        private System.Windows.Forms.TextBox txtMobileNo;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.DataGridView Gvstaffinfo;
        private System.Windows.Forms.TableLayoutPanel tblltList;
        private System.Windows.Forms.Label lblTotalStaff;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStaffid;
        private System.Windows.Forms.TextBox txtAdharNo;
        private System.Windows.Forms.TextBox txtStaffName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.ComboBox cmbstaffname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnGetAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn StaffIdg;
        private System.Windows.Forms.DataGridViewTextBoxColumn StaffName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn MobileNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn AdharCardNo;
    }
}