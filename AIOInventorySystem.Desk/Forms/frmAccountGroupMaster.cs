using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmAccountGroupMaster : Form
    {
        public int GMid = 0;
        DbClass db = new DbClass();

        public frmAccountGroupMaster()
        {
            InitializeComponent();
            BindUnitinfo();
        }

        public void clear()
        {
            try
            {
                txtgroupmastername.Text = "";
                txtDescription.Text = "";
                GMid = 0;
                btnAdd.Enabled = true;
                btnAdd.Text = "Add";
                BindUnitinfo();
                this.ActiveControl = txtgroupmastername;
            }
            catch (Exception)
            { }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                clear();                
            }
            catch (Exception)
            { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AccountingGroupMasterRepository ACGrpRepo = new AccountingGroupMasterRepository();
                var unitdata = ACGrpRepo.GetAll().Where(t => t.ACGName == txtgroupmastername.Text.Trim()).FirstOrDefault();
                if (unitdata != null)
                {
                    MessageBox.Show("This Group Name Already Present.","Warning");
                    clear();
                    this.ActiveControl = txtgroupmastername;
                }
                else
                {
                    if (txtgroupmastername.Text.Trim() != "")
                    {
                        ACGrpRepo = new AccountingGroupMasterRepository();
                        var supp = ACGrpRepo.GetAll().Where(t => t.ACGName.ToLower() == txtgroupmastername.Text.Trim().ToLower()).FirstOrDefault();
                        if (supp != null)
                        {
                            MessageBox.Show("Already Saved This Record.", "Error");
                            txtgroupmastername.Text = "";
                            this.ActiveControl = txtgroupmastername;
                        }
                        else
                        {
                            if (btnAdd.Text == "Add")
                            {
                                AccountingGroupMaster ACGrpInfo = new AccountingGroupMaster();
                                ACGrpInfo.ACGName = txtgroupmastername.Text.Trim();
                                ACGrpInfo.Description = txtDescription.Text.Trim();
                                ACGrpRepo.Add(ACGrpInfo);
                                ACGrpRepo.Save();
                                MessageBox.Show("Group Name Add Successfully.", "Success");
                                clear();
                            }
                            else
                            {
                                AccountingGroupMaster ACGrpInfo = new AccountingGroupMaster();
                                ACGrpInfo = ACGrpRepo.GetById(GMid);
                                ACGrpInfo.ACGName = txtgroupmastername.Text.Trim();
                                ACGrpInfo.Description = txtDescription.Text.Trim();
                                ACGrpRepo.Edit(ACGrpInfo);
                                ACGrpRepo.Save();
                                MessageBox.Show("Group Name updated Successfully.", "Success");
                                clear();
                            }
                        }
                    }
                    else
                    {
                        if (txtgroupmastername.Text == "")
                        {
                            MessageBox.Show("Enter Group Name.", "Warning");
                            this.ActiveControl= txtgroupmastername;
                        }
                    }
                }
                ACGrpRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void BindUnitinfo()
        {
            try
            {
                AccountingGroupMasterRepository ACGrpRepo = new AccountingGroupMasterRepository();
                dtgvList.Rows.Clear();
                List<AccountingGroupMaster> ItemList = ACGrpRepo.GetAll();
                if (ItemList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        dtgvList.Rows.Add();
                        dtgvList.Rows[i].Cells["AGMID"].Value = item.Id;
                        dtgvList.Rows[i].Cells["ACGorupMasterName"].Value = item.ACGName;
                        dtgvList.Rows[i].Cells["Description"].Value = item.Description;
                        i++;
                    }
                }
                ACGrpRepo.Dispose();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void dtgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                GMid = Convert.ToInt32(dtgvList.Rows[e.RowIndex].Cells["AGMID"].Value);
                txtgroupmastername.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["ACGorupMasterName"].Value);
                txtDescription.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["Description"].Value);
                btnAdd.Text = "Update";
            }
            catch (Exception)
            { }
        }
    }
}
