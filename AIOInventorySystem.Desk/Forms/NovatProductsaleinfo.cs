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
    public partial class NovatProductsaleinfo : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public NovatProductsaleinfo()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            fillcombo();
            fillGroupname();
            customerdata();
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

        public void Searchlist()
        {
            try
            {
                string strsql = "";
                if (CommonMethod.commProduct == true)
                    strsql = "Select distinct m.BillNo as 'Bill No',CONVERT(VARCHAR(24),m.BillDate,103) as 'Bill Date',m.UserSeries as 'UserSeries',m.CustomerName as 'Customer Name',d.ProductId as 'Product Name',d.Unit,d.Quantity,isnull(d.TotalQuantitySubtitle,0) as 'SubQty',cast(fnm.SSS as decimal(18,3)) as 'Rate',d.TotalAmount from CustomerBillMaster as m inner join CustomerBillDetail as d on m.Id=d.BillNo inner join ProductInformation as p on d.ProductId=p.ProductName left join GroupMaster as g on  p.GroupMasterId=g.GroupMasterId  cross apply (Select (Isnull(d.Rate,0)-(Isnull(d.Rate,0)*isnull(d.Discount,0)/100)) as 'SDiscAmt') as c cross apply(Select isnull(c.SDiscAmt,0)-(isnull(c.SDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp Cross apply (Select isnull(sp.spdamt,0)+(isnull(sp.spdamt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm where m.CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "' and (m.BillNo like 'E%')";
                else
                    strsql = "Select distinct m.BillNo as 'Bill No',CONVERT(VARCHAR(24),m.BillDate,103) as 'Bill Date',m.UserSeries as 'UserSeries',m.CustomerName as 'Customer Name',d.ProductId as 'Product Name',d.Unit,d.Quantity,isnull(d.TotalQuantitySubtitle,0) as 'SubQty',cast(fnm.SSS as decimal(18,3)) as 'Rate',d.TotalAmount from CustomerBillMaster as m inner join CustomerBillDetail as d on m.Id=d.BillNo inner join ProductInformation as p on d.ProductId=p.ProductName and m.compid=p.compid left join GroupMaster as g on  p.GroupMasterId=g.GroupMasterId  cross apply (Select (Isnull(d.Rate,0)-(Isnull(d.Rate,0)*isnull(d.Discount,0)/100)) as 'SDiscAmt') as c cross apply(Select isnull(c.SDiscAmt,0)-(isnull(c.SDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp Cross apply (Select isnull(sp.spdamt,0)+(isnull(sp.spdamt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm where m.CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'  and (m.BillNo like 'E%')";
                if (chkproductname.Checked == true)
                {
                    if (txtProductname.Text != "")
                    {
                        string Proname1 = txtProductname.Text.Trim().Split(',')[0];
                        string company = txtProductname.Text.Trim().Split(',')[1];
                        string unit = txtProductname.Text.Trim().Split(',')[2];
                        strsql = strsql + " and d.ProductId='" + Proname1 + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "'";
                    }
                    else
                    {
                        MessageBox.Show("Enter Product Name");
                        this.ActiveControl = txtProductname;
                    }
                }
                if (chkcompanyname.Checked == true)
                {
                    if (cmbcompany.Text != "")
                        strsql = strsql + " and d.CompanyName='" + cmbcompany.Text + "' ";
                    else
                    {
                        MessageBox.Show("Select Company Name");
                        this.ActiveControl = cmbcompany;
                    }
                }
                if (chkdate.Checked == true)
                {
                    if (dtpfromdate.Value.Date <= dtptodate.Value.Date)
                        strsql = strsql + "and  m.Billdate>='" + cm.ValidFromDate(dtpfromdate.Value.Date) + "' and m.Billdate<='" + cm.ValidToDate(dtptodate.Value.Date) + "'";
                    else
                    {
                        MessageBox.Show("To Date Should Be Greater Than From Date");
                        this.ActiveControl = dtptodate;
                    }
                }
                if (chkgroupname.Checked == true)
                {
                    if (cmbgroupMaster.Text != "")
                        strsql = strsql + " and g.GroupMasterId=" + Convert.ToInt32(cmbgroupMaster.SelectedValue) + "";
                    else
                    {
                        MessageBox.Show("Select Group Name");
                        this.ActiveControl = cmbgroupMaster;
                    }
                }
                if (chkcode.Checked == true)
                {
                    if (txtcode.Text != "")
                        strsql = strsql + " and d.Code='" + txtcode.Text + "'";
                    else
                    {
                        MessageBox.Show("Enter Code");
                        this.ActiveControl = txtcode;
                    }
                }
                if (chkCustname.Checked == true)
                {
                    if (cmbcustname.SelectedIndex > 0)
                        strsql = strsql + "and m.Customername='" + cmbcustname.Text + "' and m.CustomerId=" + cmbcustname.SelectedValue + "";
                    else
                    {
                        MessageBox.Show("Select Customer Name");
                        this.ActiveControl = cmbcustname;
                    }
                }
                if (chkShipPartyName.Checked == true)
                {
                    if (cmbShipPartyName.SelectedIndex > 0)
                        strsql = strsql + " and m.ShipCustomerId=" + cmbShipPartyName.SelectedValue + "";
                    else
                    {
                        MessageBox.Show("Select Customer Name");
                        this.ActiveControl = cmbcustname;
                    }
                }
                if (chkdate.Checked == false && chkcode.Checked == false && chkcompanyname.Checked == false && chkgroupname.Checked == false && chkproductname.Checked == false && chkCustname.Checked == false && chkShipPartyName.Checked == false)
                {
                    MessageBox.Show("Please Select Check Box");
                    this.ActiveControl = chkproductname;
                }
                else
                {
                    GvproductInfo.DataSource = null;
                    DataTable dt = new DataTable();
                    dt = db.GetTable(strsql);
                    GvproductInfo.DataSource = dt;
                    lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(TotalAmount)", ""));
                    lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(Quantity)", ""));
                    lblTotSubQty.Text = Convert.ToString(dt.Compute("Sum(SubQty)", ""));
                    if (GvproductInfo.Rows.Count > 0)
                    {
                        var results = dt.Select("SubQty<>0 or SubQty<>null");
                        if (results.Count() > 0)
                        {
                            lblTotSubQty.Visible = true;
                            label6.Visible = true;
                        }
                        else
                        {
                            GvproductInfo.Columns["SubQty"].Visible = false;
                            lblTotSubQty.Visible = false;
                            label6.Visible = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Record Not Found");
                        this.ActiveControl = txtProductname;
                    }
                }
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
                    SqlCommand cmd = null;
                    if (CommonMethod.commProduct == true)
                        cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation ", con);
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
                this.ActiveControl = txtProductname;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd1 = new SqlCommand("SELECT d.Code FROM CustomerBillDetail d inner join CustomerBillMaster m on m.Id=d.BillNo WHERE d.Code IS NOT NULL and m.CompId='" + CommonMethod.CompId + "'", con);
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
                }
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
                    txtcode.Text = "";
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
                    txtcode.Text = "";
                    txtProductname.Text = "";
                    fillGroupname();
                    chkgroupname.Checked = false;
                    customerdata();
                    chkCustname.Checked = false;
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
                    fillcombo();
                    fillGroupname();
                    chkgroupname.Checked = false;
                    customerdata();
                    chkCustname.Checked = false;
                    txtProductname.Text = "";
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

            if (txtcode.Text.Trim() != "")
            {
                CustomerBillDetailRepository productrepo = new CustomerBillDetailRepository();
                var productdata = productrepo.GetAll().Where(t => t.Code == txtcode.Text && t.CustomerBillMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                if (productdata == null)
                {
                    MessageBox.Show("This Serial Number Is Not Valid.", "Warning");
                    txtcode.Text = "";
                    this.ActiveControl = txtcode;
                }
                productrepo.Dispose();
            }
        }

        private void chkgroupname_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkgroupname.Checked == true)
                {
                    chkproductname.Checked = false;
                    chkcompanyname.Checked = false;
                    fillcombo();
                    txtcode.Text = "";
                    txtProductname.Text = "";
                    chkcode.Checked = false;
                    customerdata();
                    chkCustname.Checked = false;
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

        public void customerdata()
        {
            try
            {
                CustomerRepository custrepo = new CustomerRepository();
                List<CustomerInformation> customerdata = new List<CustomerInformation>();
                customerdata.Clear();
                customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CustomerName).ToList();
                customerdata.Add(new CustomerInformation { CustomerName = "Select", CustomerID = 0 });
                customerdata.Reverse();
                cmbcustname.DataSource = customerdata;
                cmbcustname.ValueMember = "CustomerID";
                cmbcustname.DisplayMember = "CustomerName";
                custrepo.Dispose();
                cmbcustname.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        private void chkCustname_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkCustname.Checked == true)
                {
                    chkcompanyname.Checked = false;
                    fillcombo();
                    fillGroupname();
                    txtcode.Text = "";
                    chkcode.Checked = false;
                    chkgroupname.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbcustname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btncheck;
            }
            catch (Exception)
            { }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string pname1 = "", custname = "", mfgcompany = "", from1 = "", to1 = "";
                int groupid = 0, code = 0,ShipPartyId=0;
                if (chkproductname.Checked == true || chkgroupname.Checked == true || chkCustname.Checked == true || chkcode.Checked == true || chkcompanyname.Checked == true || chkdate.Checked == true)
                {
                    if (chkproductname.Checked == true && txtProductname.Text != "")
                        pname1 = txtProductname.Text;
                    if (chkCustname.Checked == true && cmbcustname.Text != "")
                        custname = cmbcustname.Text;
                    if (chkcompanyname.Checked == true && cmbcompany.Text != "")
                        mfgcompany = cmbcompany.Text;
                    if (chkgroupname.Checked == true && cmbgroupMaster.Text != "")
                        groupid = Convert.ToInt32(cmbgroupMaster.SelectedValue);
                    if (chkcode.Checked == true && txtcode.Text != "")
                        code = Convert.ToInt32(txtcode.Text);
                    if (chkdate.Checked == true && dtpfromdate.Value.Date <= dtptodate.Value.Date)
                    {
                        from1 = dtpfromdate.Text;
                        to1 = dtptodate.Text;
                    }
                }
                else
                {
                    MessageBox.Show("Plase Check CheckBox First.", "Warning");
                    goto b;
                }
                RptProductWiseSale prodrpt = new RptProductWiseSale(pname1, custname, mfgcompany, groupid, code, false, from1, to1, ShipPartyId);
                prodrpt.ShowDialog();
                prodrpt.Dispose();
            b:
                { }
            }
            catch (Exception) { }
        }

        private void btncheck_Click(object sender, EventArgs e)
        {
            try
            {
                Searchlist();
            }
            catch (Exception) { }
        }

        private void btnew_Click(object sender, EventArgs e)
        {
            try
            {
                txtProductname.Text = "";
                this.ActiveControl = txtProductname;
                txtcode.Text = "";
                GvproductInfo.DataSource = null;
                GvproductInfo.Rows.Clear();
                lblTotalAmount.Text = "0.00";
                lbltotalqty.Text = "0.00";
                fillcombo();
                fillGroupname();
                chkcompanyname.Checked = false;
                chkproductname.Checked = false; chkcode.Checked = false;
                chkgroupname.Checked = false;
                customerdata();
                chkCustname.Checked = false;
            }
            catch (Exception)
            { }
        }
    }
}