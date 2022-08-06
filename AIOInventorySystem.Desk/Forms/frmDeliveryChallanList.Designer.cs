namespace AIOInventorySystem.Desk.Forms
{
    partial class frmDeliveryChallanList
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
            this.chkcompletedc = new System.Windows.Forms.CheckBox();
            this.cmbcustomername = new System.Windows.Forms.ComboBox();
            this.chkbetweendate = new System.Windows.Forms.CheckBox();
            this.chkporderno = new System.Windows.Forms.CheckBox();
            this.dtpToPorderDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromPorderdate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.GvproductInfo = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.lblTotalBill = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.rbtnDeleteMultipleBill = new System.Windows.Forms.Button();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.btntodaylist = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.GvproductInfo)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkcompletedc
            // 
            this.chkcompletedc.AutoSize = true;
            this.chkcompletedc.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkcompletedc.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcompletedc.ForeColor = System.Drawing.Color.Crimson;
            this.chkcompletedc.Location = new System.Drawing.Point(696, 3);
            this.chkcompletedc.Name = "chkcompletedc";
            this.chkcompletedc.Size = new System.Drawing.Size(193, 23);
            this.chkcompletedc.TabIndex = 3;
            this.chkcompletedc.Text = "Completed Delivery Challan";
            this.chkcompletedc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkcompletedc.UseVisualStyleBackColor = true;
            this.chkcompletedc.CheckedChanged += new System.EventHandler(this.chkcompletedc_CheckedChanged);
            // 
            // cmbcustomername
            // 
            this.cmbcustomername.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcustomername.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcustomername.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbcustomername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustomername.FormattingEnabled = true;
            this.cmbcustomername.Location = new System.Drawing.Point(138, 2);
            this.cmbcustomername.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustomername.Name = "cmbcustomername";
            this.cmbcustomername.Size = new System.Drawing.Size(248, 25);
            this.cmbcustomername.TabIndex = 1;
            this.cmbcustomername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcustomername_KeyDown);
            this.cmbcustomername.Leave += new System.EventHandler(this.cmbcustomername_Leave);
            // 
            // chkbetweendate
            // 
            this.chkbetweendate.AutoSize = true;
            this.chkbetweendate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkbetweendate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbetweendate.Location = new System.Drawing.Point(4, 2);
            this.chkbetweendate.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkbetweendate.Name = "chkbetweendate";
            this.chkbetweendate.Size = new System.Drawing.Size(92, 25);
            this.chkbetweendate.TabIndex = 4;
            this.chkbetweendate.Text = "From Date";
            this.chkbetweendate.UseVisualStyleBackColor = true;
            this.chkbetweendate.CheckedChanged += new System.EventHandler(this.chkbetweendate_CheckedChanged);
            this.chkbetweendate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkbetweendate_KeyDown);
            // 
            // chkporderno
            // 
            this.chkporderno.AutoSize = true;
            this.chkporderno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkporderno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkporderno.Location = new System.Drawing.Point(4, 2);
            this.chkporderno.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkporderno.Name = "chkporderno";
            this.chkporderno.Size = new System.Drawing.Size(130, 25);
            this.chkporderno.TabIndex = 0;
            this.chkporderno.Text = "Customer Name";
            this.chkporderno.UseVisualStyleBackColor = true;
            this.chkporderno.CheckedChanged += new System.EventHandler(this.chkporderno_CheckedChanged);
            this.chkporderno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkporderno_KeyDown);
            // 
            // dtpToPorderDate
            // 
            this.dtpToPorderDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtpToPorderDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToPorderDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToPorderDate.Location = new System.Drawing.Point(233, 2);
            this.dtpToPorderDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpToPorderDate.Name = "dtpToPorderDate";
            this.dtpToPorderDate.Size = new System.Drawing.Size(94, 25);
            this.dtpToPorderDate.TabIndex = 6;
            this.dtpToPorderDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpToPorderDate_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(198, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 25);
            this.label2.TabIndex = 29;
            this.label2.Text = "To";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpFromPorderdate
            // 
            this.dtpFromPorderdate.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtpFromPorderdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromPorderdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromPorderdate.Location = new System.Drawing.Point(100, 2);
            this.dtpFromPorderdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFromPorderdate.Name = "dtpFromPorderdate";
            this.dtpFromPorderdate.Size = new System.Drawing.Size(94, 25);
            this.dtpFromPorderdate.TabIndex = 5;
            this.dtpFromPorderdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpFromPorderdate_KeyDown);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(892, 35);
            this.label5.TabIndex = 11;
            this.label5.Text = " Delivery Challan List";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GvproductInfo
            // 
            this.GvproductInfo.AllowUserToAddRows = false;
            this.GvproductInfo.AllowUserToDeleteRows = false;
            this.GvproductInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvproductInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvproductInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvproductInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvproductInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvproductInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column2});
            this.GvproductInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvproductInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvproductInfo.Location = new System.Drawing.Point(3, 96);
            this.GvproductInfo.Name = "GvproductInfo";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvproductInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GvproductInfo.RowHeadersWidth = 25;
            this.GvproductInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvproductInfo.Size = new System.Drawing.Size(886, 498);
            this.GvproductInfo.TabIndex = 11;
            this.GvproductInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvproductInfo_CellClick);
            this.GvproductInfo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GvproductInfo_KeyPress);
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Select";
            this.Column3.Name = "Column3";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Update";
            this.Column1.Image = global::AIOInventorySystem.Desk.Properties.Resources.Update;
            this.Column1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.DividerWidth = 1;
            this.Column2.HeaderText = "Customer Bill";
            this.Column2.Image = global::AIOInventorySystem.Desk.Properties.Resources.custbill;
            this.Column2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Column2.Name = "Column2";
            // 
            // lblTotalBill
            // 
            this.lblTotalBill.AutoSize = true;
            this.lblTotalBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalBill.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBill.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalBill.Location = new System.Drawing.Point(797, 2);
            this.lblTotalBill.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotalBill.Name = "lblTotalBill";
            this.lblTotalBill.Size = new System.Drawing.Size(93, 25);
            this.lblTotalBill.TabIndex = 87;
            this.lblTotalBill.Text = "0";
            this.lblTotalBill.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(713, 2);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 25);
            this.label8.TabIndex = 86;
            this.label8.Text = "Total Bills:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.31632F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.06437F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.61931F));
            this.tableLayoutPanel1.Controls.Add(this.chkporderno, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbcustomername, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkcompletedc, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 35);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(892, 29);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 10;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.5F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.5F));
            this.tableLayoutPanel2.Controls.Add(this.rbtnDeleteMultipleBill, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.chkbetweendate, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnGetAll, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.btntodaylist, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.dtpFromPorderdate, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTotalBill, 9, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnSearch, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label8, 8, 0);
            this.tableLayoutPanel2.Controls.Add(this.dtpToPorderDate, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 64);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(892, 29);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // rbtnDeleteMultipleBill
            // 
            this.rbtnDeleteMultipleBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.rbtnDeleteMultipleBill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.rbtnDeleteMultipleBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtnDeleteMultipleBill.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnDeleteMultipleBill.ForeColor = System.Drawing.Color.White;
            this.rbtnDeleteMultipleBill.Location = new System.Drawing.Point(562, 1);
            this.rbtnDeleteMultipleBill.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.rbtnDeleteMultipleBill.Name = "rbtnDeleteMultipleBill";
            this.rbtnDeleteMultipleBill.Size = new System.Drawing.Size(147, 27);
            this.rbtnDeleteMultipleBill.TabIndex = 10;
            this.rbtnDeleteMultipleBill.Text = "Delete Multiple Bill";
            this.rbtnDeleteMultipleBill.UseVisualStyleBackColor = false;
            this.rbtnDeleteMultipleBill.Click += new System.EventHandler(this.rbtnDeleteMultipleBill_Click);
            // 
            // btnGetAll
            // 
            this.btnGetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnGetAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGetAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAll.ForeColor = System.Drawing.Color.White;
            this.btnGetAll.Location = new System.Drawing.Point(500, 1);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(58, 27);
            this.btnGetAll.TabIndex = 9;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // btntodaylist
            // 
            this.btntodaylist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btntodaylist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btntodaylist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btntodaylist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntodaylist.ForeColor = System.Drawing.Color.White;
            this.btntodaylist.Location = new System.Drawing.Point(393, 1);
            this.btntodaylist.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btntodaylist.Name = "btntodaylist";
            this.btntodaylist.Size = new System.Drawing.Size(103, 27);
            this.btntodaylist.TabIndex = 8;
            this.btntodaylist.Text = "Today\'s List";
            this.btntodaylist.UseVisualStyleBackColor = false;
            this.btntodaylist.Click += new System.EventHandler(this.btntodaylist_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(331, 1);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(58, 27);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tblltMain.Controls.Add(this.GvproductInfo, 0, 3);
            this.tblltMain.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84F));
            this.tblltMain.Size = new System.Drawing.Size(892, 597);
            this.tblltMain.TabIndex = 0;
            // 
            // frmDeliveryChallanList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(892, 597);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "frmDeliveryChallanList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DeliveryChallan List";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmDeliveryChallanList_FormClosed);
            this.Load += new System.EventHandler(this.frmDeliveryChallanList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvproductInfo)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tblltMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkbetweendate;
        private System.Windows.Forms.CheckBox chkporderno;
        private System.Windows.Forms.DateTimePicker dtpToPorderDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromPorderdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbcustomername;
        private System.Windows.Forms.CheckBox chkcompletedc;
        private System.Windows.Forms.DataGridView GvproductInfo;
        private System.Windows.Forms.Label lblTotalBill;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column3;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.DataGridViewImageColumn Column2;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Button rbtnDeleteMultipleBill;
        private System.Windows.Forms.Button btnGetAll;
        private System.Windows.Forms.Button btntodaylist;
        private System.Windows.Forms.Button btnSearch;
    }
}