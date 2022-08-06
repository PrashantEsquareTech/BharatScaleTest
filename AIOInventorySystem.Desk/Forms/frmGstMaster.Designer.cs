namespace AIOInventorySystem.Desk.Forms
{
    partial class frmGstMaster
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotaluUnits = new System.Windows.Forms.Label();
            this.tblltList = new System.Windows.Forms.TableLayoutPanel();
            this.GVList = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GSTIDgrid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GSTPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApplyCSGST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApplyIGST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.sgrpBox2 = new System.Windows.Forms.GroupBox();
            this.txtGStID = new System.Windows.Forms.TextBox();
            this.sgrpBox1 = new System.Windows.Forms.GroupBox();
            this.tblltInfo = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtgstpercent = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.tblltGST = new System.Windows.Forms.TableLayoutPanel();
            this.rbtigst = new System.Windows.Forms.RadioButton();
            this.rbtcnsgst = new System.Windows.Forms.RadioButton();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblltList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVList)).BeginInit();
            this.sgrpBox2.SuspendLayout();
            this.sgrpBox1.SuspendLayout();
            this.tblltInfo.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.tblltGST.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
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
            this.label1.Size = new System.Drawing.Size(112, 24);
            this.label1.TabIndex = 15;
            this.label1.Text = "GST ID:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.label2.Size = new System.Drawing.Size(112, 24);
            this.label2.TabIndex = 17;
            this.label2.Text = "GST Persent:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.label5.Size = new System.Drawing.Size(384, 37);
            this.label5.TabIndex = 11;
            this.label5.Text = "GST Information";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotaluUnits
            // 
            this.lblTotaluUnits.AutoSize = true;
            this.lblTotaluUnits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotaluUnits.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotaluUnits.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotaluUnits.Location = new System.Drawing.Point(121, 2);
            this.lblTotaluUnits.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotaluUnits.Name = "lblTotaluUnits";
            this.lblTotaluUnits.Size = new System.Drawing.Size(251, 27);
            this.lblTotaluUnits.TabIndex = 48;
            this.lblTotaluUnits.Text = "0";
            this.lblTotaluUnits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tblltList
            // 
            this.tblltList.ColumnCount = 2;
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68F));
            this.tblltList.Controls.Add(this.lblTotaluUnits, 1, 0);
            this.tblltList.Controls.Add(this.GVList, 0, 1);
            this.tblltList.Controls.Add(this.label4, 0, 0);
            this.tblltList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltList.Location = new System.Drawing.Point(2, 22);
            this.tblltList.Name = "tblltList";
            this.tblltList.RowCount = 2;
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88F));
            this.tblltList.Size = new System.Drawing.Size(374, 261);
            this.tblltList.TabIndex = 7;
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
            this.GSTIDgrid,
            this.GSTPercent,
            this.ApplyCSGST,
            this.ApplyIGST});
            this.tblltList.SetColumnSpan(this.GVList, 2);
            this.GVList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GVList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GVList.Location = new System.Drawing.Point(2, 33);
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
            this.GVList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GVList.Size = new System.Drawing.Size(370, 226);
            this.GVList.TabIndex = 7;
            this.GVList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GVList_CellClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // GSTIDgrid
            // 
            this.GSTIDgrid.HeaderText = "GSTID";
            this.GSTIDgrid.Name = "GSTIDgrid";
            this.GSTIDgrid.Width = 50;
            // 
            // GSTPercent
            // 
            this.GSTPercent.HeaderText = "GST Percent";
            this.GSTPercent.Name = "GSTPercent";
            this.GSTPercent.Width = 110;
            // 
            // ApplyCSGST
            // 
            this.ApplyCSGST.HeaderText = "Apply for C&SGST";
            this.ApplyCSGST.Name = "ApplyCSGST";
            this.ApplyCSGST.Width = 95;
            // 
            // ApplyIGST
            // 
            this.ApplyIGST.HeaderText = "Apply for IGST";
            this.ApplyIGST.Name = "ApplyIGST";
            this.ApplyIGST.Width = 95;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(2, 2);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 27);
            this.label4.TabIndex = 47;
            this.label4.Text = "Total Records:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // sgrpBox2
            // 
            this.sgrpBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.sgrpBox2.Controls.Add(this.tblltList);
            this.sgrpBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sgrpBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sgrpBox2.ForeColor = System.Drawing.Color.Black;
            this.sgrpBox2.Location = new System.Drawing.Point(3, 182);
            this.sgrpBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sgrpBox2.Name = "sgrpBox2";
            this.sgrpBox2.Padding = new System.Windows.Forms.Padding(2, 4, 2, 3);
            this.sgrpBox2.Size = new System.Drawing.Size(378, 286);
            this.sgrpBox2.TabIndex = 7;
            this.sgrpBox2.TabStop = false;
            this.sgrpBox2.Text = "All GST Percent Information";
            // 
            // txtGStID
            // 
            this.txtGStID.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtGStID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGStID.ForeColor = System.Drawing.Color.Black;
            this.txtGStID.Location = new System.Drawing.Point(118, 2);
            this.txtGStID.Margin = new System.Windows.Forms.Padding(2);
            this.txtGStID.Name = "txtGStID";
            this.txtGStID.ReadOnly = true;
            this.txtGStID.Size = new System.Drawing.Size(102, 25);
            this.txtGStID.TabIndex = 0;
            this.txtGStID.TabStop = false;
            // 
            // sgrpBox1
            // 
            this.sgrpBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.sgrpBox1.Controls.Add(this.tblltInfo);
            this.sgrpBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sgrpBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sgrpBox1.ForeColor = System.Drawing.Color.Black;
            this.sgrpBox1.Location = new System.Drawing.Point(0, 37);
            this.sgrpBox1.Margin = new System.Windows.Forms.Padding(0);
            this.sgrpBox1.Name = "sgrpBox1";
            this.sgrpBox1.Padding = new System.Windows.Forms.Padding(2, 7, 2, 3);
            this.sgrpBox1.Size = new System.Drawing.Size(384, 141);
            this.sgrpBox1.TabIndex = 0;
            this.sgrpBox1.TabStop = false;
            this.sgrpBox1.Text = "State Information";
            // 
            // tblltInfo
            // 
            this.tblltInfo.ColumnCount = 2;
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.78947F));
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.21053F));
            this.tblltInfo.Controls.Add(this.tblltButtons, 0, 3);
            this.tblltInfo.Controls.Add(this.label1, 0, 0);
            this.tblltInfo.Controls.Add(this.label2, 0, 1);
            this.tblltInfo.Controls.Add(this.label3, 0, 2);
            this.tblltInfo.Controls.Add(this.txtGStID, 1, 0);
            this.tblltInfo.Controls.Add(this.txtgstpercent, 1, 1);
            this.tblltInfo.Controls.Add(this.tblltGST, 1, 2);
            this.tblltInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltInfo.Location = new System.Drawing.Point(2, 25);
            this.tblltInfo.Name = "tblltInfo";
            this.tblltInfo.RowCount = 4;
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltInfo.Size = new System.Drawing.Size(380, 113);
            this.tblltInfo.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 6;
            this.tblltInfo.SetColumnSpan(this.tblltButtons, 2);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltButtons.Controls.Add(this.btnClose, 4, 0);
            this.tblltButtons.Controls.Add(this.btnnew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnDelete, 3, 0);
            this.tblltButtons.Controls.Add(this.btnAdd, 2, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 84);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(380, 29);
            this.tblltButtons.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(259, 1);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(64, 27);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(55, 1);
            this.btnnew.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(64, 27);
            this.btnnew.TabIndex = 4;
            this.btnnew.Text = "New";
            this.btnnew.UseVisualStyleBackColor = false;
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(191, 1);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(64, 27);
            this.btnDelete.TabIndex = 5;
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
            this.btnAdd.Location = new System.Drawing.Point(123, 1);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(64, 27);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
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
            this.label3.Size = new System.Drawing.Size(112, 24);
            this.label3.TabIndex = 18;
            this.label3.Text = "Apply For:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtgstpercent
            // 
            this.txtgstpercent.BackColor = System.Drawing.Color.White;
            this.txtgstpercent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtgstpercent.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtgstpercent.ForeColor = System.Drawing.Color.Black;
            this.txtgstpercent.Location = new System.Drawing.Point(118, 30);
            this.txtgstpercent.Margin = new System.Windows.Forms.Padding(2);
            this.txtgstpercent.Name = "txtgstpercent";
            this.txtgstpercent.Size = new System.Drawing.Size(260, 25);
            this.txtgstpercent.TabIndex = 1;
            // 
            // tblltGST
            // 
            this.tblltGST.ColumnCount = 2;
            this.tblltGST.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltGST.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltGST.Controls.Add(this.rbtigst, 1, 0);
            this.tblltGST.Controls.Add(this.rbtcnsgst, 0, 0);
            this.tblltGST.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltGST.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblltGST.Location = new System.Drawing.Point(116, 56);
            this.tblltGST.Margin = new System.Windows.Forms.Padding(0);
            this.tblltGST.Name = "tblltGST";
            this.tblltGST.RowCount = 1;
            this.tblltGST.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltGST.Size = new System.Drawing.Size(264, 28);
            this.tblltGST.TabIndex = 2;
            // 
            // rbtigst
            // 
            this.rbtigst.AutoSize = true;
            this.rbtigst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtigst.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtigst.Location = new System.Drawing.Point(137, 2);
            this.rbtigst.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.rbtigst.Name = "rbtigst";
            this.rbtigst.Size = new System.Drawing.Size(125, 24);
            this.rbtigst.TabIndex = 20;
            this.rbtigst.Text = "IGST";
            this.rbtigst.UseVisualStyleBackColor = true;
            // 
            // rbtcnsgst
            // 
            this.rbtcnsgst.AutoSize = true;
            this.rbtcnsgst.Checked = true;
            this.rbtcnsgst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtcnsgst.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtcnsgst.Location = new System.Drawing.Point(5, 2);
            this.rbtcnsgst.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.rbtcnsgst.Name = "rbtcnsgst";
            this.rbtcnsgst.Size = new System.Drawing.Size(125, 24);
            this.rbtcnsgst.TabIndex = 19;
            this.rbtcnsgst.TabStop = true;
            this.rbtcnsgst.Text = "CGST + SGST";
            this.rbtcnsgst.UseVisualStyleBackColor = true;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.sgrpBox1, 0, 1);
            this.tblltMain.Controls.Add(this.sgrpBox2, 0, 2);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62F));
            this.tblltMain.Size = new System.Drawing.Size(384, 472);
            this.tblltMain.TabIndex = 0;
            // 
            // frmGstMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(384, 472);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmGstMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GST Information";
            this.Load += new System.EventHandler(this.frmUnitInformation_Load);
            this.tblltList.ResumeLayout(false);
            this.tblltList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GVList)).EndInit();
            this.sgrpBox2.ResumeLayout(false);
            this.sgrpBox1.ResumeLayout(false);
            this.tblltInfo.ResumeLayout(false);
            this.tblltInfo.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.tblltGST.ResumeLayout(false);
            this.tblltGST.PerformLayout();
            this.tblltMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotaluUnits;
        private System.Windows.Forms.TableLayoutPanel tblltList;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox sgrpBox2;
        private System.Windows.Forms.TextBox txtGStID;
        private System.Windows.Forms.GroupBox sgrpBox1;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private RachitControls.NumericTextBox txtgstpercent;
        private System.Windows.Forms.RadioButton rbtigst;
        private System.Windows.Forms.RadioButton rbtcnsgst;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tblltInfo;
        private System.Windows.Forms.TableLayoutPanel tblltGST;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView GVList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn GSTIDgrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn GSTPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApplyCSGST;
        private System.Windows.Forms.DataGridViewTextBoxColumn ApplyIGST;
    }
}