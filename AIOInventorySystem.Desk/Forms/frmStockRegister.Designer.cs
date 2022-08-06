namespace AIOInventorySystem.Desk.Forms
{
    partial class frmStockRegister
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
            this.dtgStock = new System.Windows.Forms.DataGridView();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProduct = new System.Windows.Forms.TextBox();
            this.chkMonthwise = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dtgStock)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgStock
            // 
            this.dtgStock.AllowUserToAddRows = false;
            this.dtgStock.AllowUserToDeleteRows = false;
            this.dtgStock.BackgroundColor = System.Drawing.Color.White;
            this.dtgStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblltMain.SetColumnSpan(this.dtgStock, 6);
            this.dtgStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgStock.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgStock.Location = new System.Drawing.Point(3, 97);
            this.dtgStock.Name = "dtgStock";
            this.dtgStock.ReadOnly = true;
            this.dtgStock.Size = new System.Drawing.Size(738, 352);
            this.dtgStock.TabIndex = 8;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Location = new System.Drawing.Point(262, 2);
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(115, 25);
            this.dtpFrom.TabIndex = 2;
            this.dtpFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpFrom_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(158, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Date From";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpTo
            // 
            this.dtpTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Location = new System.Drawing.Point(410, 2);
            this.dtpTo.Margin = new System.Windows.Forms.Padding(2);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(115, 25);
            this.dtpTo.TabIndex = 3;
            this.dtpTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtpTo_KeyDown);
            this.dtpTo.Leave += new System.EventHandler(this.dtpTo_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(381, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "To";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(299, 67);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 25);
            this.label4.TabIndex = 4;
            this.label4.Text = "Product:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtProduct
            // 
            this.txtProduct.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtProduct.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtProduct.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProduct.Location = new System.Drawing.Point(373, 67);
            this.txtProduct.Margin = new System.Windows.Forms.Padding(2);
            this.txtProduct.Name = "txtProduct";
            this.txtProduct.Size = new System.Drawing.Size(219, 25);
            this.txtProduct.TabIndex = 5;
            this.txtProduct.Enter += new System.EventHandler(this.txtProduct_Enter);
            this.txtProduct.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProduct_KeyDown);
            this.txtProduct.Leave += new System.EventHandler(this.txtProduct_Leave);
            // 
            // chkMonthwise
            // 
            this.chkMonthwise.AutoSize = true;
            this.chkMonthwise.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkMonthwise.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMonthwise.Location = new System.Drawing.Point(5, 2);
            this.chkMonthwise.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkMonthwise.Name = "chkMonthwise";
            this.chkMonthwise.Size = new System.Drawing.Size(149, 25);
            this.chkMonthwise.TabIndex = 1;
            this.chkMonthwise.Text = "Display Monthwise";
            this.chkMonthwise.UseVisualStyleBackColor = true;
            this.chkMonthwise.CheckedChanged += new System.EventHandler(this.chkMonthwise_CheckedChanged);
            this.chkMonthwise.KeyDown += new System.Windows.Forms.KeyEventHandler(this.chkMonthwise_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 25);
            this.label1.TabIndex = 10;
            this.label1.Text = "Category:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbCategory
            // 
            this.cmbCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCategory.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(76, 67);
            this.cmbCategory.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(219, 25);
            this.cmbCategory.TabIndex = 4;
            this.cmbCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCategory_KeyDown);
            this.cmbCategory.Leave += new System.EventHandler(this.cmbCategory_Leave);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(596, 67);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(70, 25);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(670, 67);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(72, 25);
            this.btnPrint.TabIndex = 7;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 6;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.Controls.Add(this.label8, 0, 0);
            this.tblltMain.Controls.Add(this.dtgStock, 0, 3);
            this.tblltMain.Controls.Add(this.btnPrint, 5, 2);
            this.tblltMain.Controls.Add(this.btnSearch, 4, 2);
            this.tblltMain.Controls.Add(this.txtProduct, 3, 2);
            this.tblltMain.Controls.Add(this.cmbCategory, 1, 2);
            this.tblltMain.Controls.Add(this.label4, 2, 2);
            this.tblltMain.Controls.Add(this.label1, 0, 2);
            this.tblltMain.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 79F));
            this.tblltMain.Size = new System.Drawing.Size(744, 452);
            this.tblltMain.TabIndex = 0;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label8, 6);
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(744, 36);
            this.label8.TabIndex = 38;
            this.label8.Text = "Stock Register";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tblltMain.SetColumnSpan(this.tableLayoutPanel1, 6);
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29F));
            this.tableLayoutPanel1.Controls.Add(this.chkMonthwise, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpFrom, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.dtpTo, 4, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 36);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(744, 29);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // frmStockRegister
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(744, 452);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmStockRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock Register";
            this.Load += new System.EventHandler(this.frmStockRegister_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgStock)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgStock;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProduct;
        private System.Windows.Forms.CheckBox chkMonthwise;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}