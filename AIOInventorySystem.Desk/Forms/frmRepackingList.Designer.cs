namespace AIOInventorySystem.Desk.Forms
{
    partial class frmRepackingList
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
            this.chkRepackNo = new System.Windows.Forms.CheckBox();
            this.chkproductname = new System.Windows.Forms.CheckBox();
            this.txtproductname = new System.Windows.Forms.TextBox();
            this.GvPorderInfo = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.rbtnDeleteMultipleBill = new System.Windows.Forms.Button();
            this.txtRepackNo = new AIOInventorySystem.Desk.RachitControls.NumericTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.GvPorderInfo)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkRepackNo
            // 
            this.chkRepackNo.AutoSize = true;
            this.chkRepackNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkRepackNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRepackNo.Location = new System.Drawing.Point(5, 37);
            this.chkRepackNo.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkRepackNo.Name = "chkRepackNo";
            this.chkRepackNo.Size = new System.Drawing.Size(102, 23);
            this.chkRepackNo.TabIndex = 1;
            this.chkRepackNo.Text = "Repack No";
            this.chkRepackNo.UseVisualStyleBackColor = true;
            this.chkRepackNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkporderno_KeyDown);
            // 
            // chkproductname
            // 
            this.chkproductname.AutoSize = true;
            this.chkproductname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkproductname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkproductname.Location = new System.Drawing.Point(208, 37);
            this.chkproductname.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkproductname.Name = "chkproductname";
            this.chkproductname.Size = new System.Drawing.Size(118, 23);
            this.chkproductname.TabIndex = 3;
            this.chkproductname.Text = "Product Name";
            this.chkproductname.UseVisualStyleBackColor = true;
            this.chkproductname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkproductname_KeyDown);
            // 
            // txtproductname
            // 
            this.txtproductname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtproductname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtproductname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtproductname.Location = new System.Drawing.Point(330, 37);
            this.txtproductname.Margin = new System.Windows.Forms.Padding(2);
            this.txtproductname.Name = "txtproductname";
            this.txtproductname.Size = new System.Drawing.Size(207, 25);
            this.txtproductname.TabIndex = 4;
            this.txtproductname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtproductname_KeyDown);
            this.txtproductname.Leave += new System.EventHandler(this.txtproductname_Leave);
            // 
            // GvPorderInfo
            // 
            this.GvPorderInfo.AllowUserToAddRows = false;
            this.GvPorderInfo.AllowUserToDeleteRows = false;
            this.GvPorderInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvPorderInfo.BackgroundColor = System.Drawing.Color.White;
            this.GvPorderInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GvPorderInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvPorderInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column1});
            this.tblltMain.SetColumnSpan(this.GvPorderInfo, 6);
            this.GvPorderInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvPorderInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvPorderInfo.Location = new System.Drawing.Point(3, 65);
            this.GvPorderInfo.Name = "GvPorderInfo";
            this.GvPorderInfo.RowHeadersWidth = 15;
            this.GvPorderInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvPorderInfo.Size = new System.Drawing.Size(778, 354);
            this.GvPorderInfo.TabIndex = 7;
            this.GvPorderInfo.TabStop = false;
            this.GvPorderInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvPorderInfo_CellClick);
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Select";
            this.Column2.Name = "Column2";
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Update";
            this.Column1.Image = global::AIOInventorySystem.Desk.Properties.Resources.Update;
            this.Column1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Column1.Name = "Column1";
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 6;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltMain.Controls.Add(this.btnSearch, 4, 1);
            this.tblltMain.Controls.Add(this.label8, 0, 0);
            this.tblltMain.Controls.Add(this.rbtnDeleteMultipleBill, 5, 1);
            this.tblltMain.Controls.Add(this.GvPorderInfo, 0, 2);
            this.tblltMain.Controls.Add(this.chkRepackNo, 0, 1);
            this.tblltMain.Controls.Add(this.txtproductname, 3, 1);
            this.tblltMain.Controls.Add(this.chkproductname, 2, 1);
            this.tblltMain.Controls.Add(this.txtRepackNo, 1, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tblltMain.Size = new System.Drawing.Size(784, 422);
            this.tblltMain.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(541, 36);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(82, 25);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label8, 6);
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(784, 35);
            this.label8.TabIndex = 38;
            this.label8.Text = "RePacking List";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbtnDeleteMultipleBill
            // 
            this.rbtnDeleteMultipleBill.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.rbtnDeleteMultipleBill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.rbtnDeleteMultipleBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rbtnDeleteMultipleBill.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnDeleteMultipleBill.ForeColor = System.Drawing.Color.White;
            this.rbtnDeleteMultipleBill.Location = new System.Drawing.Point(627, 36);
            this.rbtnDeleteMultipleBill.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.rbtnDeleteMultipleBill.Name = "rbtnDeleteMultipleBill";
            this.rbtnDeleteMultipleBill.Size = new System.Drawing.Size(155, 25);
            this.rbtnDeleteMultipleBill.TabIndex = 6;
            this.rbtnDeleteMultipleBill.Text = "Delete Multiple Bills";
            this.rbtnDeleteMultipleBill.UseVisualStyleBackColor = false;
            this.rbtnDeleteMultipleBill.Click += new System.EventHandler(this.rbtnDeleteMultipleBill_Click);
            // 
            // txtRepackNo
            // 
            this.txtRepackNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRepackNo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepackNo.Location = new System.Drawing.Point(111, 37);
            this.txtRepackNo.Margin = new System.Windows.Forms.Padding(2);
            this.txtRepackNo.Name = "txtRepackNo";
            this.txtRepackNo.Size = new System.Drawing.Size(90, 25);
            this.txtRepackNo.TabIndex = 2;
            this.txtRepackNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPOrderno_KeyDown);
            // 
            // frmRepackingList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(784, 422);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmRepackingList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Repacking List";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmRepackingList_FormClosed);
            this.Load += new System.EventHandler(this.frmRepackingList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvPorderInfo)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkRepackNo;
        private System.Windows.Forms.CheckBox chkproductname;
        private System.Windows.Forms.TextBox txtproductname;
        private System.Windows.Forms.DataGridView GvPorderInfo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column2;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Label label8;
        private RachitControls.NumericTextBox txtRepackNo;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button rbtnDeleteMultipleBill;
    }
}