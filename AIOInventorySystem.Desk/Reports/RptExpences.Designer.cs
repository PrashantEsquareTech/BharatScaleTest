namespace AIOInventorySystem.Desk.Reports
{
    partial class RptExpences
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
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.dtptodate = new System.Windows.Forms.DateTimePicker();
            this.detailLable2 = new System.Windows.Forms.Label();
            this.detailLable1 = new System.Windows.Forms.Label();
            this.dtpfromdate = new System.Windows.Forms.DateTimePicker();
            this.btnPrint = new System.Windows.Forms.Button();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 7;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.Controls.Add(this.crystalReportViewer1, 0, 1);
            this.tblltMain.Controls.Add(this.dtptodate, 4, 0);
            this.tblltMain.Controls.Add(this.detailLable2, 3, 0);
            this.tblltMain.Controls.Add(this.detailLable1, 1, 0);
            this.tblltMain.Controls.Add(this.dtpfromdate, 2, 0);
            this.tblltMain.Controls.Add(this.btnPrint, 6, 0);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 2;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95F));
            this.tblltMain.Size = new System.Drawing.Size(884, 562);
            this.tblltMain.TabIndex = 16;
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tblltMain.SetColumnSpan(this.crystalReportViewer1, 7);
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(3, 31);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(878, 528);
            this.crystalReportViewer1.TabIndex = 2;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
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
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(794, 2);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(88, 24);
            this.btnPrint.TabIndex = 211;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // RptExpences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "RptExpences";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Expences Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.DateTimePicker dtptodate;
        private System.Windows.Forms.Label detailLable2;
        private System.Windows.Forms.Label detailLable1;
        private System.Windows.Forms.DateTimePicker dtpfromdate;
        private System.Windows.Forms.Button btnPrint;

    }
}