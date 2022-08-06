using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmFestivals : Form
    {
        public int updateId, updateIdForFestivalTem;
        DbClass db = new DbClass();

        public frmFestivals()
        {
            InitializeComponent();
            BindData();
            BindFestivalMessageData();
            FillCombo();
            MaxFestivalId();
            MaxFestivalTemplateId();
        }

        public void MaxFestivalTemplateId()
        {
            try
            {
                FestivalTemplateRepository festtempRepo = new FestivalTemplateRepository();
                var mid = festtempRepo.GetAll().Max(t => t.FestivalTemplateNo);
                if (mid == null)
                    txtFestivalTemplateNo.Text = "1";
                else
                    txtFestivalTemplateNo.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                festtempRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void MaxFestivalId()
        {
            try
            {
                FestivalRepository festRepo = new FestivalRepository();
                var mid = festRepo.GetAll().Max(t => t.FestivalNo);
                if (mid == null)
                    txtFestivalNo.Text = "1";
                else
                    txtFestivalNo.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                festRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void FillCombo()
        {
            try
            {
                FestivalRepository festRepo = new FestivalRepository();
                List<Festival> festData = new List<Festival>();
                festData.Clear();
                festData = festRepo.GetAll().OrderByDescending(t => t.FestivalName).ToList();
                festData.Add(new Festival { FestivalName = "", FestivalNo = 0 });
                festData.Reverse();
                cmbFestival.DataSource = festData;
                cmbFestival.ValueMember = "FestivalNo";
                cmbFestival.DisplayMember = "FestivalName";
                festRepo.Dispose();
                cmbFestival.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void BindData()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT Id,FestivalName, FestivalDate, FestivalDesc FROM Festivals", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GvFestival.DataSource = dt;
                GvFestival.Refresh();
                GvFestival.Columns["Id"].Width = 60;
                GvFestival.Columns["FestivalName"].Width = 125;
                GvFestival.Columns["FestivalDate"].Width = 80;
                GvFestival.Columns["FestivalDesc"].Width = 150;
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    this.Close();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void BindFestivalMessageData()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT Id,FestivalName, FestivalsMessage FROM FestivalTemplates", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvFestivalTemplate.DataSource = dt;
                GvFestivalTemplate.Refresh();
                GvFestivalTemplate.Columns["Id"].Width = 60;
                GvFestivalTemplate.Columns["FestivalName"].Width = 155;
                GvFestivalTemplate.Columns["FestivalsMessage"].Width = 200;
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            txtFestivalName.Text = "";
            txtFestivalDesc.Text = "";
            dtpFestivalDate.Value = DateTime.Now.Date;
            cmbFestival.SelectedIndex = 0;
            btnSave.Text = "Save";
            BindData();
            MaxFestivalId();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFestivalName.Text.Trim() != "")
                {
                    FestivalRepository festRepo = new FestivalRepository();
                    Festival festData = new Festival();
                    festData.FestivalNo = Convert.ToInt32(txtFestivalNo.Text);
                    festData.FestivalName = txtFestivalName.Text.Trim();
                    festData.FestivalDate = Convert.ToDateTime(dtpFestivalDate.Text);
                    festData.FestivalDesc = txtFestivalDesc.Text.Trim();
                    if (btnSave.Text == "Save")
                    {
                        festRepo.Add(festData);
                        festRepo.Save();
                        MessageBox.Show("Record save successfully.", "Success");
                    }
                    else
                    {
                        festData.Id = updateId;
                        festRepo.Edit(festData);
                        festRepo.Save();
                        MessageBox.Show("Record updated successfully.", "Success");
                    }
                    this.ActiveControl = txtFestivalName;
                    Clear();
                    festRepo.Dispose();
                }
                else
                    MessageBox.Show("Please select Festival Name.", "Warning");
            }
            catch (Exception)
            { }
        }

        private void GvFestival_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                updateId = Convert.ToInt32(GvFestival.Rows[e.RowIndex].Cells[0].Value);
                txtFestivalName.Text = Convert.ToString(GvFestival.Rows[e.RowIndex].Cells[1].Value);
                dtpFestivalDate.Text = Convert.ToString(GvFestival.Rows[e.RowIndex].Cells[2].Value);
                txtFestivalDesc.Text = Convert.ToString(GvFestival.Rows[e.RowIndex].Cells[3].Value);
                btnSave.Text = "Update";
                this.ActiveControl = txtFestivalName;
            }
            catch (Exception)
            { }
        }

        public void clearTemp()
        {
            cmbFestival.SelectedIndex = 0;
            txtFetivalMsg.Text = "";
            btnTempSave.Text = "Save";
            BindFestivalMessageData();
            MaxFestivalTemplateId();
        }

        private void GvFestivalTemplate_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                updateIdForFestivalTem = Convert.ToInt32(GvFestivalTemplate.Rows[e.RowIndex].Cells[0].Value);
                cmbFestival.Text = Convert.ToString(GvFestivalTemplate.Rows[e.RowIndex].Cells[1].Value);
                txtFetivalMsg.Text = Convert.ToString(GvFestivalTemplate.Rows[e.RowIndex].Cells[2].Value);
                btnTempSave.Text = "Update";
                this.ActiveControl = cmbFestival;
            }
            catch (Exception)
            { }
        }

        private void txtFetivalMsg_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtFetivalMsg.Text.Length > 160)
                {
                    txtFetivalMsg.Text = txtFetivalMsg.Text.Remove(txtFetivalMsg.Text.Length - (txtFetivalMsg.Text.Length - 160));
                    MessageBox.Show("Enter Only 160 Charater.", "Warning");
                }
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void frmFestivals_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void btnTempNew_Click(object sender, EventArgs e)
        {
            clearTemp();
        }

        private void btnTempSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbFestival.Text.Trim() != "")
                {
                    if (txtFetivalMsg.Text.Length < 160)
                    {
                        FestivalTemplateRepository festRepo = new FestivalTemplateRepository();
                        FestivalTemplate festData = new FestivalTemplate();
                        festData.FestivalTemplateNo = Convert.ToInt32(txtFestivalTemplateNo.Text);
                        festData.FestivalId = Convert.ToInt32(cmbFestival.SelectedValue);
                        festData.FestivalName = cmbFestival.Text.Trim();
                        festData.FestivalsMessage = txtFetivalMsg.Text.Trim();
                        if (btnTempSave.Text == "Save")
                        {
                            festRepo.Add(festData);
                            festRepo.Save();
                            MessageBox.Show("Record save successfully.", "Success");
                        }
                        else
                        {
                            festData.Id = updateIdForFestivalTem;
                            festRepo.Edit(festData);
                            festRepo.Save();
                            MessageBox.Show("Record updated successfully.", "Success");
                        }
                        this.ActiveControl = txtFestivalName;
                        clearTemp();
                        festRepo.Dispose();
                    }
                    else
                        MessageBox.Show("Message lenghth is over.", "Warning");
                }
                else
                    MessageBox.Show("Please select Festival Name.", "Warning");
            }
            catch (Exception)
            { }
        }

        private void btnTempClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }
    }
}