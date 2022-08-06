using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class FrmHistoryDetails : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public FrmHistoryDetails()
        {
            InitializeComponent();
            customerdata();
            fillsupplier();
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
                custrepo.Dispose();
                cmbcustomer.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void fillsupplier()
        {
            try
            {
                SupplierRepository supplierRepo = new SupplierRepository();
                List<SupplierInformation> customerdata = new List<SupplierInformation>();
                customerdata.Clear();
                if (CommonMethod.commProduct == true)
                    customerdata = supplierRepo.GetAll().OrderByDescending(t => t.SupplierName).ToList();
                else
                    customerdata = supplierRepo.GetAll().Where(t => t.CompId == Convert.ToInt32(CommonMethod.CompId)).OrderByDescending(t => t.SupplierName).ToList();
                customerdata.Add(new SupplierInformation { SupplierName = "Select", SupplierCode = 0 });
                customerdata.Reverse();
                cmbsupplier.DataSource = customerdata;
                cmbsupplier.ValueMember = "SupplierCode";
                cmbsupplier.DisplayMember = "SupplierName";
                supplierRepo.Dispose();
                cmbsupplier.SelectedIndex = 0;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkType.Checked == true)
                {
                    if (cmbType.Text != "")
                    {
                        if (chkcustomername.Checked == true)
                        {
                            if (cmbcustomer.SelectedIndex > 0)
                            {
                                DataTable dt = new DataTable();
                                db.connect();
                                SqlDataAdapter da = new SqlDataAdapter("Select BillNo as 'Bill No',Convert(varchar(24),BillDate,103) as 'Bill Date',SupplierOrCustomerName as 'Name',Amount,DeleteType as 'Type' ,Convert(varchar(24),CurrentDate,103) as 'Bill Delete Date' from DeleteHistoryTable where SupplierOrCustomerName='" + cmbcustomer.Text + "' and DeleteType='" + cmbType.Text + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                                da.Fill(dt);
                                GVHistoryDetails.DataSource = dt;
                                GVHistoryDetails.Refresh();
                                if (GVHistoryDetails.Rows.Count != 0)
                                {
                                    GVHistoryDetails.Columns[2].HeaderText = "Customer Name";
                                    foreach (DataGridViewColumn col in GVHistoryDetails.Columns)
                                    {
                                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                        col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                                    }
                                    GVHistoryDetails.Columns[0].Width = 50;
                                    GVHistoryDetails.Columns[1].Width = 70;
                                    GVHistoryDetails.Columns[2].Width = 200;
                                    GVHistoryDetails.Columns[3].Width = 80;
                                    GVHistoryDetails.Columns[4].Width = 180;
                                    GVHistoryDetails.Columns[5].Width = 100;
                                }
                                else
                                {
                                    MessageBox.Show("Record not found.", "Warning");
                                    this.ActiveControl = cmbcustomer;
                                }
                                db.CloseConnection();
                                dt.Dispose();
                            }
                            else
                            {
                                MessageBox.Show("Select Customer Name.", "Warning");
                                this.ActiveControl = cmbcustomer;
                            }
                        }
                        else if (chksupplier.Checked == true)
                        {
                            if (cmbsupplier.SelectedIndex > 0)
                            {
                                DataTable dt = new DataTable();
                                db.connect();
                                SqlDataAdapter da = new SqlDataAdapter("Select BillNo as 'Bill No',Convert(varchar(24),BillDate,103) as 'Bill Date',SupplierOrCustomerName as 'Name',Amount,DeleteType as 'Type' ,Convert(varchar(24),CurrentDate,103) as 'Bill Delete Date' from DeleteHistoryTable where SupplierOrCustomerName='" + cmbsupplier.Text + "' and DeleteType='" + cmbType.Text + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                                da.Fill(dt);
                                GVHistoryDetails.DataSource = dt;
                                GVHistoryDetails.Refresh();
                                if (GVHistoryDetails.Rows.Count != 0)
                                {
                                    GVHistoryDetails.Columns[2].HeaderText = "Supplier Name";
                                    foreach (DataGridViewColumn col in GVHistoryDetails.Columns)
                                    {
                                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                        col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                                    }
                                    GVHistoryDetails.Columns[0].Width = 50;
                                    GVHistoryDetails.Columns[1].Width = 70;
                                    GVHistoryDetails.Columns[2].Width = 200;
                                    GVHistoryDetails.Columns[3].Width = 80;
                                    GVHistoryDetails.Columns[4].Width = 180;
                                    GVHistoryDetails.Columns[5].Width = 100;
                                }
                                else
                                {
                                    MessageBox.Show("Record not found.", "Warning");
                                    this.ActiveControl = cmbsupplier;
                                }
                                db.CloseConnection();
                                dt.Dispose();
                            }
                            else
                            {
                                MessageBox.Show("Select supplier Name.", "Warning");
                                this.ActiveControl = cmbsupplier;
                            }
                        }
                        else
                        {
                            DataTable dt = new DataTable();
                            db.connect();
                            SqlDataAdapter da = new SqlDataAdapter("Select BillNo as 'Bill No',Convert(varchar(24),BillDate,103) as 'Bill Date',SupplierOrCustomerName as 'Name',Amount,DeleteType as 'Type' ,Convert(varchar(24),CurrentDate,103) as 'Bill Delete Date' from DeleteHistoryTable where DeleteType='" + cmbType.Text + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                            da.Fill(dt);
                            GVHistoryDetails.DataSource = dt;
                            GVHistoryDetails.Refresh();
                            if (GVHistoryDetails.Rows.Count != 0)
                            {
                                if (cmbType.Text == "Invoice" || cmbType.Text == "Sale Return" || cmbType.Text == "Delivery Challan" || cmbType.Text == "Order Booking" || cmbType.Text == "Quotation")
                                    GVHistoryDetails.Columns[2].HeaderText = "Customer Name";
                                else if (cmbType.Text == "Purchase Bill" || cmbType.Text == "Purchase Return" || cmbType.Text == "Purchase Order Place" || cmbType.Text == "Receipt Challan")
                                    GVHistoryDetails.Columns[2].HeaderText = "Supplier Name";
                                else
                                    GVHistoryDetails.Columns[2].HeaderText = "Name";
                                foreach (DataGridViewColumn col in GVHistoryDetails.Columns)
                                {
                                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                    col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                                }
                                GVHistoryDetails.Columns[0].Width = 50;
                                GVHistoryDetails.Columns[1].Width = 70;
                                GVHistoryDetails.Columns[2].Width = 200;
                                GVHistoryDetails.Columns[3].Width = 80;
                                GVHistoryDetails.Columns[4].Width = 180;
                                GVHistoryDetails.Columns[5].Width = 100;
                            }
                            else
                            {
                                MessageBox.Show("Record not found.", "Warning");
                                this.ActiveControl = cmbsupplier;
                            }
                            db.CloseConnection();
                            dt.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select Type.", "Warning");
                        this.ActiveControl = cmbType;
                    }
                }
                else if (chkcustomername.Checked == true)
                {
                    if (cmbcustomer.SelectedIndex > 0)
                    {
                        DataTable dt = new DataTable();
                        db.connect();
                        SqlDataAdapter da = new SqlDataAdapter("Select BillNo as 'Bill No',Convert(varchar(24),BillDate,103) as 'Bill Date',SupplierOrCustomerName as 'Name',Amount,DeleteType as 'Type' ,Convert(varchar(24),CurrentDate,103) as 'Bill Delete Date' from DeleteHistoryTable where SupplierOrCustomerName='" + cmbcustomer.Text + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                        da.Fill(dt);
                        GVHistoryDetails.DataSource = dt;
                        GVHistoryDetails.Refresh();
                        if (GVHistoryDetails.Rows.Count != 0)
                        {
                            GVHistoryDetails.Columns[2].HeaderText = "Customer Name";
                            foreach (DataGridViewColumn col in GVHistoryDetails.Columns)
                            {
                                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                            }
                            GVHistoryDetails.Columns[0].Width = 50;
                            GVHistoryDetails.Columns[1].Width = 70;
                            GVHistoryDetails.Columns[2].Width = 200;
                            GVHistoryDetails.Columns[3].Width = 80;
                            GVHistoryDetails.Columns[4].Width = 180;
                            GVHistoryDetails.Columns[5].Width = 100;
                        }
                        else
                        {
                            MessageBox.Show("Record not found.", "Warning");
                            this.ActiveControl = cmbcustomer;
                        }
                        db.CloseConnection();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Select Customer Name.", "Warning");
                        this.ActiveControl = cmbcustomer;
                    }
                }
                else if (chksupplier.Checked == true)
                {
                    if (cmbsupplier.SelectedIndex > 0)
                    {
                        DataTable dt = new DataTable();
                        db.connect();
                        SqlDataAdapter da = new SqlDataAdapter("Select BillNo as 'Bill No',Convert(varchar(24),BillDate,103) as 'Bill Date',SupplierOrCustomerName as 'Name',Amount,DeleteType as 'Type' ,Convert(varchar(24),CurrentDate,103) as 'Bill Delete Date' from DeleteHistoryTable where SupplierOrCustomerName='" + cmbsupplier.Text + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                        da.Fill(dt);
                        GVHistoryDetails.DataSource = dt;
                        GVHistoryDetails.Refresh();
                        if (GVHistoryDetails.Rows.Count != 0)
                        {
                            GVHistoryDetails.Columns[2].HeaderText = "Supplier Name";
                            foreach (DataGridViewColumn col in GVHistoryDetails.Columns)
                            {
                                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                            }
                            GVHistoryDetails.Columns[0].Width = 50;
                            GVHistoryDetails.Columns[1].Width = 70;
                            GVHistoryDetails.Columns[2].Width = 200;
                            GVHistoryDetails.Columns[3].Width = 80;
                            GVHistoryDetails.Columns[4].Width = 180;
                            GVHistoryDetails.Columns[5].Width = 100;
                        }
                        else
                        {
                            MessageBox.Show("Record not found.", "Warning");
                            this.ActiveControl = cmbsupplier;
                        }
                        db.CloseConnection();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Select Supplier.", "Warning");
                        this.ActiveControl = cmbsupplier;
                    }
                }
            }
            catch (Exception) { }
        }

        private void rbtnGetAll_Click(object sender, EventArgs e)
        {
            try
            {
                chkcustomername.Checked = false;
                chksupplier.Checked = false;
                chkType.Checked = false;
                fillsupplier(); customerdata();
                DataTable dt = new DataTable();
                db.connect();
                SqlDataAdapter da = new SqlDataAdapter("Select BillNo as 'Bill No',Convert(varchar(24),BillDate,103) as 'Bill Date',SupplierOrCustomerName as 'Name',Amount,DeleteType as 'Type' ,Convert(varchar(24),CurrentDate,103) as 'Bill Delete Date' from DeleteHistoryTable where CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'", db.Connection);
                da.Fill(dt);
                GVHistoryDetails.DataSource = dt;
                GVHistoryDetails.Refresh();
                if (GVHistoryDetails.Rows.Count != 0)
                {
                    foreach (DataGridViewColumn col in GVHistoryDetails.Columns)
                    {
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        col.HeaderCell.Style.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Pixel);
                    }
                    GVHistoryDetails.Columns[0].Width = 50;
                    GVHistoryDetails.Columns[1].Width = 70;
                    GVHistoryDetails.Columns[2].Width = 200;
                    GVHistoryDetails.Columns[3].Width = 80;
                    GVHistoryDetails.Columns[4].Width = 180;
                    GVHistoryDetails.Columns[5].Width = 100;
                }
                else
                {
                    MessageBox.Show("Record not found.", "Warning");
                    this.ActiveControl = cmbsupplier;
                }
                db.CloseConnection();
                dt.Dispose();
            }
            catch (Exception) { }
        }

        private void chkcustomername_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkcustomername.Checked == true)
                {
                    chksupplier.Checked = false;
                    fillsupplier();
                }
            }
            catch (Exception) { }
        }

        private void chksupplier_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chksupplier.Checked == true)
                {
                    chkcustomername.Checked = false;
                    customerdata();
                }
            }
            catch (Exception) { }
        }
    }
}