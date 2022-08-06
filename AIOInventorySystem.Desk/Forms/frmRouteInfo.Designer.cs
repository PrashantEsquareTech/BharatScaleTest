namespace AIOInventorySystem.Desk.Forms
{
    partial class frmRouteInfo
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
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtVillageName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnVillageSave = new System.Windows.Forms.Button();
            this.btnVillageDelete = new System.Windows.Forms.Button();
            this.grdVillage = new System.Windows.Forms.DataGridView();
            this.grdRoute = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRouteName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnRouteDelete = new System.Windows.Forms.Button();
            this.btnRouteSave = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbRouteName = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRouteId = new System.Windows.Forms.TextBox();
            this.txtVillageId = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.PID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRouteID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RouteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VillageID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VillageName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RouteNameGrd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdVillage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRoute)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.label5.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, -2);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(764, 41);
            this.label5.TabIndex = 11;
            this.label5.Text = " Route Information";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label1.Location = new System.Drawing.Point(371, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Vilage Name";
            // 
            // txtVillageName
            // 
            this.txtVillageName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVillageName.Location = new System.Drawing.Point(512, 137);
            this.txtVillageName.Name = "txtVillageName";
            this.txtVillageName.Size = new System.Drawing.Size(205, 27);
            this.txtVillageName.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(349, 52);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(406, 36);
            this.label2.TabIndex = 14;
            this.label2.Text = " Create Village";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnVillageSave
            // 
            this.btnVillageSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnVillageSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnVillageSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVillageSave.ForeColor = System.Drawing.Color.White;
            this.btnVillageSave.Location = new System.Drawing.Point(510, 210);
            this.btnVillageSave.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnVillageSave.Name = "btnVillageSave";
            this.btnVillageSave.Size = new System.Drawing.Size(74, 31);
            this.btnVillageSave.TabIndex = 15;
            this.btnVillageSave.Text = "Save";
            this.btnVillageSave.UseVisualStyleBackColor = false;
            this.btnVillageSave.Click += new System.EventHandler(this.btnVillageSave_Click);
            // 
            // btnVillageDelete
            // 
            this.btnVillageDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnVillageDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnVillageDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVillageDelete.ForeColor = System.Drawing.Color.White;
            this.btnVillageDelete.Location = new System.Drawing.Point(588, 210);
            this.btnVillageDelete.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnVillageDelete.Name = "btnVillageDelete";
            this.btnVillageDelete.Size = new System.Drawing.Size(74, 31);
            this.btnVillageDelete.TabIndex = 17;
            this.btnVillageDelete.Text = "Delete";
            this.btnVillageDelete.UseVisualStyleBackColor = false;
            // 
            // grdVillage
            // 
            this.grdVillage.AllowUserToAddRows = false;
            this.grdVillage.AllowUserToDeleteRows = false;
            this.grdVillage.BackgroundColor = System.Drawing.Color.White;
            this.grdVillage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdVillage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.VillageID,
            this.VillageName,
            this.RouteNameGrd});
            this.grdVillage.GridColor = System.Drawing.Color.WhiteSmoke;
            this.grdVillage.Location = new System.Drawing.Point(355, 259);
            this.grdVillage.Name = "grdVillage";
            this.grdVillage.ReadOnly = true;
            this.grdVillage.RowHeadersWidth = 15;
            this.grdVillage.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdVillage.Size = new System.Drawing.Size(400, 231);
            this.grdVillage.TabIndex = 18;
            this.grdVillage.TabStop = false;
            this.grdVillage.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdVillage_CellClick);
            // 
            // grdRoute
            // 
            this.grdRoute.AllowUserToAddRows = false;
            this.grdRoute.AllowUserToDeleteRows = false;
            this.grdRoute.BackgroundColor = System.Drawing.Color.White;
            this.grdRoute.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRoute.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PID,
            this.PRouteID,
            this.RouteName});
            this.grdRoute.GridColor = System.Drawing.Color.WhiteSmoke;
            this.grdRoute.Location = new System.Drawing.Point(16, 211);
            this.grdRoute.Name = "grdRoute";
            this.grdRoute.ReadOnly = true;
            this.grdRoute.RowHeadersWidth = 15;
            this.grdRoute.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdRoute.Size = new System.Drawing.Size(307, 282);
            this.grdRoute.TabIndex = 23;
            this.grdRoute.TabStop = false;
            this.grdRoute.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdRoute_CellClick);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.label3.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(18, 52);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(305, 36);
            this.label3.TabIndex = 20;
            this.label3.Text = " Create Route";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtRouteName
            // 
            this.txtRouteName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRouteName.Location = new System.Drawing.Point(150, 134);
            this.txtRouteName.Name = "txtRouteName";
            this.txtRouteName.Size = new System.Drawing.Size(173, 27);
            this.txtRouteName.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(20, 139);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 20);
            this.label4.TabIndex = 24;
            this.label4.Text = "Route Name";
            // 
            // btnRouteDelete
            // 
            this.btnRouteDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnRouteDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRouteDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRouteDelete.ForeColor = System.Drawing.Color.White;
            this.btnRouteDelete.Location = new System.Drawing.Point(203, 175);
            this.btnRouteDelete.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnRouteDelete.Name = "btnRouteDelete";
            this.btnRouteDelete.Size = new System.Drawing.Size(74, 31);
            this.btnRouteDelete.TabIndex = 27;
            this.btnRouteDelete.Text = "Delete";
            this.btnRouteDelete.UseVisualStyleBackColor = false;
            // 
            // btnRouteSave
            // 
            this.btnRouteSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(52)))), ((int)(((byte)(142)))));
            this.btnRouteSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRouteSave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRouteSave.ForeColor = System.Drawing.Color.White;
            this.btnRouteSave.Location = new System.Drawing.Point(125, 176);
            this.btnRouteSave.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.btnRouteSave.Name = "btnRouteSave";
            this.btnRouteSave.Size = new System.Drawing.Size(74, 31);
            this.btnRouteSave.TabIndex = 25;
            this.btnRouteSave.Text = "Save";
            this.btnRouteSave.UseVisualStyleBackColor = false;
            this.btnRouteSave.Click += new System.EventHandler(this.btnRouteSave_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label6.Location = new System.Drawing.Point(371, 171);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 20);
            this.label6.TabIndex = 28;
            this.label6.Text = "Select Route";
            // 
            // cmbRouteName
            // 
            this.cmbRouteName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRouteName.FormattingEnabled = true;
            this.cmbRouteName.Location = new System.Drawing.Point(512, 171);
            this.cmbRouteName.Name = "cmbRouteName";
            this.cmbRouteName.Size = new System.Drawing.Size(205, 28);
            this.cmbRouteName.TabIndex = 29;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label7.Location = new System.Drawing.Point(20, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 20);
            this.label7.TabIndex = 31;
            this.label7.Text = "Route ID";
            // 
            // txtRouteId
            // 
            this.txtRouteId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRouteId.Location = new System.Drawing.Point(150, 103);
            this.txtRouteId.Name = "txtRouteId";
            this.txtRouteId.Size = new System.Drawing.Size(173, 27);
            this.txtRouteId.TabIndex = 30;
            // 
            // txtVillageId
            // 
            this.txtVillageId.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVillageId.Location = new System.Drawing.Point(512, 106);
            this.txtVillageId.Name = "txtVillageId";
            this.txtVillageId.Size = new System.Drawing.Size(205, 27);
            this.txtVillageId.TabIndex = 33;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label8.Location = new System.Drawing.Point(371, 106);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 20);
            this.label8.TabIndex = 32;
            this.label8.Text = "Vilage ID";
            // 
            // PID
            // 
            this.PID.HeaderText = "ID";
            this.PID.Name = "PID";
            this.PID.ReadOnly = true;
            this.PID.Visible = false;
            this.PID.Width = 50;
            // 
            // PRouteID
            // 
            this.PRouteID.HeaderText = "Route ID";
            this.PRouteID.Name = "PRouteID";
            this.PRouteID.ReadOnly = true;
            this.PRouteID.Width = 80;
            // 
            // RouteName
            // 
            this.RouteName.HeaderText = "Route Name";
            this.RouteName.Name = "RouteName";
            this.RouteName.ReadOnly = true;
            this.RouteName.Width = 200;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            this.ID.Width = 150;
            // 
            // VillageID
            // 
            this.VillageID.HeaderText = "VillageID";
            this.VillageID.Name = "VillageID";
            this.VillageID.ReadOnly = true;
            this.VillageID.Width = 80;
            // 
            // VillageName
            // 
            this.VillageName.HeaderText = "VillageName";
            this.VillageName.Name = "VillageName";
            this.VillageName.ReadOnly = true;
            this.VillageName.Width = 150;
            // 
            // RouteNameGrd
            // 
            this.RouteNameGrd.HeaderText = "RouteName";
            this.RouteNameGrd.Name = "RouteNameGrd";
            this.RouteNameGrd.ReadOnly = true;
            this.RouteNameGrd.Width = 150;
            // 
            // frmRouteInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 593);
            this.Controls.Add(this.txtVillageId);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtRouteId);
            this.Controls.Add(this.cmbRouteName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnRouteDelete);
            this.Controls.Add(this.btnRouteSave);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.grdRoute);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRouteName);
            this.Controls.Add(this.grdVillage);
            this.Controls.Add(this.btnVillageDelete);
            this.Controls.Add(this.btnVillageSave);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtVillageName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmRouteInfo";
            this.Text = "frmRouteInfo";
            ((System.ComponentModel.ISupportInitialize)(this.grdVillage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRoute)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtVillageName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnVillageSave;
        private System.Windows.Forms.Button btnVillageDelete;
        private System.Windows.Forms.DataGridView grdVillage;
        private System.Windows.Forms.DataGridView grdRoute;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRouteName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnRouteDelete;
        private System.Windows.Forms.Button btnRouteSave;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbRouteName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRouteId;
        private System.Windows.Forms.TextBox txtVillageId;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn PID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRouteID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RouteName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn VillageID;
        private System.Windows.Forms.DataGridViewTextBoxColumn VillageName;
        private System.Windows.Forms.DataGridViewTextBoxColumn RouteNameGrd;

    }
}