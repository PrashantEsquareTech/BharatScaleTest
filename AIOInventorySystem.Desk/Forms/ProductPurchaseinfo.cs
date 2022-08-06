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
    public partial class ProductPurchaseinfo : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public ProductPurchaseinfo()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            fillcombo();
            fillGroupname();
            fillGodownName();
            fillsupplier();
        }

        public void fillGodownName()
        {
            try
            {
                GodownMasterRepository GDRepo = new GodownMasterRepository();
                List<GodownMaster> GDData = new List<GodownMaster>();
                GDData.Clear();
                if (CommonMethod.commProduct == true)
                    GDData = GDRepo.GetAll().OrderByDescending(t => t.GodownName).ToList();
                else
                    GDData = GDRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.GodownName).ToList();
                if (GDData.Count > 0)
                {
                    GDData.Add(new GodownMaster { GodownName = "Shop", ID = 0 });
                    GDData.Reverse();
                    cmbGodownName.DataSource = GDData;
                    cmbGodownName.ValueMember = "ID";
                    cmbGodownName.DisplayMember = "GodownName";
                    cmbGodownName.Enabled = true;
                    GDRepo.Dispose();
                }
                else
                {
                    cmbGodownName.DataSource = null;
                    cmbGodownName.Items.Clear();
                    cmbGodownName.DisplayMember = "Text";
                    cmbGodownName.ValueMember = "Value";
                    cmbGodownName.Items.Add(new { Text = "Shop", Value = 0 });
                    cmbGodownName.SelectedIndex = 0;
                    cmbGodownName.Enabled = false;
                }
            }
            catch (Exception)
            { }
        }
        public void fillsupplier()
        {
            try
            {
                SupplierRepository supplierrepo = new SupplierRepository();
                List<SupplierInformation> sdata = new List<SupplierInformation>();
                sdata.Clear();
                if (CommonMethod.commProduct == true)
                    sdata = supplierrepo.GetAll().OrderByDescending(t => t.SupplierName).ToList();
                else
                    sdata = supplierrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.SupplierName).ToList();
                sdata.Add(new SupplierInformation { SupplierName = "Select", SupplierCode = 0 });
                sdata.Reverse();
                cmbsuppliername.DataSource = sdata;
                cmbsuppliername.ValueMember = "SupplierCode";
                cmbsuppliername.DisplayMember = "SupplierName";
                supplierrepo.Dispose();
                cmbsuppliername.SelectedIndex = 0;
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
                        { MyCollection.Add(reader.GetString(0) + "," + reader.GetString(1) + "," + reader.GetString(2)); }
                        catch (Exception)
                        { }
                    }
                    txtProductname.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd1 = new SqlCommand("SELECT d.Code FROM PurchaseDetail d inner join PurchaseMaster m on m.Id=d.PorderNo WHERE d.Code IS NOT NULL and m.CompId='" + CommonMethod.CompId + "' ", con);
                    con.Open();
                    SqlDataReader reader = cmd1.ExecuteReader();
                    AutoCompleteStringCollection MyCollection1 = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        { MyCollection1.Add(reader.GetString(0)); }
                        catch (Exception)
                        { }
                    }
                    txtcode.AutoCompleteCustomSource = MyCollection1;
                    con.Close();
                    reader.Dispose();
                }
                this.ActiveControl = txtcode;
            }
            catch (Exception)
            { }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
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
                chkgroupname.Checked = false;
                chkcompanyname.Checked = false;
                chkproductname.Checked = false;
                chkcode.Checked = false;
                fillsupplier();
                chksuppliername.Checked = false;
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
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void SearchList()
        {
            try
            {
                string strsql = "";
                if (cmbGodownName.SelectedIndex == 0)
                    strsql = "Select m.PorderNo,Convert(varchar(24),m.PorderDate,103)  as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm where  m.SupplierName!='First Stock' and (d.SGstPercent!=NULL or d.SGstPercent!=0 or d.CGstPercent!=NULL or d.CGstPercent!=0 or d.IGstPercent!=NULL or d.IGstPercent!=0 or m.TAXType='TaxPurchase' ) and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and (m.StockIn='" + cmbGodownName.SelectedValue + "' or m.StockIn is null)";
                else
                    strsql = "Select m.PorderNo,Convert(varchar(24),m.PorderDate,103)  as 'Porder Date',m.Suppliername as 'Supplier Name',d.ProductName as 'Product Name',cast(fnm.SSS as decimal(18,2)) as 'Pur Price',(d.Quantity+isnull(d.FreeQty,0)) as Quantity,isnull(TotalQuantitySubtitle,0) as 'SubQty',d.TotalAmount from PurchaseMaster as m inner join PurchaseDetail as d on m.Id=d.PorderNo inner join ProductInformation as p on p.ProductName=d.ProductName and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit left join GroupMaster as g on g.GroupMasterId=p.GroupMasterId  cross apply (Select (Isnull(d.PurchasePrice,0)-(Isnull(d.PurchasePrice,0)*isnull(d.Discount,0)/100)) as 'PDiscAmt') as c cross apply(Select isnull(c.PDiscAmt,0)-(isnull(c.PDiscAmt,0)*isnull(d.SpcDisc,0)/100) as 'spdamt') as sp cross apply (Select isnull(sp.spdamt,0)-(isnull(sp.spdamt,0)*isnull(d.ThreeLevelDiscPer,0)/100) as 'ThreeDiscAmt') as tmn Cross apply (Select isnull(tmn.ThreeDiscAmt,0)+(isnull(tmn.ThreeDiscAmt,0)*(case when (d.SGstPercent!=NULL or d.SGstPercent!=0)  and (d.SGstPercent!=NULL or d.SGstPercent!=0) then Isnull(d.SGstPercent,0)+Isnull(d.CGstPercent,0) else case when (d.SGstPercent is null  or d.SGstPercent=0) and (d.CGstPercent is null  or d.CGstPercent=0) then Isnull(d.IGstPercent,0) else 0 end end)/100) as 'SSS') as fnm where m.SupplierName!='First Stock' and (d.SGstPercent!=NULL or d.SGstPercent!=0 or d.CGstPercent!=NULL or d.CGstPercent!=0 or d.IGstPercent!=NULL or d.IGstPercent!=0 or m.TAXType='TaxPurchase' ) and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' and m.StockIn='" + cmbGodownName.SelectedValue + "'";
                if (chkproductname.Checked == true)
                {
                    if (txtProductname.Text != "")
                    {
                        string Proname1 = txtProductname.Text.Trim().Split(',')[0];
                        string company = txtProductname.Text.Trim().Split(',')[1];
                        string unit = txtProductname.Text.Trim().Split(',')[2];
                        strsql = strsql + " and d.ProductName='" + Proname1 + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "'";
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
                if (chkbetweendate.Checked == true)
                {
                    if (dtpfromdate.Value.Date <= dtptodate.Value.Date)
                        strsql = strsql + " and m.PorderDate>='" + cm.ValidFromDate(dtpfromdate.Value.Date) + "' and m.PorderDate<='" + cm.ValidToDate(dtptodate.Value.Date) + "' ";
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
                if (chksuppliername.Checked == true)
                {
                    if (cmbsuppliername.SelectedIndex > 0)
                        strsql = strsql + "and m.Suppliername='" + cmbsuppliername.Text + "' and m.SupplierId=" + cmbsuppliername.SelectedValue + "";
                    else
                    {
                        MessageBox.Show("Select Supplier Name");
                        this.ActiveControl = cmbsuppliername;
                    }
                }
                if (chkbetweendate.Checked == false && chkcode.Checked == false && chkcompanyname.Checked == false && chkgroupname.Checked == false && chkproductname.Checked == false && chksuppliername.Checked == false)
                {
                    MessageBox.Show("Please Select Check Box");
                    this.ActiveControl = chkproductname;
                }
                else
                {
                    strsql = strsql + " order by PorderDate";
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
                            label1.Visible = true;
                        }
                        else
                        {
                            GvproductInfo.Columns["SubQty"].Visible = false;
                            lblTotSubQty.Visible = false;
                            label1.Visible = false;
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
                    PurchaseDetailRepository productrepo = new PurchaseDetailRepository();
                    var productdata = productrepo.GetAll().Where(t => t.Code == txtcode.Text && t.PurchaseMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (productdata == null)
                    {
                        MessageBox.Show("This Serial Number Is Not Valid.", "Warning");
                        txtcode.Text = "";
                        this.ActiveControl = txtcode;
                    }
                    productrepo.Dispose();
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

        private void cmbcompany_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbcompany.Text != "Select")
                {
                    ManufactureCInfoRepository unitrepo = new ManufactureCInfoRepository();
                    ManuCompanyInfo unitdat = new ManuCompanyInfo();
                    if (CommonMethod.commProduct == true)
                        unitdat = unitrepo.GetAll().Where(t => t.CompanyName == cmbcompany.Text).FirstOrDefault();
                    else
                        unitdat = unitrepo.GetAll().Where(t => t.CompanyName == cmbcompany.Text && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (unitdat == null)
                    {
                        MessageBox.Show("Invalid Company Name.", "Warning");
                        fillcombo();
                        this.ActiveControl = cmbcompany;
                    }
                    unitrepo.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void cmbGodownName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btncheck;
            }
            catch (Exception)
            { }
        }

        private void chkproductname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtProductname;
            }
            catch (Exception)
            { }
        }

        private void chkcompanyname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcompany;
            }
            catch (Exception)
            { }
        }

        private void chkcode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtcode;
            }
            catch (Exception)
            { }
        }

        private void chkgroupname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbgroupMaster;
            }
            catch (Exception)
            { }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int groupid = 0, code = 0;
                string pname1 = "", suppliername = "", mfgcompany = "", from1 = "", to1 = "";
                if (chkproductname.Checked == true || chkgroupname.Checked == true || chkcode.Checked == true || chkcompanyname.Checked == true || chkbetweendate.Checked == true || chksuppliername.Checked == true)
                {
                    if (chkproductname.Checked == true)
                        pname1 = txtProductname.Text;
                    if (chkgroupname.Checked == true)
                        groupid = Convert.ToInt32(cmbgroupMaster.SelectedValue);
                    if (chkcode.Checked == true)
                        code = Convert.ToInt32(txtcode.Text);
                    if (chkcompanyname.Checked == true)
                        mfgcompany = cmbcompany.Text;
                    if (chkbetweendate.Checked == true && dtpfromdate.Value.Date <= dtptodate.Value.Date)
                    {
                        from1 = dtpfromdate.Text;
                        to1 = dtptodate.Text;
                    }
                    if (chksuppliername.Checked == true)
                        suppliername = cmbsuppliername.Text;
                }
                else
                    MessageBox.Show("Please Check CheckBox First.", "Warning");
                RptProductWisePurchase crpt = new RptProductWisePurchase(pname1, suppliername, mfgcompany, groupid, code, true, from1, to1, Convert.ToInt32(cmbGodownName.SelectedIndex));
                crpt.ShowDialog();
                crpt.Dispose();
            }
            catch (Exception) { }
        }

        private void chkbetweendate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpfromdate;
            }
            catch (Exception) { }
        }

        private void dtpfromdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtptodate;
            }
            catch (Exception) { }
        }

        private void dtptodate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btncheck;
            }
            catch (Exception) { }
        }

        private void chksuppliername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbsuppliername;
            }
            catch (Exception) { }
        }

        private void cmbsuppliername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btncheck;
            }
            catch (Exception) { }
        }

        private void btncheck_Click(object sender, EventArgs e)
        {
            try
            {
                SearchList();
            }
            catch (Exception) { }
        }
    }
}