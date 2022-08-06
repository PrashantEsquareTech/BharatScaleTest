namespace AIOInventorySystem.Desk.Forms
{
    partial class frmMasterSettings
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
            this.chkProduct = new System.Windows.Forms.CheckBox();
            this.chkManuComp = new System.Windows.Forms.CheckBox();
            this.chkUnit = new System.Windows.Forms.CheckBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.tblltMain.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkProduct
            // 
            this.chkProduct.AutoSize = true;
            this.chkProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkProduct.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkProduct.Location = new System.Drawing.Point(41, 42);
            this.chkProduct.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkProduct.Name = "chkProduct";
            this.chkProduct.Size = new System.Drawing.Size(201, 25);
            this.chkProduct.TabIndex = 1;
            this.chkProduct.Text = "Product";
            this.chkProduct.UseVisualStyleBackColor = true;
            // 
            // chkManuComp
            // 
            this.chkManuComp.AutoSize = true;
            this.chkManuComp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkManuComp.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkManuComp.Location = new System.Drawing.Point(41, 71);
            this.chkManuComp.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkManuComp.Name = "chkManuComp";
            this.chkManuComp.Size = new System.Drawing.Size(201, 25);
            this.chkManuComp.TabIndex = 2;
            this.chkManuComp.Text = "Manufacturing Company";
            this.chkManuComp.UseVisualStyleBackColor = true;
            // 
            // chkUnit
            // 
            this.chkUnit.AutoSize = true;
            this.chkUnit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkUnit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUnit.Location = new System.Drawing.Point(41, 100);
            this.chkUnit.Margin = new System.Windows.Forms.Padding(5, 2, 2, 2);
            this.chkUnit.Name = "chkUnit";
            this.chkUnit.Size = new System.Drawing.Size(201, 25);
            this.chkUnit.TabIndex = 3;
            this.chkUnit.Text = "Unit";
            this.chkUnit.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOk.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(63, 2);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(57, 31);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(124, 2);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(57, 31);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 2;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tblltMain.Controls.Add(this.chkProduct, 1, 1);
            this.tblltMain.Controls.Add(this.chkManuComp, 1, 2);
            this.tblltMain.Controls.Add(this.chkUnit, 1, 3);
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.tblltButtons, 0, 4);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 5;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.0025F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.33183F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.33183F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.33183F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.002F));
            this.tblltMain.Size = new System.Drawing.Size(244, 162);
            this.tblltMain.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.SetColumnSpan(this.label5, 2);
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(244, 40);
            this.label5.TabIndex = 12;
            this.label5.Text = "Common Masters";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 4;
            this.tblltMain.SetColumnSpan(this.tblltButtons, 2);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltButtons.Controls.Add(this.btnOk, 1, 0);
            this.tblltButtons.Controls.Add(this.btnCancel, 2, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 127);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(244, 35);
            this.tblltButtons.TabIndex = 4;
            // 
            // frmMasterSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(244, 162);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmMasterSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Master Settings";
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkProduct;
        private System.Windows.Forms.CheckBox chkManuComp;
        private System.Windows.Forms.CheckBox chkUnit;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
    }
}