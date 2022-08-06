namespace AIOInventorySystem.Desk.Forms
{
    partial class frmSendSMSTracking
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
            this.lblSendSMS = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkBillNo = new System.Windows.Forms.CheckBox();
            this.chkMobile = new System.Windows.Forms.CheckBox();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.chkStatus = new System.Windows.Forms.CheckBox();
            this.GvSendSMS = new System.Windows.Forms.DataGridView();
            this.cmbcustomername = new System.Windows.Forms.ComboBox();
            this.chkCustName = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnsearch = new System.Windows.Forms.Button();
            this.btngetall = new System.Windows.Forms.Button();
            this.btnTOdaySMS = new System.Windows.Forms.Button();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.GvSendSMS)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSendSMS
            // 
            this.lblSendSMS.AutoSize = true;
            this.lblSendSMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSendSMS.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSendSMS.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblSendSMS.Location = new System.Drawing.Point(738, 39);
            this.lblSendSMS.Margin = new System.Windows.Forms.Padding(2);
            this.lblSendSMS.Name = "lblSendSMS";
            this.lblSendSMS.Size = new System.Drawing.Size(94, 25);
            this.lblSendSMS.TabIndex = 46;
            this.lblSendSMS.Text = "0.00";
            this.lblSendSMS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(638, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 25);
            this.label1.TabIndex = 45;
            this.label1.Text = "SMS Count:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkBillNo
            // 
            this.chkBillNo.AutoSize = true;
            this.chkBillNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkBillNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBillNo.Location = new System.Drawing.Point(325, 68);
            this.chkBillNo.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkBillNo.Name = "chkBillNo";
            this.chkBillNo.Size = new System.Drawing.Size(68, 25);
            this.chkBillNo.TabIndex = 7;
            this.chkBillNo.Text = "Bill No";
            this.chkBillNo.UseVisualStyleBackColor = true;
            // 
            // chkMobile
            // 
            this.chkMobile.AutoSize = true;
            this.chkMobile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkMobile.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMobile.Location = new System.Drawing.Point(5, 68);
            this.chkMobile.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkMobile.Name = "chkMobile";
            this.chkMobile.Size = new System.Drawing.Size(122, 25);
            this.chkMobile.TabIndex = 5;
            this.chkMobile.Text = "Mobile No";
            this.chkMobile.UseVisualStyleBackColor = true;
            // 
            // cmbStatus
            // 
            this.cmbStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(397, 39);
            this.cmbStatus.Margin = new System.Windows.Forms.Padding(2);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(237, 25);
            this.cmbStatus.TabIndex = 4;
            this.cmbStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbStatus_KeyDown);
            // 
            // chkStatus
            // 
            this.chkStatus.AutoSize = true;
            this.chkStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkStatus.Location = new System.Drawing.Point(325, 39);
            this.chkStatus.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(68, 25);
            this.chkStatus.TabIndex = 3;
            this.chkStatus.Text = "Status";
            this.chkStatus.UseVisualStyleBackColor = true;
            // 
            // GvSendSMS
            // 
            this.GvSendSMS.AllowUserToAddRows = false;
            this.GvSendSMS.AllowUserToDeleteRows = false;
            this.GvSendSMS.BackgroundColor = System.Drawing.Color.White;
            this.GvSendSMS.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GvSendSMS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblltMain.SetColumnSpan(this.GvSendSMS, 6);
            this.GvSendSMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvSendSMS.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvSendSMS.Location = new System.Drawing.Point(3, 98);
            this.GvSendSMS.Name = "GvSendSMS";
            this.GvSendSMS.ReadOnly = true;
            this.GvSendSMS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvSendSMS.Size = new System.Drawing.Size(828, 431);
            this.GvSendSMS.TabIndex = 12;
            // 
            // cmbcustomername
            // 
            this.cmbcustomername.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbcustomername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcustomername.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcustomername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustomername.FormattingEnabled = true;
            this.cmbcustomername.Location = new System.Drawing.Point(131, 39);
            this.cmbcustomername.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustomername.Name = "cmbcustomername";
            this.cmbcustomername.Size = new System.Drawing.Size(187, 25);
            this.cmbcustomername.TabIndex = 2;
            this.cmbcustomername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcustomername_KeyDown);
            // 
            // chkCustName
            // 
            this.chkCustName.AutoSize = true;
            this.chkCustName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkCustName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCustName.Location = new System.Drawing.Point(5, 39);
            this.chkCustName.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkCustName.Name = "chkCustName";
            this.chkCustName.Size = new System.Drawing.Size(122, 25);
            this.chkCustName.TabIndex = 1;
            this.chkCustName.Text = "Customer Name";
            this.chkCustName.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label5, 6);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(834, 37);
            this.label5.TabIndex = 11;
            this.label5.Text = "Sending SMS List";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 6;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.5F));
            this.tblltMain.Controls.Add(this.tblltButtons, 4, 2);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.chkCustName, 0, 1);
            this.tblltMain.Controls.Add(this.chkStatus, 2, 1);
            this.tblltMain.Controls.Add(this.GvSendSMS, 0, 3);
            this.tblltMain.Controls.Add(this.lblSendSMS, 5, 1);
            this.tblltMain.Controls.Add(this.chkMobile, 0, 2);
            this.tblltMain.Controls.Add(this.cmbcustomername, 1, 1);
            this.tblltMain.Controls.Add(this.label1, 4, 1);
            this.tblltMain.Controls.Add(this.cmbStatus, 3, 1);
            this.tblltMain.Controls.Add(this.chkBillNo, 2, 2);
            this.tblltMain.Controls.Add(this.txtMobile, 1, 2);
            this.tblltMain.Controls.Add(this.txtBillNo, 3, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82F));
            this.tblltMain.Size = new System.Drawing.Size(834, 532);
            this.tblltMain.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 3;
            this.tblltMain.SetColumnSpan(this.tblltButtons, 2);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblltButtons.Controls.Add(this.btnsearch, 0, 0);
            this.tblltButtons.Controls.Add(this.btngetall, 2, 0);
            this.tblltButtons.Controls.Add(this.btnTOdaySMS, 1, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(636, 66);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(198, 29);
            this.tblltButtons.TabIndex = 9;
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
            this.btnsearch.Size = new System.Drawing.Size(62, 27);
            this.btnsearch.TabIndex = 9;
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
            this.btngetall.Location = new System.Drawing.Point(134, 1);
            this.btngetall.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btngetall.Name = "btngetall";
            this.btngetall.Size = new System.Drawing.Size(62, 27);
            this.btngetall.TabIndex = 11;
            this.btngetall.Text = "Get All";
            this.btngetall.UseVisualStyleBackColor = false;
            this.btngetall.Click += new System.EventHandler(this.btngetall_Click);
            // 
            // btnTOdaySMS
            // 
            this.btnTOdaySMS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnTOdaySMS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTOdaySMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTOdaySMS.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTOdaySMS.ForeColor = System.Drawing.Color.White;
            this.btnTOdaySMS.Location = new System.Drawing.Point(68, 1);
            this.btnTOdaySMS.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnTOdaySMS.Name = "btnTOdaySMS";
            this.btnTOdaySMS.Size = new System.Drawing.Size(62, 27);
            this.btnTOdaySMS.TabIndex = 10;
            this.btnTOdaySMS.Text = "Today";
            this.btnTOdaySMS.UseVisualStyleBackColor = false;
            this.btnTOdaySMS.Click += new System.EventHandler(this.btnTOdaySMS_Click);
            // 
            // txtMobile
            // 
            this.txtMobile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMobile.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobile.Location = new System.Drawing.Point(131, 68);
            this.txtMobile.Margin = new System.Windows.Forms.Padding(2);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(187, 25);
            this.txtMobile.TabIndex = 6;
            this.txtMobile.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMobile_KeyDown);
            // 
            // txtBillNo
            // 
            this.txtBillNo.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtBillNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillNo.Location = new System.Drawing.Point(397, 68);
            this.txtBillNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.Size = new System.Drawing.Size(100, 25);
            this.txtBillNo.TabIndex = 8;
            this.txtBillNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBillNo_KeyDown);
            // 
            // frmSendSMSTracking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(834, 532);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1134, 634);
            this.Name = "frmSendSMSTracking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Send SMS Tracking";
            this.Load += new System.EventHandler(this.frmSendSMSTracking_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvSendSMS)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GvSendSMS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbcustomername;
        private System.Windows.Forms.CheckBox chkCustName;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.CheckBox chkStatus;
        private System.Windows.Forms.CheckBox chkMobile;
        private System.Windows.Forms.CheckBox chkBillNo;
        private System.Windows.Forms.Label lblSendSMS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.TextBox txtBillNo;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Button btngetall;
        private System.Windows.Forms.Button btnTOdaySMS;
    }
}