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
    public partial class frmGodownMaster : Form
    {
        DbClass db = new DbClass();
        public int gdwnid;

        public frmGodownMaster()
        {
            InitializeComponent();
            BindGodownInfo();
        }

        public void clear()
        {
            try
            {
                txtGodownName.Text = "";
                txtDescription.Text = "";
                BindGodownInfo();
                btnAdd.Text = "ADD";
                btnDelete.Enabled = false;
                this.ActiveControl = txtGodownName;
            }
            catch (Exception)
            { }
        }

        public void BindGodownInfo()
        {
            try
            {
                GodownMasterRepository GodownRepo = new GodownMasterRepository();
                GVList.Rows.Clear();
                List<GodownMaster> ItemList = new List<GodownMaster>();
                if (CommonMethod.commProduct == true)
                    ItemList = GodownRepo.GetAll().ToList();
                else
                    ItemList = GodownRepo.GetAll().Where(t => t.CompId == Convert.ToInt32(CommonMethod.CompId)).ToList();
                if (ItemList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        GVList.Rows.Add();
                        GVList.Rows[i].Cells["Godown"].Value = item.GodownName;
                        GVList.Rows[i].Cells["Description"].Value = item.Description;
                        GVList.Rows[i].Cells["Id"].Value = item.ID;
                        i++;
                    }
                }
                GodownRepo.Dispose();
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
            else if (keyData == Keys.F3)
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
                if (txtGodownName.Text != "")
                {
                    GodownMasterRepository GodownRepo = new GodownMasterRepository();
                    GodownMaster GodownInfo = new GodownMaster();
                    GodownInfo.GodownName = txtGodownName.Text.Trim();
                    GodownInfo.Description = txtDescription.Text.Trim();

                    if (btnAdd.Text == "Update")
                    {
                        GodownMasterRepository GodownRepo1 = new GodownMasterRepository();
                        GodownMaster GodownInfo1 = new GodownMaster();
                        GodownInfo1 = GodownRepo1.GetById(gdwnid);
                        GodownInfo.CompId = GodownInfo1.CompId;
                        GodownInfo.ID = gdwnid;
                        GodownRepo.Edit(GodownInfo);
                        GodownRepo.Save();
                        MessageBox.Show("Godown Info Updated Successfully.", "Success");
                        clear();
                        GodownRepo1.Dispose();
                        GodownRepo.Dispose();
                    }
                    else
                    {
                        GodownMaster cust = new GodownMaster();
                        if (CommonMethod.commProduct == true)
                            cust = GodownRepo.GetAll().Where(t => t.GodownName.ToLower() == txtGodownName.Text.ToLower()).FirstOrDefault();
                        else
                            cust = GodownRepo.GetAll().Where(t => t.GodownName.ToLower() == txtGodownName.Text.ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (cust != null)
                        {
                            MessageBox.Show("Already Saved This Record.", "Error");
                            txtGodownName.Text = "";
                            this.ActiveControl = txtGodownName;
                        }
                        else
                        {
                            GodownInfo.CompId = CommonMethod.CompId;
                            GodownRepo.Add(GodownInfo);
                            GodownRepo.Save();
                            MessageBox.Show("Godown Info Added Successfully.", "Success");
                            clear();
                        }
                    }
                    GodownRepo.Dispose();
                }
                else
                {
                    MessageBox.Show("Enter Godown Name.", "Warning");
                    this.ActiveControl = txtGodownName;
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

        private void frmGodownMaster_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            }
            catch (Exception)
            { }
            try
            {
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd;
                    if (CommonMethod.commProduct == true)
                        cmd = new SqlCommand("SELECT GodownName FROM GodownMaster", con);
                    else
                        cmd = new SqlCommand("SELECT GodownName FROM GodownMaster where CompId='" + CommonMethod.CompId + "'", con);
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
                    txtGodownName.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnAdd;
            }
            catch (Exception)
            { }
        }

        private void btnAdd_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnDelete;
            }
            catch (Exception)
            { }
        }

        private void btnDelete_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnClose;
            }
            catch (Exception)
            { }
        }

        private void btnClose_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = GVList;
            }
            catch (Exception)
            { }
        }

        private void GvUnitInfo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtGodownName;
            }
            catch (Exception)
            { }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
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
                    if (gdwnid != 0)
                    {
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPCheckforIdUsedorNot", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "Godown";
                            command.Parameters.Add("@Id", SqlDbType.VarChar).Value = gdwnid;
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
                            MessageBox.Show("This Godown used for transaction. You Can Not Delete.", "Warning");
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
                gdwnid = Convert.ToInt32(GVList.Rows[e.RowIndex].Cells["Id"].Value);
                txtGodownName.Text = Convert.ToString(GVList.Rows[e.RowIndex].Cells["Godown"].Value);
                txtDescription.Text = Convert.ToString(GVList.Rows[e.RowIndex].Cells["Description"].Value);
                btnAdd.Text = "Update";
                btnDelete.Enabled = true;
            }
            catch (Exception)
            { }
        }
    }
}