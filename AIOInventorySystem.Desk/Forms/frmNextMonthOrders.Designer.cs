namespace AIOInventorySystem.Desk.Forms
{
    partial class frmNextMonthOrders
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
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.GvorderInfo = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderBookingNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderBookingDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeliveryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblltMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvorderInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(584, 36);
            this.label5.TabIndex = 36;
            this.label5.Text = "Next Month Order Booking List";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.GvorderInfo, 0, 1);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 2;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92F));
            this.tblltMain.Size = new System.Drawing.Size(584, 462);
            this.tblltMain.TabIndex = 0;
            // 
            // GvorderInfo
            // 
            this.GvorderInfo.AllowUserToAddRows = false;
            this.GvorderInfo.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvorderInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvorderInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvorderInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.OrderBookingNo,
            this.OrderBookingDate,
            this.CustomerName,
            this.DeliveryDate});
            this.GvorderInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvorderInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvorderInfo.Location = new System.Drawing.Point(2, 38);
            this.GvorderInfo.Margin = new System.Windows.Forms.Padding(2);
            this.GvorderInfo.Name = "GvorderInfo";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvorderInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GvorderInfo.RowHeadersWidth = 15;
            this.GvorderInfo.Size = new System.Drawing.Size(580, 422);
            this.GvorderInfo.TabIndex = 0;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // OrderBookingNo
            // 
            this.OrderBookingNo.HeaderText = "Order Booking No";
            this.OrderBookingNo.Name = "OrderBookingNo";
            this.OrderBookingNo.Width = 140;
            // 
            // OrderBookingDate
            // 
            this.OrderBookingDate.HeaderText = "Order Booking Date";
            this.OrderBookingDate.Name = "OrderBookingDate";
            this.OrderBookingDate.Width = 150;
            // 
            // CustomerName
            // 
            this.CustomerName.HeaderText = "Customer Name";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.Width = 150;
            // 
            // DeliveryDate
            // 
            this.DeliveryDate.HeaderText = "Delivery Date";
            this.DeliveryDate.Name = "DeliveryDate";
            this.DeliveryDate.Width = 120;
            // 
            // frmNextMonthOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(584, 462);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "frmNextMonthOrders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Next Month Orders Booking";
            this.Load += new System.EventHandler(this.frmNextMonthOrders_Load);
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvorderInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.DataGridView GvorderInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderBookingNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderBookingDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeliveryDate;
    }
}