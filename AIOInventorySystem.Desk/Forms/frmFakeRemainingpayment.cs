using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmFakeRemainingpayment : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public frmFakeRemainingpayment()
        {
            InitializeComponent();
            getremainingpayment();
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
                GvRemainingpayment.Columns[5].Visible = false;
        }

        public void getremainingpayment()
        {
            try
            {
                decimal totAmount = 0, totalcount = 0;
                FakeRemainingPaymentRepository remainingpayrepo = new FakeRemainingPaymentRepository();
                CustomerRepository custrepo = new CustomerRepository();
                List<FakeRemainingPayment> remainingpay = new List<FakeRemainingPayment>();
                DataTable dt = db.GetTable("Select Distinct(CustomerId) from FakeRemainingPayment WHERE TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                if (dt.Rows.Count > 0)
                {
                    int j = 0;
                    GvRemainingpayment.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int custid = Convert.ToInt32(dt.Rows[i][0].ToString());
                        var customerdata = custrepo.GetAll().Where(t => t.CustomerID == custid && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        List<FakeRemainingPayment> remainingamt = remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        decimal netamt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && (t.Type != "Receipt") && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmt));
                        decimal Paidamt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt));
                        string remainigamt = Convert.ToString(netamt - Paidamt);
                        if (Convert.ToDecimal(remainigamt) < -1)
                        { }
                        else
                            totAmount = totAmount + Convert.ToDecimal(remainigamt);
                        if (remainigamt != "0.00")
                        {
                            GvRemainingpayment.Rows.Add();
                            GvRemainingpayment.Rows[j].Cells[0].Value = customerdata.CustomerName;
                            GvRemainingpayment.Rows[j].Cells[1].Value = customerdata.CustomeAddress;
                            GvRemainingpayment.Rows[j].Cells[2].Value = customerdata.Mobileno;
                            GvRemainingpayment.Rows[j].Cells[3].Value = remainigamt;
                            GvRemainingpayment.Rows[j].Cells[4].Value = customerdata.CustomerID;
                            GvRemainingpayment.Rows[j].Cells[5].Value = AIOInventorySystem.Desk.Properties.Resources.Send_SMS;
                            j++;
                            totalcount = j;
                        }
                        netamt = 0; Paidamt = 0; remainigamt = "0";
                    }
                }
                lblTotalMount.Text = Convert.ToString(totAmount);
                lblCustomerCount.Text = Convert.ToString(totalcount);
                remainingpayrepo.Dispose();
                custrepo.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                RptFakeRemainingPayment rempay = new RptFakeRemainingPayment();
                rempay.ShowDialog();
                rempay.Dispose();
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

        private void frmFakeRemainingpayment_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void GvRemainingpayment_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5)
                {
                    bool connection = NetworkInterface.GetIsNetworkAvailable();
                    if (connection == true)
                    {
                        SettingRepository sRepo = new SettingRepository();
                        Setting sData = sRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (sData.SendSMS == "True")
                        {
                            CompanyInformationRepository compRepo = new CompanyInformationRepository();
                            CompanyInformation compData = compRepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();
                            string mobileno = GvRemainingpayment.Rows[e.RowIndex].Cells[2].Value.ToString();
                            string smsText = "This is just a friendly reminder that your bill payment of Rs " + Convert.ToString(GvRemainingpayment.Rows[e.RowIndex].Cells[3].Value) + " is overdue.Please pay your bill.Ragards: " + compData.CompanyName + " Mobile No: " + compData.ContactNo + ".";
                            int msgLenghth = smsText.Length;
                            if (msgLenghth < 160)
                            {
                                string MobNo = mobileno;
                                WebClient client = new WebClient();
                                string URL = "";
                                SMSRepository smsRepo = new SMSRepository();
                                SendSM smsData = new SendSM();
                                try
                                {
                                    if (!string.IsNullOrEmpty(MobNo.Trim()))
                                    {
                                        if (System.Text.RegularExpressions.Regex.IsMatch(MobNo.Trim(), "[^0-9]"))
                                        {
                                            MessageBox.Show("Please add valid mobile no.", "Warning");
                                            smsData.Status = "Mobile not valid For for Remaining Payment SMS";
                                            smsRepo.Add(smsData);
                                            smsRepo.Save();
                                        }
                                        else if (MobNo.Trim().Length == 10)
                                        {
                                            string SmsStatusMsg = string.Empty;
                                            smsData.CustomerId = Convert.ToInt32(GvRemainingpayment.Rows[e.RowIndex].Cells[4].Value);
                                            smsData.CustomerName = Convert.ToString(GvRemainingpayment.Rows[e.RowIndex].Cells[0].Value);
                                            smsData.MobileNo = mobileno;
                                            smsData.SmsText = smsText;
                                            smsData.SmsDate = DateTime.Now.Date;
                                            URL = "http://bhashsms.com/api/sendmsg.php?user=" + sData.SMSUserId + "&pass=" + sData.SMSPassword + "&sender=" + sData.SMSKey + "&phone=" + MobNo + "&text=" + smsText + "&priority=ndnd&stype=normal";
                                            string result = apicall(URL);
                                            if (result.Contains("Username/Password Incorrect") == true)
                                            {
                                                smsData.Status = "Wrong User Name or Password For Mannual SMS";
                                                smsRepo.Add(smsData);
                                                smsRepo.Save();
                                            }
                                            else if (result.Contains("Sender ID Does not Exist") == true)
                                            {
                                                smsData.Status = "Sender ID Does not Exist For Mannual SMS";
                                                smsRepo.Add(smsData);
                                                smsRepo.Save();
                                            }
                                            else
                                            {
                                                GvRemainingpayment.Rows[e.RowIndex].Cells[5].Value = AIOInventorySystem.Desk.Properties.Resources.Succesfully_Send;
                                                smsData.Status = "Remaining Payment SMS";
                                                smsRepo.Add(smsData);
                                                smsRepo.Save();
                                                MessageBox.Show("SMS Send Successfully.","Success");
                                            }
                                        }
                                        else
                                        {
                                            smsData.Status = "Mobile no not valid for Remaining Payment SMS";
                                            smsRepo.Add(smsData);
                                            smsRepo.Save();
                                        }
                                    }
                                    else
                                    {
                                        smsData.Status = "Mobile dose not exsist For Remaining Payment SMS";
                                        smsRepo.Add(smsData);
                                        smsRepo.Save();
                                    }
                                }
                                catch (Exception)
                                {
                                    smsData.Status = "Send Option is Auto but customer dose not have mobile no!!";
                                    smsRepo.Add(smsData);
                                    smsRepo.Save();
                                }
                                smsRepo.Dispose();
                            }
                            else
                                MessageBox.Show("Message lenghth over!!");
                            compRepo.Dispose();
                        }
                        sRepo.Dispose();
                    }
                    else
                        MessageBox.Show("Internet connection in not available");
                }
            }
            catch (Exception)
            { }
        }
    }
}