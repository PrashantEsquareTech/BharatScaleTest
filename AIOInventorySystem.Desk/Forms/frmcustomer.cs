using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;
using Excel = Microsoft.Office.Interop.Excel;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmcustomer : Form
    {
        DbClass db = new DbClass();
        public int Customerid, receiptId;
        public decimal bonusInRs = 0, bonusInPer = 0;
        SettingRepository settRepo = new SettingRepository();
        Setting settData = new Setting();
        CommonMethod cm = new CommonMethod();

        public frmcustomer()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            settData = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (settData != null)
            {
                if (settData.Applyonmarathifont == "Y")
                {
                    txtAdd.Font = new Font("Shivaji01", 12);
                    txtName.Font = new Font("Shivaji01", 12);
                }
                else
                {
                    txtAdd.Font = new Font("Segoe UI", 10);
                    txtName.Font = new Font("Segoe UI", 10);
                }
            }
            maxcustomerid();
            fillcombo();
            fillState();
            fillMistriCombo();
            fillsearchmistricombo();
            hidelabel();
            fillDestinationName();
            btndelete.Enabled = false;
            cmbCustomerType.SelectedIndex = cmbCustomerType.FindStringExact("Retailer");
            cmbRouteLoad();
            this.ActiveControl = txtcustomercode;
        }

        public void cmbRouteLoad()
        {
            try
            {
                VillageRepository ACGRepo = new VillageRepository();
                List<tblVillageInfo> ACGdata = new List<tblVillageInfo>();
                ACGdata.Clear();
                ACGdata = ACGRepo.GetAll().OrderByDescending(t => t.VillageName).ToList();
                ACGdata.Add(new tblVillageInfo { VillageName = "Select", VillageId = 0 });
                ACGdata.Reverse();
                cmbRouteName.DataSource = ACGdata;
                cmbRouteName.ValueMember = "VillageId";
                cmbRouteName.DisplayMember = "VillageName";
                ACGRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public frmcustomer(int cid)
        {
            try
            {
                InitializeComponent();
                cm.changedatetimepickerrange(this);
                settData = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (settData != null)
                {
                    if (settData.Applyonmarathifont == "Y")
                    {
                        txtAdd.Font = new Font("Shivaji01", 12);
                        txtName.Font = new Font("Shivaji01", 12);
                    }
                    else
                    {
                        txtAdd.Font = new Font("Segoe UI", 10);
                        txtName.Font = new Font("Segoe UI", 10);
                    }
                }
                fillMistriCombo();
                fillcombo();
                fillState();
                fillMistriCombo();
                fillsearchmistricombo();
                hidelabel();
                fillDestinationName();
                cmbRouteLoad();
                CustomerInformation custdata = new CustomerInformation();
                CustomerRepository custrepo = new CustomerRepository();
                custdata = custrepo.GetAll().Where(t => t.CustomerID == cid && t.CompId == CommonMethod.CompId).FirstOrDefault();
                Customerid = custdata.Id;
                txtCustomerID.Text = custdata.CustomerID.ToString();
                txtCustomerName.Text = custdata.CustomerName.ToString();
                if (!string.IsNullOrEmpty(custdata.CustomeAddress))
                    txtCustomerAddress.Text = custdata.CustomeAddress.ToString();
                if (!string.IsNullOrEmpty(custdata.Mobileno))
                    txtmobileno.Text = custdata.Mobileno.ToString();
                if (!string.IsNullOrEmpty(custdata.Landlineno))
                    txtlandlineNo.Text = custdata.Landlineno.ToString();
                if (!string.IsNullOrEmpty(custdata.BuyerTinNo))
                    txttinno.Text = custdata.BuyerTinNo.ToString();
                if (!string.IsNullOrEmpty(custdata.Village))
                    txtvillage.Text = custdata.Village.ToString();
                if (!string.IsNullOrEmpty(custdata.FirmName))
                    txtFirmName.Text = custdata.FirmName.ToString();
                if (!string.IsNullOrEmpty(custdata.BirthDate.ToString()))
                    dtpBirthDate.Value = custdata.BirthDate.Value;
                if (!string.IsNullOrEmpty(custdata.Adharcardno))
                    txtAdharCardNo.Text = custdata.Adharcardno.ToString();
                if (Convert.ToString(custdata.CustomerType) == null)
                    cmbCustomerType.SelectedIndex = cmbCustomerType.FindStringExact("Retailer");
                else
                    cmbCustomerType.Text = custdata.CustomerType.ToString();
                if (!string.IsNullOrEmpty(custdata.Panno))
                    txtPanno.Text = custdata.Panno;
                if (!string.IsNullOrEmpty(custdata.State))
                    cmbState.Text = custdata.State;
                if (!string.IsNullOrEmpty(Convert.ToString(custdata.CreditDays)))
                    txtcreditdays.Text = Convert.ToString(custdata.CreditDays);
                else
                    txtcreditdays.Text = "0";
                if (custdata.MistriCode == null)
                    cmbMechanic.Text = "";
                else
                {
                    CustomerRepository custrepo1 = new CustomerRepository();
                    var Mistridata = custrepo1.GetAll().Where(t => t.MistriCode == Convert.ToInt32(custdata.MistriCode) && t.CustomerID == cid && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    cmbMechanic.Text = Mistridata.MistriName;
                    custrepo.Dispose();
                }
                RemainingPaymentRepository paymentrepo = new RemainingPaymentRepository();
                try
                {
                    decimal OpeAmt = Convert.ToDecimal(paymentrepo.GetAll().Where(t => t.Type == "OpeningBalance" && t.CustomerId == cid && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.NetAmt));
                    txtopeningBal.Text = Convert.ToString(OpeAmt);
                }
                catch (Exception)
                { txtopeningBal.Text = "0"; }
                txtFssaiNo.Text = custdata.FSSAINo;
                try
                {
                    txtBusinessName.Text = custdata.BusinessName;
                }
                catch (Exception)
                { txtBusinessName.Text = ""; }

                try
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPremainingpayment", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                    command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "CustRem";
                    command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = cid;
                    DataTable dt1 = new DataTable();
                    dt1.Load(command.ExecuteReader());
                    object sumtotAmount = new object();
                    sumtotAmount = dt1.Compute("Sum(TotalReaminingAmount)", "");
                    db.CloseConnection();
                    lblAmt.Visible = true;
                    lblRemAmt.Visible = true;
                    lblRemAmt.Text = Convert.ToString(sumtotAmount);
                    dt1.Dispose();
                }
                catch (Exception)
                { db.CloseConnection(); }
                btndelete.Enabled = false;
                CustomerCount();
                btnSave.Text = "Update";
                this.ActiveControl = txtmobileno;
            }
            catch (Exception)
            { MessageBox.Show("error"); }
        }

        public void hidelabel()
        {
            try
            {
                lblAmt.Visible = false;
                lblRemAmt.Visible = false;
            }
            catch (Exception)
            { }
        }

        public void CustomerCount()
        {
            try
            {
                CustomerRepository custrepo = new CustomerRepository();
                lblTotaluCustomers.Text = Convert.ToString(custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Count());
                custrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillDestinationName()
        {
            CompanyInformationRepository compRepo = new CompanyInformationRepository();
            List<CompanyInformation> compdata = new List<CompanyInformation>();
            compdata.Clear();
            compdata = compRepo.GetAll().Where(t => t.Id != CommonMethod.CompId).OrderByDescending(t => t.CompanyName).ToList();
            compdata.Add(new CompanyInformation { CompanyName = "Select", CompanyId = 0 });
            compdata.Reverse();
            cmbdestination.DataSource = compdata;
            cmbdestination.ValueMember = "CompanyId";
            cmbdestination.DisplayMember = "CompanyName";
            compRepo.Dispose();
            cmbdestination.SelectedIndex = 0;
        }

        public void maxcustomerid()
        {
            try
            {
                CustomerRepository custrepo = new CustomerRepository();
                var mid = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.CustomerID);
                if (mid == null)
                    txtCustomerID.Text = "1";
                else
                    txtCustomerID.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                custrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                txtCustomerName.Text = "";
                txtCustomerAddress.Text = "";
                txtmobileno.Text = "";
                txtlandlineNo.Text = "";
                maxcustomerid();
                btndelete.Enabled = false;
                fillMistriCombo();
                hidelabel();
                fillsearchmistricombo();
                btnSave.Text = "Save";
                txttinno.Text = "";
                txtvillage.Text = "";
                txtopeningBal.Text = "";
                cmbCustomerType.SelectedIndex = cmbCustomerType.FindStringExact("Retailer");
                txtFirmName.Text = "";
                dtpBirthDate.Value = DateTime.Now.Date;
                txtPanno.Text = "";
                fillDestinationName();
                txtAdharCardNo.Text = "";
                txtName.Text = "";
                txtAdd.Text = "";
                txtcreditdays.Text = "0";
                txtcustomercode.Text = "";
                fillState();
                GvcustomerInfo.Rows.Clear();
                linkLabel1.Visible = false;
                tblltSwitch.Visible = false;
                dtpGSTINaddeddate.Value = DateTime.Now.Date;
                dtpOpeningBalanceDate.Value = DateTime.Now.Date;
                rdbBonusInPer.Visible = false;
                rdbBonusInRs.Checked = false;
                rdbBonusInPer.Checked = false;
                rdbBonusInRs.Visible = false;
                txtBonusInPer.Visible = false;
                txtBonusInRs.Visible = false;
                txtBonusInRs.Text = "0";
                txtBonusInPer.Text = "0";
                tblltSwitch.Visible = false;
                fillcombo();
                cmbRouteLoad();
                txtopeningBal.Enabled = true;
                this.ActiveControl = cmbMechanic;
                txtFssaiNo.Text = "";
                txtBusinessName.Text = "";
            }
            catch (Exception)
            { }
        }

        public void fillMistriCombo()
        {
            try
            {
                MistriRepository MistriRepo = new MistriRepository();
                List<MistriInformation> mistridata = new List<MistriInformation>();
                mistridata.Clear();
                mistridata = MistriRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.MistriName).ToList();
                mistridata.Add(new MistriInformation { MistriName = "Select", MistriCode = 0 });
                mistridata.Reverse();
                cmbMechanic.DataSource = mistridata;
                cmbMechanic.ValueMember = "MistriCode";
                cmbMechanic.DisplayMember = "MistriName";
                MistriRepo.Dispose();
                cmbMechanic.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void fillsearchmistricombo()
        {
            try
            {
                MistriRepository MistriRepo = new MistriRepository();
                List<MistriInformation> mistridata = new List<MistriInformation>();
                mistridata.Clear();
                mistridata = MistriRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.MistriName).ToList();
                mistridata.Add(new MistriInformation { MistriName = "Select", MistriCode = 0 });
                mistridata.Reverse();
                cmbmechname.DataSource = mistridata;
                cmbmechname.ValueMember = "MistriCode";
                cmbmechname.DisplayMember = "MistriName";
                MistriRepo.Dispose();
                cmbmechname.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void fillcombo()
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
                cmbcustomername.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void Bindcustomerinfo()
        {
            try
            {
                CustomerRepository custrepo = new CustomerRepository();
                GvcustomerInfo.Rows.Clear();
                List<CustomerInformation> ItemList = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).ToList();
                lblTotaluCustomers.Text = Convert.ToString(ItemList.Count());
                if (ItemList.Count != 0)
                {
                    DateTime dtGstinDate = CommonMethod.startdate;
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        GvcustomerInfo.Rows.Add();
                        GvcustomerInfo.Rows[i].Cells["CustomerName"].Value = item.CustomerName;
                        GvcustomerInfo.Rows[i].Cells["Address"].Value = item.CustomeAddress;
                        GvcustomerInfo.Rows[i].Cells["MobileNo"].Value = item.Mobileno;
                        GvcustomerInfo.Rows[i].Cells["LandlineNo"].Value = item.Landlineno;
                        GvcustomerInfo.Rows[i].Cells["CustomercID"].Value = item.CustomerID;
                        GvcustomerInfo.Rows[i].Cells["ID"].Value = item.Id;
                        GvcustomerInfo.Rows[i].Cells["MechanicId"].Value = item.MistriCode;
                        GvcustomerInfo.Rows[i].Cells["MechanicName"].Value = item.MistriName;
                        GvcustomerInfo.Rows[i].Cells["TINNo"].Value = item.BuyerTinNo;
                        GvcustomerInfo.Rows[i].Cells["Village"].Value = item.Village;
                        GvcustomerInfo.Rows[i].Cells["CustType"].Value = item.CustomerType;
                        GvcustomerInfo.Rows[i].Cells["FirmName"].Value = item.FirmName;
                        GvcustomerInfo.Rows[i].Cells["BirthDate"].Value = item.BirthDate;
                        GvcustomerInfo.Rows[i].Cells["PANNo"].Value = item.Panno;
                        GvcustomerInfo.Rows[i].Cells["StateCode"].Value = item.Statecode;
                        GvcustomerInfo.Rows[i].Cells["State"].Value = item.State;
                        GvcustomerInfo.Rows[i].Cells["AdharCardNo"].Value = item.Adharcardno;
                        GvcustomerInfo.Rows[i].Cells["CompanyId"].Value = item.CompId;
                        GvcustomerInfo.Rows[i].Cells["MarathiName"].Value = item.CustNameMarathi;
                        GvcustomerInfo.Rows[i].Cells["MarathiAddress"].Value = item.CustAddMarathi;
                        GvcustomerInfo.Rows[i].Cells["CreditDays"].Value = item.CreditDays;
                        GvcustomerInfo.Rows[i].Cells["CustomerCode"].Value = item.CustomerCode;
                        GvcustomerInfo.Rows[i].Cells["OpeningDate"].Value = item.OpeningBalanceDate;
                        GvcustomerInfo.Rows[i].Cells["GSTINDate"].Value = (item.GSTINAddedDate == null) ? dtGstinDate : item.GSTINAddedDate;
                        GvcustomerInfo.Rows[i].Cells["BonusIn"].Value = item.BonusIn;
                        GvcustomerInfo.Rows[i].Cells["BonusInRsNPer"].Value = item.BonusInRsNPer;
                        GvcustomerInfo.Rows[i].Cells["FssaiNo"].Value = item.FSSAINo;
                        GvcustomerInfo.Rows[i].Cells["BusinessName"].Value = item.BusinessName;
                        try
                        {
                            VillageRepository rRepo = new VillageRepository();
                            string routeName = rRepo.GetAll().Where(t => t.VillageId == item.RouteId).FirstOrDefault().VillageName;
                            rRepo.Dispose();
                            GvcustomerInfo.Rows[i].Cells["RouteId"].Value = routeName;
                        }
                        catch (Exception)
                        {
                        }

                        List<CustomerInformation> bonusList = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId && (t.BonusIn == "Bonus In Per" || t.BonusIn == "Bonus In Rs")).ToList();
                        if (bonusList.Count != 0)
                        {
                            bonusInRs = 0;
                            CustomerBillMasterRepository CustBMrepo = new CustomerBillMasterRepository();
                            List<CustomerBillMaster> clist = new List<CustomerBillMaster>();
                            clist = CustBMrepo.GetAll().Where(t1 => t1.CustomerId == Convert.ToInt32(item.CustomerID) && t1.CompId == CommonMethod.CompId && t1.TransactionYear == CommonMethod.TransactionYear).ToList();
                            if (clist.Count > 0)
                            {
                                foreach (var item1 in clist)
                                {
                                    if (item.BonusIn == "Bonus In Rs")
                                        bonusInRs = bonusInRs + Convert.ToDecimal(item.BonusInRsNPer);
                                    else if (item.BonusIn == "Bonus In Per")
                                        bonusInRs = bonusInRs + (Convert.ToDecimal(item1.NetAmount) * Convert.ToDecimal(item.BonusInRsNPer) / 100);
                                }
                                GvcustomerInfo.Columns["TotalBonus"].Visible = true;
                            }
                            if (item.BonusIn == "Bonus In Rs" || item.BonusIn == "Bonus In Per")
                                GvcustomerInfo.Rows[i].Cells["TotalBonus"].Value = bonusInRs;
                        }
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
                MistriRepository MistriRepo = new MistriRepository();
                MistriInformation MistriInfo = new MistriInformation();
                RemainingPaymentRepository paymentrepo = new RemainingPaymentRepository();
                RemainingPayment paydata = new RemainingPayment();
                if (txtCustomerName.Text.Trim() != "" && cmbCustomerType.Text != "" && cmbState.SelectedIndex > 0)
                {
                    if (btnSave.Text == "Update")
                    {
                        if (tblltSwitch.Visible == true)
                        {
                            if (cmbdestination.Text != "" && cmbdestination.Text != "Select")
                            {
                                CustomerRepository custrepo1 = new CustomerRepository();
                                var cust = custrepo1.GetAll().Where(t => t.CustomerName.ToLower() == txtCustomerName.Text.Trim().ToLower() && t.CompId == Convert.ToInt32(cmbdestination.SelectedValue)).FirstOrDefault();
                                if (cust != null)
                                {
                                    MessageBox.Show("Already Having This Name.", "Error");
                                    txtCustomerName.Text = "";
                                    this.ActiveControl = txtCustomerName;
                                    goto a;
                                }
                                else
                                {
                                    try
                                    {
                                        CustomerRepository custrepo = new CustomerRepository();
                                        CustomerInformation custdata = new CustomerInformation();
                                        string maxid = "";
                                        try
                                        {
                                            var mid = custrepo.GetAll().Where(t => t.CompId == Convert.ToInt32(cmbdestination.SelectedValue)).Max(t => t.CustomerID);
                                            if (mid == null)
                                                maxid = "1";
                                            else
                                                maxid = Convert.ToString(Convert.ToInt32(mid) + 1);
                                        }
                                        catch (Exception)
                                        { }
                                        custdata.CustomerID = Convert.ToInt32(maxid);
                                        custdata.CustomerName = txtCustomerName.Text.Trim();
                                        custdata.CustomeAddress = txtCustomerAddress.Text.Trim();
                                        custdata.Landlineno = txtlandlineNo.Text.Trim();
                                        custdata.Mobileno = txtmobileno.Text.Trim();
                                        custdata.BuyerTinNo = txttinno.Text.Trim();
                                        custdata.Village = txtvillage.Text.Trim();
                                        custdata.CustomerType = cmbCustomerType.Text;
                                        custdata.FirmName = txtFirmName.Text;
                                        custdata.BirthDate = Convert.ToDateTime(dtpBirthDate.Value);
                                        custdata.Adharcardno = txtAdharCardNo.Text;
                                        if (cmbMechanic.SelectedIndex == 0)
                                        {
                                            custdata.MistriCode = null;
                                            custdata.MistriName = "";
                                        }
                                        else
                                        {
                                            var Mistridata = MistriRepo.GetAll().Where(t => t.MistriCode == Convert.ToInt32(cmbMechanic.SelectedValue)).FirstOrDefault();
                                            custdata.MistriCode = Mistridata.Id;
                                            custdata.MistriName = cmbMechanic.Text;
                                        }
                                        custdata.CompId = Convert.ToInt32(cmbdestination.SelectedValue);
                                        custdata.Panno = txtPanno.Text;
                                        custdata.State = cmbState.Text;
                                        custdata.Statecode = Convert.ToInt32(cmbState.SelectedValue);
                                        custdata.CustNameMarathi = txtName.Text;
                                        custdata.CustAddMarathi = txtAdd.Text;
                                        if (rdbBonusInRs.Checked == true)
                                        {
                                            custdata.BonusInRsNPer = Convert.ToDecimal(txtBonusInRs.Text);
                                            custdata.BonusIn = rdbBonusInRs.Text;
                                        }
                                        else if (rdbBonusInPer.Checked == true)
                                        {
                                            custdata.BonusInRsNPer = Convert.ToDecimal(txtBonusInPer.Text);
                                            custdata.BonusIn = rdbBonusInPer.Text;
                                        }
                                        else
                                        {
                                            custdata.BonusInRsNPer = 0;
                                            custdata.BonusIn = null;
                                        }
                                        custdata.CreditDays = Convert.ToInt32(txtcreditdays.Text);
                                        custdata.CustomerCode = txtcustomercode.Text;
                                        if (txtopeningBal.Text.Trim() != "0" && !string.IsNullOrEmpty(txtopeningBal.Text))
                                            custdata.OpeningBalanceDate = dtpOpeningBalanceDate.Value;
                                        if (txttinno.Text.Trim() != "" && !string.IsNullOrEmpty(txttinno.Text))
                                            custdata.GSTINAddedDate = dtpGSTINaddeddate.Value;
                                        custdata.RouteId = Convert.ToInt32(cmbRouteName.SelectedValue);
                                        custdata.FSSAINo = txtFssaiNo.Text;
                                        custdata.BusinessName = txtBusinessName.Text;
                                        custrepo.Add(custdata);
                                        custrepo.Save();
                                        if (txtopeningBal.Text.Trim() != "0" && !string.IsNullOrEmpty(txtopeningBal.Text) && txtopeningBal.Text.Trim() != "0.00")
                                        {
                                            try
                                            {
                                                int Billrecieptno = 0;
                                                var mid = paymentrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.BillReceiptNo);
                                                if (mid == null)
                                                    Billrecieptno = 1;
                                                else
                                                    Billrecieptno = Convert.ToInt32(mid) + 1;
                                                paydata.CustomerId = Convert.ToInt32(custdata.CustomerID);
                                                paydata.Customername = Convert.ToString(custdata.CustomerName);
                                                paydata.BillReceiptNo = Billrecieptno;
                                                paydata.Type = "OpeningBalance";
                                                if (txtopeningBal.Text != "")
                                                {
                                                    paydata.NetAmt = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                                    paydata.RemainingAmt = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                                }
                                                else
                                                    paydata.NetAmt = 0;
                                                paydata.CreateDate = dtpOpeningBalanceDate.Value;
                                                paydata.TransactionYear = CommonMethod.TransactionYear;
                                                paydata.CompId = Convert.ToInt32(cmbdestination.SelectedValue);
                                                paymentrepo.Add(paydata);
                                                paymentrepo.Save();
                                            }
                                            catch (Exception)
                                            { }
                                        }
                                        MessageBox.Show("Record Saved Sucessfully.", "Success");
                                        clear();
                                        this.ActiveControl = txtCustomerName;
                                    }
                                    catch (Exception) { }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please Select Destination Company Name.", "Warning");
                                this.ActiveControl = cmbdestination;
                                goto b;
                            }
                        }
                        else
                        {
                            CustomerRepository custrepo1 = new CustomerRepository();
                            var cust = custrepo1.GetAll().Where(t => t.CustomerName.ToLower() == txtCustomerName.Text.Trim().ToLower() && t.CustomerID != Convert.ToInt32(txtCustomerID.Text) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            if (cust != null)
                            {
                                MessageBox.Show("Already Having This Name.", "Error");
                                txtCustomerName.Text = "";
                                this.ActiveControl = txtCustomerName;
                                goto a;
                            }
                            else
                            {
                                CustomerRepository custrepo = new CustomerRepository();
                                CustomerInformation custdata = new CustomerInformation();
                                string oldCustName = custdata.CustomerName;
                                custdata = custrepo.GetById(Customerid);
                                custdata.CustomerID = Convert.ToInt32(txtCustomerID.Text);
                                custdata.CustomerName = txtCustomerName.Text.Trim();
                                custdata.CustomeAddress = txtCustomerAddress.Text.Trim();
                                custdata.Landlineno = txtlandlineNo.Text.Trim();
                                custdata.Mobileno = txtmobileno.Text.Trim();
                                custdata.BuyerTinNo = txttinno.Text.Trim();
                                custdata.Village = txtvillage.Text.Trim();
                                custdata.CustomerType = cmbCustomerType.Text;
                                custdata.FirmName = txtFirmName.Text;
                                custdata.BirthDate = Convert.ToDateTime(dtpBirthDate.Value);
                                custdata.Adharcardno = txtAdharCardNo.Text;
                                if (cmbMechanic.SelectedIndex == 0)
                                {
                                    custdata.MistriCode = null;
                                    custdata.MistriName = "";
                                }
                                else
                                {
                                    var Mistridata = MistriRepo.GetAll().Where(t => t.MistriCode == Convert.ToInt32(cmbMechanic.SelectedValue) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                    custdata.MistriCode = Mistridata.Id;
                                    custdata.MistriName = cmbMechanic.Text;
                                }
                                custdata.CompId = CommonMethod.CompId;
                                custdata.Panno = txtPanno.Text;
                                custdata.State = cmbState.Text;
                                custdata.Statecode = Convert.ToInt32(cmbState.SelectedValue);
                                custdata.CustNameMarathi = txtName.Text;
                                custdata.CustAddMarathi = txtAdd.Text;
                                custdata.CreditDays = Convert.ToInt32(txtcreditdays.Text);
                                custdata.CustomerCode = txtcustomercode.Text;
                                if (txtopeningBal.Text.Trim() != "0" && !string.IsNullOrEmpty(txtopeningBal.Text))
                                    custdata.OpeningBalanceDate = dtpOpeningBalanceDate.Value;
                                if (txttinno.Text.Trim() != "" && !string.IsNullOrEmpty(txttinno.Text))
                                    custdata.GSTINAddedDate = dtpGSTINaddeddate.Value;
                                if (rdbBonusInRs.Checked == true)
                                {
                                    custdata.BonusInRsNPer = Convert.ToDecimal(txtBonusInRs.Text);
                                    custdata.BonusIn = rdbBonusInRs.Text;
                                }
                                else if (rdbBonusInPer.Checked == true)
                                {
                                    custdata.BonusInRsNPer = Convert.ToDecimal(txtBonusInPer.Text);
                                    custdata.BonusIn = rdbBonusInPer.Text;
                                }
                                else
                                {
                                    custdata.BonusInRsNPer = 0;
                                    custdata.BonusIn = null;
                                }
                                custdata.RouteId = Convert.ToInt32(cmbRouteName.SelectedValue);
                                custdata.FSSAINo = txtFssaiNo.Text;
                                custdata.BusinessName = txtBusinessName.Text;
                                custrepo.Edit(custdata);
                                custrepo.Save();

                                if (oldCustName != custdata.CustomerName)
                                {
                                    try
                                    {
                                        db.connect();
                                        SqlCommand command = new SqlCommand("SPUpdateCustomer", db.Connection);
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = custdata.CustomerID;
                                        command.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = custdata.CustomerName;
                                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                        command.ExecuteNonQuery();
                                        db.CloseConnection();
                                    }
                                    catch (Exception)
                                    { db.CloseConnection(); }
                                }
                                if (txtopeningBal.Text != "0" || txtopeningBal.Text.Trim() != "0.00" && txtopeningBal.Enabled == true && !string.IsNullOrEmpty(txtopeningBal.Text))
                                {
                                    try
                                    {
                                        RemainingPaymentRepository pRepo = new RemainingPaymentRepository();
                                        var remData = paymentrepo.GetAll().Where(t => t.Type == "OpeningBalance" && t.CustomerId == custdata.CustomerID && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                        if (remData != null)
                                        {
                                            paydata.CustomerId = Convert.ToInt32(custdata.CustomerID);
                                            paydata.Customername = Convert.ToString(custdata.CustomerName);
                                            paydata.BillReceiptNo = remData.BillReceiptNo;
                                            paydata.CreateDate = dtpOpeningBalanceDate.Value;
                                            paydata.Type = "OpeningBalance";
                                            if (txtopeningBal.Text != "")
                                            {
                                                paydata.NetAmt = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                                paydata.RemainingAmt = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                                decimal decremamt = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                                RemainingPaymentRepository remainingpayrepo2 = new RemainingPaymentRepository();
                                                List<RemainingPayment> remainingdata2 = new List<RemainingPayment>();
                                                remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(custdata.CustomerID) && t.Id > remData.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                                int i = 0;
                                                foreach (var item in remainingdata2)
                                                {
                                                    decimal decnetpaidamt = 0;
                                                    decnetpaidamt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt);
                                                    item.RemainingAmt = decnetpaidamt + decremamt;
                                                    decremamt = decnetpaidamt + decremamt;
                                                    remainingpayrepo2.Edit(item);
                                                    remainingpayrepo2.Save();
                                                    i++;
                                                }
                                            }
                                            else
                                                paydata.NetAmt = 0;
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
                                                int Billrecieptno = 0;
                                                var mid = paymentrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.BillReceiptNo);
                                                if (mid == null)
                                                    Billrecieptno = 1;
                                                else
                                                    Billrecieptno = Convert.ToInt32(mid) + 1;
                                                paydata.CustomerId = Convert.ToInt32(custdata.CustomerID);
                                                paydata.Customername = Convert.ToString(custdata.CustomerName);
                                                paydata.BillReceiptNo = Billrecieptno;
                                                paydata.Type = "OpeningBalance";
                                                if (txtopeningBal.Text != "")
                                                {
                                                    paydata.NetAmt = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                                    paydata.RemainingAmt = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                                }
                                                else
                                                    paydata.NetAmt = 0;
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
                                if (txtopeningBal.Text == "0" || txtopeningBal.Text.Trim() == "0.00")
                                {
                                    RemainingPaymentRepository remPayrepo = new RemainingPaymentRepository();
                                    var remdata = remPayrepo.GetAll().Where(t => t.Type == "OpeningBalance" && t.CustomerId == custdata.CustomerID && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    if (remdata != null)
                                    {
                                        receiptId = Convert.ToInt32(remdata.Id);
                                        RemainingPaymentRepository remRepo = new RemainingPaymentRepository();
                                        RemainingPayment remdata1 = remRepo.GetAll().Where(t => t.Id == receiptId && t.Type == "OpeningBalance" && t.CustomerId == custdata.CustomerID && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                        remRepo.Remove(remdata1);
                                        remRepo.Save();
                                        if (remdata1 != null)
                                        {
                                            RemainingPaymentRepository remainingpayrepo1 = new RemainingPaymentRepository();
                                            RemainingPayment remainingdata1 = new RemainingPayment();
                                            remainingdata1 = remainingpayrepo1.GetAll().Where(t => t.CustomerId == Convert.ToInt32(remdata1.CustomerId) && t.Id < remdata1.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                                            if (remainingdata1 != null)
                                            {
                                                decimal decremamt = Convert.ToDecimal(remainingdata1.RemainingAmt);
                                                RemainingPaymentRepository remainingpayrepo2 = new RemainingPaymentRepository();
                                                List<RemainingPayment> remainingdata2 = new List<RemainingPayment>();
                                                remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(remdata1.CustomerId) && t.Id > remdata1.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                                                int i = 0;
                                                foreach (var item in remainingdata2)
                                                {
                                                    decimal decnetpaidamt = 0;
                                                    decnetpaidamt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt);
                                                    item.RemainingAmt = decnetpaidamt + decremamt;
                                                    decremamt = decnetpaidamt + decremamt;
                                                    remainingpayrepo2.Edit(item);
                                                    remainingpayrepo2.Save();
                                                    i++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            MessageBox.Show("Record Updated Sucessfully.", "Success");
                            GvcustomerInfo.Rows.Clear();
                            clear();
                        }
                    a:
                        {
                            clear();
                            this.ActiveControl = cmbcustomername;
                        }
                    b:
                        { }
                    }
                    else
                    {
                        CustomerRepository custrepo1 = new CustomerRepository();
                        var cust = custrepo1.GetAll().Where(t => t.CustomerName.ToLower() == txtCustomerName.Text.Trim().ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (cust != null)
                        {
                            MessageBox.Show("Already Saved This Record.", "Error");
                            txtCustomerName.Text = "";
                            this.ActiveControl = txtCustomerName;
                        }
                        else
                        {
                            CustomerRepository custrepo = new CustomerRepository();
                            CustomerInformation custdata = new CustomerInformation();
                            maxcustomerid();
                            custdata.CustomerID = Convert.ToInt32(txtCustomerID.Text);
                            custdata.CustomerName = txtCustomerName.Text.Trim();
                            custdata.CustomeAddress = txtCustomerAddress.Text.Trim();
                            custdata.Landlineno = txtlandlineNo.Text.Trim();
                            custdata.Mobileno = txtmobileno.Text.Trim();
                            custdata.BuyerTinNo = txttinno.Text.Trim();
                            custdata.Village = txtvillage.Text.Trim();
                            custdata.CustomerType = cmbCustomerType.Text;
                            custdata.FirmName = txtFirmName.Text;
                            custdata.BirthDate = Convert.ToDateTime(dtpBirthDate.Value);
                            custdata.Adharcardno = txtAdharCardNo.Text;
                            if (cmbMechanic.SelectedIndex == 0)
                            {
                                custdata.MistriCode = null;
                                custdata.MistriName = "";
                            }
                            else
                            {
                                var Mistridata = MistriRepo.GetAll().Where(t => t.MistriCode == Convert.ToInt32(cmbMechanic.SelectedValue)).FirstOrDefault();
                                custdata.MistriCode = Mistridata.Id;
                                custdata.MistriName = cmbMechanic.Text;
                            }
                            custdata.CompId = CommonMethod.CompId;
                            custdata.Panno = txtPanno.Text;
                            custdata.State = cmbState.Text;
                            custdata.Statecode = Convert.ToInt32(cmbState.SelectedValue);
                            custdata.CustNameMarathi = txtName.Text;
                            custdata.CustAddMarathi = txtAdd.Text;
                            custdata.CreditDays = Convert.ToInt32(txtcreditdays.Text);
                            custdata.CustomerCode = txtcustomercode.Text;
                            if (txtopeningBal.Text.Trim() != "0" && !string.IsNullOrEmpty(txtopeningBal.Text))
                                custdata.OpeningBalanceDate = dtpOpeningBalanceDate.Value;
                            if (txttinno.Text.Trim() != "" && !string.IsNullOrEmpty(txttinno.Text))
                                custdata.GSTINAddedDate = dtpGSTINaddeddate.Value;
                            if (rdbBonusInRs.Checked == true)
                            {
                                custdata.BonusInRsNPer = Convert.ToDecimal(txtBonusInRs.Text);
                                custdata.BonusIn = rdbBonusInRs.Text;
                            }
                            else if (rdbBonusInPer.Checked == true)
                            {
                                custdata.BonusInRsNPer = Convert.ToDecimal(txtBonusInPer.Text);
                                custdata.BonusIn = rdbBonusInPer.Text;
                            }
                            else
                            {
                                custdata.BonusInRsNPer = 0;
                                custdata.BonusIn = null;
                            }
                            custdata.RouteId = Convert.ToInt32(cmbRouteName.SelectedValue);
                            custdata.FSSAINo = txtFssaiNo.Text;
                            custdata.BusinessName = txtBusinessName.Text;
                            custrepo.Add(custdata);
                            custrepo.Save();
                            if (txtopeningBal.Text.Trim() != "0.00" && !string.IsNullOrEmpty(txtopeningBal.Text) && txtopeningBal.Text.Trim() != "0")
                            {
                                try
                                {
                                    int Billrecieptno = 0;
                                    var mid = paymentrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.BillReceiptNo);
                                    if (mid == null)
                                        Billrecieptno = 1;
                                    else
                                        Billrecieptno = Convert.ToInt32(mid) + 1;
                                    paydata.CustomerId = Convert.ToInt32(custdata.CustomerID);
                                    paydata.Customername = Convert.ToString(custdata.CustomerName);
                                    paydata.BillReceiptNo = Billrecieptno;
                                    paydata.Type = "OpeningBalance";
                                    if (txtopeningBal.Text != "")
                                    {
                                        paydata.NetAmt = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                        paydata.RemainingAmt = Convert.ToDecimal(txtopeningBal.Text.Trim());
                                    }
                                    else
                                        paydata.NetAmt = 0;
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
                        }
                    }
                }
                else
                {
                    if (txtCustomerName.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter Customername.", "Warning");
                        this.ActiveControl = txtCustomerName;
                    }
                    else if (cmbState.SelectedIndex == 0)
                    {
                        MessageBox.Show("Select State.", "Warning");
                        this.ActiveControl = cmbState;
                    }
                    else
                    {
                        MessageBox.Show("Select Customer Type.", "Warning");
                        this.ActiveControl = cmbCustomerType;
                    }
                }
                MistriRepo.Dispose();
                paymentrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Bindcustomerinfo();
            fillcombo();
            this.ActiveControl = cmbMechanic;
            btndelete.Enabled = false;
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void frmcustomer_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                dtpBirthDate.MinDate = DateTimePicker.MinimumDateTime;
                dtpBirthDate.MaxDate = DateTimePicker.MaximumDateTime;

                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = new SqlCommand("SELECT CustomerName,CustomerID FROM CustomerInformation where CompId='" + CommonMethod.CompId + "'", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        { MyCollection.Add(reader.GetString(0)); }
                        catch (Exception)
                        { }
                    }
                    cmbcustomername.AutoCompleteCustomSource = MyCollection;
                    txtCustomerName.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
            try
            {
                //string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                //using (SqlConnection con = new SqlConnection(cnString1))
                //{
                //    SqlCommand cmd = new SqlCommand("SELECT CustomerName FROM CustomerInformation where CompId='" + CommonMethod.CompId + "'", con);
                //    con.Open();
                //    SqlDataReader reader = cmd.ExecuteReader();
                //    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                //    while (reader.Read())
                //    {
                //        try
                //        {
                //            MyCollection.Add(reader.GetString(0));
                //        }
                //        catch (Exception)
                //        { }
                //    }
                //    txtCustomerName.AutoCompleteCustomSource = MyCollection;
                //    con.Close();
                //    reader.Dispose();
                //}
                SettingRepository sRepo = new SettingRepository();
                Setting sdata = new Setting();
                sdata = sRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (sdata != null)
                {
                    if (sdata.Applyonmarathifont == "Y")
                    {
                        txtAdd.Font = new Font("Shivaji01", 12);
                        txtName.Font = new Font("Shivaji01", 12);
                    }
                    else
                    {
                        txtAdd.Font = new Font("Microsoft Sans Serif", 10);
                        txtName.Font = new Font("Microsoft Sans Serif", 10);
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
                if (cmbcustomername.Text != "Select" && cmbmechname.Text == "Select")
                {
                    RptCustomerInfo custrpt = new RptCustomerInfo(cmbcustomername.Text, null);
                    custrpt.Show();
                }
                else if (cmbcustomername.Text == "Select" && cmbmechname.Text != "Select")
                {
                    RptCustomerInfo custrpt = new RptCustomerInfo(null, cmbmechname.Text);
                    custrpt.Show();
                }
                else
                {
                    RptCustomerInfo custrpt = new RptCustomerInfo();
                    custrpt.Show();
                }
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void cmbcustomername_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DateTime dtGstinDate = CommonMethod.startdate;
                cmbmechname.Text = "Select";
                CustomerRepository custrepo = new CustomerRepository();
                var custdata = custrepo.GetAll().Where(t => t.CustomerName == cmbcustomername.Text.Trim() && t.CustomerID == Convert.ToInt32(cmbcustomername.SelectedValue) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                var customerid = custdata.Id;
                GvcustomerInfo.Rows.Clear();
                GvcustomerInfo.Rows.Add();
                GvcustomerInfo.Rows[0].Cells["CustomerName"].Value = custdata.CustomerName;
                GvcustomerInfo.Rows[0].Cells["Address"].Value = custdata.CustomeAddress;
                GvcustomerInfo.Rows[0].Cells["MobileNo"].Value = custdata.Mobileno;
                GvcustomerInfo.Rows[0].Cells["LandlineNo"].Value = custdata.Landlineno;
                GvcustomerInfo.Rows[0].Cells["CustomercID"].Value = custdata.CustomerID;
                GvcustomerInfo.Rows[0].Cells["ID"].Value = custdata.Id;
                GvcustomerInfo.Rows[0].Cells["MechanicId"].Value = custdata.MistriCode;
                GvcustomerInfo.Rows[0].Cells["MechanicName"].Value = custdata.MistriName;
                GvcustomerInfo.Rows[0].Cells["TINNo"].Value = custdata.BuyerTinNo;
                GvcustomerInfo.Rows[0].Cells["Village"].Value = custdata.Village;
                GvcustomerInfo.Rows[0].Cells["CustType"].Value = custdata.CustomerType;
                GvcustomerInfo.Rows[0].Cells["FirmName"].Value = custdata.FirmName;
                try
                {
                    GvcustomerInfo.Rows[0].Cells["BirthDate"].Value = custdata.BirthDate.Value;
                }
                catch (Exception)
                { }
                GvcustomerInfo.Rows[0].Cells["PANNo"].Value = custdata.Panno;
                GvcustomerInfo.Rows[0].Cells["StateCode"].Value = custdata.Statecode;
                GvcustomerInfo.Rows[0].Cells["State"].Value = custdata.State;
                GvcustomerInfo.Rows[0].Cells["AdharCardNo"].Value = custdata.Adharcardno;
                GvcustomerInfo.Rows[0].Cells["CompanyId"].Value = custdata.CompId;
                GvcustomerInfo.Rows[0].Cells["MarathiName"].Value = custdata.CustNameMarathi;
                GvcustomerInfo.Rows[0].Cells["MarathiAddress"].Value = custdata.CustAddMarathi;
                GvcustomerInfo.Rows[0].Cells["CreditDays"].Value = custdata.CreditDays;
                GvcustomerInfo.Rows[0].Cells["CustomerCode"].Value = custdata.CustomerCode;
                GvcustomerInfo.Rows[0].Cells["OpeningDate"].Value = custdata.OpeningBalanceDate;
                GvcustomerInfo.Rows[0].Cells["GSTINDate"].Value = (custdata.GSTINAddedDate == null) ? dtGstinDate : custdata.GSTINAddedDate;
                GvcustomerInfo.Rows[0].Cells["BonusIn"].Value = custdata.BonusIn;
                GvcustomerInfo.Rows[0].Cells["BonusInRsNPer"].Value = custdata.BonusInRsNPer;
                GvcustomerInfo.Rows[0].Cells["FssaiNo"].Value = custdata.FSSAINo;
                GvcustomerInfo.Rows[0].Cells["BusinessName"].Value = custdata.BusinessName;
                VillageRepository rRepo = new VillageRepository();
                string routeName = rRepo.GetAll().Where(t => t.VillageId == custdata.RouteId).FirstOrDefault().VillageName;
                rRepo.Dispose();
                GvcustomerInfo.Rows[0].Cells["RouteId"].Value = routeName;
                List<CustomerInformation> bonusList = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId && (t.BonusIn == "Bonus In Per" || t.BonusIn == "Bonus In Rs")).ToList();
                if (bonusList.Count != 0)
                {
                    bonusInRs = 0;
                    CustomerBillMasterRepository CustBMrepo = new CustomerBillMasterRepository();
                    List<CustomerBillMaster> clist = new List<CustomerBillMaster>();
                    clist = CustBMrepo.GetAll().Where(t1 => t1.CustomerId == Convert.ToInt32(custdata.CustomerID) && t1.CompId == CommonMethod.CompId && t1.TransactionYear == CommonMethod.TransactionYear).ToList();
                    if (clist.Count > 0)
                    {
                        foreach (var item1 in clist)
                        {
                            if (custdata.BonusIn == "Bonus In Rs")
                                bonusInRs = bonusInRs + Convert.ToDecimal(custdata.BonusInRsNPer);
                            else if (custdata.BonusIn == "Bonus In Per")
                                bonusInRs = bonusInRs + (Convert.ToDecimal(item1.NetAmount) * Convert.ToDecimal(custdata.BonusInRsNPer) / 100);
                        }
                        GvcustomerInfo.Columns[26].Visible = true;
                    }
                    if (custdata.BonusIn == "Bonus In Rs" || custdata.BonusIn == "Bonus In Per")
                        GvcustomerInfo.Rows[0].Cells["TotalBonus"].Value = bonusInRs;
                }
                custrepo.Dispose();
            }
            catch (Exception)
            { fillcombo(); }
        }

        private void cmbmechname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbcustomername.Text = "Select";
                CustomerRepository custrepo = new CustomerRepository();
                List<CustomerInformation> ItemList = custrepo.GetAll().Where(t => t.MistriName == cmbmechname.Text && t.MistriCode == Convert.ToInt32(cmbmechname.SelectedValue) && t.CompId == CommonMethod.CompId).ToList();

                if (ItemList.Count != 0)
                {
                    GvcustomerInfo.Rows.Clear();
                    int i = 0;
                    foreach (var item in ItemList)
                    {
                        GvcustomerInfo.Rows.Add();
                        GvcustomerInfo.Rows[i].Cells["CustomerName"].Value = item.CustomerName;
                        GvcustomerInfo.Rows[i].Cells["Address"].Value = item.CustomeAddress;
                        GvcustomerInfo.Rows[i].Cells["MobileNo"].Value = item.Mobileno;
                        GvcustomerInfo.Rows[i].Cells["LandlineNo"].Value = item.Landlineno;
                        GvcustomerInfo.Rows[i].Cells["CustomercID"].Value = item.CustomerID;
                        GvcustomerInfo.Rows[i].Cells["ID"].Value = item.Id;
                        GvcustomerInfo.Rows[i].Cells["MechanicId"].Value = item.MistriCode;
                        GvcustomerInfo.Rows[i].Cells["MechanicName"].Value = item.MistriName;
                        GvcustomerInfo.Rows[i].Cells["TINNo"].Value = item.BuyerTinNo;
                        GvcustomerInfo.Rows[i].Cells["Village"].Value = item.Village;
                        GvcustomerInfo.Rows[i].Cells["CustType"].Value = item.CustomerType;
                        GvcustomerInfo.Rows[i].Cells["FirmName"].Value = item.FirmName;
                        try
                        {
                            GvcustomerInfo.Rows[i].Cells["BirthDate"].Value = item.BirthDate.Value;
                        }
                        catch (Exception)
                        { }
                        GvcustomerInfo.Rows[i].Cells["PANNo"].Value = item.Panno;
                        GvcustomerInfo.Rows[i].Cells["StateCode"].Value = item.Statecode;
                        GvcustomerInfo.Rows[i].Cells["State"].Value = item.State;
                        GvcustomerInfo.Rows[i].Cells["AdharCardNo"].Value = item.Adharcardno;
                        GvcustomerInfo.Rows[i].Cells["CompanyId"].Value = item.CompId;
                        GvcustomerInfo.Rows[i].Cells["MarathiName"].Value = item.CustNameMarathi;
                        GvcustomerInfo.Rows[i].Cells["MarathiAddress"].Value = item.CustAddMarathi;
                        GvcustomerInfo.Rows[i].Cells["CreditDays"].Value = item.CreditDays;
                        GvcustomerInfo.Rows[0].Cells["CustomerCode"].Value = item.CustomerCode;
                        GvcustomerInfo.Rows[0].Cells["OpeningDate"].Value = item.OpeningBalanceDate;
                        GvcustomerInfo.Rows[0].Cells["GSTINDate"].Value = item.GSTINAddedDate;
                        GvcustomerInfo.Rows[0].Cells["BonusIn"].Value = item.BonusIn;
                        GvcustomerInfo.Rows[0].Cells["BonusInRsNPer"].Value = item.BonusInRsNPer;
                        RouteRepository rRepo = new RouteRepository();
                        string routeName = rRepo.GetAll().Where(t => t.RouteId == item.RouteId).FirstOrDefault().RouteName;
                        rRepo.Dispose();
                        GvcustomerInfo.Rows[0].Cells["RouteId"].Value = routeName;
                        GvcustomerInfo.Rows[0].Cells["FssaiNo"].Value = item.FSSAINo;
                        GvcustomerInfo.Rows[0].Cells["BusinessName"].Value = item.BusinessName;
                        List<CustomerInformation> bonusList = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId && (t.BonusIn == "Bonus In Per" || t.BonusIn == "Bonus In Rs")).ToList();
                        if (bonusList.Count != 0)
                        {
                            bonusInRs = 0;
                            CustomerBillMasterRepository CustBMrepo = new CustomerBillMasterRepository();
                            List<CustomerBillMaster> clist = new List<CustomerBillMaster>();
                            clist = CustBMrepo.GetAll().Where(t1 => t1.CustomerId == Convert.ToInt32(item.CustomerID) && t1.CompId == CommonMethod.CompId && t1.TransactionYear == CommonMethod.TransactionYear).ToList();
                            if (clist.Count > 0)
                            {
                                foreach (var item1 in clist)
                                {
                                    if (item.BonusIn == "Bonus In Rs")
                                        bonusInRs = bonusInRs + Convert.ToDecimal(item.BonusInRsNPer);
                                    else if (item.BonusIn == "Bonus In Per")
                                        bonusInRs = bonusInRs + (Convert.ToDecimal(item1.NetAmount) * Convert.ToDecimal(item.BonusInRsNPer) / 100);
                                }
                                GvcustomerInfo.Columns[26].Visible = true;
                            }
                            if (item.BonusIn == "Bonus In Rs" || item.BonusIn == "Bonus In Per")
                                GvcustomerInfo.Rows[i].Cells["TotalBonus"].Value = bonusInRs;
                        }
                        i++;
                    }
                }
                custrepo.Dispose();
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

        private void txtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbCustomerType;
            }
            catch (Exception)
            { }
        }

        private void txtvillage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtlandlineNo;
            }
            catch (Exception)
            { }
        }

        public void fillState()
        {
            try
            {
                StateMasterRepository unitrepo = new StateMasterRepository();
                List<StateMaster> unitdata = new List<StateMaster>();
                unitdata.Clear();
                unitdata = unitrepo.GetAll().OrderByDescending(t => t.StateName).ToList();
                unitdata.Add(new StateMaster { StateName = "Select", StateId = 0 });
                unitdata.Reverse();
                cmbState.DataSource = unitdata;
                cmbState.ValueMember = "StateId";
                cmbState.DisplayMember = "StateName";
                unitrepo.Dispose();
                CompanyInformationRepository cRepo = new CompanyInformationRepository();
                var cinfo = cRepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();
                if (cinfo != null)
                    cmbState.SelectedValue = cinfo.StateCode;
                cRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tblltSwitch.Visible = true;
        }

        private void cmbcustomername_Leave(object sender, EventArgs e)
        {
            if (cmbcustomername.Text != "Select" && cmbcustomername.Text != "")
            {
                CommonMethod comMethod = new CommonMethod();
                if (comMethod.ValidCustomer(cmbcustomername.Text) == 1)
                { }
                else
                {
                    MessageBox.Show("Invalid Customer Name.", "Warning");
                    fillcombo();
                    this.ActiveControl = cmbcustomername;
                }
            }
        }

        private void cmbMechanic_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtCustomerName;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmMistri mistri = new frmMistri();
                        mistri.ShowDialog();
                        fillMistriCombo();
                        this.ActiveControl = cmbMechanic;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbCustomerType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtcreditdays;
            }
            catch (Exception)
            { }
        }

        private void txtopeningBal_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtCustomerAddress;
            }
            catch (Exception)
            { }
        }

        private void txtCustomerAddress_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtvillage;
            }
            catch (Exception)
            { }
        }

        private void txtlandlineNo_KeyDown(object sender, KeyEventArgs e)
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
                    this.ActiveControl = txttinno;
            }
            catch (Exception)
            { }
        }

        private void txttinno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtFirmName;
            }
            catch (Exception)
            { }
        }

        private void txtFirmName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpBirthDate;
            }
            catch (Exception)
            { }
        }

        private void dtpBirthDate_KeyDown(object sender, KeyEventArgs e)
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
                    this.ActiveControl = txtName;
            }
            catch (Exception)
            { }
        }

        private void cmbdestination_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtName;
            }
            catch (Exception)
            { }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    frmSettings settings = new frmSettings();
                    settings.ShowDialog();
                    frmcustomer_Load(null, null);
                }
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtAdd;
            }
            catch (Exception)
            { }
        }

        private void txtAdd_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    frmSettings settings = new frmSettings();
                    settings.ShowDialog();
                    frmcustomer_Load(null, null);
                }
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
                    this.ActiveControl = cmbcustomername;
            }
            catch (Exception)
            { }
        }

        private void cmbcustomername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbmechname;
            }
            catch (Exception)
            { }
        }

        private void cmbmechname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btngetall;

            }
            catch (Exception)
            { }
        }

        private void btngetall_KeyDown(object sender, KeyEventArgs e)
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
                    this.ActiveControl = GvcustomerInfo;
            }
            catch (Exception)
            { }
        }

        private void GvcustomerInfo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbMechanic;
            }
            catch (Exception)
            { }
        }

        private void txtcreditdays_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtcreditdays.Text == "")
                {
                    txtcreditdays.Text = "0";
                    this.ActiveControl = txtcreditdays;
                }
            }
            catch (Exception)
            { }
        }

        private void txtcustomercode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtcustomercode.Text != "")
                {
                    CustomerRepository customerRepo1 = new CustomerRepository();
                    var custdata = customerRepo1.GetAll().Where(t => t.CustomerCode == txtcustomercode.Text && t.CustomerCode != "").FirstOrDefault();
                    if (custdata != null)
                    {
                        MessageBox.Show("Already save this code");
                        txtcustomercode.Text = "";
                        this.ActiveControl = txtcustomercode;
                    }
                }
            }
            catch (Exception) { }
        }

        private void txtopeningBal_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtopeningBal.Text != "0" && txtopeningBal.Text != "0.00")
                {
                    lblopeningBal.Visible = true;
                    dtpOpeningBalanceDate.Visible = true;
                }
                else
                {
                    lblopeningBal.Visible = false;
                    dtpOpeningBalanceDate.Visible = false;
                }
            }
            catch (Exception)
            { }
        }

        private void txttinno_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txttinno.Text != "")
                {
                    lblgstinaddesddate.Visible = true;
                    dtpGSTINaddeddate.Visible = true;
                }
                else
                {
                    lblgstinaddesddate.Visible = false;
                    dtpGSTINaddeddate.Visible = false;
                }
            }
            catch (Exception)
            { }
        }

        private void txtcustomercode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbMechanic;
            }
            catch (Exception)
            { }
        }

        private void txtcreditdays_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtopeningBal;
            }
            catch (Exception) { }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                rdbBonusInPer.Visible = true;
                rdbBonusInRs.Visible = true;
                txtBonusInPer.Visible = true;
                txtBonusInRs.Visible = true;
                txtBonusInPer.Enabled = true;
                txtBonusInRs.Enabled = true;
                rdbBonusInPer.Enabled = true;
                rdbBonusInRs.Enabled = true;
            }
            catch (Exception)
            { }
        }

        private void txtBonusInRs_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBonusInRs.Text != "")
                { }
                else
                {
                    txtBonusInRs.Text = "0";
                    this.ActiveControl = txtBonusInRs;
                }
            }
            catch (Exception)
            { }
        }

        private void txtBonusInPer_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtBonusInPer.Text != "")
                { }
                else
                {
                    txtBonusInPer.Text = "0";
                    this.ActiveControl = txtBonusInPer;
                }
            }
            catch (Exception)
            { }
        }

        private void rdbBonusInRs_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbBonusInRs.Checked == true)
                {
                    txtBonusInPer.Enabled = false;
                    txtBonusInRs.Enabled = true;
                }
                else
                    txtBonusInRs.Enabled = false;
            }
            catch (Exception)
            { }
        }

        private void rdbBonusInPer_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbBonusInPer.Checked == true)
                {
                    txtBonusInRs.Enabled = false;
                    txtBonusInPer.Enabled = true;
                }
                else
                    txtBonusInPer.Enabled = false;
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
                adp = new SqlDataAdapter("Select CustomerName,CustomeAddress,MobileNo,BuyerTinNo from CustomerInformation where CompId='" + CommonMethod.CompId + "'", db.Connection);
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
                excelSheet.Cells[1, 1] = "Customer Name";//"GROUP_NAME";
                excelSheet.Cells[1, 2] = "Customer Address";
                excelSheet.Cells[1, 3] = "Mobile No";
                excelSheet.Cells[1, 4] = "GSTIn No";//"MARATHI";
                excelSheet.Columns.AutoFit();
                excelworkBook.SaveAs("AddCustomerInfo.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                excelworkBook.Close(true, misValue, misValue);
                excel.Quit();
                releaseObject(excelSheet);
                releaseObject(excelworkBook);
                releaseObject(excel);
                MessageBox.Show("Excel file created , you can find the file C:\\Users\\Documents\\AddCustomerInfo.xls");
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
                            maxcustomerid();
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
                            string startYear = transactionyear.Split('-')[0];
                            DateTime gstInDate = CommonMethod.startdate;
                            System.Windows.Forms.Application.DoEvents();
                            progressBar1.Value = i;
                            string a = Convert.ToString(dtexcel.Rows[i][0].ToString());
                            if (!string.IsNullOrEmpty(Convert.ToString(dtexcel.Rows[i][0].ToString())))
                            {
                                CustomerRepository custRepo = new CustomerRepository();
                                CustomerInformation custData = new CustomerInformation();
                                CustomerInformation custData1 = new CustomerInformation();
                                custData1 = custRepo.GetAll().Where(t => t.CustomerName.Trim().ToLower() == a.Trim().ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (custData1 != null)
                                {
                                    custData1.CustomerName = a.Trim();
                                    custData1.CustomeAddress = Convert.ToString(dtexcel.Rows[i][1].ToString());
                                    custData1.Mobileno = Convert.ToString(dtexcel.Rows[i][2].ToString());

                                    if (dtexcel.Rows[i][3].ToString() != null || Convert.ToString(dtexcel.Rows[i][3].ToString()) != "")
                                    {
                                        custData1.GSTINAddedDate = gstInDate;
                                        custData1.BuyerTinNo = Convert.ToString(dtexcel.Rows[i][3].ToString());
                                    }
                                    custData1.CustomerType = "Retailer";
                                    CompanyInformationRepository cRepo = new CompanyInformationRepository();
                                    var cinfo = cRepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();
                                    if (cinfo != null)
                                    {
                                        custData1.State = cinfo.State;
                                        custData1.Statecode = cinfo.StateCode;
                                    }
                                    custData1.CompId = CommonMethod.CompId;
                                    custRepo.Edit(custData1);
                                    custRepo.Save();
                                }
                                else
                                {
                                    maxcustomerid();
                                    custData.CustomerID = Convert.ToInt32(txtCustomerID.Text);
                                    custData.CustomerName = a.Trim();
                                    custData.CustomeAddress = Convert.ToString(dtexcel.Rows[i][1].ToString());
                                    custData.Mobileno = Convert.ToString(dtexcel.Rows[i][2].ToString());
                                    if (dtexcel.Rows[i][3].ToString() != null || Convert.ToString(dtexcel.Rows[i][3].ToString()) != "")
                                    {
                                        custData.GSTINAddedDate = gstInDate;
                                        custData.BuyerTinNo = Convert.ToString(dtexcel.Rows[i][3].ToString());
                                    }
                                    custData.CustomerType = "Retailer";
                                    CompanyInformationRepository cRepo = new CompanyInformationRepository();
                                    var cinfo = cRepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();
                                    if (cinfo != null)
                                    {
                                        custData.State = cinfo.State;
                                        custData.Statecode = cinfo.StateCode;
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

        private void GvcustomerInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Customerid = Convert.ToInt32(GvcustomerInfo.Rows[e.RowIndex].Cells["ID"].Value);
                txtCustomerID.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["CustomercID"].Value);
                txtCustomerName.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["CustomerName"].Value);
                txtCustomerAddress.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["Address"].Value);
                txtmobileno.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["MobileNo"].Value);
                txtlandlineNo.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["LandlineNo"].Value);
                txttinno.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["TINNo"].Value);
                txtvillage.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["Village"].Value);
                txtFirmName.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["FirmName"].Value);
                if (Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["BirthDate"].Value) == "")
                    dtpBirthDate.Value = dtpBirthDate.Value;
                else
                    dtpBirthDate.Value = Convert.ToDateTime(GvcustomerInfo.Rows[e.RowIndex].Cells["BirthDate"].Value);
                if (Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["CustType"].Value) == "")
                    cmbCustomerType.SelectedIndex = cmbCustomerType.FindStringExact("Retailer");
                else
                    cmbCustomerType.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["CustType"].Value);
                txtPanno.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["PANNo"].Value);
                cmbState.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["State"].Value);
                txtAdharCardNo.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["AdharCardNo"].Value);
                var Mistriid = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["MechanicId"].Value);
                if (Mistriid == "")
                    cmbMechanic.Text = "";
                else
                {
                    CustomerRepository custrepo = new CustomerRepository();
                    var Mistridata = custrepo.GetAll().Where(t => t.MistriCode == Convert.ToInt32(Mistriid) && t.CustomerID == Convert.ToInt32(GvcustomerInfo.Rows[e.RowIndex].Cells["CustomercID"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    cmbMechanic.Text = Mistridata.MistriName;
                    custrepo.Dispose();
                }
                txtName.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["MarathiName"].Value);
                txtAdd.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["MarathiAddress"].Value);
                if (!string.IsNullOrEmpty(Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["CreditDays"].Value)))
                    txtcreditdays.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["CreditDays"].Value);
                else
                    txtcreditdays.Text = "0";
                txtcustomercode.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["CustomerCode"].Value);
                //RouteRepository rRepo = new RouteRepository();
                //string routeName = rRepo.GetAll().Where(t => t.RouteName == Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["RouteId"].Value)).FirstOrDefault().RouteName;
                //rRepo.Dispose();

                cmbRouteName.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["RouteId"].Value);
                txtFssaiNo.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["FssaiNo"].Value);
                try
                {
                    txtBusinessName.Text = GvcustomerInfo.Rows[e.RowIndex].Cells["BusinessName"].Value.ToString();
                }
                catch (Exception)
                { }                
                //if (Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["OpeningDate"].Value) == "")
                //    dtpOpeningBalanceDate.Value = DateTime.Now.Date;
                //else
                //    dtpOpeningBalanceDate.Value = Convert.ToDateTime(GvcustomerInfo.Rows[e.RowIndex].Cells["OpeningDate"].Value);
                if (Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["GSTINDate"].Value) == "")
                    dtpGSTINaddeddate.Value = DateTime.Now.Date;
                else
                {
                    try
                    {
                        dtpGSTINaddeddate.Value = Convert.ToDateTime(GvcustomerInfo.Rows[e.RowIndex].Cells["GSTINDate"].Value);
                    }
                    catch (Exception)
                    {
                        tblTransactionYearDatesRepository Repo = new tblTransactionYearDatesRepository();
                        dtpGSTINaddeddate.Value = Convert.ToDateTime(CommonMethod.startdate);
                        //dtpGSTINaddeddate.Value = Convert.ToDateTime(Repo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().StartDate);
                    }
                }
                if (Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["BonusIn"].Value) == "Bonus In Rs" || Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["BonusIn"].Value) == "Bonus In Per")
                {
                    rdbBonusInPer.Visible = true;
                    rdbBonusInRs.Visible = true;
                    txtBonusInPer.Visible = true;
                    txtBonusInRs.Visible = true;
                    if (Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["BonusIn"].Value) == "Bonus In Rs")
                    {
                        rdbBonusInRs.Checked = true;
                        txtBonusInRs.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["BonusInRsNPer"].Value);
                    }
                    else if (Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["BonusIn"].Value) == "Bonus In Per")
                    {
                        rdbBonusInPer.Checked = true;
                        txtBonusInPer.Text = Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["BonusInRsNPer"].Value);
                    }
                }
                RemainingPaymentRepository paymentrepo = new RemainingPaymentRepository();
                try
                {
                    RemainingPayment PayData = new RemainingPayment();
                    PayData = paymentrepo.GetAll().Where(t => t.Type == "OpeningBalance" && t.CustomerId == Convert.ToInt32(GvcustomerInfo.Rows[e.RowIndex].Cells["CustomercID"].Value) && t.TransactionYear == CommonMethod.TransactionYear && (t.CompId == CommonMethod.CompId)).FirstOrDefault();
                    //decimal OpeAmt = Convert.ToDecimal(paymentrepo.GetAll().Where(t => t.Type == "OpeningBalance" && t.CustomerId == Convert.ToInt32(GvcustomerInfo.Rows[e.RowIndex].Cells["CustomercID"].Value) && t.TransactionYear == CommonMethod.TransactionYear && (t.CompId == CommonMethod.CompId)).FirstOrDefault().NetAmt);
                    decimal OpeAmt = Convert.ToDecimal(PayData.NetAmt);
                    txtopeningBal.Text = Convert.ToString(OpeAmt);
                    if (OpeAmt != 0)
                        dtpOpeningBalanceDate.Value = Convert.ToDateTime(PayData.CreateDate);
                    else
                    {
                        if (Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["OpeningDate"].Value) == "")
                            dtpOpeningBalanceDate.Value = DateTime.Now.Date;
                        else
                            dtpOpeningBalanceDate.Value = Convert.ToDateTime(GvcustomerInfo.Rows[e.RowIndex].Cells["OpeningDate"].Value);
                    }
                }
                catch (Exception)
                {
                    txtopeningBal.Text = "0";
                    try
                    {
                        if (Convert.ToString(GvcustomerInfo.Rows[e.RowIndex].Cells["OpeningDate"].Value) == "")
                            dtpOpeningBalanceDate.Value = DateTime.Now.Date;
                        else
                            dtpOpeningBalanceDate.Value = Convert.ToDateTime(GvcustomerInfo.Rows[e.RowIndex].Cells["OpeningDate"].Value);
                    }
                    catch (Exception)
                    { dtpOpeningBalanceDate.Value = DateTime.Now.Date; }
                }
                try
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPremainingpayment", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                    command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "CustRem";
                    command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = Convert.ToInt32(GvcustomerInfo.Rows[e.RowIndex].Cells["CustomercID"].Value);

                    DataTable dt1 = new DataTable();
                    dt1.Load(command.ExecuteReader());
                    object sumtotAmount = new object();
                    sumtotAmount = dt1.Compute("Sum(TotalReaminingAmount)", "");
                    db.CloseConnection();
                    lblAmt.Visible = true;
                    lblRemAmt.Visible = true;
                    lblAmt.Text = Convert.ToString(sumtotAmount);
                    dt1.Dispose();
                }
                catch (Exception)
                { db.CloseConnection(); }
                btnSave.Text = "Update";
                btndelete.Enabled = true;
                linkLabel1.Visible = true;
                this.ActiveControl = cmbMechanic;
                paymentrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnMechForm_Click(object sender, EventArgs e)
        {
            try
            {
                frmMistri mistriinfo = new frmMistri();
                mistriinfo.ShowDialog();
                fillMistriCombo();
            }
            catch (Exception)
            { }
        }

        private void btnMechForm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtCustomerName;
            }
            catch (Exception)
            { }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    string strcheck = "F";
                    if (Customerid != 0)
                    {
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPCheckforIdUsedorNot", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "Customer";
                            command.Parameters.Add("@Id", SqlDbType.VarChar).Value = Customerid;
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
                            MessageBox.Show("Customer name is Used. You Can Not Delete.", "Warning");
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
    }
}