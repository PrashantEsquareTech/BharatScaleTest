namespace AIOInventorySystem.Desk.Forms
{
    partial class frmAllProductInformation
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
            this.pnlUpdateGST = new System.Windows.Forms.Panel();
            this.tblltUpdateGST = new System.Windows.Forms.TableLayoutPanel();
            this.btnGSTClose = new System.Windows.Forms.Button();
            this.btnUpdateGST = new System.Windows.Forms.Button();
            this.label55 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.cmbPIgst = new System.Windows.Forms.ComboBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.cmbPCgst = new System.Windows.Forms.ComboBox();
            this.cmbPSgst = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.cmbigst = new System.Windows.Forms.ComboBox();
            this.label33 = new System.Windows.Forms.Label();
            this.cmbcgst = new System.Windows.Forms.ComboBox();
            this.cmbsgst = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblltRow3 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbProductType = new System.Windows.Forms.ComboBox();
            this.chkProductType = new System.Windows.Forms.CheckBox();
            this.btnsearch = new System.Windows.Forms.Button();
            this.btngetall = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.btncopyProducts = new System.Windows.Forms.Button();
            this.btnexportExcel = new System.Windows.Forms.Button();
            this.lnkQuickUpdate = new System.Windows.Forms.LinkLabel();
            this.label9 = new System.Windows.Forms.Label();
            this.tblltRow1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbdestination = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.cmbBarcodeStatus = new System.Windows.Forms.ComboBox();
            this.chkPrefix = new System.Windows.Forms.CheckBox();
            this.txtpname = new System.Windows.Forms.TextBox();
            this.chkBracodeStatus = new System.Windows.Forms.CheckBox();
            this.chkpname = new System.Windows.Forms.CheckBox();
            this.cmbgroupMaster = new System.Windows.Forms.ComboBox();
            this.cmbcomanyname = new System.Windows.Forms.ComboBox();
            this.chkgroupname = new System.Windows.Forms.CheckBox();
            this.chkcompanyname = new System.Windows.Forms.CheckBox();
            this.cmbPrefix = new System.Windows.Forms.ComboBox();
            this.GvproductInfo = new System.Windows.Forms.DataGridView();
            this.Updateg = new System.Windows.Forms.DataGridViewImageColumn();
            this.Remove = new System.Windows.Forms.DataGridViewImageColumn();
            this.pnlUpdateGST.SuspendLayout();
            this.tblltUpdateGST.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.tblltRow3.SuspendLayout();
            this.tblltRow1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvproductInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlUpdateGST
            // 
            this.pnlUpdateGST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUpdateGST.Controls.Add(this.tblltUpdateGST);
            this.pnlUpdateGST.Location = new System.Drawing.Point(414, 129);
            this.pnlUpdateGST.Margin = new System.Windows.Forms.Padding(0);
            this.pnlUpdateGST.Name = "pnlUpdateGST";
            this.pnlUpdateGST.Size = new System.Drawing.Size(220, 140);
            this.pnlUpdateGST.TabIndex = 93;
            this.pnlUpdateGST.Visible = false;
            // 
            // tblltUpdateGST
            // 
            this.tblltUpdateGST.ColumnCount = 4;
            this.tblltUpdateGST.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblltUpdateGST.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.tblltUpdateGST.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.tblltUpdateGST.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltUpdateGST.Controls.Add(this.btnGSTClose, 3, 0);
            this.tblltUpdateGST.Controls.Add(this.btnUpdateGST, 1, 4);
            this.tblltUpdateGST.Controls.Add(this.label55, 2, 0);
            this.tblltUpdateGST.Controls.Add(this.label38, 0, 3);
            this.tblltUpdateGST.Controls.Add(this.cmbPIgst, 1, 3);
            this.tblltUpdateGST.Controls.Add(this.label37, 0, 2);
            this.tblltUpdateGST.Controls.Add(this.label56, 1, 0);
            this.tblltUpdateGST.Controls.Add(this.label36, 0, 1);
            this.tblltUpdateGST.Controls.Add(this.cmbPCgst, 1, 2);
            this.tblltUpdateGST.Controls.Add(this.cmbPSgst, 1, 1);
            this.tblltUpdateGST.Controls.Add(this.label32, 3, 1);
            this.tblltUpdateGST.Controls.Add(this.label35, 3, 2);
            this.tblltUpdateGST.Controls.Add(this.cmbigst, 2, 3);
            this.tblltUpdateGST.Controls.Add(this.label33, 3, 3);
            this.tblltUpdateGST.Controls.Add(this.cmbcgst, 2, 2);
            this.tblltUpdateGST.Controls.Add(this.cmbsgst, 2, 1);
            this.tblltUpdateGST.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltUpdateGST.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblltUpdateGST.Location = new System.Drawing.Point(0, 0);
            this.tblltUpdateGST.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltUpdateGST.Name = "tblltUpdateGST";
            this.tblltUpdateGST.RowCount = 5;
            this.tblltUpdateGST.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltUpdateGST.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltUpdateGST.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltUpdateGST.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltUpdateGST.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltUpdateGST.Size = new System.Drawing.Size(218, 138);
            this.tblltUpdateGST.TabIndex = 20;
            // 
            // btnGSTClose
            // 
            this.btnGSTClose.BackColor = System.Drawing.Color.Red;
            this.btnGSTClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGSTClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGSTClose.ForeColor = System.Drawing.Color.Snow;
            this.btnGSTClose.Location = new System.Drawing.Point(187, 2);
            this.btnGSTClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnGSTClose.Name = "btnGSTClose";
            this.btnGSTClose.Size = new System.Drawing.Size(29, 23);
            this.btnGSTClose.TabIndex = 28;
            this.btnGSTClose.Text = "X";
            this.btnGSTClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGSTClose.UseVisualStyleBackColor = false;
            this.btnGSTClose.Click += new System.EventHandler(this.btnGSTClose_Click);
            // 
            // btnUpdateGST
            // 
            this.btnUpdateGST.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnUpdateGST.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tblltUpdateGST.SetColumnSpan(this.btnUpdateGST, 2);
            this.btnUpdateGST.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpdateGST.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateGST.ForeColor = System.Drawing.Color.White;
            this.btnUpdateGST.Location = new System.Drawing.Point(64, 109);
            this.btnUpdateGST.Margin = new System.Windows.Forms.Padding(17, 1, 17, 1);
            this.btnUpdateGST.Name = "btnUpdateGST";
            this.btnUpdateGST.Size = new System.Drawing.Size(104, 28);
            this.btnUpdateGST.TabIndex = 27;
            this.btnUpdateGST.Text = "Update GST";
            this.btnUpdateGST.UseVisualStyleBackColor = false;
            this.btnUpdateGST.Click += new System.EventHandler(this.btnUpdateGST_Click);
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label55.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.Location = new System.Drawing.Point(118, 2);
            this.label55.Margin = new System.Windows.Forms.Padding(2);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(65, 23);
            this.label55.TabIndex = 127;
            this.label55.Text = "Sales";
            this.label55.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label38.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.Location = new System.Drawing.Point(2, 83);
            this.label38.Margin = new System.Windows.Forms.Padding(2);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(43, 23);
            this.label38.TabIndex = 126;
            this.label38.Text = "IGst:";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPIgst
            // 
            this.cmbPIgst.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPIgst.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPIgst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPIgst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPIgst.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPIgst.FormattingEnabled = true;
            this.cmbPIgst.Location = new System.Drawing.Point(49, 83);
            this.cmbPIgst.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPIgst.Name = "cmbPIgst";
            this.cmbPIgst.Size = new System.Drawing.Size(65, 29);
            this.cmbPIgst.TabIndex = 23;
            this.cmbPIgst.SelectedIndexChanged += new System.EventHandler(this.cmbPIgst_SelectedIndexChanged);
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label37.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.ForeColor = System.Drawing.Color.Black;
            this.label37.Location = new System.Drawing.Point(2, 56);
            this.label37.Margin = new System.Windows.Forms.Padding(2);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(43, 23);
            this.label37.TabIndex = 125;
            this.label37.Text = "CGst:";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label56.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label56.Location = new System.Drawing.Point(49, 2);
            this.label56.Margin = new System.Windows.Forms.Padding(2);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(65, 23);
            this.label56.TabIndex = 97;
            this.label56.Text = "Purchase";
            this.label56.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label36.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.Black;
            this.label36.Location = new System.Drawing.Point(2, 29);
            this.label36.Margin = new System.Windows.Forms.Padding(2);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(43, 23);
            this.label36.TabIndex = 124;
            this.label36.Text = "SGst:";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbPCgst
            // 
            this.cmbPCgst.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPCgst.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPCgst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPCgst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPCgst.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPCgst.FormattingEnabled = true;
            this.cmbPCgst.Location = new System.Drawing.Point(49, 56);
            this.cmbPCgst.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPCgst.Name = "cmbPCgst";
            this.cmbPCgst.Size = new System.Drawing.Size(65, 29);
            this.cmbPCgst.TabIndex = 22;
            this.cmbPCgst.SelectedIndexChanged += new System.EventHandler(this.cmbPCgst_SelectedIndexChanged);
            // 
            // cmbPSgst
            // 
            this.cmbPSgst.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbPSgst.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbPSgst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPSgst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPSgst.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPSgst.FormattingEnabled = true;
            this.cmbPSgst.Location = new System.Drawing.Point(49, 29);
            this.cmbPSgst.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPSgst.Name = "cmbPSgst";
            this.cmbPSgst.Size = new System.Drawing.Size(65, 29);
            this.cmbPSgst.TabIndex = 21;
            this.cmbPSgst.SelectedIndexChanged += new System.EventHandler(this.cmbPSgst_SelectedIndexChanged);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label32.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label32.Location = new System.Drawing.Point(188, 27);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(27, 27);
            this.label32.TabIndex = 123;
            this.label32.Text = "%";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label35.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label35.Location = new System.Drawing.Point(188, 54);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(27, 27);
            this.label35.TabIndex = 121;
            this.label35.Text = "%";
            // 
            // cmbigst
            // 
            this.cmbigst.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbigst.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbigst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbigst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbigst.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbigst.FormattingEnabled = true;
            this.cmbigst.Location = new System.Drawing.Point(118, 83);
            this.cmbigst.Margin = new System.Windows.Forms.Padding(2);
            this.cmbigst.Name = "cmbigst";
            this.cmbigst.Size = new System.Drawing.Size(65, 29);
            this.cmbigst.TabIndex = 26;
            this.cmbigst.SelectedIndexChanged += new System.EventHandler(this.cmbigst_SelectedIndexChanged);
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label33.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label33.Location = new System.Drawing.Point(188, 81);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(27, 27);
            this.label33.TabIndex = 122;
            this.label33.Text = "%";
            // 
            // cmbcgst
            // 
            this.cmbcgst.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcgst.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcgst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcgst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcgst.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcgst.FormattingEnabled = true;
            this.cmbcgst.Location = new System.Drawing.Point(118, 56);
            this.cmbcgst.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcgst.Name = "cmbcgst";
            this.cmbcgst.Size = new System.Drawing.Size(65, 29);
            this.cmbcgst.TabIndex = 25;
            this.cmbcgst.SelectedIndexChanged += new System.EventHandler(this.cmbcgst_SelectedIndexChanged);
            // 
            // cmbsgst
            // 
            this.cmbsgst.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbsgst.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbsgst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbsgst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbsgst.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbsgst.FormattingEnabled = true;
            this.cmbsgst.Location = new System.Drawing.Point(118, 29);
            this.cmbsgst.Margin = new System.Windows.Forms.Padding(2);
            this.cmbsgst.Name = "cmbsgst";
            this.cmbsgst.Size = new System.Drawing.Size(65, 29);
            this.cmbsgst.TabIndex = 24;
            this.cmbsgst.SelectedIndexChanged += new System.EventHandler(this.cmbsgst_SelectedIndexChanged);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(150, 296);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(0);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(670, 25);
            this.progressBar1.TabIndex = 132;
            this.progressBar1.Visible = false;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.tblltRow3, 0, 2);
            this.tblltMain.Controls.Add(this.label9, 0, 0);
            this.tblltMain.Controls.Add(this.tblltRow1, 0, 1);
            this.tblltMain.Controls.Add(this.GvproductInfo, 0, 3);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblltMain.Size = new System.Drawing.Size(964, 612);
            this.tblltMain.TabIndex = 0;
            // 
            // tblltRow3
            // 
            this.tblltRow3.ColumnCount = 8;
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltRow3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltRow3.Controls.Add(this.cmbProductType, 0, 0);
            this.tblltRow3.Controls.Add(this.chkProductType, 0, 0);
            this.tblltRow3.Controls.Add(this.btnsearch, 3, 0);
            this.tblltRow3.Controls.Add(this.btngetall, 4, 0);
            this.tblltRow3.Controls.Add(this.btnprint, 5, 0);
            this.tblltRow3.Controls.Add(this.btncopyProducts, 7, 0);
            this.tblltRow3.Controls.Add(this.btnexportExcel, 6, 0);
            this.tblltRow3.Controls.Add(this.lnkQuickUpdate, 2, 0);
            this.tblltRow3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow3.Location = new System.Drawing.Point(0, 97);
            this.tblltRow3.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow3.Name = "tblltRow3";
            this.tblltRow3.RowCount = 1;
            this.tblltRow3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltRow3.Size = new System.Drawing.Size(964, 30);
            this.tblltRow3.TabIndex = 12;
            // 
            // cmbProductType
            // 
            this.cmbProductType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbProductType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbProductType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbProductType.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProductType.FormattingEnabled = true;
            this.cmbProductType.Items.AddRange(new object[] {
            "Standard",
            "Serialized",
            "Assembly",
            "Matrix",
            "Kit",
            "Scrap",
            "Work Type"});
            this.cmbProductType.Location = new System.Drawing.Point(127, 2);
            this.cmbProductType.Margin = new System.Windows.Forms.Padding(2);
            this.cmbProductType.Name = "cmbProductType";
            this.cmbProductType.Size = new System.Drawing.Size(285, 29);
            this.cmbProductType.TabIndex = 12;
            // 
            // chkProductType
            // 
            this.chkProductType.AutoSize = true;
            this.chkProductType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkProductType.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkProductType.Location = new System.Drawing.Point(6, 2);
            this.chkProductType.Margin = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this.chkProductType.Name = "chkProductType";
            this.chkProductType.Size = new System.Drawing.Size(117, 26);
            this.chkProductType.TabIndex = 11;
            this.chkProductType.Text = "Product Type";
            this.chkProductType.UseVisualStyleBackColor = true;
            this.chkProductType.CheckedChanged += new System.EventHandler(this.chkProductType_CheckedChanged);
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(522, 2);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(73, 26);
            this.btnsearch.TabIndex = 14;
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
            this.btngetall.Location = new System.Drawing.Point(599, 2);
            this.btngetall.Margin = new System.Windows.Forms.Padding(2);
            this.btngetall.Name = "btngetall";
            this.btngetall.Size = new System.Drawing.Size(73, 26);
            this.btngetall.TabIndex = 15;
            this.btngetall.Text = "GetAll";
            this.btngetall.UseVisualStyleBackColor = false;
            this.btngetall.Click += new System.EventHandler(this.btngetall_Click);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(676, 2);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(73, 26);
            this.btnprint.TabIndex = 16;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // btncopyProducts
            // 
            this.btncopyProducts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btncopyProducts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btncopyProducts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btncopyProducts.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncopyProducts.ForeColor = System.Drawing.Color.White;
            this.btncopyProducts.Location = new System.Drawing.Point(859, 2);
            this.btncopyProducts.Margin = new System.Windows.Forms.Padding(2);
            this.btncopyProducts.Name = "btncopyProducts";
            this.btncopyProducts.Size = new System.Drawing.Size(103, 26);
            this.btncopyProducts.TabIndex = 18;
            this.btncopyProducts.Text = "Copy Product";
            this.btncopyProducts.UseVisualStyleBackColor = false;
            // 
            // btnexportExcel
            // 
            this.btnexportExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnexportExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnexportExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnexportExcel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnexportExcel.ForeColor = System.Drawing.Color.White;
            this.btnexportExcel.Location = new System.Drawing.Point(753, 2);
            this.btnexportExcel.Margin = new System.Windows.Forms.Padding(2);
            this.btnexportExcel.Name = "btnexportExcel";
            this.btnexportExcel.Size = new System.Drawing.Size(102, 26);
            this.btnexportExcel.TabIndex = 17;
            this.btnexportExcel.Text = "Export Excel";
            this.btnexportExcel.UseVisualStyleBackColor = false;
            this.btnexportExcel.Click += new System.EventHandler(this.btnexportExcel_Click);
            // 
            // lnkQuickUpdate
            // 
            this.lnkQuickUpdate.AutoSize = true;
            this.lnkQuickUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lnkQuickUpdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkQuickUpdate.Location = new System.Drawing.Point(416, 2);
            this.lnkQuickUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.lnkQuickUpdate.Name = "lnkQuickUpdate";
            this.lnkQuickUpdate.Size = new System.Drawing.Size(102, 26);
            this.lnkQuickUpdate.TabIndex = 13;
            this.lnkQuickUpdate.TabStop = true;
            this.lnkQuickUpdate.Text = "Quick Update";
            this.lnkQuickUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lnkQuickUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkQuickUpdate_LinkClicked);
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
            this.label9.Size = new System.Drawing.Size(964, 36);
            this.label9.TabIndex = 34;
            this.label9.Text = "Product Information";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tblltRow1
            // 
            this.tblltRow1.ColumnCount = 6;
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltRow1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltRow1.Controls.Add(this.cmbdestination, 5, 1);
            this.tblltRow1.Controls.Add(this.label22, 4, 1);
            this.tblltRow1.Controls.Add(this.cmbBarcodeStatus, 3, 1);
            this.tblltRow1.Controls.Add(this.chkPrefix, 4, 0);
            this.tblltRow1.Controls.Add(this.txtpname, 1, 0);
            this.tblltRow1.Controls.Add(this.chkBracodeStatus, 2, 1);
            this.tblltRow1.Controls.Add(this.chkpname, 0, 0);
            this.tblltRow1.Controls.Add(this.cmbgroupMaster, 1, 1);
            this.tblltRow1.Controls.Add(this.cmbcomanyname, 3, 0);
            this.tblltRow1.Controls.Add(this.chkgroupname, 0, 1);
            this.tblltRow1.Controls.Add(this.chkcompanyname, 2, 0);
            this.tblltRow1.Controls.Add(this.cmbPrefix, 5, 0);
            this.tblltRow1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltRow1.Location = new System.Drawing.Point(0, 36);
            this.tblltRow1.Margin = new System.Windows.Forms.Padding(0);
            this.tblltRow1.Name = "tblltRow1";
            this.tblltRow1.RowCount = 2;
            this.tblltRow1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltRow1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltRow1.Size = new System.Drawing.Size(964, 61);
            this.tblltRow1.TabIndex = 0;
            // 
            // cmbdestination
            // 
            this.cmbdestination.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbdestination.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbdestination.FormattingEnabled = true;
            this.cmbdestination.Location = new System.Drawing.Point(839, 32);
            this.cmbdestination.Margin = new System.Windows.Forms.Padding(2);
            this.cmbdestination.Name = "cmbdestination";
            this.cmbdestination.Size = new System.Drawing.Size(123, 29);
            this.cmbdestination.TabIndex = 10;
            this.cmbdestination.SelectedIndexChanged += new System.EventHandler(this.cmbdestination_SelectedIndexChanged);
            this.cmbdestination.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbdestination_KeyDown);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.Color.Black;
            this.label22.Location = new System.Drawing.Point(753, 32);
            this.label22.Margin = new System.Windows.Forms.Padding(2);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(82, 27);
            this.label22.TabIndex = 10;
            this.label22.Text = "Destination:";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbBarcodeStatus
            // 
            this.cmbBarcodeStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBarcodeStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBarcodeStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbBarcodeStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBarcodeStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBarcodeStatus.FormattingEnabled = true;
            this.cmbBarcodeStatus.Items.AddRange(new object[] {
            "No Barcode",
            "Static Barcode",
            "Our Barcode"});
            this.cmbBarcodeStatus.Location = new System.Drawing.Point(541, 32);
            this.cmbBarcodeStatus.Margin = new System.Windows.Forms.Padding(2);
            this.cmbBarcodeStatus.Name = "cmbBarcodeStatus";
            this.cmbBarcodeStatus.Size = new System.Drawing.Size(208, 29);
            this.cmbBarcodeStatus.TabIndex = 9;
            // 
            // chkPrefix
            // 
            this.chkPrefix.AutoSize = true;
            this.chkPrefix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkPrefix.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrefix.Location = new System.Drawing.Point(757, 2);
            this.chkPrefix.Margin = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this.chkPrefix.Name = "chkPrefix";
            this.chkPrefix.Size = new System.Drawing.Size(78, 26);
            this.chkPrefix.TabIndex = 4;
            this.chkPrefix.Text = "Prefix";
            this.chkPrefix.UseVisualStyleBackColor = true;
            this.chkPrefix.CheckedChanged += new System.EventHandler(this.chkPrefix_CheckedChanged);
            this.chkPrefix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkPrefix_KeyDown);
            // 
            // txtpname
            // 
            this.txtpname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtpname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtpname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtpname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpname.Location = new System.Drawing.Point(127, 2);
            this.txtpname.Margin = new System.Windows.Forms.Padding(2);
            this.txtpname.Name = "txtpname";
            this.txtpname.Size = new System.Drawing.Size(285, 29);
            this.txtpname.TabIndex = 1;
            this.txtpname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpname_KeyDown);
            this.txtpname.Leave += new System.EventHandler(this.txtpname_Leave);
            // 
            // chkBracodeStatus
            // 
            this.chkBracodeStatus.AutoSize = true;
            this.chkBracodeStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkBracodeStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBracodeStatus.Location = new System.Drawing.Point(420, 32);
            this.chkBracodeStatus.Margin = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this.chkBracodeStatus.Name = "chkBracodeStatus";
            this.chkBracodeStatus.Size = new System.Drawing.Size(117, 27);
            this.chkBracodeStatus.TabIndex = 8;
            this.chkBracodeStatus.Text = "Barcode Status";
            this.chkBracodeStatus.UseVisualStyleBackColor = true;
            // 
            // chkpname
            // 
            this.chkpname.AutoSize = true;
            this.chkpname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkpname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkpname.Location = new System.Drawing.Point(6, 2);
            this.chkpname.Margin = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this.chkpname.Name = "chkpname";
            this.chkpname.Size = new System.Drawing.Size(117, 26);
            this.chkpname.TabIndex = 0;
            this.chkpname.Text = "Product Name";
            this.chkpname.UseVisualStyleBackColor = true;
            this.chkpname.CheckedChanged += new System.EventHandler(this.chkpname_CheckedChanged_1);
            this.chkpname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkpname_KeyDown);
            // 
            // cmbgroupMaster
            // 
            this.cmbgroupMaster.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbgroupMaster.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbgroupMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbgroupMaster.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbgroupMaster.ForeColor = System.Drawing.Color.Black;
            this.cmbgroupMaster.FormattingEnabled = true;
            this.cmbgroupMaster.Location = new System.Drawing.Point(127, 32);
            this.cmbgroupMaster.Margin = new System.Windows.Forms.Padding(2);
            this.cmbgroupMaster.Name = "cmbgroupMaster";
            this.cmbgroupMaster.Size = new System.Drawing.Size(285, 29);
            this.cmbgroupMaster.TabIndex = 7;
            this.cmbgroupMaster.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbgroupMaster_KeyDown);
            // 
            // cmbcomanyname
            // 
            this.cmbcomanyname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcomanyname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcomanyname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcomanyname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcomanyname.ForeColor = System.Drawing.Color.Black;
            this.cmbcomanyname.FormattingEnabled = true;
            this.cmbcomanyname.Location = new System.Drawing.Point(541, 2);
            this.cmbcomanyname.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcomanyname.Name = "cmbcomanyname";
            this.cmbcomanyname.Size = new System.Drawing.Size(208, 29);
            this.cmbcomanyname.TabIndex = 3;
            this.cmbcomanyname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcomanyname_KeyDown);
            // 
            // chkgroupname
            // 
            this.chkgroupname.AutoSize = true;
            this.chkgroupname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkgroupname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkgroupname.Location = new System.Drawing.Point(6, 32);
            this.chkgroupname.Margin = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this.chkgroupname.Name = "chkgroupname";
            this.chkgroupname.Size = new System.Drawing.Size(117, 27);
            this.chkgroupname.TabIndex = 6;
            this.chkgroupname.Text = "Group Name";
            this.chkgroupname.UseVisualStyleBackColor = true;
            this.chkgroupname.CheckedChanged += new System.EventHandler(this.chkgroupname_CheckedChanged);
            this.chkgroupname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkgroupname_KeyDown);
            // 
            // chkcompanyname
            // 
            this.chkcompanyname.AutoSize = true;
            this.chkcompanyname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkcompanyname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcompanyname.Location = new System.Drawing.Point(420, 2);
            this.chkcompanyname.Margin = new System.Windows.Forms.Padding(6, 2, 2, 2);
            this.chkcompanyname.Name = "chkcompanyname";
            this.chkcompanyname.Size = new System.Drawing.Size(117, 26);
            this.chkcompanyname.TabIndex = 2;
            this.chkcompanyname.Text = "Mfg. Company";
            this.chkcompanyname.UseVisualStyleBackColor = true;
            this.chkcompanyname.CheckedChanged += new System.EventHandler(this.chkcompanyname_CheckedChanged_1);
            this.chkcompanyname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkcompanyname_KeyDown);
            // 
            // cmbPrefix
            // 
            this.cmbPrefix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbPrefix.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPrefix.FormattingEnabled = true;
            this.cmbPrefix.Location = new System.Drawing.Point(839, 2);
            this.cmbPrefix.Margin = new System.Windows.Forms.Padding(2);
            this.cmbPrefix.Name = "cmbPrefix";
            this.cmbPrefix.Size = new System.Drawing.Size(123, 29);
            this.cmbPrefix.TabIndex = 5;
            this.cmbPrefix.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbPrefix_KeyDown);
            // 
            // GvproductInfo
            // 
            this.GvproductInfo.AllowUserToAddRows = false;
            this.GvproductInfo.AllowUserToDeleteRows = false;
            this.GvproductInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvproductInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvproductInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GvproductInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvproductInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Updateg,
            this.Remove});
            this.GvproductInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvproductInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvproductInfo.Location = new System.Drawing.Point(2, 129);
            this.GvproductInfo.Margin = new System.Windows.Forms.Padding(2);
            this.GvproductInfo.Name = "GvproductInfo";
            this.GvproductInfo.ReadOnly = true;
            this.GvproductInfo.RowHeadersWidth = 20;
            this.GvproductInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvproductInfo.Size = new System.Drawing.Size(960, 481);
            this.GvproductInfo.TabIndex = 19;
            this.GvproductInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvproductInfo_CellClick);
            this.GvproductInfo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GvproductInfo_KeyPress);
            // 
            // Updateg
            // 
            this.Updateg.HeaderText = "Update";
            this.Updateg.Image = global::AIOInventorySystem.Desk.Properties.Resources.Update;
            this.Updateg.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Updateg.Name = "Updateg";
            this.Updateg.ReadOnly = true;
            // 
            // Remove
            // 
            this.Remove.HeaderText = "Remove";
            this.Remove.Image = global::AIOInventorySystem.Desk.Properties.Resources.Remove;
            this.Remove.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Remove.Name = "Remove";
            this.Remove.ReadOnly = true;
            // 
            // frmAllProductInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(964, 612);
            this.Controls.Add(this.pnlUpdateGST);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmAllProductInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "All Product Information";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAllProductInformation_FormClosed);
            this.Load += new System.EventHandler(this.frmAllProductInformation_Load);
            this.pnlUpdateGST.ResumeLayout(false);
            this.tblltUpdateGST.ResumeLayout(false);
            this.tblltUpdateGST.PerformLayout();
            this.tblltMain.ResumeLayout(false);
            this.tblltRow3.ResumeLayout(false);
            this.tblltRow3.PerformLayout();
            this.tblltRow1.ResumeLayout(false);
            this.tblltRow1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvproductInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkpname;
        private System.Windows.Forms.CheckBox chkcompanyname;
        private System.Windows.Forms.ComboBox cmbcomanyname;
        private System.Windows.Forms.TextBox txtpname;
        private System.Windows.Forms.DataGridView GvproductInfo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TableLayoutPanel tblltRow1;
        private System.Windows.Forms.ComboBox cmbgroupMaster;
        private System.Windows.Forms.CheckBox chkgroupname;
        private System.Windows.Forms.CheckBox chkPrefix;
        private System.Windows.Forms.ComboBox cmbPrefix;
        private System.Windows.Forms.ComboBox cmbdestination;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.CheckBox chkBracodeStatus;
        private System.Windows.Forms.ComboBox cmbBarcodeStatus;
        private System.Windows.Forms.LinkLabel lnkQuickUpdate;
        private System.Windows.Forms.Panel pnlUpdateGST;
        private System.Windows.Forms.Button btnGSTClose;
        private System.Windows.Forms.ComboBox cmbigst;
        private System.Windows.Forms.ComboBox cmbcgst;
        private System.Windows.Forms.ComboBox cmbsgst;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.ComboBox cmbPIgst;
        private System.Windows.Forms.ComboBox cmbPSgst;
        private System.Windows.Forms.ComboBox cmbPCgst;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltRow3;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Button btnexportExcel;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button btngetall;
        private System.Windows.Forms.Button btncopyProducts;
        private System.Windows.Forms.TableLayoutPanel tblltUpdateGST;
        private System.Windows.Forms.Button btnUpdateGST;
        private System.Windows.Forms.DataGridViewImageColumn Updateg;
        private System.Windows.Forms.DataGridViewImageColumn Remove;
        private System.Windows.Forms.CheckBox chkProductType;
        private System.Windows.Forms.ComboBox cmbProductType;
    }
}