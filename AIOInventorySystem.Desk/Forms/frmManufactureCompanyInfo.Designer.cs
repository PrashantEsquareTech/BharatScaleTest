namespace AIOInventorySystem.Desk.Forms
{
    partial class frmManufactureCompanyInfo
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
            this.label5 = new System.Windows.Forms.Label();
            this.sgrpBox2 = new System.Windows.Forms.GroupBox();
            this.Gvcompanynfo = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompanyId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContactNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockAlert = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MarathiName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sgrpBox1 = new System.Windows.Forms.GroupBox();
            this.tblltInfo = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtMarathiName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtCompanyID = new System.Windows.Forms.TextBox();
            this.txtStockAlert = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtcontactNo = new System.Windows.Forms.TextBox();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCompanyAddress = new System.Windows.Forms.TextBox();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.sgrpBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gvcompanynfo)).BeginInit();
            this.sgrpBox1.SuspendLayout();
            this.tblltInfo.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
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
            this.label5.Size = new System.Drawing.Size(434, 37);
            this.label5.TabIndex = 36;
            this.label5.Text = "Manufacture Company Info";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sgrpBox2
            // 
            this.sgrpBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.sgrpBox2.Controls.Add(this.Gvcompanynfo);
            this.sgrpBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sgrpBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sgrpBox2.ForeColor = System.Drawing.Color.Black;
            this.sgrpBox2.Location = new System.Drawing.Point(2, 326);
            this.sgrpBox2.Margin = new System.Windows.Forms.Padding(2);
            this.sgrpBox2.Name = "sgrpBox2";
            this.sgrpBox2.Padding = new System.Windows.Forms.Padding(2);
            this.sgrpBox2.Size = new System.Drawing.Size(430, 214);
            this.sgrpBox2.TabIndex = 11;
            this.sgrpBox2.TabStop = false;
            this.sgrpBox2.Text = "Company List";
            // 
            // Gvcompanynfo
            // 
            this.Gvcompanynfo.AllowUserToAddRows = false;
            this.Gvcompanynfo.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Gvcompanynfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Gvcompanynfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Gvcompanynfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.CompanyId,
            this.CompName,
            this.Address,
            this.ContactNo,
            this.StockAlert,
            this.MarathiName,
            this.CompId});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Gvcompanynfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.Gvcompanynfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Gvcompanynfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.Gvcompanynfo.Location = new System.Drawing.Point(2, 20);
            this.Gvcompanynfo.Margin = new System.Windows.Forms.Padding(2);
            this.Gvcompanynfo.Name = "Gvcompanynfo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Gvcompanynfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Gvcompanynfo.RowHeadersWidth = 15;
            this.Gvcompanynfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Gvcompanynfo.Size = new System.Drawing.Size(426, 192);
            this.Gvcompanynfo.TabIndex = 11;
            this.Gvcompanynfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Gvcompanynfo_CellClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // CompanyId
            // 
            this.CompanyId.HeaderText = "CompanyId";
            this.CompanyId.Name = "CompanyId";
            this.CompanyId.Visible = false;
            this.CompanyId.Width = 170;
            // 
            // CompName
            // 
            this.CompName.HeaderText = "Company Name";
            this.CompName.Name = "CompName";
            this.CompName.Width = 150;
            // 
            // Address
            // 
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            this.Address.Width = 120;
            // 
            // ContactNo
            // 
            this.ContactNo.HeaderText = "Contact No";
            this.ContactNo.Name = "ContactNo";
            this.ContactNo.Width = 120;
            // 
            // StockAlert
            // 
            this.StockAlert.HeaderText = "Stock Alert";
            this.StockAlert.Name = "StockAlert";
            // 
            // MarathiName
            // 
            this.MarathiName.HeaderText = "Marathi Name";
            this.MarathiName.Name = "MarathiName";
            this.MarathiName.Visible = false;
            // 
            // CompId
            // 
            this.CompId.HeaderText = "CompId";
            this.CompId.Name = "CompId";
            this.CompId.Visible = false;
            // 
            // sgrpBox1
            // 
            this.sgrpBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.sgrpBox1.Controls.Add(this.tblltInfo);
            this.sgrpBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sgrpBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sgrpBox1.ForeColor = System.Drawing.Color.Black;
            this.sgrpBox1.Location = new System.Drawing.Point(2, 39);
            this.sgrpBox1.Margin = new System.Windows.Forms.Padding(2);
            this.sgrpBox1.Name = "sgrpBox1";
            this.sgrpBox1.Padding = new System.Windows.Forms.Padding(2);
            this.sgrpBox1.Size = new System.Drawing.Size(430, 283);
            this.sgrpBox1.TabIndex = 0;
            this.sgrpBox1.TabStop = false;
            this.sgrpBox1.Text = "Company  Information";
            // 
            // tblltInfo
            // 
            this.tblltInfo.ColumnCount = 3;
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63F));
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltInfo.Controls.Add(this.tblltButtons, 0, 8);
            this.tblltInfo.Controls.Add(this.txtMarathiName, 1, 2);
            this.tblltInfo.Controls.Add(this.label6, 0, 2);
            this.tblltInfo.Controls.Add(this.label1, 0, 0);
            this.tblltInfo.Controls.Add(this.label18, 2, 6);
            this.tblltInfo.Controls.Add(this.txtCompanyID, 1, 0);
            this.tblltInfo.Controls.Add(this.txtStockAlert, 1, 6);
            this.tblltInfo.Controls.Add(this.label2, 0, 1);
            this.tblltInfo.Controls.Add(this.label20, 0, 6);
            this.tblltInfo.Controls.Add(this.label3, 0, 3);
            this.tblltInfo.Controls.Add(this.txtcontactNo, 1, 5);
            this.tblltInfo.Controls.Add(this.txtCompanyName, 1, 1);
            this.tblltInfo.Controls.Add(this.label4, 0, 5);
            this.tblltInfo.Controls.Add(this.label7, 2, 1);
            this.tblltInfo.Controls.Add(this.txtCompanyAddress, 1, 3);
            this.tblltInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblltInfo.Location = new System.Drawing.Point(2, 20);
            this.tblltInfo.Margin = new System.Windows.Forms.Padding(0);
            this.tblltInfo.Name = "tblltInfo";
            this.tblltInfo.RowCount = 9;
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.42886F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.42886F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.42886F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.42886F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.42886F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.42886F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.42886F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.9992F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.9988F));
            this.tblltInfo.Size = new System.Drawing.Size(426, 261);
            this.tblltInfo.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 6;
            this.tblltInfo.SetColumnSpan(this.tblltButtons, 3);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.Controls.Add(this.btnClose, 4, 0);
            this.tblltButtons.Controls.Add(this.btnnew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnDelete, 3, 0);
            this.tblltButtons.Controls.Add(this.btnSave, 2, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 223);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(426, 38);
            this.tblltButtons.TabIndex = 7;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(282, 1);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 36);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnClose_KeyDown);
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(72, 1);
            this.btnnew.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(66, 36);
            this.btnnew.TabIndex = 8;
            this.btnnew.Text = "New";
            this.btnnew.UseVisualStyleBackColor = false;
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            this.btnnew.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnnew_KeyDown);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(212, 1);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(66, 36);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(142, 1);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(66, 36);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnSave_KeyDown);
            // 
            // txtMarathiName
            // 
            this.txtMarathiName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtMarathiName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtMarathiName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMarathiName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMarathiName.ForeColor = System.Drawing.Color.Black;
            this.txtMarathiName.Location = new System.Drawing.Point(129, 60);
            this.txtMarathiName.Margin = new System.Windows.Forms.Padding(2);
            this.txtMarathiName.Name = "txtMarathiName";
            this.txtMarathiName.Size = new System.Drawing.Size(264, 25);
            this.txtMarathiName.TabIndex = 3;
            this.txtMarathiName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMarathiName_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(2, 60);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 25);
            this.label6.TabIndex = 90;
            this.label6.Text = "Marathi  Name:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 25);
            this.label1.TabIndex = 86;
            this.label1.Text = "Company Id:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label18.Location = new System.Drawing.Point(395, 174);
            this.label18.Margin = new System.Windows.Forms.Padding(0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(31, 29);
            this.label18.TabIndex = 88;
            this.label18.Text = "*";
            // 
            // txtCompanyID
            // 
            this.txtCompanyID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtCompanyID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyID.Location = new System.Drawing.Point(129, 2);
            this.txtCompanyID.Margin = new System.Windows.Forms.Padding(2);
            this.txtCompanyID.Name = "txtCompanyID";
            this.txtCompanyID.ReadOnly = true;
            this.txtCompanyID.Size = new System.Drawing.Size(120, 25);
            this.txtCompanyID.TabIndex = 1;
            this.txtCompanyID.TabStop = false;
            // 
            // txtStockAlert
            // 
            this.txtStockAlert.BackColor = System.Drawing.Color.White;
            this.txtStockAlert.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtStockAlert.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStockAlert.ForeColor = System.Drawing.Color.Black;
            this.txtStockAlert.Location = new System.Drawing.Point(129, 176);
            this.txtStockAlert.Margin = new System.Windows.Forms.Padding(2);
            this.txtStockAlert.Name = "txtStockAlert";
            this.txtStockAlert.Size = new System.Drawing.Size(120, 25);
            this.txtStockAlert.TabIndex = 6;
            this.txtStockAlert.Text = "0";
            this.txtStockAlert.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStockAlert_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(2, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 25);
            this.label2.TabIndex = 82;
            this.label2.Text = "Company  Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(2, 176);
            this.label20.Margin = new System.Windows.Forms.Padding(2);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(123, 25);
            this.label20.TabIndex = 87;
            this.label20.Text = "Stock Alert Value:";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(2, 89);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 25);
            this.label3.TabIndex = 83;
            this.label3.Text = "Company Address:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtcontactNo
            // 
            this.txtcontactNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtcontactNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcontactNo.ForeColor = System.Drawing.Color.Black;
            this.txtcontactNo.Location = new System.Drawing.Point(129, 147);
            this.txtcontactNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtcontactNo.Name = "txtcontactNo";
            this.txtcontactNo.Size = new System.Drawing.Size(264, 25);
            this.txtcontactNo.TabIndex = 5;
            this.txtcontactNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcontactNo_KeyDown);
            this.txtcontactNo.Leave += new System.EventHandler(this.txtcontactNo_Leave);
            // 
            // txtCompanyName
            // 
            this.txtCompanyName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtCompanyName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCompanyName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyName.ForeColor = System.Drawing.Color.Black;
            this.txtCompanyName.Location = new System.Drawing.Point(129, 31);
            this.txtCompanyName.Margin = new System.Windows.Forms.Padding(2);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(264, 25);
            this.txtCompanyName.TabIndex = 2;
            this.txtCompanyName.TextChanged += new System.EventHandler(this.txtCompanyName_TextChanged);
            this.txtCompanyName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCompanyName_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(2, 147);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 25);
            this.label4.TabIndex = 84;
            this.label4.Text = "Contact No:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(395, 29);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 29);
            this.label7.TabIndex = 85;
            this.label7.Text = "*";
            // 
            // txtCompanyAddress
            // 
            this.txtCompanyAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCompanyAddress.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompanyAddress.ForeColor = System.Drawing.Color.Black;
            this.txtCompanyAddress.Location = new System.Drawing.Point(129, 89);
            this.txtCompanyAddress.Margin = new System.Windows.Forms.Padding(2);
            this.txtCompanyAddress.Multiline = true;
            this.txtCompanyAddress.Name = "txtCompanyAddress";
            this.tblltInfo.SetRowSpan(this.txtCompanyAddress, 2);
            this.txtCompanyAddress.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtCompanyAddress.Size = new System.Drawing.Size(264, 54);
            this.txtCompanyAddress.TabIndex = 4;
            this.txtCompanyAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCompanyAddress_KeyDown);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.sgrpBox1, 0, 1);
            this.tblltMain.Controls.Add(this.sgrpBox2, 0, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblltMain.Size = new System.Drawing.Size(434, 542);
            this.tblltMain.TabIndex = 0;
            // 
            // frmManufactureCompanyInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(434, 542);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmManufactureCompanyInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manufacture Company Information";
            this.Load += new System.EventHandler(this.frmManufactureCompanyInfo_Load);
            this.sgrpBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Gvcompanynfo)).EndInit();
            this.sgrpBox1.ResumeLayout(false);
            this.tblltInfo.ResumeLayout(false);
            this.tblltInfo.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.tblltMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox sgrpBox2;
        private System.Windows.Forms.GroupBox sgrpBox1;
        //private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        //private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        //private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        //private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        //private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtMarathiName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label2;
        private RachitControls.NumericTextBox txtStockAlert;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCompanyID;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtcontactNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCompanyAddress;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.TableLayoutPanel tblltInfo;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView Gvcompanynfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompanyId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContactNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockAlert;
        private System.Windows.Forms.DataGridViewTextBoxColumn MarathiName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompId;
        //private System.Windows.Forms.DataGridViewTextBoxColumn column6;
    }
}