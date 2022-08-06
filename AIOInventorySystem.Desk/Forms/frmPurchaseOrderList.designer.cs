namespace AIOInventorySystem.Desk.Forms
{
    partial class frmPurchaseOrderList
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
            this.disamt = new System.Windows.Forms.Label();
            this.chkdiscount = new System.Windows.Forms.CheckBox();
            this.lbltoatdisamt = new System.Windows.Forms.Label();
            this.chktranscharge = new System.Windows.Forms.CheckBox();
            this.cmbcashsredit = new System.Windows.Forms.ComboBox();
            this.chkcashcredit = new System.Windows.Forms.CheckBox();
            this.GvPorderInfo = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.cmbsname = new System.Windows.Forms.ComboBox();
            this.chksname = new System.Windows.Forms.CheckBox();
            this.chkbetweendate = new System.Windows.Forms.CheckBox();
            this.chkporderno = new System.Windows.Forms.CheckBox();
            this.dtpToPorderDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFromPorderdate = new System.Windows.Forms.DateTimePicker();
            this.lblTotalBill = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chkfirststock = new System.Windows.Forms.CheckBox();
            this.cmbgroupMaster = new System.Windows.Forms.ComboBox();
            this.chkgroupname = new System.Windows.Forms.CheckBox();
            this.cmbcompany = new System.Windows.Forms.ComboBox();
            this.chkmfgcom = new System.Windows.Forms.CheckBox();
            this.chkproductname = new System.Windows.Forms.CheckBox();
            this.txtproductname = new System.Windows.Forms.TextBox();
            this.chkTaxPurchase = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkMPurRefNo = new System.Windows.Forms.CheckBox();
            this.txtManRefNo = new System.Windows.Forms.TextBox();
            this.cmbOrderBy = new System.Windows.Forms.ComboBox();
            this.chkOrderBy = new System.Windows.Forms.CheckBox();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.tblltLogin = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.label58 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.txtusername = new System.Windows.Forms.TextBox();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblltRow1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtPOrderno = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.tblltRow2 = new System.Windows.Forms.TableLayoutPanel();
            this.tblltRow3 = new System.Windows.Forms.TableLayoutPanel();
            this.tblltRow4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.btntodayslist = new System.Windows.Forms.Button();
            this.rbtnMultipleDeletebills = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GvPorderInfo)).BeginInit();
            this.pnlLogin.SuspendLayout();
            this.tblltLogin.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.tblltRow1.SuspendLayout();
            this.tblltRow2.SuspendLayout();
            this.tblltRow3.SuspendLayout();
            this.tblltRow4.SuspendLayout();
            this.SuspendLayout();
            // 
            // disamt
            // 
            this.disamt.AutoSize = true;
            this.disamt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.disamt.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.disamt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.disamt.Location = new System.Drawing.Point(866, 2);
            this.disamt.Margin = new System.Windows.Forms.Padding(2);
            this.disamt.Name = "disamt";
            this.disamt.Size = new System.Drawing.Size(116, 25);
            this.disamt.TabIndex = 53;
            this.disamt.Text = "0.00";
            this.disamt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.disamt.Visible = false;
            // 
            // chkdiscount
            // 
            this.chkdiscount.AutoSize = true;
            this.chkdiscount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkdiscount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkdiscount.Location = new System.Drawing.Point(901, 2);
            this.chkdiscount.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkdiscount.Name = "chkdiscount";
            this.chkdiscount.Size = new System.Drawing.Size(81, 25);
            this.chkdiscount.TabIndex = 18;
            this.chkdiscount.Text = "Discount";
            this.chkdiscount.UseVisualStyleBackColor = true;
            this.chkdiscount.CheckedChanged += new System.EventHandler(this.chkdiscount_CheckedChanged);
            this.chkdiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkdiscount_KeyDown);
            // 
            // lbltoatdisamt
            // 
            this.lbltoatdisamt.AutoSize = true;
            this.lbltoatdisamt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltoatdisamt.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltoatdisamt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbltoatdisamt.Location = new System.Drawing.Point(689, 2);
            this.lbltoatdisamt.Margin = new System.Windows.Forms.Padding(2);
            this.lbltoatdisamt.Name = "lbltoatdisamt";
            this.lbltoatdisamt.Size = new System.Drawing.Size(173, 25);
            this.lbltoatdisamt.TabIndex = 52;
            this.lbltoatdisamt.Text = "Total Purchase Amount:";
            this.lbltoatdisamt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbltoatdisamt.Visible = false;
            // 
            // chktranscharge
            // 
            this.chktranscharge.AutoSize = true;
            this.chktranscharge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chktranscharge.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chktranscharge.Location = new System.Drawing.Point(4, 2);
            this.chktranscharge.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chktranscharge.Name = "chktranscharge";
            this.chktranscharge.Size = new System.Drawing.Size(126, 25);
            this.chktranscharge.TabIndex = 19;
            this.chktranscharge.Text = "Transport chrg";
            this.chktranscharge.UseVisualStyleBackColor = true;
            this.chktranscharge.CheckedChanged += new System.EventHandler(this.chktranscharge_CheckedChanged);
            this.chktranscharge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chktranscharge_KeyDown);
            // 
            // cmbcashsredit
            // 
            this.cmbcashsredit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbcashsredit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcashsredit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcashsredit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcashsredit.FormattingEnabled = true;
            this.cmbcashsredit.Location = new System.Drawing.Point(791, 2);
            this.cmbcashsredit.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcashsredit.Name = "cmbcashsredit";
            this.cmbcashsredit.Size = new System.Drawing.Size(104, 25);
            this.cmbcashsredit.TabIndex = 17;
            this.cmbcashsredit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcashsredit_KeyDown);
            // 
            // chkcashcredit
            // 
            this.chkcashcredit.AutoSize = true;
            this.chkcashcredit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkcashcredit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcashcredit.Location = new System.Drawing.Point(725, 2);
            this.chkcashcredit.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkcashcredit.Name = "chkcashcredit";
            this.chkcashcredit.Size = new System.Drawing.Size(62, 25);
            this.chkcashcredit.TabIndex = 16;
            this.chkcashcredit.Text = "Mode";
            this.chkcashcredit.UseVisualStyleBackColor = true;
            this.chkcashcredit.CheckedChanged += new System.EventHandler(this.chkcashcredit_CheckedChanged);
            this.chkcashcredit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkcashcredit_KeyDown);
            // 
            // GvPorderInfo
            // 
            this.GvPorderInfo.AllowUserToAddRows = false;
            this.GvPorderInfo.AllowUserToDeleteRows = false;
            this.GvPorderInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvPorderInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvPorderInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvPorderInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvPorderInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvPorderInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GvPorderInfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.GvPorderInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvPorderInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvPorderInfo.Location = new System.Drawing.Point(3, 154);
            this.GvPorderInfo.Name = "GvPorderInfo";
            this.GvPorderInfo.RowHeadersWidth = 15;
            this.GvPorderInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvPorderInfo.Size = new System.Drawing.Size(978, 435);
            this.GvPorderInfo.TabIndex = 30;
            this.GvPorderInfo.TabStop = false;
            this.GvPorderInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvPorderInfo_CellClick);
            this.GvPorderInfo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GvPorderInfo_KeyPress);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 70.12343F;
            this.Column1.HeaderText = "Select Bill";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.FillWeight = 47.94764F;
            this.Column2.HeaderText = "Update";
            this.Column2.Image = global::AIOInventorySystem.Desk.Properties.Resources.Update;
            this.Column2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Column2.Name = "Column2";
            // 
            // cmbsname
            // 
            this.cmbsname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbsname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbsname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbsname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbsname.FormattingEnabled = true;
            this.cmbsname.Location = new System.Drawing.Point(129, 2);
            this.cmbsname.Margin = new System.Windows.Forms.Padding(2);
            this.cmbsname.Name = "cmbsname";
            this.cmbsname.Size = new System.Drawing.Size(217, 25);
            this.cmbsname.TabIndex = 9;
            this.cmbsname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbsname_KeyDown);
            this.cmbsname.Leave += new System.EventHandler(this.cmbsname_Leave);
            // 
            // chksname
            // 
            this.chksname.AutoSize = true;
            this.chksname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chksname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chksname.Location = new System.Drawing.Point(4, 2);
            this.chksname.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chksname.Name = "chksname";
            this.chksname.Size = new System.Drawing.Size(121, 25);
            this.chksname.TabIndex = 8;
            this.chksname.Text = "Supplier Name";
            this.chksname.UseVisualStyleBackColor = true;
            this.chksname.CheckedChanged += new System.EventHandler(this.chksname_CheckedChanged);
            this.chksname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chksname_KeyDown);
            // 
            // chkbetweendate
            // 
            this.chkbetweendate.AutoSize = true;
            this.chkbetweendate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkbetweendate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbetweendate.Location = new System.Drawing.Point(445, 2);
            this.chkbetweendate.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkbetweendate.Name = "chkbetweendate";
            this.chkbetweendate.Size = new System.Drawing.Size(92, 25);
            this.chkbetweendate.TabIndex = 5;
            this.chkbetweendate.Text = "From Date";
            this.chkbetweendate.UseVisualStyleBackColor = true;
            this.chkbetweendate.CheckedChanged += new System.EventHandler(this.chkbetweendate_CheckedChanged);
            this.chkbetweendate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkbetweendate_KeyDown);
            // 
            // chkporderno
            // 
            this.chkporderno.AutoSize = true;
            this.chkporderno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkporderno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkporderno.Location = new System.Drawing.Point(4, 2);
            this.chkporderno.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkporderno.Name = "chkporderno";
            this.chkporderno.Size = new System.Drawing.Size(102, 25);
            this.chkporderno.TabIndex = 1;
            this.chkporderno.Text = "POrder No";
            this.chkporderno.UseVisualStyleBackColor = true;
            this.chkporderno.CheckedChanged += new System.EventHandler(this.chkporderno_CheckedChanged);
            this.chkporderno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkporderno_KeyDown);
            // 
            // dtpToPorderDate
            // 
            this.dtpToPorderDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpToPorderDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToPorderDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToPorderDate.Location = new System.Drawing.Point(698, 2);
            this.dtpToPorderDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpToPorderDate.Name = "dtpToPorderDate";
            this.dtpToPorderDate.Size = new System.Drawing.Size(114, 25);
            this.dtpToPorderDate.TabIndex = 7;
            this.dtpToPorderDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpToPorderDate_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(659, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "To";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpFromPorderdate
            // 
            this.dtpFromPorderdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFromPorderdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromPorderdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromPorderdate.Location = new System.Drawing.Point(541, 2);
            this.dtpFromPorderdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFromPorderdate.Name = "dtpFromPorderdate";
            this.dtpFromPorderdate.Size = new System.Drawing.Size(114, 25);
            this.dtpFromPorderdate.TabIndex = 6;
            this.dtpFromPorderdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpFromPorderdate_KeyDown);
            // 
            // lblTotalBill
            // 
            this.lblTotalBill.AutoSize = true;
            this.lblTotalBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalBill.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBill.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalBill.Location = new System.Drawing.Point(875, 2);
            this.lblTotalBill.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotalBill.Name = "lblTotalBill";
            this.lblTotalBill.Size = new System.Drawing.Size(107, 25);
            this.lblTotalBill.TabIndex = 91;
            this.lblTotalBill.Text = "0";
            this.lblTotalBill.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(816, 2);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 25);
            this.label8.TabIndex = 90;
            this.label8.Text = "Bills:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkfirststock
            // 
            this.chkfirststock.AutoSize = true;
            this.chkfirststock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkfirststock.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkfirststock.Location = new System.Drawing.Point(136, 2);
            this.chkfirststock.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkfirststock.Name = "chkfirststock";
            this.chkfirststock.Size = new System.Drawing.Size(94, 25);
            this.chkfirststock.TabIndex = 20;
            this.chkfirststock.Text = "First Stock";
            this.chkfirststock.UseVisualStyleBackColor = true;
            this.chkfirststock.CheckedChanged += new System.EventHandler(this.chkfirststock_CheckedChanged);
            this.chkfirststock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkfirststock_KeyDown);
            // 
            // cmbgroupMaster
            // 
            this.cmbgroupMaster.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbgroupMaster.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbgroupMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbgroupMaster.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbgroupMaster.ForeColor = System.Drawing.Color.Black;
            this.cmbgroupMaster.FormattingEnabled = true;
            this.cmbgroupMaster.Location = new System.Drawing.Point(468, 2);
            this.cmbgroupMaster.Margin = new System.Windows.Forms.Padding(2);
            this.cmbgroupMaster.Name = "cmbgroupMaster";
            this.cmbgroupMaster.Size = new System.Drawing.Size(217, 25);
            this.cmbgroupMaster.TabIndex = 11;
            this.cmbgroupMaster.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbgroupMaster_KeyDown);
            // 
            // chkgroupname
            // 
            this.chkgroupname.AutoSize = true;
            this.chkgroupname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkgroupname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkgroupname.Location = new System.Drawing.Point(352, 2);
            this.chkgroupname.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkgroupname.Name = "chkgroupname";
            this.chkgroupname.Size = new System.Drawing.Size(112, 25);
            this.chkgroupname.TabIndex = 10;
            this.chkgroupname.Text = "Group Name";
            this.chkgroupname.UseVisualStyleBackColor = true;
            this.chkgroupname.CheckedChanged += new System.EventHandler(this.chkgroupname_CheckedChanged);
            this.chkgroupname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkgroupname_KeyDown);
            // 
            // cmbcompany
            // 
            this.cmbcompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcompany.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcompany.ForeColor = System.Drawing.Color.Black;
            this.cmbcompany.FormattingEnabled = true;
            this.cmbcompany.Location = new System.Drawing.Point(566, 2);
            this.cmbcompany.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcompany.Name = "cmbcompany";
            this.cmbcompany.Size = new System.Drawing.Size(153, 25);
            this.cmbcompany.TabIndex = 15;
            this.cmbcompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcompany_KeyDown);
            this.cmbcompany.Leave += new System.EventHandler(this.cmbcompany_Leave);
            // 
            // chkmfgcom
            // 
            this.chkmfgcom.AutoSize = true;
            this.chkmfgcom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkmfgcom.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkmfgcom.Location = new System.Drawing.Point(431, 2);
            this.chkmfgcom.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkmfgcom.Name = "chkmfgcom";
            this.chkmfgcom.Size = new System.Drawing.Size(131, 25);
            this.chkmfgcom.TabIndex = 14;
            this.chkmfgcom.Text = "Mfg. Com. Name";
            this.chkmfgcom.UseVisualStyleBackColor = true;
            this.chkmfgcom.CheckedChanged += new System.EventHandler(this.chkmfgcom_CheckedChanged);
            this.chkmfgcom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkmfgcom_KeyDown);
            // 
            // chkproductname
            // 
            this.chkproductname.AutoSize = true;
            this.chkproductname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkproductname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkproductname.Location = new System.Drawing.Point(4, 2);
            this.chkproductname.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkproductname.Name = "chkproductname";
            this.chkproductname.Size = new System.Drawing.Size(112, 25);
            this.chkproductname.TabIndex = 12;
            this.chkproductname.Text = "Product Name";
            this.chkproductname.UseVisualStyleBackColor = true;
            this.chkproductname.CheckedChanged += new System.EventHandler(this.chkproductname_CheckedChanged);
            this.chkproductname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkproductname_KeyDown);
            // 
            // txtproductname
            // 
            this.txtproductname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtproductname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtproductname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtproductname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtproductname.Location = new System.Drawing.Point(120, 2);
            this.txtproductname.Margin = new System.Windows.Forms.Padding(2);
            this.txtproductname.Name = "txtproductname";
            this.txtproductname.Size = new System.Drawing.Size(305, 25);
            this.txtproductname.TabIndex = 13;
            this.txtproductname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtproductname_KeyDown);
            // 
            // chkTaxPurchase
            // 
            this.chkTaxPurchase.AutoSize = true;
            this.chkTaxPurchase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkTaxPurchase.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTaxPurchase.Location = new System.Drawing.Point(236, 2);
            this.chkTaxPurchase.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkTaxPurchase.Name = "chkTaxPurchase";
            this.chkTaxPurchase.Size = new System.Drawing.Size(52, 25);
            this.chkTaxPurchase.TabIndex = 21;
            this.chkTaxPurchase.Text = "TPB";
            this.chkTaxPurchase.UseVisualStyleBackColor = true;
            this.chkTaxPurchase.CheckedChanged += new System.EventHandler(this.chkTaxPurchase_CheckedChanged);
            this.chkTaxPurchase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkTaxPurchase_KeyDown);
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
            this.label5.Size = new System.Drawing.Size(984, 35);
            this.label5.TabIndex = 11;
            this.label5.Text = "Purchase Bill List";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkMPurRefNo
            // 
            this.chkMPurRefNo.AutoSize = true;
            this.chkMPurRefNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkMPurRefNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMPurRefNo.Location = new System.Drawing.Point(210, 2);
            this.chkMPurRefNo.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkMPurRefNo.Name = "chkMPurRefNo";
            this.chkMPurRefNo.Size = new System.Drawing.Size(121, 25);
            this.chkMPurRefNo.TabIndex = 3;
            this.chkMPurRefNo.Text = "M. Pur. Ref. No";
            this.chkMPurRefNo.UseVisualStyleBackColor = true;
            // 
            // txtManRefNo
            // 
            this.txtManRefNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtManRefNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtManRefNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtManRefNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtManRefNo.Location = new System.Drawing.Point(335, 2);
            this.txtManRefNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtManRefNo.Name = "txtManRefNo";
            this.txtManRefNo.Size = new System.Drawing.Size(104, 25);
            this.txtManRefNo.TabIndex = 4;
            this.txtManRefNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtManRefNo_KeyDown);
            // 
            // cmbOrderBy
            // 
            this.cmbOrderBy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbOrderBy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbOrderBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbOrderBy.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOrderBy.ForeColor = System.Drawing.Color.Black;
            this.cmbOrderBy.FormattingEnabled = true;
            this.cmbOrderBy.Items.AddRange(new object[] {
            "POrderNo",
            "PDate",
            "SupplierName"});
            this.cmbOrderBy.Location = new System.Drawing.Point(381, 2);
            this.cmbOrderBy.Margin = new System.Windows.Forms.Padding(2);
            this.cmbOrderBy.Name = "cmbOrderBy";
            this.cmbOrderBy.Size = new System.Drawing.Size(122, 25);
            this.cmbOrderBy.TabIndex = 23;
            // 
            // chkOrderBy
            // 
            this.chkOrderBy.AutoSize = true;
            this.chkOrderBy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkOrderBy.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkOrderBy.Location = new System.Drawing.Point(294, 2);
            this.chkOrderBy.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkOrderBy.Name = "chkOrderBy";
            this.chkOrderBy.Size = new System.Drawing.Size(83, 25);
            this.chkOrderBy.TabIndex = 22;
            this.chkOrderBy.Text = "OrderBy";
            this.chkOrderBy.UseVisualStyleBackColor = true;
            // 
            // pnlLogin
            // 
            this.pnlLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlLogin.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlLogin.Controls.Add(this.tblltLogin);
            this.pnlLogin.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlLogin.Location = new System.Drawing.Point(359, 184);
            this.pnlLogin.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(280, 110);
            this.pnlLogin.TabIndex = 32;
            this.pnlLogin.Visible = false;
            // 
            // tblltLogin
            // 
            this.tblltLogin.ColumnCount = 3;
            this.tblltLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.02778F));
            this.tblltLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.90278F));
            this.tblltLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltLogin.Controls.Add(this.tblltButtons, 0, 3);
            this.tblltLogin.Controls.Add(this.txtpassword, 1, 2);
            this.tblltLogin.Controls.Add(this.label58, 0, 1);
            this.tblltLogin.Controls.Add(this.label56, 0, 2);
            this.tblltLogin.Controls.Add(this.txtusername, 1, 1);
            this.tblltLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltLogin.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblltLogin.Location = new System.Drawing.Point(0, 0);
            this.tblltLogin.Margin = new System.Windows.Forms.Padding(0);
            this.tblltLogin.Name = "tblltLogin";
            this.tblltLogin.RowCount = 4;
            this.tblltLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltLogin.Size = new System.Drawing.Size(280, 110);
            this.tblltLogin.TabIndex = 32;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 4;
            this.tblltLogin.SetColumnSpan(this.tblltButtons, 3);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltButtons.Controls.Add(this.btnLogin, 1, 0);
            this.tblltButtons.Controls.Add(this.btnClose, 2, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblltButtons.Location = new System.Drawing.Point(0, 76);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(280, 34);
            this.tblltButtons.TabIndex = 34;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(58, 2);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(80, 30);
            this.btnLogin.TabIndex = 34;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(142, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 30);
            this.btnClose.TabIndex = 35;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtpassword
            // 
            this.txtpassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtpassword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpassword.Location = new System.Drawing.Point(97, 51);
            this.txtpassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.Size = new System.Drawing.Size(152, 25);
            this.txtpassword.TabIndex = 33;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label58.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.ForeColor = System.Drawing.Color.White;
            this.label58.Location = new System.Drawing.Point(2, 24);
            this.label58.Margin = new System.Windows.Forms.Padding(2);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(91, 23);
            this.label58.TabIndex = 0;
            this.label58.Text = "User Name:";
            this.label58.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label56.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.ForeColor = System.Drawing.Color.White;
            this.label56.Location = new System.Drawing.Point(2, 51);
            this.label56.Margin = new System.Windows.Forms.Padding(2);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(91, 23);
            this.label56.TabIndex = 1;
            this.label56.Text = "Password:";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtusername
            // 
            this.txtusername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtusername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtusername.Location = new System.Drawing.Point(97, 24);
            this.txtusername.Margin = new System.Windows.Forms.Padding(2);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(152, 25);
            this.txtusername.TabIndex = 32;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.GvPorderInfo, 0, 5);
            this.tblltMain.Controls.Add(this.tblltRow1, 0, 1);
            this.tblltMain.Controls.Add(this.tblltRow2, 0, 2);
            this.tblltMain.Controls.Add(this.tblltRow3, 0, 3);
            this.tblltMain.Controls.Add(this.tblltRow4, 0, 4);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 6;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 74F));
            this.tblltMain.Size = new System.Drawing.Size(984, 592);
            this.tblltMain.TabIndex = 0;
            // 
            // tblltRow1
            // 
            this.tblltRow1.ColumnCount = 10;
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltRow1.Controls.Add(this.lblTotalBill, 9, 0);
            this.tblltRow1.Controls.Add(this.dtpToPorderDate, 7, 0);
            this.tblltRow1.Controls.Add(this.label8, 8, 0);
            this.tblltRow1.Controls.Add(this.chkporderno, 0, 0);
            this.tblltRow1.Controls.Add(this.label2, 6, 0);
            this.tblltRow1.Controls.Add(this.txtPOrderno, 1, 0);
            this.tblltRow1.Controls.Add(this.dtpFromPorderdate, 5, 0);
            this.tblltRow1.Controls.Add(this.txtManRefNo, 3, 0);
            this.tblltRow1.Controls.Add(this.chkbetweendate, 4, 0);
            this.tblltRow1.Controls.Add(this.chkMPurRefNo, 2, 0);
            this.tblltRow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblltRow1.Location = new System.Drawing.Point(0, 35);
            this.tblltRow1.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow1.Name = "tblltRow1";
            this.tblltRow1.RowCount = 1;
            this.tblltRow1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow1.Size = new System.Drawing.Size(984, 29);
            this.tblltRow1.TabIndex = 0;
            // 
            // txtPOrderno
            // 
            this.txtPOrderno.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtPOrderno.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtPOrderno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPOrderno.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPOrderno.Location = new System.Drawing.Point(110, 2);
            this.txtPOrderno.Margin = new System.Windows.Forms.Padding(2);
            this.txtPOrderno.Name = "txtPOrderno";
            this.txtPOrderno.Size = new System.Drawing.Size(94, 25);
            this.txtPOrderno.TabIndex = 2;
            this.txtPOrderno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPOrderno_KeyDown);
            // 
            // tblltRow2
            // 
            this.tblltRow2.ColumnCount = 6;
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltRow2.Controls.Add(this.disamt, 5, 0);
            this.tblltRow2.Controls.Add(this.cmbgroupMaster, 3, 0);
            this.tblltRow2.Controls.Add(this.lbltoatdisamt, 4, 0);
            this.tblltRow2.Controls.Add(this.chksname, 0, 0);
            this.tblltRow2.Controls.Add(this.chkgroupname, 2, 0);
            this.tblltRow2.Controls.Add(this.cmbsname, 1, 0);
            this.tblltRow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblltRow2.Location = new System.Drawing.Point(0, 64);
            this.tblltRow2.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow2.Name = "tblltRow2";
            this.tblltRow2.RowCount = 1;
            this.tblltRow2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow2.Size = new System.Drawing.Size(984, 29);
            this.tblltRow2.TabIndex = 8;
            // 
            // tblltRow3
            // 
            this.tblltRow3.ColumnCount = 7;
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.5F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.5F));
            this.tblltRow3.Controls.Add(this.cmbcompany, 3, 0);
            this.tblltRow3.Controls.Add(this.chkdiscount, 6, 0);
            this.tblltRow3.Controls.Add(this.chkproductname, 0, 0);
            this.tblltRow3.Controls.Add(this.chkmfgcom, 2, 0);
            this.tblltRow3.Controls.Add(this.cmbcashsredit, 5, 0);
            this.tblltRow3.Controls.Add(this.txtproductname, 1, 0);
            this.tblltRow3.Controls.Add(this.chkcashcredit, 4, 0);
            this.tblltRow3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblltRow3.Location = new System.Drawing.Point(0, 93);
            this.tblltRow3.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow3.Name = "tblltRow3";
            this.tblltRow3.RowCount = 1;
            this.tblltRow3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow3.Size = new System.Drawing.Size(984, 29);
            this.tblltRow3.TabIndex = 12;
            // 
            // tblltRow4
            // 
            this.tblltRow4.ColumnCount = 10;
            this.tblltRow4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltRow4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.5F));
            this.tblltRow4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltRow4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.5F));
            this.tblltRow4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltRow4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltRow4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltRow4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltRow4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltRow4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltRow4.Controls.Add(this.btnSearch, 5, 0);
            this.tblltRow4.Controls.Add(this.btnGetAll, 6, 0);
            this.tblltRow4.Controls.Add(this.chktranscharge, 0, 0);
            this.tblltRow4.Controls.Add(this.cmbOrderBy, 4, 0);
            this.tblltRow4.Controls.Add(this.chkTaxPurchase, 2, 0);
            this.tblltRow4.Controls.Add(this.btntodayslist, 7, 0);
            this.tblltRow4.Controls.Add(this.chkOrderBy, 3, 0);
            this.tblltRow4.Controls.Add(this.rbtnMultipleDeletebills, 8, 0);
            this.tblltRow4.Controls.Add(this.chkfirststock, 1, 0);
            this.tblltRow4.Controls.Add(this.btnprint, 9, 0);
            this.tblltRow4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblltRow4.Location = new System.Drawing.Point(0, 122);
            this.tblltRow4.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow4.Name = "tblltRow4";
            this.tblltRow4.RowCount = 1;
            this.tblltRow4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow4.Size = new System.Drawing.Size(984, 29);
            this.tblltRow4.TabIndex = 19;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(507, 1);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(70, 27);
            this.btnSearch.TabIndex = 24;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnGetAll
            // 
            this.btnGetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnGetAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGetAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAll.ForeColor = System.Drawing.Color.White;
            this.btnGetAll.Location = new System.Drawing.Point(581, 1);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(70, 27);
            this.btnGetAll.TabIndex = 25;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // btntodayslist
            // 
            this.btntodayslist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btntodayslist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btntodayslist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btntodayslist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntodayslist.ForeColor = System.Drawing.Color.White;
            this.btntodayslist.Location = new System.Drawing.Point(655, 1);
            this.btntodayslist.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btntodayslist.Name = "btntodayslist";
            this.btntodayslist.Size = new System.Drawing.Size(101, 27);
            this.btntodayslist.TabIndex = 26;
            this.btntodayslist.Text = "Today\'s List";
            this.btntodayslist.UseVisualStyleBackColor = false;
            this.btntodayslist.Click += new System.EventHandler(this.btntodayslist_Click);
            // 
            // rbtnMultipleDeletebills
            // 
            this.rbtnMultipleDeletebills.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.rbtnMultipleDeletebills.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.rbtnMultipleDeletebills.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtnMultipleDeletebills.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnMultipleDeletebills.ForeColor = System.Drawing.Color.White;
            this.rbtnMultipleDeletebills.Location = new System.Drawing.Point(760, 1);
            this.rbtnMultipleDeletebills.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.rbtnMultipleDeletebills.Name = "rbtnMultipleDeletebills";
            this.rbtnMultipleDeletebills.Size = new System.Drawing.Size(154, 27);
            this.rbtnMultipleDeletebills.TabIndex = 27;
            this.rbtnMultipleDeletebills.Text = "Delete Multiple Bills";
            this.rbtnMultipleDeletebills.UseVisualStyleBackColor = false;
            this.rbtnMultipleDeletebills.Click += new System.EventHandler(this.rbtnMultipleDeletebills_Click);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(918, 1);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(64, 27);
            this.btnprint.TabIndex = 28;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // frmPurchaseOrderList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(984, 592);
            this.Controls.Add(this.pnlLogin);
            this.Controls.Add(this.tblltMain);
            this.MaximizeBox = false;
            this.Name = "frmPurchaseOrderList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Purchase Bill List";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPurchaseOrderList_FormClosed);
            this.Load += new System.EventHandler(this.frmPurchaseOrderList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvPorderInfo)).EndInit();
            this.pnlLogin.ResumeLayout(false);
            this.tblltLogin.ResumeLayout(false);
            this.tblltLogin.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.tblltMain.ResumeLayout(false);
            this.tblltRow1.ResumeLayout(false);
            this.tblltRow1.PerformLayout();
            this.tblltRow2.ResumeLayout(false);
            this.tblltRow2.PerformLayout();
            this.tblltRow3.ResumeLayout(false);
            this.tblltRow3.PerformLayout();
            this.tblltRow4.ResumeLayout(false);
            this.tblltRow4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkbetweendate;
        private System.Windows.Forms.CheckBox chkporderno;
        private System.Windows.Forms.DateTimePicker dtpToPorderDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFromPorderdate;
        private System.Windows.Forms.CheckBox chksname;
        private System.Windows.Forms.ComboBox cmbsname;
        private System.Windows.Forms.DataGridView GvPorderInfo;
        private System.Windows.Forms.ComboBox cmbcashsredit;
        private System.Windows.Forms.CheckBox chkcashcredit;
        private System.Windows.Forms.CheckBox chktranscharge;
        private System.Windows.Forms.Label disamt;
        private System.Windows.Forms.CheckBox chkdiscount;
        private System.Windows.Forms.Label lbltoatdisamt;
        private System.Windows.Forms.Label lblTotalBill;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkgroupname;
        private System.Windows.Forms.ComboBox cmbgroupMaster;
        private System.Windows.Forms.CheckBox chkproductname;
        private System.Windows.Forms.ComboBox cmbcompany;
        private System.Windows.Forms.CheckBox chkmfgcom;
        private System.Windows.Forms.TextBox txtproductname;
        private System.Windows.Forms.CheckBox chkfirststock;
        private System.Windows.Forms.CheckBox chkTaxPurchase;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewImageColumn Column2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbOrderBy;
        private System.Windows.Forms.CheckBox chkOrderBy;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.CheckBox chkMPurRefNo;
        private System.Windows.Forms.TextBox txtManRefNo;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltRow1;
        private System.Windows.Forms.TableLayoutPanel tblltRow2;
        private System.Windows.Forms.TableLayoutPanel tblltRow3;
        private System.Windows.Forms.TableLayoutPanel tblltRow4;
        private RachitControls.NumericTextBox txtPOrderno;
        private System.Windows.Forms.Button btntodayslist;
        private System.Windows.Forms.Button rbtnMultipleDeletebills;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button btnGetAll;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TableLayoutPanel tblltLogin;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
    }
}