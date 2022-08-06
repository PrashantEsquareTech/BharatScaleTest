namespace AIOInventorySystem.Desk.Forms
{
    partial class frmProductInMarathi
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
            this.dtgProductInfo = new System.Windows.Forms.DataGridView();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.SrNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnglishProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MarathiProduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Updated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DtlId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtgProductInfo)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgProductInfo
            // 
            this.dtgProductInfo.AllowUserToAddRows = false;
            this.dtgProductInfo.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgProductInfo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgProductInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgProductInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SrNo,
            this.EnglishProduct,
            this.MarathiProduct,
            this.Updated,
            this.ProductId,
            this.Unit,
            this.OStock,
            this.UStock,
            this.DtlId});
            this.dtgProductInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgProductInfo.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgProductInfo.Location = new System.Drawing.Point(2, 39);
            this.dtgProductInfo.Margin = new System.Windows.Forms.Padding(2);
            this.dtgProductInfo.Name = "dtgProductInfo";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgProductInfo.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dtgProductInfo.RowHeadersWidth = 15;
            this.dtgProductInfo.Size = new System.Drawing.Size(790, 392);
            this.dtgProductInfo.TabIndex = 1;
            this.dtgProductInfo.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgProductInfo_CellEndEdit);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.label2, 0, 0);
            this.tblltMain.Controls.Add(this.dtgProductInfo, 0, 1);
            this.tblltMain.Controls.Add(this.tblltButtons, 0, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.Size = new System.Drawing.Size(794, 472);
            this.tblltMain.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(794, 37);
            this.label2.TabIndex = 12;
            this.label2.Text = "Product List";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 6;
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.Controls.Add(this.btnCancel, 3, 0);
            this.tblltButtons.Controls.Add(this.btnUpdate, 2, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 433);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(794, 39);
            this.tblltButtons.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(398, 2);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(128, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(266, 2);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(128, 35);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            this.btnUpdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnUpdate_KeyDown);
            // 
            // SrNo
            // 
            this.SrNo.HeaderText = "Sr.No.";
            this.SrNo.Name = "SrNo";
            this.SrNo.Width = 50;
            // 
            // EnglishProduct
            // 
            this.EnglishProduct.HeaderText = "Product In English";
            this.EnglishProduct.Name = "EnglishProduct";
            this.EnglishProduct.ReadOnly = true;
            this.EnglishProduct.Width = 180;
            // 
            // MarathiProduct
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MarathiProduct.DefaultCellStyle = dataGridViewCellStyle2;
            this.MarathiProduct.HeaderText = "Product In Marathi";
            this.MarathiProduct.Name = "MarathiProduct";
            this.MarathiProduct.Width = 220;
            // 
            // Updated
            // 
            this.Updated.HeaderText = "Updated";
            this.Updated.Name = "Updated";
            this.Updated.Visible = false;
            // 
            // ProductId
            // 
            this.ProductId.HeaderText = "ProductId";
            this.ProductId.Name = "ProductId";
            this.ProductId.Visible = false;
            // 
            // Unit
            // 
            this.Unit.HeaderText = "Stock (Unit)";
            this.Unit.Name = "Unit";
            this.Unit.ReadOnly = true;
            // 
            // OStock
            // 
            this.OStock.HeaderText = "Opening Stock";
            this.OStock.Name = "OStock";
            this.OStock.Width = 120;
            // 
            // UStock
            // 
            this.UStock.HeaderText = "updatestock";
            this.UStock.Name = "UStock";
            // 
            // DtlId
            // 
            this.DtlId.HeaderText = "DtlId";
            this.DtlId.Name = "DtlId";
            this.DtlId.Visible = false;
            // 
            // frmProductInMarathi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(794, 472);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmProductInMarathi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Products In Marathi";
            this.Load += new System.EventHandler(this.frmProductInMarathi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgProductInfo)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgProductInfo;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SrNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnglishProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn MarathiProduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn Updated;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn OStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn UStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn DtlId;
    }
}