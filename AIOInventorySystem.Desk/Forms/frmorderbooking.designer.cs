namespace AIOInventorySystem.Desk.Forms
{
    partial class frmorderbooking
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gvProductRemStock = new System.Windows.Forms.DataGridView();
            this.ProductNameR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnCustForm = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnorderbookinglist = new System.Windows.Forms.Button();
            this.grpProductDetail = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.GvProductInfo = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductNameg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Company = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remove = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.cmbUnit = new System.Windows.Forms.ComboBox();
            this.chkcompanyname = new System.Windows.Forms.CheckBox();
            this.cmbcomanyname = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtProductname = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtQuantity = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpdelieverydate = new System.Windows.Forms.DateTimePicker();
            this.txtorderno = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtporderdate = new System.Windows.Forms.DateTimePicker();
            this.cmbcustomername = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtgvcode = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtprefixproduct = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.gvProductRemStock)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.grpProductDetail.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvProductInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvcode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gvProductRemStock
            // 
            this.gvProductRemStock.AllowUserToAddRows = false;
            this.gvProductRemStock.AllowUserToDeleteRows = false;
            this.gvProductRemStock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gvProductRemStock.BackgroundColor = System.Drawing.Color.White;
            this.gvProductRemStock.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvProductRemStock.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gvProductRemStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvProductRemStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProductNameR,
            this.RemQty,
            this.UnitR});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvProductRemStock.DefaultCellStyle = dataGridViewCellStyle2;
            this.gvProductRemStock.GridColor = System.Drawing.Color.WhiteSmoke;
            this.gvProductRemStock.Location = new System.Drawing.Point(226, 21);
            this.gvProductRemStock.Name = "gvProductRemStock";
            this.gvProductRemStock.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.NullValue = null;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvProductRemStock.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gvProductRemStock.RowHeadersWidth = 15;
            dataGridViewCellStyle4.NullValue = null;
            this.gvProductRemStock.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvProductRemStock.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvProductRemStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gvProductRemStock.Size = new System.Drawing.Size(301, 62);
            this.gvProductRemStock.TabIndex = 161;
            this.gvProductRemStock.Visible = false;
            // 
            // ProductNameR
            // 
            this.ProductNameR.HeaderText = "ProductName";
            this.ProductNameR.Name = "ProductNameR";
            this.ProductNameR.ReadOnly = true;
            this.ProductNameR.Width = 145;
            // 
            // RemQty
            // 
            this.RemQty.HeaderText = "RemQty";
            this.RemQty.Name = "RemQty";
            this.RemQty.ReadOnly = true;
            this.RemQty.Width = 65;
            // 
            // UnitR
            // 
            this.UnitR.HeaderText = "Unit";
            this.UnitR.Name = "UnitR";
            this.UnitR.ReadOnly = true;
            this.UnitR.Width = 70;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 9;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.5F));
            this.tblltMain.Controls.Add(this.btnCustForm, 6, 1);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.tblltButtons, 0, 3);
            this.tblltMain.Controls.Add(this.grpProductDetail, 0, 2);
            this.tblltMain.Controls.Add(this.label1, 0, 1);
            this.tblltMain.Controls.Add(this.dtpdelieverydate, 8, 1);
            this.tblltMain.Controls.Add(this.txtorderno, 1, 1);
            this.tblltMain.Controls.Add(this.label2, 7, 1);
            this.tblltMain.Controls.Add(this.label4, 2, 1);
            this.tblltMain.Controls.Add(this.dtporderdate, 3, 1);
            this.tblltMain.Controls.Add(this.cmbcustomername, 5, 1);
            this.tblltMain.Controls.Add(this.label3, 4, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.Size = new System.Drawing.Size(904, 462);
            this.tblltMain.TabIndex = 0;
            // 
            // btnCustForm
            // 
            this.btnCustForm.BackgroundImage = global::AIOInventorySystem.Desk.Properties.Resources.newbutton;
            this.btnCustForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCustForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCustForm.FlatAppearance.BorderSize = 0;
            this.btnCustForm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCustForm.Location = new System.Drawing.Point(663, 38);
            this.btnCustForm.Margin = new System.Windows.Forms.Padding(2);
            this.btnCustForm.Name = "btnCustForm";
            this.btnCustForm.Size = new System.Drawing.Size(32, 23);
            this.btnCustForm.TabIndex = 4;
            this.btnCustForm.UseVisualStyleBackColor = true;
            this.btnCustForm.Click += new System.EventHandler(this.btnCustForm_Click);
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
            this.label5.Size = new System.Drawing.Size(904, 36);
            this.label5.TabIndex = 37;
            this.label5.Text = "Order Booking";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 8;
            this.tblltMain.SetColumnSpan(this.tblltButtons, 9);
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
            this.tblltButtons.Controls.Add(this.btnDelete, 3, 0);
            this.tblltButtons.Controls.Add(this.btnorderbookinglist, 6, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 427);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(904, 35);
            this.tblltButtons.TabIndex = 14;
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(115, 2);
            this.btnnew.Margin = new System.Windows.Forms.Padding(2);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(109, 31);
            this.btnnew.TabIndex = 15;
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
            this.btnsave.Location = new System.Drawing.Point(228, 2);
            this.btnsave.Margin = new System.Windows.Forms.Padding(2);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(109, 31);
            this.btnsave.TabIndex = 14;
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
            this.btnprint.Location = new System.Drawing.Point(567, 2);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(109, 31);
            this.btnprint.TabIndex = 18;
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
            this.btnClose.Location = new System.Drawing.Point(454, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(109, 31);
            this.btnClose.TabIndex = 17;
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
            this.btnDelete.Location = new System.Drawing.Point(341, 2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(109, 31);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnorderbookinglist
            // 
            this.btnorderbookinglist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnorderbookinglist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnorderbookinglist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnorderbookinglist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnorderbookinglist.ForeColor = System.Drawing.Color.White;
            this.btnorderbookinglist.Location = new System.Drawing.Point(680, 2);
            this.btnorderbookinglist.Margin = new System.Windows.Forms.Padding(2);
            this.btnorderbookinglist.Name = "btnorderbookinglist";
            this.btnorderbookinglist.Size = new System.Drawing.Size(109, 31);
            this.btnorderbookinglist.TabIndex = 19;
            this.btnorderbookinglist.Text = "Booking List";
            this.btnorderbookinglist.UseVisualStyleBackColor = false;
            this.btnorderbookinglist.Click += new System.EventHandler(this.btnorderbookinglist_Click);
            // 
            // grpProductDetail
            // 
            this.tblltMain.SetColumnSpan(this.grpProductDetail, 9);
            this.grpProductDetail.Controls.Add(this.tableLayoutPanel3);
            this.grpProductDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpProductDetail.Location = new System.Drawing.Point(1, 64);
            this.grpProductDetail.Margin = new System.Windows.Forms.Padding(1);
            this.grpProductDetail.Name = "grpProductDetail";
            this.grpProductDetail.Size = new System.Drawing.Size(902, 362);
            this.grpProductDetail.TabIndex = 6;
            this.grpProductDetail.TabStop = false;
            this.grpProductDetail.Text = "Product Detail";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 11;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.5F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tableLayoutPanel3.Controls.Add(this.GvProductInfo, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btnAdd, 10, 0);
            this.tableLayoutPanel3.Controls.Add(this.label23, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.cmbUnit, 6, 0);
            this.tableLayoutPanel3.Controls.Add(this.chkcompanyname, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.cmbcomanyname, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label8, 9, 0);
            this.tableLayoutPanel3.Controls.Add(this.label11, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label10, 7, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtProductname, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label6, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.txtQuantity, 8, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.ForeColor = System.Drawing.Color.Black;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 21);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(896, 338);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // GvProductInfo
            // 
            this.GvProductInfo.AllowUserToAddRows = false;
            this.GvProductInfo.AllowUserToDeleteRows = false;
            this.GvProductInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvProductInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvProductInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvProductInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
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
            this.tableLayoutPanel3.SetColumnSpan(this.GvProductInfo, 11);
            this.GvProductInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvProductInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvProductInfo.Location = new System.Drawing.Point(2, 35);
            this.GvProductInfo.Margin = new System.Windows.Forms.Padding(2);
            this.GvProductInfo.Name = "GvProductInfo";
            this.GvProductInfo.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvProductInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.GvProductInfo.RowHeadersWidth = 20;
            this.GvProductInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvProductInfo.Size = new System.Drawing.Size(892, 301);
            this.GvProductInfo.TabIndex = 13;
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
            this.Remove.Image = global::AIOInventorySystem.Desk.Properties.Resources.Remove;
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
            this.btnAdd.Location = new System.Drawing.Point(847, 2);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(47, 29);
            this.btnAdd.TabIndex = 12;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.ForeColor = System.Drawing.Color.Black;
            this.label23.Location = new System.Drawing.Point(532, 2);
            this.label23.Margin = new System.Windows.Forms.Padding(2);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(67, 29);
            this.label23.TabIndex = 39;
            this.label23.Text = "Sale Unit:";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbUnit
            // 
            this.cmbUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUnit.ForeColor = System.Drawing.Color.Black;
            this.cmbUnit.FormattingEnabled = true;
            this.cmbUnit.Location = new System.Drawing.Point(603, 2);
            this.cmbUnit.Margin = new System.Windows.Forms.Padding(2);
            this.cmbUnit.Name = "cmbUnit";
            this.cmbUnit.Size = new System.Drawing.Size(94, 25);
            this.cmbUnit.TabIndex = 10;
            this.cmbUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUnit_KeyDown);
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
            this.chkcompanyname.Size = new System.Drawing.Size(13, 29);
            this.chkcompanyname.TabIndex = 7;
            this.chkcompanyname.Text = "Mfg. Comp";
            this.chkcompanyname.UseVisualStyleBackColor = true;
            this.chkcompanyname.Visible = false;
            // 
            // cmbcomanyname
            // 
            this.cmbcomanyname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcomanyname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcomanyname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcomanyname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcomanyname.ForeColor = System.Drawing.Color.Black;
            this.cmbcomanyname.FormattingEnabled = true;
            this.cmbcomanyname.Location = new System.Drawing.Point(19, 2);
            this.cmbcomanyname.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcomanyname.Name = "cmbcomanyname";
            this.cmbcomanyname.Size = new System.Drawing.Size(94, 25);
            this.cmbcomanyname.TabIndex = 8;
            this.cmbcomanyname.SelectedIndexChanged += new System.EventHandler(this.cmbcomanyname_SelectedIndexChanged);
            this.cmbcomanyname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcomanyname_KeyDown);
            this.cmbcomanyname.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cmbcomanyname_MouseClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(823, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 33);
            this.label8.TabIndex = 45;
            this.label8.Text = "*";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(117, 2);
            this.label11.Margin = new System.Windows.Forms.Padding(2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(103, 29);
            this.label11.TabIndex = 35;
            this.label11.Text = "Product Name:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(701, 2);
            this.label10.Margin = new System.Windows.Forms.Padding(2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 29);
            this.label10.TabIndex = 36;
            this.label10.Text = "Quantity:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProductname
            // 
            this.txtProductname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtProductname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtProductname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductname.Location = new System.Drawing.Point(224, 2);
            this.txtProductname.Margin = new System.Windows.Forms.Padding(2);
            this.txtProductname.Name = "txtProductname";
            this.txtProductname.Size = new System.Drawing.Size(282, 25);
            this.txtProductname.TabIndex = 9;
            this.txtProductname.TextChanged += new System.EventHandler(this.txtProductname_TextChanged);
            this.txtProductname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductname_KeyDown);
            this.txtProductname.Leave += new System.EventHandler(this.txtProductname_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(508, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 33);
            this.label6.TabIndex = 44;
            this.label6.Text = "*";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuantity.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(772, 2);
            this.txtQuantity.Margin = new System.Windows.Forms.Padding(2);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(49, 25);
            this.txtQuantity.TabIndex = 11;
            this.txtQuantity.Text = "0";
            this.txtQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuantity_KeyDown);
            this.txtQuantity.Leave += new System.EventHandler(this.txtQuantity_Leave);
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
            this.label1.Size = new System.Drawing.Size(77, 23);
            this.label1.TabIndex = 40;
            this.label1.Text = "O. B. No:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpdelieverydate
            // 
            this.dtpdelieverydate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpdelieverydate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpdelieverydate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpdelieverydate.Location = new System.Drawing.Point(807, 38);
            this.dtpdelieverydate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpdelieverydate.Name = "dtpdelieverydate";
            this.dtpdelieverydate.Size = new System.Drawing.Size(95, 25);
            this.dtpdelieverydate.TabIndex = 5;
            this.dtpdelieverydate.ValueChanged += new System.EventHandler(this.dtpdelieverydate_ValueChanged);
            this.dtpdelieverydate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpdelieverydate_KeyDown);
            // 
            // txtorderno
            // 
            this.txtorderno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtorderno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtorderno.ForeColor = System.Drawing.Color.Black;
            this.txtorderno.Location = new System.Drawing.Point(83, 38);
            this.txtorderno.Margin = new System.Windows.Forms.Padding(2);
            this.txtorderno.Name = "txtorderno";
            this.txtorderno.ReadOnly = true;
            this.txtorderno.Size = new System.Drawing.Size(77, 25);
            this.txtorderno.TabIndex = 1;
            this.txtorderno.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(699, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 23);
            this.label2.TabIndex = 49;
            this.label2.Text = "Delivery Date:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(164, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 23);
            this.label4.TabIndex = 41;
            this.label4.Text = "O. B. Date:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtporderdate
            // 
            this.dtporderdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtporderdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtporderdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtporderdate.Location = new System.Drawing.Point(245, 38);
            this.dtporderdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtporderdate.Name = "dtporderdate";
            this.dtporderdate.Size = new System.Drawing.Size(90, 25);
            this.dtporderdate.TabIndex = 2;
            // 
            // cmbcustomername
            // 
            this.cmbcustomername.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcustomername.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcustomername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcustomername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustomername.FormattingEnabled = true;
            this.cmbcustomername.Location = new System.Drawing.Point(465, 38);
            this.cmbcustomername.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustomername.Name = "cmbcustomername";
            this.cmbcustomername.Size = new System.Drawing.Size(194, 25);
            this.cmbcustomername.TabIndex = 3;
            this.cmbcustomername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcustomername_KeyDown);
            this.cmbcustomername.Leave += new System.EventHandler(this.cmbcustomername_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(339, 38);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 23);
            this.label3.TabIndex = 42;
            this.label3.Text = "Customer Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtgvcode
            // 
            this.dtgvcode.AllowUserToAddRows = false;
            this.dtgvcode.AllowUserToDeleteRows = false;
            this.dtgvcode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgvcode.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvcode.BackgroundColor = System.Drawing.Color.White;
            this.dtgvcode.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvcode.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dtgvcode.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvcode.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvcode.DefaultCellStyle = dataGridViewCellStyle8;
            this.dtgvcode.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgvcode.Location = new System.Drawing.Point(703, 119);
            this.dtgvcode.Margin = new System.Windows.Forms.Padding(2);
            this.dtgvcode.Name = "dtgvcode";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.NullValue = null;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvcode.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dtgvcode.RowHeadersWidth = 10;
            dataGridViewCellStyle10.NullValue = null;
            this.dtgvcode.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dtgvcode.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgvcode.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dtgvcode.Size = new System.Drawing.Size(153, 125);
            this.dtgvcode.TabIndex = 12;
            this.dtgvcode.Visible = false;
            this.dtgvcode.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvcode_CellEndEdit);
            this.dtgvcode.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dtgvcode_EditingControlShowing);
            this.dtgvcode.Leave += new System.EventHandler(this.dtgvcode_Leave);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Code";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // txtprefixproduct
            // 
            this.txtprefixproduct.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtprefixproduct.ForeColor = System.Drawing.Color.Red;
            this.txtprefixproduct.FormattingEnabled = true;
            this.txtprefixproduct.ItemHeight = 20;
            this.txtprefixproduct.Location = new System.Drawing.Point(7, 117);
            this.txtprefixproduct.Name = "txtprefixproduct";
            this.txtprefixproduct.Size = new System.Drawing.Size(207, 244);
            this.txtprefixproduct.TabIndex = 392;
            this.txtprefixproduct.Visible = false;
            this.txtprefixproduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtprefixproduct_KeyDown);
            this.txtprefixproduct.Leave += new System.EventHandler(this.txtprefixproduct_Leave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(282, 149);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(295, 269);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 158;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // frmorderbooking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(904, 462);
            this.Controls.Add(this.txtprefixproduct);
            this.Controls.Add(this.dtgvcode);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.gvProductRemStock);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "frmorderbooking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Order Booking";
            this.Load += new System.EventHandler(this.frmorderbooking_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvProductRemStock)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.grpProductDetail.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvProductInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvcode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbcustomername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtorderno;
        private System.Windows.Forms.DateTimePicker dtpdelieverydate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkcompanyname;
        private System.Windows.Forms.ComboBox cmbcomanyname;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtProductname;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtporderdate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dtgvcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.ComboBox cmbUnit;
        private System.Windows.Forms.DataGridView gvProductRemStock;
        private System.Windows.Forms.ListBox txtprefixproduct;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnorderbookinglist;
        private System.Windows.Forms.GroupBox grpProductDetail;
        private System.Windows.Forms.Button btnCustForm;
        private System.Windows.Forms.Button btnAdd;
        private RachitControls.NumericTextBox txtQuantity;
        private System.Windows.Forms.DataGridView GvProductInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductNameg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Company;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewImageColumn Remove;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductNameR;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn UnitR;
    }
}