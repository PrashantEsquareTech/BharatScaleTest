namespace AIOInventorySystem.Desk.Forms
{
    partial class frmVatRemainingStock
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cmbcomanyname = new System.Windows.Forms.ComboBox();
            this.lbltotalqty = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lbltotalremamt = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtProductname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbGodownName = new System.Windows.Forms.ComboBox();
            this.cmbGroupName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkDate = new System.Windows.Forms.CheckBox();
            this.ChkProductType = new System.Windows.Forms.CheckBox();
            this.dtpfromdate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.GvPorderInfo = new System.Windows.Forms.DataGridView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btncheck = new System.Windows.Forms.Button();
            this.btnCompoundUnitStock = new System.Windows.Forms.Button();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GvPorderInfo)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbcomanyname
            // 
            this.cmbcomanyname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcomanyname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcomanyname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcomanyname.ForeColor = System.Drawing.Color.Black;
            this.cmbcomanyname.FormattingEnabled = true;
            this.cmbcomanyname.Location = new System.Drawing.Point(408, 37);
            this.cmbcomanyname.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcomanyname.Name = "cmbcomanyname";
            this.cmbcomanyname.Size = new System.Drawing.Size(155, 25);
            this.cmbcomanyname.TabIndex = 2;
            this.cmbcomanyname.SelectedIndexChanged += new System.EventHandler(this.cmbcomanyname_SelectedIndexChanged);
            this.cmbcomanyname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcomanyname_KeyDown);
            // 
            // lbltotalqty
            // 
            this.lbltotalqty.AutoSize = true;
            this.lbltotalqty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltotalqty.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalqty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbltotalqty.Location = new System.Drawing.Point(779, 65);
            this.lbltotalqty.Margin = new System.Windows.Forms.Padding(2);
            this.lbltotalqty.Name = "lbltotalqty";
            this.lbltotalqty.Size = new System.Drawing.Size(103, 24);
            this.lbltotalqty.TabIndex = 57;
            this.lbltotalqty.Text = "0.00";
            this.lbltotalqty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(567, 65);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(208, 24);
            this.label3.TabIndex = 56;
            this.label3.Text = "Total Remaining Stock Qty:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(779, 37);
            this.lblTotalAmount.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(103, 24);
            this.lblTotalAmount.TabIndex = 48;
            this.lblTotalAmount.Text = "0.00";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbltotalremamt
            // 
            this.lbltotalremamt.AutoSize = true;
            this.lbltotalremamt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltotalremamt.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalremamt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbltotalremamt.Location = new System.Drawing.Point(567, 37);
            this.lbltotalremamt.Margin = new System.Windows.Forms.Padding(2);
            this.lbltotalremamt.Name = "lbltotalremamt";
            this.lbltotalremamt.Size = new System.Drawing.Size(208, 24);
            this.lbltotalremamt.TabIndex = 47;
            this.lbltotalremamt.Text = "Stock Valuation:";
            this.lbltotalremamt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.label5.Size = new System.Drawing.Size(884, 35);
            this.label5.TabIndex = 11;
            this.label5.Text = "Without GST Remaining Stock";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtProductname
            // 
            this.txtProductname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtProductname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtProductname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProductname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductname.Location = new System.Drawing.Point(108, 37);
            this.txtProductname.Margin = new System.Windows.Forms.Padding(2);
            this.txtProductname.Name = "txtProductname";
            this.txtProductname.Size = new System.Drawing.Size(159, 25);
            this.txtProductname.TabIndex = 1;
            this.txtProductname.TextChanged += new System.EventHandler(this.txtProductname_TextChanged);
            this.txtProductname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductname_KeyDown);
            this.txtProductname.Leave += new System.EventHandler(this.txtProductname_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(271, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 24);
            this.label2.TabIndex = 59;
            this.label2.Text = "Mfg Comapny:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(2, 37);
            this.label11.Margin = new System.Windows.Forms.Padding(2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(102, 24);
            this.label11.TabIndex = 60;
            this.label11.Text = "Product Name:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbGodownName
            // 
            this.cmbGodownName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbGodownName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGodownName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGodownName.ForeColor = System.Drawing.Color.Black;
            this.cmbGodownName.FormattingEnabled = true;
            this.cmbGodownName.Location = new System.Drawing.Point(408, 65);
            this.cmbGodownName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbGodownName.Name = "cmbGodownName";
            this.cmbGodownName.Size = new System.Drawing.Size(155, 25);
            this.cmbGodownName.TabIndex = 4;
            this.cmbGodownName.SelectedIndexChanged += new System.EventHandler(this.cmbGodownName_SelectedIndexChanged);
            this.cmbGodownName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbGodownName_KeyDown);
            // 
            // cmbGroupName
            // 
            this.cmbGroupName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbGroupName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbGroupName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbGroupName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGroupName.ForeColor = System.Drawing.Color.Black;
            this.cmbGroupName.FormattingEnabled = true;
            this.cmbGroupName.Location = new System.Drawing.Point(108, 65);
            this.cmbGroupName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbGroupName.Name = "cmbGroupName";
            this.cmbGroupName.Size = new System.Drawing.Size(159, 25);
            this.cmbGroupName.TabIndex = 3;
            this.cmbGroupName.SelectedIndexChanged += new System.EventHandler(this.cmbGroupName_SelectedIndexChanged);
            this.cmbGroupName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbGroupName_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(271, 65);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 24);
            this.label1.TabIndex = 60;
            this.label1.Text = "Godown Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkDate
            // 
            this.chkDate.AutoSize = true;
            this.chkDate.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDate.ForeColor = System.Drawing.Color.Black;
            this.chkDate.Location = new System.Drawing.Point(50, 93);
            this.chkDate.Margin = new System.Windows.Forms.Padding(2);
            this.chkDate.Name = "chkDate";
            this.chkDate.Size = new System.Drawing.Size(54, 24);
            this.chkDate.TabIndex = 5;
            this.chkDate.Text = "Date";
            this.chkDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDate.UseVisualStyleBackColor = true;
            this.chkDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkDate_KeyDown);
            // 
            // ChkProductType
            // 
            this.ChkProductType.AutoSize = true;
            this.ChkProductType.Dock = System.Windows.Forms.DockStyle.Right;
            this.ChkProductType.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkProductType.ForeColor = System.Drawing.Color.Black;
            this.ChkProductType.Location = new System.Drawing.Point(273, 93);
            this.ChkProductType.Margin = new System.Windows.Forms.Padding(2);
            this.ChkProductType.Name = "ChkProductType";
            this.ChkProductType.Size = new System.Drawing.Size(131, 24);
            this.ChkProductType.TabIndex = 7;
            this.ChkProductType.Text = "Assembly Product";
            this.ChkProductType.UseVisualStyleBackColor = true;
            this.ChkProductType.CheckedChanged += new System.EventHandler(this.ChkProductType_CheckedChanged);
            // 
            // dtpfromdate
            // 
            this.dtpfromdate.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtpfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfromdate.Location = new System.Drawing.Point(108, 93);
            this.dtpfromdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpfromdate.Name = "dtpfromdate";
            this.dtpfromdate.Size = new System.Drawing.Size(115, 25);
            this.dtpfromdate.TabIndex = 6;
            this.dtpfromdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpfromdate_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(2, 65);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 24);
            this.label4.TabIndex = 62;
            this.label4.Text = "Group Name:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.GvPorderInfo.Location = new System.Drawing.Point(3, 123);
            this.GvPorderInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
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
            this.GvPorderInfo.Size = new System.Drawing.Size(878, 385);
            this.GvPorderInfo.TabIndex = 12;
            this.GvPorderInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvPorderInfo_CellClick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(237, 371);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(537, 24);
            this.progressBar1.TabIndex = 174;
            this.progressBar1.Visible = false;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 6;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltMain.Controls.Add(this.chkDate, 0, 3);
            this.tblltMain.Controls.Add(this.lbltotalqty, 5, 2);
            this.tblltMain.Controls.Add(this.cmbGodownName, 3, 2);
            this.tblltMain.Controls.Add(this.lblTotalAmount, 5, 1);
            this.tblltMain.Controls.Add(this.label3, 4, 2);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.dtpfromdate, 1, 3);
            this.tblltMain.Controls.Add(this.ChkProductType, 2, 3);
            this.tblltMain.Controls.Add(this.cmbGroupName, 1, 2);
            this.tblltMain.Controls.Add(this.lbltotalremamt, 4, 1);
            this.tblltMain.Controls.Add(this.GvPorderInfo, 0, 4);
            this.tblltMain.Controls.Add(this.label1, 2, 2);
            this.tblltMain.Controls.Add(this.label11, 0, 1);
            this.tblltMain.Controls.Add(this.cmbcomanyname, 3, 1);
            this.tblltMain.Controls.Add(this.label2, 2, 1);
            this.tblltMain.Controls.Add(this.txtProductname, 1, 1);
            this.tblltMain.Controls.Add(this.label4, 0, 2);
            this.tblltMain.Controls.Add(this.tblltButtons, 3, 3);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 5;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.5F));
            this.tblltMain.Size = new System.Drawing.Size(884, 512);
            this.tblltMain.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 5;
            this.tblltMain.SetColumnSpan(this.tblltButtons, 3);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltButtons.Controls.Add(this.btncheck, 0, 0);
            this.tblltButtons.Controls.Add(this.btnCompoundUnitStock, 1, 0);
            this.tblltButtons.Controls.Add(this.btnGetAll, 2, 0);
            this.tblltButtons.Controls.Add(this.btnClose, 4, 0);
            this.tblltButtons.Controls.Add(this.btnPrint, 3, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(406, 91);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(478, 28);
            this.tblltButtons.TabIndex = 8;
            // 
            // btncheck
            // 
            this.btncheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btncheck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btncheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btncheck.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncheck.ForeColor = System.Drawing.Color.White;
            this.btncheck.Location = new System.Drawing.Point(2, 1);
            this.btncheck.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btncheck.Name = "btncheck";
            this.btncheck.Size = new System.Drawing.Size(67, 26);
            this.btncheck.TabIndex = 8;
            this.btncheck.Text = "Check";
            this.btncheck.UseVisualStyleBackColor = false;
            this.btncheck.Click += new System.EventHandler(this.btncheck_Click);
            // 
            // btnCompoundUnitStock
            // 
            this.btnCompoundUnitStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnCompoundUnitStock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCompoundUnitStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCompoundUnitStock.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompoundUnitStock.ForeColor = System.Drawing.Color.White;
            this.btnCompoundUnitStock.Location = new System.Drawing.Point(73, 1);
            this.btnCompoundUnitStock.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnCompoundUnitStock.Name = "btnCompoundUnitStock";
            this.btnCompoundUnitStock.Size = new System.Drawing.Size(163, 26);
            this.btnCompoundUnitStock.TabIndex = 9;
            this.btnCompoundUnitStock.Text = "Compound Unit";
            this.btnCompoundUnitStock.UseVisualStyleBackColor = false;
            this.btnCompoundUnitStock.Click += new System.EventHandler(this.btnCompoundUnitStock_Click);
            // 
            // btnGetAll
            // 
            this.btnGetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnGetAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGetAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAll.ForeColor = System.Drawing.Color.White;
            this.btnGetAll.Location = new System.Drawing.Point(240, 1);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(91, 26);
            this.btnGetAll.TabIndex = 10;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(406, 1);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 26);
            this.btnClose.TabIndex = 11;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(335, 1);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(67, 26);
            this.btnPrint.TabIndex = 12;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // frmVatRemainingStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(884, 512);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1170, 806);
            this.Name = "frmVatRemainingStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Without GST Remaining Stock";
            this.Load += new System.EventHandler(this.Stock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvPorderInfo)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbcomanyname;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lbltotalremamt;
        private System.Windows.Forms.Label lbltotalqty;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProductname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView GvPorderInfo;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox cmbGodownName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbGroupName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpfromdate;
        private System.Windows.Forms.CheckBox chkDate;
        private System.Windows.Forms.CheckBox ChkProductType;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btncheck;
        private System.Windows.Forms.Button btnCompoundUnitStock;
        private System.Windows.Forms.Button btnGetAll;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnPrint;
    }
}