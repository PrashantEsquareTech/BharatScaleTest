namespace AIOInventorySystem.Desk.Forms
{
    partial class frmServiceInvoiceList
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
            this.txtServiceNo = new System.Windows.Forms.TextBox();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.CmbCustomer = new System.Windows.Forms.ComboBox();
            this.txtPavatiNo = new System.Windows.Forms.TextBox();
            this.txtCLMNo = new System.Windows.Forms.TextBox();
            this.txtMake = new System.Windows.Forms.TextBox();
            this.txtMachineNo = new System.Windows.Forms.TextBox();
            this.chkServiceNo = new System.Windows.Forms.CheckBox();
            this.chkFromDate = new System.Windows.Forms.CheckBox();
            this.chkCustomer = new System.Windows.Forms.CheckBox();
            this.chkCLMNo = new System.Windows.Forms.CheckBox();
            this.chkPavatiNo = new System.Windows.Forms.CheckBox();
            this.chkMake = new System.Windows.Forms.CheckBox();
            this.chkMachineNo = new System.Windows.Forms.CheckBox();
            this.gvList = new System.Windows.Forms.DataGridView();
            this.Update = new System.Windows.Forms.DataGridViewImageColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.lblTotalServiceCharges = new System.Windows.Forms.Label();
            this.lblTotalVerificationFees = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblltRow1 = new System.Windows.Forms.TableLayoutPanel();
            this.tblltRow2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnPrintRegister = new System.Windows.Forms.Button();
            this.tblltFooter = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.tblltRow1.SuspendLayout();
            this.tblltRow2.SuspendLayout();
            this.tblltFooter.SuspendLayout();
            this.SuspendLayout();
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
            this.label5.Size = new System.Drawing.Size(1379, 45);
            this.label5.TabIndex = 29;
            this.label5.Text = " Service Invoice List";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtServiceNo
            // 
            this.txtServiceNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtServiceNo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServiceNo.Location = new System.Drawing.Point(161, 2);
            this.txtServiceNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtServiceNo.Name = "txtServiceNo";
            this.txtServiceNo.Size = new System.Drawing.Size(131, 30);
            this.txtServiceNo.TabIndex = 2;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFromDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFromDate.Location = new System.Drawing.Point(456, 2);
            this.dtpFromDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(159, 30);
            this.dtpFromDate.TabIndex = 4;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpToDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Location = new System.Drawing.Point(689, 2);
            this.dtpToDate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(159, 30);
            this.dtpToDate.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(623, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 2, 3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 35);
            this.label1.TabIndex = 38;
            this.label1.Text = "To";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CmbCustomer
            // 
            this.CmbCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmbCustomer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbCustomer.FormattingEnabled = true;
            this.CmbCustomer.Location = new System.Drawing.Point(1047, 2);
            this.CmbCustomer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CmbCustomer.Name = "CmbCustomer";
            this.CmbCustomer.Size = new System.Drawing.Size(329, 31);
            this.CmbCustomer.TabIndex = 7;
            this.CmbCustomer.Text = " ";
            // 
            // txtPavatiNo
            // 
            this.txtPavatiNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPavatiNo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPavatiNo.Location = new System.Drawing.Point(388, 2);
            this.txtPavatiNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPavatiNo.Name = "txtPavatiNo";
            this.txtPavatiNo.Size = new System.Drawing.Size(90, 30);
            this.txtPavatiNo.TabIndex = 11;
            // 
            // txtCLMNo
            // 
            this.txtCLMNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCLMNo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCLMNo.Location = new System.Drawing.Point(127, 2);
            this.txtCLMNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCLMNo.Name = "txtCLMNo";
            this.txtCLMNo.Size = new System.Drawing.Size(131, 30);
            this.txtCLMNo.TabIndex = 9;
            // 
            // txtMake
            // 
            this.txtMake.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMake.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMake.Location = new System.Drawing.Point(580, 2);
            this.txtMake.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMake.Name = "txtMake";
            this.txtMake.Size = new System.Drawing.Size(173, 30);
            this.txtMake.TabIndex = 13;
            // 
            // txtMachineNo
            // 
            this.txtMachineNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMachineNo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMachineNo.Location = new System.Drawing.Point(910, 2);
            this.txtMachineNo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMachineNo.Name = "txtMachineNo";
            this.txtMachineNo.Size = new System.Drawing.Size(187, 30);
            this.txtMachineNo.TabIndex = 15;
            // 
            // chkServiceNo
            // 
            this.chkServiceNo.AutoSize = true;
            this.chkServiceNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkServiceNo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkServiceNo.Location = new System.Drawing.Point(5, 2);
            this.chkServiceNo.Margin = new System.Windows.Forms.Padding(5, 2, 3, 2);
            this.chkServiceNo.Name = "chkServiceNo";
            this.chkServiceNo.Size = new System.Drawing.Size(150, 35);
            this.chkServiceNo.TabIndex = 1;
            this.chkServiceNo.Text = "Service No";
            this.chkServiceNo.UseVisualStyleBackColor = true;
            // 
            // chkFromDate
            // 
            this.chkFromDate.AutoSize = true;
            this.chkFromDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkFromDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFromDate.Location = new System.Drawing.Point(300, 2);
            this.chkFromDate.Margin = new System.Windows.Forms.Padding(5, 2, 3, 2);
            this.chkFromDate.Name = "chkFromDate";
            this.chkFromDate.Size = new System.Drawing.Size(150, 35);
            this.chkFromDate.TabIndex = 3;
            this.chkFromDate.Text = "From Date";
            this.chkFromDate.UseVisualStyleBackColor = true;
            // 
            // chkCustomer
            // 
            this.chkCustomer.AutoSize = true;
            this.chkCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkCustomer.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCustomer.Location = new System.Drawing.Point(856, 2);
            this.chkCustomer.Margin = new System.Windows.Forms.Padding(5, 2, 3, 2);
            this.chkCustomer.Name = "chkCustomer";
            this.chkCustomer.Size = new System.Drawing.Size(185, 35);
            this.chkCustomer.TabIndex = 6;
            this.chkCustomer.Text = "Customer Name";
            this.chkCustomer.UseVisualStyleBackColor = true;
            // 
            // chkCLMNo
            // 
            this.chkCLMNo.AutoSize = true;
            this.chkCLMNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkCLMNo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCLMNo.Location = new System.Drawing.Point(5, 2);
            this.chkCLMNo.Margin = new System.Windows.Forms.Padding(5, 2, 3, 2);
            this.chkCLMNo.Name = "chkCLMNo";
            this.chkCLMNo.Size = new System.Drawing.Size(116, 35);
            this.chkCLMNo.TabIndex = 8;
            this.chkCLMNo.Text = "CLM No";
            this.chkCLMNo.UseVisualStyleBackColor = true;
            // 
            // chkPavatiNo
            // 
            this.chkPavatiNo.AutoSize = true;
            this.chkPavatiNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkPavatiNo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPavatiNo.Location = new System.Drawing.Point(266, 2);
            this.chkPavatiNo.Margin = new System.Windows.Forms.Padding(5, 2, 3, 2);
            this.chkPavatiNo.Name = "chkPavatiNo";
            this.chkPavatiNo.Size = new System.Drawing.Size(116, 35);
            this.chkPavatiNo.TabIndex = 10;
            this.chkPavatiNo.Text = "Pavati No";
            this.chkPavatiNo.UseVisualStyleBackColor = true;
            // 
            // chkMake
            // 
            this.chkMake.AutoSize = true;
            this.chkMake.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkMake.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMake.Location = new System.Drawing.Point(486, 2);
            this.chkMake.Margin = new System.Windows.Forms.Padding(5, 2, 3, 2);
            this.chkMake.Name = "chkMake";
            this.chkMake.Size = new System.Drawing.Size(88, 35);
            this.chkMake.TabIndex = 12;
            this.chkMake.Text = "Make";
            this.chkMake.UseVisualStyleBackColor = true;
            // 
            // chkMachineNo
            // 
            this.chkMachineNo.AutoSize = true;
            this.chkMachineNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkMachineNo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMachineNo.Location = new System.Drawing.Point(761, 2);
            this.chkMachineNo.Margin = new System.Windows.Forms.Padding(5, 2, 3, 2);
            this.chkMachineNo.Name = "chkMachineNo";
            this.chkMachineNo.Size = new System.Drawing.Size(143, 35);
            this.chkMachineNo.TabIndex = 14;
            this.chkMachineNo.Text = "Machine No";
            this.chkMachineNo.UseVisualStyleBackColor = true;
            // 
            // gvList
            // 
            this.gvList.AllowUserToAddRows = false;
            this.gvList.AllowUserToDeleteRows = false;
            this.gvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvList.BackgroundColor = System.Drawing.Color.White;
            this.gvList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Update});
            this.gvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.gvList.Location = new System.Drawing.Point(3, 125);
            this.gvList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gvList.Name = "gvList";
            this.gvList.RowHeadersWidth = 15;
            this.gvList.RowTemplate.Height = 28;
            this.gvList.Size = new System.Drawing.Size(1373, 404);
            this.gvList.TabIndex = 19;
            this.gvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvList_CellClick);
            // 
            // Update
            // 
            this.Update.HeaderText = "Update";
            this.Update.Image = global::AIOInventorySystem.Desk.Properties.Resources.Update;
            this.Update.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Update.Name = "Update";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 2);
            this.label10.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(338, 32);
            this.label10.TabIndex = 95;
            this.label10.Text = "Total Service Charge";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTotalServiceCharges
            // 
            this.lblTotalServiceCharges.AutoSize = true;
            this.lblTotalServiceCharges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalServiceCharges.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalServiceCharges.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalServiceCharges.Location = new System.Drawing.Point(347, 2);
            this.lblTotalServiceCharges.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblTotalServiceCharges.Name = "lblTotalServiceCharges";
            this.lblTotalServiceCharges.Size = new System.Drawing.Size(338, 32);
            this.lblTotalServiceCharges.TabIndex = 96;
            this.lblTotalServiceCharges.Text = "---";
            // 
            // lblTotalVerificationFees
            // 
            this.lblTotalVerificationFees.AutoSize = true;
            this.lblTotalVerificationFees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalVerificationFees.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalVerificationFees.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalVerificationFees.Location = new System.Drawing.Point(1035, 2);
            this.lblTotalVerificationFees.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblTotalVerificationFees.Name = "lblTotalVerificationFees";
            this.lblTotalVerificationFees.Size = new System.Drawing.Size(341, 32);
            this.lblTotalVerificationFees.TabIndex = 98;
            this.lblTotalVerificationFees.Text = "---";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(691, 2);
            this.label13.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(338, 32);
            this.label13.TabIndex = 97;
            this.label13.Text = "Total Verfication Fess";
            this.label13.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(1103, 2);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(83, 35);
            this.btnSearch.TabIndex = 16;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnGetAll
            // 
            this.btnGetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnGetAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGetAll.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAll.ForeColor = System.Drawing.Color.White;
            this.btnGetAll.Location = new System.Drawing.Point(1192, 2);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(83, 35);
            this.btnGetAll.TabIndex = 17;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.gvList, 0, 3);
            this.tblltMain.Controls.Add(this.tblltRow1, 0, 1);
            this.tblltMain.Controls.Add(this.tblltRow2, 0, 2);
            this.tblltMain.Controls.Add(this.tblltFooter, 0, 4);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 5;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.Size = new System.Drawing.Size(1379, 567);
            this.tblltMain.TabIndex = 0;
            // 
            // tblltRow1
            // 
            this.tblltRow1.ColumnCount = 8;
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.5F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.5F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tblltRow1.Controls.Add(this.chkServiceNo, 0, 0);
            this.tblltRow1.Controls.Add(this.txtServiceNo, 1, 0);
            this.tblltRow1.Controls.Add(this.chkFromDate, 2, 0);
            this.tblltRow1.Controls.Add(this.dtpFromDate, 3, 0);
            this.tblltRow1.Controls.Add(this.label1, 4, 0);
            this.tblltRow1.Controls.Add(this.dtpToDate, 5, 0);
            this.tblltRow1.Controls.Add(this.chkCustomer, 6, 0);
            this.tblltRow1.Controls.Add(this.CmbCustomer, 7, 0);
            this.tblltRow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow1.Location = new System.Drawing.Point(0, 45);
            this.tblltRow1.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow1.Name = "tblltRow1";
            this.tblltRow1.RowCount = 1;
            this.tblltRow1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow1.Size = new System.Drawing.Size(1379, 39);
            this.tblltRow1.TabIndex = 1;
            // 
            // tblltRow2
            // 
            this.tblltRow2.ColumnCount = 11;
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.5F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.5F));
            this.tblltRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltRow2.Controls.Add(this.chkCLMNo, 0, 0);
            this.tblltRow2.Controls.Add(this.btnGetAll, 9, 0);
            this.tblltRow2.Controls.Add(this.txtCLMNo, 1, 0);
            this.tblltRow2.Controls.Add(this.btnSearch, 8, 0);
            this.tblltRow2.Controls.Add(this.chkPavatiNo, 2, 0);
            this.tblltRow2.Controls.Add(this.txtPavatiNo, 3, 0);
            this.tblltRow2.Controls.Add(this.chkMake, 4, 0);
            this.tblltRow2.Controls.Add(this.txtMake, 5, 0);
            this.tblltRow2.Controls.Add(this.chkMachineNo, 6, 0);
            this.tblltRow2.Controls.Add(this.txtMachineNo, 7, 0);
            this.tblltRow2.Controls.Add(this.btnPrintRegister, 10, 0);
            this.tblltRow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow2.Location = new System.Drawing.Point(0, 84);
            this.tblltRow2.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow2.Name = "tblltRow2";
            this.tblltRow2.RowCount = 1;
            this.tblltRow2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow2.Size = new System.Drawing.Size(1379, 39);
            this.tblltRow2.TabIndex = 8;
            // 
            // btnPrintRegister
            // 
            this.btnPrintRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnPrintRegister.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrintRegister.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPrintRegister.ForeColor = System.Drawing.Color.White;
            this.btnPrintRegister.Location = new System.Drawing.Point(1281, 2);
            this.btnPrintRegister.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrintRegister.Name = "btnPrintRegister";
            this.btnPrintRegister.Size = new System.Drawing.Size(95, 35);
            this.btnPrintRegister.TabIndex = 18;
            this.btnPrintRegister.Text = "Register";
            this.btnPrintRegister.UseVisualStyleBackColor = false;
            this.btnPrintRegister.Click += new System.EventHandler(this.btnPrintRegister_Click);
            // 
            // tblltFooter
            // 
            this.tblltFooter.ColumnCount = 4;
            this.tblltFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltFooter.Controls.Add(this.label10, 0, 0);
            this.tblltFooter.Controls.Add(this.lblTotalVerificationFees, 3, 0);
            this.tblltFooter.Controls.Add(this.lblTotalServiceCharges, 1, 0);
            this.tblltFooter.Controls.Add(this.label13, 2, 0);
            this.tblltFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltFooter.Location = new System.Drawing.Point(0, 531);
            this.tblltFooter.Margin = new System.Windows.Forms.Padding(0);
            this.tblltFooter.Name = "tblltFooter";
            this.tblltFooter.RowCount = 1;
            this.tblltFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltFooter.Size = new System.Drawing.Size(1379, 36);
            this.tblltFooter.TabIndex = 97;
            // 
            // frmServiceInvoiceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1379, 567);
            this.Controls.Add(this.tblltMain);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmServiceInvoiceList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Service Invoice List";
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltRow1.ResumeLayout(false);
            this.tblltRow1.PerformLayout();
            this.tblltRow2.ResumeLayout(false);
            this.tblltRow2.PerformLayout();
            this.tblltFooter.ResumeLayout(false);
            this.tblltFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtServiceNo;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CmbCustomer;
        private System.Windows.Forms.TextBox txtPavatiNo;
        private System.Windows.Forms.TextBox txtCLMNo;
        private System.Windows.Forms.TextBox txtMake;
        private System.Windows.Forms.TextBox txtMachineNo;
        private System.Windows.Forms.CheckBox chkServiceNo;
        private System.Windows.Forms.CheckBox chkFromDate;
        private System.Windows.Forms.CheckBox chkCustomer;
        private System.Windows.Forms.CheckBox chkCLMNo;
        private System.Windows.Forms.CheckBox chkPavatiNo;
        private System.Windows.Forms.CheckBox chkMake;
        private System.Windows.Forms.CheckBox chkMachineNo;
        private System.Windows.Forms.DataGridView gvList;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblTotalServiceCharges;
        private System.Windows.Forms.Label lblTotalVerificationFees;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnGetAll;
        private System.Windows.Forms.DataGridViewImageColumn Update;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltRow1;
        private System.Windows.Forms.TableLayoutPanel tblltRow2;
        private System.Windows.Forms.TableLayoutPanel tblltFooter;
        private System.Windows.Forms.Button btnPrintRegister;
    }
}