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

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmCreditList : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public frmCreditList()
        {
            InitializeComponent();
            BindgridCredit();
            cm.changedatetimepickerrange(this);
            customerdata();
            progressBar1.Visible = false;
            grpboxSMS.Visible = false;
            label3.Visible = false;
        }

        public void BindgridCredit()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Select Id,BillNo,CONVERT(VARCHAR(24),Billdate,103) as 'Billdate',CONVERT(VARCHAR(24),PaymentDate,103) as 'CreditDate' ,	DATEDIFF(day,Billdate,GETDATE()) AS Days, CustomerName,NetAmount,PaidAmount, RemainingAmount as RemAmount from CustomerBillMaster where /*CONVERT(VARCHAR(24),BillDate,101)!=CONVERT(VARCHAR(24),PaymentDate,101) and*/ YN='0' and RemainingAmount !=0.00 and billno like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvBillInfo.DataSource = dt;
                GvBillInfo.Refresh();
                GvBillInfo.Columns["Id"].Visible = false;
                /*Add Button*/
                if (GvBillInfo.Columns.Contains("btnPaid"))
                {
                    if (GvBillInfo.Columns["btnPaid"].Visible == false)
                        GvBillInfo.Columns["btnPaid"].Visible = true;
                }
                else
                {
                    DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                    bcol.HeaderText = "Send Credit SMS";
                    bcol.Text = "Send SMS";
                    bcol.Name = "btnPaid";
                    bcol.FlatStyle = FlatStyle.System;
                    bcol.UseColumnTextForButtonValue = true;
                    GvBillInfo.Columns.Add(bcol);
                }
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        public void BindgridPaid()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("Select Id,BillNo,CONVERT(VARCHAR(24),Billdate,103) as 'Billdate',CONVERT(VARCHAR(24),PaymentDate,103) as 'CreditDate' ,	DATEDIFF(day,Billdate,GETDATE()) AS Days, CustomerName,NetAmount,PaidAmount, RemainingAmount as RemAmount from CustomerBillMaster where CONVERT(VARCHAR(24),BillDate,101)!=CONVERT(VARCHAR(24),PaymentDate,101) and YN='1' and RemainingAmount !=0.00  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GvBillInfo.DataSource = dt;
                GvBillInfo.Refresh();
                GvBillInfo.Columns["Id"].Visible = false;
                GvBillInfo.Columns["BillNo"].Width = 80;
                GvBillInfo.Columns["Billdate"].Width = 100;
                GvBillInfo.Columns["CreditDate"].Width = 100;
                GvBillInfo.Columns["Days"].Width = 50;
                GvBillInfo.Columns["CustomerName"].Width = 230;
                GvBillInfo.Columns["NetAmount"].Width = 100;
                GvBillInfo.Columns["PaidAmount"].Width = 100;
                GvBillInfo.Columns["RemAmount"].Width = 100;
                if (GvBillInfo.Columns.Contains("btnPaid"))
                    GvBillInfo.Columns["btnPaid"].Visible = false;
                da.Dispose();
                dt.Dispose();
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
                cmbcustomername.DataSource = customerdata;
                cmbcustomername.ValueMember = "CustomerID";
                cmbcustomername.DisplayMember = "CustomerName";
                custrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void chkbillno_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkbillno.Checked == true)
                    chkbetweendate.Checked = false;
            }
            catch (Exception)
            { }
        }

        private void chkbetweendate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkbetweendate.Checked == true)
                {
                    chkbillno.Checked = false;
                    customerdata();
                    cmbcustomername.Text = "Select";
                }
            }
            catch (Exception)
            { }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                if (chkpaid.Checked == true)
                {
                    if (chkbillno.Checked == true)
                    {
                        if (cmbcustomername.Text != "Select")
                            da = new SqlDataAdapter("Select Id,BillNo,CONVERT(VARCHAR(24),Billdate,103) as 'Billdate',CONVERT(VARCHAR(24),PaymentDate,103) as 'CreditDate' ,	DATEDIFF(day,Billdate,GETDATE()) AS Days, CustomerName,NetAmount,PaidAmount, RemainingAmount as RemAmount from CustomerBillMaster where CustomerId='" + cmbcustomername.SelectedValue + "' and CONVERT(VARCHAR(24),BillDate,101)!=CONVERT(VARCHAR(24),PaymentDate,101) and YN='1' and RemainingAmount !=0.00  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        else
                        {
                            MessageBox.Show("Select Customer Name.", "Warning");
                            this.ActiveControl = cmbcustomername;
                        }
                    }
                    else if (chkbetweendate.Checked == true)
                    {
                        if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                            da = new SqlDataAdapter("Select Id,BillNo,CONVERT(VARCHAR(24),Billdate,103) as 'Billdate',CONVERT(VARCHAR(24),PaymentDate,103) as 'CreditDate' ,	DATEDIFF(day,Billdate,GETDATE()) AS Days, CustomerName,NetAmount,PaidAmount, RemainingAmount as RemAmount from CustomerBillMaster where PaymentDate>='" + (cm.ValidFromDate(dtpfromdate.Value.Date)).ToString() + "' and PaymentDate<='" + (cm.ValidToDate(dtptodate.Value.Date)).ToString() + "' and CONVERT(VARCHAR(24),BillDate,101)!=CONVERT(VARCHAR(24),PaymentDate,101) and YN='1' and RemainingAmount !=0.00 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        else
                        {
                            MessageBox.Show("To Date Should greater than from date.", "Warning");
                            this.ActiveControl = dtpfromdate;
                        }
                    }
                    else
                        BindgridPaid();
                }
                else
                {
                    if (chkbillno.Checked == true)
                    {
                        if (cmbcustomername.Text != "Select")
                            da = new SqlDataAdapter("Select Id,BillNo,CONVERT(VARCHAR(24),Billdate,103) as 'Billdate',CONVERT(VARCHAR(24),PaymentDate,103) as 'CreditDate' ,	DATEDIFF(day,Billdate,GETDATE()) AS Days, CustomerName,NetAmount,PaidAmount, RemainingAmount as RemAmount from CustomerBillMaster where CustomerId='" + cmbcustomername.SelectedValue + "' /*and CONVERT(VARCHAR(24),BillDate,101)!=CONVERT(VARCHAR(24),PaymentDate,101)*/ and YN='0' and RemainingAmount !=0.00 and Billno like 'T%' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        else
                        {
                            MessageBox.Show("Select Customer Name.", "Warning");
                            this.ActiveControl = cmbcustomername;
                        }
                    }
                    else if (chkbetweendate.Checked == true)
                    {
                        if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                            da = new SqlDataAdapter("Select Id,BillNo,CONVERT(VARCHAR(24),Billdate,103) as 'Billdate',CONVERT(VARCHAR(24),PaymentDate,103) as 'CreditDate' ,	DATEDIFF(day,Billdate,GETDATE()) AS Days, CustomerName,NetAmount,PaidAmount, RemainingAmount as RemAmount from CustomerBillMaster where PaymentDate>='" + (cm.ValidFromDate(dtpfromdate.Value.Date)).ToString() + "' and PaymentDate<='" + (cm.ValidToDate(dtptodate.Value.Date)).ToString() + "' /*and CONVERT(VARCHAR(24),BillDate,101)!=CONVERT(VARCHAR(24),PaymentDate,101)*/ and YN='0' and Billno like 'T%' and RemainingAmount !=0.00 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        else
                        {
                            MessageBox.Show("To Date Should greater than from date.", "Warning");
                            this.ActiveControl = dtpfromdate;
                        }
                    }
                    else
                        BindgridCredit();
                }
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count != 0)
                {
                    GvBillInfo.DataSource = dt;
                    GvBillInfo.Refresh();
                    if (GvBillInfo.Rows.Count != 0)
                    {
                        GvBillInfo.Columns["Id"].Visible = false;
                        GvBillInfo.Columns["BillNo"].Width = 80;
                        GvBillInfo.Columns["Billdate"].Width = 100;
                        GvBillInfo.Columns["CreditDate"].Width = 100;
                        GvBillInfo.Columns["Days"].Width = 50;
                        GvBillInfo.Columns["CustomerName"].Width = 230;
                        GvBillInfo.Columns["NetAmount"].Width = 100;
                        GvBillInfo.Columns["PaidAmount"].Width = 100;
                        GvBillInfo.Columns["RemAmount"].Width = 100;
                        if (GvBillInfo.Columns.Contains("btnPaid"))
                            GvBillInfo.Columns["btnPaid"].Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("Record not found.", "Warning");
                        this.ActiveControl = cmbcustomername;
                    }
                    da.Dispose();
                    dt.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void btngetall_Click(object sender, EventArgs e)
        {
            try
            {
                dtpfromdate.Value = DateTime.Now.Date;
                dtptodate.Value = DateTime.Now.Date;
                customerdata();
                chkbillno.CheckState = 0;
                chkbetweendate.CheckState = 0;
                chkpaid.CheckState = 0;
                if (chkpaid.Checked == true)
                    BindgridPaid();
                else
                    BindgridCredit();
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

        private void GvBillInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                if (e.ColumnIndex == 0)
                {
                    sendCreditSMS(Convert.ToString(GvBillInfo.Rows[e.RowIndex].Cells["BillNo"].Value), Convert.ToDecimal(GvBillInfo.Rows[e.RowIndex].Cells["RemAmount"].Value));
                    if (chkbillno.Checked == true)
                    {
                        if (cmbcustomername.Text != "Select")
                            da = new SqlDataAdapter("Select Id,BillNo,CONVERT(VARCHAR(24),Billdate,103) as 'Billdate',CONVERT(VARCHAR(24),PaymentDate,103) as 'CreditDate' ,	DATEDIFF(day,Billdate,GETDATE()) AS Days, CustomerName,NetAmount,PaidAmount, RemainingAmount as RemAmount from CustomerBillMaster where CustomerId='" + cmbcustomername.SelectedValue + "' and CONVERT(VARCHAR(24),BillDate,101)!=CONVERT(VARCHAR(24),PaymentDate,101) and YN='0' and RemainingAmount !=0.00 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        else
                        {
                            MessageBox.Show("Select Customer Name.", "Warning");
                            this.ActiveControl = cmbcustomername;
                        }
                    }
                    else if (chkbetweendate.Checked == true)
                    {
                        if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                            da = new SqlDataAdapter("Select Id,BillNo,CONVERT(VARCHAR(24),Billdate,103) as 'Billdate',CONVERT(VARCHAR(24),PaymentDate,103) as 'CreditDate' ,	DATEDIFF(day,Billdate,GETDATE()) AS Days, CustomerName,NetAmount,PaidAmount, RemainingAmount as RemAmount from CustomerBillMaster where PaymentDate>='" + (cm.ValidFromDate(dtpfromdate.Value.Date)).ToString() + "' and PaymentDate<='" + (cm.ValidToDate(dtptodate.Value.Date)).ToString() + "' and CONVERT(VARCHAR(24),BillDate,101)!=CONVERT(VARCHAR(24),PaymentDate,101) and YN='0' and RemainingAmount !=0.00 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        else
                        {
                            MessageBox.Show("To Date Should greater than from date.", "Warning");
                            this.ActiveControl = dtpfromdate;
                        }
                    }
                    else
                        BindgridCredit();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        GvBillInfo.DataSource = dt;
                        GvBillInfo.Refresh();
                        if (GvBillInfo.Rows.Count != 0)
                        {
                            GvBillInfo.Columns["Id"].Visible = false;
                            GvBillInfo.Columns["BillNo"].Width = 80;
                            GvBillInfo.Columns["Billdate"].Width = 100;
                            GvBillInfo.Columns["CreditDate"].Width = 100;
                            GvBillInfo.Columns["Days"].Width = 50;
                            GvBillInfo.Columns["CustomerName"].Width = 230;
                            GvBillInfo.Columns["NetAmount"].Width = 100;
                            GvBillInfo.Columns["PaidAmount"].Width = 100;
                            GvBillInfo.Columns["RemAmount"].Width = 100;
                            /*Add Button*/
                            if (GvBillInfo.Columns.Contains("btnPaid"))
                            {
                                if (GvBillInfo.Columns["btnPaid"].Visible == false)
                                    GvBillInfo.Columns["btnPaid"].Visible = true;
                            }
                            else
                            {
                                DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                                bcol.HeaderText = "Paid";
                                bcol.Text = "Paid";
                                bcol.Name = "btnPaid";
                                bcol.FlatStyle = FlatStyle.System;
                                bcol.UseColumnTextForButtonValue = true;
                                GvBillInfo.Columns.Add(bcol);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Record not found.", "Warning");
                            this.ActiveControl = cmbcustomername;
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                }
            }
            catch (Exception)
            { }
        }

        public void sendCreditSMS(string billNo, decimal creditAmount)
        {
            try
            {
                SettingRepository sRepo = new SettingRepository();
                Setting sData = sRepo.GetAll().FirstOrDefault();
                if (sData.SendSMS == "True")
                {
                    DataTable dt = db.GetTable("Select c.CompanyName,cu.CustomerID,cu.CustomerName,cu.Mobileno from CompanyInformation c, CustomerInformation cu, " +
                        "CustomerBillMaster cb where c.Id=cu.CompId and c.Id=cb.CompId and c.Id=" + CommonMethod.CompId + " and cb.BillNo='" + billNo + "' " +
                        "and cu.CustomerID=cb.CustomerId and cb.TransactionYear='" + CommonMethod.TransactionYear + "'");
                    string smsText = "", URL = "";
                    if (sData.MarathiSMS == "True")
                        smsText = dt.Rows[0]["CompanyName"].ToString() + " मध्ये आपले स्वागत आहे. या बिल नं " + billNo + " साठी तुमच्या नावे रु. " + creditAmount + " आहे." + Environment.NewLine + " पुन्हा अवश्य भेट द्या...धन्यवाद.";
                    else
                        smsText = "Welcome to " + dt.Rows[0]["CompanyName"].ToString() + ". Your credit amount is " + creditAmount + ". For against Bill of " + billNo + Environment.NewLine + "Please Visit Again...Thank You!!";

                    List<int> list = new List<int>();
                    WebClient client = new WebClient();
                    SMSRepository smsRepo = new SMSRepository();
                    SendSM smsData = new SendSM();
                    try
                    {
                        string SmsStatusMsg = string.Empty;
                        smsData.CustomerId = Convert.ToInt32(dt.Rows[0]["CustomerID"].ToString());
                        smsData.CustomerName = dt.Rows[0]["CustomerName"].ToString();
                        smsData.MobileNo = dt.Rows[0]["Mobileno"].ToString();
                        smsData.SmsText = smsText;
                        smsData.SmsDate = DateTime.Now.Date;
                        smsData.BillNo = billNo;
                        smsData.CompId = CommonMethod.CompId;
                        if (!string.IsNullOrEmpty(dt.Rows[0]["Mobileno"].ToString()))
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(dt.Rows[0]["Mobileno"].ToString(), "[^0-9]"))
                            {
                                smsData.Status = "Mobile no not valid for Single Credit SMS from Credit List";
                                smsRepo.Add(smsData);
                                smsRepo.Save();
                            }
                            else if (dt.Rows[0]["Mobileno"].ToString().Length == 10)
                            {
                                URL = sData.SMSURLPath + sData.SMSUserId + "&pass=" + sData.SMSPassword + "&sender=" + sData.SMSKey + "&phone=" + dt.Rows[0]["Mobileno"].ToString() + "&text=" + smsText + "&priority=ndnd&stype=normal";
                                string result = apicall(URL);
                                if (result.Contains("Username/Password Incorrect") == true)
                                {
                                    smsData.Status = "Wrong User Name or Password for Single Credit SMS from Credit List";
                                    smsRepo.Add(smsData);
                                    smsRepo.Save();
                                }
                                else if (result.Contains("Sender ID Does not Exist") == true)
                                {
                                    smsData.Status = "Sender ID Does not Existfor Single Credit SMS from Credit List";
                                    smsRepo.Add(smsData);
                                    smsRepo.Save();
                                }
                                else
                                {
                                    smsData.Status = "Credit SMS for Single Credit SMS from Credit List";
                                    smsRepo.Add(smsData);
                                    smsRepo.Save();
                                    MessageBox.Show("SMS Send Successfully.", "Success");
                                }
                            }
                            else
                            {
                                smsData.Status = "Mobile no not valid for Single Credit SMS from Credit List";
                                smsRepo.Add(smsData);
                                smsRepo.Save();
                            }
                        }
                        else
                        {
                            smsData.Status = "Mobile dose not exsist for Single Credit SMS from Credit List";
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
                sRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void GvBillInfo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                if (e.KeyChar == (Char)Keys.Enter)
                {
                    if (GvBillInfo.CurrentCellAddress.X == 0)
                    {
                        CustomerBillMasterRepository Custmasterrepo = new CustomerBillMasterRepository();
                        CustomerBillMaster billdata = new CustomerBillMaster();
                        int id = Custmasterrepo.GetAll().Where(t => t.BillNo == Convert.ToString(GvBillInfo.Rows[GvBillInfo.CurrentCellAddress.X].Cells["BillNo"].Value) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault().Id;
                        billdata = Custmasterrepo.GetById(id);
                        billdata.YN = "1";
                        Custmasterrepo.Edit(billdata);
                        Custmasterrepo.Save();
                        if (chkbillno.Checked == true)
                        {
                            if (cmbcustomername.Text != "Select")
                                da = new SqlDataAdapter("Select Id,BillNo,CONVERT(VARCHAR(24),Billdate,103) as 'Billdate',CONVERT(VARCHAR(24),PaymentDate,103) as 'CreditDate' ,	DATEDIFF(day,Billdate,GETDATE()) AS Days, CustomerName,NetAmount,PaidAmount, RemainingAmount as RemAmount from CustomerBillMaster where CustomerId='" + cmbcustomername.SelectedValue + "' and CONVERT(VARCHAR(24),BillDate,101)!=CONVERT(VARCHAR(24),PaymentDate,101) and YN='0' and RemainingAmount !=0.00  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                            else
                            {
                                MessageBox.Show("Select Customer Name.", "Warning");
                                this.ActiveControl = cmbcustomername;
                            }
                        }
                        else if (chkbetweendate.Checked == true)
                        {
                            if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                                da = new SqlDataAdapter("Select Id,BillNo,CONVERT(VARCHAR(24),Billdate,103) as 'Billdate',CONVERT(VARCHAR(24),PaymentDate,103) as 'CreditDate' ,	DATEDIFF(day,Billdate,GETDATE()) AS Days, CustomerName,NetAmount,PaidAmount, RemainingAmount as RemAmount from CustomerBillMaster where PaymentDate>='" + cm.ValidFromDate(dtpfromdate.Value.Date).ToString() + "' and PaymentDate<='" + cm.ValidToDate(dtptodate.Value.Date).ToString() + "' and CONVERT(VARCHAR(24),BillDate,101)!=CONVERT(VARCHAR(24),PaymentDate,101) and YN='0' and RemainingAmount !=0.00 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                            else
                            {
                                MessageBox.Show("To Date Should greater than from date.", "Warning");
                                this.ActiveControl = dtpfromdate;
                            }
                        }
                        else
                            BindgridCredit();
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            GvBillInfo.DataSource = dt;
                            GvBillInfo.Refresh();
                            if (GvBillInfo.Rows.Count != 0)
                            {
                                GvBillInfo.Columns["Id"].Visible = false;
                                GvBillInfo.Columns["BillNo"].Width = 80;
                                GvBillInfo.Columns["Billdate"].Width = 100;
                                GvBillInfo.Columns["CreditDate"].Width = 100;
                                GvBillInfo.Columns["Days"].Width = 50;
                                GvBillInfo.Columns["CustomerName"].Width = 230;
                                GvBillInfo.Columns["NetAmount"].Width = 100;
                                GvBillInfo.Columns["PaidAmount"].Width = 100;
                                GvBillInfo.Columns["RemAmount"].Width = 100;
                                /*Add Button*/
                                if (GvBillInfo.Columns.Contains("btnPaid"))
                                {
                                    if (GvBillInfo.Columns["btnPaid"].Visible == false)
                                        GvBillInfo.Columns["btnPaid"].Visible = true;
                                }
                                else
                                {
                                    DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                                    bcol.HeaderText = "Paid";
                                    bcol.Text = "Paid";
                                    bcol.Name = "btnPaid";
                                    bcol.UseColumnTextForButtonValue = true;
                                    GvBillInfo.Columns.Add(bcol);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Record not found.", "Warning");
                                this.ActiveControl = cmbcustomername;
                            }
                            da.Dispose(); dt.Dispose();
                        }
                        Custmasterrepo.Dispose();
                    }
                }
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

        private void dtpfromdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtptodate;
            }
            catch (Exception)
            { }
        }

        private void dtptodate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chkpaid_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void frmCreditList_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void cmbcustomername_Leave(object sender, EventArgs e)
        {
            if (cmbcustomername.Text != "" && cmbcustomername.Text != "Select")
            {
                CommonMethod cm = new CommonMethod();
                if (cm.ValidCustomer(cmbcustomername.Text) == 1)
                { }
                else
                {
                    MessageBox.Show("Invalid Customer.", "Warning");
                    customerdata();
                    this.ActiveControl = cmbcustomername;
                }
            }
        }

        private void chkbillno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcustomername;
            }
            catch (Exception)
            { }
        }

        private void chkbetweendate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpfromdate;
            }
            catch (Exception)
            { }
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
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
                        CompanyInformationRepository compRepo = new CompanyInformationRepository();
                        CompanyInformation compData = compRepo.GetAll().FirstOrDefault();
                        CustomerBillMasterRepository cMasterRepo = new CustomerBillMasterRepository();
                        CreditAmountRepository CreditRepo = new CreditAmountRepository();
                        List<CreditAmount> CreditData = new List<CreditAmount>();
                        List<CustomerBillMaster> cMasterData = new List<CustomerBillMaster>();
                        List<int> list = new List<int>();
                        try
                        {
                            cMasterData = cMasterRepo.GetAll().Where(t => t.YN == "0" && t.RemainingAmount > Convert.ToDecimal(0) && t.Billdate.Value.Date != t.PaymentDate.Value.Date && t.PaymentDate.Value.Date < DateTime.Now.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        }
                        catch (Exception)
                        { }

                        if (cMasterData.Count() != 0)
                        {
                            grpboxSMS.Visible = true;
                            label3.Visible = true;
                            progressBar1.Visible = true;

                            progressBar1.Minimum = 1;
                            progressBar1.Maximum = cMasterData.Count();
                            int k = 1;
                            foreach (var i in cMasterData)
                            {
                                progressBar1.Value = k;
                                CustomerRepository custRepo = new CustomerRepository();
                                string mobileno = custRepo.GetAll().Where(t => t.CustomerID == Convert.ToInt32(i.CustomerId) && t.CustomerName == i.CustomerName && t.CompId == CommonMethod.CompId).FirstOrDefault().Mobileno;
                                string smsText = "";
                                if (sData.MarathiSMS == "True")
                                    smsText = compData.CompanyName + " मध्ये आपले स्वागत आहे. या बिल नं " + i.RemainingAmount + " साठी तुमच्या नावे रु. " + i.RemainingAmount + " आहे." + Environment.NewLine + " पुन्हा अवश्य भेट द्या...धन्यवाद.";
                                else
                                    smsText = "Welcome to " + compData.CompanyName + ". Your credit amount is " + i.RemainingAmount + ". For against Bill of " + i.RemainingAmount + Environment.NewLine + "Please Visit Again...Thank You!!";

                                WebClient client = new WebClient();
                                string MobNo = mobileno, URL = "";
                                SMSRepository smsRepo = new SMSRepository();
                                SendSM smsData = new SendSM();
                                try
                                {
                                    string SmsStatusMsg = string.Empty;
                                    smsData.CustomerId = Convert.ToInt32(i.CustomerId);
                                    smsData.CustomerName = i.CustomerName;
                                    smsData.MobileNo = mobileno;
                                    smsData.SmsText = smsText;
                                    smsData.SmsDate = DateTime.Now.Date;
                                    smsData.BillNo = i.BillNo;
                                    smsData.CompId = CommonMethod.CompId;
                                    if (!string.IsNullOrEmpty(MobNo))
                                    {
                                        if (System.Text.RegularExpressions.Regex.IsMatch(MobNo, "[^0-9]"))
                                        {
                                            smsData.Status = "Mobile no not valid for Credit SMS from MDI Load";
                                            smsRepo.Add(smsData);
                                            smsRepo.Save();
                                        }
                                        else if (MobNo.Length == 10)
                                        {
                                            URL = "http://bhashsms.com/api/sendmsg.php?user=" + sData.SMSUserId + "&pass=" + sData.SMSPassword + "&sender=" + sData.SMSKey + "&phone=" + MobNo + "&text=" + smsText + "&priority=ndnd&stype=normal";
                                            string result = apicall(URL);
                                            if (result.Contains("Username/Password Incorrect") == true)
                                            {
                                                smsData.Status = "Wrong User Name or Password For Credit SMS from MDI Load";
                                                smsRepo.Add(smsData);
                                                smsRepo.Save();
                                            }
                                            else if (result.Contains("Sender ID Does not Exist") == true)
                                            {
                                                smsData.Status = "Sender ID Does not Exist For Credit SMS from MDI Load";
                                                smsRepo.Add(smsData);
                                                smsRepo.Save();
                                            }
                                            else
                                            {
                                                smsData.Status = "Credit SMS from MDI Load";
                                                smsRepo.Add(smsData);
                                                smsRepo.Save();
                                                MessageBox.Show("SMS Send Successfully.", "Success");
                                            }
                                        }
                                        else
                                        {
                                            smsData.Status = "Mobile no not valid for Credit SMS from MDI Load";
                                            smsRepo.Add(smsData);
                                            smsRepo.Save();
                                        }
                                    }
                                    else
                                    {
                                        smsData.Status = "Mobile dose not exsist. Credit SMS from MDI Load";
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
                                k++;
                                custRepo.Dispose();
                                smsRepo.Dispose();
                            }
                            progressBar1.Visible = false;
                            grpboxSMS.Visible = false;
                            label3.Visible = false;
                        }
                        compRepo.Dispose();
                        cMasterRepo.Dispose();
                        CreditRepo.Dispose();
                    }
                    sRepo.Dispose();
                }
                else
                    MessageBox.Show("Internet connection in not available");
            }
            catch (Exception)
            { }
        }

        private void btnSMSReport_Click(object sender, EventArgs e)
        {
            try
            {
                frmSendSMSTracking compinfo = new frmSendSMSTracking();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
            }
            catch (Exception)
            { }
        }
    }
}