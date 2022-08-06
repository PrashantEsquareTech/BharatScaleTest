using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmSaleBillReturnCustomerWise : Form
    {
        public static frmSaleBillReturnCustomerWise custbillinfo;
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        public decimal totalamt = 0, mvat = 0, mdvat = 0, PRAmt = 0, TotalNetAmt = 0;
        public int srNo, colIndex = -1, sbcolIndex = -1, custmasterid, billtype;
        public string printId, ProductCategory;
        List<int> deleteid = new List<int>();
        List<int> SelectedRows = new List<int>();
        List<CustomerBillDetail> CustDPredata = new List<CustomerBillDetail>();
        ToolTip tooltipbtn = new ToolTip();

        public frmSaleBillReturnCustomerWise()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxbillid();
            customerdata();
            btnUpdate.Enabled = false;
            deleteid.Clear();
            SelectedRows.Clear();
            CustDPredata.Clear();
        }

        public frmSaleBillReturnCustomerWise(int type, string a)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            billtype = type;
            customerdata();
            btnUpdate.Enabled = false;
            deleteid.Clear();
            SelectedRows.Clear();
        }

        public frmSaleBillReturnCustomerWise(string billno)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            btnsave.Enabled = true;
            maxbillid();
            customerdata();
            deleteid.Clear();
            SelectedRows.Clear();
        }

        public void maxbillid()
        {
            try
            {
                GoodMasterRepository SaleRMRepo = new GoodMasterRepository();
                int mid = Convert.ToInt32(SaleRMRepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.GoodReturnNo));
                if (mid.ToString() == "" || mid == 0)
                    srNo = 1;
                else
                    srNo = mid + 1;
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
                pnlMultiSaleReturn.Visible = false;
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
                    if (txtProductname.Text != "" && txtQuantity.Text != "")
                    {
                        int i = dtgvProductInfo.RowCount;
                        string proname = txtProductname.Text.Trim().Split(',')[0];
                        string company = txtProductname.Text.Trim().Split(',')[1];
                        string unit = txtProductname.Text.Trim().Split(',')[2];
                        dtgvProductInfo.Rows.Add();
                        dtgvProductInfo.Rows[i].Cells[0].Value = proname;
                        dtgvProductInfo.Rows[i].Cells[1].Value = company;
                        dtgvProductInfo.Rows[i].Cells[2].Value = unit;
                        dtgvProductInfo.Rows[i].Cells[3].Value = cmbUnit.Text;
                        dtgvProductInfo.Rows[i].Cells[4].Value = Convert.ToDecimal(txtQuantity.Text);
                        dtgvProductInfo.Rows[i].Cells[5].Value = txtBillNos.Text;
                        dtgvProductInfo.Rows[i].Cells[6].Value = txtReason.Text;
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
            customerdata();
            btnsave.Enabled = true;
            btnUpdate.Enabled = false;
            txtProductname.Text = "";
            txtQuantity.Text = "0";
            colIndex = -1;
            sbcolIndex = -1;
            CustDPredata.Clear();
            deleteid.Clear();
            SelectedRows.Clear();
            dtgvBillList.Rows.Clear();
            dtgvProductInfo.Rows.Clear();
            dtgvBillList.Visible = false;
            txtBillNos.Text = "";
            try
            { dtpsrdate.Value = DateTime.Now.Date; }
            catch (Exception)
            { }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            clear();
            this.ActiveControl = cmbcustomername;
        }

        public void updateremainigpayment(string mode, int rowid)
        {
            try
            {
                string bno = dtgvProductInfo.Rows[rowid].Cells[5].Value.ToString();
                CustomerBillMasterRepository BillRepo = new CustomerBillMasterRepository();
                CustomerBillMaster BillData = new CustomerBillMaster();
                BillData = BillRepo.GetAll().Where(t => t.BillNo == bno && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                RemainingPaymentRepository remainingpayrepo = new RemainingPaymentRepository();
                RemainingPaymentRepository payrepo = new RemainingPaymentRepository();
                RemainingPayment remainingdata1 = new RemainingPayment();
                var remainingpay = remainingpayrepo.GetAll().Where(t => t.BillId == bno && t.Type == "BillReturn" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                var SRSubDiffpay = remainingpayrepo.GetAll().Where(t => t.BillId == bno && t.Type == "SRSubDiff" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt);
                RemainingPaymentRepository remainingrepo = new RemainingPaymentRepository();
                RemainingPayment paydata = new RemainingPayment();
                try
                {
                    RemainingPaymentRepository remainingpayrepo2 = new RemainingPaymentRepository();
                    List<RemainingPayment> remainingamt = remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.BillId == bno && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();
                    decimal netamt = Convert.ToDecimal(remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.BillId == bno && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && (t.Type != "Receipt")).Sum(t => t.NetAmt));
                    decimal Paidamt = Convert.ToDecimal(remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.BillId == bno && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt));
                    if (remainingamt.Count != 0)
                        PRAmt = Convert.ToDecimal(netamt - Paidamt);
                    decimal PAmt = Convert.ToDecimal(remainingpayrepo2.GetAll().Where(t => t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.BillId == bno && t.Type != "Receipt" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Sum(t => t.PaidAmt));
                    decimal Pamt = Paidamt - PAmt;
                    remainingpayrepo.Dispose();
                }
                catch (Exception)
                { }
                if (remainingpay != null)
                {
                    if (TotalNetAmt > 0)
                    {
                        if (TotalNetAmt == Convert.ToDecimal(remainingpay.PaidAmt) && SRSubDiffpay == null)
                        {
                            paydata = new RemainingPayment();
                            paydata.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                            paydata.Customername = Convert.ToString(cmbcustomername.Text);
                            if (PRAmt == 0)
                            {
                                paydata.NetAmt = TotalNetAmt;
                                paydata.PaidAmt = TotalNetAmt;
                            }
                            else
                            {
                                if (TotalNetAmt <= PRAmt)
                                    paydata.PaidAmt = TotalNetAmt;
                                else
                                {
                                    paydata.NetAmt = TotalNetAmt - PRAmt;
                                    paydata.PaidAmt = TotalNetAmt;
                                }
                            }

                            paydata.BillId = Convert.ToString(bno);
                            paydata.BillDate = Convert.ToDateTime(BillData.Billdate);
                            paydata.CreateDate = Convert.ToDateTime(BillData.Billdate);
                            paydata.PaymentDate = Convert.ToDateTime(BillData.PaymentDate);
                            paydata.Type = "BillReturn";
                            paydata.PaymentMode = mode;
                            paydata.TransactionYear = CommonMethod.TransactionYear;
                            paydata.CompId = CommonMethod.CompId;
                            paydata.Id = remainingpay.Id;
                            remainingrepo.Edit(paydata);
                            remainingrepo.Save();
                        }
                        else
                        {
                            paydata = new RemainingPayment();
                            paydata.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                            paydata.Customername = Convert.ToString(cmbcustomername.Text);
                            if (PRAmt == 0)
                            {
                                paydata.NetAmt = TotalNetAmt - Convert.ToDecimal(remainingpay.PaidAmt) - Convert.ToDecimal(SRSubDiffpay);
                                paydata.PaidAmt = TotalNetAmt - Convert.ToDecimal(remainingpay.PaidAmt) - Convert.ToDecimal(SRSubDiffpay);
                            }
                            else
                            {
                                if (TotalNetAmt <= PRAmt)
                                    paydata.PaidAmt = TotalNetAmt - Convert.ToDecimal(remainingpay.PaidAmt) - Convert.ToDecimal(SRSubDiffpay);
                                else
                                {
                                    if (TotalNetAmt - Convert.ToDecimal(remainingpay.PaidAmt) <= PRAmt)
                                        paydata.PaidAmt = TotalNetAmt - Convert.ToDecimal(remainingpay.PaidAmt) - Convert.ToDecimal(SRSubDiffpay);
                                    else
                                    {
                                        paydata.NetAmt = TotalNetAmt - Convert.ToDecimal(remainingpay.PaidAmt) - Convert.ToDecimal(SRSubDiffpay) - PRAmt;
                                        paydata.PaidAmt = TotalNetAmt - Convert.ToDecimal(remainingpay.PaidAmt) - Convert.ToDecimal(SRSubDiffpay);
                                    }
                                }
                            }
                            paydata.BillId = Convert.ToString(bno);
                            paydata.BillDate = Convert.ToDateTime(BillData.Billdate);
                            paydata.CreateDate = Convert.ToDateTime(BillData.Billdate);
                            paydata.PaymentDate = Convert.ToDateTime(BillData.PaymentDate);
                            paydata.Type = "SRSubDiff";
                            paydata.PaymentMode = mode;
                            paydata.TransactionYear = CommonMethod.TransactionYear;
                            paydata.CompId = CommonMethod.CompId;
                            remainingrepo.Add(paydata);
                            remainingrepo.Save();
                        }
                    }
                }
                else
                {
                    paydata = new RemainingPayment();
                    paydata.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                    paydata.Customername = Convert.ToString(cmbcustomername.Text);
                    if (PRAmt == 0)
                    {
                        paydata.NetAmt = TotalNetAmt;
                        paydata.PaidAmt = TotalNetAmt;
                    }
                    else
                    {
                        if (TotalNetAmt <= PRAmt)
                            paydata.PaidAmt = TotalNetAmt;
                        else
                        {
                            paydata.NetAmt = TotalNetAmt - PRAmt;
                            paydata.PaidAmt = TotalNetAmt;
                        }
                    }
                    paydata.BillId = Convert.ToString(bno);
                    paydata.BillDate = Convert.ToDateTime(BillData.Billdate);
                    paydata.CreateDate = Convert.ToDateTime(BillData.Billdate);
                    paydata.PaymentDate = Convert.ToDateTime(BillData.PaymentDate);
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

        public void savemaster(int rowid)
        {
            decimal Disc = 0, pdisc = 0, spdisc = 0, pTotalAmt = 0, pAmt1 = 0, SGstAmt = 0, CGstAmt = 0, IGstAmt = 0, cessAmt = 0;
            try
            {
                GoodMasterRepository SaleRMRepo = new GoodMasterRepository();
                GoodReturnMaster srmasterdata = new GoodReturnMaster();
                CustomerBillMasterRepository BillMRepo = new CustomerBillMasterRepository();
                CustomerBillMaster BillMData = new CustomerBillMaster();
                CustomerBillDetailRepository BillDRepo = new CustomerBillDetailRepository();
                CustomerBillDetail BillDData = new CustomerBillDetail();
                GoodMasterRepository GRMRepo = new GoodMasterRepository();
                GoodReturnMaster GRMData = new GoodReturnMaster();

                BillMData = BillMRepo.GetAll().Where(t => t.BillNo == dtgvProductInfo.Rows[rowid].Cells[5].Value.ToString() && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                BillDData = BillDRepo.GetAll().Where(t => t.Id == Convert.ToInt32(dtgvProductInfo.Rows[rowid].Cells[6].Value) && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear && t.CustomerBillMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                GRMData = GRMRepo.GetAll().Where(t => t.BillNo == BillMData.BillNo && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                if (GRMData == null)
                {
                    srmasterdata.GoodReturnNo = Convert.ToInt32(srNo);
                    srmasterdata.GoodReturnDate = dtpsrdate.Value;
                    srmasterdata.BillNo = Convert.ToString(BillMData.BillNo);
                    srmasterdata.Billdate = Convert.ToDateTime(BillMData.Billdate);
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

                    Disc = Convert.ToDecimal(BillMData.Discount);
                    srmasterdata.Discount = Convert.ToDecimal(BillMData.Discount);
                    decimal tamt = 0, damt = 0, namt = 0;
                    tamt = Convert.ToDecimal(BillDData.TotalAmount);
                    damt = tamt * Convert.ToDecimal(BillMData.Discount) / 100;
                    namt = tamt - damt;
                    srmasterdata.Vat = 0;
                    srmasterdata.TotalAmount = Convert.ToDecimal(tamt);
                    srmasterdata.NetAmount = Convert.ToDecimal(namt);
                    srmasterdata.TransactionYear = CommonMethod.TransactionYear;
                    srmasterdata.CompId = CommonMethod.CompId;
                    SaleRMRepo.Add(srmasterdata);
                    SaleRMRepo.Save();
                    printId = Convert.ToString(srmasterdata.GoodReturnNo);
                    custmasterid = Convert.ToInt32(srmasterdata.Id);

                    ///-------------Added record in detail
                    GoodDetailRepository SaleRDRepo = new GoodDetailRepository();
                    GoodReturnDetail srdetaildata = SaleRDRepo.GetAll().Where(t => t.BNID == BillDData.Id && t.GoodReturnMaster.CompId == CommonMethod.CompId && t.GoodReturnMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    if (srdetaildata == null)
                    {
                        GoodReturnDetail srdetaildata1 = new GoodReturnDetail();
                        srdetaildata1.GoodReturnNo = custmasterid;
                        srdetaildata1.ProductId = Convert.ToString(BillDData.ProductId);
                        srdetaildata1.CompanyName = Convert.ToString(BillDData.CompanyName);
                        srdetaildata1.Unit = Convert.ToString(BillDData.Unit);
                        srdetaildata1.SaleUnit = BillDData.SaleUnit.ToString();
                        srdetaildata1.Quantity = Convert.ToDecimal(dtgvProductInfo.Rows[rowid].Cells[4].Value.ToString());
                        pAmt1 = Convert.ToDecimal(BillDData.Rate) * Convert.ToDecimal(dtgvProductInfo.Rows[rowid].Cells[4].Value.ToString());
                        srdetaildata1.Code = Convert.ToString(BillDData.Code);
                        srdetaildata1.Rate = Convert.ToDecimal(BillDData.Rate);
                        srdetaildata1.Amount = Convert.ToDecimal(pAmt1);
                        pdisc = (pAmt1 * Convert.ToDecimal(BillDData.Discount) / 100);
                        srdetaildata1.Discount = Convert.ToDecimal(BillDData.Discount);
                        spdisc = ((pAmt1 - pdisc) * Convert.ToDecimal(BillDData.SpcDisc) / 100);
                        srdetaildata1.SpcDisc = Convert.ToDecimal(BillDData.SpcDisc);
                        srdetaildata1.SGstPercent = Convert.ToDecimal(BillDData.SGstPercent);
                        SGstAmt = (Convert.ToDecimal(BillDData.SGstAmt) / Convert.ToDecimal(dtgvProductInfo.Rows[rowid].Cells[8].Value.ToString())) * Convert.ToDecimal(dtgvBillList.Rows[rowid].Cells[4].Value.ToString());
                        srdetaildata1.SGstAmt = SGstAmt;
                        srdetaildata1.CGstPercent = Convert.ToDecimal(BillDData.CGstPercent);
                        CGstAmt = (Convert.ToDecimal(BillDData.CGstAmt) / Convert.ToDecimal(dtgvProductInfo.Rows[rowid].Cells[8].Value.ToString())) * Convert.ToDecimal(dtgvBillList.Rows[rowid].Cells[4].Value.ToString());
                        srdetaildata1.CGstAmt = CGstAmt;
                        srdetaildata1.IGstPercent = Convert.ToDecimal(BillDData.IGstPercent);
                        IGstAmt = (Convert.ToDecimal(BillDData.IGstAmt) / Convert.ToDecimal(dtgvProductInfo.Rows[rowid].Cells[8].Value.ToString())) * Convert.ToDecimal(dtgvBillList.Rows[rowid].Cells[4].Value.ToString());
                        srdetaildata1.IGstAmt = IGstAmt;
                        srdetaildata1.Cess = Convert.ToDecimal(BillDData.Cess);
                        cessAmt = Convert.ToDecimal(BillDData.CessAmt);
                        srdetaildata1.CessAmt = Convert.ToDecimal(BillDData.CessAmt);
                        srdetaildata1.SaleRate = Convert.ToDecimal(BillDData.SaleRate);
                        srdetaildata1.HSNCode = Convert.ToString(BillDData.HSNCode);
                        pTotalAmt = pAmt1 - pdisc - spdisc + SGstAmt + CGstAmt + IGstAmt + cessAmt;
                        srdetaildata1.TotalAmount = Convert.ToDecimal(pTotalAmt);
                        srdetaildata1.Reason = Convert.ToString(txtReason.Text);
                        srdetaildata1.BNID = Convert.ToInt32(BillDData.Id);
                        SaleRDRepo.Add(srdetaildata1);
                        SaleRDRepo.Save();
                    }
                }
                else
                {
                    GRMData.GoodReturnDate = dtpsrdate.Value;
                    GRMData.BillNo = Convert.ToString(BillMData.BillNo);
                    GRMData.Billdate = Convert.ToDateTime(BillMData.Billdate);
                    try
                    {
                        if (Convert.ToInt32(cmbcustomername.SelectedValue) > 0)
                        {
                            GRMData.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                            GRMData.CustomerName = cmbcustomername.Text;
                        }
                    }
                    catch (Exception)
                    { }

                    Disc = Convert.ToDecimal(BillMData.Discount);
                    GRMData.Discount = Convert.ToDecimal(BillMData.Discount);
                    decimal tamt = 0, damt = 0, namt = 0;
                    tamt = Convert.ToDecimal(BillDData.TotalAmount);
                    damt = tamt * Convert.ToDecimal(BillMData.Discount) / 100;
                    namt = tamt - damt;
                    GRMData.Vat = 0;
                    GRMData.TotalAmount = Convert.ToDecimal(tamt);
                    GRMData.NetAmount = Convert.ToDecimal(namt);
                    GRMData.TransactionYear = CommonMethod.TransactionYear;
                    GRMData.CompId = CommonMethod.CompId;
                    GRMRepo.Edit(GRMData);
                    GRMRepo.Save();
                    printId = Convert.ToString(GRMData.GoodReturnNo);
                    custmasterid = Convert.ToInt32(GRMData.Id);

                    //---------------- Updated record in detail
                    GoodDetailRepository GDRRepo = new GoodDetailRepository();
                    GoodReturnDetail GDRData = new GoodReturnDetail();
                    GDRData = GDRRepo.GetById(Convert.ToInt32(dtgvProductInfo.Rows[rowid].Cells[7].Value.ToString()));
                    if (GDRData != null)
                    {
                        GDRData.GoodReturnNo = custmasterid;
                        GDRData.ProductId = Convert.ToString(BillDData.ProductId);
                        GDRData.CompanyName = Convert.ToString(BillDData.CompanyName);
                        GDRData.Unit = Convert.ToString(BillDData.Unit);
                        GDRData.SaleUnit = BillDData.SaleUnit.ToString();
                        GDRData.Quantity = Convert.ToDecimal(dtgvProductInfo.Rows[rowid].Cells[4].Value.ToString()) + GDRData.Quantity;
                        pAmt1 = Convert.ToDecimal(BillDData.Rate) * (Convert.ToDecimal(GDRData.Quantity));
                        GDRData.Code = Convert.ToString(BillDData.Code);
                        GDRData.Rate = Convert.ToDecimal(BillDData.Rate);
                        GDRData.Amount = Convert.ToDecimal(pAmt1);
                        pdisc = (pAmt1 * Convert.ToDecimal(BillDData.Discount) / 100);
                        GDRData.Discount = Convert.ToDecimal(pdisc);
                        spdisc = ((pAmt1 - pdisc) * Convert.ToDecimal(BillDData.SpcDisc) / 100);
                        GDRData.SpcDisc = Convert.ToDecimal(spdisc);
                        GDRData.SGstPercent = Convert.ToDecimal(BillDData.SGstPercent);
                        SGstAmt = (Convert.ToDecimal(BillDData.SGstAmt) / Convert.ToDecimal(dtgvProductInfo.Rows[rowid].Cells[8].Value)) * Convert.ToDecimal(dtgvBillList.Rows[rowid].Cells[4].Value);
                        GDRData.SGstAmt = Convert.ToDecimal(GDRData.SGstAmt) + SGstAmt;
                        GDRData.CGstPercent = Convert.ToDecimal(BillDData.CGstPercent);
                        CGstAmt = (Convert.ToDecimal(BillDData.CGstAmt) / Convert.ToDecimal(dtgvProductInfo.Rows[rowid].Cells[8].Value)) * Convert.ToDecimal(dtgvBillList.Rows[rowid].Cells[4].Value);
                        GDRData.CGstAmt = Convert.ToDecimal(GDRData.CGstAmt) + CGstAmt;
                        GDRData.IGstPercent = Convert.ToDecimal(BillDData.IGstPercent);
                        IGstAmt = (Convert.ToDecimal(BillDData.IGstAmt) / Convert.ToDecimal(dtgvProductInfo.Rows[rowid].Cells[8].Value)) * Convert.ToDecimal(dtgvBillList.Rows[rowid].Cells[4].Value);
                        GDRData.IGstAmt = Convert.ToDecimal(GDRData.IGstAmt) + IGstAmt;
                        GDRData.Cess = Convert.ToDecimal(BillDData.Cess);
                        cessAmt = Convert.ToDecimal(BillDData.CessAmt);
                        GDRData.CessAmt = Convert.ToDecimal(BillDData.CessAmt);
                        GDRData.SaleRate = Convert.ToDecimal(BillDData.SaleRate);
                        GDRData.HSNCode = Convert.ToString(BillDData.HSNCode);
                        pTotalAmt = pAmt1 - pdisc - spdisc + SGstAmt + CGstAmt + IGstAmt + cessAmt;
                        GDRData.TotalAmount = Convert.ToDecimal(pTotalAmt);
                        GDRData.Reason = Convert.ToString(txtReason.Text);
                        GDRData.BNID = Convert.ToInt32(BillDData.Id);
                        GDRRepo.Edit(GDRData);
                        GDRRepo.Save();
                    }
                }
                SaleRMRepo.Dispose();
            }
            catch (Exception)
            { }

            try
            {
                GoodMasterRepository SaleRMReponew = new GoodMasterRepository();
                GoodReturnMaster srmasterdatanew = new GoodReturnMaster();
                srmasterdatanew = SaleRMReponew.GetById(custmasterid);
                GoodDetailRepository GRDRepo = new GoodDetailRepository();
                GoodReturnDetail GRDData = new GoodReturnDetail();
                decimal STotalAmt = Convert.ToDecimal(GRDRepo.GetAll().Where(t => t.GoodReturnNo == custmasterid && t.GoodReturnMaster.CompId == CommonMethod.CompId && t.GoodReturnMaster.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.TotalAmount));
                TotalNetAmt = STotalAmt - (STotalAmt * Disc / 100);
                srmasterdatanew.TotalAmount = STotalAmt;
                srmasterdatanew.NetAmount = TotalNetAmt;
                SaleRMReponew.Edit(srmasterdatanew);
                SaleRMReponew.Save();
            }
            catch (Exception)
            { }
        }

        public void savestock()
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

                string pname = txtProductname.Text.Trim().Split(',')[0].ToString();
                string company = txtProductname.Text.Trim().Split(',')[1].ToString();
                string unit = txtProductname.Text.Trim().Split(',')[2].ToString();
                ProductInformation productData = new ProductInformation();
                if (CommonMethod.commProduct == true)
                    productData = productrepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                else
                    productData = productrepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                var stockData = stockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (stockData != null)
                {
                    var updateStockData = stockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    int updateID = updateStockData.Id; ;
                    decimal srQty = 0, ramQty = 0, rate = 0, oqty = 0;
                    try
                    {
                        try
                        { oqty = Convert.ToDecimal(updateStockData.OpeningQty); }
                        catch (Exception)
                        { oqty = 0; }
                        srQty = Convert.ToDecimal(SaleRDRepo.GetAll().Where(t => t.ProductId == pname && t.CompanyName == company && t.Unit == unit && t.GoodReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.GoodReturnMaster.CompId == CommonMethod.CompId).Sum(t => t.Quantity));
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

                    try
                    {
                        SettingRepository settingrepo = new SettingRepository();
                        var settingdata = settingrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).LastOrDefault();
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand();
                            if (settingdata.StockOn == "Purchase Rate")
                                command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
                            else if (settingdata.StockOn == "Sale Rate")
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
                            dtRate.Dispose();
                            db.CloseConnection();
                        }
                        catch (Exception)
                        { db.CloseConnection(); }
                    }
                    catch (Exception)
                    { }
                    updateSData.ProductRate = rate;
                    updateSData.SaleReturnQty = srQty;
                    updateSData.PurReturnQty = Convert.ToDecimal(updateStockData.PurReturnQty);
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
                SaleRDRepo.Dispose();
                PRDRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbcustomername.Text != "Select" && txtProductname.Text != "")
                {
                    if (dtgvProductInfo.Rows.Count > 0)
                    {
                        maxbillid();
                        GoodMasterRepository SaleRMRepo = new GoodMasterRepository();
                        var BillId = SaleRMRepo.GetAll().Where(t => t.GoodReturnNo == Convert.ToInt32(srNo) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (BillId != null)
                        {
                            MessageBox.Show("Already Saved This Record.", "Warning");
                            this.ActiveControl = btnnew;
                        }
                        else
                        {
                            for (int i = 0; i < dtgvProductInfo.Rows.Count; i++)
                            {
                                if (Convert.ToDecimal(dtgvProductInfo.Rows[i].Cells[4].Value) != 0)  //return Quantity
                                {
                                    savemaster(i);
                                    if (dtgvProductInfo.Rows[i].Cells[5].Value.ToString().StartsWith("T"))
                                        billtype = 1;
                                    else if (dtgvProductInfo.Rows[i].Cells[5].Value.ToString().StartsWith("E"))
                                        billtype = 2;
                                    if (billtype == 1 || billtype == 2)
                                        updateremainigpayment("Cash", i);
                                }
                            }
                            savestock();
                            MessageBox.Show("Sale Return saved.", "Success");
                            clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select BillNo.", "Warning");
                        this.ActiveControl = dtgvBillList;
                    }
                }
                else
                {
                    if (cmbcustomername.Text == "Select")
                    {
                        MessageBox.Show("Select Customer Name.", "Warning");
                        this.ActiveControl = cmbcustomername;
                    }
                    else if (txtProductname.Text == "")
                    {
                        MessageBox.Show("Select Product Name.", "Warning");
                        this.ActiveControl = txtProductname;
                    }
                    else
                    {
                        MessageBox.Show("Enter Quantity.", "Warning");
                        this.ActiveControl = txtQuantity;
                    }
                }
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
                        CompanyInformation compData = compRepo.GetAll().FirstOrDefault();

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
                                    //  return SmsStatusMsg;
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
                decimal Quantity = Convert.ToDecimal(srdetaildata.Quantity);

                ProductInformation productData = new ProductInformation();
                if (CommonMethod.commProduct == true)
                    productData = productrepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                else
                    productData = productrepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (billtype == 1 || billtype == 2)
                {
                    var stockData = stockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (stockData != null)
                    {
                        var updateStockData = stockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
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
                            srQty = Convert.ToDecimal(SaleRDRepo.GetAll().Where(t => t.ProductId == pname && t.CompanyName == company && t.Unit == unit && t.GoodReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.GoodReturnMaster.CompId == CommonMethod.CompId).Sum(t => t.Quantity));
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

                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
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
                            dtRate.Dispose();
                            db.CloseConnection();
                        }
                        catch (Exception)
                        {
                            db.CloseConnection();
                        }
                        updateSData.ProductRate = rate;
                        updateSData.TransactionYear = CommonMethod.TransactionYear;
                        updateSData.Id = updateID;
                        updateStockRepo.Edit(updateSData);
                        updateStockRepo.Save();
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
                            if (productdata.Category == "Motor")
                            {
                                MessageBox.Show("Please use Sale Return Form for this Product Return.", "Warning");
                                txtProductname.Text = "";
                                this.ActiveControl = txtProductname;
                            }
                            else
                            {
                                cmbUnit.Items.Clear();
                                cmbUnit.DisplayMember = "Text";
                                cmbUnit.ValueMember = "Value";

                                cmbUnit.Items.Add(new { Text = productdata.Uniti, Value = productdata.Uniti });
                                if (productdata.AlternateUnit != null && productdata.AlternateUnit != "")
                                    cmbUnit.Items.Add(new { Text = productdata.AlternateUnit, Value = productdata.AlternateUnit });
                                cmbUnit.SelectedIndex = 0;
                            }

                            ///////////Get Product Details from sale and good returns
                            if (cmbcustomername.SelectedIndex != 0 && txtProductname.Text != "")
                            {
                                dtgvBillList.Rows.Clear();
                                if (proname.Contains("'"))
                                    proname = proname.Replace("'", "''");
                                SqlDataAdapter da = new SqlDataAdapter();
                                if (cmbUnit.Text == unit)
                                    da = new SqlDataAdapter("Select distinct m.BillNo from CustomerBillMaster m inner join CustomerBillDetail d on m.Id=d.BillNo where CustomerId=" + Convert.ToInt32(cmbcustomername.SelectedValue) + " and ProductId='" + proname + "' and CompanyName='" + company + "' and Unit='" + unit + "' and (SaleUnit='" + cmbUnit.Text + "' or SaleUnit is null) and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                                else
                                    da = new SqlDataAdapter("Select distinct m.BillNo from CustomerBillMaster m inner join CustomerBillDetail d on m.Id=d.BillNo where CustomerId=" + Convert.ToInt32(cmbcustomername.SelectedValue) + " and ProductId='" + proname + "' and CompanyName='" + company + "' and Unit='" + unit + "' and SaleUnit='" + cmbUnit.Text + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'", db.Connection);
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        SqlDataAdapter da1 = new SqlDataAdapter();
                                        DataTable dt1 = new DataTable();
                                        if (cmbUnit.Text == unit)
                                        {
                                            da1 = new SqlDataAdapter("SELECT CM.BillNo,CD.Quantity as SaleQty,isnull(GD.Quantity,0) as ReturnQty, " +
                                                                     "(CD.Quantity-isnull(GD.Quantity,0)) as ActualQty, CD.Id as SaleId, GD.Id as ReturnId " +
                                                                     "from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.id=cd.billno " +
                                                                     "left join GoodReturnMaster as gm on cm.billno=gm.billno and cm.compid=gm.compid left join GoodReturnDetail as gd " +
                                                                     "on cd.productid=gd.productid and gd.companyname=cd.companyname and cd.unit=gd.unit and gd.BNID=cd.Id " +
                                                                     "where CM.BillNo ='" + dt.Rows[i][0].ToString() + "' and CD.ProductId='" + proname + "' and CD.CompanyName='" + company + "' and CD.Unit='" + unit + "' and (CD.SaleUnit='" + cmbUnit.Text + "' or CD.SaleUnit is null) " +
                                                                     "and cm.compid='" + CommonMethod.CompId + "' and cm.Transactionyear='" + CommonMethod.TransactionYear + "'", db.Connection);
                                            dt1.Rows.Clear();
                                            da1.Fill(dt1);
                                        }
                                        else
                                        {
                                            //                                  0           1                       2                       
                                            da1 = new SqlDataAdapter("SELECT CM.BillNo,CD.Quantity as SaleQty,isnull(GD.Quantity,0) as ReturnQty, " +
                                                                    "(CD.Quantity-isnull(GD.Quantity,0)) as ActualQty, CD.Id as SaleId, GD.Id as ReturnId " +   // 3,4,5
                                                                    "from CustomerBillMaster as cm join CustomerBillDetail as cd on cm.id=cd.billno " +
                                                                    "left join GoodReturnMaster as gm on cm.billno=gm.billno and cm.compid=gm.compid left join GoodReturnDetail as gd " +
                                                                    "on cd.productid=gd.productid and gd.companyname=cd.companyname and cd.unit=gd.unit and gd.BNID=cd.Id " +
                                                                    "where CM.BillNo ='" + dt.Rows[i][0].ToString() + "' and CD.ProductId='" + proname + "' and CD.CompanyName='" + company + "' and CD.Unit='" + unit + "' and CD.SaleUnit='" + cmbUnit.Text + "'" +
                                                                    "and cm.compid='" + CommonMethod.CompId + "' and cm.Transactionyear='" + CommonMethod.TransactionYear + "'", db.Connection);
                                            dt1.Rows.Clear();
                                            da1.Fill(dt1);
                                        }
                                        if (dt1.Rows.Count > 0)
                                        {
                                            for (int j = 0; j < dt1.Rows.Count; j++)
                                            {
                                                if (Convert.ToDecimal(dt1.Rows[j][3].ToString()) != 0)
                                                {
                                                    int l = dtgvBillList.Rows.Count;
                                                    dtgvBillList.Rows.Add();
                                                    dtgvBillList.Rows[l].Cells[1].Value = dt1.Rows[j][0].ToString();
                                                    dtgvBillList.Rows[l].Cells[2].Value = dt1.Rows[j][1].ToString();
                                                    dtgvBillList.Rows[l].Cells[3].Value = dt1.Rows[j][3].ToString();
                                                    dtgvBillList.Rows[l].Cells[4].Value = 0;
                                                    dtgvBillList.Rows[l].Cells[5].Value = dt1.Rows[j][4].ToString();
                                                    dtgvBillList.Rows[l].Cells[6].Value = dt1.Rows[j][5].ToString();
                                                }
                                            }
                                        }
                                        if (dtgvBillList.Rows.Count > 0)
                                        {
                                            pnlMultiSaleReturn.Visible = true;
                                            dtgvBillList.Visible = true;
                                        }
                                    }
                                }
                            }
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
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnbilllist_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                custbillinfo = this;
                frmSaleBillReturnCustomerWise p = new frmSaleBillReturnCustomerWise();
                p.Visible = false;
                frmSaleReturnList saleretlist = new frmSaleReturnList();
                saleretlist.ShowDialog();
            }
            catch (Exception)
            { }
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

        private void txtNetAmt_KeyPress(object sender, KeyPressEventArgs e)
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

        private void cmbcustomername_Leave(object sender, EventArgs e)
        {
            if (cmbcustomername.Text != "" && cmbcustomername.Text != "Select")
            {
                CommonMethod cm = new CommonMethod();
                if (cm.ValidCustomer(cmbcustomername.Text) == 1)
                { }
                else
                {
                    MessageBox.Show("Invalid Customer.", "Warning");
                    customerdata();
                    this.ActiveControl = cmbcustomername;
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            //------------Nilophar 2102
            try
            {
                if (cmbcustomername.Text != "Select")
                {
                    if (txtProductname.Text != "")
                    {
                        string proname = txtProductname.Text.Trim().Split(',')[0];
                        string company = txtProductname.Text.Trim().Split(',')[1];
                        string unit = txtProductname.Text.Trim().Split(',')[2];
                        for (int ii = 0; ii < dtgvBillList.Rows.Count; ii++)
                        {
                            if (!string.IsNullOrEmpty(dtgvBillList.Rows[ii].Cells[4].Value.ToString()) && Convert.ToDecimal(dtgvBillList.Rows[ii].Cells[4].Value) != 0)
                            {
                                int i = dtgvProductInfo.RowCount;
                                dtgvProductInfo.Rows.Add();
                                dtgvProductInfo.Rows[i].Cells[0].Value = proname;
                                dtgvProductInfo.Rows[i].Cells[1].Value = company;
                                dtgvProductInfo.Rows[i].Cells[2].Value = unit;
                                dtgvProductInfo.Rows[i].Cells[3].Value = cmbUnit.Text;
                                dtgvProductInfo.Rows[i].Cells[4].Value = dtgvBillList.Rows[ii].Cells[4].Value;
                                dtgvProductInfo.Rows[i].Cells[5].Value = dtgvBillList.Rows[ii].Cells[1].Value;
                                dtgvProductInfo.Rows[i].Cells[6].Value = dtgvBillList.Rows[ii].Cells[5].Value;
                                dtgvProductInfo.Rows[i].Cells[7].Value = dtgvBillList.Rows[ii].Cells[6].Value;
                                dtgvProductInfo.Rows[i].Cells[8].Value = dtgvBillList.Rows[ii].Cells[2].Value;
                            }
                        }
                    }
                }
                pnlMultiSaleReturn.Visible = false;
                dtgvBillList.Visible = false;
            }
            catch (Exception)
            { }
            //-------------------------
        }

        private void dtgvBillList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
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
                    if (Convert.ToBoolean(dtgvBillList.Rows[e.RowIndex].Cells[0].Value) == true)
                    {
                        decimal saleqty = 0, enterqty = 0, srqty = 0, remqty1 = 0;
                        saleqty = Convert.ToDecimal(dtgvBillList.Rows[e.RowIndex].Cells[2].Value.ToString());
                        enterqty = Convert.ToDecimal(dtgvBillList.Rows[e.RowIndex].Cells[4].Value.ToString());
                        if (ProductCategory == "Motor")
                        {
                            try
                            {
                                srqty = Convert.ToDecimal(GdetailRepo.GetAll().Where(t => t.ProductId == proname && t.CompanyName == company && t.Unit == unit && t.GoodReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.GoodReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault().Quantity);
                            }
                            catch (Exception)
                            { srqty = 0; }
                            if (srqty == 0)
                            {
                                if (Convert.ToDecimal(txtQuantity.Text) != 1)
                                {
                                    MessageBox.Show("Must be 1 quantity for return.", "Warning");
                                    dtgvBillList.Rows[e.RowIndex].Cells[4].Value = "";
                                    this.ActiveControl = dtgvBillList;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Product Remaining Quantity in Stock is " + remqty, "Warning");
                                dtgvBillList.Rows[e.RowIndex].Cells[4].Value = "";
                                this.ActiveControl = dtgvBillList;
                            }
                        }
                        else
                        {
                            Gdetail.Quantity = GdetailRepo.GetAll().Where(t => t.ProductId == proname && t.CompanyName == company && t.Unit == unit && t.GoodReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.GoodReturnMaster.CompId == CommonMethod.CompId).Sum(t => t.Quantity);
                            remqty1 = saleqty - Convert.ToDecimal(Gdetail.Quantity);
                            if (enterqty <= remqty1)
                            { }
                            else
                            {
                                MessageBox.Show("Product Remaining Quantity in Stock is " + remqty1, "Warning");
                                dtgvBillList.Rows[e.RowIndex].Cells[4].Value = "";
                                this.ActiveControl = dtgvBillList;
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        private void cmbcustomername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    this.ActiveControl = txtProductname;
            }
            catch (Exception) { }
        }

        private void txtProductname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    this.ActiveControl = dtgvBillList;
            }
            catch (Exception) { }
        }

        private void dtpsrdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    this.ActiveControl = cmbcustomername;
            }
            catch (Exception) { }
        }

        private void txtReason_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    this.ActiveControl = btnsave;
            }
            catch (Exception) { }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (CustDPredata.Count == 0)
                {
                    if (cmbcustomername.Text != "Select")
                    {
                        //if (gvSaleReturnDetails.Rows.Count != 0)
                        //{
                        //    if (billtype == 1 || billtype == 2 || billtype == 3)
                        //        updatemaster();
                        //    if (deleteid.Count != 0)
                        //    {
                        //        for (int x = 0; x < deleteid.Count; x++)
                        //        {
                        //            deleteitemwithstockupdation(deleteid[x]);
                        //        }
                        //    }
                        //    for (int i = 0; i < gvSaleReturnDetails.Rows.Count; i++)
                        //    {
                        //        int Rowid = Convert.ToInt32(gvSaleReturnDetails.Rows[i].Cells[9].Value);
                        //        if (Rowid != 0)
                        //        {
                        //            if (billtype == 1 || billtype == 2 || billtype == 3)
                        //                updatedetail(i, Rowid, "update");
                        //        }
                        //        else
                        //        {
                        //            if (billtype == 1 || billtype == 2 || billtype == 3)
                        //                updatedetail(i, Rowid, "save");
                        //        }
                        //        if (billtype == 1 || billtype == 2)
                        //            updatestock(i);
                        //    }

                        //    //Custmasterrepo = new CustomerBillMasterRepository();
                        //    //CustomerBillMaster CustBillMaster = new CustomerBillMaster();
                        //    //CustBillMaster.Id = Custmasterrepo.GetAll().Where(t => t.BillNo == txtbillno.Text).FirstOrDefault().Id;

                        //    //Custdetailrepo = new CustomerBillDetailRepository();
                        //    //List<CustomerBillDetail> CustBillDetail = new List<CustomerBillDetail>();
                        //    //CustBillDetail = Custdetailrepo.GetAll().Where(t => t.BillNo == CustBillMaster.Id).ToList();
                        //    //if (CustBillDetail.Count < GvProductInfo.Rows.Count)
                        //    //{
                        //    //    for (int j = CustBillDetail.Count; j < GvProductInfo.Rows.Count; j++)
                        //    //    {
                        //    //        savebilldetail(j, CustBillMaster.Id);
                        //    //        savebillstock(j);
                        //    //    }
                        //    //}

                        //    //if (Convert.ToDecimal(txtSBNetAmt.Text) != Convert.ToDecimal(txtSBPaidAmt.Text))
                        //    //{
                        //    if (billtype == 1 || billtype == 2)
                        //        updateremainigpayment("Cash");
                        //    //}
                        //    MessageBox.Show("Sale Return Updated.", "Success");
                        //    clear();
                        //}
                        //else
                        //    MessageBox.Show("Add Product Information.", "Warning");
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
                    //if (Convert.ToDecimal(txtSBRemAmt.Text) >= Convert.ToDecimal(txtNetAmt.Text))
                    //{
                    //    if (cmbcustomername.Text != "Select")
                    //    {
                    //        if (gvSaleReturnDetails.Rows.Count != 0)
                    //        {
                    //            if (billtype == 1 || billtype == 2 || billtype == 3)
                    //                updatemaster();
                    //            for (int i = 0; i < gvSaleReturnDetails.Rows.Count; i++)
                    //            {
                    //                int Rowid = Convert.ToInt32(gvSaleReturnDetails.Rows[i].Cells[9].Value);
                    //                if (Rowid != 0)
                    //                {
                    //                    if (billtype == 1 || billtype == 2 || billtype == 3)
                    //                        updatedetail(i, Rowid, "update");
                    //                }
                    //                else
                    //                {
                    //                    if (billtype == 1 || billtype == 2 || billtype == 3)
                    //                        updatedetail(i, Rowid, "save");
                    //                }
                    //                if (billtype == 1 || billtype == 2)
                    //                    updatestock(i);
                    //            }

                    //            CustomerBillMasterRepository Custmasterrepo = new CustomerBillMasterRepository();
                    //            CustomerBillMaster CustBillMaster = new CustomerBillMaster();
                    //            CustBillMaster.Id = Custmasterrepo.GetAll().Where(t => t.BillNo == txtbillno.Text && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().Id;

                    //            CustomerBillDetailRepository Custdetailrepo = new CustomerBillDetailRepository();
                    //            List<CustomerBillDetail> CustBillDetail = new List<CustomerBillDetail>();
                    //            CustBillDetail = Custdetailrepo.GetAll().Where(t => t.BillNo == CustBillMaster.Id && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear).ToList();
                    //            if (CustBillDetail.Count < GvProductInfo.Rows.Count)
                    //            {
                    //                for (int j = CustBillDetail.Count; j < GvProductInfo.Rows.Count; j++)
                    //                {
                    //                    savebilldetail(j, CustBillMaster.Id);
                    //                    savebillstock(j);
                    //                }
                    //            }

                    //            try
                    //            {
                    //                if (CustDPredata.Count > 0)
                    //                {
                    //                    foreach (var item in CustDPredata)
                    //                    {
                    //                        Custdetailrepo = new CustomerBillDetailRepository();
                    //                        CustomerBillDetail CustBillDetail1 = new CustomerBillDetail();
                    //                        CustBillDetail1 = Custdetailrepo.GetAll().Where(t => t.Id == item.Id && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();

                    //                        if (CustBillDetail1.Id == item.Id)
                    //                        {
                    //                            Custdetailrepo = new CustomerBillDetailRepository();
                    //                            CustomerBillDetail custddata = Custdetailrepo.GetById(CustBillDetail1.Id);
                    //                            custddata.Status = "Inactive";
                    //                            Custdetailrepo.Edit(custddata);
                    //                            Custdetailrepo.Save();
                    //                        }
                    //                    }
                    //                    Custdetailrepo = new CustomerBillDetailRepository();
                    //                    decimal dtotalamt = Convert.ToDecimal(Custdetailrepo.GetAll().Where(t => t.BillNo == CustBillMaster.Id && t.Status != "Inactive" && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.TotalAmount));
                    //                    Custmasterrepo = new CustomerBillMasterRepository();
                    //                    var billmaster = Custmasterrepo.GetById(CustBillMaster.Id);
                    //                    billmaster.TotalAmount = dtotalamt;
                    //                    decimal discamt = dtotalamt * Convert.ToDecimal(billmaster.Discount) / 100;
                    //                    decimal vatamt = (dtotalamt - discamt) * Convert.ToDecimal(billmaster.Vat) / 100;
                    //                    billmaster.NetAmount = Math.Round((dtotalamt - discamt + vatamt + Convert.ToDecimal(billmaster.TransportCharges) + Convert.ToDecimal(billmaster.HamaliCharges)), 2, MidpointRounding.AwayFromZero);
                    //                    billmaster.PaidAmount = Convert.ToDecimal(txtSBPaidAmt.Text);
                    //                    billmaster.RemainingAmount = Math.Round((dtotalamt - discamt + vatamt + Convert.ToDecimal(billmaster.TransportCharges) + Convert.ToDecimal(billmaster.HamaliCharges)), 2, MidpointRounding.AwayFromZero) - Convert.ToDecimal(billmaster.PaidAmount);
                    //                    Custmasterrepo.Edit(billmaster);
                    //                    Custmasterrepo.Save();
                    //                    updateremainingpayment("Cash");
                    //                }
                    //            }
                    //            catch (Exception)
                    //            {
                    //            }

                    //            if (billtype == 1 || billtype == 2)
                    //                updateremainigpayment("Cash");
                    //            MessageBox.Show("Sale Return Updated.", "Success");
                    //            clear();
                    //           this.ActiveControl= txtbillno;
                    //            Custmasterrepo.Dispose();
                    //            Custdetailrepo.Dispose();
                    //        }
                    //        else
                    //            MessageBox.Show("Add Product Information.", "Warning");
                    //    }
                    //    else
                    //    {
                    //        if (cmbcustomername.Text == "Select")
                    //        {
                    //            MessageBox.Show("Select Customer Name.", "Warning");
                    //           this.ActiveControl= cmbcustomername;
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Return amount is greater than remaining amount.", "Warning");
                    //    txtSBPaidAmt.Enabled = true;
                    // this.ActiveControl=   txtSBPaidAmt;
                    //}
                }
            }
            catch (Exception)
            { }
        }
    }
}