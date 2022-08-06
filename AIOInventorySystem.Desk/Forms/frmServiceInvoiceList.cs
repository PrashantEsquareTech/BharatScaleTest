using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmServiceInvoiceList : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public frmServiceInvoiceList()
        {
            InitializeComponent();
            BindCustomerNames();
        }

        private void BindCustomerNames()
        {
            try
            {
                CustomerRepository custrepo = new CustomerRepository();
                List<CustomerInformation> customerdata = new List<CustomerInformation>();
                customerdata.Clear();
                customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CustomerName).ToList();
                customerdata.Add(new CustomerInformation { CustomerName = "Select", Id = 0 });
                customerdata.Reverse();
                CmbCustomer.DataSource = customerdata;
                CmbCustomer.ValueMember = "Id";
                CmbCustomer.DisplayMember = "CustomerName";
                custrepo.Dispose();
                CmbCustomer.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT Distinct SM.Id, SM.ServiceNo, CONVERT(VARCHAR(24),SM.ServiceDate,103) as 'ServiceDate', CI.CustomerName, SM.CLMNo, SM.PavatiNo, SM.ServiceNetAmt, SM.VerificationNetAmt FROM ServiceInvoiceMaster as SM INNER JOIN CustomerInformation as CI ON SM.CustomerId = CI.CustomerID INNER JOIN ServiceInvoiceDetail as SD ON SM.Id=SD.ServiceId where SM.CompId=" + CommonMethod.CompId + " and  SM.TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvList.DataSource = dt;
                gvList.Refresh();
                gvList.Columns["Id"].Visible = false;
                gvList.Columns["ServiceNo"].Width = 50;
                gvList.Columns["ServiceDate"].Width = 70;
                gvList.Columns["CustomerName"].Width = 120;
                gvList.Columns["CLMNo"].Width = 70;
                gvList.Columns["PavatiNo"].Width = 70;
                gvList.Columns["ServiceNetAmt"].Width = 80;
                gvList.Columns["VerificationNetAmt"].Width = 80;
                lblTotalServiceCharges.Text = Convert.ToString(dt.Compute("Sum(ServiceNetAmt)", ""));
                lblTotalVerificationFees.Text = Convert.ToString(dt.Compute("Sum(VerificationNetAmt)", ""));
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void gvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                frmServiceInvoice qua = new frmServiceInvoice(Convert.ToInt32(gvList.Rows[e.RowIndex].Cells["Id"].Value));
                qua.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void btnPrintRegister_Click(object sender, EventArgs e)
        {
            RptServicePrint qua = new RptServicePrint();
            qua.ShowDialog();
        }

        private void Search()
        {
            try
            {
                StringBuilder sbSQLQuery = new StringBuilder();
                sbSQLQuery.Append("SELECT Distinct SM.Id, SM.ServiceNo, CONVERT(VARCHAR(24),SM.ServiceDate,103) as 'ServiceDate', CI.CustomerName, SM.CLMNo, SM.PavatiNo, SM.ServiceNetAmt, SM.VerificationNetAmt  FROM ServiceInvoiceMaster as SM INNER JOIN  CustomerInformation as CI ON SM.CustomerId = CI.CustomerID INNER JOIN ServiceInvoiceDetail as SD ON SM.Id=SD.ServiceId where SM.CompId=" + CommonMethod.CompId + " and  SM.TransactionYear='" + CommonMethod.TransactionYear + "'");
                if (chkServiceNo.Checked)
                    sbSQLQuery.Append(" and SM.ServiceNo=" + txtServiceNo.Text + "");
                if(chkFromDate.Checked)
                    sbSQLQuery.Append(" and SM.ServiceDate>='" + cm.ValidFromDate(dtpFromDate.Value.Date).ToString() + "' and SM.ServiceDate<='" + cm.ValidToDate(dtpToDate.Value.Date).ToString() + "' ");
                if (chkCustomer.Checked)
                    sbSQLQuery.Append(" and SM.CustomerId=" + CmbCustomer.SelectedValue + "");
                if (chkCLMNo.Checked)
                    sbSQLQuery.Append(" and SM.CLMNo='" + txtCLMNo.Text + "'");
                if (chkPavatiNo.Checked)
                    sbSQLQuery.Append(" and SM.PavatiNo='" + txtPavatiNo.Text + "'");
                if (chkMake.Checked)
                    sbSQLQuery.Append(" and SD.Make='" + txtMake.Text + "'");
                if (chkMachineNo.Checked)
                    sbSQLQuery.Append(" and SD.MachineNo='" + txtMachineNo.Text + "'");

                SqlDataAdapter da = new SqlDataAdapter(sbSQLQuery.ToString(), db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvList.DataSource = dt;
                gvList.Refresh();
                gvList.Columns["Id"].Visible = false;
                gvList.Columns["ServiceNo"].Width = 50;
                gvList.Columns["ServiceDate"].Width = 70;
                gvList.Columns["CustomerName"].Width = 120;
                gvList.Columns["CLMNo"].Width = 70;
                gvList.Columns["PavatiNo"].Width = 70;
                gvList.Columns["ServiceNetAmt"].Width = 80;
                gvList.Columns["VerificationNetAmt"].Width = 80;
                lblTotalServiceCharges.Text = Convert.ToString(dt.Compute("Sum(ServiceNetAmt)", ""));
                lblTotalVerificationFees.Text = Convert.ToString(dt.Compute("Sum(VerificationNetAmt)", ""));
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Search();
            }
            catch (Exception)
            { }
        }
    }
}
