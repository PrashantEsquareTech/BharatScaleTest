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
    public partial class FrmNippleFeederSyytemQuotationList : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public FrmNippleFeederSyytemQuotationList()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            fillCustomercombo();
            TodaysList();
        }

        public void fillCustomercombo()
        {
            try
            {
                CustomerRepository custrepo = new CustomerRepository();
                List<CustomerInformation> staffdata = new List<CustomerInformation>();
                staffdata.Clear();
                staffdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CustomerName).ToList();
                staffdata.Add(new CustomerInformation { CustomerName = "Select", Id = 0 });
                staffdata.Reverse();
                cmbCustomerName.DataSource = staffdata;
                cmbCustomerName.ValueMember = "Id";
                cmbCustomerName.DisplayMember = "CustomerName";
                custrepo.Dispose();
                cmbCustomerName.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    this.Close();
                    return true;
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        public void loadRefernceNo()
        {
            try
            {
                txtRefNo.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = null;
                    cmd = new SqlCommand("SELECT RefernceNo FROM tblProjectAtGlanceMaster where compid=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'", con);
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
                    txtRefNo.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception) { }
        }

        private void FrmNippleFeederSyytemQuotationList_Load(object sender, EventArgs e)
        {
            try
            {
                loadRefernceNo();
            }
            catch (Exception)
            { }
        }

        public void TodaysList()
        {
            try
            {
                string currDate = DateTime.Now.ToShortDateString();
                DataTable dt = new DataTable();
                dt = db.GetTable("Select distinct nfm.Id,pm.RefernceNo,Convert(varchar(24),nfm.Date,103) as 'Date',c.CustomerName,nfm.Type as 'Qut.Type',nfm.NippleFeederType as 'Nipple / Feeder Type',nfm.TotalCostPerHouse as 'Total Cost Per Bird',nfm.NippleSystemFeederCost as 'Cost Of Total System',nfm.SuspensionSystemCost as 'Suspension System Cost',nfm.AllTotalCost as 'Total Amt' from tblNippleFeederSystemMaster as nfm inner join tblProjectAtGlanceMaster as pm on pm.Id=nfm.ReferenceId inner join CustomerInformation as c on c.Id=pm.CustomerId where nfm.CompId=" + CommonMethod.CompId + " and nfm.TransactionYear='" + CommonMethod.TransactionYear + "' and Convert(varchar(24),nfm.Date,103)='" + currDate + "'");
                GVList.DataSource = null;
                GVList.DataSource = dt;
                if (GVList.Rows.Count > 0)
                {
                    lblTotal.Text = Convert.ToString(GVList.Rows.Count);
                    GVList.Columns["Id"].Visible = false;
                }
                else
                    GVList.Columns["Id"].Visible = false;
            }
            catch (Exception)
            { }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string currDate = DateTime.Now.ToShortDateString();
                string strsql = "";
                strsql = "Select  distinct nfm.Id, pm.RefernceNo,Convert(varchar(24),nfm.Date,103) as 'Date',c.CustomerName,nfm.Type as 'Qut.Type',nfm.NippleFeederType as 'Nipple / Feeder Type',nfm.TotalCostPerHouse as 'Total Cost Per Bird',nfm.NippleSystemFeederCost as 'Cost Of Total System',nfm.SuspensionSystemCost as 'Suspension System Cost',nfm.AllTotalCost as 'Total Amt' from tblNippleFeederSystemMaster as nfm inner join tblProjectAtGlanceMaster as pm on pm.Id=nfm.ReferenceId inner join CustomerInformation as c on c.Id=pm.CustomerId where nfm.CompId=" + CommonMethod.CompId + " and nfm.TransactionYear='" + CommonMethod.TransactionYear + "'";
                DataTable dt = new DataTable();
                if (chkCustomerName.Checked == true)
                {
                    if (cmbCustomerName.SelectedIndex > 0)
                        strsql = strsql + "and pm.CustomerId=" + Convert.ToInt32(cmbCustomerName.SelectedValue) + "";
                    else
                    {
                        MessageBox.Show("Select Customer Name");
                        this.ActiveControl = cmbCustomerName;
                    }
                }
                if (chkRefNo.Checked == true)
                {
                    if (txtRefNo.Text != "")
                        strsql = strsql + "and pm.RefernceNo='" + Convert.ToString(txtRefNo.Text) + "'";
                    else
                    {
                        MessageBox.Show("Enter Reference Number");
                        this.ActiveControl = txtRefNo;
                    }
                }
                if (chkSystemType.Checked == true)
                {
                    if (cmbSystemType.Text != "")
                        strsql = strsql + "and nfm.Type='" + Convert.ToString(cmbSystemType.Text) + "'";
                    else
                    {
                        MessageBox.Show("Select System Type");
                        this.ActiveControl = cmbSystemType;
                    }
                }
                if (chkDate.Checked == true)
                {
                    if (dtpFromDate.Value.Date <= dtpToDate.Value.Date)
                        strsql = strsql + "and Convert(varchar(24),nfm.Date,103) between '" + dtpFromDate.Value.ToString("dd/MM/yyyy") + "' and '" + dtpToDate.Value.ToString("dd/MM/yyyy") + "'";
                    else
                    {
                        MessageBox.Show("From Date Should be Grater Than To Date");
                        this.ActiveControl = dtpToDate;
                    }
                }
                if (chkCustomerName.Checked == false && chkDate.Checked == false && chkSystemType.Checked == false && chkRefNo.Checked == false)
                {
                    MessageBox.Show("Select Cheack Box");
                    this.ActiveControl = chkCustomerName;
                }
                else
                {
                    dt = db.GetTable(strsql);
                    GVList.DataSource = null;
                    GVList.DataSource = dt;
                    if (GVList.Rows.Count > 0)
                    {
                        lblTotal.Text = Convert.ToString(GVList.Rows.Count);
                        GVList.Columns["Id"].Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Record Not Found");
                        this.ActiveControl = cmbCustomerName;
                        GVList.Columns["Id"].Visible = false;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void GVList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    if (FrmNippleAndEzypanQuotation.nipplefeederQta == null)
                    {
                        FrmNippleAndEzypanQuotation qut = new FrmNippleAndEzypanQuotation(Convert.ToInt32(GVList.Rows[e.RowIndex].Cells[1].Value));
                        qut.ShowDialog();
                        FrmNippleAndEzypanQuotation.nipplefeederQta = null;
                    }
                    else
                    {
                        this.Close();
                        FrmNippleAndEzypanQuotation.nipplefeederQta.LoadData(Convert.ToInt32(GVList.Rows[e.RowIndex].Cells[1].Value));
                        FrmNippleAndEzypanQuotation.nipplefeederQta.Show();
                        FrmNippleAndEzypanQuotation.nipplefeederQta = null;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            try
            {
                chkCustomerName.Checked = false; chkDate.Checked = false;
                chkSystemType.Checked = false; chkRefNo.Checked = false;
                fillCustomercombo(); cmbSystemType.Text = "";
                dtpFromDate.Value = DateTime.Now.Date; dtpToDate.Value = DateTime.Now.Date;
                txtRefNo.Text = "";
                DataTable dt = new DataTable();
                dt = db.GetTable("Select distinct nfm.Id, pm.RefernceNo,Convert(varchar(24),nfm.Date,103) as 'Date',c.CustomerName,nfm.Type as 'Qut.Type',nfm.NippleFeederType as 'Nipple / Feeder Type',nfm.TotalCostPerHouse as 'Total Cost Per Bird',nfm.NippleSystemFeederCost as 'Cost Of Total System',nfm.SuspensionSystemCost as 'Suspension System Cost',nfm.AllTotalCost as 'Total Amt' from tblNippleFeederSystemMaster as nfm inner join tblProjectAtGlanceMaster as pm on pm.Id=nfm.ReferenceId inner join CustomerInformation as c on c.Id=pm.CustomerId where nfm.CompId=" + CommonMethod.CompId + " and nfm.TransactionYear='" + CommonMethod.TransactionYear + "'");
                GVList.DataSource = null;
                GVList.DataSource = dt;
                if (GVList.Rows.Count > 0)
                {
                    GVList.Columns["Id"].Visible = false;
                    lblTotal.Text = Convert.ToString(GVList.Rows.Count);
                }
                else
                    lblTotal.Text = "0";
            }
            catch (Exception)
            { }
        }
    }
}