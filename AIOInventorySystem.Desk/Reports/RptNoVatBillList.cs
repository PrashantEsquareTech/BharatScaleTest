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
    public partial class RptNoVatBillList : Form
    {
        DbClass db = new DbClass();
        NoVatBillListCrystalReport vatrpt = new NoVatBillListCrystalReport();
        CommonMethod cm = new CommonMethod();

        public RptNoVatBillList()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            getvatBill();
        }

        public RptNoVatBillList(int custid)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            getvatBill(custid);
        }

        public RptNoVatBillList(string mode)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            getvatbillmodwise(mode);

        }

        public RptNoVatBillList(string fromdt, string todt)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            getvatBill(fromdt, todt);
        }

        public RptNoVatBillList(int salesmanid,string t)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            getvatBill(salesmanid,t);
        }

        public void getvatbillmodwise(string mode)
        {
            try
            {
                if (mode == "Cash")
                {
                    DSNoVatBillList ds = new DSNoVatBillList();
                    SqlDataAdapter da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");
                    DataTable dt = new DataTable();
                    CustomerBillMasterRepository cbillmasterrepo = new CustomerBillMasterRepository();
                    List<CustomerBillMaster> ItemList1 = cbillmasterrepo.GetAll().Where(t => t.RemainingAmount == 0 && t.BillNo.StartsWith("E") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                    if (ItemList1 != null)
                    {
                        int k = 1;
                        foreach (var item in ItemList1)
                        {
                            string billno = Convert.ToString(item.BillNo);
                            string Billdate = Convert.ToString(item.Billdate.Value);
                            string Cname = item.CustomerName;
                            string Tamt = Convert.ToString(item.TotalAmount);
                            string Dis = Convert.ToString(item.Discount);
                            string vat = Convert.ToString(item.Vat);
                            string Namt = Convert.ToString(item.NetAmount);
                            string rcptno = Convert.ToString(item.BillRcptNo);
                            dt = ds.Tables["NoVatBillList"];
                            dt.Rows.Add(new object[8] { k, billno, Billdate, Cname, Tamt, Dis, Namt, rcptno });
                            k++;
                        }
                    }
                    vatrpt = new NoVatBillListCrystalReport();
                    vatrpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = vatrpt;
                    db.CloseConnection();
                    cbillmasterrepo.Dispose();
                    da.Dispose();
                    ds.Dispose();
                    dt.Dispose();
                }
                else
                {
                    DSNoVatBillList ds = new DSNoVatBillList();
                    SqlDataAdapter da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    DataTable dt = new DataTable();
                    CustomerBillMasterRepository cbillmasterrepo = new CustomerBillMasterRepository();

                    List<CustomerBillMaster> ItemList1 = cbillmasterrepo.GetAll().Where(t => t.RemainingAmount == 0 && t.BillNo.StartsWith("E") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                    if (ItemList1 != null)
                    {
                        int k = 1;
                        foreach (var item in ItemList1)
                        {
                            string billno = Convert.ToString(item.BillNo);
                            string Billdate = Convert.ToString(item.Billdate.Value);
                            string Cname = item.CustomerName;
                            string Tamt = Convert.ToString(item.TotalAmount);
                            string Dis = Convert.ToString(item.Discount);
                            string vat = Convert.ToString(item.Vat);
                            string Namt = Convert.ToString(item.NetAmount);
                            string rcptno = Convert.ToString(item.BillRcptNo);
                            dt = ds.Tables["NoVatBillList"];
                            dt.Rows.Add(new object[8] { k, billno, Billdate, Cname, Tamt, Dis, Namt, rcptno });
                            k++;
                        }
                    }
                    vatrpt = new NoVatBillListCrystalReport();
                    vatrpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = vatrpt;
                    db.CloseConnection();
                    cbillmasterrepo.Dispose();
                    da.Dispose();
                    ds.Dispose();
                    dt.Dispose();
                }
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void getvatBill(int custid)
        {
            try
            {
                DSNoVatBillList ds = new DSNoVatBillList();
                SqlDataAdapter da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                DataTable dt = new DataTable();
                CustomerBillMasterRepository cbillmasterrepo = new CustomerBillMasterRepository();
                List<CustomerBillMaster> ItemList1 = cbillmasterrepo.GetAll().Where(t => t.CustomerId == custid && t.BillNo.StartsWith("E") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                if (ItemList1 != null)
                {
                    int k = 1;
                    foreach (var item in ItemList1)
                    {
                        string billno = Convert.ToString(item.BillNo);
                        string Billdate = Convert.ToString(item.Billdate.Value);
                        string Cname = item.CustomerName;
                        string Tamt = Convert.ToString(item.TotalAmount);
                        string Dis = Convert.ToString(item.Discount);
                        string vat = Convert.ToString(item.Vat);
                        string Namt = Convert.ToString(item.NetAmount);
                        string rcptno = Convert.ToString(item.BillRcptNo);
                        dt = ds.Tables["NoVatBillList"];
                        dt.Rows.Add(new object[8] { k, billno, Billdate, Cname, Tamt, Dis, Namt, rcptno });
                        k++;
                    }
                }
                vatrpt = new NoVatBillListCrystalReport();
                vatrpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = vatrpt;
                db.CloseConnection();
                cbillmasterrepo.Dispose();
                da.Dispose();
                ds.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void getvatBill(int salesmanid,string a)
        {
            try
            {
                DSNoVatBillList ds = new DSNoVatBillList();
                SqlDataAdapter da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                DataTable dt = new DataTable();
                CustomerBillMasterRepository cbillmasterrepo = new CustomerBillMasterRepository();
                List<CustomerBillMaster> ItemList1 = cbillmasterrepo.GetAll().Where(t => t.StaffId == salesmanid && t.BillNo.StartsWith("E") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                if (ItemList1 != null)
                {
                    int k = 1;
                    foreach (var item in ItemList1)
                    {
                        string billno = Convert.ToString(item.BillNo);
                        string Billdate = Convert.ToString(item.Billdate.Value);
                        string Cname = item.CustomerName;
                        string Tamt = Convert.ToString(item.TotalAmount);
                        string Dis = Convert.ToString(item.Discount);
                        string vat = Convert.ToString(item.Vat);
                        string Namt = Convert.ToString(item.NetAmount);
                        string rcptno = Convert.ToString(item.BillRcptNo);
                        dt = ds.Tables["NoVatBillList"];
                        dt.Rows.Add(new object[8] { k, billno, Billdate, Cname, Tamt, Dis, Namt, rcptno });
                        k++;
                    }
                }
                vatrpt = new NoVatBillListCrystalReport();
                vatrpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = vatrpt;
                db.CloseConnection();
                cbillmasterrepo.Dispose();
                da.Dispose();
                ds.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void getvatBill()
        {
            try
            {
                DSNoVatBillList ds = new DSNoVatBillList();
                SqlDataAdapter da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");
                DataTable dt = new DataTable();
                CustomerBillMasterRepository cbillmasterrepo = new CustomerBillMasterRepository();
                List<CustomerBillMaster> ItemList1 = cbillmasterrepo.GetAll().Where(t => t.BillNo.StartsWith("E") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                if (ItemList1 != null)
                {
                    int k = 1;
                    foreach (var item in ItemList1)
                    {
                        string billno = Convert.ToString(item.BillNo);
                        string Billdate = Convert.ToString(item.Billdate.Value);
                        string Cname = item.CustomerName;
                        string Tamt = Convert.ToString(item.TotalAmount);
                        string Dis = Convert.ToString(item.Discount);
                        string vat = Convert.ToString(item.Vat);
                        string Namt = Convert.ToString(item.NetAmount);
                        string rcptno = Convert.ToString(item.BillRcptNo);
                        dt = ds.Tables["NoVatBillList"];
                        dt.Rows.Add(new object[8] { k, billno, Billdate, Cname, Tamt, Dis, Namt, rcptno });
                        k++;
                    }
                }
                vatrpt = new NoVatBillListCrystalReport();
                vatrpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = vatrpt;
                db.CloseConnection();
                cbillmasterrepo.Dispose();
                da.Dispose();
                ds.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        public void getvatBill(string fromdt, string todt)
        {
            try
            {
                DSNoVatBillList ds = new DSNoVatBillList();
                SqlDataAdapter da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                DataTable dt = new DataTable();
                CustomerBillMasterRepository cbillmasterrepo = new CustomerBillMasterRepository();

                CommonMethod com = new CommonMethod();
                string toDate = com.ValidToDate(Convert.ToDateTime(todt));
                string fromDate = com.ValidFromDate(Convert.ToDateTime(fromdt));

                List<CustomerBillMaster> ItemList1 = cbillmasterrepo.GetAll().Where(t => t.Billdate >= Convert.ToDateTime(fromDate) && t.Billdate <= Convert.ToDateTime(toDate) && t.BillNo.StartsWith("E") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                if (ItemList1 != null)
                {
                    int k = 1;
                    foreach (var item in ItemList1)
                    {
                        string billno = Convert.ToString(item.BillNo);
                        string Billdate = Convert.ToString(item.Billdate.Value);
                        string Cname = item.CustomerName;
                        string Tamt = Convert.ToString(item.TotalAmount);
                        string Dis = Convert.ToString(item.Discount);
                        string vat = Convert.ToString(item.Vat);
                        string Namt = Convert.ToString(item.NetAmount);
                        string rcptno = Convert.ToString(item.BillRcptNo);
                        dt = ds.Tables["NoVatBillList"];
                        dt.Rows.Add(new object[8] { k, billno, Billdate, Cname, Tamt, Dis, Namt, rcptno });
                        k++;
                    }
                }
                vatrpt = new NoVatBillListCrystalReport();
                vatrpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = vatrpt;
                db.CloseConnection();
                cbillmasterrepo.Dispose();
                da.Dispose();
                ds.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                DSNoVatBillList ds = new DSNoVatBillList();
                SqlDataAdapter da = new SqlDataAdapter("Select * from CompanyInformation  where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                DataTable dt = new DataTable();
                CustomerBillMasterRepository cbillmasterrepo = new CustomerBillMasterRepository();

                List<CustomerBillMaster> ItemList1 = cbillmasterrepo.GetAll().Where(t => t.Billdate >= dtpfromdate.Value && t.Billdate <= dtptodate.Value && t.BillNo.StartsWith("E") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                if (ItemList1 != null)
                {
                    int k = 1;
                    foreach (var item in ItemList1)
                    {
                        string billno = Convert.ToString(item.BillNo);
                        string Billdate = Convert.ToString(item.Billdate.Value);
                        string Cname = item.CustomerName;
                        string Tamt = Convert.ToString(item.TotalAmount);
                        string Dis = Convert.ToString(item.Discount);
                        string vat = Convert.ToString(item.Vat);
                        string Namt = Convert.ToString(item.NetAmount);
                        string rcptno = Convert.ToString(item.BillRcptNo);
                        dt = ds.Tables["NoVatBillList"];
                        dt.Rows.Add(new object[8] { k, billno, Billdate, Cname, Tamt, Dis, Namt, rcptno });
                        k++;
                    }
                }
                vatrpt = new NoVatBillListCrystalReport();
                vatrpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = vatrpt;
                db.CloseConnection();
                cbillmasterrepo.Dispose();
                da.Dispose();
                ds.Dispose();
                dt.Dispose();
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
            getvatBill();
        }
    }
}