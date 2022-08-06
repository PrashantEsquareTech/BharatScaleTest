using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmUserSeries : Form
    {
        DbClass db = new DbClass();

        public frmUserSeries()
        {
            InitializeComponent();
            clearFormControls();
            fillUserSeries();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearFormControls();
        }

        private void clearFormControls()
        {
            txtStartChar.Text = "";
            txtStartNumber.Text = "";
            txtSeparator.Text = "";
            this.ActiveControl = txtStartChar;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UserSeriesRepository userseriesRepo = new UserSeriesRepository();
                UserSeriesOfVoucher userseriesData = userseriesRepo.GetAll().Where(t => t.StartCharacter == txtStartChar.Text && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (userseriesData != null)
                {
                    userseriesData.StartNumber = Convert.ToInt32(txtStartNumber.Text);
                    userseriesData.UseSeparator = txtSeparator.Text;
                    userseriesData.CompId = CommonMethod.CompId;
                    userseriesRepo.Edit(userseriesData);
                    userseriesRepo.Save();
                }
                else
                {
                    UserSeriesOfVoucher seriesData = new UserSeriesOfVoucher();
                    seriesData.StartCharacter = txtStartChar.Text;
                    seriesData.StartNumber = Convert.ToInt32(txtStartNumber.Text);
                    seriesData.UseSeparator = txtSeparator.Text;
                    seriesData.CompId = CommonMethod.CompId;
                    userseriesRepo.Add(seriesData);
                    userseriesRepo.Save();
                    MessageBox.Show("Record Saved!!");
                    clearFormControls();
                }
                userseriesRepo.Dispose();
                fillUserSeries();
            }
            catch (Exception)
            { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                db.connect();
                SqlCommand cmd = new SqlCommand("select count(id) from customerbillmaster where userseries like '" + txtStartChar.Text.Trim() + "%'", db.Connection);
                var mid = cmd.ExecuteScalar();
                if (mid == null || Convert.ToInt32(mid) == 0)
                {
                    UserSeriesRepository userrepo = new UserSeriesRepository();
                    UserSeriesOfVoucher seriesData = userrepo.GetAll().Where(t => t.StartCharacter == txtStartChar.Text && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (seriesData != null)
                    {
                        userrepo.Remove(seriesData);
                        userrepo.Save();
                        MessageBox.Show("Record Deleted Sucessfully!");
                        clearFormControls();
                        fillUserSeries();
                    }
                    userrepo.Dispose();
                }
                else
                {
                    MessageBox.Show("This series is used, you can not delete it!");
                    cmd = null;
                    db.CloseConnection();
                }
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void fillUserSeries()
        {
            try
            {
                UserSeriesRepository userRepo = new UserSeriesRepository();
                List<UserSeriesOfVoucher> seriesData = userRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).ToList();
                dtgUserSeries.Rows.Clear();
                int i = 0;
                foreach (var item in seriesData)
                {
                    dtgUserSeries.Rows.Add();
                    dtgUserSeries.Rows[i].Cells["StartCharacter"].Value = item.StartCharacter.ToString();
                    dtgUserSeries.Rows[i].Cells["Separator"].Value = item.UseSeparator.ToString();
                    dtgUserSeries.Rows[i].Cells["StartByNumber"].Value = item.StartNumber.ToString();
                    dtgUserSeries.Rows[i].Cells["CompId"].Value = item.CompId.ToString();
                    i++;
                }
            }
            catch (Exception)
            { }
        }

        private void dtgUserSeries_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(dtgUserSeries.Rows[e.RowIndex].Cells["CompId"].Value)))
                {
                    txtStartChar.Text = Convert.ToString(dtgUserSeries.Rows[e.RowIndex].Cells["StartCharacter"].Value);
                    txtSeparator.Text = Convert.ToString(dtgUserSeries.Rows[e.RowIndex].Cells["Separator"].Value);
                    txtStartNumber.Text = Convert.ToString(dtgUserSeries.Rows[e.RowIndex].Cells["StartByNumber"].Value);
                }
            }
            catch (Exception)
            { }
        }
    }
}
