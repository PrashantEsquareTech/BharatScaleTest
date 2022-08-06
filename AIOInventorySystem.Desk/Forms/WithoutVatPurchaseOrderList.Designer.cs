namespace AIOInventorySystem.Desk.Forms
{
    partial class WithoutVatPurchaseOrderList
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
            this.cmbcompany = new System.Windows.Forms.ComboBox();
            this.chkmfgcom = new System.Windows.Forms.CheckBox();
            this.chkproductname = new System.Windows.Forms.CheckBox();
            this.txtproductname = new System.Windows.Forms.TextBox();
            this.cmbgroupMaster = new System.Windows.Forms.ComboBox();
            this.chksname = new System.Windows.Forms.CheckBox();
            this.chkgroupname = new System.Windows.Forms.CheckBox();
            this.cmbsname = new System.Windows.Forms.ComboBox();
            this.lblTotalBill = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chkMRefNo = new System.Windows.Forms.CheckBox();
            this.txtManRefNo = new System.Windows.Forms.TextBox();
            this.chkfirststock = new System.Windows.Forms.CheckBox();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lbltotalremamt = new System.Windows.Forms.Label();
            this.chkbetweendate = new System.Windows.Forms.CheckBox();
            this.dtpFromPorderdate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpToPorderDate = new System.Windows.Forms.DateTimePicker();
            this.chktranscharge = new System.Windows.Forms.CheckBox();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btntodayslist = new System.Windows.Forms.Button();
            this.chkporderno = new System.Windows.Forms.CheckBox();
            this.chkcashcredit = new System.Windows.Forms.CheckBox();
            this.cmbcashsredit = new System.Windows.Forms.ComboBox();
            this.chkdiscount = new System.Windows.Forms.CheckBox();
            this.disamt = new System.Windows.Forms.Label();
            this.lbltoatdisamt = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.GvPorderInfo = new System.Windows.Forms.DataGridView();
            this.Updateg = new System.Windows.Forms.DataGridViewImageColumn();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblltRow1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtPOrderno = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.tblltRow2 = new System.Windows.Forms.TableLayoutPanel();
            this.tblltRow3 = new System.Windows.Forms.TableLayoutPanel();
            this.tblltTotalls = new System.Windows.Forms.TableLayoutPanel();
            this.tblltLogin = new System.Windows.Forms.TableLayoutPanel();
            this.tblltLoginButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.label58 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.txtusername = new System.Windows.Forms.TextBox();
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.tblltButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvPorderInfo)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.tblltRow1.SuspendLayout();
            this.tblltRow2.SuspendLayout();
            this.tblltRow3.SuspendLayout();
            this.tblltTotalls.SuspendLayout();
            this.tblltLogin.SuspendLayout();
            this.tblltLoginButtons.SuspendLayout();
            this.pnlLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbcompany
            // 
            this.cmbcompany.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcompany.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcompany.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcompany.ForeColor = System.Drawing.Color.Black;
            this.cmbcompany.FormattingEnabled = true;
            this.cmbcompany.Location = new System.Drawing.Point(741, 2);
            this.cmbcompany.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcompany.Name = "cmbcompany";
            this.cmbcompany.Size = new System.Drawing.Size(141, 25);
            this.cmbcompany.TabIndex = 15;
            this.cmbcompany.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcompany_KeyDown);
            // 
            // chkmfgcom
            // 
            this.chkmfgcom.AutoSize = true;
            this.chkmfgcom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkmfgcom.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkmfgcom.Location = new System.Drawing.Point(602, 2);
            this.chkmfgcom.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkmfgcom.Name = "chkmfgcom";
            this.chkmfgcom.Size = new System.Drawing.Size(135, 25);
            this.chkmfgcom.TabIndex = 14;
            this.chkmfgcom.Text = "Mfg. Com. Name";
            this.chkmfgcom.UseVisualStyleBackColor = true;
            this.chkmfgcom.CheckedChanged += new System.EventHandler(this.chkmfgcom_CheckedChanged);
            // 
            // chkproductname
            // 
            this.chkproductname.AutoSize = true;
            this.chkproductname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkproductname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkproductname.Location = new System.Drawing.Point(303, 2);
            this.chkproductname.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkproductname.Name = "chkproductname";
            this.chkproductname.Size = new System.Drawing.Size(117, 25);
            this.chkproductname.TabIndex = 12;
            this.chkproductname.Text = "Product Name";
            this.chkproductname.UseVisualStyleBackColor = true;
            this.chkproductname.CheckedChanged += new System.EventHandler(this.chkproductname_CheckedChanged);
            // 
            // txtproductname
            // 
            this.txtproductname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtproductname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtproductname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtproductname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtproductname.Location = new System.Drawing.Point(424, 2);
            this.txtproductname.Margin = new System.Windows.Forms.Padding(2);
            this.txtproductname.Name = "txtproductname";
            this.txtproductname.Size = new System.Drawing.Size(172, 25);
            this.txtproductname.TabIndex = 13;
            this.txtproductname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtproductname_KeyDown);
            this.txtproductname.Leave += new System.EventHandler(this.txtproductname_Leave);
            // 
            // cmbgroupMaster
            // 
            this.cmbgroupMaster.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbgroupMaster.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbgroupMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbgroupMaster.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbgroupMaster.ForeColor = System.Drawing.Color.Black;
            this.cmbgroupMaster.FormattingEnabled = true;
            this.cmbgroupMaster.Location = new System.Drawing.Point(112, 2);
            this.cmbgroupMaster.Margin = new System.Windows.Forms.Padding(2);
            this.cmbgroupMaster.Name = "cmbgroupMaster";
            this.cmbgroupMaster.Size = new System.Drawing.Size(110, 25);
            this.cmbgroupMaster.TabIndex = 17;
            this.cmbgroupMaster.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbgroupMaster_KeyDown);
            // 
            // chksname
            // 
            this.chksname.AutoSize = true;
            this.chksname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chksname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chksname.Location = new System.Drawing.Point(4, 2);
            this.chksname.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chksname.Name = "chksname";
            this.chksname.Size = new System.Drawing.Size(117, 25);
            this.chksname.TabIndex = 10;
            this.chksname.Text = "Supplier Name";
            this.chksname.UseVisualStyleBackColor = true;
            this.chksname.CheckedChanged += new System.EventHandler(this.chksname_CheckedChanged);
            // 
            // chkgroupname
            // 
            this.chkgroupname.AutoSize = true;
            this.chkgroupname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkgroupname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkgroupname.Location = new System.Drawing.Point(4, 2);
            this.chkgroupname.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkgroupname.Name = "chkgroupname";
            this.chkgroupname.Size = new System.Drawing.Size(104, 25);
            this.chkgroupname.TabIndex = 16;
            this.chkgroupname.Text = "Group Name";
            this.chkgroupname.UseVisualStyleBackColor = true;
            this.chkgroupname.CheckedChanged += new System.EventHandler(this.chkgroupname_CheckedChanged);
            // 
            // cmbsname
            // 
            this.cmbsname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbsname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbsname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbsname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbsname.FormattingEnabled = true;
            this.cmbsname.Location = new System.Drawing.Point(125, 2);
            this.cmbsname.Margin = new System.Windows.Forms.Padding(2);
            this.cmbsname.Name = "cmbsname";
            this.cmbsname.Size = new System.Drawing.Size(172, 25);
            this.cmbsname.TabIndex = 11;
            this.cmbsname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbsname_KeyDown);
            // 
            // lblTotalBill
            // 
            this.lblTotalBill.AutoSize = true;
            this.lblTotalBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalBill.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalBill.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalBill.Location = new System.Drawing.Point(125, 2);
            this.lblTotalBill.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotalBill.Name = "lblTotalBill";
            this.lblTotalBill.Size = new System.Drawing.Size(119, 29);
            this.lblTotalBill.TabIndex = 91;
            this.lblTotalBill.Text = "0";
            this.lblTotalBill.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(2, 2);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 29);
            this.label8.TabIndex = 90;
            this.label8.Text = "Total Bills:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkMRefNo
            // 
            this.chkMRefNo.AutoSize = true;
            this.chkMRefNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkMRefNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMRefNo.Location = new System.Drawing.Point(550, 2);
            this.chkMRefNo.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkMRefNo.Name = "chkMRefNo";
            this.chkMRefNo.Size = new System.Drawing.Size(100, 25);
            this.chkMRefNo.TabIndex = 6;
            this.chkMRefNo.Text = "M P. Ref No";
            this.chkMRefNo.UseVisualStyleBackColor = true;
            // 
            // txtManRefNo
            // 
            this.txtManRefNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtManRefNo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtManRefNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtManRefNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtManRefNo.Location = new System.Drawing.Point(654, 2);
            this.txtManRefNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtManRefNo.Name = "txtManRefNo";
            this.txtManRefNo.Size = new System.Drawing.Size(66, 25);
            this.txtManRefNo.TabIndex = 7;
            this.txtManRefNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtManRefNo_KeyDown);
            // 
            // chkfirststock
            // 
            this.chkfirststock.AutoSize = true;
            this.chkfirststock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkfirststock.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkfirststock.Location = new System.Drawing.Point(444, 2);
            this.chkfirststock.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkfirststock.Name = "chkfirststock";
            this.chkfirststock.Size = new System.Drawing.Size(91, 25);
            this.chkfirststock.TabIndex = 20;
            this.chkfirststock.Text = "First Stock";
            this.chkfirststock.UseVisualStyleBackColor = true;
            this.chkfirststock.CheckedChanged += new System.EventHandler(this.chkfirststock_CheckedChanged);
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalAmount.Location = new System.Drawing.Point(759, 2);
            this.lblTotalAmount.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(123, 29);
            this.lblTotalAmount.TabIndex = 50;
            this.lblTotalAmount.Text = "0.00";
            this.lblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTotalAmount.Visible = false;
            // 
            // lbltotalremamt
            // 
            this.lbltotalremamt.AutoSize = true;
            this.lbltotalremamt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltotalremamt.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltotalremamt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbltotalremamt.Location = new System.Drawing.Point(565, 2);
            this.lbltotalremamt.Margin = new System.Windows.Forms.Padding(2);
            this.lbltotalremamt.Name = "lbltotalremamt";
            this.lbltotalremamt.Size = new System.Drawing.Size(190, 29);
            this.lbltotalremamt.TabIndex = 49;
            this.lbltotalremamt.Text = "Total Transport Charges:";
            this.lbltotalremamt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbltotalremamt.Visible = false;
            // 
            // chkbetweendate
            // 
            this.chkbetweendate.AutoSize = true;
            this.chkbetweendate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkbetweendate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbetweendate.Location = new System.Drawing.Point(224, 2);
            this.chkbetweendate.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkbetweendate.Name = "chkbetweendate";
            this.chkbetweendate.Size = new System.Drawing.Size(91, 25);
            this.chkbetweendate.TabIndex = 3;
            this.chkbetweendate.Text = "From Date";
            this.chkbetweendate.UseVisualStyleBackColor = true;
            this.chkbetweendate.CheckedChanged += new System.EventHandler(this.chkbetweendate_CheckedChanged);
            // 
            // dtpFromPorderdate
            // 
            this.dtpFromPorderdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFromPorderdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromPorderdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromPorderdate.Location = new System.Drawing.Point(319, 2);
            this.dtpFromPorderdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFromPorderdate.Name = "dtpFromPorderdate";
            this.dtpFromPorderdate.Size = new System.Drawing.Size(93, 25);
            this.dtpFromPorderdate.TabIndex = 4;
            this.dtpFromPorderdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpFromPorderdate_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(416, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "To";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpToPorderDate
            // 
            this.dtpToPorderDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpToPorderDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToPorderDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToPorderDate.Location = new System.Drawing.Point(451, 2);
            this.dtpToPorderDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpToPorderDate.Name = "dtpToPorderDate";
            this.dtpToPorderDate.Size = new System.Drawing.Size(93, 25);
            this.dtpToPorderDate.TabIndex = 5;
            this.dtpToPorderDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpToPorderDate_KeyDown);
            // 
            // chktranscharge
            // 
            this.chktranscharge.AutoSize = true;
            this.chktranscharge.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chktranscharge.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chktranscharge.Location = new System.Drawing.Point(316, 2);
            this.chktranscharge.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chktranscharge.Name = "chktranscharge";
            this.chktranscharge.Size = new System.Drawing.Size(122, 25);
            this.chktranscharge.TabIndex = 19;
            this.chktranscharge.Text = "Transport chrgs";
            this.chktranscharge.UseVisualStyleBackColor = true;
            this.chktranscharge.CheckedChanged += new System.EventHandler(this.chktranscharge_CheckedChanged);
            this.chktranscharge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chktranscharge_KeyDown);
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 4;
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtons.Controls.Add(this.btnprint, 0, 0);
            this.tblltButtons.Controls.Add(this.btnGetAll, 0, 0);
            this.tblltButtons.Controls.Add(this.btnSearch, 0, 0);
            this.tblltButtons.Controls.Add(this.btntodayslist, 0, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(537, 0);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(347, 29);
            this.tblltButtons.TabIndex = 1;
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(196, 2);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(72, 25);
            this.btnprint.TabIndex = 24;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // btnGetAll
            // 
            this.btnGetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnGetAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGetAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAll.ForeColor = System.Drawing.Color.White;
            this.btnGetAll.Location = new System.Drawing.Point(272, 2);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(73, 25);
            this.btnGetAll.TabIndex = 23;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(2, 2);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(72, 25);
            this.btnSearch.TabIndex = 21;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btntodayslist
            // 
            this.btntodayslist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btntodayslist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btntodayslist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btntodayslist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntodayslist.ForeColor = System.Drawing.Color.White;
            this.btntodayslist.Location = new System.Drawing.Point(78, 2);
            this.btntodayslist.Margin = new System.Windows.Forms.Padding(2);
            this.btntodayslist.Name = "btntodayslist";
            this.btntodayslist.Size = new System.Drawing.Size(114, 25);
            this.btntodayslist.TabIndex = 22;
            this.btntodayslist.Text = "Today\'s List";
            this.btntodayslist.UseVisualStyleBackColor = false;
            this.btntodayslist.Click += new System.EventHandler(this.btntodayslist_Click);
            // 
            // chkporderno
            // 
            this.chkporderno.AutoSize = true;
            this.chkporderno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkporderno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkporderno.Location = new System.Drawing.Point(4, 2);
            this.chkporderno.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkporderno.Name = "chkporderno";
            this.chkporderno.Size = new System.Drawing.Size(144, 25);
            this.chkporderno.TabIndex = 1;
            this.chkporderno.Text = "Purchase Order No";
            this.chkporderno.UseVisualStyleBackColor = true;
            this.chkporderno.CheckedChanged += new System.EventHandler(this.chkporderno_CheckedChanged);
            // 
            // chkcashcredit
            // 
            this.chkcashcredit.AutoSize = true;
            this.chkcashcredit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkcashcredit.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcashcredit.Location = new System.Drawing.Point(726, 2);
            this.chkcashcredit.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkcashcredit.Name = "chkcashcredit";
            this.chkcashcredit.Size = new System.Drawing.Size(64, 25);
            this.chkcashcredit.TabIndex = 8;
            this.chkcashcredit.Text = "Mode";
            this.chkcashcredit.UseVisualStyleBackColor = true;
            this.chkcashcredit.CheckedChanged += new System.EventHandler(this.chkcashcredit_CheckedChanged);
            // 
            // cmbcashsredit
            // 
            this.cmbcashsredit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbcashsredit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcashsredit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcashsredit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcashsredit.FormattingEnabled = true;
            this.cmbcashsredit.Location = new System.Drawing.Point(794, 2);
            this.cmbcashsredit.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcashsredit.Name = "cmbcashsredit";
            this.cmbcashsredit.Size = new System.Drawing.Size(88, 25);
            this.cmbcashsredit.TabIndex = 9;
            this.cmbcashsredit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcashsredit_KeyDown);
            // 
            // chkdiscount
            // 
            this.chkdiscount.AutoSize = true;
            this.chkdiscount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkdiscount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkdiscount.Location = new System.Drawing.Point(228, 2);
            this.chkdiscount.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkdiscount.Name = "chkdiscount";
            this.chkdiscount.Size = new System.Drawing.Size(82, 25);
            this.chkdiscount.TabIndex = 18;
            this.chkdiscount.Text = "Discount";
            this.chkdiscount.UseVisualStyleBackColor = true;
            this.chkdiscount.CheckedChanged += new System.EventHandler(this.chkdiscount_CheckedChanged);
            this.chkdiscount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkdiscount_KeyDown);
            // 
            // disamt
            // 
            this.disamt.AutoSize = true;
            this.disamt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.disamt.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.disamt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.disamt.Location = new System.Drawing.Point(442, 2);
            this.disamt.Margin = new System.Windows.Forms.Padding(2);
            this.disamt.Name = "disamt";
            this.disamt.Size = new System.Drawing.Size(119, 29);
            this.disamt.TabIndex = 53;
            this.disamt.Text = "0.00";
            this.disamt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.disamt.Visible = false;
            // 
            // lbltoatdisamt
            // 
            this.lbltoatdisamt.AutoSize = true;
            this.lbltoatdisamt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltoatdisamt.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltoatdisamt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbltoatdisamt.Location = new System.Drawing.Point(248, 2);
            this.lbltoatdisamt.Margin = new System.Windows.Forms.Padding(2);
            this.lbltoatdisamt.Name = "lbltoatdisamt";
            this.lbltoatdisamt.Size = new System.Drawing.Size(190, 29);
            this.lbltoatdisamt.TabIndex = 52;
            this.lbltoatdisamt.Text = "Total discount Amount:";
            this.lbltoatdisamt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbltoatdisamt.Visible = false;
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
            this.label5.Size = new System.Drawing.Size(884, 37);
            this.label5.TabIndex = 11;
            this.label5.Text = "Without GST Purchase Bill List";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GvPorderInfo
            // 
            this.GvPorderInfo.AllowUserToAddRows = false;
            this.GvPorderInfo.AllowUserToDeleteRows = false;
            this.GvPorderInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
            this.GvPorderInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Updateg});
            this.GvPorderInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvPorderInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvPorderInfo.Location = new System.Drawing.Point(3, 127);
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
            this.GvPorderInfo.Size = new System.Drawing.Size(878, 369);
            this.GvPorderInfo.TabIndex = 26;
            this.GvPorderInfo.TabStop = false;
            this.GvPorderInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvPorderInfo_CellClick);
            this.GvPorderInfo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GvPorderInfo_KeyPress);
            // 
            // Updateg
            // 
            this.Updateg.HeaderText = "Update";
            this.Updateg.Image = global::AIOInventorySystem.Desk.Properties.Resources.Update;
            this.Updateg.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Updateg.Name = "Updateg";
            this.Updateg.ReadOnly = true;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.tblltRow1, 0, 1);
            this.tblltMain.Controls.Add(this.tblltRow2, 0, 2);
            this.tblltMain.Controls.Add(this.tblltRow3, 0, 3);
            this.tblltMain.Controls.Add(this.GvPorderInfo, 0, 4);
            this.tblltMain.Controls.Add(this.tblltTotalls, 0, 5);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 6;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.Size = new System.Drawing.Size(884, 532);
            this.tblltMain.TabIndex = 0;
            // 
            // tblltRow1
            // 
            this.tblltRow1.ColumnCount = 10;
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltRow1.Controls.Add(this.chkporderno, 0, 0);
            this.tblltRow1.Controls.Add(this.txtPOrderno, 1, 0);
            this.tblltRow1.Controls.Add(this.cmbcashsredit, 9, 0);
            this.tblltRow1.Controls.Add(this.chkbetweendate, 2, 0);
            this.tblltRow1.Controls.Add(this.chkcashcredit, 8, 0);
            this.tblltRow1.Controls.Add(this.dtpToPorderDate, 5, 0);
            this.tblltRow1.Controls.Add(this.label2, 4, 0);
            this.tblltRow1.Controls.Add(this.dtpFromPorderdate, 3, 0);
            this.tblltRow1.Controls.Add(this.txtManRefNo, 7, 0);
            this.tblltRow1.Controls.Add(this.chkMRefNo, 6, 0);
            this.tblltRow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblltRow1.Location = new System.Drawing.Point(0, 37);
            this.tblltRow1.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow1.Name = "tblltRow1";
            this.tblltRow1.RowCount = 1;
            this.tblltRow1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow1.Size = new System.Drawing.Size(884, 29);
            this.tblltRow1.TabIndex = 0;
            // 
            // txtPOrderno
            // 
            this.txtPOrderno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPOrderno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPOrderno.Location = new System.Drawing.Point(152, 2);
            this.txtPOrderno.Margin = new System.Windows.Forms.Padding(2);
            this.txtPOrderno.Name = "txtPOrderno";
            this.txtPOrderno.Size = new System.Drawing.Size(66, 25);
            this.txtPOrderno.TabIndex = 2;
            this.txtPOrderno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPOrderno_KeyDown);
            // 
            // tblltRow2
            // 
            this.tblltRow2.ColumnCount = 6;
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltRow2.Controls.Add(this.chksname, 0, 0);
            this.tblltRow2.Controls.Add(this.cmbcompany, 5, 0);
            this.tblltRow2.Controls.Add(this.cmbsname, 1, 0);
            this.tblltRow2.Controls.Add(this.chkmfgcom, 4, 0);
            this.tblltRow2.Controls.Add(this.chkproductname, 2, 0);
            this.tblltRow2.Controls.Add(this.txtproductname, 3, 0);
            this.tblltRow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow2.Location = new System.Drawing.Point(0, 66);
            this.tblltRow2.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow2.Name = "tblltRow2";
            this.tblltRow2.RowCount = 1;
            this.tblltRow2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow2.Size = new System.Drawing.Size(884, 29);
            this.tblltRow2.TabIndex = 10;
            // 
            // tblltRow3
            // 
            this.tblltRow3.ColumnCount = 6;
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.5F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39F));
            this.tblltRow3.Controls.Add(this.cmbgroupMaster, 1, 0);
            this.tblltRow3.Controls.Add(this.chkgroupname, 0, 0);
            this.tblltRow3.Controls.Add(this.chkdiscount, 2, 0);
            this.tblltRow3.Controls.Add(this.chktranscharge, 3, 0);
            this.tblltRow3.Controls.Add(this.chkfirststock, 4, 0);
            this.tblltRow3.Controls.Add(this.tblltButtons, 5, 0);
            this.tblltRow3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow3.Location = new System.Drawing.Point(0, 95);
            this.tblltRow3.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow3.Name = "tblltRow3";
            this.tblltRow3.RowCount = 1;
            this.tblltRow3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow3.Size = new System.Drawing.Size(884, 29);
            this.tblltRow3.TabIndex = 16;
            // 
            // tblltTotalls
            // 
            this.tblltTotalls.ColumnCount = 6;
            this.tblltTotalls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltTotalls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltTotalls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblltTotalls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltTotalls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblltTotalls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltTotalls.Controls.Add(this.lblTotalAmount, 5, 0);
            this.tblltTotalls.Controls.Add(this.disamt, 3, 0);
            this.tblltTotalls.Controls.Add(this.lbltotalremamt, 4, 0);
            this.tblltTotalls.Controls.Add(this.label8, 0, 0);
            this.tblltTotalls.Controls.Add(this.lbltoatdisamt, 2, 0);
            this.tblltTotalls.Controls.Add(this.lblTotalBill, 1, 0);
            this.tblltTotalls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltTotalls.Location = new System.Drawing.Point(0, 499);
            this.tblltTotalls.Margin = new System.Windows.Forms.Padding(0);
            this.tblltTotalls.Name = "tblltTotalls";
            this.tblltTotalls.RowCount = 1;
            this.tblltTotalls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltTotalls.Size = new System.Drawing.Size(884, 33);
            this.tblltTotalls.TabIndex = 29;
            // 
            // tblltLogin
            // 
            this.tblltLogin.ColumnCount = 3;
            this.tblltLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblltLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tblltLogin.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltLogin.Controls.Add(this.tblltLoginButtons, 0, 3);
            this.tblltLogin.Controls.Add(this.label58, 0, 1);
            this.tblltLogin.Controls.Add(this.label56, 0, 2);
            this.tblltLogin.Controls.Add(this.txtpassword, 1, 2);
            this.tblltLogin.Controls.Add(this.txtusername, 1, 1);
            this.tblltLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltLogin.Location = new System.Drawing.Point(0, 0);
            this.tblltLogin.Name = "tblltLogin";
            this.tblltLogin.RowCount = 4;
            this.tblltLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltLogin.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltLogin.Size = new System.Drawing.Size(250, 120);
            this.tblltLogin.TabIndex = 30;
            // 
            // tblltLoginButtons
            // 
            this.tblltLoginButtons.ColumnCount = 4;
            this.tblltLogin.SetColumnSpan(this.tblltLoginButtons, 3);
            this.tblltLoginButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltLoginButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltLoginButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltLoginButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltLoginButtons.Controls.Add(this.btnClose, 2, 0);
            this.tblltLoginButtons.Controls.Add(this.btnLogin, 1, 0);
            this.tblltLoginButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltLoginButtons.Location = new System.Drawing.Point(0, 84);
            this.tblltLoginButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltLoginButtons.Name = "tblltLoginButtons";
            this.tblltLoginButtons.RowCount = 1;
            this.tblltLoginButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltLoginButtons.Size = new System.Drawing.Size(250, 36);
            this.tblltLoginButtons.TabIndex = 33;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(126, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(58, 32);
            this.btnClose.TabIndex = 34;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(64, 2);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(58, 32);
            this.btnLogin.TabIndex = 33;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label58.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label58.ForeColor = System.Drawing.Color.White;
            this.label58.Location = new System.Drawing.Point(2, 26);
            this.label58.Margin = new System.Windows.Forms.Padding(2);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(83, 26);
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
            this.label56.Location = new System.Drawing.Point(2, 56);
            this.label56.Margin = new System.Windows.Forms.Padding(2);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(83, 26);
            this.label56.TabIndex = 1;
            this.label56.Text = "Password:";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtpassword
            // 
            this.txtpassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtpassword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpassword.Location = new System.Drawing.Point(89, 56);
            this.txtpassword.Margin = new System.Windows.Forms.Padding(2);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.Size = new System.Drawing.Size(146, 25);
            this.txtpassword.TabIndex = 32;
            // 
            // txtusername
            // 
            this.txtusername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtusername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtusername.Location = new System.Drawing.Point(89, 26);
            this.txtusername.Margin = new System.Windows.Forms.Padding(2);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(146, 25);
            this.txtusername.TabIndex = 31;
            // 
            // pnlLogin
            // 
            this.pnlLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlLogin.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlLogin.Controls.Add(this.tblltLogin);
            this.pnlLogin.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlLogin.Location = new System.Drawing.Point(319, 177);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(250, 120);
            this.pnlLogin.TabIndex = 92;
            this.pnlLogin.Visible = false;
            // 
            // WithoutVatPurchaseOrderList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(884, 532);
            this.Controls.Add(this.pnlLogin);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "WithoutVatPurchaseOrderList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WithOut GST Purchase Bill List";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmPurchaseOrderList_FormClosed);
            this.Load += new System.EventHandler(this.frmPurchaseOrderList_Load);
            this.tblltButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GvPorderInfo)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltRow1.ResumeLayout(false);
            this.tblltRow1.PerformLayout();
            this.tblltRow2.ResumeLayout(false);
            this.tblltRow2.PerformLayout();
            this.tblltRow3.ResumeLayout(false);
            this.tblltRow3.PerformLayout();
            this.tblltTotalls.ResumeLayout(false);
            this.tblltTotalls.PerformLayout();
            this.tblltLogin.ResumeLayout(false);
            this.tblltLogin.PerformLayout();
            this.tblltLoginButtons.ResumeLayout(false);
            this.pnlLogin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
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
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lbltotalremamt;
        private System.Windows.Forms.Label disamt;
        private System.Windows.Forms.CheckBox chkdiscount;
        private System.Windows.Forms.Label lbltoatdisamt;
        private System.Windows.Forms.Label lblTotalBill;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.CheckBox chkgroupname;
        private System.Windows.Forms.ComboBox cmbgroupMaster;
        private System.Windows.Forms.CheckBox chkproductname;
        private System.Windows.Forms.ComboBox cmbcompany;
        private System.Windows.Forms.CheckBox chkmfgcom;
        private System.Windows.Forms.TextBox txtproductname;
        private System.Windows.Forms.CheckBox chkfirststock;
        private System.Windows.Forms.CheckBox chkMRefNo;
        private System.Windows.Forms.TextBox txtManRefNo;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltRow1;
        private RachitControls.NumericTextBox txtPOrderno;
        private System.Windows.Forms.TableLayoutPanel tblltRow2;
        private System.Windows.Forms.TableLayoutPanel tblltRow3;
        private System.Windows.Forms.TableLayoutPanel tblltTotalls;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button btnGetAll;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btntodayslist;
        private System.Windows.Forms.TableLayoutPanel tblltLogin;
        private System.Windows.Forms.TableLayoutPanel tblltLoginButtons;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Label label58;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.DataGridViewImageColumn Updateg;
    }
}