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
    public partial class frmPurchaseOrderList : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        string transType = "", PurchaseAcc1 = "";
        DateTime fdate1, tdate1;
        public int id, productentrytype = 0;
        List<int> billList = new List<int>();

        public frmPurchaseOrderList()
        {
            InitializeComponent();
            fillsupplier();
            cm.changedatetimepickerrange(this);
            fillcashcredit();
            fillGroupname();
            fillcombo();
            hide();
            SettingRepository settRepo = new SettingRepository();
            Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (sdata.DeleteSaleBill == "True")
                rbtnMultipleDeletebills.Enabled = true;
            else
                rbtnMultipleDeletebills.Enabled = false;
            Bindgrid();
        }

        public frmPurchaseOrderList(DateTime from, DateTime to, string transtype = "")
        {
            InitializeComponent();
            fillsupplier();
            cm.changedatetimepickerrange(this);
            fillcashcredit();
            fillGroupname();
            fillcombo();
            hide();
            fdate1 = from;
            tdate1 = to;
            transType = transtype;
            Bindgrid(from, to, transtype);
            SettingRepository settRepo = new SettingRepository();
            Setting sdata = new Setting();
            if (sdata.DeleteSaleBill == "True")
                rbtnMultipleDeletebills.Enabled = true;
            else
                rbtnMultipleDeletebills.Enabled = false;
        }

        public frmPurchaseOrderList(string PurchaseAcc, DateTime fdate, DateTime tdate)
        {
            InitializeComponent();
            fillsupplier();
            cm.changedatetimepickerrange(this);
            fillcashcredit();
            fillGroupname();
            fillcombo();
            hide();
            fdate1 = fdate;
            tdate1 = tdate;
            PurchaseAcc1 = PurchaseAcc;
            SettingRepository settRepo = new SettingRepository();
            Setting sdata = new Setting();
            if (sdata.DeleteSaleBill == "True")
                rbtnMultipleDeletebills.Enabled = true;
            else
                rbtnMultipleDeletebills.Enabled = false;
            TotalPLACToPurchase(PurchaseAcc, fdate, tdate);
        }

        public void Bindgrid(DateTime from, DateTime to, string transactiontype = "")
        {
            try
            {
                CommonMethod com = new CommonMethod();
                string strsql = "";
                if (transactiontype == "Cash")
                    strsql = "SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.ManualPurRefNo,pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.Suppliername!='First Stock' and pm.PorderDate >= '" + com.ValidFromDate(from.Date) + "' and  pm.PorderDate <= '" + com.ValidToDate(to.Date) + "' and pm.PaidAmount!=0 and (pd.SGstPercent!=NULL or pd.SGstPercent!=0 or pd.CGstPercent!=NULL or pd.CGstPercent!=0 or pd.IGstPercent!=NULL or pd.IGstPercent!=0 or pm.TAXType='TaxPurchase') and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "' and pm.RemainingAmount='0' order by CONVERT(VARCHAR(24),pm.PorderDate,103),pm.ID";
                else
                    strsql = "SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.ManualPurRefNo,pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.Suppliername!='First Stock' and pm.PorderDate >= '" + com.ValidFromDate(from.Date) + "' and  pm.PorderDate <= '" + com.ValidToDate(to.Date) + "' and (pd.SGstPercent!=NULL or pd.SGstPercent!=0 or pd.CGstPercent!=NULL or pd.CGstPercent!=0 or pd.IGstPercent!=NULL or pd.IGstPercent!=0 or pm.TAXType='TaxPurchase') and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "' order by CONVERT(VARCHAR(24),pm.PorderDate,103),pm.ID";
                SqlDataAdapter da = new SqlDataAdapter(strsql, db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvPorderInfo.DataSource = dt;
                lbltoatdisamt.Visible = true;
                lbltoatdisamt.Text = "Total Purchase Amount:";
                disamt.Show();
                disamt.Text = Convert.ToString(dt.Compute("Sum(Total_Amount)", ""));
                GvPorderInfo.Refresh();
                lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                if (GvPorderInfo.Rows.Count != 0)
                {
                    if (rbtnMultipleDeletebills.Enabled == false)
                        GvPorderInfo.Columns[0].Visible = false;
                    else
                        GvPorderInfo.Columns[0].Visible = true;
                    GvPorderInfo.Columns["ID"].Visible = false;
                    GvPorderInfo.Columns["Purchase Order Number"].Width = 150;
                    GvPorderInfo.Columns["Purchase Order Date"].Width = 150;
                    GvPorderInfo.Columns["ManualPurRefNo"].Width = 150;
                    GvPorderInfo.Columns["Supplier Name"].Width = 250;
                    GvPorderInfo.Columns["Total_Amount"].Width = 155;
                    this.ActiveControl = dtpFromPorderdate;
                }
                da.Dispose();
                dt.Dispose();
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

        public void Bindgrid()
        {
            try
            {
                DateTime date = DateTime.Now.Date;
                SqlDataAdapter da = new SqlDataAdapter("SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.ManualPurRefNo,pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.Suppliername!='First Stock' and (pd.SGstPercent!=NULL or pd.SGstPercent!=0 or pd.CGstPercent!=NULL or pd.CGstPercent!=0 or pd.IGstPercent!=NULL or pd.IGstPercent!=0 or pm.TAXType='TaxPurchase') and CONVERT(VARCHAR(24),pm.PorderDate,103)='" + date.ToString("dd/MM/yyyy") + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "' order by CONVERT(VARCHAR(24),pm.PorderDate,103),pm.ID", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvPorderInfo.DataSource = dt;
                lbltoatdisamt.Visible = true;
                lbltoatdisamt.Text = "Total Purchase Amount:";
                disamt.Show();
                disamt.Text = Convert.ToString(dt.Compute("Sum(Total_Amount)", ""));
                lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                GvPorderInfo.Refresh();
                if (rbtnMultipleDeletebills.Enabled == false)
                    GvPorderInfo.Columns[0].Visible = false;
                else
                    GvPorderInfo.Columns[0].Visible = true;
                GvPorderInfo.Columns["ID"].Visible = false;
                GvPorderInfo.Columns["Purchase Order Number"].Width = 150;
                GvPorderInfo.Columns["Purchase Order Date"].Width = 150;
                GvPorderInfo.Columns["ManualPurRefNo"].Width = 150;
                GvPorderInfo.Columns["Supplier Name"].Width = 250;
                GvPorderInfo.Columns["Total_Amount"].Width = 155;
                da.Dispose();
                dt.Dispose();
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
                cmbsname.DataSource = sdata;
                cmbsname.ValueMember = "SupplierCode";
                cmbsname.DisplayMember = "SupplierName";
                supplierrepo.Dispose();
                cmbsname.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void AutoCompletePONo()
        {
            try
            {
                txtPOrderno.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = new SqlCommand("SELECT CONVERT(varchar(10), PorderNo) FROM PurchaseMaster where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " ", con);
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
                    txtPOrderno.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        public void AutoCompleteProductName()
        {
            try
            {
                txtproductname.Text = "";
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
                    txtproductname.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        public void AutoRefNo()
        {
            try
            {
                txtManRefNo.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = null;
                    cmd = new SqlCommand("SELECT ManualPurRefNo FROM PurchaseMaster where compid=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'", con);
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
                    txtManRefNo.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        public void fillcashcredit()
        {
            cmbcashsredit.Items.Clear();
            cmbcashsredit.Text = "Select";
            cmbcashsredit.Items.Add("Cash");
            cmbcashsredit.Items.Add("Credit");
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    dtpFromPorderdate.Value = DateTime.Now.Date;
                    dtpToPorderDate.Value = DateTime.Now.Date;
                }
                catch (Exception)
                { }
                txtPOrderno.Text = "";
                chkporderno.CheckState = 0;
                chkbetweendate.CheckState = 0;
                chksname.CheckState = 0;
                chkcashcredit.CheckState = 0;
                chktranscharge.CheckState = 0;
                fillcashcredit();
                fillsupplier();
                hide();
                chkdiscount.CheckState = 0;
                fillcombo();
                fillGroupname();
                chkgroupname.CheckState = 0;
                chkproductname.CheckState = 0;
                chkmfgcom.CheckState = 0;
                chkfirststock.CheckState = 0;
                chkTaxPurchase.CheckState = 0;
                chkMPurRefNo.Checked = false;
                txtManRefNo.Text = "";
            }
            catch (Exception)
            { }
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.ManualPurRefNo,pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.Suppliername!='First Stock' and (pd.SGstPercent!=NULL or pd.SGstPercent!=0 or pd.CGstPercent!=NULL or pd.CGstPercent!=0 or pd.IGstPercent!=NULL or pd.IGstPercent!=0 or pm.TAXType='TaxPurchase') and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "' order by CONVERT(VARCHAR(24),pm.PorderDate,103),pm.ID", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvPorderInfo.DataSource = dt;
                lbltoatdisamt.Visible = true;
                lbltoatdisamt.Text = "Total Purchase Amount:";
                disamt.Show();
                disamt.Text = Convert.ToString(dt.Compute("Sum(Total_Amount)", ""));
                lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                if (rbtnMultipleDeletebills.Enabled == false)
                    GvPorderInfo.Columns[0].Visible = false;
                else
                    GvPorderInfo.Columns[0].Visible = true;
                GvPorderInfo.Columns["ID"].Visible = false;
                GvPorderInfo.Columns["Purchase Order Number"].Width = 150;
                GvPorderInfo.Columns["Purchase Order Date"].Width = 150;
                GvPorderInfo.Columns["ManualPurRefNo"].Width = 150;
                GvPorderInfo.Columns["Supplier Name"].Width = 250;
                GvPorderInfo.Columns["Total_Amount"].Width = 155;
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        public void searchlist()
        {
            string strsql = "";
            bool isvalid = true;
            try
            {
                hide();
                if (CommonMethod.commProduct == true)
                    strsql = "SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.ManualPurRefNo,pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount',pm.PorderDate FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo inner join ProductInformation as p on pd.ProductName=p.ProductName where (pd.SGstPercent!=NULL or pd.SGstPercent!=0 or pd.CGstPercent!=NULL or pd.CGstPercent!=0 or pd.IGstPercent!=NULL or pd.IGstPercent!=0 or pm.TAXType='TaxPurchase')  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'";
                else
                    strsql = "SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.ManualPurRefNo,pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount',pm.PorderDate FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo inner join ProductInformation as p on pd.ProductName=p.ProductName and pm.compid=p.compid where (pd.SGstPercent!=NULL or pd.SGstPercent!=0 or pd.CGstPercent!=NULL or pd.CGstPercent!=0 or pd.IGstPercent!=NULL or pd.IGstPercent!=0 or pm.TAXType='TaxPurchase')  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "' and p.compid=" + CommonMethod.CompId + " ";

                if (chkporderno.Checked == true && isvalid == true)
                {
                    if (txtPOrderno.Text != "")
                        strsql = strsql + " and pm.PorderNo='" + txtPOrderno.Text + "'";
                    else
                    {
                        MessageBox.Show("Enter Porder No.", "Warning");
                        this.ActiveControl = txtPOrderno;
                        isvalid = false;
                    }
                }
                if (chkbetweendate.Checked == true && isvalid == true)
                {
                    if (dtpFromPorderdate.Value.Date <= dtpToPorderDate.Value.Date)
                    {
                        CommonMethod com = new CommonMethod();
                        strsql = strsql + " and pm.Suppliername!='First Stock' and pm.PorderDate>='" + com.ValidFromDate(dtpFromPorderdate.Value.Date) + "' and pm.PorderDate<='" + com.ValidToDate(dtpToPorderDate.Value.Date) + "'";
                    }
                    else
                    {
                        MessageBox.Show("To Date Should greater than or equal to from date.", "Warning");
                        this.ActiveControl = dtpToPorderDate;
                        isvalid = false;
                    }
                }
                if (chksname.Checked == true && isvalid == true)
                {
                    if (cmbsname.Text != "")
                        strsql = strsql + " and pm.Suppliername!='First Stock' and pm.SupplierId='" + cmbsname.SelectedValue + "'";
                    else
                    {
                        MessageBox.Show("Select Supplier name.", "Warning");
                        this.ActiveControl = cmbsname;
                        isvalid = true;
                    }
                }
                if (chkproductname.Checked == true && isvalid == true)
                {
                    if (txtproductname.Text != "")
                    {
                        string Proname1 = txtproductname.Text.Trim().Split(',')[0];
                        string company = txtproductname.Text.Trim().Split(',')[1];
                        string unit = txtproductname.Text.Trim().Split(',')[2];
                        string Proname = "";
                        if ((Proname1).Contains("'"))
                            Proname = Proname1.Replace("'", "''");
                        else
                            Proname = Proname1;
                        strsql = strsql + " and pd.ProductName='" + Proname + "' and pd.CompanyName='" + company + "' and pd.Unit='" + unit + "'";
                    }
                    else
                    {
                        MessageBox.Show("Select Product Name.", "Warning");
                        this.ActiveControl = txtproductname;
                        isvalid = true;
                    }
                }
                if (chkcashcredit.Checked == true && isvalid == true)
                {
                    if (cmbcashsredit.Text != "Select")
                    {
                        if (cmbcashsredit.Text == "Cash")
                            strsql = strsql + "and pm.RemainingAmount='" + 0.00 + "'";
                        else
                            strsql = strsql + "and pm.RemainingAmount!='" + 0.00 + "'";
                    }
                    else
                    {
                        MessageBox.Show("Select Mode.", "Warning");
                        this.ActiveControl = cmbcashsredit;
                        isvalid = false;
                    }
                }
                if (chkmfgcom.Checked == true && isvalid == true)
                {
                    if (cmbcompany.Text != "Select")
                        strsql = strsql + "and pd.CompanyName='" + cmbcompany.Text + "'";
                    else
                    {
                        MessageBox.Show("Select mfg Company.", "Warning");
                        this.ActiveControl = cmbcompany;
                        isvalid = false;
                    }
                }
                if (chkgroupname.Checked == true && isvalid == true)
                {
                    if (cmbgroupMaster.Text != "Select")
                        strsql = strsql + "and p.GroupMasterId='" + cmbgroupMaster.SelectedValue + "' ";
                    else
                    {
                        MessageBox.Show("Select Group Name.", "Warning");
                        isvalid = false;
                    }
                }
                if (chktranscharge.Checked == true && isvalid == true)
                    strsql = strsql + "and pm.TransportCharges!=0 ";
                if (chkdiscount.Checked == true && isvalid == true)
                    strsql = strsql + "and pm.Discount!=0";
                if (chkTaxPurchase.Checked == true)
                    strsql = strsql + " and pm.TAXType='TaxPurchase' ";
                if (chkMPurRefNo.Checked == true)
                {
                    if (txtManRefNo.Text != "")
                        strsql = strsql + " and pm.ManualPurRefNo='" + txtManRefNo.Text + "'";
                    else
                    {
                        MessageBox.Show("Enter Manual Ref No");
                        this.ActiveControl = txtManRefNo;
                    }
                }
                if (chkfirststock.Checked == true)
                {
                    //if (strsql != "")
                    //    strsql = strsql + " and pm.Suppliername='First Stock' ";
                    //else
                    strsql = "SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.ManualPurRefNo,pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.Suppliername='First Stock' and (pd.SGstPercent!=NULL or pd.SGstPercent!=0 or pd.CGstPercent!=NULL or pd.CGstPercent!=0 or pd.IGstPercent!=NULL or pd.IGstPercent!=0 or pm.TAXType='TaxPurchase')  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "' ";
                }

                strsql = strsql + "GROUP BY pm.ID,pm.PorderNo,CONVERT(VARCHAR(24),pm.PorderDate,103),pm.ManualPurRefNo,pm.Suppliername,pm.NetAmount,pm.PorderDate ";
                if (chkOrderBy.Checked)
                {
                    if (cmbOrderBy.Text != "")
                    {
                        if (cmbOrderBy.Text == "POrderNo")
                            strsql = strsql + "order by pm.POrderNo";
                        else if (cmbOrderBy.Text == "PDate")
                            //strsql = strsql + "order by CONVERT(VARCHAR(24),pm.PorderDate,103),pm.ID";
                            strsql = strsql + "order by pm.PorderDate";
                        else if (cmbOrderBy.Text == "SupplierName")
                            strsql = strsql + "order by pm.Suppliername";
                    }
                    else
                        strsql = strsql + "order by pm.PorderDate";
                }
                else
                    strsql = strsql + "order by pm.PorderDate";
                SqlDataAdapter da = new SqlDataAdapter(strsql, db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvPorderInfo.DataSource = dt;
                lbltoatdisamt.Visible = true;
                lbltoatdisamt.Text = "Total Purchase Amount:";
                disamt.Show();
                disamt.Text = Convert.ToString(dt.Compute("Sum(Total_Amount)", ""));
                GvPorderInfo.Refresh();
                lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                if (GvPorderInfo.Rows.Count != 0)
                {
                    if (rbtnMultipleDeletebills.Enabled == false)
                        GvPorderInfo.Columns[0].Visible = false;
                    else
                        GvPorderInfo.Columns[0].Visible = true;
                    GvPorderInfo.Columns["ID"].Visible = false;
                    GvPorderInfo.Columns["Purchase Order Number"].Width = 150;
                    GvPorderInfo.Columns["Purchase Order Date"].Width = 150;
                    GvPorderInfo.Columns["ManualPurRefNo"].Width = 150;
                    GvPorderInfo.Columns["Supplier Name"].Width = 250;
                    GvPorderInfo.Columns["Total_Amount"].Width = 155;
                    GvPorderInfo.Columns["PorderDate"].Visible = false;
                }
                else
                    MessageBox.Show("Record not found.", "Warning");
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception) { }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            hide();
            searchlist();
        }

        public void hide()
        {
            try
            {
                lbltoatdisamt.Visible = false;
                disamt.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void chkbetweendate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkbetweendate.Checked == true)
                {
                    transType = "";
                    PurchaseAcc1 = "";
                }
            }
            catch (Exception)
            { }
        }

        private void chksname_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chksname.Checked == true)
                {
                    chkporderno.Checked = false;
                    txtPOrderno.Text = "";
                    cmbcashsredit.Text = "Select";
                    chkcashcredit.Checked = false;
                    chktranscharge.Checked = false;
                    chkdiscount.Checked = false;
                    fillGroupname();
                    cmbcompany.Text = "Select";
                    txtproductname.Text = "";
                    chkgroupname.Checked = false;
                    chkmfgcom.Checked = false;
                    chkproductname.Checked = false;
                    chkfirststock.Checked = false;
                    chkTaxPurchase.Checked = false;
                    transType = "";
                    PurchaseAcc1 = "";
                }
            }
            catch (Exception)
            { }
        }

        private void chkporderno_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkporderno.Checked == true)
                {
                    chksname.Checked = false;
                    cmbsname.Text = "Select";
                    cmbcashsredit.Text = "Select";
                    chkcashcredit.Checked = false;
                    chktranscharge.Checked = false;
                    chkdiscount.Checked = false;
                    fillGroupname();
                    cmbcompany.Text = "Select";
                    txtproductname.Text = "";
                    chkgroupname.Checked = false;
                    chkmfgcom.Checked = false;
                    chkproductname.Checked = false;
                    chkfirststock.Checked = false;
                    chkTaxPurchase.Checked = false;
                    transType = "";
                    PurchaseAcc1 = "";
                }
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

        private void GvPorderInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    SettingRepository sRepo = new SettingRepository();
                    Setting sdata = sRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (Convert.ToString(GvPorderInfo.Rows[e.RowIndex].Cells["Supplier Name"].Value) != "First stock")
                    {
                        id = Convert.ToInt32(GvPorderInfo.Rows[e.RowIndex].Cells["Purchase Order Number"].Value);
                        if (sdata.ShowLoginWindowOnForm == "P")
                        {
                            pnlLogin.Visible = true;
                            this.ActiveControl = txtusername;
                        }
                        else
                        {
                            if (frmPurchaseOrder.purorderinfo == null)
                            {
                                frmPurchaseOrder purord = new frmPurchaseOrder(id);
                                purord.ShowDialog();
                                frmPurchaseOrder.purorderinfo = null;
                            }
                            else
                            {
                                this.Close();
                                frmPurchaseOrder.purorderinfo.LoadData(id);
                                frmPurchaseOrder.purorderinfo.Show();
                                frmPurchaseOrder.purorderinfo = null;
                            }
                        }
                    }
                    else
                        MessageBox.Show("This Purchase Order User Can not Updated.", "Warning");
                }
            }
            catch (Exception)
            { }
        }

        private void GvPorderInfo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (Char)Keys.Enter)
                {
                    if (GvPorderInfo.CurrentCellAddress.X == 1)
                    {
                        frmPurchaseOrder pdata = new frmPurchaseOrder(Convert.ToInt32(GvPorderInfo.Rows[GvPorderInfo.CurrentCellAddress.Y].Cells["Purchase Order Number"].Value));
                        pdata.WindowState = System.Windows.Forms.FormWindowState.Normal;
                        pdata.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                        pdata.ShowDialog();
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void chkcashcredit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkcashcredit.Checked == true)
                {
                    chksname.Checked = false;
                    cmbsname.Text = "Select";
                    chkporderno.Checked = false;
                    txtPOrderno.Text = "";
                    chktranscharge.Checked = false;
                    chkdiscount.Checked = false;
                    fillGroupname();
                    cmbcompany.Text = "Select";
                    txtproductname.Text = "";
                    chkgroupname.Checked = false;
                    chkmfgcom.Checked = false;
                    chkproductname.Checked = false;
                    chkfirststock.Checked = false;
                    chkTaxPurchase.Checked = false;
                    transType = ""; PurchaseAcc1 = "";
                }
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                printAll();
            }
            catch (Exception)
            { }
        }

        public void printAll()
        {
            try
            {
                string mode = null;
                int pono = 0;
                RptAllPurchase allpurchrpt;
                string supplier = "";
                string from1 = "";
                string to1 = "";
                string productname = "";
                string mfgcomp = "";
                if (chksname.Checked == true)
                    supplier = cmbsname.Text;
                if (chkporderno.Checked == true)
                    pono = Convert.ToInt32(txtPOrderno.Text);
                if (chkmfgcom.Checked == true)
                    mfgcomp = cmbcompany.Text;
                if (chkcashcredit.Checked == true && cmbcashsredit.Text != "Select")
                    mode = cmbcashsredit.Text;
                if (chkbetweendate.Checked == true && dtpFromPorderdate.Value.Date <= dtpToPorderDate.Value.Date)
                {
                    from1 = dtpFromPorderdate.Text;
                    to1 = dtpToPorderDate.Text;
                }
                if (chkproductname.Checked == true)
                    productname = txtproductname.Text;
                if (transType == "Cash")
                {
                    from1 = Convert.ToString(fdate1);
                    to1 = Convert.ToString(tdate1);
                }
                else if (transType == "Day")
                {
                    from1 = Convert.ToString(fdate1);
                    to1 = Convert.ToString(tdate1);
                }
                if (PurchaseAcc1 != "")
                {
                    from1 = Convert.ToString(fdate1);
                    to1 = Convert.ToString(tdate1);
                }
                allpurchrpt = new RptAllPurchase(supplier, mode, mfgcomp, pono, from1, to1, productname, transType, PurchaseAcc1);
                allpurchrpt.Show();
            }
            catch (Exception) { }
        }

        private void chktranscharge_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chktranscharge.Checked == true)
                {
                    chksname.Checked = false;
                    cmbsname.Text = "Select";
                    chkporderno.Checked = false;
                    txtPOrderno.Text = "";
                    chkcashcredit.Checked = false;
                    fillcashcredit();
                    chkdiscount.Checked = false;
                    fillGroupname();
                    cmbcompany.Text = "Select";
                    txtproductname.Text = "";
                    chkgroupname.Checked = false;
                    chkmfgcom.Checked = false;
                    chkproductname.Checked = false;
                    chkfirststock.Checked = false;
                    chkTaxPurchase.Checked = false;
                    transType = ""; PurchaseAcc1 = "";
                }
            }
            catch (Exception)
            { }
        }

        private void chkdiscount_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkdiscount.Checked == true)
                {
                    chksname.Checked = false;
                    cmbsname.Text = "Select";
                    chkporderno.Checked = false;
                    txtPOrderno.Text = "";
                    chkcashcredit.Checked = false;
                    fillcashcredit();
                    chktranscharge.Checked = false;
                    fillGroupname();
                    cmbcompany.Text = "Select";
                    txtproductname.Text = "";
                    chkgroupname.Checked = false;
                    chkmfgcom.Checked = false;
                    chkproductname.Checked = false;
                    chkfirststock.Checked = false;
                    chkTaxPurchase.Checked = false;
                    transType = ""; PurchaseAcc1 = "";
                }
            }
            catch (Exception)
            { }
        }

        private void frmPurchaseOrderList_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                frmPurchaseOrder.purorderinfo = null;
            }
            catch (Exception)
            { }
        }

        private void btntodayslist_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    dtpFromPorderdate.Value = DateTime.Now.Date;
                    dtpToPorderDate.Value = DateTime.Now.Date;
                }
                catch (Exception)
                { }
                txtPOrderno.Text = "";
                chkporderno.CheckState = 0;
                chkbetweendate.CheckState = 0;
                chksname.CheckState = 0;
                chkcashcredit.CheckState = 0;
                chktranscharge.CheckState = 0;
                fillcashcredit();
                fillsupplier();
                hide();
                chkdiscount.CheckState = 0;
                fillcombo();
                fillGroupname();
                chkgroupname.CheckState = 0;
                chkproductname.CheckState = 0;
                chkmfgcom.CheckState = 0;
                chkfirststock.CheckState = 0;
                chkTaxPurchase.CheckState = 0;
            }
            catch (Exception)
            { }
            try
            {
                hide();
                DateTime date = DateTime.Now.Date;
                SqlDataAdapter da = new SqlDataAdapter("SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.ManualPurRefNo,pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.Suppliername!='First Stock' and (pd.SGstPercent!=NULL or pd.SGstPercent!=0 or pd.CGstPercent!=NULL or pd.CGstPercent!=0 or pd.IGstPercent!=NULL or pd.IGstPercent!=0 ) and CONVERT(VARCHAR(24),pm.PorderDate,103)='" + date.ToString("dd/MM/yyyy") + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvPorderInfo.DataSource = dt;
                object sumObject;
                sumObject = dt.Compute("Sum(Total_Amount)", "");
                lbltoatdisamt.Visible = true;
                lbltoatdisamt.Text = "Total Purchase Amount:";
                disamt.Show();
                disamt.Text = Convert.ToString(sumObject);
                lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                GvPorderInfo.Refresh();
                GvPorderInfo.Columns["ID"].Visible = false;
                if (rbtnMultipleDeletebills.Enabled == false)
                    GvPorderInfo.Columns[0].Visible = false;
                else
                    GvPorderInfo.Columns[0].Visible = true;
                GvPorderInfo.Columns["ID"].Visible = false;
                GvPorderInfo.Columns["Purchase Order Number"].Width = 150;
                GvPorderInfo.Columns["Purchase Order Date"].Width = 150;
                GvPorderInfo.Columns["ManualPurRefNo"].Width = 150;
                GvPorderInfo.Columns["Supplier Name"].Width = 250;
                GvPorderInfo.Columns["Total_Amount"].Width = 155;
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void txtPOrderno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void cmbcashsredit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void chkdiscount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void dtpFromPorderdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void dtpToPorderDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void chktranscharge_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void cmbsname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void frmPurchaseOrderList_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                AutoCompleteProductName();
                AutoCompletePONo();
                AutoRefNo();
            }
            catch (Exception)
            { }
        }

        private void cmbgroupMaster_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void txtproductname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void cmbcompany_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
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
                    chksname.Checked = false;
                    cmbsname.Text = "Select";
                    chkporderno.Checked = false;
                    txtPOrderno.Text = "";
                    chkcashcredit.Checked = false;
                    fillcashcredit();
                    chktranscharge.Checked = false;
                    cmbcompany.Text = "Select";
                    txtproductname.Text = "";
                    chkdiscount.Checked = false;
                    chkmfgcom.Checked = false;
                    chkproductname.Checked = false;
                    chkfirststock.Checked = false;
                    chkTaxPurchase.Checked = false;
                    transType = ""; PurchaseAcc1 = "";
                }
            }
            catch (Exception)
            { }
        }

        private void chkproductname_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkproductname.Checked == true)
                {
                    chksname.Checked = false;
                    cmbsname.Text = "Select";
                    chkporderno.Checked = false;
                    txtPOrderno.Text = "";
                    chkcashcredit.Checked = false;
                    fillcashcredit();
                    chktranscharge.Checked = false;
                    fillGroupname();
                    cmbcompany.Text = "Select";
                    chkgroupname.Checked = false;
                    chkmfgcom.Checked = false;
                    chkdiscount.Checked = false;
                    chkfirststock.Checked = false;
                    chkTaxPurchase.Checked = false;
                    transType = "";
                    PurchaseAcc1 = "";
                }
            }
            catch (Exception)
            { }
        }

        private void chkmfgcom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkmfgcom.Checked == true)
                {
                    chksname.Checked = false;
                    cmbsname.Text = "Select";
                    chkporderno.Checked = false;
                    txtPOrderno.Text = "";
                    chkcashcredit.Checked = false;
                    fillcashcredit();
                    chktranscharge.Checked = false;
                    fillGroupname();
                    txtproductname.Text = "";
                    chkgroupname.Checked = false;
                    chkdiscount.Checked = false;
                    chkproductname.Checked = false;
                    chkfirststock.Checked = false;
                    chkTaxPurchase.Checked = false;
                    transType = ""; PurchaseAcc1 = "";
                }
            }
            catch (Exception)
            { }
        }

        private void chkfirststock_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkfirststock.Checked == true)
                {
                    chktranscharge.Checked = false;
                    chksname.Checked = false;
                    cmbsname.Text = "Select";
                    chkporderno.Checked = false;
                    txtPOrderno.Text = "";
                    chkcashcredit.Checked = false;
                    fillcashcredit();
                    chkdiscount.Checked = false;
                    fillGroupname();
                    cmbcompany.Text = "Select";
                    txtproductname.Text = "";
                    chkgroupname.Checked = false;
                    chkmfgcom.Checked = false;
                    chkproductname.Checked = false;
                    chkTaxPurchase.Checked = false;
                    transType = ""; PurchaseAcc1 = "";
                }
            }
            catch (Exception)
            { }
        }

        private void cmbsname_Leave(object sender, EventArgs e)
        {
            if (cmbsname.Text != "" && cmbsname.Text != "Select")
            {
                CommonMethod cm = new CommonMethod();
                if (cm.ValidSupplier(cmbsname.Text) == 1)
                { }
                else
                {
                    MessageBox.Show("Invalid Supplier.", "Warning");
                    fillsupplier();
                    this.ActiveControl = cmbsname;
                }
            }
        }

        private void cmbcompany_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbcompany.Text != "Select" && cmbcompany.Text != "")
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

        private void chkTaxPurchase_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkTaxPurchase.Checked == true)
                {
                    chktranscharge.Checked = false;
                    chkfirststock.Checked = false;
                    chksname.Checked = false;
                    cmbsname.Text = "Select";
                    chkporderno.Checked = false;
                    txtPOrderno.Text = "";
                    chkcashcredit.Checked = false;
                    fillcashcredit();
                    chkdiscount.Checked = false;
                    fillGroupname();
                    cmbcompany.Text = "Select";
                    txtproductname.Text = "";
                    chkgroupname.Checked = false;
                    chkmfgcom.Checked = false;
                    chkproductname.Checked = false;
                    transType = ""; PurchaseAcc1 = "";
                }
            }
            catch (Exception)
            { }
        }

        private void chkfirststock_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void chkTaxPurchase_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void chkmfgcom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcompany;
            }
            catch (Exception)
            { }
        }

        private void chkproductname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtproductname;
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

        private void chksname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbsname;
            }
            catch (Exception)
            { }
        }

        private void chkbetweendate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpFromPorderdate;
            }
            catch (Exception)
            { }
        }

        private void chkcashcredit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcashsredit;
            }
            catch (Exception)
            { }
        }

        private void chkporderno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtPOrderno;
            }
            catch (Exception)
            { }
        }

        public void TotalPLACToPurchase(string PurchaseAcc, DateTime fdate, DateTime tdate)
        {
            try
            {
                if (PurchaseAcc == "Scrap Purchases Battery")
                {
                    try
                    {
                        CommonMethod com = new CommonMethod();
                        string strsql = "";
                        if (CommonMethod.commProduct == true)
                            strsql = "SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.ManualPurRefNo,pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo inner join SupplierInformation as s on s.SupplierCode=pm.SupplierId where pm.Suppliername!='First Stock' and pm.PorderDate >= '" + com.ValidFromDate(fdate.Date) + "' and  pm.PorderDate <= '" + com.ValidToDate(tdate.Date) + "' and pm.TAXType='TaxPurchase' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "' and s.StateId=27";
                        else
                            strsql = "SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.ManualPurRefNo,pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo inner join SupplierInformation as s on s.SupplierCode=pm.SupplierId where pm.Suppliername!='First Stock' and pm.PorderDate >= '" + com.ValidFromDate(fdate.Date) + "' and  pm.PorderDate <= '" + com.ValidToDate(tdate.Date) + "' and pm.TAXType='TaxPurchase' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "' and s.StateId=27 and s.CompId='" + CommonMethod.CompId + "'";

                        SqlDataAdapter da = new SqlDataAdapter(strsql, db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvPorderInfo.DataSource = dt;
                        lbltoatdisamt.Visible = true;
                        lbltoatdisamt.Text = "Total Purchase Amount:";
                        disamt.Show();
                        disamt.Text = Convert.ToString(dt.Compute("Sum(Total_Amount)", ""));
                        GvPorderInfo.Refresh();
                        lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                        if (GvPorderInfo.Rows.Count != 0)
                        {
                            GvPorderInfo.Columns["ID"].Visible = false;
                            if (rbtnMultipleDeletebills.Enabled == false)
                                GvPorderInfo.Columns[0].Visible = false;
                            GvPorderInfo.Columns["ID"].Visible = false;
                            GvPorderInfo.Columns["Purchase Order Number"].Width = 150;
                            GvPorderInfo.Columns["Purchase Order Date"].Width = 150;
                            GvPorderInfo.Columns["ManualPurRefNo"].Width = 150;
                            GvPorderInfo.Columns["Supplier Name"].Width = 250;
                            GvPorderInfo.Columns["Total_Amount"].Width = 155;
                            this.ActiveControl = dtpFromPorderdate;
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                    catch (Exception)
                    { }
                }
                else if (PurchaseAcc == "OMS Purchase @")
                {
                    try
                    {
                        CommonMethod com = new CommonMethod();
                        string strsql = "";
                        if (CommonMethod.commProduct == true)
                            strsql = "SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.ManualPurRefNo,pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo inner join SupplierInformation as s on s.SupplierCode=pm.SupplierId where pm.Suppliername!='First Stock' and pm.PorderDate >= '" + com.ValidFromDate(fdate.Date) + "' and  pm.PorderDate <= '" + com.ValidToDate(tdate.Date) + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "' and s.StateId!=27";
                        else
                            strsql = "SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.ManualPurRefNo,pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo inner join SupplierInformation as s on s.SupplierCode=pm.SupplierId where pm.Suppliername!='First Stock' and pm.PorderDate >= '" + com.ValidFromDate(fdate.Date) + "' and  pm.PorderDate <= '" + com.ValidToDate(tdate.Date) + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "' and s.StateId!=27 and s.CompId='" + CommonMethod.CompId + "'";
                        SqlDataAdapter da = new SqlDataAdapter(strsql, db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvPorderInfo.DataSource = dt;
                        lbltoatdisamt.Text = "Total Purchase Amount:";
                        disamt.Show();
                        disamt.Text = Convert.ToString(dt.Compute("Sum(Total_Amount)", ""));
                        GvPorderInfo.Refresh();
                        lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                        if (GvPorderInfo.Rows.Count != 0)
                        {
                            GvPorderInfo.Columns["ID"].Visible = false;
                            GvPorderInfo.Columns["Purchase Order Number"].Width = 150;
                            GvPorderInfo.Columns["Purchase Order Date"].Width = 150;
                            GvPorderInfo.Columns["ManualPurRefNo"].Width = 150;
                            GvPorderInfo.Columns["Supplier Name"].Width = 250;
                            GvPorderInfo.Columns["Total_Amount"].Width = 155;
                            this.ActiveControl = dtpFromPorderdate;
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                    catch (Exception)
                    { }
                }
                else if (PurchaseAcc == "Purchase (URD)")
                {
                    try
                    {
                        CommonMethod com = new CommonMethod();
                        string strsql = "";
                        if (CommonMethod.commProduct == true)
                            strsql = "SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.ManualPurRefNo,pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo inner join SupplierInformation as s on s.SupplierCode=pm.SupplierId where pm.Suppliername!='First Stock' and pm.PorderDate >= '" + com.ValidFromDate(fdate.Date) + "' and  pm.PorderDate <= '" + com.ValidToDate(tdate.Date) + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "' and s.VatNo='' and pm.TAXType is null and s.StateId=27";
                        else
                            strsql = "SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.ManualPurRefNo,pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo inner join SupplierInformation as s on s.SupplierCode=pm.SupplierId where pm.Suppliername!='First Stock' and pm.PorderDate >= '" + com.ValidFromDate(fdate.Date) + "' and  pm.PorderDate <= '" + com.ValidToDate(tdate.Date) + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "' and s.VatNo='' and pm.TAXType is null and s.StateId=27 and s.CompId='" + CommonMethod.CompId + "'";
                        SqlDataAdapter da = new SqlDataAdapter(strsql, db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvPorderInfo.DataSource = dt;
                        lbltoatdisamt.Visible = true;
                        lbltoatdisamt.Text = "Total Purchase Amount:";
                        disamt.Show();
                        disamt.Text = Convert.ToString(dt.Compute("Sum(Total_Amount)", ""));
                        GvPorderInfo.Refresh();
                        lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                        if (GvPorderInfo.Rows.Count != 0)
                        {
                            GvPorderInfo.Columns["ID"].Visible = false;
                            GvPorderInfo.Columns["Purchase Order Number"].Width = 150;
                            GvPorderInfo.Columns["Purchase Order Date"].Width = 150;
                            GvPorderInfo.Columns["ManualPurRefNo"].Width = 150;
                            GvPorderInfo.Columns["Supplier Name"].Width = 250;
                            GvPorderInfo.Columns["Total_Amount"].Width = 155;
                            this.ActiveControl = dtpFromPorderdate;
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                    catch (Exception)
                    { }
                }
                else if (PurchaseAcc == "Purchase @ 0.00" || PurchaseAcc == "Purchase @ 5.00" || PurchaseAcc == "Purchase @ 12.00" || PurchaseAcc == "Purchase @ 18.00" || PurchaseAcc == "Purchase @ 28.00")
                {
                    try
                    {
                        CommonMethod com = new CommonMethod();
                        string strsql = "";
                        if (CommonMethod.commProduct == true)
                            strsql = "SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.ManualPurRefNo,pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo inner join SupplierInformation as s on s.SupplierCode=pm.SupplierId where pm.Suppliername!='First Stock' and pm.PorderDate >= '" + com.ValidFromDate(fdate.Date) + "' and  pm.PorderDate <= '" + com.ValidToDate(tdate.Date) + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "' and s.VatNo!='' and pm.TAXType is null and s.StateId=27";
                        else
                            strsql = "SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.ManualPurRefNo,pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo inner join SupplierInformation as s on s.SupplierCode=pm.SupplierId where pm.Suppliername!='First Stock' and pm.PorderDate >= '" + com.ValidFromDate(fdate.Date) + "' and  pm.PorderDate <= '" + com.ValidToDate(tdate.Date) + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "' and s.VatNo!='' and pm.TAXType is null and s.StateId=27 and s.CompId='" + CommonMethod.CompId + "'";
                        SqlDataAdapter da = new SqlDataAdapter(strsql, db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvPorderInfo.DataSource = dt;
                        lbltoatdisamt.Visible = true;
                        lbltoatdisamt.Text = "Total Purchase Amount:";
                        disamt.Show();
                        disamt.Text = Convert.ToString(dt.Compute("Sum(Total_Amount)", ""));
                        GvPorderInfo.Refresh();
                        lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                        if (GvPorderInfo.Rows.Count != 0)
                        {
                            GvPorderInfo.Columns["ID"].Visible = false;
                            GvPorderInfo.Columns["Purchase Order Number"].Width = 150;
                            GvPorderInfo.Columns["Purchase Order Date"].Width = 150;
                            GvPorderInfo.Columns["ManualPurRefNo"].Width = 150;
                            GvPorderInfo.Columns["Supplier Name"].Width = 250;
                            GvPorderInfo.Columns["Total_Amount"].Width = 155;
                            this.ActiveControl = dtpFromPorderdate;
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                    catch (Exception)
                    { }
                }
            }
            catch (Exception) { }
        }

        private void getProductType(string proname, string company, string unit)
        {
            ProductRepository prodrepo = new ProductRepository();
            ProductInformation productdata = new ProductInformation();
            if (CommonMethod.commProduct == true)
                productdata = prodrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
            else
                productdata = prodrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();

            if (productdata != null)
            {
                productentrytype = Convert.ToInt32(productdata.ProductType);  // allinone standard
                if (productdata.ProductType == 0 && productdata.WithBatchAndExpiry == "N" && productdata.WithBatch == "N")
                    productentrytype = 0;
                else if (productdata.ProductType == 0 && productdata.WithBatchAndExpiry == "Y")
                    productentrytype = 1;
                else if (productdata.ProductType == 0 && productdata.WithBatch == "Y") //krushi
                    productentrytype = 2;
                else if (productdata.ProductType == 3 && productdata.WithoutSize == "N")  //clothing
                    productentrytype = 3;
                else if (productdata.ProductType == 3 && productdata.WithoutSize == "Y")  //clothing
                    productentrytype = 4;
                else if (productdata.ProductType == 1) // serialized
                    productentrytype = 5;
                else if (productdata.ProductType == 2) // Assembly
                    productentrytype = 6;
                else if (productdata.ProductType == 4) //kit
                    productentrytype = 7;
                else if (productdata.ProductType == 5) //scrap
                    productentrytype = 8;
                else
                    productentrytype = 0;
            }
        }

        private void rbtnMultipleDeletebills_Click(object sender, EventArgs e)
        {
            try
            {
                billList.Clear();
                foreach (DataGridViewRow row in GvPorderInfo.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    if (chk.Value != null)
                    {
                        if ((bool)chk.Value == true)
                        {
                            if (billList.Contains(Convert.ToInt32(row.Cells[2].Value)))
                            { }
                            else
                                billList.Add(Convert.ToInt32(row.Cells[2].Value));
                        }
                    }
                }
                if (billList.Count != 0)
                    DeleteMultiplebill(billList);
                else
                    MessageBox.Show("Please select at least one bill!!");
                this.ActiveControl = chkporderno;
                Bindgrid();
            }
            catch (Exception) { }
        }

        public void DeleteMultiplebill(List<int> billList)
        {
            try
            {
                for (int q = 0; q < billList.Count; q++)
                {
                    AIOInventorySystem.Data.Model.Stock sdata = new Data.Model.Stock();
                    string productname = "", unit = "", company = "", alternateunit = "", CDType = "", Suppliername = "", DeletedBillNo = "";
                    decimal Quantity = 0, newsqty = 0, sqty = 0, pqty = 0, prqty = 0, newrqty = 0, rqty = 0, srqty = 0, dqty = 0, oqty = 0, freeqty = 0, NetAmount = 0, remRawQty = 0;
                    int cntProduct = 0, pno = 0, rawqty = 0;
                    string proname = "", unit1 = "", company1 = "", batchno = "", expirydate = "";
                    PurchaseMasterRepository purmastRepo = new PurchaseMasterRepository();
                    var purData = purmastRepo.GetAll().Where(t => t.Id == billList[q] && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    if (purData != null)
                    {
                        pno = Convert.ToInt32(purData.PorderNo);
                        Suppliername = Convert.ToString(purData.Suppliername);
                        SupplierRemainingPaymentRepository suppRemRepo = new SupplierRemainingPaymentRepository();
                        var suppremData = suppRemRepo.GetAll().Where(t => t.BillId == pno && t.Suppliername == Convert.ToString(Suppliername) && t.Type == "Receipt" && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();

                        var suppremData1 = suppRemRepo.GetAll().Where(t => t.BillId == pno && t.Suppliername == Convert.ToString(Suppliername) && t.Type == "Purchase" && t.PaidAmt != 0 && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && (t.PaymentMode == "Cash" || t.PaymentMode == "Cheque")).FirstOrDefault();

                        SuppCreditDebitNoteMasterRepository suppCDRepo = new SuppCreditDebitNoteMasterRepository();
                        var suppCDData = suppCDRepo.GetAll().Where(t => t.PorderNo == pno && t.Suppliername == Convert.ToString(Suppliername) && t.PorderDate.Value.ToShortDateString() == purData.PorderDate.Value.ToShortDateString() && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();

                        PReturnMasterRepository PurRetRepo = new PReturnMasterRepository();
                        var PRData = PurRetRepo.GetAll().Where(t => t.PorderNo == pno && t.Suppliername == Convert.ToString(Suppliername) && t.PorderDate.Value.ToShortDateString() == purData.PorderDate.Value.ToShortDateString() && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();

                        PurchaseDetailRepository detailRepo1 = new PurchaseDetailRepository();
                        List<PurchaseDetail> detailData1 = detailRepo1.GetAll().Where(t => t.PorderNo == billList[q] && (t.BatchNo != null || t.ExpireDate != null) && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).ToList();
                        if (detailData1.Count != 0)
                        {
                            foreach (var pdata in detailData1)
                            {
                                proname = pdata.ProductName;
                                unit1 = pdata.Unit;
                                company1 = pdata.CompanyName;
                                batchno = pdata.BatchNo;
                                if (pdata.ExpireDate != null)
                                    expirydate = Convert.ToString(pdata.ExpireDate.Value.ToShortDateString());
                                else
                                    expirydate = "";
                                getProductType(proname, company1, unit1);
                                if (productentrytype == 1 || productentrytype == 2)
                                {
                                    CustomerBillDetailRepository custDetailRepo = new CustomerBillDetailRepository();
                                    List<CustomerBillDetail> custdetailData = new List<CustomerBillDetail>();
                                    if (productentrytype == 1)
                                        custdetailData = custDetailRepo.GetAll().Where(t => t.ProductId == proname && t.Unit == unit1 && t.CompanyName == company1 && (t.BatchNo == batchno && t.Expiry == expirydate)).ToList();
                                    else
                                        custdetailData = custDetailRepo.GetAll().Where(t => t.ProductId == proname && t.Unit == unit1 && t.CompanyName == company1 && (t.BatchNo == batchno)).ToList();
                                    if (custdetailData.Count != 0)
                                    {
                                        foreach (var cdata in custdetailData)
                                        {
                                            cntProduct++;
                                        }
                                    }
                                }
                                else if (productentrytype == 6)
                                {
                                    CustomerBillDetailRepository custDetailRepo = new CustomerBillDetailRepository();
                                    List<CustomerBillDetail> custdetailData = new List<CustomerBillDetail>();
                                    if (productentrytype == 1)
                                        custdetailData = custDetailRepo.GetAll().Where(t => t.ProductId == proname && t.Unit == unit1 && t.CompanyName == company1).ToList();
                                    else
                                        custdetailData = custDetailRepo.GetAll().Where(t => t.ProductId == proname && t.Unit == unit1 && t.CompanyName == company1).ToList();
                                    if (custdetailData.Count != 0)
                                    {
                                        foreach (var cdata in custdetailData)
                                        {
                                            cntProduct++;
                                        }
                                    }
                                }
                                else
                                {
                                    DataTable dtAssembly = db.GetTable("Select * from tblAssemblyProductDetails where ProductName='" + proname + "' and CompanyName='" + company1 + "' and Unit='" + unit1 + "' and CompId=" + CommonMethod.CompId + "");
                                    if (dtAssembly.Rows.Count > 0)
                                    {
                                        //                                                  0                   1                 2                     3                          4                   
                                        DataTable dtStock = db.GetTable("Select isnull(PurchaseQty,0), isnull(SaleQty,0),isnull(PurReturnQty,0),isnull(SaleReturnQty,0),isnull(OpeningQty,0) from Stock where ProductName='" + proname + "' and MfgCompany='" + company1 + "' and Unit='" + unit1 + "' and CompId=" + CommonMethod.CompId + "");
                                        if (dtStock.Rows.Count > 0)
                                        {
                                            remRawQty = Convert.ToDecimal(dtStock.Rows[0][4].ToString()) + Convert.ToDecimal(dtStock.Rows[0][0].ToString()) - Convert.ToDecimal(dtStock.Rows[0][1].ToString()) - Convert.ToDecimal(dtStock.Rows[0][2].ToString()) + Convert.ToDecimal(dtStock.Rows[0][3].ToString()) - Convert.ToDecimal(pdata.Quantity);
                                            if (remRawQty < 0)
                                                rawqty = 1;
                                        }
                                    }
                                }
                            }
                        }
                        if (suppremData != null || suppremData1 != null || suppCDData != null || PRData != null || cntProduct != 0 || rawqty == 1)
                        {
                            if (suppremData != null || suppremData1 != null)
                                MessageBox.Show(+pno + " this Purchase Bill can't delete ," + Environment.NewLine + " because this bill is Paid.", "Warning");
                            else if (suppCDData != null)
                            {
                                CDType = suppCDData.CDType;
                                MessageBox.Show(+pno + " this Purchase Bill can't delete ," + Environment.NewLine + " because this bill is " + CDType + ".", "Warning");
                            }
                            else if (PRData != null)
                                MessageBox.Show(+pno + " this Purchase Bill can't delete ," + Environment.NewLine + " because this bill is purchase return.", "Warning");
                            else if (cntProduct != 0)
                                MessageBox.Show(+pno + " this Purchase Bill can't delete ," + Environment.NewLine + " because this bill is used in batch product in sale bill.", "Warning");
                            else if (rawqty == 1)
                                MessageBox.Show("You can't delete this Purchase Bill," + Environment.NewLine + " because this raw products used for " + Environment.NewLine + "assembly product in sale bill.", "Warning");
                        }
                        else
                        {
                            int purchaseId = purmastRepo.GetAll().Where(t => t.Id == billList[q] && t.PorderNo == pno && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().Id;
                            try
                            {
                                DeletedBillNo = Convert.ToString(purData.PorderNo);
                                Suppliername = Convert.ToString(purData.Suppliername);
                                NetAmount = Convert.ToDecimal(purData.NetAmount);

                                DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                                DeleteHistoryTable historydata = new DeleteHistoryTable();
                                historydata.DeleteType = "Purchase Bill";
                                historydata.BillNo = DeletedBillNo;
                                historydata.SupplierOrCustomerName = Suppliername;
                                historydata.BillDate = Convert.ToDateTime(purData.PorderDate);
                                historydata.CurrentDate = DateTime.Now.Date;
                                historydata.Amount = Convert.ToDecimal(NetAmount);
                                historydata.CompId = CommonMethod.CompId;
                                historydata.TransactionYear = CommonMethod.TransactionYear;
                                historyRepo.Add(historydata);
                                historyRepo.Save();
                            }
                            catch (Exception) { }

                            RecieptChallanMasterRepository receiptRepo = new RecieptChallanMasterRepository();
                            var receiptData = receiptRepo.GetAll().Where(t => t.POrderNo == pno && t.SupplierName == Convert.ToString(Suppliername) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                            if (receiptData != null)
                            {
                                int receiptId = receiptData.Id;
                                try
                                {
                                    DeletedBillNo = Convert.ToString(receiptData.RecieptChallanNo);
                                    Suppliername = Convert.ToString(receiptData.SupplierName);
                                    DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                                    DeleteHistoryTable historydata = new DeleteHistoryTable();
                                    historydata.DeleteType = "Receipt Challan";
                                    historydata.BillNo = DeletedBillNo;
                                    historydata.SupplierOrCustomerName = Suppliername;
                                    historydata.Amount = Convert.ToDecimal(0);
                                    historydata.BillDate = Convert.ToDateTime(receiptData.RecieptChallanDate);
                                    historydata.CurrentDate = DateTime.Now.Date;
                                    historydata.CompId = CommonMethod.CompId;
                                    historydata.TransactionYear = CommonMethod.TransactionYear;
                                    historyRepo.Add(historydata);
                                    historyRepo.Save();
                                }
                                catch (Exception) { }
                                DataTable dtRCM = db.GetTable("delete from RecieptChalanDetail where RecieptChallanNo='" + receiptId + "' ");
                                DataTable dtRCD = db.GetTable("delete from RecieptChallanMaster where Id='" + receiptId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "' ");
                                dtRCM.Dispose();
                                dtRCD.Dispose();
                            }
                            SupplierRemainingPaymentRepository SuppRemaningRepo = new SupplierRemainingPaymentRepository();
                            var suppremaingData = SuppRemaningRepo.GetAll().Where(t => t.BillId == pno && t.Suppliername == Convert.ToString(Suppliername) && t.Type == "Purchase" && t.PaidAmt == 0 && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                            if (suppremaingData != null)
                            {
                                decimal decremamt = 0;
                                int ReceiptId = SuppRemaningRepo.GetAll().Where(t => t.BillId == pno && t.Suppliername == Convert.ToString(Suppliername) && t.Type == "Purchase" && t.PaidAmt == 0 && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().Id;
                                SupplierRemainingPaymentRepository SupplierRemRepo = new SupplierRemainingPaymentRepository();
                                SupplierRemainingPayment SupplierRemdata = SupplierRemRepo.GetById(ReceiptId);
                                SupplierRemRepo.Remove(SupplierRemdata);
                                SupplierRemRepo.Save();

                                SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                                SupplierRemainingPayment remdata1 = SupplierRemRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(SupplierRemdata.SupplierId) && t.Id < ReceiptId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                if (remdata1 != null)
                                    decremamt = Convert.ToDecimal(remdata1.RemainingAmt);
                                else
                                    decremamt = 0;
                                SupplierRemainingPaymentRepository remainingpayrepo2 = new SupplierRemainingPaymentRepository();
                                List<SupplierRemainingPayment> remainingdata2 = new List<SupplierRemainingPayment>();
                                remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.SupplierId == Convert.ToInt32(SupplierRemdata.SupplierId) && t.Id > ReceiptId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();

                                int i = 0;
                                foreach (var item in remainingdata2)
                                {
                                    item.RemainingAmt = (Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt)) + decremamt;
                                    decremamt = (Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt)) + decremamt;
                                    remainingpayrepo2.Edit(item);
                                    remainingpayrepo2.Save();
                                    i++;
                                }
                                remainingpayrepo1.Dispose();
                                remainingpayrepo2.Dispose();
                            }
                            PurchaseDetailRepository PRdetailRepo = new PurchaseDetailRepository();
                            List<PurchaseDetail> detailList = PRdetailRepo.GetAll().Where(t => t.PorderNo == purchaseId).ToList();
                            if (detailList.Count != 0)
                            {
                                foreach (var item1 in detailList)
                                {
                                    int detailId = 0; decimal rate = 0; int updateStockId = 0; decimal repackQty = 0;
                                    productname = item1.ProductName;
                                    company = item1.CompanyName;
                                    unit = item1.Unit;
                                    alternateunit = item1.PurchaseUnit;
                                    detailId = item1.Id;
                                    Quantity = Convert.ToDecimal(item1.Quantity);
                                    freeqty = Convert.ToDecimal(item1.FreeQty);
                                    ProductRepository productRepo = new ProductRepository();
                                    ProductInformation proddata = new ProductInformation();
                                    if (CommonMethod.commProduct == true)
                                        proddata = productRepo.GetAll().Where(t => t.ProductName == productname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                                    else
                                        proddata = productRepo.GetAll().Where(t => t.ProductName == productname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();

                                    RepackDetailRepository repackDRepo = new RepackDetailRepository();
                                    repackQty = Convert.ToDecimal(repackDRepo.GetAll().Where(t => t.ProductNo == proddata.ID && t.RepackMaster.CompId == CommonMethod.CompId && t.RepackMaster.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.Qty));

                                    DataTable dt = db.GetTable("Delete from PurchaseDetail where PorderNo='" + purchaseId + "' and Id='" + detailId + "'");
                                    if (productentrytype == 9)
                                    { }
                                    else if (productentrytype == 6)
                                    { }
                                    else
                                    {
                                        StockRepository stockRepo = new StockRepository();
                                        var stockdata = stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                        if (stockdata != null)
                                        {
                                            newsqty = 0; sqty = 0; pqty = 0; prqty = 0; newrqty = 0; rqty = 0; srqty = 0; dqty = 0; oqty = 0;
                                            sqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().SaleQty);
                                            pqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().PurchaseQty);
                                            rqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().RemQty);
                                            oqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().OpeningQty);
                                            dqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().DeadQty);
                                            prqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().PurReturnQty);
                                            srqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().SaleReturnQty);
                                            newsqty = (pqty + repackQty) - Quantity - freeqty;
                                            newrqty = oqty + newsqty - prqty - sqty + srqty - dqty;
                                            updateStockId = stockdata.Id;
                                        }
                                        sdata = stockRepo.GetById(stockdata.Id);
                                        if (sdata != null)
                                        {
                                            sdata.ProductId = stockdata.ProductId;
                                            sdata.ProductName = productname;
                                            sdata.MfgCompany = company;
                                            sdata.Unit = unit;
                                            sdata.OpeningQty = stockdata.OpeningQty;
                                            sdata.DeadQty = stockdata.DeadQty;
                                            sdata.PurchaseQty = Convert.ToDecimal(newsqty);
                                            sdata.SaleQty = Convert.ToDecimal(sqty);
                                            sdata.RemQty = Convert.ToDecimal(newrqty);
                                            sdata.SaleReturnQty = stockdata.SaleReturnQty;
                                            sdata.PurReturnQty = stockdata.PurReturnQty;

                                            SettingRepository settRepo = new SettingRepository();
                                            Setting settData = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                                            try
                                            {
                                                db.connect();
                                                SqlCommand command = new SqlCommand();
                                                if (settData.StockOn == "Purchase Rate")
                                                    command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
                                                else if (settData.StockOn == "Sale Rate")
                                                    command = new SqlCommand("SPNewAvgSaleRate", db.Connection);
                                                command.CommandType = CommandType.StoredProcedure;

                                                command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = productname;
                                                command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = company;
                                                command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = unit;
                                                command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                                                command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = '0';
                                                command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                                SqlDataReader reader = command.ExecuteReader();
                                                DataTable dtRate = new DataTable();
                                                dtRate.Load(reader);
                                                rate = Convert.ToDecimal(dtRate.Rows[0][0].ToString());
                                                db.CloseConnection();
                                                dtRate.Dispose();
                                            }
                                            catch (Exception)
                                            { db.CloseConnection(); }

                                            sdata.ProductRate = Convert.ToDecimal(rate);
                                            sdata.CompId = CommonMethod.CompId;
                                            sdata.TransactionYear = CommonMethod.TransactionYear;
                                            sdata.Id = updateStockId;
                                            stockRepo.Edit(sdata);
                                            stockRepo.Save();
                                        }
                                        stockRepo.Dispose();
                                        dt.Dispose();
                                    }
                                }
                                PRdetailRepo.Dispose();
                                db.connect();
                                DataTable dt1 = db.GetTable("Delete from PurchaseMaster where Id='" + purchaseId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                                MessageBox.Show("Purchase Bill No. " + pno + " is deleted sucessfully.", "Success");
                                dt1.Dispose();
                                db.CloseConnection();
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtusername.Text != "" && txtpassword.Text != "")
                {
                    LoginRepository loginrepo = new LoginRepository();
                    AIOInventorySystem.Data.Model.Login itemdata = new Data.Model.Login();
                    itemdata = loginrepo.GetAll().Where(t => t.Username == txtusername.Text).FirstOrDefault();
                    if (itemdata != null)
                    {
                        SettingRepository setrepo = new SettingRepository();
                        Setting setdata = setrepo.GetAll().Where(t => t.FormPassword == txtpassword.Text).FirstOrDefault();
                        if (setdata != null)
                        {
                            pnlLogin.Visible = false;
                            txtpassword.Text = "";
                            txtusername.Text = "";
                            if (frmPurchaseOrder.purorderinfo == null)
                            {
                                frmPurchaseOrder purord = new frmPurchaseOrder(id);
                                purord.ShowDialog();
                                frmPurchaseOrder.purorderinfo = null;
                            }
                            else
                            {
                                this.Close();
                                frmPurchaseOrder.purorderinfo.LoadData(id);
                                frmPurchaseOrder.purorderinfo.Show();
                                frmPurchaseOrder.purorderinfo = null;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Password is not correct.", "Warning");
                            this.ActiveControl = txtpassword;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Username is not correct.", "Warning");
                        this.ActiveControl = txtusername;
                    }
                }
                else
                    MessageBox.Show("Enter UserName And Password.", "Warning");
            }
            catch (Exception)
            { }
        }

        private void txtManRefNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                pnlLogin.Visible = false;
                txtusername.Text = "";
                txtpassword.Text = "";
            }
            catch (Exception)
            { }
        }
    }
}