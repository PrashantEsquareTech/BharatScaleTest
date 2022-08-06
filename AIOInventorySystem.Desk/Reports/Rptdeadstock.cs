using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class Rptdeadstock : Form
    {
        DbClass db = new DbClass();

        public Rptdeadstock()
        {
            InitializeComponent();
            getdeadstock();
        }

        public void getdeadstock()
        {
            try
            {
                db.connect();
                SqlDataAdapter da;

                DSdeadstock ds = new DSdeadstock();
                da = new SqlDataAdapter("Select * from DeadProductMaster where TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "DeadProductMaster");

                da = new SqlDataAdapter("Select d.* from DeadProductDetail d inner join DeadProductMaster m on d.DeadNo=m.Id where m.TransactionYear='" + CommonMethod.TransactionYear + "'  and m.CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "DeadProductDetail");

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId, db.Connection);

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ProductInformation");

                Deadstock deadrep = new Deadstock();
                deadrep.SetDataSource(ds);
                crystalReportViewer1.ReportSource = deadrep;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                ds.Dispose();
                da.Dispose();
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
