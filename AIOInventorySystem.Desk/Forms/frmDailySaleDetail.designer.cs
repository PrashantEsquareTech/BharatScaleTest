namespace AIOInventorySystem.Desk.Forms
{
    partial class frmDailySaleDetail
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
            this.lblsaleamount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dtpfromdate = new System.Windows.Forms.DateTimePicker();
            this.GvCredit = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.txtpname = new System.Windows.Forms.TextBox();
            this.cmbcomanyname = new System.Windows.Forms.ComboBox();
            this.chkproductname = new System.Windows.Forms.CheckBox();
            this.chkCompanyName = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tblltFilters = new System.Windows.Forms.TableLayoutPanel();
            this.tblltbuttons = new System.Windows.Forms.TableLayoutPanel();
            this.btngetall = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnsearch = new System.Windows.Forms.Button();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.chkBillNo = new System.Windows.Forms.CheckBox();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.GvCredit)).BeginInit();
            this.tblltFilters.SuspendLayout();
            this.tblltbuttons.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblsaleamount
            // 
            this.lblsaleamount.AutoSize = true;
            this.lblsaleamount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblsaleamount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsaleamount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblsaleamount.Location = new System.Drawing.Point(701, 491);
            this.lblsaleamount.Margin = new System.Windows.Forms.Padding(2);
            this.lblsaleamount.Name = "lblsaleamount";
            this.lblsaleamount.Size = new System.Drawing.Size(231, 29);
            this.lblsaleamount.TabIndex = 47;
            this.lblsaleamount.Text = "0";
            this.lblsaleamount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(468, 491);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(229, 29);
            this.label6.TabIndex = 44;
            this.label6.Text = "Total Sale Amt:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpfromdate
            // 
            this.dtpfromdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfromdate.Location = new System.Drawing.Point(123, 2);
            this.dtpfromdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpfromdate.Name = "dtpfromdate";
            this.dtpfromdate.Size = new System.Drawing.Size(112, 25);
            this.dtpfromdate.TabIndex = 2;
            this.dtpfromdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpfromdate_KeyDown);
            // 
            // GvCredit
            // 
            this.GvCredit.AllowUserToAddRows = false;
            this.GvCredit.AllowUserToDeleteRows = false;
            this.GvCredit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvCredit.BackgroundColor = System.Drawing.Color.White;
            this.GvCredit.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvCredit.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvCredit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblltMain.SetColumnSpan(this.GvCredit, 4);
            this.GvCredit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvCredit.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvCredit.Location = new System.Drawing.Point(3, 96);
            this.GvCredit.Name = "GvCredit";
            this.GvCredit.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvCredit.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GvCredit.RowHeadersWidth = 10;
            this.GvCredit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvCredit.Size = new System.Drawing.Size(928, 390);
            this.GvCredit.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label5, 4);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(934, 36);
            this.label5.TabIndex = 37;
            this.label5.Text = "Daily Sale Details";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtpname
            // 
            this.txtpname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtpname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tblltFilters.SetColumnSpan(this.txtpname, 3);
            this.txtpname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtpname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpname.Location = new System.Drawing.Point(123, 30);
            this.txtpname.Margin = new System.Windows.Forms.Padding(2);
            this.txtpname.Name = "txtpname";
            this.txtpname.Size = new System.Drawing.Size(265, 25);
            this.txtpname.TabIndex = 7;
            this.txtpname.Text = " ";
            this.txtpname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpname_KeyDown);
            this.txtpname.Leave += new System.EventHandler(this.txtpname_Leave);
            // 
            // cmbcomanyname
            // 
            this.cmbcomanyname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcomanyname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcomanyname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcomanyname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcomanyname.ForeColor = System.Drawing.Color.Black;
            this.cmbcomanyname.FormattingEnabled = true;
            this.cmbcomanyname.Location = new System.Drawing.Point(522, 30);
            this.cmbcomanyname.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcomanyname.Name = "cmbcomanyname";
            this.cmbcomanyname.Size = new System.Drawing.Size(173, 25);
            this.cmbcomanyname.TabIndex = 9;
            this.cmbcomanyname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcomanyname_KeyDown);
            this.cmbcomanyname.Leave += new System.EventHandler(this.cmbcomanyname_Leave);
            // 
            // chkproductname
            // 
            this.chkproductname.AutoSize = true;
            this.chkproductname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkproductname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkproductname.Location = new System.Drawing.Point(6, 30);
            this.chkproductname.Margin = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this.chkproductname.Name = "chkproductname";
            this.chkproductname.Size = new System.Drawing.Size(113, 25);
            this.chkproductname.TabIndex = 6;
            this.chkproductname.Text = "Product Name";
            this.chkproductname.UseVisualStyleBackColor = true;
            this.chkproductname.CheckedChanged += new System.EventHandler(this.chkproductname_CheckedChanged);
            this.chkproductname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkproductname_KeyDown);
            // 
            // chkCompanyName
            // 
            this.chkCompanyName.AutoSize = true;
            this.chkCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkCompanyName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCompanyName.Location = new System.Drawing.Point(396, 30);
            this.chkCompanyName.Margin = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this.chkCompanyName.Name = "chkCompanyName";
            this.chkCompanyName.Size = new System.Drawing.Size(122, 25);
            this.chkCompanyName.TabIndex = 8;
            this.chkCompanyName.Text = "Company Name";
            this.chkCompanyName.UseVisualStyleBackColor = true;
            this.chkCompanyName.CheckedChanged += new System.EventHandler(this.chkCompanyName_CheckedChanged);
            this.chkCompanyName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkCompanyName_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(2, 491);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 29);
            this.label1.TabIndex = 115;
            this.label1.Text = "Total Profit Amt:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(235, 491);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(229, 29);
            this.label3.TabIndex = 116;
            this.label3.Text = "0";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tblltFilters
            // 
            this.tblltFilters.ColumnCount = 7;
            this.tblltMain.SetColumnSpan(this.tblltFilters, 4);
            this.tblltFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tblltFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltFilters.Controls.Add(this.tblltbuttons, 6, 1);
            this.tblltFilters.Controls.Add(this.chkproductname, 0, 1);
            this.tblltFilters.Controls.Add(this.txtpname, 1, 1);
            this.tblltFilters.Controls.Add(this.txtBillNo, 5, 0);
            this.tblltFilters.Controls.Add(this.chkBillNo, 4, 0);
            this.tblltFilters.Controls.Add(this.dtpToDate, 3, 0);
            this.tblltFilters.Controls.Add(this.label4, 2, 0);
            this.tblltFilters.Controls.Add(this.dtpfromdate, 1, 0);
            this.tblltFilters.Controls.Add(this.chkDate, 0, 0);
            this.tblltFilters.Controls.Add(this.cmbcomanyname, 5, 1);
            this.tblltFilters.Controls.Add(this.chkCompanyName, 4, 1);
            this.tblltFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltFilters.Location = new System.Drawing.Point(0, 36);
            this.tblltFilters.Margin = new System.Windows.Forms.Padding(0);
            this.tblltFilters.Name = "tblltFilters";
            this.tblltFilters.RowCount = 2;
            this.tblltFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltFilters.Size = new System.Drawing.Size(934, 57);
            this.tblltFilters.TabIndex = 0;
            // 
            // tblltbuttons
            // 
            this.tblltbuttons.ColumnCount = 4;
            this.tblltbuttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltbuttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltbuttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblltbuttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltbuttons.Controls.Add(this.btngetall, 2, 0);
            this.tblltbuttons.Controls.Add(this.btnPrint, 3, 0);
            this.tblltbuttons.Controls.Add(this.btnsearch, 1, 0);
            this.tblltbuttons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltbuttons.Location = new System.Drawing.Point(697, 28);
            this.tblltbuttons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltbuttons.Name = "tblltbuttons";
            this.tblltbuttons.RowCount = 1;
            this.tblltbuttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltbuttons.Size = new System.Drawing.Size(237, 29);
            this.tblltbuttons.TabIndex = 10;
            // 
            // btngetall
            // 
            this.btngetall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btngetall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btngetall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btngetall.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngetall.ForeColor = System.Drawing.Color.White;
            this.btngetall.Location = new System.Drawing.Point(84, 1);
            this.btngetall.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btngetall.Name = "btngetall";
            this.btngetall.Size = new System.Drawing.Size(90, 27);
            this.btngetall.TabIndex = 11;
            this.btngetall.Text = "Today\'s List";
            this.btngetall.UseVisualStyleBackColor = false;
            this.btngetall.Click += new System.EventHandler(this.btngetall_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(178, 1);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(57, 27);
            this.btnPrint.TabIndex = 12;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(13, 1);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(67, 27);
            this.btnsearch.TabIndex = 10;
            this.btnsearch.Text = "Search";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // txtBillNo
            // 
            this.txtBillNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtBillNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtBillNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBillNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillNo.Location = new System.Drawing.Point(522, 2);
            this.txtBillNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.Size = new System.Drawing.Size(173, 25);
            this.txtBillNo.TabIndex = 5;
            this.txtBillNo.Text = " ";
            this.txtBillNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillNo_KeyDown);
            this.txtBillNo.Leave += new System.EventHandler(this.txtBillNo_Leave);
            // 
            // chkBillNo
            // 
            this.chkBillNo.AutoSize = true;
            this.chkBillNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkBillNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBillNo.Location = new System.Drawing.Point(396, 2);
            this.chkBillNo.Margin = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this.chkBillNo.Name = "chkBillNo";
            this.chkBillNo.Size = new System.Drawing.Size(122, 24);
            this.chkBillNo.TabIndex = 4;
            this.chkBillNo.Text = "BillNo";
            this.chkBillNo.UseVisualStyleBackColor = true;
            this.chkBillNo.CheckedChanged += new System.EventHandler(this.chkBillNo_CheckedChanged);
            this.chkBillNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkBillNo_KeyDown);
            // 
            // dtpToDate
            // 
            this.dtpToDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpToDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(276, 2);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(112, 25);
            this.dtpToDate.TabIndex = 3;
            this.dtpToDate.Visible = false;
            this.dtpToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpToDate_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(239, 2);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 24);
            this.label4.TabIndex = 36;
            this.label4.Text = "To";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = false;
            // 
            // chkDate
            // 
            this.chkDate.AutoSize = true;
            this.chkDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDate.Location = new System.Drawing.Point(6, 2);
            this.chkDate.Margin = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(113, 24);
            this.chkDate.TabIndex = 1;
            this.chkDate.Text = "Date";
            this.chkDate.UseVisualStyleBackColor = true;
            this.chkDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkDate_KeyDown);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 4;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.tblltFilters, 0, 1);
            this.tblltMain.Controls.Add(this.label1, 0, 3);
            this.tblltMain.Controls.Add(this.GvCredit, 0, 2);
            this.tblltMain.Controls.Add(this.label3, 1, 3);
            this.tblltMain.Controls.Add(this.label6, 2, 3);
            this.tblltMain.Controls.Add(this.lblsaleamount, 3, 3);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.Size = new System.Drawing.Size(934, 522);
            this.tblltMain.TabIndex = 0;
            // 
            // frmDailySaleDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(934, 522);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1500, 570);
            this.Name = "frmDailySaleDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Daily Sale Details";
            this.Load += new System.EventHandler(this.frmDailySaleDetail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvCredit)).EndInit();
            this.tblltFilters.ResumeLayout(false);
            this.tblltFilters.PerformLayout();
            this.tblltbuttons.ResumeLayout(false);
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpfromdate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblsaleamount;
        private System.Windows.Forms.DataGridView GvCredit;
        private System.Windows.Forms.TextBox txtpname;
        private System.Windows.Forms.ComboBox cmbcomanyname;
        private System.Windows.Forms.CheckBox chkproductname;
        private System.Windows.Forms.CheckBox chkCompanyName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tblltFilters;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBillNo;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkBillNo;
        private System.Windows.Forms.TableLayoutPanel tblltbuttons;
        private System.Windows.Forms.CheckBox chkDate;
        private System.Windows.Forms.Button btngetall;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnsearch;
    }
}