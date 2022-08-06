namespace AIOInventorySystem.Desk.Forms
{
    partial class frmcolor
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
            this.label11 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtColorName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtcolorid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotaluUnits = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbll = new System.Windows.Forms.TableLayoutPanel();
            this.grpColorInfo = new System.Windows.Forms.GroupBox();
            this.tblltColorInfo = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtonCat = new System.Windows.Forms.TableLayoutPanel();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpboxAllInfo = new System.Windows.Forms.GroupBox();
            this.tblltAllInfo = new System.Windows.Forms.TableLayoutPanel();
            this.dtgvList = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClrID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbll.SuspendLayout();
            this.grpColorInfo.SuspendLayout();
            this.tblltColorInfo.SuspendLayout();
            this.tblltButtonCat.SuspendLayout();
            this.grpboxAllInfo.SuspendLayout();
            this.tblltAllInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label11.Location = new System.Drawing.Point(355, 28);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 28);
            this.label11.TabIndex = 20;
            this.label11.Text = "*";
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(108, 58);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(2);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.tblltColorInfo.SetRowSpan(this.txtDescription, 2);
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDescription.Size = new System.Drawing.Size(245, 52);
            this.txtDescription.TabIndex = 3;
            // 
            // txtColorName
            // 
            this.txtColorName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtColorName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColorName.ForeColor = System.Drawing.Color.Black;
            this.txtColorName.Location = new System.Drawing.Point(108, 30);
            this.txtColorName.Margin = new System.Windows.Forms.Padding(2);
            this.txtColorName.Name = "txtColorName";
            this.txtColorName.Size = new System.Drawing.Size(245, 25);
            this.txtColorName.TabIndex = 2;
            this.txtColorName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtColorName_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(2, 58);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 24);
            this.label3.TabIndex = 10;
            this.label3.Text = "Description:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(2, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 24);
            this.label2.TabIndex = 9;
            this.label2.Text = "Color Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtcolorid
            // 
            this.txtcolorid.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtcolorid.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcolorid.ForeColor = System.Drawing.Color.Black;
            this.txtcolorid.Location = new System.Drawing.Point(108, 2);
            this.txtcolorid.Margin = new System.Windows.Forms.Padding(2);
            this.txtcolorid.Name = "txtcolorid";
            this.txtcolorid.ReadOnly = true;
            this.txtcolorid.Size = new System.Drawing.Size(100, 25);
            this.txtcolorid.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "Color ID:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotaluUnits
            // 
            this.lblTotaluUnits.AutoSize = true;
            this.lblTotaluUnits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotaluUnits.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotaluUnits.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotaluUnits.Location = new System.Drawing.Point(140, 2);
            this.lblTotaluUnits.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotaluUnits.Name = "lblTotaluUnits";
            this.lblTotaluUnits.Size = new System.Drawing.Size(254, 19);
            this.lblTotaluUnits.TabIndex = 58;
            this.lblTotaluUnits.Text = "0";
            this.lblTotaluUnits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label15.Location = new System.Drawing.Point(2, 2);
            this.label15.Margin = new System.Windows.Forms.Padding(2);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(134, 19);
            this.label15.TabIndex = 57;
            this.label15.Text = "Total Colors:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(404, 33);
            this.label5.TabIndex = 11;
            this.label5.Text = "Color Information";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tbll
            // 
            this.tbll.ColumnCount = 1;
            this.tbll.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbll.Controls.Add(this.label5, 0, 0);
            this.tbll.Controls.Add(this.grpColorInfo, 0, 1);
            this.tbll.Controls.Add(this.grpboxAllInfo, 0, 2);
            this.tbll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbll.Location = new System.Drawing.Point(0, 0);
            this.tbll.Margin = new System.Windows.Forms.Padding(0);
            this.tbll.Name = "tbll";
            this.tbll.RowCount = 3;
            this.tbll.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tbll.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39F));
            this.tbll.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54F));
            this.tbll.Size = new System.Drawing.Size(404, 482);
            this.tbll.TabIndex = 0;
            // 
            // grpColorInfo
            // 
            this.grpColorInfo.Controls.Add(this.tblltColorInfo);
            this.grpColorInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpColorInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpColorInfo.Location = new System.Drawing.Point(2, 35);
            this.grpColorInfo.Margin = new System.Windows.Forms.Padding(2);
            this.grpColorInfo.Name = "grpColorInfo";
            this.grpColorInfo.Padding = new System.Windows.Forms.Padding(2);
            this.grpColorInfo.Size = new System.Drawing.Size(400, 183);
            this.grpColorInfo.TabIndex = 0;
            this.grpColorInfo.TabStop = false;
            this.grpColorInfo.Text = "Color Information";
            // 
            // tblltColorInfo
            // 
            this.tblltColorInfo.ColumnCount = 3;
            this.tblltColorInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tblltColorInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63F));
            this.tblltColorInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltColorInfo.Controls.Add(this.label11, 2, 1);
            this.tblltColorInfo.Controls.Add(this.label1, 0, 0);
            this.tblltColorInfo.Controls.Add(this.label2, 0, 1);
            this.tblltColorInfo.Controls.Add(this.label3, 0, 2);
            this.tblltColorInfo.Controls.Add(this.txtcolorid, 1, 0);
            this.tblltColorInfo.Controls.Add(this.txtColorName, 1, 1);
            this.tblltColorInfo.Controls.Add(this.txtDescription, 1, 2);
            this.tblltColorInfo.Controls.Add(this.tblltButtonCat, 0, 5);
            this.tblltColorInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltColorInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblltColorInfo.Location = new System.Drawing.Point(2, 20);
            this.tblltColorInfo.Name = "tblltColorInfo";
            this.tblltColorInfo.RowCount = 6;
            this.tblltColorInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltColorInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltColorInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltColorInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltColorInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltColorInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltColorInfo.Size = new System.Drawing.Size(396, 161);
            this.tblltColorInfo.TabIndex = 0;
            // 
            // tblltButtonCat
            // 
            this.tblltButtonCat.ColumnCount = 6;
            this.tblltColorInfo.SetColumnSpan(this.tblltButtonCat, 3);
            this.tblltButtonCat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltButtonCat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtonCat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtonCat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtonCat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtonCat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltButtonCat.Controls.Add(this.btnnew, 1, 0);
            this.tblltButtonCat.Controls.Add(this.btnAdd, 2, 0);
            this.tblltButtonCat.Controls.Add(this.btnDelete, 3, 0);
            this.tblltButtonCat.Controls.Add(this.btnClose, 4, 0);
            this.tblltButtonCat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtonCat.Location = new System.Drawing.Point(0, 128);
            this.tblltButtonCat.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtonCat.Name = "tblltButtonCat";
            this.tblltButtonCat.RowCount = 1;
            this.tblltButtonCat.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtonCat.Size = new System.Drawing.Size(396, 33);
            this.tblltButtonCat.TabIndex = 4;
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(57, 1);
            this.btnnew.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(67, 31);
            this.btnnew.TabIndex = 5;
            this.btnnew.Text = "New";
            this.btnnew.UseVisualStyleBackColor = false;
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(128, 1);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(67, 31);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(199, 1);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(67, 31);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(270, 1);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(67, 31);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grpboxAllInfo
            // 
            this.grpboxAllInfo.Controls.Add(this.tblltAllInfo);
            this.grpboxAllInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpboxAllInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpboxAllInfo.Location = new System.Drawing.Point(2, 222);
            this.grpboxAllInfo.Margin = new System.Windows.Forms.Padding(2);
            this.grpboxAllInfo.Name = "grpboxAllInfo";
            this.grpboxAllInfo.Padding = new System.Windows.Forms.Padding(2);
            this.grpboxAllInfo.Size = new System.Drawing.Size(400, 258);
            this.grpboxAllInfo.TabIndex = 8;
            this.grpboxAllInfo.TabStop = false;
            this.grpboxAllInfo.Text = "All Color Information";
            // 
            // tblltAllInfo
            // 
            this.tblltAllInfo.ColumnCount = 2;
            this.tblltAllInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblltAllInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tblltAllInfo.Controls.Add(this.dtgvList, 0, 1);
            this.tblltAllInfo.Controls.Add(this.lblTotaluUnits, 1, 0);
            this.tblltAllInfo.Controls.Add(this.label15, 0, 0);
            this.tblltAllInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltAllInfo.Location = new System.Drawing.Point(2, 20);
            this.tblltAllInfo.Name = "tblltAllInfo";
            this.tblltAllInfo.RowCount = 2;
            this.tblltAllInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltAllInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tblltAllInfo.Size = new System.Drawing.Size(396, 236);
            this.tblltAllInfo.TabIndex = 0;
            // 
            // dtgvList
            // 
            this.dtgvList.AllowUserToAddRows = false;
            this.dtgvList.AllowUserToDeleteRows = false;
            this.dtgvList.BackgroundColor = System.Drawing.Color.White;
            this.dtgvList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.ClrID,
            this.ColorName,
            this.Description});
            this.tblltAllInfo.SetColumnSpan(this.dtgvList, 2);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgvList.Location = new System.Drawing.Point(2, 25);
            this.dtgvList.Margin = new System.Windows.Forms.Padding(2);
            this.dtgvList.Name = "dtgvList";
            this.dtgvList.ReadOnly = true;
            this.dtgvList.RowHeadersWidth = 10;
            this.dtgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvList.Size = new System.Drawing.Size(392, 209);
            this.dtgvList.TabIndex = 9;
            this.dtgvList.TabStop = false;
            this.dtgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvList_CellClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // ClrID
            // 
            this.ClrID.HeaderText = "Color ID";
            this.ClrID.Name = "ClrID";
            this.ClrID.ReadOnly = true;
            // 
            // ColorName
            // 
            this.ColorName.HeaderText = "Color Name";
            this.ColorName.Name = "ColorName";
            this.ColorName.ReadOnly = true;
            this.ColorName.Width = 200;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Visible = false;
            // 
            // frmcolor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(404, 482);
            this.Controls.Add(this.tbll);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmcolor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Color Information";
            this.tbll.ResumeLayout(false);
            this.tbll.PerformLayout();
            this.grpColorInfo.ResumeLayout(false);
            this.tblltColorInfo.ResumeLayout(false);
            this.tblltColorInfo.PerformLayout();
            this.tblltButtonCat.ResumeLayout(false);
            this.grpboxAllInfo.ResumeLayout(false);
            this.tblltAllInfo.ResumeLayout(false);
            this.tblltAllInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtColorName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtcolorid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotaluUnits;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tbll;
        private System.Windows.Forms.GroupBox grpColorInfo;
        private System.Windows.Forms.TableLayoutPanel tblltColorInfo;
        private System.Windows.Forms.TableLayoutPanel tblltButtonCat;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox grpboxAllInfo;
        private System.Windows.Forms.TableLayoutPanel tblltAllInfo;
        private System.Windows.Forms.DataGridView dtgvList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClrID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}