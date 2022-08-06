using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.RachitControls;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmManufactureCompanyInfo : Form
    {
        public int cid;
        DbClass db = new DbClass();
        private int preComp = 0;
        SettingRepository settRepo = new SettingRepository();
        Setting settData = new Setting();

        public frmManufactureCompanyInfo()
        {
            InitializeComponent();
            settData = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (settData != null)
            {
                if (settData.Applyonmarathifont == "Y")
                    txtMarathiName.Font = new Font("Shivaji01", 12);
                else
                    txtMarathiName.Font = new Font("Microsoft Sans Serif", 10);
            }
            companymaxid();
            BankinfoGridbind();
            this.ActiveControl = txtCompanyName;
        }

        public void clear()
        {
            txtCompanyName.Text = "";
            txtCompanyAddress.Text = "";
            txtcontactNo.Text = "";
            txtStockAlert.Text = "0";
            preComp = 0;
            companymaxid();
            BankinfoGridbind();
            btnSave.Text = "Save";
            this.ActiveControl = txtCompanyName;
        }

        public void companymaxid()
        {
            try
            {
                var mid = 0;
                ManufactureCInfoRepository manufactureinforepo = new ManufactureCInfoRepository();
                if (CommonMethod.commProduct == true)
                    mid = Convert.ToInt32(manufactureinforepo.GetAll().Max(t => t.CompanyId));
                else
                    mid = Convert.ToInt32(manufactureinforepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.CompanyId));
                if (mid == null)
                    txtCompanyID.Text = "1";
                else
                    txtCompanyID.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                manufactureinforepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void BankinfoGridbind()
        {
            try
            {
                ManufactureCInfoRepository manufactureinforepo = new ManufactureCInfoRepository();
                List<ManuCompanyInfo> cinfo = new List<ManuCompanyInfo>();
                if (CommonMethod.commProduct == true)
                    cinfo = manufactureinforepo.GetAll().ToList();
                else
                    cinfo = manufactureinforepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).ToList();
                Gvcompanynfo.Rows.Clear();
                if (cinfo.Count != 0)
                {
                    int i = 0;
                    foreach (var item in cinfo)
                    {
                        Gvcompanynfo.Rows.Add();
                        Gvcompanynfo.Rows[i].Cells["CompName"].Value = item.CompanyName;
                        Gvcompanynfo.Rows[i].Cells["Address"].Value = item.CompanyAddress;
                        Gvcompanynfo.Rows[i].Cells["ContactNo"].Value = item.ContactNo;
                        Gvcompanynfo.Rows[i].Cells["CompanyId"].Value = item.CompanyId;
                        Gvcompanynfo.Rows[i].Cells["Id"].Value = item.Id;
                        Gvcompanynfo.Rows[i].Cells["StockAlert"].Value = item.StockAlert;
                        Gvcompanynfo.Rows[i].Cells["MarathiName"].Value = item.CompanyMarathi == null ? "" : item.CompanyMarathi;
                        Gvcompanynfo.Rows[i].Cells["CompId"].Value = item.CompId;
                        i++;
                    }
                }
                manufactureinforepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string premfgcompname = "";
                if (btnSave.Text == "Update")
                {
                    if (txtCompanyName.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter Company Name.", "Error");
                        this.ActiveControl = txtCompanyName;
                    }
                    else
                    {
                        ManufactureCInfoRepository manufactureinforepo1 = new ManufactureCInfoRepository();
                        ManuCompanyInfo BillId = new ManuCompanyInfo();
                        if (CommonMethod.commProduct == true)
                            BillId = manufactureinforepo1.GetAll().Where(t => t.CompanyId != Convert.ToInt32(txtCompanyID.Text) && t.CompanyName.ToLower() == Convert.ToString(txtCompanyName.Text.Trim()).ToLower()).FirstOrDefault();
                        else
                            BillId = manufactureinforepo1.GetAll().Where(t => t.CompanyId != Convert.ToInt32(txtCompanyID.Text) && t.CompId == CommonMethod.CompId && t.CompanyName.ToLower() == Convert.ToString(txtCompanyName.Text.Trim()).ToLower()).FirstOrDefault();
                        if (BillId != null)
                        {
                            MessageBox.Show("Already Saved This Name.", "Error");
                            this.ActiveControl = txtCompanyName;
                        }
                        else
                        {
                            try
                            {
                                ManufactureCInfoRepository mfgcomprepo = new ManufactureCInfoRepository();
                                ManuCompanyInfo mfgcompdata = new ManuCompanyInfo();
                                if (CommonMethod.commProduct == true)
                                    mfgcompdata = mfgcomprepo.GetAll().Where(t => t.CompanyId == Convert.ToInt32(txtCompanyID.Text)).FirstOrDefault();
                                else
                                    mfgcompdata = mfgcomprepo.GetAll().Where(t => t.CompanyId == Convert.ToInt32(txtCompanyID.Text) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                premfgcompname = Convert.ToString(mfgcompdata.CompanyName);
                                mfgcomprepo.Dispose();
                            }
                            catch (Exception)
                            { }

                            ManufactureCInfoRepository manufactureinforepo = new ManufactureCInfoRepository();
                            ManuCompanyInfo companyinfo = new ManuCompanyInfo();
                            companyinfo.Id = cid;
                            companyinfo.CompanyId = Convert.ToInt32(txtCompanyID.Text);
                            companyinfo.CompanyName = txtCompanyName.Text.Trim();
                            companyinfo.CompanyAddress = txtCompanyAddress.Text.Trim();
                            companyinfo.ContactNo = txtcontactNo.Text.Trim();
                            companyinfo.StockAlert = Convert.ToInt32(txtStockAlert.Text);
                            companyinfo.CompanyMarathi = txtMarathiName.Text;
                            companyinfo.CompId = preComp;
                            manufactureinforepo.Edit(companyinfo);
                            manufactureinforepo.Save();
                            if (premfgcompname != txtCompanyName.Text.Trim())
                            {
                                try
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPUpdateMFGCompName", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = premfgcompname;
                                    command.Parameters.Add("@NewCName", SqlDbType.VarChar).Value = txtCompanyName.Text.Trim();
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                    command.ExecuteNonQuery();
                                    premfgcompname = "";
                                    db.CloseConnection();
                                }
                                catch (Exception)
                                {
                                    db.CloseConnection();
                                }
                            }
                            MessageBox.Show("Record Updated Sucessfully.", "Success");
                            clear();
                            manufactureinforepo.Dispose();
                            this.ActiveControl = txtCompanyName;
                        }
                    }
                }
                else
                {
                    if (txtCompanyName.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter Company Name.", "Error");
                        this.ActiveControl = txtCompanyName;
                    }
                    else
                    {
                        ManufactureCInfoRepository manufactureinforepo = new ManufactureCInfoRepository();
                        ManuCompanyInfo BillId = new ManuCompanyInfo();
                        if (CommonMethod.commProduct == true)
                            BillId = manufactureinforepo.GetAll().Where(t => t.CompanyName.ToLower() == Convert.ToString(txtCompanyName.Text.Trim()).ToLower()).FirstOrDefault();
                        else
                            BillId = manufactureinforepo.GetAll().Where(t => t.CompanyName.ToLower() == Convert.ToString(txtCompanyName.Text.Trim()).ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (BillId != null)
                        {
                            MessageBox.Show("Already Saved This Record.", "Error");
                            this.ActiveControl = txtCompanyName;
                        }
                        else
                        {
                            ManufactureCInfoRepository manufactureinforepo1 = new ManufactureCInfoRepository();
                            ManuCompanyInfo companyinfo = new ManuCompanyInfo();
                            companymaxid();
                            companyinfo.CompanyId = Convert.ToInt32(txtCompanyID.Text);
                            companyinfo.CompanyName = txtCompanyName.Text.Trim();
                            companyinfo.CompanyAddress = txtCompanyAddress.Text.Trim();
                            companyinfo.ContactNo = txtcontactNo.Text.Trim();
                            companyinfo.StockAlert = Convert.ToInt32(txtStockAlert.Text);
                            companyinfo.CompanyMarathi = txtMarathiName.Text;
                            companyinfo.CompId = CommonMethod.CompId;
                            manufactureinforepo1.Add(companyinfo);
                            manufactureinforepo1.Save();
                            MessageBox.Show("Record Saved Sucessfully.", "Success");
                            clear();
                            this.ActiveControl = txtCompanyName;
                            manufactureinforepo1.Dispose();
                        }
                        manufactureinforepo.Dispose();
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void Gvcompanynfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cid = Convert.ToInt32(Gvcompanynfo.Rows[e.RowIndex].Cells["Id"].Value);
                txtCompanyID.Text = Convert.ToString(Gvcompanynfo.Rows[e.RowIndex].Cells["CompanyId"].Value);
                txtCompanyName.Text = Convert.ToString(Gvcompanynfo.Rows[e.RowIndex].Cells["CompName"].Value);
                txtCompanyAddress.Text = Convert.ToString(Gvcompanynfo.Rows[e.RowIndex].Cells["Address"].Value);
                txtcontactNo.Text = Convert.ToString(Gvcompanynfo.Rows[e.RowIndex].Cells["ContactNo"].Value);
                if (Gvcompanynfo.Rows[e.RowIndex].Cells["StockAlert"].Value == null)
                    txtStockAlert.Text = Convert.ToString(0);
                else
                    txtStockAlert.Text = Convert.ToString(Gvcompanynfo.Rows[e.RowIndex].Cells["StockAlert"].Value);
                txtMarathiName.Text = Convert.ToString(Gvcompanynfo.Rows[e.RowIndex].Cells["MarathiName"].Value);
                preComp = Convert.ToInt32(Gvcompanynfo.Rows[e.RowIndex].Cells["CompId"].Value);
                btnSave.Text = "Update";
                this.ActiveControl = txtCompanyName;
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            clear();
            this.ActiveControl = txtCompanyName;
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

        private void txtCompanyName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                if (tb != null)
                {
                    int pos = tb.SelectionStart;
                    int length = tb.Text.Length;
                    tb.Text = tb.Text.Replace(",", "");
                    int diff = length - tb.Text.Length;
                    tb.SelectionStart = pos == 0 || diff == 0 ? pos : pos - diff;
                }
                TextBox tb1 = sender as TextBox;
                if (tb1 != null)
                {
                    int pos = tb1.SelectionStart;
                    int length = tb1.Text.Length;
                    tb1.Text = tb1.Text.Replace("'", "");
                    int diff = length - tb1.Text.Length;
                    tb1.SelectionStart = pos == 0 || diff == 0 ? pos : pos - diff;
                }
            }
            catch (Exception)
            { }
        }

        private void txtcontactNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtcontactNo.Text != "")
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(txtcontactNo.Text, "[^0-9]"))
                    {
                        MessageBox.Show("Please enter only numbers.");
                        txtcontactNo.Text.Remove(txtcontactNo.Text.Length - 1);
                        this.ActiveControl = txtcontactNo;
                    }
                    else if (txtcontactNo.Text.Length != 10)
                    {
                        MessageBox.Show("Please enter valid 10 digits contact numbers.");
                        this.ActiveControl = txtcontactNo;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtCompanyName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtMarathiName;
            }
            catch (Exception)
            { }
        }

        private void frmManufactureCompanyInfo_Load(object sender, EventArgs e)
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
                    SqlCommand cmd = null;
                    if (CommonMethod.commProduct == true)
                        cmd = new SqlCommand("SELECT CompanyName FROM ManuCompanyInfo", con);
                    else
                        cmd = new SqlCommand("SELECT CompanyName FROM ManuCompanyInfo where compid=" + CommonMethod.CompId, con);
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
                    txtCompanyName.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
                SettingRepository sRepo = new SettingRepository();
                Setting sdata = new Setting();
                sdata = sRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (sdata != null)
                {
                    if (sdata.Applyonmarathifont == "Y")
                        txtMarathiName.Font = new Font("Shivaji01", 12);
                    else
                        txtMarathiName.Font = new Font("Segoe UI", 10);
                }
            }
            catch (Exception)
            { }
        }

        private void txtMarathiName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    frmSettings settings = new frmSettings();
                    settings.ShowDialog();
                    frmManufactureCompanyInfo_Load(null, null);
                }
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtCompanyAddress;
            }
            catch (Exception)
            { }
        }

        private void txtCompanyAddress_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtcontactNo;
            }
            catch (Exception)
            { }
        }

        private void txtcontactNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtStockAlert;
            }
            catch (Exception)
            { }
        }

        private void txtStockAlert_KeyDown(object sender, KeyEventArgs e)
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
                    this.ActiveControl = btnnew;
            }
            catch (Exception)
            { }
        }

        private void btnnew_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtCompanyName;
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
                    if (cid != 0)
                    {
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPCheckforIdUsedorNot", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "MFGCompany";
                            command.Parameters.Add("@Id", SqlDbType.VarChar).Value = cid;
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
                            MessageBox.Show("This Manufacturing Company is Used. You Can Not Delete.", "Warning");
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
    }
}