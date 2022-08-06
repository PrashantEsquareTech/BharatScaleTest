namespace AIOInventorySystem.Desk.Forms
{
    partial class frmBonus
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
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.sgrpBox2 = new System.Windows.Forms.GroupBox();
            this.tblltSerachInfo = new System.Windows.Forms.TableLayoutPanel();
            this.btnsearch = new System.Windows.Forms.Button();
            this.GvreceiptInfo = new System.Windows.Forms.DataGridView();
            this.btnGettAll = new System.Windows.Forms.Button();
            this.chkcustomername1 = new System.Windows.Forms.CheckBox();
            this.cmbcustomername1 = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.lbltbonus = new System.Windows.Forms.Label();
            this.sgrpBox1 = new System.Windows.Forms.GroupBox();
            this.tblltInputs = new System.Windows.Forms.TableLayoutPanel();
            this.txtnarration = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtchequeno = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUnpaidList = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.dtpchequedate = new System.Windows.Forms.DateTimePicker();
            this.label14 = new System.Windows.Forms.Label();
            this.txtRemainingAmt = new System.Windows.Forms.TextBox();
            this.cmbpaymentmode = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tblltCustAdd = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbbank = new System.Windows.Forms.ComboBox();
            this.txtPaidAmount = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtBankName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTotalAmt = new System.Windows.Forms.TextBox();
            this.cmbcustomer = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpreceiptDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtreciptNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tblltMain.SuspendLayout();
            this.sgrpBox2.SuspendLayout();
            this.tblltSerachInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvreceiptInfo)).BeginInit();
            this.sgrpBox1.SuspendLayout();
            this.tblltInputs.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.tblltCustAdd.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.label9, 0, 0);
            this.tblltMain.Controls.Add(this.sgrpBox2, 0, 2);
            this.tblltMain.Controls.Add(this.sgrpBox1, 0, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57F));
            this.tblltMain.Size = new System.Drawing.Size(834, 632);
            this.tblltMain.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(834, 37);
            this.label9.TabIndex = 33;
            this.label9.Text = "Customer Bonus Ledger A/c";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sgrpBox2
            // 
            this.sgrpBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.sgrpBox2.Controls.Add(this.tblltSerachInfo);
            this.sgrpBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sgrpBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sgrpBox2.Location = new System.Drawing.Point(2, 272);
            this.sgrpBox2.Margin = new System.Windows.Forms.Padding(2);
            this.sgrpBox2.Name = "sgrpBox2";
            this.sgrpBox2.Padding = new System.Windows.Forms.Padding(2);
            this.sgrpBox2.Size = new System.Drawing.Size(830, 358);
            this.sgrpBox2.TabIndex = 17;
            this.sgrpBox2.TabStop = false;
            this.sgrpBox2.Text = "Search Bonus Information";
            // 
            // tblltSerachInfo
            // 
            this.tblltSerachInfo.ColumnCount = 7;
            this.tblltSerachInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltSerachInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblltSerachInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltSerachInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltSerachInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltSerachInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltSerachInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltSerachInfo.Controls.Add(this.btnsearch, 5, 0);
            this.tblltSerachInfo.Controls.Add(this.GvreceiptInfo, 0, 1);
            this.tblltSerachInfo.Controls.Add(this.btnGettAll, 6, 0);
            this.tblltSerachInfo.Controls.Add(this.chkcustomername1, 0, 0);
            this.tblltSerachInfo.Controls.Add(this.cmbcustomername1, 1, 0);
            this.tblltSerachInfo.Controls.Add(this.label16, 2, 0);
            this.tblltSerachInfo.Controls.Add(this.lbltbonus, 3, 0);
            this.tblltSerachInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltSerachInfo.Location = new System.Drawing.Point(2, 20);
            this.tblltSerachInfo.Margin = new System.Windows.Forms.Padding(0);
            this.tblltSerachInfo.Name = "tblltSerachInfo";
            this.tblltSerachInfo.RowCount = 2;
            this.tblltSerachInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltSerachInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91F));
            this.tblltSerachInfo.Size = new System.Drawing.Size(826, 336);
            this.tblltSerachInfo.TabIndex = 17;
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(660, 2);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(78, 26);
            this.btnsearch.TabIndex = 19;
            this.btnsearch.Text = "Search";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // GvreceiptInfo
            // 
            this.GvreceiptInfo.AllowUserToAddRows = false;
            this.GvreceiptInfo.AllowUserToDeleteRows = false;
            this.GvreceiptInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvreceiptInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvreceiptInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvreceiptInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvreceiptInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblltSerachInfo.SetColumnSpan(this.GvreceiptInfo, 7);
            this.GvreceiptInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvreceiptInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvreceiptInfo.Location = new System.Drawing.Point(3, 33);
            this.GvreceiptInfo.Name = "GvreceiptInfo";
            this.GvreceiptInfo.ReadOnly = true;
            this.GvreceiptInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvreceiptInfo.Size = new System.Drawing.Size(820, 300);
            this.GvreceiptInfo.TabIndex = 21;
            this.GvreceiptInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvreceiptInfo_CellClick);
            // 
            // btnGettAll
            // 
            this.btnGettAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnGettAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGettAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGettAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGettAll.ForeColor = System.Drawing.Color.White;
            this.btnGettAll.Location = new System.Drawing.Point(742, 2);
            this.btnGettAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnGettAll.Name = "btnGettAll";
            this.btnGettAll.Size = new System.Drawing.Size(82, 26);
            this.btnGettAll.TabIndex = 20;
            this.btnGettAll.Text = "GetAll";
            this.btnGettAll.UseVisualStyleBackColor = false;
            this.btnGettAll.Click += new System.EventHandler(this.btnGettAll_Click);
            // 
            // chkcustomername1
            // 
            this.chkcustomername1.AutoSize = true;
            this.chkcustomername1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkcustomername1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcustomername1.Location = new System.Drawing.Point(6, 2);
            this.chkcustomername1.Margin = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this.chkcustomername1.Name = "chkcustomername1";
            this.chkcustomername1.Size = new System.Drawing.Size(124, 26);
            this.chkcustomername1.TabIndex = 17;
            this.chkcustomername1.Text = "Customer Name";
            this.chkcustomername1.UseVisualStyleBackColor = true;
            // 
            // cmbcustomername1
            // 
            this.cmbcustomername1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcustomername1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcustomername1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcustomername1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustomername1.FormattingEnabled = true;
            this.cmbcustomername1.Location = new System.Drawing.Point(134, 2);
            this.cmbcustomername1.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustomername1.Name = "cmbcustomername1";
            this.cmbcustomername1.Size = new System.Drawing.Size(177, 25);
            this.cmbcustomername1.TabIndex = 18;
            this.cmbcustomername1.Leave += new System.EventHandler(this.cmbcustomername1_Leave);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label16.Location = new System.Drawing.Point(315, 2);
            this.label16.Margin = new System.Windows.Forms.Padding(2);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(95, 26);
            this.label16.TabIndex = 30;
            this.label16.Text = "Total Bonus:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbltbonus
            // 
            this.lbltbonus.AutoSize = true;
            this.lbltbonus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltbonus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbltbonus.Location = new System.Drawing.Point(414, 2);
            this.lbltbonus.Margin = new System.Windows.Forms.Padding(2);
            this.lbltbonus.Name = "lbltbonus";
            this.lbltbonus.Size = new System.Drawing.Size(119, 26);
            this.lbltbonus.TabIndex = 31;
            this.lbltbonus.Text = "0.00";
            this.lbltbonus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sgrpBox1
            // 
            this.sgrpBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.sgrpBox1.Controls.Add(this.tblltInputs);
            this.sgrpBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sgrpBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sgrpBox1.Location = new System.Drawing.Point(2, 39);
            this.sgrpBox1.Margin = new System.Windows.Forms.Padding(2);
            this.sgrpBox1.Name = "sgrpBox1";
            this.sgrpBox1.Padding = new System.Windows.Forms.Padding(2);
            this.sgrpBox1.Size = new System.Drawing.Size(830, 229);
            this.sgrpBox1.TabIndex = 0;
            this.sgrpBox1.TabStop = false;
            this.sgrpBox1.Text = "Customer Bonus Information";
            // 
            // tblltInputs
            // 
            this.tblltInputs.ColumnCount = 6;
            this.tblltInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblltInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblltInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltInputs.Controls.Add(this.txtnarration, 4, 5);
            this.tblltInputs.Controls.Add(this.label11, 3, 5);
            this.tblltInputs.Controls.Add(this.txtchequeno, 4, 4);
            this.tblltInputs.Controls.Add(this.label15, 3, 4);
            this.tblltInputs.Controls.Add(this.label13, 3, 3);
            this.tblltInputs.Controls.Add(this.tblltButtons, 0, 6);
            this.tblltInputs.Controls.Add(this.dtpchequedate, 4, 3);
            this.tblltInputs.Controls.Add(this.label14, 3, 0);
            this.tblltInputs.Controls.Add(this.txtRemainingAmt, 1, 5);
            this.tblltInputs.Controls.Add(this.cmbpaymentmode, 4, 0);
            this.tblltInputs.Controls.Add(this.label6, 0, 5);
            this.tblltInputs.Controls.Add(this.label10, 5, 0);
            this.tblltInputs.Controls.Add(this.tblltCustAdd, 2, 2);
            this.tblltInputs.Controls.Add(this.cmbbank, 4, 2);
            this.tblltInputs.Controls.Add(this.txtPaidAmount, 1, 4);
            this.tblltInputs.Controls.Add(this.label12, 3, 1);
            this.tblltInputs.Controls.Add(this.txtBankName, 4, 1);
            this.tblltInputs.Controls.Add(this.label3, 0, 4);
            this.tblltInputs.Controls.Add(this.txtTotalAmt, 1, 3);
            this.tblltInputs.Controls.Add(this.cmbcustomer, 1, 2);
            this.tblltInputs.Controls.Add(this.label5, 0, 3);
            this.tblltInputs.Controls.Add(this.label1, 0, 2);
            this.tblltInputs.Controls.Add(this.label7, 0, 1);
            this.tblltInputs.Controls.Add(this.dtpreceiptDate, 1, 1);
            this.tblltInputs.Controls.Add(this.label2, 0, 0);
            this.tblltInputs.Controls.Add(this.txtreciptNo, 1, 0);
            this.tblltInputs.Controls.Add(this.label4, 3, 2);
            this.tblltInputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltInputs.Location = new System.Drawing.Point(2, 20);
            this.tblltInputs.Margin = new System.Windows.Forms.Padding(0);
            this.tblltInputs.Name = "tblltInputs";
            this.tblltInputs.RowCount = 7;
            this.tblltInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltInputs.Size = new System.Drawing.Size(826, 207);
            this.tblltInputs.TabIndex = 0;
            // 
            // txtnarration
            // 
            this.tblltInputs.SetColumnSpan(this.txtnarration, 2);
            this.txtnarration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtnarration.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnarration.Location = new System.Drawing.Point(554, 147);
            this.txtnarration.Margin = new System.Windows.Forms.Padding(2);
            this.txtnarration.Multiline = true;
            this.txtnarration.Name = "txtnarration";
            this.txtnarration.Size = new System.Drawing.Size(270, 25);
            this.txtnarration.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(414, 147);
            this.label11.Margin = new System.Windows.Forms.Padding(2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(136, 25);
            this.label11.TabIndex = 108;
            this.label11.Text = "Narration:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtchequeno
            // 
            this.txtchequeno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtchequeno.Enabled = false;
            this.txtchequeno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtchequeno.Location = new System.Drawing.Point(554, 118);
            this.txtchequeno.Margin = new System.Windows.Forms.Padding(2);
            this.txtchequeno.Name = "txtchequeno";
            this.txtchequeno.Size = new System.Drawing.Size(202, 25);
            this.txtchequeno.TabIndex = 10;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(414, 118);
            this.label15.Margin = new System.Windows.Forms.Padding(2);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(136, 25);
            this.label15.TabIndex = 26;
            this.label15.Text = "Cheque No:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(414, 89);
            this.label13.Margin = new System.Windows.Forms.Padding(2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(136, 25);
            this.label13.TabIndex = 25;
            this.label13.Text = "Cheque Date:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 7;
            this.tblltInputs.SetColumnSpan(this.tblltButtons, 6);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltButtons.Controls.Add(this.btnnew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnSave, 2, 0);
            this.tblltButtons.Controls.Add(this.btnUnpaidList, 5, 0);
            this.tblltButtons.Controls.Add(this.btnclose, 3, 0);
            this.tblltButtons.Controls.Add(this.btndelete, 4, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 174);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(826, 33);
            this.tblltButtons.TabIndex = 12;
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(117, 2);
            this.btnnew.Margin = new System.Windows.Forms.Padding(2);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(103, 29);
            this.btnnew.TabIndex = 13;
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
            this.btnSave.Location = new System.Drawing.Point(224, 2);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(103, 29);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUnpaidList
            // 
            this.btnUnpaidList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnUnpaidList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUnpaidList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUnpaidList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUnpaidList.ForeColor = System.Drawing.Color.White;
            this.btnUnpaidList.Location = new System.Drawing.Point(545, 2);
            this.btnUnpaidList.Margin = new System.Windows.Forms.Padding(2);
            this.btnUnpaidList.Name = "btnUnpaidList";
            this.btnUnpaidList.Size = new System.Drawing.Size(161, 29);
            this.btnUnpaidList.TabIndex = 16;
            this.btnUnpaidList.Text = "Unpaid Bonus List";
            this.btnUnpaidList.UseVisualStyleBackColor = false;
            this.btnUnpaidList.Click += new System.EventHandler(this.btnpaidList_Click);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnclose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnclose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(331, 2);
            this.btnclose.Margin = new System.Windows.Forms.Padding(2);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(103, 29);
            this.btnclose.TabIndex = 14;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btndelete
            // 
            this.btndelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btndelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btndelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btndelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.Location = new System.Drawing.Point(438, 2);
            this.btndelete.Margin = new System.Windows.Forms.Padding(2);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(103, 29);
            this.btndelete.TabIndex = 15;
            this.btndelete.Text = "Delete";
            this.btndelete.UseVisualStyleBackColor = false;
            // 
            // dtpchequedate
            // 
            this.dtpchequedate.CustomFormat = "dd/MM/yyyy";
            this.dtpchequedate.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtpchequedate.Enabled = false;
            this.dtpchequedate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpchequedate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpchequedate.Location = new System.Drawing.Point(554, 89);
            this.dtpchequedate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpchequedate.Name = "dtpchequedate";
            this.dtpchequedate.Size = new System.Drawing.Size(120, 25);
            this.dtpchequedate.TabIndex = 9;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(414, 2);
            this.label14.Margin = new System.Windows.Forms.Padding(2);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(136, 25);
            this.label14.TabIndex = 23;
            this.label14.Text = "Payment Mode:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRemainingAmt
            // 
            this.txtRemainingAmt.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtRemainingAmt.Enabled = false;
            this.txtRemainingAmt.Location = new System.Drawing.Point(142, 147);
            this.txtRemainingAmt.Margin = new System.Windows.Forms.Padding(2);
            this.txtRemainingAmt.Name = "txtRemainingAmt";
            this.txtRemainingAmt.Size = new System.Drawing.Size(120, 25);
            this.txtRemainingAmt.TabIndex = 5;
            this.txtRemainingAmt.TabStop = false;
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
            "RTGS",
            "By Swipe"});
            this.cmbpaymentmode.Location = new System.Drawing.Point(554, 2);
            this.cmbpaymentmode.Margin = new System.Windows.Forms.Padding(2);
            this.cmbpaymentmode.Name = "cmbpaymentmode";
            this.cmbpaymentmode.Size = new System.Drawing.Size(202, 25);
            this.cmbpaymentmode.TabIndex = 6;
            this.cmbpaymentmode.SelectedIndexChanged += new System.EventHandler(this.cmbpaymentmode_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label6.Location = new System.Drawing.Point(2, 147);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 25);
            this.label6.TabIndex = 23;
            this.label6.Text = "Remaining Amount:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label10.Location = new System.Drawing.Point(758, 0);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 29);
            this.label10.TabIndex = 100;
            this.label10.Text = "*";
            // 
            // tblltCustAdd
            // 
            this.tblltCustAdd.ColumnCount = 2;
            this.tblltCustAdd.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltCustAdd.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltCustAdd.Controls.Add(this.label8, 1, 0);
            this.tblltCustAdd.Controls.Add(this.button1, 0, 0);
            this.tblltCustAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltCustAdd.Location = new System.Drawing.Point(346, 58);
            this.tblltCustAdd.Margin = new System.Windows.Forms.Padding(0);
            this.tblltCustAdd.Name = "tblltCustAdd";
            this.tblltCustAdd.RowCount = 1;
            this.tblltCustAdd.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltCustAdd.Size = new System.Drawing.Size(66, 29);
            this.tblltCustAdd.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(33, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 29);
            this.label8.TabIndex = 98;
            this.label8.Text = "*";
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::AIOInventorySystem.Desk.Properties.Resources.newbutton;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(2, 2);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(29, 25);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cmbbank
            // 
            this.cmbbank.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbbank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbbank.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbbank.FormattingEnabled = true;
            this.cmbbank.Location = new System.Drawing.Point(554, 60);
            this.cmbbank.Margin = new System.Windows.Forms.Padding(2);
            this.cmbbank.Name = "cmbbank";
            this.cmbbank.Size = new System.Drawing.Size(202, 25);
            this.cmbbank.TabIndex = 8;
            // 
            // txtPaidAmount
            // 
            this.txtPaidAmount.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtPaidAmount.Location = new System.Drawing.Point(142, 118);
            this.txtPaidAmount.Margin = new System.Windows.Forms.Padding(2);
            this.txtPaidAmount.Name = "txtPaidAmount";
            this.txtPaidAmount.Size = new System.Drawing.Size(120, 25);
            this.txtPaidAmount.TabIndex = 4;
            this.txtPaidAmount.TextChanged += new System.EventHandler(this.txtPaidAmount_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(414, 31);
            this.label12.Margin = new System.Windows.Forms.Padding(2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(136, 25);
            this.label12.TabIndex = 25;
            this.label12.Text = "Bank Name:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBankName
            // 
            this.txtBankName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBankName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBankName.Location = new System.Drawing.Point(554, 31);
            this.txtBankName.Margin = new System.Windows.Forms.Padding(2);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Size = new System.Drawing.Size(202, 25);
            this.txtBankName.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label3.Location = new System.Drawing.Point(2, 118);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 25);
            this.label3.TabIndex = 23;
            this.label3.Text = "Paid Amount:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalAmt
            // 
            this.txtTotalAmt.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtTotalAmt.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalAmt.Location = new System.Drawing.Point(142, 89);
            this.txtTotalAmt.Margin = new System.Windows.Forms.Padding(2);
            this.txtTotalAmt.Name = "txtTotalAmt";
            this.txtTotalAmt.ReadOnly = true;
            this.txtTotalAmt.Size = new System.Drawing.Size(120, 25);
            this.txtTotalAmt.TabIndex = 4;
            this.txtTotalAmt.TabStop = false;
            this.txtTotalAmt.Text = "0";
            // 
            // cmbcustomer
            // 
            this.cmbcustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcustomer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustomer.FormattingEnabled = true;
            this.cmbcustomer.Location = new System.Drawing.Point(142, 60);
            this.cmbcustomer.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustomer.Name = "cmbcustomer";
            this.cmbcustomer.Size = new System.Drawing.Size(202, 25);
            this.cmbcustomer.TabIndex = 2;
            this.cmbcustomer.SelectedIndexChanged += new System.EventHandler(this.cmbcustomer_SelectedIndexChanged);
            this.cmbcustomer.Leave += new System.EventHandler(this.cmbcustomer_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(2, 89);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 25);
            this.label5.TabIndex = 21;
            this.label5.Text = "Total Bonus Amount:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 25);
            this.label1.TabIndex = 19;
            this.label1.Text = "Customer Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(2, 31);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 25);
            this.label7.TabIndex = 17;
            this.label7.Text = "Date:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpreceiptDate
            // 
            this.dtpreceiptDate.CustomFormat = "dd/MM/yyyy";
            this.dtpreceiptDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtpreceiptDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpreceiptDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpreceiptDate.Location = new System.Drawing.Point(142, 31);
            this.dtpreceiptDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpreceiptDate.Name = "dtpreceiptDate";
            this.dtpreceiptDate.Size = new System.Drawing.Size(120, 25);
            this.dtpreceiptDate.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.label2.Location = new System.Drawing.Point(2, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 25);
            this.label2.TabIndex = 103;
            this.label2.Text = "Receipt No:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtreciptNo
            // 
            this.txtreciptNo.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtreciptNo.Enabled = false;
            this.txtreciptNo.Location = new System.Drawing.Point(142, 2);
            this.txtreciptNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtreciptNo.Name = "txtreciptNo";
            this.txtreciptNo.Size = new System.Drawing.Size(120, 25);
            this.txtreciptNo.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(414, 60);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 25);
            this.label4.TabIndex = 24;
            this.label4.Text = "Deposit In:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmBonus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(834, 632);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmBonus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bonus";
            this.tblltMain.ResumeLayout(false);
            this.sgrpBox2.ResumeLayout(false);
            this.tblltSerachInfo.ResumeLayout(false);
            this.tblltSerachInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvreceiptInfo)).EndInit();
            this.sgrpBox1.ResumeLayout(false);
            this.tblltInputs.ResumeLayout(false);
            this.tblltInputs.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.tblltCustAdd.ResumeLayout(false);
            this.tblltCustAdd.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.GroupBox sgrpBox2;
        private System.Windows.Forms.TableLayoutPanel tblltSerachInfo;
        private System.Windows.Forms.CheckBox chkcustomername1;
        private System.Windows.Forms.ComboBox cmbcustomername1;
        private System.Windows.Forms.DataGridView GvreceiptInfo;
        private System.Windows.Forms.GroupBox sgrpBox1;
        private System.Windows.Forms.TableLayoutPanel tblltInputs;
        private System.Windows.Forms.TableLayoutPanel tblltCustAdd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpreceiptDate;
        private System.Windows.Forms.ComboBox cmbcustomer;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbpaymentmode;
        private System.Windows.Forms.ComboBox cmbbank;
        private System.Windows.Forms.DateTimePicker dtpchequedate;
        private System.Windows.Forms.TextBox txtchequeno;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtnarration;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTotalAmt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtreciptNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRemainingAmt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPaidAmount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtBankName;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lbltbonus;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Button btnGettAll;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUnpaidList;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btndelete;


    }
}