namespace AIOInventorySystem.Desk.Forms
{
    partial class frmSupplierCreditDebitNoteList
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
            this.label5 = new System.Windows.Forms.Label();
            this.tblltRow2 = new System.Windows.Forms.TableLayoutPanel();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpCDTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpCDFrom = new System.Windows.Forms.DateTimePicker();
            this.chkCreditDebitNoteDate = new System.Windows.Forms.CheckBox();
            this.chkBillDate = new System.Windows.Forms.CheckBox();
            this.txtCDNO = new System.Windows.Forms.TextBox();
            this.chkCreditDebitNo = new System.Windows.Forms.CheckBox();
            this.chkCustomerName = new System.Windows.Forms.CheckBox();
            this.cmbCustomerName = new System.Windows.Forms.ComboBox();
            this.chkBillNo = new System.Windows.Forms.CheckBox();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCnt = new System.Windows.Forms.Label();
            this.GVCreditDebitList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.tblltRow2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVCreditDebitList)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
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
            this.label5.Size = new System.Drawing.Size(934, 36);
            this.label5.TabIndex = 15;
            this.label5.Text = "Credit Debit Note List";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltRow2
            // 
            this.tblltRow2.ColumnCount = 8;
            this.tblltMain.SetColumnSpan(this.tblltRow2, 6);
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltRow2.Controls.Add(this.dtpToDate, 7, 0);
            this.tblltRow2.Controls.Add(this.label2, 6, 0);
            this.tblltRow2.Controls.Add(this.dtpFromDate, 5, 0);
            this.tblltRow2.Controls.Add(this.dtpCDTo, 3, 0);
            this.tblltRow2.Controls.Add(this.label1, 2, 0);
            this.tblltRow2.Controls.Add(this.dtpCDFrom, 1, 0);
            this.tblltRow2.Controls.Add(this.chkCreditDebitNoteDate, 0, 0);
            this.tblltRow2.Controls.Add(this.chkBillDate, 4, 0);
            this.tblltRow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow2.Location = new System.Drawing.Point(0, 65);
            this.tblltRow2.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow2.Name = "tblltRow2";
            this.tblltRow2.RowCount = 1;
            this.tblltRow2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow2.Size = new System.Drawing.Size(734, 29);
            this.tblltRow2.TabIndex = 6;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpToDate.Location = new System.Drawing.Point(623, 2);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(109, 25);
            this.dtpToDate.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(588, 3);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "To";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFromDate.Location = new System.Drawing.Point(477, 2);
            this.dtpFromDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(106, 25);
            this.dtpFromDate.TabIndex = 10;
            // 
            // dtpCDTo
            // 
            this.dtpCDTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpCDTo.Location = new System.Drawing.Point(272, 2);
            this.dtpCDTo.Margin = new System.Windows.Forms.Padding(2);
            this.dtpCDTo.Name = "dtpCDTo";
            this.dtpCDTo.Size = new System.Drawing.Size(106, 25);
            this.dtpCDTo.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(236, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "To";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpCDFrom
            // 
            this.dtpCDFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpCDFrom.Location = new System.Drawing.Point(126, 2);
            this.dtpCDFrom.Margin = new System.Windows.Forms.Padding(2);
            this.dtpCDFrom.Name = "dtpCDFrom";
            this.dtpCDFrom.Size = new System.Drawing.Size(106, 25);
            this.dtpCDFrom.TabIndex = 7;
            // 
            // chkCreditDebitNoteDate
            // 
            this.chkCreditDebitNoteDate.AutoSize = true;
            this.chkCreditDebitNoteDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkCreditDebitNoteDate.Location = new System.Drawing.Point(5, 2);
            this.chkCreditDebitNoteDate.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkCreditDebitNoteDate.Name = "chkCreditDebitNoteDate";
            this.chkCreditDebitNoteDate.Size = new System.Drawing.Size(117, 25);
            this.chkCreditDebitNoteDate.TabIndex = 6;
            this.chkCreditDebitNoteDate.Text = "Credit Debit Date";
            this.chkCreditDebitNoteDate.UseVisualStyleBackColor = true;
            // 
            // chkBillDate
            // 
            this.chkBillDate.AutoSize = true;
            this.chkBillDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkBillDate.Location = new System.Drawing.Point(385, 2);
            this.chkBillDate.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkBillDate.Name = "chkBillDate";
            this.chkBillDate.Size = new System.Drawing.Size(88, 25);
            this.chkBillDate.TabIndex = 9;
            this.chkBillDate.Text = "PO Date";
            this.chkBillDate.UseVisualStyleBackColor = true;
            // 
            // txtCDNO
            // 
            this.txtCDNO.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCDNO.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCDNO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCDNO.Location = new System.Drawing.Point(648, 38);
            this.txtCDNO.Margin = new System.Windows.Forms.Padding(2);
            this.txtCDNO.Name = "txtCDNO";
            this.txtCDNO.Size = new System.Drawing.Size(84, 25);
            this.txtCDNO.TabIndex = 5;
            // 
            // chkCreditDebitNo
            // 
            this.chkCreditDebitNo.AutoSize = true;
            this.chkCreditDebitNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkCreditDebitNo.Location = new System.Drawing.Point(521, 38);
            this.chkCreditDebitNo.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkCreditDebitNo.Name = "chkCreditDebitNo";
            this.chkCreditDebitNo.Size = new System.Drawing.Size(123, 25);
            this.chkCreditDebitNo.TabIndex = 4;
            this.chkCreditDebitNo.Text = "Credit Debit No";
            this.chkCreditDebitNo.UseVisualStyleBackColor = true;
            // 
            // chkCustomerName
            // 
            this.chkCustomerName.AutoSize = true;
            this.chkCustomerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkCustomerName.Location = new System.Drawing.Point(5, 38);
            this.chkCustomerName.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkCustomerName.Name = "chkCustomerName";
            this.chkCustomerName.Size = new System.Drawing.Size(114, 25);
            this.chkCustomerName.TabIndex = 0;
            this.chkCustomerName.Text = "Supplier Name";
            this.chkCustomerName.UseVisualStyleBackColor = true;
            // 
            // cmbCustomerName
            // 
            this.cmbCustomerName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCustomerName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCustomerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCustomerName.FormattingEnabled = true;
            this.cmbCustomerName.Location = new System.Drawing.Point(123, 38);
            this.cmbCustomerName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCustomerName.Name = "cmbCustomerName";
            this.cmbCustomerName.Size = new System.Drawing.Size(201, 25);
            this.cmbCustomerName.TabIndex = 1;
            // 
            // chkBillNo
            // 
            this.chkBillNo.AutoSize = true;
            this.chkBillNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkBillNo.Location = new System.Drawing.Point(331, 38);
            this.chkBillNo.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkBillNo.Name = "chkBillNo";
            this.chkBillNo.Size = new System.Drawing.Size(95, 25);
            this.chkBillNo.TabIndex = 2;
            this.chkBillNo.Text = "Porder No";
            this.chkBillNo.UseVisualStyleBackColor = true;
            // 
            // txtBillNo
            // 
            this.txtBillNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtBillNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtBillNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBillNo.Location = new System.Drawing.Point(430, 38);
            this.txtBillNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.Size = new System.Drawing.Size(84, 25);
            this.txtBillNo.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(736, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 25);
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
            this.lblCnt.Location = new System.Drawing.Point(838, 38);
            this.lblCnt.Margin = new System.Windows.Forms.Padding(2);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(94, 25);
            this.lblCnt.TabIndex = 15;
            this.lblCnt.Text = "0";
            this.lblCnt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GVCreditDebitList
            // 
            this.GVCreditDebitList.AllowUserToAddRows = false;
            this.GVCreditDebitList.BackgroundColor = System.Drawing.Color.White;
            this.GVCreditDebitList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVCreditDebitList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.tblltMain.SetColumnSpan(this.GVCreditDebitList, 8);
            this.GVCreditDebitList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GVCreditDebitList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GVCreditDebitList.Location = new System.Drawing.Point(3, 97);
            this.GVCreditDebitList.Name = "GVCreditDebitList";
            this.GVCreditDebitList.RowHeadersWidth = 15;
            this.GVCreditDebitList.Size = new System.Drawing.Size(928, 462);
            this.GVCreditDebitList.TabIndex = 14;
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
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 8;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.Controls.Add(this.tblltRow2, 0, 2);
            this.tblltMain.Controls.Add(this.btnSearch, 6, 2);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.btnGetAll, 7, 2);
            this.tblltMain.Controls.Add(this.chkCustomerName, 0, 1);
            this.tblltMain.Controls.Add(this.chkCreditDebitNo, 4, 1);
            this.tblltMain.Controls.Add(this.cmbCustomerName, 1, 1);
            this.tblltMain.Controls.Add(this.txtCDNO, 5, 1);
            this.tblltMain.Controls.Add(this.chkBillNo, 2, 1);
            this.tblltMain.Controls.Add(this.GVCreditDebitList, 0, 3);
            this.tblltMain.Controls.Add(this.txtBillNo, 3, 1);
            this.tblltMain.Controls.Add(this.label3, 6, 1);
            this.tblltMain.Controls.Add(this.lblCnt, 7, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.25F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.25F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83F));
            this.tblltMain.Size = new System.Drawing.Size(934, 562);
            this.tblltMain.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(749, 67);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(15, 2, 15, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(72, 25);
            this.btnSearch.TabIndex = 12;
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
            this.btnGetAll.Location = new System.Drawing.Point(851, 67);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(15, 2, 15, 2);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(68, 25);
            this.btnGetAll.TabIndex = 13;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // frmSupplierCreditDebitNoteList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(934, 562);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmSupplierCreditDebitNoteList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Supplier Credit Debit Note List";
            this.Load += new System.EventHandler(this.frmSupplierCreditDebitNoteList_Load);
            this.tblltRow2.ResumeLayout(false);
            this.tblltRow2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVCreditDebitList)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tblltRow2;
        private System.Windows.Forms.CheckBox chkCreditDebitNoteDate;
        private System.Windows.Forms.TextBox txtCDNO;
        private System.Windows.Forms.CheckBox chkCreditDebitNo;
        private System.Windows.Forms.CheckBox chkCustomerName;
        private System.Windows.Forms.ComboBox cmbCustomerName;
        private System.Windows.Forms.CheckBox chkBillNo;
        private System.Windows.Forms.TextBox txtBillNo;
        private System.Windows.Forms.DateTimePicker dtpCDFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpCDTo;
        private System.Windows.Forms.CheckBox chkBillDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCnt;
        private System.Windows.Forms.DataGridView GVCreditDebitList;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnGetAll;
    }
}