namespace AIOInventorySystem.Desk.Forms
{
    partial class frmIncompleteorderbook
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
            this.GvorderInfo = new System.Windows.Forms.DataGridView();
            this.chkcompletedc = new System.Windows.Forms.CheckBox();
            this.chkorderno = new System.Windows.Forms.CheckBox();
            this.cmbcustomername = new System.Windows.Forms.ComboBox();
            this.chkcustname = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.dtpdeliveryfromdate = new System.Windows.Forms.DateTimePicker();
            this.chkDeliverydate = new System.Windows.Forms.CheckBox();
            this.dtpdeliverytodate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.tblltRow2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtorderno = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.rbtnDeleteMultipleBill = new System.Windows.Forms.Button();
            this.btndelivery = new System.Windows.Forms.Button();
            this.btnTodaysList = new System.Windows.Forms.Button();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.lblTotalBill = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.Selectg = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DeleteBookingNo = new System.Windows.Forms.DataGridViewImageColumn();
            this.CustomerBill = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.GvorderInfo)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.tblltRow2.SuspendLayout();
            this.SuspendLayout();
            // 
            // GvorderInfo
            // 
            this.GvorderInfo.AllowUserToAddRows = false;
            this.GvorderInfo.AllowUserToDeleteRows = false;
            this.GvorderInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvorderInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvorderInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvorderInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvorderInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvorderInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selectg,
            this.DeleteBookingNo,
            this.CustomerBill});
            this.tblltMain.SetColumnSpan(this.GvorderInfo, 7);
            this.GvorderInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvorderInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvorderInfo.Location = new System.Drawing.Point(3, 96);
            this.GvorderInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GvorderInfo.Name = "GvorderInfo";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvorderInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GvorderInfo.RowHeadersWidth = 15;
            this.GvorderInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvorderInfo.Size = new System.Drawing.Size(898, 392);
            this.GvorderInfo.TabIndex = 14;
            this.GvorderInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvorderInfo_CellClick);
            this.GvorderInfo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GvorderInfo_KeyPress);
            // 
            // chkcompletedc
            // 
            this.chkcompletedc.AutoSize = true;
            this.chkcompletedc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkcompletedc.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcompletedc.ForeColor = System.Drawing.Color.Crimson;
            this.chkcompletedc.Location = new System.Drawing.Point(706, 36);
            this.chkcompletedc.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkcompletedc.Name = "chkcompletedc";
            this.chkcompletedc.Size = new System.Drawing.Size(196, 25);
            this.chkcompletedc.TabIndex = 6;
            this.chkcompletedc.Text = "Complete Order Booking";
            this.chkcompletedc.UseVisualStyleBackColor = true;
            this.chkcompletedc.CheckedChanged += new System.EventHandler(this.chkcompletedc_CheckedChanged);
            // 
            // chkorderno
            // 
            this.chkorderno.AutoSize = true;
            this.chkorderno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkorderno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkorderno.Location = new System.Drawing.Point(4, 65);
            this.chkorderno.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkorderno.Name = "chkorderno";
            this.chkorderno.Size = new System.Drawing.Size(143, 25);
            this.chkorderno.TabIndex = 7;
            this.chkorderno.Text = "Order Booking No";
            this.chkorderno.UseVisualStyleBackColor = true;
            this.chkorderno.CheckedChanged += new System.EventHandler(this.chkorderno_CheckedChanged);
            this.chkorderno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkorderno_KeyDown);
            // 
            // cmbcustomername
            // 
            this.cmbcustomername.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcustomername.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcustomername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcustomername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustomername.FormattingEnabled = true;
            this.cmbcustomername.Location = new System.Drawing.Point(151, 36);
            this.cmbcustomername.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustomername.Name = "cmbcustomername";
            this.cmbcustomername.Size = new System.Drawing.Size(194, 25);
            this.cmbcustomername.TabIndex = 2;
            this.cmbcustomername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcustomername_KeyDown);
            this.cmbcustomername.Leave += new System.EventHandler(this.cmbcustomername_Leave);
            // 
            // chkcustname
            // 
            this.chkcustname.AutoSize = true;
            this.chkcustname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkcustname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcustname.Location = new System.Drawing.Point(4, 36);
            this.chkcustname.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkcustname.Name = "chkcustname";
            this.chkcustname.Size = new System.Drawing.Size(143, 25);
            this.chkcustname.TabIndex = 1;
            this.chkcustname.Text = "Customer Name";
            this.chkcustname.UseVisualStyleBackColor = true;
            this.chkcustname.CheckedChanged += new System.EventHandler(this.chkcustname_CheckedChanged);
            this.chkcustname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkcustname_KeyDown);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label5, 7);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(904, 34);
            this.label5.TabIndex = 36;
            this.label5.Text = "Order Booking List";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 7;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.GvorderInfo, 0, 3);
            this.tblltMain.Controls.Add(this.chkcustname, 0, 1);
            this.tblltMain.Controls.Add(this.chkorderno, 0, 2);
            this.tblltMain.Controls.Add(this.cmbcustomername, 1, 1);
            this.tblltMain.Controls.Add(this.dtpdeliveryfromdate, 3, 1);
            this.tblltMain.Controls.Add(this.chkDeliverydate, 2, 1);
            this.tblltMain.Controls.Add(this.dtpdeliverytodate, 5, 1);
            this.tblltMain.Controls.Add(this.label1, 4, 1);
            this.tblltMain.Controls.Add(this.chkcompletedc, 6, 1);
            this.tblltMain.Controls.Add(this.tblltRow2, 1, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81F));
            this.tblltMain.Size = new System.Drawing.Size(904, 492);
            this.tblltMain.TabIndex = 0;
            // 
            // dtpdeliveryfromdate
            // 
            this.dtpdeliveryfromdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpdeliveryfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpdeliveryfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpdeliveryfromdate.Location = new System.Drawing.Point(416, 36);
            this.dtpdeliveryfromdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpdeliveryfromdate.Name = "dtpdeliveryfromdate";
            this.dtpdeliveryfromdate.Size = new System.Drawing.Size(122, 25);
            this.dtpdeliveryfromdate.TabIndex = 4;
            this.dtpdeliveryfromdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpdeliveryfromdate_KeyDown);
            // 
            // chkDeliverydate
            // 
            this.chkDeliverydate.AutoSize = true;
            this.chkDeliverydate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkDeliverydate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDeliverydate.Location = new System.Drawing.Point(351, 36);
            this.chkDeliverydate.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkDeliverydate.Name = "chkDeliverydate";
            this.chkDeliverydate.Size = new System.Drawing.Size(61, 25);
            this.chkDeliverydate.TabIndex = 3;
            this.chkDeliverydate.Text = " Date";
            this.chkDeliverydate.UseVisualStyleBackColor = true;
            this.chkDeliverydate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkDeliverydate_KeyDown);
            // 
            // dtpdeliverytodate
            // 
            this.dtpdeliverytodate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpdeliverytodate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpdeliverytodate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpdeliverytodate.Location = new System.Drawing.Point(578, 36);
            this.dtpdeliverytodate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpdeliverytodate.Name = "dtpdeliverytodate";
            this.dtpdeliverytodate.Size = new System.Drawing.Size(122, 25);
            this.dtpdeliverytodate.TabIndex = 5;
            this.dtpdeliverytodate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpdeliverytodate_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(542, 36);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 25);
            this.label1.TabIndex = 68;
            this.label1.Text = "To";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltRow2
            // 
            this.tblltRow2.ColumnCount = 8;
            this.tblltMain.SetColumnSpan(this.tblltRow2, 6);
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.5F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.5F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltRow2.Controls.Add(this.txtorderno, 0, 0);
            this.tblltRow2.Controls.Add(this.rbtnDeleteMultipleBill, 5, 0);
            this.tblltRow2.Controls.Add(this.btndelivery, 4, 0);
            this.tblltRow2.Controls.Add(this.btnTodaysList, 2, 0);
            this.tblltRow2.Controls.Add(this.btnGetAll, 3, 0);
            this.tblltRow2.Controls.Add(this.lblTotalBill, 7, 0);
            this.tblltRow2.Controls.Add(this.label8, 6, 0);
            this.tblltRow2.Controls.Add(this.btnSearch, 1, 0);
            this.tblltRow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow2.Location = new System.Drawing.Point(149, 63);
            this.tblltRow2.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow2.Name = "tblltRow2";
            this.tblltRow2.RowCount = 1;
            this.tblltRow2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow2.Size = new System.Drawing.Size(755, 29);
            this.tblltRow2.TabIndex = 8;
            // 
            // txtorderno
            // 
            this.txtorderno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtorderno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtorderno.Location = new System.Drawing.Point(2, 2);
            this.txtorderno.Margin = new System.Windows.Forms.Padding(2);
            this.txtorderno.Name = "txtorderno";
            this.txtorderno.Size = new System.Drawing.Size(79, 25);
            this.txtorderno.TabIndex = 8;
            this.txtorderno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtorderno_KeyDown);
            // 
            // rbtnDeleteMultipleBill
            // 
            this.rbtnDeleteMultipleBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.rbtnDeleteMultipleBill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.rbtnDeleteMultipleBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtnDeleteMultipleBill.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnDeleteMultipleBill.ForeColor = System.Drawing.Color.White;
            this.rbtnDeleteMultipleBill.Location = new System.Drawing.Point(462, 1);
            this.rbtnDeleteMultipleBill.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.rbtnDeleteMultipleBill.Name = "rbtnDeleteMultipleBill";
            this.rbtnDeleteMultipleBill.Size = new System.Drawing.Size(139, 27);
            this.rbtnDeleteMultipleBill.TabIndex = 13;
            this.rbtnDeleteMultipleBill.Text = "Delete Multiple Bill";
            this.rbtnDeleteMultipleBill.UseVisualStyleBackColor = false;
            this.rbtnDeleteMultipleBill.Click += new System.EventHandler(this.rbtnDeleteMultipleBill_Click);
            // 
            // btndelivery
            // 
            this.btndelivery.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btndelivery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btndelivery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btndelivery.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelivery.ForeColor = System.Drawing.Color.White;
            this.btndelivery.Location = new System.Drawing.Point(311, 1);
            this.btndelivery.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btndelivery.Name = "btndelivery";
            this.btndelivery.Size = new System.Drawing.Size(147, 27);
            this.btndelivery.TabIndex = 12;
            this.btndelivery.Text = "Todays Delivery List";
            this.btndelivery.UseVisualStyleBackColor = false;
            this.btndelivery.Click += new System.EventHandler(this.btndelivery_Click);
            // 
            // btnTodaysList
            // 
            this.btnTodaysList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnTodaysList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTodaysList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTodaysList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTodaysList.ForeColor = System.Drawing.Color.White;
            this.btnTodaysList.Location = new System.Drawing.Point(149, 1);
            this.btnTodaysList.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnTodaysList.Name = "btnTodaysList";
            this.btnTodaysList.Size = new System.Drawing.Size(94, 27);
            this.btnTodaysList.TabIndex = 10;
            this.btnTodaysList.Text = "Today\'s List";
            this.btnTodaysList.UseVisualStyleBackColor = false;
            this.btnTodaysList.Click += new System.EventHandler(this.btnTodaysList_Click);
            // 
            // btnGetAll
            // 
            this.btnGetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnGetAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGetAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAll.ForeColor = System.Drawing.Color.White;
            this.btnGetAll.Location = new System.Drawing.Point(247, 1);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(60, 27);
            this.btnGetAll.TabIndex = 11;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // lblTotalBill
            // 
            this.lblTotalBill.AutoSize = true;
            this.lblTotalBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalBill.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBill.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalBill.Location = new System.Drawing.Point(688, 2);
            this.lblTotalBill.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotalBill.Name = "lblTotalBill";
            this.lblTotalBill.Size = new System.Drawing.Size(65, 25);
            this.lblTotalBill.TabIndex = 89;
            this.lblTotalBill.Text = "0";
            this.lblTotalBill.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(605, 2);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(79, 25);
            this.label8.TabIndex = 88;
            this.label8.Text = "Total Bills:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(85, 1);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(60, 27);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click_1);
            // 
            // Selectg
            // 
            this.Selectg.HeaderText = "Select";
            this.Selectg.Name = "Selectg";
            this.Selectg.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Selectg.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // DeleteBookingNo
            // 
            this.DeleteBookingNo.HeaderText = "Delete Order Booking";
            this.DeleteBookingNo.Image = global::AIOInventorySystem.Desk.Properties.Resources.button_delete_order__1_;
            this.DeleteBookingNo.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.DeleteBookingNo.Name = "DeleteBookingNo";
            // 
            // CustomerBill
            // 
            this.CustomerBill.HeaderText = "Customer Bill";
            this.CustomerBill.Image = global::AIOInventorySystem.Desk.Properties.Resources.custbill;
            this.CustomerBill.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.CustomerBill.Name = "CustomerBill";
            // 
            // frmIncompleteorderbook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(904, 492);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmIncompleteorderbook";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Incomplete Order Book List";
            this.Load += new System.EventHandler(this.frmIncompleteorderbook_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvorderInfo)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.tblltRow2.ResumeLayout(false);
            this.tblltRow2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkorderno;
        private System.Windows.Forms.ComboBox cmbcustomername;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkcustname;
        private System.Windows.Forms.CheckBox chkcompletedc;
        private System.Windows.Forms.DataGridView GvorderInfo;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.CheckBox chkDeliverydate;
        private System.Windows.Forms.DateTimePicker dtpdeliveryfromdate;
        private System.Windows.Forms.DateTimePicker dtpdeliverytodate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tblltRow2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTotalBill;
        private System.Windows.Forms.Button btndelivery;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnGetAll;
        private System.Windows.Forms.Button btnTodaysList;
        private System.Windows.Forms.Button rbtnDeleteMultipleBill;
        private RachitControls.NumericTextBox txtorderno;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selectg;
        private System.Windows.Forms.DataGridViewImageColumn DeleteBookingNo;
        private System.Windows.Forms.DataGridViewImageColumn CustomerBill;
    }
}