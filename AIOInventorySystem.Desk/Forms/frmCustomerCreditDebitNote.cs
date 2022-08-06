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
    public partial class frmCustomerCreditDebitNote : Form
    {
        CommonMethod cm = new CommonMethod();
        DbClass db = new DbClass();
        public decimal totalamt = 0, mvat = 0, mdvat = 0, amt = 0, tempNetamt, tempPaidamt, tempRemamt, PRAmt = 0, Pamt = 0, fnetamt = 0, rdoff = 1;
        public int custdetailid, BillMasterID, qdid, RemainingPayId, colIndex = -1, sbcolIndex = -1, custmasterid, custdtlid, billtype, intamtchk;
        public static decimal grdqty = 0;
        public string printId, custbillno, ProductCategory, taxinclude, cdtype;
        static string creditstatus = "0";
        List<int> deleteid = new List<int>();
        public static frmCustomerCreditDebitNote creditdebitnote;
        List<CustomerBillDetail> CustDPredata = new List<CustomerBillDetail>();
        SettingRepository setrepo = new SettingRepository();
        Setting setData = new Setting();

        public frmCustomerCreditDebitNote()
        {
            InitializeComponent();
        }

        public frmCustomerCreditDebitNote(string type)
        {
            InitializeComponent();
            cdtype = type;
            if (cdtype == "CN")
                label5.Text = "Credit Note";
            else if (cdtype == "DN")
                label5.Text = "Debit Note";
            cm.changedatetimepickerrange(this);
            maxbillid();
            customerdata();
            fillgst();
            btnUpdate.Enabled = false;
            CustDPredata.Clear();
        }

        public frmCustomerCreditDebitNote(string billno, int CDNNo)
        {
            InitializeComponent();
            if (cdtype == "CN")
                label5.Text = "Credit Note";
            else if (cdtype == "DN")
                label5.Text = "Debit Note";
            cm.changedatetimepickerrange(this);
            maxbillid();
            customerdata();
            fillgst();
            btnUpdate.Enabled = false;
            CustDPredata.Clear();
            billdata(billno);
        }

        public void maxbillid()
        {
            try
            {
                CustCreditDebitNoteMasterRepository SaleRMRepo = new CustCreditDebitNoteMasterRepository();
                int mid = Convert.ToInt32(SaleRMRepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.CustCreditDebitNo));
                if (mid.ToString() == "" || mid == 0)
                    txtsrno.Text = "1";
                else
                    txtsrno.Text = Convert.ToString(mid + 1);
                SaleRMRepo.Dispose();
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
                cmbcustomername.DataSource = customerdata;
                cmbcustomername.ValueMember = "CustomerID";
                cmbcustomername.DisplayMember = "CustomerName";
                custrepo.Dispose();
                cmbcustomername.SelectedIndex = 0;
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
                    cmbsgst.SelectedIndex = 0;
                }
                DataTable dtcgst = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforSnCGST='Y'");
                if (dtcgst.Rows.Count > 0)
                {
                    cmbcgst.ValueMember = "GSTNo";
                    cmbcgst.DisplayMember = "GSTPercent";
                    cmbcgst.DataSource = dtcgst;
                    cmbcgst.SelectedIndex = 0;
                }
                DataTable dtigst = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforIGST='Y'");
                if (dtigst.Rows.Count > 0)
                {
                    cmbigst.ValueMember = "GSTNo";
                    cmbigst.DisplayMember = "GSTPercent";
                    cmbigst.DataSource = dtigst;
                    cmbigst.SelectedIndex = 0;
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
            if (keyData == Keys.F3)
            {
                if (btnsave.Enabled == true)
                    btnsave_Click(null, null);
                else if (btnUpdate.Enabled == true)
                    btnUpdate_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void frmCustomerCreditDebitNote_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                txtbillno.Text = "";

                bool userSetting = false;
                setrepo = new SettingRepository();
                setData = setrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (setData != null)
                {
                    if (setData.ShowUserDefinedSeries == "Y")
                        userSetting = true;
                }

                string cnString2 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                if (userSetting == false)
                {
                    using (SqlConnection con = new SqlConnection(cnString2))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT BillNo FROM CustomerBillMaster where TransactionYear = '" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", con);
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
                        txtbillno.AutoCompleteCustomSource = MyCollection;
                        con.Close();
                        reader.Dispose();
                    }
                }
                else
                {
                    using (SqlConnection con = new SqlConnection(cnString2))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT userseries FROM CustomerBillMaster where (userseries is not null or userseries != '') and TransactionYear = '" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", con);
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
                        txtbillno.AutoCompleteCustomSource = MyCollection;
                        con.Close();
                        reader.Dispose();
                    }
                }

                txtProductname.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = null;
                    if (CommonMethod.commProduct == true)
                        cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation", con);
                    else
                        cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where compid=" + CommonMethod.CompId, con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    AutoCompleteStringCollection MyCollection = new AutoCompleteStringCollection();
                    while (reader.Read())
                    {
                        try
                        { MyCollection.Add(reader.GetString(0) + "," + reader.GetString(1) + "," + reader.GetString(2)); }
                        catch (Exception)
                        { }
                    }
                    txtProductname.AutoCompleteCustomSource = MyCollection;
                    con.Close();
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
                if (cmbcustomername.Text != "Select")
                {
                    if (txtTaxableamt.Text != "" && cmbreason.Text != "")
                    {
                        if (sbcolIndex != -1)
                        {
                            int i = gvSaleReturnDetails.RowCount;
                            string proname = txtProductname.Text.Trim().Split(',')[0];
                            string company = txtProductname.Text.Trim().Split(',')[1];
                            string unit = txtProductname.Text.Trim().Split(',')[2];
                            int sbid = Convert.ToInt32(dtgvProdutDetail.Rows[sbcolIndex].Cells["Id"].Value);
                            string NewReason = cmbreason.Text;
                            DataTable dtbill = db.GetTable("Select cd.* from  CustomerBillDetail as cd inner join CustomerBillMaster as cm on cm.Id=cd.BillNo where cd.Id=" + sbid + " and cm.CompId=" + CommonMethod.CompId + " and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cd.ProductId = '" + proname + "' and cd.CompanyName = '" + company + "' and cd.Unit = '" + unit + "'");

                            CustomerBillDetailRepository Custdetailrepo = new CustomerBillDetailRepository();
                            if (dtbill.Rows.Count > 0)
                            {
                                for (int k = 0; k < gvSaleReturnDetails.Rows.Count; k++)
                                {
                                    string productname = Convert.ToString(gvSaleReturnDetails.Rows[k].Cells["CDProductName"].Value);
                                    string compname = Convert.ToString(gvSaleReturnDetails.Rows[k].Cells["CDCompany"].Value);
                                    string unitname = Convert.ToString(gvSaleReturnDetails.Rows[k].Cells["CDUnit"].Value);
                                    int prpoid = Convert.ToInt32(gvSaleReturnDetails.Rows[k].Cells["CDBNID"].Value);
                                    string returnReason = Convert.ToString(gvSaleReturnDetails.Rows[k].Cells["Reason"].Value);
                                    if (productname == proname && company == compname && unit == unitname && sbid == prpoid && NewReason.Trim() == returnReason.Trim())
                                    {
                                        try
                                        {
                                            gvSaleReturnDetails.Rows[k].Cells["CDProductName"].Value = proname;
                                            gvSaleReturnDetails.Rows[k].Cells["CDCompany"].Value = company;
                                            gvSaleReturnDetails.Rows[k].Cells["CDUnit"].Value = unit;
                                            gvSaleReturnDetails.Rows[k].Cells["CDSaleUnit"].Value = dtgvProdutDetail.Rows[sbcolIndex].Cells["SaleUnit"].Value.ToString();
                                            gvSaleReturnDetails.Rows[k].Cells["CDAmount"].Value = Convert.ToDecimal(txtTaxableamt.Text);
                                            decimal sgstamt = 0, cgstamt = 0, igstamt = 0, amount = 0, cessamt = 0;
                                            amount = Convert.ToDecimal(txtTaxableamt.Text);
                                            if (cmbsgst.Text != "")
                                            {
                                                gvSaleReturnDetails.Rows[k].Cells["CDSGST"].Value = cmbsgst.Text;
                                                sgstamt = amount * Convert.ToDecimal(cmbsgst.Text) / 100;
                                                gvSaleReturnDetails.Rows[k].Cells["CDSGSTAmt"].Value = sgstamt;
                                            }
                                            else
                                            {
                                                gvSaleReturnDetails.Rows[k].Cells["CDSGST"].Value = 0;
                                                gvSaleReturnDetails.Rows[k].Cells["CDSGSTAmt"].Value = 0;
                                            }
                                            if (cmbcgst.Text != "")
                                            {
                                                gvSaleReturnDetails.Rows[k].Cells["CDCGST"].Value = cmbcgst.Text;
                                                cgstamt = amount * Convert.ToDecimal(cmbcgst.Text) / 100;
                                                gvSaleReturnDetails.Rows[k].Cells["CDCGSTAmt"].Value = cgstamt;
                                            }
                                            else
                                            {
                                                gvSaleReturnDetails.Rows[k].Cells["CDCGST"].Value = 0;
                                                gvSaleReturnDetails.Rows[k].Cells["CDCGSTAmt"].Value = 0;
                                            }
                                            if (cmbigst.Text != "")
                                            {
                                                gvSaleReturnDetails.Rows[k].Cells["CDIGST"].Value = cmbigst.Text;
                                                igstamt = amount * Convert.ToDecimal(cmbigst.Text) / 100;
                                                gvSaleReturnDetails.Rows[k].Cells["CDIGSTAmt"].Value = igstamt;
                                            }
                                            else
                                            {
                                                gvSaleReturnDetails.Rows[k].Cells["CDIGST"].Value = 0;
                                                gvSaleReturnDetails.Rows[k].Cells["CDIGSTAmt"].Value = 0;
                                            }
                                            if (txtpCess.Text != "")
                                            {
                                                gvSaleReturnDetails.Rows[k].Cells["CDCess"].Value = txtpCess.Text;
                                                cessamt = amount * Convert.ToDecimal(txtpCess.Text) / 100;
                                                gvSaleReturnDetails.Rows[k].Cells["CDCessAmt"].Value = cessamt;
                                            }
                                            else
                                            {
                                                gvSaleReturnDetails.Rows[k].Cells["CDCess"].Value = 0;
                                                gvSaleReturnDetails.Rows[k].Cells["CDCessAmt"].Value = 0;
                                            }
                                            gvSaleReturnDetails.Rows[k].Cells["CDTotalAmt"].Value = txtpTotalAmt.Text;
                                            gvSaleReturnDetails.Rows[k].Cells["Reason"].Value = cmbreason.Text;
                                            gvSaleReturnDetails.Rows[k].Cells["CDBNID"].Value = sbid.ToString();
                                            gvSaleReturnDetails.Rows[k].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                            totalamt = 0;
                                            totalamt = gvSaleReturnDetails.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["CDTotalAmt"].Value));
                                            txtTotalAmt.Text = Convert.ToString(Math.Round(totalamt, 2, MidpointRounding.AwayFromZero));
                                            clearAdd();
                                        }
                                        catch (Exception)
                                        { }
                                        goto w;
                                    }
                                }

                                try
                                {
                                    gvSaleReturnDetails.Rows.Add();
                                    gvSaleReturnDetails.Rows[i].Cells["CDProductName"].Value = proname;
                                    gvSaleReturnDetails.Rows[i].Cells["CDCompany"].Value = company;
                                    gvSaleReturnDetails.Rows[i].Cells["CDUnit"].Value = unit;
                                    gvSaleReturnDetails.Rows[i].Cells["CDSaleUnit"].Value = dtgvProdutDetail.Rows[sbcolIndex].Cells["SaleUnit"].Value.ToString();
                                    gvSaleReturnDetails.Rows[i].Cells["CDAmount"].Value = Convert.ToDecimal(txtTaxableamt.Text);
                                    decimal sgstamt = 0, cgstamt = 0, igstamt = 0, amount = 0, cessamt = 0;
                                    amount = Convert.ToDecimal(txtTaxableamt.Text);
                                    if (cmbsgst.Text != "")
                                    {
                                        gvSaleReturnDetails.Rows[i].Cells["CDSGST"].Value = cmbsgst.Text;
                                        sgstamt = amount * Convert.ToDecimal(cmbsgst.Text) / 100;
                                        gvSaleReturnDetails.Rows[i].Cells["CDSGSTAmt"].Value = sgstamt;
                                    }
                                    else
                                    {
                                        gvSaleReturnDetails.Rows[i].Cells["CDSGST"].Value = 0;
                                        gvSaleReturnDetails.Rows[i].Cells["CDSGSTAmt"].Value = 0;
                                    }
                                    if (cmbcgst.Text != "")
                                    {
                                        gvSaleReturnDetails.Rows[i].Cells["CDCGST"].Value = cmbcgst.Text;
                                        cgstamt = amount * Convert.ToDecimal(cmbcgst.Text) / 100;
                                        gvSaleReturnDetails.Rows[i].Cells["CDCGSTAmt"].Value = cgstamt;
                                    }
                                    else
                                    {
                                        gvSaleReturnDetails.Rows[i].Cells["CDCGST"].Value = 0;
                                        gvSaleReturnDetails.Rows[i].Cells["CDCGSTAmt"].Value = 0;
                                    }
                                    if (cmbigst.Text != "")
                                    {
                                        gvSaleReturnDetails.Rows[i].Cells["CDIGST"].Value = cmbigst.Text;
                                        igstamt = amount * Convert.ToDecimal(cmbigst.Text) / 100;
                                        gvSaleReturnDetails.Rows[i].Cells["CDIGSTAmt"].Value = igstamt;
                                    }
                                    else
                                    {
                                        gvSaleReturnDetails.Rows[i].Cells["CDIGST"].Value = 0;
                                        gvSaleReturnDetails.Rows[i].Cells["CDIGSTAmt"].Value = 0;
                                    }
                                    if (txtpCess.Text != "")
                                    {
                                        gvSaleReturnDetails.Rows[i].Cells["CDCess"].Value = txtpCess.Text;
                                        cessamt = amount * Convert.ToDecimal(txtpCess.Text) / 100;
                                        gvSaleReturnDetails.Rows[i].Cells["CDCessAmt"].Value = cessamt;
                                    }
                                    else
                                    {
                                        gvSaleReturnDetails.Rows[i].Cells["CDCess"].Value = 0;
                                        gvSaleReturnDetails.Rows[i].Cells["CDCessAmt"].Value = 0;
                                    }

                                    gvSaleReturnDetails.Rows[i].Cells["CDTotalAmt"].Value = txtpTotalAmt.Text;
                                    gvSaleReturnDetails.Rows[i].Cells["Reason"].Value = cmbreason.Text;
                                    gvSaleReturnDetails.Rows[i].Cells["CDBNID"].Value = sbid.ToString();
                                    gvSaleReturnDetails.Rows[i].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;

                                    totalamt = 0;
                                    totalamt = gvSaleReturnDetails.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["CDTotalAmt"].Value));
                                    txtTotalAmt.Text = Convert.ToString(Math.Round(totalamt, 2, MidpointRounding.AwayFromZero));
                                    clearAdd();
                                }
                                catch (Exception)
                                { }
                            }
                            Custdetailrepo.Dispose();
                        }
                    w: { }
                    }
                    else
                    {
                        if (txtTaxableamt.Text == "")
                        {
                            MessageBox.Show("Enter Amount.","Warning");
                            this.ActiveControl = txtTaxableamt;
                        }
                        else
                        {
                            MessageBox.Show("Select Reason.","Warning");
                            this.ActiveControl = cmbreason;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Enter Customer name.","Warning");
                    this.ActiveControl = cmbcustomername;
                }
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                mvat = 0; mdvat = 0;
                maxbillid();
                txtbillno.Text = "";
                customerdata();
                txtSBPaidAmt.Enabled = false;
                btnsave.Enabled = true;
                btnUpdate.Enabled = false;
                txtProductname.Text = "";
                cmbUnit.Items.Clear();
                txtSBtotalamt.Text = "0";
                txtSBDisc.Text = "0";
                txtSBTransChrg.Text = "0";
                txtSBHamliChrgs.Text = "0";
                txtSBNetAmt.Text = "0";
                txtSBPaidAmt.Text = "0";
                txtSBRemAmt.Text = "0";
                fillgst();
                txtpCess.Text = "0";
                txtpTotalAmt.Text = "0";
                dtgvProdutDetail.Rows.Clear();
                gvSaleReturnDetails.Rows.Clear();
                txtTotalAmt.Text = "0";
                txtNetAmt.Text = "0";
                colIndex = -1;
                sbcolIndex = -1;
                CustDPredata.Clear();
            }
            catch (Exception)
            { }
            try
            {
                dtpsrdate.Value = DateTime.Now.Date;
                dtpbilldate.Value = DateTime.Now.Date;
                dtppaymentdate.Value = DateTime.Now.Date;
            }
            catch (Exception)
            { }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            clear();
            this.ActiveControl = txtbillno;
        }

        public void savemaster()
        {
            try
            {
                CustCreditDebitNoteMasterRepository SaleRMRepo = new CustCreditDebitNoteMasterRepository();
                CustCreditDebitMaster srmasterdata = new CustCreditDebitMaster();
                maxbillid();
                srmasterdata.CustCreditDebitNo = Convert.ToInt32(txtsrno.Text);
                srmasterdata.CustCreditDebitDate = dtpsrdate.Value;
                srmasterdata.BillNo = Convert.ToString(txtbillno.Text);
                srmasterdata.Billdate = Convert.ToDateTime(dtpbilldate.Value);
                try
                {
                    if (Convert.ToInt32(cmbcustomername.SelectedValue) > 0)
                    {
                        srmasterdata.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                        srmasterdata.CustomerName = cmbcustomername.Text;
                    }
                }
                catch (Exception)
                { }
                srmasterdata.Vat = 0;
                if (txtNetAmt.Text != "")
                    srmasterdata.NetAmount = Convert.ToDecimal(txtNetAmt.Text);
                else
                    srmasterdata.NetAmount = 0;
                if (txtTotalAmt.Text != "")
                    srmasterdata.TotalAmount = Convert.ToDecimal(txtTotalAmt.Text);
                else
                    srmasterdata.TotalAmount = 0;
                srmasterdata.TransactionYear = CommonMethod.TransactionYear;
                srmasterdata.CompId = CommonMethod.CompId;
                if (cdtype == "CN")
                    srmasterdata.CDType = "Credit Note";
                else if (cdtype == "DN")
                    srmasterdata.CDType = "Debit Note";
                SaleRMRepo.Add(srmasterdata);
                SaleRMRepo.Save();
                printId = Convert.ToString(srmasterdata.CustCreditDebitNo);
                custmasterid = Convert.ToInt32(srmasterdata.Id);
                SaleRMRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void savedetail(int i)
        {
            try
            {
                CustCreditDebitNoteDetailRepository SaleRDRepo = new CustCreditDebitNoteDetailRepository();
                CustCreditDebitDetail srdetaildata = new CustCreditDebitDetail();

                srdetaildata.CustCreditDebitNo = custmasterid;
                srdetaildata.ProductId = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["CDProductName"].Value);
                srdetaildata.CompanyName = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["CDCompany"].Value);
                srdetaildata.Unit = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["CDUnit"].Value);
                srdetaildata.SaleUnit = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["CDSaleUnit"].Value);
                srdetaildata.Amount = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDAmount"].Value);
                srdetaildata.SGstPercent = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDSGST"].Value);
                srdetaildata.SGstAmt = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDSGSTAmt"].Value);
                srdetaildata.CGstPercent = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDCGST"].Value);
                srdetaildata.CGstAmt = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDCGSTAmt"].Value);
                srdetaildata.IGstPercent = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDIGST"].Value);
                srdetaildata.IGstAmt = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDIGSTAmt"].Value);
                srdetaildata.Cess = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDCess"].Value);
                srdetaildata.CessAmt = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDCessAmt"].Value);
                srdetaildata.TotalAmount = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDTotalAmt"].Value);
                srdetaildata.Reason = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["Reason"].Value);
                srdetaildata.BNID = Convert.ToInt32(gvSaleReturnDetails.Rows[i].Cells["CDBNID"].Value);
                srdetaildata.AssemblyChildStatus = "N";
                SaleRDRepo.Add(srdetaildata);
                SaleRDRepo.Save();
                custdtlid = Convert.ToInt32(srdetaildata.Id);
                int BDetailId = Convert.ToInt32(srdetaildata.BNID);
                string Reason = Convert.ToString(srdetaildata.Reason);
                SaleRDRepo.Dispose();
                try
                {
                    ProductRepository productRepo = new ProductRepository();
                    var pData = productRepo.GetAll().Where(t => t.ProductName == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["CDProductName"].Value) && t.ManufactureCompany == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["CDCompany"].Value) && t.Uniti == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["CDUnit"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (pData != null)
                    {
                        AssemeblyProductDetailsRepository assemblyDRepo = new AssemeblyProductDetailsRepository();
                        List<tblAssemblyProductDetail> assemblyDList = assemblyDRepo.GetAll().Where(t => t.AssemblyProductId == pData.ID && t.CompId == CommonMethod.CompId).ToList();
                        if (assemblyDList.Count > 0)
                        {
                            foreach (var itemAssembly in assemblyDList)
                            {
                                CustCreditDebitNoteDetailRepository SaleRDRepo1 = new CustCreditDebitNoteDetailRepository();
                                CustCreditDebitDetail srdetaildata1 = new CustCreditDebitDetail();
                                srdetaildata1.CustCreditDebitNo = custmasterid;
                                srdetaildata1.ProductId = Convert.ToString(itemAssembly.ProductName);
                                srdetaildata1.CompanyName = Convert.ToString(itemAssembly.CompanyName);
                                srdetaildata1.Unit = Convert.ToString(itemAssembly.Unit);
                                srdetaildata1.SaleUnit = Convert.ToString(itemAssembly.Unit);
                                srdetaildata1.Amount = Convert.ToDecimal(0);
                                srdetaildata1.SGstPercent = Convert.ToDecimal(0);
                                srdetaildata1.SGstAmt = Convert.ToDecimal(0);
                                srdetaildata1.CGstPercent = Convert.ToDecimal(0);
                                srdetaildata1.CGstAmt = Convert.ToDecimal(0);
                                srdetaildata1.IGstPercent = Convert.ToDecimal(0);
                                srdetaildata1.IGstAmt = Convert.ToDecimal(0);
                                srdetaildata1.Cess = Convert.ToDecimal(0);
                                srdetaildata1.CessAmt = Convert.ToDecimal(0);
                                srdetaildata1.AssemblyChildStatus = "Y";
                                srdetaildata1.TotalAmount = Convert.ToDecimal(0);
                                srdetaildata1.Reason = Reason;
                                CustomerBillDetailRepository custDRepo = new CustomerBillDetailRepository();
                                var custData = custDRepo.GetAll().Where(t => t.Id == BDetailId && t.CustomerBillMaster.CompId == CommonMethod.CompId && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                if (custData != null)
                                {
                                    custDRepo = new CustomerBillDetailRepository();
                                    var custDetailData = custDRepo.GetAll().Where(t => t.BillNo == custData.BillNo && t.ProductId == itemAssembly.ProductName && t.CompanyName == itemAssembly.CompanyName && t.Unit == itemAssembly.Unit && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear && t.CustomerBillMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                                    if (custDetailData != null)
                                        srdetaildata1.BNID = Convert.ToInt32(custDetailData.Id);
                                }
                                SaleRDRepo1.Add(srdetaildata1);
                                SaleRDRepo1.Save();
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
                RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                RemainingPayment remainingdata = new RemainingPayment();
                remainingdata.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                remainingdata.Customername = Convert.ToString(cmbcustomername.Text);
                if (cdtype == "CN")
                {
                    remainingdata.NetAmt = 0;
                    remainingdata.PaidAmt = Convert.ToDecimal(txtNetAmt.Text);
                    RemainingPaymentRepository remainingpayrepo1 = new RemainingPaymentRepository();
                    RemainingPayment remainingdata1 = new RemainingPayment();
                    remainingdata1 = remainingpayrepo1.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                    decimal decremamt = 0;
                    if (remainingdata1 != null)
                    {
                        decremamt = Convert.ToDecimal(remainingdata1.RemainingAmt);
                        remainingdata.RemainingAmt = decremamt - Convert.ToDecimal(txtNetAmt.Text);
                    }
                    else
                        remainingdata.RemainingAmt = 0;
                    remainingdata.Type = "Credit Note";
                }
                else if (cdtype == "DN")
                {
                    remainingdata.NetAmt = Convert.ToDecimal(txtNetAmt.Text);
                    remainingdata.PaidAmt = 0;
                    RemainingPaymentRepository remainingpayrepo1 = new RemainingPaymentRepository();
                    RemainingPayment remainingdata1 = new RemainingPayment();
                    remainingdata1 = remainingpayrepo1.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                    decimal decremamt = 0;
                    if (remainingdata1 != null)
                    {
                        decremamt = Convert.ToDecimal(remainingdata1.RemainingAmt);
                        remainingdata.RemainingAmt = decremamt + Convert.ToDecimal(txtNetAmt.Text);
                    }
                    else
                        remainingdata.RemainingAmt = 0;
                    remainingdata.Type = "Debit Note";
                }
                remainingdata.CreateDate = Convert.ToDateTime(dtpbilldate.Value);
                remainingdata.PaymentDate = Convert.ToDateTime(dtppaymentdate.Value);
                remainingdata.PaymentMode = mode;
                remainingdata.BillId = Convert.ToString(txtbillno.Text);
                remainingdata.BillDate = Convert.ToDateTime(dtpbilldate.Value);
                remainingdata.TransactionYear = CommonMethod.TransactionYear;
                remainingdata.CompId = CommonMethod.CompId;
                remainingpayrepo.Add(remainingdata);
                remainingpayrepo.Save();
                remainingpayrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                CustCreditDebitNoteMasterRepository SaleRMRepo = new CustCreditDebitNoteMasterRepository();
                var BillId = SaleRMRepo.GetAll().Where(t => t.CustCreditDebitNo == Convert.ToInt32(txtsrno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (BillId != null)
                {
                    MessageBox.Show("Already Saved This Record.","Warning");
                    this.ActiveControl = btnnew;
                }
                else
                {
                    if (cmbcustomername.Text != "Select")
                    {
                        if (gvSaleReturnDetails.Rows.Count != 0)
                        {
                            savemaster();
                            for (int i = 0; i < gvSaleReturnDetails.Rows.Count; i++)
                            {
                                savedetail(i);
                            }
                            if (billtype == 1 || billtype == 2)
                                saveremainingpayment("Cash");
                            MessageBox.Show("Credit debit Note saved.","Success");
                            clear();
                            this.ActiveControl = txtbillno;
                        }
                        else
                            MessageBox.Show("Add Product Information.","Warning");
                    }
                    else
                    {
                        if (cmbcustomername.Text == "Select")
                        {
                            MessageBox.Show("Select Customer Name.","Warning");
                            this.ActiveControl = cmbcustomername;
                        }
                    }
                }
                SaleRMRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void updatemaster()
        {
            try
            {
                CustCreditDebitNoteMasterRepository SaleRMRepo = new CustCreditDebitNoteMasterRepository();
                CustCreditDebitMaster srmasterdata = new CustCreditDebitMaster();
                srmasterdata = SaleRMRepo.GetAll().Where(t => t.CustCreditDebitNo == Convert.ToInt32(txtsrno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                srmasterdata.CustCreditDebitDate = dtpsrdate.Value;
                srmasterdata.BillNo = Convert.ToString(txtbillno.Text);
                srmasterdata.Billdate = Convert.ToDateTime(dtpbilldate.Value);
                try
                {
                    if (Convert.ToInt32(cmbcustomername.SelectedValue) > 0)
                    {
                        srmasterdata.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                        srmasterdata.CustomerName = cmbcustomername.Text;
                    }
                }
                catch (Exception)
                { }
                srmasterdata.Vat = 0;
                if (txtNetAmt.Text != "")
                    srmasterdata.NetAmount = Convert.ToDecimal(txtNetAmt.Text);
                else
                    srmasterdata.NetAmount = Convert.ToDecimal(txtTotalAmt.Text);
                if (txtTotalAmt.Text != "")
                    srmasterdata.TotalAmount = Convert.ToDecimal(txtTotalAmt.Text);
                else
                    srmasterdata.TotalAmount = 0;
                srmasterdata.TransactionYear = CommonMethod.TransactionYear;
                srmasterdata.CompId = CommonMethod.CompId;
                if (cdtype == "CN")
                    srmasterdata.CDType = "Credit Note";
                else if (cdtype == "DN")
                    srmasterdata.CDType = "Debit Note";
                SaleRMRepo.Edit(srmasterdata);
                SaleRMRepo.Save();
                printId = Convert.ToString(srmasterdata.CustCreditDebitNo);
                BillMasterID = srmasterdata.Id;
                SaleRMRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void updatedetail(int i, int Rowid, string usevent)
        {
            try
            {
                CustCreditDebitNoteDetailRepository SaleRDRepo = new CustCreditDebitNoteDetailRepository();
                CustCreditDebitDetail srdetaildata = new CustCreditDebitDetail();
                if (usevent == "update")
                    srdetaildata = SaleRDRepo.GetById(Rowid);
                srdetaildata.CustCreditDebitNo = BillMasterID;
                srdetaildata.ProductId = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["CDProductName"].Value);
                srdetaildata.CompanyName = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["CDCompany"].Value);
                srdetaildata.Unit = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["CDUnit"].Value);
                srdetaildata.SaleUnit = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["CDSaleUnit"].Value);
                srdetaildata.Amount = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDAmount"].Value);
                srdetaildata.SGstPercent = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDSGST"].Value);
                srdetaildata.SGstAmt = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDSGSTAmt"].Value);
                srdetaildata.CGstPercent = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDCGST"].Value);
                srdetaildata.CGstAmt = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDCGSTAmt"].Value);
                srdetaildata.IGstPercent = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDIGST"].Value);
                srdetaildata.IGstAmt = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDIGSTAmt"].Value);
                srdetaildata.Cess = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDCess"].Value);
                srdetaildata.CessAmt = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDCessAmt"].Value);
                srdetaildata.TotalAmount = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["CDTotalAmt"].Value);
                srdetaildata.Reason = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["Reason"].Value);
                srdetaildata.BNID = Convert.ToInt32(gvSaleReturnDetails.Rows[i].Cells["CDBNID"].Value);
                srdetaildata.AssemblyChildStatus = "N";
                if (usevent == "update")
                    SaleRDRepo.Edit(srdetaildata);
                if (usevent == "save")
                    SaleRDRepo.Add(srdetaildata);
                SaleRDRepo.Save();
                custdtlid = srdetaildata.Id;
                SaleRDRepo.Dispose();
                int BDetailId = Convert.ToInt32(srdetaildata.BNID);
                string Reason = Convert.ToString(srdetaildata.Reason);
                try
                {
                    ProductRepository productRepo = new ProductRepository();
                    var pData = productRepo.GetAll().Where(t => t.ProductName == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["CDProductName"].Value) && t.ManufactureCompany == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["CDCompany"].Value) && t.Uniti == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["CDUnit"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (pData != null)
                    {
                        AssemeblyProductDetailsRepository assemblyDRepo = new AssemeblyProductDetailsRepository();
                        List<tblAssemblyProductDetail> assemblyDList = assemblyDRepo.GetAll().Where(t => t.AssemblyProductId == pData.ID && t.CompId == CommonMethod.CompId).ToList();
                        if (assemblyDList.Count > 0)
                        {
                            foreach (var itemAssembly in assemblyDList)
                            {
                                int UpdateSRId = 0;
                                CustCreditDebitNoteDetailRepository SaleRDRepo1 = new CustCreditDebitNoteDetailRepository();
                                CustCreditDebitDetail srdetaildata1 = new CustCreditDebitDetail();
                                if (usevent == "update")
                                {
                                    var srDetailData = SaleRDRepo1.GetAll().Where(t => t.CustCreditDebitNo == BillMasterID && t.ProductId == itemAssembly.ProductName && t.CompanyName == itemAssembly.CompanyName && t.Unit == itemAssembly.Unit && t.CustCreditDebitMaster.CompId == CommonMethod.CompId && t.CustCreditDebitMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    if (srDetailData != null)
                                        UpdateSRId = Convert.ToInt32(srDetailData.Id);
                                    srdetaildata1 = SaleRDRepo1.GetById(UpdateSRId);
                                }
                                srdetaildata1.CustCreditDebitNo = custmasterid;
                                srdetaildata1.ProductId = Convert.ToString(itemAssembly.ProductName);
                                srdetaildata1.CompanyName = Convert.ToString(itemAssembly.CompanyName);
                                srdetaildata1.Unit = Convert.ToString(itemAssembly.Unit);
                                srdetaildata1.SaleUnit = Convert.ToString(itemAssembly.Unit);
                                srdetaildata1.Amount = Convert.ToDecimal(0);
                                srdetaildata1.SGstPercent = Convert.ToDecimal(0);
                                srdetaildata1.SGstAmt = Convert.ToDecimal(0);
                                srdetaildata1.CGstPercent = Convert.ToDecimal(0);
                                srdetaildata1.CGstAmt = Convert.ToDecimal(0);
                                srdetaildata1.IGstPercent = Convert.ToDecimal(0);
                                srdetaildata1.IGstAmt = Convert.ToDecimal(0);
                                srdetaildata1.Cess = Convert.ToDecimal(0);
                                srdetaildata1.CessAmt = Convert.ToDecimal(0);
                                srdetaildata1.AssemblyChildStatus = "Y";
                                srdetaildata1.TotalAmount = Convert.ToDecimal(0);
                                srdetaildata1.Reason = Reason;
                                CustomerBillDetailRepository custDRepo = new CustomerBillDetailRepository();
                                var custData = custDRepo.GetAll().Where(t => t.Id == BDetailId && t.CustomerBillMaster.CompId == CommonMethod.CompId && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                if (custData != null)
                                {
                                    custDRepo = new CustomerBillDetailRepository();
                                    var custDetailData = custDRepo.GetAll().Where(t => t.BillNo == custData.BillNo && t.ProductId == itemAssembly.ProductName && t.CompanyName == itemAssembly.CompanyName && t.Unit == itemAssembly.Unit && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear && t.CustomerBillMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                                    if (custDetailData != null)
                                        srdetaildata1.BNID = Convert.ToInt32(custDetailData.Id);
                                }
                                if (usevent == "update")
                                    SaleRDRepo1.Edit(srdetaildata1);
                                if (usevent == "save")
                                    SaleRDRepo1.Add(srdetaildata1);
                                SaleRDRepo1.Save();
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
                RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                RemainingPaymentRepository payrepo = new RemainingPaymentRepository();
                RemainingPayment remainingdata1 = new RemainingPayment();
                RemainingPayment remainingpay = new RemainingPayment();
                if (cdtype == "CN")
                    remainingpay = remainingpayrepo.GetAll().Where(t => t.BillId == Convert.ToString(custbillno) && t.Type == "Credit Note" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                else if (cdtype == "DN")
                    remainingpay = remainingpayrepo.GetAll().Where(t => t.BillId == Convert.ToString(custbillno) && t.Type == "Debit Note" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();

                RemainingPaymentRepository remainingrepo = new RemainingPaymentRepository();
                RemainingPayment paydata = new RemainingPayment();
                if (remainingpay != null)
                {
                    if (Convert.ToString(txtNetAmt.Text) != "")
                    {
                        paydata = new RemainingPayment();
                        paydata.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                        paydata.Customername = Convert.ToString(cmbcustomername.Text);
                        decimal decremamt = 0;
                        if (cdtype == "CN")
                        {
                            paydata.NetAmt = 0;
                            paydata.PaidAmt = Convert.ToDecimal(txtNetAmt.Text);
                            RemainingPaymentRepository remainingpayrepo1 = new RemainingPaymentRepository();
                            RemainingPayment remainingdata2 = new RemainingPayment();
                            remainingdata2 = remainingpayrepo1.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.Id < remainingpay.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                            if (remainingdata1 != null)
                            {
                                decremamt = Convert.ToDecimal(remainingdata2.RemainingAmt);
                                paydata.RemainingAmt = decremamt - Convert.ToDecimal(txtNetAmt.Text);
                                decremamt = decremamt - Convert.ToDecimal(txtNetAmt.Text);
                            }
                            else
                            {
                                paydata.RemainingAmt = 0;
                                decremamt = 0;
                            }
                            paydata.Type = "Credit Note";
                        }
                        else if (cdtype == "DN")
                        {
                            paydata.NetAmt = Convert.ToDecimal(txtNetAmt.Text);
                            paydata.PaidAmt = 0;
                            RemainingPaymentRepository remainingpayrepo1 = new RemainingPaymentRepository();
                            RemainingPayment remainingdata2 = new RemainingPayment();
                            remainingdata2 = remainingpayrepo1.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.Id < remainingpay.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                            if (remainingdata1 != null)
                            {
                                decremamt = Convert.ToDecimal(remainingdata2.RemainingAmt);
                                paydata.RemainingAmt = decremamt + Convert.ToDecimal(txtNetAmt.Text);
                                decremamt = decremamt + Convert.ToDecimal(txtNetAmt.Text);
                            }
                            else
                            {
                                paydata.RemainingAmt = 0;
                                decremamt = 0;
                            }
                            paydata.Type = "Debit Note";
                        }
                        RemainingPaymentRepository remainingpayrepo2 = new RemainingPaymentRepository();
                        List<RemainingPayment> remainingdata3 = new List<RemainingPayment>();
                        remainingdata3 = remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.Id > remainingpay.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                        paydata.BillId = Convert.ToString(txtbillno.Text);
                        paydata.BillDate = Convert.ToDateTime(dtpbilldate.Value);
                        paydata.CreateDate = Convert.ToDateTime(dtpbilldate.Value);
                        paydata.PaymentDate = Convert.ToDateTime(dtppaymentdate.Value);
                        paydata.PaymentMode = mode;
                        paydata.TransactionYear = CommonMethod.TransactionYear;
                        paydata.CompId = CommonMethod.CompId;
                        RemainingPayId = remainingpay.Id;
                        paydata.Id = RemainingPayId;
                        remainingrepo.Edit(paydata);
                        remainingrepo.Save();

                        int i = 0;
                        foreach (var item in remainingdata3)
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
                else
                {
                    paydata = new RemainingPayment();
                    paydata.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                    paydata.Customername = Convert.ToString(cmbcustomername.Text);
                    if (cdtype == "CN")
                    {
                        paydata.NetAmt = 0;
                        paydata.PaidAmt = Convert.ToDecimal(txtNetAmt.Text);
                        paydata.Type = "Credit Note";
                    }
                    else if (cdtype == "DN")
                    {

                        paydata.NetAmt = Convert.ToDecimal(txtNetAmt.Text);
                        paydata.PaidAmt = 0;
                        paydata.Type = "Debit Note";
                    }
                    paydata.RemainingAmt = remainingpay.RemainingAmt;
                    paydata.BillId = Convert.ToString(txtbillno.Text);
                    paydata.BillDate = Convert.ToDateTime(dtpbilldate.Value);
                    paydata.CreateDate = Convert.ToDateTime(dtpbilldate.Value);
                    paydata.PaymentDate = Convert.ToDateTime(dtppaymentdate.Value);
                    paydata.PaymentMode = mode;
                    paydata.TransactionYear = CommonMethod.TransactionYear;
                    paydata.CompId = CommonMethod.CompId;
                    remainingrepo.Add(paydata);
                    remainingrepo.Save();
                }
                remainingpayrepo.Dispose();
                payrepo.Dispose();
                remainingrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void clearAdd()
        {
            try
            {
                txtProductname.Text = "";
                cmbUnit.Items.Clear();
                fillgst();
                txtpCess.Text = "0";
                txtTaxableamt.Text = "0";
                txtpTotalAmt.Text = "0";
                totalamt = 0;
                cmbreason.SelectedIndex = -1;
                sbcolIndex = -1;
            }
            catch (Exception)
            { }
        }

        public void cleartext()
        {
            try
            {
                txtpTotalAmt.Text = "0";
            }
            catch (Exception)
            { }
        }

        public void getGstCalculation()
        {
            decimal SGAmt = 0, CGAmt = 0, IGAmt = 0, TaxableAmt = 0, sgstpercent = 0, cgstpercent = 0, Igstpercent = 0, Cessamt = 0;
            try
            {
                if (txtTaxableamt.Text != "")
                {
                    sgstpercent = Convert.ToDecimal(cmbsgst.Text);
                    cgstpercent = Convert.ToDecimal(cmbcgst.Text);
                    Igstpercent = Convert.ToDecimal(cmbigst.Text);
                    TaxableAmt = Convert.ToDecimal(txtTaxableamt.Text);
                    if (cmbsgst.Text != "")
                        SGAmt = (Convert.ToDecimal(TaxableAmt) * Convert.ToDecimal(cmbsgst.Text)) / 100;
                    if (cmbcgst.Text != "")
                        CGAmt = (Convert.ToDecimal(TaxableAmt) * Convert.ToDecimal(cmbcgst.Text)) / 100;
                    if (cmbigst.Text != "")
                        IGAmt = (Convert.ToDecimal(TaxableAmt) * Convert.ToDecimal(cmbigst.Text)) / 100;
                    if (txtpCess.Text != "")
                        Cessamt = (Convert.ToDecimal(TaxableAmt) * Convert.ToDecimal(txtpCess.Text)) / 100;
                    TaxableAmt = TaxableAmt + SGAmt + CGAmt + IGAmt + Cessamt;
                    txtpTotalAmt.Text = Convert.ToString(Math.Round(TaxableAmt, 2, MidpointRounding.AwayFromZero));
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error On Calculating Amount", "Warning");
                this.ActiveControl = txtTotalAmt;
            }
        }

        public void billdata(string billno)
        {
            try
            {
                char a = billno.FirstOrDefault();
                if (a == 'T')
                    billtype = 1;
                else if (a == 'E')
                    billtype = 2;
                else if (a == 'F')
                    billtype = 3;
                if (billtype != 3)
                {
                    CustomerBillMasterRepository Custmasterrepo = new CustomerBillMasterRepository();
                    var custmasterData = Custmasterrepo.GetAll().Where(t => (t.BillNo == Convert.ToString(billno) || t.UserSeries == Convert.ToString(billno)) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    custbillno = billno;
                    txtbillno.Text = Convert.ToString(custmasterData.BillNo);
                    dtpbilldate.Value = custmasterData.Billdate.Value.Date;
                    cmbcustomername.Text = custmasterData.CustomerName;
                    custdetailid = custmasterData.Id;
                    dtppaymentdate.Value = custmasterData.PaymentDate.Value.Date;
                    txtSBtotalamt.Text = Convert.ToString(custmasterData.TotalAmount);
                    txtSBDisc.Text = Convert.ToString(custmasterData.Discount);
                    txtSBTransChrg.Text = Convert.ToString(custmasterData.TransportCharges);
                    txtSBHamliChrgs.Text = Convert.ToString(custmasterData.HamaliCharges);
                    txtSBNetAmt.Text = Convert.ToString(custmasterData.NetAmount);
                    txtSBPaidAmt.Text = Convert.ToString(custmasterData.PaidAmount);
                    txtSBRemAmt.Text = Convert.ToString(custmasterData.RemainingAmount);
                    tempNetamt = Convert.ToDecimal(custmasterData.NetAmount);
                    tempPaidamt = Convert.ToDecimal(custmasterData.PaidAmount);
                    tempRemamt = Convert.ToDecimal(custmasterData.RemainingAmount);
                    mvat = Convert.ToDecimal(custmasterData.Vat);
                    decimal sbdamt = ((Convert.ToDecimal(custmasterData.TotalAmount) * Convert.ToDecimal(custmasterData.Discount)) / 100);
                    decimal sbvamt = (((Convert.ToDecimal(custmasterData.TotalAmount) - sbdamt) * Convert.ToDecimal(custmasterData.Vat)) / 100);
                    decimal sbcalamt = (Convert.ToDecimal(custmasterData.TotalAmount) - sbdamt + sbvamt + Convert.ToDecimal(custmasterData.TransportCharges) + Convert.ToDecimal(custmasterData.HamaliCharges));
                    if (sbcalamt == tempNetamt)
                        intamtchk = 0;
                    else
                        intamtchk = 1;
                    creditstatus = Convert.ToString(custmasterData.YN);
                    BillMasterID = custmasterData.Id;
                    var billDetailData = custmasterData.CustomerBillDetails;
                    dtgvProdutDetail.Rows.Clear();
                    int i = 0;
                    foreach (var item in billDetailData)
                    {
                        if (item.AssemblyChildStatus == "N" || item.AssemblyChildStatus == null || item.AssemblyChildStatus == "")
                        {
                            dtgvProdutDetail.Rows.Add();
                            dtgvProdutDetail.Rows[i].Cells["Id"].Value = item.Id;
                            dtgvProdutDetail.Rows[i].Cells["ProductNameg"].Value = item.ProductId;
                            dtgvProdutDetail.Rows[i].Cells["Company"].Value = item.CompanyName;
                            dtgvProdutDetail.Rows[i].Cells["Unit"].Value = item.Unit;
                            if (item.SaleUnit != null)
                                dtgvProdutDetail.Rows[i].Cells["SaleUnit"].Value = item.SaleUnit;
                            else
                                dtgvProdutDetail.Rows[i].Cells["SaleUnit"].Value = item.Unit;
                            if (item.Unit != item.SaleUnit && item.SaleUnit != null)
                            {
                                ProductRepository productrepo = new ProductRepository();
                                var productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductId && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit).FirstOrDefault();
                                dtgvProdutDetail.Rows[i].Cells["Quantity1"].Value = Convert.ToDecimal(item.Quantity) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                            }
                            else
                                dtgvProdutDetail.Rows[i].Cells["Quantity1"].Value = item.Quantity;
                            dtgvProdutDetail.Rows[i].Cells["Code"].Value = item.Code;
                            dtgvProdutDetail.Rows[i].Cells["Rate"].Value = item.Rate;
                            dtgvProdutDetail.Rows[i].Cells["Amount"].Value = item.Amount;
                            dtgvProdutDetail.Rows[i].Cells["Disc"].Value = item.Discount;
                            dtgvProdutDetail.Rows[i].Cells["SpDisc"].Value = item.SpcDisc;
                            dtgvProdutDetail.Rows[i].Cells["SGST"].Value = item.SGstPercent;
                            dtgvProdutDetail.Rows[i].Cells["SGSTAmt"].Value = item.SGstAmt;
                            dtgvProdutDetail.Rows[i].Cells["CGST"].Value = item.CGstPercent;
                            dtgvProdutDetail.Rows[i].Cells["CGSTAmt"].Value = item.CGstAmt;
                            dtgvProdutDetail.Rows[i].Cells["IGST"].Value = item.IGstPercent;
                            dtgvProdutDetail.Rows[i].Cells["IGSTAmt"].Value = item.IGstAmt;
                            dtgvProdutDetail.Rows[i].Cells["Cess"].Value = item.Cess;
                            dtgvProdutDetail.Rows[i].Cells["CessAmt"].Value = item.CessAmt;
                            dtgvProdutDetail.Rows[i].Cells["SaleRate"].Value = item.SaleRate;
                            dtgvProdutDetail.Rows[i].Cells["HSNCode"].Value = item.HSNCode;
                            dtgvProdutDetail.Rows[i].Cells["TotalAmount"].Value = item.TotalAmount;
                            if (mvat != 0)
                                mdvat = mdvat + Convert.ToDecimal(item.Vat);
                            i++;
                        }
                    }
                    btnsave.Enabled = true;
                    btnUpdate.Enabled = false;
                    CustCreditDebitNoteMasterRepository SaleRMRepo = new CustCreditDebitNoteMasterRepository();
                    CustCreditDebitMaster srmdata = new CustCreditDebitMaster();
                    if (cdtype == "CN")
                        srmdata = SaleRMRepo.GetAll().Where(t => (t.BillNo == Convert.ToString(billno) || t.BillNo == Convert.ToString(custmasterData.BillNo)) && t.CDType == "Credit Note" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    else if (cdtype == "DN")
                        srmdata = SaleRMRepo.GetAll().Where(t => (t.BillNo == Convert.ToString(billno) || t.BillNo == Convert.ToString(custmasterData.BillNo)) && t.CDType == "Debit Note" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (srmdata != null)
                    {
                        printId = srmdata.CustCreditDebitNo.ToString();
                        txtsrno.Text = srmdata.CustCreditDebitNo.ToString();
                        dtpsrdate.Value = srmdata.CustCreditDebitDate.Value;
                        txtTotalAmt.Text = srmdata.TotalAmount.ToString();
                        txtNetAmt.Text = srmdata.NetAmount.ToString();
                        var srddata = srmdata.CustCreditDebitDetails;
                        gvSaleReturnDetails.Rows.Clear();
                        int j = 0;
                        foreach (var item in srddata)
                        {
                            if (item.AssemblyChildStatus == "N" || item.AssemblyChildStatus == null || item.AssemblyChildStatus == "")
                            {
                                gvSaleReturnDetails.Rows.Add();
                                gvSaleReturnDetails.Rows[j].Cells["CDId"].Value = item.Id;
                                gvSaleReturnDetails.Rows[j].Cells["CDProductName"].Value = item.ProductId;
                                gvSaleReturnDetails.Rows[j].Cells["CDCompany"].Value = item.CompanyName;
                                gvSaleReturnDetails.Rows[j].Cells["CDUnit"].Value = item.Unit;
                                if (item.SaleUnit != null)
                                    gvSaleReturnDetails.Rows[j].Cells["CDSaleUnit"].Value = item.SaleUnit;
                                else
                                    gvSaleReturnDetails.Rows[j].Cells["CDSaleUnit"].Value = item.Unit;
                                gvSaleReturnDetails.Rows[j].Cells["CDAmount"].Value = item.Amount;
                                gvSaleReturnDetails.Rows[j].Cells["CDSGST"].Value = item.SGstPercent;
                                gvSaleReturnDetails.Rows[j].Cells["CDSGSTAmt"].Value = item.SGstAmt;
                                gvSaleReturnDetails.Rows[j].Cells["CDCGST"].Value = item.CGstPercent;
                                gvSaleReturnDetails.Rows[j].Cells["CDCGSTAmt"].Value = item.CGstAmt;
                                gvSaleReturnDetails.Rows[j].Cells["CDIGST"].Value = item.IGstPercent;
                                gvSaleReturnDetails.Rows[j].Cells["CDIGSTAmt"].Value = item.IGstAmt;
                                gvSaleReturnDetails.Rows[j].Cells["CDCess"].Value = item.Cess;
                                gvSaleReturnDetails.Rows[j].Cells["CDCessAmt"].Value = item.CessAmt;
                                gvSaleReturnDetails.Rows[j].Cells["CDTotalAmt"].Value = item.TotalAmount;
                                gvSaleReturnDetails.Rows[j].Cells["Reason"].Value = item.Reason;
                                gvSaleReturnDetails.Rows[j].Cells["CDBNID"].Value = item.BNID;
                                gvSaleReturnDetails.Rows[j].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                j++;
                            }
                        }
                        btnsave.Enabled = false;
                        btnUpdate.Enabled = true;
                        printId = txtsrno.Text.ToString();
                    }
                    Custmasterrepo.Dispose();
                    SaleRMRepo.Dispose();
                }
                else
                {
                    MessageBox.Show("Fake Bill not Valid for return.","Warning");
                    txtbillno.Text = "";
                    this.ActiveControl = txtbillno;
                }
                if (btnUpdate.Enabled == true)
                    btnDelete.Enabled = true;
            }
            catch (Exception)
            { }
        }

        private void txtbillno_Leave(object sender, EventArgs e)
        {
            try
            {
                dtgvProdutDetail.Rows.Clear();
                gvSaleReturnDetails.Rows.Clear();
                maxbillid();
                txtSBtotalamt.Text = "0";
                txtSBDisc.Text = "0";
                txtSBTransChrg.Text = "0";
                txtSBHamliChrgs.Text = "0";
                txtSBNetAmt.Text = "0";
                txtSBPaidAmt.Text = "0";
                txtSBRemAmt.Text = "0";
                txtTotalAmt.Text = "0";
                txtNetAmt.Text = "0";
                if (txtbillno.Text != "")
                    billdata(txtbillno.Text);
            }
            catch (Exception)
            { }
        }

        private void txtTotalAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTotalAmt.Text != "")
                    SRCalculation();
            }
            catch (Exception)
            { }
        }

        public void SRCalculation()
        {
            try
            {
                decimal tamt = 0, d = 0, v = 0, damt = 0, vamt = 0, namt = 0;
                tamt = Convert.ToDecimal(txtTotalAmt.Text);
                damt = tamt * d / 100;
                if (intamtchk == 0)
                    vamt = (tamt - damt) * v / 100;
                namt = Convert.ToDecimal(Math.Round(tamt, 2, MidpointRounding.AwayFromZero)) - Convert.ToDecimal(Math.Round(damt, 2, MidpointRounding.AwayFromZero)) + Convert.ToDecimal(Math.Round(vamt, 2, MidpointRounding.AwayFromZero));
                txtNetAmt.Text = Convert.ToString(Math.Round(namt, 2, MidpointRounding.AwayFromZero));
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
                fnetamt = Convert.ToDecimal(netamt) + Convert.ToDecimal(ramt);
                txtNetAmt.Text = Convert.ToString(Math.Round(fnetamt, 2, MidpointRounding.AwayFromZero));
            }
            catch (Exception)
            { }
        }

        private void txtTaxableamt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                getGstCalculation();
            }
            catch (Exception)
            { }
        }        

        private void txtbillno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtgvProdutDetail;
            }
            catch (Exception)
            { }
        }

        private void txtTaxableamt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbreason;
            }
            catch (Exception)
            { }
        }

        private void cmbreason_KeyDown(object sender, KeyEventArgs e)
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
                RptCustomerCreditDebitNote custCDRpt = new RptCustomerCreditDebitNote(Convert.ToInt32(printId));
                custCDRpt.ShowDialog();
            }
            catch (Exception) { }
        }

        private void dtpsrdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtbillno;
            }
            catch (Exception) { }
        }

        private void btnbilllist_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                creditdebitnote = this;
                frmCustomerCreditDebitNote p = new frmCustomerCreditDebitNote();
                p.Visible = false;
                string sType = "";
                if (cdtype == "CN")
                    sType = "Credit Note";
                else
                    sType = "Debit Note";
                frmCustomerCerditDebitNoteList crdrlist = new frmCustomerCerditDebitNoteList(sType);
                crdrlist.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbcustomername.Text != "Select")
                {
                    if (gvSaleReturnDetails.Rows.Count != 0)
                    {
                        if (billtype == 1 || billtype == 2 || billtype == 3)
                            updatemaster();
                        if (deleteid.Count > 0)
                        {
                            for (int x = 0; x < deleteid.Count; x++)
                            {
                                CustCreditDebitNoteDetailRepository suppCDNoteRepo = new CustCreditDebitNoteDetailRepository();
                                var suppCDNoteData = suppCDNoteRepo.GetAll().Where(t => t.Id == deleteid[x]).FirstOrDefault();
                                ProductRepository proRepo = new ProductRepository();
                                var pData = proRepo.GetAll().Where(t => t.ProductName == suppCDNoteData.ProductId && t.ManufactureCompany == suppCDNoteData.CompanyName && t.Uniti == suppCDNoteData.Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (pData != null)
                                {
                                    if (pData.ProductType == 2)
                                    {
                                        AssemeblyProductDetailsRepository assemblyDRepo = new AssemeblyProductDetailsRepository();
                                        List<tblAssemblyProductDetail> assemblyList = assemblyDRepo.GetAll().Where(t => t.AssemblyProductId == pData.ID).ToList();
                                        if (assemblyList.Count > 0)
                                        {
                                            int MasterId = 0;
                                            MasterId = Convert.ToInt32(suppCDNoteData.CustCreditDebitNo);
                                            foreach (var itemAssembly in assemblyList)
                                            {
                                                suppCDNoteRepo = new CustCreditDebitNoteDetailRepository();
                                                var sDData = suppCDNoteRepo.GetAll().Where(t => t.ProductId == itemAssembly.ProductName && t.CompanyName == itemAssembly.CompanyName && t.Unit == itemAssembly.Unit && t.CustCreditDebitNo == MasterId).FirstOrDefault();
                                                if (sDData != null)
                                                {
                                                    CustCreditDebitNoteDetailRepository suppCDNoteRepo1 = new CustCreditDebitNoteDetailRepository();
                                                    CustCreditDebitDetail sDetailData = suppCDNoteRepo1.GetById(sDData.Id);
                                                    suppCDNoteRepo1.Remove(sDetailData);
                                                    suppCDNoteRepo1.Save();
                                                }
                                            }
                                        }
                                        else
                                        { }
                                        suppCDNoteRepo = new CustCreditDebitNoteDetailRepository();
                                        CustCreditDebitDetail suppDData = suppCDNoteRepo.GetById(deleteid[x]);
                                        suppCDNoteRepo.Remove(suppDData);
                                        suppCDNoteRepo.Save();
                                    }
                                    else
                                    {
                                        suppCDNoteRepo = new CustCreditDebitNoteDetailRepository();
                                        CustCreditDebitDetail suppDData = suppCDNoteRepo.GetById(deleteid[x]);
                                        suppCDNoteRepo.Remove(suppDData);
                                        suppCDNoteRepo.Save();
                                    }
                                }
                            }
                        }
                        for (int i = 0; i < gvSaleReturnDetails.Rows.Count; i++)
                        {
                            int Rowid = Convert.ToInt32(gvSaleReturnDetails.Rows[i].Cells["CDId"].Value);
                            if (Rowid != 0)
                            {
                                if (billtype == 1 || billtype == 2 || billtype == 3)
                                    updatedetail(i, Rowid, "update");
                            }
                            else
                            {
                                if (billtype == 1 || billtype == 2 || billtype == 3)
                                    updatedetail(i, Rowid, "save");
                            }
                        }
                        if (billtype == 1 || billtype == 2)
                            updateremainigpayment("Cash");
                        MessageBox.Show("Credit debit Note Updated.", "Success");
                        clear();
                        this.ActiveControl = txtbillno;
                    }
                    else
                        MessageBox.Show("Add Product Information.", "Warning");
                }
                else
                {
                    if (cmbcustomername.Text == "Select")
                    {
                        MessageBox.Show("Select Customer Name.","Warning");
                        this.ActiveControl = cmbcustomername;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to Delete this Customer Credit Debit Note ?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    AIOInventorySystem.Data.Model.Stock sdata = new Data.Model.Stock();
                    string CDType = "", Suppliername = "", DeletedBillNo = "";
                    int CDNMId = 0;
                    decimal NetAmount = 0;
                    CustCreditDebitNoteMasterRepository custCDNRepo = new CustCreditDebitNoteMasterRepository();
                    var custCDNdata = custCDNRepo.GetAll().Where(t => t.CustCreditDebitNo == Convert.ToInt32(printId) && t.BillNo == custbillno && t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    if (custCDNdata != null)
                    {
                        CDType = custCDNdata.CDType;
                        // add details for history table
                        try
                        {
                            DeletedBillNo = Convert.ToString(custCDNdata.CustCreditDebitNo);
                            Suppliername = Convert.ToString(custCDNdata.CustomerName);
                            NetAmount = Convert.ToDecimal(custCDNdata.NetAmount);
                            DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                            DeleteHistoryTable historydata = new DeleteHistoryTable();
                            historydata.DeleteType = "Customer" + " " + CDType;
                            historydata.BillNo = DeletedBillNo;
                            historydata.SupplierOrCustomerName = Suppliername;
                            historydata.Amount = Convert.ToDecimal(NetAmount);
                            historydata.CurrentDate = DateTime.Now.Date;
                            historydata.BillDate = Convert.ToDateTime(custCDNdata.CustCreditDebitDate);
                            historydata.CompId = CommonMethod.CompId;
                            historydata.TransactionYear = CommonMethod.TransactionYear;
                            historyRepo.Add(historydata);
                            historyRepo.Save();
                        }
                        catch (Exception) { }

                        CDNMId = Convert.ToInt32(custCDNdata.Id);
                        RemainingPaymentRepository RemPayRepo = new RemainingPaymentRepository();
                        var remData = RemPayRepo.GetAll().Where(t => t.BillId == custbillno && t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.Type == CDType && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                        if (remData != null)
                        {
                            decimal decremamt = 0;
                            int ReceiptId = RemPayRepo.GetAll().Where(t => t.BillId == custbillno && t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.Type == CDType && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().Id;
                            RemainingPaymentRepository remRepo = new RemainingPaymentRepository();
                            RemainingPayment remdata = remRepo.GetById(ReceiptId);
                            remRepo.Remove(remdata);
                            remRepo.Save();
                            if (remdata.PaymentMode != "Cash")
                            {
                                BankStatementRepository Brepo = new BankStatementRepository();
                                BankStatement bdata = new BankStatement();
                                if (remdata.PaymentMode == "Cheque")
                                    bdata = Brepo.GetAll().Where(t => t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.Chequeno && t.Deposits == remdata.PaidAmt && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                else
                                    bdata = Brepo.GetAll().Where(t => t.Bankname == remdata.Bankname && t.TransactionDate.Value.ToShortDateString() == remdata.CreateDate.Value.ToShortDateString() && t.ChequeNo == remdata.Chequeno && t.Deposits == remdata.PaidAmt && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (bdata != null)
                                {
                                    BankStatement bsdata = Brepo.GetById(bdata.Id);
                                    Brepo.Remove(bsdata);
                                    Brepo.Save();
                                }
                            }
                            RemainingPaymentRepository remainingpayrepo1 = new RemainingPaymentRepository();
                            RemainingPayment remainingdata1 = new RemainingPayment();
                            remainingdata1 = remainingpayrepo1.GetAll().Where(t => t.CustomerId == Convert.ToInt32(remdata.CustomerId) && t.Id < remdata.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                            if (remainingdata1 != null)
                                decremamt = Convert.ToDecimal(remainingdata1.RemainingAmt);
                            else
                                decremamt = 0;
                            RemainingPaymentRepository remainingpayrepo2 = new RemainingPaymentRepository();
                            List<RemainingPayment> remainingdata2 = new List<RemainingPayment>();
                            remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(remdata.CustomerId) && t.Id > remdata.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                            int i = 0;
                            foreach (var item in remainingdata2)
                            {
                                decremamt = (Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt)) + decremamt;
                                item.RemainingAmt = decremamt;                                
                                remainingpayrepo2.Edit(item);
                                remainingpayrepo2.Save();
                                i++;
                            }
                        }
                        db.connect();
                        DataTable dt = db.GetTable("Delete from CustCreditDebitDetail where CustCreditDebitNo='" + CDNMId + "'");
                        DataTable dt1 = db.GetTable("Delete from CustCreditDebitMaster where Id='" + CDNMId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                        MessageBox.Show("Record Deleted Successfully.", "Success");
                        dt.Dispose(); dt1.Dispose();
                        db.CloseConnection();
                        custCDNRepo.Dispose();
                        clear();
                        dtgvProdutDetail.Rows.Clear();
                        gvSaleReturnDetails.Rows.Clear();
                        this.ActiveControl = txtbillno;
                        btnDelete.Enabled = false;
                    }
                }
            }
            catch (Exception) { }
        }

        private void dtgvProdutDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CustomerBillDetailRepository Custdetailrepo = new CustomerBillDetailRepository();
                CustomerBillDetail custbilldata = new CustomerBillDetail();
                qdid = Convert.ToInt32(dtgvProdutDetail.Rows[e.RowIndex].Cells["Id"].Value);
                custbilldata = Custdetailrepo.GetById(qdid);
                if (custbilldata.Status != "Inactive")
                {
                    sbcolIndex = e.RowIndex;
                    grdqty = Convert.ToDecimal(dtgvProdutDetail.Rows[e.RowIndex].Cells["Quantity1"].Value);
                    txtProductname.Text = Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["ProductNameg"].Value) + "," + Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["Company"].Value) + "," + Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["Unit"].Value);
                    cmbUnit.Items.Clear();
                    cmbUnit.DisplayMember = "Text";
                    cmbUnit.ValueMember = "Value";
                    cmbUnit.Items.Add(new { Text = Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["Unit"].Value), Value = Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["Unit"].Value) });
                    if (Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["SaleUnit"].Value) != Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["Unit"].Value))
                        cmbUnit.Items.Add(new { Text = Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["SaleUnit"].Value), Value = Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["SaleUnit"].Value) });
                    cmbUnit.Text = Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["SaleUnit"].Value);
                    cmbsgst.Text = Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["SGST"].Value);
                    cmbcgst.Text = Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["CGST"].Value);
                    cmbigst.Text = Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["IGST"].Value);
                    txtpCess.Text = Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["Cess"].Value);
                    ProductRepository ProductRepo = new ProductRepository();
                    ProductInformation ProductData = new ProductInformation();
                    if (CommonMethod.commProduct == true)
                        ProductData = ProductRepo.GetAll().Where(t => t.ProductName == Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["ProductNameg"].Value) && t.ManufactureCompany == Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["Company"].Value) && t.Uniti == Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["Unit"].Value)).FirstOrDefault();
                    else
                        ProductData = ProductRepo.GetAll().Where(t => t.ProductName == Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["ProductNameg"].Value) && t.ManufactureCompany == Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["Company"].Value) && t.Uniti == Convert.ToString(dtgvProdutDetail.Rows[e.RowIndex].Cells["Unit"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (ProductData != null)
                        ProductCategory = ProductData.Category;
                    this.ActiveControl = txtTaxableamt;
                }
                else
                    qdid = 0;
                Custdetailrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void dtgvProdutDetail_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtTaxableamt;
            }
            catch (Exception) { }
        }

        private void gvSaleReturnDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvSaleReturnDetails.Columns[e.ColumnIndex].HeaderText == "Remove")
                {
                    if (btnsave.Enabled == true)
                    {
                        if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        {
                            gvSaleReturnDetails.Rows.Remove(gvSaleReturnDetails.Rows[e.RowIndex]);
                            totalamt = 0;
                            totalamt = gvSaleReturnDetails.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["CDTotalAmt"].Value));
                            txtTotalAmt.Text = Convert.ToString(Math.Round(totalamt, 2, MidpointRounding.AwayFromZero));
                        }
                    }
                    else if (gvSaleReturnDetails.Rows.Count != 1 && btnsave.Enabled == false)
                    {
                        if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        {
                            deleteid.Add(Convert.ToInt32(gvSaleReturnDetails.Rows[e.RowIndex].Cells["CDId"].Value));
                            gvSaleReturnDetails.Rows.Remove(gvSaleReturnDetails.Rows[e.RowIndex]);
                            totalamt = 0;
                            totalamt = gvSaleReturnDetails.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["CDTotalAmt"].Value));
                            txtTotalAmt.Text = Convert.ToString(Math.Round(totalamt, 2, MidpointRounding.AwayFromZero));
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