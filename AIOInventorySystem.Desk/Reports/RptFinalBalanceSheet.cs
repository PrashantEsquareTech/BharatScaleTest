using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptFinalBalanceSheet : Form
    {
        DbClass db = new DbClass();
        BalanceSheetCrystalReport BalSheetRpt = new BalanceSheetCrystalReport();
        CommonMethod cm = new CommonMethod();

        public RptFinalBalanceSheet()
        {
            InitializeComponent();
        }

        public RptFinalBalanceSheet(string vat, DateTime fdate, DateTime todate)
        {
            InitializeComponent();
            ShowBill(vat, fdate, todate);
        }

        public void ShowBill(string vat, DateTime fdate, DateTime todate)
        {
            try
            {
                db.connect();
                DSTradingnPLAC dstplac = new DSTradingnPLAC();
                SqlDataAdapter sda;

                sda = new SqlDataAdapter("SPNewBalanceSheet", db.Connection);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;

                sda.SelectCommand.Parameters.Add("@Vat", SqlDbType.VarChar).Value = vat;
                sda.SelectCommand.Parameters.Add("@FDate", SqlDbType.VarChar).Value = cm.ValidFromDate(fdate.Date);
                sda.SelectCommand.Parameters.Add("@CDate", SqlDbType.VarChar).Value = cm.ValidToDate(todate.Date);
                sda.SelectCommand.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                sda.SelectCommand.Parameters.Add("@PreTransactionYear", SqlDbType.VarChar).Value = CommonMethod.PreTransactionYear;
                sda.SelectCommand.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                sda.SelectCommand.CommandTimeout = 300;
                sda.Fill(dstplac, "TradingnPLAC");

                DataTable dt = new DataTable();
                dt = dstplac.Tables["TradingnPLAC"];
                decimal DrTotal = Convert.ToDecimal(dt.Compute("Sum(GAmountDr)", ""));
                decimal CrTotal = Convert.ToDecimal(dt.Compute("Sum(GAmountCr)", ""));

                sda = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                sda.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                sda.Fill(dstplac, "CompanyInformation");

                BalSheetRpt = new BalanceSheetCrystalReport();
                BalSheetRpt.SetDataSource(dstplac);
                BalSheetRpt.SetParameterValue("Fdate", fdate);
                BalSheetRpt.SetParameterValue("TDate", todate);
                BalSheetRpt.SetParameterValue("DrTotal", DrTotal);
                BalSheetRpt.SetParameterValue("CrTotal", CrTotal);
                crystalReportViewer1.ReportSource = BalSheetRpt;
                db.CloseConnection();
                dstplac.Dispose();
                sda.Dispose();
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
    }
}