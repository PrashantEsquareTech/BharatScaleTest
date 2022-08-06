using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmCustomerLedger : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        DateTime currentdate = Convert.ToDateTime(DateTime.Now), fdate1, fdate2, tdate1, tdate2;
        public int customerid, receiptid, ACGId = 0;
        public string customername, billreceiptno, booktype1 = "", typeOfLedger = "";
        public decimal totalPaidAmount = 0;
        private bool userSeries = false;
        List<string> multipleReceiptPrintList = new List<string>();
        SettingRepository setrepo = new SettingRepository();
        Setting setdata = new Setting();

        public frmCustomerLedger()
        {
            typeOfLedger = "All";
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            Maxcustid();
            fillcombo(); fillStaffname();
            fillcombo1(); fillSalesman(); fillSiteName(); fillSiteNameS();
            cmbbank.Enabled = false;
            txtchequeno.Enabled = false;
            dtpchequedate.Enabled = false;
            txtbankname.Enabled = false;
            customerdata();
            customerdata1();
            hidelabel();
            setrepo = new SettingRepository();
            setdata = setrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (setdata != null)
            {
                if (setdata.ShowUserDefinedSeries == "Y")
                    userSeries = true;
                else
                    userSeries = false;
            }
        }

        public frmCustomerLedger(string separateLedger)
        {
            typeOfLedger = separateLedger;
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            Maxcustid();
            fillcombo(); fillStaffname();
            fillcombo1(); fillSalesman(); fillSiteName(); fillSiteNameS();
            cmbbank.Enabled = false;
            txtchequeno.Enabled = false;
            dtpchequedate.Enabled = false;
            txtbankname.Enabled = false;
            customerdata();
            customerdata1();
            hidelabel();
            setrepo = new SettingRepository();
            setdata = setrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (setdata != null)
            {
                if (setdata.ShowUserDefinedSeries == "Y")
                    userSeries = true;
                else
                    userSeries = false;
            }
        }

        public frmCustomerLedger(int custid)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            Maxcustid();
            fillcombo(); fillStaffname();
            fillcombo1(); fillSalesman(); fillSiteName(); fillSiteNameS();
            cmbbank.Enabled = false;
            txtchequeno.Enabled = false;
            dtpreceiptDate.Enabled = false;
            dtpchequedate.Enabled = false;
            txtbankname.Enabled = false;
            customerdata();
            customerdata1();
            this.ActiveControl = cmbcustomer;
            hidelabel();
            setrepo = new SettingRepository();
            setdata = setrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (setdata != null)
            {
                if (setdata.ShowUserDefinedSeries == "Y")
                    userSeries = true;
                else
                    userSeries = false;
            }
            typeOfLedger = "All";
            chkcustomername1.Checked = true;
            cmbcustomername1.SelectedValue = custid;
            loaddata(custid);
        }

        public frmCustomerLedger(DateTime from, DateTime to, string booktype)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            Maxcustid();
            fillcombo(); fillStaffname();
            fillcombo1(); fillSalesman(); fillSiteName(); fillSiteNameS();
            cmbbank.Enabled = false;
            txtchequeno.Enabled = false;
            dtpreceiptDate.Enabled = false;
            dtpchequedate.Enabled = false;
            txtbankname.Enabled = false;
            customerdata();
            customerdata1();
            this.ActiveControl = cmbcustomer;
            hidelabel();
            fdate1 = from;
            tdate1 = to;
            booktype1 = booktype;
            setrepo = new SettingRepository();
            setdata = setrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (setdata != null)
            {
                if (setdata.ShowUserDefinedSeries == "Y")
                    userSeries = true;
                else
                    userSeries = false;
            }
            loaddata(from, to, booktype);
        }

        public frmCustomerLedger(string CCreditors, DateTime fdate, DateTime tdate)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            Maxcustid();
            fillcombo(); fillStaffname();
            fillcombo1(); fillSalesman(); fillSiteName(); fillSiteNameS();
            cmbbank.Enabled = false;
            txtchequeno.Enabled = false;
            dtpreceiptDate.Enabled = false;
            dtpchequedate.Enabled = false;
            txtbankname.Enabled = false;
            customerdata();
            customerdata1();
            this.ActiveControl = dtpreceiptDate;
            hidelabel();
            fdate2 = fdate;
            tdate2 = tdate;
            BalShtToCustLedger(CCreditors, fdate, tdate);
            setrepo = new SettingRepository();
            setdata = setrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
        }

        public void loaddata(DateTime from, DateTime to, string booktype)
        {
            try
            {
                hidelabel();
                GvreceiptInfo.DataSource = null;
                GvreceiptInfo.Rows.Clear();
                GvreceiptInfo.Columns.Clear();
                string errormsg = "";
                db.connect();
                SqlCommand command = new SqlCommand("SPLedgerReport", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@LedgerName", SqlDbType.NVarChar).Value = "Customer";
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = Convert.ToInt32(cmbcustomername1.SelectedValue);
                command.Parameters.Add("@ReceiptType", SqlDbType.NVarChar).Value = "Receipt";
                if (booktype == "Cash")
                    command.Parameters.Add("@PaymentMode", SqlDbType.NVarChar).Value = booktype;
                CommonMethod com = new CommonMethod();
                command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = com.ValidFromDate(from);
                command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = com.ValidToDate(to);
                if (errormsg == "")
                {
                    DataTable dt = new DataTable();
                    dt.Load(command.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        GvreceiptInfo.DataSource = dt;
                        GvreceiptInfo.Refresh();
                        if (GvreceiptInfo.Rows.Count != 0)
                        {
                            GvreceiptInfo.Columns["Id"].Visible = false;
                            GvreceiptInfo.Columns["BId"].Visible = false;
                            if (userSeries == false)
                            {
                                GvreceiptInfo.Columns["UserSeries"].Visible = false;
                                GvreceiptInfo.Columns["BillNo"].Visible = true;
                            }
                            else
                            {
                                GvreceiptInfo.Columns["UserSeries"].Visible = true;
                                GvreceiptInfo.Columns["BillNo"].Visible = false;
                            }
                            GvreceiptInfo.Columns["BalAmt"].Visible = false;
                            GvreceiptInfo.Columns["Mode"].Visible = false;
                            GvreceiptInfo.Columns["Narration"].Visible = false;

                            object sumNetAmt = dt.Compute("Sum(NetAmt)", "");
                            object sumPaidAmt = dt.Compute("Sum(PaidAmt)", "");
                            object sumtotAmount = Convert.ToDecimal(sumNetAmt) - Convert.ToDecimal(sumPaidAmt);
                            lblAmtText.Visible = true;
                            lblRemAmt.Visible = true;
                            if (Convert.ToInt32(sumtotAmount) >= 0)
                            {
                                lblAmtText.Text = "Rem Amt:";
                                lblRemAmt.Text = Convert.ToString(sumtotAmount);
                            }
                            else
                            {
                                lblAmtText.Text = "Adv Amt:";
                                lblRemAmt.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(sumtotAmount)));
                            }
                        }
                    }
                    else
                        MessageBox.Show("Record not Present.", "Warning");
                }
                else
                    MessageBox.Show(errormsg, "Warning");
                db.CloseConnection();
            }
            catch (Exception)
            { db.CloseConnection(); }            
        }

        public void loaddata(int custid)
        {
            try
            {
                hidelabel();
                GvreceiptInfo.DataSource = null;
                GvreceiptInfo.Rows.Clear();
                GvreceiptInfo.Columns.Clear();
                db.connect();
                SqlCommand command = new SqlCommand("SPLedgerReport", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@LedgerName", SqlDbType.NVarChar).Value = "Customer";
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = custid;

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                if (dt.Rows.Count > 0)
                {
                    GvreceiptInfo.DataSource = dt;
                    GvreceiptInfo.Refresh();
                    if (GvreceiptInfo.Rows.Count != 0)
                    {
                        GvreceiptInfo.Columns["Id"].Visible = false;
                        GvreceiptInfo.Columns["BId"].Visible = false;
                        if (userSeries == false)
                        {
                            GvreceiptInfo.Columns["UserSeries"].Visible = false;
                            GvreceiptInfo.Columns["BillNo"].Visible = true;
                        }
                        else
                        {
                            GvreceiptInfo.Columns["UserSeries"].Visible = true;
                            GvreceiptInfo.Columns["BillNo"].Visible = false;
                        }
                        GvreceiptInfo.Columns["BalAmt"].Visible = false;
                        GvreceiptInfo.Columns["Mode"].Visible = false;
                        GvreceiptInfo.Columns["Narration"].Visible = false;

                        object sumNetAmt = dt.Compute("Sum(NetAmt)", "");
                        object sumPaidAmt = dt.Compute("Sum(PaidAmt)", "");
                        object sumtotAmount = Convert.ToDecimal(sumNetAmt) - Convert.ToDecimal(sumPaidAmt);
                        lblAmtText.Visible = true;
                        lblRemAmt.Visible = true;
                        if (Convert.ToInt32(sumtotAmount) >= 0)
                        {
                            lblAmtText.Text = "Rem Amt:";
                            lblRemAmt.Text = Convert.ToString(sumtotAmount);
                        }
                        else
                        {
                            lblAmtText.Text = "Adv Amt:";
                            lblRemAmt.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(sumtotAmount)));
                        }
                    }
                }
                else
                    MessageBox.Show("Record not Present.", "Warning");
                db.CloseConnection();
            }
            catch (Exception)
            { db.CloseConnection(); }            
        }

        public void Maxcustid()
        {
            try
            {
                RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                var mid = remainingpayrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.BillReceiptNo);
                if (mid == null)
                    txtreceiptno.Text = "1";
                else
                    txtreceiptno.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                remainingpayrepo.Dispose();
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
                cmbcustomer.DataSource = customerdata;
                cmbcustomer.ValueMember = "CustomerID";
                cmbcustomer.DisplayMember = "CustomerName";
                custrepo.Dispose();
                cmbcustomer.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void customerdata1()
        {
            try
            {
                CustomerRepository custrepo = new CustomerRepository();
                List<CustomerInformation> customerdata = new List<CustomerInformation>();
                customerdata.Clear();
                customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CustomerName).ToList();
                customerdata.Add(new CustomerInformation { CustomerName = "Select", CustomerID = 0 });
                customerdata.Reverse();
                cmbcustomername1.DataSource = customerdata;
                cmbcustomername1.ValueMember = "CustomerID";
                cmbcustomername1.DisplayMember = "CustomerName";
                custrepo.Dispose();
                cmbcustomername1.SelectedIndex = 0;

                SqlDataAdapter da = new SqlDataAdapter("Select Distinct(Type) from RemainingPayment where CompId=" + CommonMethod.CompId + " order by Type", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    cmbtype.Items.Add(row["Type"]);
                }
                cmbtype.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void fillcombo()
        {
            try
            {
                BankRepository bankrepo = new BankRepository();
                List<BankInformation> bankdata = new List<BankInformation>();
                bankdata.Clear();
                bankdata = bankrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.Bankname).ToList();
                bankdata.Add(new BankInformation { Bankname = "Select", BankId = 0 });
                bankdata.Reverse();
                cmbbank.DataSource = bankdata;
                cmbbank.ValueMember = "BankId";
                cmbbank.DisplayMember = "Bankname";
                cmbbank.SelectedIndex = 0;
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
                cmbStaffName.DataSource = Staffdata;
                cmbStaffName.ValueMember = "ID";
                cmbStaffName.DisplayMember = "Name";
                StaffRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillSalesman()
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

        public void fillSiteName()
        {
            try
            {
                SiteInfoRepository StaffRepo = new SiteInfoRepository();
                List<SiteInfo> Staffdata = new List<SiteInfo>();
                Staffdata.Clear();
                Staffdata = StaffRepo.GetAll().OrderByDescending(t => t.SiteName).ToList();
                Staffdata.Add(new SiteInfo { SiteName = "Select", Id = 0 });
                Staffdata.Reverse();
                cmbSiteName.DataSource = Staffdata;
                cmbSiteName.ValueMember = "Id";
                cmbSiteName.DisplayMember = "SiteName";
                StaffRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillSiteNameS()
        {
            try
            {
                SiteInfoRepository StaffRepo = new SiteInfoRepository();
                List<SiteInfo> Staffdata = new List<SiteInfo>();
                Staffdata.Clear();
                Staffdata = StaffRepo.GetAll().OrderByDescending(t => t.SiteName).ToList();
                Staffdata.Add(new SiteInfo { SiteName = "Select", Id = 0 });
                Staffdata.Reverse();
                cmbSiteNameS.DataSource = Staffdata;
                cmbSiteNameS.ValueMember = "Id";
                cmbSiteNameS.DisplayMember = "SiteName";
                StaffRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillcombo1()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Select Distinct(Bankname) from RemainingPayment where Bankname!='' and CompId=" + CommonMethod.CompId + " order by Bankname", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                foreach (DataRow row in dt.Rows)
                {
                    cmbbank2.Items.Add(row["Bankname"]);
                }
                cmbbank2.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                try
                {
                    dtpreceiptDate.Value = DateTime.Now.Date;
                    dtpchequedate.Value = DateTime.Now.Date;
                }
                catch (Exception)
                { }
                fillcombo(); fillStaffname();
                fillcombo1(); fillSalesman(); fillSiteName(); fillSiteNameS();
                txtTotalAmt.Text = "";
                txtPaidAmt.Text = "";
                txtRemainigAmt.Text = "";
                customerdata();
                txtchequeno.Text = "";
                txtbankname.Text = "";
                txtnarration.Text = "";
                Maxcustid();
                cmbpaymentmode.SelectedIndex = -1;
                chkAdjustVoucher.Checked = false;
                txtAdjustAmount.Visible = false;
                cmbbank.Enabled = false;
                txtchequeno.Enabled = false;
                dtpchequedate.Enabled = false;
                txtbankname.Enabled = false;
                btnSave.Text = "Save";
                label15.Text = "Cheque No:";
                txtManualReceipt.Text = "";
            }
            catch (Exception)
            { }
        }

        public void hidelabel()
        {
            try
            {
                lblRemAmt.Visible = false;
                lblAmtText.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            clear();
            totalPaidAmount = 0;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        public void updateceredit(int custid, string custname, decimal paidamt)
        {
            try
            {
                CustomerBillMasterRepository cMasterRepo = new CustomerBillMasterRepository();
                CreditAmountRepository CreditRepo = new CreditAmountRepository();
                List<CreditAmount> CreditData = new List<CreditAmount>();
                List<CustomerBillMaster> cMasterData = new List<CustomerBillMaster>();
                decimal totalpaidamt = 0;
                try
                {
                    cMasterData = cMasterRepo.GetAll().Where(t => t.CustomerId == custid && t.CustomerName == custname && t.YN == "0" && t.RemainingAmount > Convert.ToDecimal(0) && t.Billdate.Value.Date != t.PaymentDate.Value.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                }
                catch (Exception)
                { }
                try
                {
                    CreditData = CreditRepo.GetAll().Where(t => t.CustomerId == custid && t.Customername == custname && t.CompId == CommonMethod.CompId).ToList();
                }
                catch (Exception)
                { }

                if (cMasterData != null)
                {
                    decimal prepaidamt = Convert.ToDecimal(CreditData.Sum(t => t.RemainingAmt));
                    decimal creditamt = Convert.ToDecimal(cMasterData.Sum(t => t.RemainingAmount));
                    totalpaidamt = prepaidamt + paidamt;
                    if (creditamt <= totalpaidamt)
                    {
                        foreach (var i in cMasterData)
                        {
                            CustomerBillMasterRepository cMasterRepo1 = new CustomerBillMasterRepository();
                            CustomerBillMaster cData = cMasterRepo1.GetById(i.Id);
                            cData.YN = "1";
                            cMasterRepo1.Edit(cData);
                            cMasterRepo1.Save();
                            totalpaidamt = totalpaidamt - creditamt;
                            cMasterRepo1.Dispose();
                        }
                        foreach (var k in CreditData)
                        {
                            CreditAmountRepository CreditRepo1 = new CreditAmountRepository();
                            CreditAmount crdt = CreditRepo1.GetById(k.Id);
                            CreditRepo1.Remove(crdt);
                            CreditRepo1.Save();
                            CreditRepo1.Dispose();
                        }
                        if (totalpaidamt > 0)
                        {
                            CreditAmountRepository CreditRepo2 = new CreditAmountRepository();
                            CreditAmount crdtData = new CreditAmount();
                            crdtData.CustomerId = custid;
                            crdtData.Customername = custname;
                            crdtData.RemainingAmt = paidamt;
                            crdtData.CreateDate = dtpreceiptDate.Value;
                            crdtData.CompId = CommonMethod.CompId;
                            CreditRepo2.Add(crdtData);
                            CreditRepo2.Save();
                            CreditRepo2.Dispose();
                        }
                    }
                    else
                    {
                        decimal paidamt1 = totalpaidamt;
                        foreach (var i in cMasterData)
                        {
                            if (paidamt1 >= Convert.ToDecimal(i.RemainingAmount))
                            {
                                CustomerBillMasterRepository cMasterRepo1 = new CustomerBillMasterRepository();
                                CustomerBillMaster cData = cMasterRepo1.GetById(i.Id);
                                cData.YN = "1";
                                cMasterRepo1.Edit(cData);
                                cMasterRepo1.Save();
                                paidamt1 = paidamt1 - Convert.ToDecimal(i.RemainingAmount);
                                cMasterRepo1.Dispose();
                            }
                        }
                        foreach (var k in CreditData)
                        {
                            try
                            {
                                CreditAmountRepository CreditRepo1 = new CreditAmountRepository();
                                CreditAmount crdt = CreditRepo1.GetById(k.Id);
                                CreditRepo1.Remove(crdt);
                                CreditRepo1.Save();
                                CreditRepo1.Dispose();
                            }
                            catch (Exception)
                            { }
                        }
                        if (paidamt1 > 0)
                        {
                            CreditAmountRepository CreditRepo4 = new CreditAmountRepository();
                            CreditAmount crdtData = new CreditAmount();
                            crdtData.CustomerId = custid;
                            crdtData.Customername = custname;
                            crdtData.RemainingAmt = paidamt1;
                            crdtData.CreateDate = dtpreceiptDate.Value;
                            crdtData.CompId = CommonMethod.CompId;
                            CreditRepo4.Add(crdtData);
                            CreditRepo4.Save();
                            CreditRepo4.Dispose();
                        }
                    }
                }
                cMasterRepo.Dispose();
                CreditRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbcustomer.Text != "Select" && cmbpaymentmode.Text != "" && txtPaidAmt.Text != "")
                {
                    multipleReceiptPrintList = new List<string>();
                    if (btnSave.Text == "Save")
                    {
                        RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                        RemainingPayment remainingdata = new RemainingPayment();
                        Maxcustid();
                        remainingdata.BillReceiptNo = Convert.ToInt32(txtreceiptno.Text);
                        remainingdata.CustomerId = Convert.ToInt32(cmbcustomer.SelectedValue);
                        remainingdata.Customername = cmbcustomer.Text;
                        remainingdata.CreateDate = dtpreceiptDate.Value;
                        remainingdata.Type = "Receipt";
                        remainingdata.RemainingAmt = Convert.ToDecimal(txtRemainigAmt.Text);
                        remainingdata.NetAmt = Convert.ToDecimal(0);
                        remainingdata.PaidAmt = Convert.ToDecimal(txtPaidAmt.Text);
                        remainingdata.TransactionYear = CommonMethod.TransactionYear;
                        remainingdata.CompId = CommonMethod.CompId;
                        remainingdata.PaymentMode = cmbpaymentmode.Text;
                        remainingdata.ManualReceiptNo = txtManualReceipt.Text;
                        remainingdata.Narration = txtnarration.Text;
                        remainingdata.Status = typeOfLedger;
                        if (cmbStaffName.SelectedIndex != 0)
                            remainingdata.StaffId = Convert.ToInt32(cmbStaffName.SelectedValue);
                        if (cmbSiteName.SelectedIndex != 0)
                            remainingdata.SiteId = Convert.ToInt32(cmbSiteName.SelectedValue);
                        if (cmbpaymentmode.Text == "Cash")
                        {
                            remainingdata.Bankname = "";
                            remainingdata.Chequeno = "";
                            remainingdata.Chequedate = null;
                            remainingpayrepo.Add(remainingdata);
                            remainingpayrepo.Save();
                            customerid = Convert.ToInt32(cmbcustomer.SelectedValue);
                            customername = cmbcustomer.Text;
                            billreceiptno = txtreceiptno.Text;
                            updateceredit(Convert.ToInt32(cmbcustomer.SelectedValue), cmbcustomer.Text, Convert.ToDecimal(txtPaidAmt.Text));
                            if (chkAdjustVoucher.Checked && Convert.ToDecimal(txtAdjustAmount.Text) > 0)
                            {
                                try
                                {
                                    RemainingPaymentRepository remainingpayrepoadj = new RemainingPaymentRepository();
                                    RemainingPayment remainingdataadj = new RemainingPayment();
                                    remainingdataadj.CustomerId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                    remainingdataadj.Customername = cmbcustomer.Text;
                                    remainingdataadj.CreateDate = dtpreceiptDate.Value;
                                    remainingdataadj.Type = "Adjustment";
                                    remainingdataadj.RemainingAmt = Convert.ToDecimal((Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text)) - Convert.ToDecimal(txtAdjustAmount.Text));
                                    remainingdataadj.NetAmt = Convert.ToDecimal(0);
                                    remainingdataadj.PaidAmt = Convert.ToDecimal(txtAdjustAmount.Text);
                                    remainingdataadj.TransactionYear = CommonMethod.TransactionYear;
                                    remainingdataadj.CompId = CommonMethod.CompId;
                                    remainingdataadj.PaymentMode = cmbpaymentmode.Text;
                                    remainingdataadj.Bankname = "";
                                    remainingdataadj.Chequeno = "";
                                    remainingdataadj.Chequedate = null;
                                    remainingdataadj.ManualReceiptNo = txtManualReceipt.Text;
                                    remainingdataadj.Narration = txtnarration.Text;
                                    remainingdataadj.Status = typeOfLedger;
                                    remainingpayrepoadj.Add(remainingdataadj);
                                    remainingpayrepoadj.Save();
                                }
                                catch (Exception)
                                { }
                            }
                            MessageBox.Show("Receipt Information Saved.", "Success");
                            clear();
                            this.ActiveControl = btnprint;
                        }
                        else if (cmbpaymentmode.Text == "Cheque" || cmbpaymentmode.Text == "By Swipe")
                        {
                            if (cmbbank.Text != "Select" || txtchequeno.Text != "" || txtbankname.Text != "")
                            {
                                remainingdata.Bankname = txtbankname.Text;
                                remainingdata.Chequeno = txtchequeno.Text;
                                remainingdata.Chequedate = dtpchequedate.Value;
                                remainingdata.ManualReceiptNo = txtManualReceipt.Text;
                                remainingdata.Status = typeOfLedger;
                                remainingpayrepo.Add(remainingdata);
                                remainingpayrepo.Save();
                                customerid = Convert.ToInt32(cmbcustomer.SelectedValue);
                                customername = cmbcustomer.Text;
                                billreceiptno = txtreceiptno.Text;
                                updateceredit(Convert.ToInt32(cmbcustomer.SelectedValue), cmbcustomer.Text, Convert.ToDecimal(txtPaidAmt.Text));
                                try
                                {
                                    BankRepository bankInfoRepo = new BankRepository();
                                    var bankData = bankInfoRepo.GetAll().Where(t => t.BankId == Convert.ToInt32(cmbbank.SelectedValue) && t.Bankname == cmbbank.Text).FirstOrDefault();
                                    if (bankData != null)
                                        ACGId = Convert.ToInt32(bankData.ACGId);
                                    BankStatementRepository bankrepo = new BankStatementRepository();
                                    BankStatement bankstdata = new BankStatement();

                                    bankstdata.BankId = Convert.ToInt32(cmbbank.SelectedValue);
                                    bankstdata.Bankname = Convert.ToString(cmbbank.Text);
                                    bankstdata.TransactionDate = dtpreceiptDate.Value;
                                    bankstdata.Particulars = "Receive From" + " " + Convert.ToString(cmbcustomer.Text.Trim());
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    bankstdata.Deposits = Convert.ToDecimal(remainingdata.PaidAmt);
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankstdata.TransactionType = "Receipt";
                                    bankstdata.ACGId = ACGId;
                                    remainingdata.Narration = txtnarration.Text;
                                    bankrepo.Add(bankstdata);
                                    bankrepo.Save();
                                    bankrepo.Dispose();
                                }
                                catch (Exception)
                                { }
                                if (chkAdjustVoucher.Checked && Convert.ToDecimal(txtAdjustAmount.Text) > 0)
                                {
                                    try
                                    {
                                        RemainingPaymentRepository remainingpayrepoadj = new RemainingPaymentRepository();
                                        RemainingPayment remainingdataadj = new RemainingPayment();
                                        remainingdataadj.CustomerId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                        remainingdataadj.Customername = cmbcustomer.Text;
                                        remainingdataadj.CreateDate = dtpreceiptDate.Value;
                                        remainingdataadj.Type = "Adjustment";
                                        remainingdataadj.RemainingAmt = Convert.ToDecimal((Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text)) - Convert.ToDecimal(txtAdjustAmount.Text));
                                        remainingdataadj.NetAmt = Convert.ToDecimal(0);
                                        remainingdataadj.PaidAmt = Convert.ToDecimal(txtAdjustAmount.Text);
                                        remainingdataadj.TransactionYear = CommonMethod.TransactionYear;
                                        remainingdataadj.CompId = CommonMethod.CompId;
                                        remainingdataadj.PaymentMode = cmbpaymentmode.Text;
                                        remainingdataadj.Bankname = txtbankname.Text;
                                        remainingdataadj.Chequeno = txtchequeno.Text;
                                        remainingdataadj.Chequedate = dtpchequedate.Value;
                                        remainingdataadj.ManualReceiptNo = txtManualReceipt.Text;
                                        remainingdataadj.Narration = txtnarration.Text;
                                        remainingdataadj.Status = typeOfLedger;
                                        remainingpayrepoadj.Add(remainingdataadj);
                                        remainingpayrepoadj.Save();
                                    }
                                    catch (Exception)
                                    { }
                                }
                                MessageBox.Show("Receipt Information Saved.", "Success");
                                clear();
                                this.ActiveControl = btnprint;
                            }
                            else
                            {
                                if (txtbankname.Text == "")
                                {
                                    MessageBox.Show("Enter Bank name.", "Warning");
                                    this.ActiveControl = txtbankname;
                                }
                                else if (cmbbank.Text == "Select")
                                {
                                    MessageBox.Show("Select Bank name.", "Warning");
                                    this.ActiveControl = cmbbank;
                                }
                                else
                                {
                                    MessageBox.Show("Enter Cheque Number.", "Warning");
                                    this.ActiveControl = txtchequeno;
                                }
                            }
                        }
                        else
                        {
                            if (cmbbank.Text.TrimStart() != "Select" && txtchequeno.Text.TrimStart() != "")
                            {
                                remainingdata.Bankname = cmbbank.Text;
                                remainingdata.Chequeno = txtchequeno.Text;
                                remainingdata.Chequedate = dtpchequedate.Value;
                                remainingdata.ManualReceiptNo = txtManualReceipt.Text;
                                remainingdata.Status = typeOfLedger;
                                remainingpayrepo.Add(remainingdata);
                                remainingpayrepo.Save();
                                customerid = Convert.ToInt32(cmbcustomer.SelectedValue);
                                customername = cmbcustomer.Text;
                                billreceiptno = txtreceiptno.Text;
                                updateceredit(Convert.ToInt32(cmbcustomer.SelectedValue), cmbcustomer.Text, Convert.ToDecimal(txtPaidAmt.Text));
                                try
                                {
                                    BankRepository bankInfoRepo = new BankRepository();
                                    var bankData = bankInfoRepo.GetAll().Where(t => t.BankId == Convert.ToInt32(cmbbank.SelectedValue) && t.Bankname == cmbbank.Text).FirstOrDefault();
                                    if (bankData != null)
                                        ACGId = Convert.ToInt32(bankData.ACGId);
                                    BankStatementRepository bankrepo = new BankStatementRepository();
                                    BankStatement bankstdata = new BankStatement();

                                    bankstdata.BankId = Convert.ToInt32(cmbbank.SelectedValue);
                                    bankstdata.Bankname = Convert.ToString(cmbbank.Text);
                                    bankstdata.TransactionDate = dtpreceiptDate.Value;
                                    bankstdata.Particulars = "Receive From" + " " + Convert.ToString(cmbcustomer.Text.Trim());
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    bankstdata.Deposits = Convert.ToDecimal(remainingdata.PaidAmt);
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankstdata.TransactionType = "Receipt";
                                    bankstdata.ACGId = ACGId;
                                    bankrepo.Add(bankstdata);
                                    bankrepo.Save();
                                    bankrepo.Dispose();
                                }
                                catch (Exception)
                                { }
                                if (chkAdjustVoucher.Checked && Convert.ToDecimal(txtAdjustAmount.Text) > 0)
                                {
                                    try
                                    {
                                        RemainingPaymentRepository remainingpayrepoadj = new RemainingPaymentRepository();
                                        RemainingPayment remainingdataadj = new RemainingPayment();
                                        remainingdataadj.CustomerId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                        remainingdataadj.Customername = cmbcustomer.Text;
                                        remainingdataadj.CreateDate = dtpreceiptDate.Value;
                                        remainingdataadj.Type = "Adjustment";
                                        remainingdataadj.RemainingAmt = (Convert.ToDecimal(Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text)) - Convert.ToDecimal(txtAdjustAmount.Text));
                                        remainingdataadj.NetAmt = Convert.ToDecimal(0);
                                        remainingdataadj.PaidAmt = Convert.ToDecimal(txtAdjustAmount.Text);
                                        remainingdataadj.TransactionYear = CommonMethod.TransactionYear;
                                        remainingdataadj.CompId = CommonMethod.CompId;
                                        remainingdataadj.PaymentMode = cmbpaymentmode.Text;
                                        remainingdataadj.Bankname = txtbankname.Text;
                                        remainingdataadj.Chequeno = txtchequeno.Text;
                                        remainingdataadj.Chequedate = dtpchequedate.Value;
                                        remainingdataadj.ManualReceiptNo = txtManualReceipt.Text;
                                        remainingdataadj.Narration = txtnarration.Text;
                                        remainingpayrepoadj.Add(remainingdataadj);
                                        remainingpayrepoadj.Save();
                                    }
                                    catch (Exception)
                                    { }
                                }
                                MessageBox.Show("Receipt Information Saved.", "Success");
                                clear();
                                this.ActiveControl = btnprint;
                            }
                            else
                            {
                                if (cmbbank.Text.TrimStart() == "Select")
                                {
                                    MessageBox.Show("Select Bank Name.", "Success");
                                    this.ActiveControl = cmbbank;
                                }
                                else
                                {
                                    MessageBox.Show("Enter UTR Number.", "Success");
                                    this.ActiveControl = txtchequeno;
                                }
                            }
                        }
                        remainingpayrepo.Dispose();
                    }
                    else
                    {
                        RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();

                        RemainingPayment remainingdata = new RemainingPayment();
                        remainingdata = remainingpayrepo.GetById(receiptid);
                        remainingdata.BillReceiptNo = Convert.ToInt32(txtreceiptno.Text);
                        remainingdata.CustomerId = Convert.ToInt32(cmbcustomer.SelectedValue);
                        remainingdata.Customername = cmbcustomer.Text;
                        remainingdata.CreateDate = dtpreceiptDate.Value;
                        remainingdata.Type = "Receipt";
                        remainingdata.RemainingAmt = Convert.ToDecimal(txtRemainigAmt.Text);
                        remainingdata.NetAmt = Convert.ToDecimal(0);
                        remainingdata.PaidAmt = Convert.ToDecimal(txtPaidAmt.Text);
                        remainingdata.TransactionYear = CommonMethod.TransactionYear;
                        remainingdata.CompId = CommonMethod.CompId;
                        remainingdata.PaymentMode = cmbpaymentmode.Text;
                        remainingdata.ManualReceiptNo = txtManualReceipt.Text;
                        remainingdata.Narration = txtnarration.Text;
                        remainingdata.Status = typeOfLedger;
                        if (cmbStaffName.SelectedIndex != 0)
                            remainingdata.StaffId = Convert.ToInt32(cmbStaffName.SelectedValue);
                        if (cmbSiteName.SelectedIndex != 0)
                            remainingdata.SiteId = Convert.ToInt32(cmbSiteName.SelectedValue);
                        if (cmbpaymentmode.Text == "Cash")
                        {
                            remainingdata.Bankname = "";
                            remainingdata.Chequeno = "";
                            remainingdata.Chequedate = null;
                            remainingpayrepo.Edit(remainingdata);
                            remainingpayrepo.Save();
                            customerid = Convert.ToInt32(cmbcustomer.SelectedValue);
                            decimal decremamt = Convert.ToDecimal(txtRemainigAmt.Text);

                            RemainingPaymentRepository remainingpayrepo2 = new RemainingPaymentRepository();
                            List<RemainingPayment> remainingdata2 = new List<RemainingPayment>();
                            remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(customerid) && t.Id > receiptid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                            int i = 0;
                            foreach (var item in remainingdata2)
                            {
                                item.RemainingAmt = (Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt)) + decremamt;
                                decremamt = (Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt)) + decremamt;
                                remainingpayrepo2.Edit(item);
                                remainingpayrepo2.Save();
                                i++;
                            }
                            customername = cmbcustomer.Text;
                            billreceiptno = txtreceiptno.Text;
                            updateceredit(Convert.ToInt32(cmbcustomer.SelectedValue), cmbcustomer.Text, Convert.ToDecimal(txtPaidAmt.Text));
                            if (chkAdjustVoucher.Checked && Convert.ToDecimal(txtAdjustAmount.Text) > 0)
                            {
                                try
                                {
                                    RemainingPaymentRepository remainingpayrepoadj = new RemainingPaymentRepository();
                                    RemainingPayment remainingdataadj = new RemainingPayment();
                                    remainingdataadj.CustomerId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                    remainingdataadj.Customername = cmbcustomer.Text;
                                    remainingdataadj.CreateDate = dtpreceiptDate.Value;
                                    remainingdataadj.Type = "Adjustment";
                                    remainingdataadj.RemainingAmt = Convert.ToDecimal(Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text) - Convert.ToDecimal(txtAdjustAmount.Text));
                                    remainingdataadj.NetAmt = Convert.ToDecimal(0);
                                    remainingdataadj.PaidAmt = Convert.ToDecimal(txtAdjustAmount.Text);
                                    remainingdataadj.TransactionYear = CommonMethod.TransactionYear;
                                    remainingdataadj.CompId = CommonMethod.CompId;
                                    remainingdataadj.PaymentMode = cmbpaymentmode.Text;
                                    remainingdataadj.Bankname = "";
                                    remainingdataadj.Chequeno = "";
                                    remainingdataadj.Chequedate = null;
                                    remainingdataadj.ManualReceiptNo = txtManualReceipt.Text;
                                    remainingdataadj.Narration = txtnarration.Text;
                                    remainingdataadj.Status = typeOfLedger;
                                    remainingpayrepoadj.Add(remainingdataadj);
                                    remainingpayrepoadj.Save();
                                }
                                catch (Exception)
                                { }
                            }
                            MessageBox.Show("Receipt Information Saved.", "Success");
                            clear();
                            this.ActiveControl = btnprint;
                        }
                        else if (cmbpaymentmode.Text == "Cheque" || cmbpaymentmode.Text == "By Swipe")
                        {
                            if (cmbbank.Text != "Select" || txtchequeno.Text != "" || txtbankname.Text != "")
                            {
                                remainingdata.Bankname = txtbankname.Text;
                                remainingdata.Chequeno = txtchequeno.Text;
                                remainingdata.Chequedate = dtpchequedate.Value;
                                remainingdata.ManualReceiptNo = txtManualReceipt.Text;
                                try
                                {
                                    RemainingPaymentRepository remRepo = new RemainingPaymentRepository();
                                    RemainingPayment remdata = remRepo.GetById(receiptid);
                                    BankStatementRepository BankStRepo = new BankStatementRepository();
                                    var bdata = BankStRepo.GetAll().Where(t => t.Bankname == cmbbank.Text && t.CompId == CommonMethod.CompId && t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.Chequeno && t.Deposits == Convert.ToDecimal(remdata.PaidAmt)).FirstOrDefault();
                                    BankRepository bankInfoRepo = new BankRepository();
                                    var bankData = bankInfoRepo.GetAll().Where(t => t.BankId == Convert.ToInt32(cmbbank.SelectedValue) && t.Bankname == cmbbank.Text).FirstOrDefault();
                                    if (bankData != null)
                                        ACGId = Convert.ToInt32(bankData.ACGId);
                                    BankStatementRepository bankrepo = new BankStatementRepository();
                                    BankStatement bankstdata = new BankStatement();
                                    bankstdata.BankId = Convert.ToInt32(cmbbank.SelectedValue);
                                    bankstdata.Bankname = Convert.ToString(cmbbank.Text);
                                    bankstdata.TransactionDate = dtpreceiptDate.Value;
                                    bankstdata.Particulars = "Receive From" + " " + Convert.ToString(cmbcustomer.Text.Trim());
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    bankstdata.Deposits = Convert.ToDecimal(txtPaidAmt.Text);
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankstdata.TransactionType = "Receipt";
                                    bankstdata.ACGId = ACGId;
                                    if (bdata != null)
                                    {
                                        bankstdata.Id = bdata.Id;
                                        bankrepo.Edit(bankstdata);
                                        bankrepo.Save();
                                    }
                                    else if (bdata == null)
                                    {
                                        bankrepo.Add(bankstdata);
                                        bankrepo.Save();
                                    }
                                    BankStRepo.Dispose();
                                    bankrepo.Dispose();
                                }
                                catch (Exception)
                                { }

                                remainingpayrepo.Edit(remainingdata);
                                remainingpayrepo.Save();
                                customerid = Convert.ToInt32(cmbcustomer.SelectedValue);
                                decimal decremamt = Convert.ToDecimal(txtRemainigAmt.Text);

                                RemainingPaymentRepository remainingpayrepo2 = new RemainingPaymentRepository();
                                List<RemainingPayment> remainingdata2 = new List<RemainingPayment>();
                                remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(customerid) && t.Id > receiptid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                int i = 0;
                                foreach (var item in remainingdata2)
                                {
                                    item.RemainingAmt = (Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt)) + decremamt;
                                    decremamt = (Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt)) + decremamt;
                                    remainingpayrepo2.Edit(item);
                                    remainingpayrepo2.Save();
                                    i++;
                                }
                                customername = cmbcustomer.Text;
                                billreceiptno = txtreceiptno.Text;
                                updateceredit(Convert.ToInt32(cmbcustomer.SelectedValue), cmbcustomer.Text, Convert.ToDecimal(txtPaidAmt.Text));

                                if (chkAdjustVoucher.Checked && Convert.ToDecimal(txtAdjustAmount.Text) > 0)
                                {
                                    try
                                    {
                                        RemainingPaymentRepository remainingpayrepoadj = new RemainingPaymentRepository();
                                        RemainingPayment remainingdataadj = new RemainingPayment();
                                        remainingdataadj.CustomerId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                        remainingdataadj.Customername = cmbcustomer.Text;
                                        remainingdataadj.CreateDate = dtpreceiptDate.Value;
                                        remainingdataadj.Type = "Adjustment";
                                        remainingdataadj.RemainingAmt = Convert.ToDecimal(Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text) - Convert.ToDecimal(txtAdjustAmount.Text));
                                        remainingdataadj.NetAmt = Convert.ToDecimal(0);
                                        remainingdataadj.PaidAmt = Convert.ToDecimal(txtAdjustAmount.Text);
                                        remainingdataadj.TransactionYear = CommonMethod.TransactionYear;
                                        remainingdataadj.CompId = CommonMethod.CompId;
                                        remainingdataadj.PaymentMode = cmbpaymentmode.Text;
                                        remainingdataadj.Bankname = txtbankname.Text;
                                        remainingdataadj.Chequeno = txtchequeno.Text;
                                        remainingdataadj.Chequedate = dtpchequedate.Value;
                                        remainingdataadj.ManualReceiptNo = txtManualReceipt.Text;
                                        remainingdataadj.Narration = txtnarration.Text;
                                        remainingdataadj.Status = typeOfLedger;
                                        remainingpayrepoadj.Add(remainingdataadj);
                                        remainingpayrepoadj.Save();
                                    }
                                    catch (Exception)
                                    { }
                                }
                                MessageBox.Show("Receipt Information Saved.", "Success");
                                clear();
                                this.ActiveControl = btnprint;
                            }
                            else
                            {
                                if (txtbankname.Text == "")
                                {
                                    MessageBox.Show("Enter Bank name.", "Warning");
                                    this.ActiveControl = txtbankname;
                                }
                                else if (cmbbank.Text == "Select")
                                {
                                    MessageBox.Show("Select Bank name.", "Warning");
                                    this.ActiveControl = cmbbank;
                                }
                                else
                                {
                                    MessageBox.Show("Enter Cheque Number.", "Warning");
                                    this.ActiveControl = txtchequeno;
                                }
                            }
                        }
                        else
                        {
                            if (cmbbank.Text.TrimStart() != "Select" && txtchequeno.Text.TrimStart() != "")
                            {
                                remainingdata.Bankname = cmbbank.Text;
                                remainingdata.Chequeno = txtchequeno.Text;
                                remainingdata.Chequedate = dtpchequedate.Value;
                                remainingdata.ManualReceiptNo = txtManualReceipt.Text;
                                try
                                {
                                    RemainingPaymentRepository remRepo = new RemainingPaymentRepository();
                                    RemainingPayment remdata = remRepo.GetById(receiptid);
                                    BankStatementRepository BankStRepo = new BankStatementRepository();
                                    var bdata = BankStRepo.GetAll().Where(t => t.Bankname == cmbbank.Text && t.CompId == CommonMethod.CompId && t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.Chequeno && t.Deposits == Convert.ToDecimal(remdata.PaidAmt)).FirstOrDefault();
                                    BankRepository bankInfoRepo = new BankRepository();
                                    var bankData = bankInfoRepo.GetAll().Where(t => t.BankId == Convert.ToInt32(cmbbank.SelectedValue) && t.Bankname == cmbbank.Text).FirstOrDefault();
                                    if (bankData != null)
                                        ACGId = Convert.ToInt32(bankData.ACGId);
                                    BankStatementRepository bankrepo = new BankStatementRepository();
                                    BankStatement bankstdata = new BankStatement();
                                    bankstdata.BankId = Convert.ToInt32(cmbbank.SelectedValue);
                                    bankstdata.Bankname = Convert.ToString(cmbbank.Text);
                                    bankstdata.TransactionDate = dtpreceiptDate.Value;
                                    bankstdata.Particulars = "Receive From" + " " + Convert.ToString(cmbcustomer.Text.Trim());
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    bankstdata.Deposits = Convert.ToDecimal(txtPaidAmt.Text);
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankstdata.TransactionType = "Receipt";
                                    bankstdata.ACGId = ACGId;
                                    if (bdata != null)
                                    {
                                        bankstdata.Id = bdata.Id;
                                        bankrepo.Edit(bankstdata);
                                        bankrepo.Save();
                                    }
                                    else if (bdata == null)
                                    {
                                        bankrepo.Add(bankstdata);
                                        bankrepo.Save();
                                    }
                                    BankStRepo.Dispose();
                                    bankrepo.Dispose();
                                }
                                catch (Exception)
                                { }

                                remainingpayrepo.Edit(remainingdata);
                                remainingpayrepo.Save();
                                customerid = Convert.ToInt32(cmbcustomer.SelectedValue);
                                decimal decremamt = Convert.ToDecimal(txtRemainigAmt.Text);
                                RemainingPaymentRepository remainingpayrepo2 = new RemainingPaymentRepository();
                                List<RemainingPayment> remainingdata2 = new List<RemainingPayment>();
                                remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(customerid) && t.Id > receiptid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                int i = 0;
                                foreach (var item in remainingdata2)
                                {
                                    item.RemainingAmt = (Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt)) + decremamt;
                                    decremamt = (Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt)) + decremamt;
                                    remainingpayrepo2.Edit(item);
                                    remainingpayrepo2.Save();
                                    i++;
                                }
                                customername = cmbcustomer.Text;
                                billreceiptno = txtreceiptno.Text;
                                updateceredit(Convert.ToInt32(cmbcustomer.SelectedValue), cmbcustomer.Text, Convert.ToDecimal(txtPaidAmt.Text));
                                if (chkAdjustVoucher.Checked && Convert.ToDecimal(txtAdjustAmount.Text) > 0)
                                {
                                    try
                                    {
                                        RemainingPaymentRepository remainingpayrepoadj = new RemainingPaymentRepository();
                                        RemainingPayment remainingdataadj = new RemainingPayment();
                                        remainingdataadj.CustomerId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                        remainingdataadj.Customername = cmbcustomer.Text;
                                        remainingdataadj.CreateDate = dtpreceiptDate.Value;
                                        remainingdataadj.Type = "Adjustment";
                                        remainingdataadj.RemainingAmt = Convert.ToDecimal(Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text) - Convert.ToDecimal(txtAdjustAmount.Text));
                                        remainingdataadj.NetAmt = Convert.ToDecimal(0);
                                        remainingdataadj.PaidAmt = Convert.ToDecimal(txtAdjustAmount.Text);
                                        remainingdataadj.TransactionYear = CommonMethod.TransactionYear;
                                        remainingdataadj.CompId = CommonMethod.CompId;
                                        remainingdataadj.PaymentMode = cmbpaymentmode.Text;
                                        remainingdataadj.Bankname = txtbankname.Text;
                                        remainingdataadj.Chequeno = txtchequeno.Text;
                                        remainingdataadj.Chequedate = dtpchequedate.Value;
                                        remainingdataadj.ManualReceiptNo = txtManualReceipt.Text;
                                        remainingdataadj.Narration = txtnarration.Text;
                                        remainingdataadj.Status = typeOfLedger;
                                        remainingpayrepoadj.Add(remainingdataadj);
                                        remainingpayrepoadj.Save();
                                    }
                                    catch (Exception)
                                    { }
                                }
                                MessageBox.Show("Receipt Information Saved.", "Success");
                                clear();
                                this.ActiveControl = btnprint;
                            }
                            else
                            {
                                if (cmbbank.Text.TrimStart() == "Select")
                                {
                                    MessageBox.Show("Select Bank Name.", "Warning");
                                    this.ActiveControl = cmbbank;
                                }
                                else
                                {
                                    MessageBox.Show("Enter UTR Number.", "Warning");
                                    this.ActiveControl = txtchequeno;
                                }
                            }
                        }
                        remainingpayrepo.Dispose();
                    }
                }
                else
                {
                    if (cmbcustomer.Text == "Select")
                    {
                        MessageBox.Show("Select Customer Name.", "Warning");
                        this.ActiveControl = cmbcustomer;
                    }
                    else if (txtPaidAmt.Text == "")
                    {
                        MessageBox.Show("Enter Amount.", "Warning");
                        this.ActiveControl = txtPaidAmt;
                    }
                    else
                    {
                        MessageBox.Show("Select Payment Mode.", "Warning");
                        this.ActiveControl = cmbpaymentmode;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbpaymentmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbpaymentmode.Text == "Cheque")
                {
                    cmbbank.Enabled = true;
                    txtchequeno.Enabled = true;
                    dtpchequedate.Enabled = true;
                    txtbankname.Enabled = true;
                    label15.Text = "Cheque No:";
                    label13.Text = "Cheque Date :";
                }
                else if (cmbpaymentmode.Text == "NEFT" || cmbpaymentmode.Text == "RTGS")
                {
                    cmbbank.Enabled = true;
                    txtchequeno.Enabled = true;
                    dtpchequedate.Enabled = true;
                    txtbankname.Enabled = false;
                    label15.Text = "UTR No:";
                    if (cmbpaymentmode.Text == "NEFT")
                        label13.Text = "NEFT Date:";
                    else
                        label13.Text = "RTGS Date:";
                }
                else if (cmbpaymentmode.Text == "By Swipe")
                {
                    cmbbank.Enabled = true;
                    txtbankname.Enabled = true;
                }
                else
                {
                    cmbbank.Enabled = false;
                    txtchequeno.Enabled = false;
                    dtpchequedate.Enabled = false;
                    txtbankname.Enabled = false;
                }
            }
            catch (Exception)
            { }
        }

        public void getcustomerdata()
        {
            try
            {
                if (chkcustomername1.Checked == true)
                {
                    if (cmbcustomername1.Text != "Select")
                    {
                        CustomerRepository supplierRepo = new CustomerRepository();
                        var supplierData = supplierRepo.GetAll().Where(t => t.CustomerID == Convert.ToInt32(cmbcustomername1.SelectedValue) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        int cdays = 0;
                        if (!string.IsNullOrEmpty(Convert.ToString(supplierData.CreditDays)))
                            cdays = Convert.ToInt32(supplierData.CreditDays);
                        else
                            cdays = 0;
                        SqlDataAdapter da = new SqlDataAdapter();
                        da = new SqlDataAdapter("SELECT rp.[ID],rp.BillId as 'Bill No.',cm.UserSeries as 'User Series',CONVERT(VARCHAR(24),rp.BillDate,103) as 'Bill Date',rp.[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),rp.[CreateDate ],103) as 'Receipt Date',rp.[Type],rp.[Customername] as 'Customer Name' ,rp.[NetAmt] as 'Net Amt.',rp.[PaidAmt] as 'Paid Amt.',rp.[RemainingAmt] as 'Bal. Amt' ,rp.[Paymentmode] as 'Mode',rp.[bankname] as 'Bank Name',rp.[ChequeNo] as 'Chq./UTR No.',CONVERT(VARCHAR(24),rp.[ChequeDate],103) as 'Chq./NEFT Date' FROM [RemainingPayment] rp left join " +
                                                " customerbillmaster as cm on rp.billid=cm.billno and rp.compid=cm.compid and rp.transactionyear=cm.transactionyear where cm.CustomerId='" + cmbcustomername1.SelectedValue + "' and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId=" + CommonMethod.CompId + " and DATEDIFF(DAY, CreateDate,getdate())>='" + cdays + "'", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        GvreceiptInfo.DataSource = dt;
                        GvreceiptInfo.Refresh();
                        if (GvreceiptInfo.Rows.Count != 0)
                        {
                            GvreceiptInfo.Columns["ID"].Visible = false;
                            GvreceiptInfo.Columns["BalAmt"].Visible = false;
                            GvreceiptInfo.Columns["Mode"].Visible = false;
                            if (userSeries == false)
                            {
                                GvreceiptInfo.Columns["UserSeries"].Visible = false;
                                GvreceiptInfo.Columns["BillNo"].Visible = true;
                            }
                            else
                            {
                                GvreceiptInfo.Columns["UserSeries"].Visible = true;
                                GvreceiptInfo.Columns["BillNo"].Visible = false;
                            }

                            try
                            {
                                db.connect();
                                SqlCommand command = new SqlCommand("SPremainingpayment", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                                command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "CustRem";
                                command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = cmbcustomername1.SelectedValue;
                                DataTable dt1 = new DataTable();
                                dt1.Load(command.ExecuteReader());
                                object sumtotAmount = new object();
                                sumtotAmount = dt1.Compute("Sum(TotalReaminingAmount)", "");
                                db.CloseConnection();
                                lblAmtText.Visible = true;
                                lblRemAmt.Visible = true;
                                if (Convert.ToInt32(sumtotAmount) >= 0)
                                {
                                    lblAmtText.Text = "Rem Amt:";
                                    lblRemAmt.Text = Convert.ToString(sumtotAmount);
                                }
                                else
                                {
                                    lblAmtText.Text = "Adv Amt:";
                                    lblRemAmt.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(sumtotAmount)));
                                }
                                dt.Dispose();
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
                            this.ActiveControl = cmbcustomername1;
                        }
                        else
                        {
                            MessageBox.Show("Record Not Found.", "Warning");
                            this.ActiveControl = cmbcustomername1;
                        }                        
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Please Select Customer Name.", "Warning");
                        this.ActiveControl = cmbcustomername1;
                    }
                }
                else
                    MessageBox.Show("Please Check Checkbox First.", "Warning");
            }
            catch (Exception)
            { }
        }

        private int GetDaysBetweenDates(DateTime firstDate, DateTime secondDate)
        {
            return secondDate.Subtract(firstDate).Days;
        }

        private void cmbcustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbcustomer.SelectedIndex > 0)
                {
                    try
                    {
                        db.connect();
                        SqlCommand command = new SqlCommand("SPremainingpayment", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                        command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = cmbcustomer.SelectedValue;
                        if (typeOfLedger == "All")
                            command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "CustRem";
                        else
                        {
                            command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "CustRemLedgerType";
                            command.Parameters.Add("@typeOfLedger", SqlDbType.NVarChar).Value = typeOfLedger;
                        }
                        DataTable dt1 = new DataTable();
                        dt1.Load(command.ExecuteReader());
                        object sumtotAmount = new object();
                        sumtotAmount = dt1.Compute("Sum(TotalReaminingAmount)", "");
                        db.CloseConnection();
                        txtTotalAmt.Text = Convert.ToString(sumtotAmount);
                        dt1.Dispose();
                    }
                    catch (Exception)
                    { db.CloseConnection(); }
                }
                else
                    txtTotalAmt.Text = "0";
            }
            catch (Exception)
            { }
        }

        private void txtPaidAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Enabled == true)
                {
                    if (txtPaidAmt.Text != "")
                        txtRemainigAmt.Text = Convert.ToString((Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text)));
                    else
                    {
                        txtPaidAmt.Text = "0";
                        this.ActiveControl = txtPaidAmt;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (multipleReceiptPrintList.Count == 0)
                {
                    RptReceipt receiptrpt = new RptReceipt(billreceiptno, customername, customerid, totalPaidAmount);
                    receiptrpt.ShowDialog();
                    receiptrpt.Dispose();
                }
                else
                {
                    if (setdata.OneTimeReceiptAmountInReport == "True")
                    {
                        RptReceipt receiptrpt = new RptReceipt(billreceiptno, customername, customerid, totalPaidAmount);
                        receiptrpt.ShowDialog();
                        receiptrpt.Dispose();
                    }
                    else
                    {
                        foreach (var item in multipleReceiptPrintList)
                        {
                            totalPaidAmount = 0;
                            RptReceipt receiptrpt = new RptReceipt(item.ToString(), customername, customerid, totalPaidAmount);
                            receiptrpt.ShowDialog();
                            receiptrpt.Dispose();
                        }
                    }
                }
                totalPaidAmount = 0;
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
            if (keyData == Keys.F3)
            {
                btnSave_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                hidelabel();
                GvreceiptInfo.DataSource = null;
                GvreceiptInfo.Rows.Clear();
                GvreceiptInfo.Columns.Clear();
                string errormsg = "";
                db.connect();
                SqlCommand command = new SqlCommand("SPLedgerReport", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@LedgerName", SqlDbType.NVarChar).Value = "Customer";
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                command.Parameters.Add("@typeOfLedger", SqlDbType.NVarChar).Value = typeOfLedger;
                if (chkcustomername1.Checked == true)
                {
                    if (cmbcustomername1.Text != "Select")
                        command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = Convert.ToInt32(cmbcustomername1.SelectedValue);
                    else
                        errormsg = errormsg + Environment.NewLine + "Please Select Supplier Name.";
                }
                if (chkbankname.Checked == true)
                {
                    if (cmbbank2.Text != "Select")
                        command.Parameters.Add("@BankName", SqlDbType.VarChar).Value = Convert.ToString(cmbbank2.SelectedText);
                    else
                        errormsg = errormsg + Environment.NewLine + "Please Select Bank Name.";
                }
                if (chkSalesman.Checked == true)
                {
                    if (cmbSalesman.Text != "Select")
                        command.Parameters.Add("@SalesmanId", SqlDbType.Int).Value = Convert.ToInt32(cmbSalesman.SelectedValue);
                    else
                        errormsg = errormsg + Environment.NewLine + "Please Select Salesman Name.";
                }
                if (chkSiteName.Checked == true)
                {
                    if (cmbSiteNameS.Text != "Select")
                        command.Parameters.Add("@SiteId", SqlDbType.Int).Value = Convert.ToInt32(cmbSiteNameS.SelectedValue);
                    else
                        errormsg = errormsg + Environment.NewLine + "Please Select Site Name.";
                }
                if (chktype.Checked == true)
                {
                    if (cmbtype.Text != "Select")
                        command.Parameters.Add("@ReceiptType", SqlDbType.NVarChar).Value = cmbtype.Text.ToString();
                    else
                        errormsg = errormsg + Environment.NewLine + "Please Select Type.";
                }
                if (chkdate.Checked == true)
                    command.Parameters.Add("@Date", SqlDbType.NVarChar).Value = dtpdate.Value.ToString("dd/MM/yyyy");
                if (chkbetdate.Checked == true)
                {
                    if (dtpfromdate.Value.Date <= dtptodate.Value.Date)
                    {
                        CommonMethod com = new CommonMethod();
                        command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = com.ValidFromDate(dtpfromdate.Value.Date);
                        command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = com.ValidToDate(dtptodate.Value.Date);
                    }
                    else
                        errormsg = errormsg + Environment.NewLine + "To Date Should be greater than from date.";
                }
                if (errormsg == "")
                {
                    DataTable dt = new DataTable();
                    dt.Load(command.ExecuteReader());
                    if (dt.Rows.Count > 0)
                    {
                        GvreceiptInfo.DataSource = dt;
                        GvreceiptInfo.Refresh();
                        if (GvreceiptInfo.Rows.Count != 0)
                        {
                            GvreceiptInfo.Columns["Id"].Visible = false;
                            GvreceiptInfo.Columns["BId"].Visible = false;
                            if (userSeries == false)
                            {
                                GvreceiptInfo.Columns["UserSeries"].Visible = false;
                                GvreceiptInfo.Columns["BillNo"].Visible = true;
                            }
                            else
                            {
                                GvreceiptInfo.Columns["UserSeries"].Visible = true;
                                GvreceiptInfo.Columns["BillNo"].Visible = false;
                            }
                            GvreceiptInfo.Columns["BalAmt"].Visible = false;
                            GvreceiptInfo.Columns["Mode"].Visible = false;
                            GvreceiptInfo.Columns["Narration"].Visible = false;

                            object sumNetAmt = dt.Compute("Sum(NetAmt)", "");
                            object sumPaidAmt = dt.Compute("Sum(PaidAmt)", "");
                            object sumtotAmount = Convert.ToDecimal(sumNetAmt) - Convert.ToDecimal(sumPaidAmt);
                            lblAmtText.Visible = true;
                            lblRemAmt.Visible = true;
                            if (Convert.ToInt32(sumtotAmount) >= 0)
                            {
                                lblAmtText.Text = "Rem Amt:";
                                lblRemAmt.Text = Convert.ToString(sumtotAmount);
                            }
                            else
                            {
                                lblAmtText.Text = "Adv Amt:";
                                lblRemAmt.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(sumtotAmount)));
                            }
                        }
                    }
                    else
                        MessageBox.Show("Record not Present.", "Warning");
                }
                else
                    MessageBox.Show(errormsg, "Warning");
                db.CloseConnection();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void btnprint1_Click(object sender, EventArgs e)
        {
            try
            {
                int CustId = 0, SaleId = 0,SiteId=0;
                string BankName = "", ReceiptType = "", CDate = "", FDate = "", TDate = "", PaymentMode = "";
                if (chkcustomername1.Checked == true)
                {
                    if (cmbcustomername1.Text != "Select")
                        CustId = Convert.ToInt32(cmbcustomername1.SelectedValue);
                }
                if (chkbankname.Checked == true)
                {
                    if (cmbbank2.Text != "Select")
                        BankName = Convert.ToString(cmbbank2.SelectedText);
                }
                if (chkSalesman.Checked == true)
                {
                    if (cmbSalesman.Text != "Select")
                        SaleId = Convert.ToInt32(cmbSalesman.SelectedValue);
                }
                if (chkSiteName.Checked == true)
                {
                    if (cmbSiteNameS.Text != "Select")
                        SiteId = Convert.ToInt32(cmbSiteNameS.SelectedValue);
                }
                if (chktype.Checked == true)
                {
                    if (cmbtype.Text != "Select")
                        ReceiptType = cmbtype.Text.ToString();
                }
                if (chkdate.Checked == true)
                    CDate = dtpdate.Value.ToString("dd/MM/yyyy");
                if (chkbetdate.Checked == true)
                {
                    if (dtpfromdate.Value.Date <= dtptodate.Value.Date)
                    {
                        CommonMethod com = new CommonMethod();
                        FDate = com.ValidFromDate(dtpfromdate.Value.Date);
                        TDate = com.ValidToDate(dtptodate.Value.Date);
                    }
                }
                if (booktype1 != "")
                    PaymentMode = booktype1;
                RptReceipt receiptrpt = new RptReceipt("Customer", CustId, SaleId, BankName, ReceiptType, CDate, FDate, TDate, typeOfLedger, PaymentMode, SiteId);
                receiptrpt.ShowDialog();
                receiptrpt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void GvreceiptInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["Type"].Value) == "Bill")
                {
                    string billno = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["BillNo"].Value);
                    frmCustomerBill custbill = new frmCustomerBill(billno);
                    custbill.ShowDialog();
                    btnsearch_Click(null, null);
                }
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["Type"].Value) == "OpeningBalance")
                    MessageBox.Show("Please Update this Data in Customer Information Form.", "Warning");
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["Type"].Value) == "Send To" || Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["Type"].Value) == "Receive From")
                    MessageBox.Show("Please Update this Data in Journal Entry Form.", "Warning");
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["Type"].Value) == "Receipt" || Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["Type"].Value) == "Adjustment")
                {
                    btndelete.Enabled = true;
                    receiptid = Convert.ToInt32(GvreceiptInfo.Rows[e.RowIndex].Cells["BId"].Value);
                    txtreceiptno.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["ReceiptNo"].Value);
                    try
                    {
                        dtpreceiptDate.Value = Convert.ToDateTime(GvreceiptInfo.Rows[e.RowIndex].Cells["ReceiptDate"].Value);
                    }
                    catch (Exception)
                    { }
                    cmbcustomer.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["Name"].Value);
                    if (e.RowIndex > 1)
                        txtTotalAmt.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex - 1].Cells["NetAmt"].Value);
                    else
                        txtTotalAmt.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["NetAmt"].Value);
                    txtPaidAmt.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["PaidAmt"].Value);
                    txtRemainigAmt.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["BalAmt"].Value);
                    cmbpaymentmode.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["Mode"].Value);
                    txtnarration.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["Narration"].Value);
                    if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["Mode"].Value) != "Cash")
                    {
                        txtchequeno.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["ChequeNo"].Value);
                        try
                        {
                            dtpchequedate.Value = Convert.ToDateTime(GvreceiptInfo.Rows[e.RowIndex].Cells["ChequeDate"].Value);
                        }
                        catch (Exception)
                        { }
                        RemainingPaymentRepository remRepo = new RemainingPaymentRepository();
                        RemainingPayment remdata = remRepo.GetById(receiptid);
                        BankStatementRepository BankStRepo = new BankStatementRepository();
                        BankStatement bdata = new BankStatement();
                        if (remdata.PaymentMode == "Cheque")
                        {
                            bdata = BankStRepo.GetAll().Where(t => t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.Chequeno && t.Deposits == remdata.PaidAmt && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            txtbankname.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["BankName"].Value);
                        }
                        else if (remdata.PaymentMode == "By Swipe")
                        {
                            bdata = BankStRepo.GetAll().Where(t => t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.Deposits == remdata.PaidAmt && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            txtbankname.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["BankName"].Value);
                        }
                        else
                        {
                            bdata = BankStRepo.GetAll().Where(t => t.Bankname == remdata.Bankname && t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.Chequeno && t.Deposits == remdata.PaidAmt && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            txtbankname.Text = "";
                        }
                        if (bdata != null)
                            cmbbank.Text = bdata.Bankname;
                    }
                    else
                    {
                        txtbankname.Text = "";
                        txtchequeno.Text = "";
                        cmbbank.Text = "Select";
                        dtpchequedate.Enabled = false;
                    }
                    customerid = Convert.ToInt32(cmbcustomer.SelectedValue);
                    customername = cmbcustomer.Text;
                    billreceiptno = txtreceiptno.Text;
                    RemainingPaymentRepository remRepo1 = new RemainingPaymentRepository();
                    RemainingPayment remdata1 = remRepo1.GetById(receiptid);
                    if (remdata1.ManualReceiptNo != "")
                        txtManualReceipt.Text = remdata1.ManualReceiptNo;
                    else
                        txtManualReceipt.Text = "";
                    btndelete.Enabled = true;
                    btnSave.Text = "Update";
                }
            }
            catch (Exception)
            { }
        }

        private void txtPaidAmt_Leave(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Enabled == true)
                    txtRemainigAmt.Text = Convert.ToString((Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text)));
            }
            catch (Exception)
            { }
        }

        private void chkbankname_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkbankname.Checked == true)
                {
                    chkcustomername1.Checked = false;
                    chktype.Checked = false;
                    chkdate.Checked = false;
                    chkbetdate.Checked = false;
                    customerdata1();
                }
            }
            catch (Exception)
            { }
        }

        private void chkcustomername1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkcustomername1.Checked == true)
                {
                    chkbankname.Checked = false;
                    chkdate.Checked = false;
                    chktype.Checked = false;
                    chkbetdate.Checked = false;
                    fillcombo();
                    fillcombo1(); fillSalesman();
                }
            }
            catch (Exception)
            { }
        }

        private void cmbcustomername1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void cmbbank2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void cmbtype_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chkdate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkdate.Checked == true)
                {
                    chkbankname.Checked = false;
                    chkcustomername1.Checked = false;
                    chkbetdate.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void chktype_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkdate.Checked == true)
                {
                    chkbankname.Checked = false;
                    chkcustomername1.Checked = false;
                    chkbetdate.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void chkbetdate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkbetdate.Checked == true)
                {
                    chkbankname.Checked = false;
                    chkdate.Checked = false;
                    chktype.Checked = false;
                    fillcombo();
                    fillcombo1(); fillSalesman();
                }
            }
            catch (Exception)
            { }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                RemainingPaymentRepository remRepo = new RemainingPaymentRepository();
                RemainingPayment remdata = remRepo.GetById(receiptid);
                remRepo.Remove(remdata);
                remRepo.Save();
                if (remdata.PaymentMode != "Cash")
                {
                    BankStatementRepository Brepo = new BankStatementRepository();
                    BankStatement bdata = new BankStatement();
                    if (remdata.PaymentMode == "Cheque")
                        bdata = Brepo.GetAll().Where(t => t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.Chequeno && t.Deposits == remdata.PaidAmt && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    else if (remdata.PaymentMode == "By Swipe")
                        bdata = Brepo.GetAll().Where(t => t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.TransactionType == "Receipt" && t.Deposits == remdata.PaidAmt && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    else
                        bdata = Brepo.GetAll().Where(t => t.Bankname == remdata.Bankname && t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.Deposits == remdata.PaidAmt && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (bdata != null)
                    {
                        BankStatement bsdata = Brepo.GetById(bdata.Id);
                        Brepo.Remove(bsdata);
                        Brepo.Save();
                    }
                }
                RemainingPaymentRepository remainingpayrepo1 = new RemainingPaymentRepository();
                RemainingPayment remainingdata1 = new RemainingPayment();
                remainingdata1 = remainingpayrepo1.GetAll().Where(t => t.CustomerId == Convert.ToInt32(remdata.CustomerId) && t.Id < remdata.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                decimal decremamt = Convert.ToDecimal(remainingdata1.RemainingAmt);

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
                MessageBox.Show("Record Deleted SuccessFully.", "Success");
                clear();
                btnSave.Text = "Save";
                btndelete.Enabled = false;
                btnsearch_Click(null, null);
                this.ActiveControl = btnnew;
            }
            catch (Exception)
            { }
        }

        private void frmCustomerLedger_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void chkAdjustVoucher_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAdjustVoucher.Checked)
                {
                    txtAdjustAmount.Visible = true;
                    txtAdjustAmount.Text = Convert.ToString(Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text));
                }
                else
                {
                    txtAdjustAmount.Visible = false;
                    txtAdjustAmount.Text = Convert.ToString(0);
                }
            }
            catch (Exception)
            { }
        }

        private void txtAdjustAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Enabled == true)
                {
                    decimal a = Convert.ToDecimal(txtTotalAmt.Text);
                    decimal b = Convert.ToDecimal(txtPaidAmt.Text) + Convert.ToDecimal(txtAdjustAmount.Text);
                    if (a >= b)
                    {
                        decimal c = (a - b);
                        txtRemainigAmt.Text = Convert.ToString(c);
                    }
                    else
                    {
                        txtAdjustAmount.Text = "0";
                        MessageBox.Show("AdjustMent Amount Less or equal to Total Amount.", "Warning");
                        this.ActiveControl = txtAdjustAmount;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtTotalAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTotalAmt.Text != "")
                {
                    txtPaidAmt.Text = "0";
                    txtRemainigAmt.Text = txtTotalAmt.Text;
                }
            }
            catch (Exception)
            { }
        }

        private void dtpreceiptDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtManualReceipt;
            }
            catch (Exception)
            { }
        }

        private void txtManualReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcustomer;
            }
            catch (Exception)
            { }
        }

        private void cmbcustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtPaidAmt;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmcustomer customer = new frmcustomer();
                        customer.ShowDialog();
                        customerdata();
                        this.ActiveControl = cmbcustomer;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtPaidAmt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbpaymentmode;
            }
            catch (Exception)
            { }
        }

        private void cmbpaymentmode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (cmbpaymentmode.Text == "Cash")
                        this.ActiveControl = txtnarration;
                    else
                        this.ActiveControl = txtbankname;
                }
            }
            catch (Exception)
            { }
        }

        private void txtbankname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbbank;
            }
            catch (Exception)
            { }
        }

        private void cmbbank_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpchequedate;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmBankInformation bank = new frmBankInformation();
                        bank.ShowDialog();
                        fillcombo();
                        this.ActiveControl = cmbbank;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void dtpchequedate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtchequeno;
            }
            catch (Exception)
            { }
        }

        private void txtchequeno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtnarration;
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

        private void chktype_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbtype;
            }
            catch (Exception)
            { }
        }

        private void chkdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpdate;
            }
            catch (Exception)
            { }
        }

        private void chkbankname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbbank2;
            }
            catch (Exception)
            { }
        }

        private void chkcustomername1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcustomername1;
            }
            catch (Exception)
            { }
        }

        private void chkbetdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpfromdate;
            }
            catch (Exception)
            { }
        }

        public void BalShtToCustLedger(string CCreditors, DateTime fdate, DateTime tdate)
        {
            try
            {
                GvreceiptInfo.DataSource = null;
                GvreceiptInfo.Rows.Clear();
                if (!GvreceiptInfo.Columns.Contains("Id"))
                {
                    DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
                    idColumn.Name = "Id";
                    idColumn.HeaderText = "Id";
                    GvreceiptInfo.Columns.Add(idColumn);
                }
                if (!GvreceiptInfo.Columns.Contains("BillNo"))
                {
                    DataGridViewTextBoxColumn BillidColumn = new DataGridViewTextBoxColumn();
                    BillidColumn.Name = "BillNo";
                    BillidColumn.HeaderText = "BillNo";
                    GvreceiptInfo.Columns.Add(BillidColumn);
                }
                if (!GvreceiptInfo.Columns.Contains("BillDate"))
                {
                    DataGridViewTextBoxColumn BillDateColumn = new DataGridViewTextBoxColumn();
                    BillDateColumn.Name = "BillDate";
                    BillDateColumn.HeaderText = "BillDate";
                    GvreceiptInfo.Columns.Add(BillDateColumn);
                }
                if (!GvreceiptInfo.Columns.Contains("ReceiptNo"))
                {
                    DataGridViewTextBoxColumn BillReceiptNoColumn = new DataGridViewTextBoxColumn();
                    BillReceiptNoColumn.Name = "ReceiptNo";
                    BillReceiptNoColumn.HeaderText = "ReceiptNo";
                    GvreceiptInfo.Columns.Add(BillReceiptNoColumn);
                }
                if (!GvreceiptInfo.Columns.Contains("ReceiptDate"))
                {
                    DataGridViewTextBoxColumn ReceiptDateColumn = new DataGridViewTextBoxColumn();
                    ReceiptDateColumn.Name = "ReceiptDate";
                    ReceiptDateColumn.HeaderText = "ReceiptDate";
                    GvreceiptInfo.Columns.Add(ReceiptDateColumn);
                }
                if (!GvreceiptInfo.Columns.Contains("Type"))
                {
                    DataGridViewTextBoxColumn TypeColumn = new DataGridViewTextBoxColumn();
                    TypeColumn.Name = "Type";
                    TypeColumn.HeaderText = "Type";
                    GvreceiptInfo.Columns.Add(TypeColumn);
                }
                if (!GvreceiptInfo.Columns.Contains("Name"))
                {
                    DataGridViewTextBoxColumn CustomerNameColumn = new DataGridViewTextBoxColumn();
                    CustomerNameColumn.Name = "Name";
                    CustomerNameColumn.HeaderText = "Name";
                    GvreceiptInfo.Columns.Add(CustomerNameColumn);
                }
                if (!GvreceiptInfo.Columns.Contains("NetAmt"))
                {
                    DataGridViewTextBoxColumn NetAmtColumn = new DataGridViewTextBoxColumn();
                    NetAmtColumn.Name = "NetAmt";
                    NetAmtColumn.HeaderText = "NetAmt";
                    GvreceiptInfo.Columns.Add(NetAmtColumn);
                }
                if (!GvreceiptInfo.Columns.Contains("PaidAmt"))
                {
                    DataGridViewTextBoxColumn PaidAmtColumn = new DataGridViewTextBoxColumn();
                    PaidAmtColumn.Name = "PaidAmt";
                    PaidAmtColumn.HeaderText = "PaidAmt";
                    GvreceiptInfo.Columns.Add(PaidAmtColumn);
                }
                if (!GvreceiptInfo.Columns.Contains("BalAmt"))
                {
                    DataGridViewTextBoxColumn RemAmtColumn = new DataGridViewTextBoxColumn();
                    RemAmtColumn.Name = "BalAmt";
                    RemAmtColumn.HeaderText = "BalAmt";
                    GvreceiptInfo.Columns.Add(RemAmtColumn);
                }
                if (!GvreceiptInfo.Columns.Contains("Mode"))
                {
                    DataGridViewTextBoxColumn PaymentModeColumn = new DataGridViewTextBoxColumn();
                    PaymentModeColumn.Name = "Mode";
                    PaymentModeColumn.HeaderText = "Mode";
                    GvreceiptInfo.Columns.Add(PaymentModeColumn);
                }
                if (!GvreceiptInfo.Columns.Contains("BankName"))
                {
                    DataGridViewTextBoxColumn BanknameColumn = new DataGridViewTextBoxColumn();
                    BanknameColumn.Name = "BankName";
                    BanknameColumn.HeaderText = "BankName";
                    GvreceiptInfo.Columns.Add(BanknameColumn);
                }
                if (!GvreceiptInfo.Columns.Contains("ChequeNo"))
                {
                    DataGridViewTextBoxColumn ChequeNoColumn = new DataGridViewTextBoxColumn();
                    ChequeNoColumn.Name = "ChequeNo";
                    ChequeNoColumn.HeaderText = "ChequeNo";
                    GvreceiptInfo.Columns.Add(ChequeNoColumn);
                }
                if (!GvreceiptInfo.Columns.Contains("ChequeDate"))
                {
                    DataGridViewTextBoxColumn ChequeDateColumn = new DataGridViewTextBoxColumn();
                    ChequeDateColumn.Name = "ChequeDate";
                    ChequeDateColumn.HeaderText = "ChequeDate";
                    GvreceiptInfo.Columns.Add(ChequeDateColumn);
                }
                if (!GvreceiptInfo.Columns.Contains("Narration"))
                {
                    DataGridViewTextBoxColumn NarrationColumn = new DataGridViewTextBoxColumn();
                    NarrationColumn.Name = "Narration";
                    NarrationColumn.HeaderText = "Narration";
                    GvreceiptInfo.Columns.Add(NarrationColumn);
                }
                string fromDate1 = cm.ValidFromDate(fdate.Date);
                string toDate1 = cm.ValidToDate(tdate.Date);
                DataTable dtNetAmt1; DataTable dtPaidAmt1;
                int j = 0;
                decimal CActAmt = 0, CRemAmt = 0, CNetAmt = 0, CPaidAmt = 0;
                if (CCreditors == "Customer Creditors")
                {
                    DataTable dt = db.GetTable("select distinct CustomerId,CustomerName from RemainingPayment where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int CustomerId = Convert.ToInt32(dt.Rows[i]["CustomerId"].ToString());
                            string Customername = Convert.ToString(dt.Rows[i]["CustomerName"].ToString());
                            dtNetAmt1 = db.GetTable("Select Isnull(sum(Isnull(NetAmt,0)),0) as NetAmt from RemainingPayment where CustomerId='" + CustomerId + "' and CustomerName='" + Customername + "' and CreateDate between '" + fromDate1 + "' and '" + toDate1 + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and Type!='Receipt' and CompId='" + CommonMethod.CompId + "'");
                            if (dtNetAmt1.Rows.Count > 0)
                                CNetAmt = Convert.ToDecimal(dtNetAmt1.Rows[0]["NetAmt"].ToString());
                            dtPaidAmt1 = db.GetTable("Select Isnull(sum(Isnull(PaidAmt,0)),0) as PaidAmt from RemainingPayment where CustomerId='" + CustomerId + "' and CustomerName='" + Customername + "' and CreateDate between '" + fromDate1 + "' and '" + toDate1 + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                            if (dtPaidAmt1.Rows.Count > 0)
                                CPaidAmt = Convert.ToDecimal(dtPaidAmt1.Rows[0]["PaidAmt"].ToString());
                            CRemAmt = CNetAmt - CPaidAmt;
                            if (CRemAmt < 0)
                            {
                                DataTable dtCustomer = db.GetTable("SELECT [ID],Id as BId, BillId as 'BillNo', CONVERT(VARCHAR(24),BillDate,103) as 'BillDate'," +
                                "[BillReceiptNo] as 'ReceiptNo' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'ReceiptDate',[Type],[Customername] as 'Name'," +
                                "[NetAmt] as 'NetAmt',[PaidAmt] as 'PaidAmt',[RemainingAmt] as 'BalAmt' ,[Paymentmode] as 'Mode',[bankname] as 'BankName'," +
                                "[ChequeNo] as 'ChequeNo',CONVERT(VARCHAR(24),[ChequeDate],103) as 'ChequeDate',Narration  FROM [RemainingPayment] " +
                                "where CustomerId='" + CustomerId + "' and Customername='" + Customername + "' and CreateDate >= '" + fromDate1 + "' " +
                                "and  CreateDate <= '" + toDate1 + "' and TransactionYear='" + CommonMethod.TransactionYear + "' " +
                                "and CompId=" + CommonMethod.CompId + " order by CONVERT(VARCHAR(24),[CreateDate],101)");
                                if (dtCustomer.Rows.Count > 0)
                                {
                                    for (int k = 0; k < dtCustomer.Rows.Count; k++)
                                    {
                                        GvreceiptInfo.Rows.Add();
                                        GvreceiptInfo.Rows[j].Cells["ID"].Value = dtCustomer.Rows[k]["ID"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["BId"].Value = dtCustomer.Rows[k]["BId"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["BillNo"].Value = dtCustomer.Rows[k]["BillNo"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["BillDate"].Value = dtCustomer.Rows[k]["BillDate"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["ReceiptNo"].Value = dtCustomer.Rows[k]["ReceiptNo"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["ReceiptDate"].Value = dtCustomer.Rows[k]["ReceiptDate"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["Type"].Value = dtCustomer.Rows[k]["Type"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["Name"].Value = dtCustomer.Rows[k]["Name"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["NetAmt"].Value = dtCustomer.Rows[k]["NetAmt"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["PaidAmt"].Value = dtCustomer.Rows[k]["PaidAmt"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["BalAmt"].Value = dtCustomer.Rows[k]["BalAmt"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["Mode"].Value = dtCustomer.Rows[k]["Mode"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["BankName"].Value = dtCustomer.Rows[k]["BankName"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["ChequeNo"].Value = dtCustomer.Rows[k]["ChequeNo"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["ChequeDate"].Value = dtCustomer.Rows[k]["ChequeDate"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["Narration"].Value = dtCustomer.Rows[k]["Narration"].ToString();
                                        j++;
                                    }
                                    GvreceiptInfo.Columns["ID"].Visible = false;
                                    GvreceiptInfo.Columns["BId"].Visible = false;
                                    GvreceiptInfo.Columns["BalAmt"].Visible = false;
                                    GvreceiptInfo.Columns["Mode"].Visible = false;
                                    GvreceiptInfo.Columns["Narration"].Visible = false;
                                    CActAmt = CActAmt + CRemAmt;
                                }
                            }
                        }
                    }
                }
                else
                {
                    DataTable dt = db.GetTable("select distinct CustomerId,CustomerName from RemainingPayment where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int CustomerId = Convert.ToInt32(dt.Rows[i]["CustomerId"].ToString());
                            string Customername = Convert.ToString(dt.Rows[i]["CustomerName"].ToString());
                            dtNetAmt1 = db.GetTable("Select Isnull(sum(Isnull(NetAmt,0)),0) as NetAmt from RemainingPayment where CustomerId='" + CustomerId + "' and CustomerName='" + Customername + "' and CreateDate between '" + fromDate1 + "' and '" + toDate1 + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and Type!='Receipt' and CompId='" + CommonMethod.CompId + "'");
                            if (dtNetAmt1.Rows.Count > 0)
                                CNetAmt = Convert.ToDecimal(dtNetAmt1.Rows[0]["NetAmt"].ToString());
                            dtPaidAmt1 = db.GetTable("Select Isnull(sum(Isnull(PaidAmt,0)),0) as PaidAmt from RemainingPayment where CustomerId='" + CustomerId + "' and CustomerName='" + Customername + "' and CreateDate between '" + fromDate1 + "' and '" + toDate1 + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                            if (dtPaidAmt1.Rows.Count > 0)
                                CPaidAmt = Convert.ToDecimal(dtPaidAmt1.Rows[0]["PaidAmt"].ToString());
                            CRemAmt = CNetAmt - CPaidAmt;
                            if (CRemAmt > 0)
                            {
                                DataTable dtCustomer = db.GetTable("SELECT [ID],Id as BId, BillId as 'BillNo', CONVERT(VARCHAR(24),BillDate,103) as 'BillDate',[BillReceiptNo] as 'ReceiptNo' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'ReceiptDate',[Type],[Customername] as 'Name' ,[NetAmt] as 'NetAmt',[PaidAmt] as 'PaidAmt',[RemainingAmt] as 'BalAmt' ,[Paymentmode] as 'Mode',[bankname] as 'BankName',[ChequeNo] as 'ChequeNo',CONVERT(VARCHAR(24),[ChequeDate],103) as 'ChequeDate',Narration  FROM [RemainingPayment] where CustomerId='" + CustomerId + "' and Customername='" + Customername + "' and CreateDate >= '" + fromDate1 + "' and  CreateDate <= '" + toDate1 + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " order by CONVERT(VARCHAR(24),[CreateDate],101)");
                                if (dtCustomer.Rows.Count > 0)
                                {
                                    for (int k = 0; k < dtCustomer.Rows.Count; k++)
                                    {
                                        GvreceiptInfo.Rows.Add();
                                        GvreceiptInfo.Rows[j].Cells["ID"].Value = dtCustomer.Rows[k]["ID"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["BId"].Value = dtCustomer.Rows[k]["BId"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["BillNo"].Value = dtCustomer.Rows[k]["BillNo"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["BillDate"].Value = dtCustomer.Rows[k]["BillDate"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["ReceiptNo"].Value = dtCustomer.Rows[k]["ReceiptNo"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["ReceiptDate"].Value = dtCustomer.Rows[k]["ReceiptDate"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["Type"].Value = dtCustomer.Rows[k]["Type"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["Name"].Value = dtCustomer.Rows[k]["Name"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["NetAmt"].Value = dtCustomer.Rows[k]["NetAmt"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["PaidAmt"].Value = dtCustomer.Rows[k]["PaidAmt"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["BalAmt"].Value = dtCustomer.Rows[k]["BalAmt"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["Mode"].Value = dtCustomer.Rows[k]["Mode"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["BankName"].Value = dtCustomer.Rows[k]["BankName"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["ChequeNo"].Value = dtCustomer.Rows[k]["ChequeNo"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["ChequeDate"].Value = dtCustomer.Rows[k]["ChequeDate"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["Narration"].Value = dtCustomer.Rows[k]["Narration"].ToString();
                                        j++;
                                    }
                                    GvreceiptInfo.Columns["ID"].Visible = false;
                                    GvreceiptInfo.Columns["BId"].Visible = false;
                                    GvreceiptInfo.Columns["BalAmt"].Visible = false;
                                    GvreceiptInfo.Columns["Mode"].Visible = false;
                                    GvreceiptInfo.Columns["Narration"].Visible = false;
                                    CActAmt = CActAmt + CRemAmt;
                                }
                            }
                        }
                    }
                }
                lblAmtText.Visible = true;
                lblRemAmt.Visible = true;
                if (CActAmt >= 0)
                {
                    lblAmtText.Text = "Rem Amt:";
                    lblRemAmt.Text = Convert.ToString(CActAmt);
                }
                else
                {
                    lblAmtText.Text = "Adv Amt:";
                    lblRemAmt.Text = Convert.ToString(Math.Abs(CActAmt));
                }
            }
            catch (Exception) { }
        }        

        private void txtnarration_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSave;
            }
            catch (Exception) { }
        }

        private void btnCustForm_Click(object sender, EventArgs e)
        {
            try
            {
                frmcustomer customer = new frmcustomer();
                customer.ShowDialog();
                customerdata();
            }
            catch (Exception) { }
        }

        private void btnCrDaySearch_Click(object sender, EventArgs e)
        {
            try
            {
                getcustomerdata();
            }
            catch (Exception)
            { }
        }

        private void cmbcustomer_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbcustomer.Text != "Select")
                {
                    if (cm.ValidCustomer(cmbcustomer.Text) == 1)
                    { }
                    else
                    {
                        MessageBox.Show("Customer Name is not Valid", "Warning");
                        cmbcustomer.SelectedIndex = 0;
                        this.ActiveControl = cmbcustomer;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnLedgerWithItem_Click(object sender, EventArgs e)
        {
            if (chkcustomername1.Checked == true)
            {
                if (cmbcustomername1.Text!="Select")
                {
                int CustId = Convert.ToInt32(cmbcustomername1.SelectedValue);
                frmGroupLedger receiptrpt = new frmGroupLedger(CustId);
                receiptrpt.ShowDialog();
                receiptrpt.Dispose();
                }
                else
                {
                    MessageBox.Show("Please Select Customer First!!!.", "Warning");
                }

            }
            else
            {
                MessageBox.Show("Please Select Customer Checkbox First!!!.", "Warning");
            }
        }
    }
}