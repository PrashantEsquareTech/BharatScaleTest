namespace AIOInventorySystem.Desk.Forms
{
    partial class NovatProductsaleinfo
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
            this.lbltotalqty = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbcompany = new System.Windows.Forms.ComboBox();
            this.chkcompanyname = new System.Windows.Forms.CheckBox();
            this.chkproductname = new System.Windows.Forms.CheckBox();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProductname = new System.Windows.Forms.TextBox();
            this.GvproductInfo = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dtptodate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpfromdate = new System.Windows.Forms.DateTimePicker();
            this.chkdate = new System.Windows.Forms.CheckBox();
            this.txtcode = new System.Windows.Forms.TextBox();
            this.chkcode = new System.Windows.Forms.CheckBox();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblltRow2 = new System.Windows.Forms.TableLayoutPanel();
            this.BtnPrint = new System.Windows.Forms.Button();
            this.btnew = new System.Windows.Forms.Button();
            this.btncheck = new System.Windows.Forms.Button();
            this.cmbcustname = new System.Windows.Forms.ComboBox();
            this.chkCustname = new System.Windows.Forms.CheckBox();
            this.cmbgroupMaster = new System.Windows.Forms.ComboBox();
            this.chkgroupname = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTotSubQty = new System.Windows.Forms.Label();
            this.tblltRow3 = new System.Windows.Forms.TableLayoutPanel();
            this.chkShipPartyName = new System.Windows.Forms.CheckBox();
            this.cmbShipPartyName = new System.Windows.Forms.ComboBox();
            this.btnclose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GvproductInfo)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.tblltRow2.SuspendLayout();
            this.tblltRow3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbltotalqty
            // 
            this.lbltotalqty.AutoSize = true;
            this.lbltotalqty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltotalqty.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalqty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbltotalqty.Location = new System.Drawing.Point(330, 481);
            this.lbltotalqty.Margin = new System.Windows.Forms.Padding(2);
            this.lbltotalqty.Name = "lbltotalqty";
            this.lbltotalqty.Size = new System.Drawing.Size(160, 29);
            this.lbltotalqty.TabIndex = 59;
            this.lbltotalqty.Text = "0.00";
            this.lbltotalqty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(2, 481);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 29);
            this.label3.TabIndex = 58;
            this.label3.Text = "Total Qty:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbcompany
            // 
            this.cmbcompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcompany.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcompany.FormattingEnabled = true;
            this.cmbcompany.Location = new System.Drawing.Point(486, 2);
            this.cmbcompany.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcompany.Name = "cmbcompany";
            this.cmbcompany.Size = new System.Drawing.Size(205, 25);
            this.cmbcompany.TabIndex = 4;
            this.cmbcompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcompany_KeyDown);
            // 
            // chkcompanyname
            // 
            this.chkcompanyname.AutoSize = true;
            this.chkcompanyname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkcompanyname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcompanyname.Location = new System.Drawing.Point(331, 2);
            this.chkcompanyname.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkcompanyname.Name = "chkcompanyname";
            this.chkcompanyname.Size = new System.Drawing.Size(151, 25);
            this.chkcompanyname.TabIndex = 3;
            this.chkcompanyname.Text = "Mgf.Company Name";
            this.chkcompanyname.UseVisualStyleBackColor = true;
            this.chkcompanyname.CheckedChanged += new System.EventHandler(this.chkcompanyname_CheckedChanged);
            // 
            // chkproductname
            // 
            this.chkproductname.AutoSize = true;
            this.chkproductname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkproductname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkproductname.Location = new System.Drawing.Point(4, 2);
            this.chkproductname.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkproductname.Name = "chkproductname";
            this.chkproductname.Size = new System.Drawing.Size(112, 25);
            this.chkproductname.TabIndex = 1;
            this.chkproductname.Text = "Product Name";
            this.chkproductname.UseVisualStyleBackColor = true;
            this.chkproductname.CheckedChanged += new System.EventHandler(this.chkproductname_CheckedChanged);
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(822, 481);
            this.lblTotalAmount.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(160, 29);
            this.lblTotalAmount.TabIndex = 55;
            this.lblTotalAmount.Text = "0.00";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(658, 481);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 29);
            this.label1.TabIndex = 54;
            this.label1.Text = "Total Amount:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProductname
            // 
            this.txtProductname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtProductname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtProductname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductname.Location = new System.Drawing.Point(120, 2);
            this.txtProductname.Margin = new System.Windows.Forms.Padding(2);
            this.txtProductname.Name = "txtProductname";
            this.txtProductname.Size = new System.Drawing.Size(205, 25);
            this.txtProductname.TabIndex = 2;
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
            this.tblltMain.SetColumnSpan(this.GvproductInfo, 6);
            this.GvproductInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvproductInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvproductInfo.Location = new System.Drawing.Point(3, 126);
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
            this.GvproductInfo.Size = new System.Drawing.Size(978, 349);
            this.GvproductInfo.TabIndex = 20;
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
            this.label5.Size = new System.Drawing.Size(984, 35);
            this.label5.TabIndex = 36;
            this.label5.Text = "No GST Productwise Sale";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 8;
            this.tblltMain.SetColumnSpan(this.tableLayoutPanel1, 6);
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.5F));
            this.tableLayoutPanel1.Controls.Add(this.dtptodate, 7, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpfromdate, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkdate, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkproductname, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtProductname, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.chkcompanyname, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbcompany, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 35);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 29);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dtptodate
            // 
            this.dtptodate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtptodate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtptodate.Location = new System.Drawing.Point(890, 2);
            this.dtptodate.Margin = new System.Windows.Forms.Padding(2);
            this.dtptodate.Name = "dtptodate";
            this.dtptodate.Size = new System.Drawing.Size(92, 25);
            this.dtptodate.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(851, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 25);
            this.label2.TabIndex = 63;
            this.label2.Text = "To";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpfromdate
            // 
            this.dtpfromdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromdate.Location = new System.Drawing.Point(758, 2);
            this.dtpfromdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpfromdate.Name = "dtpfromdate";
            this.dtpfromdate.Size = new System.Drawing.Size(89, 25);
            this.dtpfromdate.TabIndex = 6;
            // 
            // chkdate
            // 
            this.chkdate.AutoSize = true;
            this.chkdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkdate.Location = new System.Drawing.Point(697, 2);
            this.chkdate.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkdate.Name = "chkdate";
            this.chkdate.Size = new System.Drawing.Size(57, 25);
            this.chkdate.TabIndex = 5;
            this.chkdate.Text = "Date";
            this.chkdate.UseVisualStyleBackColor = true;
            // 
            // txtcode
            // 
            this.txtcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtcode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcode.Location = new System.Drawing.Point(881, 2);
            this.txtcode.Margin = new System.Windows.Forms.Padding(2);
            this.txtcode.Name = "txtcode";
            this.txtcode.Size = new System.Drawing.Size(101, 25);
            this.txtcode.TabIndex = 13;
            this.txtcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcode_KeyDown);
            this.txtcode.Leave += new System.EventHandler(this.txtcode_Leave);
            // 
            // chkcode
            // 
            this.chkcode.AutoSize = true;
            this.chkcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkcode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcode.Location = new System.Drawing.Point(768, 2);
            this.chkcode.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkcode.Name = "chkcode";
            this.chkcode.Size = new System.Drawing.Size(109, 25);
            this.chkcode.TabIndex = 12;
            this.chkcode.Text = "Serial No";
            this.chkcode.UseVisualStyleBackColor = true;
            this.chkcode.CheckedChanged += new System.EventHandler(this.chkcode_CheckedChanged);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 6;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltMain.Controls.Add(this.tblltRow2, 0, 2);
            this.tblltMain.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.lblTotalAmount, 3, 5);
            this.tblltMain.Controls.Add(this.label1, 2, 5);
            this.tblltMain.Controls.Add(this.label3, 0, 5);
            this.tblltMain.Controls.Add(this.lbltotalqty, 1, 5);
            this.tblltMain.Controls.Add(this.GvproductInfo, 0, 4);
            this.tblltMain.Controls.Add(this.label6, 1, 5);
            this.tblltMain.Controls.Add(this.lblTotSubQty, 2, 5);
            this.tblltMain.Controls.Add(this.tblltRow3, 0, 3);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 6;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.75F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.75F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.75F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 69.75F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.Size = new System.Drawing.Size(984, 512);
            this.tblltMain.TabIndex = 0;
            // 
            // tblltRow2
            // 
            this.tblltRow2.ColumnCount = 6;
            this.tblltMain.SetColumnSpan(this.tblltRow2, 6);
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltRow2.Controls.Add(this.txtcode, 5, 0);
            this.tblltRow2.Controls.Add(this.cmbcustname, 3, 0);
            this.tblltRow2.Controls.Add(this.chkcode, 4, 0);
            this.tblltRow2.Controls.Add(this.chkCustname, 2, 0);
            this.tblltRow2.Controls.Add(this.cmbgroupMaster, 1, 0);
            this.tblltRow2.Controls.Add(this.chkgroupname, 0, 0);
            this.tblltRow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow2.Location = new System.Drawing.Point(0, 64);
            this.tblltRow2.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow2.Name = "tblltRow2";
            this.tblltRow2.RowCount = 1;
            this.tblltRow2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow2.Size = new System.Drawing.Size(984, 29);
            this.tblltRow2.TabIndex = 8;
            // 
            // BtnPrint
            // 
            this.BtnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.BtnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnPrint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPrint.ForeColor = System.Drawing.Color.White;
            this.BtnPrint.Location = new System.Drawing.Point(806, 1);
            this.BtnPrint.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.Size = new System.Drawing.Size(84, 27);
            this.BtnPrint.TabIndex = 18;
            this.BtnPrint.Text = "Print";
            this.BtnPrint.UseVisualStyleBackColor = false;
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // btnew
            // 
            this.btnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnew.ForeColor = System.Drawing.Color.White;
            this.btnew.Location = new System.Drawing.Point(718, 1);
            this.btnew.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnew.Name = "btnew";
            this.btnew.Size = new System.Drawing.Size(84, 27);
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
            this.btncheck.Location = new System.Drawing.Point(630, 1);
            this.btncheck.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btncheck.Name = "btncheck";
            this.btncheck.Size = new System.Drawing.Size(84, 27);
            this.btncheck.TabIndex = 16;
            this.btncheck.Text = "Check";
            this.btncheck.UseVisualStyleBackColor = false;
            this.btncheck.Click += new System.EventHandler(this.btncheck_Click);
            // 
            // cmbcustname
            // 
            this.cmbcustname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcustname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcustname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcustname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustname.FormattingEnabled = true;
            this.cmbcustname.Location = new System.Drawing.Point(511, 2);
            this.cmbcustname.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustname.Name = "cmbcustname";
            this.cmbcustname.Size = new System.Drawing.Size(251, 25);
            this.cmbcustname.TabIndex = 11;
            this.cmbcustname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcustname_KeyDown);
            // 
            // chkCustname
            // 
            this.chkCustname.AutoSize = true;
            this.chkCustname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkCustname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCustname.Location = new System.Drawing.Point(335, 2);
            this.chkCustname.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkCustname.Name = "chkCustname";
            this.chkCustname.Size = new System.Drawing.Size(172, 25);
            this.chkCustname.TabIndex = 10;
            this.chkCustname.Text = "Customer Name";
            this.chkCustname.UseVisualStyleBackColor = true;
            this.chkCustname.CheckedChanged += new System.EventHandler(this.chkCustname_CheckedChanged);
            // 
            // cmbgroupMaster
            // 
            this.cmbgroupMaster.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbgroupMaster.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbgroupMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbgroupMaster.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbgroupMaster.ForeColor = System.Drawing.Color.Black;
            this.cmbgroupMaster.FormattingEnabled = true;
            this.cmbgroupMaster.Location = new System.Drawing.Point(155, 2);
            this.cmbgroupMaster.Margin = new System.Windows.Forms.Padding(2);
            this.cmbgroupMaster.Name = "cmbgroupMaster";
            this.cmbgroupMaster.Size = new System.Drawing.Size(174, 25);
            this.cmbgroupMaster.TabIndex = 9;
            this.cmbgroupMaster.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbgroupMaster_KeyDown);
            // 
            // chkgroupname
            // 
            this.chkgroupname.AutoSize = true;
            this.chkgroupname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkgroupname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkgroupname.Location = new System.Drawing.Point(4, 2);
            this.chkgroupname.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkgroupname.Name = "chkgroupname";
            this.chkgroupname.Size = new System.Drawing.Size(147, 25);
            this.chkgroupname.TabIndex = 8;
            this.chkgroupname.Text = "Group Name";
            this.chkgroupname.UseVisualStyleBackColor = true;
            this.chkgroupname.CheckedChanged += new System.EventHandler(this.chkgroupname_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(166, 481);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 29);
            this.label6.TabIndex = 67;
            this.label6.Text = "Total Sub Qty:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotSubQty
            // 
            this.lblTotSubQty.AutoSize = true;
            this.lblTotSubQty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotSubQty.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotSubQty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotSubQty.Location = new System.Drawing.Point(494, 481);
            this.lblTotSubQty.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotSubQty.Name = "lblTotSubQty";
            this.lblTotSubQty.Size = new System.Drawing.Size(160, 29);
            this.lblTotSubQty.TabIndex = 68;
            this.lblTotSubQty.Text = "0";
            this.lblTotSubQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tblltRow3
            // 
            this.tblltRow3.ColumnCount = 7;
            this.tblltMain.SetColumnSpan(this.tblltRow3, 6);
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltRow3.Controls.Add(this.cmbShipPartyName, 1, 0);
            this.tblltRow3.Controls.Add(this.chkShipPartyName, 0, 0);
            this.tblltRow3.Controls.Add(this.btncheck, 3, 0);
            this.tblltRow3.Controls.Add(this.btnew, 4, 0);
            this.tblltRow3.Controls.Add(this.BtnPrint, 5, 0);
            this.tblltRow3.Controls.Add(this.btnclose, 6, 0);
            this.tblltRow3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow3.Location = new System.Drawing.Point(0, 93);
            this.tblltRow3.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow3.Name = "tblltRow3";
            this.tblltRow3.RowCount = 1;
            this.tblltRow3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow3.Size = new System.Drawing.Size(984, 29);
            this.tblltRow3.TabIndex = 14;
            // 
            // chkShipPartyName
            // 
            this.chkShipPartyName.AutoSize = true;
            this.chkShipPartyName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkShipPartyName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShipPartyName.Location = new System.Drawing.Point(4, 2);
            this.chkShipPartyName.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkShipPartyName.Name = "chkShipPartyName";
            this.chkShipPartyName.Size = new System.Drawing.Size(151, 25);
            this.chkShipPartyName.TabIndex = 14;
            this.chkShipPartyName.Text = "Shipping Party Name";
            this.chkShipPartyName.UseVisualStyleBackColor = true;
            // 
            // cmbShipPartyName
            // 
            this.cmbShipPartyName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbShipPartyName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbShipPartyName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbShipPartyName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbShipPartyName.FormattingEnabled = true;
            this.cmbShipPartyName.Location = new System.Drawing.Point(159, 2);
            this.cmbShipPartyName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbShipPartyName.Name = "cmbShipPartyName";
            this.cmbShipPartyName.Size = new System.Drawing.Size(192, 25);
            this.cmbShipPartyName.TabIndex = 15;
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnclose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnclose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(894, 1);
            this.btnclose.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(88, 27);
            this.btnclose.TabIndex = 19;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // NovatProductsaleinfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(984, 512);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1164, 773);
            this.Name = "NovatProductsaleinfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "No GST Productsaleinfo";
            this.Load += new System.EventHandler(this.Stock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvproductInfo)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.tblltRow2.ResumeLayout(false);
            this.tblltRow2.PerformLayout();
            this.tblltRow3.ResumeLayout(false);
            this.tblltRow3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbcompany;
        private System.Windows.Forms.CheckBox chkcompanyname;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkproductname;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProductname;
        private System.Windows.Forms.DataGridView GvproductInfo;
        private System.Windows.Forms.Label lbltotalqty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.CheckBox chkcode;
        private System.Windows.Forms.TextBox txtcode;
        private System.Windows.Forms.TableLayoutPanel tblltRow2;
        private System.Windows.Forms.ComboBox cmbgroupMaster;
        private System.Windows.Forms.CheckBox chkgroupname;
        private System.Windows.Forms.ComboBox cmbcustname;
        private System.Windows.Forms.CheckBox chkCustname;
        private System.Windows.Forms.DateTimePicker dtptodate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpfromdate;
        private System.Windows.Forms.CheckBox chkdate;
        private System.Windows.Forms.Button BtnPrint;
        private System.Windows.Forms.Button btnew;
        private System.Windows.Forms.Button btncheck;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTotSubQty;
        private System.Windows.Forms.TableLayoutPanel tblltRow3;
        private System.Windows.Forms.ComboBox cmbShipPartyName;
        private System.Windows.Forms.CheckBox chkShipPartyName;
        private System.Windows.Forms.Button btnclose;

    }
}