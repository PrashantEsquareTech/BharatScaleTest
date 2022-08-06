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
    public partial class frmSendSMSTracking : Form
    {
        DbClass db = new DbClass();

        public frmSendSMSTracking()
        {
            InitializeComponent();
            FillCustomer();
            FillStatus();
        }

        public void FillStatus()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT DISTINCT Status FROM dbo.SendSMS where CompId='" + CommonMethod.CompId + "' order by Status", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmbStatus.DisplayMember = "Status";
                cmbStatus.ValueMember = null;
                cmbStatus.DataSource = dt;
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        public void FillCustomer()
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

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                lblSendSMS.Text = "0";
                if (chkCustName.Checked == true)
                {
                    if (cmbcustomername.Text != "Select")
                    {
                        if (chkCustName.Checked == true && chkStatus.Checked == true)
                        {
                            if (cmbcustomername.Text != "Select" && cmbStatus.Text != "Select")
                            {
                                GvSendSMS.DataSource = null;
                                SqlDataAdapter da = new SqlDataAdapter("SELECT CustomerName, BillNo as 'Bill No',CONVERT(VARCHAR(24),SmsDate,103) as 'SMS Date', MobileNo as 'Mobile No', SmsText as  'Text',Status FROM         dbo.SendSMS  WHERE     (CustomerId = " + Convert.ToInt32(cmbcustomername.SelectedValue) + ") AND (Status = N'" + cmbStatus.Text + "') and CompId='" + CommonMethod.CompId + "'", db.Connection);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    object SendSMS;
                                    SendSMS = dt.Compute("Count(CustomerName)", "");
                                    GvSendSMS.DataSource = dt;
                                    GvSendSMS.Refresh();
                                    lblSendSMS.Text = Convert.ToString(SendSMS);
                                    GvSendSMS.Columns[0].Width = 170;
                                    GvSendSMS.Columns[1].Width = 90;
                                    GvSendSMS.Columns[2].Width = 100;
                                    GvSendSMS.Columns[3].Width = 170;
                                    GvSendSMS.Columns[4].Width = 500;
                                }
                                else
                                    MessageBox.Show("Record Not Found.", "Warning");
                                da.Dispose();
                                dt.Dispose();
                            }
                            else
                            {
                                if (cmbcustomername.Text != "Select")
                                {
                                    MessageBox.Show("Please Select Customer Name.", "Warning");
                                    this.ActiveControl = cmbcustomername;
                                }
                                else if (cmbStatus.Text != "Select")
                                {
                                    MessageBox.Show("Please Select Status.", "Warning");
                                    this.ActiveControl = cmbStatus;
                                }
                            }
                        }
                        else if (chkCustName.Checked == true && chkMobile.Checked == true)
                        {
                            if (cmbcustomername.Text != "Select" && chkMobile.Text != "")
                            {
                                GvSendSMS.DataSource = null;
                                SqlDataAdapter da = new SqlDataAdapter("SELECT CustomerName, BillNo as 'Bill No',CONVERT(VARCHAR(24),SmsDate,103) as 'SMS Date', MobileNo as 'Mobile No', SmsText as  'Text',Status FROM         dbo.SendSMS  WHERE     (CustomerId = " + Convert.ToInt32(cmbcustomername.SelectedValue) + ") AND (MobileNo = N'" + txtMobile.Text + "') AND ((Status = N'Auto') OR (Status = N'Mannual') OR  (Status = N'Credit')) and CompId='" + CommonMethod.CompId + "'", db.Connection);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    object SendSMS;
                                    SendSMS = dt.Compute("Count(CustomerName)", "");
                                    GvSendSMS.DataSource = dt;
                                    GvSendSMS.Refresh();
                                    lblSendSMS.Text = Convert.ToString(SendSMS);
                                    GvSendSMS.Columns[0].Width = 170;
                                    GvSendSMS.Columns[1].Width = 90;
                                    GvSendSMS.Columns[2].Width = 100;
                                    GvSendSMS.Columns[3].Width = 170;
                                    GvSendSMS.Columns[4].Width = 500;
                                }
                                else
                                    MessageBox.Show("Record Not Found.", "Warning");
                                da.Dispose();
                                dt.Dispose();
                            }
                            else
                            {
                                if (cmbcustomername.Text != "Select")
                                {
                                    MessageBox.Show("Please Select Customer Name.", "Warning");
                                    this.ActiveControl = cmbcustomername;
                                }
                                else if (chkMobile.Text != "")
                                {
                                    MessageBox.Show("Please Enter Mobile No.", "Warning");
                                    this.ActiveControl = txtMobile;
                                }
                            }
                        }
                        else
                        {
                            GvSendSMS.DataSource = null;
                            SqlDataAdapter da = new SqlDataAdapter("SELECT CustomerName, BillNo as 'Bill No',CONVERT(VARCHAR(24),SmsDate,103) as 'SMS Date', MobileNo as 'Mobile No', SmsText as  'Text',Status FROM         dbo.SendSMS  WHERE     (CustomerId = " + Convert.ToInt32(cmbcustomername.SelectedValue) + ") and CompId='" + CommonMethod.CompId + "'", db.Connection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                object SendSMS;
                                SendSMS = dt.Compute("Count(CustomerName)", "");
                                GvSendSMS.DataSource = dt;
                                GvSendSMS.Refresh();
                                lblSendSMS.Text = Convert.ToString(SendSMS);
                                GvSendSMS.Columns[0].Width = 170;
                                GvSendSMS.Columns[1].Width = 90;
                                GvSendSMS.Columns[2].Width = 100;
                                GvSendSMS.Columns[3].Width = 170;
                                GvSendSMS.Columns[4].Width = 500;
                            }
                            else
                                MessageBox.Show("Record Not Found.", "Warning");
                            da.Dispose();
                            dt.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Select Customer Name.", "Warning");
                        this.ActiveControl = cmbcustomername;
                    }
                }
                else if (chkStatus.Checked == true)
                {
                    if (cmbStatus.Text != "Select")
                    {
                        GvSendSMS.DataSource = null;
                        SqlDataAdapter da = new SqlDataAdapter("SELECT CustomerName, BillNo as 'Bill No',CONVERT(VARCHAR(24),SmsDate,103) as 'SMS Date', MobileNo as 'Mobile No', SmsText as  'Text',Status FROM         dbo.SendSMS  WHERE (Status = N'" + cmbStatus.Text + "') and CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            object SendSMS;
                            SendSMS = dt.Compute("Count(CustomerName)", "");
                            GvSendSMS.DataSource = dt;
                            GvSendSMS.Refresh();
                            lblSendSMS.Text = Convert.ToString(SendSMS);
                            GvSendSMS.Columns[0].Width = 170;
                            GvSendSMS.Columns[1].Width = 90;
                            GvSendSMS.Columns[2].Width = 100;
                            GvSendSMS.Columns[3].Width = 170;
                            GvSendSMS.Columns[4].Width = 500;
                        }
                        else
                            MessageBox.Show("Record Not Found.", "Warning");
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Please Select Status.", "Warning");
                        this.ActiveControl = cmbcustomername;
                    }
                }
                else if (chkMobile.Checked == true)
                {
                    if (txtMobile.Text != "")
                    {
                        GvSendSMS.DataSource = null;
                        SqlDataAdapter da = new SqlDataAdapter("SELECT CustomerName, BillNo as 'Bill No',CONVERT(VARCHAR(24),SmsDate,103) as 'SMS Date', MobileNo as 'Mobile No', SmsText as  'Text',Status FROM         dbo.SendSMS  WHERE MobileNo = N'" + txtMobile.Text.Trim() + "' and CompId='" + CommonMethod.CompId + "' ", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            object SendSMS;
                            SendSMS = dt.Compute("Count(CustomerName)", "");
                            GvSendSMS.DataSource = dt;
                            GvSendSMS.Refresh();
                            lblSendSMS.Text = Convert.ToString(SendSMS);
                            GvSendSMS.Columns[0].Width = 170;
                            GvSendSMS.Columns[1].Width = 90;
                            GvSendSMS.Columns[2].Width = 100;
                            GvSendSMS.Columns[3].Width = 170;
                            GvSendSMS.Columns[4].Width = 500;
                        }
                        else
                            MessageBox.Show("Record Not Found.", "Warning");
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Mobile No.", "Warning");
                        this.ActiveControl = cmbcustomername;
                    }
                }
                else if (chkBillNo.Checked == true)
                {
                    if (txtBillNo.Text != "")
                    {
                        GvSendSMS.DataSource = null;
                        SqlDataAdapter da = new SqlDataAdapter("SELECT CustomerName, BillNo as 'Bill No',CONVERT(VARCHAR(24),SmsDate,103) as 'SMS Date', MobileNo as 'Mobile No', SmsText as  'Text',Status FROM         dbo.SendSMS  WHERE (BillNo = N'" + txtBillNo.Text + "') and CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            string status = dt.Rows[0][5].ToString();
                            object SendSMS, FailSMS;
                            SendSMS = dt.Compute("Count(CustomerName)", "");
                            FailSMS = dt.Compute("Count(CustomerName)", "");
                            GvSendSMS.DataSource = dt;
                            GvSendSMS.Refresh();
                            if (status != "Send Option is Auto but customer dose not have mobile no!!")
                                lblSendSMS.Text = Convert.ToString(SendSMS);
                            GvSendSMS.Columns[0].Width = 170;
                            GvSendSMS.Columns[1].Width = 90;
                            GvSendSMS.Columns[3].Width = 170;
                            GvSendSMS.Columns[4].Width = 500;
                        }
                        else
                            MessageBox.Show("Record Not Found.", "Warning");
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Bill No.", "Warning");
                        this.ActiveControl = cmbcustomername;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void frmSendSMSTracking_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            try
            {
                lblSendSMS.Text = "0";
                if (chkCustName.Checked == true)
                {
                    if (cmbcustomername.Text != "Select")
                    {
                        if (chkCustName.Checked == true && chkStatus.Checked == true)
                        {
                            if (cmbcustomername.Text != "Select" && cmbStatus.Text != "Select")
                            {
                                GvSendSMS.DataSource = null;
                                SqlDataAdapter da = new SqlDataAdapter("SELECT CustomerName, BillNo as 'Bill No',CONVERT(VARCHAR(24),SmsDate,103) as 'SMS Date', MobileNo as 'Mobile No', SmsText as  'Text',Status FROM         dbo.SendSMS  WHERE     (CustomerId = " + Convert.ToInt32(cmbcustomername.SelectedValue) + ") AND (Status = N'" + cmbStatus.Text + "') and CompId='" + CommonMethod.CompId + "' ", db.Connection);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    GvSendSMS.DataSource = dt;
                                    GvSendSMS.Refresh();
                                    GvSendSMS.Columns[0].Width = 170;
                                    GvSendSMS.Columns[1].Width = 90;
                                    GvSendSMS.Columns[2].Width = 100;
                                    GvSendSMS.Columns[3].Width = 170;
                                    GvSendSMS.Columns[4].Width = 500;
                                }
                                else
                                    MessageBox.Show("Record Not Found.", "Warning");
                                da.Dispose();
                                dt.Dispose();
                            }
                            else
                            {
                                if (cmbcustomername.Text != "Select")
                                {
                                    MessageBox.Show("Please Select Customer Name.", "Warning");
                                    this.ActiveControl = cmbcustomername;
                                }
                                else if (cmbStatus.Text != "Select")
                                {
                                    MessageBox.Show("Please Select Status.", "Warning");
                                    this.ActiveControl = cmbStatus;
                                }
                            }
                        }
                        else if (chkCustName.Checked == true && chkMobile.Checked == true)
                        {
                            if (cmbcustomername.Text != "Select" && chkMobile.Text != "")
                            {
                                GvSendSMS.DataSource = null;
                                SqlDataAdapter da = new SqlDataAdapter("SELECT CustomerName, BillNo as 'Bill No',CONVERT(VARCHAR(24),SmsDate,103) as 'SMS Date', MobileNo as 'Mobile No', SmsText as  'Text',Status FROM         dbo.SendSMS  WHERE     (CustomerId = " + Convert.ToInt32(cmbcustomername.SelectedValue) + ") AND (MobileNo = N'" + txtMobile.Text + "') AND (Status = N'Send Option is Auto but customer dose not have mobile no!!') and CompId='" + CommonMethod.CompId + "'", db.Connection);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    GvSendSMS.DataSource = dt;
                                    GvSendSMS.Refresh();
                                    GvSendSMS.Columns[0].Width = 170;
                                    GvSendSMS.Columns[1].Width = 90;
                                    GvSendSMS.Columns[2].Width = 100;
                                    GvSendSMS.Columns[3].Width = 170;
                                    GvSendSMS.Columns[4].Width = 500;
                                }
                                else
                                    MessageBox.Show("Record Not Found.", "Warning");
                                da.Dispose();
                                dt.Dispose();
                            }
                            else
                            {
                                if (cmbcustomername.Text != "Select")
                                {
                                    MessageBox.Show("Please Select Customer Name.", "Warning");
                                    this.ActiveControl = cmbcustomername;
                                }
                                else if (chkMobile.Text != "")
                                {
                                    MessageBox.Show("Please Enter Mobile No.", "Warning");
                                    this.ActiveControl = txtMobile;
                                }
                            }
                        }
                        else
                        {
                            GvSendSMS.DataSource = null;
                            SqlDataAdapter da = new SqlDataAdapter("SELECT CustomerName, BillNo as 'Bill No',CONVERT(VARCHAR(24),SmsDate,103) as 'SMS Date', MobileNo as 'Mobile No', SmsText as  'Text',Status FROM         dbo.SendSMS  WHERE     (CustomerId = " + Convert.ToInt32(cmbcustomername.SelectedValue) + ") AND (Status = N'Send Option is Auto but customer dose not have mobile no!!') and CompId='" + CommonMethod.CompId + "'", db.Connection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                GvSendSMS.DataSource = dt;
                                GvSendSMS.Refresh();
                                GvSendSMS.Columns[0].Width = 170;
                                GvSendSMS.Columns[1].Width = 90;
                                GvSendSMS.Columns[2].Width = 100;
                                GvSendSMS.Columns[3].Width = 170;
                                GvSendSMS.Columns[4].Width = 500;
                            }
                            else
                                MessageBox.Show("Record Not Found.", "Warning");
                            da.Dispose();
                            dt.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Select Customer Name.", "Warning");
                        this.ActiveControl = cmbcustomername;
                    }
                }
                else if (chkMobile.Checked == true)
                {
                    if (txtMobile.Text != "")
                    {
                        GvSendSMS.DataSource = null;
                        SqlDataAdapter da = new SqlDataAdapter("SELECT CustomerName, BillNo as 'Bill No',CONVERT(VARCHAR(24),SmsDate,103) as 'SMS Date', MobileNo as 'Mobile No', SmsText as  'Text',Status FROM         dbo.SendSMS  WHERE (MobileNo = N'" + txtMobile.Text + "') AND (Status = N'Send Option is Auto but customer dose not have mobile no!!') and CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GvSendSMS.DataSource = dt;
                            GvSendSMS.Refresh();
                            GvSendSMS.Columns[0].Width = 170;
                            GvSendSMS.Columns[1].Width = 90;
                            GvSendSMS.Columns[2].Width = 100;
                            GvSendSMS.Columns[3].Width = 170;
                            GvSendSMS.Columns[4].Width = 500;
                        }
                        else
                            MessageBox.Show("Record Not Found.", "Warning");
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Please Select Customer Name.", "Warning");
                        this.ActiveControl = cmbcustomername;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnTOdaySMS_Click(object sender, EventArgs e)
        {
            try
            {
                lblSendSMS.Text = "0";
                GvSendSMS.DataSource = null;
                string date = Convert.ToString(DateTime.Now.Date);
                string[] nowDate = date.Split(' ');
                string curDate = nowDate[0];
                SqlDataAdapter da = new SqlDataAdapter("SELECT CustomerName, BillNo as 'Bill No',SmsDate as 'SMS Date', MobileNo as 'Mobile No', SmsText as  'Text',Status FROM         dbo.SendSMS  WHERE   CONVERT(VARCHAR(24),SmsDate,103) ='" + nowDate[0] + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    object SendSMS;
                    SendSMS = dt.Compute("Count(CustomerName)", "");
                    GvSendSMS.DataSource = dt;
                    GvSendSMS.Refresh();
                    lblSendSMS.Text = Convert.ToString(SendSMS);
                    GvSendSMS.Columns[0].Width = 170;
                    GvSendSMS.Columns[1].Width = 90;
                    GvSendSMS.Columns[2].Width = 100;
                    GvSendSMS.Columns[3].Width = 170;
                    GvSendSMS.Columns[4].Width = 500;
                }
                else
                    MessageBox.Show("Record Not Found.", "Warning");
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btngetall_Click(object sender, EventArgs e)
        {
            try
            {
                lblSendSMS.Text = "0";
                GvSendSMS.DataSource = null;
                string date = Convert.ToString(DateTime.Now.Date);
                string[] nowDate = date.Split(' ');
                string curDate = nowDate[0];
                SqlDataAdapter da = new SqlDataAdapter("SELECT CustomerName, BillNo as 'Bill No',SmsDate as 'SMS Date', MobileNo as 'Mobile No', SmsText as  'Text',Status FROM         dbo.SendSMS  where CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    object SendSMS;
                    SendSMS = dt.Compute("Count(CustomerName)", "");
                    GvSendSMS.DataSource = dt;
                    GvSendSMS.Refresh();
                    lblSendSMS.Text = Convert.ToString(SendSMS);
                    GvSendSMS.Columns[0].Width = 170;
                    GvSendSMS.Columns[1].Width = 90;
                    GvSendSMS.Columns[2].Width = 100;
                    GvSendSMS.Columns[3].Width = 170;
                    GvSendSMS.Columns[4].Width = 500;
                }
                else
                    MessageBox.Show("Record Not Found.", "Warning");
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void cmbcustomername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void cmbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void txtMobile_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void txtBillNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
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
    }
}