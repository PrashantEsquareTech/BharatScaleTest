namespace AIOInventorySystem.Desk.Forms
{
    partial class FrmProprietorMaster
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tblltInfo = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.cmbAccountGroup = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtPanno = new System.Windows.Forms.TextBox();
            this.tblltBalance = new System.Windows.Forms.TableLayoutPanel();
            this.dtpOpeningBalanceDate = new System.Windows.Forms.DateTimePicker();
            this.lblOpeningBalance = new System.Windows.Forms.Label();
            this.txtopeningBal = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.txtmobileno = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtProprietorID = new System.Windows.Forms.TextBox();
            this.tblltName = new System.Windows.Forms.TableLayoutPanel();
            this.txtProprietorName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblstate = new System.Windows.Forms.Label();
            this.txtAdharCardNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tblltAddress = new System.Windows.Forms.TableLayoutPanel();
            this.lblAmt = new System.Windows.Forms.Label();
            this.txtProprietorAddress = new System.Windows.Forms.TextBox();
            this.lblRemAmt = new System.Windows.Forms.Label();
            this.tblltList = new System.Windows.Forms.TableLayoutPanel();
            this.GvProprietorInfo = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProprietorIdg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProprietorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MobileNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PANNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AdharCardNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACGroupId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OpeningBalDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btngetall = new System.Windows.Forms.Button();
            this.cmbProprietorName = new System.Windows.Forms.ComboBox();
            this.lblTotaluCustomers = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.tblltInfo.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.tblltBalance.SuspendLayout();
            this.tblltName.SuspendLayout();
            this.tblltAddress.SuspendLayout();
            this.tblltList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvProprietorInfo)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpList.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // tblltInfo
            // 
            this.tblltInfo.ColumnCount = 2;
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74F));
            this.tblltInfo.Controls.Add(this.tblltButtons, 0, 9);
            this.tblltInfo.Controls.Add(this.cmbAccountGroup, 1, 7);
            this.tblltInfo.Controls.Add(this.label21, 0, 6);
            this.tblltInfo.Controls.Add(this.txtPanno, 1, 5);
            this.tblltInfo.Controls.Add(this.tblltBalance, 1, 2);
            this.tblltInfo.Controls.Add(this.txtmobileno, 1, 4);
            this.tblltInfo.Controls.Add(this.label1, 0, 0);
            this.tblltInfo.Controls.Add(this.label14, 0, 2);
            this.tblltInfo.Controls.Add(this.label3, 0, 3);
            this.tblltInfo.Controls.Add(this.label6, 0, 4);
            this.tblltInfo.Controls.Add(this.txtProprietorID, 1, 0);
            this.tblltInfo.Controls.Add(this.tblltName, 1, 1);
            this.tblltInfo.Controls.Add(this.label19, 0, 5);
            this.tblltInfo.Controls.Add(this.lblstate, 0, 7);
            this.tblltInfo.Controls.Add(this.txtAdharCardNo, 1, 6);
            this.tblltInfo.Controls.Add(this.label2, 0, 1);
            this.tblltInfo.Controls.Add(this.tblltAddress, 1, 3);
            this.tblltInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltInfo.Location = new System.Drawing.Point(3, 22);
            this.tblltInfo.Name = "tblltInfo";
            this.tblltInfo.RowCount = 10;
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltInfo.Size = new System.Drawing.Size(455, 321);
            this.tblltInfo.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 6;
            this.tblltInfo.SetColumnSpan(this.tblltButtons, 2);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.Controls.Add(this.btnnew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnSave, 2, 0);
            this.tblltButtons.Controls.Add(this.btndelete, 3, 0);
            this.tblltButtons.Controls.Add(this.btnClose, 4, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 281);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(455, 40);
            this.tblltButtons.TabIndex = 9;
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(77, 2);
            this.btnnew.Margin = new System.Windows.Forms.Padding(2);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(71, 36);
            this.btnnew.TabIndex = 10;
            this.btnnew.Text = "New";
            this.btnnew.UseVisualStyleBackColor = false;
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            this.btnnew.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnnew_KeyDown);
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
            this.btnSave.Size = new System.Drawing.Size(71, 36);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnSave_KeyDown);
            // 
            // btndelete
            // 
            this.btndelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btndelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btndelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btndelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.Location = new System.Drawing.Point(227, 2);
            this.btndelete.Margin = new System.Windows.Forms.Padding(2);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(71, 36);
            this.btndelete.TabIndex = 11;
            this.btndelete.Text = "Delete";
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            this.btndelete.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btndelete_KeyDown);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(302, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(71, 36);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnClose_KeyDown);
            // 
            // cmbAccountGroup
            // 
            this.cmbAccountGroup.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbAccountGroup.DropDownHeight = 150;
            this.cmbAccountGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccountGroup.DropDownWidth = 250;
            this.cmbAccountGroup.Enabled = false;
            this.cmbAccountGroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAccountGroup.FormattingEnabled = true;
            this.cmbAccountGroup.IntegralHeight = false;
            this.cmbAccountGroup.Location = new System.Drawing.Point(120, 227);
            this.cmbAccountGroup.Margin = new System.Windows.Forms.Padding(2);
            this.cmbAccountGroup.Name = "cmbAccountGroup";
            this.cmbAccountGroup.Size = new System.Drawing.Size(258, 25);
            this.cmbAccountGroup.TabIndex = 8;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(2, 199);
            this.label21.Margin = new System.Windows.Forms.Padding(2);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(114, 24);
            this.label21.TabIndex = 156;
            this.label21.Text = "Adhar Card No:";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPanno
            // 
            this.txtPanno.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtPanno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPanno.Location = new System.Drawing.Point(120, 171);
            this.txtPanno.Margin = new System.Windows.Forms.Padding(2);
            this.txtPanno.Name = "txtPanno";
            this.txtPanno.Size = new System.Drawing.Size(175, 25);
            this.txtPanno.TabIndex = 6;
            this.txtPanno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPanno_KeyDown);
            // 
            // tblltBalance
            // 
            this.tblltBalance.ColumnCount = 3;
            this.tblltBalance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.03835F));
            this.tblltBalance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.62832F));
            this.tblltBalance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblltBalance.Controls.Add(this.dtpOpeningBalanceDate, 2, 0);
            this.tblltBalance.Controls.Add(this.lblOpeningBalance, 1, 0);
            this.tblltBalance.Controls.Add(this.txtopeningBal, 0, 0);
            this.tblltBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltBalance.Location = new System.Drawing.Point(118, 56);
            this.tblltBalance.Margin = new System.Windows.Forms.Padding(0);
            this.tblltBalance.Name = "tblltBalance";
            this.tblltBalance.RowCount = 1;
            this.tblltBalance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltBalance.Size = new System.Drawing.Size(337, 28);
            this.tblltBalance.TabIndex = 2;
            // 
            // dtpOpeningBalanceDate
            // 
            this.dtpOpeningBalanceDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpOpeningBalanceDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpOpeningBalanceDate.Location = new System.Drawing.Point(226, 2);
            this.dtpOpeningBalanceDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpOpeningBalanceDate.Name = "dtpOpeningBalanceDate";
            this.dtpOpeningBalanceDate.Size = new System.Drawing.Size(109, 25);
            this.dtpOpeningBalanceDate.TabIndex = 3;
            this.dtpOpeningBalanceDate.Visible = false;
            this.dtpOpeningBalanceDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpOpeningBalanceDate_KeyDown);
            // 
            // lblOpeningBalance
            // 
            this.lblOpeningBalance.AutoSize = true;
            this.lblOpeningBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOpeningBalance.ForeColor = System.Drawing.Color.Black;
            this.lblOpeningBalance.Location = new System.Drawing.Point(113, 2);
            this.lblOpeningBalance.Margin = new System.Windows.Forms.Padding(2);
            this.lblOpeningBalance.Name = "lblOpeningBalance";
            this.lblOpeningBalance.Size = new System.Drawing.Size(109, 24);
            this.lblOpeningBalance.TabIndex = 5;
            this.lblOpeningBalance.Text = "Opening Date:";
            this.lblOpeningBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblOpeningBalance.Visible = false;
            // 
            // txtopeningBal
            // 
            this.txtopeningBal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtopeningBal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtopeningBal.Location = new System.Drawing.Point(2, 2);
            this.txtopeningBal.Margin = new System.Windows.Forms.Padding(2);
            this.txtopeningBal.Name = "txtopeningBal";
            this.txtopeningBal.Size = new System.Drawing.Size(107, 25);
            this.txtopeningBal.TabIndex = 2;
            this.txtopeningBal.TextChanged += new System.EventHandler(this.txtopeningBal_TextChanged);
            this.txtopeningBal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtopeningBal_KeyDown);
            // 
            // txtmobileno
            // 
            this.txtmobileno.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtmobileno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtmobileno.ForeColor = System.Drawing.Color.Black;
            this.txtmobileno.Location = new System.Drawing.Point(120, 143);
            this.txtmobileno.Margin = new System.Windows.Forms.Padding(2);
            this.txtmobileno.Name = "txtmobileno";
            this.txtmobileno.Size = new System.Drawing.Size(225, 25);
            this.txtmobileno.TabIndex = 5;
            this.txtmobileno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtmobileno_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 24);
            this.label1.TabIndex = 9;
            this.label1.Text = "Proprietor ID:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(2, 58);
            this.label14.Margin = new System.Windows.Forms.Padding(2);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(114, 24);
            this.label14.TabIndex = 137;
            this.label14.Text = "Opening Balance:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(2, 86);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 53);
            this.label3.TabIndex = 11;
            this.label3.Text = "Address:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(2, 143);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 24);
            this.label6.TabIndex = 42;
            this.label6.Text = "Mobile No:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProprietorID
            // 
            this.txtProprietorID.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtProprietorID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProprietorID.ForeColor = System.Drawing.Color.Black;
            this.txtProprietorID.Location = new System.Drawing.Point(120, 2);
            this.txtProprietorID.Margin = new System.Windows.Forms.Padding(2);
            this.txtProprietorID.Name = "txtProprietorID";
            this.txtProprietorID.ReadOnly = true;
            this.txtProprietorID.Size = new System.Drawing.Size(100, 25);
            this.txtProprietorID.TabIndex = 0;
            this.txtProprietorID.TabStop = false;
            // 
            // tblltName
            // 
            this.tblltName.ColumnCount = 2;
            this.tblltName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.4054F));
            this.tblltName.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.59459F));
            this.tblltName.Controls.Add(this.txtProprietorName, 0, 0);
            this.tblltName.Controls.Add(this.label7, 1, 0);
            this.tblltName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltName.Location = new System.Drawing.Point(118, 28);
            this.tblltName.Margin = new System.Windows.Forms.Padding(0);
            this.tblltName.Name = "tblltName";
            this.tblltName.RowCount = 1;
            this.tblltName.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltName.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tblltName.Size = new System.Drawing.Size(337, 28);
            this.tblltName.TabIndex = 1;
            this.tblltName.TabStop = true;
            // 
            // txtProprietorName
            // 
            this.txtProprietorName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtProprietorName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtProprietorName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProprietorName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProprietorName.ForeColor = System.Drawing.Color.Black;
            this.txtProprietorName.Location = new System.Drawing.Point(2, 2);
            this.txtProprietorName.Margin = new System.Windows.Forms.Padding(2);
            this.txtProprietorName.Name = "txtProprietorName";
            this.txtProprietorName.Size = new System.Drawing.Size(250, 25);
            this.txtProprietorName.TabIndex = 1;
            this.txtProprietorName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rButton1_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(254, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 28);
            this.label7.TabIndex = 43;
            this.label7.Text = "*";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(2, 171);
            this.label19.Margin = new System.Windows.Forms.Padding(2);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(114, 24);
            this.label19.TabIndex = 148;
            this.label19.Text = "PAN No:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblstate
            // 
            this.lblstate.AutoSize = true;
            this.lblstate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblstate.ForeColor = System.Drawing.Color.Black;
            this.lblstate.Location = new System.Drawing.Point(2, 227);
            this.lblstate.Margin = new System.Windows.Forms.Padding(2);
            this.lblstate.Name = "lblstate";
            this.lblstate.Size = new System.Drawing.Size(114, 24);
            this.lblstate.TabIndex = 153;
            this.lblstate.Text = "Account Group:";
            this.lblstate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAdharCardNo
            // 
            this.txtAdharCardNo.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtAdharCardNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdharCardNo.Location = new System.Drawing.Point(120, 199);
            this.txtAdharCardNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtAdharCardNo.Name = "txtAdharCardNo";
            this.txtAdharCardNo.Size = new System.Drawing.Size(175, 25);
            this.txtAdharCardNo.TabIndex = 7;
            this.txtAdharCardNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAdharCardNo_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(2, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 24);
            this.label2.TabIndex = 10;
            this.label2.Text = "Proprietor Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblltAddress
            // 
            this.tblltAddress.ColumnCount = 3;
            this.tblltAddress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53F));
            this.tblltAddress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tblltAddress.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tblltAddress.Controls.Add(this.lblAmt, 2, 0);
            this.tblltAddress.Controls.Add(this.txtProprietorAddress, 0, 0);
            this.tblltAddress.Controls.Add(this.lblRemAmt, 1, 0);
            this.tblltAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltAddress.Location = new System.Drawing.Point(118, 84);
            this.tblltAddress.Margin = new System.Windows.Forms.Padding(0);
            this.tblltAddress.Name = "tblltAddress";
            this.tblltAddress.RowCount = 1;
            this.tblltAddress.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltAddress.Size = new System.Drawing.Size(337, 57);
            this.tblltAddress.TabIndex = 4;
            // 
            // lblAmt
            // 
            this.lblAmt.AutoSize = true;
            this.lblAmt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAmt.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblAmt.Location = new System.Drawing.Point(260, 2);
            this.lblAmt.Margin = new System.Windows.Forms.Padding(2);
            this.lblAmt.Name = "lblAmt";
            this.lblAmt.Size = new System.Drawing.Size(75, 53);
            this.lblAmt.TabIndex = 135;
            this.lblAmt.Text = "0";
            // 
            // txtProprietorAddress
            // 
            this.txtProprietorAddress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProprietorAddress.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProprietorAddress.ForeColor = System.Drawing.Color.Black;
            this.txtProprietorAddress.Location = new System.Drawing.Point(2, 2);
            this.txtProprietorAddress.Margin = new System.Windows.Forms.Padding(2);
            this.txtProprietorAddress.Multiline = true;
            this.txtProprietorAddress.Name = "txtProprietorAddress";
            this.txtProprietorAddress.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtProprietorAddress.Size = new System.Drawing.Size(174, 53);
            this.txtProprietorAddress.TabIndex = 4;
            this.txtProprietorAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCustomerAddress_KeyDown);
            // 
            // lblRemAmt
            // 
            this.lblRemAmt.AutoSize = true;
            this.lblRemAmt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRemAmt.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemAmt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblRemAmt.Location = new System.Drawing.Point(180, 2);
            this.lblRemAmt.Margin = new System.Windows.Forms.Padding(2);
            this.lblRemAmt.Name = "lblRemAmt";
            this.lblRemAmt.Size = new System.Drawing.Size(76, 53);
            this.lblRemAmt.TabIndex = 134;
            this.lblRemAmt.Text = "Rem. Amt:";
            this.lblRemAmt.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tblltList
            // 
            this.tblltList.ColumnCount = 3;
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltList.Controls.Add(this.GvProprietorInfo, 0, 2);
            this.tblltList.Controls.Add(this.btngetall, 2, 1);
            this.tblltList.Controls.Add(this.cmbProprietorName, 1, 1);
            this.tblltList.Controls.Add(this.lblTotaluCustomers, 1, 0);
            this.tblltList.Controls.Add(this.label15, 0, 0);
            this.tblltList.Controls.Add(this.label9, 0, 1);
            this.tblltList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltList.Location = new System.Drawing.Point(3, 21);
            this.tblltList.Name = "tblltList";
            this.tblltList.RowCount = 3;
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82F));
            this.tblltList.Size = new System.Drawing.Size(455, 323);
            this.tblltList.TabIndex = 15;
            // 
            // GvProprietorInfo
            // 
            this.GvProprietorInfo.AllowUserToAddRows = false;
            this.GvProprietorInfo.AllowUserToDeleteRows = false;
            this.GvProprietorInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvProprietorInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvProprietorInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GvProprietorInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvProprietorInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.ProprietorIdg,
            this.ProprietorName,
            this.Address,
            this.MobileNo,
            this.PANNo,
            this.AdharCardNo,
            this.ACGroupId,
            this.OpeningBalDate});
            this.tblltList.SetColumnSpan(this.GvProprietorInfo, 3);
            this.GvProprietorInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvProprietorInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvProprietorInfo.Location = new System.Drawing.Point(2, 60);
            this.GvProprietorInfo.Margin = new System.Windows.Forms.Padding(2);
            this.GvProprietorInfo.Name = "GvProprietorInfo";
            this.GvProprietorInfo.ReadOnly = true;
            this.GvProprietorInfo.RowHeadersWidth = 15;
            this.GvProprietorInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvProprietorInfo.Size = new System.Drawing.Size(451, 261);
            this.GvProprietorInfo.TabIndex = 18;
            this.GvProprietorInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvProprietorInfo_CellClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // ProprietorIdg
            // 
            this.ProprietorIdg.HeaderText = "Proprietor Id";
            this.ProprietorIdg.Name = "ProprietorIdg";
            this.ProprietorIdg.ReadOnly = true;
            this.ProprietorIdg.Visible = false;
            // 
            // ProprietorName
            // 
            this.ProprietorName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ProprietorName.FillWeight = 243.6548F;
            this.ProprietorName.HeaderText = "Proprietor Name";
            this.ProprietorName.Name = "ProprietorName";
            this.ProprietorName.ReadOnly = true;
            this.ProprietorName.Width = 140;
            // 
            // Address
            // 
            this.Address.FillWeight = 52.11506F;
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            this.Address.ReadOnly = true;
            // 
            // MobileNo
            // 
            this.MobileNo.FillWeight = 52.11506F;
            this.MobileNo.HeaderText = "Mobile No";
            this.MobileNo.Name = "MobileNo";
            this.MobileNo.ReadOnly = true;
            // 
            // PANNo
            // 
            this.PANNo.FillWeight = 52.11506F;
            this.PANNo.HeaderText = "PAN No";
            this.PANNo.Name = "PANNo";
            this.PANNo.ReadOnly = true;
            // 
            // AdharCardNo
            // 
            this.AdharCardNo.HeaderText = "Adhar Card No";
            this.AdharCardNo.Name = "AdharCardNo";
            this.AdharCardNo.ReadOnly = true;
            this.AdharCardNo.Visible = false;
            // 
            // ACGroupId
            // 
            this.ACGroupId.HeaderText = "AC Group Id";
            this.ACGroupId.Name = "ACGroupId";
            this.ACGroupId.ReadOnly = true;
            this.ACGroupId.Visible = false;
            // 
            // OpeningBalDate
            // 
            this.OpeningBalDate.HeaderText = "Opening Bal. Date";
            this.OpeningBalDate.Name = "OpeningBalDate";
            this.OpeningBalDate.ReadOnly = true;
            this.OpeningBalDate.Visible = false;
            // 
            // btngetall
            // 
            this.btngetall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btngetall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btngetall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btngetall.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngetall.ForeColor = System.Drawing.Color.White;
            this.btngetall.Location = new System.Drawing.Point(388, 31);
            this.btngetall.Margin = new System.Windows.Forms.Padding(2);
            this.btngetall.Name = "btngetall";
            this.btngetall.Size = new System.Drawing.Size(65, 25);
            this.btngetall.TabIndex = 17;
            this.btngetall.Text = "Get All";
            this.btngetall.UseVisualStyleBackColor = false;
            this.btngetall.Click += new System.EventHandler(this.btngetall_Click);
            // 
            // cmbProprietorName
            // 
            this.cmbProprietorName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbProprietorName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbProprietorName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbProprietorName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProprietorName.FormattingEnabled = true;
            this.cmbProprietorName.Location = new System.Drawing.Point(156, 31);
            this.cmbProprietorName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbProprietorName.Name = "cmbProprietorName";
            this.cmbProprietorName.Size = new System.Drawing.Size(228, 25);
            this.cmbProprietorName.TabIndex = 16;
            this.cmbProprietorName.SelectedIndexChanged += new System.EventHandler(this.cmbcustomername_SelectedIndexChanged);
            this.cmbProprietorName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcustomername_KeyDown);
            // 
            // lblTotaluCustomers
            // 
            this.lblTotaluCustomers.AutoSize = true;
            this.lblTotaluCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotaluCustomers.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotaluCustomers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotaluCustomers.Location = new System.Drawing.Point(156, 2);
            this.lblTotaluCustomers.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotaluCustomers.Name = "lblTotaluCustomers";
            this.lblTotaluCustomers.Size = new System.Drawing.Size(228, 25);
            this.lblTotaluCustomers.TabIndex = 50;
            this.lblTotaluCustomers.Text = "0";
            this.lblTotaluCustomers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label15.Location = new System.Drawing.Point(2, 2);
            this.label15.Margin = new System.Windows.Forms.Padding(2);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(150, 25);
            this.label15.TabIndex = 49;
            this.label15.Text = "Total Proprietors:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(2, 31);
            this.label9.Margin = new System.Windows.Forms.Padding(2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(150, 25);
            this.label9.TabIndex = 40;
            this.label9.Text = "Proprietor Name:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label5, 2);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(934, 39);
            this.label5.TabIndex = 12;
            this.label5.Text = "Proprietor Information";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 2;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.groupBox1, 0, 1);
            this.tblltMain.Controls.Add(this.grpList, 1, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 2;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.10101F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.89899F));
            this.tblltMain.Size = new System.Drawing.Size(934, 392);
            this.tblltMain.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tblltInfo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(461, 347);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Proprietor Information";
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.tblltList);
            this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpList.Location = new System.Drawing.Point(470, 42);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(461, 347);
            this.grpList.TabIndex = 14;
            this.grpList.TabStop = false;
            this.grpList.Text = "Proprietor List";
            // 
            // FrmProprietorMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(934, 392);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FrmProprietorMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Proprietor Master";
            this.tblltInfo.ResumeLayout(false);
            this.tblltInfo.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.tblltBalance.ResumeLayout(false);
            this.tblltBalance.PerformLayout();
            this.tblltName.ResumeLayout(false);
            this.tblltName.PerformLayout();
            this.tblltAddress.ResumeLayout(false);
            this.tblltAddress.PerformLayout();
            this.tblltList.ResumeLayout(false);
            this.tblltList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvProprietorInfo)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.grpList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblltBalance;
        private System.Windows.Forms.Label lblAmt;
        private System.Windows.Forms.Label lblRemAmt;
        private System.Windows.Forms.TextBox txtmobileno;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtProprietorAddress;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TableLayoutPanel tblltInfo;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtPanno;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtProprietorID;
        private System.Windows.Forms.TableLayoutPanel tblltName;
        private System.Windows.Forms.TextBox txtProprietorName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblstate;
        private System.Windows.Forms.TextBox txtAdharCardNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tblltList;
        private System.Windows.Forms.ComboBox cmbProprietorName;
        private System.Windows.Forms.Label lblTotaluCustomers;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbAccountGroup;
        private System.Windows.Forms.TableLayoutPanel tblltAddress;
        private System.Windows.Forms.DateTimePicker dtpOpeningBalanceDate;
        private System.Windows.Forms.Label lblOpeningBalance;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btngetall;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.DataGridView GvProprietorInfo;
        private RachitControls.NumericTextBox txtopeningBal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProprietorIdg;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProprietorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn MobileNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PANNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn AdharCardNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACGroupId;
        private System.Windows.Forms.DataGridViewTextBoxColumn OpeningBalDate;
    }
}