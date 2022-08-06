namespace AIOInventorySystem.Desk.Forms
{
    partial class frmJournalEntry
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
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.tblltList = new System.Windows.Forms.TableLayoutPanel();
            this.GvJEInfo = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JENo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FromGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FromAccountName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToAccountName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaidAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbFromAccNameSearch = new System.Windows.Forms.ComboBox();
            this.chkFromAccNameS = new System.Windows.Forms.CheckBox();
            this.cmbFromGrpNameSearch = new System.Windows.Forms.ComboBox();
            this.chkFromGrpNameS = new System.Windows.Forms.CheckBox();
            this.chkdate = new System.Windows.Forms.CheckBox();
            this.dtptodate = new System.Windows.Forms.DateTimePicker();
            this.dtpfromdate = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.tblltListRow2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnprintall = new System.Windows.Forms.Button();
            this.cmbToAccNameSearch = new System.Windows.Forms.ComboBox();
            this.btngetall = new System.Windows.Forms.Button();
            this.chkToAccNameS = new System.Windows.Forms.CheckBox();
            this.btnsearch = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.chkToGrpNameS = new System.Windows.Forms.CheckBox();
            this.cmbToGrpNameSearch = new System.Windows.Forms.ComboBox();
            this.grpJournalEntry = new System.Windows.Forms.GroupBox();
            this.tblltJInfo = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnnew = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtJENo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFromGroupName = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cmbFromAccountName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbToGroupName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbToAccountName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtpaidamount = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.dtpJEdate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.tblltMain.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.tblltList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvJEInfo)).BeginInit();
            this.tblltListRow2.SuspendLayout();
            this.grpJournalEntry.SuspendLayout();
            this.tblltJInfo.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.grpSearch, 0, 2);
            this.tblltMain.Controls.Add(this.grpJournalEntry, 0, 1);
            this.tblltMain.Controls.Add(this.label7, 0, 0);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tblltMain.Size = new System.Drawing.Size(874, 542);
            this.tblltMain.TabIndex = 0;
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.tblltList);
            this.grpSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSearch.Location = new System.Drawing.Point(3, 218);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(868, 321);
            this.grpSearch.TabIndex = 13;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Search Journal Entry";
            // 
            // tblltList
            // 
            this.tblltList.ColumnCount = 8;
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.5F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.25F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.25F));
            this.tblltList.Controls.Add(this.GvJEInfo, 0, 2);
            this.tblltList.Controls.Add(this.cmbFromAccNameSearch, 7, 0);
            this.tblltList.Controls.Add(this.chkFromAccNameS, 6, 0);
            this.tblltList.Controls.Add(this.cmbFromGrpNameSearch, 5, 0);
            this.tblltList.Controls.Add(this.chkFromGrpNameS, 4, 0);
            this.tblltList.Controls.Add(this.chkdate, 0, 0);
            this.tblltList.Controls.Add(this.dtptodate, 3, 0);
            this.tblltList.Controls.Add(this.dtpfromdate, 1, 0);
            this.tblltList.Controls.Add(this.label18, 2, 0);
            this.tblltList.Controls.Add(this.tblltListRow2, 0, 1);
            this.tblltList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltList.Location = new System.Drawing.Point(3, 21);
            this.tblltList.Name = "tblltList";
            this.tblltList.RowCount = 3;
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tblltList.Size = new System.Drawing.Size(862, 297);
            this.tblltList.TabIndex = 13;
            // 
            // GvJEInfo
            // 
            this.GvJEInfo.AllowUserToAddRows = false;
            this.GvJEInfo.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvJEInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvJEInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvJEInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.JENo,
            this.Date,
            this.FromGroup,
            this.FromAccountName,
            this.ToGroup,
            this.ToAccountName,
            this.PaidAmt});
            this.tblltList.SetColumnSpan(this.GvJEInfo, 8);
            this.GvJEInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvJEInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvJEInfo.Location = new System.Drawing.Point(2, 60);
            this.GvJEInfo.Margin = new System.Windows.Forms.Padding(2);
            this.GvJEInfo.Name = "GvJEInfo";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvJEInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GvJEInfo.RowHeadersWidth = 15;
            this.GvJEInfo.Size = new System.Drawing.Size(858, 235);
            this.GvJEInfo.TabIndex = 27;
            this.GvJEInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvJEInfo_CellClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // JENo
            // 
            this.JENo.HeaderText = "JE No";
            this.JENo.Name = "JENo";
            this.JENo.Width = 65;
            // 
            // Date
            // 
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            // 
            // FromGroup
            // 
            this.FromGroup.HeaderText = "From Group";
            this.FromGroup.Name = "FromGroup";
            this.FromGroup.Width = 130;
            // 
            // FromAccountName
            // 
            this.FromAccountName.HeaderText = "From Account Name";
            this.FromAccountName.Name = "FromAccountName";
            this.FromAccountName.Width = 160;
            // 
            // ToGroup
            // 
            this.ToGroup.HeaderText = "To Group";
            this.ToGroup.Name = "ToGroup";
            this.ToGroup.Width = 130;
            // 
            // ToAccountName
            // 
            this.ToAccountName.HeaderText = "To Account Name";
            this.ToAccountName.Name = "ToAccountName";
            this.ToAccountName.Width = 150;
            // 
            // PaidAmt
            // 
            this.PaidAmt.HeaderText = "Paid Amount";
            this.PaidAmt.Name = "PaidAmt";
            this.PaidAmt.Width = 110;
            // 
            // cmbFromAccNameSearch
            // 
            this.cmbFromAccNameSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbFromAccNameSearch.DropDownHeight = 110;
            this.cmbFromAccNameSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFromAccNameSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFromAccNameSearch.FormattingEnabled = true;
            this.cmbFromAccNameSearch.IntegralHeight = false;
            this.cmbFromAccNameSearch.Location = new System.Drawing.Point(721, 2);
            this.cmbFromAccNameSearch.Margin = new System.Windows.Forms.Padding(2);
            this.cmbFromAccNameSearch.Name = "cmbFromAccNameSearch";
            this.cmbFromAccNameSearch.Size = new System.Drawing.Size(139, 25);
            this.cmbFromAccNameSearch.TabIndex = 19;
            this.cmbFromAccNameSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFromAccNameSearch_KeyDown);
            // 
            // chkFromAccNameS
            // 
            this.chkFromAccNameS.AutoSize = true;
            this.chkFromAccNameS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkFromAccNameS.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFromAccNameS.Location = new System.Drawing.Point(594, 2);
            this.chkFromAccNameS.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkFromAccNameS.Name = "chkFromAccNameS";
            this.chkFromAccNameS.Size = new System.Drawing.Size(123, 25);
            this.chkFromAccNameS.TabIndex = 18;
            this.chkFromAccNameS.Text = "From AC Name";
            this.chkFromAccNameS.UseVisualStyleBackColor = true;
            this.chkFromAccNameS.CheckedChanged += new System.EventHandler(this.chkFromAccNameS_CheckedChanged);
            this.chkFromAccNameS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkFromAccNameS_KeyDown);
            // 
            // cmbFromGrpNameSearch
            // 
            this.cmbFromGrpNameSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbFromGrpNameSearch.DropDownHeight = 110;
            this.cmbFromGrpNameSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFromGrpNameSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFromGrpNameSearch.FormattingEnabled = true;
            this.cmbFromGrpNameSearch.IntegralHeight = false;
            this.cmbFromGrpNameSearch.Items.AddRange(new object[] {
            "Supplier",
            "Customer",
            "Expence",
            "Other Income",
            "PL Account"});
            this.cmbFromGrpNameSearch.Location = new System.Drawing.Point(452, 2);
            this.cmbFromGrpNameSearch.Margin = new System.Windows.Forms.Padding(2);
            this.cmbFromGrpNameSearch.Name = "cmbFromGrpNameSearch";
            this.cmbFromGrpNameSearch.Size = new System.Drawing.Size(136, 25);
            this.cmbFromGrpNameSearch.TabIndex = 17;
            this.cmbFromGrpNameSearch.SelectedIndexChanged += new System.EventHandler(this.cmbFromGrpNameSearch_SelectedIndexChanged);
            this.cmbFromGrpNameSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFromGroupname1_KeyDown);
            // 
            // chkFromGrpNameS
            // 
            this.chkFromGrpNameS.AutoSize = true;
            this.chkFromGrpNameS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkFromGrpNameS.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFromGrpNameS.Location = new System.Drawing.Point(325, 2);
            this.chkFromGrpNameS.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkFromGrpNameS.Name = "chkFromGrpNameS";
            this.chkFromGrpNameS.Size = new System.Drawing.Size(123, 25);
            this.chkFromGrpNameS.TabIndex = 16;
            this.chkFromGrpNameS.Text = "From Grp Name";
            this.chkFromGrpNameS.UseVisualStyleBackColor = true;
            this.chkFromGrpNameS.CheckedChanged += new System.EventHandler(this.chkFromgroupName_CheckedChanged);
            this.chkFromGrpNameS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkFromgroupName_KeyDown);
            // 
            // chkdate
            // 
            this.chkdate.AutoSize = true;
            this.chkdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkdate.Location = new System.Drawing.Point(4, 2);
            this.chkdate.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkdate.Name = "chkdate";
            this.chkdate.Size = new System.Drawing.Size(93, 25);
            this.chkdate.TabIndex = 13;
            this.chkdate.Text = "From Date";
            this.chkdate.UseVisualStyleBackColor = true;
            this.chkdate.CheckedChanged += new System.EventHandler(this.chkdate_CheckedChanged);
            this.chkdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkdate_KeyDown);
            // 
            // dtptodate
            // 
            this.dtptodate.CustomFormat = "dd/MM/yyyy";
            this.dtptodate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtptodate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtptodate.Location = new System.Drawing.Point(229, 2);
            this.dtptodate.Margin = new System.Windows.Forms.Padding(2);
            this.dtptodate.Name = "dtptodate";
            this.dtptodate.Size = new System.Drawing.Size(90, 25);
            this.dtptodate.TabIndex = 15;
            this.dtptodate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtptodate_KeyDown);
            // 
            // dtpfromdate
            // 
            this.dtpfromdate.CustomFormat = "dd/MM/yyyy";
            this.dtpfromdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpfromdate.Location = new System.Drawing.Point(101, 2);
            this.dtpfromdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpfromdate.Name = "dtpfromdate";
            this.dtpfromdate.Size = new System.Drawing.Size(90, 25);
            this.dtpfromdate.TabIndex = 14;
            this.dtpfromdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpfromdate_KeyDown);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(195, 2);
            this.label18.Margin = new System.Windows.Forms.Padding(2);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(30, 25);
            this.label18.TabIndex = 33;
            this.label18.Text = "To";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltListRow2
            // 
            this.tblltListRow2.ColumnCount = 9;
            this.tblltList.SetColumnSpan(this.tblltListRow2, 8);
            this.tblltListRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltListRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.5F));
            this.tblltListRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltListRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.5F));
            this.tblltListRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltListRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltListRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltListRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltListRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltListRow2.Controls.Add(this.btnprintall, 8, 0);
            this.tblltListRow2.Controls.Add(this.cmbToAccNameSearch, 3, 0);
            this.tblltListRow2.Controls.Add(this.btngetall, 7, 0);
            this.tblltListRow2.Controls.Add(this.chkToAccNameS, 2, 0);
            this.tblltListRow2.Controls.Add(this.btnsearch, 6, 0);
            this.tblltListRow2.Controls.Add(this.label19, 4, 0);
            this.tblltListRow2.Controls.Add(this.label13, 5, 0);
            this.tblltListRow2.Controls.Add(this.chkToGrpNameS, 0, 0);
            this.tblltListRow2.Controls.Add(this.cmbToGrpNameSearch, 1, 0);
            this.tblltListRow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltListRow2.Location = new System.Drawing.Point(0, 29);
            this.tblltListRow2.Margin = new System.Windows.Forms.Padding(0);
            this.tblltListRow2.Name = "tblltListRow2";
            this.tblltListRow2.RowCount = 1;
            this.tblltListRow2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltListRow2.Size = new System.Drawing.Size(862, 29);
            this.tblltListRow2.TabIndex = 20;
            // 
            // btnprintall
            // 
            this.btnprintall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprintall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprintall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnprintall.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprintall.ForeColor = System.Drawing.Color.White;
            this.btnprintall.Location = new System.Drawing.Point(800, 2);
            this.btnprintall.Margin = new System.Windows.Forms.Padding(2);
            this.btnprintall.Name = "btnprintall";
            this.btnprintall.Size = new System.Drawing.Size(60, 25);
            this.btnprintall.TabIndex = 26;
            this.btnprintall.Text = "Print";
            this.btnprintall.UseVisualStyleBackColor = false;
            this.btnprintall.Click += new System.EventHandler(this.btnprintall_Click);
            // 
            // cmbToAccNameSearch
            // 
            this.cmbToAccNameSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbToAccNameSearch.DropDownHeight = 110;
            this.cmbToAccNameSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToAccNameSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbToAccNameSearch.FormattingEnabled = true;
            this.cmbToAccNameSearch.IntegralHeight = false;
            this.cmbToAccNameSearch.Location = new System.Drawing.Point(367, 2);
            this.cmbToAccNameSearch.Margin = new System.Windows.Forms.Padding(2);
            this.cmbToAccNameSearch.Name = "cmbToAccNameSearch";
            this.cmbToAccNameSearch.Size = new System.Drawing.Size(129, 25);
            this.cmbToAccNameSearch.TabIndex = 23;
            this.cmbToAccNameSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbToAccNameSearch_KeyDown);
            // 
            // btngetall
            // 
            this.btngetall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btngetall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btngetall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btngetall.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngetall.ForeColor = System.Drawing.Color.White;
            this.btngetall.Location = new System.Drawing.Point(732, 2);
            this.btngetall.Margin = new System.Windows.Forms.Padding(2);
            this.btngetall.Name = "btngetall";
            this.btngetall.Size = new System.Drawing.Size(64, 25);
            this.btngetall.TabIndex = 25;
            this.btngetall.Text = "Get All";
            this.btngetall.UseVisualStyleBackColor = false;
            this.btngetall.Click += new System.EventHandler(this.btngetall_Click);
            // 
            // chkToAccNameS
            // 
            this.chkToAccNameS.AutoSize = true;
            this.chkToAccNameS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkToAccNameS.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkToAccNameS.Location = new System.Drawing.Point(257, 2);
            this.chkToAccNameS.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkToAccNameS.Name = "chkToAccNameS";
            this.chkToAccNameS.Size = new System.Drawing.Size(106, 25);
            this.chkToAccNameS.TabIndex = 22;
            this.chkToAccNameS.Text = "To AC Name";
            this.chkToAccNameS.UseVisualStyleBackColor = true;
            this.chkToAccNameS.CheckedChanged += new System.EventHandler(this.chkToAccNameS_CheckedChanged);
            this.chkToAccNameS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkToAccNameS_KeyDown);
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(672, 2);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(56, 25);
            this.btnsearch.TabIndex = 24;
            this.btnsearch.Text = "Search";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label19.Location = new System.Drawing.Point(500, 2);
            this.label19.Margin = new System.Windows.Forms.Padding(2);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(82, 25);
            this.label19.TabIndex = 79;
            this.label19.Text = "Total Amt:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label13.Location = new System.Drawing.Point(586, 2);
            this.label13.Margin = new System.Windows.Forms.Padding(2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 25);
            this.label13.TabIndex = 84;
            this.label13.Text = "0";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkToGrpNameS
            // 
            this.chkToGrpNameS.AutoSize = true;
            this.chkToGrpNameS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkToGrpNameS.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkToGrpNameS.Location = new System.Drawing.Point(4, 2);
            this.chkToGrpNameS.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkToGrpNameS.Name = "chkToGrpNameS";
            this.chkToGrpNameS.Size = new System.Drawing.Size(114, 25);
            this.chkToGrpNameS.TabIndex = 20;
            this.chkToGrpNameS.Text = "To Grp Name";
            this.chkToGrpNameS.UseVisualStyleBackColor = true;
            this.chkToGrpNameS.CheckedChanged += new System.EventHandler(this.chktogroupname_CheckedChanged);
            this.chkToGrpNameS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chktogroupname_KeyDown);
            // 
            // cmbToGrpNameSearch
            // 
            this.cmbToGrpNameSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbToGrpNameSearch.DropDownHeight = 110;
            this.cmbToGrpNameSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToGrpNameSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbToGrpNameSearch.FormattingEnabled = true;
            this.cmbToGrpNameSearch.IntegralHeight = false;
            this.cmbToGrpNameSearch.Items.AddRange(new object[] {
            "Supplier",
            "Customer",
            "Expence",
            "Other Income",
            "Proprietor"});
            this.cmbToGrpNameSearch.Location = new System.Drawing.Point(122, 2);
            this.cmbToGrpNameSearch.Margin = new System.Windows.Forms.Padding(2);
            this.cmbToGrpNameSearch.Name = "cmbToGrpNameSearch";
            this.cmbToGrpNameSearch.Size = new System.Drawing.Size(129, 25);
            this.cmbToGrpNameSearch.TabIndex = 21;
            this.cmbToGrpNameSearch.SelectedIndexChanged += new System.EventHandler(this.cmbToGrpNameSearch_SelectedIndexChanged);
            this.cmbToGrpNameSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbToGroupname1_KeyDown);
            // 
            // grpJournalEntry
            // 
            this.grpJournalEntry.Controls.Add(this.tblltJInfo);
            this.grpJournalEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpJournalEntry.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpJournalEntry.Location = new System.Drawing.Point(3, 41);
            this.grpJournalEntry.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpJournalEntry.Name = "grpJournalEntry";
            this.grpJournalEntry.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpJournalEntry.Size = new System.Drawing.Size(868, 170);
            this.grpJournalEntry.TabIndex = 0;
            this.grpJournalEntry.TabStop = false;
            this.grpJournalEntry.Text = "Journal Entry";
            // 
            // tblltJInfo
            // 
            this.tblltJInfo.ColumnCount = 5;
            this.tblltJInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltJInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.5F));
            this.tblltJInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltJInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.5F));
            this.tblltJInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltJInfo.Controls.Add(this.tblltButtons, 0, 4);
            this.tblltJInfo.Controls.Add(this.label5, 2, 0);
            this.tblltJInfo.Controls.Add(this.txtJENo, 3, 0);
            this.tblltJInfo.Controls.Add(this.label6, 0, 0);
            this.tblltJInfo.Controls.Add(this.label3, 0, 1);
            this.tblltJInfo.Controls.Add(this.cmbFromGroupName, 1, 1);
            this.tblltJInfo.Controls.Add(this.label20, 2, 1);
            this.tblltJInfo.Controls.Add(this.cmbFromAccountName, 3, 1);
            this.tblltJInfo.Controls.Add(this.label4, 0, 2);
            this.tblltJInfo.Controls.Add(this.cmbToGroupName, 1, 2);
            this.tblltJInfo.Controls.Add(this.label2, 2, 2);
            this.tblltJInfo.Controls.Add(this.cmbToAccountName, 3, 2);
            this.tblltJInfo.Controls.Add(this.label1, 0, 3);
            this.tblltJInfo.Controls.Add(this.txtpaidamount, 1, 3);
            this.tblltJInfo.Controls.Add(this.dtpJEdate, 1, 0);
            this.tblltJInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltJInfo.Location = new System.Drawing.Point(3, 22);
            this.tblltJInfo.Name = "tblltJInfo";
            this.tblltJInfo.RowCount = 5;
            this.tblltJInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.25F));
            this.tblltJInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.25F));
            this.tblltJInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.25F));
            this.tblltJInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.25F));
            this.tblltJInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tblltJInfo.Size = new System.Drawing.Size(862, 144);
            this.tblltJInfo.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 7;
            this.tblltJInfo.SetColumnSpan(this.tblltButtons, 5);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltButtons.Controls.Add(this.btnclose, 4, 0);
            this.tblltButtons.Controls.Add(this.btnnew, 1, 0);
            this.tblltButtons.Controls.Add(this.btndelete, 3, 0);
            this.tblltButtons.Controls.Add(this.btnsave, 2, 0);
            this.tblltButtons.Controls.Add(this.btnprint, 5, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 108);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(862, 36);
            this.tblltButtons.TabIndex = 8;
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnclose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnclose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(475, 2);
            this.btnclose.Margin = new System.Windows.Forms.Padding(2);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(82, 32);
            this.btnclose.TabIndex = 11;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(217, 2);
            this.btnnew.Margin = new System.Windows.Forms.Padding(2);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(82, 32);
            this.btnnew.TabIndex = 9;
            this.btnnew.Text = "New";
            this.btnnew.UseVisualStyleBackColor = false;
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            // 
            // btndelete
            // 
            this.btndelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btndelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btndelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btndelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.Location = new System.Drawing.Point(389, 2);
            this.btndelete.Margin = new System.Windows.Forms.Padding(2);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(82, 32);
            this.btndelete.TabIndex = 10;
            this.btndelete.Text = "Delete";
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // btnsave
            // 
            this.btnsave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsave.ForeColor = System.Drawing.Color.White;
            this.btnsave.Location = new System.Drawing.Point(303, 2);
            this.btnsave.Margin = new System.Windows.Forms.Padding(2);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(82, 32);
            this.btnsave.TabIndex = 8;
            this.btnsave.Text = "Save";
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(561, 2);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(82, 32);
            this.btnprint.TabIndex = 12;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(411, 2);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(151, 23);
            this.label5.TabIndex = 22;
            this.label5.Text = "Journal Entry No:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtJENo
            // 
            this.txtJENo.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtJENo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJENo.Location = new System.Drawing.Point(566, 2);
            this.txtJENo.Margin = new System.Windows.Forms.Padding(2);
            this.txtJENo.Name = "txtJENo";
            this.txtJENo.ReadOnly = true;
            this.txtJENo.Size = new System.Drawing.Size(83, 25);
            this.txtJENo.TabIndex = 2;
            this.txtJENo.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(2, 2);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(151, 23);
            this.label6.TabIndex = 21;
            this.label6.Text = "Date:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 23);
            this.label3.TabIndex = 38;
            this.label3.Text = "From Account Group:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbFromGroupName
            // 
            this.cmbFromGroupName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbFromGroupName.DropDownHeight = 110;
            this.cmbFromGroupName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFromGroupName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFromGroupName.FormattingEnabled = true;
            this.cmbFromGroupName.IntegralHeight = false;
            this.cmbFromGroupName.Items.AddRange(new object[] {
            "Supplier",
            "Customer",
            "Expence",
            "Other Income",
            "PL Account",
            "Bank Account"});
            this.cmbFromGroupName.Location = new System.Drawing.Point(157, 29);
            this.cmbFromGroupName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbFromGroupName.Name = "cmbFromGroupName";
            this.cmbFromGroupName.Size = new System.Drawing.Size(250, 25);
            this.cmbFromGroupName.TabIndex = 3;
            this.cmbFromGroupName.SelectedIndexChanged += new System.EventHandler(this.cmbaccountFrom1_SelectedIndexChanged);
            this.cmbFromGroupName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbaccountFrom1_KeyDown);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(411, 29);
            this.label20.Margin = new System.Windows.Forms.Padding(2);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(151, 23);
            this.label20.TabIndex = 34;
            this.label20.Text = "From Account Name:";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbFromAccountName
            // 
            this.cmbFromAccountName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbFromAccountName.DropDownHeight = 110;
            this.cmbFromAccountName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFromAccountName.Enabled = false;
            this.cmbFromAccountName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFromAccountName.FormattingEnabled = true;
            this.cmbFromAccountName.IntegralHeight = false;
            this.cmbFromAccountName.Location = new System.Drawing.Point(566, 29);
            this.cmbFromAccountName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbFromAccountName.Name = "cmbFromAccountName";
            this.cmbFromAccountName.Size = new System.Drawing.Size(250, 25);
            this.cmbFromAccountName.TabIndex = 4;
            this.cmbFromAccountName.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.cmbFromAccountName_PreviewKeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(2, 56);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 23);
            this.label4.TabIndex = 39;
            this.label4.Text = "To Group:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbToGroupName
            // 
            this.cmbToGroupName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbToGroupName.DropDownHeight = 110;
            this.cmbToGroupName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToGroupName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbToGroupName.FormattingEnabled = true;
            this.cmbToGroupName.IntegralHeight = false;
            this.cmbToGroupName.Items.AddRange(new object[] {
            "Supplier",
            "Customer",
            "Expence",
            "Other Income",
            "Proprietor",
            "Bank Account"});
            this.cmbToGroupName.Location = new System.Drawing.Point(157, 56);
            this.cmbToGroupName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbToGroupName.Name = "cmbToGroupName";
            this.cmbToGroupName.Size = new System.Drawing.Size(250, 25);
            this.cmbToGroupName.TabIndex = 5;
            this.cmbToGroupName.SelectedIndexChanged += new System.EventHandler(this.cmbaccountfrom2_SelectedIndexChanged);
            this.cmbToGroupName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbaccountfrom2_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(411, 56);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 23);
            this.label2.TabIndex = 35;
            this.label2.Text = "To Account Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbToAccountName
            // 
            this.cmbToAccountName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbToAccountName.DropDownHeight = 110;
            this.cmbToAccountName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToAccountName.Enabled = false;
            this.cmbToAccountName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbToAccountName.FormattingEnabled = true;
            this.cmbToAccountName.IntegralHeight = false;
            this.cmbToAccountName.Location = new System.Drawing.Point(566, 56);
            this.cmbToAccountName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbToAccountName.Name = "cmbToAccountName";
            this.cmbToAccountName.Size = new System.Drawing.Size(250, 25);
            this.cmbToAccountName.TabIndex = 6;
            this.cmbToAccountName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbToACGroups_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 83);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 23);
            this.label1.TabIndex = 31;
            this.label1.Text = "Paid Amount:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtpaidamount
            // 
            this.txtpaidamount.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtpaidamount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpaidamount.Location = new System.Drawing.Point(157, 83);
            this.txtpaidamount.Margin = new System.Windows.Forms.Padding(2);
            this.txtpaidamount.Name = "txtpaidamount";
            this.txtpaidamount.Size = new System.Drawing.Size(120, 25);
            this.txtpaidamount.TabIndex = 7;
            this.txtpaidamount.TextChanged += new System.EventHandler(this.txtpaidamount_TextChanged);
            this.txtpaidamount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpaidamount_KeyDown);
            // 
            // dtpJEdate
            // 
            this.dtpJEdate.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtpJEdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpJEdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpJEdate.Location = new System.Drawing.Point(157, 2);
            this.dtpJEdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpJEdate.Name = "dtpJEdate";
            this.dtpJEdate.Size = new System.Drawing.Size(120, 25);
            this.dtpJEdate.TabIndex = 1;
            this.dtpJEdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpJEdate_KeyDown);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(874, 37);
            this.label7.TabIndex = 36;
            this.label7.Text = "Journal Entry";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmJournalEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(874, 542);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmJournalEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Journal Entry";
            this.tblltMain.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.tblltList.ResumeLayout(false);
            this.tblltList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvJEInfo)).EndInit();
            this.tblltListRow2.ResumeLayout(false);
            this.tblltListRow2.PerformLayout();
            this.grpJournalEntry.ResumeLayout(false);
            this.tblltJInfo.ResumeLayout(false);
            this.tblltJInfo.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltJInfo;
        private System.Windows.Forms.ComboBox cmbFromAccountName;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpJEdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtJENo;
        private System.Windows.Forms.TableLayoutPanel tblltList;
        private System.Windows.Forms.CheckBox chkdate;
        private System.Windows.Forms.DateTimePicker dtptodate;
        private System.Windows.Forms.DateTimePicker dtpfromdate;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox chkFromGrpNameS;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cmbFromGrpNameSearch;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbToAccountName;
        private System.Windows.Forms.CheckBox chkToGrpNameS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbFromGroupName;
        private System.Windows.Forms.ComboBox cmbToGroupName;
        private System.Windows.Forms.ComboBox cmbToGrpNameSearch;
        private System.Windows.Forms.ComboBox cmbFromAccNameSearch;
        private System.Windows.Forms.CheckBox chkFromAccNameS;
        private System.Windows.Forms.TableLayoutPanel tblltListRow2;
        private System.Windows.Forms.ComboBox cmbToAccNameSearch;
        private System.Windows.Forms.CheckBox chkToAccNameS;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btnprint;
        private RachitControls.NumericTextBox txtpaidamount;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Button btngetall;
        private System.Windows.Forms.Button btnprintall;
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.GroupBox grpJournalEntry;
        private System.Windows.Forms.DataGridView GvJEInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn JENo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn FromGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn FromAccountName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToAccountName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaidAmt;
    }
}