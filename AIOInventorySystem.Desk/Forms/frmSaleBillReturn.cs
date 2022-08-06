using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmSaleBillReturn : Form
    {
        public static frmSaleBillReturn custbillinfo;
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();

        public decimal totalamt = 0, mvat = 0, mdvat = 0, rdoff = 1;
        public int BillMasterID, qdid, colIndex = -1, sbcolIndex = -1, custmasterid, billtype, intamtchk, productentrytype;
        public static decimal uqty = 0;
        public string printId, custbillno, ProductCategory, taxinclude;
        static string SBexpiryDate, cust_BillNo;
        List<int> deleteid = new List<int>();
        List<CustomerBillDetail> CustDPredata = new List<CustomerBillDetail>();
        ToolTip tooltipbtn = new ToolTip();
        private bool flgdiscountchange = false, userseriesSettings = false;

        public frmSaleBillReturn()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxbillid();
            customerdata();
            fillgst();
            getSettingDetails();
            dgvqtydetails.Visible = false;
            btnUpdate.Enabled = false;
            CustDPredata.Clear();
        }

        public frmSaleBillReturn(int type, string a)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            billtype = type;
            customerdata();
            fillgst();
            getSettingDetails();
            btnUpdate.Enabled = false;
            dgvqtydetails.Visible = false;
            this.ActiveControl = txtbillno;
        }

        public frmSaleBillReturn(string billno)
        {
            custbillno = billno;
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            dgvqtydetails.Visible = false;
            btnsave.Enabled = true;
            maxbillid();
            customerdata();
            fillgst();
            getSettingDetails();
            this.ActiveControl = txtbillno;
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
                    if (userseriesSettings == false)
                        txtbillno.Text = Convert.ToString(custmasterData.BillNo);
                    else
                    {
                        txtbillno.Text = Convert.ToString(custmasterData.UserSeries);
                        billno = Convert.ToString(custmasterData.BillNo);
                    }
                    dtpbilldate.Value = custmasterData.Billdate.Value;
                    cmbcustomername.Text = custmasterData.CustomerName;
                    dtppaymentdate.Value = custmasterData.PaymentDate.Value;
                    txtSBtotalamt.Text = Convert.ToString(custmasterData.TotalAmount);
                    txtSBDisc.Text = Convert.ToString(custmasterData.Discount);
                    txtSBTransChrg.Text = Convert.ToString(custmasterData.TransportCharges);
                    txtSBHamliChrgs.Text = Convert.ToString(custmasterData.HamaliCharges);
                    txtSBNetAmt.Text = Convert.ToString(custmasterData.NetAmount);
                    txtSBPaidAmt.Text = Convert.ToString(custmasterData.PaidAmount);
                    txtSBRemAmt.Text = Convert.ToString(custmasterData.RemainingAmount);
                    txtDiscount.Text = Convert.ToString(custmasterData.Discount);
                    decimal tempNetamt = Convert.ToDecimal(custmasterData.NetAmount);
                    decimal tempPaidamt = Convert.ToDecimal(custmasterData.PaidAmount);
                    decimal tempRemamt = Convert.ToDecimal(custmasterData.RemainingAmount);
                    mvat = Convert.ToDecimal(custmasterData.Vat);
                    decimal sbdamt = ((Convert.ToDecimal(custmasterData.TotalAmount) * Convert.ToDecimal(custmasterData.Discount)) / 100);
                    decimal sbvamt = (((Convert.ToDecimal(custmasterData.TotalAmount) - sbdamt) * Convert.ToDecimal(custmasterData.Vat)) / 100);
                    decimal sbcalamt = (Convert.ToDecimal(custmasterData.TotalAmount) - sbdamt + sbvamt + Convert.ToDecimal(custmasterData.TransportCharges) + Convert.ToDecimal(custmasterData.HamaliCharges));
                    if (sbcalamt == tempNetamt)
                        intamtchk = 0;
                    else
                        intamtchk = 1;
                    BillMasterID = custmasterData.Id;
                    var billDetailData = custmasterData.CustomerBillDetails;
                    GvProductInfo.Rows.Clear();
                    int i = 0;
                    foreach (var item in billDetailData)
                    {
                        if (item.AssemblyChildStatus == "N" || item.AssemblyChildStatus == null || item.AssemblyChildStatus == "")
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
                                GvProductInfo.Rows[i].Cells["Quantityg"].Value = item.Quantity;
                            GvProductInfo.Rows[i].Cells["Code"].Value = item.Code;
                            GvProductInfo.Rows[i].Cells["Rate"].Value = item.Rate;
                            GvProductInfo.Rows[i].Cells["Amount"].Value = item.Amount;
                            GvProductInfo.Rows[i].Cells["Disc"].Value = item.Discount;
                            GvProductInfo.Rows[i].Cells["SpDisc"].Value = item.SpcDisc;
                            GvProductInfo.Rows[i].Cells["SGST"].Value = item.SGstPercent;
                            GvProductInfo.Rows[i].Cells["SGSTAmt"].Value = item.SGstAmt;
                            GvProductInfo.Rows[i].Cells["CGST"].Value = item.CGstPercent;
                            GvProductInfo.Rows[i].Cells["CGSTAmt"].Value = item.CGstAmt;
                            GvProductInfo.Rows[i].Cells["IGST"].Value = item.IGstPercent;
                            GvProductInfo.Rows[i].Cells["IGSTAmt"].Value = item.IGstAmt;
                            GvProductInfo.Rows[i].Cells["Cess"].Value = item.Cess;
                            GvProductInfo.Rows[i].Cells["CessAmt"].Value = item.CessAmt;
                            GvProductInfo.Rows[i].Cells["SaleRate"].Value = item.SaleRate;
                            GvProductInfo.Rows[i].Cells["HSNCode"].Value = item.HSNCode;
                            GvProductInfo.Rows[i].Cells["TotalAmount"].Value = item.TotalAmount;
                            if (!string.IsNullOrEmpty(item.BatchNo))
                                GvProductInfo.Rows[i].Cells["BatchNo"].Value = item.BatchNo;
                            else
                                GvProductInfo.Rows[i].Cells["BatchNo"].Value = "";
                            if (!string.IsNullOrEmpty(Convert.ToString(item.Expiry)))
                                GvProductInfo.Rows[i].Cells["ExpiryDate"].Value = item.Expiry;
                            else
                                GvProductInfo.Rows[i].Cells["ExpiryDate"].Value = DateTime.Now.Date;
                            GvProductInfo.Rows[i].Cells["Barcode"].Value = item.Barcode;
                            GvProductInfo.Rows[i].Cells["Sizes"].Value = item.Size;
                            GvProductInfo.Rows[i].Cells["Department"].Value = item.DeptName;
                            GvProductInfo.Rows[i].Cells["Category"].Value = item.Category;
                            GvProductInfo.Rows[i].Cells["ColorName"].Value = item.ColorName;
                            GvProductInfo.Rows[i].Cells["BarcodePrice"].Value = item.barcodePrice;
                            if (mvat != 0)
                                mdvat = mdvat + Convert.ToDecimal(item.Vat);
                            i++;
                        }
                    }
                    btnsave.Enabled = true;
                    btnUpdate.Enabled = false;

                    GoodMasterRepository SaleRMRepo = new GoodMasterRepository();
                    var srmdata = SaleRMRepo.GetAll().Where(t => t.BillNo == Convert.ToString(billno) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (srmdata != null)
                    {
                        printId = srmdata.GoodReturnNo.ToString();
                        txtsrno.Text = srmdata.GoodReturnNo.ToString();
                        dtpsrdate.Value = srmdata.GoodReturnDate.Value;
                        txtTotalAmt.Text = srmdata.TotalAmount.ToString();
                        txtDiscount.Text = srmdata.Discount.ToString();
                        txtNetAmt.Text = srmdata.NetAmount.ToString();
                        var srddata = srmdata.GoodReturnDetails;
                        gvSaleReturnDetails.Rows.Clear();
                        int j = 0;
                        foreach (var item in srddata)
                        {
                            if (item.AssemblyChildStatus == "N" || item.AssemblyChildStatus == null || item.AssemblyChildStatus == "")
                            {
                                gvSaleReturnDetails.Rows.Add();
                                gvSaleReturnDetails.Rows[j].Cells["PRId"].Value = item.Id;
                                gvSaleReturnDetails.Rows[j].Cells["PRProductName"].Value = item.ProductId;
                                gvSaleReturnDetails.Rows[j].Cells["PRCompany"].Value = item.CompanyName;
                                gvSaleReturnDetails.Rows[j].Cells["PRUnit"].Value = item.Unit;
                                if (item.SaleUnit != null)
                                    gvSaleReturnDetails.Rows[j].Cells["PRSaleUnit"].Value = item.SaleUnit;
                                else
                                    gvSaleReturnDetails.Rows[j].Cells["PRSaleUnit"].Value = item.Unit;
                                if (item.Unit != item.SaleUnit && item.SaleUnit != null)
                                {
                                    ProductRepository productrepo = new ProductRepository();
                                    ProductInformation productdata = new ProductInformation();
                                    if (CommonMethod.commProduct == true)
                                        productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductId && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit).FirstOrDefault();
                                    else
                                        productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductId && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                    gvSaleReturnDetails.Rows[j].Cells["PRQuantity"].Value = Convert.ToDecimal(item.Quantity) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                }
                                else
                                    gvSaleReturnDetails.Rows[j].Cells["PRQuantity"].Value = item.Quantity;
                                gvSaleReturnDetails.Rows[j].Cells["PRCode"].Value = item.Code;
                                gvSaleReturnDetails.Rows[j].Cells["PRRate"].Value = item.Rate;
                                gvSaleReturnDetails.Rows[j].Cells["PRAmount"].Value = item.Amount;
                                gvSaleReturnDetails.Rows[j].Cells["PRDisc"].Value = item.Discount;
                                gvSaleReturnDetails.Rows[j].Cells["PRSpDisc"].Value = item.SpcDisc;
                                gvSaleReturnDetails.Rows[j].Cells["PRSGST"].Value = item.SGstPercent;
                                gvSaleReturnDetails.Rows[j].Cells["PRSGSTAmt"].Value = item.SGstAmt;
                                gvSaleReturnDetails.Rows[j].Cells["PRCGST"].Value = item.CGstPercent;
                                gvSaleReturnDetails.Rows[j].Cells["PRCGSTAmt"].Value = item.CGstAmt;
                                gvSaleReturnDetails.Rows[j].Cells["PRIGST"].Value = item.IGstPercent;
                                gvSaleReturnDetails.Rows[j].Cells["PRIGSTAmt"].Value = item.IGstAmt;
                                gvSaleReturnDetails.Rows[j].Cells["PRCess"].Value = item.Cess;
                                gvSaleReturnDetails.Rows[j].Cells["PRCessAmt"].Value = item.CessAmt;
                                gvSaleReturnDetails.Rows[j].Cells["PRSaleRate"].Value = item.SaleRate;
                                gvSaleReturnDetails.Rows[j].Cells["PRHSNCode"].Value = item.HSNCode;
                                gvSaleReturnDetails.Rows[j].Cells["PRTotalAmt"].Value = item.TotalAmount;
                                gvSaleReturnDetails.Rows[j].Cells["PRReason"].Value = item.Reason;
                                gvSaleReturnDetails.Rows[j].Cells["PRBNID"].Value = item.BNID;
                                gvSaleReturnDetails.Rows[j].Cells["PRRemove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                if (!string.IsNullOrEmpty(item.BatchNo))
                                    gvSaleReturnDetails.Rows[j].Cells["PRBatchNo"].Value = item.BatchNo;
                                else
                                    gvSaleReturnDetails.Rows[j].Cells["PRBatchNo"].Value = "";
                                if (!string.IsNullOrEmpty(Convert.ToString(item.Expiry)))
                                    gvSaleReturnDetails.Rows[j].Cells["PRExpiryDate"].Value = item.Expiry;
                                else
                                    gvSaleReturnDetails.Rows[j].Cells["PRExpiryDate"].Value = DateTime.Now.Date;
                                gvSaleReturnDetails.Rows[j].Cells["PRBarcode"].Value = item.Barcode;

                                gvSaleReturnDetails.Rows[j].Cells["PRSize"].Value = item.Size;
                                gvSaleReturnDetails.Rows[j].Cells["PRDepartment"].Value = item.DeptName;
                                gvSaleReturnDetails.Rows[j].Cells["PRCategory"].Value = item.Category;
                                gvSaleReturnDetails.Rows[j].Cells["PRColorName"].Value = item.ColorName;
                                gvSaleReturnDetails.Rows[j].Cells["PRBarcodePrice"].Value = item.barcodePrice;
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
                    MessageBox.Show("Fake Bill not Valid for return.", "Warning");
                    txtbillno.Text = "";
                    this.ActiveControl = txtbillno;
                }
                if (btnUpdate.Enabled == true)
                {
                    SettingRepository settingRepo = new SettingRepository();
                    Setting sdata = settingRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (sdata.DeleteSaleBill == "True")
                        btnDelete.Enabled = true;
                    else
                        btnDelete.Enabled = false;
                }
            }
            catch (Exception)
            { }
        }

        public void maxbillid()
        {
            try
            {
                GoodMasterRepository SaleRMRepo = new GoodMasterRepository();
                int mid = Convert.ToInt32(SaleRMRepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.GoodReturnNo));
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
            }
            catch (Exception)
            { }
        }

        private void frmCustomerBill_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                txtbillno.Text = "";
                bool userSetting = false;
                SettingRepository setrepo = new SettingRepository();
                Setting setData = setrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (setData != null)
                {
                    if (setData.ShowUserDefinedSeries == "Y")
                        userSetting = true;
                }
                setrepo.Dispose();
                if (userSetting == false)
                {
                    string cnString2 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
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
                    string cnString2 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(cnString2))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT Userseries FROM CustomerBillMaster where TransactionYear = '" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " and (userseries is not null or userseries != '')", con);
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
                bool isValid = true;
                decimal gridqty = 0;
                if (cmbcustomername.Text != "Select" && txtreason.Text != "")
                {
                    if (productentrytype == 1 || productentrytype == 2)
                    {
                        if (cmbBatchNo.Text == "")
                        {
                            MessageBox.Show("Select BatchNo.", "Warning");
                            isValid = false;
                            this.ActiveControl = cmbBatchNo;
                        }
                    }
                    string batch = "";
                    if (txtProductname.Text != "" && txtQuantity.Text != "" && isValid == true)
                    {
                        int i = gvSaleReturnDetails.RowCount;
                        string proname = txtProductname.Text.Trim().Split(',')[0];
                        string company = txtProductname.Text.Trim().Split(',')[1];
                        string unit = txtProductname.Text.Trim().Split(',')[2];
                        if (productentrytype == 1 || productentrytype == 2)
                            batch = cmbBatchNo.Text;
                        int sbid = Convert.ToInt32(GvProductInfo.Rows[sbcolIndex].Cells["Id"].Value);
                        string size = Convert.ToString(GvProductInfo.Rows[sbcolIndex].Cells["Sizes"].Value);
                        string newReason = txtreason.Text;
                        decimal txtqty = 0;
                        decimal remqty = 0, sqty = 0, srqty = 0;
                        CustomerBillMasterRepository custmastrepo = new CustomerBillMasterRepository();
                        var custbillmastdata = custmastrepo.GetAll().Where(t => (t.BillNo == txtbillno.Text || t.UserSeries == txtbillno.Text) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                        CustomerBillDetailRepository Custdetailrepo = new CustomerBillDetailRepository();
                        int Cust_BillID = custbillmastdata.Id;
                        cust_BillNo = custbillmastdata.BillNo;
                        DataTable dtbill = db.GetTable("Select cd.* from  CustomerBillDetail as cd inner join CustomerBillMaster as cm on cm.Id=cd.BillNo where cd.Id=" + sbid + " and cm.CompId=" + CommonMethod.CompId + " and cm.TransactionYear='" + CommonMethod.TransactionYear + "' and cd.ProductId = '" + proname + "' and cd.CompanyName = '" + company + "' and cd.Unit = '" + unit + "' and cd.Size='" + size + "'");
                        if (dtbill.Rows.Count > 0)
                        {
                            GoodDetailRepository SaleRDRepo = new GoodDetailRepository();
                            GoodReturnDetail SRDetail = new GoodReturnDetail();
                            CustomerBillDetail SBDetail = new CustomerBillDetail();
                            PReturnDetailRepository PRDRepo = new PReturnDetailRepository();
                            if (productentrytype == 1 || productentrytype == 2)
                            {
                                sqty = uqty;  //24032018
                                var srddata = SaleRDRepo.GetAll().Where(t => t.ProductId == proname && t.CompanyName == company && t.Unit == unit && t.Size == size && t.GoodReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.GoodReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (srddata != null)
                                    srqty = Convert.ToDecimal(SaleRDRepo.GetAll().Where(t => t.ProductId == proname && t.BatchNo == batch && t.CompanyName == company && t.Unit == unit && t.Size == size && t.GoodReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.GoodReturnMaster.CompId == CommonMethod.CompId).Sum(t => t.Quantity));
                                remqty = sqty - srqty;
                            }
                            else
                            {
                                var custdtldata = Custdetailrepo.GetAll().Where(t => t.ProductId == proname && t.CompanyName == company && t.Unit == unit && t.Size == size && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear && t.CustomerBillMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (custdtldata != null)
                                    sqty = Convert.ToDecimal(Custdetailrepo.GetAll().Where(t => t.ProductId == proname && t.CompanyName == company && t.Unit == unit && t.Size == size && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear && t.CustomerBillMaster.CompId == CommonMethod.CompId).Sum(t => t.Quantity));
                                var srddata = SaleRDRepo.GetAll().Where(t => t.ProductId == proname && t.CompanyName == company && t.Unit == unit && t.Size == size && t.GoodReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.GoodReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (srddata != null)
                                    srqty = Convert.ToDecimal(SaleRDRepo.GetAll().Where(t => t.ProductId == proname && t.CompanyName == company && t.Unit == unit && t.Size == size && t.GoodReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.GoodReturnMaster.CompId == CommonMethod.CompId).Sum(t => t.Quantity));
                                remqty = sqty - srqty;
                            }
                            try
                            {
                                for (int k = 0; k < gvSaleReturnDetails.Rows.Count; k++)
                                {
                                    string pname1 = gvSaleReturnDetails.Rows[k].Cells["PRProductName"].Value.ToString();
                                    string compname = gvSaleReturnDetails.Rows[k].Cells["PRCompany"].Value.ToString();
                                    string unitname = gvSaleReturnDetails.Rows[k].Cells["PRUnit"].Value.ToString();
                                    int srsbid = Convert.ToInt32(gvSaleReturnDetails.Rows[k].Cells["PRBNID"].Value);
                                    string returnreason = gvSaleReturnDetails.Rows[k].Cells["PRReason"].Value.ToString();
                                    if (proname == pname1 && company == compname && unit == unitname && sbid == srsbid && returnreason.Trim() == newReason.Trim())
                                    {
                                        if (unit != GvProductInfo.Rows[sbcolIndex].Cells["SaleUnit"].Value.ToString())
                                        {
                                            ProductRepository productrepo = new ProductRepository();
                                            ProductInformation productdata = new ProductInformation();
                                            if (CommonMethod.commProduct == true)
                                                productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                                            else
                                                productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                            txtqty = Convert.ToDecimal(txtQuantity.Text) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                        }
                                        else
                                            txtqty = Convert.ToDecimal(txtQuantity.Text);
                                        gridqty = Convert.ToDecimal(gvSaleReturnDetails.Rows[k].Cells["PRQuantity"].Value) + Convert.ToDecimal(txtQuantity.Text);
                                        if (remqty >= txtqty && Convert.ToDecimal(GvProductInfo.Rows[sbcolIndex].Cells["Quantityg"].Value) >= gridqty)
                                        {
                                            gvSaleReturnDetails.Rows[k].Cells["PRProductName"].Value = proname;
                                            gvSaleReturnDetails.Rows[k].Cells["PRCompany"].Value = company;
                                            gvSaleReturnDetails.Rows[k].Cells["PRUnit"].Value = unit;
                                            gvSaleReturnDetails.Rows[k].Cells["PRSaleUnit"].Value = GvProductInfo.Rows[sbcolIndex].Cells["SaleUnit"].Value.ToString();
                                            gvSaleReturnDetails.Rows[k].Cells["PRQuantity"].Value = gridqty;
                                            gvSaleReturnDetails.Rows[k].Cells["PRCode"].Value = GvProductInfo.Rows[sbcolIndex].Cells["Code"].Value.ToString();
                                            gvSaleReturnDetails.Rows[k].Cells["PRRate"].Value = txtrate.Text;
                                            gvSaleReturnDetails.Rows[k].Cells["PRAmount"].Value = gridqty * Convert.ToDecimal(txtAmount.Text);

                                            decimal amt = Convert.ToDecimal(gridqty * Convert.ToDecimal(txtAmount.Text));
                                            decimal sgstamt = 0, cgstamt = 0, igstamt = 0, cessamt = 0, discamt = 0, spcdicamt = 0, totaldiscamt = 0;

                                            if (txtpdiscount.Text != "")
                                            {
                                                gvSaleReturnDetails.Rows[k].Cells["PRDisc"].Value = txtpdiscount.Text;
                                                discamt = amt * Convert.ToDecimal(txtpdiscount.Text) / 100;
                                                totaldiscamt = amt - discamt;
                                            }
                                            else
                                                gvSaleReturnDetails.Rows[k].Cells["PRDisc"].Value = 0;
                                            if (txtpSpcDisc.Text != "")
                                            {
                                                gvSaleReturnDetails.Rows[k].Cells["PRSpDisc"].Value = txtpSpcDisc.Text;
                                                spcdicamt = (totaldiscamt) * Convert.ToDecimal(txtpSpcDisc.Text) / 100;
                                                totaldiscamt = (totaldiscamt - spcdicamt);
                                            }
                                            else
                                                gvSaleReturnDetails.Rows[k].Cells["PRSpDisc"].Value = 0;
                                            if (cmbsgst.Text != "")
                                            {
                                                gvSaleReturnDetails.Rows[k].Cells["PRSGST"].Value = cmbsgst.Text;
                                                sgstamt = totaldiscamt * Convert.ToDecimal(cmbsgst.Text) / 100;
                                                gvSaleReturnDetails.Rows[k].Cells["PRSGSTAmt"].Value = sgstamt;
                                            }
                                            else
                                            {
                                                gvSaleReturnDetails.Rows[k].Cells["PRSGST"].Value = 0;
                                                gvSaleReturnDetails.Rows[k].Cells["PRSGSTAmt"].Value = 0;
                                            }
                                            if (cmbcgst.Text != "")
                                            {
                                                gvSaleReturnDetails.Rows[k].Cells["PRCGST"].Value = cmbcgst.Text;
                                                cgstamt = totaldiscamt * Convert.ToDecimal(cmbcgst.Text) / 100;
                                                gvSaleReturnDetails.Rows[k].Cells["PRCGSTAmt"].Value = cgstamt;
                                            }
                                            else
                                            {
                                                gvSaleReturnDetails.Rows[k].Cells["PRCGST"].Value = 0;
                                                gvSaleReturnDetails.Rows[k].Cells["PRCGSTAmt"].Value = 0;
                                            }
                                            if (cmbigst.Text != "")
                                            {
                                                gvSaleReturnDetails.Rows[k].Cells["PRIGST"].Value = cmbigst.Text;
                                                igstamt = totaldiscamt * Convert.ToDecimal(cmbigst.Text) / 100;
                                                gvSaleReturnDetails.Rows[k].Cells["PRIGSTAmt"].Value = igstamt;
                                            }
                                            else
                                            {
                                                gvSaleReturnDetails.Rows[k].Cells["PRIGST"].Value = 0;
                                                gvSaleReturnDetails.Rows[k].Cells["PRIGSTAmt"].Value = 0;
                                            }
                                            if (txtpCess.Text != "")
                                            {
                                                gvSaleReturnDetails.Rows[k].Cells["PRCess"].Value = txtpCess.Text;
                                                cessamt = totaldiscamt * Convert.ToDecimal(txtpCess.Text) / 100;
                                                gvSaleReturnDetails.Rows[k].Cells["PRCessAmt"].Value = cessamt;
                                            }
                                            else
                                            {
                                                gvSaleReturnDetails.Rows[k].Cells["PRCess"].Value = 0;
                                                gvSaleReturnDetails.Rows[k].Cells["PRCessAmt"].Value = 0;
                                            }
                                            gvSaleReturnDetails.Rows[k].Cells["PRSaleRate"].Value = GvProductInfo.Rows[sbcolIndex].Cells["SaleRate"].Value;
                                            gvSaleReturnDetails.Rows[k].Cells["PRHSNCode"].Value = txtHSNCode.Text;
                                            gvSaleReturnDetails.Rows[k].Cells["PRTotalAmt"].Value = Convert.ToDecimal(gvSaleReturnDetails.Rows[k].Cells["PRTotalAmt"].Value) + Convert.ToDecimal(txtpTotalAmt.Text);
                                            gvSaleReturnDetails.Rows[k].Cells["PRReason"].Value = txtreason.Text;
                                            gvSaleReturnDetails.Rows[k].Cells["PRBNID"].Value = sbid.ToString();
                                            gvSaleReturnDetails.Rows[k].Cells["PRRemove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                            gvSaleReturnDetails.Rows[k].Cells["PRBatchNo"].Value = cmbBatchNo.Text;
                                            gvSaleReturnDetails.Rows[k].Cells["PRExpiryDate"].Value = txtExpiry.Text;
                                            gvSaleReturnDetails.Rows[k].Cells["PRBarcode"].Value = GvProductInfo.Rows[sbcolIndex].Cells["Barcode"].Value;
                                            gvSaleReturnDetails.Rows[k].Cells["PRSize"].Value = GvProductInfo.Rows[sbcolIndex].Cells["Sizes"].Value;
                                            gvSaleReturnDetails.Rows[k].Cells["PRDepartment"].Value = GvProductInfo.Rows[sbcolIndex].Cells["Department"].Value;
                                            gvSaleReturnDetails.Rows[k].Cells["PRCategory"].Value = GvProductInfo.Rows[sbcolIndex].Cells["Category"].Value;
                                            gvSaleReturnDetails.Rows[k].Cells["PRColorName"].Value = GvProductInfo.Rows[sbcolIndex].Cells["ColorName"].Value;
                                            gvSaleReturnDetails.Rows[k].Cells["PRBarcodePrice"].Value = GvProductInfo.Rows[sbcolIndex].Cells["BarcodePrice"].Value;
                                            goto s;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Quantity must be less or equal to sale quantity.", "Warning");
                                            goto s;
                                        }
                                    }
                                }

                                if (unit != GvProductInfo.Rows[sbcolIndex].Cells["SaleUnit"].Value.ToString())
                                {
                                    ProductRepository productrepo = new ProductRepository();
                                    ProductInformation productdata = new ProductInformation();
                                    if (CommonMethod.commProduct == true)
                                        productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                                    else
                                        productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                    txtqty = Convert.ToDecimal(txtQuantity.Text) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                }
                                else
                                    txtqty = Convert.ToDecimal(txtQuantity.Text);
                                if (remqty >= txtqty && Convert.ToDecimal(GvProductInfo.Rows[sbcolIndex].Cells["Quantityg"].Value) >= Convert.ToDecimal(txtQuantity.Text))
                                {
                                    gvSaleReturnDetails.Rows.Add();
                                    gvSaleReturnDetails.Rows[i].Cells["PRProductName"].Value = proname;
                                    gvSaleReturnDetails.Rows[i].Cells["PRCompany"].Value = company;
                                    gvSaleReturnDetails.Rows[i].Cells["PRUnit"].Value = unit;
                                    gvSaleReturnDetails.Rows[i].Cells["PRSaleUnit"].Value = GvProductInfo.Rows[sbcolIndex].Cells["SaleUnit"].Value.ToString();
                                    gvSaleReturnDetails.Rows[i].Cells["PRQuantity"].Value = Convert.ToDecimal(txtQuantity.Text);
                                    gvSaleReturnDetails.Rows[i].Cells["PRCode"].Value = GvProductInfo.Rows[sbcolIndex].Cells["Code"].Value.ToString();
                                    gvSaleReturnDetails.Rows[i].Cells["PRRate"].Value = txtrate.Text;
                                    gvSaleReturnDetails.Rows[i].Cells["PRAmount"].Value = Convert.ToDecimal(txtAmount.Text);
                                    decimal amt = Convert.ToDecimal(txtrate.Text) * Convert.ToDecimal(txtQuantity.Text);
                                    decimal sgstamt = 0, cgstamt = 0, igstamt = 0, cessamt = 0, discamt = 0, spcdicamt = 0, totaldiscamt = 0;
                                    if (txtpdiscount.Text != "")
                                    {
                                        gvSaleReturnDetails.Rows[i].Cells["PRDisc"].Value = txtpdiscount.Text;
                                        discamt = amt * Convert.ToDecimal(txtpdiscount.Text) / 100;
                                        totaldiscamt = (amt - discamt);
                                    }
                                    else
                                        gvSaleReturnDetails.Rows[i].Cells["PRDisc"].Value = 0;
                                    if (txtpSpcDisc.Text != "")
                                    {
                                        gvSaleReturnDetails.Rows[i].Cells["PRSpDisc"].Value = txtpSpcDisc.Text;
                                        spcdicamt = (totaldiscamt) * Convert.ToDecimal(txtpSpcDisc.Text) / 100;
                                        totaldiscamt = (totaldiscamt - spcdicamt);
                                    }
                                    else
                                        gvSaleReturnDetails.Rows[i].Cells["PRSpDisc"].Value = 0;
                                    if (cmbsgst.Text != "")
                                    {
                                        gvSaleReturnDetails.Rows[i].Cells["PRSGST"].Value = cmbsgst.Text;
                                        sgstamt = (totaldiscamt) * Convert.ToDecimal(cmbsgst.Text) / 100;
                                        gvSaleReturnDetails.Rows[i].Cells["PRSGSTAmt"].Value = sgstamt;
                                    }
                                    else
                                    {
                                        gvSaleReturnDetails.Rows[i].Cells["PRSGST"].Value = 0;
                                        gvSaleReturnDetails.Rows[i].Cells["PRSGSTAmt"].Value = 0;
                                    }
                                    if (cmbcgst.Text != "")
                                    {
                                        gvSaleReturnDetails.Rows[i].Cells["PRCGST"].Value = cmbcgst.Text;
                                        cgstamt = totaldiscamt * Convert.ToDecimal(cmbcgst.Text) / 100;
                                        gvSaleReturnDetails.Rows[i].Cells["PRCGSTAmt"].Value = cgstamt;
                                    }
                                    else
                                    {
                                        gvSaleReturnDetails.Rows[i].Cells["PRCGST"].Value = 0;
                                        gvSaleReturnDetails.Rows[i].Cells["PRCGSTAmt"].Value = 0;
                                    }
                                    if (cmbigst.Text != "")
                                    {
                                        gvSaleReturnDetails.Rows[i].Cells["PRIGST"].Value = cmbigst.Text;
                                        igstamt = totaldiscamt * Convert.ToDecimal(cmbigst.Text) / 100;
                                        gvSaleReturnDetails.Rows[i].Cells["PRIGSTAmt"].Value = igstamt;
                                    }
                                    else
                                    {
                                        gvSaleReturnDetails.Rows[i].Cells["PRIGST"].Value = 0;
                                        gvSaleReturnDetails.Rows[i].Cells["PRIGSTAmt"].Value = 0;
                                    }
                                    if (txtpCess.Text != "")
                                    {
                                        gvSaleReturnDetails.Rows[i].Cells["PRCess"].Value = txtpCess.Text;
                                        cessamt = totaldiscamt * Convert.ToDecimal(txtpCess.Text) / 100;
                                        gvSaleReturnDetails.Rows[i].Cells["PRCessAmt"].Value = cessamt;
                                    }
                                    else
                                    {
                                        gvSaleReturnDetails.Rows[i].Cells["PRCess"].Value = 0;
                                        gvSaleReturnDetails.Rows[i].Cells["PRCessAmt"].Value = 0;
                                    }
                                    gvSaleReturnDetails.Rows[i].Cells["PRSaleRate"].Value = GvProductInfo.Rows[sbcolIndex].Cells["SaleRate"].Value;
                                    gvSaleReturnDetails.Rows[i].Cells["PRHSNCode"].Value = txtHSNCode.Text;
                                    gvSaleReturnDetails.Rows[i].Cells["PRTotalAmt"].Value = txtpTotalAmt.Text;
                                    gvSaleReturnDetails.Rows[i].Cells["PRReason"].Value = txtreason.Text;
                                    gvSaleReturnDetails.Rows[i].Cells["PRBNID"].Value = sbid.ToString();
                                    gvSaleReturnDetails.Rows[i].Cells["PRRemove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                    gvSaleReturnDetails.Rows[i].Cells["PRBatchNo"].Value = cmbBatchNo.Text;
                                    gvSaleReturnDetails.Rows[i].Cells["PRExpiryDate"].Value = txtExpiry.Text;
                                    gvSaleReturnDetails.Rows[i].Cells["PRBarcode"].Value = GvProductInfo.Rows[sbcolIndex].Cells["Barcode"].Value;
                                    gvSaleReturnDetails.Rows[i].Cells["PRSize"].Value = GvProductInfo.Rows[sbcolIndex].Cells["Sizes"].Value;
                                    gvSaleReturnDetails.Rows[i].Cells["PRDepartment"].Value = GvProductInfo.Rows[sbcolIndex].Cells["Department"].Value;
                                    gvSaleReturnDetails.Rows[i].Cells["PRCategory"].Value = GvProductInfo.Rows[sbcolIndex].Cells["Category"].Value;
                                    gvSaleReturnDetails.Rows[i].Cells["PRColorName"].Value = GvProductInfo.Rows[sbcolIndex].Cells["ColorName"].Value;
                                    gvSaleReturnDetails.Rows[i].Cells["PRBarcodePrice"].Value = GvProductInfo.Rows[sbcolIndex].Cells["BarcodePrice"].Value;
                                }
                            s:
                                {
                                    totalamt = 0;
                                    totalamt = gvSaleReturnDetails.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["PRTotalAmt"].Value));
                                    txtTotalAmt.Text = Convert.ToString(Math.Round(totalamt, 2, MidpointRounding.AwayFromZero));
                                    txtProductname.Text = "";
                                    cmbUnit.Items.Clear();
                                    txtQuantity.Text = "";
                                    txtrate.Text = "";
                                    txtAmount.Text = "";
                                    txtHSNCode.Text = "";
                                    txtpdiscount.Text = "0";
                                    txtpSpcDisc.Text = "0";
                                    fillgst();
                                    txtpCess.Text = "0";
                                    lblBasicRate.Text = "0";
                                    txtTaxableamt.Text = "0";
                                    txtpTotalAmt.Text = "0";
                                    totalamt = 0;
                                    txtreason.Text = "";
                                    sbcolIndex = -1;
                                    cmbBatchNo.Text = "";
                                    txtExpiry.Text = "";
                                }
                            }
                            catch (Exception)
                            { }
                            SaleRDRepo.Dispose();
                            PRDRepo.Dispose();
                        }
                        Custdetailrepo.Dispose();
                    }
                    else
                    {
                        if (txtProductname.Text == "")
                        {
                            MessageBox.Show("Enter product Name.", "Warning");
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
                    if (cmbcustomername.Text == "Select")
                    {
                        MessageBox.Show("Enter Customer name.", "Warning");
                        this.ActiveControl = cmbcustomername;
                    }
                    else
                    {
                        MessageBox.Show("Select Reason.", "Warning");
                        this.ActiveControl = txtreason;
                    }
                }
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            mvat = 0; mdvat = 0;
            maxbillid();
            txtbillno.Text = "";
            customerdata();
            txtSBPaidAmt.Enabled = false;
            btnsave.Enabled = true;
            btnUpdate.Enabled = false;
            dgvqtydetails.Visible = false;
            txtProductname.Text = "";
            cmbUnit.Items.Clear();
            txtSBtotalamt.Text = "0";
            txtSBDisc.Text = "0";
            txtSBTransChrg.Text = "0";
            txtSBHamliChrgs.Text = "0";
            txtSBNetAmt.Text = "0";
            txtSBPaidAmt.Text = "0";
            txtSBRemAmt.Text = "0";
            txtQuantity.Text = "0";
            txtrate.Text = "0";
            txtAmount.Text = "0";
            txtpdiscount.Text = "0";
            txtpSpcDisc.Text = "0";
            fillgst();
            txtpCess.Text = "0";
            txtpTotalAmt.Text = "0";
            GvProductInfo.Rows.Clear();
            gvSaleReturnDetails.Rows.Clear();
            txtTotalAmt.Text = "0";
            txtDiscount.Text = "0";
            txtNetAmt.Text = "0";
            lblBasicRate.Text = "0";
            chkexchange.CheckState = CheckState.Unchecked;
            colIndex = -1;
            sbcolIndex = -1;
            CustDPredata.Clear();
            pnlIntegratedDtls.Visible = false;
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
                GoodMasterRepository SaleRMRepo = new GoodMasterRepository();
                GoodReturnMaster srmasterdata = new GoodReturnMaster();
                maxbillid();
                srmasterdata.GoodReturnNo = Convert.ToInt32(txtsrno.Text);
                srmasterdata.GoodReturnDate = dtpsrdate.Value;
                srmasterdata.BillNo = cust_BillNo;
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

                if (txtDiscount.Text != "")
                    srmasterdata.Discount = Convert.ToDecimal(txtDiscount.Text);
                else
                    srmasterdata.Discount = 0;
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
                SaleRMRepo.Add(srmasterdata);
                SaleRMRepo.Save();
                printId = Convert.ToString(srmasterdata.GoodReturnNo);
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
                GoodDetailRepository SaleRDRepo = new GoodDetailRepository();
                GoodReturnDetail srdetaildata = new GoodReturnDetail();

                srdetaildata.GoodReturnNo = custmasterid;
                srdetaildata.ProductId = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRProductName"].Value);
                srdetaildata.CompanyName = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRCompany"].Value);
                srdetaildata.Unit = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRUnit"].Value);
                srdetaildata.SaleUnit = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRSaleUnit"].Value);
                if (Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRUnit"].Value) != Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRSaleUnit"].Value))
                {
                    ProductRepository productrepo = new ProductRepository();
                    ProductInformation productdata = new ProductInformation();
                    if (CommonMethod.commProduct == true)
                        productdata = productrepo.GetAll().Where(t => t.ProductName == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRProductName"].Value) && t.ManufactureCompany == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRCompany"].Value) && t.Uniti == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRUnit"].Value)).FirstOrDefault();
                    else
                        productdata = productrepo.GetAll().Where(t => t.ProductName == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRProductName"].Value) && t.ManufactureCompany == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRCompany"].Value) && t.Uniti == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRUnit"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    srdetaildata.Quantity = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRQuantity"].Value) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                }
                else
                    srdetaildata.Quantity = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRQuantity"].Value);
                srdetaildata.Code = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRCode"].Value);
                srdetaildata.Rate = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRRate"].Value);
                srdetaildata.Amount = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRAmount"].Value);
                srdetaildata.Discount = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRDisc"].Value);
                srdetaildata.SpcDisc = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRSpDisc"].Value);
                srdetaildata.SGstPercent = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRSGST"].Value);
                srdetaildata.SGstAmt = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRSGSTAmt"].Value);
                srdetaildata.CGstPercent = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRCGST"].Value);
                srdetaildata.CGstAmt = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRCGSTAmt"].Value);
                srdetaildata.IGstPercent = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRIGST"].Value);
                srdetaildata.IGstAmt = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRIGSTAmt"].Value);
                srdetaildata.Cess = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRCess"].Value);
                srdetaildata.CessAmt = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRCessAmt"].Value);
                srdetaildata.SaleRate = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRSaleRate"].Value);
                srdetaildata.HSNCode = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRHSNCode"].Value);
                srdetaildata.TotalAmount = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRTotalAmt"].Value);
                srdetaildata.Reason = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRReason"].Value);
                srdetaildata.BNID = Convert.ToInt32(gvSaleReturnDetails.Rows[i].Cells["PRBNID"].Value);
                srdetaildata.BatchNo = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRBatchNo"].Value);
                srdetaildata.Expiry = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRExpiryDate"].Value);
                srdetaildata.Barcode = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRBarcode"].Value);
                srdetaildata.Size = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRSize"].Value);
                srdetaildata.DeptName = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRDepartment"].Value);
                srdetaildata.Category = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRCategory"].Value);
                srdetaildata.ColorName = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRColorName"].Value);
                srdetaildata.barcodePrice = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRBarcodePrice"].Value);
                srdetaildata.AssemblyChildStatus = "N";
                SaleRDRepo.Add(srdetaildata);
                SaleRDRepo.Save();
                decimal assemblyQty = Convert.ToDecimal(srdetaildata.Quantity);
                int BDetailId = Convert.ToInt32(srdetaildata.BNID);
                string Reason = Convert.ToString(srdetaildata.Reason);
                decimal SGSTPercent = 0, CGSTPercent = 0, IGSTPercent = 0;
                SGSTPercent = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRSGST"].Value);
                CGSTPercent = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRCGST"].Value);
                IGSTPercent = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRIGST"].Value);
                SaleRDRepo.Dispose();
                try
                {
                    ProductRepository productRepo = new ProductRepository();
                    var pData = productRepo.GetAll().Where(t => t.ProductName == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRProductName"].Value) && t.ManufactureCompany == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRCompany"].Value) && t.Uniti == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRUnit"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (pData != null)
                    {
                        AssemeblyProductDetailsRepository assemblyDRepo = new AssemeblyProductDetailsRepository();
                        List<tblAssemblyProductDetail> assemblyDList = assemblyDRepo.GetAll().Where(t => t.AssemblyProductId == pData.ID && t.CompId == CommonMethod.CompId).ToList();
                        if (assemblyDList.Count > 0)
                        {
                            foreach (var itemAssembly in assemblyDList)
                            {
                                GoodDetailRepository SaleRDRepo1 = new GoodDetailRepository();
                                GoodReturnDetail srdetaildata1 = new GoodReturnDetail();

                                srdetaildata1.GoodReturnNo = custmasterid;
                                srdetaildata1.ProductId = Convert.ToString(itemAssembly.ProductName);
                                srdetaildata1.CompanyName = Convert.ToString(itemAssembly.CompanyName);
                                srdetaildata1.Unit = Convert.ToString(itemAssembly.Unit);
                                srdetaildata1.SaleUnit = Convert.ToString(itemAssembly.Unit);
                                decimal childQty = 0, calculateChildQty = 0;
                                childQty = Convert.ToDecimal(itemAssembly.Qty);
                                calculateChildQty = childQty * assemblyQty;
                                srdetaildata1.Quantity = Convert.ToDecimal(calculateChildQty);
                                srdetaildata1.Code = "";
                                srdetaildata1.Rate = Convert.ToDecimal(0);
                                srdetaildata1.Amount = Convert.ToDecimal(0);
                                srdetaildata1.Discount = Convert.ToDecimal(0);
                                srdetaildata1.SpcDisc = Convert.ToDecimal(0);
                                srdetaildata1.SGstPercent = Convert.ToDecimal(SGSTPercent);
                                srdetaildata1.SGstAmt = Convert.ToDecimal(0);
                                srdetaildata1.CGstPercent = Convert.ToDecimal(CGSTPercent);
                                srdetaildata1.CGstAmt = Convert.ToDecimal(0);
                                srdetaildata1.IGstPercent = Convert.ToDecimal(IGSTPercent);
                                srdetaildata1.IGstAmt = Convert.ToDecimal(0);
                                srdetaildata1.Cess = Convert.ToDecimal(0);
                                srdetaildata1.CessAmt = Convert.ToDecimal(0);
                                srdetaildata1.SaleRate = Convert.ToDecimal(0);
                                srdetaildata1.HSNCode = "";
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
                                srdetaildata1.AssemblyChildStatus = "Y";
                                srdetaildata1.BatchNo = "";
                                srdetaildata1.Expiry = "";
                                srdetaildata1.Barcode = "";
                                SaleRDRepo1.Add(srdetaildata1);
                                SaleRDRepo1.Save();
                                SaleRDRepo1.Dispose();
                            }
                        }
                    }
                }
                catch (Exception)
                { }
                try
                {
                    BatchRepository BatchRepo = new BatchRepository();
                    Batch BatchData = new Batch();
                    BatchData = BatchRepo.GetAll().Where(t => t.BillId == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRBNID"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (BatchData != null)
                    {
                        PurchaseDetail pData = new PurchaseDetail();
                        PurchaseMasterRepository pMastRepo = new PurchaseMasterRepository();
                        PurchaseDetail purdetail11 = new PurchaseDetail();
                        PurchaseMaster PMasterData1 = new PurchaseMaster();
                        PurchaseDetailRepository ppRepo = new PurchaseDetailRepository();
                        PMasterData1 = pMastRepo.GetAll().Where(t => t.Id == BatchData.PorderNo && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (PMasterData1 != null)
                            purdetail11 = ppRepo.GetAll().Where(t => t.ProductName == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRProductName"].Value) && t.CompanyName == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRCompany"].Value) && t.Unit == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRUnit"].Value) && t.BatchNo == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRBatchNo"].Value) && t.PorderNo == BatchData.PorderNo && t.PurchaseMaster.TransactionYear == PMasterData1.TransactionYear && t.PurchaseMaster.CompId == PMasterData1.CompId).FirstOrDefault();
                        pData = ppRepo.GetById(purdetail11.Id);
                        pData.Status = "Active";
                        ppRepo.Edit(pData);
                        ppRepo.Save();
                    }
                }
                catch (Exception)
                { }
            }
            catch (Exception)
            { }
        }

        public void savestock(int i)
        {
            try
            {
                AIOInventorySystem.Data.Model.Stock updateSData = new Data.Model.Stock();
                StockRepository stockRepo = new StockRepository();
                StockRepository updateStockRepo = new StockRepository();
                ProductRepository productrepo = new ProductRepository();
                CustomerBillDetailRepository Custdetailrepo = new CustomerBillDetailRepository();
                GoodDetailRepository SaleRDRepo = new GoodDetailRepository();
                PReturnDetailRepository PRDRepo = new PReturnDetailRepository();

                string pname = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRProductName"].Value);
                string company = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRCompany"].Value);
                string unit = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRUnit"].Value);
                string size = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRSize"].Value);
                ProductInformation productData = new ProductInformation();
                if (CommonMethod.commProduct == true)
                    productData = productrepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                else
                    productData = productrepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (productData.ProductType == 2)
                {
                    try
                    {
                        db.connect();
                        SqlCommand command = new SqlCommand("SPSaveUpdateSalePurchaseReturnStock", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@status", SqlDbType.VarChar).Value = "SR";
                        command.Parameters.Add("@proname", SqlDbType.VarChar).Value = pname;
                        command.Parameters.Add("@cname", SqlDbType.VarChar).Value = company;
                        command.Parameters.Add("@uname", SqlDbType.VarChar).Value = unit;
                        command.Parameters.Add("@Qty", SqlDbType.Decimal).Value = 0;
                        command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        command.Parameters.Add("@event", SqlDbType.VarChar).Value = "Save";
                        command.ExecuteNonQuery();
                        db.CloseConnection();
                    }
                    catch (Exception)
                    {
                        db.CloseConnection();
                    }
                }
                else
                {
                    var stockData = stockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.Size == size && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (stockData != null)
                    {
                        var updateStockData = stockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.Size == size && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        int updateID = updateStockData.Id; ;
                        decimal srQty = 0, ramQty = 0, rate = 0, oqty = 0;
                        try
                        {
                            try
                            {
                                oqty = Convert.ToDecimal(updateStockData.OpeningQty);
                            }
                            catch (Exception)
                            {
                                oqty = 0;
                            }
                            srQty = Convert.ToDecimal(SaleRDRepo.GetAll().Where(t => t.ProductId == pname && t.CompanyName == company && t.Unit == unit && t.Size == size && t.GoodReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.GoodReturnMaster.CompId == CommonMethod.CompId).Sum(t => t.Quantity));
                            ramQty = oqty + Convert.ToDecimal(updateStockData.PurchaseQty) - Convert.ToDecimal(updateStockData.PurReturnQty) - Convert.ToDecimal(updateStockData.SaleQty) + srQty - Convert.ToDecimal(updateStockData.DeadQty);
                        }
                        catch (Exception)
                        { }
                        updateSData.ProductId = productData.ProductCode;
                        updateSData.ProductName = pname;
                        updateSData.MfgCompany = company;
                        updateSData.Unit = unit;
                        updateSData.OpeningQty = oqty;
                        updateSData.PurchaseQty = Convert.ToDecimal(updateStockData.PurchaseQty);
                        updateSData.SaleQty = Convert.ToDecimal(updateStockData.SaleQty);
                        updateSData.DeadQty = updateStockData.DeadQty;
                        updateSData.RemQty = ramQty;
                        updateSData.Size = size;
                        SettingRepository settRepo = new SettingRepository();
                        Setting settData = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();

                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand();
                            if (settData.StockOn == "Purchase Rate")
                                command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
                            else if (settData.StockOn == "Sale Rate")
                                command = new SqlCommand("SPNewAvgSaleRate", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = pname;
                            command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = company;
                            command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = unit;
                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            SqlDataReader reader = command.ExecuteReader();
                            DataTable dtRate = new DataTable();
                            dtRate.Load(reader);
                            rate = Convert.ToDecimal(dtRate.Rows[0][0].ToString());
                            db.CloseConnection();
                            dtRate.Dispose();
                        }
                        catch (Exception)
                        { db.CloseConnection(); }
                        updateSData.ProductRate = rate;
                        updateSData.SaleReturnQty = srQty;
                        updateSData.PurReturnQty = Convert.ToDecimal(updateStockData.PurReturnQty);
                        updateSData.TransactionYear = CommonMethod.TransactionYear;
                        updateSData.CompId = CommonMethod.CompId;
                        updateSData.Id = updateID;
                        updateStockRepo.Edit(updateSData);
                        updateStockRepo.Save();
                    }
                }
                stockRepo.Dispose();
                updateStockRepo.Dispose();
                productrepo.Dispose();
                Custdetailrepo.Dispose();
                SaleRDRepo.Dispose();
                PRDRepo.Dispose();
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
                remainingdata.NetAmt = 0;
                remainingdata.PaidAmt = Convert.ToDecimal(txtNetAmt.Text);
                RemainingPaymentRepository remainingpayrepo1 = new RemainingPaymentRepository();
                RemainingPayment remainingdata1 = new RemainingPayment();
                remainingdata1 = remainingpayrepo1.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();

                if (remainingdata1 != null)
                {
                    decimal decremamt = 0;
                    decremamt = Convert.ToDecimal(remainingdata1.RemainingAmt);
                    remainingdata.RemainingAmt = decremamt - Convert.ToDecimal(txtNetAmt.Text);
                }
                else
                    remainingdata.RemainingAmt = 0;
                remainingdata.CreateDate = Convert.ToDateTime(dtpsrdate.Value);
                remainingdata.PaymentDate = Convert.ToDateTime(dtppaymentdate.Value);
                remainingdata.PaymentMode = mode;
                remainingdata.Type = "BillReturn";
                CustomerBillMasterRepository Custmasterrepo = new CustomerBillMasterRepository();
                var custmasterData = Custmasterrepo.GetAll().Where(t => (t.BillNo == Convert.ToString(txtbillno.Text) || t.UserSeries == Convert.ToString(txtbillno.Text)) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (userseriesSettings == false)
                    remainingdata.BillId = Convert.ToString(custmasterData.BillNo);
                else
                    remainingdata.BillId = Convert.ToString(custmasterData.BillNo);
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

        public void savebilldetail(int i, int id)
        {
            try
            {
                CustomerBillDetailRepository Custdetailrepo = new CustomerBillDetailRepository();
                CustomerBillDetail CustBillDetail = new CustomerBillDetail();

                string pname = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                string company = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                string unit = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);

                CustBillDetail.BillNo = id;
                CustBillDetail.ProductId = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                CustBillDetail.CompanyName = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                CustBillDetail.Unit = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                CustBillDetail.Quantity = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantityg"].Value);
                CustBillDetail.Code = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                CustBillDetail.Rate = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Rate"].Value);
                CustBillDetail.Amount = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Amount"].Value);
                CustBillDetail.Discount = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Disc"].Value);
                CustBillDetail.SpcDisc = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SpDisc"].Value);
                CustBillDetail.SGstPercent = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SGST"].Value);
                CustBillDetail.SGstAmt = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SGSTAmt"].Value);
                CustBillDetail.CGstPercent = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["CGST"].Value);
                CustBillDetail.CGstAmt = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["CGSTAmt"].Value);
                CustBillDetail.IGstPercent = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["IGST"].Value);
                CustBillDetail.IGstAmt = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["IGSTAmt"].Value);
                CustBillDetail.Cess = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Cess"].Value);
                CustBillDetail.CessAmt = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["CessAmt"].Value);
                CustBillDetail.SaleRate = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SaleRate"].Value);
                CustBillDetail.HSNCode = Convert.ToString(GvProductInfo.Rows[i].Cells["HSNCode"].Value);
                CustBillDetail.TotalAmount = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["TotalAmount"].Value);
                Custdetailrepo.Add(CustBillDetail);
                Custdetailrepo.Save();
                Custdetailrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void savebillstock(int i)
        {
            try
            {
                AIOInventorySystem.Data.Model.Stock updateSData = new Data.Model.Stock();
                StockRepository stockRepo = new StockRepository();
                StockRepository updateStockRepo = new StockRepository();
                ProductRepository productrepo = new ProductRepository();
                CustomerBillDetailRepository Custdetailrepo = new CustomerBillDetailRepository();
                PReturnDetailRepository PRDRepo = new PReturnDetailRepository();
                GoodDetailRepository SRDRepo = new GoodDetailRepository();

                string pname = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                string company = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                string unit = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                string size = Convert.ToString(GvProductInfo.Rows[i].Cells["Sizes"].Value);
                ProductInformation productData = new ProductInformation();
                if (CommonMethod.commProduct == true)
                    productData = productrepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                else
                    productData = productrepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                var stockData = stockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.Size==size && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();

                if (stockData != null)
                {
                    var updateStockData = stockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.Size == size && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    int updateID = updateStockData.Id; ;
                    decimal sQty = 0, ramQty = 0, rate = 0, oqty = 0;
                    try
                    {
                        try
                        {
                            oqty = Convert.ToDecimal(updateStockData.OpeningQty);
                        }
                        catch (Exception)
                        {
                            oqty = 0;
                        }
                        sQty = Convert.ToDecimal(Custdetailrepo.GetAll().Where(t => t.ProductId == pname && t.CompanyName == company && t.Unit == unit && t.Size == size && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear && t.CustomerBillMaster.CompId == CommonMethod.CompId).Sum(t => t.Quantity));
                        ramQty = oqty + Convert.ToDecimal(updateStockData.PurchaseQty) - sQty - Convert.ToDecimal(updateStockData.DeadQty) - Convert.ToDecimal(updateStockData.PurReturnQty) + Convert.ToDecimal(updateStockData.SaleReturnQty);
                    }
                    catch (Exception)
                    { }
                    updateSData.ProductId = productData.ProductCode;
                    updateSData.ProductName = pname;
                    updateSData.MfgCompany = company;
                    updateSData.Unit = unit;
                    updateSData.OpeningQty = oqty;
                    updateSData.PurchaseQty = Convert.ToDecimal(updateStockData.PurchaseQty);
                    updateSData.SaleQty = sQty;
                    updateSData.DeadQty = updateStockData.DeadQty;
                    updateSData.PurReturnQty = updateStockData.PurReturnQty;
                    updateSData.SaleReturnQty = updateStockData.SaleReturnQty;
                    updateSData.RemQty = ramQty;
                    updateSData.Size = size;
                    
                    SettingRepository settRepo = new SettingRepository();
                    Setting settData = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    try
                    {
                        db.connect();
                        SqlCommand command = new SqlCommand();
                        if (settData.StockOn == "Purchase Rate")
                            command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
                        else if (settData.StockOn == "Sale Rate")
                            command = new SqlCommand("SPNewAvgSaleRate", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = pname;
                        command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = company;
                        command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = unit;
                        command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                        command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = '0';
                        command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        SqlDataReader reader = command.ExecuteReader();
                        DataTable dtRate = new DataTable();
                        dtRate.Load(reader);
                        rate = Convert.ToDecimal(dtRate.Rows[0][0].ToString());
                        db.CloseConnection();
                        dtRate.Dispose();
                    }
                    catch (Exception)
                    { db.CloseConnection(); }
                    updateSData.ProductRate = rate;
                    updateSData.TransactionYear = CommonMethod.TransactionYear;
                    updateSData.CompId = CommonMethod.CompId;
                    updateSData.Id = updateID;
                    updateStockRepo.Edit(updateSData);
                    updateStockRepo.Save();
                }
                stockRepo.Dispose();
                updateStockRepo.Dispose();
                productrepo.Dispose();
                Custdetailrepo.Dispose();
                PRDRepo.Dispose();
                SRDRepo.Dispose();
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
                var remainingpay = remainingpayrepo.GetAll().Where(t => t.BillId == Convert.ToString(custbillno) && t.Type == "BillReturn" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                RemainingPaymentRepository remainingrepo = new RemainingPaymentRepository();
                RemainingPayment paydata = new RemainingPayment();
                if (remainingpay != null)
                {
                    if (Convert.ToDecimal(txtNetAmt.Text) > 0)
                    {
                        paydata = new RemainingPayment();
                        paydata.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                        paydata.Customername = Convert.ToString(cmbcustomername.Text);
                        paydata.NetAmt = 0;
                        paydata.PaidAmt = Convert.ToDecimal(txtNetAmt.Text);
                        RemainingPaymentRepository remainingpayrepo1 = new RemainingPaymentRepository();
                        RemainingPayment remainingdata2 = new RemainingPayment();
                        remainingdata2 = remainingpayrepo1.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.Id < remainingpay.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                        decimal decremamt = 0;
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
                        RemainingPaymentRepository remainingpayrepo2 = new RemainingPaymentRepository();
                        List<RemainingPayment> remainingdata3 = new List<RemainingPayment>();
                        remainingdata3 = remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.Id > remainingpay.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();

                        CustomerBillMasterRepository Custmasterrepo = new CustomerBillMasterRepository();
                        var custmasterData = Custmasterrepo.GetAll().Where(t => (t.BillNo == Convert.ToString(txtbillno.Text) || t.UserSeries == Convert.ToString(txtbillno.Text)) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (userseriesSettings == false)
                            paydata.BillId = Convert.ToString(custmasterData.BillNo);
                        else
                            paydata.BillId = Convert.ToString(custmasterData.BillNo);
                        paydata.BillDate = Convert.ToDateTime(dtpbilldate.Value);
                        paydata.CreateDate = Convert.ToDateTime(dtpsrdate.Value);
                        paydata.PaymentDate = Convert.ToDateTime(dtppaymentdate.Value);
                        paydata.Type = "BillReturn";
                        paydata.PaymentMode = mode;
                        paydata.TransactionYear = CommonMethod.TransactionYear;
                        paydata.CompId = CommonMethod.CompId;
                        paydata.Id = remainingpay.Id;
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
                    paydata.NetAmt = 0;
                    paydata.PaidAmt = Convert.ToDecimal(txtNetAmt.Text);
                    paydata.RemainingAmt = 0;
                    CustomerBillMasterRepository Custmasterrepo = new CustomerBillMasterRepository();
                    var custmasterData = Custmasterrepo.GetAll().Where(t => (t.BillNo == Convert.ToString(txtbillno.Text) || t.UserSeries == Convert.ToString(txtbillno.Text)) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (userseriesSettings == false)
                        paydata.BillId = Convert.ToString(custmasterData.BillNo);
                    else
                        paydata.BillId = Convert.ToString(custmasterData.BillNo);
                    paydata.BillDate = Convert.ToDateTime(dtpbilldate.Value);
                    paydata.CreateDate = Convert.ToDateTime(dtpsrdate.Value);
                    paydata.PaymentDate = Convert.ToDateTime(dtppaymentdate.Value);
                    paydata.Type = "BillReturn";
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

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                GoodMasterRepository SaleRMRepo = new GoodMasterRepository();
                var BillId = SaleRMRepo.GetAll().Where(t => t.GoodReturnNo == Convert.ToInt32(txtsrno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (BillId != null)
                {
                    MessageBox.Show("Already Saved This Record.", "Warning");
                    this.ActiveControl = btnnew;
                }
                else
                {
                    if (CustDPredata.Count == 0)
                    {
                        if (cmbcustomername.Text != "Select")
                        {
                            if (gvSaleReturnDetails.Rows.Count != 0)
                            {
                                savemaster();
                                for (int i = 0; i < gvSaleReturnDetails.Rows.Count; i++)
                                {
                                    savedetail(i);
                                    savestock(i);
                                }
                                if (billtype == 1 || billtype == 2)
                                    saveremainingpayment("Cash");
                                MessageBox.Show("Sale Return saved.", "Success");
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
                                MessageBox.Show("Select Customer Name.", "Warning");
                                this.ActiveControl = cmbcustomername;
                            }
                        }
                    }
                    else if (CustDPredata.Count > 0)
                    {
                        if (Convert.ToDecimal(txtSBRemAmt.Text) >= Convert.ToDecimal(txtNetAmt.Text))
                        {
                            if (cmbcustomername.Text != "Select")
                            {
                                if (gvSaleReturnDetails.Rows.Count != 0)
                                {
                                    savemaster();
                                    for (int i = 0; i < gvSaleReturnDetails.Rows.Count; i++)
                                    {
                                        savedetail(i);
                                        savestock(i);
                                    }

                                    CustomerBillMasterRepository Custmasterrepo = new CustomerBillMasterRepository();
                                    CustomerBillMaster CustBillMaster = new CustomerBillMaster();
                                    CustBillMaster.Id = Custmasterrepo.GetAll().Where(t => t.BillNo == txtbillno.Text && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault().Id;

                                    CustomerBillDetailRepository Custdetailrepo = new CustomerBillDetailRepository();
                                    List<CustomerBillDetail> CustBillDetail = new List<CustomerBillDetail>();
                                    CustBillDetail = Custdetailrepo.GetAll().Where(t => t.BillNo == CustBillMaster.Id && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear && t.CustomerBillMaster.CompId == CommonMethod.CompId).ToList();
                                    if (CustBillDetail.Count < GvProductInfo.Rows.Count)
                                    {
                                        for (int j = CustBillDetail.Count; j < GvProductInfo.Rows.Count; j++)
                                        {
                                            savebilldetail(j, CustBillMaster.Id);
                                            savebillstock(j);
                                        }
                                    }

                                    try
                                    {
                                        if (CustDPredata.Count > 0)
                                        {
                                            foreach (var item in CustDPredata)
                                            {
                                                Custdetailrepo = new CustomerBillDetailRepository();
                                                CustomerBillDetail CustBillDetail1 = new CustomerBillDetail();
                                                CustBillDetail1 = Custdetailrepo.GetAll().Where(t => t.Id == item.Id && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear && t.CustomerBillMaster.CompId == CommonMethod.CompId).FirstOrDefault();

                                                if (CustBillDetail1.Id == item.Id)
                                                {
                                                    Custdetailrepo = new CustomerBillDetailRepository();
                                                    CustomerBillDetail custddata = Custdetailrepo.GetById(CustBillDetail1.Id);
                                                    custddata.Status = "Inactive";
                                                    Custdetailrepo.Edit(custddata);
                                                    Custdetailrepo.Save();
                                                }
                                            }
                                            Custdetailrepo = new CustomerBillDetailRepository();
                                            decimal dtotalamt = Convert.ToDecimal(Custdetailrepo.GetAll().Where(t => t.BillNo == CustBillMaster.Id && t.Status != "Inactive" && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear && t.CustomerBillMaster.CompId == CommonMethod.CompId).Sum(t => t.TotalAmount));
                                            Custmasterrepo = new CustomerBillMasterRepository();
                                            var billmaster = Custmasterrepo.GetById(CustBillMaster.Id);
                                            billmaster.TotalAmount = dtotalamt;
                                            decimal discamt = dtotalamt * Convert.ToDecimal(billmaster.Discount) / 100;
                                            decimal vatamt = (dtotalamt - discamt) * Convert.ToDecimal(billmaster.Vat) / 100;
                                            billmaster.NetAmount = Math.Round((dtotalamt - discamt + vatamt + Convert.ToDecimal(billmaster.TransportCharges) + Convert.ToDecimal(billmaster.HamaliCharges)), 2, MidpointRounding.AwayFromZero);
                                            billmaster.PaidAmount = Convert.ToDecimal(txtSBPaidAmt.Text);
                                            billmaster.RemainingAmount = Math.Round((dtotalamt - discamt + vatamt + Convert.ToDecimal(billmaster.TransportCharges) + Convert.ToDecimal(billmaster.HamaliCharges)), 2, MidpointRounding.AwayFromZero) - Convert.ToDecimal(billmaster.PaidAmount);
                                            Custmasterrepo.Edit(billmaster);
                                            Custmasterrepo.Save();
                                            updateremainigpayment("Cash");
                                        }
                                    }
                                    catch (Exception)
                                    { }
                                    if (billtype == 1 || billtype == 2)
                                        saveremainingpayment("Cash");
                                    MessageBox.Show("Sale Return saved.", "Success");
                                    clear();
                                    this.ActiveControl = txtbillno;
                                    Custmasterrepo.Dispose();
                                    Custdetailrepo.Dispose();
                                }
                                else
                                    MessageBox.Show("Add Product Information.", "Warning");
                            }
                            else
                            {
                                if (cmbcustomername.Text == "Select")
                                {
                                    MessageBox.Show("Select Customer Name.", "Warning");
                                    this.ActiveControl = cmbcustomername;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Return amount is greater than remaining amount.", "Warning");
                            txtSBPaidAmt.Enabled = true;
                            this.ActiveControl = txtSBPaidAmt;
                        }
                    }
                }
                SaleRMRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void SendSMS(int billno, decimal netAmount)
        {
            try
            {
                SettingRepository sRepo = new SettingRepository();
                Setting sData = sRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (sData.SendSMS == "True")
                {
                    if (sData.SendOption == "Auto" && sData.AutoForBill == "True")
                    {
                        CompanyInformationRepository compRepo = new CompanyInformationRepository();
                        CompanyInformation compData = compRepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();

                        CustomerRepository custRepo = new CustomerRepository();
                        string mobileno = custRepo.GetAll().Where(t => t.CustomerID == Convert.ToInt32(cmbcustomername.SelectedValue) && t.CustomerName == cmbcustomername.Text && t.CompId == CommonMethod.CompId).FirstOrDefault().Mobileno;
                        string smsText = "Welcome to " + compData.CompanyName + ". Your Bill No is " + billno + " and Your net amount is " + netAmount + "." + Environment.NewLine + "Please Visit Again...Thank You!!";

                        WebClient client = new WebClient();
                        string URL = "";
                        SMSRepository smsRepo = new SMSRepository();
                        SendSM smsData = new SendSM();
                        smsData.CompId = CommonMethod.CompId;
                        try
                        {
                            if (!string.IsNullOrEmpty(mobileno))
                            {
                                if (System.Text.RegularExpressions.Regex.IsMatch(mobileno, "[^0-9]"))
                                {
                                    smsData.Status = "Mobile no not valid for Auto Bill";
                                    smsRepo.Add(smsData);
                                    smsRepo.Save();
                                }
                                else if (mobileno.Length == 10)
                                {
                                    string SmsStatusMsg = string.Empty;
                                    smsData.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                                    smsData.CustomerName = cmbcustomername.Text;
                                    smsData.MobileNo = mobileno;
                                    smsData.SmsText = smsText;
                                    smsData.SmsDate = DateTime.Now.Date;
                                    smsData.BillNo = Convert.ToString(billno);
                                    URL = "http://bhashsms.com/api/sendmsg.php?user=" + sData.SMSUserId + "&pass=" + sData.SMSPassword + "&sender=" + sData.SMSKey + "&phone=" + mobileno + "&text=" + smsText + "&priority=ndnd&stype=normal";
                                    string result = apicall(URL);
                                    if (result.Contains("Username/Password Incorrect") == true)
                                    {
                                        smsData.Status = "Wrong User Name or Password For Bill Auto SMS";
                                        smsRepo.Add(smsData);
                                        smsRepo.Save();
                                    }
                                    else if (result.Contains("Sender ID Does not Exist") == true)
                                    {
                                        smsData.Status = "Sender ID Does not Exist For Bill Auto SMS";
                                        smsRepo.Add(smsData);
                                        smsRepo.Save();
                                    }
                                    else
                                    {
                                        smsData.Status = "Auto Bill";
                                        smsRepo.Add(smsData);
                                        smsRepo.Save();
                                        MessageBox.Show("SMS Send Successfully.", "Success");
                                    }
                                }
                                else
                                {
                                    smsData.Status = "Mobile no not valid for Auto Bill";
                                    smsRepo.Add(smsData);
                                    smsRepo.Save();
                                }
                            }
                            else
                            {
                                smsData.Status = "Mobile dose not exsist For Auto Bill";
                                smsRepo.Add(smsData);
                                smsRepo.Save();
                            }
                        }
                        catch (Exception)
                        {
                            smsData.Status = "Send Option is Auto but customer dose not have mobile no!!";
                            smsRepo.Add(smsData);
                            smsRepo.Save();
                        }
                        compRepo.Dispose();
                        custRepo.Dispose();
                        smsRepo.Dispose();
                    }
                }
                sRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public string apicall(string url)
        {
            HttpWebRequest httpreq = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse httpres = (HttpWebResponse)httpreq.GetResponse();
                StreamReader sr = new StreamReader(httpres.GetResponseStream());
                string results = sr.ReadToEnd();
                sr.Close();
                return results;
            }
            catch
            { return "0"; }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                RptSaleReturn srrpt = new RptSaleReturn(Convert.ToInt32(printId));
                srrpt.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtQuantity.Text != "")
                {
                    try
                    {
                        getGstCalculation();
                    }
                    catch (Exception)
                    { }
                }
            }
            catch (Exception)
            { }
        }

        public void updatemaster()
        {
            try
            {
                GoodMasterRepository SaleRMRepo = new GoodMasterRepository();
                GoodReturnMaster srmasterdata = new GoodReturnMaster();
                srmasterdata = SaleRMRepo.GetAll().Where(t => t.GoodReturnNo == Convert.ToInt32(txtsrno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                srmasterdata.BillNo = custbillno;
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
                if (txtDiscount.Text != "")
                    srmasterdata.Discount = Convert.ToDecimal(txtDiscount.Text);
                else
                    srmasterdata.Discount = 0;

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
                SaleRMRepo.Edit(srmasterdata);
                SaleRMRepo.Save();
                printId = Convert.ToString(srmasterdata.GoodReturnNo);
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
                GoodDetailRepository SaleRDRepo = new GoodDetailRepository();
                GoodReturnDetail srdetaildata = new GoodReturnDetail();
                if (usevent == "update")
                    srdetaildata = SaleRDRepo.GetById(Rowid);

                srdetaildata.GoodReturnNo = BillMasterID;
                srdetaildata.ProductId = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRProductName"].Value);
                srdetaildata.CompanyName = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRCompany"].Value);
                srdetaildata.Unit = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRUnit"].Value);
                srdetaildata.SaleUnit = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRSaleUnit"].Value);
                if (Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRUnit"].Value) != Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRSaleUnit"].Value))
                {
                    ProductRepository productrepo = new ProductRepository();
                    ProductInformation productdata = new ProductInformation();
                    if (CommonMethod.commProduct == true)
                        productdata = productrepo.GetAll().Where(t => t.ProductName == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRProductName"].Value) && t.ManufactureCompany == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRCompany"].Value) && t.Uniti == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRUnit"].Value)).FirstOrDefault();
                    else
                        productdata = productrepo.GetAll().Where(t => t.ProductName == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRProductName"].Value) && t.ManufactureCompany == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRCompany"].Value) && t.Uniti == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRUnit"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    srdetaildata.Quantity = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRQuantity"].Value) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                }
                else
                    srdetaildata.Quantity = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRQuantity"].Value);
                srdetaildata.Code = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRCode"].Value);
                srdetaildata.Rate = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRRate"].Value);
                srdetaildata.Amount = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRAmount"].Value);
                srdetaildata.Discount = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRDisc"].Value);
                srdetaildata.SpcDisc = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRSpDisc"].Value);
                srdetaildata.SGstPercent = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRSGST"].Value);
                srdetaildata.SGstAmt = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRSGSTAmt"].Value);
                srdetaildata.CGstPercent = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRCGST"].Value);
                srdetaildata.CGstAmt = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRCGSTAmt"].Value);
                srdetaildata.IGstPercent = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRIGST"].Value);
                srdetaildata.IGstAmt = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRIGSTAmt"].Value);
                srdetaildata.Cess = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRCess"].Value);
                srdetaildata.CessAmt = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRCessAmt"].Value);
                srdetaildata.SaleRate = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRSaleRate"].Value);
                srdetaildata.HSNCode = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRHSNCode"].Value);
                srdetaildata.TotalAmount = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRTotalAmt"].Value);
                srdetaildata.Reason = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRReason"].Value);
                srdetaildata.BNID = Convert.ToInt32(gvSaleReturnDetails.Rows[i].Cells["PRBNID"].Value);
                srdetaildata.Barcode = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRBarcode"].Value);
                srdetaildata.Size = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRSize"].Value);
                srdetaildata.DeptName = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRDepartment"].Value);
                srdetaildata.Category = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRCategory"].Value);
                srdetaildata.ColorName = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRColorName"].Value);
                srdetaildata.barcodePrice = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRBarcodePrice"].Value);

                srdetaildata.AssemblyChildStatus = "N";
                if (usevent == "update")
                    SaleRDRepo.Edit(srdetaildata);
                if (usevent == "save")
                    SaleRDRepo.Add(srdetaildata);
                SaleRDRepo.Save();
                SaleRDRepo.Dispose();
                decimal assemblyQty = Convert.ToDecimal(srdetaildata.Quantity);
                int BDetailId = Convert.ToInt32(srdetaildata.BNID);
                string Reason = Convert.ToString(srdetaildata.Reason);
                decimal SGSTPercent = 0, CGSTPercent = 0, IGSTPercent = 0;
                SGSTPercent = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRSGST"].Value);
                CGSTPercent = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRCGST"].Value);
                IGSTPercent = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRIGST"].Value);
                try
                {
                    ProductRepository productRepo = new ProductRepository();
                    var pData = productRepo.GetAll().Where(t => t.ProductName == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRProductName"].Value) && t.ManufactureCompany == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRCompany"].Value) && t.Uniti == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRUnit"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (pData != null)
                    {
                        AssemeblyProductDetailsRepository assemblyDRepo = new AssemeblyProductDetailsRepository();
                        List<tblAssemblyProductDetail> assemblyDList = assemblyDRepo.GetAll().Where(t => t.AssemblyProductId == pData.ID && t.CompId == CommonMethod.CompId).ToList();
                        if (assemblyDList.Count > 0)
                        {
                            foreach (var itemAssembly in assemblyDList)
                            {
                                int UpdateSRId = 0;
                                GoodDetailRepository SaleRDRepo1 = new GoodDetailRepository();
                                GoodReturnDetail srdetaildata1 = new GoodReturnDetail();
                                if (usevent == "update")
                                {
                                    var srDetailData = SaleRDRepo1.GetAll().Where(t => t.GoodReturnNo == BillMasterID && t.ProductId == itemAssembly.ProductName && t.CompanyName == itemAssembly.CompanyName && t.Unit == itemAssembly.Unit && t.GoodReturnMaster.CompId == CommonMethod.CompId && t.GoodReturnMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    if (srDetailData != null)
                                        UpdateSRId = Convert.ToInt32(srDetailData.Id);
                                    srdetaildata1 = SaleRDRepo1.GetById(UpdateSRId);
                                }
                                srdetaildata1.GoodReturnNo = BillMasterID;
                                srdetaildata1.ProductId = Convert.ToString(itemAssembly.ProductName);
                                srdetaildata1.CompanyName = Convert.ToString(itemAssembly.CompanyName);
                                srdetaildata1.Unit = Convert.ToString(itemAssembly.Unit);
                                srdetaildata1.SaleUnit = Convert.ToString(itemAssembly.Unit);
                                srdetaildata1.Quantity = Convert.ToDecimal(Convert.ToDecimal(itemAssembly.Qty) * assemblyQty);
                                srdetaildata1.Code = "";
                                srdetaildata1.Rate = Convert.ToDecimal(0);
                                srdetaildata1.Amount = Convert.ToDecimal(0);
                                srdetaildata1.Discount = Convert.ToDecimal(0);
                                srdetaildata1.SpcDisc = Convert.ToDecimal(0);
                                srdetaildata1.SGstPercent = Convert.ToDecimal(SGSTPercent);
                                srdetaildata1.SGstAmt = Convert.ToDecimal(0);
                                srdetaildata1.CGstPercent = Convert.ToDecimal(CGSTPercent);
                                srdetaildata1.CGstAmt = Convert.ToDecimal(0);
                                srdetaildata1.IGstPercent = Convert.ToDecimal(IGSTPercent);
                                srdetaildata1.IGstAmt = Convert.ToDecimal(0);
                                srdetaildata1.Cess = Convert.ToDecimal(0);
                                srdetaildata1.CessAmt = Convert.ToDecimal(0);
                                srdetaildata1.SaleRate = Convert.ToDecimal(0);
                                srdetaildata1.HSNCode = "";
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
                                srdetaildata1.AssemblyChildStatus = "Y";
                                srdetaildata1.BatchNo = "";
                                srdetaildata1.Expiry = "";
                                srdetaildata1.Barcode = "";
                                if (usevent == "update")
                                    SaleRDRepo1.Edit(srdetaildata1);
                                else
                                    SaleRDRepo1.Add(srdetaildata1);
                                SaleRDRepo1.Save();
                                SaleRDRepo1.Dispose();
                            }
                        }
                    }
                }
                catch (Exception)
                { }

                try
                {
                    BatchRepository BatchRepo = new BatchRepository();
                    Batch BatchData = new Batch();
                    BatchData = BatchRepo.GetAll().Where(t => t.BillId == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRBNID"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (BatchData != null)
                    {
                        PurchaseDetail pData = new PurchaseDetail();
                        PurchaseMasterRepository pMastRepo = new PurchaseMasterRepository();
                        PurchaseDetail purdetail11 = new PurchaseDetail();
                        PurchaseMaster PMasterData1 = new PurchaseMaster();
                        PurchaseDetailRepository ppRepo = new PurchaseDetailRepository();
                        PMasterData1 = pMastRepo.GetAll().Where(t => t.Id == BatchData.PorderNo && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                        if (PMasterData1 != null)
                            purdetail11 = ppRepo.GetAll().Where(t => t.ProductName == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRProductName"].Value) && t.CompanyName == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRCompany"].Value) && t.Unit == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRUnit"].Value) && t.BatchNo == Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRBatchNo"].Value) && t.PorderNo == BatchData.PorderNo && t.PurchaseMaster.TransactionYear == PMasterData1.TransactionYear).FirstOrDefault();
                        pData = ppRepo.GetById(purdetail11.Id);
                        pData.Status = "Active";
                        ppRepo.Edit(pData);
                        ppRepo.Save();
                    }
                }
                catch (Exception)
                { }
            }
            catch (Exception)
            { }
        }

        public void updatestock(int i)
        {
            try
            {
                AIOInventorySystem.Data.Model.Stock updateSData = new Data.Model.Stock();
                StockRepository stockRepo = new StockRepository();
                StockRepository updateStockRepo = new StockRepository();
                ProductRepository productrepo = new ProductRepository();
                CustomerBillDetailRepository Custdetailrepo = new CustomerBillDetailRepository();
                GoodDetailRepository SaleRDRepo = new GoodDetailRepository();
                PReturnDetailRepository PRDRepo = new PReturnDetailRepository();

                string pname = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRProductName"].Value);
                string company = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRCompany"].Value);
                string unit = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRUnit"].Value);
                string size = Convert.ToString(gvSaleReturnDetails.Rows[i].Cells["PRSize"].Value);

                ProductInformation productData = new ProductInformation();
                if (CommonMethod.commProduct == true)
                    productData = productrepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                else
                    productData = productrepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (productData.ProductType == 2)
                {
                    try
                    {
                        db.connect();
                        SqlCommand command = new SqlCommand("SPSaveUpdateSalePurchaseReturnStock", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@status", SqlDbType.VarChar).Value = "SR";
                        command.Parameters.Add("@proname", SqlDbType.VarChar).Value = pname;
                        command.Parameters.Add("@cname", SqlDbType.VarChar).Value = company;
                        command.Parameters.Add("@uname", SqlDbType.VarChar).Value = unit;
                        command.Parameters.Add("@Qty", SqlDbType.Decimal).Value = 0;
                        command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        command.Parameters.Add("@event", SqlDbType.VarChar).Value = "Save";
                        command.ExecuteNonQuery();
                        db.CloseConnection();
                    }
                    catch (Exception)
                    {
                        db.CloseConnection();
                    }
                }
                else
                {
                    var stockData = stockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.Size == size && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (stockData != null)
                    {
                        var updateStockData = stockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.Size == size && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        int updateID = updateStockData.Id; ;
                        decimal srQty = 0, ramQty = 0, rate = 0, oqty = 0;
                        try
                        {
                            try
                            { oqty = Convert.ToDecimal(updateStockData.OpeningQty); }
                            catch (Exception)
                            { oqty = 0; }
                            srQty = Convert.ToDecimal(SaleRDRepo.GetAll().Where(t => t.ProductId == pname && t.CompanyName == company && t.Unit == unit && t.Size == size && t.GoodReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.GoodReturnMaster.CompId == CommonMethod.CompId).Sum(t => t.Quantity));
                            ramQty = oqty + Convert.ToDecimal(updateStockData.PurchaseQty) - Convert.ToDecimal(updateStockData.PurReturnQty) - Convert.ToDecimal(updateStockData.SaleQty) + srQty - Convert.ToDecimal(updateStockData.DeadQty);
                        }
                        catch (Exception)
                        { }
                        updateSData.ProductId = productData.ProductCode;
                        updateSData.ProductName = pname;
                        updateSData.MfgCompany = company;
                        updateSData.Unit = unit;
                        updateSData.OpeningQty = oqty;
                        updateSData.PurchaseQty = Convert.ToDecimal(updateStockData.PurchaseQty);
                        updateSData.SaleQty = Convert.ToDecimal(updateStockData.SaleQty);
                        updateSData.DeadQty = updateStockData.DeadQty;
                        updateSData.RemQty = ramQty;
                        updateSData.ProductRate = Convert.ToDecimal(gvSaleReturnDetails.Rows[i].Cells["PRRate"].Value);
                        updateSData.PurReturnQty = Convert.ToDecimal(updateStockData.PurReturnQty);
                        updateSData.SaleReturnQty = srQty;
                        updateSData.Size = size;
                        SettingRepository settRepo = new SettingRepository();
                        Setting settData = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand();
                            if (settData.StockOn == "Purchase Rate")
                                command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
                            else if (settData.StockOn == "Sale Rate")
                                command = new SqlCommand("SPNewAvgSaleRate", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = pname;
                            command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = company;
                            command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = unit;
                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            SqlDataReader reader = command.ExecuteReader();
                            DataTable dtRate = new DataTable();
                            dtRate.Load(reader);
                            rate = Convert.ToDecimal(dtRate.Rows[0][0].ToString());
                            db.CloseConnection();
                            dtRate.Dispose();
                        }
                        catch (Exception)
                        { db.CloseConnection(); }
                        updateSData.ProductRate = rate;
                        updateSData.TransactionYear = CommonMethod.TransactionYear;
                        updateSData.CompId = CommonMethod.CompId;
                        updateSData.Id = updateID;
                        updateStockRepo.Edit(updateSData);
                        updateStockRepo.Save();
                    }
                }
                stockRepo.Dispose(); updateStockRepo.Dispose(); productrepo.Dispose();
                Custdetailrepo.Dispose(); SaleRDRepo.Dispose(); PRDRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void deleteitemwithstockupdation(int i)
        {
            try
            {
                GoodDetailRepository SaleRDRepo = new GoodDetailRepository();
                StockRepository stockRepo = new StockRepository();
                AIOInventorySystem.Data.Model.Stock updateSData = new Data.Model.Stock();
                StockRepository updateStockRepo = new StockRepository();
                ProductRepository productrepo = new ProductRepository();
                GoodReturnDetail srdetaildata = SaleRDRepo.GetById(i);

                string pname = srdetaildata.ProductId;
                string company = srdetaildata.CompanyName;
                string unit = srdetaildata.Unit;
                string size = srdetaildata.Size;
                decimal Quantity = Convert.ToDecimal(srdetaildata.Quantity);
                int MasterId = Convert.ToInt32(srdetaildata.GoodReturnNo);
                ProductInformation productData = new ProductInformation();
                if (CommonMethod.commProduct == true)
                    productData = productrepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                else
                    productData = productrepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (productData.ProductType == 2)
                {
                    try
                    {
                        db.connect();
                        SqlCommand command = new SqlCommand("SPSaveUpdateSalePurchaseReturnStock", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@status", SqlDbType.VarChar).Value = "SR";
                        command.Parameters.Add("@proname", SqlDbType.VarChar).Value = pname;
                        command.Parameters.Add("@cname", SqlDbType.VarChar).Value = company;
                        command.Parameters.Add("@uname", SqlDbType.VarChar).Value = unit;
                        command.Parameters.Add("@Qty", SqlDbType.Decimal).Value = Quantity;
                        command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        command.Parameters.Add("@event", SqlDbType.VarChar).Value = "Remove";
                        command.Parameters.Add("@MasterId", SqlDbType.Int).Value = MasterId;
                        command.ExecuteNonQuery();
                        db.CloseConnection();
                    }
                    catch (Exception)
                    {
                        db.CloseConnection();
                    }
                }
                else
                {
                    if (billtype == 1 || billtype == 2)
                    {
                        var stockData = stockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.Size == size && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (stockData != null)
                        {
                            var updateStockData = stockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.Size == size && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            int updateID = updateStockData.Id; ;
                            decimal srQty = 0, ramQty = 0, rate = 0, oqty = 0;
                            try
                            {
                                try
                                { oqty = Convert.ToDecimal(updateStockData.OpeningQty); }
                                catch (Exception)
                                { oqty = 0; }
                                srQty = Convert.ToDecimal(SaleRDRepo.GetAll().Where(t => t.ProductId == pname && t.CompanyName == company && t.Unit == unit && t.Size == size && t.GoodReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.GoodReturnMaster.CompId == CommonMethod.CompId).Sum(t => t.Quantity));
                                srQty = srQty - Quantity;
                                ramQty = oqty + Convert.ToDecimal(updateStockData.PurchaseQty) - Convert.ToDecimal(updateStockData.PurReturnQty) - Convert.ToDecimal(updateStockData.SaleQty) + srQty - Convert.ToDecimal(updateStockData.DeadQty);
                            }
                            catch (Exception)
                            { }
                            updateSData.ProductId = productData.ProductCode;
                            updateSData.ProductName = pname;
                            updateSData.MfgCompany = company;
                            updateSData.Unit = unit;
                            updateSData.OpeningQty = oqty;
                            updateSData.PurchaseQty = Convert.ToDecimal(updateStockData.PurchaseQty);
                            updateSData.SaleQty = Convert.ToDecimal(updateStockData.SaleQty);
                            updateSData.DeadQty = updateStockData.DeadQty;
                            updateSData.RemQty = ramQty;
                            updateSData.PurReturnQty = Convert.ToDecimal(updateStockData.PurReturnQty);
                            updateSData.SaleReturnQty = srQty;
                            updateSData.Size = size;
                            SettingRepository settRepo = new SettingRepository();
                            Setting settData = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();

                            try
                            {
                                db.connect();
                                SqlCommand command = new SqlCommand();
                                if (settData.StockOn == "Purchase Rate")
                                    command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
                                else if (settData.StockOn == "Sale Rate")
                                    command = new SqlCommand("SPNewAvgSaleRate", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = pname;
                                command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = company;
                                command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = unit;
                                command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                                command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = '0';
                                command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                SqlDataReader reader = command.ExecuteReader();
                                DataTable dtRate = new DataTable();
                                dtRate.Load(reader);
                                rate = Convert.ToDecimal(dtRate.Rows[0][0].ToString());
                                db.CloseConnection();
                                dtRate.Dispose();
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
                            updateSData.ProductRate = rate;
                            updateSData.TransactionYear = CommonMethod.TransactionYear;
                            updateSData.CompId = CommonMethod.CompId;
                            updateSData.Id = updateID;
                            updateStockRepo.Edit(updateSData);
                            updateStockRepo.Save();
                        }
                    }
                }
                GoodDetailRepository SaleRDRepo2 = new GoodDetailRepository();
                GoodReturnDetail srdetaildata2 = SaleRDRepo2.GetById(i);
                SaleRDRepo2.Remove(srdetaildata2);
                SaleRDRepo2.Save();
                SaleRDRepo2.Dispose(); SaleRDRepo.Dispose();
                productrepo.Dispose(); stockRepo.Dispose(); updateStockRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void cleartext()
        {
            txtQuantity.Text = "0";
            txtrate.Text = "0";
            txtpdiscount.Text = "0";
            txtAmount.Text = "0";
            txtpTotalAmt.Text = "0";
        }

        private void txtProductname_Leave(object sender, EventArgs e)
        {
            try
            {
                custbillinfo = this;
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
                        if (productdata == null)
                        {
                            MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                            txtProductname.Text = "";
                            this.ActiveControl = txtProductname;
                        }
                        else
                        {
                            try
                            {
                                int i = 0;
                                GoodDetailRepository gdr = new GoodDetailRepository();
                                CustomerBillDetailRepository cbdr = new CustomerBillDetailRepository();
                                List<CustomerBillDetail> cbdlist = cbdr.GetAll().Where(t => t.ProductId == proname && t.CompanyName == company && t.Unit == unit && t.CustomerBillMaster.CompId == CommonMethod.CompId && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear).ToList();
                                if (cbdlist != null)
                                {
                                    dgvqtydetails.Rows.Clear();
                                    foreach (var item in cbdlist)
                                    {
                                        dgvqtydetails.Visible = true;
                                        dgvqtydetails.Rows.Add();
                                        dgvqtydetails.Rows[i].Cells[0].Value = item.CustomerBillMaster.BillNo;
                                        dgvqtydetails.Rows[i].Cells[1].Value = item.Quantity;
                                        try
                                        {
                                            var rqty = Convert.ToDecimal(gdr.GetAll().Where(t => t.BNID == item.Id).FirstOrDefault().Quantity);
                                            dgvqtydetails.Rows[i].Cells[2].Value = rqty;
                                        }
                                        catch (Exception) { }
                                        i++;
                                    }
                                    this.ActiveControl = txtbillno;
                                }
                            }
                            catch (Exception)
                            { }
                            if (chkexchange.Checked)
                            {
                                txtrate.Enabled = true;
                                txtAmount.Enabled = true;
                                txtpdiscount.Enabled = true;
                                txtpTotalAmt.Enabled = true;
                                txtQuantity.Text = "0";
                                txtrate.Text = Convert.ToString(productdata.Rate);
                            }
                            taxinclude = Convert.ToString(productdata.TaxInclusive);
                            productentrytype = Convert.ToInt32(productdata.ProductType);  // allinone standard
                            if (productdata.ProductType == 0 && productdata.WithBatchAndExpiry == "N" && productdata.WithBatch == "N")
                                productentrytype = 0;
                            else if (productdata.ProductType == 0 && productdata.WithBatchAndExpiry == "Y")
                                productentrytype = 1;
                            else if (productdata.ProductType == 0 && productdata.WithBatch == "Y") //krushi
                                productentrytype = 2;
                            else if (productdata.ProductType == 3 && productdata.WithoutSize == "N")  //clothing
                                productentrytype = 3;
                            else if (productdata.ProductType == 3 && productdata.WithoutSize == "Y")  //clothing
                                productentrytype = 4;
                            else if (productdata.ProductType == 1) // serialized
                                productentrytype = 5;
                            else if (productdata.ProductType == 2) // Assembly
                                productentrytype = 6;
                            else if (productdata.ProductType == 4) //kit
                                productentrytype = 7;
                            else if (productdata.ProductType == 5) //scrap
                                productentrytype = 8;
                            AdjustPanelDetails(productentrytype);
                        }
                        productrepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                        txtProductname.Text = "";
                        this.ActiveControl = txtProductname;
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
                if (btnsave.Enabled == true)
                    btnsave_Click(null, null);
                else if (btnUpdate.Enabled == true)
                    btnUpdate_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtQuantity.Text != "")
                {
                    string proname = txtProductname.Text.Trim().Split(',')[0];
                    string company = txtProductname.Text.Trim().Split(',')[1];
                    string unit = txtProductname.Text.Trim().Split(',')[2];
                    decimal remqty = 0;
                    PurchaseDetailRepository pdetailrepo = new PurchaseDetailRepository();
                    PurchaseDetail pddata = new PurchaseDetail();
                    CustomerBillDetailRepository Custdetailrepo = new CustomerBillDetailRepository();
                    CustomerBillDetail cddata = new CustomerBillDetail();
                    DeadDetailRepository DeaddetailRepo = new DeadDetailRepository();
                    DeadProductDetail deaddetail = new DeadProductDetail();
                    PReturnDetailRepository PrdetailRepo = new PReturnDetailRepository();
                    PurchaseReturnDetail prdetail = new PurchaseReturnDetail();
                    GoodDetailRepository GdetailRepo = new GoodDetailRepository();
                    GoodReturnDetail Gdetail = new GoodReturnDetail();
                    if (chkreturn.Checked)
                    {
                        if (sbcolIndex != -1)
                        {
                            string productname = GvProductInfo.Rows[sbcolIndex].Cells["ProductNameg"].Value.ToString();
                            string compname = GvProductInfo.Rows[sbcolIndex].Cells["Company"].Value.ToString();
                            string unitname = GvProductInfo.Rows[sbcolIndex].Cells["Unit"].Value.ToString();
                            decimal txtqty = 0, enterqty = 0;
                            if (unitname != GvProductInfo.Rows[sbcolIndex].Cells["SaleUnit"].Value.ToString())
                            {
                                ProductRepository productrepo = new ProductRepository();
                                ProductInformation productdata = new ProductInformation();
                                if (CommonMethod.commProduct == true)
                                    productdata = productrepo.GetAll().Where(t => t.ProductName == productname && t.ManufactureCompany == compname && t.Uniti == unitname).FirstOrDefault();
                                else
                                    productdata = productrepo.GetAll().Where(t => t.ProductName == productname && t.ManufactureCompany == compname && t.Uniti == unitname && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                txtqty = Convert.ToDecimal(GvProductInfo.Rows[sbcolIndex].Cells["Quantityg"].Value) * ((Convert.ToDecimal(productdata.UnitQty)) / (Convert.ToDecimal(productdata.AlternateUnitQty)));
                                enterqty = Convert.ToDecimal(txtQuantity.Text) * ((Convert.ToDecimal(productdata.UnitQty)) / (Convert.ToDecimal(productdata.AlternateUnitQty)));
                                if (gvSaleReturnDetails.RowCount > 0)
                                {
                                    for (int k = 0; k < gvSaleReturnDetails.RowCount; k++)
                                    {
                                        if (gvSaleReturnDetails.Rows[k].Cells["PRProductName"].Value.ToString() == productname && gvSaleReturnDetails.Rows[k].Cells["PRCompany"].Value.ToString() == compname && gvSaleReturnDetails.Rows[k].Cells["PRCompany"].Value.ToString() == unitname)
                                        {
                                            if (gvSaleReturnDetails.Rows[k].Cells["PRUnit"].Value.ToString() != unitname)
                                                enterqty = enterqty + Convert.ToDecimal(gvSaleReturnDetails.Rows[k].Cells["PRQuantity"].Value.ToString()) * ((Convert.ToDecimal(productdata.UnitQty)) / (Convert.ToDecimal(productdata.AlternateUnitQty)));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                txtqty = Convert.ToDecimal(GvProductInfo.Rows[sbcolIndex].Cells["Quantityg"].Value);
                                enterqty = Convert.ToDecimal(txtQuantity.Text);
                                if (gvSaleReturnDetails.RowCount > 0)
                                {
                                    for (int k = 0; k < gvSaleReturnDetails.RowCount; k++)
                                    {
                                        if (gvSaleReturnDetails.Rows[k].Cells["PRProductName"].Value.ToString() == productname && gvSaleReturnDetails.Rows[k].Cells["PRCompany"].Value.ToString() == compname && gvSaleReturnDetails.Rows[k].Cells["PRCompany"].Value.ToString() == unitname)
                                        {
                                            if (gvSaleReturnDetails.Rows[k].Cells["PRUnit"].Value.ToString() == unitname)
                                                enterqty = enterqty + Convert.ToDecimal(gvSaleReturnDetails.Rows[k].Cells["PRQuantity"].Value.ToString());
                                        }
                                    }
                                }
                            }
                            if (ProductCategory == "Motor")
                            {
                                decimal srqty = 0;
                                try
                                {
                                    srqty = Convert.ToDecimal(GdetailRepo.GetAll().Where(t => t.ProductId == productname && t.CompanyName == compname && t.Unit == unitname && t.Code == GvProductInfo.Rows[sbcolIndex].Cells["Code"].Value.ToString() && t.GoodReturnMaster.GoodReturnNo == Convert.ToInt32(txtsrno.Text) && t.GoodReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.GoodReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault().Quantity);
                                }
                                catch (Exception)
                                { srqty = 0; }
                                if (srqty == 0)
                                {
                                    if (Convert.ToDecimal(txtQuantity.Text) != 1)
                                    {
                                        MessageBox.Show("Must be 1 quantity for return.", "Warning");
                                        txtQuantity.Text = "1";
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Product Remaining Quantity in Stock is " + remqty, "Warning");
                                    txtQuantity.Text = "";
                                    this.ActiveControl = txtQuantity;
                                }
                            }
                            else
                            {
                                Gdetail.Quantity = GdetailRepo.GetAll().Where(t => t.ProductId == productname && t.CompanyName == compname && t.Unit == unitname && t.GoodReturnMaster.GoodReturnNo == Convert.ToInt32(txtsrno.Text) && t.GoodReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.GoodReturnMaster.CompId == CommonMethod.CompId).Sum(t => t.Quantity);
                                remqty = txtqty - Convert.ToDecimal(Gdetail.Quantity);
                                if (enterqty <= remqty)
                                { }
                                else
                                {
                                    MessageBox.Show("Product Remaining Quantity in Stock is " + remqty, "Warning");
                                    txtQuantity.Text = "";
                                    this.ActiveControl = txtQuantity;
                                }
                            }
                        }
                    }
                    try
                    {
                        decimal rate, qty = 0;
                        if (txtrate.Text == "")
                            rate = 0;
                        else
                            rate = Convert.ToDecimal(txtrate.Text);
                        if (txtQuantity.Text != "")
                            qty = Convert.ToDecimal(txtQuantity.Text);
                        if (txtAmount.Text == "0")
                            txtAmount.Text = Convert.ToString(Math.Round(rate * qty, 2, MidpointRounding.AwayFromZero));
                    }
                    catch (Exception)
                    { }
                    pdetailrepo.Dispose(); Custdetailrepo.Dispose(); DeaddetailRepo.Dispose();
                    PrdetailRepo.Dispose(); GdetailRepo.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        public void getGstCalculation()
        {
            decimal d = 0, damt = 0;
            decimal SGAmt = 0, CGAmt = 0, IGAmt = 0, TaxableAmt = 0, CalculatedAmt = 0, sgstpercent = 0, cgstpercent = 0, Igstpercent = 0, SpcDiscAmt = 0, Cessamt = 0;
            try
            {
                if (txtQuantity.Text != "" && txtrate.Text != "")
                {
                    sgstpercent = Convert.ToDecimal(cmbsgst.Text);
                    cgstpercent = Convert.ToDecimal(cmbcgst.Text);
                    Igstpercent = Convert.ToDecimal(cmbigst.Text);
                    /**********************/
                    if (taxinclude == "True")
                    {
                        decimal basicrate = 0;
                        basicrate = Convert.ToDecimal(txtrate.Text) * 100 / (100 + (cgstpercent + sgstpercent + Igstpercent));
                        lblBasicRate.Text = Convert.ToString(Math.Round(basicrate, 2, MidpointRounding.AwayFromZero));

                        CalculatedAmt = Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtrate.Text);
                        txtAmount.Text = Convert.ToString(Math.Round(CalculatedAmt, 2, MidpointRounding.AwayFromZero));
                        TaxableAmt = basicrate * Convert.ToDecimal(txtQuantity.Text);

                        if (flgdiscountchange == true)
                        {
                            decimal discount = 0;
                            discount = Convert.ToDecimal(txtpdisAmt.Text) / Convert.ToDecimal(TaxableAmt) * 100;
                            damt = Convert.ToDecimal(txtpdisAmt.Text);
                            txtpdiscount.Text = Convert.ToString(Math.Round(discount, 2, MidpointRounding.AwayFromZero));
                            TaxableAmt = (TaxableAmt - Convert.ToDecimal(txtpdisAmt.Text));
                        }
                        else
                        {
                            if (txtpdiscount.Text != "")
                            {
                                damt = (TaxableAmt * Convert.ToDecimal(txtpdiscount.Text) / 100);
                                txtpdisAmt.Text = Convert.ToString(Math.Round(damt, 2, MidpointRounding.AwayFromZero));
                                TaxableAmt = (TaxableAmt - damt);
                            }
                        }
                        if (txtpSpcDisc.Text != "")
                        {
                            SpcDiscAmt = (TaxableAmt * Convert.ToDecimal(txtpSpcDisc.Text) / 100);
                            TaxableAmt = (TaxableAmt - SpcDiscAmt);
                        }
                        txtTaxableamt.Text = Convert.ToString(Math.Round(TaxableAmt, 2, MidpointRounding.AwayFromZero));
                        if (cmbsgst.Text != "")
                            SGAmt = (TaxableAmt * Convert.ToDecimal(cmbsgst.Text) / 100);
                        if (cmbcgst.Text != "")
                            CGAmt = (TaxableAmt * Convert.ToDecimal(cmbcgst.Text) / 100);
                        if (cmbigst.Text != "")
                            IGAmt = (TaxableAmt * Convert.ToDecimal(cmbigst.Text) / 100);
                        if (txtpCess.Text != "")
                            Cessamt = (TaxableAmt * Convert.ToDecimal(txtpCess.Text) / 100);
                        TaxableAmt = TaxableAmt + SGAmt + CGAmt + IGAmt + Cessamt;
                        txtpTotalAmt.Text = Convert.ToString(Math.Round(TaxableAmt, 2, MidpointRounding.AwayFromZero));
                    }
                    else
                    {
                        TaxableAmt = Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtrate.Text);
                        txtAmount.Text = Convert.ToString(TaxableAmt);
                        if (flgdiscountchange == true)
                        {
                            decimal discount = 0;
                            discount = Convert.ToDecimal(txtpdisAmt.Text) / Convert.ToDecimal(TaxableAmt) * 100;
                            damt = Convert.ToDecimal(txtpdisAmt.Text);
                            txtpdiscount.Text = Convert.ToString(Math.Round(discount, 2, MidpointRounding.AwayFromZero));
                            txtpTotalAmt.Text = Convert.ToString(Math.Round(TaxableAmt - Convert.ToDecimal(txtpdisAmt.Text), 2, MidpointRounding.AwayFromZero));
                        }
                        else
                        {
                            if (txtpdiscount.Text != "")
                            {
                                d = Convert.ToDecimal(txtpdiscount.Text);
                                damt = (TaxableAmt * d) / 100;
                                txtpdisAmt.Text = Convert.ToString(Math.Round(damt, 2, MidpointRounding.AwayFromZero));
                                txtpTotalAmt.Text = Convert.ToString(Math.Round(TaxableAmt - damt, 2, MidpointRounding.AwayFromZero));
                            }
                        }
                        if (txtpSpcDisc.Text != "")
                        {
                            SpcDiscAmt = ((TaxableAmt - damt) * Convert.ToDecimal(txtpSpcDisc.Text)) / 100;
                            txtpTotalAmt.Text = Convert.ToString(Math.Round(TaxableAmt - damt - SpcDiscAmt, 2, MidpointRounding.AwayFromZero));
                        }
                        TaxableAmt = Convert.ToDecimal(txtpTotalAmt.Text);
                        txtTaxableamt.Text = txtpTotalAmt.Text;
                        /*******************/
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
                        decimal basicrate = 0;
                        basicrate = Convert.ToDecimal(txtrate.Text);
                        lblBasicRate.Text = Convert.ToString(Math.Round(basicrate, 2, MidpointRounding.AwayFromZero));
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error On Calculating Amount", "Warning");
                this.ActiveControl = txtTotalAmt;
            }
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            try
            {
                GoodMasterRepository SRMRepo = new GoodMasterRepository();
                decimal netamount = Convert.ToDecimal(SRMRepo.GetAll().Where(t => t.GoodReturnNo == Convert.ToInt32(printId) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault().NetAmount);
                SendSMSMannual(printId, netamount);
                SRMRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void SendSMSMannual(string billno, decimal netAmount)
        {
            try
            {
                SettingRepository sRepo = new SettingRepository();
                Setting sData = sRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (sData.SendSMS == "True")
                {
                    CompanyInformationRepository compRepo = new CompanyInformationRepository();
                    CompanyInformation compData = compRepo.GetAll().Where(t => t.CompanyId == CommonMethod.CompId).FirstOrDefault();

                    CustomerRepository custRepo = new CustomerRepository();
                    string mobileno = custRepo.GetAll().Where(t => t.CustomerID == Convert.ToInt32(cmbcustomername.SelectedValue) && t.CustomerName == cmbcustomername.Text && t.CompId == CommonMethod.CompId).FirstOrDefault().Mobileno;
                    string smsText = "Welcome to " + compData.CompanyName + ". Your Bill No is " + billno + " and Your net amount is " + netAmount + "." + Environment.NewLine + "Please Visit Again...Thank You!!";
                    string MobNo = mobileno;
                    WebClient client = new WebClient();
                    string URL = "";
                    SMSRepository smsRepo = new SMSRepository();
                    SendSM smsData = new SendSM();
                    smsData.CompId = CommonMethod.CompId;
                    try
                    {
                        if (!string.IsNullOrEmpty(MobNo))
                        {
                            if (System.Text.RegularExpressions.Regex.IsMatch(MobNo, "[^0-9]"))
                            {
                                smsData.Status = "Mobile not valid For for Mannual SMS";
                                smsRepo.Add(smsData);
                                smsRepo.Save();
                            }
                            else if (MobNo.Length == 10)
                            {
                                string SmsStatusMsg = string.Empty;
                                smsData.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                                smsData.CustomerName = cmbcustomername.Text;
                                smsData.MobileNo = mobileno;
                                smsData.SmsText = smsText;
                                smsData.SmsDate = DateTime.Now.Date;
                                smsData.BillNo = Convert.ToString(billno);
                                URL = "http://bhashsms.com/api/sendmsg.php?user=" + sData.SMSUserId + "&pass=" + sData.SMSPassword + "&sender=" + sData.SMSKey + "&phone=" + MobNo + "&text=" + smsText + "&priority=ndnd&stype=normal";
                                string result = apicall(URL);
                                if (result.Contains("Username/Password Incorrect") == true)
                                {
                                    smsData.Status = "Wrong User Name or Password For Mannual SMS";
                                    smsRepo.Add(smsData);
                                    smsRepo.Save();
                                }
                                else if (result.Contains("Sender ID Does not Exist") == true)
                                {
                                    smsData.Status = "Sender ID Does not Exist For Mannual SMS";
                                    smsRepo.Add(smsData);
                                    smsRepo.Save();
                                }
                                else
                                {
                                    smsData.Status = "Mannual SMS";
                                    smsRepo.Add(smsData);
                                    smsRepo.Save();
                                    MessageBox.Show("SMS Send Successfully.", "Success");
                                }
                            }
                            else
                            {
                                smsData.Status = "Mobile no not valid for Mannual SMS";
                                smsRepo.Add(smsData);
                                smsRepo.Save();
                            }
                        }
                        else
                        {
                            smsData.Status = "Mobile dose not exsist For Mannual SMS";
                            smsRepo.Add(smsData);
                            smsRepo.Save();
                        }
                    }
                    catch (Exception)
                    {
                        smsData.Status = "Send Option is Auto but customer dose not have mobile no!!";
                        smsRepo.Add(smsData);
                        smsRepo.Save();
                    }
                    compRepo.Dispose();
                    custRepo.Dispose();
                    smsRepo.Dispose();
                }
                sRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtbillno_Leave(object sender, EventArgs e)
        {
            try
            {
                GvProductInfo.Rows.Clear();
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
                txtDiscount.Text = "0";
                txtNetAmt.Text = "0";
                if (txtbillno.Text != "")
                    billdata(txtbillno.Text);
                dgvqtydetails.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void txtbillno_TextChanged(object sender, EventArgs e)
        {
            dgvqtydetails.Visible = false;
        }

        private void txtSBPaidAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtSBRemAmt.Text = "";
                if (Convert.ToDecimal(txtSBNetAmt.Text) >= Convert.ToDecimal(txtSBPaidAmt.Text))
                {
                    decimal c = (Convert.ToDecimal(txtSBNetAmt.Text) - Convert.ToDecimal(txtSBPaidAmt.Text));
                    txtSBRemAmt.Text = Convert.ToString(Math.Round(c, 2, MidpointRounding.AwayFromZero));
                }
            }
            catch (Exception)
            { }
        }

        private void txtProductname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtProductname.Text == "")
                {
                    if (dgvqtydetails.Visible == true)
                        dgvqtydetails.Visible = false;
                }
                else
                {
                    string proname = txtProductname.Text.Trim().Split(',')[0];
                    string company = txtProductname.Text.Trim().Split(',')[1];
                    string unit = txtProductname.Text.Trim().Split(',')[2];
                    ProductRepository productrepo = new ProductRepository();
                    ProductInformation productdata = new ProductInformation();
                    if (CommonMethod.commProduct == true)
                        productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                    else
                        productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    taxinclude = Convert.ToString(productdata.TaxInclusive);
                    productrepo.Dispose();
                }
            }
            catch (Exception) { }
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
                decimal tamt = 0, v = 0, damt = 0, vamt = 0, namt = 0;
                tamt = Convert.ToDecimal(txtTotalAmt.Text);
                damt = tamt * Convert.ToDecimal(txtDiscount.Text) / 100;
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
                decimal fnetamt = Convert.ToDecimal(netamt) + Convert.ToDecimal(ramt);
                txtNetAmt.Text = Convert.ToString(Math.Round(fnetamt, 2, MidpointRounding.AwayFromZero));
            }
            catch (Exception)
            { }
        }

        private void txtbillno_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = GvProductInfo;
            }
            catch (Exception)
            { }
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
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
                    this.ActiveControl = btnAdd;
            }
            catch (Exception)
            { }
        }

        private void AdjustPanelDetails(int product_entrytype)
        {
            GvProductInfo.Columns[22].Visible = false;
            GvProductInfo.Columns[23].Visible = false;
            if (product_entrytype == 1 || product_entrytype == 2)  //producttype == 0 && (WithBatchExp == "Y" || onlyBatch == "Y")
            {
                // 1 - krushi with batch and expiry
                pnlIntegratedDtls.Visible = true;
                pnlIntegratedDtls.Width = 384;
                pnlIntegratedDtls.Height = 33;
                pnlIntegratedDtls.Location = new Point(319, 140);
                if (product_entrytype == 2)
                    txtExpiry.Enabled = false;
                GvProductInfo.Columns[21].Visible = true;
                GvProductInfo.Columns[22].Visible = true;
                this.ActiveControl = cmbBatchNo;
            }
            else if (product_entrytype == 4)// without size //matrix
            {
                cmbBatchNo.Enabled = false;
                txtExpiry.Enabled = false;
                pnlIntegratedDtls.Visible = false;
            }
            else if (product_entrytype == 3)  // With Size
            {
                cmbBatchNo.Enabled = false;
                txtExpiry.Enabled = false;
                pnlIntegratedDtls.Visible = true;
                pnlIntegratedDtls.Width = 384;
                pnlIntegratedDtls.Height = 182;
                pnlIntegratedDtls.Location = new Point(319, 140);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnlIntegratedDtls.Visible = false;
        }

        private void txtBatchNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (cmbBatchNo.Text != "" && e.KeyCode == Keys.Enter)
                this.ActiveControl = txtExpiry;
        }

        private void dtpExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pnlIntegratedDtls.Visible = false;
                this.ActiveControl = txtrate;
            }
        }

        private void cmbBatchNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BatchRepository BatchRepo = new BatchRepository();
                Batch BatchData = new Batch();
                BatchData = BatchRepo.GetAll().Where(t => t.BatchNo == cmbBatchNo.Text && t.BillId == Convert.ToString(qdid) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                txtExpiry.Text = BatchData.ExpiryDate.ToString();
                uqty = Convert.ToDecimal(BatchRepo.GetAll().Where(t => t.BatchNo == cmbBatchNo.Text && t.BillId == Convert.ToString(qdid) && t.CompId == CommonMethod.CompId).Sum(t => t.UsedQty));
                txtQuantity.Text = uqty.ToString();
            }
            catch (Exception)
            { }
        }

        private void cmbBatchNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BatchRepository BatchRepo = new BatchRepository();
                Batch BatchData = new Batch();
                BatchData = BatchRepo.GetAll().Where(t => t.BatchNo == cmbBatchNo.Text && t.BillId == Convert.ToString(qdid) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                txtExpiry.Text = BatchData.ExpiryDate.ToString();
                uqty = Convert.ToDecimal(BatchRepo.GetAll().Where(t => t.BatchNo == cmbBatchNo.Text && t.BillId == Convert.ToString(qdid) && t.CompId == CommonMethod.CompId).Sum(t => t.UsedQty));
                txtQuantity.Text = uqty.ToString();
            }
            catch (Exception)
            { }
        }

        private void getSettingDetails()
        {
            SettingRepository setrepo = new SettingRepository();
            Setting setData = setrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (setData != null)
            {
                if (setData.ShowUserDefinedSeries == "Y")
                    userseriesSettings = true;
                else
                    userseriesSettings = false;
            }
            setrepo.Dispose();
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

        private void getProductType(string proname, string company, string unit)
        {
            ProductRepository prodrepo = new ProductRepository();
            ProductInformation productdata = new ProductInformation();
            if (CommonMethod.commProduct == true)
                productdata = prodrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
            else
                productdata = prodrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (productdata != null)
            {
                productentrytype = Convert.ToInt32(productdata.ProductType);  // allinone standard
                if (productdata.ProductType == 0 && productdata.WithBatchAndExpiry == "N" && productdata.WithBatch == "N")
                    productentrytype = 0;
                else if (productdata.ProductType == 0 && productdata.WithBatchAndExpiry == "Y")
                    productentrytype = 1;
                else if (productdata.ProductType == 0 && productdata.WithBatch == "Y") //krushi
                    productentrytype = 2;
                else if (productdata.ProductType == 3 && productdata.WithoutSize == "N")  //clothing
                    productentrytype = 3;
                else if (productdata.ProductType == 3 && productdata.WithoutSize == "Y")  //clothing
                    productentrytype = 4;
                else if (productdata.ProductType == 1) // serialized
                    productentrytype = 5;
                else if (productdata.ProductType == 2) // Assembly
                    productentrytype = 6;
                else if (productdata.ProductType == 4) //kit
                    productentrytype = 7;
                else if (productdata.ProductType == 5) //scrap
                    productentrytype = 8;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (CustDPredata.Count == 0)
                {
                    if (cmbcustomername.Text != "Select")
                    {
                        if (gvSaleReturnDetails.Rows.Count != 0)
                        {
                            if (billtype == 1 || billtype == 2 || billtype == 3)
                                updatemaster();
                            if (deleteid.Count != 0)
                            {
                                for (int x = 0; x < deleteid.Count; x++)
                                {
                                    deleteitemwithstockupdation(deleteid[x]);
                                }
                            }
                            for (int i = 0; i < gvSaleReturnDetails.Rows.Count; i++)
                            {
                                int Rowid = Convert.ToInt32(gvSaleReturnDetails.Rows[i].Cells["PRId"].Value);
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
                                if (billtype == 1 || billtype == 2)
                                    updatestock(i);
                            }
                            if (billtype == 1 || billtype == 2)
                                updateremainigpayment("Cash");
                            MessageBox.Show("Sale Return Updated.", "Success");
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
                            MessageBox.Show("Select Customer Name.", "Warning");
                            this.ActiveControl = cmbcustomername;
                        }
                    }
                }
                else if (CustDPredata.Count > 0)
                {
                    if (Convert.ToDecimal(txtSBRemAmt.Text) >= Convert.ToDecimal(txtNetAmt.Text))
                    {
                        if (cmbcustomername.Text != "Select")
                        {
                            if (gvSaleReturnDetails.Rows.Count != 0)
                            {
                                if (billtype == 1 || billtype == 2 || billtype == 3)
                                    updatemaster();
                                for (int i = 0; i < gvSaleReturnDetails.Rows.Count; i++)
                                {
                                    int Rowid = Convert.ToInt32(gvSaleReturnDetails.Rows[i].Cells["PRId"].Value);
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
                                    if (billtype == 1 || billtype == 2)
                                        updatestock(i);
                                }
                                CustomerBillMasterRepository Custmasterrepo = new CustomerBillMasterRepository();
                                CustomerBillMaster CustBillMaster = new CustomerBillMaster();
                                CustBillMaster.Id = Custmasterrepo.GetAll().Where(t => t.BillNo == txtbillno.Text && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault().Id;

                                CustomerBillDetailRepository Custdetailrepo = new CustomerBillDetailRepository();
                                List<CustomerBillDetail> CustBillDetail = new List<CustomerBillDetail>();
                                CustBillDetail = Custdetailrepo.GetAll().Where(t => t.BillNo == CustBillMaster.Id && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear && t.CustomerBillMaster.CompId == CommonMethod.CompId).ToList();
                                if (CustBillDetail.Count < GvProductInfo.Rows.Count)
                                {
                                    for (int j = CustBillDetail.Count; j < GvProductInfo.Rows.Count; j++)
                                    {
                                        savebilldetail(j, CustBillMaster.Id);
                                        savebillstock(j);
                                    }
                                }
                                try
                                {
                                    if (CustDPredata.Count > 0)
                                    {
                                        foreach (var item in CustDPredata)
                                        {
                                            Custdetailrepo = new CustomerBillDetailRepository();
                                            CustomerBillDetail CustBillDetail1 = new CustomerBillDetail();
                                            CustBillDetail1 = Custdetailrepo.GetAll().Where(t => t.Id == item.Id && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear && t.CustomerBillMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                                            if (CustBillDetail1.Id == item.Id)
                                            {
                                                Custdetailrepo = new CustomerBillDetailRepository();
                                                CustomerBillDetail custddata = Custdetailrepo.GetById(CustBillDetail1.Id);
                                                custddata.Status = "Inactive";
                                                Custdetailrepo.Edit(custddata);
                                                Custdetailrepo.Save();
                                            }
                                        }
                                        Custdetailrepo = new CustomerBillDetailRepository();
                                        decimal dtotalamt = Convert.ToDecimal(Custdetailrepo.GetAll().Where(t => t.BillNo == CustBillMaster.Id && t.Status != "Inactive" && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear && t.CustomerBillMaster.CompId == CommonMethod.CompId).Sum(t => t.TotalAmount));
                                        Custmasterrepo = new CustomerBillMasterRepository();
                                        var billmaster = Custmasterrepo.GetById(CustBillMaster.Id);
                                        billmaster.TotalAmount = dtotalamt;
                                        decimal discamt = dtotalamt * Convert.ToDecimal(billmaster.Discount) / 100;
                                        decimal vatamt = (dtotalamt - discamt) * Convert.ToDecimal(billmaster.Vat) / 100;
                                        billmaster.NetAmount = Math.Round((dtotalamt - discamt + vatamt + Convert.ToDecimal(billmaster.TransportCharges) + Convert.ToDecimal(billmaster.HamaliCharges)), 2, MidpointRounding.AwayFromZero);
                                        billmaster.PaidAmount = Convert.ToDecimal(txtSBPaidAmt.Text);
                                        billmaster.RemainingAmount = Math.Round((dtotalamt - discamt + vatamt + Convert.ToDecimal(billmaster.TransportCharges) + Convert.ToDecimal(billmaster.HamaliCharges)), 2, MidpointRounding.AwayFromZero) - Convert.ToDecimal(billmaster.PaidAmount);
                                        Custmasterrepo.Edit(billmaster);
                                        Custmasterrepo.Save();
                                        updateremainigpayment("Cash");
                                    }
                                }
                                catch (Exception)
                                { }
                                if (billtype == 1 || billtype == 2)
                                    updateremainigpayment("Cash");
                                MessageBox.Show("Sale Return Updated.", "Success");
                                clear();
                                this.ActiveControl = txtbillno;
                                Custmasterrepo.Dispose();
                                Custdetailrepo.Dispose();
                            }
                            else
                                MessageBox.Show("Add Product Information.", "Warning");
                        }
                        else
                        {
                            if (cmbcustomername.Text == "Select")
                            {
                                MessageBox.Show("Select Customer Name.", "Warning");
                                this.ActiveControl = cmbcustomername;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Return amount is greater than remaining amount.", "Warning");
                        txtSBPaidAmt.Enabled = true;
                        this.ActiveControl = txtSBPaidAmt;
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
                if ((MessageBox.Show("Are you sure to Delete this Sale Return ?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    AIOInventorySystem.Data.Model.Stock sdata = new Data.Model.Stock();
                    string productname = "", unit = "", company = "", alternateunit = "";
                    decimal Quantity = 0, newsqty = 0, sqty = 0, pqty = 0, prqty = 0, newrqty = 0, rqty = 0, srqty = 0, dqty = 0, oqty = 0;
                    decimal NetAmount = 0; string Suppliername = "", DeletedBillNo = "";
                    GoodMasterRepository salereturnRepo = new GoodMasterRepository();
                    var salereturndata = salereturnRepo.GetAll().Where(t => t.BillNo == Convert.ToString(custbillno) && t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    if (salereturndata != null)
                    {
                        // add details for history table
                        try
                        {
                            DeletedBillNo = Convert.ToString(salereturndata.GoodReturnNo);
                            Suppliername = Convert.ToString(salereturndata.CustomerName);
                            NetAmount = Convert.ToDecimal(salereturndata.NetAmount);
                            DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                            DeleteHistoryTable historydata = new DeleteHistoryTable();
                            historydata.DeleteType = "Sale Return";
                            historydata.BillNo = DeletedBillNo;
                            historydata.SupplierOrCustomerName = Suppliername;
                            historydata.Amount = Convert.ToDecimal(NetAmount);
                            historydata.CurrentDate = DateTime.Now.Date;
                            historydata.BillDate = Convert.ToDateTime(salereturndata.GoodReturnDate);
                            historydata.CompId = CommonMethod.CompId;
                            historydata.TransactionYear = CommonMethod.TransactionYear;
                            historyRepo.Add(historydata);
                            historyRepo.Save();
                        }
                        catch (Exception) { }

                        int SReturnId = salereturnRepo.GetAll().Where(t => t.BillNo == Convert.ToString(custbillno) && t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && t.Billdate.Value.ToShortDateString() == salereturndata.Billdate.Value.ToShortDateString()).FirstOrDefault().Id;
                        RemainingPaymentRepository RemPaymentRepo = new RemainingPaymentRepository();
                        var remainingData = RemPaymentRepo.GetAll().Where(t => t.BillId == Convert.ToString(custbillno) && t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.Type == "BillReturn" && t.PaidAmt != 0 && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (remainingData != null)
                        {
                            int ReceiptId = RemPaymentRepo.GetAll().Where(t => t.BillId == Convert.ToString(custbillno) && t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.Type == "BillReturn" && t.PaidAmt != 0 && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault().Id;
                            RemainingPaymentRepository remRepo = new RemainingPaymentRepository();
                            RemainingPayment remdata = remRepo.GetById(ReceiptId);
                            remRepo.Remove(remdata);
                            remRepo.Save();

                            decimal decremamt = 0;
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
                                decimal decnetpaidamt = 0;
                                decnetpaidamt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt);
                                item.RemainingAmt = decnetpaidamt + decremamt;
                                decremamt = decnetpaidamt + decremamt;
                                remainingpayrepo2.Edit(item);
                                remainingpayrepo2.Save();
                                i++;
                            }
                        }
                        GoodDetailRepository saledetailRepo = new GoodDetailRepository();
                        List<GoodReturnDetail> SDtdata = saledetailRepo.GetAll().Where(t => t.GoodReturnNo == SReturnId).ToList();
                        if (SDtdata.Count != 0)
                        {
                            foreach (var item in SDtdata)
                            {
                                int detailId = 0; decimal rate = 0; int updateStockId = 0;
                                string batchno = item.BatchNo;
                                DateTime expireDate;
                                decimal BUQty = 0, BPurQty = 0, FreeQty = 0, BSRQty = 0, BRQty = 0, BPRQty = 0, netQty = 0;
                                if (item.Expiry == null || item.Expiry == "")
                                    expireDate = DateTime.Now.Date;
                                else
                                    expireDate = Convert.ToDateTime(item.Expiry);
                                productname = item.ProductId;
                                company = item.CompanyName;
                                unit = item.Unit;
                                alternateunit = item.SaleUnit;
                                detailId = item.Id;
                                Quantity = Convert.ToDecimal(item.Quantity);
                                getProductType(productname, company, unit);
                                if (productentrytype == 1 || productentrytype == 2)
                                {
                                    BatchRepository batchRepo = new BatchRepository();
                                    var batchdata = batchRepo.GetAll().Where(t => t.ProductName == productname && t.ManufactureCompany == company && t.Unit == unit && t.MasterBillNo == custbillno && (t.BatchNo == batchno) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    if (batchdata != null)
                                    {
                                        PurchaseMasterRepository mastRepo1 = new PurchaseMasterRepository();
                                        var purData1 = mastRepo1.GetAll().Where(t => t.Id == batchdata.PorderNo && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                        if (purData1 != null)
                                        {
                                            PurchaseDetailRepository detailrepo1 = new PurchaseDetailRepository();
                                            var purdetaildata1 = detailrepo1.GetAll().Where(t => t.PorderNo == purData1.Id && t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.BatchNo == batchno && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                            if (purdetaildata1 != null)
                                            {
                                                PReturnDetailRepository PRdetailRepo = new PReturnDetailRepository();
                                                var PRdetdata = PRdetailRepo.GetAll().Where(t => t.POID == purdetaildata1.Id && t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.BatchNo == batchno && t.PurchaseReturnMaster.CompId == CommonMethod.CompId && t.PurchaseReturnMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                                if (PRdetdata != null)
                                                    BPRQty = Convert.ToDecimal(PRdetdata.Quantity);
                                                else
                                                    BPRQty = 0;
                                                BPurQty = Convert.ToDecimal(detailrepo1.GetAll().Where(t => t.PorderNo == purData1.Id && t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.BatchNo == batchno && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.Quantity));
                                                FreeQty = Convert.ToDecimal(detailrepo1.GetAll().Where(t => t.PorderNo == purData1.Id && t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.BatchNo == batchno && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.FreeQty));
                                                netQty = (BPurQty + FreeQty) - BPRQty;
                                                CustomerBillMasterRepository custMRepo = new CustomerBillMasterRepository();
                                                var custMData = custMRepo.GetAll().Where(t => t.BillNo == Convert.ToString(custbillno) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                                CustomerBillDetailRepository custDRepo = new CustomerBillDetailRepository();
                                                var custDetData = custDRepo.GetAll().Where(t => t.ProductId == productname && t.CompanyName == company && t.Unit == unit && (t.BatchNo == batchno) && t.CustomerBillMaster.CompId == CommonMethod.CompId && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                                if (custDetData != null)
                                                    BUQty = Convert.ToDecimal(custDRepo.GetAll().Where(t => t.ProductId == productname && t.CompanyName == company && t.Unit == unit && (t.BatchNo == batchno) && t.CustomerBillMaster.CompId == CommonMethod.CompId && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.Quantity));
                                                try
                                                {
                                                    GoodDetailRepository SRRepo = new GoodDetailRepository();
                                                    GoodReturnDetail SRData = new GoodReturnDetail();
                                                    SRData = SRRepo.GetAll().Where(t => t.ProductId == productname && t.CompanyName == company && t.Unit == unit && t.BatchNo == batchno).FirstOrDefault();
                                                    BSRQty = BSRQty + Convert.ToDecimal(SRData.Quantity);
                                                }
                                                catch (Exception)
                                                { }
                                                BRQty = netQty - (BUQty);
                                                if (BRQty != 0)
                                                {
                                                    int pdID = purdetaildata1.Id;
                                                    PurchaseDetail pdata1 = new PurchaseDetail();
                                                    pdata1 = detailrepo1.GetById(pdID);
                                                    pdata1.Status = "Active";
                                                    detailrepo1.Edit(pdata1);
                                                    detailrepo1.Save();
                                                }
                                                else
                                                {
                                                    int pdID = purdetaildata1.Id;
                                                    PurchaseDetail pdata1 = new PurchaseDetail();
                                                    pdata1 = detailrepo1.GetById(pdID);
                                                    pdata1.Status = "Inactive";
                                                    detailrepo1.Edit(pdata1);
                                                    detailrepo1.Save();
                                                }
                                            }
                                        }
                                    }
                                    batchRepo.Dispose();
                                }
                                if (productentrytype == 9)
                                { }
                                else if (productentrytype == 6)
                                { }
                                else
                                {
                                    StockRepository stockRepo = new StockRepository();
                                    var stockdata = stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    if (stockdata != null)
                                    {
                                        newsqty = 0; sqty = 0; pqty = 0; prqty = 0; newrqty = 0; rqty = 0; srqty = 0; dqty = 0; oqty = 0;
                                        sqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().SaleQty);
                                        pqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().PurchaseQty);
                                        rqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().RemQty);
                                        oqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().OpeningQty);
                                        dqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().DeadQty);
                                        prqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().PurReturnQty);
                                        srqty = Convert.ToDecimal(stockRepo.GetAll().Where(t => t.ProductName == Convert.ToString(productname) && t.MfgCompany == Convert.ToString(company) && t.Unit == Convert.ToString(unit) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().SaleReturnQty);
                                        newsqty = srqty - Quantity;//sale return qty
                                        newrqty = oqty + pqty - prqty - sqty + newsqty - dqty;
                                        updateStockId = stockdata.Id;
                                    }
                                    sdata = stockRepo.GetById(stockdata.Id);
                                    if (sdata != null)
                                    {
                                        sdata.ProductId = stockdata.ProductId;
                                        sdata.ProductName = productname;
                                        sdata.MfgCompany = company;
                                        sdata.Unit = unit;
                                        sdata.OpeningQty = stockdata.OpeningQty;
                                        sdata.DeadQty = stockdata.DeadQty;
                                        sdata.PurchaseQty = Convert.ToDecimal(pqty);
                                        sdata.SaleQty = Convert.ToDecimal(sqty);
                                        sdata.RemQty = Convert.ToDecimal(newrqty);
                                        sdata.SaleReturnQty = Convert.ToDecimal(newsqty);
                                        sdata.PurReturnQty = stockdata.PurReturnQty;

                                        SettingRepository settRepo = new SettingRepository();
                                        Setting settData = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        try
                                        {
                                            db.connect();
                                            SqlCommand command = new SqlCommand();
                                            if (settData.StockOn == "Purchase Rate")
                                                command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
                                            else if (settData.StockOn == "Sale Rate")
                                                command = new SqlCommand("SPNewAvgSaleRate", db.Connection);
                                            command.CommandType = CommandType.StoredProcedure;

                                            command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = productname;
                                            command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = company;
                                            command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = unit;
                                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                                            command.Parameters.Add("@CDate", SqlDbType.VarChar).Value = '0';
                                            command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                            SqlDataReader reader = command.ExecuteReader();
                                            DataTable dtRate = new DataTable();
                                            dtRate.Load(reader);
                                            rate = Convert.ToDecimal(dtRate.Rows[0][0].ToString());
                                            db.CloseConnection();
                                            dtRate.Dispose();
                                        }
                                        catch (Exception)
                                        { db.CloseConnection(); }
                                        sdata.ProductRate = Convert.ToDecimal(rate);
                                        sdata.CompId = CommonMethod.CompId;
                                        sdata.TransactionYear = CommonMethod.TransactionYear;
                                        sdata.Id = updateStockId;
                                        stockRepo.Edit(sdata);
                                        stockRepo.Save();
                                    }
                                }
                            }
                        }
                        db.connect();
                        DataTable dt1 = db.GetTable("Delete from GoodReturnDetail where GoodReturnNo='" + SReturnId + "'");
                        DataTable dt = db.GetTable("Delete from GoodReturnMaster where Id='" + SReturnId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                        MessageBox.Show("Record Deleted Sucessfully.", "Success");
                        dt.Dispose();
                        dt1.Dispose();
                        db.CloseConnection();
                        GvProductInfo.Rows.Clear();
                        clear();
                        this.ActiveControl = btnnew;
                        btnDelete.Enabled = false;
                        saledetailRepo.Dispose();
                    }
                }
            }
            catch (Exception) { }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                custbillinfo = this;
                frmSaleBillReturn p = new frmSaleBillReturn();
                p.Visible = false;
                frmSaleReturnList saleretlist = new frmSaleReturnList();
                saleretlist.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void GvProductInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                CustomerBillDetailRepository Custdetailrepo = new CustomerBillDetailRepository();
                CustomerBillDetail custbilldata = new CustomerBillDetail();
                qdid = Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value);
                custbilldata = Custdetailrepo.GetById(qdid);
                if (custbilldata.Status != "Inactive")
                {
                    sbcolIndex = e.RowIndex;
                    txtProductname.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) + "," + Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value) + "," + Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value);
                    txtQuantity.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Quantityg"].Value);
                    cmbUnit.Items.Clear();
                    cmbUnit.DisplayMember = "Text";
                    cmbUnit.ValueMember = "Value";
                    cmbUnit.Items.Add(new { Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value), Value = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value) });
                    if (Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleUnit"].Value) != Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value))
                        cmbUnit.Items.Add(new { Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleUnit"].Value), Value = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleUnit"].Value) });
                    cmbUnit.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleUnit"].Value);
                    txtrate.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Rate"].Value);
                    txtAmount.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Amount"].Value);
                    txtpdiscount.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Disc"].Value);
                    txtpSpcDisc.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SpDisc"].Value);
                    cmbsgst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SGST"].Value);
                    cmbcgst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["CGST"].Value);
                    cmbigst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["IGST"].Value);
                    txtpCess.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Cess"].Value);
                    txtHSNCode.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["HSNCode"].Value);
                    txtpTotalAmt.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["TotalAmount"].Value);
                    string batch = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["BatchNo"].Value);
                    SBexpiryDate = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ExpiryDate"].Value);
                    if (batch.Contains(','))
                    {
                        cmbBatchNo.Items.Clear();
                        string[] batcharray = batch.Split(',');
                        for (int i = 0; i < batcharray.Length; i++)
                        {
                            cmbBatchNo.Items.Add(batcharray[i].ToString());
                        }
                        cmbBatchNo.Text = batcharray[0].ToString();
                    }
                    else
                    {
                        cmbBatchNo.Items.Clear();
                        cmbBatchNo.Items.Add(batch.ToString());
                        cmbBatchNo.Text = batch;
                        txtExpiry.Text = SBexpiryDate;
                    }

                    ProductRepository ProductRepo = new ProductRepository();
                    ProductInformation ProductData = new ProductInformation();
                    if (CommonMethod.commProduct == true)
                        ProductData = ProductRepo.GetAll().Where(t => t.ProductName == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) && t.ManufactureCompany == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value) && t.Uniti == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value)).FirstOrDefault();
                    else
                        ProductData = ProductRepo.GetAll().Where(t => t.ProductName == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) && t.ManufactureCompany == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value) && t.Uniti == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();

                    if (ProductData != null)
                        ProductCategory = ProductData.Category;
                    txtreason.Text = "Sale Return";
                    this.ActiveControl = txtQuantity;
                }
                else
                    qdid = 0;
                Custdetailrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void gvSaleReturnDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GvProductInfo.Columns[e.ColumnIndex].HeaderText == "Remove")
                //if (e.ColumnIndex == 24)
                {
                    if (btnsave.Enabled == true)
                    {
                        if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        {
                            gvSaleReturnDetails.Rows.Remove(gvSaleReturnDetails.Rows[e.RowIndex]);
                            totalamt = 0;
                            totalamt = gvSaleReturnDetails.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["PRTotalAmt"].Value));
                            txtTotalAmt.Text = Convert.ToString(Math.Round(totalamt, 2, MidpointRounding.AwayFromZero));
                        }
                    }
                    else if (gvSaleReturnDetails.Rows.Count != 1 && btnsave.Enabled == false)
                    {
                        if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        {
                            deleteid.Add(Convert.ToInt32(gvSaleReturnDetails.Rows[e.RowIndex].Cells["PRId"].Value));
                            gvSaleReturnDetails.Rows.Remove(gvSaleReturnDetails.Rows[e.RowIndex]);
                            totalamt = 0;
                            totalamt = gvSaleReturnDetails.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["PRTotalAmt"].Value));
                            txtTotalAmt.Text = Convert.ToString(Math.Round(totalamt, 2, MidpointRounding.AwayFromZero));
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }
    }
}