using System;
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
    public partial class frmCashProfit : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public frmCashProfit()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            try
            {
                dtpfromdate.Value = DateTime.Now.Date;
                fillcombo();
                getall();
            }
            catch (Exception)
            { }
        }

        public void fillcombo()
        {
            try
            {
                DataTable dt = null;
                if (CommonMethod.commProduct == null)
                    dt = db.Gettable("Select Distinct(ManufactureCompany) from ProductInformation order by ManufactureCompany");
                else
                    dt = db.Gettable("Select Distinct(ManufactureCompany) from ProductInformation where compid=" + CommonMethod.CompId + " order by ManufactureCompany");
                cmbcomanyname.DataSource = dt;
                cmbcomanyname.ValueMember = "ManufactureCompany";
                cmbcomanyname.DisplayMember = "ManufactureCompany";
            }
            catch (Exception)
            { }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkDate.Checked)
                {
                    CommonMethod cm = new CommonMethod();
                    DataTable dt = new DataTable();
                    lblsaleamount.Text = "0";
                    lblProfit.Text = "0";
                    try
                    {
                        db.connect();
                        SqlCommand command = new SqlCommand("SPCashProfit", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@FromDate", SqlDbType.VarChar).Value = cm.ValidFromDate(dtpfromdate.Value.Date); //dtpfromdate.Value.Date.ToShortDateString();
                        command.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = cm.ValidToDate(dtpToDate.Value.Date);// dtpfromdate.Value.Date.ToShortDateString();
                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        SqlDataReader reader = command.ExecuteReader();
                        dt.Load(reader);
                        reader.Dispose();
                        command.Dispose();
                        db.CloseConnection();
                    }
                    catch (Exception)
                    { db.CloseConnection(); }

                    GvCredit.DataSource = null;
                    GvCredit.DataSource = dt;
                    GvCredit.Refresh();
                    if (GvCredit.Rows.Count != 0)
                    {
                        GvCredit.Columns["ProductName"].Width = 180;
                        GvCredit.Columns["MarathiProductName"].Width = 150;
                        GvCredit.Columns["CompanyName"].Width = 120;
                        GvCredit.Columns["Unit"].Width = 60;
                        GvCredit.Columns["Quantity"].Width = 60;
                        GvCredit.Columns["SRate"].Width = 70;
                        GvCredit.Columns["PRate"].Width = 70;
                        GvCredit.Columns["SaleAmt"].Width = 70;
                        GvCredit.Columns["Profit"].Width = 70;

                        if (Convert.ToString(dt.Compute("Sum(Profit)", "")) != "")
                            lblProfit.Text = Convert.ToString(dt.Compute("Sum(Profit)", ""));
                        else
                            lblProfit.Text = "0";

                        if (Convert.ToString(dt.Compute("Sum(SaleAmt)", "")) != "")
                            lblsaleamount.Text = Convert.ToString(dt.Compute("Sum(SaleAmt)", ""));
                        else
                            lblsaleamount.Text = "0";
                    }
                    else
                        MessageBox.Show("Record not found", "");
                }
            }
            catch (Exception)
            { }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (chkDate.Checked)
            {
                RptDailySaleDetail dsrpt = new RptDailySaleDetail(cm.ValidFromDate(dtpfromdate.Value.Date), cm.ValidToDate(dtpToDate.Value.Date), "a");
                dsrpt.ShowDialog();
                dsrpt.Dispose();
            }
        }

        public void getall()
        {
            try
            {
                CommonMethod cm = new CommonMethod();
                DataTable dt = new DataTable();
                lblsaleamount.Text = "0";
                lblProfit.Text = "0";
                try
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPCashProfit", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@FromDate", SqlDbType.VarChar).Value = cm.ValidFromDate(dtpfromdate.Value.Date); //dtpfromdate.Value.Date.ToShortDateString();
                    command.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = cm.ValidToDate(dtpToDate.Value.Date);// dtpfromdate.Value.Date.ToShortDateString();
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                    SqlDataReader reader = command.ExecuteReader();
                    dt.Load(reader);
                    reader.Dispose();
                    command.Dispose();
                    db.CloseConnection();
                }
                catch (Exception)
                { db.CloseConnection(); }

                GvCredit.DataSource = null;
                GvCredit.DataSource = dt;
                GvCredit.Refresh();
                if (GvCredit.Rows.Count != 0)
                {
                    GvCredit.Columns["ProductName"].Width = 180;
                    GvCredit.Columns["MarathiProductName"].Width = 150;
                    GvCredit.Columns["CompanyName"].Width = 120;
                    GvCredit.Columns["Unit"].Width = 60;
                    GvCredit.Columns["Quantity"].Width = 60;
                    GvCredit.Columns["SRate"].Width = 70;
                    GvCredit.Columns["PRate"].Width = 70;
                    GvCredit.Columns["SaleAmt"].Width = 70;
                    GvCredit.Columns["Profit"].Width = 70;

                    if (Convert.ToString(dt.Compute("Sum(Profit)", "")) != "")
                        lblProfit.Text = Convert.ToString(dt.Compute("Sum(Profit)", ""));
                    else
                        lblProfit.Text = "0";

                    if (Convert.ToString(dt.Compute("Sum(SaleAmt)", "")) != "")
                        lblsaleamount.Text = Convert.ToString(dt.Compute("Sum(SaleAmt)", ""));
                    else
                        lblsaleamount.Text = "0";
                }
            }
            catch (Exception)
            { }
        }

        private void btngetall_Click(object sender, EventArgs e)
        {
            getall();
            try
            {
                chkproductname.Checked = false;
                chkCompanyName.Checked = false;
            }
            catch (Exception)
            { }
            txtpname.Text = "";
            fillcombo();
            dtpfromdate.Value = DateTime.Now.Date;
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

        private void frmDailySaleDetail_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                txtpname.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = null;
                    if (CommonMethod.commProduct == true)
                        cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation", con);
                    else
                        cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where compid=" + CommonMethod.CompId, con);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        {
                            MyCollection.Add(reader.GetString(0) + "," + reader.GetString(1) + "," + reader.GetString(2));
                        }
                        catch (Exception)
                        { }
                    }
                    txtpname.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void chkproductname_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkproductname.Checked)
                {
                    chkCompanyName.Checked = false;
                    fillcombo();
                }
                else
                    txtpname.Text = "";
            }
            catch (Exception)
            { }
        }

        private void chkCompanyName_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkCompanyName.Checked)
                {
                    fillcombo();
                    chkproductname.Checked = false;
                }
                else
                    fillcombo();
            }
            catch (Exception)
            { }
        }

        private void txtpname_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtpname.Text != "")
                {
                    string Proname = "", company = "", unit = "";
                    if ((txtpname.Text).Contains(","))
                    {
                        try
                        {
                            Proname = txtpname.Text.Trim().Split(',')[0];
                            company = txtpname.Text.Trim().Split(',')[1];
                            unit = txtpname.Text.Trim().Split(',')[2];
                        }
                        catch (Exception)
                        { }

                        ProductRepository productrepo = new ProductRepository();
                        ProductInformation productdata = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            productdata = productrepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                        else
                            productdata = productrepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (productdata == null)
                        {
                            MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                            txtpname.Text = "";
                            this.ActiveControl = txtpname;
                        }
                        productrepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                        txtpname.Text = "";
                        this.ActiveControl = txtpname;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtpname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void cmbcomanyname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void dtpfromdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void cmbcomanyname_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbcomanyname.Text != "Select" && cmbcomanyname.Text != "Select")
                {
                    ManufactureCInfoRepository unitrepo = new ManufactureCInfoRepository();
                    var unitdat = unitrepo.GetAll().Where(t => t.CompanyName == cmbcomanyname.Text).FirstOrDefault();
                    if (unitdat == null)
                    {
                        MessageBox.Show("Invalid Company Name.", "Warning");
                        fillcombo();
                        this.ActiveControl = cmbcomanyname;
                    }
                    unitrepo.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void dtpToDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void txtBillNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chkCompanyName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcomanyname;
            }
            catch (Exception)
            { }
        }

        private void chkproductname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtpname;
            }
            catch (Exception)
            { }
        }

        private void chkDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpfromdate;
            }
            catch (Exception)
            { }
        }
    }
}