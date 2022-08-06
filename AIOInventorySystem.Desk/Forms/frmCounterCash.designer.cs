namespace AIOInventorySystem.Desk.Forms
{
    partial class frmCounterCash
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtpfromdate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOpeningCash = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtClosingCash = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lbltotalDrAmount = new System.Windows.Forms.Label();
            this.lblTotalCrAmount = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.GVCr = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVDr = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblltInputButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAddOpeningCash = new System.Windows.Forms.Button();
            this.btnAdjustment = new System.Windows.Forms.Button();
            this.btnsearch = new System.Windows.Forms.Button();
            this.btngetall = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPrintAll = new System.Windows.Forms.Button();
            this.tblltInput = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.GVCr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVDr)).BeginInit();
            this.tblltInputButtons.SuspendLayout();
            this.tblltInput.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpfromdate
            // 
            this.dtpfromdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfromdate.Location = new System.Drawing.Point(2, 25);
            this.dtpfromdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpfromdate.Name = "dtpfromdate";
            this.dtpfromdate.Size = new System.Drawing.Size(98, 25);
            this.dtpfromdate.TabIndex = 1;
            this.dtpfromdate.ValueChanged += new System.EventHandler(this.dtpfromdate_ValueChanged);
            this.dtpfromdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpfromdate_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(2, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 19);
            this.label2.TabIndex = 34;
            this.label2.Text = "Date";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label7, 4);
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(844, 37);
            this.label7.TabIndex = 36;
            this.label7.Text = "Counter Cash";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtOpeningCash
            // 
            this.txtOpeningCash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOpeningCash.Enabled = false;
            this.txtOpeningCash.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOpeningCash.Location = new System.Drawing.Point(104, 25);
            this.txtOpeningCash.Margin = new System.Windows.Forms.Padding(2);
            this.txtOpeningCash.Name = "txtOpeningCash";
            this.txtOpeningCash.Size = new System.Drawing.Size(91, 25);
            this.txtOpeningCash.TabIndex = 2;
            this.txtOpeningCash.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOpeningCash_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(104, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 19);
            this.label1.TabIndex = 52;
            this.label1.Text = "Opening Cash";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(199, 2);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(92, 19);
            this.label8.TabIndex = 54;
            this.label8.Text = "Closing Cash";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtClosingCash
            // 
            this.txtClosingCash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtClosingCash.Enabled = false;
            this.txtClosingCash.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClosingCash.Location = new System.Drawing.Point(199, 25);
            this.txtClosingCash.Margin = new System.Windows.Forms.Padding(2);
            this.txtClosingCash.Name = "txtClosingCash";
            this.txtClosingCash.Size = new System.Drawing.Size(92, 25);
            this.txtClosingCash.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(2, 98);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(207, 23);
            this.label4.TabIndex = 159;
            this.label4.Text = "Dr";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(170, 284);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(502, 26);
            this.progressBar1.TabIndex = 158;
            this.progressBar1.Visible = false;
            // 
            // lbltotalDrAmount
            // 
            this.lbltotalDrAmount.AutoSize = true;
            this.lbltotalDrAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltotalDrAmount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalDrAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbltotalDrAmount.Location = new System.Drawing.Point(213, 509);
            this.lbltotalDrAmount.Margin = new System.Windows.Forms.Padding(2);
            this.lbltotalDrAmount.Name = "lbltotalDrAmount";
            this.lbltotalDrAmount.Size = new System.Drawing.Size(207, 31);
            this.lbltotalDrAmount.TabIndex = 157;
            this.lbltotalDrAmount.Text = "0";
            this.lbltotalDrAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalCrAmount
            // 
            this.lblTotalCrAmount.AutoSize = true;
            this.lblTotalCrAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalCrAmount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCrAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalCrAmount.Location = new System.Drawing.Point(635, 509);
            this.lblTotalCrAmount.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotalCrAmount.Name = "lblTotalCrAmount";
            this.lblTotalCrAmount.Size = new System.Drawing.Size(207, 31);
            this.lblTotalCrAmount.TabIndex = 156;
            this.lblTotalCrAmount.Text = "0";
            this.lblTotalCrAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(2, 509);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(207, 31);
            this.label6.TabIndex = 155;
            this.label6.Text = "Total Amount";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(424, 509);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(207, 31);
            this.label5.TabIndex = 154;
            this.label5.Text = "Total Amount";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GVCr
            // 
            this.GVCr.AllowUserToAddRows = false;
            this.GVCr.AllowUserToDeleteRows = false;
            this.GVCr.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GVCr.BackgroundColor = System.Drawing.Color.White;
            this.GVCr.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVCr.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GVCr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVCr.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2});
            this.tblltMain.SetColumnSpan(this.GVCr, 2);
            this.GVCr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GVCr.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GVCr.Location = new System.Drawing.Point(425, 126);
            this.GVCr.Name = "GVCr";
            this.GVCr.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVCr.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.GVCr.RowHeadersWidth = 10;
            this.GVCr.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GVCr.Size = new System.Drawing.Size(416, 378);
            this.GVCr.TabIndex = 153;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Particulars";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Amount";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // GVDr
            // 
            this.GVDr.AllowUserToAddRows = false;
            this.GVDr.AllowUserToDeleteRows = false;
            this.GVDr.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GVDr.BackgroundColor = System.Drawing.Color.White;
            this.GVDr.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVDr.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GVDr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVDr.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.tblltMain.SetColumnSpan(this.GVDr, 2);
            this.GVDr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GVDr.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GVDr.Location = new System.Drawing.Point(3, 126);
            this.GVDr.Name = "GVDr";
            this.GVDr.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVDr.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GVDr.RowHeadersWidth = 10;
            this.GVDr.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GVDr.Size = new System.Drawing.Size(416, 378);
            this.GVDr.TabIndex = 152;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Particulars";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Amount";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // tblltInputButtons
            // 
            this.tblltInputButtons.ColumnCount = 8;
            this.tblltMain.SetColumnSpan(this.tblltInputButtons, 4);
            this.tblltInputButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblltInputButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tblltInputButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltInputButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltInputButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tblltInputButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltInputButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltInputButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltInputButtons.Controls.Add(this.btnUpdate, 1, 0);
            this.tblltInputButtons.Controls.Add(this.btnAddOpeningCash, 2, 0);
            this.tblltInputButtons.Controls.Add(this.btnAdjustment, 3, 0);
            this.tblltInputButtons.Controls.Add(this.btnsearch, 4, 0);
            this.tblltInputButtons.Controls.Add(this.btngetall, 5, 0);
            this.tblltInputButtons.Controls.Add(this.btnPrint, 6, 0);
            this.tblltInputButtons.Controls.Add(this.btnPrintAll, 7, 0);
            this.tblltInputButtons.Controls.Add(this.tblltInput, 0, 0);
            this.tblltInputButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltInputButtons.Location = new System.Drawing.Point(3, 40);
            this.tblltInputButtons.Name = "tblltInputButtons";
            this.tblltInputButtons.RowCount = 1;
            this.tblltInputButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltInputButtons.Size = new System.Drawing.Size(838, 53);
            this.tblltInputButtons.TabIndex = 0;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(294, 20);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(1, 20, 1, 1);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(60, 32);
            this.btnUpdate.TabIndex = 4;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAddOpeningCash
            // 
            this.btnAddOpeningCash.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnAddOpeningCash.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddOpeningCash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAddOpeningCash.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddOpeningCash.ForeColor = System.Drawing.Color.White;
            this.btnAddOpeningCash.Location = new System.Drawing.Point(356, 20);
            this.btnAddOpeningCash.Margin = new System.Windows.Forms.Padding(1, 20, 1, 1);
            this.btnAddOpeningCash.Name = "btnAddOpeningCash";
            this.btnAddOpeningCash.Size = new System.Drawing.Size(132, 32);
            this.btnAddOpeningCash.TabIndex = 5;
            this.btnAddOpeningCash.Text = "Add Opening Cash";
            this.btnAddOpeningCash.UseVisualStyleBackColor = false;
            this.btnAddOpeningCash.Click += new System.EventHandler(this.btnAddOpeningCash_Click);
            // 
            // btnAdjustment
            // 
            this.btnAdjustment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnAdjustment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdjustment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdjustment.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdjustment.ForeColor = System.Drawing.Color.White;
            this.btnAdjustment.Location = new System.Drawing.Point(490, 20);
            this.btnAdjustment.Margin = new System.Windows.Forms.Padding(1, 20, 1, 1);
            this.btnAdjustment.Name = "btnAdjustment";
            this.btnAdjustment.Size = new System.Drawing.Size(90, 32);
            this.btnAdjustment.TabIndex = 6;
            this.btnAdjustment.Text = "Adjustment";
            this.btnAdjustment.UseVisualStyleBackColor = false;
            this.btnAdjustment.Click += new System.EventHandler(this.btnAdjustment_Click);
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(582, 20);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(1, 20, 1, 1);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(60, 32);
            this.btnsearch.TabIndex = 7;
            this.btnsearch.Text = "Search";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // btngetall
            // 
            this.btngetall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btngetall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btngetall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btngetall.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngetall.ForeColor = System.Drawing.Color.White;
            this.btngetall.Location = new System.Drawing.Point(644, 20);
            this.btngetall.Margin = new System.Windows.Forms.Padding(1, 20, 1, 1);
            this.btngetall.Name = "btngetall";
            this.btngetall.Size = new System.Drawing.Size(65, 32);
            this.btngetall.TabIndex = 8;
            this.btngetall.Text = "Current";
            this.btngetall.UseVisualStyleBackColor = false;
            this.btngetall.Click += new System.EventHandler(this.btngetall_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(711, 20);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(1, 20, 1, 1);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(56, 32);
            this.btnPrint.TabIndex = 9;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPrintAll
            // 
            this.btnPrintAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnPrintAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrintAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrintAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintAll.ForeColor = System.Drawing.Color.White;
            this.btnPrintAll.Location = new System.Drawing.Point(769, 20);
            this.btnPrintAll.Margin = new System.Windows.Forms.Padding(1, 20, 1, 1);
            this.btnPrintAll.Name = "btnPrintAll";
            this.btnPrintAll.Size = new System.Drawing.Size(68, 32);
            this.btnPrintAll.TabIndex = 10;
            this.btnPrintAll.Text = "Print All";
            this.btnPrintAll.UseVisualStyleBackColor = false;
            this.btnPrintAll.Click += new System.EventHandler(this.btnPrintAll_Click);
            // 
            // tblltInput
            // 
            this.tblltInput.ColumnCount = 3;
            this.tblltInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblltInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.5F));
            this.tblltInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.5F));
            this.tblltInput.Controls.Add(this.label2, 0, 0);
            this.tblltInput.Controls.Add(this.dtpfromdate, 0, 1);
            this.tblltInput.Controls.Add(this.label1, 1, 0);
            this.tblltInput.Controls.Add(this.txtOpeningCash, 1, 1);
            this.tblltInput.Controls.Add(this.label8, 2, 0);
            this.tblltInput.Controls.Add(this.txtClosingCash, 2, 1);
            this.tblltInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltInput.Location = new System.Drawing.Point(0, 0);
            this.tblltInput.Margin = new System.Windows.Forms.Padding(0);
            this.tblltInput.Name = "tblltInput";
            this.tblltInput.RowCount = 2;
            this.tblltInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tblltInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tblltInput.Size = new System.Drawing.Size(293, 53);
            this.tblltInput.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(635, 98);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(207, 23);
            this.label3.TabIndex = 160;
            this.label3.Text = "Cr";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 4;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltMain.Controls.Add(this.label7, 0, 0);
            this.tblltMain.Controls.Add(this.label4, 0, 2);
            this.tblltMain.Controls.Add(this.label3, 3, 2);
            this.tblltMain.Controls.Add(this.GVDr, 0, 3);
            this.tblltMain.Controls.Add(this.label6, 0, 4);
            this.tblltMain.Controls.Add(this.tblltInputButtons, 0, 1);
            this.tblltMain.Controls.Add(this.GVCr, 2, 3);
            this.tblltMain.Controls.Add(this.lbltotalDrAmount, 1, 4);
            this.tblltMain.Controls.Add(this.label5, 2, 4);
            this.tblltMain.Controls.Add(this.lblTotalCrAmount, 3, 4);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 5;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.Size = new System.Drawing.Size(844, 542);
            this.tblltMain.TabIndex = 0;
            // 
            // frmCounterCash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(844, 542);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "frmCounterCash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Counter Cash";
            this.Load += new System.EventHandler(this.frmCounterCash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GVCr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVDr)).EndInit();
            this.tblltInputButtons.ResumeLayout(false);
            this.tblltInput.ResumeLayout(false);
            this.tblltInput.PerformLayout();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpfromdate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtOpeningCash;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtClosingCash;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lbltotalDrAmount;
        private System.Windows.Forms.Label lblTotalCrAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView GVCr;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridView GVDr;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.TableLayoutPanel tblltInputButtons;
        private System.Windows.Forms.TableLayoutPanel tblltInput;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAddOpeningCash;
        private System.Windows.Forms.Button btnAdjustment;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Button btngetall;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnPrintAll;
    }
}