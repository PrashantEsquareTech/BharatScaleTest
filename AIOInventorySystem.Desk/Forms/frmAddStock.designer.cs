namespace AIOInventorySystem.Desk.Forms
{
    partial class frmAddStock
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tblltMasterInput = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbGodownName = new System.Windows.Forms.ComboBox();
            this.dtpPorderdate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblvat = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnImportExcel = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnPrintBarcode = new System.Windows.Forms.Button();
            this.txtvat = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.tblltDetail = new System.Windows.Forms.TableLayoutPanel();
            this.GvProductInfo = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductNameg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Company = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PurUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MandatoryCodeSeries = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsModified = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpiryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StaticBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChooseBarcodeProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrintBarcode = new System.Windows.Forms.DataGridViewImageColumn();
            this.Remove = new System.Windows.Forms.DataGridViewImageColumn();
            this.tblltDetailRow1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtpname = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.txtQuantity = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.rbtnaddproduct = new System.Windows.Forms.Button();
            this.tblltDetailRow2 = new System.Windows.Forms.TableLayoutPanel();
            this.txtsalerate = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.lblBatch = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblExp = new System.Windows.Forms.Label();
            this.dtpExpiry = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBatchNo = new System.Windows.Forms.TextBox();
            this.txtStaticBarcode = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.grpDetail = new System.Windows.Forms.GroupBox();
            this.dtgvcode = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tblltMasterInput.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.tblltDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvProductInfo)).BeginInit();
            this.tblltDetailRow1.SuspendLayout();
            this.tblltDetailRow2.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.grpDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvcode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tblltMasterInput
            // 
            this.tblltMasterInput.ColumnCount = 4;
            this.tblltMasterInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.546539F));
            this.tblltMasterInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.00716F));
            this.tblltMasterInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.73986F));
            this.tblltMasterInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.94511F));
            this.tblltMasterInput.Controls.Add(this.label5, 2, 0);
            this.tblltMasterInput.Controls.Add(this.cmbGodownName, 3, 0);
            this.tblltMasterInput.Controls.Add(this.dtpPorderdate, 1, 0);
            this.tblltMasterInput.Controls.Add(this.label4, 0, 0);
            this.tblltMasterInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMasterInput.Location = new System.Drawing.Point(0, 36);
            this.tblltMasterInput.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMasterInput.Name = "tblltMasterInput";
            this.tblltMasterInput.RowCount = 1;
            this.tblltMasterInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMasterInput.Size = new System.Drawing.Size(864, 29);
            this.tblltMasterInput.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(196, 2);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 25);
            this.label5.TabIndex = 163;
            this.label5.Text = "Stock In:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbGodownName
            // 
            this.cmbGodownName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbGodownName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbGodownName.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbGodownName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGodownName.FormattingEnabled = true;
            this.cmbGodownName.Location = new System.Drawing.Point(288, 2);
            this.cmbGodownName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbGodownName.Name = "cmbGodownName";
            this.cmbGodownName.Size = new System.Drawing.Size(152, 25);
            this.cmbGodownName.TabIndex = 2;
            this.cmbGodownName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbGodownName_KeyDown);
            // 
            // dtpPorderdate
            // 
            this.dtpPorderdate.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtpPorderdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPorderdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPorderdate.Location = new System.Drawing.Point(84, 2);
            this.dtpPorderdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpPorderdate.Name = "dtpPorderdate";
            this.dtpPorderdate.Size = new System.Drawing.Size(108, 25);
            this.dtpPorderdate.TabIndex = 1;
            this.dtpPorderdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpPorderdate_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(2, 2);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 25);
            this.label4.TabIndex = 48;
            this.label4.Text = "Date:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 8;
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltButtons.Controls.Add(this.label1, 3, 0);
            this.tblltButtons.Controls.Add(this.lblvat, 1, 0);
            this.tblltButtons.Controls.Add(this.btnNew, 1, 1);
            this.tblltButtons.Controls.Add(this.btnSave, 2, 1);
            this.tblltButtons.Controls.Add(this.btnClose, 3, 1);
            this.tblltButtons.Controls.Add(this.btnImportExcel, 4, 1);
            this.tblltButtons.Controls.Add(this.btnExportExcel, 5, 1);
            this.tblltButtons.Controls.Add(this.btnPrintBarcode, 6, 1);
            this.tblltButtons.Controls.Add(this.txtvat, 2, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 429);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 2;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48F));
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52F));
            this.tblltButtons.Size = new System.Drawing.Size(864, 63);
            this.tblltButtons.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(326, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 26);
            this.label1.TabIndex = 46;
            this.label1.Text = "%";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Visible = false;
            // 
            // lblvat
            // 
            this.lblvat.AutoSize = true;
            this.lblvat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblvat.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblvat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblvat.Location = new System.Drawing.Point(110, 2);
            this.lblvat.Margin = new System.Windows.Forms.Padding(2);
            this.lblvat.Name = "lblvat";
            this.lblvat.Size = new System.Drawing.Size(104, 26);
            this.lblvat.TabIndex = 14;
            this.lblvat.Text = "Vat:";
            this.lblvat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblvat.Visible = false;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(110, 32);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(104, 29);
            this.btnNew.TabIndex = 14;
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
            this.btnSave.Location = new System.Drawing.Point(218, 32);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(104, 29);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnSave_KeyDown);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(326, 32);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(104, 29);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnImportExcel
            // 
            this.btnImportExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnImportExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnImportExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnImportExcel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportExcel.ForeColor = System.Drawing.Color.White;
            this.btnImportExcel.Location = new System.Drawing.Point(434, 32);
            this.btnImportExcel.Margin = new System.Windows.Forms.Padding(2);
            this.btnImportExcel.Name = "btnImportExcel";
            this.btnImportExcel.Size = new System.Drawing.Size(104, 29);
            this.btnImportExcel.TabIndex = 16;
            this.btnImportExcel.Text = "Import Excel";
            this.btnImportExcel.UseVisualStyleBackColor = false;
            this.btnImportExcel.Click += new System.EventHandler(this.btnImportExcel_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnExportExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExportExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExportExcel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportExcel.ForeColor = System.Drawing.Color.White;
            this.btnExportExcel.Location = new System.Drawing.Point(542, 32);
            this.btnExportExcel.Margin = new System.Windows.Forms.Padding(2);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(104, 29);
            this.btnExportExcel.TabIndex = 17;
            this.btnExportExcel.Text = "Export Excel";
            this.btnExportExcel.UseVisualStyleBackColor = false;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnPrintBarcode
            // 
            this.btnPrintBarcode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnPrintBarcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrintBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrintBarcode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintBarcode.ForeColor = System.Drawing.Color.White;
            this.btnPrintBarcode.Location = new System.Drawing.Point(650, 32);
            this.btnPrintBarcode.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrintBarcode.Name = "btnPrintBarcode";
            this.btnPrintBarcode.Size = new System.Drawing.Size(104, 29);
            this.btnPrintBarcode.TabIndex = 18;
            this.btnPrintBarcode.Text = "Print Barcode";
            this.btnPrintBarcode.UseVisualStyleBackColor = false;
            this.btnPrintBarcode.Click += new System.EventHandler(this.btnPrintBarcode_Click);
            // 
            // txtvat
            // 
            this.txtvat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtvat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtvat.Location = new System.Drawing.Point(218, 2);
            this.txtvat.Margin = new System.Windows.Forms.Padding(2);
            this.txtvat.Name = "txtvat";
            this.txtvat.Size = new System.Drawing.Size(104, 25);
            this.txtvat.TabIndex = 177;
            this.txtvat.TabStop = false;
            this.txtvat.Text = "0";
            this.txtvat.Visible = false;
            this.txtvat.TextChanged += new System.EventHandler(this.txtvat_TextChanged);
            // 
            // tblltDetail
            // 
            this.tblltDetail.ColumnCount = 1;
            this.tblltDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltDetail.Controls.Add(this.GvProductInfo, 0, 2);
            this.tblltDetail.Controls.Add(this.tblltDetailRow1, 0, 0);
            this.tblltDetail.Controls.Add(this.tblltDetailRow2, 0, 1);
            this.tblltDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltDetail.Location = new System.Drawing.Point(3, 21);
            this.tblltDetail.Margin = new System.Windows.Forms.Padding(0);
            this.tblltDetail.Name = "tblltDetail";
            this.tblltDetail.RowCount = 3;
            this.tblltDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.25F));
            this.tblltDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.25F));
            this.tblltDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.5F));
            this.tblltDetail.Size = new System.Drawing.Size(852, 334);
            this.tblltDetail.TabIndex = 3;
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
            this.Quantity,
            this.Code,
            this.MandatoryCodeSeries,
            this.IsModified,
            this.BatchNo,
            this.ExpiryDate,
            this.SaleRate,
            this.Barcode,
            this.StaticBarcode,
            this.ChooseBarcodeProduct,
            this.PrintBarcode,
            this.Remove});
            this.GvProductInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvProductInfo.GridColor = System.Drawing.Color.Red;
            this.GvProductInfo.Location = new System.Drawing.Point(3, 64);
            this.GvProductInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.GvProductInfo.Name = "GvProductInfo";
            this.GvProductInfo.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvProductInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GvProductInfo.RowHeadersWidth = 15;
            this.GvProductInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvProductInfo.Size = new System.Drawing.Size(846, 266);
            this.GvProductInfo.TabIndex = 110;
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
            this.ProductNameg.FillWeight = 85.1371F;
            this.ProductNameg.HeaderText = "Product Name";
            this.ProductNameg.Name = "ProductNameg";
            this.ProductNameg.ReadOnly = true;
            // 
            // Company
            // 
            this.Company.FillWeight = 53.71481F;
            this.Company.HeaderText = "Company";
            this.Company.Name = "Company";
            this.Company.ReadOnly = true;
            // 
            // Unit
            // 
            this.Unit.FillWeight = 53.71481F;
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            // 
            // PurUnit
            // 
            this.PurUnit.FillWeight = 53.71481F;
            this.PurUnit.HeaderText = "PurUnit";
            this.PurUnit.Name = "PurUnit";
            this.PurUnit.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.FillWeight = 53.71481F;
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // Code
            // 
            this.Code.FillWeight = 53.71481F;
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            // 
            // MandatoryCodeSeries
            // 
            this.MandatoryCodeSeries.FillWeight = 53.71481F;
            this.MandatoryCodeSeries.HeaderText = "Mandatory Code Series";
            this.MandatoryCodeSeries.Name = "MandatoryCodeSeries";
            this.MandatoryCodeSeries.ReadOnly = true;
            // 
            // IsModified
            // 
            this.IsModified.HeaderText = "IsModified";
            this.IsModified.Name = "IsModified";
            this.IsModified.ReadOnly = true;
            this.IsModified.Visible = false;
            // 
            // BatchNo
            // 
            this.BatchNo.FillWeight = 53.71481F;
            this.BatchNo.HeaderText = "BatchNo";
            this.BatchNo.Name = "BatchNo";
            this.BatchNo.ReadOnly = true;
            // 
            // ExpiryDate
            // 
            this.ExpiryDate.FillWeight = 53.71481F;
            this.ExpiryDate.HeaderText = "ExpiryDate";
            this.ExpiryDate.Name = "ExpiryDate";
            this.ExpiryDate.ReadOnly = true;
            // 
            // SaleRate
            // 
            this.SaleRate.HeaderText = "SaleRate";
            this.SaleRate.Name = "SaleRate";
            this.SaleRate.ReadOnly = true;
            this.SaleRate.Visible = false;
            // 
            // Barcode
            // 
            this.Barcode.FillWeight = 53.71481F;
            this.Barcode.HeaderText = "Barcode";
            this.Barcode.Name = "Barcode";
            this.Barcode.ReadOnly = true;
            // 
            // StaticBarcode
            // 
            this.StaticBarcode.HeaderText = "Static Barcode";
            this.StaticBarcode.Name = "StaticBarcode";
            this.StaticBarcode.ReadOnly = true;
            this.StaticBarcode.Visible = false;
            // 
            // ChooseBarcodeProduct
            // 
            this.ChooseBarcodeProduct.FillWeight = 53.71481F;
            this.ChooseBarcodeProduct.HeaderText = "Choose Barcode Product";
            this.ChooseBarcodeProduct.Name = "ChooseBarcodeProduct";
            this.ChooseBarcodeProduct.ReadOnly = true;
            this.ChooseBarcodeProduct.Visible = false;
            // 
            // PrintBarcode
            // 
            this.PrintBarcode.HeaderText = "PrintBarcode";
            this.PrintBarcode.Image = global::AIOInventorySystem.Desk.Properties.Resources.button_print_barcode;
            this.PrintBarcode.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.PrintBarcode.Name = "PrintBarcode";
            this.PrintBarcode.ReadOnly = true;
            this.PrintBarcode.Visible = false;
            // 
            // Remove
            // 
            this.Remove.FillWeight = 53.71481F;
            this.Remove.HeaderText = "Remove";
            this.Remove.Image = global::AIOInventorySystem.Desk.Properties.Resources.Remove;
            this.Remove.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Remove.Name = "Remove";
            this.Remove.ReadOnly = true;
            // 
            // tblltDetailRow1
            // 
            this.tblltDetailRow1.ColumnCount = 10;
            this.tblltDetailRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltDetailRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltDetailRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tblltDetailRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltDetailRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tblltDetailRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltDetailRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltDetailRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltDetailRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltDetailRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltDetailRow1.Controls.Add(this.label3, 0, 0);
            this.tblltDetailRow1.Controls.Add(this.txtpname, 1, 0);
            this.tblltDetailRow1.Controls.Add(this.label12, 2, 0);
            this.tblltDetailRow1.Controls.Add(this.label2, 5, 0);
            this.tblltDetailRow1.Controls.Add(this.cmbUnit, 6, 0);
            this.tblltDetailRow1.Controls.Add(this.label6, 7, 0);
            this.tblltDetailRow1.Controls.Add(this.label13, 9, 0);
            this.tblltDetailRow1.Controls.Add(this.txtQuantity, 8, 0);
            this.tblltDetailRow1.Controls.Add(this.rbtnaddproduct, 3, 0);
            this.tblltDetailRow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltDetailRow1.Location = new System.Drawing.Point(0, 0);
            this.tblltDetailRow1.Margin = new System.Windows.Forms.Padding(0);
            this.tblltDetailRow1.Name = "tblltDetailRow1";
            this.tblltDetailRow1.RowCount = 1;
            this.tblltDetailRow1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltDetailRow1.Size = new System.Drawing.Size(852, 30);
            this.tblltDetailRow1.TabIndex = 13;
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
            this.label3.Size = new System.Drawing.Size(106, 26);
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
            this.txtpname.Location = new System.Drawing.Point(112, 2);
            this.txtpname.Margin = new System.Windows.Forms.Padding(2);
            this.txtpname.Name = "txtpname";
            this.txtpname.Size = new System.Drawing.Size(251, 25);
            this.txtpname.TabIndex = 3;
            this.txtpname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpname_KeyDown);
            this.txtpname.Leave += new System.EventHandler(this.txtpname_Leave);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label12.Location = new System.Drawing.Point(368, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 30);
            this.label12.TabIndex = 44;
            this.label12.Text = "*";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(451, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 26);
            this.label2.TabIndex = 162;
            this.label2.Text = "Unit:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.cmbUnit.Location = new System.Drawing.Point(502, 2);
            this.cmbUnit.Margin = new System.Windows.Forms.Padding(2);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(115, 25);
            this.cmbUnit.TabIndex = 5;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(621, 2);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 26);
            this.label6.TabIndex = 12;
            this.label6.Text = "Quantity:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label13.Location = new System.Drawing.Point(809, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(23, 30);
            this.label13.TabIndex = 45;
            this.label13.Text = "*";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuantity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(706, 2);
            this.txtQuantity.Margin = new System.Windows.Forms.Padding(2);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(98, 25);
            this.txtQuantity.TabIndex = 6;
            this.txtQuantity.Text = "0";
            this.txtQuantity.Enter += new System.EventHandler(this.txtQuantity_Enter);
            this.txtQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuantity_KeyDown);
            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantity_KeyPress);
            this.txtQuantity.Leave += new System.EventHandler(this.txtQuantity_Leave);
            // 
            // rbtnaddproduct
            // 
            this.rbtnaddproduct.BackgroundImage = global::AIOInventorySystem.Desk.Properties.Resources.newbutton;
            this.rbtnaddproduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.rbtnaddproduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtnaddproduct.FlatAppearance.BorderSize = 0;
            this.rbtnaddproduct.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbtnaddproduct.Location = new System.Drawing.Point(392, 2);
            this.rbtnaddproduct.Margin = new System.Windows.Forms.Padding(2);
            this.rbtnaddproduct.Name = "rbtnaddproduct";
            this.rbtnaddproduct.Size = new System.Drawing.Size(30, 26);
            this.rbtnaddproduct.TabIndex = 4;
            this.rbtnaddproduct.UseVisualStyleBackColor = true;
            this.rbtnaddproduct.Click += new System.EventHandler(this.rbtnaddproduct_Click);
            // 
            // tblltDetailRow2
            // 
            this.tblltDetailRow2.ColumnCount = 10;
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltDetailRow2.Controls.Add(this.txtsalerate, 5, 0);
            this.tblltDetailRow2.Controls.Add(this.btnAdd, 9, 0);
            this.tblltDetailRow2.Controls.Add(this.btnGetAll, 8, 0);
            this.tblltDetailRow2.Controls.Add(this.lblBatch, 0, 0);
            this.tblltDetailRow2.Controls.Add(this.label8, 6, 0);
            this.tblltDetailRow2.Controls.Add(this.lblExp, 2, 0);
            this.tblltDetailRow2.Controls.Add(this.dtpExpiry, 3, 0);
            this.tblltDetailRow2.Controls.Add(this.label7, 4, 0);
            this.tblltDetailRow2.Controls.Add(this.txtBatchNo, 1, 0);
            this.tblltDetailRow2.Controls.Add(this.txtStaticBarcode, 7, 0);
            this.tblltDetailRow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltDetailRow2.Location = new System.Drawing.Point(0, 30);
            this.tblltDetailRow2.Margin = new System.Windows.Forms.Padding(0);
            this.tblltDetailRow2.Name = "tblltDetailRow2";
            this.tblltDetailRow2.RowCount = 1;
            this.tblltDetailRow2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltDetailRow2.Size = new System.Drawing.Size(852, 30);
            this.tblltDetailRow2.TabIndex = 14;
            // 
            // txtsalerate
            // 
            this.txtsalerate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtsalerate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsalerate.Location = new System.Drawing.Point(412, 2);
            this.txtsalerate.Margin = new System.Windows.Forms.Padding(2);
            this.txtsalerate.Name = "txtsalerate";
            this.txtsalerate.Size = new System.Drawing.Size(81, 25);
            this.txtsalerate.TabIndex = 9;
            this.txtsalerate.Text = "0";
            this.txtsalerate.Visible = false;
            this.txtsalerate.TextChanged += new System.EventHandler(this.txtsalerate_TextChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(781, 2);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(69, 26);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnGetAll
            // 
            this.btnGetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnGetAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGetAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAll.ForeColor = System.Drawing.Color.White;
            this.btnGetAll.Location = new System.Drawing.Point(713, 2);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(64, 26);
            this.btnGetAll.TabIndex = 12;
            this.btnGetAll.Text = "GetAll";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // lblBatch
            // 
            this.lblBatch.AutoSize = true;
            this.lblBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBatch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatch.ForeColor = System.Drawing.Color.Black;
            this.lblBatch.Location = new System.Drawing.Point(2, 2);
            this.lblBatch.Margin = new System.Windows.Forms.Padding(2);
            this.lblBatch.Name = "lblBatch";
            this.lblBatch.Size = new System.Drawing.Size(72, 26);
            this.lblBatch.TabIndex = 163;
            this.lblBatch.Text = "Batch No:";
            this.lblBatch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBatch.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(497, 2);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 26);
            this.label8.TabIndex = 169;
            this.label8.Text = "Static Barcode:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label8.Visible = false;
            // 
            // lblExp
            // 
            this.lblExp.AutoSize = true;
            this.lblExp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExp.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExp.ForeColor = System.Drawing.Color.Black;
            this.lblExp.Location = new System.Drawing.Point(171, 2);
            this.lblExp.Margin = new System.Windows.Forms.Padding(2);
            this.lblExp.Name = "lblExp";
            this.lblExp.Size = new System.Drawing.Size(59, 26);
            this.lblExp.TabIndex = 165;
            this.lblExp.Text = "Expiry:";
            this.lblExp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblExp.Visible = false;
            // 
            // dtpExpiry
            // 
            this.dtpExpiry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpExpiry.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpExpiry.Location = new System.Drawing.Point(234, 2);
            this.dtpExpiry.Margin = new System.Windows.Forms.Padding(2);
            this.dtpExpiry.Name = "dtpExpiry";
            this.dtpExpiry.Size = new System.Drawing.Size(89, 25);
            this.dtpExpiry.TabIndex = 8;
            this.dtpExpiry.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(327, 2);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(81, 26);
            this.label7.TabIndex = 167;
            this.label7.Text = "Sale Rate:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label7.Visible = false;
            // 
            // txtBatchNo
            // 
            this.txtBatchNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBatchNo.Location = new System.Drawing.Point(78, 2);
            this.txtBatchNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtBatchNo.Name = "txtBatchNo";
            this.txtBatchNo.Size = new System.Drawing.Size(89, 25);
            this.txtBatchNo.TabIndex = 7;
            this.txtBatchNo.TabStop = false;
            this.txtBatchNo.Visible = false;
            this.txtBatchNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBatchNo_KeyDown);
            // 
            // txtStaticBarcode
            // 
            this.txtStaticBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStaticBarcode.Location = new System.Drawing.Point(603, 2);
            this.txtStaticBarcode.Margin = new System.Windows.Forms.Padding(2);
            this.txtStaticBarcode.Name = "txtStaticBarcode";
            this.txtStaticBarcode.Size = new System.Drawing.Size(106, 25);
            this.txtStaticBarcode.TabIndex = 10;
            this.txtStaticBarcode.TabStop = false;
            this.txtStaticBarcode.Visible = false;
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
            this.label9.Size = new System.Drawing.Size(864, 36);
            this.label9.TabIndex = 34;
            this.label9.Text = "Add Stock (Opening)";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.label9, 0, 0);
            this.tblltMain.Controls.Add(this.tblltButtons, 0, 3);
            this.tblltMain.Controls.Add(this.tblltMasterInput, 0, 1);
            this.tblltMain.Controls.Add(this.grpDetail, 0, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltMain.Size = new System.Drawing.Size(864, 492);
            this.tblltMain.TabIndex = 0;
            // 
            // grpDetail
            // 
            this.grpDetail.Controls.Add(this.tblltDetail);
            this.grpDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDetail.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDetail.Location = new System.Drawing.Point(3, 68);
            this.grpDetail.Name = "grpDetail";
            this.grpDetail.Size = new System.Drawing.Size(858, 358);
            this.grpDetail.TabIndex = 3;
            this.grpDetail.TabStop = false;
            this.grpDetail.Text = "Product Detail";
            // 
            // dtgvcode
            // 
            this.dtgvcode.AllowUserToAddRows = false;
            this.dtgvcode.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dtgvcode.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dtgvcode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgvcode.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvcode.BackgroundColor = System.Drawing.Color.White;
            this.dtgvcode.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvcode.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dtgvcode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvcode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvcode.DefaultCellStyle = dataGridViewCellStyle5;
            this.dtgvcode.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgvcode.Location = new System.Drawing.Point(690, 119);
            this.dtgvcode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtgvcode.Name = "dtgvcode";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.NullValue = null;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvcode.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dtgvcode.RowHeadersWidth = 10;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.NullValue = null;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.dtgvcode.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dtgvcode.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgvcode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dtgvcode.Size = new System.Drawing.Size(132, 143);
            this.dtgvcode.TabIndex = 30;
            this.dtgvcode.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Code";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(250, 279);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(364, 23);
            this.progressBar1.TabIndex = 165;
            this.progressBar1.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(290, 202);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(295, 212);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 164;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // frmAddStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(864, 492);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.dtgvcode);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1192, 782);
            this.Name = "frmAddStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Stock (Opening)";
            this.Load += new System.EventHandler(this.frmAddStock_Load);
            this.tblltMasterInput.ResumeLayout(false);
            this.tblltMasterInput.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.tblltButtons.PerformLayout();
            this.tblltDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GvProductInfo)).EndInit();
            this.tblltDetailRow1.ResumeLayout(false);
            this.tblltDetailRow1.PerformLayout();
            this.tblltDetailRow2.ResumeLayout(false);
            this.tblltDetailRow2.PerformLayout();
            this.tblltMain.ResumeLayout(false);
            this.grpDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvcode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtpname;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblvat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker dtpPorderdate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.TableLayoutPanel tblltMasterInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbGodownName;
        private System.Windows.Forms.Label lblExp;
        private System.Windows.Forms.Label lblBatch;
        private System.Windows.Forms.DateTimePicker dtpExpiry;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltDetail;
        private System.Windows.Forms.TableLayoutPanel tblltDetailRow1;
        private System.Windows.Forms.TableLayoutPanel tblltDetailRow2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnGetAll;
        private System.Windows.Forms.Button btnPrintBarcode;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnImportExcel;
        private RachitControls.NumericTextBox txtQuantity;
        private RachitControls.NumericTextBox txtsalerate;
        private System.Windows.Forms.TextBox txtBatchNo;
        private System.Windows.Forms.TextBox txtStaticBarcode;
        private System.Windows.Forms.Button rbtnaddproduct;
        private RachitControls.NumericTextBox txtvat;
        private System.Windows.Forms.DataGridView GvProductInfo;
        private System.Windows.Forms.GroupBox grpDetail;
        private System.Windows.Forms.DataGridView dtgvcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductNameg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Company;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn PurUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn MandatoryCodeSeries;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsModified;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpiryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn StaticBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChooseBarcodeProduct;
        private System.Windows.Forms.DataGridViewImageColumn PrintBarcode;
        private System.Windows.Forms.DataGridViewImageColumn Remove;
    }
}