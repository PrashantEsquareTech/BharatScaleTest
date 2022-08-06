namespace AIOInventorySystem.Desk.Forms
{
    partial class frmDelieveryChallan
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtVehicalno = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.lblmechname = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtchallanno = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpchallandate = new System.Windows.Forms.DateTimePicker();
            this.cmbcustomername = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tblltList = new System.Windows.Forms.TableLayoutPanel();
            this.GvProductInfo = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductNameg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Company = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remove = new System.Windows.Forms.DataGridViewImageColumn();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.label23 = new System.Windows.Forms.Label();
            this.cmbcomanyname = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtProductname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.grpProductDetail = new System.Windows.Forms.GroupBox();
            this.btnCustForm = new System.Windows.Forms.Button();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btndeliverychallenlist = new System.Windows.Forms.Button();
            this.txtprefixproduct = new System.Windows.Forms.ListBox();
            this.gvProductRemStock = new System.Windows.Forms.DataGridView();
            this.ProductNameR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemQtyR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgvcode = new System.Windows.Forms.DataGridView();
            this.CodeC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtQuantity = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tblltList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvProductInfo)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.grpProductDetail.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductRemStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvcode)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(352, 194);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(295, 283);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 158;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // txtVehicalno
            // 
            this.txtVehicalno.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtVehicalno.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.tblltMain.SetColumnSpan(this.txtVehicalno, 3);
            this.txtVehicalno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtVehicalno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVehicalno.FormattingEnabled = true;
            this.txtVehicalno.Location = new System.Drawing.Point(889, 68);
            this.txtVehicalno.Margin = new System.Windows.Forms.Padding(2);
            this.txtVehicalno.Name = "txtVehicalno";
            this.txtVehicalno.Size = new System.Drawing.Size(143, 25);
            this.txtVehicalno.TabIndex = 5;
            this.txtVehicalno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVehicalno_KeyDown);
            this.txtVehicalno.Leave += new System.EventHandler(this.txtVehicalno_Leave);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(2, 68);
            this.label20.Margin = new System.Windows.Forms.Padding(2);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(125, 25);
            this.label20.TabIndex = 52;
            this.label20.Text = "Mechanic Name:";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblmechname
            // 
            this.lblmechname.AutoSize = true;
            this.tblltMain.SetColumnSpan(this.lblmechname, 2);
            this.lblmechname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblmechname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmechname.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblmechname.Location = new System.Drawing.Point(131, 68);
            this.lblmechname.Margin = new System.Windows.Forms.Padding(2);
            this.lblmechname.Name = "lblmechname";
            this.lblmechname.Size = new System.Drawing.Size(233, 25);
            this.lblmechname.TabIndex = 49;
            this.lblmechname.Text = "----------------------------------------";
            this.lblmechname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(368, 68);
            this.label19.Margin = new System.Windows.Forms.Padding(2);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(130, 25);
            this.label19.TabIndex = 51;
            this.label19.Text = "Balance Amt:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(502, 39);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 25);
            this.label3.TabIndex = 50;
            this.label3.Text = "Customer Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(2, 39);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 25);
            this.label1.TabIndex = 39;
            this.label1.Text = "D. C.  No:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtchallanno
            // 
            this.txtchallanno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtchallanno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtchallanno.ForeColor = System.Drawing.Color.Black;
            this.txtchallanno.Location = new System.Drawing.Point(131, 39);
            this.txtchallanno.Margin = new System.Windows.Forms.Padding(2);
            this.txtchallanno.Name = "txtchallanno";
            this.txtchallanno.ReadOnly = true;
            this.txtchallanno.Size = new System.Drawing.Size(109, 25);
            this.txtchallanno.TabIndex = 1;
            this.txtchallanno.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(244, 39);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 25);
            this.label4.TabIndex = 40;
            this.label4.Text = "D. C. Date:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpchallandate
            // 
            this.dtpchallandate.CustomFormat = "dd/MM/yyyy";
            this.dtpchallandate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpchallandate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpchallandate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpchallandate.Location = new System.Drawing.Point(368, 39);
            this.dtpchallandate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpchallandate.Name = "dtpchallandate";
            this.dtpchallandate.Size = new System.Drawing.Size(130, 25);
            this.dtpchallandate.TabIndex = 2;
            this.dtpchallandate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpchallandate_KeyDown);
            // 
            // cmbcustomername
            // 
            this.cmbcustomername.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcustomername.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcustomername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcustomername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustomername.FormattingEnabled = true;
            this.cmbcustomername.Location = new System.Drawing.Point(631, 39);
            this.cmbcustomername.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustomername.Name = "cmbcustomername";
            this.cmbcustomername.Size = new System.Drawing.Size(254, 25);
            this.cmbcustomername.TabIndex = 3;
            this.cmbcustomername.TextChanged += new System.EventHandler(this.cmbcustomername_TextChanged);
            this.cmbcustomername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcustomername_KeyDown);
            this.cmbcustomername.Leave += new System.EventHandler(this.cmbcustomername_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(889, 39);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 25);
            this.label7.TabIndex = 53;
            this.label7.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(502, 68);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 25);
            this.label6.TabIndex = 49;
            this.label6.Text = "--------";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(631, 68);
            this.label9.Margin = new System.Windows.Forms.Padding(2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(254, 25);
            this.label9.TabIndex = 50;
            this.label9.Text = "Vehicle No:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblltList
            // 
            this.tblltList.ColumnCount = 11;
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltList.Controls.Add(this.txtQuantity, 8, 0);
            this.tblltList.Controls.Add(this.GvProductInfo, 0, 1);
            this.tblltList.Controls.Add(this.label8, 9, 0);
            this.tblltList.Controls.Add(this.label10, 7, 0);
            this.tblltList.Controls.Add(this.cmbUnit, 6, 0);
            this.tblltList.Controls.Add(this.label23, 5, 0);
            this.tblltList.Controls.Add(this.cmbcomanyname, 1, 0);
            this.tblltList.Controls.Add(this.label11, 2, 0);
            this.tblltList.Controls.Add(this.txtProductname, 3, 0);
            this.tblltList.Controls.Add(this.label2, 4, 0);
            this.tblltList.Controls.Add(this.btnAdd, 10, 0);
            this.tblltList.Controls.Add(this.label12, 0, 0);
            this.tblltList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltList.ForeColor = System.Drawing.Color.Black;
            this.tblltList.Location = new System.Drawing.Point(3, 21);
            this.tblltList.Name = "tblltList";
            this.tblltList.RowCount = 2;
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92F));
            this.tblltList.Size = new System.Drawing.Size(1022, 381);
            this.tblltList.TabIndex = 6;
            // 
            // GvProductInfo
            // 
            this.GvProductInfo.AllowUserToAddRows = false;
            this.GvProductInfo.AllowUserToDeleteRows = false;
            this.GvProductInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvProductInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvProductInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvProductInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.GvProductInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvProductInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.ProductNameg,
            this.Company,
            this.Unit,
            this.SaleUnit,
            this.Quantity,
            this.Code,
            this.Remove});
            this.tblltList.SetColumnSpan(this.GvProductInfo, 11);
            this.GvProductInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvProductInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvProductInfo.Location = new System.Drawing.Point(3, 33);
            this.GvProductInfo.Name = "GvProductInfo";
            this.GvProductInfo.ReadOnly = true;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvProductInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.GvProductInfo.RowHeadersWidth = 20;
            this.GvProductInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvProductInfo.Size = new System.Drawing.Size(1016, 345);
            this.GvProductInfo.TabIndex = 11;
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
            this.ProductNameg.FillWeight = 100.2143F;
            this.ProductNameg.HeaderText = "Product Name";
            this.ProductNameg.Name = "ProductNameg";
            this.ProductNameg.ReadOnly = true;
            // 
            // Company
            // 
            this.Company.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Company.FillWeight = 53.6548F;
            this.Company.HeaderText = "Company";
            this.Company.Name = "Company";
            this.Company.ReadOnly = true;
            this.Company.Width = 140;
            // 
            // Unit
            // 
            this.Unit.FillWeight = 32.86875F;
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            // 
            // SaleUnit
            // 
            this.SaleUnit.FillWeight = 63.06959F;
            this.SaleUnit.HeaderText = "SaleUnit";
            this.SaleUnit.Name = "SaleUnit";
            this.SaleUnit.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.FillWeight = 32.86875F;
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // Code
            // 
            this.Code.FillWeight = 63.06959F;
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            // 
            // Remove
            // 
            this.Remove.FillWeight = 63.06959F;
            this.Remove.HeaderText = "Remove";
            this.Remove.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Remove.Name = "Remove";
            this.Remove.ReadOnly = true;
            this.Remove.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Remove.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(945, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 30);
            this.label8.TabIndex = 45;
            this.label8.Text = "*";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(796, 3);
            this.label10.Margin = new System.Windows.Forms.Padding(3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 24);
            this.label10.TabIndex = 36;
            this.label10.Text = "Quantity:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbUnit
            // 
            this.cmbUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUnit.ForeColor = System.Drawing.Color.Black;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(704, 2);
            this.cmbUnit.Margin = new System.Windows.Forms.Padding(2);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(87, 25);
            this.cmbUnit.TabIndex = 8;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Black;
            this.label23.Location = new System.Drawing.Point(644, 3);
            this.label23.Margin = new System.Windows.Forms.Padding(3);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(55, 24);
            this.label23.TabIndex = 37;
            this.label23.Text = "Unit:";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbcomanyname
            // 
            this.cmbcomanyname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcomanyname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcomanyname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcomanyname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcomanyname.ForeColor = System.Drawing.Color.Black;
            this.cmbcomanyname.FormattingEnabled = true;
            this.cmbcomanyname.Location = new System.Drawing.Point(73, 2);
            this.cmbcomanyname.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcomanyname.Name = "cmbcomanyname";
            this.cmbcomanyname.Size = new System.Drawing.Size(118, 25);
            this.cmbcomanyname.TabIndex = 6;
            this.cmbcomanyname.SelectedIndexChanged += new System.EventHandler(this.cmbcomanyname_SelectedIndexChanged);
            this.cmbcomanyname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcomanyname_KeyDown);
            this.cmbcomanyname.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbcomanyname_MouseClick);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(196, 3);
            this.label11.Margin = new System.Windows.Forms.Padding(3);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 24);
            this.label11.TabIndex = 35;
            this.label11.Text = "Product Name:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProductname
            // 
            this.txtProductname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtProductname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtProductname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductname.Location = new System.Drawing.Point(317, 2);
            this.txtProductname.Margin = new System.Windows.Forms.Padding(2);
            this.txtProductname.Name = "txtProductname";
            this.txtProductname.Size = new System.Drawing.Size(302, 25);
            this.txtProductname.TabIndex = 7;
            this.txtProductname.TextChanged += new System.EventHandler(this.txtProductname_TextChanged);
            this.txtProductname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductname_KeyDown);
            this.txtProductname.Leave += new System.EventHandler(this.txtProductname_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(621, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 30);
            this.label2.TabIndex = 44;
            this.label2.Text = "*";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(967, 2);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(53, 26);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(3, 3);
            this.label12.Margin = new System.Windows.Forms.Padding(3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 24);
            this.label12.TabIndex = 46;
            this.label12.Text = "Prefix:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.label5.Size = new System.Drawing.Size(1034, 37);
            this.label5.TabIndex = 36;
            this.label5.Text = "Delivery Challan";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 9;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.Controls.Add(this.txtVehicalno, 6, 2);
            this.tblltMain.Controls.Add(this.label7, 6, 1);
            this.tblltMain.Controls.Add(this.grpProductDetail, 0, 3);
            this.tblltMain.Controls.Add(this.label20, 0, 2);
            this.tblltMain.Controls.Add(this.cmbcustomername, 5, 1);
            this.tblltMain.Controls.Add(this.dtpchallandate, 3, 1);
            this.tblltMain.Controls.Add(this.label4, 2, 1);
            this.tblltMain.Controls.Add(this.txtchallanno, 1, 1);
            this.tblltMain.Controls.Add(this.label1, 0, 1);
            this.tblltMain.Controls.Add(this.label3, 4, 1);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.btnCustForm, 7, 1);
            this.tblltMain.Controls.Add(this.lblmechname, 1, 2);
            this.tblltMain.Controls.Add(this.label19, 3, 2);
            this.tblltMain.Controls.Add(this.label6, 4, 2);
            this.tblltMain.Controls.Add(this.label9, 5, 2);
            this.tblltMain.Controls.Add(this.tblltButtons, 1, 3);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 5;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.Size = new System.Drawing.Size(1034, 542);
            this.tblltMain.TabIndex = 0;
            // 
            // grpProductDetail
            // 
            this.tblltMain.SetColumnSpan(this.grpProductDetail, 9);
            this.grpProductDetail.Controls.Add(this.tblltList);
            this.grpProductDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpProductDetail.Location = new System.Drawing.Point(3, 98);
            this.grpProductDetail.Name = "grpProductDetail";
            this.grpProductDetail.Size = new System.Drawing.Size(1028, 405);
            this.grpProductDetail.TabIndex = 6;
            this.grpProductDetail.TabStop = false;
            this.grpProductDetail.Text = "Product Details";
            // 
            // btnCustForm
            // 
            this.btnCustForm.BackgroundImage = global::AIOInventorySystem.Desk.Properties.Resources.newbutton;
            this.btnCustForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCustForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCustForm.FlatAppearance.BorderSize = 0;
            this.btnCustForm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCustForm.Location = new System.Drawing.Point(909, 39);
            this.btnCustForm.Margin = new System.Windows.Forms.Padding(2);
            this.btnCustForm.Name = "btnCustForm";
            this.btnCustForm.Size = new System.Drawing.Size(37, 25);
            this.btnCustForm.TabIndex = 4;
            this.btnCustForm.UseVisualStyleBackColor = true;
            this.btnCustForm.Click += new System.EventHandler(this.btnCustForm_Click);
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 9;
            this.tblltMain.SetColumnSpan(this.tblltButtons, 9);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltButtons.Controls.Add(this.btnnew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnsave, 2, 0);
            this.tblltButtons.Controls.Add(this.btnprint, 6, 0);
            this.tblltButtons.Controls.Add(this.btnClose, 5, 0);
            this.tblltButtons.Controls.Add(this.btnDelete, 4, 0);
            this.tblltButtons.Controls.Add(this.btnUpdate, 3, 0);
            this.tblltButtons.Controls.Add(this.btndeliverychallenlist, 7, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 506);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(1034, 36);
            this.tblltButtons.TabIndex = 12;
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(134, 2);
            this.btnnew.Margin = new System.Windows.Forms.Padding(2);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(99, 32);
            this.btnnew.TabIndex = 13;
            this.btnnew.Text = "New";
            this.btnnew.UseVisualStyleBackColor = false;
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            // 
            // btnsave
            // 
            this.btnsave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsave.ForeColor = System.Drawing.Color.White;
            this.btnsave.Location = new System.Drawing.Point(237, 2);
            this.btnsave.Margin = new System.Windows.Forms.Padding(2);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(99, 32);
            this.btnsave.TabIndex = 12;
            this.btnsave.Text = "Save";
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(649, 2);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(99, 32);
            this.btnprint.TabIndex = 17;
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
            this.btnClose.Location = new System.Drawing.Point(546, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(99, 32);
            this.btnClose.TabIndex = 16;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(443, 2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(99, 32);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(340, 2);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(99, 32);
            this.btnUpdate.TabIndex = 14;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btndeliverychallenlist
            // 
            this.btndeliverychallenlist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btndeliverychallenlist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btndeliverychallenlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btndeliverychallenlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndeliverychallenlist.ForeColor = System.Drawing.Color.White;
            this.btndeliverychallenlist.Location = new System.Drawing.Point(752, 2);
            this.btndeliverychallenlist.Margin = new System.Windows.Forms.Padding(2);
            this.btndeliverychallenlist.Name = "btndeliverychallenlist";
            this.btndeliverychallenlist.Size = new System.Drawing.Size(114, 32);
            this.btndeliverychallenlist.TabIndex = 18;
            this.btndeliverychallenlist.Text = "Challan List";
            this.btndeliverychallenlist.UseVisualStyleBackColor = false;
            this.btndeliverychallenlist.Click += new System.EventHandler(this.btndeliverychallenlist_Click);
            // 
            // txtprefixproduct
            // 
            this.txtprefixproduct.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtprefixproduct.ForeColor = System.Drawing.Color.Red;
            this.txtprefixproduct.FormattingEnabled = true;
            this.txtprefixproduct.ItemHeight = 21;
            this.txtprefixproduct.Location = new System.Drawing.Point(12, 158);
            this.txtprefixproduct.Name = "txtprefixproduct";
            this.txtprefixproduct.Size = new System.Drawing.Size(225, 319);
            this.txtprefixproduct.TabIndex = 393;
            this.txtprefixproduct.Visible = false;
            this.txtprefixproduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtprefixproduct_KeyDown);
            this.txtprefixproduct.Leave += new System.EventHandler(this.txtprefixproduct_Leave);
            // 
            // gvProductRemStock
            // 
            this.gvProductRemStock.AllowUserToAddRows = false;
            this.gvProductRemStock.AllowUserToDeleteRows = false;
            this.gvProductRemStock.BackgroundColor = System.Drawing.Color.White;
            this.gvProductRemStock.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvProductRemStock.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.gvProductRemStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProductRemStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductNameR,
            this.RemQtyR,
            this.UnitR});
            this.gvProductRemStock.GridColor = System.Drawing.Color.WhiteSmoke;
            this.gvProductRemStock.Location = new System.Drawing.Point(310, 37);
            this.gvProductRemStock.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gvProductRemStock.Name = "gvProductRemStock";
            this.gvProductRemStock.ReadOnly = true;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.NullValue = null;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvProductRemStock.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.gvProductRemStock.RowHeadersWidth = 10;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle14.NullValue = null;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.gvProductRemStock.RowsDefaultCellStyle = dataGridViewCellStyle14;
            this.gvProductRemStock.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvProductRemStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gvProductRemStock.Size = new System.Drawing.Size(280, 80);
            this.gvProductRemStock.TabIndex = 163;
            this.gvProductRemStock.Visible = false;
            // 
            // ProductNameR
            // 
            this.ProductNameR.HeaderText = "ProductName";
            this.ProductNameR.Name = "ProductNameR";
            this.ProductNameR.ReadOnly = true;
            this.ProductNameR.Width = 125;
            // 
            // RemQtyR
            // 
            this.RemQtyR.HeaderText = "RemQty";
            this.RemQtyR.Name = "RemQtyR";
            this.RemQtyR.ReadOnly = true;
            this.RemQtyR.Width = 70;
            // 
            // UnitR
            // 
            this.UnitR.HeaderText = "Unit";
            this.UnitR.Name = "UnitR";
            this.UnitR.ReadOnly = true;
            this.UnitR.Width = 70;
            // 
            // dtgvcode
            // 
            this.dtgvcode.AllowUserToAddRows = false;
            this.dtgvcode.AllowUserToDeleteRows = false;
            this.dtgvcode.BackgroundColor = System.Drawing.Color.White;
            this.dtgvcode.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvcode.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dtgvcode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvcode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CodeC});
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvcode.DefaultCellStyle = dataGridViewCellStyle16;
            this.dtgvcode.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgvcode.Location = new System.Drawing.Point(800, 158);
            this.dtgvcode.Name = "dtgvcode";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.NullValue = null;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvcode.RowHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.dtgvcode.RowHeadersWidth = 10;
            dataGridViewCellStyle18.NullValue = null;
            this.dtgvcode.RowsDefaultCellStyle = dataGridViewCellStyle18;
            this.dtgvcode.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgvcode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dtgvcode.Size = new System.Drawing.Size(137, 173);
            this.dtgvcode.TabIndex = 409;
            this.dtgvcode.Visible = false;
            // 
            // CodeC
            // 
            this.CodeC.HeaderText = "Code";
            this.CodeC.Name = "CodeC";
            this.CodeC.Width = 125;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuantity.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(877, 3);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(65, 25);
            this.txtQuantity.TabIndex = 9;
            this.txtQuantity.Text = "0";
            this.txtQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuantity_KeyDown);
            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantity_KeyPress);
            this.txtQuantity.Leave += new System.EventHandler(this.txtQuantity_Leave);
            // 
            // frmDelieveryChallan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1034, 542);
            this.Controls.Add(this.dtgvcode);
            this.Controls.Add(this.txtprefixproduct);
            this.Controls.Add(this.gvProductRemStock);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "frmDelieveryChallan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delivery Challan";
            this.Load += new System.EventHandler(this.frmDelieveryChallan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tblltList.ResumeLayout(false);
            this.tblltList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvProductInfo)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.grpProductDetail.ResumeLayout(false);
            this.tblltButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvProductRemStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvcode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpchallandate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtchallanno;
        private System.Windows.Forms.ComboBox cmbcomanyname;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProductname;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblmechname;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cmbcustomername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tblltList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox txtVehicalno;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Button btnCustForm;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btndeliverychallenlist;
        private RachitControls.NumericTextBox txtQuantity;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox grpProductDetail;
        private System.Windows.Forms.DataGridView GvProductInfo;
        private System.Windows.Forms.ListBox txtprefixproduct;
        private System.Windows.Forms.DataGridView gvProductRemStock;
        private System.Windows.Forms.DataGridView dtgvcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductNameg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Company;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewImageColumn Remove;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductNameR;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemQtyR;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitR;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodeC;
    }
}