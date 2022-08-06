using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.RachitControls;
using System.Data.SqlClient;
using System.Data;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class FrmProprietorMaster : Form
    {
        DbClass db = new DbClass();
        public int Proprietorid;
        CommonMethod cm = new CommonMethod();

        public FrmProprietorMaster()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxcustomerid();            
            fillcombo();
            BindAcGroup();
            Bindcustomerinfo();
            btndelete.Enabled = false;
            cmbAccountGroup.SelectedIndex = cmbAccountGroup.FindStringExact("Capital Account");
            this.ActiveControl = txtProprietorName;
        }

        public FrmProprietorMaster(int pid)
        {
            try
            {
                InitializeComponent();
                cm.changedatetimepickerrange(this);
                Proprietorid = pid;
                ProprietorMaster custdata = new ProprietorMaster();
                ProprietorMasterRepository custrepo = new ProprietorMasterRepository();
                custdata = custrepo.GetAll().Where(t => t.ProprietorId == pid && t.CompId == CommonMethod.CompId).FirstOrDefault();
                txtProprietorID.Text = custdata.ProprietorId.ToString();
                txtProprietorName.Text = custdata.ProprietorName.ToString();
                txtProprietorAddress.Text = custdata.Address.ToString();
                txtmobileno.Text = custdata.MobileNo.ToString();
                txtAdharCardNo.Text = custdata.AdharCardNo.ToString();
                txtPanno.Text = custdata.Panno;
                cmbAccountGroup.SelectedValue = custdata.ACGId;
                dtpOpeningBalanceDate.Value = Convert.ToDateTime(custdata.OpeningBalanceDate);
                ProprietorLedgerRepository paymentrepo = new ProprietorLedgerRepository();
                try
                {
                    decimal OpeAmt = Convert.ToDecimal(paymentrepo.GetAll().Where(t => t.Type == "OpeningBalance" && t.ProprietorId == pid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault().NetAmount);
                    txtopeningBal.Text = Convert.ToString(OpeAmt);
                }
                catch (Exception)
                {
                    txtopeningBal.Text = "0";
                }
                try
                {
                    ProprietorLedgerRepository remainingpayrepo = new ProprietorLedgerRepository();
                    List<ProprietorLedger> remainingamt = remainingpayrepo.GetAll().Where(t => t.ProprietorId == pid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                    decimal netamt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.ProprietorId == pid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmount));
                    decimal Paidamt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.ProprietorId == pid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmount));
                    lblAmt.Visible = true;
                    lblRemAmt.Visible = true;
                    if (remainingamt != null)
                        lblAmt.Text = Convert.ToString(netamt - Paidamt);
                    else
                        lblAmt.Text = "0";
                    remainingpayrepo.Dispose();
                }
                catch (Exception)
                { }
                Bindcustomerinfo();
                btndelete.Enabled = false;
                btnSave.Text = "Update";
                this.ActiveControl = txtmobileno;
            }
            catch (Exception)
            {
                MessageBox.Show("error");
            }
        }

        public void maxcustomerid()
        {
            try
            {
                ProprietorMasterRepository custrepo = new ProprietorMasterRepository();
                var mid = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.ProprietorId);
                if (mid == null)
                    txtProprietorID.Text = "1";
                else
                    txtProprietorID.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                custrepo.Dispose();
            }
            catch (Exception)
            { }
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
                cmbAccountGroup.DataSource = ACGdata;
                cmbAccountGroup.ValueMember = "Id";
                cmbAccountGroup.DisplayMember = "ACGName";
                ACGRepo.Dispose();
                cmbAccountGroup.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                txtProprietorName.Text = "";
                txtProprietorAddress.Text = "";
                txtmobileno.Text = "";
                maxcustomerid();
                btndelete.Enabled = false;
                fillcombo();
                BindAcGroup();
                btnSave.Text = "Save";
                txtopeningBal.Text = "";
                cmbAccountGroup.SelectedIndex = cmbAccountGroup.FindStringExact("Capital Account");
                txtopeningBal.Enabled = true;
                txtPanno.Text = "";
                txtAdharCardNo.Text = "";
                Bindcustomerinfo();
            }
            catch (Exception)
            { }
        }

        public void fillcombo()
        {
            try
            {
                ProprietorMasterRepository custrepo = new ProprietorMasterRepository();
                List<ProprietorMaster> customerdata = new List<ProprietorMaster>();
                customerdata.Clear();
                customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.ProprietorName).ToList();
                customerdata.Add(new ProprietorMaster { ProprietorName = "Select", ProprietorId = 0 });
                customerdata.Reverse();
                cmbProprietorName.DataSource = customerdata;
                cmbProprietorName.ValueMember = "ProprietorId";
                cmbProprietorName.DisplayMember = "ProprietorName";
                custrepo.Dispose();
                cmbProprietorName.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void Bindcustomerinfo()
        {
            try
            {
                ProprietorMasterRepository custrepo = new ProprietorMasterRepository();
                GvProprietorInfo.Rows.Clear();
                List<ProprietorMaster> ItemList = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).ToList();
                lblTotaluCustomers.Text = Convert.ToString(ItemList.Count());
                if (ItemList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        GvProprietorInfo.Rows.Add();
                        GvProprietorInfo.Rows[i].Cells["Id"].Value = item.Id;
                        GvProprietorInfo.Rows[i].Cells["ProprietorIdg"].Value = item.ProprietorId;
                        GvProprietorInfo.Rows[i].Cells["ProprietorName"].Value = item.ProprietorName;
                        GvProprietorInfo.Rows[i].Cells["Address"].Value = item.Address;
                        GvProprietorInfo.Rows[i].Cells["MobileNo"].Value = item.MobileNo;
                        GvProprietorInfo.Rows[i].Cells["PANNo"].Value = item.Panno;
                        GvProprietorInfo.Rows[i].Cells["AdharCardNo"].Value = item.AdharCardNo;
                        GvProprietorInfo.Rows[i].Cells["ACGroupId"].Value = item.ACGId;
                        GvProprietorInfo.Rows[i].Cells["OpeningBalDate"].Value = item.OpeningBalanceDate;
                        i++;
                    }
                }
                custrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ProprietorLedgerRepository paymentrepo = new ProprietorLedgerRepository();
                ProprietorLedger paydata = new ProprietorLedger();
                if (txtProprietorName.Text.Trim() != "" && cmbAccountGroup.SelectedIndex > 0)
                {
                    if (btnSave.Text == "Update")
                    {
                        ProprietorMasterRepository custrepo1 = new ProprietorMasterRepository();
                        var cust = custrepo1.GetAll().Where(t => t.ProprietorName.ToLower() == txtProprietorName.Text.Trim().ToLower() && t.ProprietorId != Convert.ToInt32(txtProprietorID.Text) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (cust != null)
                        {
                            MessageBox.Show("Already Having This Name.", "Error");
                            txtProprietorName.Text = "";
                            this.ActiveControl = txtProprietorName;
                            goto a;
                        }
                        else
                        {
                            ProprietorMasterRepository custrepo = new ProprietorMasterRepository();
                            ProprietorMaster custdata = new ProprietorMaster();
                            custdata = custrepo.GetById(Proprietorid);
                            custdata.ProprietorId = Convert.ToInt32(txtProprietorID.Text);
                            custdata.ProprietorName = txtProprietorName.Text.Trim();
                            custdata.Address = txtProprietorAddress.Text.Trim();
                            custdata.MobileNo = txtmobileno.Text.Trim();
                            custdata.Panno = txtPanno.Text;
                            custdata.AdharCardNo = txtAdharCardNo.Text;
                            custdata.CompId = CommonMethod.CompId;
                            custdata.ACGId = Convert.ToInt32(cmbAccountGroup.SelectedValue);
                            if (txtopeningBal.Text.Trim() != "" && !string.IsNullOrEmpty(txtopeningBal.Text))
                                custdata.OpeningBalanceDate = dtpOpeningBalanceDate.Value;
                            custrepo.Edit(custdata);
                            custrepo.Save();
                            if (txtopeningBal.Text != "" && txtopeningBal.Enabled == true && !string.IsNullOrEmpty(txtopeningBal.Text))
                            {
                                try
                                {
                                    ProprietorLedgerRepository pRepo = new ProprietorLedgerRepository();
                                    var remData = paymentrepo.GetAll().Where(t => t.Type == "OpeningBalance" && t.ProprietorId == custdata.ProprietorId && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    if (remData != null)
                                    {
                                        paydata.ProprietorId = Convert.ToInt32(custdata.ProprietorId);
                                        paydata.ProprietorName = Convert.ToString(custdata.ProprietorName);
                                        paydata.ReceiptNo = remData.ReceiptNo;
                                        paydata.CreateDate = dtpOpeningBalanceDate.Value;
                                        paydata.Type = "OpeningBalance";
                                        if (txtopeningBal.Text != "")
                                        {
                                            paydata.NetAmount = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                            paydata.RemainingAmount = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                            decimal decremamt = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                            ProprietorLedgerRepository remainingpayrepo2 = new ProprietorLedgerRepository();
                                            List<ProprietorLedger> remainingdata2 = new List<ProprietorLedger>();
                                            remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.ProprietorId == Convert.ToInt32(custdata.ProprietorId) && t.Id > remData.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                            int i = 0;
                                            foreach (var item in remainingdata2)
                                            {
                                                decimal decnetpaidamt = 0;
                                                decnetpaidamt = Convert.ToDecimal(item.NetAmount) - Convert.ToDecimal(item.PaidAmount);
                                                item.RemainingAmount = decnetpaidamt + decremamt;
                                                decremamt = decnetpaidamt + decremamt;
                                                remainingpayrepo2.Edit(item);
                                                remainingpayrepo2.Save();
                                                i++;
                                            }
                                        }
                                        else
                                            paydata.NetAmount = 0;
                                        paydata.TransactionYear = CommonMethod.TransactionYear;
                                        paydata.CompId = CommonMethod.CompId;
                                        paydata.Id = remData.Id;
                                        pRepo.Edit(paydata);
                                        pRepo.Save();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            var mid = paymentrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.ReceiptNo);
                                            if (mid == null)
                                                paydata.ReceiptNo = 1;
                                            else
                                                paydata.ReceiptNo = Convert.ToInt32(mid) + 1;
                                            paydata.ProprietorId = Convert.ToInt32(custdata.ProprietorId);
                                            paydata.ProprietorName = Convert.ToString(custdata.ProprietorName);

                                            paydata.Type = "OpeningBalance";
                                            if (txtopeningBal.Text != "")
                                            {
                                                paydata.NetAmount = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                                paydata.RemainingAmount = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                            }
                                            else
                                                paydata.NetAmount = 0;
                                            paydata.CreateDate = dtpOpeningBalanceDate.Value;
                                            paydata.TransactionYear = CommonMethod.TransactionYear;
                                            paydata.CompId = CommonMethod.CompId;
                                            paymentrepo.Add(paydata);
                                            paymentrepo.Save();
                                        }
                                        catch (Exception)
                                        { }
                                    }
                                    pRepo.Dispose();
                                }
                                catch (Exception)
                                { }
                            }
                        }
                        MessageBox.Show("Record Updated Sucessfully.", "Success");
                        clear();
                        btndelete.Enabled = false;
                        txtopeningBal.Enabled = true;
                    a:
                        {
                            clear();
                            btndelete.Enabled = false;
                            txtopeningBal.Enabled = true;
                            this.ActiveControl = cmbProprietorName;
                        }
                    }
                    else
                    {
                        ProprietorMasterRepository custrepo1 = new ProprietorMasterRepository();
                        var cust = custrepo1.GetAll().Where(t => t.ProprietorName.ToLower() == txtProprietorName.Text.Trim().ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (cust != null)
                        {
                            MessageBox.Show("Already Saved This Record.", "Error");
                            txtProprietorName.Text = "";
                            this.ActiveControl = txtProprietorName;
                        }
                        else
                        {
                            ProprietorMasterRepository custrepo = new ProprietorMasterRepository();
                            ProprietorMaster custdata = new ProprietorMaster();
                            maxcustomerid();
                            custdata.ProprietorId = Convert.ToInt32(txtProprietorID.Text);
                            custdata.ProprietorName = txtProprietorName.Text.Trim();
                            custdata.Address = txtProprietorAddress.Text.Trim();
                            custdata.MobileNo = txtmobileno.Text.Trim();
                            custdata.AdharCardNo = txtAdharCardNo.Text;
                            custdata.CompId = CommonMethod.CompId;
                            custdata.Panno = txtPanno.Text;
                            if (txtopeningBal.Text.Trim() != "" && !string.IsNullOrEmpty(txtopeningBal.Text))
                                custdata.OpeningBalanceDate = dtpOpeningBalanceDate.Value;
                            custdata.ACGId = Convert.ToInt32(cmbAccountGroup.SelectedValue);
                            custrepo.Add(custdata);
                            custrepo.Save();
                            if (txtopeningBal.Text.Trim() != "" && !string.IsNullOrEmpty(txtopeningBal.Text))
                            {
                                try
                                {
                                    var mid = paymentrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.ReceiptNo);
                                    if (mid == null)
                                        paydata.ReceiptNo = 1;
                                    else
                                        paydata.ReceiptNo = Convert.ToInt32(mid) + 1;
                                    paydata.ProprietorId = Convert.ToInt32(custdata.ProprietorId);
                                    paydata.ProprietorName = Convert.ToString(custdata.ProprietorName);
                                    paydata.Type = "OpeningBalance";
                                    if (txtopeningBal.Text != "")
                                    {
                                        paydata.NetAmount = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                        paydata.RemainingAmount = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                    }
                                    else
                                        paydata.NetAmount = 0;
                                    paydata.CreateDate = dtpOpeningBalanceDate.Value;
                                    paydata.TransactionYear = CommonMethod.TransactionYear;
                                    paydata.CompId = CommonMethod.CompId;
                                    paymentrepo.Add(paydata);
                                    paymentrepo.Save();
                                }
                                catch (Exception)
                                { }
                            }
                            MessageBox.Show("Record Saved Sucessfully.", "Success");
                            clear();
                            btndelete.Enabled = false;
                        }
                    }
                }
                else
                {
                    if (txtProprietorName.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter Proprietor name.", "Warning");
                        this.ActiveControl = txtProprietorName;
                    }
                    else if (cmbAccountGroup.SelectedIndex < 0)
                    {
                        MessageBox.Show("Select Account Group.", "Warning");
                        this.ActiveControl = cmbAccountGroup;
                    }
                }
                paymentrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            clear();
            this.ActiveControl = txtProprietorName;
            btndelete.Enabled = false;
            txtopeningBal.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    string strcheck = "F";
                    if (Proprietorid != 0)
                    {
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPCheckforIdUsedorNot", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "Proprietor";
                            command.Parameters.Add("@Id", SqlDbType.VarChar).Value = Proprietorid;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@Check", SqlDbType.NVarChar);
                            command.Parameters["@Check"].Direction = ParameterDirection.Output;
                            command.Parameters["@Check"].Size = 5;
                            command.ExecuteReader();
                            strcheck = command.Parameters["@Check"].Value.ToString();
                            db.CloseConnection();
                        }
                        catch (Exception)
                        { db.CloseConnection(); }
                        if (strcheck == "T")
                            MessageBox.Show("Proprietor name is Used. You Can Not Delete.", "Warning");
                        else
                        {
                            MessageBox.Show("Record Deleted Sucessfully.", "Success");
                            clear();
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbcustomername_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ProprietorMasterRepository custrepo = new ProprietorMasterRepository();
                var custdata = custrepo.GetAll().Where(t => t.ProprietorName == cmbProprietorName.Text.Trim() && t.ProprietorId == Convert.ToInt32(cmbProprietorName.SelectedValue) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                GvProprietorInfo.Rows.Clear();
                GvProprietorInfo.Rows.Add();
                GvProprietorInfo.Rows[0].Cells["Id"].Value = custdata.Id;
                GvProprietorInfo.Rows[0].Cells["ProprietorIdg"].Value = custdata.ProprietorId;
                GvProprietorInfo.Rows[0].Cells["ProprietorName"].Value = custdata.ProprietorName;
                GvProprietorInfo.Rows[0].Cells["Address"].Value = custdata.Address;
                GvProprietorInfo.Rows[0].Cells["MobileNo"].Value = custdata.MobileNo;
                GvProprietorInfo.Rows[0].Cells["PANNo"].Value = custdata.Panno;
                GvProprietorInfo.Rows[0].Cells["AdharCardNo"].Value = custdata.AdharCardNo;
                GvProprietorInfo.Rows[0].Cells["ACGroupId"].Value = custdata.ACGId;
                GvProprietorInfo.Rows[0].Cells["OpeningBalDate"].Value = custdata.OpeningBalanceDate;
                custrepo.Dispose();
            }
            catch (Exception)
            { fillcombo(); }
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
                btnSave_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void rButton1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtopeningBal;
            }
            catch (Exception)
            { }
        }

        private void txtopeningBal_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (txtopeningBal.Text != "")
                {
                    if (e.KeyData == Keys.Enter)
                        this.ActiveControl = dtpOpeningBalanceDate;
                }
                else
                {
                    if (e.KeyData == Keys.Enter)
                        this.ActiveControl = txtProprietorAddress;
                }
            }
            catch (Exception)
            { }
        }

        private void txtCustomerAddress_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtmobileno;
            }
            catch (Exception)
            { }
        }

        private void txtmobileno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtPanno;
            }
            catch (Exception)
            { }
        }

        private void txtPanno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtAdharCardNo;
            }
            catch (Exception)
            { }
        }

        private void txtAdharCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSave;
            }
            catch (Exception)
            { }
        }

        private void btnSave_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btndelete;
            }
            catch (Exception)
            { }
        }

        private void btndelete_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnClose;
            }
            catch (Exception)
            { }
        }

        private void btnClose_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnnew;
            }
            catch (Exception)
            { }
        }

        private void btnnew_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbProprietorName;
            }
            catch (Exception)
            { }
        }

        private void cmbcustomername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btngetall;
            }
            catch (Exception)
            { }
        }

        private void btngetall_Click(object sender, EventArgs e)
        {
            try
            {
                fillcombo(); 
                Bindcustomerinfo();                
                this.ActiveControl = txtProprietorName;
                btndelete.Enabled = false;
            }
            catch (Exception)
            { }
        }

        private void txtopeningBal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtopeningBal.Text != "")
                {
                    lblOpeningBalance.Visible = true;
                    dtpOpeningBalanceDate.Visible = true;
                }
                else
                {
                    lblOpeningBalance.Visible = false;
                    dtpOpeningBalanceDate.Visible = false;
                }
            }
            catch (Exception) { }
        }

        private void dtpOpeningBalanceDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtProprietorAddress;
            }
            catch (Exception)
            { }
        }

        private void GvProprietorInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Proprietorid = Convert.ToInt32(GvProprietorInfo.Rows[e.RowIndex].Cells["Id"].Value);
                txtProprietorID.Text = Convert.ToString(GvProprietorInfo.Rows[e.RowIndex].Cells["ProprietorIdg"].Value);
                txtProprietorName.Text = Convert.ToString(GvProprietorInfo.Rows[e.RowIndex].Cells["ProprietorName"].Value);
                txtProprietorAddress.Text = Convert.ToString(GvProprietorInfo.Rows[e.RowIndex].Cells["Address"].Value);
                txtmobileno.Text = Convert.ToString(GvProprietorInfo.Rows[e.RowIndex].Cells["MobileNo"].Value);
                txtPanno.Text = Convert.ToString(GvProprietorInfo.Rows[e.RowIndex].Cells["PANNo"].Value);
                txtAdharCardNo.Text = Convert.ToString(GvProprietorInfo.Rows[e.RowIndex].Cells["AdharCardNo"].Value);
                cmbAccountGroup.SelectedValue = Convert.ToInt32(GvProprietorInfo.Rows[e.RowIndex].Cells["ACGroupId"].Value);
                if (Convert.ToString(GvProprietorInfo.Rows[e.RowIndex].Cells["OpeningBalDate"].Value) == "")
                    dtpOpeningBalanceDate.Value = DateTime.Now.Date;
                else
                    dtpOpeningBalanceDate.Value = Convert.ToDateTime(GvProprietorInfo.Rows[e.RowIndex].Cells["OpeningBalDate"].Value);
                ProprietorLedgerRepository paymentrepo = new ProprietorLedgerRepository();
                try
                {
                    decimal OpeAmt = Convert.ToDecimal(paymentrepo.GetAll().Where(t => t.Type == "OpeningBalance" && t.ProprietorId == Convert.ToInt32(GvProprietorInfo.Rows[e.RowIndex].Cells["ProprietorIdg"].Value) && t.TransactionYear == CommonMethod.TransactionYear && (t.CompId == CommonMethod.CompId)).FirstOrDefault().NetAmount);
                    txtopeningBal.Text = Convert.ToString(OpeAmt);
                }
                catch (Exception)
                {
                    txtopeningBal.Text = "0";
                }
                try
                {
                    ProprietorLedgerRepository remainingpayrepo = new ProprietorLedgerRepository();
                    List<ProprietorLedger> remainingamt = remainingpayrepo.GetAll().Where(t => t.ProprietorId == Convert.ToInt32(GvProprietorInfo.Rows[e.RowIndex].Cells["ProprietorIdg"].Value) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                    decimal netamt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.ProprietorId == Convert.ToInt32(GvProprietorInfo.Rows[e.RowIndex].Cells["ProprietorIdg"].Value) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmount));
                    decimal Paidamt = Convert.ToDecimal(remainingpayrepo.GetAll().Where(t => t.ProprietorId == Convert.ToInt32(GvProprietorInfo.Rows[e.RowIndex].Cells["ProprietorIdg"].Value) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmount));
                    lblAmt.Visible = true;
                    lblRemAmt.Visible = true;
                    if (remainingamt != null)
                        lblAmt.Text = Convert.ToString(netamt - Paidamt);
                    else
                        lblAmt.Text = "0";
                    remainingpayrepo.Dispose();
                }
                catch (Exception)
                { }
                btnSave.Text = "Update";
                btndelete.Enabled = true;
                paymentrepo.Dispose();
            }
            catch (Exception)
            { }
        }
    }
}