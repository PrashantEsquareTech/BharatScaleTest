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
    public partial class RptFakeBillReceipt : Form
    {
        DbClass db = new DbClass();
        ReceiptCrystalReport1 receiptrpt;
        CustomerReceiptrpt customerreceiptrpt;
        public int index;
        public string totalremainingamount;
        public decimal TNetAmt = 0, TPaidAmt = 0;

        public RptFakeBillReceipt()
        {
            InitializeComponent();
            customerdata();
        }

        public RptFakeBillReceipt(string bankname)
        {
            InitializeComponent();
            loaddata(bankname);
            customerdata();
        }

        public RptFakeBillReceipt(int custid, string customername)
        {
            InitializeComponent();
            loaddata(custid, customername);
            customerdata();
        }

        public RptFakeBillReceipt(string receiptid, string customername, int custid)
        {
            string CustId = receiptid;
            InitializeComponent();
            loaddata(receiptid, customername, custid);
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

        public void loaddata()
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                List<RemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                TNetAmt = Convert.ToDecimal(remainingamt.Sum(t => t.NetAmt));
                TPaidAmt = Convert.ToDecimal(remainingamt.Sum(t => t.PaidAmt));
                if (remainingamt != null)
                    totalremainingamount = Convert.ToString(TNetAmt - TPaidAmt);

                DSReceipt ds = new DSReceipt();
                da = new SqlDataAdapter("Select * from RemainingPayment where TransactionYear = '" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "RemainingPayment");
                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                receiptrpt = new ReceiptCrystalReport1();
                receiptrpt.SetDataSource(ds);
                receiptrpt.SetParameterValue("TotalRemainingAmount", totalremainingamount);
                receiptrpt.SetParameterValue("TotalNetAmount", TNetAmt);
                receiptrpt.SetParameterValue("TotalPaidAmount", TPaidAmt);
                crystalReportViewer1.ReportSource = receiptrpt;

                crystalReportViewer1.Refresh();
                db.CloseConnection();
                remainingpayrepo.Dispose();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void loaddata(string bankname)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                List<RemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.Bankname == bankname && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                if (remainingamt != null)
                    totalremainingamount = "0";

                DSReceipt ds = new DSReceipt();
                da = new SqlDataAdapter("Select * from RemainingPayment where Bankname='" + bankname + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "RemainingPayment");
                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                receiptrpt = new ReceiptCrystalReport1();
                receiptrpt.SetDataSource(ds);
                receiptrpt.SetParameterValue("TotalRemainingAmount", totalremainingamount);
                receiptrpt.SetParameterValue("TotalNetAmount", TNetAmt);
                receiptrpt.SetParameterValue("TotalPaidAmount", TPaidAmt);
                crystalReportViewer1.ReportSource = receiptrpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                remainingpayrepo.Dispose();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void loaddata(int custid, string customername)
        {
            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPremainingpayment", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "CustRem";
                command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = Convert.ToInt32(custid);

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
            try
            {
                string bankname = "", chequedate = null, chequeno = "", receiptdate;
                CustomerBillMasterRepository custmastrepo = new CustomerBillMasterRepository();
                CustomerBillMaster custdata = new CustomerBillMaster();
                FakeRemainingPaymentRepository remainingpayrepo = new FakeRemainingPaymentRepository();
                List<FakeRemainingPayment> customerdata = remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(custid) && (t.Type == "Bill" || t.Type == "OpeningBalance" || t.Type == "Receipt" || t.Type == "BillReturn") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                DSReceipt ds = new DSReceipt();
                DataTable dt = new DataTable();
                int i = 0;
                if (customerdata != null)
                {
                    foreach (var item in customerdata)
                    {
                        if (item.Type == "BillReturn")
                        {
                            var totalprnet = remainingpayrepo.GetAll().Where(t => t.BillId == Convert.ToString(item.BillId) && (t.Type == "SRSubDiff" || t.Type == "BillReturn") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmt);
                            var totalprpaid = remainingpayrepo.GetAll().Where(t => t.BillId == Convert.ToString(item.BillId) && (t.Type == "SRSubDiff" || t.Type == "BillReturn") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt);
                            string billdate = item.CreateDate.Value.ToShortDateString();
                            receiptdate = "";
                            string custname = item.Customername;
                            string netamt = Convert.ToString(totalprnet);
                            string paidamt = Convert.ToString(totalprpaid);
                            string remainingamt = "0";
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
                            dt.Rows.Add(new object[] { receiptdate, custname, netamt, paidamt, remainingamt, paymentmode, bankname, chequeno, chequedate, billdate, type, billno });
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
                            receiptdate = "";
                            string custname = custdata.CustomerName;
                            string netamt = Convert.ToString(custdata.NetAmount);
                            string paidamt = Convert.ToString(custdata.PaidAmount);
                            string remainingamt = Convert.ToString(custdata.RemainingAmount);
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
                            dt.Rows.Add(new object[] { receiptdate, custname, netamt, paidamt, remainingamt, paymentmode, bankname, chequeno, chequedate, billdate, type, billno });
                            i++;
                        }
                        else
                        {
                            string billdate = "";
                            try
                            {
                                receiptdate = item.CreateDate.Value.ToShortDateString();
                            }
                            catch (Exception)
                            {
                                receiptdate = null;
                            }
                            string custname = item.Customername;
                            string type = Convert.ToString(item.Type);
                            string billno = "";
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
                            billno = Convert.ToString(item.BillId);
                            string netamt = Convert.ToString(item.NetAmt);
                            string paidamt = Convert.ToString(item.PaidAmt);
                            string remainingamt = Convert.ToString(item.RemainingAmt);
                            if (item.Type == "Receipt")
                            {
                                dt = ds.Tables["ReceiptInfo"];
                                if (dt.Rows.Count > 0)
                                {
                                    for (int k = 0; k < dt.Rows.Count; k++)
                                    {
                                        if (dt.Rows[k][0].ToString() != "")
                                        {
                                            if (Convert.ToDateTime(dt.Rows[k][0].ToString()) == Convert.ToDateTime(receiptdate) && dt.Rows[k][10].ToString() == "Receipt")
                                            {
                                                billno = dt.Rows[k][11].ToString() + "," + Convert.ToString(item.BillId);
                                                paidamt = Convert.ToString(Convert.ToDecimal(dt.Rows[k][3].ToString()) + Convert.ToDecimal(item.PaidAmt));
                                                remainingamt = Convert.ToString(Convert.ToDecimal(dt.Rows[k][4].ToString()) + Convert.ToDecimal(item.RemainingAmt));
                                                index = k;
                                                goto a;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                paidamt = Convert.ToString(item.PaidAmt);
                                remainingamt = Convert.ToString(item.RemainingAmt);
                            }
                            dt = ds.Tables["ReceiptInfo"];
                            dt.Rows.Add(new object[] { receiptdate, custname, netamt, paidamt, remainingamt, paymentmode, bankname, chequeno, chequedate, billdate, type, billno });
                            goto b;
                        a:
                            {
                                dt = ds.Tables["ReceiptInfo"];
                                dt.Rows[index][11] = billno;
                                dt.Rows[index][3] = paidamt;
                                dt.Rows[index][4] = remainingamt;
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

        public void loaddata(string receiptid, string customername, int custid)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                try
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPremainingpayment", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                    command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "CustRem";
                    command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = Convert.ToInt32(custid);

                    DataTable dt1 = new DataTable();
                    dt1.Load(command.ExecuteReader());
                    db.CloseConnection();
                    totalremainingamount = Convert.ToString(dt1.Compute("Sum(TotalReaminingAmount)", ""));
                    dt1.Dispose();
                }
                catch (Exception)
                { db.CloseConnection(); }
                DSReceipt ds = new DSReceipt();
                da = new SqlDataAdapter("Select * from RemainingPayment where BillReceiptNo='" + receiptid + "' and TransactionYear = '" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "RemainingPayment");
                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                customerreceiptrpt = new CustomerReceiptrpt();
                customerreceiptrpt.SetDataSource(ds);
                customerreceiptrpt.SetParameterValue("TotalRemainingAmount", totalremainingamount);
                crystalReportViewer1.ReportSource = customerreceiptrpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
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

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                loaddata(Convert.ToInt32(cmbcustomer.SelectedValue), cmbcustomer.Text);
            }
            catch (Exception)
            { }
        }
    }
}