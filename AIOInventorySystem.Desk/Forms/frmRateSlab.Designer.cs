namespace AIOInventorySystem.Desk.Forms
{
    partial class frmRateSlab
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.dtgRateSlab = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtyFrom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QtyTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DtlId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remove = new System.Windows.Forms.DataGridViewImageColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSlab = new System.Windows.Forms.TextBox();
            this.cmbSlabDescription = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.tblltSearch = new System.Windows.Forms.TableLayoutPanel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.tblltInfo = new System.Windows.Forms.TableLayoutPanel();
            this.tblltRow1 = new System.Windows.Forms.TableLayoutPanel();
            this.tblltRow2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRateSlab)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.grpList.SuspendLayout();
            this.tblltSearch.SuspendLayout();
            this.grpInfo.SuspendLayout();
            this.tblltInfo.SuspendLayout();
            this.tblltRow1.SuspendLayout();
            this.tblltRow2.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "From:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(131, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "To:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(243, 2);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 26);
            this.label4.TabIndex = 3;
            this.label4.Text = "Rate:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID
            // 
            this.txtID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(36, 2);
            this.txtID.Margin = new System.Windows.Forms.Padding(2);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(82, 25);
            this.txtID.TabIndex = 0;
            this.txtID.TabStop = false;
            // 
            // txtFrom
            // 
            this.txtFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFrom.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrom.Location = new System.Drawing.Point(62, 2);
            this.txtFrom.Margin = new System.Windows.Forms.Padding(2);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(65, 25);
            this.txtFrom.TabIndex = 2;
            this.txtFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFrom_KeyDown);
            this.txtFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFrom_KeyPress);
            // 
            // txtTo
            // 
            this.txtTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTo.Location = new System.Drawing.Point(174, 2);
            this.txtTo.Margin = new System.Windows.Forms.Padding(2);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(65, 25);
            this.txtTo.TabIndex = 3;
            this.txtTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTo_KeyDown);
            this.txtTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTo_KeyPress);
            this.txtTo.Leave += new System.EventHandler(this.txtTo_Leave);
            // 
            // txtRate
            // 
            this.txtRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRate.Location = new System.Drawing.Point(299, 2);
            this.txtRate.Margin = new System.Windows.Forms.Padding(2);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(65, 25);
            this.txtRate.TabIndex = 4;
            this.txtRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRate_KeyDown);
            this.txtRate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRate_KeyPress);
            // 
            // dtgRateSlab
            // 
            this.dtgRateSlab.AllowUserToAddRows = false;
            this.dtgRateSlab.BackgroundColor = System.Drawing.Color.White;
            this.dtgRateSlab.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgRateSlab.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.QtyFrom,
            this.QtyTo,
            this.Rate,
            this.DtlId,
            this.Remove});
            this.tblltSearch.SetColumnSpan(this.dtgRateSlab, 3);
            this.dtgRateSlab.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dtgRateSlab.Location = new System.Drawing.Point(2, 30);
            this.dtgRateSlab.Margin = new System.Windows.Forms.Padding(2);
            this.dtgRateSlab.Name = "dtgRateSlab";
            this.dtgRateSlab.RowHeadersVisible = false;
            this.dtgRateSlab.RowHeadersWidth = 15;
            this.dtgRateSlab.Size = new System.Drawing.Size(428, 249);
            this.dtgRateSlab.TabIndex = 8;
            this.dtgRateSlab.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgRateSlab_CellClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "ID";
            this.Id.Name = "Id";
            this.Id.Width = 45;
            // 
            // QtyFrom
            // 
            this.QtyFrom.HeaderText = "Quantity From";
            this.QtyFrom.Name = "QtyFrom";
            this.QtyFrom.Width = 125;
            // 
            // QtyTo
            // 
            this.QtyTo.HeaderText = "Quantity To";
            this.QtyTo.Name = "QtyTo";
            this.QtyTo.Width = 115;
            // 
            // Rate
            // 
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.Width = 70;
            // 
            // DtlId
            // 
            this.DtlId.HeaderText = "DtlId";
            this.DtlId.Name = "DtlId";
            this.DtlId.Visible = false;
            // 
            // Remove
            // 
            this.Remove.HeaderText = "Remove";
            this.Remove.Image = global::AIOInventorySystem.Desk.Properties.Resources.Remove;
            this.Remove.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Remove.Name = "Remove";
            this.Remove.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Remove.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Remove.Width = 80;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(122, 2);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "Slab Description:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSlab
            // 
            this.txtSlab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSlab.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSlab.Location = new System.Drawing.Point(251, 2);
            this.txtSlab.Margin = new System.Windows.Forms.Padding(2);
            this.txtSlab.Name = "txtSlab";
            this.txtSlab.Size = new System.Drawing.Size(179, 25);
            this.txtSlab.TabIndex = 1;
            this.txtSlab.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSlab_KeyDown);
            // 
            // cmbSlabDescription
            // 
            this.cmbSlabDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSlabDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSlabDescription.FormattingEnabled = true;
            this.cmbSlabDescription.Location = new System.Drawing.Point(131, 2);
            this.cmbSlabDescription.Margin = new System.Windows.Forms.Padding(2);
            this.cmbSlabDescription.Name = "cmbSlabDescription";
            this.cmbSlabDescription.Size = new System.Drawing.Size(212, 25);
            this.cmbSlabDescription.TabIndex = 6;
            this.cmbSlabDescription.SelectedIndexChanged += new System.EventHandler(this.cmbSlabDescription_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(2, 2);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 24);
            this.label6.TabIndex = 16;
            this.label6.Text = "Slab Description:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.grpList, 0, 2);
            this.tblltMain.Controls.Add(this.grpInfo, 0, 1);
            this.tblltMain.Controls.Add(this.tblltButtons, 0, 3);
            this.tblltMain.Controls.Add(this.label7, 0, 0);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 66F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.Size = new System.Drawing.Size(444, 472);
            this.tblltMain.TabIndex = 0;
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.tblltSearch);
            this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpList.Location = new System.Drawing.Point(3, 129);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(438, 305);
            this.grpList.TabIndex = 6;
            this.grpList.TabStop = false;
            this.grpList.Text = "List";
            // 
            // tblltSearch
            // 
            this.tblltSearch.ColumnCount = 3;
            this.tblltSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltSearch.Controls.Add(this.btnSearch, 2, 0);
            this.tblltSearch.Controls.Add(this.label6, 0, 0);
            this.tblltSearch.Controls.Add(this.cmbSlabDescription, 1, 0);
            this.tblltSearch.Controls.Add(this.dtgRateSlab, 0, 1);
            this.tblltSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltSearch.Location = new System.Drawing.Point(3, 21);
            this.tblltSearch.Margin = new System.Windows.Forms.Padding(0);
            this.tblltSearch.Name = "tblltSearch";
            this.tblltSearch.RowCount = 2;
            this.tblltSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tblltSearch.Size = new System.Drawing.Size(432, 281);
            this.tblltSearch.TabIndex = 6;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(347, 1);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(83, 26);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.tblltInfo);
            this.grpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInfo.Location = new System.Drawing.Point(3, 40);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(438, 83);
            this.grpInfo.TabIndex = 0;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Info";
            // 
            // tblltInfo
            // 
            this.tblltInfo.ColumnCount = 1;
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltInfo.Controls.Add(this.tblltRow1, 0, 0);
            this.tblltInfo.Controls.Add(this.tblltRow2, 0, 1);
            this.tblltInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltInfo.Location = new System.Drawing.Point(3, 21);
            this.tblltInfo.Margin = new System.Windows.Forms.Padding(1);
            this.tblltInfo.Name = "tblltInfo";
            this.tblltInfo.RowCount = 2;
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltInfo.Size = new System.Drawing.Size(432, 59);
            this.tblltInfo.TabIndex = 0;
            // 
            // tblltRow1
            // 
            this.tblltRow1.ColumnCount = 4;
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.tblltRow1.Controls.Add(this.label1, 0, 0);
            this.tblltRow1.Controls.Add(this.label5, 2, 0);
            this.tblltRow1.Controls.Add(this.txtID, 1, 0);
            this.tblltRow1.Controls.Add(this.txtSlab, 3, 0);
            this.tblltRow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow1.Location = new System.Drawing.Point(0, 0);
            this.tblltRow1.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow1.Name = "tblltRow1";
            this.tblltRow1.RowCount = 1;
            this.tblltRow1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow1.Size = new System.Drawing.Size(432, 29);
            this.tblltRow1.TabIndex = 0;
            // 
            // tblltRow2
            // 
            this.tblltRow2.ColumnCount = 7;
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltRow2.Controls.Add(this.btnAdd, 6, 0);
            this.tblltRow2.Controls.Add(this.txtRate, 5, 0);
            this.tblltRow2.Controls.Add(this.label2, 0, 0);
            this.tblltRow2.Controls.Add(this.txtTo, 3, 0);
            this.tblltRow2.Controls.Add(this.label4, 4, 0);
            this.tblltRow2.Controls.Add(this.txtFrom, 1, 0);
            this.tblltRow2.Controls.Add(this.label3, 2, 0);
            this.tblltRow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow2.Location = new System.Drawing.Point(0, 29);
            this.tblltRow2.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow2.Name = "tblltRow2";
            this.tblltRow2.RowCount = 1;
            this.tblltRow2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow2.Size = new System.Drawing.Size(432, 30);
            this.tblltRow2.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(368, 1);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(62, 28);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnAdd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnAdd_KeyDown);
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 7;
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltButtons.Controls.Add(this.btnNew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnSave, 2, 0);
            this.tblltButtons.Controls.Add(this.btnDelete, 3, 0);
            this.tblltButtons.Controls.Add(this.btnCancel, 4, 0);
            this.tblltButtons.Controls.Add(this.btnClose, 5, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 437);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(444, 35);
            this.tblltButtons.TabIndex = 9;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(46, 2);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(67, 31);
            this.btnNew.TabIndex = 10;
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
            this.btnSave.Location = new System.Drawing.Point(117, 2);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(67, 31);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Add";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(188, 2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(67, 31);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(259, 2);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(67, 31);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(330, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(67, 31);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(444, 37);
            this.label7.TabIndex = 14;
            this.label7.Text = "Rate Slab";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmRateSlab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(444, 472);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmRateSlab";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rate Slab";
            ((System.ComponentModel.ISupportInitialize)(this.dtgRateSlab)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.grpList.ResumeLayout(false);
            this.tblltSearch.ResumeLayout(false);
            this.tblltSearch.PerformLayout();
            this.grpInfo.ResumeLayout(false);
            this.tblltInfo.ResumeLayout(false);
            this.tblltRow1.ResumeLayout(false);
            this.tblltRow1.PerformLayout();
            this.tblltRow2.ResumeLayout(false);
            this.tblltRow2.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.TextBox txtRate;
        private System.Windows.Forms.DataGridView dtgRateSlab;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSlab;
        private System.Windows.Forms.ComboBox cmbSlabDescription;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel tblltRow1;
        private System.Windows.Forms.TableLayoutPanel tblltRow2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TableLayoutPanel tblltSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtyFrom;
        private System.Windows.Forms.DataGridViewTextBoxColumn QtyTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DtlId;
        private System.Windows.Forms.DataGridViewImageColumn Remove;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.TableLayoutPanel tblltInfo;
    }
}