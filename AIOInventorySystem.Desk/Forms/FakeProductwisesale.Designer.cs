namespace AIOInventorySystem.Desk.Forms
{
    partial class FakeProductwisesale
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbcompany = new System.Windows.Forms.ComboBox();
            this.chkcompanyname = new System.Windows.Forms.CheckBox();
            this.chkproductname = new System.Windows.Forms.CheckBox();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProductname = new System.Windows.Forms.TextBox();
            this.GvproductInfo = new System.Windows.Forms.DataGridView();
            this.lbltotalqty = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tblltInputRow1 = new System.Windows.Forms.TableLayoutPanel();
            this.chkcode = new System.Windows.Forms.CheckBox();
            this.txtcode = new System.Windows.Forms.TextBox();
            this.tblltBottom = new System.Windows.Forms.TableLayoutPanel();
            this.tblltInputRow2 = new System.Windows.Forms.TableLayoutPanel();
            this.chkgroupname = new System.Windows.Forms.CheckBox();
            this.cmbgroupMaster = new System.Windows.Forms.ComboBox();
            this.btncheck = new System.Windows.Forms.Button();
            this.btnew = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.GvproductInfo)).BeginInit();
            this.tblltInputRow1.SuspendLayout();
            this.tblltBottom.SuspendLayout();
            this.tblltInputRow2.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbcompany
            // 
            this.cmbcompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcompany.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcompany.FormattingEnabled = true;
            this.cmbcompany.Location = new System.Drawing.Point(487, 2);
            this.cmbcompany.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcompany.Name = "cmbcompany";
            this.cmbcompany.Size = new System.Drawing.Size(155, 25);
            this.cmbcompany.TabIndex = 4;
            this.cmbcompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcompany_KeyDown);
            // 
            // chkcompanyname
            // 
            this.chkcompanyname.AutoSize = true;
            this.chkcompanyname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkcompanyname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcompanyname.Location = new System.Drawing.Point(331, 2);
            this.chkcompanyname.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkcompanyname.Name = "chkcompanyname";
            this.chkcompanyname.Size = new System.Drawing.Size(152, 26);
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
            this.chkproductname.Location = new System.Drawing.Point(5, 2);
            this.chkproductname.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkproductname.Name = "chkproductname";
            this.chkproductname.Size = new System.Drawing.Size(116, 26);
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
            this.lblTotalAmount.Location = new System.Drawing.Point(664, 2);
            this.lblTotalAmount.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(218, 29);
            this.lblTotalAmount.TabIndex = 66;
            this.lblTotalAmount.Text = "0.00";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(523, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 29);
            this.label1.TabIndex = 65;
            this.label1.Text = "Total Amount:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.label5.Size = new System.Drawing.Size(884, 33);
            this.label5.TabIndex = 36;
            this.label5.Text = "Fake Productwise Sale";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtProductname
            // 
            this.txtProductname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtProductname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtProductname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductname.Location = new System.Drawing.Point(125, 2);
            this.txtProductname.Margin = new System.Windows.Forms.Padding(2);
            this.txtProductname.Name = "txtProductname";
            this.txtProductname.Size = new System.Drawing.Size(199, 25);
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GvproductInfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.GvproductInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvproductInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvproductInfo.Location = new System.Drawing.Point(3, 97);
            this.GvproductInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GvproductInfo.Name = "GvproductInfo";
            this.GvproductInfo.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvproductInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GvproductInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvproductInfo.Size = new System.Drawing.Size(878, 378);
            this.GvproductInfo.TabIndex = 65;
            // 
            // lbltotalqty
            // 
            this.lbltotalqty.AutoSize = true;
            this.lbltotalqty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltotalqty.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalqty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbltotalqty.Location = new System.Drawing.Point(326, 2);
            this.lbltotalqty.Margin = new System.Windows.Forms.Padding(2);
            this.lbltotalqty.Name = "lbltotalqty";
            this.lbltotalqty.Size = new System.Drawing.Size(193, 29);
            this.lbltotalqty.TabIndex = 70;
            this.lbltotalqty.Text = "0.00";
            this.lbltotalqty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(2, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(320, 29);
            this.label3.TabIndex = 69;
            this.label3.Text = "Total Qty:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblltInputRow1
            // 
            this.tblltInputRow1.ColumnCount = 6;
            this.tblltInputRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltInputRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tblltInputRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltInputRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltInputRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltInputRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblltInputRow1.Controls.Add(this.chkcode, 4, 0);
            this.tblltInputRow1.Controls.Add(this.txtcode, 5, 0);
            this.tblltInputRow1.Controls.Add(this.chkproductname, 0, 0);
            this.tblltInputRow1.Controls.Add(this.txtProductname, 1, 0);
            this.tblltInputRow1.Controls.Add(this.chkcompanyname, 2, 0);
            this.tblltInputRow1.Controls.Add(this.cmbcompany, 3, 0);
            this.tblltInputRow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltInputRow1.Location = new System.Drawing.Point(0, 33);
            this.tblltInputRow1.Margin = new System.Windows.Forms.Padding(0);
            this.tblltInputRow1.Name = "tblltInputRow1";
            this.tblltInputRow1.RowCount = 1;
            this.tblltInputRow1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltInputRow1.Size = new System.Drawing.Size(884, 30);
            this.tblltInputRow1.TabIndex = 0;
            // 
            // chkcode
            // 
            this.chkcode.AutoSize = true;
            this.chkcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkcode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcode.Location = new System.Drawing.Point(649, 2);
            this.chkcode.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkcode.Name = "chkcode";
            this.chkcode.Size = new System.Drawing.Size(81, 26);
            this.chkcode.TabIndex = 5;
            this.chkcode.Text = "Serial No";
            this.chkcode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkcode.UseVisualStyleBackColor = true;
            this.chkcode.CheckedChanged += new System.EventHandler(this.chkcode_CheckedChanged);
            // 
            // txtcode
            // 
            this.txtcode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtcode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtcode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcode.Location = new System.Drawing.Point(734, 2);
            this.txtcode.Margin = new System.Windows.Forms.Padding(2);
            this.txtcode.Name = "txtcode";
            this.txtcode.Size = new System.Drawing.Size(148, 25);
            this.txtcode.TabIndex = 6;
            this.txtcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcode_KeyDown);
            this.txtcode.Leave += new System.EventHandler(this.txtcode_Leave);
            // 
            // tblltBottom
            // 
            this.tblltBottom.ColumnCount = 4;
            this.tblltBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.67042F));
            this.tblltBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.27222F));
            this.tblltBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.973F));
            this.tblltBottom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltBottom.Controls.Add(this.label3, 0, 0);
            this.tblltBottom.Controls.Add(this.lbltotalqty, 1, 0);
            this.tblltBottom.Controls.Add(this.lblTotalAmount, 3, 0);
            this.tblltBottom.Controls.Add(this.label1, 2, 0);
            this.tblltBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltBottom.Location = new System.Drawing.Point(0, 479);
            this.tblltBottom.Margin = new System.Windows.Forms.Padding(0);
            this.tblltBottom.Name = "tblltBottom";
            this.tblltBottom.RowCount = 1;
            this.tblltBottom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltBottom.Size = new System.Drawing.Size(884, 33);
            this.tblltBottom.TabIndex = 71;
            // 
            // tblltInputRow2
            // 
            this.tblltInputRow2.ColumnCount = 6;
            this.tblltInputRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltInputRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tblltInputRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tblltInputRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltInputRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltInputRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltInputRow2.Controls.Add(this.chkgroupname, 0, 0);
            this.tblltInputRow2.Controls.Add(this.cmbgroupMaster, 1, 0);
            this.tblltInputRow2.Controls.Add(this.btncheck, 3, 0);
            this.tblltInputRow2.Controls.Add(this.btnew, 4, 0);
            this.tblltInputRow2.Controls.Add(this.btnclose, 5, 0);
            this.tblltInputRow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltInputRow2.Location = new System.Drawing.Point(0, 63);
            this.tblltInputRow2.Margin = new System.Windows.Forms.Padding(0);
            this.tblltInputRow2.Name = "tblltInputRow2";
            this.tblltInputRow2.RowCount = 1;
            this.tblltInputRow2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltInputRow2.Size = new System.Drawing.Size(884, 30);
            this.tblltInputRow2.TabIndex = 7;
            // 
            // chkgroupname
            // 
            this.chkgroupname.AutoSize = true;
            this.chkgroupname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkgroupname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkgroupname.Location = new System.Drawing.Point(5, 2);
            this.chkgroupname.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkgroupname.Name = "chkgroupname";
            this.chkgroupname.Size = new System.Drawing.Size(116, 26);
            this.chkgroupname.TabIndex = 7;
            this.chkgroupname.Text = "Group Name";
            this.chkgroupname.UseVisualStyleBackColor = true;
            this.chkgroupname.CheckedChanged += new System.EventHandler(this.chkgroupname_CheckedChanged);
            // 
            // cmbgroupMaster
            // 
            this.cmbgroupMaster.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbgroupMaster.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbgroupMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbgroupMaster.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbgroupMaster.ForeColor = System.Drawing.Color.Black;
            this.cmbgroupMaster.FormattingEnabled = true;
            this.cmbgroupMaster.Location = new System.Drawing.Point(125, 2);
            this.cmbgroupMaster.Margin = new System.Windows.Forms.Padding(2);
            this.cmbgroupMaster.Name = "cmbgroupMaster";
            this.cmbgroupMaster.Size = new System.Drawing.Size(199, 25);
            this.cmbgroupMaster.TabIndex = 8;
            this.cmbgroupMaster.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbgroupMaster_KeyDown);
            // 
            // btncheck
            // 
            this.btncheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btncheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btncheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btncheck.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncheck.ForeColor = System.Drawing.Color.White;
            this.btncheck.Location = new System.Drawing.Point(619, 2);
            this.btncheck.Margin = new System.Windows.Forms.Padding(2);
            this.btncheck.Name = "btncheck";
            this.btncheck.Size = new System.Drawing.Size(84, 26);
            this.btncheck.TabIndex = 9;
            this.btncheck.Text = "Check";
            this.btncheck.UseVisualStyleBackColor = false;
            this.btncheck.Click += new System.EventHandler(this.btncheck_Click);
            // 
            // btnew
            // 
            this.btnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnew.ForeColor = System.Drawing.Color.White;
            this.btnew.Location = new System.Drawing.Point(707, 2);
            this.btnew.Margin = new System.Windows.Forms.Padding(2);
            this.btnew.Name = "btnew";
            this.btnew.Size = new System.Drawing.Size(84, 26);
            this.btnew.TabIndex = 10;
            this.btnew.Text = "New";
            this.btnew.UseVisualStyleBackColor = false;
            this.btnew.Click += new System.EventHandler(this.btnew_Click);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnclose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnclose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(795, 2);
            this.btnclose.Margin = new System.Windows.Forms.Padding(2);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(87, 26);
            this.btnclose.TabIndex = 11;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.tblltInputRow1, 0, 1);
            this.tblltMain.Controls.Add(this.tblltInputRow2, 0, 2);
            this.tblltMain.Controls.Add(this.GvproductInfo, 0, 3);
            this.tblltMain.Controls.Add(this.tblltBottom, 0, 4);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 5;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.Size = new System.Drawing.Size(884, 512);
            this.tblltMain.TabIndex = 0;
            // 
            // FakeProductwisesale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(884, 512);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "FakeProductwisesale";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fake Productwise Sale";
            this.Load += new System.EventHandler(this.Stock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvproductInfo)).EndInit();
            this.tblltInputRow1.ResumeLayout(false);
            this.tblltInputRow1.PerformLayout();
            this.tblltBottom.ResumeLayout(false);
            this.tblltBottom.PerformLayout();
            this.tblltInputRow2.ResumeLayout(false);
            this.tblltInputRow2.PerformLayout();
            this.tblltMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbcompany;
        private System.Windows.Forms.CheckBox chkcompanyname;
        private System.Windows.Forms.CheckBox chkproductname;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProductname;
        private System.Windows.Forms.DataGridView GvproductInfo;
        private System.Windows.Forms.Label lbltotalqty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tblltInputRow1;
        private System.Windows.Forms.CheckBox chkcode;
        private System.Windows.Forms.TextBox txtcode;
        private System.Windows.Forms.TableLayoutPanel tblltBottom;
        private System.Windows.Forms.TableLayoutPanel tblltInputRow2;
        private System.Windows.Forms.CheckBox chkgroupname;
        private System.Windows.Forms.ComboBox cmbgroupMaster;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Button btncheck;
        private System.Windows.Forms.Button btnew;
        private System.Windows.Forms.Button btnclose;
    }
}