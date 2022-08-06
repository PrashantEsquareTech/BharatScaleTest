namespace AIOInventorySystem.Desk.Forms
{
    partial class frmCreditList
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
            this.grpboxSMS = new System.Windows.Forms.GroupBox();
            this.tblltSendSMSGroup = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.chkpaid = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkbetweendate = new System.Windows.Forms.CheckBox();
            this.chkbillno = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.GvBillInfo = new System.Windows.Forms.DataGridView();
            this.cmbcustomername = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtptodate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpfromdate = new System.Windows.Forms.DateTimePicker();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnSMSReport = new System.Windows.Forms.Button();
            this.btnSendSMS = new System.Windows.Forms.Button();
            this.btngetall = new System.Windows.Forms.Button();
            this.btnsearch = new System.Windows.Forms.Button();
            this.grpboxSMS.SuspendLayout();
            this.tblltSendSMSGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvBillInfo)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpboxSMS
            // 
            this.grpboxSMS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpboxSMS.Controls.Add(this.tblltSendSMSGroup);
            this.grpboxSMS.Location = new System.Drawing.Point(199, 253);
            this.grpboxSMS.Margin = new System.Windows.Forms.Padding(1);
            this.grpboxSMS.Name = "grpboxSMS";
            this.grpboxSMS.Padding = new System.Windows.Forms.Padding(1);
            this.grpboxSMS.Size = new System.Drawing.Size(630, 84);
            this.grpboxSMS.TabIndex = 75;
            this.grpboxSMS.TabStop = false;
            this.grpboxSMS.Text = "Send Credit SMS";
            // 
            // tblltSendSMSGroup
            // 
            this.tblltSendSMSGroup.ColumnCount = 1;
            this.tblltSendSMSGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltSendSMSGroup.Controls.Add(this.label3, 0, 0);
            this.tblltSendSMSGroup.Controls.Add(this.progressBar1, 0, 1);
            this.tblltSendSMSGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltSendSMSGroup.Location = new System.Drawing.Point(1, 19);
            this.tblltSendSMSGroup.Margin = new System.Windows.Forms.Padding(0);
            this.tblltSendSMSGroup.Name = "tblltSendSMSGroup";
            this.tblltSendSMSGroup.RowCount = 3;
            this.tblltSendSMSGroup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tblltSendSMSGroup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblltSendSMSGroup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltSendSMSGroup.Size = new System.Drawing.Size(628, 64);
            this.tblltSendSMSGroup.TabIndex = 60;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(2, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(624, 31);
            this.label3.TabIndex = 59;
            this.label3.Text = "Send Credit SMS";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(2, 37);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(624, 18);
            this.progressBar1.TabIndex = 58;
            // 
            // chkpaid
            // 
            this.chkpaid.AutoSize = true;
            this.chkpaid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkpaid.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkpaid.Location = new System.Drawing.Point(468, 39);
            this.chkpaid.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkpaid.Name = "chkpaid";
            this.chkpaid.Size = new System.Drawing.Size(57, 21);
            this.chkpaid.TabIndex = 6;
            this.chkpaid.Text = "Paid";
            this.chkpaid.UseVisualStyleBackColor = true;
            this.chkpaid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkpaid_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(191, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(18, 30);
            this.label7.TabIndex = 73;
            this.label7.Text = "*";
            // 
            // chkbetweendate
            // 
            this.chkbetweendate.AutoSize = true;
            this.chkbetweendate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkbetweendate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbetweendate.Location = new System.Drawing.Point(217, 39);
            this.chkbetweendate.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkbetweendate.Name = "chkbetweendate";
            this.chkbetweendate.Size = new System.Drawing.Size(92, 21);
            this.chkbetweendate.TabIndex = 3;
            this.chkbetweendate.Text = "Date";
            this.chkbetweendate.UseVisualStyleBackColor = true;
            this.chkbetweendate.CheckedChanged += new System.EventHandler(this.chkbetweendate_CheckedChanged);
            this.chkbetweendate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkbetweendate_KeyDown);
            // 
            // chkbillno
            // 
            this.chkbillno.AutoSize = true;
            this.chkbillno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkbillno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbillno.Location = new System.Drawing.Point(5, 39);
            this.chkbillno.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkbillno.Name = "chkbillno";
            this.chkbillno.Size = new System.Drawing.Size(181, 21);
            this.chkbillno.TabIndex = 1;
            this.chkbillno.Text = "Customer Name";
            this.chkbillno.UseVisualStyleBackColor = true;
            this.chkbillno.CheckedChanged += new System.EventHandler(this.chkbillno_CheckedChanged);
            this.chkbillno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkbillno_KeyDown);
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
            this.label5.Size = new System.Drawing.Size(994, 37);
            this.label5.TabIndex = 11;
            this.label5.Text = "Credit List";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.tblltMain.SetColumnSpan(this.GvBillInfo, 11);
            this.GvBillInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvBillInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvBillInfo.Location = new System.Drawing.Point(3, 96);
            this.GvBillInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GvBillInfo.Name = "GvBillInfo";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvBillInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GvBillInfo.Size = new System.Drawing.Size(988, 432);
            this.GvBillInfo.TabIndex = 12;
            this.GvBillInfo.TabStop = false;
            this.GvBillInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvBillInfo_CellClick);
            this.GvBillInfo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GvBillInfo_KeyPress);
            // 
            // cmbcustomername
            // 
            this.cmbcustomername.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcustomername.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcustomername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcustomername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustomername.FormattingEnabled = true;
            this.cmbcustomername.Location = new System.Drawing.Point(2, 64);
            this.cmbcustomername.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustomername.Name = "cmbcustomername";
            this.cmbcustomername.Size = new System.Drawing.Size(184, 25);
            this.cmbcustomername.TabIndex = 2;
            this.cmbcustomername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcustomername_KeyDown);
            this.cmbcustomername.Leave += new System.EventHandler(this.cmbcustomername_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(442, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 30);
            this.label1.TabIndex = 74;
            this.label1.Text = "*";
            // 
            // dtptodate
            // 
            this.dtptodate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtptodate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtptodate.Location = new System.Drawing.Point(342, 64);
            this.dtptodate.Margin = new System.Windows.Forms.Padding(2);
            this.dtptodate.Name = "dtptodate";
            this.dtptodate.Size = new System.Drawing.Size(95, 25);
            this.dtptodate.TabIndex = 5;
            this.dtptodate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtptodate_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(313, 64);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 26);
            this.label2.TabIndex = 71;
            this.label2.Text = "To";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpfromdate
            // 
            this.dtpfromdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfromdate.Location = new System.Drawing.Point(214, 64);
            this.dtpfromdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpfromdate.Name = "dtpfromdate";
            this.dtpfromdate.Size = new System.Drawing.Size(95, 25);
            this.dtpfromdate.TabIndex = 4;
            this.dtpfromdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpfromdate_KeyDown);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 11;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.5F));
            this.tblltMain.Controls.Add(this.btnSMSReport, 10, 2);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.btnSendSMS, 9, 2);
            this.tblltMain.Controls.Add(this.chkbillno, 0, 1);
            this.tblltMain.Controls.Add(this.btngetall, 8, 2);
            this.tblltMain.Controls.Add(this.GvBillInfo, 0, 3);
            this.tblltMain.Controls.Add(this.btnsearch, 7, 2);
            this.tblltMain.Controls.Add(this.dtpfromdate, 2, 2);
            this.tblltMain.Controls.Add(this.cmbcustomername, 0, 2);
            this.tblltMain.Controls.Add(this.chkpaid, 6, 1);
            this.tblltMain.Controls.Add(this.label7, 1, 2);
            this.tblltMain.Controls.Add(this.chkbetweendate, 2, 1);
            this.tblltMain.Controls.Add(this.label2, 3, 2);
            this.tblltMain.Controls.Add(this.dtptodate, 4, 2);
            this.tblltMain.Controls.Add(this.label1, 5, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.75F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.75F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.5F));
            this.tblltMain.Size = new System.Drawing.Size(994, 532);
            this.tblltMain.TabIndex = 0;
            this.tblltMain.TabStop = true;
            // 
            // btnSMSReport
            // 
            this.btnSMSReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSMSReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSMSReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSMSReport.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSMSReport.ForeColor = System.Drawing.Color.White;
            this.btnSMSReport.Location = new System.Drawing.Point(834, 63);
            this.btnSMSReport.Margin = new System.Windows.Forms.Padding(1);
            this.btnSMSReport.Name = "btnSMSReport";
            this.btnSMSReport.Size = new System.Drawing.Size(159, 28);
            this.btnSMSReport.TabIndex = 10;
            this.btnSMSReport.Text = "Show Send SMS Report";
            this.btnSMSReport.UseVisualStyleBackColor = false;
            this.btnSMSReport.Click += new System.EventHandler(this.btnSMSReport_Click);
            // 
            // btnSendSMS
            // 
            this.btnSendSMS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSendSMS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSendSMS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSendSMS.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendSMS.ForeColor = System.Drawing.Color.White;
            this.btnSendSMS.Location = new System.Drawing.Point(666, 63);
            this.btnSendSMS.Margin = new System.Windows.Forms.Padding(1);
            this.btnSendSMS.Name = "btnSendSMS";
            this.btnSendSMS.Size = new System.Drawing.Size(166, 28);
            this.btnSendSMS.TabIndex = 9;
            this.btnSendSMS.Text = "Send Credit SMS to ALL";
            this.btnSendSMS.UseVisualStyleBackColor = false;
            this.btnSendSMS.Click += new System.EventHandler(this.btnSendSMS_Click);
            // 
            // btngetall
            // 
            this.btngetall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btngetall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btngetall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btngetall.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngetall.ForeColor = System.Drawing.Color.White;
            this.btngetall.Location = new System.Drawing.Point(597, 63);
            this.btngetall.Margin = new System.Windows.Forms.Padding(1);
            this.btngetall.Name = "btngetall";
            this.btngetall.Size = new System.Drawing.Size(67, 28);
            this.btngetall.TabIndex = 8;
            this.btngetall.Text = "Get All";
            this.btngetall.UseVisualStyleBackColor = false;
            this.btngetall.Click += new System.EventHandler(this.btngetall_Click);
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(528, 63);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(1);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(67, 28);
            this.btnsearch.TabIndex = 7;
            this.btnsearch.Text = "Search";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // frmCreditList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(994, 532);
            this.Controls.Add(this.grpboxSMS);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmCreditList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Credit List";
            this.Load += new System.EventHandler(this.frmCreditList_Load);
            this.grpboxSMS.ResumeLayout(false);
            this.tblltSendSMSGroup.ResumeLayout(false);
            this.tblltSendSMSGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvBillInfo)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpboxSMS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox chkpaid;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkbetweendate;
        private System.Windows.Forms.CheckBox chkbillno;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView GvBillInfo;
        private System.Windows.Forms.ComboBox cmbcustomername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtptodate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpfromdate;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Button btnSMSReport;
        private System.Windows.Forms.Button btnSendSMS;
        private System.Windows.Forms.Button btngetall;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.TableLayoutPanel tblltSendSMSGroup;

    }
}