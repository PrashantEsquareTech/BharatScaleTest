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
    public partial class FakeProductwisesale : Form
    {
        DbClass db = new DbClass(); public decimal total;
        CommonMethod cm = new CommonMethod();

        public FakeProductwisesale()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            fillcombo();
            fillGroupname();
        }

        public void fillcombo()
        {
            try
            {
                ManufactureCInfoRepository manufactureinforepo = new ManufactureCInfoRepository();
                List<ManuCompanyInfo> cdata = new List<ManuCompanyInfo>();
                cdata.Clear();
                if (CommonMethod.commProduct == true)
                    cdata = manufactureinforepo.GetAll().OrderByDescending(t => t.CompanyName).ToList();
                else
                    cdata = manufactureinforepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CompanyName).ToList();
                cdata.Add(new ManuCompanyInfo { CompanyName = "Select", CompanyId = 0 });
                cdata.Reverse();
                cmbcompany.DataSource = cdata;
                cmbcompany.ValueMember = "CompanyId";
                cmbcompany.DisplayMember = "CompanyName";
                manufactureinforepo.Dispose();
                cmbcompany.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void fillGroupname()
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
                cmbgroupMaster.DataSource = unitdata;
                cmbgroupMaster.ValueMember = "GroupMasterId";
                cmbgroupMaster.DisplayMember = "GroupMasterName";
                unitrepo.Dispose();
                cmbgroupMaster.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        private void Stock_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd;
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
                    txtProductname.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd1;
                    if (CommonMethod.commProduct == true)
                        cmd1 = new SqlCommand("SELECT d.Code FROM PurchaseDetail d inner join PurchaseMaster m on m.Id=d.PorderNo WHERE d.Code IS NOT NULL ", con);
                    else
                        cmd1 = new SqlCommand("SELECT d.Code FROM PurchaseDetail d inner join PurchaseMaster m on m.Id=d.PorderNo WHERE d.Code IS NOT NULL and  CompId='" + CommonMethod.CompId + "'", con);
                    con.Open();
                    SqlDataReader reader = cmd1.ExecuteReader();
                    AutoCompleteStringCollection MyCollection1 = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        {
                            MyCollection1.Add(reader.GetString(0));
                        }
                        catch (Exception)
                        { }
                    }
                    txtcode.AutoCompleteCustomSource = MyCollection1;
                    con.Close();
                    reader.Dispose();
                }
                this.ActiveControl = txtProductname;
            }
            catch (Exception)
            { }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
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

        private void chkproductname_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkproductname.Checked == true)
                {
                    chkcompanyname.Checked = false;
                    chkcode.Checked = false;
                    fillcombo();
                    fillGroupname();
                    chkgroupname.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void chkcompanyname_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkcompanyname.Checked == true)
                {
                    chkproductname.Checked = false;
                    chkcode.Checked = false;
                    txtProductname.Text = "";
                    fillGroupname();
                    chkgroupname.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void txtProductname_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtProductname.Text.Trim() != "")
                {
                    string proname = "", company = "", unit = "";
                    if ((txtProductname.Text).Contains(","))
                    {
                        try
                        {
                            proname = txtProductname.Text.Trim().Split(',')[0];
                            company = txtProductname.Text.Trim().Split(',')[1];
                            unit = txtProductname.Text.Trim().Split(',')[2];
                        }
                        catch (Exception)
                        { }
                        ProductRepository productrepo = new ProductRepository();
                        ProductInformation productdata = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                        else
                            productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (productdata == null)
                        {
                            MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                            txtProductname.Text = "";
                            this.ActiveControl = txtProductname;
                        }
                        productrepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                        txtProductname.Text = "";
                        this.ActiveControl = txtProductname;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtProductname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btncheck;
            }
            catch (Exception)
            { }
        }

        private void cmbcompany_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btncheck;
            }
            catch (Exception)
            { }
        }

        private void chkcode_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkcode.Checked == true)
                {
                    chkproductname.Checked = false;
                    chkcompanyname.Checked = false;
                    txtProductname.Text = "";
                    fillGroupname();
                    chkgroupname.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void txtcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btncheck;
            }
            catch (Exception)
            { }
        }

        private void txtcode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtcode.Text.Trim() != "")
                {
                    PurchaseDetailRepository purchaserepo = new PurchaseDetailRepository();
                    var PurchaseData = purchaserepo.GetAll().Where(t => t.Code == txtcode.Text && t.PurchaseMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (PurchaseData == null)
                    {
                        MessageBox.Show("This Serial Number Is Not Valid.", "Warning");
                        txtcode.Text = "";
                        this.ActiveControl = txtcode;
                    }
                    purchaserepo.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void chkgroupname_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkgroupname.Checked == true)
                {
                    chkproductname.Checked = false;
                    chkcompanyname.Checked = false;
                    txtProductname.Text = "";
                    chkcode.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbgroupMaster_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btncheck;
            }
            catch (Exception)
            { }
        }

        private void btncheck_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataTable dt = new DataTable();
                if (chkproductname.Checked == true)
                {
                    if (txtProductname.Text.Trim() != "")
                    {
                        string Proname1 = txtProductname.Text.Trim().Split(',')[0];
                        string company = txtProductname.Text.Trim().Split(',')[1];
                        string unit = txtProductname.Text.Trim().Split(',')[2];
                        string Proname = "";
                        if ((Proname1).Contains("'"))
                            Proname = Proname1.Replace("'", "''");
                        else
                            Proname = Proname1;
                        da = new SqlDataAdapter("Select m.BillNo as 'Bill No',m.BillDate as 'Bill Date',m.CustomerName as 'Customer Name',d.ProductId as 'Product Name',d.Unit,d.Quantity,d.Rate,d.TotalAmount from CustomerBillMaster as m inner join CustomerBillDetail as d on m.Id=d.BillNo Where ProductId='" + Proname + "' and CompanyName='" + company + "' and Unit='" + unit + "' and  m.BillNo like 'F%' and TransactionYear='" + CommonMethod.TransactionYear + "' and  CompId='" + CommonMethod.CompId + "'", db.Connection);
                    }
                    else
                    {
                        MessageBox.Show("Enter Product Name.", "Warning");
                        this.ActiveControl = txtProductname;
                    }
                }
                if (chkcompanyname.Checked == true)
                {
                    if (cmbcompany.SelectedIndex > 0)
                        da = new SqlDataAdapter("Select m.BillNo as 'Bill No',CONVERT(VARCHAR(24),m.BillDate,103) as 'Bill Date',m.CustomerName as 'Customer Name',d.ProductId as 'Product Name',d.Unit,d.Quantity,d.Rate,d.TotalAmount from CustomerBillMaster as m inner join CustomerBillDetail as d on m.Id=d.BillNo Where CompanyName='" + cmbcompany.Text + "' and  m.BillNo like 'F%' and TransactionYear='" + CommonMethod.TransactionYear + "' and  CompId='" + CommonMethod.CompId + "'", db.Connection);
                    else
                    {
                        MessageBox.Show("Select Company Name.", "Warning");
                        this.ActiveControl = cmbcompany;
                    }
                }
                if (chkcode.Checked == true)
                {
                    if (txtcode.Text != "")
                        da = new SqlDataAdapter("Select m.BillNo as 'Bill No',CONVERT(VARCHAR(24),m.BillDate,103) as 'Bill Date',m.CustomerName as 'Customer Name',d.ProductId as 'Product Name',d.Unit,d.Quantity,d.Rate,d.TotalAmount from CustomerBillMaster as m inner join CustomerBillDetail as d on m.Id=d.BillNo Where Code='" + txtcode.Text + "' and  m.BillNo like 'F%' and TransactionYear='" + CommonMethod.TransactionYear + "' and  CompId='" + CommonMethod.CompId + "'", db.Connection);
                    else
                    {
                        MessageBox.Show("Enter Serial No.", "Warning");
                        this.ActiveControl = txtcode;
                    }
                }
                if (chkgroupname.Checked)
                {
                    if (cmbgroupMaster.SelectedIndex > 0)
                        da = new SqlDataAdapter("Select m.BillNo as 'Bill No',CONVERT(VARCHAR(24),m.BillDate,103) as 'Bill Date',m.CustomerName as 'Customer Name',d.ProductId as 'Product Name',d.Unit,d.Quantity,d.Rate,d.TotalAmount from CustomerBillMaster as m inner join CustomerBillDetail as d on m.Id=d.BillNo inner join ProductInformation as p on d.ProductId=p.ProductName Where p.GroupMasterId='" + cmbgroupMaster.SelectedValue + "' and p.compid=" + CommonMethod.CompId + " and m.BillNo like 'F%' and TransactionYear='" + CommonMethod.TransactionYear + "' and  CompId='" + CommonMethod.CompId + "'", db.Connection);
                    else
                    {
                        MessageBox.Show("Select Group Name.", "Warning");
                        this.ActiveControl = cmbgroupMaster;
                    }
                }
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(TotalAmount)", ""));
                    lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(Quantity)", ""));
                    GvproductInfo.DataSource = dt;
                    GvproductInfo.Refresh();
                    if (GvproductInfo.Rows.Count != 0)
                    {
                        GvproductInfo.Columns["Product Name"].Width = 120;
                        this.ActiveControl = txtProductname;
                    }
                    else
                    {
                        MessageBox.Show("Record Not found.", "Warning");
                        this.ActiveControl = txtProductname;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnew_Click(object sender, EventArgs e)
        {
            try
            {
                txtProductname.Text = "";
                this.ActiveControl = txtProductname;
                GvproductInfo.DataSource = null;
                GvproductInfo.Rows.Clear();
                lblTotalAmount.Text = "0.00";
                lbltotalqty.Text = "0.00";
                cmbcompany.SelectedIndex = 0;
                cmbgroupMaster.SelectedIndex = 0;
                chkcompanyname.Checked = false;
                chkproductname.Checked = false;
                chkcode.Checked = false;
                chkgroupname.Checked = false;
            }
            catch (Exception)
            { }
        }
    }
}