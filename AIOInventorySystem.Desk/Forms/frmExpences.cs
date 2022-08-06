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
    public partial class frmExpences : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        string maxid, expencename1 = "", ExpenceName1 = "", ACCName1 = "";
        public int expenceid;
        DateTime fdate1, fromdate1, tdate1, todate1;

        public frmExpences()
        {
            InitializeComponent();
            maxexpencid();
            BindAcGroup();
            fillcombo();
            fillexp();
            fillexp1();
            Bindexpenceinfo();
            cmbbankname.Enabled = false;
            txtchequeno.Enabled = false;
            dtpchequedate.Enabled = false;
            btndelete.Enabled = false;
            cm.changedatetimepickerrange(this);
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
            {
                btnsave.Enabled = false;
                btndelete.Enabled = false;
                btnnew.Enabled = false;
            }
        }

        public frmExpences(string ExpenceName, DateTime from, DateTime to)
        {
            InitializeComponent();
            maxexpencid();
            BindAcGroup();
            fillcombo();
            fillexp();
            fillexp1();
            fdate1 = from;
            tdate1 = to;
            expencename1 = ExpenceName;
            Bindexpenceinfo(ExpenceName, from, to);
            cmbbankname.Enabled = false;
            txtchequeno.Enabled = false;
            dtpchequedate.Enabled = false;
            btndelete.Enabled = false;
            cm.changedatetimepickerrange(this);
            this.ActiveControl = cmbexpence;
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
            {
                btnsave.Enabled = false;
                btndelete.Enabled = false;
                btnnew.Enabled = false;
            }
        }

        public frmExpences(DateTime fdate, DateTime tdate, string ExpenceName, string ACCName)
        {
            InitializeComponent();
            maxexpencid();
            BindAcGroup();
            fillcombo();
            fillexp();
            fillexp1();
            fromdate1 = fdate;
            todate1 = tdate;
            ExpenceName1 = ExpenceName;
            ACCName1 = ACCName;
            BalShtToExpence(fdate, tdate, ExpenceName, ACCName);
            cmbbankname.Enabled = false;
            txtchequeno.Enabled = false;
            dtpchequedate.Enabled = false;
            btndelete.Enabled = false;
            cm.changedatetimepickerrange(this);
            this.ActiveControl = cmbexpence;
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
                ACGdata = ACGRepo.GetAll().OrderByDescending(t => t.ACGName).OrderByDescending(t => t.ACGName).ToList();
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

        public void Bindexpenceinfo(string ExpenceName, DateTime from, DateTime to)
        {
            try
            {
                ExpenceRepository expencerepo = new ExpenceRepository();
                GvexpenceInfo.Rows.Clear();
                int i = 0;
                List<Expence> ItemList = expencerepo.GetAll().Where(t => t.ExpenceName == ExpenceName && t.ExpenceDate.Value.Date >= from.Date && t.ExpenceDate.Value.Date <= to.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                decimal totalexpence = Convert.ToDecimal(expencerepo.GetAll().Where(t => t.ExpenceName == ExpenceName && t.ExpenceDate.Value.Date >= from.Date && t.ExpenceDate.Value.Date <= to.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmount));
                label13.Text = Convert.ToString(totalexpence);
                if (ItemList != null)
                {
                    foreach (var item in ItemList)
                    {
                        GvexpenceInfo.Rows.Add();
                        GvexpenceInfo.Rows[i].Cells["Date"].Value = item.ExpenceDate.Value.ToShortDateString();
                        GvexpenceInfo.Rows[i].Cells["ExpId"].Value = item.ExpenceID;
                        if (item.ACGId == null)
                            GvexpenceInfo.Rows[i].Cells["ACGroup"].Value = "";
                        else
                            GvexpenceInfo.Rows[i].Cells["ACGroup"].Value = item.AccountingGroupMaster.ACGName;
                        GvexpenceInfo.Rows[i].Cells["ExpenceName"].Value = item.ExpenceName;
                        GvexpenceInfo.Rows[i].Cells["From"].Value = item.FromPerson;
                        GvexpenceInfo.Rows[i].Cells["To"].Value = item.ToPerson;
                        GvexpenceInfo.Rows[i].Cells["Reason"].Value = item.Reason;
                        GvexpenceInfo.Rows[i].Cells["Mode"].Value = item.PaymentMode;
                        GvexpenceInfo.Rows[i].Cells["BankName"].Value = item.Bankname;
                        GvexpenceInfo.Rows[i].Cells["ChqUTRNo"].Value = item.ChequeNo;
                        GvexpenceInfo.Rows[i].Cells["ChqNEFTDate"].Value = item.ChequeDate;
                        GvexpenceInfo.Rows[i].Cells["PaidAmount"].Value = item.PaidAmount;
                        GvexpenceInfo.Rows[i].Cells["Id"].Value = item.Id;
                        GvexpenceInfo.Rows[i].Cells["ACGId"].Value = item.ACGId;
                        i++;
                    }
                }
                this.ActiveControl = dtptodate;
                expencerepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void BalShtToExpence(DateTime fdate, DateTime tdate, string ExpenceName, string ACCName)
        {
            try
            {
                ExpenceRepository expencerepo = new ExpenceRepository();
                GvexpenceInfo.Rows.Clear();
                int i = 0;
                List<Expence> ItemList = expencerepo.GetAll().Where(t => t.ExpenceName == ExpenceName && t.ExpenceDate.Value.Date >= fdate.Date && t.ExpenceDate.Value.Date <= tdate.Date && (t.AccountingGroupMaster.ACGName == ACCName) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                decimal totalexpence = Convert.ToDecimal(expencerepo.GetAll().Where(t => t.ExpenceName == ExpenceName && t.ExpenceDate.Value.Date >= fdate.Date && t.ExpenceDate.Value.Date <= tdate.Date && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmount));
                label13.Text = Convert.ToString(totalexpence);
                if (ItemList != null)
                {
                    foreach (var item in ItemList)
                    {
                        GvexpenceInfo.Rows.Add();
                        GvexpenceInfo.Rows[i].Cells["Date"].Value = item.ExpenceDate.Value.ToShortDateString();
                        GvexpenceInfo.Rows[i].Cells["ExpId"].Value = item.ExpenceID;
                        if (item.ACGId == null)
                            GvexpenceInfo.Rows[i].Cells["ACGroup"].Value = "";
                        else
                            GvexpenceInfo.Rows[i].Cells["ACGroup"].Value = item.AccountingGroupMaster.ACGName;
                        GvexpenceInfo.Rows[i].Cells["ExpenceName"].Value = item.ExpenceName;
                        GvexpenceInfo.Rows[i].Cells["From"].Value = item.FromPerson;
                        GvexpenceInfo.Rows[i].Cells["To"].Value = item.ToPerson;
                        GvexpenceInfo.Rows[i].Cells["Reason"].Value = item.Reason;
                        GvexpenceInfo.Rows[i].Cells["Mode"].Value = item.PaymentMode;
                        GvexpenceInfo.Rows[i].Cells["BankName"].Value = item.Bankname;
                        GvexpenceInfo.Rows[i].Cells["ChqUTRNo"].Value = item.ChequeNo;
                        GvexpenceInfo.Rows[i].Cells["ChqNEFTDate"].Value = item.ChequeDate;
                        GvexpenceInfo.Rows[i].Cells["PaidAmount"].Value = item.PaidAmount;
                        GvexpenceInfo.Rows[i].Cells["Id"].Value = item.Id;
                        GvexpenceInfo.Rows[i].Cells["ACGId"].Value = item.ACGId;
                        i++;
                    }
                }
                this.ActiveControl = dtptodate;
                expencerepo.Dispose();
            }
            catch (Exception) { }
        }

        public void maxexpencid()
        {
            try
            {
                DataTable dt = db.GetTable("Select max(ExpenceID) from Expences WHERE TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                if (dt.Rows.Count > 0)
                {
                    maxid = dt.Rows[0][0].ToString();
                    if (maxid == "")
                        txtexpenceno.Text = "1";
                    else
                        txtexpenceno.Text = Convert.ToString(Convert.ToInt32(Convert.ToInt32(maxid) + 1));
                }
                dt.Dispose();
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
            btnsave.Text = "Save";
            dtpchequedate.Enabled = false; btndelete.Enabled = false;
            fillcombo();
            fillexp();
            fillexp1();
            cmbpaymentmode.SelectedIndex = -1;
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
            {
                btnsave.Enabled = false;
                btndelete.Enabled = false;
                btnnew.Enabled = false;
            }
            try
            {
                dtpexpencedate.Value = DateTime.Now.Date;
                dtpchequedate.Value = DateTime.Now.Date;
            }
            catch (Exception)
            { }
            Bindexpenceinfo();
            this.ActiveControl = btnnew;
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

        public void fillexp()
        {
            try
            {
                ExpenceMasterRepository expmastRepo = new ExpenceMasterRepository();
                List<ExpenceMaster> expdata = new List<ExpenceMaster>();
                expdata.Clear();
                expdata = expmastRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.ExpenceName).ToList();
                expdata.Add(new ExpenceMaster { ExpenceName = "Select", ExpenceId = 0 });
                expdata.Reverse();
                cmbexpence.DataSource = expdata;
                cmbexpence.ValueMember = "ExpenceId";
                cmbexpence.DisplayMember = "ExpenceName";
                expmastRepo.Dispose();
                cmbexpence.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void fillexp1()
        {
            try
            {
                ExpenceMasterRepository expmastRepo = new ExpenceMasterRepository();
                List<ExpenceMaster> expdata = new List<ExpenceMaster>();
                expdata.Clear();
                expdata = expmastRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.ExpenceName).ToList();
                expdata.Add(new ExpenceMaster { ExpenceName = "Select", ExpenceId = 0 });
                expdata.Reverse();
                cmbexpencename1.DataSource = expdata;
                cmbexpencename1.ValueMember = "ExpenceId";
                cmbexpencename1.DisplayMember = "ExpenceName";
                expmastRepo.Dispose();
                cmbexpencename1.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void Bindexpenceinfo()
        {
            try
            {
                ExpenceRepository expencerepo = new ExpenceRepository();
                GvexpenceInfo.Rows.Clear();
                List<Expence> ItemList = expencerepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                label13.Text = Convert.ToString(Convert.ToDecimal(ItemList.Sum(t => t.PaidAmount)));
                if (ItemList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        GvexpenceInfo.Rows.Add();
                        GvexpenceInfo.Rows[i].Cells["Date"].Value = item.ExpenceDate.Value.ToShortDateString();
                        GvexpenceInfo.Rows[i].Cells["ExpId"].Value = item.ExpenceID;
                        try
                        {
                            GvexpenceInfo.Rows[i].Cells["ACGroup"].Value = item.AccountingGroupMaster.ACGName;
                        }
                        catch (Exception)
                        { }
                        GvexpenceInfo.Rows[i].Cells["ExpenceName"].Value = item.ExpenceName;
                        GvexpenceInfo.Rows[i].Cells["From"].Value = item.FromPerson;
                        GvexpenceInfo.Rows[i].Cells["To"].Value = item.ToPerson;
                        GvexpenceInfo.Rows[i].Cells["Reason"].Value = item.Reason;
                        GvexpenceInfo.Rows[i].Cells["Mode"].Value = item.PaymentMode;
                        GvexpenceInfo.Rows[i].Cells["BankName"].Value = item.Bankname;
                        GvexpenceInfo.Rows[i].Cells["ChqUTRNo"].Value = item.ChequeNo;
                        GvexpenceInfo.Rows[i].Cells["ChqNEFTDate"].Value = item.ChequeDate;
                        GvexpenceInfo.Rows[i].Cells["PaidAmount"].Value = item.PaidAmount;
                        GvexpenceInfo.Rows[i].Cells["Id"].Value = item.Id;
                        GvexpenceInfo.Rows[i].Cells["ACGId"].Value = item.ACGId;
                        i++;
                    }
                }
                expencerepo.Dispose();
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
                if (cmbexpence.Text != "Select" && cmbexpence.Text != "" && cmbACGroups.Text != "Select" && cmbACGroups.Text != "")
                {
                    if (txtto.Text.Trim() != "")
                    {
                        if (cmbpaymentmode.Text != "")
                        {
                            if (txtpaidamount.Text != "")
                            {
                                ExpenceRepository expencerepo = new ExpenceRepository();
                                Expence expencedata = new Expence();
                                expencedata.ExpenceDate = Convert.ToDateTime(dtpexpencedate.Value);
                                expencedata.ExpenceName = cmbexpence.Text;
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
                                        expencedata.ExpenceID = Convert.ToInt32(txtexpenceno.Text);
                                        expencedata.Bankname = "";
                                        expencedata.ChequeNo = "";
                                        expencedata.ChequeDate = null;
                                        expencedata.PaidAmount = Convert.ToDouble(txtpaidamount.Text);
                                        expencedata.Id = expenceid;
                                        expencerepo.Edit(expencedata);
                                        expencerepo.Save();
                                        MessageBox.Show("Expence Information Updated.", "success");
                                        clear();
                                    }
                                    else if (cmbpaymentmode.Text == "Cheque")
                                    {
                                        if (cmbbankname.Text != "Select" && txtchequeno.Text != "")
                                        {
                                            try
                                            {
                                                BankStatementRepository BankStRepo = new BankStatementRepository();
                                                ExpenceRepository expencerepo1 = new ExpenceRepository();
                                                Expence expencedata1 = expencerepo1.GetById(expenceid);
                                                var bdata = BankStRepo.GetAll().Where(t => t.Bankname == expencedata1.Bankname && t.CompId == CommonMethod.CompId && t.TransactionDate == expencedata1.ExpenceDate && t.ChequeNo == expencedata1.ChequeNo && t.Withdrawals == Convert.ToDecimal(expencedata1.PaidAmount)).FirstOrDefault();
                                                BankStatementRepository bankrepo = new BankStatementRepository();
                                                BankStatement bankstdata = new BankStatement();
                                                bankstdata.BankId = Convert.ToInt32(cmbbankname.SelectedValue);
                                                bankstdata.Bankname = Convert.ToString(cmbbankname.Text);
                                                bankstdata.TransactionDate = Convert.ToDateTime(dtpexpencedate.Value);
                                                bankstdata.Particulars = "Paid to" + " " + Convert.ToString(cmbexpence.Text.Trim());
                                                bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                                bankstdata.Withdrawals = Convert.ToDecimal(txtpaidamount.Text);
                                                bankstdata.TransactionType = "Expence";
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
                                            expencedata.ExpenceID = Convert.ToInt32(txtexpenceno.Text);
                                            expencedata.Bankname = cmbbankname.Text;
                                            expencedata.ChequeNo = txtchequeno.Text;
                                            expencedata.ChequeDate = Convert.ToDateTime(dtpchequedate.Value);
                                            expencedata.PaidAmount = Convert.ToDouble(txtpaidamount.Text);
                                            expencedata.Id = expenceid;
                                            expencerepo.Edit(expencedata);
                                            expencerepo.Save();
                                            MessageBox.Show("Expence Information Updated.", "success");
                                            clear();
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
                                                ExpenceRepository expencerepo1 = new ExpenceRepository();
                                                Expence expencedata1 = expencerepo1.GetById(expenceid);
                                                var bdata = BankStRepo.GetAll().Where(t => t.Bankname == expencedata1.Bankname && t.CompId == CommonMethod.CompId && t.TransactionDate == expencedata1.ExpenceDate && t.ChequeNo == expencedata1.ChequeNo && t.Withdrawals == Convert.ToDecimal(expencedata1.PaidAmount)).FirstOrDefault();
                                                BankStatementRepository bankrepo = new BankStatementRepository();
                                                BankStatement bankstdata = new BankStatement();
                                                bankstdata.BankId = Convert.ToInt32(cmbbankname.SelectedValue);
                                                bankstdata.Bankname = Convert.ToString(cmbbankname.Text);
                                                bankstdata.TransactionDate = Convert.ToDateTime(dtpexpencedate.Value);
                                                bankstdata.Particulars = "Paid to" + " " + Convert.ToString(cmbexpence.Text.Trim());
                                                bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                                bankstdata.Withdrawals = Convert.ToDecimal(txtpaidamount.Text);
                                                bankstdata.TransactionType = "Expence";
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
                                            expencedata.ExpenceID = Convert.ToInt32(txtexpenceno.Text);
                                            expencedata.Bankname = cmbbankname.Text;
                                            expencedata.ChequeNo = txtchequeno.Text;
                                            expencedata.ChequeDate = Convert.ToDateTime(dtpchequedate.Value);
                                            expencedata.PaidAmount = Convert.ToDouble(txtpaidamount.Text);
                                            expencedata.Id = expenceid;
                                            expencerepo.Edit(expencedata);
                                            expencerepo.Save();
                                            MessageBox.Show("Expence Information Updated.", "Warning");
                                            clear();
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
                                        expencedata.ExpenceID = Convert.ToInt32(txtexpenceno.Text);
                                        expencedata.Bankname = "";
                                        expencedata.ChequeNo = "";
                                        expencedata.ChequeDate = null;
                                        expencerepo.Add(expencedata);
                                        expencerepo.Save();
                                        MessageBox.Show("Expence Information Saved.", "Success");
                                        clear();
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
                                                bankstdata.TransactionDate = Convert.ToDateTime(dtpexpencedate.Value);
                                                bankstdata.Particulars = "Paid to" + " " + Convert.ToString(cmbexpence.Text.Trim());
                                                bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                                bankstdata.Withdrawals = Convert.ToDecimal(txtpaidamount.Text);
                                                bankstdata.Balance = 0;
                                                bankstdata.CompId = CommonMethod.CompId;
                                                bankstdata.TransactionType = "Expence";
                                                bankrepo.Add(bankstdata);
                                                bankrepo.Save();
                                                bankrepo.Dispose();
                                            }
                                            catch (Exception)
                                            { }
                                            maxexpencid();
                                            expencedata.ExpenceID = Convert.ToInt32(txtexpenceno.Text);
                                            expencedata.Bankname = cmbbankname.Text;
                                            expencedata.ChequeNo = txtchequeno.Text;
                                            expencedata.ChequeDate = Convert.ToDateTime(dtpchequedate.Value);
                                            expencedata.PaidAmount = Convert.ToDouble(txtpaidamount.Text);
                                            expencerepo.Add(expencedata);
                                            expencerepo.Save();
                                            MessageBox.Show("Expence Information Saved.", "success");
                                            clear();
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
                                                bankstdata.TransactionDate = Convert.ToDateTime(dtpexpencedate.Value);
                                                bankstdata.Particulars = "Paid to" + " " + Convert.ToString(cmbexpence.Text.Trim());
                                                bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                                bankstdata.Withdrawals = Convert.ToDecimal(txtpaidamount.Text);
                                                bankstdata.Balance = 0;
                                                bankstdata.CompId = CommonMethod.CompId;
                                                bankstdata.TransactionType = "Expence";
                                                bankrepo.Add(bankstdata);
                                                bankrepo.Save();
                                                bankrepo.Dispose();
                                            }
                                            catch (Exception)
                                            { }
                                            maxexpencid();
                                            expencedata.ExpenceID = Convert.ToInt32(txtexpenceno.Text);
                                            expencedata.Bankname = cmbbankname.Text;
                                            expencedata.ChequeNo = txtchequeno.Text;
                                            expencedata.ChequeDate = Convert.ToDateTime(dtpchequedate.Value);
                                            expencedata.PaidAmount = Convert.ToDouble(txtpaidamount.Text);
                                            expencerepo.Add(expencedata);
                                            expencerepo.Save();
                                            MessageBox.Show("Expence Information Saved.", "success");
                                            clear();
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
                            MessageBox.Show("Select PaymentType.", "Warning");
                            this.ActiveControl = cmbpaymentmode;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Enter To Person name.", "Warning");
                        this.ActiveControl = txtto;
                    }
                }
                else
                {
                    if (cmbexpence.Text == "Select" || cmbexpence.Text == "")
                    {
                        MessageBox.Show("Select Expence name.", "Warning");
                        this.ActiveControl = cmbexpence;
                    }
                    else
                    {
                        MessageBox.Show("Select Group.", "Warning");
                        this.ActiveControl = cmbACGroups;
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

        private void btngetall_Click(object sender, EventArgs e)
        {
            try
            {
                Bindexpenceinfo();
                dtpfromdate.Value = DateTime.Now.Date;
                dtptodate.Value = DateTime.Now.Date;
                chkdate.Checked = false;
                chkexpname.Checked = false;
                clear();
                expencename1 = "";
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
                        ExpenceRepository expencerepo = new ExpenceRepository();
                        GvexpenceInfo.Rows.Clear();
                        int i = 0;
                        List<Expence> ItemList = expencerepo.GetAll().Where(t => t.ExpenceDate >= dtpfromdate.Value && t.ExpenceDate <= dtptodate.Value && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        label13.Text = Convert.ToString(Convert.ToDecimal(ItemList.Sum(t => t.PaidAmount)));
                        if (ItemList != null)
                        {
                            foreach (var item in ItemList)
                            {
                                GvexpenceInfo.Rows.Add();
                                GvexpenceInfo.Rows[i].Cells["Date"].Value = item.ExpenceDate.Value.ToShortDateString();
                                GvexpenceInfo.Rows[i].Cells["ExpId"].Value = item.ExpenceID;
                                GvexpenceInfo.Rows[i].Cells["ACGroup"].Value = item.AccountingGroupMaster.ACGName;
                                GvexpenceInfo.Rows[i].Cells["ExpenceName"].Value = item.ExpenceName;
                                GvexpenceInfo.Rows[i].Cells["From"].Value = item.FromPerson;
                                GvexpenceInfo.Rows[i].Cells["To"].Value = item.ToPerson;
                                GvexpenceInfo.Rows[i].Cells["Reason"].Value = item.Reason;
                                GvexpenceInfo.Rows[i].Cells["Mode"].Value = item.PaymentMode;
                                GvexpenceInfo.Rows[i].Cells["BankName"].Value = item.Bankname;
                                GvexpenceInfo.Rows[i].Cells["ChqUTRNo"].Value = item.ChequeNo;
                                GvexpenceInfo.Rows[i].Cells["ChqNEFTDate"].Value = item.ChequeDate;
                                GvexpenceInfo.Rows[i].Cells["PaidAmount"].Value = item.PaidAmount;
                                GvexpenceInfo.Rows[i].Cells["Id"].Value = item.Id;
                                GvexpenceInfo.Rows[i].Cells["ACGId"].Value = item.ACGId;
                                i++;
                            }
                        }
                        this.ActiveControl = dtptodate;
                        expencerepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("To Date Should greater than from date.", "Warning");
                        this.ActiveControl = dtptodate;
                    }
                }
                if (chkexpname.Checked == true)
                {
                    if (cmbexpencename1.Text != "Select")
                    {
                        ExpenceRepository expencerepo = new ExpenceRepository();
                        GvexpenceInfo.Rows.Clear();
                        int i = 0;
                        List<Expence> ItemList = expencerepo.GetAll().Where(t => t.ExpenceName == cmbexpencename1.Text && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        label13.Text = Convert.ToString(Convert.ToDecimal(ItemList.Sum(t => t.PaidAmount)));
                        if (ItemList != null)
                        {
                            foreach (var item in ItemList)
                            {
                                GvexpenceInfo.Rows.Add();
                                GvexpenceInfo.Rows[i].Cells["Date"].Value = item.ExpenceDate.Value.ToShortDateString();
                                GvexpenceInfo.Rows[i].Cells["ExpId"].Value = item.ExpenceID;
                                GvexpenceInfo.Rows[i].Cells["ACGroup"].Value = item.AccountingGroupMaster.ACGName;
                                GvexpenceInfo.Rows[i].Cells["ExpenceName"].Value = item.ExpenceName;
                                GvexpenceInfo.Rows[i].Cells["From"].Value = item.FromPerson;
                                GvexpenceInfo.Rows[i].Cells["To"].Value = item.ToPerson;
                                GvexpenceInfo.Rows[i].Cells["Reason"].Value = item.Reason;
                                GvexpenceInfo.Rows[i].Cells["Mode"].Value = item.PaymentMode;
                                GvexpenceInfo.Rows[i].Cells["BankName"].Value = item.Bankname;
                                GvexpenceInfo.Rows[i].Cells["ChqUTRNo"].Value = item.ChequeNo;
                                GvexpenceInfo.Rows[i].Cells["ChqNEFTDate"].Value = item.ChequeDate;
                                GvexpenceInfo.Rows[i].Cells["PaidAmount"].Value = item.PaidAmount;
                                GvexpenceInfo.Rows[i].Cells["Id"].Value = item.Id;
                                GvexpenceInfo.Rows[i].Cells["ACGId"].Value = item.ACGId;
                                i++;
                            }
                        }
                        this.ActiveControl = cmbexpencename1;
                        expencerepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Select Expense Name.", "Warning");
                        this.ActiveControl = cmbexpencename1;
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
                if (Convert.ToInt32(txtexpenceno.Text) <= Convert.ToInt32(maxid))
                {
                    RptExpences expenceidobj = new RptExpences(Convert.ToInt32(txtexpenceno.Text));
                    expenceidobj.ShowDialog();
                    expenceidobj.Dispose();
                }
                else
                {
                    MessageBox.Show("Select Expence From List.", "Warning");
                    this.ActiveControl = cmbexpence;
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
                ExpenceRepository expenceRepo = new ExpenceRepository();
                Expence expencemast = expenceRepo.GetById(expenceid);
                expenceRepo.Remove(expencemast);
                expenceRepo.Save();
                if (expencemast.PaymentMode != "Cash")
                {
                    BankStatementRepository Brepo = new BankStatementRepository();
                    var bdata = Brepo.GetAll().Where(t => t.Bankname == expencemast.Bankname && t.TransactionDate.Value.ToShortDateString() == expencemast.ExpenceDate.Value.ToShortDateString() && t.ChequeNo == expencemast.ChequeNo && t.Withdrawals == Convert.ToDecimal(expencemast.PaidAmount) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (bdata != null)
                    {
                        BankStatement bsdata = Brepo.GetById(bdata.Id);
                        Brepo.Remove(bsdata);
                        Brepo.Save();
                    }
                }
                MessageBox.Show("Record deleted Sucessfully.", "Success");
                clear();
                expenceRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void chkexpname_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                chkdate.Checked = false;
                dtpfromdate.Value = DateTime.Now.Date;
                dtptodate.Value = DateTime.Now.Date;
                expencename1 = "";
                ExpenceName1 = "";
            }
            catch (Exception)
            { }
        }

        private void chkdate_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                chkexpname.Checked = false;
                cmbexpencename1.Text = "Select";
                expencename1 = "";
                ExpenceName1 = "";
            }
            catch (Exception)
            { }
        }

        private void btnprintall_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkexpname.Checked == true && cmbexpencename1.Text != "Select")
                {
                    string expencename = cmbexpencename1.Text;
                    RptExpences expenceidobj = new RptExpences(expencename);
                    expenceidobj.ShowDialog();
                    expenceidobj.Dispose();
                }
                else if (chkdate.Checked == true && dtpfromdate.Value.Date < dtptodate.Value.Date)
                {
                    RptExpences expenceidobj = new RptExpences(Convert.ToString(dtpfromdate.Value), Convert.ToString(dtptodate.Value));
                    expenceidobj.ShowDialog();
                    expenceidobj.Dispose();
                }
                else if (expencename1 != "")
                {
                    RptExpences expenceidobj = new RptExpences(expencename1, Convert.ToString(fdate1), Convert.ToString(tdate1));
                    expenceidobj.ShowDialog();
                    expenceidobj.Dispose();
                }
                else if (ExpenceName1 != "")
                {
                    RptExpences expencesRpt = new RptExpences(fromdate1, todate1, ExpenceName1, ACCName1);
                    expencesRpt.ShowDialog();
                    expencesRpt.Dispose();
                }
                else
                {
                    RptExpences expenceidobj = new RptExpences();
                    expenceidobj.ShowDialog();
                    expenceidobj.Dispose();
                }
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

        private void cmbexpencename1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
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

        private void frmExpences_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
        }

        private void cmbACGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbACGroups.Text != "Select")
                {
                    if (cmbACGroups.Text == "Capital Account")
                    {
                        try
                        {
                            SettingRepository SettingRepo = new SettingRepository();
                            Setting SetData = new Setting();
                            SetData = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                            cmbexpence.DataSource = null;
                            cmbexpence.DisplayMember = "Text";
                            cmbexpence.ValueMember = "Value";
                            cmbexpence.Items.Clear();
                            if (SetData.PropriterName.Contains(','))
                            {
                                string[] propList = SetData.PropriterName.Split(',');
                                for (int i = 0; i < propList.Length; i++)
                                {
                                    cmbexpence.Items.Add(new { Text = propList[i], Value = i });
                                }
                            }
                            else
                                cmbexpence.Items.Add(new { Text = SetData.PropriterName, Value = 0 });
                            cmbexpence.SelectedIndex = 0;
                        }
                        catch (Exception)
                        { }
                    }
                    else if (cmbACGroups.Text == "Direct Incomes" || cmbACGroups.Text == "Income (Direct)" || cmbACGroups.Text == "Income (Indirect)" || cmbACGroups.Text == "Indirect Incomes")
                    {
                        try
                        {
                            IncomeMasterRepository expmastRepo = new IncomeMasterRepository();
                            List<IncomeMaster> expdata = new List<IncomeMaster>();
                            expdata.Clear();
                            expdata = expmastRepo.GetAll().Where(t => t.ACGId == Convert.ToInt32(cmbACGroups.SelectedValue) && t.CompId == CommonMethod.CompId).ToList();
                            expdata.Reverse();
                            cmbexpence.DataSource = expdata;
                            cmbexpence.ValueMember = "IncomeId";
                            cmbexpence.DisplayMember = "IncomeName";
                            expmastRepo.Dispose();
                        }
                        catch (Exception)
                        { }
                    }
                    else
                    {
                        try
                        {
                            ExpenceMasterRepository expmastRepo = new ExpenceMasterRepository();
                            List<ExpenceMaster> expdata = new List<ExpenceMaster>();
                            expdata.Clear();
                            expdata = expmastRepo.GetAll().Where(t => t.ACGId == Convert.ToInt32(cmbACGroups.SelectedValue) && t.CompId == CommonMethod.CompId).ToList();
                            expdata.Reverse();
                            cmbexpence.DataSource = expdata;
                            cmbexpence.ValueMember = "ExpenceId";
                            cmbexpence.DisplayMember = "ExpenceName";
                            expmastRepo.Dispose();
                        }
                        catch (Exception)
                        { }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbACGroups_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbexpence;
            }
            catch (Exception)
            { }
        }

        private void cmbexpence_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtfrom;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmExpenceMaster expences = new frmExpenceMaster();
                        expences.ShowDialog();
                        fillexp();
                        this.ActiveControl = cmbexpence;
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
                    this.ActiveControl = txtto;
            }
            catch (Exception)
            { }
        }

        private void txtto_KeyDown(object sender, KeyEventArgs e)
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
                    this.ActiveControl = cmbpaymentmode;
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
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmBankInformation bank = new frmBankInformation();
                        bank.ShowDialog();
                        fillcombo();
                        this.ActiveControl = cmbbankname;
                    }
                }
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

        private void chkexpname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbexpencename1;
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

        private void GvexpenceInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["Reason"].Value) != "Journal Entry")
                {
                    dtpexpencedate.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["Date"].Value);
                    txtexpenceno.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["ExpId"].Value);
                    cmbACGroups.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["ACGroup"].Value);
                    cmbexpence.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["ExpenceName"].Value);
                    txtfrom.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["From"].Value);
                    txtto.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["To"].Value);
                    txtreason.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["Reason"].Value);
                    cmbpaymentmode.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["Mode"].Value);
                    cmbbankname.Text = Convert.ToString(GvexpenceInfo.Rows[e.RowIndex].Cells["BankName"].Value);
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

        private void btnExpForm_Click(object sender, EventArgs e)
        {
            try
            {
                frmExpenceMaster expmast = new frmExpenceMaster();
                expmast.ShowDialog();
                fillexp();
                this.ActiveControl = cmbexpence;
            }
            catch (Exception)
            { }
        }
    }
}

            