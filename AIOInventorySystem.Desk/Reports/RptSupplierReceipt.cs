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
    public partial class RptSupplierReceipt : Form
    {
        DbClass db = new DbClass();
        SupplierReceiptCrystalReport srpt;
        public string bankname, chequedate, chequeno, totalremainingamount, Narration;
        public decimal TNetAmt = 0, TPaidAmt = 0, totalRem1 = 0, AfterDateRemAmt = 0;
        public int index;

        public RptSupplierReceipt()
        {
            InitializeComponent();
            customerdata();
        }

        public RptSupplierReceipt(int custid, string suppliername)
        {
            InitializeComponent();
            customerdata();
        a:
            {
                string input = "";
                input = Microsoft.VisualBasic.Interaction.InputBox("1 For Normal Print and 2 For Credit Day Wise print", "Remaining Payment", "", 250, 250);
                if (input == "1" || input == "")
                    loaddata(custid, suppliername);
                else if (input == "2")
                    loaddataCreditDay(custid, suppliername);
                else
                    goto a;
            }
        }

        public RptSupplierReceipt(int custid, string suppliername, DateTime fromdate, DateTime todate)
        {
            InitializeComponent();
            customerdata();
            loaddata(custid, suppliername, fromdate, todate);
        }

        public RptSupplierReceipt(string Type, DateTime fromdate, DateTime todate)
        {
            InitializeComponent();
            customerdata();
            loaddata(Type, fromdate, todate);
        }

        public RptSupplierReceipt(string Type)
        {
            InitializeComponent();
            loaddata(Type);
            customerdata();
        }

        public RptSupplierReceipt(string Type, string suppname)
        {
            InitializeComponent();
            customerdata();
            loaddata(Type, suppname);
        }

        public RptSupplierReceipt(string Type, DateTime fdate1, DateTime tdate1, string booktype2)
        {
            InitializeComponent();
            customerdata();
            loaddata(Type, fdate1, tdate1, booktype2);
        }

        public RptSupplierReceipt(DateTime fdate, DateTime tdate)
        {
            InitializeComponent();
            customerdata();
            loaddata(fdate, tdate);
        }

        public RptSupplierReceipt(DateTime fdate, DateTime tdate, string SCreditors)
        {
            InitializeComponent();
            customerdata();
            loaddata(fdate, tdate, SCreditors);
        }

        public void loaddata(int custid, string suppliername, DateTime fromdate, DateTime todate)
        {
            try
            {
                CommonMethod com = new CommonMethod();
                string toDate = com.ValidToDate(todate.Date);
                string fromDate = com.ValidFromDate(fromdate.Date);
                db.connect();
                //Stored Procedure code
                SqlCommand command = new SqlCommand("SPSuppRemainingPayment", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "SuppDate";
                command.Parameters.Add("@SupplierID", SqlDbType.NVarChar).Value = Convert.ToInt32(custid);
                command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = fromDate;
                command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = toDate;
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
                SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                List<SupplierRemainingPayment> remainingamt1 = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(custid) && t.CreateDate.Value.Date < fromdate.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).OrderBy(t => t.CreateDate).ToList();
                var netamt1 = remainingamt1.Where(t => t.Type != "Receipt").Sum(t => t.NetAmt);
                var Paidamt1 = remainingamt1.Sum(t => t.PaidAmt);
                if (remainingamt1 != null)
                    AfterDateRemAmt = Convert.ToDecimal(netamt1) - Convert.ToDecimal(Paidamt1);
                remainingpayrepo1.Dispose();
            }
            catch (Exception)
            { }
            try
            {
                PurchaseMasterRepository purchaseMastRepo = new PurchaseMasterRepository();
                PurchaseMaster purdata = new PurchaseMaster();
                SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                List<SupplierRemainingPayment> customerdata = remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(custid) && t.CreateDate.Value.Date >= fromdate.Date && t.CreateDate.Value.Date <= todate.Date && (t.Type == "Purchase" || t.Type == "OpeningBalance" || t.Type == "Receipt" || t.Type == "PurchaseReturn" || t.Type == "Adjustment" || t.Type == "Credit Note" || t.Type == "Debit Note" || t.Type == "Send To" || t.Type == "Receive From") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).OrderBy(t => t.CreateDate.Value.Date).ToList();
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
                srpt = new SupplierReceiptCrystalReport();
                srpt.SetDataSource(ds);
                srpt.SetParameterValue("TotalRemainingAmount", totalremainingamount);
                srpt.SetParameterValue("TNetAmt", TNetAmt);
                srpt.SetParameterValue("TPaidAmt", TPaidAmt);
                srpt.SetParameterValue("Flag", "Show");
                srpt.SetParameterValue("ShowPrevAmt", "Show");
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

        public void customerdata()
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

        public void loaddata1(string bankname)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                List<SupplierRemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.Bankname == bankname && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                if (remainingamt != null)
                    totalremainingamount = "0";

                DSSupplierReceipt ds = new DSSupplierReceipt();
                da = new SqlDataAdapter("Select * from SupplierRemainingPayment where Bankname='" + bankname + "' and TransactionYear = '" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "SupplierRemainingPayment");
                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                srpt = new SupplierReceiptCrystalReport();
                srpt.SetDataSource(ds);
                srpt.SetParameterValue("TotalRemainingAmount", totalremainingamount);
                srpt.SetParameterValue("TNetAmt", TNetAmt);
                srpt.SetParameterValue("TPaidAmt", TPaidAmt);

                crystalReportViewer1.ReportSource = srpt;

                crystalReportViewer1.Refresh();
                db.CloseConnection();
                remainingpayrepo.Dispose();
                da.Dispose(); ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void loaddata(int custid, string suppliername)
        {
            totalRem1 = 0;
            try
            {
                db.connect();
                //Stored Procedure code
                SqlCommand command = new SqlCommand("SPSuppRemainingPayment", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "SuppRem";
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
                List<SupplierRemainingPayment> customerdata = remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(custid) && (t.Type == "Purchase" || t.Type == "OpeningBalance" || t.Type == "Receipt" || t.Type == "PurchaseReturn" || t.Type == "Adjustment" || t.Type == "Credit Note" || t.Type == "Debit Note" || t.Type == "Send To" || t.Type == "Receive From") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).OrderBy(t => t.CreateDate.Value.Date).ToList();
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
                            totalRem1 = totalRem1 + (Convert.ToDecimal(netamt) - Convert.ToDecimal(paidamt));
                            string remainingamt = Convert.ToString(totalRem1);
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
                            totalRem1 = totalRem1 + (Convert.ToDecimal(purdata.NetAmount) - Convert.ToDecimal(purdata.PaidAmount));
                            string remainingamt = Convert.ToString(totalRem1);
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
                            totalRem1 = totalRem1 + (Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt));
                            string remainingamt = Convert.ToString(totalRem1);
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
                                remainingamt = Convert.ToString(totalRem1);
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
                srpt = new SupplierReceiptCrystalReport();
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

        public void loaddataCreditDay(int custid, string suppliername)
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
                srpt = new SupplierReceiptCrystalReport();
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

        public void loaddata(string Type)
        {
            try
            {
                db.connect();
                //Stored Procedure code
                SqlCommand command = new SqlCommand("SPSuppRemainingPayment", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "Type";
                command.Parameters.Add("@ReceiptType", SqlDbType.NVarChar).Value = Type;
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
                List<SupplierRemainingPayment> customerdata = remainingpayrepo.GetAll().Where(t => t.Type == Type && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).OrderBy(t => t.CreateDate.Value.Date).ToList();
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
                            string paymentmode = item.PaymentMode;
                            if (item.Narration == null || item.Narration == "")
                                Narration = "";
                            else
                                Narration = Convert.ToString(item.Narration);
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
                            string paymentmode = item.PaymentMode;
                            if (item.Narration == null || item.Narration == "")
                                Narration = "";
                            else
                                Narration = Convert.ToString(item.Narration);
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
                            if (item.Type == "Receipt" || item.Type == "Adjustment")
                            {
                                SqlDataAdapter da1 = new SqlDataAdapter("Select Distinct BillId from BillWiseSupplierReceipt where SupplierReceiptNo=" + item.Id + " and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' ", db.Connection);
                                DataTable dt1 = new DataTable();
                                da1.Fill(dt1);
                                string ConcateBillId = null;
                                for (int j = 0; j < dt1.Rows.Count; j++)
                                {
                                    if (!string.IsNullOrEmpty(ConcateBillId))
                                        ConcateBillId = ConcateBillId + "," + dt1.Rows[j][0].ToString();
                                    else
                                        ConcateBillId = dt1.Rows[j][0].ToString();
                                }
                                pono = ConcateBillId;
                            }
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
                srpt = new SupplierReceiptCrystalReport();
                srpt.SetDataSource(ds);
                srpt.SetParameterValue("TotalRemainingAmount", totalremainingamount);
                srpt.SetParameterValue("TNetAmt", TNetAmt);
                srpt.SetParameterValue("TPaidAmt", TPaidAmt);
                srpt.SetParameterValue("Flag", "Hide");
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

        public void loaddata(string Type, DateTime fromdate, DateTime todate)
        {
            try
            {
                CommonMethod com = new CommonMethod();
                string toDate = com.ValidToDate(todate.Date);
                string fromDate = com.ValidFromDate(fromdate.Date);
                db.connect();
                //Stored Procedure code
                SqlCommand command = new SqlCommand("SPSuppRemainingPayment", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "TypeDate";
                command.Parameters.Add("@ReceiptType", SqlDbType.NVarChar).Value = Type;
                command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = fromDate;
                command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = toDate;
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
                List<SupplierRemainingPayment> customerdata = remainingpayrepo.GetAll().Where(t => t.Type == Type && t.CreateDate.Value.Date >= fromdate.Date && t.CreateDate.Value.Date <= todate.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).OrderBy(t => t.CreateDate.Value.Date).ToList();
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
                            string paymentmode = item.PaymentMode;
                            if (item.Narration == null || item.Narration == "")
                                Narration = "";
                            else
                                Narration = Convert.ToString(item.Narration);
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
                            if (item.Type == "Receipt" || item.Type == "Adjustment")
                            {
                                SqlDataAdapter da1 = new SqlDataAdapter("Select Distinct BillId from BillWiseSupplierReceipt where SupplierReceiptNo=" + item.Id + " and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' ", db.Connection);
                                DataTable dt1 = new DataTable();
                                da1.Fill(dt1);
                                string ConcateBillId = null;
                                for (int j = 0; j < dt1.Rows.Count; j++)
                                {
                                    if (!string.IsNullOrEmpty(ConcateBillId))
                                        ConcateBillId = ConcateBillId + "," + dt1.Rows[j][0].ToString();
                                    else
                                        ConcateBillId = dt1.Rows[j][0].ToString();
                                }
                                pono = ConcateBillId;
                            }
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
                srpt = new SupplierReceiptCrystalReport();
                srpt.SetDataSource(ds);
                srpt.SetParameterValue("TotalRemainingAmount", totalremainingamount);
                srpt.SetParameterValue("TNetAmt", TNetAmt);
                srpt.SetParameterValue("TPaidAmt", TPaidAmt);
                srpt.SetParameterValue("Flag", "Hide");
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

        public void loaddata(string Type, string suppname)
        {
            try
            {
                SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                List<SupplierRemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.Type == Type && t.Suppliername == suppname && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                TNetAmt = Convert.ToDecimal(remainingamt.Sum(t => t.NetAmt));
                TPaidAmt = Convert.ToDecimal(remainingamt.Sum(t => t.PaidAmt));
                if (remainingamt != null)
                    totalremainingamount = Convert.ToString(TNetAmt - TPaidAmt);
                remainingpayrepo.Dispose();
            }
            catch (Exception)
            { }
            try
            {
                PurchaseMasterRepository purchaseMastRepo = new PurchaseMasterRepository();
                PurchaseMaster purdata = new PurchaseMaster();
                SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                List<SupplierRemainingPayment> customerdata = remainingpayrepo.GetAll().Where(t => t.Type == Type && t.Suppliername == suppname && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).OrderBy(t => t.CreateDate.Value.Date).ToList();
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
                            string paymentmode = item.PaymentMode;
                            if (item.Narration == null || item.Narration == "")
                                Narration = "";
                            else
                                Narration = Convert.ToString(item.Narration);
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
                            if (item.Type == "Receipt" || item.Type == "Adjustment")
                            {
                                SqlDataAdapter da1 = new SqlDataAdapter("Select Distinct BillId from BillWiseSupplierReceipt where SupplierReceiptNo=" + item.Id + " and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' ", db.Connection);
                                DataTable dt1 = new DataTable();
                                da1.Fill(dt1);
                                string ConcateBillId = null;
                                for (int j = 0; j < dt1.Rows.Count; j++)
                                {
                                    if (!string.IsNullOrEmpty(ConcateBillId))
                                        ConcateBillId = ConcateBillId + "," + dt1.Rows[j][0].ToString();
                                    else
                                        ConcateBillId = dt1.Rows[j][0].ToString();
                                }
                                pono = ConcateBillId;
                            }
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
                srpt = new SupplierReceiptCrystalReport();
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

        public void loaddata(string Type, DateTime fdate1, DateTime tdate1, string booktype2)
        {
            try
            {
                if (booktype2 == "Cash")
                {
                    try
                    {
                        SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                        List<SupplierRemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.CreateDate.Value.Date >= fdate1.Date && t.CreateDate.Value.Date <= tdate1.Date && t.Type == Type && t.PaymentMode == "Cash" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).OrderBy(t => t.CreateDate).ToList();
                        TNetAmt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.CreateDate.Value.Date >= fdate1.Date && t.CreateDate.Value.Date <= tdate1.Date && t.Type == Type && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmt));
                        TPaidAmt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.CreateDate.Value.Date >= fdate1.Date && t.CreateDate.Value.Date <= tdate1.Date && t.Type == Type && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt));
                        if (remainingamt != null)
                            totalremainingamount = Convert.ToString(TNetAmt - TPaidAmt);
                        remainingpayrepo.Dispose();
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        PurchaseMasterRepository purchaseMastRepo = new PurchaseMasterRepository();
                        PurchaseMaster purdata = new PurchaseMaster();
                        SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                        List<SupplierRemainingPayment> customerdata = remainingpayrepo.GetAll().Where(t => t.Type == Type && t.CreateDate.Value.Date >= fdate1.Date && t.CreateDate.Value.Date <= tdate1.Date && t.PaymentMode == "Cash" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).OrderBy(t => t.CreateDate.Value.Date).ToList();
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
                                    string paymentmode = item.PaymentMode;
                                    if (item.Narration == null || item.Narration == "")
                                        Narration = "";
                                    else
                                        Narration = Convert.ToString(item.Narration);
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
                                    if (item.Type == "Receipt" || item.Type == "Adjustment")
                                    {
                                        SqlDataAdapter da1 = new SqlDataAdapter("Select Distinct BillId from BillWiseSupplierReceipt where SupplierReceiptNo=" + item.Id + " and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' ", db.Connection);
                                        DataTable dt1 = new DataTable();
                                        da1.Fill(dt1);
                                        string ConcateBillId = null;
                                        for (int j = 0; j < dt1.Rows.Count; j++)
                                        {
                                            if (!string.IsNullOrEmpty(ConcateBillId))
                                                ConcateBillId = ConcateBillId + "," + dt1.Rows[j][0].ToString();
                                            else
                                                ConcateBillId = dt1.Rows[j][0].ToString();
                                        }
                                        pono = ConcateBillId;
                                    }
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
                        srpt = new SupplierReceiptCrystalReport();
                        srpt.SetDataSource(ds);
                        srpt.SetParameterValue("TotalRemainingAmount", totalremainingamount);
                        srpt.SetParameterValue("TNetAmt", TNetAmt);
                        srpt.SetParameterValue("TPaidAmt", TPaidAmt);
                        srpt.SetParameterValue("Flag", "Hide");
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
                else
                {
                    try
                    {
                        CommonMethod com = new CommonMethod();
                        string toDate = com.ValidToDate(tdate1.Date);
                        string fromDate = com.ValidFromDate(fdate1.Date);
                        db.connect();
                        //Stored Procedure code
                        SqlCommand command = new SqlCommand("SPSuppRemainingPayment", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                        command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "TypeDate";
                        command.Parameters.Add("@ReceiptType", SqlDbType.NVarChar).Value = Type;
                        command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = fromDate;
                        command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = toDate;
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
                        List<SupplierRemainingPayment> customerdata = remainingpayrepo.GetAll().Where(t => t.CreateDate.Value.Date >= fdate1.Date && t.CreateDate.Value.Date <= tdate1.Date && t.Type == Type && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).OrderBy(t => t.CreateDate).ToList();
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
                                    string paymentmode = item.PaymentMode;
                                    if (item.Narration == null || item.Narration == "")
                                        Narration = "";
                                    else
                                        Narration = Convert.ToString(item.Narration);
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
                                    if (item.Type == "Receipt" || item.Type == "Adjustment")
                                    {
                                        SqlDataAdapter da1 = new SqlDataAdapter("Select Distinct BillId from BillWiseSupplierReceipt where SupplierReceiptNo=" + item.Id + " and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' ", db.Connection);
                                        DataTable dt1 = new DataTable();
                                        da1.Fill(dt1);
                                        string ConcateBillId = null;
                                        for (int j = 0; j < dt1.Rows.Count; j++)
                                        {
                                            if (!string.IsNullOrEmpty(ConcateBillId))
                                                ConcateBillId = ConcateBillId + "," + dt1.Rows[j][0].ToString();
                                            else
                                                ConcateBillId = dt1.Rows[j][0].ToString();
                                        }
                                        pono = ConcateBillId;
                                    }
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
                        srpt = new SupplierReceiptCrystalReport();
                        srpt.SetDataSource(ds);
                        srpt.SetParameterValue("TotalRemainingAmount", totalremainingamount);
                        srpt.SetParameterValue("TNetAmt", TNetAmt);
                        srpt.SetParameterValue("TPaidAmt", TPaidAmt);
                        srpt.SetParameterValue("Flag", "Hide");
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
            }
            catch (Exception) { }
        }

        public void loaddata(DateTime fdate, DateTime tdate)
        {
            try
            {
                CommonMethod com = new CommonMethod();
                string toDate = com.ValidToDate(tdate.Date);
                string fromDate = com.ValidFromDate(fdate.Date);
                db.connect();
                //Stored Procedure code
                SqlCommand command = new SqlCommand("SPSuppRemainingPayment", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "Date";
                command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = fromDate;
                command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = toDate;
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
                List<SupplierRemainingPayment> customerdata = remainingpayrepo.GetAll().Where(t => t.CreateDate.Value.Date >= fdate.Date && t.CreateDate.Value.Date <= tdate.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).OrderBy(t => t.CreateDate.Value.Date).ToList();
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
                            string paymentmode = item.PaymentMode;
                            if (item.Narration == null || item.Narration == "")
                                Narration = "";
                            else
                                Narration = Convert.ToString(item.Narration);
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
                            if (item.Type == "Receipt" || item.Type == "Adjustment")
                            {
                                SqlDataAdapter da1 = new SqlDataAdapter("Select Distinct BillId from BillWiseSupplierReceipt where SupplierReceiptNo=" + item.Id + " and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' ", db.Connection);
                                DataTable dt1 = new DataTable();
                                da1.Fill(dt1);
                                string ConcateBillId = null;
                                for (int j = 0; j < dt1.Rows.Count; j++)
                                {
                                    if (!string.IsNullOrEmpty(ConcateBillId))
                                        ConcateBillId = ConcateBillId + "," + dt1.Rows[j][0].ToString();
                                    else
                                        ConcateBillId = dt1.Rows[j][0].ToString();
                                }
                                pono = ConcateBillId;
                            }
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
                srpt = new SupplierReceiptCrystalReport();
                srpt.SetDataSource(ds);
                srpt.SetParameterValue("TotalRemainingAmount", totalremainingamount);
                srpt.SetParameterValue("TNetAmt", TNetAmt);
                srpt.SetParameterValue("TPaidAmt", TPaidAmt);
                srpt.SetParameterValue("Flag", "Hide");
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

        public void loaddata(DateTime fdate, DateTime tdate, string SCreditors)
        {
            int supplierid;
            string suppliername = "";
            decimal remainingAmt1 = 0, totalRemAmt = 0;
            SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
            SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
            PurchaseMasterRepository purchaseMastRepo = new PurchaseMasterRepository();
            PurchaseMaster purdata = new PurchaseMaster();
            DSSupplierReceipt ds = new DSSupplierReceipt();
            DataTable dt = new DataTable();
            try
            {
                DataTable dtSupplier = db.GetTable("select distinct SupplierId,SupplierName from SupplierRemainingPayment where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                if (dtSupplier.Rows.Count > 0)
                {
                    for (int m = 0; m < dtSupplier.Rows.Count; m++)
                    {
                        supplierid = Convert.ToInt32(dtSupplier.Rows[m][0].ToString());
                        suppliername = Convert.ToString(dtSupplier.Rows[m][1].ToString());
                        remainingpayrepo = new SupplierRemainingPaymentRepository();
                        List<SupplierRemainingPayment> remainingamt1 = remainingpayrepo.GetAll().Where(t => t.SupplierId == supplierid && t.Suppliername == suppliername && t.CreateDate.Value.Date >= fdate.Date && t.CreateDate.Value.Date <= tdate.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).OrderBy(t => t.CreateDate).ToList();
                        var netamt1 = remainingpayrepo.GetAll().Where(t => t.SupplierId == supplierid && t.Suppliername == suppliername && t.CreateDate.Value.Date >= fdate.Date && t.CreateDate.Value.Date <= tdate.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmt);
                        var Paidamt = remainingpayrepo.GetAll().Where(t => t.SupplierId == supplierid && t.Suppliername == suppliername && t.CreateDate.Value.Date >= fdate.Date && t.CreateDate.Value.Date <= tdate.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt);
                        if (remainingamt1 != null)
                            remainingAmt1 = (Convert.ToDecimal(netamt1) - Convert.ToDecimal(Paidamt));
                        if (remainingAmt1 > 0)
                        {
                            totalRemAmt = totalRemAmt + remainingAmt1;
                            TNetAmt = TNetAmt + Convert.ToDecimal(netamt1);
                            TPaidAmt = TNetAmt + Convert.ToDecimal(Paidamt);
                            remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                            List<SupplierRemainingPayment> customerdata = remainingpayrepo.GetAll().Where(t => t.SupplierId == supplierid && t.Suppliername == suppliername && t.CreateDate.Value.Date >= fdate.Date && t.CreateDate.Value.Date <= tdate.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).OrderBy(t => t.CreateDate.Value.Date).ToList();
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
                                        string paymentmode = item.PaymentMode;
                                        if (item.Narration == null || item.Narration == "")
                                            Narration = "";
                                        else
                                            Narration = Convert.ToString(item.Narration);
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
                                        if (item.Type == "Receipt" || item.Type == "Adjustment")
                                        {
                                            SqlDataAdapter da1 = new SqlDataAdapter("Select Distinct BillId from BillWiseSupplierReceipt where SupplierReceiptNo=" + item.Id + " and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' ", db.Connection);
                                            DataTable dt1 = new DataTable();
                                            da1.Fill(dt1);
                                            string ConcateBillId = null;
                                            for (int j = 0; j < dt1.Rows.Count; j++)
                                            {
                                                if (!string.IsNullOrEmpty(ConcateBillId))
                                                    ConcateBillId = ConcateBillId + "," + dt1.Rows[j][0].ToString();
                                                else
                                                    ConcateBillId = dt1.Rows[j][0].ToString();
                                            }
                                            pono = ConcateBillId;
                                        }
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
                        }
                    }
                }
                totalremainingamount = Convert.ToString(totalRemAmt);
                remainingpayrepo1.Dispose();
                srpt = new SupplierReceiptCrystalReport();
                srpt.SetDataSource(ds);
                srpt.SetParameterValue("TotalRemainingAmount", totalremainingamount);
                srpt.SetParameterValue("TNetAmt", TNetAmt);
                srpt.SetParameterValue("TPaidAmt", TPaidAmt);
                srpt.SetParameterValue("Flag", "Hide");
                srpt.SetParameterValue("ShowPrevAmt", "Hide");
                srpt.SetParameterValue("AfterDateRemAmt", AfterDateRemAmt);
                crystalReportViewer1.ReportSource = srpt;
                db.CloseConnection();
                remainingpayrepo.Dispose();
                ds.Dispose();
                dt.Dispose();
                purchaseMastRepo.Dispose();
            }
            catch (Exception) { db.CloseConnection(); }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                int supp = Convert.ToInt32(cmbcustomer.SelectedValue);
                string suupname = cmbcustomer.Text;
            a:
                {
                    string input = "";
                    input = Microsoft.VisualBasic.Interaction.InputBox("1 For Normal Print and 2 For Credit Day Wise print", "Remaining Payment", "", 250, 250);
                    if (input == "1" || input == "")
                        loaddata(supp, suupname);
                    else if (input == "2")
                        loaddataCreditDay(supp, suupname);
                    else
                        goto a;
                }
            }
            catch (Exception)
            { }
        }
    }
}