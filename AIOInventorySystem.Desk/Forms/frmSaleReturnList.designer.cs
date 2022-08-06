namespace AIOInventorySystem.Desk.Forms
{
    partial class frmSaleReturnList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.chkbetweendate = new System.Windows.Forms.CheckBox();
            this.chkporderno = new System.Windows.Forms.CheckBox();
            this.dtpToPorderDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromPorderdate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbcustomername = new System.Windows.Forms.ComboBox();
            this.chksname = new System.Windows.Forms.CheckBox();
            this.txtbillno = new System.Windows.Forms.TextBox();
            this.checkboxbillno = new System.Windows.Forms.CheckBox();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.dtgvList = new System.Windows.Forms.DataGridView();
            this.Selectg = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Updateg = new System.Windows.Forms.DataGridViewImageColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleReturnNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleReturnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.rbtnDeleteMultipleBill = new System.Windows.Forms.Button();
            this.txtsrno = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.tblltMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // chkbetweendate
            // 
            this.chkbetweendate.AutoSize = true;
            this.chkbetweendate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkbetweendate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbetweendate.Location = new System.Drawing.Point(299, 37);
            this.chkbetweendate.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkbetweendate.Name = "chkbetweendate";
            this.chkbetweendate.Size = new System.Drawing.Size(84, 24);
            this.chkbetweendate.TabIndex = 4;
            this.chkbetweendate.Text = "Date";
            this.chkbetweendate.UseVisualStyleBackColor = true;
            this.chkbetweendate.CheckedChanged += new System.EventHandler(this.chkbetweendate_CheckedChanged);
            this.chkbetweendate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkbetweendate_KeyDown);
            // 
            // chkporderno
            // 
            this.chkporderno.AutoSize = true;
            this.chkporderno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkporderno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkporderno.Location = new System.Drawing.Point(5, 37);
            this.chkporderno.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkporderno.Name = "chkporderno";
            this.chkporderno.Size = new System.Drawing.Size(116, 24);
            this.chkporderno.TabIndex = 0;
            this.chkporderno.Text = "Sale Return No";
            this.chkporderno.UseVisualStyleBackColor = true;
            this.chkporderno.CheckedChanged += new System.EventHandler(this.chkporderno_CheckedChanged);
            this.chkporderno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkporderno_KeyDown);
            // 
            // dtpToPorderDate
            // 
            this.dtpToPorderDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpToPorderDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToPorderDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToPorderDate.Location = new System.Drawing.Point(416, 65);
            this.dtpToPorderDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpToPorderDate.Name = "dtpToPorderDate";
            this.dtpToPorderDate.Size = new System.Drawing.Size(87, 25);
            this.dtpToPorderDate.TabIndex = 6;
            this.dtpToPorderDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpToPorderDate_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(387, 65);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 24);
            this.label2.TabIndex = 19;
            this.label2.Text = "To";
            // 
            // dtpFromPorderdate
            // 
            this.dtpFromPorderdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFromPorderdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromPorderdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromPorderdate.Location = new System.Drawing.Point(296, 65);
            this.dtpFromPorderdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFromPorderdate.Name = "dtpFromPorderdate";
            this.dtpFromPorderdate.Size = new System.Drawing.Size(87, 25);
            this.dtpFromPorderdate.TabIndex = 5;
            this.dtpFromPorderdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpFromPorderdate_KeyDown);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label5, 8);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(728, 35);
            this.label5.TabIndex = 13;
            this.label5.Text = "Sale Return List";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbcustomername
            // 
            this.cmbcustomername.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcustomername.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbcustomername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcustomername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustomername.FormattingEnabled = true;
            this.cmbcustomername.Location = new System.Drawing.Point(125, 65);
            this.cmbcustomername.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustomername.Name = "cmbcustomername";
            this.cmbcustomername.Size = new System.Drawing.Size(167, 25);
            this.cmbcustomername.TabIndex = 3;
            this.cmbcustomername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcustomername_KeyDown);
            this.cmbcustomername.Leave += new System.EventHandler(this.cmbcustomername_Leave);
            // 
            // chksname
            // 
            this.chksname.AutoSize = true;
            this.chksname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chksname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chksname.Location = new System.Drawing.Point(128, 37);
            this.chksname.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chksname.Name = "chksname";
            this.chksname.Size = new System.Drawing.Size(164, 24);
            this.chksname.TabIndex = 2;
            this.chksname.Text = "Customer Name";
            this.chksname.UseVisualStyleBackColor = true;
            this.chksname.CheckedChanged += new System.EventHandler(this.chksname_CheckedChanged);
            this.chksname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chksname_KeyDown);
            // 
            // txtbillno
            // 
            this.txtbillno.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtbillno.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtbillno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbillno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbillno.Location = new System.Drawing.Point(507, 65);
            this.txtbillno.Margin = new System.Windows.Forms.Padding(2);
            this.txtbillno.Name = "txtbillno";
            this.txtbillno.Size = new System.Drawing.Size(72, 25);
            this.txtbillno.TabIndex = 8;
            this.txtbillno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbillno_KeyDown);
            // 
            // checkboxbillno
            // 
            this.checkboxbillno.AutoSize = true;
            this.checkboxbillno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkboxbillno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkboxbillno.Location = new System.Drawing.Point(510, 37);
            this.checkboxbillno.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.checkboxbillno.Name = "checkboxbillno";
            this.checkboxbillno.Size = new System.Drawing.Size(69, 24);
            this.checkboxbillno.TabIndex = 7;
            this.checkboxbillno.Text = "Bill No";
            this.checkboxbillno.UseVisualStyleBackColor = true;
            this.checkboxbillno.CheckedChanged += new System.EventHandler(this.checkboxbillno_CheckedChanged);
            this.checkboxbillno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.checkboxbillno_KeyDown);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 8;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.Controls.Add(this.dtgvList, 0, 3);
            this.tblltMain.Controls.Add(this.btnSearch, 6, 2);
            this.tblltMain.Controls.Add(this.btnGetAll, 7, 2);
            this.tblltMain.Controls.Add(this.rbtnDeleteMultipleBill, 6, 1);
            this.tblltMain.Controls.Add(this.dtpFromPorderdate, 2, 2);
            this.tblltMain.Controls.Add(this.dtpToPorderDate, 4, 2);
            this.tblltMain.Controls.Add(this.txtbillno, 5, 2);
            this.tblltMain.Controls.Add(this.cmbcustomername, 1, 2);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.chkporderno, 0, 1);
            this.tblltMain.Controls.Add(this.label2, 3, 2);
            this.tblltMain.Controls.Add(this.chksname, 1, 1);
            this.tblltMain.Controls.Add(this.chkbetweendate, 2, 1);
            this.tblltMain.Controls.Add(this.checkboxbillno, 5, 1);
            this.tblltMain.Controls.Add(this.txtsrno, 0, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82F));
            this.tblltMain.Size = new System.Drawing.Size(728, 512);
            this.tblltMain.TabIndex = 0;
            // 
            // dtgvList
            // 
            this.dtgvList.AllowUserToAddRows = false;
            this.dtgvList.AllowUserToDeleteRows = false;
            this.dtgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvList.BackgroundColor = System.Drawing.Color.White;
            this.dtgvList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dtgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selectg,
            this.Updateg,
            this.Id,
            this.SaleReturnNo,
            this.SaleReturnDate,
            this.BillNo,
            this.CustomerName,
            this.TotalAmt});
            this.tblltMain.SetColumnSpan(this.dtgvList, 8);
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvList.DefaultCellStyle = dataGridViewCellStyle4;
            this.dtgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgvList.Location = new System.Drawing.Point(3, 94);
            this.dtgvList.Name = "dtgvList";
            this.dtgvList.ReadOnly = true;
            this.dtgvList.RowHeadersWidth = 20;
            this.dtgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvList.Size = new System.Drawing.Size(722, 415);
            this.dtgvList.TabIndex = 12;
            this.dtgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvList_CellClick);
            // 
            // Selectg
            // 
            this.Selectg.FillWeight = 35.65482F;
            this.Selectg.HeaderText = "Select";
            this.Selectg.Name = "Selectg";
            this.Selectg.ReadOnly = true;
            // 
            // Updateg
            // 
            this.Updateg.HeaderText = "Update";
            this.Updateg.Name = "Updateg";
            this.Updateg.ReadOnly = true;
            this.Updateg.Visible = false;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // SaleReturnNo
            // 
            this.SaleReturnNo.FillWeight = 69.26904F;
            this.SaleReturnNo.HeaderText = "Sale Return No";
            this.SaleReturnNo.Name = "SaleReturnNo";
            this.SaleReturnNo.ReadOnly = true;
            // 
            // SaleReturnDate
            // 
            this.SaleReturnDate.FillWeight = 81.26904F;
            this.SaleReturnDate.HeaderText = "Sale Return Date";
            this.SaleReturnDate.Name = "SaleReturnDate";
            this.SaleReturnDate.ReadOnly = true;
            // 
            // BillNo
            // 
            this.BillNo.FillWeight = 65.26904F;
            this.BillNo.HeaderText = "Bill No";
            this.BillNo.Name = "BillNo";
            this.BillNo.ReadOnly = true;
            // 
            // CustomerName
            // 
            this.CustomerName.FillWeight = 91.26904F;
            this.CustomerName.HeaderText = "Customer Name";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            // 
            // TotalAmt
            // 
            this.TotalAmt.FillWeight = 65.26904F;
            this.TotalAmt.HeaderText = "Total Amount";
            this.TotalAmt.Name = "TotalAmt";
            this.TotalAmt.ReadOnly = true;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(583, 64);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(68, 26);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnGetAll
            // 
            this.btnGetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnGetAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGetAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAll.ForeColor = System.Drawing.Color.White;
            this.btnGetAll.Location = new System.Drawing.Point(655, 64);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(71, 26);
            this.btnGetAll.TabIndex = 10;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // rbtnDeleteMultipleBill
            // 
            this.rbtnDeleteMultipleBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.rbtnDeleteMultipleBill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tblltMain.SetColumnSpan(this.rbtnDeleteMultipleBill, 2);
            this.rbtnDeleteMultipleBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtnDeleteMultipleBill.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnDeleteMultipleBill.ForeColor = System.Drawing.Color.White;
            this.rbtnDeleteMultipleBill.Location = new System.Drawing.Point(583, 36);
            this.rbtnDeleteMultipleBill.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.rbtnDeleteMultipleBill.Name = "rbtnDeleteMultipleBill";
            this.rbtnDeleteMultipleBill.Size = new System.Drawing.Size(143, 26);
            this.rbtnDeleteMultipleBill.TabIndex = 11;
            this.rbtnDeleteMultipleBill.Text = "Delete Multiple Bills";
            this.rbtnDeleteMultipleBill.UseVisualStyleBackColor = false;
            this.rbtnDeleteMultipleBill.Click += new System.EventHandler(this.rbtnDeleteMultipleBill_Click);
            // 
            // txtsrno
            // 
            this.txtsrno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtsrno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsrno.Location = new System.Drawing.Point(2, 65);
            this.txtsrno.Margin = new System.Windows.Forms.Padding(2);
            this.txtsrno.Name = "txtsrno";
            this.txtsrno.Size = new System.Drawing.Size(119, 25);
            this.txtsrno.TabIndex = 1;
            this.txtsrno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsrno_KeyDown);
            // 
            // frmSaleReturnList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(728, 512);
            this.Controls.Add(this.tblltMain);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(744, 590);
            this.Name = "frmSaleReturnList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SaleReturn List";
            this.Load += new System.EventHandler(this.frmSaleReturnList_Load);
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkbetweendate;
        private System.Windows.Forms.CheckBox chkporderno;
        private System.Windows.Forms.DateTimePicker dtpToPorderDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromPorderdate;
        private System.Windows.Forms.ComboBox cmbcustomername;
        private System.Windows.Forms.CheckBox chksname;
        private System.Windows.Forms.TextBox txtbillno;
        private System.Windows.Forms.CheckBox checkboxbillno;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private RachitControls.NumericTextBox txtsrno;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnGetAll;
        private System.Windows.Forms.Button rbtnDeleteMultipleBill;
        private System.Windows.Forms.DataGridView dtgvList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selectg;
        private System.Windows.Forms.DataGridViewImageColumn Updateg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleReturnNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleReturnDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmt;
    }
}