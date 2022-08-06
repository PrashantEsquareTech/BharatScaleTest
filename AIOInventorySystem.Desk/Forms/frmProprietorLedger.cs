using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.RachitControls;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmProprietorLedger : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        DateTime currentdate = Convert.ToDateTime(DateTime.Now);
        public int receiptid;
        public string ProLedname1 = "";
        List<string> multipleReceiptPrintList = new List<string>();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        DateTime fdate1, tdate1;

        public frmProprietorLedger()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            Maxcustid();
            fillcombo();
            fillcombo1();
            cmbbank.Enabled = false;
            txtchequeno.Enabled = false;
            dtpchequedate.Enabled = false;
            ProprietorData();
            ProprietorData1();
        }

        public frmProprietorLedger(string ProledName, DateTime fdate, DateTime tdate)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            Maxcustid();
            fillcombo();
            fillcombo1();
            cmbbank.Enabled = false;
            txtchequeno.Enabled = false;
            dtpchequedate.Enabled = false;
            ProprietorData();
            ProprietorData1();
            this.ActiveControl = cmbProprietor;
            ProLedname1 = ProledName;
            fdate1 = fdate;
            tdate1 = tdate;
            BalShtToProLedger(ProledName, fdate, tdate);
        }

        public void Maxcustid()
        {
            try
            {
                ProprietorLedgerRepository remainingpayrepo = new ProprietorLedgerRepository();
                var mid = remainingpayrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.ReceiptNo);
                if (mid == null)
                    txtreceiptno.Text = "1";
                else
                    txtreceiptno.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                remainingpayrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void ProprietorData()
        {
            try
            {
                ProprietorMasterRepository custrepo = new ProprietorMasterRepository();
                List<ProprietorMaster> customerdata = new List<ProprietorMaster>();
                customerdata.Clear();
                customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.ProprietorName).ToList();
                customerdata.Add(new ProprietorMaster { ProprietorName = "Select", ProprietorId = 0 });
                customerdata.Reverse();
                cmbProprietor.DataSource = customerdata;
                cmbProprietor.ValueMember = "ProprietorId";
                cmbProprietor.DisplayMember = "ProprietorName";
                custrepo.Dispose();
                cmbProprietor.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void ProprietorData1()
        {
            try
            {
                ProprietorMasterRepository custrepo = new ProprietorMasterRepository();
                List<ProprietorMaster> customerdata = new List<ProprietorMaster>();
                customerdata.Clear();
                customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.ProprietorName).ToList();
                customerdata.Add(new ProprietorMaster { ProprietorName = "Select", ProprietorId = 0 });
                customerdata.Reverse();
                cmbProprietorname1.DataSource = customerdata;
                cmbProprietorname1.ValueMember = "ProprietorId";
                cmbProprietorname1.DisplayMember = "ProprietorName";
                custrepo.Dispose();
                cmbProprietorname1.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void fillcombo()
        {
            try
            {
                BankRepository bankrepo = new BankRepository();
                List<BankInformation> bankdata = new List<BankInformation>();
                bankdata.Clear();
                bankdata = bankrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.Bankname).ToList();
                bankdata.Add(new BankInformation { Bankname = "Select", BankId = 0 });
                bankdata.Reverse();
                cmbbank.DataSource = bankdata;
                cmbbank.ValueMember = "BankId";
                cmbbank.DisplayMember = "Bankname";
                bankrepo.Dispose();
                cmbbank.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void fillcombo1()
        {
            try
            {
                BankRepository bankrepo = new BankRepository();
                List<BankInformation> bankdata = new List<BankInformation>();
                bankdata.Clear();
                bankdata = bankrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.Bankname).ToList();
                bankdata.Add(new BankInformation { Bankname = "Select", BankId = 0 });
                bankdata.Reverse();
                cmbbank2.DataSource = bankdata;
                cmbbank2.ValueMember = "BankId";
                cmbbank2.DisplayMember = "Bankname";
                bankrepo.Dispose();
                cmbbank2.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                try
                {
                    dtpreceiptDate.Value = DateTime.Now.Date;
                    dtpchequedate.Value = DateTime.Now.Date;
                }
                catch (Exception)
                { }
                cmbTransactiontype.SelectedIndex = -1;
                txtPaidAmt.Text = "0";
                txtparticular.Text = "";
                ProprietorData();
                txtchequeno.Text = "";
                Maxcustid();
                cmbpaymentmode.SelectedIndex = -1;
                cmbbank.Enabled = false;
                txtchequeno.Enabled = false;
                dtpchequedate.Enabled = false;
                btnSave.Text = "Save";
                label15.Text = "Cheque No:";
                if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
                {
                    btnSave.Enabled = false;
                    btnnew.Enabled = false;
                }
                btndelete.Enabled = false;
                fillcombo();
                fillcombo1();
            }
            catch (Exception)
            { }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
            }
            catch (Exception)
            { }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveupdate();
        }

        public void saveupdate()
        {
            try
            {
                if (cmbProprietor.Text != "Select" && cmbTransactiontype.Text != "" && txtPaidAmt.Text != "0" && cmbpaymentmode.Text != "")
                {
                    if (btnSave.Text == "Save")
                    {
                        ProprietorLedgerRepository remainingpayrepo = new ProprietorLedgerRepository();
                        ProprietorLedger remainingdata = new ProprietorLedger();
                        Maxcustid();
                        remainingdata.ReceiptNo = Convert.ToInt32(txtreceiptno.Text);
                        remainingdata.ProprietorId = Convert.ToInt32(cmbProprietor.SelectedValue);
                        remainingdata.ProprietorName = cmbProprietor.Text;
                        remainingdata.CreateDate = dtpreceiptDate.Value;
                        remainingdata.Type = cmbTransactiontype.Text;
                        if (cmbTransactiontype.Text == "Credit")
                        {
                            remainingdata.NetAmount = Convert.ToDecimal(txtPaidAmt.Text);
                            remainingdata.PaidAmount = Convert.ToDecimal(0);
                        }
                        else if (cmbTransactiontype.Text == "Debit")
                        {
                            remainingdata.NetAmount = Convert.ToDecimal(0);
                            remainingdata.PaidAmount = Convert.ToDecimal(txtPaidAmt.Text);
                        }
                        remainingdata.TransactionYear = CommonMethod.TransactionYear;
                        remainingdata.CompId = CommonMethod.CompId;
                        remainingdata.PaymentMode = cmbpaymentmode.Text;
                        if (cmbpaymentmode.Text == "Cash")
                        {
                            remainingdata.BankName = "";
                            remainingdata.ChequeNo = "";
                            remainingdata.ChequeDate = null;
                            remainingpayrepo.Add(remainingdata);
                            remainingpayrepo.Save();
                            MessageBox.Show("Receipt Information Saved.", "Success");
                            clear();
                        }
                        else if (cmbpaymentmode.Text == "Cheque")
                        {
                            if (cmbbank.Text != "Select" && txtchequeno.Text != "")
                            {
                                remainingdata.BankName = cmbbank.Text;
                                remainingdata.ChequeNo = txtchequeno.Text;
                                remainingdata.ChequeDate = dtpchequedate.Value;
                                remainingpayrepo.Add(remainingdata);
                                remainingpayrepo.Save();

                                try
                                {
                                    BankStatementRepository bankrepo = new BankStatementRepository();
                                    BankStatement bankstdata = new BankStatement();

                                    bankstdata.BankId = Convert.ToInt32(cmbbank.SelectedValue);
                                    bankstdata.Bankname = Convert.ToString(cmbbank.Text);
                                    bankstdata.TransactionDate = dtpreceiptDate.Value;
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    if (cmbTransactiontype.Text == "Credit")
                                    {
                                        bankstdata.Deposits = Convert.ToDecimal(txtPaidAmt.Text);
                                        bankstdata.Particulars = "Receive From" + " " + Convert.ToString(cmbProprietor.Text.Trim());
                                    }
                                    else if (cmbTransactiontype.Text == "Debit")
                                    {
                                        bankstdata.Withdrawals = Convert.ToDecimal(txtPaidAmt.Text);
                                        bankstdata.Particulars = "Send To" + " " + Convert.ToString(cmbProprietor.Text.Trim());
                                    }
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankstdata.TransactionType = "Proprietor Receipt";
                                    bankrepo.Add(bankstdata);
                                    bankrepo.Save();
                                    bankrepo.Dispose();
                                }
                                catch (Exception)
                                { }
                                MessageBox.Show("Receipt Information Saved.", "Success");
                                clear();
                            }
                            else
                            {
                                if (cmbbank.Text == "Select")
                                {
                                    MessageBox.Show("Select Bank name.", "Warning");
                                    this.ActiveControl = cmbbank;
                                }
                                else
                                {
                                    MessageBox.Show("Enter Cheque Number.", "Warning");
                                    this.ActiveControl = txtchequeno;
                                }
                            }
                        }
                        else
                        {
                            if (cmbbank.Text.TrimStart() != "Select" && txtchequeno.Text.TrimStart() != "")
                            {
                                remainingdata.BankName = cmbbank.Text;
                                remainingdata.ChequeNo = txtchequeno.Text;
                                remainingdata.ChequeDate = dtpchequedate.Value;
                                remainingpayrepo.Add(remainingdata);
                                remainingpayrepo.Save();
                                try
                                {
                                    BankStatementRepository bankrepo = new BankStatementRepository();
                                    BankStatement bankstdata = new BankStatement();

                                    bankstdata.BankId = Convert.ToInt32(cmbbank.SelectedValue);
                                    bankstdata.Bankname = Convert.ToString(cmbbank.Text);
                                    bankstdata.TransactionDate = dtpreceiptDate.Value;
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    if (cmbTransactiontype.Text == "Credit")
                                    {
                                        bankstdata.Deposits = Convert.ToDecimal(txtPaidAmt.Text);
                                        bankstdata.Particulars = "Receive From" + " " + Convert.ToString(cmbProprietor.Text.Trim());
                                    }
                                    else if (cmbTransactiontype.Text == "Debit")
                                    {
                                        bankstdata.Withdrawals = Convert.ToDecimal(txtPaidAmt.Text);
                                        bankstdata.Particulars = "Send To" + " " + Convert.ToString(cmbProprietor.Text.Trim());
                                    }
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankstdata.TransactionType = "Proprietor Receipt";
                                    bankrepo.Add(bankstdata);
                                    bankrepo.Save();
                                    bankrepo.Dispose();
                                }
                                catch (Exception)
                                { }
                                MessageBox.Show("Receipt Information Saved.", "Success");
                                clear();
                            }
                            else
                            {
                                if (cmbbank.Text.TrimStart() == "Select")
                                {
                                    MessageBox.Show("Select Bank Name.", "Warning");
                                    this.ActiveControl = cmbbank;
                                }
                                else
                                {
                                    MessageBox.Show("Enter UTR Number.", "Warning");
                                    this.ActiveControl = txtchequeno;
                                }
                            }
                        }
                        remainingpayrepo.Dispose();
                    }
                    else
                    {
                        ProprietorLedgerRepository remainingpayrepo = new ProprietorLedgerRepository();
                        ProprietorLedger remainingdata = new ProprietorLedger();
                        remainingdata = remainingpayrepo.GetById(receiptid);
                        remainingdata.ReceiptNo = Convert.ToInt32(txtreceiptno.Text);
                        remainingdata.ProprietorId = Convert.ToInt32(cmbProprietor.SelectedValue);
                        remainingdata.ProprietorName = cmbProprietor.Text;
                        remainingdata.CreateDate = dtpreceiptDate.Value;
                        remainingdata.Type = cmbTransactiontype.Text;
                        if (cmbTransactiontype.Text == "Credit")
                        {
                            remainingdata.NetAmount = Convert.ToDecimal(txtPaidAmt.Text);
                            remainingdata.PaidAmount = Convert.ToDecimal(0);
                        }
                        else if (cmbTransactiontype.Text == "Debit")
                        {
                            remainingdata.NetAmount = Convert.ToDecimal(0);
                            remainingdata.PaidAmount = Convert.ToDecimal(txtPaidAmt.Text);
                        }
                        remainingdata.TransactionYear = CommonMethod.TransactionYear;
                        remainingdata.CompId = CommonMethod.CompId;
                        remainingdata.PaymentMode = cmbpaymentmode.Text;
                        if (cmbpaymentmode.Text == "Cash")
                        {
                            remainingdata.BankName = "";
                            remainingdata.ChequeNo = "";
                            remainingdata.ChequeDate = null;
                            remainingpayrepo.Edit(remainingdata);
                            remainingpayrepo.Save();
                            MessageBox.Show("Receipt Information Saved.", "Success");
                            clear();
                        }
                        else if (cmbpaymentmode.Text == "Cheque")
                        {
                            if (cmbbank.Text != "Select" && txtchequeno.Text != "")
                            {
                                remainingdata.BankName = cmbbank.Text;
                                remainingdata.ChequeNo = txtchequeno.Text;
                                remainingdata.ChequeDate = dtpchequedate.Value;
                                try
                                {
                                    ProprietorLedgerRepository remRepo = new ProprietorLedgerRepository();
                                    ProprietorLedger remdata = remRepo.GetById(receiptid);
                                    BankStatementRepository BankStRepo = new BankStatementRepository();
                                    BankStatement bdata = new BankStatement();
                                    if (remdata.Type == "Credit")
                                        bdata = BankStRepo.GetAll().Where(t => t.Bankname == cmbbank.Text && t.CompId == CommonMethod.CompId && t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.ChequeNo && t.Deposits == Convert.ToDecimal(remdata.NetAmount)).FirstOrDefault();
                                    else if (remdata.Type == "Debit")
                                        bdata = BankStRepo.GetAll().Where(t => t.Bankname == cmbbank.Text && t.CompId == CommonMethod.CompId && t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.ChequeNo && t.Deposits == Convert.ToDecimal(remdata.PaidAmount)).FirstOrDefault();

                                    BankStatementRepository bankrepo = new BankStatementRepository();
                                    BankStatement bankstdata = new BankStatement();
                                    bankstdata.BankId = Convert.ToInt32(cmbbank.SelectedValue);
                                    bankstdata.Bankname = Convert.ToString(cmbbank.Text);
                                    bankstdata.TransactionDate = dtpreceiptDate.Value;
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    if (cmbTransactiontype.Text == "Credit")
                                    {
                                        bankstdata.Deposits = Convert.ToDecimal(txtPaidAmt.Text);
                                        bankstdata.Particulars = "Receive From" + " " + Convert.ToString(cmbProprietor.Text.Trim());
                                    }
                                    else if (cmbTransactiontype.Text == "Debit")
                                    {
                                        bankstdata.Withdrawals = Convert.ToDecimal(txtPaidAmt.Text);
                                        bankstdata.Particulars = "Send To" + " " + Convert.ToString(cmbProprietor.Text.Trim());
                                    }
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankstdata.TransactionType = "Proprietor Receipt";
                                    if (!string.IsNullOrEmpty(Convert.ToString(bdata)))
                                    {
                                        bankstdata.Id = bdata.Id;
                                        bankrepo.Edit(bankstdata);
                                        bankrepo.Save();
                                    }
                                    else if (string.IsNullOrEmpty(Convert.ToString(bdata)))
                                    {
                                        bankrepo.Add(bankstdata);
                                        bankrepo.Save();
                                    }
                                    BankStRepo.Dispose();
                                    bankrepo.Dispose();
                                }
                                catch (Exception)
                                { }
                                remainingpayrepo.Edit(remainingdata);
                                remainingpayrepo.Save();
                                MessageBox.Show("Receipt Information Saved.", "Success");
                                clear();
                            }
                            else
                            {
                                if (cmbbank.Text == "Select")
                                {
                                    MessageBox.Show("Select Bank name.", "Warning");
                                    this.ActiveControl = cmbbank;
                                }
                                else
                                {
                                    MessageBox.Show("Enter Cheque Number.", "Warning");
                                    this.ActiveControl = txtchequeno;
                                }
                            }
                        }
                        else
                        {
                            if (cmbbank.Text.TrimStart() != "Select" && txtchequeno.Text.TrimStart() != "")
                            {
                                remainingdata.BankName = cmbbank.Text;
                                remainingdata.ChequeNo = txtchequeno.Text;
                                remainingdata.ChequeDate = dtpchequedate.Value;
                                try
                                {
                                    ProprietorLedgerRepository remRepo = new ProprietorLedgerRepository();
                                    ProprietorLedger remdata = remRepo.GetById(receiptid);
                                    BankStatementRepository BankStRepo = new BankStatementRepository();
                                    BankStatement bdata = new BankStatement();
                                    if (remdata.Type == "Credit")
                                        bdata = BankStRepo.GetAll().Where(t => t.Bankname == cmbbank.Text && t.CompId == CommonMethod.CompId && t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.ChequeNo && t.Deposits == Convert.ToDecimal(remdata.NetAmount)).FirstOrDefault();
                                    else if (remdata.Type == "Debit")
                                        bdata = BankStRepo.GetAll().Where(t => t.Bankname == cmbbank.Text && t.CompId == CommonMethod.CompId && t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.ChequeNo && t.Deposits == Convert.ToDecimal(remdata.PaidAmount)).FirstOrDefault();
                                    BankStatementRepository bankrepo = new BankStatementRepository();
                                    BankStatement bankstdata = new BankStatement();
                                    bankstdata.BankId = Convert.ToInt32(cmbbank.SelectedValue);
                                    bankstdata.Bankname = Convert.ToString(cmbbank.Text);
                                    bankstdata.TransactionDate = dtpreceiptDate.Value;
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    if (cmbTransactiontype.Text == "Credit")
                                    {
                                        bankstdata.Deposits = Convert.ToDecimal(txtPaidAmt.Text);
                                        bankstdata.Particulars = "Receive From" + " " + Convert.ToString(cmbProprietor.Text.Trim());
                                    }
                                    else if (cmbTransactiontype.Text == "Debit")
                                    {
                                        bankstdata.Withdrawals = Convert.ToDecimal(txtPaidAmt.Text);
                                        bankstdata.Particulars = "Send To" + " " + Convert.ToString(cmbProprietor.Text.Trim());
                                    }
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankstdata.TransactionType = "Proprietor Receipt";
                                    if (!string.IsNullOrEmpty(Convert.ToString(bdata)))
                                    {
                                        bankstdata.Id = bdata.Id;
                                        bankrepo.Edit(bankstdata);
                                        bankrepo.Save();
                                    }
                                    else if (string.IsNullOrEmpty(Convert.ToString(bdata)))
                                    {
                                        bankrepo.Add(bankstdata);
                                        bankrepo.Save();
                                    }
                                    BankStRepo.Dispose();
                                    bankrepo.Dispose();
                                }
                                catch (Exception)
                                { }
                                remainingpayrepo.Edit(remainingdata);
                                remainingpayrepo.Save();
                                MessageBox.Show("Receipt Information Saved.", "Success");
                                clear();
                            }
                            else
                            {
                                if (cmbbank.Text.TrimStart() == "Select")
                                {
                                    MessageBox.Show("Select Bank Name.", "Warning");
                                    this.ActiveControl = cmbbank;
                                }
                                else
                                {
                                    MessageBox.Show("Enter UTR Number.", "Warning");
                                    this.ActiveControl = txtchequeno;
                                }
                            }
                        }
                        remainingpayrepo.Dispose();
                    }
                }
                else
                {
                    if (cmbProprietor.Text == "Select")
                    {
                        MessageBox.Show("Select Proprietor Name.", "Warning");
                        this.ActiveControl = cmbProprietor;
                    }
                    else if (cmbTransactiontype.Text == "")
                    {
                        MessageBox.Show("Select Transaction Type.", "Warning");
                        this.ActiveControl = cmbTransactiontype;
                    }
                    else if (txtPaidAmt.Text == "0")
                    {
                        MessageBox.Show("Enter Amount.", "Warning");
                        this.ActiveControl = txtPaidAmt;
                    }
                    else if (cmbpaymentmode.Text == "")
                    {
                        MessageBox.Show("Select Payment Mode.", "Warning");
                        this.ActiveControl = cmbpaymentmode;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbpaymentmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbpaymentmode.Text == "Cheque")
                {
                    cmbbank.Enabled = true;
                    txtchequeno.Enabled = true;
                    dtpchequedate.Enabled = true;
                    label15.Text = "Cheque No:";
                    label13.Text = "Cheque Date :";
                }
                else if (cmbpaymentmode.Text == "NEFT" || cmbpaymentmode.Text == "RTGS")
                {
                    cmbbank.Enabled = true;
                    txtchequeno.Enabled = true;
                    dtpchequedate.Enabled = true;
                    label15.Text = "UTR No:";
                    if (cmbpaymentmode.Text == "NEFT")
                        label13.Text = "NEFT Date:";
                    else
                        label13.Text = "RTGS Date:";
                }
                else
                {
                    cmbbank.Enabled = false;
                    txtchequeno.Enabled = false;
                    dtpchequedate.Enabled = false;
                }
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
            else if (keyData == Keys.F3)
            {
                saveupdate();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkProprietorname1.Checked == true)
                {
                    if (cmbProprietorname1.Text != "Select")
                    {
                        if (chktype.Checked == true)
                        {
                            if (cmbtype.Text != "Select")
                            {
                                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],ReceiptNo as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type] As 'Transaction Type',ProprietorName as 'Proprietor Name' ,NetAmount as 'Credit Amount',PaidAmount as 'Debit Amount',[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM ProprietorLedger where ProprietorId='" + cmbProprietorname1.SelectedValue + "' and Type='" + cmbtype.Text + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " order by CreateDate", db.Connection);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                GvreceiptInfo.DataSource = dt;
                                GvreceiptInfo.Refresh();
                                if (GvreceiptInfo.Rows.Count != 0)
                                {
                                    GvreceiptInfo.Columns["ID"].Visible = false;
                                    GvreceiptInfo.Columns["Mode"].Visible = false;
                                    try
                                    {
                                        ProprietorLedgerRepository remainingpayrepo = new ProprietorLedgerRepository();
                                        List<ProprietorLedger> remainingamt = remainingpayrepo.GetAll().Where(t => t.ProprietorId == Convert.ToInt32(cmbProprietorname1.SelectedValue) && t.Type == Convert.ToString(cmbtype.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                        var netamt = remainingpayrepo.GetAll().Where(t => t.ProprietorId == Convert.ToInt32(cmbProprietorname1.SelectedValue) && t.Type == Convert.ToString(cmbtype.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmount);
                                        var Paidamt = remainingpayrepo.GetAll().Where(t => t.ProprietorId == Convert.ToInt32(cmbProprietorname1.SelectedValue) && t.Type == Convert.ToString(cmbtype.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmount);
                                        if (remainingamt != null)
                                        {
                                            lblAmtText.Visible = true;
                                            lblRemAmt.Visible = true;
                                            if (Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt) >= 0)
                                            {
                                                lblAmtText.Text = "Rem Amt:";
                                                lblRemAmt.Text = Convert.ToString(Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt));
                                            }
                                            else
                                            {
                                                lblAmtText.Text = "Adv Amt:";
                                                lblRemAmt.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt)));
                                            }
                                        }
                                        remainingpayrepo.Dispose();
                                    }
                                    catch (Exception)
                                    { }
                                }
                                else
                                    MessageBox.Show("Record Not Found.", "Warning");
                                da.Dispose();
                                dt.Dispose();
                                this.ActiveControl = cmbtype;
                            }
                            else
                            {
                                MessageBox.Show("please Select Type.", "Warning");
                                this.ActiveControl = cmbtype;
                            }
                        }
                        else if (chkbetdate.Checked)
                        {
                            if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                            {
                                CommonMethod com = new CommonMethod();
                                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],ReceiptNo as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type] As 'Transaction Type',ProprietorName as 'Proprietor Name' ,NetAmount as 'Credit Amount',PaidAmount as 'Debit Amount',[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM ProprietorLedger where ProprietorId='" + cmbProprietorname1.SelectedValue + "' and CreateDate >= '" + com.ValidFromDate(dtpfromdate.Value.Date) + "' and  CreateDate <= '" + com.ValidToDate(dtptodate.Value.Date) + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " order by CreateDate", db.Connection);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                GvreceiptInfo.DataSource = dt;
                                GvreceiptInfo.Refresh();
                                if (GvreceiptInfo.Rows.Count != 0)
                                {
                                    GvreceiptInfo.Columns["ID"].Visible = false;
                                    GvreceiptInfo.Columns["Mode"].Visible = false;
                                    this.ActiveControl = cmbbank2;
                                }
                                else
                                {
                                    MessageBox.Show("Record Not Present.", "Warning");
                                    this.ActiveControl = cmbbank2;
                                }
                                da.Dispose();
                                dt.Dispose();
                            }
                            else
                            {
                                MessageBox.Show("To Date Should greater than from date.", "Warning");
                                this.ActiveControl = dtpfromdate;
                            }
                        }
                        else
                        {
                            SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],ReceiptNo as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type] As 'Transaction Type',ProprietorName as 'Proprietor Name' ,NetAmount as 'Credit Amount',PaidAmount as 'Debit Amount',[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM ProprietorLedger where ProprietorId='" + cmbProprietorname1.SelectedValue + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " order by CreateDate", db.Connection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            GvreceiptInfo.DataSource = dt;
                            GvreceiptInfo.Refresh();
                            if (GvreceiptInfo.Rows.Count != 0)
                            {
                                GvreceiptInfo.Columns["ID"].Visible = false;
                                GvreceiptInfo.Columns["Mode"].Visible = false;
                                try
                                {
                                    ProprietorLedgerRepository remainingpayrepo = new ProprietorLedgerRepository();
                                    List<ProprietorLedger> remainingamt = remainingpayrepo.GetAll().Where(t => t.ProprietorId == Convert.ToInt32(cmbProprietorname1.SelectedValue) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                    var netamt = remainingpayrepo.GetAll().Where(t => t.ProprietorId == Convert.ToInt32(cmbProprietorname1.SelectedValue) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmount);
                                    var Paidamt = remainingpayrepo.GetAll().Where(t => t.ProprietorId == Convert.ToInt32(cmbProprietorname1.SelectedValue) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmount);
                                    if (remainingamt != null)
                                    {
                                        lblAmtText.Visible = true;
                                        lblRemAmt.Visible = true;
                                        if (Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt) >= 0)
                                        {
                                            lblAmtText.Text = "Rem Amt:";
                                            lblRemAmt.Text = Convert.ToString(Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt));
                                        }
                                        else
                                        {
                                            lblAmtText.Text = "Adv Amt:";
                                            lblRemAmt.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt)));
                                        }
                                    }
                                    remainingpayrepo.Dispose();
                                }
                                catch (Exception)
                                { }
                                this.ActiveControl = cmbProprietorname1;
                            }
                            else
                            {
                                MessageBox.Show("Record Not Found.", "Warning");
                                this.ActiveControl = cmbProprietorname1;
                            }
                            da.Dispose();
                            dt.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("please Select Customer Name.", "Warning");
                        this.ActiveControl = cmbProprietorname1;
                    }
                }
                else if (chkbankname.Checked == true)
                {
                    if (cmbbank2.Text != "Select")
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],ReceiptNo as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type] As 'Transaction Type',ProprietorName as 'Proprietor Name' ,NetAmount as 'Credit Amount',PaidAmount as 'Debit Amount',[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM ProprietorLedger where Bankname='" + cmbbank2.Text + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " order by CreateDate", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvreceiptInfo.DataSource = dt;
                        GvreceiptInfo.Refresh();
                        if (GvreceiptInfo.Rows.Count != 0)
                        {
                            GvreceiptInfo.Columns["ID"].Visible = false;
                            GvreceiptInfo.Columns["Mode"].Visible = false;
                            this.ActiveControl = cmbbank2;
                        }
                        else
                        {
                            MessageBox.Show("Record Not Present.", "Warning");
                            this.ActiveControl = cmbbank2;
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("please Select Bank Name.", "Warning");
                        this.ActiveControl = cmbbank2;
                    }
                }
                else if (chkdate.Checked == true)
                {
                    if (chktype.Checked == true)
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],ReceiptNo as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type] As 'Transaction Type',ProprietorName as 'Proprietor Name' ,NetAmount as 'Credit Amount',PaidAmount as 'Debit Amount',[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM ProprietorLedger where Type='" + cmbtype.Text + "' and CONVERT(VARCHAR(24),CreateDate,103)='" + dtpdate.Value.ToString("dd/MM/yyyy") + "'  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " order by CreateDate", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvreceiptInfo.DataSource = dt;
                        GvreceiptInfo.Refresh();
                        if (GvreceiptInfo.Rows.Count != 0)
                        {
                            GvreceiptInfo.Columns["ID"].Visible = false;
                            GvreceiptInfo.Columns["Mode"].Visible = false;
                            try
                            {
                                ProprietorLedgerRepository remainingpayrepo = new ProprietorLedgerRepository();
                                List<ProprietorLedger> remainingamt = remainingpayrepo.GetAll().Where(t => t.Type == Convert.ToString(cmbtype.Text) && t.CreateDate.Value.ToString("dd/MM/yyyy") == dtpdate.Value.ToString("dd/MM/yyyy") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                var netamt = remainingpayrepo.GetAll().Where(t => t.Type == Convert.ToString(cmbtype.Text) && t.CreateDate.Value.ToString("dd/MM/yyyy") == dtpdate.Value.ToString("dd/MM/yyyy") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmount);
                                var Paidamt = remainingpayrepo.GetAll().Where(t => t.Type == Convert.ToString(cmbtype.Text) && t.CreateDate.Value.ToString("dd/MM/yyyy") == dtpdate.Value.ToString("dd/MM/yyyy") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmount);
                                if (remainingamt != null)
                                {
                                    lblAmtText.Visible = true;
                                    lblRemAmt.Visible = true;
                                    if (Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt) >= 0)
                                    {
                                        lblAmtText.Text = "Rem Amt:";
                                        lblRemAmt.Text = Convert.ToString(Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt));
                                    }
                                    else
                                    {
                                        lblAmtText.Text = "Adv Amt:";
                                        lblRemAmt.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt)));
                                    }
                                }
                                remainingpayrepo.Dispose();
                            }
                            catch (Exception)
                            { }
                        }
                        else
                            MessageBox.Show("Record Not Found.", "Warning");
                        da.Dispose();
                        dt.Dispose();
                        this.ActiveControl = cmbtype;
                    }
                    else
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],ReceiptNo as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type] As 'Transaction Type',ProprietorName as 'Proprietor Name' ,NetAmount as 'Credit Amount',PaidAmount as 'Debit Amount',[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM ProprietorLedger where  CONVERT(VARCHAR(24),CreateDate,103)='" + dtpdate.Value.ToString("dd/MM/yyyy") + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " order by CreateDate", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvreceiptInfo.DataSource = dt;
                        GvreceiptInfo.Refresh();
                        if (GvreceiptInfo.Rows.Count != 0)
                        {
                            GvreceiptInfo.Columns["ID"].Visible = false;
                            GvreceiptInfo.Columns["Mode"].Visible = false;
                            try
                            {
                                ProprietorLedgerRepository remainingpayrepo = new ProprietorLedgerRepository();
                                List<ProprietorLedger> remainingamt = remainingpayrepo.GetAll().Where(t => t.CreateDate.Value.ToString("dd/MM/yyyy") == dtpdate.Value.ToString("dd/MM/yyyy") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                var netamt = remainingpayrepo.GetAll().Where(t => t.CreateDate.Value.ToString("dd/MM/yyyy") == dtpdate.Value.ToString("dd/MM/yyyy") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmount);
                                var Paidamt = remainingpayrepo.GetAll().Where(t => t.CreateDate.Value.ToString("dd/MM/yyyy") == dtpdate.Value.ToString("dd/MM/yyyy") && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmount);
                                if (remainingamt != null)
                                {
                                    lblAmtText.Visible = true;
                                    lblRemAmt.Visible = true;
                                    if (Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt) >= 0)
                                    {
                                        lblAmtText.Text = "Rem Amt:";
                                        lblRemAmt.Text = Convert.ToString(Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt));
                                    }
                                    else
                                    {
                                        lblAmtText.Text = "Adv Amt:";
                                        lblRemAmt.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt)));
                                    }
                                }
                                remainingpayrepo.Dispose();
                            }
                            catch (Exception)
                            { }
                        }
                        else
                            MessageBox.Show("Record Not Found.", "Warning");
                        da.Dispose();
                        dt.Dispose();
                        this.ActiveControl = dtpdate;
                    }
                }
                else if (chktype.Checked == true)
                {
                    if (cmbtype.Text != "Select" && cmbtype.Text != "")
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],ReceiptNo as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type] As 'Transaction Type',ProprietorName as 'Proprietor Name' ,NetAmount as 'Credit Amount',PaidAmount as 'Debit Amount',[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM ProprietorLedger where Type='" + cmbtype.Text + "'  and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " order by CreateDate", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvreceiptInfo.DataSource = dt;
                        GvreceiptInfo.Refresh();
                        if (GvreceiptInfo.Rows.Count != 0)
                        {
                            GvreceiptInfo.Columns["ID"].Visible = false;
                            GvreceiptInfo.Columns["Mode"].Visible = false;
                            try
                            {
                                ProprietorLedgerRepository remainingpayrepo = new ProprietorLedgerRepository();
                                List<ProprietorLedger> remainingamt = remainingpayrepo.GetAll().Where(t => t.Type == Convert.ToString(cmbtype.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                var netamt = remainingpayrepo.GetAll().Where(t => t.Type == Convert.ToString(cmbtype.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmount);
                                var Paidamt = remainingpayrepo.GetAll().Where(t => t.Type == Convert.ToString(cmbtype.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmount);
                                if (remainingamt != null)
                                {
                                    lblAmtText.Visible = true;
                                    lblRemAmt.Visible = true;
                                    if (Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt) >= 0)
                                    {
                                        lblAmtText.Text = "Rem Amt:";
                                        lblRemAmt.Text = Convert.ToString(Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt));
                                    }
                                    else
                                    {
                                        lblAmtText.Text = "Adv Amt:";
                                        lblRemAmt.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt)));
                                    }
                                }
                                remainingpayrepo.Dispose();
                            }
                            catch (Exception)
                            { }
                        }
                        else
                            MessageBox.Show("Record Not Found.", "Warning");
                        da.Dispose();
                        dt.Dispose();
                        this.ActiveControl = cmbtype;
                    }
                    else
                    {
                        MessageBox.Show("Please Select Type.", "Warning");
                        this.ActiveControl = cmbtype;
                    }
                }
                else if (chkbetdate.Checked)
                {
                    if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                    {
                        CommonMethod com = new CommonMethod();
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],ReceiptNo as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type] As 'Transaction Type',ProprietorName as 'Proprietor Name' ,NetAmount as 'Credit Amount',PaidAmount as 'Debit Amount',[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM ProprietorLedger where CreateDate >= '" + com.ValidFromDate(dtpfromdate.Value.Date) + "' and  CreateDate <= '" + com.ValidToDate(dtptodate.Value.Date) + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " order by CreateDate", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        GvreceiptInfo.DataSource = dt;
                        GvreceiptInfo.Refresh();
                        if (GvreceiptInfo.Rows.Count != 0)
                        {
                            GvreceiptInfo.Columns["ID"].Visible = false;
                            GvreceiptInfo.Columns["Mode"].Visible = false;
                            this.ActiveControl = cmbbank2;
                        }
                        else
                        {
                            MessageBox.Show("Record Not Present.", "Warning");
                            this.ActiveControl = cmbbank2;
                        }
                        da.Dispose();
                        dt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("To Date Should greater than from date.", "Warning");
                        this.ActiveControl = dtpfromdate;
                    }
                }
                else
                    MessageBox.Show("Please Check Checkbox First.", "Warning");
            }
            catch (Exception)
            { }
        }

        private void btnprint1_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkProprietorname1.Checked == true && cmbProprietorname1.Text != "Select")
                {
                    RptProprietorLedger receiptrpt = new RptProprietorLedger(Convert.ToInt32(cmbProprietorname1.SelectedValue), cmbProprietorname1.Text);
                    receiptrpt.ShowDialog();
                    receiptrpt.Dispose();
                }
                else if (ProLedname1 != "")
                {
                    RptProprietorLedger receiptrpt = new RptProprietorLedger(ProLedname1, fdate1, tdate1);
                    receiptrpt.ShowDialog();
                    receiptrpt.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void GvreceiptInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[3].Value) == "OpeningBalance")
                    MessageBox.Show("Please Update this Data in Proprietor Information Form.", "Warning");
                else
                {
                    btndelete.Enabled = true;
                    receiptid = Convert.ToInt32(GvreceiptInfo.Rows[e.RowIndex].Cells[0].Value);
                    txtreceiptno.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[1].Value);
                    try
                    {
                        dtpreceiptDate.Value = Convert.ToDateTime(GvreceiptInfo.Rows[e.RowIndex].Cells[2].Value);
                    }
                    catch (Exception)
                    { }
                    cmbProprietor.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[4].Value);
                    cmbTransactiontype.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[3].Value);
                    if (cmbTransactiontype.Text == "Credit")
                        txtPaidAmt.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[5].Value);
                    else if (cmbTransactiontype.Text == "Debit")
                        txtPaidAmt.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[6].Value);
                    cmbpaymentmode.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[7].Value);
                    if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[7].Value) != "Cash")
                    {
                        txtchequeno.Text = Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells[9].Value);
                        try
                        {
                            dtpchequedate.Value = Convert.ToDateTime(GvreceiptInfo.Rows[e.RowIndex].Cells[10].Value);
                        }
                        catch (Exception)
                        { }
                        ProprietorLedgerRepository remRepo = new ProprietorLedgerRepository();
                        ProprietorLedger remdata = remRepo.GetById(receiptid);
                        BankStatementRepository BankStRepo = new BankStatementRepository();
                        BankStatement bdata = new BankStatement();
                        if (remdata.PaymentMode == "Cheque")
                        {
                            if (cmbTransactiontype.Text == "Credit")
                                bdata = BankStRepo.GetAll().Where(t => t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.ChequeNo && t.Deposits == remdata.NetAmount && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            else if (cmbTransactiontype.Text == "Debit")
                                bdata = BankStRepo.GetAll().Where(t => t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.ChequeNo && t.Withdrawals == remdata.PaidAmount && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        }
                        else
                        {
                            if (cmbTransactiontype.Text == "Credit")
                                bdata = BankStRepo.GetAll().Where(t => t.Bankname == remdata.BankName && t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.ChequeNo && t.Deposits == remdata.NetAmount && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            else if (cmbTransactiontype.Text == "Debit")
                                bdata = BankStRepo.GetAll().Where(t => t.Bankname == remdata.BankName && t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.ChequeNo && t.Withdrawals == remdata.PaidAmount && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        }
                        if (bdata != null)
                            cmbbank.Text = bdata.Bankname;
                    }
                    else
                    {
                        txtchequeno.Text = "";
                        cmbbank.Text = "Select";
                        dtpchequedate.Enabled = false;
                    }
                    btndelete.Enabled = true;
                    btnSave.Text = "Update";
                }
            }
            catch (Exception)
            { }
        }

        private void chkbankname_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkbankname.Checked == true)
                {
                    chkProprietorname1.Checked = false;
                    chktype.Checked = false;
                    chkdate.Checked = false;
                    chkbetdate.Checked = false;
                    ProprietorData1();
                }
            }
            catch (Exception)
            { }
        }

        private void chkcustomername1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkProprietorname1.Checked == true)
                {
                    chkbankname.Checked = false;
                    chkdate.Checked = false;
                    chktype.Checked = false;
                    chkbetdate.Checked = false;
                    fillcombo();
                    fillcombo1();
                }
            }
            catch (Exception)
            { }
        }

        private void cmbbank2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void cmbtype_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chkdate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkdate.Checked == true)
                {
                    chkbankname.Checked = false;
                    chkProprietorname1.Checked = false;
                    chkbetdate.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void chktype_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkdate.Checked == true)
                {
                    chkbankname.Checked = false;
                    chkProprietorname1.Checked = false;
                    chkbetdate.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void chkbetdate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkbetdate.Checked == true)
                {
                    chkbankname.Checked = false;
                    chkdate.Checked = false;
                    chktype.Checked = false;
                    fillcombo();
                    fillcombo1();
                }
            }
            catch (Exception)
            { }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                ProprietorLedgerRepository remRepo = new ProprietorLedgerRepository();
                ProprietorLedger remdata = remRepo.GetById(receiptid);
                remRepo.Remove(remdata);
                remRepo.Save();
                if (remdata.PaymentMode != "Cash")
                {
                    BankStatementRepository Brepo = new BankStatementRepository();
                    BankStatement bdata = new BankStatement();
                    if (remdata.PaymentMode == "Cheque")
                    {
                        if (remdata.Type == "Credit")
                            bdata = Brepo.GetAll().Where(t => t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.ChequeNo && t.Deposits == remdata.PaidAmount && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        else if (remdata.Type == "Debit")
                            bdata = Brepo.GetAll().Where(t => t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.ChequeNo && t.Withdrawals == remdata.PaidAmount && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    }
                    else
                    {
                        if (remdata.Type == "Credit")
                            bdata = Brepo.GetAll().Where(t => t.Bankname == remdata.BankName && t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.ChequeNo && t.Deposits == remdata.PaidAmount && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (remdata.Type == "Debit")
                            bdata = Brepo.GetAll().Where(t => t.Bankname == remdata.BankName && t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.ChequeNo && t.Withdrawals == remdata.PaidAmount && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    }
                    if (bdata != null)
                    {
                        BankStatement bsdata = Brepo.GetById(bdata.Id);
                        Brepo.Remove(bsdata);
                        Brepo.Save();
                    }
                }
                MessageBox.Show("Record Deleted SuccessFully.", "Success");
                clear();
                btnsearch_Click(null, null);
                this.ActiveControl = btnnew;
            }
            catch (Exception)
            { }
        }

        private void dtpreceiptDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbProprietor;
            }
            catch (Exception)
            { }
        }

        private void cmbcustomer_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbTransactiontype;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        FrmProprietorMaster proprietor = new FrmProprietorMaster();
                        proprietor.ShowDialog();
                        ProprietorData();
                        this.ActiveControl = cmbProprietor;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtPaidAmt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtparticular;
            }
            catch (Exception)
            { }
        }

        private void cmbpaymentmode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (cmbpaymentmode.Text == "Cash")
                        this.ActiveControl = btnSave;
                    else
                        this.ActiveControl = cmbbank;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbbank_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpchequedate;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmBankInformation bank = new frmBankInformation();
                        bank.ShowDialog();
                        fillcombo();
                        this.ActiveControl = cmbbank;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtchequeno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSave;
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

        private void chktype_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbtype;
            }
            catch (Exception)
            { }
        }

        private void chkdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpdate;
            }
            catch (Exception)
            { }
        }

        private void chkbankname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbbank2;
            }
            catch (Exception)
            { }
        }

        private void chkcustomername1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbProprietorname1;
            }
            catch (Exception)
            { }
        }

        private void chkbetdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpfromdate;
            }
            catch (Exception)
            { }
        }

        private void cmbTransactiontype_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtPaidAmt;
            }
            catch (Exception)
            { }
        }

        private void txtparticular_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbpaymentmode;
            }
            catch (Exception)
            { }
        }

        private void cmbProprietorname1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void txtPaidAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtPaidAmt.Text == "")
                {
                    txtPaidAmt.Text = "0";
                    this.ActiveControl = txtPaidAmt;
                }
            }
            catch (Exception)
            { }
        }

        public void BalShtToProLedger(string proLedName, DateTime fdate, DateTime tdate)
        {
            try
            {
                CommonMethod com = new CommonMethod();
                SqlDataAdapter da = new SqlDataAdapter("SELECT [ID],ReceiptNo as 'Receipt No.' ,CONVERT(VARCHAR(24),[CreateDate ],103) as 'Receipt Date',[Type] As 'Transaction Type',ProprietorName as 'Proprietor Name' ,NetAmount as 'Credit Amount',PaidAmount as 'Debit Amount',[Paymentmode] as 'Mode',[bankname] as 'Bank Name',[ChequeNo] as 'Chq./UTR No.',CONVERT(VARCHAR(24),[ChequeDate],103) as 'Chq./NEFT Date'  FROM ProprietorLedger where ProprietorName='" + proLedName + "' and CreateDate >= '" + com.ValidFromDate(fdate.Date) + "' and  CreateDate <= '" + com.ValidToDate(tdate.Date) + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " order by CreateDate", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvreceiptInfo.DataSource = dt;
                GvreceiptInfo.Refresh();
                if (GvreceiptInfo.Rows.Count != 0)
                {
                    GvreceiptInfo.Columns["ID"].Visible = false;
                    GvreceiptInfo.Columns["Mode"].Visible = false;
                    try
                    {
                        ProprietorLedgerRepository remainingpayrepo = new ProprietorLedgerRepository();
                        List<ProprietorLedger> remainingamt = remainingpayrepo.GetAll().Where(t => t.ProprietorName == proLedName && t.CreateDate.Value.Date >= fdate.Date && t.CreateDate.Value.Date <= tdate.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        var netamt = remainingpayrepo.GetAll().Where(t => t.ProprietorName == proLedName && t.CreateDate.Value.Date >= fdate.Date && t.CreateDate.Value.Date <= tdate.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmount);
                        var Paidamt = remainingpayrepo.GetAll().Where(t => t.ProprietorName == proLedName && t.CreateDate.Value.Date >= fdate.Date && t.CreateDate.Value.Date <= tdate.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmount);
                        if (remainingamt != null)
                        {
                            lblAmtText.Visible = true;
                            lblRemAmt.Visible = true;
                            if (Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt) >= 0)
                            {
                                lblAmtText.Text = "Rem Amt:";
                                lblRemAmt.Text = Convert.ToString(Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt));
                            }
                            else
                            {
                                lblAmtText.Text = "Adv Amt:";
                                lblRemAmt.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(netamt) - Convert.ToDecimal(Paidamt)));
                            }
                        }
                        remainingpayrepo.Dispose();
                    }
                    catch (Exception)
                    { }
                }
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception) { }
        }

        private void btnProprietorForm_Click(object sender, EventArgs e)
        {
            try
            {
                FrmProprietorMaster pmast = new FrmProprietorMaster();
                pmast.ShowDialog();
                ProprietorData();
                ProprietorData1();
            }
            catch (Exception)
            { }
        }
    }
}