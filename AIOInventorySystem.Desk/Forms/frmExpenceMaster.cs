using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.RachitControls;
using System.Data;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmExpenceMaster : Form
    {
        DbClass db = new DbClass();
        public int expid = 0, compid = 0;

        public frmExpenceMaster()
        {
            InitializeComponent();
            BindAcGroup();
            maxexpenceid();
            Bindexpenceinfo();
        }

        public void BindAcGroup()
        {
            try
            {
                AccountingGroupMasterRepository ACGRepo = new AccountingGroupMasterRepository();
                List<AccountingGroupMaster> ACGdata = new List<AccountingGroupMaster>();
                ACGdata.Clear();
                ACGdata = ACGRepo.GetAll().OrderByDescending(t => t.ACGName).ToList();
                ACGdata.Add(new AccountingGroupMaster { ACGName = "Select", Id = 0 });
                ACGdata.Reverse();
                cmbACGroups.DataSource = ACGdata;
                cmbACGroups.ValueMember = "Id";
                cmbACGroups.DisplayMember = "ACGName";
                ACGRepo.Dispose();
                cmbACGroups.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void maxexpenceid()
        {
            try
            {
                ExpenceMasterRepository expmastRepo = new ExpenceMasterRepository();
                var mid = expmastRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.ExpenceId);
                if (mid == null)
                    txtexpenceid.Text = "1";
                else
                    txtexpenceid.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                expmastRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                txtexpencename.Text = "";
                txtDescription.Text = "";
                BindAcGroup();
                maxexpenceid();
                Bindexpenceinfo();
                btnAdd.Text = "Add";
                btnDelete.Enabled = false;
                this.ActiveControl = txtexpencename;
            }
            catch (Exception)
            { }
        }

        public void Bindexpenceinfo()
        {
            try
            {
                ExpenceMasterRepository expmastRepo = new ExpenceMasterRepository();
                dtgvList.Rows.Clear();
                List<ExpenceMaster> ItemList = expmastRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).ToList();
                if (ItemList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        dtgvList.Rows.Add();
                        dtgvList.Rows[i].Cells["ACGId"].Value = item.ACGId;
                        try
                        {
                            dtgvList.Rows[i].Cells["ACGName"].Value = item.AccountingGroupMaster.ACGName;
                        }
                        catch (Exception)
                        { }
                        dtgvList.Rows[i].Cells["Id"].Value = item.ID;
                        dtgvList.Rows[i].Cells["ExpenceId"].Value = item.ExpenceId;
                        dtgvList.Rows[i].Cells["ExpenceName"].Value = item.ExpenceName;
                        dtgvList.Rows[i].Cells["Description"].Value = item.Description;
                        dtgvList.Rows[i].Cells["ListCompId"].Value = item.CompId;
                        i++;
                    }
                }
                expmastRepo.Dispose();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtexpencename.Text != "" && cmbACGroups.Text != "Select")
                {
                    ExpenceMasterRepository expmastRepo = new ExpenceMasterRepository();
                    ExpenceMaster expinfo = new ExpenceMaster();

                    expinfo.ExpenceName = txtexpencename.Text.Trim();
                    expinfo.Description = txtDescription.Text.Trim();
                    expinfo.ACGId = Convert.ToInt32(cmbACGroups.SelectedValue.ToString());
                    if (btnAdd.Text == "Update")
                    {
                        ExpenceMasterRepository expmastRepo1 = new ExpenceMasterRepository();
                        ExpenceMaster expinfo1 = new ExpenceMaster();
                        expinfo1 = expmastRepo1.GetById(expid);
                        string pexpname = expinfo1.ExpenceName;
                        expinfo.ExpenceId = Convert.ToInt32(txtexpenceid.Text);
                        expinfo.CompId = Convert.ToInt32(CommonMethod.CompId);
                        expinfo.ID = expid;

                        expmastRepo.Edit(expinfo);
                        expmastRepo.Save();

                        ExpenceRepository ExpRepo = new ExpenceRepository();
                        List<Expence> ExpData = new List<Expence>();
                        ExpData = ExpRepo.GetAll().Where(t => t.ExpenceName.ToLower() == pexpname.ToLower() && t.CompId == CommonMethod.CompId).ToList();
                        if (ExpData != null)
                        {
                            foreach (var item in ExpData)
                            {
                                ExpenceRepository ExpRepo1 = new ExpenceRepository();
                                Expence ExpData1 = ExpRepo1.GetById(item.Id);
                                ExpData1.ExpenceName = txtexpencename.Text.Trim();

                                ExpRepo1.Edit(ExpData1);
                                ExpRepo1.Save();
                                ExpRepo1.Dispose();
                            }
                        }
                        MessageBox.Show("Expence Update Successfully.", "success");
                        clear();
                        expmastRepo1.Dispose();
                        ExpRepo.Dispose();
                    }
                    else
                    {
                        var cust = expmastRepo.GetAll().Where(t => t.ExpenceName.ToLower() == txtexpencename.Text.ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (cust != null)
                        {
                            MessageBox.Show("Already Saved This Record.", "Error");
                            txtexpencename.Text = "";
                            this.ActiveControl = txtexpencename;
                        }
                        else
                        {
                            maxexpenceid();
                            expinfo.ExpenceId = Convert.ToInt32(txtexpenceid.Text);
                            expinfo.CompId = Convert.ToInt32(CommonMethod.CompId);
                            expmastRepo.Add(expinfo);
                            expmastRepo.Save();
                            MessageBox.Show("Expence Add Successfully.", "success");
                            clear();
                        }
                    }
                    expmastRepo.Dispose();
                }
                else
                {
                    if (txtexpencename.Text == "")
                    {
                        MessageBox.Show("Enter Expence Name.", "Warning");
                        this.ActiveControl = txtexpencename;
                    }
                    else
                    {
                        MessageBox.Show("Select Group.", "Warning");
                        this.ActiveControl = cmbACGroups;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtexpencename_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnAdd;
            }
            catch (Exception)
            { }
        }

        private void frmExpenceMaster_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            try
            {
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = new SqlCommand("SELECT ExpenceName FROM ExpenceMaster where CompId='" + CommonMethod.CompId + "'", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        {
                            MyCollection.Add(reader.GetString(0));
                        }
                        catch (Exception)
                        { }
                    }
                    txtexpencename.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    string strcheck = "F";
                    if (expid != 0)
                    {
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPCheckforIdUsedorNot", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "Expence";
                            command.Parameters.Add("@Id", SqlDbType.VarChar).Value = expid;
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
                            MessageBox.Show("This Expense already used for transaction. You Can Not Delete.", "Warning");
                        else
                        {
                            MessageBox.Show("Record Deleted Sucessfully.", "Success");
                            clear();
                        }
                    }
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

        private void dtgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cmbACGroups.SelectedValue = Convert.ToInt32(dtgvList.Rows[e.RowIndex].Cells["ACGId"].Value);
                expid = Convert.ToInt32(dtgvList.Rows[e.RowIndex].Cells["Id"].Value);
                txtexpenceid.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["ExpenceId"].Value);
                txtexpencename.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["ExpenceName"].Value);
                txtDescription.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["Description"].Value);
                compid = Convert.ToInt32(dtgvList.Rows[e.RowIndex].Cells["ListCompId"].Value);
                btnAdd.Text = "Update";
                btnDelete.Enabled = true;
            }
            catch (Exception)
            { }
        }
    }
}