namespace AIOInventorySystem.Desk.Forms
{
    partial class frmVehicleReport
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
            this.ChkDate = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtptodate = new System.Windows.Forms.DateTimePicker();
            this.dtgvVehicleReport = new System.Windows.Forms.DataGridView();
            this.dtpfromdate = new System.Windows.Forms.DateTimePicker();
            this.chkVehicleNo = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkCustomer = new System.Windows.Forms.CheckBox();
            this.cmbcustomer = new System.Windows.Forms.ComboBox();
            this.cmbVehicle = new System.Windows.Forms.ComboBox();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvVehicleReport)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChkDate
            // 
            this.ChkDate.AutoSize = true;
            this.ChkDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChkDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDate.Location = new System.Drawing.Point(529, 37);
            this.ChkDate.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.ChkDate.Name = "ChkDate";
            this.ChkDate.Size = new System.Drawing.Size(94, 26);
            this.ChkDate.TabIndex = 5;
            this.ChkDate.Text = "From Date";
            this.ChkDate.UseVisualStyleBackColor = true;
            this.ChkDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChkDate_KeyDown);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label5, 11);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1034, 35);
            this.label5.TabIndex = 13;
            this.label5.Text = "Vehicle Report";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtptodate
            // 
            this.dtptodate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtptodate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtptodate.Location = new System.Drawing.Point(753, 37);
            this.dtptodate.Margin = new System.Windows.Forms.Padding(2);
            this.dtptodate.Name = "dtptodate";
            this.dtptodate.Size = new System.Drawing.Size(89, 25);
            this.dtptodate.TabIndex = 7;
            this.dtptodate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtptodate_KeyDown);
            // 
            // dtgvVehicleReport
            // 
            this.dtgvVehicleReport.AllowUserToAddRows = false;
            this.dtgvVehicleReport.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvVehicleReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvVehicleReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblltMain.SetColumnSpan(this.dtgvVehicleReport, 11);
            this.dtgvVehicleReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvVehicleReport.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgvVehicleReport.Location = new System.Drawing.Point(3, 69);
            this.dtgvVehicleReport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtgvVehicleReport.Name = "dtgvVehicleReport";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvVehicleReport.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtgvVehicleReport.Size = new System.Drawing.Size(1028, 429);
            this.dtgvVehicleReport.TabIndex = 11;
            // 
            // dtpfromdate
            // 
            this.dtpfromdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromdate.Location = new System.Drawing.Point(627, 37);
            this.dtpfromdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpfromdate.Name = "dtpfromdate";
            this.dtpfromdate.Size = new System.Drawing.Size(89, 25);
            this.dtpfromdate.TabIndex = 6;
            this.dtpfromdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpfromdate_KeyDown);
            // 
            // chkVehicleNo
            // 
            this.chkVehicleNo.AutoSize = true;
            this.chkVehicleNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkVehicleNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkVehicleNo.Location = new System.Drawing.Point(326, 37);
            this.chkVehicleNo.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkVehicleNo.Name = "chkVehicleNo";
            this.chkVehicleNo.Size = new System.Drawing.Size(94, 26);
            this.chkVehicleNo.TabIndex = 3;
            this.chkVehicleNo.Text = "Vehicle No";
            this.chkVehicleNo.UseVisualStyleBackColor = true;
            this.chkVehicleNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkVehicleNo_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(720, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 26);
            this.label2.TabIndex = 22;
            this.label2.Text = "To";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkCustomer
            // 
            this.chkCustomer.AutoSize = true;
            this.chkCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkCustomer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCustomer.Location = new System.Drawing.Point(4, 37);
            this.chkCustomer.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkCustomer.Name = "chkCustomer";
            this.chkCustomer.Size = new System.Drawing.Size(123, 26);
            this.chkCustomer.TabIndex = 1;
            this.chkCustomer.Text = "Customer Name";
            this.chkCustomer.UseVisualStyleBackColor = true;
            this.chkCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkCustomer_KeyDown);
            // 
            // cmbcustomer
            // 
            this.cmbcustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcustomer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustomer.FormattingEnabled = true;
            this.cmbcustomer.Location = new System.Drawing.Point(131, 37);
            this.cmbcustomer.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustomer.Name = "cmbcustomer";
            this.cmbcustomer.Size = new System.Drawing.Size(189, 25);
            this.cmbcustomer.TabIndex = 2;
            this.cmbcustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcustomer_KeyDown);
            // 
            // cmbVehicle
            // 
            this.cmbVehicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbVehicle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVehicle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbVehicle.FormattingEnabled = true;
            this.cmbVehicle.ItemHeight = 17;
            this.cmbVehicle.Location = new System.Drawing.Point(424, 37);
            this.cmbVehicle.Margin = new System.Windows.Forms.Padding(2);
            this.cmbVehicle.Name = "cmbVehicle";
            this.cmbVehicle.Size = new System.Drawing.Size(99, 25);
            this.cmbVehicle.TabIndex = 4;
            this.cmbVehicle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbVehicle_KeyDown);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 11;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.75F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.75F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.75F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.25F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.Controls.Add(this.dtptodate, 7, 1);
            this.tblltMain.Controls.Add(this.dtpfromdate, 5, 1);
            this.tblltMain.Controls.Add(this.label2, 6, 1);
            this.tblltMain.Controls.Add(this.btnGetAll, 10, 1);
            this.tblltMain.Controls.Add(this.btnprint, 9, 1);
            this.tblltMain.Controls.Add(this.btnSearch, 8, 1);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.cmbVehicle, 3, 1);
            this.tblltMain.Controls.Add(this.cmbcustomer, 1, 1);
            this.tblltMain.Controls.Add(this.dtgvVehicleReport, 0, 2);
            this.tblltMain.Controls.Add(this.chkCustomer, 0, 1);
            this.tblltMain.Controls.Add(this.chkVehicleNo, 2, 1);
            this.tblltMain.Controls.Add(this.ChkDate, 4, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87F));
            this.tblltMain.Size = new System.Drawing.Size(1034, 502);
            this.tblltMain.TabIndex = 0;
            // 
            // btnGetAll
            // 
            this.btnGetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnGetAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGetAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAll.ForeColor = System.Drawing.Color.White;
            this.btnGetAll.Location = new System.Drawing.Point(970, 36);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(62, 28);
            this.btnGetAll.TabIndex = 10;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(908, 36);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(58, 28);
            this.btnprint.TabIndex = 9;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(846, 36);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(58, 28);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // frmVehicleReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1034, 502);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmVehicleReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vehicle Report";
            ((System.ComponentModel.ISupportInitialize)(this.dtgvVehicleReport)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox ChkDate;
        private System.Windows.Forms.DateTimePicker dtptodate;
        private System.Windows.Forms.DataGridView dtgvVehicleReport;
        private System.Windows.Forms.DateTimePicker dtpfromdate;
        private System.Windows.Forms.CheckBox chkVehicleNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbVehicle;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkCustomer;
        private System.Windows.Forms.ComboBox cmbcustomer;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button btnGetAll;
    }
}