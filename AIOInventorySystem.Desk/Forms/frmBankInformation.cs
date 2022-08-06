using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmBankInformation : Form
    {
        BankRepository bankrepo;
        DbClass db = new DbClass();
        public int bankid;

        public frmBankInformation()
        {
            InitializeComponent();
            BindAcGroup();
            bankmaxid();
            BankinfoGridbind();
            btnDelete.Enabled = false;
            this.ActiveControl = cmbACGroups;
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
                cmbACGroups.SelectedText = "Bank Accounts";
            }
            catch (Exception)
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BankRepository unitrepo = new BankRepository();
                var unitdata = unitrepo.GetAll().Where(t => t.Bankname.ToLower().Trim() == txtBankName.Text.ToLower().Trim() && t.BankId != Convert.ToInt32(txtBankID.Text) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (unitdata != null)
                {
                    MessageBox.Show("This Bank Name Already Present.", "Warning");
                    clear();
                    this.ActiveControl = txtBankName;
                }
                else
                {
                    if (cmbACGroups.Text != "Select" && txtBankName.Text.Trim() != "" && cmbACGroups.Text != "")
                    {
                        if (btnSave.Text == "Update")
                        {
                            bankrepo = new BankRepository();
                            BankInformation bankinfo = new BankInformation();
                            bankinfo.ID = bankid;
                            bankinfo.BankId = Convert.ToInt32(txtBankID.Text);
                            bankinfo.Bankname = txtBankName.Text.Trim();
                            bankinfo.BankAddress = txtBankAddress.Text;
                            bankinfo.BankCno = txtContactno.Text;
                            bankinfo.IFSCCode = txtIFSC.Text;
                            bankinfo.ACGId = Convert.ToInt32(cmbACGroups.SelectedValue.ToString());
                            bankinfo.CompId = CommonMethod.CompId;
                            bankrepo.Edit(bankinfo);
                            bankrepo.Save();
                            BankStatementRepository BankStRepo = new BankStatementRepository();
                            BankStatement bankdata1 = BankStRepo.GetAll().Where(t => t.BankId == Convert.ToInt32(txtBankID.Text) && t.TransactionType == "Opening Balance" && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            if (bankdata1 != null)
                            {
                                bankdata1.ACGId = Convert.ToInt32(cmbACGroups.SelectedValue);
                                if (cmbACGroups.Text == "Bank OD A/C")
                                {
                                    bankdata1.Withdrawals = Convert.ToDecimal(bankdata1.Deposits);
                                    bankdata1.Deposits = null;
                                }
                                else
                                {
                                    bankdata1.Deposits = Convert.ToDecimal(bankdata1.Withdrawals);
                                    bankdata1.Withdrawals = null;
                                }
                                BankStRepo.Edit(bankdata1);
                                BankStRepo.Save();
                            }
                            try
                            {
                                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                                using (SqlConnection con = new SqlConnection(cnString1))
                                {
                                    SqlCommand cmd = new SqlCommand("UPDATE BankStatement SET ACGId='" + Convert.ToInt32(cmbACGroups.SelectedValue.ToString()) + "' where BankId='" + Convert.ToInt32(txtBankID.Text) + "' and Bankname='" + txtBankName.Text + "' and CompId='" + CommonMethod.CompId + "'", con);
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                }
                            }
                            catch (Exception)
                            { }
                            MessageBox.Show("Record Updated Sucessfully.", "Success");
                            clear();
                        }
                        else
                        {
                            bankrepo = new BankRepository();
                            BankInformation bankinfo = new BankInformation();
                            bankmaxid();
                            bankinfo.BankId = Convert.ToInt32(txtBankID.Text);
                            bankinfo.Bankname = txtBankName.Text.Trim();
                            bankinfo.BankAddress = txtBankAddress.Text;
                            bankinfo.BankCno = txtContactno.Text;
                            bankinfo.IFSCCode = txtIFSC.Text;
                            bankinfo.ACGId = Convert.ToInt32(cmbACGroups.SelectedValue.ToString());
                            bankinfo.CompId = CommonMethod.CompId;
                            bankrepo.Add(bankinfo);
                            bankrepo.Save();
                            MessageBox.Show("Record Saved Sucessfully.", "Success");
                            clear();
                        }
                    }
                    else
                    {
                        if (cmbACGroups.Text == "Select" || cmbACGroups.Text == "")
                        {
                            MessageBox.Show("Select Account Group Master.", "Warning");
                            this.ActiveControl = cmbACGroups;
                        }
                        else if (txtBankName.Text == "")
                        {
                            MessageBox.Show("Enter Bank Name.", "Warning");
                            this.ActiveControl = txtBankName;
                        }
                    }
                }
                unitrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                txtBankName.Text = "";
                txtBankAddress.Text = "";
                txtContactno.Text = "";
                btnDelete.Enabled = false;
                this.ActiveControl = cmbACGroups;
                txtIFSC.Text = "";
                bankmaxid();
                cmbACGroups.SelectedIndex = 0;
                BankinfoGridbind();
                txtBankName.Text = "";
                txtBankAddress.Text = "";
                txtContactno.Text = "";
                txtIFSC.Text = "";
                btnSave.Text = "Save";
                this.ActiveControl = txtBankName;
            }
            catch (Exception)
            { }
        }

        public void bankmaxid()
        {
            try
            {
                bankrepo = new BankRepository();
                var mid = bankrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.BankId);
                if (mid == null)
                    txtBankID.Text = "1";
                else
                    txtBankID.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
            }
            catch (Exception)
            { }
        }

        public void BankinfoGridbind()
        {
            try
            {
                bankrepo = new BankRepository();
                List<BankInformation> bankinfo = bankrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).ToList();
                gvBankinfo.Rows.Clear();
                if (bankinfo.Count != 0)
                {
                    int i = 0;
                    foreach (var item in bankinfo)
                    {
                        gvBankinfo.Rows.Add();
                        gvBankinfo.Rows[i].Cells["Id"].Value = item.ID;
                        gvBankinfo.Rows[i].Cells["BankUId"].Value = item.BankId;
                        gvBankinfo.Rows[i].Cells["BankName"].Value = item.Bankname;
                        gvBankinfo.Rows[i].Cells["Address"].Value = item.BankAddress;
                        gvBankinfo.Rows[i].Cells["accountNo"].Value = item.BankCno;
                        gvBankinfo.Rows[i].Cells["IFSCCode"].Value = item.IFSCCode;
                        try
                        {
                            gvBankinfo.Rows[i].Cells["AccountGroup"].Value = item.AccountingGroupMaster.ACGName;
                        }
                        catch (Exception)
                        { }
                        i++;
                    }
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
                    if (bankid != 0)
                    {
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPCheckforIdUsedorNot", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "Bank";
                            command.Parameters.Add("@Id", SqlDbType.VarChar).Value = bankid;
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
                            MessageBox.Show("This Bank is Used. You Can Not Delete.", "Warning");
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                this.ActiveControl = txtBankName;
            }
            catch (Exception)
            { }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                RptBankinfo bankrpt = new RptBankinfo();
                bankrpt.ShowDialog();
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
                btnSave_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmBankInformation_Load(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("SELECT Bankname FROM BankInformation where compid=" + Convert.ToInt32(CommonMethod.CompId), con);
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
                    txtBankName.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void txtBankName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtBankAddress;
            }
            catch (Exception)
            { }
        }

        private void txtBankAddress_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtContactno;
            }
            catch (Exception)
            { }
        }

        private void txtContactno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtIFSC;
            }
            catch (Exception)
            { }
        }

        private void txtIFSC_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSave;
            }
            catch (Exception)
            { }
        }

        private void btnSave_KeyDown(object sender, KeyEventArgs e)
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
                    this.ActiveControl = btnPrint;
            }
            catch (Exception)
            { }
        }

        private void btnPrint_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnNew;
            }
            catch (Exception)
            { }
        }

        private void btnNew_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = gvBankinfo;
            }
            catch (Exception)
            { }
        }

        private void gvBankinfo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtBankName;
            }
            catch (Exception)
            { }
        }

        private void cmbACGroups_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtBankName;
            }
            catch (Exception)
            { }
        }

        private void gvBankinfo1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtBankName;
            }
            catch (Exception)
            { }
        }

        private void gvBankinfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bankid = Convert.ToInt32(gvBankinfo.Rows[e.RowIndex].Cells["Id"].Value);
                txtBankID.Text = Convert.ToString(gvBankinfo.Rows[e.RowIndex].Cells["BankUId"].Value);
                txtBankName.Text = Convert.ToString(gvBankinfo.Rows[e.RowIndex].Cells["BankName"].Value);
                txtBankAddress.Text = Convert.ToString(gvBankinfo.Rows[e.RowIndex].Cells["Address"].Value);
                txtContactno.Text = Convert.ToString(gvBankinfo.Rows[e.RowIndex].Cells["AccountNo"].Value);
                txtIFSC.Text = Convert.ToString(gvBankinfo.Rows[e.RowIndex].Cells["IFSCCode"].Value);
                cmbACGroups.Text = Convert.ToString(gvBankinfo.Rows[e.RowIndex].Cells["AccountGroup"].Value);
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
                this.ActiveControl = cmbACGroups;
            }
            catch (Exception)
            { }
        }
    }
}