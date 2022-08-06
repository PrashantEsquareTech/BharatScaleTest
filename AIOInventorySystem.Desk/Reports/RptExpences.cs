using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptExpences : Form
    {
        public int ExpenceId;
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        ExpenceCrystalReport1 expencerpt;

        public RptExpences()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            getreport();
        }

        public RptExpences(int expenceNo)
        {
            ExpenceId = expenceNo;
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            getreport(ExpenceId);
        }

        public RptExpences(string expencename)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            getreport(expencename);
        }

        public RptExpences(string from, string to)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            betweenreport(from, to);
        }

        public RptExpences(string expencename, string from, string to)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            betweenreport(expencename, from, to);
        }

        public RptExpences(DateTime fdate, DateTime tdate, string ExpenceName1, string ACCName1)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            betweenreport(fdate, tdate, ExpenceName1, ACCName1);
        }

        public void getreport()
        {
            try
            {
                DSExpence ds = new DSExpence();
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from Expences where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Expences");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                expencerpt = new ExpenceCrystalReport1();
                expencerpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = expencerpt;
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void getreport(int expenceno)
        {
            try
            {
                DSExpence ds = new DSExpence();
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from Expences where ExpenceID=" + ExpenceId + " and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Expences");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                expencerpt = new ExpenceCrystalReport1();
                expencerpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = expencerpt;
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void getreport(string expencename)
        {
            try
            {
                DSExpence ds = new DSExpence();
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from Expences where ExpenceName='" + expencename + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Expences");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                expencerpt = new ExpenceCrystalReport1();
                expencerpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = expencerpt;
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void betweenreport(string from, string to)
        {
            try
            {
                DSExpence ds = new DSExpence();
                SqlDataAdapter da;

                da = new SqlDataAdapter("Select * from Expences where ExpenceDate between '" + cm.ValidFromDate(Convert.ToDateTime(from)) + "' and '" + cm.ValidToDate(Convert.ToDateTime(to)) + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Expences");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                expencerpt = new ExpenceCrystalReport1();
                expencerpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = expencerpt;
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void betweenreport(string expencename, string from, string to)
        {
            try
            {
                DSExpence ds = new DSExpence();
                SqlDataAdapter da;

                da = new SqlDataAdapter("Select * from Expences where ExpenceDate between '" + cm.ValidFromDate(Convert.ToDateTime(from)) + "' and '" + cm.ValidToDate(Convert.ToDateTime(to)) + "' and ExpenceName='" + expencename + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Expences");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                expencerpt = new ExpenceCrystalReport1();
                expencerpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = expencerpt;
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                {
                    DSExpence ds = new DSExpence();
                    SqlDataAdapter da;
                    da = new SqlDataAdapter("Select * from Expences where ExpenceDate between '" + cm.ValidFromDate(dtpfromdate.Value.Date) + "' and '" + cm.ValidToDate(dtptodate.Value.Date) + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "' ", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "Expences");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    expencerpt = new ExpenceCrystalReport1();
                    expencerpt.SetDataSource(ds);

                    crystalReportViewer1.ReportSource = expencerpt;
                    db.CloseConnection();
                    da.Dispose();
                    ds.Dispose();
                }
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void betweenreport(DateTime fdate, DateTime tdate, string Expencename1, string ACCName1)
        {
            try
            {
                DSExpence ds = new DSExpence();
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from Expences e inner join AccountingGroupMaster m on e.ACGId=m.Id where ExpenceDate between '" + cm.ValidFromDate(Convert.ToDateTime(fdate.Date)) + "' and '" + cm.ValidToDate(Convert.ToDateTime(tdate.Date)) + "' and ExpenceName='" + Expencename1 + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "' and ACGId In (Select Id from AccountingGroupMaster where ACGName='" + ACCName1 + "')", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "Expences");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                expencerpt = new ExpenceCrystalReport1();
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