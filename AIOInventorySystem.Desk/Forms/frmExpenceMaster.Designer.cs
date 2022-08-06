namespace AIOInventorySystem.Desk.Forms
{
    partial class frmExpenceMaster
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
            this.tblltExpInfo = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtexpenceid = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtexpencename = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbACGroups = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtgvList = new System.Windows.Forms.DataGridView();
            this.ACGId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACGName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpenceId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpenceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ListCompId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblltExpInfo.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // tblltExpInfo
            // 
            this.tblltExpInfo.ColumnCount = 3;
            this.tblltExpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltExpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this.tblltExpInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltExpInfo.Controls.Add(this.tblltButtons, 0, 4);
            this.tblltExpInfo.Controls.Add(this.label6, 2, 1);
            this.tblltExpInfo.Controls.Add(this.label4, 0, 1);
            this.tblltExpInfo.Controls.Add(this.label1, 0, 0);
            this.tblltExpInfo.Controls.Add(this.txtexpenceid, 1, 0);
            this.tblltExpInfo.Controls.Add(this.txtDescription, 1, 3);
            this.tblltExpInfo.Controls.Add(this.label3, 0, 3);
            this.tblltExpInfo.Controls.Add(this.label2, 0, 2);
            this.tblltExpInfo.Controls.Add(this.txtexpencename, 1, 2);
            this.tblltExpInfo.Controls.Add(this.label11, 2, 2);
            this.tblltExpInfo.Controls.Add(this.cmbACGroups, 1, 1);
            this.tblltExpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltExpInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblltExpInfo.Location = new System.Drawing.Point(3, 21);
            this.tblltExpInfo.Name = "tblltExpInfo";
            this.tblltExpInfo.RowCount = 5;
            this.tblltExpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltExpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltExpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltExpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.tblltExpInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltExpInfo.Size = new System.Drawing.Size(422, 176);
            this.tblltExpInfo.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 6;
            this.tblltExpInfo.SetColumnSpan(this.tblltButtons, 3);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.Controls.Add(this.btnClose, 4, 0);
            this.tblltButtons.Controls.Add(this.btnNew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnDelete, 3, 0);
            this.tblltButtons.Controls.Add(this.btnAdd, 2, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 140);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(422, 36);
            this.tblltButtons.TabIndex = 5;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(282, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 32);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(72, 2);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(66, 32);
            this.btnNew.TabIndex = 6;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(212, 2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(66, 32);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(142, 2);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(66, 32);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(396, 28);
            this.label6.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 28);
            this.label6.TabIndex = 23;
            this.label6.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(2, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 24);
            this.label4.TabIndex = 22;
            this.label4.Text = "Account Group:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "Expense ID:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtexpenceid
            // 
            this.txtexpenceid.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtexpenceid.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtexpenceid.ForeColor = System.Drawing.Color.Black;
            this.txtexpenceid.Location = new System.Drawing.Point(128, 2);
            this.txtexpenceid.Margin = new System.Windows.Forms.Padding(2);
            this.txtexpenceid.Name = "txtexpenceid";
            this.txtexpenceid.ReadOnly = true;
            this.txtexpenceid.Size = new System.Drawing.Size(100, 25);
            this.txtexpenceid.TabIndex = 1;
            this.txtexpenceid.TabStop = false;
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(128, 86);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(2);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDescription.Size = new System.Drawing.Size(266, 52);
            this.txtDescription.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(2, 86);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 52);
            this.label3.TabIndex = 10;
            this.label3.Text = "Description:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(2, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 24);
            this.label2.TabIndex = 9;
            this.label2.Text = "Expense Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtexpencename
            // 
            this.txtexpencename.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtexpencename.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtexpencename.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtexpencename.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtexpencename.ForeColor = System.Drawing.Color.Black;
            this.txtexpencename.Location = new System.Drawing.Point(128, 58);
            this.txtexpencename.Margin = new System.Windows.Forms.Padding(2);
            this.txtexpencename.Name = "txtexpencename";
            this.txtexpencename.Size = new System.Drawing.Size(266, 25);
            this.txtexpencename.TabIndex = 3;
            this.txtexpencename.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtexpencename_KeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label11.Location = new System.Drawing.Point(396, 56);
            this.label11.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 28);
            this.label11.TabIndex = 20;
            this.label11.Text = "*";
            // 
            // cmbACGroups
            // 
            this.cmbACGroups.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbACGroups.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbACGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbACGroups.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbACGroups.FormattingEnabled = true;
            this.cmbACGroups.Location = new System.Drawing.Point(128, 30);
            this.cmbACGroups.Margin = new System.Windows.Forms.Padding(2);
            this.cmbACGroups.Name = "cmbACGroups";
            this.cmbACGroups.Size = new System.Drawing.Size(266, 25);
            this.cmbACGroups.TabIndex = 2;
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
            this.label5.Size = new System.Drawing.Size(434, 37);
            this.label5.TabIndex = 11;
            this.label5.Text = "Expense Information";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.groupBox1, 0, 1);
            this.tblltMain.Controls.Add(this.groupBox2, 0, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41F));
            this.tblltMain.Size = new System.Drawing.Size(434, 412);
            this.tblltMain.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tblltExpInfo);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(428, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Expence Information";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtgvList);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(3, 246);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(428, 163);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Expence List";
            // 
            // dtgvList
            // 
            this.dtgvList.AllowUserToAddRows = false;
            this.dtgvList.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ACGId,
            this.ACGName,
            this.Id,
            this.ExpenceId,
            this.ExpenceName,
            this.Description,
            this.ListCompId});
            this.dtgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgvList.Location = new System.Drawing.Point(3, 21);
            this.dtgvList.Margin = new System.Windows.Forms.Padding(2);
            this.dtgvList.Name = "dtgvList";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvList.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtgvList.RowHeadersWidth = 15;
            this.dtgvList.Size = new System.Drawing.Size(422, 139);
            this.dtgvList.TabIndex = 9;
            this.dtgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvList_CellClick);
            // 
            // ACGId
            // 
            this.ACGId.HeaderText = "ACGId";
            this.ACGId.Name = "ACGId";
            this.ACGId.Visible = false;
            // 
            // ACGName
            // 
            this.ACGName.HeaderText = "ACGroupName";
            this.ACGName.Name = "ACGName";
            this.ACGName.Width = 150;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // ExpenceId
            // 
            this.ExpenceId.HeaderText = "ExpenceId";
            this.ExpenceId.Name = "ExpenceId";
            this.ExpenceId.Visible = false;
            // 
            // ExpenceName
            // 
            this.ExpenceName.HeaderText = "Expence Name";
            this.ExpenceName.MinimumWidth = 150;
            this.ExpenceName.Name = "ExpenceName";
            this.ExpenceName.Width = 150;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            // 
            // ListCompId
            // 
            this.ListCompId.HeaderText = "CompId";
            this.ListCompId.Name = "ListCompId";
            this.ListCompId.Visible = false;
            // 
            // frmExpenceMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(434, 412);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "frmExpenceMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Expense Master";
            this.Load += new System.EventHandler(this.frmExpenceMaster_Load);
            this.tblltExpInfo.ResumeLayout(false);
            this.tblltExpInfo.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.tblltMain.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtexpencename;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtexpenceid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tblltExpInfo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbACGroups;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dtgvList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACGId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACGName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpenceId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpenceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn ListCompId;
    }
}