namespace AIOInventorySystem.Desk.Forms
{
    partial class frmSendMail
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMailSender = new System.Windows.Forms.TextBox();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.txtBody = new System.Windows.Forms.TextBox();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblAttachment = new System.Windows.Forms.Label();
            this.tblltDetail = new System.Windows.Forms.TableLayoutPanel();
            this.tblltButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.lnkAttachFile = new System.Windows.Forms.LinkLabel();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.tblltDetail.SuspendLayout();
            this.tblltButtons.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
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
            this.label5.Size = new System.Drawing.Size(434, 34);
            this.label5.TabIndex = 39;
            this.label5.Text = "Send Mail";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMailSender
            // 
            this.txtMailSender.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMailSender.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMailSender.ForeColor = System.Drawing.Color.Black;
            this.txtMailSender.Location = new System.Drawing.Point(99, 2);
            this.txtMailSender.Margin = new System.Windows.Forms.Padding(2);
            this.txtMailSender.Name = "txtMailSender";
            this.txtMailSender.Size = new System.Drawing.Size(291, 25);
            this.txtMailSender.TabIndex = 1;
            this.txtMailSender.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMailSender_KeyDown);
            // 
            // txtFrom
            // 
            this.txtFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFrom.Enabled = false;
            this.txtFrom.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrom.ForeColor = System.Drawing.Color.Black;
            this.txtFrom.Location = new System.Drawing.Point(99, 29);
            this.txtFrom.Margin = new System.Windows.Forms.Padding(2);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(291, 25);
            this.txtFrom.TabIndex = 2;
            // 
            // txtSubject
            // 
            this.txtSubject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSubject.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubject.ForeColor = System.Drawing.Color.Black;
            this.txtSubject.Location = new System.Drawing.Point(99, 83);
            this.txtSubject.Margin = new System.Windows.Forms.Padding(2);
            this.txtSubject.Multiline = true;
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(291, 23);
            this.txtSubject.TabIndex = 4;
            this.txtSubject.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSubject_KeyDown);
            // 
            // txtBody
            // 
            this.tblltDetail.SetColumnSpan(this.txtBody, 2);
            this.txtBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBody.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBody.ForeColor = System.Drawing.Color.Black;
            this.txtBody.Location = new System.Drawing.Point(99, 158);
            this.txtBody.Margin = new System.Windows.Forms.Padding(2);
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.tblltDetail.SetRowSpan(this.txtBody, 3);
            this.txtBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtBody.Size = new System.Drawing.Size(321, 77);
            this.txtBody.TabIndex = 7;
            this.txtBody.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBody_KeyDown);
            // 
            // txtTo
            // 
            this.txtTo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtTo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTo.ForeColor = System.Drawing.Color.Black;
            this.txtTo.Location = new System.Drawing.Point(99, 56);
            this.txtTo.Margin = new System.Windows.Forms.Padding(2);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(291, 25);
            this.txtTo.TabIndex = 3;
            this.txtTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTo_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblAttachment);
            this.groupBox1.Controls.Add(this.tblltDetail);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(3, 38);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(428, 300);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Send Mail Information";
            // 
            // lblAttachment
            // 
            this.lblAttachment.AutoSize = true;
            this.lblAttachment.ForeColor = System.Drawing.Color.Black;
            this.lblAttachment.Location = new System.Drawing.Point(176, 170);
            this.lblAttachment.Name = "lblAttachment";
            this.lblAttachment.Size = new System.Drawing.Size(0, 17);
            this.lblAttachment.TabIndex = 87;
            // 
            // tblltDetail
            // 
            this.tblltDetail.ColumnCount = 3;
            this.tblltDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tblltDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tblltDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tblltDetail.Controls.Add(this.tblltButtons, 0, 9);
            this.tblltDetail.Controls.Add(this.label10, 2, 2);
            this.tblltDetail.Controls.Add(this.txtBody, 1, 6);
            this.tblltDetail.Controls.Add(this.label7, 0, 0);
            this.tblltDetail.Controls.Add(this.label3, 0, 6);
            this.tblltDetail.Controls.Add(this.label9, 2, 1);
            this.tblltDetail.Controls.Add(this.label1, 0, 1);
            this.tblltDetail.Controls.Add(this.label8, 2, 0);
            this.tblltDetail.Controls.Add(this.label2, 0, 2);
            this.tblltDetail.Controls.Add(this.txtMailSender, 1, 0);
            this.tblltDetail.Controls.Add(this.label4, 0, 3);
            this.tblltDetail.Controls.Add(this.linkLabel1, 1, 5);
            this.tblltDetail.Controls.Add(this.txtFrom, 1, 1);
            this.tblltDetail.Controls.Add(this.label6, 0, 4);
            this.tblltDetail.Controls.Add(this.txtSubject, 1, 3);
            this.tblltDetail.Controls.Add(this.txtTo, 1, 2);
            this.tblltDetail.Controls.Add(this.lnkAttachFile, 1, 4);
            this.tblltDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltDetail.Location = new System.Drawing.Point(3, 22);
            this.tblltDetail.Margin = new System.Windows.Forms.Padding(0);
            this.tblltDetail.Name = "tblltDetail";
            this.tblltDetail.RowCount = 10;
            this.tblltDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
            this.tblltDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltDetail.Size = new System.Drawing.Size(422, 274);
            this.tblltDetail.TabIndex = 0;
            // 
            // tblltButtons
            // 
            this.tblltButtons.ColumnCount = 4;
            this.tblltDetail.SetColumnSpan(this.tblltButtons, 3);
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltButtons.Controls.Add(this.btnSend, 1, 0);
            this.tblltButtons.Controls.Add(this.btnClose, 2, 0);
            this.tblltButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtons.Location = new System.Drawing.Point(0, 237);
            this.tblltButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtons.Name = "tblltButtons";
            this.tblltButtons.RowCount = 1;
            this.tblltButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtons.Size = new System.Drawing.Size(422, 37);
            this.tblltButtons.TabIndex = 8;
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnSend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(128, 2);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(80, 33);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "Send Mail";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(212, 2);
            this.btnClose.Margin = new System.Windows.Forms.Padding(2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 33);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label10.Location = new System.Drawing.Point(395, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(23, 27);
            this.label10.TabIndex = 91;
            this.label10.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(2, 2);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 23);
            this.label7.TabIndex = 88;
            this.label7.Text = "Mail Sender:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(2, 158);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 23);
            this.label3.TabIndex = 83;
            this.label3.Text = "Body:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label9.Location = new System.Drawing.Point(395, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 27);
            this.label9.TabIndex = 90;
            this.label9.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(2, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 23);
            this.label1.TabIndex = 85;
            this.label1.Text = "From:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(395, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(23, 27);
            this.label8.TabIndex = 89;
            this.label8.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(2, 56);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 23);
            this.label2.TabIndex = 82;
            this.label2.Text = "To:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(2, 83);
            this.label4.Margin = new System.Windows.Forms.Padding(2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 23);
            this.label4.TabIndex = 84;
            this.label4.Text = "Subject:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.OrangeRed;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.ForeColor = System.Drawing.Color.OrangeRed;
            this.linkLabel1.LinkColor = System.Drawing.Color.OrangeRed;
            this.linkLabel1.Location = new System.Drawing.Point(99, 134);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(291, 20);
            this.linkLabel1.TabIndex = 6;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Cancel Attachement";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.OrangeRed;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(2, 110);
            this.label6.Margin = new System.Windows.Forms.Padding(2);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 20);
            this.label6.TabIndex = 86;
            this.label6.Text = "Attachment:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lnkAttachFile
            // 
            this.lnkAttachFile.ActiveLinkColor = System.Drawing.Color.Crimson;
            this.lnkAttachFile.AutoSize = true;
            this.lnkAttachFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lnkAttachFile.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkAttachFile.ForeColor = System.Drawing.Color.Crimson;
            this.lnkAttachFile.LinkColor = System.Drawing.Color.Crimson;
            this.lnkAttachFile.Location = new System.Drawing.Point(99, 110);
            this.lnkAttachFile.Margin = new System.Windows.Forms.Padding(2);
            this.lnkAttachFile.Name = "lnkAttachFile";
            this.lnkAttachFile.Size = new System.Drawing.Size(291, 20);
            this.lnkAttachFile.TabIndex = 5;
            this.lnkAttachFile.TabStop = true;
            this.lnkAttachFile.Text = "Attach";
            this.lnkAttachFile.VisitedLinkColor = System.Drawing.Color.Crimson;
            this.lnkAttachFile.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAttachFile_LinkClicked);
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 1;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltMain.Controls.Add(this.label5, 0, 0);
            this.tblltMain.Controls.Add(this.groupBox1, 0, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(0);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 2;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90F));
            this.tblltMain.Size = new System.Drawing.Size(434, 342);
            this.tblltMain.TabIndex = 0;
            // 
            // frmSendMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(434, 342);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "frmSendMail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Send Mail";
            this.Load += new System.EventHandler(this.frmSendMail_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tblltDetail.ResumeLayout(false);
            this.tblltDetail.PerformLayout();
            this.tblltButtons.ResumeLayout(false);
            this.tblltMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtMailSender;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label lblAttachment;
        private System.Windows.Forms.LinkLabel lnkAttachFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.TextBox txtBody;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.TableLayoutPanel tblltDetail;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TableLayoutPanel tblltButtons;
    }
}