namespace AIOInventorySystem.Desk.Forms
{
    partial class frmTaxSlab
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
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.cmbGst = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dtgTaxSlab = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FromAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToAmt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.label9 = new System.Windows.Forms.Label();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgTaxSlab)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 66);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Amount:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(192, 66);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "To Amount:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "ID:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtID
            // 
            this.txtID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.Location = new System.Drawing.Point(97, 38);
            this.txtID.Margin = new System.Windows.Forms.Padding(2);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(91, 25);
            this.txtID.TabIndex = 1;
            this.txtID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtID_KeyDown);
            // 
            // txtFrom
            // 
            this.txtFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFrom.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrom.Location = new System.Drawing.Point(97, 66);
            this.txtFrom.Margin = new System.Windows.Forms.Padding(2);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(91, 25);
            this.txtFrom.TabIndex = 3;
            this.txtFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFrom_KeyDown);
            this.txtFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFrom_KeyPress);
            // 
            // txtDescription
            // 
            this.tblltMain.SetColumnSpan(this.txtDescription, 3);
            this.txtDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(287, 38);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(2);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(285, 25);
            this.txtDescription.TabIndex = 2;
            this.txtDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescription_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(192, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "Description:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTo
            // 
            this.txtTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTo.Location = new System.Drawing.Point(287, 66);
            this.txtTo.Margin = new System.Windows.Forms.Padding(2);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(91, 25);
            this.txtTo.TabIndex = 4;
            this.txtTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTo_KeyDown);
            this.txtTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTo_KeyPress);
            // 
            // cmbGst
            // 
            this.cmbGst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbGst.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGst.FormattingEnabled = true;
            this.cmbGst.Items.AddRange(new object[] {
            "0",
            "5",
            "12",
            "18",
            "28"});
            this.cmbGst.Location = new System.Drawing.Point(477, 66);
            this.cmbGst.Margin = new System.Windows.Forms.Padding(2);
            this.cmbGst.Name = "cmbGst";
            this.cmbGst.Size = new System.Drawing.Size(95, 25);
            this.cmbGst.TabIndex = 5;
            this.cmbGst.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbGst_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(382, 66);
            this.label5.Margin = new System.Windows.Forms.Padding(2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 24);
            this.label5.TabIndex = 14;
            this.label5.Text = "GST (%):";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtgTaxSlab
            // 
            this.dtgTaxSlab.AllowUserToAddRows = false;
            this.dtgTaxSlab.AllowUserToDeleteRows = false;
            this.dtgTaxSlab.BackgroundColor = System.Drawing.Color.White;
            this.dtgTaxSlab.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgTaxSlab.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Description,
            this.FromAmt,
            this.ToAmt,
            this.GST});
            this.tblltMain.SetColumnSpan(this.dtgTaxSlab, 6);
            this.dtgTaxSlab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgTaxSlab.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgTaxSlab.Location = new System.Drawing.Point(3, 131);
            this.dtgTaxSlab.Name = "dtgTaxSlab";
            this.dtgTaxSlab.ReadOnly = true;
            this.dtgTaxSlab.RowHeadersWidth = 15;
            this.dtgTaxSlab.Size = new System.Drawing.Size(568, 148);
            this.dtgTaxSlab.TabIndex = 11;
            this.dtgTaxSlab.TabStop = false;
            this.dtgTaxSlab.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgTaxSlab_CellClick);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 50;
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 180;
            // 
            // FromAmt
            // 
            this.FromAmt.HeaderText = "From Amount";
            this.FromAmt.Name = "FromAmt";
            this.FromAmt.ReadOnly = true;
            this.FromAmt.Width = 120;
            // 
            // ToAmt
            // 
            this.ToAmt.HeaderText = "To Amount";
            this.ToAmt.Name = "ToAmt";
            this.ToAmt.ReadOnly = true;
            this.ToAmt.Width = 120;
            // 
            // GST
            // 
            this.GST.HeaderText = "GST(%)";
            this.GST.Name = "GST";
            this.GST.ReadOnly = true;
            this.GST.Width = 90;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 6;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltMain.Controls.Add(this.label9, 0, 0);
            this.tblltMain.Controls.Add(this.tblltButtons, 0, 3);
            this.tblltMain.Controls.Add(this.label2, 0, 1);
            this.tblltMain.Controls.Add(this.dtgTaxSlab, 0, 4);
            this.tblltMain.Controls.Add(this.txtID, 1, 1);
            this.tblltMain.Controls.Add(this.cmbGst, 5, 2);
            this.tblltMain.Controls.Add(this.label5, 4, 2);
            this.tblltMain.Controls.Add(this.label4, 2, 1);
            this.tblltMain.Controls.Add(this.txtDescription, 3, 1);
            this.tblltMain.Controls.Add(this.label1, 0, 2);
            this.tblltMain.Controls.Add(this.txtFrom, 1, 2);
            this.tblltMain.Controls.Add(this.label3, 2, 2);
            this.tblltMain.Controls.Add(this.txtTo, 3, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 5;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 54F));
            this.tblltMain.Size = new System.Drawing.Size(574, 282);
            this.tblltMain.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label9, 6);
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(574, 36);
            this.label9.TabIndex = 37;
            this.label9.Text = "Tax Slab Information";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 7;
            this.tblltMain.SetColumnSpan(this.tblltButtons, 6);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.Controls.Add(this.btnNew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnSave, 2, 0);
            this.tblltButtons.Controls.Add(this.btnclose, 5, 0);
            this.tblltButtons.Controls.Add(this.btnDelete, 4, 0);
            this.tblltButtons.Controls.Add(this.btnUpdate, 3, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 92);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(574, 36);
            this.tblltButtons.TabIndex = 6;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(83, 2);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(77, 32);
            this.btnNew.TabIndex = 7;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(164, 2);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(77, 32);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnclose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnclose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(407, 2);
            this.btnclose.Margin = new System.Windows.Forms.Padding(2);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(77, 32);
            this.btnclose.TabIndex = 10;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(326, 2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(77, 32);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.ForeColor = System.Drawing.Color.White;
            this.btnUpdate.Location = new System.Drawing.Point(245, 2);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(77, 32);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // frmTaxSlab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(574, 282);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmTaxSlab";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tax Slab Master";
            ((System.ComponentModel.ISupportInitialize)(this.dtgTaxSlab)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.ComboBox cmbGst;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dtgTaxSlab;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn FromAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn ToAmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn GST;
        private System.Windows.Forms.Label label9;
    }
}