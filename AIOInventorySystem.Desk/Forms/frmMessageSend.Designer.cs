namespace AIOInventorySystem.Desk.Forms
{
    partial class frmMessageSend
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
            this.GvRemainingpayment = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContactNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemPayment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PaymentDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Send = new System.Windows.Forms.DataGridViewImageColumn();
            this.tblltMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvRemainingpayment)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(884, 40);
            this.label5.TabIndex = 38;
            this.label5.Text = "Message Send";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.GvRemainingpayment, 0, 1);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 2;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 92F));
            this.tblltMain.Size = new System.Drawing.Size(884, 512);
            this.tblltMain.TabIndex = 0;
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
            this.Id,
            this.CustomerName,
            this.Address,
            this.ContactNo,
            this.RemPayment,
            this.PaymentDate,
            this.Send});
            this.GvRemainingpayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvRemainingpayment.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvRemainingpayment.Location = new System.Drawing.Point(2, 42);
            this.GvRemainingpayment.Margin = new System.Windows.Forms.Padding(2);
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
            this.GvRemainingpayment.Size = new System.Drawing.Size(880, 468);
            this.GvRemainingpayment.TabIndex = 0;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // CustomerName
            // 
            this.CustomerName.HeaderText = "Customer Name";
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.Width = 180;
            // 
            // Address
            // 
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            this.Address.Width = 200;
            // 
            // ContactNo
            // 
            this.ContactNo.HeaderText = "ContactNo";
            this.ContactNo.Name = "ContactNo";
            this.ContactNo.Width = 130;
            // 
            // RemPayment
            // 
            this.RemPayment.HeaderText = "Remaining Payment";
            this.RemPayment.Name = "RemPayment";
            this.RemPayment.Width = 150;
            // 
            // PaymentDate
            // 
            this.PaymentDate.HeaderText = "PaymentDate";
            this.PaymentDate.Name = "PaymentDate";
            // 
            // Send
            // 
            this.Send.HeaderText = "Send";
            this.Send.Name = "Send";
            // 
            // frmMessageSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 512);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "frmMessageSend";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Message Send";
            this.Load += new System.EventHandler(this.frmMessageSend_Load);
            this.tblltMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GvRemainingpayment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.DataGridView GvRemainingpayment;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContactNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemPayment;
        private System.Windows.Forms.DataGridViewTextBoxColumn PaymentDate;
        private System.Windows.Forms.DataGridViewImageColumn Send;
    }
}