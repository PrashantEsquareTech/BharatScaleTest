namespace AIOInventorySystem.Desk.Forms
{
    partial class frmRemainingpayment
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
            this.lblTotalMount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCustomerCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbVillage = new System.Windows.Forms.ComboBox();
            this.GvRemainingpayment = new System.Windows.Forms.DataGridView();
            this.chkCustomerType = new System.Windows.Forms.CheckBox();
            this.cmbCustomerType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtptodate = new System.Windows.Forms.DateTimePicker();
            this.label19 = new System.Windows.Forms.Label();
            this.dtpfromdate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btngetall = new System.Windows.Forms.Button();
            this.btnprint = new System.Windows.Forms.Button();
            this.btncreditlist = new System.Windows.Forms.Button();
            this.btnsearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GvRemainingpayment)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTotalMount
            // 
            this.lblTotalMount.AutoSize = true;
            this.lblTotalMount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalMount.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalMount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotalMount.Location = new System.Drawing.Point(412, 2);
            this.lblTotalMount.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotalMount.Name = "lblTotalMount";
            this.lblTotalMount.Size = new System.Drawing.Size(96, 26);
            this.lblTotalMount.TabIndex = 176;
            this.lblTotalMount.Text = "---";
            this.lblTotalMount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(262, 2);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 26);
            this.label2.TabIndex = 175;
            this.label2.Text = "Total Amount:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCustomerCount
            // 
            this.lblCustomerCount.AutoSize = true;
            this.lblCustomerCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCustomerCount.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblCustomerCount.Location = new System.Drawing.Point(162, 2);
            this.lblCustomerCount.Margin = new System.Windows.Forms.Padding(2);
            this.lblCustomerCount.Name = "lblCustomerCount";
            this.lblCustomerCount.Size = new System.Drawing.Size(96, 26);
            this.lblCustomerCount.TabIndex = 174;
            this.lblCustomerCount.Text = "---";
            this.lblCustomerCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(2, 2);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(156, 26);
            this.label7.TabIndex = 173;
            this.label7.Text = "Total Customers:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label5, 10);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(925, 35);
            this.label5.TabIndex = 36;
            this.label5.Text = "Customer Credits";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbVillage
            // 
            this.cmbVillage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbVillage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbVillage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbVillage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbVillage.FormattingEnabled = true;
            this.cmbVillage.Location = new System.Drawing.Point(108, 37);
            this.cmbVillage.Margin = new System.Windows.Forms.Padding(2);
            this.cmbVillage.Name = "cmbVillage";
            this.cmbVillage.Size = new System.Drawing.Size(176, 36);
            this.cmbVillage.TabIndex = 0;
            this.cmbVillage.SelectedIndexChanged += new System.EventHandler(this.cmbVillage_SelectedIndexChanged);
            // 
            // GvRemainingpayment
            // 
            this.GvRemainingpayment.AllowUserToAddRows = false;
            this.GvRemainingpayment.AllowUserToDeleteRows = false;
            this.GvRemainingpayment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GvRemainingpayment.BackgroundColor = System.Drawing.Color.White;
            this.GvRemainingpayment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblltMain.SetColumnSpan(this.GvRemainingpayment, 10);
            this.GvRemainingpayment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvRemainingpayment.GridColor = System.Drawing.Color.WhiteSmoke;
            this.GvRemainingpayment.Location = new System.Drawing.Point(3, 98);
            this.GvRemainingpayment.Name = "GvRemainingpayment";
            this.GvRemainingpayment.RowHeadersWidth = 15;
            this.GvRemainingpayment.Size = new System.Drawing.Size(919, 411);
            this.GvRemainingpayment.TabIndex = 10;
            this.GvRemainingpayment.TabStop = false;
            this.GvRemainingpayment.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GvRemainingpayment_CellClick);
            // 
            // chkCustomerType
            // 
            this.chkCustomerType.AutoSize = true;
            this.chkCustomerType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkCustomerType.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCustomerType.Location = new System.Drawing.Point(651, 37);
            this.chkCustomerType.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkCustomerType.Name = "chkCustomerType";
            this.chkCustomerType.Size = new System.Drawing.Size(122, 26);
            this.chkCustomerType.TabIndex = 3;
            this.chkCustomerType.Text = "Customer Type";
            this.chkCustomerType.UseVisualStyleBackColor = true;
            // 
            // cmbCustomerType
            // 
            this.cmbCustomerType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbCustomerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomerType.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCustomerType.FormattingEnabled = true;
            this.cmbCustomerType.Items.AddRange(new object[] {
            "Retailer",
            "Dealer"});
            this.cmbCustomerType.Location = new System.Drawing.Point(777, 37);
            this.cmbCustomerType.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCustomerType.Name = "cmbCustomerType";
            this.cmbCustomerType.Size = new System.Drawing.Size(146, 36);
            this.cmbCustomerType.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(288, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 26);
            this.label1.TabIndex = 188;
            this.label1.Text = "From Date:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtptodate
            // 
            this.dtptodate.CustomFormat = "dd/MM/yyyy";
            this.dtptodate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtptodate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtptodate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtptodate.Location = new System.Drawing.Point(537, 37);
            this.dtptodate.Margin = new System.Windows.Forms.Padding(2);
            this.dtptodate.Name = "dtptodate";
            this.dtptodate.Size = new System.Drawing.Size(107, 33);
            this.dtptodate.TabIndex = 2;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(500, 37);
            this.label19.Margin = new System.Windows.Forms.Padding(2);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(33, 26);
            this.label19.TabIndex = 186;
            this.label19.Text = "To";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpfromdate
            // 
            this.dtpfromdate.CustomFormat = "dd/MM/yyyy";
            this.dtpfromdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpfromdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpfromdate.Location = new System.Drawing.Point(389, 37);
            this.dtpfromdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpfromdate.Name = "dtpfromdate";
            this.dtpfromdate.Size = new System.Drawing.Size(107, 33);
            this.dtpfromdate.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 37);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 26);
            this.label3.TabIndex = 193;
            this.label3.Text = "Village Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 8;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltMain.Controls.Add(this.tblltButtons, 0, 2);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.label3, 0, 1);
            this.tblltMain.Controls.Add(this.cmbVillage, 1, 1);
            this.tblltMain.Controls.Add(this.label1, 2, 1);
            this.tblltMain.Controls.Add(this.dtpfromdate, 3, 1);
            this.tblltMain.Controls.Add(this.label19, 4, 1);
            this.tblltMain.Controls.Add(this.dtptodate, 5, 1);
            this.tblltMain.Controls.Add(this.chkCustomerType, 6, 1);
            this.tblltMain.Controls.Add(this.cmbCustomerType, 7, 1);
            this.tblltMain.Controls.Add(this.GvRemainingpayment, 0, 3);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81F));
            this.tblltMain.Size = new System.Drawing.Size(925, 512);
            this.tblltMain.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 9;
            this.tblltMain.SetColumnSpan(this.tblltButtons, 10);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.3913F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.86957F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.30435F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.86957F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.347826F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.695652F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.695652F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.695652F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.13043F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblltButtons.Controls.Add(this.btngetall, 6, 0);
            this.tblltButtons.Controls.Add(this.btnprint, 7, 0);
            this.tblltButtons.Controls.Add(this.btncreditlist, 8, 0);
            this.tblltButtons.Controls.Add(this.label7, 0, 0);
            this.tblltButtons.Controls.Add(this.lblCustomerCount, 1, 0);
            this.tblltButtons.Controls.Add(this.label2, 2, 0);
            this.tblltButtons.Controls.Add(this.lblTotalMount, 3, 0);
            this.tblltButtons.Controls.Add(this.btnsearch, 5, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 65);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(925, 30);
            this.tblltButtons.TabIndex = 5;
            // 
            // btngetall
            // 
            this.btngetall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btngetall.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btngetall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btngetall.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngetall.ForeColor = System.Drawing.Color.White;
            this.btngetall.Location = new System.Drawing.Point(632, 2);
            this.btngetall.Margin = new System.Windows.Forms.Padding(2);
            this.btngetall.Name = "btngetall";
            this.btngetall.Size = new System.Drawing.Size(76, 26);
            this.btngetall.TabIndex = 6;
            this.btngetall.Text = "GetAll";
            this.btngetall.UseVisualStyleBackColor = false;
            this.btngetall.Click += new System.EventHandler(this.btngetall_Click);
            // 
            // btnprint
            // 
            this.btnprint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnprint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnprint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnprint.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprint.ForeColor = System.Drawing.Color.White;
            this.btnprint.Location = new System.Drawing.Point(712, 2);
            this.btnprint.Margin = new System.Windows.Forms.Padding(2);
            this.btnprint.Name = "btnprint";
            this.btnprint.Size = new System.Drawing.Size(76, 26);
            this.btnprint.TabIndex = 7;
            this.btnprint.Text = "Print";
            this.btnprint.UseVisualStyleBackColor = false;
            this.btnprint.Click += new System.EventHandler(this.btnprint_Click);
            // 
            // btncreditlist
            // 
            this.btncreditlist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btncreditlist.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btncreditlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btncreditlist.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncreditlist.ForeColor = System.Drawing.Color.White;
            this.btncreditlist.Location = new System.Drawing.Point(792, 2);
            this.btncreditlist.Margin = new System.Windows.Forms.Padding(2);
            this.btncreditlist.Name = "btncreditlist";
            this.btncreditlist.Size = new System.Drawing.Size(131, 26);
            this.btncreditlist.TabIndex = 9;
            this.btncreditlist.Text = "Credit Day List";
            this.btncreditlist.UseVisualStyleBackColor = false;
            this.btncreditlist.Click += new System.EventHandler(this.btncreditlist_Click);
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.Color.White;
            this.btnsearch.Location = new System.Drawing.Point(552, 2);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(76, 26);
            this.btnsearch.TabIndex = 5;
            this.btnsearch.Text = "Search";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // frmRemainingpayment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(925, 512);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "frmRemainingpayment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Remaining Payment List";
            this.Load += new System.EventHandler(this.frmRemainingpayment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GvRemainingpayment)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.tblltButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCustomerCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblTotalMount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView GvRemainingpayment;
        private System.Windows.Forms.ComboBox cmbVillage;
        private System.Windows.Forms.CheckBox chkCustomerType;
        private System.Windows.Forms.ComboBox cmbCustomerType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtptodate;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker dtpfromdate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btngetall;
        private System.Windows.Forms.Button btnprint;
        private System.Windows.Forms.Button btncreditlist;
        private System.Windows.Forms.Button btnsearch;
    }
}