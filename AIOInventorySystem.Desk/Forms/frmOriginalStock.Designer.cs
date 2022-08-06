namespace AIOInventorySystem.Desk.Forms
{
    partial class frmOriginalStock
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbcomanyname = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProductname = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.GvPorderInfo = new System.Windows.Forms.DataGridView();
            this.lbltotalqty = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbltotalremamt = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.cmbGroupName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbGodownName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tblltRow3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.btnminusstock = new System.Windows.Forms.Button();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.btnCompoundUnit = new System.Windows.Forms.Button();
            this.btncheck = new System.Windows.Forms.Button();
            this.dtpfromdate = new System.Windows.Forms.DateTimePicker();
            this.chkAssemeblyProduct = new System.Windows.Forms.CheckBox();
            this.chkSize = new System.Windows.Forms.CheckBox();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.GvPorderInfo)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.tblltRow3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(341, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 25);
            this.label1.TabIndex = 48;
            this.label1.Text = "Mfg. Company:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbcomanyname
            // 
            this.cmbcomanyname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcomanyname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcomanyname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcomanyname.ForeColor = System.Drawing.Color.Black;
            this.cmbcomanyname.FormattingEnabled = true;
            this.cmbcomanyname.Location = new System.Drawing.Point(454, 41);
            this.cmbcomanyname.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcomanyname.Name = "cmbcomanyname";
            this.cmbcomanyname.Size = new System.Drawing.Size(153, 29);
            this.cmbcomanyname.TabIndex = 2;
            this.cmbcomanyname.SelectedIndexChanged += new System.EventHandler(this.cmbcomanyname_SelectedIndexChanged);
            this.cmbcomanyname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcomanyname_KeyDown);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label5, 6);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(984, 39);
            this.label5.TabIndex = 11;
            this.label5.Text = "Remaining Stock";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtProductname
            // 
            this.txtProductname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtProductname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtProductname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductname.Location = new System.Drawing.Point(115, 41);
            this.txtProductname.Margin = new System.Windows.Forms.Padding(2);
            this.txtProductname.Name = "txtProductname";
            this.txtProductname.Size = new System.Drawing.Size(222, 29);
            this.txtProductname.TabIndex = 1;
            this.txtProductname.TextChanged += new System.EventHandler(this.txtProductname_TextChanged);
            this.txtProductname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductname_KeyDown);
            this.txtProductname.Leave += new System.EventHandler(this.txtProductname_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(2, 41);
            this.label11.Margin = new System.Windows.Forms.Padding(2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(109, 25);
            this.label11.TabIndex = 40;
            this.label11.Text = "Product Name:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(837, 41);
            this.lblTotalAmount.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(145, 25);
            this.lblTotalAmount.TabIndex = 50;
            this.lblTotalAmount.Text = "0.00";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // GvPorderInfo
            // 
            this.GvPorderInfo.AllowUserToAddRows = false;
            this.GvPorderInfo.AllowUserToDeleteRows = false;
            this.GvPorderInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.GvPorderInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvPorderInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvPorderInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvPorderInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblltMain.SetColumnSpan(this.GvPorderInfo, 6);
            this.GvPorderInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvPorderInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvPorderInfo.Location = new System.Drawing.Point(2, 128);
            this.GvPorderInfo.Margin = new System.Windows.Forms.Padding(2);
            this.GvPorderInfo.Name = "GvPorderInfo";
            this.GvPorderInfo.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvPorderInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GvPorderInfo.RowHeadersWidth = 15;
            this.GvPorderInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvPorderInfo.Size = new System.Drawing.Size(980, 431);
            this.GvPorderInfo.TabIndex = 15;
            this.GvPorderInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvPorderInfo_CellClick);
            // 
            // lbltotalqty
            // 
            this.lbltotalqty.AutoSize = true;
            this.lbltotalqty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltotalqty.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalqty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbltotalqty.Location = new System.Drawing.Point(837, 70);
            this.lbltotalqty.Margin = new System.Windows.Forms.Padding(2);
            this.lbltotalqty.Name = "lbltotalqty";
            this.lbltotalqty.Size = new System.Drawing.Size(145, 25);
            this.lbltotalqty.TabIndex = 53;
            this.lbltotalqty.Text = "0.00";
            this.lbltotalqty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(611, 70);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 25);
            this.label3.TabIndex = 52;
            this.label3.Text = "Total Remaining Stock Qty:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbltotalremamt
            // 
            this.lbltotalremamt.AutoSize = true;
            this.lbltotalremamt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltotalremamt.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalremamt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbltotalremamt.Location = new System.Drawing.Point(611, 41);
            this.lbltotalremamt.Margin = new System.Windows.Forms.Padding(2);
            this.lbltotalremamt.Name = "lbltotalremamt";
            this.lbltotalremamt.Size = new System.Drawing.Size(222, 25);
            this.lbltotalremamt.TabIndex = 49;
            this.lbltotalremamt.Text = "Stock Valuation:";
            this.lbltotalremamt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 6;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.cmbGroupName, 1, 2);
            this.tblltMain.Controls.Add(this.label4, 0, 2);
            this.tblltMain.Controls.Add(this.cmbGodownName, 3, 2);
            this.tblltMain.Controls.Add(this.GvPorderInfo, 0, 4);
            this.tblltMain.Controls.Add(this.lbltotalqty, 5, 2);
            this.tblltMain.Controls.Add(this.label2, 2, 2);
            this.tblltMain.Controls.Add(this.label11, 0, 1);
            this.tblltMain.Controls.Add(this.lblTotalAmount, 5, 1);
            this.tblltMain.Controls.Add(this.txtProductname, 1, 1);
            this.tblltMain.Controls.Add(this.label3, 4, 2);
            this.tblltMain.Controls.Add(this.lbltotalremamt, 4, 1);
            this.tblltMain.Controls.Add(this.label1, 2, 1);
            this.tblltMain.Controls.Add(this.cmbcomanyname, 3, 1);
            this.tblltMain.Controls.Add(this.tblltRow3, 0, 3);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 5;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.25F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.25F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.25F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.25F));
            this.tblltMain.Size = new System.Drawing.Size(984, 561);
            this.tblltMain.TabIndex = 0;
            // 
            // cmbGroupName
            // 
            this.cmbGroupName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbGroupName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbGroupName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbGroupName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGroupName.ForeColor = System.Drawing.Color.Black;
            this.cmbGroupName.FormattingEnabled = true;
            this.cmbGroupName.Location = new System.Drawing.Point(115, 70);
            this.cmbGroupName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbGroupName.Name = "cmbGroupName";
            this.cmbGroupName.Size = new System.Drawing.Size(222, 29);
            this.cmbGroupName.TabIndex = 3;
            this.cmbGroupName.SelectedIndexChanged += new System.EventHandler(this.cmbGroupName_SelectedIndexChanged);
            this.cmbGroupName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbGroupName_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(2, 70);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 25);
            this.label4.TabIndex = 93;
            this.label4.Text = "Group Name:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbGodownName
            // 
            this.cmbGodownName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbGodownName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGodownName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGodownName.ForeColor = System.Drawing.Color.Black;
            this.cmbGodownName.FormattingEnabled = true;
            this.cmbGodownName.Location = new System.Drawing.Point(454, 70);
            this.cmbGodownName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbGodownName.Name = "cmbGodownName";
            this.cmbGodownName.Size = new System.Drawing.Size(153, 29);
            this.cmbGodownName.TabIndex = 4;
            this.cmbGodownName.SelectedIndexChanged += new System.EventHandler(this.cmbGodownName_SelectedIndexChanged);
            this.cmbGodownName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbGodownName_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(341, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 25);
            this.label2.TabIndex = 93;
            this.label2.Text = "Godown Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblltRow3
            // 
            this.tblltRow3.ColumnCount = 11;
            this.tblltMain.SetColumnSpan(this.tblltRow3, 6);
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltRow3.Controls.Add(this.btnClose, 10, 0);
            this.tblltRow3.Controls.Add(this.btnprint, 9, 0);
            this.tblltRow3.Controls.Add(this.chkDate, 0, 0);
            this.tblltRow3.Controls.Add(this.btnminusstock, 8, 0);
            this.tblltRow3.Controls.Add(this.btnGetAll, 5, 0);
            this.tblltRow3.Controls.Add(this.btnCompoundUnit, 7, 0);
            this.tblltRow3.Controls.Add(this.btncheck, 6, 0);
            this.tblltRow3.Controls.Add(this.dtpfromdate, 1, 0);
            this.tblltRow3.Controls.Add(this.chkAssemeblyProduct, 2, 0);
            this.tblltRow3.Controls.Add(this.chkSize, 3, 0);
            this.tblltRow3.Controls.Add(this.txtSize, 4, 0);
            this.tblltRow3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow3.Location = new System.Drawing.Point(0, 97);
            this.tblltRow3.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow3.Name = "tblltRow3";
            this.tblltRow3.RowCount = 1;
            this.tblltRow3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow3.Size = new System.Drawing.Size(984, 29);
            this.tblltRow3.TabIndex = 5;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(910, 1);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72, 27);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(842, 1);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(64, 27);
            this.btnprint.TabIndex = 12;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // chkDate
            // 
            this.chkDate.AutoSize = true;
            this.chkDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDate.ForeColor = System.Drawing.Color.Black;
            this.chkDate.Location = new System.Drawing.Point(7, 2);
            this.chkDate.Margin = new System.Windows.Forms.Padding(7, 2, 2, 2);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(59, 25);
            this.chkDate.TabIndex = 5;
            this.chkDate.Text = "Date";
            this.chkDate.UseVisualStyleBackColor = true;
            this.chkDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkDate_KeyDown);
            // 
            // btnminusstock
            // 
            this.btnminusstock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnminusstock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnminusstock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnminusstock.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnminusstock.ForeColor = System.Drawing.Color.White;
            this.btnminusstock.Location = new System.Drawing.Point(734, 1);
            this.btnminusstock.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnminusstock.Name = "btnminusstock";
            this.btnminusstock.Size = new System.Drawing.Size(104, 27);
            this.btnminusstock.TabIndex = 11;
            this.btnminusstock.Text = "Minus Stock";
            this.btnminusstock.UseVisualStyleBackColor = false;
            this.btnminusstock.Click += new System.EventHandler(this.btnminusstock_Click);
            // 
            // btnGetAll
            // 
            this.btnGetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnGetAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGetAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAll.ForeColor = System.Drawing.Color.White;
            this.btnGetAll.Location = new System.Drawing.Point(461, 1);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(69, 27);
            this.btnGetAll.TabIndex = 8;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // btnCompoundUnit
            // 
            this.btnCompoundUnit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnCompoundUnit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCompoundUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCompoundUnit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompoundUnit.ForeColor = System.Drawing.Color.White;
            this.btnCompoundUnit.Location = new System.Drawing.Point(607, 1);
            this.btnCompoundUnit.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnCompoundUnit.Name = "btnCompoundUnit";
            this.btnCompoundUnit.Size = new System.Drawing.Size(123, 27);
            this.btnCompoundUnit.TabIndex = 10;
            this.btnCompoundUnit.Text = "Compound Unit";
            this.btnCompoundUnit.UseVisualStyleBackColor = false;
            this.btnCompoundUnit.Click += new System.EventHandler(this.btnCompoundUnit_Click);
            // 
            // btncheck
            // 
            this.btncheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btncheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btncheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btncheck.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncheck.ForeColor = System.Drawing.Color.White;
            this.btncheck.Location = new System.Drawing.Point(534, 1);
            this.btncheck.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btncheck.Name = "btncheck";
            this.btncheck.Size = new System.Drawing.Size(69, 27);
            this.btncheck.TabIndex = 9;
            this.btncheck.Text = "Check";
            this.btncheck.UseVisualStyleBackColor = false;
            this.btncheck.Click += new System.EventHandler(this.btncheck_Click);
            // 
            // dtpfromdate
            // 
            this.dtpfromdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfromdate.Location = new System.Drawing.Point(70, 2);
            this.dtpfromdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpfromdate.Name = "dtpfromdate";
            this.dtpfromdate.Size = new System.Drawing.Size(94, 29);
            this.dtpfromdate.TabIndex = 6;
            // 
            // chkAssemeblyProduct
            // 
            this.chkAssemeblyProduct.AutoSize = true;
            this.chkAssemeblyProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkAssemeblyProduct.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAssemeblyProduct.ForeColor = System.Drawing.Color.Black;
            this.chkAssemeblyProduct.Location = new System.Drawing.Point(173, 2);
            this.chkAssemeblyProduct.Margin = new System.Windows.Forms.Padding(7, 2, 2, 2);
            this.chkAssemeblyProduct.Name = "chkAssemeblyProduct";
            this.chkAssemeblyProduct.Size = new System.Drawing.Size(138, 25);
            this.chkAssemeblyProduct.TabIndex = 7;
            this.chkAssemeblyProduct.Text = "Assembly Product";
            this.chkAssemeblyProduct.UseVisualStyleBackColor = true;
            this.chkAssemeblyProduct.CheckedChanged += new System.EventHandler(this.chkAssemeblyProduct_CheckedChanged);
            // 
            // chkSize
            // 
            this.chkSize.AutoSize = true;
            this.chkSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkSize.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSize.ForeColor = System.Drawing.Color.Black;
            this.chkSize.Location = new System.Drawing.Point(317, 2);
            this.chkSize.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkSize.Name = "chkSize";
            this.chkSize.Size = new System.Drawing.Size(62, 25);
            this.chkSize.TabIndex = 15;
            this.chkSize.Text = "Size";
            this.chkSize.UseVisualStyleBackColor = true;
            // 
            // txtSize
            // 
            this.txtSize.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtSize.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSize.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSize.Location = new System.Drawing.Point(383, 2);
            this.txtSize.Margin = new System.Windows.Forms.Padding(2);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(74, 29);
            this.txtSize.TabIndex = 16;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(258, 283);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(460, 23);
            this.progressBar1.TabIndex = 175;
            this.progressBar1.Visible = false;
            // 
            // frmOriginalStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "frmOriginalStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Original Stock";
            this.Load += new System.EventHandler(this.frmOriginalStock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvPorderInfo)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.tblltRow3.ResumeLayout(false);
            this.tblltRow3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbcomanyname;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtProductname;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.DataGridView GvPorderInfo;
        private System.Windows.Forms.Label lbltotalqty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbltotalremamt;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltRow3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbGodownName;
        private System.Windows.Forms.ComboBox cmbGroupName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpfromdate;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox chkDate;
        private System.Windows.Forms.CheckBox chkAssemeblyProduct;
        private System.Windows.Forms.Button btnminusstock;
        private System.Windows.Forms.Button btnGetAll;
        private System.Windows.Forms.Button btnCompoundUnit;
        private System.Windows.Forms.Button btncheck;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chkSize;
        private System.Windows.Forms.TextBox txtSize;
    }
}