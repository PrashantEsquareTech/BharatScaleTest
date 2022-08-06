using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmJournalEntry : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        public string maxsuppreceiptno, maxcustreceiptno, maxexpenceno, maxincomeno, MaxPropRecptId, maxid;
        public int Journalid;

        public frmJournalEntry()
        {
            InitializeComponent();
            maxJEid();
            BindJEinfo();
            cm.changedatetimepickerrange(this);
            btndelete.Enabled = false;
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
            {
                btnsave.Enabled = false;
                btndelete.Enabled = false;
                btnnew.Enabled = false;
            }
        }

        public void BindJEinfo()
        {
            try
            {
                JournalEntryRepository expencerepo = new JournalEntryRepository();
                GvJEInfo.Rows.Clear();
                List<JournalEntry> ItemList = expencerepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                label13.Text = Convert.ToString(Convert.ToDecimal(ItemList.Sum(t => t.Amount)));
                if (ItemList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        GvJEInfo.Rows.Add();
                        GvJEInfo.Rows[i].Cells["JENo"].Value = item.JENo;
                        GvJEInfo.Rows[i].Cells["Date"].Value = item.JEDate.Value.ToShortDateString();
                        GvJEInfo.Rows[i].Cells["FromGroup"].Value = item.FromGroup;
                        GvJEInfo.Rows[i].Cells["FromAccountName"].Value = item.FromAccountName;
                        GvJEInfo.Rows[i].Cells["ToGroup"].Value = item.ToGroup;
                        GvJEInfo.Rows[i].Cells["ToAccountName"].Value = item.ToAccountName;
                        GvJEInfo.Rows[i].Cells["PaidAmt"].Value = item.Amount;
                        GvJEInfo.Rows[i].Cells["Id"].Value = item.Id;
                        i++;
                    }
                }
                expencerepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void maxJEid()
        {
            try
            {
                DataTable dt = db.GetTable("Select max(JENo) from JournalEntry WHERE TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                if (dt.Rows.Count > 0)
                {
                    maxid = dt.Rows[0][0].ToString();
                    if (maxid == "")
                        txtJENo.Text = "1";
                    else
                        txtJENo.Text = Convert.ToString(Convert.ToInt32(maxid + 1));
                }
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            txtpaidamount.Text = "";
            BindJEinfo();
            maxJEid(); btnsave.Text = "Save";
            btndelete.Enabled = false;
            cmbFromGroupName.SelectedIndex = -1;
            cmbToGroupName.SelectedIndex = -1;
            cmbFromAccountName.DataSource = null;
            cmbToAccountName.DataSource = null;
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
            {
                btnsave.Enabled = false;
                btndelete.Enabled = false;
                btnnew.Enabled = false;
            }
            try
            {
                dtpJEdate.Value = DateTime.Now.Date;
            }
            catch (Exception)
            { }
            cmbFromGroupName.Enabled = true;
            cmbToGroupName.Enabled = true;
            cmbFromAccountName.Enabled = true;
            cmbToAccountName.Enabled = true;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                    this.Close();
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

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbFromAccountName.Text != "Select" && cmbFromAccountName.Text != "" && cmbToAccountName.Text != "Select" && cmbToAccountName.Text != "")
                {
                    if (txtpaidamount.Text != "")
                    {
                        JournalEntryRepository expencerepo = new JournalEntryRepository();
                        JournalEntry expencedata = new JournalEntry();
                        expencedata.JEDate = Convert.ToDateTime(dtpJEdate.Value);
                        expencedata.FromGroup = cmbFromGroupName.Text;
                        expencedata.FromAccountId = Convert.ToInt32(cmbFromAccountName.SelectedValue);
                        expencedata.FromAccountName = Convert.ToString(cmbFromAccountName.Text);
                        expencedata.ToGroup = cmbToGroupName.Text;
                        expencedata.ToAccountId = Convert.ToInt32(cmbToAccountName.SelectedValue);
                        expencedata.ToAccountName = Convert.ToString(cmbToAccountName.Text);
                        expencedata.Amount = Convert.ToDecimal(txtpaidamount.Text);
                        expencedata.TransactionYear = CommonMethod.TransactionYear;
                        expencedata.CompId = CommonMethod.CompId;
                        if (btnsave.Text == "Update")
                        {
                            JournalEntryRepository JRepo = new JournalEntryRepository();
                            var Jdata = JRepo.GetById(Journalid);
                            if (cmbFromGroupName.Text == "Supplier")
                            {
                                SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                                SupplierRemainingPayment remainingdata = remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbFromAccountName.SelectedValue) && t.Suppliername == cmbFromAccountName.Text && t.Type == "Send To" && t.CreateDate.Value.ToShortDateString() == Jdata.JEDate.Value.ToShortDateString() && t.PaidAmt == Jdata.Amount && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                remainingdata.SupplierId = Convert.ToInt32(cmbFromAccountName.SelectedValue);
                                remainingdata.Suppliername = cmbFromAccountName.Text;
                                remainingdata.CreateDate = dtpJEdate.Value;
                                remainingdata.Type = "Send To";
                                remainingdata.RemainingAmt = Convert.ToDecimal(0);
                                remainingdata.NetAmt = Convert.ToDecimal(0);
                                remainingdata.PaidAmt = Convert.ToDecimal(txtpaidamount.Text);
                                remainingdata.TransactionYear = CommonMethod.TransactionYear;
                                remainingdata.CompId = CommonMethod.CompId;
                                remainingdata.ReceiptBy = CommonMethod.CompId;
                                remainingpayrepo.Edit(remainingdata);
                                remainingpayrepo.Save();
                            }
                            else if (cmbFromGroupName.Text == "Customer")
                            {
                                RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                                RemainingPayment remainingdata = remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbFromAccountName.SelectedValue) && t.Customername == cmbFromAccountName.Text && t.Type == "Send To" && t.CreateDate.Value.ToShortDateString() == Jdata.JEDate.Value.ToShortDateString() && t.PaidAmt == Jdata.Amount && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                remainingdata.CustomerId = Convert.ToInt32(cmbFromAccountName.SelectedValue);
                                remainingdata.Customername = cmbFromAccountName.Text;
                                remainingdata.CreateDate = dtpJEdate.Value;
                                remainingdata.Type = "Send To";
                                remainingdata.RemainingAmt = Convert.ToDecimal(0);
                                remainingdata.NetAmt = Convert.ToDecimal(0);
                                remainingdata.PaidAmt = Convert.ToDecimal(txtpaidamount.Text);
                                remainingdata.TransactionYear = CommonMethod.TransactionYear;
                                remainingdata.CompId = CommonMethod.CompId;
                                remainingpayrepo.Edit(remainingdata);
                                remainingpayrepo.Save();
                            }
                            else if (cmbFromGroupName.Text == "Expence")
                            {
                                ExpenceRepository ExpenceRepo = new ExpenceRepository();
                                Expence ExpenceData = ExpenceRepo.GetAll().Where(t => t.ExpenceName == cmbFromAccountName.Text && t.ExpenceDate.Value.ToShortDateString() == Jdata.JEDate.Value.ToShortDateString() && t.Reason == "Journal Entry" && t.PaidAmount == Convert.ToDouble(Jdata.Amount * -1) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                ExpenceData.ExpenceDate = Convert.ToDateTime(dtpJEdate.Value);
                                ExpenceData.ExpenceName = cmbFromAccountName.Text;
                                ExpenceData.FromPerson = "";
                                ExpenceData.ToPerson = "Send To " + cmbToAccountName.Text.Trim();
                                ExpenceData.Reason = "Journal Entry";
                                ExpenceData.PaidAmount = Convert.ToDouble(txtpaidamount.Text) * -1;
                                ExpenceData.TransactionYear = CommonMethod.TransactionYear;
                                ExpenceData.CompId = CommonMethod.CompId;
                                ExpenceRepo.Edit(ExpenceData);
                                ExpenceRepo.Save();
                            }
                            else if (cmbFromGroupName.Text == "Other Income")
                            {
                                IncomeRepository IncomeRepo = new IncomeRepository();
                                Income Incomedata = IncomeRepo.GetAll().Where(t => t.IncomeMID == Convert.ToInt32(cmbFromAccountName.SelectedValue) && t.IncomeDate.Value.ToShortDateString() == Jdata.JEDate.Value.ToShortDateString() && t.Reason == "Journal Entry" && t.PaidAmount == Convert.ToDouble(Jdata.Amount) * -1 && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                Incomedata.IncomeDate = Convert.ToDateTime(dtpJEdate.Value);
                                Incomedata.IncomeMID = Convert.ToInt32(cmbFromAccountName.SelectedValue);
                                Incomedata.FromPerson = "Send To " + cmbToAccountName.Text.Trim();
                                Incomedata.Reason = "Journal Entry";
                                Incomedata.PaidAmount = Convert.ToDouble(txtpaidamount.Text) * -1;
                                Incomedata.TransactionYear = CommonMethod.TransactionYear;
                                Incomedata.CompId = CommonMethod.CompId;
                                IncomeRepo.Edit(Incomedata);
                                IncomeRepo.Save();
                            }
                            else if (cmbFromGroupName.Text == "Bank Account")
                            {
                                BankStatementRepository bankrepo = new BankStatementRepository();
                                BankStatement bankstdata = bankrepo.GetAll().Where(t => t.BankId == Convert.ToInt32(cmbFromAccountName.SelectedValue) && t.Bankname == cmbFromAccountName.Text && t.TransactionDate.Value.ToShortDateString() == Jdata.JEDate.Value.ToShortDateString() && t.TransactionType == "Journal Entry" && t.Withdrawals == Convert.ToDecimal(Jdata.Amount) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                bankstdata.BankId = Convert.ToInt32(cmbFromAccountName.SelectedValue);
                                bankstdata.Bankname = Convert.ToString(cmbFromAccountName.Text);
                                bankstdata.TransactionDate = dtpJEdate.Value;
                                bankstdata.Particulars = "Send To " + cmbToAccountName.Text.Trim();
                                bankstdata.ChequeNo = Convert.ToString("");
                                bankstdata.TransactionType = "Journal Entry";
                                BankRepository brepo = new BankRepository();
                                var bdata = brepo.GetAll().Where(t => t.BankId == Convert.ToInt32(cmbFromAccountName.SelectedValue) && t.Bankname == cmbFromAccountName.Text).FirstOrDefault();
                                if (!string.IsNullOrEmpty(Convert.ToString(bdata)))
                                    bankstdata.ACGId = Convert.ToInt32(bdata.ACGId);
                                bankstdata.Withdrawals = Convert.ToDecimal(txtpaidamount.Text);
                                bankstdata.Balance = 0;
                                bankstdata.CompId = CommonMethod.CompId;
                                bankrepo.Edit(bankstdata);
                                bankrepo.Save();
                            }

                            if (cmbToGroupName.Text == "Supplier")
                            {
                                SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                                SupplierRemainingPayment remainingdata = remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbToAccountName.SelectedValue) && t.Suppliername == cmbToAccountName.Text && t.Type == "Receive From" && t.CreateDate.Value.ToShortDateString() == Jdata.JEDate.Value.ToShortDateString() && t.NetAmt == Jdata.Amount && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                remainingdata.SupplierId = Convert.ToInt32(cmbToAccountName.SelectedValue);
                                remainingdata.Suppliername = cmbToAccountName.Text;
                                remainingdata.CreateDate = dtpJEdate.Value;
                                remainingdata.Type = "Receive From";
                                remainingdata.RemainingAmt = Convert.ToDecimal(0);
                                remainingdata.NetAmt = Convert.ToDecimal(txtpaidamount.Text);
                                remainingdata.PaidAmt = Convert.ToDecimal(0);
                                remainingdata.TransactionYear = CommonMethod.TransactionYear;
                                remainingdata.CompId = CommonMethod.CompId;
                                remainingdata.ReceiptBy = CommonMethod.CompId;
                                remainingpayrepo.Edit(remainingdata);
                                remainingpayrepo.Save();
                            }
                            else if (cmbToGroupName.Text == "Customer")
                            {
                                RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                                RemainingPayment remainingdata = remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbToAccountName.SelectedValue) && t.Customername == cmbToAccountName.Text && t.Type == "Receive From" && t.CreateDate.Value.ToShortDateString() == Jdata.JEDate.Value.ToShortDateString() && t.NetAmt == Jdata.Amount && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                remainingdata.CustomerId = Convert.ToInt32(cmbToAccountName.SelectedValue);
                                remainingdata.Customername = cmbToAccountName.Text;
                                remainingdata.CreateDate = dtpJEdate.Value;
                                remainingdata.Type = "Receive From";
                                remainingdata.RemainingAmt = Convert.ToDecimal(0);
                                remainingdata.NetAmt = Convert.ToDecimal(txtpaidamount.Text);
                                remainingdata.PaidAmt = Convert.ToDecimal(0);
                                remainingdata.TransactionYear = CommonMethod.TransactionYear;
                                remainingdata.CompId = CommonMethod.CompId;
                                remainingpayrepo.Edit(remainingdata);
                                remainingpayrepo.Save();
                            }
                            else if (cmbToGroupName.Text == "Expence")
                            {
                                ExpenceRepository ExpenceRepo = new ExpenceRepository();
                                Expence ExpenceData = ExpenceRepo.GetAll().Where(t => t.ExpenceName == cmbToAccountName.Text && t.ExpenceDate.Value.ToShortDateString() == Jdata.JEDate.Value.ToShortDateString() && t.Reason == "Journal Entry" && t.PaidAmount == Convert.ToDouble(Jdata.Amount) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                ExpenceData.ExpenceDate = Convert.ToDateTime(dtpJEdate.Value);
                                ExpenceData.ExpenceName = cmbToAccountName.Text;
                                ExpenceData.FromPerson = "";
                                ExpenceData.ToPerson = "Receive From " + cmbFromAccountName.Text.Trim();
                                ExpenceData.Reason = "Journal Entry";
                                ExpenceData.PaidAmount = Convert.ToDouble(txtpaidamount.Text);
                                ExpenceData.TransactionYear = CommonMethod.TransactionYear;
                                ExpenceData.CompId = CommonMethod.CompId;
                                ExpenceRepo.Edit(ExpenceData);
                                ExpenceRepo.Save();
                            }
                            else if (cmbToGroupName.Text == "Other Income")
                            {
                                IncomeRepository IncomeRepo = new IncomeRepository();
                                Income Incomedata = IncomeRepo.GetAll().Where(t => t.IncomeMID == Convert.ToInt32(cmbToAccountName.SelectedValue) && t.IncomeDate.Value.ToShortDateString() == Jdata.JEDate.Value.ToShortDateString() && t.Reason == "Journal Entry" && t.PaidAmount == Convert.ToDouble(Jdata.Amount) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                Incomedata.IncomeDate = Convert.ToDateTime(dtpJEdate.Value);
                                Incomedata.IncomeMID = Convert.ToInt32(cmbToAccountName.SelectedValue);
                                Incomedata.FromPerson = "Receive From " + cmbFromAccountName.Text.Trim(); ;
                                Incomedata.Reason = "Journal Entry";
                                Incomedata.PaidAmount = Convert.ToDouble(txtpaidamount.Text);
                                Incomedata.TransactionYear = CommonMethod.TransactionYear;
                                Incomedata.CompId = CommonMethod.CompId;
                                IncomeRepo.Edit(Incomedata);
                                IncomeRepo.Save();
                            }
                            else if (cmbToGroupName.Text == "Proprietor")
                            {
                                try
                                {
                                    ProprietorLedgerRepository remainingpayrepo = new ProprietorLedgerRepository();
                                    ProprietorLedger remainingdata = new ProprietorLedger();
                                    remainingdata = remainingpayrepo.GetAll().Where(t => t.ProprietorId == Convert.ToInt32(cmbToAccountName.SelectedValue) && t.CreateDate.Value.ToShortDateString() == Jdata.JEDate.Value.ToShortDateString() && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    remainingdata.ProprietorId = Convert.ToInt32(cmbToAccountName.SelectedValue);
                                    remainingdata.ProprietorName = cmbToAccountName.Text;
                                    remainingdata.CreateDate = dtpJEdate.Value;
                                    remainingdata.Type = "Receive From";
                                    remainingdata.NetAmount = Convert.ToDecimal(txtpaidamount.Text);
                                    remainingdata.PaidAmount = Convert.ToDecimal(0);
                                    remainingdata.TransactionYear = CommonMethod.TransactionYear;
                                    remainingdata.Reasons = "Journal Entry";
                                    remainingdata.CompId = CommonMethod.CompId;
                                    remainingpayrepo.Edit(remainingdata);
                                    remainingpayrepo.Save();
                                }
                                catch (Exception)
                                { }
                            }
                            else if (cmbToGroupName.Text == "Bank Account")
                            {
                                BankStatementRepository bankrepo = new BankStatementRepository();
                                BankStatement bankstdata = bankrepo.GetAll().Where(t => t.BankId == Convert.ToInt32(cmbToAccountName.SelectedValue) && t.Bankname == cmbToAccountName.Text && t.TransactionDate.Value.ToShortDateString() == Jdata.JEDate.Value.ToShortDateString() && t.TransactionType == "Journal Entry" && t.Deposits == Convert.ToDecimal(Jdata.Amount) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                bankstdata.BankId = Convert.ToInt32(cmbToAccountName.SelectedValue);
                                bankstdata.Bankname = Convert.ToString(cmbToAccountName.Text);
                                bankstdata.TransactionDate = dtpJEdate.Value;
                                bankstdata.Particulars = "Receive From " + cmbFromAccountName.Text.Trim(); ;
                                bankstdata.ChequeNo = Convert.ToString("");
                                bankstdata.TransactionType = "Journal Entry";
                                BankRepository brepo = new BankRepository();
                                var bdata = brepo.GetAll().Where(t => t.BankId == Convert.ToInt32(cmbToAccountName.SelectedValue) && t.Bankname == cmbToAccountName.Text).FirstOrDefault();
                                if (!string.IsNullOrEmpty(Convert.ToString(bdata)))
                                    bankstdata.ACGId = Convert.ToInt32(bdata.ACGId);
                                bankstdata.Deposits = Convert.ToDecimal(txtpaidamount.Text);
                                bankstdata.Balance = 0;
                                bankstdata.CompId = CommonMethod.CompId;
                                bankrepo.Edit(bankstdata);
                                bankrepo.Save();
                            }
                            expencedata.JENo = Convert.ToInt32(txtJENo.Text);
                            expencedata.Id = Journalid;
                            expencerepo.Edit(expencedata);
                            expencerepo.Save();
                            MessageBox.Show("Journal Entry Updated.", "Success");
                            clear();
                            this.ActiveControl = btnnew;
                        }
                        else
                        {
                            if (cmbFromGroupName.Text == "Supplier")
                            {
                                SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                                SupplierRemainingPayment remainingdata = new SupplierRemainingPayment();
                                maxsuppid();
                                remainingdata.BillReceiptNo = Convert.ToInt32(maxsuppreceiptno);
                                remainingdata.SupplierId = Convert.ToInt32(cmbFromAccountName.SelectedValue);
                                remainingdata.Suppliername = cmbFromAccountName.Text;
                                remainingdata.CreateDate = dtpJEdate.Value;
                                remainingdata.Type = "Send To";
                                remainingdata.RemainingAmt = Convert.ToDecimal(0);
                                remainingdata.NetAmt = Convert.ToDecimal(0);
                                remainingdata.PaidAmt = Convert.ToDecimal(txtpaidamount.Text);
                                remainingdata.TransactionYear = CommonMethod.TransactionYear;
                                remainingdata.CompId = CommonMethod.CompId;
                                remainingdata.ReceiptBy = CommonMethod.CompId;
                                remainingpayrepo.Add(remainingdata);
                                remainingpayrepo.Save();
                            }
                            else if (cmbFromGroupName.Text == "Customer")
                            {
                                RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                                RemainingPayment remainingdata = new RemainingPayment();
                                Maxcustid();
                                remainingdata.BillReceiptNo = Convert.ToInt32(maxcustreceiptno);
                                remainingdata.CustomerId = Convert.ToInt32(cmbFromAccountName.SelectedValue);
                                remainingdata.Customername = cmbFromAccountName.Text;
                                remainingdata.CreateDate = dtpJEdate.Value;
                                remainingdata.Type = "Send To";
                                remainingdata.RemainingAmt = Convert.ToDecimal(0);
                                remainingdata.NetAmt = Convert.ToDecimal(0);
                                remainingdata.PaidAmt = Convert.ToDecimal(txtpaidamount.Text);
                                remainingdata.TransactionYear = CommonMethod.TransactionYear;
                                remainingdata.CompId = CommonMethod.CompId;
                                remainingpayrepo.Add(remainingdata);
                                remainingpayrepo.Save();
                            }
                            else if (cmbFromGroupName.Text == "Expence")
                            {
                                ExpenceRepository ExpenceRepo = new ExpenceRepository();
                                Expence ExpenceData = new Expence();
                                maxexpencid();
                                ExpenceMasterRepository emastRepo = new ExpenceMasterRepository();
                                var emastdata = emastRepo.GetAll().Where(t => t.ExpenceName == cmbFromAccountName.Text).FirstOrDefault();
                                if (!string.IsNullOrEmpty(Convert.ToString(emastdata)))
                                    ExpenceData.ACGId = emastdata.ACGId;
                                ExpenceData.ExpenceID = Convert.ToInt32(maxexpenceno);
                                ExpenceData.ExpenceDate = Convert.ToDateTime(dtpJEdate.Value);
                                ExpenceData.ExpenceName = cmbFromAccountName.Text;
                                ExpenceData.FromPerson = "";
                                ExpenceData.ToPerson = "Send To " + cmbToAccountName.Text.Trim();
                                ExpenceData.Reason = "Journal Entry";
                                ExpenceData.PaidAmount = Convert.ToDouble(txtpaidamount.Text) * -1;
                                ExpenceData.TransactionYear = CommonMethod.TransactionYear;
                                ExpenceData.CompId = CommonMethod.CompId;
                                ExpenceRepo.Add(ExpenceData);
                                ExpenceRepo.Save();
                            }
                            else if (cmbFromGroupName.Text == "Other Income")
                            {
                                IncomeRepository IncomeRepo = new IncomeRepository();
                                Income Incomedata = new Income();
                                maxincomeid();
                                IncomeMasterRepository imastrepo = new IncomeMasterRepository();
                                var imastdata = imastrepo.GetAll().Where(t => t.IncomeName == cmbFromAccountName.Text).FirstOrDefault();
                                if (!string.IsNullOrEmpty(Convert.ToString(imastdata)))
                                    Incomedata.ACGId = Convert.ToInt32(imastdata.ACGId);
                                Incomedata.IncomeID = Convert.ToInt32(maxincomeno);
                                Incomedata.IncomeDate = Convert.ToDateTime(dtpJEdate.Value);
                                Incomedata.IncomeMID = Convert.ToInt32(cmbFromAccountName.SelectedValue);
                                Incomedata.FromPerson = "Send To " + cmbToAccountName.Text.Trim();
                                Incomedata.Reason = "Journal Entry";
                                Incomedata.PaidAmount = Convert.ToDouble(txtpaidamount.Text) * -1;
                                Incomedata.TransactionYear = CommonMethod.TransactionYear;
                                Incomedata.CompId = CommonMethod.CompId;
                                IncomeRepo.Add(Incomedata);
                                IncomeRepo.Save();
                            }
                            else if (cmbFromGroupName.Text == "Bank Account")
                            {
                                BankStatementRepository bankrepo = new BankStatementRepository();
                                BankStatement bankstdata = new BankStatement();
                                bankstdata.BankId = Convert.ToInt32(cmbFromAccountName.SelectedValue);
                                bankstdata.Bankname = Convert.ToString(cmbFromAccountName.Text);
                                bankstdata.TransactionDate = dtpJEdate.Value;
                                bankstdata.Particulars = "Send To " + cmbToAccountName.Text.Trim();
                                bankstdata.ChequeNo = Convert.ToString("");
                                bankstdata.TransactionType = "Journal Entry";
                                BankRepository brepo = new BankRepository();
                                var bdata = brepo.GetAll().Where(t => t.BankId == Convert.ToInt32(cmbFromAccountName.SelectedValue) && t.Bankname == cmbFromAccountName.Text).FirstOrDefault();
                                if (!string.IsNullOrEmpty(Convert.ToString(bdata)))
                                    bankstdata.ACGId = Convert.ToInt32(bdata.ACGId);
                                bankstdata.Withdrawals = Convert.ToDecimal(txtpaidamount.Text);
                                bankstdata.Balance = 0;
                                bankstdata.CompId = CommonMethod.CompId;
                                bankrepo.Add(bankstdata);
                                bankrepo.Save();
                            }

                            if (cmbToGroupName.Text == "Supplier")
                            {
                                SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                                SupplierRemainingPayment remainingdata = new SupplierRemainingPayment();
                                maxsuppid();
                                remainingdata.BillReceiptNo = Convert.ToInt32(maxsuppreceiptno);
                                remainingdata.SupplierId = Convert.ToInt32(cmbToAccountName.SelectedValue);
                                remainingdata.Suppliername = cmbToAccountName.Text;
                                remainingdata.CreateDate = dtpJEdate.Value;
                                remainingdata.Type = "Receive From";
                                remainingdata.RemainingAmt = Convert.ToDecimal(0);
                                remainingdata.NetAmt = Convert.ToDecimal(txtpaidamount.Text);
                                remainingdata.PaidAmt = Convert.ToDecimal(0);
                                remainingdata.TransactionYear = CommonMethod.TransactionYear;
                                remainingdata.CompId = CommonMethod.CompId;
                                remainingdata.ReceiptBy = CommonMethod.CompId;
                                remainingpayrepo.Add(remainingdata);
                                remainingpayrepo.Save();
                            }
                            else if (cmbToGroupName.Text == "Customer")
                            {
                                RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                                RemainingPayment remainingdata = new RemainingPayment();
                                Maxcustid();
                                remainingdata.BillReceiptNo = Convert.ToInt32(maxcustreceiptno);
                                remainingdata.CustomerId = Convert.ToInt32(cmbToAccountName.SelectedValue);
                                remainingdata.Customername = cmbToAccountName.Text;
                                remainingdata.CreateDate = dtpJEdate.Value;
                                remainingdata.Type = "Receive From";
                                remainingdata.RemainingAmt = Convert.ToDecimal(0);
                                remainingdata.NetAmt = Convert.ToDecimal(txtpaidamount.Text);
                                remainingdata.PaidAmt = Convert.ToDecimal(0);
                                remainingdata.TransactionYear = CommonMethod.TransactionYear;
                                remainingdata.CompId = CommonMethod.CompId;
                                remainingpayrepo.Add(remainingdata);
                                remainingpayrepo.Save();
                            }
                            else if (cmbToGroupName.Text == "Expence")
                            {
                                ExpenceRepository ExpenceRepo = new ExpenceRepository();
                                Expence ExpenceData = new Expence();
                                maxexpencid();
                                ExpenceMasterRepository emastRepo = new ExpenceMasterRepository();
                                var emastdata = emastRepo.GetAll().Where(t => t.ExpenceName == cmbToAccountName.Text).FirstOrDefault();
                                if (!string.IsNullOrEmpty(Convert.ToString(emastdata)))
                                    ExpenceData.ACGId = emastdata.ACGId;
                                ExpenceData.ExpenceID = Convert.ToInt32(maxexpenceno);
                                ExpenceData.ExpenceDate = Convert.ToDateTime(dtpJEdate.Value);
                                ExpenceData.ExpenceName = cmbToAccountName.Text;
                                ExpenceData.FromPerson = "";
                                ExpenceData.ToPerson = "Receive From " + cmbFromAccountName.Text.Trim();
                                ExpenceData.Reason = "Journal Entry";
                                ExpenceData.PaidAmount = Convert.ToDouble(txtpaidamount.Text);
                                ExpenceData.TransactionYear = CommonMethod.TransactionYear;
                                ExpenceData.CompId = CommonMethod.CompId;
                                ExpenceRepo.Add(ExpenceData);
                                ExpenceRepo.Save();
                            }
                            else if (cmbToGroupName.Text == "Other Income")
                            {
                                IncomeRepository IncomeRepo = new IncomeRepository();
                                Income Incomedata = new Income();
                                maxincomeid();
                                IncomeMasterRepository imastrepo = new IncomeMasterRepository();
                                var imastdata = imastrepo.GetAll().Where(t => t.IncomeName == cmbToAccountName.Text).FirstOrDefault();
                                if (!string.IsNullOrEmpty(Convert.ToString(imastdata)))
                                    Incomedata.ACGId = Convert.ToInt32(imastdata.ACGId);
                                Incomedata.IncomeID = Convert.ToInt32(maxincomeno);
                                Incomedata.IncomeDate = Convert.ToDateTime(dtpJEdate.Value);
                                Incomedata.IncomeMID = Convert.ToInt32(cmbToAccountName.SelectedValue);
                                Incomedata.FromPerson = "Receive From " + cmbFromAccountName.Text.Trim(); ;
                                Incomedata.Reason = "Journal Entry";
                                Incomedata.PaidAmount = Convert.ToDouble(txtpaidamount.Text);
                                Incomedata.TransactionYear = CommonMethod.TransactionYear;
                                Incomedata.CompId = CommonMethod.CompId;
                                IncomeRepo.Add(Incomedata);
                                IncomeRepo.Save();
                            }
                            else if (cmbToGroupName.Text == "Proprietor")
                            {
                                try
                                {
                                    ProprietorLedgerRepository remainingpayrepo = new ProprietorLedgerRepository();
                                    ProprietorLedger remainingdata = new ProprietorLedger();
                                    MaxProid();
                                    remainingdata.ReceiptNo = Convert.ToInt32(MaxPropRecptId);
                                    remainingdata.ProprietorId = Convert.ToInt32(cmbToAccountName.SelectedValue);
                                    remainingdata.ProprietorName = cmbToAccountName.Text;
                                    remainingdata.CreateDate = dtpJEdate.Value;
                                    remainingdata.Type = "Receive From";
                                    remainingdata.Reasons = "Journal Entry";
                                    remainingdata.NetAmount = Convert.ToDecimal(txtpaidamount.Text);
                                    remainingdata.PaidAmount = Convert.ToDecimal(0);
                                    remainingdata.TransactionYear = CommonMethod.TransactionYear;
                                    remainingdata.CompId = CommonMethod.CompId;
                                    remainingpayrepo.Add(remainingdata);
                                    remainingpayrepo.Save();
                                }
                                catch (Exception)
                                { }
                            }
                            else if (cmbToGroupName.Text == "Bank Account")
                            {
                                BankStatementRepository bankrepo = new BankStatementRepository();
                                BankStatement bankstdata = new BankStatement();
                                bankstdata.BankId = Convert.ToInt32(cmbToAccountName.SelectedValue);
                                bankstdata.Bankname = Convert.ToString(cmbToAccountName.Text);
                                bankstdata.TransactionDate = dtpJEdate.Value;
                                bankstdata.Particulars = "Receive From " + cmbFromAccountName.Text.Trim(); ;
                                bankstdata.ChequeNo = Convert.ToString("");
                                bankstdata.TransactionType = "Journal Entry";
                                BankRepository brepo = new BankRepository();
                                var bdata = brepo.GetAll().Where(t => t.BankId == Convert.ToInt32(cmbToAccountName.SelectedValue) && t.Bankname == cmbToAccountName.Text).FirstOrDefault();
                                if (!string.IsNullOrEmpty(Convert.ToString(bdata)))
                                    bankstdata.ACGId = Convert.ToInt32(bdata.ACGId);
                                bankstdata.Deposits = Convert.ToDecimal(txtpaidamount.Text);
                                bankstdata.Balance = 0;
                                bankstdata.CompId = CommonMethod.CompId;
                                bankrepo.Add(bankstdata);
                                bankrepo.Save();
                            }
                            maxJEid();
                            expencedata.JENo = Convert.ToInt32(txtJENo.Text);
                            expencerepo.Add(expencedata);
                            expencerepo.Save();
                            MessageBox.Show("Journal Entry Saved.", "Success");
                            clear();
                            this.ActiveControl = btnnew;
                        }
                        expencerepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Enter Paid Amount.", "Warning");
                        this.ActiveControl = txtpaidamount;
                    }
                }
                else
                {
                    if (cmbFromAccountName.Text == "Select" || cmbFromAccountName.Text == "")
                    {
                        MessageBox.Show("Select From Account Name.", "Warning");
                        this.ActiveControl = cmbFromAccountName;
                    }
                    else
                    {
                        MessageBox.Show("Select To Account Name.", "Warning");
                        this.ActiveControl = cmbToAccountName;
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
                BindJEinfo();
                dtpfromdate.Value = DateTime.Now.Date;
                dtptodate.Value = DateTime.Now.Date;
                chkdate.Checked = false;
                chkFromGrpNameS.Checked = false;
                chkToGrpNameS.Checked = false;
                cmbFromGrpNameSearch.SelectedIndex = -1;
                cmbToGrpNameSearch.SelectedIndex = -1;
            }
            catch (Exception)
            { }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkdate.Checked == true)
                {
                    if (dtpfromdate.Value.Date <= dtptodate.Value.Date)
                    {
                        if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                        {
                            JournalEntryRepository expencerepo = new JournalEntryRepository();
                            GvJEInfo.Rows.Clear();
                            int i = 0;
                            List<JournalEntry> ItemList = expencerepo.GetAll().Where(t => t.JEDate.Value.Date >= dtpfromdate.Value.Date && t.JEDate.Value.Date <= dtptodate.Value.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                            label13.Text = Convert.ToString(Convert.ToDecimal(ItemList.Sum(t => t.Amount)));
                            if (ItemList != null)
                            {
                                foreach (var item in ItemList)
                                {
                                    GvJEInfo.Rows.Add();
                                    GvJEInfo.Rows[i].Cells["JENo"].Value = item.JENo;
                                    GvJEInfo.Rows[i].Cells["Date"].Value = item.JEDate.Value.ToShortDateString();
                                    GvJEInfo.Rows[i].Cells["FromGroup"].Value = item.FromGroup;
                                    GvJEInfo.Rows[i].Cells["FromAccountName"].Value = item.FromAccountName;
                                    GvJEInfo.Rows[i].Cells["ToGroup"].Value = item.ToGroup;
                                    GvJEInfo.Rows[i].Cells["ToAccountName"].Value = item.ToAccountName;
                                    GvJEInfo.Rows[i].Cells["PaidAmt"].Value = item.Amount;
                                    GvJEInfo.Rows[i].Cells["Id"].Value = item.Id;
                                    i++;
                                }
                            }
                            this.ActiveControl = dtptodate;
                            expencerepo.Dispose();
                        }
                        else
                        {
                            JournalEntryRepository expencerepo = new JournalEntryRepository();
                            GvJEInfo.Rows.Clear();
                            int i = 0;
                            List<JournalEntry> ItemList = expencerepo.GetAll().Where(t => t.JEDate.Value.Date == dtpfromdate.Value.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                            label13.Text = Convert.ToString(Convert.ToDecimal(ItemList.Sum(t => t.Amount)));
                            if (ItemList != null)
                            {
                                foreach (var item in ItemList)
                                {
                                    GvJEInfo.Rows.Add();
                                    GvJEInfo.Rows[i].Cells["JENo"].Value = item.JENo;
                                    GvJEInfo.Rows[i].Cells["Date"].Value = item.JEDate.Value.ToShortDateString();
                                    GvJEInfo.Rows[i].Cells["FromGroup"].Value = item.FromGroup;
                                    GvJEInfo.Rows[i].Cells["FromAccountName"].Value = item.FromAccountName;
                                    GvJEInfo.Rows[i].Cells["ToGroup"].Value = item.ToGroup;
                                    GvJEInfo.Rows[i].Cells["ToAccountName"].Value = item.ToAccountName;
                                    GvJEInfo.Rows[i].Cells["PaidAmt"].Value = item.Amount;
                                    GvJEInfo.Rows[i].Cells["Id"].Value = item.Id;
                                    i++;
                                }
                            }
                            this.ActiveControl = dtptodate;
                            expencerepo.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("To Date Should greater than from date or equal to from date.", "Warning");
                        this.ActiveControl = dtptodate;
                    }
                }
                else if (chkFromGrpNameS.Checked == true)
                {
                    if (cmbFromGrpNameSearch.Text != "Select" && cmbFromGrpNameSearch.Text != "")
                    {
                        if (chkFromAccNameS.Checked == true)
                        {
                            if (cmbFromAccNameSearch.Text != "Select" && cmbFromAccNameSearch.Text != "")
                            {
                                if (chkToGrpNameS.Checked == true)
                                {
                                    if (cmbToGrpNameSearch.Text != "Select" && cmbToGrpNameSearch.Text != "")
                                    {
                                        if (chkToAccNameS.Checked == true)
                                        {
                                            if (cmbToAccNameSearch.Text != "Select" && cmbToAccNameSearch.Text != "")
                                            {
                                                JournalEntryRepository expencerepo = new JournalEntryRepository();
                                                GvJEInfo.Rows.Clear();
                                                int i = 0;
                                                List<JournalEntry> ItemList = expencerepo.GetAll().Where(t => t.FromGroup == cmbFromGrpNameSearch.Text && t.FromAccountName == cmbFromAccNameSearch.Text && t.FromAccountId == Convert.ToInt32(cmbFromAccNameSearch.SelectedValue) && t.ToGroup == cmbToGrpNameSearch.Text && t.ToAccountName == cmbToAccNameSearch.Text && t.ToAccountId == Convert.ToInt32(cmbToAccNameSearch.SelectedValue) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                                if (ItemList != null)
                                                {
                                                    foreach (var item in ItemList)
                                                    {
                                                        GvJEInfo.Rows.Add();
                                                        GvJEInfo.Rows[i].Cells["JENo"].Value = item.JENo;
                                                        GvJEInfo.Rows[i].Cells["Date"].Value = item.JEDate.Value.ToShortDateString();
                                                        GvJEInfo.Rows[i].Cells["FromGroup"].Value = item.FromGroup;
                                                        GvJEInfo.Rows[i].Cells["FromAccountName"].Value = item.FromAccountName;
                                                        GvJEInfo.Rows[i].Cells["ToGroup"].Value = item.ToGroup;
                                                        GvJEInfo.Rows[i].Cells["ToAccountName"].Value = item.ToAccountName;
                                                        GvJEInfo.Rows[i].Cells["PaidAmt"].Value = item.Amount;
                                                        GvJEInfo.Rows[i].Cells["Id"].Value = item.Id;
                                                        i++;
                                                    }
                                                    label13.Text = Convert.ToString(Convert.ToDecimal(ItemList.Sum(t => t.Amount)));
                                                }
                                                expencerepo.Dispose();
                                            }
                                            else
                                            {
                                                MessageBox.Show("Select To Account Name.", "Warning");
                                                this.ActiveControl = cmbToAccNameSearch;
                                            }
                                        }
                                        else
                                        {
                                            JournalEntryRepository expencerepo = new JournalEntryRepository();
                                            GvJEInfo.Rows.Clear();
                                            int i = 0;
                                            List<JournalEntry> ItemList = expencerepo.GetAll().Where(t => t.FromGroup == cmbFromGrpNameSearch.Text && t.FromAccountName == cmbFromAccNameSearch.Text && t.FromAccountId == Convert.ToInt32(cmbFromAccNameSearch.SelectedValue) && t.ToGroup == cmbToGrpNameSearch.Text && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                            if (ItemList != null)
                                            {
                                                foreach (var item in ItemList)
                                                {
                                                    GvJEInfo.Rows.Add();
                                                    GvJEInfo.Rows[i].Cells["JENo"].Value = item.JENo;
                                                    GvJEInfo.Rows[i].Cells["Date"].Value = item.JEDate.Value.ToShortDateString();
                                                    GvJEInfo.Rows[i].Cells["FromGroup"].Value = item.FromGroup;
                                                    GvJEInfo.Rows[i].Cells["FromAccountName"].Value = item.FromAccountName;
                                                    GvJEInfo.Rows[i].Cells["ToGroup"].Value = item.ToGroup;
                                                    GvJEInfo.Rows[i].Cells["ToAccountName"].Value = item.ToAccountName;
                                                    GvJEInfo.Rows[i].Cells["PaidAmt"].Value = item.Amount;
                                                    GvJEInfo.Rows[i].Cells["Id"].Value = item.Id;
                                                    i++;
                                                }
                                                label13.Text = Convert.ToString(Convert.ToDecimal(ItemList.Sum(t => t.Amount)));
                                            }
                                            expencerepo.Dispose();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Select To Group Name.", "Warning");
                                        this.ActiveControl = cmbToGrpNameSearch;
                                    }
                                }
                                else
                                {
                                    JournalEntryRepository expencerepo = new JournalEntryRepository();
                                    GvJEInfo.Rows.Clear();
                                    int i = 0;
                                    List<JournalEntry> ItemList = expencerepo.GetAll().Where(t => t.FromGroup == cmbFromGrpNameSearch.Text && t.FromAccountName == cmbFromAccNameSearch.Text && t.FromAccountId == Convert.ToInt32(cmbFromAccNameSearch.SelectedValue) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                    if (ItemList != null)
                                    {
                                        foreach (var item in ItemList)
                                        {
                                            GvJEInfo.Rows.Add();
                                            GvJEInfo.Rows[i].Cells["JENo"].Value = item.JENo;
                                            GvJEInfo.Rows[i].Cells["Date"].Value = item.JEDate.Value.ToShortDateString();
                                            GvJEInfo.Rows[i].Cells["FromGroup"].Value = item.FromGroup;
                                            GvJEInfo.Rows[i].Cells["FromAccountName"].Value = item.FromAccountName;
                                            GvJEInfo.Rows[i].Cells["ToGroup"].Value = item.ToGroup;
                                            GvJEInfo.Rows[i].Cells["ToAccountName"].Value = item.ToAccountName;
                                            GvJEInfo.Rows[i].Cells["PaidAmt"].Value = item.Amount;
                                            GvJEInfo.Rows[i].Cells["Id"].Value = item.Id;
                                            i++;
                                        }
                                        label13.Text = Convert.ToString(Convert.ToDecimal(ItemList.Sum(t => t.Amount)));
                                    }
                                    expencerepo.Dispose();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Select From Account Name.", "Warning");
                                this.ActiveControl = cmbFromAccNameSearch;
                            }
                        }
                        else
                        {
                            JournalEntryRepository expencerepo = new JournalEntryRepository();
                            GvJEInfo.Rows.Clear();
                            int i = 0;
                            List<JournalEntry> ItemList = expencerepo.GetAll().Where(t => t.FromGroup == cmbFromGrpNameSearch.Text && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                            if (ItemList != null)
                            {
                                foreach (var item in ItemList)
                                {
                                    GvJEInfo.Rows.Add();
                                    GvJEInfo.Rows[i].Cells["JENo"].Value = item.JENo;
                                    GvJEInfo.Rows[i].Cells["Date"].Value = item.JEDate.Value.ToShortDateString();
                                    GvJEInfo.Rows[i].Cells["FromGroup"].Value = item.FromGroup;
                                    GvJEInfo.Rows[i].Cells["FromAccountName"].Value = item.FromAccountName;
                                    GvJEInfo.Rows[i].Cells["ToGroup"].Value = item.ToGroup;
                                    GvJEInfo.Rows[i].Cells["ToAccountName"].Value = item.ToAccountName;
                                    GvJEInfo.Rows[i].Cells["PaidAmt"].Value = item.Amount;
                                    GvJEInfo.Rows[i].Cells["Id"].Value = item.Id;
                                    i++;
                                }
                                label13.Text = Convert.ToString(Convert.ToDecimal(ItemList.Sum(t => t.Amount)));
                            }
                            expencerepo.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select From Group Name.", "Warning");
                        this.ActiveControl = cmbFromGrpNameSearch;
                    }
                }
                else if (chkToGrpNameS.Checked == true)
                {
                    if (cmbToGrpNameSearch.Text != "Select" && cmbToGrpNameSearch.Text != "")
                    {
                        if (chkToAccNameS.Checked == true)
                        {
                            if (cmbToAccNameSearch.Text != "Select" && cmbToAccNameSearch.Text != "")
                            {
                                JournalEntryRepository expencerepo = new JournalEntryRepository();
                                GvJEInfo.Rows.Clear();
                                int i = 0;
                                List<JournalEntry> ItemList = expencerepo.GetAll().Where(t => t.ToGroup == cmbToGrpNameSearch.Text && t.ToAccountName == cmbToAccNameSearch.Text && t.ToAccountId == Convert.ToInt32(cmbFromAccNameSearch.SelectedValue) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                if (ItemList != null)
                                {
                                    foreach (var item in ItemList)
                                    {
                                        GvJEInfo.Rows.Add();
                                        GvJEInfo.Rows[i].Cells["JENo"].Value = item.JENo;
                                        GvJEInfo.Rows[i].Cells["Date"].Value = item.JEDate.Value.ToShortDateString();
                                        GvJEInfo.Rows[i].Cells["FromGroup"].Value = item.FromGroup;
                                        GvJEInfo.Rows[i].Cells["FromAccountName"].Value = item.FromAccountName;
                                        GvJEInfo.Rows[i].Cells["ToGroup"].Value = item.ToGroup;
                                        GvJEInfo.Rows[i].Cells["ToAccountName"].Value = item.ToAccountName;
                                        GvJEInfo.Rows[i].Cells["PaidAmt"].Value = item.Amount;
                                        GvJEInfo.Rows[i].Cells["Id"].Value = item.Id;
                                        i++;
                                    }
                                    label13.Text = Convert.ToString(Convert.ToDecimal(ItemList.Sum(t => t.Amount)));
                                }
                                expencerepo.Dispose();
                            }
                            else
                            {
                                MessageBox.Show("Select From Account Name.", "Warning");
                                this.ActiveControl = cmbToAccNameSearch;
                            }
                        }
                        else
                        {
                            JournalEntryRepository expencerepo = new JournalEntryRepository();
                            GvJEInfo.Rows.Clear();
                            int i = 0;
                            List<JournalEntry> ItemList = expencerepo.GetAll().Where(t => t.ToGroup == cmbToGrpNameSearch.Text && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                            if (ItemList != null)
                            {
                                foreach (var item in ItemList)
                                {
                                    GvJEInfo.Rows.Add();
                                    GvJEInfo.Rows[i].Cells["JENo"].Value = item.JENo;
                                    GvJEInfo.Rows[i].Cells["Date"].Value = item.JEDate.Value.ToShortDateString();
                                    GvJEInfo.Rows[i].Cells["FromGroup"].Value = item.FromGroup;
                                    GvJEInfo.Rows[i].Cells["FromAccountName"].Value = item.FromAccountName;
                                    GvJEInfo.Rows[i].Cells["ToGroup"].Value = item.ToGroup;
                                    GvJEInfo.Rows[i].Cells["ToAccountName"].Value = item.ToAccountName;
                                    GvJEInfo.Rows[i].Cells["PaidAmt"].Value = item.Amount;
                                    GvJEInfo.Rows[i].Cells["Id"].Value = item.Id;
                                    i++;
                                }
                                label13.Text = Convert.ToString(Convert.ToDecimal(ItemList.Sum(t => t.Amount)));
                            }
                            expencerepo.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select From Group Name.", "Warning");
                        this.ActiveControl = cmbToGrpNameSearch;
                    }
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
                btnsave_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                JournalEntryRepository expenceRepo = new JournalEntryRepository();
                JournalEntry expencemast = expenceRepo.GetById(Journalid);
                if (cmbFromGroupName.Text == "Supplier")
                {
                    SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                    SupplierRemainingPayment remainingdata = remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbFromAccountName.SelectedValue) && t.Suppliername == cmbFromAccountName.Text && t.Type == "Send To" && t.CreateDate.Value.ToShortDateString() == expencemast.JEDate.Value.ToShortDateString() && t.PaidAmt == expencemast.Amount && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();

                    remainingpayrepo.Remove(remainingdata);
                    remainingpayrepo.Save();
                }
                else if (cmbFromGroupName.Text == "Customer")
                {
                    RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                    RemainingPayment remainingdata = remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbFromAccountName.SelectedValue) && t.Customername == cmbFromAccountName.Text && t.Type == "Send To" && t.CreateDate.Value.ToShortDateString() == expencemast.JEDate.Value.ToShortDateString() && t.PaidAmt == expencemast.Amount && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();

                    remainingpayrepo.Remove(remainingdata);
                    remainingpayrepo.Save();
                }
                else if (cmbFromGroupName.Text == "Expence")
                {
                    ExpenceRepository ExpenceRepo = new ExpenceRepository();
                    Expence ExpenceData = ExpenceRepo.GetAll().Where(t => t.ExpenceName == cmbFromAccountName.Text && t.ExpenceDate.Value.ToShortDateString() == expencemast.JEDate.Value.ToShortDateString() && t.Reason == "Journal Entry" && t.PaidAmount == Convert.ToDouble(expencemast.Amount * -1) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();

                    ExpenceRepo.Remove(ExpenceData);
                    ExpenceRepo.Save();
                }
                else if (cmbFromGroupName.Text == "Other Income")
                {
                    IncomeRepository IncomeRepo = new IncomeRepository();
                    Income Incomedata = IncomeRepo.GetAll().Where(t => t.IncomeMID == Convert.ToInt32(cmbFromAccountName.SelectedValue) && t.IncomeDate.Value.ToShortDateString() == expencemast.JEDate.Value.ToShortDateString() && t.Reason == "Journal Entry" && t.PaidAmount == Convert.ToDouble(expencemast.Amount) * -1 && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();

                    IncomeRepo.Remove(Incomedata);
                    IncomeRepo.Save();
                }
                else if (cmbFromGroupName.Text == "Bank Account")
                {
                    BankStatementRepository bankrepo = new BankStatementRepository();
                    BankStatement bankstdata = bankrepo.GetAll().Where(t => t.BankId == Convert.ToInt32(cmbFromAccountName.SelectedValue) && t.Bankname == cmbFromAccountName.Text && t.TransactionDate.Value.ToShortDateString() == expencemast.JEDate.Value.ToShortDateString() && t.TransactionType == "Journal Entry" && t.Withdrawals == Convert.ToDecimal(expencemast.Amount) && t.CompId == CommonMethod.CompId).FirstOrDefault();

                    bankrepo.Remove(bankstdata);
                    bankrepo.Save();
                }
                if (cmbToGroupName.Text == "Supplier")
                {
                    SupplierRemainingPaymentRepository remainingpayrepo = new SupplierRemainingPaymentRepository();
                    SupplierRemainingPayment remainingdata = remainingpayrepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbToAccountName.SelectedValue) && t.Suppliername == cmbToAccountName.Text && t.Type == "Receive From" && t.CreateDate.Value.ToShortDateString() == expencemast.JEDate.Value.ToShortDateString() && t.NetAmt == expencemast.Amount && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();

                    remainingpayrepo.Remove(remainingdata);
                    remainingpayrepo.Save();
                }
                else if (cmbToGroupName.Text == "Customer")
                {
                    RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                    RemainingPayment remainingdata = remainingpayrepo.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbToAccountName.SelectedValue) && t.Customername == cmbToAccountName.Text && t.Type == "Receive From" && t.CreateDate.Value.ToShortDateString() == expencemast.JEDate.Value.ToShortDateString() && t.NetAmt == expencemast.Amount && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();

                    remainingpayrepo.Remove(remainingdata);
                    remainingpayrepo.Save();
                }
                else if (cmbToGroupName.Text == "Expence")
                {
                    ExpenceRepository ExpenceRepo = new ExpenceRepository();
                    Expence ExpenceData = ExpenceRepo.GetAll().Where(t => t.ExpenceName == cmbToAccountName.Text && t.ExpenceDate.Value.ToShortDateString() == expencemast.JEDate.Value.ToShortDateString() && t.Reason == "Journal Entry" && t.PaidAmount == Convert.ToDouble(expencemast.Amount) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();

                    ExpenceRepo.Remove(ExpenceData);
                    ExpenceRepo.Save();
                }
                else if (cmbToGroupName.Text == "Other Income")
                {
                    IncomeRepository IncomeRepo = new IncomeRepository();
                    Income Incomedata = IncomeRepo.GetAll().Where(t => t.IncomeMID == Convert.ToInt32(cmbToAccountName.SelectedValue) && t.IncomeDate.Value.ToShortDateString() == expencemast.JEDate.Value.ToShortDateString() && t.Reason == "Journal Entry" && t.PaidAmount == Convert.ToDouble(expencemast.Amount) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();

                    IncomeRepo.Remove(Incomedata);
                    IncomeRepo.Save();
                }
                else if (cmbToGroupName.Text == "Proprietor")
                {
                    ProprietorLedgerRepository remainingpayrepo = new ProprietorLedgerRepository();
                    ProprietorLedger remainingdata = new ProprietorLedger();
                    remainingdata = remainingpayrepo.GetAll().Where(t => t.ProprietorId == Convert.ToInt32(cmbToAccountName.SelectedValue) && t.CreateDate.Value.ToShortDateString() == expencemast.JEDate.Value.ToShortDateString() && t.Reasons == "Journal Entry" && t.NetAmount == expencemast.Amount && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    remainingpayrepo.Remove(remainingdata);
                    remainingpayrepo.Save();
                }
                else if (cmbToGroupName.Text == "Bank Account")
                {
                    BankStatementRepository bankrepo = new BankStatementRepository();
                    BankStatement bankstdata = bankrepo.GetAll().Where(t => t.BankId == Convert.ToInt32(cmbToAccountName.SelectedValue) && t.Bankname == cmbToAccountName.Text && t.TransactionDate.Value.ToShortDateString() == expencemast.JEDate.Value.ToShortDateString() && t.TransactionType == "Journal Entry" && t.Deposits == Convert.ToDecimal(expencemast.Amount) && t.CompId == CommonMethod.CompId).FirstOrDefault();

                    bankrepo.Remove(bankstdata);
                    bankrepo.Save();
                }
                expenceRepo.Remove(expencemast);
                expenceRepo.Save();
                MessageBox.Show("Record deleted Sucessfully.", "Success");
                clear();
                btndelete.Enabled = false;
                this.ActiveControl = btnnew;
                expenceRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void txtpaidamount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtpaidamount.Text == "")
                {
                    txtpaidamount.Text = "0";
                    this.ActiveControl = txtpaidamount;
                }
            }
            catch (Exception)
            { }
        }

        private void chkdate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkdate.Checked)
                {
                    chkFromGrpNameS.Checked = false;
                    chkToGrpNameS.Checked = false;
                }
            }
            catch (Exception)
            { }
        }

        private void chkFromgroupName_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkFromGrpNameS.Checked)
                    chkdate.Checked = false;
            }
            catch (Exception)
            { }
        }

        private void chktogroupname_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkToGrpNameS.Checked)
                    chkdate.Checked = false;
            }
            catch (Exception)
            { }
        }

        private void cmbToACGroups_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtpaidamount;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        if (cmbToGrpNameSearch.Text == "Supplier")
                        {
                            frmSupplierInformation supplier = new frmSupplierInformation();
                            supplier.ShowDialog();
                            supplierdata();
                            this.ActiveControl = cmbToAccountName;
                        }
                        else if (cmbToGrpNameSearch.Text == "Customer")
                        {
                            frmcustomer customer = new frmcustomer();
                            customer.ShowDialog();
                            customerdata();
                            this.ActiveControl = cmbToAccountName;
                        }
                        else if (cmbToGrpNameSearch.Text == "Expence")
                        {
                            frmExpenceMaster expence = new frmExpenceMaster();
                            expence.ShowDialog();
                            expencedata();
                            this.ActiveControl = cmbToAccountName;
                        }
                        else if (cmbToGrpNameSearch.Text == "Other Income")
                        {
                            frmIncomeMaster income = new frmIncomeMaster();
                            income.ShowDialog();
                            Otherincomedata();
                            this.ActiveControl = cmbToAccountName;
                        }
                        else if (cmbToGrpNameSearch.Text == "Bank Accounts")
                        {
                            frmBankInformation bank = new frmBankInformation();
                            bank.ShowDialog();
                            bankdata();
                            this.ActiveControl = cmbToAccountName;
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtpaidamount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsave;
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

        private void chkdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpfromdate;
            }
            catch (Exception)
            { }
        }

        private void cmbFromGroupname1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chkFromgroupName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbFromGrpNameSearch;
            }
            catch (Exception)
            { }
        }

        private void cmbToGroupname1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chktogroupname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbToGrpNameSearch;
            }
            catch (Exception)
            { }
        }

        private void cmbaccountFrom1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbFromAccountName;
            }
            catch (Exception)
            { }
        }

        private void dtpJEdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbFromGroupName;
            }
            catch (Exception)
            { }
        }

        private void cmbaccountfrom2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbToAccountName;
            }
            catch (Exception)
            { }
        }

        private void cmbaccountFrom1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbFromGroupName.Text == "Supplier" || cmbFromGroupName.Text == "Customer" || cmbFromGroupName.Text == "Expence" || cmbFromGroupName.Text == "Other Income" || cmbFromGroupName.Text == "PL Account" || cmbFromGroupName.Text == "Bank Account")
                {
                    cmbFromAccountName.Enabled = true;
                    if (cmbFromGroupName.Text == "Supplier")
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
                            cmbFromAccountName.DataSource = customerdata;
                            cmbFromAccountName.ValueMember = "SupplierCode";
                            cmbFromAccountName.DisplayMember = "SupplierName";
                            custrepo.Dispose();
                            label20.Text = "From Supplier Name:";
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbFromGroupName.Text == "Customer")
                    {
                        try
                        {
                            CustomerRepository custrepo = new CustomerRepository();
                            List<CustomerInformation> customerdata = new List<CustomerInformation>();
                            customerdata.Clear();
                            customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CustomerName).ToList();
                            customerdata.Add(new CustomerInformation { CustomerName = "Select", CustomerID = 0 });
                            customerdata.Reverse();
                            cmbFromAccountName.DataSource = customerdata;
                            cmbFromAccountName.ValueMember = "CustomerID";
                            cmbFromAccountName.DisplayMember = "CustomerName";
                            custrepo.Dispose();
                            label20.Text = "From Customer Name:";
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbFromGroupName.Text == "Expence")
                    {
                        try
                        {
                            ExpenceMasterRepository expmastRepo = new ExpenceMasterRepository();
                            List<ExpenceMaster> expdata = new List<ExpenceMaster>();
                            expdata.Clear();
                            expdata = expmastRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.ExpenceName).ToList();
                            expdata.Add(new ExpenceMaster { ExpenceName = "Select", ExpenceId = 0 });
                            expdata.Reverse();
                            cmbFromAccountName.DataSource = expdata;
                            cmbFromAccountName.ValueMember = "ExpenceId";
                            cmbFromAccountName.DisplayMember = "ExpenceName";
                            expmastRepo.Dispose();
                            label20.Text = "From Expence Name:";
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbFromGroupName.Text == "Other Income")
                    {
                        try
                        {
                            IncomeMasterRepository expmastRepo = new IncomeMasterRepository();
                            List<IncomeMaster> expdata = new List<IncomeMaster>();
                            expdata.Clear();
                            expdata = expmastRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.IncomeName).ToList();
                            expdata.Add(new IncomeMaster { IncomeName = "Select", IncomeId = 0 });
                            expdata.Reverse();
                            cmbFromAccountName.DataSource = expdata;
                            cmbFromAccountName.ValueMember = "IncomeId";
                            cmbFromAccountName.DisplayMember = "IncomeName";
                            expmastRepo.Dispose();
                            label20.Text = "From OtherIncome Name:";
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbFromGroupName.Text == "PL Account")
                    {
                        try
                        {
                            cmbFromAccountName.DataSource = null;
                            cmbFromAccountName.Items.Clear();
                            cmbFromAccountName.DisplayMember = "Text";
                            cmbFromAccountName.ValueMember = "Value";
                            cmbFromAccountName.Items.Add(new { Text = "Profit And Loss A/C", Value = 0 });
                            cmbFromAccountName.SelectedIndex = 0;
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbFromGroupName.Text == "Bank Account")
                    {
                        try
                        {
                            BankRepository BankRepo = new BankRepository();
                            List<BankInformation> bankdata = new List<BankInformation>();
                            bankdata.Clear();
                            bankdata = BankRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.Bankname).ToList();
                            bankdata.Add(new BankInformation { Bankname = "Select", BankId = 0 });
                            bankdata.Reverse();
                            cmbFromAccountName.DataSource = bankdata;
                            cmbFromAccountName.ValueMember = "BankId";
                            cmbFromAccountName.DisplayMember = "Bankname";
                            BankRepo.Dispose();
                            label20.Text = "From Bank Name:";
                        }
                        catch (Exception)
                        { }
                    }
                }
                else
                {
                    cmbFromAccountName.Enabled = false;
                    cmbFromAccountName.SelectedIndex = 0;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbaccountfrom2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbToGroupName.Text == "Supplier" || cmbToGroupName.Text == "Customer" || cmbToGroupName.Text == "Expence" || cmbToGroupName.Text == "Other Income" || cmbToGroupName.Text == "Proprietor" || cmbToGroupName.Text == "Bank Account")
                {
                    cmbToAccountName.Enabled = true;
                    if (cmbToGroupName.Text == "Supplier")
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
                            cmbToAccountName.DataSource = customerdata;
                            cmbToAccountName.ValueMember = "SupplierCode";
                            cmbToAccountName.DisplayMember = "SupplierName";
                            custrepo.Dispose();
                            label2.Text = "To Supplier Name:";
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbToGroupName.Text == "Customer")
                    {
                        try
                        {
                            CustomerRepository custrepo = new CustomerRepository();
                            List<CustomerInformation> customerdata = new List<CustomerInformation>();
                            customerdata.Clear();
                            customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CustomerName).ToList();
                            customerdata.Add(new CustomerInformation { CustomerName = "Select", CustomerID = 0 });
                            customerdata.Reverse();
                            cmbToAccountName.DataSource = customerdata;
                            cmbToAccountName.ValueMember = "CustomerID";
                            cmbToAccountName.DisplayMember = "CustomerName";
                            custrepo.Dispose();
                            label2.Text = "To Customer Name:";
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbToGroupName.Text == "Expence")
                    {
                        try
                        {
                            ExpenceMasterRepository expmastRepo = new ExpenceMasterRepository();
                            List<ExpenceMaster> expdata = new List<ExpenceMaster>();
                            expdata.Clear();
                            expdata = expmastRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.ExpenceName).ToList();
                            expdata.Add(new ExpenceMaster { ExpenceName = "Select", ExpenceId = 0 });
                            expdata.Reverse();
                            cmbToAccountName.DataSource = expdata;
                            cmbToAccountName.ValueMember = "ExpenceId";
                            cmbToAccountName.DisplayMember = "ExpenceName";
                            expmastRepo.Dispose();
                            label2.Text = "To Expence Name:";
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbToGroupName.Text == "Other Income")
                    {
                        try
                        {
                            IncomeMasterRepository expmastRepo = new IncomeMasterRepository();
                            List<IncomeMaster> expdata = new List<IncomeMaster>();
                            expdata.Clear();
                            expdata = expmastRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.IncomeName).ToList();
                            expdata.Add(new IncomeMaster { IncomeName = "Select", IncomeId = 0 });
                            expdata.Reverse();
                            cmbToAccountName.DataSource = expdata;
                            cmbToAccountName.ValueMember = "IncomeId";
                            cmbToAccountName.DisplayMember = "IncomeName";
                            expmastRepo.Dispose();
                            label2.Text = "To OtherIncome Name:";
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbToGroupName.Text == "Proprietor")
                    {
                        try
                        {
                            ProprietorMasterRepository custrepo = new ProprietorMasterRepository();
                            List<ProprietorMaster> customerdata = new List<ProprietorMaster>();
                            customerdata.Clear();
                            customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId && t.ACGId == t.AccountingGroupMaster.Id).OrderByDescending(t => t.ProprietorName).ToList();
                            customerdata.Add(new ProprietorMaster { ProprietorName = "Select", ProprietorId = 0 });
                            customerdata.Reverse();
                            cmbToAccountName.DataSource = customerdata;
                            cmbToAccountName.ValueMember = "ProprietorId";
                            cmbToAccountName.DisplayMember = "ProprietorName";
                            custrepo.Dispose();
                            label2.Text = "To Proprietor Name:";
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbToGroupName.Text == "Bank Account")
                    {
                        try
                        {
                            BankRepository BankRepo = new BankRepository();
                            List<BankInformation> bankdata = new List<BankInformation>();
                            bankdata.Clear();
                            bankdata = BankRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.Bankname).ToList();
                            bankdata.Add(new BankInformation { Bankname = "Select", BankId = 0 });
                            bankdata.Reverse();
                            cmbToAccountName.DataSource = bankdata;
                            cmbToAccountName.ValueMember = "BankId";
                            cmbToAccountName.DisplayMember = "Bankname";
                            BankRepo.Dispose();
                            label2.Text = "To Bank Name:";
                        }
                        catch (Exception)
                        { }
                    }
                }
                else
                {
                    cmbToAccountName.Enabled = false;
                    cmbToAccountName.SelectedIndex = 0;
                }
            }
            catch (Exception)
            { }
        }

        public void maxincomeid()
        {
            try
            {
                DataTable dt = db.GetTable("Select max(IncomeID) from Income where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                if (dt.Rows.Count > 0)
                {
                    string maxincid = dt.Rows[0][0].ToString();
                    if (maxincid == "")
                        maxincomeno = "1";
                    else
                        maxincomeno = Convert.ToString(Convert.ToInt32(maxid) + 1);
                }
                dt.Dispose();
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
                    string maxid1 = dt.Rows[0][0].ToString();
                    if (maxid1 == "")
                        maxexpenceno = "1";
                    else
                        maxexpenceno = Convert.ToString(Convert.ToInt32(maxid) + 1);
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

        public void MaxProid()
        {
            try
            {
                ProprietorLedgerRepository remainingpayrepo = new ProprietorLedgerRepository();
                var mid = remainingpayrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.ReceiptNo);
                if (mid == null)
                    MaxPropRecptId = "1";
                else
                    MaxPropRecptId = Convert.ToString(Convert.ToInt32(mid) + 1);
                remainingpayrepo.Dispose();
            }
            catch (Exception)
            { }
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
                cmbFromAccNameSearch.DataSource = customerdata;
                cmbFromAccNameSearch.ValueMember = "SupplierCode";
                cmbFromAccNameSearch.DisplayMember = "SupplierName";
                custrepo.Dispose();
                cmbFromAccNameSearch.SelectedIndex = 0;
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
                cmbFromAccNameSearch.DataSource = customerdata;
                cmbFromAccNameSearch.ValueMember = "CustomerID";
                cmbFromAccNameSearch.DisplayMember = "CustomerName";
                custrepo.Dispose();
                cmbFromAccNameSearch.SelectedIndex = 0;
            }
            catch (Exception)
            { }
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
                cmbFromAccNameSearch.DataSource = expdata;
                cmbFromAccNameSearch.ValueMember = "ExpenceId";
                cmbFromAccNameSearch.DisplayMember = "ExpenceName";
                expmastRepo.Dispose();
                cmbFromAccNameSearch.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void Otherincomedata()
        {
            try
            {
                IncomeMasterRepository expmastRepo = new IncomeMasterRepository();
                List<IncomeMaster> expdata = new List<IncomeMaster>();
                expdata.Clear();
                expdata = expmastRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.IncomeName).ToList();
                expdata.Add(new IncomeMaster { IncomeName = "Select", IncomeId = 0 });
                expdata.Reverse();
                cmbFromAccNameSearch.DataSource = expdata;
                cmbFromAccNameSearch.ValueMember = "IncomeId";
                cmbFromAccNameSearch.DisplayMember = "IncomeName";
                expmastRepo.Dispose();
                cmbFromAccNameSearch.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void bankdata()
        {
            try
            {
                BankRepository expmastRepo = new BankRepository();
                List<BankInformation> expdata = new List<BankInformation>();
                expdata.Clear();
                expdata = expmastRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.Bankname).ToList();
                expdata.Add(new BankInformation { Bankname = "Select", BankId = 0 });
                expdata.Reverse();
                cmbFromAccNameSearch.DataSource = expdata;
                cmbFromAccNameSearch.ValueMember = "BankId";
                cmbFromAccNameSearch.DisplayMember = "Bankname";
                expmastRepo.Dispose();
                cmbFromAccNameSearch.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void Proprietordata()
        {
            try
            {
                ProprietorMasterRepository custrepo = new ProprietorMasterRepository();
                List<ProprietorMaster> customerdata = new List<ProprietorMaster>();
                customerdata.Clear();
                customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId && t.ACGId == t.AccountingGroupMaster.Id).OrderByDescending(t => t.ProprietorName).ToList();
                customerdata.Add(new ProprietorMaster { ProprietorName = "Select", ProprietorId = 0 });
                customerdata.Reverse();
                cmbToAccountName.DataSource = customerdata;
                cmbToAccountName.ValueMember = "ProprietorId";
                cmbToAccountName.DisplayMember = "ProprietorName";
                custrepo.Dispose();
                cmbToAccountName.SelectedIndex = 0;
            }
            catch (Exception) { }
        }

        public void supplierdata1()
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
                cmbToAccNameSearch.DataSource = customerdata;
                cmbToAccNameSearch.ValueMember = "SupplierCode";
                cmbToAccNameSearch.DisplayMember = "SupplierName";
                custrepo.Dispose();
                cmbToAccNameSearch.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void customerdata1()
        {
            try
            {
                CustomerRepository custrepo = new CustomerRepository();
                List<CustomerInformation> customerdata = new List<CustomerInformation>();
                customerdata.Clear();
                customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CustomerName).ToList();
                customerdata.Add(new CustomerInformation { CustomerName = "Select", CustomerID = 0 });
                customerdata.Reverse();
                cmbToAccNameSearch.DataSource = customerdata;
                cmbToAccNameSearch.ValueMember = "CustomerID";
                cmbToAccNameSearch.DisplayMember = "CustomerName";
                custrepo.Dispose();
                cmbToAccNameSearch.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void expencedata1()
        {
            try
            {
                ExpenceMasterRepository expmastRepo = new ExpenceMasterRepository();
                List<ExpenceMaster> expdata = new List<ExpenceMaster>();
                expdata.Clear();
                expdata = expmastRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.ExpenceName).ToList();
                expdata.Add(new ExpenceMaster { ExpenceName = "Select", ExpenceId = 0 });
                expdata.Reverse();
                cmbToAccNameSearch.DataSource = expdata;
                cmbToAccNameSearch.ValueMember = "ExpenceId";
                cmbToAccNameSearch.DisplayMember = "ExpenceName";
                expmastRepo.Dispose();
                cmbToAccNameSearch.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void Otherincomedata1()
        {
            try
            {
                IncomeMasterRepository expmastRepo = new IncomeMasterRepository();
                List<IncomeMaster> expdata = new List<IncomeMaster>();
                expdata.Clear();
                expdata = expmastRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.IncomeName).ToList();
                expdata.Add(new IncomeMaster { IncomeName = "Select", IncomeId = 0 });
                expdata.Reverse();
                cmbToAccNameSearch.DataSource = expdata;
                cmbToAccNameSearch.ValueMember = "IncomeId";
                cmbToAccNameSearch.DisplayMember = "IncomeName";
                expmastRepo.Dispose();
                cmbToAccNameSearch.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void Proprietordata1()
        {
            try
            {
                ProprietorMasterRepository custrepo = new ProprietorMasterRepository();
                List<ProprietorMaster> customerdata = new List<ProprietorMaster>();
                customerdata.Clear();
                customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId && t.ACGId == t.AccountingGroupMaster.Id).OrderByDescending(t => t.ProprietorName).ToList();
                customerdata.Add(new ProprietorMaster { ProprietorName = "Select", ProprietorId = 0 });
                customerdata.Reverse();
                cmbToAccNameSearch.DataSource = customerdata;
                cmbToAccNameSearch.ValueMember = "ProprietorId";
                cmbToAccNameSearch.DisplayMember = "ProprietorName";
                custrepo.Dispose();
                cmbToAccNameSearch.SelectedIndex = 0;
            }
            catch (Exception) { }
        }

        private void cmbFromGrpNameSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbFromGrpNameSearch.Text == "Supplier" || cmbFromGrpNameSearch.Text == "Customer" || cmbFromGrpNameSearch.Text == "Expence" || cmbFromGrpNameSearch.Text == "Other Income" || cmbFromGrpNameSearch.Text == "PL Account")
                {
                    cmbFromAccNameSearch.Enabled = true;
                    if (cmbFromGrpNameSearch.Text == "Supplier")
                    {
                        supplierdata();
                        label20.Text = "From Supplier Name:";
                    }
                    else if (cmbFromGrpNameSearch.Text == "Customer")
                    {
                        customerdata();
                        label20.Text = "From Customer Name:";
                    }
                    else if (cmbFromGrpNameSearch.Text == "Expence")
                    {
                        expencedata();
                        label20.Text = "From Expence Name:";
                    }
                    else if (cmbFromGrpNameSearch.Text == "Other Income")
                    {
                        Otherincomedata();
                        label20.Text = "From OtherIncome Name:";
                    }
                    else if (cmbFromGrpNameSearch.Text == "PL Account")
                    {
                        try
                        {
                            cmbFromAccNameSearch.DataSource = null;
                            cmbFromAccNameSearch.DisplayMember = "Text";
                            cmbFromAccNameSearch.ValueMember = "Value";
                            cmbFromAccNameSearch.Items.Clear();
                            cmbFromAccNameSearch.Items.Add(new { Text = "Profit And Loss A/C", Value = 0 });
                            cmbFromAccNameSearch.SelectedIndex = 0;
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbFromGrpNameSearch.Text == "Bank Account")
                    {
                        bankdata();
                        label20.Text = "From Bank Name:";
                    }
                }
                else
                {
                    cmbFromAccNameSearch.Enabled = false;
                    cmbFromAccNameSearch.SelectedIndex = 0;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbToGrpNameSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbToGrpNameSearch.Text == "Supplier" || cmbToGrpNameSearch.Text == "Customer" || cmbToGrpNameSearch.Text == "Expence" || cmbToGrpNameSearch.Text == "Other Income" || cmbToGrpNameSearch.Text == "Proprietor" || cmbToGrpNameSearch.Text == "Bank Account")
                {
                    cmbToAccNameSearch.Enabled = true;
                    if (cmbToGrpNameSearch.Text == "Supplier")
                    {
                        try
                        {
                            supplierdata1();
                            label2.Text = "To Supplier Name:";
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbToGrpNameSearch.Text == "Customer")
                    {
                        try
                        {
                            customerdata1();
                            label2.Text = "To Customer Name:";
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbToGrpNameSearch.Text == "Expence")
                    {
                        try
                        {
                            expencedata1();
                            label2.Text = "To Expence Name:";
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbToGrpNameSearch.Text == "Other Income")
                    {
                        try
                        {
                            Otherincomedata1();
                            label2.Text = "To OtherIncome Name:";
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbToGrpNameSearch.Text == "Proprietor")
                    {
                        try
                        {
                            Proprietordata1();
                            label2.Text = "To Proprietor Name:";
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbToGrpNameSearch.Text == "Bank Account")
                    {
                        try
                        {
                            bankdata();
                            label2.Text = "To Bank Name:";
                        }
                        catch (Exception)
                        { }
                    }
                }
                else
                {
                    cmbToAccNameSearch.Enabled = false;
                    cmbToAccNameSearch.SelectedIndex = 0;
                }
            }
            catch (Exception)
            { }
        }

        private void btnprintall_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkdate.Checked == true)
                {
                    if (dtpfromdate.Value.Date <= dtptodate.Value.Date)
                    {
                        if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                        {
                            CommonMethod cm = new CommonMethod();
                            RptJournalEntry journalentryrpt = new RptJournalEntry(cm.ValidFromDate(dtpfromdate.Value.Date), "", 0, cm.ValidToDate(dtptodate.Value.Date), "", 0, "dft");
                            journalentryrpt.ShowDialog();
                            journalentryrpt.Dispose();
                        }
                        else if (dtpfromdate.Value.Date == dtptodate.Value.Date)
                        {
                            CommonMethod cm = new CommonMethod();
                            RptJournalEntry journalentryrpt = new RptJournalEntry(cm.ValidFromDate(dtpfromdate.Value.Date), "", 0, "", "", 0, "d");
                            journalentryrpt.ShowDialog();
                            journalentryrpt.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("To Date Should greater than from date or equal to from date.", "Warning");
                        this.ActiveControl = dtptodate;
                    }
                }
                else if (chkFromGrpNameS.Checked == true)
                {
                    if (cmbFromGrpNameSearch.Text != "Select" && cmbFromGrpNameSearch.Text != "")
                    {
                        if (chkFromAccNameS.Checked == true)
                        {
                            if (cmbFromAccNameSearch.Text != "Select" && cmbFromAccNameSearch.Text != "")
                            {
                                if (chkToGrpNameS.Checked == true)
                                {
                                    if (cmbToGrpNameSearch.Text != "Select" && cmbToGrpNameSearch.Text != "")
                                    {
                                        if (chkToAccNameS.Checked == true)
                                        {
                                            if (cmbToAccNameSearch.Text != "Select" && cmbToAccNameSearch.Text != "")
                                            {
                                                RptJournalEntry journalentryrpt = new RptJournalEntry(cmbFromGrpNameSearch.Text, cmbFromAccNameSearch.Text, Convert.ToInt32(cmbFromAccNameSearch.SelectedValue), cmbToGrpNameSearch.Text, cmbToAccNameSearch.Text, Convert.ToInt32(cmbToAccNameSearch.SelectedValue), "fata");
                                                journalentryrpt.ShowDialog();
                                                journalentryrpt.Dispose();
                                            }
                                            else
                                            {
                                                MessageBox.Show("Select To Account Name.", "Warning");
                                                this.ActiveControl = cmbToAccNameSearch;
                                            }
                                        }
                                        else
                                        {
                                            RptJournalEntry journalentryrpt = new RptJournalEntry(cmbFromGrpNameSearch.Text, cmbFromAccNameSearch.Text, Convert.ToInt32(cmbFromAccNameSearch.SelectedValue), cmbToGrpNameSearch.Text, "", 0, "fat");
                                            journalentryrpt.ShowDialog();
                                            journalentryrpt.Dispose();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Select To Group Name.", "Warning");
                                        this.ActiveControl = cmbToGrpNameSearch;
                                    }
                                }
                                else
                                {
                                    RptJournalEntry journalentryrpt = new RptJournalEntry(cmbFromGrpNameSearch.Text, cmbFromAccNameSearch.Text, Convert.ToInt32(cmbFromAccNameSearch.SelectedValue), "", "", 0, "fa");
                                    journalentryrpt.ShowDialog();
                                    journalentryrpt.Dispose();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Select From Account Name.", "Warning");
                                this.ActiveControl = cmbFromAccNameSearch;
                            }
                        }
                        else
                        {
                            RptJournalEntry journalentryrpt = new RptJournalEntry(cmbFromGrpNameSearch.Text, "", 0, "", "", 0, "f");
                            journalentryrpt.ShowDialog();
                            journalentryrpt.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select From Group Name.", "Warning");
                        this.ActiveControl = cmbFromGrpNameSearch;
                    }
                }
                else if (chkToGrpNameS.Checked == true)
                {
                    if (cmbToGrpNameSearch.Text != "Select" && cmbToGrpNameSearch.Text != "")
                    {
                        if (chkToAccNameS.Checked == true)
                        {
                            if (cmbToAccNameSearch.Text != "Select" && cmbToAccNameSearch.Text != "")
                            {
                                RptJournalEntry journalentryrpt = new RptJournalEntry("", "", 0, cmbToGrpNameSearch.Text, cmbToAccNameSearch.Text, Convert.ToInt32(cmbToAccNameSearch.SelectedValue), "ta");
                                journalentryrpt.ShowDialog();
                                journalentryrpt.Dispose();
                            }
                            else
                            {
                                MessageBox.Show("Select From Account Name.", "Warning");
                                this.ActiveControl = cmbToAccNameSearch;
                            }
                        }
                        else
                        {
                            RptJournalEntry journalentryrpt = new RptJournalEntry("", "", 0, cmbToGrpNameSearch.Text, "", 0, "t");
                            journalentryrpt.ShowDialog();
                            journalentryrpt.Dispose();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select From Group Name.", "Warning");
                        this.ActiveControl = cmbToGrpNameSearch;
                    }
                }
                else
                {
                    RptJournalEntry journalentryrpt = new RptJournalEntry("", "", 0, "", "", 0, "ga");
                    journalentryrpt.ShowDialog();
                    journalentryrpt.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void chkFromAccNameS_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkFromAccNameS.Checked == true)
                    chkdate.Checked = false;
            }
            catch (Exception)
            { }
        }

        private void chkToAccNameS_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkToAccNameS.Checked == true)
                    chkdate.Checked = false;
            }
            catch (Exception)
            { }
        }

        private void cmbFromAccountName_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbToGroupName;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        if (cmbFromGroupName.Text == "Supplier")
                        {
                            frmSupplierInformation supplier = new frmSupplierInformation();
                            supplier.ShowDialog();
                            supplierdata();
                            this.ActiveControl = cmbFromAccountName;
                        }
                        else if (cmbFromGroupName.Text == "Customer")
                        {
                            frmcustomer customer = new frmcustomer();
                            customer.ShowDialog();
                            customerdata();
                            this.ActiveControl = cmbFromAccountName;
                        }
                        else if (cmbFromGroupName.Text == "Expence")
                        {
                            frmExpenceMaster expence = new frmExpenceMaster();
                            expence.ShowDialog();
                            expencedata();
                            this.ActiveControl = cmbFromAccountName;
                        }
                        else if (cmbFromGroupName.Text == "Other Income")
                        {
                            frmIncomeMaster income = new frmIncomeMaster();
                            income.ShowDialog();
                            Otherincomedata();
                            this.ActiveControl = cmbFromAccountName;
                        }
                        else if (cmbFromGroupName.Text == "Bank Accounts")
                        {
                            frmBankInformation bank = new frmBankInformation();
                            bank.ShowDialog();
                            bankdata();
                            this.ActiveControl = cmbFromAccountName;
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        private void chkFromAccNameS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbFromAccNameSearch;
            }
            catch (Exception) { }
        }

        private void chkToAccNameS_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbToGrpNameSearch;
            }
            catch (Exception) { }
        }

        private void cmbFromAccNameSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception) { }
        }

        private void cmbToAccNameSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception) { }
        }

        private void GvJEInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnsave.Text = "Update";
                txtJENo.Text = Convert.ToString(GvJEInfo.Rows[e.RowIndex].Cells["JENo"].Value);
                dtpJEdate.Value = Convert.ToDateTime(GvJEInfo.Rows[e.RowIndex].Cells["Date"].Value);
                cmbFromGroupName.Text = Convert.ToString(GvJEInfo.Rows[e.RowIndex].Cells["FromGroup"].Value);
                cmbFromAccountName.Text = Convert.ToString(GvJEInfo.Rows[e.RowIndex].Cells["FromAccountName"].Value);
                cmbToGroupName.Text = Convert.ToString(GvJEInfo.Rows[e.RowIndex].Cells["ToGroup"].Value);
                cmbToAccountName.Text = Convert.ToString(GvJEInfo.Rows[e.RowIndex].Cells["ToAccountName"].Value);
                txtpaidamount.Text = Convert.ToString(GvJEInfo.Rows[e.RowIndex].Cells["PaidAmt"].Value);
                Journalid = Convert.ToInt32(GvJEInfo.Rows[e.RowIndex].Cells["Id"].Value);
                btndelete.Enabled = true;
                if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
                {
                    btnsave.Enabled = false;
                    btndelete.Enabled = false;
                    btnnew.Enabled = false;
                }
                cmbFromGroupName.Enabled = false;
                cmbToGroupName.Enabled = false;
                cmbFromAccountName.Enabled = false;
                cmbToAccountName.Enabled = false;
            }
            catch (Exception)
            { }
        }
    }
}