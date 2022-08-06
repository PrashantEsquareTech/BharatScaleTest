namespace AIOInventorySystem.Desk.Forms
{
    partial class frmPurchaseOrderPlace
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblsupplieropno = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbsuppliername = new System.Windows.Forms.ComboBox();
            this.dtpPorderdate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtporderno = new System.Windows.Forms.TextBox();
            this.tblltDetail1 = new System.Windows.Forms.TableLayoutPanel();
            this.rbtnaddproduct = new System.Windows.Forms.Button();
            this.GvProductInfo = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductNameg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Company = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remove = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtQuantity = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.chkcompanyname = new System.Windows.Forms.CheckBox();
            this.cmbcomanyname = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtpname = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnSuppForm = new System.Windows.Forms.Button();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnSendMail = new System.Windows.Forms.Button();
            this.btnorderlist = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtsupplierpono = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.gvProductRemStock = new System.Windows.Forms.DataGridView();
            this.ProductNameR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblltDetail1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvProductInfo)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.grpInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductRemStock)).BeginInit();
            this.SuspendLayout();
            // 
            // lblsupplieropno
            // 
            this.lblsupplieropno.AutoSize = true;
            this.lblsupplieropno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblsupplieropno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsupplieropno.ForeColor = System.Drawing.Color.Black;
            this.lblsupplieropno.Location = new System.Drawing.Point(158, 37);
            this.lblsupplieropno.Margin = new System.Windows.Forms.Padding(2);
            this.lblsupplieropno.Name = "lblsupplieropno";
            this.lblsupplieropno.Size = new System.Drawing.Size(104, 25);
            this.lblsupplieropno.TabIndex = 68;
            this.lblsupplieropno.Text = "Supp. POP No:";
            this.lblsupplieropno.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label9.Location = new System.Drawing.Point(921, 35);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 29);
            this.label9.TabIndex = 44;
            this.label9.Text = "*";
            // 
            // cmbsuppliername
            // 
            this.cmbsuppliername.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbsuppliername.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbsuppliername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbsuppliername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbsuppliername.FormattingEnabled = true;
            this.cmbsuppliername.Location = new System.Drawing.Point(668, 37);
            this.cmbsuppliername.Margin = new System.Windows.Forms.Padding(2);
            this.cmbsuppliername.Name = "cmbsuppliername";
            this.cmbsuppliername.Size = new System.Drawing.Size(251, 25);
            this.cmbsuppliername.TabIndex = 4;
            this.cmbsuppliername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbsuppliername_KeyDown);
            this.cmbsuppliername.Leave += new System.EventHandler(this.cmbsuppliername_Leave);
            // 
            // dtpPorderdate
            // 
            this.dtpPorderdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpPorderdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPorderdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPorderdate.Location = new System.Drawing.Point(437, 37);
            this.dtpPorderdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpPorderdate.Name = "dtpPorderdate";
            this.dtpPorderdate.Size = new System.Drawing.Size(114, 25);
            this.dtpPorderdate.TabIndex = 3;
            this.dtpPorderdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpPorderdate_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(555, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 25);
            this.label2.TabIndex = 26;
            this.label2.Text = "Supplier Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(354, 37);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 25);
            this.label4.TabIndex = 25;
            this.label4.Text = "POP Date:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(2, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 25);
            this.label1.TabIndex = 24;
            this.label1.Text = "POP No:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtporderno
            // 
            this.txtporderno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtporderno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtporderno.ForeColor = System.Drawing.Color.Black;
            this.txtporderno.Location = new System.Drawing.Point(75, 37);
            this.txtporderno.Margin = new System.Windows.Forms.Padding(2);
            this.txtporderno.Name = "txtporderno";
            this.txtporderno.ReadOnly = true;
            this.txtporderno.Size = new System.Drawing.Size(79, 25);
            this.txtporderno.TabIndex = 1;
            this.txtporderno.TabStop = false;
            // 
            // tblltDetail1
            // 
            this.tblltDetail1.ColumnCount = 12;
            this.tblltDetail1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.5F));
            this.tblltDetail1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.5F));
            this.tblltDetail1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.75F));
            this.tblltDetail1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltDetail1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tblltDetail1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.5F));
            this.tblltDetail1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.75F));
            this.tblltDetail1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltDetail1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltDetail1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltDetail1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tblltDetail1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltDetail1.Controls.Add(this.rbtnaddproduct, 5, 0);
            this.tblltDetail1.Controls.Add(this.GvProductInfo, 0, 1);
            this.tblltDetail1.Controls.Add(this.btnAdd, 11, 0);
            this.tblltDetail1.Controls.Add(this.txtQuantity, 9, 0);
            this.tblltDetail1.Controls.Add(this.cmbUnit, 7, 0);
            this.tblltDetail1.Controls.Add(this.label13, 10, 0);
            this.tblltDetail1.Controls.Add(this.label34, 6, 0);
            this.tblltDetail1.Controls.Add(this.chkcompanyname, 0, 0);
            this.tblltDetail1.Controls.Add(this.cmbcomanyname, 1, 0);
            this.tblltDetail1.Controls.Add(this.label6, 8, 0);
            this.tblltDetail1.Controls.Add(this.label3, 2, 0);
            this.tblltDetail1.Controls.Add(this.txtpname, 3, 0);
            this.tblltDetail1.Controls.Add(this.label12, 4, 0);
            this.tblltDetail1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltDetail1.Location = new System.Drawing.Point(3, 21);
            this.tblltDetail1.Name = "tblltDetail1";
            this.tblltDetail1.RowCount = 2;
            this.tblltDetail1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltDetail1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93F));
            this.tblltDetail1.Size = new System.Drawing.Size(972, 414);
            this.tblltDetail1.TabIndex = 6;
            // 
            // rbtnaddproduct
            // 
            this.rbtnaddproduct.BackgroundImage = global::AIOInventorySystem.Desk.Properties.Resources.newbutton;
            this.rbtnaddproduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.rbtnaddproduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtnaddproduct.FlatAppearance.BorderSize = 0;
            this.rbtnaddproduct.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbtnaddproduct.Location = new System.Drawing.Point(601, 2);
            this.rbtnaddproduct.Margin = new System.Windows.Forms.Padding(2);
            this.rbtnaddproduct.Name = "rbtnaddproduct";
            this.rbtnaddproduct.Size = new System.Drawing.Size(30, 24);
            this.rbtnaddproduct.TabIndex = 10;
            this.rbtnaddproduct.UseVisualStyleBackColor = true;
            this.rbtnaddproduct.Click += new System.EventHandler(this.rbtnaddproduct_Click);
            // 
            // GvProductInfo
            // 
            this.GvProductInfo.AllowUserToAddRows = false;
            this.GvProductInfo.AllowUserToDeleteRows = false;
            this.GvProductInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvProductInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvProductInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GvProductInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvProductInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.ProductNameg,
            this.Company,
            this.Unit,
            this.PurUnit,
            this.Quantity,
            this.Remove});
            this.tblltDetail1.SetColumnSpan(this.GvProductInfo, 12);
            this.GvProductInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvProductInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvProductInfo.Location = new System.Drawing.Point(3, 31);
            this.GvProductInfo.Name = "GvProductInfo";
            this.GvProductInfo.ReadOnly = true;
            this.GvProductInfo.RowHeadersWidth = 20;
            this.GvProductInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvProductInfo.Size = new System.Drawing.Size(966, 380);
            this.GvProductInfo.TabIndex = 14;
            this.GvProductInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvProductInfo_CellClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // ProductNameg
            // 
            this.ProductNameg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProductNameg.FillWeight = 8.849561F;
            this.ProductNameg.HeaderText = "Product Name";
            this.ProductNameg.Name = "ProductNameg";
            this.ProductNameg.ReadOnly = true;
            this.ProductNameg.Width = 300;
            // 
            // Company
            // 
            this.Company.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Company.FillWeight = 243.6548F;
            this.Company.HeaderText = "Company";
            this.Company.Name = "Company";
            this.Company.ReadOnly = true;
            this.Company.Width = 200;
            // 
            // Unit
            // 
            this.Unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Unit.FillWeight = 16.85363F;
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            this.Unit.Width = 120;
            // 
            // PurUnit
            // 
            this.PurUnit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PurUnit.FillWeight = 38.05496F;
            this.PurUnit.HeaderText = "Purchase Unit";
            this.PurUnit.Name = "PurUnit";
            this.PurUnit.ReadOnly = true;
            this.PurUnit.Width = 120;
            // 
            // Quantity
            // 
            this.Quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Quantity.FillWeight = 87.87808F;
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // Remove
            // 
            this.Remove.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Remove.FillWeight = 204.7089F;
            this.Remove.HeaderText = "Remove";
            this.Remove.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Remove.Name = "Remove";
            this.Remove.ReadOnly = true;
            this.Remove.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Remove.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(922, 1);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(48, 26);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuantity.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(835, 2);
            this.txtQuantity.Margin = new System.Windows.Forms.Padding(2);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(64, 25);
            this.txtQuantity.TabIndex = 12;
            this.txtQuantity.Text = "0";
            this.txtQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuantity_KeyDown);
            // 
            // cmbUnit
            // 
            this.cmbUnit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbUnit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUnit.ForeColor = System.Drawing.Color.Black;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(690, 2);
            this.cmbUnit.Margin = new System.Windows.Forms.Padding(2);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(73, 25);
            this.cmbUnit.TabIndex = 11;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label13.Location = new System.Drawing.Point(901, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 28);
            this.label13.TabIndex = 45;
            this.label13.Text = "*";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label34.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.Color.Black;
            this.label34.Location = new System.Drawing.Point(635, 2);
            this.label34.Margin = new System.Windows.Forms.Padding(2);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(51, 24);
            this.label34.TabIndex = 68;
            this.label34.Text = "P Unit:";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkcompanyname
            // 
            this.chkcompanyname.AutoSize = true;
            this.chkcompanyname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkcompanyname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcompanyname.ForeColor = System.Drawing.Color.Black;
            this.chkcompanyname.Location = new System.Drawing.Point(2, 2);
            this.chkcompanyname.Margin = new System.Windows.Forms.Padding(2);
            this.chkcompanyname.Name = "chkcompanyname";
            this.chkcompanyname.Size = new System.Drawing.Size(98, 24);
            this.chkcompanyname.TabIndex = 7;
            this.chkcompanyname.Text = "Mfg. Comp.";
            this.chkcompanyname.UseVisualStyleBackColor = true;
            this.chkcompanyname.CheckedChanged += new System.EventHandler(this.chkcompanyname_CheckedChanged);
            // 
            // cmbcomanyname
            // 
            this.cmbcomanyname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcomanyname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcomanyname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcomanyname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcomanyname.ForeColor = System.Drawing.Color.Black;
            this.cmbcomanyname.FormattingEnabled = true;
            this.cmbcomanyname.Location = new System.Drawing.Point(104, 2);
            this.cmbcomanyname.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcomanyname.Name = "cmbcomanyname";
            this.cmbcomanyname.Size = new System.Drawing.Size(127, 25);
            this.cmbcomanyname.TabIndex = 8;
            this.cmbcomanyname.SelectedIndexChanged += new System.EventHandler(this.cmbcomanyname_SelectedIndexChanged);
            this.cmbcomanyname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcomanyname_KeyDown);
            this.cmbcomanyname.Leave += new System.EventHandler(this.cmbcomanyname_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(767, 2);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 24);
            this.label6.TabIndex = 29;
            this.label6.Text = "Quantity:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(235, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 24);
            this.label3.TabIndex = 28;
            this.label3.Text = "Product Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtpname
            // 
            this.txtpname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtpname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtpname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtpname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpname.Location = new System.Drawing.Point(339, 2);
            this.txtpname.Margin = new System.Windows.Forms.Padding(2);
            this.txtpname.Name = "txtpname";
            this.txtpname.Size = new System.Drawing.Size(239, 25);
            this.txtpname.TabIndex = 9;
            this.txtpname.Text = " ";
            this.txtpname.TextChanged += new System.EventHandler(this.txtpname_TextChanged);
            this.txtpname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpname_KeyDown);
            this.txtpname.Leave += new System.EventHandler(this.txtpname_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label12.Location = new System.Drawing.Point(580, 0);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 28);
            this.label12.TabIndex = 44;
            this.label12.Text = "*";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label5, 10);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(984, 35);
            this.label5.TabIndex = 26;
            this.label5.Text = "Purchase Order Place";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 10;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.5F));
            this.tblltMain.Controls.Add(this.btnSuppForm, 9, 1);
            this.tblltMain.Controls.Add(this.tblltButtons, 0, 3);
            this.tblltMain.Controls.Add(this.txtsupplierpono, 3, 1);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.label1, 0, 1);
            this.tblltMain.Controls.Add(this.label9, 8, 1);
            this.tblltMain.Controls.Add(this.txtporderno, 1, 1);
            this.tblltMain.Controls.Add(this.cmbsuppliername, 7, 1);
            this.tblltMain.Controls.Add(this.lblsupplieropno, 2, 1);
            this.tblltMain.Controls.Add(this.label2, 6, 1);
            this.tblltMain.Controls.Add(this.dtpPorderdate, 5, 1);
            this.tblltMain.Controls.Add(this.label4, 4, 1);
            this.tblltMain.Controls.Add(this.grpInfo, 0, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.Size = new System.Drawing.Size(984, 542);
            this.tblltMain.TabIndex = 0;
            // 
            // btnSuppForm
            // 
            this.btnSuppForm.BackgroundImage = global::AIOInventorySystem.Desk.Properties.Resources.newbutton;
            this.btnSuppForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSuppForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSuppForm.FlatAppearance.BorderSize = 0;
            this.btnSuppForm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSuppForm.Location = new System.Drawing.Point(947, 37);
            this.btnSuppForm.Margin = new System.Windows.Forms.Padding(2);
            this.btnSuppForm.Name = "btnSuppForm";
            this.btnSuppForm.Size = new System.Drawing.Size(35, 25);
            this.btnSuppForm.TabIndex = 5;
            this.btnSuppForm.UseVisualStyleBackColor = true;
            this.btnSuppForm.Click += new System.EventHandler(this.btnSuppForm_Click);
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 10;
            this.tblltMain.SetColumnSpan(this.tblltButtons, 10);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.5F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.5F));
            this.tblltButtons.Controls.Add(this.btnSendMail, 8, 0);
            this.tblltButtons.Controls.Add(this.btnorderlist, 7, 0);
            this.tblltButtons.Controls.Add(this.btnNew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnSave, 2, 0);
            this.tblltButtons.Controls.Add(this.btnUpdate, 3, 0);
            this.tblltButtons.Controls.Add(this.btnDelete, 4, 0);
            this.tblltButtons.Controls.Add(this.btnprint, 6, 0);
            this.tblltButtons.Controls.Add(this.btnClose, 5, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 508);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(984, 34);
            this.tblltButtons.TabIndex = 15;
            // 
            // btnSendMail
            // 
            this.btnSendMail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSendMail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSendMail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSendMail.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendMail.ForeColor = System.Drawing.Color.White;
            this.btnSendMail.Location = new System.Drawing.Point(780, 2);
            this.btnSendMail.Margin = new System.Windows.Forms.Padding(2);
            this.btnSendMail.Name = "btnSendMail";
            this.btnSendMail.Size = new System.Drawing.Size(84, 30);
            this.btnSendMail.TabIndex = 22;
            this.btnSendMail.Text = "Send Mail";
            this.btnSendMail.UseVisualStyleBackColor = false;
            this.btnSendMail.Click += new System.EventHandler(this.btnSendMail_Click);
            // 
            // btnorderlist
            // 
            this.btnorderlist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnorderlist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnorderlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnorderlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnorderlist.ForeColor = System.Drawing.Color.White;
            this.btnorderlist.Location = new System.Drawing.Point(643, 2);
            this.btnorderlist.Margin = new System.Windows.Forms.Padding(2);
            this.btnorderlist.Name = "btnorderlist";
            this.btnorderlist.Size = new System.Drawing.Size(133, 30);
            this.btnorderlist.TabIndex = 21;
            this.btnorderlist.Text = "Order Place List";
            this.btnorderlist.UseVisualStyleBackColor = false;
            this.btnorderlist.Click += new System.EventHandler(this.btnorderlist_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(115, 2);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(84, 30);
            this.btnNew.TabIndex = 16;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(203, 2);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(84, 30);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(291, 2);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(84, 30);
            this.btnUpdate.TabIndex = 17;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(379, 2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(84, 30);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(555, 2);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(84, 30);
            this.btnprint.TabIndex = 20;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(467, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 30);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtsupplierpono
            // 
            this.txtsupplierpono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtsupplierpono.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsupplierpono.Location = new System.Drawing.Point(266, 37);
            this.txtsupplierpono.Margin = new System.Windows.Forms.Padding(2);
            this.txtsupplierpono.Name = "txtsupplierpono";
            this.txtsupplierpono.Size = new System.Drawing.Size(84, 25);
            this.txtsupplierpono.TabIndex = 2;
            this.txtsupplierpono.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsupplierpono_KeyDown);
            // 
            // grpInfo
            // 
            this.tblltMain.SetColumnSpan(this.grpInfo, 10);
            this.grpInfo.Controls.Add(this.tblltDetail1);
            this.grpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInfo.Location = new System.Drawing.Point(3, 67);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(978, 438);
            this.grpInfo.TabIndex = 6;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Product Information";
            // 
            // gvProductRemStock
            // 
            this.gvProductRemStock.AllowUserToAddRows = false;
            this.gvProductRemStock.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.gvProductRemStock.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gvProductRemStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvProductRemStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvProductRemStock.BackgroundColor = System.Drawing.Color.White;
            this.gvProductRemStock.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvProductRemStock.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gvProductRemStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProductRemStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductNameR,
            this.RemQty});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvProductRemStock.DefaultCellStyle = dataGridViewCellStyle3;
            this.gvProductRemStock.GridColor = System.Drawing.Color.WhiteSmoke;
            this.gvProductRemStock.Location = new System.Drawing.Point(345, 21);
            this.gvProductRemStock.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gvProductRemStock.Name = "gvProductRemStock";
            this.gvProductRemStock.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvProductRemStock.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.gvProductRemStock.RowHeadersWidth = 10;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.NullValue = null;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.gvProductRemStock.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.gvProductRemStock.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvProductRemStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gvProductRemStock.Size = new System.Drawing.Size(242, 64);
            this.gvProductRemStock.TabIndex = 62;
            this.gvProductRemStock.Visible = false;
            // 
            // ProductNameR
            // 
            this.ProductNameR.FillWeight = 131.9797F;
            this.ProductNameR.HeaderText = "ProductName";
            this.ProductNameR.Name = "ProductNameR";
            this.ProductNameR.ReadOnly = true;
            // 
            // RemQty
            // 
            this.RemQty.FillWeight = 68.02031F;
            this.RemQty.HeaderText = "RemQty";
            this.RemQty.Name = "RemQty";
            this.RemQty.ReadOnly = true;
            // 
            // frmPurchaseOrderPlace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(984, 542);
            this.Controls.Add(this.gvProductRemStock);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1144, 705);
            this.Name = "frmPurchaseOrderPlace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Order Place";
            this.Load += new System.EventHandler(this.fmPurchaseOrder_Load);
            this.tblltDetail1.ResumeLayout(false);
            this.tblltDetail1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvProductInfo)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.grpInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvProductRemStock)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtporderno;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpPorderdate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbsuppliername;
        private System.Windows.Forms.TextBox txtpname;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbcomanyname;
        private System.Windows.Forms.CheckBox chkcompanyname;
        private System.Windows.Forms.Label lblsupplieropno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.TableLayoutPanel tblltDetail1;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.DataGridView gvProductRemStock;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.GroupBox grpInfo;
        private RachitControls.NumericTextBox txtsupplierpono;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnSendMail;
        private System.Windows.Forms.Button btnorderlist;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button btnDelete;
        private RachitControls.NumericTextBox txtQuantity;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView GvProductInfo;
        private System.Windows.Forms.Button btnSuppForm;
        private System.Windows.Forms.Button rbtnaddproduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductNameg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Company;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewImageColumn Remove;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductNameR;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemQty;
    }
}