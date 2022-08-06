using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmVehicleReport : Form
    {
        CommonMethod cm = new CommonMethod();
        DbClass db = new DbClass();

        public frmVehicleReport()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            fillVehicle();
            customerdata();
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

        public void fillVehicle()
        {
            try
            {
                VehicleInformationRepository Vehiclerepo = new VehicleInformationRepository();
                List<VehicleInformation> cdata = new List<VehicleInformation>();
                cdata.Clear();
                cdata = Vehiclerepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.VehicleNumber).ToList();
                cdata.Add(new VehicleInformation { VehicleNumber = "Select", VehicleId = 0 });
                cdata.Reverse();
                cmbVehicle.DataSource = cdata;
                cmbVehicle.ValueMember = "VehicleId";
                cmbVehicle.DisplayMember = "VehicleNumber";
                Vehiclerepo.Dispose();
                cmbVehicle.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void customerdata()
        {
            try
            {
                CustomerRepository custrepo1 = new CustomerRepository();
                List<CustomerInformation> custdata = new List<CustomerInformation>();
                custdata.Clear();
                custdata = custrepo1.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CustomerName).ToList();
                custdata.Add(new CustomerInformation { CustomerName = "Select", CustomerID = 0 });
                custdata.Reverse();
                cmbcustomer.DataSource = custdata;
                cmbcustomer.ValueMember = "CustomerID";
                cmbcustomer.DisplayMember = "CustomerName";
                custrepo1.Dispose();
                cmbcustomer.SelectedIndex = 0;
            }
            catch (Exception) { }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                if (chkVehicleNo.Checked == true)
                {
                    if (ChkDate.Checked == true)
                    {
                        if (cmbVehicle.SelectedIndex > 0)
                        {
                            if (dtpfromdate.Value.Date <= dtptodate.Value.Date)
                                da = new SqlDataAdapter("select cm.BillNo,Convert(varchar(24),Billdate,103) as 'Bill Date',Convert(varchar(24),DcDate,103) as 'DC Date' ,cm.CustomerName,cm.Narration,dc.VehicalNo,cm.NetAmount,cm.DCNo from CustomerBillMaster as cm inner join DeliveryChallanMaster as dc on dc.DeliveryChallanNo=cm.DCNo where dc.VehicalNo='" + cmbVehicle.Text + "' and Billdate>='" + cm.ValidFromDate(dtpfromdate.Value.Date).ToString() + "' and Billdate<='" + cm.ValidToDate(dtptodate.Value.Date).ToString() + "' and cm.DcNo is not null and dc.VehicalNo is not null and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' order by Billdate,BillNo", db.Connection);
                            else
                                MessageBox.Show("From Date Should be Greater Than To Date");
                        }
                    }
                    else if (chkCustomer.Checked == true)
                    {
                        if (cmbcustomer.SelectedIndex > 0)
                            da = new SqlDataAdapter("select cm.BillNo,Convert(varchar(24),Billdate,103) as 'Bill Date',Convert(varchar(24),DcDate,103) as 'DC Date' ,cm.CustomerName,cm.Narration,dc.VehicalNo,cm.NetAmount,cm.DCNo from CustomerBillMaster as cm inner join DeliveryChallanMaster as dc on dc.DeliveryChallanNo=cm.DCNo where dc.VehicalNo='" + cmbVehicle.Text + "' and cm.CustomerID='" + cmbcustomer.SelectedValue + "' and cm.DcNo is not null and dc.VehicalNo is not null and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' order by Billdate,BillNo", db.Connection);
                    }
                    else
                    {
                        if (cmbVehicle.SelectedIndex > 0)
                            da = new SqlDataAdapter("select cm.BillNo,Convert(varchar(24),Billdate,103) as 'Bill Date',Convert(varchar(24),DcDate,103) as 'DC Date' ,cm.CustomerName,cm.Narration,dc.VehicalNo,cm.NetAmount,cm.DCNo from CustomerBillMaster as cm inner join DeliveryChallanMaster as dc on dc.DeliveryChallanNo=cm.DCNo where dc.VehicalNo='" + cmbVehicle.Text + "' and cm.DcNo is not null and dc.VehicalNo is not null and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' order by Billdate,BillNo", db.Connection);
                    }
                }
                else if (ChkDate.Checked == true)
                {
                    if (dtpfromdate.Value.Date <= dtptodate.Value.Date)
                    {
                        if (chkCustomer.Checked == true)
                        {
                            if (cmbcustomer.SelectedIndex > 0)
                                da = new SqlDataAdapter("select cm.BillNo,Convert(varchar(24),Billdate,103) as 'Bill Date',Convert(varchar(24),DcDate,103) as 'DC Date' ,cm.CustomerName,cm.Narration,dc.VehicalNo,cm.NetAmount,cm.DCNo from CustomerBillMaster as cm inner join DeliveryChallanMaster as dc on dc.DeliveryChallanNo=cm.DCNo where Billdate>='" + cm.ValidFromDate(dtpfromdate.Value.Date).ToString() + "' and Billdate<='" + cm.ValidToDate(dtptodate.Value.Date).ToString() + "' and cm.CustomerID='" + cmbcustomer.SelectedValue + "' and cm.DcNo is not null and dc.VehicalNo is not null and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' order by Billdate,BillNo", db.Connection);
                            else
                            {
                                MessageBox.Show("Select Customer Name");
                                this.ActiveControl = cmbcustomer;
                            }
                        }
                        else
                            da = new SqlDataAdapter("select cm.BillNo,Convert(varchar(24),Billdate,103) as 'Bill Date',Convert(varchar(24),DcDate,103) as 'DC Date' ,cm.CustomerName,cm.Narration,dc.VehicalNo,cm.NetAmount,cm.DCNo from CustomerBillMaster as cm inner join DeliveryChallanMaster as dc on dc.DeliveryChallanNo=cm.DCNo where Billdate>='" + cm.ValidFromDate(dtpfromdate.Value.Date).ToString() + "' and Billdate<='" + cm.ValidToDate(dtptodate.Value.Date).ToString() + "' and cm.DcNo is not null and dc.VehicalNo is not null and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' order by Billdate,BillNo", db.Connection);
                    }
                    else
                        MessageBox.Show("From Date Should be Greater Than To Date");
                }
                else if (chkCustomer.Checked == true)
                {
                    if (cmbcustomer.SelectedIndex > 0)
                        da = new SqlDataAdapter("select cm.BillNo,Convert(varchar(24),Billdate,103) as 'Bill Date',Convert(varchar(24),DcDate,103) as 'DC Date' ,cm.CustomerName,cm.Narration,dc.VehicalNo,cm.NetAmount,cm.DCNo from CustomerBillMaster as cm inner join DeliveryChallanMaster as dc on dc.DeliveryChallanNo=cm.DCNo where cm.CustomerID='" + cmbcustomer.SelectedValue + "' and cm.DcNo is not null and dc.VehicalNo is not null and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' order by Billdate,BillNo", db.Connection);
                    else
                    {
                        MessageBox.Show("Select Customer Name");
                        this.ActiveControl = cmbcustomer;
                    }
                }
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dtgvVehicleReport.DataSource = dt;
                    dtgvVehicleReport.Refresh();
                    if (dtgvVehicleReport.Rows.Count != 0)
                    {
                        dtgvVehicleReport.Columns["DCNo"].Visible = false;
                        dtgvVehicleReport.Columns[0].Width = 70;
                        dtgvVehicleReport.Columns[1].Width = 80;
                        dtgvVehicleReport.Columns[2].Width = 80;
                        dtgvVehicleReport.Columns[3].Width = 180;
                        dtgvVehicleReport.Columns[4].Width = 180;
                        dtgvVehicleReport.Columns[5].Width = 90;
                        dtgvVehicleReport.Columns[6].Width = 80;
                        dtgvVehicleReport.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                    }
                }
                else
                    MessageBox.Show("Record not found");
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                string customername = "", vehicleno = "", fromdate = "", todate = "";
                RptVehicleReport rpt;
                if (chkCustomer.Checked == true)
                    customername = cmbcustomer.Text;
                if (chkVehicleNo.Checked == true)
                    vehicleno = cmbVehicle.Text;
                if (ChkDate.Checked == true)
                {
                    fromdate = dtpfromdate.Text;
                    todate = dtptodate.Text;
                }
                rpt = new RptVehicleReport(vehicleno, fromdate, todate, customername);
                rpt.ShowDialog();
            }
            catch (Exception) { }
        }

        private void chkCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcustomer;
            }
            catch (Exception) { }
        }

        private void cmbcustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception) { }
        }

        private void chkVehicleNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbVehicle;
            }
            catch (Exception) { }
        }

        private void cmbVehicle_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception) { }
        }

        private void ChkDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpfromdate;
            }
            catch (Exception) { }
        }

        private void dtpfromdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtptodate;
            }
            catch (Exception) { }
        }

        private void dtptodate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception) { }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            try
            {
                chkCustomer.Checked = false;
                ChkDate.Checked = false;
                chkVehicleNo.Checked = false;
                SqlDataAdapter da = new SqlDataAdapter("select cm.BillNo,Convert(varchar(24),Billdate,103) as 'Bill Date',Convert(varchar(24),DcDate,103) as 'DC Date' ,cm.CustomerName,cm.Narration,dc.VehicalNo,cm.NetAmount,cm.DCNo from CustomerBillMaster as cm inner join DeliveryChallanMaster as dc on dc.DeliveryChallanNo=cm.DCNo where cm.DcNo is not null and dc.VehicalNo is not null and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cm.CompId='" + CommonMethod.CompId + "' order by Billdate,BillNo", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dtgvVehicleReport.Rows.Clear();
                dtgvVehicleReport.DataSource = dt;
                dtgvVehicleReport.Refresh();
                if (dtgvVehicleReport.Rows.Count != 0)
                {
                    dtgvVehicleReport.Columns["DCNo"].Visible = false;
                    dtgvVehicleReport.Columns[0].Width = 70;
                    dtgvVehicleReport.Columns[1].Width = 80;
                    dtgvVehicleReport.Columns[2].Width = 80;
                    dtgvVehicleReport.Columns[3].Width = 180;
                    dtgvVehicleReport.Columns[4].Width = 180;
                    dtgvVehicleReport.Columns[5].Width = 100;
                    dtgvVehicleReport.Columns[6].Width = 80;
                    dtgvVehicleReport.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopRight;
                }
                else
                    MessageBox.Show("Record not found");
            }
            catch (Exception)
            { }
        }
    }
}