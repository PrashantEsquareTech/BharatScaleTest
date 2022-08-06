namespace AIOInventorySystem.Desk.Forms
{
    partial class DemoVersion
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
            this.dtpenddate = new System.Windows.Forms.DateTimePicker();
            this.dtpstartdate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tblltButton = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.rdbtnDemo = new System.Windows.Forms.RadioButton();
            this.rdbtnRental = new System.Windows.Forms.RadioButton();
            this.rdbtnFull = new System.Windows.Forms.RadioButton();
            this.tblltMain.SuspendLayout();
            this.tblltButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 5;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.Controls.Add(this.dtpenddate, 2, 5);
            this.tblltMain.Controls.Add(this.dtpstartdate, 2, 4);
            this.tblltMain.Controls.Add(this.label4, 1, 4);
            this.tblltMain.Controls.Add(this.label3, 1, 5);
            this.tblltMain.Controls.Add(this.tblltButton, 1, 6);
            this.tblltMain.Controls.Add(this.label6, 0, 0);
            this.tblltMain.Controls.Add(this.rdbtnDemo, 1, 2);
            this.tblltMain.Controls.Add(this.rdbtnRental, 2, 2);
            this.tblltMain.Controls.Add(this.rdbtnFull, 3, 2);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 7;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltMain.Size = new System.Drawing.Size(324, 191);
            this.tblltMain.TabIndex = 0;
            // 
            // dtpenddate
            // 
            this.tblltMain.SetColumnSpan(this.dtpenddate, 2);
            this.dtpenddate.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtpenddate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpenddate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpenddate.Location = new System.Drawing.Point(115, 120);
            this.dtpenddate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpenddate.Name = "dtpenddate";
            this.dtpenddate.Size = new System.Drawing.Size(116, 25);
            this.dtpenddate.TabIndex = 2;
            // 
            // dtpstartdate
            // 
            this.tblltMain.SetColumnSpan(this.dtpstartdate, 2);
            this.dtpstartdate.Dock = System.Windows.Forms.DockStyle.Left;
            this.dtpstartdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpstartdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpstartdate.Location = new System.Drawing.Point(115, 88);
            this.dtpstartdate.Margin = new System.Windows.Forms.Padding(2);
            this.dtpstartdate.Name = "dtpstartdate";
            this.dtpstartdate.Size = new System.Drawing.Size(116, 25);
            this.dtpstartdate.TabIndex = 1;
            this.dtpstartdate.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(18, 88);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 28);
            this.label4.TabIndex = 27;
            this.label4.Text = "Start Date:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(18, 120);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 28);
            this.label3.TabIndex = 30;
            this.label3.Text = "End Date:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tblltButton
            // 
            this.tblltButton.ColumnCount = 4;
            this.tblltMain.SetColumnSpan(this.tblltButton, 3);
            this.tblltButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltButton.Controls.Add(this.btnSave, 1, 0);
            this.tblltButton.Controls.Add(this.btnClose, 2, 0);
            this.tblltButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButton.Location = new System.Drawing.Point(16, 150);
            this.tblltButton.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButton.Name = "tblltButton";
            this.tblltButton.RowCount = 1;
            this.tblltButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButton.Size = new System.Drawing.Size(291, 41);
            this.tblltButton.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(74, 3);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(68, 35);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(146, 3);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(68, 35);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label6, 5);
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(324, 38);
            this.label6.TabIndex = 38;
            this.label6.Text = "Version";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rdbtnDemo
            // 
            this.rdbtnDemo.AutoSize = true;
            this.rdbtnDemo.Checked = true;
            this.rdbtnDemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdbtnDemo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbtnDemo.ForeColor = System.Drawing.Color.Navy;
            this.rdbtnDemo.Location = new System.Drawing.Point(20, 49);
            this.rdbtnDemo.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.rdbtnDemo.Name = "rdbtnDemo";
            this.rdbtnDemo.Size = new System.Drawing.Size(91, 26);
            this.rdbtnDemo.TabIndex = 39;
            this.rdbtnDemo.TabStop = true;
            this.rdbtnDemo.Text = "Demo";
            this.rdbtnDemo.UseVisualStyleBackColor = true;
            this.rdbtnDemo.CheckedChanged += new System.EventHandler(this.rdbtnDemo_CheckedChanged);
            // 
            // rdbtnRental
            // 
            this.rdbtnRental.AutoSize = true;
            this.rdbtnRental.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdbtnRental.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbtnRental.ForeColor = System.Drawing.Color.Navy;
            this.rdbtnRental.Location = new System.Drawing.Point(117, 49);
            this.rdbtnRental.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.rdbtnRental.Name = "rdbtnRental";
            this.rdbtnRental.Size = new System.Drawing.Size(91, 26);
            this.rdbtnRental.TabIndex = 40;
            this.rdbtnRental.Text = "Rental";
            this.rdbtnRental.UseVisualStyleBackColor = true;
            this.rdbtnRental.CheckedChanged += new System.EventHandler(this.rdbtnRental_CheckedChanged);
            // 
            // rdbtnFull
            // 
            this.rdbtnFull.AutoSize = true;
            this.rdbtnFull.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rdbtnFull.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbtnFull.ForeColor = System.Drawing.Color.Navy;
            this.rdbtnFull.Location = new System.Drawing.Point(214, 49);
            this.rdbtnFull.Margin = new System.Windows.Forms.Padding(4, 2, 2, 2);
            this.rdbtnFull.Name = "rdbtnFull";
            this.rdbtnFull.Size = new System.Drawing.Size(91, 26);
            this.rdbtnFull.TabIndex = 41;
            this.rdbtnFull.Text = "Full";
            this.rdbtnFull.UseVisualStyleBackColor = true;
            this.rdbtnFull.CheckedChanged += new System.EventHandler(this.rdbtnFull_CheckedChanged);
            // 
            // DemoVersion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(324, 191);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DemoVersion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Version";
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.tblltButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtpenddate;
        private System.Windows.Forms.DateTimePicker dtpstartdate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tblltButton;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.RadioButton rdbtnDemo;
        private System.Windows.Forms.RadioButton rdbtnRental;
        private System.Windows.Forms.RadioButton rdbtnFull;
    }
}