using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptDailySP : Form
    {
        DbClass db = new DbClass();
        DailySPCrystalReport drcrreport = new DailySPCrystalReport();
        CounterCashAllCrystalReport countercashrpt = new CounterCashAllCrystalReport();
        CommonMethod cm = new CommonMethod();

        public RptDailySP()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            dailyspreport(dtpfromdate.Value);
        }

        public RptDailySP(DateTime fdate)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            dailyspreport(fdate);
        }

        public RptDailySP(DateTime fromdt, DateTime todt)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            dailyspreport(fromdt, todt);
        }

        public void dailyspreport(DateTime fromdt, DateTime todt)
        {
            try
            {
                DSCounterCashAll ds = new DSCounterCashAll();
                SqlDataAdapter sda;

                sda = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                sda.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                sda.Fill(ds, "CompanyInformation");
                string toDate = cm.ValidToDate(todt);
                string fromDate = cm.ValidFromDate(fromdt);
                sda = new SqlDataAdapter("Select * from CounterCash where Date between '" + fromDate + "' and '" + toDate + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                sda.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                sda.Fill(ds, "CounterCash");

                countercashrpt = new CounterCashAllCrystalReport();
                countercashrpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = countercashrpt;
                ds.Dispose();
                sda.Dispose();
            }
            catch (Exception)
            { }
        }

        public void dailyspreport(DateTime fdate)
        {
            //New SP Logic
            try
            {
                string fromDate = cm.ValidFromDate(fdate.Date), toDate = cm.ValidToDate(fdate.Date);

                db.connect();
                DSDayBook ds = new DSDayBook();
                SqlDataAdapter sda;

                sda = new SqlDataAdapter("SPCounterCashBook", db.Connection);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@Side", SqlDbType.VarChar).Value = "Dr";
                sda.SelectCommand.Parameters.Add("@FDate", SqlDbType.VarChar).Value = fromDate;
                sda.SelectCommand.Parameters.Add("@CDate", SqlDbType.VarChar).Value = toDate;
                sda.SelectCommand.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                sda.Fill(ds, "CashBookDr");

                sda = new SqlDataAdapter("SPCounterCashBook", db.Connection);
                sda.SelectCommand.CommandType = CommandType.StoredProcedure;
                sda.SelectCommand.Parameters.Add("@Side", SqlDbType.VarChar).Value = "Cr";
                sda.SelectCommand.Parameters.Add("@FDate", SqlDbType.VarChar).Value = fromDate;
                sda.SelectCommand.Parameters.Add("@CDate", SqlDbType.VarChar).Value = toDate;
                sda.SelectCommand.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                sda.Fill(ds, "CashBookCr");
                db.CloseConnection();

                sda = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                sda.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                sda.Fill(ds, "CompanyInformation");

                drcrreport = new DailySPCrystalReport();
                drcrreport.SetDataSource(ds);

                CounterCashRepository CounterCashRepo = new CounterCashRepository();
                CounterCash CounterCashData = new CounterCash();
                CounterCashData = CounterCashRepo.GetAll().Where(t => t.Date.Value.ToString("dd/MM/yyyy") == fdate.ToString("dd/MM/yyyy") && t.CompId == CommonMethod.CompId).FirstOrDefault();
                drcrreport.SetParameterValue("CDate", CounterCashData.Date);
                drcrreport.SetParameterValue("OpeningCash", CounterCashData.OpeningCash);
                drcrreport.SetParameterValue("ClosingCash", CounterCashData.ClosingCash);
                crystalReportViewer1.ReportSource = drcrreport;
                ds.Dispose();
                sda.Dispose();
            }
            catch (Exception)
            {
                db.CloseConnection();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpfromdate.Value.Date == dtptodate.Value.Date)
                    dailyspreport(dtpfromdate.Value);
                else if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                    dailyspreport(dtpfromdate.Value.Date, dtptodate.Value.Date);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
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