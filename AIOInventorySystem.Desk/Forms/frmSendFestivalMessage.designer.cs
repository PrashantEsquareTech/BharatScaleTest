namespace AIOInventorySystem.Desk.Forms
{
    partial class frmSendFestivalMessage
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblCustomerCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkSelect = new System.Windows.Forms.CheckBox();
            this.cmbFestival = new System.Windows.Forms.ComboBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.GvSMSReport = new System.Windows.Forms.DataGridView();
            this.cmbCustomerName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMobileNo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.GvCustomer = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.chkRegard = new System.Windows.Forms.CheckBox();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblltFestivalSMS = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.tblltSMS = new System.Windows.Forms.TableLayoutPanel();
            this.tblltList = new System.Windows.Forms.TableLayoutPanel();
            this.btnCustomers = new System.Windows.Forms.Button();
            this.btnSuppliers = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GvSMSReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvCustomer)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.tblltFestivalSMS.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.tblltSMS.SuspendLayout();
            this.tblltList.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(177, 248);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(460, 23);
            this.progressBar1.TabIndex = 173;
            // 
            // lblCustomerCount
            // 
            this.lblCustomerCount.AutoSize = true;
            this.lblCustomerCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCustomerCount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblCustomerCount.Location = new System.Drawing.Point(148, 2);
            this.lblCustomerCount.Margin = new System.Windows.Forms.Padding(2);
            this.lblCustomerCount.Name = "lblCustomerCount";
            this.lblCustomerCount.Size = new System.Drawing.Size(77, 29);
            this.lblCustomerCount.TabIndex = 172;
            this.lblCustomerCount.Text = "---";
            this.lblCustomerCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(2, 2);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(142, 29);
            this.label7.TabIndex = 171;
            this.label7.Text = "Total Customers:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkSelect
            // 
            this.chkSelect.AutoSize = true;
            this.chkSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkSelect.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSelect.Location = new System.Drawing.Point(321, 35);
            this.chkSelect.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.Size = new System.Drawing.Size(84, 29);
            this.chkSelect.TabIndex = 13;
            this.chkSelect.Text = "Select All";
            this.chkSelect.UseVisualStyleBackColor = true;
            this.chkSelect.CheckedChanged += new System.EventHandler(this.chkSelect_CheckedChanged);
            // 
            // cmbFestival
            // 
            this.cmbFestival.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbFestival.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbFestival.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFestival.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFestival.FormattingEnabled = true;
            this.cmbFestival.Location = new System.Drawing.Point(250, 2);
            this.cmbFestival.Margin = new System.Windows.Forms.Padding(2);
            this.cmbFestival.Name = "cmbFestival";
            this.cmbFestival.Size = new System.Drawing.Size(155, 25);
            this.cmbFestival.TabIndex = 9;
            this.cmbFestival.SelectedIndexChanged += new System.EventHandler(this.cmbFestival_SelectedIndexChanged);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label38.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.Location = new System.Drawing.Point(124, 2);
            this.label38.Margin = new System.Windows.Forms.Padding(2);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(122, 24);
            this.label38.TabIndex = 169;
            this.label38.Text = "Select Festivals:";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(2, 2);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 24);
            this.label4.TabIndex = 167;
            this.label4.Text = "Festival SMS";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GvSMSReport
            // 
            this.GvSMSReport.AllowUserToAddRows = false;
            this.GvSMSReport.AllowUserToDeleteRows = false;
            this.GvSMSReport.BackgroundColor = System.Drawing.Color.White;
            this.GvSMSReport.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GvSMSReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblltFestivalSMS.SetColumnSpan(this.GvSMSReport, 2);
            this.GvSMSReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvSMSReport.GridColor = System.Drawing.Color.Red;
            this.GvSMSReport.Location = new System.Drawing.Point(3, 213);
            this.GvSMSReport.Name = "GvSMSReport";
            this.GvSMSReport.ReadOnly = true;
            this.GvSMSReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvSMSReport.Size = new System.Drawing.Size(401, 261);
            this.GvSMSReport.TabIndex = 10;
            this.GvSMSReport.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvSMSReport_CellClick);
            // 
            // cmbCustomerName
            // 
            this.cmbCustomerName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCustomerName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCustomerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCustomerName.DropDownHeight = 110;
            this.cmbCustomerName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCustomerName.FormattingEnabled = true;
            this.cmbCustomerName.IntegralHeight = false;
            this.cmbCustomerName.Items.AddRange(new object[] {
            "Cash",
            "Cheque"});
            this.cmbCustomerName.Location = new System.Drawing.Point(140, 2);
            this.cmbCustomerName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCustomerName.Name = "cmbCustomerName";
            this.cmbCustomerName.Size = new System.Drawing.Size(265, 25);
            this.cmbCustomerName.TabIndex = 1;
            this.cmbCustomerName.SelectedIndexChanged += new System.EventHandler(this.cmbCustomerName_SelectedIndexChanged);
            this.cmbCustomerName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCustomerName_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 24);
            this.label1.TabIndex = 165;
            this.label1.Text = "Customer Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMobileNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobileNo.Location = new System.Drawing.Point(140, 30);
            this.txtMobileNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.Size = new System.Drawing.Size(265, 25);
            this.txtMobileNo.TabIndex = 2;
            this.txtMobileNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMobileNo_KeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(2, 30);
            this.label11.Margin = new System.Windows.Forms.Padding(2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(134, 24);
            this.label11.TabIndex = 161;
            this.label11.Text = "Mobile No:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMessage
            // 
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Enabled = false;
            this.txtMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.Location = new System.Drawing.Point(140, 58);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(2);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.tblltFestivalSMS.SetRowSpan(this.txtMessage, 3);
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMessage.Size = new System.Drawing.Size(265, 80);
            this.txtMessage.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(2, 58);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(134, 24);
            this.label6.TabIndex = 152;
            this.label6.Text = "Festival Message:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GvCustomer
            // 
            this.GvCustomer.AllowUserToAddRows = false;
            this.GvCustomer.AllowUserToDeleteRows = false;
            this.GvCustomer.BackgroundColor = System.Drawing.Color.White;
            this.GvCustomer.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GvCustomer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvCustomer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.tblltList.SetColumnSpan(this.GvCustomer, 4);
            this.GvCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvCustomer.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvCustomer.Location = new System.Drawing.Point(3, 69);
            this.GvCustomer.Name = "GvCustomer";
            this.GvCustomer.RowHeadersWidth = 15;
            this.GvCustomer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvCustomer.Size = new System.Drawing.Size(401, 405);
            this.GvCustomer.TabIndex = 14;
            this.GvCustomer.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvCustomer_CellClick);
            this.GvCustomer.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvCustomer_CellValueChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Select";
            this.Column1.Name = "Column1";
            this.Column1.Width = 50;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(2, 35);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 29);
            this.label3.TabIndex = 145;
            this.label3.Text = "Customer Phone List";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label5, 2);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(814, 35);
            this.label5.TabIndex = 26;
            this.label5.Text = "Send Festival Wishesh SMS";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCount
            // 
            this.txtCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCount.Location = new System.Drawing.Point(327, 2);
            this.txtCount.Margin = new System.Windows.Forms.Padding(2);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(78, 25);
            this.txtCount.TabIndex = 8;
            // 
            // chkRegard
            // 
            this.chkRegard.AutoSize = true;
            this.chkRegard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkRegard.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRegard.Location = new System.Drawing.Point(5, 2);
            this.chkRegard.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkRegard.Name = "chkRegard";
            this.chkRegard.Size = new System.Drawing.Size(111, 29);
            this.chkRegard.TabIndex = 4;
            this.chkRegard.Text = "Add Regards";
            this.chkRegard.UseVisualStyleBackColor = true;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 2;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.tblltFestivalSMS, 0, 1);
            this.tblltMain.Controls.Add(this.tblltList, 1, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 2;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93F));
            this.tblltMain.Size = new System.Drawing.Size(814, 512);
            this.tblltMain.TabIndex = 0;
            // 
            // tblltFestivalSMS
            // 
            this.tblltFestivalSMS.ColumnCount = 2;
            this.tblltFestivalSMS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tblltFestivalSMS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66F));
            this.tblltFestivalSMS.Controls.Add(this.tblltButtons, 0, 5);
            this.tblltFestivalSMS.Controls.Add(this.label1, 0, 0);
            this.tblltFestivalSMS.Controls.Add(this.cmbCustomerName, 1, 0);
            this.tblltFestivalSMS.Controls.Add(this.label11, 0, 1);
            this.tblltFestivalSMS.Controls.Add(this.txtMobileNo, 1, 1);
            this.tblltFestivalSMS.Controls.Add(this.label6, 0, 2);
            this.tblltFestivalSMS.Controls.Add(this.txtMessage, 1, 2);
            this.tblltFestivalSMS.Controls.Add(this.GvSMSReport, 0, 8);
            this.tblltFestivalSMS.Controls.Add(this.tblltSMS, 0, 7);
            this.tblltFestivalSMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltFestivalSMS.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblltFestivalSMS.Location = new System.Drawing.Point(0, 35);
            this.tblltFestivalSMS.Margin = new System.Windows.Forms.Padding(0);
            this.tblltFestivalSMS.Name = "tblltFestivalSMS";
            this.tblltFestivalSMS.RowCount = 9;
            this.tblltFestivalSMS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltFestivalSMS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltFestivalSMS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltFestivalSMS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltFestivalSMS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltFestivalSMS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltFestivalSMS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tblltFestivalSMS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltFestivalSMS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tblltFestivalSMS.Size = new System.Drawing.Size(407, 477);
            this.tblltFestivalSMS.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 5;
            this.tblltFestivalSMS.SetColumnSpan(this.tblltButtons, 2);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltButtons.Controls.Add(this.btnClose, 3, 0);
            this.tblltButtons.Controls.Add(this.btnSend, 2, 0);
            this.tblltButtons.Controls.Add(this.btnNew, 1, 0);
            this.tblltButtons.Controls.Add(this.txtCount, 4, 0);
            this.tblltButtons.Controls.Add(this.chkRegard, 0, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 140);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(407, 33);
            this.tblltButtons.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(258, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(65, 29);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(189, 2);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(65, 29);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(120, 2);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(65, 29);
            this.btnNew.TabIndex = 6;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // tblltSMS
            // 
            this.tblltSMS.ColumnCount = 3;
            this.tblltFestivalSMS.SetColumnSpan(this.tblltSMS, 2);
            this.tblltSMS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltSMS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31F));
            this.tblltSMS.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39F));
            this.tblltSMS.Controls.Add(this.label4, 0, 0);
            this.tblltSMS.Controls.Add(this.label38, 1, 0);
            this.tblltSMS.Controls.Add(this.cmbFestival, 2, 0);
            this.tblltSMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltSMS.Location = new System.Drawing.Point(0, 182);
            this.tblltSMS.Margin = new System.Windows.Forms.Padding(0);
            this.tblltSMS.Name = "tblltSMS";
            this.tblltSMS.RowCount = 1;
            this.tblltSMS.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltSMS.Size = new System.Drawing.Size(407, 28);
            this.tblltSMS.TabIndex = 9;
            // 
            // tblltList
            // 
            this.tblltList.ColumnCount = 4;
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblltList.Controls.Add(this.btnCustomers, 2, 1);
            this.tblltList.Controls.Add(this.label7, 0, 0);
            this.tblltList.Controls.Add(this.GvCustomer, 0, 2);
            this.tblltList.Controls.Add(this.chkSelect, 3, 1);
            this.tblltList.Controls.Add(this.btnSuppliers, 1, 1);
            this.tblltList.Controls.Add(this.lblCustomerCount, 1, 0);
            this.tblltList.Controls.Add(this.label3, 0, 1);
            this.tblltList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblltList.Location = new System.Drawing.Point(407, 35);
            this.tblltList.Margin = new System.Windows.Forms.Padding(0);
            this.tblltList.Name = "tblltList";
            this.tblltList.RowCount = 3;
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86F));
            this.tblltList.Size = new System.Drawing.Size(407, 477);
            this.tblltList.TabIndex = 11;
            // 
            // btnCustomers
            // 
            this.btnCustomers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnCustomers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCustomers.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCustomers.ForeColor = System.Drawing.Color.White;
            this.btnCustomers.Location = new System.Drawing.Point(229, 35);
            this.btnCustomers.Margin = new System.Windows.Forms.Padding(2);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.Size = new System.Drawing.Size(85, 29);
            this.btnCustomers.TabIndex = 12;
            this.btnCustomers.Text = "Customers";
            this.btnCustomers.UseVisualStyleBackColor = false;
            this.btnCustomers.Click += new System.EventHandler(this.btnCustomers_Click);
            // 
            // btnSuppliers
            // 
            this.btnSuppliers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSuppliers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSuppliers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSuppliers.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSuppliers.ForeColor = System.Drawing.Color.White;
            this.btnSuppliers.Location = new System.Drawing.Point(148, 35);
            this.btnSuppliers.Margin = new System.Windows.Forms.Padding(2);
            this.btnSuppliers.Name = "btnSuppliers";
            this.btnSuppliers.Size = new System.Drawing.Size(77, 29);
            this.btnSuppliers.TabIndex = 11;
            this.btnSuppliers.Text = "Suppliers";
            this.btnSuppliers.UseVisualStyleBackColor = false;
            this.btnSuppliers.Click += new System.EventHandler(this.btnSuppliers_Click);
            // 
            // frmSendFestivalMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(814, 512);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "frmSendFestivalMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Send Festival Message";
            this.Load += new System.EventHandler(this.frmSendFestivalMessage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvSMSReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvCustomer)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.tblltFestivalSMS.ResumeLayout(false);
            this.tblltFestivalSMS.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.tblltButtons.PerformLayout();
            this.tblltSMS.ResumeLayout(false);
            this.tblltSMS.PerformLayout();
            this.tblltList.ResumeLayout(false);
            this.tblltList.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView GvCustomer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMobileNo;
        private System.Windows.Forms.ComboBox cmbCustomerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView GvSMSReport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbFestival;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.CheckBox chkSelect;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCustomerCount;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.CheckBox chkRegard;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltFestivalSMS;
        private System.Windows.Forms.TableLayoutPanel tblltSMS;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.TableLayoutPanel tblltList;
        private System.Windows.Forms.Button btnSuppliers;
        private System.Windows.Forms.Button btnCustomers;
    }
}