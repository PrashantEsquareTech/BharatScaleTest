namespace AIOInventorySystem.Desk.Forms
{
    partial class frmTallyExport
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
            this.pnlDateExport = new System.Windows.Forms.Panel();
            this.tblltDates = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpfromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.lbltot1 = new System.Windows.Forms.Label();
            this.lbltot2 = new System.Windows.Forms.Label();
            this.lbltot3 = new System.Windows.Forms.Label();
            this.lbltot4 = new System.Windows.Forms.Label();
            this.lbltot5 = new System.Windows.Forms.Label();
            this.lbltot7 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.totMasterCount = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.pnlErrorPanel = new System.Windows.Forms.Panel();
            this.tblltErrorList = new System.Windows.Forms.TableLayoutPanel();
            this.lblLabel10 = new System.Windows.Forms.Label();
            this.lblLabel9 = new System.Windows.Forms.Label();
            this.lblLabel8 = new System.Windows.Forms.Label();
            this.lbltot9 = new System.Windows.Forms.Label();
            this.lblLabel7 = new System.Windows.Forms.Label();
            this.lblLabel5 = new System.Windows.Forms.Label();
            this.lbltot8 = new System.Windows.Forms.Label();
            this.lblLabel4 = new System.Windows.Forms.Label();
            this.lbltot6 = new System.Windows.Forms.Label();
            this.lblLabel3 = new System.Windows.Forms.Label();
            this.lblLabel2 = new System.Windows.Forms.Label();
            this.lblLabel1 = new System.Windows.Forms.Label();
            this.lblLabel6 = new System.Windows.Forms.Label();
            this.lbltot10 = new System.Windows.Forms.Label();
            this.tblltButtonOK = new System.Windows.Forms.TableLayoutPanel();
            this.btnOK = new System.Windows.Forms.Button();
            this.totTransactionCount = new System.Windows.Forms.Label();
            this.barprgrsbar = new System.Windows.Forms.ProgressBar();
            this.tblltMain = new System.Windows.Forms.TableLayoutPanel();
            this.btnTransactionExport = new System.Windows.Forms.Button();
            this.btnMasterExport = new System.Windows.Forms.Button();
            this.pnlDetailedErrorPanel = new System.Windows.Forms.Panel();
            this.tblltDetail = new System.Windows.Forms.TableLayoutPanel();
            this.btnErrorOK = new System.Windows.Forms.Button();
            this.lblErrorLabel = new System.Windows.Forms.Label();
            this.dtgvErrorList = new System.Windows.Forms.DataGridView();
            this.pnlDateExport.SuspendLayout();
            this.tblltDates.SuspendLayout();
            this.pnlErrorPanel.SuspendLayout();
            this.tblltErrorList.SuspendLayout();
            this.tblltButtonOK.SuspendLayout();
            this.tblltMain.SuspendLayout();
            this.pnlDetailedErrorPanel.SuspendLayout();
            this.tblltDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvErrorList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlDateExport
            // 
            this.pnlDateExport.BackColor = System.Drawing.Color.PaleTurquoise;
            this.pnlDateExport.Controls.Add(this.tblltDates);
            this.pnlDateExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDateExport.Location = new System.Drawing.Point(3, 144);
            this.pnlDateExport.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pnlDateExport.Name = "pnlDateExport";
            this.pnlDateExport.Size = new System.Drawing.Size(180, 140);
            this.pnlDateExport.TabIndex = 2;
            this.pnlDateExport.Visible = false;
            // 
            // tblltDates
            // 
            this.tblltDates.ColumnCount = 2;
            this.tblltDates.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tblltDates.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tblltDates.Controls.Add(this.btnClose, 1, 3);
            this.tblltDates.Controls.Add(this.btnExport, 0, 3);
            this.tblltDates.Controls.Add(this.label1, 0, 0);
            this.tblltDates.Controls.Add(this.label2, 0, 1);
            this.tblltDates.Controls.Add(this.dtpfromDate, 1, 0);
            this.tblltDates.Controls.Add(this.dtpToDate, 1, 1);
            this.tblltDates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltDates.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tblltDates.Location = new System.Drawing.Point(0, 0);
            this.tblltDates.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltDates.Name = "tblltDates";
            this.tblltDates.RowCount = 4;
            this.tblltDates.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tblltDates.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tblltDates.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31F));
            this.tblltDates.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23F));
            this.tblltDates.Size = new System.Drawing.Size(180, 140);
            this.tblltDates.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(91, 109);
            this.btnClose.Margin = new System.Windows.Forms.Padding(10, 2, 20, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(69, 29);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(10, 109);
            this.btnExport.Margin = new System.Windows.Forms.Padding(10, 2, 10, 2);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(61, 29);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "From Date:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "To Date:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpfromDate
            // 
            this.dtpfromDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpfromDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfromDate.Location = new System.Drawing.Point(84, 3);
            this.dtpfromDate.Name = "dtpfromDate";
            this.dtpfromDate.Size = new System.Drawing.Size(93, 25);
            this.dtpfromDate.TabIndex = 3;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpToDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpToDate.Location = new System.Drawing.Point(84, 35);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(93, 25);
            this.dtpToDate.TabIndex = 4;
            // 
            // lbltot1
            // 
            this.lbltot1.AutoSize = true;
            this.lbltot1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltot1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltot1.ForeColor = System.Drawing.Color.Red;
            this.lbltot1.Location = new System.Drawing.Point(217, 3);
            this.lbltot1.Margin = new System.Windows.Forms.Padding(3);
            this.lbltot1.Name = "lbltot1";
            this.lbltot1.Size = new System.Drawing.Size(208, 22);
            this.lbltot1.TabIndex = 12;
            this.lbltot1.Text = "0";
            this.lbltot1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbltot1.Click += new System.EventHandler(this.lbltot1_Click);
            // 
            // lbltot2
            // 
            this.lbltot2.AutoSize = true;
            this.lbltot2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltot2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltot2.ForeColor = System.Drawing.Color.Red;
            this.lbltot2.Location = new System.Drawing.Point(217, 31);
            this.lbltot2.Margin = new System.Windows.Forms.Padding(3);
            this.lbltot2.Name = "lbltot2";
            this.lbltot2.Size = new System.Drawing.Size(208, 22);
            this.lbltot2.TabIndex = 13;
            this.lbltot2.Text = "0";
            this.lbltot2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbltot2.Click += new System.EventHandler(this.lbltot2_Click);
            // 
            // lbltot3
            // 
            this.lbltot3.AutoSize = true;
            this.lbltot3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltot3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltot3.ForeColor = System.Drawing.Color.Red;
            this.lbltot3.Location = new System.Drawing.Point(217, 59);
            this.lbltot3.Margin = new System.Windows.Forms.Padding(3);
            this.lbltot3.Name = "lbltot3";
            this.lbltot3.Size = new System.Drawing.Size(208, 22);
            this.lbltot3.TabIndex = 14;
            this.lbltot3.Text = "0";
            this.lbltot3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbltot3.Click += new System.EventHandler(this.lbltot3_Click);
            // 
            // lbltot4
            // 
            this.lbltot4.AutoSize = true;
            this.lbltot4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltot4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltot4.ForeColor = System.Drawing.Color.Red;
            this.lbltot4.Location = new System.Drawing.Point(217, 87);
            this.lbltot4.Margin = new System.Windows.Forms.Padding(3);
            this.lbltot4.Name = "lbltot4";
            this.lbltot4.Size = new System.Drawing.Size(208, 22);
            this.lbltot4.TabIndex = 15;
            this.lbltot4.Text = "0";
            this.lbltot4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbltot4.Click += new System.EventHandler(this.lbltot4_Click);
            // 
            // lbltot5
            // 
            this.lbltot5.AutoSize = true;
            this.lbltot5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltot5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltot5.ForeColor = System.Drawing.Color.Red;
            this.lbltot5.Location = new System.Drawing.Point(217, 115);
            this.lbltot5.Margin = new System.Windows.Forms.Padding(3);
            this.lbltot5.Name = "lbltot5";
            this.lbltot5.Size = new System.Drawing.Size(208, 22);
            this.lbltot5.TabIndex = 16;
            this.lbltot5.Text = "0";
            this.lbltot5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbltot5.Click += new System.EventHandler(this.lbltot5_Click);
            // 
            // lbltot7
            // 
            this.lbltot7.AutoSize = true;
            this.lbltot7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltot7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltot7.ForeColor = System.Drawing.Color.Red;
            this.lbltot7.Location = new System.Drawing.Point(217, 171);
            this.lbltot7.Margin = new System.Windows.Forms.Padding(3);
            this.lbltot7.Name = "lbltot7";
            this.lbltot7.Size = new System.Drawing.Size(208, 22);
            this.lbltot7.TabIndex = 17;
            this.lbltot7.Text = "0";
            this.lbltot7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbltot7.Click += new System.EventHandler(this.lbltot7_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label15.Location = new System.Drawing.Point(189, 47);
            this.label15.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(180, 35);
            this.label15.TabIndex = 18;
            this.label15.Text = "Total Transaction Count :";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // totMasterCount
            // 
            this.totMasterCount.AutoSize = true;
            this.totMasterCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totMasterCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totMasterCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.totMasterCount.Location = new System.Drawing.Point(375, 4);
            this.totMasterCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.totMasterCount.Name = "totMasterCount";
            this.totMasterCount.Size = new System.Drawing.Size(156, 35);
            this.totMasterCount.TabIndex = 20;
            this.totMasterCount.Text = "0";
            this.totMasterCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.totMasterCount.Click += new System.EventHandler(this.totMasterCount_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label13.Location = new System.Drawing.Point(189, 4);
            this.label13.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(180, 35);
            this.label13.TabIndex = 24;
            this.label13.Text = "Total Master Count :";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlErrorPanel
            // 
            this.pnlErrorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlErrorPanel.Controls.Add(this.tblltErrorList);
            this.pnlErrorPanel.Location = new System.Drawing.Point(57, 15);
            this.pnlErrorPanel.Margin = new System.Windows.Forms.Padding(0);
            this.pnlErrorPanel.Name = "pnlErrorPanel";
            this.pnlErrorPanel.Size = new System.Drawing.Size(430, 320);
            this.pnlErrorPanel.TabIndex = 25;
            this.pnlErrorPanel.Visible = false;
            this.pnlErrorPanel.VisibleChanged += new System.EventHandler(this.pnlErrorPanel_VisibleChanged);
            // 
            // tblltErrorList
            // 
            this.tblltErrorList.ColumnCount = 2;
            this.tblltErrorList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltErrorList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblltErrorList.Controls.Add(this.lbltot1, 1, 0);
            this.tblltErrorList.Controls.Add(this.lblLabel10, 0, 9);
            this.tblltErrorList.Controls.Add(this.lblLabel9, 0, 8);
            this.tblltErrorList.Controls.Add(this.lbltot2, 1, 1);
            this.tblltErrorList.Controls.Add(this.lblLabel8, 0, 7);
            this.tblltErrorList.Controls.Add(this.lbltot9, 1, 8);
            this.tblltErrorList.Controls.Add(this.lblLabel7, 0, 6);
            this.tblltErrorList.Controls.Add(this.lbltot3, 1, 2);
            this.tblltErrorList.Controls.Add(this.lbltot4, 1, 3);
            this.tblltErrorList.Controls.Add(this.lbltot5, 1, 4);
            this.tblltErrorList.Controls.Add(this.lblLabel5, 0, 4);
            this.tblltErrorList.Controls.Add(this.lbltot8, 1, 7);
            this.tblltErrorList.Controls.Add(this.lblLabel4, 0, 3);
            this.tblltErrorList.Controls.Add(this.lbltot6, 1, 5);
            this.tblltErrorList.Controls.Add(this.lblLabel3, 0, 2);
            this.tblltErrorList.Controls.Add(this.lbltot7, 1, 6);
            this.tblltErrorList.Controls.Add(this.lblLabel2, 0, 1);
            this.tblltErrorList.Controls.Add(this.lblLabel1, 0, 0);
            this.tblltErrorList.Controls.Add(this.lblLabel6, 0, 5);
            this.tblltErrorList.Controls.Add(this.lbltot10, 1, 9);
            this.tblltErrorList.Controls.Add(this.tblltButtonOK, 0, 10);
            this.tblltErrorList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltErrorList.Location = new System.Drawing.Point(0, 0);
            this.tblltErrorList.Name = "tblltErrorList";
            this.tblltErrorList.RowCount = 11;
            this.tblltErrorList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tblltErrorList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tblltErrorList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tblltErrorList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tblltErrorList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tblltErrorList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tblltErrorList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tblltErrorList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tblltErrorList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tblltErrorList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tblltErrorList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.090909F));
            this.tblltErrorList.Size = new System.Drawing.Size(428, 318);
            this.tblltErrorList.TabIndex = 35;
            // 
            // lblLabel10
            // 
            this.lblLabel10.AutoSize = true;
            this.lblLabel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLabel10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel10.ForeColor = System.Drawing.Color.Red;
            this.lblLabel10.Location = new System.Drawing.Point(3, 255);
            this.lblLabel10.Margin = new System.Windows.Forms.Padding(3);
            this.lblLabel10.Name = "lblLabel10";
            this.lblLabel10.Size = new System.Drawing.Size(208, 22);
            this.lblLabel10.TabIndex = 33;
            this.lblLabel10.Text = "Errored Expense IDs :";
            this.lblLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLabel9
            // 
            this.lblLabel9.AutoSize = true;
            this.lblLabel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLabel9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel9.ForeColor = System.Drawing.Color.Red;
            this.lblLabel9.Location = new System.Drawing.Point(3, 227);
            this.lblLabel9.Margin = new System.Windows.Forms.Padding(3);
            this.lblLabel9.Name = "lblLabel9";
            this.lblLabel9.Size = new System.Drawing.Size(208, 22);
            this.lblLabel9.TabIndex = 31;
            this.lblLabel9.Text = "lblLabel9";
            this.lblLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLabel8
            // 
            this.lblLabel8.AutoSize = true;
            this.lblLabel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLabel8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel8.ForeColor = System.Drawing.Color.Red;
            this.lblLabel8.Location = new System.Drawing.Point(3, 199);
            this.lblLabel8.Margin = new System.Windows.Forms.Padding(3);
            this.lblLabel8.Name = "lblLabel8";
            this.lblLabel8.Size = new System.Drawing.Size(208, 22);
            this.lblLabel8.TabIndex = 28;
            this.lblLabel8.Text = "Errored Expense IDs :";
            this.lblLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbltot9
            // 
            this.lbltot9.AutoSize = true;
            this.lbltot9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltot9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltot9.ForeColor = System.Drawing.Color.Red;
            this.lbltot9.Location = new System.Drawing.Point(217, 227);
            this.lbltot9.Margin = new System.Windows.Forms.Padding(3);
            this.lbltot9.Name = "lbltot9";
            this.lbltot9.Size = new System.Drawing.Size(208, 22);
            this.lbltot9.TabIndex = 32;
            this.lbltot9.Text = "0";
            this.lbltot9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbltot9.Click += new System.EventHandler(this.lbltot9_Click);
            // 
            // lblLabel7
            // 
            this.lblLabel7.AutoSize = true;
            this.lblLabel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLabel7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel7.ForeColor = System.Drawing.Color.Red;
            this.lblLabel7.Location = new System.Drawing.Point(3, 171);
            this.lblLabel7.Margin = new System.Windows.Forms.Padding(3);
            this.lblLabel7.Name = "lblLabel7";
            this.lblLabel7.Size = new System.Drawing.Size(208, 22);
            this.lblLabel7.TabIndex = 26;
            this.lblLabel7.Text = "Errored Expense IDs :";
            this.lblLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLabel5
            // 
            this.lblLabel5.AutoSize = true;
            this.lblLabel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLabel5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel5.ForeColor = System.Drawing.Color.Red;
            this.lblLabel5.Location = new System.Drawing.Point(3, 115);
            this.lblLabel5.Margin = new System.Windows.Forms.Padding(3);
            this.lblLabel5.Name = "lblLabel5";
            this.lblLabel5.Size = new System.Drawing.Size(208, 22);
            this.lblLabel5.TabIndex = 21;
            this.lblLabel5.Text = "Errored Product IDs :";
            this.lblLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbltot8
            // 
            this.lbltot8.AutoSize = true;
            this.lbltot8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltot8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltot8.ForeColor = System.Drawing.Color.Red;
            this.lbltot8.Location = new System.Drawing.Point(217, 199);
            this.lbltot8.Margin = new System.Windows.Forms.Padding(3);
            this.lbltot8.Name = "lbltot8";
            this.lbltot8.Size = new System.Drawing.Size(208, 22);
            this.lbltot8.TabIndex = 29;
            this.lbltot8.Text = "0";
            this.lbltot8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbltot8.Click += new System.EventHandler(this.lbltot8_Click);
            // 
            // lblLabel4
            // 
            this.lblLabel4.AutoSize = true;
            this.lblLabel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLabel4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel4.ForeColor = System.Drawing.Color.Red;
            this.lblLabel4.Location = new System.Drawing.Point(3, 87);
            this.lblLabel4.Margin = new System.Windows.Forms.Padding(3);
            this.lblLabel4.Name = "lblLabel4";
            this.lblLabel4.Size = new System.Drawing.Size(208, 22);
            this.lblLabel4.TabIndex = 19;
            this.lblLabel4.Text = "Errored ManuComp IDs :";
            this.lblLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbltot6
            // 
            this.lbltot6.AutoSize = true;
            this.lbltot6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltot6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltot6.ForeColor = System.Drawing.Color.Red;
            this.lbltot6.Location = new System.Drawing.Point(217, 143);
            this.lbltot6.Margin = new System.Windows.Forms.Padding(3);
            this.lbltot6.Name = "lbltot6";
            this.lbltot6.Size = new System.Drawing.Size(208, 22);
            this.lbltot6.TabIndex = 27;
            this.lbltot6.Text = "0";
            this.lbltot6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbltot6.Click += new System.EventHandler(this.lbltot6_Click);
            // 
            // lblLabel3
            // 
            this.lblLabel3.AutoSize = true;
            this.lblLabel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLabel3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel3.ForeColor = System.Drawing.Color.Red;
            this.lblLabel3.Location = new System.Drawing.Point(3, 59);
            this.lblLabel3.Margin = new System.Windows.Forms.Padding(3);
            this.lblLabel3.Name = "lblLabel3";
            this.lblLabel3.Size = new System.Drawing.Size(208, 22);
            this.lblLabel3.TabIndex = 17;
            this.lblLabel3.Text = "Errored Unit IDs :";
            this.lblLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLabel2
            // 
            this.lblLabel2.AutoSize = true;
            this.lblLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLabel2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel2.ForeColor = System.Drawing.Color.Red;
            this.lblLabel2.Location = new System.Drawing.Point(3, 31);
            this.lblLabel2.Margin = new System.Windows.Forms.Padding(3);
            this.lblLabel2.Name = "lblLabel2";
            this.lblLabel2.Size = new System.Drawing.Size(208, 22);
            this.lblLabel2.TabIndex = 15;
            this.lblLabel2.Text = "Errored Suuplier IDs :";
            this.lblLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLabel1
            // 
            this.lblLabel1.AutoSize = true;
            this.lblLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLabel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel1.ForeColor = System.Drawing.Color.Red;
            this.lblLabel1.Location = new System.Drawing.Point(3, 3);
            this.lblLabel1.Margin = new System.Windows.Forms.Padding(3);
            this.lblLabel1.Name = "lblLabel1";
            this.lblLabel1.Size = new System.Drawing.Size(208, 22);
            this.lblLabel1.TabIndex = 13;
            this.lblLabel1.Text = "Total Customer Names :";
            this.lblLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLabel6
            // 
            this.lblLabel6.AutoSize = true;
            this.lblLabel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLabel6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabel6.ForeColor = System.Drawing.Color.Red;
            this.lblLabel6.Location = new System.Drawing.Point(3, 143);
            this.lblLabel6.Margin = new System.Windows.Forms.Padding(3);
            this.lblLabel6.Name = "lblLabel6";
            this.lblLabel6.Size = new System.Drawing.Size(208, 22);
            this.lblLabel6.TabIndex = 23;
            this.lblLabel6.Text = "Errored Expense IDs :";
            this.lblLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbltot10
            // 
            this.lbltot10.AutoSize = true;
            this.lbltot10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltot10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltot10.ForeColor = System.Drawing.Color.Red;
            this.lbltot10.Location = new System.Drawing.Point(217, 255);
            this.lbltot10.Margin = new System.Windows.Forms.Padding(3);
            this.lbltot10.Name = "lbltot10";
            this.lbltot10.Size = new System.Drawing.Size(208, 22);
            this.lbltot10.TabIndex = 34;
            this.lbltot10.Text = "0";
            this.lbltot10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbltot10.Click += new System.EventHandler(this.lbltot10_Click);
            // 
            // tblltButtonOK
            // 
            this.tblltButtonOK.ColumnCount = 3;
            this.tblltErrorList.SetColumnSpan(this.tblltButtonOK, 2);
            this.tblltButtonOK.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblltButtonOK.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltButtonOK.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tblltButtonOK.Controls.Add(this.btnOK, 1, 0);
            this.tblltButtonOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltButtonOK.Location = new System.Drawing.Point(0, 280);
            this.tblltButtonOK.Margin = new System.Windows.Forms.Padding(0);
            this.tblltButtonOK.Name = "tblltButtonOK";
            this.tblltButtonOK.RowCount = 1;
            this.tblltButtonOK.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltButtonOK.Size = new System.Drawing.Size(428, 38);
            this.tblltButtonOK.TabIndex = 36;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(173, 2);
            this.btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(81, 26);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // totTransactionCount
            // 
            this.totTransactionCount.AutoSize = true;
            this.totTransactionCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.totTransactionCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totTransactionCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.totTransactionCount.Location = new System.Drawing.Point(375, 47);
            this.totTransactionCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.totTransactionCount.Name = "totTransactionCount";
            this.totTransactionCount.Size = new System.Drawing.Size(156, 35);
            this.totTransactionCount.TabIndex = 26;
            this.totTransactionCount.Text = "0";
            this.totTransactionCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.totTransactionCount.Click += new System.EventHandler(this.totTransactionCount_Click);
            // 
            // barprgrsbar
            // 
            this.barprgrsbar.Location = new System.Drawing.Point(33, 160);
            this.barprgrsbar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.barprgrsbar.Name = "barprgrsbar";
            this.barprgrsbar.Size = new System.Drawing.Size(479, 30);
            this.barprgrsbar.TabIndex = 27;
            this.barprgrsbar.Visible = false;
            // 
            // tblltMain
            // 
            this.tblltMain.ColumnCount = 3;
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tblltMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tblltMain.Controls.Add(this.btnTransactionExport, 0, 1);
            this.tblltMain.Controls.Add(this.btnMasterExport, 0, 0);
            this.tblltMain.Controls.Add(this.label13, 1, 0);
            this.tblltMain.Controls.Add(this.totMasterCount, 2, 0);
            this.tblltMain.Controls.Add(this.pnlDateExport, 0, 3);
            this.tblltMain.Controls.Add(this.label15, 1, 1);
            this.tblltMain.Controls.Add(this.totTransactionCount, 2, 1);
            this.tblltMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltMain.Location = new System.Drawing.Point(0, 0);
            this.tblltMain.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tblltMain.Name = "tblltMain";
            this.tblltMain.RowCount = 5;
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41F));
            this.tblltMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tblltMain.Size = new System.Drawing.Size(534, 362);
            this.tblltMain.TabIndex = 0;
            // 
            // btnTransactionExport
            // 
            this.btnTransactionExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnTransactionExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnTransactionExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTransactionExport.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransactionExport.ForeColor = System.Drawing.Color.White;
            this.btnTransactionExport.Location = new System.Drawing.Point(2, 46);
            this.btnTransactionExport.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnTransactionExport.Name = "btnTransactionExport";
            this.btnTransactionExport.Size = new System.Drawing.Size(182, 37);
            this.btnTransactionExport.TabIndex = 2;
            this.btnTransactionExport.Text = "Export Transactions";
            this.btnTransactionExport.UseVisualStyleBackColor = false;
            this.btnTransactionExport.Click += new System.EventHandler(this.btnTransactionExport_Click);
            // 
            // btnMasterExport
            // 
            this.btnMasterExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnMasterExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnMasterExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMasterExport.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMasterExport.ForeColor = System.Drawing.Color.White;
            this.btnMasterExport.Location = new System.Drawing.Point(2, 3);
            this.btnMasterExport.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnMasterExport.Name = "btnMasterExport";
            this.btnMasterExport.Size = new System.Drawing.Size(182, 37);
            this.btnMasterExport.TabIndex = 1;
            this.btnMasterExport.Text = "Export Masters";
            this.btnMasterExport.UseVisualStyleBackColor = false;
            this.btnMasterExport.Click += new System.EventHandler(this.btnMasterExport_Click);
            // 
            // pnlDetailedErrorPanel
            // 
            this.pnlDetailedErrorPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDetailedErrorPanel.Controls.Add(this.tblltDetail);
            this.pnlDetailedErrorPanel.Location = new System.Drawing.Point(72, 30);
            this.pnlDetailedErrorPanel.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDetailedErrorPanel.Name = "pnlDetailedErrorPanel";
            this.pnlDetailedErrorPanel.Size = new System.Drawing.Size(400, 300);
            this.pnlDetailedErrorPanel.TabIndex = 32;
            this.pnlDetailedErrorPanel.Visible = false;
            // 
            // tblltDetail
            // 
            this.tblltDetail.ColumnCount = 1;
            this.tblltDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblltDetail.Controls.Add(this.btnErrorOK, 0, 2);
            this.tblltDetail.Controls.Add(this.lblErrorLabel, 0, 0);
            this.tblltDetail.Controls.Add(this.dtgvErrorList, 0, 1);
            this.tblltDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblltDetail.Location = new System.Drawing.Point(0, 0);
            this.tblltDetail.Name = "tblltDetail";
            this.tblltDetail.RowCount = 3;
            this.tblltDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tblltDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82F));
            this.tblltDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tblltDetail.Size = new System.Drawing.Size(398, 298);
            this.tblltDetail.TabIndex = 33;
            // 
            // btnErrorOK
            // 
            this.btnErrorOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnErrorOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnErrorOK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnErrorOK.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnErrorOK.ForeColor = System.Drawing.Color.White;
            this.btnErrorOK.Location = new System.Drawing.Point(160, 269);
            this.btnErrorOK.Margin = new System.Windows.Forms.Padding(160, 2, 160, 2);
            this.btnErrorOK.Name = "btnErrorOK";
            this.btnErrorOK.Size = new System.Drawing.Size(78, 27);
            this.btnErrorOK.TabIndex = 8;
            this.btnErrorOK.Text = "OK";
            this.btnErrorOK.UseVisualStyleBackColor = false;
            // 
            // lblErrorLabel
            // 
            this.lblErrorLabel.AutoSize = true;
            this.lblErrorLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblErrorLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.lblErrorLabel.Location = new System.Drawing.Point(3, 3);
            this.lblErrorLabel.Margin = new System.Windows.Forms.Padding(3);
            this.lblErrorLabel.Name = "lblErrorLabel";
            this.lblErrorLabel.Size = new System.Drawing.Size(392, 17);
            this.lblErrorLabel.TabIndex = 30;
            this.lblErrorLabel.Text = "0";
            // 
            // dtgvErrorList
            // 
            this.dtgvErrorList.BackgroundColor = System.Drawing.Color.White;
            this.dtgvErrorList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgvErrorList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgvErrorList.GridColor = System.Drawing.Color.WhiteSmoke;
            this.dtgvErrorList.Location = new System.Drawing.Point(3, 26);
            this.dtgvErrorList.Name = "dtgvErrorList";
            this.dtgvErrorList.Size = new System.Drawing.Size(392, 238);
            this.dtgvErrorList.TabIndex = 31;
            // 
            // frmTallyExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(534, 362);
            this.Controls.Add(this.barprgrsbar);
            this.Controls.Add(this.pnlDetailedErrorPanel);
            this.Controls.Add(this.pnlErrorPanel);
            this.Controls.Add(this.tblltMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmTallyExport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tally Export";
            this.pnlDateExport.ResumeLayout(false);
            this.tblltDates.ResumeLayout(false);
            this.tblltDates.PerformLayout();
            this.pnlErrorPanel.ResumeLayout(false);
            this.tblltErrorList.ResumeLayout(false);
            this.tblltErrorList.PerformLayout();
            this.tblltButtonOK.ResumeLayout(false);
            this.tblltMain.ResumeLayout(false);
            this.tblltMain.PerformLayout();
            this.pnlDetailedErrorPanel.ResumeLayout(false);
            this.tblltDetail.ResumeLayout(false);
            this.tblltDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgvErrorList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlDateExport;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpfromDate;
        private System.Windows.Forms.Label lbltot1;
        private System.Windows.Forms.Label lbltot2;
        private System.Windows.Forms.Label lbltot3;
        private System.Windows.Forms.Label lbltot4;
        private System.Windows.Forms.Label lbltot5;
        private System.Windows.Forms.Label lbltot7;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label totMasterCount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel pnlErrorPanel;
        private System.Windows.Forms.Label lblLabel6;
        private System.Windows.Forms.Label lblLabel5;
        private System.Windows.Forms.Label lblLabel4;
        private System.Windows.Forms.Label lblLabel3;
        private System.Windows.Forms.Label lblLabel2;
        private System.Windows.Forms.Label lblLabel1;
        private System.Windows.Forms.Label lbltot6;
        private System.Windows.Forms.Label lblLabel7;
        private System.Windows.Forms.Label totTransactionCount;
        private System.Windows.Forms.Label lblLabel8;
        private System.Windows.Forms.Label lbltot8;
        private System.Windows.Forms.ProgressBar barprgrsbar;
        private System.Windows.Forms.Label lbltot9;
        private System.Windows.Forms.Label lblLabel9;
        private System.Windows.Forms.Label lbltot10;
        private System.Windows.Forms.Label lblLabel10;
        private System.Windows.Forms.TableLayoutPanel tblltMain;
        private System.Windows.Forms.Button btnTransactionExport;
        private System.Windows.Forms.Button btnMasterExport;
        private System.Windows.Forms.TableLayoutPanel tblltDates;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TableLayoutPanel tblltErrorList;
        private System.Windows.Forms.TableLayoutPanel tblltButtonOK;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel pnlDetailedErrorPanel;
        private System.Windows.Forms.TableLayoutPanel tblltDetail;
        private System.Windows.Forms.Button btnErrorOK;
        private System.Windows.Forms.Label lblErrorLabel;
        private System.Windows.Forms.DataGridView dtgvErrorList;
    }
}