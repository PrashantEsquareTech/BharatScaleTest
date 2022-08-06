using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptBankinfo : Form
    {
        DbClass db = new DbClass();
        BankInfoCrystalReport bankrpt = new BankInfoCrystalReport();

        public RptBankinfo()
        {
            InitializeComponent();
            getdata();
        }

        public void getdata()
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                BankinfoDS ds = new BankinfoDS();
                da = new SqlDataAdapter("Select * from BankInformation where CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "BankInformation");
                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                bankrpt = new BankInfoCrystalReport();
                bankrpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = bankrpt;
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
    }
}