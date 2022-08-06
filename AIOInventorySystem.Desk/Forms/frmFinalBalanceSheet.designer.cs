namespace AIOInventorySystem.Desk.Forms
{
    partial class frmFinalBalanceSheet
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tblpnlmain = new System.Windows.Forms.TableLayoutPanel();
            this.GVProfitNLossDr = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnsearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpfromdate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.GVSuppNCustDebitors = new System.Windows.Forms.DataGridView();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.GVCustNSuppCreditors = new System.Windows.Forms.DataGridView();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblpnlmain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVProfitNLossDr)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVSuppNCustDebitors)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVCustNSuppCreditors)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(206, 415);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(681, 0);
            this.progressBar1.TabIndex = 151;
            this.progressBar1.Visible = false;
            // 
            // tblpnlmain
            // 
            this.tblpnlmain.ColumnCount = 1;
            this.tblpnlmain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblpnlmain.Controls.Add(this.GVProfitNLossDr, 0, 2);
            this.tblpnlmain.Controls.Add(this.label9, 0, 0);
            this.tblpnlmain.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tblpnlmain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblpnlmain.Location = new System.Drawing.Point(0, 0);
            this.tblpnlmain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblpnlmain.Name = "tblpnlmain";
            this.tblpnlmain.RowCount = 3;
            this.tblpnlmain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblpnlmain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblpnlmain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89F));
            this.tblpnlmain.Size = new System.Drawing.Size(1084, 562);
            this.tblpnlmain.TabIndex = 0;
            // 
            // GVProfitNLossDr
            // 
            this.GVProfitNLossDr.AllowUserToAddRows = false;
            this.GVProfitNLossDr.AllowUserToDeleteRows = false;
            this.GVProfitNLossDr.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GVProfitNLossDr.BackgroundColor = System.Drawing.Color.White;
            this.GVProfitNLossDr.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVProfitNLossDr.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GVProfitNLossDr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVProfitNLossDr.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this._,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8});
            this.GVProfitNLossDr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GVProfitNLossDr.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GVProfitNLossDr.Location = new System.Drawing.Point(2, 63);
            this.GVProfitNLossDr.Margin = new System.Windows.Forms.Padding(2);
            this.GVProfitNLossDr.Name = "GVProfitNLossDr";
            this.GVProfitNLossDr.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVProfitNLossDr.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.GVProfitNLossDr.RowHeadersWidth = 10;
            this.GVProfitNLossDr.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.GVProfitNLossDr.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GVProfitNLossDr.Size = new System.Drawing.Size(1080, 497);
            this.GVProfitNLossDr.TabIndex = 7;
            this.GVProfitNLossDr.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GVProfitNLossDr_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Id";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Particulars";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // _
            // 
            this._.HeaderText = "_";
            this._.Name = "_";
            this._.ReadOnly = true;
            // 
            // Column3
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column3.HeaderText = "_";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column4
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            dataGridViewCellStyle3.NullValue = null;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column4.HeaderText = "_";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Particulars";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "_";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column7.HeaderText = "_";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column8
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            dataGridViewCellStyle5.NullValue = null;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column8.HeaderText = "_";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
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
            this.label9.Size = new System.Drawing.Size(1084, 33);
            this.label9.TabIndex = 34;
            this.label9.Text = "Balance Sheet";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel1.Controls.Add(this.btnsearch, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpfromdate, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpToDate, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnPrint, 6, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 33);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1084, 28);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(945, 1);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(65, 26);
            this.btnsearch.TabIndex = 3;
            this.btnsearch.Text = "Search";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 24);
            this.label1.TabIndex = 140;
            this.label1.Text = "From";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpfromdate
            // 
            this.dtpfromdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfromdate.Location = new System.Drawing.Point(71, 2);
            this.dtpfromdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpfromdate.Name = "dtpfromdate";
            this.dtpfromdate.Size = new System.Drawing.Size(122, 25);
            this.dtpfromdate.TabIndex = 1;
            this.dtpfromdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpfromdate_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(197, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 24);
            this.label2.TabIndex = 139;
            this.label2.Text = "To";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpToDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(243, 2);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(122, 25);
            this.dtpToDate.TabIndex = 2;
            this.dtpToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpToDate_KeyDown);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(1014, 1);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(68, 26);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.GVSuppNCustDebitors);
            this.panel2.Location = new System.Drawing.Point(735, 243);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(250, 130);
            this.panel2.TabIndex = 155;
            this.panel2.Visible = false;
            // 
            // GVSuppNCustDebitors
            // 
            this.GVSuppNCustDebitors.AllowUserToAddRows = false;
            this.GVSuppNCustDebitors.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVSuppNCustDebitors.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.GVSuppNCustDebitors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVSuppNCustDebitors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column11,
            this.Column12});
            this.GVSuppNCustDebitors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GVSuppNCustDebitors.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GVSuppNCustDebitors.Location = new System.Drawing.Point(0, 0);
            this.GVSuppNCustDebitors.Margin = new System.Windows.Forms.Padding(2);
            this.GVSuppNCustDebitors.Name = "GVSuppNCustDebitors";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVSuppNCustDebitors.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.GVSuppNCustDebitors.RowHeadersWidth = 15;
            this.GVSuppNCustDebitors.Size = new System.Drawing.Size(250, 130);
            this.GVSuppNCustDebitors.TabIndex = 0;
            this.GVSuppNCustDebitors.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GVSuppNCustDebitors_CellClick);
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Particulars";
            this.Column11.Name = "Column11";
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Amount";
            this.Column12.Name = "Column12";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.GVCustNSuppCreditors);
            this.panel1.Location = new System.Drawing.Point(67, 243);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 130);
            this.panel1.TabIndex = 154;
            this.panel1.Visible = false;
            // 
            // GVCustNSuppCreditors
            // 
            this.GVCustNSuppCreditors.AllowUserToAddRows = false;
            this.GVCustNSuppCreditors.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVCustNSuppCreditors.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.GVCustNSuppCreditors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVCustNSuppCreditors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column9,
            this.Column10});
            this.GVCustNSuppCreditors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GVCustNSuppCreditors.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GVCustNSuppCreditors.Location = new System.Drawing.Point(0, 0);
            this.GVCustNSuppCreditors.Margin = new System.Windows.Forms.Padding(2);
            this.GVCustNSuppCreditors.Name = "GVCustNSuppCreditors";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVCustNSuppCreditors.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.GVCustNSuppCreditors.RowHeadersWidth = 15;
            this.GVCustNSuppCreditors.Size = new System.Drawing.Size(250, 130);
            this.GVCustNSuppCreditors.TabIndex = 0;
            this.GVCustNSuppCreditors.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GVCustNSuppCreditors_CellClick);
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Particulars";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Amount";
            this.Column10.Name = "Column10";
            // 
            // frmFinalBalanceSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1084, 562);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tblpnlmain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmFinalBalanceSheet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Balance Sheet";
            this.Load += new System.EventHandler(this.frmFinalBalanceSheet_Load);
            this.tblpnlmain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GVProfitNLossDr)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GVSuppNCustDebitors)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GVCustNSuppCreditors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GVProfitNLossDr;
        private System.Windows.Forms.DateTimePicker dtpfromdate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TableLayoutPanel tblpnlmain;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn _;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView GVSuppNCustDebitors;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView GVCustNSuppCreditors;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Button btnPrint;
    }
}