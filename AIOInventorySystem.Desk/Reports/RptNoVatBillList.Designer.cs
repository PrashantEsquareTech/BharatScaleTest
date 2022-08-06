namespace AIOInventorySystem.Desk.Reports
{
    partial class RptNoVatBillList
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
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.dtptodate = new System.Windows.Forms.DateTimePicker();
            this.detailLable2 = new System.Windows.Forms.Label();
            this.detailLable1 = new System.Windows.Forms.Label();
            this.dtpfromdate = new System.Windows.Forms.DateTimePicker();
            this.btnprint = new System.Windows.Forms.Button();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tblltMain.SetColumnSpan(this.crystalReportViewer1, 8);
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(3, 31);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(878, 528);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 8;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.Controls.Add(this.dtptodate, 4, 0);
            this.tblltMain.Controls.Add(this.detailLable2, 3, 0);
            this.tblltMain.Controls.Add(this.detailLable1, 1, 0);
            this.tblltMain.Controls.Add(this.dtpfromdate, 2, 0);
            this.tblltMain.Controls.Add(this.btnprint, 6, 0);
            this.tblltMain.Controls.Add(this.crystalReportViewer1, 0, 1);
            this.tblltMain.Controls.Add(this.btnGetAll, 7, 0);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 2;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95F));
            this.tblltMain.Size = new System.Drawing.Size(884, 562);
            this.tblltMain.TabIndex = 31;
            // 
            // dtptodate
            // 
            this.dtptodate.CustomFormat = "";
            this.dtptodate.Dock = System.Windows.Forms.DockStyle.Top;
            this.dtptodate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtptodate.Location = new System.Drawing.Point(504, 2);
            this.dtptodate.Margin = new System.Windows.Forms.Padding(2);
            this.dtptodate.Name = "dtptodate";
            this.dtptodate.Size = new System.Drawing.Size(110, 25);
            this.dtptodate.TabIndex = 2;
            // 
            // detailLable2
            // 
            this.detailLable2.AutoSize = true;
            this.detailLable2.Dock = System.Windows.Forms.DockStyle.Top;
            this.detailLable2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailLable2.ForeColor = System.Drawing.Color.Black;
            this.detailLable2.Location = new System.Drawing.Point(469, 2);
            this.detailLable2.Margin = new System.Windows.Forms.Padding(2);
            this.detailLable2.Name = "detailLable2";
            this.detailLable2.Size = new System.Drawing.Size(31, 17);
            this.detailLable2.TabIndex = 10;
            this.detailLable2.Text = "To";
            this.detailLable2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // detailLable1
            // 
            this.detailLable1.AutoSize = true;
            this.detailLable1.Dock = System.Windows.Forms.DockStyle.Top;
            this.detailLable1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailLable1.ForeColor = System.Drawing.Color.Black;
            this.detailLable1.Location = new System.Drawing.Point(249, 2);
            this.detailLable1.Margin = new System.Windows.Forms.Padding(2);
            this.detailLable1.Name = "detailLable1";
            this.detailLable1.Size = new System.Drawing.Size(102, 17);
            this.detailLable1.TabIndex = 8;
            this.detailLable1.Text = "From Date";
            this.detailLable1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpfromdate
            // 
            this.dtpfromdate.CustomFormat = "";
            this.dtpfromdate.Dock = System.Windows.Forms.DockStyle.Top;
            this.dtpfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpfromdate.Location = new System.Drawing.Point(355, 2);
            this.dtpfromdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpfromdate.Name = "dtpfromdate";
            this.dtpfromdate.Size = new System.Drawing.Size(110, 25);
            this.dtpfromdate.TabIndex = 1;
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprint.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(706, 2);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(84, 24);
            this.btnprint.TabIndex = 211;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // btnGetAll
            // 
            this.btnGetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnGetAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetAll.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnGetAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAll.ForeColor = System.Drawing.Color.White;
            this.btnGetAll.Location = new System.Drawing.Point(794, 2);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(88, 24);
            this.btnGetAll.TabIndex = 212;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // RptNoVatBillList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "RptNoVatBillList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NoVat Bill List Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.DateTimePicker dtptodate;
        private System.Windows.Forms.Label detailLable2;
        private System.Windows.Forms.Label detailLable1;
        private System.Windows.Forms.DateTimePicker dtpfromdate;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button btnGetAll;
    }
}