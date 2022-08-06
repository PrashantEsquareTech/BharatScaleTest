namespace AIOInventorySystem.Desk.Forms
{
    partial class frmProprietorLedger
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
            this.GvreceiptInfo = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtparticular = new System.Windows.Forms.TextBox();
            this.txtreceiptno = new System.Windows.Forms.TextBox();
            this.dtpreceiptDate = new System.Windows.Forms.DateTimePicker();
            this.cmbTransactiontype = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbProprietor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbpaymentmode = new System.Windows.Forms.ComboBox();
            this.txtchequeno = new System.Windows.Forms.TextBox();
            this.dtpchequedate = new System.Windows.Forms.DateTimePicker();
            this.cmbbank = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tblltList = new System.Windows.Forms.TableLayoutPanel();
            this.chkProprietorname1 = new System.Windows.Forms.CheckBox();
            this.chkbankname = new System.Windows.Forms.CheckBox();
            this.dtptodate = new System.Windows.Forms.DateTimePicker();
            this.cmbProprietorname1 = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.cmbbank2 = new System.Windows.Forms.ComboBox();
            this.cmbtype = new System.Windows.Forms.ComboBox();
            this.dtpfromdate = new System.Windows.Forms.DateTimePicker();
            this.lblAmtText = new System.Windows.Forms.Label();
            this.chkbetdate = new System.Windows.Forms.CheckBox();
            this.chktype = new System.Windows.Forms.CheckBox();
            this.chkdate = new System.Windows.Forms.CheckBox();
            this.dtpdate = new System.Windows.Forms.DateTimePicker();
            this.lblRemAmt = new System.Windows.Forms.Label();
            this.tblltListButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnprint1 = new System.Windows.Forms.Button();
            this.btnsearch = new System.Windows.Forms.Button();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.grpReceipt = new System.Windows.Forms.GroupBox();
            this.tblltReceipt = new System.Windows.Forms.TableLayoutPanel();
            this.tblltRow1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnProprietorForm = new System.Windows.Forms.Button();
            this.txtPaidAmt = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GvreceiptInfo)).BeginInit();
            this.tblltList.SuspendLayout();
            this.tblltListButtons.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.grpList.SuspendLayout();
            this.grpReceipt.SuspendLayout();
            this.tblltReceipt.SuspendLayout();
            this.tblltRow1.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // GvreceiptInfo
            // 
            this.GvreceiptInfo.AllowUserToAddRows = false;
            this.GvreceiptInfo.AllowUserToDeleteRows = false;
            this.GvreceiptInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvreceiptInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvreceiptInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GvreceiptInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblltList.SetColumnSpan(this.GvreceiptInfo, 8);
            this.GvreceiptInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvreceiptInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvreceiptInfo.Location = new System.Drawing.Point(2, 58);
            this.GvreceiptInfo.Margin = new System.Windows.Forms.Padding(2);
            this.GvreceiptInfo.Name = "GvreceiptInfo";
            this.GvreceiptInfo.ReadOnly = true;
            this.GvreceiptInfo.RowHeadersWidth = 15;
            this.GvreceiptInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvreceiptInfo.Size = new System.Drawing.Size(844, 221);
            this.GvreceiptInfo.TabIndex = 27;
            this.GvreceiptInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvreceiptInfo_CellClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(409, 58);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 29);
            this.label4.TabIndex = 109;
            this.label4.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(466, 31);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 25);
            this.label2.TabIndex = 108;
            this.label2.Text = "Bank Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtparticular
            // 
            this.txtparticular.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtparticular.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtparticular.ForeColor = System.Drawing.Color.Black;
            this.txtparticular.Location = new System.Drawing.Point(129, 118);
            this.txtparticular.Margin = new System.Windows.Forms.Padding(2);
            this.txtparticular.Multiline = true;
            this.txtparticular.Name = "txtparticular";
            this.tblltReceipt.SetRowSpan(this.txtparticular, 2);
            this.txtparticular.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtparticular.Size = new System.Drawing.Size(275, 56);
            this.txtparticular.TabIndex = 5;
            this.txtparticular.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtparticular_KeyDown);
            // 
            // txtreceiptno
            // 
            this.txtreceiptno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtreceiptno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtreceiptno.Location = new System.Drawing.Point(188, 2);
            this.txtreceiptno.Margin = new System.Windows.Forms.Padding(2);
            this.txtreceiptno.Name = "txtreceiptno";
            this.txtreceiptno.ReadOnly = true;
            this.txtreceiptno.Size = new System.Drawing.Size(89, 25);
            this.txtreceiptno.TabIndex = 0;
            this.txtreceiptno.TabStop = false;
            // 
            // dtpreceiptDate
            // 
            this.dtpreceiptDate.CustomFormat = "dd/MM/yyyy";
            this.dtpreceiptDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpreceiptDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpreceiptDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpreceiptDate.Location = new System.Drawing.Point(2, 2);
            this.dtpreceiptDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpreceiptDate.Name = "dtpreceiptDate";
            this.dtpreceiptDate.Size = new System.Drawing.Size(89, 25);
            this.dtpreceiptDate.TabIndex = 0;
            this.dtpreceiptDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpreceiptDate_KeyDown);
            // 
            // cmbTransactiontype
            // 
            this.cmbTransactiontype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTransactiontype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransactiontype.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTransactiontype.FormattingEnabled = true;
            this.cmbTransactiontype.Items.AddRange(new object[] {
            "Credit",
            "Debit"});
            this.cmbTransactiontype.Location = new System.Drawing.Point(129, 60);
            this.cmbTransactiontype.Margin = new System.Windows.Forms.Padding(2);
            this.cmbTransactiontype.Name = "cmbTransactiontype";
            this.cmbTransactiontype.Size = new System.Drawing.Size(275, 25);
            this.cmbTransactiontype.TabIndex = 3;
            this.cmbTransactiontype.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbTransactiontype_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(409, 29);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 29);
            this.label8.TabIndex = 98;
            this.label8.Text = "*";
            // 
            // cmbProprietor
            // 
            this.cmbProprietor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbProprietor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbProprietor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbProprietor.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProprietor.FormattingEnabled = true;
            this.cmbProprietor.Location = new System.Drawing.Point(129, 31);
            this.cmbProprietor.Margin = new System.Windows.Forms.Padding(2);
            this.cmbProprietor.Name = "cmbProprietor";
            this.cmbProprietor.Size = new System.Drawing.Size(275, 25);
            this.cmbProprietor.TabIndex = 1;
            this.cmbProprietor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcustomer_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 118);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 25);
            this.label3.TabIndex = 22;
            this.label3.Text = "Reason:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(2, 89);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 25);
            this.label6.TabIndex = 21;
            this.label6.Text = "Amount:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(2, 60);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 25);
            this.label5.TabIndex = 20;
            this.label5.Text = "Transaction Type:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 25);
            this.label1.TabIndex = 19;
            this.label1.Text = "Proprietor Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(95, 2);
            this.label12.Margin = new System.Windows.Forms.Padding(2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 25);
            this.label12.TabIndex = 18;
            this.label12.Text = "Receipt No:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(466, 2);
            this.label14.Margin = new System.Windows.Forms.Padding(2);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(114, 25);
            this.label14.TabIndex = 23;
            this.label14.Text = "Payment Mode:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(466, 60);
            this.label13.Margin = new System.Windows.Forms.Padding(2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(114, 25);
            this.label13.TabIndex = 25;
            this.label13.Text = "Cheque Date:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(2, 2);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 25);
            this.label7.TabIndex = 17;
            this.label7.Text = "Date:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(466, 89);
            this.label15.Margin = new System.Windows.Forms.Padding(2);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(114, 25);
            this.label15.TabIndex = 26;
            this.label15.Text = "Cheque No:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label10.Location = new System.Drawing.Point(813, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 29);
            this.label10.TabIndex = 100;
            this.label10.Text = "*";
            // 
            // cmbpaymentmode
            // 
            this.cmbpaymentmode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbpaymentmode.DropDownHeight = 110;
            this.cmbpaymentmode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbpaymentmode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbpaymentmode.FormattingEnabled = true;
            this.cmbpaymentmode.IntegralHeight = false;
            this.cmbpaymentmode.Items.AddRange(new object[] {
            "Cash",
            "Cheque",
            "NEFT",
            "RTGS"});
            this.cmbpaymentmode.Location = new System.Drawing.Point(584, 2);
            this.cmbpaymentmode.Margin = new System.Windows.Forms.Padding(2);
            this.cmbpaymentmode.Name = "cmbpaymentmode";
            this.cmbpaymentmode.Size = new System.Drawing.Size(224, 25);
            this.cmbpaymentmode.TabIndex = 6;
            this.cmbpaymentmode.SelectedIndexChanged += new System.EventHandler(this.cmbpaymentmode_SelectedIndexChanged);
            this.cmbpaymentmode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbpaymentmode_KeyDown);
            // 
            // txtchequeno
            // 
            this.txtchequeno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtchequeno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtchequeno.Location = new System.Drawing.Point(584, 89);
            this.txtchequeno.Margin = new System.Windows.Forms.Padding(2);
            this.txtchequeno.Name = "txtchequeno";
            this.txtchequeno.Size = new System.Drawing.Size(224, 25);
            this.txtchequeno.TabIndex = 9;
            this.txtchequeno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtchequeno_KeyDown);
            // 
            // dtpchequedate
            // 
            this.dtpchequedate.CustomFormat = "dd/MM/yyyy";
            this.dtpchequedate.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtpchequedate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpchequedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpchequedate.Location = new System.Drawing.Point(584, 60);
            this.dtpchequedate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpchequedate.Name = "dtpchequedate";
            this.dtpchequedate.Size = new System.Drawing.Size(120, 25);
            this.dtpchequedate.TabIndex = 8;
            // 
            // cmbbank
            // 
            this.cmbbank.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbbank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbank.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbbank.FormattingEnabled = true;
            this.cmbbank.Location = new System.Drawing.Point(584, 31);
            this.cmbbank.Margin = new System.Windows.Forms.Padding(2);
            this.cmbbank.Name = "cmbbank";
            this.cmbbank.Size = new System.Drawing.Size(224, 25);
            this.cmbbank.TabIndex = 7;
            this.cmbbank.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbbank_KeyDown);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(854, 37);
            this.label9.TabIndex = 33;
            this.label9.Text = "Proprietor Ledger";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltList
            // 
            this.tblltList.ColumnCount = 8;
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltList.Controls.Add(this.chkProprietorname1, 0, 0);
            this.tblltList.Controls.Add(this.GvreceiptInfo, 0, 2);
            this.tblltList.Controls.Add(this.chkbankname, 0, 1);
            this.tblltList.Controls.Add(this.dtptodate, 7, 0);
            this.tblltList.Controls.Add(this.cmbProprietorname1, 1, 0);
            this.tblltList.Controls.Add(this.label19, 6, 0);
            this.tblltList.Controls.Add(this.cmbbank2, 1, 1);
            this.tblltList.Controls.Add(this.cmbtype, 5, 1);
            this.tblltList.Controls.Add(this.dtpfromdate, 5, 0);
            this.tblltList.Controls.Add(this.lblAmtText, 2, 0);
            this.tblltList.Controls.Add(this.chkbetdate, 4, 0);
            this.tblltList.Controls.Add(this.chktype, 4, 1);
            this.tblltList.Controls.Add(this.chkdate, 2, 1);
            this.tblltList.Controls.Add(this.dtpdate, 3, 1);
            this.tblltList.Controls.Add(this.lblRemAmt, 3, 0);
            this.tblltList.Controls.Add(this.tblltListButtons, 6, 1);
            this.tblltList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltList.Location = new System.Drawing.Point(0, 18);
            this.tblltList.Margin = new System.Windows.Forms.Padding(0);
            this.tblltList.Name = "tblltList";
            this.tblltList.RowCount = 3;
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tblltList.Size = new System.Drawing.Size(848, 281);
            this.tblltList.TabIndex = 14;
            // 
            // chkProprietorname1
            // 
            this.chkProprietorname1.AutoSize = true;
            this.chkProprietorname1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkProprietorname1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkProprietorname1.Location = new System.Drawing.Point(6, 2);
            this.chkProprietorname1.Margin = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this.chkProprietorname1.Name = "chkProprietorname1";
            this.chkProprietorname1.Size = new System.Drawing.Size(127, 24);
            this.chkProprietorname1.TabIndex = 14;
            this.chkProprietorname1.Text = "Proprietor Name";
            this.chkProprietorname1.UseVisualStyleBackColor = true;
            this.chkProprietorname1.CheckedChanged += new System.EventHandler(this.chkcustomername1_CheckedChanged);
            this.chkProprietorname1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkcustomername1_KeyDown);
            // 
            // chkbankname
            // 
            this.chkbankname.AutoSize = true;
            this.chkbankname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkbankname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbankname.Location = new System.Drawing.Point(6, 30);
            this.chkbankname.Margin = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this.chkbankname.Name = "chkbankname";
            this.chkbankname.Size = new System.Drawing.Size(127, 24);
            this.chkbankname.TabIndex = 19;
            this.chkbankname.Text = "Bank Name";
            this.chkbankname.UseVisualStyleBackColor = true;
            this.chkbankname.CheckedChanged += new System.EventHandler(this.chkbankname_CheckedChanged);
            this.chkbankname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkbankname_KeyDown);
            // 
            // dtptodate
            // 
            this.dtptodate.CustomFormat = "dd/MM/yyyy";
            this.dtptodate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtptodate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtptodate.Location = new System.Drawing.Point(736, 2);
            this.dtptodate.Margin = new System.Windows.Forms.Padding(2);
            this.dtptodate.Name = "dtptodate";
            this.dtptodate.Size = new System.Drawing.Size(110, 25);
            this.dtptodate.TabIndex = 18;
            this.dtptodate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtptodate_KeyDown);
            // 
            // cmbProprietorname1
            // 
            this.cmbProprietorname1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbProprietorname1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbProprietorname1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbProprietorname1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProprietorname1.FormattingEnabled = true;
            this.cmbProprietorname1.Location = new System.Drawing.Point(137, 2);
            this.cmbProprietorname1.Margin = new System.Windows.Forms.Padding(2);
            this.cmbProprietorname1.Name = "cmbProprietorname1";
            this.cmbProprietorname1.Size = new System.Drawing.Size(165, 25);
            this.cmbProprietorname1.TabIndex = 15;
            this.cmbProprietorname1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbProprietorname1_KeyDown);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(703, 2);
            this.label19.Margin = new System.Windows.Forms.Padding(2);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(29, 24);
            this.label19.TabIndex = 107;
            this.label19.Text = "To";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbbank2
            // 
            this.cmbbank2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbbank2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbank2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbbank2.FormattingEnabled = true;
            this.cmbbank2.Location = new System.Drawing.Point(137, 30);
            this.cmbbank2.Margin = new System.Windows.Forms.Padding(2);
            this.cmbbank2.Name = "cmbbank2";
            this.cmbbank2.Size = new System.Drawing.Size(165, 25);
            this.cmbbank2.TabIndex = 20;
            this.cmbbank2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbbank2_KeyDown);
            // 
            // cmbtype
            // 
            this.cmbtype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbtype.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbtype.FormattingEnabled = true;
            this.cmbtype.Items.AddRange(new object[] {
            "Credit",
            "Debit",
            "OpeningBalance"});
            this.cmbtype.Location = new System.Drawing.Point(593, 30);
            this.cmbtype.Margin = new System.Windows.Forms.Padding(2);
            this.cmbtype.Name = "cmbtype";
            this.cmbtype.Size = new System.Drawing.Size(106, 25);
            this.cmbtype.TabIndex = 24;
            this.cmbtype.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbtype_KeyDown);
            // 
            // dtpfromdate
            // 
            this.dtpfromdate.CustomFormat = "dd/MM/yyyy";
            this.dtpfromdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpfromdate.Location = new System.Drawing.Point(593, 2);
            this.dtpfromdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpfromdate.Name = "dtpfromdate";
            this.dtpfromdate.Size = new System.Drawing.Size(106, 25);
            this.dtpfromdate.TabIndex = 17;
            this.dtpfromdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpfromdate_KeyDown);
            // 
            // lblAmtText
            // 
            this.lblAmtText.AutoSize = true;
            this.lblAmtText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAmtText.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmtText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAmtText.Location = new System.Drawing.Point(306, 2);
            this.lblAmtText.Margin = new System.Windows.Forms.Padding(2);
            this.lblAmtText.Name = "lblAmtText";
            this.lblAmtText.Size = new System.Drawing.Size(72, 24);
            this.lblAmtText.TabIndex = 116;
            this.lblAmtText.Text = "Rem.Amt:";
            this.lblAmtText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkbetdate
            // 
            this.chkbetdate.AutoSize = true;
            this.chkbetdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkbetdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbetdate.Location = new System.Drawing.Point(496, 2);
            this.chkbetdate.Margin = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this.chkbetdate.Name = "chkbetdate";
            this.chkbetdate.Size = new System.Drawing.Size(93, 24);
            this.chkbetdate.TabIndex = 16;
            this.chkbetdate.Text = "From Date";
            this.chkbetdate.UseVisualStyleBackColor = true;
            this.chkbetdate.CheckedChanged += new System.EventHandler(this.chkbetdate_CheckedChanged);
            this.chkbetdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkbetdate_KeyDown);
            // 
            // chktype
            // 
            this.chktype.AutoSize = true;
            this.chktype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chktype.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chktype.Location = new System.Drawing.Point(496, 30);
            this.chktype.Margin = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this.chktype.Name = "chktype";
            this.chktype.Size = new System.Drawing.Size(93, 24);
            this.chktype.TabIndex = 23;
            this.chktype.Text = "Type";
            this.chktype.UseVisualStyleBackColor = true;
            this.chktype.CheckedChanged += new System.EventHandler(this.chktype_CheckedChanged);
            this.chktype.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chktype_KeyDown);
            // 
            // chkdate
            // 
            this.chkdate.AutoSize = true;
            this.chkdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkdate.Location = new System.Drawing.Point(310, 30);
            this.chkdate.Margin = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this.chkdate.Name = "chkdate";
            this.chkdate.Size = new System.Drawing.Size(68, 24);
            this.chkdate.TabIndex = 21;
            this.chkdate.Text = "Date";
            this.chkdate.UseVisualStyleBackColor = true;
            this.chkdate.CheckedChanged += new System.EventHandler(this.chkdate_CheckedChanged);
            this.chkdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkdate_KeyDown);
            // 
            // dtpdate
            // 
            this.dtpdate.CustomFormat = "dd/MM/yyyy";
            this.dtpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpdate.Location = new System.Drawing.Point(382, 30);
            this.dtpdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpdate.Name = "dtpdate";
            this.dtpdate.Size = new System.Drawing.Size(106, 25);
            this.dtpdate.TabIndex = 22;
            this.dtpdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpreceiptDate_KeyDown);
            // 
            // lblRemAmt
            // 
            this.lblRemAmt.AutoSize = true;
            this.lblRemAmt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRemAmt.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemAmt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblRemAmt.Location = new System.Drawing.Point(382, 2);
            this.lblRemAmt.Margin = new System.Windows.Forms.Padding(2);
            this.lblRemAmt.Name = "lblRemAmt";
            this.lblRemAmt.Size = new System.Drawing.Size(106, 24);
            this.lblRemAmt.TabIndex = 115;
            this.lblRemAmt.Text = "0";
            this.lblRemAmt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tblltListButtons
            // 
            this.tblltListButtons.ColumnCount = 2;
            this.tblltList.SetColumnSpan(this.tblltListButtons, 2);
            this.tblltListButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltListButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltListButtons.Controls.Add(this.btnprint1, 0, 0);
            this.tblltListButtons.Controls.Add(this.btnsearch, 0, 0);
            this.tblltListButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltListButtons.Location = new System.Drawing.Point(701, 28);
            this.tblltListButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltListButtons.Name = "tblltListButtons";
            this.tblltListButtons.RowCount = 1;
            this.tblltListButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltListButtons.Size = new System.Drawing.Size(147, 28);
            this.tblltListButtons.TabIndex = 25;
            // 
            // btnprint1
            // 
            this.btnprint1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprint1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprint1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnprint1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint1.ForeColor = System.Drawing.Color.White;
            this.btnprint1.Location = new System.Drawing.Point(75, 1);
            this.btnprint1.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnprint1.Name = "btnprint1";
            this.btnprint1.Size = new System.Drawing.Size(70, 26);
            this.btnprint1.TabIndex = 26;
            this.btnprint1.Text = "Print";
            this.btnprint1.UseVisualStyleBackColor = false;
            this.btnprint1.Click += new System.EventHandler(this.btnprint1_Click);
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(2, 1);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(69, 26);
            this.btnsearch.TabIndex = 25;
            this.btnsearch.Text = "Search";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.grpList, 0, 2);
            this.tblltMain.Controls.Add(this.label9, 0, 0);
            this.tblltMain.Controls.Add(this.grpReceipt, 0, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56F));
            this.tblltMain.Size = new System.Drawing.Size(854, 542);
            this.tblltMain.TabIndex = 0;
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.tblltList);
            this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpList.Location = new System.Drawing.Point(3, 240);
            this.grpList.Name = "grpList";
            this.grpList.Padding = new System.Windows.Forms.Padding(0);
            this.grpList.Size = new System.Drawing.Size(848, 299);
            this.grpList.TabIndex = 14;
            this.grpList.TabStop = false;
            this.grpList.Text = "Proprietor Receipt List";
            // 
            // grpReceipt
            // 
            this.grpReceipt.Controls.Add(this.tblltReceipt);
            this.grpReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpReceipt.Location = new System.Drawing.Point(3, 40);
            this.grpReceipt.Name = "grpReceipt";
            this.grpReceipt.Padding = new System.Windows.Forms.Padding(0);
            this.grpReceipt.Size = new System.Drawing.Size(848, 194);
            this.grpReceipt.TabIndex = 0;
            this.grpReceipt.TabStop = false;
            this.grpReceipt.Text = "Proprietor Receipt";
            // 
            // tblltReceipt
            // 
            this.tblltReceipt.ColumnCount = 7;
            this.tblltReceipt.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltReceipt.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tblltReceipt.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tblltReceipt.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltReceipt.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltReceipt.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tblltReceipt.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltReceipt.Controls.Add(this.tblltRow1, 1, 0);
            this.tblltReceipt.Controls.Add(this.label2, 4, 1);
            this.tblltReceipt.Controls.Add(this.label4, 2, 2);
            this.tblltReceipt.Controls.Add(this.txtparticular, 1, 4);
            this.tblltReceipt.Controls.Add(this.label7, 0, 0);
            this.tblltReceipt.Controls.Add(this.label1, 0, 1);
            this.tblltReceipt.Controls.Add(this.label14, 4, 0);
            this.tblltReceipt.Controls.Add(this.label15, 4, 3);
            this.tblltReceipt.Controls.Add(this.txtchequeno, 5, 3);
            this.tblltReceipt.Controls.Add(this.label13, 4, 2);
            this.tblltReceipt.Controls.Add(this.cmbProprietor, 1, 1);
            this.tblltReceipt.Controls.Add(this.label3, 0, 4);
            this.tblltReceipt.Controls.Add(this.dtpchequedate, 5, 2);
            this.tblltReceipt.Controls.Add(this.cmbTransactiontype, 1, 2);
            this.tblltReceipt.Controls.Add(this.label6, 0, 3);
            this.tblltReceipt.Controls.Add(this.label8, 2, 1);
            this.tblltReceipt.Controls.Add(this.cmbbank, 5, 1);
            this.tblltReceipt.Controls.Add(this.label10, 6, 0);
            this.tblltReceipt.Controls.Add(this.label5, 0, 2);
            this.tblltReceipt.Controls.Add(this.cmbpaymentmode, 5, 0);
            this.tblltReceipt.Controls.Add(this.btnProprietorForm, 3, 1);
            this.tblltReceipt.Controls.Add(this.txtPaidAmt, 1, 3);
            this.tblltReceipt.Controls.Add(this.tblltButtons, 4, 5);
            this.tblltReceipt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltReceipt.Location = new System.Drawing.Point(0, 18);
            this.tblltReceipt.Name = "tblltReceipt";
            this.tblltReceipt.RowCount = 6;
            this.tblltReceipt.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltReceipt.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltReceipt.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltReceipt.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltReceipt.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltReceipt.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltReceipt.Size = new System.Drawing.Size(848, 176);
            this.tblltReceipt.TabIndex = 0;
            // 
            // tblltRow1
            // 
            this.tblltRow1.ColumnCount = 3;
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblltRow1.Controls.Add(this.dtpreceiptDate, 0, 0);
            this.tblltRow1.Controls.Add(this.txtreceiptno, 2, 0);
            this.tblltRow1.Controls.Add(this.label12, 1, 0);
            this.tblltRow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow1.Location = new System.Drawing.Point(127, 0);
            this.tblltRow1.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow1.Name = "tblltRow1";
            this.tblltRow1.RowCount = 1;
            this.tblltRow1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow1.Size = new System.Drawing.Size(279, 29);
            this.tblltRow1.TabIndex = 0;
            // 
            // btnProprietorForm
            // 
            this.btnProprietorForm.BackgroundImage = global::AIOInventorySystem.Desk.Properties.Resources.newbutton;
            this.btnProprietorForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnProprietorForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnProprietorForm.FlatAppearance.BorderSize = 0;
            this.btnProprietorForm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnProprietorForm.Location = new System.Drawing.Point(433, 31);
            this.btnProprietorForm.Margin = new System.Windows.Forms.Padding(2);
            this.btnProprietorForm.Name = "btnProprietorForm";
            this.btnProprietorForm.Size = new System.Drawing.Size(29, 25);
            this.btnProprietorForm.TabIndex = 2;
            this.btnProprietorForm.UseVisualStyleBackColor = true;
            this.btnProprietorForm.Click += new System.EventHandler(this.btnProprietorForm_Click);
            // 
            // txtPaidAmt
            // 
            this.txtPaidAmt.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtPaidAmt.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPaidAmt.Location = new System.Drawing.Point(129, 89);
            this.txtPaidAmt.Margin = new System.Windows.Forms.Padding(2);
            this.txtPaidAmt.Name = "txtPaidAmt";
            this.txtPaidAmt.Size = new System.Drawing.Size(100, 25);
            this.txtPaidAmt.TabIndex = 4;
            this.txtPaidAmt.Text = "0";
            this.txtPaidAmt.TextChanged += new System.EventHandler(this.txtPaidAmt_TextChanged);
            this.txtPaidAmt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPaidAmt_KeyDown);
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 6;
            this.tblltReceipt.SetColumnSpan(this.tblltButtons, 3);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.Controls.Add(this.btnnew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnSave, 2, 0);
            this.tblltButtons.Controls.Add(this.btndelete, 3, 0);
            this.tblltButtons.Controls.Add(this.btnclose, 4, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(464, 145);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(384, 31);
            this.tblltButtons.TabIndex = 10;
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(65, 2);
            this.btnnew.Margin = new System.Windows.Forms.Padding(2);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(59, 27);
            this.btnnew.TabIndex = 11;
            this.btnnew.Text = "New";
            this.btnnew.UseVisualStyleBackColor = false;
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(128, 2);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(59, 27);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btndelete
            // 
            this.btndelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btndelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btndelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btndelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.Location = new System.Drawing.Point(191, 2);
            this.btndelete.Margin = new System.Windows.Forms.Padding(2);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(59, 27);
            this.btndelete.TabIndex = 12;
            this.btndelete.Text = "Delete";
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnclose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnclose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(254, 2);
            this.btnclose.Margin = new System.Windows.Forms.Padding(2);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(59, 27);
            this.btnclose.TabIndex = 13;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // frmProprietorLedger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(854, 542);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmProprietorLedger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proprietor Ledger";
            ((System.ComponentModel.ISupportInitialize)(this.GvreceiptInfo)).EndInit();
            this.tblltList.ResumeLayout(false);
            this.tblltList.PerformLayout();
            this.tblltListButtons.ResumeLayout(false);
            this.tblltMain.ResumeLayout(false);
            this.grpList.ResumeLayout(false);
            this.grpReceipt.ResumeLayout(false);
            this.tblltReceipt.ResumeLayout(false);
            this.tblltReceipt.PerformLayout();
            this.tblltRow1.ResumeLayout(false);
            this.tblltRow1.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView GvreceiptInfo;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbpaymentmode;
        private System.Windows.Forms.ComboBox cmbbank;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpreceiptDate;
        private System.Windows.Forms.TextBox txtreceiptno;
        private System.Windows.Forms.ComboBox cmbProprietor;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpchequedate;
        private System.Windows.Forms.TextBox txtchequeno;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbTransactiontype;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tblltList;
        private System.Windows.Forms.CheckBox chkProprietorname1;
        private System.Windows.Forms.CheckBox chkbankname;
        private System.Windows.Forms.DateTimePicker dtptodate;
        private System.Windows.Forms.ComboBox cmbProprietorname1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cmbbank2;
        private System.Windows.Forms.ComboBox cmbtype;
        private System.Windows.Forms.DateTimePicker dtpfromdate;
        private System.Windows.Forms.Label lblAmtText;
        private System.Windows.Forms.CheckBox chkbetdate;
        private System.Windows.Forms.CheckBox chktype;
        private System.Windows.Forms.CheckBox chkdate;
        private System.Windows.Forms.DateTimePicker dtpdate;
        private System.Windows.Forms.Label lblRemAmt;
        private System.Windows.Forms.TableLayoutPanel tblltListButtons;
        private System.Windows.Forms.TextBox txtparticular;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.GroupBox grpReceipt;
        private System.Windows.Forms.TableLayoutPanel tblltReceipt;
        private System.Windows.Forms.TableLayoutPanel tblltRow1;
        private System.Windows.Forms.Button btnProprietorForm;
        private RachitControls.NumericTextBox txtPaidAmt;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnprint1;
        private System.Windows.Forms.Button btnsearch;
    }
}