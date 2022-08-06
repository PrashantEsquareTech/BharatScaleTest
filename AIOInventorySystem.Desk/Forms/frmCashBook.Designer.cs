namespace AIOInventorySystem.Desk.Forms
{
    partial class frmCashBook
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
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpfromdate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.GVDr = new System.Windows.Forms.DataGridView();
            this.DrParticulars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DrAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GVCr = new System.Windows.Forms.DataGridView();
            this.CrParticulars = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CrAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label6 = new System.Windows.Forms.Label();
            this.lbltotalDrAmount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotalCrAmount = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnsearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GVDr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVCr)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label9, 8);
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(694, 35);
            this.label9.TabIndex = 36;
            this.label9.Text = "Cash Book";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 26);
            this.label1.TabIndex = 144;
            this.label1.Text = "Date From";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpfromdate
            // 
            this.dtpfromdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfromdate.Location = new System.Drawing.Point(88, 37);
            this.dtpfromdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpfromdate.Name = "dtpfromdate";
            this.dtpfromdate.Size = new System.Drawing.Size(108, 25);
            this.dtpfromdate.TabIndex = 0;
            this.dtpfromdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpfromdate_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(200, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 26);
            this.label2.TabIndex = 143;
            this.label2.Text = "To";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpToDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(234, 37);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(108, 25);
            this.dtpToDate.TabIndex = 1;
            this.dtpToDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpToDate_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(2, 67);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 22);
            this.label4.TabIndex = 151;
            this.label4.Text = "Dr";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(604, 67);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 22);
            this.label3.TabIndex = 152;
            this.label3.Text = "Cr";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // GVDr
            // 
            this.GVDr.AllowUserToAddRows = false;
            this.GVDr.AllowUserToDeleteRows = false;
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
            this.DrParticulars,
            this.DrAmount});
            this.tblltMain.SetColumnSpan(this.GVDr, 4);
            this.GVDr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GVDr.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GVDr.Location = new System.Drawing.Point(3, 95);
            this.GVDr.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.GVDr.Size = new System.Drawing.Size(338, 314);
            this.GVDr.TabIndex = 153;
            this.GVDr.TabStop = false;
            this.GVDr.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GVDr_CellClick);
            // 
            // DrParticulars
            // 
            this.DrParticulars.HeaderText = "Particulars";
            this.DrParticulars.Name = "DrParticulars";
            this.DrParticulars.ReadOnly = true;
            this.DrParticulars.Width = 200;
            // 
            // DrAmount
            // 
            this.DrAmount.HeaderText = "Amount";
            this.DrAmount.Name = "DrAmount";
            this.DrAmount.ReadOnly = true;
            this.DrAmount.Width = 130;
            // 
            // GVCr
            // 
            this.GVCr.AllowUserToAddRows = false;
            this.GVCr.AllowUserToDeleteRows = false;
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
            this.CrParticulars,
            this.CrAmount});
            this.tblltMain.SetColumnSpan(this.GVCr, 4);
            this.GVCr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GVCr.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GVCr.Location = new System.Drawing.Point(347, 95);
            this.GVCr.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.GVCr.Size = new System.Drawing.Size(344, 314);
            this.GVCr.TabIndex = 154;
            this.GVCr.TabStop = false;
            this.GVCr.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GVCr_CellClick);
            // 
            // CrParticulars
            // 
            this.CrParticulars.HeaderText = "Particulars";
            this.CrParticulars.Name = "CrParticulars";
            this.CrParticulars.ReadOnly = true;
            this.CrParticulars.Width = 200;
            // 
            // CrAmount
            // 
            this.CrAmount.HeaderText = "Amount";
            this.CrAmount.Name = "CrAmount";
            this.CrAmount.ReadOnly = true;
            this.CrAmount.Width = 130;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.tblltMain.SetColumnSpan(this.label6, 2);
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(2, 415);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(194, 25);
            this.label6.TabIndex = 156;
            this.label6.Text = "Total Amount:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbltotalDrAmount
            // 
            this.lbltotalDrAmount.AutoSize = true;
            this.tblltMain.SetColumnSpan(this.lbltotalDrAmount, 2);
            this.lbltotalDrAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltotalDrAmount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalDrAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbltotalDrAmount.Location = new System.Drawing.Point(200, 415);
            this.lbltotalDrAmount.Margin = new System.Windows.Forms.Padding(2);
            this.lbltotalDrAmount.Name = "lbltotalDrAmount";
            this.lbltotalDrAmount.Size = new System.Drawing.Size(142, 25);
            this.lbltotalDrAmount.TabIndex = 158;
            this.lbltotalDrAmount.Text = "0";
            this.lbltotalDrAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.tblltMain.SetColumnSpan(this.label5, 2);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label5.Location = new System.Drawing.Point(346, 415);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 25);
            this.label5.TabIndex = 155;
            this.label5.Text = "Total Amount:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalCrAmount
            // 
            this.lblTotalCrAmount.AutoSize = true;
            this.tblltMain.SetColumnSpan(this.lblTotalCrAmount, 2);
            this.lblTotalCrAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalCrAmount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCrAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalCrAmount.Location = new System.Drawing.Point(518, 415);
            this.lblTotalCrAmount.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotalCrAmount.Name = "lblTotalCrAmount";
            this.lblTotalCrAmount.Size = new System.Drawing.Size(174, 25);
            this.lblTotalCrAmount.TabIndex = 157;
            this.lblTotalCrAmount.Text = "0";
            this.lblTotalCrAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressBar1.Location = new System.Drawing.Point(142, 230);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(407, 24);
            this.progressBar1.TabIndex = 159;
            this.progressBar1.Visible = false;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 8;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.25F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.25F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltMain.Controls.Add(this.label9, 0, 0);
            this.tblltMain.Controls.Add(this.lbltotalDrAmount, 2, 4);
            this.tblltMain.Controls.Add(this.label6, 0, 4);
            this.tblltMain.Controls.Add(this.label1, 0, 1);
            this.tblltMain.Controls.Add(this.label5, 4, 4);
            this.tblltMain.Controls.Add(this.dtpfromdate, 1, 1);
            this.tblltMain.Controls.Add(this.lblTotalCrAmount, 6, 4);
            this.tblltMain.Controls.Add(this.label2, 2, 1);
            this.tblltMain.Controls.Add(this.dtpToDate, 3, 1);
            this.tblltMain.Controls.Add(this.label3, 7, 2);
            this.tblltMain.Controls.Add(this.label4, 0, 2);
            this.tblltMain.Controls.Add(this.GVDr, 0, 3);
            this.tblltMain.Controls.Add(this.GVCr, 4, 3);
            this.tblltMain.Controls.Add(this.btnPrint, 7, 1);
            this.tblltMain.Controls.Add(this.btnsearch, 6, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 5;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.Size = new System.Drawing.Size(694, 442);
            this.tblltMain.TabIndex = 0;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(604, 37);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(88, 26);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(518, 37);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(82, 26);
            this.btnsearch.TabIndex = 2;
            this.btnsearch.Text = "Search";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // frmCashBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(694, 442);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmCashBook";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cash Book";
            ((System.ComponentModel.ISupportInitialize)(this.GVDr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GVCr)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpfromdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView GVDr;
        private System.Windows.Forms.DataGridView GVCr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbltotalDrAmount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalCrAmount;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrParticulars;
        private System.Windows.Forms.DataGridViewTextBoxColumn DrAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn CrParticulars;
        private System.Windows.Forms.DataGridViewTextBoxColumn CrAmount;
    }
}