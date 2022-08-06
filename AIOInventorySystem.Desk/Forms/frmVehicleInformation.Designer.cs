namespace AIOInventorySystem.Desk.Forms
{
    partial class frmVehicleInformation
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
            this.label9 = new System.Windows.Forms.Label();
            this.lblTotaluUnits = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVehicleID = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtVehicleNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.tblltList = new System.Windows.Forms.TableLayoutPanel();
            this.dtgvList = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VehicleIdg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VehicleNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.tblltInfo = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tblltMain.SuspendLayout();
            this.grpList.SuspendLayout();
            this.tblltList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvList)).BeginInit();
            this.grpInfo.SuspendLayout();
            this.tblltInfo.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label9.Location = new System.Drawing.Point(392, 27);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 27);
            this.label9.TabIndex = 199;
            this.label9.Text = "*";
            // 
            // lblTotaluUnits
            // 
            this.lblTotaluUnits.AutoSize = true;
            this.lblTotaluUnits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotaluUnits.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotaluUnits.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotaluUnits.Location = new System.Drawing.Point(137, 2);
            this.lblTotaluUnits.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotaluUnits.Name = "lblTotaluUnits";
            this.lblTotaluUnits.Size = new System.Drawing.Size(283, 22);
            this.lblTotaluUnits.TabIndex = 197;
            this.lblTotaluUnits.Text = "0";
            this.lblTotaluUnits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.label4.Size = new System.Drawing.Size(131, 22);
            this.label4.TabIndex = 196;
            this.label4.Text = "Total Vehicles:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtVehicleID
            // 
            this.txtVehicleID.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtVehicleID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVehicleID.ForeColor = System.Drawing.Color.Black;
            this.txtVehicleID.Location = new System.Drawing.Point(120, 2);
            this.txtVehicleID.Margin = new System.Windows.Forms.Padding(2);
            this.txtVehicleID.Name = "txtVehicleID";
            this.txtVehicleID.ReadOnly = true;
            this.txtVehicleID.Size = new System.Drawing.Size(110, 25);
            this.txtVehicleID.TabIndex = 0;
            this.txtVehicleID.TabStop = false;
            // 
            // txtDescription
            // 
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.ForeColor = System.Drawing.Color.Black;
            this.txtDescription.Location = new System.Drawing.Point(120, 56);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(2);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.tblltInfo.SetRowSpan(this.txtDescription, 2);
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDescription.Size = new System.Drawing.Size(270, 50);
            this.txtDescription.TabIndex = 2;
            this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescription_KeyDown);
            // 
            // txtVehicleNumber
            // 
            this.txtVehicleNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtVehicleNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtVehicleNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtVehicleNumber.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVehicleNumber.ForeColor = System.Drawing.Color.Black;
            this.txtVehicleNumber.Location = new System.Drawing.Point(120, 29);
            this.txtVehicleNumber.Margin = new System.Windows.Forms.Padding(2);
            this.txtVehicleNumber.Name = "txtVehicleNumber";
            this.txtVehicleNumber.Size = new System.Drawing.Size(270, 25);
            this.txtVehicleNumber.TabIndex = 1;
            this.txtVehicleNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVehicleNumber_KeyDown);
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
            this.label3.Size = new System.Drawing.Size(114, 23);
            this.label3.TabIndex = 193;
            this.label3.Text = "Description:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.label1.Size = new System.Drawing.Size(114, 23);
            this.label1.TabIndex = 192;
            this.label1.Text = "Vehicle ID:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.label5.Size = new System.Drawing.Size(434, 35);
            this.label5.TabIndex = 11;
            this.label5.Text = "Vehicle Information";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(2, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 23);
            this.label2.TabIndex = 191;
            this.label2.Text = "Vehicle Number:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.grpList, 0, 2);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.grpInfo, 0, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57F));
            this.tblltMain.Size = new System.Drawing.Size(434, 512);
            this.tblltMain.TabIndex = 0;
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.tblltList);
            this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpList.Location = new System.Drawing.Point(3, 222);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(428, 287);
            this.grpList.TabIndex = 7;
            this.grpList.TabStop = false;
            this.grpList.Text = "Vehicle List";
            // 
            // tblltList
            // 
            this.tblltList.ColumnCount = 2;
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68F));
            this.tblltList.Controls.Add(this.dtgvList, 0, 1);
            this.tblltList.Controls.Add(this.label4, 0, 0);
            this.tblltList.Controls.Add(this.lblTotaluUnits, 1, 0);
            this.tblltList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltList.Location = new System.Drawing.Point(3, 21);
            this.tblltList.Name = "tblltList";
            this.tblltList.RowCount = 2;
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tblltList.Size = new System.Drawing.Size(422, 263);
            this.tblltList.TabIndex = 7;
            // 
            // dtgvList
            // 
            this.dtgvList.AllowUserToAddRows = false;
            this.dtgvList.AllowUserToDeleteRows = false;
            this.dtgvList.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.VehicleIdg,
            this.VehicleNumber,
            this.Description});
            this.tblltList.SetColumnSpan(this.dtgvList, 2);
            this.dtgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgvList.Location = new System.Drawing.Point(3, 29);
            this.dtgvList.Name = "dtgvList";
            this.dtgvList.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvList.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtgvList.RowHeadersWidth = 15;
            this.dtgvList.Size = new System.Drawing.Size(416, 231);
            this.dtgvList.TabIndex = 8;
            this.dtgvList.TabStop = false;
            this.dtgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvVehicleInfo_CellClick);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            this.ID.Width = 50;
            // 
            // VehicleIdg
            // 
            this.VehicleIdg.HeaderText = "Vehicle ID";
            this.VehicleIdg.Name = "VehicleIdg";
            this.VehicleIdg.ReadOnly = true;
            // 
            // VehicleNumber
            // 
            this.VehicleNumber.HeaderText = "Vehicle Number";
            this.VehicleNumber.Name = "VehicleNumber";
            this.VehicleNumber.ReadOnly = true;
            this.VehicleNumber.Width = 150;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 140;
            // 
            // grpInfo
            // 
            this.grpInfo.Controls.Add(this.tblltInfo);
            this.grpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInfo.Location = new System.Drawing.Point(3, 38);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Size = new System.Drawing.Size(428, 178);
            this.grpInfo.TabIndex = 0;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "Vehicle Information";
            // 
            // tblltInfo
            // 
            this.tblltInfo.ColumnCount = 3;
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltInfo.Controls.Add(this.label1, 0, 0);
            this.tblltInfo.Controls.Add(this.label9, 2, 1);
            this.tblltInfo.Controls.Add(this.label2, 0, 1);
            this.tblltInfo.Controls.Add(this.label3, 0, 2);
            this.tblltInfo.Controls.Add(this.txtVehicleID, 1, 0);
            this.tblltInfo.Controls.Add(this.txtVehicleNumber, 1, 1);
            this.tblltInfo.Controls.Add(this.txtDescription, 1, 2);
            this.tblltInfo.Controls.Add(this.tblltButtons, 0, 5);
            this.tblltInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltInfo.Location = new System.Drawing.Point(3, 21);
            this.tblltInfo.Name = "tblltInfo";
            this.tblltInfo.RowCount = 6;
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltInfo.Size = new System.Drawing.Size(422, 154);
            this.tblltInfo.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 6;
            this.tblltInfo.SetColumnSpan(this.tblltButtons, 3);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltButtons.Controls.Add(this.btnnew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnAdd, 2, 0);
            this.tblltButtons.Controls.Add(this.btnDelete, 3, 0);
            this.tblltButtons.Controls.Add(this.btnClose, 4, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 123);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(422, 31);
            this.tblltButtons.TabIndex = 3;
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(61, 1);
            this.btnnew.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(71, 29);
            this.btnnew.TabIndex = 4;
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
            this.btnAdd.Location = new System.Drawing.Point(136, 1);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(71, 29);
            this.btnAdd.TabIndex = 3;
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
            this.btnDelete.Location = new System.Drawing.Point(211, 1);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(71, 29);
            this.btnDelete.TabIndex = 5;
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
            this.btnClose.Location = new System.Drawing.Point(286, 1);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(71, 29);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmVehicleInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(434, 512);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmVehicleInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vehicle Information";
            this.tblltMain.ResumeLayout(false);
            this.grpList.ResumeLayout(false);
            this.tblltList.ResumeLayout(false);
            this.tblltList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvList)).EndInit();
            this.grpInfo.ResumeLayout(false);
            this.tblltInfo.ResumeLayout(false);
            this.tblltInfo.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblTotaluUnits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtVehicleID;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtVehicleNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.TableLayoutPanel tblltInfo;
        private System.Windows.Forms.TableLayoutPanel tblltList;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dtgvList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn VehicleIdg;
        private System.Windows.Forms.DataGridViewTextBoxColumn VehicleNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
    }
}