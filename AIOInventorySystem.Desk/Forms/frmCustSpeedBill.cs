using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmCustSpeedBill : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        public int intflag = 0, intrdoff = 1, intTempCustNo, billtype = 1, BillMasterID;
        public decimal fnetamt = 0, dectotAmt = 0, dectotSGST = 0, dectotCGST = 0, dectotIGST = 0;
        public string billid;

        public frmCustSpeedBill()
        {
            InitializeComponent();
        }

        private void frmCustSpeedBill_Load(object sender, EventArgs e)
        {
            try
            {
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                intTempCustNo = tbcntCust1.SelectedIndex + 1;
                dtpbilldate.GotFocus += new EventHandler(dtpbilldate_GotFocus);
                dtpbilldate.LostFocus += new EventHandler(dtpbilldate_LostFocus);
                cmbcustomername.GotFocus += new EventHandler(cmbcustomername_GotFocus);
                cmbcustomername.LostFocus += new EventHandler(cmbcustomername_LostFocus);
                txtNarration.GotFocus += new EventHandler(txtNarration_GotFocus);
                txtNarration.LostFocus += new EventHandler(txtNarration_LostFocus);
                txtDiscount.GotFocus += new EventHandler(txtDiscount_GotFocus);
                txtDiscount.LostFocus += new EventHandler(txtDiscount_LostFocus);
                txtDiscAmt.GotFocus += new EventHandler(txtDiscAmt_GotFocus);
                txtDiscAmt.LostFocus += new EventHandler(txtDiscAmt_LostFocus);
                txtotherchrgs.GotFocus += new EventHandler(txtotherchrgs_GotFocus);
                txtotherchrgs.LostFocus += new EventHandler(txtotherchrgs_LostFocus);
                txtPaidAmt.GotFocus += new EventHandler(txtPaidAmt_GotFocus);
                txtPaidAmt.LostFocus += new EventHandler(txtPaidAmt_LostFocus);
            }
            catch (Exception)
            { }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            try
            {
                pnlMasterRecord.Visible = false;
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
            else if (keyData == Keys.F12)//Save Dialogue Open
            {
                if (intflag == 0)
                {
                    pnlMasterRecord.Visible = true;
                    intflag = 1;
                    maxbillidtype(1);
                    customerdata();
                    this.ActiveControl = cmbcustomername;
                    txtTotalAmt.Text = lblNetAmt1.Text;
                    lbltotSGST.Text = Convert.ToString(Math.Round(dectotSGST, 2, MidpointRounding.AwayFromZero));
                    lbltotCGST.Text = Convert.ToString(Math.Round(dectotCGST, 2, MidpointRounding.AwayFromZero));
                    lbltotIGST.Text = Convert.ToString(Math.Round(dectotIGST, 2, MidpointRounding.AwayFromZero));
                }
                else
                {
                    pnlMasterRecord.Visible = false;
                    intflag = 0;
                }
            }
            else if (keyData == Keys.F2)//Focus on ItemName
            {
                if (tbcntCust1.SelectedIndex == 0)
                {
                    int rowi = dtgvItemDetails1.RowCount - 1;
                    dtgvItemDetails1.CurrentCell = dtgvItemDetails1.Rows[rowi].Cells[1];
                    this.ActiveControl = dtgvItemDetails1;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void customerdata()
        {
            try
            {
                CustomerRepository custrepo = new CustomerRepository();
                List<CustomerInformation> customerdata = new List<CustomerInformation>();
                customerdata.Clear();
                customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).ToList();
                customerdata.Add(new CustomerInformation { CustomerName = "Select", CustomerID = 0 });
                customerdata.Reverse();
                cmbcustomername.DataSource = customerdata;
                cmbcustomername.ValueMember = "CustomerID";
                cmbcustomername.DisplayMember = "CustomerName";
                custrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void CalculateNetAmt()
        {
            decimal d, o, total, damt = 0, netamt;
            total = Convert.ToDecimal(txtTotalAmt.Text);
            if (txtDiscount.Text != "")
            {
                d = Convert.ToDecimal(txtDiscount.Text);
                damt = (total * d) / 100;
                txtDiscAmt.Text = Convert.ToString(Math.Round(damt, 2, MidpointRounding.AwayFromZero));
            }
            if (txtotherchrgs.Text != "")
                o = Convert.ToDecimal(txtotherchrgs.Text);
            else
                o = 0;
            netamt = total - Math.Round(damt, 2, MidpointRounding.AwayFromZero) + o;
            lblNetAmt.Text = Convert.ToString(Math.Round(netamt, 2, MidpointRounding.AwayFromZero));
            if (intrdoff == 1)
                RoundOff(netamt);
        }

        public void RoundOff(decimal netamt)
        {
            try
            {
                decimal b = Math.Round(netamt);
                decimal c = 0;
                if (netamt > b)
                {
                    c = netamt - b;
                    lblroundoff.Text = "-" + c.ToString();
                }
                else
                {
                    c = b - netamt;
                    lblroundoff.Text = c.ToString();
                }
                fnetamt = Convert.ToDecimal(netamt) + Convert.ToDecimal(lblroundoff.Text);
                lblFinalNetAmt.Text = Convert.ToString(Math.Round(fnetamt, 2, MidpointRounding.AwayFromZero));
                txtPaidAmt.Text = Convert.ToString(Math.Round(fnetamt, 2, MidpointRounding.AwayFromZero));
            }
            catch (Exception)
            { }
        }

        private void txtTotalAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateNetAmt();
            }
            catch (Exception)
            { }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(txtDiscount.Text) <= 100)
                {
                    CalculateNetAmt();
                }
                else
                {
                    MessageBox.Show("Enter Discount Between 0 to 100 %.", "Warning");
                    txtDiscount.Text = "0";
                    this.ActiveControl = txtDiscount;
                }
            }
            catch (Exception)
            { }
        }

        private void txtDiscAmt_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtDiscAmt.Text != "" && txtTotalAmt.Text != "")
                {
                    if (Convert.ToDecimal(txtDiscAmt.Text) <= Convert.ToDecimal(txtTotalAmt.Text))
                    {
                        decimal d, o, total, damt = 0, netamt;
                        total = Convert.ToDecimal(txtTotalAmt.Text);
                        if (txtDiscAmt.Text != "" && Convert.ToDecimal(txtDiscAmt.Text) != 0)
                        {
                            damt = Convert.ToDecimal(txtDiscAmt.Text);
                            decimal disc = damt * 100 / total;
                            txtDiscount.Text = Convert.ToString(Math.Round(disc, 2, MidpointRounding.AwayFromZero));
                            d = Convert.ToDecimal(txtDiscount.Text);
                        }
                        if (txtotherchrgs.Text != "")
                            o = Convert.ToDecimal(txtotherchrgs.Text);
                        else
                            o = 0;
                        netamt = total - Math.Round(damt, 2, MidpointRounding.AwayFromZero) + o;
                        lblNetAmt.Text = Convert.ToString(Math.Round(netamt, 2, MidpointRounding.AwayFromZero));
                        if (intrdoff == 1)
                            RoundOff(netamt);
                    }
                    else
                    {
                        MessageBox.Show("Enter Discount Less or Equal to Total Amount.", "Warning");
                        txtDiscAmt.Text = "0";
                        this.ActiveControl = txtDiscAmt;
                    }
                }
                else
                {
                    txtDiscAmt.Text = "0";
                    this.ActiveControl = txtDiscAmt;
                }
            }
            catch (Exception)
            { }
        }

        private void txtotherchrgs_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateNetAmt();
            }
            catch (Exception)
            { }
        }

        private void txtPaidAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(lblFinalNetAmt.Text) >= Convert.ToDecimal(txtPaidAmt.Text))
                {
                    decimal rem;
                    rem = Convert.ToDecimal(lblFinalNetAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text);
                    lblbalAmt.Text = Convert.ToString(Math.Round(rem, 2, MidpointRounding.AwayFromZero));
                }
            }
            catch (Exception)
            { }
        }

        private void lblNetAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(lblFinalNetAmt.Text) >= Convert.ToDecimal(txtPaidAmt.Text))
                {
                    decimal rem;
                    rem = Convert.ToDecimal(lblFinalNetAmt.Text) - Convert.ToDecimal(txtPaidAmt.Text);
                    lblbalAmt.Text = Convert.ToString(Math.Round(rem, 2, MidpointRounding.AwayFromZero));
                }
            }
            catch (Exception)
            { }
        }

        void dtpbilldate_GotFocus(object sender, EventArgs e)
        {
            dtpbilldate.BackColor = Color.Pink;
        }
        void dtpbilldate_LostFocus(object sender, EventArgs e)
        {
            dtpbilldate.BackColor = Color.White;
        }
        void cmbcustomername_GotFocus(object sender, EventArgs e)
        {
            cmbcustomername.BackColor = Color.Pink;
        }
        void cmbcustomername_LostFocus(object sender, EventArgs e)
        {
            cmbcustomername.BackColor = Color.White;
        }
        void txtNarration_GotFocus(object sender, EventArgs e)
        {
            txtNarration.BackColor = Color.Pink;
        }
        void txtNarration_LostFocus(object sender, EventArgs e)
        {
            txtNarration.BackColor = Color.White;
        }
        void txtDiscount_GotFocus(object sender, EventArgs e)
        {
            txtDiscount.BackColor = Color.Pink;
        }
        void txtDiscount_LostFocus(object sender, EventArgs e)
        {
            txtDiscount.BackColor = Color.White;
        }
        void txtDiscAmt_GotFocus(object sender, EventArgs e)
        {
            txtDiscAmt.BackColor = Color.Pink;
        }
        void txtDiscAmt_LostFocus(object sender, EventArgs e)
        {
            txtDiscAmt.BackColor = Color.White;
        }
        void txtotherchrgs_GotFocus(object sender, EventArgs e)
        {
            txtotherchrgs.BackColor = Color.Pink;
        }
        void txtotherchrgs_LostFocus(object sender, EventArgs e)
        {
            txtotherchrgs.BackColor = Color.White;
        }
        void txtPaidAmt_GotFocus(object sender, EventArgs e)
        {
            txtPaidAmt.BackColor = Color.Pink;
        }
        void txtPaidAmt_LostFocus(object sender, EventArgs e)
        {
            txtPaidAmt.BackColor = Color.White;
        }

        public void maxbillidtype(int Type)
        {
            try
            {
                string initial = "";
                if (Type == 1)
                    initial = "T";
                else if (Type == 2)
                    initial = "E";
                else if (Type == 3)
                    initial = "F";
                try
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SELECT MAX(CAST(SUBSTRING(BillNo, 2, len(BillNo)-1) AS int))+1 FROM CustomerBillMaster where BillNo like '" + initial + "%' and TransactionYear = '" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                    var maxid = command.ExecuteScalar();
                    db.CloseConnection();
                    if (maxid.ToString() == "")
                        txtbillno.Text = initial + "1";
                    else
                        txtbillno.Text = initial + maxid;
                }
                catch (Exception)
                { db.CloseConnection(); }
            }
            catch (Exception)
            { }
        }

        public void savemaster()
        {
            try
            {
                CustomerBillMasterRepository Custmasterrepo = new CustomerBillMasterRepository();
                CustomerBillMaster billdata = new CustomerBillMaster();

                billdata.BillNo = Convert.ToString(txtbillno.Text);
                billdata.BillRcptNo = null;
                billdata.Billdate = Convert.ToDateTime(dtpbilldate.Value);
                billdata.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                billdata.CustomerName = cmbcustomername.Text;
                if (txtDiscount.Text != "")
                    billdata.Discount = Convert.ToDecimal(txtDiscount.Text);
                else
                    billdata.Discount = 0;
                if (lblFinalNetAmt.Text != "")
                    billdata.NetAmount = Convert.ToDecimal(lblFinalNetAmt.Text);
                else
                    billdata.NetAmount = Convert.ToDecimal(txtTotalAmt.Text);
                if (txtPaidAmt.Text != "")
                    billdata.PaidAmount = Convert.ToDecimal(txtPaidAmt.Text);
                else
                    billdata.PaidAmount = 0;
                if (lblbalAmt.Text != "")
                    billdata.RemainingAmount = Convert.ToDecimal(lblbalAmt.Text);
                else
                    billdata.RemainingAmount = 0;
                if (txtTotalAmt.Text != "")
                    billdata.TotalAmount = Convert.ToDecimal(txtTotalAmt.Text);
                else
                    billdata.TotalAmount = 0;
                billdata.Vat = 0;
                if (txtotherchrgs.Text != "")
                    billdata.TransportCharges = Convert.ToDecimal(txtotherchrgs.Text);
                else
                    billdata.TransportCharges = 0;
                try
                {
                    billdata.RoundOff = Convert.ToDecimal(lblroundoff.Text);
                }
                catch (Exception)
                { }
                billdata.YN = Convert.ToString("0");
                billdata.BillStatus = 0;
                billdata.Narration = txtNarration.Text.ToString();
                billdata.TransactionYear = CommonMethod.TransactionYear;
                billdata.CompId = CommonMethod.CompId;
                billid = Convert.ToString(txtbillno.Text);
                Custmasterrepo.Add(billdata);
                Custmasterrepo.Save();
                BillMasterID = Convert.ToInt32(billdata.Id);
                Custmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void saveremainingpayment(string mode)
        {
            try
            {
                RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                RemainingPayment remainingdata = new RemainingPayment();

                remainingdata.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                remainingdata.Customername = Convert.ToString(cmbcustomername.Text);
                if (lblFinalNetAmt.Text != "")
                    remainingdata.NetAmt = Convert.ToDecimal(lblFinalNetAmt.Text);
                else
                    remainingdata.NetAmt = Convert.ToDecimal(txtTotalAmt.Text);
                if (txtPaidAmt.Text != "")
                    remainingdata.PaidAmt = Convert.ToDecimal(txtPaidAmt.Text);
                else
                    remainingdata.PaidAmt = 0;
                if (lblbalAmt.Text != "")
                    remainingdata.RemainingAmt = Convert.ToDecimal(lblbalAmt.Text);
                else
                    remainingdata.RemainingAmt = 0;
                remainingdata.CreateDate = Convert.ToDateTime(dtpbilldate.Value);
                remainingdata.PaymentDate = Convert.ToDateTime(dtpbilldate.Value);
                remainingdata.PaymentMode = mode;
                remainingdata.Type = "Bill";
                remainingdata.BillId = Convert.ToString(txtbillno.Text);
                remainingdata.BillDate = Convert.ToDateTime(dtpbilldate.Value);
                remainingdata.TransactionYear = CommonMethod.TransactionYear;
                remainingdata.CompId = CommonMethod.CompId;
                remainingpayrepo.Add(remainingdata);
                remainingpayrepo.Save();
                remainingpayrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void savefakebillremainingpayment(string mode)
        {
            try
            {
                FakeRemainingPaymentRepository remainingpayrepo = new FakeRemainingPaymentRepository();
                FakeRemainingPayment remainingdata = new FakeRemainingPayment();

                remainingdata.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                remainingdata.Customername = Convert.ToString(cmbcustomername.Text);
                if (lblFinalNetAmt.Text != "")
                    remainingdata.NetAmt = Convert.ToDecimal(lblFinalNetAmt.Text);
                else
                    remainingdata.NetAmt = Convert.ToDecimal(txtTotalAmt.Text);
                if (txtPaidAmt.Text != "")
                    remainingdata.PaidAmt = Convert.ToDecimal(txtPaidAmt.Text);
                else
                    remainingdata.PaidAmt = 0;
                if (lblbalAmt.Text != "")
                    remainingdata.RemainingAmt = Convert.ToDecimal(lblbalAmt.Text);
                else
                    remainingdata.RemainingAmt = 0;
                remainingdata.CreateDate = Convert.ToDateTime(dtpbilldate.Value);
                remainingdata.PaymentDate = Convert.ToDateTime(dtpbilldate.Value);
                remainingdata.PaymentMode = mode;
                remainingdata.Type = "Bill";
                remainingdata.BillId = Convert.ToString(txtbillno.Text);
                remainingdata.BillDate = Convert.ToDateTime(dtpbilldate.Value);
                remainingdata.TransactionYear = CommonMethod.TransactionYear;
                remainingdata.CompId = CommonMethod.CompId;
                remainingpayrepo.Add(remainingdata);
                remainingpayrepo.Save();
                remainingpayrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void SendSMS(string billno, decimal netAmount)
        {
            try
            {
                bool connection = NetworkInterface.GetIsNetworkAvailable();
                if (connection == true)
                {
                    SettingRepository sRepo = new SettingRepository();
                    Setting sData = sRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (sData.SendSMS == "True")
                    {
                        if (sData.SendOption == "Auto" && sData.AutoForBill == "True")
                        {
                            CompanyInformationRepository compRepo = new CompanyInformationRepository();
                            CompanyInformation compData = compRepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();
                            CustomerRepository custRepo = new CustomerRepository();
                            string mobileno = custRepo.GetAll().Where(t => t.CustomerID == Convert.ToInt32(cmbcustomername.SelectedValue) && t.CustomerName == cmbcustomername.Text && t.CompId == CommonMethod.CompId).FirstOrDefault().Mobileno;
                            string smsText = "Welcome to " + compData.CompanyName + ". Your Bill No is " + billno + " and Your net amount is " + netAmount + "." + Environment.NewLine + "Please Visit Again...Thank You!!";
                            WebClient client = new WebClient();
                            string URL = "";
                            SMSRepository smsRepo = new SMSRepository();
                            SendSM smsData = new SendSM();
                            try
                            {
                                if (!string.IsNullOrEmpty(mobileno))
                                {
                                    if (System.Text.RegularExpressions.Regex.IsMatch(mobileno, "[^0-9]"))
                                    {
                                        smsData.Status = "Mobile no not valid for Auto Bill";
                                        smsRepo.Add(smsData);
                                        smsRepo.Save();
                                    }
                                    else if (mobileno.Length == 10)
                                    {
                                        string SmsStatusMsg = string.Empty;
                                        //  return SmsStatusMsg;
                                        smsData.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                                        smsData.CustomerName = cmbcustomername.Text;
                                        smsData.MobileNo = mobileno;
                                        smsData.SmsText = smsText;
                                        smsData.SmsDate = DateTime.Now.Date;
                                        smsData.BillNo = Convert.ToString(billno);
                                        URL = "http://bhashsms.com/api/sendmsg.php?user=" + sData.SMSUserId + "&pass=" + sData.SMSPassword + "&sender=" + sData.SMSKey + "&phone=" + mobileno + "&text=" + smsText + "&priority=ndnd&stype=normal";
                                        string result = apicall(URL);
                                        if (result.Contains("Username/Password Incorrect") == true)
                                        {
                                            smsData.Status = "Wrong User Name or Password For Bill Auto SMS";
                                            smsRepo.Add(smsData);
                                            smsRepo.Save();
                                        }
                                        else if (result.Contains("Sender ID Does not Exist") == true)
                                        {
                                            smsData.Status = "Sender ID Does not Exist For Bill Auto SMS";
                                            smsRepo.Add(smsData);
                                            smsRepo.Save();
                                        }
                                        else
                                        {
                                            smsData.Status = "Auto Bill";
                                            smsRepo.Add(smsData);
                                            smsRepo.Save();
                                            MessageBox.Show("SMS Send Successfully.","Success");
                                        }
                                    }
                                    else
                                    {
                                        smsData.Status = "Mobile no not valid for Auto Bill";
                                        smsRepo.Add(smsData);
                                        smsRepo.Save();
                                    }
                                }
                                else
                                {
                                    smsData.Status = "Mobile dose not exsist For Auto Bill";
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
                        }
                    }
                    sRepo.Dispose();
                }
                else
                    MessageBox.Show("Internet connection in not available");
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

        public void clear()
        {
            maxbillidtype(1);
            dtpbilldate.Value = DateTime.Today;
            customerdata();
            txtNarration.Text = string.Empty;
            txtTotalAmt.Text = "0";
            txtDiscount.Text = "0";
            txtDiscAmt.Text = "0";
            txtotherchrgs.Text = "0";
            txtPaidAmt.Text = "0";
            lblFinalNetAmt.Text = "0";
            lblroundoff.Text = "0"; ;
            lblbalAmt.Text = "0";
            lbltotSGST.Text = "0"; ;
            lbltotCGST.Text = "0";
            lbltotIGST.Text = "0";
            if (tbcntCust1.SelectedIndex == 0)
                lblNetAmt1.Text = "0.00";
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerBillMasterRepository Custmasterrepo = new CustomerBillMasterRepository();
                var BillId = Custmasterrepo.GetAll().Where(t => t.BillNo == Convert.ToString(txtbillno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                Custmasterrepo.Dispose();
                if (BillId != null)
                    MessageBox.Show("Already Saved This Record.", "Warning");
                else
                {
                    if (cmbcustomername.Text != "Select" && dtgvItemDetails1.Rows.Count != 0)
                    {
                        if (txtPaidAmt.Text != "0" || (txtPaidAmt.Text == "0" && lblbalAmt.Text != "0"))
                        {
                            //CustomerBillMaster table entry code
                            savemaster();
                            for (int i = 0; i < dtgvItemDetails1.Rows.Count; i++)
                            {
                                try
                                {
                                    string pname = Convert.ToString(dtgvItemDetails1.Rows[i].Cells[1].Value);
                                    string Proname = pname.Split(',')[0];
                                    string company = pname.Split(',')[1];
                                    string unit = pname.Split(',')[2];

                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPPOnBillSaveUpdate", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;

                                    command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'B';
                                    command.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                    command.Parameters.Add("@event", SqlDbType.Int).Value = 1;
                                    command.Parameters.Add("@billtype", SqlDbType.Int).Value = billtype;
                                    command.Parameters.Add("@BillNo", SqlDbType.Int).Value = BillMasterID;
                                    command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Proname;// Convert.ToString(dtgvItemDetails1.Rows[i].Cells[1].Value);
                                    command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = company;
                                    command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = unit;
                                    command.Parameters.Add("@AlternateUnit", SqlDbType.NVarChar).Value = unit;
                                    command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(dtgvItemDetails1.Rows[i].Cells[2].Value);
                                    command.Parameters.Add("@FreeQty", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                    command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Convert.ToString("");
                                    command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = Convert.ToDecimal(dtgvItemDetails1.Rows[i].Cells[3].Value);
                                    command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Convert.ToDecimal(dtgvItemDetails1.Rows[i].Cells[4].Value);
                                    command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                    command.Parameters.Add("@SGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(dtgvItemDetails1.Rows[i].Cells[5].Value);
                                    command.Parameters.Add("@SGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(dtgvItemDetails1.Rows[i].Cells[6].Value);
                                    command.Parameters.Add("@CGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(dtgvItemDetails1.Rows[i].Cells[7].Value);
                                    command.Parameters.Add("@CGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(dtgvItemDetails1.Rows[i].Cells[8].Value);
                                    command.Parameters.Add("@IGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(dtgvItemDetails1.Rows[i].Cells[9].Value);
                                    command.Parameters.Add("@IGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(dtgvItemDetails1.Rows[i].Cells[10].Value);
                                    command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = Convert.ToDecimal(dtgvItemDetails1.Rows[i].Cells[11].Value);
                                    ProductRepository productrepo = new ProductRepository();
                                    var productdata = productrepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                                    productrepo.Dispose();
                                    command.Parameters.Add("@HSNcode", SqlDbType.NVarChar).Value = Convert.ToString(productdata.HSNCode);
                                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                                    command.Parameters.Add("@StockIn", SqlDbType.Int).Value = 0;
                                    command.Parameters.Add("@ProdSubtitle", SqlDbType.NVarChar).Value = null; //nilophar 14072017
                                    command.ExecuteNonQuery();
                                    db.CloseConnection();
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                            }
                            //RemainingPayment table entry code
                            if (billtype == 1 || billtype == 2)
                            {
                                if (Convert.ToDecimal(lblbalAmt.Text) != 0)
                                    saveremainingpayment("Cash");
                            }
                            else
                            {
                                if (Convert.ToDecimal(lblbalAmt.Text) != 0)
                                    savefakebillremainingpayment("Cash");
                            }
                            MessageBox.Show("Invoice saved.", "Success");
                            SendSMS(billid, Convert.ToDecimal(lblFinalNetAmt.Text));
                            dtgvItemDetails1.Rows.Clear();
                            clear();
                            pnlMasterRecord.Visible = false;
                            this.ActiveControl = btnprint;
                        }
                        else
                        {
                            MessageBox.Show("Please fill paid amount.", "Warning");
                            this.ActiveControl = txtPaidAmt;
                        }
                    }
                    else
                    {
                        if (cmbcustomername.Text == "Select")
                        {
                            MessageBox.Show("Select Customer Name.", "Warning");
                            this.ActiveControl = cmbcustomername;
                        }
                        else
                            MessageBox.Show("Add Product Information.", "Warning");
                    }
                    goto w;
                }
            w:
                { }
                Custmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            { }
        }

        private void dtgvItemDetails1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {

                if (dtgvItemDetails1.CurrentCell.ColumnIndex == 1)
                {
                    string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                    SqlDataReader dreader;
                    SqlConnection conn = new SqlConnection(cnString1);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    AutoCompleteStringCollection acItemName = new AutoCompleteStringCollection();

                    if (CommonMethod.commProduct == true)
                        cmd.CommandText = "(SELECT ProductName+','+ManufactureCompany+','+Uniti as ItemName FROM ProductInformation)";
                    else
                        cmd.CommandText = "(SELECT ProductName+','+ManufactureCompany+','+Uniti as ItemName FROM ProductInformation where compid=" + CommonMethod.CompId + ")";
                    conn.Open();
                    dreader = cmd.ExecuteReader();
                    if (dreader.HasRows == true)
                    {
                        while (dreader.Read())
                            acItemName.Add(dreader["ItemName"].ToString());
                    }
                    dreader.Close();
                    TextBox txtItemName = e.Control as TextBox;
                    if (txtItemName != null)
                    {
                        txtItemName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtItemName.AutoCompleteCustomSource = acItemName;
                        txtItemName.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void dtgvItemDetails1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                dtgvItemDetails1.Rows[e.RowIndex - 1].Cells[0].Value = intTempCustNo;
                dtgvItemDetails1.Rows[e.RowIndex - 1].Cells[2].Value = 0;
                dtgvItemDetails1.Rows[e.RowIndex - 1].Cells[3].Value = 0.00;
                dtgvItemDetails1.Rows[e.RowIndex - 1].Cells[4].Value = 0.00;
                dtgvItemDetails1.Rows[e.RowIndex - 1].Cells[5].Value = 0;
                dtgvItemDetails1.Rows[e.RowIndex - 1].Cells[6].Value = 0.00;
                dtgvItemDetails1.Rows[e.RowIndex - 1].Cells[7].Value = 0;
                dtgvItemDetails1.Rows[e.RowIndex - 1].Cells[8].Value = 0.00;
                dtgvItemDetails1.Rows[e.RowIndex - 1].Cells[9].Value = 0;
                dtgvItemDetails1.Rows[e.RowIndex - 1].Cells[10].Value = 0.00;
                dtgvItemDetails1.Rows[e.RowIndex - 1].Cells[11].Value = 0.00;
            }
            catch (Exception)
            { }
        }

        private void dtgvItemDetails1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    string pname = dtgvItemDetails1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string proname = pname.Split(',')[0];
                    string company = pname.Split(',')[1];
                    string unit = pname.Split(',')[2];
                    ProductRepository productrepo = new ProductRepository();
                    var productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                    productrepo.Dispose();
                    if (productdata == null)
                        MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                    else
                    {
                        if (dtgvItemDetails1.Rows[e.RowIndex].Cells[2].Value.ToString() == "0")
                        {
                            dtgvItemDetails1.Rows[e.RowIndex].Cells[3].Value = productdata.Rate;
                            dtgvItemDetails1.Rows[e.RowIndex].Cells[4].Value = productdata.Rate * 0;
                            dtgvItemDetails1.Rows[e.RowIndex].Cells[5].Value = productdata.SGstPercent;
                            dtgvItemDetails1.Rows[e.RowIndex].Cells[6].Value = (productdata.Rate * 0) * productdata.SGstPercent / 100;
                            dtgvItemDetails1.Rows[e.RowIndex].Cells[7].Value = productdata.CGstPercent;
                            dtgvItemDetails1.Rows[e.RowIndex].Cells[8].Value = (productdata.Rate * 0) * productdata.CGstPercent / 100;
                            dtgvItemDetails1.Rows[e.RowIndex].Cells[9].Value = productdata.IGstPercent;
                            dtgvItemDetails1.Rows[e.RowIndex].Cells[10].Value = (productdata.Rate * 0) * productdata.IGstPercent / 100;
                            dtgvItemDetails1.Rows[e.RowIndex].Cells[11].Value = Convert.ToDecimal(dtgvItemDetails1.Rows[e.RowIndex].Cells[4].Value) + Convert.ToDecimal(dtgvItemDetails1.Rows[e.RowIndex].Cells[6].Value) + Convert.ToDecimal(dtgvItemDetails1.Rows[e.RowIndex].Cells[8].Value) + Convert.ToDecimal(dtgvItemDetails1.Rows[e.RowIndex].Cells[10].Value);
                        }
                    }
                }
                else if (e.ColumnIndex != 0 || e.ColumnIndex != 1)
                {
                    if (dtgvItemDetails1.Rows[e.RowIndex].Cells[2].Value.ToString() != "0")
                    {
                        decimal decamt = Convert.ToDecimal(dtgvItemDetails1.Rows[e.RowIndex].Cells[2].Value) * Convert.ToDecimal(dtgvItemDetails1.Rows[e.RowIndex].Cells[3].Value);
                        dtgvItemDetails1.Rows[e.RowIndex].Cells[4].Value = Convert.ToString(Math.Round(decamt, 2, MidpointRounding.AwayFromZero));
                        decimal decSGSTAmt = Convert.ToDecimal(dtgvItemDetails1.Rows[e.RowIndex].Cells[4].Value) * Convert.ToDecimal(dtgvItemDetails1.Rows[e.RowIndex].Cells[5].Value) / 100;
                        dtgvItemDetails1.Rows[e.RowIndex].Cells[6].Value = Convert.ToString(Math.Round(decSGSTAmt, 2, MidpointRounding.AwayFromZero));
                        decimal decCGSTAmt = Convert.ToDecimal(dtgvItemDetails1.Rows[e.RowIndex].Cells[4].Value) * Convert.ToDecimal(dtgvItemDetails1.Rows[e.RowIndex].Cells[7].Value) / 100;
                        dtgvItemDetails1.Rows[e.RowIndex].Cells[8].Value = Convert.ToString(Math.Round(decCGSTAmt, 2, MidpointRounding.AwayFromZero));
                        decimal decIGSTAmt = Convert.ToDecimal(dtgvItemDetails1.Rows[e.RowIndex].Cells[4].Value) * Convert.ToDecimal(dtgvItemDetails1.Rows[e.RowIndex].Cells[9].Value) / 100;
                        dtgvItemDetails1.Rows[e.RowIndex].Cells[10].Value = Convert.ToString(Math.Round(decIGSTAmt, 2, MidpointRounding.AwayFromZero));
                        dtgvItemDetails1.Rows[e.RowIndex].Cells[11].Value = Convert.ToDecimal(dtgvItemDetails1.Rows[e.RowIndex].Cells[4].Value) + Convert.ToDecimal(dtgvItemDetails1.Rows[e.RowIndex].Cells[6].Value) + Convert.ToDecimal(dtgvItemDetails1.Rows[e.RowIndex].Cells[8].Value) + Convert.ToDecimal(dtgvItemDetails1.Rows[e.RowIndex].Cells[10].Value);
                        dectotAmt = dtgvItemDetails1.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells[11].Value));
                        dectotSGST = dtgvItemDetails1.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells[6].Value));
                        dectotCGST = dtgvItemDetails1.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells[8].Value));
                        dectotIGST = dtgvItemDetails1.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells[10].Value));
                        lblNetAmt1.Text = Convert.ToString(Math.Round(dectotAmt, 2, MidpointRounding.AwayFromZero));
                    }
                }
            }
            catch (Exception)
            { }
        }
    }
}