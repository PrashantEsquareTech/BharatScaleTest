using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmCompareLedger : Form
    {
        DbClass db = new DbClass();
        public int custid, Suppid;

        public frmCompareLedger()
        {
            InitializeComponent();
            Supplierdata();
            customerdata();
        }

        public void Supplierdata()
        {
            try
            {
                SupplierRepository supplierrepo = new SupplierRepository();
                List<SupplierInformation> customerdata = new List<SupplierInformation>();
                customerdata.Clear();
                if (CommonMethod.commProduct == true)
                    customerdata = supplierrepo.GetAll().OrderByDescending(t => t.SupplierName).ToList();
                else
                    customerdata = supplierrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.SupplierName).ToList();
                customerdata.Add(new SupplierInformation { SupplierName = "Select", SupplierCode = 0 });
                customerdata.Reverse();
                cmbsupplier.DataSource = customerdata;
                cmbsupplier.ValueMember = "SupplierCode";
                cmbsupplier.DisplayMember = "SupplierName";
                cmbsupplier.SelectedIndex = 0;
                supplierrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void customerdata()
        {
            try
            {
                CustomerRepository custrepo = new CustomerRepository();
                List<CustomerInformation> customerdata = new List<CustomerInformation>();
                customerdata.Clear();
                customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CustomerName).ToList();
                customerdata.Add(new CustomerInformation { CustomerName = "Select", CustomerID = 0 });
                customerdata.Reverse();
                cmbcustomer.DataSource = customerdata;
                cmbcustomer.ValueMember = "CustomerID";
                cmbcustomer.DisplayMember = "CustomerName";
                cmbcustomer.SelectedValue = 0;
                custrepo.Dispose();
            }
            catch (Exception)
            { }
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

        private void frmCompareLedger_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void cmbcustomer_Leave(object sender, EventArgs e)
        {
            if (cmbcustomer.Text != "" && cmbcustomer.Text != "Select")
            {
                CommonMethod cm = new CommonMethod();
                if (cm.ValidCustomer(cmbcustomer.Text) == 1)
                { }
                else
                {
                    MessageBox.Show("Invalid Customer.", "Warning");
                    customerdata();
                    this.ActiveControl = cmbcustomer;
                }
            }
        }

        private void cmbsupplier_Leave(object sender, EventArgs e)
        {
            if (cmbsupplier.Text != "" && cmbsupplier.Text != "Select")
            {
                CommonMethod cm = new CommonMethod();
                if (cm.ValidSupplier(cmbsupplier.Text) == 1)
                { }
                else
                {
                    MessageBox.Show("Invalid Supplier.", "Warning");
                    Supplierdata();
                    this.ActiveControl = cmbsupplier;
                }
            }
        }

        private void cmbcustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void cmbsupplier_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void GvRemainingpayment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    frmCustomerLedger custdata = new frmCustomerLedger(custid);
                    custdata.ShowDialog();
                }
                else if (e.ColumnIndex == 2)
                {
                    frmSupplierLedger suppdata = new frmSupplierLedger(Suppid);
                    suppdata.ShowDialog();
                }
            }
            catch (Exception)
            { }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbcustomer.Text != "Select" && cmbsupplier.Text != "Select")
                {
                    custid = Convert.ToInt32(cmbcustomer.SelectedValue);
                    Suppid = Convert.ToInt32(cmbsupplier.SelectedValue);

                    db.connect();
                    //Stored Procedure code
                    SqlCommand command = new SqlCommand("SPCompareLedger", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@CustId", SqlDbType.Int).Value = cmbcustomer.SelectedValue;
                    command.Parameters.Add("@SuppId", SqlDbType.Int).Value = cmbsupplier.SelectedValue;
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                    DataTable dt = new DataTable();
                    dt.Load(command.ExecuteReader());
                    GvRemainingpayment.Rows.Clear();
                    GvRemainingpayment.DataSource = dt;
                    GvRemainingpayment.Columns["CustomerName"].Width = 250;
                    GvRemainingpayment.Columns["CRemainingAmt"].Width = 150;
                    GvRemainingpayment.Columns["SupplierName"].Width = 250;
                    GvRemainingpayment.Columns["SReaminingAmount"].Width = 150;
                    GvRemainingpayment.Columns["FinalAmount"].Width = 150;
                }
                else
                {
                    if (cmbcustomer.Text == "Select")
                    {
                        MessageBox.Show("Select Customer Name.", "Warning");
                        this.ActiveControl = cmbcustomer;
                    }
                    else
                    {
                        MessageBox.Show("Select Supplier Name.", "Warning");
                        this.ActiveControl = cmbsupplier;
                    }
                }
            }
            catch (Exception)
            { }
        }
    }
}