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
    public partial class WithoutVatPurchaseOrderList : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        RptAllPurchase plist = new RptAllPurchase();
        public int id;

        public WithoutVatPurchaseOrderList()
        {
            InitializeComponent();
            fillsupplier();
            cm.changedatetimepickerrange(this);
            fillcashcredit();
            fillGroupname();
            fillcombo();
            hide();
            Bindgrid();
        }

        public WithoutVatPurchaseOrderList(DateTime from, DateTime to)
        {
            InitializeComponent();
            fillsupplier();
            cm.changedatetimepickerrange(this);
            fillcashcredit();
            fillGroupname();
            fillcombo();
            hide();
            Bindgrid(from, to);
            
        }

        public void Bindgrid(DateTime from, DateTime to)
        {
            try
            {
                CommonMethod com = new CommonMethod();
                SqlDataAdapter da = new SqlDataAdapter("SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.Suppliername!='First Stock' and pm.PorderDate >= '" + com.ValidFromDate(from.Date) + "' and  pm.PorderDate <= '" + com.ValidToDate(to.Date) + "' and pm.PaidAmount!=0 and (pm.TotalGst=0 or pm.TotalGst=null) and pm.TAXType is NULL  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "' and pm.PorderNo not in(Select POrderNo from RecieptChallanMaster r where r.TransactionYear='" + CommonMethod.TransactionYear + "' and r.CompId='" + CommonMethod.CompId + "' and Status=1)", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvPorderInfo.DataSource = dt;
                object sumObject;
                sumObject = dt.Compute("Sum(Total_Amount)", "");
                lbltoatdisamt.Visible = true;
                lbltoatdisamt.Text = "Total Purchase Amount:";
                disamt.Show();
                disamt.Text = Convert.ToString(sumObject);
                GvPorderInfo.Refresh();
                lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                if (GvPorderInfo.Rows.Count != 0)
                {
                    GvPorderInfo.Columns["ID"].Visible = false;
                    GvPorderInfo.Columns["Updateg"].Width = 70;
                    GvPorderInfo.Columns["Purchase Order Number"].Width = 150;
                    GvPorderInfo.Columns["Purchase Order Date"].Width = 150;
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

        public void btnexportshow()
        {
            //if (GvPorderInfo.Rows.Count > 0)
            //    btnexport.Enabled = true;
            //else
            //    btnexport.Enabled = false;
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
                SqlDataAdapter da = new SqlDataAdapter("SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.Suppliername!='First Stock' and CONVERT(VARCHAR(24),pm.PorderDate,103)='" + date.ToString("dd/MM/yyyy") + "' and (pm.TotalGst=0 or pm.TotalGst=null) and pm.TAXType is NULL  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'  and pm.PorderNo not in(Select POrderNo from RecieptChallanMaster r where r.TransactionYear='" + CommonMethod.TransactionYear + "' and r.CompId='" + CommonMethod.CompId + "' and Status=1)", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvPorderInfo.DataSource = dt;
                lbltoatdisamt.Visible = true;
                lbltoatdisamt.Text = "Total Purchase Amount:";
                disamt.Show();
                disamt.Text = Convert.ToString(dt.Compute("Sum(Total_Amount)", ""));
                lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                GvPorderInfo.Refresh();
                GvPorderInfo.Columns["ID"].Visible = false;
                GvPorderInfo.Columns["Updateg"].Width = 70;
                GvPorderInfo.Columns["Purchase Order Number"].Width = 150;
                GvPorderInfo.Columns["Purchase Order Date"].Width = 150;
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
                chkMRefNo.Checked = true;
                txtManRefNo.Text = "";
            }
            catch (Exception)
            { }
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.Suppliername!='First Stock'  and (pm.TotalGst=0 or pm.TotalGst is null) and pm.TAXType is NULL  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'  and pm.PorderNo not in(Select POrderNo from RecieptChallanMaster r where r.TransactionYear='" + CommonMethod.TransactionYear + "' and r.CompId='" + CommonMethod.CompId + "' and Status=1)", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvPorderInfo.DataSource = dt;
                lbltoatdisamt.Visible = true;
                lbltoatdisamt.Text = "Total Purchase Amount:";
                disamt.Show();
                disamt.Text = Convert.ToString(dt.Compute("Sum(Total_Amount)", ""));
                lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                GvPorderInfo.Refresh();
                GvPorderInfo.Columns["ID"].Visible = false;
                GvPorderInfo.Columns[2].Width = 150;
                GvPorderInfo.Columns[3].Width = 150;
                GvPorderInfo.Columns[4].Width = 250;
                GvPorderInfo.Columns[5].Width = 155;
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                hide();
                DataTable dt = new DataTable();
                object sumObject = 0;
                if (chkporderno.Checked == true)
                {
                    if (txtPOrderno.Text != "")
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.PorderNo='" + txtPOrderno.Text + "'  and (pm.TotalGst=0 or pm.TotalGst is null) and pm.TAXType is NULL  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'  and pm.PorderNo not in(Select POrderNo from RecieptChallanMaster r where r.TransactionYear='" + CommonMethod.TransactionYear + "' and r.CompId='" + CommonMethod.CompId + "' and Status=1)", db.Connection);
                        da.Fill(dt);
                        GvPorderInfo.DataSource = dt;
                        sumObject = dt.Compute("Sum(Total_Amount)", "");
                        lbltoatdisamt.Text = "Total Purchase Amount:";
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                        MessageBox.Show("Select purchase Order Number.");
                    this.ActiveControl = txtPOrderno;
                }
                else if (chkbetweendate.Checked == true)
                {
                    if (dtpFromPorderdate.Text != "" && dtpToPorderDate.Text != "")
                    {
                        if (dtpFromPorderdate.Value.Date < dtpToPorderDate.Value.Date)
                        {
                            if (chksname.Checked == true)
                            {
                                if (cmbsname.Text != "Select")
                                {
                                    CommonMethod com = new CommonMethod();
                                    string toDate = com.ValidToDate(dtpToPorderDate.Value.Date);
                                    string fromDate = com.ValidFromDate(dtpFromPorderdate.Value.Date);
                                    SqlDataAdapter da = new SqlDataAdapter("SELECT  distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.Suppliername!='First Stock' and pm.PorderDate >= '" + fromDate + "' and  pm.PorderDate <= '" + toDate + "' and pm.SupplierId='" + cmbsname.SelectedValue + "'  and (pm.TotalGst=0 or pm.TotalGst is null) and pm.TAXType is NULL  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'  and pm.PorderNo not in(Select POrderNo from RecieptChallanMaster r where r.TransactionYear='" + CommonMethod.TransactionYear + "' and r.CompId='" + CommonMethod.CompId + "' and Status=1)", db.Connection);
                                    da.Fill(dt);
                                    sumObject = dt.Compute("Sum(Total_Amount)", "");
                                    lbltoatdisamt.Text = "Total Purchase Amount:";
                                    da.Dispose();
                                    dt.Dispose();
                                }
                                else
                                    MessageBox.Show("Select Supplier Name.", "Warning");
                                this.ActiveControl = cmbsname;
                            }
                            else
                            {
                                CommonMethod com = new CommonMethod();
                                SqlDataAdapter da = new SqlDataAdapter("SELECT pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.Suppliername!='First Stock' and pm.PorderDate >= '" + com.ValidFromDate(dtpFromPorderdate.Value.Date) + "' and  pm.PorderDate <= '" + com.ValidToDate(dtpToPorderDate.Value.Date) + "'   and (pm.TotalGst=0 or pm.TotalGst is null) and pm.TAXType is NULL  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'  and pm.PorderNo not in(Select POrderNo from RecieptChallanMaster r where r.TransactionYear='" + CommonMethod.TransactionYear + "' and r.CompId='" + CommonMethod.CompId + "' and Status=1) group by pm.ID,pm.PorderNo,pm.PorderDate,pm.Suppliername,pm.NetAmount order by pm.PorderDate", db.Connection);
                                da.Fill(dt);
                                sumObject = dt.Compute("Sum(Total_Amount)", "");
                                lbltoatdisamt.Text = "Total Purchase Amount:";
                                da.Dispose();
                                dt.Dispose();
                                this.ActiveControl = dtpFromPorderdate;
                            }
                        }
                        else
                        {
                            MessageBox.Show("To Date Should greater than from date.", "Warning");
                            this.ActiveControl = dtpToPorderDate;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select purchase Order Dates.", "Warning");
                        this.ActiveControl = dtpFromPorderdate;
                    }
                }
                else if (chksname.Checked == true)
                {
                    if (cmbsname.Text != "Select")
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.SupplierId='" + cmbsname.SelectedValue + "'  and (pm.TotalGst=0 or pm.TotalGst is null) and pm.TAXType is NULL  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'  and pm.PorderNo not in(Select POrderNo from RecieptChallanMaster r where r.TransactionYear='" + CommonMethod.TransactionYear + "' and r.CompId='" + CommonMethod.CompId + "' and Status=1)", db.Connection);
                        da.Fill(dt);
                        sumObject = dt.Compute("Sum(Total_Amount)", "");
                        lbltoatdisamt.Text = "Total Purchase Amount:";
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                        MessageBox.Show("Select Supplier Name.", "Warning");
                    this.ActiveControl = cmbsname;
                }
                else if (chkcashcredit.Checked == true)
                {
                    if (cmbcashsredit.Text != "")
                    {
                        if (cmbcashsredit.Text == "Cash")
                        {
                            SqlDataAdapter da = new SqlDataAdapter("SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',pm.SupplierpoNo as 'Supplier PorderNo',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Net_Amount',pm.PaidAmount as 'Paid Amount' ,pm.RemainingAmount as 'Bal. Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.Suppliername!='First Stock' and pm.RemainingAmount='" + 0.00 + "'  and (pm.TotalGst=0 or pm.TotalGst is null) and pm.TAXType is NULL  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'  and pm.PorderNo not in(Select POrderNo from RecieptChallanMaster r where r.TransactionYear='" + CommonMethod.TransactionYear + "' and r.CompId='" + CommonMethod.CompId + "' and Status=1)", db.Connection);
                            da.Fill(dt);
                            sumObject = dt.Compute("Sum(Net_Amount)", "");
                            lbltoatdisamt.Text = "Total Purchase Amount:";
                            da.Dispose();
                            dt.Dispose();
                        }
                        else
                        {
                            SqlDataAdapter da = new SqlDataAdapter("SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',pm.SupplierpoNo as 'Supplier PorderNo',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Net_Amount',pm.PaidAmount as 'Paid Amount' ,pm.RemainingAmount as 'Bal. Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.Suppliername!='First Stock' and pm.RemainingAmount!='" + 0.00 + "'  and (pm.TotalGst=0 or pm.TotalGst is null) and pm.TAXType is NULL  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'  and pm.PorderNo not in(Select POrderNo from RecieptChallanMaster r where r.TransactionYear='" + CommonMethod.TransactionYear + "' and r.CompId='" + CommonMethod.CompId + "' and Status=1)", db.Connection);
                            da.Fill(dt);
                            sumObject = dt.Compute("Sum(Net_Amount)", "");
                            lbltoatdisamt.Text = "Total Purchase Amount:";
                            da.Dispose();
                            dt.Dispose();
                        }
                    }
                    else
                        MessageBox.Show("Select Cash Or credit option.", "Warning");
                    this.ActiveControl = cmbcashsredit;
                }
                else if (chktranscharge.Checked == true)
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount',pm.TransportCharges FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.TransportCharges!=0   and (pm.TotalGst=0 or pm.TotalGst is null) and pm.TAXType is NULL  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'  and pm.PorderNo not in(Select POrderNo from RecieptChallanMaster r where r.TransactionYear='" + CommonMethod.TransactionYear + "' and r.CompId='" + CommonMethod.CompId + "' and Status=1)", db.Connection);
                    da.Fill(dt);
                    sumObject = dt.Compute("Sum(TransportCharges)", "");
                    lblTotalAmount.Visible = true;
                    lbltotalremamt.Show();
                    lblTotalAmount.Text = Convert.ToString(sumObject);
                    sumObject = dt.Compute("Sum(Total_Amount)", "");
                    da.Dispose();
                    dt.Dispose();
                    this.ActiveControl = chktranscharge;
                }
                else if (chkdiscount.Checked == true)
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount',pm.Discount,cast((pm.TotalAmount*pm.Discount/100) as decimal(18,2)) as 'Dis_Amt' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.Discount!=0   and (pm.TotalGst=0 or pm.TotalGst is null) and pm.TAXType is NULL  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'  and pm.PorderNo not in(Select POrderNo from RecieptChallanMaster r where r.TransactionYear='" + CommonMethod.TransactionYear + "' and r.CompId='" + CommonMethod.CompId + "' and Status=1)", db.Connection);
                    da.Fill(dt);
                    sumObject = dt.Compute("Sum(Dis_Amt)", "");
                    lbltoatdisamt.Text = "Total discount Amount:";
                    da.Dispose();
                    dt.Dispose();
                    this.ActiveControl = chkdiscount;
                }
                else if (chkproductname.Checked == true)
                {
                    if (txtproductname.Text.Trim() != "")
                    {
                        string Proname1 = txtproductname.Text.Trim().Split(',')[0];
                        string company = txtproductname.Text.Trim().Split(',')[1];
                        string unit = txtproductname.Text.Trim().Split(',')[2];
                        string Proname = "";
                        if ((Proname1).Contains("'"))
                            Proname = Proname1.Replace("'", "''");
                        else
                            Proname = Proname1;
                        SqlDataAdapter da = new SqlDataAdapter("SELECT  distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo inner join ProductInformation as p on pd.ProductName=p.ProductName where pd.ProductName='" + Proname + "' and pd.CompanyName='" + company + "' and pd.Unit='" + unit + "'   and (pm.TotalGst=0 or pm.TotalGst is null) and pm.TAXType is NULL  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'  and pm.PorderNo not in(Select POrderNo from RecieptChallanMaster r where r.TransactionYear='" + CommonMethod.TransactionYear + "' and r.CompId='" + CommonMethod.CompId + "' and Status=1)", db.Connection);
                        da.Fill(dt);
                        sumObject = dt.Compute("Sum(Total_Amount)", "");
                        lbltoatdisamt.Text = "Total Purchase Amount:";
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                        MessageBox.Show("Enter Product Name.", "Warning");
                    this.ActiveControl = txtproductname;
                }
                else if (chkmfgcom.Checked == true)
                {
                    if (cmbcompany.SelectedIndex > 0)
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT  distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo inner join ProductInformation as p on pd.ProductName=p.ProductName where  pd.CompanyName='" + cmbcompany.Text + "'   and (pm.TotalGst=0 or pm.TotalGst is null) and pm.TAXType is NULL  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'  and pm.PorderNo not in(Select POrderNo from RecieptChallanMaster r where r.TransactionYear='" + CommonMethod.TransactionYear + "' and r.CompId='" + CommonMethod.CompId + "' and Status=1)", db.Connection);
                        da.Fill(dt);
                        sumObject = dt.Compute("Sum(Total_Amount)", "");
                        lbltoatdisamt.Text = "Total Purchase Amount:";
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                        MessageBox.Show("Select  Company Name.", "Warning");
                    this.ActiveControl = cmbcompany;
                }
                else if (chkgroupname.Checked == true)
                {
                    if (cmbgroupMaster.SelectedIndex > 0)
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT  distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo inner join ProductInformation as p on pd.ProductName=p.ProductName where  p.GroupMasterId='" + cmbgroupMaster.SelectedValue + "'   and (pm.TotalGst=0 or pm.TotalGst is null) and pm.TAXType is NULL and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'  and pm.PorderNo not in(Select POrderNo from RecieptChallanMaster r where r.TransactionYear='" + CommonMethod.TransactionYear + "' and r.CompId='" + CommonMethod.CompId + "' and Status=1)", db.Connection);
                        da.Fill(dt);
                        sumObject = dt.Compute("Sum(Total_Amount)", "");
                        lbltoatdisamt.Text = "Total Purchase Amount:";
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                        MessageBox.Show("Select Group Name.", "Warning");
                    this.ActiveControl = cmbgroupMaster;
                }
                else if (chkfirststock.Checked == true)
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT  distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where  pm.Suppliername='First Stock' and (pm.TotalGst=0 or pm.TotalGst is null) and pm.TAXType is NULL and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'  and pm.PorderNo not in(Select POrderNo from RecieptChallanMaster r where r.TransactionYear='" + CommonMethod.TransactionYear + "' and r.CompId='" + CommonMethod.CompId + "' and Status=1)", db.Connection);
                    da.Fill(dt);
                    sumObject = dt.Compute("Sum(Total_Amount)", "");
                    lbltoatdisamt.Text = "Total Purchase Amount:";
                    da.Dispose();
                    dt.Dispose();
                    this.ActiveControl = cmbsname;
                }
                else if (chkMRefNo.Checked == true)
                {
                    if (txtManRefNo.Text != "")
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT  distinct pm.ID,pm.PorderNo As 'Purchase Order Number',CONVERT(VARCHAR(24),pm.PorderDate,103) as 'Purchase Order Date',pm.Suppliername as 'Supplier Name',pm.NetAmount as 'Total_Amount' FROM PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo  where pm.ManualPurRefNo='" + txtManRefNo.Text + "'  and (pm.TotalGst=0 or pm.TotalGst is null) and pm.TAXType is NULL  and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId='" + CommonMethod.CompId + "'  and pm.PorderNo not in(Select POrderNo from RecieptChallanMaster r where r.TransactionYear='" + CommonMethod.TransactionYear + "' and r.CompId='" + CommonMethod.CompId + "' and Status=1)", db.Connection);
                        da.Fill(dt);
                        sumObject = dt.Compute("Sum(Total_Amount)", "");
                        lbltoatdisamt.Text = "Total Purchase Amount:";
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Enter Manual Pur Ref No.", "Warning");
                        this.ActiveControl = txtManRefNo;
                    }
                }
                GvPorderInfo.DataSource = dt;
                lbltoatdisamt.Visible = true;
                disamt.Show();
                disamt.Text = Convert.ToString(sumObject);
                GvPorderInfo.Refresh();
                lblTotalBill.Text = Convert.ToString(GvPorderInfo.Rows.Count);
                if (GvPorderInfo.Rows.Count != 0)
                {
                    GvPorderInfo.Columns["ID"].Visible = false;
                    GvPorderInfo.Columns["Updateg"].Width = 70;
                    GvPorderInfo.Columns["Purchase Order Number"].Width = 150;
                    GvPorderInfo.Columns["Purchase Order Date"].Width = 150;
                    GvPorderInfo.Columns["Supplier Name"].Width = 250;
                    GvPorderInfo.Columns["Total_Amount"].Width = 155;
                    try
                    {
                        GvPorderInfo.Columns["Supplier PorderNo"].Width = 100;
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        GvPorderInfo.Columns["TransportCharges"].Width = 90;
                        //lbltoatdisamt.Visible = false;
                        //disamt.Hide();
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        GvPorderInfo.Columns["Discount"].Width = 90;
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        GvPorderInfo.Columns["Dis_Amt"].Width = 90;
                    }
                    catch (Exception)
                    { }
                }
                else
                    MessageBox.Show("Record not found.", "Warning");
            }
            catch (Exception)
            { }
            
        }

        public void hide()
        {
            try
            {
                lbltotalremamt.Visible = false;
                lblTotalAmount.Visible = false;
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
                    chkporderno.Checked = false;
                    txtPOrderno.Text = "";
                    chkcashcredit.Checked = false;
                    cmbsname.Text = "Select";
                    cmbcashsredit.Text = "Select";
                    chktranscharge.Checked = false;
                    chkdiscount.Checked = false;
                    fillGroupname();
                    cmbcompany.Text = "Select";
                    txtproductname.Text = "";
                    chkgroupname.Checked = false;
                    chkmfgcom.Checked = false;
                    chkproductname.Checked = false;
                    chkfirststock.Checked = false;
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
                    chkbetweendate.Checked = false;
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
                if (e.ColumnIndex == 0)
                {
                    if (Convert.ToString(GvPorderInfo.Rows[e.RowIndex].Cells[4].Value) != "First stock")
                    {
                        SettingRepository sRepo = new SettingRepository();
                        Setting sdata = sRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
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
                    if (GvPorderInfo.CurrentCellAddress.X == 0)
                    {
                        frmPurchaseOrder pdata = new frmPurchaseOrder(Convert.ToInt32(GvPorderInfo.Rows[GvPorderInfo.CurrentCellAddress.Y].Cells["Id"].Value));
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
                    chkbetweendate.Checked = false;
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
                }
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkporderno.Checked == true && txtPOrderno.Text != "")
                {
                    RptWithoutVatAllPurchase plist = new RptWithoutVatAllPurchase(Convert.ToInt32(txtPOrderno.Text), null);
                    plist.Show();
                }
                else if (chkcashcredit.Checked == true && cmbcashsredit.Text != "Select")
                {
                    RptWithoutVatAllPurchase plist = new RptWithoutVatAllPurchase(0, cmbcashsredit.Text);
                    plist.Show();
                }
                else if (chkbetweendate.Checked == true && dtpFromPorderdate.Value.Date < dtpToPorderDate.Value.Date)
                {
                    RptWithoutVatAllPurchase plist = new RptWithoutVatAllPurchase(dtpFromPorderdate.Text, dtpToPorderDate.Text);
                    plist.Show();
                }
                else if (chksname.Checked == true && cmbsname.Text != "Select")
                {
                    RptWithoutVatAllPurchase plist = new RptWithoutVatAllPurchase(cmbsname.Text);
                    plist.Show();
                }
                else
                {
                    RptWithoutVatAllPurchase plist = new RptWithoutVatAllPurchase();
                    plist.Show();
                }
            }
            catch (Exception)
            { }
        }

        private void chktranscharge_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chktranscharge.Checked == true)
                {
                    chkbetweendate.Checked = false;
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
                    chkbetweendate.Checked = false;
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
                hide();
                Bindgrid(); 
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
                    chkbetweendate.Checked = false;
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
                    chkbetweendate.Checked = false;
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
                    chkbetweendate.Checked = false;
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
                    chkbetweendate.Checked = false;
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
                    chkdiscount.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void txtproductname_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtproductname.Text.Trim() != "")
                {
                    string proname = "", company = "", unit = "";
                    if ((txtproductname.Text).Contains(","))
                    {
                        try
                        {
                            proname = txtproductname.Text.Trim().Split(',')[0];
                            company = txtproductname.Text.Trim().Split(',')[1];
                            unit = txtproductname.Text.Trim().Split(',')[2];
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
                            txtproductname.Text = "";
                            this.ActiveControl = txtproductname;
                        }
                    }
                    else
                    {
                        MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                        txtproductname.Text = "";
                        this.ActiveControl = txtproductname;
                    }
                }
            }
            catch (Exception)
            { }
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