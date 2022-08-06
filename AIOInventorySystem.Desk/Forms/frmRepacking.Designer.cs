namespace AIOInventorySystem.Desk.Forms
{
    partial class frmRepacking
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
            this.txtRepackNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpRepack = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpExpiry = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label51 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtpname = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnRepackList = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnupdate = new System.Windows.Forms.Button();
            this.rbtndelete = new System.Windows.Forms.Button();
            this.btnPrintBarcode = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.grpProductDetail = new System.Windows.Forms.GroupBox();
            this.tblltDetail = new System.Windows.Forms.TableLayoutPanel();
            this.GvProductInfo = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductNameg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Company = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaleRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MRP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpiryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Conversion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remove = new System.Windows.Forms.DataGridViewImageColumn();
            this.txtConversion = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtMRP = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.txtSaleRate = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.txtQuantity = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.tblltPName = new System.Windows.Forms.TableLayoutPanel();
            this.rbtnaddproduct = new System.Windows.Forms.Button();
            this.tblltMain.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.grpProductDetail.SuspendLayout();
            this.tblltDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvProductInfo)).BeginInit();
            this.tblltPName.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRepackNo
            // 
            this.txtRepackNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRepackNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepackNo.ForeColor = System.Drawing.Color.Black;
            this.txtRepackNo.Location = new System.Drawing.Point(107, 37);
            this.txtRepackNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtRepackNo.Name = "txtRepackNo";
            this.txtRepackNo.ReadOnly = true;
            this.txtRepackNo.Size = new System.Drawing.Size(101, 29);
            this.txtRepackNo.TabIndex = 0;
            this.txtRepackNo.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(212, 37);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 25);
            this.label4.TabIndex = 28;
            this.label4.Text = "Repack Date:";
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
            this.label1.Size = new System.Drawing.Size(101, 25);
            this.label1.TabIndex = 27;
            this.label1.Text = "Repack No:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpRepack
            // 
            this.dtpRepack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpRepack.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpRepack.Location = new System.Drawing.Point(317, 37);
            this.dtpRepack.Margin = new System.Windows.Forms.Padding(2);
            this.dtpRepack.Name = "dtpRepack";
            this.dtpRepack.Size = new System.Drawing.Size(136, 29);
            this.dtpRepack.TabIndex = 1;
            this.dtpRepack.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpRepack_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(486, 29);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 23);
            this.label5.TabIndex = 35;
            this.label5.Text = "Conversion:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpExpiry
            // 
            this.dtpExpiry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpExpiry.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpExpiry.Location = new System.Drawing.Point(390, 29);
            this.dtpExpiry.Margin = new System.Windows.Forms.Padding(2);
            this.dtpExpiry.Name = "dtpExpiry";
            this.dtpExpiry.Size = new System.Drawing.Size(92, 29);
            this.dtpExpiry.TabIndex = 7;
            this.dtpExpiry.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpExpiry_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(304, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 23);
            this.label2.TabIndex = 67;
            this.label2.Text = "Expiry Date:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label13.Location = new System.Drawing.Point(642, 0);
            this.label13.Margin = new System.Windows.Forms.Padding(0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 27);
            this.label13.TabIndex = 45;
            this.label13.Text = "*";
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label51.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label51.ForeColor = System.Drawing.Color.Black;
            this.label51.Location = new System.Drawing.Point(2, 29);
            this.label51.Margin = new System.Windows.Forms.Padding(2);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(99, 23);
            this.label51.TabIndex = 35;
            this.label51.Text = "Sale Rate:";
            this.label51.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(486, 2);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 23);
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
            this.label3.Location = new System.Drawing.Point(2, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 23);
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
            this.txtpname.Location = new System.Drawing.Point(2, 2);
            this.txtpname.Margin = new System.Windows.Forms.Padding(2);
            this.txtpname.Name = "txtpname";
            this.txtpname.Size = new System.Drawing.Size(319, 29);
            this.txtpname.TabIndex = 2;
            this.txtpname.Text = " ";
            this.txtpname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpname_KeyDown);
            this.txtpname.Leave += new System.EventHandler(this.txtpname_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(177, 29);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 23);
            this.label7.TabIndex = 33;
            this.label7.Text = "MRP:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label12.Location = new System.Drawing.Point(323, 0);
            this.label12.Margin = new System.Windows.Forms.Padding(0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 27);
            this.label12.TabIndex = 44;
            this.label12.Text = "*";
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 5;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblltMain.Controls.Add(this.tblltButtons, 0, 3);
            this.tblltMain.Controls.Add(this.label8, 0, 0);
            this.tblltMain.Controls.Add(this.label1, 0, 1);
            this.tblltMain.Controls.Add(this.txtRepackNo, 1, 1);
            this.tblltMain.Controls.Add(this.label4, 2, 1);
            this.tblltMain.Controls.Add(this.dtpRepack, 3, 1);
            this.tblltMain.Controls.Add(this.grpProductDetail, 0, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.Size = new System.Drawing.Size(704, 422);
            this.tblltMain.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 9;
            this.tblltMain.SetColumnSpan(this.tblltButtons, 5);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.5F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.5F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.5F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.5F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.5F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.5F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltButtons.Controls.Add(this.btnRepackList, 7, 0);
            this.tblltButtons.Controls.Add(this.btnNew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnSave, 2, 0);
            this.tblltButtons.Controls.Add(this.btnupdate, 3, 0);
            this.tblltButtons.Controls.Add(this.rbtndelete, 4, 0);
            this.tblltButtons.Controls.Add(this.btnPrintBarcode, 6, 0);
            this.tblltButtons.Controls.Add(this.btnclose, 5, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 386);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(704, 36);
            this.tblltButtons.TabIndex = 11;
            // 
            // btnRepackList
            // 
            this.btnRepackList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnRepackList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRepackList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRepackList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRepackList.ForeColor = System.Drawing.Color.White;
            this.btnRepackList.Location = new System.Drawing.Point(521, 2);
            this.btnRepackList.Margin = new System.Windows.Forms.Padding(2);
            this.btnRepackList.Name = "btnRepackList";
            this.btnRepackList.Size = new System.Drawing.Size(91, 32);
            this.btnRepackList.TabIndex = 17;
            this.btnRepackList.Text = "Repack List";
            this.btnRepackList.UseVisualStyleBackColor = false;
            this.btnRepackList.Click += new System.EventHandler(this.btnRepackList_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(86, 2);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(62, 32);
            this.btnNew.TabIndex = 12;
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
            this.btnSave.Location = new System.Drawing.Point(152, 2);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(62, 32);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnupdate
            // 
            this.btnupdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnupdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnupdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnupdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnupdate.ForeColor = System.Drawing.Color.White;
            this.btnupdate.Location = new System.Drawing.Point(218, 2);
            this.btnupdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnupdate.Name = "btnupdate";
            this.btnupdate.Size = new System.Drawing.Size(62, 32);
            this.btnupdate.TabIndex = 13;
            this.btnupdate.Text = "Update";
            this.btnupdate.UseVisualStyleBackColor = false;
            this.btnupdate.Click += new System.EventHandler(this.btnupdate_Click);
            // 
            // rbtndelete
            // 
            this.rbtndelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.rbtndelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.rbtndelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtndelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtndelete.ForeColor = System.Drawing.Color.White;
            this.rbtndelete.Location = new System.Drawing.Point(284, 2);
            this.rbtndelete.Margin = new System.Windows.Forms.Padding(2);
            this.rbtndelete.Name = "rbtndelete";
            this.rbtndelete.Size = new System.Drawing.Size(62, 32);
            this.rbtndelete.TabIndex = 14;
            this.rbtndelete.Text = "Delete";
            this.rbtndelete.UseVisualStyleBackColor = false;
            this.rbtndelete.Click += new System.EventHandler(this.rbtndelete_Click);
            // 
            // btnPrintBarcode
            // 
            this.btnPrintBarcode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnPrintBarcode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrintBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrintBarcode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintBarcode.ForeColor = System.Drawing.Color.White;
            this.btnPrintBarcode.Location = new System.Drawing.Point(416, 2);
            this.btnPrintBarcode.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrintBarcode.Name = "btnPrintBarcode";
            this.btnPrintBarcode.Size = new System.Drawing.Size(101, 32);
            this.btnPrintBarcode.TabIndex = 16;
            this.btnPrintBarcode.Text = "Print Barcode";
            this.btnPrintBarcode.UseVisualStyleBackColor = false;
            this.btnPrintBarcode.Click += new System.EventHandler(this.btnPrintBarcode_Click);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnclose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnclose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(350, 2);
            this.btnclose.Margin = new System.Windows.Forms.Padding(2);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(62, 32);
            this.btnclose.TabIndex = 15;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label8, 5);
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(704, 35);
            this.label8.TabIndex = 37;
            this.label8.Text = "RePacking";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpProductDetail
            // 
            this.tblltMain.SetColumnSpan(this.grpProductDetail, 5);
            this.grpProductDetail.Controls.Add(this.tblltDetail);
            this.grpProductDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpProductDetail.Location = new System.Drawing.Point(3, 67);
            this.grpProductDetail.Name = "grpProductDetail";
            this.grpProductDetail.Size = new System.Drawing.Size(698, 316);
            this.grpProductDetail.TabIndex = 2;
            this.grpProductDetail.TabStop = false;
            this.grpProductDetail.Text = "Product Information";
            // 
            // tblltDetail
            // 
            this.tblltDetail.ColumnCount = 9;
            this.tblltDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.92537F));
            this.tblltDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.44776F));
            this.tblltDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.960199F));
            this.tblltDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.44776F));
            this.tblltDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.43781F));
            this.tblltDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.93035F));
            this.tblltDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.43781F));
            this.tblltDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.44776F));
            this.tblltDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.965174F));
            this.tblltDetail.Controls.Add(this.GvProductInfo, 0, 2);
            this.tblltDetail.Controls.Add(this.txtConversion, 7, 1);
            this.tblltDetail.Controls.Add(this.btnAdd, 8, 1);
            this.tblltDetail.Controls.Add(this.txtMRP, 3, 1);
            this.tblltDetail.Controls.Add(this.txtSaleRate, 1, 1);
            this.tblltDetail.Controls.Add(this.dtpExpiry, 5, 1);
            this.tblltDetail.Controls.Add(this.txtQuantity, 7, 0);
            this.tblltDetail.Controls.Add(this.tblltPName, 1, 0);
            this.tblltDetail.Controls.Add(this.label3, 0, 0);
            this.tblltDetail.Controls.Add(this.label13, 8, 0);
            this.tblltDetail.Controls.Add(this.label5, 6, 1);
            this.tblltDetail.Controls.Add(this.label6, 6, 0);
            this.tblltDetail.Controls.Add(this.label51, 0, 1);
            this.tblltDetail.Controls.Add(this.label2, 4, 1);
            this.tblltDetail.Controls.Add(this.label7, 2, 1);
            this.tblltDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltDetail.Location = new System.Drawing.Point(3, 25);
            this.tblltDetail.Name = "tblltDetail";
            this.tblltDetail.RowCount = 3;
            this.tblltDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.5F));
            this.tblltDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.5F));
            this.tblltDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81F));
            this.tblltDetail.Size = new System.Drawing.Size(692, 288);
            this.tblltDetail.TabIndex = 2;
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
            this.Quantity,
            this.SaleRate,
            this.MRP,
            this.ExpiryDate,
            this.Barcode,
            this.Conversion,
            this.Remove});
            this.tblltDetail.SetColumnSpan(this.GvProductInfo, 9);
            this.GvProductInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvProductInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvProductInfo.Location = new System.Drawing.Point(3, 57);
            this.GvProductInfo.Name = "GvProductInfo";
            this.GvProductInfo.ReadOnly = true;
            this.GvProductInfo.RowHeadersWidth = 20;
            this.GvProductInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvProductInfo.Size = new System.Drawing.Size(686, 228);
            this.GvProductInfo.TabIndex = 10;
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
            // Quantity
            // 
            this.Quantity.FillWeight = 32.86875F;
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.Name = "Quantity";
            this.Quantity.ReadOnly = true;
            // 
            // SaleRate
            // 
            this.SaleRate.FillWeight = 63.06959F;
            this.SaleRate.HeaderText = "SaleRate";
            this.SaleRate.Name = "SaleRate";
            this.SaleRate.ReadOnly = true;
            // 
            // MRP
            // 
            this.MRP.FillWeight = 63.06959F;
            this.MRP.HeaderText = "MRP";
            this.MRP.Name = "MRP";
            this.MRP.ReadOnly = true;
            // 
            // ExpiryDate
            // 
            this.ExpiryDate.HeaderText = "Expiry Date";
            this.ExpiryDate.Name = "ExpiryDate";
            this.ExpiryDate.ReadOnly = true;
            this.ExpiryDate.Visible = false;
            // 
            // Barcode
            // 
            this.Barcode.FillWeight = 63.06959F;
            this.Barcode.HeaderText = "Barcode";
            this.Barcode.Name = "Barcode";
            this.Barcode.ReadOnly = true;
            // 
            // Conversion
            // 
            this.Conversion.HeaderText = "Conversion";
            this.Conversion.Name = "Conversion";
            this.Conversion.ReadOnly = true;
            this.Conversion.Visible = false;
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
            // txtConversion
            // 
            this.txtConversion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConversion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConversion.Location = new System.Drawing.Point(572, 29);
            this.txtConversion.Margin = new System.Windows.Forms.Padding(2);
            this.txtConversion.Name = "txtConversion";
            this.txtConversion.Size = new System.Drawing.Size(68, 29);
            this.txtConversion.TabIndex = 8;
            this.txtConversion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtConversion_KeyDown);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(644, 28);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(46, 25);
            this.btnAdd.TabIndex = 9;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtMRP
            // 
            this.txtMRP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMRP.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMRP.Location = new System.Drawing.Point(232, 29);
            this.txtMRP.Margin = new System.Windows.Forms.Padding(2);
            this.txtMRP.Name = "txtMRP";
            this.txtMRP.Size = new System.Drawing.Size(68, 29);
            this.txtMRP.TabIndex = 6;
            this.txtMRP.Text = "0";
            this.txtMRP.TextChanged += new System.EventHandler(this.txtMRP_TextChanged);
            this.txtMRP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMRP_KeyDown);
            // 
            // txtSaleRate
            // 
            this.txtSaleRate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSaleRate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaleRate.Location = new System.Drawing.Point(105, 29);
            this.txtSaleRate.Margin = new System.Windows.Forms.Padding(2);
            this.txtSaleRate.Name = "txtSaleRate";
            this.txtSaleRate.Size = new System.Drawing.Size(68, 29);
            this.txtSaleRate.TabIndex = 5;
            this.txtSaleRate.Text = "0";
            this.txtSaleRate.TextChanged += new System.EventHandler(this.txtSaleRate_TextChanged);
            this.txtSaleRate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSaleRate_KeyDown);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQuantity.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantity.Location = new System.Drawing.Point(572, 2);
            this.txtQuantity.Margin = new System.Windows.Forms.Padding(2);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(68, 29);
            this.txtQuantity.TabIndex = 4;
            this.txtQuantity.Text = "0";
            this.txtQuantity.TextChanged += new System.EventHandler(this.txtQuantity_TextChanged);
            this.txtQuantity.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuantity_KeyDown);
            this.txtQuantity.Leave += new System.EventHandler(this.txtQuantity_Leave);
            // 
            // tblltPName
            // 
            this.tblltPName.ColumnCount = 3;
            this.tblltDetail.SetColumnSpan(this.tblltPName, 5);
            this.tblltPName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tblltPName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltPName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltPName.Controls.Add(this.rbtnaddproduct, 2, 0);
            this.tblltPName.Controls.Add(this.label12, 1, 0);
            this.tblltPName.Controls.Add(this.txtpname, 0, 0);
            this.tblltPName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltPName.Location = new System.Drawing.Point(103, 0);
            this.tblltPName.Margin = new System.Windows.Forms.Padding(0);
            this.tblltPName.Name = "tblltPName";
            this.tblltPName.RowCount = 1;
            this.tblltPName.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltPName.Size = new System.Drawing.Size(381, 27);
            this.tblltPName.TabIndex = 2;
            // 
            // rbtnaddproduct
            // 
            this.rbtnaddproduct.BackgroundImage = global::AIOInventorySystem.Desk.Properties.Resources.newbutton;
            this.rbtnaddproduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.rbtnaddproduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtnaddproduct.FlatAppearance.BorderSize = 0;
            this.rbtnaddproduct.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.rbtnaddproduct.Location = new System.Drawing.Point(344, 2);
            this.rbtnaddproduct.Margin = new System.Windows.Forms.Padding(2);
            this.rbtnaddproduct.Name = "rbtnaddproduct";
            this.rbtnaddproduct.Size = new System.Drawing.Size(35, 23);
            this.rbtnaddproduct.TabIndex = 3;
            this.rbtnaddproduct.UseVisualStyleBackColor = true;
            this.rbtnaddproduct.Click += new System.EventHandler(this.rbtnaddproduct_Click);
            // 
            // frmRepacking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(704, 422);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmRepacking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Repacking";
            this.Load += new System.EventHandler(this.frmRepacking_Load);
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.grpProductDetail.ResumeLayout(false);
            this.tblltDetail.ResumeLayout(false);
            this.tblltDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvProductInfo)).EndInit();
            this.tblltPName.ResumeLayout(false);
            this.tblltPName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtRepackNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpRepack;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtpname;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpExpiry;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnRepackList;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnupdate;
        private System.Windows.Forms.Button rbtndelete;
        private System.Windows.Forms.Button btnPrintBarcode;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.GroupBox grpProductDetail;
        private System.Windows.Forms.TableLayoutPanel tblltDetail;
        private System.Windows.Forms.Button rbtnaddproduct;
        private System.Windows.Forms.Button btnAdd;
        private RachitControls.NumericTextBox txtQuantity;
        private RachitControls.NumericTextBox txtSaleRate;
        private RachitControls.NumericTextBox txtMRP;
        private RachitControls.NumericTextBox txtConversion;
        private System.Windows.Forms.DataGridView GvProductInfo;
        private System.Windows.Forms.TableLayoutPanel tblltPName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductNameg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Company;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn SaleRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn MRP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpiryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Conversion;
        private System.Windows.Forms.DataGridViewImageColumn Remove;
    }
}