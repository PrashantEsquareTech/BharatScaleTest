namespace AIOInventorySystem.Desk.Reports
{
    partial class RptCustomerbill
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtbillno = new System.Windows.Forms.TextBox();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnPrintReturn = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tblltMain.SetColumnSpan(this.crystalReportViewer1, 6);
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(3, 31);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(878, 528);
            this.crystalReportViewer1.TabIndex = 0;
            this.crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(214, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 23);
            this.label1.TabIndex = 21;
            this.label1.Text = "Bill No:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtbillno
            // 
            this.txtbillno.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtbillno.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbillno.ForeColor = System.Drawing.Color.Black;
            this.txtbillno.Location = new System.Drawing.Point(337, 2);
            this.txtbillno.Margin = new System.Windows.Forms.Padding(2);
            this.txtbillno.Name = "txtbillno";
            this.txtbillno.Size = new System.Drawing.Size(119, 29);
            this.txtbillno.TabIndex = 1;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 6;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltMain.Controls.Add(this.crystalReportViewer1, 0, 1);
            this.tblltMain.Controls.Add(this.label1, 1, 0);
            this.tblltMain.Controls.Add(this.txtbillno, 2, 0);
            this.tblltMain.Controls.Add(this.btnPrintReturn, 5, 0);
            this.tblltMain.Controls.Add(this.btnprint, 4, 0);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 2;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95F));
            this.tblltMain.Size = new System.Drawing.Size(884, 562);
            this.tblltMain.TabIndex = 22;
            // 
            // btnPrintReturn
            // 
            this.btnPrintReturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnPrintReturn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrintReturn.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnPrintReturn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrintReturn.ForeColor = System.Drawing.Color.White;
            this.btnPrintReturn.Location = new System.Drawing.Point(778, 2);
            this.btnPrintReturn.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrintReturn.Name = "btnPrintReturn";
            this.btnPrintReturn.Size = new System.Drawing.Size(104, 24);
            this.btnPrintReturn.TabIndex = 213;
            this.btnPrintReturn.Text = "Print Return";
            this.btnPrintReturn.UseVisualStyleBackColor = false;
            this.btnPrintReturn.Click += new System.EventHandler(this.btnPrintReturn_Click);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprint.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(672, 2);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(102, 24);
            this.btnprint.TabIndex = 212;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // RptCustomerbill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(884, 562);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "RptCustomerbill";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Invoice Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbillno;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Button btnPrintReturn;
        private System.Windows.Forms.Button btnprint;
    }
}