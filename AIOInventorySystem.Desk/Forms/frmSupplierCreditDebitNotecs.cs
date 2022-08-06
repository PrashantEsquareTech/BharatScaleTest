using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmSupplierCreditDebitNotecs : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        public decimal rdoff = 1;
        public int purmasterid, printId, pocolIndex = -1, intamtchk;
        public string cdtype;
        public static frmPurchaseReturn custbillinfo;
        public static frmSupplierCreditDebitNotecs suppcreditdebit;
        List<int> deleteid = new List<int>();
        List<PurchaseDetail> PurDPredata = new List<PurchaseDetail>();

        public frmSupplierCreditDebitNotecs()
        {
            InitializeComponent();
        }

        public frmSupplierCreditDebitNotecs(string type)
        {
            InitializeComponent();
            cdtype = type;
            if (cdtype == "CN")
                label5.Text = "Credit Note";
            else if (cdtype == "DN")
                label5.Text = "Debit Note";
            maxbillid();
            fillgst();
            customerdata();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            PurDPredata.Clear();
            cm.changedatetimepickerrange(this);
        }

        public frmSupplierCreditDebitNotecs(int PorderNo)
        {
            InitializeComponent();
            maxbillid();
            fillgst();
            customerdata();
            if (cdtype == "CN")
                label5.Text = "Credit Note";
            else if (cdtype == "DN")
                label5.Text = "Debit Note";
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            PurDPredata.Clear();
            cm.changedatetimepickerrange(this);
            LoadData(PorderNo);
        }

        public void maxbillid()
        {
            try
            {
                SuppCreditDebitNoteMasterRepository PRMRepo = new SuppCreditDebitNoteMasterRepository();
                int maxid = Convert.ToInt32(PRMRepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.SuppCreditDebitNo));
                if (maxid == 0)
                    txtPRNo.Text = "1";
                else
                    txtPRNo.Text = Convert.ToString(maxid + 1);
                PRMRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void customerdata()
        {
            try
            {
                SupplierRepository supplierrepo = new SupplierRepository();
                List<SupplierInformation> sdata = new List<SupplierInformation>();
                sdata.Clear();
                if (CommonMethod.commProduct == true)
                    sdata = supplierrepo.GetAll().OrderByDescending(t => t.SupplierName).ToList();
                else
                    sdata = supplierrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.SupplierName).ToList();
                sdata.Add(new SupplierInformation { SupplierName = "Select", SupplierCode = 0 });
                sdata.Reverse();
                cmbsuppliername.DataSource = sdata;
                cmbsuppliername.ValueMember = "SupplierCode";
                cmbsuppliername.DisplayMember = "SupplierName";
                supplierrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillgst()
        {
            try
            {
                DataTable dtsgst = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforSnCGST='Y'");
                if (dtsgst.Rows.Count > 0)
                {
                    cmbsgst.ValueMember = "GSTNo";
                    cmbsgst.DisplayMember = "GSTPercent";
                    cmbsgst.DataSource = dtsgst;
                }

                DataTable dtcgst = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforSnCGST='Y'");
                if (dtcgst.Rows.Count > 0)
                {
                    cmbcgst.ValueMember = "GSTNo";
                    cmbcgst.DisplayMember = "GSTPercent";
                    cmbcgst.DataSource = dtcgst;
                }

                DataTable dtigst = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforIGST='Y'");
                if (dtigst.Rows.Count > 0)
                {
                    cmbigst.ValueMember = "GSTNo";
                    cmbigst.DisplayMember = "GSTPercent";
                    cmbigst.DataSource = dtigst;
                }

            }
            catch (Exception) { }
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
                if (btnsave.Enabled == true)
                {
                    btnsave_Click(null, null);
                    return true;
                }
                else if (btnUpdate.Enabled == true)
                {
                    btnUpdate_Click(null, null);
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmSupplierCreditDebitNotecs_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                txtporderno.Text = "";
                string cnString2 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString2))
                {
                    SqlCommand cmd = new SqlCommand("SELECT PorderNo FROM PurchaseMaster where TransactionYear = '" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        {
                            MyCollection.Add(reader.GetInt32(0).ToString());
                        }
                        catch (Exception)
                        { }
                    }
                    txtporderno.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    cmd.Dispose();
                    reader.Dispose();
                }

                txtpname.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = null;
                    if (CommonMethod.commProduct == true)
                        cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation ", con);
                    else
                        cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where compid=" + CommonMethod.CompId, con);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        {
                            MyCollection.Add(reader.GetString(0) + "," + reader.GetString(1) + "," + reader.GetString(2));
                        }
                        catch (Exception)
                        { }
                    }
                    txtpname.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    cmd.Dispose();
                    reader.Dispose();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbsuppliername.Text != "Select")
                {
                    if (txtamount.Text != "" && cmbReason.Text != "")
                    {
                        if (pocolIndex != -1)
                        {
                            int i = gvpurreturnproducts.RowCount;
                            decimal sgstamt = 0, cgstamt = 0, igstamt = 0, cessamt = 0, discamt = 0, spcdiscamt = 0, othdiscamt = 0;
                            try
                            {
                                string productname = txtpname.Text.Trim().Split(',')[0];
                                string company = txtpname.Text.Trim().Split(',')[1];
                                string unit = txtpname.Text.Trim().Split(',')[2];
                                string NewReason = cmbReason.Text;
                                int poid = Convert.ToInt32(GvProductInfo.Rows[pocolIndex].Cells["Id"].Value);
                                for (int k = 0; k < gvpurreturnproducts.Rows.Count; k++)
                                {
                                    string proname = Convert.ToString(gvpurreturnproducts.Rows[k].Cells["PRProductName"].Value);
                                    string compname = Convert.ToString(gvpurreturnproducts.Rows[k].Cells["PRCompany"].Value);
                                    string unitname = Convert.ToString(gvpurreturnproducts.Rows[k].Cells["PRUnit"].Value);
                                    int prpoid = Convert.ToInt32(gvpurreturnproducts.Rows[k].Cells["PRPOID"].Value);
                                    string returnReason = Convert.ToString(gvpurreturnproducts.Rows[k].Cells["PRReason"].Value);

                                    if (productname == proname && company == compname && unit == unitname && poid == prpoid && NewReason.Trim() == returnReason.Trim())
                                    {
                                        gvpurreturnproducts.Rows[k].Cells["PRProductName"].Value = productname;
                                        gvpurreturnproducts.Rows[k].Cells["PRCompany"].Value = company;
                                        gvpurreturnproducts.Rows[k].Cells["PRUnit"].Value = unit;
                                        gvpurreturnproducts.Rows[k].Cells["PRPurUnit"].Value = cmbUnit.Text;
                                        gvpurreturnproducts.Rows[k].Cells["PRAmount"].Value = txtamount.Text;
                                        decimal amt = Convert.ToDecimal(txtamount.Text);

                                        if (txtpdiscount.Text != "")
                                        {
                                            discamt = amt * Convert.ToDecimal(txtpdiscount.Text) / 100;
                                            gvpurreturnproducts.Rows[k].Cells["PRDisc"].Value = txtpdiscount.Text;
                                        }
                                        else
                                        {
                                            discamt = 0;
                                            gvpurreturnproducts.Rows[k].Cells["PRDisc"].Value = 0;
                                        }
                                        if (txtpSpcDisc.Text != "")
                                        {
                                            spcdiscamt = (amt - discamt) * Convert.ToDecimal(txtpSpcDisc.Text) / 100;
                                            gvpurreturnproducts.Rows[k].Cells["PRSpcDisc"].Value = txtpSpcDisc.Text;
                                        }
                                        else
                                        {
                                            spcdiscamt = 0;
                                            gvpurreturnproducts.Rows[k].Cells["PRSpcDisc"].Value = txtpSpcDisc.Text;
                                        }
                                        if (txtOthDisc.Text != "")
                                        {
                                            othdiscamt = (amt - discamt - spcdiscamt) * Convert.ToDecimal(txtOthDisc.Text) / 100;
                                            gvpurreturnproducts.Rows[k].Cells["PR3LevelDisc"].Value = txtOthDisc.Text;
                                        }
                                        amt = amt - discamt - spcdiscamt - othdiscamt;
                                        if (cmbsgst.Text == "")
                                        {
                                            gvpurreturnproducts.Rows[k].Cells["PRSGST"].Value = 0;
                                            gvpurreturnproducts.Rows[k].Cells["PRSGSTAmt"].Value = 0;
                                        }
                                        else
                                        {
                                            gvpurreturnproducts.Rows[k].Cells["PRSGST"].Value = cmbsgst.Text;
                                            sgstamt = amt * Convert.ToDecimal(cmbsgst.Text) / 100;
                                            gvpurreturnproducts.Rows[k].Cells["PRSGSTAmt"].Value = sgstamt;
                                        }
                                        if (cmbcgst.Text == "")
                                        {
                                            gvpurreturnproducts.Rows[k].Cells["PRCGST"].Value = 0;
                                            gvpurreturnproducts.Rows[k].Cells["PRCGSTAmt"].Value = 0;
                                        }
                                        else
                                        {
                                            gvpurreturnproducts.Rows[k].Cells["PRCGST"].Value = cmbcgst.Text;
                                            cgstamt = amt * Convert.ToDecimal(cmbcgst.Text) / 100;
                                            gvpurreturnproducts.Rows[k].Cells["PRCGSTAmt"].Value = cgstamt;
                                        }
                                        if (cmbigst.Text == "")
                                        {
                                            gvpurreturnproducts.Rows[k].Cells["PRIGST"].Value = 0;
                                            gvpurreturnproducts.Rows[k].Cells["PRIGSTAmt"].Value = 0;
                                        }
                                        else
                                        {
                                            gvpurreturnproducts.Rows[k].Cells["PRIGST"].Value = cmbigst.Text;
                                            igstamt = amt * Convert.ToDecimal(cmbigst.Text) / 100;
                                            gvpurreturnproducts.Rows[k].Cells["PRIGSTAmt"].Value = igstamt;
                                        }
                                        if (txtpCess.Text == "")
                                        {
                                            gvpurreturnproducts.Rows[k].Cells["PRCess"].Value = 0;
                                            gvpurreturnproducts.Rows[k].Cells["PRCessAmt"].Value = 0;
                                        }
                                        else
                                        {
                                            gvpurreturnproducts.Rows[k].Cells["PRCess"].Value = txtpCess.Text;
                                            cessamt = amt * Convert.ToDecimal(txtpCess.Text) / 100;
                                            gvpurreturnproducts.Rows[k].Cells["PRCessAmt"].Value = cessamt;
                                        }
                                        gvpurreturnproducts.Rows[k].Cells["PRTotalAmt"].Value = txtpTotalAmt.Text;
                                        gvpurreturnproducts.Rows[k].Cells["PRReason"].Value = cmbReason.Text;
                                        gvpurreturnproducts.Rows[k].Cells["PRRemove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                        gvpurreturnproducts.Rows[k].Cells["PRPOID"].Value = GvProductInfo.Rows[pocolIndex].Cells["Id"].Value;
                                        txtTotalAmt.Text = Convert.ToString(gvpurreturnproducts.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["PRTotalAmt"].Value)));
                                        txtpname.Text = "";
                                        cmbUnit.Items.Clear();
                                        txtamount.Text = "";
                                        fillgst();
                                        txtpCess.Text = "0";
                                        txtpTotalAmt.Text = "0";
                                        cmbReason.SelectedIndex = -1;
                                        txtpdiscount.Text = "0";
                                        txtpSpcDisc.Text = "0";
                                        txtOthDisc.Text = "0";
                                        txtTaxableAmt.Text = "0";
                                        goto a;
                                    }
                                }
                            b:
                                {
                                    gvpurreturnproducts.Rows.Add();
                                    gvpurreturnproducts.Rows[i].Cells["PRProductName"].Value = productname;
                                    gvpurreturnproducts.Rows[i].Cells["PRCompany"].Value = company;
                                    gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value = unit;
                                    gvpurreturnproducts.Rows[i].Cells["PRPurUnit"].Value = cmbUnit.Text;
                                    gvpurreturnproducts.Rows[i].Cells["PRAmount"].Value = txtamount.Text;
                                    decimal amt = Convert.ToDecimal(txtamount.Text);

                                    if (txtpdiscount.Text != "")
                                    {
                                        discamt = amt * Convert.ToDecimal(txtpdiscount.Text) / 100;
                                        gvpurreturnproducts.Rows[i].Cells["PRDisc"].Value = txtpdiscount.Text;
                                    }
                                    else
                                    {
                                        discamt = 0;
                                        gvpurreturnproducts.Rows[i].Cells["PRDisc"].Value = 0;
                                    }
                                    if (txtpSpcDisc.Text != "")
                                    {
                                        spcdiscamt = (amt - discamt) * Convert.ToDecimal(txtpSpcDisc.Text) / 100;
                                        gvpurreturnproducts.Rows[i].Cells["PRSpcDisc"].Value = txtpSpcDisc.Text;
                                    }
                                    else
                                    {
                                        spcdiscamt = 0;
                                        gvpurreturnproducts.Rows[i].Cells["PRSpcDisc"].Value = txtpSpcDisc.Text;
                                    }
                                    if (txtOthDisc.Text != "")
                                    {
                                        othdiscamt = (amt - discamt - spcdiscamt) * Convert.ToDecimal(txtOthDisc.Text) / 100;
                                        gvpurreturnproducts.Rows[i].Cells["PR3LevelDisc"].Value = txtOthDisc.Text;
                                    }
                                    amt = amt - discamt - spcdiscamt - othdiscamt;

                                    if (cmbsgst.Text == "")
                                    {
                                        gvpurreturnproducts.Rows[i].Cells["PRSGST"].Value = 0;
                                        gvpurreturnproducts.Rows[i].Cells["PRSGSTAmt"].Value = 0;
                                    }
                                    else
                                    {
                                        gvpurreturnproducts.Rows[i].Cells["PRSGST"].Value = cmbsgst.Text;
                                        sgstamt = amt * Convert.ToDecimal(cmbsgst.Text) / 100;
                                        gvpurreturnproducts.Rows[i].Cells["PRSGSTAmt"].Value = sgstamt;
                                    }
                                    if (cmbcgst.Text == "")
                                    {
                                        gvpurreturnproducts.Rows[i].Cells["PRCGST"].Value = 0;
                                        gvpurreturnproducts.Rows[i].Cells["PRCGSTAmt"].Value = 0;
                                    }
                                    else
                                    {
                                        gvpurreturnproducts.Rows[i].Cells["PRCGST"].Value = cmbcgst.Text;
                                        cgstamt = amt * Convert.ToDecimal(cmbcgst.Text) / 100;
                                        gvpurreturnproducts.Rows[i].Cells["PRCGSTAmt"].Value = cgstamt;
                                    }
                                    if (cmbigst.Text == "")
                                    {
                                        gvpurreturnproducts.Rows[i].Cells["PRIGST"].Value = 0;
                                        gvpurreturnproducts.Rows[i].Cells["PRIGSTAmt"].Value = 0;
                                    }
                                    else
                                    {
                                        gvpurreturnproducts.Rows[i].Cells["PRIGST"].Value = cmbigst.Text;
                                        igstamt = amt * Convert.ToDecimal(cmbigst.Text) / 100;
                                        gvpurreturnproducts.Rows[i].Cells["PRIGSTAmt"].Value = igstamt;
                                    }
                                    if (txtpCess.Text == "")
                                    {
                                        gvpurreturnproducts.Rows[i].Cells["PRCess"].Value = 0;
                                        gvpurreturnproducts.Rows[i].Cells["PRCessAmt"].Value = 0;
                                    }
                                    else
                                    {
                                        gvpurreturnproducts.Rows[i].Cells["PRCess"].Value = txtpCess.Text;
                                        cessamt = amt * Convert.ToDecimal(txtpCess.Text) / 100;
                                        gvpurreturnproducts.Rows[i].Cells["PRCessAmt"].Value = cessamt;
                                    }
                                    gvpurreturnproducts.Rows[i].Cells["PRTotalAmt"].Value = txtpTotalAmt.Text;
                                    gvpurreturnproducts.Rows[i].Cells["PRReason"].Value = cmbReason.Text;
                                    gvpurreturnproducts.Rows[i].Cells["PRRemove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                    gvpurreturnproducts.Rows[i].Cells["PRPOID"].Value = GvProductInfo.Rows[pocolIndex].Cells["Id"].Value;
                                    txtTotalAmt.Text = Convert.ToString(gvpurreturnproducts.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["PRTotalAmt"].Value)));
                                    txtpname.Text = "";
                                    cmbUnit.Items.Clear();
                                    txtamount.Text = "";
                                    fillgst();
                                    txtpCess.Text = "0";
                                    txtpTotalAmt.Text = "0";
                                    txtpdiscount.Text = "0";
                                    txtpSpcDisc.Text = "0";
                                    txtOthDisc.Text = "0";
                                    txtTaxableAmt.Text = "0";
                                    cmbReason.SelectedIndex = -1;
                                }
                            a: { }
                            }
                            catch (Exception)
                            { }
                        }
                    }
                    else
                    {
                        if (txtamount.Text == "")
                        {
                            MessageBox.Show("Enter Amount.", "Warning");
                            this.ActiveControl = txtamount;
                        }
                        else
                        {
                            MessageBox.Show("Select Reason.", "Warning");
                            this.ActiveControl = cmbReason;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Enter Supplier name.", "Warning");
                    this.ActiveControl = cmbsuppliername;
                }
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                maxbillid();
                txtPOPaidAmt.Enabled = false;
                cmbcgst.Enabled = false;
                txtpTotalAmt.Enabled = false;
                btnsave.Enabled = true;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                txtPOtotalamt.Text = "0";
                txtPODisc.Text = "0";
                txtDiscount.Text = "0";
                txtpdiscount.Text = "0";
                txtpSpcDisc.Text = "0";
                txtOthDisc.Text = "0";
                txtPOTransChrg.Text = "0";
                txtPOPaidAmt.Text = "0";
                txtPORemAmt.Text = "0";
                txtpname.Text = "";
                cmbUnit.Items.Clear();
                txtDiscount.Text = "0";
                customerdata();
                fillgst();
                txtTotalAmt.Text = "0";
                GvProductInfo.Rows.Clear();
                gvpurreturnproducts.Rows.Clear();
                ttnetamt.Text = "0";
                btnsave.Text = "Save";
                txtporderno.Text = "";
                txtsupplierpono.Text = "";
                txtpTotalAmt.Text = "";
                txtamount.Text = "0";
                PurDPredata.Clear();
            }
            catch (Exception)
            { }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                this.ActiveControl = txtporderno;
            }
            catch (Exception)
            { }
        }

        public void savemaster()
        {
            try
            {
                SuppCreditDebitNoteMasterRepository PRMRepo = new SuppCreditDebitNoteMasterRepository();
                SuppCreditDebitMaster PRMData = new SuppCreditDebitMaster();
                maxbillid();
                PRMData.SuppCreditDebitNo = Convert.ToInt32(txtPRNo.Text);
                PRMData.SuppCreditDebitDate = dtpPRDate.Value;
                PRMData.PorderNo = Convert.ToInt32(txtporderno.Text);
                PRMData.PorderDate = dtpPorderdate.Value;
                PRMData.SupplierpoNo = Convert.ToString(txtsupplierpono.Text);
                PRMData.SupplierId = Convert.ToInt32(cmbsuppliername.SelectedValue);
                PRMData.Suppliername = cmbsuppliername.Text;
                PRMData.TotalAmount = Convert.ToDecimal(txtTotalAmt.Text);
                if (txtDiscount.Text == "")
                    PRMData.Discount = Convert.ToDecimal(0);
                else
                    PRMData.Discount = Convert.ToDecimal(txtDiscount.Text);
                if (txtSpDisc.Text == "")
                    PRMData.SpcDiscount = Convert.ToDecimal(0);
                else
                    PRMData.SpcDiscount = Convert.ToDecimal(txtSpDisc.Text);
                PRMData.NetAmount = Convert.ToDecimal(ttnetamt.Text);
                PRMData.TransactionYear = CommonMethod.TransactionYear;
                PRMData.CompId = CommonMethod.CompId;
                if (cdtype == "CN")
                    PRMData.CDType = "Credit Note";
                else if (cdtype == "DN")
                    PRMData.CDType = "Debit Note";
                PRMRepo.Add(PRMData);
                PRMRepo.Save();
                printId = Convert.ToInt32(PRMData.SuppCreditDebitNo);
                purmasterid = Convert.ToInt32(PRMData.Id);
                PRMRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void savedetail(int i)
        {
            try
            {
                SuppCreditDebitDetail PRDData = new SuppCreditDebitDetail();
                SuppCreditDebitNoteDetailRepository PRDRepo = new SuppCreditDebitNoteDetailRepository();
                PRDData.SuppCreditDebitNo = purmasterid;
                PRDData.ProductName = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRProductName"].Value);
                PRDData.CompanyName = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRCompany"].Value);
                PRDData.Unit = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value);
                PRDData.PurchaseUnit = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRPurUnit"].Value);
                PRDData.Amount = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRAmount"].Value);
                if (gvpurreturnproducts.Rows[i].Cells["PRDisc"].Value == null)
                    PRDData.Discount = 0;
                else
                    PRDData.Discount = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRDisc"].Value);
                if (gvpurreturnproducts.Rows[i].Cells["PRSpcDisc"].Value == null)
                    PRDData.SpcDiscount = 0;
                else
                    PRDData.SpcDiscount = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRSpcDisc"].Value);
                if (gvpurreturnproducts.Rows[i].Cells["PR3LevelDisc"].Value == null)
                    PRDData.ThreeLevelDicount = 0;
                else
                    PRDData.ThreeLevelDicount = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PR3LevelDisc"].Value);
                PRDData.SGstPercent = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRSGST"].Value);
                PRDData.SGstAmt = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRSGSTAmt"].Value);
                PRDData.CGstPercent = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRCGST"].Value);
                PRDData.CGstAmt = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRCGSTAmt"].Value);
                PRDData.IGstPercent = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRIGST"].Value);
                PRDData.IGstAmt = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRIGSTAmt"].Value);
                PRDData.Cess = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRCess"].Value);
                PRDData.CessAmt = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRCessAmt"].Value);
                PRDData.TotalAmount = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRTotalAmt"].Value);
                PRDData.Reason = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRReason"].Value);
                PRDData.POID = Convert.ToInt32(gvpurreturnproducts.Rows[i].Cells["PRPOID"].Value);
                PRDData.AssemblyChildStatus = "N";
                PRDRepo.Add(PRDData);
                PRDRepo.Save();
                PRDRepo.Dispose();
                int BDetailId = Convert.ToInt32(PRDData.POID);
                string Reason = Convert.ToString(PRDData.Reason);
                try
                {
                    ProductRepository productRepo = new ProductRepository();
                    var pData = productRepo.GetAll().Where(t => t.ProductName == Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRProductName"].Value) && t.ManufactureCompany == Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRCompany"].Value) && t.Uniti == Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (pData != null)
                    {
                        AssemeblyProductDetailsRepository assemblyDRepo = new AssemeblyProductDetailsRepository();
                        List<tblAssemblyProductDetail> assemblyDList = assemblyDRepo.GetAll().Where(t => t.AssemblyProductId == pData.ID && t.CompId == CommonMethod.CompId).ToList();
                        if (assemblyDList.Count > 0)
                        {
                            foreach (var itemAssembly in assemblyDList)
                            {
                                SuppCreditDebitDetail PRDData1 = new SuppCreditDebitDetail();
                                SuppCreditDebitNoteDetailRepository PRDRepo1 = new SuppCreditDebitNoteDetailRepository();
                                PRDData1.SuppCreditDebitNo = purmasterid;
                                PRDData1.ProductName = Convert.ToString(itemAssembly.ProductName);
                                PRDData1.CompanyName = Convert.ToString(itemAssembly.CompanyName);
                                PRDData1.Unit = Convert.ToString(itemAssembly.Unit);
                                PRDData1.PurchaseUnit = Convert.ToString(itemAssembly.Unit);
                                PRDData1.Amount = Convert.ToDecimal(0);
                                PRDData1.ThreeLevelDicount = 0;
                                PRDData1.SGstPercent = Convert.ToDecimal(0);
                                PRDData1.SGstAmt = Convert.ToDecimal(0);
                                PRDData1.CGstPercent = Convert.ToDecimal(0);
                                PRDData1.CGstAmt = Convert.ToDecimal(0);
                                PRDData1.IGstPercent = Convert.ToDecimal(0);
                                PRDData1.IGstAmt = Convert.ToDecimal(0);
                                PRDData1.Cess = Convert.ToDecimal(0);
                                PRDData1.CessAmt = Convert.ToDecimal(0);
                                PRDData1.TotalAmount = Convert.ToDecimal(0);
                                PRDData1.Reason = Convert.ToString(0);
                                PurchaseDetailRepository custDRepo = new PurchaseDetailRepository();
                                var custData = custDRepo.GetAll().Where(t => t.Id == BDetailId && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                if (custData != null)
                                {
                                    custDRepo = new PurchaseDetailRepository();
                                    var custDetailData = custDRepo.GetAll().Where(t => t.PorderNo == custData.PorderNo && t.ProductName == itemAssembly.ProductName && t.CompanyName == itemAssembly.CompanyName && t.Unit == itemAssembly.Unit && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                                    if (custDetailData != null)
                                        PRDData1.POID = Convert.ToInt32(custDetailData.Id);
                                }
                                PRDData1.AssemblyChildStatus = "Y";
                                PRDRepo1.Add(PRDData1);
                                PRDRepo1.Save();
                                PRDRepo1.Dispose();
                            }
                        }
                    }
                }
                catch (Exception)
                { }
            }
            catch (Exception)
            { }
        }

        public void saveremainingpayment(string mode)
        {
            try
            {
                SupplierRemainingPaymentRepository sremainingpaymentrepo = new SupplierRemainingPaymentRepository();
                SupplierRemainingPayment remainingdata = new SupplierRemainingPayment();
                remainingdata.SupplierId = Convert.ToInt32(cmbsuppliername.SelectedValue);
                remainingdata.Suppliername = Convert.ToString(cmbsuppliername.Text);
                if (cdtype == "DN")
                {
                    remainingdata.NetAmt = 0;
                    remainingdata.PaidAmt = Convert.ToDecimal(ttnetamt.Text);
                    SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                    SupplierRemainingPayment remainingdata1 = new SupplierRemainingPayment();
                    remainingdata1 = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                    decimal decremamt = 0;
                    if (remainingdata1 != null)
                    {
                        decremamt = Convert.ToDecimal(remainingdata1.RemainingAmt);
                        remainingdata.RemainingAmt = decremamt - Convert.ToDecimal(ttnetamt.Text);
                    }
                    else
                        remainingdata.RemainingAmt = 0;
                    remainingdata.Type = "Debit Note";
                }
                else if (cdtype == "CN")
                {
                    remainingdata.NetAmt = Convert.ToDecimal(ttnetamt.Text);
                    remainingdata.PaidAmt = 0;
                    SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                    SupplierRemainingPayment remainingdata1 = new SupplierRemainingPayment();
                    remainingdata1 = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                    decimal decremamt = 0;
                    if (remainingdata1 != null)
                    {
                        decremamt = Convert.ToDecimal(remainingdata1.RemainingAmt);
                        remainingdata.RemainingAmt = decremamt + Convert.ToDecimal(ttnetamt.Text);
                    }
                    else
                        remainingdata.RemainingAmt = 0;
                    remainingdata.Type = "Credit Note";
                }
                remainingdata.BillId = Convert.ToInt32(txtporderno.Text);
                remainingdata.BillDate = Convert.ToDateTime(dtpPRDate.Value);
                remainingdata.CreateDate = Convert.ToDateTime(dtpPRDate.Value);
                remainingdata.PaymentMode = mode;
                remainingdata.TransactionYear = CommonMethod.TransactionYear;
                remainingdata.CompId = CommonMethod.CompId;
                sremainingpaymentrepo.Add(remainingdata);
                sremainingpaymentrepo.Save();
                sremainingpaymentrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                SuppCreditDebitNoteMasterRepository PRMRepo = new SuppCreditDebitNoteMasterRepository();
                var BillId = PRMRepo.GetAll().Where(t => t.SuppCreditDebitNo == Convert.ToInt32(txtPRNo.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (BillId != null)
                {
                    MessageBox.Show("Already Saved This Record.", "Warning");
                    this.ActiveControl = btnnew;
                }
                else
                {
                    if (PurDPredata.Count == 0)
                    {
                        if (txtporderno.Text != "" && (txtTotalAmt.Text != "" || txtTotalAmt.Text != "0") && gvpurreturnproducts.RowCount > 0)
                        {
                            savemaster();   //Start of PurchaseReturnMaster entry 
                            for (int i = 0; i < gvpurreturnproducts.Rows.Count; i++)
                            {
                                savedetail(i);//Start of PurchaseReturnDetail entry                            
                            }
                            saveremainingpayment("Cash");//Start of SupplierRemainingPayment entry    
                            gvpurreturnproducts.Rows.Clear();
                            MessageBox.Show("Debit Credit Note saved.", "Success");
                            clear();
                            this.ActiveControl = txtporderno;
                        }
                        else
                        {
                            if (txtporderno.Text == "")
                            {
                                MessageBox.Show("Select Purchase Number.", "Warning");
                                this.ActiveControl = txtporderno;
                            }
                            else if (gvpurreturnproducts.RowCount <= 0)
                            {
                                MessageBox.Show("Add Product Information.", "Warning");
                                this.ActiveControl = txtpname;
                            }
                        }
                    }
                }
                PRMRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void getGstCalculation()
        {
            try
            {
                decimal SGstAmt = 0, CGstAmt = 0, IGstAmt = 0, Taxableamt = 0, cessAmt = 0, discAmt = 0, spdiscAmt = 0, othDiscAmt = 0;
                if (txtamount.Text != "")
                {
                    Taxableamt = Convert.ToDecimal(txtamount.Text);
                    if (txtpdiscount.Text != "")
                        discAmt = Convert.ToDecimal(Taxableamt * Convert.ToDecimal(txtpdiscount.Text) / 100);
                    if (txtpSpcDisc.Text != "")
                        spdiscAmt = Convert.ToDecimal((Taxableamt - discAmt) * Convert.ToDecimal(txtpSpcDisc.Text) / 100);
                    if (txtOthDisc.Text != "")
                        othDiscAmt = Convert.ToDecimal((Taxableamt - discAmt - spdiscAmt) * Convert.ToDecimal(txtOthDisc.Text) / 100);
                    Taxableamt = Taxableamt - discAmt - spdiscAmt - othDiscAmt;
                    txtTaxableAmt.Text = Convert.ToString(Math.Round(Taxableamt, 2, MidpointRounding.AwayFromZero));
                    if (cmbsgst.Text != "")
                        SGstAmt = Taxableamt * Convert.ToDecimal(cmbsgst.Text) / 100;
                    if (cmbcgst.Text != "")
                        CGstAmt = Taxableamt * Convert.ToDecimal(cmbcgst.Text) / 100;
                    if (cmbigst.Text != "")
                        IGstAmt = Taxableamt * Convert.ToDecimal(cmbigst.Text) / 100;
                    if (txtpCess.Text != "")
                        cessAmt = Taxableamt * Convert.ToDecimal(txtpCess.Text) / 100;
                    Taxableamt = Taxableamt + SGstAmt + CGstAmt + IGstAmt + cessAmt;
                    txtpTotalAmt.Text = Convert.ToString(Math.Round(Taxableamt, 2, MidpointRounding.AwayFromZero));
                }
            }
            catch (Exception)
            { }
        }

        public void updatemaster()
        {
            try
            {
                SuppCreditDebitNoteMasterRepository PRMRepo = new SuppCreditDebitNoteMasterRepository();
                SuppCreditDebitMaster PRMData = new SuppCreditDebitMaster();
                PRMData = PRMRepo.GetAll().Where(t => t.SuppCreditDebitNo == Convert.ToInt32(txtPRNo.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                PRMData.SuppCreditDebitDate = dtpPRDate.Value;
                PRMData.PorderNo = Convert.ToInt32(txtporderno.Text);
                PRMData.PorderDate = dtpPorderdate.Value;
                PRMData.SupplierpoNo = Convert.ToString(txtsupplierpono.Text);
                PRMData.SupplierId = Convert.ToInt32(cmbsuppliername.SelectedValue);
                PRMData.Suppliername = cmbsuppliername.Text;
                PRMData.TotalAmount = Convert.ToDecimal(txtTotalAmt.Text);
                if (txtDiscount.Text == "")
                    PRMData.Discount = Convert.ToDecimal(0);
                else
                    PRMData.Discount = Convert.ToDecimal(txtDiscount.Text);
                if (txtSpDisc.Text == "")
                    PRMData.SpcDiscount = Convert.ToDecimal(0);
                else
                    PRMData.SpcDiscount = Convert.ToDecimal(txtSpDisc.Text);
                PRMData.NetAmount = Convert.ToDecimal(ttnetamt.Text);
                PRMData.TransactionYear = CommonMethod.TransactionYear;
                PRMData.CompId = CommonMethod.CompId;
                if (cdtype == "CN")
                    PRMData.CDType = "Credit Note";
                else if (cdtype == "DN")
                    PRMData.CDType = "Debit Note";
                PRMRepo.Edit(PRMData);
                PRMRepo.Save();
                printId = Convert.ToInt32(PRMData.SuppCreditDebitNo);
                purmasterid = Convert.ToInt32(PRMData.Id);
                PRMRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void updatedetail(int i, int Rowid, string usevent)
        {
            try
            {
                SuppCreditDebitNoteDetailRepository PRDRepo = new SuppCreditDebitNoteDetailRepository();
                SuppCreditDebitDetail PRDData = new SuppCreditDebitDetail();
                if (usevent == "update")
                    PRDData = PRDRepo.GetById(Rowid);
                PRDData.SuppCreditDebitNo = purmasterid;
                PRDData.ProductName = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRProductName"].Value);
                PRDData.CompanyName = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRCompany"].Value);
                PRDData.Unit = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value);
                PRDData.PurchaseUnit = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRPurUnit"].Value);
                PRDData.Amount = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRAmount"].Value);
                if (gvpurreturnproducts.Rows[i].Cells["PRDisc"].Value == null)
                    PRDData.Discount = 0;
                else
                    PRDData.Discount = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRDisc"].Value);
                if (gvpurreturnproducts.Rows[i].Cells["PRSpcDisc"].Value == null)
                    PRDData.SpcDiscount = 0;
                else
                    PRDData.SpcDiscount = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRSpcDisc"].Value);
                if (gvpurreturnproducts.Rows[i].Cells["PR3LevelDisc"].Value == null)
                    PRDData.ThreeLevelDicount = 0;
                else
                    PRDData.ThreeLevelDicount = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PR3LevelDisc"].Value);
                PRDData.SGstPercent = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRSGST"].Value);
                PRDData.SGstAmt = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRSGSTAmt"].Value);
                PRDData.CGstPercent = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRCGST"].Value);
                PRDData.CGstAmt = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRCGSTAmt"].Value);
                PRDData.IGstPercent = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRIGST"].Value);
                PRDData.IGstAmt = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRIGSTAmt"].Value);
                PRDData.Cess = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRCess"].Value);
                PRDData.CessAmt = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRCessAmt"].Value);
                PRDData.TotalAmount = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRTotalAmt"].Value);
                PRDData.Reason = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRReason"].Value);
                PRDData.POID = Convert.ToInt32(gvpurreturnproducts.Rows[i].Cells["PRPOID"].Value);
                PRDData.AssemblyChildStatus = "N";
                if (usevent == "update")
                    PRDRepo.Edit(PRDData);
                if (usevent == "save")
                    PRDRepo.Add(PRDData);
                PRDRepo.Save();
                PRDRepo.Dispose();
                int BDetailId = Convert.ToInt32(PRDData.POID);
                string Reason = Convert.ToString(PRDData.Reason);
                try
                {
                    ProductRepository productRepo = new ProductRepository();
                    var pData = productRepo.GetAll().Where(t => t.ProductName == Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRProductName"].Value) && t.ManufactureCompany == Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRCompany"].Value) && t.Uniti == Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (pData != null)
                    {
                        AssemeblyProductDetailsRepository assemblyDRepo = new AssemeblyProductDetailsRepository();
                        List<tblAssemblyProductDetail> assemblyDList = assemblyDRepo.GetAll().Where(t => t.AssemblyProductId == pData.ID && t.CompId == CommonMethod.CompId).ToList();
                        if (assemblyDList.Count > 0)
                        {
                            foreach (var itemAssembly in assemblyDList)
                            {
                                int UpdateSRId = 0;
                                SuppCreditDebitNoteDetailRepository PRDRepo1 = new SuppCreditDebitNoteDetailRepository();
                                SuppCreditDebitDetail PRDData1 = new SuppCreditDebitDetail();
                                if (usevent == "update")
                                {
                                    var srDetailData = PRDRepo1.GetAll().Where(t => t.SuppCreditDebitNo == purmasterid && t.ProductName == itemAssembly.ProductName && t.CompanyName == itemAssembly.CompanyName && t.Unit == itemAssembly.Unit && t.SuppCreditDebitMaster.CompId == CommonMethod.CompId && t.SuppCreditDebitMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    if (srDetailData != null)
                                        UpdateSRId = Convert.ToInt32(srDetailData.Id);
                                    PRDData1 = PRDRepo1.GetById(UpdateSRId);
                                }
                                PRDData1.SuppCreditDebitNo = purmasterid;
                                PRDData1.ProductName = Convert.ToString(itemAssembly.ProductName);
                                PRDData1.CompanyName = Convert.ToString(itemAssembly.CompanyName);
                                PRDData1.Unit = Convert.ToString(itemAssembly.Unit);
                                PRDData1.PurchaseUnit = Convert.ToString(itemAssembly.Unit);
                                PRDData1.Amount = Convert.ToDecimal(0);
                                PRDData1.ThreeLevelDicount = 0;
                                PRDData1.SGstPercent = Convert.ToDecimal(0);
                                PRDData1.SGstAmt = Convert.ToDecimal(0);
                                PRDData1.CGstPercent = Convert.ToDecimal(0);
                                PRDData1.CGstAmt = Convert.ToDecimal(0);
                                PRDData1.IGstPercent = Convert.ToDecimal(0);
                                PRDData1.IGstAmt = Convert.ToDecimal(0);
                                PRDData1.Cess = Convert.ToDecimal(0);
                                PRDData1.CessAmt = Convert.ToDecimal(0);
                                PRDData1.TotalAmount = Convert.ToDecimal(0);
                                PRDData1.AssemblyChildStatus = "Y";
                                PRDData1.Reason = Reason;
                                PurchaseDetailRepository custDRepo = new PurchaseDetailRepository();
                                var custData = custDRepo.GetAll().Where(t => t.Id == BDetailId && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                if (custData != null)
                                {
                                    custDRepo = new PurchaseDetailRepository();
                                    var custDetailData = custDRepo.GetAll().Where(t => t.PorderNo == custData.PorderNo && t.ProductName == itemAssembly.ProductName && t.CompanyName == itemAssembly.CompanyName && t.Unit == itemAssembly.Unit && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                                    if (custDetailData != null)
                                        PRDData1.POID = Convert.ToInt32(custDetailData.Id);
                                }
                                if (usevent == "update")
                                    PRDRepo1.Edit(PRDData1);
                                if (usevent == "save")
                                    PRDRepo1.Add(PRDData1);
                                PRDRepo1.Save();
                                PRDRepo1.Dispose();
                            }
                        }
                    }
                }
                catch (Exception)
                { }
            }
            catch (Exception)
            { }
        }

        public void updateremainigpayment(string mode)
        {
            try
            {
                SupplierRemainingPaymentRepository sremainingpaymentrepo = new SupplierRemainingPaymentRepository();
                SupplierRemainingPayment remainingpay = new SupplierRemainingPayment();
                if (cdtype == "DN")
                    remainingpay = sremainingpaymentrepo.GetAll().Where(t => t.BillId == Convert.ToInt32(txtporderno.Text) && t.Type == "Debit Note" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                else if (cdtype == "CN")
                    remainingpay = sremainingpaymentrepo.GetAll().Where(t => t.BillId == Convert.ToInt32(txtporderno.Text) && t.Type == "Credit Note" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                SupplierRemainingPaymentRepository remainingrepo = new SupplierRemainingPaymentRepository();
                SupplierRemainingPayment paydata = new SupplierRemainingPayment();
                if (remainingpay != null)
                {
                    if (Convert.ToString(ttnetamt.Text) != "")
                    {
                        paydata = new SupplierRemainingPayment();
                        paydata.SupplierId = Convert.ToInt32(cmbsuppliername.SelectedValue);
                        paydata.Suppliername = Convert.ToString(cmbsuppliername.Text);
                        decimal decremamt = 0;
                        if (cdtype == "DN")
                        {
                            paydata.NetAmt = 0;
                            paydata.PaidAmt = Convert.ToDecimal(ttnetamt.Text);
                            SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                            SupplierRemainingPayment remainingdata1 = new SupplierRemainingPayment();
                            remainingdata1 = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.Id < remainingpay.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                            if (remainingdata1 != null)
                            {
                                decremamt = Convert.ToDecimal(remainingdata1.RemainingAmt);
                                paydata.RemainingAmt = decremamt - Convert.ToDecimal(ttnetamt.Text);
                                decremamt = decremamt - Convert.ToDecimal(ttnetamt.Text);
                            }
                            else
                            {
                                paydata.RemainingAmt = 0;
                                decremamt = 0;
                            }
                            paydata.Type = "Debit Note";
                        }
                        else if (cdtype == "CN")
                        {
                            paydata.NetAmt = Convert.ToDecimal(ttnetamt.Text);
                            paydata.PaidAmt = 0;
                            SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                            SupplierRemainingPayment remainingdata1 = new SupplierRemainingPayment();
                            remainingdata1 = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.Id < remainingpay.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                            if (remainingdata1 != null)
                            {
                                decremamt = Convert.ToDecimal(remainingdata1.RemainingAmt);
                                paydata.RemainingAmt = decremamt + Convert.ToDecimal(ttnetamt.Text);
                                decremamt = decremamt + Convert.ToDecimal(ttnetamt.Text);
                            }
                            else
                            {
                                paydata.RemainingAmt = 0;
                                decremamt = 0;
                            }
                            paydata.Type = "Credit Note";
                        }

                        SupplierRemainingPaymentRepository remainingpayrepo2 = new SupplierRemainingPaymentRepository();
                        List<SupplierRemainingPayment> remainingdata2 = new List<SupplierRemainingPayment>();
                        remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.Id > remainingpay.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        paydata.BillId = Convert.ToInt32(txtporderno.Text);
                        paydata.BillDate = Convert.ToDateTime(dtpPRDate.Value);
                        paydata.CreateDate = Convert.ToDateTime(dtpPRDate.Value);
                        paydata.PaymentMode = mode;
                        paydata.TransactionYear = CommonMethod.TransactionYear;
                        paydata.CompId = CommonMethod.CompId;
                        paydata.Id = remainingpay.Id;
                        remainingrepo.Edit(paydata);
                        remainingrepo.Save();

                        int i = 0;
                        foreach (var item in remainingdata2)
                        {
                            item.RemainingAmt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                            decremamt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                            remainingpayrepo2.Edit(item);
                            remainingpayrepo2.Save();
                            i++;
                        }
                    }
                }
                else
                {
                    paydata = new SupplierRemainingPayment();
                    paydata.SupplierId = Convert.ToInt32(cmbsuppliername.SelectedValue);
                    paydata.Suppliername = Convert.ToString(cmbsuppliername.Text);
                    if (cdtype == "DN")
                    {
                        paydata.NetAmt = 0;
                        paydata.PaidAmt = Convert.ToDecimal(ttnetamt.Text);
                        paydata.Type = "Debit Note";
                    }
                    else if (cdtype == "CN")
                    {

                        paydata.NetAmt = Convert.ToDecimal(ttnetamt.Text);
                        paydata.PaidAmt = 0;
                        paydata.Type = "Credit Note";
                    }
                    paydata.RemainingAmt = remainingpay.RemainingAmt;
                    paydata.BillId = Convert.ToInt32(txtporderno.Text);
                    paydata.BillDate = Convert.ToDateTime(dtpPRDate.Value);
                    paydata.CreateDate = Convert.ToDateTime(dtpPRDate.Value);
                    paydata.PaymentMode = mode;
                    paydata.TransactionYear = CommonMethod.TransactionYear;
                    paydata.CompId = CommonMethod.CompId;
                    remainingrepo.Add(paydata);
                    remainingrepo.Save();
                }
                sremainingpaymentrepo.Dispose();
                remainingrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void cleartext()
        {
            txtamount.Text = "0";
            txtpTotalAmt.Text = "0";
        }

        public void LoadData(int PorderId)
        {
            try
            {
                GvProductInfo.Rows.Clear();
                PurchaseMasterRepository purmrepo = new PurchaseMasterRepository();
                var PurchaeData = purmrepo.GetAll().Where(t => t.PorderNo == PorderId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (PurchaeData.Suppliername != "First stock")
                {
                    txtporderno.Text = Convert.ToString(PurchaeData.PorderNo);
                    txtsupplierpono.Text = Convert.ToString(PurchaeData.SupplierpoNo);
                    dtpPorderdate.Text = Convert.ToString(PurchaeData.PorderDate.Value);
                    cmbsuppliername.Text = PurchaeData.Suppliername;
                    txtPOtotalamt.Text = Convert.ToString(PurchaeData.TotalAmount);
                    txtPODisc.Text = Convert.ToString(PurchaeData.Discount);
                    txtPOSpDisc.Text = Convert.ToString(PurchaeData.SpcDiscount);
                    txtPOTransChrg.Text = Convert.ToString(PurchaeData.TransportCharges);
                    txtPONetAmt.Text = Convert.ToString(PurchaeData.NetAmount);
                    txtPOPaidAmt.Text = Convert.ToString(PurchaeData.PaidAmount);
                    txtPORemAmt.Text = Convert.ToString(PurchaeData.RemainingAmount);
                    txtDiscount.Text = Convert.ToString(PurchaeData.Discount);
                    txtSpDisc.Text = Convert.ToString(PurchaeData.SpcDiscount);
                    decimal tempNetamt = Convert.ToDecimal(PurchaeData.NetAmount);
                    decimal tempPaidamt = Convert.ToDecimal(PurchaeData.PaidAmount);
                    decimal tempRemamt = Convert.ToDecimal(PurchaeData.RemainingAmount);
                    decimal sbdamt = ((Convert.ToDecimal(PurchaeData.TotalAmount) * Convert.ToDecimal(PurchaeData.Discount)) / 100);
                    decimal sbvamt = (((Convert.ToDecimal(PurchaeData.TotalAmount) - sbdamt) * Convert.ToDecimal(PurchaeData.Vat)) / 100);
                    decimal sbcalamt = (Convert.ToDecimal(PurchaeData.TotalAmount) - sbdamt + sbvamt + Convert.ToDecimal(PurchaeData.TransportCharges));
                    if (Math.Round(sbcalamt) == tempNetamt)
                        intamtchk = 0;
                    else
                        intamtchk = 1;
                    var PurchaseDetailData = PurchaeData.PurchaseDetails;
                    int i = 0;
                    foreach (var item in PurchaseDetailData)
                    {
                        if (item.AssemblyChildStatus == "N" || item.AssemblyChildStatus == null || item.AssemblyChildStatus == "")
                        {
                            GvProductInfo.Rows.Add();
                            GvProductInfo.Rows[i].Cells["Id"].Value = item.Id;
                            GvProductInfo.Rows[i].Cells["ProductNameg"].Value = item.ProductName;
                            GvProductInfo.Rows[i].Cells["Company"].Value = item.CompanyName;
                            GvProductInfo.Rows[i].Cells["Unit"].Value = item.Unit;
                            if (item.PurchaseUnit != null)
                                GvProductInfo.Rows[i].Cells["PurUnit"].Value = item.PurchaseUnit;
                            else
                                GvProductInfo.Rows[i].Cells["PurUnit"].Value = item.Unit;
                            if (item.Unit != item.PurchaseUnit && item.PurchaseUnit != null)
                            {
                                ProductRepository productrepo = new ProductRepository();
                                ProductInformation productdata = new ProductInformation();
                                if (CommonMethod.commProduct == true)
                                    productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit).FirstOrDefault();
                                else
                                    productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                GvProductInfo.Rows[i].Cells["Quantityg"].Value = Convert.ToDecimal(item.Quantity) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                GvProductInfo.Rows[i].Cells["FrQty"].Value = Convert.ToDecimal(item.FreeQty) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                            }
                            else
                            {
                                GvProductInfo.Rows[i].Cells["Quantityg"].Value = item.Quantity;
                                GvProductInfo.Rows[i].Cells["FrQty"].Value = item.FreeQty;
                            }
                            GvProductInfo.Rows[i].Cells["Code"].Value = item.Code;
                            GvProductInfo.Rows[i].Cells["Price"].Value = item.PurchasePrice;
                            GvProductInfo.Rows[i].Cells["Amount"].Value = item.Amount;
                            GvProductInfo.Rows[i].Cells["Disc"].Value = item.Discount;
                            GvProductInfo.Rows[i].Cells["SpDisc"].Value = item.SpcDisc;
                            GvProductInfo.Rows[i].Cells["ThreeLevelDisc"].Value = item.ThreeLevelDiscPer;
                            GvProductInfo.Rows[i].Cells["SGST"].Value = item.SGstPercent;
                            GvProductInfo.Rows[i].Cells["SGSTAmt"].Value = item.SGstAmt;
                            GvProductInfo.Rows[i].Cells["CGST"].Value = item.CGstPercent;
                            GvProductInfo.Rows[i].Cells["CGSTAmt"].Value = item.CGstAmt;
                            GvProductInfo.Rows[i].Cells["IGST"].Value = item.IGstPercent;
                            GvProductInfo.Rows[i].Cells["IGSTAmt"].Value = item.IGstAmt;
                            GvProductInfo.Rows[i].Cells["Cess"].Value = item.Cess;
                            GvProductInfo.Rows[i].Cells["CessAmt"].Value = item.CessAmt;
                            GvProductInfo.Rows[i].Cells["TotalAmount"].Value = item.TotalAmount;

                            i++;
                        }
                    }
                    btnsave.Enabled = true;
                    btnUpdate.Enabled = false;
                    dtpPorderdate.Enabled = false;
                    txtsupplierpono.Enabled = false;
                    cmbsuppliername.Enabled = false;

                    SuppCreditDebitNoteMasterRepository PRMRepo = new SuppCreditDebitNoteMasterRepository();
                    SuppCreditDebitMaster PRMData = new SuppCreditDebitMaster();
                    if (cdtype == "CN")
                        PRMData = PRMRepo.GetAll().Where(t => t.PorderNo == PorderId && t.CDType == "Credit Note" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    else if (cdtype == "DN")
                        PRMData = PRMRepo.GetAll().Where(t => t.PorderNo == PorderId && t.CDType == "Debit Note" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (PRMData != null)
                    {
                        txtPRNo.Text = Convert.ToString(PRMData.SuppCreditDebitNo);
                        dtpPRDate.Value = PRMData.SuppCreditDebitDate.Value;
                        txtTotalAmt.Text = Convert.ToString(PRMData.TotalAmount);
                        txtDiscount.Text = Convert.ToString(PRMData.Discount);
                        ttnetamt.Text = Convert.ToString(PRMData.NetAmount);
                        var PRDData = PRMData.SuppCreditDebitDetails;
                        gvpurreturnproducts.Rows.Clear();
                        int j = 0;
                        foreach (var item in PRDData)
                        {
                            if (item.AssemblyChildStatus == "N" || item.AssemblyChildStatus == null || item.AssemblyChildStatus == "")
                            {
                                gvpurreturnproducts.Rows.Add();
                                gvpurreturnproducts.Rows[j].Cells["PRId"].Value = item.Id;
                                gvpurreturnproducts.Rows[j].Cells["PRProductName"].Value = item.ProductName;
                                gvpurreturnproducts.Rows[j].Cells["PRCompany"].Value = item.CompanyName;
                                gvpurreturnproducts.Rows[j].Cells["PRUnit"].Value = item.Unit;
                                if (item.PurchaseUnit != null)
                                    gvpurreturnproducts.Rows[j].Cells["PRPurUnit"].Value = item.PurchaseUnit;
                                else
                                    gvpurreturnproducts.Rows[j].Cells["PRPurUnit"].Value = item.Unit;
                                gvpurreturnproducts.Rows[j].Cells["PRAmount"].Value = item.Amount;
                                gvpurreturnproducts.Rows[j].Cells["PRDisc"].Value = item.Discount;
                                gvpurreturnproducts.Rows[j].Cells["PRSpcDisc"].Value = item.SpcDiscount;
                                gvpurreturnproducts.Rows[j].Cells["PR3LevelDisc"].Value = item.ThreeLevelDicount;
                                gvpurreturnproducts.Rows[j].Cells["PRSGST"].Value = item.SGstPercent;
                                gvpurreturnproducts.Rows[j].Cells["PRSGSTAmt"].Value = item.SGstAmt;
                                gvpurreturnproducts.Rows[j].Cells["PRCGST"].Value = item.CGstPercent;
                                gvpurreturnproducts.Rows[j].Cells["PRCGSTAmt"].Value = item.CGstAmt;
                                gvpurreturnproducts.Rows[j].Cells["PRIGST"].Value = item.IGstPercent;
                                gvpurreturnproducts.Rows[j].Cells["PRIGSTAmt"].Value = item.IGstAmt;
                                gvpurreturnproducts.Rows[j].Cells["PRCess"].Value = item.Cess;
                                gvpurreturnproducts.Rows[j].Cells["PRCessAmt"].Value = item.CessAmt;
                                gvpurreturnproducts.Rows[j].Cells["PRTotalAmt"].Value = item.TotalAmount;
                                gvpurreturnproducts.Rows[j].Cells["PRReason"].Value = item.Reason;
                                gvpurreturnproducts.Rows[j].Cells["PRRemove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                gvpurreturnproducts.Rows[j].Cells["PRPOID"].Value = item.POID;
                                j++;
                            }
                        }
                        btnsave.Enabled = false;
                        btnUpdate.Enabled = true;
                    }
                    printId = Convert.ToInt32(txtPRNo.Text);
                    purmrepo.Dispose();
                    PRMRepo.Dispose();
                }
                else
                {
                    MessageBox.Show("First Stock not Valid for Return.", "Warning");
                    txtporderno.Text = "";
                    this.ActiveControl = txtporderno;
                }
                if (btnUpdate.Enabled == true)
                    btnDelete.Enabled = true;
            }
            catch (Exception)
            { }
        }

        private void txtporderno_Leave(object sender, EventArgs e)
        {
            try
            {
                gvpurreturnproducts.Rows.Clear();
                GvProductInfo.Rows.Clear();
                maxbillid();
                txtPOtotalamt.Text = "0";
                txtPODisc.Text = "0";
                txtPOSpDisc.Text = "0";
                txtPOTransChrg.Text = "0";
                txtPONetAmt.Text = "0";
                txtPOPaidAmt.Text = "0";
                txtPORemAmt.Text = "0";
                txtTotalAmt.Text = "0";
                txtDiscount.Text = "0";
                txtSpDisc.Text = "0";
                ttnetamt.Text = "0";
                LoadData(Convert.ToInt32(txtporderno.Text));
            }
            catch (Exception)
            { }
        }

        private void txtporderno_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTotalAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTotalAmt.Text != "")
                    PRCalculation();
            }
            catch (Exception)
            { }
        }

        public void PRCalculation()
        {
            try
            {
                decimal tamt = 0, v = 0, damt = 0, sdamt = 0, vamt = 0, namt = 0;
                tamt = Convert.ToDecimal(txtTotalAmt.Text);
                damt = tamt * Convert.ToDecimal(txtDiscount.Text) / 100;
                sdamt = (tamt - damt) * Convert.ToDecimal(txtSpDisc.Text) / 100;
                if (intamtchk == 0)
                    vamt = (tamt - damt - sdamt) * v / 100;
                namt = Convert.ToDecimal(Math.Round(tamt, 2, MidpointRounding.AwayFromZero)) - Convert.ToDecimal(Math.Round(damt, 2, MidpointRounding.AwayFromZero)) - Convert.ToDecimal(Math.Round(sdamt, 2, MidpointRounding.AwayFromZero)) + Convert.ToDecimal(Math.Round(vamt, 2, MidpointRounding.AwayFromZero));
                ttnetamt.Text = Convert.ToString(Math.Round(namt, 2, MidpointRounding.AwayFromZero));
                if (rdoff == 1)
                    RoundOff(namt);
            }
            catch (Exception)
            { }
        }

        public void RoundOff(decimal netamt)
        {
            try
            {
                string ramt;
                decimal b = Math.Round(netamt), c = 0;
                if (netamt > b)
                {
                    c = netamt - b;
                    ramt = "-" + Convert.ToString(Math.Round(c, 2, MidpointRounding.AwayFromZero));
                }
                else
                {
                    c = b - netamt;
                    ramt = Convert.ToString(Math.Round(c, 2, MidpointRounding.AwayFromZero));
                }
                decimal fnetamt = Convert.ToDecimal(netamt) + Convert.ToDecimal(ramt);
                ttnetamt.Text = Convert.ToString(Math.Round(fnetamt, 2, MidpointRounding.AwayFromZero));
            }
            catch (Exception)
            { }
        }

        private void txtamount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtamount.Text != "")
                    getGstCalculation();
            }
            catch (Exception)
            { }
        }

        private void txtamount_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtporderno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = GvProductInfo;
            }
            catch (Exception)
            { }
        }

        private void txtamount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbReason;
            }
            catch (Exception)
            { }
        }

        private void cmbReason_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnAdd;
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                RptSupplierCreditNote suppCDNote = new RptSupplierCreditNote(printId);
                suppCDNote.ShowDialog();

            }
            catch (Exception) { }
        }

        private void dtpPRDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtporderno;
            }
            catch (Exception) { }
        }

        private void GvProductInfo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtamount;
            }
            catch (Exception) { }
        }

        private void btnbilllist_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                suppcreditdebit = this;
                frmSupplierCreditDebitNotecs p = new frmSupplierCreditDebitNotecs();
                p.Visible = false;
                string sType = "";
                if (cdtype == "CN")
                    sType = "Credit Note";
                else
                    sType = "Debit Note";
                frmSupplierCreditDebitNoteList crdrlist = new frmSupplierCreditDebitNoteList(sType);
                crdrlist.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void txtpdiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtpdiscount.Text != "")
                    getGstCalculation();
                else
                    txtDiscount.Text = "0";
            }
            catch (Exception)
            { }
        }

        private void txtpSpcDisc_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtpSpcDisc.Text != "")
                    getGstCalculation();
                else
                    txtpSpcDisc.Text = "0";
            }
            catch (Exception)
            { }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbsuppliername.Text != "")
                {
                    if (gvpurreturnproducts.Rows.Count != 0)
                    {
                        updatemaster();
                        if (deleteid.Count > 0)
                        {
                            for (int x = 0; x < deleteid.Count; x++)
                            {
                                SuppCreditDebitNoteDetailRepository suppCDNoteRepo = new SuppCreditDebitNoteDetailRepository();
                                var suppCDNoteData = suppCDNoteRepo.GetAll().Where(t => t.Id == deleteid[x]).FirstOrDefault();
                                ProductRepository proRepo = new ProductRepository();
                                var pData = proRepo.GetAll().Where(t => t.ProductName == suppCDNoteData.ProductName && t.ManufactureCompany == suppCDNoteData.CompanyName && t.Uniti == suppCDNoteData.Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (pData != null)
                                {
                                    if (pData.ProductType == 2)
                                    {
                                        AssemeblyProductDetailsRepository assemblyDRepo = new AssemeblyProductDetailsRepository();
                                        List<tblAssemblyProductDetail> assemblyList = assemblyDRepo.GetAll().Where(t => t.AssemblyProductId == pData.ID).ToList();
                                        if (assemblyList.Count > 0)
                                        {
                                            int MasterId = 0;
                                            MasterId = Convert.ToInt32(suppCDNoteData.SuppCreditDebitNo);
                                            foreach (var itemAssembly in assemblyList)
                                            {
                                                suppCDNoteRepo = new SuppCreditDebitNoteDetailRepository();
                                                var sDData = suppCDNoteRepo.GetAll().Where(t => t.ProductName == itemAssembly.ProductName && t.CompanyName == itemAssembly.CompanyName && t.Unit == itemAssembly.Unit && t.SuppCreditDebitNo == MasterId).FirstOrDefault();
                                                if (sDData != null)
                                                {
                                                    SuppCreditDebitNoteDetailRepository suppCDNoteRepo1 = new SuppCreditDebitNoteDetailRepository();
                                                    SuppCreditDebitDetail sDetailData = suppCDNoteRepo1.GetById(sDData.Id);
                                                    suppCDNoteRepo1.Remove(sDetailData);
                                                    suppCDNoteRepo1.Save();
                                                }
                                            }
                                        }
                                        else
                                        { }
                                        suppCDNoteRepo = new SuppCreditDebitNoteDetailRepository();
                                        SuppCreditDebitDetail suppDData = suppCDNoteRepo.GetById(deleteid[x]);
                                        suppCDNoteRepo.Remove(suppDData);
                                        suppCDNoteRepo.Save();
                                    }
                                    else
                                    {
                                        suppCDNoteRepo = new SuppCreditDebitNoteDetailRepository();
                                        SuppCreditDebitDetail suppDData = suppCDNoteRepo.GetById(deleteid[x]);
                                        suppCDNoteRepo.Remove(suppDData);
                                        suppCDNoteRepo.Save();
                                    }
                                }
                            }
                        }
                        for (int i = 0; i < gvpurreturnproducts.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(gvpurreturnproducts.Rows[i].Cells["PRId"].Value) != 0)
                                updatedetail(i, Convert.ToInt32(gvpurreturnproducts.Rows[i].Cells["PRId"].Value), "update");
                            else
                                updatedetail(i, Convert.ToInt32(gvpurreturnproducts.Rows[i].Cells["PRId"].Value), "save");
                        }
                        updateremainigpayment("Cash");
                        gvpurreturnproducts.Rows.Clear();
                        MessageBox.Show("Purchase Return Updated.", "Success");
                        clear();
                        this.ActiveControl = txtporderno;
                    }
                    else
                        MessageBox.Show("Add Product.", "Warning");
                }
                else
                {
                    MessageBox.Show("Select Supplier Name.", "Warning");
                    this.ActiveControl = cmbsuppliername;
                }
            }
            catch (Exception)
            { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to Delete this Supplier Credit Debit Note ?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    AIOInventorySystem.Data.Model.Stock sdata = new Data.Model.Stock();
                    string CDType = "";
                    int CDNMId = 0;
                    SuppCreditDebitNoteMasterRepository suppCDNRepo = new SuppCreditDebitNoteMasterRepository();
                    var suppCDNdata = suppCDNRepo.GetAll().Where(t => t.SuppCreditDebitNo == printId && t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.PorderNo == Convert.ToInt32(txtporderno.Text) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    if (suppCDNdata != null)
                    {
                        CDType = suppCDNdata.CDType;
                        // add details for history table
                        try
                        {
                            DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                            DeleteHistoryTable historydata = new DeleteHistoryTable();
                            historydata.DeleteType = "Supplier" + " " + CDType;
                            historydata.BillNo = Convert.ToString(suppCDNdata.SuppCreditDebitNo);
                            historydata.SupplierOrCustomerName = Convert.ToString(suppCDNdata.Suppliername);
                            historydata.Amount = Convert.ToDecimal(suppCDNdata.NetAmount);
                            historydata.CurrentDate = DateTime.Now.Date;
                            historydata.BillDate = Convert.ToDateTime(suppCDNdata.SuppCreditDebitDate);
                            historydata.CompId = CommonMethod.CompId;
                            historydata.TransactionYear = CommonMethod.TransactionYear;
                            historyRepo.Add(historydata);
                            historyRepo.Save();
                        }
                        catch (Exception) { }

                        CDNMId = Convert.ToInt32(suppCDNdata.Id);
                        SupplierRemainingPaymentRepository suppRemRepo = new SupplierRemainingPaymentRepository();
                        var remData = suppRemRepo.GetAll().Where(t => t.BillId == Convert.ToInt32(txtporderno.Text) && t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.Type == CDType && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                        if (remData != null)
                        {
                            decimal decremamt = 0;
                            int ReceiptId = suppRemRepo.GetAll().Where(t => t.BillId == Convert.ToInt32(txtporderno.Text) && t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.Type == CDType && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().Id;
                            SupplierRemainingPaymentRepository remRepo = new SupplierRemainingPaymentRepository();
                            SupplierRemainingPayment remdata = remRepo.GetById(ReceiptId);
                            remRepo.Remove(remdata);
                            remRepo.Save();
                            if (remdata.PaymentMode != "Cash")
                            {
                                BankStatementRepository Brepo = new BankStatementRepository();
                                var bdata = Brepo.GetAll().Where(t => t.Bankname == remdata.Bankname && t.TransactionDate == remdata.CreateDate && t.ChequeNo == remdata.Chequeno && t.Withdrawals == remdata.PaidAmt && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (bdata != null)
                                {
                                    BankStatement bsdata = Brepo.GetById(bdata.Id);
                                    Brepo.Remove(bsdata);
                                    Brepo.Save();
                                }
                            }

                            SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                            SupplierRemainingPayment remdata1 = remRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(remdata.SupplierId) && t.Id < ReceiptId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                            if (remdata1 != null)
                                decremamt = Convert.ToDecimal(remdata1.RemainingAmt);
                            else
                                decremamt = 0;
                            SupplierRemainingPaymentRepository remainingpayrepo2 = new SupplierRemainingPaymentRepository();
                            List<SupplierRemainingPayment> remainingdata2 = new List<SupplierRemainingPayment>();
                            remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.SupplierId == Convert.ToInt32(remdata.SupplierId) && t.Id > ReceiptId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                            int i = 0;
                            foreach (var item in remainingdata2)
                            {
                                item.RemainingAmt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                                decremamt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                                remainingpayrepo2.Edit(item);
                                remainingpayrepo2.Save();
                                i++;
                            }
                        }
                        db.connect();
                        DataTable dt = db.GetTable("delete from SuppCreditDebitDetail where SuppCreditDebitNo='" + CDNMId + "'");
                        DataTable dt1 = db.GetTable("delete from SuppCreditDebitMaster where Id='" + CDNMId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                        MessageBox.Show("Record Deleted Successfully.", "Success");
                        dt.Dispose(); dt1.Dispose();
                        db.CloseConnection();
                        suppRemRepo.Dispose();
                        clear();
                        this.ActiveControl = txtporderno;
                        btnDelete.Enabled = false;
                    }
                }
            }
            catch (Exception) { }
        }

        private void GvProductInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                PurchaseDetailRepository pdetailrepo = new PurchaseDetailRepository();
                PurchaseDetail purdetaildata = new PurchaseDetail();
                purdetaildata = pdetailrepo.GetById(Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value));
                if (purdetaildata.Status != "Inactive")
                {
                    pocolIndex = e.RowIndex;
                    txtpname.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) + "," + Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value) + "," + Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value);
                    cmbUnit.Items.Clear();
                    cmbUnit.DisplayMember = "Text";
                    cmbUnit.ValueMember = "Value";
                    cmbUnit.Items.Add(new { Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["PurUnit"].Value), Value = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["PurUnit"].Value) });
                    cmbUnit.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["PurUnit"].Value);
                    if (GvProductInfo.Rows[e.RowIndex].Cells["Disc"].Value == null)
                        txtpdiscount.Text = "0";
                    else
                        txtpdiscount.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Disc"].Value);
                    if (GvProductInfo.Rows[e.RowIndex].Cells["SpDisc"].Value == null)
                        txtpSpcDisc.Text = "0";
                    else
                        txtpSpcDisc.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SpDisc"].Value);
                    if (GvProductInfo.Rows[e.RowIndex].Cells["ThreeLevelDisc"].Value == null)
                        txtOthDisc.Text = "0";
                    else
                        txtOthDisc.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ThreeLevelDisc"].Value);
                    cmbsgst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SGST"].Value);
                    cmbcgst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["CGST"].Value);
                    cmbigst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["IGST"].Value);
                    txtpCess.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Cess"].Value);
                    ProductRepository ProductRepo = new ProductRepository();
                    ProductInformation ProductData = new ProductInformation();
                    if (CommonMethod.commProduct == true)
                        ProductData = ProductRepo.GetAll().Where(t => t.ProductName == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) && t.ManufactureCompany == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value) && t.Uniti == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value)).FirstOrDefault();
                    else
                        ProductData = ProductRepo.GetAll().Where(t => t.ProductName == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) && t.ManufactureCompany == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value) && t.Uniti == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();

                    this.ActiveControl = txtamount;
                }
                pdetailrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void gvpurreturnproducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GvProductInfo.Columns[e.ColumnIndex].HeaderText == "Remove")
                //if (e.ColumnIndex == 19)
                {
                    if (btnsave.Enabled == true)
                    {
                        if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        {
                            gvpurreturnproducts.Rows.Remove(gvpurreturnproducts.Rows[e.RowIndex]);
                            txtTotalAmt.Text = Convert.ToString(Math.Round((gvpurreturnproducts.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["PRTotalAmt"].Value))), 2, MidpointRounding.AwayFromZero));
                        }
                    }
                    else if (gvpurreturnproducts.Rows.Count != 1 && btnsave.Enabled == false)
                    {
                        if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        {
                            deleteid.Add(Convert.ToInt32(gvpurreturnproducts.Rows[e.RowIndex].Cells["PRId"].Value));
                            gvpurreturnproducts.Rows.Remove(gvpurreturnproducts.Rows[e.RowIndex]);
                            txtTotalAmt.Text = Convert.ToString(Math.Round((gvpurreturnproducts.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["PRTotalAmt"].Value))), 2, MidpointRounding.AwayFromZero));
                        }
                    }
                    else
                        MessageBox.Show("This Grid has only 1 product." + Environment.NewLine + "You Cannot Remove It.", "Warning");
                }
            }
            catch (Exception)
            { }
        }
    }
}