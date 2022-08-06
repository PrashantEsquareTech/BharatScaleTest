namespace AIOInventorySystem.Desk.Forms
{
    partial class ProductSaleinfo
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
            this.txtProductname = new System.Windows.Forms.TextBox();
            this.GvproductInfo = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.chkproductname = new System.Windows.Forms.CheckBox();
            this.chkcompanyname = new System.Windows.Forms.CheckBox();
            this.tblltRow1 = new System.Windows.Forms.TableLayoutPanel();
            this.dtptodate = new System.Windows.Forms.DateTimePicker();
            this.dtpfromdate = new System.Windows.Forms.DateTimePicker();
            this.chkbetweendate = new System.Windows.Forms.CheckBox();
            this.cmbcompany = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tblltRow2 = new System.Windows.Forms.TableLayoutPanel();
            this.chkcode = new System.Windows.Forms.CheckBox();
            this.cmbcustname = new System.Windows.Forms.ComboBox();
            this.chkcustName = new System.Windows.Forms.CheckBox();
            this.cmbgroupMaster = new System.Windows.Forms.ComboBox();
            this.chkgroupname = new System.Windows.Forms.CheckBox();
            this.txtcode = new System.Windows.Forms.TextBox();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnew = new System.Windows.Forms.Button();
            this.btncheck = new System.Windows.Forms.Button();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.lbltotalqty = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTotSubQty = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalAvgAmt = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbShiipingPartyName = new System.Windows.Forms.ComboBox();
            this.chkShippingPartyName = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.GvproductInfo)).BeginInit();
            this.tblltRow1.SuspendLayout();
            this.tblltRow2.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtProductname
            // 
            this.txtProductname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtProductname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtProductname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductname.Location = new System.Drawing.Point(445, 2);
            this.txtProductname.Margin = new System.Windows.Forms.Padding(2);
            this.txtProductname.Name = "txtProductname";
            this.txtProductname.Size = new System.Drawing.Size(233, 25);
            this.txtProductname.TabIndex = 5;
            this.txtProductname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductname_KeyDown);
            this.txtProductname.Leave += new System.EventHandler(this.txtProductname_Leave);
            // 
            // GvproductInfo
            // 
            this.GvproductInfo.AllowUserToAddRows = false;
            this.GvproductInfo.AllowUserToDeleteRows = false;
            this.GvproductInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvproductInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvproductInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvproductInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvproductInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblltMain.SetColumnSpan(this.GvproductInfo, 8);
            this.GvproductInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvproductInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvproductInfo.Location = new System.Drawing.Point(3, 133);
            this.GvproductInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GvproductInfo.Name = "GvproductInfo";
            this.GvproductInfo.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvproductInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GvproductInfo.RowHeadersWidth = 15;
            this.GvproductInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvproductInfo.Size = new System.Drawing.Size(1028, 387);
            this.GvproductInfo.TabIndex = 20;
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
            this.label5.Size = new System.Drawing.Size(1034, 39);
            this.label5.TabIndex = 13;
            this.label5.Text = "Product Wise Sale";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkproductname
            // 
            this.chkproductname.AutoSize = true;
            this.chkproductname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkproductname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkproductname.Location = new System.Drawing.Point(323, 2);
            this.chkproductname.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkproductname.Name = "chkproductname";
            this.chkproductname.Size = new System.Drawing.Size(118, 26);
            this.chkproductname.TabIndex = 4;
            this.chkproductname.Text = "Product Name";
            this.chkproductname.UseVisualStyleBackColor = true;
            this.chkproductname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkproductname_KeyDown);
            // 
            // chkcompanyname
            // 
            this.chkcompanyname.AutoSize = true;
            this.chkcompanyname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkcompanyname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcompanyname.Location = new System.Drawing.Point(684, 2);
            this.chkcompanyname.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkcompanyname.Name = "chkcompanyname";
            this.chkcompanyname.Size = new System.Drawing.Size(118, 26);
            this.chkcompanyname.TabIndex = 6;
            this.chkcompanyname.Text = "Mgf.Company";
            this.chkcompanyname.UseVisualStyleBackColor = true;
            this.chkcompanyname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkcompanyname_KeyDown);
            // 
            // tblltRow1
            // 
            this.tblltRow1.ColumnCount = 8;
            this.tblltMain.SetColumnSpan(this.tblltRow1, 8);
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblltRow1.Controls.Add(this.dtptodate, 3, 0);
            this.tblltRow1.Controls.Add(this.dtpfromdate, 1, 0);
            this.tblltRow1.Controls.Add(this.chkbetweendate, 0, 0);
            this.tblltRow1.Controls.Add(this.chkproductname, 4, 0);
            this.tblltRow1.Controls.Add(this.txtProductname, 5, 0);
            this.tblltRow1.Controls.Add(this.chkcompanyname, 6, 0);
            this.tblltRow1.Controls.Add(this.cmbcompany, 7, 0);
            this.tblltRow1.Controls.Add(this.label2, 2, 0);
            this.tblltRow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow1.Location = new System.Drawing.Point(0, 39);
            this.tblltRow1.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow1.Name = "tblltRow1";
            this.tblltRow1.RowCount = 1;
            this.tblltRow1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow1.Size = new System.Drawing.Size(1034, 30);
            this.tblltRow1.TabIndex = 0;
            this.tblltRow1.TabStop = true;
            // 
            // dtptodate
            // 
            this.dtptodate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtptodate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtptodate.Location = new System.Drawing.Point(218, 2);
            this.dtptodate.Margin = new System.Windows.Forms.Padding(2);
            this.dtptodate.Name = "dtptodate";
            this.dtptodate.Size = new System.Drawing.Size(99, 25);
            this.dtptodate.TabIndex = 3;
            this.dtptodate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtptodate_KeyDown);
            // 
            // dtpfromdate
            // 
            this.dtpfromdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfromdate.Location = new System.Drawing.Point(74, 2);
            this.dtpfromdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpfromdate.Name = "dtpfromdate";
            this.dtpfromdate.Size = new System.Drawing.Size(99, 25);
            this.dtpfromdate.TabIndex = 2;
            this.dtpfromdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpfromdate_KeyDown);
            // 
            // chkbetweendate
            // 
            this.chkbetweendate.AutoSize = true;
            this.chkbetweendate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkbetweendate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbetweendate.Location = new System.Drawing.Point(4, 2);
            this.chkbetweendate.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkbetweendate.Name = "chkbetweendate";
            this.chkbetweendate.Size = new System.Drawing.Size(66, 26);
            this.chkbetweendate.TabIndex = 1;
            this.chkbetweendate.Text = "Date";
            this.chkbetweendate.UseVisualStyleBackColor = true;
            this.chkbetweendate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkbetweendate_KeyDown);
            // 
            // cmbcompany
            // 
            this.cmbcompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcompany.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcompany.FormattingEnabled = true;
            this.cmbcompany.Location = new System.Drawing.Point(806, 2);
            this.cmbcompany.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcompany.Name = "cmbcompany";
            this.cmbcompany.Size = new System.Drawing.Size(226, 25);
            this.cmbcompany.TabIndex = 7;
            this.cmbcompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcompany_KeyDown);
            this.cmbcompany.Leave += new System.EventHandler(this.cmbcompany_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(177, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 26);
            this.label2.TabIndex = 67;
            this.label2.Text = "To";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltRow2
            // 
            this.tblltRow2.ColumnCount = 6;
            this.tblltMain.SetColumnSpan(this.tblltRow2, 8);
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.737864F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.708738F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.737864F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.62136F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.62136F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.27184F));
            this.tblltRow2.Controls.Add(this.chkcode, 0, 0);
            this.tblltRow2.Controls.Add(this.cmbcustname, 5, 0);
            this.tblltRow2.Controls.Add(this.chkcustName, 4, 0);
            this.tblltRow2.Controls.Add(this.cmbgroupMaster, 3, 0);
            this.tblltRow2.Controls.Add(this.chkgroupname, 2, 0);
            this.tblltRow2.Controls.Add(this.txtcode, 1, 0);
            this.tblltRow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow2.Location = new System.Drawing.Point(0, 69);
            this.tblltRow2.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow2.Name = "tblltRow2";
            this.tblltRow2.RowCount = 1;
            this.tblltRow2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow2.Size = new System.Drawing.Size(1034, 30);
            this.tblltRow2.TabIndex = 8;
            // 
            // chkcode
            // 
            this.chkcode.AutoSize = true;
            this.chkcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkcode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcode.Location = new System.Drawing.Point(4, 2);
            this.chkcode.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkcode.Name = "chkcode";
            this.chkcode.Size = new System.Drawing.Size(111, 26);
            this.chkcode.TabIndex = 8;
            this.chkcode.Text = "Serial No";
            this.chkcode.UseVisualStyleBackColor = true;
            this.chkcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkcode_KeyDown);
            // 
            // cmbcustname
            // 
            this.cmbcustname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcustname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcustname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcustname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustname.FormattingEnabled = true;
            this.cmbcustname.Location = new System.Drawing.Point(706, 2);
            this.cmbcustname.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustname.Name = "cmbcustname";
            this.cmbcustname.Size = new System.Drawing.Size(326, 25);
            this.cmbcustname.TabIndex = 13;
            this.cmbcustname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcustname_KeyDown);
            this.cmbcustname.Leave += new System.EventHandler(this.cmbcustname_Leave);
            // 
            // chkcustName
            // 
            this.chkcustName.AutoSize = true;
            this.chkcustName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkcustName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcustName.Location = new System.Drawing.Point(538, 2);
            this.chkcustName.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkcustName.Name = "chkcustName";
            this.chkcustName.Size = new System.Drawing.Size(164, 26);
            this.chkcustName.TabIndex = 12;
            this.chkcustName.Text = "Customer Name";
            this.chkcustName.UseVisualStyleBackColor = true;
            this.chkcustName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkcustName_KeyDown);
            // 
            // cmbgroupMaster
            // 
            this.cmbgroupMaster.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbgroupMaster.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbgroupMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbgroupMaster.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbgroupMaster.ForeColor = System.Drawing.Color.Black;
            this.cmbgroupMaster.FormattingEnabled = true;
            this.cmbgroupMaster.Location = new System.Drawing.Point(366, 2);
            this.cmbgroupMaster.Margin = new System.Windows.Forms.Padding(2);
            this.cmbgroupMaster.Name = "cmbgroupMaster";
            this.cmbgroupMaster.Size = new System.Drawing.Size(166, 25);
            this.cmbgroupMaster.TabIndex = 11;
            this.cmbgroupMaster.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbgroupMaster_KeyDown);
            // 
            // chkgroupname
            // 
            this.chkgroupname.AutoSize = true;
            this.chkgroupname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkgroupname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkgroupname.Location = new System.Drawing.Point(251, 2);
            this.chkgroupname.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkgroupname.Name = "chkgroupname";
            this.chkgroupname.Size = new System.Drawing.Size(111, 26);
            this.chkgroupname.TabIndex = 10;
            this.chkgroupname.Text = "Group Name";
            this.chkgroupname.UseVisualStyleBackColor = true;
            this.chkgroupname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkgroupname_KeyDown);
            // 
            // txtcode
            // 
            this.txtcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtcode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcode.Location = new System.Drawing.Point(119, 2);
            this.txtcode.Margin = new System.Windows.Forms.Padding(2);
            this.txtcode.Name = "txtcode";
            this.txtcode.Size = new System.Drawing.Size(126, 25);
            this.txtcode.TabIndex = 9;
            this.txtcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcode_KeyDown);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnclose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnclose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(941, 2);
            this.btnclose.Margin = new System.Windows.Forms.Padding(2);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(91, 26);
            this.btnclose.TabIndex = 19;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(848, 2);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(89, 26);
            this.btnprint.TabIndex = 18;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // btnew
            // 
            this.btnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnew.ForeColor = System.Drawing.Color.White;
            this.btnew.Location = new System.Drawing.Point(755, 2);
            this.btnew.Margin = new System.Windows.Forms.Padding(2);
            this.btnew.Name = "btnew";
            this.btnew.Size = new System.Drawing.Size(89, 26);
            this.btnew.TabIndex = 17;
            this.btnew.Text = "New";
            this.btnew.UseVisualStyleBackColor = false;
            this.btnew.Click += new System.EventHandler(this.btnew_Click);
            // 
            // btncheck
            // 
            this.btncheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btncheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btncheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btncheck.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncheck.ForeColor = System.Drawing.Color.White;
            this.btncheck.Location = new System.Drawing.Point(662, 2);
            this.btncheck.Margin = new System.Windows.Forms.Padding(2);
            this.btncheck.Name = "btncheck";
            this.btncheck.Size = new System.Drawing.Size(89, 26);
            this.btncheck.TabIndex = 16;
            this.btncheck.Text = "Check";
            this.btncheck.UseVisualStyleBackColor = false;
            this.btncheck.Click += new System.EventHandler(this.btncheck_Click);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 8;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.tblltRow2, 0, 2);
            this.tblltMain.Controls.Add(this.tblltRow1, 0, 1);
            this.tblltMain.Controls.Add(this.label3, 0, 5);
            this.tblltMain.Controls.Add(this.GvproductInfo, 0, 4);
            this.tblltMain.Controls.Add(this.lbltotalqty, 1, 5);
            this.tblltMain.Controls.Add(this.label6, 2, 5);
            this.tblltMain.Controls.Add(this.lblTotSubQty, 3, 5);
            this.tblltMain.Controls.Add(this.label4, 4, 5);
            this.tblltMain.Controls.Add(this.label1, 6, 5);
            this.tblltMain.Controls.Add(this.lblTotalAvgAmt, 5, 5);
            this.tblltMain.Controls.Add(this.lblTotalAmount, 7, 5);
            this.tblltMain.Controls.Add(this.tableLayoutPanel1, 0, 3);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 6;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblltMain.Size = new System.Drawing.Size(1034, 561);
            this.tblltMain.TabIndex = 0;
            this.tblltMain.TabStop = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(2, 526);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 33);
            this.label3.TabIndex = 47;
            this.label3.Text = "Total Qty:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbltotalqty
            // 
            this.lbltotalqty.AutoSize = true;
            this.lbltotalqty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltotalqty.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalqty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbltotalqty.Location = new System.Drawing.Point(115, 526);
            this.lbltotalqty.Margin = new System.Windows.Forms.Padding(2);
            this.lbltotalqty.Name = "lbltotalqty";
            this.lbltotalqty.Size = new System.Drawing.Size(99, 33);
            this.lbltotalqty.TabIndex = 48;
            this.lbltotalqty.Text = "0.00";
            this.lbltotalqty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(218, 526);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 33);
            this.label6.TabIndex = 65;
            this.label6.Text = "Total Sub Qty:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotSubQty
            // 
            this.lblTotSubQty.AutoSize = true;
            this.lblTotSubQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotSubQty.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotSubQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotSubQty.Location = new System.Drawing.Point(352, 526);
            this.lblTotSubQty.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotSubQty.Name = "lblTotSubQty";
            this.lblTotSubQty.Size = new System.Drawing.Size(99, 33);
            this.lblTotSubQty.TabIndex = 66;
            this.lblTotSubQty.Text = "0";
            this.lblTotSubQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(455, 526);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 33);
            this.label4.TabIndex = 64;
            this.label4.Text = "Total Avg Amount:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(764, 526);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 33);
            this.label1.TabIndex = 43;
            this.label1.Text = "Total Amount:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalAvgAmt
            // 
            this.lblTotalAvgAmt.AutoSize = true;
            this.lblTotalAvgAmt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalAvgAmt.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAvgAmt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalAvgAmt.Location = new System.Drawing.Point(630, 526);
            this.lblTotalAvgAmt.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotalAvgAmt.Name = "lblTotalAvgAmt";
            this.lblTotalAvgAmt.Size = new System.Drawing.Size(130, 33);
            this.lblTotalAvgAmt.TabIndex = 64;
            this.lblTotalAvgAmt.Text = "0.00";
            this.lblTotalAvgAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTotalAvgAmt.Visible = false;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(898, 526);
            this.lblTotalAmount.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(134, 33);
            this.lblTotalAmount.TabIndex = 44;
            this.lblTotalAmount.Text = "0.00";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tblltMain.SetColumnSpan(this.tableLayoutPanel1, 8);
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tableLayoutPanel1.Controls.Add(this.btncheck, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnprint, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnew, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbShiipingPartyName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkShippingPartyName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnclose, 6, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 99);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1034, 30);
            this.tableLayoutPanel1.TabIndex = 14;
            // 
            // cmbShiipingPartyName
            // 
            this.cmbShiipingPartyName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbShiipingPartyName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbShiipingPartyName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbShiipingPartyName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbShiipingPartyName.FormattingEnabled = true;
            this.cmbShiipingPartyName.Location = new System.Drawing.Point(167, 2);
            this.cmbShiipingPartyName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbShiipingPartyName.Name = "cmbShiipingPartyName";
            this.cmbShiipingPartyName.Size = new System.Drawing.Size(202, 25);
            this.cmbShiipingPartyName.TabIndex = 15;
            // 
            // chkShippingPartyName
            // 
            this.chkShippingPartyName.AutoSize = true;
            this.chkShippingPartyName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkShippingPartyName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShippingPartyName.Location = new System.Drawing.Point(4, 2);
            this.chkShippingPartyName.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkShippingPartyName.Name = "chkShippingPartyName";
            this.chkShippingPartyName.Size = new System.Drawing.Size(159, 26);
            this.chkShippingPartyName.TabIndex = 14;
            this.chkShippingPartyName.Text = "Shipping Party Name";
            this.chkShippingPartyName.UseVisualStyleBackColor = true;
            // 
            // ProductSaleinfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1034, 561);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "ProductSaleinfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Product Sale";
            this.Load += new System.EventHandler(this.Stock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvproductInfo)).EndInit();
            this.tblltRow1.ResumeLayout(false);
            this.tblltRow1.PerformLayout();
            this.tblltRow2.ResumeLayout(false);
            this.tblltRow2.PerformLayout();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GvproductInfo;
        private System.Windows.Forms.TextBox txtProductname;
        private System.Windows.Forms.CheckBox chkproductname;
        private System.Windows.Forms.CheckBox chkcompanyname;
        private System.Windows.Forms.TableLayoutPanel tblltRow1;
        private System.Windows.Forms.TableLayoutPanel tblltRow2;
        private System.Windows.Forms.ComboBox cmbgroupMaster;
        private System.Windows.Forms.CheckBox chkgroupname;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbcustname;
        private System.Windows.Forms.CheckBox chkcustName;
        private System.Windows.Forms.ComboBox cmbcompany;
        private System.Windows.Forms.TextBox txtcode;
        private System.Windows.Forms.CheckBox chkcode;
        private System.Windows.Forms.DateTimePicker dtptodate;
        private System.Windows.Forms.DateTimePicker dtpfromdate;
        private System.Windows.Forms.CheckBox chkbetweendate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Label lblTotalAvgAmt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbltotalqty;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btncheck;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button btnew;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTotSubQty;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cmbShiipingPartyName;
        private System.Windows.Forms.CheckBox chkShippingPartyName;
    }
}