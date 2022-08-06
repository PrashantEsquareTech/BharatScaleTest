namespace AIOInventorySystem.Desk.Forms
{
    partial class frmReminder
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
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.txtremmessage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReminderID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.grpReminderInfo = new System.Windows.Forms.GroupBox();
            this.tblltInfo = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnnew = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpList = new System.Windows.Forms.GroupBox();
            this.dtgvList = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompIdg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblltMain.SuspendLayout();
            this.grpReminderInfo.SuspendLayout();
            this.tblltInfo.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.grpList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimePicker2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker2.Location = new System.Drawing.Point(96, 58);
            this.dateTimePicker2.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(90, 25);
            this.dateTimePicker2.TabIndex = 2;
            this.dateTimePicker2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker2_KeyDown);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dateTimePicker1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(96, 30);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(90, 25);
            this.dateTimePicker1.TabIndex = 1;
            this.dateTimePicker1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker1_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(2, 58);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 24);
            this.label7.TabIndex = 22;
            this.label7.Text = "Time:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtremmessage
            // 
            this.txtremmessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtremmessage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtremmessage.ForeColor = System.Drawing.Color.Black;
            this.txtremmessage.Location = new System.Drawing.Point(284, 2);
            this.txtremmessage.Margin = new System.Windows.Forms.Padding(2);
            this.txtremmessage.Multiline = true;
            this.txtremmessage.Name = "txtremmessage";
            this.tblltInfo.SetRowSpan(this.txtremmessage, 3);
            this.txtremmessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtremmessage.Size = new System.Drawing.Size(186, 80);
            this.txtremmessage.TabIndex = 3;
            this.txtremmessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtremmessage_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(190, 2);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 24);
            this.label3.TabIndex = 10;
            this.label3.Text = "Message:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(2, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 24);
            this.label2.TabIndex = 9;
            this.label2.Text = "Date:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReminderID
            // 
            this.txtReminderID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReminderID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReminderID.ForeColor = System.Drawing.Color.Black;
            this.txtReminderID.Location = new System.Drawing.Point(96, 2);
            this.txtReminderID.Margin = new System.Windows.Forms.Padding(2);
            this.txtReminderID.Name = "txtReminderID";
            this.txtReminderID.ReadOnly = true;
            this.txtReminderID.Size = new System.Drawing.Size(90, 25);
            this.txtReminderID.TabIndex = 0;
            this.txtReminderID.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "Reminder ID:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(484, 36);
            this.label4.TabIndex = 41;
            this.label4.Text = "Reminder Information";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.grpReminderInfo, 0, 1);
            this.tblltMain.Controls.Add(this.label4, 0, 0);
            this.tblltMain.Controls.Add(this.grpList, 0, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tblltMain.Size = new System.Drawing.Size(484, 462);
            this.tblltMain.TabIndex = 0;
            // 
            // grpReminderInfo
            // 
            this.grpReminderInfo.Controls.Add(this.tblltInfo);
            this.grpReminderInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpReminderInfo.Location = new System.Drawing.Point(3, 39);
            this.grpReminderInfo.Name = "grpReminderInfo";
            this.grpReminderInfo.Size = new System.Drawing.Size(478, 141);
            this.grpReminderInfo.TabIndex = 0;
            this.grpReminderInfo.TabStop = false;
            this.grpReminderInfo.Text = "Reminder Information";
            // 
            // tblltInfo
            // 
            this.tblltInfo.ColumnCount = 4;
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblltInfo.Controls.Add(this.tblltButtons, 0, 3);
            this.tblltInfo.Controls.Add(this.dateTimePicker2, 1, 2);
            this.tblltInfo.Controls.Add(this.label1, 0, 0);
            this.tblltInfo.Controls.Add(this.dateTimePicker1, 1, 1);
            this.tblltInfo.Controls.Add(this.label7, 0, 2);
            this.tblltInfo.Controls.Add(this.txtReminderID, 1, 0);
            this.tblltInfo.Controls.Add(this.label3, 2, 0);
            this.tblltInfo.Controls.Add(this.txtremmessage, 3, 0);
            this.tblltInfo.Controls.Add(this.label2, 0, 1);
            this.tblltInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltInfo.Location = new System.Drawing.Point(3, 21);
            this.tblltInfo.Margin = new System.Windows.Forms.Padding(0);
            this.tblltInfo.Name = "tblltInfo";
            this.tblltInfo.RowCount = 4;
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 24F));
            this.tblltInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28F));
            this.tblltInfo.Size = new System.Drawing.Size(472, 117);
            this.tblltInfo.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 6;
            this.tblltInfo.SetColumnSpan(this.tblltButtons, 4);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tblltButtons.Controls.Add(this.btnnew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnAdd, 2, 0);
            this.tblltButtons.Controls.Add(this.btndelete, 3, 0);
            this.tblltButtons.Controls.Add(this.btnClose, 4, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 84);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(472, 33);
            this.tblltButtons.TabIndex = 4;
            // 
            // btnnew
            // 
            this.btnnew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnnew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnew.ForeColor = System.Drawing.Color.White;
            this.btnnew.Location = new System.Drawing.Point(80, 2);
            this.btnnew.Margin = new System.Windows.Forms.Padding(2);
            this.btnnew.Name = "btnnew";
            this.btnnew.Size = new System.Drawing.Size(74, 29);
            this.btnnew.TabIndex = 5;
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
            this.btnAdd.Location = new System.Drawing.Point(158, 2);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(74, 29);
            this.btnAdd.TabIndex = 4;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btndelete
            // 
            this.btndelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btndelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btndelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btndelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.ForeColor = System.Drawing.Color.White;
            this.btndelete.Location = new System.Drawing.Point(236, 2);
            this.btndelete.Margin = new System.Windows.Forms.Padding(2);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(74, 29);
            this.btndelete.TabIndex = 6;
            this.btndelete.Text = "Delete";
            this.btndelete.UseVisualStyleBackColor = false;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(314, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(74, 29);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.dtgvList);
            this.grpList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpList.Location = new System.Drawing.Point(3, 186);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(478, 273);
            this.grpList.TabIndex = 8;
            this.grpList.TabStop = false;
            this.grpList.Text = "Reminder List";
            // 
            // dtgvList
            // 
            this.dtgvList.AllowUserToAddRows = false;
            this.dtgvList.AllowUserToDeleteRows = false;
            this.dtgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgvList.BackgroundColor = System.Drawing.Color.White;
            this.dtgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.RemID,
            this.CreateDate,
            this.Date,
            this.Time,
            this.Message,
            this.CompIdg});
            this.dtgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgvList.Location = new System.Drawing.Point(3, 21);
            this.dtgvList.Margin = new System.Windows.Forms.Padding(2);
            this.dtgvList.Name = "dtgvList";
            this.dtgvList.RowHeadersWidth = 15;
            this.dtgvList.Size = new System.Drawing.Size(472, 249);
            this.dtgvList.TabIndex = 8;
            this.dtgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgvList_CellClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // RemID
            // 
            this.RemID.FillWeight = 52.28427F;
            this.RemID.HeaderText = "RemID";
            this.RemID.Name = "RemID";
            // 
            // CreateDate
            // 
            this.CreateDate.FillWeight = 86.92893F;
            this.CreateDate.HeaderText = "Create Date";
            this.CreateDate.Name = "CreateDate";
            // 
            // Date
            // 
            this.Date.FillWeight = 86.92893F;
            this.Date.HeaderText = "Date";
            this.Date.Name = "Date";
            // 
            // Time
            // 
            this.Time.FillWeight = 86.92893F;
            this.Time.HeaderText = "Time";
            this.Time.Name = "Time";
            // 
            // Message
            // 
            this.Message.FillWeight = 86.92893F;
            this.Message.HeaderText = "Message";
            this.Message.Name = "Message";
            // 
            // CompIdg
            // 
            this.CompIdg.HeaderText = "CompId";
            this.CompIdg.Name = "CompIdg";
            this.CompIdg.Visible = false;
            // 
            // frmReminder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(484, 462);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmReminder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reminder Information";
            this.Load += new System.EventHandler(this.frmReminder_Load);
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.grpReminderInfo.ResumeLayout(false);
            this.tblltInfo.ResumeLayout(false);
            this.tblltInfo.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.grpList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtremmessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtReminderID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.GroupBox grpReminderInfo;
        private System.Windows.Forms.TableLayoutPanel tblltInfo;
        private System.Windows.Forms.GroupBox grpList;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnnew;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dtgvList;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn RemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.DataGridViewTextBoxColumn Message;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompIdg;
    }
}