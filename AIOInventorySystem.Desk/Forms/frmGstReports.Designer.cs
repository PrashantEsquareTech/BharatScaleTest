namespace AIOInventorySystem.Desk.Forms
{
    partial class frmGstReports
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
            this.detailLable2 = new System.Windows.Forms.Label();
            this.dtptomonth = new System.Windows.Forms.DateTimePicker();
            this.detailLable1 = new System.Windows.Forms.Label();
            this.dtpMonth = new System.Windows.Forms.DateTimePicker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblgstin = new System.Windows.Forms.Label();
            this.lblRegPerson = new System.Windows.Forms.Label();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnShow = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // detailLable2
            // 
            this.detailLable2.AutoSize = true;
            this.detailLable2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailLable2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailLable2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.detailLable2.Location = new System.Drawing.Point(228, 2);
            this.detailLable2.Margin = new System.Windows.Forms.Padding(2);
            this.detailLable2.Name = "detailLable2";
            this.detailLable2.Size = new System.Drawing.Size(70, 24);
            this.detailLable2.TabIndex = 21;
            this.detailLable2.Text = "To Date";
            this.detailLable2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtptomonth
            // 
            this.dtptomonth.CustomFormat = "";
            this.dtptomonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtptomonth.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtptomonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtptomonth.Location = new System.Drawing.Point(302, 2);
            this.dtptomonth.Margin = new System.Windows.Forms.Padding(2);
            this.dtptomonth.Name = "dtptomonth";
            this.dtptomonth.Size = new System.Drawing.Size(121, 25);
            this.dtptomonth.TabIndex = 1;
            // 
            // detailLable1
            // 
            this.detailLable1.AutoSize = true;
            this.detailLable1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailLable1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detailLable1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.detailLable1.Location = new System.Drawing.Point(2, 2);
            this.detailLable1.Margin = new System.Windows.Forms.Padding(2);
            this.detailLable1.Name = "detailLable1";
            this.detailLable1.Size = new System.Drawing.Size(97, 24);
            this.detailLable1.TabIndex = 20;
            this.detailLable1.Text = "From Date:";
            this.detailLable1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpMonth
            // 
            this.dtpMonth.CustomFormat = "";
            this.dtpMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpMonth.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpMonth.Location = new System.Drawing.Point(103, 2);
            this.dtpMonth.Margin = new System.Windows.Forms.Padding(2);
            this.dtpMonth.Name = "dtpMonth";
            this.dtpMonth.Size = new System.Drawing.Size(121, 25);
            this.dtpMonth.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblltMain.SetColumnSpan(this.dataGridView1, 5);
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridView1.Location = new System.Drawing.Point(2, 114);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Size = new System.Drawing.Size(530, 206);
            this.dataGridView1.TabIndex = 22;
            // 
            // dataGridView2
            // 
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tblltMain.SetColumnSpan(this.dataGridView2, 5);
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(4, 354);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(526, 204);
            this.dataGridView2.TabIndex = 23;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.tblltMain.SetColumnSpan(this.label1, 2);
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 86);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(222, 24);
            this.label1.TabIndex = 24;
            this.label1.Text = "Sale Bill Transaction";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.tblltMain.SetColumnSpan(this.label2, 2);
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 324);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(222, 24);
            this.label2.TabIndex = 25;
            this.label2.Text = "Purchase Bill Transaction";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(2, 30);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 24);
            this.label3.TabIndex = 26;
            this.label3.Text = "GSTIN:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.tblltMain.SetColumnSpan(this.label4, 3);
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(2, 58);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(296, 24);
            this.label4.TabIndex = 27;
            this.label4.Text = "Legal Name of the registered person:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblgstin
            // 
            this.lblgstin.AutoSize = true;
            this.lblgstin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblgstin.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblgstin.Location = new System.Drawing.Point(105, 32);
            this.lblgstin.Margin = new System.Windows.Forms.Padding(4);
            this.lblgstin.Name = "lblgstin";
            this.lblgstin.Size = new System.Drawing.Size(117, 20);
            this.lblgstin.TabIndex = 28;
            this.lblgstin.Text = "label5";
            this.lblgstin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRegPerson
            // 
            this.lblRegPerson.AutoSize = true;
            this.lblRegPerson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRegPerson.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegPerson.Location = new System.Drawing.Point(304, 60);
            this.lblRegPerson.Margin = new System.Windows.Forms.Padding(4);
            this.lblRegPerson.Name = "lblRegPerson";
            this.lblRegPerson.Size = new System.Drawing.Size(117, 20);
            this.lblRegPerson.TabIndex = 29;
            this.lblRegPerson.Text = "label5";
            this.lblRegPerson.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 5;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.5F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltMain.Controls.Add(this.btnShow, 4, 0);
            this.tblltMain.Controls.Add(this.detailLable1, 0, 0);
            this.tblltMain.Controls.Add(this.lblRegPerson, 3, 2);
            this.tblltMain.Controls.Add(this.dataGridView2, 0, 6);
            this.tblltMain.Controls.Add(this.label2, 0, 5);
            this.tblltMain.Controls.Add(this.dtpMonth, 1, 0);
            this.tblltMain.Controls.Add(this.label1, 0, 3);
            this.tblltMain.Controls.Add(this.dataGridView1, 0, 4);
            this.tblltMain.Controls.Add(this.lblgstin, 1, 1);
            this.tblltMain.Controls.Add(this.label4, 0, 2);
            this.tblltMain.Controls.Add(this.detailLable2, 2, 0);
            this.tblltMain.Controls.Add(this.dtptomonth, 3, 0);
            this.tblltMain.Controls.Add(this.label3, 0, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 7;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.5F));
            this.tblltMain.Size = new System.Drawing.Size(534, 562);
            this.tblltMain.TabIndex = 0;
            // 
            // btnShow
            // 
            this.btnShow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnShow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnShow.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow.ForeColor = System.Drawing.Color.White;
            this.btnShow.Location = new System.Drawing.Point(441, 2);
            this.btnShow.Margin = new System.Windows.Forms.Padding(16, 2, 16, 2);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(77, 24);
            this.btnShow.TabIndex = 2;
            this.btnShow.Text = "Show";
            this.btnShow.UseVisualStyleBackColor = false;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // frmGstReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(534, 562);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmGstReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "3B Details";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label detailLable2;
        private System.Windows.Forms.DateTimePicker dtptomonth;
        private System.Windows.Forms.Label detailLable1;
        private System.Windows.Forms.DateTimePicker dtpMonth;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblgstin;
        private System.Windows.Forms.Label lblRegPerson;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Button btnShow;
    }
}