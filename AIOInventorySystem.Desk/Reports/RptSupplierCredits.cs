using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using System.Data.SqlClient;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptSupplierCredits : Form
    {
        DbClass db = new DbClass();
        rptSupplierCredits rempayrpt = new rptSupplierCredits();

        public RptSupplierCredits()
        {
            InitializeComponent();
        }

        public RptSupplierCredits(string input, DateTime frmdate, DateTime todate)
        {
            InitializeComponent();
            loadData(input, frmdate, todate);
            //if (input == "1")
            //    getremainingpayment();
            //else if (input == "2")
            //    getremainingpaymentCreditDayWise();
        }

        //public RptSupplierCredits()
        //{
        //    InitializeComponent();
        //    getremainingpaymentInBetweenDateWise(frmdate, todate);
        //}

        public void loadData(string flag, DateTime frmdate, DateTime todate)
        {
            try
            {
                db.connect();
                DsSupplierCredits ds = new DsSupplierCredits();
                SqlDataAdapter sda1;
                sda1 = new SqlDataAdapter("SPSuppRemainingPayment", db.Connection);
                sda1.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda1.SelectCommand.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                sda1.SelectCommand.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                if (flag == "1")
                    sda1.SelectCommand.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "Load";
                else if (flag == "2")
                    sda1.SelectCommand.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "CreditList";
                else if (flag == "3")
                {
                    sda1.SelectCommand.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "DateWise";
                    CommonMethod com = new CommonMethod();
                    string fromDate = com.ValidFromDate(frmdate);
                    string toDate = com.ValidToDate(todate);
                    sda1.SelectCommand.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = fromDate;
                    sda1.SelectCommand.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = toDate;
                }
                sda1.Fill(ds, "CustomerCredit");
                rempayrpt = new rptSupplierCredits();
                rempayrpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = rempayrpt;
                db.CloseConnection();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        //public void getremainingpayment()
        //{
        //    try
        //    {
        //        DsSupplierCredits ds = new DsSupplierCredits();
        //        DataTable dt = new DataTable();
        //        DataTable dt1 = db.GetTable("Select Distinct(SupplierId) from SupplierRemainingPayment where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
        //        if (dt1.Rows.Count > 0)
        //        {
        //            int j = 1;
        //            for (int i = 0; i < dt1.Rows.Count; i++)
        //            {
        //                try
        //                {
        //                    SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
        //                    int suptid = Convert.ToInt32(dt1.Rows[i]["SupplierId"].ToString());
        //                    SupplierRepository supplierRepo = new SupplierRepository();
        //                    SupplierInformation supplierData = new SupplierInformation();
        //                    if (CommonMethod.commProduct == true)
        //                        supplierData = supplierRepo.GetAll().Where(t => t.SupplierCode == suptid).FirstOrDefault();
        //                    else
        //                        supplierData = supplierRepo.GetAll().Where(t => t.SupplierCode == suptid && t.CompId == CommonMethod.CompId).FirstOrDefault();
        //                    List<SupplierRemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(dt1.Rows[i]["SupplierId"].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
        //                    decimal netamt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(dt1.Rows[i]["SupplierId"].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.Type != "Receipt").Sum(t => t.NetAmt));
        //                    decimal Paidamt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(dt1.Rows[i]["SupplierId"].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt));
        //                    string remainigamt = Convert.ToString(netamt - Paidamt);
        //                    if (remainigamt != "0.00")
        //                    {
        //                        string cusatname = supplierData.SupplierName;
        //                        string cusataddress = supplierData.SupplierAddress;
        //                        string cno = supplierData.ContactNo;
        //                        string remamt = remainigamt;
        //                        dt = ds.Tables["RemainingPayment"];
        //                        dt.Rows.Add(new object[5] { j, cusatname, cusataddress, cno, remamt });
        //                        j++;
        //                    }
        //                    netamt = 0; Paidamt = 0; remainigamt = "0";
        //                    remainingpayrepo.Dispose(); supplierRepo.Dispose();
        //                }
        //                catch (Exception)
        //                { }
        //            }
        //        }
        //        rempayrpt = new rptSupplierCredits();
        //        rempayrpt.SetDataSource(ds);
        //        crystalReportViewer1.ReportSource = rempayrpt;
        //        db.CloseConnection();

        //        dt.Dispose();
        //        dt1.Dispose();
        //        ds.Dispose();
        //    }
        //    catch (Exception)
        //    { db.CloseConnection(); }
        //}

        //public void getremainingpaymentCreditDayWise()
        //{
        //    try
        //    {
        //        DsSupplierCredits ds = new DsSupplierCredits();
        //        DataTable dt = new DataTable();
        //        DataTable dt1 = db.GetTable("Select Distinct(SupplierId) from SupplierRemainingPayment where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
        //        if (dt1.Rows.Count > 0)
        //        {
        //            int j = 1;
        //            for (int i = 0; i < dt1.Rows.Count; i++)
        //            {
        //                try
        //                {
        //                    SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
        //                    int suptid = Convert.ToInt32(dt1.Rows[i]["SupplierId"].ToString());
        //                    SupplierRepository supplierRepo = new SupplierRepository();
        //                    SupplierInformation supplierData = new SupplierInformation();
        //                    if (CommonMethod.commProduct == true)
        //                        supplierData = supplierRepo.GetAll().Where(t => t.SupplierCode == suptid).FirstOrDefault();
        //                    else
        //                        supplierData = supplierRepo.GetAll().Where(t => t.SupplierCode == suptid && t.CompId == CommonMethod.CompId).FirstOrDefault();
        //                    int cdays = 0;
        //                    if (!string.IsNullOrEmpty(Convert.ToString(supplierData.CreditDays)))
        //                        cdays = Convert.ToInt32(supplierData.CreditDays);
        //                    else
        //                        cdays = 0;
        //                    List<SupplierRemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(dt1.Rows[i]["SupplierId"].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
        //                    decimal netamt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(dt1.Rows[i]["SupplierId"].ToString()) && (DateTime.Now - t.CreateDate.Value).Days >= cdays && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.Type != "Receipt").Sum(t => t.NetAmt));
        //                    decimal Paidamt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(dt1.Rows[i]["SupplierId"].ToString()) && (DateTime.Now - t.CreateDate.Value).Days >= cdays && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt));
        //                    string remainigamt = Convert.ToString(netamt - Paidamt);
        //                    if (remainigamt != "0.00")
        //                    {
        //                        string cusatname = supplierData.SupplierName;
        //                        string cusataddress = supplierData.SupplierAddress;
        //                        string cno = supplierData.ContactNo;
        //                        string remamt = remainigamt;
        //                        dt = ds.Tables["RemainingPayment"];
        //                        dt.Rows.Add(new object[5] { j, cusatname, cusataddress, cno, remamt });
        //                        j++;
        //                    }
        //                    netamt = 0; Paidamt = 0; remainigamt = "0";
        //                    remainingpayrepo.Dispose(); supplierRepo.Dispose();
        //                }
        //                catch (Exception)
        //                { }
        //            }
        //        }
        //        rempayrpt = new rptSupplierCredits();
        //        rempayrpt.SetDataSource(ds);
        //        crystalReportViewer1.ReportSource = rempayrpt;
        //        db.CloseConnection();
        //        dt.Dispose();
        //        dt1.Dispose();
        //        ds.Dispose();
        //    }
        //    catch (Exception)
        //    { db.CloseConnection(); }
        //}

        //public void getremainingpaymentInBetweenDateWise(DateTime fdate, DateTime tdate)
        //{
        //    try
        //    {
        //        DsSupplierCredits ds = new DsSupplierCredits();
        //        DataTable dt = new DataTable();
        //        DataTable dt1 = db.GetTable("Select Distinct(SupplierId) from SupplierRemainingPayment where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
        //        if (dt1.Rows.Count > 0)
        //        {
        //            int j = 1;
        //            for (int i = 0; i < dt1.Rows.Count; i++)
        //            {
        //                try
        //                {
        //                    SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
        //                    int suptid = Convert.ToInt32(dt1.Rows[i]["SupplierId"].ToString());
        //                    SupplierRepository supplierRepo = new SupplierRepository();
        //                    SupplierInformation supplierData = new SupplierInformation();
        //                    if (CommonMethod.commProduct == true)
        //                        supplierData = supplierRepo.GetAll().Where(t => t.SupplierCode == suptid).FirstOrDefault();
        //                    else
        //                        supplierData = supplierRepo.GetAll().Where(t => t.SupplierCode == suptid && t.CompId == CommonMethod.CompId).FirstOrDefault();
        //                    List<SupplierRemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(dt1.Rows[i]["SupplierId"].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
        //                    decimal netamt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(dt1.Rows[i]["SupplierId"].ToString()) && t.CreateDate.Value >= fdate && t.CreateDate.Value <= tdate && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.Type != "Receipt").Sum(t => t.NetAmt));
        //                    decimal Paidamt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(dt1.Rows[i]["SupplierId"].ToString()) && t.CreateDate.Value >= fdate && t.CreateDate.Value <= tdate && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt));
        //                    decimal netamtOld = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(dt1.Rows[i]["SupplierId"].ToString()) && t.CreateDate.Value <= fdate && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.Type != "Receipt").Sum(t => t.NetAmt));
        //                    decimal Paidamtold = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(dt1.Rows[i]["SupplierId"].ToString()) && t.CreateDate.Value <= fdate && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt));
        //                    decimal remainigamt = Convert.ToDecimal(netamt - Paidamt);
        //                    decimal remainigamtold = Convert.ToDecimal(netamtOld - Paidamtold);
        //                    if (remainigamt != 0)
        //                    {
        //                        string cusatname = supplierData.SupplierName;
        //                        string cusataddress = supplierData.SupplierAddress;
        //                        string cno = supplierData.ContactNo;
        //                        string remamt = Convert.ToString(remainigamt + remainigamtold);
        //                        dt = ds.Tables["RemainingPayment"];
        //                        dt.Rows.Add(new object[7] { j, cusatname, cusataddress, cno, remamt, remainigamtold, remainigamt });
        //                        j++;
        //                    }
        //                    netamt = 0; Paidamt = 0; remainigamt = 0; remainigamtold = 0;
        //                    remainingpayrepo.Dispose(); supplierRepo.Dispose();
        //                }
        //                catch (Exception)
        //                { }
        //            }
        //        }
        //        SupplierBalanceDateWise rempayrpt1 = new SupplierBalanceDateWise();
        //        rempayrpt1.SetDataSource(ds);
        //        rempayrpt1.SetParameterValue("FromDate", fdate);
        //        rempayrpt1.SetParameterValue("Todate", tdate);
        //        crystalReportViewer1.ReportSource = rempayrpt1;
        //        db.CloseConnection();
        //        dt.Dispose();
        //        dt1.Dispose();
        //        ds.Dispose();
        //    }
        //    catch (Exception)
        //    { db.CloseConnection(); }
        //}

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
    }
}