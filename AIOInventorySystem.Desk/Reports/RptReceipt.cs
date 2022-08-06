using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptReceipt : Form
    {
        DbClass db = new DbClass();
        ReceiptCrystalReport1 receiptrpt;
        CustomerReceiptrpt customerreceiptrpt;
        public int index;
        public string bankname, chequedate, chequeno, BillreceiptNO, totalremainingamount, Narration, ManualRecptNo, Number, NetAmtInWord, strFlag = "";
        public decimal TNetAmt = 0, TPaidAmt = 0, totalRem1 = 0, AfterDateRemAmt = 0;
        CurrencyToWord CurrencytoWord = new CurrencyToWord();

        public RptReceipt()
        {
            InitializeComponent();
            customerdata();
        }

        public RptReceipt(string Flag, int CustId, int SaleId, string BankName, string ReceiptType, string CDate, string FDate, string TDate, string typeOfLedger, string PaymentMode, int SiteId)
        {
            InitializeComponent();
            loaddata(Flag, CustId, SaleId, BankName, ReceiptType, CDate, FDate, TDate, typeOfLedger, PaymentMode, SiteId);
            strFlag = Flag;
            if (Flag == "Customer")
                customerdata();
            else if (Flag == "Supplier")
                supplierdata();
        }

        public RptReceipt(string receiptid, string customername, int custid, decimal totalPaidAmount)
        {
            string CustId = receiptid;
            InitializeComponent();
            loaddata(receiptid, customername, custid, totalPaidAmount);
            customerdata();
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
            }
            catch (Exception)
            { }
        }

        public void supplierdata()
        {
            try
            {
                SupplierRepository custrepo = new SupplierRepository();
                List<SupplierInformation> customerdata = new List<SupplierInformation>();
                customerdata.Clear();
                if (CommonMethod.commProduct == true)
                    customerdata = custrepo.GetAll();
                else
                    customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.SupplierName).ToList();
                customerdata.Add(new SupplierInformation { SupplierName = "Select", SupplierCode = 0 });
                customerdata.Reverse();
                cmbcustomer.DataSource = customerdata;
                cmbcustomer.ValueMember = "SupplierCode";
                cmbcustomer.DisplayMember = "SupplierName";
                custrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void loaddata(string Flag, int CustId, int SaleId, string BankName, string ReceiptType, string CDate, string FDate, string TDate, string typeOfLedger, string PaymentMode, int SiteId)
        {
            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPLedgerReport", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@LedgerName", SqlDbType.NVarChar).Value = Flag;
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                command.Parameters.Add("@typeOfLedger", SqlDbType.NVarChar).Value = typeOfLedger;

                if (CustId != 0)
                    command.Parameters.Add("@CustomerId", SqlDbType.Int).Value = CustId;
                if (BankName != "")
                    command.Parameters.Add("@BankName", SqlDbType.VarChar).Value = BankName;
                if (SaleId != 0)
                    command.Parameters.Add("@SalesmanId", SqlDbType.Int).Value = SaleId;
                if (SiteId != 0)
                    command.Parameters.Add("@SiteId", SqlDbType.Int).Value = SiteId;
                if (ReceiptType != "")
                    command.Parameters.Add("@ReceiptType", SqlDbType.NVarChar).Value = ReceiptType;
                if (CDate != "")
                    command.Parameters.Add("@Date", SqlDbType.NVarChar).Value = CDate;
                if (FDate != "" && TDate != "")
                {
                    CommonMethod com = new CommonMethod();
                    command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = FDate;
                    command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = TDate;
                }
                if (PaymentMode != "")
                    command.Parameters.Add("@PaymentMode", SqlDbType.VarChar).Value = PaymentMode;
                decimal TotRemAmt = 0;
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                if (dt.Rows.Count > 0)
                {
                    object sumNetAmt = dt.Compute("Sum(NetAmt)", "");
                    object sumPaidAmt = dt.Compute("Sum(PaidAmt)", "");
                    object sumtotAmount = Convert.ToDecimal(sumNetAmt) - Convert.ToDecimal(sumPaidAmt);
                    TotRemAmt = Convert.ToDecimal(sumtotAmount);
                }
                //else
                //    MessageBox.Show("Record not Present.", "Warning");

                SqlDataAdapter da;
                DSReceipt ds = new DSReceipt();
                da = new SqlDataAdapter("Select * from tblRemainingPaymentReport", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tblRemainingPaymentReport");

                CustomerLedgerRpt rptledger = new CustomerLedgerRpt();
                rptledger.SetDataSource(ds);
                rptledger.SetParameterValue("TotaRemAmt", TotRemAmt);
                rptledger.SetParameterValue("Flag", Flag);
                //receiptrpt.SetParameterValue("TotalNetAmount", TNetAmt);
                //receiptrpt.SetParameterValue("TotalPaidAmount", TPaidAmt);
                //receiptrpt.SetParameterValue("Flag", "Show");
                //receiptrpt.SetParameterValue("AfterDateAmt", AfterDateRemAmt);
                //receiptrpt.SetParameterValue("ShowPrevAmt", "Show");

                crystalReportViewer1.ReportSource = rptledger;
                db.CloseConnection();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void loaddata(string receiptid, string customername, int custid, decimal totalPaidAmt)
        {
            try
            {
                string ManualRcptNo = "";
                SqlDataAdapter da;
                try
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPremainingpayment", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                    command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "CustRem";
                    command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = custid;
                    DataTable dt1 = new DataTable();
                    dt1.Load(command.ExecuteReader());
                    object sumtotAmount = new object();
                    sumtotAmount = dt1.Compute("Sum(TotalReaminingAmount)", "");
                    db.CloseConnection();
                    totalremainingamount = Convert.ToString(sumtotAmount);
                    dt1.Dispose();
                }
                catch (Exception)
                { db.CloseConnection(); }

                DSReceipt ds = new DSReceipt();
                da = new SqlDataAdapter("Select * from RemainingPayment where BillReceiptNo='" + receiptid + "' and TransactionYear = '" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "RemainingPayment");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                customerreceiptrpt = new CustomerReceiptrpt();
                customerreceiptrpt.SetDataSource(ds);
                ManualRcptNo = Convert.ToString((ds.Tables["RemainingPayment"].Rows[0]["ManualReceiptNo"].ToString()));
                customerreceiptrpt.SetParameterValue("ManualRecpNo", ManualRcptNo);
                if (totalPaidAmt == 0)
                {
                    decimal p = Convert.ToDecimal(ds.Tables["RemainingPayment"].Rows[0]["PaidAmt"].ToString());
                    customerreceiptrpt.SetParameterValue("PaidAmount", p);
                    customerreceiptrpt.SetParameterValue("TotalRemainingAmount", 0);
                }
                else
                {
                    customerreceiptrpt.SetParameterValue("TotalRemainingAmount", totalPaidAmt);
                    customerreceiptrpt.SetParameterValue("PaidAmount", 0);
                }
                DataTable dtNetAmt = new DataTable();
                da.Fill(dtNetAmt);
                if (dtNetAmt.Rows.Count > 0)
                {
                    Number = Math.Round(Convert.ToDecimal(ds.Tables["RemainingPayment"].Rows[0]["PaidAmt"].ToString()), 0, MidpointRounding.AwayFromZero).ToString().Split('.')[0];
                    CurrencytoWord = new CurrencyToWord();
                    NetAmtInWord = "Rs." + CurrencytoWord.ConvertToWord(Number) + "Only";
                }
                customerreceiptrpt.SetParameterValue("NetAmtInWord", NetAmtInWord);
                crystalReportViewer1.ReportSource = customerreceiptrpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
                totalPaidAmt = 0;
            }
            catch (Exception)
            { db.CloseConnection(); }
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

        public void loaddataCreditDay(int custid, string customername)
        {
            int cdays = 0; AfterDateRemAmt = 0;
            try
            {
                CustomerRepository supplierRepo = new CustomerRepository();
                var supplierData = supplierRepo.GetAll().Where(t => t.CustomerID == custid && t.CompId == CommonMethod.CompId).FirstOrDefault();

                if (!string.IsNullOrEmpty(Convert.ToString(supplierData.CreditDays)))
                    cdays = Convert.ToInt32(supplierData.CreditDays);
                else
                    cdays = 0;

                try
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPremainingpayment", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                    command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "CustRem";
                    command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = custid;
                    DataTable dt1 = new DataTable();
                    dt1.Load(command.ExecuteReader());
                    object sumtotAmount = new object();
                    sumtotAmount = dt1.Compute("Sum(TotalReaminingAmount)", "");
                    db.CloseConnection();
                    totalremainingamount = Convert.ToString(sumtotAmount);
                    dt1.Dispose();
                }
                catch (Exception)
                { db.CloseConnection(); }
            }
            catch (Exception)
            { }
            try
            {
                CustomerBillMasterRepository custmastrepo = new CustomerBillMasterRepository();
                CustomerBillMaster custdata = new CustomerBillMaster();
                RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                List<RemainingPayment> customerdata = remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(custid) && (t.Type == "Bill" || t.Type == "OpeningBalance" || t.Type == "Receipt" || t.Type == "BillReturn" || t.Type == "Adjustment" || t.Type == "Credit Note" || t.Type == "Debit Note" || t.Type == "Send To" || t.Type == "Receive From") && (DateTime.Now - t.CreateDate.Value).Days >= cdays && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).OrderBy(t => t.CreateDate.Value.Date).ToList();
                DSReceipt ds = new DSReceipt();
                DataTable dt = new DataTable();
                int i = 0;
                if (customerdata != null)
                {
                    foreach (var item in customerdata)
                    {
                        if (item.Type == "BillReturn")
                        {
                            var totalprnet = remainingpayrepo.GetAll().Where(t => t.BillId == Convert.ToString(item.BillId) && t.Type == "BillReturn" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmt);
                            var totalprpaid = remainingpayrepo.GetAll().Where(t => t.BillId == Convert.ToString(item.BillId) && t.Type == "BillReturn" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt);
                            string billdate = item.CreateDate.Value.ToShortDateString();
                            custdata = custmastrepo.GetAll().Where(t => t.BillNo == item.BillId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            try
                            {
                                BillreceiptNO = custdata.BillRcptNo;
                            }
                            catch (Exception)
                            {
                                BillreceiptNO = "";
                            }
                            string custname = item.Customername;
                            string netamt = Convert.ToString(totalprnet);
                            string paidamt = Convert.ToString(totalprpaid);
                            totalRem1 = totalRem1 + (Convert.ToDecimal(netamt) - Convert.ToDecimal(paidamt));
                            string remainingamt = Convert.ToString(totalRem1);
                            string type = Convert.ToString(item.Type);
                            string billno = Convert.ToString(item.BillId);
                            if (item.Narration == null || item.Narration == "")
                                Narration = "";
                            else
                                Narration = Convert.ToString(item.Narration);
                            if (item.ManualReceiptNo == null || item.ManualReceiptNo == "")
                                ManualRecptNo = "";
                            else
                                ManualRecptNo = Convert.ToString(item.ManualReceiptNo);
                            string paymentmode = item.PaymentMode;
                            if (paymentmode == "Cash")
                            {
                                bankname = "";
                                chequeno = "";
                                chequedate = null;
                            }
                            if (paymentmode == null)
                            {
                                bankname = "";
                                chequeno = "";
                                chequedate = null;
                            }
                            if (paymentmode == "Cheque")
                            {
                                bankname = item.Bankname;
                                chequeno = item.Chequeno;
                                chequedate = item.CreateDate.Value.ToShortDateString();
                            }
                            dt = ds.Tables["ReceiptInfo"];
                            dt.Rows.Add(new object[] { BillreceiptNO, custname, netamt, paidamt, remainingamt, paymentmode, bankname, chequeno, chequedate, billdate, type, billno, Narration, ManualRecptNo });
                            i++;
                        }
                        else if (item.Type == "Bill")
                        {
                            custdata = custmastrepo.GetAll().Where(t => t.BillNo == item.BillId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            string billdate = "";
                            try
                            {
                                billdate = custdata.Billdate.Value.ToShortDateString();
                            }
                            catch (Exception)
                            {
                                billdate = null;
                            }
                            try
                            {
                                BillreceiptNO = custdata.BillRcptNo;
                            }
                            catch (Exception)
                            {
                                BillreceiptNO = "";
                            }
                            string custname = custdata.CustomerName;
                            string netamt = Convert.ToString(custdata.NetAmount);
                            string paidamt = Convert.ToString(custdata.PaidAmount);
                            totalRem1 = totalRem1 + (Convert.ToDecimal(custdata.NetAmount) - Convert.ToDecimal(custdata.PaidAmount));
                            string remainingamt = Convert.ToString(totalRem1);
                            if (item.Narration == null || item.Narration == "")
                                Narration = "";
                            else
                                Narration = Convert.ToString(item.Narration);
                            if (item.ManualReceiptNo == null || item.ManualReceiptNo == "")
                                ManualRecptNo = "";
                            else
                                ManualRecptNo = Convert.ToString(item.ManualReceiptNo);
                            string type = Convert.ToString(item.Type);
                            string billno = Convert.ToString(item.BillId);
                            string paymentmode = item.PaymentMode;
                            if (paymentmode == "Cash")
                            {
                                bankname = "";
                                chequeno = "";
                                chequedate = null;
                            }
                            if (paymentmode == null)
                            {
                                bankname = "";
                                chequeno = "";
                                chequedate = null;
                            }
                            if (paymentmode == "Cheque")
                            {
                                bankname = item.Bankname;
                                chequeno = item.Chequeno;
                                chequedate = item.CreateDate.Value.ToShortDateString();
                            }
                            dt = ds.Tables["ReceiptInfo"];
                            dt.Rows.Add(new object[] { BillreceiptNO, custname, netamt, paidamt, remainingamt, paymentmode, bankname, chequeno, chequedate, billdate, type, billno, Narration, ManualRecptNo });
                            i++;
                        }
                        else
                        {
                            string billdate = item.CreateDate.Value.ToShortDateString();
                            BillreceiptNO = Convert.ToString(item.BillReceiptNo);
                            string custname = item.Customername;
                            string type = Convert.ToString(item.Type);
                            string billno = "";
                            if (item.Narration == null || item.Narration == "")
                                Narration = "";
                            else
                                Narration = Convert.ToString(item.Narration);
                            if (item.ManualReceiptNo == null || item.ManualReceiptNo == "")
                                ManualRecptNo = "";
                            else
                                ManualRecptNo = Convert.ToString(item.ManualReceiptNo);
                            string paymentmode = item.PaymentMode;
                            if (paymentmode == "Cash")
                            {
                                bankname = "";
                                chequeno = "";
                                chequedate = null;
                            }
                            if (paymentmode == null)
                            {
                                bankname = "";
                                chequeno = "";
                                chequedate = null;
                            }
                            if (paymentmode == "Cheque")
                            {
                                bankname = item.Bankname;
                                chequeno = item.Chequeno;
                                chequedate = item.CreateDate.Value.ToShortDateString();
                            }
                            if (paymentmode == "NEFT" || paymentmode == "RTGS")
                            {
                                bankname = item.Bankname;
                                chequeno = item.Chequeno;
                                chequedate = null;
                            }
                            billno = Convert.ToString(item.BillId);
                            string netamt = Convert.ToString(item.NetAmt);
                            string paidamt = Convert.ToString(item.PaidAmt);
                            totalRem1 = totalRem1 + (Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt));
                            string remainingamt = Convert.ToString(totalRem1);
                            if (item.Type == "Receipt")
                                dt = ds.Tables["ReceiptInfo"];
                            else
                            {
                                paidamt = Convert.ToString(item.PaidAmt);
                                remainingamt = Convert.ToString(totalRem1);
                            }
                            dt = ds.Tables["ReceiptInfo"];
                            dt.Rows.Add(new object[] { BillreceiptNO, custname, netamt, paidamt, remainingamt, paymentmode, bankname, chequeno, chequedate, billdate, type, billno, Narration, ManualRecptNo });
                            goto b;
                        a:
                            {
                                dt = ds.Tables["ReceiptInfo"];
                                dt.Rows[index]["billno"] = billno;
                                dt.Rows[index]["paidamt"] = paidamt;
                                dt.Rows[index]["remainingamt"] = remainingamt;
                                dt.Rows[index]["paymentmode"] = paymentmode;
                            }
                        b:
                            {
                                i++;
                            }
                        }
                    }
                }
                receiptrpt = new ReceiptCrystalReport1();
                receiptrpt.SetDataSource(ds);
                receiptrpt.SetParameterValue("TotalRemainingAmount", totalremainingamount);
                receiptrpt.SetParameterValue("TotalNetAmount", TNetAmt);
                receiptrpt.SetParameterValue("TotalPaidAmount", TPaidAmt);
                receiptrpt.SetParameterValue("Flag", "Show");
                receiptrpt.SetParameterValue("AfterDateAmt", AfterDateRemAmt);
                receiptrpt.SetParameterValue("ShowPrevAmt", "Hide");
                crystalReportViewer1.ReportSource = receiptrpt;
                db.CloseConnection();
                custmastrepo.Dispose();
                remainingpayrepo.Dispose();
                ds.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void loadSupplierdataCreditDay(int custid, string suppliername)
        {
            int cdays = 0;
            try
            {
                SupplierRepository supplierRepo = new SupplierRepository();
                var supplierData = supplierRepo.GetAll().Where(t => t.SupplierCode == custid).FirstOrDefault();
                if (!string.IsNullOrEmpty(Convert.ToString(supplierData.CreditDays)))
                    cdays = Convert.ToInt32(supplierData.CreditDays);
                else
                    cdays = 0;
            }
            catch (Exception) { }
            try
            {
                db.connect();
                //Stored Procedure code
                SqlCommand command = new SqlCommand("SPSuppRemainingPayment", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "SuppCreditList";
                command.Parameters.Add("@SupplierID", SqlDbType.Int).Value = Convert.ToInt32(custid);
                DataTable dt1 = new DataTable();
                dt1.Load(command.ExecuteReader());
                db.CloseConnection();
                totalremainingamount = Convert.ToString(dt1.Compute("Sum(TotalReaminingAmount)", ""));
                dt1.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }

            try
            {
                PurchaseMasterRepository purchaseMastRepo = new PurchaseMasterRepository();
                PurchaseMaster purdata = new PurchaseMaster();
                SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                List<SupplierRemainingPayment> customerdata = remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(custid) && (t.Type == "Purchase" || t.Type == "OpeningBalance" || t.Type == "Receipt" || t.Type == "PurchaseReturn" || t.Type == "Adjustment" || t.Type == "Credit Note" || t.Type == "Debit Note" || t.Type == "Send To" || t.Type == "Receive From") && (DateTime.Now - t.CreateDate.Value).Days >= cdays && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).OrderBy(t => t.CreateDate.Value.Date).ToList();
                DSSupplierReceipt ds = new DSSupplierReceipt();

                DataTable dt = new DataTable();
                int i = 0;
                if (customerdata != null)
                {
                    foreach (var item in customerdata)
                    {
                        if (item.Type == "PurchaseReturn")
                        {
                            var totalnet = remainingpayrepo.GetAll().Where(t => t.BillId == Convert.ToInt32(item.BillId) && t.Type == "PurchaseReturn" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == item.CompId).Sum(t => t.NetAmt);
                            var totalpaid = remainingpayrepo.GetAll().Where(t => t.BillId == Convert.ToInt32(item.BillId) && t.Type == "PurchaseReturn" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == item.CompId).Sum(t => t.PaidAmt);
                            string billdate = item.CreateDate.Value.ToShortDateString();
                            string receiptdate = "";
                            string custname = item.Suppliername;
                            string netamt = Convert.ToString(totalnet);
                            string paidamt = Convert.ToString(totalpaid);
                            string remainingamt = "0";
                            string type = Convert.ToString(item.Type);
                            if (item.Narration == null || item.Narration == "")
                                Narration = "";
                            else
                                Narration = Convert.ToString(item.Narration);
                            string pono;
                            try
                            {
                                if (item.Type == "Receipt")
                                    pono = Convert.ToString(item.BillId);
                                else
                                    pono = "";
                            }
                            catch (Exception)
                            { pono = ""; }
                            string paymentmode = item.PaymentMode;
                            if (paymentmode == "Cash")
                            {
                                bankname = "";
                                chequeno = "";
                                chequedate = null;
                            }
                            if (paymentmode == null)
                            {
                                bankname = "";
                                chequeno = "";
                                chequedate = null;
                            }
                            if (paymentmode == "Cheque")
                            {
                                bankname = item.Bankname;
                                chequeno = item.Chequeno;
                                chequedate = item.CreateDate.Value.ToShortDateString();
                            }
                            if (paymentmode == "NEFT" || paymentmode == "RTGS")
                            {
                                bankname = item.Bankname;
                                chequeno = item.Chequeno;
                                chequedate = null;
                            }
                            dt = ds.Tables["ReceiptInfo"];
                            dt.Rows.Add(new object[] { billdate, receiptdate, custname, netamt, paidamt, remainingamt, paymentmode, bankname, chequeno, chequedate, type, pono, Narration });
                            i++;
                        }
                        else if (item.Type == "Purchase")
                        {
                            purdata = purchaseMastRepo.GetAll().Where(t => t.PorderNo == item.BillId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == item.CompId).FirstOrDefault();
                            string billdate = purdata.PorderDate.Value.ToShortDateString();
                            string receiptdate = "";
                            string custname = purdata.Suppliername;
                            string netamt = Convert.ToString(purdata.NetAmount);
                            string paidamt = Convert.ToString(purdata.PaidAmount);
                            string remainingamt = Convert.ToString(purdata.RemainingAmount);
                            string pono = Convert.ToString(purdata.PorderNo);
                            string type = Convert.ToString(item.Type);
                            if (item.Narration == null || item.Narration == "")
                                Narration = "";
                            else
                                Narration = Convert.ToString(item.Narration);
                            string paymentmode = item.PaymentMode;
                            if (paymentmode == "Cash")
                            {
                                bankname = "";
                                chequeno = "";
                                chequedate = null;
                            }
                            if (paymentmode == null)
                            {
                                bankname = "";
                                chequeno = "";
                                chequedate = "";
                            }
                            if (paymentmode == "Cheque")
                            {
                                bankname = item.Bankname;
                                chequeno = item.Chequeno;
                                chequedate = item.CreateDate.Value.ToShortDateString();
                            }
                            if (paymentmode == "NEFT" || paymentmode == "RTGS")
                            {
                                bankname = item.Bankname;
                                chequeno = item.Chequeno;
                                chequedate = null;
                            }
                            dt = ds.Tables["ReceiptInfo"];
                            dt.Rows.Add(new object[] { billdate, receiptdate, custname, netamt, paidamt, remainingamt, paymentmode, bankname, chequeno, chequedate, type, pono, Narration });
                            i++;
                        }
                        else
                        {
                            string billdate = "";
                            string receiptdate = item.CreateDate.Value.ToShortDateString();
                            string custname = item.Suppliername;
                            string netamt = Convert.ToString(item.NetAmt);
                            string paidamt = Convert.ToString(item.PaidAmt);
                            string remainingamt = Convert.ToString(item.RemainingAmt);
                            string type = Convert.ToString(item.Type);
                            string pono = Convert.ToString(item.BillId);
                            if (item.Narration == null || item.Narration == "")
                                Narration = "";
                            else
                                Narration = Convert.ToString(item.Narration);
                            string paymentmode = item.PaymentMode;
                            if (paymentmode == "Cash")
                            {
                                bankname = "";
                                chequeno = "";
                                chequedate = null;
                            }
                            if (paymentmode == null)
                            {
                                bankname = "";
                                chequeno = "";
                                chequedate = null;
                            }
                            if (paymentmode == "Cheque")
                            {
                                bankname = item.Bankname;
                                chequeno = item.Chequeno;
                                chequedate = item.CreateDate.Value.ToShortDateString();
                            }
                            if (paymentmode == "NEFT" || paymentmode == "RTGS")
                            {
                                bankname = item.Bankname;
                                chequeno = item.Chequeno;
                                chequedate = null;
                            }
                            if (item.Type == "Receipt")
                                dt = ds.Tables["ReceiptInfo"];
                            else
                            {
                                paidamt = Convert.ToString(item.PaidAmt);
                                remainingamt = Convert.ToString(item.RemainingAmt);
                            }
                            dt = ds.Tables["ReceiptInfo"];
                            dt.Rows.Add(new object[] { billdate, receiptdate, custname, netamt, paidamt, remainingamt, paymentmode, bankname, chequeno, chequedate, type, pono, Narration });
                            goto b;
                        a:
                            {
                                dt = ds.Tables["ReceiptInfo"];
                                dt.Rows[index][11] = pono;
                                dt.Rows[index][4] = paidamt;
                                dt.Rows[index][5] = remainingamt;
                                dt.Rows[index][6] = paymentmode;
                            }
                        b:
                            {
                                i++;
                            }
                        }
                    }
                }
                SupplierReceiptCrystalReport srpt = new SupplierReceiptCrystalReport();
                srpt.SetDataSource(ds);
                srpt.SetParameterValue("TotalRemainingAmount", totalremainingamount);
                srpt.SetParameterValue("TNetAmt", TNetAmt);
                srpt.SetParameterValue("TPaidAmt", TPaidAmt);
                srpt.SetParameterValue("Flag", "Show");
                srpt.SetParameterValue("ShowPrevAmt", "Hide");
                srpt.SetParameterValue("AfterDateRemAmt", AfterDateRemAmt);
                crystalReportViewer1.ReportSource = srpt;
                db.CloseConnection();
                remainingpayrepo.Dispose();
                ds.Dispose();
                dt.Dispose();
                purchaseMastRepo.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void loaddata(DateTime fdate, DateTime tdate, string CCreditors)
        {
            int customerid;
            string CustName;
            decimal remainingAmt = 0;
            AfterDateRemAmt = 0;
            CustomerBillMasterRepository custmastrepo = new CustomerBillMasterRepository();
            CustomerBillMaster custdata = new CustomerBillMaster();
            RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
            RemainingPaymentRepository remainingpayrepo1 = new RemainingPaymentRepository();
            DSReceipt ds = new DSReceipt();
            DataTable dt = new DataTable();
            bool userSeries = false;
            SettingRepository setrepo = new SettingRepository();
            Setting setdata = setrepo.GetAll().FirstOrDefault();
            if (setdata != null)
            {
                if (setdata.ShowUserDefinedSeries == "Y")
                    userSeries = true;
                else
                    userSeries = false;
            }
            if (CCreditors == "Customer Creditors")
            {
                try
                {
                    DataTable dtCustomer = db.GetTable("select distinct CustomerId,CustomerName from RemainingPayment where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                    if (dtCustomer.Rows.Count > 0)
                    {
                        for (int k = 0; k < dtCustomer.Rows.Count; k++)
                        {
                            customerid = Convert.ToInt32(dtCustomer.Rows[k][0].ToString());
                            CustName = Convert.ToString(dtCustomer.Rows[k][1].ToString());
                            remainingpayrepo1 = new RemainingPaymentRepository();
                            List<RemainingPayment> remainingamt1 = remainingpayrepo1.GetAll().Where(t => t.CustomerId == Convert.ToInt32(customerid) && t.Customername == Convert.ToString(CustName) && t.CreateDate.Value.Date >= fdate.Date && t.CreateDate.Value.Date <= tdate.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                            decimal netamt1 = Convert.ToDecimal(remainingpayrepo1.GetAll().Where(t => t.CustomerId == Convert.ToInt32(customerid) && t.Customername == Convert.ToString(CustName) && t.CreateDate.Value.Date >= fdate.Date && t.CreateDate.Value.Date <= tdate.Date && (t.Type != "Receipt") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmt));
                            decimal Paidamt1 = Convert.ToDecimal(remainingpayrepo1.GetAll().Where(t => t.CustomerId == Convert.ToInt32(customerid) && t.Customername == Convert.ToString(CustName) && t.CreateDate.Value.Date >= fdate.Date && t.CreateDate.Value.Date <= tdate.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt));
                            if (remainingamt1 != null)
                                remainingAmt = (Convert.ToDecimal(netamt1) - Convert.ToDecimal(Paidamt1));
                            if (remainingAmt < 0)
                            {
                                totalRem1 = totalRem1 + remainingAmt;
                                TNetAmt = TNetAmt + netamt1;
                                TPaidAmt = TPaidAmt + Paidamt1;
                                List<RemainingPayment> customerdata = remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(customerid) && t.Customername == Convert.ToString(CustName) && t.CreateDate.Value.Date >= fdate.Date && t.CreateDate.Value.Date <= tdate.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).OrderBy(t => t.CreateDate.Value.Date).ToList();
                                int i = 0;
                                if (customerdata != null)
                                {
                                    foreach (var item in customerdata)
                                    {
                                        if (item.Type == "BillReturn")
                                        {
                                            var totalprnet = remainingpayrepo.GetAll().Where(t => t.BillId == Convert.ToString(item.BillId) && t.Type == "BillReturn" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmt);
                                            var totalprpaid = remainingpayrepo.GetAll().Where(t => t.BillId == Convert.ToString(item.BillId) && t.Type == "BillReturn" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt);
                                            string billdate = item.CreateDate.Value.ToShortDateString();
                                            custdata = custmastrepo.GetAll().Where(t => t.BillNo == item.BillId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                            try
                                            {
                                                BillreceiptNO = custdata.BillRcptNo;
                                            }
                                            catch (Exception)
                                            {
                                                BillreceiptNO = "";
                                            }
                                            string custname = item.Customername;
                                            string netamt = Convert.ToString(totalprnet);
                                            string paidamt = Convert.ToString(totalprpaid);
                                            string remainingamt = "0";
                                            string type = Convert.ToString(item.Type);
                                            string billno = "";
                                            if (userSeries == false)
                                                billno = Convert.ToString(item.BillId);
                                            else
                                                billno = Convert.ToString(custdata.UserSeries);
                                            if (item.Narration == null || item.Narration == "")
                                                Narration = "";
                                            else
                                                Narration = Convert.ToString(item.Narration);
                                            if (item.ManualReceiptNo == null || item.ManualReceiptNo == "")
                                                ManualRecptNo = "";
                                            else
                                                ManualRecptNo = Convert.ToString(item.ManualReceiptNo);
                                            string paymentmode = item.PaymentMode;
                                            if (paymentmode == "Cash")
                                            {
                                                bankname = "";
                                                chequeno = "";
                                                chequedate = null;
                                            }
                                            if (paymentmode == null)
                                            {
                                                bankname = "";
                                                chequeno = "";
                                                chequedate = null;
                                            }
                                            if (paymentmode == "Cheque")
                                            {
                                                bankname = item.Bankname;
                                                chequeno = item.Chequeno;
                                                chequedate = item.CreateDate.Value.ToShortDateString();
                                            }
                                            dt = ds.Tables["ReceiptInfo"];
                                            dt.Rows.Add(new object[] { BillreceiptNO, custname, netamt, paidamt, remainingamt, paymentmode, bankname, chequeno, chequedate, billdate, type, billno, Narration, ManualRecptNo });
                                            i++;
                                        }
                                        else if (item.Type == "Bill")
                                        {
                                            custdata = custmastrepo.GetAll().Where(t => t.BillNo == item.BillId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                            string billdate = "";
                                            try
                                            {
                                                billdate = custdata.Billdate.Value.ToShortDateString();
                                            }
                                            catch (Exception)
                                            {
                                                billdate = null;
                                            }
                                            try
                                            {
                                                BillreceiptNO = custdata.BillRcptNo;
                                            }
                                            catch (Exception)
                                            {
                                                BillreceiptNO = "";
                                            }
                                            string custname = custdata.CustomerName;
                                            string netamt = Convert.ToString(custdata.NetAmount);
                                            string paidamt = Convert.ToString(custdata.PaidAmount);
                                            string remainingamt = Convert.ToString(custdata.RemainingAmount);
                                            string type = Convert.ToString(item.Type);
                                            string billno = "";
                                            if (userSeries == false)
                                                billno = Convert.ToString(item.BillId);
                                            else
                                                billno = Convert.ToString(custdata.UserSeries);
                                            if (item.Narration == null || item.Narration == "")
                                                Narration = "";
                                            else
                                                Narration = Convert.ToString(item.Narration);
                                            if (item.ManualReceiptNo == null || item.ManualReceiptNo == "")
                                                ManualRecptNo = "";
                                            else
                                                ManualRecptNo = Convert.ToString(item.ManualReceiptNo);
                                            string paymentmode = item.PaymentMode;
                                            if (paymentmode == "Cash")
                                            {
                                                bankname = "";
                                                chequeno = "";
                                                chequedate = null;
                                            }
                                            if (paymentmode == null)
                                            {
                                                bankname = "";
                                                chequeno = "";
                                                chequedate = null;
                                            }
                                            if (paymentmode == "Cheque")
                                            {
                                                bankname = item.Bankname;
                                                chequeno = item.Chequeno;
                                                chequedate = item.CreateDate.Value.ToShortDateString();
                                            }
                                            dt = ds.Tables["ReceiptInfo"];
                                            dt.Rows.Add(new object[] { BillreceiptNO, custname, netamt, paidamt, remainingamt, paymentmode, bankname, chequeno, chequedate, billdate, type, billno, Narration, ManualRecptNo });
                                            i++;
                                        }
                                        else
                                        {
                                            string billdate = item.CreateDate.Value.ToShortDateString();
                                            BillreceiptNO = Convert.ToString(item.BillReceiptNo);
                                            string custname = item.Customername;
                                            string type = Convert.ToString(item.Type);
                                            string billno = "";
                                            if (item.Narration == null || item.Narration == "")
                                                Narration = "";
                                            else
                                                Narration = Convert.ToString(item.Narration);
                                            if (item.ManualReceiptNo == null || item.ManualReceiptNo == "")
                                                ManualRecptNo = "";
                                            else
                                                ManualRecptNo = Convert.ToString(item.ManualReceiptNo);
                                            string paymentmode = item.PaymentMode;
                                            if (paymentmode == "Cash")
                                            {
                                                bankname = "";
                                                chequeno = "";
                                                chequedate = null;
                                            }
                                            if (paymentmode == null)
                                            {
                                                bankname = "";
                                                chequeno = "";
                                                chequedate = null;
                                            }
                                            if (paymentmode == "Cheque")
                                            {
                                                bankname = item.Bankname;
                                                chequeno = item.Chequeno;
                                                chequedate = item.CreateDate.Value.ToShortDateString();
                                            }
                                            if (paymentmode == "NEFT" || paymentmode == "RTGS")
                                            {
                                                bankname = item.Bankname;
                                                chequeno = item.Chequeno;
                                                chequedate = null;
                                            }
                                            if (userSeries == false)
                                                billno = Convert.ToString(item.BillId);
                                            else
                                                billno = Convert.ToString(custdata.UserSeries);
                                            string netamt = Convert.ToString(item.NetAmt);
                                            string paidamt = Convert.ToString(item.PaidAmt);
                                            string remainingamt = Convert.ToString(item.RemainingAmt);
                                            if (item.Type == "Receipt")
                                                dt = ds.Tables["ReceiptInfo"];
                                            else
                                            {
                                                paidamt = Convert.ToString(item.PaidAmt);
                                                remainingamt = Convert.ToString(item.RemainingAmt);
                                            }
                                            dt = ds.Tables["ReceiptInfo"];
                                            dt.Rows.Add(new object[] { BillreceiptNO, custname, netamt, paidamt, remainingamt, paymentmode, bankname, chequeno, chequedate, billdate, type, billno, Narration, ManualRecptNo });
                                            goto b;
                                        a:
                                            {
                                                dt = ds.Tables["ReceiptInfo"];
                                                dt.Rows[index]["billno"] = billno;
                                                dt.Rows[index]["paidamt"] = paidamt;
                                                dt.Rows[index]["remainingamt"] = remainingamt;
                                                dt.Rows[index]["paymentmode"] = paymentmode;
                                            }
                                        b:
                                            {
                                                i++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    totalremainingamount = Convert.ToString(totalRem1);
                    remainingpayrepo1.Dispose();
                    receiptrpt = new ReceiptCrystalReport1();
                    receiptrpt.SetDataSource(ds);
                    receiptrpt.SetParameterValue("TotalRemainingAmount", totalremainingamount);
                    receiptrpt.SetParameterValue("TotalNetAmount", TNetAmt);
                    receiptrpt.SetParameterValue("TotalPaidAmount", TPaidAmt);
                    receiptrpt.SetParameterValue("Flag", "Hide");
                    receiptrpt.SetParameterValue("AfterDateAmt", AfterDateRemAmt);
                    receiptrpt.SetParameterValue("ShowPrevAmt", "Hide");
                    crystalReportViewer1.ReportSource = receiptrpt;
                    db.CloseConnection();
                    custmastrepo.Dispose();
                    remainingpayrepo.Dispose();
                    ds.Dispose();
                    dt.Dispose();
                    dtCustomer.Dispose();
                }
                catch (Exception)
                { db.CloseConnection(); }
            }
            else
            {
                try
                {
                    DataTable dtCustomer = db.GetTable("select distinct CustomerId,CustomerName from RemainingPayment where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                    if (dtCustomer.Rows.Count > 0)
                    {
                        for (int k = 0; k < dtCustomer.Rows.Count; k++)
                        {
                            customerid = Convert.ToInt32(dtCustomer.Rows[k][0].ToString());
                            CustName = Convert.ToString(dtCustomer.Rows[k][1].ToString());
                            remainingpayrepo1 = new RemainingPaymentRepository();
                            List<RemainingPayment> remainingamt1 = remainingpayrepo1.GetAll().Where(t => t.CustomerId == Convert.ToInt32(customerid) && t.Customername == Convert.ToString(CustName) && t.CreateDate.Value.Date >= fdate.Date && t.CreateDate.Value.Date <= tdate.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                            decimal netamt1 = Convert.ToDecimal(remainingpayrepo1.GetAll().Where(t => t.CustomerId == Convert.ToInt32(customerid) && t.Customername == Convert.ToString(CustName) && t.CreateDate.Value.Date >= fdate.Date && t.CreateDate.Value.Date <= tdate.Date && (t.Type != "Receipt") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmt));
                            decimal Paidamt1 = Convert.ToDecimal(remainingpayrepo1.GetAll().Where(t => t.CustomerId == Convert.ToInt32(customerid) && t.Customername == Convert.ToString(CustName) && t.CreateDate.Value.Date >= fdate.Date && t.CreateDate.Value.Date <= tdate.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt));
                            if (remainingamt1 != null)
                                remainingAmt = (Convert.ToDecimal(netamt1) - Convert.ToDecimal(Paidamt1));
                            if (remainingAmt > 0)
                            {
                                totalRem1 = totalRem1 + remainingAmt;
                                TNetAmt = TNetAmt + netamt1;
                                TPaidAmt = TPaidAmt + Paidamt1;
                                List<RemainingPayment> customerdata = remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(customerid) && t.Customername == Convert.ToString(CustName) && t.CreateDate.Value.Date >= fdate.Date && t.CreateDate.Value.Date <= tdate.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).OrderBy(t => t.CreateDate.Value.Date).ToList();
                                int i = 0;
                                if (customerdata != null)
                                {
                                    foreach (var item in customerdata)
                                    {
                                        if (item.Type == "BillReturn")
                                        {
                                            var totalprnet = remainingpayrepo.GetAll().Where(t => t.BillId == Convert.ToString(item.BillId) && t.Type == "BillReturn" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmt);
                                            var totalprpaid = remainingpayrepo.GetAll().Where(t => t.BillId == Convert.ToString(item.BillId) && t.Type == "BillReturn" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt);
                                            string billdate = item.CreateDate.Value.ToShortDateString();
                                            custdata = custmastrepo.GetAll().Where(t => t.BillNo == item.BillId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                            try
                                            {
                                                BillreceiptNO = custdata.BillRcptNo;
                                            }
                                            catch (Exception)
                                            {
                                                BillreceiptNO = "";
                                            }
                                            string custname = item.Customername;
                                            string netamt = Convert.ToString(totalprnet);
                                            string paidamt = Convert.ToString(totalprpaid);
                                            string remainingamt = "0";
                                            string type = Convert.ToString(item.Type);
                                            string billno = "";
                                            if (userSeries == false)
                                                billno = Convert.ToString(item.BillId);
                                            else
                                                billno = Convert.ToString(custdata.UserSeries);
                                            if (item.Narration == null || item.Narration == "")
                                                Narration = "";
                                            else
                                                Narration = Convert.ToString(item.Narration);
                                            if (item.ManualReceiptNo == null || item.ManualReceiptNo == "")
                                                ManualRecptNo = "";
                                            else
                                                ManualRecptNo = Convert.ToString(item.ManualReceiptNo);
                                            string paymentmode = item.PaymentMode;
                                            if (paymentmode == "Cash")
                                            {
                                                bankname = "";
                                                chequeno = "";
                                                chequedate = null;
                                            }
                                            if (paymentmode == null)
                                            {
                                                bankname = "";
                                                chequeno = "";
                                                chequedate = null;
                                            }
                                            if (paymentmode == "Cheque")
                                            {
                                                bankname = item.Bankname;
                                                chequeno = item.Chequeno;
                                                chequedate = item.CreateDate.Value.ToShortDateString();
                                            }
                                            dt = ds.Tables["ReceiptInfo"];
                                            dt.Rows.Add(new object[] { BillreceiptNO, custname, netamt, paidamt, remainingamt, paymentmode, bankname, chequeno, chequedate, billdate, type, billno, Narration, ManualRecptNo });
                                            i++;
                                        }
                                        else if (item.Type == "Bill")
                                        {
                                            custdata = custmastrepo.GetAll().Where(t => t.BillNo == item.BillId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                            string billdate = "";
                                            try
                                            {
                                                billdate = custdata.Billdate.Value.ToShortDateString();
                                            }
                                            catch (Exception)
                                            {
                                                billdate = null;
                                            }
                                            try
                                            {
                                                BillreceiptNO = custdata.BillRcptNo;
                                            }
                                            catch (Exception)
                                            {
                                                BillreceiptNO = "";
                                            }
                                            string custname = custdata.CustomerName;
                                            string netamt = Convert.ToString(custdata.NetAmount);
                                            string paidamt = Convert.ToString(custdata.PaidAmount);
                                            string remainingamt = Convert.ToString(custdata.RemainingAmount);
                                            string type = Convert.ToString(item.Type);
                                            string billno = "";
                                            if (userSeries == false)
                                                billno = Convert.ToString(item.BillId);
                                            else
                                                billno = Convert.ToString(custdata.UserSeries);
                                            if (item.Narration == null || item.Narration == "")
                                                Narration = "";
                                            else
                                                Narration = Convert.ToString(item.Narration);
                                            if (item.ManualReceiptNo == null || item.ManualReceiptNo == "")
                                                ManualRecptNo = "";
                                            else
                                                ManualRecptNo = Convert.ToString(item.ManualReceiptNo);
                                            string paymentmode = item.PaymentMode;
                                            if (paymentmode == "Cash")
                                            {
                                                bankname = "";
                                                chequeno = "";
                                                chequedate = null;
                                            }
                                            if (paymentmode == null)
                                            {
                                                bankname = "";
                                                chequeno = "";
                                                chequedate = null;
                                            }
                                            if (paymentmode == "Cheque")
                                            {
                                                bankname = item.Bankname;
                                                chequeno = item.Chequeno;
                                                chequedate = item.CreateDate.Value.ToShortDateString();
                                            }
                                            dt = ds.Tables["ReceiptInfo"];
                                            dt.Rows.Add(new object[] { BillreceiptNO, custname, netamt, paidamt, remainingamt, paymentmode, bankname, chequeno, chequedate, billdate, type, billno, Narration, ManualRecptNo });
                                            i++;
                                        }
                                        else
                                        {
                                            string billdate = item.CreateDate.Value.ToShortDateString();
                                            BillreceiptNO = Convert.ToString(item.BillReceiptNo);
                                            string custname = item.Customername;
                                            string type = Convert.ToString(item.Type);
                                            string billno = "";
                                            if (item.Narration == null || item.Narration == "")
                                                Narration = "";
                                            else
                                                Narration = Convert.ToString(item.Narration);
                                            if (item.ManualReceiptNo == null || item.ManualReceiptNo == "")
                                                ManualRecptNo = "";
                                            else
                                                ManualRecptNo = Convert.ToString(item.ManualReceiptNo);
                                            string paymentmode = item.PaymentMode;
                                            if (paymentmode == "Cash")
                                            {
                                                bankname = "";
                                                chequeno = "";
                                                chequedate = null;
                                            }
                                            if (paymentmode == null)
                                            {
                                                bankname = "";
                                                chequeno = "";
                                                chequedate = null;
                                            }
                                            if (paymentmode == "Cheque")
                                            {
                                                bankname = item.Bankname;
                                                chequeno = item.Chequeno;
                                                chequedate = item.CreateDate.Value.ToShortDateString();
                                            }
                                            if (paymentmode == "NEFT" || paymentmode == "RTGS")
                                            {
                                                bankname = item.Bankname;
                                                chequeno = item.Chequeno;
                                                chequedate = null;
                                            }
                                            if (userSeries == false)
                                                billno = Convert.ToString(item.BillId);
                                            else
                                                billno = Convert.ToString(custdata.UserSeries);
                                            string netamt = Convert.ToString(item.NetAmt);
                                            string paidamt = Convert.ToString(item.PaidAmt);
                                            string remainingamt = Convert.ToString(item.RemainingAmt);
                                            if (item.Type == "Receipt")
                                                dt = ds.Tables["ReceiptInfo"];
                                            else
                                            {
                                                paidamt = Convert.ToString(item.PaidAmt);
                                                remainingamt = Convert.ToString(item.RemainingAmt);
                                            }
                                            dt = ds.Tables["ReceiptInfo"];
                                            dt.Rows.Add(new object[] { BillreceiptNO, custname, netamt, paidamt, remainingamt, paymentmode, bankname, chequeno, chequedate, billdate, type, billno, Narration, ManualRecptNo });
                                            goto b;
                                        a:
                                            {
                                                dt = ds.Tables["ReceiptInfo"];
                                                dt.Rows[index]["billno"] = billno;
                                                dt.Rows[index]["paidamt"] = paidamt;
                                                dt.Rows[index]["remainingamt"] = remainingamt;
                                                dt.Rows[index]["paymentmode"] = paymentmode;
                                            }
                                        b:
                                            {
                                                i++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    totalremainingamount = Convert.ToString(totalRem1);
                    remainingpayrepo1.Dispose();
                    receiptrpt = new ReceiptCrystalReport1();
                    receiptrpt.SetDataSource(ds);
                    receiptrpt.SetParameterValue("TotalRemainingAmount", totalremainingamount);
                    receiptrpt.SetParameterValue("TotalNetAmount", TNetAmt);
                    receiptrpt.SetParameterValue("TotalPaidAmount", TPaidAmt);
                    receiptrpt.SetParameterValue("Flag", "Hide");
                    receiptrpt.SetParameterValue("AfterDateAmt", AfterDateRemAmt);
                    receiptrpt.SetParameterValue("ShowPrevAmt", "Hide");
                    crystalReportViewer1.ReportSource = receiptrpt;
                    db.CloseConnection();
                    custmastrepo.Dispose();
                    remainingpayrepo.Dispose();
                    ds.Dispose();
                    dt.Dispose();
                }
                catch (Exception)
                { db.CloseConnection(); }
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                int custID = Convert.ToInt32(cmbcustomer.SelectedValue);
                string custname = cmbcustomer.Text;
            a:
                {
                    string input = "";
                    input = Microsoft.VisualBasic.Interaction.InputBox("1 For Normal Print and 2 For Credit Day Wise print", "Remaining Payment", "", 250, 250);
                    if (strFlag == "Customer")
                    {
                        if (input == "1" || input == "")
                            loaddata("Customer", custID, 0, "", "", "", "", "", "All", "", 0);
                        else if (input == "2")
                            loaddataCreditDay(custID, custname);
                        else
                            goto a;
                    }
                    else if (strFlag == "Supplier")
                    {
                        if (input == "1" || input == "")
                            loaddata("Supplier", custID, 0, "", "", "", "", "", "All", "", 0);
                        else if (input == "2")
                            loadSupplierdataCreditDay(custID, custname);
                        else
                            goto a;
                    }
                }
            }
            catch (Exception)
            { }
        }
    }
}