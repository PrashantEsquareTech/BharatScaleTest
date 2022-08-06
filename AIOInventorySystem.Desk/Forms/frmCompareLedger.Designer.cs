namespace AIOInventorySystem.Desk.Forms
{
    partial class frmCompareLedger
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbcustomer = new System.Windows.Forms.ComboBox();
            this.cmbsupplier = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.GvRemainingpayment = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tblltMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvRemainingpayment)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Left;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(316, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 31);
            this.label4.TabIndex = 101;
            this.label4.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(652, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 31);
            this.label3.TabIndex = 100;
            this.label3.Text = "*";
            // 
            // cmbcustomer
            // 
            this.cmbcustomer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcustomer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbcustomer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcustomer.FormattingEnabled = true;
            this.cmbcustomer.Location = new System.Drawing.Point(119, 42);
            this.cmbcustomer.Margin = new System.Windows.Forms.Padding(2);
            this.cmbcustomer.Name = "cmbcustomer";
            this.cmbcustomer.Size = new System.Drawing.Size(192, 25);
            this.cmbcustomer.TabIndex = 1;
            this.cmbcustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbcustomer_KeyDown);
            this.cmbcustomer.Leave += new System.EventHandler(this.cmbcustomer_Leave);
            // 
            // cmbsupplier
            // 
            this.cmbsupplier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbsupplier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbsupplier.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbsupplier.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbsupplier.FormattingEnabled = true;
            this.cmbsupplier.Location = new System.Drawing.Point(455, 42);
            this.cmbsupplier.Margin = new System.Windows.Forms.Padding(2);
            this.cmbsupplier.Name = "cmbsupplier";
            this.cmbsupplier.Size = new System.Drawing.Size(192, 25);
            this.cmbsupplier.TabIndex = 2;
            this.cmbsupplier.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbsupplier_KeyDown);
            this.cmbsupplier.Leave += new System.EventHandler(this.cmbsupplier_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(338, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 27);
            this.label1.TabIndex = 21;
            this.label1.Text = "Supplier Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label5, 8);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(784, 40);
            this.label5.TabIndex = 36;
            this.label5.Text = "Compare Ledger";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 8;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.Controls.Add(this.GvRemainingpayment, 0, 2);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.label3, 5, 1);
            this.tblltMain.Controls.Add(this.label4, 2, 1);
            this.tblltMain.Controls.Add(this.cmbsupplier, 4, 1);
            this.tblltMain.Controls.Add(this.cmbcustomer, 1, 1);
            this.tblltMain.Controls.Add(this.label1, 3, 1);
            this.tblltMain.Controls.Add(this.label2, 0, 1);
            this.tblltMain.Controls.Add(this.btnSearch, 7, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77F));
            this.tblltMain.Size = new System.Drawing.Size(784, 312);
            this.tblltMain.TabIndex = 0;
            // 
            // GvRemainingpayment
            // 
            this.GvRemainingpayment.AllowUserToAddRows = false;
            this.GvRemainingpayment.AllowUserToDeleteRows = false;
            this.GvRemainingpayment.BackgroundColor = System.Drawing.Color.White;
            this.GvRemainingpayment.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvRemainingpayment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.GvRemainingpayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblltMain.SetColumnSpan(this.GvRemainingpayment, 8);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GvRemainingpayment.DefaultCellStyle = dataGridViewCellStyle2;
            this.GvRemainingpayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvRemainingpayment.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvRemainingpayment.Location = new System.Drawing.Point(2, 73);
            this.GvRemainingpayment.Margin = new System.Windows.Forms.Padding(2);
            this.GvRemainingpayment.Name = "GvRemainingpayment";
            this.GvRemainingpayment.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.GvRemainingpayment.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.GvRemainingpayment.RowHeadersWidth = 10;
            this.GvRemainingpayment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GvRemainingpayment.Size = new System.Drawing.Size(780, 237);
            this.GvRemainingpayment.TabIndex = 4;
            this.GvRemainingpayment.TabStop = false;
            this.GvRemainingpayment.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvRemainingpayment_CellClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 27);
            this.label2.TabIndex = 102;
            this.label2.Text = "Customer Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(721, 42);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(61, 27);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // frmCompareLedger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(784, 312);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "frmCompareLedger";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compare Ledger";
            this.Load += new System.EventHandler(this.frmCompareLedger_Load);
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvRemainingpayment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbsupplier;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbcustomer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView GvRemainingpayment;
        private System.Windows.Forms.Button btnSearch;
    }
}