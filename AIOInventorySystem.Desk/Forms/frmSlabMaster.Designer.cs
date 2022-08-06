namespace AIOInventorySystem.Desk.Forms
{
    partial class frmSlabMaster
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
            this.label5 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtQtySlab = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.grpQtySlab = new System.Windows.Forms.GroupBox();
            this.tblltFixedSize = new System.Windows.Forms.TableLayoutPanel();
            this.GVSlabmaster = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QuantitySlab = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SlabUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SlabId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.cmbSlabGroup = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtgSizeRange = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbDescription = new System.Windows.Forms.ComboBox();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.grpDynamicAdd = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tblltSlabGroup = new System.Windows.Forms.TableLayoutPanel();
            this.rdbtnDynamicSize = new System.Windows.Forms.RadioButton();
            this.rdbtnFixedSize = new System.Windows.Forms.RadioButton();
            this.grpQtySlab.SuspendLayout();
            this.tblltFixedSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVSlabmaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSizeRange)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.grpDynamicAdd.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.tblltSlabGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label5, 4);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(784, 41);
            this.label5.TabIndex = 16;
            this.label5.Text = "Quantity Slab";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDescription
            // 
            this.tblltFixedSize.SetColumnSpan(this.txtDescription, 3);
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Location = new System.Drawing.Point(98, 3);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(279, 23);
            this.txtDescription.TabIndex = 1;
            this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescription_KeyDown);
            this.txtDescription.Leave += new System.EventHandler(this.txtDescription_Leave);
            // 
            // txtQtySlab
            // 
            this.txtQtySlab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQtySlab.Location = new System.Drawing.Point(98, 31);
            this.txtQtySlab.Name = "txtQtySlab";
            this.txtQtySlab.Size = new System.Drawing.Size(89, 23);
            this.txtQtySlab.TabIndex = 2;
            this.txtQtySlab.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQtySlab_KeyDown);
            this.txtQtySlab.Leave += new System.EventHandler(this.txtQtySlab_Leave);
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(199, 44);
            this.txtId.Name = "txtId";
            this.txtId.ReadOnly = true;
            this.txtId.Size = new System.Drawing.Size(79, 23);
            this.txtId.TabIndex = 40;
            // 
            // grpQtySlab
            // 
            this.tblltMain.SetColumnSpan(this.grpQtySlab, 2);
            this.grpQtySlab.Controls.Add(this.tblltFixedSize);
            this.grpQtySlab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpQtySlab.Location = new System.Drawing.Point(3, 72);
            this.grpQtySlab.Name = "grpQtySlab";
            this.grpQtySlab.Size = new System.Drawing.Size(386, 303);
            this.grpQtySlab.TabIndex = 17;
            this.grpQtySlab.TabStop = false;
            this.grpQtySlab.Text = "All Information";
            // 
            // tblltFixedSize
            // 
            this.tblltFixedSize.ColumnCount = 4;
            this.tblltFixedSize.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltFixedSize.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltFixedSize.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltFixedSize.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltFixedSize.Controls.Add(this.GVSlabmaster, 0, 2);
            this.tblltFixedSize.Controls.Add(this.label4, 0, 0);
            this.tblltFixedSize.Controls.Add(this.txtDescription, 1, 0);
            this.tblltFixedSize.Controls.Add(this.label7, 3, 1);
            this.tblltFixedSize.Controls.Add(this.label9, 0, 1);
            this.tblltFixedSize.Controls.Add(this.cmbUnit, 2, 1);
            this.tblltFixedSize.Controls.Add(this.txtQtySlab, 1, 1);
            this.tblltFixedSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltFixedSize.Location = new System.Drawing.Point(3, 19);
            this.tblltFixedSize.Name = "tblltFixedSize";
            this.tblltFixedSize.RowCount = 3;
            this.tblltFixedSize.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltFixedSize.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltFixedSize.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tblltFixedSize.Size = new System.Drawing.Size(380, 281);
            this.tblltFixedSize.TabIndex = 42;
            // 
            // GVSlabmaster
            // 
            this.GVSlabmaster.AllowUserToAddRows = false;
            this.GVSlabmaster.AllowUserToDeleteRows = false;
            this.GVSlabmaster.BackgroundColor = System.Drawing.Color.White;
            this.GVSlabmaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVSlabmaster.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Description,
            this.QuantitySlab,
            this.SlabUnit,
            this.SlabId});
            this.tblltFixedSize.SetColumnSpan(this.GVSlabmaster, 4);
            this.GVSlabmaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GVSlabmaster.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GVSlabmaster.Location = new System.Drawing.Point(3, 59);
            this.GVSlabmaster.Name = "GVSlabmaster";
            this.GVSlabmaster.ReadOnly = true;
            this.GVSlabmaster.RowHeadersVisible = false;
            this.GVSlabmaster.Size = new System.Drawing.Size(374, 219);
            this.GVSlabmaster.TabIndex = 42;
            this.GVSlabmaster.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GVSlabmaster_CellClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 50;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 120;
            // 
            // QuantitySlab
            // 
            this.QuantitySlab.HeaderText = "QunatitySlab";
            this.QuantitySlab.Name = "QuantitySlab";
            this.QuantitySlab.ReadOnly = true;
            // 
            // SlabUnit
            // 
            this.SlabUnit.HeaderText = "SlabUnit";
            this.SlabUnit.Name = "SlabUnit";
            this.SlabUnit.ReadOnly = true;
            // 
            // SlabId
            // 
            this.SlabId.HeaderText = "SlabId";
            this.SlabId.Name = "SlabId";
            this.SlabId.ReadOnly = true;
            this.SlabId.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1, 1);
            this.label4.Margin = new System.Windows.Forms.Padding(1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 26);
            this.label4.TabIndex = 14;
            this.label4.Text = "Description:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(288, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 28);
            this.label7.TabIndex = 22;
            this.label7.Text = "*";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1, 29);
            this.label9.Margin = new System.Windows.Forms.Padding(1);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 26);
            this.label9.TabIndex = 10;
            this.label9.Text = "Quantity Slab:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbUnit
            // 
            this.cmbUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Items.AddRange(new object[] {
            "Inch",
            "Feet"});
            this.cmbUnit.Location = new System.Drawing.Point(193, 31);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(89, 23);
            this.cmbUnit.TabIndex = 3;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
            this.cmbUnit.Leave += new System.EventHandler(this.cmbUnit_Leave);
            // 
            // cmbSlabGroup
            // 
            this.cmbSlabGroup.FormattingEnabled = true;
            this.cmbSlabGroup.Items.AddRange(new object[] {
            "Fixed Size",
            "Dynamic Size"});
            this.cmbSlabGroup.Location = new System.Drawing.Point(654, 3);
            this.cmbSlabGroup.Name = "cmbSlabGroup";
            this.cmbSlabGroup.Size = new System.Drawing.Size(81, 23);
            this.cmbSlabGroup.TabIndex = 0;
            this.cmbSlabGroup.Visible = false;
            this.cmbSlabGroup.SelectedIndexChanged += new System.EventHandler(this.cmbSlabGroup_SelectedIndexChanged);
            this.cmbSlabGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbSlabGroup_KeyDown);
            this.cmbSlabGroup.Leave += new System.EventHandler(this.cmbSlabGroup_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1, 42);
            this.label8.Margin = new System.Windows.Forms.Padding(1);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(194, 26);
            this.label8.TabIndex = 9;
            this.label8.Text = "ID:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1, 1);
            this.label10.Margin = new System.Windows.Forms.Padding(1);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 26);
            this.label10.TabIndex = 13;
            this.label10.Text = "Description:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(393, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 26);
            this.label2.TabIndex = 24;
            this.label2.Text = "Slab Group:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtgSizeRange
            // 
            this.dtgSizeRange.AllowUserToAddRows = false;
            this.dtgSizeRange.AllowUserToDeleteRows = false;
            this.dtgSizeRange.BackgroundColor = System.Drawing.Color.White;
            this.dtgSizeRange.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgSizeRange.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.tableLayoutPanel2.SetColumnSpan(this.dtgSizeRange, 5);
            this.dtgSizeRange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgSizeRange.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgSizeRange.Location = new System.Drawing.Point(3, 59);
            this.dtgSizeRange.Name = "dtgSizeRange";
            this.dtgSizeRange.ReadOnly = true;
            this.dtgSizeRange.RowHeadersVisible = false;
            this.dtgSizeRange.Size = new System.Drawing.Size(374, 219);
            this.dtgSizeRange.TabIndex = 30;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "From Size";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "To Size";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "DtlID";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Visible = false;
            // 
            // txtTo
            // 
            this.txtTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTo.Location = new System.Drawing.Point(231, 31);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(70, 23);
            this.txtTo.TabIndex = 5;
            this.txtTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTo_KeyDown);
            this.txtTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTo_KeyPress);
            this.txtTo.Leave += new System.EventHandler(this.txtTo_Leave);
            // 
            // txtFrom
            // 
            this.txtFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFrom.Location = new System.Drawing.Point(79, 31);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(70, 23);
            this.txtFrom.TabIndex = 4;
            this.txtFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFrom_KeyDown);
            this.txtFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFrom_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(153, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 26);
            this.label3.TabIndex = 26;
            this.label3.Text = "To";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 26);
            this.label1.TabIndex = 25;
            this.label1.Text = "Range From:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbDescription
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.cmbDescription, 3);
            this.cmbDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbDescription.FormattingEnabled = true;
            this.cmbDescription.Location = new System.Drawing.Point(79, 3);
            this.cmbDescription.Name = "cmbDescription";
            this.cmbDescription.Size = new System.Drawing.Size(222, 23);
            this.cmbDescription.TabIndex = 1;
            this.cmbDescription.SelectedIndexChanged += new System.EventHandler(this.cmbDescription_SelectedIndexChanged);
            this.cmbDescription.Leave += new System.EventHandler(this.cmbDescription_Leave);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 4;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblltMain.Controls.Add(this.grpDynamicAdd, 2, 2);
            this.tblltMain.Controls.Add(this.grpQtySlab, 0, 2);
            this.tblltMain.Controls.Add(this.tblltButtons, 0, 3);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.label8, 0, 1);
            this.tblltMain.Controls.Add(this.txtId, 1, 1);
            this.tblltMain.Controls.Add(this.label2, 2, 1);
            this.tblltMain.Controls.Add(this.tblltSlabGroup, 3, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.Size = new System.Drawing.Size(784, 412);
            this.tblltMain.TabIndex = 41;
            // 
            // grpDynamicAdd
            // 
            this.tblltMain.SetColumnSpan(this.grpDynamicAdd, 2);
            this.grpDynamicAdd.Controls.Add(this.tableLayoutPanel2);
            this.grpDynamicAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDynamicAdd.Enabled = false;
            this.grpDynamicAdd.Location = new System.Drawing.Point(395, 72);
            this.grpDynamicAdd.Name = "grpDynamicAdd";
            this.grpDynamicAdd.Size = new System.Drawing.Size(386, 303);
            this.grpDynamicAdd.TabIndex = 41;
            this.grpDynamicAdd.TabStop = false;
            this.grpDynamicAdd.Text = "Range";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 5;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.btnAdd, 4, 1);
            this.tableLayoutPanel2.Controls.Add(this.dtgSizeRange, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmbDescription, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtFrom, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.txtTo, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.label3, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(380, 281);
            this.tableLayoutPanel2.TabIndex = 42;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(305, 29);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(74, 26);
            this.btnAdd.TabIndex = 192;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 7;
            this.tblltMain.SetColumnSpan(this.tblltButtons, 4);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.78322F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.28671F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.28671F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.28671F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.28671F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.28671F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.78322F));
            this.tblltButtons.Controls.Add(this.btnNew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnSave, 2, 0);
            this.tblltButtons.Controls.Add(this.btnDelete, 3, 0);
            this.tblltButtons.Controls.Add(this.btnCancel, 4, 0);
            this.tblltButtons.Controls.Add(this.btnClose, 5, 0);
            this.tblltButtons.Controls.Add(this.cmbSlabGroup, 6, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 378);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(784, 34);
            this.tblltButtons.TabIndex = 65;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(133, 2);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(100, 30);
            this.btnNew.TabIndex = 185;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(237, 2);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 186;
            this.btnSave.Text = "Save";
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
            this.btnDelete.Location = new System.Drawing.Point(341, 2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 187;
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
            this.btnCancel.Location = new System.Drawing.Point(445, 2);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 188;
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
            this.btnClose.Location = new System.Drawing.Point(549, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 190;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tblltSlabGroup
            // 
            this.tblltSlabGroup.ColumnCount = 2;
            this.tblltSlabGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltSlabGroup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltSlabGroup.Controls.Add(this.rdbtnDynamicSize, 1, 0);
            this.tblltSlabGroup.Controls.Add(this.rdbtnFixedSize, 0, 0);
            this.tblltSlabGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltSlabGroup.Location = new System.Drawing.Point(509, 41);
            this.tblltSlabGroup.Margin = new System.Windows.Forms.Padding(0);
            this.tblltSlabGroup.Name = "tblltSlabGroup";
            this.tblltSlabGroup.RowCount = 1;
            this.tblltSlabGroup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltSlabGroup.Size = new System.Drawing.Size(275, 28);
            this.tblltSlabGroup.TabIndex = 66;
            // 
            // rdbtnDynamicSize
            // 
            this.rdbtnDynamicSize.AutoSize = true;
            this.rdbtnDynamicSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdbtnDynamicSize.Location = new System.Drawing.Point(140, 3);
            this.rdbtnDynamicSize.Name = "rdbtnDynamicSize";
            this.rdbtnDynamicSize.Size = new System.Drawing.Size(132, 22);
            this.rdbtnDynamicSize.TabIndex = 1;
            this.rdbtnDynamicSize.TabStop = true;
            this.rdbtnDynamicSize.Text = "Dynamic Size";
            this.rdbtnDynamicSize.UseVisualStyleBackColor = true;
            this.rdbtnDynamicSize.CheckedChanged += new System.EventHandler(this.rdbtnDynamicSize_CheckedChanged);
            // 
            // rdbtnFixedSize
            // 
            this.rdbtnFixedSize.AutoSize = true;
            this.rdbtnFixedSize.Checked = true;
            this.rdbtnFixedSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdbtnFixedSize.Location = new System.Drawing.Point(3, 3);
            this.rdbtnFixedSize.Name = "rdbtnFixedSize";
            this.rdbtnFixedSize.Size = new System.Drawing.Size(131, 22);
            this.rdbtnFixedSize.TabIndex = 0;
            this.rdbtnFixedSize.TabStop = true;
            this.rdbtnFixedSize.Text = "Fixed Size";
            this.rdbtnFixedSize.UseVisualStyleBackColor = true;
            this.rdbtnFixedSize.CheckedChanged += new System.EventHandler(this.rdbtnFixedSize_CheckedChanged);
            // 
            // frmSlabMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(784, 412);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmSlabMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quantity Slab";
            this.grpQtySlab.ResumeLayout(false);
            this.tblltFixedSize.ResumeLayout(false);
            this.tblltFixedSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVSlabmaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSizeRange)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.grpDynamicAdd.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.tblltSlabGroup.ResumeLayout(false);
            this.tblltSlabGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtQtySlab;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox grpQtySlab;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbSlabGroup;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dtgSizeRange;
        private System.Windows.Forms.ComboBox cmbDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox grpDynamicAdd;
        private System.Windows.Forms.DataGridView GVSlabmaster;
        private System.Windows.Forms.TableLayoutPanel tblltSlabGroup;
        private System.Windows.Forms.RadioButton rdbtnDynamicSize;
        private System.Windows.Forms.RadioButton rdbtnFixedSize;
        private System.Windows.Forms.TableLayoutPanel tblltFixedSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn QuantitySlab;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlabUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn SlabId;
    }
}