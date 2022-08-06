using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmSendFestivalMessage : Form
    {
        List<int> rowIndexStatus = new List<int>();
        DbClass db = new DbClass();
        public int customerCount;

        public frmSendFestivalMessage()
        {
            InitializeComponent();
            BindData();
            FillCombo();
            CountCustomes();
            UpdateStatus();
            progressBar1.Visible = false;
        }

        public void UpdateStatus()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Update Customerinformation Set MessageStatus=0 where CompId='" + CommonMethod.CompId + "'", db.Connection);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            { }
        }

        public void FillCombo()
        {
            try
            {
                CustomerRepository custRepo = new CustomerRepository();
                List<CustomerInformation> custData = new List<CustomerInformation>();
                custData.Clear();
                custData = custRepo.GetAll().Where(t => t.ContactNo != Convert.ToString(' ') && t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CustomerName).ToList();
                custData.Add(new CustomerInformation { CustomerName = "", CustomerID = 0 });
                custData.Reverse();
                cmbCustomerName.DataSource = custData;
                cmbCustomerName.ValueMember = "CustomerID";
                cmbCustomerName.DisplayMember = "CustomerName";
                custRepo.Dispose();
                cmbCustomerName.SelectedIndex = 0;
                FestivalRepository festRepo = new FestivalRepository();
                List<Festival> festData = new List<Festival>();
                festData.Clear();
                festData = festRepo.GetAll().OrderByDescending(t => t.FestivalName).ToList();
                festData.Add(new Festival { FestivalName = "", FestivalNo = 0 });
                festData.Reverse();
                cmbFestival.DataSource = festData;
                cmbFestival.ValueMember = "FestivalNo";
                cmbFestival.DisplayMember = "FestivalName";
                festRepo.Dispose();
                cmbFestival.SelectedIndex = 0;
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

        public void CountCustomes()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT Count(Id) FROM dbo.CustomerInformation Where  Mobileno!='' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblCustomerCount.Text = dt.Rows[0][0].ToString();
                    customerCount = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        public void BindData()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT Id,CustomerID, Mobileno,CustomerName,CustomeAddress FROM dbo.CustomerInformation Where  Mobileno!='' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvCustomer.DataSource = dt;
                GvCustomer.Refresh();
                GvCustomer.Columns[1].Visible = false;
                GvCustomer.Columns[2].Visible = false;
                GvCustomer.Columns[3].Width = 80;
                GvCustomer.Columns[4].Width = 130;
                GvCustomer.Columns[5].Width = 160;
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void cmbFestival_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT FestivalName, FestivalsMessage FROM dbo.FestivalTemplates Where FestivalId='" + Convert.ToInt32(cmbFestival.SelectedValue) + "'", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvSMSReport.DataSource = dt;
                GvSMSReport.Refresh();
                GvSMSReport.Columns[1].Width = 500;
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void cmbCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CustomerRepository custRepo = new CustomerRepository();
                txtMobileNo.Text = custRepo.GetAll().Where(t => t.CustomerID == Convert.ToInt32(cmbCustomerName.SelectedValue) && t.CompId == CommonMethod.CompId).FirstOrDefault().Mobileno;
                custRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void GvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != 0)
                {
                    CustomerRepository custRepo = new CustomerRepository();
                    txtMobileNo.Text = Convert.ToString(GvCustomer.Rows[e.RowIndex].Cells[3].Value);
                    rowIndexStatus.Add(e.RowIndex);
                    custRepo.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void GvSMSReport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMessage.Text = Convert.ToString(GvSMSReport.Rows[e.RowIndex].Cells[1].Value);
                txtCount.Text = Convert.ToString(txtMessage.Text.Length);
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                    this.Close();
            }
            catch (Exception)
            { }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            BindData();
            GvSMSReport.DataSource = null;
            txtMobileNo.Text = "";
            FillCombo();
            rowIndexStatus = new List<int>();
            chkSelect.Checked = false;
            txtCount.Text = "";
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                bool connection = NetworkInterface.GetIsNetworkAvailable();
                if (connection == true)
                {
                    string findChar = txtMessage.Text;
                    if (!findChar.Contains('&'))
                    {
                        SettingRepository sRepo = new SettingRepository();
                        Setting sData = sRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                        CompanyInformationRepository compRepo = new CompanyInformationRepository();
                        CompanyInformation compData = compRepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();
                        CustomerRepository custRepo = new CustomerRepository();
                        string mobileno = null;
                        try
                        {
                            mobileno = custRepo.GetAll().Where(t => t.Mobileno == txtMobileNo.Text && t.CompId == CommonMethod.CompId).FirstOrDefault().Mobileno;
                        }
                        catch (Exception)
                        { }

                        WebClient client = new WebClient();
                        string URL = "";
                        SMSRepository smsRepo = new SMSRepository();
                        SendSM smsData = new SendSM();
                        smsData.CompId = CommonMethod.CompId;
                        if (txtMessage.Text != "")
                        {
                            if (txtMobileNo.Text != "")
                            {
                                try
                                {
                                    int charCount = txtMessage.Text.Length;
                                    if (charCount < 160)
                                    {
                                        if (!string.IsNullOrEmpty(mobileno))
                                        {
                                            if (System.Text.RegularExpressions.Regex.IsMatch(txtMobileNo.Text, "[^0-9]"))
                                            {
                                                smsData.Status = "Mobile not valid For Single Customer Festival SMS";
                                                smsRepo.Add(smsData);
                                                smsRepo.Save();
                                            }
                                            else if (txtMobileNo.Text.Length == 10)
                                            {
                                                string SmsStatusMsg = string.Empty;
                                                if (cmbCustomerName.Text != "")
                                                {
                                                    smsData.CustomerId = Convert.ToInt32(cmbCustomerName.SelectedValue);
                                                    smsData.CustomerName = cmbCustomerName.Text;
                                                }
                                                else
                                                {
                                                    try
                                                    {
                                                        smsData.CustomerId = Convert.ToInt32(GvCustomer.Rows[rowIndexStatus[0]].Cells[2].Value);
                                                        smsData.CustomerName = Convert.ToString(GvCustomer.Rows[rowIndexStatus[0]].Cells[4].Value);
                                                    }
                                                    catch (Exception)
                                                    { }
                                                }
                                                smsData.MobileNo = txtMobileNo.Text;
                                                smsData.SmsText = txtMessage.Text;
                                                smsData.SmsDate = DateTime.Now.Date;
                                                string resultString = Regex.Replace(txtMessage.Text, @"^\s*$\n|\r", "", RegexOptions.Multiline).TrimEnd(); ;
                                                URL = sData.SMSURLPath + sData.SMSUserId + "&pass=" + sData.SMSPassword + "&sender=" + sData.SMSKey + "&phone=" + txtMobileNo.Text + "&text=" + txtMessage.Text + "&priority=ndnd&stype=normal";
                                                string result = apicall(URL);
                                                if (result.Contains("Username/Password Incorrect") == true)
                                                {
                                                    smsData.Status = "Wrong User Name or Password For Single Festival SMS";
                                                    smsRepo.Add(smsData);
                                                    smsRepo.Save();
                                                }
                                                else if (result.Contains("Sender ID Does not Exist") == true)
                                                {
                                                    smsData.Status = "Sender ID Does not Exist For Single Festival SMS";
                                                    smsRepo.Add(smsData);
                                                    smsRepo.Save();
                                                }
                                                else
                                                {
                                                    smsData.Status = "For Single Customer Festival SMS";
                                                    smsRepo.Add(smsData);
                                                    smsRepo.Save();
                                                    MessageBox.Show("SMS Send Successfully.", "Success");
                                                }
                                            }
                                            else
                                            {
                                                smsData.Status = "Mobile  not valid For Single Customer Festival SMS";
                                                smsRepo.Add(smsData);
                                                smsRepo.Save();
                                            }
                                        }
                                        else
                                        {
                                            URL = "http://bhashsms.com/api/sendmsg.php?user=" + sData.SMSUserId + "&pass=" + sData.SMSPassword + "&sender=" + sData.SMSKey + "&phone=" + txtMobileNo.Text + "&text=" + txtMessage.Text + "&priority=ndnd&stype=normal";
                                            string result = apicall(URL);
                                            if (result.Contains("Username/Password Incorrect") == true)
                                            {
                                                smsData.Status = "Wrong User Name or Password For new customer  Festival SMS";
                                                smsRepo.Add(smsData);
                                                smsRepo.Save();
                                            }
                                            else if (result.Contains("Sender ID Does not Exist") == true)
                                            {
                                                smsData.Status = "Sender ID Does not Exist For new customer Festival SMS";
                                                smsRepo.Add(smsData);
                                                smsRepo.Save();
                                            }
                                            else
                                            {
                                                smsData.MobileNo = txtMobileNo.Text;
                                                smsData.SmsText = txtMessage.Text;
                                                smsData.SmsDate = DateTime.Now.Date;
                                                smsData.Status = "For New Customer Festival SMS";
                                                smsRepo.Add(smsData);
                                                smsRepo.Save();
                                                MessageBox.Show("SMS Send Successfully.", "Success");
                                            }
                                        }
                                    }
                                    else
                                        MessageBox.Show("SMS lenghth is over.", "Warning");
                                }
                                catch (Exception)
                                {
                                    smsData.Status = "For Single Customer Festival message error...Only 160 charactre!!";
                                    smsRepo.Add(smsData);
                                    smsRepo.Save();
                                }
                            }
                            else if (chkSelect.Checked == true)
                            {
                                try
                                {
                                    int charCount = txtMessage.Text.Length;
                                    if (charCount < 160)
                                        SendSMS();
                                    else
                                        MessageBox.Show("SMS lenghth is over. Only 160 charactre.", "Warning");
                                }
                                catch (Exception)
                                {
                                    smsData.Status = "For Multiple Customer Festival message error!!";
                                    smsRepo.Add(smsData);
                                    smsRepo.Save();
                                }
                            }
                            else if (rowIndexStatus.Count > 0)
                            {
                                try
                                {
                                    int charCount = txtMessage.Text.Length;
                                    if (charCount < 160)
                                    {
                                        string mobileNo = "";
                                        for (int i = 0; i < rowIndexStatus.Count; i++)
                                        {
                                            try
                                            {
                                                if (System.Text.RegularExpressions.Regex.IsMatch(Convert.ToString(GvCustomer.Rows[rowIndexStatus[i]].Cells[3].Value), "[^0-9]"))
                                                {
                                                    smsData.Status = "Mobile not valid For Multiple Selected Customer Festival SMS";
                                                    smsRepo.Add(smsData);
                                                    smsRepo.Save();
                                                }
                                                else if (Convert.ToString(GvCustomer.Rows[rowIndexStatus[i]].Cells[3].Value).Length == 10)
                                                {
                                                    mobileNo += Convert.ToString(GvCustomer.Rows[rowIndexStatus[i]].Cells[3].Value) + ",";
                                                    string SmsStatusMsg = string.Empty;
                                                    smsData.CustomerId = Convert.ToInt32(GvCustomer.Rows[rowIndexStatus[i]].Cells[2].Value);
                                                    smsData.CustomerName = Convert.ToString(GvCustomer.Rows[rowIndexStatus[i]].Cells[4].Value);
                                                    smsData.MobileNo = Convert.ToString(GvCustomer.Rows[rowIndexStatus[i]].Cells[3].Value);
                                                    smsData.SmsText = txtMessage.Text;
                                                    smsData.SmsDate = DateTime.Now.Date;

                                                }
                                                else
                                                {
                                                    smsData.Status = "Mobile  not valid For Multiple Selected Customer Festival SMS";
                                                    smsRepo.Add(smsData);
                                                    smsRepo.Save();
                                                }
                                            }
                                            catch (Exception)
                                            { }
                                        }
                                        int totalLenghth = mobileNo.Length;
                                        if (totalLenghth > 0)
                                        {
                                            string mobile1 = mobileNo.Remove(totalLenghth - 1);
                                            URL = sData.SMSURLPath + sData.SMSUserId + "&pass=" + sData.SMSPassword + "&sender=" + sData.SMSKey + "&phone=" + mobile1 + "&text=" + txtMessage.Text + "&priority=ndnd&stype=normal";
                                            string result = apicall(URL);
                                            if (result.Contains("Username/Password Incorrect") == true)
                                            {
                                                smsData.Status = "Username/Password Incorrect For Multiple Selected Customer Festival SMS";
                                                smsRepo.Add(smsData);
                                                smsRepo.Save();
                                            }
                                            else if (result.Contains("Sender ID Does not Exist") == true)
                                            {
                                                smsData.Status = "Sender ID Does not Exist For Multiple Selected Customer Festival SMS";
                                                smsRepo.Add(smsData);
                                                smsRepo.Save();
                                            }
                                            else
                                            {
                                                smsData.Status = "For Multiple Selected Customer Festival SMS";
                                                smsRepo.Add(smsData);
                                                smsRepo.Save();
                                                MessageBox.Show("SMS Send Successfully.", "Success");
                                            }
                                        }
                                        else
                                            MessageBox.Show("Please select at least one valid mobile no.", "Warning");
                                    }
                                    else
                                        MessageBox.Show("SMS lenghth is over...Only 160 charactre.", "Warning");
                                }
                                catch (Exception)
                                {
                                    smsData.Status = "For Multiple Customer Festival message error!!";
                                    smsRepo.Add(smsData);
                                    smsRepo.Save();
                                }
                            }
                            BindData();
                            GvSMSReport.DataSource = null;
                            txtMobileNo.Text = "";
                            txtMessage.Text = "";
                            FillCombo();
                            chkSelect.Checked = false;
                            rowIndexStatus = new List<int>();
                        }
                        else
                            MessageBox.Show("Please select message.", "Warning");
                        sRepo.Dispose();
                        compRepo.Dispose();
                        custRepo.Dispose();
                        smsRepo.Dispose();
                    }
                    else
                        MessageBox.Show("Please remove '&' in the message");
                }
                else
                    MessageBox.Show("Internet connection in not available");
            }
            catch (Exception)
            { }
        }

        public void SendSMS()
        {
            try
            {
                string URL = "";
                int lastCustomerID = 0;
                int divideCusomerCount = customerCount / 15;
                SettingRepository sRepo = new SettingRepository();
                Setting sData = sRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                CompanyInformationRepository compRepo = new CompanyInformationRepository();
                CompanyInformation compData = compRepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();
                CustomerRepository custRepo = new CustomerRepository();
                SMSRepository smsRepo = new SMSRepository();
                SendSM smsData = new SendSM();
                smsData.CompId = CommonMethod.CompId;
                int q = 1;
                progressBar1.Minimum = q;
                progressBar1.Maximum = divideCusomerCount;
                progressBar1.Visible = true;

                for (int i = 0; i < divideCusomerCount; i++)
                {
                    progressBar1.Value = q;
                    if (i == 0)
                    {
                        SqlDataAdapter da = new SqlDataAdapter(" select Top 15 Id,CustomerID, LTRIM(Mobileno),CustomerName,CustomeAddress from CustomerInformation  Where  Mobileno!='' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            string mobileNo = "";
                            for (int k = 0; k < dt.Rows.Count; k++)
                            {
                                if (k == 14)
                                    lastCustomerID = Convert.ToInt32(dt.Rows[k][0]);
                                try
                                {
                                    if (System.Text.RegularExpressions.Regex.IsMatch(Convert.ToString(dt.Rows[k][2]), "[^0-9]"))
                                    {
                                        smsData.Status = "Mobile not valid For Multiple Customer Festival SMS";
                                        smsRepo.Add(smsData);
                                        smsRepo.Save();
                                    }
                                    else if (Convert.ToString(dt.Rows[k][2]).Length == 10)
                                    {
                                        mobileNo += Convert.ToString(dt.Rows[k][2]) + ",";
                                        string SmsStatusMsg = string.Empty;
                                        smsData.CustomerId = Convert.ToInt32(dt.Rows[k][1]);
                                        smsData.CustomerName = Convert.ToString(dt.Rows[k][3]);
                                        smsData.MobileNo = Convert.ToString(dt.Rows[k][2]);
                                        smsData.SmsText = txtMessage.Text;
                                        smsData.SmsDate = DateTime.Now.Date;
                                    }
                                    else
                                    {
                                        smsData.Status = "Mobile not valid For Multiple Customer Festival SMS";
                                        smsRepo.Add(smsData);
                                        smsRepo.Save();
                                    }
                                }
                                catch (Exception)
                                { }
                            }
                            int totalLenghth = mobileNo.Length;
                            if (totalLenghth > 0)
                            {
                                string mobile1 = mobileNo.Remove(totalLenghth - 1);
                                URL = sData.SMSURLPath + sData.SMSUserId + "&pass=" + sData.SMSPassword + "&sender=" + sData.SMSKey + "&phone=" + mobile1 + "&text=" + txtMessage.Text + "&priority=ndnd&stype=normal";
                                string result = apicall(URL);
                                if (result.Contains("Username/Password Incorrect") == true)
                                {
                                    smsData.Status = "Username/Password Incorrect For Multiple Selected Customer Festival SMS";
                                    smsRepo.Add(smsData);
                                    smsRepo.Save();
                                }
                                else if (result.Contains("Sender ID Does not Exist") == true)
                                {
                                    smsData.Status = "Sender ID Does not Exist For Multiple Selected Customer Festival SMS";
                                    smsRepo.Add(smsData);
                                    smsRepo.Save();
                                }
                                else
                                {
                                    smsData.Status = "For Multiple Selected Customer Festival SMS";
                                    smsRepo.Add(smsData);
                                    smsRepo.Save();
                                    MessageBox.Show("SMS Send Successfully.", "Success");
                                }
                            }
                            else
                                MessageBox.Show("Please select at least one valid mobile no.", "Warning");
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                    {
                        SqlDataAdapter da = new SqlDataAdapter("Select Top 15 Id,CustomerID, LTRIM(Mobileno),CustomerName,CustomeAddress from CustomerInformation  Where  Mobileno!='' AND  ID>" + lastCustomerID + " and CompId='" + CommonMethod.CompId + "'", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            string mobileNo = "";
                            for (int k = 0; k < dt.Rows.Count; k++)
                            {
                                if (k == 14)
                                    lastCustomerID = Convert.ToInt32(dt.Rows[k][0]);
                                try
                                {
                                    if (System.Text.RegularExpressions.Regex.IsMatch(Convert.ToString(dt.Rows[k][2]), "[^0-9]"))
                                    {
                                        smsData.Status = "Mobile not valid For Multiple Customer Festival SMS";
                                        smsRepo.Add(smsData);
                                        smsRepo.Save();
                                    }
                                    else if (Convert.ToString(dt.Rows[k][2]).Length == 10)
                                    {
                                        mobileNo += Convert.ToString(dt.Rows[k][2]) + ",";
                                        string SmsStatusMsg = string.Empty;
                                        smsData.CustomerId = Convert.ToInt32(dt.Rows[k][1]);
                                        smsData.CustomerName = Convert.ToString(dt.Rows[k][3]);
                                        smsData.MobileNo = Convert.ToString(dt.Rows[k][2]);
                                        smsData.SmsText = txtMessage.Text;
                                        smsData.SmsDate = DateTime.Now.Date;
                                    }
                                    else
                                    {
                                        smsData.Status = "Mobile  not valid For Multiple Customer Festival SMS";
                                        smsRepo.Add(smsData);
                                        smsRepo.Save();
                                    }
                                }
                                catch (Exception)
                                { }
                            }
                            int totalLenghth = mobileNo.Length;
                            if (totalLenghth > 0)
                            {
                                string mobile1 = mobileNo.Remove(totalLenghth - 1);
                                URL = sData.SMSURLPath + sData.SMSUserId + "&pass=" + sData.SMSPassword + "&sender=" + sData.SMSKey + "&phone=" + mobile1 + "&text=" + txtMessage.Text + "&priority=ndnd&stype=normal";
                                string result = apicall(URL);
                                if (result.Contains("Username/Password Incorrect") == true)
                                {
                                    smsData.Status = "Username/Password Incorrect For Multiple Selected Customer Festival SMS";
                                    smsRepo.Add(smsData);
                                    smsRepo.Save();
                                }
                                else if (result.Contains("Sender ID Does not Exist") == true)
                                {
                                    smsData.Status = "Sender ID Does not Exist For Multiple Selected Customer Festival SMS";
                                    smsRepo.Add(smsData);
                                    smsRepo.Save();
                                }
                                else
                                {
                                    smsData.Status = "For Multiple Selected Customer Festival SMS";
                                    smsRepo.Add(smsData);
                                    smsRepo.Save();
                                    MessageBox.Show("SMS Send Successfully.", "Success");
                                }
                            }
                            else
                                MessageBox.Show("Please select at least one valid mobile no.", "Warning");
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                    q++;
                }
                progressBar1.Visible = false;
                sRepo.Dispose();
                compRepo.Dispose();
                custRepo.Dispose();
                smsRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public string apicall(string url)
        {
            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                StreamReader sr = new StreamReader(httpres.GetResponseStream());
                string results = sr.ReadToEnd();
                sr.Close();
                return results;
            }
            catch
            {
                return "0";
            }
        }

        private void GvCustomer_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((sender as DataGridView).CurrentCell is DataGridViewCheckBoxCell)
                {
                    if (Convert.ToBoolean(((sender as DataGridView).CurrentCell as DataGridViewCheckBoxCell).Value))
                    {
                        rowIndexStatus.Add(e.RowIndex);
                        txtMobileNo.Text = "";
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSelect.Checked == true)
                {
                    foreach (DataGridViewRow row in GvCustomer.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        if (chk.Value != null)
                        {
                            if ((bool)chk.Value == true)
                            { }
                            else
                                chk.Value = true;
                        }
                        else
                            chk.Value = true;
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in GvCustomer.Rows)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                        if (chk.Value == null)
                        {
                            if ((bool)chk.Value == true)
                                chk.Value = false;
                        }
                        else
                            chk.Value = false;
                    }
                }
                txtMobileNo.Text = "";
            }
            catch (Exception)
            { }
        }

        public void CountSuppliers()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT Count(Id) FROM dbo.SupplierInformation Where  ContactNo!='' ", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblCustomerCount.Text = dt.Rows[0][0].ToString();
                    customerCount = Convert.ToInt32(dt.Rows[0][0].ToString());
                }
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        public void BindSuppliers()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT Id,SupplierCode, ContactNo,SupplierName,SupplierAddress FROM dbo.SupplierInformation Where  ContactNo!='' ", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvCustomer.DataSource = dt;
                GvCustomer.Refresh();
                GvCustomer.Columns[1].Visible = false;
                GvCustomer.Columns[2].Visible = false;
                GvCustomer.Columns[3].Width = 80;
                GvCustomer.Columns[4].Width = 130;
                GvCustomer.Columns[5].Width = 160;
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnSuppliers_Click(object sender, EventArgs e)
        {
            try
            {
                BindSuppliers();
                CountSuppliers();
            }
            catch (Exception)
            { }
        }

        private void frmSendFestivalMessage_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void cmbCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtMobileNo;
            }
            catch (Exception) { }
        }

        private void txtMobileNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSend;
            }
            catch (Exception) { }
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            try
            {
                CountCustomes();
                BindData();
            }
            catch (Exception)
            { }
        }
    }
}