using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Reports
{
    public partial class RptWithoutVatAllCustomerBill : Form
    {
        DbClass db = new DbClass();
        WithoutVatAllCustomerBillCrystalReport salerpt = new WithoutVatAllCustomerBillCrystalReport();

        public RptWithoutVatAllCustomerBill()
        {
            InitializeComponent();
            Getdata();
        }

        public void Getdata()
        {
            try
            {
                db.connect();
                SqlDataAdapter da;
                da = new SqlDataAdapter("Select * from CustomerBillMaster Where Vat==0 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                DSAllSale ds = new DSAllSale();
                da.Fill(ds, "CustomerBillMaster");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");

                salerpt = new WithoutVatAllCustomerBillCrystalReport();
                salerpt.SetDataSource(ds);
                crystalReportViewer1.ReportSource = salerpt;
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
                CustomerBillMasterRepository Custmasterrepo = new CustomerBillMasterRepository();
                var custdata = Custmasterrepo.GetAll().Where(t => t.BillNo == Convert.ToString(txtbillno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (custdata != null)
                {
                    db.connect();
                    SqlDataAdapter da;
                    da = new SqlDataAdapter("Select * from CustomerBillMaster Where BillNo='" + txtbillno.Text + "' and Vat==0 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    DSAllSale ds = new DSAllSale();
                    da.Fill(ds, "CustomerBillMaster");

                    da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                    da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                    da.Fill(ds, "CompanyInformation");

                    salerpt = new WithoutVatAllCustomerBillCrystalReport();
                    salerpt.SetDataSource(ds);
                    crystalReportViewer1.ReportSource = salerpt;
                    crystalReportViewer1.Refresh();
                    db.CloseConnection();
                    da.Dispose();
                    ds.Dispose();
                }
                else
                {
                    MessageBox.Show("This Invoice Number is  Not Present.", "Warning");
                    txtbillno.Text = "";
                    this.ActiveControl = txtbillno;
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
            txtbillno.Text = "";
            Getdata();
        }
    }
}