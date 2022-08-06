namespace AIOInventorySystem.Desk.Forms
{
    partial class frmQuotationList
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
            this.cmbcustomername = new System.Windows.Forms.ComboBox();
            this.dtpfromdate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtptodate = new System.Windows.Forms.DateTimePicker();
            this.lblTotalBill = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btntodaylist = new System.Windows.Forms.Button();
            this.rbtnDeleteMultipleBill = new System.Windows.Forms.Button();
            this.btnsearch = new System.Windows.Forms.Button();
            this.btngetall = new System.Windows.Forms.Button();
            this.chkbillno = new System.Windows.Forms.CheckBox();
            this.chkcompletedc = new System.Windows.Forms.CheckBox();
            this.chkQuoNo = new System.Windows.Forms.CheckBox();
            this.chkfromdate = new System.Windows.Forms.CheckBox();
            this.GvBillInfo = new System.Windows.Forms.DataGridView();
            this.Selectg = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Updateg = new System.Windows.Forms.DataGridViewImageColumn();
            this.CustBill = new System.Windows.Forms.DataGridViewImageColumn();
            this.Duplicate = new System.Windows.Forms.DataGridViewLinkColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.txtquotationNo = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvBillInfo)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbcustomername
            // 
            this.cmbcustomername.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcustomername.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcustomername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcustomername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustomername.FormattingEnabled = true;
            this.cmbcustomername.Location = new System.Drawing.Point(2, 31);
            this.cmbcustomername.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustomername.Name = "cmbcustomername";
            this.cmbcustomername.Size = new System.Drawing.Size(172, 29);
            this.cmbcustomername.TabIndex = 2;
            this.cmbcustomername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcustomername_KeyDown);
            this.cmbcustomername.Leave += new System.EventHandler(this.cmbcustomername_Leave);
            // 
            // dtpfromdate
            // 
            this.dtpfromdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfromdate.Location = new System.Drawing.Point(178, 31);
            this.dtpfromdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpfromdate.Name = "dtpfromdate";
            this.dtpfromdate.Size = new System.Drawing.Size(93, 29);
            this.dtpfromdate.TabIndex = 4;
            this.dtpfromdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpfromdate_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(275, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 25);
            this.label2.TabIndex = 54;
            this.label2.Text = "To";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtptodate
            // 
            this.dtptodate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtptodate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtptodate.Location = new System.Drawing.Point(310, 31);
            this.dtptodate.Margin = new System.Windows.Forms.Padding(2);
            this.dtptodate.Name = "dtptodate";
            this.dtptodate.Size = new System.Drawing.Size(93, 29);
            this.dtptodate.TabIndex = 5;
            this.dtptodate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtptodate_KeyDown);
            // 
            // lblTotalBill
            // 
            this.lblTotalBill.AutoSize = true;
            this.lblTotalBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalBill.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBill.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalBill.Location = new System.Drawing.Point(609, 2);
            this.lblTotalBill.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotalBill.Name = "lblTotalBill";
            this.lblTotalBill.Size = new System.Drawing.Size(84, 25);
            this.lblTotalBill.TabIndex = 91;
            this.lblTotalBill.Text = "0";
            this.lblTotalBill.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(521, 2);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 25);
            this.label8.TabIndex = 90;
            this.label8.Text = "Total Bills:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21F));
            this.tableLayoutPanel1.Controls.Add(this.tblltButtons, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.label8, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbcustomername, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dtpfromdate, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.dtptodate, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.txtquotationNo, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblTotalBill, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkbillno, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkcompletedc, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkQuoNo, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkfromdate, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 38);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(884, 58);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.TabStop = true;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 4;
            this.tableLayoutPanel1.SetColumnSpan(this.tblltButtons, 3);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.16418F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.87065F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.16418F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.80099F));
            this.tblltButtons.Controls.Add(this.btntodaylist, 1, 0);
            this.tblltButtons.Controls.Add(this.rbtnDeleteMultipleBill, 3, 0);
            this.tblltButtons.Controls.Add(this.btnsearch, 0, 0);
            this.tblltButtons.Controls.Add(this.btngetall, 2, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(519, 29);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(365, 29);
            this.tblltButtons.TabIndex = 200;
            // 
            // btntodaylist
            // 
            this.btntodaylist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btntodaylist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btntodaylist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btntodaylist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntodaylist.ForeColor = System.Drawing.Color.White;
            this.btntodaylist.Location = new System.Drawing.Point(64, 1);
            this.btntodaylist.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btntodaylist.Name = "btntodaylist";
            this.btntodaylist.Size = new System.Drawing.Size(90, 27);
            this.btntodaylist.TabIndex = 9;
            this.btntodaylist.Text = "Today\'s List";
            this.btntodaylist.UseVisualStyleBackColor = false;
            this.btntodaylist.Click += new System.EventHandler(this.btntodaylist_Click);
            // 
            // rbtnDeleteMultipleBill
            // 
            this.rbtnDeleteMultipleBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.rbtnDeleteMultipleBill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.rbtnDeleteMultipleBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtnDeleteMultipleBill.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnDeleteMultipleBill.ForeColor = System.Drawing.Color.White;
            this.rbtnDeleteMultipleBill.Location = new System.Drawing.Point(220, 1);
            this.rbtnDeleteMultipleBill.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.rbtnDeleteMultipleBill.Name = "rbtnDeleteMultipleBill";
            this.rbtnDeleteMultipleBill.Size = new System.Drawing.Size(143, 27);
            this.rbtnDeleteMultipleBill.TabIndex = 11;
            this.rbtnDeleteMultipleBill.Text = "Delete Multiple Bills";
            this.rbtnDeleteMultipleBill.UseVisualStyleBackColor = false;
            this.rbtnDeleteMultipleBill.Click += new System.EventHandler(this.rbtnDeleteMultipleBill_Click);
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(2, 1);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(58, 27);
            this.btnsearch.TabIndex = 8;
            this.btnsearch.Text = "Search";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // btngetall
            // 
            this.btngetall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btngetall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btngetall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btngetall.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngetall.ForeColor = System.Drawing.Color.White;
            this.btngetall.Location = new System.Drawing.Point(158, 1);
            this.btngetall.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btngetall.Name = "btngetall";
            this.btngetall.Size = new System.Drawing.Size(58, 27);
            this.btngetall.TabIndex = 10;
            this.btngetall.Text = "Get All";
            this.btngetall.UseVisualStyleBackColor = false;
            this.btngetall.Click += new System.EventHandler(this.btngetall_Click);
            // 
            // chkbillno
            // 
            this.chkbillno.AutoSize = true;
            this.chkbillno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkbillno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbillno.Location = new System.Drawing.Point(5, 2);
            this.chkbillno.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkbillno.Name = "chkbillno";
            this.chkbillno.Size = new System.Drawing.Size(169, 25);
            this.chkbillno.TabIndex = 1;
            this.chkbillno.Text = "Customer Name";
            this.chkbillno.UseVisualStyleBackColor = true;
            this.chkbillno.CheckedChanged += new System.EventHandler(this.chkbillno_CheckedChanged);
            this.chkbillno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkbillno_KeyDown);
            // 
            // chkcompletedc
            // 
            this.chkcompletedc.AutoSize = true;
            this.chkcompletedc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkcompletedc.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcompletedc.ForeColor = System.Drawing.Color.Crimson;
            this.chkcompletedc.Location = new System.Drawing.Point(699, 2);
            this.chkcompletedc.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkcompletedc.Name = "chkcompletedc";
            this.chkcompletedc.Size = new System.Drawing.Size(183, 25);
            this.chkcompletedc.TabIndex = 12;
            this.chkcompletedc.Text = "Completed Quotations";
            this.chkcompletedc.UseVisualStyleBackColor = true;
            this.chkcompletedc.CheckedChanged += new System.EventHandler(this.chkcompletedc_CheckedChanged);
            // 
            // chkQuoNo
            // 
            this.chkQuoNo.AutoSize = true;
            this.chkQuoNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkQuoNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkQuoNo.Location = new System.Drawing.Point(410, 2);
            this.chkQuoNo.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkQuoNo.Name = "chkQuoNo";
            this.chkQuoNo.Size = new System.Drawing.Size(107, 25);
            this.chkQuoNo.TabIndex = 6;
            this.chkQuoNo.Text = "Quotation No";
            this.chkQuoNo.UseVisualStyleBackColor = true;
            this.chkQuoNo.CheckedChanged += new System.EventHandler(this.chkQuoNo_CheckedChanged);
            this.chkQuoNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkQuoNo_KeyDown);
            // 
            // chkfromdate
            // 
            this.chkfromdate.AutoSize = true;
            this.chkfromdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkfromdate.Location = new System.Drawing.Point(181, 2);
            this.chkfromdate.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkfromdate.Name = "chkfromdate";
            this.chkfromdate.Size = new System.Drawing.Size(90, 25);
            this.chkfromdate.TabIndex = 3;
            this.chkfromdate.Text = "From Date";
            this.chkfromdate.UseVisualStyleBackColor = true;
            this.chkfromdate.CheckedChanged += new System.EventHandler(this.chkfromdate_CheckedChanged);
            this.chkfromdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkfromdate_KeyDown);
            // 
            // GvBillInfo
            // 
            this.GvBillInfo.AllowUserToAddRows = false;
            this.GvBillInfo.AllowUserToDeleteRows = false;
            this.GvBillInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvBillInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvBillInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GvBillInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvBillInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selectg,
            this.Updateg,
            this.CustBill,
            this.Duplicate});
            this.GvBillInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvBillInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvBillInfo.Location = new System.Drawing.Point(3, 99);
            this.GvBillInfo.Name = "GvBillInfo";
            this.GvBillInfo.RowHeadersWidth = 15;
            this.GvBillInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvBillInfo.Size = new System.Drawing.Size(878, 410);
            this.GvBillInfo.TabIndex = 13;
            this.GvBillInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvBillInfo_CellClick);
            this.GvBillInfo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GvBillInfo_KeyPress);
            // 
            // Selectg
            // 
            this.Selectg.FillWeight = 46.5729F;
            this.Selectg.HeaderText = "Select";
            this.Selectg.Name = "Selectg";
            // 
            // Updateg
            // 
            this.Updateg.FillWeight = 75.9835F;
            this.Updateg.HeaderText = "Update";
            this.Updateg.Image = global::AIOInventorySystem.Desk.Properties.Resources.Update;
            this.Updateg.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Updateg.Name = "Updateg";
            // 
            // CustBill
            // 
            this.CustBill.FillWeight = 115.0071F;
            this.CustBill.HeaderText = "Customer Bill";
            this.CustBill.Image = global::AIOInventorySystem.Desk.Properties.Resources.make_bill1;
            this.CustBill.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.CustBill.Name = "CustBill";
            // 
            // Duplicate
            // 
            this.Duplicate.FillWeight = 162.4366F;
            this.Duplicate.HeaderText = "Make Duplicate";
            this.Duplicate.Name = "Duplicate";
            this.Duplicate.Text = "Make Duplicate";
            this.Duplicate.UseColumnTextForLinkValue = true;
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
            this.label5.Size = new System.Drawing.Size(884, 38);
            this.label5.TabIndex = 11;
            this.label5.Text = "Quotation List";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tblltMain.Controls.Add(this.GvBillInfo, 0, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81F));
            this.tblltMain.Size = new System.Drawing.Size(884, 512);
            this.tblltMain.TabIndex = 0;
            // 
            // txtquotationNo
            // 
            this.txtquotationNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtquotationNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtquotationNo.Location = new System.Drawing.Point(407, 31);
            this.txtquotationNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtquotationNo.Name = "txtquotationNo";
            this.txtquotationNo.Size = new System.Drawing.Size(110, 29);
            this.txtquotationNo.TabIndex = 7;
            this.txtquotationNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtquotationNo_KeyDown);
            // 
            // frmQuotationList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(884, 512);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "frmQuotationList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quotation List";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmQuotationList_FormClosed);
            this.Load += new System.EventHandler(this.frmQuotationList_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GvBillInfo)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbcustomername;
        private System.Windows.Forms.DateTimePicker dtptodate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpfromdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkQuoNo;
        private System.Windows.Forms.CheckBox chkbillno;
        private System.Windows.Forms.DataGridView GvBillInfo;
        private System.Windows.Forms.Label lblTotalBill;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckBox chkfromdate;
        private System.Windows.Forms.CheckBox chkcompletedc;
        private RachitControls.NumericTextBox txtquotationNo;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btntodaylist;
        private System.Windows.Forms.Button rbtnDeleteMultipleBill;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Button btngetall;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selectg;
        private System.Windows.Forms.DataGridViewImageColumn Updateg;
        private System.Windows.Forms.DataGridViewImageColumn CustBill;
        private System.Windows.Forms.DataGridViewLinkColumn Duplicate;
    }
}