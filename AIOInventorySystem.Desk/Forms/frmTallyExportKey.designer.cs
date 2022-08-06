namespace AIOInventorySystem.Desk.Forms
{
    partial class frmTallyExportKey
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
            this.txtActivationKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAccept = new System.Windows.Forms.Button();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButton = new System.Windows.Forms.TableLayoutPanel();
            this.tblltMain.SuspendLayout();
            this.tblltButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtActivationKey
            // 
            this.txtActivationKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtActivationKey.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtActivationKey.Location = new System.Drawing.Point(134, 25);
            this.txtActivationKey.Margin = new System.Windows.Forms.Padding(2);
            this.txtActivationKey.Name = "txtActivationKey";
            this.txtActivationKey.PasswordChar = '*';
            this.txtActivationKey.Size = new System.Drawing.Size(158, 25);
            this.txtActivationKey.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 25);
            this.label2.TabIndex = 24;
            this.label2.Text = "Tally Activation Key:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAccept
            // 
            this.btnAccept.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnAccept.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAccept.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAccept.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccept.ForeColor = System.Drawing.Color.White;
            this.btnAccept.Location = new System.Drawing.Point(100, 3);
            this.btnAccept.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(94, 35);
            this.btnAccept.TabIndex = 2;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = false;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 2;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tblltMain.Controls.Add(this.tblltButton, 0, 3);
            this.tblltMain.Controls.Add(this.txtActivationKey, 1, 1);
            this.tblltMain.Controls.Add(this.label2, 0, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 4;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblltMain.Size = new System.Drawing.Size(294, 116);
            this.tblltMain.TabIndex = 0;
            // 
            // tblltButton
            // 
            this.tblltButton.ColumnCount = 3;
            this.tblltMain.SetColumnSpan(this.tblltButton, 2);
            this.tblltButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblltButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblltButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblltButton.Controls.Add(this.btnAccept, 1, 0);
            this.tblltButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButton.Location = new System.Drawing.Point(0, 75);
            this.tblltButton.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButton.Name = "tblltButton";
            this.tblltButton.RowCount = 1;
            this.tblltButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButton.Size = new System.Drawing.Size(294, 41);
            this.tblltButton.TabIndex = 2;
            // 
            // frmTallyExportKey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(294, 116);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(490, 280);
            this.Name = "frmTallyExportKey";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tally Key";
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.tblltButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtActivationKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltButton;

    }
}