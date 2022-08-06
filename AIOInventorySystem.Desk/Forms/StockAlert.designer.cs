namespace AIOInventorySystem.Desk.Forms
{
    partial class StockAlert
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
            this.GvPorderInfo = new System.Windows.Forms.DataGridView();
            this.lblTotaCount = new System.Windows.Forms.Label();
            this.lablecount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbunitname = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtpname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbCompanyName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btngetall = new System.Windows.Forms.Button();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.GvPorderInfo)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // GvPorderInfo
            // 
            this.GvPorderInfo.AllowUserToAddRows = false;
            this.GvPorderInfo.AllowUserToDeleteRows = false;
            this.GvPorderInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvPorderInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvPorderInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvPorderInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvPorderInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblltMain.SetColumnSpan(this.GvPorderInfo, 4);
            this.GvPorderInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvPorderInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvPorderInfo.Location = new System.Drawing.Point(3, 103);
            this.GvPorderInfo.Name = "GvPorderInfo";
            this.GvPorderInfo.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvPorderInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GvPorderInfo.RowHeadersWidth = 15;
            this.GvPorderInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvPorderInfo.Size = new System.Drawing.Size(578, 406);
            this.GvPorderInfo.TabIndex = 6;
            // 
            // lblTotaCount
            // 
            this.lblTotaCount.AutoSize = true;
            this.lblTotaCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotaCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotaCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotaCount.Location = new System.Drawing.Point(491, 2);
            this.lblTotaCount.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotaCount.Name = "lblTotaCount";
            this.lblTotaCount.Size = new System.Drawing.Size(91, 26);
            this.lblTotaCount.TabIndex = 48;
            this.lblTotaCount.Text = "0.00";
            this.lblTotaCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lablecount
            // 
            this.lablecount.AutoSize = true;
            this.lablecount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lablecount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lablecount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lablecount.Location = new System.Drawing.Point(316, 2);
            this.lablecount.Margin = new System.Windows.Forms.Padding(2);
            this.lablecount.Name = "lablecount";
            this.lablecount.Size = new System.Drawing.Size(171, 26);
            this.lablecount.TabIndex = 47;
            this.lablecount.Text = "Total Product Count:";
            this.lablecount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.label1.Size = new System.Drawing.Size(54, 26);
            this.label1.TabIndex = 38;
            this.label1.Text = "Unit:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbunitname
            // 
            this.cmbunitname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbunitname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbunitname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbunitname.ForeColor = System.Drawing.Color.Black;
            this.cmbunitname.FormattingEnabled = true;
            this.cmbunitname.Location = new System.Drawing.Point(60, 2);
            this.cmbunitname.Margin = new System.Windows.Forms.Padding(2);
            this.cmbunitname.Name = "cmbunitname";
            this.cmbunitname.Size = new System.Drawing.Size(112, 25);
            this.cmbunitname.TabIndex = 3;
            this.cmbunitname.SelectedIndexChanged += new System.EventHandler(this.cmbunitname_SelectedIndexChanged);
            this.cmbunitname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbunitname_KeyDown);
            this.cmbunitname.Leave += new System.EventHandler(this.cmbunitname_Leave);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label5, 4);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(584, 40);
            this.label5.TabIndex = 11;
            this.label5.Text = "Stock Alert";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtpname
            // 
            this.txtpname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtpname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtpname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtpname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpname.Location = new System.Drawing.Point(107, 42);
            this.txtpname.Margin = new System.Windows.Forms.Padding(2);
            this.txtpname.Name = "txtpname";
            this.txtpname.Size = new System.Drawing.Size(194, 25);
            this.txtpname.TabIndex = 1;
            this.txtpname.Text = " ";
            this.txtpname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpname_KeyDown);
            this.txtpname.Leave += new System.EventHandler(this.txtpname_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(2, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 26);
            this.label2.TabIndex = 58;
            this.label2.Text = "Product Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCompanyName
            // 
            this.cmbCompanyName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbCompanyName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCompanyName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCompanyName.ForeColor = System.Drawing.Color.Black;
            this.cmbCompanyName.FormattingEnabled = true;
            this.cmbCompanyName.Location = new System.Drawing.Point(410, 42);
            this.cmbCompanyName.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCompanyName.Name = "cmbCompanyName";
            this.cmbCompanyName.Size = new System.Drawing.Size(172, 25);
            this.cmbCompanyName.TabIndex = 2;
            this.cmbCompanyName.SelectedIndexChanged += new System.EventHandler(this.cmbCompanyName_SelectedIndexChanged);
            this.cmbCompanyName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCompanyName_KeyDown);
            this.cmbCompanyName.Leave += new System.EventHandler(this.cmbCompanyName_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(305, 42);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 26);
            this.label3.TabIndex = 60;
            this.label3.Text = "MFG Company:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tblltMain.SetColumnSpan(this.tableLayoutPanel2, 4);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tableLayoutPanel2.Controls.Add(this.cmbunitname, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btnClose, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.btngetall, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.lablecount, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTotaCount, 5, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 70);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(584, 30);
            this.tableLayoutPanel2.TabIndex = 3;
            this.tableLayoutPanel2.TabStop = true;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(246, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(66, 26);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btngetall
            // 
            this.btngetall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btngetall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btngetall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btngetall.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngetall.ForeColor = System.Drawing.Color.White;
            this.btngetall.Location = new System.Drawing.Point(176, 2);
            this.btngetall.Margin = new System.Windows.Forms.Padding(2);
            this.btngetall.Name = "btngetall";
            this.btngetall.Size = new System.Drawing.Size(66, 26);
            this.btngetall.TabIndex = 4;
            this.btngetall.Text = "Get All";
            this.btngetall.UseVisualStyleBackColor = false;
            this.btngetall.Click += new System.EventHandler(this.btngetall_Click);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 4;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.cmbCompanyName, 3, 1);
            this.tblltMain.Controls.Add(this.GvPorderInfo, 0, 3);
            this.tblltMain.Controls.Add(this.label3, 2, 1);
            this.tblltMain.Controls.Add(this.label2, 0, 1);
            this.tblltMain.Controls.Add(this.txtpname, 1, 1);
            this.tblltMain.Controls.Add(this.tableLayoutPanel2, 0, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tblltMain.Size = new System.Drawing.Size(584, 512);
            this.tblltMain.TabIndex = 0;
            // 
            // StockAlert
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(584, 512);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "StockAlert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Alert";
            this.Load += new System.EventHandler(this.Stock_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvPorderInfo)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbunitname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotaCount;
        private System.Windows.Forms.Label lablecount;
        private System.Windows.Forms.DataGridView GvPorderInfo;
        private System.Windows.Forms.TextBox txtpname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbCompanyName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Button btngetall;
        private System.Windows.Forms.Button btnClose;
    }
}