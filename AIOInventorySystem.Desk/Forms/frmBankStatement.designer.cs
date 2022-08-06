namespace AIOInventorySystem.Desk.Forms
{
    partial class frmBankStatement
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
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.tblltList = new System.Windows.Forms.TableLayoutPanel();
            this.GvreceiptInfo = new System.Windows.Forms.DataGridView();
            this.tblltListRow2 = new System.Windows.Forms.TableLayoutPanel();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnsearch = new System.Windows.Forms.Button();
            this.chkbankname = new System.Windows.Forms.CheckBox();
            this.cmbsbankname1 = new System.Windows.Forms.ComboBox();
            this.chkfromdate = new System.Windows.Forms.CheckBox();
            this.dtpfrom = new System.Windows.Forms.DateTimePicker();
            this.dtpto = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnaddopening = new System.Windows.Forms.Button();
            this.txtchequeno = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkcheque = new System.Windows.Forms.CheckBox();
            this.cmbcustorsupp = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbtransaction = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtparticular = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblcustorsupp = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.dtpdate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbbankname = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cmbACGroups = new System.Windows.Forms.ComboBox();
            this.txtAmount = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.tblltMain.SuspendLayout();
            this.grpList.SuspendLayout();
            this.tblltList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvreceiptInfo)).BeginInit();
            this.tblltListRow2.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 6;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tblltMain.Controls.Add(this.grpList, 0, 6);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.tblltButtons, 3, 5);
            this.tblltMain.Controls.Add(this.txtchequeno, 4, 3);
            this.tblltMain.Controls.Add(this.label6, 3, 4);
            this.tblltMain.Controls.Add(this.chkcheque, 3, 3);
            this.tblltMain.Controls.Add(this.cmbcustorsupp, 4, 2);
            this.tblltMain.Controls.Add(this.label11, 5, 3);
            this.tblltMain.Controls.Add(this.cmbtransaction, 4, 1);
            this.tblltMain.Controls.Add(this.label8, 5, 1);
            this.tblltMain.Controls.Add(this.label10, 5, 2);
            this.tblltMain.Controls.Add(this.txtparticular, 1, 3);
            this.tblltMain.Controls.Add(this.label9, 3, 1);
            this.tblltMain.Controls.Add(this.label7, 2, 1);
            this.tblltMain.Controls.Add(this.lblcustorsupp, 3, 2);
            this.tblltMain.Controls.Add(this.label19, 0, 3);
            this.tblltMain.Controls.Add(this.dtpdate, 1, 5);
            this.tblltMain.Controls.Add(this.label4, 0, 5);
            this.tblltMain.Controls.Add(this.label3, 0, 2);
            this.tblltMain.Controls.Add(this.cmbbankname, 1, 2);
            this.tblltMain.Controls.Add(this.label16, 2, 2);
            this.tblltMain.Controls.Add(this.label17, 0, 1);
            this.tblltMain.Controls.Add(this.cmbACGroups, 1, 1);
            this.tblltMain.Controls.Add(this.txtAmount, 4, 4);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 7;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65.5F));
            this.tblltMain.Size = new System.Drawing.Size(784, 532);
            this.tblltMain.TabIndex = 0;
            this.tblltMain.TabStop = true;
            // 
            // grpList
            // 
            this.tblltMain.SetColumnSpan(this.grpList, 6);
            this.grpList.Controls.Add(this.tblltList);
            this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpList.Location = new System.Drawing.Point(3, 185);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(778, 344);
            this.grpList.TabIndex = 14;
            this.grpList.TabStop = false;
            this.grpList.Text = "Bank Statement List";
            // 
            // tblltList
            // 
            this.tblltList.ColumnCount = 6;
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltList.Controls.Add(this.GvreceiptInfo, 0, 2);
            this.tblltList.Controls.Add(this.tblltListRow2, 0, 1);
            this.tblltList.Controls.Add(this.chkbankname, 0, 0);
            this.tblltList.Controls.Add(this.cmbsbankname1, 1, 0);
            this.tblltList.Controls.Add(this.chkfromdate, 2, 0);
            this.tblltList.Controls.Add(this.dtpfrom, 3, 0);
            this.tblltList.Controls.Add(this.dtpto, 5, 0);
            this.tblltList.Controls.Add(this.label13, 4, 0);
            this.tblltList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltList.Location = new System.Drawing.Point(3, 21);
            this.tblltList.Name = "tblltList";
            this.tblltList.RowCount = 3;
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82F));
            this.tblltList.Size = new System.Drawing.Size(772, 320);
            this.tblltList.TabIndex = 14;
            // 
            // GvreceiptInfo
            // 
            this.GvreceiptInfo.AllowUserToAddRows = false;
            this.GvreceiptInfo.AllowUserToDeleteRows = false;
            this.GvreceiptInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GvreceiptInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvreceiptInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvreceiptInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvreceiptInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvreceiptInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblltList.SetColumnSpan(this.GvreceiptInfo, 6);
            this.GvreceiptInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvreceiptInfo.Location = new System.Drawing.Point(3, 59);
            this.GvreceiptInfo.Name = "GvreceiptInfo";
            this.GvreceiptInfo.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvreceiptInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GvreceiptInfo.RowHeadersWidth = 10;
            this.GvreceiptInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvreceiptInfo.Size = new System.Drawing.Size(766, 258);
            this.GvreceiptInfo.TabIndex = 22;
            this.GvreceiptInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvreceiptInfo_CellClick);
            // 
            // tblltListRow2
            // 
            this.tblltListRow2.ColumnCount = 7;
            this.tblltList.SetColumnSpan(this.tblltListRow2, 6);
            this.tblltListRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltListRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.40816F));
            this.tblltListRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltListRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.40816F));
            this.tblltListRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.20408F));
            this.tblltListRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.20408F));
            this.tblltListRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.20408F));
            this.tblltListRow2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblltListRow2.Controls.Add(this.label14, 0, 0);
            this.tblltListRow2.Controls.Add(this.label12, 3, 0);
            this.tblltListRow2.Controls.Add(this.label2, 2, 0);
            this.tblltListRow2.Controls.Add(this.label15, 1, 0);
            this.tblltListRow2.Controls.Add(this.btnprint, 6, 0);
            this.tblltListRow2.Controls.Add(this.btnsearch, 5, 0);
            this.tblltListRow2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltListRow2.Location = new System.Drawing.Point(0, 28);
            this.tblltListRow2.Margin = new System.Windows.Forms.Padding(0);
            this.tblltListRow2.Name = "tblltListRow2";
            this.tblltListRow2.RowCount = 1;
            this.tblltListRow2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltListRow2.Size = new System.Drawing.Size(772, 28);
            this.tblltListRow2.TabIndex = 19;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label14.Location = new System.Drawing.Point(2, 2);
            this.label14.Margin = new System.Windows.Forms.Padding(2);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(106, 24);
            this.label14.TabIndex = 116;
            this.label14.Text = "Opening  Amt.:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label14.Visible = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label12.Location = new System.Drawing.Point(379, 2);
            this.label12.Margin = new System.Windows.Forms.Padding(2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(153, 24);
            this.label12.TabIndex = 111;
            this.label12.Text = "0";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(269, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 24);
            this.label2.TabIndex = 110;
            this.label2.Text = "Bal. Amt.:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label15.Location = new System.Drawing.Point(112, 2);
            this.label15.Margin = new System.Windows.Forms.Padding(2);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(153, 24);
            this.label15.TabIndex = 117;
            this.label15.Text = "0";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label15.Visible = false;
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(692, 2);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(78, 24);
            this.btnprint.TabIndex = 20;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(614, 2);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(74, 24);
            this.btnsearch.TabIndex = 19;
            this.btnsearch.Text = "Search";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // chkbankname
            // 
            this.chkbankname.AutoSize = true;
            this.chkbankname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkbankname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbankname.Location = new System.Drawing.Point(15, 3);
            this.chkbankname.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
            this.chkbankname.Name = "chkbankname";
            this.chkbankname.Size = new System.Drawing.Size(97, 22);
            this.chkbankname.TabIndex = 14;
            this.chkbankname.Text = "Bank Name";
            this.chkbankname.UseVisualStyleBackColor = true;
            this.chkbankname.CheckedChanged += new System.EventHandler(this.chkbankname_CheckedChanged);
            this.chkbankname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkbankname_KeyDown);
            // 
            // cmbsbankname1
            // 
            this.cmbsbankname1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbsbankname1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbsbankname1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbsbankname1.FormattingEnabled = true;
            this.cmbsbankname1.Location = new System.Drawing.Point(117, 2);
            this.cmbsbankname1.Margin = new System.Windows.Forms.Padding(2);
            this.cmbsbankname1.Name = "cmbsbankname1";
            this.cmbsbankname1.Size = new System.Drawing.Size(204, 25);
            this.cmbsbankname1.TabIndex = 15;
            this.cmbsbankname1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbsbankname1_KeyDown);
            // 
            // chkfromdate
            // 
            this.chkfromdate.AutoSize = true;
            this.chkfromdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkfromdate.Location = new System.Drawing.Point(338, 3);
            this.chkfromdate.Margin = new System.Windows.Forms.Padding(15, 3, 3, 3);
            this.chkfromdate.Name = "chkfromdate";
            this.chkfromdate.Size = new System.Drawing.Size(97, 22);
            this.chkfromdate.TabIndex = 16;
            this.chkfromdate.Text = "From Date";
            this.chkfromdate.UseVisualStyleBackColor = true;
            this.chkfromdate.CheckedChanged += new System.EventHandler(this.chkfromdate_CheckedChanged);
            this.chkfromdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkfromdate_KeyDown);
            // 
            // dtpfrom
            // 
            this.dtpfrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpfrom.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfrom.Location = new System.Drawing.Point(440, 2);
            this.dtpfrom.Margin = new System.Windows.Forms.Padding(2);
            this.dtpfrom.Name = "dtpfrom";
            this.dtpfrom.Size = new System.Drawing.Size(111, 25);
            this.dtpfrom.TabIndex = 17;
            this.dtpfrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpfrom_KeyDown);
            // 
            // dtpto
            // 
            this.dtpto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpto.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpto.Location = new System.Drawing.Point(655, 2);
            this.dtpto.Margin = new System.Windows.Forms.Padding(2);
            this.dtpto.Name = "dtpto";
            this.dtpto.Size = new System.Drawing.Size(115, 25);
            this.dtpto.TabIndex = 18;
            this.dtpto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpto_KeyDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(556, 3);
            this.label13.Margin = new System.Windows.Forms.Padding(3);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(94, 22);
            this.label13.TabIndex = 115;
            this.label13.Text = "To Date";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.label5.Size = new System.Drawing.Size(784, 37);
            this.label5.TabIndex = 36;
            this.label5.Text = "Bank Statement";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 7;
            this.tblltMain.SetColumnSpan(this.tblltButtons, 3);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltButtons.Controls.Add(this.btnnew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnSave, 2, 0);
            this.tblltButtons.Controls.Add(this.btndelete, 3, 0);
            this.tblltButtons.Controls.Add(this.btnclose, 4, 0);
            this.tblltButtons.Controls.Add(this.btnaddopening, 5, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(383, 153);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(401, 29);
            this.tblltButtons.TabIndex = 9;
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(18, 2);
            this.btnnew.Margin = new System.Windows.Forms.Padding(2);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(60, 25);
            this.btnnew.TabIndex = 10;
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
            this.btnSave.Location = new System.Drawing.Point(82, 2);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(60, 25);
            this.btnSave.TabIndex = 9;
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
            this.btndelete.Location = new System.Drawing.Point(146, 2);
            this.btndelete.Margin = new System.Windows.Forms.Padding(2);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(60, 25);
            this.btndelete.TabIndex = 11;
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
            this.btnclose.Location = new System.Drawing.Point(210, 2);
            this.btnclose.Margin = new System.Windows.Forms.Padding(2);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(60, 25);
            this.btnclose.TabIndex = 12;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnaddopening
            // 
            this.btnaddopening.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnaddopening.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnaddopening.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnaddopening.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnaddopening.ForeColor = System.Drawing.Color.White;
            this.btnaddopening.Location = new System.Drawing.Point(274, 2);
            this.btnaddopening.Margin = new System.Windows.Forms.Padding(2);
            this.btnaddopening.Name = "btnaddopening";
            this.btnaddopening.Size = new System.Drawing.Size(108, 25);
            this.btnaddopening.TabIndex = 13;
            this.btnaddopening.Text = "Add Opening";
            this.btnaddopening.UseVisualStyleBackColor = false;
            this.btnaddopening.Click += new System.EventHandler(this.btnaddopening_Click);
            // 
            // txtchequeno
            // 
            this.txtchequeno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtchequeno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtchequeno.ForeColor = System.Drawing.Color.Black;
            this.txtchequeno.Location = new System.Drawing.Point(526, 97);
            this.txtchequeno.Margin = new System.Windows.Forms.Padding(2);
            this.txtchequeno.Name = "txtchequeno";
            this.txtchequeno.Size = new System.Drawing.Size(231, 25);
            this.txtchequeno.TabIndex = 7;
            this.txtchequeno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtchequeno_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(385, 126);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 25);
            this.label6.TabIndex = 58;
            this.label6.Text = "Amount:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkcheque
            // 
            this.chkcheque.AutoSize = true;
            this.chkcheque.Dock = System.Windows.Forms.DockStyle.Right;
            this.chkcheque.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcheque.Location = new System.Drawing.Point(400, 97);
            this.chkcheque.Margin = new System.Windows.Forms.Padding(15, 2, 2, 2);
            this.chkcheque.Name = "chkcheque";
            this.chkcheque.Size = new System.Drawing.Size(122, 25);
            this.chkcheque.TabIndex = 6;
            this.chkcheque.Text = "Cheque/UTR No";
            this.chkcheque.UseVisualStyleBackColor = true;
            this.chkcheque.CheckedChanged += new System.EventHandler(this.chkcheque_CheckedChanged);
            this.chkcheque.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkcheque_KeyDown);
            // 
            // cmbcustorsupp
            // 
            this.cmbcustorsupp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcustorsupp.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcustorsupp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcustorsupp.Enabled = false;
            this.cmbcustorsupp.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustorsupp.FormattingEnabled = true;
            this.cmbcustorsupp.Location = new System.Drawing.Point(526, 68);
            this.cmbcustorsupp.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustorsupp.Name = "cmbcustorsupp";
            this.cmbcustorsupp.Size = new System.Drawing.Size(231, 25);
            this.cmbcustorsupp.TabIndex = 6;
            this.cmbcustorsupp.SelectedIndexChanged += new System.EventHandler(this.cmbcustorsupp_SelectedIndexChanged);
            this.cmbcustorsupp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcustorsupp_KeyDown);
            this.cmbcustorsupp.Leave += new System.EventHandler(this.cmbcustorsupp_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label11.Location = new System.Drawing.Point(759, 95);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 29);
            this.label11.TabIndex = 113;
            this.label11.Text = "*";
            // 
            // cmbtransaction
            // 
            this.cmbtransaction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbtransaction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbtransaction.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbtransaction.ForeColor = System.Drawing.Color.Black;
            this.cmbtransaction.FormattingEnabled = true;
            this.cmbtransaction.Items.AddRange(new object[] {
            "Withdrawals",
            "Deposits",
            "Receipt",
            "Payment",
            "Expence"});
            this.cmbtransaction.Location = new System.Drawing.Point(526, 39);
            this.cmbtransaction.Margin = new System.Windows.Forms.Padding(2);
            this.cmbtransaction.Name = "cmbtransaction";
            this.cmbtransaction.Size = new System.Drawing.Size(231, 25);
            this.cmbtransaction.TabIndex = 5;
            this.cmbtransaction.SelectedIndexChanged += new System.EventHandler(this.cmbtransaction_SelectedIndexChanged);
            this.cmbtransaction.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbtransaction_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Left;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(759, 37);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 29);
            this.label8.TabIndex = 111;
            this.label8.Text = "*";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Left;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label10.Location = new System.Drawing.Point(759, 66);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 29);
            this.label10.TabIndex = 112;
            this.label10.Text = "*";
            this.label10.Visible = false;
            // 
            // txtparticular
            // 
            this.txtparticular.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtparticular.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtparticular.ForeColor = System.Drawing.Color.Black;
            this.txtparticular.Location = new System.Drawing.Point(127, 97);
            this.txtparticular.Margin = new System.Windows.Forms.Padding(2);
            this.txtparticular.Multiline = true;
            this.txtparticular.Name = "txtparticular";
            this.tblltMain.SetRowSpan(this.txtparticular, 2);
            this.txtparticular.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtparticular.Size = new System.Drawing.Size(231, 54);
            this.txtparticular.TabIndex = 3;
            this.txtparticular.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtparticular_KeyDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(385, 39);
            this.label9.Margin = new System.Windows.Forms.Padding(2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(137, 25);
            this.label9.TabIndex = 55;
            this.label9.Text = "Transaction:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(360, 37);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 29);
            this.label7.TabIndex = 53;
            this.label7.Text = "*";
            // 
            // lblcustorsupp
            // 
            this.lblcustorsupp.AutoSize = true;
            this.lblcustorsupp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblcustorsupp.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcustorsupp.ForeColor = System.Drawing.Color.Black;
            this.lblcustorsupp.Location = new System.Drawing.Point(385, 68);
            this.lblcustorsupp.Margin = new System.Windows.Forms.Padding(2);
            this.lblcustorsupp.Name = "lblcustorsupp";
            this.lblcustorsupp.Size = new System.Drawing.Size(137, 25);
            this.lblcustorsupp.TabIndex = 114;
            this.lblcustorsupp.Text = "Party Name:";
            this.lblcustorsupp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(2, 97);
            this.label19.Margin = new System.Windows.Forms.Padding(2);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(121, 25);
            this.label19.TabIndex = 51;
            this.label19.Text = "Particulars:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpdate
            // 
            this.dtpdate.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtpdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpdate.Location = new System.Drawing.Point(127, 155);
            this.dtpdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpdate.Name = "dtpdate";
            this.dtpdate.Size = new System.Drawing.Size(120, 25);
            this.dtpdate.TabIndex = 4;
            this.dtpdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpdate_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(2, 155);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 25);
            this.label4.TabIndex = 40;
            this.label4.Text = "Date:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(2, 68);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 25);
            this.label3.TabIndex = 50;
            this.label3.Text = "Bank/ Party Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbbankname
            // 
            this.cmbbankname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbbankname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbbankname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbankname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbbankname.FormattingEnabled = true;
            this.cmbbankname.Location = new System.Drawing.Point(127, 68);
            this.cmbbankname.Margin = new System.Windows.Forms.Padding(2);
            this.cmbbankname.Name = "cmbbankname";
            this.cmbbankname.Size = new System.Drawing.Size(231, 25);
            this.cmbbankname.TabIndex = 2;
            this.cmbbankname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbbankname_KeyDown);
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label16.Location = new System.Drawing.Point(360, 66);
            this.label16.Margin = new System.Windows.Forms.Padding(0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(23, 29);
            this.label16.TabIndex = 115;
            this.label16.Text = "*";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Black;
            this.label17.Location = new System.Drawing.Point(2, 39);
            this.label17.Margin = new System.Windows.Forms.Padding(2);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(121, 25);
            this.label17.TabIndex = 117;
            this.label17.Text = "Account Group:";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbACGroups
            // 
            this.cmbACGroups.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbACGroups.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbACGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbACGroups.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbACGroups.FormattingEnabled = true;
            this.cmbACGroups.Location = new System.Drawing.Point(127, 39);
            this.cmbACGroups.Margin = new System.Windows.Forms.Padding(2);
            this.cmbACGroups.Name = "cmbACGroups";
            this.cmbACGroups.Size = new System.Drawing.Size(231, 25);
            this.cmbACGroups.TabIndex = 1;
            this.cmbACGroups.SelectedIndexChanged += new System.EventHandler(this.cmbACGroups_SelectedIndexChanged);
            this.cmbACGroups.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbACGroups_KeyDown);
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(526, 126);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(2);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(120, 25);
            this.txtAmount.TabIndex = 8;
            this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAmount_KeyDown);
            // 
            // frmBankStatement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(784, 532);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmBankStatement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bank Statement";
            this.Load += new System.EventHandler(this.frmBankStatement_Load);
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.grpList.ResumeLayout(false);
            this.tblltList.ResumeLayout(false);
            this.tblltList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvreceiptInfo)).EndInit();
            this.tblltListRow2.ResumeLayout(false);
            this.tblltListRow2.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtpdate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbtransaction;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cmbbankname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtparticular;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtchequeno;
        private System.Windows.Forms.CheckBox chkcheque;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbsbankname1;
        private System.Windows.Forms.CheckBox chkbankname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView GvreceiptInfo;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltList;
        private System.Windows.Forms.ComboBox cmbcustorsupp;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblcustorsupp;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chkfromdate;
        private System.Windows.Forms.DateTimePicker dtpfrom;
        private System.Windows.Forms.DateTimePicker dtpto;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cmbACGroups;
        private System.Windows.Forms.TableLayoutPanel tblltListRow2;
        private RachitControls.NumericTextBox txtAmount;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnaddopening;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.GroupBox grpList;
    }
}