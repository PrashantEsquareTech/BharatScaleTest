using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptOtherIncome : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        OtherIncomeReport expencerpt;

        public RptOtherIncome()
        {
            InitializeComponent();
            getreport();
        }

        public RptOtherIncome(int incomeno)
        {
            InitializeComponent();
            getreport(incomeno);
        }

        public RptOtherIncome(string expencename)
        {
            InitializeComponent();
            getreport(expencename);
        }

        public RptOtherIncome(string from, string to)
        {
            InitializeComponent();
            betweenreport(from, to);
        }

        public RptOtherIncome(string from, string to, string incomename1)
        {
            InitializeComponent();
            betweenreport(from, to, incomename1);
        }

        public RptOtherIncome(string incomename, DateTime fdate, DateTime tdate, string ACCGName)
        {
            InitializeComponent();
            getreport(incomename, fdate, tdate, ACCGName);
        }

        public void getreport(int incomeno)
        {
            try
            {
                DSOtherIncome ds = new DSOtherIncome();
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from Income where IncomeID='" + incomeno + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Income");
                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");
                da = new SqlDataAdapter("Select * from IncomeMaster where CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "IncomeMaster");
                expencerpt = new OtherIncomeReport();
                expencerpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = expencerpt;
                db.CloseConnection();
                ds.Dispose();
                da.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
                db.CloseConnection();
            }
        }

        public void betweenreport(string from, string to)
        {
            try
            {
                DSOtherIncome ds = new DSOtherIncome();
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from Income where   IncomeDate>='" + from + "' and  IncomeDate<='" + to + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Income");
                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");
                da = new SqlDataAdapter("Select * from IncomeMaster where CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "IncomeMaster");
                expencerpt = new OtherIncomeReport();
                expencerpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = expencerpt;
                db.CloseConnection();
                ds.Dispose();
                da.Dispose();
            }
            catch (Exception)
            {
                db.CloseConnection();
            }
        }

        public void getreport(string expencename)
        {
            try
            {
                DSOtherIncome ds = new DSOtherIncome();
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from Income where TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Income");

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                da = new SqlDataAdapter("Select * from IncomeMaster Where IncomeName='" + expencename + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "IncomeMaster");
                expencerpt = new OtherIncomeReport();
                expencerpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = expencerpt;
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void getreport()
        {
            try
            {
                DSOtherIncome ds = new DSOtherIncome();
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from Income where TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Income");
                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");
                da = new SqlDataAdapter("Select * from IncomeMaster where CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "IncomeMaster");
                expencerpt = new OtherIncomeReport();
                expencerpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = expencerpt;
                db.CloseConnection();
                ds.Dispose();
                da.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
                db.CloseConnection();
            }
        }

        public void betweenreport(string from, string to, string incomename1)
        {
            try
            {
                DSOtherIncome ds = new DSOtherIncome();
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from Income as inc inner join IncomeMaster as inm on inm.Id=inc.IncomeMID where IncomeDate>='" + cm.ValidFromDate(Convert.ToDateTime(from)) + "' and  IncomeDate<='" + cm.ValidToDate(Convert.ToDateTime(to)) + "' and inm.IncomeName='" + incomename1 + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and inc.CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Income");
                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");
                da = new SqlDataAdapter("Select * from IncomeMaster where CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "IncomeMaster");
                expencerpt = new OtherIncomeReport();
                expencerpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = expencerpt;
                db.CloseConnection();
                ds.Dispose();
                da.Dispose();
            }
            catch (Exception)
            {
                db.CloseConnection();
            }
        }

        public void getreport(string incomename, DateTime fdate, DateTime tdate, string ACCGName)
        {
            try
            {
                DSOtherIncome ds = new DSOtherIncome();
                SqlDataAdapter da;
                CommonMethod cm = new CommonMethod();
                da = new SqlDataAdapter("Select * from Income as i left join AccountingGroupMaster as ac on i.ACGId=ac.Id where TransactionYear='" + CommonMethod.TransactionYear + "'  and i.CompId='" + CommonMethod.CompId + "' and ACGName='" + ACCGName + "' and IncomeDate>='" + cm.ValidFromDate(fdate.Date) + "' and  IncomeDate<='" + cm.ValidToDate(tdate.Date) + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Income");

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                da = new SqlDataAdapter("Select * from IncomeMaster as im left join AccountingGroupMaster as ac on im.ACGId=ac.Id Where IncomeName='" + incomename + "' and ACGName='" + ACCGName + "' and im.CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "IncomeMaster");
                expencerpt = new OtherIncomeReport();
                expencerpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = expencerpt;
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
    }
}