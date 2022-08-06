using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptCashBook : Form
    {
        DbClass db = new DbClass();
        CashBookReport Cash_BookRpt = new CashBookReport();
        CommonMethod cm = new CommonMethod();

        public RptCashBook()
        {
            InitializeComponent();
        }

        public RptCashBook(DateTime fdate, DateTime todate)
        {
            InitializeComponent();
            ShowBill(fdate, todate);
        }

        public void ShowBill(DateTime fdate, DateTime todate)
        {
            try
            {
                string fromDate = cm.ValidFromDate(fdate.Date), toDate = cm.ValidToDate(todate.Date);

                db.connect();
                DsCashBook ds = new DsCashBook();
                SqlDataAdapter sda;

                sda = new SqlDataAdapter("SPCashBook", db.Connection);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@Side", SqlDbType.VarChar).Value = "Dr";
                sda.SelectCommand.Parameters.Add("@FDate", SqlDbType.VarChar).Value = fromDate;
                sda.SelectCommand.Parameters.Add("@CDate", SqlDbType.VarChar).Value = toDate;
                sda.SelectCommand.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                sda.SelectCommand.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                sda.Fill(ds, "CashBookDr");

                sda = new SqlDataAdapter("SPCashBook", db.Connection);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@Side", SqlDbType.VarChar).Value = "Cr";
                sda.SelectCommand.Parameters.Add("@FDate", SqlDbType.VarChar).Value = fromDate;
                sda.SelectCommand.Parameters.Add("@CDate", SqlDbType.VarChar).Value = toDate;
                sda.SelectCommand.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                sda.SelectCommand.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                sda.Fill(ds, "CashBookCr");

                sda = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                sda.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                sda.Fill(ds, "CompanyInformation");

                Cash_BookRpt = new CashBookReport();
                Cash_BookRpt.SetDataSource(ds);
                Cash_BookRpt.SetParameterValue("Fdate", fdate);
                Cash_BookRpt.SetParameterValue("TDate", todate);
                crystalReportViewer1.ReportSource = Cash_BookRpt;
                db.CloseConnection();
                sda.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            {
                db.CloseConnection();
            }
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
    }
}