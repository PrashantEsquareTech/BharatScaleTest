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
    public partial class frmSupplierLedger : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        public int ACGId = 0, receiptid;
        DateTime fdate1, fdate2, tdate1, tdate2;
        string booktype1 = "", SCreditors1 = "", typeOfLedger = "";

        public frmSupplierLedger()
        {
            typeOfLedger = "All";
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            FillFirmNames();
            fillcombo();
            fillcombo1();
            cmbbank.Enabled = false;
            txtchequeno.Enabled = false;
            dtpchequedate.Enabled = false;
            maxreceiptid();
            customerdata();
            supplierdata();
            hidelabel();
            this.ActiveControl = cmbcustomer;
        }

        public frmSupplierLedger(string separateLedger)
        {
            typeOfLedger = separateLedger;
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            FillFirmNames();
            fillcombo();
            fillcombo1();
            cmbbank.Enabled = false;
            txtchequeno.Enabled = false;
            dtpchequedate.Enabled = false;
            maxreceiptid();
            customerdata();
            supplierdata();
            hidelabel();
            this.ActiveControl = cmbcustomer;
        }

        public frmSupplierLedger(int suppid)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            FillFirmNames();
            fillcombo();
            fillcombo1();
            cmbbank.Enabled = false;
            txtchequeno.Enabled = false;
            dtpreceiptDate.Enabled = false;
            maxreceiptid();
            customerdata();
            supplierdata();
            hidelabel();
            typeOfLedger = "All";
            chksuppliername1.Checked = true;
            cmbsuppliername1.SelectedValue = suppid;
            loaddata(suppid);
            this.ActiveControl = cmbsuppliername1;
        }

        public frmSupplierLedger(DateTime from, DateTime to, string booktype)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            FillFirmNames();
            fillcombo();
            fillcombo1();
            cmbbank.Enabled = false;
            txtchequeno.Enabled = false;
            dtpreceiptDate.Enabled = false;
            maxreceiptid();
            customerdata();
            supplierdata();
            hidelabel();
            fdate1 = from;
            tdate1 = to;
            booktype1 = booktype;
            loaddata(from, to, booktype);
            this.ActiveControl = cmbcustomer;
        }

        public frmSupplierLedger(string SCreditors, DateTime fdate, DateTime tdate)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            FillFirmNames();
            fillcombo();
            fillcombo1();
            cmbbank.Enabled = false;
            txtchequeno.Enabled = false;
            dtpreceiptDate.Enabled = false;
            maxreceiptid();
            customerdata();
            supplierdata();
            hidelabel();
            fdate2 = fdate;
            tdate2 = tdate;
            SCreditors1 = SCreditors;
            BalShtToSupplier(SCreditors, fdate, tdate);
        }

        public void loaddata(DateTime from, DateTime to, string booktype)
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
                command.Parameters.Add("@LedgerName", SqlDbType.NVarChar).Value = "Supplier";
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = Convert.ToInt32(cmbsuppliername1.SelectedValue);
                command.Parameters.Add("@ReceiptType", SqlDbType.NVarChar).Value = "Receipt";
                if (booktype == "Cash")
                    command.Parameters.Add("@PaymentMode", SqlDbType.NVarChar).Value = booktype;
                CommonMethod com = new CommonMethod();
                command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = com.ValidFromDate(from);
                command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = com.ValidToDate(to);
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
                        GvreceiptInfo.Columns["UserSeries"].Visible = false;
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

        public void loaddata(int suppid)
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
                command.Parameters.Add("@LedgerName", SqlDbType.NVarChar).Value = "Supplier";
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = Convert.ToInt32(suppid);
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
                        GvreceiptInfo.Columns["UserSeries"].Visible = false;
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

        public void maxreceiptid()
        {
            try
            {
                SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
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
                SupplierRepository supplierrepo = new SupplierRepository();
                List<SupplierInformation> customerdata = new List<SupplierInformation>();
                customerdata.Clear();
                if (CommonMethod.commProduct == true)
                    customerdata = supplierrepo.GetAll().OrderByDescending(t => t.SupplierName).ToList();
                else
                    customerdata = supplierrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.SupplierName).ToList();
                customerdata.Add(new SupplierInformation { SupplierName = "Select", SupplierCode = 0 });
                customerdata.Reverse();
                cmbcustomer.DataSource = customerdata;
                cmbcustomer.ValueMember = "SupplierCode";
                cmbcustomer.DisplayMember = "SupplierName";
                supplierrepo.Dispose();
                cmbcustomer.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void supplierdata()
        {
            try
            {
                SupplierRepository supplierrepo = new SupplierRepository();
                List<SupplierInformation> customerdata = new List<SupplierInformation>();
                customerdata.Clear();
                if (CommonMethod.commProduct == true)
                    customerdata = supplierrepo.GetAll().OrderByDescending(t => t.SupplierName).ToList();
                else
                    customerdata = supplierrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.SupplierName).ToList();
                customerdata.Add(new SupplierInformation { SupplierName = "Select", SupplierCode = 0 });
                customerdata.Reverse();
                cmbsuppliername1.DataSource = customerdata;
                cmbsuppliername1.ValueMember = "SupplierCode";
                cmbsuppliername1.DisplayMember = "SupplierName";
                supplierrepo.Dispose();
                cmbsuppliername1.SelectedIndex = 0;
                SqlDataAdapter da = new SqlDataAdapter("Select Distinct(Type) from SupplierRemainingPayment where CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
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

        public void FillFirmNames()
        {
            try
            {
                CompanyInformationRepository CompRepo = new CompanyInformationRepository();
                List<CompanyInformation> CompData = new List<CompanyInformation>();
                CompData.Clear();
                if (CommonMethod.commProduct == true)
                    CompData = CompRepo.GetAll();
                else
                    CompData = CompRepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).ToList();
                cmbFirmName.DataSource = CompData;
                cmbFirmName.ValueMember = "CompanyId";
                cmbFirmName.DisplayMember = "CompanyName";
                cmbFirmName.SelectedValue = CommonMethod.CompId;
                CompRepo.Dispose();
                cmbFirmName.SelectedIndex = 0;
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
                bankrepo.Dispose();
                cmbbank.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void fillcombo1()
        {
            try
            {
                BankRepository bankrepo = new BankRepository();
                List<BankInformation> bankdata = new List<BankInformation>();
                bankdata.Clear();
                bankdata = bankrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.Bankname).ToList();
                bankdata.Add(new BankInformation { Bankname = "Select", BankId = 0 });
                bankdata.Reverse();
                cmbbank2.DataSource = bankdata;
                cmbbank2.ValueMember = "BankId";
                cmbbank2.DisplayMember = "Bankname";
                bankrepo.Dispose();
                cmbbank2.SelectedIndex = 0;
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

        public void clear()
        {
            try
            {
                txtTotalAmt.Text = "";
                txtPaidAmt.Text = "";
                txtRemainigAmt.Text = "";
                customerdata();
                txtchequeno.Text = "";
                maxreceiptid();
                try
                {
                    dtpchequedate.Value = DateTime.Now.Date;
                    dtpreceiptDate.Value = DateTime.Now.Date;
                }
                catch (Exception)
                { }
                cmbpaymentmode.SelectedIndex = -1;
                cmbbank.Enabled = false;
                txtchequeno.Enabled = false;
                dtpchequedate.Enabled = false;
                FillFirmNames();
                fillcombo();
                fillcombo1();
                btnSave.Text = "Save";
                label15.Text = "Cheque No:";
                chkAdjustVoucher.Checked = false;
                txtAdjustAmount.Visible = false;
                txtnarration.Text = "";
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int Id;
                if (cmbcustomer.Text != "Select" && cmbpaymentmode.Text != "" && txtPaidAmt.Text != "")
                {
                    if (btnSave.Text == "Save")
                    {
                        SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                        SupplierRemainingPayment remainingdata = new SupplierRemainingPayment();
                        maxreceiptid();
                        remainingdata.BillReceiptNo = Convert.ToInt32(txtreceiptno.Text);
                        remainingdata.SupplierId = Convert.ToInt32(cmbcustomer.SelectedValue);
                        remainingdata.Suppliername = cmbcustomer.Text;
                        remainingdata.CreateDate = dtpreceiptDate.Value;
                        remainingdata.Type = "Receipt";
                        remainingdata.RemainingAmt = Convert.ToDecimal(txtRemainigAmt.Text);
                        remainingdata.NetAmt = Convert.ToDecimal(0);
                        remainingdata.PaidAmt = Convert.ToDecimal(txtPaidAmt.Text);
                        remainingdata.PaymentMode = cmbpaymentmode.Text;
                        remainingdata.TransactionYear = CommonMethod.TransactionYear;
                        remainingdata.CompId = Convert.ToInt32(cmbFirmName.SelectedValue);
                        remainingdata.Narration = txtnarration.Text;
                        remainingdata.Status = typeOfLedger;
                        remainingdata.ReceiptBy = CommonMethod.CompId;
                        if (cmbpaymentmode.Text == "Cash")
                        {
                            remainingdata.Bankname = "";
                            remainingdata.Chequeno = "";
                            remainingdata.Chequedate = null;
                            remainingpayrepo.Add(remainingdata);
                            remainingpayrepo.Save();
                            Id = remainingdata.Id;
                            if (chkAdjustVoucher.Checked && Convert.ToDecimal(txtAdjustAmount.Text) > 0)
                            {
                                try
                                {
                                    SupplierRemainingPaymentRepository remainingpayrepoadj = new SupplierRemainingPaymentRepository();
                                    SupplierRemainingPayment remainingdataadj = new SupplierRemainingPayment();
                                    remainingdataadj.SupplierId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                    remainingdataadj.Suppliername = cmbcustomer.Text;
                                    remainingdataadj.CreateDate = dtpreceiptDate.Value;
                                    remainingdataadj.Type = "Adjustment";
                                    decimal total = Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text);
                                    decimal rem = total - Convert.ToDecimal(txtAdjustAmount.Text);
                                    remainingdataadj.RemainingAmt = Convert.ToDecimal(rem);
                                    remainingdataadj.NetAmt = Convert.ToDecimal(0);
                                    remainingdataadj.PaidAmt = Convert.ToDecimal(txtAdjustAmount.Text);
                                    remainingdataadj.TransactionYear = CommonMethod.TransactionYear;
                                    remainingdataadj.CompId = CommonMethod.CompId;
                                    remainingdataadj.PaymentMode = cmbpaymentmode.Text;
                                    remainingdataadj.Bankname = "";
                                    remainingdataadj.Chequeno = "";
                                    remainingdataadj.Chequedate = null;
                                    remainingdata.Narration = txtnarration.Text;
                                    remainingdata.Status = typeOfLedger;
                                    remainingpayrepoadj.Add(remainingdataadj);
                                    remainingpayrepoadj.Save();
                                }
                                catch (Exception)
                                { }
                            }
                            decimal Rcptpaidamt = Convert.ToDecimal(txtPaidAmt.Text);
                            decimal billamt = 0, remamt = 0;
                            DataTable dt = new DataTable();
                            try
                            {
                                db.connect();
                                SqlCommand command = new SqlCommand("SPBillNoForLedger", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.Add("@Ledger", SqlDbType.VarChar).Value = 'P';
                                command.Parameters.Add("@Id", SqlDbType.VarChar).Value = cmbcustomer.SelectedValue;
                                command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(cmbFirmName.SelectedValue);

                                SqlDataAdapter adapter = new SqlDataAdapter(command);
                                adapter.Fill(dt);
                                db.CloseConnection();
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
                            if (dt.Rows.Count > 0)
                            {
                                SupplierRemainingPaymentRepository suprempayRepo = new SupplierRemainingPaymentRepository();
                                SupplierRemainingPayment suprempayData = new SupplierRemainingPayment();
                                suprempayData = suprempayRepo.GetAll().Where(t => t.Id < Id && t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.Type == "Receipt" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                if (suprempayData != null)
                                {
                                    if (suprempayData.RemainingAmt < 0)
                                        Rcptpaidamt = Rcptpaidamt + Math.Abs(Convert.ToDecimal(suprempayData.RemainingAmt));
                                }
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (Rcptpaidamt > 0)
                                    {
                                        if (dt.Rows[i][0].ToString() != "0")
                                        {
                                            BillWiseSupplierReceiptRepository BWSRRepo = new BillWiseSupplierReceiptRepository();
                                            BillWiseSupplierReceipt BWSRData = new BillWiseSupplierReceipt();
                                            BWSRData = BWSRRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                            if (BWSRData != null)
                                                billamt = Convert.ToDecimal(BWSRData.RemainigAmount);
                                            else
                                            {
                                                SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                                                SupplierRemainingPayment remdata = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                                billamt = Convert.ToDecimal(remdata.NetAmt) - Convert.ToDecimal(remdata.PaidAmt);
                                                remainingpayrepo1.Dispose();
                                            }
                                            BWSRRepo.Dispose();
                                        }
                                        else
                                        {
                                            BillWiseSupplierReceiptRepository BWSRRepo = new BillWiseSupplierReceiptRepository();
                                            BillWiseSupplierReceipt BWSRData = new BillWiseSupplierReceipt();
                                            BWSRData = BWSRRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(0) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                            if (BWSRData != null)
                                                billamt = Convert.ToDecimal(BWSRData.RemainigAmount);
                                            else
                                            {
                                                SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                                                SupplierRemainingPayment remainingamt = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && (t.Type == "OpeningBalance" || t.BillId == 0) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == Convert.ToInt32(cmbFirmName.SelectedValue)).FirstOrDefault();
                                                billamt = Convert.ToDecimal(remainingamt.NetAmt) - Convert.ToDecimal(remainingamt.PaidAmt);
                                                remainingpayrepo1.Dispose();
                                            }
                                            BWSRRepo.Dispose();
                                        }
                                        BillWiseSupplierReceiptRepository BWSRRepo1 = new BillWiseSupplierReceiptRepository();
                                        BillWiseSupplierReceipt BWSRData1 = new BillWiseSupplierReceipt();
                                        BWSRData1.SupplierReceiptNo = Convert.ToInt32(remainingdata.Id);
                                        BWSRData1.SupplierId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                        BWSRData1.BillId = Convert.ToInt32(dt.Rows[i][0].ToString());
                                        if (Rcptpaidamt >= billamt)
                                        {
                                            Rcptpaidamt = Rcptpaidamt - billamt;
                                            BWSRData1.PaidAmount = Convert.ToDecimal(billamt);
                                            BWSRData1.RemainigAmount = Convert.ToDecimal(0);
                                        }
                                        else
                                        {
                                            BWSRData1.PaidAmount = Convert.ToDecimal(Rcptpaidamt);
                                            remamt = billamt - Rcptpaidamt;
                                            Rcptpaidamt = 0;
                                            BWSRData1.RemainigAmount = Convert.ToDecimal(remamt);
                                        }
                                        BWSRData1.TransactionYear = CommonMethod.TransactionYear;
                                        BWSRData1.CompId = Convert.ToInt32(cmbFirmName.SelectedValue);
                                        BWSRRepo1.Add(BWSRData1);
                                        BWSRRepo1.Save();
                                    }
                                    else
                                        break;
                                }
                            }
                            MessageBox.Show("Supplier Receipt Information Saved.", "Success");
                            clear();
                            this.ActiveControl = btnnew;
                        }
                        else if (cmbpaymentmode.Text == "Cheque")
                        {
                            if (cmbbank.Text != "Select" && txtchequeno.Text != "")
                            {
                                remainingdata.Bankname = cmbbank.Text;
                                remainingdata.Chequeno = txtchequeno.Text;
                                remainingdata.Chequedate = dtpchequedate.Value;
                                remainingdata.Status = typeOfLedger;
                                remainingpayrepo.Add(remainingdata);
                                remainingpayrepo.Save();
                                Id = remainingdata.Id;
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
                                    bankstdata.Particulars = "Paid to" + " " + Convert.ToString(cmbcustomer.Text.Trim());
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    bankstdata.Withdrawals = Convert.ToDecimal(remainingdata.PaidAmt);
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankstdata.TransactionType = "Payment";
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
                                        SupplierRemainingPaymentRepository remainingpayrepoadj = new SupplierRemainingPaymentRepository();
                                        SupplierRemainingPayment remainingdataadj = new SupplierRemainingPayment();
                                        remainingdataadj.SupplierId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                        remainingdataadj.Suppliername = cmbcustomer.Text;
                                        remainingdataadj.CreateDate = dtpreceiptDate.Value;
                                        remainingdataadj.Type = "Adjustment";
                                        remainingdataadj.RemainingAmt = Convert.ToDecimal((Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text)) - Convert.ToDecimal(txtAdjustAmount.Text));
                                        remainingdataadj.NetAmt = Convert.ToDecimal(0);
                                        remainingdataadj.PaidAmt = Convert.ToDecimal(txtAdjustAmount.Text);
                                        remainingdataadj.TransactionYear = CommonMethod.TransactionYear;
                                        remainingdataadj.CompId = CommonMethod.CompId;
                                        remainingdataadj.PaymentMode = cmbpaymentmode.Text;
                                        remainingdataadj.Bankname = cmbbank.Text;
                                        remainingdataadj.Chequeno = txtchequeno.Text;
                                        remainingdataadj.Chequedate = dtpchequedate.Value;
                                        remainingdata.Narration = txtnarration.Text;
                                        remainingdata.Status = typeOfLedger;
                                        remainingpayrepoadj.Add(remainingdataadj);
                                        remainingpayrepoadj.Save();
                                    }
                                    catch (Exception)
                                    { }
                                }

                                decimal Rcptpaidamt = Convert.ToDecimal(txtPaidAmt.Text);
                                decimal billamt = 0, remamt = 0;
                                DataTable dt = new DataTable();
                                try
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPBillNoForLedger", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;

                                    command.Parameters.Add("@Ledger", SqlDbType.VarChar).Value = 'P';
                                    command.Parameters.Add("@Id", SqlDbType.VarChar).Value = cmbcustomer.SelectedValue;
                                    command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(cmbFirmName.SelectedValue);

                                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                                    adapter.Fill(dt);
                                    db.CloseConnection();
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                                if (dt.Rows.Count > 0)
                                {
                                    SupplierRemainingPaymentRepository suprempayRepo = new SupplierRemainingPaymentRepository();
                                    SupplierRemainingPayment suprempayData = new SupplierRemainingPayment();
                                    suprempayData = suprempayRepo.GetAll().Where(t => t.Id < Id && t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.Type == "Receipt" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                    if (suprempayData != null)
                                    {
                                        if (suprempayData.RemainingAmt < 0)
                                            Rcptpaidamt = Rcptpaidamt + Math.Abs(Convert.ToDecimal(suprempayData.RemainingAmt));
                                    }
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if (Rcptpaidamt > 0)
                                        {
                                            if (dt.Rows[i][0].ToString() != "0")
                                            {
                                                BillWiseSupplierReceiptRepository BWSRRepo = new BillWiseSupplierReceiptRepository();
                                                BillWiseSupplierReceipt BWSRData = new BillWiseSupplierReceipt();
                                                BWSRData = BWSRRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                                if (BWSRData != null)
                                                    billamt = Convert.ToDecimal(BWSRData.RemainigAmount);
                                                else
                                                {
                                                    SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                                                    SupplierRemainingPayment remdata = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                                    billamt = Convert.ToDecimal(remdata.NetAmt) - Convert.ToDecimal(remdata.PaidAmt);
                                                    remainingpayrepo1.Dispose();
                                                }
                                                BWSRRepo.Dispose();
                                            }
                                            else
                                            {
                                                BillWiseSupplierReceiptRepository BWSRRepo = new BillWiseSupplierReceiptRepository();
                                                BillWiseSupplierReceipt BWSRData = new BillWiseSupplierReceipt();
                                                BWSRData = BWSRRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(0) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                                if (BWSRData != null)
                                                    billamt = Convert.ToDecimal(BWSRData.RemainigAmount);
                                                else
                                                {
                                                    SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                                                    SupplierRemainingPayment remainingamt = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && (t.Type == "OpeningBalance" || t.BillId == 0) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == Convert.ToInt32(cmbFirmName.SelectedValue)).FirstOrDefault();
                                                    billamt = Convert.ToDecimal(remainingamt.NetAmt) - Convert.ToDecimal(remainingamt.PaidAmt);
                                                    remainingpayrepo1.Dispose();
                                                }
                                                BWSRRepo.Dispose();
                                            }
                                            BillWiseSupplierReceiptRepository BWSRRepo1 = new BillWiseSupplierReceiptRepository();
                                            BillWiseSupplierReceipt BWSRData1 = new BillWiseSupplierReceipt();
                                            BWSRData1.SupplierReceiptNo = Convert.ToInt32(remainingdata.Id);
                                            BWSRData1.SupplierId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                            BWSRData1.BillId = Convert.ToInt32(dt.Rows[i][0].ToString());

                                            if (Rcptpaidamt >= billamt)
                                            {
                                                Rcptpaidamt = Rcptpaidamt - billamt;
                                                BWSRData1.PaidAmount = Convert.ToDecimal(billamt);
                                                BWSRData1.RemainigAmount = Convert.ToDecimal(0);
                                            }
                                            else
                                            {
                                                BWSRData1.PaidAmount = Convert.ToDecimal(Rcptpaidamt);
                                                remamt = billamt - Rcptpaidamt;
                                                Rcptpaidamt = 0;
                                                BWSRData1.RemainigAmount = Convert.ToDecimal(remamt);
                                            }
                                            BWSRData1.TransactionYear = CommonMethod.TransactionYear;
                                            BWSRData1.CompId = Convert.ToInt32(cmbFirmName.SelectedValue);
                                            BWSRRepo1.Add(BWSRData1);
                                            BWSRRepo1.Save();
                                        }
                                        else
                                            break;
                                    }
                                }
                                MessageBox.Show("Supplier Receipt Information Saved.", "Success");
                                clear();
                                this.ActiveControl = btnnew;
                            }
                            else
                            {
                                if (cmbbank.Text == "Select")
                                {
                                    MessageBox.Show("Select Bank Name.", "Warning");
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
                                remainingdata.Status = typeOfLedger;
                                remainingpayrepo.Add(remainingdata);
                                remainingpayrepo.Save();
                                Id = remainingdata.Id;
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
                                    bankstdata.Particulars = "Paid to" + " " + Convert.ToString(cmbcustomer.Text.Trim());
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    bankstdata.Withdrawals = Convert.ToDecimal(remainingdata.PaidAmt);
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankstdata.TransactionType = "Payment";
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
                                        SupplierRemainingPaymentRepository remainingpayrepoadj = new SupplierRemainingPaymentRepository();
                                        SupplierRemainingPayment remainingdataadj = new SupplierRemainingPayment();
                                        remainingdataadj.SupplierId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                        remainingdataadj.Suppliername = cmbcustomer.Text;
                                        remainingdataadj.CreateDate = dtpreceiptDate.Value;
                                        remainingdataadj.Type = "Adjustment";
                                        remainingdataadj.RemainingAmt = Convert.ToDecimal((Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text)) - Convert.ToDecimal(txtAdjustAmount.Text));
                                        remainingdataadj.NetAmt = Convert.ToDecimal(0);
                                        remainingdataadj.PaidAmt = Convert.ToDecimal(txtAdjustAmount.Text);
                                        remainingdataadj.TransactionYear = CommonMethod.TransactionYear;
                                        remainingdataadj.CompId = CommonMethod.CompId;
                                        remainingdataadj.PaymentMode = cmbpaymentmode.Text;
                                        remainingdataadj.Bankname = cmbbank.Text;
                                        remainingdataadj.Chequeno = txtchequeno.Text;
                                        remainingdataadj.Chequedate = dtpchequedate.Value;
                                        remainingdataadj.Narration = txtnarration.Text;
                                        remainingdataadj.Status = typeOfLedger;
                                        remainingpayrepoadj.Add(remainingdataadj);
                                        remainingpayrepoadj.Save();
                                    }
                                    catch (Exception)
                                    { }
                                }
                                decimal Rcptpaidamt = Convert.ToDecimal(txtPaidAmt.Text);
                                decimal billamt = 0, remamt = 0;
                                DataTable dt = new DataTable();
                                try
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPBillNoForLedger", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;

                                    command.Parameters.Add("@Ledger", SqlDbType.VarChar).Value = 'P';
                                    command.Parameters.Add("@Id", SqlDbType.VarChar).Value = cmbcustomer.SelectedValue;
                                    command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(cmbFirmName.SelectedValue);

                                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                                    adapter.Fill(dt);
                                    db.CloseConnection();
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                                if (dt.Rows.Count > 0)
                                {
                                    SupplierRemainingPaymentRepository suprempayRepo = new SupplierRemainingPaymentRepository();
                                    SupplierRemainingPayment suprempayData = new SupplierRemainingPayment();
                                    suprempayData = suprempayRepo.GetAll().Where(t => t.Id < Id && t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.Type == "Receipt" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                    if (suprempayData != null)
                                    {
                                        if (suprempayData.RemainingAmt < 0)
                                            Rcptpaidamt = Rcptpaidamt + Math.Abs(Convert.ToDecimal(suprempayData.RemainingAmt));
                                    }
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if (Rcptpaidamt > 0)
                                        {
                                            if (dt.Rows[i][0].ToString() != "0")
                                            {
                                                BillWiseSupplierReceiptRepository BWSRRepo = new BillWiseSupplierReceiptRepository();
                                                BillWiseSupplierReceipt BWSRData = new BillWiseSupplierReceipt();
                                                BWSRData = BWSRRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                                if (BWSRData != null)
                                                    billamt = Convert.ToDecimal(BWSRData.RemainigAmount);
                                                else
                                                {
                                                    SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                                                    SupplierRemainingPayment remdata = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                                    billamt = Convert.ToDecimal(remdata.NetAmt) - Convert.ToDecimal(remdata.PaidAmt);
                                                    remainingpayrepo1.Dispose();
                                                }
                                                BWSRRepo.Dispose();
                                            }
                                            else
                                            {
                                                BillWiseSupplierReceiptRepository BWSRRepo = new BillWiseSupplierReceiptRepository();
                                                BillWiseSupplierReceipt BWSRData = new BillWiseSupplierReceipt();
                                                BWSRData = BWSRRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(0) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                                if (BWSRData != null)
                                                    billamt = Convert.ToDecimal(BWSRData.RemainigAmount);
                                                else
                                                {
                                                    SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                                                    SupplierRemainingPayment remainingamt = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && (t.Type == "OpeningBalance" || t.BillId == 0) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == Convert.ToInt32(cmbFirmName.SelectedValue)).FirstOrDefault();
                                                    billamt = Convert.ToDecimal(remainingamt.NetAmt) - Convert.ToDecimal(remainingamt.PaidAmt);
                                                    remainingpayrepo1.Dispose();
                                                }
                                                BWSRRepo.Dispose();
                                            }
                                            BillWiseSupplierReceiptRepository BWSRRepo1 = new BillWiseSupplierReceiptRepository();
                                            BillWiseSupplierReceipt BWSRData1 = new BillWiseSupplierReceipt();
                                            BWSRData1.SupplierReceiptNo = Convert.ToInt32(remainingdata.Id);
                                            BWSRData1.SupplierId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                            BWSRData1.BillId = Convert.ToInt32(dt.Rows[i][0].ToString());
                                            if (Rcptpaidamt >= billamt)
                                            {
                                                Rcptpaidamt = Rcptpaidamt - billamt;
                                                BWSRData1.PaidAmount = Convert.ToDecimal(billamt);
                                                BWSRData1.RemainigAmount = Convert.ToDecimal(0);
                                            }
                                            else
                                            {
                                                BWSRData1.PaidAmount = Convert.ToDecimal(Rcptpaidamt);
                                                remamt = billamt - Rcptpaidamt;
                                                Rcptpaidamt = 0;
                                                BWSRData1.RemainigAmount = Convert.ToDecimal(remamt);
                                            }
                                            BWSRData1.TransactionYear = CommonMethod.TransactionYear;
                                            BWSRData1.CompId = Convert.ToInt32(cmbFirmName.SelectedValue);
                                            BWSRRepo1.Add(BWSRData1);
                                            BWSRRepo1.Save();
                                        }
                                        else
                                            break;
                                    }
                                }
                                MessageBox.Show("Supplier Receipt Information Saved.", "Success");
                                clear();
                                this.ActiveControl = btnnew;
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
                            remainingpayrepo.Dispose();
                        }
                    }
                    else
                    {
                        decimal OldPaidAmt = 0;
                        SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                        SupplierRemainingPayment remainingdata = new SupplierRemainingPayment();
                        remainingdata = remainingpayrepo.GetById(receiptid);
                        OldPaidAmt = Convert.ToDecimal(remainingdata.PaidAmt);
                        remainingdata.BillReceiptNo = Convert.ToInt32(txtreceiptno.Text);
                        remainingdata.SupplierId = Convert.ToInt32(cmbcustomer.SelectedValue);
                        remainingdata.Suppliername = cmbcustomer.Text;
                        remainingdata.CreateDate = dtpreceiptDate.Value;
                        remainingdata.Type = "Receipt";
                        remainingdata.RemainingAmt = Convert.ToDecimal(txtRemainigAmt.Text);
                        remainingdata.NetAmt = Convert.ToDecimal(0);
                        remainingdata.PaidAmt = Convert.ToDecimal(txtPaidAmt.Text);
                        remainingdata.PaymentMode = cmbpaymentmode.Text;
                        remainingdata.TransactionYear = CommonMethod.TransactionYear;
                        remainingdata.CompId = Convert.ToInt32(cmbFirmName.SelectedValue);
                        remainingdata.Narration = txtnarration.Text;
                        remainingdata.Status = typeOfLedger;
                        remainingdata.ReceiptBy = CommonMethod.CompId;
                        Id = remainingdata.Id;
                        if (cmbpaymentmode.Text == "Cash")
                        {
                            remainingdata.Bankname = "";
                            remainingdata.Chequeno = "";
                            remainingdata.Chequedate = null;
                            remainingpayrepo.Edit(remainingdata);
                            remainingpayrepo.Save();
                            decimal decremamt = Convert.ToDecimal(txtRemainigAmt.Text);
                            SupplierRemainingPaymentRepository remainingpayrepo2 = new SupplierRemainingPaymentRepository();
                            List<SupplierRemainingPayment> remainingdata2 = new List<SupplierRemainingPayment>();
                            remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.Id > receiptid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();

                            int j = 0;
                            foreach (var item in remainingdata2)
                            {
                                item.RemainingAmt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                                decremamt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                                remainingpayrepo2.Edit(item);
                                remainingpayrepo2.Save();
                                j++;
                            }
                            if (chkAdjustVoucher.Checked && Convert.ToDecimal(txtAdjustAmount.Text) > 0)
                            {
                                try
                                {
                                    SupplierRemainingPaymentRepository remainingpayrepoadj = new SupplierRemainingPaymentRepository();
                                    SupplierRemainingPayment remainingdataadj = new SupplierRemainingPayment();
                                    remainingdataadj.SupplierId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                    remainingdataadj.Suppliername = cmbcustomer.Text;
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
                                    remainingdataadj.Narration = txtnarration.Text;
                                    remainingdataadj.Status = typeOfLedger;
                                    remainingpayrepoadj.Add(remainingdataadj);
                                    remainingpayrepoadj.Save();
                                }
                                catch (Exception)
                                { }
                            }
                            if (OldPaidAmt != Convert.ToDecimal(txtPaidAmt.Text))
                            {
                                BillWiseSupplierReceiptRepository BWSRRepoOld = new BillWiseSupplierReceiptRepository();
                                List<BillWiseSupplierReceipt> BWSRDataOld = new List<BillWiseSupplierReceipt>();
                                BWSRDataOld = BWSRRepoOld.GetAll().Where(t => t.SupplierReceiptNo == receiptid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                if (BWSRDataOld != null)
                                {
                                    foreach (var item in BWSRDataOld)
                                    {
                                        BWSRRepoOld.Remove(item);
                                        BWSRRepoOld.Save();
                                    }
                                }
                                decimal Rcptpaidamt = Convert.ToDecimal(txtPaidAmt.Text);
                                decimal billamt = 0, remamt = 0;
                                DataTable dt = new DataTable();
                                try
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPBillNoForLedger", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;

                                    command.Parameters.Add("@Ledger", SqlDbType.VarChar).Value = 'P';
                                    command.Parameters.Add("@Id", SqlDbType.VarChar).Value = cmbcustomer.SelectedValue;
                                    command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(cmbFirmName.SelectedValue);

                                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                                    adapter.Fill(dt);
                                    db.CloseConnection();
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                                if (dt.Rows.Count > 0)
                                {
                                    SupplierRemainingPaymentRepository suprempayRepo = new SupplierRemainingPaymentRepository();
                                    SupplierRemainingPayment suprempayData = new SupplierRemainingPayment();
                                    suprempayData = suprempayRepo.GetAll().Where(t => t.Id < Id && t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.Type == "Receipt" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                    if (suprempayData != null)
                                    {
                                        if (suprempayData.RemainingAmt < 0)
                                            Rcptpaidamt = Rcptpaidamt + Math.Abs(Convert.ToDecimal(suprempayData.RemainingAmt));
                                    }
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        if (Rcptpaidamt > 0)
                                        {
                                            if (dt.Rows[i][0].ToString() != "0")
                                            {
                                                BillWiseSupplierReceiptRepository BWSRRepo = new BillWiseSupplierReceiptRepository();
                                                BillWiseSupplierReceipt BWSRData = new BillWiseSupplierReceipt();
                                                BWSRData = BWSRRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                                if (BWSRData != null)
                                                    billamt = Convert.ToDecimal(BWSRData.RemainigAmount);
                                                else
                                                {
                                                    SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                                                    SupplierRemainingPayment remdata = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                                    billamt = Convert.ToDecimal(remdata.NetAmt) - Convert.ToDecimal(remdata.PaidAmt);
                                                    remainingpayrepo1.Dispose();
                                                }
                                                BWSRRepo.Dispose();
                                            }
                                            else
                                            {
                                                BillWiseSupplierReceiptRepository BWSRRepo = new BillWiseSupplierReceiptRepository();
                                                BillWiseSupplierReceipt BWSRData = new BillWiseSupplierReceipt();
                                                BWSRData = BWSRRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(0) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                                if (BWSRData != null)
                                                    billamt = Convert.ToDecimal(BWSRData.RemainigAmount);
                                                else
                                                {
                                                    SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                                                    SupplierRemainingPayment remainingamt = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && (t.Type == "OpeningBalance" || t.BillId == 0) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == Convert.ToInt32(cmbFirmName.SelectedValue)).FirstOrDefault();
                                                    billamt = Convert.ToDecimal(remainingamt.NetAmt) - Convert.ToDecimal(remainingamt.PaidAmt);
                                                    remainingpayrepo1.Dispose();
                                                }
                                                BWSRRepo.Dispose();
                                            }
                                            BillWiseSupplierReceiptRepository BWSRRepo1 = new BillWiseSupplierReceiptRepository();
                                            BillWiseSupplierReceipt BWSRData1 = new BillWiseSupplierReceipt();
                                            BWSRData1.SupplierReceiptNo = Convert.ToInt32(remainingdata.Id);
                                            BWSRData1.SupplierId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                            BWSRData1.BillId = Convert.ToInt32(dt.Rows[i][0].ToString());

                                            if (Rcptpaidamt >= billamt)
                                            {
                                                Rcptpaidamt = Rcptpaidamt - billamt;
                                                BWSRData1.PaidAmount = Convert.ToDecimal(billamt);
                                                BWSRData1.RemainigAmount = Convert.ToDecimal(0);
                                            }
                                            else
                                            {
                                                BWSRData1.PaidAmount = Convert.ToDecimal(Rcptpaidamt);
                                                remamt = billamt - Rcptpaidamt;
                                                Rcptpaidamt = 0;
                                                BWSRData1.RemainigAmount = Convert.ToDecimal(remamt);
                                            }
                                            BWSRData1.TransactionYear = CommonMethod.TransactionYear;
                                            BWSRData1.CompId = Convert.ToInt32(cmbFirmName.SelectedValue);
                                            BWSRRepo1.Add(BWSRData1);
                                            BWSRRepo1.Save();
                                        }
                                        else
                                            break;
                                    }
                                }
                            }
                            MessageBox.Show("Supplier Receipt Information Saved.", "Success");
                            clear();
                            this.ActiveControl = btnnew;
                        }
                        else if (cmbpaymentmode.Text == "Cheque")
                        {
                            if (cmbbank.Text != "Select" && txtchequeno.Text != "")
                            {
                                remainingdata.Bankname = cmbbank.Text;
                                remainingdata.Chequeno = txtchequeno.Text;
                                remainingdata.Chequedate = dtpchequedate.Value;
                                try
                                {
                                    SupplierRemainingPaymentRepository remRepo = new SupplierRemainingPaymentRepository();
                                    SupplierRemainingPayment remdata = remRepo.GetById(receiptid);
                                    BankStatementRepository BankStRepo = new BankStatementRepository();
                                    BankRepository bankInfoRepo = new BankRepository();
                                    var bankData = bankInfoRepo.GetAll().Where(t => t.BankId == Convert.ToInt32(cmbbank.SelectedValue) && t.Bankname == cmbbank.Text).FirstOrDefault();
                                    if (bankData != null)
                                        ACGId = Convert.ToInt32(bankData.ACGId);
                                    var bdata = BankStRepo.GetAll().Where(t => t.Bankname == remdata.Bankname && t.CompId == CommonMethod.CompId && t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.Chequeno && t.Withdrawals == Convert.ToDecimal(remdata.PaidAmt)).FirstOrDefault();
                                    BankStatementRepository bankrepo = new BankStatementRepository();
                                    BankStatement bankstdata = new BankStatement();
                                    bankstdata.BankId = Convert.ToInt32(cmbbank.SelectedValue);
                                    bankstdata.Bankname = Convert.ToString(cmbbank.Text);
                                    bankstdata.TransactionDate = dtpreceiptDate.Value;
                                    bankstdata.Particulars = "Paid to" + " " + Convert.ToString(cmbcustomer.Text.Trim());
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    bankstdata.Withdrawals = Convert.ToDecimal(txtPaidAmt.Text);
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankstdata.TransactionType = "Payment";
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
                                decimal decremamt = Convert.ToDecimal(txtRemainigAmt.Text);
                                SupplierRemainingPaymentRepository remainingpayrepo2 = new SupplierRemainingPaymentRepository();
                                List<SupplierRemainingPayment> remainingdata2 = new List<SupplierRemainingPayment>();
                                remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.Id > receiptid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();

                                int j = 0;
                                foreach (var item in remainingdata2)
                                {
                                    item.RemainingAmt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                                    decremamt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                                    remainingpayrepo2.Edit(item);
                                    remainingpayrepo2.Save();
                                    j++;
                                }
                                if (chkAdjustVoucher.Checked && Convert.ToDecimal(txtAdjustAmount.Text) > 0)
                                {
                                    try
                                    {
                                        SupplierRemainingPaymentRepository remainingpayrepoadj = new SupplierRemainingPaymentRepository();
                                        SupplierRemainingPayment remainingdataadj = new SupplierRemainingPayment();
                                        remainingdataadj.SupplierId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                        remainingdataadj.Suppliername = cmbcustomer.Text;
                                        remainingdataadj.CreateDate = dtpreceiptDate.Value;
                                        remainingdataadj.Type = "Adjustment";
                                        remainingdataadj.RemainingAmt = Convert.ToDecimal((Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text)) - Convert.ToDecimal(txtAdjustAmount.Text));
                                        remainingdataadj.NetAmt = Convert.ToDecimal(0);
                                        remainingdataadj.PaidAmt = Convert.ToDecimal(txtAdjustAmount.Text);
                                        remainingdataadj.TransactionYear = CommonMethod.TransactionYear;
                                        remainingdataadj.CompId = CommonMethod.CompId;
                                        remainingdataadj.PaymentMode = cmbpaymentmode.Text;
                                        remainingdataadj.Bankname = cmbbank.Text;
                                        remainingdataadj.Chequeno = txtchequeno.Text;
                                        remainingdataadj.Chequedate = dtpchequedate.Value;
                                        remainingdataadj.Narration = txtnarration.Text;
                                        remainingdataadj.Status = typeOfLedger;
                                        remainingpayrepoadj.Add(remainingdataadj);
                                        remainingpayrepoadj.Save();
                                    }
                                    catch (Exception)
                                    { }
                                }
                                if (OldPaidAmt != Convert.ToDecimal(txtPaidAmt.Text))
                                {
                                    BillWiseSupplierReceiptRepository BWSRRepoOld = new BillWiseSupplierReceiptRepository();
                                    List<BillWiseSupplierReceipt> BWSRDataOld = new List<BillWiseSupplierReceipt>();
                                    BWSRDataOld = BWSRRepoOld.GetAll().Where(t => t.SupplierReceiptNo == receiptid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                    if (BWSRDataOld != null)
                                    {
                                        foreach (var item in BWSRDataOld)
                                        {
                                            BWSRRepoOld.Remove(item);
                                            BWSRRepoOld.Save();
                                        }
                                    }
                                    decimal Rcptpaidamt = Convert.ToDecimal(txtPaidAmt.Text);
                                    decimal billamt = 0, remamt = 0;
                                    DataTable dt = new DataTable();
                                    try
                                    {
                                        db.connect();
                                        SqlCommand command = new SqlCommand("SPBillNoForLedger", db.Connection);
                                        command.CommandType = CommandType.StoredProcedure;

                                        command.Parameters.Add("@Ledger", SqlDbType.VarChar).Value = 'P';
                                        command.Parameters.Add("@Id", SqlDbType.VarChar).Value = cmbcustomer.SelectedValue;
                                        command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(cmbFirmName.SelectedValue);

                                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                                        adapter.Fill(dt);
                                        db.CloseConnection();
                                    }
                                    catch (Exception)
                                    { db.CloseConnection(); }
                                    if (dt.Rows.Count > 0)
                                    {
                                        SupplierRemainingPaymentRepository suprempayRepo = new SupplierRemainingPaymentRepository();
                                        SupplierRemainingPayment suprempayData = new SupplierRemainingPayment();
                                        suprempayData = suprempayRepo.GetAll().Where(t => t.Id < Id && t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.Type == "Receipt" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                        if (suprempayData != null)
                                        {
                                            if (suprempayData.RemainingAmt < 0)
                                                Rcptpaidamt = Rcptpaidamt + Math.Abs(Convert.ToDecimal(suprempayData.RemainingAmt));
                                        }
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            if (Rcptpaidamt > 0)
                                            {
                                                if (dt.Rows[i][0].ToString() != "0")
                                                {
                                                    BillWiseSupplierReceiptRepository BWSRRepo = new BillWiseSupplierReceiptRepository();
                                                    BillWiseSupplierReceipt BWSRData = new BillWiseSupplierReceipt();
                                                    BWSRData = BWSRRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                                    if (BWSRData != null)
                                                        billamt = Convert.ToDecimal(BWSRData.RemainigAmount);
                                                    else
                                                    {
                                                        SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                                                        SupplierRemainingPayment remdata = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                                        billamt = Convert.ToDecimal(remdata.NetAmt) - Convert.ToDecimal(remdata.PaidAmt);
                                                        remainingpayrepo1.Dispose();
                                                    }
                                                    BWSRRepo.Dispose();
                                                }
                                                else
                                                {
                                                    BillWiseSupplierReceiptRepository BWSRRepo = new BillWiseSupplierReceiptRepository();
                                                    BillWiseSupplierReceipt BWSRData = new BillWiseSupplierReceipt();
                                                    BWSRData = BWSRRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(0) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                                    if (BWSRData != null)
                                                        billamt = Convert.ToDecimal(BWSRData.RemainigAmount);
                                                    else
                                                    {
                                                        SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                                                        SupplierRemainingPayment remainingamt = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && (t.Type == "OpeningBalance" || t.BillId == 0) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == Convert.ToInt32(cmbFirmName.SelectedValue)).FirstOrDefault();
                                                        billamt = Convert.ToDecimal(remainingamt.NetAmt) - Convert.ToDecimal(remainingamt.PaidAmt);
                                                        remainingpayrepo1.Dispose();
                                                    }
                                                    BWSRRepo.Dispose();
                                                }
                                                BillWiseSupplierReceiptRepository BWSRRepo1 = new BillWiseSupplierReceiptRepository();
                                                BillWiseSupplierReceipt BWSRData1 = new BillWiseSupplierReceipt();
                                                BWSRData1.SupplierReceiptNo = Convert.ToInt32(remainingdata.Id);
                                                BWSRData1.SupplierId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                                BWSRData1.BillId = Convert.ToInt32(dt.Rows[i][0].ToString());

                                                if (Rcptpaidamt >= billamt)
                                                {
                                                    Rcptpaidamt = Rcptpaidamt - billamt;
                                                    BWSRData1.PaidAmount = Convert.ToDecimal(billamt);
                                                    BWSRData1.RemainigAmount = Convert.ToDecimal(0);
                                                }
                                                else
                                                {
                                                    BWSRData1.PaidAmount = Convert.ToDecimal(Rcptpaidamt);
                                                    remamt = billamt - Rcptpaidamt;
                                                    Rcptpaidamt = 0;
                                                    BWSRData1.RemainigAmount = Convert.ToDecimal(remamt);
                                                }
                                                BWSRData1.TransactionYear = CommonMethod.TransactionYear;
                                                BWSRData1.CompId = Convert.ToInt32(cmbFirmName.SelectedValue);
                                                BWSRRepo1.Add(BWSRData1);
                                                BWSRRepo1.Save();
                                            }
                                            else
                                                break;
                                        }
                                    }
                                }
                                MessageBox.Show("Supplier Receipt Information Saved.", "Success");
                                clear();
                                this.ActiveControl = btnnew;
                            }
                            else
                            {
                                if (cmbbank.Text == "Select")
                                {
                                    MessageBox.Show("Select Bank Name.", "Warning");
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
                                try
                                {
                                    SupplierRemainingPaymentRepository remRepo = new SupplierRemainingPaymentRepository();
                                    SupplierRemainingPayment remdata = remRepo.GetById(receiptid);
                                    BankStatementRepository BankStRepo = new BankStatementRepository();
                                    BankRepository bankInfoRepo = new BankRepository();
                                    var bankData = bankInfoRepo.GetAll().Where(t => t.BankId == Convert.ToInt32(cmbbank.SelectedValue) && t.Bankname == cmbbank.Text).FirstOrDefault();
                                    if (bankData != null)
                                        ACGId = Convert.ToInt32(bankData.ACGId);
                                    var bdata = BankStRepo.GetAll().Where(t => t.Bankname == cmbbank.Text && t.CompId == CommonMethod.CompId && t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.Chequeno && t.Withdrawals == Convert.ToDecimal(remdata.PaidAmt)).FirstOrDefault();

                                    BankStatementRepository bankrepo = new BankStatementRepository();
                                    BankStatement bankstdata = new BankStatement();

                                    bankstdata.BankId = Convert.ToInt32(cmbbank.SelectedValue);
                                    bankstdata.Bankname = Convert.ToString(cmbbank.Text);
                                    bankstdata.TransactionDate = dtpreceiptDate.Value;
                                    bankstdata.Particulars = "Paid to" + " " + Convert.ToString(cmbcustomer.Text.Trim());
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    bankstdata.Withdrawals = Convert.ToDecimal(txtPaidAmt.Text);
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankstdata.TransactionType = "Payment";
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

                                decimal decremamt = Convert.ToDecimal(txtRemainigAmt.Text);
                                SupplierRemainingPaymentRepository remainingpayrepo2 = new SupplierRemainingPaymentRepository();
                                List<SupplierRemainingPayment> remainingdata2 = new List<SupplierRemainingPayment>();
                                remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.Id > receiptid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();

                                int j = 0;
                                foreach (var item in remainingdata2)
                                {
                                    item.RemainingAmt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                                    decremamt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                                    remainingpayrepo2.Edit(item);
                                    remainingpayrepo2.Save();
                                    j++;
                                }

                                if (chkAdjustVoucher.Checked && Convert.ToDecimal(txtAdjustAmount.Text) > 0)
                                {
                                    try
                                    {
                                        SupplierRemainingPaymentRepository remainingpayrepoadj = new SupplierRemainingPaymentRepository();
                                        SupplierRemainingPayment remainingdataadj = new SupplierRemainingPayment();
                                        remainingdataadj.SupplierId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                        remainingdataadj.Suppliername = cmbcustomer.Text;
                                        remainingdataadj.CreateDate = dtpreceiptDate.Value;
                                        remainingdataadj.Type = "Adjustment";
                                        remainingdataadj.RemainingAmt = Convert.ToDecimal((Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text)) - Convert.ToDecimal(txtAdjustAmount.Text));
                                        remainingdataadj.NetAmt = Convert.ToDecimal(0);
                                        remainingdataadj.PaidAmt = Convert.ToDecimal(txtAdjustAmount.Text);
                                        remainingdataadj.TransactionYear = CommonMethod.TransactionYear;
                                        remainingdataadj.CompId = CommonMethod.CompId;
                                        remainingdataadj.PaymentMode = cmbpaymentmode.Text;
                                        remainingdataadj.Bankname = cmbbank.Text;
                                        remainingdataadj.Chequeno = txtchequeno.Text;
                                        remainingdataadj.Chequedate = dtpchequedate.Value;
                                        remainingdataadj.Narration = txtnarration.Text;
                                        remainingdataadj.Status = typeOfLedger;
                                        remainingpayrepoadj.Add(remainingdataadj);
                                        remainingpayrepoadj.Save();
                                    }
                                    catch (Exception)
                                    { }
                                }
                                if (OldPaidAmt != Convert.ToDecimal(txtPaidAmt.Text))
                                {
                                    BillWiseSupplierReceiptRepository BWSRRepoOld = new BillWiseSupplierReceiptRepository();
                                    List<BillWiseSupplierReceipt> BWSRDataOld = new List<BillWiseSupplierReceipt>();
                                    BWSRDataOld = BWSRRepoOld.GetAll().Where(t => t.SupplierReceiptNo == receiptid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                    if (BWSRDataOld != null)
                                    {
                                        foreach (var item in BWSRDataOld)
                                        {
                                            BWSRRepoOld.Remove(item);
                                            BWSRRepoOld.Save();
                                        }
                                    }
                                    decimal Rcptpaidamt = Convert.ToDecimal(txtPaidAmt.Text);
                                    decimal billamt = 0, remamt = 0;
                                    DataTable dt = new DataTable();
                                    try
                                    {
                                        db.connect();
                                        SqlCommand command = new SqlCommand("SPBillNoForLedger", db.Connection);
                                        command.CommandType = CommandType.StoredProcedure;

                                        command.Parameters.Add("@Ledger", SqlDbType.VarChar).Value = 'P';
                                        command.Parameters.Add("@Id", SqlDbType.VarChar).Value = cmbcustomer.SelectedValue;
                                        command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(cmbFirmName.SelectedValue);

                                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                                        adapter.Fill(dt);
                                        db.CloseConnection();
                                    }
                                    catch (Exception)
                                    { db.CloseConnection(); }
                                    if (dt.Rows.Count > 0)
                                    {
                                        SupplierRemainingPaymentRepository suprempayRepo = new SupplierRemainingPaymentRepository();
                                        SupplierRemainingPayment suprempayData = new SupplierRemainingPayment();
                                        suprempayData = suprempayRepo.GetAll().Where(t => t.Id < Id && t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.Type == "Receipt" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                        if (suprempayData != null)
                                        {
                                            if (suprempayData.RemainingAmt < 0)
                                                Rcptpaidamt = Rcptpaidamt + Math.Abs(Convert.ToDecimal(suprempayData.RemainingAmt));
                                        }
                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            if (Rcptpaidamt > 0)
                                            {
                                                if (dt.Rows[i][0].ToString() != "0")
                                                {
                                                    BillWiseSupplierReceiptRepository BWSRRepo = new BillWiseSupplierReceiptRepository();
                                                    BillWiseSupplierReceipt BWSRData = new BillWiseSupplierReceipt();
                                                    BWSRData = BWSRRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                                    if (BWSRData != null)
                                                        billamt = Convert.ToDecimal(BWSRData.RemainigAmount);
                                                    else
                                                    {
                                                        SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                                                        SupplierRemainingPayment remdata = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                                        billamt = Convert.ToDecimal(remdata.NetAmt) - Convert.ToDecimal(remdata.PaidAmt);
                                                        remainingpayrepo1.Dispose();
                                                    }
                                                    BWSRRepo.Dispose();
                                                }
                                                else
                                                {
                                                    BillWiseSupplierReceiptRepository BWSRRepo = new BillWiseSupplierReceiptRepository();
                                                    BillWiseSupplierReceipt BWSRData = new BillWiseSupplierReceipt();
                                                    BWSRData = BWSRRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && t.BillId == Convert.ToInt32(0) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                                    if (BWSRData != null)
                                                        billamt = Convert.ToDecimal(BWSRData.RemainigAmount);
                                                    else
                                                    {
                                                        SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                                                        SupplierRemainingPayment remainingamt = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustomer.SelectedValue) && (t.Type == "OpeningBalance" || t.BillId == 0) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == Convert.ToInt32(cmbFirmName.SelectedValue)).FirstOrDefault();
                                                        billamt = Convert.ToDecimal(remainingamt.NetAmt) - Convert.ToDecimal(remainingamt.PaidAmt);
                                                        remainingpayrepo1.Dispose();
                                                    }
                                                    BWSRRepo.Dispose();
                                                }
                                                BillWiseSupplierReceiptRepository BWSRRepo1 = new BillWiseSupplierReceiptRepository();
                                                BillWiseSupplierReceipt BWSRData1 = new BillWiseSupplierReceipt();
                                                BWSRData1.SupplierReceiptNo = Convert.ToInt32(remainingdata.Id);
                                                BWSRData1.SupplierId = Convert.ToInt32(cmbcustomer.SelectedValue);
                                                BWSRData1.BillId = Convert.ToInt32(dt.Rows[i][0].ToString());

                                                if (Rcptpaidamt >= billamt)
                                                {
                                                    Rcptpaidamt = Rcptpaidamt - billamt;
                                                    BWSRData1.PaidAmount = Convert.ToDecimal(billamt);
                                                    BWSRData1.RemainigAmount = Convert.ToDecimal(0);
                                                }
                                                else
                                                {
                                                    BWSRData1.PaidAmount = Convert.ToDecimal(Rcptpaidamt);
                                                    remamt = billamt - Rcptpaidamt;
                                                    Rcptpaidamt = 0;
                                                    BWSRData1.RemainigAmount = Convert.ToDecimal(remamt);
                                                }
                                                BWSRData1.TransactionYear = CommonMethod.TransactionYear;
                                                BWSRData1.CompId = Convert.ToInt32(cmbFirmName.SelectedValue);
                                                BWSRRepo1.Add(BWSRData1);
                                                BWSRRepo1.Save();
                                            }
                                            else
                                                break;
                                        }
                                    }
                                }
                                MessageBox.Show("Supplier Receipt Information Saved.", "Success");
                                clear();
                                this.ActiveControl = btnnew;
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
                        MessageBox.Show("Enter Paid Amount.", "Warning");
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

        public void getcustomerdata()
        {
            try
            {
                if (chksuppliername1.Checked == true)
                {
                    if (cmbsuppliername1.Text != "Select")
                    {
                        SupplierRepository supplierRepo = new SupplierRepository();
                        SupplierInformation supplierData = new SupplierInformation();
                        if (CommonMethod.commProduct == true)
                            supplierData = supplierRepo.GetAll().Where(t => t.SupplierCode == Convert.ToInt32(cmbsuppliername1.SelectedValue)).FirstOrDefault();
                        else
                            supplierData = supplierRepo.GetAll().Where(t => t.SupplierCode == Convert.ToInt32(cmbsuppliername1.SelectedValue) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        int cdays = 0;
                        if (!string.IsNullOrEmpty(Convert.ToString(supplierData.CreditDays)))
                            cdays = Convert.ToInt32(supplierData.CreditDays);
                        else
                            cdays = 0;
                        SqlDataAdapter da = new SqlDataAdapter();
                        da = new SqlDataAdapter("SELECT [ID],[ID] as BId,BillId as 'BillNo',CONVERT(VARCHAR(24),BillDate,103) as 'BillDate',[BillReceiptNo] as 'ReceiptNo' ,CONVERT(VARCHAR(24),[CreateDate],103) as 'ReceiptDate',[Type],[Suppliername] as 'Name' ,[NetAmt] as 'NetAmt',[PaidAmt] as 'PaidAmt',[RemainingAmt] as 'BalAmt' ,[PaymentMode] as 'Mode',[Bankname] as 'BankName',[Chequeno] as 'ChequeNo',CONVERT(VARCHAR(24),[Chequedate],103) as 'ChequeDate',CompId  FROM [SupplierRemainingPayment] where SupplierId='" + cmbsuppliername1.SelectedValue + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' and DATEDIFF(DAY, CreateDate,getdate())>='" + cdays + "'", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvreceiptInfo.DataSource = dt;
                        GvreceiptInfo.Refresh();
                        if (GvreceiptInfo.Rows.Count != 0)
                        {
                            GvreceiptInfo.Columns["ID"].Visible = false;
                            GvreceiptInfo.Columns["Bid"].Visible = false;
                            GvreceiptInfo.Columns["CompId"].Visible = false;
                            GvreceiptInfo.Columns["BalAmt"].Visible = false;
                            GvreceiptInfo.Columns["Mode"].Visible = false;
                        }
                        else
                            MessageBox.Show("Record not Present.", "Warning");
                        try
                        {
                            SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                            List<SupplierRemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbsuppliername1.SelectedValue) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                            var netamt = remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbsuppliername1.SelectedValue) && (DateTime.Now - t.CreateDate.Value).Days >= cdays && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && (t.Type != "Receipt")).Sum(t => t.NetAmt);
                            var Paidamt = remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbsuppliername1.SelectedValue) && (DateTime.Now - t.CreateDate.Value).Days >= cdays && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt);

                            if (remainingamt != null)
                            {
                                lblAmtText.Visible = true;
                                lblRemAmt.Visible = true;
                                if (Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt) >= 0)
                                {
                                    lblAmtText.Text = "Rem Amt:";
                                    lblRemAmt.Text = Convert.ToString(Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt));
                                }
                                else
                                {
                                    lblAmtText.Text = "Adv Amt:";
                                    lblRemAmt.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt)));
                                }
                            }
                            remainingpayrepo.Dispose();
                        }
                        catch (Exception)
                        { }
                        this.ActiveControl = cmbsuppliername1;
                        da.Dispose(); dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Please Select Supplier Name.", "Warning");
                        this.ActiveControl = cmbsuppliername1;
                    }
                }
                else
                {
                    MessageBox.Show("Please check checkbox first.", "Warning");
                    this.ActiveControl = chksuppliername1;
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
                    label15.Text = "Cheque No:";
                    label13.Text = "Cheque Date:";
                }
                else if (cmbpaymentmode.Text == "NEFT" || cmbpaymentmode.Text == "RTGS")
                {
                    cmbbank.Enabled = true;
                    txtchequeno.Enabled = true;
                    dtpchequedate.Enabled = true;
                    label15.Text = "UTR No:";
                    if (cmbpaymentmode.Text == "NEFT")
                        label13.Text = "NEFT Date :";
                    else
                        label13.Text = "RTGS Date :";
                }
                else
                {
                    cmbbank.Enabled = false;
                    txtchequeno.Enabled = false;
                    dtpchequedate.Enabled = false;
                }
            }
            catch (Exception)
            { }
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
                        //Stored Procedure code
                        SqlCommand command = new SqlCommand("SPSuppRemainingPayment", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                        if (typeOfLedger == "All")
                            command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "SuppRem";
                        else
                        {
                            command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "SuppRemLedger";
                            command.Parameters.Add("@typeOfLedger", SqlDbType.NVarChar).Value = typeOfLedger;
                        }
                        command.Parameters.Add("@SupplierID", SqlDbType.Int).Value = Convert.ToInt32(cmbcustomer.SelectedValue);
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
                        txtRemainigAmt.Text = Convert.ToString(Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text));
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
                command.Parameters.Add("@LedgerName", SqlDbType.NVarChar).Value = "Supplier";
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                command.Parameters.Add("@typeOfLedger", SqlDbType.NVarChar).Value = typeOfLedger;
                if (chksuppliername1.Checked == true)
                {
                    if (cmbsuppliername1.Text != "Select")
                        command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = Convert.ToInt32(cmbsuppliername1.SelectedValue);
                    else
                        errormsg = errormsg + Environment.NewLine + "Please Select SupplierName.";
                }
                if (chkbankname.Checked == true)
                {
                    if (cmbbank2.Text != "Select")
                        command.Parameters.Add("@BankName", SqlDbType.Int).Value = Convert.ToInt32(cmbbank2.SelectedValue);
                    else
                        errormsg = errormsg + Environment.NewLine + "Please Select BankName.";
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
                            GvreceiptInfo.Columns["UserSeries"].Visible = false;
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
                int CustId = 0, SaleId = 0;
                string BankName = "", ReceiptType = "", CDate = "", FDate = "", TDate = "", PaymentMode = "";
                if (chksuppliername1.Checked == true)
                {
                    if (cmbsuppliername1.Text != "Select")
                        CustId = Convert.ToInt32(cmbsuppliername1.SelectedValue);
                }
                if (chkbankname.Checked == true)
                {
                    if (cmbbank2.Text != "Select")
                        BankName = Convert.ToString(cmbbank2.SelectedText);
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
                RptReceipt receiptrpt = new RptReceipt("Supplier", CustId, SaleId, BankName, ReceiptType, CDate, FDate, TDate, typeOfLedger, PaymentMode, 0);
                receiptrpt.ShowDialog();
                receiptrpt.Dispose();
            }
            catch (Exception)
            { }
            try
            {
                if (SCreditors1 != "")
                {
                    RptSupplierReceipt receiptrpt = new RptSupplierReceipt(fdate2, tdate2, SCreditors1);
                    receiptrpt.ShowDialog();
                    receiptrpt.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void chksuppliername1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chksuppliername1.Checked == true)
                {
                    chkbankname.Checked = false;
                    chkdate.Checked = false;
                    fillcombo();
                    fillcombo1();
                }
            }
            catch (Exception)
            { }
        }

        private void GvreceiptInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["Type"].Value) == "Purchase")
                {
                    //if (Convert.ToInt32(GvreceiptInfo.Rows[e.RowIndex].Cells[14].Value) == CommonMethod.CompId)
                    //{
                    frmPurchaseOrder purord = new frmPurchaseOrder(Convert.ToInt32(GvreceiptInfo.Rows[e.RowIndex].Cells["BillNo"].Value));
                    purord.ShowDialog();
                    btnsearch_Click(null, null);
                    //}
                    //else
                    //    MessageBox.Show("This Purchase Created By Another" + Environment.NewLine + "Person So U Can not Update it.", "Warning");
                }
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["Type"].Value) == "OpeningBalance")
                    MessageBox.Show("Please Update this Data in Supplier Information Form.", "Warning");
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["Type"].Value) == "Send To" || Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["Type"].Value) == "Receive From")
                    MessageBox.Show("Please Update this Data in Journal Entry Form.", "Warning");
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["Type"].Value) == "Receipt" || Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["Type"].Value) == "Adjustment")
                {
                    receiptid = Convert.ToInt32(GvreceiptInfo.Rows[e.RowIndex].Cells["BId"].Value);
                    txtreceiptno.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["ReceiptNo"].Value);
                    dtpreceiptDate.Value = Convert.ToDateTime(GvreceiptInfo.Rows[e.RowIndex].Cells["ReceiptDate"].Value);
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
                        cmbbank.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["BankName"].Value);
                        txtchequeno.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["ChequeNo"].Value);
                        try
                        {
                            dtpchequedate.Value = Convert.ToDateTime(GvreceiptInfo.Rows[e.RowIndex].Cells["ChequeDate"].Value);
                        }
                        catch (Exception)
                        { }
                    }
                    else
                    {
                        txtchequeno.Text = "";
                        cmbbank.Text = "Select";
                        dtpchequedate.Enabled = false;
                    }
                    btndelete.Enabled = true;
                    btnSave.Text = "Update";
                }
            }
            catch (Exception)
            { }
        }

        private void txtPaidAmt_Leave_1(object sender, EventArgs e)
        {
            try
            {
                txtRemainigAmt.Text = Convert.ToString(Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text));
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
                    chksuppliername1.Checked = false;
                    chkdate.Checked = false;
                    chktype.Checked = false;
                    chkbetdate.Checked = false;
                    supplierdata();
                }
            }
            catch (Exception)
            { }
        }

        private void cmbsuppliername1_KeyDown(object sender, KeyEventArgs e)
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

        private void chkdate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkdate.Checked == true)
                {
                    chksuppliername1.Checked = false;
                    chkbankname.Checked = false;
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
                if (chktype.Checked == true)
                    chkbankname.Checked = false;
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
                SupplierRemainingPaymentRepository remRepo = new SupplierRemainingPaymentRepository();
                SupplierRemainingPayment remdata = remRepo.GetById(receiptid);
                remRepo.Remove(remdata);
                remRepo.Save();
                if (remdata.PaymentMode != "Cash")
                {
                    BankStatementRepository Brepo = new BankStatementRepository();
                    var bdata = Brepo.GetAll().Where(t => t.Bankname == remdata.Bankname && t.TransactionDate == remdata.CreateDate && t.ChequeNo == remdata.Chequeno && t.Withdrawals == remdata.PaidAmt && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (bdata != null)
                    {
                        BankStatement bsdata = Brepo.GetById(bdata.Id);
                        Brepo.Remove(bsdata);
                        Brepo.Save();
                    }
                }
                BillWiseSupplierReceiptRepository BillWiseSR = new BillWiseSupplierReceiptRepository();
                List<BillWiseSupplierReceipt> BWSRData = new List<BillWiseSupplierReceipt>();
                BWSRData = BillWiseSR.GetAll().Where(t => t.SupplierReceiptNo == receiptid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                foreach (var item in BWSRData)
                {
                    BillWiseSR.Remove(item);
                    BillWiseSR.Save();
                }
                SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                SupplierRemainingPayment remdata1 = remRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(remdata.SupplierId) && t.Id < receiptid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                decimal decremamt = Convert.ToDecimal(remdata1.RemainingAmt);
                SupplierRemainingPaymentRepository remainingpayrepo2 = new SupplierRemainingPaymentRepository();
                List<SupplierRemainingPayment> remainingdata2 = new List<SupplierRemainingPayment>();
                remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.SupplierId == Convert.ToInt32(remdata.SupplierId) && t.Id > receiptid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();

                int i = 0;
                foreach (var item in remainingdata2)
                {
                    item.RemainingAmt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                    decremamt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
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

        private void frmSupplierLedger_Load(object sender, EventArgs e)
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

        private void cmbFirmName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                customerdata();
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
                    this.ActiveControl = cmbFirmName;
            }
            catch (Exception)
            { }
        }

        private void cmbFirmName_KeyDown(object sender, KeyEventArgs e)
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
                        frmSupplierInformation supplier = new frmSupplierInformation();
                        supplier.ShowDialog();
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
                        this.ActiveControl = cmbbank;
                }
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

        private void chksuppliername1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbsuppliername1;
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

        public void BalShtToSupplier(string SCreditors, DateTime fdate, DateTime tdate)
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
                if (!GvreceiptInfo.Columns.Contains("CompId"))
                {
                    DataGridViewTextBoxColumn CompIdColumn = new DataGridViewTextBoxColumn();
                    CompIdColumn.Name = "CompId";
                    CompIdColumn.HeaderText = "CompId";
                    GvreceiptInfo.Columns.Add(CompIdColumn);
                }
                if (!GvreceiptInfo.Columns.Contains("Narration"))
                {
                    DataGridViewTextBoxColumn NarrationColumn = new DataGridViewTextBoxColumn();
                    NarrationColumn.Name = "Narration";
                    NarrationColumn.HeaderText = "Narration";
                    GvreceiptInfo.Columns.Add(NarrationColumn);
                }
                string fromDate1 = cm.ValidFromDate(Convert.ToDateTime(fdate.Date));
                string toDate1 = cm.ValidToDate(Convert.ToDateTime(tdate.Date));
                DataTable dtNetAmt; DataTable dtPaidAmt; int k = 0, j = 0, i = 0;
                decimal SActAmt = 0; decimal SRemAmt = 0; decimal SNetAmt = 0; decimal SPaidAmt = 0;
                DataTable dt1 = db.GetTable("select distinct SupplierId,SupplierName from SupplierRemainingPayment where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                if (dt1.Rows.Count > 0)
                {
                    for (i = 0; i < dt1.Rows.Count; i++)
                    {
                        int SupplierId = Convert.ToInt32(dt1.Rows[i][0].ToString());
                        string Suppliername = Convert.ToString(dt1.Rows[i][1].ToString());
                        dtNetAmt = db.GetTable("Select Isnull(sum(Isnull(NetAmt,0)),0) from SupplierRemainingPayment where SupplierId='" + SupplierId + "' and Suppliername='" + Suppliername + "' and CreateDate between '" + fromDate1 + "' and '" + toDate1 + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and Type!='Receipt' and CompId='" + CommonMethod.CompId + "'");
                        if (dtNetAmt.Rows.Count > 0)
                            SNetAmt = Convert.ToDecimal(dtNetAmt.Rows[0][0].ToString());
                        dtPaidAmt = db.GetTable("Select Isnull(sum(Isnull(PaidAmt,0)),0) from SupplierRemainingPayment where SupplierId='" + SupplierId + "' and Suppliername='" + Suppliername + "' and CreateDate between '" + fromDate1 + "' and '" + toDate1 + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                        if (dtPaidAmt.Rows.Count > 0)
                            SPaidAmt = Convert.ToDecimal(dtPaidAmt.Rows[0][0].ToString());
                        SRemAmt = SNetAmt - SPaidAmt;
                        if (SRemAmt > 0)
                        {
                            DataTable dtSupplier = db.GetTable("SELECT Id,[ID] as BId,Cast(BillId as Varchar(100)) as 'BillNo',CONVERT(VARCHAR(24),BillDate,103) as 'BillDate',[BillReceiptNo] as 'ReceiptNo' ,CONVERT(VARCHAR(24),[CreateDate],103) as 'ReceiptDate',[Type],[Suppliername] as 'Name' ,[NetAmt] as 'NetAmt',[PaidAmt] as 'PaidAmt',[RemainingAmt] as 'BalAmt' ,[PaymentMode] as 'Mode',[Bankname] as 'BankName',[Chequeno] as 'ChequeNo',CONVERT(VARCHAR(24),[Chequedate],103) as 'ChequeDate',CompId,Narration  FROM [SupplierRemainingPayment] where SupplierId='" + SupplierId + "' and SupplierName='" + Suppliername + "' and CreateDate>='" + fromDate1 + "' and CreateDate<='" + toDate1 + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' order by CONVERT(VARCHAR(24),[CreateDate ],101)");
                            if (dtSupplier.Rows.Count > 0)
                            {
                                for (k = 0; k < dtSupplier.Rows.Count; k++)
                                {
                                    GvreceiptInfo.Rows.Add();
                                    GvreceiptInfo.Rows[j].Cells[0].Value = dtSupplier.Rows[k][0].ToString();
                                    GvreceiptInfo.Rows[j].Cells[1].Value = dtSupplier.Rows[k][1].ToString();
                                    GvreceiptInfo.Rows[j].Cells[2].Value = dtSupplier.Rows[k][2].ToString();
                                    GvreceiptInfo.Rows[j].Cells[3].Value = dtSupplier.Rows[k][3].ToString();
                                    GvreceiptInfo.Rows[j].Cells[4].Value = dtSupplier.Rows[k][4].ToString();
                                    GvreceiptInfo.Rows[j].Cells[5].Value = dtSupplier.Rows[k][5].ToString();
                                    GvreceiptInfo.Rows[j].Cells[6].Value = dtSupplier.Rows[k][6].ToString();
                                    GvreceiptInfo.Rows[j].Cells[7].Value = dtSupplier.Rows[k][7].ToString();
                                    GvreceiptInfo.Rows[j].Cells[8].Value = dtSupplier.Rows[k][8].ToString();
                                    GvreceiptInfo.Rows[j].Cells[9].Value = dtSupplier.Rows[k][9].ToString();
                                    GvreceiptInfo.Rows[j].Cells[10].Value = dtSupplier.Rows[k][10].ToString();
                                    GvreceiptInfo.Rows[j].Cells[11].Value = dtSupplier.Rows[k][11].ToString();
                                    GvreceiptInfo.Rows[j].Cells[12].Value = dtSupplier.Rows[k][12].ToString();
                                    GvreceiptInfo.Rows[j].Cells[13].Value = dtSupplier.Rows[k][13].ToString();
                                    GvreceiptInfo.Rows[j].Cells[14].Value = dtSupplier.Rows[k][14].ToString();
                                    j++;
                                }
                                GvreceiptInfo.Columns["ID"].Visible = false;
                                GvreceiptInfo.Columns["BId"].Visible = false;
                                GvreceiptInfo.Columns["CompId"].Visible = false;
                                GvreceiptInfo.Columns["BalAmt"].Visible = false;
                                GvreceiptInfo.Columns["Mode"].Visible = false;
                                GvreceiptInfo.Columns["Narration"].Visible = false;
                                SActAmt = SActAmt + SRemAmt;
                            }
                        }
                    }
                }
                lblAmtText.Visible = true;
                lblRemAmt.Visible = true;
                if (SActAmt >= 0)
                {
                    lblAmtText.Text = "Rem Amt:";
                    lblRemAmt.Text = Convert.ToString(SActAmt);
                }
                else
                {
                    lblAmtText.Text = "Adv Amt:";
                    lblRemAmt.Text = Convert.ToString(Math.Abs(SActAmt));
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

        private void btnSuppAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmSupplierInformation supplier = new frmSupplierInformation();
                supplier.ShowDialog();
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
                    if (cm.ValidSupplier(cmbcustomer.Text) == 1)
                    { }
                    else
                    {
                        MessageBox.Show("Supplier Name is not Valid", "Warning");
                        cmbcustomer.SelectedIndex = 0;
                        this.ActiveControl = cmbcustomer;
                    }
                }
            }
            catch (Exception)
            { }
        }
    }
}