using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.RachitControls;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmStaffInfo : Form
    {
        DbClass db = new DbClass();
        public int Staffid;

        public frmStaffInfo()
        {
            InitializeComponent();
            clear();
            btnDelete.Enabled = false;
        }

        public void maxUnitid()
        {
            try
            {
                StaffRepository staffrepo = new StaffRepository();
                StaffInfo staffdata = new StaffInfo();
                if (staffdata != null)
                {
                    var mid = staffrepo.GetAll().Max(t => t.ID);
                    if (mid == null)
                        txtStaffid.Text = "1";
                    else
                        txtStaffid.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                }
                else
                    txtStaffid.Text = "1";
                staffrepo.Dispose();
            }
            catch (Exception)
            { txtStaffid.Text = "1"; }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                StaffRepository staffRepo = new StaffRepository();
                if (txtStaffName.Text.Trim() != "" && txtAddress.Text != "")
                {
                    if (btnSave.Text == "Save")
                    {
                        var staff = staffRepo.GetAll().Where(t => t.Name.ToLower() == txtStaffName.Text.Trim().ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (staff != null)
                        {
                            MessageBox.Show("Already Having This Name.", "Error");
                            txtStaffName.Text = "";
                            this.ActiveControl = txtStaffName;
                        }
                        else
                        {
                            StaffInfo sinfo = new StaffInfo();
                            sinfo.Name = txtStaffName.Text.Trim();
                            sinfo.Address = txtAddress.Text.Trim();
                            sinfo.MobileNo = txtMobileNo.Text.Trim();
                            sinfo.AdharCardNo = txtAdharNo.Text.Trim();
                            sinfo.CompId = CommonMethod.CompId;
                            staffRepo.Add(sinfo);
                            staffRepo.Save();
                            MessageBox.Show("Staff Information Saved.", "Success");
                            clear();
                        }
                    }
                    else
                    {
                        var supp = staffRepo.GetAll().Where(t => t.ID == Convert.ToInt32(txtStaffid.Text) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        Staffid = Convert.ToInt32(supp.ID);
                        StaffInfo sinfo1 = new StaffInfo();
                        sinfo1 = staffRepo.GetById(Staffid);
                        sinfo1.Name = txtStaffName.Text.Trim();
                        sinfo1.Address = txtAddress.Text.Trim();
                        sinfo1.MobileNo = txtMobileNo.Text.Trim();
                        sinfo1.AdharCardNo = txtAdharNo.Text.Trim();
                        sinfo1.CompId = CommonMethod.CompId;
                        staffRepo.Edit(sinfo1);
                        staffRepo.Save();
                        MessageBox.Show("Staff Information Updated.", "Success");
                        clear();
                        btnDelete.Enabled = false;
                    }
                }
                else
                {
                    if (txtStaffName.Text == "")
                        MessageBox.Show("Enter Staff Name.", "Warning");
                    else
                        MessageBox.Show("Enter Address Name.", "Warning");
                }
                staffRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                txtStaffName.Text = "";
                txtAddress.Text = "";
                txtMobileNo.Text = "";
                txtAdharNo.Text = "";
                Staffid = 0;
                maxUnitid();
                btnSave.Text = "Save";
                fillsearchstaffcombo();
                BindStaffinfo();
                this.ActiveControl = txtStaffName;
            }
            catch (Exception)
            { }
        }

        public void BindStaffinfo()
        {
            StaffRepository staffrepo = new StaffRepository();
            Gvstaffinfo.Rows.Clear();
            List<StaffInfo> ItemList = staffrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).ToList();
            lblTotalStaff.Text = Convert.ToString(ItemList.Count);
            if (ItemList.Count != 0)
            {
                int i = 0;
                foreach (var item in ItemList)
                {
                    Gvstaffinfo.Rows.Add();
                    Gvstaffinfo.Rows[i].Cells["StaffIdg"].Value = item.ID;
                    Gvstaffinfo.Rows[i].Cells["StaffName"].Value = item.Name;
                    Gvstaffinfo.Rows[i].Cells["Address"].Value = item.Address;
                    Gvstaffinfo.Rows[i].Cells["MobileNo"].Value = item.MobileNo;
                    Gvstaffinfo.Rows[i].Cells["AdharCardNo"].Value = item.AdharCardNo;
                    i++;
                }
            }
            staffrepo.Dispose();
        }

        public void fillsearchstaffcombo()
        {
            try
            {
                StaffRepository staffrepo = new StaffRepository();
                List<StaffInfo> staffdata = new List<StaffInfo>();
                staffdata.Clear();
                staffdata = staffrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.Name).ToList();
                staffdata.Add(new StaffInfo { Name = "Select", ID = 0 });
                staffdata.Reverse();
                cmbstaffname.DataSource = staffdata;
                cmbstaffname.ValueMember = "ID";
                cmbstaffname.DisplayMember = "Name";
                staffrepo.Dispose();
                cmbstaffname.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                StaffRepository staffrepo = new StaffRepository();
                var supp = staffrepo.GetAll().Where(t => t.ID == Convert.ToInt32(txtStaffid.Text) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                Staffid = Convert.ToInt32(supp.ID);
                StaffInfo sinfo = new StaffInfo();
                sinfo = staffrepo.GetById(Staffid);
                if (sinfo.Name == txtStaffName.Text.Trim())
                {
                    staffrepo.Remove(sinfo);
                    staffrepo.Save();
                    MessageBox.Show("Record Deleted Sucessfully.", "Success");
                    clear();
                    btnSave.Enabled = true;
                    btnDelete.Enabled = false;
                }
                else
                    MessageBox.Show("Staff name is not correct for delete.", "Warning");
                this.ActiveControl = txtStaffName;
                staffrepo.Dispose();
            }
            catch (Exception) { }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            clear();
            btnSave.Enabled = true;
        }

        private void Gvstaffinfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtStaffName.Text = Convert.ToString(Gvstaffinfo.Rows[e.RowIndex].Cells["StaffName"].Value);
                txtStaffid.Text = Convert.ToString(Gvstaffinfo.Rows[e.RowIndex].Cells["StaffIdg"].Value);
                txtAddress.Text = Convert.ToString(Gvstaffinfo.Rows[e.RowIndex].Cells["Address"].Value);
                txtMobileNo.Text = Convert.ToString(Gvstaffinfo.Rows[e.RowIndex].Cells["MobileNo"].Value);
                txtAdharNo.Text = Convert.ToString(Gvstaffinfo.Rows[e.RowIndex].Cells["AdharCardNo"].Value);
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
            }
            catch (Exception) { }
        }

        private void cmbstaffname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                StaffRepository staffRepo = new StaffRepository();
                var staffname = staffRepo.GetAll().Where(t => t.Name == cmbstaffname.Text && t.CompId == CommonMethod.CompId).FirstOrDefault();
                Gvstaffinfo.Rows.Clear();
                Gvstaffinfo.Rows.Add();
                Gvstaffinfo.Rows[0].Cells["StaffName"].Value = staffname.Name;
                Gvstaffinfo.Rows[0].Cells["Address"].Value = staffname.Address;
                Gvstaffinfo.Rows[0].Cells["MobileNo"].Value = staffname.MobileNo;
                Gvstaffinfo.Rows[0].Cells["AdharCardNo"].Value = staffname.AdharCardNo;
                Gvstaffinfo.Rows[0].Cells["StaffIdg"].Value = staffname.ID;
                staffRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void txtStaffName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
                this.ActiveControl = txtAddress;
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
            try
            {
                if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                    this.Close();
            }
            catch (Exception)
            { }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            try
            {
                fillsearchstaffcombo();
                btnDelete.Enabled = false;
                BindStaffinfo();
            }
            catch (Exception)
            { }
        }
    }
}