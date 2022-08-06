using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmQuotation : Form
    {
        public static frmQuotation quainfo;
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public decimal totalamt = 0, vat5amt, vat12amt, tpuramt = 0;
        public static decimal setvat1 = 0, setvat2 = 0, NewSizeQty = 0, qtySlab = 0, SizeQty = 0;
        private int DynamicDtlSlabId = 0, FormulaConstCount = 0, intcell = 0;
        public int quatno, colIndex = -1, QuotationNo, flag = 0, codeflag = 0, formulaCount = 0;
        public string CustomerType, ProductCategory, taxinclude, HasMandatoryCodeSr = "Y", subtitlestring, RateDecrease = "", RateIncrease = "", getSlabUnit = "",
            SizeSpecString = "";
        private static bool ismandatory = false, refreshFlag = false, getFormula = false;
        List<int> deleteid = new List<int>();
        private bool PrefixToBind = false, prefixFilled = false;
        private static char slabGroup;
        private decimal[] arrSlab = new decimal[20];

        public frmQuotation()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            SettingRepository SettingRepo = new SettingRepository();
            Setting SetData = new Setting();
            SetData = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (SetData.VatOption == "Exclusive")
            {
                setvat1 = Convert.ToDecimal(SetData.Vat1);
                setvat2 = Convert.ToDecimal(SetData.Vat2);
            }
            if (SetData.CompositionScheme == "True")
            {
                cmbsgst.Enabled = false;
                txtVat.Enabled = false;
                cmbcgst.Enabled = false;
                cmbigst.Enabled = false;
            }
            maxbillid();
            fillcompany();
            customerdata();
            fillgst();
            btnUpdate.Enabled = false;
            deleteid.Clear();
        }

        public frmQuotation(int Qno)
        {
            QuotationNo = Qno;
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            SettingRepository SettingRepo = new SettingRepository();
            Setting SetData = new Setting();
            SetData = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (SetData.VatOption == "Exclusive")
            {
                setvat1 = Convert.ToDecimal(SetData.Vat1);
                setvat2 = Convert.ToDecimal(SetData.Vat2);
            }
            if (SetData.CompositionScheme == "True")
            {
                cmbsgst.Enabled = false;
                txtVat.Enabled = false;
                cmbcgst.Enabled = false;
                cmbigst.Enabled = false;
            }
            maxbillid();
            fillcompany();
            customerdata();
            fillgst();
            btnUpdate.Enabled = false;
            deleteid.Clear();
            QData(QuotationNo);
            this.ActiveControl = btnUpdate;
        }

        public frmQuotation(int Qno, string md)
        {
            QuotationNo = Qno;
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            SettingRepository SettingRepo = new SettingRepository();
            Setting SetData = new Setting();
            SetData = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (SetData.VatOption == "Exclusive")
            {
                setvat1 = Convert.ToDecimal(SetData.Vat1);
                setvat2 = Convert.ToDecimal(SetData.Vat2);
            }
            if (SetData.CompositionScheme == "True")
            {
                cmbsgst.Enabled = false;
                txtVat.Enabled = false;
                cmbcgst.Enabled = false;
                cmbigst.Enabled = false;
            }
            maxbillid();
            fillcompany();
            customerdata();
            fillgst();
            deleteid.Clear();
            btnUpdate.Enabled = false;
            QData(QuotationNo, md);
        }

        public void QData(int Qno, string md)
        {
            try
            {
                vat5amt = 0; vat12amt = 0; tpuramt = 0;
                QuotationMasterRepository Qmasterrepo = new QuotationMasterRepository();
                var custmasterData = Qmasterrepo.GetAll().Where(t => t.Id == Convert.ToInt32(Qno) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                dtpquotationdate.Value = custmasterData.Quotationdate.Value;
                txtTotalAmt.Text = Convert.ToString(custmasterData.TotalAmount);
                txtDiscount.Text = Convert.ToString(custmasterData.Discount);
                txtVat.Text = Convert.ToString(custmasterData.Vat);
                txttranchrg.Text = Convert.ToString(custmasterData.TransportCharges);
                txthamalichrg.Text = Convert.ToString(custmasterData.HamaliCharges);
                txtNetAmt.Text = Convert.ToString(custmasterData.NetAmount);
                var billDetailData = custmasterData.QuotationDetails;
                GvProductInfo.Rows.Clear();
                int i = 0;
                foreach (var item in billDetailData)
                {
                    GvProductInfo.Rows.Add();
                    GvProductInfo.Rows[i].Cells["Id"].Value = item.Id;
                    GvProductInfo.Rows[i].Cells["ProductNameg"].Value = item.ProductId;
                    GvProductInfo.Rows[i].Cells["Company"].Value = item.CompanyName;
                    GvProductInfo.Rows[i].Cells["Unit"].Value = item.Unit;
                    if (item.SaleUnit != null)
                        GvProductInfo.Rows[i].Cells["SaleUnit"].Value = item.SaleUnit;
                    else
                        GvProductInfo.Rows[i].Cells["SaleUnit"].Value = item.Unit;
                    if (item.Unit != item.SaleUnit && item.SaleUnit != null)
                    {
                        ProductRepository productrepo = new ProductRepository();
                        ProductInformation productdata = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductId && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit).FirstOrDefault();
                        else
                            productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductId && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        GvProductInfo.Rows[i].Cells["Quantityg"].Value = Convert.ToDecimal(item.Quantity) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                    }
                    else
                        GvProductInfo.Rows[i].Cells["Quantityg"].Value = Convert.ToDouble(item.Quantity);
                    GvProductInfo.Rows[i].Cells["Code"].Value = item.Code;
                    GvProductInfo.Rows[i].Cells["Rate"].Value = item.Rate;
                    GvProductInfo.Rows[i].Cells["Amount"].Value = item.Amount;
                    GvProductInfo.Rows[i].Cells["Disc"].Value = item.Discount;
                    GvProductInfo.Rows[i].Cells["SGST"].Value = item.SGstPercent;
                    GvProductInfo.Rows[i].Cells["SGSTAmt"].Value = item.SGstAmt;
                    GvProductInfo.Rows[i].Cells["CGST"].Value = item.CGstPercent;
                    GvProductInfo.Rows[i].Cells["CGSTAmt"].Value = item.CGstAmt;
                    GvProductInfo.Rows[i].Cells["IGST"].Value = item.IGstPercent;
                    GvProductInfo.Rows[i].Cells["IGSTAmt"].Value = item.IGstAmt;
                    GvProductInfo.Rows[i].Cells["TotalAmount"].Value = item.TotalAmount;
                    GvProductInfo.Rows[i].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                    GvProductInfo.Rows[i].Cells["HSNCode"].Value = item.HSNCode;
                    GvProductInfo.Rows[i].Cells["SaleRate"].Value = (item.SaleRate == null ? item.Rate : item.SaleRate);
                    GvProductInfo.Rows[i].Cells["Subtitle"].Value = item.Prodsubtitle;
                    i++;
                }
                btnsave.Enabled = true;
                Qmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void QData(int Qno)
        {
            try
            {
                vat5amt = 0; vat12amt = 0; tpuramt = 0;
                QuotationMasterRepository Qmasterrepo = new QuotationMasterRepository();
                var custmasterData = Qmasterrepo.GetAll().Where(t => t.Id == Convert.ToInt32(Qno) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                txtquotationno.Text = Convert.ToString(custmasterData.QuotationNo);
                dtpquotationdate.Value = custmasterData.Quotationdate.Value;
                txtTotalAmt.Text = Convert.ToString(custmasterData.TotalAmount);
                txtDiscount.Text = Convert.ToString(custmasterData.Discount);
                txtVat.Text = Convert.ToString(custmasterData.Vat);
                txttranchrg.Text = Convert.ToString(custmasterData.TransportCharges);
                txthamalichrg.Text = Convert.ToString(custmasterData.HamaliCharges);
                txtNetAmt.Text = Convert.ToString(custmasterData.NetAmount);
                cmbcustomername.Text = custmasterData.CustomerName;
                QuotationNo = custmasterData.Id;
                quatno = Convert.ToInt32(txtquotationno.Text);
                var billDetailData = custmasterData.QuotationDetails;
                GvProductInfo.Rows.Clear();
                int i = 0;
                foreach (var item in billDetailData)
                {
                    GvProductInfo.Rows.Add();
                    GvProductInfo.Rows[i].Cells["Id"].Value = item.Id;
                    GvProductInfo.Rows[i].Cells["ProductNameg"].Value = item.ProductId;
                    GvProductInfo.Rows[i].Cells["Company"].Value = item.CompanyName;
                    GvProductInfo.Rows[i].Cells["Unit"].Value = item.Unit;
                    if (item.SaleUnit != null)
                        GvProductInfo.Rows[i].Cells["SaleUnit"].Value = item.SaleUnit;
                    else
                        GvProductInfo.Rows[i].Cells["SaleUnit"].Value = item.Unit;
                    if (item.Unit != item.SaleUnit && item.SaleUnit != null)
                    {
                        ProductRepository productrepo = new ProductRepository();
                        ProductInformation productdata = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductId && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit).FirstOrDefault();
                        else
                            productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductId && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        GvProductInfo.Rows[i].Cells["Quantityg"].Value = Convert.ToDecimal(item.Quantity) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                    }
                    else
                        GvProductInfo.Rows[i].Cells["Quantityg"].Value = Convert.ToDecimal(item.Quantity);
                    GvProductInfo.Rows[i].Cells["Code"].Value = item.Code;
                    GvProductInfo.Rows[i].Cells["Rate"].Value = item.Rate;
                    GvProductInfo.Rows[i].Cells["Amount"].Value = item.Amount;
                    GvProductInfo.Rows[i].Cells["Disc"].Value = item.Discount;
                    GvProductInfo.Rows[i].Cells["SGST"].Value = item.SGstPercent;
                    GvProductInfo.Rows[i].Cells["SGSTAmt"].Value = item.SGstAmt;
                    GvProductInfo.Rows[i].Cells["CGST"].Value = item.CGstPercent;
                    GvProductInfo.Rows[i].Cells["CGSTAmt"].Value = item.CGstAmt;
                    GvProductInfo.Rows[i].Cells["IGST"].Value = item.IGstPercent;
                    GvProductInfo.Rows[i].Cells["IGSTAmt"].Value = item.IGstAmt;
                    GvProductInfo.Rows[i].Cells["TotalAmount"].Value = item.TotalAmount;
                    GvProductInfo.Rows[i].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                    GvProductInfo.Rows[i].Cells["HSNCode"].Value = item.HSNCode;
                    GvProductInfo.Rows[i].Cells["SaleRate"].Value = (item.SaleRate == null ? item.Rate : item.SaleRate);
                    GvProductInfo.Rows[i].Cells["Subtitle"].Value = item.Prodsubtitle;
                    try
                    {
                        db.connect();
                        SqlCommand command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = item.ProductId;
                        command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = item.CompanyName;
                        command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = item.Unit;
                        command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                        command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = '0';
                        command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        SqlDataReader reader = command.ExecuteReader();
                        DataTable dtRate = new DataTable();
                        dtRate.Load(reader);
                        decimal avgprice = 0;
                        avgprice = Convert.ToDecimal(dtRate.Rows[0][0].ToString());
                        dtRate.Dispose();
                        decimal qty = Convert.ToDecimal(item.Quantity);
                        tpuramt = tpuramt + (avgprice * qty);
                        db.CloseConnection();
                    }
                    catch (Exception)
                    { db.CloseConnection(); }
                    i++;
                }
                lbltpuramt.Text = Convert.ToString(Math.Round(tpuramt, 0, MidpointRounding.AwayFromZero));
                if (Convert.ToInt32(custmasterData.Status) == 0)
                {
                    btnprint.Enabled = true;
                    btnsave.Enabled = false;
                    btnUpdate.Enabled = false;
                }
                else
                {
                    btnprint.Enabled = true;
                    btnsave.Enabled = false;
                    btnUpdate.Enabled = true;
                    SettingRepository settRepo = new SettingRepository();
                    Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (sdata.DeleteSaleBill == "True")
                        btnDelete.Enabled = true;
                    else
                        btnDelete.Enabled = false;
                }
                Qmasterrepo.Dispose();
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

        public void maxbillid()
        {
            try
            {
                QuotationMasterRepository Qmasterrepo = new QuotationMasterRepository();
                var mid = Qmasterrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.QuotationNo);
                if (mid == null)
                    txtquotationno.Text = "1";
                else
                    txtquotationno.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                Qmasterrepo.Dispose();
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

        public void fillcompany()
        {
            try
            {
                prefixFilled = false;
                ProductRepository custrepo = new ProductRepository();
                List<ProductInformation> customerdata = new List<ProductInformation>();
                customerdata.Clear();
                if (CommonMethod.commProduct == true)
                    customerdata = custrepo.GetAll().Where(t => t.Prefix != null).GroupBy(t => t.Prefix).Select(t => t.FirstOrDefault()).OrderByDescending(t => t.Prefix).ToList();
                else
                    customerdata = custrepo.GetAll().Where(t => t.Prefix != null && t.CompId == CommonMethod.CompId).GroupBy(t => t.Prefix).Select(t => t.FirstOrDefault()).OrderByDescending(t => t.Prefix).ToList();
                customerdata.Add(new ProductInformation { Prefix = "Select ", ProductCode = 0 });
                customerdata.Reverse();
                cmbcomanyname.DataSource = customerdata;
                cmbcomanyname.ValueMember = "ProductCode";
                cmbcomanyname.DisplayMember = "Prefix";
                custrepo.Dispose();
                prefixFilled = true;
                cmbcomanyname.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        private void frmQuotation_Load(object sender, EventArgs e)
        {
            if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Height;
            if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            try
            {
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
                        {
                            MyCollection.Add(reader.GetString(0) + "," + reader.GetString(1) + "," + reader.GetString(2));
                        }
                        catch (Exception)
                        { }
                    }
                    txtProductname.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
                txtprefixproduct.Visible = false;
                fillProductSubtitles();
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtQuantity.Text != "")
                {
                    getGstCalculation();
                    try
                    {
                        decimal rate;
                        if (txtrate.Text == "")
                            rate = 0;
                        else
                            rate = Convert.ToDecimal(txtrate.Text);
                        decimal amt = rate * Convert.ToDecimal(txtQuantity.Text);
                        txtAmount.Text = Convert.ToString(amt);
                        PCalculation();
                    }
                    catch (Exception)
                    { }
                }
                else
                {
                    txtQuantity.Text = "0";
                    this.ActiveControl = txtQuantity;
                }
            }
            catch (Exception)
            { }
        }

        private void txtrate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtrate.Text != "")
                {
                    decimal rate;
                    if (txtrate.Text == "")
                        rate = 0;
                    else
                        rate = Convert.ToDecimal(txtrate.Text);
                    decimal amt = rate * Convert.ToDecimal(txtQuantity.Text);
                    txtAmount.Text = Convert.ToString(amt);
                    getGstCalculation();
                }
                else
                {
                    txtrate.Text = "0";
                    this.ActiveControl = txtrate;
                }
            }
            catch (Exception)
            { }
        }

        private void txtpdiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(txtpdiscount.Text) <= 100)
                    getGstCalculation();
                else
                {
                    MessageBox.Show("Enter discount between 0 to 100 %.", "Warning");
                    txtpdiscount.Text = "0";
                    this.ActiveControl = txtpdiscount;
                }
            }
            catch (Exception)
            {
                txtpdiscount.Text = "0";
                this.ActiveControl = txtpdiscount;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbcustomername.Text.Trim() != "")
                {
                    if (txtProductname.Text.Trim() != "" && txtQuantity.Text != "")
                    {
                        txtQuantity.Enabled = true;
                        SettingRepository SettingRepo = new SettingRepository();
                        Setting SetData = new Setting();
                        SetData = SettingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (SetData.VatOption == "Exclusive")
                        {
                            setvat1 = Convert.ToDecimal(SetData.Vat1);
                            setvat2 = Convert.ToDecimal(SetData.Vat2);
                        }
                        SettingRepo.Dispose();
                        if (ismandatory == true)
                        {
                            for (int i = 0; i < dtgvcode.RowCount; i++)
                            {
                                if (dtgvcode.Rows[i].Cells[0].Value.ToString() == "")
                                {
                                    MessageBox.Show("All Quantity must have Serial No or Code.", "Warning");
                                    dtgvcode.Visible = true;
                                    this.ActiveControl = dtgvcode;
                                    dtgvcode.CurrentCell = dtgvcode[0, i];
                                    dtgvcode.CurrentCell.Selected = true;
                                    goto a;
                                }
                            }
                        }
                        if (colIndex != -1)
                        {
                            string Proname = txtProductname.Text.Trim().Split(',')[0];
                            string company = txtProductname.Text.Trim().Split(',')[1];
                            string unit = txtProductname.Text.Trim().Split(',')[2];
                            if (FormulaConstCount == formulaCount)
                                SizeQty = 0;
                            if (txtSubtitle.Text != "" && dtgvFormulae.RowCount == 0) { Proname = txtSubtitle.Text + "," + Proname; }
                            GvProductInfo.Rows[colIndex].Cells["ProductNameg"].Value = Proname;
                            GvProductInfo.Rows[colIndex].Cells["Company"].Value = company;
                            GvProductInfo.Rows[colIndex].Cells["Unit"].Value = unit;
                            GvProductInfo.Rows[colIndex].Cells["SaleUnit"].Value = cmbUnit.Text;
                            GvProductInfo.Rows[colIndex].Cells["Quantityg"].Value = txtQuantity.Text;
                            if (ProductCategory == "Motor")
                                GvProductInfo.Rows[colIndex].Cells["Code"].Value = dtgvcode.Rows[0].Cells[0].Value.ToString();
                            else
                                GvProductInfo.Rows[colIndex].Cells["Code"].Value = "";
                            GvProductInfo.Rows[colIndex].Cells["Rate"].Value = lblBasicRate.Text; // txtrate.Text;  17012018 nilophar
                            GvProductInfo.Rows[colIndex].Cells["Amount"].Value = txtAmount.Text;
                            GvProductInfo.Rows[colIndex].Cells["Disc"].Value = txtpdiscount.Text;
                            GvProductInfo.Rows[colIndex].Cells["SGST"].Value = cmbsgst.Text;
                            GvProductInfo.Rows[colIndex].Cells["SGSTAmt"].Value = lblpvat.Text;
                            GvProductInfo.Rows[colIndex].Cells["CGST"].Value = cmbcgst.Text;
                            GvProductInfo.Rows[colIndex].Cells["CGSTAmt"].Value = lblCGst.Text;
                            GvProductInfo.Rows[colIndex].Cells["IGST"].Value = cmbigst.Text;
                            GvProductInfo.Rows[colIndex].Cells["IGSTAmt"].Value = lblIGst.Text;
                            GvProductInfo.Rows[colIndex].Cells["TotalAmount"].Value = txtpTotalAmt.Text;
                            GvProductInfo.Rows[colIndex].Cells["HSNCode"].Value = txtHSNCode.Text;
                            GvProductInfo.Rows[colIndex].Cells["SaleRate"].Value = txtrate.Text;
                            GvProductInfo.Rows[colIndex].Cells["Subtitle"].Value = txtSubtitle.Text;
                            colIndex = -1;
                            totalamt = 0;
                            totalamt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["TotalAmount"].Value));
                            txtTotalAmt.Text = Convert.ToString(Math.Round(totalamt, 2, MidpointRounding.AwayFromZero));
                            clearatADD();
                        }
                        else
                        {
                            txtTotalAmt.Text = "";
                            txtNetAmt.Text = "";
                            txtDiscount.Text = "";
                            txtVat.Text = "";
                            lbldiscount.Text = "0";
                            lblvat.Text = "0";

                            string Proname = txtProductname.Text.Trim().Split(',')[0];
                            string company = txtProductname.Text.Trim().Split(',')[1];
                            string unit = txtProductname.Text.Trim().Split(',')[2];

                            if (ProductCategory == "Motor" && ismandatory == true)
                            {
                                for (int l = 0; l < dtgvcode.RowCount; l++)
                                {
                                    GvProductInfo.Rows.Add();
                                    int i = GvProductInfo.RowCount;
                                    GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                                    GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                                    GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                                    GvProductInfo.Rows[i - 1].Cells["SaleUnit"].Value = cmbUnit.Text;
                                    GvProductInfo.Rows[i - 1].Cells["Quantityg"].Value = "1";
                                    GvProductInfo.Rows[i - 1].Cells["Code"].Value = dtgvcode.Rows[l].Cells[0].Value.ToString();
                                    GvProductInfo.Rows[i - 1].Cells["Rate"].Value = lblBasicRate.Text; //txtrate.text 
                                    GvProductInfo.Rows[i - 1].Cells["Amount"].Value = Convert.ToString(Math.Round(Convert.ToDecimal(txtAmount.Text) / Convert.ToDecimal(txtQuantity.Text), 2, MidpointRounding.AwayFromZero));
                                    if (txtpdiscount.Text == "")
                                        GvProductInfo.Rows[i - 1].Cells["Disc"].Value = 0;
                                    else
                                        GvProductInfo.Rows[i - 1].Cells["Disc"].Value = txtpdiscount.Text;
                                    GvProductInfo.Rows[i - 1].Cells["SGST"].Value = cmbsgst.Text;
                                    GvProductInfo.Rows[i - 1].Cells["SGSTAmt"].Value = Convert.ToString(Convert.ToDecimal(lblpvat.Text) / Convert.ToDecimal(txtQuantity.Text));
                                    GvProductInfo.Rows[i - 1].Cells["CGST"].Value = cmbcgst.Text;
                                    GvProductInfo.Rows[i - 1].Cells["CGSTAmt"].Value = Convert.ToString(Convert.ToDecimal(lblCGst.Text) / Convert.ToDecimal(txtQuantity.Text));
                                    GvProductInfo.Rows[i - 1].Cells["IGST"].Value = cmbigst.Text;
                                    GvProductInfo.Rows[i - 1].Cells["IGSTAmt"].Value = Convert.ToString(Convert.ToDecimal(lblIGst.Text) / Convert.ToDecimal(txtQuantity.Text));
                                    GvProductInfo.Rows[i - 1].Cells["TotalAmount"].Value = Convert.ToDecimal(txtpTotalAmt.Text) / Convert.ToDecimal(txtQuantity.Text);
                                    GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                    GvProductInfo.Rows[i - 1].Cells["HSNCode"].Value = txtHSNCode.Text;
                                    GvProductInfo.Rows[i - 1].Cells["SaleRate"].Value = txtrate.Text;
                                    totalamt = 0;
                                    totalamt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["TotalAmount"].Value));
                                    txtTotalAmt.Text = Convert.ToString(Math.Round(totalamt, 2, MidpointRounding.AwayFromZero));
                                }
                            }
                            else
                            {
                                if (txtSubtitle.Text != "" && dtgvFormulae.RowCount == 0) { Proname = txtSubtitle.Text + "," + Proname; }
                                GvProductInfo.Rows.Add();
                                int i = GvProductInfo.RowCount;
                                GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                                GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                                GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                                GvProductInfo.Rows[i - 1].Cells["SaleUnit"].Value = cmbUnit.Text;
                                GvProductInfo.Rows[i - 1].Cells["Quantityg"].Value = txtQuantity.Text;
                                GvProductInfo.Rows[i - 1].Cells["Code"].Value = "";
                                GvProductInfo.Rows[i - 1].Cells["Rate"].Value = lblBasicRate.Text;   //txtrate.Text;
                                GvProductInfo.Rows[i - 1].Cells["Amount"].Value = txtAmount.Text;
                                if (txtpdiscount.Text == "")
                                    GvProductInfo.Rows[i - 1].Cells["Disc"].Value = 0;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["Disc"].Value = txtpdiscount.Text;
                                GvProductInfo.Rows[i - 1].Cells["SGST"].Value = cmbsgst.Text;
                                GvProductInfo.Rows[i - 1].Cells["SGSTAmt"].Value = lblpvat.Text;
                                GvProductInfo.Rows[i - 1].Cells["CGST"].Value = cmbcgst.Text;
                                GvProductInfo.Rows[i - 1].Cells["CGSTAmt"].Value = lblCGst.Text;
                                GvProductInfo.Rows[i - 1].Cells["IGST"].Value = cmbigst.Text;
                                GvProductInfo.Rows[i - 1].Cells["IGSTAmt"].Value = lblIGst.Text;
                                GvProductInfo.Rows[i - 1].Cells["TotalAmount"].Value = txtpTotalAmt.Text;
                                GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                GvProductInfo.Rows[i - 1].Cells["HSNCode"].Value = txtHSNCode.Text;
                                GvProductInfo.Rows[i - 1].Cells["SaleRate"].Value = txtrate.Text;
                                GvProductInfo.Rows[i - 1].Cells["Subtitle"].Value = txtSubtitle.Text;
                                totalamt = 0;
                                totalamt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["TotalAmount"].Value));
                                txtTotalAmt.Text = Convert.ToString(Math.Round(totalamt, 2, MidpointRounding.AwayFromZero));
                            }
                            clearatADD();
                        }
                    a: { }
                    }
                    else
                    {
                        if (txtProductname.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter Product Name.", "Warning");
                            this.ActiveControl = txtProductname;
                        }
                        else
                        {
                            MessageBox.Show("Enter Quantity.", "Warning");
                            this.ActiveControl = txtQuantity;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Enter Customer name.", "Warning");
                    this.ActiveControl = cmbcustomername;
                }
                ismandatory = false;
                dtgvcode.Visible = false;
            }
            catch (Exception)
            { }
        }

        public void VatCalculation()
        {
            try
            {
                vat5amt = 0;
                vat12amt = 0;
                tpuramt = 0;
                decimal vat1 = 0, vat2 = 0, disc = 0, discamt = 0, amt = 0, vatamt;
                for (int q = 0; q < GvProductInfo.Rows.Count; q++)
                {
                    if (Convert.ToDecimal(GvProductInfo.Rows[q].Cells["SGST"].Value) != 0)
                    {
                        ProductRepository productrepo = new ProductRepository();
                        ProductInformation productdata = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            productdata = productrepo.GetAll().Where(t => t.ProductName == Convert.ToString(GvProductInfo.Rows[q].Cells["ProductNameg"].Value) && t.ManufactureCompany == Convert.ToString(GvProductInfo.Rows[q].Cells["Company"].Value) && t.Uniti == Convert.ToString(GvProductInfo.Rows[q].Cells["Unit"].Value)).FirstOrDefault();
                        else
                            productdata = productrepo.GetAll().Where(t => t.ProductName == Convert.ToString(GvProductInfo.Rows[q].Cells["ProductNameg"].Value) && t.ManufactureCompany == Convert.ToString(GvProductInfo.Rows[q].Cells["Company"].Value) && t.Uniti == Convert.ToString(GvProductInfo.Rows[q].Cells["Unit"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        taxinclude = Convert.ToString(productdata.TaxInclusive);
                        productrepo.Dispose();
                        vatamt = 0; discamt = 0;
                        amt = Convert.ToDecimal(GvProductInfo.Rows[q].Cells["Quantityg"].Value) * Convert.ToDecimal(GvProductInfo.Rows[q].Cells["Rate"].Value);
                        disc = Convert.ToDecimal(GvProductInfo.Rows[q].Cells["Disc"].Value);
                        if (disc != 0)
                            discamt = amt * disc / 100;
                        if (Convert.ToDecimal(GvProductInfo.Rows[q].Cells["SGST"].Value) == setvat1)
                        {
                            if (taxinclude == "False" || taxinclude == null)
                            {
                                vat1 = Convert.ToDecimal(GvProductInfo.Rows[q].Cells["SGST"].Value);
                                vatamt = (amt - discamt) * vat1 / 100;
                                vat5amt += vatamt;
                            }
                            else
                            {
                                vat1 = Convert.ToDecimal(GvProductInfo.Rows[q].Cells["SGST"].Value);
                                vatamt = Convert.ToDecimal(GvProductInfo.Rows[q].Cells["Amount"].Value) * vat1 / 100;
                                vat5amt += vatamt;
                            }
                        }
                        else if (Convert.ToDecimal(GvProductInfo.Rows[q].Cells["SGST"].Value) == setvat2)
                        {
                            if (taxinclude == "False" || taxinclude == null)
                            {
                                vat2 = Convert.ToDecimal(GvProductInfo.Rows[q].Cells["SGST"].Value);
                                vatamt = (amt - discamt) * vat2 / 100;
                                vat12amt += vatamt;
                            }
                            else
                            {
                                vat2 = Convert.ToDecimal(GvProductInfo.Rows[q].Cells["SGST"].Value);
                                vatamt = Convert.ToDecimal(GvProductInfo.Rows[q].Cells["Amount"].Value) * vat2 / 100;
                                vat12amt += vatamt;
                            }
                        }
                    }
                    try
                    {
                        db.connect();
                        SqlCommand command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = GvProductInfo.Rows[q].Cells["ProductNameg"].Value;
                        command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = GvProductInfo.Rows[q].Cells["Company"].Value;
                        command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = GvProductInfo.Rows[q].Cells["Unit"].Value;
                        command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                        command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = '0';
                        command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        SqlDataReader reader = command.ExecuteReader();
                        DataTable dtRate = new DataTable();
                        dtRate.Load(reader);
                        decimal avgprice = 0;
                        avgprice = Convert.ToDecimal(dtRate.Rows[0][0].ToString());
                        dtRate.Dispose();
                        decimal qty = 0;
                        if (GvProductInfo.Rows[q].Cells["Unit"].Value.ToString() != GvProductInfo.Rows[q].Cells["SaleUnit"].Value.ToString())
                        {
                            ProductRepository productrepo1 = new ProductRepository();
                            ProductInformation productdata1 = new ProductInformation();
                            if (CommonMethod.commProduct == true)
                                productdata1 = productrepo1.GetAll().Where(t => t.ProductName == GvProductInfo.Rows[q].Cells["ProductNameg"].Value.ToString() && t.ManufactureCompany == GvProductInfo.Rows[q].Cells["Company"].Value.ToString() && t.Uniti == GvProductInfo.Rows[q].Cells["Unit"].Value.ToString()).FirstOrDefault();
                            else
                                productdata1 = productrepo1.GetAll().Where(t => t.ProductName == GvProductInfo.Rows[q].Cells["ProductNameg"].Value.ToString() && t.ManufactureCompany == GvProductInfo.Rows[q].Cells["Company"].Value.ToString() && t.Uniti == GvProductInfo.Rows[q].Cells["Unit"].Value.ToString() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            qty = Convert.ToDecimal(GvProductInfo.Rows[q].Cells["Quantityg"].Value) * (Convert.ToDecimal(productdata1.UnitQty) / Convert.ToDecimal(productdata1.AlternateUnitQty));
                        }
                        else
                            qty = Convert.ToDecimal(GvProductInfo.Rows[q].Cells["Quantityg"].Value);
                        tpuramt = tpuramt + (avgprice * qty);
                        db.CloseConnection();
                    }
                    catch (Exception)
                    { db.CloseConnection(); }
                }
                lbltpuramt.Text = tpuramt.ToString();
                if (vat1 != 0)
                {
                    if (vat2 != 0)
                    {
                        lbl2vatamt.Text = Convert.ToString(Math.Round(vat12amt, 2, MidpointRounding.AwayFromZero));
                        lbl2vat.Text = vat2.ToString() + " %";
                    }
                    else
                    {
                        lbl2vatamt.Text = "0";
                        lbl2vat.Text = " %";
                    }
                    lblvat.Text = Convert.ToString(Math.Round(vat5amt, 2, MidpointRounding.AwayFromZero));
                    txtVat.Text = vat1.ToString();
                    txtVat.Enabled = false;
                }
                else if (vat2 != 0)
                {
                    lblvat.Text = Convert.ToString(Math.Round(vat12amt, 2, MidpointRounding.AwayFromZero));
                    txtVat.Text = vat2.ToString();
                    txtVat.Enabled = false;
                    if (vat1 == 0)
                    {
                        lbl2vatamt.Text = "0";
                        lbl2vat.Text = " %";
                    }
                }
                else
                {
                    if ((Convert.ToDecimal(txtTotalAmt.Text) - Convert.ToDecimal(lbldiscount.Text) + Convert.ToDecimal(lblvat.Text)) != Convert.ToDecimal(txtNetAmt.Text))
                    {
                        lbl2vatamt.Text = Convert.ToString(Math.Round(vat12amt, 2, MidpointRounding.AwayFromZero));
                        lblvat.Text = Convert.ToString(Math.Round(vat12amt, 2, MidpointRounding.AwayFromZero));
                        lbl2vat.Text = vat2.ToString() + " %";
                        txtVat.Text = vat1.ToString();
                        txtVat.Enabled = true;
                    }
                }
            }
            catch (Exception)
            { }
        }

        public void clearatADD()
        {
            txtProductname.Text = "";
            cmbUnit.Items.Clear();
            txtQuantity.Text = "";
            txtrate.Text = "";
            txtDiscount.Text = "";
            txtAmount.Text = "";
            txtpTotalAmt.Text = "";
            txtpdiscount.Text = "0";
            txtDiscount.Text = "0";
            totalamt = 0;
            txtVat.Text = "0";
            txtSubtitle.Text = "";
            this.ActiveControl = txtProductname;
            this.ActiveControl = cmbcomanyname;
            lblpdiscount.Text = "0";
            lblpvat.Text = "0";
            lblpredisc.Text = "---------";
            lblprepurrate.Text = "-------------";
            txtHSNCode.Text = "";
            fillgst();
            colIndex = -1;
            dtgvcode.Rows.Clear();
            pnlSubtitle.Visible = false;
            dtgvFormulae.Visible = false;
            dtgvFormulae.Rows.Clear();
            dtgvFormulae.Columns.Clear();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(txtDiscount.Text) <= 100)
                    CalculateNetAmt();
                else
                {
                    MessageBox.Show("Enter Discount Between 0 to 100 %.", "Warning");
                    txtDiscount.Text = "0";
                    this.ActiveControl = txtDiscount;
                }
            }
            catch (Exception)
            {
                txtDiscount.Text = "0";
                this.ActiveControl = txtDiscount;
            }
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(txtVat.Text) <= 100)
                    CalculateNetAmt();
                else
                {
                    MessageBox.Show("Enter Vat Between 0 to 100 %.", "Warning");
                    txtVat.Text = "0";
                    this.ActiveControl = txtVat;
                }
            }
            catch (Exception)
            {
                txtVat.Text = "0";
                this.ActiveControl = txtVat;
            }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                txtTotalAmt.Text = "";
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                colIndex = -1;
                lbltpuramt.Text = "0";
                vat5amt = 0;
                vat12amt = 0;
                lbl2vat.Text = "%";
                lbl2vatamt.Text = "0";
                maxbillid();
                formulaCount = 0;
                btnsave.Enabled = true;
                txtProductname.Text = "";
                cmbUnit.Items.Clear();
                txtQuantity.Text = "";
                txtDiscount.Text = "";
                customerdata();
                fillcompany();
                txtTotalAmt.Text = "";
                GvProductInfo.Rows.Clear();
                txtNetAmt.Text = "";
                btnUpdate.Enabled = false;
                btnsave.Text = "Save";
                txtVat.Text = "0";
                txthamalichrg.Text = "0";
                txttranchrg.Text = "0";
                lblvat.Text = "0";
                lbldiscount.Text = "0";
                txtpdiscount.Text = "0";
                txtpTotalAmt.Text = "";
                txtrate.Text = "";
                txtAmount.Text = "";
                subtitlestring = ""; RateDecrease = ""; RateIncrease = ""; getSlabUnit = ""; SizeSpecString = "";
                fillgst();
                txtVat.Enabled = true;
                pnlSubtitle.Visible = false;
                txtSubtitle.Text = "";
                try
                { dtpquotationdate.Value = DateTime.Now.Date; }
                catch (Exception)
                { }
                NewSizeQty = 0; qtySlab = 0; SizeQty = 0;
                getFormula = false;
            }
            catch (Exception)
            { }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                maxbillid();
                QuotationMasterRepository Qmasterrepo1 = new QuotationMasterRepository();
                var BillId = Qmasterrepo1.GetAll().Where(t => t.QuotationNo == Convert.ToInt32(txtquotationno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (BillId != null)
                {
                    MessageBox.Show("Already Saved This Record.", "Warning");
                    this.ActiveControl = btnnew;
                }
                else
                {
                    if (cmbcustomername.Text != "Select" && cmbcustomername.Text != "")
                    {
                        if (GvProductInfo.Rows.Count != 0)
                        {
                            QuotationMasterRepository Qmasterrepo = new QuotationMasterRepository();
                            QuotationDetailRepository Qdetailrepo = new QuotationDetailRepository();
                            maxbillid();
                            QuotationMaster billdata = new QuotationMaster();
                            billdata.QuotationNo = Convert.ToInt32(txtquotationno.Text);
                            billdata.Quotationdate = Convert.ToDateTime(dtpquotationdate.Value.ToShortDateString());
                            billdata.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                            billdata.CustomerName = cmbcustomername.Text;
                            if (txtDiscount.Text != "")
                                billdata.Discount = Convert.ToDecimal(txtDiscount.Text);
                            else
                                billdata.Discount = 0;
                            if (txtNetAmt.Text != "")
                                billdata.NetAmount = Convert.ToDecimal(txtNetAmt.Text);
                            else
                                billdata.NetAmount = Convert.ToDecimal(txtTotalAmt.Text);
                            if (txtTotalAmt.Text != "")
                                billdata.TotalAmount = Convert.ToDecimal(txtTotalAmt.Text);
                            else
                                billdata.TotalAmount = 0;
                            if (txtVat.Text != "")
                                billdata.Vat = Convert.ToDecimal(txtVat.Text);
                            else
                                billdata.Vat = 0;
                            if (txttranchrg.Text != "")
                                billdata.TransportCharges = Convert.ToDecimal(txttranchrg.Text);
                            else
                                billdata.TransportCharges = 0;
                            if (txthamalichrg.Text != "")
                                billdata.HamaliCharges = Convert.ToDecimal(txthamalichrg.Text);
                            else
                                billdata.HamaliCharges = 0;
                            billdata.TransactionYear = CommonMethod.TransactionYear;
                            billdata.CompId = CommonMethod.CompId;
                            billdata.Status = 1;
                            Qmasterrepo.Add(billdata);
                            Qmasterrepo.Save();
                            if (deleteid.Count != 0)
                            {
                                for (int x = 0; x < deleteid.Count; x++)
                                {
                                    QuotationDetailRepository Qdetailrepo1 = new QuotationDetailRepository();
                                    QuotationDetail quotationcontroldateil = Qdetailrepo.GetById(deleteid[x]);
                                    Qdetailrepo1.Remove(quotationcontroldateil);
                                    Qdetailrepo1.Save();
                                    Qdetailrepo1.Dispose();
                                }
                            }
                            QuotationDetail bdetaildata = new QuotationDetail();
                            for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                            {
                                try
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPQuotationSaveUpdate", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;

                                    command.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                    command.Parameters.Add("@event", SqlDbType.Int).Value = 1;
                                    command.Parameters.Add("@QuotationNo", SqlDbType.Int).Value = billdata.Id;
                                    command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                    command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                    command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                    command.Parameters.Add("@SaleUnit", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["SaleUnit"].Value);
                                    command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantityg"].Value);
                                    command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                                    command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Rate"].Value);
                                    command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Amount"].Value);
                                    command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Disc"].Value);
                                    command.Parameters.Add("@Vat", SqlDbType.Decimal).Value = 0;
                                    command.Parameters.Add("@SGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SGST"].Value);
                                    command.Parameters.Add("@SGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SGSTAmt"].Value);
                                    command.Parameters.Add("@CGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["CGST"].Value);
                                    command.Parameters.Add("@CGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["CGSTAmt"].Value);
                                    command.Parameters.Add("@IGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["IGST"].Value);
                                    command.Parameters.Add("@IGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["IGSTAmt"].Value);
                                    command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["TotalAmount"].Value);
                                    command.Parameters.Add("@HSNCode", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["HSNCode"].Value);
                                    command.Parameters.Add("@SaleRate", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SaleRate"].Value);
                                    command.Parameters.Add("@Prodsubtitle", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Subtitle"].Value);
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                                    command.ExecuteNonQuery();
                                    db.CloseConnection();
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                            }
                            quatno = Convert.ToInt32(txtquotationno.Text);
                            MessageBox.Show("Quotation saved.", "Success");
                            GvProductInfo.Rows.Clear();
                            clear();
                            this.ActiveControl = btnprint;
                            Qmasterrepo.Dispose();
                            Qdetailrepo.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Add Product Information.", "Warning");
                            this.ActiveControl = txtProductname;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select Customer Name.", "Warning");
                        this.ActiveControl = cmbcustomername;
                    }
                }
                Qmasterrepo1.Dispose();
            }
            catch (Exception)
            { }
        }

        private void cmbcomanyname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbcomanyname.Text != "" && PrefixToBind == false && prefixFilled == true)
                {
                    if (cmbcomanyname.Text != "Select")
                    {
                        PrefixToBind = true;
                        txtprefixproduct.Text = cmbcomanyname.Text;
                        txtprefixproduct.Visible = true;
                        getProductsbyPrefix(cmbcomanyname.Text);
                        this.ActiveControl = txtprefixproduct;
                    }
                }
                else
                    txtprefixproduct.Text = "";
            }
            catch (Exception)
            { }
        }

        private void getProductsbyPrefix(string productprefix)
        {
            try
            {
                txtProductname.Text = "";
                productprefix = productprefix.Trim();
                if (productprefix != "")
                {
                    string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(cnString1))
                    {
                        List<string> lstproduct = new List<string>();
                        SqlCommand cmd = null;
                        if (CommonMethod.commProduct == true)
                            cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where Prefix like '" + productprefix + "%'", con);
                        else
                            cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where Prefix like '" + productprefix + "%' and compid=" + CommonMethod.CompId, con);

                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            string strproduct = "";
                            strproduct = reader.GetString(0) + "," + reader.GetString(1) + "," + reader.GetString(2);
                            lstproduct.Add(strproduct);
                        }
                        if (lstproduct.Count > 0)
                        {
                            txtprefixproduct.DataSource = lstproduct;  // assigned list to listbox
                            txtprefixproduct.Visible = true;
                        }
                    }
                }
                else
                    txtprefixproduct.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                int quatationid = quatno;
                RptQuotation custrpt = new RptQuotation(quatationid);
                custrpt.ShowDialog();
                custrpt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void txtProductname_Leave(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Visible = false;
                gvProductRemStock.Visible = false;
                if (flag == 1)
                {
                    if (txtProductname.Text.Trim() != "")
                    {
                        string proname = "", company = "", unit = "";
                        if ((txtProductname.Text).Contains(","))
                        {
                            try
                            {
                                proname = txtProductname.Text.Trim().Split(',')[0];
                                company = txtProductname.Text.Trim().Split(',')[1];
                                unit = txtProductname.Text.Trim().Split(',')[2];
                            }
                            catch (Exception)
                            { }
                            SettingRepository settingrepo = new SettingRepository();
                            var settingdata = settingrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).LastOrDefault();
                            ProductRepository productrepo = new ProductRepository();
                            ProductInformation productdata = new ProductInformation();
                            if (CommonMethod.commProduct == true)
                                productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                            else
                                productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            if (productdata == null)
                            {
                                MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                                txtProductname.Text = "";
                                this.ActiveControl = txtProductname;
                            }
                            else
                            {
                                if (colIndex != -1)
                                {
                                    if (txtProductname.Text != Convert.ToString(GvProductInfo.Rows[colIndex].Cells["ProductNameg"].Value) + "," + GvProductInfo.Rows[colIndex].Cells["Company"].Value + "," + GvProductInfo.Rows[colIndex].Cells["Unit"].Value)
                                    {
                                        if (CustomerType == "Retailer" || CustomerType == null)
                                            txtrate.Text = Convert.ToString(productdata.Rate);
                                        else
                                        {
                                            if (productdata.DealerRate != null)
                                                txtrate.Text = Convert.ToString(productdata.DealerRate);
                                            else
                                                txtrate.Text = Convert.ToString(productdata.Rate);
                                        }
                                        cmbUnit.Items.Clear();
                                        cmbUnit.DisplayMember = "Text";
                                        cmbUnit.ValueMember = "Value";
                                        cmbUnit.Items.Add(new { Text = productdata.Uniti, Value = productdata.Uniti });
                                        if (productdata.AlternateUnit != null && productdata.AlternateUnit != "")
                                            cmbUnit.Items.Add(new { Text = productdata.AlternateUnit, Value = productdata.AlternateUnit });
                                        cmbUnit.SelectedIndex = 0;
                                        pnlSubtitle.Visible = false;
                                        txtSubtitle.Visible = false;
                                        if (string.IsNullOrEmpty(productdata.Formulae) && productdata.Usesubtitle == "Y")
                                        {
                                            pnlSubtitle.Visible = true;
                                            txtSubtitle.Visible = true;
                                            if (subtitlestring != "")
                                                txtSubtitle.Text = subtitlestring;
                                        }
                                        else if (!string.IsNullOrEmpty(productdata.Formulae) && (productdata.Usesubtitle == "Y" || productdata.Usesubtitle != "Y") && getFormula == false)
                                        {
                                            try
                                            {
                                                int input = 0;
                                                input = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox("Enter Number of Raw Material use for this product", "Raw Material", "0", 500, 250));
                                                if (input != 0)
                                                {
                                                    dtgvFormulae.Rows.Clear();
                                                    dtgvFormulae.Columns.Clear();
                                                    //Add Columns to datagridview of formulae
                                                    formulaCount = 0;
                                                    string[] arrayformulae = productdata.Formulae.Split('*', '/', '+', '-');
                                                    if (dtgvFormulae.Columns.Count == 0)
                                                    {
                                                        for (int m = 0; m < arrayformulae.Length; m++)
                                                        {
                                                            int constantVal;
                                                            bool outResult = int.TryParse(arrayformulae[m].ToString(), out constantVal);
                                                            if (outResult == true)
                                                                dtgvFormulae.Columns.Add(arrayformulae[m].ToString(), "Constant");
                                                            else
                                                            {
                                                                formulaCount++;
                                                                dtgvFormulae.Columns.Add(arrayformulae[m].ToString(), arrayformulae[m].ToString());
                                                            }
                                                            dtgvFormulae.Columns[m].Width = 62;
                                                        }
                                                        dtgvFormulae.Columns.Add("Result", "Result");

                                                        dtgvFormulae.Columns.Add("Name", "Name");
                                                        dtgvFormulae.Columns["Result"].Width = 65;
                                                        dtgvFormulae.Columns["Name"].Width = 200;
                                                    }
                                                    //Add rows to datagridview of formulae
                                                    for (int n = 0; n < input; n++)
                                                    {
                                                        dtgvFormulae.Rows.Add();
                                                    }
                                                    dtgvFormulae.Visible = true;
                                                    dtgvFormulae.Height = dtgvFormulae.ColumnHeadersHeight + dtgvFormulae.Rows[0].Height * (dtgvFormulae.RowCount + 1);
                                                    for (int m = 0; m < dtgvFormulae.ColumnCount; m++) // to get width of grid
                                                    {
                                                        if (dtgvFormulae.Columns[m].HeaderText == "Constant")
                                                            dtgvFormulae.Columns[m].Visible = false;
                                                        dtgvFormulae.Width += dtgvFormulae.Columns[m].Width;
                                                        dtgvFormulae.Columns[m].HeaderCell.Style.BackColor = Color.Navy;
                                                        dtgvFormulae.Columns[m].HeaderCell.Style.ForeColor = Color.PaleVioletRed;
                                                        dtgvFormulae.Columns[m].HeaderCell.Style.Font = new Font("Arial", 12);
                                                    }
                                                    if (dtgvFormulae.Rows.Count > 0)
                                                    {
                                                        getFormula = true;
                                                        dtgvFormulae.CurrentCell = dtgvFormulae.Rows[0].Cells[0];
                                                        this.ActiveControl = dtgvFormulae;
                                                    }
                                                }
                                            }
                                            catch (Exception)
                                            { }
                                        }
                                        txtHSNCode.Text = productdata.HSNCode;
                                        if (settingdata.CompositionScheme == "True")
                                        { }
                                        else
                                        {
                                            cmbsgst.Text = Convert.ToString(productdata.CGstPercent);
                                            cmbcgst.Text = Convert.ToString(productdata.SGstPercent);
                                            cmbigst.Text = Convert.ToString(productdata.IGstPercent);
                                        }
                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(productdata.Formulae) && productdata.Usesubtitle == "Y")
                                        {
                                            pnlSubtitle.Visible = true;
                                            txtSubtitle.Visible = true;
                                            if (subtitlestring != "")
                                                txtSubtitle.Text = subtitlestring;
                                        }
                                        else if (!string.IsNullOrEmpty(productdata.Formulae) && (productdata.Usesubtitle == "Y" || productdata.Usesubtitle != "Y") && getFormula == false)
                                        {
                                            try
                                            {
                                                string[] strformulae = txtSubtitle.Text.ToString().Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                                                dtgvFormulae.Rows.Clear();
                                                dtgvFormulae.Columns.Clear();
                                                //Add Columns to datagridview of formulae
                                                string[] arrayformulae = productdata.Formulae.Split('*', '/', '+', '-');
                                                formulaCount = 0;
                                                if (dtgvFormulae.Columns.Count == 0)
                                                {
                                                    for (int m = 0; m < arrayformulae.Length; m++)
                                                    {
                                                        int constantVal;
                                                        bool outResult = int.TryParse(arrayformulae[m].ToString(), out constantVal);
                                                        if (outResult == true)
                                                            dtgvFormulae.Columns.Add(arrayformulae[m].ToString(), "Constant");
                                                        else
                                                        {
                                                            formulaCount++;
                                                            dtgvFormulae.Columns.Add(arrayformulae[m].ToString(), arrayformulae[m].ToString());
                                                        }
                                                        dtgvFormulae.Columns[m].Width = 62;
                                                    }
                                                    dtgvFormulae.Columns.Add("Result", "Result");
                                                    if (slabGroup == 'F')
                                                    {
                                                        dtgvFormulae.Columns.Add("Rate(-)", "Rate(-" + getSlabUnit.Trim() + ")");
                                                        dtgvFormulae.Columns.Add("Rate(+)", "Rate(+" + getSlabUnit.Trim() + ")");
                                                        dtgvFormulae.Columns["Rate(-)"].Width = 70;
                                                        dtgvFormulae.Columns["Rate(+)"].Width = 70;
                                                    }
                                                    dtgvFormulae.Columns.Add("Name", "Name");
                                                    dtgvFormulae.Columns["Result"].Width = 65;
                                                    dtgvFormulae.Columns["Name"].Width = 200;
                                                }
                                                //Add rows to datagridview of formulae                                                
                                                for (int n = 0; n < strformulae.Length; n++)
                                                {
                                                    string namestr = "", snamestr = "";
                                                    if (strformulae.Length != dtgvFormulae.RowCount)
                                                        dtgvFormulae.Rows.Add();
                                                    string[] arrayformulae1 = strformulae[n].ToString().Split('*', '/', '+', '-', '=', ' ');
                                                    for (int i = 0; i < arrayformulae1.Length; i++)
                                                    {
                                                        if (i == dtgvFormulae.ColumnCount - 1)
                                                        {
                                                            int j = dtgvFormulae.ColumnCount - 1;
                                                            for (i = dtgvFormulae.ColumnCount - 1; i < arrayformulae1.Length; i++)
                                                                snamestr = snamestr + arrayformulae1[i].ToString() + " ";
                                                            dtgvFormulae.Rows[n].Cells[j].Value = snamestr;
                                                        }
                                                        else
                                                        {
                                                            namestr = arrayformulae1[i].ToString();
                                                            dtgvFormulae.Rows[n].Cells[i].Value = namestr;
                                                        }
                                                    }
                                                }
                                                dtgvFormulae.Visible = true;
                                                dtgvFormulae.Height = dtgvFormulae.ColumnHeadersHeight + dtgvFormulae.Rows[0].Height * (dtgvFormulae.RowCount + 1);
                                                dtgvFormulae.Width = 0;
                                                for (int m = 0; m < dtgvFormulae.ColumnCount; m++) // to get width of grid
                                                {
                                                    if (dtgvFormulae.Columns[m].HeaderText == "Constant")
                                                        dtgvFormulae.Columns[m].Visible = false;
                                                    dtgvFormulae.Width += dtgvFormulae.Columns[m].Width;
                                                    dtgvFormulae.Columns[m].HeaderCell.Style.BackColor = Color.Navy;
                                                    dtgvFormulae.Columns[m].HeaderCell.Style.ForeColor = Color.PaleVioletRed;
                                                    dtgvFormulae.Columns[m].HeaderCell.Style.Font = new Font("Arial", 12);
                                                }
                                                if (dtgvFormulae.Rows.Count > 0)
                                                {
                                                    getFormula = true;
                                                    dtgvFormulae.CurrentCell = dtgvFormulae.Rows[0].Cells[0];
                                                    this.ActiveControl = dtgvFormulae;
                                                }
                                            }
                                            catch (Exception)
                                            { }
                                        }
                                    }
                                }
                                else
                                {
                                    pnlSubtitle.Visible = false;
                                    txtSubtitle.Visible = false;
                                    if (string.IsNullOrEmpty(productdata.Formulae) && productdata.Usesubtitle == "Y")
                                    {
                                        pnlSubtitle.Visible = true;
                                        txtSubtitle.Visible = true;
                                        if (subtitlestring != "")
                                            txtSubtitle.Text = subtitlestring;
                                    }
                                    else if (!string.IsNullOrEmpty(productdata.Formulae) && (productdata.Usesubtitle == "Y" || productdata.Usesubtitle != "Y") && getFormula == false)
                                    {
                                        try
                                        {
                                            int input = 0;
                                            input = Convert.ToInt32(Microsoft.VisualBasic.Interaction.InputBox("Enter Number of Raw Material use for this product", "Raw Material", "0", 500, 250));
                                            if (input != 0)
                                            {
                                                dtgvFormulae.Rows.Clear();
                                                dtgvFormulae.Columns.Clear();
                                                //Add Columns to datagridview of formulae
                                                string[] arrayformulae = productdata.Formulae.Split('*', '/', '+', '-');
                                                formulaCount = 0;
                                                if (dtgvFormulae.Columns.Count == 0)
                                                {
                                                    for (int m = 0; m < arrayformulae.Length; m++)
                                                    {
                                                        int constantVal;
                                                        bool outResult = int.TryParse(arrayformulae[m].ToString(), out constantVal);
                                                        if (outResult == true)
                                                            dtgvFormulae.Columns.Add(arrayformulae[m].ToString(), "Constant");
                                                        else
                                                        {
                                                            formulaCount++;
                                                            dtgvFormulae.Columns.Add(arrayformulae[m].ToString(), arrayformulae[m].ToString());
                                                        }
                                                        dtgvFormulae.Columns[m].Width = 65;
                                                    }
                                                    dtgvFormulae.Columns.Add("Result", "Result");
                                                    dtgvFormulae.Columns.Add("Name", "Name");
                                                    dtgvFormulae.Columns["Result"].Width = 65;
                                                    dtgvFormulae.Columns["Name"].Width = 200;
                                                }
                                                for (int n = 0; n < input; n++)
                                                {
                                                    dtgvFormulae.Rows.Add();
                                                    int rowCount = dtgvFormulae.Rows.Count - 1;
                                                    int arrayformcount = 0;
                                                    for (int m = 0; m < arrayformulae.Length; m++)
                                                    {
                                                        int constantVal;
                                                        bool outResult = int.TryParse(arrayformulae[m].ToString(), out constantVal);
                                                        if (outResult == true)
                                                            dtgvFormulae.Rows[rowCount].Cells[m].Value = constantVal.ToString();
                                                        else
                                                            dtgvFormulae.Rows[n].Cells[m].Value = "0";
                                                        arrayformcount = m;
                                                    }
                                                    for (int j = arrayformcount; j < dtgvFormulae.Columns.Count; j++) //----------to set default value nilophar 13042018
                                                    {
                                                        dtgvFormulae.Rows[n].Cells[j].Value = "";
                                                    }
                                                }
                                                ///------------ Add Constant values to grid
                                                dtgvFormulae.Visible = true;
                                                dtgvFormulae.Height = dtgvFormulae.ColumnHeadersHeight + dtgvFormulae.Rows[0].Height * (dtgvFormulae.RowCount + 1);
                                                dtgvFormulae.Width = 0;
                                                for (int m = 0; m < dtgvFormulae.ColumnCount; m++) // to get width of grid
                                                {
                                                    if (dtgvFormulae.Columns[m].HeaderText == "Constant")
                                                        dtgvFormulae.Columns[m].Visible = false;
                                                    dtgvFormulae.Width += dtgvFormulae.Columns[m].Width;
                                                    dtgvFormulae.Columns[m].HeaderCell.Style.BackColor = Color.Navy;
                                                    dtgvFormulae.Columns[m].HeaderCell.Style.ForeColor = Color.PaleVioletRed;
                                                    dtgvFormulae.Columns[m].HeaderCell.Style.Font = new Font("Arial", 12);
                                                }
                                                if (dtgvFormulae.Rows.Count > 0)
                                                {
                                                    getFormula = true;
                                                    dtgvFormulae.CurrentCell = dtgvFormulae.Rows[0].Cells[0];
                                                    this.ActiveControl = dtgvFormulae;
                                                }
                                            }
                                        }
                                        catch (Exception)
                                        { }
                                    }
                                    if (CustomerType == "Retailer" || CustomerType == null)
                                        txtrate.Text = Convert.ToString(productdata.Rate);
                                    else
                                    {
                                        if (productdata.DealerRate != null)
                                            txtrate.Text = Convert.ToString(productdata.DealerRate);
                                        else
                                            txtrate.Text = Convert.ToString(productdata.Rate);
                                    }
                                    if (txtpdiscount.Text == "0")
                                        txtpdiscount.Text = Convert.ToString(productdata.ProductDiscount);
                                    taxinclude = Convert.ToString(productdata.TaxInclusive);
                                    cmbUnit.Items.Clear();
                                    cmbUnit.DisplayMember = "Text";
                                    cmbUnit.ValueMember = "Value";
                                    cmbUnit.Items.Add(new { Text = productdata.Uniti, Value = productdata.Uniti });
                                    if (productdata.AlternateUnit != null && productdata.AlternateUnit != "")
                                        cmbUnit.Items.Add(new { Text = productdata.AlternateUnit, Value = productdata.AlternateUnit });
                                    cmbUnit.SelectedIndex = 0;
                                }
                                ProductCategory = productdata.Category;
                                txtHSNCode.Text = productdata.HSNCode;
                                if (settingdata.CompositionScheme == "True")
                                { }
                                else
                                {
                                    cmbsgst.Text = Convert.ToString(productdata.CGstPercent);
                                    cmbcgst.Text = Convert.ToString(productdata.SGstPercent);
                                    cmbigst.Text = Convert.ToString(productdata.IGstPercent);
                                }
                            }

                            PurchaseDetailRepository purdtlrepo = new PurchaseDetailRepository();
                            var purdata = purdtlrepo.GetAll().Where(t => t.ProductName == proname && t.Unit == unit && t.CompanyName == company && t.Unit == unit && t.PurchasePrice != null && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).LastOrDefault();
                            if (purdata != null)
                            {
                                if (Convert.ToDecimal(purdata.Vat) > 0)
                                {
                                    decimal purprice = Convert.ToDecimal(purdata.PurchasePrice) + (Convert.ToDecimal(purdata.PurchasePrice) * Convert.ToDecimal(purdata.Vat) / 100);
                                    lblprepurrate.Text = Convert.ToString(purprice) + " /-";
                                }
                                else
                                    lblprepurrate.Text = Convert.ToString(purdata.PurchasePrice) + " /-";
                                lblpredisc.Text = purdata.Discount.ToString() + " %";
                                HasMandatoryCodeSr = purdata.HasMandatoryCodeSeries;
                            }
                            productrepo.Dispose();
                            purdtlrepo.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                            txtProductname.Text = "";
                            this.ActiveControl = txtProductname;
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnquatationlist_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                quainfo = this;
                frmQuotation p = new frmQuotation();
                p.Visible = false;
                frmQuotationList qualist = new frmQuotationList();
                qualist.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void fillProductSubtitles()
        {
            string pname = txtProductname.Text.Split(',')[0];
            string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cnString1))
            {
                SqlCommand cmd = new SqlCommand("Select distinct cb.prodsubtitle from CustomerBillDetail as cb join customerbillmaster as cm on cb.BillNo=cm.Id where customerid='" + cmbcustomername.SelectedValue + "' and ProductId ='" + pname + "' and prodsubtitle is not null", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                AutoCompleteStringCollection mycollection = new AutoCompleteStringCollection();
                while (reader.Read())
                {
                    try
                    {
                        mycollection.Add(reader.GetString(0));
                    }
                    catch (Exception)
                    { }
                }
                txtSubtitle.AutoCompleteCustomSource = mycollection;
                if (subtitlestring != "")
                    txtSubtitle.Text = subtitlestring;
                reader.Dispose();
                con.Close();
            }
        }

        private void txtProductname_TextChanged(object sender, EventArgs e)
        {
            string product_prefix = "";
            try
            {
                if (txtProductname.Text != "")
                {
                    flag = 1;
                    gvProductRemStock.Visible = true;
                    gvProductRemStock.Rows.Clear();
                    string proname = "", company = "", unit = "";
                    if ((txtProductname.Text).Contains(","))
                    {
                        try
                        {
                            proname = txtProductname.Text.Trim().Split(',')[0];
                            company = txtProductname.Text.Trim().Split(',')[1];
                            unit = txtProductname.Text.Trim().Split(',')[2];
                            ProductRepository productrepo = new ProductRepository();
                            ProductInformation productdata = new ProductInformation();
                            SettingRepository settingrepo = new SettingRepository();
                            var settingdata = settingrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).LastOrDefault();
                            if (CommonMethod.commProduct == true)
                                productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                            else
                                productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            if (productdata != null)
                            {
                                taxinclude = Convert.ToString(productdata.TaxInclusive);
                                txtHSNCode.Text = productdata.HSNCode;
                                product_prefix = productdata.Prefix;
                                PrefixToBind = true;
                                cmbcomanyname.Text = product_prefix;
                                if (productdata.Usesubtitle == "Y")
                                {
                                    pnlSubtitle.Visible = true;
                                    txtSubtitle.Visible = true;
                                }
                                if (subtitlestring != "")
                                    txtSubtitle.Text = subtitlestring;
                                productrepo.Dispose();
                            }
                            try
                            {
                                db.connect();
                                SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = proname;
                                command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = company;
                                command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = unit;
                                command.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'P';
                                command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'a';
                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                                command.CommandTimeout = 300;
                                DataTable dt = new DataTable();
                                dt.Load(command.ExecuteReader());
                                if (dt.Rows.Count > 0)
                                {
                                    gvProductRemStock.Rows.Add();
                                    gvProductRemStock.Rows[0].Cells[0].Value = dt.Rows[0]["ProductName"].ToString();
                                    if (dt.Rows.Count == 1)
                                        gvProductRemStock.Rows[0].Cells[1].Value = dt.Rows[0]["RemQty"].ToString();
                                    else
                                        gvProductRemStock.Rows[0].Cells[1].Value = dt.Compute("Sum(RemQty)", "RemQty>=0").ToString();
                                    gvProductRemStock.Rows[0].Cells[2].Value = dt.Rows[0]["Unit"].ToString();
                                }
                                //SqlDataReader reader = command.ExecuteReader();
                                //while (reader.Read())
                                //{
                                //    gvProductRemStock.Rows.Add();
                                //    gvProductRemStock.Rows[0].Cells[0].Value = reader.GetString(1);
                                //    gvProductRemStock.Rows[0].Cells[1].Value = reader.GetSqlDecimal(11).ToString();
                                //    gvProductRemStock.Rows[0].Cells[2].Value = reader.GetString(3);
                                //}
                                db.CloseConnection();
                                settingrepo.Dispose();
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
                        }
                        catch (Exception)
                        { }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            try
            {
                if (colIndex != -1)
                {
                    if (ProductCategory == "Motor" && refreshFlag == false)
                    {
                        if (Convert.ToDecimal(txtQuantity.Text) != 1)
                        {
                            MessageBox.Show("Quantity must be 1 for update.", "Warning");
                            txtQuantity.Text = "1";
                        }
                    }
                    else
                        goto s;
                }
                else
                {
                    if (ProductCategory == "Motor" && refreshFlag == false)
                    {
                        if (MessageBox.Show("Do you want to enter series", "Enter Series", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            ismandatory = true;
                            refreshFlag = true;
                            goto t;
                        }
                        else
                        {
                            ismandatory = false;
                            refreshFlag = false;
                            goto s;
                        }
                    }
                    else
                        goto s;
                }
            t:
                {
                    dtgvcode.Rows.Clear();
                    for (int i = 0; i < Convert.ToInt32(txtQuantity.Text); i++)
                    {
                        dtgvcode.Rows.Add();
                        if (colIndex != -1)
                            dtgvcode.Rows[i].Cells[0].Value = GvProductInfo.Rows[colIndex].Cells["Code"].Value.ToString();
                        else
                            dtgvcode.Rows[i].Cells[0].Value = "";
                    }
                    dtgvcode.Visible = true;
                    this.ActiveControl = dtgvcode;
                    dtgvcode.Height = dtgvcode.ColumnHeadersHeight + dtgvcode.Rows[0].Height * (dtgvcode.RowCount + 1);
                }
            s:
                {
                    getGstCalculation();
                }
            }
            catch (Exception)
            { }
        }

        private void cmbcustomername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcomanyname;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmcustomer customer = new frmcustomer();
                        customer.ShowDialog();
                        customerdata();
                        this.ActiveControl = cmbcustomername;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtProductname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtrate;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmProductInformation product = new frmProductInformation();
                        product.ShowDialog();
                        frmQuotation_Load(null, null);
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtTotalAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateNetAmt();
            }
            catch (Exception)
            { }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(txtAmount.Text) != 0)
                    getGstCalculation();
            }
            catch (Exception)
            { }
        }

        private void cmbcustomername_Leave(object sender, EventArgs e)
        {
            try
            {
                CustomerRepository custrepo = new CustomerRepository();
                var Mistriname = custrepo.GetAll().Where(t => t.CustomerID == Convert.ToInt32(cmbcustomername.SelectedValue) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (Mistriname != null)
                    CustomerType = Mistriname.CustomerType;
                else
                    CustomerType = null;
                if (txtProductname.Text != "")
                    this.ActiveControl = txtProductname;
                custrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void dtgvcode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (codeflag == 0)
                {
                    dtgvcode.Visible = false;
                    this.ActiveControl = txtrate;
                }
                else
                    this.ActiveControl = dtgvcode;
            }
            catch (Exception)
            { }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (txtProductname.Text != "")
                {
                    gvprepurchasedata.Visible = true;
                    string newpname = "";
                    if ((txtProductname.Text).Contains("'"))
                        newpname = txtProductname.Text.Replace("'", "''");
                    else
                        newpname = txtProductname.Text.Trim();
                    string proname = newpname.Split(',')[0];
                    string company = newpname.Split(',')[1];
                    string unit = newpname.Split(',')[2];
                    SqlDataAdapter da = null;
                    if (CommonMethod.commProduct == true)
                        da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),m.PorderDate,103) as 'PO Date',cast(case when d.Unit=d.PurchaseUnit or d.PurchaseUnit is null then d.PurchasePrice else (d.PurchasePrice/(Select UnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit and compid=" + CommonMethod.CompId + ")) end as Decimal(18,2)) as 'Price', d.Discount as 'Disc', m.Vat, cast(case when d.Unit=d.PurchaseUnit or d.PurchaseUnit is null then (d.TotalAmount/d.Quantity)+(d.TotalAmount/d.Quantity)*(m.Vat/100) else  ((d.TotalAmount/(d.Quantity/((Select UnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit)/(Select AlternateUnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit))))+ (d.TotalAmount/(d.Quantity/((Select UnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit)/(Select AlternateUnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit)))*(m.Vat/100)))/(Select UnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit) end as decimal(18,2)) as 'TotalAmount' ,d.Unit FROM PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo  where ProductName='" + proname + "' and CompanyName='" + company + "' and Unit='" + unit + "' and m.Suppliername<>'First stock' ORDER BY d.Id DESC ", db.Connection);
                    else
                        da = new SqlDataAdapter("SELECT CONVERT(VARCHAR(24),m.PorderDate,103) as 'PO Date',cast(case when d.Unit=d.PurchaseUnit or d.PurchaseUnit is null then d.PurchasePrice else (d.PurchasePrice/(Select UnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit and compid=" + CommonMethod.CompId + ")) end as Decimal(18,2)) as 'Price', d.Discount as 'Disc', m.Vat, cast(case when d.Unit=d.PurchaseUnit or d.PurchaseUnit is null then (d.TotalAmount/d.Quantity)+(d.TotalAmount/d.Quantity)*(m.Vat/100) else  ((d.TotalAmount/(d.Quantity/((Select UnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit and compid=" + CommonMethod.CompId + ")/(Select AlternateUnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit and compid=" + CommonMethod.CompId + "))))+ (d.TotalAmount/(d.Quantity/((Select UnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit and compid=" + CommonMethod.CompId + ")/(Select AlternateUnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit and compid=" + CommonMethod.CompId + ")))*(m.Vat/100)))/(Select UnitQty from ProductInformation where ProductName=d.ProductName and ManufactureCompany=d.CompanyName and Uniti=d.Unit and compid=" + CommonMethod.CompId + ") end as decimal(18,2)) as 'TotalAmount' ,d.Unit FROM PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo  where ProductName='" + proname + "' and CompanyName='" + company + "' and Unit='" + unit + "' and m.Suppliername<>'First stock' and m.CompId=" + CommonMethod.CompId + " ORDER BY d.Id DESC ", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    gvprepurchasedata.DataSource = dt;
                    gvprepurchasedata.Refresh();
                    if (gvprepurchasedata.Rows.Count != 0)
                    {
                        gvprepurchasedata.Columns[0].Width = 70;
                        gvprepurchasedata.Columns[1].Width = 70;
                        gvprepurchasedata.Columns[2].Width = 45;
                        gvprepurchasedata.Columns[3].Width = 45;
                        gvprepurchasedata.Columns[4].Width = 80;
                        gvprepurchasedata.Columns[5].Width = 53;
                    }
                    da.Dispose();
                    dt.Dispose();
                    this.ActiveControl = gvprepurchasedata;
                }
                else
                {
                    MessageBox.Show("Select Product Name first.", "Warning");
                    this.ActiveControl = txtProductname;
                }
            }
            catch (Exception)
            { gvprepurchasedata.Visible = false; }
        }

        private void gvprepurchasedata_Leave(object sender, EventArgs e)
        {
            try
            {
                gvprepurchasedata.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void gvprepurchasedata_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter || e.KeyData == Keys.Tab)
                    this.ActiveControl = txtProductname;
            }
            catch (Exception)
            { }
        }

        private void dtgvcode_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dtgvcode.CurrentCell.ColumnIndex == 0)
                {
                    string Proname = txtProductname.Text.Trim().Split(',')[0];
                    string company = txtProductname.Text.Trim().Split(',')[1];
                    string unit = txtProductname.Text.Trim().Split(',')[2];
                    string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                    SqlDataReader dreader;
                    SqlConnection conn = new SqlConnection(cnString1);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    AutoCompleteStringCollection acBusIDSorce = new AutoCompleteStringCollection();
                    cmd.CommandText = "(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + Proname + "' AND gd.CompanyName='" + company + "' AND gd.Unit='" + unit + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in( Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))) union (Select  d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where ProductName='" + Proname + "' and MfgCompany='" + company + "' and Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15))  order by Id DESC ) and m.Id>(Select top 1 m.Id from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) order by Id DESC ) )  union SELECT d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null) and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and m.CompId=pm.CompId and (pm.StockIn=0 or pm.StockIn is null)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + Proname + "' AND gd.CompanyName='" + company + "' AND gd.Unit='" + unit + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))";
                    conn.Open();
                    dreader = cmd.ExecuteReader();
                    if (dreader.HasRows == true)
                    {
                        while (dreader.Read())
                            acBusIDSorce.Add(dreader["Code"].ToString());
                    }
                    else
                        MessageBox.Show("Data not Found");
                    dreader.Close();
                    TextBox txtBusID = e.Control as TextBox;
                    if (txtBusID != null)
                    {
                        txtBusID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtBusID.AutoCompleteCustomSource = acBusIDSorce;
                        txtBusID.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void dtgvcode_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (txtProductname.Text != "" && dtgvcode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
                {
                    codeflag = 0;
                    string Proname = txtProductname.Text.Trim().Split(',')[0];
                    string company = txtProductname.Text.Trim().Split(',')[1];
                    string unit = txtProductname.Text.Trim().Split(',')[2];
                    SqlDataAdapter da = new SqlDataAdapter("(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + Proname + "' AND gd.CompanyName='" + company + "' AND gd.Unit='" + unit + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in( Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))) union (Select  d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where ProductName='" + Proname + "' and MfgCompany='" + company + "' and Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15))  order by Id DESC ) and m.Id>(Select top 1 m.Id from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) order by Id DESC ) )  union SELECT d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null) and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and m.CompId=pm.CompId and (pm.StockIn=0 or pm.StockIn is null)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + Proname + "' AND gd.CompanyName='" + company + "' AND gd.Unit='" + unit + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    int intflag = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() != "")
                        {
                            if (dtgvcode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != dt.Rows[i][0].ToString())
                                intflag = 0;
                            else
                            {
                                intflag = 1;
                                goto a;
                            }
                        }
                    }
                    if (intflag == 0)
                    {
                        MessageBox.Show("This Serial No is not Saved.", "Warning");
                        dtgvcode.Visible = true;
                        dtgvcode.ClearSelection();
                        dtgvcode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                        dtgvcode.CurrentCell = dtgvcode[e.ColumnIndex, e.RowIndex];
                        dtgvcode.BeginEdit(true);
                        codeflag = 1;
                        goto b;
                    }
                a: { }
                    if (dtgvcode.RowCount > 0)
                    {
                        for (int i = 0; i < dtgvcode.RowCount; i++)
                        {
                            if (e.RowIndex != i)
                            {
                                if (dtgvcode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == dtgvcode.Rows[i].Cells[e.ColumnIndex].Value.ToString())
                                {
                                    MessageBox.Show("This Serial No is already Added to List.", "Warning");
                                    dtgvcode.Visible = true;
                                    dtgvcode.ClearSelection();
                                    dtgvcode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                                    dtgvcode.CurrentCell = dtgvcode.Rows[e.RowIndex].Cells[e.ColumnIndex];
                                    dtgvcode.CurrentCell.Selected = true;
                                    dtgvcode.BeginEdit(true);
                                    codeflag = 1;
                                    break;
                                }
                            }
                        }
                    }
                }
            b: { }
                if (dtgvcode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "")
                {
                    MessageBox.Show("All Quantity must have Serial No or Code.", "Warning");
                    dtgvcode.Visible = true;
                    dtgvcode.CurrentCell.Selected = true;
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
            if (keyData == (Keys.F11))
            {
                if (txtProductname.Text != "")
                {
                    string Proname = txtProductname.Text.Trim().Split(',')[0];
                    string company = txtProductname.Text.Trim().Split(',')[1];
                    string unit = txtProductname.Text.Trim().Split(',')[2];
                    ProductRepository productrepo = new ProductRepository();
                    ProductInformation pData = new ProductInformation();
                    if (CommonMethod.commProduct == true)
                        pData = productrepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                    else
                        pData = productrepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();

                    if (pData != null)
                    {
                        if (pData.Image != null)
                        {
                            pictureBox1.Visible = true;
                            pictureBox1.Height = 283;
                            pictureBox1.Width = 295;
                            byte[] @photoProduct;
                            @photoProduct = (byte[])pData.Image;
                            System.IO.MemoryStream ms = new System.IO.MemoryStream(@photoProduct);
                            pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                            this.ActiveControl = txtProductname;
                        }
                        else
                        {
                            pictureBox1.Image = null;
                            MessageBox.Show("Image Not Available.", "Warning");
                            this.ActiveControl = txtProductname;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Enter Product Name First.", "Warning");
                    this.ActiveControl = txtProductname;
                }
            }
            if (keyData == Keys.F3)
            {
                if (btnsave.Enabled == true)
                    btnsave_Click(null, null);
                else if (btnUpdate.Enabled == true)
                    btnUpdate_Click(null, null);
                this.ActiveControl = cmbcustomername;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Visible = false;
                this.ActiveControl = txtProductname;
            }
            catch (Exception)
            { }
        }

        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbUnit.Text != "" || cmbUnit.Text != "Select")
                {
                    if (txtProductname.Text != "")
                    {
                        string proname = "", company = "", unit = "";
                        if ((txtProductname.Text).Contains(","))
                        {
                            try
                            {
                                proname = txtProductname.Text.Trim().Split(',')[0];
                                company = txtProductname.Text.Trim().Split(',')[1];
                                unit = txtProductname.Text.Trim().Split(',')[2];
                            }
                            catch (Exception)
                            { }
                            ProductRepository productrepo = new ProductRepository();
                            ProductInformation productdata = new ProductInformation();
                            if (CommonMethod.commProduct == true)
                                productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                            else
                                productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            productrepo.Dispose();
                            if (productdata == null)
                            {
                                MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                                txtProductname.Text = "";
                                this.ActiveControl = txtProductname;
                            }
                            else
                            {
                                if (colIndex != -1)
                                {
                                    if (txtProductname.Text != Convert.ToString(GvProductInfo.Rows[colIndex].Cells["ProductNameg"].Value) + "," + GvProductInfo.Rows[colIndex].Cells["Company"].Value + "," + GvProductInfo.Rows[colIndex].Cells["Unit"].Value)
                                    {
                                        if (cmbUnit.Text == productdata.Uniti)
                                        {
                                            if (CustomerType == "Retailer" || CustomerType == null)
                                                txtrate.Text = Convert.ToString(productdata.Rate);
                                            else
                                            {
                                                if (productdata.DealerRate != null)
                                                    txtrate.Text = Convert.ToString(productdata.DealerRate);
                                                else
                                                    txtrate.Text = Convert.ToString(productdata.Rate);
                                            }
                                        }
                                        else if (cmbUnit.Text == productdata.AlternateUnit)
                                            txtrate.Text = Convert.ToString(productdata.AlternateUnitRate);
                                    }
                                }
                                else
                                {
                                    if (cmbUnit.Text == productdata.Uniti)
                                    {
                                        if (CustomerType == "Retailer" || CustomerType == null)
                                            txtrate.Text = Convert.ToString(productdata.Rate);
                                        else
                                        {
                                            if (productdata.DealerRate != null)
                                                txtrate.Text = Convert.ToString(productdata.DealerRate);
                                            else
                                                txtrate.Text = Convert.ToString(productdata.Rate);
                                        }
                                    }
                                    else if (cmbUnit.Text == productdata.AlternateUnit)
                                        txtrate.Text = Convert.ToString(productdata.AlternateUnitRate);
                                }
                                ProductCategory = productdata.Category;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int i = e.KeyChar;
                if (i == 46 || (i >= 48 && i <= 57) || i == 8)
                { }
                else
                    e.Handled = true;
            }
            catch (Exception)
            { }
        }

        public void PCalculation()
        {
            try
            {
                SettingRepository settingrepo = new SettingRepository();
                var settingdata = settingrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).LastOrDefault();
                if (settingdata.VatOption == "Inclusive")
                {
                    //decimal d = 0, v = 0, damt = 0, vamt = 0, totalpamt = 0;
                    //string b = txtpvat.Text;
                    //decimal a = Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtrate.Text);
                    //if (a != 0)
                    //{
                    //    if (b == "0.00")
                    //        txtpvat.Text = Convert.ToString(settingdata.VatValue);
                    //}
                    //else
                    //    txtpvat.Text = "0";
                    //totalpamt = (a * 100) / (Convert.ToDecimal(txtpvat.Text) + 100);
                    //txtAmount.Text = Convert.ToString(Math.Round(totalpamt, 2, MidpointRounding.AwayFromZero));

                    //vamt = (Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(txtpvat.Text) / 100);
                    //lblpvat.Text = Convert.ToString(Math.Round(vamt, 2, MidpointRounding.AwayFromZero));
                    //if (txtpdiscount.Text != "")
                    //{
                    //    d = Convert.ToDecimal(txtpdiscount.Text);
                    //    damt = Convert.ToDecimal(txtAmount.Text) * d / 100;
                    //    lblpdiscount.Text = Convert.ToString(Math.Round(damt, 2, MidpointRounding.AwayFromZero));
                    //}
                    //txtpTotalAmt.Text = Convert.ToString(Math.Round(a - damt, 2, MidpointRounding.AwayFromZero));
                }
                else if (settingdata.VatOption == "Exclusive")
                {
                    decimal totalpamt = 0, damt = 0, vamt = 0;
                    lblpvat.Text = "0";
                    decimal a = Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtrate.Text);
                    if (txtpdiscount.Text != "")
                    {
                        damt = (a * Convert.ToDecimal(txtpdiscount.Text)) / 100;
                        lblpdiscount.Text = Convert.ToString(Math.Round(damt, 2, MidpointRounding.AwayFromZero));
                        txtpTotalAmt.Text = Convert.ToString(Math.Round(a - damt, 2, MidpointRounding.AwayFromZero));
                        if (cmbsgst.Text != "")
                        {
                            totalpamt = a - damt;
                            if (taxinclude == "True")
                            {
                                vamt = Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(cmbsgst.Text) / 100;
                                lblpvat.Text = Convert.ToString(Math.Round(vamt, 2, MidpointRounding.AwayFromZero));
                            }
                            else
                            {
                                vamt = ((a - damt) * Convert.ToDecimal(cmbsgst.Text)) / 100;
                                lblpvat.Text = Convert.ToString(Math.Round(vamt, 2, MidpointRounding.AwayFromZero));
                                totalpamt = a - damt + vamt;
                                txtpTotalAmt.Text = Convert.ToString(Math.Round(totalpamt, 2, MidpointRounding.AwayFromZero));
                            }
                        }
                    }
                    else if (cmbsgst.Text != "")
                    {
                        vamt = (Convert.ToDecimal(txtAmount.Text) * Convert.ToDecimal(cmbsgst.Text)) / 100;
                        lblpvat.Text = Convert.ToString(Math.Round(vamt, 2, MidpointRounding.AwayFromZero));
                        totalpamt = Convert.ToDecimal(txtAmount.Text) + vamt;
                        if (taxinclude == "False" || taxinclude == null)
                            txtpTotalAmt.Text = Convert.ToString(Math.Round(totalpamt, 2, MidpointRounding.AwayFromZero));
                        else
                            txtpTotalAmt.Text = Convert.ToString(Math.Round(a, 2, MidpointRounding.AwayFromZero));
                    }
                    else
                        txtpTotalAmt.Text = Convert.ToString(Math.Round(a, 2, MidpointRounding.AwayFromZero));
                }
                settingrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void CalculateNetAmt()
        {
            decimal v, h, t, total, damt = 0, vamt = 0, netamt;
            total = Convert.ToDecimal(txtTotalAmt.Text);
            if (txtDiscount.Text != "")// && Convert.ToDecimal(txtDiscount.Text) != 0)
            {
                damt = (total * Convert.ToDecimal(txtDiscount.Text)) / 100;
                lbldiscount.Text = Convert.ToString(Math.Round(damt, 2, MidpointRounding.AwayFromZero));
            }
            if (vat5amt == 0 && vat12amt == 0)
            {
                if (txtVat.Text != "")
                    v = Convert.ToDecimal(txtVat.Text);
                else
                    v = 0;
                vamt = ((total - damt) * v) / 100;
                lblvat.Text = Convert.ToString(Math.Round(vamt, 2, MidpointRounding.AwayFromZero));
            }
            if (txttranchrg.Text != "")
                t = Convert.ToDecimal(txttranchrg.Text);
            else
                t = 0;
            if (txthamalichrg.Text != "")
                h = Convert.ToDecimal(txthamalichrg.Text);
            else
                h = 0;
            netamt = total - Math.Round(damt, 2, MidpointRounding.AwayFromZero) + Math.Round(vamt, 2, MidpointRounding.AwayFromZero) + t + h;
            txtNetAmt.Text = Convert.ToString(netamt);
        }

        private void txtAmount_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtAmount.Text != "")
                {
                    if (txtQuantity.Text != "")
                        getGstCalculation();
                }
            }
            catch (Exception)
            { }
        }

        private void txtpTotalAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                getGstCalculation();
            }
            catch (Exception)
            { }
        }

        private void txttranchrg_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txttranchrg.Text != "")
                    CalculateNetAmt();
                else
                {
                    txttranchrg.Text = "0";
                    this.ActiveControl = txttranchrg;
                }
            }
            catch (Exception)
            {
                txttranchrg.Text = "0";
                this.ActiveControl = txttranchrg;
            }
        }

        private void txthamalichrg_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txttranchrg.Text != "")
                    CalculateNetAmt();
                else
                {
                    txthamalichrg.Text = "0";
                    this.ActiveControl = txthamalichrg;
                }
            }
            catch (Exception)
            {
                txthamalichrg.Text = "0";
                this.ActiveControl = txthamalichrg;
            }
        }

        private void getGstCalculation()
        {
            try
            {
                if (taxinclude == "False")
                {
                    decimal discamt = 0, taxableamt = 0, SGstamt = 0, CGstamt = 0, IGstamt = 0;
                    taxableamt = Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtrate.Text);
                    txtAmount.Text = Convert.ToString(Math.Round(taxableamt, 2, MidpointRounding.AwayFromZero));
                    lblBasicRate.Text = txtrate.Text;
                    if (txtpdiscount.Text != "")
                    {
                        discamt = taxableamt * Convert.ToDecimal(txtpdiscount.Text) / 100;
                        taxableamt = taxableamt - discamt;
                        lblpdiscount.Text = Convert.ToString(Math.Round(discamt, 2, MidpointRounding.AwayFromZero));
                    }
                    if (cmbsgst.Text != "")
                    {
                        SGstamt = taxableamt * Convert.ToDecimal(cmbsgst.Text) / 100;
                        lblpvat.Text = Convert.ToString(Math.Round(SGstamt, 2, MidpointRounding.AwayFromZero));
                    }
                    if (cmbcgst.Text != "")
                    {
                        CGstamt = taxableamt * Convert.ToDecimal(cmbcgst.Text) / 100;
                        lblCGst.Text = Convert.ToString(Math.Round(CGstamt, 2, MidpointRounding.AwayFromZero));
                    }
                    if (cmbigst.Text != "")
                    {
                        IGstamt = taxableamt * Convert.ToDecimal(cmbigst.Text) / 100;
                        lblIGst.Text = Convert.ToString(Math.Round(IGstamt, 2, MidpointRounding.AwayFromZero));
                    }
                    taxableamt = taxableamt + SGstamt + CGstamt + IGstamt;
                    txtpTotalAmt.Text = Convert.ToString(Math.Round(taxableamt, 2, MidpointRounding.AwayFromZero));
                }
                else
                {  // tax inclusive product
                    decimal discamt = 0, taxableamt = 0, SGstamt = 0, CGstamt = 0, IGstamt = 0, cgstpercent = 0, sgstpercent = 0, igstpercent = 0, basicrate = 0, CalculatedAmt = 0, BasicTaxableAmt = 0, basicDiscamt = 0;
                    cgstpercent = Convert.ToDecimal(cmbcgst.Text);
                    sgstpercent = Convert.ToDecimal(cmbsgst.Text);
                    igstpercent = Convert.ToDecimal(cmbigst.Text);
                    basicrate = Convert.ToDecimal(txtrate.Text) * 100 / (100 + (cgstpercent + sgstpercent + igstpercent));
                    lblBasicRate.Text = Convert.ToString(Math.Round(basicrate, 2, MidpointRounding.AwayFromZero));
                    CalculatedAmt = Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtrate.Text);
                    txtAmount.Text = Convert.ToString(Math.Round(CalculatedAmt, 2, MidpointRounding.AwayFromZero));
                    BasicTaxableAmt = basicrate * Convert.ToDecimal(txtQuantity.Text);
                    taxableamt = Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtrate.Text);
                    txtAmount.Text = Convert.ToString(Math.Round(taxableamt, 2, MidpointRounding.AwayFromZero));
                    if (txtpdiscount.Text != "")
                    {
                        discamt = taxableamt * Convert.ToDecimal(txtpdiscount.Text) / 100;
                        taxableamt = taxableamt - discamt;
                        basicDiscamt = BasicTaxableAmt * Convert.ToDecimal(txtpdiscount.Text) / 100;
                        BasicTaxableAmt = BasicTaxableAmt - basicDiscamt;
                        lblpdiscount.Text = Convert.ToString(Math.Round(basicDiscamt, 2, MidpointRounding.AwayFromZero));
                    }
                    if (cmbsgst.Text != "")
                    {
                        SGstamt = BasicTaxableAmt * Convert.ToDecimal(cmbsgst.Text) / 100;
                        lblpvat.Text = Convert.ToString(Math.Round(SGstamt, 2, MidpointRounding.AwayFromZero));
                    }
                    if (cmbcgst.Text != "")
                    {
                        CGstamt = BasicTaxableAmt * Convert.ToDecimal(cmbcgst.Text) / 100;
                        lblCGst.Text = Convert.ToString(Math.Round(CGstamt, 2, MidpointRounding.AwayFromZero));
                    }
                    if (cmbigst.Text != "")
                    {
                        IGstamt = BasicTaxableAmt * Convert.ToDecimal(cmbigst.Text) / 100;
                        lblIGst.Text = Convert.ToString(Math.Round(IGstamt, 2, MidpointRounding.AwayFromZero));
                    }
                    taxableamt = BasicTaxableAmt + SGstamt + CGstamt + IGstamt;
                    txtpTotalAmt.Text = Convert.ToString(Math.Round(taxableamt, 2, MidpointRounding.AwayFromZero));
                }
            }
            catch (Exception)
            { }
        }

        private void cmbsgst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbsgst.SelectedIndex > 0)
                {
                    cmbigst.Enabled = false;
                    getGstCalculation();
                    cmbcgst.SelectedValue = cmbsgst.SelectedValue;
                }
                else
                {
                    lblpvat.Text = "0";
                    getGstCalculation();
                    cmbcgst.SelectedValue = cmbsgst.SelectedValue;
                    cmbigst.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbcgst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbcgst.SelectedIndex > 0)
                {
                    cmbigst.Enabled = false;
                    getGstCalculation();
                    cmbsgst.SelectedValue = cmbcgst.SelectedValue;
                }
                else
                {
                    lblCGst.Text = "0";
                    getGstCalculation();
                    cmbsgst.SelectedValue = cmbcgst.SelectedValue;
                    cmbigst.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbigst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbigst.SelectedIndex > 0)
                {
                    cmbsgst.SelectedIndex = 0;
                    cmbcgst.SelectedIndex = 0;
                    cmbsgst.Enabled = false;
                    cmbcgst.Enabled = false;
                    getGstCalculation();
                }
                else
                {
                    lblIGst.Text = "0";
                    getGstCalculation();
                    cmbsgst.Enabled = true;
                    cmbcgst.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void txtprefixproduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && txtprefixproduct.Text != "")
            {
                PrefixToBind = true;
                txtProductname.Text = txtprefixproduct.Text;
                txtprefixproduct.Visible = false;
                this.ActiveControl = txtProductname;
                if (txtProductname.Text != "")
                    this.ActiveControl = txtQuantity;
                else
                    this.ActiveControl = txtProductname;
            }
        }

        private void cmbcomanyname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PrefixToBind = false;
                if (cmbcomanyname.Text != "" && cmbcomanyname.SelectedIndex != 0)
                {
                    getProductsbyPrefix(cmbcomanyname.Text);
                    this.ActiveControl = txtprefixproduct;
                }
                else
                    this.ActiveControl = txtProductname;
            }
            if (Control.ModifierKeys == Keys.Alt)
            {
                if (e.KeyCode == Keys.N)
                {
                    frmProductInformation product = new frmProductInformation();
                    product.ShowDialog();
                    fillcompany();
                    this.ActiveControl = cmbcomanyname;
                }
            }
        }

        private void cmbcomanyname_MouseClick(object sender, MouseEventArgs e)
        {
            PrefixToBind = false;
        }

        private void txtProductname_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtQuantity;
            }
            catch (Exception)
            { }
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtrate;
            }
            catch (Exception)
            { }
        }

        private void txtrate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnAdd;
            }
            catch (Exception)
            { }
        }

        private void txtrate_Enter(object sender, EventArgs e)
        {
            refreshFlag = false;
        }

        private void txtprefixproduct_Leave(object sender, EventArgs e)
        {
            try
            {
                txtprefixproduct.Visible = false;
                this.ActiveControl = cmbcomanyname;
            }
            catch (Exception)
            { }
        }

        private void dtgvcode_EditingControlShowing_1(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dtgvcode.CurrentCell.ColumnIndex == 0)
                {
                    string Proname = txtProductname.Text.Trim().Split(',')[0];
                    string company = txtProductname.Text.Trim().Split(',')[1];
                    string unit = txtProductname.Text.Trim().Split(',')[2];
                    string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                    SqlDataReader dreader;
                    SqlConnection conn = new SqlConnection(cnString1);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    AutoCompleteStringCollection acBusIDSorce = new AutoCompleteStringCollection();

                    cmd.CommandText = "(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + Proname + "' AND gd.CompanyName='" + company + "' AND gd.Unit='" + unit + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in( Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))) union (Select  d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where ProductName='" + Proname + "' and MfgCompany='" + company + "' and Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15))  order by Id DESC ) and m.Id>(Select top 1 m.Id from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) order by Id DESC ) )  union SELECT d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null) and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and m.CompId=pm.CompId and (pm.StockIn=0 or pm.StockIn is null)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + Proname + "' AND gd.CompanyName='" + company + "' AND gd.Unit='" + unit + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))";
                    conn.Open();
                    dreader = cmd.ExecuteReader();
                    if (dreader.HasRows == true)
                    {
                        while (dreader.Read())
                            acBusIDSorce.Add(dreader["Code"].ToString());
                    }
                    else
                    {
                        if (HasMandatoryCodeSr == "Y")
                            MessageBox.Show("Data not Found");
                    }
                    dreader.Close();
                    if (HasMandatoryCodeSr == "Y")
                    {
                        TextBox txtBusID = e.Control as TextBox;
                        if (txtBusID != null)
                        {
                            txtBusID.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            txtBusID.AutoCompleteCustomSource = acBusIDSorce;
                            txtBusID.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmUnitInformation unit = new frmUnitInformation();
                        unit.ShowDialog();
                    }
                }
            }
            catch (Exception) { }
        }

        private void dtpquotationdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcustomername;
            }
            catch (Exception) { }
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txttranchrg;
            }
            catch (Exception) { }
        }

        private void txttranchrg_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txthamalichrg;
            }
            catch (Exception) { }
        }

        private void txthamalichrg_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsave;
            }
            catch (Exception) { }
        }

        private void dtgvFormulae_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != dtgvFormulae.Columns.Count - 1)
            {
                string productformulae = "";
                try
                {
                    if (txtProductname.Text != "")
                    {
                        string proname = "", company = "", unit = "";
                        if ((txtProductname.Text).Contains(","))
                        {
                            try
                            {
                                proname = txtProductname.Text.Trim().Split(',')[0];
                                company = txtProductname.Text.Trim().Split(',')[1];
                                unit = txtProductname.Text.Trim().Split(',')[2];
                            }
                            catch (Exception)
                            { }
                            ProductRepository productrepo = new ProductRepository();
                            var productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                            if (productdata == null)
                            {
                                MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                                txtProductname.Text = "";
                                this.ActiveControl = txtProductname;
                            }
                            else
                            {
                                if (productdata.Formulae != "" || productdata.Formulae != null)
                                    productformulae = productdata.Formulae.ToString();
                            }
                        }
                    }

                    string x = null;
                    for (int i = 0; i < productformulae.Length; i++)
                    {
                        string specialChar = @"+-*/";
                        foreach (var item in specialChar)
                        {
                            if (productformulae[i].ToString().Contains(item))
                            {
                                if (x == null)
                                    x = productformulae[i].ToString();
                                else
                                    x = x + ',' + productformulae[i].ToString();
                            }
                        }
                    }
                    string[] arraystr = new string[x.Length];
                    arraystr = x.ToString().Split(',');
                    decimal maxQtySlab = 0;
                    if (getSlabUnit == "In")
                    {
                        for (int m = 0; m < 20; m++)
                        {
                            maxQtySlab += qtySlab;
                            arrSlab[m] = maxQtySlab;
                        }
                    }
                    else
                    {
                        for (int m = 0; m < 20; m++)
                        {
                            maxQtySlab += (qtySlab * 12);  //1 ft
                            arrSlab[m] = maxQtySlab;
                        }
                    }
                    decimal calci = 0;
                    calci = getProperSlab(e.RowIndex, arraystr);  //get slab quantity
                    try
                    {
                        RateDecrease = dtgvFormulae.Rows[e.RowIndex].Cells["Rate(-)"].Value.ToString();
                        if (RateDecrease == "") { RateDecrease = "0"; }
                        if (Convert.ToDecimal(RateDecrease) != 0)
                            calci = getProperSlab(e.RowIndex, arraystr, Convert.ToDecimal(RateDecrease));
                        SizeSpecString = SizeSpecString + ",RD:" + RateDecrease;
                    }
                    catch (Exception)
                    {
                        RateDecrease = "0";
                    }
                    try
                    {
                        RateIncrease = dtgvFormulae.Rows[e.RowIndex].Cells["Rate(+)"].Value.ToString();
                        if (RateIncrease == "") { RateIncrease = "0"; }
                        if (Convert.ToDecimal(RateIncrease) != 0)
                            calci = getProperSlab(e.RowIndex, arraystr, 0, Convert.ToDecimal(RateIncrease));
                        SizeSpecString = SizeSpecString + ",RI:" + RateIncrease;
                    }
                    catch (Exception)
                    {
                        RateIncrease = "0";
                    }
                    string strName = "";
                    try
                    {
                        strName = dtgvFormulae.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                        if (!string.IsNullOrEmpty(strName))
                            SizeSpecString = SizeSpecString + ",Name:" + strName;
                    }
                    catch (Exception)
                    {
                        strName = "";
                    }
                    if (slabGroup == 'F')
                        dtgvFormulae.Rows[e.RowIndex].Cells[dtgvFormulae.Columns.Count - 4].Value = Convert.ToString(Math.Round(calci, 2, MidpointRounding.AwayFromZero));
                    else
                        dtgvFormulae.Rows[e.RowIndex].Cells[dtgvFormulae.Columns.Count - 2].Value = Convert.ToString(Math.Round(calci, 2, MidpointRounding.AwayFromZero));
                }
                catch (Exception)
                { }
            }
        }

        private decimal getProperSlab(int rowIndex, string[] arraystr, decimal rateDe = 0, decimal rateIn = 0)
        {
            int slabAppliedCount = 0;  // Slab applied to how many values of formula
            string tempSizedata = "";
            SizeSpecString = "";
            decimal calcQty = 0, GridQty = 0, unitQty = 0, productSlab = 0;
            bool boolrateDe = false, boolrateIn = false;
            ///============== Slab unit and rate increase and decrease unit considered same ===========
            for (int j = 0; j < dtgvFormulae.Columns.Count - 1; j++)
            {
                try
                {
                    if (dtgvFormulae.Rows[rowIndex].Cells[j].Value != null)  //e.RowIndex
                    {
                        if (dtgvFormulae.Columns[j].HeaderText != "Result")
                            GridQty = Math.Round(Convert.ToDecimal(dtgvFormulae.Rows[rowIndex].Cells[j].Value.ToString()), 2, MidpointRounding.AwayFromZero);
                        else
                            GridQty = 0;
                        if (dtgvFormulae.Columns[j].HeaderText != "Constant" && dtgvFormulae.Columns[j].HeaderText != "Result" && dtgvFormulae.Columns[j].HeaderText != "Name" && slabAppliedCount < FormulaConstCount)
                        {
                            decimal maxQtySlab = 0;
                            if (slabGroup == 'F')  //----- Fixed Size
                            {
                                for (int m = 0; m < 100; m++)
                                {
                                    if (getSlabUnit == "In")
                                    {
                                        maxQtySlab += qtySlab;
                                        productSlab = qtySlab;
                                    }
                                    else //feet
                                    {
                                        maxQtySlab += (qtySlab * 12);
                                        productSlab = (qtySlab * 12);
                                    }
                                    if (GridQty <= maxQtySlab && GridQty != 0)
                                    {
                                        if (rateDe != 0)
                                        {
                                            if (getSlabUnit == "Ft")
                                                unitQty = rateDe;
                                            if (getSlabUnit == "In")
                                                unitQty = rateDe / qtySlab;  // total slabs to decrease
                                            int arrcount = 0;
                                            for (int jj = arrSlab.Length - 1; jj >= 0; jj--)
                                            {
                                                if (arrSlab[jj] != 0 && arrSlab[jj] < GridQty)
                                                {
                                                    arrcount++;
                                                    if (arrcount == unitQty)
                                                    {
                                                        GridQty = arrSlab[jj];
                                                        boolrateDe = true;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        if (rateIn != 0)
                                        {
                                            if (getSlabUnit == "Ft")
                                                unitQty = rateIn;
                                            if (getSlabUnit == "In")
                                                unitQty = rateIn / qtySlab;  // total slabs to increase
                                            int arrcount = 0;
                                            for (int jj = 0; jj <= arrSlab.Length - 1; jj++)
                                            {
                                                if (arrSlab[jj] != 0 && arrSlab[jj] > GridQty)
                                                {
                                                    arrcount++;
                                                    if (arrcount == unitQty)
                                                    {
                                                        GridQty = arrSlab[jj];
                                                        boolrateIn = true;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        if (boolrateDe == false && boolrateIn == false)
                                            GridQty = maxQtySlab;
                                        //--------------------
                                        slabAppliedCount += 1;
                                        if (slabAppliedCount <= FormulaConstCount)
                                        {
                                            tempSizedata = SizeQty.ToString();
                                            if (SizeSpecString.Trim() == "")
                                                SizeSpecString = tempSizedata;
                                            else
                                                SizeSpecString = SizeSpecString + "*" + tempSizedata;
                                        }
                                        break;
                                    }
                                    else
                                        SizeQty = GridQty;
                                } //----------------- Fixed Size
                            }
                            else //-------------- Dynamic size
                            {
                                if (DynamicDtlSlabId != 0)
                                {
                                    decimal nearbyQty = 0;
                                    SlabDetailRepository slabdtlrepo = new SlabDetailRepository();
                                    List<SlabDetail> slablist = slabdtlrepo.GetAll().Where(t => t.SlabId == DynamicDtlSlabId).ToList();
                                    if (slablist != null)
                                    {
                                        decimal minDiff = 0, prevDiff = 100;
                                        foreach (var item in slablist)
                                        {
                                            if (slabAppliedCount == 0)
                                            {
                                                minDiff = Convert.ToDecimal(item.FromRange - GridQty);
                                                if (prevDiff > minDiff && minDiff >= 0)
                                                {
                                                    prevDiff = minDiff;
                                                    nearbyQty = Convert.ToDecimal(item.FromRange);
                                                }
                                            }
                                            else if (slabAppliedCount == 1)
                                            {
                                                minDiff = Convert.ToDecimal(item.ToRange - GridQty);
                                                if (prevDiff > minDiff && minDiff >= 0)
                                                {
                                                    prevDiff = minDiff;
                                                    nearbyQty = Convert.ToDecimal(item.ToRange);
                                                }
                                            }
                                        }
                                        SizeQty = GridQty;
                                        GridQty = nearbyQty;
                                        slabAppliedCount += 1;
                                        if (slabAppliedCount <= FormulaConstCount)
                                        {
                                            tempSizedata = SizeQty.ToString();
                                            if (SizeSpecString.Trim() == "")
                                                SizeSpecString = tempSizedata;
                                            else
                                                SizeSpecString = SizeSpecString + "*" + tempSizedata;
                                        }
                                    }
                                }
                            }
                        }
                        else if (dtgvFormulae.Columns[j].HeaderText != "Constant" && dtgvFormulae.Columns[j].HeaderText != "Result" && dtgvFormulae.Columns[j].HeaderText != "Name" && slabAppliedCount == FormulaConstCount)
                        {
                            if (FormulaConstCount == 0) // changes 21/5/2018
                            {
                                SizeQty = GridQty;
                                tempSizedata = SizeQty.ToString();
                                if (SizeSpecString.Trim() == "")
                                    SizeSpecString = tempSizedata;
                                else
                                    SizeSpecString = SizeSpecString + "*" + tempSizedata;
                                SizeQty = 0;
                            }
                            else
                            {
                                SizeQty = GridQty;
                                NewSizeQty = NewSizeQty + SizeQty;
                                SizeQty = NewSizeQty;
                            }
                        }
                        if (j == 0)
                            calcQty = GridQty;//Math.Round(Convert.ToDecimal(dtgvFormulae.Rows[e.RowIndex].Cells[j].Value.ToString()), 2, MidpointRounding.AwayFromZero);                      
                        else
                        {
                            if ((j - 1) < arraystr.Length)
                            {
                                if (arraystr[j - 1].ToString() == "*")
                                    calcQty = Math.Round(Convert.ToDecimal(calcQty * GridQty), 2, MidpointRounding.AwayFromZero);//Convert.ToDecimal(dtgvFormulae.Rows[e.RowIndex].Cells[j].Value.ToString())), 2, MidpointRounding.AwayFromZero);
                                else if (arraystr[j - 1].ToString() == "/")
                                    calcQty = Math.Round(Convert.ToDecimal(calcQty / GridQty), 2, MidpointRounding.AwayFromZero);//Convert.ToDecimal(dtgvFormulae.Rows[e.RowIndex].Cells[j].Value.ToString())), 2, MidpointRounding.AwayFromZero);
                                else if (arraystr[j - 1].ToString() == "+")
                                    calcQty = Math.Round(Convert.ToDecimal(calcQty + GridQty), 2, MidpointRounding.AwayFromZero);//Convert.ToDecimal(dtgvFormulae.Rows[e.RowIndex].Cells[j].Value.ToString())), 2, MidpointRounding.AwayFromZero);
                                else if (arraystr[j - 1].ToString() == "-")
                                    calcQty = Math.Round(Convert.ToDecimal(calcQty - GridQty), 2, MidpointRounding.AwayFromZero);//Convert.ToDecimal(dtgvFormulae.Rows[e.RowIndex].Cells[j].Value.ToString())), 2, MidpointRounding.AwayFromZero);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    return calcQty;
                }
            }
            return calcQty;
        }

        private void dtgvFormulae_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == dtgvFormulae.RowCount - 1 && e.ColumnIndex == dtgvFormulae.ColumnCount - 1)
                    intcell = 1;
                else
                    intcell = 0;
            }
            catch (Exception)
            { }
        }

        private void dtgvFormulae_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (intcell == 1)
                {
                    if (e.KeyCode == Keys.Tab)
                        e.Handled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void dtgvFormulae_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (intcell == 1)
                {
                    if (e.KeyChar == (char)Keys.Tab)
                        e.Handled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void getFormuleQuantity()
        {
            string productformulae = "";
            try
            {
                dtgvFormulae.EndEdit(DataGridViewDataErrorContexts.Commit);
                if (txtProductname.Text != "")
                {
                    string proname = "", company = "", unit = "";
                    if ((txtProductname.Text).Contains(","))
                    {
                        try
                        {
                            proname = txtProductname.Text.Split(',')[0];
                            company = txtProductname.Text.Split(',')[1];
                            unit = txtProductname.Text.Split(',')[2];
                        }
                        catch (Exception)
                        { }

                        ProductRepository productrepo = new ProductRepository();
                        var productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                        if (productdata == null)
                        {
                            MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                            txtProductname.Text = "";
                            this.ActiveControl = txtProductname;
                        }
                        else
                        {
                            if (productdata.Formulae != "" || productdata.Formulae != null)
                                productformulae = productdata.Formulae.ToString();
                        }
                    }
                }
                string x = null;
                for (int i = 0; i < productformulae.Length; i++)
                {
                    string specialChar = @"+-*/";
                    foreach (var item in specialChar)
                    {
                        if (productformulae[i].ToString().Contains(item))
                        {
                            if (x == null)
                                x = productformulae[i].ToString();
                            else
                                x = x + ',' + productformulae[i].ToString();
                        }
                    }
                }
                string[] arraystr = new string[x.Length];
                arraystr = x.ToString().Split(',');
                dtgvFormulae.Visible = false;
                string formdata = null, dtgvdata = null;
                decimal Result = 0;
                try
                {
                    int rowCount = Convert.ToInt32(dtgvFormulae.Rows.Count);
                    for (int ij = 0; ij < rowCount; ij++)
                    {
                        for (int j = 0; j < dtgvFormulae.Columns.Count; j++)
                        {
                            if (!string.IsNullOrEmpty(dtgvFormulae.Rows[ij].Cells[j].Value.ToString()))
                            {
                                String GridValue = dtgvFormulae.Rows[ij].Cells[j].Value.ToString();
                                if (!string.IsNullOrEmpty(dtgvFormulae.Rows[ij].Cells[j].Value.ToString()))  // condition added on 13042018 nilophar 
                                {
                                    dtgvdata = dtgvFormulae.Rows[ij].Cells[j].Value.ToString();
                                    if (formdata == null)
                                        formdata = dtgvdata + arraystr[j];
                                    else if (j == arraystr.Length)
                                        formdata = formdata + dtgvdata;
                                    else if (j == (arraystr.Length + 1) && j == (dtgvFormulae.Columns.Count - 4) && slabGroup == 'F')  //(Extra two columns Rate Increase and Rate Decrease)
                                    {
                                        formdata = formdata + "=" + Convert.ToString(Math.Round(Convert.ToDecimal(dtgvdata), 2, MidpointRounding.AwayFromZero));
                                        Result = Result + Math.Round(Convert.ToDecimal(dtgvdata), 2, MidpointRounding.AwayFromZero);
                                    }
                                    else if (j == (arraystr.Length + 1) && j == (dtgvFormulae.Columns.Count - 2) && slabGroup == 'D')
                                    {
                                        formdata = formdata + "=" + Convert.ToString(Math.Round(Convert.ToDecimal(dtgvdata), 2, MidpointRounding.AwayFromZero));
                                        Result = Result + Math.Round(Convert.ToDecimal(dtgvdata), 2, MidpointRounding.AwayFromZero);
                                    }
                                    else if (j == (dtgvFormulae.Columns.Count - 3) && slabGroup == 'F')
                                        formdata = formdata + " " + dtgvdata;
                                    else if (j == (dtgvFormulae.Columns.Count - 2) && slabGroup == 'F')
                                        formdata = formdata + " " + dtgvdata;
                                    else if (j == (arraystr.Length + 1) && Convert.ToString(dtgvFormulae.Columns[j].HeaderText) == "Result")
                                    {
                                        formdata = formdata + "=" + Convert.ToString(Math.Round(Convert.ToDecimal(dtgvdata), 2, MidpointRounding.AwayFromZero));
                                        Result = Result + Math.Round(Convert.ToDecimal(dtgvdata), 2, MidpointRounding.AwayFromZero);
                                    }
                                    else if (!string.IsNullOrEmpty(dtgvFormulae.Rows[ij].Cells["Name"].Value.ToString()) && Convert.ToString(dtgvFormulae.Columns[j].HeaderText) == "Name")
                                        formdata = formdata + " " + dtgvdata;
                                    else
                                        formdata = formdata + dtgvdata + arraystr[j];
                                }
                            }
                            else
                                formdata = formdata + " ";
                        }
                        if (ij != rowCount - 1)
                            formdata = formdata + ":";
                        else
                            formdata = formdata + "";
                    }
                }
                catch (Exception)
                { }

                if (formdata != null)
                    txtSubtitle.Text = formdata.ToString();
                txtQuantity.Text = Result.ToString();
                if (Result != 0)
                {
                    txtQuantity.Text = Result.ToString();
                    txtQuantity.Enabled = false;
                }
                else
                    txtQuantity.Enabled = true;
                getFormula = false;
                this.ActiveControl = txtrate;
            }
            catch (Exception)
            { }
        }

        private void dtgvFormulae_Leave(object sender, EventArgs e)
        {
            getFormuleQuantity();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbcustomername.Text != "Select" && cmbcustomername.Text != "")
                {
                    if (GvProductInfo.Rows.Count != 0)
                    {
                        QuotationMasterRepository Qmasterrepo = new QuotationMasterRepository();
                        QuotationDetailRepository Qdetailrepo = new QuotationDetailRepository();
                        QuotationMaster billdata = new QuotationMaster();

                        billdata.QuotationNo = Convert.ToInt32(txtquotationno.Text);
                        billdata.Quotationdate = Convert.ToDateTime(dtpquotationdate.Value.ToShortDateString());
                        billdata.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                        billdata.CustomerName = cmbcustomername.Text;
                        if (txtDiscount.Text != "")
                            billdata.Discount = Convert.ToDecimal(txtDiscount.Text);
                        else
                            billdata.Discount = 0;
                        if (txtNetAmt.Text != "")
                            billdata.NetAmount = Convert.ToDecimal(txtNetAmt.Text);
                        else
                            billdata.NetAmount = Convert.ToDecimal(txtTotalAmt.Text);
                        if (txtTotalAmt.Text != "")
                            billdata.TotalAmount = Convert.ToDecimal(txtTotalAmt.Text);
                        else
                            billdata.TotalAmount = 0;
                        if (txtVat.Text != "")
                            billdata.Vat = Convert.ToDecimal(txtVat.Text);
                        else
                            billdata.Vat = 0;
                        if (txttranchrg.Text != "")
                            billdata.TransportCharges = Convert.ToDecimal(txttranchrg.Text);
                        else
                            billdata.TransportCharges = 0;
                        if (txthamalichrg.Text != "")
                            billdata.HamaliCharges = Convert.ToDecimal(txthamalichrg.Text);
                        else
                            billdata.HamaliCharges = 0;
                        billdata.TransactionYear = CommonMethod.TransactionYear;
                        billdata.CompId = CommonMethod.CompId;
                        billdata.Status = 1;
                        billdata.Id = QuotationNo;
                        Qmasterrepo.Edit(billdata);
                        Qmasterrepo.Save();
                        if (deleteid.Count != 0)
                        {
                            for (int x = 0; x < deleteid.Count; x++)
                            {
                                QuotationDetailRepository Qdetailrepo1 = new QuotationDetailRepository();
                                QuotationDetail quotationcontroldateil = Qdetailrepo1.GetById(deleteid[x]);
                                Qdetailrepo1.Remove(quotationcontroldateil);
                                Qdetailrepo1.Save();
                                Qdetailrepo1.Dispose();
                            }
                        }
                        QuotationDetail billdetaildata = new QuotationDetail();
                        List<QuotationDetail> list = Qdetailrepo.GetAll().Where(t => t.QuotationNo == billdata.Id && t.QuotationMaster.TransactionYear == CommonMethod.TransactionYear && t.QuotationMaster.CompId == CommonMethod.CompId).ToList();
                        for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                        {
                            try
                            {
                                db.connect();
                                SqlCommand command = new SqlCommand("SPQuotationSaveUpdate", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;
                                if (Convert.ToInt32(GvProductInfo.Rows[i].Cells["Id"].Value) != 0)
                                {
                                    command.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(GvProductInfo.Rows[i].Cells["Id"].Value);
                                    command.Parameters.Add("@event", SqlDbType.Int).Value = 2;
                                }
                                else
                                {
                                    command.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                    command.Parameters.Add("@event", SqlDbType.Int).Value = 1;
                                }
                                command.Parameters.Add("@QuotationNo", SqlDbType.Int).Value = billdata.Id;
                                command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                command.Parameters.Add("@SaleUnit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["SaleUnit"].Value);
                                command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantityg"].Value);
                                command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                                command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Rate"].Value);
                                command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Amount"].Value);
                                command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Disc"].Value);
                                command.Parameters.Add("@Vat", SqlDbType.Decimal).Value = 0;
                                command.Parameters.Add("@SGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SGST"].Value);
                                command.Parameters.Add("@SGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SGSTAmt"].Value);
                                command.Parameters.Add("@CGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["CGST"].Value);
                                command.Parameters.Add("@CGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["CGSTAmt"].Value);
                                command.Parameters.Add("@IGstPercent", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["IGST"].Value);
                                command.Parameters.Add("@IGstAmt", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["IGSTAmt"].Value);
                                command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["TotalAmount"].Value);
                                command.Parameters.Add("@HSNCode", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["HSNCode"].Value);
                                command.Parameters.Add("@SaleRate", SqlDbType.Decimal).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["SaleRate"].Value);
                                command.Parameters.Add("@Prodsubtitle", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Subtitle"].Value);
                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                                command.ExecuteNonQuery();
                                db.CloseConnection();
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
                        }
                        MessageBox.Show("Quotation Updated.", "Success");
                        GvProductInfo.Rows.Clear();
                        clear();
                        this.ActiveControl = btnprint;
                        Qmasterrepo.Dispose();
                        Qdetailrepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Add Product Information.", "Warning");
                        this.ActiveControl = txtProductname;
                    }
                }
                else
                {
                    MessageBox.Show("Select Customer Name.", "Warning");
                    this.ActiveControl = cmbcustomername;
                }
            }
            catch (Exception)
            { }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to Delete this Quotation ?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    int QutMId = 0;
                    QuotationMasterRepository QutMastRepo = new QuotationMasterRepository();
                    var QuotationData = QutMastRepo.GetAll().Where(t => t.QuotationNo == Convert.ToInt32(txtquotationno.Text) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && t.Status == 1).FirstOrDefault();
                    if (QuotationData != null)
                    {
                        // add details for history table
                        try
                        {
                            DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                            DeleteHistoryTable historydata = new DeleteHistoryTable();
                            historydata.DeleteType = "Quotation";
                            historydata.BillNo = Convert.ToString(QuotationData.QuotationNo);
                            historydata.SupplierOrCustomerName = Convert.ToString(QuotationData.CustomerName);
                            historydata.Amount = Convert.ToDecimal(QuotationData.NetAmount);
                            historydata.CurrentDate = DateTime.Now.Date;
                            historydata.BillDate = Convert.ToDateTime(QuotationData.Quotationdate);
                            historydata.CompId = CommonMethod.CompId;
                            historydata.TransactionYear = CommonMethod.TransactionYear;
                            historyRepo.Add(historydata);
                            historyRepo.Save();
                        }
                        catch (Exception) { }
                        QutMId = Convert.ToInt32(QuotationData.Id);
                        DataTable dt = db.GetTable("delete from QuotationDetail where QuotationNo='" + QutMId + "' ");
                        DataTable dt1 = db.GetTable("delete from QuotationMaster where Id='" + QutMId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                        MessageBox.Show("Record Deleted Sucessfully.", "Success");
                        dt.Dispose();
                        dt1.Dispose();
                        db.CloseConnection();
                        GvProductInfo.Rows.Clear();
                        clear();
                        this.ActiveControl = dtpquotationdate;
                        btnDelete.Enabled = false;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void GvProductInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GvProductInfo.Columns[e.ColumnIndex].HeaderText == "Remove")
                //if (e.ColumnIndex == 17)
                {
                    if (GvProductInfo.Rows.Count != 1 && btnsave.Enabled == false)
                    {
                        if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        {
                            txtDiscount.Text = "";
                            txtVat.Text = "";
                            lbldiscount.Text = "0";
                            lblvat.Text = "0";
                            string pname1 = "", productstring = GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value.ToString();
                            if (productstring.Contains(',') == true)
                            {
                                string[] strtext = productstring.Split(',');
                                pname1 = strtext[1];
                                subtitlestring = strtext[0];
                            }
                            else
                                pname1 = GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value.ToString();
                            if (Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value) != 0)
                            {
                                deleteid.Add(Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value));
                                GvProductInfo.Rows.Remove(GvProductInfo.Rows[e.RowIndex]);                                
                            }
                            else
                                GvProductInfo.Rows.Remove(GvProductInfo.Rows[e.RowIndex]);
                            totalamt = 0;
                            totalamt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["TotalAmount"].Value));
                            txtTotalAmt.Text = Convert.ToString(totalamt);
                            totalamt = 0;
                            VatCalculation();
                        }
                        clearatADD();
                    }
                    else if (btnsave.Enabled == true)
                    {
                        txtDiscount.Text = "";
                        txtVat.Text = "";
                        lbldiscount.Text = "0";
                        lblvat.Text = "0";
                        if (Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value) != 0)
                        {
                            deleteid.Add(Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value));
                            GvProductInfo.Rows.Remove(GvProductInfo.Rows[e.RowIndex]);                            
                        }
                        else
                            GvProductInfo.Rows.Remove(GvProductInfo.Rows[e.RowIndex]);
                        totalamt = 0;
                        totalamt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["TotalAmount"].Value));
                        txtTotalAmt.Text = Convert.ToString(totalamt);
                        totalamt = 0;
                        VatCalculation();
                        clearatADD();
                    }
                    else
                        MessageBox.Show("This Invoice has only 1 product." + Environment.NewLine + "You have to delete full quotation.", "Warning");
                }
                else
                {
                    txtpdiscount.Text = "";
                    lblpvat.Text = "0";
                    lblpdiscount.Text = "0";
                    string pname1 = "", productstring = GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value.ToString();
                    if (productstring.Contains(',') == true)
                    {
                        pname1 = productstring.Split(',')[1];
                        subtitlestring = productstring.Split(',')[0];
                    }
                    else
                        pname1 = GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value.ToString();
                    txtProductname.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) + "," + GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value + "," + GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value;
                    cmbUnit.Items.Clear();
                    cmbUnit.DisplayMember = "Text";
                    cmbUnit.ValueMember = "Value";
                    cmbUnit.Items.Add(new { Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value), Value = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value) });
                    if (Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleUnit"].Value) != Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value))
                        cmbUnit.Items.Add(new { Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleUnit"].Value), Value = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleUnit"].Value) });
                    cmbUnit.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleUnit"].Value);
                    txtQuantity.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Quantityg"].Value);
                    if (ProductCategory == "Motor")
                    {
                        dtgvcode.Rows.Clear();
                        dtgvcode.Rows.Add();
                        dtgvcode.Rows[0].Cells[0].Value = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Code"].Value);
                    }
                    lblBasicRate.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Rate"].Value);
                    txtAmount.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Amount"].Value);
                    txtpdiscount.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Disc"].Value);
                    cmbsgst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SGST"].Value);
                    lblpvat.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SGSTAmt"].Value);
                    cmbcgst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["CGST"].Value);
                    lblCGst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["CGSTAmt"].Value);
                    cmbigst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["IGST"].Value);
                    lblIGst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["IGSTAmt"].Value);
                    txtpTotalAmt.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["TotalAmount"].Value);
                    txtHSNCode.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["HSNCode"].Value);
                    txtrate.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleRate"].Value);
                    txtSubtitle.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Subtitle"].Value);
                    colIndex = e.RowIndex;
                    this.ActiveControl = txtProductname;
                }
            }
            catch (Exception)
            { }
        }

        private void btnCustForm_Click(object sender, EventArgs e)
        {
            try
            {
                frmcustomer cust = new frmcustomer();
                cust.ShowDialog();
                customerdata();
            }
            catch (Exception)
            { }
        }
    }
}