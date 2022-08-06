using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmSubGroupMaster : Form
    {
        SubGroupMasterRepository subgroupRepo;
        int SGMasterId;

        public frmSubGroupMaster()
        {
            InitializeComponent();
            fillgroupmaster();
            Bindgrid();
        }

        public void fillgroupmaster()
        {
            try
            {
                GroupMasterRepository unitrepo = new GroupMasterRepository();
                List<GroupMaster> unitdata = new List<GroupMaster>();
                unitdata.Clear();
                if (CommonMethod.commProduct == true)
                    unitdata = unitrepo.GetAll().OrderByDescending(t => t.GroupMasterName).ToList();
                else
                    unitdata = unitrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.GroupMasterName).ToList();
                unitdata.Add(new GroupMaster { GroupMasterName = "Select", GroupMasterId = 0 });
                unitdata.Reverse();
                cmbgroup.DataSource = unitdata;
                cmbgroup.ValueMember = "GroupMasterId";
                cmbgroup.DisplayMember = "GroupMasterName";
                unitrepo.Dispose();
                cmbgroup.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                txtsubgroupname.Text = "";
                txtdesc.Text = "";
                fillgroupmaster();
                Bindgrid();
                btnAdd.Text = "Add";
                this.ActiveControl = txtsubgroupname;
            }
            catch (Exception) { }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
            }
            catch (Exception) { }
        }

        public void Bindgrid()
        {
            try
            {
                SubGroupMasterRepository smasterRepo = new SubGroupMasterRepository();
                dtgvList.Rows.Clear();
                List<SubGroupMaster> Itemlist = new List<SubGroupMaster>();
                Itemlist = smasterRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).ToList();
                if (Itemlist.Count != 0)
                {
                    int i = 0;
                    foreach (var item in Itemlist)
                    {
                        dtgvList.Rows.Add();
                        dtgvList.Rows[i].Cells["Id"].Value = item.Id;
                        dtgvList.Rows[i].Cells["SubGroupName"].Value = item.SubGroupName;
                        dtgvList.Rows[i].Cells["GroupName"].Value = item.GroupName;
                        dtgvList.Rows[i].Cells["Description"].Value = item.Description;
                        dtgvList.Rows[i].Cells["GroupId"].Value = item.GroupMasterId;
                        i++;
                    }
                }
            }
            catch (Exception) { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtsubgroupname.Text != "" && cmbgroup.SelectedIndex > 0)
                {
                    subgroupRepo = new SubGroupMasterRepository();
                    SubGroupMaster subgroupdata = new SubGroupMaster();
                    if (btnAdd.Text == "Add")
                    {
                        var subdata = subgroupRepo.GetAll().Where(t => t.SubGroupName.ToLower() == txtsubgroupname.Text.ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (subdata != null)
                        {
                            MessageBox.Show("This Sub Group Name Already Saved.", "Success");
                            txtsubgroupname.Text = "";
                            this.ActiveControl = txtsubgroupname;
                        }
                        else
                        {
                            SubGroupMasterRepository SGMastRepo = new SubGroupMasterRepository();
                            SubGroupMaster SGdata = new SubGroupMaster();
                            SGdata.SubGroupName = txtsubgroupname.Text;
                            SGdata.GroupMasterId = Convert.ToInt32(cmbgroup.SelectedValue);
                            SGdata.GroupName = cmbgroup.Text;
                            SGdata.Description = txtdesc.Text;
                            SGdata.CompId = CommonMethod.CompId;
                            SGMastRepo.Add(SGdata);
                            SGMastRepo.Save();
                            MessageBox.Show("Sub Group Name Added Successfully.", "Success");
                            clear();
                        }
                    }
                    else
                    {
                        SubGroupMasterRepository SGMastRepo = new SubGroupMasterRepository();
                        SubGroupMaster SGdata = new SubGroupMaster();
                        SGdata = SGMastRepo.GetById(SGMasterId);
                        SGdata.SubGroupName = txtsubgroupname.Text;
                        SGdata.GroupMasterId = Convert.ToInt32(cmbgroup.SelectedValue);
                        SGdata.GroupName = cmbgroup.Text;
                        SGdata.Description = txtdesc.Text;
                        SGdata.CompId = CommonMethod.CompId;
                        SGMastRepo.Edit(SGdata);
                        SGMastRepo.Save();
                        MessageBox.Show("Sub Group Name Update Successfully.", "Success");
                        clear();
                    }
                }
                else
                {
                    if (txtsubgroupname.Text == "")
                    {
                        MessageBox.Show("Enter Sub Group Name.", "Warning");
                        this.ActiveControl = txtsubgroupname;
                    }
                    else
                    {
                        MessageBox.Show("Select Group Name.", "Warning");
                        this.ActiveControl = cmbgroup;
                    }
                }
            }
            catch (Exception) { }
        }

        private void frmSubGroupMaster_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            try
            {
                if (Keys.Escape == e.KeyCode)
                {
                    if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        this.Close();
                }
            }
            catch (Exception) { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SubGroupMasterRepository smRepo = new SubGroupMasterRepository();
                SubGroupMaster smdata = smRepo.GetById(SGMasterId);
                smRepo.Remove(smdata);
                smRepo.Save();
                MessageBox.Show("Record Deleted Sucessfully.", "Success");
                clear();
                btnAdd.Enabled = true;
                btnDelete.Enabled = false;
            }
            catch (Exception) { }
        }

        private void btnaddgroup_Click(object sender, EventArgs e)
        {
            try
            {
                frmGroupMaster grpmast = new frmGroupMaster();
                grpmast.ShowDialog();
                fillgroupmaster();
            }
            catch (Exception) { }
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
            else if (keyData == Keys.F3)
            {
                btnAdd_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void dtgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SGMasterId = Convert.ToInt32(dtgvList.Rows[e.RowIndex].Cells["Id"].Value);
                txtsubgroupname.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["SubGroupName"].Value);
                cmbgroup.SelectedValue = Convert.ToInt32(dtgvList.Rows[e.RowIndex].Cells["GroupId"].Value);
                txtdesc.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["Description"].Value);
                btnAdd.Text = "Update";
                btnDelete.Enabled = true;
            }
            catch (Exception) { }
        }
    }
}