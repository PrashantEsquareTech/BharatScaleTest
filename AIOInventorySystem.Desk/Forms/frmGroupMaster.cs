using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using System.Data.SqlClient;
using System.Data;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmGroupMaster : Form
    {
        public int GMid = 0;
        DbClass db = new DbClass();

        public frmGroupMaster()
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
                BindUnitinfo();
                btnAdd.Text = "Add";
            }
            catch (Exception)
            { }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                btnAdd.Enabled = true;
                this.ActiveControl = txtgroupmastername;
            }
            catch (Exception)
            { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                GroupMasterRepository unitrepo = new GroupMasterRepository();
                GroupMaster unitdata = new GroupMaster();
                if (CommonMethod.commProduct == true)
                    unitdata = unitrepo.GetAll().Where(t => t.GroupMasterName == txtgroupmastername.Text.Trim()).FirstOrDefault();
                else
                    unitdata = unitrepo.GetAll().Where(t => t.GroupMasterName == txtgroupmastername.Text.Trim() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (unitdata != null)
                {
                    MessageBox.Show("This Group Name Already Present.", "Warning");
                    clear();
                    this.ActiveControl = txtgroupmastername;
                }
                else
                {
                    if (txtgroupmastername.Text.Trim() != "")
                    {
                        unitrepo = new GroupMasterRepository();
                        GroupMaster supp = new GroupMaster();
                        if (CommonMethod.commProduct == true)
                            supp = unitrepo.GetAll().Where(t => t.GroupMasterName.ToLower() == txtgroupmastername.Text.Trim().ToLower()).FirstOrDefault();
                        else
                            supp = unitrepo.GetAll().Where(t => t.GroupMasterName.ToLower() == txtgroupmastername.Text.Trim().ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
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
                                GroupMaster unitinfo = new GroupMaster();
                                unitinfo.GroupMasterName = txtgroupmastername.Text.Trim();
                                unitinfo.Description = txtDescription.Text.Trim();
                                unitinfo.CompId = CommonMethod.CompId;
                                unitrepo.Add(unitinfo);
                                unitrepo.Save();
                                MessageBox.Show("Group Name Add Successfully.", "Success");
                                clear();
                                this.ActiveControl = txtgroupmastername;
                            }
                            else
                            {
                                string uname = "";
                                GroupMaster unitinfo = new GroupMaster();
                                unitinfo = unitrepo.GetById(GMid);
                                uname = unitinfo.GroupMasterName.ToString();
                                unitinfo.GroupMasterName = txtgroupmastername.Text.Trim();
                                unitinfo.Description = txtDescription.Text.Trim();
                                unitinfo.CompId = unitinfo.CompId;
                                unitrepo.Edit(unitinfo);
                                unitrepo.Save();
                                MessageBox.Show("Group Name updated Successfully.", "Success");
                                clear();
                                this.ActiveControl= txtgroupmastername;
                            }
                        }
                    }
                    else
                    {
                        if (txtgroupmastername.Text == "")
                        {
                            MessageBox.Show("Enter Group Name.", "Warning");
                            this.ActiveControl = txtgroupmastername;
                        }
                    }
                }
                unitrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void BindUnitinfo()
        {
            try
            {
                GroupMasterRepository unitrepo = new GroupMasterRepository();
                List<GroupMaster> ItemList = new List<GroupMaster>();
                GVList.Rows.Clear();
                if (CommonMethod.commProduct == true)
                    ItemList = unitrepo.GetAll().ToList();
                else
                    ItemList = unitrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).ToList();
                if (ItemList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        GVList.Rows.Add();
                        GVList.Rows[i].Cells["Id"].Value = item.GroupMasterId;
                        GVList.Rows[i].Cells["GroupName"].Value = item.GroupMasterName;
                        GVList.Rows[i].Cells["Description"].Value = item.Description;
                        i++;
                    }
                }
                unitrepo.Dispose();
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
            {
                btnAdd_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void GVList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                GMid = Convert.ToInt32(GVList.Rows[e.RowIndex].Cells["Id"].Value);
                txtgroupmastername.Text = Convert.ToString(GVList.Rows[e.RowIndex].Cells["GroupName"].Value);
                txtDescription.Text = Convert.ToString(GVList.Rows[e.RowIndex].Cells["Description"].Value);
                btnAdd.Text = "Update";
            }
            catch (Exception)
            { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string strcheck = "F";
                if (GMid != 0)
                {
                    try
                    {
                        db.connect();
                        SqlCommand command = new SqlCommand("SPCheckforIdUsedorNot", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "Group";
                        command.Parameters.Add("@Id", SqlDbType.VarChar).Value = GMid;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        command.Parameters.Add("@Check", SqlDbType.NVarChar);
                        command.Parameters["@Check"].Direction = ParameterDirection.Output;
                        command.Parameters["@Check"].Size = 5;
                        command.ExecuteReader();
                        strcheck = command.Parameters["@Check"].Value.ToString();
                        db.CloseConnection();
                    }
                    catch (Exception)
                    { db.CloseConnection(); }
                    if (strcheck == "T")
                        MessageBox.Show("This Group is Used. You Can Not Delete.", "Warning");
                    else
                    {
                        MessageBox.Show("Record Deleted Sucessfully.", "Success");
                        clear();
                    }
                }
            }
            catch (Exception)
            { }
        }
    }
}
