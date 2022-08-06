namespace AIOInventorySystem.Desk.Forms
{
    partial class frmCategory
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
            this.label5 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.grpSubCatList = new System.Windows.Forms.GroupBox();
            this.GvSubCatInfo = new System.Windows.Forms.DataGridView();
            this.grpCatInfo = new System.Windows.Forms.GroupBox();
            this.tblltCategoryInfo = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtcatcode = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCategoryName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.tblltButtonCat = new System.Windows.Forms.TableLayoutPanel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpSubCatInfo = new System.Windows.Forms.GroupBox();
            this.tblltSubCategoryInfo = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtsubcatcode = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtsubcatName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtsubDescription = new System.Windows.Forms.TextBox();
            this.tblltSubButton = new System.Windows.Forms.TableLayoutPanel();
            this.btnsnew = new System.Windows.Forms.Button();
            this.btnsclose = new System.Windows.Forms.Button();
            this.btnsdelete = new System.Windows.Forms.Button();
            this.btnsadd = new System.Windows.Forms.Button();
            this.grpCatList = new System.Windows.Forms.GroupBox();
            this.GvUnitInfo = new System.Windows.Forms.DataGridView();
            this.CategoryID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblltMain.SuspendLayout();
            this.grpSubCatList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvSubCatInfo)).BeginInit();
            this.grpCatInfo.SuspendLayout();
            this.tblltCategoryInfo.SuspendLayout();
            this.tblltButtonCat.SuspendLayout();
            this.grpSubCatInfo.SuspendLayout();
            this.tblltSubCategoryInfo.SuspendLayout();
            this.tblltSubButton.SuspendLayout();
            this.grpCatList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvUnitInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label5, 2);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(614, 36);
            this.label5.TabIndex = 11;
            this.label5.Text = "Category Information";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 2;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblltMain.Controls.Add(this.grpSubCatList, 1, 2);
            this.tblltMain.Controls.Add(this.grpCatInfo, 0, 1);
            this.tblltMain.Controls.Add(this.grpSubCatInfo, 0, 2);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.grpCatList, 1, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45.5F));
            this.tblltMain.Size = new System.Drawing.Size(614, 402);
            this.tblltMain.TabIndex = 0;
            // 
            // grpSubCatList
            // 
            this.grpSubCatList.Controls.Add(this.GvSubCatInfo);
            this.grpSubCatList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSubCatList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSubCatList.Location = new System.Drawing.Point(371, 221);
            this.grpSubCatList.Name = "grpSubCatList";
            this.grpSubCatList.Size = new System.Drawing.Size(240, 178);
            this.grpSubCatList.TabIndex = 22;
            this.grpSubCatList.TabStop = false;
            this.grpSubCatList.Text = "All Sub Category List";
            // 
            // GvSubCatInfo
            // 
            this.GvSubCatInfo.AllowUserToAddRows = false;
            this.GvSubCatInfo.AllowUserToDeleteRows = false;
            this.GvSubCatInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvSubCatInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvSubCatInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvSubCatInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvSubCatInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvSubCatInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvSubCatInfo.Location = new System.Drawing.Point(3, 21);
            this.GvSubCatInfo.Margin = new System.Windows.Forms.Padding(2);
            this.GvSubCatInfo.Name = "GvSubCatInfo";
            this.GvSubCatInfo.ReadOnly = true;
            this.GvSubCatInfo.RowHeadersWidth = 10;
            this.GvSubCatInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvSubCatInfo.Size = new System.Drawing.Size(234, 154);
            this.GvSubCatInfo.TabIndex = 22;
            this.GvSubCatInfo.TabStop = false;
            this.GvSubCatInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvSubCatInfo_CellClick);
            // 
            // grpCatInfo
            // 
            this.grpCatInfo.Controls.Add(this.tblltCategoryInfo);
            this.grpCatInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCatInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCatInfo.Location = new System.Drawing.Point(3, 39);
            this.grpCatInfo.Name = "grpCatInfo";
            this.grpCatInfo.Size = new System.Drawing.Size(362, 176);
            this.grpCatInfo.TabIndex = 0;
            this.grpCatInfo.TabStop = false;
            this.grpCatInfo.Text = "Category Information";
            // 
            // tblltCategoryInfo
            // 
            this.tblltCategoryInfo.ColumnCount = 3;
            this.tblltCategoryInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tblltCategoryInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tblltCategoryInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltCategoryInfo.Controls.Add(this.label2, 0, 0);
            this.tblltCategoryInfo.Controls.Add(this.label11, 2, 0);
            this.tblltCategoryInfo.Controls.Add(this.txtcatcode, 1, 1);
            this.tblltCategoryInfo.Controls.Add(this.label13, 0, 1);
            this.tblltCategoryInfo.Controls.Add(this.label3, 0, 2);
            this.tblltCategoryInfo.Controls.Add(this.txtCategoryName, 1, 0);
            this.tblltCategoryInfo.Controls.Add(this.txtDescription, 1, 2);
            this.tblltCategoryInfo.Controls.Add(this.tblltButtonCat, 0, 5);
            this.tblltCategoryInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltCategoryInfo.Location = new System.Drawing.Point(3, 21);
            this.tblltCategoryInfo.Name = "tblltCategoryInfo";
            this.tblltCategoryInfo.RowCount = 6;
            this.tblltCategoryInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltCategoryInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltCategoryInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltCategoryInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltCategoryInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltCategoryInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltCategoryInfo.Size = new System.Drawing.Size(356, 152);
            this.tblltCategoryInfo.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(2, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 23);
            this.label2.TabIndex = 9;
            this.label2.Text = "Category Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label11.Location = new System.Drawing.Point(333, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 27);
            this.label11.TabIndex = 20;
            this.label11.Text = "*";
            // 
            // txtcatcode
            // 
            this.txtcatcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtcatcode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcatcode.ForeColor = System.Drawing.Color.Black;
            this.txtcatcode.Location = new System.Drawing.Point(119, 29);
            this.txtcatcode.Margin = new System.Windows.Forms.Padding(2);
            this.txtcatcode.Name = "txtcatcode";
            this.txtcatcode.Size = new System.Drawing.Size(209, 25);
            this.txtcatcode.TabIndex = 2;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(2, 29);
            this.label13.Margin = new System.Windows.Forms.Padding(2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(113, 23);
            this.label13.TabIndex = 22;
            this.label13.Text = "Category Code:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(2, 56);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 23);
            this.label3.TabIndex = 10;
            this.label3.Text = "Description:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCategoryName
            // 
            this.txtCategoryName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCategoryName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCategoryName.ForeColor = System.Drawing.Color.Black;
            this.txtCategoryName.Location = new System.Drawing.Point(119, 2);
            this.txtCategoryName.Margin = new System.Windows.Forms.Padding(2);
            this.txtCategoryName.Name = "txtCategoryName";
            this.txtCategoryName.Size = new System.Drawing.Size(209, 25);
            this.txtCategoryName.TabIndex = 1;
            this.txtCategoryName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCategoryName_KeyDown);
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(119, 56);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(2);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.tblltCategoryInfo.SetRowSpan(this.txtDescription, 2);
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDescription.Size = new System.Drawing.Size(209, 50);
            this.txtDescription.TabIndex = 3;
            // 
            // tblltButtonCat
            // 
            this.tblltButtonCat.ColumnCount = 6;
            this.tblltCategoryInfo.SetColumnSpan(this.tblltButtonCat, 3);
            this.tblltButtonCat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltButtonCat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtonCat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtonCat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtonCat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtonCat.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltButtonCat.Controls.Add(this.btnNew, 1, 0);
            this.tblltButtonCat.Controls.Add(this.btnAdd, 2, 0);
            this.tblltButtonCat.Controls.Add(this.btnDelete, 3, 0);
            this.tblltButtonCat.Controls.Add(this.btnClose, 4, 0);
            this.tblltButtonCat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtonCat.Location = new System.Drawing.Point(0, 123);
            this.tblltButtonCat.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtonCat.Name = "tblltButtonCat";
            this.tblltButtonCat.RowCount = 1;
            this.tblltButtonCat.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtonCat.Size = new System.Drawing.Size(356, 29);
            this.tblltButtonCat.TabIndex = 4;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(51, 1);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(60, 27);
            this.btnNew.TabIndex = 5;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(115, 1);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(60, 27);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click_1);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(179, 1);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(60, 27);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click_1);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(243, 1);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(60, 27);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grpSubCatInfo
            // 
            this.grpSubCatInfo.Controls.Add(this.tblltSubCategoryInfo);
            this.grpSubCatInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSubCatInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSubCatInfo.Location = new System.Drawing.Point(3, 221);
            this.grpSubCatInfo.Name = "grpSubCatInfo";
            this.grpSubCatInfo.Size = new System.Drawing.Size(362, 178);
            this.grpSubCatInfo.TabIndex = 8;
            this.grpSubCatInfo.TabStop = false;
            this.grpSubCatInfo.Text = "SubCategory Information";
            // 
            // tblltSubCategoryInfo
            // 
            this.tblltSubCategoryInfo.ColumnCount = 3;
            this.tblltSubCategoryInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38F));
            this.tblltSubCategoryInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tblltSubCategoryInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltSubCategoryInfo.Controls.Add(this.label10, 0, 0);
            this.tblltSubCategoryInfo.Controls.Add(this.label4, 2, 0);
            this.tblltSubCategoryInfo.Controls.Add(this.txtsubcatcode, 1, 1);
            this.tblltSubCategoryInfo.Controls.Add(this.label15, 0, 1);
            this.tblltSubCategoryInfo.Controls.Add(this.txtsubcatName, 1, 0);
            this.tblltSubCategoryInfo.Controls.Add(this.label6, 0, 2);
            this.tblltSubCategoryInfo.Controls.Add(this.txtsubDescription, 1, 2);
            this.tblltSubCategoryInfo.Controls.Add(this.tblltSubButton, 0, 5);
            this.tblltSubCategoryInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltSubCategoryInfo.Location = new System.Drawing.Point(3, 21);
            this.tblltSubCategoryInfo.Margin = new System.Windows.Forms.Padding(0);
            this.tblltSubCategoryInfo.Name = "tblltSubCategoryInfo";
            this.tblltSubCategoryInfo.RowCount = 6;
            this.tblltSubCategoryInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltSubCategoryInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltSubCategoryInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltSubCategoryInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltSubCategoryInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltSubCategoryInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltSubCategoryInfo.Size = new System.Drawing.Size(356, 154);
            this.tblltSubCategoryInfo.TabIndex = 8;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(2, 2);
            this.label10.Margin = new System.Windows.Forms.Padding(2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(131, 23);
            this.label10.TabIndex = 23;
            this.label10.Text = "SubCategory Name:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(333, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 27);
            this.label4.TabIndex = 20;
            this.label4.Text = "*";
            // 
            // txtsubcatcode
            // 
            this.txtsubcatcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtsubcatcode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsubcatcode.ForeColor = System.Drawing.Color.Black;
            this.txtsubcatcode.Location = new System.Drawing.Point(137, 29);
            this.txtsubcatcode.Margin = new System.Windows.Forms.Padding(2);
            this.txtsubcatcode.Name = "txtsubcatcode";
            this.txtsubcatcode.Size = new System.Drawing.Size(191, 25);
            this.txtsubcatcode.TabIndex = 12;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(2, 29);
            this.label15.Margin = new System.Windows.Forms.Padding(2);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(131, 23);
            this.label15.TabIndex = 26;
            this.label15.Text = "SubCategory Code:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtsubcatName
            // 
            this.txtsubcatName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtsubcatName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsubcatName.ForeColor = System.Drawing.Color.Black;
            this.txtsubcatName.Location = new System.Drawing.Point(137, 2);
            this.txtsubcatName.Margin = new System.Windows.Forms.Padding(2);
            this.txtsubcatName.Name = "txtsubcatName";
            this.txtsubcatName.Size = new System.Drawing.Size(191, 25);
            this.txtsubcatName.TabIndex = 11;
            this.txtsubcatName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtsubcatName_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(2, 56);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 23);
            this.label6.TabIndex = 10;
            this.label6.Text = "Description:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtsubDescription
            // 
            this.txtsubDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtsubDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsubDescription.ForeColor = System.Drawing.Color.Black;
            this.txtsubDescription.Location = new System.Drawing.Point(137, 56);
            this.txtsubDescription.Margin = new System.Windows.Forms.Padding(2);
            this.txtsubDescription.Multiline = true;
            this.txtsubDescription.Name = "txtsubDescription";
            this.tblltSubCategoryInfo.SetRowSpan(this.txtsubDescription, 2);
            this.txtsubDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtsubDescription.Size = new System.Drawing.Size(191, 50);
            this.txtsubDescription.TabIndex = 13;
            // 
            // tblltSubButton
            // 
            this.tblltSubButton.ColumnCount = 6;
            this.tblltSubCategoryInfo.SetColumnSpan(this.tblltSubButton, 3);
            this.tblltSubButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltSubButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltSubButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltSubButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltSubButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltSubButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltSubButton.Controls.Add(this.btnsnew, 1, 0);
            this.tblltSubButton.Controls.Add(this.btnsclose, 4, 0);
            this.tblltSubButton.Controls.Add(this.btnsdelete, 3, 0);
            this.tblltSubButton.Controls.Add(this.btnsadd, 2, 0);
            this.tblltSubButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltSubButton.Location = new System.Drawing.Point(0, 123);
            this.tblltSubButton.Margin = new System.Windows.Forms.Padding(0);
            this.tblltSubButton.Name = "tblltSubButton";
            this.tblltSubButton.RowCount = 1;
            this.tblltSubButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltSubButton.Size = new System.Drawing.Size(356, 31);
            this.tblltSubButton.TabIndex = 14;
            // 
            // btnsnew
            // 
            this.btnsnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsnew.ForeColor = System.Drawing.Color.White;
            this.btnsnew.Location = new System.Drawing.Point(51, 1);
            this.btnsnew.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnsnew.Name = "btnsnew";
            this.btnsnew.Size = new System.Drawing.Size(60, 29);
            this.btnsnew.TabIndex = 15;
            this.btnsnew.Text = "New";
            this.btnsnew.UseVisualStyleBackColor = false;
            this.btnsnew.Click += new System.EventHandler(this.btnsnew_Click);
            // 
            // btnsclose
            // 
            this.btnsclose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsclose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsclose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsclose.ForeColor = System.Drawing.Color.White;
            this.btnsclose.Location = new System.Drawing.Point(243, 1);
            this.btnsclose.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnsclose.Name = "btnsclose";
            this.btnsclose.Size = new System.Drawing.Size(60, 29);
            this.btnsclose.TabIndex = 17;
            this.btnsclose.Text = "Close";
            this.btnsclose.UseVisualStyleBackColor = false;
            this.btnsclose.Click += new System.EventHandler(this.btnsclose_Click);
            // 
            // btnsdelete
            // 
            this.btnsdelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsdelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsdelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsdelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsdelete.ForeColor = System.Drawing.Color.White;
            this.btnsdelete.Location = new System.Drawing.Point(179, 1);
            this.btnsdelete.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnsdelete.Name = "btnsdelete";
            this.btnsdelete.Size = new System.Drawing.Size(60, 29);
            this.btnsdelete.TabIndex = 16;
            this.btnsdelete.Text = "Delete";
            this.btnsdelete.UseVisualStyleBackColor = false;
            this.btnsdelete.Click += new System.EventHandler(this.btnsdelete_Click);
            // 
            // btnsadd
            // 
            this.btnsadd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsadd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsadd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsadd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsadd.ForeColor = System.Drawing.Color.White;
            this.btnsadd.Location = new System.Drawing.Point(115, 1);
            this.btnsadd.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnsadd.Name = "btnsadd";
            this.btnsadd.Size = new System.Drawing.Size(60, 29);
            this.btnsadd.TabIndex = 14;
            this.btnsadd.Text = "Add";
            this.btnsadd.UseVisualStyleBackColor = false;
            this.btnsadd.Click += new System.EventHandler(this.btnsadd_Click);
            // 
            // grpCatList
            // 
            this.grpCatList.Controls.Add(this.GvUnitInfo);
            this.grpCatList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCatList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCatList.Location = new System.Drawing.Point(371, 39);
            this.grpCatList.Name = "grpCatList";
            this.grpCatList.Size = new System.Drawing.Size(240, 176);
            this.grpCatList.TabIndex = 20;
            this.grpCatList.TabStop = false;
            this.grpCatList.Text = "All Category List";
            // 
            // GvUnitInfo
            // 
            this.GvUnitInfo.AllowUserToAddRows = false;
            this.GvUnitInfo.AllowUserToDeleteRows = false;
            this.GvUnitInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvUnitInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvUnitInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GvUnitInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvUnitInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CategoryID,
            this.CategoryName,
            this.Code,
            this.Description});
            this.GvUnitInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvUnitInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvUnitInfo.Location = new System.Drawing.Point(3, 21);
            this.GvUnitInfo.Margin = new System.Windows.Forms.Padding(2);
            this.GvUnitInfo.Name = "GvUnitInfo";
            this.GvUnitInfo.ReadOnly = true;
            this.GvUnitInfo.RowHeadersWidth = 10;
            this.GvUnitInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvUnitInfo.Size = new System.Drawing.Size(234, 152);
            this.GvUnitInfo.TabIndex = 20;
            this.GvUnitInfo.TabStop = false;
            this.GvUnitInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvUnitInfo_CellClick);
            // 
            // CategoryID
            // 
            this.CategoryID.HeaderText = "CategoryID";
            this.CategoryID.Name = "CategoryID";
            this.CategoryID.ReadOnly = true;
            this.CategoryID.Visible = false;
            this.CategoryID.Width = 200;
            // 
            // CategoryName
            // 
            this.CategoryName.HeaderText = "Category Name";
            this.CategoryName.Name = "CategoryName";
            this.CategoryName.ReadOnly = true;
            this.CategoryName.Width = 130;
            // 
            // Code
            // 
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Visible = false;
            // 
            // frmCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(614, 402);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmCategory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Category Information";
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.grpSubCatList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GvSubCatInfo)).EndInit();
            this.grpCatInfo.ResumeLayout(false);
            this.tblltCategoryInfo.ResumeLayout(false);
            this.tblltCategoryInfo.PerformLayout();
            this.tblltButtonCat.ResumeLayout(false);
            this.grpSubCatInfo.ResumeLayout(false);
            this.tblltSubCategoryInfo.ResumeLayout(false);
            this.tblltSubCategoryInfo.PerformLayout();
            this.tblltSubButton.ResumeLayout(false);
            this.grpCatList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GvUnitInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtsubDescription;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtsubcatName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtcatcode;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtsubcatcode;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltCategoryInfo;
        private System.Windows.Forms.TableLayoutPanel tblltButtonCat;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnsnew;
        private System.Windows.Forms.Button btnsadd;
        private System.Windows.Forms.Button btnsdelete;
        private System.Windows.Forms.Button btnsclose;
        private System.Windows.Forms.TableLayoutPanel tblltSubCategoryInfo;
        private System.Windows.Forms.TableLayoutPanel tblltSubButton;
        private System.Windows.Forms.DataGridView GvUnitInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridView GvSubCatInfo;
        private System.Windows.Forms.GroupBox grpSubCatList;
        private System.Windows.Forms.GroupBox grpCatInfo;
        private System.Windows.Forms.GroupBox grpSubCatInfo;
        private System.Windows.Forms.GroupBox grpCatList;
    }
}