namespace AIOInventorySystem.Desk.Forms
{
    partial class frmloginForm
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
            this.components = new System.ComponentModel.Container();
            this.txtusername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbTransactionYear = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.tblltInputs = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnlogin = new System.Windows.Forms.Button();
            this.tblltMain.SuspendLayout();
            this.tblltInputs.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtusername
            // 
            this.txtusername.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtusername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtusername.ForeColor = System.Drawing.Color.Black;
            this.txtusername.Location = new System.Drawing.Point(146, 44);
            this.txtusername.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtusername.Name = "txtusername";
            this.txtusername.Size = new System.Drawing.Size(251, 29);
            this.txtusername.TabIndex = 1;
            this.txtusername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtusername_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label2.Location = new System.Drawing.Point(3, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 36);
            this.label2.TabIndex = 5;
            this.label2.Text = "UserName:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label3.Location = new System.Drawing.Point(3, 82);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 38);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtpassword
            // 
            this.txtpassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtpassword.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpassword.ForeColor = System.Drawing.Color.Black;
            this.txtpassword.Location = new System.Drawing.Point(146, 84);
            this.txtpassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.PasswordChar = '*';
            this.txtpassword.Size = new System.Drawing.Size(251, 29);
            this.txtpassword.TabIndex = 2;
            this.txtpassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpassword_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.label5.Location = new System.Drawing.Point(3, 2);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 36);
            this.label5.TabIndex = 9;
            this.label5.Text = "Year:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbTransactionYear
            // 
            this.cmbTransactionYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTransactionYear.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTransactionYear.FormattingEnabled = true;
            this.cmbTransactionYear.ItemHeight = 21;
            this.cmbTransactionYear.Location = new System.Drawing.Point(146, 4);
            this.cmbTransactionYear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbTransactionYear.Name = "cmbTransactionYear";
            this.cmbTransactionYear.Size = new System.Drawing.Size(251, 29);
            this.cmbTransactionYear.TabIndex = 0;
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.Color.PowderBlue;
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar1.Location = new System.Drawing.Point(27, 235);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(27, 2, 27, 2);
            this.progressBar1.Maximum = 50;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(373, 24);
            this.progressBar1.TabIndex = 11;
            this.progressBar1.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tblltMain
            // 
            this.tblltMain.BackColor = System.Drawing.Color.Transparent;
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.progressBar1, 0, 4);
            this.tblltMain.Controls.Add(this.tblltInputs, 0, 1);
            this.tblltMain.Controls.Add(this.tblltButtons, 0, 3);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 5;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.Size = new System.Drawing.Size(427, 261);
            this.tblltMain.TabIndex = 0;
            // 
            // tblltInputs
            // 
            this.tblltInputs.ColumnCount = 3;
            this.tblltInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34F));
            this.tblltInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 62F));
            this.tblltInputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4F));
            this.tblltInputs.Controls.Add(this.cmbTransactionYear, 1, 0);
            this.tblltInputs.Controls.Add(this.label3, 0, 2);
            this.tblltInputs.Controls.Add(this.label5, 0, 0);
            this.tblltInputs.Controls.Add(this.label2, 0, 1);
            this.tblltInputs.Controls.Add(this.txtusername, 1, 1);
            this.tblltInputs.Controls.Add(this.txtpassword, 1, 2);
            this.tblltInputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltInputs.Location = new System.Drawing.Point(4, 43);
            this.tblltInputs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tblltInputs.Name = "tblltInputs";
            this.tblltInputs.RowCount = 3;
            this.tblltInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblltInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblltInputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tblltInputs.Size = new System.Drawing.Size(419, 122);
            this.tblltInputs.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 4;
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltButtons.Controls.Add(this.btnClose, 2, 0);
            this.tblltButtons.Controls.Add(this.btnlogin, 1, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 187);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(427, 46);
            this.tblltButtons.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnClose.Location = new System.Drawing.Point(216, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(79, 42);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnlogin
            // 
            this.btnlogin.BackColor = System.Drawing.Color.White;
            this.btnlogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnlogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnlogin.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlogin.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnlogin.Location = new System.Drawing.Point(131, 2);
            this.btnlogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(79, 42);
            this.btnlogin.TabIndex = 3;
            this.btnlogin.Text = "Login";
            this.btnlogin.UseVisualStyleBackColor = false;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // frmloginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::AIOInventorySystem.Desk.Properties.Resources.Photo_Background_Application4;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(427, 261);
            this.Controls.Add(this.tblltMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "frmloginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmloginForm_FormClosing);
            this.tblltMain.ResumeLayout(false);
            this.tblltInputs.ResumeLayout(false);
            this.tblltInputs.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtusername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbTransactionYear;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltInputs;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnlogin;
    }
}