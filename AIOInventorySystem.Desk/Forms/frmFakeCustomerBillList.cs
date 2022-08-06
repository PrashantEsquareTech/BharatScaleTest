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
    public partial class frmFakeCustomerBillList : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public frmFakeCustomerBillList()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            customerdata();
            fillMfgCompany();
            fillGroup();
            AutoCompleteBillNo();
            AutoCompleteProductName();
            fillcashcredit();
            hide();
            Bindgrid();
            this.ActiveControl = chkbillno;
        }

        public frmFakeCustomerBillList(DateTime from, DateTime to)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            customerdata();
            fillMfgCompany();
            fillGroup();
            AutoCompleteBillNo();
            AutoCompleteProductName();
            fillcashcredit();
            hide();
            Bindgrid(from, to);
            this.ActiveControl = chkbillno;
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

        public void AutoCompleteBillNo()
        {
            try
            {
                txtbillno.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = new SqlCommand("SELECT BillNo FROM CustomerBillMaster where BillNo like 'F%' and BillStatus=0  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' ", con);
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
                    txtbillno.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
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
                cmbcustomername.DataSource = customerdata;
                cmbcustomername.ValueMember = "CustomerID";
                cmbcustomername.DisplayMember = "CustomerName";
                custrepo.Dispose();
                cmbcustomername.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void fillMfgCompany()
        {
            try
            {
                ManufactureCInfoRepository MfgCompRepo = new ManufactureCInfoRepository();
                List<ManuCompanyInfo> MfgCompData = new List<ManuCompanyInfo>();
                MfgCompData.Clear();
                if (CommonMethod.commProduct == true)
                    MfgCompData = MfgCompRepo.GetAll();
                else
                    MfgCompData = MfgCompRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CompanyName).ToList();
                MfgCompData.Add(new ManuCompanyInfo { CompanyName = "Select", CompanyId = 0 });
                MfgCompData.Reverse();
                cmbcompany.DataSource = MfgCompData;
                cmbcompany.ValueMember = "CompanyId";
                cmbcompany.DisplayMember = "CompanyName";
                MfgCompRepo.Dispose();
                cmbcompany.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void fillGroup()
        {
            try
            {
                GroupMasterRepository GroupRepo = new GroupMasterRepository();
                List<GroupMaster> GroupData = new List<GroupMaster>();
                GroupData.Clear();
                if (CommonMethod.commProduct == true)
                    GroupData = GroupRepo.GetAll().ToList();
                else
                    GroupData = GroupRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.GroupMasterName).ToList();
                GroupData.Add(new GroupMaster { GroupMasterName = "Select", GroupMasterId = 0 });
                GroupData.Reverse();
                cmbgroupMaster.DataSource = GroupData;
                cmbgroupMaster.ValueMember = "GroupMasterId";
                cmbgroupMaster.DisplayMember = "GroupMasterName";
                GroupRepo.Dispose();
                cmbgroupMaster.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void Bindgrid(DateTime from, DateTime to)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount]  FROM [CustomerBillMaster] where Billdate>='" + cm.ValidFromDate(from.Date) + "' and Billdate<='" + cm.ValidToDate(to.Date) + "'  and BillNo like 'F%' and BillStatus=0  and PaidAmount!=0 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvBillInfo.DataSource = dt;
                lbltotalhamalichrges.Visible = true;
                lbltotalhamalichrges.Text = "Total Sale Amount:";
                lbltotalchargestext.Visible = true;
                lbltotalchargestext.Text = Convert.ToString(dt.Compute("Sum(NetAmount)", ""));
                lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                GvBillInfo.Refresh();
                if (GvBillInfo.Rows.Count != 0)
                {
                    GvBillInfo.Columns["Id"].Visible = false;
                    GvBillInfo.Columns["Updateg"].Width = 80;
                    GvBillInfo.Columns["makeoriginal"].Width = 80;
                    GvBillInfo.Columns["BillNo"].Width = 80;
                    GvBillInfo.Columns["Bill Date"].Width = 120;
                    GvBillInfo.Columns["CustomerName"].Width = 170;
                    GvBillInfo.Columns["TotalAmount"].Width = 100;
                    GvBillInfo.Columns["Discount"].Width = 70;
                    GvBillInfo.Columns["NetAmount"].Width = 100;
                    this.ActiveControl = dtpfromdate;
                }
                else
                {
                    MessageBox.Show("Record not found.", "Warning");
                    this.ActiveControl = dtpfromdate;
                }
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        public void Bindgrid()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount]  FROM [CustomerBillMaster] where   CONVERT(VARCHAR(24),Billdate,101)=CONVERT(VARCHAR(24),GETDATE(),101) and BillNo like 'F%' and BillStatus=0  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvBillInfo.DataSource = dt;
                lbltotalhamalichrges.Visible = true;
                lbltotalhamalichrges.Text = "Total Sale Amount:";
                lbltotalchargestext.Visible = true;
                lbltotalchargestext.Text = Convert.ToString(dt.Compute("Sum(NetAmount)", ""));
                lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                GvBillInfo.Refresh();
                if (GvBillInfo.Rows.Count != 0)
                {
                    GvBillInfo.Columns["Id"].Visible = false;
                    GvBillInfo.Columns["Updateg"].Width = 80;
                    GvBillInfo.Columns["makeoriginal"].Width = 80;
                    GvBillInfo.Columns["BillNo"].Width = 80;
                    GvBillInfo.Columns["Bill Date"].Width = 120;
                    GvBillInfo.Columns["CustomerName"].Width = 170;
                    GvBillInfo.Columns["TotalAmount"].Width = 100;
                    GvBillInfo.Columns["Discount"].Width = 70;
                    GvBillInfo.Columns["NetAmount"].Width = 100;
                }
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void chkbillno_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkbillno.Checked == true)
                {
                    customerdata();
                    fillcashcredit();
                    chkcashcredit.Checked = false;
                    chkquotation.Checked = false;
                    chkbetweendate.Checked = false;
                    chkcustbillno.Checked = false;
                    chkBillRcptNo.Checked = false;
                    chkproductname.Checked = false;
                    chkmfgcom.Checked = false;
                    chkgroupname.Checked = false;
                    Chkhamaliandtrancharges.Checked = false;
                    chktransportchrg.Checked = false;
                    chkhamalichr.Checked = false;
                }
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
                    chkbillno.Checked = false;
                    fillcashcredit();
                    chkcashcredit.Checked = false;
                    chkquotation.Checked = false;
                    chkcustbillno.Checked = false;
                    chkBillRcptNo.Checked = false;
                    chkproductname.Checked = false;
                    chkmfgcom.Checked = false;
                    chkgroupname.Checked = false;
                    Chkhamaliandtrancharges.Checked = false;
                    chktransportchrg.Checked = false;
                    chkhamalichr.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                hide();
                DataTable dt = new DataTable();

                if (chkbillno.Checked == true)
                {
                    if (cmbcustomername.Text != "Select")
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [Id] ,[BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount]  FROM [CustomerBillMaster] where CustomerId='" + cmbcustomername.SelectedValue + "'  and BillNo like 'F%' and BillStatus=0  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        da.Fill(dt);
                        lbltotalhamalichrges.Text = "Total Sale Amount:";
                        lbltotalchargestext.Text = Convert.ToString(dt.Compute("Sum(NetAmount)", ""));
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Select Customer Name.", "Warning");
                        this.ActiveControl = cmbcustomername;
                    }
                }
                else if (chkcashcredit.Checked == true)
                {
                    if (cmbcashcredit.Text != "")
                    {
                        if (cmbcashcredit.Text == "Cash")
                        {
                            SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount] as 'Dis.',[NetAmount],[PaidAmount],[RemainingAmount] as 'Bal. Amount' FROM [CustomerBillMaster] where RemainingAmount='0.00'  and BillNo like 'F%' and BillStatus=0  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                            da.Fill(dt);
                            lbltotalhamalichrges.Text = "Total Sale Amount:";
                            lbltotalchargestext.Text = Convert.ToString(dt.Compute("Sum(NetAmount)", ""));
                            da.Dispose();
                            dt.Dispose();
                        }
                        else
                        {
                            SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount] as 'Dis.',[NetAmount],[PaidAmount],[RemainingAmount] as 'Bal. Amount' FROM [CustomerBillMaster] where RemainingAmount !='0.00'  and BillNo like 'F%' and BillStatus=0  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                            da.Fill(dt);
                            lbltotalhamalichrges.Text = "Total Sale Amount:";
                            lbltotalchargestext.Text = Convert.ToString(dt.Compute("Sum(NetAmount)", ""));
                            da.Dispose();
                            dt.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select Cash Credit First.", "Warning");
                        this.ActiveControl = cmbcashcredit;
                    }
                }
                else if (chkquotation.Checked == true)
                {
                    if (txtPOrderno.Text != "")
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [Id] ,[BillNo],QuotationNo as 'Quotation No',CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount]  FROM [CustomerBillMaster] where QuotationNo='" + txtPOrderno.Text + "'  and BillNo like 'F%' and BillStatus=0  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        da.Fill(dt);
                        lbltotalhamalichrges.Text = "Total Sale Amount:";
                        lbltotalchargestext.Text = Convert.ToString(dt.Compute("Sum(NetAmount)", ""));
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                        MessageBox.Show("Enter Quotation Number.", "Warning");
                    this.ActiveControl = txtPOrderno;
                }
                else if (chkbetweendate.Checked == true)
                {
                    if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount]  FROM [CustomerBillMaster] where Billdate>='" + cm.ValidFromDate(dtpfromdate.Value.Date) + "' and Billdate<='" + cm.ValidToDate(dtptodate.Value.Date) + "'  and BillNo like 'F%' and BillStatus=0  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        da.Fill(dt);
                        lbltotalhamalichrges.Text = "Total Sale Amount:";
                        lbltotalchargestext.Text = Convert.ToString(dt.Compute("Sum(NetAmount)", ""));
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("To Date Should greater than from date.", "Warning");
                        this.ActiveControl = dtptodate;
                    }
                }
                else if (chkcustbillno.Checked == true)
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT [Id] ,[BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount] FROM [CustomerBillMaster] where BillNo='" + txtbillno.Text + "' and BillNo like 'F%' and BillStatus=0  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                    da.Fill(dt);
                    lbltotalhamalichrges.Text = "Total Sale Amount:";
                    lbltotalchargestext.Text = Convert.ToString(dt.Compute("Sum(NetAmount)", ""));
                    da.Dispose();
                    dt.Dispose();
                }
                else if (chkBillRcptNo.Checked == true)
                {
                    if (txtBillRcptNo.Text != "")
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [Id] ,[BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount] FROM [CustomerBillMaster] where BillRcptNo='" + txtBillRcptNo.Text + "'  and  BillNo like 'F%' and BillStatus=0  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        da.Fill(dt);
                        lbltotalhamalichrges.Text = "Total Sale Amount:";
                        lbltotalchargestext.Text = Convert.ToString(dt.Compute("Sum(NetAmount)", ""));
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                        MessageBox.Show("Enter Bill Receipt Number.", "Warning");
                }
                else if (chkproductname.Checked == true)
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
                        SqlDataAdapter da = new SqlDataAdapter("SELECT m.[Id] ,m.[BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],m.[TotalAmount],m.[Discount],[NetAmount] FROM [CustomerBillMaster] as m inner join CustomerBillDetail as d on d.BillNo=m.Id where d.ProductId='" + Proname + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "'  and  m.BillNo like 'F%' and BillStatus=0  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        da.Fill(dt);
                        lbltotalhamalichrges.Text = "Total Sale Amount:";
                        lbltotalchargestext.Text = Convert.ToString(dt.Compute("Sum(NetAmount)", ""));
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                        MessageBox.Show("Enter Product Name.", "Warning");
                }
                else if (chkmfgcom.Checked == true)
                {
                    if (cmbcompany.Text != "Select")
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT m.[Id] ,m.[BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],m.[TotalAmount],m.[Discount],[NetAmount] FROM [CustomerBillMaster] as m inner join CustomerBillDetail as d on d.BillNo=m.Id where d.CompanyName='" + cmbcompany.Text + "'  and  m.BillNo like 'F%' and BillStatus=0  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        da.Fill(dt);
                        lbltotalhamalichrges.Text = "Total Sale Amount:";
                        lbltotalchargestext.Text = Convert.ToString(dt.Compute("Sum(NetAmount)", ""));
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                        MessageBox.Show("Select Mfg Company Name.", "Warning");
                }
                else if (chkgroupname.Checked == true)
                {
                    if (cmbgroupMaster.Text != "Select")
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT m.[Id] ,m.[BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],m.[TotalAmount],m.[Discount],[NetAmount] FROM [CustomerBillMaster] as m inner join CustomerBillDetail as d on d.BillNo=m.Id ,ProductInformation as p where p.GroupMasterId=" + cmbgroupMaster.SelectedValue + " and p.ProductName=d.ProductId and p.ManufactureCompany=d.CompanyName and p.Uniti=d.Unit and m.BillNo like 'F%' and BillStatus=0  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        da.Fill(dt);
                        lbltotalhamalichrges.Text = "Total Sale Amount:";
                        lbltotalchargestext.Text = Convert.ToString(dt.Compute("Sum(NetAmount)", ""));
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                        MessageBox.Show("Select Group Name.", "Warning");
                }
                else if (Chkhamaliandtrancharges.Checked == true)
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount],[TransportCharges],[HamaliCharges]  FROM [CustomerBillMaster] where HamaliCharges!=0 and TransportCharges!=0  and BillNo like 'F%' and BillStatus=0  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                    da.Fill(dt);
                    lbltranchar.Visible = true;
                    lbltranscharestext.Visible = true;
                    lbltranscharestext.Text = Convert.ToString(dt.Compute("Sum(TransportCharges)", ""));
                    lblhamalichrges.Visible = true;
                    lblhamalichrgestext.Visible = true;
                    lblhamalichrgestext.Text = Convert.ToString(dt.Compute("Sum(HamaliCharges)", ""));
                    lbltotalhamalichrges.Text = "Total Hamali And Transport charges:";
                    lbltotalchargestext.Text = Convert.ToString((Convert.ToDecimal(lbltranscharestext.Text) + Convert.ToDecimal(lblhamalichrgestext.Text)));
                    da.Dispose();
                    dt.Dispose();
                }
                else if (chktransportchrg.Checked == true)
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount] ,[TransportCharges] FROM [CustomerBillMaster] where TransportCharges!=0  and BillNo like 'F%' and BillStatus=0  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                    da.Fill(dt);
                    lbltranscharestext.Text = Convert.ToString(dt.Compute("Sum(TransportCharges)", ""));
                    da.Dispose();
                    dt.Dispose();
                }
                else if (chkhamalichr.Checked == true)
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount] ,[HamaliCharges] FROM [CustomerBillMaster] where HamaliCharges!=0  and BillNo like 'F%' and BillStatus=0  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                    da.Fill(dt);
                    lblhamalichrgestext.Text = Convert.ToString(dt.Compute("Sum(HamaliCharges)", ""));
                    GvBillInfo.Refresh();
                    da.Dispose();
                    dt.Dispose();
                }

                GvBillInfo.DataSource = dt;
                lbltotalhamalichrges.Visible = true;
                lbltotalchargestext.Visible = true;
                lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                GvBillInfo.Refresh();
                if (GvBillInfo.Rows.Count != 0)
                {
                    GvBillInfo.Columns["Id"].Visible = false;
                    GvBillInfo.Columns["Updateg"].Width = 80;
                    GvBillInfo.Columns["makeoriginal"].Width = 80;
                    GvBillInfo.Columns["BillNo"].Width = 80;
                    GvBillInfo.Columns["Bill Date"].Width = 120;
                    GvBillInfo.Columns["CustomerName"].Width = 170;
                    GvBillInfo.Columns["TotalAmount"].Width = 100;
                    GvBillInfo.Columns["Discount"].Width = 70;
                    GvBillInfo.Columns["NetAmount"].Width = 100;
                    try
                    {
                        GvBillInfo.Columns["HamaliCharges"].Width = 80;
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        GvBillInfo.Columns["TransportCharges"].Width = 80;
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        GvBillInfo.Columns["Quotation No"].Width = 70;
                    }
                    catch (Exception)
                    { }
                }
                else
                {
                    MessageBox.Show("Record not found.", "Warning");
                }
                db.CloseConnection();

            }
            catch (Exception)
            { }
        }

        private void btngetall_Click(object sender, EventArgs e)
        {
            try
            {
                dtpfromdate.Value = DateTime.Now.Date;
                dtptodate.Value = DateTime.Now.Date;
                customerdata(); fillMfgCompany(); fillGroup();
                chkbillno.CheckState = 0;
                chkbetweendate.CheckState = 0;
                chkcashcredit.CheckState = 0;
                chkcustbillno.CheckState = 0;
                chkhamalichr.CheckState = 0;
                Chkhamaliandtrancharges.CheckState = 0;
                chkquotation.CheckState = 0;
                fillcashcredit();
                hide();
            }
            catch (Exception)
            { }
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount]  FROM [CustomerBillMaster] where  BillNo like 'F%' and BillStatus=0  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvBillInfo.DataSource = dt;
                lbltotalhamalichrges.Visible = true;
                lbltotalhamalichrges.Text = "Total Sale Amount:";
                lbltotalchargestext.Visible = true;
                lbltotalchargestext.Text = Convert.ToString(dt.Compute("Sum(NetAmount)", ""));
                lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                GvBillInfo.Refresh();
                if (GvBillInfo.Rows.Count != 0)
                {
                    GvBillInfo.Columns["Id"].Visible = false;
                    GvBillInfo.Columns[3].Width = 100;
                    GvBillInfo.Columns[4].Width = 120;
                    GvBillInfo.Columns[5].Width = 170;
                    GvBillInfo.Columns[6].Width = 100;
                    GvBillInfo.Columns[7].Width = 70;
                    GvBillInfo.Columns[8].Width = 100;
                }
                da.Dispose();
                dt.Dispose();
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

        private void GvBillInfo_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (chkcompletebill.Checked == true)
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (frmCustomerBill.custbillinfo == null)
                        {
                            frmCustomerBill custbill = new frmCustomerBill(Convert.ToString(GvBillInfo.Rows[e.RowIndex].Cells[3].Value));
                            custbill.ShowDialog();
                            frmCustomerBill.custbillinfo = null;
                        }
                        else
                        {
                            this.Close();
                            frmCustomerBill.custbillinfo.billdata(Convert.ToString(GvBillInfo.Rows[e.RowIndex].Cells[3].Value));
                            frmCustomerBill.custbillinfo.Show();
                            frmCustomerBill.custbillinfo = null;
                        }
                    }
                }
                else
                {
                    if (e.ColumnIndex == 0)
                    {
                        if (frmCustomerBill.custbillinfo == null)
                        {
                            frmCustomerBill custbill = new frmCustomerBill(Convert.ToString(GvBillInfo.Rows[e.RowIndex].Cells[3].Value));
                            custbill.ShowDialog();
                            frmCustomerBill.custbillinfo = null;
                        }
                        else
                        {
                            this.Close();
                            frmCustomerBill.custbillinfo.billdata(Convert.ToString(GvBillInfo.Rows[e.RowIndex].Cells[3].Value));
                            frmCustomerBill.custbillinfo.Show();
                            frmCustomerBill.custbillinfo = null;
                        }
                    }
                    if (e.ColumnIndex == 1)
                    {
                        try
                        {
                        a:
                            {
                                string input = "";
                                input = Microsoft.VisualBasic.Interaction.InputBox("", "", "1", 500, 250);
                                if (input == "")
                                { }
                                else if (input == "1" || input == "2")
                                {
                                    frmCustomerBill srpt = new frmCustomerBill(Convert.ToInt32(input), "a", Convert.ToString(GvBillInfo.Rows[e.RowIndex].Cells[3].Value));
                                    srpt.WindowState = System.Windows.Forms.FormWindowState.Normal;
                                    srpt.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                    srpt.Show();
                                }
                                else
                                { goto a; }
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void GvBillInfo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (Char)Keys.Enter)
                {
                    if (GvBillInfo.CurrentCellAddress.X == 0)
                    {
                        frmCustomerBill custbillform = new frmCustomerBill(Convert.ToString(GvBillInfo.Rows[GvBillInfo.CurrentCellAddress.Y].Cells[2].Value));
                        custbillform.Show();
                    }
                }
            }
            catch (Exception)
            { }
        }

        public void fillcashcredit()
        {
            cmbcashcredit.Items.Clear();
            cmbcashcredit.Text = "Select";
            cmbcashcredit.Items.Add("Cash");
            cmbcashcredit.Items.Add("Credit");
        }

        private void chkcashcredit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkcashcredit.Checked == true)
                {
                    chkbillno.Checked = false;
                    chkquotation.Checked = false;
                    chkbetweendate.Checked = false;
                    chkcustbillno.Checked = false;
                    chkBillRcptNo.Checked = false;
                    chkproductname.Checked = false;
                    chkmfgcom.Checked = false;
                    chkgroupname.Checked = false;
                    Chkhamaliandtrancharges.Checked = false;
                    chktransportchrg.Checked = false;
                    chkhamalichr.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                RptAllCustomerBill vatobjectnew;
                int custid = 0;
                string mode = null, fromdt = "", todt = "", billreceiptno = "", productname = "", mfgcomp = "", custbillno = "";
                int billtype = 3;
                if (chkbetweendate.Checked == true && dtpfromdate.Value.Date <= dtptodate.Value.Date)
                {
                    fromdt = dtpfromdate.Text;
                    todt = dtptodate.Text;
                }
                if (chkcashcredit.Checked == true && cmbcashcredit.Text != "Select")
                    mode = cmbcashcredit.Text;
                if (chkbillno.Checked == true)
                    custid = Convert.ToInt32(cmbcustomername.SelectedValue);
                if (chkBillRcptNo.Checked == true && txtBillRcptNo.Text != "")
                    billreceiptno = txtBillRcptNo.Text;
                if (chkproductname.Checked == true)
                    productname = txtproductname.Text;
                if (chkmfgcom.Checked == true)
                    mfgcomp = cmbcompany.Text;
                if (chkcustbillno.Checked == true)
                    custbillno = txtbillno.Text;
                vatobjectnew = new RptAllCustomerBill(billtype, fromdt, todt, mode, custid, billreceiptno, productname, mfgcomp, custbillno);
                vatobjectnew.ShowDialog();
                vatobjectnew.Dispose();
            }
            catch (Exception)
            { }
        }

        private void Chkhamaliandtrancharges_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (Chkhamaliandtrancharges.Checked == true)
                {
                    chkbillno.Checked = false;
                    fillcashcredit();
                    chkcashcredit.Checked = false;
                    chkquotation.Checked = false;
                    chkbetweendate.Checked = false;
                    chkcustbillno.Checked = false;
                    chkBillRcptNo.Checked = false;
                    chkproductname.Checked = false;
                    chkmfgcom.Checked = false;
                    chkgroupname.Checked = false;
                    chktransportchrg.Checked = false;
                    chkhamalichr.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void chkhamalichar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkhamalichr.Checked == true)
                {
                    chkbillno.Checked = false;
                    fillcashcredit();
                    chkcashcredit.Checked = false;
                    chkquotation.Checked = false;
                    chkbetweendate.Checked = false;
                    chkcustbillno.Checked = false;
                    chkBillRcptNo.Checked = false;
                    chkproductname.Checked = false;
                    chkmfgcom.Checked = false;
                    chkgroupname.Checked = false;
                    Chkhamaliandtrancharges.Checked = false;
                    chktransportchrg.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void chktranchar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkcustbillno.Checked == true)
                {
                    chkbillno.Checked = false;
                    customerdata();
                    fillMfgCompany();
                    fillGroup();
                    fillcashcredit();
                    chkbetweendate.Checked = false;
                    chkcashcredit.Checked = false;
                    chkhamalichr.Checked = false;
                    Chkhamaliandtrancharges.Checked = false;
                    chkquotation.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        public void hide()
        {
            try
            {
                lblhamalichrges.Visible = false;
                lblhamalichrgestext.Visible = false;
                lbltranchar.Visible = false;
                lbltranscharestext.Visible = false;
                lbltotalhamalichrges.Visible = false;
                lbltotalchargestext.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void frmCustomerBillList_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                frmCustomerBill.custbillinfo = null;
            }
            catch (Exception)
            { }
        }

        private void btntodaylist_Click(object sender, EventArgs e)
        {
            try
            {
                hide();
                Bindgrid();
            }
            catch (Exception)
            { }
        }

        private void cmbcustomername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void cmbcashcredit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void Chkhamaliandtrancharges_KeyDown(object sender, KeyEventArgs e)
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

        private void dtptodate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chkhamalichar_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chktranchar_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chkquotation_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkquotation.Checked == true)
                {
                    chkbillno.Checked = false;
                    fillcashcredit();
                    chkcashcredit.Checked = false;
                    chkbetweendate.Checked = false;
                    chkcustbillno.Checked = false;
                    chkBillRcptNo.Checked = false;
                    chkproductname.Checked = false;
                    chkmfgcom.Checked = false;
                    chkgroupname.Checked = false;
                    Chkhamaliandtrancharges.Checked = false;
                    chktransportchrg.Checked = false;
                    chkhamalichr.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void txtPOrderno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void frmCustomerBillList_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            customerdata(); fillMfgCompany(); fillGroup();
            AutoCompleteBillNo(); AutoCompleteProductName();
        }

        private void chkBillRcptNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkBillRcptNo.Checked == true)
                {
                    chkbillno.Checked = false;
                    fillcashcredit();
                    chkcashcredit.Checked = false;
                    chkquotation.Checked = false;
                    chkbetweendate.Checked = false;
                    chkcustbillno.Checked = false;
                    chkproductname.Checked = false;
                    chkmfgcom.Checked = false;
                    chkgroupname.Checked = false;
                    Chkhamaliandtrancharges.Checked = false;
                    chktransportchrg.Checked = false;
                    chkhamalichr.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void chkcustbillno_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkcustbillno.Checked == true)
                {
                    chkbillno.Checked = false;
                    fillcashcredit();
                    chkcashcredit.Checked = false;
                    chkquotation.Checked = false;
                    chkbetweendate.Checked = false;
                    AutoCompleteBillNo();
                    chkBillRcptNo.Checked = false;
                    chkproductname.Checked = false;
                    chkmfgcom.Checked = false;
                    chkgroupname.Checked = false;
                    Chkhamaliandtrancharges.Checked = false;
                    chktransportchrg.Checked = false;
                    chkhamalichr.Checked = false;
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
                    chkbillno.Checked = false;
                    fillcashcredit();
                    chkcashcredit.Checked = false;
                    chkquotation.Checked = false;
                    chkbetweendate.Checked = false;
                    chkcustbillno.Checked = false;
                    chkBillRcptNo.Checked = false;
                    AutoCompleteProductName();
                    chkmfgcom.Checked = false;
                    chkgroupname.Checked = false;
                    Chkhamaliandtrancharges.Checked = false;
                    chktransportchrg.Checked = false;
                    chkhamalichr.Checked = false;
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
                    chkbillno.Checked = false;
                    fillcashcredit();
                    chkcashcredit.Checked = false;
                    chkquotation.Checked = false;
                    chkbetweendate.Checked = false;
                    chkcustbillno.Checked = false;
                    chkBillRcptNo.Checked = false;
                    chkproductname.Checked = false;
                    fillMfgCompany();
                    chkgroupname.Checked = false;
                    Chkhamaliandtrancharges.Checked = false;
                    chktransportchrg.Checked = false;
                    chkhamalichr.Checked = false;
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
                    chkbillno.Checked = false;
                    fillcashcredit();
                    chkcashcredit.Checked = false;
                    chkquotation.Checked = false;
                    chkbetweendate.Checked = false;
                    chkcustbillno.Checked = false;
                    chkBillRcptNo.Checked = false;
                    chkproductname.Checked = false;
                    chkmfgcom.Checked = false;
                    fillGroup();
                    Chkhamaliandtrancharges.Checked = false;
                    chktransportchrg.Checked = false;
                    chkhamalichr.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void chktransportchrg_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chktransportchrg.Checked == true)
                {
                    chkbillno.Checked = false;
                    fillcashcredit();
                    chkcashcredit.Checked = false;
                    chkquotation.Checked = false;
                    chkbetweendate.Checked = false;
                    chkcustbillno.Checked = false;
                    chkBillRcptNo.Checked = false;
                    chkproductname.Checked = false;
                    chkmfgcom.Checked = false;
                    chkgroupname.Checked = false;
                    Chkhamaliandtrancharges.Checked = false;
                    chkhamalichr.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void chkcompletebill_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkcompletebill.Checked == true)
                {
                    try
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount]  FROM [CustomerBillMaster] where   CONVERT(VARCHAR(24),Billdate,101)=CONVERT(VARCHAR(24),GETDATE(),101) and BillNo like 'F%' and BillStatus=1  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvBillInfo.DataSource = dt;
                        lbltotalhamalichrges.Visible = true;
                        lbltotalhamalichrges.Text = "Total Sale Amount:";
                        lbltotalchargestext.Visible = true;
                        lbltotalchargestext.Text = Convert.ToString(dt.Compute("Sum(NetAmount)", ""));
                        lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                        GvBillInfo.Refresh();

                        if (GvBillInfo.Rows.Count != 0)
                        {
                            GvBillInfo.Columns["Id"].Visible = false;
                            GvBillInfo.Columns[3].Width = 100;
                            GvBillInfo.Columns[4].Width = 120;
                            GvBillInfo.Columns[5].Width = 170;
                            GvBillInfo.Columns[6].Width = 100;
                            GvBillInfo.Columns[7].Width = 70;
                            GvBillInfo.Columns[8].Width = 100;
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                    catch (Exception)
                    { }
                }
                else
                    Bindgrid();
            }
            catch (Exception)
            { }
        }

        private void btnMultiPrint_Click(object sender, EventArgs e)
        {
            try
            {
                frmPrintMultipleNoVatBillList p = new frmPrintMultipleNoVatBillList("Fake");
                p.ShowDialog();
            }
            catch (Exception)
            { }
        }
    }
}