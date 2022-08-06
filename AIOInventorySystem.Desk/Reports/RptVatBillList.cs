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
    public partial class RptVatBillList : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        VatBillListCrystalReport vatrpt = new VatBillListCrystalReport();

        public RptVatBillList()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            getvatBill();
        }

        public void getvatBill()
        {
            try
            {
                DSVatBillList ds = new DSVatBillList();
                DataTable dt = new DataTable();
                DataTable dt1 = db.GetTable("Select * from CustomerBillMaster Where BillNo like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "");
                if (dt1.Rows.Count > 0)
                {
                    int j = 1;
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        int Vbillno = Convert.ToInt32(dt1.Rows[i]["BillNo"].ToString());
                        string VBilldate = dt1.Rows[i]["Billdate"].ToString();
                        string VCname = dt1.Rows[i]["CustomerName"].ToString();
                        string VTamt = dt1.Rows[i]["TotalAmount"].ToString();
                        string VDis = dt1.Rows[i]["Discount"].ToString();
                        string Vvat = dt1.Rows[i]["Vat"].ToString();
                        string VNamt = dt1.Rows[i]["NetAmount"].ToString();
                        dt = ds.Tables["VatBillList"];
                        dt.Rows.Add(new object[8] { j, Vbillno, VBilldate, VCname, VTamt, VDis, Vvat, VNamt });
                        j++;
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");
                vatrpt = new VatBillListCrystalReport();
                vatrpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = vatrpt;
                db.CloseConnection();
                da.Dispose(); ds.Dispose();
                dt.Dispose(); dt1.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                DSVatBillList ds = new DSVatBillList();
                DataTable dt = new DataTable();
                CustomerBillMasterRepository cbillmasterrepo = new CustomerBillMasterRepository();
                List<CustomerBillMaster> ItemList = cbillmasterrepo.GetAll().Where(t => t.BillNo.StartsWith("T") && t.Billdate >= dtpfromdate.Value.Date && t.Billdate <= dtptodate.Value.Date && t.CompId == CommonMethod.CompId && t.TransactionYear==CommonMethod.TransactionYear).ToList();
                if (ItemList != null)
                {
                    foreach (var item in ItemList)
                    {
                        int j = 1;
                        int Vbillno = Convert.ToInt32(item.BillNo);
                        string VBilldate = item.Billdate.Value.ToString("dd/MM/yyyy");
                        string VCname = item.CustomerName;
                        string VTamt = Convert.ToString(item.TotalAmount);
                        string VDis = Convert.ToString(item.Discount);
                        string Vvat = Convert.ToString(item.Vat);
                        string VNamt = Convert.ToString(item.NetAmount);

                        dt = ds.Tables["VatBillList"];
                        dt.Rows.Add(new object[8] { j, Vbillno, VBilldate, VCname, VTamt, VDis, Vvat, VNamt });
                        j++;
                    }
                }
                vatrpt = new VatBillListCrystalReport();
                vatrpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = vatrpt;
                db.CloseConnection();
                cbillmasterrepo.Dispose();
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