using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.RachitControls;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class FrmFakeCustomerLedger1 : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        DateTime currentdate = Convert.ToDateTime(DateTime.Now);
        public int customerid, receiptid, colIndex;
        List<string> multipleReceiptPrintList = new List<string>();

        public FrmFakeCustomerLedger1()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            Maxcustid();
            fillcombo();
            fillcombo1();
            cmbbank.Enabled = false;
            txtchequeno.Enabled = false;
            dtpchequedate.Enabled = false;
            txtbankname.Enabled = false;
            customerdata();
            customerdata1();
            this.ActiveControl = cmbcustomer;
            hidelabel();
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
            {
                btnSave.Enabled = false;
                btnnew.Enabled = false;
            }
        }

        public FrmFakeCustomerLedger1(int custId)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            Maxcustid();
            fillcombo();
            fillcombo1();
            cmbbank.Enabled = false;
            txtchequeno.Enabled = false;
            dtpreceiptDate.Enabled = false;
            dtpchequedate.Enabled = false;
            txtbankname.Enabled = false;
            customerdata();
            customerdata1();
            this.ActiveControl = cmbcustomer;
            hidelabel();
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
            {
                btnSave.Enabled = false;
                btnnew.Enabled = false;
            }
        }

        public FrmFakeCustomerLedger1(DateTime from, DateTime to)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            Maxcustid();
            fillcombo();
            fillcombo1();
            cmbbank.Enabled = false;
            txtchequeno.Enabled = false;
            dtpreceiptDate.Enabled = false;
            dtpchequedate.Enabled = false;
            txtbankname.Enabled = false;
            customerdata();
            customerdata1();
            this.ActiveControl = cmbcustomer;
            hidelabel();
            loaddata(from, to);
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
            {
                btnSave.Enabled = false;
                btnnew.Enabled = false;
            }
        }

        public void loaddata(DateTime from, DateTime to)
        {
            try
            {
                hidelabel();
                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],BillId as 'Bill No.',CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type],[Customername] as 'Customer Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./ UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM [FakeRemainingPayment] where CreateDate>='" + cm.ValidFromDate(from.Date) + "' and CreateDate<='" + cm.ValidToDate(to.Date) + "' and Type='Receipt' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GvreceiptInfo.DataSource = dt;
                GvreceiptInfo.Refresh();
                if (GvreceiptInfo.Rows.Count != 0)
                {
                    GvreceiptInfo.Columns["ID"].Visible = false;
                    GvreceiptInfo.Columns["Bal. Amt"].Visible = false;
                    GvreceiptInfo.Columns["Mode"].Visible = false;
                }
                if (GvreceiptInfo.Rows.Count > 0)
                {
                    for (int i = 0; i < GvreceiptInfo.Rows.Count; i++)
                    {
                        DataGridViewRow selectedRow = GvreceiptInfo.Rows[i];
                        try
                        {
                            DateTime billdate = Convert.ToDateTime(Convert.ToString(selectedRow.Cells[3].Value));
                            // Difference in days, hours, and minutes.
                            TimeSpan ts = currentdate - billdate;
                            // Difference in days.
                            DataGridViewCell cell = selectedRow.Cells[0];
                            cell.Value = Convert.ToString(Convert.ToInt32(ts.Days).ToString());
                        }
                        catch (Exception)
                        { }
                    }
                }
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        public void loaddata(int custid)
        {
            try
            {
                hidelabel();
                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],BillId as 'Bill No.',CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type],[Customername] as 'Customer Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./ UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM [FakeRemainingPayment] where CustomerId='" + custid + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GvreceiptInfo.DataSource = dt;
                GvreceiptInfo.Refresh();
                if (GvreceiptInfo.Rows.Count != 0)
                {
                    GvreceiptInfo.Columns["ID"].Visible = false;
                    GvreceiptInfo.Columns["Bal. Amt"].Visible = false;
                    GvreceiptInfo.Columns["Mode"].Visible = false;
                    try
                    {
                        FakeRemainingPaymentRepository remainingpayrepo = new FakeRemainingPaymentRepository();
                        List<FakeRemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == custid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        var netamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == custid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && (t.Type != "Receipt")).Sum(t => t.NetAmt);
                        var Paidamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == custid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt);

                        if (remainingamt != null)
                        {
                            decimal a = Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt);
                            lblAmtText.Visible = true;
                            lblRemAmt.Visible = true;
                            if (a >= 0)
                            {
                                lblAmtText.Text = "Rem Amt:";
                                lblRemAmt.Text = Convert.ToString(a);
                            }
                            else
                            {
                                lblAmtText.Text = "Adv Amt:";
                                lblRemAmt.Text = Convert.ToString(Math.Abs(a));
                            }
                        }
                        remainingpayrepo.Dispose();
                    }
                    catch (Exception)
                    { }
                    this.ActiveControl = cmbcustomername1;
                }
                else
                {
                    MessageBox.Show("Record Not Found.", "Warning");
                    this.ActiveControl = cmbcustomername1;
                }
                if (GvreceiptInfo.Rows.Count > 0)
                {
                    for (int i = 0; i < GvreceiptInfo.Rows.Count; i++)
                    {
                        DataGridViewRow selectedRow = GvreceiptInfo.Rows[i];
                        string bdate = Convert.ToString(selectedRow.Cells[3].Value);
                        try
                        {
                            DateTime billdate = Convert.ToDateTime(bdate.ToString());
                            // Difference in days, hours, and minutes.
                            TimeSpan ts = currentdate - billdate;
                            // Difference in days.
                            DataGridViewCell cell = selectedRow.Cells[0];
                            cell.Value = Convert.ToString(Convert.ToInt32(ts.Days).ToString());
                        }
                        catch (Exception)
                        { }
                    }
                }
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        public void Maxcustid()
        {
            try
            {
                FakeRemainingPaymentRepository remainingpayrepo = new FakeRemainingPaymentRepository();
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
                //bind customer names
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
                //bind customer type
                SqlDataAdapter da = new SqlDataAdapter("Select Distinct(Type) from FakeRemainingPayment where CompId=" + CommonMethod.CompId + " order by Type", db.Connection);
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
                bankdata = bankrepo.GetAll().OrderByDescending(t => t.Bankname).ToList();
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

        public void fillcombo1()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Select Distinct(Bankname) from FakeRemainingPayment where Bankname!='' and CompId=" + CommonMethod.CompId + " order by Bankname", db.Connection);
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
                txtTotalAmt.Text = "";
                txtPaidAmt.Text = "";
                txtRemainigAmt.Text = "";
                cmbcustomer.SelectedIndex = 0;
                txtchequeno.Text = "";
                txtbankname.Text = "";
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
                if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
                {
                    btnSave.Enabled = false;
                    btnnew.Enabled = false;
                }
                btndelete.Enabled = false;
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
                        FakeRemainingPaymentRepository remainingpayrepo = new FakeRemainingPaymentRepository();
                        FakeRemainingPayment remainingdata = new FakeRemainingPayment();
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
                        if (cmbpaymentmode.Text == "Cash")
                        {
                            remainingdata.Bankname = "";
                            remainingdata.Chequeno = "";
                            remainingdata.Chequedate = null;
                            remainingpayrepo.Add(remainingdata);
                            remainingpayrepo.Save();
                            customerid = Convert.ToInt32(cmbcustomer.SelectedValue);
                            if (chkAdjustVoucher.Checked && Convert.ToDecimal(txtAdjustAmount.Text) > 0)
                            {
                                try
                                {
                                    FakeRemainingPaymentRepository remainingpayrepoadj = new FakeRemainingPaymentRepository();
                                    FakeRemainingPayment remainingdataadj = new FakeRemainingPayment();
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
                                    remainingpayrepoadj.Add(remainingdataadj);
                                    remainingpayrepoadj.Save();
                                }
                                catch (Exception)
                                { }
                            }
                            MessageBox.Show("Receipt Information Saved.", "Success");
                            clear();
                            fillcombo();
                            this.ActiveControl = btnprint;
                        }
                        else if (cmbpaymentmode.Text == "Cheque")
                        {
                            if (cmbbank.Text != "Select" && txtchequeno.Text != "" && txtbankname.Text != "")
                            {
                                remainingdata.Bankname = txtbankname.Text;
                                remainingdata.Chequeno = txtchequeno.Text;
                                remainingdata.Chequedate = dtpchequedate.Value;
                                remainingpayrepo.Add(remainingdata);
                                remainingpayrepo.Save();
                                customerid = Convert.ToInt32(cmbcustomer.SelectedValue);
                                if (chkAdjustVoucher.Checked && Convert.ToDecimal(txtAdjustAmount.Text) > 0)
                                {
                                    try
                                    {
                                        FakeRemainingPaymentRepository remainingpayrepoadj = new FakeRemainingPaymentRepository();
                                        FakeRemainingPayment remainingdataadj = new FakeRemainingPayment();
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
                                        remainingpayrepoadj.Add(remainingdataadj);
                                        remainingpayrepoadj.Save();
                                    }
                                    catch (Exception)
                                    { }
                                }
                                MessageBox.Show("Receipt Information Saved.", "Success");
                                clear();
                                fillcombo();
                                fillcombo1();
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
                                remainingpayrepo.Add(remainingdata);
                                remainingpayrepo.Save();
                                customerid = Convert.ToInt32(cmbcustomer.SelectedValue);

                                if (chkAdjustVoucher.Checked && Convert.ToDecimal(txtAdjustAmount.Text) > 0)
                                {
                                    try
                                    {
                                        FakeRemainingPaymentRepository remainingpayrepoadj = new FakeRemainingPaymentRepository();
                                        FakeRemainingPayment remainingdataadj = new FakeRemainingPayment();
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
                                        remainingpayrepoadj.Add(remainingdataadj);
                                        remainingpayrepoadj.Save();
                                    }
                                    catch (Exception)
                                    { }
                                }
                                MessageBox.Show("Receipt Information Saved.", "Success");
                                clear();
                                fillcombo();
                                fillcombo1();
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
                        FakeRemainingPaymentRepository remainingpayrepo = new FakeRemainingPaymentRepository();
                        FakeRemainingPayment remainingdata = new FakeRemainingPayment();
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
                        if (cmbpaymentmode.Text == "Cash")
                        {
                            remainingdata.Bankname = "";
                            remainingdata.Chequeno = "";
                            remainingdata.Chequedate = null;
                            remainingpayrepo.Edit(remainingdata);
                            remainingpayrepo.Save();
                            customerid = Convert.ToInt32(cmbcustomer.SelectedValue);
                            decimal decremamt = Convert.ToDecimal(txtRemainigAmt.Text);
                            FakeRemainingPaymentRepository remainingpayrepo2 = new FakeRemainingPaymentRepository();
                            List<FakeRemainingPayment> remainingdata2 = new List<FakeRemainingPayment>();
                            remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(customerid) && t.Id > receiptid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
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
                            if (chkAdjustVoucher.Checked && Convert.ToDecimal(txtAdjustAmount.Text) > 0)
                            {
                                try
                                {
                                    FakeRemainingPaymentRepository remainingpayrepoadj = new FakeRemainingPaymentRepository();
                                    FakeRemainingPayment remainingdataadj = new FakeRemainingPayment();
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
                                    remainingpayrepoadj.Add(remainingdataadj);
                                    remainingpayrepoadj.Save();
                                }
                                catch (Exception)
                                { }
                            }
                            MessageBox.Show("Receipt Information Saved.", "Success");
                            clear();
                            fillcombo();
                            fillcombo1();
                            this.ActiveControl = btnprint;
                        }
                        else if (cmbpaymentmode.Text == "Cheque")
                        {
                            if (cmbbank.Text != "Select" && txtchequeno.Text != "" && txtbankname.Text != "")
                            {
                                remainingdata.Bankname = txtbankname.Text;
                                remainingdata.Chequeno = txtchequeno.Text;
                                remainingdata.Chequedate = dtpchequedate.Value;
                                remainingpayrepo.Edit(remainingdata);
                                remainingpayrepo.Save();
                                customerid = Convert.ToInt32(cmbcustomer.SelectedValue);
                                decimal decremamt = Convert.ToDecimal(txtRemainigAmt.Text);

                                FakeRemainingPaymentRepository remainingpayrepo2 = new FakeRemainingPaymentRepository();
                                List<FakeRemainingPayment> remainingdata2 = new List<FakeRemainingPayment>();
                                remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(customerid) && t.Id > receiptid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
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
                                if (chkAdjustVoucher.Checked && Convert.ToDecimal(txtAdjustAmount.Text) > 0)
                                {
                                    try
                                    {
                                        FakeRemainingPaymentRepository remainingpayrepoadj = new FakeRemainingPaymentRepository();
                                        FakeRemainingPayment remainingdataadj = new FakeRemainingPayment();
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
                                        remainingpayrepoadj.Add(remainingdataadj);
                                        remainingpayrepoadj.Save();
                                    }
                                    catch (Exception)
                                    { }
                                }
                                MessageBox.Show("Receipt Information Saved.", "Success");
                                clear();
                                fillcombo();
                                fillcombo1();
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
                                remainingpayrepo.Edit(remainingdata);
                                remainingpayrepo.Save();
                                customerid = Convert.ToInt32(cmbcustomer.SelectedValue);
                                decimal decremamt = Convert.ToDecimal(txtRemainigAmt.Text);

                                FakeRemainingPaymentRepository remainingpayrepo2 = new FakeRemainingPaymentRepository();
                                List<FakeRemainingPayment> remainingdata2 = new List<FakeRemainingPayment>();
                                remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(customerid) && t.Id > receiptid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
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
                                if (chkAdjustVoucher.Checked && Convert.ToDecimal(txtAdjustAmount.Text) > 0)
                                {
                                    try
                                    {
                                        FakeRemainingPaymentRepository remainingpayrepoadj = new FakeRemainingPaymentRepository();
                                        FakeRemainingPayment remainingdataadj = new FakeRemainingPayment();
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
                                        remainingpayrepoadj.Add(remainingdataadj);
                                        remainingpayrepoadj.Save();
                                    }
                                    catch (Exception)
                                    { }
                                }
                                MessageBox.Show("Receipt Information Saved.", "Success");
                                clear();
                                fillcombo();
                                fillcombo1();
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
            catch (Exception) { }
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
                    label13.Text = "Cheque Date:";
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
                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],BillId as 'Bill No.',CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type],[Customername] as 'Customer Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM [FakeRemainingPayment] where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvreceiptInfo.DataSource = dt;
                GvreceiptInfo.Refresh();
                GvreceiptInfo.Columns["ID"].Visible = false;
                GvreceiptInfo.Columns["Bal. Amt"].Visible = false;
                GvreceiptInfo.Columns["Mode"].Visible = false;
                if (colIndex < 14)
                {
                    DataGridViewColumn bcol = new DataGridViewTextBoxColumn();
                    bcol.HeaderText = "Days";
                    colIndex = GvreceiptInfo.Columns.Add(bcol);

                    if (GvreceiptInfo.Rows.Count > 0)
                    {
                        for (int i = 0; i < GvreceiptInfo.Rows.Count; i++)
                        {
                            DataGridViewRow selectedRow = GvreceiptInfo.Rows[i];
                            try
                            {
                                DateTime billdate = Convert.ToDateTime(Convert.ToString(selectedRow.Cells[2].Value));
                                // Difference in days, hours, and minutes.
                                TimeSpan ts = currentdate - billdate;
                                // Difference in days.
                                DataGridViewCell cell = selectedRow.Cells[colIndex];
                                cell.Value = Convert.ToString(ts.Days.ToString());
                            }
                            catch (Exception)
                            { }
                        }
                    }
                }
                else
                {
                    if (GvreceiptInfo.Rows.Count > 0)
                    {
                        for (int i = 0; i < GvreceiptInfo.Rows.Count; i++)
                        {
                            DataGridViewRow selectedRow = GvreceiptInfo.Rows[i];
                            try
                            {
                                DateTime billdate = Convert.ToDateTime(Convert.ToString(selectedRow.Cells[3].Value));
                                // Difference in days, hours, and minutes.
                                TimeSpan ts = currentdate - billdate;
                                // Difference in days.
                                DataGridViewCell cell = selectedRow.Cells[0];
                                cell.Value = Convert.ToString(ts.Days.ToString());
                            }
                            catch (Exception)
                            { }
                        }
                    }
                }
                customerdata1();
                chkcustomername1.Checked = false;
                fillcombo();
                fillcombo1();
                chkbankname.Checked = false;
                clear();
                hidelabel();
                da.Dispose();
                dt.Dispose();
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
                FakeRemainingPaymentRepository remainingpayrepo = new FakeRemainingPaymentRepository();
                List<FakeRemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                decimal netamt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && (t.Type != "Receipt")).Sum(t => t.NetAmt));
                decimal Paidamt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt));

                if (remainingamt != null)
                    txtTotalAmt.Text = Convert.ToString(netamt - Paidamt);
                remainingpayrepo.Dispose();
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
                    {
                        decimal a = 0;
                        a = Convert.ToDecimal(txtTotalAmt.Text);
                        decimal b = Convert.ToDecimal(txtPaidAmt.Text);
                        txtRemainigAmt.Text = Convert.ToString((a - b));
                    }
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

        private void btngetall_Click(object sender, EventArgs e)
        {
            try
            {
                getcustomerdata();
                cmbcustomername1.Text = "";
                chkcustomername1.Checked = false;
                clear();
                hidelabel();
            }
            catch (Exception)
            { }

        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                hidelabel();
                SqlDataAdapter da = new SqlDataAdapter();
                if (chkcustomername1.Checked == true)
                {
                    if (cmbcustomername1.Text != "Select")
                    {
                        if (chktype.Checked == true)
                        {
                            if (cmbtype.Text != "Select")
                            {
                                da = new SqlDataAdapter("SELECT [ID],BillId as 'Bill No.',CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type],[Customername] as 'Customer Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM [FakeRemainingPayment] where CustomerId='" + cmbcustomername1.SelectedValue + "' and Type='" + cmbtype.Text + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                                this.ActiveControl = cmbtype;
                            }
                            else
                            {
                                MessageBox.Show("please Select Type.", "Warning");
                                this.ActiveControl = cmbtype;
                            }
                        }
                        else if (chkbetdate.Checked)
                        {
                            if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                            {
                                CommonMethod com = new CommonMethod();
                                da = new SqlDataAdapter("SELECT [ID], BillId as 'Bill No.', CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type],[Customername] as 'Customer Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM [FakeRemainingPayment] where CustomerId='" + cmbcustomername1.SelectedValue + "' and CreateDate >= '" + com.ValidFromDate(dtpfromdate.Value.Date) + "' and  CreateDate <= '" + com.ValidToDate(dtptodate.Value.Date) + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                            }
                            else
                            {
                                MessageBox.Show("To Date Should greater than from date.", "Warning");
                                this.ActiveControl = dtpfromdate;
                            }

                        }
                        else
                            da = new SqlDataAdapter("SELECT [ID],BillId as 'Bill No.',CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type],[Customername] as 'Customer Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM [FakeRemainingPayment] where CustomerId='" + cmbcustomername1.SelectedValue + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                    }
                    else
                    {
                        MessageBox.Show("please Select Customer Name.", "Warning");
                        this.ActiveControl = cmbcustomername1;
                    }
                }
                else if (chkbankname.Checked == true)
                {
                    if (cmbbank2.Text != "Select")
                    {
                        da = new SqlDataAdapter("SELECT [ID], BillId as 'Bill No.', CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type],[Customername] as 'Customer Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM [FakeRemainingPayment] where Bankname='" + cmbbank2.Text + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);

                    }
                    else
                    {
                        MessageBox.Show("Please Select Bank Name.", "Warning");
                        this.ActiveControl = cmbbank2;
                    }
                }
                else if (chkdate.Checked == true)
                {
                    if (chktype.Checked == true)
                    {
                        da = new SqlDataAdapter("SELECT [ID],BillId as 'Bill No.',CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type],[Customername] as 'Customer Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM [FakeRemainingPayment] where Type='" + cmbtype.Text + "' and CONVERT(VARCHAR(24),CreateDate,103)='" + dtpdate.Value.ToString("dd/MM/yyyy") + "'  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        this.ActiveControl = cmbtype;

                    }
                    else
                    {
                        da = new SqlDataAdapter("SELECT [ID],BillId as 'Bill No.',CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type],[Customername] as 'Customer Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM [FakeRemainingPayment] where  CONVERT(VARCHAR(24),CreateDate,103)='" + dtpdate.Value.ToString("dd/MM/yyyy") + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        this.ActiveControl = dtpdate;
                    }
                }
                else if (chktype.Checked == true)
                {
                    if (cmbtype.Text != "Select" && cmbtype.Text != "")
                    {
                        da = new SqlDataAdapter("SELECT [ID],BillId as 'Bill No.',CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type],[Customername] as 'Customer Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM [FakeRemainingPayment] where Type='" + cmbtype.Text + "'  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        this.ActiveControl = cmbtype;

                    }
                    else
                    {
                        MessageBox.Show("Please Select Type.", "Warning");
                        this.ActiveControl = cmbtype;
                    }
                }
                else if (chkbetdate.Checked)
                {
                    if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                    {
                        CommonMethod com = new CommonMethod();
                        da = new SqlDataAdapter("SELECT [ID], BillId as 'Bill No.', CONVERT(VARCHAR(24),BillDate,103) as 'Bill Date',[BillReceiptNo] as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type],[Customername] as 'Customer Name' ,[NetAmt] as 'Net Amt.',[PaidAmt] as 'Paid Amt.',[RemainingAmt] as 'Bal. Amt' ,[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM [FakeRemainingPayment] where CreateDate >= '" + com.ValidFromDate(dtpfromdate.Value.Date) + "' and  CreateDate <= '" + com.ValidToDate(dtptodate.Value.Date) + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);

                    }
                    else
                    {
                        MessageBox.Show("To Date Should greater than from date.", "Warning");
                        this.ActiveControl = dtpfromdate;
                    }
                }
                else
                    MessageBox.Show("Please Check Checkbox First.", "Warning");
                DataTable dt = new DataTable();
                da.Fill(dt);

                GvreceiptInfo.DataSource = dt;
                GvreceiptInfo.Refresh();
                if (GvreceiptInfo.Rows.Count != 0)
                {
                    GvreceiptInfo.Columns["ID"].Visible = false;
                    GvreceiptInfo.Columns["Bal. Amt"].Visible = false;
                    GvreceiptInfo.Columns["Mode"].Visible = false;
                    try
                    {
                        FakeRemainingPaymentRepository remainingpayrepo = new FakeRemainingPaymentRepository();
                        List<FakeRemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomername1.SelectedValue) && t.Type == Convert.ToString(cmbtype.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        var netamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomername1.SelectedValue) && t.Type == Convert.ToString(cmbtype.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && (t.Type != "Receipt")).Sum(t => t.NetAmt);
                        var Paidamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomername1.SelectedValue) && t.Type == Convert.ToString(cmbtype.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt);

                        if (remainingamt != null)
                        {
                            decimal a = Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt);
                            lblAmtText.Visible = true;
                            lblRemAmt.Visible = true;
                            if (a >= 0)
                            {
                                lblAmtText.Text = "Rem Amt:";
                                lblRemAmt.Text = Convert.ToString(a);
                            }
                            else
                            {
                                lblAmtText.Text = "Adv Amt:";
                                lblRemAmt.Text = Convert.ToString(Math.Abs(a));
                            }
                        }
                        remainingpayrepo.Dispose();
                    }
                    catch (Exception)
                    { }
                }
                else
                    MessageBox.Show("Record Not Found.", "Warning");
                if (GvreceiptInfo.Rows.Count > 0)
                {
                    for (int i = 0; i < GvreceiptInfo.Rows.Count; i++)
                    {
                        DataGridViewRow selectedRow = GvreceiptInfo.Rows[i];
                        try
                        {
                            DateTime billdate = Convert.ToDateTime(Convert.ToString(selectedRow.Cells[3].Value));
                            // Difference in days, hours, and minutes.
                            TimeSpan ts = currentdate - billdate;
                            // Difference in days.
                            DataGridViewCell cell = selectedRow.Cells[0];
                            cell.Value = Convert.ToString(ts.Days.ToString());
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                da.Dispose();
                dt.Dispose();

            }
            catch (Exception)
            { }

        }

        private void btnprint1_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkcustomername1.Checked == true && cmbcustomername1.Text != "Select")
                {
                    RptFakeBillReceipt receiptrpt = new RptFakeBillReceipt(Convert.ToInt32(cmbcustomername1.SelectedValue), cmbcustomername1.Text);
                    receiptrpt.ShowDialog();
                    receiptrpt.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void GvreceiptInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = e.ColumnIndex;
                int s1 = e.RowIndex;
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[5].Value) == "Bill")
                {
                    frmCustomerBill custbill = new frmCustomerBill(Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[1].Value));
                    custbill.ShowDialog();
                    btnsearch_Click(null, null);
                }
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[5].Value) == "OpeningBalance")
                    MessageBox.Show("Please Update this Data in Customer Information Form.", "Warning");
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[5].Value) == "Receipt")
                {
                    btndelete.Enabled = true;
                    receiptid = Convert.ToInt32(GvreceiptInfo.Rows[e.RowIndex].Cells[0].Value);
                    txtreceiptno.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[3].Value);
                    try
                    {
                        dtpreceiptDate.Value = Convert.ToDateTime(GvreceiptInfo.Rows[e.RowIndex].Cells[4].Value);
                    }
                    catch (Exception)
                    { }
                    cmbcustomer.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[6].Value);
                    txtTotalAmt.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex - 1].Cells[9].Value);
                    txtPaidAmt.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[8].Value);
                    txtRemainigAmt.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[9].Value);
                    cmbpaymentmode.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[10].Value);
                    if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[10].Value) != "Cash")
                    {
                        txtchequeno.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[12].Value);
                        try
                        {
                            dtpchequedate.Value = Convert.ToDateTime(GvreceiptInfo.Rows[e.RowIndex].Cells[13].Value);
                        }
                        catch (Exception)
                        { }

                        FakeRemainingPaymentRepository remRepo = new FakeRemainingPaymentRepository();
                        FakeRemainingPayment remdata = remRepo.GetById(receiptid);
                        BankStatementRepository BankStRepo = new BankStatementRepository();
                        BankStatement bdata = new BankStatement();
                        if (remdata.PaymentMode == "Cheque")
                        {
                            bdata = BankStRepo.GetAll().Where(t => t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.Chequeno && t.Deposits == remdata.PaidAmt && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            txtbankname.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[11].Value);
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
                    FakeRemainingPaymentRepository remRepo1 = new FakeRemainingPaymentRepository();
                    FakeRemainingPayment remdata1 = remRepo1.GetById(receiptid);
                    btndelete.Enabled = true;
                    btnSave.Text = "Update";
                }
            }
            catch (Exception) { }
        }

        private void txtPaidAmt_Leave(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Enabled == true)
                {
                    decimal a = 0;
                    a = Convert.ToDecimal(txtTotalAmt.Text);
                    decimal b = Convert.ToDecimal(txtPaidAmt.Text);
                    txtRemainigAmt.Text = Convert.ToString(a - b);
                }
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
                    fillcombo1();
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

        private void dtpdate_KeyDown(object sender, KeyEventArgs e)
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
                    fillcombo1();
                }
            }
            catch (Exception)
            { }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                FakeRemainingPaymentRepository remRepo = new FakeRemainingPaymentRepository();
                FakeRemainingPayment remdata = remRepo.GetById(receiptid);
                remRepo.Remove(remdata);
                remRepo.Save();

                FakeRemainingPaymentRepository remainingpayrepo1 = new FakeRemainingPaymentRepository();
                FakeRemainingPayment remainingdata1 = new FakeRemainingPayment();
                remainingdata1 = remainingpayrepo1.GetAll().Where(t => t.CustomerId == Convert.ToInt32(remdata.CustomerId) && t.Id < remdata.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                decimal decremamt = Convert.ToDecimal(remainingdata1.RemainingAmt);

                FakeRemainingPaymentRepository remainingpayrepo2 = new FakeRemainingPaymentRepository();
                List<FakeRemainingPayment> remainingdata2 = new List<FakeRemainingPayment>();
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

                btnsearch_Click(null, null);
                this.ActiveControl = btnnew;
            }
            catch (Exception)
            { }
        }

        private void FrmFakeCustomerLedger1_Load(object sender, EventArgs e)
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
                        this.ActiveControl = btnSave;
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
                    this.ActiveControl = btnSave;
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
    }
}