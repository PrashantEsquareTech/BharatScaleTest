using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptPurchaseorderPlace : Form
    {
        DbClass db = new DbClass();
        PurchaseOrderPlaceCrystalReport poprpt = new PurchaseOrderPlaceCrystalReport();

        public RptPurchaseorderPlace()
        {
            InitializeComponent();
        }

        public RptPurchaseorderPlace(int purchaseno)
        {
            InitializeComponent();
            GetData(purchaseno);
        }

        public void GetData(int porderno)
        {
            try
            {
                int purchaseid = 0, suppcode = 0;
                string custadd = "", mobileno = "", custname = "";
                db.connect();
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from PurchaseOrderPlaceMaster Where PorderNo='" + porderno + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DSPurchaseOrderPlace ds = new DSPurchaseOrderPlace();
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "PurchaseOrderPlaceMaster");
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    purchaseid = Convert.ToInt32(dt.Rows[0]["Id"].ToString());
                    suppcode = Convert.ToInt32(dt.Rows[0]["SupplierId"].ToString());
                }
                DataTable dt1;
                if (CommonMethod.commProduct == true)
                    dt1 = db.GetTable("Select * from SupplierInformation Where SupplierCode=" + suppcode + "  ");
                else
                    dt1 = db.GetTable("Select * from SupplierInformation Where SupplierCode=" + suppcode + " and CompId='" + CommonMethod.CompId + "' ");
                if (dt1.Rows.Count > 0)
                {
                    custname = dt1.Rows[0]["SupplierName"].ToString();
                    custadd = dt1.Rows[0]["SupplierAddress"].ToString();
                    mobileno = dt1.Rows[0]["ContactNo"].ToString();
                }                

                da = new SqlDataAdapter("Select d.* from PurchaseOrderPlaceDetail d inner join PurchaseOrderPlaceMaster m on m.Id=d.PorderNo Where d.PorderNo=" + purchaseid + "  and m.CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "PurchaseOrderPlaceDetail");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select * from ProductInformation", db.Connection);
                else
                    da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId, db.Connection);

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "ProductInformation");

                poprpt = new PurchaseOrderPlaceCrystalReport();
                poprpt.SetDataSource(ds);

                poprpt.SetParameterValue("Suppliername", custname);
                poprpt.SetParameterValue("Supplieraddress", custadd);
                poprpt.SetParameterValue("mobileno", mobileno);
                crystalReportViewer1.ReportSource = poprpt;
                crystalReportViewer1.Refresh();
                db.CloseConnection();
                ds.Dispose();
                da.Dispose();
                dt.Dispose();
                dt1.Dispose();
            }
            catch (Exception)
            {
                db.CloseConnection();
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                PurchaseOrderPlaceMasterRepository pmasterrepo = new PurchaseOrderPlaceMasterRepository();
                var custdata = pmasterrepo.GetAll().Where(t => t.PorderNo == Convert.ToInt32(txtorderno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (custdata != null)
                    GetData(Convert.ToInt32(txtorderno.Text));
                else
                {
                    MessageBox.Show("This Purchase Order Number is not Present.", "Warning");
                    txtorderno.Text = "";
                    this.ActiveControl = txtorderno;
                }
                pmasterrepo.Dispose();
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