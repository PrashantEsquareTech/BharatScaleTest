using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptRemainingPayment : Form
    {
        DbClass db = new DbClass();
        RemainingPaymentCrystalReport1 rempayrpt = new RemainingPaymentCrystalReport1();

        public RptRemainingPayment()
        {
            InitializeComponent();
            loadData("Load", DateTime.Now, DateTime.Now, "", "");
        }

        //public RptRemainingPayment(string customertype)
        //{
        //    InitializeComponent();
        //    getremainingpayment(customertype);
        //}

        //public RptRemainingPayment(string flag, string villagename)
        //{
        //    InitializeComponent();
        //    getremainingpayment(flag, villagename);
        //}

        //public RptRemainingPayment(string flag, DateTime fdate, DateTime tdate)
        //{
        //    InitializeComponent();
        //    getremainingpayment(flag, fdate, tdate);
        //}

        public RptRemainingPayment(string flag, DateTime fdate, DateTime tdate, string villagename, string customertype)
        {
            InitializeComponent();
            loadData(flag, fdate, tdate, villagename, customertype);
        }

        public void loadData(string flag, DateTime fdate, DateTime tdate, string villagename, string customertype)
        {
            try
            {
                db.connect();
                DSRemainingPayment ds = new DSRemainingPayment();
                SqlDataAdapter sda1;
                sda1 = new SqlDataAdapter("SPremainingpayment", db.Connection);
                sda1.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda1.SelectCommand.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                sda1.SelectCommand.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                sda1.SelectCommand.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = flag;
                if (flag == "DateWise")
                {
                    CommonMethod com = new CommonMethod();
                    string toDate = com.ValidToDate(tdate);
                    string fromDate = com.ValidFromDate(fdate);
                    sda1.SelectCommand.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = fromDate;
                    sda1.SelectCommand.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = toDate;
                }
                if (villagename != "")
                {
                   
                    sda1.SelectCommand.Parameters.Add("@Village", SqlDbType.NVarChar).Value = villagename;
                }
                if (customertype != "")
                    sda1.SelectCommand.Parameters.Add("@CustomerType", SqlDbType.NVarChar).Value = customertype;
                
                sda1.Fill(ds, "CustomerCredit");
                if (flag == "DateWise")
                {
                    CustomerBalanceDateWise rempayrpt = new CustomerBalanceDateWise();
                    rempayrpt.SetDataSource(ds);
                    rempayrpt.SetParameterValue("FromDate", fdate);
                    rempayrpt.SetParameterValue("Todate", tdate);
                    crystalReportViewer1.ReportSource = rempayrpt;
                }
                else
                {
                    rempayrpt = new RemainingPaymentCrystalReport1();
                    rempayrpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = rempayrpt;
                }
                db.CloseConnection();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }
        
        //public void getremainingpayment(string flag, string villagename)
        //{
        //    try
        //    {
        //        db.connect();
        //        DSRemainingPayment ds = new DSRemainingPayment();
        //        SqlDataAdapter sda1;
        //        sda1 = new SqlDataAdapter("SPremainingpayment", db.Connection);
        //        sda1.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        sda1.SelectCommand.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
        //        sda1.SelectCommand.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
        //        sda1.SelectCommand.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = flag;
        //        sda1.SelectCommand.Parameters.Add("@Village", SqlDbType.NVarChar).Value = villagename;
        //        sda1.Fill(ds, "CustomerCredit");
        //        rempayrpt = new RemainingPaymentCrystalReport1();
        //        rempayrpt.SetDataSource(ds);

        //        crystalReportViewer1.ReportSource = rempayrpt;
        //        db.CloseConnection();
        //        ds.Dispose();
        //    }
        //    catch (Exception)
        //    { db.CloseConnection(); }
        //}

        //public void getremainingpayment(string flag, DateTime fdate, DateTime tdate)
        //{
        //    try
        //    {
        //        db.connect();
        //        DSRemainingPayment ds = new DSRemainingPayment();
        //        SqlDataAdapter sda1;
        //        sda1 = new SqlDataAdapter("SPremainingpayment", db.Connection);
        //        sda1.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        sda1.SelectCommand.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
        //        sda1.SelectCommand.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
        //        sda1.SelectCommand.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = flag;
        //        if (flag == "DateWise")
        //        {
        //            CommonMethod com = new CommonMethod();
        //            string toDate = com.ValidToDate(tdate);
        //            string fromDate = com.ValidFromDate(fdate);
        //            sda1.SelectCommand.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = fromDate;
        //            sda1.SelectCommand.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = toDate;
        //        }
        //        sda1.Fill(ds, "CustomerCredit");
        //        CustomerBalanceDateWise rempayrpt1 = new CustomerBalanceDateWise();
        //        rempayrpt1.SetDataSource(ds);
        //        rempayrpt1.SetParameterValue("FromDate", fdate);
        //        rempayrpt1.SetParameterValue("Todate", tdate);

        //        crystalReportViewer1.ReportSource = rempayrpt1;
        //        db.CloseConnection();
        //        ds.Dispose();
        //    }
        //    catch (Exception)
        //    { db.CloseConnection(); }
        //}

        //public void getremainingpayment(string customertype)
        //{
        //    try
        //    {
        //        string flag = "CustomerType";
        //        db.connect();
        //        DSRemainingPayment ds = new DSRemainingPayment();
        //        SqlDataAdapter sda1;
        //        sda1 = new SqlDataAdapter("SPremainingpayment", db.Connection);
        //        sda1.SelectCommand.CommandType = CommandType.StoredProcedure;
        //        sda1.SelectCommand.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
        //        sda1.SelectCommand.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
        //        sda1.SelectCommand.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = flag;
        //        sda1.SelectCommand.Parameters.Add("@CustomerType", SqlDbType.NVarChar).Value = customertype;
        //        sda1.Fill(ds, "CustomerCredit");
        //        rempayrpt = new RemainingPaymentCrystalReport1();
        //        rempayrpt.SetDataSource(ds);

        //        crystalReportViewer1.ReportSource = rempayrpt;
        //        db.CloseConnection();
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