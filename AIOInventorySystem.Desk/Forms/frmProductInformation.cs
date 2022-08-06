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
    public partial class frmProductInformation : Form
    {
        DbClass db = new DbClass();
        public static frmProductInformation productinfo;
        public int updateID;
        byte[] @photoProduct;
        ToolTip btnToolTip = new ToolTip();
        CommonMethod cm = new CommonMethod();
        SettingRepository settRepo = new SettingRepository();
        Setting settData = new Setting();
        List<int> deletedId = new List<int>();
        public string OldUnit = "", strcheck = "F";
        string strCon = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
        private static int defaultproducttype = 0, defaultDepartment = 0, defaultSubcategory = 0, defaultColor = 0, preComp = 0, colIndex = -1;
        private bool slabApplied = false;

        public frmProductInformation()
        {
            InitializeComponent();
            this.ActiveControl = cmbgroupMaster;
            fillslabdesc();
            progressBar1.Visible = false;
            cmbBarcodeStatus.SelectedIndex = 0;
            chkMfgDefault.Checked = false;
            chkGroupDefault.Checked = false;
            settData = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (settData != null)
            {
                if (settData.Applyonmarathifont == "Y")
                    txtMarathiName.Font = new Font("Shivaji01", 12);
                else
                    txtMarathiName.Font = new Font("Microsoft Sans Serif", 10);
            }
            clear();
            btnSave.Text = "Save";
            chkDefGST.Checked = false;
        }

        public frmProductInformation(int Pid)
        {
            InitializeComponent();
            btnDelete.Enabled = false;
            settData = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
            if (settData != null)
            {
                if (settData.Applyonmarathifont == "Y")
                    txtMarathiName.Font = new Font("Shivaji01", 12);
                else
                    txtMarathiName.Font = new Font("Microsoft Sans Serif", 10);
            }
            clear();
            fillcombo();
            fillunit();
            fillgst();
            fillAlternateUnit();
            fillTripleUnit();
            fillgroupmaster();
            fillslabdesc();
            fillRateSlab();
            loadData(Pid);
            progressBar1.Visible = false;
        }

        public void loadData(int ProductId)
        {
            try
            {
                ProductRepository productrepo = new ProductRepository();
                ProductInformation pData = new ProductInformation();
                if (CommonMethod.commProduct == true)
                    pData = productrepo.GetAll().Where(t => t.ID == ProductId).FirstOrDefault();
                else
                    pData = productrepo.GetAll().Where(t => t.ID == ProductId && t.CompId == CommonMethod.CompId).FirstOrDefault();

                if (pData.GroupMasterId != null)
                    cmbgroupMaster.SelectedValue = pData.GroupMasterId;
                txtProductID.Text = Convert.ToString(pData.ProductCode);
                txtProductName.Text = pData.ProductName;
                cmbcompany.Text = pData.ManufactureCompany;
                cmbunit.Text = pData.Uniti;
                OldUnit = pData.Uniti;
                if (pData.AlternateUnit != null)
                {
                    try
                    {
                        db.connect();
                        SqlCommand command = new SqlCommand("SPCheckforIdUsedorNot", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "Product";
                        command.Parameters.Add("@Id", SqlDbType.VarChar).Value = ProductId;
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
                    chkAlternateUnit.Checked = true;
                    txtAlternateUnitQty.Text = "1";
                    cmbAlternateUnit.Text = pData.AlternateUnit.ToString();
                    txtUnitQty.Text = pData.UnitQty.ToString();
                    txtAlternateUnitRate.Text = pData.AlternateUnitRate.ToString();
                    SettingRepository SetRepo = new SettingRepository();
                    Setting SetData = new Setting();
                    SetData = SetRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (SetData.ThreeUnit == "True")// Checking for Three Unit Setting
                    {
                        rdbtnDouble.Visible = true;
                        rdbtnTriple.Visible = true;
                        if (pData.TripleUnit != null)
                        {
                            rdbtnTriple.Checked = true;
                            cmbTripleUnit.Text = pData.TripleUnit;
                            txtTripleUnitQty.Text = pData.TripleUnitQty.ToString();
                            txtSecondUnitQty.Text = pData.SecondUnitQty.ToString();
                            lblSecondUnit.Text = pData.AlternateUnit;
                        }
                        else
                            rdbtnDouble.Checked = true;
                    }
                    if (strcheck == "T")
                    {
                        chkAlternateUnit.Enabled = false;
                        chkBatchExpiry.Enabled = false;
                        chkWithBatch.Enabled = false;
                    }
                    else
                        txtUnitQty.Enabled = true;
                    txtUnitQty.Enabled = true;
                }
                else
                    chkAlternateUnit.Checked = false;
                if (pData.PPOnMRP == "True")
                {
                    chkpponmrp.Checked = true;
                    txtrate.Text = Convert.ToString(pData.Rate);
                    txtPurchaseRate.Text = Convert.ToString(pData.PurchaseRate);
                }
                else
                {
                    chkpponmrp.Checked = false;
                    txtPurchaseRate.Text = Convert.ToString(pData.PurchaseRate);
                    txtrate.Text = Convert.ToString(pData.Rate);
                }
                txtDealerRate.Text = Convert.ToString(pData.DealerRate);
                if (pData.TaxInclusive == "True")
                    chkInclusive.Checked = true;
                else
                    chkInclusive.Checked = false;
                txtVat.Text = Convert.ToString(pData.Vat);
                txtDiscount.Text = Convert.ToString(pData.ProductDiscount);
                ttdec.Text = pData.decription;
                cmbCategory.Text = pData.Category;
                if (pData.StockAlert != null || pData.StockAlert.ToString() != "")
                    txtStockAlert.Text = pData.StockAlert.ToString();
                else
                    txtStockAlert.Text = "0";
                if (pData.Image != null)
                {
                    @photoProduct = (byte[])pData.Image;
                    MemoryStream ms = new MemoryStream(@photoProduct);
                    pictureBox1.Image = Image.FromStream(ms);
                }
                txtRackNo.Text = pData.RackNO;
                txtMarathiName.Text = pData.MarathiName;
                txtAltrPurchaseRate.Text = Convert.ToString(pData.AlternateUnitPurchaseRate);
                txtAltrnteWholeSaleRate.Text = Convert.ToString(pData.AlternateDealerRate);
                txtPrefix.Text = pData.Prefix;
                updateID = pData.ID;
                txtHSN.Text = pData.HSNCode;
                if (!string.IsNullOrEmpty(pData.SGstPercent.ToString()))
                    cmbsgst.Text = Convert.ToString(pData.SGstPercent);
                if (!string.IsNullOrEmpty(pData.CGstPercent.ToString()))
                    cmbcgst.Text = Convert.ToString(pData.CGstPercent);
                if (!string.IsNullOrEmpty(pData.IGstPercent.ToString()))
                    cmbigst.Text = Convert.ToString(pData.IGstPercent);
                txtcess.Text = Convert.ToString(pData.Cess);
                if (pData.HasWarranty == "Y")
                {
                    txtWarranty.Text = pData.Warranty;
                    chkWarranty.Checked = true;
                }
                chkSubtitle.Checked = false;
                if (pData.Usesubtitle == "Y")
                    chkSubtitle.Checked = true;

                if (pData.BarcodeStatus == null)
                    cmbBarcodeStatus.SelectedIndex = 0;
                else
                    cmbBarcodeStatus.Text = pData.BarcodeStatus;
                txtPurDisc.Text = Convert.ToString(pData.PurchaseDiscount);
                if (string.IsNullOrEmpty(pData.Formulae))
                {
                    chkMfgUnit.Checked = false;
                    txtFormulae.Text = string.Empty;
                }
                else
                {
                    chkMfgUnit.Checked = true;
                    txtFormulae.Text = pData.Formulae;
                }
                if (pData.QtySlabId == null)
                    chkqtyslab.Checked = false;
                else
                {
                    chkqtyslab.Checked = true;
                    cmbslabdescription.SelectedValue = Convert.ToInt32(pData.QtySlabId);
                    txtvaluecount.Text = Convert.ToString(pData.SlabAppliedOnValue);
                }
                //-----------------new fields on integration
                if (!string.IsNullOrEmpty(pData.ProductType.ToString()))
                {
                    try
                    {
                        cmbProductType.SelectedIndex = Convert.ToInt32(pData.ProductType);
                    }
                    catch (Exception)
                    { }
                }
                if (pData.WithBatchAndExpiry == "Y")
                    chkBatchExpiry.Checked = true;
                else
                    chkBatchExpiry.Checked = false;
                if (pData.TaxSlab == "Y")
                    chkTaxSlab.Checked = true;
                else
                    chkTaxSlab.Checked = false;

                if (pData.WithoutSize == "Y")
                    chkWithoutsize.Checked = true;
                else
                    chkWithoutsize.Checked = false;
                if (pData.WithBatch == "Y")
                    chkWithBatch.Checked = true;
                else
                    chkWithBatch.Checked = false;
                if (!string.IsNullOrEmpty(pData.SubCatId.ToString()))
                    cmbsubcatname.SelectedValue = pData.SubCatId;
                if (!string.IsNullOrEmpty(pData.ColorId.ToString()))
                    cmbcolor.SelectedValue = pData.ColorId;
                if (!string.IsNullOrEmpty(pData.DeptId.ToString()))
                    cmbDept.SelectedValue = pData.DeptId;
                if (!string.IsNullOrEmpty(pData.RepackStatus))
                    cmbRepackStatus.Text = pData.RepackStatus;
                if (!string.IsNullOrEmpty(pData.BulkItemId.ToString()))
                    cmbbulkitem.SelectedValue = pData.BulkItemId;
                if (!string.IsNullOrEmpty(pData.RepackConversion.ToString()))
                    txtRepackConversion.Text = Convert.ToString(pData.RepackConversion);
                if (!string.IsNullOrEmpty(pData.Psgstpercent.ToString()))
                    cmbPSgst.Text = Convert.ToString(pData.Psgstpercent);
                if (!string.IsNullOrEmpty(pData.Pcgstpercent.ToString()))
                    cmbPCgst.Text = Convert.ToString(pData.Pcgstpercent);
                if (!string.IsNullOrEmpty(pData.Pigstpercent.ToString()))
                    cmbPIgst.Text = Convert.ToString(pData.Pigstpercent);
                //------------------------

                if (pData.RateSlab == "Y")
                {
                    chkRateSlab.Checked = true;
                    cmbRateSlab.SelectedValue = pData.RateSlabId;
                }
                else
                    chkRateSlab.Checked = false;
                if (pData.BarcodeStatus == "Static Barcode")
                {
                    pnldefStaticbarcode.Visible = true;
                    if (pData.StaticBarcodeDefaultInBill == "Y")
                        rdbtnYes.Checked = true;
                    else if (pData.StaticBarcodeDefaultInBill == "N")
                        rdbtnNo.Checked = true;
                }
                if (pData.QuantitySubtitle == "Y")
                {
                    chkQuantitySubtitle.Checked = true;
                    if (pData.QtySubCount != null)
                        txtQtySub.Text = pData.QtySubCount.ToString();
                }
                else
                    chkQuantitySubtitle.Checked = false;

                preComp = Convert.ToInt32(pData.CompId);
                if (pData.ProductType == 2)
                {
                    var assemblyList = pData.tblAssemblyProductDetails;
                    if (assemblyList.Count > 0)
                    {
                        int i = 0;
                        gvAssemblyProductList.Rows.Clear();
                        foreach (var item in assemblyList)
                        {
                            gvAssemblyProductList.Rows.Add();
                            gvAssemblyProductList.Rows[i].Cells[0].Value = item.ProductName;
                            gvAssemblyProductList.Rows[i].Cells[1].Value = item.CompanyName;
                            gvAssemblyProductList.Rows[i].Cells[2].Value = item.Unit;
                            gvAssemblyProductList.Rows[i].Cells[3].Value = item.Qty;
                            gvAssemblyProductList.Rows[i].Cells[5].Value = item.Id;
                            gvAssemblyProductList.Rows[i].Cells[4].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                            i++;
                        }
                    }
                }
                //////---------------------------------nilophar 10052018

                PurchaseDetailRepository purrepo = new PurchaseDetailRepository();
                PurchaseDetail purchasedata = purrepo.GetAll().Where(t => t.ProductName == pData.ProductName && t.CompanyName == pData.ManufactureCompany && t.Unit == pData.Uniti && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                if (purchasedata != null)
                {
                    chkWithBatch.Enabled = false;
                    chkBatchExpiry.Enabled = false;
                }

                CustomerBillDetailRepository custrepo = new CustomerBillDetailRepository();
                CustomerBillDetail custdtldata = custrepo.GetAll().Where(t => t.ProductId == pData.ProductName && t.CompanyName == pData.ManufactureCompany && t.Unit == pData.Uniti && t.CustomerBillMaster.CompId == CommonMethod.CompId && t.CustomerBillMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                if (custdtldata != null)
                {
                    chkWithBatch.Enabled = false;
                    chkBatchExpiry.Enabled = false;
                }
                custrepo.Dispose();
                purrepo.Dispose();
                btnSave.Text = "Update";
                btnDelete.Enabled = true;
                productrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillunit()
        {
            try
            {
                UnitRepository unitrepo = new UnitRepository();
                List<Unit> unitdata = new List<Unit>();
                unitdata.Clear();
                if (CommonMethod.commProduct == true)
                    unitdata = unitrepo.GetAll().OrderByDescending(t => t.UnitName).ToList();
                else
                    unitdata = unitrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.UnitName).ToList();
                unitdata.Add(new Unit { UnitName = "Select", UnitID = 0 });
                unitdata.Reverse();
                cmbunit.DataSource = unitdata;
                cmbunit.ValueMember = "UnitID";
                cmbunit.DisplayMember = "UnitName";
                unitrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillAlternateUnit()
        {
            try
            {
                UnitRepository unitrepo = new UnitRepository();
                List<Unit> unitdata = new List<Unit>();
                unitdata.Clear();
                if (CommonMethod.commProduct == true)
                    unitdata = unitrepo.GetAll().OrderByDescending(t => t.UnitName).ToList();
                else
                    unitdata = unitrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.UnitName).ToList();
                unitdata.Add(new Unit { UnitName = "Select", UnitID = 0 });
                unitdata.Reverse();
                cmbAlternateUnit.DataSource = unitdata;
                cmbAlternateUnit.ValueMember = "UnitID";
                cmbAlternateUnit.DisplayMember = "UnitName";
                unitrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillTripleUnit()
        {
            try
            {
                UnitRepository unitrepo = new UnitRepository();
                List<Unit> unitdata = new List<Unit>();
                unitdata.Clear();
                if (CommonMethod.commProduct == true)
                    unitdata = unitrepo.GetAll().OrderByDescending(t => t.UnitName).ToList();
                else
                    unitdata = unitrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.UnitName).ToList();
                unitdata.Add(new Unit { UnitName = "Select", UnitID = 0 });
                unitdata.Reverse();
                cmbTripleUnit.DataSource = unitdata;
                cmbTripleUnit.ValueMember = "UnitID";
                cmbTripleUnit.DisplayMember = "UnitName";
                unitrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillgroupmaster()
        {
            try
            {
                GroupMasterRepository unitrepo = new GroupMasterRepository();
                List<GroupMaster> unitdata = new List<GroupMaster>();
                unitdata.Clear();
                if (CommonMethod.commProduct == true)
                    unitdata = unitrepo.GetAll().OrderByDescending(t => t.GroupMasterName).ToList();
                else
                    unitdata = unitrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.GroupMasterName).ToList();
                unitdata.Add(new GroupMaster { GroupMasterName = "Select", GroupMasterId = 0 });
                unitdata.Reverse();
                cmbgroupMaster.DataSource = unitdata;
                cmbgroupMaster.ValueMember = "GroupMasterId";
                cmbgroupMaster.DisplayMember = "GroupMasterName";
                unitrepo.Dispose();
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

                DataTable dtsgst1 = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforSnCGST='Y'");
                if (dtsgst1.Rows.Count > 0)
                {
                    cmbPSgst.ValueMember = "GSTNo";
                    cmbPSgst.DisplayMember = "GSTPercent";
                    cmbPSgst.DataSource = dtsgst1;
                }

                DataTable dtcgst1 = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforSnCGST='Y'");
                if (dtcgst1.Rows.Count > 0)
                {
                    cmbPCgst.ValueMember = "GSTNo";
                    cmbPCgst.DisplayMember = "GSTPercent";
                    cmbPCgst.DataSource = dtcgst1;
                }

                DataTable dtigst1 = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforIGST='Y'");
                if (dtigst1.Rows.Count > 0)
                {
                    cmbPIgst.ValueMember = "GSTNo";
                    cmbPIgst.DisplayMember = "GSTPercent";
                    cmbPIgst.DataSource = dtigst1;
                }
            }
            catch (Exception) { }
        }

        private void fillsubcat()
        {
            try
            {
                SubCategoryRepository subcatrepo = new SubCategoryRepository();
                List<SubCategory> subcatdata = new List<SubCategory>();
                subcatdata.Clear();
                subcatdata = subcatrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.SubCatName).ToList();
                subcatdata.Add(new SubCategory { SubCatName = "Select", SubCatId = 0 });
                subcatdata.Reverse();
                cmbsubcatname.DataSource = subcatdata;
                cmbsubcatname.ValueMember = "SubCatId";
                cmbsubcatname.DisplayMember = "SubCatName";
            }
            catch (Exception)
            { }
        }

        private void fillcat()
        {
            try
            {
                CategoryRepository catrepo = new CategoryRepository();
                List<Category> catdata = new List<Category>();
                catdata.Clear();
                catdata = catrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CategoryName).ToList();
                catdata.Add(new Category { CategoryName = "Select", CatID = 0 });
                catdata.Reverse();
                cmbCategory.DataSource = catdata;
                cmbCategory.ValueMember = "CatID";
                cmbCategory.DisplayMember = "CategoryName";
            }
            catch (Exception)
            { }
        }

        private void FillDept()
        {
            try
            {
                DepartemetRepository deptRepo = new DepartemetRepository();
                List<Departement> deptdata = new List<Departement>();
                deptdata.Clear();
                deptdata = deptRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.DeptName).ToList();
                deptdata.Add(new Departement { DeptName = "Select", DeptNo = 0 });
                deptdata.Reverse();
                cmbDept.DataSource = deptdata;
                cmbDept.ValueMember = "DeptNo";
                cmbDept.DisplayMember = "DeptName";
            }
            catch (Exception)
            { }
        }

        private void fillColor()
        {
            try
            {
                ColorRepository colorrepo = new ColorRepository();
                List<Colorinfo> colordata = new List<Colorinfo>();
                colordata.Clear();
                colordata = colorrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.ColorName).ToList();
                colordata.Add(new Colorinfo { ColorName = "Select", ColorId = 0 });
                colordata.Reverse();
                cmbcolor.DataSource = colordata;
                cmbcolor.ValueMember = "ColorId";
                cmbcolor.DisplayMember = "ColorName";
            }
            catch (Exception)
            { }
        }

        public void fillcombo()
        {
            try
            {
                ManufactureCInfoRepository manufactureinforepo = new ManufactureCInfoRepository();
                List<ManuCompanyInfo> cdata = new List<ManuCompanyInfo>();
                cdata.Clear();
                if (CommonMethod.commProduct == true)
                    cdata = manufactureinforepo.GetAll().OrderByDescending(t => t.CompanyName).ToList();
                else
                    cdata = manufactureinforepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.CompanyName).ToList();

                cdata.Add(new ManuCompanyInfo { CompanyName = "Select", CompanyId = 0 });
                cdata.Reverse();
                cmbcompany.DataSource = cdata;
                cmbcompany.ValueMember = "CompanyId";
                cmbcompany.DisplayMember = "CompanyName";
                manufactureinforepo.Dispose();
            }
            catch (Exception)
            { }
        }

        void conv_photoproduct()
        {
            @photoProduct = null;

            if (pictureBox1.Image != null)
            {
                MemoryStream ms = new MemoryStream();
                pictureBox1.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] photo_aray = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(photo_aray, 0, photo_aray.Length);
                @photoProduct = photo_aray;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool Validated = true;
            try
            {
                if (chkAlternateUnit.Checked)
                {
                    if (txtUnitQty.Text == "" || Convert.ToDecimal(txtUnitQty.Text) == 0)
                    {
                        MessageBox.Show("Please Enter Unit Quantity.", "Warning");
                        Validated = false;
                    }
                }
            }
            catch (Exception)
            { }

            try
            {
                ProductRepository productrepo = new ProductRepository();
                ProductInformation pinfo = new ProductInformation();
                if (txtProductName.Text.Trim() != "" && cmbcompany.Text != "Select" && cmbcompany.Text != "" && Validated == true)
                {
                    if (cmbunit.Text != "Select")
                    {
                        if (txtPurchaseRate.Text.Trim() != "" && txtrate.Text.Trim() != "" && txtDealerRate.Text.Trim() != "")
                            saveProductInformation();
                        else
                        {
                            if (txtPurchaseRate.Text.Trim() == "")
                            {
                                MessageBox.Show("Enter Purchase Rate.", "Error");
                                this.ActiveControl = txtPurchaseRate;
                            }
                            else if (txtrate.Text.Trim() == "")
                            {
                                MessageBox.Show("Enter Rate.", "Error");
                                this.ActiveControl = txtrate;
                            }
                            else if (txtDealerRate.Text.Trim() == "")
                            {
                                MessageBox.Show("Enter Dealer Rate.", "Error");
                                this.ActiveControl = txtDealerRate;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select Unit.", "Error");
                        this.ActiveControl = cmbunit;
                    }
                }
                else
                {
                    if (txtProductName.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter Product Name.", "Error");
                        this.ActiveControl = txtProductName;
                    }
                    else if (cmbcompany.Text == "Select" || cmbcompany.Text == "")
                    {
                        MessageBox.Show("Select Manufacture Company.", "Error");
                        this.ActiveControl = cmbcompany;
                    }
                }
                productrepo.Dispose();
                pnlFormula.Visible = false;
            }
            catch (Exception)
            { }
        }

        public void saveProductInformation()
        {
            try
            {
                if (chkAlternateUnit.Checked)
                {
                    if (txtUnitQty.Text == "" || Convert.ToDecimal(txtUnitQty.Text) == 0)
                        MessageBox.Show("Please Enter Unit Quantity.", "Warning");
                }
                if (txtProductName.Text.Trim() == "")
                {
                    MessageBox.Show("Enter Product Name.", "Error");
                    this.ActiveControl = txtProductName;
                    goto a;
                }
                if (cmbcompany.Text == "Select" || cmbcompany.Text == "")
                {
                    MessageBox.Show("Select  Manufacture Company.", "Error");
                    this.ActiveControl = cmbcompany;
                    goto a;
                }
                if (cmbunit.Text == "Select")
                {
                    MessageBox.Show("Select Unit.", "Error");
                    this.ActiveControl = cmbunit;
                    goto a;
                }
                if (txtPurchaseRate.Text.Trim() == "")
                {
                    MessageBox.Show("Enter Purchase Rate.", "Error");
                    this.ActiveControl = txtPurchaseRate;
                    goto a;
                }
                if (txtrate.Text.Trim() == "")
                {
                    MessageBox.Show("Enter Rate.", "Error");
                    this.ActiveControl = txtrate;
                    goto a;
                }
                if (txtDealerRate.Text.Trim() == "")
                {
                    MessageBox.Show("Enter Dealer Rate.", "Error");
                    this.ActiveControl = txtDealerRate;
                    goto a;
                }
                if (cmbProductType.Text == "Assembly")
                {
                    if (gvAssemblyProductList.Rows.Count > 0)
                    { }
                    else
                    {
                        MessageBox.Show("Enter Child Product.", "Error");
                        pnlAssemblyDetails.Visible = true;
                        this.ActiveControl = txtChildProductAssembly;
                        goto a;
                    }
                }
                ProductRepository productrepo = new ProductRepository();
                ProductInformation pinfo = new ProductInformation();
                string prevproductname = "", premfgcompname = "", preunitname = "", prealternateunit = "", preTUName = "";
                decimal PreUnitQty = 0, PreAltUnitQty = 0, PreSUQty = 0, PreTUQty = 0;
                if (btnSave.Text == "Save")
                {
                    ProductRepository productrepo1 = new ProductRepository();
                    ProductInformation BillId = new ProductInformation();
                    if (CommonMethod.commProduct == true)
                        BillId = productrepo1.GetAll().Where(t => t.ProductName.Trim().ToLower() == txtProductName.Text.Trim().ToLower() && t.ManufactureCompany.Trim().ToLower() == cmbcompany.Text.Trim().ToLower() && t.Uniti.Trim().ToLower() == cmbunit.Text.Trim().ToLower()).FirstOrDefault();
                    else
                        BillId = productrepo1.GetAll().Where(t => t.ProductName.Trim().ToLower() == txtProductName.Text.Trim().ToLower() && t.ManufactureCompany.Trim().ToLower() == cmbcompany.Text.Trim().ToLower() && t.Uniti.Trim().ToLower() == cmbunit.Text.Trim().ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (BillId != null)
                    {
                        MessageBox.Show("Already Saved This Record.", "Error");
                        txtProductName.Text = "";
                        fillcombo();
                        fillunit();
                        fillAlternateUnit();
                        fillTripleUnit();
                        fillgroupmaster();
                        fillRateSlab();
                        txtrate.Text = "";
                        ttdec.Text = "";
                        this.ActiveControl = btnNew;
                    }
                    else
                    {
                        maxproductid();
                        pinfo.ProductCode = Convert.ToInt32(txtProductID.Text);
                        pinfo.ProductName = txtProductName.Text.Trim();
                        pinfo.ManufactureCompany = cmbcompany.Text.Trim();
                        pinfo.PurchaseRate = Convert.ToDecimal(txtPurchaseRate.Text);
                        pinfo.Rate = Convert.ToDecimal(txtrate.Text.Trim());
                        pinfo.DealerRate = Convert.ToDecimal(txtDealerRate.Text.Trim());
                        pinfo.Uniti = cmbunit.Text;

                        pinfo.HSNCode = txtHSN.Text;
                        if (cmbsgst.Text == "")
                            pinfo.SGstPercent = 0;
                        else
                            pinfo.SGstPercent = Convert.ToDecimal(cmbsgst.Text);
                        if (cmbcgst.Text == "")
                            pinfo.CGstPercent = 0;
                        else
                            pinfo.CGstPercent = Convert.ToDecimal(cmbcgst.Text);
                        if (cmbigst.Text == "")
                            pinfo.IGstPercent = 0;
                        else
                            pinfo.IGstPercent = Convert.ToDecimal(cmbigst.Text);
                        if (txtcess.Text == "")
                            pinfo.Cess = 0;
                        else
                            pinfo.Cess = Convert.ToDecimal(txtcess.Text);
                        if (chkAlternateUnit.Checked)
                        {
                            if (rdbtnTriple.Checked)
                            {
                                if (cmbTripleUnit.Text != "Select" && Convert.ToDecimal(txtTripleUnitQty.Text) > 0 && Convert.ToDecimal(txtSecondUnitQty.Text) > 0)
                                {
                                    pinfo.TripleUnit = cmbTripleUnit.Text;
                                    pinfo.TripleUnitQty = Convert.ToDecimal(txtTripleUnitQty.Text);
                                    pinfo.SecondUnitQty = Convert.ToDecimal(txtSecondUnitQty.Text);
                                }
                                else
                                {
                                    if (cmbTripleUnit.Text == "Select")
                                    {
                                        MessageBox.Show("Please Select Third Unit.", "Warning");
                                        this.ActiveControl = cmbTripleUnit;
                                        goto a;
                                    }
                                    else if (txtTripleUnitQty.Text == "" || Convert.ToDecimal(txtTripleUnitQty.Text) <= 0)
                                    {
                                        MessageBox.Show("Please Enter Third Unit Quantity.", "Warning");
                                        this.ActiveControl = txtTripleUnitQty;
                                        goto a;
                                    }
                                    else if (txtSecondUnitQty.Text == "" || Convert.ToDecimal(txtSecondUnitQty.Text) <= 0)
                                    {
                                        MessageBox.Show("Please Enter Second Unit Quantity.", "Warning");
                                        this.ActiveControl = txtSecondUnitQty;
                                        goto a;
                                    }
                                }
                            }
                            if (cmbAlternateUnit.Text != "Select" && Convert.ToDecimal(txtAlternateUnitQty.Text) > 0 && Convert.ToDecimal(txtUnitQty.Text) > 0 && txtAlternateUnitRate.Text != "")
                            {
                                pinfo.AlternateUnit = cmbAlternateUnit.Text;
                                pinfo.AlternateUnitQty = Convert.ToDecimal(txtAlternateUnitQty.Text);
                                pinfo.UnitQty = Convert.ToDecimal(txtUnitQty.Text);
                                pinfo.AlternateUnitRate = Convert.ToDecimal(txtAlternateUnitRate.Text);
                            }
                            else
                            {
                                if (cmbAlternateUnit.Text == "Select")
                                {
                                    MessageBox.Show("Please Select Alternate Unit.", "Warning");
                                    this.ActiveControl = cmbAlternateUnit;
                                    goto a;
                                }
                                else if (txtAlternateUnitQty.Text == "" || Convert.ToDecimal(txtAlternateUnitQty.Text) <= 0)
                                {
                                    MessageBox.Show("Please Enter Alternate Unit Quantity.", "Warning");
                                    this.ActiveControl = txtAlternateUnitQty;
                                    goto a;
                                }
                                else if (txtUnitQty.Text == "" || Convert.ToDecimal(txtUnitQty.Text) <= 0)
                                {
                                    MessageBox.Show("Please Enter Unit Quantity.", "Warning");
                                    this.ActiveControl = txtUnitQty;
                                    goto a;
                                }
                                else
                                {
                                    MessageBox.Show("Please Enter Alternate Unit Rate.", "Warning");
                                    this.ActiveControl = txtAlternateUnitRate;
                                    goto a;
                                }
                            }
                        }
                        pinfo.CategoryId = 0;
                        if (cmbProductType.SelectedIndex == 0) //standard
                            pinfo.Category = "N/A";
                        else if (cmbProductType.SelectedIndex == 1) //serialized
                            pinfo.Category = "Motor";
                        else
                        {
                            pinfo.Category = cmbCategory.Text;
                            pinfo.CategoryId = cmbCategory.SelectedIndex;
                        }
                        if (chkMfgUnit.Checked)
                        {
                            if (txtFormulae.Text != "")
                                pinfo.Formulae = txtFormulae.Text.Trim();
                            else
                            {
                                MessageBox.Show("Please Enter Formulae for Manufacture Unit.", "Warning");
                                this.ActiveControl = txtFormulae;
                                goto a;
                            }
                            if (cmbslabdescription.SelectedIndex > 0)
                                pinfo.QtySlabId = Convert.ToInt32(cmbslabdescription.SelectedValue);
                            else
                                pinfo.QtySlabId = null;

                            if (txtvaluecount.Text == "") { txtvaluecount.Text = "0"; }
                            pinfo.SlabAppliedOnValue = Convert.ToInt32(txtvaluecount.Text);
                        }
                        pinfo.StockAlert = Convert.ToInt32(txtStockAlert.Text);
                        pinfo.decription = ttdec.Text.Trim();
                        if (txtVat.Text == "")
                            pinfo.Vat = 0;
                        else
                            pinfo.Vat = Convert.ToDecimal(txtVat.Text);
                        if (txtDiscount.Text == "")
                            pinfo.ProductDiscount = 0;
                        else
                            pinfo.ProductDiscount = Convert.ToDecimal(txtDiscount.Text);

                        if (chkInclusive.Checked)
                            pinfo.TaxInclusive = "True";
                        else
                            pinfo.TaxInclusive = "False";

                        if (chkpponmrp.Checked)
                            pinfo.PPOnMRP = "True";
                        else
                            pinfo.PPOnMRP = "False";
                        conv_photoproduct();
                        pinfo.Image = @photoProduct;
                        if (cmbgroupMaster.Text != "Select")
                            pinfo.GroupMasterId = Convert.ToInt32(cmbgroupMaster.SelectedValue);
                        pinfo.RackNO = txtRackNo.Text;
                        pinfo.MarathiName = txtMarathiName.Text;
                        if (!string.IsNullOrEmpty(txtAltrPurchaseRate.Text))
                            pinfo.AlternateUnitPurchaseRate = Convert.ToDecimal(txtAltrPurchaseRate.Text);
                        else
                            pinfo.AlternateUnitPurchaseRate = 0;
                        if (!string.IsNullOrEmpty(txtAltrnteWholeSaleRate.Text))
                            pinfo.AlternateDealerRate = Convert.ToDecimal(txtAltrnteWholeSaleRate.Text);
                        else
                            pinfo.AlternateDealerRate = 0;
                        pinfo.Prefix = txtPrefix.Text;
                        if (chkSubtitle.Checked == true)
                            pinfo.Usesubtitle = "Y";
                        else
                            pinfo.Usesubtitle = "N";
                        if (chkWarranty.Checked == true)
                        {
                            pinfo.HasWarranty = "Y";
                            pinfo.Warranty = txtWarranty.Text;
                        }
                        else
                        {
                            pinfo.HasWarranty = "N";
                            pinfo.Warranty = "";
                        }
                        pinfo.BarcodeStatus = cmbBarcodeStatus.Text;
                        pinfo.PurchaseDiscount = Convert.ToDecimal(txtPurDisc.Text == "" ? "0" : txtPurDisc.Text);
                        //-------------new field on integration 01/03/2018
                        pinfo.ProductType = cmbProductType.SelectedIndex;
                        if (chkTaxSlab.Checked == true)
                            pinfo.TaxSlab = "Y";
                        else
                            pinfo.TaxSlab = "N";
                        if (chkWithoutsize.Checked == true)
                            pinfo.WithoutSize = "Y";
                        else
                            pinfo.WithoutSize = "N";
                        if (chkWithBatch.Checked == true)
                            pinfo.WithBatch = "Y";
                        else
                            pinfo.WithBatch = "N";
                        if (chkBatchExpiry.Checked == true)
                            pinfo.WithBatchAndExpiry = "Y";
                        else
                            pinfo.WithBatchAndExpiry = "N";
                        if (cmbProductType.SelectedIndex == 3)
                        {
                            pinfo.SubCatId = Convert.ToInt32(cmbsubcatname.SelectedValue);
                            pinfo.SubCatName = cmbsubcatname.Text;
                            pinfo.DeptId = Convert.ToInt32(cmbDept.SelectedValue);
                            pinfo.DeptName = cmbDept.Text;
                            pinfo.ColorId = Convert.ToInt32(cmbcolor.SelectedValue);
                            pinfo.ColorName = cmbcolor.Text;
                        }
                        if (!string.IsNullOrEmpty(cmbRepackStatus.Text))
                        {
                            pinfo.RepackStatus = cmbRepackStatus.Text;
                            pinfo.BulkItemId = Convert.ToInt32(cmbbulkitem.SelectedValue);
                            pinfo.RepackConversion = Convert.ToDecimal(txtRepackConversion.Text);
                        }
                        else
                        {
                            pinfo.RepackStatus = "";
                            pinfo.BulkItemId = 0;
                            pinfo.RepackConversion = 0;
                        }
                        if (cmbPCgst.Text != "")
                            pinfo.Pcgstpercent = Convert.ToDecimal(cmbPCgst.Text);
                        if (cmbPSgst.Text != "")
                            pinfo.Psgstpercent = Convert.ToDecimal(cmbPSgst.Text);
                        if (cmbPIgst.Text != "")
                            pinfo.Pigstpercent = Convert.ToDecimal(cmbPIgst.Text);
                        //------------------------end of new fields

                        if (chkRateSlab.Checked == true)
                        {
                            pinfo.RateSlab = "Y";
                            pinfo.RateSlabId = Convert.ToInt32(cmbRateSlab.SelectedValue);
                        }
                        else
                            pinfo.RateSlab = "N";
                        if (cmbBarcodeStatus.Text == "Static Barcode")
                        {
                            if (rdbtnYes.Checked == true)
                                pinfo.StaticBarcodeDefaultInBill = "Y";
                            else if (rdbtnNo.Checked == true)
                                pinfo.StaticBarcodeDefaultInBill = "N";
                        }
                        if (chkQuantitySubtitle.Checked == true)
                        {
                            pinfo.QuantitySubtitle = "Y";
                            pinfo.QtySubCount = Convert.ToDecimal(txtQtySub.Text);
                        }
                        else
                        {
                            pinfo.QuantitySubtitle = "N";
                            pinfo.QtySubCount = 0;
                        }

                        pinfo.CompId = CommonMethod.CompId;
                        productrepo1.Add(pinfo);
                        productrepo1.Save();
                        if (cmbProductType.SelectedIndex == 2)
                        {
                            if (gvAssemblyProductList.Rows.Count > 0)
                            {
                                for (int i = 0; i < gvAssemblyProductList.Rows.Count; i++)
                                {
                                    AssemeblyProductDetailsRepository assemblyPDetailRepo = new AssemeblyProductDetailsRepository();
                                    tblAssemblyProductDetail assemblyPDData = new tblAssemblyProductDetail();
                                    assemblyPDData.AssemblyProductId = pinfo.ID;
                                    assemblyPDData.ProductName = Convert.ToString(gvAssemblyProductList.Rows[i].Cells[0].Value);
                                    assemblyPDData.CompanyName = Convert.ToString(gvAssemblyProductList.Rows[i].Cells[1].Value);
                                    assemblyPDData.Unit = Convert.ToString(gvAssemblyProductList.Rows[i].Cells[2].Value);
                                    assemblyPDData.Qty = Convert.ToDecimal(gvAssemblyProductList.Rows[i].Cells[3].Value);
                                    assemblyPDData.CompId = CommonMethod.CompId;
                                    assemblyPDetailRepo.Add(assemblyPDData);
                                    assemblyPDetailRepo.Save();
                                }
                            }
                        }
                        MessageBox.Show("Product Information Saved.", "Success");
                        clear();
                        this.ActiveControl = cmbgroupMaster;
                        productrepo1.Dispose();
                    }
                }
                else
                {
                    try
                    {
                        ProductRepository prdtrepo = new ProductRepository();
                        ProductInformation PreProductData = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            PreProductData = prdtrepo.GetAll().Where(t => t.ProductCode == Convert.ToInt32(txtProductID.Text)).FirstOrDefault();
                        else
                            PreProductData = prdtrepo.GetAll().Where(t => t.ProductCode == Convert.ToInt32(txtProductID.Text) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        prevproductname = Convert.ToString(PreProductData.ProductName);
                        premfgcompname = Convert.ToString(PreProductData.ManufactureCompany);
                        preunitname = Convert.ToString(PreProductData.Uniti);
                        PreUnitQty = Convert.ToDecimal(PreProductData.UnitQty);
                        if (PreProductData.AlternateUnit != null)
                        {
                            prealternateunit = Convert.ToString(PreProductData.AlternateUnit);
                            PreAltUnitQty = Convert.ToDecimal(PreProductData.AlternateUnitQty);
                        }
                        else
                        {
                            prealternateunit = "";
                            PreAltUnitQty = 1;
                        }
                        if (PreProductData.TripleUnit != null)
                        {
                            preTUName = Convert.ToString(PreProductData.TripleUnit);
                            PreTUQty = Convert.ToDecimal(PreProductData.TripleUnitQty);
                            PreSUQty = Convert.ToDecimal(PreProductData.SecondUnitQty);
                        }
                        else
                        {
                            preTUName = "";
                            PreTUQty = 1;
                            PreSUQty = 1;
                        }
                        prdtrepo.Dispose();
                    }
                    catch (Exception)
                    { }
                    ProductRepository productrepo1 = new ProductRepository();
                    ProductInformation BillId = new ProductInformation();
                    if (CommonMethod.commProduct == true)
                        BillId = productrepo1.GetAll().Where(t => t.ProductName.Trim().ToLower() == txtProductName.Text.Trim().ToLower() && t.ManufactureCompany.ToLower() == cmbcompany.Text.ToLower() && t.Uniti.ToLower() == cmbunit.Text.ToLower() && t.ProductCode != Convert.ToInt32(txtProductID.Text)).FirstOrDefault();
                    else
                        BillId = productrepo1.GetAll().Where(t => t.ProductName.Trim().ToLower() == txtProductName.Text.Trim().ToLower() && t.ManufactureCompany.ToLower() == cmbcompany.Text.ToLower() && t.Uniti.ToLower() == cmbunit.Text.ToLower() && t.ProductCode != Convert.ToInt32(txtProductID.Text) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (BillId != null)
                    {
                        MessageBox.Show("Already Saved This Record.", "Error");
                        this.ActiveControl = txtProductName;
                    }
                    else
                    {
                        pinfo.ProductCode = Convert.ToInt32(txtProductID.Text);
                        pinfo.ProductName = txtProductName.Text.Trim();
                        pinfo.ManufactureCompany = cmbcompany.Text;
                        pinfo.PurchaseRate = Convert.ToDecimal(txtPurchaseRate.Text);
                        pinfo.Rate = Convert.ToDecimal(txtrate.Text.Trim());
                        pinfo.DealerRate = Convert.ToDecimal(txtDealerRate.Text.Trim());
                        pinfo.Uniti = cmbunit.Text;
                        pinfo.HSNCode = txtHSN.Text;
                        if (cmbsgst.Text == "")
                            pinfo.SGstPercent = 0;
                        else
                            pinfo.SGstPercent = Convert.ToDecimal(cmbsgst.Text);
                        if (cmbcgst.Text == "")
                            pinfo.CGstPercent = 0;
                        else
                            pinfo.CGstPercent = Convert.ToDecimal(cmbcgst.Text);
                        if (cmbigst.Text == "")
                            pinfo.IGstPercent = 0;
                        else
                            pinfo.IGstPercent = Convert.ToDecimal(cmbigst.Text);
                        if (txtcess.Text == "")
                            pinfo.Cess = 0;
                        else
                            pinfo.Cess = Convert.ToDecimal(txtcess.Text);
                        if (chkAlternateUnit.Checked)
                        {
                            if (rdbtnTriple.Checked)
                            {
                                if (cmbTripleUnit.Text != "Select" && Convert.ToDecimal(txtTripleUnitQty.Text) > 0 && Convert.ToDecimal(txtSecondUnitQty.Text) > 0)
                                {
                                    pinfo.TripleUnit = cmbTripleUnit.Text;
                                    pinfo.TripleUnitQty = Convert.ToDecimal(txtTripleUnitQty.Text);
                                    pinfo.SecondUnitQty = Convert.ToDecimal(txtSecondUnitQty.Text);
                                }
                                else
                                {
                                    if (cmbTripleUnit.Text == "Select")
                                    {
                                        MessageBox.Show("Please Select Third Unit.", "Warning");
                                        this.ActiveControl = cmbTripleUnit;
                                        goto a;
                                    }
                                    else if (txtTripleUnitQty.Text == "" || Convert.ToDecimal(txtTripleUnitQty.Text) <= 0)
                                    {
                                        MessageBox.Show("Please Enter Third Unit Quantity.", "Warning");
                                        this.ActiveControl = txtTripleUnitQty;
                                        goto a;
                                    }
                                    else if (txtSecondUnitQty.Text == "" || Convert.ToDecimal(txtSecondUnitQty.Text) <= 0)
                                    {
                                        MessageBox.Show("Please Enter Second Unit Quantity.", "Warning");
                                        this.ActiveControl = txtSecondUnitQty;
                                        goto a;
                                    }
                                }
                            }
                            if (cmbAlternateUnit.Text != "Select" && Convert.ToDecimal(txtAlternateUnitQty.Text) > 0 && Convert.ToDecimal(txtUnitQty.Text) > 0 && txtAlternateUnitRate.Text != "")
                            {
                                pinfo.AlternateUnit = cmbAlternateUnit.Text;
                                pinfo.AlternateUnitQty = Convert.ToDecimal(txtAlternateUnitQty.Text);
                                pinfo.UnitQty = Convert.ToDecimal(txtUnitQty.Text);
                                pinfo.AlternateUnitRate = Convert.ToDecimal(txtAlternateUnitRate.Text);
                            }
                            else
                            {
                                if (cmbAlternateUnit.Text == "Select")
                                {
                                    MessageBox.Show("Please Select Alternate Unit.", "Warning");
                                    this.ActiveControl = cmbAlternateUnit;
                                    goto a;
                                }
                                else if (txtAlternateUnitQty.Text == "" || Convert.ToDecimal(txtAlternateUnitQty.Text) <= 0)
                                {
                                    MessageBox.Show("Please Enter Alternate Unit Quantity.", "Warning");
                                    this.ActiveControl = txtAlternateUnitQty;
                                    goto a;
                                }
                                else if (txtUnitQty.Text == "" || Convert.ToDecimal(txtUnitQty.Text) <= 0)
                                {
                                    MessageBox.Show("Please Enter Unit Quantity.", "Warning");
                                    this.ActiveControl = txtUnitQty;
                                    goto a;
                                }
                                else
                                {
                                    MessageBox.Show("Please Enter Alternate Unit Rate.", "Warning");
                                    this.ActiveControl = txtAlternateUnitRate;
                                    goto a;
                                }
                            }
                        }
                        if (chkMfgUnit.Checked)
                        {
                            if (txtFormulae.Text != "")
                                pinfo.Formulae = txtFormulae.Text.Trim();
                            else
                            {
                                MessageBox.Show("Please Enter Formulae for Manufacture Unit.", "Warning");
                                this.ActiveControl = txtFormulae;
                                goto a;
                            }
                            if (cmbslabdescription.SelectedIndex > 0)
                                pinfo.QtySlabId = Convert.ToInt32(cmbslabdescription.SelectedValue);
                            else
                                pinfo.QtySlabId = null;
                            if (txtvaluecount.Text == "") { txtvaluecount.Text = "0"; }
                            pinfo.SlabAppliedOnValue = Convert.ToInt32(txtvaluecount.Text);
                        }
                        pinfo.StockAlert = Convert.ToInt32(txtStockAlert.Text);
                        pinfo.decription = ttdec.Text.Trim();
                        if (txtVat.Text == "")
                            pinfo.Vat = 0;
                        else
                            pinfo.Vat = Convert.ToDecimal(txtVat.Text);
                        if (txtDiscount.Text == "")
                            pinfo.ProductDiscount = 0;
                        else
                            pinfo.ProductDiscount = Convert.ToDecimal(txtDiscount.Text);

                        if (chkInclusive.Checked)
                            pinfo.TaxInclusive = "True";
                        else
                            pinfo.TaxInclusive = "False";

                        if (chkpponmrp.Checked)
                            pinfo.PPOnMRP = "True";
                        else
                            pinfo.PPOnMRP = "False";

                        conv_photoproduct();
                        pinfo.Image = @photoProduct;
                        if (cmbgroupMaster.Text != "Select")
                            pinfo.GroupMasterId = Convert.ToInt32(cmbgroupMaster.SelectedValue);
                        pinfo.RackNO = txtRackNo.Text;
                        pinfo.MarathiName = txtMarathiName.Text;
                        if (!string.IsNullOrEmpty(txtAltrPurchaseRate.Text))
                            pinfo.AlternateUnitPurchaseRate = Convert.ToDecimal(txtAltrPurchaseRate.Text);
                        else
                            pinfo.AlternateUnitPurchaseRate = 0;
                        if (!string.IsNullOrEmpty(txtAltrnteWholeSaleRate.Text))
                            pinfo.AlternateDealerRate = Convert.ToDecimal(txtAltrnteWholeSaleRate.Text);
                        else
                            pinfo.AlternateDealerRate = 0;
                        pinfo.Prefix = txtPrefix.Text;
                        if (chkSubtitle.Checked == true)
                            pinfo.Usesubtitle = "Y";
                        else
                            pinfo.Usesubtitle = "N";
                        pinfo.ID = updateID;
                        if (chkWarranty.Checked == true)
                        {
                            pinfo.HasWarranty = "Y";
                            pinfo.Warranty = txtWarranty.Text;
                        }
                        else
                        {
                            pinfo.HasWarranty = "N";
                            pinfo.Warranty = "";
                        }
                        pinfo.BarcodeStatus = cmbBarcodeStatus.Text;
                        pinfo.PurchaseDiscount = Convert.ToDecimal(txtPurDisc.Text == "" ? "0" : txtPurDisc.Text);
                        //-------------new field on integration 01/03/2018
                        pinfo.ProductType = cmbProductType.SelectedIndex;
                        pinfo.CategoryId = 0;
                        if (cmbProductType.SelectedIndex == 0) //standard
                            pinfo.Category = "N/A";
                        else if (cmbProductType.SelectedIndex == 1) //serialized
                            pinfo.Category = "Motor";
                        else
                        {
                            pinfo.Category = cmbCategory.Text;
                            pinfo.CategoryId = cmbCategory.SelectedIndex;
                        }
                        if (chkTaxSlab.Checked == true)
                            pinfo.TaxSlab = "Y";
                        else
                            pinfo.TaxSlab = "N";
                        if (chkWithoutsize.Checked == true)
                            pinfo.WithoutSize = "Y";
                        else
                            pinfo.WithoutSize = "N";
                        if (chkWithBatch.Checked == true)
                            pinfo.WithBatch = "Y";
                        else
                            pinfo.WithBatch = "N";
                        if (chkBatchExpiry.Checked == true)
                            pinfo.WithBatchAndExpiry = "Y";
                        else
                            pinfo.WithBatchAndExpiry = "N";
                        if (cmbProductType.SelectedIndex == 3)
                        {
                            pinfo.SubCatId = Convert.ToInt32(cmbsubcatname.SelectedValue);
                            pinfo.SubCatName = cmbsubcatname.Text;
                            pinfo.DeptId = Convert.ToInt32(cmbDept.SelectedValue);
                            pinfo.DeptName = cmbDept.Text;
                            pinfo.ColorId = Convert.ToInt32(cmbcolor.SelectedValue);
                            pinfo.ColorName = cmbcolor.Text;
                        }
                        else
                        {
                            pinfo.SubCatId = 0;
                            pinfo.SubCatName = "";
                            pinfo.DeptId = 0;
                            pinfo.DeptName = "";
                            pinfo.ColorId = 0;
                            pinfo.ColorName = "";
                        }
                        if (!string.IsNullOrEmpty(cmbRepackStatus.Text))
                        {
                            pinfo.RepackStatus = cmbRepackStatus.Text;
                            pinfo.BulkItemId = Convert.ToInt32(cmbbulkitem.SelectedValue);
                            pinfo.RepackConversion = Convert.ToDecimal(txtRepackConversion.Text);
                        }
                        else
                        {
                            pinfo.RepackStatus = "";
                            pinfo.BulkItemId = 0;
                            pinfo.RepackConversion = 0;
                        }
                        if (cmbPCgst.Text != "")
                            pinfo.Pcgstpercent = Convert.ToDecimal(cmbPCgst.Text);
                        if (cmbPSgst.Text != "")
                            pinfo.Psgstpercent = Convert.ToDecimal(cmbPSgst.Text);
                        if (cmbPIgst.Text != "")
                            pinfo.Pigstpercent = Convert.ToDecimal(cmbPIgst.Text);
                        //------------------------end of new fields
                        if (chkRateSlab.Checked == true)
                        {
                            pinfo.RateSlab = "Y";
                            pinfo.RateSlabId = Convert.ToInt32(cmbRateSlab.SelectedValue);
                        }
                        else
                            pinfo.RateSlab = "N";
                        if (cmbBarcodeStatus.Text == "Static Barcode")
                        {
                            if (rdbtnYes.Checked == true)
                                pinfo.StaticBarcodeDefaultInBill = "Y";
                            else if (rdbtnNo.Checked == true)
                                pinfo.StaticBarcodeDefaultInBill = "N";
                        }
                        if (chkQuantitySubtitle.Checked == true)
                        {
                            pinfo.QuantitySubtitle = "Y";
                            pinfo.QtySubCount = Convert.ToDecimal(txtQtySub.Text);
                        }
                        else
                        {
                            pinfo.QuantitySubtitle = "N";
                            pinfo.QtySubCount = 0;
                        }
                        pinfo.CompId = preComp;  // on update company not changed , it remains as it is, whatever stored at first time
                        productrepo.Edit(pinfo);
                        productrepo.Save();
                        if (prevproductname != txtProductName.Text.Trim() || premfgcompname != cmbcompany.Text || preunitname != cmbunit.Text || prealternateunit != cmbAlternateUnit.Text || PreUnitQty.ToString() != txtUnitQty.Text || PreAltUnitQty.ToString() != txtAlternateUnitQty.Text || preTUName != cmbTripleUnit.Text || PreSUQty.ToString() != txtSecondUnitQty.ToString() || PreTUQty.ToString() != txtTripleUnitQty.ToString())
                        {
                            try
                            {
                                string alternateunit = "", TripleUnitName = "";
                                if (cmbAlternateUnit.SelectedIndex != 0)
                                    alternateunit = cmbAlternateUnit.Text;
                                if (cmbTripleUnit.SelectedIndex != 0)
                                    TripleUnitName = cmbTripleUnit.Text;

                                db.connect();
                                SqlCommand command = new SqlCommand("SPUpdateProductName1", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = prevproductname;
                                command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = premfgcompname;
                                command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = preunitname;
                                command.Parameters.Add("@PreAUName", SqlDbType.VarChar).Value = prealternateunit;
                                command.Parameters.Add("@NewPName", SqlDbType.VarChar).Value = txtProductName.Text.Trim();
                                command.Parameters.Add("@NewCName", SqlDbType.VarChar).Value = cmbcompany.Text;
                                command.Parameters.Add("@NewUName", SqlDbType.VarChar).Value = cmbunit.Text;
                                command.Parameters.Add("@NewAUName", SqlDbType.VarChar).Value = alternateunit;
                                command.Parameters.Add("@PreUQty", SqlDbType.Decimal).Value = Convert.ToDecimal(PreUnitQty);
                                command.Parameters.Add("@PreAlUQty", SqlDbType.Decimal).Value = Convert.ToDecimal(PreAltUnitQty);
                                command.Parameters.Add("@PreTUName", SqlDbType.VarChar).Value = preTUName;
                                command.Parameters.Add("@NewTUName", SqlDbType.VarChar).Value = TripleUnitName;
                                command.Parameters.Add("@PreSUQty", SqlDbType.Decimal).Value = Convert.ToDecimal(PreSUQty);
                                command.Parameters.Add("@PreTUQty", SqlDbType.Decimal).Value = Convert.ToDecimal(PreTUQty);
                                command.Parameters.Add("@TransactionYear", SqlDbType.VarChar).Value = CommonMethod.TransactionYear;
                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                command.ExecuteNonQuery();
                                prevproductname = "";
                                premfgcompname = "";
                                preunitname = "";
                                prealternateunit = "";
                                db.CloseConnection();
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
                        }
                        if (cmbProductType.SelectedIndex == 2)
                        {
                            if (deletedId.Count > 0)
                            {
                                for (int x = 0; x < deletedId.Count; x++)
                                {
                                    AssemeblyProductDetailsRepository assemblyDRepo = new AssemeblyProductDetailsRepository();
                                    tblAssemblyProductDetail assemblyDData = assemblyDRepo.GetById(deletedId[x]);
                                    assemblyDRepo.Remove(assemblyDData);
                                    assemblyDRepo.Save();
                                }
                            }
                            if (gvAssemblyProductList.Rows.Count > 0)
                            {
                                for (int i = 0; i < gvAssemblyProductList.Rows.Count; i++)
                                {
                                    if (Convert.ToInt32(gvAssemblyProductList.Rows[i].Cells[5].Value) != 0)
                                    {
                                        AssemeblyProductDetailsRepository assemblyPDetailRepo = new AssemeblyProductDetailsRepository();
                                        tblAssemblyProductDetail assemblyPDData = assemblyPDetailRepo.GetById(Convert.ToInt32(gvAssemblyProductList.Rows[i].Cells[5].Value));
                                        assemblyPDData.AssemblyProductId = pinfo.ID;
                                        assemblyPDData.ProductName = Convert.ToString(gvAssemblyProductList.Rows[i].Cells[0].Value);
                                        assemblyPDData.CompanyName = Convert.ToString(gvAssemblyProductList.Rows[i].Cells[1].Value);
                                        assemblyPDData.Unit = Convert.ToString(gvAssemblyProductList.Rows[i].Cells[2].Value);
                                        assemblyPDData.Qty = Convert.ToDecimal(gvAssemblyProductList.Rows[i].Cells[3].Value);
                                        assemblyPDData.CompId = CommonMethod.CompId;
                                        assemblyPDetailRepo.Edit(assemblyPDData);
                                        assemblyPDetailRepo.Save();
                                    }
                                    else
                                    {
                                        AssemeblyProductDetailsRepository assemblyPDetailRepo = new AssemeblyProductDetailsRepository();
                                        tblAssemblyProductDetail assemblyPDData = new tblAssemblyProductDetail();
                                        assemblyPDData.AssemblyProductId = pinfo.ID;
                                        assemblyPDData.ProductName = Convert.ToString(gvAssemblyProductList.Rows[i].Cells[0].Value);
                                        assemblyPDData.CompanyName = Convert.ToString(gvAssemblyProductList.Rows[i].Cells[1].Value);
                                        assemblyPDData.Unit = Convert.ToString(gvAssemblyProductList.Rows[i].Cells[2].Value);
                                        assemblyPDData.Qty = Convert.ToDecimal(gvAssemblyProductList.Rows[i].Cells[3].Value);
                                        assemblyPDData.CompId = CommonMethod.CompId;
                                        assemblyPDetailRepo.Add(assemblyPDData);
                                        assemblyPDetailRepo.Save();
                                    }
                                }
                            }
                        }
                        MessageBox.Show("Product Information Updated.", "Success");
                        clear();
                        this.ActiveControl = cmbgroupMaster;
                    }
                }
            a: { }
            }
            catch (Exception) { }
        }

        public void maxproductid()
        {
            try
            {
                ProductRepository productrepo = new ProductRepository();
                var mid = 0;
                if (CommonMethod.commProduct == true)
                    mid = Convert.ToInt32(productrepo.GetAll().Max(t => t.ProductCode));
                else
                    mid = Convert.ToInt32(productrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.ProductCode));
                if (mid == null)
                    txtProductID.Text = "1";
                else
                    txtProductID.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                productrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                btnSave.Text = "Save";
                AutoCompleteProducts();
                AutoCompleteChildProduct();
                if (chkGroupDefault.Checked)
                { }
                else
                    fillgroupmaster();
                if (chkMfgDefault.Checked == true)
                { }
                else
                {
                    fillcombo();
                    fillunit();
                    fillAlternateUnit();
                    fillTripleUnit();
                }
                txtProductName.Text = "";
                txtUnitQty.Text = "0";
                txtAlternateUnitQty.Text = "0";
                txtSecondUnitQty.Text = "0";
                lblSecondUnit.Text = "";
                txtAlternateUnitRate.Text = "0";
                txtPurchaseRate.Text = "0";
                txtrate.Text = "0";
                ttdec.Text = "";
                txtVat.Text = "0";
                txtDiscount.Text = "0";
                txtDealerRate.Text = "0";
                cmbCategory.Text = "";
                txtStockAlert.Text = "0";
                txtcess.Text = "0";
                txtHSN.Text = "";
                maxproductid();
                btnDelete.Enabled = false;
                pictureBox1.Image = null;
                cmbunit.Enabled = true;
                chkAlternateUnit.Checked = false;
                txtretailratepersent.Text = "";
                txtdealerratepersent.Text = "";
                txtpurpriceopercent.Text = "";
                chkpponmrp.Checked = false;
                txtRackNo.Text = "";
                txtMarathiName.Text = "";
                txtPrefix.Text = "";
                txtAltrnteWholeSaleRate.Text = "0";
                txtAltrPurchaseRate.Text = "0";
                chkSubtitle.Checked = false;
                txtWarranty.Text = "";
                txtWarranty.Enabled = false;
                chkWarranty.Checked = false;
                cmbBarcodeStatus.SelectedIndex = 0;
                txtPurDisc.Text = "0";
                pnldefStaticbarcode.Visible = false;
                rdbtnYes.Checked = false;
                rdbtnNo.Checked = false;
                if (chkDefGST.Checked)
                { }
                else
                    fillgst();
                chkTaxSlab.Checked = false;
                chkWithBatch.Checked = false;
                chkBatchExpiry.Checked = false;
                tabControl1.SelectedIndex = 0;
                btnDelete.Enabled = false;
                chkMfgUnit.Checked = false;
                pnlFormula.Visible = false;
                FillDept();
                fillColor();
                fillsubcat();
                fillcat();
                fillRateSlab();
                cmbProductType.SelectedIndex = 0;
                cmbDept.SelectedIndex = defaultDepartment;
                cmbcolor.SelectedIndex = defaultColor;
                cmbsubcatname.SelectedIndex = defaultSubcategory;
                cmbProductType.SelectedIndex = defaultproducttype;
                if (defaultproducttype == 0)
                {
                    chkWithBatch.Enabled = true;
                    chkWithoutsize.Enabled = true;
                    chkBatchExpiry.Enabled = true;
                    pnlAssemblyDetails.Visible = false;
                }
                cmbRepackStatus.SelectedIndex = -1;
                cmbbulkitem.SelectedIndex = -1; ;
                txtRepackConversion.Text = "0";
                chkInclusive.Checked = true;
                chkRateSlab.Checked = false;
                cmbRateSlab.SelectedIndex = -1;
                cmbRateSlab.Enabled = false;
                preComp = 0;
                txtChildProductAssembly.Text = "";
                txtChildQty.Text = "";
                cmbChildUnit.Text = "";
                gvAssemblyProductList.Rows.Clear();
                cmbRepackStatus.Enabled = true;
                cmbbulkitem.Enabled = true;
                txtRepackConversion.Enabled = true;
                chkRateSlab.Enabled = true;
                chkQuantitySubtitle.Checked = false;
                txtQtySub.Enabled = false;
                txtQtySub.Text = "0";
                
                this.ActiveControl = txtProductName;
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbunit_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbunit.Text != "Select")
                {
                    UnitRepository unitrepo = new UnitRepository();
                    Unit unitdat = new Unit();
                    if (CommonMethod.commProduct == true)
                        unitdat = unitrepo.GetAll().Where(t => t.UnitName == cmbunit.Text).FirstOrDefault();
                    else
                        unitdat = unitrepo.GetAll().Where(t => t.UnitName == cmbunit.Text && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (unitdat == null)
                    {
                        MessageBox.Show("Invalid Unit.", "Warning");
                        cmbunit.Text = "Select";
                        this.ActiveControl = cmbunit;
                    }
                    unitrepo.Dispose();
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
            else if (keyData == Keys.F3)
            {
                saveProductInformation();
                return true;
            }
            else if (keyData == Keys.Tab)
            {
                if (pnlFormula.Visible == true && slabApplied == true)
                {
                    pnlFormula.Visible = false;
                    slabApplied = false;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                RptProductInformation productrpt = new RptProductInformation();
                productrpt.ShowDialog();
                productrpt.Dispose();
            }
            catch (Exception)
            { }
        }

        private void txtpname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                if (tb != null)
                {
                    int pos = tb.SelectionStart;
                    int length = tb.Text.Length;
                    tb.Text = tb.Text.Replace(",", "");
                    int diff = length - tb.Text.Length;
                    tb.SelectionStart = pos == 0 || diff == 0 ? pos : pos - diff;
                    txtMarathiName.Text = "";
                }
                TextBox tb1 = sender as TextBox;
                if (tb1 != null)
                {
                    int pos = tb1.SelectionStart;
                    int length = tb1.Text.Length;
                    tb1.Text = tb1.Text.Replace("'", "");
                    int diff = length - tb1.Text.Length;
                    tb1.SelectionStart = pos == 0 || diff == 0 ? pos : pos - diff;
                }
            }
            catch (Exception)
            { }
        }

        private void btnlist_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                productinfo = this;
                frmProductInformation p = new frmProductInformation();
                p.Visible = false;
                frmAllProductInformation allpro = new frmAllProductInformation();
                allpro.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void cmbcompany_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    //if (cmbProductType.SelectedIndex == 3)
                    //    this.ActiveControl = cmbDept;
                    //else
                        this.ActiveControl = cmbunit;
                }
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmManufactureCompanyInfo company = new frmManufactureCompanyInfo();
                        company.ShowDialog();
                        fillcombo();
                        this.ActiveControl = cmbcompany;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbunit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = chkAlternateUnit;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmUnitInformation unit = new frmUnitInformation();
                        unit.ShowDialog();
                        fillunit();
                        this.ActiveControl = cmbunit;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void frmProductInformation_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                btnToolTip.SetToolTip(btnAddGroup, "Click to add Group Master");
                btnToolTip.SetToolTip(btnAddMfgComp, "Click to add Mfg. Company");
                btnToolTip.SetToolTip(btnAddUnit, "Click to add Unit");
                btnToolTip.SetToolTip(txtretailratepersent, "Enter Retail Rate Persent On Purchase Rate");
                btnToolTip.SetToolTip(txtdealerratepersent, "Enter Dealer Rate Persent On Purchase Rate");
                SettingRepository sRepo = new SettingRepository();
                Setting sdata = new Setting();
                sdata = sRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (sdata != null)
                {
                    if (sdata.Applyonmarathifont == "Y")
                        txtMarathiName.Font = new Font("Shivaji01", 12);
                    else
                        txtMarathiName.Font = new Font("Microsoft Sans Serif", 10);
                }
                AutoCompleteProducts();
                AutoCompleteChildProduct();
            }
            catch (Exception)
            { }
        }

        public void AutoCompleteProducts()
        {
            try
            {
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = null;
                    if (CommonMethod.commProduct == true)
                        cmd = new SqlCommand("SELECT ProductName FROM ProductInformation", con);
                    else
                        cmd = new SqlCommand("SELECT ProductName FROM ProductInformation where compid=" + CommonMethod.CompId, con);
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
                    txtProductName.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
            try
            {
                string cnString2 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con2 = new SqlConnection(cnString2))
                {
                    SqlCommand cmd2 = null;
                    if (CommonMethod.commProduct == true)
                        cmd2 = new SqlCommand("SELECT Prefix FROM ProductInformation where Prefix!=''", con2);
                    else
                        cmd2 = new SqlCommand("SELECT Prefix FROM ProductInformation where Prefix!='' and compid=" + CommonMethod.CompId, con2);

                    con2.Open();
                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    AutoCompleteStringCollection MyCollection2 = new AutoCompleteStringCollection();
                    while (reader2.Read())
                    {
                        try
                        {
                            MyCollection2.Add(reader2.GetString(0));
                        }
                        catch (Exception)
                        { }
                    }
                    txtPrefix.AutoCompleteCustomSource = MyCollection2;
                    con2.Close();
                    reader2.Dispose();
                }
            }
            catch (Exception)
            { }
        }
        public void AutoCompleteChildProduct()
        {
            try
            {
                txtChildProductAssembly.Text = "";
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
                    txtChildProductAssembly.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception) { }
        }

        private void cmbcompany_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbcompany.Text != "Select")
                {
                    ManufactureCInfoRepository unitrepo = new ManufactureCInfoRepository();
                    ManuCompanyInfo unitdat = new ManuCompanyInfo();
                    if (CommonMethod.commProduct == true)
                        unitdat = unitrepo.GetAll().Where(t => t.CompanyName == cmbcompany.Text).FirstOrDefault();
                    else
                        unitdat = unitrepo.GetAll().Where(t => t.CompanyName == cmbcompany.Text && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (unitdat == null)
                    {
                        MessageBox.Show("Invalid Company Name.", "Warning");
                        fillcombo();
                        this.ActiveControl = cmbcompany;
                    }
                    unitrepo.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void chkAlternateUnit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAlternateUnit.Checked == true)
                {
                    cmbRepackStatus.Enabled = false;
                    txtRepackConversion.Enabled = false;
                    cmbbulkitem.Enabled = false;
                    if (cmbunit.SelectedIndex != 0)
                    {
                        SettingRepository SetRepo = new SettingRepository();
                        Setting SetData = new Setting();
                        SetData = SetRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (SetData.ThreeUnit == "True")// Checking for Three Unit Setting
                        {
                            rdbtnDouble.Visible = true;
                            rdbtnTriple.Visible = true;
                            this.ActiveControl = rdbtnDouble;
                        }
                        else
                            this.ActiveControl = cmbAlternateUnit;
                        txtAlternateUnitQty.Text = "1";
                        cmbAlternateUnit.Enabled = true;
                        txtUnitQty.Enabled = true;
                        txtAlternateUnitRate.Enabled = true;
                        txtAltrnteWholeSaleRate.Enabled = true;
                        txtAltrPurchaseRate.Enabled = true;
                        lblUnit.Text = cmbunit.Text;
                        if (btnSave.Text == "Update" && strcheck == "F" && cmbAlternateUnit.SelectedIndex == 0)
                            cmbAlternateUnit.Text = OldUnit;
                    }
                    else
                    {
                        MessageBox.Show("Please Select Unit.", "Warning");
                        chkAlternateUnit.Checked = false;
                        this.ActiveControl = cmbunit;
                    }
                }
                else
                {
                    cmbRepackStatus.Enabled = true;
                    txtRepackConversion.Enabled = true;
                    cmbbulkitem.Enabled = true;
                    txtAlternateUnitQty.Text = "0";
                    txtUnitQty.Text = "0";
                    txtAlternateUnitRate.Text = "0";
                    txtAlternateUnitQty.Enabled = false;
                    cmbAlternateUnit.Enabled = false;
                    txtUnitQty.Enabled = false;
                    txtAlternateUnitRate.Enabled = false;
                    txtAltrnteWholeSaleRate.Enabled = false;
                    txtAltrPurchaseRate.Enabled = false;
                    lblUnit.Text = "";
                    this.ActiveControl = cmbCategory;
                }
            }
            catch (Exception)
            { }
        }

        private void txtAlternateUnitQty_Leave(object sender, EventArgs e)
        {
            try
            {
                if (chkAlternateUnit.Checked)
                {
                    if (txtAlternateUnitQty.Text == "" || Convert.ToDecimal(txtAlternateUnitQty.Text) == 0)
                    {
                        MessageBox.Show("Please Enter Alternate Unit Quantity.", "Warning");
                        this.ActiveControl = txtAlternateUnitQty;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbAlternateUnit_Leave(object sender, EventArgs e)
        {
            try
            {
                if (chkAlternateUnit.Checked)
                {
                    SettingRepository SetRepo = new SettingRepository();
                    Setting SetData = new Setting();
                    SetData = SetRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (SetData.ThreeUnit == "True")// Checking for Three Unit Setting
                    {
                        if (rdbtnTriple.Checked == true)
                        {
                            if (cmbAlternateUnit.Text != "Select" || cmbAlternateUnit.Text != "")
                                lblSecondUnit.Text = cmbAlternateUnit.Text;
                        }
                    }
                    else if (cmbAlternateUnit.Text == "" || cmbAlternateUnit.Text == "Select")
                        MessageBox.Show("Please Select Alternate Unit.", "Warning");
                }
            }
            catch (Exception)
            { }
        }

        private void txtUnitQty_Leave(object sender, EventArgs e)
        {
            try
            {
                if (chkAlternateUnit.Checked)
                {
                    if (cmbAlternateUnit.Text == "" || cmbAlternateUnit.Text == "Select")
                        MessageBox.Show("Please Select Alternate Unit.", "Warning");
                    else if (txtUnitQty.Text == "" || Convert.ToDecimal(txtUnitQty.Text) == 0)
                        MessageBox.Show("Please Enter Unit Quantity.", "Warning");
                    else if (rdbtnTriple.Checked == true)
                        this.ActiveControl = cmbTripleUnit;
                }
            }
            catch (Exception)
            { }
        }

        private void txtAlternateUnitRate_Leave(object sender, EventArgs e)
        {
            try
            {
                if (chkAlternateUnit.Checked)
                {
                    if (txtAlternateUnitRate.Text == "")
                    {
                        MessageBox.Show("Please Enter Alternate Unit Rate Quantity.", "Warning");
                        this.ActiveControl = txtAlternateUnitRate;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbunit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAlternateUnit.Checked)
                    lblUnit.Text = cmbunit.Text;
                else
                    lblUnit.Text = "";
            }
            catch (Exception)
            { }
        }

        private void txtUnitQty_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Update")
                {
                    if (OldUnit == cmbAlternateUnit.Text)
                    {
                        txtPurchaseRate.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtPurchaseRate.Text) / Convert.ToDecimal(txtUnitQty.Text), 2, MidpointRounding.AwayFromZero));
                        txtrate.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtrate.Text) / Convert.ToDecimal(txtUnitQty.Text), 2, MidpointRounding.AwayFromZero));
                        txtDealerRate.Text = Convert.ToString(Math.Round(Convert.ToDecimal(txtDealerRate.Text) / Convert.ToDecimal(txtUnitQty.Text), 2, MidpointRounding.AwayFromZero));
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbAlternateUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbAlternateUnit.SelectedIndex != 0)
                {
                    if (cmbAlternateUnit.Text == cmbunit.Text)
                    {
                        MessageBox.Show("Main Unit and Alternate Unit must be different", "Warning");
                        cmbAlternateUnit.Text = "Select";
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtrate_Leave(object sender, EventArgs e)
        {
            try
            {
                if (chkpponmrp.Checked == true)
                {
                    if (txtrate.Text != "")
                    {
                        if (txtpurpriceopercent.Text != "")
                        {
                            decimal damt = (Convert.ToDecimal(txtrate.Text) * Convert.ToDecimal(txtpurpriceopercent.Text)) / 100;
                            decimal r = Convert.ToDecimal(txtrate.Text) + damt;
                            txtPurchaseRate.Text = Convert.ToString(Math.Round(r, 2, MidpointRounding.AwayFromZero));
                        }
                        if (txtdealerratepersent.Text != "")
                        {
                            decimal damt = (Convert.ToDecimal(txtrate.Text) * Convert.ToDecimal(txtdealerratepersent.Text)) / 100;
                            decimal r = Convert.ToDecimal(txtrate.Text) + damt;
                            txtDealerRate.Text = Convert.ToString(Math.Round(r, 2, MidpointRounding.AwayFromZero));
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtretailratepersent_Leave(object sender, EventArgs e)
        {
            try
            {
                if (chkpponmrp.Checked == false)
                {
                    if (txtPurchaseRate.Text != "" && txtretailratepersent.Text != "")
                    {
                        decimal damt = (Convert.ToDecimal(txtPurchaseRate.Text) * Convert.ToDecimal(txtretailratepersent.Text)) / 100;
                        decimal r = Convert.ToDecimal(txtPurchaseRate.Text) + damt;
                        txtrate.Text = Convert.ToString(Math.Round(r, 2, MidpointRounding.AwayFromZero));
                    }
                    else
                    {
                        if (txtPurchaseRate.Text == "")
                        {
                            MessageBox.Show("Please Enter Purchase Rate.", "Warning");
                            txtPurchaseRate.Text = "0";
                            this.ActiveControl = txtPurchaseRate;
                        }
                        else
                        {
                            txtretailratepersent.Text = "0";
                            this.ActiveControl = txtretailratepersent;
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtdealerratepersent_Leave(object sender, EventArgs e)
        {
            try
            {
                if (chkpponmrp.Checked == false)
                {
                    if (txtPurchaseRate.Text != "" && txtdealerratepersent.Text != "")
                    {
                        decimal damt = (Convert.ToDecimal(txtPurchaseRate.Text) * Convert.ToDecimal(txtdealerratepersent.Text)) / 100;
                        decimal r = Convert.ToDecimal(txtPurchaseRate.Text) + damt;
                        txtDealerRate.Text = Convert.ToString(Math.Round(r, 2, MidpointRounding.AwayFromZero));
                    }
                    else
                    {
                        if (txtPurchaseRate.Text == "")
                        {
                            MessageBox.Show("Please Enter Purchase Rate.", "Warning");
                            txtPurchaseRate.Text = "0";
                            this.ActiveControl = txtPurchaseRate;
                        }
                        else
                        {
                            txtdealerratepersent.Text = "0";
                            this.ActiveControl = txtdealerratepersent;
                        }
                    }
                }
                else
                {
                    if (txtrate.Text != "" && txtdealerratepersent.Text != "")
                    {
                        decimal damt = (Convert.ToDecimal(txtrate.Text) * Convert.ToDecimal(txtdealerratepersent.Text)) / 100;
                        decimal r = Convert.ToDecimal(txtrate.Text) + damt;
                        txtDealerRate.Text = Convert.ToString(Math.Round(r, 2, MidpointRounding.AwayFromZero));
                    }
                    else
                    {
                        if (txtrate.Text == "")
                        {
                            MessageBox.Show("Please Enter MRP Rate.", "Warning");
                            txtrate.Text = "0";
                            this.ActiveControl = txtrate;
                        }
                        else
                        {
                            txtdealerratepersent.Text = "0";
                            this.ActiveControl = txtdealerratepersent;
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtPurchaseRate_Leave(object sender, EventArgs e)
        {
            try
            {
                if (chkpponmrp.Checked == false)
                {
                    if (txtPurchaseRate.Text != "")
                    {
                        if (txtretailratepersent.Text != "")
                        {
                            decimal damt = (Convert.ToDecimal(txtPurchaseRate.Text) * Convert.ToDecimal(txtretailratepersent.Text)) / 100;
                            decimal r = Convert.ToDecimal(txtPurchaseRate.Text) + damt;
                            txtrate.Text = Convert.ToString(Math.Round(r, 2, MidpointRounding.AwayFromZero));
                        }
                        if (txtdealerratepersent.Text != "")
                        {
                            decimal damt = (Convert.ToDecimal(txtPurchaseRate.Text) * Convert.ToDecimal(txtdealerratepersent.Text)) / 100;
                            decimal r = Convert.ToDecimal(txtPurchaseRate.Text) + damt;
                            txtDealerRate.Text = Convert.ToString(Math.Round(r, 2, MidpointRounding.AwayFromZero));
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtrate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkpponmrp.Checked == false)
                {
                    if (txtrate.Text != "")
                    {
                        decimal damt = (Convert.ToDecimal(txtrate.Text) - Convert.ToDecimal(txtPurchaseRate.Text));
                        decimal r = (damt * 100) / Convert.ToDecimal(txtPurchaseRate.Text);
                        txtretailratepersent.Text = Convert.ToString(Math.Round(r, 2, MidpointRounding.AwayFromZero));
                    }
                }
                else
                    txtretailratepersent.Text = "";
            }
            catch (Exception)
            { }
        }

        private void txtDealerRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkpponmrp.Checked == false)
                {
                    if (txtDealerRate.Text != "")
                    {
                        decimal damt = (Convert.ToDecimal(txtDealerRate.Text) - Convert.ToDecimal(txtPurchaseRate.Text));
                        decimal r = (damt * 100) / Convert.ToDecimal(txtPurchaseRate.Text);
                        txtdealerratepersent.Text = Convert.ToString(Math.Round(r, 2, MidpointRounding.AwayFromZero));
                    }
                }
                else
                {
                    if (txtDealerRate.Text != "")
                    {
                        decimal damt = (Convert.ToDecimal(txtDealerRate.Text) - Convert.ToDecimal(txtrate.Text));
                        decimal r = (damt * 100) / Convert.ToDecimal(txtrate.Text);
                        txtdealerratepersent.Text = Convert.ToString(Math.Round(r, 2, MidpointRounding.AwayFromZero));
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtretailratepersent_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            try
            {
                int i = e.KeyChar;
                if (i == 46 || (i > 47 && i <= 57) || i == 8 || i == 45)
                { }
                else
                    e.Handled = true;
            }
            catch (Exception)
            { }
        }

        private void txtdealerratepersent_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            try
            {
                int i = e.KeyChar;
                if (i == 46 || (i > 47 && i <= 57) || i == 8 || i == 45)
                { }
                else
                    e.Handled = true;
            }
            catch (Exception)
            { }
        }

        private void txtpurpriceopercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            try
            {
                int i = e.KeyChar;
                if (i == 46 || (i > 47 && i <= 57) || i == 8 || i == 45)
                { }
                else
                    e.Handled = true;
            }
            catch (Exception)
            { }
        }

        private void txtpurpriceopercent_Leave(object sender, EventArgs e)
        {
            try
            {
                if (chkpponmrp.Checked == true)
                {
                    if (txtrate.Text != "" && txtpurpriceopercent.Text != "")
                    {
                        decimal damt = (Convert.ToDecimal(txtrate.Text) * Convert.ToDecimal(txtpurpriceopercent.Text)) / 100;
                        decimal r = Convert.ToDecimal(txtrate.Text) + damt;
                        txtPurchaseRate.Text = Convert.ToString(Math.Round(r, 2, MidpointRounding.AwayFromZero));
                    }
                    else
                    {
                        if (txtrate.Text == "")
                        {
                            MessageBox.Show("Please Enter MRP Rate.", "Warning");
                            txtrate.Text = "0";
                            this.ActiveControl = txtrate;
                        }
                        else
                        {
                            txtpurpriceopercent.Text = "0";
                            this.ActiveControl = txtpurpriceopercent;
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtPurchaseRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkpponmrp.Checked == true)
                {
                    if (txtPurchaseRate.Text != "")
                    {
                        decimal damt = (Convert.ToDecimal(txtPurchaseRate.Text) - Convert.ToDecimal(txtrate.Text));
                        decimal r = (damt * 100) / Convert.ToDecimal(txtrate.Text);
                        txtpurpriceopercent.Text = Convert.ToString(Math.Round(r, 2, MidpointRounding.AwayFromZero));
                    }
                }
                else
                    txtpurpriceopercent.Text = "";
            }
            catch (Exception)
            { }
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
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
                    OleDbDataAdapter oleAdpt = new OleDbDataAdapter("SELECT * FROM [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "]", con); //here we read data from sheet1  
                    oleAdpt.Fill(dtexcel); //fill excel data into dataTable  F10,F11,F14,F17,F18,F19,F20,F21 
                    int i;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = dtexcel.Rows.Count;
                    progressBar1.Visible = true;
                    for (i = 0; i < dtexcel.Rows.Count; i++)
                    {
                        try
                        {
                            System.Windows.Forms.Application.DoEvents();
                            progressBar1.Value = i;
                            string a = Convert.ToString(dtexcel.Rows[i][2].ToString());
                            string b = Convert.ToString(dtexcel.Rows[i][3].ToString());
                            string c = Convert.ToString(dtexcel.Rows[i][5].ToString());
                            string d = Convert.ToString(dtexcel.Rows[i][6].ToString());
                            if (!string.IsNullOrEmpty(Convert.ToString(dtexcel.Rows[i][2].ToString())) && !string.IsNullOrEmpty(Convert.ToString(dtexcel.Rows[i][3].ToString())) && !string.IsNullOrEmpty(Convert.ToString(dtexcel.Rows[i][5].ToString())) && !string.IsNullOrEmpty(Convert.ToString(dtexcel.Rows[i][6].ToString())))
                            {
                                string Proname1 = Convert.ToString(dtexcel.Rows[i][3].ToString());
                                string Proname = "";
                                if ((Proname1).Contains("'"))
                                    Proname = Proname1.Replace("'", "");
                                else
                                    Proname = Proname1;
                                string Proname3 = "";
                                if ((Proname).Contains(","))
                                    Proname3 = Proname.Replace(",", "");
                                else
                                    Proname3 = Proname;
                                ProductInformation pinfo = new ProductInformation();
                                ProductRepository productrepo1 = new ProductRepository();
                                ProductInformation pinfo1 = new ProductInformation();
                                if (CommonMethod.commProduct == true)
                                    pinfo1 = productrepo1.GetAll().Where(t => t.ProductName.Trim().ToLower() == b.Trim().ToLower() && t.ManufactureCompany.ToLower() == c.ToLower() && t.Uniti.ToLower() == d.ToLower()).FirstOrDefault();
                                else
                                    pinfo1 = productrepo1.GetAll().Where(t => t.ProductName.Trim().ToLower() == b.Trim().ToLower() && t.ManufactureCompany.ToLower() == c.ToLower() && t.Uniti.ToLower() == d.ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (pinfo1 != null)
                                {
                                    pinfo1.ProductType = Convert.ToInt32(dtexcel.Rows[i][2].ToString());
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][8].ToString()))
                                        pinfo1.PurchaseRate = Convert.ToDecimal(dtexcel.Rows[i][8].ToString());
                                    else
                                        pinfo1.PurchaseRate = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][9].ToString()))
                                        pinfo1.DealerRate = Convert.ToDecimal(dtexcel.Rows[i][9].ToString());
                                    else
                                        pinfo1.DealerRate = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][10].ToString()))
                                        pinfo1.Rate = Convert.ToDecimal(dtexcel.Rows[i][10].ToString());
                                    else
                                        pinfo1.Rate = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][12].ToString()))
                                        pinfo1.AlternateUnitQty = 1;
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(dtexcel.Rows[i][11].ToString()))
                                            pinfo1.AlternateUnitQty = 1;
                                        else
                                            pinfo1.AlternateUnitQty = null;
                                    }
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][7].ToString()))
                                        pinfo1.UnitQty = Convert.ToDecimal(dtexcel.Rows[i][7].ToString());
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(dtexcel.Rows[i][11].ToString()))
                                            pinfo1.UnitQty = 1;
                                        else
                                            pinfo1.UnitQty = null;
                                    }
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][15].ToString()))
                                        pinfo1.AlternateUnitRate = Convert.ToDecimal(dtexcel.Rows[i][15].ToString());
                                    else
                                        pinfo1.AlternateUnitRate = null;
                                    pinfo1.MarathiName = Convert.ToString(dtexcel.Rows[i][4].ToString());
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][11].ToString()))
                                        pinfo1.AlternateUnit = Convert.ToString(dtexcel.Rows[i][11].ToString());
                                    else
                                        pinfo1.AlternateUnit = null;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][13].ToString()))
                                        pinfo1.AlternateUnitPurchaseRate = Convert.ToDecimal(dtexcel.Rows[i][13].ToString());
                                    else
                                        pinfo1.AlternateUnitPurchaseRate = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][14].ToString()))
                                        pinfo1.AlternateDealerRate = Convert.ToDecimal(dtexcel.Rows[i][14].ToString());
                                    else
                                        pinfo1.AlternateDealerRate = 0;
                                    pinfo1.Prefix = Convert.ToString(dtexcel.Rows[i][1].ToString());
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][30].ToString()))
                                        pinfo1.Category = Convert.ToString(dtexcel.Rows[i][30].ToString());
                                    else
                                        pinfo1.Category = "N/A";
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][31].ToString()))
                                        pinfo1.StockAlert = Convert.ToInt32(dtexcel.Rows[i][31].ToString());
                                    else
                                        pinfo1.StockAlert = 0;
                                    pinfo1.RackNO = Convert.ToString(dtexcel.Rows[i][32].ToString());
                                    pinfo1.HSNCode = Convert.ToString(dtexcel.Rows[i][26].ToString());
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][16].ToString()))
                                        pinfo1.SGstPercent = Convert.ToDecimal(dtexcel.Rows[i][16].ToString());
                                    else
                                        pinfo1.SGstPercent = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][17].ToString()))
                                        pinfo1.CGstPercent = Convert.ToDecimal(dtexcel.Rows[i][17].ToString());
                                    else
                                        pinfo1.CGstPercent = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][18].ToString()))
                                        pinfo1.IGstPercent = Convert.ToDecimal(dtexcel.Rows[i][18].ToString());
                                    else
                                        pinfo1.IGstPercent = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][19].ToString()))
                                        pinfo1.ProductDiscount = Convert.ToDecimal(dtexcel.Rows[i][19].ToString());
                                    else
                                        pinfo1.ProductDiscount = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][20].ToString()))
                                        pinfo1.Psgstpercent = Convert.ToDecimal(dtexcel.Rows[i][20].ToString());
                                    else
                                        pinfo1.Psgstpercent = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][21].ToString()))
                                        pinfo1.Pcgstpercent = Convert.ToDecimal(dtexcel.Rows[i][21].ToString());
                                    else
                                        pinfo1.Pcgstpercent = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][22].ToString()))
                                        pinfo1.Pigstpercent = Convert.ToDecimal(dtexcel.Rows[i][22].ToString());
                                    else
                                        pinfo1.Pigstpercent = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][23].ToString()))
                                        pinfo1.PurchaseDiscount = Convert.ToDecimal(dtexcel.Rows[i][23].ToString());
                                    else
                                        pinfo1.PurchaseDiscount = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][24].ToString()))
                                        pinfo1.Cess = Convert.ToDecimal(dtexcel.Rows[i][24].ToString());
                                    else
                                        pinfo1.Cess = 0;
                                    pinfo1.decription = Convert.ToString(dtexcel.Rows[i][33].ToString());
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][25].ToString()))
                                        pinfo1.TaxInclusive = Convert.ToString(dtexcel.Rows[i][25].ToString());
                                    else
                                        pinfo1.TaxInclusive = "True";
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][27].ToString()))
                                        pinfo1.BarcodeStatus = Convert.ToString(dtexcel.Rows[i][27].ToString());
                                    else
                                        pinfo1.BarcodeStatus = "No Barcode";
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][28].ToString()))
                                        pinfo1.WithBatch = Convert.ToString(dtexcel.Rows[i][28].ToString());
                                    else
                                        pinfo1.WithBatch = "N";
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][29].ToString()))
                                        pinfo1.WithBatchAndExpiry = Convert.ToString(dtexcel.Rows[i][29].ToString());
                                    else
                                        pinfo1.WithBatchAndExpiry = "N";
                                    pinfo1.CompId = CommonMethod.CompId;
                                    pinfo1.QuantitySubtitle = "N";
                                    productrepo1.Edit(pinfo1);
                                    productrepo1.Save();
                                    productrepo1.Dispose();
                                }
                                else
                                {
                                    maxproductid();
                                    pinfo.ProductCode = Convert.ToInt32(txtProductID.Text);
                                    pinfo.ProductType = Convert.ToInt32(dtexcel.Rows[i][2].ToString());
                                    pinfo.ProductName = Proname3;
                                    pinfo.ManufactureCompany = Convert.ToString(dtexcel.Rows[i][5].ToString());
                                    SaveCompany(Convert.ToString(dtexcel.Rows[i][5].ToString()));
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][8].ToString()))
                                        pinfo.PurchaseRate = Convert.ToDecimal(dtexcel.Rows[i][8].ToString());
                                    else
                                        pinfo.PurchaseRate = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][10].ToString()))
                                        pinfo.Rate = Convert.ToDecimal(dtexcel.Rows[i][10].ToString());
                                    else
                                        pinfo.Rate = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][9].ToString()))
                                        pinfo.DealerRate = Convert.ToDecimal(dtexcel.Rows[i][9].ToString());
                                    else
                                        pinfo.DealerRate = 0;
                                    pinfo.Uniti = Convert.ToString(dtexcel.Rows[i][6].ToString());
                                    SaveUnit(Convert.ToString(dtexcel.Rows[i][6].ToString()));
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][11].ToString()))
                                        pinfo.AlternateUnit = Convert.ToString(dtexcel.Rows[i][11].ToString());
                                    else
                                        pinfo.AlternateUnit = null;
                                    SaveUnit(Convert.ToString(dtexcel.Rows[i][11].ToString()));
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][12].ToString()))
                                        pinfo.AlternateUnitQty = 1;
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(dtexcel.Rows[i][11].ToString()))
                                            pinfo.AlternateUnitQty = 1;
                                        else
                                            pinfo.AlternateUnitQty = null;
                                    }
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][7].ToString()))
                                        pinfo.UnitQty = Convert.ToDecimal(dtexcel.Rows[i][7].ToString());
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(dtexcel.Rows[i][11].ToString()))
                                            pinfo.UnitQty = 1;
                                        else
                                            pinfo.UnitQty = null;
                                    }
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][15].ToString()))
                                        pinfo.AlternateUnitRate = Convert.ToDecimal(dtexcel.Rows[i][14].ToString());
                                    else
                                        pinfo.AlternateUnitRate = null;
                                    pinfo.MarathiName = Convert.ToString(dtexcel.Rows[i][4].ToString());
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][13].ToString()))
                                        pinfo.AlternateUnitPurchaseRate = Convert.ToDecimal(dtexcel.Rows[i][13].ToString());
                                    else
                                        pinfo.AlternateUnitPurchaseRate = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][14].ToString()))
                                        pinfo.AlternateDealerRate = Convert.ToDecimal(dtexcel.Rows[i][14].ToString());
                                    else
                                        pinfo.AlternateDealerRate = 0;
                                    pinfo.Prefix = Convert.ToString(dtexcel.Rows[i][1].ToString());
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][30].ToString()))
                                        pinfo.Category = Convert.ToString(dtexcel.Rows[i][30].ToString());
                                    else
                                        pinfo.Category = "N/A";
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][31].ToString()))
                                        pinfo.StockAlert = Convert.ToInt32(dtexcel.Rows[i][31].ToString());
                                    else
                                        pinfo.StockAlert = 0;
                                    pinfo.RackNO = Convert.ToString(dtexcel.Rows[i][32].ToString());
                                    pinfo.HSNCode = Convert.ToString(dtexcel.Rows[i][26].ToString());
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][16].ToString()))
                                        pinfo.SGstPercent = Convert.ToDecimal(dtexcel.Rows[i][16].ToString());
                                    else
                                        pinfo.SGstPercent = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][17].ToString()))
                                        pinfo.CGstPercent = Convert.ToDecimal(dtexcel.Rows[i][17].ToString());
                                    else
                                        pinfo.CGstPercent = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][18].ToString()))
                                        pinfo.IGstPercent = Convert.ToDecimal(dtexcel.Rows[i][18].ToString());
                                    else
                                        pinfo.IGstPercent = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][19].ToString()))
                                        pinfo.ProductDiscount = Convert.ToDecimal(dtexcel.Rows[i][19].ToString());
                                    else
                                        pinfo.ProductDiscount = 0;

                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][20].ToString()))
                                        pinfo.Psgstpercent = Convert.ToDecimal(dtexcel.Rows[i][20].ToString());
                                    else
                                        pinfo.Psgstpercent = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][21].ToString()))
                                        pinfo.Pcgstpercent = Convert.ToDecimal(dtexcel.Rows[i][21].ToString());
                                    else
                                        pinfo.Pcgstpercent = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][22].ToString()))
                                        pinfo.Pigstpercent = Convert.ToDecimal(dtexcel.Rows[i][22].ToString());
                                    else
                                        pinfo.Pigstpercent = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][23].ToString()))
                                        pinfo.PurchaseDiscount = Convert.ToDecimal(dtexcel.Rows[i][23].ToString());
                                    else
                                        pinfo.PurchaseDiscount = 0;
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][24].ToString()))
                                        pinfo.Cess = Convert.ToDecimal(dtexcel.Rows[i][24].ToString());
                                    else
                                        pinfo.Cess = 0;
                                    pinfo.decription = Convert.ToString(dtexcel.Rows[i][33].ToString());
                                    pinfo.QuantitySubtitle = "N";
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][25].ToString()))
                                        pinfo.TaxInclusive = Convert.ToString(dtexcel.Rows[i][25].ToString());
                                    else
                                        pinfo.TaxInclusive = "True";
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][27].ToString()))
                                        pinfo.BarcodeStatus = Convert.ToString(dtexcel.Rows[i][27].ToString());
                                    else
                                        pinfo.BarcodeStatus = "No Barcode";
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][28].ToString()))
                                        pinfo.WithBatch = Convert.ToString(dtexcel.Rows[i][28].ToString());
                                    else
                                        pinfo.WithBatch = "N";
                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][29].ToString()))
                                        pinfo.WithBatchAndExpiry = Convert.ToString(dtexcel.Rows[i][29].ToString());
                                    else
                                        pinfo.WithBatchAndExpiry = "N";
                                    pinfo.PPOnMRP = "False";
                                    pinfo.Vat = 0;
                                    pinfo.Image = null;
                                    pinfo.Usesubtitle = "N";
                                    pinfo.HasWarranty = "N";
                                    pinfo.Warranty = "";
                                    pinfo.TaxSlab = "N";
                                    pinfo.WithoutSize = "N";
                                    pinfo.BulkItemId = 0;
                                    pinfo.RepackConversion = 0;
                                    pinfo.RepackStatus = "";
                                    pinfo.RateSlab = "N";
                                    pinfo.CompId = CommonMethod.CompId;

                                    if (!string.IsNullOrEmpty(dtexcel.Rows[i][0].ToString()))
                                    {
                                        SaveGroup(Convert.ToString(dtexcel.Rows[i][0].ToString()));
                                        GroupMasterRepository gMastRepo = new GroupMasterRepository();
                                        int gId = 0;
                                        try
                                        {
                                            gId = gMastRepo.GetAll().Where(t => t.GroupMasterName == Convert.ToString(dtexcel.Rows[i][0].ToString()) && t.CompId == CommonMethod.CompId).FirstOrDefault().GroupMasterId;
                                        }
                                        catch (Exception)
                                        { }
                                        gMastRepo.Dispose();
                                        pinfo.GroupMasterId = gId;
                                    }
                                    productrepo1.Add(pinfo);
                                    productrepo1.Save();
                                    productrepo1.Dispose();
                                }
                            }
                        }
                        catch (Exception)
                        { }
                    }
                    progressBar1.Visible = false;
                }
                catch { }
            }
            return dtexcel;
        }

        public void SaveGroup(string gName)
        {
            try
            {
                GroupMasterRepository unitrepo = new GroupMasterRepository();
                var supp = unitrepo.GetAll().Where(t => t.GroupMasterName.ToLower() == gName.ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (supp != null)
                { }
                else
                {
                    GroupMaster unitinfo = new GroupMaster();
                    unitinfo.GroupMasterName = gName;
                    unitinfo.Description = null;
                    unitinfo.CompId = CommonMethod.CompId;
                    unitrepo.Add(unitinfo);
                    unitrepo.Save();
                    unitrepo.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        public void SaveCompany(string companyName)
        {
            try
            {
                ManufactureCInfoRepository manufactureinforepo = new ManufactureCInfoRepository();
                ManuCompanyInfo BillId = new ManuCompanyInfo();
                if (CommonMethod.commProduct == true)
                    BillId = manufactureinforepo.GetAll().Where(t => t.CompanyName.Trim().ToLower() == companyName.Trim().ToLower()).FirstOrDefault();
                else
                    BillId = manufactureinforepo.GetAll().Where(t => t.CompanyName.Trim().ToLower() == companyName.Trim().ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();

                if (BillId != null)
                { }
                else
                {
                    ManufactureCInfoRepository manufactureinforepo1 = new ManufactureCInfoRepository();
                    ManuCompanyInfo companyinfo = new ManuCompanyInfo();
                    int n = companymaxid();
                    companyinfo.CompanyId = n;
                    companyinfo.CompanyName = companyName.Trim();
                    companyinfo.CompanyAddress = null;
                    companyinfo.ContactNo = null;
                    companyinfo.StockAlert = 0;
                    companyinfo.CompId = CommonMethod.CompId;
                    manufactureinforepo1.Add(companyinfo);
                    manufactureinforepo1.Save();
                    manufactureinforepo1.Dispose();
                }
                manufactureinforepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public int companymaxid()
        {
            try
            {
                int n;
                var mid = 0;
                ManufactureCInfoRepository manufactureinforepo = new ManufactureCInfoRepository();
                if (CommonMethod.commProduct == true)
                    mid = Convert.ToInt32(manufactureinforepo.GetAll().Max(t => t.CompanyId));
                else
                    mid = Convert.ToInt32(manufactureinforepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.CompanyId));
                if (mid == null)
                    n = 1;
                else
                    n = Convert.ToInt32(mid) + 1;
                manufactureinforepo.Dispose();
                return n;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void SaveUnit(string unit)
        {
            try
            {
                UnitRepository unitrepo = new UnitRepository();
                Unit supp = new Unit();
                if (CommonMethod.commProduct == true)
                    supp = unitrepo.GetAll().Where(t => t.UnitName.ToLower() == unit.ToLower()).FirstOrDefault();
                else
                    supp = unitrepo.GetAll().Where(t => t.UnitName.ToLower() == unit.ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (supp != null)
                { }
                else
                {
                    if (unit != "")
                    {
                        Unit unitinfo = new Unit();
                        int n = maxUnitid();
                        unitinfo.UnitID = n;
                        unitinfo.UnitName = unit;
                        unitinfo.Description = null;
                        unitinfo.UnitMarathi = null;
                        unitinfo.GSTUnitName = null;
                        unitinfo.CompId = CommonMethod.CompId;
                        unitinfo.StockAlertValue = 0;
                        unitrepo.Add(unitinfo);
                        unitrepo.Save();
                    }
                }
                unitrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public int maxUnitid()
        {
            try
            {
                int n;
                UnitRepository unitrepo = new UnitRepository();
                int? mid = 0;

                if (CommonMethod.commProduct == true)
                    mid = Convert.ToInt32(unitrepo.GetAll().Max(t => t.UnitID));
                else
                    mid = Convert.ToInt32(unitrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).Max(t => t.UnitID));

                if (mid == null)
                    n = 1;
                else
                    n = Convert.ToInt32(mid) + 1;
                unitrepo.Dispose();
                return n;
            }
            catch (Exception)
            { return 0; }
        }

        private void chkWarranty_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWarranty.Checked == true)
            {
                txtWarranty.Enabled = true;
                chkWithBatch.Enabled = false;
            }
            else
            {
                txtWarranty.Enabled = false;
                chkWithBatch.Enabled = true;
                txtWarranty.Text = "";
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //--------------Attempt3........
                Microsoft.Office.Interop.Excel.Application excel;
                Microsoft.Office.Interop.Excel.Workbook excelworkBook;
                Microsoft.Office.Interop.Excel.Worksheet excelSheet;
                object misValue = System.Reflection.Missing.Value;
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;
                excel.DisplayAlerts = false;
                excelworkBook = excel.Workbooks.Add(Type.Missing);
                excelSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelworkBook.ActiveSheet;
                excelSheet.Application.ActiveWindow.ScrollRow = 1;
                excelSheet.Application.ActiveWindow.SplitRow = 1;
                excelSheet.Application.ActiveWindow.FreezePanes = true;

                string data = null;
                DataSet ds = new DataSet();

                SqlDataAdapter adp = null;
                if (CommonMethod.commProduct == true)
                    adp = new SqlDataAdapter("Select GroupName = (SELECT GroupMasterName FROM GroupMaster as g  WHERE g.GroupMasterId=p.GroupMasterId and g.compid=p.compid),Prefix,ProductType,ProductName,MarathiName,ManufactureCompany,Uniti,UnitQty,PurchaseRate,DealerRate,Rate,AlternateUnit,AlternateUnitQty,AlternateUnitPurchaseRate,AlternateDealerRate,AlternateUnitRate,SGstPercent,CGstPercent,IGstPercent,ProductDiscount,Pcgstpercent,Psgstpercent,Pigstpercent,PurchaseDiscount,Cess,TaxInclusive,HSNCode,BarcodeStatus,WithBatch,WithBatchAndExpiry,Category,StockAlert,RackNO,decription from ProductInformation as p", db.Connection);
                else
                    adp = new SqlDataAdapter("Select GroupName = (SELECT GroupMasterName FROM GroupMaster as g  WHERE g.GroupMasterId=p.GroupMasterId and g.compid=p.compid),Prefix,ProductType,ProductName,MarathiName,ManufactureCompany,Uniti,UnitQty,PurchaseRate,DealerRate,Rate,AlternateUnit,AlternateUnitQty,AlternateUnitPurchaseRate,AlternateDealerRate,AlternateUnitRate,SGstPercent,CGstPercent,IGstPercent,ProductDiscount,Pcgstpercent,Psgstpercent,Pigstpercent,PurchaseDiscount,Cess,TaxInclusive,HSNCode,BarcodeStatus,WithBatch,WithBatchAndExpiry,Category,StockAlert,RackNO,decription from ProductInformation as p where p.compid=" + CommonMethod.CompId, db.Connection);
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
                excelSheet.Cells[1, 1] = "Group Name";//"GROUP_NAME";
                excelSheet.Cells[1, 2] = "Prefix";
                excelSheet.Cells[1, 3] = "Product Type";
                excelSheet.Cells[1, 4] = "Product Name";//"NAME";
                excelSheet.Cells[1, 5] = "Marathi Name";//"MARATHI";
                excelSheet.Cells[1, 6] = "MFG. Company";//"MFG";
                excelSheet.Cells[1, 7] = "Simple Unit";//"UNIT1";
                excelSheet.Cells[1, 8] = "Simple Unit Value";//"U_VALUE1";
                excelSheet.Cells[1, 9] = "Purchase Rate For Simple Unit";//"P_RATE1";
                excelSheet.Cells[1, 10] = "Wholesale Rate For Simple Unit";// "WSRATE1";
                excelSheet.Cells[1, 11] = "Sale Rate For Simple Unit";// "SRATE1";
                excelSheet.Cells[1, 12] = "Alternate Unit";//"UNIT2";
                excelSheet.Cells[1, 13] = "Alternate Unit Value";// "U_VALUE2";
                excelSheet.Cells[1, 14] = "Purchase Rate For Alternate Unit";// "P_RATE2";
                excelSheet.Cells[1, 15] = "Wholesale Rate For Alternate Unit";// "WSRATE2";
                excelSheet.Cells[1, 16] = "Sale Rate For Alternate Unit";// "SRATE2";
                excelSheet.Cells[1, 17] = "Sale SGST %";
                excelSheet.Cells[1, 18] = "Sale CGST %";
                excelSheet.Cells[1, 19] = "Sale IGST %";
                excelSheet.Cells[1, 20] = "Sale Disc. %";
                excelSheet.Cells[1, 21] = "Purchase SGST %";
                excelSheet.Cells[1, 22] = "Purchase CGST %";
                excelSheet.Cells[1, 23] = "Purchase IGST %";
                excelSheet.Cells[1, 24] = "Purchase Disc. %";
                excelSheet.Cells[1, 25] = "Cess";
                excelSheet.Cells[1, 26] = "Tax Inclusive";
                excelSheet.Cells[1, 27] = "HSN Code";
                excelSheet.Cells[1, 28] = "Barcode Status";
                excelSheet.Cells[1, 29] = "With Batch";
                excelSheet.Cells[1, 30] = "With Batch And Expiry";
                excelSheet.Cells[1, 31] = "Category";
                excelSheet.Cells[1, 32] = "Stock Alert";
                excelSheet.Cells[1, 33] = "Rack No";
                excelSheet.Cells[1, 34] = "Description";
                excelSheet.Columns.AutoFit();
                excelworkBook.SaveAs("AddProductInfo.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                excelworkBook.Close(true, misValue, misValue);
                excel.Quit();
                releaseObject(excelSheet);
                releaseObject(excelworkBook);
                releaseObject(excel);
                MessageBox.Show("Excel file created , you can find the file C:\\Users\\Documents\\AddProductInfo.xls");
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

        private void cmbsgst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbsgst.SelectedIndex > 0)
                {
                    cmbigst.Enabled = false;
                    cmbcgst.SelectedValue = cmbsgst.SelectedValue;
                }
                else
                {
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
                    cmbsgst.SelectedValue = cmbcgst.SelectedValue;
                }
                else
                {
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
                }
                else
                {
                    cmbsgst.Enabled = true;
                    cmbcgst.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbgroupMaster_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtProductName;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmGroupMaster group = new frmGroupMaster();
                        group.ShowDialog();
                        fillgroupmaster();
                        this.ActiveControl = cmbgroupMaster;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void button2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtProductName;
            }
            catch (Exception)
            { }
        }

        private void txtProductName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbProductType;
            }
            catch (Exception)
            { }
        }

        private void chkGroupDefault_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtPrefix;
            }
            catch (Exception)
            { }
        }

        private void txtPrefix_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtMarathiName;
            }
            catch (Exception)
            { }
        }

        private void txtMarathiName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F1)
                {
                    frmSettings settings = new frmSettings();
                    settings.ShowDialog();
                    frmProductInformation_Load(null, null);
                }

                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcompany;
            }
            catch (Exception)
            { }
        }

        private void chkAlternateUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (chkAlternateUnit.Checked == true)
                    {
                        SettingRepository SetRepo = new SettingRepository();
                        Setting SetData = new Setting();
                        SetData = SetRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (SetData.ThreeUnit == "True")// Checking for Three Unit Setting
                        {
                            rdbtnDouble.Visible = true;
                            rdbtnTriple.Visible = true;
                            this.ActiveControl = rdbtnDouble;
                        }
                        else
                            this.ActiveControl = txtAlternateUnitQty;
                    }
                    else
                        this.ActiveControl = txtHSN;
                }
            }
            catch (Exception)
            { }
        }

        private void chkMfgDefault_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbCategory;
            }
            catch (Exception)
            { }
        }

        private void txtAlternateUnitQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbAlternateUnit;
            }
            catch (Exception)
            { }
        }

        private void cmbAlternateUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtUnitQty;
            }
            catch (Exception)
            { }
        }

        private void txtUnitQty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtAlternateUnitRate;
            }
            catch (Exception)
            { }
        }

        private void txtAlternateUnitRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtAltrnteWholeSaleRate;
            }
            catch (Exception)
            { }
        }

        private void txtAltrnteWholeSaleRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtAltrPurchaseRate;
            }
            catch (Exception)
            { }
        }

        private void txtAltrPurchaseRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtHSN;
            }
            catch (Exception)
            { }
        }

        private void cmbCategory_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtHSN;
            }
            catch (Exception)
            { }
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtRackNo;
            }
            catch (Exception)
            { }
        }

        private void chkInclusive_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = chkWithoutsize;
            }
            catch (Exception)
            { }
        }

        private void txtpurpriceopercent_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtPurchaseRate;
            }
            catch (Exception)
            { }
        }

        private void txtPurchaseRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtretailratepersent;
            }
            catch (Exception)
            { }
        }

        private void txtretailratepersent_KeyDown(object sender, KeyEventArgs e)
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
                    this.ActiveControl = txtdealerratepersent;
            }
            catch (Exception)
            { }
        }

        private void txtDealerRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtcess;
            }
            catch (Exception)
            { }
        }

        private void txtStockAlert_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtPurDisc;
            }
            catch (Exception)
            { }
        }

        private void txtRackNo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnAddGroup;
            }
            catch (Exception)
            { }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = chkWarranty;
            }
            catch (Exception)
            { }
        }

        private void chkWarranty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (chkWarranty.Checked == true)
                        this.ActiveControl = txtWarranty;
                    else
                        this.ActiveControl = chkSubtitle;
                }
            }
            catch (Exception)
            { }
        }

        private void txtWarranty_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = chkSubtitle;
            }
            catch (Exception)
            { }
        }

        private void ttdec_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbBarcodeStatus;
            }
            catch (Exception)
            { }
        }

        private void chkSubtitle_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = chkWithBatch;
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
                    this.ActiveControl = btnlist;
            }
            catch (Exception)
            { }
        }

        private void btnlist_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnImportExcel;
            }
            catch (Exception)
            { }
        }

        private void btnImportExcel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnExportExcel;
            }
            catch (Exception)
            { }
        }

        private void btnExportExcel_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbgroupMaster;
            }
            catch (Exception)
            { }
        }

        private void txtHSN_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = chkMfgUnit;
            }
            catch (Exception)
            { }
        }

        private void txtdealerratepersent_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtDealerRate;
            }
            catch (Exception)
            { }
        }

        private void cmbsgst_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbPIgst;
            }
            catch (Exception)
            { }
        }

        private void cmbigst_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSave;
            }
            catch (Exception)
            { }
        }

        private void txtcess_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbPSgst;
            }
            catch (Exception)
            { }
        }

        private void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    cmbsubcatname.Enabled = false;
                    cmbDept.Enabled = false;
                    cmbcolor.Enabled = false;
                    chkdept.Enabled = false;
                    chksubcat.Enabled = false;
                    chkcolor.Enabled = false;
                    chkWithBatch.Enabled = false;
                    chkWithoutsize.Enabled = false;
                    chkBatchExpiry.Enabled = false;
                    cmbCategory.Enabled = false;
                    btnaddColor.Enabled = false;
                    btnaddDept.Enabled = false;
                    btnaddSubCat.Enabled = false;
                    chkdept.Enabled = false;
                    chkcolor.Enabled = false;
                    chksubcat.Enabled = false;
                    pnlAssemblyDetails.Visible = false;
                    if (Convert.ToInt32(cmbProductType.SelectedIndex) == 3) //Matrix
                    {
                        cmbsubcatname.Enabled = true;
                        cmbDept.Enabled = true;
                        cmbcolor.Enabled = true;
                        chkdept.Enabled = true;
                        chksubcat.Enabled = true;
                        chkcolor.Enabled = true;
                        chkWithoutsize.Enabled = true;
                        cmbCategory.Enabled = true;
                        btnaddColor.Enabled = true;
                        btnaddDept.Enabled = true;
                        btnaddSubCat.Enabled = true;
                        chkdept.Enabled = true;
                        chkcolor.Enabled = true;
                        chksubcat.Enabled = true;
                        pnlAssemblyDetails.Visible = false;
                    }
                    else if (Convert.ToInt32(cmbProductType.SelectedIndex) == 0)//standard
                    {
                        chkWithBatch.Enabled = true;
                        chkWithoutsize.Enabled = true;
                        chkBatchExpiry.Enabled = true;
                        pnlAssemblyDetails.Visible = false;
                    }
                    else if (Convert.ToInt32(cmbProductType.SelectedIndex) == 2)//Assembly Product
                    {
                        cmbsubcatname.Enabled = false;
                        cmbDept.Enabled = false;
                        cmbcolor.Enabled = false;
                        chkdept.Enabled = false;
                        chksubcat.Enabled = false;
                        chkcolor.Enabled = false;
                        chkWithoutsize.Enabled = false;
                        cmbCategory.Enabled = false;
                        btnaddColor.Enabled = false;
                        btnaddDept.Enabled = false;
                        btnaddSubCat.Enabled = false;
                        chkdept.Enabled = false;
                        chkcolor.Enabled = false;
                        chksubcat.Enabled = false;
                        chkWithBatch.Enabled = false;
                        chkWithoutsize.Enabled = false;
                        chkBatchExpiry.Enabled = false;
                        cmbRepackStatus.Enabled = false;
                        cmbbulkitem.Enabled = false;
                        txtRepackConversion.Enabled = false;
                        chkRateSlab.Enabled = false;
                        pnlAssemblyDetails.Visible = true;
                        this.ActiveControl = txtChildProductAssembly;
                    }
                }
                else
                {
                    try
                    {
                        db.connect();
                        SqlCommand command = new SqlCommand("SPCheckforIdUsedorNot", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "Product";
                        command.Parameters.Add("@Id", SqlDbType.VarChar).Value = updateID;
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
                    {
                        MessageBox.Show("This is not update to product type");
                        ProductRepository proRepo = new ProductRepository();
                        var pData = proRepo.GetAll().Where(t => t.ID == updateID && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (pData != null)
                            cmbProductType.SelectedIndex = Convert.ToInt32(pData.ProductType);
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbBarcodeStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (cmbBarcodeStatus.Text == "Static Barcode")
                this.ActiveControl = rdbtnYes;
            else
            {
                if (e.KeyCode == Keys.Enter)
                    this.ActiveControl = btnSave;
            }
        }

        private void chkDefProductType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDefProductType.Checked == true)
                defaultproducttype = cmbProductType.SelectedIndex;
            else
                defaultproducttype = 0;
        }

        private void chkdept_CheckedChanged(object sender, EventArgs e)
        {
            if (chkdept.Checked == true)
                defaultDepartment = cmbDept.SelectedIndex;
            else
                defaultDepartment = 0;
        }

        private void chksubcat_CheckedChanged(object sender, EventArgs e)
        {
            if (chksubcat.Checked == true)
                defaultSubcategory = cmbsubcatname.SelectedIndex;
            else
                defaultSubcategory = 0;
        }

        private void chkcolor_CheckedChanged(object sender, EventArgs e)
        {
            if (chkcolor.Checked == true)
                defaultColor = cmbcolor.SelectedIndex;
            else
                defaultColor = 0;
        }

        private void cmbPSgst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPSgst.SelectedIndex > 0)
                {
                    cmbPIgst.Enabled = false;
                    cmbPCgst.SelectedValue = cmbPSgst.SelectedValue;
                }
                else
                {
                    cmbPCgst.SelectedValue = cmbPSgst.SelectedValue;
                    cmbPIgst.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbPCgst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPCgst.SelectedIndex > 0)
                {
                    cmbPIgst.Enabled = false;
                    cmbPSgst.SelectedValue = cmbPCgst.SelectedValue;
                }
                else
                {
                    cmbPSgst.SelectedValue = cmbPCgst.SelectedValue;
                    cmbPIgst.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbPIgst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPIgst.SelectedIndex > 0)
                {
                    cmbPSgst.SelectedIndex = 0;
                    cmbPCgst.SelectedIndex = 0;
                    cmbPSgst.Enabled = false;
                    cmbPCgst.Enabled = false;
                }
                else
                {
                    cmbPSgst.Enabled = true;
                    cmbPCgst.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void chkMfgUnit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkMfgUnit.Checked == true)
                {
                    lblFormulae.Visible = true;
                    txtFormulae.Visible = true;
                    pnlFormula.Visible = true;
                    this.ActiveControl = txtFormulae;
                }
                else
                {
                    lblFormulae.Visible = false;
                    txtFormulae.Visible = false;
                    txtFormulae.Text = string.Empty;
                    fillslabdesc();
                    chkqtyslab.Checked = false;
                    pnlFormula.Visible = false;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbRepackStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbRepackStatus.SelectedIndex == 0)
                {
                    cmbbulkitem.Enabled = true;
                    fillBlukItem();
                }
                else
                {
                    cmbbulkitem.Enabled = false;
                    cmbbulkitem.Text = string.Empty;
                }
            }
            catch (Exception)
            { }
        }

        public void fillBlukItem()
        {
            try
            {
                DataTable dtbulkitem = null;
                if (CommonMethod.commProduct == true)
                    dtbulkitem = db.Gettable("SELECT id,(ProductName +','+ ManufactureCompany+','+Uniti) AS ProductName FROM ProductInformation where RepackStatus='Bulk'");
                else
                    dtbulkitem = db.Gettable("SELECT id,(ProductName +','+ ManufactureCompany+','+Uniti) AS ProductName FROM ProductInformation where RepackStatus='Bulk' and compid=" + CommonMethod.CompId);
                if (dtbulkitem.Rows.Count > 0)
                {
                    cmbbulkitem.ValueMember = "Id";
                    cmbbulkitem.DisplayMember = "ProductName";
                    cmbbulkitem.DataSource = dtbulkitem;
                }
            }
            catch (Exception) { }
        }

        private void txtRepackConversion_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtRepackConversion.Text == "")
                {
                    txtRepackConversion.Text = "0";
                    this.ActiveControl = txtRepackConversion;
                }
            }
            catch (Exception)
            { }
        }

        private void chkBatchExpiry_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBatchExpiry.Checked == true)
            {
                chkWarranty.Enabled = false;
                chkWithBatch.Checked = false;
            }
            else
                chkWarranty.Enabled = true;
        }

        private void chkqtyslab_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkqtyslab.Checked == true)
                {
                    cmbslabdescription.Enabled = true;
                    txtvaluecount.Enabled = true;
                }
                else
                {
                    cmbslabdescription.Enabled = false;
                    txtvaluecount.Enabled = false;
                }
            }
            catch (Exception) { }
        }

        public void fillslabdesc()
        {
            try
            {
                SlabMasterRepository slabrepo = new SlabMasterRepository();
                List<SlabMaster> listdata = new List<SlabMaster>();
                listdata.Clear();
                listdata = slabrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.Description).ToList();
                listdata.Add(new SlabMaster { Description = "Select", Id = 0 });
                listdata.Reverse();
                cmbslabdescription.DataSource = listdata;
                cmbslabdescription.ValueMember = "Id";
                cmbslabdescription.DisplayMember = "Description";
                slabrepo.Dispose();
            }
            catch (Exception) { }
        }

        private void chkDefGST_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkDefGST.Checked == true)
                { }
                else if (chkDefGST.Checked == false)
                    fillgst();
            }
            catch (Exception) { }
        }

        private void txtvaluecount_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtvaluecount.Text != "")
                slabApplied = true;
            if (txtvaluecount.Text != "" && cmbslabdescription.Text != "Select" && cmbslabdescription.Text.Trim() != "" && e.KeyCode == Keys.Enter)
            {
                pnlFormula.Visible = false;
                this.ActiveControl = btnSave;
            }
        }

        private void cmbslabdescription_Leave(object sender, EventArgs e)
        {
            if (cmbslabdescription.Text != "")
                slabApplied = true;
        }

        private void txtFormulae_Leave(object sender, EventArgs e)
        {
            if (txtFormulae.Text != "")
                this.ActiveControl = cmbslabdescription;
        }

        private void chkWithBatch_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWithBatch.Checked == true)
                chkBatchExpiry.Checked = false;
        }

        private void rdbtnDouble_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbtnDouble.Checked == true)
                {
                    txtTripleUnitQty.Text = "0";
                    txtSecondUnitQty.Text = "0";
                    cmbTripleUnit.SelectedIndex = 0;
                    lblSecondUnit.Text = "UNIT";
                    txtTripleUnitQty.Visible = false;
                    cmbTripleUnit.Visible = false;
                    txtSecondUnitQty.Visible = false;
                    lblSecondUnit.Visible = false;
                    txtAlternateUnitQty.Text = "1";
                    cmbAlternateUnit.Enabled = true;
                    txtUnitQty.Enabled = true;
                    txtAlternateUnitRate.Enabled = true;
                    txtAltrnteWholeSaleRate.Enabled = true;
                    txtAltrPurchaseRate.Enabled = true;
                    lblUnit.Text = cmbunit.Text;
                    if (btnSave.Text == "Update" && strcheck == "F" && cmbAlternateUnit.SelectedIndex == 0)
                        cmbAlternateUnit.Text = OldUnit;
                    if (cmbAlternateUnit.Text != "Select" || cmbAlternateUnit.Text != "")
                        lblSecondUnit.Text = cmbAlternateUnit.Text;
                    this.ActiveControl = cmbAlternateUnit;
                }
            }
            catch (Exception)
            { }
        }

        private void rdbtnTriple_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdbtnTriple.Checked == true)
                {
                    txtAlternateUnitQty.Text = "1";
                    cmbAlternateUnit.Enabled = true;
                    txtUnitQty.Enabled = true;
                    txtAlternateUnitRate.Enabled = true;
                    txtAltrnteWholeSaleRate.Enabled = true;
                    txtAltrPurchaseRate.Enabled = true;
                    lblUnit.Text = cmbunit.Text;
                    if (btnSave.Text == "Update" && strcheck == "F" && cmbAlternateUnit.SelectedIndex == 0)
                        cmbAlternateUnit.Text = OldUnit;
                    txtTripleUnitQty.Text = "1";
                    txtSecondUnitQty.Text = "0";
                    cmbTripleUnit.Enabled = true;
                    txtSecondUnitQty.Enabled = true;
                    txtTripleUnitQty.Visible = true;
                    cmbTripleUnit.Visible = true;
                    txtSecondUnitQty.Visible = true;
                    lblSecondUnit.Visible = true;
                    lblSecondUnit.Text = cmbAlternateUnit.Text;
                    this.ActiveControl = cmbAlternateUnit;
                }
            }
            catch (Exception)
            { }
        }

        private void chkRateSlab_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRateSlab.Checked == true)
                cmbRateSlab.Enabled = true;
            else
                cmbRateSlab.Enabled = false;
        }

        private void fillRateSlab()
        {
            try
            {
                cmbRateSlab.DataSource = null;
                RateSlabRepository slabrepo = new RateSlabRepository();
                List<RateSlab> listdata = new List<RateSlab>();
                listdata.Clear();
                listdata = slabrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.SlabDescription).ToList();
                cmbRateSlab.DataSource = listdata;
                cmbRateSlab.ValueMember = "ID";
                cmbRateSlab.DisplayMember = "SlabDescription";
                slabrepo.Dispose();
            }
            catch (Exception) { }
        }

        private void cmbslabdescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    this.ActiveControl = txtvaluecount;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmSlabMaster slab = new frmSlabMaster();
                        slab.ShowDialog();
                        fillslabdesc();
                        this.ActiveControl = cmbslabdescription;
                    }
                }
            }
            catch (Exception) { }
        }

        private void cmbRateSlab_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmRateSlab rateslab = new frmRateSlab();
                        rateslab.ShowDialog();
                        fillRateSlab();
                        this.ActiveControl = cmbRateSlab;
                    }
                }
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSave;
            }
            catch (Exception) { }
        }

        private void cmbProductType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtPrefix;
            }
            catch (Exception) { }
        }

        private void txtAlternateUnitRate_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtAltrnteWholeSaleRate;
            }
            catch (Exception) { }
        }

        private void txtAltrnteWholeSaleRate_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtPrefix;
            }
            catch (Exception) { }
        }

        private void chkMfgUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (chkMfgUnit.Checked == true)
                        this.ActiveControl = txtFormulae;
                    else
                        this.ActiveControl = txtPurchaseRate;
                }
            }
            catch (Exception) { }
        }

        private void cmbPSgst_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbsgst;
            }
            catch (Exception)
            { }
        }

        private void cmbPIgst_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbigst;
            }
            catch (Exception)
            { }
        }

        private void txtPurDisc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtDiscount;
            }
            catch (Exception)
            { }
        }

        private void chkWithBatch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = chkBatchExpiry;
            }
            catch (Exception)
            { }
        }

        private void chkBatchExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = chkTaxSlab;
            }
            catch (Exception)
            { }
        }

        private void chkTaxSlab_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = chkRateSlab;
            }
            catch (Exception)
            { }
        }

        private void chkRateSlab_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (chkRateSlab.Checked == true)
                        this.ActiveControl = cmbRateSlab;
                    else
                        this.ActiveControl = btnSave;
                }
            }
            catch (Exception) { }
        }

        private void cmbRepackStatus_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (cmbRepackStatus.Text == "Repackage")
                        this.ActiveControl = cmbbulkitem;
                    else
                        this.ActiveControl = txtRepackConversion;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbbulkitem_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtRepackConversion;
            }
            catch (Exception) { }
        }

        private void txtRepackConversion_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSave;
            }
            catch (Exception) { }
        }

        private void chkWithoutsize_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = chkWarranty;
            }
            catch (Exception) { }
        }

        private void chkqtyslab_Leave(object sender, EventArgs e)
        {
            try
            {
                if (chkqtyslab.Checked == false)
                {
                    pnlFormula.Visible = false;
                    this.ActiveControl = txtPurchaseRate;
                }
            }
            catch (Exception) { }
        }

        private void cmbBarcodeStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbBarcodeStatus.Text == "Static Barcode")
                {
                    pnldefStaticbarcode.Visible = true;
                    rdbtnYes.Checked = true;
                }
                else
                    pnldefStaticbarcode.Visible = false;
            }
            catch (Exception) { }
        }

        private void rdbtnYes_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (rdbtnYes.Checked == true)
                    this.ActiveControl = btnSave;
                else if (rdbtnNo.Checked == true)
                    this.ActiveControl = btnSave;
            }
            catch (Exception) { }
        }

        private void cmbProductType_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbProductType.Text.Trim() == "")
                {
                    MessageBox.Show("Select Product Type.", "Warning");
                    this.ActiveControl = cmbProductType;

                    cmbProductType.Items.Clear();
                    cmbProductType.Items.Add("Standard");
                    cmbProductType.Items.Add("Serialized");
                    cmbProductType.Items.Add("Assembly");
                    cmbProductType.Items.Add("Matrix");
                    cmbProductType.Items.Add("Kit");
                    cmbProductType.Items.Add("Scrap");
                    cmbProductType.Items.Add("Work Type");
                    cmbProductType.SelectedIndex = cmbProductType.FindStringExact("Standard");
                }
            }
            catch (Exception)
            { }
        }

        private void txtChildProductAssembly_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtChildProductAssembly.Text != "")
                {
                    if ((txtChildProductAssembly.Text).Contains(","))
                    {
                        string Glbproname = txtChildProductAssembly.Text.Trim().Split(',')[0];
                        string Glbcompany = txtChildProductAssembly.Text.Trim().Split(',')[1];
                        string Glbunit = txtChildProductAssembly.Text.Trim().Split(',')[2];
                        ProductRepository productrepo = new ProductRepository();
                        var ProductDataGlobal = productrepo.GetAll().Where(t => t.ProductName == Glbproname && t.ManufactureCompany == Glbcompany && t.Uniti == Glbunit).FirstOrDefault();
                        if (ProductDataGlobal != null)
                        {
                            cmbChildUnit.Items.Clear();
                            cmbChildUnit.DisplayMember = "Text";
                            cmbChildUnit.ValueMember = "Value";
                            cmbChildUnit.Items.Add(new { Text = ProductDataGlobal.Uniti, Value = ProductDataGlobal.Uniti });
                            if (ProductDataGlobal.AlternateUnit != null && ProductDataGlobal.AlternateUnit != "")
                                cmbChildUnit.Items.Add(new { Text = ProductDataGlobal.AlternateUnit, Value = ProductDataGlobal.AlternateUnit });
                            if (ProductDataGlobal.TripleUnit != null && ProductDataGlobal.TripleUnit != "")
                                cmbChildUnit.Items.Add(new { Text = ProductDataGlobal.TripleUnit, Value = ProductDataGlobal.TripleUnit });
                            cmbChildUnit.SelectedIndex = 0;
                        }
                        else
                        {
                            MessageBox.Show("This Product is not Valid","Warning");
                            txtChildProductAssembly.Text = "";
                            this.ActiveControl = txtChildProductAssembly;
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtChildProductAssembly.Text != "" && txtChildQty.Text != "" && (txtChildProductAssembly.Text).Contains(","))
                {
                    for (int x = 0; x < gvAssemblyProductList.Rows.Count; x++)
                    {
                        string Proname = txtChildProductAssembly.Text.Trim().Split(',')[0];
                        string company = txtChildProductAssembly.Text.Trim().Split(',')[1];
                        string unit = txtChildProductAssembly.Text.Trim().Split(',')[2];
                        if (Proname == Convert.ToString(gvAssemblyProductList.Rows[x].Cells[0].Value) && company == Convert.ToString(gvAssemblyProductList.Rows[x].Cells[1].Value) && unit == Convert.ToString(gvAssemblyProductList.Rows[x].Cells[2].Value))
                        {
                            MessageBox.Show("This Product is Already add in GridView", "Warning");
                            txtChildProductAssembly.Text = "";
                            cmbChildUnit.Text = "";
                            txtChildQty.Text = "";
                            this.ActiveControl = txtChildProductAssembly;
                            goto a;
                        }
                    }
                    if (colIndex != -1)
                    {
                        string Proname = txtChildProductAssembly.Text.Trim().Split(',')[0];
                        string company = txtChildProductAssembly.Text.Trim().Split(',')[1];
                        string unit = txtChildProductAssembly.Text.Trim().Split(',')[2];
                        gvAssemblyProductList.Rows[colIndex].Cells[0].Value = Proname;
                        gvAssemblyProductList.Rows[colIndex].Cells[1].Value = company;
                        gvAssemblyProductList.Rows[colIndex].Cells[2].Value = unit;
                        gvAssemblyProductList.Rows[colIndex].Cells[3].Value = txtChildQty.Text;
                        gvAssemblyProductList.Rows[colIndex].Cells[4].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                        txtChildQty.Text = "";
                        txtChildProductAssembly.Text = "";
                        cmbChildUnit.Text = "";
                        this.ActiveControl = txtChildProductAssembly;
                        colIndex = -1;
                    }
                    else
                    {
                        int i = gvAssemblyProductList.Rows.Count;
                        gvAssemblyProductList.Rows.Add();
                        string Proname = txtChildProductAssembly.Text.Trim().Split(',')[0];
                        string company = txtChildProductAssembly.Text.Trim().Split(',')[1];
                        string unit = txtChildProductAssembly.Text.Trim().Split(',')[2];
                        gvAssemblyProductList.Rows[i].Cells[0].Value = Proname;
                        gvAssemblyProductList.Rows[i].Cells[1].Value = company;
                        gvAssemblyProductList.Rows[i].Cells[2].Value = unit;
                        gvAssemblyProductList.Rows[i].Cells[3].Value = txtChildQty.Text;
                        gvAssemblyProductList.Rows[i].Cells[4].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                        txtChildQty.Text = "";
                        txtChildProductAssembly.Text = "";
                        cmbChildUnit.Text = "";
                        this.ActiveControl = txtChildProductAssembly;
                        colIndex = -1;
                    }
                a: { }
                }
                else
                {
                    if (txtChildProductAssembly.Text == "")
                    {
                        MessageBox.Show("Enter Product Name.", "Warning");
                        this.ActiveControl = txtChildProductAssembly;
                    }
                    else
                    {
                        MessageBox.Show("Enter Quantity.", "Warning");
                        this.ActiveControl = txtChildQty;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void gvAssemblyProductList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                {
                    if (btnSave.Text != "Update")
                        gvAssemblyProductList.Rows.Remove(gvAssemblyProductList.Rows[e.RowIndex]);
                    else
                    {
                        DataTable dt = db.GetTable("select d.ProductName from PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo where d.Productname='" + txtProductName.Text + "' and Unit='" + cmbunit.Text + "' and CompanyName='" + cmbcompany.Text + "' and m.CompId=" + CommonMethod.CompId + " and m.TransactionYear='" + CommonMethod.TransactionYear + "' Union  Select cd.ProductId from CustomerBillDetail as cd inner join CustomerBillMaster as cm on cm.Id=cd.BillNo where ProductId='" + txtProductName.Text + "' and CompanyName='" + cmbcompany.Text + "' and Unit='" + cmbunit.Text + "' and cm.CompId=" + CommonMethod.CompId + " and cm.TransactionYear='" + CommonMethod.TransactionYear + "' Union  Select ProductName from Stock where ProductName='" + txtProductName.Text + "' and MfgCompany='" + cmbcompany.Text + "' and Unit='" + cmbunit.Text + "' and CompId=" + CommonMethod.CompId + " and TransactionYear='" + CommonMethod.TransactionYear + "'");
                        if (dt.Rows.Count > 0)
                            MessageBox.Show("This Assembly Child Product is not delete because " + Environment.NewLine + " They can use in Purchase Bill and Customer Bill");
                        else
                        {
                            deletedId.Add(Convert.ToInt32(gvAssemblyProductList.Rows[e.RowIndex].Cells[5].Value));
                            gvAssemblyProductList.Rows.Remove(gvAssemblyProductList.Rows[e.RowIndex]);
                        }
                    }
                }
                else
                {
                    txtChildProductAssembly.Text = Convert.ToString(gvAssemblyProductList.Rows[e.RowIndex].Cells[0].Value) + ',' + Convert.ToString(gvAssemblyProductList.Rows[e.RowIndex].Cells[1].Value) + ',' + Convert.ToString(gvAssemblyProductList.Rows[e.RowIndex].Cells[2].Value);
                    cmbChildUnit.Text = Convert.ToString(gvAssemblyProductList.Rows[e.RowIndex].Cells[2].Value);
                    txtChildQty.Text = Convert.ToString(gvAssemblyProductList.Rows[e.RowIndex].Cells[3].Value);
                    colIndex = e.RowIndex;
                    this.ActiveControl = txtChildProductAssembly;
                }
            }
            catch (Exception)
            { }
        }

        private void btnCloseAssembly_Click(object sender, EventArgs e)
        {
            try
            {
                if (gvAssemblyProductList.Rows.Count > 0)
                {
                    pnlAssemblyDetails.Visible = false;
                    this.ActiveControl = txtPrefix;
                }
                else
                {
                    MessageBox.Show("Please Add Child Product");
                    this.ActiveControl = txtChildProductAssembly;
                    pnlAssemblyDetails.Visible = true;
                }
            }
            catch (Exception)
            { }
        }

        private void lnkAssemeblyProductDetails_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                if (cmbProductType.SelectedIndex == 2)
                    pnlAssemblyDetails.Visible = true;
            }
            catch (Exception)
            { }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            chkMfgDefault.Checked = false;
            chkGroupDefault.Checked = false;
            slabApplied = false;
            clear();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    try
                    {
                        db.connect();
                        SqlCommand command = new SqlCommand("SPCheckforIdUsedorNot", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "ProductD";
                        command.Parameters.Add("@Id", SqlDbType.VarChar).Value = updateID;
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
                        MessageBox.Show("This Product is Used. You Can Not Delete.", "Warning");
                    else
                    {
                        MessageBox.Show("Record Deleted Sucessfully.", "Success");
                        clear();
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.Filter = "jpeg|*.jpg|bmp|*.bmp|all files|*.*";
                DialogResult res = openFileDialog1.ShowDialog();
                if (res == DialogResult.OK)
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
            catch (Exception)
            { }
        }

        private void btnaddDept_Click(object sender, EventArgs e)
        {
            try
            {
                frmDeptartement dept = new frmDeptartement();
                dept.ShowDialog();
                FillDept();
                this.ActiveControl = cmbDept;
            }
            catch (Exception) { }
        }

        private void btnaddSubCat_Click(object sender, EventArgs e)
        {
            try
            {
                frmCategory cat = new frmCategory();
                cat.WindowState = System.Windows.Forms.FormWindowState.Normal;
                cat.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                cat.ShowDialog();
                fillsubcat();
                this.ActiveControl = cmbsubcatname;
            }
            catch (Exception) { }
        }

        private void btnaddColor_Click(object sender, EventArgs e)
        {
            try
            {
                frmcolor compinfo = new frmcolor();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
                fillColor();
                this.ActiveControl = cmbcolor;
            }
            catch (Exception)
            { }
        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            try
            {
                frmGroupMaster grpmast = new frmGroupMaster();
                grpmast.ShowDialog();
                fillgroupmaster();
            }
            catch (Exception)
            { }
        }

        private void btnAddMfgComp_Click(object sender, EventArgs e)
        {
            try
            {
                frmManufactureCompanyInfo compinfo = new frmManufactureCompanyInfo();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
                fillcombo();
                this.ActiveControl = cmbcompany;
            }
            catch (Exception)
            { }
        }

        private void btnAddMfgComp_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = chkAlternateUnit;
            }
            catch (Exception)
            { }
        }

        private void btnAddUnit_Click(object sender, EventArgs e)
        {
            try
            {
                frmUnitInformation unit = new frmUnitInformation();
                unit.ShowDialog();
                fillunit();
                fillAlternateUnit();
                fillTripleUnit();
                this.ActiveControl = cmbunit;
            }
            catch (Exception)
            { }
        }

        private void btnAddUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = chkAlternateUnit;
            }
            catch (Exception)
            { }
        }

        private void chkQuantitySubtitle_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkQuantitySubtitle.Checked == true)
                    txtQtySub.Enabled = true;
                else
                    txtQtySub.Enabled = false;
            }
            catch (Exception)
            { }
        }
    }
}