using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptTradingnPLAC : Form
    {
        DbClass db = new DbClass();
        TradingnPLACCrystalReport TPLACRpt = new TradingnPLACCrystalReport();
        CommonMethod cm = new CommonMethod();

        public RptTradingnPLAC()
        {
            InitializeComponent();
        }

        public RptTradingnPLAC(string vat, DateTime fdate, DateTime todate)
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
                sda = new SqlDataAdapter("SPTradingnProfitLossAC", db.Connection);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@Flag", SqlDbType.VarChar).Value = 't';
                sda.SelectCommand.Parameters.Add("@Vat", SqlDbType.VarChar).Value = vat;
                sda.SelectCommand.Parameters.Add("@FDate", SqlDbType.VarChar).Value = cm.ValidFromDate(fdate.Date);
                sda.SelectCommand.Parameters.Add("@CDate", SqlDbType.VarChar).Value = cm.ValidToDate(todate.Date);
                sda.SelectCommand.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                sda.SelectCommand.Parameters.Add("@PreTransactionYear", SqlDbType.VarChar).Value = CommonMethod.PreTransactionYear;
                sda.SelectCommand.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                sda.SelectCommand.CommandTimeout = 1000;
                sda.Fill(dstplac, "TradingnPLAC");

                DataTable dt = new DataTable();
                dt = dstplac.Tables["TradingnPLAC"];
                decimal PLAmt;
                PLAmt = Convert.ToDecimal(dt.Rows[dt.Rows.Count - 2][4].ToString());

                sda = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                sda.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                sda.Fill(dstplac, "CompanyInformation");

                TPLACRpt = new TradingnPLACCrystalReport();
                TPLACRpt.SetDataSource(dstplac);
                TPLACRpt.SetParameterValue("Fdate", fdate);
                TPLACRpt.SetParameterValue("TDate", todate);
                TPLACRpt.SetParameterValue("pl", PLAmt);
                crystalReportViewer1.ReportSource = TPLACRpt;
                db.CloseConnection();
                dstplac.Dispose();
                sda.Dispose();
                dt.Dispose();
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