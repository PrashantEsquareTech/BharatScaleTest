namespace AIOInventorySystem.Desk.Forms
{
    partial class frmSaleBillReturnCustomerWise
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.dtpsrdate = new System.Windows.Forms.DateTimePicker();
            this.label19 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbcustomername = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtProductname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.tblltDetailRow2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtBillNos = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQuantity = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.pnlMultiSaleReturn = new System.Windows.Forms.Panel();
            this.tblltPanel = new System.Windows.Forms.TableLayoutPanel();
            this.dtgvBillList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOk = new System.Windows.Forms.Button();
            this.dtgvProductInfo = new System.Windows.Forms.DataGridView();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnbilllist = new System.Windows.Forms.Button();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblltMasterRow1 = new System.Windows.Forms.TableLayoutPanel();
            this.tblltREason = new System.Windows.Forms.TableLayoutPanel();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.ProductNameg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Company = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BillNosg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleReturnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblltDetailRow2.SuspendLayout();
            this.pnlMultiSaleReturn.SuspendLayout();
            this.tblltPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvBillList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvProductInfo)).BeginInit();
            this.tblltButtons.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.tblltMasterRow1.SuspendLayout();
            this.tblltREason.SuspendLayout();
            this.grpList.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtReason
            // 
            this.txtReason.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtReason.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtReason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReason.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReason.Location = new System.Drawing.Point(128, 2);
            this.txtReason.Margin = new System.Windows.Forms.Padding(2);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(754, 25);
            this.txtReason.TabIndex = 8;
            this.txtReason.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtReason_KeyDown);
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
            this.label4.Size = new System.Drawing.Size(102, 26);
            this.label4.TabIndex = 111;
            this.label4.Text = "Product Name:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label37.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.ForeColor = System.Drawing.Color.Black;
            this.label37.Location = new System.Drawing.Point(2, 2);
            this.label37.Margin = new System.Windows.Forms.Padding(2);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(122, 26);
            this.label37.TabIndex = 149;
            this.label37.Text = "Reason:";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpsrdate
            // 
            this.dtpsrdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpsrdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpsrdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpsrdate.Location = new System.Drawing.Point(108, 2);
            this.dtpsrdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpsrdate.Name = "dtpsrdate";
            this.dtpsrdate.Size = new System.Drawing.Size(110, 25);
            this.dtpsrdate.TabIndex = 0;
            this.dtpsrdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpsrdate_KeyDown);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(2, 2);
            this.label19.Margin = new System.Windows.Forms.Padding(2);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(102, 26);
            this.label19.TabIndex = 110;
            this.label19.Text = "S R Date:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(617, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 30);
            this.label7.TabIndex = 44;
            this.label7.Text = "*";
            // 
            // cmbcustomername
            // 
            this.cmbcustomername.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcustomername.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcustomername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcustomername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustomername.FormattingEnabled = true;
            this.cmbcustomername.Location = new System.Drawing.Point(354, 2);
            this.cmbcustomername.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustomername.Name = "cmbcustomername";
            this.cmbcustomername.Size = new System.Drawing.Size(261, 25);
            this.cmbcustomername.TabIndex = 1;
            this.cmbcustomername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcustomername_KeyDown);
            this.cmbcustomername.Leave += new System.EventHandler(this.cmbcustomername_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(222, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 26);
            this.label3.TabIndex = 31;
            this.label3.Text = "Customer Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(504, 2);
            this.label10.Margin = new System.Windows.Forms.Padding(2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 26);
            this.label10.TabIndex = 36;
            this.label10.Text = "Quantity:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label10.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(642, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 30);
            this.label8.TabIndex = 45;
            this.label8.Text = "*";
            // 
            // txtProductname
            // 
            this.txtProductname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtProductname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtProductname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductname.Location = new System.Drawing.Point(108, 2);
            this.txtProductname.Margin = new System.Windows.Forms.Padding(2);
            this.txtProductname.Name = "txtProductname";
            this.txtProductname.Size = new System.Drawing.Size(208, 25);
            this.txtProductname.TabIndex = 2;
            this.txtProductname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductname_KeyDown);
            this.txtProductname.Leave += new System.EventHandler(this.txtProductname_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(318, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 30);
            this.label2.TabIndex = 44;
            this.label2.Text = "*";
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
            this.label5.TabIndex = 12;
            this.label5.Text = "Sale Return";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbUnit
            // 
            this.cmbUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.Enabled = false;
            this.cmbUnit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUnit.ForeColor = System.Drawing.Color.Black;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(416, 2);
            this.cmbUnit.Margin = new System.Windows.Forms.Padding(2);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(84, 25);
            this.cmbUnit.TabIndex = 3;
            // 
            // tblltDetailRow2
            // 
            this.tblltDetailRow2.ColumnCount = 11;
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltDetailRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltDetailRow2.Controls.Add(this.btnAdd, 10, 0);
            this.tblltDetailRow2.Controls.Add(this.txtBillNos, 9, 0);
            this.tblltDetailRow2.Controls.Add(this.label4, 0, 0);
            this.tblltDetailRow2.Controls.Add(this.label6, 8, 0);
            this.tblltDetailRow2.Controls.Add(this.txtProductname, 1, 0);
            this.tblltDetailRow2.Controls.Add(this.cmbUnit, 4, 0);
            this.tblltDetailRow2.Controls.Add(this.label8, 7, 0);
            this.tblltDetailRow2.Controls.Add(this.label1, 3, 0);
            this.tblltDetailRow2.Controls.Add(this.label10, 5, 0);
            this.tblltDetailRow2.Controls.Add(this.label2, 2, 0);
            this.tblltDetailRow2.Controls.Add(this.txtQuantity, 6, 0);
            this.tblltDetailRow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltDetailRow2.Location = new System.Drawing.Point(0, 63);
            this.tblltDetailRow2.Margin = new System.Windows.Forms.Padding(0);
            this.tblltDetailRow2.Name = "tblltDetailRow2";
            this.tblltDetailRow2.RowCount = 1;
            this.tblltDetailRow2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltDetailRow2.Size = new System.Drawing.Size(884, 30);
            this.tblltDetailRow2.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(828, 2);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(54, 26);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtBillNos
            // 
            this.txtBillNos.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtBillNos.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtBillNos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBillNos.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillNos.Location = new System.Drawing.Point(740, 2);
            this.txtBillNos.Margin = new System.Windows.Forms.Padding(2);
            this.txtBillNos.Name = "txtBillNos";
            this.txtBillNos.Size = new System.Drawing.Size(84, 25);
            this.txtBillNos.TabIndex = 5;
            this.txtBillNos.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(670, 2);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 26);
            this.label6.TabIndex = 150;
            this.label6.Text = "Bill Nos:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(346, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 26);
            this.label1.TabIndex = 151;
            this.label1.Text = "Sale Unit:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuantity.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(574, 2);
            this.txtQuantity.Margin = new System.Windows.Forms.Padding(2);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(66, 25);
            this.txtQuantity.TabIndex = 4;
            // 
            // pnlMultiSaleReturn
            // 
            this.pnlMultiSaleReturn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlMultiSaleReturn.BackColor = System.Drawing.Color.DarkCyan;
            this.pnlMultiSaleReturn.Controls.Add(this.tblltPanel);
            this.pnlMultiSaleReturn.Location = new System.Drawing.Point(226, 146);
            this.pnlMultiSaleReturn.Name = "pnlMultiSaleReturn";
            this.pnlMultiSaleReturn.Size = new System.Drawing.Size(442, 246);
            this.pnlMultiSaleReturn.TabIndex = 30;
            // 
            // tblltPanel
            // 
            this.tblltPanel.BackColor = System.Drawing.Color.Azure;
            this.tblltPanel.ColumnCount = 2;
            this.tblltPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.7104F));
            this.tblltPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.28959F));
            this.tblltPanel.Controls.Add(this.dtgvBillList, 0, 0);
            this.tblltPanel.Controls.Add(this.btnOk, 1, 1);
            this.tblltPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltPanel.Location = new System.Drawing.Point(0, 0);
            this.tblltPanel.Margin = new System.Windows.Forms.Padding(0);
            this.tblltPanel.Name = "tblltPanel";
            this.tblltPanel.RowCount = 2;
            this.tblltPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.17886F));
            this.tblltPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.82114F));
            this.tblltPanel.Size = new System.Drawing.Size(442, 246);
            this.tblltPanel.TabIndex = 30;
            // 
            // dtgvBillList
            // 
            this.dtgvBillList.AllowUserToAddRows = false;
            this.dtgvBillList.BackgroundColor = System.Drawing.Color.White;
            this.dtgvBillList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvBillList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column5,
            this.Column4,
            this.Column6,
            this.Column7});
            this.tblltPanel.SetColumnSpan(this.dtgvBillList, 2);
            this.dtgvBillList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvBillList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgvBillList.Location = new System.Drawing.Point(3, 3);
            this.dtgvBillList.Name = "dtgvBillList";
            this.dtgvBillList.RowHeadersWidth = 15;
            this.dtgvBillList.Size = new System.Drawing.Size(436, 206);
            this.dtgvBillList.TabIndex = 1;
            this.dtgvBillList.Visible = false;
            this.dtgvBillList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvBillList_CellEndEdit);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Select";
            this.Column1.Name = "Column1";
            this.Column1.Width = 45;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "BillNo";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Sale Qty";
            this.Column3.Name = "Column3";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Actual Qty";
            this.Column5.Name = "Column5";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Return Qty";
            this.Column4.Name = "Column4";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "SaleId";
            this.Column6.Name = "Column6";
            this.Column6.Visible = false;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "SRId";
            this.Column7.Name = "Column7";
            this.Column7.Visible = false;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnOk.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(372, 214);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(68, 30);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // dtgvProductInfo
            // 
            this.dtgvProductInfo.AllowUserToAddRows = false;
            this.dtgvProductInfo.AllowUserToDeleteRows = false;
            this.dtgvProductInfo.BackgroundColor = System.Drawing.Color.White;
            this.dtgvProductInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvProductInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvProductInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvProductInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductNameg,
            this.Company,
            this.Unit,
            this.SaleUnit,
            this.Quantity,
            this.BillNosg,
            this.SaleId,
            this.SaleReturnId,
            this.SaleQty});
            this.dtgvProductInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvProductInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgvProductInfo.Location = new System.Drawing.Point(3, 21);
            this.dtgvProductInfo.Name = "dtgvProductInfo";
            this.dtgvProductInfo.ReadOnly = true;
            this.dtgvProductInfo.RowHeadersWidth = 10;
            this.dtgvProductInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvProductInfo.Size = new System.Drawing.Size(876, 287);
            this.dtgvProductInfo.TabIndex = 7;
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
            this.tblltButtons.Controls.Add(this.btnnew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnsave, 2, 0);
            this.tblltButtons.Controls.Add(this.btnprint, 5, 0);
            this.tblltButtons.Controls.Add(this.btnClose, 4, 0);
            this.tblltButtons.Controls.Add(this.btnUpdate, 3, 0);
            this.tblltButtons.Controls.Add(this.btnbilllist, 6, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 436);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(884, 36);
            this.tblltButtons.TabIndex = 9;
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(112, 2);
            this.btnnew.Margin = new System.Windows.Forms.Padding(2);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(106, 32);
            this.btnnew.TabIndex = 10;
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
            this.btnsave.Location = new System.Drawing.Point(222, 2);
            this.btnsave.Margin = new System.Windows.Forms.Padding(2);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(106, 32);
            this.btnsave.TabIndex = 9;
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
            this.btnprint.Location = new System.Drawing.Point(552, 2);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(106, 32);
            this.btnprint.TabIndex = 13;
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
            this.btnClose.Location = new System.Drawing.Point(442, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(106, 32);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(332, 2);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(106, 32);
            this.btnUpdate.TabIndex = 11;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnbilllist
            // 
            this.btnbilllist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnbilllist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnbilllist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnbilllist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnbilllist.ForeColor = System.Drawing.Color.White;
            this.btnbilllist.Location = new System.Drawing.Point(662, 2);
            this.btnbilllist.Margin = new System.Windows.Forms.Padding(2);
            this.btnbilllist.Name = "btnbilllist";
            this.btnbilllist.Size = new System.Drawing.Size(106, 32);
            this.btnbilllist.TabIndex = 14;
            this.btnbilllist.Text = "Bill List";
            this.btnbilllist.UseVisualStyleBackColor = false;
            this.btnbilllist.Click += new System.EventHandler(this.btnbilllist_Click);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.tblltMasterRow1, 0, 1);
            this.tblltMain.Controls.Add(this.tblltButtons, 0, 5);
            this.tblltMain.Controls.Add(this.tblltDetailRow2, 0, 2);
            this.tblltMain.Controls.Add(this.tblltREason, 0, 4);
            this.tblltMain.Controls.Add(this.grpList, 0, 3);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 6;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.Size = new System.Drawing.Size(884, 472);
            this.tblltMain.TabIndex = 0;
            // 
            // tblltMasterRow1
            // 
            this.tblltMasterRow1.ColumnCount = 6;
            this.tblltMasterRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltMasterRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMasterRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltMasterRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltMasterRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMasterRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltMasterRow1.Controls.Add(this.label19, 0, 0);
            this.tblltMasterRow1.Controls.Add(this.label3, 2, 0);
            this.tblltMasterRow1.Controls.Add(this.dtpsrdate, 1, 0);
            this.tblltMasterRow1.Controls.Add(this.label7, 4, 0);
            this.tblltMasterRow1.Controls.Add(this.cmbcustomername, 3, 0);
            this.tblltMasterRow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMasterRow1.Location = new System.Drawing.Point(0, 33);
            this.tblltMasterRow1.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMasterRow1.Name = "tblltMasterRow1";
            this.tblltMasterRow1.RowCount = 1;
            this.tblltMasterRow1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMasterRow1.Size = new System.Drawing.Size(884, 30);
            this.tblltMasterRow1.TabIndex = 0;
            // 
            // tblltREason
            // 
            this.tblltREason.ColumnCount = 2;
            this.tblltREason.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.31718F));
            this.tblltREason.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.68282F));
            this.tblltREason.Controls.Add(this.label37, 0, 0);
            this.tblltREason.Controls.Add(this.txtReason, 1, 0);
            this.tblltREason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltREason.Location = new System.Drawing.Point(0, 406);
            this.tblltREason.Margin = new System.Windows.Forms.Padding(0);
            this.tblltREason.Name = "tblltREason";
            this.tblltREason.RowCount = 1;
            this.tblltREason.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltREason.Size = new System.Drawing.Size(884, 30);
            this.tblltREason.TabIndex = 8;
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.dtgvProductInfo);
            this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpList.Location = new System.Drawing.Point(1, 94);
            this.grpList.Margin = new System.Windows.Forms.Padding(1);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(882, 311);
            this.grpList.TabIndex = 7;
            this.grpList.TabStop = false;
            this.grpList.Text = "Bill List";
            // 
            // ProductNameg
            // 
            this.ProductNameg.HeaderText = "Product Name";
            this.ProductNameg.Name = "ProductNameg";
            this.ProductNameg.ReadOnly = true;
            this.ProductNameg.Width = 200;
            // 
            // Company
            // 
            this.Company.HeaderText = "Company";
            this.Company.Name = "Company";
            this.Company.ReadOnly = true;
            this.Company.Width = 150;
            // 
            // Unit
            // 
            this.Unit.HeaderText = "Unit";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            // 
            // SaleUnit
            // 
            this.SaleUnit.HeaderText = "Sale Unit";
            this.SaleUnit.Name = "SaleUnit";
            this.SaleUnit.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // BillNosg
            // 
            this.BillNosg.HeaderText = "BillNos";
            this.BillNosg.Name = "BillNosg";
            this.BillNosg.ReadOnly = true;
            this.BillNosg.Width = 150;
            // 
            // SaleId
            // 
            this.SaleId.HeaderText = "SaleID";
            this.SaleId.Name = "SaleId";
            this.SaleId.ReadOnly = true;
            this.SaleId.Visible = false;
            this.SaleId.Width = 50;
            // 
            // SaleReturnId
            // 
            this.SaleReturnId.HeaderText = "SaleReturnId";
            this.SaleReturnId.Name = "SaleReturnId";
            this.SaleReturnId.ReadOnly = true;
            this.SaleReturnId.Visible = false;
            this.SaleReturnId.Width = 50;
            // 
            // SaleQty
            // 
            this.SaleQty.HeaderText = "SaleQty";
            this.SaleQty.Name = "SaleQty";
            this.SaleQty.ReadOnly = true;
            this.SaleQty.Visible = false;
            // 
            // frmSaleBillReturnCustomerWise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(884, 472);
            this.Controls.Add(this.pnlMultiSaleReturn);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.MaximizeBox = false;
            this.Name = "frmSaleBillReturnCustomerWise";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sale Return";
            this.Load += new System.EventHandler(this.frmCustomerBill_Load);
            this.tblltDetailRow2.ResumeLayout(false);
            this.tblltDetailRow2.PerformLayout();
            this.pnlMultiSaleReturn.ResumeLayout(false);
            this.tblltPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvBillList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvProductInfo)).EndInit();
            this.tblltButtons.ResumeLayout(false);
            this.tblltMain.ResumeLayout(false);
            this.tblltMasterRow1.ResumeLayout(false);
            this.tblltMasterRow1.PerformLayout();
            this.tblltREason.ResumeLayout(false);
            this.tblltREason.PerformLayout();
            this.grpList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtProductname;
        private System.Windows.Forms.ComboBox cmbcustomername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpsrdate;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.TableLayoutPanel tblltDetailRow2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBillNos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dtgvBillList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridView dtgvProductInfo;
        private System.Windows.Forms.Panel pnlMultiSaleReturn;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnbilllist;
        private System.Windows.Forms.TableLayoutPanel tblltPanel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltMasterRow1;
        private RachitControls.NumericTextBox txtQuantity;
        private System.Windows.Forms.TableLayoutPanel tblltREason;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductNameg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Company;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn BillNosg;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleReturnId;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleQty;
    }
}