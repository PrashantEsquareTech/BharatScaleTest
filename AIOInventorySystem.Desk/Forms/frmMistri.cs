using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmMistri : Form
    {
        DbClass db = new DbClass();
        public int MistriId;

        public frmMistri()
        {
            InitializeComponent();
            maxcustomerid();
            Mistrifillcombo();
            BindMistriinfo();
        }

        public void maxcustomerid()
        {
            try
            {
                MistriRepository MistriRepo = new MistriRepository();
                var mid = MistriRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.MistriCode);
                if (mid == null)
                    txtMistriID.Text = "1";
                else
                    txtMistriID.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                MistriRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            txtMistriName.Text = "";
            txtMistriAddress.Text = "";
            txtMistriContactNo.Text = "";
            txtMistriEmailID.Text = "";
            maxcustomerid();
            BindMistriinfo();
            Mistrifillcombo();
            btnSave.Text = "Save";
        }

        public void Mistrifillcombo()
        {
            try
            {
                MistriRepository MistriRepo = new MistriRepository();
                List<MistriInformation> mistridata = new List<MistriInformation>();
                mistridata.Clear();
                mistridata = MistriRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.MistriName).ToList();
                mistridata.Add(new MistriInformation { MistriName = "", MistriCode = 0 });
                mistridata.Reverse();
                cmbMechname.DataSource = mistridata;
                cmbMechname.ValueMember = "MistriCode";
                cmbMechname.DisplayMember = "MistriName";
                MistriRepo.Dispose();
                cmbMechname.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void BindMistriinfo()
        {
            try
            {
                MistriRepository MistriRepo = new MistriRepository();
                GvMistriInfo.Rows.Clear();
                List<MistriInformation> ItemList = MistriRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).ToList();
                if (ItemList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        GvMistriInfo.Rows.Add();
                        GvMistriInfo.Rows[i].Cells["MechanicName"].Value = item.MistriName;
                        GvMistriInfo.Rows[i].Cells["Address"].Value = item.MistriAddress;
                        GvMistriInfo.Rows[i].Cells["ContactNo"].Value = item.ContactNo;
                        GvMistriInfo.Rows[i].Cells["EmailId"].Value = item.EmailId;
                        GvMistriInfo.Rows[i].Cells["MistriCode"].Value = item.MistriCode;
                        GvMistriInfo.Rows[i].Cells["Id"].Value = item.Id;
                        i++;
                    }
                }
                MistriRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                MistriRepository MistriRepo = new MistriRepository();
                MistriInformation MistriInfo = new MistriInformation();
                if (txtMistriName.Text == "")
                {
                    MessageBox.Show("Enter Mechanic Name.", "Warning");
                    clear();
                    this.ActiveControl = txtMistriName;
                }
                else
                {
                    MistriInfo.MistriName = txtMistriName.Text;
                    MistriInfo.MistriAddress = txtMistriAddress.Text;
                    MistriInfo.ContactNo = txtMistriContactNo.Text;
                    MistriInfo.EmailId = txtMistriEmailID.Text;
                    MistriInfo.CompId = CommonMethod.CompId;
                    if (btnSave.Text == "Update")
                    {
                        MistriInfo.MistriCode = Convert.ToInt32(txtMistriID.Text);
                        MistriInfo.Id = MistriId;
                        MistriInfo.CompId = CommonMethod.CompId;
                        MistriRepo.Edit(MistriInfo);
                        MistriRepo.Save();
                        clear();
                        MessageBox.Show("Record Updated Successfully.", "Success");
                        this.ActiveControl = btnnew;
                    }
                    else
                    {
                        maxcustomerid();
                        MistriInfo.MistriCode = Convert.ToInt32(txtMistriID.Text);
                        MistriRepo.Add(MistriInfo);
                        MistriRepo.Save();
                        clear();
                        MessageBox.Show("Record Saved Successfully.", "Success");
                        this.ActiveControl = btnnew;
                    }
                }
                MistriRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                    this.Close();
            }
            catch (Exception)
            { }
        }

        private void cmbMechname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MistriRepository MistriRepo = new MistriRepository();
                GvMistriInfo.Rows.Clear();
                List<MistriInformation> Mistridata = MistriRepo.GetAll().Where(t => t.MistriCode == Convert.ToInt32(cmbMechname.SelectedValue) && t.CompId == CommonMethod.CompId).ToList();
                if (Mistridata.Count != 0)
                {
                    int i = 0;
                    foreach (var item in Mistridata)
                    {
                        GvMistriInfo.Rows.Add();
                        GvMistriInfo.Rows[i].Cells["MechanicName"].Value = item.MistriName;
                        GvMistriInfo.Rows[i].Cells["Address"].Value = item.MistriAddress;
                        GvMistriInfo.Rows[i].Cells["ContactNo"].Value = item.ContactNo;
                        GvMistriInfo.Rows[i].Cells["EmailId"].Value = item.EmailId;
                        GvMistriInfo.Rows[i].Cells["MistriCode"].Value = item.MistriCode;
                        GvMistriInfo.Rows[i].Cells["Id"].Value = item.Id;
                        i++;
                    }
                }
                MistriRepo.Dispose();
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
            if (keyData == Keys.F3)
                btnSave_Click(null, null);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmMistri_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.M)
            {
                frmMistri frmmist = new frmMistri();
                frmmist.Show();
            }
        }

        private void frmMistri_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            BindMistriinfo();
        }

        private void GvMistriInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                MistriId = Convert.ToInt32(GvMistriInfo.Rows[e.RowIndex].Cells["Id"].Value);
                txtMistriID.Text = Convert.ToString(GvMistriInfo.Rows[e.RowIndex].Cells["MistriCode"].Value);
                txtMistriName.Text = Convert.ToString(GvMistriInfo.Rows[e.RowIndex].Cells["MechanicName"].Value);
                txtMistriAddress.Text = Convert.ToString(GvMistriInfo.Rows[e.RowIndex].Cells["Address"].Value);
                txtMistriContactNo.Text = Convert.ToString(GvMistriInfo.Rows[e.RowIndex].Cells["ContactNo"].Value);
                txtMistriEmailID.Text = Convert.ToString(GvMistriInfo.Rows[e.RowIndex].Cells["EmailId"].Value);
                btnSave.Text = "Update";
            }
            catch (Exception)
            { }
        }
    }
}