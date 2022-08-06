namespace AIOInventorySystem.Desk.Forms
{
    partial class frmProjectAtGlanceQuotationList
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
            this.label5 = new System.Windows.Forms.Label();
            this.GVList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRefNo = new System.Windows.Forms.TextBox();
            this.chkRefNo = new System.Windows.Forms.CheckBox();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.cmbEmployeeName = new System.Windows.Forms.ComboBox();
            this.chkEmployeeName = new System.Windows.Forms.CheckBox();
            this.cmbCustomerName = new System.Windows.Forms.ComboBox();
            this.chkCustomerName = new System.Windows.Forms.CheckBox();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblltRow2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tblltRow1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.GVList)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.tblltRow2.SuspendLayout();
            this.tblltRow1.SuspendLayout();
            this.SuspendLayout();
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
            this.label5.TabIndex = 34;
            this.label5.Text = "Project At Glance Quotation List";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GVList
            // 
            this.GVList.AllowUserToAddRows = false;
            this.GVList.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GVList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.GVList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GVList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GVList.Location = new System.Drawing.Point(2, 98);
            this.GVList.Margin = new System.Windows.Forms.Padding(2);
            this.GVList.Name = "GVList";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVList.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GVList.RowHeadersWidth = 15;
            this.GVList.Size = new System.Drawing.Size(880, 412);
            this.GVList.TabIndex = 11;
            this.GVList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GVList_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Update";
            this.Column1.Image = global::AIOInventorySystem.Desk.Properties.Resources.Update;
            this.Column1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Column1.Name = "Column1";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotal.Location = new System.Drawing.Point(794, 2);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(88, 25);
            this.lblTotal.TabIndex = 13;
            this.lblTotal.Text = "0";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(706, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "Total Qut:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRefNo
            // 
            this.txtRefNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtRefNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtRefNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRefNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefNo.Location = new System.Drawing.Point(474, 2);
            this.txtRefNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtRefNo.Name = "txtRefNo";
            this.txtRefNo.Size = new System.Drawing.Size(88, 25);
            this.txtRefNo.TabIndex = 8;
            // 
            // chkRefNo
            // 
            this.chkRefNo.AutoSize = true;
            this.chkRefNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkRefNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRefNo.Location = new System.Drawing.Point(357, 2);
            this.chkRefNo.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkRefNo.Name = "chkRefNo";
            this.chkRefNo.Size = new System.Drawing.Size(113, 25);
            this.chkRefNo.TabIndex = 7;
            this.chkRefNo.Text = "Reference No";
            this.chkRefNo.UseVisualStyleBackColor = true;
            this.chkRefNo.CheckedChanged += new System.EventHandler(this.chkRefNo_CheckedChanged);
            // 
            // dtpToDate
            // 
            this.dtpToDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpToDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Location = new System.Drawing.Point(574, 2);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(110, 25);
            this.dtpToDate.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(539, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "To";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFromDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Location = new System.Drawing.Point(425, 2);
            this.dtpFromDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(110, 25);
            this.dtpFromDate.TabIndex = 3;
            // 
            // chkDate
            // 
            this.chkDate.AutoSize = true;
            this.chkDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDate.Location = new System.Drawing.Point(357, 2);
            this.chkDate.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(64, 25);
            this.chkDate.TabIndex = 2;
            this.chkDate.Text = "Date";
            this.chkDate.UseVisualStyleBackColor = true;
            this.chkDate.CheckedChanged += new System.EventHandler(this.chkDate_CheckedChanged);
            // 
            // cmbEmployeeName
            // 
            this.cmbEmployeeName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbEmployeeName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbEmployeeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbEmployeeName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmployeeName.FormattingEnabled = true;
            this.cmbEmployeeName.Location = new System.Drawing.Point(134, 2);
            this.cmbEmployeeName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbEmployeeName.Name = "cmbEmployeeName";
            this.cmbEmployeeName.Size = new System.Drawing.Size(217, 25);
            this.cmbEmployeeName.TabIndex = 6;
            // 
            // chkEmployeeName
            // 
            this.chkEmployeeName.AutoSize = true;
            this.chkEmployeeName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkEmployeeName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEmployeeName.Location = new System.Drawing.Point(4, 2);
            this.chkEmployeeName.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkEmployeeName.Name = "chkEmployeeName";
            this.chkEmployeeName.Size = new System.Drawing.Size(126, 25);
            this.chkEmployeeName.TabIndex = 5;
            this.chkEmployeeName.Text = "Employee Name";
            this.chkEmployeeName.UseVisualStyleBackColor = true;
            this.chkEmployeeName.CheckedChanged += new System.EventHandler(this.chkEmployeeName_CheckedChanged);
            // 
            // cmbCustomerName
            // 
            this.cmbCustomerName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCustomerName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCustomerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCustomerName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCustomerName.FormattingEnabled = true;
            this.cmbCustomerName.Location = new System.Drawing.Point(134, 2);
            this.cmbCustomerName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCustomerName.Name = "cmbCustomerName";
            this.cmbCustomerName.Size = new System.Drawing.Size(217, 25);
            this.cmbCustomerName.TabIndex = 1;
            // 
            // chkCustomerName
            // 
            this.chkCustomerName.AutoSize = true;
            this.chkCustomerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkCustomerName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCustomerName.Location = new System.Drawing.Point(4, 2);
            this.chkCustomerName.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkCustomerName.Name = "chkCustomerName";
            this.chkCustomerName.Size = new System.Drawing.Size(126, 25);
            this.chkCustomerName.TabIndex = 0;
            this.chkCustomerName.Text = "Customer Name";
            this.chkCustomerName.UseVisualStyleBackColor = true;
            this.chkCustomerName.CheckedChanged += new System.EventHandler(this.chkCustomerName_CheckedChanged);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.tblltRow2, 0, 2);
            this.tblltMain.Controls.Add(this.tblltRow1, 0, 1);
            this.tblltMain.Controls.Add(this.GVList, 0, 3);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.75F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.75F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81F));
            this.tblltMain.Size = new System.Drawing.Size(884, 512);
            this.tblltMain.TabIndex = 0;
            // 
            // tblltRow2
            // 
            this.tblltRow2.ColumnCount = 8;
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.5F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.5F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltRow2.Controls.Add(this.btnGetAll, 5, 0);
            this.tblltRow2.Controls.Add(this.lblTotal, 7, 0);
            this.tblltRow2.Controls.Add(this.btnSearch, 4, 0);
            this.tblltRow2.Controls.Add(this.chkEmployeeName, 0, 0);
            this.tblltRow2.Controls.Add(this.label2, 6, 0);
            this.tblltRow2.Controls.Add(this.cmbEmployeeName, 1, 0);
            this.tblltRow2.Controls.Add(this.chkRefNo, 2, 0);
            this.tblltRow2.Controls.Add(this.txtRefNo, 3, 0);
            this.tblltRow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow2.Location = new System.Drawing.Point(0, 67);
            this.tblltRow2.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow2.Name = "tblltRow2";
            this.tblltRow2.RowCount = 1;
            this.tblltRow2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow2.Size = new System.Drawing.Size(884, 29);
            this.tblltRow2.TabIndex = 5;
            // 
            // btnGetAll
            // 
            this.btnGetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnGetAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGetAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAll.ForeColor = System.Drawing.Color.White;
            this.btnGetAll.Location = new System.Drawing.Point(636, 1);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(66, 27);
            this.btnGetAll.TabIndex = 10;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(566, 1);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(66, 27);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tblltRow1
            // 
            this.tblltRow1.ColumnCount = 7;
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblltRow1.Controls.Add(this.chkCustomerName, 0, 0);
            this.tblltRow1.Controls.Add(this.cmbCustomerName, 1, 0);
            this.tblltRow1.Controls.Add(this.chkDate, 2, 0);
            this.tblltRow1.Controls.Add(this.dtpFromDate, 3, 0);
            this.tblltRow1.Controls.Add(this.label1, 4, 0);
            this.tblltRow1.Controls.Add(this.dtpToDate, 5, 0);
            this.tblltRow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow1.Location = new System.Drawing.Point(0, 38);
            this.tblltRow1.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow1.Name = "tblltRow1";
            this.tblltRow1.RowCount = 1;
            this.tblltRow1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow1.Size = new System.Drawing.Size(884, 29);
            this.tblltRow1.TabIndex = 0;
            // 
            // frmProjectAtGlanceQuotationList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(884, 512);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmProjectAtGlanceQuotationList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Project At Glance Quotation List";
            this.Load += new System.EventHandler(this.frmProjectAtGlanceQuotationList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GVList)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltRow2.ResumeLayout(false);
            this.tblltRow2.PerformLayout();
            this.tblltRow1.ResumeLayout(false);
            this.tblltRow1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbEmployeeName;
        private System.Windows.Forms.CheckBox chkEmployeeName;
        private System.Windows.Forms.ComboBox cmbCustomerName;
        private System.Windows.Forms.CheckBox chkCustomerName;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.CheckBox chkDate;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRefNo;
        private System.Windows.Forms.CheckBox chkRefNo;
        private System.Windows.Forms.DataGridView GVList;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltRow1;
        private System.Windows.Forms.TableLayoutPanel tblltRow2;
        private System.Windows.Forms.Button btnGetAll;
        private System.Windows.Forms.Button btnSearch;
    }
}