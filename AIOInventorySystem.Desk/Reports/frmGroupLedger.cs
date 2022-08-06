using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using AIOInventorySystem.Desk.Reports;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk
{
    public partial class frmGroupLedger : Form
    {

        public frmGroupLedger()
        {

            InitializeComponent();

        }
        public frmGroupLedger(int customerId)
        {

            InitializeComponent();
            LoadData(customerId);
        }
        public void LoadData(int customerId)
        {
            try
            {
                DbClass db = new DbClass();
                SqlDataAdapter da;
                DataTable dt = new DataTable();
                GroupCustomerLedger ds = new GroupCustomerLedger();
                da = new SqlDataAdapter("Select * from RemainingPayment where CustomerId='" + customerId + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "RemainingPayment");
               

                crptGroupCustomerLedger rptledger = new crptGroupCustomerLedger();
               

                SqlDataAdapter da1;
                CustomerBillDetailRepository cDetailRepo = new CustomerBillDetailRepository();
                List<string> billList = cDetailRepo.GetAll().OrderBy(x => x.CustomerBillMaster.CustomerId == customerId)
                                    .Select(x => x.BillNo.ToString())
                                    .Distinct()  // Remove duplicated entries
                                    .ToList();
                string joinBillNos = string.Join(",", billList);
                da = new SqlDataAdapter("SELECT dbo.CustomerBillDetail.ProductId, dbo.CustomerBillDetail.Quantity, dbo.CustomerBillDetail.Rate, dbo.CustomerBillDetail.Amount, dbo.CustomerBillMaster.BillNo FROM dbo.CustomerBillDetail INNER JOIN dbo.CustomerBillMaster ON dbo.CustomerBillDetail.BillNo = dbo.CustomerBillMaster.Id WHERE dbo.CustomerBillDetail.BillNo IN (" + joinBillNos + ") and dbo.CustomerBillMaster.TransactionYear='" + CommonMethod.TransactionYear + "' and dbo.CustomerBillMaster.CompId='" + CommonMethod.CompId + "'", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "tblItemDetail");

                da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                da.Fill(ds, "CompanyInformation");  

                rptledger.SetDataSource(ds);
                crystalReportViewer1.ReportSource = null;
                crystalReportViewer1.ReportSource = rptledger;
                db.CloseConnection();
                ds.Dispose();
            }
            catch (Exception)
            {
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
