namespace AIOInventorySystem.Desk.Forms
{
    partial class frmPrintMultipleNoVatBillList
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
            this.lblTotalBill = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.GvBillInfo = new System.Windows.Forms.DataGridView();
            this.Selectg = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Updateg = new System.Windows.Forms.DataGridViewImageColumn();
            this.cmbcustomername = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkbillno = new System.Windows.Forms.CheckBox();
            this.chkbetweendate = new System.Windows.Forms.CheckBox();
            this.dtptodate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpfromdate = new System.Windows.Forms.DateTimePicker();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnsearch = new System.Windows.Forms.Button();
            this.btngetall = new System.Windows.Forms.Button();
            this.btnMultiPrint = new System.Windows.Forms.Button();
            this.pnlCopies = new System.Windows.Forms.Panel();
            this.tblltCopies = new System.Windows.Forms.TableLayoutPanel();
            this.label56 = new System.Windows.Forms.Label();
            this.chkCust = new System.Windows.Forms.CheckBox();
            this.chkSend = new System.Windows.Forms.CheckBox();
            this.chkTrans = new System.Windows.Forms.CheckBox();
            this.btnCOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GvBillInfo)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.pnlCopies.SuspendLayout();
            this.tblltCopies.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTotalBill
            // 
            this.lblTotalBill.AutoSize = true;
            this.lblTotalBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalBill.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBill.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalBill.Location = new System.Drawing.Point(970, 44);
            this.lblTotalBill.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotalBill.Name = "lblTotalBill";
            this.lblTotalBill.Size = new System.Drawing.Size(62, 24);
            this.lblTotalBill.TabIndex = 115;
            this.lblTotalBill.Text = "0";
            this.lblTotalBill.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(888, 44);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 24);
            this.label8.TabIndex = 114;
            this.label8.Text = "Total Bills:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GvBillInfo
            // 
            this.GvBillInfo.AllowUserToAddRows = false;
            this.GvBillInfo.AllowUserToDeleteRows = false;
            this.GvBillInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvBillInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvBillInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvBillInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvBillInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvBillInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selectg,
            this.Updateg});
            this.tblltMain.SetColumnSpan(this.GvBillInfo, 11);
            this.GvBillInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvBillInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvBillInfo.Location = new System.Drawing.Point(2, 72);
            this.GvBillInfo.Margin = new System.Windows.Forms.Padding(2);
            this.GvBillInfo.Name = "GvBillInfo";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvBillInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GvBillInfo.RowHeadersWidth = 15;
            this.GvBillInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvBillInfo.Size = new System.Drawing.Size(1030, 488);
            this.GvBillInfo.TabIndex = 13;
            this.GvBillInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvBillInfo_CellClick);
            // 
            // Selectg
            // 
            this.Selectg.HeaderText = "Select";
            this.Selectg.Name = "Selectg";
            // 
            // Updateg
            // 
            this.Updateg.HeaderText = "Update";
            this.Updateg.Image = global::AIOInventorySystem.Desk.Properties.Resources.Update;
            this.Updateg.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Updateg.Name = "Updateg";
            // 
            // cmbcustomername
            // 
            this.cmbcustomername.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcustomername.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcustomername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcustomername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustomername.FormattingEnabled = true;
            this.cmbcustomername.Location = new System.Drawing.Point(136, 44);
            this.cmbcustomername.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustomername.Name = "cmbcustomername";
            this.cmbcustomername.Size = new System.Drawing.Size(151, 25);
            this.cmbcustomername.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label2, 11);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1034, 42);
            this.label2.TabIndex = 11;
            this.label2.Text = "Print Multiple Bills";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkbillno
            // 
            this.chkbillno.AutoSize = true;
            this.chkbillno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkbillno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbillno.Location = new System.Drawing.Point(4, 44);
            this.chkbillno.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkbillno.Name = "chkbillno";
            this.chkbillno.Size = new System.Drawing.Size(128, 24);
            this.chkbillno.TabIndex = 1;
            this.chkbillno.Text = "Customer Name";
            this.chkbillno.UseVisualStyleBackColor = true;
            // 
            // chkbetweendate
            // 
            this.chkbetweendate.AutoSize = true;
            this.chkbetweendate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkbetweendate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbetweendate.Location = new System.Drawing.Point(293, 44);
            this.chkbetweendate.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkbetweendate.Name = "chkbetweendate";
            this.chkbetweendate.Size = new System.Drawing.Size(92, 24);
            this.chkbetweendate.TabIndex = 3;
            this.chkbetweendate.Text = "From Date";
            this.chkbetweendate.UseVisualStyleBackColor = true;
            // 
            // dtptodate
            // 
            this.dtptodate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtptodate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtptodate.Location = new System.Drawing.Point(515, 44);
            this.dtptodate.Margin = new System.Windows.Forms.Padding(2);
            this.dtptodate.Name = "dtptodate";
            this.dtptodate.Size = new System.Drawing.Size(91, 25);
            this.dtptodate.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(484, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 24);
            this.label1.TabIndex = 119;
            this.label1.Text = "To";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpfromdate
            // 
            this.dtpfromdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfromdate.Location = new System.Drawing.Point(389, 44);
            this.dtpfromdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpfromdate.Name = "dtpfromdate";
            this.dtpfromdate.Size = new System.Drawing.Size(91, 25);
            this.dtpfromdate.TabIndex = 4;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 11;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.25F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.25F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.Controls.Add(this.btnsearch, 6, 1);
            this.tblltMain.Controls.Add(this.label2, 0, 0);
            this.tblltMain.Controls.Add(this.btngetall, 7, 1);
            this.tblltMain.Controls.Add(this.btnMultiPrint, 8, 1);
            this.tblltMain.Controls.Add(this.chkbillno, 0, 1);
            this.tblltMain.Controls.Add(this.lblTotalBill, 10, 1);
            this.tblltMain.Controls.Add(this.GvBillInfo, 0, 2);
            this.tblltMain.Controls.Add(this.cmbcustomername, 1, 1);
            this.tblltMain.Controls.Add(this.dtptodate, 5, 1);
            this.tblltMain.Controls.Add(this.label8, 9, 1);
            this.tblltMain.Controls.Add(this.chkbetweendate, 2, 1);
            this.tblltMain.Controls.Add(this.dtpfromdate, 3, 1);
            this.tblltMain.Controls.Add(this.label1, 4, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87.5F));
            this.tblltMain.Size = new System.Drawing.Size(1034, 562);
            this.tblltMain.TabIndex = 0;
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(610, 43);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(63, 26);
            this.btnsearch.TabIndex = 6;
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
            this.btngetall.Location = new System.Drawing.Point(677, 43);
            this.btngetall.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btngetall.Name = "btngetall";
            this.btngetall.Size = new System.Drawing.Size(63, 26);
            this.btngetall.TabIndex = 7;
            this.btngetall.Text = "Get All";
            this.btngetall.UseVisualStyleBackColor = false;
            this.btngetall.Click += new System.EventHandler(this.btngetall_Click);
            // 
            // btnMultiPrint
            // 
            this.btnMultiPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnMultiPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMultiPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMultiPrint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMultiPrint.ForeColor = System.Drawing.Color.White;
            this.btnMultiPrint.Location = new System.Drawing.Point(744, 43);
            this.btnMultiPrint.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnMultiPrint.Name = "btnMultiPrint";
            this.btnMultiPrint.Size = new System.Drawing.Size(140, 26);
            this.btnMultiPrint.TabIndex = 8;
            this.btnMultiPrint.Text = "Print Multiple Bills";
            this.btnMultiPrint.UseVisualStyleBackColor = false;
            this.btnMultiPrint.Click += new System.EventHandler(this.btnMultiPrint_Click);
            // 
            // pnlCopies
            // 
            this.pnlCopies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlCopies.Controls.Add(this.tblltCopies);
            this.pnlCopies.Location = new System.Drawing.Point(677, 74);
            this.pnlCopies.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlCopies.Name = "pnlCopies";
            this.pnlCopies.Size = new System.Drawing.Size(177, 114);
            this.pnlCopies.TabIndex = 9;
            // 
            // tblltCopies
            // 
            this.tblltCopies.ColumnCount = 2;
            this.tblltCopies.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tblltCopies.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblltCopies.Controls.Add(this.label56, 0, 0);
            this.tblltCopies.Controls.Add(this.chkCust, 0, 1);
            this.tblltCopies.Controls.Add(this.chkSend, 0, 3);
            this.tblltCopies.Controls.Add(this.chkTrans, 0, 2);
            this.tblltCopies.Controls.Add(this.btnCOk, 1, 3);
            this.tblltCopies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltCopies.Location = new System.Drawing.Point(0, 0);
            this.tblltCopies.Name = "tblltCopies";
            this.tblltCopies.RowCount = 4;
            this.tblltCopies.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltCopies.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltCopies.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltCopies.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltCopies.Size = new System.Drawing.Size(175, 112);
            this.tblltCopies.TabIndex = 9;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label56.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.ForeColor = System.Drawing.Color.Navy;
            this.label56.Location = new System.Drawing.Point(3, 3);
            this.label56.Margin = new System.Windows.Forms.Padding(3);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(107, 22);
            this.label56.TabIndex = 156;
            this.label56.Text = "Select Copies :";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkCust
            // 
            this.chkCust.AutoSize = true;
            this.chkCust.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkCust.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCust.Location = new System.Drawing.Point(10, 31);
            this.chkCust.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.chkCust.Name = "chkCust";
            this.chkCust.Size = new System.Drawing.Size(100, 22);
            this.chkCust.TabIndex = 9;
            this.chkCust.Text = "Original";
            this.chkCust.UseVisualStyleBackColor = true;
            // 
            // chkSend
            // 
            this.chkSend.AutoSize = true;
            this.chkSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkSend.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSend.Location = new System.Drawing.Point(10, 87);
            this.chkSend.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.chkSend.Name = "chkSend";
            this.chkSend.Size = new System.Drawing.Size(100, 22);
            this.chkSend.TabIndex = 11;
            this.chkSend.Text = "Sender";
            this.chkSend.UseVisualStyleBackColor = true;
            // 
            // chkTrans
            // 
            this.chkTrans.AutoSize = true;
            this.chkTrans.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkTrans.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTrans.Location = new System.Drawing.Point(10, 59);
            this.chkTrans.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.chkTrans.Name = "chkTrans";
            this.chkTrans.Size = new System.Drawing.Size(100, 22);
            this.chkTrans.TabIndex = 10;
            this.chkTrans.Text = "Transporter";
            this.chkTrans.UseVisualStyleBackColor = true;
            // 
            // btnCOk
            // 
            this.btnCOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnCOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCOk.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCOk.ForeColor = System.Drawing.Color.White;
            this.btnCOk.Location = new System.Drawing.Point(115, 86);
            this.btnCOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnCOk.Name = "btnCOk";
            this.btnCOk.Size = new System.Drawing.Size(58, 24);
            this.btnCOk.TabIndex = 12;
            this.btnCOk.Text = "OK";
            this.btnCOk.UseVisualStyleBackColor = false;
            this.btnCOk.Click += new System.EventHandler(this.btnCOk_Click);
            // 
            // frmPrintMultipleNoVatBillList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1034, 562);
            this.Controls.Add(this.pnlCopies);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmPrintMultipleNoVatBillList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Multiple Bill List";
            this.Load += new System.EventHandler(this.frmPrintMultipleNoVatBillList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvBillInfo)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.pnlCopies.ResumeLayout(false);
            this.tblltCopies.ResumeLayout(false);
            this.tblltCopies.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTotalBill;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView GvBillInfo;
        private System.Windows.Forms.ComboBox cmbcustomername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkbillno;
        private System.Windows.Forms.CheckBox chkbetweendate;
        private System.Windows.Forms.DateTimePicker dtptodate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpfromdate;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Panel pnlCopies;
        private System.Windows.Forms.CheckBox chkSend;
        private System.Windows.Forms.CheckBox chkTrans;
        private System.Windows.Forms.CheckBox chkCust;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Button btnMultiPrint;
        private System.Windows.Forms.Button btngetall;
        private System.Windows.Forms.TableLayoutPanel tblltCopies;
        private System.Windows.Forms.Button btnCOk;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selectg;
        private System.Windows.Forms.DataGridViewImageColumn Updateg;
    }
}