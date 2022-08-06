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
    public partial class frmPrintMultipleNoVatBillList : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        private string input_Copies = "", input = "0";
        List<int> billList = new List<int>();
        string FlagData;

        public frmPrintMultipleNoVatBillList()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            getalldata();
            customerdata();
        }

        public frmPrintMultipleNoVatBillList(string flag)
        {
            FlagData = flag;
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            if (flag == "WithoutVat")
                getalldata();
            else if (flag == "WithVat")
                VatGridData();
            else if (flag == "Fake")
                fakeGridData();
            customerdata();
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
                cmbcustomername.DataSource = customerdata;
                cmbcustomername.ValueMember = "CustomerID";
                cmbcustomername.DisplayMember = "CustomerName";
                custrepo.Dispose();
                cmbcustomername.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void fakeGridData()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount]  FROM [CustomerBillMaster] where BillNo like 'F%' and BillStatus=0  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvBillInfo.DataSource = dt;
                GvBillInfo.Refresh();
                lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                if (GvBillInfo.Rows.Count != 0)
                {
                    GvBillInfo.Columns["Id"].Visible = false;
                    GvBillInfo.Columns[3].Width = 60;
                    GvBillInfo.Columns[4].Width = 100;
                    GvBillInfo.Columns[5].Width = 150;
                    GvBillInfo.Columns[6].Width = 120;
                    GvBillInfo.Columns[7].Width = 70;
                    GvBillInfo.Columns[8].Width = 100;
                }
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        public void VatGridData()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount]  FROM [CustomerBillMaster] where BillNo like 'T%'  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvBillInfo.DataSource = dt;
                lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                GvBillInfo.Refresh();
                if (GvBillInfo.Rows.Count != 0)
                {
                    GvBillInfo.Columns["Id"].Visible = false;
                    GvBillInfo.Columns[2].Width = 100;
                    GvBillInfo.Columns[3].Width = 120;
                    GvBillInfo.Columns[4].Width = 170;
                    GvBillInfo.Columns[5].Width = 100;
                    GvBillInfo.Columns[6].Width = 70;
                    GvBillInfo.Columns[7].Width = 100;
                }
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        public void getalldata()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount]  FROM [CustomerBillMaster] where BillNo like 'E%'  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvBillInfo.DataSource = dt;
                GvBillInfo.Refresh();
                lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                if (GvBillInfo.Rows.Count != 0)
                {
                    GvBillInfo.Columns["Id"].Visible = false;
                    GvBillInfo.Columns[3].Width = 100;
                    GvBillInfo.Columns[4].Width = 100;
                    GvBillInfo.Columns[5].Width = 170;
                    GvBillInfo.Columns[6].Width = 100;
                    GvBillInfo.Columns[7].Width = 100;
                    GvBillInfo.Columns[8].Width = 100;
                }
                da.Dispose();
                dt.Dispose();
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

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (FlagData == "WithoutVat")
                {
                    if (chkbetweendate.Checked == true)
                    {
                        if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                        {
                            SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount]  FROM [CustomerBillMaster] where Billdate >= '" + cm.ValidFromDate(dtpfromdate.Value.Date) + "' and Billdate <= '" + cm.ValidToDate(dtptodate.Value.Date) + "' and BillNo like 'E%'  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            GvBillInfo.DataSource = dt;
                            GvBillInfo.Refresh();
                            lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                            if (GvBillInfo.Rows.Count != 0)
                            {
                                GvBillInfo.Columns["Id"].Visible = false;
                                GvBillInfo.Columns[3].Width = 100;
                                GvBillInfo.Columns[4].Width = 100;
                                GvBillInfo.Columns[5].Width = 170;
                                GvBillInfo.Columns[6].Width = 100;
                                GvBillInfo.Columns[7].Width = 100;
                                GvBillInfo.Columns[8].Width = 100;
                                this.ActiveControl = dtpfromdate;
                            }
                            else
                            {
                                MessageBox.Show("Record Not found.", "Warning");
                                this.ActiveControl = dtpfromdate;
                            }
                            da.Dispose();
                            dt.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("To Date Should greater than from date.", "Warning");
                            this.ActiveControl = dtptodate;
                        }
                    }
                    if (chkbillno.Checked == true)
                    {
                        if (cmbcustomername.Text != "Select" && cmbcustomername.Text != "")
                        {
                            SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount]  FROM [CustomerBillMaster] where CustomerId='" + cmbcustomername.SelectedValue + "' and BillNo like 'E%' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            GvBillInfo.DataSource = dt;
                            GvBillInfo.Refresh();
                            lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                            if (GvBillInfo.Rows.Count != 0)
                            {
                                GvBillInfo.Columns["Id"].Visible = false;
                                GvBillInfo.Columns[3].Width = 100;
                                GvBillInfo.Columns[4].Width = 100;
                                GvBillInfo.Columns[5].Width = 170;
                                GvBillInfo.Columns[6].Width = 100;
                                GvBillInfo.Columns[7].Width = 100;
                                GvBillInfo.Columns[8].Width = 100;
                                this.ActiveControl = cmbcustomername;
                            }
                            else
                            {
                                MessageBox.Show("Record Not found.", "Warning");
                                this.ActiveControl = cmbcustomername;
                            }
                            da.Dispose();
                            dt.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Select Customer Name.", "Warning");
                            this.ActiveControl = cmbcustomername;
                        }
                    }
                }
                else if (FlagData == "WithVat")
                {
                    if (chkbillno.Checked == true)
                    {
                        if (cmbcustomername.Text != "Select")
                        {
                            SqlDataAdapter da = new SqlDataAdapter("SELECT [Id] ,[BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount]  FROM [CustomerBillMaster] where CustomerId='" + cmbcustomername.SelectedValue + "' and BillNo like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            GvBillInfo.DataSource = dt;
                            lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                            GvBillInfo.Refresh();
                            if (GvBillInfo.Rows.Count != 0)
                            {
                                GvBillInfo.Columns["Id"].Visible = false;
                                GvBillInfo.Columns[2].Width = 100;
                                GvBillInfo.Columns[3].Width = 120;
                                GvBillInfo.Columns[4].Width = 170;
                                GvBillInfo.Columns[5].Width = 100;
                                GvBillInfo.Columns[6].Width = 70;
                                GvBillInfo.Columns[7].Width = 100;
                                this.ActiveControl = cmbcustomername;
                            }
                            else
                            {
                                MessageBox.Show("Record not found.", "Warning");
                                this.ActiveControl = cmbcustomername;
                            }
                            da.Dispose();
                            dt.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Select Customer Name.", "Warning");
                            this.ActiveControl = cmbcustomername;
                        }
                    }
                    if (chkbetweendate.Checked == true)
                    {
                        if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                        {
                            SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount]  FROM [CustomerBillMaster] where Billdate>='" + cm.ValidFromDate(dtpfromdate.Value.Date) + "' and Billdate<='" + cm.ValidToDate(dtptodate.Value.Date) + "' and BillNo like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            GvBillInfo.DataSource = dt;
                            lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                            GvBillInfo.Refresh();
                            if (GvBillInfo.Rows.Count != 0)
                            {
                                GvBillInfo.Columns["Id"].Visible = false;
                                GvBillInfo.Columns[2].Width = 100;
                                GvBillInfo.Columns[3].Width = 120;
                                GvBillInfo.Columns[4].Width = 170;
                                GvBillInfo.Columns[5].Width = 100;
                                GvBillInfo.Columns[6].Width = 70;
                                GvBillInfo.Columns[7].Width = 100;
                                this.ActiveControl = dtpfromdate;
                            }
                            else
                            {
                                MessageBox.Show("Record not found.", "Warning");
                                this.ActiveControl = dtpfromdate;
                            }
                            da.Dispose();
                            dt.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("To Date Should greater than from date.", "Warning");
                            this.ActiveControl = dtptodate;
                        }
                    }
                }
                else if (FlagData == "Fake")
                {
                    if (chkbillno.Checked == true)
                    {
                        if (cmbcustomername.Text != "Select")
                        {
                            SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount]  FROM [CustomerBillMaster] where BillNo like 'F%' and CustomerId='" + cmbcustomername.SelectedValue + "' and BillStatus=0 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            GvBillInfo.DataSource = dt;
                            GvBillInfo.Refresh();
                            lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                            if (GvBillInfo.Rows.Count != 0)
                            {
                                GvBillInfo.Columns["Id"].Visible = false;
                                GvBillInfo.Columns[3].Width = 60;
                                GvBillInfo.Columns[4].Width = 100;
                                GvBillInfo.Columns[5].Width = 150;
                                GvBillInfo.Columns[6].Width = 120;
                                GvBillInfo.Columns[7].Width = 70;
                                GvBillInfo.Columns[8].Width = 100;
                                this.ActiveControl = cmbcustomername;
                            }
                            else
                            {
                                MessageBox.Show("Record not found.", "Warning");
                                this.ActiveControl = cmbcustomername;
                            }
                            da.Dispose();
                            dt.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Select Customer Name.", "Warning");
                            this.ActiveControl = cmbcustomername;
                        }
                    }
                    if (chkbetweendate.Checked == true)
                    {
                        if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                        {
                            SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [BillNo],CONVERT(VARCHAR(24),Billdate,103) as 'Bill Date',[CustomerName],[TotalAmount],[Discount],[NetAmount]  FROM [CustomerBillMaster] where BillNo like 'F%' and Billdate>='" + cm.ValidFromDate(dtpfromdate.Value.Date) + "' and Billdate<='" + cm.ValidToDate(dtptodate.Value.Date) + "' and BillStatus=0 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            GvBillInfo.DataSource = dt;
                            GvBillInfo.Refresh();
                            lblTotalBill.Text = Convert.ToString(GvBillInfo.Rows.Count);
                            if (GvBillInfo.Rows.Count != 0)
                            {
                                GvBillInfo.Columns["Id"].Visible = false;
                                GvBillInfo.Columns[3].Width = 60;
                                GvBillInfo.Columns[4].Width = 100;
                                GvBillInfo.Columns[5].Width = 150;
                                GvBillInfo.Columns[6].Width = 120;
                                GvBillInfo.Columns[7].Width = 70;
                                GvBillInfo.Columns[8].Width = 100;
                                this.ActiveControl = dtpfromdate;
                            }
                            else
                            {
                                MessageBox.Show("Record not found.", "Warning");
                                this.ActiveControl = dtpfromdate;
                            }
                            da.Dispose();
                            dt.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("To Date Should greater than from date.", "Warning");
                            this.ActiveControl = dtptodate;
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btngetall_Click(object sender, EventArgs e)
        {
            try
            {
                if (FlagData == "WithoutVat")
                    getalldata();
                else if (FlagData == "WithVat")
                    VatGridData();
                else if (FlagData == "Fake")
                    fakeGridData();
            }
            catch (Exception)
            { }
        }

        private void GvBillInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GvBillInfo.Columns[e.ColumnIndex].HeaderText == "Update")
                {
                    frmCustomerBill custbillform = new frmCustomerBill(Convert.ToString(GvBillInfo.Rows[e.RowIndex].Cells[3].Value));
                    custbillform.ShowDialog();
                }
            }
            catch (Exception)
            { }
        }

        private void frmPrintMultipleNoVatBillList_Load(object sender, EventArgs e)
        {
            pnlCopies.Visible = false;
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void btnCOk_Click(object sender, EventArgs e)
        {
            string CopyString = "";
            pnlCopies.Visible = false;

            if (Convert.ToInt32(input_Copies) > 3)
            {
                MessageBox.Show("Copies should not be more than 3", "Warning");
                input_Copies = "0";
            }
            else
            {
                if (Convert.ToInt32(input_Copies) == 1)
                {
                    chkCust.Checked = true;
                    if (chkCust.Checked == true)
                        CopyString = "Original Copy";
                    if (chkTrans.Checked == true)
                        CopyString = "Transporter Copy";
                    if (chkSend.Checked == true)
                        CopyString = "Sender Copy";
                }
                if (Convert.ToInt32(input_Copies) == 2)
                {
                    if (chkCust.Checked == true)
                    {
                        if (CopyString != "")
                            CopyString = CopyString + "," + "Original Copy";
                        else
                            CopyString = "Original Copy";
                    }
                    if (chkTrans.Checked == true)
                    {
                        if (CopyString != "")
                            CopyString = CopyString + "," + "Transporter Copy";
                        else
                            CopyString = "Transporter Copy";
                    }
                    if (chkSend.Checked == true)
                    {
                        if (CopyString != "")
                            CopyString = CopyString + "," + "Sender Copy";
                        else
                            CopyString = "Sender Copy";
                    }
                }
                if (Convert.ToInt32(input_Copies) == 3)
                    CopyString = "Original Copy,Transporter Copy,Sender Copy";
            }

            string[] splitcopies;
            for (int i = 1; i <= Convert.ToInt32(input_Copies); i++)
            {
                string copyType = "";
                if (CopyString.Contains(','))
                {
                    splitcopies = CopyString.Split(',');
                    copyType = splitcopies[i - 1];
                }
                else
                    copyType = CopyString;
                if (FlagData == "WithVat")
                {
                    RptCustomerbill custrpt12 = new RptCustomerbill(billList, "WithVat", input, copyType);
                }
            }
        }

        private void btnMultiPrint_Click(object sender, EventArgs e)
        {
            try
            {
                SettingRepository SettingRepo = new SettingRepository();
                Setting SettingData = new Setting();
                SettingData = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (SettingData != null)
                {
                    if (!string.IsNullOrEmpty(SettingData.PrinterName))
                    {
                        billList.Clear();
                        foreach (DataGridViewRow row in GvBillInfo.Rows)
                        {
                            DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                            if (chk.Value != null)
                            {
                                if ((bool)chk.Value == true)
                                {
                                    if (billList.Contains(Convert.ToInt32(row.Cells[2].Value)))
                                    { }
                                    else
                                        billList.Add(Convert.ToInt32(row.Cells[2].Value));
                                }
                            }
                        }
                        if (billList.Count != 0)
                        {
                            if (SettingData.TaxInvoicePrintOn == "Both")
                                input = Microsoft.VisualBasic.Interaction.InputBox("1 For Portrait and 2 For Landscape and 3 For A5 and 4 For Marathi Bill", "Tax Invoice", "", 250, 250);
                            else if (SettingData.TaxInvoicePrintOn == "Portrait")
                                input = "1";
                            else if (SettingData.TaxInvoicePrintOn == "HoriZontal")
                                input = "2";
                            else if (SettingData.TaxInvoicePrintOn == "Marathi")
                                input = "4";
                            else if (SettingData.TaxInvoicePrintOn == "A5")
                                input = "3";
                            else if (SettingData.TaxInvoicePrintOn == "MarathiA6")
                                input = "5";
                            else if (SettingData.TaxInvoicePrintOn == "Slab")
                                input = "7";
                            else if (SettingData.TaxInvoicePrintOn == "A6")
                                input = "8";
                            else if (SettingData.TaxInvoicePrintOn == "HorizontalA5")
                                input = "9";
                            if (FlagData == "WithVat")
                            {
                                if (SettingData.MultipleCopies == "Y")
                                {
                                    input_Copies = Microsoft.VisualBasic.Interaction.InputBox("How Many Copies?", "Tax Invoice", "", 250, 250);
                                    if (Convert.ToInt32(input_Copies) > 3)
                                    {
                                        MessageBox.Show("Copies should not be more than 3", "Warning");
                                        input_Copies = "0";
                                    }
                                    else
                                        pnlCopies.Visible = true;
                                }
                                else
                                {
                                    string copyType = "Original Copy";
                                    RptCustomerbill custrpt12 = new RptCustomerbill(billList, "WithVat", input, copyType);
                                }
                            }
                            else
                            {
                                if (FlagData == "Fake")
                                {
                                    RptCustomerbill custrpt1 = new RptCustomerbill(billList, "Fake", input);
                                }
                                else if (FlagData == "WithoutVat")
                                {
                                    RptNoVatBill custnovat = new RptNoVatBill(billList, "WithoutVat");
                                }
                            }
                        }
                        else
                            MessageBox.Show("Please select at least one bill!!");
                    }
                }
                else
                {
                    MessageBox.Show("PrinterName is not set.", "Warning");
                    this.ActiveControl = btnMultiPrint;
                }
                SettingRepo.Dispose();
            }
            catch (Exception)
            { }
        }
    }
}