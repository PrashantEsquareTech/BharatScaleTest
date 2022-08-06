using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptDrCr : Form
    {
        DbClass db = new DbClass();
        DebitCreditCrystalReport1 drcrreport = new DebitCreditCrystalReport1();
        CommonMethod cm = new CommonMethod();

        public RptDrCr()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            getallreport();
        }

        public RptDrCr(string from, string to)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            betweenreport(from, to);
        }

        public void getallreport()
        {
            try
            {
                db.connect();
                DSPurSale ds = new DSPurSale();
                DataTable dt = new DataTable();
                SqlDataAdapter da;

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                CustomerBillMasterRepository billmasterrepo = new CustomerBillMasterRepository();
                CustomerBillMaster custmaster = new CustomerBillMaster();
                List<CustomerBillMaster> billlist = billmasterrepo.GetAll().Where(t => t.BillNo.StartsWith("E") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();

                if (billlist != null)
                {
                    foreach (var item in billlist)
                    {
                        int j = 1;
                        CustomerRepository custrepo = new CustomerRepository();
                        string billno = Convert.ToString(item.BillNo);
                        string billdate = Convert.ToString(item.Billdate);
                        string custname = item.CustomerName;
                        var customerdata = custrepo.GetAll().Where(t => t.CustomerID == Convert.ToInt32(item.CustomerId) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        string custadd = customerdata.CustomeAddress;
                        string contact = customerdata.ContactNo;
                        string namt = Convert.ToString(item.NetAmount);
                        dt = ds.Tables["CustomerBill"];
                        dt.Rows.Add(new object[7] { billno, billdate, custname, custadd, contact, namt, j });
                        j++;
                        custrepo.Dispose();
                    }
                }

                PurchaseMasterRepository pmasterrepo = new PurchaseMasterRepository();
                PurchaseMaster pOrder = new PurchaseMaster();
                List<PurchaseMaster> pOrderList = pmasterrepo.GetAll().Where(t => (t.Vat == 0 || t.TAXType != "TaxPurchase") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();

                int i = 1;
                if (pOrderList != null)
                {
                    foreach (var item in pOrderList)
                    {
                        string sname = null, saddress = null, cno = null;
                        int pno = Convert.ToInt32(item.PorderNo);
                        SupplierRepository supplierrepo = new SupplierRepository();
                        SupplierInformation supplierdata = new SupplierInformation();
                        string pdate = Convert.ToString(item.PorderDate);
                        int sid = Convert.ToInt32(item.SupplierId);
                        try
                        {
                            if (CommonMethod.commProduct == true)
                                supplierdata = supplierrepo.GetAll().Where(t => t.SupplierCode == sid).FirstOrDefault();
                            else
                                supplierdata = supplierrepo.GetAll().Where(t => t.SupplierCode == sid && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            sname = supplierdata.SupplierName;
                            saddress = supplierdata.SupplierAddress;
                            cno = supplierdata.ContactNo;
                        }
                        catch (Exception)
                        { }
                        decimal tamt = Convert.ToDecimal(item.NetAmount);
                        dt = ds.Tables["PurchaseOrder"];
                        dt.Rows.Add(new object[7] { pno, pdate, sname, saddress, cno, tamt, i });
                        i++;
                        supplierrepo.Dispose();
                    }
                }
                drcrreport = new DebitCreditCrystalReport1();
                drcrreport.SetDataSource(ds);
                drcrreport.Subreports[0].SetDataSource(ds);
                drcrreport.Subreports[1].SetDataSource(ds);
                crystalReportViewer1.ReportSource = drcrreport;
                db.CloseConnection();
                da.Dispose(); dt.Dispose(); ds.Dispose();
                pmasterrepo.Dispose(); billmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void betweenreport(string from, string to)
        {
            try
            {
                db.connect();
                DSPurSale ds = new DSPurSale();
                DataTable dt = new DataTable();
                SqlDataAdapter da;

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                CustomerBillMasterRepository billmasterrepo = new CustomerBillMasterRepository();
                CustomerBillMaster custmaster = new CustomerBillMaster();
                DateTime fromdate = Convert.ToDateTime(from), todate = Convert.ToDateTime(to);
                string fdate = Convert.ToString(cm.ValidFromDate(fromdate)), tdate = Convert.ToString(cm.ValidFromDate(todate));

                List<CustomerBillMaster> billlist = billmasterrepo.GetAll().Where(t => t.Billdate.Value.Date >= fromdate.Date && t.Billdate.Value.Date <= todate.Date && t.BillNo.StartsWith("E") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();

                if (billlist != null)
                {
                    foreach (var item in billlist)
                    {
                        int j = 1;
                        CustomerRepository custrepo = new CustomerRepository();
                        string billno = Convert.ToString(item.BillNo);
                        string billdate = Convert.ToString(item.Billdate.Value);
                        string custname = item.CustomerName;
                        var customerdata = custrepo.GetAll().Where(t => t.CustomerID == Convert.ToInt32(item.CustomerId) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        string custadd = customerdata.CustomeAddress;
                        string contact = customerdata.ContactNo;
                        string namt = Convert.ToString(item.NetAmount);
                        dt = ds.Tables["CustomerBill"];
                        dt.Rows.Add(new object[7] { billno, billdate, custname, custadd, contact, namt, j });
                        j++;
                        custrepo.Dispose();
                    }
                }

                PurchaseMasterRepository pmasterrepo = new PurchaseMasterRepository();
                PurchaseMaster pOrder = new PurchaseMaster();
                List<PurchaseMaster> pOrderList = pmasterrepo.GetAll().Where(t => t.PorderDate.Value.Date >= fromdate.Date && t.PorderDate.Value.Date <= todate.Date && t.Vat == 0 && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();

                int i = 1;
                if (pOrderList != null)
                {
                    foreach (var item in pOrderList)
                    {
                        string sname = null, saddress = null, cno = null, pdate;
                        SupplierRepository supplierrepo = new SupplierRepository();
                        int pno = Convert.ToInt32(item.PorderNo);
                        pdate = Convert.ToString(item.PorderDate.Value);
                        PurchaseMasterRepository masterRepo = new PurchaseMasterRepository();
                        var purchaseData = masterRepo.GetAll().Where(t => t.PorderNo == pno && t.Suppliername == "First stock").FirstOrDefault();
                        if (purchaseData != null)
                        {
                            sname = "First stock";
                            saddress = "";
                            cno = "";
                        }
                        else
                        {
                            int sid = Convert.ToInt32(item.SupplierId);
                            SupplierInformation supplierdata = new SupplierInformation();
                            if (CommonMethod.commProduct == true)
                                supplierdata = supplierrepo.GetAll().Where(t => t.SupplierCode == sid).FirstOrDefault();
                            else
                                supplierdata = supplierrepo.GetAll().Where(t => t.SupplierCode == sid && (t.CompId == CommonMethod.CompId || t.CompId == null)).FirstOrDefault();
                            if (supplierdata != null)
                            {
                                sname = supplierdata.SupplierName;
                                saddress = supplierdata.SupplierAddress;
                                cno = supplierdata.ContactNo;
                            }
                        }
                        decimal tamt = Convert.ToDecimal(item.NetAmount);
                        dt = ds.Tables["PurchaseOrder"];
                        dt.Rows.Add(new object[7] { pno, pdate, sname, saddress, cno, tamt, i });
                        i++;
                        supplierrepo.Dispose();
                    }
                }
                drcrreport = new DebitCreditCrystalReport1();
                drcrreport.SetDataSource(ds);
                drcrreport.Subreports[0].SetDataSource(ds);
                drcrreport.Subreports[1].SetDataSource(ds);
                crystalReportViewer1.ReportSource = drcrreport;
                db.CloseConnection();
                da.Dispose(); dt.Dispose(); ds.Dispose();
                pmasterrepo.Dispose(); billmasterrepo.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
                db.CloseConnection();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                db.connect();
                DSPurSale ds = new DSPurSale();
                DataTable dt = new DataTable();
                SqlDataAdapter da;

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                CustomerBillMasterRepository billmasterrepo = new CustomerBillMasterRepository();
                CustomerBillMaster custmaster = new CustomerBillMaster();
                List<CustomerBillMaster> billlist = billmasterrepo.GetAll().Where(t => t.Billdate.Value.Date >= dtpfromdate.Value.Date && t.Billdate.Value.Date <= dtptodate.Value.Date && t.BillNo.StartsWith("E") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();

                if (billlist != null)
                {
                    foreach (var item in billlist)
                    {
                        int j = 1;
                        CustomerRepository custrepo = new CustomerRepository();
                        string billno = Convert.ToString(item.BillNo);
                        string billdate = Convert.ToString(item.Billdate.Value);
                        string custname = item.CustomerName;
                        var customerdata = custrepo.GetAll().Where(t => t.CustomerID == Convert.ToInt32(item.CustomerId) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        string custadd = customerdata.CustomeAddress;
                        string contact = customerdata.ContactNo;
                        string namt = Convert.ToString(item.NetAmount);
                        dt = ds.Tables["CustomerBill"];
                        dt.Rows.Add(new object[7] { billno, billdate, custname, custadd, contact, namt, j });
                        j++;
                        custrepo.Dispose();
                    }
                }

                PurchaseMasterRepository pmasterrepo = new PurchaseMasterRepository();
                PurchaseMaster pOrder = new PurchaseMaster();
                List<PurchaseMaster> pOrderList = pmasterrepo.GetAll().Where(t => t.PorderDate.Value.Date >= dtpfromdate.Value.Date && t.PorderDate.Value.Date <= dtptodate.Value.Date && (t.Vat == 0 && t.TAXType != "TaxPurchase") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();

                int i = 1;
                if (pOrderList != null)
                {
                    foreach (var item in pOrderList)
                    {
                        int pno = Convert.ToInt32(item.PorderNo);
                        SupplierRepository supplierrepo = new SupplierRepository();
                        string pdate = Convert.ToString(item.PorderDate.Value);
                        int sid = Convert.ToInt32(item.SupplierId);
                        SupplierInformation supplierdata = new SupplierInformation();
                        if (CommonMethod.commProduct == true)
                            supplierdata = supplierrepo.GetAll().Where(t => t.SupplierCode == sid).FirstOrDefault();
                        else
                            supplierdata = supplierrepo.GetAll().Where(t => t.SupplierCode == sid && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        string sname = supplierdata.SupplierName;
                        string saddress = supplierdata.SupplierAddress;
                        string cno = supplierdata.ContactNo;
                        decimal tamt = Convert.ToDecimal(item.NetAmount);
                        dt = ds.Tables["PurchaseOrder"];
                        dt.Rows.Add(new object[7] { pno, pdate, sname, saddress, cno, tamt, i });
                        i++;
                        supplierrepo.Dispose();
                    }
                }
                drcrreport = new DebitCreditCrystalReport1();
                drcrreport.SetDataSource(ds);
                drcrreport.Subreports[0].SetDataSource(ds);
                drcrreport.Subreports[1].SetDataSource(ds);
                crystalReportViewer1.ReportSource = drcrreport;
                db.CloseConnection();
                da.Dispose(); dt.Dispose(); ds.Dispose();
                pmasterrepo.Dispose(); billmasterrepo.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
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