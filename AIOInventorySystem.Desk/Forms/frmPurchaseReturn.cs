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
    public partial class frmPurchaseReturn : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        public decimal totalamt = 0, mdvat = 0, mvat = 0, rdoff = 1;
        public int purmasterid, printId, pocolIndex = -1, colIndex = -1, StockIn, intamtchk, productentrytype, porderno;
        public string ProductCategory;
        public static frmPurchaseReturn custbillinfo;
        List<int> deleteid = new List<int>();
        List<PurchaseDetail> PurDPredata = new List<PurchaseDetail>();

        public frmPurchaseReturn()
        {
            InitializeComponent();
            maxbillid();
            fillgst();
            customerdata();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            PurDPredata.Clear();
            cm.changedatetimepickerrange(this);
        }

        public frmPurchaseReturn(int id)
        {
            InitializeComponent();
            customerdata();
            fillgst();
            btnsave.Enabled = false;
            btnUpdate.Enabled = true;
            btnDelete.Enabled = false;
            cm.changedatetimepickerrange(this);
            billdata(id);
            PurDPredata.Clear();
        }

        public frmPurchaseReturn(int type, string a)
        {
            InitializeComponent();
            customerdata();
            fillgst();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            PurDPredata.Clear();
            cm.changedatetimepickerrange(this);
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

        public void billdata(int id)
        {
            try
            {
                gvpurreturnproducts.Rows.Clear();
                PReturnMasterRepository PRMRepo = new PReturnMasterRepository();
                var PRMdata = PRMRepo.GetById(id);
                txtPRNo.Text = PRMdata.PurchaseReturnNo.ToString();
                dtpPRDate.Text = PRMdata.PurchaseReturnDate.Value.ToString();
                txtporderno.Text = PRMdata.PorderNo.ToString();
                LoadData(Convert.ToInt32(PRMdata.PorderNo));
                txtTotalAmt.Text = Convert.ToString(PRMdata.TotalAmount);
                txtDiscount.Text = Convert.ToString(PRMdata.Discount);
                txtSpDisc.Text = Convert.ToString(PRMdata.SpcDiscount);
                ttnetamt.Text = Convert.ToString(PRMdata.NetAmount);
                printId = Convert.ToInt32(PRMdata.PurchaseReturnNo);
                var PurchaseDetailData = PRMdata.PurchaseReturnDetails;
                int i = 0;
                foreach (var item in PurchaseDetailData)
                {
                    if (item.AssemblyChildStatus == "N" || item.AssemblyChildStatus == null || item.AssemblyChildStatus == "")
                    {
                        gvpurreturnproducts.Rows.Add();
                        gvpurreturnproducts.Rows[i].Cells["PRIDg"].Value = item.Id;
                        gvpurreturnproducts.Rows[i].Cells["PRProductName"].Value = item.ProductName;
                        gvpurreturnproducts.Rows[i].Cells["PRCompany"].Value = item.CompanyName;
                        gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value = item.Unit;
                        if (item.PurchaseUnit != null)
                            gvpurreturnproducts.Rows[i].Cells["PRPurUnit"].Value = item.PurchaseUnit;
                        else
                            gvpurreturnproducts.Rows[i].Cells["PRPurUnit"].Value = item.Unit;
                        if (item.Unit != item.PurchaseUnit && item.PurchaseUnit != null)
                        {
                            ProductRepository productrepo = new ProductRepository();
                            ProductInformation productdata = new ProductInformation();
                            if (CommonMethod.commProduct == true)
                                productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit).FirstOrDefault();
                            else
                                productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            gvpurreturnproducts.Rows[i].Cells["PRQuantity"].Value = Convert.ToDecimal(item.Quantity) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                            gvpurreturnproducts.Rows[i].Cells["PRFrQty"].Value = Convert.ToDecimal(item.FreeQty) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                        }
                        else
                        {
                            gvpurreturnproducts.Rows[i].Cells["PRQuantity"].Value = Convert.ToDouble(item.Quantity);
                            gvpurreturnproducts.Rows[i].Cells["PRFrQty"].Value = Convert.ToDouble(item.FreeQty);
                        }
                        gvpurreturnproducts.Rows[i].Cells["PRCode"].Value = item.Code;
                        gvpurreturnproducts.Rows[i].Cells["PRPrice"].Value = item.PurchasePrice;
                        gvpurreturnproducts.Rows[i].Cells["PRAmount"].Value = item.Amount;
                        gvpurreturnproducts.Rows[i].Cells["PRDisc"].Value = item.Discount;
                        gvpurreturnproducts.Rows[i].Cells["PRSpDisc"].Value = item.SpcDisc;
                        gvpurreturnproducts.Rows[i].Cells["PRSGST"].Value = item.SGstPercent;
                        gvpurreturnproducts.Rows[i].Cells["PRSGSTAmt"].Value = item.SGstAmt;
                        gvpurreturnproducts.Rows[i].Cells["PRCGST"].Value = item.CGstPercent;
                        gvpurreturnproducts.Rows[i].Cells["PRCGSTAmt"].Value = item.CGstAmt;
                        gvpurreturnproducts.Rows[i].Cells["PRIGST"].Value = item.IGstPercent;
                        gvpurreturnproducts.Rows[i].Cells["PRIGSTAmt"].Value = item.IGstAmt;
                        gvpurreturnproducts.Rows[i].Cells["PRCess"].Value = item.Cess;
                        gvpurreturnproducts.Rows[i].Cells["PRCessAmt"].Value = item.CessAmt;
                        gvpurreturnproducts.Rows[i].Cells["PRTotalAmt"].Value = item.TotalAmount;
                        gvpurreturnproducts.Rows[i].Cells["PRReason"].Value = item.Reason;
                        gvpurreturnproducts.Rows[i].Cells["PRRemove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                        gvpurreturnproducts.Rows[i].Cells["PRPOID"].Value = item.POID;
                        gvpurreturnproducts.Rows[i].Cells["PRBarcode"].Value = item.Barcode;
                        if (item.ThreeLevelDiscount == null)
                            gvpurreturnproducts.Rows[i].Cells["PR3LevelDisc"].Value = "0";
                        else
                            gvpurreturnproducts.Rows[i].Cells["PR3LevelDisc"].Value = item.ThreeLevelDiscount;
                        gvpurreturnproducts.Rows[i].Cells["PRSize"].Value = item.Size;
                        gvpurreturnproducts.Rows[i].Cells["PRDepartment"].Value = item.DeptName;
                        gvpurreturnproducts.Rows[i].Cells["PRCategory"].Value = item.Category;
                        gvpurreturnproducts.Rows[i].Cells["PRColorName"].Value = item.ColorName;
                        gvpurreturnproducts.Rows[i].Cells["PRBarcodePrice"].Value = item.barcodePrice;
                        i++;
                    }
                }
                txtporderno.Text = PRMdata.PorderNo.ToString();
                PRMRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void maxbillid()
        {
            try
            {
                PReturnMasterRepository PRMRepo = new PReturnMasterRepository();
                int maxid = Convert.ToInt32(PRMRepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.PurchaseReturnNo));
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
                cmbsuppliername.SelectedIndex = 0;
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
                decimal gridqty = 0, pogridqty = 0, prgridqty = 0;
                if (cmbsuppliername.Text != "Select" && txtReason.Text != "")
                {
                    if (txtpname.Text != "" && txtQuantity.Text != "")
                    {
                        if (pocolIndex != -1)
                        {
                            int i = gvpurreturnproducts.RowCount;
                            try
                            {
                                string productname = txtpname.Text.Trim().Split(',')[0];
                                string company = txtpname.Text.Trim().Split(',')[1];
                                string unit = txtpname.Text.Trim().Split(',')[2];
                                string NewReason = txtReason.Text;
                                decimal txtqty = 0;
                                int poid = Convert.ToInt32(GvProductInfo.Rows[pocolIndex].Cells["Id"].Value);
                                string size = Convert.ToString(GvProductInfo.Rows[pocolIndex].Cells["Sizeg"].Value);
                                for (int k = 0; k < gvpurreturnproducts.Rows.Count; k++)
                                {
                                    string proname = Convert.ToString(gvpurreturnproducts.Rows[k].Cells["PRProductName"].Value);
                                    string compname = Convert.ToString(gvpurreturnproducts.Rows[k].Cells["PRCompany"].Value);
                                    string unitname = Convert.ToString(gvpurreturnproducts.Rows[k].Cells["PRUnit"].Value);
                                    
                                    int prpoid = Convert.ToInt32(gvpurreturnproducts.Rows[k].Cells["PRPOID"].Value);
                                    string returnReason = Convert.ToString(gvpurreturnproducts.Rows[k].Cells["PRReason"].Value);

                                    if (productname == proname && company == compname && unit == unitname && poid == prpoid && NewReason.Trim() == returnReason.Trim())
                                    {
                                        decimal remqty = 0, ptotalamount = 0, originalpqty = 0, srqty = 0;
                                        //---------------used quantity in batch 
                                        if (productentrytype == 1 || productentrytype == 2)
                                        {
                                            originalpqty = Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["Quantityg"].Value) + Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["FrQty"].Value);
                                            PurchaseMasterRepository PurMRepo = new PurchaseMasterRepository();
                                            PurchaseMaster PurMData = new PurchaseMaster();
                                            PurMData = PurMRepo.GetAll().Where(t => t.PorderNo == Convert.ToInt32(txtporderno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                            if (PurMData != null)
                                            {
                                                BatchRepository BatchRepo = new BatchRepository();
                                                Batch BatchData = new Batch();
                                                decimal Usedqty = Convert.ToDecimal(BatchRepo.GetAll().Where(t => t.PorderNo == PurMData.Id && t.ProductName == proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == txtBatchNo.Text && t.CompId == CommonMethod.CompId).Sum(t => t.UsedQty));
                                                try
                                                {
                                                    BatchRepository batchrpstr = new BatchRepository();
                                                    List<Batch> BData = new List<Batch>();
                                                    BData = batchrpstr.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == txtBatchNo.Text && t.Status == "1" && t.PorderNo == PurMData.Id && t.CompId == CommonMethod.CompId).ToList();
                                                    if (BData != null)
                                                    {
                                                        foreach (var item1 in BData)
                                                        {
                                                            try
                                                            {
                                                                GoodDetailRepository SRRepo = new GoodDetailRepository();
                                                                GoodReturnDetail SRData = new GoodReturnDetail();
                                                                SRData = SRRepo.GetAll().Where(t => t.ProductId == item1.ProductName && t.CompanyName == item1.ManufactureCompany && t.Unit == item1.Unit && t.BNID == Convert.ToInt32(item1.BillId) && t.BatchNo == item1.BatchNo && t.GoodReturnMaster.CompId == CommonMethod.CompId && t.GoodReturnMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                                                srqty = srqty + Convert.ToDecimal(SRData.Quantity);
                                                            }
                                                            catch (Exception)
                                                            { }
                                                        }
                                                    }
                                                }
                                                catch (Exception)
                                                { }
                                                remqty = originalpqty - Usedqty + srqty;
                                            }
                                        }
                                        //-----------------

                                        StockRepository StockRepo = new StockRepository();
                                        PReturnDetailRepository PrdetailRepo = new PReturnDetailRepository();
                                        PurchaseReturnDetail prdetail = new PurchaseReturnDetail();
                                        AIOInventorySystem.Data.Model.Stock StockData = new AIOInventorySystem.Data.Model.Stock();
                                        if (StockIn == 0)
                                        {
                                            StockData = StockRepo.GetAll().Where(t => t.ProductName == productname && t.MfgCompany == company && t.Unit == unit && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                            if (StockData != null)
                                            {
                                                if (remqty != 0)
                                                    remqty += Convert.ToDecimal(StockData.RemQty);  //nilophar 20032018 added on integration
                                                else
                                                    remqty = Convert.ToDecimal(StockData.RemQty);
                                            }
                                        }
                                        else
                                        {
                                            GodownStockRepository GDStockRepo = new GodownStockRepository();
                                            GodownStock GDStockData = new GodownStock();
                                            if (Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["SGST"].Value) != 0 || Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["IGST"].Value) != 0)
                                                GDStockData = GDStockRepo.GetAll().Where(t => t.ProductName == productname && t.MfgCompany == company && t.Unit == unit && t.GoDownId == StockIn && t.Vat == true && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                            else
                                                GDStockData = GDStockRepo.GetAll().Where(t => t.ProductName == productname && t.MfgCompany == company && t.Unit == unit && t.GoDownId == StockIn && t.Vat == false && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                            if (GDStockData != null)
                                            {
                                                if (remqty != 0)
                                                    remqty += Convert.ToDecimal(GDStockData.RemQty);  //nilophar 20032018 added on integration
                                                else
                                                    remqty = Convert.ToDecimal(GDStockData.RemQty);
                                            }
                                        }

                                        PReturnMasterRepository PRMRepo = new PReturnMasterRepository();
                                        var prdataexist = PRMRepo.GetAll().Where(t => t.PurchaseReturnNo == Convert.ToInt32(txtPRNo.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        if (prdataexist != null)
                                        {
                                            int prmid = Convert.ToInt32(PRMRepo.GetAll().Where(t => t.PurchaseReturnNo == Convert.ToInt32(txtPRNo.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault().Id);
                                            PrdetailRepo = new PReturnDetailRepository();
                                            prdetail.Quantity = PrdetailRepo.GetAll().Where(t => t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.PurchaseReturnNo == prmid && t.PurchaseReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseReturnMaster.CompId == CommonMethod.CompId).Sum(t => t.Quantity);
                                            remqty = remqty + Convert.ToDecimal(prdetail.Quantity);
                                        }
                                        if (GvProductInfo.Rows[pocolIndex].Cells["Unit"].Value.ToString() != GvProductInfo.Rows[pocolIndex].Cells["PurUnit"].Value.ToString())
                                        {
                                            ProductRepository productrepo = new ProductRepository();
                                            ProductInformation productdata = new ProductInformation();
                                            if (CommonMethod.commProduct == true)
                                                productdata = productrepo.GetAll().Where(t => t.ProductName == productname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                                            else
                                                productdata = productrepo.GetAll().Where(t => t.ProductName == productname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                            decimal qty, frqty;
                                            qty = Convert.ToDecimal(gvpurreturnproducts.Rows[k].Cells["PRQuantity"].Value) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                            frqty = Convert.ToDecimal(gvpurreturnproducts.Rows[k].Cells["PRFrQty"].Value) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                            txtqty = Convert.ToDecimal(txtQuantity.Text) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                            remqty = remqty - (qty + frqty);
                                            prgridqty = qty + frqty;
                                            qty = Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["Quantityg"].Value) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                            frqty = Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["FrQty"].Value) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                            pogridqty = qty + frqty;
                                        }
                                        else
                                        {
                                            txtqty = Convert.ToDecimal(txtQuantity.Text);
                                            remqty = remqty - Convert.ToDecimal(gvpurreturnproducts.Rows[k].Cells["PRQuantity"].Value) + Convert.ToDecimal(gvpurreturnproducts.Rows[k].Cells["PRFrQty"].Value);
                                            pogridqty = Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["Quantityg"].Value) + Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["FrQty"].Value);
                                            prgridqty = Convert.ToDecimal(gvpurreturnproducts.Rows[k].Cells["PRQuantity"].Value) + Convert.ToDecimal(gvpurreturnproducts.Rows[k].Cells["PRFrQty"].Value);
                                        }
                                        gridqty = Convert.ToDecimal(txtQuantity.Text) + Convert.ToDecimal(gvpurreturnproducts.Rows[k].Cells["PRQuantity"].Value) + Convert.ToDecimal(gvpurreturnproducts.Rows[k].Cells["PRFrQty"].Value);
                                        if (remqty >= txtqty && txtqty <= (pogridqty - prgridqty))
                                        {
                                            ptotalamount = gridqty * Convert.ToDecimal(txtpurchaseprice.Text);
                                            gvpurreturnproducts.Rows[k].Cells["PRProductName"].Value = productname;
                                            gvpurreturnproducts.Rows[k].Cells["PRCompany"].Value = company;
                                            gvpurreturnproducts.Rows[k].Cells["PRUnit"].Value = unit;
                                            gvpurreturnproducts.Rows[k].Cells["PRPurUnit"].Value = cmbUnit.Text;
                                            gvpurreturnproducts.Rows[k].Cells["PRQuantity"].Value = gridqty;
                                            gvpurreturnproducts.Rows[k].Cells["PRFrQty"].Value = "0";
                                            gvpurreturnproducts.Rows[k].Cells["PRCode"].Value = GvProductInfo.Rows[pocolIndex].Cells["Code"].Value;
                                            gvpurreturnproducts.Rows[k].Cells["PRPrice"].Value = txtpurchaseprice.Text;
                                            gvpurreturnproducts.Rows[k].Cells["PRAmount"].Value = Math.Round(ptotalamount, 2, MidpointRounding.AwayFromZero);
                                            decimal pdiscamt = 0, spcdiscamt = 0, othdiscamt = 0, sgstamt = 0, cgstamt = 0, igstamt = 0, taxableamt = 0, totaldiscamt = 0, cessamt = 0;
                                            if (txtpdiscount.Text == "")
                                                gvpurreturnproducts.Rows[k].Cells["PRDisc"].Value = 0;
                                            else
                                            {
                                                gvpurreturnproducts.Rows[k].Cells["PRDisc"].Value = txtpdiscount.Text;
                                                pdiscamt = (ptotalamount * Convert.ToDecimal(txtpdiscount.Text)) / 100;
                                                totaldiscamt = (ptotalamount - pdiscamt);
                                            }
                                            if (txtpSpcDisc.Text == "")
                                                gvpurreturnproducts.Rows[k].Cells["PRSpDisc"].Value = 0;
                                            else
                                            {
                                                gvpurreturnproducts.Rows[k].Cells["PRSpDisc"].Value = txtpSpcDisc.Text;
                                                spcdiscamt = ((totaldiscamt) * Convert.ToDecimal(txtpSpcDisc.Text)) / 100;
                                                totaldiscamt = (totaldiscamt - spcdiscamt);
                                            }
                                            if (txtOthDisc.Text == "")
                                                gvpurreturnproducts.Rows[k].Cells["PR3LevelDisc"].Value = 0;
                                            else
                                            {
                                                gvpurreturnproducts.Rows[k].Cells["PR3LevelDisc"].Value = txtOthDisc.Text;
                                                othdiscamt = ((totaldiscamt) * Convert.ToDecimal(txtOthDisc.Text)) / 100;
                                                totaldiscamt = (totaldiscamt - othdiscamt);
                                            }
                                            if (cmbsgst.Text == "")
                                            {
                                                gvpurreturnproducts.Rows[k].Cells["PRSGST"].Value = 0;
                                                gvpurreturnproducts.Rows[k].Cells["PRSGSTAmt"].Value = 0;
                                            }
                                            else
                                            {
                                                gvpurreturnproducts.Rows[k].Cells["PRSGST"].Value = cmbsgst.Text;
                                                sgstamt = ((totaldiscamt) * (Convert.ToDecimal(cmbsgst.Text))) / 100;
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
                                                cgstamt = ((totaldiscamt) * (Convert.ToDecimal(cmbcgst.Text))) / 100;
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
                                                igstamt = ((totaldiscamt) * (Convert.ToDecimal(cmbigst.Text))) / 100;
                                                gvpurreturnproducts.Rows[k].Cells["PRIGSTAmt"].Value = igstamt;
                                                taxableamt = totaldiscamt + sgstamt + cgstamt + igstamt;
                                            }
                                            if (txtpCess.Text == "")
                                            {
                                                gvpurreturnproducts.Rows[k].Cells["PRCess"].Value = 0;
                                                gvpurreturnproducts.Rows[k].Cells["PRCessAmt"].Value = 0;
                                            }
                                            else
                                            {
                                                gvpurreturnproducts.Rows[k].Cells["PRCess"].Value = txtpCess.Text;
                                                cessamt = ((totaldiscamt) * (Convert.ToDecimal(txtpCess.Text))) / 100;
                                                gvpurreturnproducts.Rows[k].Cells["PRCessAmt"].Value = cessamt;
                                            }
                                            gvpurreturnproducts.Rows[k].Cells["PRTotalAmt"].Value = Convert.ToDecimal(gvpurreturnproducts.Rows[k].Cells["PRTotalAmt"].Value) + Convert.ToDecimal(txtpTotalAmt.Text);
                                            gvpurreturnproducts.Rows[k].Cells["PRReason"].Value = txtReason.Text;
                                            gvpurreturnproducts.Rows[k].Cells["PRRemove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                            gvpurreturnproducts.Rows[k].Cells["PRPOID"].Value = GvProductInfo.Rows[pocolIndex].Cells["Id"].Value;
                                            gvpurreturnproducts.Rows[k].Cells["PRBarcode"].Value = GvProductInfo.Rows[pocolIndex].Cells["Barcode"].Value;
                                            if (productentrytype == 1 || productentrytype == 2)  // with batch and expiry and With batch only
                                            {
                                                gvpurreturnproducts.Rows[k].Cells["PRBatchNo"].Value = txtBatchNo.Text;
                                                if (productentrytype != 2)
                                                    gvpurreturnproducts.Rows[k].Cells["PRExpiryDate"].Value = dtpExpiry.Value;
                                            }
                                            gvpurreturnproducts.Rows[k].Cells["PRSize"].Value = GvProductInfo.Rows[pocolIndex].Cells["Sizeg"].Value;
                                            gvpurreturnproducts.Rows[k].Cells["PRDepartment"].Value = GvProductInfo.Rows[pocolIndex].Cells["Department"].Value;
                                            gvpurreturnproducts.Rows[k].Cells["PRCategory"].Value = GvProductInfo.Rows[pocolIndex].Cells["Category"].Value;
                                            gvpurreturnproducts.Rows[k].Cells["PRColorName"].Value = GvProductInfo.Rows[pocolIndex].Cells["ColorName"].Value;
                                            gvpurreturnproducts.Rows[k].Cells["PRBarcodePrice"].Value = GvProductInfo.Rows[pocolIndex].Cells["BarcodePrice"].Value;
                                            totalamt = 0;
                                            totalamt = gvpurreturnproducts.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["PRTotalAmt"].Value));
                                            txtTotalAmt.Text = Convert.ToString(totalamt);
                                            ClearAdd();
                                            PRMRepo.Dispose(); StockRepo.Dispose(); PrdetailRepo.Dispose();
                                            goto a;
                                        }
                                        else
                                        {
                                            MessageBox.Show("You already add this product in Product Return Grid.", "Warning");
                                            goto a;
                                        }
                                    }
                                }
                            b:
                                {
                                    decimal remqty = 0;
                                    if (StockIn == 0)
                                    {
                                        StockRepository StockRepo = new StockRepository();
                                        AIOInventorySystem.Data.Model.Stock StockData = new AIOInventorySystem.Data.Model.Stock();
                                        StockData = StockRepo.GetAll().Where(t => t.ProductName == productname && t.MfgCompany == company && t.Unit == unit && t.Size == size && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        if (StockData != null)
                                            remqty = Convert.ToDecimal(StockData.RemQty);
                                    }
                                    else
                                    {
                                        GodownStockRepository StockRepo = new GodownStockRepository();
                                        GodownStock StockData = new GodownStock();
                                        if (Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["SGST"].Value) != 0 || Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["IGST"].Value) != 0)
                                            StockData = StockRepo.GetAll().Where(t => t.ProductName == productname && t.MfgCompany == company && t.Unit == unit && t.GoDownId == StockIn && t.Vat == true && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        else
                                            StockData = StockRepo.GetAll().Where(t => t.ProductName == productname && t.MfgCompany == company && t.Unit == unit && t.GoDownId == StockIn && t.Vat == false && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        if (StockData != null)
                                            remqty = Convert.ToDecimal(StockData.RemQty);
                                    }
                                    //------ check for batch data
                                    decimal originalpqty = 0, srqty = 0, Usedqty = 0;
                                    try
                                    {
                                        PurchaseMasterRepository PurMRepo = new PurchaseMasterRepository();
                                        PurchaseMaster PurMData = new PurchaseMaster();
                                        PurMData = PurMRepo.GetAll().Where(t => t.PorderNo == Convert.ToInt32(txtporderno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        if (PurMData != null)
                                        {
                                            BatchRepository BatchRepo = new BatchRepository();
                                            Batch BatchData = new Batch();
                                            Usedqty = Convert.ToDecimal(BatchRepo.GetAll().Where(t => t.PorderNo == PurMData.Id && t.ProductName == productname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == txtBatchNo.Text && t.CompId == CommonMethod.CompId).Sum(t => t.UsedQty));
                                            try
                                            {
                                                BatchRepository batchrpstr = new BatchRepository();
                                                List<Batch> BData = new List<Batch>();
                                                BData = batchrpstr.GetAll().Where(t => t.ProductName == productname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == txtBatchNo.Text && t.Status == "1" && t.PorderNo == PurMData.Id && t.CompId == CommonMethod.CompId).ToList();
                                                if (BData != null)
                                                {
                                                    foreach (var item1 in BData)
                                                    {
                                                        try
                                                        {
                                                            GoodDetailRepository SRRepo = new GoodDetailRepository();
                                                            GoodReturnDetail SRData = new GoodReturnDetail();
                                                            SRData = SRRepo.GetAll().Where(t => t.ProductId == item1.ProductName && t.CompanyName == item1.ManufactureCompany && t.Unit == item1.Unit && t.BNID == Convert.ToInt32(item1.BillId) && t.BatchNo == item1.BatchNo && t.GoodReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.GoodReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                                                            srqty = srqty + Convert.ToDecimal(SRData.Quantity);
                                                        }
                                                        catch (Exception)
                                                        { }
                                                    }
                                                }
                                            }
                                            catch (Exception)
                                            { }
                                        }
                                    }
                                    catch (Exception)
                                    { }
                                    //------------------------------

                                    decimal qty = 0, frqty = 0, poqty = 0, pofrqty = 0;
                                    if (GvProductInfo.Rows[pocolIndex].Cells["Unit"].Value.ToString() != GvProductInfo.Rows[pocolIndex].Cells["PurUnit"].Value.ToString())
                                    {
                                        ProductRepository productrepo = new ProductRepository();
                                        ProductInformation productdata = new ProductInformation();
                                        if (CommonMethod.commProduct == true)
                                            productdata = productrepo.GetAll().Where(t => t.ProductName == productname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                                        else
                                            productdata = productrepo.GetAll().Where(t => t.ProductName == productname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        originalpqty = Convert.ToDecimal(Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["Quantityg"].Value) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty))) + Convert.ToDecimal(Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["FrQty"].Value) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty)));
                                        remqty = originalpqty - Usedqty + srqty;
                                        qty = Convert.ToDecimal(txtQuantity.Text) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                        frqty = Convert.ToDecimal(txtFreeQty.Text) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                        poqty = Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["Quantityg"].Value) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                        pofrqty = Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["FrQty"].Value) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                    }
                                    else
                                    {
                                        originalpqty = Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["Quantityg"].Value) + Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["FrQty"].Value);
                                        remqty = originalpqty - Usedqty + srqty;
                                        qty = Convert.ToDecimal(txtQuantity.Text);
                                        frqty = Convert.ToDecimal(txtFreeQty.Text);
                                        poqty = Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["Quantityg"].Value);
                                        pofrqty = Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["FrQty"].Value);
                                    }
                                    if (remqty >= qty + frqty && (qty + frqty) <= (poqty + pofrqty))
                                    {
                                        gvpurreturnproducts.Rows.Add();
                                        gvpurreturnproducts.Rows[i].Cells["PRProductName"].Value = productname;
                                        gvpurreturnproducts.Rows[i].Cells["PRCompany"].Value = company;
                                        gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value = unit;
                                        gvpurreturnproducts.Rows[i].Cells["PRPurUnit"].Value = cmbUnit.Text;
                                        gvpurreturnproducts.Rows[i].Cells["PRQuantity"].Value = txtQuantity.Text;
                                        gvpurreturnproducts.Rows[i].Cells["PRFrQty"].Value = txtFreeQty.Text;
                                        gvpurreturnproducts.Rows[i].Cells["PRCode"].Value = GvProductInfo.Rows[pocolIndex].Cells["Code"].Value;
                                        gvpurreturnproducts.Rows[i].Cells["PRPrice"].Value = txtpurchaseprice.Text;
                                        gvpurreturnproducts.Rows[i].Cells["PRAmount"].Value = txtamount.Text;
                                        decimal amt = Convert.ToDecimal(txtamount.Text);
                                        decimal pdiscamt = 0, spdicamt = 0, othdicamt = 0, cgstamt = 0, sgstamt = 0, igstamt = 0, taxableamt = 0, totaldiscamt = 0, cessamt = 0;
                                        if (txtpdiscount.Text == "")
                                            gvpurreturnproducts.Rows[i].Cells["PRDisc"].Value = 0;
                                        else
                                        {
                                            gvpurreturnproducts.Rows[i].Cells["PRDisc"].Value = txtpdiscount.Text;
                                            pdiscamt = amt * Convert.ToDecimal(txtpdiscount.Text) / 100;
                                            totaldiscamt = (amt - pdiscamt);
                                        }
                                        if (txtpSpcDisc.Text == "")
                                            gvpurreturnproducts.Rows[i].Cells["PRSpDisc"].Value = 0;
                                        else
                                        {
                                            gvpurreturnproducts.Rows[i].Cells["PRSpDisc"].Value = txtpSpcDisc.Text;
                                            spdicamt = (totaldiscamt) * Convert.ToDecimal(txtpSpcDisc.Text) / 100;
                                            totaldiscamt = (totaldiscamt - spdicamt);
                                        }
                                        if (txtOthDisc.Text == "")
                                            gvpurreturnproducts.Rows[i].Cells["PR3LevelDisc"].Value = 0;
                                        else
                                        {
                                            gvpurreturnproducts.Rows[i].Cells["PR3LevelDisc"].Value = txtOthDisc.Text;
                                            othdicamt = (totaldiscamt) * Convert.ToDecimal(txtOthDisc.Text) / 100;
                                            totaldiscamt = (totaldiscamt - othdicamt);
                                        }
                                        if (cmbsgst.Text == "")
                                        {
                                            gvpurreturnproducts.Rows[i].Cells["PRSGST"].Value = 0;
                                            gvpurreturnproducts.Rows[i].Cells["PRSGSTAmt"].Value = 0;
                                        }
                                        else
                                        {
                                            gvpurreturnproducts.Rows[i].Cells["PRSGST"].Value = cmbcgst.Text;
                                            sgstamt = (totaldiscamt) * Convert.ToDecimal(cmbcgst.Text) / 100;
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
                                            cgstamt = (totaldiscamt) * Convert.ToDecimal(cmbcgst.Text) / 100;
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
                                            igstamt = (totaldiscamt) * Convert.ToDecimal(cmbigst.Text) / 100;
                                            gvpurreturnproducts.Rows[i].Cells["PRIGSTAmt"].Value = igstamt;
                                            taxableamt = totaldiscamt + sgstamt + cgstamt + igstamt;
                                        }
                                        if (txtpCess.Text == "")
                                        {
                                            gvpurreturnproducts.Rows[i].Cells["PRCess"].Value = 0;
                                            gvpurreturnproducts.Rows[i].Cells["PRCessAmt"].Value = 0;
                                        }
                                        else
                                        {
                                            gvpurreturnproducts.Rows[i].Cells["PRCess"].Value = txtpCess.Text;
                                            cessamt = (totaldiscamt) * Convert.ToDecimal(txtpCess.Text) / 100;
                                            gvpurreturnproducts.Rows[i].Cells["PRCessAmt"].Value = cessamt;
                                        }
                                        gvpurreturnproducts.Rows[i].Cells["PRTotalAmt"].Value = txtpTotalAmt.Text;
                                        gvpurreturnproducts.Rows[i].Cells["PRReason"].Value = txtReason.Text;
                                        gvpurreturnproducts.Rows[i].Cells["PRRemove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                        gvpurreturnproducts.Rows[i].Cells["PRPOID"].Value = GvProductInfo.Rows[pocolIndex].Cells["Id"].Value;
                                        gvpurreturnproducts.Rows[i].Cells["PRBatchNo"].Value = txtBatchNo.Text;
                                        gvpurreturnproducts.Rows[i].Cells["PRExpiryDate"].Value = GvProductInfo.Rows[pocolIndex].Cells["ExpiryDate"].Value;
                                        gvpurreturnproducts.Rows[i].Cells["PRBarcode"].Value = GvProductInfo.Rows[pocolIndex].Cells["Barcode"].Value;
                                        gvpurreturnproducts.Rows[i].Cells["PRSize"].Value = GvProductInfo.Rows[pocolIndex].Cells["Sizeg"].Value;
                                        gvpurreturnproducts.Rows[i].Cells["PRDepartment"].Value = GvProductInfo.Rows[pocolIndex].Cells["Department"].Value;
                                        gvpurreturnproducts.Rows[i].Cells["PRCategory"].Value = GvProductInfo.Rows[pocolIndex].Cells["Category"].Value;
                                        gvpurreturnproducts.Rows[i].Cells["PRColorName"].Value = GvProductInfo.Rows[pocolIndex].Cells["ColorName"].Value;
                                        gvpurreturnproducts.Rows[i].Cells["PRBarcodePrice"].Value = GvProductInfo.Rows[pocolIndex].Cells["BarcodePrice"].Value;
                                        totalamt = 0;
                                        totalamt = gvpurreturnproducts.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["PRTotalAmt"].Value));
                                        txtTotalAmt.Text = Convert.ToString(totalamt);
                                        ClearAdd();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Product Quantity less than purchase Quantity.", "Warning");
                                        this.ActiveControl = txtQuantity;
                                    }
                                }
                            a:
                                { }
                            }
                            catch (Exception)
                            { }
                        }
                    }
                    else
                    {
                        if (txtpname.Text == "")
                        {
                            MessageBox.Show("Enter product Name.", "Warning");
                            this.ActiveControl = txtpname;
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
                    if (cmbsuppliername.Text == "Select")
                    {
                        MessageBox.Show("Enter Supplier name.", "Warning");
                        this.ActiveControl = cmbsuppliername;
                    }
                    else if (txtReason.Text == "")
                    {
                        MessageBox.Show("Enter Reason.", "Warning");
                        this.ActiveControl = cmbsuppliername;
                    }
                }
            }
            catch (Exception)
            { }
        }

        public void ClearAdd()
        {
            txtpname.Text = "";
            cmbUnit.Items.Clear();
            txtQuantity.Text = "";
            txtHSN.Text = "";
            txtamount.Text = "";
            txtpurchaseprice.Text = "0";
            txtpdiscount.Text = "0";
            txtpSpcDisc.Text = "0";
            fillgst();
            txtpCess.Text = "0";
            txtpTotalAmt.Text = "0";
            txtOthDisc.Text = "0";
            txtReason.Text = "";
            txtBatchNo.Text = "";
            dtpExpiry.Value = DateTime.Now.Date;
            totalamt = 0;
        }

        public void clear()
        {
            try
            {
                mvat = 0; mdvat = 0;
                maxbillid();
                txtPOPaidAmt.Enabled = false;
                txtFreeQty.Enabled = false;
                txtpurchaseprice.Enabled = false;
                txtamount.Enabled = false;
                txtpdiscount.Enabled = false;
                cmbcgst.Enabled = false;
                txtpTotalAmt.Enabled = false;
                btnsave.Enabled = true;
                btnUpdate.Enabled = false;
                txtpurchaseprice.ReadOnly = true;
                txtpdiscount.ReadOnly = true;
                txtPOtotalamt.Text = "0";
                txtPODisc.Text = "0";
                txtPOTransChrg.Text = "0";
                txtPOPaidAmt.Text = "0";
                txtPORemAmt.Text = "0";
                txtpname.Text = "";
                cmbUnit.Items.Clear();
                txtQuantity.Text = "0";
                txtDiscount.Text = "0";
                customerdata();
                fillgst();
                txtpdiscount.Text = "0";
                txtTotalAmt.Text = "0";
                GvProductInfo.Rows.Clear();
                gvpurreturnproducts.Rows.Clear();
                ttnetamt.Text = "0";
                txtOthDisc.Text = "0";
                btnsave.Text = "Save";
                txtporderno.Text = "";
                txtsupplierpono.Text = "";
                txtpurchaseprice.Text = "";
                txtpTotalAmt.Text = "";
                txtamount.Text = "0";
                colIndex = -1;
                PurDPredata.Clear();
                pnlIntegratedDtls.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            clear();
            this.ActiveControl = txtporderno;
        }

        public void savemaster()
        {
            try
            {
                PReturnMasterRepository PRMRepo = new PReturnMasterRepository();
                PurchaseReturnMaster PRMData = new PurchaseReturnMaster();
                maxbillid();
                PRMData.PurchaseReturnNo = Convert.ToInt32(txtPRNo.Text);
                PRMData.PurchaseReturnDate = dtpPRDate.Value;
                PRMData.PorderNo = Convert.ToInt32(txtporderno.Text);
                PRMData.PorderDate = dtpPorderdate.Value;
                PRMData.SupplierpoNo = Convert.ToString(txtsupplierpono.Text);
                PRMData.SupplierId = Convert.ToInt32(cmbsuppliername.SelectedValue);
                PRMData.Suppliername = cmbsuppliername.Text;
                PRMData.TotalAmount = Convert.ToDecimal(txtTotalAmt.Text);
                PRMData.Vat = Convert.ToDecimal(0);
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
                PRMRepo.Add(PRMData);
                PRMRepo.Save();
                printId = Convert.ToInt32(PRMData.PurchaseReturnNo);
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
                PurchaseReturnDetail PRDData = new PurchaseReturnDetail();
                PReturnDetailRepository PRDRepo = new PReturnDetailRepository();
                PRDData.PurchaseReturnNo = purmasterid;
                PRDData.ProductName = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRProductName"].Value);
                PRDData.CompanyName = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRCompany"].Value);
                PRDData.Unit = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value);
                PRDData.PurchaseUnit = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRPurUnit"].Value);
                if (Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value) != Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRPurUnit"].Value))
                {
                    ProductRepository productrepo = new ProductRepository();
                    ProductInformation productdata = new ProductInformation();
                    if (CommonMethod.commProduct == true)
                        productdata = productrepo.GetAll().Where(t => t.ProductName == Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRProductName"].Value) && t.ManufactureCompany == Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRCompany"].Value) && t.Uniti == Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value)).FirstOrDefault();
                    else
                        productdata = productrepo.GetAll().Where(t => t.ProductName == Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRProductName"].Value) && t.ManufactureCompany == Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRCompany"].Value) && t.Uniti == Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    PRDData.Quantity = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRQuantity"].Value) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                    PRDData.FreeQty = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRFrQty"].Value) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                }
                else
                {
                    PRDData.Quantity = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRQuantity"].Value);
                    PRDData.FreeQty = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRFrQty"].Value);
                }
                PRDData.Code = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRCode"].Value);
                PRDData.PurchasePrice = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRPrice"].Value);
                PRDData.Amount = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRAmount"].Value);
                PRDData.Discount = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRDisc"].Value);
                PRDData.SpcDisc = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRSpDisc"].Value);
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
                PRDData.BatchNo = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRBatchNo"].Value);
                if (gvpurreturnproducts.Rows[i].Cells["PRExpiryDate"].Value == "" || gvpurreturnproducts.Rows[i].Cells["PRExpiryDate"].Value == null)
                    PRDData.ExpireDate = null;
                else
                    PRDData.ExpireDate = Convert.ToDateTime(gvpurreturnproducts.Rows[i].Cells["PRExpiryDate"].Value);
                PRDData.Barcode = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRBarcode"].Value);
                PRDData.Size = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRSize"].Value);
                PRDData.DeptName = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRDepartment"].Value);
                PRDData.Category = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRCategory"].Value);
                PRDData.ColorName = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRColorName"].Value);
                PRDData.barcodePrice = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRBarcodePrice"].Value);
                if (gvpurreturnproducts.Rows[i].Cells["PR3LevelDisc"].Value == null)
                    PRDData.ThreeLevelDiscount = 0;
                else
                    PRDData.ThreeLevelDiscount = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PR3LevelDisc"].Value);
                PRDData.AssemblyChildStatus = "N";
                PRDRepo.Add(PRDData);
                PRDRepo.Save();
                PRDRepo.Dispose();
                decimal assemblyQty = Convert.ToDecimal(PRDData.Quantity);
                int BDetailId = Convert.ToInt32(PRDData.POID);
                string Reason = Convert.ToString(PRDData.Reason);
                decimal SGSTPercent = 0, CGSTPercent = 0, IGSTPercent = 0;
                SGSTPercent = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRSGST"].Value);
                CGSTPercent = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRCGST"].Value);
                IGSTPercent = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRIGST"].Value);
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
                                PurchaseReturnDetail PRDData1 = new PurchaseReturnDetail();
                                PReturnDetailRepository PRDRepo1 = new PReturnDetailRepository();
                                PRDData1.PurchaseReturnNo = purmasterid;
                                PRDData1.ProductName = Convert.ToString(itemAssembly.ProductName);
                                PRDData1.CompanyName = Convert.ToString(itemAssembly.CompanyName);
                                PRDData1.Unit = Convert.ToString(itemAssembly.Unit);
                                PRDData1.PurchaseUnit = Convert.ToString(itemAssembly.Unit);
                                decimal childQty = 0, calculateChildQty = 0;
                                childQty = Convert.ToDecimal(itemAssembly.Qty);
                                calculateChildQty = childQty * assemblyQty;
                                PRDData1.Quantity = Convert.ToDecimal(calculateChildQty);
                                PRDData1.Code = Convert.ToString("");
                                PRDData1.PurchasePrice = Convert.ToDecimal(0);
                                PRDData1.Amount = Convert.ToDecimal(0);
                                PRDData1.Discount = Convert.ToDecimal(0);
                                PRDData1.SpcDisc = Convert.ToDecimal(0);
                                PRDData1.SGstPercent = Convert.ToDecimal(SGSTPercent);
                                PRDData1.SGstAmt = Convert.ToDecimal(0);
                                PRDData1.CGstPercent = Convert.ToDecimal(CGSTPercent);
                                PRDData1.CGstAmt = Convert.ToDecimal(0);
                                PRDData1.IGstPercent = Convert.ToDecimal(IGSTPercent);
                                PRDData1.IGstAmt = Convert.ToDecimal(0);
                                PRDData1.Cess = Convert.ToDecimal(0);
                                PRDData1.CessAmt = Convert.ToDecimal(0);
                                PRDData1.TotalAmount = Convert.ToDecimal(0);
                                PRDData1.ThreeLevelDiscount = 0;
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
                                PRDData1.BatchNo = "";
                                PRDData1.ExpireDate = DateTime.Now.Date;
                                PRDData1.Barcode = "";
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
                string pname = "", cname = "", uname = "", batchno1 = "";
                DateTime expireDate;
                decimal BUQty = 0, BPurQty = 0, FreeQty = 0, BSRQty = 0;
                int PMID = 0;
                pname = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRProductName"].Value);
                cname = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRCompany"].Value);
                uname = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value);
                batchno1 = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRBatchNo"].Value);
                expireDate = Convert.ToDateTime(gvpurreturnproducts.Rows[i].Cells["PRExpiryDate"].Value);
                getProductType(pname, cname, uname);
                if (productentrytype == 1 || productentrytype == 2)
                {
                    PurchaseMasterRepository pmastrepo = new PurchaseMasterRepository();
                    var pdData = pmastrepo.GetAll().Where(t => t.PorderNo == porderno && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    if (pdData != null)
                        PMID = Convert.ToInt32(pdData.Id);
                    BatchRepository batchRepo = new BatchRepository();
                    var batchdata = batchRepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == cname && t.Unit == uname && t.PorderNo == PMID && t.BatchNo == batchno1 && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    if (batchdata != null)
                    {
                        BUQty = Convert.ToDecimal(batchRepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == cname && t.Unit == uname && t.PorderNo == PMID && t.BatchNo == batchno1 && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.UsedQty));
                        GoodDetailRepository SRRepo = new GoodDetailRepository();
                        GoodReturnDetail SRData = new GoodReturnDetail();
                        SRData = SRRepo.GetAll().Where(t => t.ProductId == pname && t.CompanyName == cname && t.Unit == uname && t.BNID == Convert.ToInt32(batchdata.BillId) && t.BatchNo == batchno1).FirstOrDefault();
                        if (SRData != null)
                            BSRQty = BSRQty + Convert.ToDecimal(SRData.Quantity);
                    }
                    else
                        BUQty = 0;
                    PurchaseDetailRepository purchaseRepo = new PurchaseDetailRepository();
                    var purchaseData = purchaseRepo.GetAll().Where(t => t.ProductName == pname && t.CompanyName == cname && t.Unit == uname && t.PorderNo == Convert.ToInt32(PMID) && t.BatchNo == batchno1).FirstOrDefault();
                    if (purchaseData != null)
                    {
                        BPurQty = Convert.ToDecimal(purchaseRepo.GetAll().Where(t => t.ProductName == pname && t.CompanyName == cname && t.Unit == uname && t.PorderNo == Convert.ToInt32(PMID) && t.BatchNo == batchno1).Sum(t => t.Quantity));
                        FreeQty = Convert.ToDecimal(purchaseRepo.GetAll().Where(t => t.ProductName == pname && t.CompanyName == cname && t.Unit == uname && t.PorderNo == Convert.ToInt32(PMID) && t.BatchNo == batchno1).Sum(t => t.FreeQty));
                    }
                    if (((BPurQty + FreeQty) - PRDData.Quantity) == (BUQty - BSRQty))
                    {
                        PurchaseMasterRepository mastRepo = new PurchaseMasterRepository();
                        var purData = mastRepo.GetAll().Where(t => t.PorderNo == porderno && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                        if (purData != null)
                        {
                            PurchaseDetailRepository detailrepo = new PurchaseDetailRepository();
                            var purdetaildata = detailrepo.GetAll().Where(t => t.Id == PRDData.POID && t.ProductName == pname && t.CompanyName == cname && t.Unit == uname && t.BatchNo == batchno1).FirstOrDefault();
                            if (purdetaildata != null)
                            {
                                int pdID = purdetaildata.Id;
                                PurchaseDetail pdata = new PurchaseDetail();
                                pdata = detailrepo.GetById(pdID);
                                pdata.Status = "Inactive";
                                detailrepo.Edit(pdata);
                                detailrepo.Save();
                            }
                        }
                    }
                    else
                    {
                        PurchaseMasterRepository mastRepo1 = new PurchaseMasterRepository();
                        var purData1 = mastRepo1.GetAll().Where(t => t.PorderNo == porderno && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                        if (purData1 != null)
                        {
                            PurchaseDetailRepository detailrepo1 = new PurchaseDetailRepository();
                            var purdetaildata1 = detailrepo1.GetAll().Where(t => t.Id == PRDData.POID && t.ProductName == pname && t.CompanyName == cname && t.Unit == uname && t.BatchNo == batchno1).FirstOrDefault();
                            if (purdetaildata1 != null)
                            {
                                int pdID = purdetaildata1.Id;
                                PurchaseDetail pdata1 = new PurchaseDetail();
                                pdata1 = detailrepo1.GetById(pdID);
                                pdata1.Status = "Active";
                                detailrepo1.Edit(pdata1);
                                detailrepo1.Save();
                            }
                        }
                    }
                }
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
                PReturnDetailRepository productreturndetail = new PReturnDetailRepository();

                string pname = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRProductName"].Value);
                string company = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRCompany"].Value);
                string unit = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value);
                string size = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRSize"].Value);
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
                        command.Parameters.Add("@status", SqlDbType.VarChar).Value = "PR";
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
                    if (StockIn == 0)
                    {
                        var stockData = stockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.Size == size && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (stockData != null)
                        {
                            var updateStockData = stockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.Size == size && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            int updateID = updateStockData.Id;
                            decimal prreturnqty = 0, ramQty = 0, rate = 0, oqty = 0;
                            try
                            {
                                try
                                { oqty = Convert.ToDecimal(updateStockData.OpeningQty); }
                                catch (Exception)
                                { oqty = 0; }
                                prreturnqty = Convert.ToDecimal(productreturndetail.GetAll().Where(t => t.ProductName == pname && t.CompanyName == company && t.Unit == unit && t.Size == size && t.PurchaseReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseReturnMaster.CompId == CommonMethod.CompId).Sum(t => t.Quantity));
                                ramQty = oqty + Convert.ToDecimal(updateStockData.PurchaseQty) - Convert.ToDecimal(updateStockData.SaleQty) - Convert.ToDecimal(updateStockData.DeadQty) - prreturnqty + Convert.ToDecimal(updateStockData.SaleReturnQty);
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
                            updateSData.SaleReturnQty = Convert.ToDecimal(updateStockData.SaleReturnQty);
                            updateSData.PurReturnQty = Convert.ToDecimal(prreturnqty);
                            updateSData.Size = size;
                            updateSData.TransactionYear = CommonMethod.TransactionYear;
                            updateSData.CompId = CommonMethod.CompId;
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
                            updateSData.Id = updateID;
                            updateStockRepo.Edit(updateSData);
                            updateStockRepo.Save();
                        }
                    }
                    else
                    {
                        GodownStockRepository GDStockRepo = new GodownStockRepository();
                        GodownStock GDStockData = new GodownStock();
                        GodownStock GDStockData1 = new GodownStock();

                        if (Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRSGST"].Value) != 0 || Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRIGST"].Value) != 0)
                            GDStockData1 = GDStockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.GoDownId == StockIn && t.Vat == true && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        else
                            GDStockData1 = GDStockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.GoDownId == StockIn && t.Vat == false && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (GDStockData1 != null)
                        {
                            int updateID = GDStockData1.Id;
                            decimal prreturnqty = 0, ramQty = 0, oqty = 0, addqty = 0;
                            try
                            {
                                try
                                { oqty = Convert.ToDecimal(GDStockData1.OpeningQty); }
                                catch (Exception)
                                { oqty = 0; }
                                prreturnqty = Convert.ToDecimal(productreturndetail.GetAll().Where(t => t.PurchaseReturnMaster.Id == purmasterid && t.ProductName == pname && t.CompanyName == company && t.Unit == unit && t.PurchaseReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseReturnMaster.CompId == CommonMethod.CompId).Sum(t => t.Quantity));
                                PurchaseDetailRepository PurDRepo = new PurchaseDetailRepository();
                                if (Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRSGST"].Value) != 0 || Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRIGST"].Value) != 0)
                                    addqty = Convert.ToDecimal(PurDRepo.GetAll().Where(t => t.ProductName == pname && t.CompanyName == company && t.Unit == unit && (t.SGstPercent != 0 || t.SGstPercent == null || t.CGstPercent != 0 || t.CGstPercent == null || t.IGstPercent != 0 || t.IGstPercent == null || t.PurchaseMaster.TAXType == "TaxPurchase") && t.PurchaseMaster.StockIn == StockIn && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId).Sum(t => (t.Quantity + t.FreeQty)));
                                else
                                    addqty = Convert.ToDecimal(PurDRepo.GetAll().Where(t => t.ProductName == pname && t.CompanyName == company && t.Unit == unit && (t.SGstPercent == 0 || t.SGstPercent == null) && (t.CGstPercent == 0 || t.CGstPercent == null) && (t.IGstPercent == 0 || t.IGstPercent == null) && t.PurchaseMaster.TAXType == null && t.PurchaseMaster.StockIn == StockIn && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId).Sum(t => (t.Quantity + t.FreeQty)));
                                ramQty = oqty + addqty - Convert.ToDecimal(GDStockData1.RemoveQty) - prreturnqty;
                            }
                            catch (Exception)
                            { }
                            GDStockData = GDStockRepo.GetById(updateID);
                            GDStockData.AddQty = addqty - prreturnqty;
                            GDStockData.RemQty = ramQty;
                            GodownStockRepository GDStockRepo1 = new GodownStockRepository();
                            GDStockRepo.Edit(GDStockData);
                            GDStockRepo.Save();
                        }
                    }
                }
                productrepo.Dispose(); stockRepo.Dispose(); updateStockRepo.Dispose(); productreturndetail.Dispose();
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
                remainingdata.BillId = Convert.ToInt32(txtporderno.Text);
                remainingdata.BillDate = Convert.ToDateTime(dtpPorderdate.Value);
                remainingdata.CreateDate = Convert.ToDateTime(dtpPRDate.Value);
                remainingdata.Type = "PurchaseReturn";
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
                PReturnMasterRepository PRMRepo = new PReturnMasterRepository();
                var BillId = PRMRepo.GetAll().Where(t => t.PurchaseReturnNo == Convert.ToInt32(txtPRNo.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
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
                                savestock(i);//Start of Stock entry
                            }
                            saveremainingpayment("Cash");//Start of SupplierRemainingPayment entry    
                            gvpurreturnproducts.Rows.Clear();
                            MessageBox.Show("Purchase Return saved.", "Success");
                            clear();
                            this.ActiveControl = txtporderno;
                        }
                        else
                        {
                            if (txtporderno.Text == "")
                            {
                                MessageBox.Show("Select Purchase Order.", "Warning");
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

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                RptPurchaseReturn custrpt = new RptPurchaseReturn(printId);
                custrpt.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void getGstCalculation()
        {
            try
            {
                decimal Discamt = 0, SGstAmt = 0, CGstAmt = 0, IGstAmt = 0, Taxableamt = 0, SpcDiscAmt = 0, cessAmt = 0, OthDiscAmt = 0;
                if (txtQuantity.Text != "" && txtpurchaseprice.Text != "")
                {
                    Taxableamt = Convert.ToDecimal(txtQuantity.Text) * Convert.ToDecimal(txtpurchaseprice.Text);
                    txtamount.Text = Convert.ToString(Math.Round(Taxableamt, 2, MidpointRounding.AwayFromZero));
                    if (txtpdiscount.Text != "")
                        Discamt = (Taxableamt * Convert.ToDecimal(txtpdiscount.Text)) / 100;
                    txtpdisAmt.Text = Convert.ToString(Math.Round(Discamt, 2, MidpointRounding.AwayFromZero));
                    if (txtpSpcDisc.Text != "")
                        SpcDiscAmt = ((Taxableamt - Discamt) * Convert.ToDecimal(txtpSpcDisc.Text)) / 100;
                    if (txtOthDisc.Text != "")
                        OthDiscAmt = ((Taxableamt - Discamt - SpcDiscAmt) * Convert.ToDecimal(txtOthDisc.Text)) / 100;
                    Taxableamt = Taxableamt - Discamt - SpcDiscAmt - OthDiscAmt;
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

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtQuantity.Text != "")
                {
                    try
                    {
                        getGstCalculation();
                        totalamt = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["TotalAmount"].Value));
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
                PReturnMasterRepository PRMRepo = new PReturnMasterRepository();
                PurchaseReturnMaster PRMData = new PurchaseReturnMaster();
                PRMData = PRMRepo.GetAll().Where(t => t.PurchaseReturnNo == Convert.ToInt32(txtPRNo.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                PRMData.PurchaseReturnDate = dtpPRDate.Value;
                PRMData.PorderNo = Convert.ToInt32(txtporderno.Text);
                PRMData.PorderDate = dtpPorderdate.Value;
                PRMData.SupplierpoNo = Convert.ToString(txtsupplierpono.Text);
                PRMData.SupplierId = Convert.ToInt32(cmbsuppliername.SelectedValue);
                PRMData.Suppliername = cmbsuppliername.Text;
                PRMData.TotalAmount = Convert.ToDecimal(txtTotalAmt.Text);
                PRMData.Vat = Convert.ToDecimal(0);
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
                PRMRepo.Edit(PRMData);
                PRMRepo.Save();
                printId = Convert.ToInt32(PRMData.PurchaseReturnNo);
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
                PReturnDetailRepository PRDRepo = new PReturnDetailRepository();
                PurchaseReturnDetail PRDData = new PurchaseReturnDetail();
                if (usevent == "update")
                    PRDData = PRDRepo.GetById(Rowid);
                PRDData.PurchaseReturnNo = purmasterid;

                PRDData.ProductName = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRProductName"].Value);
                PRDData.CompanyName = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRCompany"].Value);
                PRDData.Unit = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value);
                PRDData.PurchaseUnit = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRPurUnit"].Value);
                if (Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value) != Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRPurUnit"].Value))
                {
                    ProductRepository productrepo = new ProductRepository();
                    ProductInformation productdata = new ProductInformation();
                    if (CommonMethod.commProduct == true)
                        productdata = productrepo.GetAll().Where(t => t.ProductName == Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRProductName"].Value) && t.ManufactureCompany == Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRCompany"].Value) && t.Uniti == Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value)).FirstOrDefault();
                    else
                        productdata = productrepo.GetAll().Where(t => t.ProductName == Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRProductName"].Value) && t.ManufactureCompany == Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRCompany"].Value) && t.Uniti == Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    PRDData.Quantity = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRQuantity"].Value) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                    PRDData.FreeQty = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRFrQty"].Value) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                }
                else
                {
                    PRDData.Quantity = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRQuantity"].Value);
                    PRDData.FreeQty = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRFrQty"].Value);
                }
                PRDData.Code = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRCode"].Value);
                PRDData.PurchasePrice = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRPrice"].Value);
                PRDData.Amount = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRAmount"].Value);
                PRDData.Discount = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRDisc"].Value);
                PRDData.SpcDisc = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRSpDisc"].Value);
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
                PRDData.BatchNo = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRBatchNo"].Value);
                if(gvpurreturnproducts.Rows[i].Cells["PRExpiryDate"].Value!=null)
                PRDData.ExpireDate = Convert.ToDateTime(gvpurreturnproducts.Rows[i].Cells["PRExpiryDate"].Value);
                PRDData.Barcode = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRBarcode"].Value);
                PRDData.Size = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRSize"].Value);
                PRDData.DeptName = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRDepartment"].Value);
                PRDData.Category = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRCategory"].Value);
                PRDData.ColorName = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRColorName"].Value);
                PRDData.barcodePrice = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRBarcodePrice"].Value);
                if (gvpurreturnproducts.Rows[i].Cells["PR3LevelDisc"].Value == null)
                    PRDData.ThreeLevelDiscount = 0;
                else
                    PRDData.ThreeLevelDiscount = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PR3LevelDisc"].Value);
                if (usevent == "update")
                    PRDRepo.Edit(PRDData);
                if (usevent == "save")
                    PRDRepo.Add(PRDData);
                PRDRepo.Save();
                PRDRepo.Dispose();
                decimal assemblyQty = Convert.ToDecimal(PRDData.Quantity);
                int BDetailId = Convert.ToInt32(PRDData.POID);
                string Reason = Convert.ToString(PRDData.Reason);
                decimal SGSTPercent = 0, CGSTPercent = 0, IGSTPercent = 0;
                SGSTPercent = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRSGST"].Value);
                CGSTPercent = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRCGST"].Value);
                IGSTPercent = Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRIGST"].Value);

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
                                PurchaseReturnDetail PRDData1 = new PurchaseReturnDetail();
                                PReturnDetailRepository PRDRepo1 = new PReturnDetailRepository();
                                if (usevent == "update")
                                {
                                    var srDetailData = PRDRepo1.GetAll().Where(t => t.PurchaseReturnNo == purmasterid && t.ProductName == itemAssembly.ProductName && t.CompanyName == itemAssembly.CompanyName && t.Unit == itemAssembly.Unit && t.PurchaseReturnMaster.CompId == CommonMethod.CompId && t.PurchaseReturnMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    if (srDetailData != null)
                                        UpdateSRId = Convert.ToInt32(srDetailData.Id);
                                    PRDData1 = PRDRepo1.GetById(UpdateSRId);

                                }
                                PRDData1.PurchaseReturnNo = purmasterid;
                                PRDData1.ProductName = Convert.ToString(itemAssembly.ProductName);
                                PRDData1.CompanyName = Convert.ToString(itemAssembly.CompanyName);
                                PRDData1.Unit = Convert.ToString(itemAssembly.Unit);
                                PRDData1.PurchaseUnit = Convert.ToString(itemAssembly.Unit);
                                decimal childQty = 0, calculateChildQty = 0;
                                childQty = Convert.ToDecimal(itemAssembly.Qty);
                                calculateChildQty = childQty * assemblyQty;
                                PRDData1.Quantity = Convert.ToDecimal(calculateChildQty);
                                PRDData1.Code = Convert.ToString("");
                                PRDData1.PurchasePrice = Convert.ToDecimal(0);
                                PRDData1.Amount = Convert.ToDecimal(0);
                                PRDData1.Discount = Convert.ToDecimal(0);
                                PRDData1.SpcDisc = Convert.ToDecimal(0);
                                PRDData1.SGstPercent = Convert.ToDecimal(SGSTPercent);
                                PRDData1.SGstAmt = Convert.ToDecimal(0);
                                PRDData1.CGstPercent = Convert.ToDecimal(CGSTPercent);
                                PRDData1.CGstAmt = Convert.ToDecimal(0);
                                PRDData1.IGstPercent = Convert.ToDecimal(IGSTPercent);
                                PRDData1.IGstAmt = Convert.ToDecimal(0);
                                PRDData1.Cess = Convert.ToDecimal(0);
                                PRDData1.CessAmt = Convert.ToDecimal(0);
                                PRDData1.TotalAmount = Convert.ToDecimal(0);
                                PRDData1.ThreeLevelDiscount = 0;
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
                                PRDData1.BatchNo = "";
                                PRDData1.ExpireDate = DateTime.Now.Date;
                                PRDData1.Barcode = "";
                                PRDData1.AssemblyChildStatus = "Y";
                                if (usevent == "update")
                                    PRDRepo1.Edit(PRDData1);
                                else
                                    PRDRepo1.Add(PRDData1);
                                PRDRepo1.Save();
                                PRDRepo1.Dispose();
                            }
                        }
                    }
                }
                catch (Exception)
                { }
                string pname = "", cname = "", uname = "", batchno1 = "";
                DateTime expireDate;
                decimal BUQty = 0, BPurQty = 0, FreeQty = 0, BSRQty = 0; int PMID = 0;
                pname = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRProductName"].Value);
                cname = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRCompany"].Value);
                uname = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value);
                batchno1 = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRBatchNo"].Value);
                expireDate = Convert.ToDateTime(gvpurreturnproducts.Rows[i].Cells["PRExpiryDate"].Value);
                getProductType(pname, cname, uname);
                if (productentrytype == 1 || productentrytype == 2)
                {
                    PurchaseMasterRepository pmastrepo = new PurchaseMasterRepository();
                    var pdData = pmastrepo.GetAll().Where(t => t.PorderNo == porderno && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    if (pdData != null)
                        PMID = Convert.ToInt32(pdData.Id);
                    BatchRepository batchRepo = new BatchRepository();
                    var batchdata = batchRepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == cname && t.Unit == uname && t.PorderNo == PMID && t.BatchNo == batchno1 && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    if (batchdata != null)
                    {
                        BUQty = Convert.ToDecimal(batchRepo.GetAll().Where(t => t.ProductName == pname && t.ManufactureCompany == cname && t.Unit == uname && t.PorderNo == PMID && t.BatchNo == batchno1 && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.UsedQty));
                        GoodDetailRepository SRRepo = new GoodDetailRepository();
                        GoodReturnDetail SRData = new GoodReturnDetail();
                        SRData = SRRepo.GetAll().Where(t => t.ProductId == pname && t.CompanyName == cname && t.Unit == uname && t.BNID == Convert.ToInt32(batchdata.BillId) && t.BatchNo == batchno1).FirstOrDefault();
                        if (SRData != null)
                            BSRQty = BSRQty + Convert.ToDecimal(SRData.Quantity);
                    }
                    else
                        BUQty = 0;
                    PurchaseDetailRepository purchaseRepo = new PurchaseDetailRepository();
                    var purchaseData = purchaseRepo.GetAll().Where(t => t.ProductName == pname && t.CompanyName == cname && t.Unit == uname && t.PorderNo == Convert.ToInt32(PMID) && t.BatchNo == batchno1).FirstOrDefault();
                    if (purchaseData != null)
                    {
                        BPurQty = Convert.ToDecimal(purchaseRepo.GetAll().Where(t => t.ProductName == pname && t.CompanyName == cname && t.Unit == uname && t.PorderNo == Convert.ToInt32(PMID) && t.BatchNo == batchno1).Sum(t => t.Quantity));
                        FreeQty = Convert.ToDecimal(purchaseRepo.GetAll().Where(t => t.ProductName == pname && t.CompanyName == cname && t.Unit == uname && t.PorderNo == Convert.ToInt32(PMID) && t.BatchNo == batchno1).Sum(t => t.FreeQty));
                    }
                    if (((BPurQty + FreeQty) - PRDData.Quantity) == (BUQty - BSRQty))
                    {
                        PurchaseMasterRepository mastRepo = new PurchaseMasterRepository();
                        var purData = mastRepo.GetAll().Where(t => t.PorderNo == porderno && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                        if (purData != null)
                        {
                            PurchaseDetailRepository detailrepo = new PurchaseDetailRepository();
                            var purdetaildata = detailrepo.GetAll().Where(t => t.Id == PRDData.POID && t.ProductName == pname && t.CompanyName == cname && t.Unit == uname && t.BatchNo == batchno1).FirstOrDefault();
                            if (purdetaildata != null)
                            {
                                int pdID = purdetaildata.Id;
                                PurchaseDetail pdata = new PurchaseDetail();
                                pdata = detailrepo.GetById(pdID);
                                pdata.Status = "Inactive";
                                detailrepo.Edit(pdata);
                                detailrepo.Save();
                            }
                        }
                    }
                    else
                    {
                        PurchaseMasterRepository mastRepo1 = new PurchaseMasterRepository();
                        var purData1 = mastRepo1.GetAll().Where(t => t.PorderNo == porderno && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                        if (purData1 != null)
                        {
                            PurchaseDetailRepository detailrepo1 = new PurchaseDetailRepository();
                            var purdetaildata1 = detailrepo1.GetAll().Where(t => t.Id == PRDData.POID && t.ProductName == pname && t.CompanyName == cname && t.Unit == uname && t.BatchNo == batchno1).FirstOrDefault();
                            if (purdetaildata1 != null)
                            {
                                int pdID = purdetaildata1.Id;
                                PurchaseDetail pdata1 = new PurchaseDetail();
                                pdata1 = detailrepo1.GetById(pdID);
                                pdata1.Status = "Active";
                                detailrepo1.Edit(pdata1);
                                detailrepo1.Save();
                            }
                        }
                    }
                }
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
                PReturnDetailRepository productreturndetail = new PReturnDetailRepository();

                string pname = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRProductName"].Value);
                string company = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRCompany"].Value);
                string unit = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRUnit"].Value);
                string size = Convert.ToString(gvpurreturnproducts.Rows[i].Cells["PRSize"].Value);
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
                        command.Parameters.Add("@status", SqlDbType.VarChar).Value = "PR";
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
                    if (StockIn == 0)
                    {
                        var stockData = stockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.Size == size && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (stockData != null)
                        {
                            var updateStockData = stockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.Size == size && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            int updateID = updateStockData.Id;
                            decimal prreturnqty = 0, ramQty = 0, rate = 0, oqty = 0;
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
                                prreturnqty = Convert.ToDecimal(productreturndetail.GetAll().Where(t => t.ProductName == pname && t.CompanyName == company && t.Unit == unit && t.Size == size && t.PurchaseReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseReturnMaster.CompId == CommonMethod.CompId).Sum(t => t.Quantity));
                                ramQty = oqty + Convert.ToDecimal(updateStockData.PurchaseQty) - Convert.ToDecimal(updateStockData.SaleQty) - Convert.ToDecimal(updateStockData.DeadQty) - prreturnqty + Convert.ToDecimal(updateStockData.SaleReturnQty);
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
                            updateSData.SaleReturnQty = Convert.ToDecimal(updateStockData.SaleReturnQty);
                            updateSData.PurReturnQty = Convert.ToDecimal(prreturnqty);
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
                    else
                    {
                        GodownStockRepository GDStockRepo = new GodownStockRepository();
                        GodownStock GDStockData = new GodownStock();
                        GodownStock GDStockData1 = new GodownStock();

                        if (Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRSGST"].Value) != 0 || Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRIGST"].Value) != 0)
                            GDStockData1 = GDStockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.GoDownId == StockIn && t.Vat == true && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        else
                            GDStockData1 = GDStockRepo.GetAll().Where(t => t.ProductName == pname && t.MfgCompany == company && t.Unit == unit && t.GoDownId == StockIn && t.Vat == false && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (GDStockData1 != null)
                        {
                            int updateID = GDStockData1.Id;
                            decimal prreturnqty = 0, ramQty = 0, oqty = 0, addqty = 0;
                            try
                            {
                                try
                                { oqty = Convert.ToDecimal(GDStockData1.OpeningQty); }
                                catch (Exception)
                                { oqty = 0; }
                                prreturnqty = Convert.ToDecimal(productreturndetail.GetAll().Where(t => t.PurchaseReturnMaster.Id == purmasterid && t.ProductName == pname && t.CompanyName == company && t.Unit == unit && t.PurchaseReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseReturnMaster.CompId == CommonMethod.CompId).Sum(t => t.Quantity));
                                PurchaseDetailRepository PurDRepo = new PurchaseDetailRepository();
                                if (Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRSGST"].Value) != 0 || Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRIGST"].Value) != 0)
                                    addqty = Convert.ToDecimal(PurDRepo.GetAll().Where(t => t.ProductName == pname && t.CompanyName == company && t.Unit == unit && (t.SGstPercent != 0 || t.SGstPercent == null || t.CGstPercent != 0 || t.CGstPercent == null || t.IGstPercent != 0 || t.IGstPercent == null || t.PurchaseMaster.TAXType == "TaxPurchase") && t.PurchaseMaster.StockIn == StockIn && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId).Sum(t => (t.Quantity + t.FreeQty)));
                                else
                                    addqty = Convert.ToDecimal(PurDRepo.GetAll().Where(t => t.ProductName == pname && t.CompanyName == company && t.Unit == unit && (t.SGstPercent == 0 || t.SGstPercent == null) && (t.CGstPercent == 0 || t.CGstPercent == null) && (t.IGstPercent == 0 || t.IGstPercent == null) && t.PurchaseMaster.TAXType == null && t.PurchaseMaster.StockIn == StockIn && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId).Sum(t => (t.Quantity + t.FreeQty)));
                                ramQty = oqty + addqty - Convert.ToDecimal(GDStockData1.RemoveQty) - prreturnqty;
                            }
                            catch (Exception)
                            { }
                            GDStockData = GDStockRepo.GetById(updateID);
                            GDStockData.AddQty = addqty - prreturnqty;
                            GDStockData.RemQty = ramQty;
                            GodownStockRepository GDStockRepo1 = new GodownStockRepository();
                            GDStockRepo.Edit(GDStockData);
                            GDStockRepo.Save();
                        }
                    }
                }
                productrepo.Dispose(); stockRepo.Dispose(); updateStockRepo.Dispose(); productreturndetail.Dispose();
            }
            catch (Exception)
            { }
        }

        public void updateremainigpayment(string mode)
        {
            try
            {
                SupplierRemainingPaymentRepository sremainingpaymentrepo = new SupplierRemainingPaymentRepository();
                var remainingpay = sremainingpaymentrepo.GetAll().Where(t => t.BillId == Convert.ToInt32(txtporderno.Text) && t.Type == "PurchaseReturn" && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                SupplierRemainingPaymentRepository remainingrepo = new SupplierRemainingPaymentRepository();
                SupplierRemainingPayment paydata = new SupplierRemainingPayment();
                if (remainingpay != null)
                {
                    if (Convert.ToDecimal(ttnetamt.Text) > 0)
                    {
                        paydata = new SupplierRemainingPayment();
                        paydata.SupplierId = Convert.ToInt32(cmbsuppliername.SelectedValue);
                        paydata.Suppliername = Convert.ToString(cmbsuppliername.Text);
                        paydata.NetAmt = 0;
                        paydata.PaidAmt = Convert.ToDecimal(ttnetamt.Text);
                        SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                        SupplierRemainingPayment remainingdata1 = new SupplierRemainingPayment();
                        remainingdata1 = remainingpayrepo1.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.Id < remainingpay.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                        decimal decremamt = 0;
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
                        SupplierRemainingPaymentRepository remainingpayrepo2 = new SupplierRemainingPaymentRepository();
                        List<SupplierRemainingPayment> remainingdata2 = new List<SupplierRemainingPayment>();
                        remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.Id > remainingpay.Id && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();

                        paydata.BillId = Convert.ToInt32(txtporderno.Text);
                        paydata.BillDate = Convert.ToDateTime(dtpPorderdate.Value);
                        paydata.CreateDate = Convert.ToDateTime(dtpPRDate.Value);
                        paydata.Type = "PurchaseReturn";
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
                    paydata.NetAmt = 0;
                    paydata.PaidAmt = Convert.ToDecimal(ttnetamt.Text);
                    paydata.RemainingAmt = 0;
                    paydata.BillId = Convert.ToInt32(txtporderno.Text);
                    paydata.BillDate = Convert.ToDateTime(dtpPorderdate.Value);
                    paydata.CreateDate = Convert.ToDateTime(dtpPRDate.Value);
                    paydata.Type = "PurchaseReturn";
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

        public void deleteitemwithstockupdation(int i)
        {
            try
            {
                AIOInventorySystem.Data.Model.Stock updateSData = new Data.Model.Stock();
                StockRepository stockRepo = new StockRepository();
                StockRepository updateStockRepo = new StockRepository();
                ProductRepository productrepo = new ProductRepository();
                PReturnDetailRepository productreturndetail = new PReturnDetailRepository();
                PurchaseReturnDetail prdetaildata = productreturndetail.GetById(i);

                string proname = prdetaildata.ProductName;
                string company = prdetaildata.CompanyName;
                string unit = prdetaildata.Unit;
                string size = prdetaildata.Size;
                decimal Quantity = Convert.ToDecimal(prdetaildata.Quantity);
                decimal frqty = Convert.ToDecimal(prdetaildata.FreeQty);

                int MasterId = Convert.ToInt32(prdetaildata.PurchaseReturnNo);
                ProductInformation productData = new ProductInformation();
                if (CommonMethod.commProduct == true)
                    productData = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                else
                    productData = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (productData.ProductType == 2)
                {
                    try
                    {
                        db.connect();
                        SqlCommand command = new SqlCommand("SPSaveUpdateSalePurchaseReturnStock", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@status", SqlDbType.VarChar).Value = "PR";
                        command.Parameters.Add("@proname", SqlDbType.VarChar).Value = proname;
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
                    if (StockIn == 0)
                    {
                        var stockData = stockRepo.GetAll().Where(t => t.ProductName == proname && t.MfgCompany == company && t.Unit == unit && t.Size==size && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (stockData != null)
                        {
                            var updateStockData = stockRepo.GetAll().Where(t => t.ProductName == proname && t.MfgCompany == company && t.Unit == unit && t.Size == size && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            int updateID = updateStockData.Id;
                            decimal prreturnqty = 0, prreturnfreeqty = 0, ramQty = 0, rate = 0, oqty = 0;
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
                                prreturnqty = Convert.ToDecimal(productreturndetail.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.Size == size && t.PurchaseReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseReturnMaster.CompId == CommonMethod.CompId).Sum(t => t.Quantity));
                                prreturnfreeqty = Convert.ToDecimal(productreturndetail.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.Size == size && t.PurchaseReturnMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseReturnMaster.CompId == CommonMethod.CompId).Sum(t => t.FreeQty));
                                prreturnqty = (prreturnqty + prreturnfreeqty) - (Quantity + frqty);
                                ramQty = oqty + Convert.ToDecimal(updateStockData.PurchaseQty) - Convert.ToDecimal(updateStockData.SaleQty) - Convert.ToDecimal(updateStockData.DeadQty) - prreturnqty + Convert.ToDecimal(updateStockData.SaleReturnQty);
                            }
                            catch (Exception)
                            { }
                            updateSData.ProductId = productData.ProductCode;
                            updateSData.ProductName = proname;
                            updateSData.MfgCompany = company;
                            updateSData.Unit = unit;
                            updateSData.OpeningQty = oqty;
                            updateSData.PurchaseQty = Convert.ToDecimal(updateStockData.PurchaseQty);
                            updateSData.SaleQty = Convert.ToDecimal(updateStockData.SaleQty);
                            updateSData.DeadQty = updateStockData.DeadQty;
                            updateSData.RemQty = ramQty;
                            updateSData.SaleReturnQty = Convert.ToDecimal(updateStockData.SaleReturnQty);
                            updateSData.PurReturnQty = Convert.ToDecimal(prreturnqty);
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

                                command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = proname;
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
                    else
                    {
                        GodownStockRepository GDStockRepo = new GodownStockRepository();
                        GodownStock GDStockData = new GodownStock();
                        GodownStock GDStockData1 = new GodownStock();

                        if (Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRSGST"].Value) != 0 || Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRIGST"].Value) != 0)
                            GDStockData1 = GDStockRepo.GetAll().Where(t => t.ProductName == proname && t.MfgCompany == company && t.Unit == unit && t.GoDownId == StockIn && t.Vat == true && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        else
                            GDStockData1 = GDStockRepo.GetAll().Where(t => t.ProductName == proname && t.MfgCompany == company && t.Unit == unit && t.GoDownId == StockIn && t.Vat == false && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (GDStockData1 != null)
                        {
                            int updateID = GDStockData1.Id;
                            decimal prreturnqty = 0, ramQty = 0, oqty = 0, addqty = 0;
                            try
                            {
                                try
                                { oqty = Convert.ToDecimal(GDStockData1.OpeningQty); }
                                catch (Exception)
                                { oqty = 0; }
                                prreturnqty = (Quantity + frqty);
                                PurchaseDetailRepository PurDRepo = new PurchaseDetailRepository();
                                if (Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRSGST"].Value) != 0 || Convert.ToDecimal(gvpurreturnproducts.Rows[i].Cells["PRIGST"].Value) != 0)
                                    addqty = Convert.ToDecimal(PurDRepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && (t.SGstPercent != 0 || t.SGstPercent == null || t.CGstPercent != 0 || t.CGstPercent == null || t.IGstPercent != 0 || t.IGstPercent == null || t.PurchaseMaster.TAXType == "TaxPurchase") && t.PurchaseMaster.StockIn == StockIn && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId).Sum(t => (t.Quantity + t.FreeQty)));
                                else
                                    addqty = Convert.ToDecimal(PurDRepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && (t.SGstPercent == 0 || t.SGstPercent == null) && (t.CGstPercent == 0 || t.CGstPercent == null) && (t.IGstPercent == 0 || t.IGstPercent == null) && t.PurchaseMaster.TAXType == null && t.PurchaseMaster.StockIn == StockIn && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId).Sum(t => (t.Quantity + t.FreeQty)));
                                ramQty = oqty + addqty - Convert.ToDecimal(GDStockData1.RemoveQty);
                            }
                            catch (Exception)
                            { }
                            GDStockData = GDStockRepo.GetById(updateID);
                            GDStockData.AddQty = addqty + prreturnqty;
                            GDStockData.RemQty = ramQty;
                            GodownStockRepository GDStockRepo1 = new GodownStockRepository();
                            GDStockRepo.Edit(GDStockData);
                            GDStockRepo.Save();
                        }
                    }
                }
                PReturnDetailRepository PurRDRepo2 = new PReturnDetailRepository();
                PurchaseReturnDetail prdetaildata2 = PurRDRepo2.GetById(i);
                PurRDRepo2.Remove(prdetaildata2);
                PurRDRepo2.Save();
                PurRDRepo2.Dispose();
                PurRDRepo2.Dispose(); stockRepo.Dispose(); updateStockRepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void cleartext()
        {
            txtQuantity.Text = "0";
            txtFreeQty.Text = "0";
            txtpurchaseprice.Text = "0";
            txtamount.Text = "0";
            txtpdiscount.Text = "0";
            txtpTotalAmt.Text = "0";
        }

        private void txtProductname_Leave(object sender, EventArgs e)
        {
            try
            {
                custbillinfo = this;
                if (txtpname.Text != "")
                {
                    string proname = "", company = "", unit = "";
                    if ((txtpname.Text).Contains(","))
                    {
                        try
                        {
                            proname = txtpname.Text.Trim().Split(',')[0];
                            company = txtpname.Text.Trim().Split(',')[1];
                            unit = txtpname.Text.Trim().Split(',')[2];
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
                            txtpname.Text = "";
                            this.ActiveControl = txtpname;
                        }
                        else
                            txtHSN.Text = productdata.HSNCode;
                        productrepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                        txtpname.Text = "";
                        this.ActiveControl = txtpname;
                    }

                    if (btnsave.Enabled == false)
                    {
                        decimal rate;
                        if (txtpurchaseprice.Text == "")
                            rate = 0;
                        else
                            rate = Convert.ToDecimal(txtpurchaseprice.Text);
                        decimal amt = rate * Convert.ToDecimal(txtQuantity.Text);
                        txtamount.Text = Convert.ToString(amt);
                        txtpTotalAmt.Text = Convert.ToString(amt);
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

        private void btnbilllist_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                custbillinfo = this;
                frmPurchaseReturn p = new frmPurchaseReturn();
                p.Visible = false;
                frmPurchaseReturnList custbilllist = new frmPurchaseReturnList();
                custbilllist.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal textqty = 0;
                if (txtQuantity.Text != "")
                {
                    if (txtpname.Text != "")
                    {
                        string proname = "", company = "", unit = "";
                        if ((txtpname.Text).Contains(","))
                        {
                            try
                            {
                                proname = txtpname.Text.Trim().Split(',')[0];
                                company = txtpname.Text.Trim().Split(',')[1];
                                unit = txtpname.Text.Trim().Split(',')[2];
                            }
                            catch (Exception)
                            { }
                            ProductRepository productrepo = new ProductRepository();
                            ProductInformation productdata = new ProductInformation();
                            if (CommonMethod.commProduct == true)
                                productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                            else
                                productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            if (productdata != null)
                            {
                                getProductType(proname, company, unit);
                                decimal remqty = 0, originalpqty = 0;
                                if (pocolIndex != -1)
                                {
                                    if (productentrytype == 1 || productentrytype == 2)
                                    {
                                        originalpqty = Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["Quantityg"].Value) + Convert.ToDecimal(GvProductInfo.Rows[pocolIndex].Cells["FrQty"].Value);
                                        if (cmbUnit.Text != unit)
                                            originalpqty = Convert.ToDecimal(originalpqty) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                        PurchaseMasterRepository PurMRepo = new PurchaseMasterRepository();
                                        PurchaseMaster PurMData = new PurchaseMaster();
                                        PurMData = PurMRepo.GetAll().Where(t => t.PorderNo == Convert.ToInt32(txtporderno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        if (PurMData != null)
                                        {
                                            BatchRepository BatchRepo = new BatchRepository();
                                            Batch BatchData = new Batch();
                                            decimal Usedqty = Convert.ToDecimal(BatchRepo.GetAll().Where(t => t.PorderNo == PurMData.Id && t.ProductName == proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == txtBatchNo.Text && t.CompId == CommonMethod.CompId).Sum(t => t.UsedQty));
                                            decimal srqty = 0;
                                            try
                                            {
                                                BatchRepository batchrpstr = new BatchRepository();
                                                List<Batch> BData = new List<Batch>();
                                                BData = batchrpstr.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == txtBatchNo.Text && t.Status == "1" && t.PorderNo == PurMData.Id && t.CompId == CommonMethod.CompId).ToList();
                                                if (BData != null)
                                                {
                                                    foreach (var item1 in BData)
                                                    {
                                                        try
                                                        {
                                                            GoodDetailRepository SRRepo = new GoodDetailRepository();
                                                            GoodReturnDetail SRData = new GoodReturnDetail();
                                                            SRData = SRRepo.GetAll().Where(t => t.ProductId == item1.ProductName && t.CompanyName == item1.ManufactureCompany && t.Unit == item1.Unit && t.BNID == Convert.ToInt32(item1.BillId) && t.BatchNo == item1.BatchNo && t.GoodReturnMaster.CompId == CommonMethod.CompId && t.GoodReturnMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                                            srqty = srqty + Convert.ToDecimal(SRData.Quantity);
                                                        }
                                                        catch (Exception)
                                                        { }
                                                    }
                                                }
                                            }
                                            catch (Exception)
                                            { }
                                            remqty = originalpqty - Usedqty + srqty;
                                            if (Convert.ToDecimal(txtQuantity.Text) <= remqty)
                                            { }
                                            else
                                            {
                                                MessageBox.Show("Product Remaining Quantity in Stock is " + remqty, "Warning");
                                                txtQuantity.Text = "";
                                                this.ActiveControl = txtQuantity;
                                            }
                                        }
                                    }
                                    else if (productentrytype == 6)
                                    {
                                        textqty = Convert.ToDecimal(txtQuantity.Text);
                                        try
                                        {
                                            db.connect();
                                            SqlCommand command = new SqlCommand("SPGetAssemblyProductRemQty", db.Connection);
                                            command.CommandType = CommandType.StoredProcedure;
                                            command.Parameters.Add("@proname", SqlDbType.VarChar).Value = proname;
                                            command.Parameters.Add("@cname", SqlDbType.VarChar).Value = company;
                                            command.Parameters.Add("@uname", SqlDbType.VarChar).Value = unit;
                                            command.Parameters.Add("@status", SqlDbType.VarChar).Value = "P";
                                            command.Parameters.Add("@vat", SqlDbType.VarChar).Value = "a";
                                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                                            SqlDataReader reader = command.ExecuteReader();
                                            if (reader.Read())
                                            {
                                                remqty = Convert.ToDecimal(reader.GetSqlDecimal(10).ToDouble());
                                                if (textqty <= remqty)
                                                { }
                                                else
                                                {
                                                    MessageBox.Show("Product Remaining Quantity in Stock is " + remqty, "Warning");
                                                    txtQuantity.Text = "";
                                                    this.ActiveControl = txtQuantity;
                                                }
                                            }
                                            db.CloseConnection();
                                        }
                                        catch (Exception)
                                        { db.CloseConnection(); }
                                    }
                                    else if (StockIn == 0)
                                    {
                                        string Sizeog = GvProductInfo.Rows[pocolIndex].Cells["Sizeg"].Value.ToString();
                                        StockRepository StockRepo = new StockRepository();
                                        AIOInventorySystem.Data.Model.Stock StockData = new AIOInventorySystem.Data.Model.Stock();
                                        StockData = StockRepo.GetAll().Where(t => t.ProductName == proname && t.MfgCompany == company && t.Unit == unit && t.Size == Sizeog && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        if (StockData != null)
                                            remqty = Convert.ToDecimal(StockData.RemQty);
                                        if (cmbUnit.Text == unit)
                                            textqty = Convert.ToDecimal(txtQuantity.Text);
                                        else
                                            textqty = Convert.ToDecimal(txtQuantity.Text) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                        if (textqty <= remqty)
                                        {
                                            if (ProductCategory == "Motor")
                                            {
                                                if (Convert.ToDecimal(txtQuantity.Text) != 1)
                                                {
                                                    MessageBox.Show("Must be 1 quantity for return.", "Warning");
                                                    txtQuantity.Text = "1";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Product Remaining Quantity in Stock is " + remqty, "Warning");
                                            txtQuantity.Text = "";
                                            this.ActiveControl = txtQuantity;
                                        }
                                        StockRepo.Dispose();
                                    }
                                    else
                                    {
                                        GodownStockRepository StockRepo = new GodownStockRepository();
                                        GodownStock StockData = new GodownStock();
                                        if (Convert.ToDecimal(cmbsgst.Text) != 0 || Convert.ToDecimal(cmbsgst.Text) != 0)
                                            StockData = StockRepo.GetAll().Where(t => t.ProductName == proname && t.MfgCompany == company && t.Unit == unit && t.GoDownId == StockIn && t.Vat == true && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        else
                                            StockData = StockRepo.GetAll().Where(t => t.ProductName == proname && t.MfgCompany == company && t.Unit == unit && t.GoDownId == StockIn && t.Vat == false && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        if (StockData != null)
                                            remqty = Convert.ToDecimal(StockData.RemQty);
                                        if (cmbUnit.Text == unit)
                                            textqty = Convert.ToDecimal(txtQuantity.Text);
                                        else
                                            textqty = Convert.ToDecimal(txtQuantity.Text) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                        if (textqty <= remqty)
                                        {
                                            if (ProductCategory == "Motor")
                                            {
                                                if (Convert.ToDecimal(txtQuantity.Text) != 1)
                                                {
                                                    MessageBox.Show("Must be 1 quantity for return.", "Warning");
                                                    txtQuantity.Text = "1";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Product Remaining Quantity in Stock is " + remqty, "Warning");
                                            txtQuantity.Text = "";
                                            this.ActiveControl = txtQuantity;
                                        }
                                        StockRepo.Dispose();
                                    }
                                }
                            }
                            productrepo.Dispose();
                        }
                        if (btnsave.Enabled == false)
                        {
                            decimal rate;
                            if (txtpurchaseprice.Text == "")
                                rate = 0;
                            else
                                rate = Convert.ToDecimal(txtpurchaseprice.Text);
                            decimal amt = rate * Convert.ToDecimal(txtQuantity.Text);
                            txtamount.Text = Convert.ToString(amt);
                        }
                    }
                }
            }
            catch (Exception)
            { }
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
                    porderno = Convert.ToInt32(PurchaeData.PorderNo);
                    txtsupplierpono.Text = Convert.ToString(PurchaeData.SupplierpoNo);
                    dtpPorderdate.Text = Convert.ToString(PurchaeData.PorderDate.Value);
                    cmbsuppliername.Text = PurchaeData.Suppliername;
                    if (PurchaeData.StockIn != null)
                        StockIn = Convert.ToInt32(PurchaeData.StockIn);
                    else
                        StockIn = 0;
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
                    mvat = Convert.ToDecimal(PurchaeData.Vat);
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
                            string strbarcode = "";
                            ProductRepository productrepo = new ProductRepository();
                            ProductInformation productdata = new ProductInformation();
                            if (CommonMethod.commProduct == true)
                                productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit).FirstOrDefault();
                            else
                                productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            strbarcode = productdata.BarcodeStatus;
                            if (item.Unit != item.PurchaseUnit && item.PurchaseUnit != null)
                            {
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
                            GvProductInfo.Rows[i].Cells["SGST"].Value = item.SGstPercent;
                            GvProductInfo.Rows[i].Cells["SGSTAmt"].Value = item.SGstAmt;
                            GvProductInfo.Rows[i].Cells["CGST"].Value = item.CGstPercent;
                            GvProductInfo.Rows[i].Cells["CGSTAmt"].Value = item.CGstAmt;
                            GvProductInfo.Rows[i].Cells["IGST"].Value = item.IGstPercent;
                            GvProductInfo.Rows[i].Cells["IGSTAmt"].Value = item.IGstAmt;
                            GvProductInfo.Rows[i].Cells["Cess"].Value = item.Cess;
                            GvProductInfo.Rows[i].Cells["CessAmt"].Value = item.CessAmt;
                            GvProductInfo.Rows[i].Cells["TotalAmount"].Value = item.TotalAmount;
                            if (!string.IsNullOrEmpty(item.BatchNo))
                                GvProductInfo.Rows[i].Cells["BatchNo"].Value = item.BatchNo;
                            else
                                GvProductInfo.Rows[i].Cells["BatchNo"].Value = "";
                            GvProductInfo.Rows[i].Cells["ExpiryDate"].Value = item.ExpireDate;
                            if (strbarcode == "Static Barcode")
                                GvProductInfo.Rows[i].Cells["Barcode"].Value = item.StaticBarcode;
                            else if (strbarcode == "Our Barcode")
                                GvProductInfo.Rows[i].Cells["Barcode"].Value = item.Barcode;
                            if (mvat != 0)
                                mdvat = mdvat + Convert.ToDecimal(item.Vat);
                            GvProductInfo.Rows[i].Cells["ThreeLevelDisc"].Value = item.ThreeLevelDiscPer;
                            GvProductInfo.Rows[i].Cells["Sizeg"].Value = item.Size;
                            GvProductInfo.Rows[i].Cells["Department"].Value = item.DeptName;
                            GvProductInfo.Rows[i].Cells["Category"].Value = item.CategoryName;
                            GvProductInfo.Rows[i].Cells["ColorName"].Value = item.ColorName;
                            GvProductInfo.Rows[i].Cells["BarcodePrice"].Value = item.SaleRate;
                            i++;
                        }
                    }
                    btnsave.Enabled = true;
                    btnUpdate.Enabled = false;
                    dtpPorderdate.Enabled = false;
                    txtsupplierpono.Enabled = false;
                    cmbsuppliername.Enabled = false;

                    PReturnMasterRepository PRMRepo = new PReturnMasterRepository();
                    var PRMData = PRMRepo.GetAll().Where(t => t.PorderNo == PorderId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (PRMData != null)
                    {
                        txtPRNo.Text = Convert.ToString(PRMData.PurchaseReturnNo);
                        dtpPRDate.Value = PRMData.PurchaseReturnDate.Value;
                        txtTotalAmt.Text = Convert.ToString(PRMData.TotalAmount);
                        txtDiscount.Text = Convert.ToString(PRMData.Discount);
                        ttnetamt.Text = Convert.ToString(PRMData.NetAmount);
                        var PRDData = PRMData.PurchaseReturnDetails;
                        gvpurreturnproducts.Rows.Clear();
                        int j = 0;
                        foreach (var item in PRDData)
                        {
                            if (item.AssemblyChildStatus == "N" || item.AssemblyChildStatus == null || item.AssemblyChildStatus == "")
                            {
                                gvpurreturnproducts.Rows.Add();
                                gvpurreturnproducts.Rows[j].Cells["PRIDg"].Value = item.Id;
                                gvpurreturnproducts.Rows[j].Cells["PRProductName"].Value = item.ProductName;
                                gvpurreturnproducts.Rows[j].Cells["PRCompany"].Value = item.CompanyName;
                                gvpurreturnproducts.Rows[j].Cells["PRUnit"].Value = item.Unit;
                                if (item.PurchaseUnit != null)
                                    gvpurreturnproducts.Rows[j].Cells["PRPurUnit"].Value = item.PurchaseUnit;
                                else
                                    gvpurreturnproducts.Rows[j].Cells["PRPurUnit"].Value = item.Unit;
                                if (item.Unit != item.PurchaseUnit && item.PurchaseUnit != null)
                                {
                                    ProductRepository productrepo = new ProductRepository();
                                    ProductInformation productdata = new ProductInformation();
                                    if (CommonMethod.commProduct == true)
                                        productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit).FirstOrDefault();
                                    else
                                        productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                    gvpurreturnproducts.Rows[j].Cells["PRQuantity"].Value = Convert.ToDecimal(item.Quantity) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                    gvpurreturnproducts.Rows[j].Cells["PRFrQty"].Value = Convert.ToDecimal(item.FreeQty) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                }
                                else
                                {
                                    gvpurreturnproducts.Rows[j].Cells["PRQuantity"].Value = item.Quantity;
                                    gvpurreturnproducts.Rows[j].Cells["PRFrQty"].Value = item.FreeQty;
                                }
                                gvpurreturnproducts.Rows[j].Cells["PRCode"].Value = item.Code;
                                gvpurreturnproducts.Rows[j].Cells["PRPrice"].Value = item.PurchasePrice;
                                gvpurreturnproducts.Rows[j].Cells["PRAmount"].Value = item.Amount;
                                gvpurreturnproducts.Rows[j].Cells["PRDisc"].Value = item.Discount;
                                gvpurreturnproducts.Rows[j].Cells["PRSpDisc"].Value = item.SpcDisc;
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
                                if (!string.IsNullOrEmpty(item.BatchNo))
                                    gvpurreturnproducts.Rows[j].Cells["PRBatchNo"].Value = item.BatchNo;
                                gvpurreturnproducts.Rows[j].Cells["PRExpiryDate"].Value = item.ExpireDate;
                                gvpurreturnproducts.Rows[j].Cells["PRBarcode"].Value = item.Barcode;
                                gvpurreturnproducts.Rows[j].Cells["PR3LevelDisc"].Value = item.ThreeLevelDiscount;
                                gvpurreturnproducts.Rows[j].Cells["PRSize"].Value = item.Size;
                                gvpurreturnproducts.Rows[j].Cells["PRDepartment"].Value = item.DeptName;
                                gvpurreturnproducts.Rows[j].Cells["PRCategory"].Value = item.Category;
                                gvpurreturnproducts.Rows[j].Cells["PRColorName"].Value = item.ColorName;
                                gvpurreturnproducts.Rows[j].Cells["PRBarcodePrice"].Value = item.barcodePrice;

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
                {
                    SettingRepository settRepo = new SettingRepository();
                    Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (sdata.DeleteSaleBill == "True")
                        btnDelete.Enabled = true;
                    else
                        btnDelete.Enabled = false;
                }
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
                    customerdata();
                    this.ActiveControl = cmbsuppliername;
                }
            }
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

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtReason;
            }
            catch (Exception)
            { }
        }

        private void txtReason_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnAdd;
            }
            catch (Exception)
            { }
        }

        private void txtpname_Leave(object sender, EventArgs e)
        {
            if ((txtpname.Text).Contains(","))
            {
                string proname = "", company = "", unit = "";
                try
                {
                    proname = txtpname.Text.Trim().Split(',')[0];
                    company = txtpname.Text.Trim().Split(',')[1];
                    unit = txtpname.Text.Trim().Split(',')[2];
                }
                catch (Exception)
                { }
                getProductType(proname, company, unit);
                AdjustPanelDetails(productentrytype);
            }
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

        private void AdjustPanelDetails(int product_entrytype)
        {
            GvProductInfo.Columns[21].Visible = false;
            GvProductInfo.Columns[22].Visible = false;
            if (product_entrytype == 1 || product_entrytype == 2)  //producttype == 0 && (WithBatchExp == "Y" || onlyBatch == "Y")
            {
                // 1 - krushi with batch and expiry
                pnlIntegratedDtls.Visible = true;
                pnlIntegratedDtls.Width = 384;
                pnlIntegratedDtls.Height = 33;
                pnlIntegratedDtls.Location = new Point(319, 140);
                if (product_entrytype == 2)
                    dtpExpiry.Enabled = false;
                GvProductInfo.Columns[21].Visible = true;
                GvProductInfo.Columns[22].Visible = true;
                this.ActiveControl = txtBatchNo;
            }
            else if (product_entrytype == 4)// without size //matrix
            {
                txtBatchNo.Enabled = false;
                dtpExpiry.Enabled = false;
                pnlIntegratedDtls.Visible = false;
            }
            else if (product_entrytype == 3)  // With Size
            {
                txtBatchNo.Enabled = false;
                dtpExpiry.Enabled = false;
                pnlIntegratedDtls.Visible = true;
                pnlIntegratedDtls.Width = 384;
                pnlIntegratedDtls.Height = 182;
                pnlIntegratedDtls.Location = new Point(319, 140);
            }
        }

        private void txtBatchNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtBatchNo.Text != "")
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpExpiry;
            }
        }

        private void dtpExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                this.ActiveControl = dtpExpiry;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pnlIntegratedDtls.Visible = false;
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

        private void btnAdd_Leave(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = btnsave;
            }
            catch (Exception) { }
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
                        if (deleteid.Count != 0)
                        {
                            for (int x = 0; x < deleteid.Count; x++)
                            {
                                deleteitemwithstockupdation(deleteid[x]);
                            }
                        }
                        for (int i = 0; i < gvpurreturnproducts.Rows.Count; i++)
                        {
                            if (Convert.ToInt32(gvpurreturnproducts.Rows[i].Cells["PRIDg"].Value) != 0)
                                updatedetail(i, Convert.ToInt32(gvpurreturnproducts.Rows[i].Cells["PRIDg"].Value), "update");
                            else
                                updatedetail(i, Convert.ToInt32(gvpurreturnproducts.Rows[i].Cells["PRIDg"].Value), "save");
                            updatestock(i);
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
                if ((MessageBox.Show("Are you sure to Delete this Purchase Return ?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    AIOInventorySystem.Data.Model.Stock sdata = new Data.Model.Stock();
                    string productname = "", unit = "", company = "", alternateunit = "";
                    int PMID = 0;
                    decimal Quantity = 0, newsqty = 0, sqty = 0, pqty = 0, prqty = 0, newrqty = 0, rqty = 0, srqty = 0, dqty = 0, oqty = 0;

                    PReturnMasterRepository PRmastRepo = new PReturnMasterRepository();
                    var PRMData = PRmastRepo.GetAll().Where(t => t.PorderNo == porderno && t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    if (PRMData != null)
                    {
                        // add details for history table
                        try
                        {
                            DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                            DeleteHistoryTable historydata = new DeleteHistoryTable();
                            historydata.DeleteType = "Purchase Return";
                            historydata.BillNo = Convert.ToString(PRMData.PurchaseReturnNo);
                            historydata.SupplierOrCustomerName = Convert.ToString(PRMData.Suppliername);
                            historydata.Amount = Convert.ToDecimal(PRMData.NetAmount);
                            historydata.CurrentDate = DateTime.Now.Date;
                            historydata.BillDate = Convert.ToDateTime(PRMData.PurchaseReturnDate);
                            historydata.CompId = CommonMethod.CompId;
                            historydata.TransactionYear = CommonMethod.TransactionYear;
                            historyRepo.Add(historydata);
                            historyRepo.Save();
                        }
                        catch (Exception) { }

                        decimal decremamt = 0;
                        int PReturnId = PRmastRepo.GetAll().Where(t => t.PorderNo == porderno && t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().Id;
                        SupplierRemainingPaymentRepository suppremRepo = new SupplierRemainingPaymentRepository();
                        var suppremdata = suppremRepo.GetAll().Where(t => t.BillId == porderno && t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.Type == "PurchaseReturn" && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                        if (suppremdata != null)
                        {
                            int ReceiptId = suppremRepo.GetAll().Where(t => t.BillId == porderno && t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.Type == "PurchaseReturn" && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().Id;
                            SupplierRemainingPaymentRepository SupplierRemRepo = new SupplierRemainingPaymentRepository();
                            SupplierRemainingPayment SupplierRemdata = SupplierRemRepo.GetById(ReceiptId);
                            SupplierRemRepo.Remove(SupplierRemdata);
                            SupplierRemRepo.Save();

                            SupplierRemainingPaymentRepository remainingpayrepo1 = new SupplierRemainingPaymentRepository();
                            SupplierRemainingPayment remdata1 = SupplierRemRepo.GetAll().Where(t => t.SupplierId == Convert.ToInt32(SupplierRemdata.SupplierId) && t.Id < ReceiptId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).LastOrDefault();
                            if (remdata1 != null)
                                decremamt = Convert.ToDecimal(remdata1.RemainingAmt);
                            else
                                decremamt = 0;
                            SupplierRemainingPaymentRepository remainingpayrepo2 = new SupplierRemainingPaymentRepository();
                            List<SupplierRemainingPayment> remainingdata2 = new List<SupplierRemainingPayment>();
                            remainingdata2 = remainingpayrepo2.GetAll().Where(t => t.SupplierId == Convert.ToInt32(SupplierRemdata.SupplierId) && t.Id > ReceiptId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).ToList();

                            int i = 0;
                            foreach (var item in remainingdata2)
                            {
                                item.RemainingAmt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                                decremamt = Convert.ToDecimal(item.NetAmt) - Convert.ToDecimal(item.PaidAmt) + decremamt;
                                remainingpayrepo2.Edit(item);
                                remainingpayrepo2.Save();
                                i++;
                            }
                            remainingpayrepo1.Dispose();
                            remainingpayrepo2.Dispose();
                        }

                        PReturnDetailRepository PRDetailRepo = new PReturnDetailRepository();
                        List<PurchaseReturnDetail> detailList = PRDetailRepo.GetAll().Where(t => t.PurchaseReturnNo == PReturnId).ToList();
                        if (detailList.Count != 0)
                        {
                            foreach (var item1 in detailList)
                            {
                                int detailId = 0, updateStockId = 0;
                                decimal rate = 0, BUQty = 0, BPurQty = 0, FreeQty = 0, BSRQty = 0, BRQty = 0, BPRQty = 0;
                                string batchNo = "";
                                DateTime expirydate;
                                productname = item1.ProductName;
                                company = item1.CompanyName;
                                unit = item1.Unit;
                                alternateunit = item1.PurchaseUnit;
                                detailId = item1.Id;
                                batchNo = item1.BatchNo;
                                expirydate = Convert.ToDateTime(item1.ExpireDate);
                                Quantity = Convert.ToDecimal(item1.Quantity);

                                //for batch product 27/6/2018 
                                getProductType(productname, company, unit);
                                if (productentrytype == 1 || productentrytype == 2)
                                {
                                    PurchaseMasterRepository pmrepo = new PurchaseMasterRepository();
                                    var pmdata = pmrepo.GetAll().Where(t => t.PorderNo == porderno && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    if (pmdata != null)
                                        PMID = pmdata.Id;
                                    BatchRepository batchRepo = new BatchRepository();
                                    var batchdata = batchRepo.GetAll().Where(t => t.ProductName == productname && t.ManufactureCompany == company && t.Unit == unit && t.PorderNo == PMID && t.BatchNo == batchNo && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    if (batchdata != null)
                                    {
                                        BUQty = Convert.ToDecimal(batchRepo.GetAll().Where(t => t.ProductName == productname && t.ManufactureCompany == company && t.Unit == unit && t.PorderNo == PMID && t.BatchNo == batchNo && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.UsedQty));
                                        GoodDetailRepository SRRepo = new GoodDetailRepository();
                                        GoodReturnDetail SRData = new GoodReturnDetail();
                                        SRData = SRRepo.GetAll().Where(t => t.ProductId == productname && t.CompanyName == company && t.Unit == unit && t.BNID == Convert.ToInt32(batchdata.BillId) && t.BatchNo == batchNo).FirstOrDefault();
                                        if (SRData != null)
                                            BSRQty = BSRQty + Convert.ToDecimal(SRData.Quantity);
                                    }
                                    else
                                        BUQty = 0;

                                    PurchaseDetailRepository purchaseRepo = new PurchaseDetailRepository();
                                    var purchaseData = purchaseRepo.GetAll().Where(t => t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.PorderNo == Convert.ToInt32(PMID) && t.BatchNo == batchNo).FirstOrDefault();
                                    if (purchaseData != null)
                                    {
                                        BPurQty = Convert.ToDecimal(purchaseRepo.GetAll().Where(t => t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.PorderNo == Convert.ToInt32(PMID) && t.BatchNo == batchNo).Sum(t => t.Quantity));
                                        FreeQty = Convert.ToDecimal(purchaseRepo.GetAll().Where(t => t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.PorderNo == Convert.ToInt32(PMID) && t.BatchNo == batchNo).Sum(t => t.FreeQty));
                                    }

                                    PReturnDetailRepository PRdetailRepo = new PReturnDetailRepository();
                                    var PRdetdata = PRdetailRepo.GetAll().Where(t => t.POID == purchaseData.Id && t.PurchaseReturnMaster.CompId == CommonMethod.CompId && t.PurchaseReturnMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    if (PRdetdata != null)
                                        BPRQty = Convert.ToDecimal(PRdetdata.Quantity);
                                    else
                                        BPRQty = 0;

                                    BRQty = (BPurQty + FreeQty) - (BUQty - BSRQty);
                                    if (BRQty == 0)
                                    {
                                        PurchaseMasterRepository mastRepo = new PurchaseMasterRepository();
                                        var purData = mastRepo.GetAll().Where(t => t.PorderNo == porderno && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                        if (purData != null)
                                        {
                                            PurchaseDetailRepository detailrepo = new PurchaseDetailRepository();
                                            var purdetaildata = detailrepo.GetAll().Where(t => t.Id == purchaseData.Id && t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.PorderNo == Convert.ToInt32(PMID) && t.BatchNo == batchNo).FirstOrDefault();
                                            if (purdetaildata != null)
                                            {
                                                int pdID = purdetaildata.Id;
                                                PurchaseDetail pdata = new PurchaseDetail();
                                                pdata = detailrepo.GetById(pdID);
                                                pdata.Status = "Inactive";
                                                detailrepo.Edit(pdata);
                                                detailrepo.Save();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        PurchaseMasterRepository mastRepo1 = new PurchaseMasterRepository();
                                        var purData1 = mastRepo1.GetAll().Where(t => t.PorderNo == porderno && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                        if (purData1 != null)
                                        {
                                            PurchaseDetailRepository detailrepo1 = new PurchaseDetailRepository();
                                            var purdetaildata1 = detailrepo1.GetAll().Where(t => t.Id == purchaseData.Id && t.ProductName == productname && t.CompanyName == company && t.Unit == unit && t.PorderNo == Convert.ToInt32(PMID) && t.BatchNo == batchNo).FirstOrDefault();
                                            if (purdetaildata1 != null)
                                            {
                                                int pdID = purdetaildata1.Id;
                                                PurchaseDetail pdata1 = new PurchaseDetail();
                                                pdata1 = detailrepo1.GetById(pdID);
                                                pdata1.Status = "Active";
                                                detailrepo1.Edit(pdata1);
                                                detailrepo1.Save();
                                            }
                                        }
                                    }
                                }
                                if (productentrytype == 9)
                                {
                                }
                                else if (productentrytype == 6)
                                {
                                }
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
                                        newsqty = prqty - Quantity;// purchase return qty
                                        newrqty = oqty + pqty - newsqty - sqty + srqty - dqty;
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
                                        sdata.SaleReturnQty = stockdata.SaleReturnQty;
                                        sdata.PurReturnQty = Convert.ToDecimal(newsqty);

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
                                    stockRepo.Dispose();
                                }
                            }
                        }
                        db.connect();
                        DataTable dt = db.GetTable("Delete from PurchaseReturnDetail where PurchaseReturnNo='" + PReturnId + "'");
                        DataTable dt1 = db.GetTable("Delete from PurchaseReturnMaster where Id='" + PReturnId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                        MessageBox.Show("Record Deleted Sucessfully.", "Success");
                        dt.Dispose();
                        dt1.Dispose();
                        db.CloseConnection();
                        GvProductInfo.Rows.Clear();
                        clear();
                        this.ActiveControl = btnnew;
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
                int qdid = Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value);
                purdetaildata = pdetailrepo.GetById(qdid);
                if (purdetaildata.Status != "Inactive")
                {
                    pocolIndex = e.RowIndex;
                    txtpname.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) + "," + Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value) + "," + Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value);
                    cmbUnit.Items.Clear();
                    cmbUnit.DisplayMember = "Text";
                    cmbUnit.ValueMember = "Value";
                    cmbUnit.Items.Add(new { Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["PurUnit"].Value), Value = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["PurUnit"].Value) });
                    cmbUnit.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["PurUnit"].Value);
                    txtQuantity.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Quantityg"].Value);
                    txtFreeQty.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["FrQty"].Value);
                    txtpurchaseprice.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Price"].Value);
                    txtamount.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Amount"].Value);
                    txtpdiscount.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Disc"].Value);
                    txtpSpcDisc.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SpDisc"].Value);
                    cmbsgst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SGST"].Value);
                    cmbcgst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["CGST"].Value);
                    cmbigst.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["IGST"].Value);
                    txtpCess.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Cess"].Value);
                    txtpTotalAmt.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["TotalAmount"].Value);
                    if (Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["BatchNo"].Value) != "")
                        txtBatchNo.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["BatchNo"].Value);
                    if (GvProductInfo.Rows[e.RowIndex].Cells["ExpiryDate"].Value != null)
                        dtpExpiry.Value = Convert.ToDateTime(GvProductInfo.Rows[e.RowIndex].Cells["ExpiryDate"].Value);
                    ProductRepository ProductRepo = new ProductRepository();
                    ProductInformation ProductData = new ProductInformation();
                    if (CommonMethod.commProduct == true)
                        ProductData = ProductRepo.GetAll().Where(t => t.ProductName == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) && t.ManufactureCompany == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value) && t.Uniti == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value)).FirstOrDefault();
                    else
                        ProductData = ProductRepo.GetAll().Where(t => t.ProductName == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) && t.ManufactureCompany == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value) && t.Uniti == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();

                    if (ProductData != null)
                        ProductCategory = ProductData.Category;
                    if (Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ThreeLevelDisc"].Value) != "" || GvProductInfo.Rows[e.RowIndex].Cells["ThreeLevelDisc"].Value != null)
                        txtOthDisc.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ThreeLevelDisc"].Value);
                    else
                        txtOthDisc.Text = "0";
                    txtReason.Text = "Purchase Return";
                    this.ActiveControl = txtQuantity;
                }
                else
                    qdid = 0;
                pdetailrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void gvpurreturnproducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvpurreturnproducts.Columns[e.ColumnIndex].HeaderText == "Remove")
                //if (e.ColumnIndex == 22)
                {
                    if (btnsave.Enabled == true)
                    {
                        if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        {
                            gvpurreturnproducts.Rows.Remove(gvpurreturnproducts.Rows[e.RowIndex]);
                            totalamt = 0;
                            totalamt = gvpurreturnproducts.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["PRTotalAmt"].Value));
                            txtTotalAmt.Text = Convert.ToString(Math.Round(totalamt, 2, MidpointRounding.AwayFromZero));
                        }
                    }
                    //else if (gvpurreturnproducts.Rows.Count != 1 && btnsave.Enabled == false)
                         else if (btnsave.Enabled == false)
                    {
                        if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        {
                            deleteid.Add(Convert.ToInt32(gvpurreturnproducts.Rows[e.RowIndex].Cells["PRIDg"].Value));
                            gvpurreturnproducts.Rows.Remove(gvpurreturnproducts.Rows[e.RowIndex]);
                            totalamt = 0;
                            totalamt = gvpurreturnproducts.Rows.Cast<DataGridViewRow>().Sum(t => Convert.ToDecimal(t.Cells["PRTotalAmt"].Value));
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