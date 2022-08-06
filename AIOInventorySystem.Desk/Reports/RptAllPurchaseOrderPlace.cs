using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptAllPurchaseOrderPlace : Form
    {
        DbClass db = new DbClass();
        AllPurchaseOrderPlaceCrystalReport purchaserpt = new AllPurchaseOrderPlaceCrystalReport();
       
        public RptAllPurchaseOrderPlace(int pono)
        {
            InitializeComponent();            
            Getdata(pono);            
        }

        public RptAllPurchaseOrderPlace()
        {
            InitializeComponent();
            Getdata();
        }

        public RptAllPurchaseOrderPlace(string suppname)
        {
            InitializeComponent();
            Getdata(suppname);
        }

        public RptAllPurchaseOrderPlace(string from,string to)
        {
            InitializeComponent();
            Getdata(from,to);
        }    
        
        public void Getdata()
        {
            try
            {
                db.connect();
                SqlDataAdapter da;

                da = new SqlDataAdapter("Select * from PurchaseOrderPlaceMaster Where TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DSAllPurchaseOrderPlace ds = new DSAllPurchaseOrderPlace();
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "PurchaseOrderPlaceMaster");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                purchaserpt = new AllPurchaseOrderPlaceCrystalReport();
                purchaserpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = purchaserpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void Getdata(string suppname)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;

                da = new SqlDataAdapter("Select * from PurchaseOrderPlaceMaster Where Suppliername='" + suppname + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DSAllPurchaseOrderPlace ds = new DSAllPurchaseOrderPlace();
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "PurchaseOrderPlaceMaster");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                purchaserpt = new AllPurchaseOrderPlaceCrystalReport();
                purchaserpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = purchaserpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void Getdata(string from,string to)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from PurchaseOrderPlaceMaster Where PorderDate>='" + from + "' and PorderDate<='" + to + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DSAllPurchaseOrderPlace ds = new DSAllPurchaseOrderPlace();
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "PurchaseOrderPlaceMaster");

                da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                purchaserpt = new AllPurchaseOrderPlaceCrystalReport();
                purchaserpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = purchaserpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void Getdata(int pono)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;

                da = new SqlDataAdapter("Select * from PurchaseOrderPlaceMaster Where PorderNo='" + pono + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DSAllPurchaseOrderPlace ds = new DSAllPurchaseOrderPlace();
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "PurchaseOrderPlaceMaster");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                purchaserpt = new AllPurchaseOrderPlaceCrystalReport();
                purchaserpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = purchaserpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                da.Dispose();
                ds.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                int cbn = Convert.ToInt32(txtorderno.Text);
                PurchaseMasterRepository pmasterrepo = new PurchaseMasterRepository();
                var custdata = pmasterrepo.GetAll().Where(t => t.PorderNo == cbn && t.CompId==CommonMethod.CompId).FirstOrDefault();
                if (custdata != null)
                {
                    db.connect();
                    SqlDataAdapter da;

                    da = new SqlDataAdapter("Select * from PurchaseOrderPlaceMaster Where PorderNo='" + txtorderno.Text + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    DSAllPurchaseOrderPlace ds = new DSAllPurchaseOrderPlace();
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "PurchaseOrderPlaceMaster");

                    da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    purchaserpt = new AllPurchaseOrderPlaceCrystalReport();
                    purchaserpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = purchaserpt;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    da.Dispose();
                    ds.Dispose();
                }
                else
                {
                    MessageBox.Show("This Purchase Order Number is not Present.", "Success");
                    txtorderno.Text = "";
                    this.ActiveControl = txtorderno;
                }
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

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            txtorderno.Text = "";
            Getdata();
        }
    }
}