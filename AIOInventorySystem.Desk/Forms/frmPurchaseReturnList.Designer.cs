namespace AIOInventorySystem.Desk.Forms
{
    partial class frmPurchaseReturnList
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
            this.chkbetweendate = new System.Windows.Forms.CheckBox();
            this.chkporderno = new System.Windows.Forms.CheckBox();
            this.dtpToPorderDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromPorderdate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbsname = new System.Windows.Forms.ComboBox();
            this.chksname = new System.Windows.Forms.CheckBox();
            this.chkpono = new System.Windows.Forms.CheckBox();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.GvPorderInfo = new System.Windows.Forms.DataGridView();
            this.txtPOrderno = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.txtpono = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.rbtnDeleteMultipleBill = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.Selectg = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Updateg = new System.Windows.Forms.DataGridViewImageColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurchaseReturnNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurchaseReturnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.POrderNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupplierName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblltMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvPorderInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // chkbetweendate
            // 
            this.chkbetweendate.AutoSize = true;
            this.chkbetweendate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkbetweendate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbetweendate.Location = new System.Drawing.Point(270, 38);
            this.chkbetweendate.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkbetweendate.Name = "chkbetweendate";
            this.chkbetweendate.Size = new System.Drawing.Size(92, 24);
            this.chkbetweendate.TabIndex = 5;
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
            this.chkporderno.Location = new System.Drawing.Point(5, 38);
            this.chkporderno.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkporderno.Name = "chkporderno";
            this.chkporderno.Size = new System.Drawing.Size(89, 24);
            this.chkporderno.TabIndex = 1;
            this.chkporderno.Text = "P R No";
            this.chkporderno.UseVisualStyleBackColor = true;
            this.chkporderno.CheckedChanged += new System.EventHandler(this.chkporderno_CheckedChanged);
            this.chkporderno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkporderno_KeyDown);
            // 
            // dtpToPorderDate
            // 
            this.dtpToPorderDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpToPorderDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToPorderDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToPorderDate.Location = new System.Drawing.Point(396, 66);
            this.dtpToPorderDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpToPorderDate.Name = "dtpToPorderDate";
            this.dtpToPorderDate.Size = new System.Drawing.Size(95, 25);
            this.dtpToPorderDate.TabIndex = 7;
            this.dtpToPorderDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpToPorderDate_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(366, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 24);
            this.label2.TabIndex = 19;
            this.label2.Text = "To";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpFromPorderdate
            // 
            this.dtpFromPorderdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFromPorderdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromPorderdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromPorderdate.Location = new System.Drawing.Point(267, 66);
            this.dtpFromPorderdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFromPorderdate.Name = "dtpFromPorderdate";
            this.dtpFromPorderdate.Size = new System.Drawing.Size(95, 25);
            this.dtpFromPorderdate.TabIndex = 6;
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
            this.label5.Size = new System.Drawing.Size(769, 36);
            this.label5.TabIndex = 13;
            this.label5.Text = "Purchase Return List";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbsname
            // 
            this.cmbsname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbsname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbsname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbsname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbsname.FormattingEnabled = true;
            this.cmbsname.Location = new System.Drawing.Point(98, 66);
            this.cmbsname.Margin = new System.Windows.Forms.Padding(2);
            this.cmbsname.Name = "cmbsname";
            this.cmbsname.Size = new System.Drawing.Size(165, 25);
            this.cmbsname.TabIndex = 4;
            this.cmbsname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbsname_KeyDown);
            this.cmbsname.Leave += new System.EventHandler(this.cmbsname_Leave);
            // 
            // chksname
            // 
            this.chksname.AutoSize = true;
            this.chksname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chksname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chksname.Location = new System.Drawing.Point(101, 38);
            this.chksname.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chksname.Name = "chksname";
            this.chksname.Size = new System.Drawing.Size(162, 24);
            this.chksname.TabIndex = 3;
            this.chksname.Text = "Supplier Name";
            this.chksname.UseVisualStyleBackColor = true;
            this.chksname.CheckedChanged += new System.EventHandler(this.chksname_CheckedChanged);
            this.chksname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chksname_KeyDown);
            // 
            // chkpono
            // 
            this.chkpono.AutoSize = true;
            this.chkpono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkpono.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkpono.Location = new System.Drawing.Point(498, 38);
            this.chkpono.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkpono.Name = "chkpono";
            this.chkpono.Size = new System.Drawing.Size(100, 24);
            this.chkpono.TabIndex = 8;
            this.chkpono.Text = "P O No";
            this.chkpono.UseVisualStyleBackColor = true;
            this.chkpono.CheckedChanged += new System.EventHandler(this.chkpono_CheckedChanged);
            this.chkpono.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkpono_KeyDown);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 8;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.75F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.75F));
            this.tblltMain.Controls.Add(this.GvPorderInfo, 0, 3);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.dtpToPorderDate, 4, 2);
            this.tblltMain.Controls.Add(this.dtpFromPorderdate, 2, 2);
            this.tblltMain.Controls.Add(this.chkporderno, 0, 1);
            this.tblltMain.Controls.Add(this.chksname, 1, 1);
            this.tblltMain.Controls.Add(this.chkpono, 5, 1);
            this.tblltMain.Controls.Add(this.cmbsname, 1, 2);
            this.tblltMain.Controls.Add(this.chkbetweendate, 2, 1);
            this.tblltMain.Controls.Add(this.label2, 3, 2);
            this.tblltMain.Controls.Add(this.txtPOrderno, 0, 2);
            this.tblltMain.Controls.Add(this.txtpono, 5, 2);
            this.tblltMain.Controls.Add(this.rbtnDeleteMultipleBill, 6, 1);
            this.tblltMain.Controls.Add(this.btnSearch, 6, 2);
            this.tblltMain.Controls.Add(this.btnGetAll, 7, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82F));
            this.tblltMain.Size = new System.Drawing.Size(769, 522);
            this.tblltMain.TabIndex = 0;
            // 
            // GvPorderInfo
            // 
            this.GvPorderInfo.AllowUserToAddRows = false;
            this.GvPorderInfo.AllowUserToDeleteRows = false;
            this.GvPorderInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvPorderInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvPorderInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GvPorderInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvPorderInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selectg,
            this.Updateg,
            this.Id,
            this.PurchaseReturnNo,
            this.PurchaseReturnDate,
            this.POrderNo,
            this.SupplierName,
            this.TotalAmt});
            this.tblltMain.SetColumnSpan(this.GvPorderInfo, 8);
            this.GvPorderInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvPorderInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvPorderInfo.Location = new System.Drawing.Point(3, 95);
            this.GvPorderInfo.Name = "GvPorderInfo";
            this.GvPorderInfo.RowHeadersWidth = 15;
            this.GvPorderInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvPorderInfo.Size = new System.Drawing.Size(763, 424);
            this.GvPorderInfo.TabIndex = 13;
            this.GvPorderInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvPorderInfo_CellClick);
            // 
            // txtPOrderno
            // 
            this.txtPOrderno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPOrderno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPOrderno.Location = new System.Drawing.Point(2, 66);
            this.txtPOrderno.Margin = new System.Windows.Forms.Padding(2);
            this.txtPOrderno.Name = "txtPOrderno";
            this.txtPOrderno.Size = new System.Drawing.Size(92, 25);
            this.txtPOrderno.TabIndex = 2;
            this.txtPOrderno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPOrderno_KeyDown);
            // 
            // txtpono
            // 
            this.txtpono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtpono.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpono.Location = new System.Drawing.Point(495, 66);
            this.txtpono.Margin = new System.Windows.Forms.Padding(2);
            this.txtpono.Name = "txtpono";
            this.txtpono.Size = new System.Drawing.Size(103, 25);
            this.txtpono.TabIndex = 9;
            this.txtpono.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpono_KeyDown);
            // 
            // rbtnDeleteMultipleBill
            // 
            this.rbtnDeleteMultipleBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.rbtnDeleteMultipleBill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tblltMain.SetColumnSpan(this.rbtnDeleteMultipleBill, 2);
            this.rbtnDeleteMultipleBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtnDeleteMultipleBill.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnDeleteMultipleBill.ForeColor = System.Drawing.Color.White;
            this.rbtnDeleteMultipleBill.Location = new System.Drawing.Point(602, 37);
            this.rbtnDeleteMultipleBill.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.rbtnDeleteMultipleBill.Name = "rbtnDeleteMultipleBill";
            this.rbtnDeleteMultipleBill.Size = new System.Drawing.Size(165, 26);
            this.rbtnDeleteMultipleBill.TabIndex = 12;
            this.rbtnDeleteMultipleBill.Text = "Delete Multiple Bills";
            this.rbtnDeleteMultipleBill.UseVisualStyleBackColor = false;
            this.rbtnDeleteMultipleBill.Click += new System.EventHandler(this.rbtnDeleteMultipleBill_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(602, 65);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(78, 26);
            this.btnSearch.TabIndex = 10;
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
            this.btnGetAll.Location = new System.Drawing.Point(684, 65);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(83, 26);
            this.btnGetAll.TabIndex = 11;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // Selectg
            // 
            this.Selectg.FillWeight = 28.28427F;
            this.Selectg.HeaderText = "Select";
            this.Selectg.Name = "Selectg";
            // 
            // Updateg
            // 
            this.Updateg.HeaderText = "Update";
            this.Updateg.Image = global::AIOInventorySystem.Desk.Properties.Resources.Update;
            this.Updateg.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Updateg.Name = "Updateg";
            this.Updateg.Visible = false;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // PurchaseReturnNo
            // 
            this.PurchaseReturnNo.FillWeight = 95.54314F;
            this.PurchaseReturnNo.HeaderText = "Purchase Return No";
            this.PurchaseReturnNo.Name = "PurchaseReturnNo";
            // 
            // PurchaseReturnDate
            // 
            this.PurchaseReturnDate.FillWeight = 98.54314F;
            this.PurchaseReturnDate.HeaderText = "Purchase Return Date";
            this.PurchaseReturnDate.Name = "PurchaseReturnDate";
            // 
            // POrderNo
            // 
            this.POrderNo.FillWeight = 85.54314F;
            this.POrderNo.HeaderText = "POrder No";
            this.POrderNo.Name = "POrderNo";
            // 
            // SupplierName
            // 
            this.SupplierName.FillWeight = 89.54314F;
            this.SupplierName.HeaderText = "SupplierName";
            this.SupplierName.Name = "SupplierName";
            // 
            // TotalAmt
            // 
            this.TotalAmt.FillWeight = 70.54314F;
            this.TotalAmt.HeaderText = "Total Amount";
            this.TotalAmt.Name = "TotalAmt";
            // 
            // frmPurchaseReturnList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(769, 522);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(785, 590);
            this.Name = "frmPurchaseReturnList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Return List";
            this.Load += new System.EventHandler(this.frmPurchaseReturnList_Load);
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvPorderInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkbetweendate;
        private System.Windows.Forms.CheckBox chkporderno;
        private System.Windows.Forms.DateTimePicker dtpToPorderDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromPorderdate;
        private System.Windows.Forms.ComboBox cmbsname;
        private System.Windows.Forms.CheckBox chksname;
        private System.Windows.Forms.CheckBox chkpono;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Label label5;
        private RachitControls.NumericTextBox txtPOrderno;
        private RachitControls.NumericTextBox txtpono;
        private System.Windows.Forms.Button rbtnDeleteMultipleBill;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnGetAll;
        private System.Windows.Forms.DataGridView GvPorderInfo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selectg;
        private System.Windows.Forms.DataGridViewImageColumn Updateg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurchaseReturnNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurchaseReturnDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn POrderNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmt;
    }
}