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
    public partial class frmRepacking : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        List<int> deleteid = new List<int>();
        public static frmRepacking repackinfo;
        public string barcodestatus, updateBarcode;
        public int PurchaeId, colIndex = -1, pno, purmasterid, productentrytype = 0;
        public decimal totalqty = 0;

        public frmRepacking()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxRepackid();
            this.ActiveControl = dtpRepack;
        }

        public frmRepacking(int RepackId)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxRepackid();
            LoadData(RepackId);
        }

        public void maxRepackid()
        {
            try
            {
                RepackMasterRepository rmasterrepo = new RepackMasterRepository();
                var mid = rmasterrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.RepackNo);
                if (mid == null)
                    txtRepackNo.Text = "1";
                else
                    txtRepackNo.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                rmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void LoadData(int RepackId)
        {
            try
            {
                RepackMasterRepository Rmasterrepo = new RepackMasterRepository();
                var RepackData = Rmasterrepo.GetAll().Where(t => t.ID == RepackId && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                txtRepackNo.Text = Convert.ToString(RepackData.RepackNo);
                dtpRepack.Text = Convert.ToString(RepackData.RepackDate.Value);
                PurchaeId = RepackData.ID;
                pno = Convert.ToInt32(txtRepackNo.Text);
                RepackDetailRepository Rdetailrepo = new RepackDetailRepository();
                List<RepackDetail> RepackDetailData = new List<RepackDetail>();
                RepackDetailData = Rdetailrepo.GetAll().Where(t => t.RepackNo == PurchaeId && t.RepackMaster.TransactionYear == CommonMethod.TransactionYear && t.RepackMaster.CompId == CommonMethod.CompId).ToList();
                int i = 0;
                foreach (var item in RepackDetailData)
                {
                    try
                    {
                        GvProductInfo.Rows.Add();
                        GvProductInfo.Rows[i].Cells["Id"].Value = item.ID;
                        ProductRepository productrepo = new ProductRepository();
                        ProductInformation productdata = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            productdata = productrepo.GetAll().Where(t => t.ID == item.ProductNo).FirstOrDefault();
                        else
                            productdata = productrepo.GetAll().Where(t => t.ID == item.ProductNo && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (productdata != null)
                        {
                            GvProductInfo.Rows[i].Cells["ProductNameg"].Value = productdata.ProductName;
                            GvProductInfo.Rows[i].Cells["Company"].Value = productdata.ManufactureCompany;
                            GvProductInfo.Rows[i].Cells["Unit"].Value = productdata.Uniti;
                        }
                        GvProductInfo.Rows[i].Cells["Quantity"].Value = Convert.ToDouble(item.Qty);
                        GvProductInfo.Rows[i].Cells["SaleRate"].Value = item.SaleRate;
                        GvProductInfo.Rows[i].Cells["MRP"].Value = item.MRP;
                        if (item.ExpireDate != null)
                            GvProductInfo.Rows[i].Cells["ExpiryDate"].Value = item.ExpireDate;
                        if (item.Barcode != null)
                            GvProductInfo.Rows[i].Cells["Barcode"].Value = item.Barcode;
                        else
                            GvProductInfo.Rows[i].Cells["Barcode"].Value = "";
                        if (item.Conversion != null)
                            GvProductInfo.Rows[i].Cells["Conversion"].Value = item.Conversion;
                        GvProductInfo.Rows[i].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                        i++;
                    }
                    catch (Exception)
                    { }
                }
                btnSave.Enabled = false;
                btnupdate.Enabled = true;
                if (btnupdate.Enabled == true)
                {
                    SettingRepository settRepo = new SettingRepository();
                    Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (sdata.DeleteSaleBill == "True")
                        rbtndelete.Enabled = true;
                    else
                        rbtndelete.Enabled = false;
                }
                Rmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void frmRepacking_Load(object sender, EventArgs e)
        {
            try
            {
                txtpname.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = null;
                    if (CommonMethod.commProduct == true)
                        cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation Where RepackStatus='Repackage'", con);
                    else
                        cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation Where RepackStatus='Repackage' and compid=" + CommonMethod.CompId, con);
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

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtQuantity.Text))
                {
                    txtQuantity.Text = "0";
                    this.ActiveControl = txtQuantity;
                }
            }
            catch (Exception)
            { }
        }

        private void txtSaleRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSaleRate.Text))
                {
                    txtSaleRate.Text = "0";
                    this.ActiveControl = txtSaleRate;
                }
            }
            catch (Exception)
            { }
        }

        private void txtMRP_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMRP.Text))
                {
                    txtMRP.Text = "0";
                    this.ActiveControl = txtMRP;
                }
            }
            catch (Exception)
            { }
        }

        private void txtpname_Leave(object sender, EventArgs e)
        {
            try
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
                            productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.RepackStatus == "Repackage").FirstOrDefault();
                        else
                            productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.RepackStatus == "Repackage" && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        if (productdata == null)
                        {
                            MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                            txtpname.Text = "";
                            this.ActiveControl = txtpname;
                        }
                        else
                        {
                            barcodestatus = productdata.BarcodeStatus;
                            if (!string.IsNullOrEmpty(Convert.ToString(productdata.RepackConversion)))
                                txtConversion.Text = Convert.ToString(productdata.RepackConversion);
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
                            txtMRP.Text = Convert.ToString(productdata.Rate);
                        }
                        productrepo.Dispose();
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                this.ActiveControl = dtpRepack;
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                maxRepackid();
                txtpname.Text = "";
                txtQuantity.Text = "0";
                txtMRP.Text = "0";
                colIndex = -1;
                GvProductInfo.Rows.Clear();
                btnSave.Enabled = true;
                btnupdate.Enabled = false;
                txtConversion.Text = string.Empty;
                try
                {
                    dtpRepack.Value = DateTime.Now.Date;
                    dtpExpiry.Value = DateTime.Now.Date;
                }
                catch (Exception)
                { }
                txtSaleRate.Text = "0";
                updateBarcode = string.Empty;
                barcodestatus = string.Empty;
            }
            catch (Exception)
            { }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void btnRepackList_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                repackinfo = this;
                frmRepacking p = new frmRepacking();
                p.Visible = false;
                frmRepackingList allpro = new frmRepackingList();
                allpro.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void btnPrintBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                string input = "", inputcopy = "";
                int rownum = 0, copyno = 1, colno = 0;
                input = Microsoft.VisualBasic.Interaction.InputBox("Enter row number from where you want to print " + Environment.NewLine + " from 1 to 12 ", "Page Setup", "" + rownum + "", 300, 250);
                rownum = Convert.ToInt32(input);
                input = Microsoft.VisualBasic.Interaction.InputBox("Enter column number from where you want to print " + Environment.NewLine + " from 1 to 4 ", "Page Setup", "" + colno + "", 300, 250);
                colno = Convert.ToInt32(input);
                inputcopy = Microsoft.VisualBasic.Interaction.InputBox("Enter number of copies you want to print", "Copies", "" + copyno + "", 300, 250);
                copyno = Convert.ToInt32(inputcopy);
                int lblavail = 0, totqty = 0, qtytoprint = 0;
                if (colno > 1)
                {
                    lblavail = 4 - (colno - 1);
                    totqty = Convert.ToInt32(totalqty);
                    qtytoprint = (totqty * copyno);
                }
                if (qtytoprint > lblavail)
                {
                    MessageBox.Show("You have selected more labels to print than available labels,\n Please select another order.", "Printing Barcode");
                    this.ActiveControl = btnPrintBarcode;
                }
                else
                {
                    decimal ponumber = 0;
                    if (purmasterid != 0)
                        ponumber = Convert.ToDecimal(purmasterid);
                    else if (PurchaeId != 0)
                        ponumber = Convert.ToDecimal(PurchaeId);
                    RptBarcode barrpt = new RptBarcode(ponumber, rownum, copyno, colno, "Repack");
                    barrpt.ShowDialog();
                }
            }
            catch (Exception)
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveRepack();
            }
            catch (Exception)
            { }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateRepack();
            }
            catch (Exception)
            { }
        }

        public void SaveRepack()
        {
            try
            {
                maxRepackid();
                RepackMasterRepository Rmasterrepo = new RepackMasterRepository();
                var BillId = Rmasterrepo.GetAll().Where(t => t.RepackNo == Convert.ToInt32(txtRepackNo.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (BillId != null)
                {
                    MessageBox.Show("Already Saved This Record.", "Warning");
                    this.ActiveControl = btnNew;
                }
                else
                {
                    if (GvProductInfo.Rows.Count != 0)
                    {
                        //CustomerMaster table entry code
                        savepomaster();
                        for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                        {
                            ProductRepository productrepo = new ProductRepository();
                            ProductInformation productdata = new ProductInformation();
                            if (CommonMethod.commProduct == true)
                                productdata = productrepo.GetAll().Where(t => t.ProductName == Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value) && t.ManufactureCompany == Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value) && t.Uniti == Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value)).FirstOrDefault();
                            else
                                productdata = productrepo.GetAll().Where(t => t.ProductName == Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value) && t.ManufactureCompany == Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value) && t.Uniti == Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();

                            decimal Repackconversion;
                            Repackconversion = Convert.ToDecimal(productdata.RepackConversion);

                            ProductRepository productrepo1 = new ProductRepository();
                            ProductInformation productdata1 = new ProductInformation();
                            if (CommonMethod.commProduct == true)
                                productdata1 = productrepo1.GetAll().Where(t => t.ID == productdata.BulkItemId).FirstOrDefault();
                            else
                                productdata1 = productrepo1.GetAll().Where(t => t.ID == productdata.BulkItemId && t.CompId == CommonMethod.CompId).FirstOrDefault();
                            productrepo1.Dispose();
                            decimal Bulkconversion;
                            Bulkconversion = Convert.ToDecimal(productdata1.RepackConversion);

                            decimal Bulkqty = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value) * Repackconversion / Bulkconversion;
                            try
                            {
                                db.connect();
                                SqlCommand command = new SqlCommand("SPRepackSaveUpdate", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'R';
                                command.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                command.Parameters.Add("@event", SqlDbType.Int).Value = 1;
                                command.Parameters.Add("@BillNo", SqlDbType.Int).Value = purmasterid;
                                command.Parameters.Add("@ProductId", SqlDbType.Int).Value = productdata.ID;
                                command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value);
                                command.Parameters.Add("@MRP", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["MRP"].Value);
                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(0);
                                command.Parameters.Add("@SaleRate", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SaleRate"].Value);
                                command.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Barcode"].Value);
                                command.Parameters.Add("@Conversion", SqlDbType.Decimal).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Conversion"].Value);
                                if (Convert.ToDateTime(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value) == null)
                                    command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = null;
                                else
                                    command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = Convert.ToDateTime(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value);
                                command.Parameters.Add("@BulkItemId", SqlDbType.Int).Value = productdata.BulkItemId;
                                command.Parameters.Add("@BulkQuantity", SqlDbType.Decimal).Value = Bulkqty;
                                command.ExecuteNonQuery();
                                db.CloseConnection();
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
                        }
                        GvProductInfo.Rows.Clear();
                        MessageBox.Show("Repacking Data saved.", "Success");
                        clear();
                    }
                    else
                    {
                        if (GvProductInfo.Rows.Count == 0)
                        {
                            MessageBox.Show("Please Add Record first.", "Warning");
                            this.ActiveControl = btnAdd;
                        }
                    }
                }
                Rmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void savepomaster()
        {
            try
            {
                RepackMasterRepository Rmasterrepo = new RepackMasterRepository();
                RepackMaster repackdata = new RepackMaster();
                maxRepackid();
                repackdata.RepackNo = Convert.ToInt32(txtRepackNo.Text);
                repackdata.RepackDate = dtpRepack.Value;
                repackdata.TransactionYear = CommonMethod.TransactionYear;
                repackdata.CompId = CommonMethod.CompId;
                Rmasterrepo.Add(repackdata);
                Rmasterrepo.Save();
                purmasterid = Convert.ToInt32(repackdata.ID);
                Rmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void UpdateRepack()
        {
            try
            {
                if (GvProductInfo.Rows.Count != 0)
                {
                    updatemaster();
                    if (deleteid.Count != 0)
                    {
                        for (int x = 0; x < deleteid.Count; x++)
                        {
                            try
                            {
                                db.connect();
                                SqlCommand command = new SqlCommand("SPRepackRemoveDetailWithStockUpdate", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'R';
                                command.Parameters.Add("@id", SqlDbType.Int).Value = deleteid[x];
                                command.Parameters.Add("@billtype", SqlDbType.Int).Value = 0;
                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(0);
                                command.ExecuteNonQuery();
                                db.CloseConnection();
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
                        }
                    }

                    for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                    {
                        int id = Convert.ToInt32(GvProductInfo.Rows[i].Cells["Id"].Value);

                        ProductRepository productrepo = new ProductRepository();
                        ProductInformation productdata = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            productdata = productrepo.GetAll().Where(t => t.ProductName == Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value) && t.ManufactureCompany == Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value) && t.Uniti == Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value)).FirstOrDefault();
                        else
                            productdata = productrepo.GetAll().Where(t => t.ProductName == Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value) && t.ManufactureCompany == Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value) && t.Uniti == Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        decimal Repackconversion;
                        Repackconversion = Convert.ToDecimal(productdata.RepackConversion);
                        ProductRepository productrepo1 = new ProductRepository();
                        ProductInformation productdata1 = new ProductInformation();
                        if (CommonMethod.commProduct == true)
                            productdata1 = productrepo1.GetAll().Where(t => t.ID == productdata.BulkItemId).FirstOrDefault();
                        else
                            productdata1 = productrepo1.GetAll().Where(t => t.ID == productdata.BulkItemId && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        productrepo1.Dispose();
                        decimal Bulkconversion;
                        Bulkconversion = Convert.ToDecimal(productdata1.RepackConversion);
                        decimal Bulkqty = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value) * Repackconversion / Bulkconversion;
                        if (id != 0)
                        {
                            try
                            {
                                db.connect();
                                SqlCommand command = new SqlCommand("SPRepackUpdateStockIfNameReplace", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'R';
                                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                                command.Parameters.Add("@RProductId", SqlDbType.Int).Value = productdata.ID;
                                command.Parameters.Add("@BProductId", SqlDbType.Int).Value = productdata.BulkItemId;
                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(0);
                                command.ExecuteNonQuery();
                                db.CloseConnection();
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
                        }
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPRepackSaveUpdate", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;
                            if (id != 0)
                            {
                                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                                command.Parameters.Add("@event", SqlDbType.Int).Value = 2;
                            }
                            else
                            {
                                command.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                command.Parameters.Add("@event", SqlDbType.Int).Value = 1;
                            }
                            command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'R';
                            command.Parameters.Add("@BillNo", SqlDbType.Int).Value = PurchaeId;
                            command.Parameters.Add("@ProductId", SqlDbType.Int).Value = productdata.ID;
                            command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                            command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                            command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                            command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value);
                            command.Parameters.Add("@MRP", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["MRP"].Value);
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(0);
                            command.Parameters.Add("@SaleRate", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["SaleRate"].Value);
                            command.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Barcode"].Value);
                            command.Parameters.Add("@Conversion", SqlDbType.Decimal).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Conversion"].Value);
                            if (Convert.ToDateTime(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value) == null)
                                command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = null;
                            else
                                command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = Convert.ToDateTime(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value);
                            command.Parameters.Add("@BulkItemId", SqlDbType.Int).Value = productdata.BulkItemId;
                            command.Parameters.Add("@BulkQuantity", SqlDbType.Decimal).Value = Bulkqty;
                            command.ExecuteNonQuery();
                            db.CloseConnection();
                        }
                        catch (Exception)
                        { db.CloseConnection(); }
                    }
                    clear();
                    GvProductInfo.Rows.Clear();
                    MessageBox.Show("Repacking Data Updated.", "Success");
                    deleteid.Clear();
                    this.ActiveControl = dtpRepack;
                }
                else
                {
                    MessageBox.Show("Please Add Record First.", "Warning");
                    this.ActiveControl = btnAdd;
                }
            }
            catch (Exception)
            { }
        }

        public void updatemaster()
        {
            try
            {
                RepackMasterRepository Rmasterrepo = new RepackMasterRepository();
                RepackMaster Repackdata = new RepackMaster();
                Repackdata.RepackNo = Convert.ToInt32(txtRepackNo.Text);
                Repackdata.RepackDate = dtpRepack.Value;
                Repackdata.TransactionYear = CommonMethod.TransactionYear;
                Repackdata.CompId = CommonMethod.CompId;
                Repackdata.ID = PurchaeId;
                pno = Convert.ToInt32(Repackdata.RepackNo);
                Rmasterrepo.Edit(Repackdata);
                Rmasterrepo.Save();
                Rmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private string generateBarcode()
        {
            string str_barCode = "";
            try
            {
                if (str_barCode == "" && barcodestatus == "Our Barcode")
                {
                    string small_alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                    string numbers = "1234567890";
                    string characters = numbers + small_alphabets;
                    string otp = string.Empty;
                    string otp1 = string.Empty;
                    Random random = new Random();
                    for (int q = 0; q < 3; q++)
                    {
                        otp += Convert.ToString(random.Next(10, 20));
                    }
                    for (int q = 0; q < 3; q++)
                    {
                        otp1 += Convert.ToString(random.Next(34, 56));
                    }
                    str_barCode = otp + otp1;
                    PurchaseDetailRepository pDetailRepo = new PurchaseDetailRepository();
                    var data = pDetailRepo.GetAll().Where(t => t.Barcode == str_barCode).FirstOrDefault();
                    RepackDetailRepository RDetailRepo = new RepackDetailRepository();
                    var Rdata = RDetailRepo.GetAll().Where(t => t.Barcode == str_barCode).FirstOrDefault();
                    pDetailRepo.Dispose();
                    RDetailRepo.Dispose();
                    if (data != null || Rdata != null)
                    {
                        otp = string.Empty;
                        otp1 = string.Empty;
                        str_barCode = string.Empty;
                        MessageBox.Show("This Barcode no are duplicate.", "Warning");
                        for (int q = 0; q < 3; q++)
                        {
                            otp += Convert.ToString(random.Next(24, 67));
                        }
                        for (int q = 0; q < 3; q++)
                        {
                            otp1 += Convert.ToString(random.Next(89, 01));
                        }
                        str_barCode = otp + otp1;
                        PurchaseDetailRepository pDetailReponew = new PurchaseDetailRepository();
                        var purdata = pDetailReponew.GetAll().Where(t => t.Barcode == str_barCode).FirstOrDefault();
                        RepackDetailRepository RDetailReponew = new RepackDetailRepository();
                        var Redata = RDetailReponew.GetAll().Where(t => t.Barcode == str_barCode).FirstOrDefault();
                        RDetailReponew.Dispose();
                        pDetailReponew.Dispose();
                        if (data != null || Redata != null)
                            str_barCode = "";
                    }
                }
                return str_barCode;
            }
            catch (Exception)
            {
                return str_barCode;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bool isValid = true;
            try
            {
                if (txtpname.Text.Trim() != "" && txtQuantity.Text != "0" && txtSaleRate.Text != "" && txtMRP.Text != "")
                {
                    if (colIndex != -1 && isValid == true)
                    {
                        string Proname = txtpname.Text.Trim().Split(',')[0];
                        string company = txtpname.Text.Trim().Split(',')[1];
                        string unit = txtpname.Text.Trim().Split(',')[2];
                        int i = GvProductInfo.RowCount;
                        GvProductInfo.Rows[colIndex].Cells["ProductNameg"].Value = Proname;
                        GvProductInfo.Rows[colIndex].Cells["Company"].Value = company;
                        GvProductInfo.Rows[colIndex].Cells["Unit"].Value = unit;
                        GvProductInfo.Rows[colIndex].Cells["Quantity"].Value = txtQuantity.Text;
                        GvProductInfo.Rows[colIndex].Cells["SaleRate"].Value = Convert.ToDecimal(txtSaleRate.Text);
                        GvProductInfo.Rows[colIndex].Cells["MRP"].Value = Convert.ToDecimal(txtMRP.Text);
                        GvProductInfo.Rows[colIndex].Cells["ExpiryDate"].Value = dtpExpiry.Value;
                        if (barcodestatus == "Our Barcode")
                        {
                            if (updateBarcode == "")
                                GvProductInfo.Rows[colIndex].Cells["Barcode"].Value = generateBarcode();
                            else
                                GvProductInfo.Rows[colIndex].Cells["Barcode"].Value = updateBarcode;
                        }
                        else
                            GvProductInfo.Rows[colIndex].Cells["Barcode"].Value = string.Empty;
                        GvProductInfo.Rows[colIndex].Cells["Conversion"].Value = txtConversion.Text;
                        GvProductInfo.Rows[colIndex].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                        totalqty = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["SaleRate"].Value));

                        colIndex = -1;
                        txtpname.Text = "";
                        txtQuantity.Text = "0";
                        txtMRP.Text = "0";
                        txtSaleRate.Text = "0";
                        dtpExpiry.Value = DateTime.Now.Date;
                        updateBarcode = string.Empty;
                        barcodestatus = string.Empty;
                    }
                    else if (isValid == true)
                    {
                        string Proname = txtpname.Text.Trim().Split(',')[0];
                        string company = txtpname.Text.Trim().Split(',')[1];
                        string unit = txtpname.Text.Trim().Split(',')[2];

                        GvProductInfo.Rows.Add();
                        int i = GvProductInfo.RowCount;
                        GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                        GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                        GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                        GvProductInfo.Rows[i - 1].Cells["Quantity"].Value = txtQuantity.Text;
                        GvProductInfo.Rows[i - 1].Cells["SaleRate"].Value = Convert.ToDecimal(txtSaleRate.Text);
                        GvProductInfo.Rows[i - 1].Cells["MRP"].Value = Convert.ToDecimal(txtMRP.Text);
                        GvProductInfo.Rows[i - 1].Cells["ExpiryDate"].Value = dtpExpiry.Value;
                        GvProductInfo.Rows[i - 1].Cells["Barcode"].Value = generateBarcode();
                        GvProductInfo.Rows[i - 1].Cells["Conversion"].Value = txtConversion.Text;
                        GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                    }

                    totalqty = GvProductInfo.Rows.Cast<DataGridViewRow>().Sum(x => Convert.ToDecimal(x.Cells["SaleRate"].Value));
                    txtpname.Text = "";
                    txtQuantity.Text = "0";
                    txtMRP.Text = "0";
                    txtSaleRate.Text = "0";
                    txtConversion.Text = string.Empty;
                    dtpExpiry.Value = DateTime.Now.Date;
                    updateBarcode = string.Empty;
                    barcodestatus = string.Empty;
                    this.ActiveControl = txtpname;
                }
                else
                {
                    if (txtpname.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter Product Name.", "Warning");
                        this.ActiveControl = txtpname;
                    }
                    else if (txtQuantity.Text == "0")
                    {
                        MessageBox.Show("Enter Quantity.", "Warning");
                        this.ActiveControl = txtQuantity;
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
                decimal textqty = 0;
                string Proname = "", company = "", unit = "";
                if (txtQuantity.Text != "0" && txtpname.Text != "")
                {
                    Proname = txtpname.Text.Trim().Split(',')[0];
                    company = txtpname.Text.Trim().Split(',')[1];
                    unit = txtpname.Text.Trim().Split(',')[2];
                    decimal remq = 0;
                    ProductRepository productrepo = new ProductRepository();
                    ProductInformation productdata = new ProductInformation();
                    if (CommonMethod.commProduct == true)
                        productdata = productrepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                    else
                        productdata = productrepo.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    productrepo.Dispose();
                    decimal Repackconversion;
                    Repackconversion = Convert.ToDecimal(productdata.RepackConversion);
                    ProductRepository productrepo1 = new ProductRepository();
                    ProductInformation productdata1 = new ProductInformation();
                    if (CommonMethod.commProduct == true)
                        productdata1 = productrepo1.GetAll().Where(t => t.ID == productdata.BulkItemId).FirstOrDefault();
                    else
                        productdata1 = productrepo1.GetAll().Where(t => t.ID == productdata.BulkItemId && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    productrepo1.Dispose();
                    decimal Bulkconversion;
                    Bulkconversion = Convert.ToDecimal(productdata1.RepackConversion);
                    textqty = Convert.ToDecimal(txtQuantity.Text) * Repackconversion / Bulkconversion;

                    try
                    {
                        db.connect();
                        SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = productdata1.ProductName;
                        command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = productdata1.ManufactureCompany;
                        command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = productdata1.Uniti;
                        command.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'P';
                        command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'a';
                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                        command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            if (colIndex == -1)
                                remq = Convert.ToDecimal(reader.GetSqlDecimal(11).ToDouble());
                            else if (colIndex != -1)
                                remq = (Convert.ToDecimal(reader.GetSqlDecimal(10).ToDouble()) + textqty);
                            if (textqty <= remq)
                            { }
                            else
                            {
                                if (txtpname.Focused == false)
                                {
                                    MessageBox.Show("Bulk Product less Stock Available.", "Warning");
                                    txtQuantity.Text = "0";
                                    this.ActiveControl = txtQuantity;
                                }
                            }
                        }
                        else
                        {
                            if (txtpname.Focused == false)
                            {
                                MessageBox.Show("Bulk Product less Stock Available.", "Warning");
                                txtQuantity.Text = "0";
                                this.ActiveControl = txtQuantity;
                            }
                        }
                        db.CloseConnection();
                    }
                    catch (Exception)
                    {
                        db.CloseConnection();
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
            else if (keyData == Keys.F3)
            {
                if (btnSave.Enabled == true)
                {
                    SaveRepack();
                    return true;
                }
                else if (btnupdate.Enabled == true)
                {
                    UpdateRepack();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void rbtnaddproduct_Click(object sender, EventArgs e)
        {
            try
            {
                frmProductInformation pinfo = new frmProductInformation();
                pinfo.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void txtpname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmProductInformation product = new frmProductInformation();
                        product.ShowDialog();
                        frmRepacking_Load(null, null);
                    }
                }
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtQuantity;
            }
            catch (Exception) { }
        }

        private void dtpRepack_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtpname;
            }
            catch (Exception) { }
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtSaleRate;
            }
            catch (Exception) { }
        }

        private void txtMRP_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpExpiry;
            }
            catch (Exception) { }
        }

        private void dtpExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtConversion;
            }
            catch (Exception) { }
        }

        private void txtConversion_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnAdd;
            }
            catch (Exception) { }
        }

        private void txtSaleRate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtMRP;
            }
            catch (Exception) { }
        }

        private void rbtndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to Delete this Repack ?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    AIOInventorySystem.Data.Model.Stock sdata = new Data.Model.Stock();
                    string productname = "", unit = "", company = "", BPname = "", BCname = "", BUname = "", Suppliername = "", DeletedBillNo = "";
                    decimal NetAmount = 0, Quantity = 0, newsqty = 0, sqty = 0, pqty = 0, prqty = 0, newrqty = 0, rqty = 0, srqty = 0, dqty = 0, oqty = 0, repackQty = 0;
                    int productId = 0, bulkProductId = 0;
                    RepackMasterRepository repackMRepo = new RepackMasterRepository();
                    RepackMaster repackdata = repackMRepo.GetAll().Where(t => t.RepackNo == pno && t.ID == PurchaeId && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    if (repackdata != null)
                    {
                        // add details for history table
                        try
                        {
                            DeletedBillNo = Convert.ToString(repackdata.RepackNo);
                            Suppliername = null;
                            NetAmount = 0;
                            DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                            DeleteHistoryTable historydata = new DeleteHistoryTable();
                            historydata.DeleteType = "Repack";
                            historydata.BillNo = DeletedBillNo;
                            historydata.SupplierOrCustomerName = Suppliername;
                            historydata.Amount = Convert.ToDecimal(NetAmount);
                            historydata.CurrentDate = DateTime.Now.Date;
                            historydata.BillDate = Convert.ToDateTime(repackdata.RepackDate);
                            historydata.CompId = CommonMethod.CompId;
                            historydata.TransactionYear = CommonMethod.TransactionYear;
                            historyRepo.Add(historydata);
                            historyRepo.Save();
                        }
                        catch (Exception) { }

                        RepackDetailRepository repackDRepo = new RepackDetailRepository();
                        List<RepackDetail> RList = repackDRepo.GetAll().Where(t => t.RepackNo == PurchaeId && t.RepackMaster.CompId == CommonMethod.CompId && t.RepackMaster.TransactionYear == CommonMethod.TransactionYear).ToList();
                        if (RList.Count != 0)
                        {
                            foreach (var item in RList)
                            {
                                decimal rate = 0; int updateStockId = 0;
                                productId = Convert.ToInt32(item.ProductNo);
                                bulkProductId = Convert.ToInt32(item.BulkProductNo);
                                Quantity = Convert.ToDecimal(item.Qty);
                                repackQty = Convert.ToDecimal(repackDRepo.GetAll().Where(t => t.ProductNo == productId && t.RepackNo == PurchaeId && t.RepackMaster.CompId == CommonMethod.CompId && t.RepackMaster.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.Qty));
                                ProductRepository productRepo = new ProductRepository();
                                ProductInformation proddata = new ProductInformation();
                                if (CommonMethod.commProduct == true)
                                    proddata = productRepo.GetAll().Where(t => t.ID == productId).FirstOrDefault();
                                else
                                    proddata = productRepo.GetAll().Where(t => t.ID == productId && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (proddata != null)
                                {
                                    productname = proddata.ProductName;
                                    company = proddata.ManufactureCompany;
                                    unit = proddata.Uniti;
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
                                        newsqty = (pqty) - Quantity;
                                        newrqty = oqty + newsqty - prqty - sqty + srqty - dqty;
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
                                        sdata.PurchaseQty = Convert.ToDecimal(newsqty);
                                        sdata.SaleQty = Convert.ToDecimal(sqty);
                                        sdata.RemQty = Convert.ToDecimal(newrqty);
                                        sdata.SaleReturnQty = stockdata.SaleReturnQty;
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
                                    stockRepo.Dispose();
                                }
                                if (bulkProductId != 0)
                                {
                                    ProductRepository prodRepo = new ProductRepository();
                                    ProductInformation pdata = new ProductInformation();
                                    if (CommonMethod.commProduct == true)
                                        pdata = prodRepo.GetAll().Where(t => t.ID == bulkProductId).FirstOrDefault();
                                    else
                                        pdata = prodRepo.GetAll().Where(t => t.ID == bulkProductId && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                    BPname = pdata.ProductName;
                                    BCname = pdata.ManufactureCompany;
                                    BUname = pdata.Uniti;
                                    Quantity = Convert.ToDecimal(item.BulkQty);
                                    repackQty = Convert.ToDecimal(repackDRepo.GetAll().Where(t => t.BulkProductNo == bulkProductId && t.RepackNo == PurchaeId && t.RepackMaster.CompId == CommonMethod.CompId && t.RepackMaster.TransactionYear == CommonMethod.TransactionYear).Sum(t => t.BulkQty));
                                    StockRepository stockRepo1 = new StockRepository();
                                    AIOInventorySystem.Data.Model.Stock sdata1 = new Data.Model.Stock();
                                    var stockdata1 = stockRepo1.GetAll().Where(t => t.ProductName == Convert.ToString(BPname) && t.MfgCompany == Convert.ToString(BCname) && t.Unit == Convert.ToString(BUname) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                                    if (stockdata1 != null)
                                    {
                                        newsqty = 0; sqty = 0; pqty = 0; prqty = 0; newrqty = 0; rqty = 0; srqty = 0; dqty = 0; oqty = 0;
                                        sqty = Convert.ToDecimal(stockRepo1.GetAll().Where(t => t.ProductName == Convert.ToString(BPname) && t.MfgCompany == Convert.ToString(BCname) && t.Unit == Convert.ToString(BUname) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().SaleQty);
                                        pqty = Convert.ToDecimal(stockRepo1.GetAll().Where(t => t.ProductName == Convert.ToString(BPname) && t.MfgCompany == Convert.ToString(BCname) && t.Unit == Convert.ToString(BUname) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().PurchaseQty);
                                        rqty = Convert.ToDecimal(stockRepo1.GetAll().Where(t => t.ProductName == Convert.ToString(BPname) && t.MfgCompany == Convert.ToString(BCname) && t.Unit == Convert.ToString(BUname) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().RemQty);
                                        oqty = Convert.ToDecimal(stockRepo1.GetAll().Where(t => t.ProductName == Convert.ToString(BPname) && t.MfgCompany == Convert.ToString(BCname) && t.Unit == Convert.ToString(BUname) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().OpeningQty);
                                        dqty = Convert.ToDecimal(stockRepo1.GetAll().Where(t => t.ProductName == Convert.ToString(BPname) && t.MfgCompany == Convert.ToString(BCname) && t.Unit == Convert.ToString(BUname) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().DeadQty);
                                        prqty = Convert.ToDecimal(stockRepo1.GetAll().Where(t => t.ProductName == Convert.ToString(BPname) && t.MfgCompany == Convert.ToString(BCname) && t.Unit == Convert.ToString(BUname) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().PurReturnQty);
                                        srqty = Convert.ToDecimal(stockRepo1.GetAll().Where(t => t.ProductName == Convert.ToString(BPname) && t.MfgCompany == Convert.ToString(BCname) && t.Unit == Convert.ToString(BUname) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault().SaleReturnQty);
                                        newsqty = (sqty) - Quantity;
                                        newrqty = oqty + pqty - prqty - newsqty + srqty - dqty;
                                        updateStockId = stockdata1.Id;
                                    }
                                    sdata1 = stockRepo1.GetById(stockdata1.Id);
                                    if (sdata1 != null)
                                    {
                                        sdata1.ProductId = stockdata1.ProductId;
                                        sdata1.ProductName = BPname;
                                        sdata1.MfgCompany = BCname;
                                        sdata1.Unit = BUname;
                                        sdata1.OpeningQty = stockdata1.OpeningQty;
                                        sdata1.DeadQty = stockdata1.DeadQty;
                                        sdata1.PurchaseQty = Convert.ToDecimal(pqty);
                                        sdata1.SaleQty = Convert.ToDecimal(newsqty);
                                        sdata1.RemQty = Convert.ToDecimal(newrqty);
                                        sdata1.SaleReturnQty = stockdata1.SaleReturnQty;
                                        sdata1.PurReturnQty = stockdata1.PurReturnQty;

                                        try
                                        {
                                            db.connect();
                                            SqlCommand command = new SqlCommand("SPNewAvgPurchaseRate", db.Connection);
                                            command.CommandType = CommandType.StoredProcedure;

                                            command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = BPname;
                                            command.Parameters.Add("@MfgCompany", SqlDbType.VarChar).Value = BCname;
                                            command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = BUname;
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

                                        sdata1.ProductRate = Convert.ToDecimal(rate);
                                        sdata1.CompId = CommonMethod.CompId;
                                        sdata1.TransactionYear = CommonMethod.TransactionYear;
                                        sdata.Id = updateStockId;
                                        stockRepo1.Edit(sdata1);
                                        stockRepo1.Save();
                                    }
                                    stockRepo1.Dispose();
                                }
                            }
                        }
                        db.connect();
                        DataTable dt1 = db.GetTable("delete from RepackDetail where RepackNo='" + PurchaeId + "'");
                        DataTable dt2 = db.GetTable("delete from RepackMaster Where ID='" + PurchaeId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                        MessageBox.Show("Record Delete Successfully.", "Success");
                        dt1.Dispose();
                        dt2.Dispose();
                        db.CloseConnection();
                        GvProductInfo.Rows.Clear();
                        clear();
                        this.ActiveControl = btnNew;
                        rbtndelete.Enabled = false;
                    }
                }
            }
            catch (Exception) { }
        }

        private void GvProductInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GvProductInfo.Columns[e.ColumnIndex].HeaderText == "Remove")
                //if (e.ColumnIndex == 10)
                {
                    if (GvProductInfo.Rows.Count != 1 && btnSave.Enabled == false)
                    {
                        string pname1 = GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value.ToString();
                        string cname1 = GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value.ToString();
                        string uname1 = GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value.ToString();
                        decimal txtqty = 0;
                        txtqty = Convert.ToDecimal(GvProductInfo.Rows[e.RowIndex].Cells["Quantity"].Value);
                        if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        {
                            deleteid.Add(Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value));
                            GvProductInfo.Rows.Remove(GvProductInfo.Rows[e.RowIndex]);
                            colIndex = -1;
                            txtpname.Text = "";
                            txtQuantity.Text = "0";
                            txtMRP.Text = "0";
                            txtSaleRate.Text = "0";
                            dtpExpiry.Value = DateTime.Now.Date;
                        }
                    }
                    else if (btnSave.Enabled == true)
                    {
                        if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        {
                            GvProductInfo.Rows.Remove(GvProductInfo.Rows[e.RowIndex]);
                            colIndex = -1;
                            txtpname.Text = "";
                            txtQuantity.Text = "0";
                            txtMRP.Text = "0";
                            txtSaleRate.Text = "0";
                            dtpExpiry.Value = DateTime.Now.Date;
                        }
                    }
                    else
                        MessageBox.Show("This Invoice has only 1 product." + Environment.NewLine + "You have to delete full Order.", "Warning");
                }
                else
                {
                    colIndex = e.RowIndex;
                    txtpname.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) + "," + GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value + "," + GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value;
                    txtQuantity.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Quantity"].Value);
                    txtSaleRate.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleRate"].Value);
                    txtMRP.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["MRP"].Value);
                    dtpExpiry.Value = Convert.ToDateTime(GvProductInfo.Rows[e.RowIndex].Cells["ExpiryDate"].Value);
                    updateBarcode = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Barcode"].Value);
                    txtConversion.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Conversion"].Value);
                    this.ActiveControl = txtpname;
                }
            }
            catch (Exception)
            { }
        }
    }
}