namespace AIOInventorySystem.Desk.Forms
{
    partial class frmFakeRemainingpayment
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
            this.lblCustomerCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblTotalMount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnprint = new System.Windows.Forms.Button();
            this.GvRemainingpayment = new System.Windows.Forms.DataGridView();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MobileNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemainingAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SendSMS = new System.Windows.Forms.DataGridViewImageColumn();
            this.tblltMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvRemainingpayment)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label5, 5);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(784, 35);
            this.label5.TabIndex = 36;
            this.label5.Text = "Fake Bill Customer Credits";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCustomerCount
            // 
            this.lblCustomerCount.AutoSize = true;
            this.lblCustomerCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCustomerCount.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblCustomerCount.Location = new System.Drawing.Point(228, 37);
            this.lblCustomerCount.Margin = new System.Windows.Forms.Padding(2);
            this.lblCustomerCount.Name = "lblCustomerCount";
            this.lblCustomerCount.Size = new System.Drawing.Size(180, 31);
            this.lblCustomerCount.TabIndex = 174;
            this.lblCustomerCount.Text = "---";
            this.lblCustomerCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(59, 37);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(165, 31);
            this.label7.TabIndex = 173;
            this.label7.Text = "Total Customers:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalMount
            // 
            this.lblTotalMount.AutoSize = true;
            this.lblTotalMount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalMount.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalMount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalMount.Location = new System.Drawing.Point(561, 37);
            this.lblTotalMount.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotalMount.Name = "lblTotalMount";
            this.lblTotalMount.Size = new System.Drawing.Size(221, 31);
            this.lblTotalMount.TabIndex = 176;
            this.lblTotalMount.Text = "---";
            this.lblTotalMount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(412, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 31);
            this.label2.TabIndex = 175;
            this.label2.Text = "Total Amount:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 5;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.317073F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.61863F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.50333F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.06874F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.49224F));
            this.tblltMain.Controls.Add(this.btnprint, 0, 1);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.lblTotalMount, 4, 1);
            this.tblltMain.Controls.Add(this.label7, 1, 1);
            this.tblltMain.Controls.Add(this.label2, 3, 1);
            this.tblltMain.Controls.Add(this.lblCustomerCount, 2, 1);
            this.tblltMain.Controls.Add(this.GvRemainingpayment, 0, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86F));
            this.tblltMain.Size = new System.Drawing.Size(784, 512);
            this.tblltMain.TabIndex = 0;
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(2, 37);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(53, 31);
            this.btnprint.TabIndex = 0;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // GvRemainingpayment
            // 
            this.GvRemainingpayment.AllowUserToAddRows = false;
            this.GvRemainingpayment.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvRemainingpayment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvRemainingpayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvRemainingpayment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CustomerName,
            this.CustomerAddress,
            this.MobileNo,
            this.RemainingAmt,
            this.CustomerId,
            this.SendSMS});
            this.tblltMain.SetColumnSpan(this.GvRemainingpayment, 5);
            this.GvRemainingpayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvRemainingpayment.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvRemainingpayment.Location = new System.Drawing.Point(3, 73);
            this.GvRemainingpayment.Name = "GvRemainingpayment";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvRemainingpayment.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.GvRemainingpayment.RowHeadersWidth = 15;
            this.GvRemainingpayment.Size = new System.Drawing.Size(778, 436);
            this.GvRemainingpayment.TabIndex = 1;
            this.GvRemainingpayment.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvRemainingpayment_CellClick);
            // 
            // CustomerName
            // 
            this.CustomerName.HeaderText = "Customer Name";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.Width = 200;
            // 
            // CustomerAddress
            // 
            this.CustomerAddress.HeaderText = "Customer Address";
            this.CustomerAddress.Name = "CustomerAddress";
            this.CustomerAddress.Width = 190;
            // 
            // MobileNo
            // 
            this.MobileNo.HeaderText = "Mobile No";
            this.MobileNo.Name = "MobileNo";
            this.MobileNo.Width = 130;
            // 
            // RemainingAmt
            // 
            this.RemainingAmt.HeaderText = "Remaining Amount";
            this.RemainingAmt.Name = "RemainingAmt";
            this.RemainingAmt.Width = 130;
            // 
            // CustomerId
            // 
            this.CustomerId.HeaderText = "CustomerId";
            this.CustomerId.Name = "CustomerId";
            this.CustomerId.Visible = false;
            // 
            // SendSMS
            // 
            this.SendSMS.HeaderText = "Send SMS";
            this.SendSMS.Image = global::AIOInventorySystem.Desk.Properties.Resources.Send_SMS;
            this.SendSMS.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.SendSMS.Name = "SendSMS";
            this.SendSMS.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SendSMS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.SendSMS.Width = 110;
            // 
            // frmFakeRemainingpayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(784, 512);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "frmFakeRemainingpayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fake Bill Remaining Payment List";
            this.Load += new System.EventHandler(this.frmFakeRemainingpayment_Load);
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvRemainingpayment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCustomerCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTotalMount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.DataGridView GvRemainingpayment;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn MobileNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemainingAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerId;
        private System.Windows.Forms.DataGridViewImageColumn SendSMS;
    }
}