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
    public partial class frmOtherIncome : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        public int expenceid;
        public string ACGName1 = "", Incomename = "", maxid;
        DateTime fdate, tdate;

        public frmOtherIncome()
        {
            InitializeComponent();
            maxexpencid();
            BindAcGroup();
            fillcombo();
            fillexp();
            fillincomesearch();
            Bindexpenceinfo();
            btndelete.Enabled = false;
            cm.changedatetimepickerrange(this);
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
            {
                btnsave.Enabled = false;
                btndelete.Enabled = false;
                btnnew.Enabled = false;
            }
        }

        public frmOtherIncome(string incomename, DateTime from, DateTime to)
        {
            InitializeComponent();
            maxexpencid();
            BindAcGroup();
            fillcombo();
            fillexp();
            fillincomesearch();
            Bindexpenceinfo(incomename, from, to);
            btndelete.Enabled = false;
            cm.changedatetimepickerrange(this);
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
            {
                btnsave.Enabled = false;
                btndelete.Enabled = false;
                btnnew.Enabled = false;
            }
        }

        public frmOtherIncome(string incomename, DateTime from, DateTime to, string ACCGName)
        {
            InitializeComponent();
            maxexpencid();
            BindAcGroup();
            fillcombo();
            fillexp();
            fillincomesearch();
            Incomename = incomename;
            fdate = from;
            tdate = to;
            ACGName1 = ACCGName;
            TPLACtoOtherIncome(incomename, from, to, ACCGName);
            btndelete.Enabled = false;
            cm.changedatetimepickerrange(this);
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
            {
                btnsave.Enabled = false;
                btndelete.Enabled = false;
                btnnew.Enabled = false;
            }
        }

        public void BindAcGroup()
        {
            try
            {
                AccountingGroupMasterRepository ACGRepo = new AccountingGroupMasterRepository();
                List<AccountingGroupMaster> ACGdata = new List<AccountingGroupMaster>();
                ACGdata.Clear();
                ACGdata = ACGRepo.GetAll().OrderByDescending(t => t.ACGName).ToList();
                ACGdata.Add(new AccountingGroupMaster { ACGName = "Select", Id = 0 });
                ACGdata.Reverse();
                cmbACGroups.DataSource = ACGdata;
                cmbACGroups.ValueMember = "Id";
                cmbACGroups.DisplayMember = "ACGName";
                ACGRepo.Dispose();
                cmbACGroups.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void maxexpencid()
        {
            try
            {
                DataTable dt = db.GetTable("Select max(IncomeID) from Income where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                if (dt.Rows.Count > 0)
                {
                    maxid = dt.Rows[0][0].ToString();
                    if (maxid == "")
                        txtexpenceno.Text = "1";
                    else
                        txtexpenceno.Text = Convert.ToString(Convert.ToInt32(maxid) + 1);
                }
                dt.Dispose();
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
                cmbbankname.DataSource = bankdata;
                cmbbankname.ValueMember = "BankId";
                cmbbankname.DisplayMember = "Bankname";
                bankrepo.Dispose();
                cmbbankname.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            txtfrom.Text = "";
            txtto.Text = "";
            txtreason.Text = "";
            txtpaidamount.Text = "";
            txtchequeno.Text = "";
            cmbbankname.Enabled = false;
            txtchequeno.Enabled = false;
            maxexpencid();
            Bindexpenceinfo();
            btnsave.Text = "Save";
            dtpchequedate.Enabled = false; btndelete.Enabled = false;
            BindAcGroup();
            fillexp();
            fillincomesearch();
            fillcombo();
            cmbpaymentmode.SelectedIndex = -1;
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
            {
                btnsave.Enabled = false;
                btndelete.Enabled = false;
                btnnew.Enabled = false;
            }
            try
            {
                dtpincomedate.Value = DateTime.Now.Date;
                dtpchequedate.Value = DateTime.Now.Date;
            }
            catch (Exception)
            { }
        }

        public void Bindexpenceinfo(string incomename, DateTime from, DateTime to)
        {
            try
            {
                IncomeRepository IncomeRepo = new IncomeRepository();
                GvexpenceInfo.Rows.Clear();
                int i = 0;
                List<Income> ItemList = IncomeRepo.GetAll().Where(t => t.IncomeMaster.IncomeName == incomename && t.IncomeDate.Value.Date >= from.Date && t.IncomeDate.Value.Date <= to.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                label13.Text = Convert.ToString(Convert.ToDecimal(ItemList.Sum(t => t.PaidAmount)));
                if (ItemList != null)
                {
                    foreach (var item in ItemList)
                    {
                        GvexpenceInfo.Rows.Add();
                        GvexpenceInfo.Rows[i].Cells["Date"].Value = item.IncomeDate.Value.ToShortDateString();
                        GvexpenceInfo.Rows[i].Cells["IncomeId"].Value = item.IncomeID;
                        if (item.ACGId == null)
                            GvexpenceInfo.Rows[i].Cells["ACGroup"].Value = "";
                        else
                            GvexpenceInfo.Rows[i].Cells["ACGroup"].Value = item.AccountingGroupMaster.ACGName;
                        GvexpenceInfo.Rows[i].Cells["IncomeNameg"].Value = item.IncomeMaster.IncomeName;
                        GvexpenceInfo.Rows[i].Cells["From"].Value = item.FromPerson;
                        GvexpenceInfo.Rows[i].Cells["To"].Value = item.ToPerson;
                        GvexpenceInfo.Rows[i].Cells["Reason"].Value = item.Reason;
                        GvexpenceInfo.Rows[i].Cells["Mode"].Value = item.PaymentMode;
                        GvexpenceInfo.Rows[i].Cells["bankname"].Value = item.Bankname;
                        GvexpenceInfo.Rows[i].Cells["ChqUTRNo"].Value = item.ChequeNo;
                        GvexpenceInfo.Rows[i].Cells["ChqNEFTDate"].Value = item.ChequeDate;
                        GvexpenceInfo.Rows[i].Cells["PaidAmount"].Value = item.PaidAmount;
                        GvexpenceInfo.Rows[i].Cells["Id"].Value = item.Id;
                        GvexpenceInfo.Rows[i].Cells["ACGId"].Value = item.ACGId;
                        i++;
                    }
                }
                IncomeRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void Bindexpenceinfo()
        {
            try
            {
                IncomeRepository IncomeRepo = new IncomeRepository();
                GvexpenceInfo.Rows.Clear();
                List<Income> ItemList = IncomeRepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                label13.Text = Convert.ToString(Convert.ToDecimal(ItemList.Sum(t => t.PaidAmount)));
                if (ItemList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        GvexpenceInfo.Rows.Add();
                        GvexpenceInfo.Rows[i].Cells["Date"].Value = item.IncomeDate.Value.ToShortDateString();
                        GvexpenceInfo.Rows[i].Cells["IncomeId"].Value = item.IncomeID;
                        try
                        {
                            GvexpenceInfo.Rows[i].Cells["ACGroup"].Value = item.AccountingGroupMaster.ACGName;
                        }
                        catch (Exception)
                        { }
                        GvexpenceInfo.Rows[i].Cells["IncomeNameg"].Value = item.IncomeMaster.IncomeName;
                        GvexpenceInfo.Rows[i].Cells["From"].Value = item.FromPerson;
                        GvexpenceInfo.Rows[i].Cells["To"].Value = item.ToPerson;
                        GvexpenceInfo.Rows[i].Cells["Reason"].Value = item.Reason;
                        GvexpenceInfo.Rows[i].Cells["Mode"].Value = item.PaymentMode;
                        GvexpenceInfo.Rows[i].Cells["bankname"].Value = item.Bankname;
                        GvexpenceInfo.Rows[i].Cells["ChqUTRNo"].Value = item.ChequeNo;
                        GvexpenceInfo.Rows[i].Cells["ChqNEFTDate"].Value = item.ChequeDate;
                        GvexpenceInfo.Rows[i].Cells["PaidAmount"].Value = item.PaidAmount;
                        GvexpenceInfo.Rows[i].Cells["Id"].Value = item.Id;
                        GvexpenceInfo.Rows[i].Cells["ACGId"].Value = item.ACGId;
                        i++;
                    }
                }
                IncomeRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtpaidamount.Text.Trim() != "" && cmbincome.Text.Trim() != "" && txtfrom.Text.Trim() != "" && txtto.Text.Trim() != "" && cmbACGroups.Text != "Select" && cmbACGroups.Text != "" && cmbpaymentmode.Text != "")
                {
                    IncomeRepository IncomeRepo = new IncomeRepository();
                    Income expencedata = new Income();
                    expencedata.IncomeDate = Convert.ToDateTime(dtpincomedate.Value);
                    expencedata.IncomeMID = Convert.ToInt32(cmbincome.SelectedValue);
                    expencedata.IncomeName = Convert.ToString(cmbincome.Text);
                    expencedata.FromPerson = txtfrom.Text.Trim();
                    expencedata.ToPerson = txtto.Text.Trim();
                    expencedata.Reason = txtreason.Text.Trim();
                    expencedata.PaymentMode = cmbpaymentmode.Text;
                    expencedata.PaidAmount = Convert.ToDouble(txtpaidamount.Text);
                    expencedata.TransactionYear = CommonMethod.TransactionYear;
                    expencedata.CompId = CommonMethod.CompId;
                    expencedata.ACGId = Convert.ToInt32(cmbACGroups.SelectedValue.ToString());
                    if (btnsave.Text == "Update")
                    {
                        if (cmbpaymentmode.Text == "Cash")
                        {
                            expencedata.IncomeID = Convert.ToInt32(txtexpenceno.Text);
                            expencedata.Bankname = "";
                            expencedata.ChequeNo = "";
                            expencedata.ChequeDate = null;
                            expencedata.PaidAmount = Convert.ToDouble(txtpaidamount.Text);
                            expencedata.Id = expenceid;
                            IncomeRepo.Edit(expencedata);
                            IncomeRepo.Save();
                            MessageBox.Show("Income Information Updated.", "Success");
                            clear();
                            this.ActiveControl = btnnew;
                        }
                        else if (cmbpaymentmode.Text == "Cheque")
                        {
                            if (cmbbankname.Text != "Select" && txtchequeno.Text != "")
                            {
                                try
                                {
                                    BankStatementRepository BankStRepo = new BankStatementRepository();
                                    IncomeRepository expencerepo1 = new IncomeRepository();
                                    Income expencedata1 = expencerepo1.GetById(expenceid);
                                    var bdata = BankStRepo.GetAll().Where(t => t.Bankname == expencedata1.Bankname && t.CompId == CommonMethod.CompId && t.TransactionDate == expencedata1.IncomeDate && t.ChequeNo == expencedata1.ChequeNo && t.Withdrawals == Convert.ToDecimal(expencedata1.PaidAmount)).FirstOrDefault();

                                    BankStatementRepository bankrepo = new BankStatementRepository();
                                    BankStatement bankstdata = new BankStatement();
                                    bankstdata.BankId = Convert.ToInt32(cmbbankname.SelectedValue);
                                    bankstdata.Bankname = Convert.ToString(cmbbankname.Text);
                                    bankstdata.TransactionDate = Convert.ToDateTime(dtpincomedate.Value);
                                    bankstdata.Particulars = "From" + " " + Convert.ToString(cmbincome.Text.Trim());
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    bankstdata.Deposits = Convert.ToDecimal(txtpaidamount.Text);
                                    bankstdata.TransactionType = "Income";
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    if (bdata != null)
                                    {
                                        bankstdata.Id = bdata.Id;
                                        bankrepo.Edit(bankstdata);
                                        bankrepo.Save();
                                    }
                                    else if (bdata == null)
                                    {
                                        bankrepo.Add(bankstdata);
                                        bankrepo.Save();
                                    }
                                    BankStRepo.Dispose();
                                    bankrepo.Dispose();
                                }
                                catch (Exception)
                                { }
                                expencedata.IncomeID = Convert.ToInt32(txtexpenceno.Text);
                                expencedata.Bankname = cmbbankname.Text;
                                expencedata.ChequeNo = txtchequeno.Text;
                                expencedata.ChequeDate = Convert.ToDateTime(dtpchequedate.Value);
                                expencedata.PaidAmount = Convert.ToDouble(txtpaidamount.Text);
                                expencedata.IncomeName = Convert.ToString(cmbincome.Text);
                                expencedata.Id = expenceid;
                                IncomeRepo.Edit(expencedata);
                                IncomeRepo.Save();
                                MessageBox.Show("Income Information Updated.", "Success");
                                clear();
                                this.ActiveControl = btnnew;
                            }
                            else
                            {
                                if (cmbbankname.Text == "Select")
                                {
                                    MessageBox.Show("Select Bank Name.", "Warning");
                                    this.ActiveControl = cmbbankname;
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
                            if (cmbbankname.Text.TrimStart() != "Select" && txtchequeno.Text.TrimStart() != "")
                            {
                                try
                                {
                                    BankStatementRepository BankStRepo = new BankStatementRepository();
                                    IncomeRepository expencerepo1 = new IncomeRepository();
                                    Income expencedata1 = expencerepo1.GetById(expenceid);
                                    var bdata = BankStRepo.GetAll().Where(t => t.Bankname == expencedata1.Bankname && t.CompId == CommonMethod.CompId && t.TransactionDate == expencedata1.IncomeDate && t.ChequeNo == expencedata1.ChequeNo && t.Withdrawals == Convert.ToDecimal(expencedata1.PaidAmount)).FirstOrDefault();

                                    BankStatementRepository bankrepo = new BankStatementRepository();
                                    BankStatement bankstdata = new BankStatement();
                                    bankstdata.BankId = Convert.ToInt32(cmbbankname.SelectedValue);
                                    bankstdata.Bankname = Convert.ToString(cmbbankname.Text);
                                    bankstdata.TransactionDate = Convert.ToDateTime(dtpincomedate.Value);
                                    bankstdata.Particulars = "From" + " " + Convert.ToString(cmbincome.Text.Trim());
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    bankstdata.Deposits = Convert.ToDecimal(txtpaidamount.Text);
                                    bankstdata.TransactionType = "Income";
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    if (bdata != null)
                                    {
                                        bankstdata.Id = bdata.Id;
                                        bankrepo.Edit(bankstdata);
                                        bankrepo.Save();
                                    }
                                    else if (bdata == null)
                                    {
                                        bankrepo.Add(bankstdata);
                                        bankrepo.Save();
                                    }
                                    BankStRepo.Dispose();
                                    bankrepo.Dispose();
                                }
                                catch (Exception)
                                { }
                                expencedata.IncomeID = Convert.ToInt32(txtexpenceno.Text);
                                expencedata.Bankname = cmbbankname.Text;
                                expencedata.ChequeNo = txtchequeno.Text;
                                expencedata.ChequeDate = Convert.ToDateTime(dtpchequedate.Value);
                                expencedata.PaidAmount = Convert.ToDouble(txtpaidamount.Text);
                                expencedata.IncomeName = Convert.ToString(cmbincome.Text);
                                expencedata.Id = expenceid;
                                IncomeRepo.Edit(expencedata);
                                IncomeRepo.Save();
                                MessageBox.Show("Income Information Updated.", "Success");
                                clear();
                                this.ActiveControl = btnnew;
                            }
                            else
                            {
                                if (cmbbankname.Text.TrimStart() == "Select")
                                {
                                    MessageBox.Show("Select Bank Name.", "Warning");
                                    this.ActiveControl = cmbbankname;
                                }
                                else
                                {
                                    MessageBox.Show("Enter UTR Number.", "Warning");
                                    this.ActiveControl = txtchequeno;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (cmbpaymentmode.Text == "Cash")
                        {
                            maxexpencid();
                            expencedata.IncomeID = Convert.ToInt32(txtexpenceno.Text);
                            expencedata.Bankname = "";
                            expencedata.ChequeNo = "";
                            expencedata.ChequeDate = null;
                            IncomeRepo.Add(expencedata);
                            IncomeRepo.Save();
                            MessageBox.Show("Income Information Saved.", "Success");
                            clear();
                            this.ActiveControl = btnnew;
                        }
                        else if (cmbpaymentmode.Text == "Cheque")
                        {
                            if (cmbbankname.Text != "Select" && txtchequeno.Text != "")
                            {
                                try
                                {
                                    BankStatementRepository bankrepo = new BankStatementRepository();
                                    BankStatement bankstdata = new BankStatement();
                                    bankstdata.BankId = Convert.ToInt32(cmbbankname.SelectedValue);
                                    bankstdata.Bankname = Convert.ToString(cmbbankname.Text);
                                    bankstdata.TransactionDate = Convert.ToDateTime(dtpincomedate.Value);
                                    bankstdata.Particulars = "From" + " " + Convert.ToString(cmbincome.Text.Trim());
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    bankstdata.Deposits = Convert.ToDecimal(txtpaidamount.Text);
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankstdata.TransactionType = "Income";
                                    bankrepo.Add(bankstdata);
                                    bankrepo.Save();
                                    bankrepo.Dispose();
                                }
                                catch (Exception)
                                { }
                                maxexpencid();
                                expencedata.IncomeID = Convert.ToInt32(txtexpenceno.Text);
                                expencedata.Bankname = cmbbankname.Text;
                                expencedata.ChequeNo = txtchequeno.Text;
                                expencedata.ChequeDate = Convert.ToDateTime(dtpchequedate.Value);
                                expencedata.PaidAmount = Convert.ToDouble(txtpaidamount.Text);
                                IncomeRepo.Add(expencedata);
                                IncomeRepo.Save();
                                MessageBox.Show("Income Information Saved.", "Success");
                                clear();
                                this.ActiveControl = btnnew;
                            }
                            else
                            {
                                if (cmbbankname.Text == "Select")
                                {
                                    MessageBox.Show("Select Bank Name.", "Warning");
                                    this.ActiveControl = cmbbankname;
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
                            if (cmbbankname.Text.TrimStart() != "Select" && txtchequeno.Text.TrimStart() != "")
                            {
                                try
                                {
                                    BankStatementRepository bankrepo = new BankStatementRepository();
                                    BankStatement bankstdata = new BankStatement();
                                    bankstdata.BankId = Convert.ToInt32(cmbbankname.SelectedValue);
                                    bankstdata.Bankname = Convert.ToString(cmbbankname.Text);
                                    bankstdata.TransactionDate = Convert.ToDateTime(dtpincomedate.Value);
                                    bankstdata.Particulars = "From" + " " + Convert.ToString(cmbincome.Text.Trim());
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    bankstdata.Deposits = Convert.ToDecimal(txtpaidamount.Text);
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankstdata.TransactionType = "Income";
                                    bankrepo.Add(bankstdata);
                                    bankrepo.Save();
                                    bankrepo.Dispose();
                                }
                                catch (Exception)
                                { }
                                maxexpencid();
                                expencedata.IncomeID = Convert.ToInt32(txtexpenceno.Text);
                                expencedata.Bankname = cmbbankname.Text;
                                expencedata.ChequeNo = txtchequeno.Text;
                                expencedata.ChequeDate = Convert.ToDateTime(dtpchequedate.Value);
                                expencedata.PaidAmount = Convert.ToDouble(txtpaidamount.Text);
                                IncomeRepo.Add(expencedata);
                                IncomeRepo.Save();
                                MessageBox.Show("Income Information Saved.", "Success");
                                clear();
                                this.ActiveControl = btnnew;
                            }
                            else
                            {
                                if (cmbbankname.Text.TrimStart() == "Select")
                                {
                                    MessageBox.Show("Select Bank Name.", "Warning");
                                    this.ActiveControl = cmbbankname;
                                }
                                else
                                {
                                    MessageBox.Show("Enter UTR Number.", "Warning");
                                    this.ActiveControl = txtchequeno;
                                }
                            }
                        }
                    }
                    IncomeRepo.Dispose();
                }
                else
                {
                    if (cmbincome.Text.Trim() == "")
                        MessageBox.Show("Enter Income Name.", "Warning");
                    else if (txtfrom.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter Income From.", "Warning");
                        this.ActiveControl = txtfrom;
                    }
                    else if (txtpaidamount.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter Amount.", "Warning");
                        this.ActiveControl = txtpaidamount;
                    }
                    else if (txtto.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter To Person name.", "Warning");
                        this.ActiveControl = txtto;
                    }
                    else if (cmbpaymentmode.Text == "")
                    {
                        MessageBox.Show("Select PaymentType.", "Warning");
                        this.ActiveControl = cmbpaymentmode;
                    }
                    else
                    {
                        MessageBox.Show("Select Account Group.", "Warning");
                        this.ActiveControl = txtpaidamount;
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
                Bindexpenceinfo();
                dtpfromdate.Value = DateTime.Now.Date;
                dtptodate.Value = DateTime.Now.Date;
                chkdate.Checked = false;
                chkIncome.Checked = false;
                cmbsearchincome.Enabled = false;
                clear();
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
                    if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                    {
                        IncomeRepository IncomeRepo = new IncomeRepository();
                        GvexpenceInfo.Rows.Clear();
                        int i = 0;
                        List<Income> ItemList = IncomeRepo.GetAll().Where(t => t.IncomeDate.Value.Date >= dtpfromdate.Value.Date && t.IncomeDate.Value.Date <= dtptodate.Value.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        label13.Text = Convert.ToString(Convert.ToDecimal(ItemList.Sum(t => t.PaidAmount)));
                        if (ItemList != null)
                        {
                            foreach (var item in ItemList)
                            {
                                GvexpenceInfo.Rows.Add();
                                GvexpenceInfo.Rows[i].Cells["Date"].Value = item.IncomeDate.Value.ToShortDateString();
                                GvexpenceInfo.Rows[i].Cells["IncomeId"].Value = item.IncomeID;

                                GvexpenceInfo.Rows[i].Cells["ACGroup"].Value = item.AccountingGroupMaster.ACGName;
                                GvexpenceInfo.Rows[i].Cells["IncomeNameg"].Value = item.IncomeMaster.IncomeName;
                                GvexpenceInfo.Rows[i].Cells["From"].Value = item.FromPerson;
                                GvexpenceInfo.Rows[i].Cells["To"].Value = item.ToPerson;
                                GvexpenceInfo.Rows[i].Cells["Reason"].Value = item.Reason;
                                GvexpenceInfo.Rows[i].Cells["Mode"].Value = item.PaymentMode;
                                GvexpenceInfo.Rows[i].Cells["bankname"].Value = item.Bankname;
                                GvexpenceInfo.Rows[i].Cells["ChqUTRNo"].Value = item.ChequeNo;
                                GvexpenceInfo.Rows[i].Cells["ChqNEFTDate"].Value = item.ChequeDate;
                                GvexpenceInfo.Rows[i].Cells["PaidAmount"].Value = item.PaidAmount;
                                GvexpenceInfo.Rows[i].Cells["Id"].Value = item.Id;
                                GvexpenceInfo.Rows[i].Cells["ACGId"].Value = item.ACGId;
                                i++;
                            }
                        }
                        this.ActiveControl = dtptodate;
                        IncomeRepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("To Date Should greater than from date.", "Warning");
                        this.ActiveControl = dtptodate;
                    }
                }
                if (chkIncome.Checked == true)
                {
                    if (cmbsearchincome.Text != "Select")
                    {
                        IncomeRepository irepo = new IncomeRepository();
                        GvexpenceInfo.Rows.Clear();
                        int i = 0;
                        List<Income> ItemList = irepo.GetAll().Where(t => t.IncomeMaster.IncomeName == cmbsearchincome.Text && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        label13.Text = Convert.ToString(Convert.ToDecimal(ItemList.Sum(t => t.PaidAmount)));
                        if (ItemList != null)
                        {
                            foreach (var item in ItemList)
                            {
                                GvexpenceInfo.Rows.Add();
                                GvexpenceInfo.Rows[i].Cells["Date"].Value = item.IncomeDate.Value.ToShortDateString();
                                GvexpenceInfo.Rows[i].Cells["IncomeId"].Value = item.IncomeID;
                                GvexpenceInfo.Rows[i].Cells["ACGroup"].Value = item.AccountingGroupMaster.ACGName;
                                GvexpenceInfo.Rows[i].Cells["IncomeNameg"].Value = item.IncomeMaster.IncomeName;
                                GvexpenceInfo.Rows[i].Cells["From"].Value = item.FromPerson;
                                GvexpenceInfo.Rows[i].Cells["To"].Value = item.ToPerson;
                                GvexpenceInfo.Rows[i].Cells["Reason"].Value = item.Reason;
                                GvexpenceInfo.Rows[i].Cells["Mode"].Value = item.PaymentMode;
                                GvexpenceInfo.Rows[i].Cells["bankname"].Value = item.Bankname;
                                GvexpenceInfo.Rows[i].Cells["ChqUTRNo"].Value = item.ChequeNo;
                                GvexpenceInfo.Rows[i].Cells["ChqNEFTDate"].Value = item.ChequeDate;
                                GvexpenceInfo.Rows[i].Cells["PaidAmount"].Value = item.PaidAmount;
                                GvexpenceInfo.Rows[i].Cells["Id"].Value = item.Id;
                                GvexpenceInfo.Rows[i].Cells["ACGId"].Value = item.ACGId;
                                i++;
                            }
                        }
                        this.ActiveControl = cmbsearchincome;
                        irepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Select Income Name.", "Warning");
                        this.ActiveControl = cmbsearchincome;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                expenceid = Convert.ToInt32(txtexpenceno.Text);
                if (expenceid <= Convert.ToInt32(maxid))
                {
                    RptOtherIncome expenceidobj = new RptOtherIncome(Convert.ToInt32(txtexpenceno.Text));
                    expenceidobj.ShowDialog();
                    expenceidobj.Dispose();
                }
                else
                    MessageBox.Show("Select Income From List.", "Warning");
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

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                IncomeRepository IncomeRepo = new IncomeRepository();
                Income expencemast = IncomeRepo.GetById(expenceid);
                IncomeRepo.Remove(expencemast);
                IncomeRepo.Save();
                MessageBox.Show("Record deleted Sucessfully.", "Success");
                clear();
                btndelete.Enabled = false;
                this.ActiveControl = btnnew;
                IncomeRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnprintall_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkIncome.Checked == true && cmbsearchincome.Text != "Select")
                {
                    RptOtherIncome expenceidobj = new RptOtherIncome(cmbsearchincome.Text);
                    expenceidobj.ShowDialog();
                    expenceidobj.Dispose();
                }
                else if (chkdate.Checked == true)
                {
                    if (dtpfromdate.Value.Date < dtptodate.Value.Date)
                    {
                        RptOtherIncome expenceidobj = new RptOtherIncome(dtpfromdate.Value.ToString("MM/dd/yyyy"), dtptodate.Value.ToString("MM/dd/yyyy"));
                        expenceidobj.ShowDialog();
                        expenceidobj.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("To Date Should greater than from date.", "Warning");
                        this.ActiveControl = dtptodate;
                    }
                }
                else if (Incomename != "" && ACGName1 != "")
                {
                    RptOtherIncome expenceidobj = new RptOtherIncome(Incomename, fdate, tdate, ACGName1);
                    expenceidobj.ShowDialog();
                    expenceidobj.Dispose();
                }
                else
                {
                    RptOtherIncome expenceidobj = new RptOtherIncome();
                    expenceidobj.ShowDialog();
                    expenceidobj.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void frmOtherIncome_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        public void fillexp()
        {
            try
            {
                IncomeMasterRepository expmastRepo = new IncomeMasterRepository();
                List<IncomeMaster> expdata = new List<IncomeMaster>();
                expdata.Clear();
                expdata = expmastRepo.GetAll().OrderByDescending(t => t.IncomeName).ToList();
                expdata.Add(new IncomeMaster { IncomeName = "Select", IncomeId = 0 });
                expdata.Reverse();
                cmbincome.DataSource = expdata;
                cmbincome.ValueMember = "ID";
                cmbincome.DisplayMember = "IncomeName";
                expmastRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillincomesearch()
        {
            try
            {
                IncomeMasterRepository incomrepo = new IncomeMasterRepository();
                List<IncomeMaster> expdata = new List<IncomeMaster>();
                expdata.Clear();
                expdata = incomrepo.GetAll().OrderByDescending(t => t.IncomeName).ToList();
                expdata.Add(new IncomeMaster { IncomeName = "Select", ID = 0 });
                expdata.Reverse();
                cmbsearchincome.DataSource = expdata;
                cmbsearchincome.ValueMember = "ID";
                cmbsearchincome.DisplayMember = "IncomeName";
                incomrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void cmbsearchincome_KeyDown(object sender, KeyEventArgs e)
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
                    chkIncome.Checked = false;
                    cmbsearchincome.Text = "Select";
                    cmbsearchincome.Enabled = false;
                }
            }
            catch (Exception)
            { }
        }

        private void chkIncome_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkIncome.Checked == true)
                {
                    cmbsearchincome.Enabled = true;
                    chkdate.Checked = false;
                    dtpfromdate.Value = DateTime.Now.Date;
                    dtptodate.Value = DateTime.Now.Date;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbACGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbACGroups.Text != "Select")
                {
                    try
                    {
                        IncomeMasterRepository expmastRepo = new IncomeMasterRepository();
                        List<IncomeMaster> expdata = new List<IncomeMaster>();
                        expdata.Clear();
                        expdata = expmastRepo.GetAll().Where(t => t.ACGId == Convert.ToInt32(cmbACGroups.SelectedValue)).OrderByDescending(t => t.IncomeName).ToList();
                        expdata.Reverse();
                        cmbincome.DataSource = expdata;
                        cmbincome.ValueMember = "IncomeId";
                        cmbincome.DisplayMember = "IncomeName";
                        expmastRepo.Dispose();
                    }
                    catch (Exception)
                    { }
                }
            }
            catch (Exception)
            { }
        }

        private void dtpexpencedate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbACGroups;
            }
            catch (Exception)
            { }
        }

        private void cmbACGroups_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbincome;
            }
            catch (Exception)
            { }
        }

        private void cmbincome_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtfrom;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmIncomeMaster expmast = new frmIncomeMaster();
                        expmast.ShowDialog();
                        fillexp();
                        this.ActiveControl = cmbincome;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtfrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtpaidamount;
            }
            catch (Exception)
            { }
        }

        private void txtpaidamount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtreason;
            }
            catch (Exception)
            { }
        }

        private void txtreason_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsave;
            }
            catch (Exception)
            { }
        }

        public void TPLACtoOtherIncome(string incomename, DateTime from, DateTime to, String ACCGName)
        {
            try
            {
                IncomeRepository IncomeRepo = new IncomeRepository();
                GvexpenceInfo.Rows.Clear();
                int i = 0;
                List<Income> ItemList = IncomeRepo.GetAll().Where(t => t.IncomeMaster.IncomeName == incomename && t.IncomeDate.Value.Date >= from.Date && t.IncomeDate.Value.Date <= to.Date && t.AccountingGroupMaster.ACGName == ACCGName && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                label13.Text = Convert.ToString(Convert.ToDecimal(ItemList.Sum(t => t.PaidAmount)));
                if (ItemList != null)
                {
                    foreach (var item in ItemList)
                    {
                        GvexpenceInfo.Rows.Add();
                        GvexpenceInfo.Rows[i].Cells["Date"].Value = item.IncomeDate.Value.ToShortDateString();
                        GvexpenceInfo.Rows[i].Cells["IncomeId"].Value = item.IncomeID;
                        if (item.ACGId == null)
                            GvexpenceInfo.Rows[i].Cells["ACGroup"].Value = "";
                        else
                            GvexpenceInfo.Rows[i].Cells["ACGroup"].Value = item.AccountingGroupMaster.ACGName;
                        GvexpenceInfo.Rows[i].Cells["IncomeNameg"].Value = item.IncomeMaster.IncomeName;
                        GvexpenceInfo.Rows[i].Cells["From"].Value = item.FromPerson;
                        GvexpenceInfo.Rows[i].Cells["To"].Value = item.ToPerson;
                        GvexpenceInfo.Rows[i].Cells["Reason"].Value = item.Reason;
                        GvexpenceInfo.Rows[i].Cells["Mode"].Value = item.PaymentMode;
                        GvexpenceInfo.Rows[i].Cells["bankname"].Value = item.Bankname;
                        GvexpenceInfo.Rows[i].Cells["ChqUTRNo"].Value = item.ChequeNo;
                        GvexpenceInfo.Rows[i].Cells["ChqNEFTDate"].Value = item.ChequeDate;
                        GvexpenceInfo.Rows[i].Cells["PaidAmount"].Value = item.PaidAmount;
                        GvexpenceInfo.Rows[i].Cells["Id"].Value = item.Id;
                        GvexpenceInfo.Rows[i].Cells["ACGId"].Value = item.ACGId;
                        i++;
                    }
                }
                IncomeRepo.Dispose();
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
                    cmbbankname.Enabled = true;
                    txtchequeno.Enabled = true;
                    dtpchequedate.Enabled = true;
                    label10.Text = "Cheque No:";
                    label8.Text = "Cheque Date:";
                }
                else if (cmbpaymentmode.Text == "NEFT" || cmbpaymentmode.Text == "RTGS")
                {
                    cmbbankname.Enabled = true;
                    txtchequeno.Enabled = true;
                    dtpchequedate.Enabled = true;
                    label10.Text = "UTR No:";
                    if (cmbpaymentmode.Text == "NEFT")
                        label8.Text = "NEFT Date :";
                    else
                        label8.Text = "RTGS Date :";
                }
                else
                {
                    cmbbankname.Enabled = false;
                    txtchequeno.Enabled = false;
                    dtpchequedate.Enabled = false;
                }
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
                        this.ActiveControl = txtpaidamount;
                    else
                        this.ActiveControl = cmbbankname;
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
                    this.ActiveControl = dtpchequedate;
            }
            catch (Exception)
            { }
        }

        private void dtpchequedate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtchequeno;
            }
            catch (Exception)
            { }
        }

        private void txtchequeno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtpaidamount;
            }
            catch (Exception)
            { }
        }

        private void txtpaidamount_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsave;
            }
            catch (Exception)
            { }
        }

        private void txtpaidamount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int i = e.KeyChar;
                if (i >= 46 && i <= 57 || i == 8)
                { }
                else
                    e.Handled = true;
            }
            catch (Exception)
            { }
        }

        private void btnAddIncomeM_Click(object sender, EventArgs e)
        {
            try
            {
                frmIncomeMaster expmast = new frmIncomeMaster();
                expmast.ShowDialog();
                fillexp();
                this.ActiveControl = cmbincome;
            }
            catch (Exception)
            { }
        }

        private void GvexpenceInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["To"].Value) != "Journal Entry")
                {
                    dtpincomedate.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["Date"].Value);
                    txtexpenceno.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["IncomeId"].Value);
                    cmbACGroups.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["ACGroup"].Value);
                    cmbincome.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["IncomeNameg"].Value);
                    txtfrom.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["From"].Value);
                    txtto.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["To"].Value);
                    txtreason.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["Reason"].Value);
                    cmbpaymentmode.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["Mode"].Value);
                    cmbbankname.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["bankname"].Value);
                    btnsave.Text = "Update";
                    try
                    {
                        if (Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["Mode"].Value) != "Cash")
                        {
                            txtchequeno.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["ChqUTRNo"].Value);
                            dtpchequedate.Value = Convert.ToDateTime(GvexpenceInfo.Rows[e.RowIndex].Cells["ChqNEFTDate"].Value);
                        }
                        else
                        {
                            dtpchequedate.Enabled = false;
                            txtchequeno.Text = "";
                        }
                    }
                    catch (Exception)
                    { }
                    txtpaidamount.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["PaidAmount"].Value);
                    expenceid = Convert.ToInt32(GvexpenceInfo.Rows[e.RowIndex].Cells["Id"].Value);
                    btndelete.Enabled = true;
                    if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
                    {
                        btnsave.Enabled = false;
                        btndelete.Enabled = false;
                        btnnew.Enabled = false;
                    }
                }
                else
                    MessageBox.Show("Update This Record In Journal Entry Form.", "Warning");
            }
            catch (Exception)
            { }
        }
    }
}