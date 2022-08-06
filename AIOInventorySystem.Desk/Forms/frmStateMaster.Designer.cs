namespace AIOInventorySystem.Desk.Forms
{
    partial class frmStateMaster
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
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.grpInfo = new System.Windows.Forms.GroupBox();
            this.tblltInfo = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStateName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStateID = new System.Windows.Forms.TextBox();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.tblltList = new System.Windows.Forms.TableLayoutPanel();
            this.dtgvList = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StateIDg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StateName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTotaluUnits = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tblltMain.SuspendLayout();
            this.grpInfo.SuspendLayout();
            this.tblltInfo.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.grpList.SuspendLayout();
            this.tblltList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.grpInfo, 0, 1);
            this.tblltMain.Controls.Add(this.grpList, 0, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62F));
            this.tblltMain.Size = new System.Drawing.Size(384, 392);
            this.tblltMain.TabIndex = 0;
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
            this.label5.Size = new System.Drawing.Size(384, 35);
            this.label5.TabIndex = 11;
            this.label5.Text = "State Information";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpInfo
            // 
            this.grpInfo.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.grpInfo.Controls.Add(this.tblltInfo);
            this.grpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInfo.ForeColor = System.Drawing.Color.Black;
            this.grpInfo.Location = new System.Drawing.Point(2, 37);
            this.grpInfo.Margin = new System.Windows.Forms.Padding(2);
            this.grpInfo.Name = "grpInfo";
            this.grpInfo.Padding = new System.Windows.Forms.Padding(2);
            this.grpInfo.Size = new System.Drawing.Size(380, 109);
            this.grpInfo.TabIndex = 0;
            this.grpInfo.TabStop = false;
            this.grpInfo.Text = "State Information";
            // 
            // tblltInfo
            // 
            this.tblltInfo.ColumnCount = 2;
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tblltInfo.Controls.Add(this.tblltButtons, 0, 2);
            this.tblltInfo.Controls.Add(this.label1, 0, 0);
            this.tblltInfo.Controls.Add(this.txtStateName, 1, 1);
            this.tblltInfo.Controls.Add(this.label2, 0, 1);
            this.tblltInfo.Controls.Add(this.txtStateID, 1, 0);
            this.tblltInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltInfo.Location = new System.Drawing.Point(2, 20);
            this.tblltInfo.Margin = new System.Windows.Forms.Padding(0);
            this.tblltInfo.Name = "tblltInfo";
            this.tblltInfo.RowCount = 3;
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36F));
            this.tblltInfo.Size = new System.Drawing.Size(376, 87);
            this.tblltInfo.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 6;
            this.tblltInfo.SetColumnSpan(this.tblltButtons, 2);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltButtons.Controls.Add(this.btnnew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnAdd, 2, 0);
            this.tblltButtons.Controls.Add(this.btnDelete, 3, 0);
            this.tblltButtons.Controls.Add(this.btnClose, 4, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 54);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(376, 33);
            this.tblltButtons.TabIndex = 2;
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(54, 2);
            this.btnnew.Margin = new System.Windows.Forms.Padding(2);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(63, 29);
            this.btnnew.TabIndex = 3;
            this.btnnew.Text = "New";
            this.btnnew.UseVisualStyleBackColor = false;
            this.btnnew.Click += new System.EventHandler(this.btnnew_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(121, 2);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(63, 29);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(188, 2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(63, 29);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(255, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(63, 29);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 23);
            this.label1.TabIndex = 15;
            this.label1.Text = "State ID:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStateName
            // 
            this.txtStateName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtStateName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtStateName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStateName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStateName.ForeColor = System.Drawing.Color.Black;
            this.txtStateName.Location = new System.Drawing.Point(114, 29);
            this.txtStateName.Margin = new System.Windows.Forms.Padding(2);
            this.txtStateName.Name = "txtStateName";
            this.txtStateName.Size = new System.Drawing.Size(260, 25);
            this.txtStateName.TabIndex = 1;
            this.txtStateName.TextChanged += new System.EventHandler(this.txtUnitName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(2, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 23);
            this.label2.TabIndex = 17;
            this.label2.Text = "State Name:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStateID
            // 
            this.txtStateID.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtStateID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStateID.ForeColor = System.Drawing.Color.Black;
            this.txtStateID.Location = new System.Drawing.Point(114, 2);
            this.txtStateID.Margin = new System.Windows.Forms.Padding(2);
            this.txtStateID.Name = "txtStateID";
            this.txtStateID.ReadOnly = true;
            this.txtStateID.Size = new System.Drawing.Size(120, 25);
            this.txtStateID.TabIndex = 0;
            this.txtStateID.TabStop = false;
            // 
            // grpList
            // 
            this.grpList.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this.grpList.Controls.Add(this.tblltList);
            this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpList.ForeColor = System.Drawing.Color.Black;
            this.grpList.Location = new System.Drawing.Point(2, 150);
            this.grpList.Margin = new System.Windows.Forms.Padding(2);
            this.grpList.Name = "grpList";
            this.grpList.Padding = new System.Windows.Forms.Padding(2);
            this.grpList.Size = new System.Drawing.Size(380, 240);
            this.grpList.TabIndex = 6;
            this.grpList.TabStop = false;
            this.grpList.Text = "State List";
            // 
            // tblltList
            // 
            this.tblltList.ColumnCount = 2;
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tblltList.Controls.Add(this.dtgvList, 0, 1);
            this.tblltList.Controls.Add(this.lblTotaluUnits, 1, 0);
            this.tblltList.Controls.Add(this.label4, 0, 0);
            this.tblltList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltList.Location = new System.Drawing.Point(2, 20);
            this.tblltList.Margin = new System.Windows.Forms.Padding(0);
            this.tblltList.Name = "tblltList";
            this.tblltList.RowCount = 2;
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            this.tblltList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 87F));
            this.tblltList.Size = new System.Drawing.Size(376, 218);
            this.tblltList.TabIndex = 6;
            // 
            // dtgvList
            // 
            this.dtgvList.AllowUserToAddRows = false;
            this.dtgvList.AllowUserToDeleteRows = false;
            this.dtgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvList.BackgroundColor = System.Drawing.Color.White;
            this.dtgvList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dtgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.StateIDg,
            this.StateName});
            this.tblltList.SetColumnSpan(this.dtgvList, 2);
            this.dtgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgvList.Location = new System.Drawing.Point(3, 32);
            this.dtgvList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dtgvList.Name = "dtgvList";
            this.dtgvList.ReadOnly = true;
            this.dtgvList.RowHeadersWidth = 20;
            this.dtgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgvList.Size = new System.Drawing.Size(370, 182);
            this.dtgvList.TabIndex = 6;
            this.dtgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvList_CellClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // StateIDg
            // 
            this.StateIDg.FillWeight = 47.71573F;
            this.StateIDg.HeaderText = "State ID";
            this.StateIDg.Name = "StateIDg";
            this.StateIDg.ReadOnly = true;
            // 
            // StateName
            // 
            this.StateName.FillWeight = 152.2843F;
            this.StateName.HeaderText = "State Name";
            this.StateName.Name = "StateName";
            this.StateName.ReadOnly = true;
            // 
            // lblTotaluUnits
            // 
            this.lblTotaluUnits.AutoSize = true;
            this.lblTotaluUnits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotaluUnits.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotaluUnits.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTotaluUnits.Location = new System.Drawing.Point(114, 2);
            this.lblTotaluUnits.Margin = new System.Windows.Forms.Padding(2);
            this.lblTotaluUnits.Name = "lblTotaluUnits";
            this.lblTotaluUnits.Size = new System.Drawing.Size(260, 24);
            this.lblTotaluUnits.TabIndex = 48;
            this.lblTotaluUnits.Text = "0";
            this.lblTotaluUnits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(2, 2);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 24);
            this.label4.TabIndex = 47;
            this.label4.Text = "Total States:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmStateMaster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(384, 392);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmStateMaster";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "State Information";
            this.Load += new System.EventHandler(this.frmUnitInformation_Load);
            this.tblltMain.ResumeLayout(false);
            this.grpInfo.ResumeLayout(false);
            this.tblltInfo.ResumeLayout(false);
            this.tblltInfo.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.grpList.ResumeLayout(false);
            this.tblltList.ResumeLayout(false);
            this.tblltList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.GroupBox grpInfo;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.TableLayoutPanel tblltList;
        private System.Windows.Forms.Label lblTotaluUnits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtStateID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStateName;
        private System.Windows.Forms.TableLayoutPanel tblltInfo;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dtgvList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn StateIDg;
        private System.Windows.Forms.DataGridViewTextBoxColumn StateName;
    }
}