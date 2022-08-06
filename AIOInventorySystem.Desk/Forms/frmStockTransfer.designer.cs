namespace AIOInventorySystem.Desk.Forms
{
    partial class frmStockTransfer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtpPorderdate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.tblltProductDetails = new System.Windows.Forms.TableLayoutPanel();
            this.GvProductInfo = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductNameg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Company = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantityg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DestinationId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remove = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblAvailableStock = new System.Windows.Forms.Label();
            this.lblout = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtpname = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtQuantity = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnTransfer = new System.Windows.Forms.Button();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.cmbDestinationNames = new System.Windows.Forms.ComboBox();
            this.grpProductDetail = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbSourceNames = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dtgvcode = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblltProductDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvProductInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tblltButtons.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.grpProductDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvcode)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpPorderdate
            // 
            this.dtpPorderdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpPorderdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPorderdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPorderdate.Location = new System.Drawing.Point(85, 41);
            this.dtpPorderdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpPorderdate.Name = "dtpPorderdate";
            this.dtpPorderdate.Size = new System.Drawing.Size(112, 25);
            this.dtpPorderdate.TabIndex = 1;
            this.dtpPorderdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpPorderdate_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(2, 41);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 25);
            this.label4.TabIndex = 48;
            this.label4.Text = "Date:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblltProductDetails
            // 
            this.tblltProductDetails.ColumnCount = 10;
            this.tblltProductDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltProductDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltProductDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tblltProductDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltProductDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltProductDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tblltProductDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltProductDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltProductDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltProductDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltProductDetails.Controls.Add(this.GvProductInfo, 0, 1);
            this.tblltProductDetails.Controls.Add(this.btnGetAll, 9, 0);
            this.tblltProductDetails.Controls.Add(this.btnAdd, 8, 0);
            this.tblltProductDetails.Controls.Add(this.lblAvailableStock, 7, 0);
            this.tblltProductDetails.Controls.Add(this.lblout, 6, 0);
            this.tblltProductDetails.Controls.Add(this.label3, 0, 0);
            this.tblltProductDetails.Controls.Add(this.txtpname, 1, 0);
            this.tblltProductDetails.Controls.Add(this.label6, 3, 0);
            this.tblltProductDetails.Controls.Add(this.label12, 2, 0);
            this.tblltProductDetails.Controls.Add(this.label13, 5, 0);
            this.tblltProductDetails.Controls.Add(this.txtQuantity, 4, 0);
            this.tblltProductDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltProductDetails.Location = new System.Drawing.Point(3, 21);
            this.tblltProductDetails.Name = "tblltProductDetails";
            this.tblltProductDetails.RowCount = 2;
            this.tblltProductDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltProductDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92F));
            this.tblltProductDetails.Size = new System.Drawing.Size(824, 360);
            this.tblltProductDetails.TabIndex = 4;
            // 
            // GvProductInfo
            // 
            this.GvProductInfo.AllowUserToAddRows = false;
            this.GvProductInfo.AllowUserToDeleteRows = false;
            this.GvProductInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvProductInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvProductInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvProductInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvProductInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvProductInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.ProductNameg,
            this.Company,
            this.Unit,
            this.PurUnit,
            this.Quantityg,
            this.Code,
            this.SourceId,
            this.DestinationId,
            this.Remove});
            this.tblltProductDetails.SetColumnSpan(this.GvProductInfo, 10);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GvProductInfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.GvProductInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvProductInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvProductInfo.Location = new System.Drawing.Point(3, 31);
            this.GvProductInfo.Name = "GvProductInfo";
            this.GvProductInfo.RowHeadersWidth = 15;
            this.GvProductInfo.Size = new System.Drawing.Size(818, 326);
            this.GvProductInfo.TabIndex = 8;
            this.GvProductInfo.TabStop = false;
            this.GvProductInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvProductInfo_CellClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // ProductNameg
            // 
            this.ProductNameg.FillWeight = 200F;
            this.ProductNameg.HeaderText = "Product Name";
            this.ProductNameg.Name = "ProductNameg";
            // 
            // Company
            // 
            this.Company.FillWeight = 50F;
            this.Company.HeaderText = "Company";
            this.Company.Name = "Company";
            // 
            // Unit
            // 
            this.Unit.FillWeight = 50F;
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            // 
            // PurUnit
            // 
            this.PurUnit.FillWeight = 42.86504F;
            this.PurUnit.HeaderText = "Pur Unit";
            this.PurUnit.Name = "PurUnit";
            this.PurUnit.Visible = false;
            // 
            // Quantityg
            // 
            this.Quantityg.FillWeight = 50F;
            this.Quantityg.HeaderText = "Quantity";
            this.Quantityg.Name = "Quantityg";
            // 
            // Code
            // 
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            this.Code.Visible = false;
            // 
            // SourceId
            // 
            this.SourceId.HeaderText = "SourceId";
            this.SourceId.Name = "SourceId";
            this.SourceId.Visible = false;
            // 
            // DestinationId
            // 
            this.DestinationId.HeaderText = "DestinationId";
            this.DestinationId.Name = "DestinationId";
            this.DestinationId.Visible = false;
            // 
            // Remove
            // 
            this.Remove.FillWeight = 50F;
            this.Remove.HeaderText = "Remove";
            this.Remove.Name = "Remove";
            // 
            // btnGetAll
            // 
            this.btnGetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnGetAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGetAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAll.ForeColor = System.Drawing.Color.White;
            this.btnGetAll.Location = new System.Drawing.Point(754, 1);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(68, 26);
            this.btnGetAll.TabIndex = 7;
            this.btnGetAll.Text = "GetAll";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(689, 1);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(61, 26);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblAvailableStock
            // 
            this.lblAvailableStock.AutoSize = true;
            this.lblAvailableStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvailableStock.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvailableStock.ForeColor = System.Drawing.Color.Black;
            this.lblAvailableStock.Location = new System.Drawing.Point(599, 2);
            this.lblAvailableStock.Margin = new System.Windows.Forms.Padding(2);
            this.lblAvailableStock.Name = "lblAvailableStock";
            this.lblAvailableStock.Size = new System.Drawing.Size(86, 24);
            this.lblAvailableStock.TabIndex = 165;
            this.lblAvailableStock.Text = "0";
            this.lblAvailableStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblout
            // 
            this.lblout.AutoSize = true;
            this.lblout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblout.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblout.ForeColor = System.Drawing.Color.Black;
            this.lblout.Location = new System.Drawing.Point(534, 2);
            this.lblout.Margin = new System.Windows.Forms.Padding(2);
            this.lblout.Name = "lblout";
            this.lblout.Size = new System.Drawing.Size(61, 24);
            this.lblout.TabIndex = 164;
            this.lblout.Text = "Out of  ";
            this.lblout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(2, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 24);
            this.label3.TabIndex = 11;
            this.label3.Text = "Product Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtpname
            // 
            this.txtpname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtpname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtpname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtpname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpname.Location = new System.Drawing.Point(109, 2);
            this.txtpname.Margin = new System.Windows.Forms.Padding(2);
            this.txtpname.Name = "txtpname";
            this.txtpname.Size = new System.Drawing.Size(243, 25);
            this.txtpname.TabIndex = 4;
            this.txtpname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpname_KeyDown);
            this.txtpname.Leave += new System.EventHandler(this.txtpname_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(380, 2);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 24);
            this.label6.TabIndex = 12;
            this.label6.Text = "Quantity:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Left;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label12.Location = new System.Drawing.Point(355, 1);
            this.label12.Margin = new System.Windows.Forms.Padding(1);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 26);
            this.label12.TabIndex = 44;
            this.label12.Text = "*";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Left;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label13.Location = new System.Drawing.Point(509, 1);
            this.label13.Margin = new System.Windows.Forms.Padding(1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(22, 26);
            this.label13.TabIndex = 45;
            this.label13.Text = "*";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuantity.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(445, 2);
            this.txtQuantity.Margin = new System.Windows.Forms.Padding(2);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(61, 25);
            this.txtQuantity.TabIndex = 5;
            this.txtQuantity.Text = "0";
            this.txtQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuantity_KeyDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(273, 153);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(295, 283);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 162;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label9, 6);
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(834, 39);
            this.label9.TabIndex = 34;
            this.label9.Text = "Stock Transfer";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 5;
            this.tblltMain.SetColumnSpan(this.tblltButtons, 6);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.5F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.5F));
            this.tblltButtons.Controls.Add(this.btnClose, 3, 0);
            this.tblltButtons.Controls.Add(this.btnNew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnTransfer, 2, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 456);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(834, 36);
            this.tblltButtons.TabIndex = 9;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(456, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(71, 32);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(306, 2);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(71, 32);
            this.btnNew.TabIndex = 10;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnTransfer
            // 
            this.btnTransfer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnTransfer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTransfer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTransfer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransfer.ForeColor = System.Drawing.Color.White;
            this.btnTransfer.Location = new System.Drawing.Point(381, 2);
            this.btnTransfer.Margin = new System.Windows.Forms.Padding(2);
            this.btnTransfer.Name = "btnTransfer";
            this.btnTransfer.Size = new System.Drawing.Size(71, 32);
            this.btnTransfer.TabIndex = 9;
            this.btnTransfer.Text = "Transfer";
            this.btnTransfer.UseVisualStyleBackColor = false;
            this.btnTransfer.Click += new System.EventHandler(this.btnTransfer_Click);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 6;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.999851F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.99979F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.99984F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.99958F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.00133F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.99961F));
            this.tblltMain.Controls.Add(this.cmbDestinationNames, 5, 1);
            this.tblltMain.Controls.Add(this.grpProductDetail, 0, 2);
            this.tblltMain.Controls.Add(this.label7, 4, 1);
            this.tblltMain.Controls.Add(this.label5, 2, 1);
            this.tblltMain.Controls.Add(this.tblltButtons, 0, 3);
            this.tblltMain.Controls.Add(this.cmbSourceNames, 3, 1);
            this.tblltMain.Controls.Add(this.dtpPorderdate, 1, 1);
            this.tblltMain.Controls.Add(this.label4, 0, 1);
            this.tblltMain.Controls.Add(this.label9, 0, 0);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.Size = new System.Drawing.Size(834, 492);
            this.tblltMain.TabIndex = 0;
            // 
            // cmbDestinationNames
            // 
            this.cmbDestinationNames.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbDestinationNames.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbDestinationNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbDestinationNames.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDestinationNames.FormattingEnabled = true;
            this.cmbDestinationNames.Location = new System.Drawing.Point(616, 41);
            this.cmbDestinationNames.Margin = new System.Windows.Forms.Padding(2);
            this.cmbDestinationNames.Name = "cmbDestinationNames";
            this.cmbDestinationNames.Size = new System.Drawing.Size(216, 25);
            this.cmbDestinationNames.TabIndex = 3;
            this.cmbDestinationNames.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbDestinationNames_KeyDown);
            // 
            // grpProductDetail
            // 
            this.tblltMain.SetColumnSpan(this.grpProductDetail, 6);
            this.grpProductDetail.Controls.Add(this.tblltProductDetails);
            this.grpProductDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpProductDetail.Location = new System.Drawing.Point(2, 70);
            this.grpProductDetail.Margin = new System.Windows.Forms.Padding(2);
            this.grpProductDetail.Name = "grpProductDetail";
            this.grpProductDetail.Size = new System.Drawing.Size(830, 384);
            this.grpProductDetail.TabIndex = 4;
            this.grpProductDetail.TabStop = false;
            this.grpProductDetail.Text = "Product Details";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(525, 41);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 25);
            this.label7.TabIndex = 164;
            this.label7.Text = "Destination:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(201, 41);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 25);
            this.label5.TabIndex = 163;
            this.label5.Text = "Source:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbSourceNames
            // 
            this.cmbSourceNames.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSourceNames.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSourceNames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSourceNames.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSourceNames.FormattingEnabled = true;
            this.cmbSourceNames.Location = new System.Drawing.Point(292, 41);
            this.cmbSourceNames.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSourceNames.Name = "cmbSourceNames";
            this.cmbSourceNames.Size = new System.Drawing.Size(229, 25);
            this.cmbSourceNames.TabIndex = 2;
            this.cmbSourceNames.SelectedIndexChanged += new System.EventHandler(this.cmbSourceNames_SelectedIndexChanged);
            this.cmbSourceNames.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSourceNames_KeyDown);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(223, 300);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(425, 30);
            this.progressBar1.TabIndex = 163;
            this.progressBar1.Visible = false;
            // 
            // dtgvcode
            // 
            this.dtgvcode.AllowUserToAddRows = false;
            this.dtgvcode.AllowUserToDeleteRows = false;
            this.dtgvcode.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvcode.BackgroundColor = System.Drawing.Color.White;
            this.dtgvcode.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvcode.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dtgvcode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvcode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvcode.DefaultCellStyle = dataGridViewCellStyle4;
            this.dtgvcode.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgvcode.Location = new System.Drawing.Point(408, 123);
            this.dtgvcode.Margin = new System.Windows.Forms.Padding(2);
            this.dtgvcode.Name = "dtgvcode";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.NullValue = null;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvcode.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dtgvcode.RowHeadersWidth = 10;
            dataGridViewCellStyle6.NullValue = null;
            this.dtgvcode.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dtgvcode.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgvcode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dtgvcode.Size = new System.Drawing.Size(139, 162);
            this.dtgvcode.TabIndex = 164;
            this.dtgvcode.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Code";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // frmStockTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(834, 492);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.dtgvcode);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmStockTransfer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Transfer";
            this.Load += new System.EventHandler(this.frmStockTransfer_Load);
            this.tblltProductDetails.ResumeLayout(false);
            this.tblltProductDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvProductInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tblltButtons.ResumeLayout(false);
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.grpProductDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvcode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtpname;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpPorderdate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltProductDetails;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbSourceNames;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox cmbDestinationNames;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblout;
        private System.Windows.Forms.Label lblAvailableStock;
        private System.Windows.Forms.DataGridView dtgvcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnTransfer;
        private System.Windows.Forms.Button btnGetAll;
        private System.Windows.Forms.Button btnAdd;
        private RachitControls.NumericTextBox txtQuantity;
        private System.Windows.Forms.GroupBox grpProductDetail;
        private System.Windows.Forms.DataGridView GvProductInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductNameg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Company;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantityg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceId;
        private System.Windows.Forms.DataGridViewTextBoxColumn DestinationId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remove;
    }
}