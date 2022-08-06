namespace AIOInventorySystem.Desk.Forms
{
    partial class frmFestivals
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label5 = new System.Windows.Forms.Label();
            this.GvFestivalTemplate = new System.Windows.Forms.DataGridView();
            this.GvFestival = new System.Windows.Forms.DataGridView();
            this.label38 = new System.Windows.Forms.Label();
            this.cmbFestival = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFestivalName = new System.Windows.Forms.TextBox();
            this.txtFestivalDesc = new System.Windows.Forms.TextBox();
            this.dtpFestivalDate = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFetivalMsg = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtFestivalNo = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtFestivalTemplateNo = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.grpFestivalInfo = new System.Windows.Forms.GroupBox();
            this.tblltFestInfo = new System.Windows.Forms.TableLayoutPanel();
            this.tblltFestButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpFestList = new System.Windows.Forms.GroupBox();
            this.grpTemplateInfo = new System.Windows.Forms.GroupBox();
            this.tblltTempInfo = new System.Windows.Forms.TableLayoutPanel();
            this.tblltTempButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnTempNew = new System.Windows.Forms.Button();
            this.btnTempSave = new System.Windows.Forms.Button();
            this.btnTempClose = new System.Windows.Forms.Button();
            this.grpTemplateList = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.GvFestivalTemplate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvFestival)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.grpFestivalInfo.SuspendLayout();
            this.tblltFestInfo.SuspendLayout();
            this.tblltFestButtons.SuspendLayout();
            this.grpFestList.SuspendLayout();
            this.grpTemplateInfo.SuspendLayout();
            this.tblltTempInfo.SuspendLayout();
            this.tblltTempButtons.SuspendLayout();
            this.grpTemplateList.SuspendLayout();
            this.SuspendLayout();
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
            this.label5.Size = new System.Drawing.Size(884, 36);
            this.label5.TabIndex = 26;
            this.label5.Text = "Add Festivals ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GvFestivalTemplate
            // 
            this.GvFestivalTemplate.AllowUserToAddRows = false;
            this.GvFestivalTemplate.AllowUserToDeleteRows = false;
            this.GvFestivalTemplate.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvFestivalTemplate.BackgroundColor = System.Drawing.Color.White;
            this.GvFestivalTemplate.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvFestivalTemplate.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GvFestivalTemplate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvFestivalTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvFestivalTemplate.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvFestivalTemplate.Location = new System.Drawing.Point(3, 21);
            this.GvFestivalTemplate.Margin = new System.Windows.Forms.Padding(2);
            this.GvFestivalTemplate.Name = "GvFestivalTemplate";
            this.GvFestivalTemplate.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvFestivalTemplate.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.GvFestivalTemplate.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvFestivalTemplate.Size = new System.Drawing.Size(430, 177);
            this.GvFestivalTemplate.TabIndex = 118;
            this.GvFestivalTemplate.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvFestivalTemplate_CellClick);
            // 
            // GvFestival
            // 
            this.GvFestival.AllowUserToAddRows = false;
            this.GvFestival.AllowUserToDeleteRows = false;
            this.GvFestival.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvFestival.BackgroundColor = System.Drawing.Color.White;
            this.GvFestival.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvFestival.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvFestival.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvFestival.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvFestival.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvFestival.Location = new System.Drawing.Point(3, 21);
            this.GvFestival.Margin = new System.Windows.Forms.Padding(2);
            this.GvFestival.Name = "GvFestival";
            this.GvFestival.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvFestival.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GvFestival.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvFestival.Size = new System.Drawing.Size(430, 189);
            this.GvFestival.TabIndex = 117;
            this.GvFestival.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvFestival_CellClick);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label38.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.Location = new System.Drawing.Point(2, 30);
            this.label38.Margin = new System.Windows.Forms.Padding(2);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(137, 24);
            this.label38.TabIndex = 121;
            this.label38.Text = "Select Festivals:";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbFestival
            // 
            this.cmbFestival.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbFestival.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbFestival.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFestival.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFestival.FormattingEnabled = true;
            this.cmbFestival.Location = new System.Drawing.Point(143, 30);
            this.cmbFestival.Margin = new System.Windows.Forms.Padding(2);
            this.cmbFestival.Name = "cmbFestival";
            this.cmbFestival.Size = new System.Drawing.Size(258, 25);
            this.cmbFestival.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(2, 29);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 23);
            this.label4.TabIndex = 123;
            this.label4.Text = " Festival Name:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(2, 83);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 23);
            this.label6.TabIndex = 124;
            this.label6.Text = " Festival Description:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(2, 56);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 23);
            this.label7.TabIndex = 125;
            this.label7.Text = " Festival Date:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFestivalName
            // 
            this.txtFestivalName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFestivalName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFestivalName.Location = new System.Drawing.Point(143, 29);
            this.txtFestivalName.Margin = new System.Windows.Forms.Padding(2);
            this.txtFestivalName.Name = "txtFestivalName";
            this.txtFestivalName.Size = new System.Drawing.Size(258, 25);
            this.txtFestivalName.TabIndex = 1;
            // 
            // txtFestivalDesc
            // 
            this.txtFestivalDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFestivalDesc.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFestivalDesc.Location = new System.Drawing.Point(143, 83);
            this.txtFestivalDesc.Margin = new System.Windows.Forms.Padding(2);
            this.txtFestivalDesc.Multiline = true;
            this.txtFestivalDesc.Name = "txtFestivalDesc";
            this.tblltFestInfo.SetRowSpan(this.txtFestivalDesc, 2);
            this.txtFestivalDesc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFestivalDesc.Size = new System.Drawing.Size(258, 50);
            this.txtFestivalDesc.TabIndex = 3;
            // 
            // dtpFestivalDate
            // 
            this.dtpFestivalDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtpFestivalDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFestivalDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFestivalDate.Location = new System.Drawing.Point(143, 56);
            this.dtpFestivalDate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFestivalDate.Name = "dtpFestivalDate";
            this.dtpFestivalDate.Size = new System.Drawing.Size(126, 25);
            this.dtpFestivalDate.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(403, 27);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 27);
            this.label8.TabIndex = 129;
            this.label8.Text = "*";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label9.Location = new System.Drawing.Point(403, 28);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 28);
            this.label9.TabIndex = 130;
            this.label9.Text = "*";
            // 
            // txtFetivalMsg
            // 
            this.txtFetivalMsg.AcceptsReturn = true;
            this.txtFetivalMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFetivalMsg.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFetivalMsg.Location = new System.Drawing.Point(143, 58);
            this.txtFetivalMsg.Margin = new System.Windows.Forms.Padding(2);
            this.txtFetivalMsg.Multiline = true;
            this.txtFetivalMsg.Name = "txtFetivalMsg";
            this.tblltTempInfo.SetRowSpan(this.txtFetivalMsg, 3);
            this.txtFetivalMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtFetivalMsg.Size = new System.Drawing.Size(258, 80);
            this.txtFetivalMsg.TabIndex = 9;
            this.txtFetivalMsg.TextChanged += new System.EventHandler(this.txtFetivalMsg_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(2, 58);
            this.label10.Margin = new System.Windows.Forms.Padding(2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(137, 24);
            this.label10.TabIndex = 131;
            this.label10.Text = " Festival Message:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFestivalNo
            // 
            this.txtFestivalNo.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtFestivalNo.Enabled = false;
            this.txtFestivalNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFestivalNo.Location = new System.Drawing.Point(143, 2);
            this.txtFestivalNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtFestivalNo.Name = "txtFestivalNo";
            this.txtFestivalNo.Size = new System.Drawing.Size(126, 25);
            this.txtFestivalNo.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(2, 2);
            this.label11.Margin = new System.Windows.Forms.Padding(2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(137, 23);
            this.label11.TabIndex = 133;
            this.label11.Text = " Festival No:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFestivalTemplateNo
            // 
            this.txtFestivalTemplateNo.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtFestivalTemplateNo.Enabled = false;
            this.txtFestivalTemplateNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFestivalTemplateNo.Location = new System.Drawing.Point(143, 2);
            this.txtFestivalTemplateNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtFestivalTemplateNo.Name = "txtFestivalTemplateNo";
            this.txtFestivalTemplateNo.Size = new System.Drawing.Size(126, 25);
            this.txtFestivalTemplateNo.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(2, 2);
            this.label12.Margin = new System.Windows.Forms.Padding(2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(137, 24);
            this.label12.TabIndex = 135;
            this.label12.Text = "Template No:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 2;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.grpFestivalInfo, 0, 1);
            this.tblltMain.Controls.Add(this.grpFestList, 1, 1);
            this.tblltMain.Controls.Add(this.grpTemplateInfo, 0, 2);
            this.tblltMain.Controls.Add(this.grpTemplateList, 1, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 44.5F));
            this.tblltMain.Size = new System.Drawing.Size(884, 462);
            this.tblltMain.TabIndex = 0;
            // 
            // grpFestivalInfo
            // 
            this.grpFestivalInfo.Controls.Add(this.tblltFestInfo);
            this.grpFestivalInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpFestivalInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFestivalInfo.Location = new System.Drawing.Point(3, 39);
            this.grpFestivalInfo.Name = "grpFestivalInfo";
            this.grpFestivalInfo.Size = new System.Drawing.Size(436, 213);
            this.grpFestivalInfo.TabIndex = 0;
            this.grpFestivalInfo.TabStop = false;
            this.grpFestivalInfo.Text = "Festival Info";
            // 
            // tblltFestInfo
            // 
            this.tblltFestInfo.ColumnCount = 3;
            this.tblltFestInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tblltFestInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61F));
            this.tblltFestInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltFestInfo.Controls.Add(this.tblltFestButtons, 0, 6);
            this.tblltFestInfo.Controls.Add(this.txtFestivalNo, 1, 0);
            this.tblltFestInfo.Controls.Add(this.txtFestivalName, 1, 1);
            this.tblltFestInfo.Controls.Add(this.label8, 2, 1);
            this.tblltFestInfo.Controls.Add(this.dtpFestivalDate, 1, 2);
            this.tblltFestInfo.Controls.Add(this.txtFestivalDesc, 1, 3);
            this.tblltFestInfo.Controls.Add(this.label11, 0, 0);
            this.tblltFestInfo.Controls.Add(this.label4, 0, 1);
            this.tblltFestInfo.Controls.Add(this.label7, 0, 2);
            this.tblltFestInfo.Controls.Add(this.label6, 0, 3);
            this.tblltFestInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltFestInfo.Location = new System.Drawing.Point(3, 21);
            this.tblltFestInfo.Name = "tblltFestInfo";
            this.tblltFestInfo.RowCount = 7;
            this.tblltFestInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.5F));
            this.tblltFestInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.5F));
            this.tblltFestInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.5F));
            this.tblltFestInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.5F));
            this.tblltFestInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.5F));
            this.tblltFestInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltFestInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            this.tblltFestInfo.Size = new System.Drawing.Size(430, 189);
            this.tblltFestInfo.TabIndex = 0;
            // 
            // tblltFestButtons
            // 
            this.tblltFestButtons.ColumnCount = 5;
            this.tblltFestInfo.SetColumnSpan(this.tblltFestButtons, 3);
            this.tblltFestButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltFestButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltFestButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltFestButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltFestButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltFestButtons.Controls.Add(this.btnNew, 1, 0);
            this.tblltFestButtons.Controls.Add(this.btnSave, 2, 0);
            this.tblltFestButtons.Controls.Add(this.btnClose, 3, 0);
            this.tblltFestButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltFestButtons.Location = new System.Drawing.Point(0, 153);
            this.tblltFestButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltFestButtons.Name = "tblltFestButtons";
            this.tblltFestButtons.RowCount = 1;
            this.tblltFestButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltFestButtons.Size = new System.Drawing.Size(430, 36);
            this.tblltFestButtons.TabIndex = 4;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(88, 2);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(82, 32);
            this.btnNew.TabIndex = 5;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(174, 2);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 32);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(260, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(82, 32);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grpFestList
            // 
            this.grpFestList.Controls.Add(this.GvFestival);
            this.grpFestList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpFestList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpFestList.Location = new System.Drawing.Point(445, 39);
            this.grpFestList.Name = "grpFestList";
            this.grpFestList.Size = new System.Drawing.Size(436, 213);
            this.grpFestList.TabIndex = 28;
            this.grpFestList.TabStop = false;
            this.grpFestList.Text = "Festival List";
            // 
            // grpTemplateInfo
            // 
            this.grpTemplateInfo.Controls.Add(this.tblltTempInfo);
            this.grpTemplateInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTemplateInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTemplateInfo.Location = new System.Drawing.Point(3, 258);
            this.grpTemplateInfo.Name = "grpTemplateInfo";
            this.grpTemplateInfo.Size = new System.Drawing.Size(436, 201);
            this.grpTemplateInfo.TabIndex = 7;
            this.grpTemplateInfo.TabStop = false;
            this.grpTemplateInfo.Text = "Template Info";
            // 
            // tblltTempInfo
            // 
            this.tblltTempInfo.ColumnCount = 3;
            this.tblltTempInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tblltTempInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61F));
            this.tblltTempInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltTempInfo.Controls.Add(this.tblltTempButtons, 0, 5);
            this.tblltTempInfo.Controls.Add(this.label12, 0, 0);
            this.tblltTempInfo.Controls.Add(this.label38, 0, 1);
            this.tblltTempInfo.Controls.Add(this.label10, 0, 2);
            this.tblltTempInfo.Controls.Add(this.txtFestivalTemplateNo, 1, 0);
            this.tblltTempInfo.Controls.Add(this.cmbFestival, 1, 1);
            this.tblltTempInfo.Controls.Add(this.txtFetivalMsg, 1, 2);
            this.tblltTempInfo.Controls.Add(this.label9, 2, 1);
            this.tblltTempInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltTempInfo.Location = new System.Drawing.Point(3, 21);
            this.tblltTempInfo.Name = "tblltTempInfo";
            this.tblltTempInfo.RowCount = 6;
            this.tblltTempInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltTempInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltTempInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltTempInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltTempInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltTempInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltTempInfo.Size = new System.Drawing.Size(430, 177);
            this.tblltTempInfo.TabIndex = 7;
            // 
            // tblltTempButtons
            // 
            this.tblltTempButtons.ColumnCount = 5;
            this.tblltTempInfo.SetColumnSpan(this.tblltTempButtons, 3);
            this.tblltTempButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltTempButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltTempButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltTempButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltTempButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltTempButtons.Controls.Add(this.btnTempNew, 1, 0);
            this.tblltTempButtons.Controls.Add(this.btnTempSave, 2, 0);
            this.tblltTempButtons.Controls.Add(this.btnTempClose, 3, 0);
            this.tblltTempButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltTempButtons.Location = new System.Drawing.Point(0, 140);
            this.tblltTempButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltTempButtons.Name = "tblltTempButtons";
            this.tblltTempButtons.RowCount = 1;
            this.tblltTempButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltTempButtons.Size = new System.Drawing.Size(430, 37);
            this.tblltTempButtons.TabIndex = 10;
            // 
            // btnTempNew
            // 
            this.btnTempNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnTempNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTempNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTempNew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTempNew.ForeColor = System.Drawing.Color.White;
            this.btnTempNew.Location = new System.Drawing.Point(88, 2);
            this.btnTempNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnTempNew.Name = "btnTempNew";
            this.btnTempNew.Size = new System.Drawing.Size(82, 33);
            this.btnTempNew.TabIndex = 11;
            this.btnTempNew.Text = "New";
            this.btnTempNew.UseVisualStyleBackColor = false;
            this.btnTempNew.Click += new System.EventHandler(this.btnTempNew_Click);
            // 
            // btnTempSave
            // 
            this.btnTempSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnTempSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTempSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTempSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTempSave.ForeColor = System.Drawing.Color.White;
            this.btnTempSave.Location = new System.Drawing.Point(174, 2);
            this.btnTempSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnTempSave.Name = "btnTempSave";
            this.btnTempSave.Size = new System.Drawing.Size(82, 33);
            this.btnTempSave.TabIndex = 10;
            this.btnTempSave.Text = "Save";
            this.btnTempSave.UseVisualStyleBackColor = false;
            this.btnTempSave.Click += new System.EventHandler(this.btnTempSave_Click);
            // 
            // btnTempClose
            // 
            this.btnTempClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnTempClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTempClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTempClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTempClose.ForeColor = System.Drawing.Color.White;
            this.btnTempClose.Location = new System.Drawing.Point(260, 2);
            this.btnTempClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnTempClose.Name = "btnTempClose";
            this.btnTempClose.Size = new System.Drawing.Size(82, 33);
            this.btnTempClose.TabIndex = 12;
            this.btnTempClose.Text = "Close";
            this.btnTempClose.UseVisualStyleBackColor = false;
            this.btnTempClose.Click += new System.EventHandler(this.btnTempClose_Click);
            // 
            // grpTemplateList
            // 
            this.grpTemplateList.Controls.Add(this.GvFestivalTemplate);
            this.grpTemplateList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTemplateList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTemplateList.Location = new System.Drawing.Point(445, 258);
            this.grpTemplateList.Name = "grpTemplateList";
            this.grpTemplateList.Size = new System.Drawing.Size(436, 201);
            this.grpTemplateList.TabIndex = 30;
            this.grpTemplateList.TabStop = false;
            this.grpTemplateList.Text = "Template List";
            // 
            // frmFestivals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(884, 462);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmFestivals";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Festivals";
            this.Load += new System.EventHandler(this.frmFestivals_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvFestivalTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GvFestival)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.grpFestivalInfo.ResumeLayout(false);
            this.tblltFestInfo.ResumeLayout(false);
            this.tblltFestInfo.PerformLayout();
            this.tblltFestButtons.ResumeLayout(false);
            this.grpFestList.ResumeLayout(false);
            this.grpTemplateInfo.ResumeLayout(false);
            this.tblltTempInfo.ResumeLayout(false);
            this.tblltTempInfo.PerformLayout();
            this.tblltTempButtons.ResumeLayout(false);
            this.grpTemplateList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView GvFestivalTemplate;
        private System.Windows.Forms.DataGridView GvFestival;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.ComboBox cmbFestival;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFestivalName;
        private System.Windows.Forms.TextBox txtFestivalDesc;
        private System.Windows.Forms.DateTimePicker dtpFestivalDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFetivalMsg;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtFestivalNo;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtFestivalTemplateNo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.GroupBox grpFestivalInfo;
        private System.Windows.Forms.GroupBox grpFestList;
        private System.Windows.Forms.GroupBox grpTemplateInfo;
        private System.Windows.Forms.GroupBox grpTemplateList;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnTempClose;
        private System.Windows.Forms.Button btnTempSave;
        private System.Windows.Forms.Button btnTempNew;
        private System.Windows.Forms.TableLayoutPanel tblltFestInfo;
        private System.Windows.Forms.TableLayoutPanel tblltFestButtons;
        private System.Windows.Forms.TableLayoutPanel tblltTempInfo;
        private System.Windows.Forms.TableLayoutPanel tblltTempButtons;
    }
}