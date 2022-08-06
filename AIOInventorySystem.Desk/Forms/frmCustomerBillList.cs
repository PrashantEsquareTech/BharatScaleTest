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
    public partial class frmCustomerBillList : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        List<int> billList = new List<int>();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        private string callingtype = "", transType = "";
        DateTime fdate1, tdate1;
        bool userSettings = false;
        int productentrytype;

        public frmCustomerBillList(string calledlisttype = "")
        {
            callingtype = calledlisttype;
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            customerdata();
            fillMfgCompany();
            fillGroup();
            AutoCompleteBillNo();
            AutoCompleteProductName();
            fillcashcredit(); fillStaffname();
            hide();
            FillShippingParty();
            userSettings = false;
            SettingRepository setrepo = new SettingRepository();
            Setting setData = setrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (setData != null)
            {
                if (setData.ShowUserDefinedSeries == "Y")
                {
                    userSettings = true;
                    txtUserSeries.Enabled = true;
                    chkUserSeries.Enabled = true;
                    AutoCompleteUserSeries();
                }
                else
                {
                    txtUserSeries.Enabled = false;
                    chkUserSeries.Enabled = false;
                    userSettings = false;
                }
                if (setData.DeleteSaleBill == "True")
                    rbtnDeleteMultipleBill.Enabled = true;
                else
                    rbtnDeleteMultipleBill.Enabled = false;
            }
            setrepo.Dispose();
            Bindgrid();
            RootWise();
            this.ActiveControl = chkbillno;
            cmbCustomerType.SelectedIndex = cmbCustomerType.FindStringExact("Retailer");
        }

        public frmCustomerBillList(DateTime from, DateTime to, string transtype = "")
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            customerdata();
            fillMfgCompany();
            fillGroup();
            AutoCompleteBillNo();
            AutoCompleteProductName();
            fillcashcredit(); fillStaffname();
            FillShippingParty();
            hide();
            RootWise();
            fdate1 = from;
            tdate1 = to;
            transType = transtype;
            userSettings = false;
            SettingRepository setrepo = new SettingRepository();
            Setting setData = setrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (setData != null)
            {
                if (setData.ShowUserDefinedSeries == "Y")
                {
                    txtUserSeries.Enabled = true;
                    chkUserSeries.Enabled = true;
                    userSettings = true;
                    AutoCompleteUserSeries();
                }
                else
                {
                    txtUserSeries.Enabled = false;
                    chkUserSeries.Enabled = false;
                    userSettings = false;
                }
                if (setData.DeleteSaleBill == "True")
                    rbtnDeleteMultipleBill.Enabled = true;
                else
                    rbtnDeleteMultipleBill.Enabled = false;
            }
            setrepo.Dispose();
            Bindgrid(from, to, transtype);
            this.ActiveControl = chkbillno;
        }

        public frmCustomerBillList(string CustomerAcc, DateTime from, DateTime to)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            customerdata();
            fillMfgCompany();
            fillGroup();
            AutoCompleteBillNo();
            AutoCompleteProductName();
            fillcashcredit(); fillStaffname();
            hide();
            RootWise();
            userSettings = false; FillShippingParty();
            SettingRepository setrepo = new SettingRepository();
            Setting setData = setrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (setData != null)
            {
                if (setData.ShowUserDefinedSeries == "Y")
                {
                    txtUserSeries.Enabled = true;
                    chkUserSeries.Enabled = true;
                    userSettings = true;
                    AutoCompleteUserSeries();
                }
                else
                {
                    txtUserSeries.Enabled = false;
                    chkUserSeries.Enabled = false;
                    userSettings = false;
                }
                if (setData.DeleteSaleBill == "True")
                    rbtnDeleteMultipleBill.Enabled = true;
                else
                    rbtnDeleteMultipleBill.Enabled = false;
            }
            setrepo.Dispose();
            fdate1 = from;
            tdate1 = to;
            TotalPLACToCustBillList(CustomerAcc, from, to);
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

        public void FillShippingParty()
        {
            try
            {
                DataTable dt = db.GetTable("Select distinct c.CustomerID,c.CustomerName from CustomerInformation as c inner join CustomerBillMaster as cm on c.CustomerId=cm.ShipCustomerId where cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId=" + CommonMethod.CompId + " order by c.CustomerName");
                DataRow dr = dt.NewRow();
                dr["CustomerName"] = "Select";
                dr["CustomerID"] = 0;
                dt.Rows.InsertAt(dr, 0);
                cmbShippingParty.DataSource = dt;
                cmbShippingParty.DisplayMember = "CustomerName";
                cmbShippingParty.ValueMember = "CustomerID";
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
                    SqlCommand cmd = new SqlCommand("SELECT BillNo FROM CustomerBillMaster where BillNo like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' ", con);
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

        public void AutoCompleteUserSeries()
        {
            try
            {
                txtUserSeries.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = new SqlCommand("SELECT isnull(UserSeries,'') FROM CustomerBillMaster where BillNo like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' and (userseries is not null or userseries != '')", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        {
                            string[] strUserSeries = reader.GetString(0).Split('/', '-', ':');

                            MyCollection.Add(strUserSeries[0]);
                            MyCollection.Add(reader.GetString(0));
                        }
                        catch (Exception)
                        { }
                    }
                    txtUserSeries.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        public void RootWise()
        {
            try
            {
                VillageRepository ACGRepo = new VillageRepository();
                List<tblVillageInfo> ACGdata = new List<tblVillageInfo>();
                ACGdata.Clear();
                ACGdata = ACGRepo.GetAll().OrderByDescending(t => t.VillageName).ToList();
                ACGdata.Add(new tblVillageInfo { VillageName = "Select", VillageId = 0 });
                ACGdata.Reverse();
                cmbrootwise.DataSource = ACGdata;
                cmbrootwise.ValueMember = "VillageId";
                cmbrootwise.DisplayMember = "VillageName";
                ACGRepo.Dispose();
            }
            catch (Exception)
            {
            }
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
                MfgCompData = MfgCompRepo.GetAll().OrderByDescending(t => t.CompanyMarathi).ToList();
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
                    GroupData = GroupRepo.GetAll().OrderByDescending(t => t.GroupMasterName).ToList();
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

        public void fillStaffname()
        {
            try
            {
                StaffRepository StaffRepo = new StaffRepository();
                List<StaffInfo> Staffdata = new List<StaffInfo>();
                Staffdata.Clear();
                Staffdata = StaffRepo.GetAll().OrderByDescending(t => t.Name).ToList();
                Staffdata.Add(new StaffInfo { Name = "Select", ID = 0 });
                Staffdata.Reverse();
                cmbSalesman.DataSource = Staffdata;
                cmbSalesman.ValueMember = "ID";
                cmbSalesman.DisplayMember = "Name";
                StaffRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void Bindgrid(DateTime from, DateTime to, string transactiontype = "")
        {
            try
            {
                string strsql = "";

                if (transactiontype == "Cash")
                    strsql = "SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount],PaidAmount FROM [CustomerBillMaster] where Billdate>='" + cm.ValidFromDate(from.Date).ToString() + "' and Billdate<='" + cm.ValidToDate(to.Date).ToString() + "'  and BillNo like 'T%' and PaidAmount!=0 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " order by Billdate asc";
                else
                    strsql = "SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount],PaidAmount FROM [CustomerBillMaster] where Billdate>='" + cm.ValidFromDate(from.Date).ToString() + "' and Billdate<='" + cm.ValidToDate(to.Date).ToString() + "'  and BillNo like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " order by Billdate asc";

                SqlDataAdapter da = new SqlDataAdapter(strsql, db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvBillInfo.DataSource = dt;
                if (transactiontype == "Cash")
                {
                    object sumObjectDetail1;
                    sumObjectDetail1 = dt.Compute("Sum(PaidAmount)", "");
                    lblhamalichrgestext.Text = Convert.ToString(sumObjectDetail1);
                    lblhamalichrgestext.Visible = true;
                }

                lbltotalhamalichrges.Visible = true;
                lbltotalhamalichrges.Text = "Total Sale Amount:";
                lbltotalchargestext.Visible = true;
                lbltotalchargestext.Text = Convert.ToString(dt.Compute("Sum(NetAmount)", ""));
                lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                GvBillInfo.Refresh();
                if (GvBillInfo.Rows.Count != 0)
                {
                    GvBillInfo.Columns["Selectg"].Width = 50;
                    GvBillInfo.Columns["Updateg"].Width = 80;
                    GvBillInfo.Columns["Id"].Visible = false;
                    GvBillInfo.Columns["BillNo"].Width = 100;
                    GvBillInfo.Columns["Bill Date"].Width = 120;
                    GvBillInfo.Columns["CustomerName"].Width = 170;
                    GvBillInfo.Columns["TotalAmount"].Width = 100;
                    GvBillInfo.Columns["Discount"].Width = 70;
                    GvBillInfo.Columns["NetAmount"].Width = 100;
                    GvBillInfo.Columns["PaidAmount"].Width = 100;
                    this.ActiveControl = dtpfromdate;
                }
                else
                {
                    MessageBox.Show("Record not found,", "Warning");
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
                SqlDataAdapter da;
                if (callingtype == "G")
                    //--                               2      3                                     4                              5                                                    6            7              8             9           10          11           12                               13 
                    da = new SqlDataAdapter("SELECT  m.Id,m.BillNo,isnull(m.userseries,'') as 'User Series',m.QuotationNo as 'Quotation No',CONVERT(VARCHAR(24),m.Billdate,103) as 'Bill Date',m.BillRcptNo as 'BillPONo',m.CustomerName,m.TotalAmount,m.Discount,m.NetAmount,m.PaidAmount,CAST((m.TotalAmount*m.Discount/100) AS DECIMAL(18,2) ) AS DiscAmt,m.RemainingAmount as 'Bal. Amount' FROM CustomerBillMaster as m  where m.BillNo like 'T%' and CONVERT(VARCHAR(24),m.Billdate,103)='" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "'and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' order by Billdate asc", db.Connection);
                else
                    da = new SqlDataAdapter("SELECT  m.Id,m.BillNo,isnull(m.userseries,'') as 'User Series',m.QuotationNo as 'Quotation No',CONVERT(VARCHAR(24),m.Billdate,103) as 'Bill Date',m.BillRcptNo as 'BillPONo',m.CustomerName,m.TotalAmount,m.Discount,m.NetAmount,m.PaidAmount,CAST((m.TotalAmount*m.Discount/100) AS DECIMAL(18,2) ) AS DiscAmt,m.RemainingAmount as 'Bal. Amount' FROM CustomerBillMaster as m  where m.BillNo like 'T%' and CONVERT(VARCHAR(24),m.Billdate,103)='" + DateTime.Now.Date.ToString("dd/MM/yyyy") + "'and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "' order by Billdate asc", db.Connection);

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
                    if (rbtnDeleteMultipleBill.Enabled == false)
                        GvBillInfo.Columns["Selectg"].Visible = false;
                    else
                        GvBillInfo.Columns["Selectg"].Visible = true;
                    GvBillInfo.Columns["Selectg"].Width = 50;
                    GvBillInfo.Columns["Updateg"].Width = 80;
                    GvBillInfo.Columns["Id"].Visible = false;
                    GvBillInfo.Columns["BllNo"].Width = 80;
                    GvBillInfo.Columns["User Series"].Width = 90;
                    if (userSettings == true)
                    {
                        GvBillInfo.Columns["BillNo"].Visible = false;
                        GvBillInfo.Columns["User Series"].Visible = true;
                    }
                    else
                    {
                        GvBillInfo.Columns["BillNo"].Visible = true;
                        GvBillInfo.Columns["User Series"].Visible = false;
                    }
                    GvBillInfo.Columns["Quotation No"].Width = 90;
                    GvBillInfo.Columns["Bill Date"].Width = 90;
                    GvBillInfo.Columns["BillRcptNo"].Width = 65;
                    GvBillInfo.Columns["CustomerName"].Width = 90;
                    GvBillInfo.Columns["TotalAmount"].Width = 90;
                    GvBillInfo.Columns["Discount"].Width = 70;
                    GvBillInfo.Columns["NetAmount"].Width = 80;
                    GvBillInfo.Columns["PaidAmount"].Width = 80;
                    GvBillInfo.Columns["Bal. Amount"].Width = 80;
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
                    customerdata(); fillStaffname();
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
                    transType = "";
                    chkCustomerType.Checked = false;
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
                    chkquotation.Checked = false;
                    chkcustbillno.Checked = false;
                    chkBillRcptNo.Checked = false;
                    chkmfgcom.Checked = false;
                    chkgroupname.Checked = false;
                    Chkhamaliandtrancharges.Checked = false;
                    chktransportchrg.Checked = false;
                    chkhamalichr.Checked = false;
                    transType = "";
                }
            }
            catch (Exception)
            { }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            string strsql = "";
            bool isvalid = true;
            bool isSelected = false;

            try
            {
                hide();
                strsql = "SELECT m.Id,m.BillNo,isnull(m.userseries,'') as 'User Series',m.QuotationNo as 'Quotation No',CONVERT(VARCHAR(24),m.Billdate,103) as 'Bill Date',m.BillRcptNo as 'BillPONo',m.CustomerName,m.TotalAmount,m.Discount,m.NetAmount,m.PaidAmount,CAST((m.TotalAmount*m.Discount/100) AS DECIMAL(18,2) ) AS DiscAmt,m.RemainingAmount as 'Bal. Amount' FROM CustomerBillMaster as m join CustomerBillDetail as d on m.Id=d.BillNo inner join CustomerInformation as c on m.CustomerName=c.CustomerName where m.BillNo like 'T%' and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId=" + CommonMethod.CompId + "";//22-04-2021 Avanti
                if (chkquotation.Checked == true)
                {
                    callingtype = "Sq";
                    if (txtPOrderno.Text != "")
                    {
                        isSelected = true;
                        strsql = strsql + " and m.QuotationNo='" + txtPOrderno.Text + "'";
                    }
                    else
                    {
                        MessageBox.Show("Enter Quotation No.", "Warning");
                        this.ActiveControl = txtPOrderno;
                        isvalid = false;
                    }
                }

                if (chkbillno.Checked == true && isvalid == true)
                {
                    callingtype = "Sc";
                    if (cmbcustomername.Text != "Select")
                    {
                        isSelected = true;
                        strsql = strsql + " and m.CustomerId='" + cmbcustomername.SelectedValue + "'";
                    }
                    else
                    {
                        MessageBox.Show("Select customer name from list.", "Warning");
                        this.ActiveControl = cmbcustomername;
                        isvalid = false;
                    }
                }

                if (chkcashcredit.Checked == true && isvalid == true)
                {
                    if (cmbcashcredit.Text != "")
                    {
                        isSelected = true;
                        if (cmbcashcredit.Text == "Cash")
                        {
                            callingtype = "Sca";
                            strsql = strsql + " and m.RemainingAmount=0.00";
                            lblhamalichrges.Visible = true;
                            lblhamalichrges.Text = "Total Paid Amount:";
                            lblhamalichrgestext.Visible = true;
                        }
                        else
                        {
                            callingtype = "Scr";
                            strsql = strsql + " and m.RemainingAmount!=0.00 ";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select Mode", "Warning");
                        this.ActiveControl = cmbcashcredit;
                        isvalid = false;
                    }
                }
                if (chkbetweendate.Checked == true && isvalid == true)
                {
                    if (dtpfromdate.Value.Date <= dtptodate.Value.Date)
                    {
                        callingtype = "Sd";
                        isSelected = true;
                        strsql = strsql + " and m.Billdate>='" + cm.ValidFromDate(dtpfromdate.Value.Date).ToString() + "' and m.Billdate<='" + cm.ValidToDate(dtptodate.Value.Date).ToString() + "' ";//order by m.billno";
                    }
                    else
                    {
                        MessageBox.Show("To Date Should greater than or equal to from date.", "Warning");
                        this.ActiveControl = dtptodate;
                        isvalid = false;
                    }
                }
                if (chkcustbillno.Checked == true && isvalid == true)
                {
                    if (txtbillno.Text != "")
                    {
                        callingtype = "Sb";
                        isSelected = true;
                        strsql = strsql + " and m.BillNo='" + txtbillno.Text + "'";
                    }
                    else
                    {
                        MessageBox.Show("Enter Bill No.", "Warning");
                        this.ActiveControl = txtbillno;
                        isvalid = false;
                    }
                }

                if (chkBillRcptNo.Checked == true && isvalid == true)
                {
                    if (txtBillRcptNo.Text != "")
                    {
                        callingtype = "Sbr";
                        isSelected = true;
                        strsql = strsql + " and m.BillRcptNo='" + txtBillRcptNo.Text + "'";
                    }
                    else
                    {
                        MessageBox.Show("Enter Bill Receipt No.", "Warning");
                        this.ActiveControl = txtBillRcptNo;
                        isvalid = false;
                    }
                }

                if (chkproductname.Checked == true && isvalid == true)
                {
                    if (txtproductname.Text != "")
                    {
                        callingtype = "Sp";
                        isSelected = true;
                        string Proname1 = txtproductname.Text.Trim().Split(',')[0];
                        string company = txtproductname.Text.Trim().Split(',')[1];
                        string unit = txtproductname.Text.Trim().Split(',')[2];
                        string Proname = "";
                        if ((Proname1).Contains("'"))
                            Proname = Proname1.Replace("'", "''");
                        else
                            Proname = Proname1;
                        strsql = strsql + " and d.ProductId='" + Proname + "' and d.CompanyName='" + company + "' and d.Unit='" + unit + "'";
                    }
                    else
                    {
                        MessageBox.Show("Enter Product Name.", "Warning");
                        this.ActiveControl = txtproductname;
                        isvalid = false;
                    }
                }

                if (chkmfgcom.Checked == true && isvalid == true)
                {
                    if (cmbcompany.Text != "Select")
                    {
                        callingtype = "Scom";
                        isSelected = true;
                        strsql = strsql + " and d.CompanyName='" + cmbcompany.Text + "'";
                    }
                    else
                    {
                        MessageBox.Show("Enter Manufacturing Company.", "Warning");
                        this.ActiveControl = cmbcompany;
                        isvalid = false;
                    }
                }
                if (chkrootwise.Checked == true && isvalid == true)
                {
                    if (cmbrootwise.Text != "Select")
                    {
                        callingtype = "Sr";
                        isSelected = true;
                        strsql = strsql + " and c.RouteId='" + cmbrootwise.SelectedValue + "'";
                    }
                    else
                    {
                        MessageBox.Show("Enter Root.", "Warning");
                        this.ActiveControl = cmbcompany;
                        isvalid = false;
                    }
                }

                if (chkUserSeries.Checked == true)
                {
                    if (txtUserSeries.Text != "Select")
                    {
                        callingtype = "Su";
                        isSelected = true;
                        string strUserSeries = txtUserSeries.Text.Trim();
                        strsql = strsql + " and m.userseries='" + strUserSeries + "'";
                    }
                    else
                    {
                        MessageBox.Show("Enter User Series.", "Warning");
                        this.ActiveControl = txtUserSeries;
                        isvalid = false;
                    }
                }
                if (chkgroupname.Checked == true)
                {
                    if (cmbgroupMaster.SelectedIndex > 0)
                    {
                        callingtype = "SGp";
                        isSelected = true;
                        strsql = strsql + " and m.CategoryName='" + cmbgroupMaster.Text + "'";
                    }
                    else
                    {
                        MessageBox.Show("Select Company.", "Warning");
                        this.ActiveControl = cmbgroupMaster;
                    }
                }
                if (chkCustomerType.Checked == true)
                {
                    if (cmbCustomerType.Text != "")
                    {
                        callingtype = "Sb";
                        isSelected = true;
                        strsql = strsql + " and c.CustomerType='" + cmbCustomerType.Text + "'";
                    }
                    else
                    {
                        MessageBox.Show("Select Customer Type.", "Warning");
                        this.ActiveControl = cmbCustomerType;
                    }
                }
                if (chkShippingParty.Checked == true)
                {
                    if (cmbShippingParty.SelectedIndex > 0)
                    {
                        callingtype = "SSp";
                        isSelected = true;
                        strsql = strsql + " and m.ShipCustomerId='" + Convert.ToInt32(cmbShippingParty.SelectedValue) + "'";
                    }
                    else
                    {
                        MessageBox.Show("Select Shipping Party Name");
                        this.ActiveControl = cmbShippingParty;
                    }
                }
                if (chkDiscount.Checked == true)
                {
                    callingtype = "SD";
                    isSelected = true;
                    strsql = strsql + " and m.Discount!=0 ";
                }
                if (chkSalesman.Checked == true)
                {
                    if (cmbSalesman.SelectedIndex > 0)
                    {
                        callingtype = "SM";
                        isSelected = true;
                        strsql = strsql + " and m.StaffId=" + Convert.ToInt32(cmbSalesman.SelectedValue) + "";
                    }
                }
                if (strsql != "")
                    strsql = strsql + " GROUP BY m.Id,m.BillNo,m.QuotationNo ,m.Billdate ,m.BillRcptNo,m.CustomerName,m.TotalAmount,m.Discount,m.NetAmount,m.PaidAmount,m.RemainingAmount,m.userseries ";
                if (strsql != "")
                {
                    if (chkOrderBy.Checked)
                    {
                        if (cmbOrderBy.Text != "")
                        {
                            if (cmbOrderBy.Text == "BillNo")
                                strsql = strsql + "order by len(m.BillNo),m.BillNo";
                            else if (cmbOrderBy.Text == "BDate")
                                strsql = strsql + "order by Billdate";
                            else if (cmbOrderBy.Text == "CustomerName")
                                strsql = strsql + "order by m.CustomerName";
                        }
                        else
                            strsql = strsql + "order by Billdate";
                    }
                    else
                        strsql = strsql + "order by Billdate";

                    SqlDataAdapter daDetail = new SqlDataAdapter(strsql, db.Connection);
                    DataTable dtDetail = new DataTable();
                    daDetail.Fill(dtDetail);
                    GvBillInfo.DataSource = dtDetail;
                    lblhamalichrgestext.Text = Convert.ToString(dtDetail.Compute("Sum(PaidAmount)", ""));
                    lbltotalhamalichrges.Visible = true;
                    lbltotalhamalichrges.Text = "Total Sale Amount:";
                    lbltotalchargestext.Visible = true;
                    lbltotalchargestext.Text = Convert.ToString(dtDetail.Compute("Sum(NetAmount)", ""));
                    if (chkDiscount.Checked == true)
                    {
                        lbltranchar.Visible = true;
                        lbltranchar.Text = "Total Discount Amount:";
                        lbltranscharestext.Visible = true;
                        lbltranscharestext.Text = Convert.ToString(dtDetail.Compute("Sum(DiscAmt)", ""));
                    }
                    lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                    GvBillInfo.Refresh();
                }
                if (GvBillInfo.Rows.Count != 0)
                {
                    if (rbtnDeleteMultipleBill.Enabled == false)
                        GvBillInfo.Columns["Selectg"].Visible = false;
                    else
                        GvBillInfo.Columns["Selectg"].Visible = true;
                    GvBillInfo.Columns["Selectg"].Width = 50;
                    GvBillInfo.Columns["Updateg"].Width = 80;
                    GvBillInfo.Columns["Id"].Visible = false;
                    GvBillInfo.Columns["BillNo"].Width = 80;
                    GvBillInfo.Columns["User Series"].Width = 90;
                    if (userSettings == true)
                    {
                        GvBillInfo.Columns["BillNo"].Visible = false;
                        GvBillInfo.Columns["User Series"].Visible = true;
                    }
                    else
                    {
                        GvBillInfo.Columns["BillNo"].Visible = true;
                        GvBillInfo.Columns["User Series"].Visible = false;
                    }
                    GvBillInfo.Columns["Quotation No"].Width = 90;
                    GvBillInfo.Columns["Bill Date"].Width = 90;
                    GvBillInfo.Columns["BillPONo"].Width = 65;
                    GvBillInfo.Columns["CustomerName"].Width = 90;
                    GvBillInfo.Columns["TotalAmount"].Width = 90;
                    GvBillInfo.Columns["Discount"].Width = 70;
                    GvBillInfo.Columns["NetAmount"].Width = 80;
                    GvBillInfo.Columns["PaidAmount"].Width = 80;
                    GvBillInfo.Columns["Bal. Amount"].Width = 80;
                    if (chkDiscount.Checked == true)
                    {
                        GvBillInfo.Columns["DiscAmt"].Width = 70;
                        GvBillInfo.Columns["DiscAmt"].Visible = true;
                    }
                    this.ActiveControl = cmbcustomername;
                }
                if (Chkhamaliandtrancharges.Checked == true && isSelected == false)
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',BillRcptNo as 'BillPONo',[CustomerName],[TotalAmount],[Discount],[NetAmount],[TransportCharges],[HamaliCharges]  FROM [CustomerBillMaster] where HamaliCharges!=0 and TransportCharges!=0  and BillNo like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GvBillInfo.DataSource = dt;
                    lbltranchar.Visible = true;
                    lbltranscharestext.Visible = true;
                    lbltranscharestext.Text = Convert.ToString(dt.Compute("Sum(TransportCharges)", ""));
                    lblhamalichrges.Visible = true;
                    lblhamalichrgestext.Visible = true;
                    lblhamalichrgestext.Text = Convert.ToString(dt.Compute("Sum(HamaliCharges)", ""));
                    lbltotalhamalichrges.Visible = true;
                    lbltotalchargestext.Visible = true;
                    lbltotalhamalichrges.Text = "Total Hamali And Transport charges:";
                    lbltotalchargestext.Text = Convert.ToString(Convert.ToDecimal(lbltranscharestext.Text) + Convert.ToDecimal(lblhamalichrgestext.Text));
                    lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                    GvBillInfo.Refresh();
                    if (GvBillInfo.Rows.Count != 0)
                    {
                        if (rbtnDeleteMultipleBill.Enabled == false)
                            GvBillInfo.Columns["Selectg"].Visible = false;
                        else
                            GvBillInfo.Columns["Selectg"].Visible = true;
                        GvBillInfo.Columns["Selectg"].Width = 50;
                        GvBillInfo.Columns["Updateg"].Width = 80;
                        GvBillInfo.Columns["Id"].Visible = false;
                        GvBillInfo.Columns["BillNo"].Width = 80;
                        GvBillInfo.Columns["Bill Date"].Width = 90;
                        GvBillInfo.Columns["BillPONo"].Width = 65;
                        GvBillInfo.Columns["CustomerName"].Width = 90;
                        GvBillInfo.Columns["TotalAmount"].Width = 90;
                        GvBillInfo.Columns["Discount"].Width = 70;
                        GvBillInfo.Columns["NetAmount"].Width = 80;
                        GvBillInfo.Columns["TransportCharges"].Width = 80;
                        GvBillInfo.Columns["HamaliCharges"].Width = 80;
                        this.ActiveControl = Chkhamaliandtrancharges;
                    }
                    else
                    {
                        MessageBox.Show("Record not found.", "Warning");
                        this.ActiveControl = Chkhamaliandtrancharges;
                    }
                    da.Dispose();
                    dt.Dispose();
                }
                else if (chktransportchrg.Checked == true && isSelected == false)
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',BillRcptNo as 'BillPONo',[CustomerName],[TotalAmount],[Discount],[NetAmount],[TransportCharges] FROM [CustomerBillMaster] where TransportCharges!=0  and BillNo like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GvBillInfo.DataSource = dt;
                    lbltranchar.Visible = true;
                    lbltranscharestext.Visible = true;
                    lbltranscharestext.Text = Convert.ToString(dt.Compute("Sum(TransportCharges)", ""));
                    lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                    GvBillInfo.Refresh();
                    if (GvBillInfo.Rows.Count != 0)
                    {
                        if (rbtnDeleteMultipleBill.Enabled == false)
                            GvBillInfo.Columns["Selectg"].Visible = false;
                        else
                            GvBillInfo.Columns["Selectg"].Visible = true;
                        GvBillInfo.Columns["Selectg"].Width = 50;
                        GvBillInfo.Columns["Updateg"].Width = 80;
                        GvBillInfo.Columns["Id"].Visible = false;
                        GvBillInfo.Columns["BillNo"].Width = 50;
                        GvBillInfo.Columns["Bill Date"].Width = 50;
                        GvBillInfo.Columns["BillPONo"].Width = 50;
                        GvBillInfo.Columns["CustomerName"].Width = 100;
                        GvBillInfo.Columns["TotalAmount"].Width = 90;
                        GvBillInfo.Columns["Discount"].Width = 70;
                        GvBillInfo.Columns["NetAmount"].Width = 90;
                        GvBillInfo.Columns["TransportCharges"].Width = 90;
                        this.ActiveControl = chkhamalichr;
                    }
                    else
                    {
                        MessageBox.Show("Record not found.", "Warning");
                        this.ActiveControl = chkhamalichr;
                    }
                    da.Dispose();
                    dt.Dispose();
                }
                else if (chkhamalichr.Checked == true && isSelected == false)
                {
                    SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',BillRcptNo as 'BillPONo',[CustomerName],[TotalAmount],[Discount],[NetAmount] ,[HamaliCharges] FROM [CustomerBillMaster] where HamaliCharges!=0  and BillNo like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    GvBillInfo.DataSource = dt;
                    lblhamalichrges.Visible = true;
                    lblhamalichrgestext.Visible = true;
                    lblhamalichrgestext.Text = Convert.ToString(dt.Compute("Sum(HamaliCharges)", ""));
                    lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                    GvBillInfo.Refresh();
                    if (GvBillInfo.Rows.Count != 0)
                    {
                        if (rbtnDeleteMultipleBill.Enabled == false)
                            GvBillInfo.Columns["Selectg"].Visible = false;
                        else
                            GvBillInfo.Columns["Selectg"].Visible = true;
                        GvBillInfo.Columns["Selectg"].Width = 50;
                        GvBillInfo.Columns["Updateg"].Width = 80;
                        GvBillInfo.Columns["Id"].Visible = false;
                        GvBillInfo.Columns["BillNo"].Width = 50;
                        GvBillInfo.Columns["Bill Date"].Width = 50;
                        GvBillInfo.Columns["BillPONo"].Width = 50;
                        GvBillInfo.Columns["CustomerName"].Width = 100;
                        GvBillInfo.Columns["TotalAmount"].Width = 90;
                        GvBillInfo.Columns["Discount"].Width = 70;
                        GvBillInfo.Columns["NetAmount"].Width = 90;
                        GvBillInfo.Columns["HamaliCharges"].Width = 90;
                        this.ActiveControl = chkhamalichr;
                    }
                    else
                    {
                        MessageBox.Show("Record not found.", "Warning");
                        this.ActiveControl = chkhamalichr;
                    }
                    da.Dispose();
                    dt.Dispose();
                }
                else if (isSelected == false)
                {
                    MessageBox.Show("Please select at least one option.", "Warning");
                    this.ActiveControl = chkbillno;
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
                customerdata(); fillMfgCompany(); fillGroup(); fillStaffname();
                chkbillno.CheckState = 0;
                chkbetweendate.CheckState = 0;
                chkcashcredit.CheckState = 0;
                chkcustbillno.CheckState = 0;
                chkhamalichr.CheckState = 0; chkDiscount.CheckState = 0;
                Chkhamaliandtrancharges.CheckState = 0;
                chkquotation.CheckState = 0; chkShippingParty.Checked = false;
                FillShippingParty();
                fillcashcredit();
                hide();
                callingtype = "G";
            }
            catch (Exception)
            { }
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],isnull(UserSeries,'') as 'User Series',CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',BillRcptNo as 'BillPONo',[CustomerName],[TotalAmount],[Discount],[NetAmount] FROM [CustomerBillMaster] where  BillNo like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " order by Billdate asc", db.Connection);
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
                    if (rbtnDeleteMultipleBill.Enabled == false)
                        GvBillInfo.Columns["Selectg"].Visible = false;
                    else
                        GvBillInfo.Columns["Selectg"].Visible = true;
                    GvBillInfo.Columns["Selectg"].Width = 50;
                    GvBillInfo.Columns["Updateg"].Width = 80;
                    GvBillInfo.Columns["Id"].Visible = false;
                    GvBillInfo.Columns["BillNo"].Width = 80;
                    GvBillInfo.Columns["User Series"].Width = 90;
                    if (userSettings == true)
                    {
                        GvBillInfo.Columns["BillNo"].Visible = false;
                        GvBillInfo.Columns["User Series"].Visible = true;
                    }
                    else
                    {
                        GvBillInfo.Columns["BillNo"].Visible = true;
                        GvBillInfo.Columns["User Series"].Visible = false;
                    }
                    GvBillInfo.Columns["Bill Date"].Width = 90;
                    GvBillInfo.Columns["BillPONo"].Width = 65;
                    GvBillInfo.Columns["CustomerName"].Width = 90;
                    GvBillInfo.Columns["TotalAmount"].Width = 90;
                    GvBillInfo.Columns["Discount"].Width = 70;
                    GvBillInfo.Columns["NetAmount"].Width = 80;
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
                if (GvBillInfo.Columns[e.ColumnIndex].HeaderText == "Update")
                {
                    string billno = Convert.ToString(GvBillInfo.Rows[e.RowIndex].Cells["BillNo"].Value);
                    if (frmCustomerBill.custbillinfo == null)
                    {
                        frmCustomerBill custbill = new frmCustomerBill(billno, callingtype);
                        custbill.ShowDialog();
                        frmCustomerBill.custbillinfo = null;
                    }
                    else
                    {
                        this.Close();
                        frmCustomerBill.custbillinfo.billdata(billno);
                        frmCustomerBill.custbillinfo.Show();
                        frmCustomerBill.custbillinfo = null;
                    }
                }

            }
            catch (Exception)
            {
                frmCustomerBill.custbillinfo = null;
            }
        }

        private void GvBillInfo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (Char)Keys.Enter)
                {
                    if (GvBillInfo.CurrentCellAddress.X == 1)
                    {
                        string billno = Convert.ToString(GvBillInfo.Rows[GvBillInfo.CurrentCellAddress.Y].Cells["BillNo"].Value);
                        frmCustomerBill custbillform = new frmCustomerBill(billno);
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
                    chkquotation.Checked = false;
                    chkcustbillno.Checked = false;
                    chkBillRcptNo.Checked = false;
                    chkmfgcom.Checked = false;
                    chkgroupname.Checked = false;
                    Chkhamaliandtrancharges.Checked = false;
                    chktransportchrg.Checked = false;
                    chkhamalichr.Checked = false;
                    transType = "";
                    chkCustomerType.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                string mode = null, fromdt = "", todt = "", billreceiptno = "", productname = "", orderby = "", mfgcomp = "", custbillno = "", user_BillSeries = "", root = "" ;
                int custid = 0, SalesmanId = 0,rootid=0;
                RptAllCustomerBill vatobjectnew;
                callingtype = " ";
                int billtype = 1;
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
                if (chkUserSeries.Checked == true)
                    user_BillSeries = txtUserSeries.Text;
                if (chkrootwise.Checked == true)
                {
                    rootid = Convert.ToInt32(cmbrootwise.SelectedValue);
                    
                }
                if (transType == "Cash")
                {
                    fromdt = Convert.ToString(fdate1);
                    todt = Convert.ToString(tdate1);
                }
                else if (transType == "Day")
                {
                    fromdt = Convert.ToString(fdate1);
                    todt = Convert.ToString(tdate1);
                }
                if (chkSalesman.Checked == true)
                    SalesmanId = Convert.ToInt32(cmbSalesman.SelectedValue);
                if (cmbOrderBy.Text != "")
                    orderby = cmbOrderBy.Text;
                else
                    orderby = "BDate";

               
                vatobjectnew = new RptAllCustomerBill(billtype, fromdt, todt, mode, custid, billreceiptno, productname, mfgcomp, custbillno, transType, user_BillSeries, orderby, SalesmanId,rootid);
                vatobjectnew.Show();
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
                    transType = "";
                    chkCustomerType.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void chkhamalichar_CheckedChanged(object sender, EventArgs e)
        {
            transType = "";
        }

        private void chktranchar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkcustbillno.Checked == true)
                {
                    transType = "";
                    chkCustomerType.Checked = false;
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
                chkbillno.Checked = false;
                cmbcustomername.Text = "";
                chkbetweendate.CheckState = 0;
                chkcashcredit.CheckState = 0;
                chkcustbillno.CheckState = 0;
                chkhamalichr.CheckState = 0;
                Chkhamaliandtrancharges.CheckState = 0;
                chkquotation.CheckState = 0;
                txtPOrderno.Text = "";
                txtbillno.Text = "";
                txtBillRcptNo.Text = "";
                txtproductname.Text = "";
                cmbcompany.Text = "";
                cmbgroupMaster.Text = "";
                callingtype = "T";
                hide();
                DateTime currdate = DateTime.Now.Date;
                SqlDataAdapter da = new SqlDataAdapter("SELECT [Id],[BillNo],isnull(userseries,'') as 'User Series',CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',BillRcptNo as 'BillPONo',[CustomerName],[TotalAmount],[Discount],[NetAmount]  FROM [CustomerBillMaster] where BillNo like 'T%' and CONVERT(VARCHAR(24),Billdate,103)='" + currdate.ToString("dd/MM/yyyy") + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " order by Billdate asc", db.Connection);
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
                    if (rbtnDeleteMultipleBill.Enabled == false)
                        GvBillInfo.Columns["Selectg"].Visible = false;
                    else
                        GvBillInfo.Columns["Selectg"].Visible = true;
                    GvBillInfo.Columns["Selectg"].Width = 50;
                    GvBillInfo.Columns["Updateg"].Width = 80;
                    GvBillInfo.Columns["Id"].Visible = false;
                    GvBillInfo.Columns["BillNo"].Width = 80;
                    GvBillInfo.Columns["User Series"].Width = 90;
                    if (userSettings == true)
                    {
                        GvBillInfo.Columns["BillNo"].Visible = false;
                        GvBillInfo.Columns["User Series"].Visible = true;
                    }
                    else
                    {
                        GvBillInfo.Columns["BillNo"].Visible = true;
                        GvBillInfo.Columns["User Series"].Visible = false;
                    }
                    GvBillInfo.Columns["Bill Date"].Width = 90;
                    GvBillInfo.Columns["BillPONo"].Width = 65;
                    GvBillInfo.Columns["CustomerName"].Width = 90;
                    GvBillInfo.Columns["TotalAmount"].Width = 90;
                    GvBillInfo.Columns["Discount"].Width = 70;
                    GvBillInfo.Columns["NetAmount"].Width = 80;
                }
                da.Dispose();
                dt.Dispose();
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
                    transType = "";
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
            customerdata(); fillMfgCompany(); fillGroup(); fillStaffname();
            AutoCompleteBillNo(); AutoCompleteProductName();
        }

        private void chkBillRcptNo_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkBillRcptNo.Checked == true)
                {
                    transType = "";
                    chkCustomerType.Checked = false;
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
                    transType = "";
                    chkCustomerType.Checked = false;
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
                    transType = "";
            }
            catch (Exception)
            { }

        }

        private void chkmfgcom_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkmfgcom.Checked == true)
                    transType = "";
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
                    transType = "";
                    chkCustomerType.Checked = false;
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
                    transType = "";
                    chkCustomerType.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbcustomername_Leave(object sender, EventArgs e)
        {
            if (cmbcustomername.Text != "" && cmbcustomername.Text != "Select")
            {
                CommonMethod cm = new CommonMethod();
                if (Convert.ToInt32(cm.ValidCustomer(cmbcustomername.Text)) == 1)
                { }
                else
                {
                    MessageBox.Show("Invalid Customer.", "Warning");
                    customerdata();
                    this.ActiveControl = cmbcustomername;
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
                    var unitdat = unitrepo.GetAll().Where(t => t.CompanyName == cmbcompany.Text).FirstOrDefault();
                    if (unitdat == null)
                    {
                        MessageBox.Show("Invalid Company Name.", "Warning");
                        fillMfgCompany();
                        this.ActiveControl = cmbcompany;
                    }
                    unitrepo.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void chkbillno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcustomername;
            }
            catch (Exception)
            { }
        }

        private void cmbcustomername_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chkcashcredit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcashcredit;
            }
            catch (Exception)
            { }
        }

        private void cmbcashcredit_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chkquotation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtPOrderno;
            }
            catch (Exception)
            { }
        }

        private void chkbetweendate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpfromdate;
            }
            catch (Exception)
            { }
        }

        private void dtpfromdate_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void dtptodate_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void txtbillno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chkcustbillno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtbillno;
            }
            catch (Exception)
            { }
        }

        private void txtBillRcptNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chkBillRcptNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtBillRcptNo;
            }
            catch (Exception)
            { }
        }

        private void cmbrootwise_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chkrootwise_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbrootwise;
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

        private void txtproductname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
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

        private void cmbcompany_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void cmbgroupMaster_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
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

        private void Chkhamaliandtrancharges_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chktransportchrg_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chkhamalichr_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chkrootwise_CheckedChanged(object sender, EventArgs e)
        {
            transType = "";
        }

        private void chkUserSeries_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUserSeries.Checked == true)
            {
                txtUserSeries.Enabled = true;
                AutoCompleteUserSeries();
                chkCustomerType.Checked = false;
            }
            else
                txtUserSeries.Enabled = false;
        }

        public void TotalPLACToCustBillList(string CustomerAcc, DateTime fdate, DateTime tdate)
        {
            try
            {
                string strsql = "";
                strsql = "SELECT distinct m.Id,m.BillNo,isnull(m.userseries,'') as 'User Series',m.QuotationNo as 'Quotation No',CONVERT(VARCHAR(24),m.Billdate,101) as 'Bill Date',m.BillRcptNo as 'BillPONo',m.CustomerName,m.TotalAmount,m.Discount,m.NetAmount,m.PaidAmount,m.RemainingAmount as 'Bal. Amount' FROM CustomerBillMaster as m join CustomerBillDetail as d on m.Id=d.BillNo where m.Billdate>='" + cm.ValidFromDate(fdate.Date).ToString() + "' and m.Billdate<='" + cm.ValidToDate(tdate.Date).ToString() + "' and m.BillNo like 'T%' and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId=" + CommonMethod.CompId + "GROUP BY m.Id,m.BillNo,m.QuotationNo ,CONVERT(VARCHAR(24),m.Billdate,101) ,m.BillRcptNo,m.CustomerName,m.TotalAmount,m.Discount,m.NetAmount,m.PaidAmount,m.RemainingAmount,m.userseries order by CONVERT(VARCHAR(24),[Billdate ],101),m.Id";

                SqlDataAdapter da = new SqlDataAdapter(strsql, db.Connection);
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
                    if (rbtnDeleteMultipleBill.Enabled == false)
                        GvBillInfo.Columns["Selectg"].Visible = false;
                    else
                        GvBillInfo.Columns["Selectg"].Visible = true;
                    GvBillInfo.Columns["Selectg"].Width = 50;
                    GvBillInfo.Columns["Updateg"].Width = 80;
                    GvBillInfo.Columns["Id"].Visible = false;
                    GvBillInfo.Columns["BllNo"].Width = 80;
                    GvBillInfo.Columns["User Series"].Width = 90;
                    if (userSettings == true)
                    {
                        GvBillInfo.Columns["BillNo"].Visible = false;
                        GvBillInfo.Columns["User Series"].Visible = true;
                    }
                    else
                    {
                        GvBillInfo.Columns["BillNo"].Visible = true;
                        GvBillInfo.Columns["User Series"].Visible = false;
                    }
                    GvBillInfo.Columns["Quotation No"].Width = 90;
                    GvBillInfo.Columns["Bill Date"].Width = 90;
                    GvBillInfo.Columns["BillPONo"].Width = 65;
                    GvBillInfo.Columns["CustomerName"].Width = 90;
                    GvBillInfo.Columns["TotalAmount"].Width = 90;
                    GvBillInfo.Columns["Discount"].Width = 70;
                    GvBillInfo.Columns["NetAmount"].Width = 80;
                    GvBillInfo.Columns["PaidAmount"].Width = 80;
                    GvBillInfo.Columns["Bal. Amount"].Width = 80;
                }
                else
                {
                    MessageBox.Show("Record not found.", "Warning");
                    this.ActiveControl = dtpfromdate;
                }
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception) { }
        }

        private void rbtnDeleteMultipleBill_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in GvBillInfo.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    int id = Convert.ToInt32(row.Cells[2].Value);
                    if (chk.Value != null)
                    {
                        if ((bool)chk.Value == true)
                        {
                            if (billList.Contains(id))
                            { }
                            else
                                billList.Add(id);
                        }
                    }
                }
                if (billList.Count != 0)
                {
                    DeleteMultipleBill(billList);
                    MessageBox.Show("Record Deleted Sucessfully.", "Success");
                    this.ActiveControl = cmbcustomername;
                    Bindgrid();
                }
                else
                {
                    MessageBox.Show("Select at least one Invoice.", "Warning");
                    this.ActiveControl = GvBillInfo;
                }
            }
            catch (Exception) { }
        }

        public void DeleteMultipleBill(List<int> billList)
        {
            try
            {
                for (int q = 0; q < billList.Count; q++)
                {
                    AIOInventorySystem.Data.Model.Stock sdata = new Data.Model.Stock();
                    string productname = "", unit = "", company = "", alternateunit = "", CDType = "", custbillno = "", Suppliername = "", DeletedBillNo = "";
                    decimal Quantity = 0, newsqty = 0, sqty = 0, pqty = 0, prqty = 0, newrqty = 0, rqty = 0, srqty = 0, dqty = 0, oqty = 0, NetAmount = 0;
                    CustomerBillMasterRepository Custmasterrepo = new CustomerBillMasterRepository();
                    var custData = Custmasterrepo.GetAll().Where(t => t.Id == billList[q] && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    if (custData != null)
                    {
                        custbillno = custData.BillNo;
                        var custData1 = Custmasterrepo.GetAll().Where(t => t.BillNo == Convert.ToString(custbillno) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                        if (custData1 != null)
                        {
                            Suppliername = custData1.CustomerName;
                            RemainingPaymentRepository remainingRepo1 = new RemainingPaymentRepository();
                            var remainingData1 = remainingRepo1.GetAll().Where(t => t.BillId == Convert.ToString(custbillno) && t.Type == "Receipt" && t.Customername == Convert.ToString(Suppliername) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                            var remainingData3 = remainingRepo1.GetAll().Where(t => t.BillId == Convert.ToString(custbillno) && t.Customername == Convert.ToString(Suppliername) && t.Type == "Bill" && t.PaidAmt != 0 && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && (t.PaymentMode == "Cash" || t.PaymentMode == "Cheque")).FirstOrDefault();
                            CustCreditDebitNoteMasterRepository creditdebitRepo = new CustCreditDebitNoteMasterRepository();
                            var CDCustData = creditdebitRepo.GetAll().Where(t => t.BillNo == custbillno && t.CustomerName == Suppliername && t.Billdate.Value.ToShortDateString() == custData1.Billdate.Value.ToShortDateString() && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                            GoodMasterRepository saleretunRepo = new GoodMasterRepository();
                            var returnData = saleretunRepo.GetAll().Where(t => t.BillNo == custbillno && t.CustomerName == Suppliername && t.Billdate.Value.ToShortDateString() == custData1.Billdate.Value.ToShortDateString() && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                            if (remainingData1 != null || CDCustData != null || returnData != null || remainingData3 != null)
                            {
                                if (remainingData1 != null || remainingData3 != null)
                                    MessageBox.Show(custbillno + "this Invoice can't delete," + Environment.NewLine + " because this bill is Paid.", "Warning");
                                else if (CDCustData != null)
                                {
                                    CDType = CDCustData.CDType;
                                    MessageBox.Show(custbillno + "this Invoice can't delete," + Environment.NewLine + " because this bill is " + CDType + ".", "Warning");
                                }
                                else
                                    MessageBox.Show(custbillno + "this Invoice can't delete," + Environment.NewLine + " because this bill is sale return.", "Warning");
                            }
                            else
                            {
                                // add details for history table
                                try
                                {
                                    DeletedBillNo = Convert.ToString(custData1.BillNo);
                                    Suppliername = Convert.ToString(custData1.CustomerName);
                                    NetAmount = Convert.ToDecimal(custData1.NetAmount);
                                    DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                                    DeleteHistoryTable historydata = new DeleteHistoryTable();
                                    historydata.DeleteType = "Invoice";
                                    historydata.BillNo = DeletedBillNo;
                                    historydata.SupplierOrCustomerName = Suppliername;
                                    historydata.Amount = Convert.ToDecimal(NetAmount);
                                    historydata.CurrentDate = DateTime.Now.Date;
                                    historydata.BillDate = Convert.ToDateTime(custData1.Billdate);
                                    historydata.CompId = CommonMethod.CompId;
                                    historydata.TransactionYear = CommonMethod.TransactionYear;
                                    historyRepo.Add(historydata);
                                    historyRepo.Save();
                                }
                                catch (Exception) { }
                                int custId = Custmasterrepo.GetAll().Where(t => t.BillNo == Convert.ToString(custbillno) && t.CustomerName == Convert.ToString(Suppliername) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().Id;
                                int QuotationNo = 0, DCNo = 0;
                                if (custData.QuotationNo != "")
                                    QuotationNo = Convert.ToInt32(custData.QuotationNo);
                                else
                                    QuotationNo = 0;
                                if (custData.DcNo != null)
                                    DCNo = Convert.ToInt32(custData.DcNo);
                                else
                                    DCNo = 0;
                                if (QuotationNo != 0)
                                {
                                    QuotationMasterRepository QMastRepo = new QuotationMasterRepository();
                                    var QMdata = QMastRepo.GetAll().Where(t => t.QuotationNo == QuotationNo && t.CustomerName == Convert.ToString(Suppliername) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    if (QMdata != null)
                                    {
                                        QuotationMasterRepository QutMastRepo = new QuotationMasterRepository();
                                        QuotationMaster Qdata = QutMastRepo.GetById(QMdata.Id);
                                        Qdata.Status = 1;
                                        QutMastRepo.Edit(Qdata);
                                        QutMastRepo.Save();
                                        QutMastRepo.Dispose();
                                    }
                                    QMastRepo.Dispose();
                                }
                                if (DCNo != 0)
                                {
                                    DeliveryChallanRepository DCmastrepo = new DeliveryChallanRepository();
                                    var DCdata = DCmastrepo.GetAll().Where(t => t.DeliveryChallanNo == DCNo && t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    if (DCdata != null)
                                    {
                                        DeliveryChallanRepository DCRepo = new DeliveryChallanRepository();
                                        DeliveryChallanMaster DCMdata = DCRepo.GetById(DCdata.Id);
                                        DCMdata.Status = 1;
                                        DCRepo.Edit(DCMdata);
                                        DCRepo.Save();
                                        DCRepo.Dispose();
                                    }
                                    DCmastrepo.Dispose();
                                }
                                RemainingPaymentRepository remainingRepo = new RemainingPaymentRepository();
                                var remainingData = remainingRepo.GetAll().Where(t => t.BillId == Convert.ToString(custbillno) && t.Customername == Convert.ToString(Suppliername) && t.Type == "Bill" && t.PaidAmt == 0 && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (remainingData != null)
                                {
                                    int ReceiptId = remainingRepo.GetAll().Where(t => t.BillId == Convert.ToString(custbillno) && t.Customername == Convert.ToString(Suppliername) && t.Type == "Bill" && t.PaidAmt == 0 && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault().Id;
                                    RemainingPaymentRepository remRepo = new RemainingPaymentRepository();
                                    RemainingPayment remdata = remRepo.GetById(ReceiptId);
                                    remRepo.Remove(remdata);
                                    remRepo.Save();
                                    decimal decremamt = 0;
                                    RemainingPaymentRepository remainingpayrepo1 = new RemainingPaymentRepository();
                                    RemainingPayment remainingdata1 = new RemainingPayment();
                                    remainingdata1 = remainingpayrepo1.GetAll().Where(t => t.CustomerId == Convert.ToInt32(remdata.CustomerId) && t.Id < remdata.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                    if (remainingdata1 != null)
                                        decremamt = Convert.ToDecimal(remainingdata1.RemainingAmt);
                                    else
                                        decremamt = 0;
                                    RemainingPaymentRepository remainingpayrepo2 = new RemainingPaymentRepository();
                                    List<RemainingPayment> remainingdata2 = new List<RemainingPayment>();
                                    remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(remdata.CustomerId) && t.Id > remdata.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                    int i = 0;
                                    foreach (var item in remainingdata2)
                                    {
                                        decimal decnetpaidamt = 0;
                                        decnetpaidamt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt);
                                        item.RemainingAmt = decnetpaidamt + decremamt;
                                        decremamt = decnetpaidamt + decremamt;
                                        remainingpayrepo2.Edit(item);
                                        remainingpayrepo2.Save();
                                        i++;
                                    }
                                }
                                CustomerBillDetailRepository custDetRepo = new CustomerBillDetailRepository();
                                List<CustomerBillDetail> detailList = custDetRepo.GetAll().Where(t => t.BillNo == custId).ToList();
                                if (detailList.Count != 0)
                                {
                                    foreach (var item1 in detailList)
                                    {
                                        int detailId = 0; decimal rate = 0; int updateStockId = 0;
                                        string batchno = "", expirydate = "";
                                        decimal BUQty = 0, BPurQty = 0, FreeQty = 0, BSRQty = 0, BRQty = 0, BPRQty = 0, netQty = 0;
                                        productname = item1.ProductId;
                                        company = item1.CompanyName;
                                        unit = item1.Unit;
                                        alternateunit = item1.SaleUnit;
                                        detailId = item1.Id;
                                        batchno = item1.BatchNo;
                                        expirydate = item1.Expiry;
                                        Quantity = Convert.ToDecimal(item1.Quantity);
                                        ManufacturingUnitInfoRepository mfgunitRepo = new ManufacturingUnitInfoRepository();
                                        var mfgdata = mfgunitRepo.GetAll().Where(t => t.BillDetailId == detailId && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        if (mfgdata != null)
                                        {
                                            ManufacturingUnitInfo unitdata = mfgunitRepo.GetById(mfgdata.ID);
                                            mfgunitRepo.Remove(unitdata);
                                            mfgunitRepo.Save();
                                        }
                                        mfgunitRepo.Dispose();
                                        getProductType(productname, company, unit);
                                        if (productentrytype == 1 || productentrytype == 2)
                                        {
                                            string[] barray = batchno.Split(',');
                                            for (int m = 0; m < barray.Length; m++)
                                            {
                                                BatchRepository batchRepo = new BatchRepository();
                                                var batchdata = batchRepo.GetAll().Where(t => t.ProductName == productname && t.ManufactureCompany == company && t.Unit == unit && t.BillId == Convert.ToString(detailId) && t.MasterBillNo == custbillno && (t.BatchNo == barray[m]) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                                if (batchdata != null)
                                                {
                                                    PurchaseMasterRepository mastRepo1 = new PurchaseMasterRepository();
                                                    var purData1 = mastRepo1.GetAll().Where(t => t.Id == batchdata.PorderNo && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                                    if (purData1 != null)
                                                    {
                                                        PurchaseDetailRepository detailrepo1 = new PurchaseDetailRepository();
                                                        var purdetaildata1 = detailrepo1.GetAll().Where(t => t.PorderNo == purData1.Id && t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.BatchNo == barray[m] && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                                        if (purdetaildata1 != null)
                                                        {
                                                            PReturnDetailRepository PRdetailRepo = new PReturnDetailRepository();
                                                            var PRdetdata = PRdetailRepo.GetAll().Where(t => t.POID == purdetaildata1.Id && t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.BatchNo == barray[m] && t.PurchaseReturnMaster.CompId == CommonMethod.CompId && t.PurchaseReturnMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                                            if (PRdetdata != null)
                                                                BPRQty = Convert.ToDecimal(PRdetdata.Quantity);
                                                            else
                                                                BPRQty = 0;
                                                            BPurQty = Convert.ToDecimal(detailrepo1.GetAll().Where(t => t.PorderNo == purData1.Id && t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.BatchNo == barray[m] && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.Quantity));
                                                            FreeQty = Convert.ToDecimal(detailrepo1.GetAll().Where(t => t.PorderNo == purData1.Id && t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.BatchNo == barray[m] && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.FreeQty));
                                                            netQty = (BPurQty + FreeQty) - BPRQty;
                                                            try
                                                            {
                                                                GoodDetailRepository SRRepo = new GoodDetailRepository();
                                                                GoodReturnDetail SRData = new GoodReturnDetail();
                                                                SRData = SRRepo.GetAll().Where(t => t.BNID == detailId && t.ProductId == productname && t.CompanyName == company && t.Unit == unit && t.BatchNo == barray[m]).FirstOrDefault();
                                                                BSRQty = BSRQty + Convert.ToDecimal(SRData.Quantity);
                                                            }
                                                            catch (Exception)
                                                            { }

                                                            CustomerBillMasterRepository custMRepo = new CustomerBillMasterRepository();
                                                            var custMData = custMRepo.GetAll().Where(t => t.BillNo == Convert.ToString(custbillno) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();

                                                            CustomerBillDetailRepository custDRepo = new CustomerBillDetailRepository();
                                                            var custDetData = custDRepo.GetAll().Where(t => t.ProductId == productname && t.CompanyName == company && t.Unit == unit && (t.BatchNo == barray[m]) && t.CustomerBillMaster.CompId == CommonMethod.CompId && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                                            if (custDetData != null)
                                                                BUQty = Convert.ToDecimal(custDRepo.GetAll().Where(t => t.Id == Convert.ToInt32(detailId) && t.ProductId == productname && t.CompanyName == company && t.Unit == unit && (t.BatchNo == barray[m]) && t.CustomerBillMaster.CompId == CommonMethod.CompId && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.Quantity));
                                                            BRQty = netQty - (BUQty - BSRQty);
                                                            if (BRQty != 0)
                                                            {
                                                                int pdID = purdetaildata1.Id;
                                                                PurchaseDetail pdata1 = new PurchaseDetail();
                                                                pdata1 = detailrepo1.GetById(pdID);
                                                                pdata1.Status = "Active";
                                                                detailrepo1.Edit(pdata1);
                                                                detailrepo1.Save();
                                                            }
                                                            else
                                                            {
                                                                int pdID = purdetaildata1.Id;
                                                                PurchaseDetail pdata1 = new PurchaseDetail();
                                                                pdata1 = detailrepo1.GetById(pdID);
                                                                pdata1.Status = "Inactive";
                                                                detailrepo1.Edit(pdata1);
                                                                detailrepo1.Save();
                                                            }
                                                        }
                                                    }
                                                    Batch bdata = batchRepo.GetById(batchdata.ID);
                                                    batchRepo.Remove(bdata);
                                                    batchRepo.Save();
                                                }
                                                batchRepo.Dispose();
                                            }
                                        }
                                        DataTable dt = db.GetTable("Delete from CustomerBillDetail Where BillNo=" + custId + " and Id='" + detailId + "'");
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
                                                newsqty = sqty - Quantity;
                                                newrqty = oqty + pqty - prqty - newsqty + srqty - dqty;
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
                                                sdata.PurchaseQty = Convert.ToDecimal(pqty);
                                                sdata.SaleQty = Convert.ToDecimal(newsqty);
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
                                            dt.Dispose();
                                        }
                                    }
                                }
                                db.connect();
                                DataTable dt1 = db.GetTable("Delete from CustomerBillMaster Where Id=" + custId + " and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                                dt1.Dispose();
                                db.CloseConnection();
                            }
                        }
                    }
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

        private void chkCustomerType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCustomerType.Checked == true)
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
                fillGroup();
                Chkhamaliandtrancharges.Checked = false;
                chktransportchrg.Checked = false;
                chkhamalichr.Checked = false;
                transType = "";
            }
        }

        private void btnPrintMBills_Click(object sender, EventArgs e)
        {
            try
            {
                string flag = "WithVat";
                frmPrintMultipleNoVatBillList p = new frmPrintMultipleNoVatBillList(flag);
                p.ShowDialog();
            }
            catch (Exception)
            { }
        }
    }
}