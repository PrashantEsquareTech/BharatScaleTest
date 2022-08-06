namespace AIOInventorySystem.Desk.Forms
{
    partial class frmBatchData
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dtgvBatchInfo = new System.Windows.Forms.DataGridView();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.Selectg = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ProductNameg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Company = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Expiryg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantityg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UsedQtyg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemQtyg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.POrderNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvBatchInfo)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgvBatchInfo
            // 
            this.dtgvBatchInfo.AllowUserToAddRows = false;
            this.dtgvBatchInfo.AllowUserToDeleteRows = false;
            this.dtgvBatchInfo.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvBatchInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgvBatchInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvBatchInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selectg,
            this.ProductNameg,
            this.Company,
            this.BatchNo,
            this.Expiryg,
            this.Quantityg,
            this.UsedQtyg,
            this.RemQtyg,
            this.POrderNo});
            this.tblltMain.SetColumnSpan(this.dtgvBatchInfo, 2);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtgvBatchInfo.DefaultCellStyle = dataGridViewCellStyle2;
            this.dtgvBatchInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvBatchInfo.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dtgvBatchInfo.Location = new System.Drawing.Point(3, 3);
            this.dtgvBatchInfo.Name = "dtgvBatchInfo";
            this.dtgvBatchInfo.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgvBatchInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dtgvBatchInfo.RowHeadersWidth = 15;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtgvBatchInfo.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dtgvBatchInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvBatchInfo.Size = new System.Drawing.Size(740, 216);
            this.dtgvBatchInfo.TabIndex = 0;
            this.dtgvBatchInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 2;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.Controls.Add(this.dtgvBatchInfo, 0, 0);
            this.tblltMain.Controls.Add(this.btnClose, 1, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 2;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltMain.Size = new System.Drawing.Size(746, 262);
            this.tblltMain.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Crimson;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(673, 224);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(71, 36);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Selectg
            // 
            this.Selectg.FalseValue = "false";
            this.Selectg.FillWeight = 200F;
            this.Selectg.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Selectg.HeaderText = "";
            this.Selectg.Name = "Selectg";
            this.Selectg.ReadOnly = true;
            this.Selectg.TrueValue = "true";
            this.Selectg.Width = 20;
            // 
            // ProductNameg
            // 
            this.ProductNameg.HeaderText = "Product Name";
            this.ProductNameg.Name = "ProductNameg";
            this.ProductNameg.ReadOnly = true;
            this.ProductNameg.Width = 170;
            // 
            // Company
            // 
            this.Company.HeaderText = "Company";
            this.Company.Name = "Company";
            this.Company.ReadOnly = true;
            // 
            // BatchNo
            // 
            this.BatchNo.HeaderText = "Batch No";
            this.BatchNo.Name = "BatchNo";
            this.BatchNo.ReadOnly = true;
            this.BatchNo.Width = 90;
            // 
            // Expiryg
            // 
            this.Expiryg.HeaderText = "Expiry";
            this.Expiryg.Name = "Expiryg";
            this.Expiryg.ReadOnly = true;
            this.Expiryg.Width = 90;
            // 
            // Quantityg
            // 
            this.Quantityg.HeaderText = "Quantity";
            this.Quantityg.Name = "Quantityg";
            this.Quantityg.ReadOnly = true;
            this.Quantityg.Width = 80;
            // 
            // UsedQtyg
            // 
            this.UsedQtyg.HeaderText = "Used Qty";
            this.UsedQtyg.Name = "UsedQtyg";
            this.UsedQtyg.ReadOnly = true;
            this.UsedQtyg.Width = 90;
            // 
            // RemQtyg
            // 
            this.RemQtyg.HeaderText = "Rem Qty";
            this.RemQtyg.Name = "RemQtyg";
            this.RemQtyg.ReadOnly = true;
            this.RemQtyg.Width = 90;
            // 
            // POrderNo
            // 
            this.POrderNo.HeaderText = "POrderNo";
            this.POrderNo.Name = "POrderNo";
            this.POrderNo.ReadOnly = true;
            this.POrderNo.Visible = false;
            this.POrderNo.Width = 50;
            // 
            // frmBatchData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(86)))), ((int)(((byte)(136)))));
            this.ClientSize = new System.Drawing.Size(746, 262);
            this.Controls.Add(this.tblltMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBatchData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBatchData_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmBatchData_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvBatchInfo)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgvBatchInfo;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selectg;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductNameg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Company;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expiryg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantityg;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsedQtyg;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemQtyg;
        private System.Windows.Forms.DataGridViewTextBoxColumn POrderNo;
    }
}