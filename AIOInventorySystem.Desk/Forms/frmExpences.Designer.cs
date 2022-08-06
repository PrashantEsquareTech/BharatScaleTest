namespace AIOInventorySystem.Desk.Forms
{
    partial class frmExpences
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
            this.label9 = new System.Windows.Forms.Label();
            this.tblltExpenceInfo = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dtpexpencedate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.txtexpenceno = new System.Windows.Forms.TextBox();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtchequeno = new System.Windows.Forms.TextBox();
            this.cmbbankname = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpchequedate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbpaymentmode = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.cmbACGroups = new System.Windows.Forms.ComboBox();
            this.cmbexpence = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtfrom = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtto = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtreason = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnExpForm = new System.Windows.Forms.Button();
            this.txtpaidamount = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.grpExpenceList = new System.Windows.Forms.GroupBox();
            this.tblltList = new System.Windows.Forms.TableLayoutPanel();
            this.GvexpenceInfo = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACGId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpenceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.From = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.To = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Reason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BankName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChqUTRNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChqNEFTDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaidAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkdate = new System.Windows.Forms.CheckBox();
            this.dtptodate = new System.Windows.Forms.DateTimePicker();
            this.dtpfromdate = new System.Windows.Forms.DateTimePicker();
            this.label18 = new System.Windows.Forms.Label();
            this.chkexpname = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.cmbexpencename1 = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btnsearch = new System.Windows.Forms.Button();
            this.btngetall = new System.Windows.Forms.Button();
            this.btnprintall = new System.Windows.Forms.Button();
            this.grpExpenceInfo = new System.Windows.Forms.GroupBox();
            this.tblltExpenceInfo.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.grpExpenceList.SuspendLayout();
            this.tblltList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvexpenceInfo)).BeginInit();
            this.grpExpenceInfo.SuspendLayout();
            this.SuspendLayout();
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
            this.label9.Size = new System.Drawing.Size(964, 37);
            this.label9.TabIndex = 33;
            this.label9.Text = "Expences";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltExpenceInfo
            // 
            this.tblltExpenceInfo.ColumnCount = 7;
            this.tblltExpenceInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltExpenceInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tblltExpenceInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tblltExpenceInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltExpenceInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltExpenceInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tblltExpenceInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tblltExpenceInfo.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tblltExpenceInfo.Controls.Add(this.tblltButtons, 4, 6);
            this.tblltExpenceInfo.Controls.Add(this.label6, 0, 0);
            this.tblltExpenceInfo.Controls.Add(this.txtchequeno, 5, 3);
            this.tblltExpenceInfo.Controls.Add(this.cmbbankname, 5, 1);
            this.tblltExpenceInfo.Controls.Add(this.label10, 4, 3);
            this.tblltExpenceInfo.Controls.Add(this.label17, 6, 4);
            this.tblltExpenceInfo.Controls.Add(this.label1, 4, 4);
            this.tblltExpenceInfo.Controls.Add(this.dtpchequedate, 5, 2);
            this.tblltExpenceInfo.Controls.Add(this.label8, 4, 2);
            this.tblltExpenceInfo.Controls.Add(this.cmbpaymentmode, 5, 0);
            this.tblltExpenceInfo.Controls.Add(this.label7, 4, 0);
            this.tblltExpenceInfo.Controls.Add(this.label16, 6, 0);
            this.tblltExpenceInfo.Controls.Add(this.label12, 4, 1);
            this.tblltExpenceInfo.Controls.Add(this.label20, 0, 1);
            this.tblltExpenceInfo.Controls.Add(this.cmbACGroups, 1, 1);
            this.tblltExpenceInfo.Controls.Add(this.cmbexpence, 1, 2);
            this.tblltExpenceInfo.Controls.Add(this.label11, 0, 2);
            this.tblltExpenceInfo.Controls.Add(this.label2, 0, 3);
            this.tblltExpenceInfo.Controls.Add(this.txtfrom, 1, 3);
            this.tblltExpenceInfo.Controls.Add(this.label3, 0, 4);
            this.tblltExpenceInfo.Controls.Add(this.txtto, 1, 4);
            this.tblltExpenceInfo.Controls.Add(this.label4, 0, 5);
            this.tblltExpenceInfo.Controls.Add(this.txtreason, 1, 5);
            this.tblltExpenceInfo.Controls.Add(this.label14, 2, 2);
            this.tblltExpenceInfo.Controls.Add(this.label15, 2, 4);
            this.tblltExpenceInfo.Controls.Add(this.btnExpForm, 3, 2);
            this.tblltExpenceInfo.Controls.Add(this.txtpaidamount, 5, 4);
            this.tblltExpenceInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltExpenceInfo.Location = new System.Drawing.Point(3, 21);
            this.tblltExpenceInfo.Name = "tblltExpenceInfo";
            this.tblltExpenceInfo.RowCount = 7;
            this.tblltExpenceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltExpenceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltExpenceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltExpenceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltExpenceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltExpenceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltExpenceInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltExpenceInfo.Size = new System.Drawing.Size(952, 203);
            this.tblltExpenceInfo.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.dtpexpencedate, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtexpenceno, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(123, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(323, 28);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // dtpexpencedate
            // 
            this.dtpexpencedate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpexpencedate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpexpencedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpexpencedate.Location = new System.Drawing.Point(2, 2);
            this.dtpexpencedate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpexpencedate.Name = "dtpexpencedate";
            this.dtpexpencedate.Size = new System.Drawing.Size(103, 25);
            this.dtpexpencedate.TabIndex = 0;
            this.dtpexpencedate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpexpencedate_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(109, 2);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 24);
            this.label5.TabIndex = 22;
            this.label5.Text = "Expense No:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtexpenceno
            // 
            this.txtexpenceno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtexpenceno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtexpenceno.Location = new System.Drawing.Point(216, 2);
            this.txtexpenceno.Margin = new System.Windows.Forms.Padding(2);
            this.txtexpenceno.Name = "txtexpenceno";
            this.txtexpenceno.ReadOnly = true;
            this.txtexpenceno.Size = new System.Drawing.Size(105, 25);
            this.txtexpenceno.TabIndex = 1;
            this.txtexpenceno.TabStop = false;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 7;
            this.tblltExpenceInfo.SetColumnSpan(this.tblltButtons, 3);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.Controls.Add(this.btnnew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnsave, 2, 0);
            this.tblltButtons.Controls.Add(this.btndelete, 3, 0);
            this.tblltButtons.Controls.Add(this.btnclose, 4, 0);
            this.tblltButtons.Controls.Add(this.btnPrint, 5, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(503, 168);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(449, 35);
            this.tblltButtons.TabIndex = 13;
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(66, 2);
            this.btnnew.Margin = new System.Windows.Forms.Padding(2);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(60, 31);
            this.btnnew.TabIndex = 14;
            this.btnnew.Text = "New";
            this.btnnew.UseVisualStyleBackColor = false;
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            // 
            // btnsave
            // 
            this.btnsave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsave.ForeColor = System.Drawing.Color.White;
            this.btnsave.Location = new System.Drawing.Point(130, 2);
            this.btnsave.Margin = new System.Windows.Forms.Padding(2);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(60, 31);
            this.btnsave.TabIndex = 13;
            this.btnsave.Text = "Save";
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btndelete
            // 
            this.btndelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btndelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btndelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btndelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.Location = new System.Drawing.Point(194, 2);
            this.btndelete.Margin = new System.Windows.Forms.Padding(2);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(60, 31);
            this.btndelete.TabIndex = 15;
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
            this.btnclose.Location = new System.Drawing.Point(258, 2);
            this.btnclose.Margin = new System.Windows.Forms.Padding(2);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(60, 31);
            this.btnclose.TabIndex = 16;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(322, 2);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(60, 31);
            this.btnPrint.TabIndex = 17;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(82, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 17);
            this.label6.TabIndex = 21;
            this.label6.Text = "Date:";
            // 
            // txtchequeno
            // 
            this.txtchequeno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtchequeno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtchequeno.Location = new System.Drawing.Point(619, 86);
            this.txtchequeno.Margin = new System.Windows.Forms.Padding(2);
            this.txtchequeno.Name = "txtchequeno";
            this.txtchequeno.Size = new System.Drawing.Size(310, 25);
            this.txtchequeno.TabIndex = 11;
            this.txtchequeno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtchequeno_KeyDown);
            // 
            // cmbbankname
            // 
            this.cmbbankname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbbankname.DropDownHeight = 110;
            this.cmbbankname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbankname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbbankname.FormattingEnabled = true;
            this.cmbbankname.IntegralHeight = false;
            this.cmbbankname.Items.AddRange(new object[] {
            "Cash",
            "Cheque"});
            this.cmbbankname.Location = new System.Drawing.Point(619, 30);
            this.cmbbankname.Margin = new System.Windows.Forms.Padding(2);
            this.cmbbankname.Name = "cmbbankname";
            this.cmbbankname.Size = new System.Drawing.Size(310, 25);
            this.cmbbankname.TabIndex = 9;
            this.cmbbankname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbbankname_KeyDown);
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(537, 89);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 17);
            this.label10.TabIndex = 30;
            this.label10.Text = "Cheque No:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label17.Location = new System.Drawing.Point(931, 112);
            this.label17.Margin = new System.Windows.Forms.Padding(0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(21, 28);
            this.label17.TabIndex = 47;
            this.label17.Text = "*";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(529, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 31;
            this.label1.Text = "Paid Amount:";
            // 
            // dtpchequedate
            // 
            this.dtpchequedate.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtpchequedate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpchequedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpchequedate.Location = new System.Drawing.Point(619, 58);
            this.dtpchequedate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpchequedate.Name = "dtpchequedate";
            this.dtpchequedate.Size = new System.Drawing.Size(120, 25);
            this.dtpchequedate.TabIndex = 10;
            this.dtpchequedate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpchequedate_KeyDown);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(528, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 17);
            this.label8.TabIndex = 29;
            this.label8.Text = "Cheque Date:";
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
            this.cmbpaymentmode.Location = new System.Drawing.Point(619, 2);
            this.cmbpaymentmode.Margin = new System.Windows.Forms.Padding(2);
            this.cmbpaymentmode.Name = "cmbpaymentmode";
            this.cmbpaymentmode.Size = new System.Drawing.Size(310, 25);
            this.cmbpaymentmode.TabIndex = 8;
            this.cmbpaymentmode.SelectedIndexChanged += new System.EventHandler(this.cmbpaymentmode_SelectedIndexChanged);
            this.cmbpaymentmode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbpaymentmode_KeyDown);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(515, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 17);
            this.label7.TabIndex = 27;
            this.label7.Text = "Payment Mode:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label16.Location = new System.Drawing.Point(931, 0);
            this.label16.Margin = new System.Windows.Forms.Padding(0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(21, 28);
            this.label16.TabIndex = 46;
            this.label16.Text = "*";
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(537, 33);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 17);
            this.label12.TabIndex = 28;
            this.label12.Text = "Bank Name:";
            // 
            // label20
            // 
            this.label20.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.Color.Transparent;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(33, 33);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(87, 17);
            this.label20.TabIndex = 34;
            this.label20.Text = "Group Name:";
            // 
            // cmbACGroups
            // 
            this.cmbACGroups.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbACGroups.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbACGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbACGroups.DropDownHeight = 110;
            this.cmbACGroups.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbACGroups.FormattingEnabled = true;
            this.cmbACGroups.IntegralHeight = false;
            this.cmbACGroups.Items.AddRange(new object[] {
            "Cash",
            "Cheque"});
            this.cmbACGroups.Location = new System.Drawing.Point(125, 30);
            this.cmbACGroups.Margin = new System.Windows.Forms.Padding(2);
            this.cmbACGroups.Name = "cmbACGroups";
            this.cmbACGroups.Size = new System.Drawing.Size(319, 25);
            this.cmbACGroups.TabIndex = 2;
            this.cmbACGroups.SelectedIndexChanged += new System.EventHandler(this.cmbACGroups_SelectedIndexChanged);
            this.cmbACGroups.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbACGroups_KeyDown);
            // 
            // cmbexpence
            // 
            this.cmbexpence.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbexpence.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbexpence.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbexpence.DropDownHeight = 110;
            this.cmbexpence.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbexpence.FormattingEnabled = true;
            this.cmbexpence.IntegralHeight = false;
            this.cmbexpence.Items.AddRange(new object[] {
            "Cash",
            "Cheque"});
            this.cmbexpence.Location = new System.Drawing.Point(125, 58);
            this.cmbexpence.Margin = new System.Windows.Forms.Padding(2);
            this.cmbexpence.Name = "cmbexpence";
            this.cmbexpence.Size = new System.Drawing.Size(319, 25);
            this.cmbexpence.TabIndex = 3;
            this.cmbexpence.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbexpence_KeyDown);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(22, 61);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 17);
            this.label11.TabIndex = 23;
            this.label11.Text = "Expense Name:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(79, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 17);
            this.label2.TabIndex = 24;
            this.label2.Text = "From:";
            // 
            // txtfrom
            // 
            this.txtfrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtfrom.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtfrom.Location = new System.Drawing.Point(125, 86);
            this.txtfrom.Margin = new System.Windows.Forms.Padding(2);
            this.txtfrom.Name = "txtfrom";
            this.txtfrom.Size = new System.Drawing.Size(319, 25);
            this.txtfrom.TabIndex = 5;
            this.txtfrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtfrom_KeyDown);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(95, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 17);
            this.label3.TabIndex = 25;
            this.label3.Text = "To:";
            // 
            // txtto
            // 
            this.txtto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtto.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtto.Location = new System.Drawing.Point(125, 114);
            this.txtto.Margin = new System.Windows.Forms.Padding(2);
            this.txtto.Name = "txtto";
            this.txtto.Size = new System.Drawing.Size(319, 25);
            this.txtto.TabIndex = 6;
            this.txtto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtto_KeyDown);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(66, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 17);
            this.label4.TabIndex = 26;
            this.label4.Text = "Reason:";
            // 
            // txtreason
            // 
            this.txtreason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtreason.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtreason.Location = new System.Drawing.Point(125, 142);
            this.txtreason.Margin = new System.Windows.Forms.Padding(2);
            this.txtreason.Multiline = true;
            this.txtreason.Name = "txtreason";
            this.tblltExpenceInfo.SetRowSpan(this.txtreason, 2);
            this.txtreason.Size = new System.Drawing.Size(319, 59);
            this.txtreason.TabIndex = 7;
            this.txtreason.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtreason_KeyDown);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label14.Location = new System.Drawing.Point(446, 56);
            this.label14.Margin = new System.Windows.Forms.Padding(0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(19, 28);
            this.label14.TabIndex = 44;
            this.label14.Text = "*";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label15.Location = new System.Drawing.Point(446, 112);
            this.label15.Margin = new System.Windows.Forms.Padding(0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(19, 28);
            this.label15.TabIndex = 45;
            this.label15.Text = "*";
            // 
            // btnExpForm
            // 
            this.btnExpForm.BackgroundImage = global::AIOInventorySystem.Desk.Properties.Resources.newbutton;
            this.btnExpForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExpForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExpForm.FlatAppearance.BorderSize = 0;
            this.btnExpForm.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExpForm.Location = new System.Drawing.Point(467, 58);
            this.btnExpForm.Margin = new System.Windows.Forms.Padding(2);
            this.btnExpForm.Name = "btnExpForm";
            this.btnExpForm.Size = new System.Drawing.Size(34, 24);
            this.btnExpForm.TabIndex = 4;
            this.btnExpForm.UseVisualStyleBackColor = true;
            this.btnExpForm.Click += new System.EventHandler(this.btnExpForm_Click);
            // 
            // txtpaidamount
            // 
            this.txtpaidamount.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtpaidamount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpaidamount.Location = new System.Drawing.Point(620, 115);
            this.txtpaidamount.Name = "txtpaidamount";
            this.txtpaidamount.Size = new System.Drawing.Size(120, 25);
            this.txtpaidamount.TabIndex = 12;
            this.txtpaidamount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpaidamount_KeyDown);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.grpExpenceList, 0, 2);
            this.tblltMain.Controls.Add(this.grpExpenceInfo, 0, 1);
            this.tblltMain.Controls.Add(this.label9, 0, 0);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltMain.Size = new System.Drawing.Size(964, 542);
            this.tblltMain.TabIndex = 0;
            // 
            // grpExpenceList
            // 
            this.grpExpenceList.Controls.Add(this.tblltList);
            this.grpExpenceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpExpenceList.Location = new System.Drawing.Point(3, 273);
            this.grpExpenceList.Name = "grpExpenceList";
            this.grpExpenceList.Size = new System.Drawing.Size(958, 266);
            this.grpExpenceList.TabIndex = 18;
            this.grpExpenceList.TabStop = false;
            this.grpExpenceList.Text = "Expence List";
            // 
            // tblltList
            // 
            this.tblltList.ColumnCount = 11;
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.4997F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.999716F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.999915F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.999716F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.49962F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.99954F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.499758F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.000644F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.500465F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.500465F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.500465F));
            this.tblltList.Controls.Add(this.GvexpenceInfo, 0, 1);
            this.tblltList.Controls.Add(this.chkdate, 0, 0);
            this.tblltList.Controls.Add(this.dtptodate, 3, 0);
            this.tblltList.Controls.Add(this.dtpfromdate, 1, 0);
            this.tblltList.Controls.Add(this.label18, 2, 0);
            this.tblltList.Controls.Add(this.chkexpname, 4, 0);
            this.tblltList.Controls.Add(this.label19, 6, 0);
            this.tblltList.Controls.Add(this.cmbexpencename1, 5, 0);
            this.tblltList.Controls.Add(this.label13, 7, 0);
            this.tblltList.Controls.Add(this.btnsearch, 8, 0);
            this.tblltList.Controls.Add(this.btngetall, 9, 0);
            this.tblltList.Controls.Add(this.btnprintall, 10, 0);
            this.tblltList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltList.Location = new System.Drawing.Point(3, 21);
            this.tblltList.Name = "tblltList";
            this.tblltList.RowCount = 2;
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.tblltList.Size = new System.Drawing.Size(952, 242);
            this.tblltList.TabIndex = 18;
            // 
            // GvexpenceInfo
            // 
            this.GvexpenceInfo.AllowUserToAddRows = false;
            this.GvexpenceInfo.AllowUserToDeleteRows = false;
            this.GvexpenceInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvexpenceInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvexpenceInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvexpenceInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvexpenceInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvexpenceInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.ExpId,
            this.Date,
            this.ACGId,
            this.ACGroup,
            this.ExpenceName,
            this.From,
            this.To,
            this.Reason,
            this.Mode,
            this.BankName,
            this.ChqUTRNo,
            this.ChqNEFTDate,
            this.PaidAmount});
            this.tblltList.SetColumnSpan(this.GvexpenceInfo, 11);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GvexpenceInfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.GvexpenceInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvexpenceInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvexpenceInfo.Location = new System.Drawing.Point(3, 32);
            this.GvexpenceInfo.Name = "GvexpenceInfo";
            this.GvexpenceInfo.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvexpenceInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GvexpenceInfo.RowHeadersWidth = 20;
            this.GvexpenceInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvexpenceInfo.Size = new System.Drawing.Size(946, 207);
            this.GvexpenceInfo.TabIndex = 27;
            this.GvexpenceInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvexpenceInfo_CellClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // ExpId
            // 
            this.ExpId.FillWeight = 63.06959F;
            this.ExpId.HeaderText = "ExpId";
            this.ExpId.Name = "ExpId";
            this.ExpId.ReadOnly = true;
            this.ExpId.Visible = false;
            // 
            // Date
            // 
            this.Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Date.FillWeight = 445.1381F;
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 80;
            // 
            // ACGId
            // 
            this.ACGId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ACGId.FillWeight = 53.6548F;
            this.ACGId.HeaderText = "ACGId";
            this.ACGId.Name = "ACGId";
            this.ACGId.ReadOnly = true;
            this.ACGId.Visible = false;
            this.ACGId.Width = 5;
            // 
            // ACGroup
            // 
            this.ACGroup.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ACGroup.FillWeight = 21.4853F;
            this.ACGroup.HeaderText = "ACGroup";
            this.ACGroup.Name = "ACGroup";
            this.ACGroup.ReadOnly = true;
            this.ACGroup.Width = 110;
            // 
            // ExpenceName
            // 
            this.ExpenceName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ExpenceName.FillWeight = 65.36695F;
            this.ExpenceName.HeaderText = "Expence Name";
            this.ExpenceName.Name = "ExpenceName";
            this.ExpenceName.ReadOnly = true;
            this.ExpenceName.Width = 110;
            // 
            // From
            // 
            this.From.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.From.FillWeight = 65.36695F;
            this.From.HeaderText = "From";
            this.From.Name = "From";
            this.From.ReadOnly = true;
            // 
            // To
            // 
            this.To.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.To.FillWeight = 65.36695F;
            this.To.HeaderText = "To";
            this.To.Name = "To";
            this.To.ReadOnly = true;
            // 
            // Reason
            // 
            this.Reason.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Reason.FillWeight = 65.36695F;
            this.Reason.HeaderText = "Reason";
            this.Reason.Name = "Reason";
            this.Reason.ReadOnly = true;
            // 
            // Mode
            // 
            this.Mode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Mode.FillWeight = 65.36695F;
            this.Mode.HeaderText = "Mode";
            this.Mode.Name = "Mode";
            this.Mode.ReadOnly = true;
            this.Mode.Width = 70;
            // 
            // BankName
            // 
            this.BankName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BankName.FillWeight = 65.36695F;
            this.BankName.HeaderText = "BankName";
            this.BankName.Name = "BankName";
            this.BankName.ReadOnly = true;
            this.BankName.Width = 90;
            // 
            // ChqUTRNo
            // 
            this.ChqUTRNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ChqUTRNo.FillWeight = 65.36695F;
            this.ChqUTRNo.HeaderText = "Chq/UTR No";
            this.ChqUTRNo.Name = "ChqUTRNo";
            this.ChqUTRNo.ReadOnly = true;
            this.ChqUTRNo.Width = 70;
            // 
            // ChqNEFTDate
            // 
            this.ChqNEFTDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ChqNEFTDate.FillWeight = 65.36695F;
            this.ChqNEFTDate.HeaderText = "Chq/NEFT Date";
            this.ChqNEFTDate.Name = "ChqNEFTDate";
            this.ChqNEFTDate.ReadOnly = true;
            this.ChqNEFTDate.Width = 70;
            // 
            // PaidAmount
            // 
            this.PaidAmount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.PaidAmount.FillWeight = 65.36695F;
            this.PaidAmount.HeaderText = "Paid Amount";
            this.PaidAmount.Name = "PaidAmount";
            this.PaidAmount.ReadOnly = true;
            this.PaidAmount.Width = 80;
            // 
            // chkdate
            // 
            this.chkdate.AutoSize = true;
            this.chkdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkdate.Location = new System.Drawing.Point(4, 2);
            this.chkdate.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkdate.Name = "chkdate";
            this.chkdate.Size = new System.Drawing.Size(93, 25);
            this.chkdate.TabIndex = 18;
            this.chkdate.Text = "From Date";
            this.chkdate.UseVisualStyleBackColor = true;
            this.chkdate.CheckedChanged += new System.EventHandler(this.chkdate_CheckedChanged);
            this.chkdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkdate_KeyDown);
            // 
            // dtptodate
            // 
            this.dtptodate.CustomFormat = "dd/MM/yyyy";
            this.dtptodate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtptodate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtptodate.Location = new System.Drawing.Point(224, 2);
            this.dtptodate.Margin = new System.Windows.Forms.Padding(2);
            this.dtptodate.Name = "dtptodate";
            this.dtptodate.Size = new System.Drawing.Size(91, 25);
            this.dtptodate.TabIndex = 20;
            this.dtptodate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtptodate_KeyDown);
            // 
            // dtpfromdate
            // 
            this.dtpfromdate.CustomFormat = "dd/MM/yyyy";
            this.dtpfromdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpfromdate.Location = new System.Drawing.Point(101, 2);
            this.dtpfromdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpfromdate.Name = "dtpfromdate";
            this.dtpfromdate.Size = new System.Drawing.Size(91, 25);
            this.dtpfromdate.TabIndex = 19;
            this.dtpfromdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpfromdate_KeyDown);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(196, 2);
            this.label18.Margin = new System.Windows.Forms.Padding(2);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(24, 25);
            this.label18.TabIndex = 33;
            this.label18.Text = "To";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkexpname
            // 
            this.chkexpname.AutoSize = true;
            this.chkexpname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkexpname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkexpname.Location = new System.Drawing.Point(321, 2);
            this.chkexpname.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkexpname.Name = "chkexpname";
            this.chkexpname.Size = new System.Drawing.Size(122, 25);
            this.chkexpname.TabIndex = 21;
            this.chkexpname.Text = "Expence Name";
            this.chkexpname.UseVisualStyleBackColor = true;
            this.chkexpname.CheckedChanged += new System.EventHandler(this.chkexpname_CheckedChanged);
            this.chkexpname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkexpname_KeyDown);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label19.Location = new System.Drawing.Point(599, 2);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 2, 1, 2);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(77, 25);
            this.label19.TabIndex = 79;
            this.label19.Text = "Total Exp:";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbexpencename1
            // 
            this.cmbexpencename1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbexpencename1.DropDownHeight = 110;
            this.cmbexpencename1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbexpencename1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbexpencename1.FormattingEnabled = true;
            this.cmbexpencename1.IntegralHeight = false;
            this.cmbexpencename1.Items.AddRange(new object[] {
            "Cash",
            "Cheque"});
            this.cmbexpencename1.Location = new System.Drawing.Point(447, 2);
            this.cmbexpencename1.Margin = new System.Windows.Forms.Padding(2);
            this.cmbexpencename1.Name = "cmbexpencename1";
            this.cmbexpencename1.Size = new System.Drawing.Size(148, 25);
            this.cmbexpencename1.TabIndex = 22;
            this.cmbexpencename1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbexpencename1_KeyDown);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label13.Location = new System.Drawing.Point(678, 2);
            this.label13.Margin = new System.Windows.Forms.Padding(1, 2, 2, 2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(82, 25);
            this.label13.TabIndex = 84;
            this.label13.Text = "0";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(764, 2);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(57, 25);
            this.btnsearch.TabIndex = 23;
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
            this.btngetall.Location = new System.Drawing.Point(825, 2);
            this.btngetall.Margin = new System.Windows.Forms.Padding(2);
            this.btngetall.Name = "btngetall";
            this.btngetall.Size = new System.Drawing.Size(57, 25);
            this.btngetall.TabIndex = 24;
            this.btngetall.Text = "GetAll";
            this.btngetall.UseVisualStyleBackColor = false;
            this.btngetall.Click += new System.EventHandler(this.btngetall_Click);
            // 
            // btnprintall
            // 
            this.btnprintall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprintall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprintall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnprintall.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprintall.ForeColor = System.Drawing.Color.White;
            this.btnprintall.Location = new System.Drawing.Point(886, 2);
            this.btnprintall.Margin = new System.Windows.Forms.Padding(2);
            this.btnprintall.Name = "btnprintall";
            this.btnprintall.Size = new System.Drawing.Size(64, 25);
            this.btnprintall.TabIndex = 26;
            this.btnprintall.Text = "Print";
            this.btnprintall.UseVisualStyleBackColor = false;
            this.btnprintall.Click += new System.EventHandler(this.btnprintall_Click);
            // 
            // grpExpenceInfo
            // 
            this.grpExpenceInfo.Controls.Add(this.tblltExpenceInfo);
            this.grpExpenceInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpExpenceInfo.Location = new System.Drawing.Point(3, 40);
            this.grpExpenceInfo.Name = "grpExpenceInfo";
            this.grpExpenceInfo.Size = new System.Drawing.Size(958, 227);
            this.grpExpenceInfo.TabIndex = 0;
            this.grpExpenceInfo.TabStop = false;
            this.grpExpenceInfo.Text = "Expence Information";
            // 
            // frmExpences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(964, 542);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "frmExpences";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Expences";
            this.Load += new System.EventHandler(this.frmExpences_Load);
            this.tblltExpenceInfo.ResumeLayout(false);
            this.tblltExpenceInfo.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.tblltMain.ResumeLayout(false);
            this.grpExpenceList.ResumeLayout(false);
            this.tblltList.ResumeLayout(false);
            this.tblltList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvexpenceInfo)).EndInit();
            this.grpExpenceInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpexpencedate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtexpenceno;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtreason;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtfrom;
        private System.Windows.Forms.TextBox txtto;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dtpchequedate;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbpaymentmode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtchequeno;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbbankname;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cmbexpence;
        private System.Windows.Forms.TableLayoutPanel tblltExpenceInfo;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.ComboBox cmbACGroups;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.GroupBox grpExpenceInfo;
        private System.Windows.Forms.GroupBox grpExpenceList;
        private System.Windows.Forms.TableLayoutPanel tblltList;
        private System.Windows.Forms.CheckBox chkdate;
        private System.Windows.Forms.DateTimePicker dtptodate;
        private System.Windows.Forms.DateTimePicker dtpfromdate;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.CheckBox chkexpname;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cmbexpencename1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Button btngetall;
        private System.Windows.Forms.Button btnprintall;
        private System.Windows.Forms.DataGridView GvexpenceInfo;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExpForm;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private RachitControls.NumericTextBox txtpaidamount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACGId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpenceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn From;
        private System.Windows.Forms.DataGridViewTextBoxColumn To;
        private System.Windows.Forms.DataGridViewTextBoxColumn Reason;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mode;
        private System.Windows.Forms.DataGridViewTextBoxColumn BankName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChqUTRNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChqNEFTDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaidAmount;
    }
}