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
    public partial class frmBankStatement : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        public int bankid, Bankstatid;
        public string maxsuppreceiptno, maxcustreceiptno, maxexpenceno, transType, AccountType1 = "";
        public DateTime sdate, fdate1, tdate1;
        SaveFileDialog saveFileDialog = new SaveFileDialog();

        public frmBankStatement()
        {
            InitializeComponent();
            sdate = CommonMethod.startdate;
            BindAcGroup();
            btnSave.Enabled = true;
            btndelete.Enabled = false;
            txtchequeno.Enabled = false;
            cm.changedatetimepickerrange(this);
            this.ActiveControl = cmbACGroups;
        }

        public frmBankStatement(DateTime FromDate, DateTime ToDate, String TransType)
        {
            InitializeComponent();
            btnSave.Enabled = false;
            btndelete.Enabled = false;
            txtchequeno.Enabled = false;
            btnaddopening.Enabled = false;
            btnnew.Enabled = false;
            cm.changedatetimepickerrange(this);
            sdate = CommonMethod.startdate;
            transType = TransType;
            fdate1 = FromDate;
            tdate1 = ToDate;
            LoadData(FromDate, ToDate, TransType);
        }

        public frmBankStatement(string AccountType, DateTime fdate, DateTime tdate)
        {
            InitializeComponent();
            btnSave.Enabled = false;
            btndelete.Enabled = false;
            txtchequeno.Enabled = false;
            btnaddopening.Enabled = false;
            btnnew.Enabled = false;
            cm.changedatetimepickerrange(this);
            sdate = CommonMethod.startdate;
            AccountType1 = AccountType;
            fdate1 = fdate;
            tdate1 = tdate;
            BalSheetToBankSt(AccountType, fdate, tdate);
        }

        public void BindAcGroup()
        {
            try
            {
                AccountingGroupMasterRepository ACGRepo = new AccountingGroupMasterRepository();
                List<AccountingGroupMaster> ACGdata = new List<AccountingGroupMaster>();
                ACGdata.Clear();
                ACGdata = ACGRepo.GetAll().OrderByDescending(t => t.Id).ToList();
                ACGdata.Add(new AccountingGroupMaster { ACGName = "Select", Id = 0 });
                ACGdata.Reverse();
                cmbACGroups.DataSource = ACGdata;
                cmbACGroups.ValueMember = "Id";
                cmbACGroups.DisplayMember = "ACGName";
                ACGRepo.Dispose();
                cmbACGroups.SelectedIndex = 1;
            }
            catch (Exception)
            { }
        }

        public void fillcombo(int ACGId)
        {
            try
            {
                BankRepository BankRepo = new BankRepository();
                List<BankInformation> bankdata = new List<BankInformation>();
                bankdata.Clear();
                bankdata = BankRepo.GetAll().Where(t => t.ACGId == ACGId && t.CompId == CommonMethod.CompId).OrderByDescending(t => t.Bankname).ToList();
                bankdata.Add(new BankInformation { Bankname = "Select", BankId = 0 });
                bankdata.Reverse();
                cmbbankname.DataSource = bankdata;
                cmbbankname.ValueMember = "BankId";
                cmbbankname.DisplayMember = "Bankname";
                BankRepo.Dispose();
                cmbbankname.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void fillcombo1(int ACGId)
        {
            try
            {
                BankRepository BankRepo = new BankRepository();
                List<BankInformation> bankdata = new List<BankInformation>();
                bankdata.Clear();
                bankdata = BankRepo.GetAll().Where(t => t.ACGId == ACGId && t.CompId == CommonMethod.CompId).OrderByDescending(t => t.Bankname).ToList();
                bankdata.Add(new BankInformation { Bankname = "Select", BankId = 0 });
                bankdata.Reverse();
                cmbsbankname1.DataSource = bankdata;
                cmbsbankname1.ValueMember = "BankId";
                cmbsbankname1.DisplayMember = "Bankname";
                BankRepo.Dispose();
                cmbsbankname1.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                txtparticular.Text = "";
                txtchequeno.Text = "";
                txtchequeno.Enabled = false;
                txtAmount.Text = "";
                cmbtransaction.Text = "Select";
                chkcheque.Checked = false;
                cmbtransaction.SelectedIndex = -1;
                btnSave.Text = "Save";
                cmbtransaction.Enabled = true;
                btndelete.Enabled = false;
                this.ActiveControl = cmbACGroups;
                try
                {
                    dtpdate.Value = DateTime.Now.Date;
                }
                catch (Exception)
                { }
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkbankname.Checked)
                {
                    if (cmbsbankname1.Text != "Select")
                    {
                        if (chkfromdate.Checked)
                        {
                            if (dtpfrom.Value.Date <= dtpto.Value.Date)
                            {
                                RptBankStatement bankstrpt = new RptBankStatement(Convert.ToInt32(cmbsbankname1.SelectedValue), cmbsbankname1.Text, dtpfrom.Value, dtpto.Value);
                                bankstrpt.ShowDialog();
                                bankstrpt.Dispose();
                            }
                            else
                            {
                                MessageBox.Show("To Date Should greater than or equal to from date.", "Warning");
                                this.ActiveControl = dtpto;
                            }
                        }
                        else
                        {
                            RptBankStatement bankstrpt = new RptBankStatement(Convert.ToInt32(cmbsbankname1.SelectedValue), cmbsbankname1.Text);
                            bankstrpt.ShowDialog();
                            bankstrpt.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select Bank Name.", "Warning");
                        this.ActiveControl = cmbsbankname1;
                    }
                }
                else if (transType != "")
                {
                    RptBankStatement bankstrpt = new RptBankStatement(transType, fdate1, tdate1);
                    bankstrpt.ShowDialog();
                    bankstrpt.Dispose();
                }
                else if (AccountType1 != "")
                {
                    if (AccountType1 == "Bank Account")
                    {
                        RptBankStatement bankstrpt = new RptBankStatement(fdate1, tdate1, AccountType1);
                        bankstrpt.ShowDialog();
                        bankstrpt.Dispose();
                    }
                    else
                    {
                        RptBankStatement bankstrpt = new RptBankStatement(fdate1, tdate1, AccountType1);
                        bankstrpt.ShowDialog();
                        bankstrpt.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("Please Check CheckBox.", "Warning");
                    this.ActiveControl = cmbsbankname1;
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
            if (keyData == Keys.F3)
            {
                btnSave_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbbankname.Text != "Select" && txtparticular.Text.Trim() != "" && cmbtransaction.Text != "" && cmbtransaction.Text != "Select" && txtAmount.Text.Trim() != "")
                {
                    if ((cmbtransaction.Text == "Receipt" || cmbtransaction.Text == "Payment" || cmbtransaction.Text == "Expence") && cmbcustorsupp.Text == "Select")
                    {
                        if (cmbcustorsupp.Text == "Select")
                        {
                            if (cmbtransaction.Text == "Expence")
                            {
                                MessageBox.Show("Select Expence Name.", "Warning");
                                this.ActiveControl = cmbcustorsupp;
                                goto a;
                            }
                            else
                            {
                                MessageBox.Show("Select Party Name.", "Warning");
                                this.ActiveControl = cmbcustorsupp;
                                goto a;
                            }
                        }
                    }
                    else
                    {
                        int Id;
                        if (btnSave.Text == "Save")
                        {
                            BankStatementRepository bankrepo = new BankStatementRepository();
                            BankStatement bankstdata = new BankStatement();
                            bankstdata.BankId = Convert.ToInt32(cmbbankname.SelectedValue);
                            bankstdata.Bankname = Convert.ToString(cmbbankname.Text);
                            bankstdata.TransactionDate = dtpdate.Value;
                            bankstdata.Particulars = Convert.ToString(txtparticular.Text.Trim());
                            if (chkcheque.Checked)
                                bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                            else
                                bankstdata.ChequeNo = Convert.ToString("");
                            bankstdata.TransactionType = cmbtransaction.Text;
                            bankstdata.ACGId = Convert.ToInt32(cmbACGroups.SelectedValue);
                            if (cmbtransaction.Text == "Withdrawals" || cmbtransaction.Text == "Payment" || cmbtransaction.Text == "Expence")
                            {
                                if ((cmbtransaction.Text == "Payment" || cmbtransaction.Text == "Expence") && txtchequeno.Text.Trim() == "")
                                {
                                    if (txtchequeno.Text.Trim() == "")
                                    {
                                        MessageBox.Show("Enter Cheque Or UTR Number.", "Warning");
                                        this.ActiveControl = txtchequeno;
                                        goto a;
                                    }
                                }
                                else
                                {
                                    bankstdata.Withdrawals = Convert.ToDecimal(txtAmount.Text);
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankrepo.Add(bankstdata);
                                    bankrepo.Save();
                                    if (cmbtransaction.Text == "Payment")
                                    {
                                        SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                                        SupplierRemainingPayment remainingdata = new SupplierRemainingPayment();
                                        maxsuppid();
                                        remainingdata.BillReceiptNo = Convert.ToInt32(maxsuppreceiptno);
                                        remainingdata.SupplierId = Convert.ToInt32(cmbcustorsupp.SelectedValue);
                                        remainingdata.Suppliername = cmbcustorsupp.Text;
                                        remainingdata.CreateDate = dtpdate.Value;
                                        remainingdata.Type = "Receipt";
                                        remainingdata.RemainingAmt = Convert.ToDecimal(0);
                                        remainingdata.NetAmt = Convert.ToDecimal(0);
                                        remainingdata.PaidAmt = Convert.ToDecimal(txtAmount.Text);
                                        remainingdata.TransactionYear = CommonMethod.TransactionYear;
                                        remainingdata.CompId = CommonMethod.CompId;
                                        remainingdata.ReceiptBy = CommonMethod.CompId;
                                        if (chkcheque.Checked)
                                            remainingdata.PaymentMode = "Cheque";
                                        remainingdata.Bankname = cmbbankname.Text;
                                        remainingdata.Chequeno = txtchequeno.Text;
                                        remainingdata.Chequedate = null;
                                        remainingpayrepo.Add(remainingdata);
                                        remainingpayrepo.Save();
                                        Id = remainingdata.Id;
                                        decimal Rcptpaidamt = Convert.ToDecimal(txtAmount.Text);
                                        decimal billamt = 0, remamt = 0;
                                        DataTable dt = new DataTable();
                                        try
                                        {
                                            db.connect();
                                            SqlCommand command = new SqlCommand("SPBillNoForLedger", db.Connection);
                                            command.CommandType = CommandType.StoredProcedure;

                                            command.Parameters.Add("@Ledger", SqlDbType.VarChar).Value = 'P';
                                            command.Parameters.Add("@Id", SqlDbType.Int).Value = Convert.ToInt32(cmbcustorsupp.SelectedValue);
                                            command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;

                                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                                            adapter.Fill(dt);
                                            db.CloseConnection();
                                        }
                                        catch (Exception)
                                        { db.CloseConnection(); }
                                        if (dt.Rows.Count > 0)
                                        {
                                            SupplierRemainingPaymentRepository suprempayRepo = new SupplierRemainingPaymentRepository();
                                            SupplierRemainingPayment suprempayData = new SupplierRemainingPayment();
                                            suprempayData = suprempayRepo.GetAll().Where(t => t.Id < Id && t.SupplierId == Convert.ToInt32(cmbcustorsupp.SelectedValue) && t.Type == "Receipt" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                            if (suprempayData != null)
                                            {
                                                if (suprempayData.RemainingAmt < 0)
                                                    Rcptpaidamt = Rcptpaidamt + Math.Abs(Convert.ToDecimal(suprempayData.RemainingAmt));
                                            }
                                            for (int i = 0; i < dt.Rows.Count; i++)
                                            {
                                                if (Rcptpaidamt > 0)
                                                {
                                                    if (dt.Rows[i][0].ToString() != "0")
                                                    {
                                                        BillWiseSupplierReceiptRepository BWSRRepo = new BillWiseSupplierReceiptRepository();
                                                        BillWiseSupplierReceipt BWSRData = new BillWiseSupplierReceipt();
                                                        BWSRData = BWSRRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustorsupp.SelectedValue) && t.BillId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                                        if (BWSRData != null)
                                                            billamt = Convert.ToDecimal(BWSRData.RemainigAmount);
                                                        else
                                                        {
                                                            SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                                                            SupplierRemainingPayment remdata = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustorsupp.SelectedValue) && t.BillId == Convert.ToInt32(dt.Rows[i][0].ToString()) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                                            billamt = Convert.ToDecimal(remdata.NetAmt) - Convert.ToDecimal(remdata.PaidAmt);
                                                            remainingpayrepo1.Dispose();
                                                        }
                                                        BWSRRepo.Dispose();
                                                    }
                                                    else
                                                    {
                                                        BillWiseSupplierReceiptRepository BWSRRepo = new BillWiseSupplierReceiptRepository();
                                                        BillWiseSupplierReceipt BWSRData = new BillWiseSupplierReceipt();
                                                        BWSRData = BWSRRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustorsupp.SelectedValue) && t.BillId == Convert.ToInt32(0) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                                        if (BWSRData != null)
                                                            billamt = Convert.ToDecimal(BWSRData.RemainigAmount);
                                                        else
                                                        {
                                                            SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                                                            SupplierRemainingPayment remainingamt = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbcustorsupp.SelectedValue) && (t.Type == "OpeningBalance" || t.BillId == 0) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                                            billamt = Convert.ToDecimal(remainingamt.NetAmt) - Convert.ToDecimal(remainingamt.PaidAmt);
                                                            remainingpayrepo1.Dispose();
                                                        }
                                                        BWSRRepo.Dispose();
                                                    }
                                                    BillWiseSupplierReceiptRepository BWSRRepo1 = new BillWiseSupplierReceiptRepository();
                                                    BillWiseSupplierReceipt BWSRData1 = new BillWiseSupplierReceipt();
                                                    BWSRData1.SupplierReceiptNo = Convert.ToInt32(remainingdata.Id);
                                                    BWSRData1.SupplierId = Convert.ToInt32(cmbcustorsupp.SelectedValue);
                                                    BWSRData1.BillId = Convert.ToInt32(dt.Rows[i][0].ToString());
                                                    if (Rcptpaidamt >= billamt)
                                                    {
                                                        Rcptpaidamt = Rcptpaidamt - billamt;
                                                        BWSRData1.PaidAmount = Convert.ToDecimal(billamt);
                                                        BWSRData1.RemainigAmount = Convert.ToDecimal(0);
                                                    }
                                                    else
                                                    {
                                                        BWSRData1.PaidAmount = Convert.ToDecimal(Rcptpaidamt);
                                                        remamt = billamt - Rcptpaidamt;
                                                        Rcptpaidamt = 0;
                                                        BWSRData1.RemainigAmount = Convert.ToDecimal(remamt);
                                                    }
                                                    BWSRData1.TransactionYear = CommonMethod.TransactionYear;
                                                    BWSRData1.CompId = CommonMethod.CompId;
                                                    BWSRRepo1.Add(BWSRData1);
                                                    BWSRRepo1.Save();
                                                }
                                                else
                                                    break;
                                            }
                                        }
                                    }
                                    if (cmbtransaction.Text == "Expence")
                                    {
                                        ExpenceRepository expencerepo = new ExpenceRepository();
                                        Expence expencedata = new Expence();
                                        maxexpencid();
                                        ExpenceMasterRepository emastRepo = new ExpenceMasterRepository();
                                        var emastdata = emastRepo.GetAll().Where(t => t.ExpenceName == cmbcustorsupp.Text).FirstOrDefault();
                                        if (!string.IsNullOrEmpty(Convert.ToString(emastdata)))
                                            expencedata.ACGId = emastdata.ACGId;
                                        expencedata.ExpenceID = Convert.ToInt32(maxexpenceno);
                                        expencedata.ExpenceDate = Convert.ToDateTime(dtpdate.Value);
                                        expencedata.ExpenceName = cmbcustorsupp.Text;
                                        expencedata.FromPerson = "";
                                        expencedata.ToPerson = "";
                                        expencedata.Reason = txtparticular.Text.Trim();
                                        expencedata.PaidAmount = Convert.ToDouble(txtAmount.Text);
                                        expencedata.TransactionYear = CommonMethod.TransactionYear;
                                        expencedata.CompId = CommonMethod.CompId;
                                        if (chkcheque.Checked)
                                            expencedata.PaymentMode = "Cheque";
                                        expencedata.Bankname = cmbbankname.Text;
                                        expencedata.ChequeNo = txtchequeno.Text;
                                        expencedata.ChequeDate = null;
                                        expencerepo.Add(expencedata);
                                        expencerepo.Save();
                                    }
                                }
                            }
                            else if (cmbtransaction.Text == "Deposits" || cmbtransaction.Text == "Receipt")
                            {
                                if (cmbtransaction.Text == "Receipt" && txtchequeno.Text.Trim() == "")
                                {
                                    if (txtchequeno.Text.Trim() == "")
                                    {
                                        MessageBox.Show("Enter Cheque Or UTR Number.", "Warning");
                                        this.ActiveControl = txtchequeno;
                                        goto a;
                                    }
                                }
                                else
                                {
                                    bankstdata.Deposits = Convert.ToDecimal(txtAmount.Text);
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankrepo.Add(bankstdata);
                                    bankrepo.Save();
                                    if (cmbtransaction.Text == "Receipt")
                                    {
                                        RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                                        RemainingPayment remainingdata = new RemainingPayment();
                                        Maxcustid();
                                        remainingdata.BillReceiptNo = Convert.ToInt32(maxcustreceiptno);
                                        remainingdata.CustomerId = Convert.ToInt32(cmbcustorsupp.SelectedValue);
                                        remainingdata.Customername = cmbcustorsupp.Text;
                                        remainingdata.CreateDate = dtpdate.Value;
                                        remainingdata.Type = "Receipt";
                                        remainingdata.RemainingAmt = Convert.ToDecimal(0);
                                        remainingdata.NetAmt = Convert.ToDecimal(0);
                                        remainingdata.PaidAmt = Convert.ToDecimal(txtAmount.Text);
                                        remainingdata.TransactionYear = CommonMethod.TransactionYear;
                                        remainingdata.CompId = CommonMethod.CompId;
                                        if (chkcheque.Checked)
                                            remainingdata.PaymentMode = "Cheque";
                                        remainingdata.Bankname = cmbbankname.Text;
                                        remainingdata.Chequeno = txtchequeno.Text;
                                        remainingdata.Chequedate = null;
                                        remainingpayrepo.Add(remainingdata);
                                        remainingpayrepo.Save();
                                    }
                                }
                            }
                            MessageBox.Show("Record Saved Sucessfully.", "Success");
                            clear();
                            this.ActiveControl = btnnew;
                            bankrepo.Dispose();
                        }
                        else
                        {
                            BankStatementRepository bankrepo = new BankStatementRepository();
                            BankStatement bankstdata = new BankStatement();
                            bankstdata.BankId = Convert.ToInt32(cmbbankname.SelectedValue);
                            bankstdata.Bankname = Convert.ToString(cmbbankname.Text);
                            bankstdata.TransactionDate = dtpdate.Value;
                            bankstdata.Particulars = Convert.ToString(txtparticular.Text.Trim());
                            if (chkcheque.Checked)
                                bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                            else
                                bankstdata.ChequeNo = Convert.ToString("");
                            bankstdata.TransactionType = cmbtransaction.Text;
                            bankstdata.ACGId = Convert.ToInt32(cmbACGroups.SelectedValue);
                            if (cmbtransaction.Text == "Withdrawals")
                            {
                                bankstdata.Withdrawals = Convert.ToDecimal(txtAmount.Text);
                                bankstdata.Balance = 0;
                                bankstdata.CompId = CommonMethod.CompId;
                                bankstdata.Id = Bankstatid;
                                bankrepo.Edit(bankstdata);
                                bankrepo.Save();
                            }
                            else if (cmbtransaction.Text == "Deposits")
                            {
                                bankstdata.Deposits = Convert.ToDecimal(txtAmount.Text);
                                bankstdata.Balance = 0;
                                bankstdata.CompId = CommonMethod.CompId;
                                bankstdata.Id = Bankstatid;
                                bankrepo.Edit(bankstdata);
                                bankrepo.Save();
                            }
                            MessageBox.Show("Record Update Sucessfully.", "Success");
                            clear();
                            this.ActiveControl = btnnew;
                            bankrepo.Dispose();
                        }
                    }
                a:
                    { }
                }
                else
                {
                    if (cmbbankname.Text == "" || cmbbankname.Text == "Select")
                    {
                        MessageBox.Show("Select Bank Name.", "Warning");
                        this.ActiveControl = cmbbankname;
                    }
                    else if (txtparticular.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter Particular.", "Warning");
                        this.ActiveControl = txtparticular;
                    }
                    else if (cmbtransaction.Text == "" || cmbtransaction.Text == "Select")
                    {
                        MessageBox.Show("Select Transaction.", "Warning");
                        this.ActiveControl = cmbtransaction;
                    }
                    else
                    {
                        MessageBox.Show("Enter Amount.", "Warning");
                        this.ActiveControl = txtAmount;
                    }
                }
            }
            catch (Exception)
            { }
        }

        public void maxexpencid()
        {
            try
            {
                DataTable dt = db.GetTable("Select max(ExpenceID) from Expences WHERE TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                if (dt.Rows.Count > 0)
                {
                    string maxid = dt.Rows[0]["ExpenceID"].ToString();
                    if (maxid == "")
                        maxexpenceno = "1";
                    else
                        maxexpenceno = Convert.ToString(Convert.ToInt32(Convert.ToInt32(maxid) + 1));
                }
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        public void maxsuppid()
        {
            try
            {
                SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                var mid = remainingpayrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.BillReceiptNo);
                if (mid == null)
                    maxsuppreceiptno = "1";
                else
                    maxsuppreceiptno = Convert.ToString(Convert.ToInt32(mid) + 1);
                remainingpayrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void Maxcustid()
        {
            try
            {
                RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                var mid = remainingpayrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.BillReceiptNo);
                if (mid == null)
                    maxcustreceiptno = "1";
                else
                    maxcustreceiptno = Convert.ToString(Convert.ToInt32(mid) + 1);
                remainingpayrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void chkcheque_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkcheque.Checked == true)
                    txtchequeno.Enabled = true;
                else
                {
                    txtchequeno.Enabled = false;
                    txtchequeno.Text = "";
                }
            }
            catch (Exception)
            { }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkbankname.Checked)
                {
                    if (cmbsbankname1.Text != "Select")
                    {
                        BankRepository brepo = new BankRepository();
                        int Gid = Convert.ToInt32(brepo.GetAll().Where(t => t.Bankname == cmbsbankname1.Text && t.BankId == Convert.ToInt32(cmbsbankname1.SelectedValue) && t.CompId == CommonMethod.CompId).Sum(t => t.ACGId));

                        if (chkfromdate.Checked)
                        {
                            if (dtpfrom.Value.Date <= dtpto.Value.Date)
                            {
                                label14.Visible = true;
                                label15.Visible = true;
                                DateTime opdate = dtpfrom.Value.AddDays(-1);

                                string tilldate = cm.ValidToDate(opdate);
                                SqlDataAdapter da1 = new SqlDataAdapter("Select Id,CONVERT(VARCHAR(23),TransactionDate,103) as Date,Particulars,ChequeNo as 'Chq./UTR No.',TransactionType,Withdrawals,Deposits from BankStatement where BankId='" + cmbsbankname1.SelectedValue + "' and CompId='" + CommonMethod.CompId + "'  and TransactionDate>='" + cm.ValidFromDate(sdate) + "' and TransactionDate<='" + tilldate + "'", db.Connection);
                                DataTable dt1 = new DataTable();
                                da1.Fill(dt1);
                                decimal sumObject2 = 0, sumObject3 = 0;
                                try
                                {
                                    sumObject2 = Convert.ToDecimal(dt1.Compute("Sum(Withdrawals)", ""));
                                }
                                catch (Exception)
                                { }
                                try
                                {
                                    sumObject3 = Convert.ToDecimal(dt1.Compute("Sum(Deposits)", ""));
                                }
                                catch (Exception)
                                { }
                                label15.Text = Convert.ToString(Convert.ToDecimal(sumObject3) - Convert.ToDecimal(sumObject2));

                                string toDate = cm.ValidToDate(dtpto.Value.Date), fromDate = cm.ValidFromDate(dtpfrom.Value.Date);

                                SqlDataAdapter da = new SqlDataAdapter("Select Id,CONVERT(VARCHAR(23),TransactionDate,103) as Date,Particulars,ChequeNo as 'Chq./UTR No.',TransactionType,Withdrawals,Deposits from BankStatement where BankId='" + cmbsbankname1.SelectedValue + "' and CompId='" + CommonMethod.CompId + "'  and TransactionDate>='" + fromDate + "' and TransactionDate<='" + toDate + "'", db.Connection);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                decimal sumObject = 0, sumObject1 = 0;
                                try
                                {
                                    sumObject = Convert.ToDecimal(dt.Compute("Sum(Withdrawals)", ""));
                                }
                                catch (Exception)
                                { }
                                try
                                {
                                    sumObject1 = Convert.ToDecimal(dt.Compute("Sum(Deposits)", ""));
                                }
                                catch (Exception)
                                { }
                                label12.Text = Convert.ToString(Convert.ToDecimal(label15.Text) + Convert.ToDecimal(sumObject1) - Convert.ToDecimal(sumObject));
                                GvreceiptInfo.DataSource = dt;
                                GvreceiptInfo.Refresh();
                                GvreceiptInfo.Columns["ID"].Visible = false;
                                GvreceiptInfo.Columns["Date"].Width = 80;
                                GvreceiptInfo.Columns["Particulars"].Width = 200;
                                GvreceiptInfo.Columns["ChequeNo"].Width = 80;
                                GvreceiptInfo.Columns["Withdrawals"].Width = 95;
                                GvreceiptInfo.Columns["Deposits"].Width = 95;
                                GvreceiptInfo.Columns["TransactionType"].Width = 110;
                                da.Dispose();
                                dt.Dispose();
                            }
                            else
                            {
                                MessageBox.Show("To Date Should greater than or equal to from date.", "Warning");
                                this.ActiveControl = dtpto;
                            }
                        }
                        else
                        {
                            string lastYearDate = "";
                            lastYearDate = cm.ValidToDate(CommonMethod.enddate);
                            label14.Visible = false;
                            label15.Visible = false;
                            SqlDataAdapter da = new SqlDataAdapter("Select Id,CONVERT(VARCHAR(23),TransactionDate,103) as Date,Particulars,ChequeNo as 'Chq./UTR No.',TransactionType,Withdrawals,Deposits from BankStatement where BankId='" + cmbsbankname1.SelectedValue + "' and CompId='" + CommonMethod.CompId + "' and TransactionDate>='" + cm.ValidFromDate(sdate) + "' and TransactionDate<='" + lastYearDate + "' ", db.Connection);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            decimal sumObject = 0, sumObject1 = 0;
                            try
                            {
                                sumObject = Convert.ToDecimal(dt.Compute("Sum(Withdrawals)", ""));
                            }
                            catch (Exception)
                            { }
                            try
                            {
                                sumObject1 = Convert.ToDecimal(dt.Compute("Sum(Deposits)", ""));
                            }
                            catch (Exception)
                            { }
                            if (Gid == 3)
                                label12.Text = Convert.ToString(Convert.ToDecimal(sumObject) - Convert.ToDecimal(sumObject1));
                            else
                                label12.Text = Convert.ToString(Convert.ToDecimal(sumObject1) - Convert.ToDecimal(sumObject));
                            GvreceiptInfo.DataSource = dt;
                            GvreceiptInfo.Refresh();
                            GvreceiptInfo.Columns["ID"].Visible = false;
                            GvreceiptInfo.Columns["Date"].Width = 80;
                            GvreceiptInfo.Columns["Particulars"].Width = 200;
                            GvreceiptInfo.Columns["ChequeNo"].Width = 80;
                            GvreceiptInfo.Columns["Withdrawals"].Width = 95;
                            GvreceiptInfo.Columns["Deposits"].Width = 95;
                            GvreceiptInfo.Columns["TransactionType"].Width = 110;
                            da.Dispose();
                            dt.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select Bank Name.", "Warning");
                        this.ActiveControl = cmbsbankname1;
                    }
                }
                else
                {
                    MessageBox.Show("Please Check CheckBox.", "Warning");
                    this.ActiveControl = cmbsbankname1;
                }
            }
            catch (Exception)
            { }
        }

        private void btnaddopening_Click(object sender, EventArgs e)
        {
            try
            {
                decimal openingbalance = 0;
                string input = "";
                if (cmbbankname.Text != "Select")
                {
                    BankStatementRepository BankStRepo = new BankStatementRepository();
                    bankid = Convert.ToInt32(cmbbankname.SelectedValue);
                    BankStatement bankdata1 = new BankStatement();
                    bankdata1 = BankStRepo.GetAll().Where(t => t.BankId == bankid && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (bankdata1 != null)
                    {
                        openingbalance = Convert.ToDecimal(BankStRepo.GetAll().Where(t => t.BankId == bankid && t.CompId == CommonMethod.CompId).Sum(t => t.OpeningBalance));
                        if (openingbalance > 0)
                        {
                            MessageBox.Show("Opening Balance is already entered of " + cmbbankname.Text + "");
                            this.ActiveControl = cmbbankname;
                        }
                        else
                        {
                            input = Microsoft.VisualBasic.Interaction.InputBox("Input Opening Balance of " + cmbbankname.Text + "", "Opening Balance", "" + openingbalance + "", 300, 250);
                            if (bankdata1.OpeningBalance == 0)
                            {
                                bankdata1.BankId = Convert.ToInt32(cmbbankname.SelectedValue);
                                bankdata1.Bankname = Convert.ToString(cmbbankname.Text);
                                if (dtpdate.Value != null)
                                    bankdata1.TransactionDate = dtpdate.Value;
                                else
                                    bankdata1.TransactionDate = DateTime.Now.Date;
                                bankdata1.Particulars = Convert.ToString("Opening Balance");
                                bankdata1.ChequeNo = Convert.ToString("");
                                bankdata1.ACGId = Convert.ToInt32(cmbACGroups.SelectedValue);
                                if (cmbACGroups.Text == "Bank OD A/C" || cmbACGroups.Text == "Unsecured Loans" || cmbACGroups.Text == "Loans (Liability)" || cmbACGroups.Text == "Secured Loans")
                                {
                                    bankdata1.Withdrawals = Convert.ToDecimal(input);
                                    bankdata1.Deposits = null;
                                }
                                else
                                {
                                    bankdata1.Deposits = Convert.ToDecimal(input);
                                    bankdata1.Withdrawals = null;
                                }
                                bankdata1.OpeningBalance = Convert.ToDecimal(input);
                                bankdata1.TransactionType = Convert.ToString("Opening Balance");
                                bankdata1.Balance = 0;
                                bankdata1.CompId = CommonMethod.CompId;
                                BankStRepo.Edit(bankdata1);
                                BankStRepo.Save();
                                MessageBox.Show("Opening Balance Saved Sucessfully.", "Success");
                                this.ActiveControl = cmbbankname;
                                BankStRepo.Dispose();
                            }
                            else
                            {
                                BankStatementRepository bankrepo = new BankStatementRepository();
                                BankStatement bankstdata = new BankStatement();
                                bankstdata.BankId = Convert.ToInt32(cmbbankname.SelectedValue);
                                bankstdata.Bankname = Convert.ToString(cmbbankname.Text);
                                if (dtpdate.Value != null)
                                    bankstdata.TransactionDate = dtpdate.Value;
                                else
                                    bankstdata.TransactionDate = DateTime.Now.Date;
                                bankstdata.Particulars = Convert.ToString("Opening Balance");
                                bankstdata.ChequeNo = Convert.ToString("");
                                bankstdata.ACGId = Convert.ToInt32(cmbACGroups.SelectedValue);
                                if (cmbACGroups.Text == "Bank OD A/C" || cmbACGroups.Text == "Unsecured Loans" || cmbACGroups.Text == "Loans (Liability)" || cmbACGroups.Text == "Secured Loans")
                                {
                                    bankstdata.Withdrawals = Convert.ToDecimal(input);
                                    bankstdata.Deposits = null;
                                }
                                else
                                {
                                    bankstdata.Deposits = Convert.ToDecimal(input);
                                    bankstdata.Withdrawals = null;
                                }
                                bankstdata.OpeningBalance = Convert.ToDecimal(input);
                                bankstdata.TransactionType = Convert.ToString("Opening Balance");
                                bankstdata.Balance = 0;
                                bankstdata.CompId = CommonMethod.CompId;
                                bankrepo.Add(bankstdata);
                                bankrepo.Save();
                                MessageBox.Show("Opening Balance Saved Sucessfully.", "Success");
                                this.ActiveControl = cmbbankname;
                                bankrepo.Dispose();
                            }
                        }
                    }
                    else
                    {
                        openingbalance = 0;
                        input = Microsoft.VisualBasic.Interaction.InputBox("Input Opening Balance of " + cmbbankname.Text + "", "Opening Balance", "" + openingbalance + "", 300, 250);
                        BankStatementRepository bankrepo = new BankStatementRepository();
                        BankStatement bankstdata = new BankStatement();
                        bankstdata.BankId = Convert.ToInt32(cmbbankname.SelectedValue);
                        bankstdata.Bankname = Convert.ToString(cmbbankname.Text);
                        if (dtpdate.Value != null)
                            bankstdata.TransactionDate = dtpdate.Value;
                        else
                            bankstdata.TransactionDate = DateTime.Now.Date;
                        bankstdata.Particulars = Convert.ToString("Opening Balance");
                        bankstdata.ChequeNo = Convert.ToString("");
                        bankstdata.ACGId = Convert.ToInt32(cmbACGroups.SelectedValue);
                        if (cmbACGroups.Text == "Bank OD A/C" || cmbACGroups.Text == "Unsecured Loans" || cmbACGroups.Text == "Loans (Liability)" || cmbACGroups.Text == "Secured Loans")
                        {
                            bankstdata.Withdrawals = Convert.ToDecimal(input);
                            bankstdata.Deposits = null;
                        }
                        else
                        {
                            bankstdata.Deposits = Convert.ToDecimal(input);
                            bankstdata.Withdrawals = null;
                        }
                        bankstdata.OpeningBalance = Convert.ToDecimal(input);
                        bankstdata.TransactionType = Convert.ToString("Opening Balance");
                        bankstdata.Balance = 0;
                        bankstdata.CompId = CommonMethod.CompId;
                        bankrepo.Add(bankstdata);
                        bankrepo.Save();
                        MessageBox.Show("Opening Balance Saved Sucessfully.", "Success");
                        this.ActiveControl = cmbbankname;
                        bankrepo.Dispose();
                    }
                    BankStRepo.Dispose();
                }
                else
                {
                    MessageBox.Show("Select Bank Name.", "Warning");
                    this.ActiveControl = cmbbankname;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbsbankname1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void frmBankStatement_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
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
                cmbcustorsupp.DataSource = customerdata;
                cmbcustorsupp.ValueMember = "CustomerID";
                cmbcustorsupp.DisplayMember = "CustomerName";
                custrepo.Dispose();
                lblcustorsupp.Text = "Customer Name:";
            }
            catch (Exception) { }
        }

        public void supplierdata()
        {
            try
            {
                SupplierRepository custrepo = new SupplierRepository();
                List<SupplierInformation> customerdata = new List<SupplierInformation>();
                customerdata.Clear();
                if (CommonMethod.commProduct == true)
                    customerdata = custrepo.GetAll().OrderByDescending(t => t.SupplierName).ToList();
                else
                    customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.SupplierName).ToList();
                customerdata.Add(new SupplierInformation { SupplierName = "Select", SupplierCode = 0 });
                customerdata.Reverse();
                cmbcustorsupp.DataSource = customerdata;
                cmbcustorsupp.ValueMember = "SupplierCode";
                cmbcustorsupp.DisplayMember = "SupplierName";
                custrepo.Dispose();
                lblcustorsupp.Text = "Supplier Name:";
            }
            catch (Exception) { }
        }

        public void expencedata()
        {
            try
            {
                ExpenceMasterRepository expmastRepo = new ExpenceMasterRepository();
                List<ExpenceMaster> expdata = new List<ExpenceMaster>();
                expdata.Clear();
                expdata = expmastRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.ExpenceName).ToList();
                expdata.Add(new ExpenceMaster { ExpenceName = "Select", ExpenceId = 0 });
                expdata.Reverse();
                cmbcustorsupp.DataSource = expdata;
                cmbcustorsupp.ValueMember = "ExpenceId";
                cmbcustorsupp.DisplayMember = "ExpenceName";
                expmastRepo.Dispose();
                lblcustorsupp.Text = "Expence Name:";
            }
            catch (Exception) { }
        }

        private void cmbtransaction_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbtransaction.Text == "Receipt" || cmbtransaction.Text == "Payment" || cmbtransaction.Text == "Expence")
                {
                    txtparticular.Text = "";
                    cmbcustorsupp.Enabled = true;
                    label10.Visible = true;
                    chkcheque.Checked = true;
                    if (cmbtransaction.Text == "Receipt")
                    {
                        try
                        {
                            customerdata();
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbtransaction.Text == "Payment")
                    {
                        try
                        {
                            supplierdata();
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbtransaction.Text == "Expence")
                    {
                        try
                        {
                            expencedata();
                        }
                        catch (Exception)
                        { }
                    }
                }
                else
                {
                    cmbcustorsupp.Enabled = false;
                    label10.Visible = false;
                    cmbcustorsupp.SelectedIndex = 0;
                }
            }
            catch (Exception)
            { }
        }

        private void GvreceiptInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["TransactionType"].Value) == "Opening Balance")
                {
                    try
                    {
                        string input = "";
                        BankStatementRepository BankStRepo = new BankStatementRepository();
                        Bankstatid = Convert.ToInt32(GvreceiptInfo.Rows[e.RowIndex].Cells["Id"].Value);
                        BankStatement bankdata1 = new BankStatement();
                        bankdata1 = BankStRepo.GetById(Bankstatid);
                        if (bankdata1 != null)
                        {
                        a:
                            {
                                string input1 = "";
                                input1 = Microsoft.VisualBasic.Interaction.InputBox("1 For Update, 2 For Delete", "Opening Balance", "", 250, 250);
                                if (input1 == "1" || input1 == "")
                                {
                                    input = Microsoft.VisualBasic.Interaction.InputBox("Input Opening Balance of " + cmbsbankname1.Text + "", "Opening Balance", "" + bankdata1.OpeningBalance + "", 300, 250);
                                    if (dtpdate.Value != null)
                                        bankdata1.TransactionDate = dtpdate.Value;
                                    else
                                        bankdata1.TransactionDate = DateTime.Now.Date;
                                    bankdata1.ACGId = Convert.ToInt32(cmbACGroups.SelectedValue);
                                    if (cmbACGroups.Text == "Bank OD A/C")
                                    {
                                        bankdata1.Withdrawals = Convert.ToDecimal(input);
                                        bankdata1.Deposits = null;
                                    }
                                    else
                                    {
                                        bankdata1.Deposits = Convert.ToDecimal(input);
                                        bankdata1.Withdrawals = null;
                                    }
                                    bankdata1.OpeningBalance = Convert.ToDecimal(input);
                                    bankdata1.TransactionType = Convert.ToString("Opening Balance");
                                    bankdata1.Balance = 0;
                                    bankdata1.CompId = CommonMethod.CompId;
                                    BankStRepo.Edit(bankdata1);
                                    BankStRepo.Save();
                                    MessageBox.Show("Opening Balance Updated Sucessfully.", "Success");
                                    this.ActiveControl = cmbbankname;
                                    BankStRepo.Dispose();
                                    btnsearch_Click(null, null);
                                }
                                else if (input1 == "2")
                                {
                                    if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                                    {
                                        BankStRepo.Remove(bankdata1);
                                        BankStRepo.Save();
                                        MessageBox.Show("Record Delete Sucessfully.", "Success");
                                        clear();
                                        btnsearch_Click(null, null);
                                    }
                                }
                                else
                                    goto a;
                            }
                        }
                    }
                    catch (Exception)
                    { }
                }
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["TransactionType"].Value) == "Withdrawals" || Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["TransactionType"].Value) == "Deposits")
                {
                    btnSave.Text = "Update";
                    btndelete.Enabled = true;
                    Bankstatid = Convert.ToInt32(GvreceiptInfo.Rows[e.RowIndex].Cells["Id"].Value);
                    BankStatementRepository bRepo = new BankStatementRepository();
                    var bdata = bRepo.GetById(Bankstatid);
                    cmbACGroups.SelectedValue = bdata.ACGId;
                    cmbbankname.Text = bdata.Bankname;
                    try
                    {
                        dtpdate.Value = bdata.TransactionDate.Value;
                    }
                    catch (Exception)
                    { }
                    txtparticular.Text = bdata.Particulars;
                    cmbtransaction.Text = bdata.TransactionType;
                    if (bdata.ChequeNo != "")
                        chkcheque.Checked = true;
                    else
                        chkcheque.Checked = false;
                    txtchequeno.Text = bdata.ChequeNo;
                    if (cmbtransaction.Text == "Withdrawals")
                        txtAmount.Text = Convert.ToString(bdata.Withdrawals);
                    else if (cmbtransaction.Text == "Deposits")
                        txtAmount.Text = Convert.ToString(bdata.Deposits);
                    else
                        txtAmount.Text = "0";
                    cmbtransaction.Enabled = false;
                }
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["TransactionType"].Value) == "Receipt")
                    MessageBox.Show("Please Update this in Customer Ledger Form.", "Warning");
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["TransactionType"].Value) == "Payment")
                    MessageBox.Show("Please Update this in Supplier Ledger Form.", "Warning");
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["TransactionType"].Value) == "Expence")
                    MessageBox.Show("Please Update this in Expence Form.", "Warning");
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["TransactionType"].Value) == "Purchase")
                    MessageBox.Show("Please Update this in Purchase Bill Form.", "Warning");
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["TransactionType"].Value) == "Bill")
                    MessageBox.Show("Please Update this in Invoice Form.", "Warning");
                if (Convert.ToString(GvreceiptInfo.Rows[e.RowIndex].Cells["TransactionType"].Value) == "Journal Entry")
                    MessageBox.Show("Please Update this in Journal Entry Form.", "Warning");
            }
            catch (Exception)
            { }
        }

        private void cmbcustorsupp_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbcustorsupp.SelectedIndex != 0)
                {
                    if (cmbtransaction.Text == "Receipt")
                        txtparticular.Text = "Receive From" + " " + Convert.ToString(cmbcustorsupp.Text.Trim());
                    else
                        txtparticular.Text = "Paid To" + " " + Convert.ToString(cmbcustorsupp.Text.Trim());
                }
            }
            catch (Exception)
            { }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                BankStatementRepository Brepo = new BankStatementRepository();
                BankStatement bsdata = Brepo.GetById(Bankstatid);
                if (bsdata != null)
                {
                    Brepo.Remove(bsdata);
                    Brepo.Save();
                    MessageBox.Show("Record Delete Sucessfully.", "Success");
                    clear();
                    btnsearch_Click(null, null);
                }
            }
            catch (Exception)
            { }
        }

        private void cmbbankname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtparticular;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmBankInformation bank = new frmBankInformation();
                        bank.ShowDialog();
                        fillcombo(Convert.ToInt32(cmbACGroups.SelectedValue));
                        this.ActiveControl = cmbbankname;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void dtpdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbtransaction;
            }
            catch (Exception)
            { }
        }

        private void txtparticular_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpdate;
            }
            catch (Exception)
            { }
        }

        private void cmbtransaction_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (cmbtransaction.Text == "Withdrawals" || cmbtransaction.Text == "Deposits")
                        this.ActiveControl = chkcheque;
                    else
                        this.ActiveControl = cmbcustorsupp;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbcustorsupp_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = chkcheque;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        if (cmbtransaction.Text == "Receipt")
                        {
                            frmcustomer customer = new frmcustomer();
                            customer.ShowDialog();
                            customerdata();
                            this.ActiveControl = cmbcustorsupp;
                        }
                        else if (cmbtransaction.Text == "Payment")
                        {
                            frmSupplierInformation supplier = new frmSupplierInformation();
                            supplier.ShowDialog();
                            supplierdata();
                            this.ActiveControl = cmbcustorsupp;
                        }
                        else if (cmbtransaction.Text == "Expence")
                        {
                            frmExpenceMaster expences = new frmExpenceMaster();
                            expences.ShowDialog();
                            expencedata();
                            this.ActiveControl = cmbcustorsupp;
                        }
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
                    this.ActiveControl = txtAmount;
            }
            catch (Exception)
            { }
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSave;
            }
            catch (Exception)
            { }
        }

        private void dtpfrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void dtpto_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chkfromdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpfrom;
            }
            catch (Exception)
            { }
        }

        private void chkbankname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbsbankname1;
            }
            catch (Exception)
            { }
        }

        private void cmbcustorsupp_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbcustorsupp.Text != "Select")
                {
                    if (cmbtransaction.Text == "Receipt")
                    {
                        try
                        {
                            CommonMethod cm = new CommonMethod();
                            if (cm.ValidCustomer(cmbcustorsupp.Text) == 1)
                            { }
                            else
                            {
                                MessageBox.Show("Invalid Customer Name.", "Warning");
                                cmbcustorsupp.SelectedIndex = 0;
                                this.ActiveControl = cmbcustorsupp;
                            }
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbtransaction.Text == "Payment")
                    {
                        try
                        {
                            CommonMethod cm = new CommonMethod();
                            if (cm.ValidSupplier(cmbcustorsupp.Text) == 1)
                            { }
                            else
                            {
                                MessageBox.Show("Invalid Supplier Name.", "Warning");
                                cmbcustorsupp.SelectedIndex = 0;
                                this.ActiveControl = cmbcustorsupp;
                            }
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbtransaction.Text == "Expence")
                    {
                        try
                        {
                            CommonMethod cm = new CommonMethod();
                            if (cm.ValidExpence(cmbcustorsupp.Text) == 1)
                            { }
                            else
                            {
                                MessageBox.Show("Invalid Expence Name.", "Warning");
                                cmbcustorsupp.SelectedIndex = 0;
                                this.ActiveControl = cmbcustorsupp;
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbACGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbACGroups.SelectedIndex > 0)
                {
                    fillcombo(Convert.ToInt32(cmbACGroups.SelectedValue));
                    fillcombo1(Convert.ToInt32(cmbACGroups.SelectedValue));
                }
                else
                {
                    cmbbankname.Text = "Select";
                    cmbsbankname1.Text = "Select";
                }
            }
            catch (Exception)
            { }
        }

        private void LoadData(DateTime fromDateVal, DateTime toDateVal, String TransType)
        {
            label2.Visible = false;
            label12.Visible = false;
            DateTime opdate = dtpfrom.Value.AddDays(-1);
            string tilldate = cm.ValidToDate(opdate);
            SqlDataAdapter da1 = new SqlDataAdapter("Select Id,CONVERT(VARCHAR(23),TransactionDate,103) as Date,Particulars,ChequeNo as 'Chq./UTR No.',TransactionType,Withdrawals,Deposits from BankStatement where CompId='" + CommonMethod.CompId + "'  and TransactionDate>='" + cm.ValidFromDate(sdate) + "' and TransactionDate<='" + tilldate + "'", db.Connection);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            string toDate = cm.ValidToDate(toDateVal), fromDate = cm.ValidFromDate(fromDateVal);
            SqlDataAdapter da = new SqlDataAdapter("Select Id,CONVERT(VARCHAR(23),TransactionDate,103) as Date,Particulars,ChequeNo as 'Chq./UTR No.',TransactionType,Withdrawals,Deposits from BankStatement where CompId='" + CommonMethod.CompId + "'  and TransactionDate>='" + fromDate + "' and TransactionDate<='" + toDate + "' and Transactiontype='" + TransType + "'", db.Connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GvreceiptInfo.DataSource = dt;
            GvreceiptInfo.Refresh();
            da.Dispose();
            dt.Dispose();
        }

        private void chkbankname_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                transType = "";
                AccountType1 = "";
            }
            catch (Exception) { }
        }

        private void chkfromdate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                transType = "";
                AccountType1 = "";
            }
            catch (Exception) { }
        }

        public void BalSheetToBankSt(string AccountType, DateTime fdate, DateTime tdate)
        {
            try
            {
                if (dtpfrom.Value.Date <= dtpto.Value.Date)
                {
                    if (AccountType == "Bank Accounts")
                    {
                        label14.Visible = true;
                        label15.Visible = true;
                        DateTime opdate = fdate.AddDays(-1);
                        string fromDate1 = cm.ValidFromDate(fdate.Date), toDate1 = cm.ValidToDate(tdate.Date), tilldate = cm.ValidToDate(opdate),
                            toDate = cm.ValidToDate(tdate.Date), fromDate = cm.ValidFromDate(fdate.Date);
                        if (!GvreceiptInfo.Columns.Contains("Id"))
                        {
                            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
                            idColumn.Name = "Id";
                            idColumn.HeaderText = "Id";
                            GvreceiptInfo.Columns.Add(idColumn);
                        }
                        if (!GvreceiptInfo.Columns.Contains("Date"))
                        {
                            DataGridViewTextBoxColumn DateColumn = new DataGridViewTextBoxColumn();
                            DateColumn.Name = "Date";
                            DateColumn.HeaderText = "Date";
                            GvreceiptInfo.Columns.Add(DateColumn);
                        }
                        if (!GvreceiptInfo.Columns.Contains("Particulars"))
                        {
                            DataGridViewTextBoxColumn ParticularsColumn = new DataGridViewTextBoxColumn();
                            ParticularsColumn.Name = "Particulars";
                            ParticularsColumn.HeaderText = "Particulars";
                            GvreceiptInfo.Columns.Add(ParticularsColumn);
                        }
                        if (!GvreceiptInfo.Columns.Contains("ChequeNo"))
                        {
                            DataGridViewTextBoxColumn ChequeNoColumn = new DataGridViewTextBoxColumn();
                            ChequeNoColumn.Name = "ChequeNo";
                            ChequeNoColumn.HeaderText = "ChequeNo";
                            GvreceiptInfo.Columns.Add(ChequeNoColumn);
                        }
                        if (!GvreceiptInfo.Columns.Contains("TransactionType"))
                        {
                            DataGridViewTextBoxColumn TransactionTypeColumn = new DataGridViewTextBoxColumn();
                            TransactionTypeColumn.Name = "TransactionType";
                            TransactionTypeColumn.HeaderText = "TransactionType";
                            GvreceiptInfo.Columns.Add(TransactionTypeColumn);
                        }
                        if (!GvreceiptInfo.Columns.Contains("Withdrawals"))
                        {
                            DataGridViewTextBoxColumn WithdrawalsColumn = new DataGridViewTextBoxColumn();
                            WithdrawalsColumn.Name = "Withdrawals";
                            WithdrawalsColumn.HeaderText = "Withdrawals";
                            GvreceiptInfo.Columns.Add(WithdrawalsColumn);
                        }
                        if (!GvreceiptInfo.Columns.Contains("Deposits"))
                        {
                            DataGridViewTextBoxColumn DepositsColumn = new DataGridViewTextBoxColumn();
                            DepositsColumn.Name = "Deposits";
                            DepositsColumn.HeaderText = "Deposits";
                            GvreceiptInfo.Columns.Add(DepositsColumn);
                        }
                        int j = 0;
                        decimal sumObject = 0, sumObject1 = 0;
                        GvreceiptInfo.Rows.Clear();
                        DataTable dtbank = db.GetTable("Select BankId,BankName from BankStatement b inner join AccountingGroupMaster a on b.ACGId=a.Id where TransactionDate between '" + fromDate + "' and '" + toDate + "' and CompId='" + CommonMethod.CompId + "' and ACGId In (Select Id from AccountingGroupMaster where ACGName='" + AccountType + "') group by BankId,BankName ");
                        if (dtbank.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtbank.Rows.Count; i++)
                            {
                                int bankid = Convert.ToInt32(dtbank.Rows[i]["BankId"].ToString());
                                string bankname = Convert.ToString(dtbank.Rows[i]["BankName"].ToString());
                                DataTable dtbankst = db.GetTable("Select Id,CONVERT(VARCHAR(23),TransactionDate,103) as Date,Particulars,ChequeNo as 'Chq./UTR No.',TransactionType,Withdrawals,Deposits from BankStatement where BankId='" + bankid + "' and BankName='" + bankname + "' and CompId='" + CommonMethod.CompId + "'  and TransactionDate>='" + fromDate + "' and TransactionDate<='" + toDate + "'");
                                if (dtbankst.Rows.Count > 0)
                                {
                                    try
                                    {
                                        sumObject = sumObject + Convert.ToDecimal(dtbankst.Compute("Sum(Withdrawals)", ""));
                                    }
                                    catch (Exception)
                                    { }
                                    try
                                    {
                                        sumObject1 = sumObject1 + Convert.ToDecimal(dtbankst.Compute("Sum(Deposits)", ""));
                                    }
                                    catch (Exception)
                                    { }
                                    label12.Text = Convert.ToString(Convert.ToDecimal(label15.Text) + Convert.ToDecimal(sumObject1) - Convert.ToDecimal(sumObject));

                                    GvreceiptInfo.Refresh();
                                    for (j = 0; j < dtbankst.Rows.Count; j++)
                                    {
                                        GvreceiptInfo.Rows.Add();
                                        GvreceiptInfo.Rows[j].Cells["Id"].Value = dtbankst.Rows[j]["Id"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["Date"].Value = dtbankst.Rows[j]["Date"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["Particulars"].Value = dtbankst.Rows[j]["Particulars"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["Chq./UTR No."].Value = dtbankst.Rows[j]["Chq./UTR No."].ToString();
                                        GvreceiptInfo.Rows[j].Cells["TransactionType"].Value = dtbankst.Rows[j]["TransactionType"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["Withdrawals"].Value = dtbankst.Rows[j]["Withdrawals"].ToString();
                                        GvreceiptInfo.Rows[j].Cells["Deposits"].Value = dtbankst.Rows[j]["Deposits"].ToString();

                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        label14.Visible = true;
                        label15.Visible = true;
                        DateTime opdate = fdate.AddDays(-1);
                        string bankname = AccountType, tilldate = cm.ValidToDate(opdate);
                        SqlDataAdapter da1 = new SqlDataAdapter("Select b.Id,CONVERT(VARCHAR(23),TransactionDate,103) as Date,Particulars,ChequeNo as 'Chq./UTR No.',TransactionType,Withdrawals,Deposits from BankStatement b inner join AccountingGroupMaster a on b.ACGId=a.Id where BankName='" + bankname + "' and CompId='" + CommonMethod.CompId + "'  and TransactionDate>='" + cm.ValidFromDate(sdate) + "' and TransactionDate<='" + tilldate + "' and ACGId In (Select Id from AccountingGroupMaster where ACGName='Bank OD A/C' or ACGName='Secured Loans') ", db.Connection);
                        DataTable dt1 = new DataTable();
                        da1.Fill(dt1);
                        decimal sumObject2 = 0, sumObject3 = 0;
                        try
                        {
                            sumObject2 = Convert.ToDecimal(dt1.Compute("Sum(Withdrawals)", ""));
                        }
                        catch (Exception)
                        { }
                        try
                        {
                            sumObject3 = Convert.ToDecimal(dt1.Compute("Sum(Deposits)", ""));
                        }
                        catch (Exception)
                        { }
                        label15.Text = Convert.ToString(Convert.ToDecimal(sumObject2) - Convert.ToDecimal(sumObject3));

                        string toDate = cm.ValidToDate(tdate.Date), fromDate = cm.ValidFromDate(fdate.Date);

                        SqlDataAdapter da = new SqlDataAdapter("Select b.Id,CONVERT(VARCHAR(23),TransactionDate,103) as Date,Particulars,ChequeNo as 'Chq./UTR No.',TransactionType,Withdrawals,Deposits from BankStatement b inner join AccountingGroupMaster a on b.ACGId=a.Id where BankName='" + bankname + "' and b.CompId='" + CommonMethod.CompId + "'  and TransactionDate>='" + fromDate + "' and TransactionDate<='" + toDate + "' and ACGId In (Select Id from AccountingGroupMaster where ACGName='Bank OD A/C' or ACGName='Secured Loans') ", db.Connection);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        decimal sumObject = 0, sumObject1 = 0;
                        try
                        {
                            sumObject = Convert.ToDecimal(dt.Compute("Sum(Withdrawals)", ""));
                        }
                        catch (Exception)
                        { }
                        try
                        {
                            sumObject1 = Convert.ToDecimal(dt.Compute("Sum(Deposits)", ""));
                        }
                        catch (Exception)
                        { }
                        label12.Text = Convert.ToString(Convert.ToDecimal(sumObject) - Convert.ToDecimal(sumObject1));
                        GvreceiptInfo.DataSource = dt;
                        GvreceiptInfo.Refresh();
                        GvreceiptInfo.Columns["ID"].Visible = false;
                        GvreceiptInfo.Columns["Date"].Width = 80;
                        GvreceiptInfo.Columns["Particulars"].Width = 200;
                        GvreceiptInfo.Columns["ChequeNo"].Width = 80;
                        GvreceiptInfo.Columns["Withdrawals"].Width = 95;
                        GvreceiptInfo.Columns["Deposits"].Width = 95;
                        GvreceiptInfo.Columns["TransactionType"].Width = 110;
                        da.Dispose();
                        dt.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("To Date Should greater than or equal to from date.", "Warning");
                    this.ActiveControl = dtpto;
                }
            }
            catch (Exception) { }
        }

        private void cmbACGroups_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbbankname;
            }
            catch (Exception) { }
        }

        private void chkcheque_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (chkcheque.Checked == true)
                        this.ActiveControl = txtchequeno;
                    else
                        this.ActiveControl = txtAmount;
                }
            }
            catch (Exception)
            { }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }
    }
}