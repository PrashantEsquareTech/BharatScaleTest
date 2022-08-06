using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmBonus : Form
    {
        public static int customerid, billreceiptno, ACGId = 0;
        public static string customername;
        public static frmUnpaidBonusList newlist;
        public static frmBonus newbonus;
        CommonMethod cm = new CommonMethod();

        public frmBonus()
        {
            try
            {
                InitializeComponent();
                cm.changedatetimepickerrange(this);
                MaxBonusNo();
                customerdata();
                fillcombo();
                customerdata1();
            }
            catch { }
        }

        public frmBonus(int RecieptId, frmUnpaidBonusList unpaidlist)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            newlist = unpaidlist;
            try
            {
                MaxBonusNo();
                customerdata();
                fillcombo();
                customerdata1();
                cmbcustomer.SelectedValue = RecieptId;
            }
            catch { }
        }

        private void cmbcustomer_Leave(object sender, EventArgs e)
        {
            CustomerRepository custrepo2 = new CustomerRepository();
            CustomerInformation custdata2 = new CustomerInformation();
            custdata2 = custrepo2.GetAll().Where(t => t.CustomerCode == cmbcustomer.Text && t.CustomerCode != null && t.CustomerCode != "").FirstOrDefault();
            if (custdata2 != null)
            {
                customerdata();
                cmbcustomer.SelectedValue = Convert.ToInt32(custdata2.Id);
            }
        }

        private void cmbcustomername1_Leave(object sender, EventArgs e)
        {
            CustomerRepository custrepo2 = new CustomerRepository();
            CustomerInformation custdata2 = new CustomerInformation();
            custdata2 = custrepo2.GetAll().Where(t => t.CustomerCode == cmbcustomername1.Text && t.CustomerCode != null && t.CustomerCode != "").FirstOrDefault();
            if (custdata2 != null)
            {
                customerdata1();
                cmbcustomername1.SelectedValue = Convert.ToInt32(custdata2.Id);
            }
        }

        public void bonusdata(int custId, frmUnpaidBonusList unpaidlist)
        {
            newlist = unpaidlist;
            try
            {
                MaxBonusNo();
                customerdata();
                fillcombo();
                customerdata1();
                cmbcustomer.SelectedValue = custId;
            }
            catch { }
        }

        public void customerdata1()
        {
            try
            {
                CustomerRepository custrepo = new CustomerRepository();
                List<CustomerInformation> customerdata = new List<CustomerInformation>();
                customerdata.Clear();
                customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId && t.BonusIn != null && t.BonusIn != "").OrderByDescending(t => t.CustomerName).ToList();
                customerdata.Add(new CustomerInformation { CustomerName = "Select", Id = 0 });
                customerdata.Reverse();
                cmbcustomername1.DataSource = customerdata;
                cmbcustomername1.ValueMember = "Id";
                cmbcustomername1.DisplayMember = "CustomerName";
                cmbcustomername1.SelectedValue = 0;
                custrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void cmbcustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (new DbClass()).GetTable("SELECT TOP(1) RemainingAmt from dbo.CustomerBonus where  CustomerBonus.CustomerId='" + cmbcustomer.SelectedValue + "' and CustomerBonus.TransactionYear='" + CommonMethod.TransactionYear + "' and CustomerBonus.compId=" + CommonMethod.CompId + " order by Id desc");
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        if (dt.Rows[0]["RemainingAmt"] != null && dt.Rows[0]["RemainingAmt"].ToString() != "")
                            txtTotalAmt.Text = dt.Rows[0]["RemainingAmt"].ToString();
                    }
                    catch (Exception)
                    { txtTotalAmt.Text = "0.00"; }
                }
                else
                    txtTotalAmt.Text = "0.00";
            }
            catch { }
        }

        public void customerdata()
        {
            try
            {
                CustomerRepository custrepo = new CustomerRepository();
                List<CustomerInformation> customerdata = new List<CustomerInformation>();
                customerdata.Clear();
                customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId && t.BonusIn != null && t.BonusIn != "").OrderByDescending(t => t.CustomerName).ToList();
                customerdata.Add(new CustomerInformation { CustomerName = "Select", Id = 0 });
                customerdata.Reverse();
                cmbcustomer.DataSource = customerdata;
                cmbcustomer.ValueMember = "Id";
                cmbcustomer.DisplayMember = "CustomerName";
                cmbcustomer.SelectedValue = 0;
                custrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void MaxBonusNo()
        {
            try
            {
                BonusRepository remainingpayrepo = new BonusRepository();
                var mid = remainingpayrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.BonusId);
                if (mid == null)
                    txtreciptNo.Text = "1";
                else
                    txtreciptNo.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                remainingpayrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            MaxBonusNo();
            txtchequeno.Text = "";
            txtnarration.Text = "";
            txtPaidAmount.Text = "";
            txtRemainingAmt.Text = "";
            txtTotalAmt.Text = "";
            btnSave.Text = "Save";
            cmbbank.SelectedIndex = 0;
            cmbcustomer.SelectedIndex = 0;
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
            }
            catch (Exception)
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbcustomer.Text != "Select" && cmbpaymentmode.Text != "" && txtTotalAmt.Text != "")
                {
                    if (btnSave.Text == "Save")
                    {
                        BonusRepository remainingpayrepo = new BonusRepository();
                        CustomerBonu remainingdata = new CustomerBonu();
                        remainingdata.CustomerId = Convert.ToInt32(cmbcustomer.SelectedValue);
                        remainingdata.BonusId = Convert.ToInt32(txtreciptNo.Text);
                        remainingdata.CreateDate = dtpreceiptDate.Value;
                        remainingdata.TotalBonusAmount = Convert.ToDecimal(txtTotalAmt.Text);
                        remainingdata.PaidAmt = Convert.ToDecimal(txtPaidAmount.Text);
                        remainingdata.Status = "Paid Bonus";
                        remainingdata.RemainingAmt = Convert.ToDecimal(txtRemainingAmt.Text);
                        remainingdata.TransactionYear = CommonMethod.TransactionYear;
                        remainingdata.CompId = CommonMethod.CompId;
                        remainingdata.PaymentMode = cmbpaymentmode.Text;
                        remainingdata.Narration = txtnarration.Text;
                        remainingdata.PaymentDate = dtpreceiptDate.Value;
                        if (cmbpaymentmode.Text == "Cash")
                        {
                            remainingdata.Bankname = "";
                            remainingdata.Chequeno = "";
                            remainingdata.Chequedate = null;
                            remainingpayrepo.Add(remainingdata);
                            remainingpayrepo.Save();
                            customerid = Convert.ToInt32(cmbcustomer.SelectedValue);
                            customername = cmbcustomer.Text;
                            billreceiptno = Convert.ToInt32(txtreciptNo.Text);
                            MessageBox.Show("Bonus paid Information Saved.", "Success");
                            fillcombo();
                        }
                        else if (cmbpaymentmode.Text == "Cheque" || cmbpaymentmode.Text == "By Swipe")
                        {
                            if (cmbbank.Text != "Select" || txtchequeno.Text != "" || txtBankName.Text != "")
                            {
                                if (cmbbank.Text != "Select")
                                    remainingdata.Bankname = cmbbank.SelectedValue.ToString();
                                else
                                    remainingdata.Bankname = txtBankName.Text;
                                remainingdata.Chequeno = txtchequeno.Text;
                                remainingdata.Chequedate = dtpchequedate.Value;
                                remainingpayrepo.Add(remainingdata);
                                remainingpayrepo.Save();
                                customerid = Convert.ToInt32(cmbcustomer.SelectedValue);
                                customername = cmbcustomer.Text;
                                billreceiptno = Convert.ToInt32(txtreciptNo.Text);
                                try
                                {
                                    BankRepository bankInfoRepo = new BankRepository();
                                    var bankData = bankInfoRepo.GetAll().Where(t => t.BankId == Convert.ToInt32(cmbbank.SelectedValue) && t.Bankname == cmbbank.Text).FirstOrDefault();
                                    if (bankData != null)
                                        ACGId = Convert.ToInt32(bankData.ACGId);
                                    BankStatementRepository bankrepo = new BankStatementRepository();
                                    BankStatement bankstdata = new BankStatement();
                                    bankstdata.BankId = Convert.ToInt32(cmbbank.SelectedValue);
                                    bankstdata.Bankname = Convert.ToString(cmbbank.Text);
                                    bankstdata.TransactionDate = dtpreceiptDate.Value;
                                    bankstdata.Particulars = "Paid to" + " " + Convert.ToString(cmbcustomer.Text.Trim());
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    bankstdata.Deposits = Convert.ToDecimal(remainingdata.PaidAmt);
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankstdata.TransactionType = "Bonus";
                                    bankstdata.ACGId = ACGId;
                                    remainingdata.Narration = txtnarration.Text;
                                    bankrepo.Add(bankstdata);
                                    bankrepo.Save();
                                    bankrepo.Dispose();
                                }
                                catch (Exception)
                                { }
                                MessageBox.Show("Receipt Information Saved.", "Success");
                                fillcombo();
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
                            if (cmbbank.Text.TrimStart() != "Select" && txtchequeno.Text.TrimStart() != "" || txtBankName.Text != "")
                            {
                                if (cmbbank.Text != "Select")
                                    remainingdata.Bankname = cmbbank.SelectedValue.ToString();
                                else
                                    remainingdata.Bankname = txtBankName.Text;
                                remainingdata.Chequeno = txtchequeno.Text;
                                remainingdata.Chequedate = dtpchequedate.Value;
                                remainingpayrepo.Add(remainingdata);
                                remainingpayrepo.Save();
                                customerid = Convert.ToInt32(cmbcustomer.SelectedValue);
                                customername = cmbcustomer.Text;
                                billreceiptno = Convert.ToInt32(txtreciptNo.Text);
                                try
                                {
                                    BankRepository bankInfoRepo = new BankRepository();
                                    var bankData = bankInfoRepo.GetAll().Where(t => t.BankId == Convert.ToInt32(cmbbank.SelectedValue) && t.Bankname == cmbbank.Text).FirstOrDefault();
                                    if (bankData != null)
                                        ACGId = Convert.ToInt32(bankData.ACGId);
                                    BankStatementRepository bankrepo = new BankStatementRepository();
                                    BankStatement bankstdata = new BankStatement();
                                    bankstdata.BankId = Convert.ToInt32(cmbbank.SelectedValue);
                                    bankstdata.Bankname = Convert.ToString(cmbbank.Text);
                                    bankstdata.TransactionDate = dtpreceiptDate.Value;
                                    bankstdata.Particulars = "Paid to" + " " + Convert.ToString(cmbcustomer.Text.Trim());
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    bankstdata.Deposits = Convert.ToDecimal(remainingdata.PaidAmt);
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankstdata.TransactionType = "Bonus";
                                    bankstdata.ACGId = ACGId;
                                    bankrepo.Add(bankstdata);
                                    bankrepo.Save();
                                    bankrepo.Dispose();
                                }
                                catch (Exception)
                                { }
                                MessageBox.Show("Receipt Information Saved.", "Success");
                                fillcombo();
                            }
                            else
                            {
                                if (cmbbank.Text.TrimStart() == "Select")
                                {
                                    MessageBox.Show("Select Bank Name.", "Success");
                                    this.ActiveControl = cmbbank;
                                }
                                else
                                {
                                    MessageBox.Show("Enter UTR Number.", "Success");
                                    this.ActiveControl = txtchequeno;
                                }
                            }
                        }
                        remainingpayrepo.Dispose();
                    }
                    else
                    {
                        billreceiptno = Convert.ToInt32(txtreciptNo.Text);
                        BonusRepository remainingpayrepo = new BonusRepository();
                        CustomerBonu remainingdata = new CustomerBonu();
                        remainingdata = remainingpayrepo.GetAll().Where(t => t.BonusId == billreceiptno && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                        remainingdata.CustomerId = Convert.ToInt32(cmbcustomer.SelectedValue);
                        remainingdata.CreateDate = dtpreceiptDate.Value;
                        remainingdata.TransactionYear = CommonMethod.TransactionYear;
                        remainingdata.Status = "Paid Bonus";
                        remainingdata.CompId = CommonMethod.CompId;
                        remainingdata.TotalBonusAmount = Convert.ToDecimal(txtTotalAmt.Text);
                        remainingdata.PaidAmt = Convert.ToDecimal(txtPaidAmount.Text);
                        remainingdata.RemainingAmt = Convert.ToDecimal(txtRemainingAmt.Text);
                        remainingdata.PaymentMode = cmbpaymentmode.Text;
                        remainingdata.PaymentDate = dtpreceiptDate.Value;
                        remainingdata.Narration = txtnarration.Text;
                        if (cmbpaymentmode.Text == "Cash")
                        {
                            remainingdata.Bankname = "";
                            remainingdata.Chequeno = "";
                            remainingdata.Chequedate = null;
                            remainingpayrepo.Edit(remainingdata);
                            remainingpayrepo.Save();
                            customerid = Convert.ToInt32(cmbcustomer.SelectedValue);
                            decimal decremamt = Convert.ToDecimal(txtRemainingAmt.Text);
                            BonusRepository remainingpayrepo2 = new BonusRepository();
                            List<CustomerBonu> remainingdata2 = new List<CustomerBonu>();
                            remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(customerid) && t.Id > billreceiptno && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                            int i = 0;
                            foreach (var item in remainingdata2)
                            {
                                item.RemainingAmt = (Convert.ToDecimal(item.TotalBonusAmount) - Convert.ToDecimal(item.PaidAmt)) + decremamt;
                                decremamt = (Convert.ToDecimal(item.TotalBonusAmount) - Convert.ToDecimal(item.PaidAmt)) + decremamt;
                                remainingpayrepo2.Edit(item);
                                remainingpayrepo2.Save();
                                i++;
                            }
                            customername = cmbcustomer.Text;
                            billreceiptno = Convert.ToInt32(txtreciptNo.Text);
                            MessageBox.Show("Receipt Information Saved.", "Success");
                            fillcombo();
                        }
                        else if (cmbpaymentmode.Text == "Cheque" || cmbpaymentmode.Text == "By Swipe")
                        {
                            if (cmbbank.Text != "Select" || txtchequeno.Text != "")
                            {
                                remainingdata.Bankname = cmbbank.Text;
                                remainingdata.Chequeno = txtchequeno.Text;
                                remainingdata.Chequedate = dtpchequedate.Value;
                                try
                                {
                                    BonusRepository remRepo = new BonusRepository();
                                    CustomerBonu remdata = remRepo.GetById(billreceiptno);
                                    BankStatementRepository BankStRepo = new BankStatementRepository();
                                    var bdata = BankStRepo.GetAll().Where(t => t.Bankname == cmbbank.Text && t.CompId == CommonMethod.CompId && t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.Chequeno && t.Deposits == Convert.ToDecimal(remdata.PaidAmt)).FirstOrDefault();
                                    BankRepository bankInfoRepo = new BankRepository();
                                    var bankData = bankInfoRepo.GetAll().Where(t => t.BankId == Convert.ToInt32(cmbbank.SelectedValue) && t.Bankname == cmbbank.Text).FirstOrDefault();
                                    if (bankData != null)
                                        ACGId = Convert.ToInt32(bankData.ACGId);
                                    BankStatementRepository bankrepo = new BankStatementRepository();
                                    BankStatement bankstdata = new BankStatement();
                                    bankstdata.BankId = Convert.ToInt32(cmbbank.SelectedValue);
                                    bankstdata.Bankname = Convert.ToString(cmbbank.Text);
                                    bankstdata.TransactionDate = dtpreceiptDate.Value;
                                    bankstdata.Particulars = "Paid to" + " " + Convert.ToString(cmbcustomer.Text.Trim());
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    bankstdata.Deposits = Convert.ToDecimal(txtPaidAmount.Text);
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankstdata.TransactionType = "Receipt";
                                    bankstdata.ACGId = ACGId;
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
                                remainingpayrepo.Edit(remainingdata);
                                remainingpayrepo.Save();
                                customerid = Convert.ToInt32(cmbcustomer.SelectedValue);
                                decimal decremamt = Convert.ToDecimal(txtRemainingAmt.Text);

                                BonusRepository remainingpayrepo2 = new BonusRepository();
                                List<CustomerBonu> remainingdata2 = new List<CustomerBonu>();
                                remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(customerid) && t.Id > billreceiptno && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                int i = 0;
                                foreach (var item in remainingdata2)
                                {
                                    item.RemainingAmt = (Convert.ToDecimal(item.TotalBonusAmount) - Convert.ToDecimal(item.PaidAmt)) + decremamt;
                                    decremamt = (Convert.ToDecimal(item.TotalBonusAmount) - Convert.ToDecimal(item.PaidAmt)) + decremamt;
                                    remainingpayrepo2.Edit(item);
                                    remainingpayrepo2.Save();
                                    i++;
                                }
                                customername = cmbcustomer.Text;
                                billreceiptno = Convert.ToInt32(txtreciptNo.Text);
                                MessageBox.Show("Receipt Information Saved.", "Success");
                                fillcombo();
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
                                remainingdata.Bankname = cmbbank.Text;
                                remainingdata.Chequeno = txtchequeno.Text;
                                remainingdata.Chequedate = dtpchequedate.Value;
                                try
                                {
                                    BonusRepository remRepo = new BonusRepository();
                                    CustomerBonu remdata = remRepo.GetById(billreceiptno);
                                    BankStatementRepository BankStRepo = new BankStatementRepository();
                                    var bdata = BankStRepo.GetAll().Where(t => t.Bankname == cmbbank.Text && t.CompId == CommonMethod.CompId && t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.Chequeno && t.Deposits == Convert.ToDecimal(remdata.PaidAmt)).FirstOrDefault();
                                    BankRepository bankInfoRepo = new BankRepository();
                                    var bankData = bankInfoRepo.GetAll().Where(t => t.BankId == Convert.ToInt32(cmbbank.SelectedValue) && t.Bankname == cmbbank.Text).FirstOrDefault();
                                    if (bankData != null)
                                        ACGId = Convert.ToInt32(bankData.ACGId);
                                    BankStatementRepository bankrepo = new BankStatementRepository();
                                    BankStatement bankstdata = new BankStatement();
                                    bankstdata.BankId = Convert.ToInt32(cmbbank.SelectedValue);
                                    bankstdata.Bankname = Convert.ToString(cmbbank.Text);
                                    bankstdata.TransactionDate = dtpreceiptDate.Value;
                                    bankstdata.Particulars = "Paid to" + " " + Convert.ToString(cmbcustomer.Text.Trim());
                                    bankstdata.ChequeNo = Convert.ToString(txtchequeno.Text);
                                    bankstdata.Deposits = Convert.ToDecimal(txtPaidAmount.Text);
                                    bankstdata.Balance = 0;
                                    bankstdata.CompId = CommonMethod.CompId;
                                    bankstdata.TransactionType = "Receipt";
                                    bankstdata.ACGId = ACGId;
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
                                remainingpayrepo.Edit(remainingdata);
                                remainingpayrepo.Save();
                                customerid = Convert.ToInt32(cmbcustomer.SelectedValue);
                                decimal decremamt = Convert.ToDecimal(txtRemainingAmt.Text);
                                BonusRepository remainingpayrepo2 = new BonusRepository();
                                List<CustomerBonu> remainingdata2 = new List<CustomerBonu>();
                                remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(customerid) && t.Id > billreceiptno && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                int i = 0;
                                foreach (var item in remainingdata2)
                                {
                                    item.RemainingAmt = (Convert.ToDecimal(item.TotalBonusAmount) - Convert.ToDecimal(item.PaidAmt)) + decremamt;
                                    decremamt = (Convert.ToDecimal(item.TotalBonusAmount) - Convert.ToDecimal(item.PaidAmt)) + decremamt;
                                    remainingpayrepo2.Edit(item);
                                    remainingpayrepo2.Save();
                                    i++;
                                }
                                customername = cmbcustomer.Text;
                                billreceiptno = Convert.ToInt32(txtreciptNo.Text);
                                MessageBox.Show("Receipt Information Saved.", "Success");
                                fillcombo();
                            }
                            else
                            {
                                if (cmbbank.Text.TrimStart() == "Select")
                                {
                                    MessageBox.Show("Select Bank Name.", "Success");
                                    this.ActiveControl = cmbbank;
                                }
                                else
                                {
                                    MessageBox.Show("Enter UTR Number.", "Success");
                                    this.ActiveControl = txtchequeno;
                                }
                            }
                        }
                        remainingpayrepo.Dispose();
                    }
                }
                else
                {
                    if (cmbcustomer.Text == "Select")
                    {
                        MessageBox.Show("Select Customer Name.", "Warning");
                        this.ActiveControl = cmbcustomer;
                    }
                    else if (txtPaidAmount.Text == "")
                    {
                        MessageBox.Show("Enter Amount.", "Warning");
                        this.ActiveControl = txtPaidAmount;
                    }
                    else
                    {
                        MessageBox.Show("Select Payment Mode.", "Warning");
                        this.ActiveControl = cmbpaymentmode;
                    }
                }
                clear();
                if (newlist != null)
                    newlist.fillgrid();
            }
            catch (Exception)
            { }
        }

        private void btnpaidList_Click(object sender, EventArgs e)
        {
            newbonus = this;
            frmUnpaidBonusList bonuslist = new frmUnpaidBonusList();
            bonuslist.ShowDialog();
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (new DbClass()).GetTable("SELECT TOP(1) RemainingAmt from dbo.CustomerBonus where  CustomerBonus.CustomerId='" + cmbcustomername1.SelectedValue + "' and CustomerBonus.TransactionYear='" + CommonMethod.TransactionYear + "' and CustomerBonus.compId=" + CommonMethod.CompId + " order by Id desc");
                if (dt.Rows.Count > 0)
                {
                    try
                    {
                        if (dt.Rows[0]["RemainingAmt"] != null && dt.Rows[0]["RemainingAmt"].ToString() != "")
                            lbltbonus.Text = dt.Rows[0]["RemainingAmt"].ToString();
                    }
                    catch (Exception)
                    { lbltbonus.Text = "0.00"; }
                }
                else
                    lbltbonus.Text = "0.00";
            }
            catch { }
            try
            {
                if (chkcustomername1.Checked == true)
                {
                    DataTable dt = (new DbClass()).GetTable("SELECT    CustomerInformation.CustomerName, CustomerBillMaster.BillNo , CustomerBonus.Id,CustomerBonus.BonusId, CustomerBonus.CustomerId, CustomerBonus.TotalBonusAmount, CustomerBonus.PaidAmt, CustomerBonus.RemainingAmt, " +
                                                             " CustomerBonus.PaymentMode, CustomerBonus.Bankname, CustomerBonus.Chequeno, CustomerBonus.Chequedate, CustomerBonus.PaymentDate,                                              " +
                                                             " CustomerBonus.Status, CustomerBonus.Narration                                                                                                                      " +
                                                             " FROM         CustomerBonus INNER JOIN                                                                                                                                                              " +
                                                             " CustomerInformation ON CustomerBonus.CustomerId = CustomerInformation.Id LEFT OUTER JOIN " +
                                                             " CustomerBillMaster ON CustomerBonus.BillId = CustomerBillMaster.Id where  CustomerBonus.TransactionYear= '" + CommonMethod.TransactionYear + "' and CustomerBonus.CustomerId=" + Convert.ToInt32(cmbcustomername1.SelectedValue) + " and CustomerBonus.CompId=" + CommonMethod.CompId);
                    GvreceiptInfo.DataSource = dt;
                    GvreceiptInfo.Columns["Id"].Visible = false;
                    GvreceiptInfo.Columns["BonusId"].Visible = false;
                    GvreceiptInfo.Columns["CustomerId"].Visible = false;
                }
                else
                    MessageBox.Show("Check the checkbox first");
            }
            catch { }
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtRemainingAmt.Text = (Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(txtPaidAmount.Text)).ToString();
            }
            catch { }
        }

        private void btnGettAll_Click(object sender, EventArgs e)
        {
            fillgrid();
        }

        public void fillgrid()
        {
            try
            {
                DataTable dt = (new DbClass()).GetTable("SELECT      CustomerInformation.CustomerName, CustomerBillMaster.BillNo,CustomerBonus.Id,CustomerBonus.BonusId, CustomerBonus.CustomerId, CustomerBonus.TotalBonusAmount, CustomerBonus.PaidAmt, CustomerBonus.RemainingAmt, " +
                                                         " CustomerBonus.PaymentMode, CustomerBonus.Bankname, CustomerBonus.Chequeno, CustomerBonus.Chequedate, CustomerBonus.PaymentDate,                                              " +
                                                         " CustomerBonus.Status,CustomerBonus.Narration                                                                                                                       " +
                                                         " FROM         CustomerBonus INNER JOIN                                                                                                                                                              " +
                                                         " CustomerInformation ON CustomerBonus.CustomerId = CustomerInformation.Id LEFT OUTER JOIN " +
                                                         " CustomerBillMaster ON CustomerBonus.BillId = CustomerBillMaster.Id where  CustomerBonus.TransactionYear= '" + CommonMethod.TransactionYear + "'  and CustomerBonus.CompId=" + CommonMethod.CompId);
                GvreceiptInfo.DataSource = dt;
            }
            catch { }
        }

        public void OnBonusPaid()
        {
            try
            {
                chkcustomername1.Checked = true;
                cmbcustomername1.SelectedValue = cmbcustomer.SelectedValue;
                btnsearch.PerformClick();
            }
            catch { }
        }

        private void GvreceiptInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GvreceiptInfo.Rows[e.RowIndex].Cells["Status"].Value.ToString() == "Paid Bonus")
                {
                    txtreciptNo.Text = GvreceiptInfo.Rows[e.RowIndex].Cells["BonusId"].Value.ToString();
                    cmbcustomer.SelectedValue = Convert.ToInt32(GvreceiptInfo.Rows[e.RowIndex].Cells["CustomerId"].Value.ToString());
                    txtTotalAmt.Text = GvreceiptInfo.Rows[e.RowIndex].Cells["TotalBonusAmount"].Value.ToString();
                    txtPaidAmount.Text = GvreceiptInfo.Rows[e.RowIndex].Cells["PaidAmt"].Value.ToString();
                    txtBankName.Text = GvreceiptInfo.Rows[e.RowIndex].Cells["Bankname"].Value.ToString();
                    cmbbank.Text = GvreceiptInfo.Rows[e.RowIndex].Cells["Bankname"].Value.ToString();
                    txtnarration.Text = GvreceiptInfo.Rows[e.RowIndex].Cells["Narration"].Value.ToString();
                    cmbpaymentmode.Text = GvreceiptInfo.Rows[e.RowIndex].Cells["PaymentMode"].Value.ToString();
                    txtchequeno.Text = GvreceiptInfo.Rows[e.RowIndex].Cells["Chequeno"].Value.ToString();
                    try
                    {
                        dtpchequedate.Value = Convert.ToDateTime(GvreceiptInfo.Rows[e.RowIndex].Cells["Chequedate"].Value.ToString());
                    }
                    catch { }
                    try
                    {
                        dtpreceiptDate.Value = Convert.ToDateTime(GvreceiptInfo.Rows[e.RowIndex].Cells["PaymentDate"].Value.ToString());
                    }
                    catch { }
                    btnSave.Text = "Update";
                }
                else
                {
                    MaxBonusNo();
                    txtTotalAmt.Text = "0";
                    txtPaidAmount.Text = "0";
                    txtBankName.Text = "0";
                    cmbbank.SelectedValue = 0;
                    txtnarration.Text = "";
                    cmbpaymentmode.Text = "";
                    txtchequeno.Text = "";
                    cmbcustomer.SelectedValue = 0;
                    cmbcustomer.SelectedValue = Convert.ToInt32(GvreceiptInfo.Rows[e.RowIndex].Cells["CustomerId"].Value.ToString());
                }
            }
            catch { }
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

        private void cmbpaymentmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbpaymentmode.Text == "Cash")
            {
                cmbbank.Enabled = false;
                txtBankName.Enabled = false;
            }
            else if (cmbpaymentmode.Text == "Cheque")
            {
                dtpchequedate.Enabled = true;
                dtpchequedate.Enabled = true;
            }
            else
            {
                cmbbank.Enabled = true;
                txtBankName.Enabled = true;
                dtpchequedate.Enabled = false;
                dtpchequedate.Enabled = false;
            }
        }
    }
}