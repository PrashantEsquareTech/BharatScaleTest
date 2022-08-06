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
    public partial class frmDeadStock : Form
    {
        public static frmDeadStock deadstockinfo;
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        public int DeadStockNo, DeadId, productentrytype, colindex = -1, codeflag = 0;
        public decimal savedQuantity = 0;
        List<int> deleteid = new List<int>();
        public string ProductCategory, Glbproname = "", Glbcompany = "", Glbunit = "", barcodestatus, barcode, staticbarStatus = "";
        ProductInformation ProductDataGlobal=new ProductInformation();
        private static string psize = "", pcolor = "", deptname = "", category = "";

        public frmDeadStock()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxdeadid();
            fillgst();
            fillcombo();
            fillGodownName();
            deleteid.Clear();
            btnupdate.Enabled = false;
        }

        public frmDeadStock(int DeadNo)
        {
            DeadStockNo = DeadNo;
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxdeadid();
            fillgst();
            fillcombo();
            fillGodownName();
            deleteid.Clear();
            LoadData(DeadNo);
            this.ActiveControl = cmbcomanyname;
        }

        public void fillGodownName()
        {
            try
            {
                GodownMasterRepository GDRepo = new GodownMasterRepository();
                List<GodownMaster> GDData = new List<GodownMaster>();
                GDData.Clear();
                if (CommonMethod.commProduct == true)
                    GDData = GDRepo.GetAll();
                else
                    GDData = GDRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).ToList();
                if (GDData.Count > 0)
                {
                    GDData.Add(new GodownMaster { GodownName = "Shop", ID = 0 });
                    GDData.Reverse();
                    cmbGodownName.DataSource = GDData;
                    cmbGodownName.ValueMember = "ID";
                    cmbGodownName.DisplayMember = "GodownName";
                    cmbGodownName.Enabled = true;
                    GDRepo.Dispose();
                }
                else
                {
                    cmbGodownName.DataSource = null;
                    cmbGodownName.Items.Clear();
                    cmbGodownName.DisplayMember = "Text";
                    cmbGodownName.ValueMember = "Value";
                    cmbGodownName.Items.Add(new { Text = "Shop", Value = 0 });
                    cmbGodownName.SelectedIndex = 0;
                    cmbGodownName.Enabled = false;
                }
            }
            catch (Exception)
            { }
        }

        public void autocompletedata()
        {
            try
            {
                txtpname.Text = "";
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
                    txtpname.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        public void maxdeadid()
        {
            try
            {
                DeadMasterRepository deadmasterrepo = new DeadMasterRepository();
                var mid = deadmasterrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.DeadNo);
                if (mid == null)
                    txtdeadno.Text = "1";
                else
                    txtdeadno.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                deadmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            maxdeadid();
            fillGodownName();
            txtQuantity.Text = "";
            txtpname.Text = "";
            GvProductInfo.Rows.Clear();
            dtgvcode.Rows.Clear();
            dtgvcode.Visible = false;
            btnSave.Enabled = true; btnupdate.Enabled = false;
            deleteid.Clear();
            autocompletedata();
            fillgst();
            cmbgst.Enabled = true;
            cmbGodownName.Enabled = true;
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
            {
                btnSave.Enabled = false;
                btnupdate.Enabled = false;
                btnNew.Enabled = false;
            }
            try
            { dtpdeaddate.Value = DateTime.Now.Date; }
            catch (Exception)
            { }
        }

        public void fillbatchcombo(string proname, string company, string unit)
        {
            try
            {
                DataTable dt = db.Gettable("Select Distinct BatchNo from PurchaseDetail where ProductName='" + proname + "' and CompanyName='" + company + "' and Unit='" + unit + "' and Status='Active'");
                cmbBatchNo.DataSource = dt;
                cmbBatchNo.ValueMember = "BatchNo";
                cmbBatchNo.DisplayMember = "BatchNo";
            }
            catch (Exception)
            { }
        }

        public void LoadData(int DeadNo)
        {
            try
            {
                DeadMasterRepository deadmasterrepo = new DeadMasterRepository();
                DeadProductMaster deadstockdata = new DeadProductMaster();
                var smasterData = deadmasterrepo.GetAll().Where(t => t.Id == Convert.ToInt32(DeadNo) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                txtdeadno.Text = Convert.ToString(smasterData.DeadNo);
                dtpdeaddate.Value = Convert.ToDateTime(smasterData.DeadDate);
                cmbgst.Text = Convert.ToString(smasterData.Vat);
                if (smasterData.StockIn != null)
                {
                    cmbGodownName.SelectedValue = smasterData.StockIn;
                    cmbGodownName.Enabled = false;
                }
                DeadId = smasterData.Id;
                var SDetailData = smasterData.DeadProductDetails;
                GvProductInfo.Rows.Clear();
                int i = 0;
                foreach (var item in SDetailData)
                {
                    GvProductInfo.Rows.Add();
                    GvProductInfo.Rows[i].Cells["Id"].Value = item.Id;
                    GvProductInfo.Rows[i].Cells["ProductNameg"].Value = item.ProductName;
                    GvProductInfo.Rows[i].Cells["Company"].Value = item.CompanyName;
                    GvProductInfo.Rows[i].Cells["Unit"].Value = item.Unit;
                    if (item.SaleUnit != null)
                        GvProductInfo.Rows[i].Cells["ApplyUnit"].Value = item.SaleUnit;
                    else
                        GvProductInfo.Rows[i].Cells["ApplyUnit"].Value = item.Unit;
                    if (item.Unit != item.SaleUnit && item.SaleUnit != null)
                    {
                        ProductRepository productrepo = new ProductRepository();
                        var productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        GvProductInfo.Rows[i].Cells["Quantity"].Value = Convert.ToDecimal(item.Quantity) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                    }
                    else
                        GvProductInfo.Rows[i].Cells["Quantity"].Value = Convert.ToDecimal(item.Quantity);
                    GvProductInfo.Rows[i].Cells["Code"].Value = item.Code;
                    if (!string.IsNullOrEmpty(item.BatchNo))
                        GvProductInfo.Rows[i].Cells["BatchNo"].Value = item.BatchNo;
                    else
                        GvProductInfo.Rows[i].Cells["BatchNo"].Value = "";
                    GvProductInfo.Rows[i].Cells["Barcodeg"].Value = item.Barcode;
                    GvProductInfo.Rows[i].Cells["Sizeg"].Value = item.Size;
                    GvProductInfo.Rows[i].Cells["ColorNameg"].Value = item.ColorName;
                    GvProductInfo.Rows[i].Cells["Categoryg"].Value = item.Category;
                    GvProductInfo.Rows[i].Cells["DeptNameg"].Value = item.DeptName;
                    GvProductInfo.Rows[i].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                    i++;
                }
                btnSave.Enabled = false;
                btnupdate.Enabled = true;
                cmbgst.Enabled = false;
                cmbGodownName.Enabled = false;
                deadmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void frmDeadStock_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                autocompletedata();
            }
            catch (Exception)
            { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtpname.Text.Trim() != "" && txtQuantity.Text != "" || (cmbBatchNo.Text != "" && (productentrytype == 1 || productentrytype == 2)))
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
                    if (colindex == -1)
                    {
                        string Proname = txtpname.Text.Trim().Split(',')[0];
                        string company = txtpname.Text.Trim().Split(',')[1];
                        string unit = txtpname.Text.Trim().Split(',')[2];
                        string batchno = "";
                        if (productentrytype == 1 || productentrytype == 2)
                            batchno = cmbBatchNo.Text;
                        if (ProductCategory == "Motor")
                        {
                            for (int l = 0; l < dtgvcode.RowCount; l++)
                            {
                                for (int j = 0; j < GvProductInfo.RowCount; j++)
                                {
                                    if (GvProductInfo.Rows[j].Cells["Code"].Value.ToString() == dtgvcode.Rows[l].Cells[0].Value.ToString() && GvProductInfo.Rows[j].Cells["ProductNameg"].Value.ToString() == Proname && GvProductInfo.Rows[j].Cells["Company"].Value.ToString() == company && GvProductInfo.Rows[j].Cells["Unit"].Value.ToString() == unit)
                                    {
                                        MessageBox.Show("You already entered this product.", "Warning");
                                        goto t;
                                    }
                                }
                                GvProductInfo.Rows.Add();
                                int i = GvProductInfo.RowCount;
                                GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                                GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                                GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                                GvProductInfo.Rows[i - 1].Cells["ApplyUnit"].Value = cmbUnit.Text;
                                GvProductInfo.Rows[i - 1].Cells["Quantity"].Value = 1;
                                GvProductInfo.Rows[i - 1].Cells["Code"].Value = dtgvcode.Rows[l].Cells[0].Value.ToString();
                                GvProductInfo.Rows[i - 1].Cells["BatchNo"].Value = "";//cmbBatchNo.Text;
                                GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                            }
                        }
                        else
                        {
                            for (int j = 0; j < GvProductInfo.RowCount; j++)
                            {
                                if (GvProductInfo.Rows[j].Cells["ProductNameg"].Value.ToString() == Proname && GvProductInfo.Rows[j].Cells["Company"].Value.ToString() == company && GvProductInfo.Rows[j].Cells["Unit"].Value.ToString() == unit)
                                {
                                    if (GvProductInfo.Rows[j].Cells["BatchNo"].Value.ToString() == batchno)
                                        MessageBox.Show("You already used this batch." + Environment.NewLine + "You can update that batch quantity.", "Warning");
                                    else
                                        MessageBox.Show("You already entered this product.", "Warning");
                                    goto t;
                                }
                            }
                            string colorname = AIOInventorySystem.Desk.Forms.frmBarcodeChoice.glcolorname;
                            string glsize = "";
                            try
                            {
                                glsize = Convert.ToString(AIOInventorySystem.Desk.Forms.frmBarcodeChoice.glsize);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    if (colindex != -1)
                                        glsize = Convert.ToString(GvProductInfo.Rows[colindex].Cells["Sizec"].Value);
                                    else
                                        glsize = "";
                                }
                                catch (Exception)
                                { glsize = ""; }
                            }
                            GvProductInfo.Rows.Add();
                            int i = GvProductInfo.RowCount;
                            GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                            GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                            GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                            GvProductInfo.Rows[i - 1].Cells["ApplyUnit"].Value = cmbUnit.Text;
                            GvProductInfo.Rows[i - 1].Cells["Quantity"].Value = txtQuantity.Text;
                            if (productentrytype == 1 || productentrytype == 2)
                                GvProductInfo.Rows[i - 1].Cells["BatchNo"].Value = cmbBatchNo.Text;
                            else
                                GvProductInfo.Rows[i - 1].Cells["BatchNo"].Value = "";
                            GvProductInfo.Rows[i - 1].Cells["Barcodeg"].Value = barcode;
                            GvProductInfo.Rows[i - 1].Cells["Sizeg"].Value = glsize;
                            GvProductInfo.Rows[i - 1].Cells["DeptNameg"].Value = deptname;
                            GvProductInfo.Rows[i - 1].Cells["ColorNameg"].Value = pcolor;
                            GvProductInfo.Rows[i - 1].Cells["Categoryg"].Value = category;
                            GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                        }
                    t:
                        {
                            txtpname.Text = "";
                            cmbUnit.Items.Clear();
                            txtQuantity.Text = "";
                            cmbBatchNo.Text = "";                            
                            barcode = ""; category = ""; deptname = "";
                            this.ActiveControl = txtpname;
                        }
                    }
                    else
                    {
                        string Proname = txtpname.Text.Trim().Split(',')[0];
                        string company = txtpname.Text.Trim().Split(',')[1];
                        string unit = txtpname.Text.Trim().Split(',')[2];
                        string batchno = cmbBatchNo.Text;

                        for (int j = 0; j < GvProductInfo.RowCount; j++)
                        {
                            if (j != colindex)
                            {
                                if (ProductCategory == "Motor")
                                {
                                    if (GvProductInfo.Rows[j].Cells["ProductNameg"].Value.ToString() == Proname && GvProductInfo.Rows[j].Cells["Company"].Value.ToString() == company && GvProductInfo.Rows[j].Cells["Unit"].Value.ToString() == unit && GvProductInfo.Rows[j].Cells["Code"].Value.ToString() == dtgvcode.Rows[0].Cells[0].Value.ToString())
                                    {
                                        MessageBox.Show("You already entered this product.", "Warning");
                                        goto t;
                                    }
                                }
                                else
                                {
                                    if (GvProductInfo.Rows[j].Cells["ProductNameg"].Value.ToString() == Proname && GvProductInfo.Rows[j].Cells["Company"].Value.ToString() == company && GvProductInfo.Rows[j].Cells["Unit"].Value.ToString() == unit)
                                    {
                                        if (GvProductInfo.Rows[j].Cells["BatchNo"].Value.ToString() == batchno && (productentrytype == 1 || productentrytype == 2))
                                            MessageBox.Show("You already used this batch." + Environment.NewLine + "You can update that batch quantity.", "Warning");
                                        else
                                            MessageBox.Show("You already entered this product.", "Warning");
                                        goto t;
                                    }
                                }
                            }
                        }
                        string colorname = AIOInventorySystem.Desk.Forms.frmBarcodeChoice.glcolorname;
                        string glsize = "";
                        
                            try
                            {
                                glsize = Convert.ToString(AIOInventorySystem.Desk.Forms.frmBarcodeChoice.glsize);
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    if (colindex != -1)
                                        glsize = Convert.ToString(GvProductInfo.Rows[colindex].Cells["Sizec"].Value);
                                    else
                                        glsize = "";
                                }
                                catch (Exception)
                                { glsize = ""; }
                            }                        
                        GvProductInfo.Rows[colindex].Cells["ProductNameg"].Value = Proname;
                        GvProductInfo.Rows[colindex].Cells["Company"].Value = company;
                        GvProductInfo.Rows[colindex].Cells["Unit"].Value = unit;
                        GvProductInfo.Rows[colindex].Cells["ApplyUnit"].Value = cmbUnit.Text;
                        GvProductInfo.Rows[colindex].Cells["Quantity"].Value = txtQuantity.Text;
                        if (ProductCategory == "Motor")
                            GvProductInfo.Rows[colindex].Cells["Code"].Value = dtgvcode.Rows[0].Cells[0].Value.ToString();
                        if (productentrytype == 1 || productentrytype == 2)
                            GvProductInfo.Rows[colindex].Cells["BatchNo"].Value = cmbBatchNo.Text;
                        GvProductInfo.Rows[colindex].Cells["Barcodeg"].Value = barcode;
                        GvProductInfo.Rows[colindex].Cells["Sizeg"].Value = glsize;
                        GvProductInfo.Rows[colindex].Cells["DeptNameg"].Value = deptname;
                        GvProductInfo.Rows[colindex].Cells["ColorNameg"].Value = pcolor;
                        GvProductInfo.Rows[colindex].Cells["Categoryg"].Value = category;
                    t:
                        {
                            txtpname.Text = "";
                            cmbUnit.Items.Clear();
                            txtQuantity.Text = ""; colindex = -1;
                            cmbBatchNo.Text = "";
                            barcode = ""; colorname = ""; category = ""; deptname = "";
                            this.ActiveControl = txtpname;
                        }
                    }
                a: { }
                    if (GvProductInfo.Rows.Count > 0)
                    {
                        cmbgst.Enabled = false;
                        cmbGodownName.Enabled = false;
                    }
                    else
                    {
                        cmbgst.Enabled = true;
                        cmbGodownName.Enabled = true;
                    }
                }
                else
                {
                    if (txtpname.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter Product Name.", "Warning");
                        this.ActiveControl = txtpname;
                    }
                    else if ((productentrytype == 1 || productentrytype == 2) && cmbBatchNo.Text == "")
                    {
                        MessageBox.Show("Select Batch No.", "Warning");
                        this.ActiveControl = cmbBatchNo;
                    }
                    else if (txtQuantity.Text == "")
                    {
                        MessageBox.Show("Enter Quantity.", "Warning");
                        this.ActiveControl = txtQuantity;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                maxdeadid();
                DeadMasterRepository deadmasterrepo = new DeadMasterRepository();
                DeadDetailRepository deaddetailrepo = new DeadDetailRepository();
                var StockId = deadmasterrepo.GetAll().Where(t => t.DeadNo == Convert.ToInt32(txtdeadno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (StockId != null)
                {
                    MessageBox.Show("Already Saved This Record.", "Warning");
                    this.ActiveControl = btnNew;
                }
                else
                {
                    if (GvProductInfo.Rows.Count != 0)
                    {
                        DeadProductMaster deadmaster = new DeadProductMaster();
                        maxdeadid();
                        deadmaster.DeadNo = Convert.ToInt32(txtdeadno.Text);
                        deadmaster.DeadDate = Convert.ToDateTime(dtpdeaddate.Value);
                        deadmaster.Vat = Convert.ToDecimal(cmbgst.Text);
                        deadmaster.TransactionYear = CommonMethod.TransactionYear;
                        deadmaster.CompId = CommonMethod.CompId;
                        deadmaster.StockIn = Convert.ToInt32(cmbGodownName.SelectedValue);
                        deadmasterrepo.Add(deadmaster);
                        deadmasterrepo.Save();

                        DeadProductDetail stockdata = new DeadProductDetail();
                        StockRepository updateStockRepo = new StockRepository();
                        ProductRepository productrepo = new ProductRepository();
                        StockRepository stockRepo = new StockRepository();
                        GodownStockRepository GstockRepo = new GodownStockRepository();
                        GodownStockRepository updateGStockRepo = new GodownStockRepository();
                        for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                        {
                            deaddetailrepo = new DeadDetailRepository();
                            stockdata.DeadNo = deadmaster.Id;
                            stockdata.ProductName = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                            stockdata.CompanyName = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                            stockdata.Unit = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                            stockdata.SaleUnit = Convert.ToString(GvProductInfo.Rows[i].Cells["ApplyUnit"].Value);
                            if (Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value) != Convert.ToString(GvProductInfo.Rows[i].Cells["ApplyUnit"].Value))
                            {
                                productrepo = new ProductRepository();
                                var productData = productrepo.GetAll().Where(t => t.ProductName == GvProductInfo.Rows[i].Cells["ProductNameg"].Value.ToString() && t.ManufactureCompany == GvProductInfo.Rows[i].Cells["Company"].Value.ToString() && t.Uniti == GvProductInfo.Rows[i].Cells["Unit"].Value.ToString()).FirstOrDefault();
                                stockdata.Quantity = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value) * (Convert.ToDecimal(productData.UnitQty) / Convert.ToDecimal(productData.AlternateUnitQty));
                            }
                            else
                                stockdata.Quantity = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value);
                            stockdata.Code = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                            stockdata.BatchNo = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                            stockdata.Barcode = Convert.ToString(GvProductInfo.Rows[i].Cells["Barcodeg"].Value);
                            stockdata.Size = Convert.ToString(GvProductInfo.Rows[i].Cells["Sizeg"].Value);
                            stockdata.DeptName = Convert.ToString(GvProductInfo.Rows[i].Cells["DeptNameg"].Value);
                            stockdata.ColorName = Convert.ToString(GvProductInfo.Rows[i].Cells["ColorNameg"].Value);
                            stockdata.Category = Convert.ToString(GvProductInfo.Rows[i].Cells["Categoryg"].Value);
                            deaddetailrepo.Add(stockdata);
                            deaddetailrepo.Save();
                            int deadid = stockdata.Id;
                            batchtableenrty(i, deadid);

                            try
                            {
                                if (deadmaster.StockIn == 0)
                                {
                                    AIOInventorySystem.Data.Model.Stock updateSData = new Data.Model.Stock();
                                    string proName = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                    string comName = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                    string unit = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                    string size = Convert.ToString(GvProductInfo.Rows[i].Cells["Sizeg"].Value);
                                    var productData = productrepo.GetAll().Where(t => t.ProductName == proName && t.ManufactureCompany == comName && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                    var stockData1 = stockRepo.GetAll().Where(t => t.ProductName == proName && t.MfgCompany == comName && t.Unit == unit && t.Size == size && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                    decimal rate = 0;
                                    if (stockData1 != null)
                                    {
                                        var updateStockData = stockRepo.GetAll().Where(t => t.ProductName == proName && t.MfgCompany == comName && t.Unit == unit && t.Size == size && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        int updateID = updateStockData.Id; ;
                                        decimal dQty = 0, ramQty = 0, oqty = 0;
                                        try
                                        {
                                            deaddetailrepo = new DeadDetailRepository();
                                            dQty = Convert.ToDecimal(deaddetailrepo.GetAll().Where(t => t.ProductName == proName && t.CompanyName == comName && t.Unit == unit && t.Size == size && t.DeadProductMaster.TransactionYear == CommonMethod.TransactionYear && t.DeadProductMaster.CompId == CommonMethod.CompId && t.DeadProductMaster.StockIn == deadmaster.StockIn).Sum(t => t.Quantity));
                                            try
                                            { oqty = Convert.ToDecimal(updateStockData.OpeningQty); }
                                            catch (Exception)
                                            { oqty = 0; }
                                            ramQty = oqty + Convert.ToDecimal(updateStockData.PurchaseQty) - Convert.ToDecimal(updateStockData.PurReturnQty) - Convert.ToDecimal(updateStockData.SaleQty) + Convert.ToDecimal(updateStockData.SaleReturnQty) - dQty;
                                        }
                                        catch (Exception)
                                        { }
                                        updateSData.ProductId = productData.ProductCode;
                                        updateSData.ProductName = proName;
                                        updateSData.MfgCompany = comName;
                                        updateSData.Unit = unit;
                                        updateSData.OpeningQty = oqty;
                                        updateSData.PurchaseQty = Convert.ToDecimal(updateStockData.PurchaseQty);
                                        updateSData.SaleQty = updateStockData.SaleQty;
                                        updateSData.DeadQty = dQty;
                                        updateSData.RemQty = ramQty;
                                        updateSData.PurReturnQty = updateStockData.PurReturnQty;
                                        updateSData.SaleReturnQty = updateStockData.SaleReturnQty;

                                        try
                                        {
                                            db.connect();
                                            SqlCommand command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
                                            command.CommandType = CommandType.StoredProcedure;

                                            command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = proName;
                                            command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = comName;
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
                                    GodownStock updateGData = new GodownStock();
                                    string proName = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                    string comName = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                    string unit = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                    string qty = Convert.ToString(GvProductInfo.Rows[i].Cells["Quantity"].Value);
                                    //string Size = Convert.ToString(GvProductInfo.Rows[i].Cells["Sizeg"].Value);
                                    bool vat;
                                    if (cmbgst.Text == "0.00")
                                        vat = false;
                                    else
                                        vat = true;
                                    var productData = productrepo.GetAll().Where(t => t.ProductName == proName && t.ManufactureCompany == comName && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                    var stockData1 = GstockRepo.GetAll().Where(t => t.ProductName == proName && t.MfgCompany == comName && t.Unit == unit && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.Vat == vat).FirstOrDefault();
                                    if (stockData1 != null)
                                    {
                                        var updateStockData = GstockRepo.GetAll().Where(t => t.ProductName == proName && t.MfgCompany == comName && t.Unit == unit && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.Vat == vat).FirstOrDefault();
                                        int updateID = updateStockData.Id; ;
                                        decimal ramQty = 0, oqty = 0, removeqty = 0;
                                        try
                                        {
                                            try
                                            { oqty = Convert.ToDecimal(updateStockData.OpeningQty); }
                                            catch (Exception)
                                            { oqty = 0; }
                                            removeqty = Convert.ToDecimal(updateStockData.RemoveQty) + Convert.ToDecimal(qty);
                                            ramQty = oqty + Convert.ToDecimal(updateStockData.AddQty) - Convert.ToDecimal(removeqty);
                                        }
                                        catch (Exception)
                                        { }
                                        updateGData.GoDownId = deadmaster.StockIn;
                                        updateGData.ProductName = proName;
                                        updateGData.MfgCompany = comName;
                                        updateGData.Unit = unit;
                                        updateGData.OpeningQty = oqty;
                                        updateGData.AddQty = Convert.ToDecimal(updateStockData.AddQty);
                                        updateGData.RemoveQty = Convert.ToDecimal(removeqty);
                                        updateGData.RemQty = ramQty;

                                        updateGData.TransactionYear = CommonMethod.TransactionYear;
                                        updateGData.CompId = CommonMethod.CompId;
                                        updateGData.Vat = updateStockData.Vat;
                                        updateGData.Id = updateID;
                                        updateGStockRepo.Edit(updateGData);
                                        updateGStockRepo.Save();
                                    }
                                }
                            }
                            catch (Exception)
                            { }
                        }
                        MessageBox.Show("Dead Stock saved.", "Success");
                        clear();
                        maxdeadid();
                        this.ActiveControl = txtpname;
                        updateStockRepo.Dispose();
                        productrepo.Dispose();
                        stockRepo.Dispose();
                        updateGStockRepo.Dispose();
                        GstockRepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Add Products first.", "Warning");
                        this.ActiveControl = txtpname;
                    }
                }
                deadmasterrepo.Dispose();
                deaddetailrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                string proName = "", CompanyName = "", unit = "", batchno = "",sizeg="";
                decimal Quantity = 0;
                if (GvProductInfo.Rows.Count != 0)
                {
                    DeadMasterRepository deadmasterrepo = new DeadMasterRepository();
                    DeadDetailRepository deaddetailrepo = new DeadDetailRepository();
                    DeadProductMaster deadmaster = new DeadProductMaster();
                    deadmaster.DeadNo = Convert.ToInt32(txtdeadno.Text);
                    deadmaster.DeadDate = Convert.ToDateTime(dtpdeaddate.Value);
                    deadmaster.Vat = Convert.ToDecimal(cmbgst.Text);
                    deadmaster.TransactionYear = CommonMethod.TransactionYear;
                    deadmaster.CompId = CommonMethod.CompId;
                    deadmaster.StockIn = Convert.ToInt32(cmbGodownName.SelectedValue);
                    deadmaster.Id = DeadId;
                    deadmasterrepo.Edit(deadmaster);
                    deadmasterrepo.Save();

                    DeadProductDetail stockdata = new DeadProductDetail();
                    StockRepository stockRepo = new StockRepository();
                    StockRepository updateStockRepo = new StockRepository();
                    ProductRepository productrepo = new ProductRepository();
                    GodownStockRepository GstockRepo = new GodownStockRepository();
                    GodownStockRepository updateGStockRepo = new GodownStockRepository();
                    AIOInventorySystem.Data.Model.Stock sData = new Data.Model.Stock();
                    if (deleteid.Count != 0)
                    {
                        for (int x = 0; x < deleteid.Count; x++)
                        {
                            DeadDetailRepository deaddetailrepo1 = new DeadDetailRepository();
                            DeadProductDetail ddetaildata = deaddetailrepo1.GetById(deleteid[x]);
                            try
                            {
                                if (deadmaster.StockIn == 0)
                                {
                                    AIOInventorySystem.Data.Model.Stock updateSData = new Data.Model.Stock();
                                    proName = ddetaildata.ProductName;
                                    CompanyName = ddetaildata.CompanyName;
                                    Quantity = Convert.ToDecimal(ddetaildata.Quantity);
                                    unit = Convert.ToString(ddetaildata.Unit);
                                    sizeg = Convert.ToString(ddetaildata.Size);
                                    batchno = "";
                                    if (string.IsNullOrEmpty(ddetaildata.BatchNo))
                                        batchno = ddetaildata.BatchNo;
                                    var productData = productrepo.GetAll().Where(t => t.ProductName == proName && t.ManufactureCompany == CompanyName && t.Uniti == unit).FirstOrDefault();
                                    var stockData = stockRepo.GetAll().Where(t => t.ProductName == proName && t.MfgCompany == CompanyName && t.Unit == unit && t.Size == sizeg && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                    if (stockData != null)
                                    {
                                        var updateStockData = stockRepo.GetAll().Where(t => t.ProductName == proName && t.MfgCompany == CompanyName && t.Unit == unit && t.Size == sizeg && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        int updateID = updateStockData.Id;
                                        decimal dQty = 0, ramQty = 0, rate = 0, oqty = 0;
                                        try
                                        {
                                            try
                                            { oqty = Convert.ToDecimal(updateStockData.OpeningQty); }
                                            catch (Exception)
                                            { oqty = 0; }
                                            decimal tdeadqty = Convert.ToDecimal(deaddetailrepo.GetAll().Where(t => t.ProductName == proName && t.CompanyName == CompanyName && t.Unit == unit && t.Size == sizeg && t.DeadProductMaster.TransactionYear == CommonMethod.TransactionYear && t.DeadProductMaster.CompId == CommonMethod.CompId && t.DeadProductMaster.StockIn == deadmaster.StockIn).Sum(t => t.Quantity));

                                            dQty = tdeadqty - Quantity;
                                            ramQty = oqty + Convert.ToDecimal(updateStockData.PurchaseQty) - Convert.ToDecimal(updateStockData.PurReturnQty) - Convert.ToDecimal(updateStockData.SaleQty) + Convert.ToDecimal(updateStockData.SaleReturnQty) - dQty;
                                        }
                                        catch (Exception)
                                        { }
                                        updateSData.ProductId = productData.ProductCode;
                                        updateSData.ProductName = proName;
                                        updateSData.MfgCompany = CompanyName;
                                        updateSData.Unit = unit;
                                        updateSData.OpeningQty = oqty;
                                        updateSData.PurchaseQty = Convert.ToDecimal(updateStockData.PurchaseQty);
                                        updateSData.SaleQty = updateStockData.SaleQty;
                                        updateSData.DeadQty = dQty;
                                        updateSData.RemQty = ramQty;
                                        updateSData.PurReturnQty = updateStockData.PurReturnQty;
                                        updateSData.SaleReturnQty = updateStockData.SaleReturnQty;

                                        try
                                        {
                                            db.connect();
                                            SqlCommand command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
                                            command.CommandType = CommandType.StoredProcedure;

                                            command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = proName;
                                            command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = CompanyName;
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
                                    GodownStock updateGData = new GodownStock();
                                    proName = ddetaildata.ProductName;
                                    CompanyName = ddetaildata.CompanyName;
                                    Quantity = Convert.ToDecimal(ddetaildata.Quantity);
                                    unit = Convert.ToString(ddetaildata.Unit);
                                    sizeg = Convert.ToString(ddetaildata.Size);
                                    bool vat;
                                    if (cmbgst.Text == "0.00")
                                        vat = false;
                                    else
                                        vat = true;
                                    var productData = productrepo.GetAll().Where(t => t.ProductName == proName && t.ManufactureCompany == CompanyName && t.Uniti == unit).FirstOrDefault();
                                    var stockData = GstockRepo.GetAll().Where(t => t.ProductName == proName && t.MfgCompany == CompanyName && t.Unit == unit && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.Vat == vat).FirstOrDefault();
                                    if (stockData != null)
                                    {
                                        var updateStockData = GstockRepo.GetAll().Where(t => t.ProductName == proName && t.MfgCompany == CompanyName && t.Unit == unit && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.Vat == vat).FirstOrDefault();
                                        int updateID = updateStockData.Id;
                                        decimal ramQty = 0, oqty = 0, removeqty = 0;
                                        try
                                        {
                                            try
                                            { oqty = Convert.ToDecimal(updateStockData.OpeningQty); }
                                            catch (Exception)
                                            { oqty = 0; }
                                            removeqty = Convert.ToDecimal(updateStockData.RemoveQty) - Quantity;
                                            ramQty = oqty + Convert.ToDecimal(updateStockData.AddQty) - Convert.ToDecimal(removeqty);
                                        }
                                        catch (Exception)
                                        { }
                                        updateGData.GoDownId = deadmaster.StockIn;
                                        updateGData.ProductName = proName;
                                        updateGData.MfgCompany = CompanyName;
                                        updateGData.Unit = unit;
                                        updateGData.OpeningQty = oqty;
                                        updateGData.AddQty = Convert.ToDecimal(updateStockData.AddQty);
                                        updateGData.RemoveQty = Convert.ToDecimal(removeqty);
                                        updateGData.RemQty = ramQty;
                                        updateGData.TransactionYear = CommonMethod.TransactionYear;
                                        updateGData.CompId = CommonMethod.CompId;
                                        updateGData.Vat = updateStockData.Vat;
                                        updateGData.Id = updateID;
                                        updateGStockRepo.Edit(updateGData);
                                        updateGStockRepo.Save();
                                    }
                                }

                                if (productentrytype == 1 || productentrytype == 2)
                                {
                                    BatchRepository batchrepo = new BatchRepository();
                                    Batch batch = batchrepo.GetAll().Where(t => t.ProductName == proName && t.ManufactureCompany == CompanyName && t.Unit == unit && t.BatchNo == batchno && t.DeadId == deleteid[x] && t.CompId == CommonMethod.CompId).LastOrDefault();
                                    batchrepo.Remove(batch);
                                    batchrepo.Save();
                                }
                                DeadDetailRepository deaddetailrepo2 = new DeadDetailRepository();
                                DeadProductDetail stockdateil = deaddetailrepo2.GetById(deleteid[x]);
                                deaddetailrepo2.Remove(stockdateil);
                                deaddetailrepo2.Save();
                                deaddetailrepo2.Dispose();
                            }
                            catch (Exception)
                            { }
                            deaddetailrepo1.Dispose();
                        }
                    }

                    for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                    {
                        proName = "";
                        CompanyName = "";
                        Quantity = 0;
                        unit = "";
                        sizeg = "";
                        int Rowid = Convert.ToInt32(GvProductInfo.Rows[i].Cells["Id"].Value);
                        if (Rowid != 0)
                        {
                            DeadProductDetail bdetaildata = deaddetailrepo.GetById(Rowid);
                            if (bdetaildata.ProductName != Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value))
                            {
                                try
                                {
                                    if (deadmaster.StockIn == 0)
                                    {
                                        AIOInventorySystem.Data.Model.Stock updateSData = new Data.Model.Stock();
                                        proName = bdetaildata.ProductName;
                                        CompanyName = bdetaildata.CompanyName;
                                        Quantity = Convert.ToDecimal(bdetaildata.Quantity);
                                        unit = Convert.ToString(bdetaildata.Unit);
                                        sizeg = Convert.ToString(bdetaildata.Size);
                                        var productData = productrepo.GetAll().Where(t => t.ProductName == proName && t.ManufactureCompany == CompanyName && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        var stockData = stockRepo.GetAll().Where(t => t.ProductName == proName && t.MfgCompany == CompanyName && t.Unit == unit && t.Size == sizeg && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        if (stockData != null)
                                        {
                                            var updateStockData = stockRepo.GetAll().Where(t => t.ProductName == proName && t.MfgCompany == CompanyName && t.Unit == unit && t.Size == sizeg && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                            int updateID = updateStockData.Id;
                                            decimal dQty = 0, ramQty = 0, rate = 0, oqty = 0;
                                            try
                                            {
                                                try
                                                { oqty = Convert.ToDecimal(updateStockData.OpeningQty); }
                                                catch (Exception)
                                                { oqty = 0; }
                                                decimal tdeadqty = Convert.ToDecimal(deaddetailrepo.GetAll().Where(t => t.ProductName == proName && t.CompanyName == CompanyName && t.Unit == unit && t.Size == sizeg && t.DeadProductMaster.TransactionYear == CommonMethod.TransactionYear && t.DeadProductMaster.CompId == CommonMethod.CompId && t.DeadProductMaster.StockIn == deadmaster.StockIn).Sum(t => t.Quantity));
                                                dQty = tdeadqty - Quantity;
                                                ramQty = oqty + Convert.ToDecimal(updateStockData.PurchaseQty) - Convert.ToDecimal(updateStockData.PurReturnQty) - Convert.ToDecimal(updateStockData.SaleQty) + Convert.ToDecimal(updateStockData.SaleReturnQty) - dQty;
                                            }
                                            catch (Exception)
                                            { }
                                            updateSData.ProductId = productData.ProductCode;
                                            updateSData.ProductName = proName;
                                            updateSData.MfgCompany = CompanyName;
                                            updateSData.Unit = unit;
                                            updateSData.OpeningQty = oqty;
                                            updateSData.PurchaseQty = Convert.ToDecimal(updateStockData.PurchaseQty);
                                            updateSData.SaleQty = updateStockData.SaleQty;
                                            updateSData.DeadQty = dQty;
                                            updateSData.RemQty = ramQty;
                                            updateSData.PurReturnQty = updateStockData.PurReturnQty;
                                            updateSData.SaleReturnQty = updateStockData.SaleReturnQty;

                                            try
                                            {
                                                db.connect();
                                                SqlCommand command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
                                                command.CommandType = CommandType.StoredProcedure;

                                                command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = proName;
                                                command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = CompanyName;
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
                                        GodownStock updateGData = new GodownStock();
                                        proName = bdetaildata.ProductName;
                                        CompanyName = bdetaildata.CompanyName;
                                        Quantity = Convert.ToDecimal(bdetaildata.Quantity);
                                        unit = Convert.ToString(bdetaildata.Unit);
                                        //sizeg = Convert.ToString(bdetaildata.Size);
                                        bool vat;
                                        if (cmbgst.Text == "0.00")
                                            vat = false;
                                        else
                                            vat = true;
                                        var productData = productrepo.GetAll().Where(t => t.ProductName == proName && t.ManufactureCompany == CompanyName && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                        var stockData = GstockRepo.GetAll().Where(t => t.ProductName == proName && t.MfgCompany == CompanyName && t.Unit == unit && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.Vat == vat).FirstOrDefault();
                                        if (stockData != null)
                                        {
                                            var updateStockData = GstockRepo.GetAll().Where(t => t.ProductName == proName && t.MfgCompany == CompanyName && t.Unit == unit && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.Vat == vat).FirstOrDefault();
                                            int updateID = updateStockData.Id;
                                            decimal ramQty = 0, oqty = 0, removeqty = 0;
                                            try
                                            {
                                                try
                                                { oqty = Convert.ToDecimal(updateStockData.OpeningQty); }
                                                catch (Exception)
                                                { oqty = 0; }
                                                removeqty = Convert.ToDecimal(updateStockData.RemoveQty) - Quantity;
                                                ramQty = oqty + Convert.ToDecimal(updateStockData.AddQty) - Convert.ToDecimal(removeqty);
                                            }
                                            catch (Exception)
                                            { }
                                            updateGData.GoDownId = deadmaster.StockIn;
                                            updateGData.ProductName = proName;
                                            updateGData.MfgCompany = CompanyName;
                                            updateGData.Unit = unit;
                                            updateGData.OpeningQty = oqty;
                                            updateGData.AddQty = Convert.ToDecimal(updateStockData.AddQty);
                                            updateGData.RemoveQty = Convert.ToDecimal(removeqty);
                                            updateGData.RemQty = ramQty;
                                            updateGData.TransactionYear = CommonMethod.TransactionYear;
                                            updateGData.CompId = CommonMethod.CompId;
                                            updateGData.Vat = updateStockData.Vat;
                                            updateGData.Id = updateID;
                                            updateGStockRepo.Edit(updateGData);
                                            updateGStockRepo.Save();
                                        }
                                    }
                                }
                                catch (Exception)
                                { }
                            }
                            bdetaildata.DeadNo = deadmaster.Id;
                            bdetaildata.ProductName = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                            bdetaildata.CompanyName = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                            bdetaildata.Unit = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                            bdetaildata.SaleUnit = Convert.ToString(GvProductInfo.Rows[i].Cells["ApplyUnit"].Value);
                            if (Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value) != Convert.ToString(GvProductInfo.Rows[i].Cells["ApplyUnit"].Value))
                            {
                                productrepo = new ProductRepository();
                                var productData = productrepo.GetAll().Where(t => t.ProductName == GvProductInfo.Rows[i].Cells["ProductNameg"].Value.ToString() && t.ManufactureCompany == GvProductInfo.Rows[i].Cells["Company"].Value.ToString() && t.Uniti == GvProductInfo.Rows[i].Cells["Unit"].Value.ToString()).FirstOrDefault();
                                bdetaildata.Quantity = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value) * (Convert.ToDecimal(productData.UnitQty) / Convert.ToDecimal(productData.AlternateUnitQty));
                            }
                            else
                                bdetaildata.Quantity = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value);
                            bdetaildata.Code = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                            bdetaildata.BatchNo = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                            bdetaildata.Barcode = Convert.ToString(GvProductInfo.Rows[i].Cells["Barcodeg"].Value);
                            bdetaildata.Size = Convert.ToString(GvProductInfo.Rows[i].Cells["Sizeg"].Value);
                            bdetaildata.DeptName = Convert.ToString(GvProductInfo.Rows[i].Cells["DeptNameg"].Value);
                            bdetaildata.ColorName = Convert.ToString(GvProductInfo.Rows[i].Cells["ColorNameg"].Value);
                            bdetaildata.Category = Convert.ToString(GvProductInfo.Rows[i].Cells["Categoryg"].Value);
                            deaddetailrepo.Edit(bdetaildata);
                            deaddetailrepo.Save();
                        }
                        else
                        {
                            stockdata.DeadNo = deadmaster.Id;
                            stockdata.ProductName = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                            stockdata.CompanyName = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                            stockdata.Unit = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                            stockdata.SaleUnit = Convert.ToString(GvProductInfo.Rows[i].Cells["ApplyUnit"].Value);
                            if (Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value) != Convert.ToString(GvProductInfo.Rows[i].Cells["ApplyUnit"].Value))
                            {
                                productrepo = new ProductRepository();
                                var productData = productrepo.GetAll().Where(t => t.ProductName == GvProductInfo.Rows[i].Cells["ProductNameg"].Value.ToString() && t.ManufactureCompany == GvProductInfo.Rows[i].Cells["Company"].Value.ToString() && t.Uniti == GvProductInfo.Rows[i].Cells["Unit"].Value.ToString()).FirstOrDefault();
                                stockdata.Quantity = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value) * (Convert.ToDecimal(productData.UnitQty) / Convert.ToDecimal(productData.AlternateUnitQty));
                            }
                            else
                                stockdata.Quantity = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value);
                            stockdata.Code = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                            stockdata.BatchNo = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                            stockdata.Barcode = Convert.ToString(GvProductInfo.Rows[i].Cells["Barcodeg"].Value);
                            stockdata.Size = Convert.ToString(GvProductInfo.Rows[i].Cells["Sizeg"].Value);
                            stockdata.DeptName = Convert.ToString(GvProductInfo.Rows[i].Cells["DeptNameg"].Value);
                            stockdata.ColorName = Convert.ToString(GvProductInfo.Rows[i].Cells["ColorNameg"].Value);
                            stockdata.Category = Convert.ToString(GvProductInfo.Rows[i].Cells["Categoryg"].Value);
                            deaddetailrepo.Add(stockdata);
                            deaddetailrepo.Save();
                        }
                        try
                        {
                            if (deadmaster.StockIn == 0)
                            {
                                AIOInventorySystem.Data.Model.Stock updateSData = new Data.Model.Stock();
                                proName = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                CompanyName = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                Quantity = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value);
                                unit = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                sizeg = Convert.ToString(GvProductInfo.Rows[i].Cells["Sizeg"].Value);
                                var productData = productrepo.GetAll().Where(t => t.ProductName == proName && t.ManufactureCompany == CompanyName && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                var stockData = stockRepo.GetAll().Where(t => t.ProductName == proName && t.MfgCompany == CompanyName && t.Unit == unit && t.Size==sizeg && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (stockData == null)
                                    MessageBox.Show("Quantity are not available.", "Warning");
                                else
                                {
                                    var updateStockData = stockRepo.GetAll().Where(t => t.ProductName == proName && t.MfgCompany == CompanyName && t.Unit == unit && t.Size == sizeg && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                    int updateID = updateStockData.Id; ;
                                    decimal dQty = 0, oqty = 0, ramQty = 0, rate = 0;
                                    try
                                    {
                                        try
                                        { oqty = Convert.ToDecimal(updateStockData.OpeningQty); }
                                        catch (Exception)
                                        { oqty = 0; }
                                        decimal tdeadqty = Convert.ToDecimal(deaddetailrepo.GetAll().Where(t => t.ProductName == proName && t.CompanyName == CompanyName && t.Unit == unit && t.Size == sizeg && t.DeadProductMaster.TransactionYear == CommonMethod.TransactionYear && t.DeadProductMaster.CompId == CommonMethod.CompId && t.DeadProductMaster.StockIn == deadmaster.StockIn).Sum(t => t.Quantity));
                                        dQty = tdeadqty;
                                        ramQty = oqty + Convert.ToDecimal(updateStockData.PurchaseQty) - Convert.ToDecimal(updateStockData.PurReturnQty) - Convert.ToDecimal(updateStockData.SaleQty) + Convert.ToDecimal(updateStockData.SaleReturnQty) - dQty;
                                    }
                                    catch (Exception)
                                    { }
                                    updateSData.ProductId = productData.ProductCode;
                                    updateSData.ProductName = proName;
                                    updateSData.MfgCompany = CompanyName;
                                    updateSData.Unit = unit;
                                    updateSData.OpeningQty = oqty;
                                    updateSData.PurchaseQty = Convert.ToDecimal(updateStockData.PurchaseQty);
                                    updateSData.SaleQty = updateStockData.SaleQty;
                                    updateSData.DeadQty = dQty;
                                    updateSData.RemQty = ramQty;
                                    updateSData.PurReturnQty = updateStockData.PurReturnQty;
                                    updateSData.SaleReturnQty = updateStockData.SaleReturnQty;

                                    try
                                    {
                                        db.connect();
                                        SqlCommand command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
                                        command.CommandType = CommandType.StoredProcedure;

                                        command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = proName;
                                        command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = CompanyName;
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
                                GodownStock updateGData = new GodownStock();
                                proName = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                CompanyName = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                Quantity = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value);
                                unit = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);

                                bool vat;
                                if (cmbgst.Text == "0.00")
                                    vat = false;
                                else
                                    vat = true;

                                var productData = productrepo.GetAll().Where(t => t.ProductName == proName && t.ManufactureCompany == CompanyName && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                var stockData = GstockRepo.GetAll().Where(t => t.ProductName == proName && t.MfgCompany == CompanyName && t.Unit == unit && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.Vat == vat).FirstOrDefault();
                                if (stockData == null)
                                    MessageBox.Show("Quantity are not available.", "Warning");
                                else
                                {
                                    var updateStockData = GstockRepo.GetAll().Where(t => t.ProductName == proName && t.MfgCompany == CompanyName && t.Unit == unit && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.Vat == vat).FirstOrDefault();
                                    int updateID = updateStockData.Id; ;
                                    decimal oqty = 0, ramQty = 0, removeqty = 0;
                                    try
                                    {
                                        try
                                        { oqty = Convert.ToDecimal(updateStockData.OpeningQty); }
                                        catch (Exception)
                                        { oqty = 0; }
                                        removeqty = Convert.ToDecimal(updateStockData.RemoveQty) + Quantity;
                                        ramQty = oqty + Convert.ToDecimal(updateStockData.AddQty) - Convert.ToDecimal(removeqty);
                                    }
                                    catch (Exception)
                                    { }
                                    updateGData.GoDownId = deadmaster.StockIn;
                                    updateGData.ProductName = proName;
                                    updateGData.MfgCompany = CompanyName;
                                    updateGData.Unit = unit;
                                    updateGData.OpeningQty = oqty;
                                    updateGData.AddQty = Convert.ToDecimal(updateStockData.AddQty);
                                    updateGData.RemoveQty = Convert.ToDecimal(removeqty);
                                    updateGData.RemQty = ramQty;
                                    updateGData.TransactionYear = CommonMethod.TransactionYear;
                                    updateGData.CompId = CommonMethod.CompId;
                                    updateGData.Vat = updateStockData.Vat;
                                    updateGData.Id = updateID;
                                    updateGStockRepo.Edit(updateGData);
                                    updateGStockRepo.Save();
                                }
                            }
                        }
                        catch (Exception)
                        { }
                    }
                    MessageBox.Show("Dead Stock Updated.", "Success");
                    clear();
                    maxdeadid();
                    this.ActiveControl = txtpname;
                    deadmasterrepo.Dispose();
                    deaddetailrepo.Dispose();
                    stockRepo.Dispose();
                    updateStockRepo.Dispose();
                    productrepo.Dispose();
                    GstockRepo.Dispose();
                    updateGStockRepo.Dispose();
                }
                else
                {
                    MessageBox.Show("Add Products first.", "Warning");
                    this.ActiveControl = txtpname;
                }
            }
            catch (Exception)
            { }
        }

        public void fillgst()
        {
            try
            {
                DataTable dtigst = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforIGST='Y'");
                if (dtigst.Rows.Count > 0)
                {
                    cmbgst.ValueMember = "GSTNo";
                    cmbgst.DisplayMember = "GSTPercent";
                    cmbgst.DataSource = dtigst;
                }
            }
            catch (Exception) { }
        }

        public void fillcombo()
        {
            try
            {
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
            }
            catch (Exception)
            { }
        }

        private void cmbcomanyname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ProductRepository proRepo = new ProductRepository();
                var validProduct = proRepo.GetAll().Where(t => t.Prefix == cmbcomanyname.Text).FirstOrDefault();
                if (validProduct != null)
                {
                    txtpname.Text = "";
                    string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(cnString1))
                    {
                        SqlCommand cmd = null;
                        if (CommonMethod.commProduct == true)
                            cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where Prefix='" + cmbcomanyname.Text + "'", con);
                        else
                            cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where Prefix='" + cmbcomanyname.Text + "' and compid=" + CommonMethod.CompId, con);
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
                        reader.Dispose();
                    }
                }
                else
                {
                    txtpname.Text = "";
                    string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(cnString1))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where compid=" + CommonMethod.CompId, con);
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
                        reader.Dispose();
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
                if (btnSave.Enabled == true)
                    btnSave_Click(null, null);
                else if (btnupdate.Enabled == true)
                    btnupdate_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtpname_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtpname.Text.Trim() != "")
                {
                    if ((txtpname.Text).Contains(","))
                    {
                        try
                        {
                            Glbproname = txtpname.Text.Trim().Split(',')[0];
                            Glbcompany = txtpname.Text.Trim().Split(',')[1];
                            Glbunit = txtpname.Text.Trim().Split(',')[2];                            
                        }
                        catch (Exception)
                        { }
                        ProductRepository productrepo = new ProductRepository();                            
                        if (CommonMethod.commProduct == true)
                           ProductDataGlobal = productrepo.GetAll().Where(t => t.ProductName == Glbproname && t.ManufactureCompany == Glbcompany && t.Uniti == Glbunit).FirstOrDefault();                        
                        else
                            ProductDataGlobal = productrepo.GetAll().Where(t => t.ProductName == Glbproname && t.ManufactureCompany == Glbcompany && t.Uniti == Glbunit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (ProductDataGlobal == null)
                        {
                            MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                            txtpname.Text = "";
                            this.ActiveControl = txtpname;
                        }
                        else
                        {
                            cmbUnit.Items.Clear();
                            cmbUnit.DisplayMember = "Text";
                            cmbUnit.ValueMember = "Value";
                            cmbUnit.Items.Add(new { Text = ProductDataGlobal.Uniti, Value = ProductDataGlobal.Uniti });
                            if (ProductDataGlobal.AlternateUnit != null && ProductDataGlobal.AlternateUnit != "")
                                cmbUnit.Items.Add(new { Text = ProductDataGlobal.AlternateUnit, Value = ProductDataGlobal.AlternateUnit });
                            cmbUnit.SelectedIndex = 0;
                            ProductCategory = ProductDataGlobal.Category;
                            GvProductInfo.Columns[7].Visible = false;
                            ProductCategory = ProductDataGlobal.Category;
                            barcodestatus = ProductDataGlobal.BarcodeStatus;
                            getProductType(Glbproname, Glbcompany, Glbunit);
                            getProductType(Glbproname, Glbcompany, Glbunit);
                        }
                        productrepo.Dispose();
                        if (productentrytype == 1 || productentrytype == 2)
                        {
                            label8.Visible = true;
                            cmbBatchNo.Visible = true;
                            fillbatchcombo(Glbproname, Glbcompany, Glbunit);
                        }
                        else
                        {
                            label8.Visible = false;
                            cmbBatchNo.Visible = false;
                            this.ActiveControl = txtQuantity;
                        }
                    }
                    else
                    {
                        MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                        txtpname.Text = "";
                        this.ActiveControl = txtpname;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal deadqty = 0;
                if (txtpname.Text != "")
                {
                    string proname = "", company = "", unit = "";
                    decimal remq = 0, textqty = 0;

                    proname = txtpname.Text.Trim().Split(',')[0];
                    company = txtpname.Text.Trim().Split(',')[1];
                    unit = txtpname.Text.Trim().Split(',')[2];

                    getProductType(proname, company, unit);
                    if ((txtpname.Text).Contains(","))
                    {
                        if (txtQuantity.Text != "")
                        {
                            try
                            {
                                if (unit != cmbUnit.Text)
                                {
                                    ProductRepository productrepo = new ProductRepository();
                                    var productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                                    if (productdata != null)
                                        textqty = Convert.ToDecimal(txtQuantity.Text) * (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                }
                                else
                                    textqty = Convert.ToDecimal(txtQuantity.Text);
                                if (productentrytype == 0) // standard
                                {
                                    try
                                    {
                                        if (Convert.ToInt32(cmbGodownName.SelectedValue) == 0)
                                        {
                                            string vat = "";
                                            SettingRepository settingrepo = new SettingRepository();
                                            var settingdata = settingrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).LastOrDefault();

                                            if (cmbgst.Text != "")
                                            {
                                                if (Convert.ToDecimal(cmbgst.Text) == 0)
                                                    vat = "n";
                                                else
                                                    vat = "y";
                                            }
                                            else
                                                vat = "a";
                                            db.connect();
                                            SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                                            command.CommandType = CommandType.StoredProcedure;

                                            command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = proname;
                                            command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = company;
                                            command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = unit;
                                            command.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'P';
                                            command.Parameters.Add("@vat", SqlDbType.VarChar).Value = vat;
                                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                                            command.CommandTimeout = 300;
                                            SqlDataReader reader = command.ExecuteReader();
                                            while (reader.Read())
                                            {
                                                remq = Convert.ToDecimal(reader.GetSqlDecimal(11).ToDouble());
                                                if (remq > 0)
                                                {
                                                    if (textqty > remq)
                                                    {
                                                        MessageBox.Show("Your remaining stock is " + remq, "Warning");
                                                        txtQuantity.Text = "";
                                                        this.ActiveControl = txtQuantity;
                                                    }
                                                }
                                                else
                                                {
                                                    MessageBox.Show("Stock is not available.", "Warning");
                                                    txtpname.Text = "";
                                                    txtQuantity.Text = "";
                                                    this.ActiveControl = txtpname;
                                                }
                                            }
                                            if (reader.HasRows == false)
                                            {
                                                MessageBox.Show("Stock is not available.", "Warning");
                                                txtpname.Text = "";
                                                txtQuantity.Text = "";
                                                this.ActiveControl = txtpname;
                                            }
                                            db.CloseConnection();
                                            settingrepo.Dispose();
                                            reader.Dispose();
                                        }
                                        else
                                        {
                                            GodownStockRepository gstockrepo = new GodownStockRepository();
                                            bool vat;
                                            if (cmbgst.Text == "0")
                                                vat = false;
                                            else
                                                vat = true;
                                            var gdata = gstockrepo.GetAll().Where(t => t.ProductName == proname && t.MfgCompany == company && t.Unit == unit && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId && t.Vat == vat).FirstOrDefault();
                                            remq = Convert.ToDecimal(gdata.RemQty);
                                            if (remq > 0)
                                            {
                                                if (textqty > remq)
                                                {
                                                    MessageBox.Show("Your remaining stock is " + remq, "Warning");
                                                    txtQuantity.Text = "";
                                                    this.ActiveControl = txtQuantity;
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Stock is not available.", "Warning");
                                                txtpname.Text = "";
                                                txtQuantity.Text = "";
                                                this.ActiveControl = txtpname;
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    { db.CloseConnection(); }
                                }
                                else if (productentrytype == 1 || productentrytype == 2)  // standard with batch and expiry && with batch
                                {
                                    try
                                    {
                                        BatchRepository batchrepo = new BatchRepository();
                                        decimal usedqty = Convert.ToDecimal(batchrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == cmbBatchNo.Text && t.Status != "0").Sum(t => t.UsedQty));
                                        PurchaseDetailRepository purrepo = new PurchaseDetailRepository();
                                        List<PurchaseDetail> PDData = new List<PurchaseDetail>();

                                        PDData = purrepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == cmbBatchNo.Text).ToList();
                                        decimal purqty = 0, srqty = 0;
                                        if (PDData != null)
                                        {
                                            foreach (var item in PDData)
                                            {
                                                PurchaseMasterRepository pMastRepo = new PurchaseMasterRepository();
                                                PurchaseMaster PMasterData1 = new PurchaseMaster();
                                                PurchaseDetail purdetail11 = new PurchaseDetail();
                                                PMasterData1 = pMastRepo.GetAll().Where(t => t.Id == item.PorderNo).FirstOrDefault();
                                                if (PMasterData1 != null)
                                                    purdetail11 = purrepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == cmbBatchNo.Text && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == PMasterData1.TransactionYear).FirstOrDefault();
                                                decimal prqty = 0;
                                                try
                                                {
                                                    PReturnDetailRepository PurRDRepo = new PReturnDetailRepository();
                                                    var PurDData = PurRDRepo.GetAll().Where(t => t.POID == purdetail11.Id).FirstOrDefault();
                                                    prqty = Convert.ToDecimal(PurDData.Quantity);
                                                }
                                                catch (Exception)
                                                {
                                                    prqty = 0;
                                                }

                                                purqty = purqty + Convert.ToDecimal(purdetail11.Quantity) - prqty;
                                                try
                                                {
                                                    BatchRepository batchrpstr = new BatchRepository();
                                                    List<Batch> BData = new List<Batch>();
                                                    BData = batchrpstr.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == cmbBatchNo.Text && t.Status == "1" && t.PorderNo == item.PorderNo).ToList();
                                                    if (BData != null)
                                                    {
                                                        foreach (var item1 in BData)
                                                        {
                                                            try
                                                            {
                                                                GoodDetailRepository SRRepo = new GoodDetailRepository();
                                                                GoodReturnDetail SRData = new GoodReturnDetail();
                                                                SRData = SRRepo.GetAll().Where(t => t.ProductId == item1.ProductName && t.CompanyName == item1.ManufactureCompany && t.Unit == item1.Unit && t.BNID == Convert.ToInt32(item1.BillId) && t.BatchNo == item1.BatchNo).FirstOrDefault();
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

                                        decimal remqty = purqty - (usedqty - srqty + deadqty);
                                        if (colindex != -1)
                                        {
                                            decimal qty = Convert.ToDecimal(GvProductInfo.Rows[colindex].Cells["Quantity"].Value);
                                            remqty += qty;
                                        }
                                        if (txtQuantity.Text != "")
                                        {
                                            if (Convert.ToDecimal(txtQuantity.Text) > remqty)
                                            {
                                                MessageBox.Show("Your remaining stock is " + remqty, "Warning");
                                                txtQuantity.Text = "";
                                                this.ActiveControl = txtQuantity;
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    { }
                                }
                            }
                            catch (Exception)
                            { }
                        }
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
                if (txtpname.Text != "" && dtgvcode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
                {
                    codeflag = 0;
                    string Proname = txtpname.Text.Trim().Split(',')[0];
                    string company = txtpname.Text.Trim().Split(',')[1];
                    string unit = txtpname.Text.Trim().Split(',')[2];
                    SqlDataAdapter da = new SqlDataAdapter();
                    if (Convert.ToInt32(cmbGodownName.SelectedValue) == 0)
                        da = new SqlDataAdapter("(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + ") and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in( Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))) union SELECT d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null) and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and m.CompId=pm.CompId and (pm.StockIn=0 or pm.StockIn is null)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + ") and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15))) union SELECT d.Code FROM GoodReturnDetail as d inner join GoodReturnMaster as m on m.Id=d.GoodReturnNo Where ProductId='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))", db.Connection);
                    else
                        da = new SqlDataAdapter("(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and TransferTo=Cast('G'+Cast('" + Convert.ToInt32(cmbGodownName.SelectedValue) + "' as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.StockIn=" + Convert.ToInt32(cmbGodownName.SelectedValue) + ") and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and  m.FromCompId=" + CommonMethod.CompId + " and TransferFrom=Cast('G'+Cast('" + Convert.ToInt32(cmbGodownName.SelectedValue) + "' as varchar(10)) as varchar(15)))) union SELECT d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.StockIn=" + Convert.ToInt32(cmbGodownName.SelectedValue) + " and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=pm.CompId and pm.StockIn=" + Convert.ToInt32(cmbGodownName.SelectedValue) + ") and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.StockIn=" + Convert.ToInt32(cmbGodownName.SelectedValue) + ") and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and  m.FromCompId=" + CommonMethod.CompId + " and TransferFrom=Cast('G'+Cast('" + Convert.ToInt32(cmbGodownName.SelectedValue) + "' as varchar(10)) as varchar(15)))", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    int intflag = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dtgvcode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != dt.Rows[i][0].ToString())
                            intflag = 0;
                        else
                        {
                            intflag = 1;
                            goto a;
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

        private void dtgvcode_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                if (dtgvcode.CurrentCell.ColumnIndex == 0)
                {
                    string Proname = txtpname.Text.Trim().Split(',')[0];
                    string company = txtpname.Text.Trim().Split(',')[1];
                    string unit = txtpname.Text.Trim().Split(',')[2];
                    string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                    SqlDataReader dreader;
                    SqlConnection conn = new SqlConnection(cnString1);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    AutoCompleteStringCollection acBusIDSorce = new AutoCompleteStringCollection();
                    if (Convert.ToInt32(cmbGodownName.SelectedValue) == 0)
                        cmd.CommandText = "(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + Proname + "' AND gd.CompanyName='" + company + "' AND gd.Unit='" + unit + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in( Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))) union (Select  d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where ProductName='" + Proname + "' and MfgCompany='" + company + "' and Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15))  order by Id DESC ) and m.Id>(Select top 1 m.Id from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) order by Id DESC ) )  union SELECT d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null) and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and m.CompId=pm.CompId and (pm.StockIn=0 or pm.StockIn is null)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + Proname + "' AND gd.CompanyName='" + company + "' AND gd.Unit='" + unit + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))";
                    else
                        cmd.CommandText = "(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and TransferTo=Cast('G'+Cast('" + Convert.ToInt32(cmbGodownName.SelectedValue) + "' as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.StockIn=" + Convert.ToInt32(cmbGodownName.SelectedValue) + ") and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and  m.FromCompId=" + CommonMethod.CompId + " and TransferFrom=Cast('G'+Cast('" + Convert.ToInt32(cmbGodownName.SelectedValue) + "' as varchar(10)) as varchar(15)))) union SELECT d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.StockIn=" + Convert.ToInt32(cmbGodownName.SelectedValue) + " and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=pm.CompId and pm.StockIn=" + Convert.ToInt32(cmbGodownName.SelectedValue) + ") and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.StockIn=" + Convert.ToInt32(cmbGodownName.SelectedValue) + ") and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and  m.FromCompId=" + CommonMethod.CompId + " and TransferFrom=Cast('G'+Cast('" + Convert.ToInt32(cmbGodownName.SelectedValue) + "' as varchar(10)) as varchar(15)))";
                    conn.Open();
                    dreader = cmd.ExecuteReader();
                    if (dreader.HasRows == true)
                    {
                        while (dreader.Read())
                            acBusIDSorce.Add(dreader["Code"].ToString());
                    }
                    else
                    { MessageBox.Show("Data not Found"); }
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

        private void dtgvcode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (codeflag == 0)
                {
                    if (dtgvcode.Visible == true)
                    {
                        dtgvcode.Visible = false;
                        this.ActiveControl = btnAdd;
                    }
                }
                else
                    this.ActiveControl = dtgvcode;
            }
            catch (Exception)
            { }
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            try
            {
                if (colindex == -1)
                {
                    if (ProductCategory == "Motor")
                        goto t;
                    else
                        goto s;
                }
                else
                {
                    if (ProductCategory == "Motor")
                    {
                        if (Convert.ToDecimal(txtQuantity.Text) != 1)
                        {
                            MessageBox.Show("Quantity must be 1 for update.", "Warning");
                            txtQuantity.Text = "1";
                        }
                        goto t;
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
                        if (colindex != -1)
                            dtgvcode.Rows[i].Cells[0].Value = GvProductInfo.Rows[colindex].Cells["Code"].Value.ToString();
                        else
                            dtgvcode.Rows[i].Cells[0].Value = "";
                    }
                    dtgvcode.Visible = true;
                    this.ActiveControl = dtgvcode;
                    dtgvcode.Height = dtgvcode.ColumnHeadersHeight + dtgvcode.Rows[0].Height * (dtgvcode.RowCount + 1);
                }
            s:
                {
                    decimal  qty = 0;
                    string Proname = "", company = "", unit = "", batchnos = cmbBatchNo.Text;
                    deadstockinfo = this;
                    if (txtQuantity.Text != "0" && txtQuantity.Text != "" && txtpname.Text != "")
                    {
                        qty = Convert.ToDecimal(txtQuantity.Text);
                        Proname = txtpname.Text.Trim().Split(',')[0];
                        company = txtpname.Text.Trim().Split(',')[1];
                        unit = txtpname.Text.Trim().Split(',')[2];
                        if (barcodestatus != "No Barcode" && barcodestatus != null && (productentrytype != 1 && productentrytype != 2))
                        {
                            if (barcodestatus == "Static Barcode" && barcode != "" && barcode != null)
                            { }
                            else
                            {
                                if (colindex != -1)
                                {
                                    //if (savedQuantity >= Convert.ToInt32(txtQuantity.Text))
                                    //{
                                    //    frmBarcodeChoice Barcode = new frmBarcodeChoice(Proname, company, unit, "frmBill", barcodestatus, barcode);
                                    //    Barcode.StartPosition = FormStartPosition.CenterParent;
                                    //    Barcode.ShowDialog();
                                    //}
                                    //else
                                    //{
                                        frmBarcodeChoice Barcode = new frmBarcodeChoice(Proname, company, unit, "frmDead", barcodestatus, barcode, (Convert.ToInt32(txtQuantity.Text) - savedQuantity));
                                        Barcode.StartPosition = FormStartPosition.CenterParent;
                                        Barcode.ShowDialog();
                                    //}
                                }
                                else
                                {
                                    frmBarcodeChoice Barcode = new frmBarcodeChoice(Proname, company, unit, "frmDead", barcodestatus, "", Convert.ToDecimal(txtQuantity.Text));
                                    Barcode.StartPosition = FormStartPosition.CenterParent;
                                    Barcode.ShowDialog();
                                }
                            }
                        }
                        else if (barcodestatus == "No Barcode" && productentrytype == 3)
                        {
                            frmBarcodeChoice Barcode = new frmBarcodeChoice(Proname, company, unit, "frmDead", barcodestatus, "", Convert.ToDecimal(txtQuantity.Text));
                            Barcode.StartPosition = FormStartPosition.CenterParent;
                            Barcode.ShowDialog();
                        }
                        //else if (barcodestatus != "No Barcode" && barcodestatus != null && (productentrytype == 1 || productentrytype == 2))
                        //else if (productentrytype == 1 || productentrytype == 2)  //Withbatchandexpiry and only batch
                        //{
                        //    if (qdid != 0)
                        //    {
                        //        if (txtBatchNo.Text != null && txtBatchNo.Text != "")
                        //        {
                        //            if (txtBatchNo.Text.Contains(','))
                        //            {
                        //                string[] btchArray = txtBatchNo.Text.Split(',');
                        //                for (int h = 0; h < btchArray.Length; h++)
                        //                {
                        //                    BatchRepository batchrepo = new BatchRepository();
                        //                    PurchaseDetailRepository purchaserepo = new PurchaseDetailRepository();
                        //                    List<PurchaseDetail> purdt = new List<PurchaseDetail>();
                        //                    decimal ppqty = 0, bbQty = 0;
                        //                    purdt = purchaserepo.GetAll().Where(t => t.PurchaseMaster.CompId == CommonMethod.CompId && t.ProductName == Proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == btchArray[h]).ToList();
                        //                    if (purdt != null)
                        //                    {
                        //                        foreach (var item in purdt)
                        //                        {
                        //                            decimal prqty = 0;
                        //                            try
                        //                            {
                        //                                PReturnDetailRepository PurRDRepo = new PReturnDetailRepository();
                        //                                var PurDData = PurRDRepo.GetAll().Where(t => t.POID == item.Id && t.PurchaseReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                        //                                prqty = Convert.ToDecimal(PurDData.Quantity);
                        //                            }
                        //                            catch (Exception)
                        //                            {
                        //                                prqty = 0;
                        //                            }
                        //                            ppqty = Convert.ToDecimal(item.Quantity) + Convert.ToDecimal(item.FreeQty);
                        //                            var bdata = batchrepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == btchArray[h] && t.BillId == Convert.ToString(qdid) && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        //                            if (bdata != null)
                        //                            {
                        //                                bdata.Status = "0";
                        //                                bdata.BillId = txtbillno.Text;
                        //                                batchrepo.Edit(bdata);
                        //                                batchrepo.Save();
                        //                                bbQty = Convert.ToDecimal(batchrepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == btchArray[h] && t.Status != "0" && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).Sum(t => t.UsedQty));
                        //                                decimal srqty = 0;
                        //                                try
                        //                                {
                        //                                    BatchRepository batchrpstr = new BatchRepository();
                        //                                    List<Batch> BData = new List<Batch>();
                        //                                    BData = batchrpstr.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == btchArray[h] && t.Status == "1" && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).ToList();
                        //                                    if (BData != null)
                        //                                    {
                        //                                        foreach (var item1 in BData)
                        //                                        {
                        //                                            try
                        //                                            {
                        //                                                GoodDetailRepository SRRepo = new GoodDetailRepository();
                        //                                                GoodReturnDetail SRData = new GoodReturnDetail();
                        //                                                SRData = SRRepo.GetAll().Where(t => t.ProductId == item1.ProductName && t.CompanyName == item1.ManufactureCompany && t.Unit == item1.Unit && t.BNID == Convert.ToInt32(item1.BillId) && t.BatchNo == item1.BatchNo && t.GoodReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                        //                                                if (SRData != null)
                        //                                                    srqty = srqty + Convert.ToDecimal(SRData.Quantity);
                        //                                            }
                        //                                            catch (Exception)
                        //                                            { }
                        //                                        }
                        //                                    }
                        //                                }
                        //                                catch (Exception)
                        //                                {
                        //                                    srqty = 0;
                        //                                }
                        //                                if ((ppqty - prqty) == (bbQty - srqty))
                        //                                {
                        //                                    //purachasedetail status change
                        //                                    purchaserepo = new PurchaseDetailRepository();
                        //                                    PurchaseDetail purdata = new PurchaseDetail();
                        //                                    PurchaseMasterRepository pMastRepo = new PurchaseMasterRepository();
                        //                                    PurchaseMaster PMasterData = new PurchaseMaster();
                        //                                    PMasterData = pMastRepo.GetAll().Where(t => t.Id == item.PorderNo && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        //                                    if (PMasterData != null)
                        //                                        purdata = purchaserepo.GetAll().Where(t => t.ProductName == Proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == btchArray[h] && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == PMasterData.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                        //                                    purdata.Status = "Inactive";
                        //                                    purchaserepo.Edit(purdata);
                        //                                    purchaserepo.Save();
                        //                                }
                        //                                else
                        //                                {
                        //                                    purchaserepo = new PurchaseDetailRepository();
                        //                                    PurchaseDetail purdata = new PurchaseDetail();
                        //                                    PurchaseMasterRepository pMastRepo = new PurchaseMasterRepository();
                        //                                    PurchaseMaster PMasterData = new PurchaseMaster();
                        //                                    PMasterData = pMastRepo.GetAll().Where(t => t.Id == item.PorderNo && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        //                                    if (PMasterData != null)
                        //                                        purdata = purchaserepo.GetAll().Where(t => t.ProductName == Proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == btchArray[h] && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == PMasterData.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                        //                                    purdata.Status = "Active";
                        //                                    purchaserepo.Edit(purdata);
                        //                                    purchaserepo.Save();
                        //                                }
                        //                            }
                        //                        }
                        //                    }
                        //                    purchaserepo.Dispose();
                        //                }
                        //            }
                        //            else
                        //            {
                        //                BatchRepository batchrepo = new BatchRepository();
                        //                PurchaseDetailRepository purchaserepo = new PurchaseDetailRepository();
                        //                List<PurchaseDetail> purdt = new List<PurchaseDetail>();
                        //                decimal ppqty = 0, bbQty = 0;
                        //                purdt = purchaserepo.GetAll().Where(t => t.ProductName == Proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == batchnos && t.PurchaseMaster.CompId == CommonMethod.CompId).ToList();
                        //                if (purdt != null)
                        //                {
                        //                    foreach (var item in purdt)
                        //                    {
                        //                        decimal prqty = 0;
                        //                        try
                        //                        {
                        //                            PReturnDetailRepository PurRDRepo = new PReturnDetailRepository();
                        //                            var PurDData = PurRDRepo.GetAll().Where(t => t.POID == item.Id && t.PurchaseReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                        //                            prqty = Convert.ToDecimal(PurDData.Quantity);
                        //                        }
                        //                        catch (Exception)
                        //                        {
                        //                            prqty = 0;
                        //                        }
                        //                        ppqty = Convert.ToDecimal(item.Quantity) + Convert.ToDecimal(item.FreeQty);
                        //                        var bdata = batchrepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == batchnos && t.BillId == Convert.ToString(qdid) && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        //                        if (bdata != null)
                        //                        {
                        //                            bdata.Status = "0";
                        //                            bdata.BillId = txtbillno.Text;
                        //                            batchrepo.Edit(bdata);
                        //                            batchrepo.Save();
                        //                            bbQty = Convert.ToDecimal(batchrepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == batchnos && t.Status != "0" && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).Sum(t => t.UsedQty));
                        //                            decimal srqty = 0;
                        //                            try
                        //                            {
                        //                                BatchRepository batchrpstr = new BatchRepository();
                        //                                List<Batch> BData = new List<Batch>();
                        //                                BData = batchrpstr.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == batchnos && t.Status == "1" && t.PorderNo == item.PorderNo && t.CompId == CommonMethod.CompId).ToList();
                        //                                if (BData != null)
                        //                                {
                        //                                    foreach (var item1 in BData)
                        //                                    {
                        //                                        try
                        //                                        {
                        //                                            GoodDetailRepository SRRepo = new GoodDetailRepository();
                        //                                            GoodReturnDetail SRData = new GoodReturnDetail();
                        //                                            SRData = SRRepo.GetAll().Where(t => t.ProductId == item1.ProductName && t.CompanyName == item1.ManufactureCompany && t.Unit == item1.Unit && t.BNID == Convert.ToInt32(item1.BillId) && t.BatchNo == item1.BatchNo && t.GoodReturnMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                        //                                            if (SRData != null)
                        //                                                srqty = srqty + Convert.ToDecimal(SRData.Quantity);
                        //                                        }
                        //                                        catch (Exception)
                        //                                        { }
                        //                                    }
                        //                                }
                        //                            }
                        //                            catch (Exception)
                        //                            {
                        //                                srqty = 0;
                        //                            }
                        //                            if ((ppqty - prqty) == (bbQty - srqty))
                        //                            {
                        //                                //purachasedetail status change
                        //                                purchaserepo = new PurchaseDetailRepository();
                        //                                PurchaseDetail purdata = new PurchaseDetail();
                        //                                PurchaseMasterRepository pMastRepo = new PurchaseMasterRepository();
                        //                                PurchaseMaster PMasterData = new PurchaseMaster();
                        //                                PMasterData = pMastRepo.GetAll().Where(t => t.Id == item.PorderNo && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        //                                if (PMasterData != null)
                        //                                    purdata = purchaserepo.GetAll().Where(t => t.ProductName == Proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == batchnos && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == PMasterData.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                        //                                purdata.Status = "Inactive";
                        //                                purchaserepo.Edit(purdata);
                        //                                purchaserepo.Save();
                        //                            }
                        //                            else
                        //                            {
                        //                                purchaserepo = new PurchaseDetailRepository();
                        //                                PurchaseDetail purdata = new PurchaseDetail();
                        //                                PurchaseMasterRepository pMastRepo = new PurchaseMasterRepository();
                        //                                PurchaseMaster PMasterData = new PurchaseMaster();
                        //                                PMasterData = pMastRepo.GetAll().Where(t => t.Id == item.PorderNo && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        //                                if (PMasterData != null)
                        //                                    purdata = purchaserepo.GetAll().Where(t => t.ProductName == Proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == batchnos && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == PMasterData.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId).FirstOrDefault();
                        //                                purdata.Status = "Active";
                        //                                purchaserepo.Edit(purdata);
                        //                                purchaserepo.Save();
                        //                            }
                        //                        }
                        //                    }
                        //                }
                        //                purchaserepo.Dispose();
                        //            }
                        //        }
                        //    }
                        //    if ((productentrytype == 1 || productentrytype == 2) && isvalid == true)
                        //    {
                        //        frmBatchData batch = new frmBatchData(Proname, company, unit, qty);
                        //        batch.StartPosition = FormStartPosition.Manual;
                        //        batch.Top = 300;
                        //        batch.Left = 200;
                        //        batch.ShowDialog();
                        //    }
                        //}  //End of withbatchandexpirty and onlybatch
                    }
                    this.ActiveControl = btnAdd;
                }
            }
            catch (Exception)
            { }
        }

        //public void frmBarcodeChoice(string b, decimal rqty, string size, string color = "", decimal barcodePrice = 0, decimal quantityDiff = -1)
        //{
        //    try
        //    {
        //        if (barcodestatus == "No Barcode" && productentrytype == 3)
        //        {
        //            if (!string.IsNullOrEmpty(size))
        //            {
        //                if (rqty >= Convert.ToDecimal(txtQuantity.Text))
        //                {
        //                    psize = size;
        //                    pcolor = color;
        //                }
        //                else
        //                {
        //                    MessageBox.Show("This Size Less Quantity is  Available.", "Warning");
        //                    txtQuantity.Text = Convert.ToString(rqty);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (!string.IsNullOrEmpty(b))
        //            {
        //                if (rqty >= Convert.ToDecimal(txtQuantity.Text))
        //                {
        //                    barcode = b;
        //                    psize = size;
        //                    pcolor = color;                      
        //                    b = "";
        //                }
        //                else if (btnupdate.Enabled == true && colindex != -1 && quantityDiff >= 0)
        //                {
        //                    barcode = b;
        //                    psize = size;
        //                    pcolor = color;                     
        //                    b = "";
        //                }
        //                else
        //                {
        //                    MessageBox.Show("This Barcode Less Quantity is Available.", "Warning");
        //                    txtQuantity.Text = Convert.ToString(rqty);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    { }
        //}

        public void frmBarcodeChoice(string b, decimal rqty, decimal quantityDiff = -1)
        {
            try
            {
                if (!string.IsNullOrEmpty(b))
                {
                    if (rqty >= Convert.ToDecimal(txtQuantity.Text))
                    {
                        barcode = b;
                        b = "";
                    }
                    else if (btnupdate.Enabled == true && colindex != -1 && quantityDiff >= 0)
                    {
                        barcode = b;
                        b = "";
                    }
                    else
                    {
                        MessageBox.Show("This Barcode Less Quantity is Available.", "Warning");
                        txtpname.Text = "";
                        txtQuantity.Text = "0";
                        fillgst();  fillcombo();
                        this.ActiveControl = txtpname;
                    }
                }
            }
            catch (Exception)
            { }
        }

        public void frmBarcodeChoice(string b, decimal rqty, string size, string color = "", decimal barcodePrice = 0, decimal quantityDiff = -1)
        {
            try
            {
                if (barcodestatus == "No Barcode" && productentrytype == 3)
                {
                    if (!string.IsNullOrEmpty(size))
                    {
                        if (rqty >= Convert.ToDecimal(txtQuantity.Text))
                        {
                            psize = size;
                            pcolor = color;
                        }
                        else
                        {
                            MessageBox.Show("This Size Less Quantity is  Available.", "Warning");
                            txtQuantity.Text = Convert.ToString(rqty);
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(b))
                    {
                        if (rqty >= Convert.ToDecimal(txtQuantity.Text))
                        {
                            barcode = b;
                            psize = size;
                            pcolor = color;
                            b = "";
                        }
                        else if (btnupdate.Enabled == true && colindex != -1 && quantityDiff >= 0)
                        {
                            barcode = b;
                            psize = size;
                            pcolor = color;
                            b = "";
                        }
                        else
                        {
                            MessageBox.Show("This Barcode Less Quantity is Available.", "Warning");
                            txtQuantity.Text = Convert.ToString(rqty);
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
                { e.Handled = true; }
            }
            catch (Exception)
            { }
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnAdd;
            }
            catch (Exception)
            { }
        }

        private void dtpdeaddate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbgst;
            }
            catch (Exception)
            { }
        }

        private void cmbgst_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbGodownName;
            }
            catch (Exception)
            { }
        }

        private void cmbGodownName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = label7;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmGodownMaster godown = new frmGodownMaster();
                        godown.ShowDialog();
                        fillGodownName();
                        this.ActiveControl = cmbGodownName;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbcomanyname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtpname;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmManufactureCompanyInfo company = new frmManufactureCompanyInfo();
                        company.ShowDialog();
                        fillcombo();
                        this.ActiveControl = cmbcomanyname;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtpname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtQuantity;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmProductInformation product = new frmProductInformation();
                        product.ShowDialog();
                        autocompletedata();
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
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtQuantity;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmUnitInformation unit = new frmUnitInformation();
                        unit.ShowDialog();
                    }
                }
            }
            catch (Exception)
            { }
        }

        public void batchtableenrty(int i, int deadid)
        {
            //start Batch table entry code
            try
            {
                PurchaseDetailRepository ppRepo = new PurchaseDetailRepository();
                List<PurchaseDetail> purdetail = new List<PurchaseDetail>();
                PurchaseDetail pdetail = new PurchaseDetail();
                Batch batchData = new Batch();
                BatchRepository batchRepo = new BatchRepository();
                List<decimal> nQty = new List<decimal>();
                string proname = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                string company = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                string unit = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                decimal qty = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value);
                string bStr = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                purdetail = ppRepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == bStr).ToList();
                foreach (var item in purdetail)
                {
                    if (qty > 0)
                    {
                        if (item.Status == "Active") //1
                        {
                            BatchRepository btrepo = new BatchRepository();
                            decimal purqty = Convert.ToDecimal(item.Quantity) + Convert.ToDecimal(item.FreeQty);
                            decimal usedQty = Convert.ToDecimal(btrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == bStr && t.Status != "0" && t.PorderNo == item.PorderNo).Sum(t => t.UsedQty));
                            decimal remqty = purqty - usedQty;
                            if (remqty >= qty)
                            {
                                batchData.ProductName = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                batchData.ManufactureCompany = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                batchData.Unit = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                batchData.Quantity = purqty;
                                batchData.BatchNo = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                                batchData.ExpiryDate = Convert.ToDateTime(item.ExpireDate);
                                batchData.DeadId = deadid;
                                batchData.UsedQty = qty;
                                batchData.PorderNo = item.PorderNo;
                                qty = 0;
                                batchData.Status = "1";
                                batchRepo.Add(batchData);
                                batchRepo.Save();
                                goto x;
                            }
                            else if (remqty < qty)
                            {
                                batchData.ProductName = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                batchData.ManufactureCompany = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                batchData.Unit = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                batchData.Quantity = purqty;
                                batchData.BatchNo = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                                batchData.ExpiryDate = Convert.ToDateTime(item.ExpireDate);
                                batchData.DeadId = deadid;
                                batchData.UsedQty = remqty;
                                batchData.PorderNo = item.PorderNo;
                                qty = qty - remqty;
                                batchData.Status = "1";
                                batchRepo.Add(batchData);
                                batchRepo.Save();
                            }
                        x:
                            PurchaseDetail pData = new PurchaseDetail();
                            BatchRepository brepo = new BatchRepository();
                            string bNo = bStr;
                            decimal uQty = Convert.ToDecimal(brepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == bNo && t.Status != "0" && t.PorderNo == item.PorderNo).Sum(t => t.UsedQty));
                            if (purqty == uQty)
                            {
                                ppRepo = new PurchaseDetailRepository();
                                PurchaseMasterRepository pMastRepo = new PurchaseMasterRepository();
                                PurchaseDetail purdetail11 = new PurchaseDetail();
                                PurchaseMaster PMasterData1 = new PurchaseMaster();
                                PMasterData1 = pMastRepo.GetAll().Where(t => t.Id == item.PorderNo).FirstOrDefault();
                                if (PMasterData1 != null)
                                {
                                    if (PMasterData1.TransactionYear == CommonMethod.TransactionYear)
                                        purdetail11 = ppRepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == bNo && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    else
                                        purdetail11 = ppRepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == bNo && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == PMasterData1.TransactionYear).FirstOrDefault();
                                }
                                int id = purdetail11.Id;
                                pData = ppRepo.GetById(id);
                                pData.Status = "Inactive";
                                ppRepo.Edit(pData);
                                ppRepo.Save();
                            }
                            else
                            {
                                ppRepo = new PurchaseDetailRepository();
                                PurchaseMasterRepository pMastRepo = new PurchaseMasterRepository();
                                PurchaseDetail purdetail11 = new PurchaseDetail();
                                PurchaseMaster PMasterData1 = new PurchaseMaster();
                                PMasterData1 = pMastRepo.GetAll().Where(t => t.Id == item.PorderNo).FirstOrDefault();
                                if (PMasterData1 != null)
                                {
                                    if (PMasterData1.TransactionYear == CommonMethod.TransactionYear)
                                        purdetail11 = ppRepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == bNo && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    else
                                        purdetail11 = ppRepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == bNo && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == PMasterData1.TransactionYear).FirstOrDefault();
                                }
                                int id = purdetail11.Id;
                                pData = ppRepo.GetById(id);
                                pData.Status = "Active";
                                ppRepo.Edit(pData);
                                ppRepo.Save();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            { }
            //end of Batch table entry code
        }

        public void updatebatchtable(int i, int deadid)
        {
            //start Batch table entry code
            try
            {
                PurchaseDetailRepository ppRepo = new PurchaseDetailRepository();
                List<PurchaseDetail> purdetail = new List<PurchaseDetail>();
                PurchaseDetail pdetail = new PurchaseDetail();
                Batch batchData = new Batch();
                BatchRepository batchRepo = new BatchRepository();

                batchData = batchRepo.GetAll().Where(t => t.DeadId == deadid).FirstOrDefault();
                List<Batch> batchinfo = new List<Batch>();
                batchinfo = batchRepo.GetAll().Where(t => t.DeadId == deadid).ToList();

                string proname = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                string company = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                string unit = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                string bStr = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                decimal qty = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value);
                int k = 0;

                purdetail = ppRepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == bStr).ToList();
                foreach (var item in purdetail)
                {
                    foreach (var item1 in batchinfo)
                    {
                        if (item.PorderNo == item1.PorderNo)
                        {
                            BatchRepository btrepo = new BatchRepository();
                            decimal purqty = Convert.ToDecimal(item.Quantity) + Convert.ToDecimal(item.FreeQty);
                            decimal usedQty = Convert.ToDecimal(btrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == bStr && t.Status != "0" && t.PorderNo == item.PorderNo).Sum(t => t.UsedQty));
                            decimal remqty = purqty - (usedQty - Convert.ToDecimal(item1.UsedQty));
                            if (remqty >= qty)
                            {
                                item1.ProductName = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                item1.ManufactureCompany = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                item1.Unit = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                item1.Quantity = purqty;
                                item1.BatchNo = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                                item1.ExpiryDate = Convert.ToDateTime(item.ExpireDate);
                                item1.DeadId = deadid;
                                item1.UsedQty = qty;
                                item1.PorderNo = item.PorderNo;
                                qty = 0;
                                if (item1.UsedQty == 0)
                                    item1.Status = "0";
                                else
                                    item1.Status = "1";
                                batchRepo.Edit(item1);
                                batchRepo.Save();
                                goto x;
                            }
                            else if (remqty < qty)
                            {
                                item1.ProductName = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                item1.ManufactureCompany = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                item1.Unit = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                item1.Quantity = purqty;
                                item1.BatchNo = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                                item1.ExpiryDate = Convert.ToDateTime(item.ExpireDate);
                                item1.DeadId = deadid;
                                item1.UsedQty = remqty;
                                item1.PorderNo = item.PorderNo;
                                qty = qty - remqty;
                                if (item1.UsedQty == 0)
                                    item1.Status = "0";
                                else
                                    item1.Status = "1";
                                batchRepo.Edit(item1);
                                batchRepo.Save();
                            }
                        x:
                            PurchaseDetail pData = new PurchaseDetail();
                            BatchRepository brepo = new BatchRepository();
                            string bNo = bStr;
                            decimal uQty = Convert.ToDecimal(brepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == bNo && t.Status != "0" && t.PorderNo == item.PorderNo).Sum(t => t.UsedQty));
                            if (purqty == uQty)
                            {
                                ppRepo = new PurchaseDetailRepository();
                                PurchaseMasterRepository pMastRepo = new PurchaseMasterRepository();
                                PurchaseDetail purdetail11 = new PurchaseDetail();
                                PurchaseMaster PMasterData1 = new PurchaseMaster();
                                PMasterData1 = pMastRepo.GetAll().Where(t => t.Id == item.PorderNo).FirstOrDefault();
                                if (PMasterData1 != null)
                                {
                                    if (PMasterData1.TransactionYear == CommonMethod.TransactionYear)
                                        purdetail11 = ppRepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == bNo && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    else
                                        purdetail11 = ppRepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == bNo && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == PMasterData1.TransactionYear).FirstOrDefault();
                                }
                                int id = purdetail11.Id;
                                pData = ppRepo.GetById(id);
                                pData.Status = "Inactive"; //0
                                ppRepo.Edit(pData);
                                ppRepo.Save();
                                goto t;
                            }
                            else
                            {
                                ppRepo = new PurchaseDetailRepository();
                                PurchaseMasterRepository pMastRepo = new PurchaseMasterRepository();
                                PurchaseDetail purdetail11 = new PurchaseDetail();
                                PurchaseMaster PMasterData1 = new PurchaseMaster();
                                PMasterData1 = pMastRepo.GetAll().Where(t => t.Id == item.PorderNo).FirstOrDefault();
                                if (PMasterData1 != null)
                                {
                                    if (PMasterData1.TransactionYear == CommonMethod.TransactionYear)
                                        purdetail11 = ppRepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == bNo && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    else
                                        purdetail11 = ppRepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == bNo && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == PMasterData1.TransactionYear).FirstOrDefault();
                                }
                                int id = purdetail11.Id;
                                pData = ppRepo.GetById(id);
                                pData.Status = "Active"; //1
                                ppRepo.Edit(pData);
                                ppRepo.Save();
                                goto t;
                            }
                        }
                        else
                        {
                            if (qty != 0)
                            {
                                if (item.Status == "Active")
                                {
                                    Batch batchData1 = new Batch();
                                    BatchRepository btrepo = new BatchRepository();
                                    decimal purqty = Convert.ToDecimal(item.Quantity) + Convert.ToDecimal(item.FreeQty);
                                    decimal usedQty = Convert.ToDecimal(btrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == bStr && t.Status != "0" && t.PorderNo == item.PorderNo).Sum(t => t.UsedQty));
                                    decimal remqty = purqty - usedQty;
                                    if (remqty >= qty)
                                    {
                                        batchData1.ProductName = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                        batchData1.ManufactureCompany = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                        batchData1.Unit = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                        batchData1.Quantity = purqty;
                                        batchData1.BatchNo = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                                        batchData1.ExpiryDate = Convert.ToDateTime(item.ExpireDate);
                                        batchData1.DeadId = deadid;
                                        batchData1.UsedQty = qty;
                                        batchData1.PorderNo = item.PorderNo;
                                        qty = 0;
                                        batchData1.Status = "1";
                                        batchRepo.Add(batchData1);
                                        batchRepo.Save();
                                        goto x;
                                    }
                                    else if (remqty < qty)
                                    {
                                        batchData1.ProductName = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                        batchData1.ManufactureCompany = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                        batchData1.Unit = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                        batchData1.Quantity = purqty;
                                        batchData1.BatchNo = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                                        batchData1.ExpiryDate = Convert.ToDateTime(item.ExpireDate);
                                        batchData1.DeadId = deadid;
                                        batchData1.UsedQty = remqty;
                                        batchData1.PorderNo = item.PorderNo;
                                        qty = qty - remqty;
                                        batchData1.Status = "1";
                                        batchRepo.Add(batchData1);
                                        batchRepo.Save();
                                    }
                                x:
                                    PurchaseDetail pData = new PurchaseDetail();
                                    BatchRepository brepo = new BatchRepository();
                                    string bNo = bStr;
                                    decimal uQty = Convert.ToDecimal(brepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Unit == unit && t.BatchNo == bNo && t.Status != "0" && t.PorderNo == item.PorderNo).Sum(t => t.UsedQty));
                                    if (purqty == uQty)
                                    {
                                        ppRepo = new PurchaseDetailRepository();
                                        PurchaseMasterRepository pMastRepo = new PurchaseMasterRepository();
                                        PurchaseDetail purdetail11 = new PurchaseDetail();
                                        PurchaseMaster PMasterData1 = new PurchaseMaster();
                                        PMasterData1 = pMastRepo.GetAll().Where(t => t.Id == item.PorderNo).FirstOrDefault();
                                        if (PMasterData1 != null)
                                        {
                                            if (PMasterData1.TransactionYear == CommonMethod.TransactionYear)
                                                purdetail11 = ppRepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == bNo && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                            else
                                                purdetail11 = ppRepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == bNo && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == PMasterData1.TransactionYear).FirstOrDefault();
                                        }
                                        int id = purdetail11.Id;
                                        pData = ppRepo.GetById(id);
                                        pData.Status = "Inactive";//0;
                                        ppRepo.Edit(pData);
                                        ppRepo.Save();
                                        goto t;
                                    }
                                    else
                                    {
                                        ppRepo = new PurchaseDetailRepository();
                                        PurchaseMasterRepository pMastRepo = new PurchaseMasterRepository();
                                        PurchaseDetail purdetail11 = new PurchaseDetail();
                                        PurchaseMaster PMasterData1 = new PurchaseMaster();
                                        PMasterData1 = pMastRepo.GetAll().Where(t => t.Id == item.PorderNo).FirstOrDefault();
                                        if (PMasterData1 != null)
                                        {
                                            if (PMasterData1.TransactionYear == CommonMethod.TransactionYear)
                                                purdetail11 = ppRepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == bNo && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                            else
                                                purdetail11 = ppRepo.GetAll().Where(t => t.ProductName == proname && t.CompanyName == company && t.Unit == unit && t.BatchNo == bNo && t.PorderNo == item.PorderNo && t.PurchaseMaster.TransactionYear == PMasterData1.TransactionYear).FirstOrDefault();
                                        }
                                        int id = purdetail11.Id;
                                        pData = ppRepo.GetById(id);
                                        pData.Status = "Active";
                                        ppRepo.Edit(pData);
                                        ppRepo.Save();
                                        goto t;
                                    }
                                }
                            }
                        }
                        k++;
                    }
                t:
                    { }
                }
            }
            catch (Exception)
            { }
            //end of Batch table entry code
        }

        private void getProductType(string proname, string company, string unit)
        {
            ProductRepository productrepo = new ProductRepository();
            var productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
            if (productdata != null)
            {
                productentrytype = Convert.ToInt32(productdata.ProductType);  // allinone standard
                if (productdata.ProductType == 0 && productdata.WithBatchAndExpiry == "N" && productdata.WithBatch == "N")
                    productentrytype = 0;
                else if (productdata.ProductType == 0 && productdata.WithBatchAndExpiry == "Y") //krushi with batch and expiry
                {
                    productentrytype = 1;
                    GvProductInfo.Columns[7].Visible = true;
                }
                else if (productdata.ProductType == 0 && productdata.WithBatch == "Y") //krushi only batch
                {
                    productentrytype = 2;
                    GvProductInfo.Columns[7].Visible = true;
                }
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
            productrepo.Dispose();
        }

        private void btnAdd_Leave(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = btnSave;
            }
            catch (Exception) { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                Rptdeadstock deadrep = new Rptdeadstock();
                deadrep.ShowDialog();
                deadrep.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btndeadstocklist_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                deadstockinfo = this;
                frmDeadStock p = new frmDeadStock();
                p.Visible = false;
                frmDeadStockList deadstocklist = new frmDeadStockList();
                deadstocklist.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void GvProductInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GvProductInfo.Columns[e.ColumnIndex].HeaderText == "Remove")
                {
                    if (GvProductInfo.Rows.Count != 1 && btnSave.Enabled == false)
                    {
                        if ((MessageBox.Show("Are you sure to Remove this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        {
                            deleteid.Add(Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value));
                            GvProductInfo.Rows.Remove(GvProductInfo.Rows[e.RowIndex]);
                        }
                    }
                    else if (GvProductInfo.Rows.Count != 1 && btnSave.Enabled == true)
                    {
                        if ((MessageBox.Show("Are you sure to Remove this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                            GvProductInfo.Rows.Remove(GvProductInfo.Rows[e.RowIndex]);
                    }
                    else
                    {
                        MessageBox.Show("You can't remove single product from this Dead List.", "Warning");
                        this.ActiveControl = btnupdate;
                    }
                }
                else
                {
                    txtpname.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) + "," + GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value + "," + GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value;
                    cmbUnit.Items.Clear();
                    cmbUnit.DisplayMember = "Text";
                    cmbUnit.ValueMember = "Value";
                    cmbUnit.Items.Add(new { Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value), Value = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value) });
                    if (Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ApplyUnit"].Value) != Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value))
                        cmbUnit.Items.Add(new { Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ApplyUnit"].Value), Value = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ApplyUnit"].Value) });
                    cmbUnit.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ApplyUnit"].Value);
                    txtQuantity.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Quantity"].Value);
                    cmbBatchNo.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["BatchNo"].Value);
                }
                colindex = e.RowIndex;
            }
            catch (Exception)
            { }
        }
    }
}