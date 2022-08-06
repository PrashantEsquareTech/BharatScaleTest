namespace AIOInventorySystem.Desk.Forms
{
    partial class Order_Booking_List
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
            this.chkorderno = new System.Windows.Forms.CheckBox();
            this.chkcustname = new System.Windows.Forms.CheckBox();
            this.cmbcustomername = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.dtgvList = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderBookingNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderBookingDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.txtorderno = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            this.tblltMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // chkorderno
            // 
            this.chkorderno.AutoSize = true;
            this.chkorderno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkorderno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkorderno.Location = new System.Drawing.Point(331, 37);
            this.chkorderno.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkorderno.Name = "chkorderno";
            this.chkorderno.Size = new System.Drawing.Size(137, 26);
            this.chkorderno.TabIndex = 3;
            this.chkorderno.Text = "Order Booking No";
            this.chkorderno.UseVisualStyleBackColor = true;
            this.chkorderno.CheckedChanged += new System.EventHandler(this.chkorderno_CheckedChanged);
            // 
            // chkcustname
            // 
            this.chkcustname.AutoSize = true;
            this.chkcustname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkcustname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkcustname.Location = new System.Drawing.Point(4, 37);
            this.chkcustname.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkcustname.Name = "chkcustname";
            this.chkcustname.Size = new System.Drawing.Size(130, 26);
            this.chkcustname.TabIndex = 1;
            this.chkcustname.Text = "Customer Name";
            this.chkcustname.UseVisualStyleBackColor = true;
            this.chkcustname.CheckedChanged += new System.EventHandler(this.chkcustname_CheckedChanged);
            // 
            // cmbcustomername
            // 
            this.cmbcustomername.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbcustomername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcustomername.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbcustomername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustomername.FormattingEnabled = true;
            this.cmbcustomername.Location = new System.Drawing.Point(138, 37);
            this.cmbcustomername.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustomername.Name = "cmbcustomername";
            this.cmbcustomername.Size = new System.Drawing.Size(187, 25);
            this.cmbcustomername.TabIndex = 2;
            this.cmbcustomername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcustomername_KeyDown);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label5, 6);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(684, 35);
            this.label5.TabIndex = 36;
            this.label5.Text = "Order Booking List";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 6;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltMain.Controls.Add(this.dtgvList, 0, 2);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.btnSearch, 4, 1);
            this.tblltMain.Controls.Add(this.btnGetAll, 5, 1);
            this.tblltMain.Controls.Add(this.chkcustname, 0, 1);
            this.tblltMain.Controls.Add(this.cmbcustomername, 1, 1);
            this.tblltMain.Controls.Add(this.chkorderno, 2, 1);
            this.tblltMain.Controls.Add(this.txtorderno, 3, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87F));
            this.tblltMain.Size = new System.Drawing.Size(684, 512);
            this.tblltMain.TabIndex = 0;
            // 
            // dtgvList
            // 
            this.dtgvList.AllowUserToAddRows = false;
            this.dtgvList.AllowUserToDeleteRows = false;
            this.dtgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvList.BackgroundColor = System.Drawing.Color.White;
            this.dtgvList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
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
            this.Id,
            this.OrderBookingNo,
            this.OrderBookingDate,
            this.CustomerName});
            this.tblltMain.SetColumnSpan(this.dtgvList, 6);
            this.dtgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgvList.Location = new System.Drawing.Point(3, 69);
            this.dtgvList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtgvList.Name = "dtgvList";
            this.dtgvList.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvList.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtgvList.RowHeadersWidth = 15;
            this.dtgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvList.Size = new System.Drawing.Size(678, 439);
            this.dtgvList.TabIndex = 7;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // OrderBookingNo
            // 
            this.OrderBookingNo.HeaderText = "Order Booking No";
            this.OrderBookingNo.Name = "OrderBookingNo";
            this.OrderBookingNo.ReadOnly = true;
            // 
            // OrderBookingDate
            // 
            this.OrderBookingDate.HeaderText = "Order Booking Date";
            this.OrderBookingDate.Name = "OrderBookingDate";
            this.OrderBookingDate.ReadOnly = true;
            // 
            // CustomerName
            // 
            this.CustomerName.HeaderText = "Customer Name";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(560, 37);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(57, 26);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnGetAll
            // 
            this.btnGetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnGetAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGetAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAll.ForeColor = System.Drawing.Color.White;
            this.btnGetAll.Location = new System.Drawing.Point(621, 37);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(61, 26);
            this.btnGetAll.TabIndex = 6;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // txtorderno
            // 
            this.txtorderno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtorderno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtorderno.Location = new System.Drawing.Point(472, 37);
            this.txtorderno.Margin = new System.Windows.Forms.Padding(2);
            this.txtorderno.Name = "txtorderno";
            this.txtorderno.Size = new System.Drawing.Size(84, 25);
            this.txtorderno.TabIndex = 4;
            this.txtorderno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtorderno_KeyDown);
            // 
            // Order_Booking_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(684, 512);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "Order_Booking_List";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Order Booking List";
            this.Load += new System.EventHandler(this.Order_Booking_List_Load_1);
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkorderno;
        private System.Windows.Forms.CheckBox chkcustname;
        private System.Windows.Forms.ComboBox cmbcustomername;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private RachitControls.NumericTextBox txtorderno;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnGetAll;
        private System.Windows.Forms.DataGridView dtgvList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderBookingNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderBookingDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
    }
}