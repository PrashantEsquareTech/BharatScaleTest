using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmIncomeMaster : Form
    {
        DbClass db = new DbClass();
        public int expid;

        public frmIncomeMaster()
        {
            InitializeComponent();
            BindAcGroup();
            maxincomeid1();
            Bindexpenceinfo();
        }

        public void BindAcGroup()
        {
            try
            {
                AccountingGroupMasterRepository ACGRepo = new AccountingGroupMasterRepository();
                List<AccountingGroupMaster> ACGdata = new List<AccountingGroupMaster>();
                ACGdata.Clear();
                ACGdata = ACGRepo.GetAll().OrderByDescending(t => t.ACGName).OrderByDescending(t => t.ACGName).ToList();
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

        public void maxincomeid1()
        {
            try
            {
                IncomeMasterRepository expmastRepo = new IncomeMasterRepository();
                var mid = expmastRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.IncomeId);
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
                btnAdd.Text = "Add";
                maxincomeid1();
                BindAcGroup();
                Bindexpenceinfo();
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
                IncomeMasterRepository expmastRepo = new IncomeMasterRepository();
                GVList.Rows.Clear();
                List<IncomeMaster> ItemList = expmastRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).ToList();
                if (ItemList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        GVList.Rows.Add();
                        GVList.Rows[i].Cells["ACGId"].Value = item.ACGId;
                        try
                        {
                            GVList.Rows[i].Cells["ACGroup"].Value = item.AccountingGroupMaster.ACGName;
                        }
                        catch (Exception)
                        { }
                        GVList.Rows[i].Cells["Id"].Value = item.ID;
                        GVList.Rows[i].Cells["IncomeId"].Value = item.IncomeId;
                        GVList.Rows[i].Cells["IncomeName"].Value = item.IncomeName;
                        GVList.Rows[i].Cells["Description"].Value = item.Description;
                        GVList.Rows[i].Cells["CompIdgrid"].Value = item.CompId;
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
                    IncomeMasterRepository expmastRepo = new IncomeMasterRepository();
                    IncomeMaster expinfo = new IncomeMaster();
                    expinfo.IncomeName = txtexpencename.Text.Trim();
                    expinfo.Description = txtDescription.Text.Trim();
                    expinfo.ACGId = Convert.ToInt32(cmbACGroups.SelectedValue.ToString());
                    if (btnAdd.Text == "Update")
                    {
                        IncomeMasterRepository expmastRepo1 = new IncomeMasterRepository();
                        IncomeMaster expinfo1 = new IncomeMaster();
                        expinfo1 = expmastRepo1.GetById(expid);
                        string pexpname = expinfo1.IncomeName;
                        expinfo.IncomeId = Convert.ToInt32(txtexpenceid.Text);
                        expinfo.CompId = Convert.ToInt32(CommonMethod.CompId);
                        expinfo.ID = expid;
                        expmastRepo.Edit(expinfo);
                        expmastRepo.Save();
                        MessageBox.Show("Expence Update Successfully.", "Success");
                        clear();
                        this.ActiveControl = txtexpencename;
                        expmastRepo1.Dispose();
                    }
                    else
                    {
                        var cust = expmastRepo.GetAll().Where(t => t.IncomeName.ToLower() == txtexpencename.Text.ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (cust != null)
                        {
                            MessageBox.Show("Already Saved This Record.", "Error");
                            txtexpencename.Text = "";
                            this.ActiveControl = txtexpencename;
                        }
                        else
                        {
                            maxincomeid1();
                            expinfo.IncomeId = Convert.ToInt32(txtexpenceid.Text);
                            expinfo.CompId = Convert.ToInt32(CommonMethod.CompId);
                            expmastRepo.Add(expinfo);
                            expmastRepo.Save();
                            MessageBox.Show("Income Add Successfully.", "Success");
                            clear();
                            this.ActiveControl = txtexpencename;
                        }
                    }
                    expmastRepo.Dispose();
                }
                else
                {
                    if (txtexpencename.Text == "")
                    {
                        MessageBox.Show("Enter Income Name.", "Warning");
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
                    this.ActiveControl = txtDescription;
            }
            catch (Exception)
            { }
        }

        private void frmIncomeMaster_Load(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("SELECT IncomeName FROM IncomeMaster", con);
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

        private void cmbACGroups_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.ActiveControl = txtexpencename;
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.ActiveControl = btnAdd;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clear();
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
                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "Income";
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
                            MessageBox.Show("This Income already used for transaction. You Can Not Delete.", "Warning");
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

        private void GVList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cmbACGroups.SelectedValue = Convert.ToInt32(GVList.Rows[e.RowIndex].Cells["ACGId"].Value);
                expid = Convert.ToInt32(GVList.Rows[e.RowIndex].Cells["Id"].Value);
                txtexpenceid.Text = Convert.ToString(GVList.Rows[e.RowIndex].Cells["IncomeId"].Value);
                txtexpencename.Text = Convert.ToString(GVList.Rows[e.RowIndex].Cells["IncomeName"].Value);
                txtDescription.Text = Convert.ToString(GVList.Rows[e.RowIndex].Cells["Description"].Value);
                btnAdd.Text = "Update";
                btnDelete.Enabled = true;
            }
            catch (Exception)
            { }
        }
    }
}