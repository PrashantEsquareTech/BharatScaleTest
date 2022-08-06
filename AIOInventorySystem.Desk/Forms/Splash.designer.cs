namespace AIOInventorySystem.Desk
{
    partial class Splash
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
            this.prgbar = new System.Windows.Forms.ProgressBar();
            this.lblDisplay = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.picbox = new System.Windows.Forms.PictureBox();
            this.bwActivationCheck = new System.ComponentModel.BackgroundWorker();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.picbox)).BeginInit();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // prgbar
            // 
            this.tblltMain.SetColumnSpan(this.prgbar, 2);
            this.prgbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prgbar.Location = new System.Drawing.Point(0, 246);
            this.prgbar.Margin = new System.Windows.Forms.Padding(0);
            this.prgbar.Name = "prgbar";
            this.prgbar.Size = new System.Drawing.Size(350, 23);
            this.prgbar.TabIndex = 0;
            // 
            // lblDisplay
            // 
            this.lblDisplay.AutoSize = true;
            this.lblDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDisplay.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplay.ForeColor = System.Drawing.Color.White;
            this.lblDisplay.Location = new System.Drawing.Point(0, 220);
            this.lblDisplay.Margin = new System.Windows.Forms.Padding(0);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(175, 26);
            this.lblDisplay.TabIndex = 1;
            this.lblDisplay.Text = "label1";
            this.lblDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.Color.White;
            this.lblVersion.Location = new System.Drawing.Point(175, 220);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(0);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(175, 26);
            this.lblVersion.TabIndex = 3;
            this.lblVersion.Text = "label1";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // picbox
            // 
            this.picbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tblltMain.SetColumnSpan(this.picbox, 2);
            this.picbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picbox.Image = global::AIOInventorySystem.Desk.Properties.Resources.ESquare;
            this.picbox.InitialImage = null;
            this.picbox.Location = new System.Drawing.Point(0, 0);
            this.picbox.Margin = new System.Windows.Forms.Padding(0);
            this.picbox.Name = "picbox";
            this.picbox.Size = new System.Drawing.Size(350, 220);
            this.picbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picbox.TabIndex = 4;
            this.picbox.TabStop = false;
            // 
            // bwActivationCheck
            // 
            this.bwActivationCheck.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwActivationCheck_DoWork);
            // 
            // tblltMain
            // 
            this.tblltMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.tblltMain.ColumnCount = 2;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltMain.Controls.Add(this.picbox, 0, 0);
            this.tblltMain.Controls.Add(this.prgbar, 0, 2);
            this.tblltMain.Controls.Add(this.lblVersion, 1, 1);
            this.tblltMain.Controls.Add(this.lblDisplay, 0, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 3;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltMain.Size = new System.Drawing.Size(350, 269);
            this.tblltMain.TabIndex = 5;
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.ClientSize = new System.Drawing.Size(350, 269);
            this.ControlBox = false;
            this.Controls.Add(this.tblltMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Splash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splash";
            this.Load += new System.EventHandler(this.Splash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picbox)).EndInit();
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar prgbar;
        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.PictureBox picbox;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Timer timer1;
        private System.ComponentModel.BackgroundWorker bwActivationCheck;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
    }
}