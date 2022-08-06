namespace AIOInventorySystem.Desk.Forms
{
    partial class frmSummaryDetails
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
            this.dtpMonth = new System.Windows.Forms.DateTimePicker();
            this.dtptomonth = new System.Windows.Forms.DateTimePicker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.detailLable2 = new System.Windows.Forms.Label();
            this.detailLable1 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpMonth
            // 
            this.dtpMonth.CustomFormat = "";
            this.dtpMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpMonth.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMonth.Location = new System.Drawing.Point(84, 8);
            this.dtpMonth.Margin = new System.Windows.Forms.Padding(2);
            this.dtpMonth.Name = "dtpMonth";
            this.dtpMonth.Size = new System.Drawing.Size(102, 25);
            this.dtpMonth.TabIndex = 1;
            // 
            // dtptomonth
            // 
            this.dtptomonth.CustomFormat = "";
            this.dtptomonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtptomonth.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtptomonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtptomonth.Location = new System.Drawing.Point(219, 8);
            this.dtptomonth.Margin = new System.Windows.Forms.Padding(2);
            this.dtptomonth.Name = "dtptomonth";
            this.dtptomonth.Size = new System.Drawing.Size(102, 25);
            this.dtptomonth.TabIndex = 2;
            // 
            // progressBar1
            // 
            this.tblltMain.SetColumnSpan(this.progressBar1, 4);
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(84, 39);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(299, 21);
            this.progressBar1.TabIndex = 18;
            this.progressBar1.Visible = false;
            // 
            // detailLable2
            // 
            this.detailLable2.AutoSize = true;
            this.detailLable2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailLable2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailLable2.ForeColor = System.Drawing.Color.Black;
            this.detailLable2.Location = new System.Drawing.Point(190, 8);
            this.detailLable2.Margin = new System.Windows.Forms.Padding(2);
            this.detailLable2.Name = "detailLable2";
            this.detailLable2.Size = new System.Drawing.Size(25, 27);
            this.detailLable2.TabIndex = 17;
            this.detailLable2.Text = "To";
            this.detailLable2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // detailLable1
            // 
            this.detailLable1.AutoSize = true;
            this.detailLable1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailLable1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailLable1.ForeColor = System.Drawing.Color.Black;
            this.detailLable1.Location = new System.Drawing.Point(2, 8);
            this.detailLable1.Margin = new System.Windows.Forms.Padding(2);
            this.detailLable1.Name = "detailLable1";
            this.detailLable1.Size = new System.Drawing.Size(78, 27);
            this.detailLable1.TabIndex = 15;
            this.detailLable1.Text = "From Date:";
            this.detailLable1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(325, 8);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(58, 27);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnExportExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExportExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExportExcel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportExcel.ForeColor = System.Drawing.Color.White;
            this.btnExportExcel.Location = new System.Drawing.Point(387, 8);
            this.btnExportExcel.Margin = new System.Windows.Forms.Padding(2);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(95, 27);
            this.btnExportExcel.TabIndex = 4;
            this.btnExportExcel.Text = "Export Excel";
            this.btnExportExcel.UseVisualStyleBackColor = false;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 6;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltMain.Controls.Add(this.detailLable1, 0, 1);
            this.tblltMain.Controls.Add(this.btnExportExcel, 5, 1);
            this.tblltMain.Controls.Add(this.btnPrint, 4, 1);
            this.tblltMain.Controls.Add(this.dtpMonth, 1, 1);
            this.tblltMain.Controls.Add(this.detailLable2, 2, 1);
            this.tblltMain.Controls.Add(this.dtptomonth, 3, 1);
            this.tblltMain.Controls.Add(this.progressBar1, 1, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblltMain.Size = new System.Drawing.Size(484, 62);
            this.tblltMain.TabIndex = 0;
            // 
            // frmSummaryDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(484, 62);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmSummaryDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sale Bill Reports";
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpMonth;
        private System.Windows.Forms.Label detailLable2;
        private System.Windows.Forms.DateTimePicker dtptomonth;
        private System.Windows.Forms.Label detailLable1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
    }
}