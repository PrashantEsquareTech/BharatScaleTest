using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptWithoutVatAllPurchase : Form
    {
        DbClass db = new DbClass();
        WithoutVatAllPurchaseCrystalReport purchaserpt = new WithoutVatAllPurchaseCrystalReport();

        public RptWithoutVatAllPurchase()
        {
            InitializeComponent();
            Getdata();
        }

        public RptWithoutVatAllPurchase(int pono, string mode)
        {
            InitializeComponent();
            Getdata(pono, mode);
        }

        public RptWithoutVatAllPurchase(string suppname)
        {
            InitializeComponent();
            Getdata(suppname);
        }

        public RptWithoutVatAllPurchase(string from, string to)
        {
            InitializeComponent();
            Getdata(from, to);
        }

        public void Getdata(string from, string to)
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                CommonMethod cm = new CommonMethod();
                string toDate = cm.ValidToDate(Convert.ToDateTime(to));
                string fromDate = cm.ValidFromDate(Convert.ToDateTime(from));

                da = new SqlDataAdapter("Select distinct(pm.PorderNo),pm.PorderDate,pm.Suppliername,pm.TotalAmount,pm.Discount,pm.SpcDiscount,pm.NetAmount from PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo Where pm.PorderDate>='" + fromDate + "' and pm.PorderDate<='" + toDate + "' and (pm.TotalGst=0 or pm.TotalGst=null) and pm.TAXType is NULL and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId=" + CommonMethod.CompId + " order by pm.PorderDate", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DSAllPurchase ds = new DSAllPurchase();
                da.Fill(ds, "PurchaseMaster");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                purchaserpt = new WithoutVatAllPurchaseCrystalReport();
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

                da = new SqlDataAdapter("Select distinct(pm.PorderNo),pm.PorderDate,pm.SupplierName,pm.TotalAmount,pm.Discount,pm.SpcDiscount,pm.NetAmount from PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo Where pm.Suppliername='" + suppname + "' and (pm.TotalGst=0 or pm.TotalGst=null) and pm.TAXType is NULL and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DSAllPurchase ds = new DSAllPurchase();
                da.Fill(ds, "PurchaseMaster");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                purchaserpt = new WithoutVatAllPurchaseCrystalReport();
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

        public void Getdata(int pono, string mode)
        {
            try
            {
                if (pono != 0 && mode == null)
                {
                    db.connect();
                    SqlDataAdapter da;

                    da = new SqlDataAdapter("Select distinct(pm.PorderNo),pm.PorderDate,pm.SupplierName,pm.TotalAmount,pm.Discount,pm.SpcDiscount,pm.NetAmount from PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo Where (pm.TotalGst=0 or pm.TotalGst=null) and pm.TAXType is NULL and pm.PorderNo='" + pono + "' and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId=" + CommonMethod.CompId + "", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DSAllPurchase ds = new DSAllPurchase();
                    da.Fill(ds, "PurchaseMaster");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    purchaserpt = new WithoutVatAllPurchaseCrystalReport();
                    purchaserpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = purchaserpt;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    da.Dispose();
                    ds.Dispose();
                }
                else
                {
                    if (mode == "Cash")
                    {
                        db.connect();
                        SqlDataAdapter da;

                        da = new SqlDataAdapter("Select distinct(pm.PorderNo),pm.PorderDate,pm.SupplierName,pm.TotalAmount,pm.Discount,pm.SpcDiscount,pm.NetAmount from PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo Where (pm.TotalGst=0 or pm.TotalGst=null) and pm.TAXType is NULL and pm.RemainingAmount=0.00 and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId=" + CommonMethod.CompId + "", db.Connection);
                        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        DSAllPurchase ds = new DSAllPurchase();
                        da.Fill(ds, "PurchaseMaster");

                        da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        da.Fill(ds, "CompanyInformation");

                        purchaserpt = new WithoutVatAllPurchaseCrystalReport();
                        purchaserpt.SetDataSource(ds);
                        crystalReportViewer1.ReportSource = purchaserpt;
                        crystalReportViewer1.Refresh();
                        db.CloseConnection();
                        da.Dispose();
                        ds.Dispose();
                    }
                    else
                    {
                        db.connect();
                        SqlDataAdapter da;

                        da = new SqlDataAdapter("Select distinct(pm.PorderNo),pm.PorderDate,pm.SupplierName,pm.TotalAmount,pm.Discount,pm.SpcDiscount,pm.NetAmount from PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo Where  (pm.TotalGst=0 or pm.TotalGst=null) and pm.TAXType is NULL and pm.RemainingAmount!=0.00 and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId=" + CommonMethod.CompId + "", db.Connection);
                        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        DSAllPurchase ds = new DSAllPurchase();
                        da.Fill(ds, "PurchaseMaster");

                        da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                        da.Fill(ds, "CompanyInformation");

                        purchaserpt = new WithoutVatAllPurchaseCrystalReport();
                        purchaserpt.SetDataSource(ds);
                        crystalReportViewer1.ReportSource = purchaserpt;
                        crystalReportViewer1.Refresh();
                        db.CloseConnection();
                        da.Dispose();
                        ds.Dispose();
                    }
                }
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void Getdata()
        {
            try
            {
                db.connect();
                SqlDataAdapter da;

                da = new SqlDataAdapter("Select distinct(pm.PorderNo),pm.PorderDate,pm.SupplierName,pm.TotalAmount,pm.Discount,pm.SpcDiscount,pm.NetAmount from PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo Where (pm.TotalGst=0 or pm.TotalGst=null) and pm.TAXType is NULL and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DSAllPurchase ds = new DSAllPurchase();
                da.Fill(ds, "PurchaseMaster");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                purchaserpt = new WithoutVatAllPurchaseCrystalReport();
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
                PurchaseMasterRepository pmasterrepo = new PurchaseMasterRepository();
                var custdata = pmasterrepo.GetAll().Where(t => t.PorderNo == Convert.ToInt32(txtorderno.Text) && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                if (custdata != null)
                {
                    db.connect();
                    SqlDataAdapter da;

                    da = new SqlDataAdapter("Select distinct(pm.PorderNo),pm.PorderDate,pm.SupplierName,pm.TotalAmount,pm.Discount,pm.SpcDiscount,pm.NetAmount from PurchaseMaster as pm inner join PurchaseDetail as pd on pm.Id=pd.PorderNo Where pm.PorderNo='" + txtorderno.Text + "' and (pm.TotalGst=0 or pm.TotalGst=null) and pm.TAXType is NULL and pm.TransactionYear='" + CommonMethod.TransactionYear + "' and pm.CompId=" + CommonMethod.CompId + "", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DSAllPurchase ds = new DSAllPurchase();
                    da.Fill(ds, "PurchaseMaster");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    purchaserpt = new WithoutVatAllPurchaseCrystalReport();
                    purchaserpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = purchaserpt;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    da.Dispose();
                    ds.Dispose();
                }
                else
                {
                    MessageBox.Show("This Purchase Order Number is not Present.", "Warning");
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