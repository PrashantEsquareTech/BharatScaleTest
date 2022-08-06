namespace AIOInventorySystem.Desk.Forms
{
    partial class FrmHistoryDetails
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
            this.chkType = new System.Windows.Forms.CheckBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.chkcustomername = new System.Windows.Forms.CheckBox();
            this.cmbcustomer = new System.Windows.Forms.ComboBox();
            this.chksupplier = new System.Windows.Forms.CheckBox();
            this.cmbsupplier = new System.Windows.Forms.ComboBox();
            this.GVHistoryDetails = new System.Windows.Forms.DataGridView();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.rbtnGetAll = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GVHistoryDetails)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label5, 8);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(834, 35);
            this.label5.TabIndex = 38;
            this.label5.Text = "History Details";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chkType
            // 
            this.chkType.AutoSize = true;
            this.chkType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkType.Location = new System.Drawing.Point(4, 37);
            this.chkType.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkType.Name = "chkType";
            this.chkType.Size = new System.Drawing.Size(60, 25);
            this.chkType.TabIndex = 1;
            this.chkType.Text = "Type";
            this.chkType.UseVisualStyleBackColor = true;
            // 
            // cmbType
            // 
            this.cmbType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "Invoice",
            "Purchase Bill",
            "Sale Return",
            "Purchase Return",
            "Customer Credit Note",
            "Customer Debit Note",
            "Supplier Credit Note",
            "Supplier Debit Note",
            "Receipt Challan",
            "Repack",
            "Quotation",
            "Purchase Order Place",
            "Delivery Challan ",
            "Order Booking"});
            this.cmbType.Location = new System.Drawing.Point(68, 37);
            this.cmbType.Margin = new System.Windows.Forms.Padding(2);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(91, 25);
            this.cmbType.TabIndex = 2;
            // 
            // chkcustomername
            // 
            this.chkcustomername.AutoSize = true;
            this.chkcustomername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkcustomername.Location = new System.Drawing.Point(165, 37);
            this.chkcustomername.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chkcustomername.Name = "chkcustomername";
            this.chkcustomername.Size = new System.Drawing.Size(123, 25);
            this.chkcustomername.TabIndex = 3;
            this.chkcustomername.Text = "Customer Name";
            this.chkcustomername.UseVisualStyleBackColor = true;
            this.chkcustomername.CheckedChanged += new System.EventHandler(this.chkcustomername_CheckedChanged);
            // 
            // cmbcustomer
            // 
            this.cmbcustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcustomer.FormattingEnabled = true;
            this.cmbcustomer.Location = new System.Drawing.Point(292, 37);
            this.cmbcustomer.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustomer.Name = "cmbcustomer";
            this.cmbcustomer.Size = new System.Drawing.Size(146, 25);
            this.cmbcustomer.TabIndex = 4;
            // 
            // chksupplier
            // 
            this.chksupplier.AutoSize = true;
            this.chksupplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chksupplier.Location = new System.Drawing.Point(444, 37);
            this.chksupplier.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.chksupplier.Name = "chksupplier";
            this.chksupplier.Size = new System.Drawing.Size(114, 25);
            this.chksupplier.TabIndex = 5;
            this.chksupplier.Text = "Supplier Name";
            this.chksupplier.UseVisualStyleBackColor = true;
            this.chksupplier.CheckedChanged += new System.EventHandler(this.chksupplier_CheckedChanged);
            // 
            // cmbsupplier
            // 
            this.cmbsupplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbsupplier.FormattingEnabled = true;
            this.cmbsupplier.Location = new System.Drawing.Point(562, 37);
            this.cmbsupplier.Margin = new System.Windows.Forms.Padding(2);
            this.cmbsupplier.Name = "cmbsupplier";
            this.cmbsupplier.Size = new System.Drawing.Size(141, 25);
            this.cmbsupplier.TabIndex = 6;
            // 
            // GVHistoryDetails
            // 
            this.GVHistoryDetails.AllowUserToAddRows = false;
            this.GVHistoryDetails.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVHistoryDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GVHistoryDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblltMain.SetColumnSpan(this.GVHistoryDetails, 8);
            this.GVHistoryDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GVHistoryDetails.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GVHistoryDetails.Location = new System.Drawing.Point(3, 67);
            this.GVHistoryDetails.Name = "GVHistoryDetails";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GVHistoryDetails.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GVHistoryDetails.Size = new System.Drawing.Size(828, 352);
            this.GVHistoryDetails.TabIndex = 9;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 8;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.5F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.chkType, 0, 1);
            this.tblltMain.Controls.Add(this.cmbType, 1, 1);
            this.tblltMain.Controls.Add(this.GVHistoryDetails, 0, 2);
            this.tblltMain.Controls.Add(this.chkcustomername, 2, 1);
            this.tblltMain.Controls.Add(this.cmbcustomer, 3, 1);
            this.tblltMain.Controls.Add(this.rbtnGetAll, 7, 1);
            this.tblltMain.Controls.Add(this.btnSearch, 6, 1);
            this.tblltMain.Controls.Add(this.cmbsupplier, 5, 1);
            this.tblltMain.Controls.Add(this.chksupplier, 4, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.5F));
            this.tblltMain.Size = new System.Drawing.Size(834, 422);
            this.tblltMain.TabIndex = 0;
            // 
            // rbtnGetAll
            // 
            this.rbtnGetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.rbtnGetAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.rbtnGetAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtnGetAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnGetAll.ForeColor = System.Drawing.Color.White;
            this.rbtnGetAll.Location = new System.Drawing.Point(769, 37);
            this.rbtnGetAll.Margin = new System.Windows.Forms.Padding(2);
            this.rbtnGetAll.Name = "rbtnGetAll";
            this.rbtnGetAll.Size = new System.Drawing.Size(63, 25);
            this.rbtnGetAll.TabIndex = 8;
            this.rbtnGetAll.Text = "Get All";
            this.rbtnGetAll.UseVisualStyleBackColor = false;
            this.rbtnGetAll.Click += new System.EventHandler(this.rbtnGetAll_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(707, 37);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(58, 25);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // FrmHistoryDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(834, 422);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmHistoryDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HistoryDetails";
            ((System.ComponentModel.ISupportInitialize)(this.GVHistoryDetails)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkType;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.CheckBox chkcustomername;
        private System.Windows.Forms.ComboBox cmbcustomer;
        private System.Windows.Forms.CheckBox chksupplier;
        private System.Windows.Forms.ComboBox cmbsupplier;
        private System.Windows.Forms.DataGridView GVHistoryDetails;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button rbtnGetAll;
    }
}