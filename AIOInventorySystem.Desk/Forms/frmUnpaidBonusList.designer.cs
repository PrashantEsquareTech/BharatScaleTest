namespace AIOInventorySystem.Desk.Forms
{
    partial class frmUnpaidBonusList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.GvreceiptInfo = new System.Windows.Forms.DataGridView();
            this.Pay = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblTBonus = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbcustomer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnsearch = new System.Windows.Forms.Button();
            this.btnGetAll = new System.Windows.Forms.Button();
            this.tblltMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvreceiptInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 7;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltMain.Controls.Add(this.GvreceiptInfo, 0, 2);
            this.tblltMain.Controls.Add(this.lblTBonus, 6, 1);
            this.tblltMain.Controls.Add(this.label9, 0, 0);
            this.tblltMain.Controls.Add(this.label2, 5, 1);
            this.tblltMain.Controls.Add(this.cmbcustomer, 1, 1);
            this.tblltMain.Controls.Add(this.label1, 0, 1);
            this.tblltMain.Controls.Add(this.btnsearch, 3, 1);
            this.tblltMain.Controls.Add(this.btnGetAll, 4, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87F));
            this.tblltMain.Size = new System.Drawing.Size(784, 512);
            this.tblltMain.TabIndex = 0;
            // 
            // GvreceiptInfo
            // 
            this.GvreceiptInfo.AllowUserToAddRows = false;
            this.GvreceiptInfo.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvreceiptInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvreceiptInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvreceiptInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Pay});
            this.tblltMain.SetColumnSpan(this.GvreceiptInfo, 7);
            this.GvreceiptInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvreceiptInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvreceiptInfo.Location = new System.Drawing.Point(3, 68);
            this.GvreceiptInfo.Name = "GvreceiptInfo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvreceiptInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GvreceiptInfo.RowHeadersWidth = 15;
            this.GvreceiptInfo.Size = new System.Drawing.Size(778, 441);
            this.GvreceiptInfo.TabIndex = 4;
            this.GvreceiptInfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvreceiptInfo_CellClick);
            // 
            // Pay
            // 
            this.Pay.DataPropertyName = "Pay Bonus";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.NullValue = "Pay Bonus";
            this.Pay.DefaultCellStyle = dataGridViewCellStyle2;
            this.Pay.HeaderText = "Pay Bonus";
            this.Pay.Name = "Pay";
            this.Pay.Text = "Pay Bonus";
            this.Pay.ToolTipText = "Pay Bonus";
            // 
            // lblTBonus
            // 
            this.lblTBonus.AutoSize = true;
            this.lblTBonus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTBonus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTBonus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTBonus.Location = new System.Drawing.Point(673, 37);
            this.lblTBonus.Margin = new System.Windows.Forms.Padding(2);
            this.lblTBonus.Name = "lblTBonus";
            this.lblTBonus.Size = new System.Drawing.Size(109, 26);
            this.lblTBonus.TabIndex = 1;
            this.lblTBonus.Text = "0.00";
            this.lblTBonus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label9, 7);
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(784, 35);
            this.label9.TabIndex = 33;
            this.label9.Text = "UnPaid Bonus list";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(525, 37);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 26);
            this.label2.TabIndex = 0;
            this.label2.Text = "Total Bonus to paid:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbcustomer
            // 
            this.cmbcustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcustomer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustomer.FormattingEnabled = true;
            this.cmbcustomer.Location = new System.Drawing.Point(127, 37);
            this.cmbcustomer.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustomer.Name = "cmbcustomer";
            this.cmbcustomer.Size = new System.Drawing.Size(215, 25);
            this.cmbcustomer.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 26);
            this.label1.TabIndex = 20;
            this.label1.Text = "Customer Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(385, 37);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(66, 26);
            this.btnsearch.TabIndex = 2;
            this.btnsearch.Text = "Search";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // btnGetAll
            // 
            this.btnGetAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnGetAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnGetAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnGetAll.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGetAll.ForeColor = System.Drawing.Color.White;
            this.btnGetAll.Location = new System.Drawing.Point(455, 37);
            this.btnGetAll.Margin = new System.Windows.Forms.Padding(2);
            this.btnGetAll.Name = "btnGetAll";
            this.btnGetAll.Size = new System.Drawing.Size(66, 26);
            this.btnGetAll.TabIndex = 3;
            this.btnGetAll.Text = "Get All";
            this.btnGetAll.UseVisualStyleBackColor = false;
            this.btnGetAll.Click += new System.EventHandler(this.btnGetAll_Click);
            // 
            // frmUnpaidBonusList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(784, 512);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmUnpaidBonusList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unpaid Bonus List";
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvreceiptInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbcustomer;
        private System.Windows.Forms.DataGridView GvreceiptInfo;
        private System.Windows.Forms.DataGridViewButtonColumn Pay;
        private System.Windows.Forms.Label lblTBonus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Button btnGetAll;

    }
}