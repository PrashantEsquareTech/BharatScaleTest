namespace AIOInventorySystem.Desk.Forms
{
    partial class frmOtherIncome
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.GvexpenceInfo = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IncomeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACGId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IncomeNameg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.From = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.To = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bankname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChqUTRNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChqNEFTDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaidAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblltSearch = new System.Windows.Forms.TableLayoutPanel();
            this.btnprintall = new System.Windows.Forms.Button();
            this.btngetall = new System.Windows.Forms.Button();
            this.btnsearch = new System.Windows.Forms.Button();
            this.cmbsearchincome = new System.Windows.Forms.ComboBox();
            this.chkIncome = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.chkdate = new System.Windows.Forms.CheckBox();
            this.dtpfromdate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.dtptodate = new System.Windows.Forms.DateTimePicker();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.tblltIncomeInfo = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnAddIncomeM = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtchequeno = new System.Windows.Forms.TextBox();
            this.cmbbankname = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.dtpchequedate = new System.Windows.Forms.DateTimePicker();
            this.label21 = new System.Windows.Forms.Label();
            this.cmbpaymentmode = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.txtpaidamount = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dtpincomedate = new System.Windows.Forms.DateTimePicker();
            this.label22 = new System.Windows.Forms.Label();
            this.txtexpenceno = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbACGroups = new System.Windows.Forms.ComboBox();
            this.cmbincome = new System.Windows.Forms.ComboBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.txtfrom = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtto = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.txtreason = new System.Windows.Forms.TextBox();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.GvexpenceInfo)).BeginInit();
            this.tblltSearch.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.grpInfo.SuspendLayout();
            this.tblltIncomeInfo.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // GvexpenceInfo
            // 
            this.GvexpenceInfo.AllowUserToAddRows = false;
            this.GvexpenceInfo.AllowUserToDeleteRows = false;
            this.GvexpenceInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvexpenceInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvexpenceInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvexpenceInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvexpenceInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvexpenceInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.IncomeId,
            this.ACGId,
            this.Date,
            this.ACGroup,
            this.IncomeNameg,
            this.From,
            this.To,
            this.Reason,
            this.Mode,
            this.bankname,
            this.ChqUTRNo,
            this.ChqNEFTDate,
            this.PaidAmount});
            this.tblltSearch.SetColumnSpan(this.GvexpenceInfo, 11);
            this.GvexpenceInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvexpenceInfo.GridColor = System.Drawing.Color.Red;
            this.GvexpenceInfo.Location = new System.Drawing.Point(2, 31);
            this.GvexpenceInfo.Margin = new System.Windows.Forms.Padding(2);
            this.GvexpenceInfo.Name = "GvexpenceInfo";
            this.GvexpenceInfo.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvexpenceInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GvexpenceInfo.RowHeadersWidth = 15;
            this.GvexpenceInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvexpenceInfo.Size = new System.Drawing.Size(898, 246);
            this.GvexpenceInfo.TabIndex = 27;
            this.GvexpenceInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvexpenceInfo_CellClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // IncomeId
            // 
            this.IncomeId.FillWeight = 75.48224F;
            this.IncomeId.HeaderText = "IncomeId";
            this.IncomeId.Name = "IncomeId";
            this.IncomeId.ReadOnly = true;
            this.IncomeId.Visible = false;
            // 
            // ACGId
            // 
            this.ACGId.HeaderText = "ACGId";
            this.ACGId.Name = "ACGId";
            this.ACGId.ReadOnly = true;
            this.ACGId.Visible = false;
            // 
            // Date
            // 
            this.Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Date.FillWeight = 75.86525F;
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 80;
            // 
            // ACGroup
            // 
            this.ACGroup.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ACGroup.FillWeight = 75.86525F;
            this.ACGroup.HeaderText = "ACGroup";
            this.ACGroup.Name = "ACGroup";
            this.ACGroup.ReadOnly = true;
            this.ACGroup.Width = 110;
            // 
            // IncomeNameg
            // 
            this.IncomeNameg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IncomeNameg.FillWeight = 75.86525F;
            this.IncomeNameg.HeaderText = "Income Name";
            this.IncomeNameg.Name = "IncomeNameg";
            this.IncomeNameg.ReadOnly = true;
            this.IncomeNameg.Width = 110;
            // 
            // From
            // 
            this.From.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.From.FillWeight = 75.86525F;
            this.From.HeaderText = "From";
            this.From.Name = "From";
            this.From.ReadOnly = true;
            // 
            // To
            // 
            this.To.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.To.FillWeight = 75.86525F;
            this.To.HeaderText = "To";
            this.To.Name = "To";
            this.To.ReadOnly = true;
            // 
            // Reason
            // 
            this.Reason.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Reason.FillWeight = 75.86525F;
            this.Reason.HeaderText = "Reason";
            this.Reason.Name = "Reason";
            this.Reason.ReadOnly = true;
            // 
            // Mode
            // 
            this.Mode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Mode.FillWeight = 75.86525F;
            this.Mode.HeaderText = "Mode";
            this.Mode.Name = "Mode";
            this.Mode.ReadOnly = true;
            this.Mode.Width = 70;
            // 
            // bankname
            // 
            this.bankname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.bankname.FillWeight = 75.86525F;
            this.bankname.HeaderText = "bankname";
            this.bankname.Name = "bankname";
            this.bankname.ReadOnly = true;
            this.bankname.Width = 80;
            // 
            // ChqUTRNo
            // 
            this.ChqUTRNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ChqUTRNo.FillWeight = 75.86525F;
            this.ChqUTRNo.HeaderText = "Chq/UTR No";
            this.ChqUTRNo.Name = "ChqUTRNo";
            this.ChqUTRNo.ReadOnly = true;
            this.ChqUTRNo.Width = 70;
            // 
            // ChqNEFTDate
            // 
            this.ChqNEFTDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ChqNEFTDate.FillWeight = 75.86525F;
            this.ChqNEFTDate.HeaderText = "Chq/NEFT Date";
            this.ChqNEFTDate.Name = "ChqNEFTDate";
            this.ChqNEFTDate.ReadOnly = true;
            this.ChqNEFTDate.Width = 70;
            // 
            // PaidAmount
            // 
            this.PaidAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PaidAmount.FillWeight = 75.86525F;
            this.PaidAmount.HeaderText = "Paid AMount";
            this.PaidAmount.Name = "PaidAmount";
            this.PaidAmount.ReadOnly = true;
            this.PaidAmount.Width = 80;
            // 
            // tblltSearch
            // 
            this.tblltSearch.ColumnCount = 11;
            this.tblltSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.5F));
            this.tblltSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tblltSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.5F));
            this.tblltSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltSearch.Controls.Add(this.btnprintall, 10, 0);
            this.tblltSearch.Controls.Add(this.btngetall, 9, 0);
            this.tblltSearch.Controls.Add(this.btnsearch, 8, 0);
            this.tblltSearch.Controls.Add(this.GvexpenceInfo, 0, 1);
            this.tblltSearch.Controls.Add(this.cmbsearchincome, 5, 0);
            this.tblltSearch.Controls.Add(this.chkIncome, 4, 0);
            this.tblltSearch.Controls.Add(this.label13, 7, 0);
            this.tblltSearch.Controls.Add(this.chkdate, 0, 0);
            this.tblltSearch.Controls.Add(this.dtpfromdate, 1, 0);
            this.tblltSearch.Controls.Add(this.label3, 6, 0);
            this.tblltSearch.Controls.Add(this.label18, 2, 0);
            this.tblltSearch.Controls.Add(this.dtptodate, 3, 0);
            this.tblltSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltSearch.Location = new System.Drawing.Point(3, 22);
            this.tblltSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltSearch.Name = "tblltSearch";
            this.tblltSearch.RowCount = 2;
            this.tblltSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.5F));
            this.tblltSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.5F));
            this.tblltSearch.Size = new System.Drawing.Size(902, 279);
            this.tblltSearch.TabIndex = 19;
            // 
            // btnprintall
            // 
            this.btnprintall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprintall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprintall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnprintall.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprintall.ForeColor = System.Drawing.Color.White;
            this.btnprintall.Location = new System.Drawing.Point(851, 1);
            this.btnprintall.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnprintall.Name = "btnprintall";
            this.btnprintall.Size = new System.Drawing.Size(49, 27);
            this.btnprintall.TabIndex = 26;
            this.btnprintall.Text = "Print";
            this.btnprintall.UseVisualStyleBackColor = false;
            this.btnprintall.Click += new System.EventHandler(this.btnprintall_Click);
            // 
            // btngetall
            // 
            this.btngetall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btngetall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btngetall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btngetall.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngetall.ForeColor = System.Drawing.Color.White;
            this.btngetall.Location = new System.Drawing.Point(788, 1);
            this.btngetall.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btngetall.Name = "btngetall";
            this.btngetall.Size = new System.Drawing.Size(59, 27);
            this.btngetall.TabIndex = 25;
            this.btngetall.Text = "Get All";
            this.btngetall.UseVisualStyleBackColor = false;
            this.btngetall.Click += new System.EventHandler(this.btngetall_Click);
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(725, 1);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(59, 27);
            this.btnsearch.TabIndex = 24;
            this.btnsearch.Text = "Search";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // cmbsearchincome
            // 
            this.cmbsearchincome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbsearchincome.DropDownHeight = 110;
            this.cmbsearchincome.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbsearchincome.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbsearchincome.FormattingEnabled = true;
            this.cmbsearchincome.IntegralHeight = false;
            this.cmbsearchincome.Items.AddRange(new object[] {
            "Cash",
            "Cheque"});
            this.cmbsearchincome.Location = new System.Drawing.Point(415, 2);
            this.cmbsearchincome.Margin = new System.Windows.Forms.Padding(2);
            this.cmbsearchincome.Name = "cmbsearchincome";
            this.cmbsearchincome.Size = new System.Drawing.Size(126, 25);
            this.cmbsearchincome.TabIndex = 23;
            this.cmbsearchincome.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbsearchincome_KeyDown);
            // 
            // chkIncome
            // 
            this.chkIncome.AutoSize = true;
            this.chkIncome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkIncome.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncome.Location = new System.Drawing.Point(303, 2);
            this.chkIncome.Margin = new System.Windows.Forms.Padding(2);
            this.chkIncome.Name = "chkIncome";
            this.chkIncome.Size = new System.Drawing.Size(108, 25);
            this.chkIncome.TabIndex = 22;
            this.chkIncome.Text = "Income Name";
            this.chkIncome.UseVisualStyleBackColor = true;
            this.chkIncome.CheckedChanged += new System.EventHandler(this.chkIncome_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label13.Location = new System.Drawing.Point(643, 2);
            this.label13.Margin = new System.Windows.Forms.Padding(1, 2, 2, 2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 25);
            this.label13.TabIndex = 84;
            this.label13.Text = "0";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkdate
            // 
            this.chkdate.AutoSize = true;
            this.chkdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkdate.Location = new System.Drawing.Point(2, 2);
            this.chkdate.Margin = new System.Windows.Forms.Padding(2);
            this.chkdate.Name = "chkdate";
            this.chkdate.Size = new System.Drawing.Size(90, 25);
            this.chkdate.TabIndex = 19;
            this.chkdate.Text = "From Date";
            this.chkdate.UseVisualStyleBackColor = true;
            this.chkdate.CheckedChanged += new System.EventHandler(this.chkdate_CheckedChanged);
            // 
            // dtpfromdate
            // 
            this.dtpfromdate.CustomFormat = "dd/MM/yyyy";
            this.dtpfromdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpfromdate.Location = new System.Drawing.Point(96, 2);
            this.dtpfromdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpfromdate.Name = "dtpfromdate";
            this.dtpfromdate.Size = new System.Drawing.Size(86, 25);
            this.dtpfromdate.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(545, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 2, 1, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 25);
            this.label3.TabIndex = 85;
            this.label3.Text = "Total Income:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(186, 2);
            this.label18.Margin = new System.Windows.Forms.Padding(2);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(23, 25);
            this.label18.TabIndex = 33;
            this.label18.Text = "To";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtptodate
            // 
            this.dtptodate.CustomFormat = "dd/MM/yyyy";
            this.dtptodate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtptodate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtptodate.Location = new System.Drawing.Point(213, 2);
            this.dtptodate.Margin = new System.Windows.Forms.Padding(2);
            this.dtptodate.Name = "dtptodate";
            this.dtptodate.Size = new System.Drawing.Size(86, 25);
            this.dtptodate.TabIndex = 21;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.grpInfo, 0, 1);
            this.tblltMain.Controls.Add(this.grpSearch, 0, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.5F));
            this.tblltMain.Size = new System.Drawing.Size(914, 582);
            this.tblltMain.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(914, 37);
            this.label5.TabIndex = 34;
            this.label5.Text = "Other Income";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.tblltIncomeInfo);
            this.grpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInfo.Location = new System.Drawing.Point(3, 41);
            this.grpInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpInfo.Size = new System.Drawing.Size(908, 224);
            this.grpInfo.TabIndex = 0;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Income Info";
            // 
            // tblltIncomeInfo
            // 
            this.tblltIncomeInfo.ColumnCount = 7;
            this.tblltIncomeInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.5F));
            this.tblltIncomeInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltIncomeInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.5F));
            this.tblltIncomeInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.5F));
            this.tblltIncomeInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.5F));
            this.tblltIncomeInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltIncomeInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltIncomeInfo.Controls.Add(this.tblltButtons, 4, 6);
            this.tblltIncomeInfo.Controls.Add(this.btnAddIncomeM, 3, 2);
            this.tblltIncomeInfo.Controls.Add(this.label10, 0, 0);
            this.tblltIncomeInfo.Controls.Add(this.txtchequeno, 5, 3);
            this.tblltIncomeInfo.Controls.Add(this.cmbbankname, 5, 1);
            this.tblltIncomeInfo.Controls.Add(this.label12, 4, 3);
            this.tblltIncomeInfo.Controls.Add(this.label15, 6, 4);
            this.tblltIncomeInfo.Controls.Add(this.label19, 4, 4);
            this.tblltIncomeInfo.Controls.Add(this.dtpchequedate, 5, 2);
            this.tblltIncomeInfo.Controls.Add(this.label21, 4, 2);
            this.tblltIncomeInfo.Controls.Add(this.cmbpaymentmode, 5, 0);
            this.tblltIncomeInfo.Controls.Add(this.label23, 4, 0);
            this.tblltIncomeInfo.Controls.Add(this.label24, 6, 0);
            this.tblltIncomeInfo.Controls.Add(this.label25, 4, 1);
            this.tblltIncomeInfo.Controls.Add(this.txtpaidamount, 5, 4);
            this.tblltIncomeInfo.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.tblltIncomeInfo.Controls.Add(this.label8, 0, 1);
            this.tblltIncomeInfo.Controls.Add(this.cmbACGroups, 1, 1);
            this.tblltIncomeInfo.Controls.Add(this.cmbincome, 1, 2);
            this.tblltIncomeInfo.Controls.Add(this.label29, 0, 2);
            this.tblltIncomeInfo.Controls.Add(this.label28, 0, 3);
            this.tblltIncomeInfo.Controls.Add(this.txtfrom, 1, 3);
            this.tblltIncomeInfo.Controls.Add(this.label27, 0, 4);
            this.tblltIncomeInfo.Controls.Add(this.txtto, 1, 4);
            this.tblltIncomeInfo.Controls.Add(this.label26, 0, 5);
            this.tblltIncomeInfo.Controls.Add(this.label31, 2, 2);
            this.tblltIncomeInfo.Controls.Add(this.label30, 2, 4);
            this.tblltIncomeInfo.Controls.Add(this.txtreason, 1, 5);
            this.tblltIncomeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltIncomeInfo.Location = new System.Drawing.Point(3, 22);
            this.tblltIncomeInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltIncomeInfo.Name = "tblltIncomeInfo";
            this.tblltIncomeInfo.RowCount = 7;
            this.tblltIncomeInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltIncomeInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltIncomeInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltIncomeInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltIncomeInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltIncomeInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltIncomeInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltIncomeInfo.Size = new System.Drawing.Size(902, 198);
            this.tblltIncomeInfo.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 7;
            this.tblltIncomeInfo.SetColumnSpan(this.tblltButtons, 3);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.Controls.Add(this.btnnew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnsave, 2, 0);
            this.tblltButtons.Controls.Add(this.btndelete, 3, 0);
            this.tblltButtons.Controls.Add(this.btnclose, 4, 0);
            this.tblltButtons.Controls.Add(this.btnprint, 5, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(444, 168);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(458, 30);
            this.tblltButtons.TabIndex = 14;
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(67, 1);
            this.btnnew.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(61, 28);
            this.btnnew.TabIndex = 15;
            this.btnnew.Text = "New";
            this.btnnew.UseVisualStyleBackColor = false;
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            // 
            // btnsave
            // 
            this.btnsave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsave.ForeColor = System.Drawing.Color.White;
            this.btnsave.Location = new System.Drawing.Point(132, 1);
            this.btnsave.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(61, 28);
            this.btnsave.TabIndex = 14;
            this.btnsave.Text = "Save";
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btndelete
            // 
            this.btndelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btndelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btndelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btndelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.Location = new System.Drawing.Point(197, 1);
            this.btndelete.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(61, 28);
            this.btndelete.TabIndex = 16;
            this.btndelete.Text = "Delete";
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnclose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnclose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(262, 1);
            this.btnclose.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(61, 28);
            this.btnclose.TabIndex = 17;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(327, 1);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(61, 28);
            this.btnprint.TabIndex = 18;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // btnAddIncomeM
            // 
            this.btnAddIncomeM.BackgroundImage = global::AIOInventorySystem.Desk.Properties.Resources.newbutton;
            this.btnAddIncomeM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddIncomeM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddIncomeM.FlatAppearance.BorderSize = 0;
            this.btnAddIncomeM.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddIncomeM.Location = new System.Drawing.Point(415, 59);
            this.btnAddIncomeM.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnAddIncomeM.Name = "btnAddIncomeM";
            this.btnAddIncomeM.Size = new System.Drawing.Size(27, 22);
            this.btnAddIncomeM.TabIndex = 5;
            this.btnAddIncomeM.UseVisualStyleBackColor = true;
            this.btnAddIncomeM.Click += new System.EventHandler(this.btnAddIncomeM_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(2, 3);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 22);
            this.label10.TabIndex = 21;
            this.label10.Text = "Date:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtchequeno
            // 
            this.txtchequeno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtchequeno.Enabled = false;
            this.txtchequeno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtchequeno.Location = new System.Drawing.Point(567, 86);
            this.txtchequeno.Margin = new System.Windows.Forms.Padding(2);
            this.txtchequeno.Name = "txtchequeno";
            this.txtchequeno.Size = new System.Drawing.Size(266, 25);
            this.txtchequeno.TabIndex = 12;
            this.txtchequeno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtchequeno_KeyDown);
            // 
            // cmbbankname
            // 
            this.cmbbankname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbbankname.DropDownHeight = 110;
            this.cmbbankname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbankname.Enabled = false;
            this.cmbbankname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbbankname.FormattingEnabled = true;
            this.cmbbankname.IntegralHeight = false;
            this.cmbbankname.Location = new System.Drawing.Point(567, 30);
            this.cmbbankname.Margin = new System.Windows.Forms.Padding(2);
            this.cmbbankname.Name = "cmbbankname";
            this.cmbbankname.Size = new System.Drawing.Size(266, 25);
            this.cmbbankname.TabIndex = 10;
            this.cmbbankname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbbankname_KeyDown);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(446, 87);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(117, 22);
            this.label12.TabIndex = 30;
            this.label12.Text = "Cheque No:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label15.Location = new System.Drawing.Point(835, 112);
            this.label15.Margin = new System.Windows.Forms.Padding(0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(67, 28);
            this.label15.TabIndex = 47;
            this.label15.Text = "*";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(446, 115);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(117, 22);
            this.label19.TabIndex = 31;
            this.label19.Text = "Paid Amount:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpchequedate
            // 
            this.dtpchequedate.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtpchequedate.Enabled = false;
            this.dtpchequedate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpchequedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpchequedate.Location = new System.Drawing.Point(567, 58);
            this.dtpchequedate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpchequedate.Name = "dtpchequedate";
            this.dtpchequedate.Size = new System.Drawing.Size(116, 25);
            this.dtpchequedate.TabIndex = 11;
            this.dtpchequedate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpchequedate_KeyDown);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(446, 59);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(117, 22);
            this.label21.TabIndex = 29;
            this.label21.Text = "Cheque Date:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbpaymentmode
            // 
            this.cmbpaymentmode.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbpaymentmode.DropDownHeight = 110;
            this.cmbpaymentmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpaymentmode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbpaymentmode.FormattingEnabled = true;
            this.cmbpaymentmode.IntegralHeight = false;
            this.cmbpaymentmode.Items.AddRange(new object[] {
            "Cash",
            "Cheque",
            "NEFT",
            "RTGS"});
            this.cmbpaymentmode.Location = new System.Drawing.Point(567, 2);
            this.cmbpaymentmode.Margin = new System.Windows.Forms.Padding(2);
            this.cmbpaymentmode.Name = "cmbpaymentmode";
            this.cmbpaymentmode.Size = new System.Drawing.Size(209, 25);
            this.cmbpaymentmode.TabIndex = 9;
            this.cmbpaymentmode.SelectedIndexChanged += new System.EventHandler(this.cmbpaymentmode_SelectedIndexChanged);
            this.cmbpaymentmode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbpaymentmode_KeyDown);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(446, 3);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(117, 22);
            this.label23.TabIndex = 27;
            this.label23.Text = "Payment Mode:";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label24.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label24.Location = new System.Drawing.Point(835, 0);
            this.label24.Margin = new System.Windows.Forms.Padding(0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(67, 28);
            this.label24.TabIndex = 46;
            this.label24.Text = "*";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.Color.Transparent;
            this.label25.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(446, 31);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(117, 22);
            this.label25.TabIndex = 28;
            this.label25.Text = "Bank Name:";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtpaidamount
            // 
            this.txtpaidamount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtpaidamount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpaidamount.Location = new System.Drawing.Point(567, 114);
            this.txtpaidamount.Margin = new System.Windows.Forms.Padding(2);
            this.txtpaidamount.Name = "txtpaidamount";
            this.txtpaidamount.Size = new System.Drawing.Size(266, 25);
            this.txtpaidamount.TabIndex = 13;
            this.txtpaidamount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpaidamount_KeyDown_1);
            this.txtpaidamount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtpaidamount_KeyPress);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.dtpincomedate, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label22, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtexpenceno, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(121, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(270, 28);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dtpincomedate
            // 
            this.dtpincomedate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpincomedate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpincomedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpincomedate.Location = new System.Drawing.Point(2, 2);
            this.dtpincomedate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpincomedate.Name = "dtpincomedate";
            this.dtpincomedate.Size = new System.Drawing.Size(86, 25);
            this.dtpincomedate.TabIndex = 1;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(92, 3);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(86, 22);
            this.label22.TabIndex = 22;
            this.label22.Text = "Income No:";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtexpenceno
            // 
            this.txtexpenceno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtexpenceno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtexpenceno.Location = new System.Drawing.Point(182, 2);
            this.txtexpenceno.Margin = new System.Windows.Forms.Padding(2);
            this.txtexpenceno.Name = "txtexpenceno";
            this.txtexpenceno.ReadOnly = true;
            this.txtexpenceno.Size = new System.Drawing.Size(86, 25);
            this.txtexpenceno.TabIndex = 2;
            this.txtexpenceno.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(2, 31);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 22);
            this.label8.TabIndex = 34;
            this.label8.Text = "Group Name:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbACGroups
            // 
            this.cmbACGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbACGroups.DropDownHeight = 110;
            this.cmbACGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbACGroups.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbACGroups.FormattingEnabled = true;
            this.cmbACGroups.IntegralHeight = false;
            this.cmbACGroups.Items.AddRange(new object[] {
            "Cash",
            "Cheque"});
            this.cmbACGroups.Location = new System.Drawing.Point(123, 30);
            this.cmbACGroups.Margin = new System.Windows.Forms.Padding(2);
            this.cmbACGroups.Name = "cmbACGroups";
            this.cmbACGroups.Size = new System.Drawing.Size(266, 25);
            this.cmbACGroups.TabIndex = 3;
            this.cmbACGroups.SelectedIndexChanged += new System.EventHandler(this.cmbACGroups_SelectedIndexChanged);
            this.cmbACGroups.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbACGroups_KeyDown);
            // 
            // cmbincome
            // 
            this.cmbincome.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbincome.DropDownHeight = 110;
            this.cmbincome.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbincome.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbincome.FormattingEnabled = true;
            this.cmbincome.IntegralHeight = false;
            this.cmbincome.Items.AddRange(new object[] {
            "Cash",
            "Cheque"});
            this.cmbincome.Location = new System.Drawing.Point(123, 58);
            this.cmbincome.Margin = new System.Windows.Forms.Padding(2);
            this.cmbincome.Name = "cmbincome";
            this.cmbincome.Size = new System.Drawing.Size(266, 25);
            this.cmbincome.TabIndex = 4;
            this.cmbincome.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbincome_KeyDown);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label29.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(2, 59);
            this.label29.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(117, 22);
            this.label29.TabIndex = 23;
            this.label29.Text = "Income Name:";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.BackColor = System.Drawing.Color.Transparent;
            this.label28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label28.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(2, 87);
            this.label28.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(117, 22);
            this.label28.TabIndex = 24;
            this.label28.Text = "From:";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtfrom
            // 
            this.txtfrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtfrom.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtfrom.Location = new System.Drawing.Point(123, 86);
            this.txtfrom.Margin = new System.Windows.Forms.Padding(2);
            this.txtfrom.Name = "txtfrom";
            this.txtfrom.Size = new System.Drawing.Size(266, 25);
            this.txtfrom.TabIndex = 6;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label27.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(2, 115);
            this.label27.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(117, 22);
            this.label27.TabIndex = 25;
            this.label27.Text = "To:";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtto
            // 
            this.txtto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtto.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtto.Location = new System.Drawing.Point(123, 114);
            this.txtto.Margin = new System.Windows.Forms.Padding(2);
            this.txtto.Name = "txtto";
            this.txtto.Size = new System.Drawing.Size(266, 25);
            this.txtto.TabIndex = 7;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label26.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(2, 143);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(117, 22);
            this.label26.TabIndex = 26;
            this.label26.Text = "Reason:";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label31.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label31.Location = new System.Drawing.Point(391, 56);
            this.label31.Margin = new System.Windows.Forms.Padding(0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(22, 28);
            this.label31.TabIndex = 44;
            this.label31.Text = "*";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label30.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label30.Location = new System.Drawing.Point(391, 112);
            this.label30.Margin = new System.Windows.Forms.Padding(0);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(22, 28);
            this.label30.TabIndex = 45;
            this.label30.Text = "*";
            // 
            // txtreason
            // 
            this.txtreason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtreason.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtreason.Location = new System.Drawing.Point(123, 142);
            this.txtreason.Margin = new System.Windows.Forms.Padding(2);
            this.txtreason.Multiline = true;
            this.txtreason.Name = "txtreason";
            this.tblltIncomeInfo.SetRowSpan(this.txtreason, 2);
            this.txtreason.Size = new System.Drawing.Size(266, 54);
            this.txtreason.TabIndex = 8;
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.tblltSearch);
            this.grpSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSearch.Location = new System.Drawing.Point(3, 273);
            this.grpSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpSearch.Size = new System.Drawing.Size(908, 305);
            this.grpSearch.TabIndex = 19;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Search Income";
            // 
            // frmOtherIncome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(914, 582);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1164, 1296);
            this.Name = "frmOtherIncome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Other Income";
            this.Load += new System.EventHandler(this.frmOtherIncome_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvexpenceInfo)).EndInit();
            this.tblltSearch.ResumeLayout(false);
            this.tblltSearch.PerformLayout();
            this.tblltMain.ResumeLayout(false);
            this.grpInfo.ResumeLayout(false);
            this.tblltIncomeInfo.ResumeLayout(false);
            this.tblltIncomeInfo.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.grpSearch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DateTimePicker dtptodate;
        private System.Windows.Forms.CheckBox chkdate;
        private System.Windows.Forms.DateTimePicker dtpfromdate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tblltSearch;
        private System.Windows.Forms.CheckBox chkIncome;
        private System.Windows.Forms.ComboBox cmbsearchincome;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltIncomeInfo;
        private System.Windows.Forms.ComboBox cmbACGroups;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtchequeno;
        private System.Windows.Forms.ComboBox cmbbankname;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker dtpchequedate;
        private System.Windows.Forms.DateTimePicker dtpincomedate;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtexpenceno;
        private System.Windows.Forms.ComboBox cmbpaymentmode;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtpaidamount;
        private System.Windows.Forms.TextBox txtreason;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtto;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtfrom;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox cmbincome;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnAddIncomeM;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.DataGridView GvexpenceInfo;
        private System.Windows.Forms.Button btnprintall;
        private System.Windows.Forms.Button btngetall;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn IncomeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACGId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn IncomeNameg;
        private System.Windows.Forms.DataGridViewTextBoxColumn From;
        private System.Windows.Forms.DataGridViewTextBoxColumn To;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reason;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mode;
        private System.Windows.Forms.DataGridViewTextBoxColumn bankname;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChqUTRNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChqNEFTDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaidAmount;
    }
}