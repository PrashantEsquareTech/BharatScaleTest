namespace AIOInventorySystem.Desk.Forms
{
    partial class frmSendMailList
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
            this.label1 = new System.Windows.Forms.Label();
            this.GvSendMail = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.cmbsname = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTotalSendMail = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnGetAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GvSendMail)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label1, 6);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(654, 35);
            this.label1.TabIndex = 40;
            this.label1.Text = "Send Mail List Track";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GvSendMail
            // 
            this.GvSendMail.AllowUserToAddRows = false;
            this.GvSendMail.AllowUserToDeleteRows = false;
            this.GvSendMail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvSendMail.BackgroundColor = System.Drawing.Color.White;
            this.GvSendMail.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GvSendMail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvSendMail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.tblltMain.SetColumnSpan(this.GvSendMail, 6);
            this.GvSendMail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvSendMail.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvSendMail.Location = new System.Drawing.Point(3, 68);
            this.GvSendMail.Name = "GvSendMail";
            this.GvSendMail.ReadOnly = true;
            this.GvSendMail.RowHeadersWidth = 15;
            this.GvSendMail.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvSendMail.Size = new System.Drawing.Size(648, 371);
            this.GvSendMail.TabIndex = 4;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Update";
            this.Column1.Image = global::AIOInventorySystem.Desk.Properties.Resources.Update;
            this.Column1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // cmbsname
            // 
            this.cmbsname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbsname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbsname.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbsname.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbsname.FormattingEnabled = true;
            this.cmbsname.Location = new System.Drawing.Point(106, 37);
            this.cmbsname.Margin = new System.Windows.Forms.Padding(2);
            this.cmbsname.Name = "cmbsname";
            this.cmbsname.Size = new System.Drawing.Size(159, 25);
            this.cmbsname.TabIndex = 1;
            this.cmbsname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbsname_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(2, 37);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 26);
            this.label7.TabIndex = 70;
            this.label7.Text = " Sender Name:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalSendMail
            // 
            this.lblTotalSendMail.AutoSize = true;
            this.lblTotalSendMail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalSendMail.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSendMail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalSendMail.Location = new System.Drawing.Point(562, 37);
            this.lblTotalSendMail.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotalSendMail.Name = "lblTotalSendMail";
            this.lblTotalSendMail.Size = new System.Drawing.Size(90, 26);
            this.lblTotalSendMail.TabIndex = 91;
            this.lblTotalSendMail.Text = "0";
            this.lblTotalSendMail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(425, 37);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 26);
            this.label8.TabIndex = 90;
            this.label8.Text = "Total Send Mail:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 6;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltMain.Controls.Add(this.btnSearch, 2, 1);
            this.tblltMain.Controls.Add(this.label1, 0, 0);
            this.tblltMain.Controls.Add(this.btnGetAll, 3, 1);
            this.tblltMain.Controls.Add(this.lblTotalSendMail, 5, 1);
            this.tblltMain.Controls.Add(this.GvSendMail, 0, 2);
            this.tblltMain.Controls.Add(this.label8, 4, 1);
            this.tblltMain.Controls.Add(this.label7, 0, 1);
            this.tblltMain.Controls.Add(this.cmbsname, 1, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tblltMain.Size = new System.Drawing.Size(654, 442);
            this.tblltMain.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(269, 37);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(74, 26);
            this.btnSearch.TabIndex = 2;
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
            this.btnGetAll.Location = new System.Drawing.Point(347, 37);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(74, 26);
            this.btnGetAll.TabIndex = 3;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // frmSendMailList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(654, 442);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1057, 670);
            this.Name = "frmSendMailList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Send Mail List";
            this.Load += new System.EventHandler(this.frmSendMailList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvSendMail)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView GvSendMail;
        private System.Windows.Forms.DataGridViewImageColumn Column1;
        private System.Windows.Forms.ComboBox cmbsname;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTotalSendMail;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnGetAll;
    }
}