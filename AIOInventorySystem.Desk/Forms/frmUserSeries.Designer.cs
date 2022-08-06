namespace AIOInventorySystem.Desk.Forms
{
    partial class frmUserSeries
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
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.txtSeparator = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.txtStartNumber = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txtStartChar = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.dtgUserSeries = new System.Windows.Forms.DataGridView();
            this.StartCharacter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Separator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartByNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CompId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgUserSeries)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label33.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.Color.Crimson;
            this.label33.Location = new System.Drawing.Point(270, 91);
            this.label33.Margin = new System.Windows.Forms.Padding(2);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(112, 24);
            this.label33.TabIndex = 176;
            this.label33.Text = "e.g. :  6";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label32.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.Crimson;
            this.label32.Location = new System.Drawing.Point(270, 63);
            this.label32.Margin = new System.Windows.Forms.Padding(2);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(112, 24);
            this.label32.TabIndex = 175;
            this.label32.Text = "like : /,-";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSeparator
            // 
            this.txtSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSeparator.Location = new System.Drawing.Point(136, 63);
            this.txtSeparator.Margin = new System.Windows.Forms.Padding(2);
            this.txtSeparator.Name = "txtSeparator";
            this.txtSeparator.Size = new System.Drawing.Size(130, 25);
            this.txtSeparator.TabIndex = 2;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label31.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(2, 63);
            this.label31.Margin = new System.Windows.Forms.Padding(2);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(130, 24);
            this.label31.TabIndex = 173;
            this.label31.Text = "Enter Separator:";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.tblltMain.SetColumnSpan(this.label30, 3);
            this.label30.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label30.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.Color.Crimson;
            this.label30.Location = new System.Drawing.Point(2, 119);
            this.label30.Margin = new System.Windows.Forms.Padding(2);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(380, 24);
            this.label30.TabIndex = 172;
            this.label30.Text = "e.g. : Series Start from AB-6";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStartNumber
            // 
            this.txtStartNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStartNumber.Location = new System.Drawing.Point(136, 91);
            this.txtStartNumber.Margin = new System.Windows.Forms.Padding(2);
            this.txtStartNumber.Name = "txtStartNumber";
            this.txtStartNumber.Size = new System.Drawing.Size(130, 25);
            this.txtStartNumber.TabIndex = 3;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label29.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.ForeColor = System.Drawing.Color.Crimson;
            this.label29.Location = new System.Drawing.Point(270, 35);
            this.label29.Margin = new System.Windows.Forms.Padding(2);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(112, 24);
            this.label29.TabIndex = 170;
            this.label29.Text = "e.g. : AB";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStartChar
            // 
            this.txtStartChar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStartChar.Location = new System.Drawing.Point(136, 35);
            this.txtStartChar.Margin = new System.Windows.Forms.Padding(2);
            this.txtStartChar.Name = "txtStartChar";
            this.txtStartChar.Size = new System.Drawing.Size(130, 25);
            this.txtStartChar.TabIndex = 1;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label27.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(2, 91);
            this.label27.Margin = new System.Windows.Forms.Padding(2);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(130, 24);
            this.label27.TabIndex = 168;
            this.label27.Text = "Start By Number:";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label28.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(2, 35);
            this.label28.Margin = new System.Windows.Forms.Padding(2);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(130, 24);
            this.label28.TabIndex = 167;
            this.label28.Text = "Start By Characters:";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtgUserSeries
            // 
            this.dtgUserSeries.AllowUserToAddRows = false;
            this.dtgUserSeries.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgUserSeries.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgUserSeries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgUserSeries.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StartCharacter,
            this.Separator,
            this.StartByNumber,
            this.CompId});
            this.tblltMain.SetColumnSpan(this.dtgUserSeries, 3);
            this.dtgUserSeries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgUserSeries.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgUserSeries.Location = new System.Drawing.Point(3, 181);
            this.dtgUserSeries.Name = "dtgUserSeries";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtgUserSeries.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtgUserSeries.RowHeadersWidth = 15;
            this.dtgUserSeries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgUserSeries.Size = new System.Drawing.Size(378, 148);
            this.dtgUserSeries.TabIndex = 8;
            this.dtgUserSeries.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgUserSeries_CellClick);
            // 
            // StartCharacter
            // 
            this.StartCharacter.HeaderText = "Start Characters";
            this.StartCharacter.Name = "StartCharacter";
            this.StartCharacter.Width = 150;
            // 
            // Separator
            // 
            this.Separator.HeaderText = "Separator";
            this.Separator.Name = "Separator";
            this.Separator.Width = 80;
            // 
            // StartByNumber
            // 
            this.StartByNumber.HeaderText = "Start By Number";
            this.StartByNumber.Name = "StartByNumber";
            this.StartByNumber.Width = 130;
            // 
            // CompId
            // 
            this.CompId.HeaderText = "CompId";
            this.CompId.Name = "CompId";
            this.CompId.Visible = false;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 3;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltMain.Controls.Add(this.tblltButtons, 0, 5);
            this.tblltMain.Controls.Add(this.label9, 0, 0);
            this.tblltMain.Controls.Add(this.dtgUserSeries, 0, 6);
            this.tblltMain.Controls.Add(this.label28, 0, 1);
            this.tblltMain.Controls.Add(this.label31, 0, 2);
            this.tblltMain.Controls.Add(this.label27, 0, 3);
            this.tblltMain.Controls.Add(this.txtStartChar, 1, 1);
            this.tblltMain.Controls.Add(this.txtSeparator, 1, 2);
            this.tblltMain.Controls.Add(this.label30, 0, 4);
            this.tblltMain.Controls.Add(this.label33, 2, 3);
            this.tblltMain.Controls.Add(this.txtStartNumber, 1, 3);
            this.tblltMain.Controls.Add(this.label32, 2, 2);
            this.tblltMain.Controls.Add(this.label29, 2, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 7;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46F));
            this.tblltMain.Size = new System.Drawing.Size(384, 332);
            this.tblltMain.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 6;
            this.tblltMain.SetColumnSpan(this.tblltButtons, 3);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tblltButtons.Controls.Add(this.btnNew, 1, 0);
            this.tblltButtons.Controls.Add(this.btnSave, 2, 0);
            this.tblltButtons.Controls.Add(this.btnDelete, 3, 0);
            this.tblltButtons.Controls.Add(this.btnClose, 4, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 145);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(384, 33);
            this.tblltButtons.TabIndex = 4;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNew.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(65, 2);
            this.btnNew.Margin = new System.Windows.Forms.Padding(2);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(59, 29);
            this.btnNew.TabIndex = 5;
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
            this.btnSave.Location = new System.Drawing.Point(128, 2);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(59, 29);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(191, 2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(59, 29);
            this.btnDelete.TabIndex = 6;
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
            this.btnClose.Location = new System.Drawing.Point(254, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(59, 29);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label9, 3);
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(0, 0);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(384, 33);
            this.label9.TabIndex = 34;
            this.label9.Text = "User Series";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmUserSeries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(384, 332);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmUserSeries";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Series";
            ((System.ComponentModel.ISupportInitialize)(this.dtgUserSeries)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox txtSeparator;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtStartNumber;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtStartChar;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.DataGridView dtgUserSeries;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartCharacter;
        private System.Windows.Forms.DataGridViewTextBoxColumn Separator;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartByNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompId;
    }
}