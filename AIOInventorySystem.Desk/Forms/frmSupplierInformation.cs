using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;
using Excel = Microsoft.Office.Interop.Excel;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmSupplierInformation : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        public int Supplierid;
        int sreceiptId;

        public frmSupplierInformation()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxproductid();
            fillState();
            btnDelete.Enabled = false;
            fillsearchsuppliercombo();
            SettingRepository settingRepo = new SettingRepository();
            Setting settingdata = new Setting();
            settingdata = settingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (settingdata != null)
            {
                if (settingdata.ShowLicNum == "All")
                {
                    txtSeedLicNo.Enabled = true;
                    txtInscLicNo.Enabled = true;
                    txtFertiLicNo.Enabled = true;
                    label16.Enabled = true;
                    label17.Enabled = true;
                    label18.Enabled = true;
                }
                else
                {
                    txtSeedLicNo.Enabled = false;
                    txtInscLicNo.Enabled = false;
                    txtFertiLicNo.Enabled = false;
                    label16.Enabled = false;
                    label17.Enabled = false;
                    label18.Enabled = false;
                }
            }
            this.ActiveControl = txtSupplierName;
        }

        public void fillState()
        {
            try
            {
                StateMasterRepository unitrepo = new StateMasterRepository();
                List<StateMaster> unitdata = new List<StateMaster>();
                unitdata.Clear();
                unitdata = unitrepo.GetAll().OrderByDescending(t => t.StateName).ToList();
                unitdata.Add(new StateMaster { StateName = "Select", Id = 0 });
                unitdata.Reverse();
                cmbState.DataSource = unitdata;
                cmbState.ValueMember = "Id";
                cmbState.DisplayMember = "StateName";
                unitrepo.Dispose();
                CompanyInformationRepository cRepo = new CompanyInformationRepository();
                var cinfo = cRepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();
                if (cinfo != null)
                {
                    StateMaster state = new StateMaster();
                    StateMasterRepository staterepo = new StateMasterRepository();
                    state = staterepo.GetAll().Where(t => t.StateId == cinfo.StateCode).FirstOrDefault();
                    cmbState.SelectedValue = state.Id;
                }
                cRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SupplierRemainingPaymentRepository suppaymentRepo = new SupplierRemainingPaymentRepository();
                SupplierRemainingPayment suppaydata = new SupplierRemainingPayment();
                if (btnSave.Text == "Update")
                {
                    if (txtSupplierName.Text.Trim() != "")
                    {
                        SupplierRepository supplierRepo1 = new SupplierRepository();
                        SupplierInformation supp = new SupplierInformation();
                        if (CommonMethod.commProduct == true)
                            supp = supplierRepo1.GetAll().Where(t => t.SupplierName.ToLower() == txtSupplierName.Text.Trim().ToLower() && t.SupplierCode != Convert.ToInt32(txtSupplierID.Text)).FirstOrDefault();
                        else
                            supp = supplierRepo1.GetAll().Where(t => t.SupplierName.ToLower() == txtSupplierName.Text.Trim().ToLower() && t.SupplierCode != Convert.ToInt32(txtSupplierID.Text) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (supp != null)
                        {
                            MessageBox.Show("Already Having This Name.", "Error");
                            txtSupplierName.Text = "";
                            this.ActiveControl = txtSupplierName;
                        }
                        else
                        {
                            SupplierRepository sRepo = new SupplierRepository();
                            string olSupplierName = sRepo.GetById(Supplierid).SupplierName;
                            SupplierRepository supplierRepo = new SupplierRepository();
                            SupplierInformation sinfo = new SupplierInformation();
                            sinfo.SupplierCode = Convert.ToInt32(txtSupplierID.Text);
                            sinfo.SupplierName = txtSupplierName.Text.Trim();
                            sinfo.SupplierAddress = txtSupplierAddress.Text.Trim();
                            sinfo.ContactNo = txtContactNo.Text.Trim();
                            sinfo.EmailId = txtEmailID.Text.Trim();
                            sinfo.VatNo = txtVatNo.Text.Trim();
                            sinfo.TinNo = txtTinNo.Text.Trim();
                            sinfo.Panno = txtPanno.Text.Trim();
                            sinfo.LandlineNo = txtlandlineno.Text.Trim();
                            sinfo.StateId = Convert.ToInt32(cmbState.SelectedValue);
                            sinfo.CreditDays = Convert.ToInt32(txtCreditDays.Text);
                            sinfo.SeedLicNo = txtSeedLicNo.Text.Trim();
                            sinfo.InscLicNo = txtInscLicNo.Text.Trim();
                            sinfo.FertiLicNo = txtFertiLicNo.Text.Trim();
                            sinfo.BankName = txtBankName.Text.Trim();
                            sinfo.BankACNo = txtBankAcNo.Text.Trim();
                            sinfo.IFSCCode = txtIFSCCode.Text.Trim();
                            if (txtopeningBal.Text != "" && txtopeningBal.Enabled == true && !string.IsNullOrEmpty(txtopeningBal.Text))
                            {
                                sinfo.OpeningBalanceDate = dtpopeningbalanceDate.Value;
                            }
                            sinfo.Id = Supplierid;
                            sinfo.CompId = sRepo.GetById(Supplierid).CompId;
                            supplierRepo.Edit(sinfo);
                            supplierRepo.Save();

                            if (olSupplierName != sinfo.SupplierName)
                            {
                                try
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPUpdateSupplier", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.Add("@SupplierID", SqlDbType.Int).Value = sinfo.SupplierCode;
                                    command.Parameters.Add("@SupplierName", SqlDbType.VarChar).Value = sinfo.SupplierName;
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                    command.ExecuteNonQuery();
                                    db.CloseConnection();
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                            }

                            if (txtopeningBal.Text != "0" && txtopeningBal.Enabled == true && !string.IsNullOrEmpty(txtopeningBal.Text) && txtopeningBal.Text != "0.00")
                            {
                                try
                                {
                                    SupplierRemainingPaymentRepository spRepo = new SupplierRemainingPaymentRepository();
                                    var remData = suppaymentRepo.GetAll().Where(t => t.Type == "OpeningBalance" && t.SupplierId == sinfo.SupplierCode && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                    if (remData != null)
                                    {
                                        //Save Opening balance with calculating total balance
                                        suppaydata.SupplierId = Convert.ToInt32(sinfo.SupplierCode);
                                        suppaydata.Suppliername = Convert.ToString(sinfo.SupplierName);
                                        suppaydata.BillReceiptNo = remData.BillReceiptNo;
                                        suppaydata.Type = "OpeningBalance";
                                        suppaydata.CreateDate = dtpopeningbalanceDate.Value;
                                        if (txtopeningBal.Text != "")
                                        {
                                            suppaydata.NetAmt = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                            suppaydata.RemainingAmt = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                            decimal decremamt = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                            SupplierRemainingPaymentRepository remainingpayrepo2 = new SupplierRemainingPaymentRepository();
                                            List<SupplierRemainingPayment> remainingdata2 = new List<SupplierRemainingPayment>();
                                            remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.SupplierId == Convert.ToInt32(sinfo.SupplierCode) && t.Id > remData.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                            int i = 0;
                                            foreach (var item in remainingdata2)
                                            {
                                                item.RemainingAmt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                                                decremamt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                                                item.CompId = CommonMethod.CompId;
                                                remainingpayrepo2.Edit(item);
                                                remainingpayrepo2.Save();
                                                i++;
                                            }
                                        }
                                        else
                                            suppaydata.NetAmt = 0;
                                        suppaydata.TransactionYear = CommonMethod.TransactionYear;
                                        suppaydata.CompId = CommonMethod.CompId;
                                        suppaydata.Id = remData.Id;
                                        spRepo.Edit(suppaydata);
                                        spRepo.Save();
                                    }
                                    else
                                    {
                                        try
                                        {
                                            int Billrecieptno = 0;
                                            var mid = suppaymentRepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.BillReceiptNo);
                                            if (mid == null)
                                                Billrecieptno = 1;
                                            else
                                                Billrecieptno = Convert.ToInt32(mid) + 1;
                                            suppaydata.SupplierId = Convert.ToInt32(sinfo.SupplierCode);
                                            suppaydata.Suppliername = Convert.ToString(sinfo.SupplierName);
                                            suppaydata.BillReceiptNo = Billrecieptno;
                                            suppaydata.Type = "OpeningBalance";
                                            if (txtopeningBal.Text != "")
                                            {
                                                suppaydata.NetAmt = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                                suppaydata.RemainingAmt = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                            }
                                            else
                                                suppaydata.NetAmt = 0;
                                            suppaydata.CreateDate = dtpopeningbalanceDate.Value;
                                            suppaydata.TransactionYear = CommonMethod.TransactionYear;
                                            suppaydata.CompId = CommonMethod.CompId;
                                            suppaymentRepo.Add(suppaydata);
                                            suppaymentRepo.Save();
                                        }
                                        catch (Exception)
                                        { }
                                    }
                                    spRepo.Dispose();
                                }
                                catch (Exception)
                                { }
                            }
                            if (txtopeningBal.Text == "0" || txtopeningBal.Text == "0.00")
                            {
                                SupplierRemainingPaymentRepository remRepo = new SupplierRemainingPaymentRepository();
                                SupplierRemainingPayment remdata = remRepo.GetAll().Where(t => t.Type == "OpeningBalance" && t.SupplierId == sinfo.SupplierCode && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                if (remdata != null)
                                {
                                    sreceiptId = remdata.Id;
                                    SupplierRemainingPaymentRepository remRepo1 = new SupplierRemainingPaymentRepository();
                                    SupplierRemainingPayment remdata1 = remRepo.GetAll().Where(t => t.Id == sreceiptId && t.Type == "OpeningBalance" && t.SupplierId == sinfo.SupplierCode && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    remRepo.Remove(remdata1);
                                    remRepo.Save();

                                    if (remdata1 != null)
                                    {
                                        SupplierRemainingPaymentRepository remRepo2 = new SupplierRemainingPaymentRepository();
                                        SupplierRemainingPayment remdata2 = remRepo2.GetAll().Where(t => t.SupplierId == Convert.ToInt32(remdata1.SupplierId) && t.Id < sreceiptId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                        if (remdata2 != null)
                                        {
                                            decimal decremamt = Convert.ToDecimal(remdata2.RemainingAmt);
                                            SupplierRemainingPaymentRepository remainingpayrepo2 = new SupplierRemainingPaymentRepository();
                                            List<SupplierRemainingPayment> remainingdata2 = new List<SupplierRemainingPayment>();
                                            remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.SupplierId == Convert.ToInt32(remdata.SupplierId) && t.Id > sreceiptId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                            int i = 0;
                                            foreach (var item in remainingdata2)
                                            {
                                                item.RemainingAmt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                                                decremamt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                                                item.CompId = CommonMethod.CompId;
                                                remainingpayrepo2.Edit(item);
                                                remainingpayrepo2.Save();
                                                i++;
                                            }
                                        }
                                    }
                                }
                            }
                            MessageBox.Show("Supplier Information Updated.", "Success");
                            clear();
                            this.ActiveControl = txtSupplierName;
                            sRepo.Dispose();
                            supplierRepo.Dispose();
                        }
                    }
                    else
                        MessageBox.Show("Enter Supplier Name.", "Warning");
                }
                else
                {
                    if (txtSupplierName.Text.Trim() != "")
                    {
                        SupplierRepository supplierRepo1 = new SupplierRepository();
                        SupplierInformation supp = new SupplierInformation();
                        if (CommonMethod.commProduct == true)
                            supp = supplierRepo1.GetAll().Where(t => t.SupplierName.ToLower() == txtSupplierName.Text.Trim().ToLower()).FirstOrDefault();
                        else
                            supp = supplierRepo1.GetAll().Where(t => t.SupplierName.ToLower() == txtSupplierName.Text.Trim().ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (supp != null)
                        {
                            MessageBox.Show("Already Saved This Record.", "Error");
                            txtSupplierName.Text = "";
                            this.ActiveControl = txtSupplierName;
                        }
                        else
                        {
                            SupplierRepository supplierRepo = new SupplierRepository();
                            SupplierInformation sinfo = new SupplierInformation();
                            maxproductid();
                            sinfo.SupplierCode = Convert.ToInt32(txtSupplierID.Text);
                            sinfo.SupplierName = txtSupplierName.Text.Trim();
                            sinfo.SupplierAddress = txtSupplierAddress.Text.Trim();
                            sinfo.ContactNo = txtContactNo.Text.Trim();
                            sinfo.EmailId = txtEmailID.Text.Trim();
                            sinfo.VatNo = txtVatNo.Text.Trim();
                            sinfo.TinNo = txtTinNo.Text.Trim();
                            sinfo.LandlineNo = txtlandlineno.Text.Trim();
                            sinfo.StateId = Convert.ToInt32(cmbState.SelectedValue);
                            sinfo.CreditDays = Convert.ToInt32(txtCreditDays.Text);
                            sinfo.SeedLicNo = txtSeedLicNo.Text.Trim();
                            sinfo.InscLicNo = txtInscLicNo.Text.Trim();
                            sinfo.FertiLicNo = txtFertiLicNo.Text.Trim();
                            sinfo.BankName = txtBankName.Text.Trim();
                            sinfo.BankACNo = txtBankAcNo.Text.Trim();
                            sinfo.IFSCCode = txtIFSCCode.Text.Trim();
                            if (txtopeningBal.Text != "" && txtopeningBal.Enabled == true && !string.IsNullOrEmpty(txtopeningBal.Text))
                                sinfo.OpeningBalanceDate = dtpopeningbalanceDate.Value;
                            sinfo.CompId = CommonMethod.CompId;
                            supplierRepo.Add(sinfo);
                            supplierRepo.Save();

                            if (txtopeningBal.Text != "0" && !string.IsNullOrEmpty(txtopeningBal.Text) && txtopeningBal.Text != "0.00")
                            {
                                try
                                {
                                    int Billrecieptno = 0;
                                    var mid = suppaymentRepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.BillReceiptNo);
                                    if (mid == null)
                                        Billrecieptno = 1;
                                    else
                                        Billrecieptno = Convert.ToInt32(mid) + 1;
                                    suppaydata.SupplierId = Convert.ToInt32(sinfo.SupplierCode);
                                    suppaydata.Suppliername = Convert.ToString(sinfo.SupplierName);
                                    suppaydata.BillReceiptNo = Billrecieptno;
                                    suppaydata.Type = "OpeningBalance";
                                    if (txtopeningBal.Text != "")
                                    {
                                        suppaydata.NetAmt = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                        suppaydata.RemainingAmt = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                    }
                                    else
                                        suppaydata.NetAmt = 0;
                                    suppaydata.CreateDate = dtpopeningbalanceDate.Value;
                                    suppaydata.TransactionYear = CommonMethod.TransactionYear;
                                    suppaydata.CompId = CommonMethod.CompId;
                                    suppaymentRepo.Add(suppaydata);
                                    suppaymentRepo.Save();
                                }
                                catch (Exception)
                                { }
                            }
                            MessageBox.Show("Supplier Information Saved.", "Success");
                            clear();
                            this.ActiveControl = txtSupplierName;
                            supplierRepo.Dispose();
                        }
                        supplierRepo1.Dispose();
                    }
                    else
                        MessageBox.Show("Enter Supplier Name.", "Warning");
                }
                suppaymentRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void maxproductid()
        {
            try
            {
                SupplierRepository supplierRepo = new SupplierRepository();
                var mid = 0;
                if (CommonMethod.commProduct == true)
                    mid = Convert.ToInt32(supplierRepo.GetAll().Max(t => t.SupplierCode));
                else
                    mid = Convert.ToInt32(supplierRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.SupplierCode));
                if (mid == null)
                    txtSupplierID.Text = "1";
                else
                    txtSupplierID.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                supplierRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                fillState();
                txtSupplierName.Text = "";
                txtSupplierAddress.Text = "";
                txtContactNo.Text = "";
                txtEmailID.Text = "";
                txtVatNo.Text = "";
                txtTinNo.Text = "";
                maxproductid();
                txtlandlineno.Text = "";
                btnSave.Text = "Save";
                txtopeningBal.Text = "";
                txtPanno.Text = "";
                txtCreditDays.Text = "0";
                txtSeedLicNo.Text = "";
                txtInscLicNo.Text = "";
                txtFertiLicNo.Text = "";
                txtIFSCCode.Text = "";
                txtBankName.Text = "";
                txtBankAcNo.Text = "";
                dtgvList.Rows.Clear();
                fillsearchsuppliercombo();
                btnSave.Enabled = true;
                btnDelete.Enabled = false;
                this.ActiveControl = btnnew;
                txtopeningBal.Enabled = true;
            }
            catch (Exception)
            { }
        }

        public void Bindsupplierinfo()
        {
            try
            {
                SupplierRepository supplierRepo = new SupplierRepository();
                dtgvList.Rows.Clear();
                List<SupplierInformation> ItemList = new List<SupplierInformation>();
                if (CommonMethod.commProduct == true)
                    ItemList = supplierRepo.GetAll();
                else
                    ItemList = supplierRepo.GetAll().Where(t => t.CompId == Convert.ToInt32(CommonMethod.CompId)).ToList();
                lblTotaluSuppliers.Text = Convert.ToString(ItemList.Count);
                if (ItemList.Count != 0)
                {
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        dtgvList.Rows.Add();
                        dtgvList.Rows[i].Cells["SupplierName"].Value = item.SupplierName;
                        dtgvList.Rows[i].Cells["Address"].Value = item.SupplierAddress;
                        dtgvList.Rows[i].Cells["ContactNo"].Value = item.ContactNo;
                        dtgvList.Rows[i].Cells["EmailId"].Value = item.EmailId;
                        dtgvList.Rows[i].Cells["SupplierIdg"].Value = item.SupplierCode;
                        dtgvList.Rows[i].Cells["Id"].Value = item.Id;
                        dtgvList.Rows[i].Cells["GSTIN"].Value = item.VatNo;
                        dtgvList.Rows[i].Cells["TinNo"].Value = item.TinNo;
                        dtgvList.Rows[i].Cells["PANNo"].Value = item.Panno;
                        dtgvList.Rows[i].Cells["LandlineNo"].Value = item.LandlineNo;
                        try
                        {
                            dtgvList.Rows[i].Cells["StateId"].Value = item.StateId;
                            dtgvList.Rows[i].Cells["State"].Value = item.StateMaster.StateName;
                        }
                        catch (Exception)
                        { }
                        dtgvList.Rows[i].Cells["CreditDays"].Value = item.CreditDays;
                        dtgvList.Rows[i].Cells["SeedLicNo"].Value = item.SeedLicNo;
                        dtgvList.Rows[i].Cells["InscLicNo"].Value = item.InscLicNo;
                        dtgvList.Rows[i].Cells["FertiLicNo"].Value = item.FertiLicNo;
                        dtgvList.Rows[i].Cells["OpeningBalDate"].Value = item.OpeningBalanceDate;
                        dtgvList.Rows[i].Cells["BankName"].Value = item.BankName;
                        dtgvList.Rows[i].Cells["BankACNo"].Value = item.BankACNo;
                        dtgvList.Rows[i].Cells["IFSCCode"].Value = item.IFSCCode;
                        i++;
                    }
                }
                supplierRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillsearchsuppliercombo()
        {
            try
            {
                SupplierRepository supplierRepo = new SupplierRepository();
                List<SupplierInformation> customerdata = new List<SupplierInformation>();
                customerdata.Clear();
                if (CommonMethod.commProduct == true)
                    customerdata = supplierRepo.GetAll().OrderByDescending(t => t.SupplierName).ToList();
                else
                    customerdata = supplierRepo.GetAll().Where(t => t.CompId == Convert.ToInt32(CommonMethod.CompId)).OrderByDescending(t => t.SupplierName).ToList();
                customerdata.Add(new SupplierInformation { SupplierName = "Select", SupplierCode = 0 });
                customerdata.Reverse();
                cmbsuppliername.DataSource = customerdata;
                cmbsuppliername.ValueMember = "SupplierCode";
                cmbsuppliername.DisplayMember = "SupplierName";
                supplierRepo.Dispose();
                cmbsuppliername.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                Bindsupplierinfo();
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

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbsuppliername.Text != "Select")
                {
                    RptSupplierinfo srpt = new RptSupplierinfo(cmbsuppliername.Text);
                    srpt.ShowDialog();
                    srpt.Dispose();
                }
                else
                {
                    RptSupplierinfo srpt = new RptSupplierinfo();
                    srpt.ShowDialog();
                    srpt.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    string strcheck = "F";
                    if (Supplierid != 0)
                    {
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPCheckforIdUsedorNot", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "Supplier";
                            command.Parameters.Add("@Id", SqlDbType.VarChar).Value = Supplierid;
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
                            MessageBox.Show("Supplier name is Used. You Can Not Delete.", "Warning");
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

        private void cmbsuppliername_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SupplierRepository supplierRepo = new SupplierRepository();
                SupplierInformation suppliername = new SupplierInformation();
                if (CommonMethod.commProduct == true)
                    suppliername = supplierRepo.GetAll().Where(t => t.SupplierName == cmbsuppliername.Text && t.SupplierCode == Convert.ToInt32(cmbsuppliername.SelectedValue)).FirstOrDefault();
                else
                    suppliername = supplierRepo.GetAll().Where(t => t.SupplierName == cmbsuppliername.Text && t.SupplierCode == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                var supplierid = suppliername.Id;
                dtgvList.Rows.Clear();
                dtgvList.Rows.Add();
                dtgvList.Rows[0].Cells["SupplierName"].Value = suppliername.SupplierName;
                dtgvList.Rows[0].Cells["Address"].Value = suppliername.SupplierAddress;
                dtgvList.Rows[0].Cells["ContactNo"].Value = suppliername.ContactNo;
                dtgvList.Rows[0].Cells["EmailId"].Value = suppliername.EmailId;
                dtgvList.Rows[0].Cells["SupplierIdg"].Value = suppliername.SupplierCode;
                dtgvList.Rows[0].Cells["Id"].Value = suppliername.Id;
                dtgvList.Rows[0].Cells["GSTIN"].Value = suppliername.VatNo;
                dtgvList.Rows[0].Cells["TinNo"].Value = suppliername.TinNo;
                dtgvList.Rows[0].Cells["PANNo"].Value = suppliername.Panno;
                dtgvList.Rows[0].Cells["LandlineNo"].Value = suppliername.LandlineNo;
                dtgvList.Rows[0].Cells["StateId"].Value = suppliername.StateId;
                dtgvList.Rows[0].Cells["State"].Value = suppliername.StateMaster.StateName;
                dtgvList.Rows[0].Cells["CreditDays"].Value = suppliername.CreditDays;
                dtgvList.Rows[0].Cells["FertiLicNo"].Value = suppliername.FertiLicNo;
                dtgvList.Rows[0].Cells["InscLicNo"].Value = suppliername.InscLicNo;
                dtgvList.Rows[0].Cells["SeedLicNo"].Value = suppliername.SeedLicNo;
                dtgvList.Rows[0].Cells["OpeningBalDate"].Value = suppliername.OpeningBalanceDate;
                dtgvList.Rows[0].Cells["BankName"].Value = suppliername.BankName;
                dtgvList.Rows[0].Cells["BankACNo"].Value = suppliername.BankACNo;
                dtgvList.Rows[0].Cells["IFSCCode"].Value = suppliername.IFSCCode;
                supplierRepo.Dispose();
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
                btnSave_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtSupplierName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtopeningBal;
            }
            catch (Exception)
            { }
        }

        private void txtEmailID_Leave(object sender, EventArgs e)
        {
            try
            {
                Regex mRegxExpression;
                if (txtEmailID.Text.Trim() != string.Empty)
                {
                    mRegxExpression = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");
                    if (!mRegxExpression.IsMatch(txtEmailID.Text.Trim()))
                    {
                        MessageBox.Show("Please enter valid Email-ID.");
                        this.ActiveControl = txtEmailID;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void frmSupplierInformation_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            try
            {
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd;
                    if (CommonMethod.commProduct == true)
                        cmd = new SqlCommand("SELECT SupplierName FROM SupplierInformation", con);
                    else
                        cmd = new SqlCommand("SELECT SupplierName FROM SupplierInformation where compid=" + CommonMethod.CompId, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        {
                            MyCollection.Add(reader.GetString(0));
                        }
                        catch (Exception)
                        { }
                    }
                    txtSupplierName.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void cmbsuppliername_Leave(object sender, EventArgs e)
        {
            if (cmbsuppliername.Text != "" && cmbsuppliername.Text != "Select")
            {
                CommonMethod cm = new CommonMethod();
                if (cm.ValidSupplier(cmbsuppliername.Text) == 1)
                { }
                else
                {
                    MessageBox.Show("Invalid Supplier.", "Warning");
                    fillsearchsuppliercombo();
                    this.ActiveControl = cmbsuppliername;
                }
            }
        }

        private void txtopeningBal_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dtpopeningbalanceDate.Visible == true)
                {
                    if (e.KeyData == Keys.Enter)
                        this.ActiveControl = dtpopeningbalanceDate;
                }
                else
                {
                    if (e.KeyData == Keys.Enter)
                        this.ActiveControl = txtSupplierAddress;
                }
            }
            catch (Exception)
            { }
        }

        private void txtSupplierAddress_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbState;
            }
            catch (Exception)
            { }
        }

        private void cmbState_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtCreditDays;
            }
            catch (Exception)
            { }
        }

        private void txtContactNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtlandlineno;
            }
            catch (Exception)
            { }
        }

        private void txtlandlineno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtEmailID;
            }
            catch (Exception)
            { }
        }

        private void txtEmailID_KeyDown(object sender, KeyEventArgs e)
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
                    this.ActiveControl = txtVatNo;
            }
            catch (Exception)
            { }
        }

        private void txtVatNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtSeedLicNo;
            }
            catch (Exception)
            { }
        }

        private void txtTinNo_KeyDown(object sender, KeyEventArgs e)
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
                    this.ActiveControl = btnDelete;
            }
            catch (Exception)
            { }
        }

        private void btnDelete_KeyDown(object sender, KeyEventArgs e)
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
                    this.ActiveControl = cmbsuppliername;
            }
            catch (Exception)
            { }
        }

        private void cmbsuppliername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception)
            { }
        }

        private void btnSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnprint;
            }
            catch (Exception)
            { }
        }

        private void btnprint_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtgvList;
            }
            catch (Exception)
            { }
        }

        private void txtCreditDays_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtCreditDays.Text == "")
                {
                    txtCreditDays.Text = "0";
                    this.ActiveControl = txtCreditDays;
                }
            }
            catch (Exception)
            { }
        }

        private void txtSeedLicNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtInscLicNo;
            }
            catch (Exception)
            { }
        }

        private void txtInscLicNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtFertiLicNo;
            }
            catch (Exception)
            { }
        }

        private void txtFertiLicNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtBankName;
            }
            catch (Exception)
            { }
        }

        private void txtopeningBal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtopeningBal.Text != "0" && txtopeningBal.Text != "0.00")
                {
                    dtpopeningbalanceDate.Visible = true;
                    label19.Visible = true;
                }
                else
                {
                    dtpopeningbalanceDate.Visible = false;
                    label19.Visible = false;
                }
            }
            catch (Exception) { }
        }

        private void txtCreditDays_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtContactNo;
            }
            catch (Exception)
            { }
        }

        private void txtBankName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtBankAcNo;
            }
            catch (Exception)
            { }
        }

        private void txtBankAcNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtIFSCCode;
            }
            catch (Exception)
            { }
        }

        private void txtIFSCCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSave;
            }
            catch (Exception)
            { }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel;
                Microsoft.Office.Interop.Excel.Workbook excelworkBook;
                Microsoft.Office.Interop.Excel.Worksheet excelSheet;

                object misValue = System.Reflection.Missing.Value;
                // Start Excel and get Application object.
                excel = new Microsoft.Office.Interop.Excel.Application();

                // for making Excel visible
                excel.Visible = false;
                excel.DisplayAlerts = false;

                // Creation a new Workbook
                excelworkBook = excel.Workbooks.Add(Type.Missing);

                // Workk sheet
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                // excelSheet.Name = "Test work sheet";

                excelSheet.Application.ActiveWindow.ScrollRow = 1;
                excelSheet.Application.ActiveWindow.SplitRow = 1;
                excelSheet.Application.ActiveWindow.FreezePanes = true;

                string data = null;
                DataSet ds = new DataSet();
                SqlDataAdapter adp = null;
                adp = new SqlDataAdapter("Select SupplierName,SupplierAddress,ContactNo,VatNo from SupplierInformation where CompId='" + CommonMethod.CompId + "'", db.Connection);
                adp.Fill(ds);

                progressBar1.Minimum = 0;
                progressBar1.Maximum = ds.Tables[0].Rows.Count;
                progressBar1.Visible = true;
                int m = 2;
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    System.Windows.Forms.Application.DoEvents();
                    progressBar1.Value = i;
                    for (int j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        data = ds.Tables[0].Rows[i].ItemArray[j].ToString();
                        excelSheet.Cells[m, j + 1] = data;
                    }
                    m++;
                }

                excelSheet.get_Range("A1", "AH1").Font.Bold = true;
                excelSheet.get_Range("A1", "AH1").Font.Color = System.Drawing.Color.Red;
                excelSheet.Cells[1, 1] = "Supplier Name";//"GROUP_NAME";
                excelSheet.Cells[1, 2] = "Supplier Address";
                excelSheet.Cells[1, 3] = "Contact No";
                excelSheet.Cells[1, 4] = "GSTIn No";//"MARATHI";
                excelSheet.Columns.AutoFit();
                excelworkBook.SaveAs("AddSupplierInfo.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                excelworkBook.Close(true, misValue, misValue);
                excel.Quit();
                releaseObject(excelSheet);
                releaseObject(excelworkBook);
                releaseObject(excel);
                MessageBox.Show("Excel file created , you can find the file C:\\Users\\Documents\\AddSupplierInfo.xls");
                progressBar1.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog file = new OpenFileDialog(); //open dialog to choose file  
                if (file.ShowDialog() == System.Windows.Forms.DialogResult.OK) //if there is a file choosen by the user  
                {
                    if (Path.GetExtension(file.FileName).CompareTo(".xls") == 0 || Path.GetExtension(file.FileName).CompareTo(".xlsx") == 0)
                    {
                        try
                        {
                            DataTable dtExcel = new DataTable();
                            dtExcel = ReadExcel(file.FileName, Path.GetExtension(file.FileName)); //read excel file  
                            maxproductid();
                            MessageBox.Show("Import Excel file sucessfully.", "Success");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                    else
                        MessageBox.Show("Please choose .xls or .xlsx file only.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); //custom messageBox to show error  
                }
            }
            catch (Exception)
            { }
        }

        public DataTable ReadExcel(string fileName, string fileExt)
        {
            string conn = string.Empty;
            DataTable dtexcel = new DataTable();
            if (fileExt.CompareTo(".xls") == 0)
                conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            else
                conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties='Excel 12.0;HDR=NO';"; //for above excel 2007  

            using (OleDbConnection con = new OleDbConnection(conn))
            {
                con.Open();
                var sheets = con.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                try
                {
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("SELECT * FROM [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]", con);
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  F10,F11,F14,F17,F18,F19,F20,F21 
                    int i;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = dtexcel.Rows.Count;
                    progressBar1.Visible = true;
                    for (i = 0; i < dtexcel.Rows.Count; i++)
                    {
                        try
                        {
                            string transactionyear = CommonMethod.TransactionYear;
                            string[] year = transactionyear.Split('-');
                            string startYear = year[0];
                            DateTime gstInDate = CommonMethod.startdate;
                            System.Windows.Forms.Application.DoEvents();
                            progressBar1.Value = i;
                            string a = Convert.ToString(dtexcel.Rows[i][0].ToString());
                            if (!string.IsNullOrEmpty(Convert.ToString(dtexcel.Rows[i][0].ToString())))
                            {
                                SupplierRepository custRepo = new SupplierRepository();
                                SupplierInformation custData = new SupplierInformation();
                                SupplierInformation custData1 = new SupplierInformation();
                                custData1 = custRepo.GetAll().Where(t => t.SupplierName.Trim().ToLower() == a.Trim().ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (custData1 != null)
                                {
                                    custData1.SupplierName = a.Trim();
                                    custData1.SupplierAddress = Convert.ToString(dtexcel.Rows[i][1].ToString());
                                    custData1.ContactNo = Convert.ToString(dtexcel.Rows[i][2].ToString());
                                    if (dtexcel.Rows[i][3].ToString() != null || Convert.ToString(dtexcel.Rows[i][3].ToString()) != "")
                                        custData1.VatNo = Convert.ToString(dtexcel.Rows[i][3].ToString());
                                    CompanyInformationRepository cRepo = new CompanyInformationRepository();
                                    var cinfo = cRepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();
                                    if (cinfo != null)
                                    {
                                        StateMaster stateData = new StateMaster();
                                        StateMasterRepository stateRepo = new StateMasterRepository();
                                        stateData = stateRepo.GetAll().Where(t => t.StateName == cinfo.State && t.StateId == cinfo.StateCode).FirstOrDefault();
                                        if (stateData != null)
                                            custData1.StateId = stateData.Id;
                                        stateRepo.Dispose();
                                    }
                                    custData1.CompId = CommonMethod.CompId;
                                    custRepo.Edit(custData1);
                                    custRepo.Save();
                                }
                                else
                                {
                                    maxproductid();
                                    custData.SupplierCode = Convert.ToInt32(txtSupplierID.Text);
                                    custData.SupplierName = a.Trim();
                                    custData.SupplierAddress = Convert.ToString(dtexcel.Rows[i][1].ToString());
                                    custData.ContactNo = Convert.ToString(dtexcel.Rows[i][2].ToString());

                                    if (dtexcel.Rows[i][3].ToString() != null || Convert.ToString(dtexcel.Rows[i][3].ToString()) != "")
                                        custData.VatNo = Convert.ToString(dtexcel.Rows[i][3].ToString());
                                    CompanyInformationRepository cRepo = new CompanyInformationRepository();
                                    var cinfo = cRepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();
                                    if (cinfo != null)
                                    {
                                        StateMaster stateData = new StateMaster();
                                        StateMasterRepository stateRepo = new StateMasterRepository();
                                        stateData = stateRepo.GetAll().Where(t => t.StateName == cinfo.State && t.StateId == cinfo.StateCode).FirstOrDefault();
                                        if (stateData != null)
                                            custData.StateId = stateData.Id;
                                        stateRepo.Dispose();
                                    }
                                    custData.CompId = CommonMethod.CompId;
                                    custRepo.Add(custData);
                                    custRepo.Save();
                                }
                            }
                        }
                        catch (Exception)
                        { }
                    }
                    progressBar1.Visible = false;
                }
                catch (Exception)
                { }
            }
            return dtexcel;
        }

        private void dtgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Supplierid = Convert.ToInt32(dtgvList.Rows[e.RowIndex].Cells["Id"].Value);
                txtSupplierID.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["SupplierIdg"].Value);
                txtSupplierName.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["SupplierName"].Value);
                txtSupplierAddress.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["Address"].Value);
                txtContactNo.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["ContactNo"].Value);
                txtEmailID.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["EmailId"].Value);
                txtVatNo.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["GSTIN"].Value);
                txtTinNo.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["TinNo"].Value);
                txtPanno.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["PANNo"].Value);
                txtlandlineno.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["LandlineNo"].Value);
                cmbState.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["State"].Value);
                txtBankName.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["BankName"].Value);
                txtBankAcNo.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["BankACNo"].Value);
                txtIFSCCode.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["IFSCCode"].Value);
                if (!string.IsNullOrEmpty(Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["CreditDays"].Value)))
                    txtCreditDays.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["CreditDays"].Value);
                else
                    txtCreditDays.Text = "0";
                SupplierRemainingPaymentRepository SupRemRepo = new SupplierRemainingPaymentRepository();
                try
                {
                    SupplierRemainingPayment SuppData = new SupplierRemainingPayment();
                    SuppData = SupRemRepo.GetAll().Where(t => t.Type == "OpeningBalance" && t.SupplierId == Convert.ToInt32(dtgvList.Rows[e.RowIndex].Cells["SupplierIdg"].Value) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    decimal OpeAmt = Convert.ToDecimal(SuppData.NetAmt);
                    txtopeningBal.Text = Convert.ToString(OpeAmt);
                    if (OpeAmt != 0)
                        dtpopeningbalanceDate.Value = Convert.ToDateTime(SuppData.CreateDate);
                    else
                    {
                        if (Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["OpeningBalDate"].Value) == "")
                            dtpopeningbalanceDate.Value = DateTime.Now.Date;
                        else
                            dtpopeningbalanceDate.Value = Convert.ToDateTime(Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["OpeningBalDate"].Value));
                    }
                }
                catch (Exception)
                {
                    txtopeningBal.Text = "0";
                    try
                    {
                        if (Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["OpeningBalDate"].Value) == "")
                            dtpopeningbalanceDate.Value = DateTime.Now.Date;
                        else
                            dtpopeningbalanceDate.Value = Convert.ToDateTime(Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["OpeningBalDate"].Value));
                    }
                    catch (Exception)
                    { dtpopeningbalanceDate.Value = DateTime.Now.Date; }
                }
                txtSeedLicNo.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["SeedLicNo"].Value);
                txtInscLicNo.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["InscLicNo"].Value);
                txtFertiLicNo.Text = Convert.ToString(dtgvList.Rows[e.RowIndex].Cells["FertiLicNo"].Value);
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
                SupRemRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void dtgvList_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtSupplierName;
            }
            catch (Exception)
            { }
        }
    }
}