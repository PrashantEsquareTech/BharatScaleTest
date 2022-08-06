namespace AIOInventorySystem.Desk.Forms
{
    partial class frmCustomerCerditDebitNoteList
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
            this.label9 = new System.Windows.Forms.Label();
            this.tblltFliter = new System.Windows.Forms.TableLayoutPanel();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.chkCreditDebitNoteDate = new System.Windows.Forms.CheckBox();
            this.txtCDNO = new System.Windows.Forms.TextBox();
            this.chkCreditDebitNo = new System.Windows.Forms.CheckBox();
            this.chkCustomerName = new System.Windows.Forms.CheckBox();
            this.cmbCustomerName = new System.Windows.Forms.ComboBox();
            this.chkBillNo = new System.Windows.Forms.CheckBox();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.dtpCDFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpCDTo = new System.Windows.Forms.DateTimePicker();
            this.chkBillDate = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCnt = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.GVCreditDebitList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.tblltMain.SuspendLayout();
            this.tblltFliter.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVCreditDebitList)).BeginInit();
            this.SuspendLayout();
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.label9, 0, 0);
            this.tblltMain.Controls.Add(this.tblltFliter, 0, 1);
            this.tblltMain.Controls.Add(this.GVCreditDebitList, 0, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79F));
            this.tblltMain.Size = new System.Drawing.Size(954, 462);
            this.tblltMain.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(954, 36);
            this.label9.TabIndex = 34;
            this.label9.Text = "Credit Debit Note List";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltFliter
            // 
            this.tblltFliter.ColumnCount = 8;
            this.tblltMain.SetColumnSpan(this.tblltFliter, 2);
            this.tblltFliter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltFliter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tblltFliter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.5F));
            this.tblltFliter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.5F));
            this.tblltFliter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.5F));
            this.tblltFliter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltFliter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltFliter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.5F));
            this.tblltFliter.Controls.Add(this.btnGetAll, 7, 1);
            this.tblltFliter.Controls.Add(this.chkCreditDebitNoteDate, 0, 1);
            this.tblltFliter.Controls.Add(this.txtCDNO, 5, 0);
            this.tblltFliter.Controls.Add(this.chkCreditDebitNo, 4, 0);
            this.tblltFliter.Controls.Add(this.chkCustomerName, 0, 0);
            this.tblltFliter.Controls.Add(this.cmbCustomerName, 1, 0);
            this.tblltFliter.Controls.Add(this.chkBillNo, 2, 0);
            this.tblltFliter.Controls.Add(this.txtBillNo, 3, 0);
            this.tblltFliter.Controls.Add(this.tableLayoutPanel3, 1, 1);
            this.tblltFliter.Controls.Add(this.chkBillDate, 2, 1);
            this.tblltFliter.Controls.Add(this.tableLayoutPanel4, 3, 1);
            this.tblltFliter.Controls.Add(this.label3, 6, 0);
            this.tblltFliter.Controls.Add(this.lblCnt, 7, 0);
            this.tblltFliter.Controls.Add(this.btnSearch, 6, 1);
            this.tblltFliter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltFliter.Location = new System.Drawing.Point(0, 36);
            this.tblltFliter.Margin = new System.Windows.Forms.Padding(0);
            this.tblltFliter.Name = "tblltFliter";
            this.tblltFliter.RowCount = 2;
            this.tblltFliter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltFliter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltFliter.Size = new System.Drawing.Size(954, 60);
            this.tblltFliter.TabIndex = 0;
            // 
            // btnGetAll
            // 
            this.btnGetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnGetAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGetAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAll.ForeColor = System.Drawing.Color.White;
            this.btnGetAll.Location = new System.Drawing.Point(855, 32);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(6, 2, 6, 2);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(93, 26);
            this.btnGetAll.TabIndex = 14;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // chkCreditDebitNoteDate
            // 
            this.chkCreditDebitNoteDate.AutoSize = true;
            this.chkCreditDebitNoteDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkCreditDebitNoteDate.Location = new System.Drawing.Point(5, 32);
            this.chkCreditDebitNoteDate.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkCreditDebitNoteDate.Name = "chkCreditDebitNoteDate";
            this.chkCreditDebitNoteDate.Size = new System.Drawing.Size(126, 26);
            this.chkCreditDebitNoteDate.TabIndex = 6;
            this.chkCreditDebitNoteDate.Text = "Credit Debit Date";
            this.chkCreditDebitNoteDate.UseVisualStyleBackColor = true;
            // 
            // txtCDNO
            // 
            this.txtCDNO.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCDNO.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCDNO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCDNO.Location = new System.Drawing.Point(662, 2);
            this.txtCDNO.Margin = new System.Windows.Forms.Padding(2);
            this.txtCDNO.Name = "txtCDNO";
            this.txtCDNO.Size = new System.Drawing.Size(81, 25);
            this.txtCDNO.TabIndex = 5;
            // 
            // chkCreditDebitNo
            // 
            this.chkCreditDebitNo.AutoSize = true;
            this.chkCreditDebitNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkCreditDebitNo.Location = new System.Drawing.Point(537, 2);
            this.chkCreditDebitNo.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkCreditDebitNo.Name = "chkCreditDebitNo";
            this.chkCreditDebitNo.Size = new System.Drawing.Size(121, 26);
            this.chkCreditDebitNo.TabIndex = 4;
            this.chkCreditDebitNo.Text = "Credit Debit No";
            this.chkCreditDebitNo.UseVisualStyleBackColor = true;
            // 
            // chkCustomerName
            // 
            this.chkCustomerName.AutoSize = true;
            this.chkCustomerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkCustomerName.Location = new System.Drawing.Point(5, 2);
            this.chkCustomerName.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkCustomerName.Name = "chkCustomerName";
            this.chkCustomerName.Size = new System.Drawing.Size(126, 26);
            this.chkCustomerName.TabIndex = 0;
            this.chkCustomerName.Text = "Customer Name";
            this.chkCustomerName.UseVisualStyleBackColor = true;
            // 
            // cmbCustomerName
            // 
            this.cmbCustomerName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCustomerName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCustomerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCustomerName.FormattingEnabled = true;
            this.cmbCustomerName.Location = new System.Drawing.Point(135, 2);
            this.cmbCustomerName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCustomerName.Name = "cmbCustomerName";
            this.cmbCustomerName.Size = new System.Drawing.Size(224, 25);
            this.cmbCustomerName.TabIndex = 1;
            // 
            // chkBillNo
            // 
            this.chkBillNo.AutoSize = true;
            this.chkBillNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkBillNo.Location = new System.Drawing.Point(366, 2);
            this.chkBillNo.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkBillNo.Name = "chkBillNo";
            this.chkBillNo.Size = new System.Drawing.Size(74, 26);
            this.chkBillNo.TabIndex = 2;
            this.chkBillNo.Text = "Bill No";
            this.chkBillNo.UseVisualStyleBackColor = true;
            // 
            // txtBillNo
            // 
            this.txtBillNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtBillNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtBillNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBillNo.Location = new System.Drawing.Point(444, 2);
            this.txtBillNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.Size = new System.Drawing.Size(86, 25);
            this.txtBillNo.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 3;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.5F));
            this.tableLayoutPanel3.Controls.Add(this.dtpCDFrom, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.dtpCDTo, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(133, 30);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(228, 30);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // dtpCDFrom
            // 
            this.dtpCDFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpCDFrom.Location = new System.Drawing.Point(2, 2);
            this.dtpCDFrom.Margin = new System.Windows.Forms.Padding(2);
            this.dtpCDFrom.Name = "dtpCDFrom";
            this.dtpCDFrom.Size = new System.Drawing.Size(92, 25);
            this.dtpCDFrom.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(98, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 26);
            this.label1.TabIndex = 9;
            this.label1.Text = "To";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpCDTo
            // 
            this.dtpCDTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpCDTo.Location = new System.Drawing.Point(132, 2);
            this.dtpCDTo.Margin = new System.Windows.Forms.Padding(2);
            this.dtpCDTo.Name = "dtpCDTo";
            this.dtpCDTo.Size = new System.Drawing.Size(94, 25);
            this.dtpCDTo.TabIndex = 8;
            // 
            // chkBillDate
            // 
            this.chkBillDate.AutoSize = true;
            this.chkBillDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkBillDate.Location = new System.Drawing.Point(366, 32);
            this.chkBillDate.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkBillDate.Name = "chkBillDate";
            this.chkBillDate.Size = new System.Drawing.Size(74, 26);
            this.chkBillDate.TabIndex = 9;
            this.chkBillDate.Text = "Bill Date";
            this.chkBillDate.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tblltFliter.SetColumnSpan(this.tableLayoutPanel4, 2);
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.5F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42.5F));
            this.tableLayoutPanel4.Controls.Add(this.dtpFromDate, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.dtpToDate, 2, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(442, 30);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(218, 30);
            this.tableLayoutPanel4.TabIndex = 11;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFromDate.Location = new System.Drawing.Point(2, 2);
            this.dtpFromDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(88, 25);
            this.dtpFromDate.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(94, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "To";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpToDate.Location = new System.Drawing.Point(126, 2);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(90, 25);
            this.dtpToDate.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(747, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 26);
            this.label3.TabIndex = 14;
            this.label3.Text = "Total CDN:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCnt
            // 
            this.lblCnt.AutoSize = true;
            this.lblCnt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCnt.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCnt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblCnt.Location = new System.Drawing.Point(851, 2);
            this.lblCnt.Margin = new System.Windows.Forms.Padding(2);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(101, 26);
            this.lblCnt.TabIndex = 15;
            this.lblCnt.Text = "0";
            this.lblCnt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(751, 32);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(6, 2, 6, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(92, 26);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // GVCreditDebitList
            // 
            this.GVCreditDebitList.AllowUserToAddRows = false;
            this.GVCreditDebitList.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVCreditDebitList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GVCreditDebitList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVCreditDebitList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.GVCreditDebitList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GVCreditDebitList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GVCreditDebitList.Location = new System.Drawing.Point(3, 99);
            this.GVCreditDebitList.Name = "GVCreditDebitList";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVCreditDebitList.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GVCreditDebitList.RowHeadersWidth = 15;
            this.GVCreditDebitList.Size = new System.Drawing.Size(948, 360);
            this.GVCreditDebitList.TabIndex = 15;
            this.GVCreditDebitList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GVCreditDebitList_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Update";
            this.Column1.Image = global::AIOInventorySystem.Desk.Properties.Resources.Update;
            this.Column1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Column1.Name = "Column1";
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.HeaderText = "Update";
            this.dataGridViewImageColumn1.Image = global::AIOInventorySystem.Desk.Properties.Resources.Update;
            this.dataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            // 
            // frmCustomerCerditDebitNoteList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(954, 462);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmCustomerCerditDebitNoteList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Cerdit Debit Note List";
            this.Load += new System.EventHandler(this.frmCustomerCerditDebitNoteList_Load);
            this.tblltMain.ResumeLayout(false);
            this.tblltFliter.ResumeLayout(false);
            this.tblltFliter.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVCreditDebitList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltFliter;
        private System.Windows.Forms.CheckBox chkCustomerName;
        private System.Windows.Forms.ComboBox cmbCustomerName;
        private System.Windows.Forms.TextBox txtCDNO;
        private System.Windows.Forms.CheckBox chkCreditDebitNo;
        private System.Windows.Forms.CheckBox chkBillNo;
        private System.Windows.Forms.TextBox txtBillNo;
        private System.Windows.Forms.CheckBox chkCreditDebitNoteDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.DateTimePicker dtpCDFrom;
        private System.Windows.Forms.DateTimePicker dtpCDTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkBillDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCnt;
        private System.Windows.Forms.DataGridView GVCreditDebitList;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnGetAll;
        private System.Windows.Forms.Button btnSearch;
    }
}