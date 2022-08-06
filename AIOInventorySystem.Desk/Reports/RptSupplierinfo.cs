using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptSupplierinfo : Form
    {
        DbClass db = new DbClass();
        SupplierInfoCrystalReport sinforpt = new SupplierInfoCrystalReport();

        public RptSupplierinfo()
        {
            InitializeComponent();
            getdata();
        }

        public RptSupplierinfo(string suppname)
        {
            InitializeComponent();
            getdata(suppname);
        }

        public void getdata(string suppname)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                SupplierDS ds = new SupplierDS();
                if(CommonMethod.commProduct==true)
                    da = new SqlDataAdapter("Select * from SupplierInformation where SupplierName='" + suppname + "' ", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from SupplierInformation where SupplierName='" + suppname + "' and CompId='"+CommonMethod.CompId+"' ", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "SupplierInformation");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                sinforpt = new SupplierInfoCrystalReport();
                sinforpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = sinforpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void getdata()
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                SupplierDS ds = new SupplierDS();
                da = new SqlDataAdapter("Select * from SupplierInformation ", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "SupplierInformation");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                sinforpt = new SupplierInfoCrystalReport();
                sinforpt.SetDataSource(ds);

                crystalReportViewer1.ReportSource = sinforpt;
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