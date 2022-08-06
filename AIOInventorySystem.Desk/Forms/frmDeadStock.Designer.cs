namespace AIOInventorySystem.Desk.Forms
{
    partial class frmDeadStock
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbBatchNo = new System.Windows.Forms.ComboBox();
            this.tblltInputDetails = new System.Windows.Forms.TableLayoutPanel();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtpname = new System.Windows.Forms.TextBox();
            this.cmbcomanyname = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtQuantity = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.GvProductInfo = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductNameg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Company = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApplyUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Barcodeg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sizeg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColorNameg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Categoryg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeptNameg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remove = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtdeadno = new System.Windows.Forms.TextBox();
            this.dtpdeaddate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.lblvat = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btndeadstocklist = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnupdate = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.cmbgst = new System.Windows.Forms.ComboBox();
            this.cmbGodownName = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtgvcode = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblltInputDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvProductInfo)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvcode)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(174, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 24);
            this.label4.TabIndex = 60;
            this.label4.Text = "Dead Date:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(240, 30);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 24);
            this.label6.TabIndex = 30;
            this.label6.Text = "Quantity:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbBatchNo
            // 
            this.cmbBatchNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbBatchNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBatchNo.FormattingEnabled = true;
            this.cmbBatchNo.Location = new System.Drawing.Point(87, 30);
            this.cmbBatchNo.Margin = new System.Windows.Forms.Padding(2);
            this.cmbBatchNo.Name = "cmbBatchNo";
            this.cmbBatchNo.Size = new System.Drawing.Size(149, 25);
            this.cmbBatchNo.TabIndex = 8;
            this.cmbBatchNo.Visible = false;
            // 
            // tblltInputDetails
            // 
            this.tblltInputDetails.ColumnCount = 8;
            this.tblltInputDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltInputDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltInputDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltInputDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.tblltInputDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tblltInputDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltInputDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltInputDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltInputDetails.Controls.Add(this.cmbUnit, 6, 0);
            this.tblltInputDetails.Controls.Add(this.label6, 2, 1);
            this.tblltInputDetails.Controls.Add(this.label23, 5, 0);
            this.tblltInputDetails.Controls.Add(this.cmbBatchNo, 1, 1);
            this.tblltInputDetails.Controls.Add(this.label8, 0, 1);
            this.tblltInputDetails.Controls.Add(this.txtpname, 3, 0);
            this.tblltInputDetails.Controls.Add(this.cmbcomanyname, 1, 0);
            this.tblltInputDetails.Controls.Add(this.label3, 2, 0);
            this.tblltInputDetails.Controls.Add(this.label12, 4, 0);
            this.tblltInputDetails.Controls.Add(this.label13, 7, 0);
            this.tblltInputDetails.Controls.Add(this.txtQuantity, 3, 1);
            this.tblltInputDetails.Controls.Add(this.GvProductInfo, 0, 2);
            this.tblltInputDetails.Controls.Add(this.btnAdd, 7, 1);
            this.tblltInputDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltInputDetails.Location = new System.Drawing.Point(3, 22);
            this.tblltInputDetails.Name = "tblltInputDetails";
            this.tblltInputDetails.RowCount = 3;
            this.tblltInputDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.5F));
            this.tblltInputDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.5F));
            this.tblltInputDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87F));
            this.tblltInputDetails.Size = new System.Drawing.Size(854, 431);
            this.tblltInputDetails.TabIndex = 4;
            this.tblltInputDetails.TabStop = true;
            // 
            // cmbUnit
            // 
            this.cmbUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUnit.ForeColor = System.Drawing.Color.Black;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(683, 2);
            this.cmbUnit.Margin = new System.Windows.Forms.Padding(2);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(107, 25);
            this.cmbUnit.TabIndex = 7;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label23.Location = new System.Drawing.Point(598, 2);
            this.label23.Margin = new System.Windows.Forms.Padding(2);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(81, 24);
            this.label23.TabIndex = 64;
            this.label23.Text = "Apply Unit:";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(2, 30);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(81, 24);
            this.label8.TabIndex = 11;
            this.label8.Text = "Batch No:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Visible = false;
            // 
            // txtpname
            // 
            this.txtpname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtpname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtpname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtpname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpname.Location = new System.Drawing.Point(351, 2);
            this.txtpname.Margin = new System.Windows.Forms.Padding(2);
            this.txtpname.Name = "txtpname";
            this.txtpname.Size = new System.Drawing.Size(218, 25);
            this.txtpname.TabIndex = 6;
            this.txtpname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpname_KeyDown);
            this.txtpname.Leave += new System.EventHandler(this.txtpname_Leave);
            // 
            // cmbcomanyname
            // 
            this.cmbcomanyname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcomanyname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcomanyname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcomanyname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcomanyname.ForeColor = System.Drawing.Color.Black;
            this.cmbcomanyname.FormattingEnabled = true;
            this.cmbcomanyname.Location = new System.Drawing.Point(87, 2);
            this.cmbcomanyname.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcomanyname.Name = "cmbcomanyname";
            this.cmbcomanyname.Size = new System.Drawing.Size(149, 25);
            this.cmbcomanyname.TabIndex = 5;
            this.cmbcomanyname.SelectedIndexChanged += new System.EventHandler(this.cmbcomanyname_SelectedIndexChanged);
            this.cmbcomanyname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcomanyname_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(240, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 24);
            this.label3.TabIndex = 28;
            this.label3.Text = "Product Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label12.Location = new System.Drawing.Point(571, 0);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 28);
            this.label12.TabIndex = 44;
            this.label12.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label13.Location = new System.Drawing.Point(792, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(62, 28);
            this.label13.TabIndex = 45;
            this.label13.Text = "*";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtQuantity.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(351, 30);
            this.txtQuantity.Margin = new System.Windows.Forms.Padding(2);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(100, 25);
            this.txtQuantity.TabIndex = 9;
            this.txtQuantity.TextChanged += new System.EventHandler(this.txtQuantity_TextChanged);
            this.txtQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuantity_KeyDown);
            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantity_KeyPress);
            this.txtQuantity.Leave += new System.EventHandler(this.txtQuantity_Leave);
            // 
            // GvProductInfo
            // 
            this.GvProductInfo.AllowUserToAddRows = false;
            this.GvProductInfo.AllowUserToDeleteRows = false;
            this.GvProductInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvProductInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvProductInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvProductInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.GvProductInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvProductInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.ProductNameg,
            this.Company,
            this.Unit,
            this.ApplyUnit,
            this.Quantity,
            this.Code,
            this.BatchNo,
            this.Barcodeg,
            this.Sizeg,
            this.ColorNameg,
            this.Categoryg,
            this.DeptNameg,
            this.Remove});
            this.tblltInputDetails.SetColumnSpan(this.GvProductInfo, 8);
            this.GvProductInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvProductInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvProductInfo.Location = new System.Drawing.Point(3, 59);
            this.GvProductInfo.Name = "GvProductInfo";
            this.GvProductInfo.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvProductInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.GvProductInfo.RowHeadersWidth = 10;
            this.GvProductInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvProductInfo.Size = new System.Drawing.Size(848, 369);
            this.GvProductInfo.TabIndex = 11;
            this.GvProductInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvProductInfo_CellClick);
            // 
            // Id
            // 
            this.Id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            this.Id.Width = 5;
            // 
            // ProductNameg
            // 
            this.ProductNameg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProductNameg.HeaderText = "Product Name";
            this.ProductNameg.Name = "ProductNameg";
            this.ProductNameg.ReadOnly = true;
            this.ProductNameg.Width = 200;
            // 
            // Company
            // 
            this.Company.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Company.HeaderText = "Company";
            this.Company.Name = "Company";
            this.Company.ReadOnly = true;
            this.Company.Width = 120;
            // 
            // Unit
            // 
            this.Unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            // 
            // ApplyUnit
            // 
            this.ApplyUnit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ApplyUnit.HeaderText = "Apply Unit";
            this.ApplyUnit.Name = "ApplyUnit";
            this.ApplyUnit.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            this.Quantity.Width = 80;
            // 
            // Code
            // 
            this.Code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Width = 80;
            // 
            // BatchNo
            // 
            this.BatchNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BatchNo.HeaderText = "Batch No";
            this.BatchNo.Name = "BatchNo";
            this.BatchNo.ReadOnly = true;
            this.BatchNo.Width = 90;
            // 
            // Barcodeg
            // 
            this.Barcodeg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Barcodeg.HeaderText = "Barcode";
            this.Barcodeg.Name = "Barcodeg";
            this.Barcodeg.ReadOnly = true;
            this.Barcodeg.Width = 70;
            // 
            // Sizeg
            // 
            this.Sizeg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Sizeg.HeaderText = "Size";
            this.Sizeg.Name = "Sizeg";
            this.Sizeg.ReadOnly = true;
            this.Sizeg.Width = 50;
            // 
            // ColorNameg
            // 
            this.ColorNameg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ColorNameg.HeaderText = "ColorName";
            this.ColorNameg.Name = "ColorNameg";
            this.ColorNameg.ReadOnly = true;
            this.ColorNameg.Width = 50;
            // 
            // Categoryg
            // 
            this.Categoryg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Categoryg.HeaderText = "Category";
            this.Categoryg.Name = "Categoryg";
            this.Categoryg.ReadOnly = true;
            this.Categoryg.Width = 50;
            // 
            // DeptNameg
            // 
            this.DeptNameg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DeptNameg.HeaderText = "DeptName";
            this.DeptNameg.Name = "DeptNameg";
            this.DeptNameg.ReadOnly = true;
            this.DeptNameg.Width = 50;
            // 
            // Remove
            // 
            this.Remove.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Remove.HeaderText = "Remove";
            this.Remove.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Remove.Name = "Remove";
            this.Remove.ReadOnly = true;
            this.Remove.Width = 70;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(794, 29);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(58, 26);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnAdd.Leave += new System.EventHandler(this.btnAdd_Leave);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label5, 9);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(864, 36);
            this.label5.TabIndex = 37;
            this.label5.Text = "Dead Stock";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(2, 38);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 59;
            this.label1.Text = "Dead  No:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtdeadno
            // 
            this.txtdeadno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtdeadno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdeadno.ForeColor = System.Drawing.Color.Black;
            this.txtdeadno.Location = new System.Drawing.Point(88, 38);
            this.txtdeadno.Margin = new System.Windows.Forms.Padding(2);
            this.txtdeadno.Name = "txtdeadno";
            this.txtdeadno.ReadOnly = true;
            this.txtdeadno.Size = new System.Drawing.Size(82, 25);
            this.txtdeadno.TabIndex = 0;
            this.txtdeadno.TabStop = false;
            // 
            // dtpdeaddate
            // 
            this.dtpdeaddate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpdeaddate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpdeaddate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpdeaddate.Location = new System.Drawing.Point(260, 38);
            this.dtpdeaddate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpdeaddate.Name = "dtpdeaddate";
            this.dtpdeaddate.Size = new System.Drawing.Size(99, 25);
            this.dtpdeaddate.TabIndex = 1;
            this.dtpdeaddate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpdeaddate_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(536, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 20);
            this.label2.TabIndex = 65;
            this.label2.Text = "%";
            // 
            // lblvat
            // 
            this.lblvat.AutoSize = true;
            this.lblvat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblvat.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblvat.ForeColor = System.Drawing.Color.Black;
            this.lblvat.Location = new System.Drawing.Point(363, 38);
            this.lblvat.Margin = new System.Windows.Forms.Padding(2);
            this.lblvat.Name = "lblvat";
            this.lblvat.Size = new System.Drawing.Size(82, 24);
            this.lblvat.TabIndex = 64;
            this.lblvat.Text = "GST:";
            this.lblvat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 9;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltMain.Controls.Add(this.tblltButtons, 0, 3);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.cmbgst, 5, 1);
            this.tblltMain.Controls.Add(this.cmbGodownName, 8, 1);
            this.tblltMain.Controls.Add(this.label7, 7, 1);
            this.tblltMain.Controls.Add(this.label2, 6, 1);
            this.tblltMain.Controls.Add(this.label1, 0, 1);
            this.tblltMain.Controls.Add(this.txtdeadno, 1, 1);
            this.tblltMain.Controls.Add(this.lblvat, 4, 1);
            this.tblltMain.Controls.Add(this.label4, 2, 1);
            this.tblltMain.Controls.Add(this.dtpdeaddate, 3, 1);
            this.tblltMain.Controls.Add(this.groupBox1, 0, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.Size = new System.Drawing.Size(864, 562);
            this.tblltMain.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 8;
            this.tblltMain.SetColumnSpan(this.tblltButtons, 9);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tblltButtons.Controls.Add(this.btndeadstocklist, 6, 0);
            this.tblltButtons.Controls.Add(this.btnprint, 5, 0);
            this.tblltButtons.Controls.Add(this.btnClose, 4, 0);
            this.tblltButtons.Controls.Add(this.btnupdate, 3, 0);
            this.tblltButtons.Controls.Add(this.btnSave, 2, 0);
            this.tblltButtons.Controls.Add(this.btnNew, 1, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 527);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(864, 35);
            this.tblltButtons.TabIndex = 12;
            // 
            // btndeadstocklist
            // 
            this.btndeadstocklist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btndeadstocklist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btndeadstocklist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btndeadstocklist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndeadstocklist.ForeColor = System.Drawing.Color.White;
            this.btndeadstocklist.Location = new System.Drawing.Point(545, 2);
            this.btndeadstocklist.Margin = new System.Windows.Forms.Padding(2);
            this.btndeadstocklist.Name = "btndeadstocklist";
            this.btndeadstocklist.Size = new System.Drawing.Size(116, 31);
            this.btndeadstocklist.TabIndex = 17;
            this.btndeadstocklist.Text = "Dead Stock List";
            this.btndeadstocklist.UseVisualStyleBackColor = false;
            this.btndeadstocklist.Click += new System.EventHandler(this.btndeadstocklist_Click);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(476, 2);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(65, 31);
            this.btnprint.TabIndex = 16;
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
            this.btnClose.Location = new System.Drawing.Point(407, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(65, 31);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnupdate
            // 
            this.btnupdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnupdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnupdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnupdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnupdate.ForeColor = System.Drawing.Color.White;
            this.btnupdate.Location = new System.Drawing.Point(338, 2);
            this.btnupdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnupdate.Name = "btnupdate";
            this.btnupdate.Size = new System.Drawing.Size(65, 31);
            this.btnupdate.TabIndex = 14;
            this.btnupdate.Text = "Update";
            this.btnupdate.UseVisualStyleBackColor = false;
            this.btnupdate.Click += new System.EventHandler(this.btnupdate_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(269, 2);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(65, 31);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(200, 2);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(65, 31);
            this.btnNew.TabIndex = 13;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // cmbgst
            // 
            this.cmbgst.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbgst.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbgst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbgst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbgst.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbgst.FormattingEnabled = true;
            this.cmbgst.Location = new System.Drawing.Point(449, 38);
            this.cmbgst.Margin = new System.Windows.Forms.Padding(2);
            this.cmbgst.Name = "cmbgst";
            this.cmbgst.Size = new System.Drawing.Size(82, 25);
            this.cmbgst.TabIndex = 2;
            this.cmbgst.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbgst_KeyDown);
            // 
            // cmbGodownName
            // 
            this.cmbGodownName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbGodownName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbGodownName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbGodownName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGodownName.FormattingEnabled = true;
            this.cmbGodownName.Location = new System.Drawing.Point(646, 38);
            this.cmbGodownName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbGodownName.Name = "cmbGodownName";
            this.cmbGodownName.Size = new System.Drawing.Size(216, 25);
            this.cmbGodownName.TabIndex = 3;
            this.cmbGodownName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbGodownName_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(560, 38);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 24);
            this.label7.TabIndex = 165;
            this.label7.Text = "Stock In:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox1
            // 
            this.tblltMain.SetColumnSpan(this.groupBox1, 9);
            this.groupBox1.Controls.Add(this.tblltInputDetails);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(2, 67);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(860, 457);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Product Information";
            // 
            // dtgvcode
            // 
            this.dtgvcode.AllowUserToAddRows = false;
            this.dtgvcode.AllowUserToDeleteRows = false;
            this.dtgvcode.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvcode.BackgroundColor = System.Drawing.Color.White;
            this.dtgvcode.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvcode.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dtgvcode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvcode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvcode.DefaultCellStyle = dataGridViewCellStyle10;
            this.dtgvcode.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgvcode.Location = new System.Drawing.Point(351, 148);
            this.dtgvcode.Name = "dtgvcode";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.NullValue = null;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvcode.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dtgvcode.RowHeadersWidth = 10;
            dataGridViewCellStyle12.NullValue = null;
            this.dtgvcode.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dtgvcode.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgvcode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dtgvcode.Size = new System.Drawing.Size(169, 156);
            this.dtgvcode.TabIndex = 13;
            this.dtgvcode.Visible = false;
            this.dtgvcode.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvcode_CellEndEdit);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Code";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // frmDeadStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(864, 562);
            this.Controls.Add(this.dtgvcode);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmDeadStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dead Stock";
            this.Load += new System.EventHandler(this.frmDeadStock_Load);
            this.tblltInputDetails.ResumeLayout(false);
            this.tblltInputDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvProductInfo)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvcode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbcomanyname;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtpname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtdeadno;
        private System.Windows.Forms.DateTimePicker dtpdeaddate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblvat;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.TableLayoutPanel tblltInputDetails;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbGodownName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbgst;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbBatchNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private RachitControls.NumericTextBox txtQuantity;
        private System.Windows.Forms.DataGridView GvProductInfo;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button btndeadstocklist;
        private System.Windows.Forms.DataGridView dtgvcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        public System.Windows.Forms.Button btnupdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductNameg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Company;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApplyUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Barcodeg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sizeg;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColorNameg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Categoryg;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeptNameg;
        private System.Windows.Forms.DataGridViewImageColumn Remove;

    }
}