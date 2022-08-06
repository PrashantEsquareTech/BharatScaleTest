namespace AIOInventorySystem.Desk.Forms
{
    partial class frmIncomeMaster
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
            this.tblltInfo = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbACGroups = new System.Windows.Forms.ComboBox();
            this.txtexpenceid = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtexpencename = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.grpIncomeInfo = new System.Windows.Forms.GroupBox();
            this.grpIncomeList = new System.Windows.Forms.GroupBox();
            this.GVList = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACGId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ACGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IncomeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IncomeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompIdgrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblltInfo.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.grpIncomeInfo.SuspendLayout();
            this.grpIncomeList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVList)).BeginInit();
            this.SuspendLayout();
            // 
            // tblltInfo
            // 
            this.tblltInfo.ColumnCount = 3;
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64F));
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltInfo.Controls.Add(this.tblltButtons, 0, 5);
            this.tblltInfo.Controls.Add(this.label6, 2, 2);
            this.tblltInfo.Controls.Add(this.label4, 0, 1);
            this.tblltInfo.Controls.Add(this.label1, 0, 0);
            this.tblltInfo.Controls.Add(this.cmbACGroups, 1, 1);
            this.tblltInfo.Controls.Add(this.txtexpenceid, 1, 0);
            this.tblltInfo.Controls.Add(this.label11, 2, 1);
            this.tblltInfo.Controls.Add(this.txtDescription, 1, 3);
            this.tblltInfo.Controls.Add(this.label3, 0, 3);
            this.tblltInfo.Controls.Add(this.label2, 0, 2);
            this.tblltInfo.Controls.Add(this.txtexpencename, 1, 2);
            this.tblltInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblltInfo.Location = new System.Drawing.Point(3, 22);
            this.tblltInfo.Name = "tblltInfo";
            this.tblltInfo.RowCount = 6;
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltInfo.Size = new System.Drawing.Size(422, 172);
            this.tblltInfo.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 6;
            this.tblltInfo.SetColumnSpan(this.tblltButtons, 3);
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
            this.tblltButtons.Location = new System.Drawing.Point(0, 135);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(422, 37);
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
            this.btnClose.Size = new System.Drawing.Size(66, 33);
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
            this.btnNew.Size = new System.Drawing.Size(66, 33);
            this.btnNew.TabIndex = 6;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
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
            this.btnDelete.Size = new System.Drawing.Size(66, 33);
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
            this.btnAdd.Size = new System.Drawing.Size(66, 33);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label6.Location = new System.Drawing.Point(396, 54);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 27);
            this.label6.TabIndex = 21;
            this.label6.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(2, 29);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 23);
            this.label4.TabIndex = 24;
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
            this.label1.Size = new System.Drawing.Size(122, 23);
            this.label1.TabIndex = 8;
            this.label1.Text = "Income ID:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbACGroups
            // 
            this.cmbACGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbACGroups.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbACGroups.FormattingEnabled = true;
            this.cmbACGroups.Location = new System.Drawing.Point(128, 29);
            this.cmbACGroups.Margin = new System.Windows.Forms.Padding(2);
            this.cmbACGroups.Name = "cmbACGroups";
            this.cmbACGroups.Size = new System.Drawing.Size(266, 25);
            this.cmbACGroups.TabIndex = 2;
            this.cmbACGroups.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbACGroups_KeyDown);
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
            this.txtexpenceid.Size = new System.Drawing.Size(120, 25);
            this.txtexpenceid.TabIndex = 1;
            this.txtexpenceid.TabStop = false;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label11.Location = new System.Drawing.Point(396, 27);
            this.label11.Margin = new System.Windows.Forms.Padding(0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(23, 27);
            this.label11.TabIndex = 20;
            this.label11.Text = "*";
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(128, 83);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(2);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.tblltInfo.SetRowSpan(this.txtDescription, 2);
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDescription.Size = new System.Drawing.Size(266, 50);
            this.txtDescription.TabIndex = 4;
            this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescription_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(2, 83);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 23);
            this.label3.TabIndex = 10;
            this.label3.Text = "Description:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(2, 56);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 23);
            this.label2.TabIndex = 9;
            this.label2.Text = "Income Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtexpencename
            // 
            this.txtexpencename.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtexpencename.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtexpencename.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtexpencename.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtexpencename.ForeColor = System.Drawing.Color.Black;
            this.txtexpencename.Location = new System.Drawing.Point(128, 56);
            this.txtexpencename.Margin = new System.Windows.Forms.Padding(2);
            this.txtexpencename.Name = "txtexpencename";
            this.txtexpencename.Size = new System.Drawing.Size(266, 25);
            this.txtexpencename.TabIndex = 3;
            this.txtexpencename.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtexpencename_KeyDown);
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
            this.label5.Size = new System.Drawing.Size(434, 39);
            this.label5.TabIndex = 11;
            this.label5.Text = "Income Information";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.grpIncomeInfo, 0, 1);
            this.tblltMain.Controls.Add(this.grpIncomeList, 0, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7.843137F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41.17647F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.98039F));
            this.tblltMain.Size = new System.Drawing.Size(434, 502);
            this.tblltMain.TabIndex = 0;
            // 
            // grpIncomeInfo
            // 
            this.grpIncomeInfo.Controls.Add(this.tblltInfo);
            this.grpIncomeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpIncomeInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpIncomeInfo.Location = new System.Drawing.Point(3, 43);
            this.grpIncomeInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpIncomeInfo.Name = "grpIncomeInfo";
            this.grpIncomeInfo.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpIncomeInfo.Size = new System.Drawing.Size(428, 198);
            this.grpIncomeInfo.TabIndex = 0;
            this.grpIncomeInfo.TabStop = false;
            this.grpIncomeInfo.Text = "Income Info";
            // 
            // grpIncomeList
            // 
            this.grpIncomeList.Controls.Add(this.GVList);
            this.grpIncomeList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpIncomeList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpIncomeList.Location = new System.Drawing.Point(3, 249);
            this.grpIncomeList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpIncomeList.Name = "grpIncomeList";
            this.grpIncomeList.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpIncomeList.Size = new System.Drawing.Size(428, 249);
            this.grpIncomeList.TabIndex = 9;
            this.grpIncomeList.TabStop = false;
            this.grpIncomeList.Text = "Income List";
            // 
            // GVList
            // 
            this.GVList.AllowUserToAddRows = false;
            this.GVList.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GVList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.ACGId,
            this.ACGroup,
            this.IncomeId,
            this.IncomeName,
            this.Description,
            this.CompIdgrid});
            this.GVList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GVList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GVList.Location = new System.Drawing.Point(3, 22);
            this.GVList.Margin = new System.Windows.Forms.Padding(2);
            this.GVList.Name = "GVList";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVList.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GVList.RowHeadersWidth = 15;
            this.GVList.Size = new System.Drawing.Size(422, 223);
            this.GVList.TabIndex = 9;
            this.GVList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GVList_CellClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // ACGId
            // 
            this.ACGId.HeaderText = "ACGId";
            this.ACGId.Name = "ACGId";
            this.ACGId.Visible = false;
            this.ACGId.Width = 170;
            // 
            // ACGroup
            // 
            this.ACGroup.HeaderText = "ACGroup";
            this.ACGroup.Name = "ACGroup";
            // 
            // IncomeId
            // 
            this.IncomeId.HeaderText = "IncomeId";
            this.IncomeId.Name = "IncomeId";
            this.IncomeId.Visible = false;
            // 
            // IncomeName
            // 
            this.IncomeName.HeaderText = "Income Name";
            this.IncomeName.Name = "IncomeName";
            this.IncomeName.Width = 130;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.Width = 170;
            // 
            // CompIdgrid
            // 
            this.CompIdgrid.HeaderText = "CompIdgrid";
            this.CompIdgrid.Name = "CompIdgrid";
            this.CompIdgrid.Visible = false;
            // 
            // frmIncomeMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(434, 502);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1164, 657);
            this.Name = "frmIncomeMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Income Master";
            this.Load += new System.EventHandler(this.frmIncomeMaster_Load);
            this.tblltInfo.ResumeLayout(false);
            this.tblltInfo.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.tblltMain.ResumeLayout(false);
            this.grpIncomeInfo.ResumeLayout(false);
            this.grpIncomeList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GVList)).EndInit();
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
        private System.Windows.Forms.TableLayoutPanel tblltInfo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbACGroups;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.GroupBox grpIncomeInfo;
        private System.Windows.Forms.GroupBox grpIncomeList;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView GVList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACGId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ACGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn IncomeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn IncomeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompIdgrid;
    }
}