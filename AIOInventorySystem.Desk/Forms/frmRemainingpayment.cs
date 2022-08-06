using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    public partial class frmRemainingpayment : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public frmRemainingpayment()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
           // FillVillage();
            cmbRouteLoad();
            cmbCustomerType.SelectedIndex = cmbCustomerType.FindStringExact("Retailer");
            getremainingpayment("Load");
        }
        public void cmbRouteLoad()
        {
            try
            {
                VillageRepository ACGRepo = new VillageRepository();
                List<tblVillageInfo> ACGdata = new List<tblVillageInfo>();
                ACGdata.Clear();
                ACGdata = ACGRepo.GetAll().OrderByDescending(t => t.VillageName).ToList();
                ACGdata.Add(new tblVillageInfo { VillageName = "Select", VillageId = 0 });
                ACGdata.Reverse();
                cmbVillage.DataSource = ACGdata;
                cmbVillage.ValueMember = "VillageId";
                cmbVillage.DisplayMember = "VillageName";
                ACGRepo.Dispose();
            }
            catch (Exception)
            {
            }
        }
        //public void FillVillage()
        //{
        //    try
        //    {
        //        CustomerRepository custRepo = new CustomerRepository();
        //        List<CustomerInformation> custData = new List<CustomerInformation>();
        //        custData.Clear();
        //        custData = custRepo.GetAll().Where(t => t.Village != null && t.Village != "" && t.CompId == CommonMethod.CompId).OrderByDescending(t => t.Village).GroupBy(t => t.Village).Select(t => t.FirstOrDefault()).ToList();
        //        custData.Add(new CustomerInformation { Village = "Select", CustomerID = 0 });
        //        custData.Reverse();
        //        cmbVillage.DataSource = custData;
        //        cmbVillage.ValueMember = "CustomerID";
        //        cmbVillage.DisplayMember = "Village";
        //        custRepo.Dispose();
        //        cmbVillage.SelectedIndex = 0;
        //    }
        //    catch (Exception)
        //    { }
        //}

        public void getremainingpayment(string flag)
        {
            try
            {
                decimal totAmount = 0, totalcount = 0;
                db.connect();
                //Stored Procedure code
                SqlCommand command = new SqlCommand("SPremainingpayment", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = flag;
                if (flag == "Village")
                {
                    //RouteRepository rRepo = new RouteRepository();
                    //string routeName = rRepo.GetAll().Where(t => t.RouteId == cmbVillage.SelectedValue).FirstOrDefault().RouteName;
                    //rRepo.Dispose();
                    command.Parameters.Add("@Village", SqlDbType.NVarChar).Value = cmbVillage.SelectedValue;
                }
                
                if (flag == "DateWise")
                {
                    CommonMethod com = new CommonMethod();
                    command.Parameters.Add("@FromDate", SqlDbType.NVarChar).Value = com.ValidFromDate(dtpfromdate.Value.Date);
                    command.Parameters.Add("@ToDate", SqlDbType.NVarChar).Value = com.ValidToDate(dtptodate.Value.Date);
                }
                if (flag == "CustomerType")
                    command.Parameters.Add("@CustomerType", SqlDbType.NVarChar).Value = cmbCustomerType.Text;
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                object sumtotAmount = new object();
                sumtotAmount = dt.Compute("Sum(TotalReaminingAmount)", "");
                GvRemainingpayment.DataSource = dt;
                db.CloseConnection();
                if (!string.IsNullOrEmpty(Convert.ToString(sumtotAmount)))
                    totAmount = Convert.ToDecimal(sumtotAmount);
                totalcount = dt.Rows.Count;
                GvRemainingpayment.Columns["CustomerID"].Visible = false;
                GvRemainingpayment.Columns["CustomerName"].Width = 250;
                GvRemainingpayment.Columns["CustomerAddress"].Width = 220;
                GvRemainingpayment.Columns["MobileNO"].Width = 140;
                GvRemainingpayment.Columns["TotalReaminingAmount"].Width = 150;
                if (!GvRemainingpayment.Columns.Contains("Send SMS"))
                {
                    DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
                    imageColumn.Name = "Send SMS";
                    imageColumn.HeaderText = "SendSMS";
                    imageColumn.Image = AIOInventorySystem.Desk.Properties.Resources.Send_SMS;
                    imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    imageColumn.Width = 70;
                    GvRemainingpayment.Columns.Add(imageColumn);
                }
                lblTotalMount.Text = Convert.ToString(totAmount);
                lblCustomerCount.Text = Convert.ToString(totalcount);
                dt.Dispose();
            }
            catch (Exception)
            { db.CloseConnection(); }

        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                string customertype = "", villagename = "", flag = "";
                if (chkCustomerType.Checked == true)
                    customertype = cmbCustomerType.Text;
                if (cmbVillage.Text != "Select")
                    villagename =Convert.ToString(cmbVillage.SelectedValue);
            a:
                {
                    string input = "";
                    input = Microsoft.VisualBasic.Interaction.InputBox("1 For Normal Print and 2 For Credit Day Wise Print and 3 For In Between Date Print,4 For Village", "Remaining Payment", "1", 400, 300);
                    if (input == "1")
                        flag = "Load";
                    else if (input == "2")
                        flag = "CreditList";
                    else if (input == "3")
                        flag = "DateWise";
                    else if (input == "4")
                        flag = "Village";

                    else
                        goto a;
                }
                RptRemainingPayment rempay = new RptRemainingPayment(flag, dtpfromdate.Value, dtptodate.Value, villagename, customertype);
                rempay.ShowDialog();
                rempay.Dispose();
                //if (cmbVillage.Text == "Select")
                //{
                //a:
                //    {
                //        if (chkCustomerType.Checked == true)
                //        {
                //            string customertype = cmbCustomerType.Text;
                //            RptRemainingPayment rempay = new RptRemainingPayment(customertype);
                //            rempay.ShowDialog();
                //            rempay.Dispose();
                //        }
                //        else
                //        {
                //            string input = "";
                //            input = Microsoft.VisualBasic.Interaction.InputBox("1 For Normal Print and 2 For Credit Day Wise Print and 3 For In Between Date Print", "Remaining Payment", "", 250, 250);
                //            if (input == "1" || input == "")
                //            {
                //                RptRemainingPayment rempay = new RptRemainingPayment("Load", "");
                //                rempay.ShowDialog();
                //                rempay.Dispose();
                //            }
                //            else if (input == "2")
                //            {
                //                RptRemainingPayment rempay = new RptRemainingPayment("CreditList", "");
                //                rempay.ShowDialog();
                //                rempay.Dispose();
                //            }
                //            else if (input == "3")
                //            {
                //                RptRemainingPayment rempay = new RptRemainingPayment("DateWise", dtpfromdate.Value, dtptodate.Value);
                //                rempay.ShowDialog();
                //                rempay.Dispose();
                //            }
                //            else
                //                goto a;
                //        }
                //    }
                //}
                //else
                //{
                //    RptRemainingPayment rempay = new RptRemainingPayment("Village", cmbVillage.Text);
                //    rempay.ShowDialog();
                //    rempay.Dispose();
                //}
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

        private void frmRemainingpayment_Load(object sender, EventArgs e)
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
                if (e.ColumnIndex == 0)
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
                            string mobileno = GvRemainingpayment.Rows[e.RowIndex].Cells[4].Value.ToString();
                            string smsText = "";
                            if (sData.MarathiSMS == "True")
                                smsText = "आपली उधारी रु. " + Convert.ToString(GvRemainingpayment.Rows[e.RowIndex].Cells[5].Value) + " आहे.कृपया जमा करावी. " + compData.CompanyName + " मोबाईल नं: " + compData.ContactNo + ".";
                            else
                                smsText = "This is just a friendly reminder that your bill payment of Rs " + Convert.ToString(GvRemainingpayment.Rows[e.RowIndex].Cells[5].Value) + " is overdue.Please pay your bill.Ragards: " + compData.CompanyName + " Mobile No: " + compData.ContactNo + ".";
                            int msgLenghth = smsText.Length;
                            if (msgLenghth < 160)
                            {
                                string MobNo = mobileno;
                                WebClient client = new WebClient();
                                string URL = "";
                                SMSRepository smsRepo = new SMSRepository();
                                SendSM smsData = new SendSM();
                                smsData.CompId = CommonMethod.CompId;
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
                                            //  return SmsStatusMsg;
                                            smsData.CustomerId = Convert.ToInt32(GvRemainingpayment.Rows[e.RowIndex].Cells[1].Value);
                                            smsData.CustomerName = Convert.ToString(GvRemainingpayment.Rows[e.RowIndex].Cells[2].Value);
                                            smsData.MobileNo = mobileno;
                                            smsData.SmsText = smsText;
                                            smsData.SmsDate = DateTime.Now.Date;
                                            URL = sData.SMSURLPath + sData.SMSUserId + "&pass=" + sData.SMSPassword + "&sender=" + sData.SMSKey + "&phone=" + MobNo + "&text=" + smsText + "&priority=ndnd&stype=normal";
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
                                            else if (result.Contains("Insufficient Credits") == true)
                                            {
                                                smsData.Status = "Insufficient Credits";
                                                smsRepo.Add(smsData);
                                                smsRepo.Save();
                                            }
                                            else
                                            {
                                                GvRemainingpayment.Rows[e.RowIndex].Cells[0].Value = AIOInventorySystem.Desk.Properties.Resources.Succesfully_Send;
                                                smsData.Status = "Remaining Payment SMS";
                                                smsRepo.Add(smsData);
                                                smsRepo.Save();
                                                MessageBox.Show("SMS Send Successfully.", "Success");
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
                else if (e.ColumnIndex == 4)
                {
                    try
                    {
                        frmcustomer expmast = new frmcustomer(Convert.ToInt32(GvRemainingpayment.Rows[e.RowIndex].Cells["CustomerID"].Value));
                        expmast.ShowDialog();
                       // FillVillage();
                        cmbRouteLoad();
                        getremainingpayment("Load");
                        expmast.Dispose();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("error");
                    }
                }
                else if (e.ColumnIndex == 2)
                {
                    frmCustomerLedger custdata = new frmCustomerLedger(Convert.ToInt32(GvRemainingpayment.Rows[e.RowIndex].Cells["CustomerID"].Value));
                    custdata.ShowDialog();
                }
            }
            catch (Exception)
            { }
        }

        private void cmbVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbVillage.Text == "Select")
                    getremainingpayment("Load");
                else
                    getremainingpayment("Village");
            }
            catch (Exception)
            { }
        }

        private void btncreditlist_Click(object sender, EventArgs e)
        {
            try
            {
                getremainingpayment("CreditList");
            }
            catch (Exception)
            { }
        }

        private void btngetall_Click(object sender, EventArgs e)
        {
            try
            {
                chkCustomerType.Checked = false;
                getremainingpayment("Load");
            }
            catch (Exception)
            { }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                GvRemainingpayment.DataSource = null;
                if (chkCustomerType.Checked == true)
                {
                    cmbVillage.Text = "Select";
                    getremainingpayment("CustomerType");
                }
                else
                    getremainingpayment("DateWise");
            }
            catch (Exception)
            { }
        }
    }
}