using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;
using AIOInventorySystem.Desk.Reports;
using Excel = Microsoft.Office.Interop.Excel;
using System.Security.Cryptography;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmAddStock : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        ToolTip tooltipbtn = new ToolTip();
        public string ponumber, ProductCategory;
        public int colIndex = -1, POID, flag = 0, codeflag = 0, productentrytype = 0, totalqty = 0;
        private static bool isaddmandatory = false, refreshFlag = false;
        private string HasMandatoryseries = "Y", barcodestatus = "", updateBarcode, batch = "";
        List<int> deleteid = new List<int>();
        List<int> billList = new List<int>();
        ProductRepository productrepo1 = new ProductRepository();
        ProductInformation productdata = new ProductInformation();

        public frmAddStock()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxpurchaseorderid();
            fillGodownName();
        }

        public frmAddStock(int Stockno)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxpurchaseorderid();
            fillGodownName();
        }

        public void maxpurchaseorderid()
        {
            try
            {
                PurchaseMasterRepository pmasterrepo = new PurchaseMasterRepository();
                var mid = pmasterrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.PorderNo);
                if (mid == null)
                    ponumber = "1";
                else
                    ponumber = Convert.ToString(Convert.ToInt32(mid) + 1);
                pmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillGodownName()
        {
            try
            {
                GodownMasterRepository GDRepo = new GodownMasterRepository();
                List<GodownMaster> GDData = new List<GodownMaster>();
                GDData.Clear();
                if (CommonMethod.commProduct == true)
                    GDData = GDRepo.GetAll().OrderByDescending(t=>t.GodownName).ToList();
                else
                    GDData = GDRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.GodownName).ToList();
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                PurchaseMasterRepository pmasterRepo = new PurchaseMasterRepository();
                PurchaseMaster pmaster = new PurchaseMaster();
                if (btnSave.Text == "Save")
                {
                    if (GvProductInfo.Rows.Count != 0)
                    {
                        try
                        {
                            maxpurchaseorderid();
                            pmaster.PorderNo = Convert.ToInt32(ponumber);
                            pmaster.PorderDate = dtpPorderdate.Value;
                            pmaster.Suppliername = "First stock";
                            pmaster.Vat = Convert.ToDecimal(txtvat.Text);
                            pmaster.TransactionYear = CommonMethod.TransactionYear;
                            pmaster.CompId = CommonMethod.CompId;
                            pmaster.StockIn = Convert.ToInt32(cmbGodownName.SelectedValue);
                            pmasterRepo.Add(pmaster);
                            pmasterRepo.Save();
                            POID = pmaster.Id;
                        }
                        catch (Exception)
                        { }

                        for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                        {
                            try
                            {
                                db.connect();
                                SqlCommand command = new SqlCommand("SPPOnBillSaveUpdate", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'P';
                                command.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                command.Parameters.Add("@event", SqlDbType.Int).Value = 1;
                                command.Parameters.Add("@billtype", SqlDbType.Int).Value = 0;
                                command.Parameters.Add("@BillNo", SqlDbType.Int).Value = POID;
                                command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                command.Parameters.Add("@AlternateUnit", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["PurUnit"].Value);
                                command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value);
                                command.Parameters.Add("@FreeQty", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                                command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                command.Parameters.Add("@Vat", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                                command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                                command.Parameters.Add("@HasMandatoryCodeSeries", SqlDbType.Char).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["MandatoryCodeSeries"].Value);
                                command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                                command.Parameters.Add("@SaleRate", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Salerate"].Value);
                                command.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Barcode"].Value);
                                command.Parameters.Add("@StaticBarcode", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["StaticBarcode"].Value);
                                try
                                {
                                    if (GvProductInfo.Rows[i].Cells["ExpiryDate"].Value == null || GvProductInfo.Rows[i].Cells["ExpiryDate"].Value.ToString() == "")
                                        command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = null;
                                    else
                                        command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = Convert.ToDateTime(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value);
                                }
                                catch (Exception)
                                { command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = null; }
                                command.Parameters.Add("@Size", SqlDbType.VarChar).Value = "";
                                command.ExecuteNonQuery();
                                db.CloseConnection();
                                command.Dispose();
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
                        }
                        MessageBox.Show("Stock saved Successfully.", "Success");
                        clear();
                    }
                    else
                        MessageBox.Show("Add Product First.", "Warning");
                    pmasterRepo.Dispose();
                    this.ActiveControl = txtpname;
                }
                else
                {
                    string ismodified = "N";
                    if (GvProductInfo.RowCount > 0)
                    {
                        for (int i = 0; i < GvProductInfo.RowCount; i++)
                        {
                            if (GvProductInfo.Rows[i].Cells["IsModified"].Value.ToString() != null)
                                ismodified = GvProductInfo.Rows[i].Cells["IsModified"].Value.ToString();
                            else
                                ismodified = "N";

                            if (deleteid.Count != 0)
                            {
                                for (int x = 0; x < deleteid.Count; x++)
                                {
                                    try
                                    {
                                        db.connect();
                                        SqlCommand command = new SqlCommand("SPPOnBillRemoveDetailWithStockUpdate", db.Connection);
                                        command.CommandType = CommandType.StoredProcedure;

                                        command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'P';
                                        command.Parameters.Add("@id", SqlDbType.Int).Value = deleteid[x];
                                        command.Parameters.Add("@billtype", SqlDbType.Int).Value = 0;
                                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                        command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);

                                        command.ExecuteNonQuery();
                                        db.CloseConnection();
                                    }
                                    catch (Exception)
                                    { db.CloseConnection(); }
                                }
                            }
                            if (ismodified == "Y")
                            {
                                try
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPPOnBillUpdateStockIfNameReplace", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;

                                    command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'P';
                                    command.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(GvProductInfo.Rows[i].Cells["Id"].Value.ToString());
                                    command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                    command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                                    command.ExecuteNonQuery();
                                    db.CloseConnection();
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                                try
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPPOnBillSaveUpdate", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;

                                    command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'A';
                                    if (GvProductInfo.Rows[i].Cells["Id"].Value == null)
                                        command.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                    else
                                        command.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(GvProductInfo.Rows[i].Cells["Id"].Value.ToString());
                                    command.Parameters.Add("@event", SqlDbType.Int).Value = 2;
                                    command.Parameters.Add("@billtype", SqlDbType.Int).Value = 0;
                                    command.Parameters.Add("@BillNo", SqlDbType.Int).Value = 0;
                                    command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                    command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                    command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                    command.Parameters.Add("@AlternateUnit", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["PurUnit"].Value);
                                    command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value);
                                    command.Parameters.Add("@FreeQty", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                    command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                                    command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                    command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                    command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                    command.Parameters.Add("@Vat", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                    command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                                    command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                                    command.Parameters.Add("@HasMandatoryCodeSeries", SqlDbType.Char).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["MandatoryCodeSeries"].Value);
                                    command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                                    command.Parameters.Add("@SaleRate", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Salerate"].Value);
                                    command.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Barcode"].Value);
                                    command.Parameters.Add("@StaticBarcode", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["StaticBarcode"].Value);
                                    try
                                    {
                                        if (GvProductInfo.Rows[i].Cells["ExpiryDate"].Value == null || GvProductInfo.Rows[i].Cells["ExpiryDate"].Value.ToString() == "")
                                            command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = null;
                                        else
                                            command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = Convert.ToDateTime(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value);
                                    }
                                    catch (Exception)
                                    { command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = null; }
                                    command.Parameters.Add("@Size", SqlDbType.VarChar).Value = "";
                                    command.ExecuteNonQuery();
                                    db.CloseConnection();
                                    command.Dispose();
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                            }
                        }
                        MessageBox.Show("Stock Updated Successfully.", "Success");
                        deleteid.Clear();
                        clear();
                    }
                }
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            try
            {
                btnSave.Text = "Save";
                txtQuantity.Text = "";
                txtpname.Text = "";
                GvProductInfo.Rows.Clear();
                btnSave.Enabled = true;
                autocompletedata();
                dtgvcode.Rows.Clear();
                cmbUnit.Items.Clear();
                txtvat.Text = "0";
                maxpurchaseorderid();
                isaddmandatory = false;
                txtBatchNo.Text = "";
                dtpExpiry.Value = DateTime.Now.Date;
                txtBatchNo.Visible = false;
                dtpExpiry.Visible = false;
                lblBatch.Visible = false;
                lblExp.Visible = false;
                txtsalerate.Text = "0";
                txtStaticBarcode.Text = "";
                colIndex = -1;
                this.ActiveControl = txtpname;
            }
            catch (Exception)
            { }
        }

        public void autocompletedata()
        {
            try
            {
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
                    cmd.Dispose();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtpname.Text != "" && txtQuantity.Text != "")
                {
                    if (productentrytype == 1) //krushi project with batch and expiry 
                    {
                        if (txtBatchNo.Text == "")
                        {
                            MessageBox.Show("Enter Batch Number.", "Success");
                            this.ActiveControl = txtBatchNo;
                            goto a;
                        }
                        if (dtpExpiry.Value.Date < dtpPorderdate.Value.Date)
                        {
                            MessageBox.Show("Expiry Date must be greater than Todays Date.", "Success");
                            this.ActiveControl = dtpExpiry;
                            goto a;
                        }
                    }
                    else if (productentrytype == 2) //krushi project with batch
                    {
                        if (txtBatchNo.Text == "")
                        {
                            MessageBox.Show("Enter Batch Number.", "Success");
                            this.ActiveControl = txtBatchNo;
                            goto a;
                        }
                    }
                    if (barcodestatus == "Static Barcode" || barcodestatus == "Our Barcode")
                    {
                        if (txtsalerate.Text == "0")
                        {
                            MessageBox.Show("Product must have SaleRate", "Warning");
                            this.ActiveControl = txtsalerate;
                            goto a;
                        }
                        else if (barcodestatus == "Static Barcode" && txtStaticBarcode.Text == "")
                        {
                            MessageBox.Show("Product must have Static Barcode", "Warning");
                            this.ActiveControl = txtStaticBarcode;
                            goto a;
                        }
                    }
                    if (isaddmandatory == true)
                    {
                        for (int i = 0; i < dtgvcode.RowCount; i++)
                        {
                            if (dtgvcode.Rows[i].Cells[0].Value.ToString() == "")
                            {
                                MessageBox.Show("All Quantity must have Serial No or Code", "Warning");
                                dtgvcode.Visible = true;
                                this.ActiveControl = dtgvcode;
                                dtgvcode.CurrentCell = dtgvcode[0, i];
                                dtgvcode.CurrentCell.Selected = true;
                                goto a;
                            }
                        }
                    }
                    string Proname = txtpname.Text.Trim().Split(',')[0];
                    string company = txtpname.Text.Trim().Split(',')[1];
                    string unit = txtpname.Text.Trim().Split(',')[2];
                    PurchaseDetailRepository pdetailrepo = new PurchaseDetailRepository();
                    if (colIndex != -1)
                    {
                        if (barcodestatus == "Our Barcode" || barcodestatus == "Static Barcode")
                        {
                            List<PurchaseDetail> plist = pdetailrepo.GetAll().Where(t => t.ProductName == Proname && t.CompanyName == company && t.Unit == unit && t.PurchaseMaster.Suppliername == "First stock" && t.PurchaseMaster.TAXType != "TAXPurchase" && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.StockIn == Convert.ToInt32(cmbGodownName.SelectedValue)).ToList();
                            if (plist.Count > 0)
                            {
                                if (plist.Count == 1)
                                    goto Col;
                                else
                                {
                                    var StockId = pdetailrepo.GetAll().FirstOrDefault();
                                    if (barcodestatus == "Our Barcode")
                                        StockId = pdetailrepo.GetAll().Where(t => t.ProductName == Proname && t.CompanyName == company && t.Unit == unit && t.SaleRate == Convert.ToDecimal(txtsalerate.Text) && t.PurchaseMaster.Suppliername == "First stock" && t.PurchaseMaster.TAXType != "TAXPurchase" && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.StockIn == Convert.ToInt32(cmbGodownName.SelectedValue)).FirstOrDefault();
                                    else
                                        StockId = pdetailrepo.GetAll().Where(t => t.ProductName == Proname && t.CompanyName == company && t.Unit == unit && t.SaleRate == Convert.ToDecimal(txtsalerate.Text) && t.PurchaseMaster.Suppliername == "First stock" && t.PurchaseMaster.TAXType != "TAXPurchase" && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.StockIn == Convert.ToInt32(cmbGodownName.SelectedValue)).FirstOrDefault();
                                    if (StockId == null)
                                        goto Col;
                                    else
                                    {
                                        MessageBox.Show("Add Different Sale Rate in This Product", "Warning");
                                        this.ActiveControl = txtsalerate;
                                        goto col1;
                                    }
                                }
                            }
                        }
                        else
                            goto Col;
                    Col:
                        {
                            GvProductInfo.Rows[colIndex].Cells["ProductNameg"].Value = Proname;
                            GvProductInfo.Rows[colIndex].Cells["Company"].Value = company;
                            GvProductInfo.Rows[colIndex].Cells["Unit"].Value = unit;
                            GvProductInfo.Rows[colIndex].Cells["PurUnit"].Value = cmbUnit.Text;
                            GvProductInfo.Rows[colIndex].Cells["Quantity"].Value = txtQuantity.Text;
                            GvProductInfo.Rows[colIndex].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                            GvProductInfo.Rows[colIndex].Cells["PrintBarcode"].Value = AIOInventorySystem.Desk.Properties.Resources.button_print_barcode;
                            if (ProductCategory == "Motor" && isaddmandatory == true)
                            {
                                GvProductInfo.Rows[colIndex].Cells["MandatoryCodeSeries"].Value = "Y";
                                GvProductInfo.Rows[colIndex].Cells["Code"].Value = dtgvcode.Rows[0].Cells[0].Value.ToString();
                            }
                            else
                                GvProductInfo.Rows[colIndex].Cells["MandatoryCodeSeries"].Value = "N";
                            GvProductInfo.Rows[colIndex].Cells["IsModified"].Value = "Y";   //Y - Product modified , N- if not modified (used for reference only on updation)

                            if (productentrytype == 1 || productentrytype == 2)
                            {
                                GvProductInfo.Rows[colIndex].Cells["BatchNo"].Value = txtBatchNo.Text;
                                if (productentrytype != 2)
                                    GvProductInfo.Rows[colIndex].Cells["ExpiryDate"].Value = dtpExpiry.Value;
                            }
                            if (barcodestatus == "Our Barcode")
                            {
                                if (updateBarcode == "" || updateBarcode == null)
                                    GvProductInfo.Rows[colIndex].Cells["Barcode"].Value = generateBarcode();
                                else
                                    GvProductInfo.Rows[colIndex].Cells["Barcode"].Value = updateBarcode;
                            }
                            else
                                GvProductInfo.Rows[colIndex].Cells["Barcode"].Value = string.Empty;
                            GvProductInfo.Rows[colIndex].Cells["Salerate"].Value = txtsalerate.Text;
                            GvProductInfo.Rows[colIndex].Cells["StaticBarcode"].Value = txtStaticBarcode.Text;
                            colIndex = -1;
                            txtpname.Text = "";
                            txtQuantity.Text = "";
                            txtBatchNo.Text = "";
                            txtsalerate.Text = "0";
                            txtStaticBarcode.Text = "";
                            dtpExpiry.Value = DateTime.Now.Date;
                            cmbUnit.Items.Clear();
                            this.ActiveControl = txtpname;
                        }
                    col1: { }
                    }
                    else
                    {
                        if (btnSave.Text == "Update")
                        {
                            var StockId = pdetailrepo.GetAll().FirstOrDefault();
                            if (barcodestatus == "Our Barcode" || barcodestatus == "Static Barcode")
                            {
                                StockId = pdetailrepo.GetAll().Where(t => t.ProductName == Proname && t.CompanyName == company && t.Unit == unit && t.SaleRate == Convert.ToDecimal(txtsalerate.Text) && t.PurchaseMaster.Suppliername == "First stock" && t.PurchaseMaster.TAXType != "TaxPurchase" && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.StockIn == Convert.ToInt32(cmbGodownName.SelectedValue)).FirstOrDefault();
                                if (StockId == null)
                                {
                                    if (btnSave.Text == "Update")
                                    {
                                        if (ProductCategory != "Motor")
                                        {
                                            updateBarcode = "";
                                            GvProductInfo.Rows.Add();
                                            int i = GvProductInfo.RowCount;
                                            GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                                            GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                                            GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                                            GvProductInfo.Rows[i - 1].Cells["PurUnit"].Value = cmbUnit.Text;
                                            GvProductInfo.Rows[i - 1].Cells["Quantity"].Value = txtQuantity.Text;
                                            GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                            GvProductInfo.Rows[i - 1].Cells["PrintBarcode"].Value = AIOInventorySystem.Desk.Properties.Resources.button_print_barcode;
                                            GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = " ";
                                            if (productentrytype == 1 || productentrytype == 2)
                                            {
                                                GvProductInfo.Rows[i - 1].Cells["BatchNo"].Value = txtBatchNo.Text;
                                                if (productentrytype != 2)
                                                    GvProductInfo.Rows[i - 1].Cells["ExpiryDate"].Value = dtpExpiry.Value;
                                            }
                                            else
                                            {
                                                GvProductInfo.Rows[i - 1].Cells["BatchNo"].Value = "";
                                                GvProductInfo.Rows[i - 1].Cells["ExpiryDate"].Value = null;
                                            }
                                            if (barcodestatus == "Our Barcode")
                                            {
                                                if (updateBarcode == "" || updateBarcode == null)
                                                    GvProductInfo.Rows[i - 1].Cells["Barcode"].Value = generateBarcode();
                                            }
                                            else
                                                GvProductInfo.Rows[i - 1].Cells["Barcode"].Value = string.Empty;
                                            GvProductInfo.Rows[i - 1].Cells["Salerate"].Value = txtsalerate.Text;
                                            GvProductInfo.Rows[i - 1].Cells["StaticBarcode"].Value = txtStaticBarcode.Text;
                                            GvProductInfo.Rows[i - 1].Cells["IsModified"].Value = "Y";
                                        }
                                        else if (ProductCategory == "Motor")
                                        {
                                            if (isaddmandatory == true)
                                            {
                                                for (int l = 0; l < dtgvcode.RowCount; l++)
                                                {
                                                    GvProductInfo.Rows.Add();
                                                    int i = GvProductInfo.RowCount;
                                                    GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                                                    GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                                                    GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                                                    GvProductInfo.Rows[i - 1].Cells["PurUnit"].Value = cmbUnit.Text;
                                                    GvProductInfo.Rows[i - 1].Cells["Quantity"].Value = 1;
                                                    GvProductInfo.Rows[i - 1].Cells["Code"].Value = dtgvcode.Rows[l].Cells[0].Value.ToString();
                                                    GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                                    GvProductInfo.Rows[i - 1].Cells["PrintBarcode"].Value = AIOInventorySystem.Desk.Properties.Resources.button_print_barcode;
                                                    if (isaddmandatory == true)
                                                        GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = "Y";
                                                    else
                                                        GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = "N";
                                                }
                                            }
                                            else
                                            {
                                                GvProductInfo.Rows.Add();
                                                int i = GvProductInfo.RowCount;
                                                GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                                                GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                                                GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                                                GvProductInfo.Rows[i - 1].Cells["PurUnit"].Value = cmbUnit.Text;
                                                GvProductInfo.Rows[i - 1].Cells["Quantity"].Value = txtQuantity.Text;
                                                GvProductInfo.Rows[i - 1].Cells["Code"].Value = "";
                                                GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                                GvProductInfo.Rows[i - 1].Cells["PrintBarcode"].Value = AIOInventorySystem.Desk.Properties.Resources.button_print_barcode;
                                                if (isaddmandatory == true)
                                                    GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = "Y";
                                                else
                                                    GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = "N";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Already Saved This Product Stock.", "Warning");
                                    txtpname.Text = "";
                                    txtQuantity.Text = "";
                                }
                            }
                            else
                            {
                                var StockId1 = pdetailrepo.GetAll().FirstOrDefault();
                                if (productentrytype == 1 || productentrytype == 2)
                                    StockId1 = pdetailrepo.GetAll().Where(t => t.ProductName == Proname && t.CompanyName == company && t.Unit == unit && t.PurchaseMaster.Suppliername == "First stock" && t.PurchaseMaster.TAXType != "TaxPurchase" && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.StockIn == Convert.ToInt32(cmbGodownName.SelectedValue) && t.BatchNo.Trim() == txtBatchNo.Text.Trim()).FirstOrDefault();
                                else
                                    StockId1 = pdetailrepo.GetAll().Where(t => t.ProductName == Proname && t.CompanyName == company && t.Unit == unit && t.PurchaseMaster.Suppliername == "First stock" && t.PurchaseMaster.TAXType != "TaxPurchase" && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.StockIn == Convert.ToInt32(cmbGodownName.SelectedValue)).FirstOrDefault();
                                if (StockId1 != null)
                                {
                                    MessageBox.Show("Already Saved This Product Stock.", "Warning");
                                    txtpname.Text = "";
                                    txtQuantity.Text = "";
                                }
                                else
                                {
                                    btnSave.Text = "Save";
                                    if (btnSave.Text == "Save")
                                    {
                                        if (ProductCategory != "Motor")
                                        {
                                            GvProductInfo.Rows.Add();
                                            int i = GvProductInfo.RowCount;
                                            GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                                            GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                                            GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                                            GvProductInfo.Rows[i - 1].Cells["PurUnit"].Value = cmbUnit.Text;
                                            GvProductInfo.Rows[i - 1].Cells["Quantity"].Value = txtQuantity.Text;
                                            GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                            GvProductInfo.Rows[i - 1].Cells["PrintBarcode"].Value = AIOInventorySystem.Desk.Properties.Resources.button_print_barcode;
                                            GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = " ";
                                            if (productentrytype == 1 || productentrytype == 2)
                                            {
                                                GvProductInfo.Rows[i - 1].Cells["BatchNo"].Value = txtBatchNo.Text;
                                                if (productentrytype != 2)
                                                    GvProductInfo.Rows[i - 1].Cells["ExpiryDate"].Value = dtpExpiry.Value;
                                            }
                                            else
                                            {
                                                GvProductInfo.Rows[i - 1].Cells["BatchNo"].Value = "";
                                                GvProductInfo.Rows[i - 1].Cells["ExpiryDate"].Value = null;
                                            }
                                            if (barcodestatus == "Our Barcode")
                                            {
                                                if (updateBarcode == "" || updateBarcode == null)
                                                    GvProductInfo.Rows[i - 1].Cells["Barcode"].Value = generateBarcode();
                                                else
                                                    GvProductInfo.Rows[i - 1].Cells["Barcode"].Value = updateBarcode;
                                            }
                                            else
                                                GvProductInfo.Rows[i - 1].Cells["Barcode"].Value = string.Empty;
                                            GvProductInfo.Rows[i - 1].Cells["Salerate"].Value = txtsalerate.Text;
                                            GvProductInfo.Rows[i - 1].Cells["StaticBarcode"].Value = txtStaticBarcode.Text;
                                        }
                                        else if (ProductCategory == "Motor")
                                        {
                                            if (isaddmandatory == true)
                                            {
                                                for (int l = 0; l < dtgvcode.RowCount; l++)
                                                {
                                                    GvProductInfo.Rows.Add();
                                                    int i = GvProductInfo.RowCount;
                                                    GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                                                    GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                                                    GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                                                    GvProductInfo.Rows[i - 1].Cells["PurUnit"].Value = cmbUnit.Text;
                                                    GvProductInfo.Rows[i - 1].Cells["Quantity"].Value = 1;
                                                    GvProductInfo.Rows[i - 1].Cells["Code"].Value = dtgvcode.Rows[l].Cells[0].Value.ToString();
                                                    GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                                    if (isaddmandatory == true)
                                                        GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = "Y";
                                                    else
                                                        GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = "N";
                                                }
                                            }
                                            else
                                            {
                                                GvProductInfo.Rows.Add();
                                                int i = GvProductInfo.RowCount;
                                                GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                                                GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                                                GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                                                GvProductInfo.Rows[i - 1].Cells["PurUnit"].Value = cmbUnit.Text;
                                                GvProductInfo.Rows[i - 1].Cells["Quantity"].Value = txtQuantity.Text;
                                                GvProductInfo.Rows[i - 1].Cells["Code"].Value = "";
                                                GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                                if (isaddmandatory == true)
                                                    GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = "Y";
                                                else
                                                    GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = "N";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            var StockId = pdetailrepo.GetAll().FirstOrDefault();
                            if (productentrytype == 1 || productentrytype == 2)
                                StockId = pdetailrepo.GetAll().Where(t => t.ProductName == Proname && t.CompanyName == company && t.Unit == unit && t.PurchaseMaster.Suppliername == "First stock" && t.PurchaseMaster.TAXType != "TaxPurchase" && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.StockIn == Convert.ToInt32(cmbGodownName.SelectedValue) && t.BatchNo.Trim() == txtBatchNo.Text.Trim()).FirstOrDefault();
                            else
                                StockId = pdetailrepo.GetAll().Where(t => t.ProductName == Proname && t.CompanyName == company && t.Unit == unit && t.PurchaseMaster.Suppliername == "First stock" && t.PurchaseMaster.TAXType != "TaxPurchase" && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.StockIn == Convert.ToInt32(cmbGodownName.SelectedValue)).FirstOrDefault();
                            if (StockId != null)
                            {
                                MessageBox.Show("Already Saved This Product Stock.", "Warning");
                                txtpname.Text = "";
                                txtQuantity.Text = "";
                            }
                            else
                            {
                                btnSave.Text = "Save";
                                if (btnSave.Text == "Save")
                                {
                                    if (ProductCategory != "Motor")
                                    {
                                        GvProductInfo.Rows.Add();
                                        int i = GvProductInfo.RowCount;
                                        GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                                        GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                                        GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                                        GvProductInfo.Rows[i - 1].Cells["PurUnit"].Value = cmbUnit.Text;
                                        GvProductInfo.Rows[i - 1].Cells["Quantity"].Value = txtQuantity.Text;
                                        GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                        GvProductInfo.Rows[i - 1].Cells["PrintBarcode"].Value = AIOInventorySystem.Desk.Properties.Resources.button_print_barcode;
                                        GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = " ";
                                        if (productentrytype == 1 || productentrytype == 2)
                                        {
                                            GvProductInfo.Rows[i - 1].Cells["BatchNo"].Value = txtBatchNo.Text;
                                            if (productentrytype != 2)
                                                GvProductInfo.Rows[i - 1].Cells["ExpiryDate"].Value = dtpExpiry.Value;
                                        }
                                        else
                                        {
                                            GvProductInfo.Rows[i - 1].Cells["BatchNo"].Value = "";
                                            GvProductInfo.Rows[i - 1].Cells["ExpiryDate"].Value = null;
                                        }
                                        if (barcodestatus == "Our Barcode")
                                        {
                                            if (updateBarcode == "" || updateBarcode == null)
                                                GvProductInfo.Rows[i - 1].Cells["Barcode"].Value = generateBarcode();
                                            else
                                                GvProductInfo.Rows[i - 1].Cells["Barcode"].Value = updateBarcode;
                                        }
                                        else
                                            GvProductInfo.Rows[i - 1].Cells["Barcode"].Value = string.Empty;
                                        GvProductInfo.Rows[i - 1].Cells["Salerate"].Value = txtsalerate.Text;
                                        GvProductInfo.Rows[i - 1].Cells["StaticBarcode"].Value = txtStaticBarcode.Text;
                                    }
                                    else if (ProductCategory == "Motor")
                                    {
                                        if (isaddmandatory == true)
                                        {
                                            for (int l = 0; l < dtgvcode.RowCount; l++)
                                            {
                                                GvProductInfo.Rows.Add();
                                                int i = GvProductInfo.RowCount;
                                                GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                                                GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                                                GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                                                GvProductInfo.Rows[i - 1].Cells["PurUnit"].Value = cmbUnit.Text;
                                                GvProductInfo.Rows[i - 1].Cells["Quantity"].Value = 1;
                                                GvProductInfo.Rows[i - 1].Cells["Code"].Value = dtgvcode.Rows[l].Cells[0].Value.ToString();
                                                GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                                GvProductInfo.Rows[i - 1].Cells["PrintBarcode"].Value = AIOInventorySystem.Desk.Properties.Resources.button_print_barcode;
                                                if (isaddmandatory == true)
                                                    GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = "Y";
                                                else
                                                    GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = "N";
                                            }
                                        }
                                        else
                                        {
                                            GvProductInfo.Rows.Add();
                                            int i = GvProductInfo.RowCount;
                                            GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                                            GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                                            GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                                            GvProductInfo.Rows[i - 1].Cells["PurUnit"].Value = cmbUnit.Text;
                                            GvProductInfo.Rows[i - 1].Cells["Quantity"].Value = txtQuantity.Text;
                                            GvProductInfo.Rows[i - 1].Cells["Code"].Value = "";
                                            GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                            GvProductInfo.Rows[i - 1].Cells["PrintBarcode"].Value = AIOInventorySystem.Desk.Properties.Resources.button_print_barcode;
                                            if (isaddmandatory == true)
                                                GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = "Y";
                                            else
                                                GvProductInfo.Rows[i - 1].Cells["MandatoryCodeSeries"].Value = "N";
                                        }
                                    }
                                }
                            }
                        }
                        txtpname.Text = "";
                        txtQuantity.Text = "";
                        txtBatchNo.Text = "";
                        txtsalerate.Text = "0";
                        txtStaticBarcode.Text = "";
                        dtpExpiry.Value = DateTime.Now.Date;
                        dtgvcode.Rows.Clear();
                        this.ActiveControl = txtpname;
                    }
                    pdetailrepo.Dispose();
                a: { }
                }
                else
                {
                    if (txtpname.Text == "")
                    {
                        MessageBox.Show("Enter Product Name.", "Warning");
                        this.ActiveControl = txtpname;
                    }
                    else
                    {
                        MessageBox.Show("Enter Quantity.", "Warning");
                        this.ActiveControl = txtQuantity;
                    }
                }
                isaddmandatory = false;
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                isaddmandatory = false;
                if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                    this.Close();
            }
            catch (Exception)
            { }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clear();
            this.ActiveControl = txtpname;
        }

        private void frmAddStock_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                tooltipbtn.SetToolTip(this.rbtnaddproduct, "Click To Add Product");
                dtpExpiry.MinDate = DateTimePicker.MinimumDateTime;
                dtpExpiry.MaxDate = DateTimePicker.MaximumDateTime;
                autocompletedata();
            }
            catch (Exception)
            { }
        }

        private void txtpname_Leave(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Visible = false;
                decimal flag = 1;
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
                        productrepo1 = new ProductRepository();
                        productdata = new ProductInformation();

                        if (CommonMethod.commProduct == true)
                            productdata = productrepo1.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                        else
                            productdata = productrepo1.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();

                        if ((productdata.ProductName).Contains("'"))
                            productdata.ProductName = productdata.ProductName.Replace("'", "''");
                        if ((productdata.ManufactureCompany).Contains("'"))
                            productdata.ManufactureCompany = productdata.ManufactureCompany.Replace("'", "''");
                        if ((productdata.Uniti).Contains("'"))
                            productdata.Uniti = productdata.Uniti.Replace("'", "''");

                        if (productdata == null)
                        {
                            MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                            txtpname.Text = "";
                            this.ActiveControl = txtpname;
                        }
                        else
                        {
                            getProductType(proname, company, unit);
                            cmbUnit.Items.Clear();
                            cmbUnit.DisplayMember = "Text";
                            cmbUnit.ValueMember = "Value";

                            cmbUnit.Items.Add(new { Text = productdata.Uniti, Value = productdata.Uniti });
                            if (productdata.AlternateUnit != null && productdata.AlternateUnit != "")
                                cmbUnit.Items.Add(new { Text = productdata.AlternateUnit, Value = productdata.AlternateUnit });
                            cmbUnit.SelectedIndex = 0;
                            ProductCategory = productdata.Category;
                            barcodestatus = productdata.BarcodeStatus;
                            if ((GvProductInfo.RowCount == 0 && btnSave.Text == "Save") || (GvProductInfo.RowCount != 0 && btnSave.Text == "Update"))
                            {
                                //                                   0          1            2          3         4             5       6           7       8
                                DataTable dt = db.GetTable("Select d.Id,d.ProductName,d.CompanyName,d.Unit,d.PurchaseUnit,d.Quantity,d.Code,m.Id as MId,d.Barcode," +
                                    //     9            10          11          12
                                    "d.SaleRate,d.StaticBarcode,d.BatchNo,d.ExpireDate from PurchaseDetail d inner join PurchaseMaster m on m.Id=d.PorderNo where m.Suppliername='First stock' and m.TAXType is null and d.ProductName='" + productdata.ProductName + "' and d.CompanyName='" + productdata.ManufactureCompany + "' and d.Unit='" + productdata.Uniti + "' and m.StockIn='" + cmbGodownName.SelectedValue + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId='" + CommonMethod.CompId + "'");
                                if (dt.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        int j = GvProductInfo.RowCount;
                                        for (int o = 0; o < GvProductInfo.RowCount; o++)
                                        {
                                            if (dt.Rows[i]["ProductName"].ToString() == GvProductInfo.Rows[o].Cells["ProductNameg"].Value.ToString() && dt.Rows[i]["CompanyName"].ToString() == GvProductInfo.Rows[o].Cells["Company"].Value.ToString() && dt.Rows[i]["Unit"].ToString() == GvProductInfo.Rows[o].Cells["Unit"].Value.ToString() && Convert.ToInt32(dt.Rows[i]["Id"].ToString()) == Convert.ToInt32(GvProductInfo.Rows[o].Cells["Id"].Value.ToString()))
                                            {
                                                flag = 0;
                                                break;
                                            }
                                            else
                                                flag = 1;
                                        }
                                        if (flag == 1)
                                        {
                                            PurchaseDetailRepository PurDRepo = new PurchaseDetailRepository();
                                            int id = Convert.ToInt32(PurDRepo.GetAll().Where(t => t.ProductName == dt.Rows[i]["ProductName"].ToString() && t.CompanyName == dt.Rows[i]["CompanyName"].ToString() && t.Unit == dt.Rows[i]["Unit"].ToString() && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId).LastOrDefault().Id);
                                            if (dt.Rows[i]["Code"].ToString() != "")
                                            {
                                                for (int k = i; k > 0; k--)
                                                {
                                                    if (dt.Rows[i]["MId"].ToString() == dt.Rows[k]["MId"].ToString() && dt.Rows[k]["Code"].ToString() != "")
                                                    {
                                                        GvProductInfo.Rows.Add();
                                                        GvProductInfo.Rows[j].Cells["Id"].Value = dt.Rows[k]["Id"].ToString();
                                                        GvProductInfo.Rows[j].Cells["ProductNameg"].Value = dt.Rows[k]["ProductName"].ToString();
                                                        GvProductInfo.Rows[j].Cells["Company"].Value = dt.Rows[k]["CompanyName"].ToString();
                                                        GvProductInfo.Rows[j].Cells["Unit"].Value = dt.Rows[k]["Unit"].ToString();
                                                        if (dt.Rows[k]["PurchaseUnit"].ToString() != "")
                                                            GvProductInfo.Rows[j].Cells["PurUnit"].Value = dt.Rows[k]["PurchaseUnit"].ToString();
                                                        else
                                                            GvProductInfo.Rows[j].Cells["PurUnit"].Value = dt.Rows[k]["Unit"].ToString();
                                                        if (dt.Rows[k]["Unit"].ToString() != dt.Rows[k]["PurchaseUnit"].ToString() && dt.Rows[k]["PurchaseUnit"].ToString() != "")
                                                        {
                                                            ProductRepository productrepo = new ProductRepository();
                                                            ProductInformation productdata1 = new ProductInformation();
                                                            if (CommonMethod.commProduct == true)
                                                                productdata1 = productrepo.GetAll().Where(t => t.ProductName == dt.Rows[k]["ProductName"].ToString() && t.ManufactureCompany == dt.Rows[k]["CompanyName"].ToString() && t.Uniti == dt.Rows[k]["Unit"].ToString()).FirstOrDefault();
                                                            else
                                                                productdata1 = productrepo.GetAll().Where(t => t.ProductName == dt.Rows[k]["ProductName"].ToString() && t.ManufactureCompany == dt.Rows[k]["CompanyName"].ToString() && t.Uniti == dt.Rows[k]["Unit"].ToString() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                                            decimal qty;
                                                            qty = Convert.ToDecimal(dt.Rows[k]["Quantity"].ToString()) / (Convert.ToDecimal(productdata1.UnitQty) / Convert.ToDecimal(productdata1.AlternateUnitQty));
                                                            GvProductInfo.Rows[j].Cells["Quantity"].Value = qty;
                                                        }
                                                        else
                                                            GvProductInfo.Rows[j].Cells["Quantity"].Value = dt.Rows[k]["Quantity"].ToString();
                                                        GvProductInfo.Rows[j].Cells["Code"].Value = dt.Rows[k]["Code"].ToString();
                                                        if (barcodestatus == "Our Barcode" || barcodestatus == "Static Barcode")
                                                        {
                                                            GvProductInfo.Rows[j].Cells["Barcode"].Value = dt.Rows[k]["Barcode"].ToString();
                                                            GvProductInfo.Rows[j].Cells["Salerate"].Value = dt.Rows[k]["SaleRate"].ToString();
                                                            GvProductInfo.Rows[j].Cells["StaticBarcode"].Value = dt.Rows[k]["StaticBarcode"].ToString();
                                                            GvProductInfo.Rows[j].Cells["IsModified"].Value = "Y";
                                                            GvProductInfo.Rows[j].Cells["ChooseBarcodeProduct"].Value = true;
                                                        }
                                                        if (productentrytype == 1 || productentrytype == 2)
                                                        {
                                                            GvProductInfo.Rows[j].Cells["BatchNo"].Value = dt.Rows[i]["BatchNo"].ToString();
                                                            GvProductInfo.Rows[j].Cells["ExpiryDate"].Value = dt.Rows[i]["ExpireDate"].ToString();
                                                        }
                                                        GvProductInfo.Rows[j].Cells["IsModified"].Value = "Y";
                                                        GvProductInfo.Rows[j].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                                        GvProductInfo.Rows[j].Cells["PrintBarcode"].Value = AIOInventorySystem.Desk.Properties.Resources.button_print_barcode;
                                                        j++;
                                                        btnSave.Text = "Update";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                GvProductInfo.Rows.Add();
                                                GvProductInfo.Rows[j].Cells["Id"].Value = dt.Rows[i]["Id"].ToString();
                                                GvProductInfo.Rows[j].Cells["ProductNameg"].Value = dt.Rows[i]["ProductName"].ToString();
                                                GvProductInfo.Rows[j].Cells["Company"].Value = dt.Rows[i]["CompanyName"].ToString();
                                                GvProductInfo.Rows[j].Cells["Unit"].Value = dt.Rows[i]["Unit"].ToString();
                                                if (dt.Rows[i]["PurchaseUnit"].ToString() != "")
                                                    GvProductInfo.Rows[j].Cells["PurUnit"].Value = dt.Rows[i]["PurchaseUnit"].ToString();
                                                else
                                                    GvProductInfo.Rows[j].Cells["PurUnit"].Value = dt.Rows[i]["Unit"].ToString();
                                                if (dt.Rows[i]["Unit"].ToString() != dt.Rows[i]["PurchaseUnit"].ToString() && dt.Rows[i]["PurchaseUnit"].ToString() != "")
                                                {
                                                    ProductRepository productrepo = new ProductRepository();
                                                    ProductInformation productdata1 = new ProductInformation();
                                                    if (CommonMethod.commProduct == true)
                                                        productdata1 = productrepo.GetAll().Where(t => t.ProductName == dt.Rows[i]["ProductName"].ToString() && t.ManufactureCompany == dt.Rows[i]["CompanyName"].ToString() && t.Uniti == dt.Rows[i]["Unit"].ToString()).FirstOrDefault();
                                                    else
                                                        productdata1 = productrepo.GetAll().Where(t => t.ProductName == dt.Rows[i]["ProductName"].ToString() && t.ManufactureCompany == dt.Rows[i]["CompanyName"].ToString() && t.Uniti == dt.Rows[i]["Unit"].ToString() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                                    GvProductInfo.Rows[j].Cells["Quantity"].Value = Convert.ToDecimal((Convert.ToDecimal(dt.Rows[i]["Quantity"].ToString()) / (Convert.ToDecimal(productdata1.UnitQty) / Convert.ToDecimal(productdata1.AlternateUnitQty)))).ToString();
                                                }
                                                else
                                                    GvProductInfo.Rows[j].Cells["Quantity"].Value = dt.Rows[i]["Quantity"].ToString();
                                                GvProductInfo.Rows[j].Cells["Code"].Value = dt.Rows[i]["Code"].ToString();
                                                if (barcodestatus == "Our Barcode" || barcodestatus == "Static Barcode")
                                                {
                                                    GvProductInfo.Rows[j].Cells["Barcode"].Value = dt.Rows[i]["Barcode"].ToString();
                                                    GvProductInfo.Rows[j].Cells["Salerate"].Value = dt.Rows[i]["SaleRate"].ToString();
                                                    GvProductInfo.Rows[j].Cells["StaticBarcode"].Value = dt.Rows[i]["StaticBarcode"].ToString();
                                                    GvProductInfo.Rows[j].Cells["IsModified"].Value = "Y";
                                                    GvProductInfo.Rows[j].Cells["ChooseBarcodeProduct"].Value = true;
                                                }
                                                if (productentrytype == 1 || productentrytype == 2)
                                                {
                                                    GvProductInfo.Rows[j].Cells["BatchNo"].Value = dt.Rows[i]["BatchNo"].ToString();
                                                    GvProductInfo.Rows[j].Cells["ExpiryDate"].Value = dt.Rows[i]["ExpireDate"].ToString();
                                                }
                                                GvProductInfo.Rows[j].Cells["IsModified"].Value = "Y";
                                                GvProductInfo.Rows[j].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                                GvProductInfo.Rows[j].Cells["PrintBarcode"].Value = AIOInventorySystem.Desk.Properties.Resources.button_print_barcode;
                                                j++;
                                                btnSave.Text = "Update";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        barcodestatus = productdata.BarcodeStatus;
                        if (barcodestatus == "Static Barcode" || barcodestatus == "Our Barcode")
                        {
                            if (barcodestatus == "Static Barcode")
                            {
                                txtStaticBarcode.Visible = true;
                                txtsalerate.Visible = true;
                                label7.Visible = true;
                                label8.Visible = true;
                            }
                            else
                            {
                                txtsalerate.Visible = true;
                                label7.Visible = true;
                            }
                        }
                        else
                        {
                            txtStaticBarcode.Visible = false;
                            txtStaticBarcode.Text = string.Empty;
                            txtsalerate.Visible = false;
                            label7.Visible = false;
                            label8.Visible = false;
                        }
                        if (productentrytype == 1)
                        {
                            lblBatch.Visible = true;
                            lblExp.Visible = true;
                            txtBatchNo.Visible = true;
                            dtpExpiry.Visible = true;
                        }
                        else if (productentrytype == 2)
                        {
                            lblBatch.Visible = true;
                            txtBatchNo.Visible = true;
                        }
                        else
                        {
                            lblBatch.Visible = false;
                            lblExp.Visible = false;
                            txtBatchNo.Visible = false;
                            dtpExpiry.Visible = false;
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
                refreshFlag = false;
            }
            catch (Exception)
            { }
        }

        private void txtvat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(txtvat.Text) <= 100)
                { }
                else
                {
                    MessageBox.Show("Enter Vat Between 0 to 100 %.", "Warning");
                    txtvat.Text = "0";
                    this.ActiveControl = txtvat;
                }
            }
            catch (Exception)
            { }
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            try
            {
                if (productentrytype == 1)
                {
                    txtBatchNo.Visible = true;
                    dtpExpiry.Visible = true;
                    lblBatch.Visible = true;
                    lblExp.Visible = true;
                }
                else if (productentrytype == 2)
                {
                    txtBatchNo.Visible = true;
                    lblBatch.Visible = true;
                }
                else
                {
                    txtBatchNo.Visible = false;
                    dtpExpiry.Visible = false;
                    lblBatch.Visible = false;
                    lblExp.Visible = false;
                }

                if (colIndex != -1)
                {
                    if (ProductCategory == "Motor")
                    {
                        if (Convert.ToDecimal(txtQuantity.Text) != 1 && isaddmandatory == true)
                        {
                            MessageBox.Show("Quantity must be 1 for update.", "Warning");
                            txtQuantity.Text = "1";
                        }
                        if (refreshFlag == false)
                        {
                            if (MessageBox.Show("Do you want to enter series", "Enter Series", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                isaddmandatory = true;
                                refreshFlag = true;
                                goto t;
                            }
                            else
                            {
                                isaddmandatory = false;
                                refreshFlag = false;
                                goto s;
                            }
                        }
                    }
                    else
                        goto s;
                }
                else if (colIndex == -1)
                {
                    if (ProductCategory == "Motor")
                    {
                        if (refreshFlag == false)
                            if (MessageBox.Show("Do you want to enter series", "Enter Series", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                isaddmandatory = true;
                                refreshFlag = true;
                                goto t;
                            }
                            else
                            {
                                isaddmandatory = false;
                                refreshFlag = true;
                                goto s;
                            }
                    }
                    else
                        goto s;
                }
            t:
                {
                    dtgvcode.Rows.Clear();
                    for (int i = 0; i < Convert.ToDecimal(txtQuantity.Text); i++)
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

            s: { }
            }
            catch (Exception)
            { }
        }

        private void dtgvcode_Leave(object sender, EventArgs e)
        {
            try
            {
                int currentRow_code = 0;
                bool findBlank = false;
                if (codeflag == 0)
                {
                    if (dtgvcode.Visible == true)
                    {
                        if (dtgvcode.RowCount > 0)
                        {
                            for (int i = 0; i < dtgvcode.RowCount; i++)
                            {
                                if (dtgvcode.Rows[i].Cells[0].Value.ToString() == "")
                                {
                                    findBlank = true;
                                    currentRow_code = i;
                                }
                            }
                            if (findBlank == true)
                            {
                                MessageBox.Show("All Quantity must have Serial No or Code.", "Warning");
                                dtgvcode.Visible = true;
                                this.ActiveControl = dtgvcode;
                                dtgvcode.CurrentCell = dtgvcode[0, currentRow_code];
                                dtgvcode.CurrentCell.Selected = true;
                            }
                            else
                            {
                                dtgvcode.Visible = false;
                                this.ActiveControl = btnAdd;
                            }
                        }
                        else
                        {
                            dtgvcode.Visible = false;
                            this.ActiveControl = btnAdd;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < dtgvcode.RowCount; i++)
                    {
                        if (dtgvcode.Rows[i].Cells[0].Value.ToString() == "")
                        {
                            findBlank = true;
                            currentRow_code = i;
                        }
                    }
                    if (findBlank == true)
                    {
                        MessageBox.Show("All Quantity must have Serial No or Code.", "Warning");
                        dtgvcode.Visible = true;
                        this.ActiveControl = dtgvcode;
                        dtgvcode.CurrentCell = dtgvcode[0, currentRow_code];
                        dtgvcode.CurrentCell.Selected = true;
                    }
                    else
                    {
                        dtgvcode.Visible = false;
                        this.ActiveControl = btnAdd;
                    }
                }
                refreshFlag = false;
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
                    flag = 0;
                    string Proname = txtpname.Text.Trim().Split(',')[0];
                    string company = txtpname.Text.Trim().Split(',')[1];
                    string unit = txtpname.Text.Trim().Split(',')[2];

                    SqlDataAdapter da = new SqlDataAdapter("(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + Proname + "' AND gd.CompanyName='" + company + "' AND gd.Unit='" + unit + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in( Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))) union (Select  d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where ProductName='" + Proname + "' and MfgCompany='" + company + "' and Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15))  order by Id DESC ) and m.Id>(Select top 1 m.Id from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)) order by Id DESC ) )  union SELECT d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null) and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and m.CompId=pm.CompId and (pm.StockIn=0 or pm.StockIn is null)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + Proname + "' AND gd.CompanyName='" + company + "' AND gd.Unit='" + unit + "' and gm.CompId=" + CommonMethod.CompId + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + CommonMethod.CompId + "'as varchar(10)) as varchar(15)))", db.Connection);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    int intflag = 1;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dtgvcode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != dt.Rows[i]["Code"].ToString())
                            intflag = 1;
                        else
                        {
                            intflag = 0;
                            goto a;
                        }
                    }
                a: { }
                    if (intflag == 0)
                    {
                        MessageBox.Show("This Serial No is already Saved.", "Warning");
                        dtgvcode.Visible = true;
                        dtgvcode.ClearSelection();
                        dtgvcode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                        this.ActiveControl = dtgvcode;
                        dtgvcode.CurrentCell = dtgvcode[e.ColumnIndex, e.RowIndex];
                        dtgvcode.BeginEdit(true);
                        codeflag = 1;
                        goto b;
                    }
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
                                    flag = 1;
                                    break;
                                }
                            }
                        }
                    }
                }
            b: { }
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
                if (txtpname.Text != "")
                {
                    string Proname = txtpname.Text.Trim().Split(',')[0];
                    string company = txtpname.Text.Trim().Split(',')[1];
                    string unit = txtpname.Text.Trim().Split(',')[2];

                    productrepo1 = new ProductRepository();
                    productdata = new ProductInformation();

                    if (CommonMethod.commProduct == true)
                        productdata = productrepo1.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                    else
                        productdata = productrepo1.GetAll().Where(t => t.ProductName == Proname && t.ManufactureCompany == company && t.Uniti == unit && t.CompId == CommonMethod.CompId).FirstOrDefault();

                    if (productdata != null)
                    {
                        if (productdata.Image != null)
                        {
                            pictureBox1.Visible = true;
                            pictureBox1.Height = 283;
                            pictureBox1.Width = 295;
                            byte[] @photoProduct;
                            @photoProduct = (byte[])productdata.Image;
                            System.IO.MemoryStream ms = new System.IO.MemoryStream(@photoProduct);
                            pictureBox1.Image = System.Drawing.Image.FromStream(ms);
                            this.ActiveControl = txtpname;
                        }
                        else
                        {
                            pictureBox1.Image = null;
                            MessageBox.Show("Image Not Available.", "Warning");
                            this.ActiveControl = txtpname;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Enter Product Name First.", "Warning");
                    this.ActiveControl = txtpname;
                }
            }
            if (keyData == Keys.F3)
            {
                btnSave_Click(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Visible = true;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = 100;
                int intgdid;
                DataTable dt = new DataTable();
                intgdid = Convert.ToInt32(cmbGodownName.SelectedValue);
                if (intgdid != 0)
                    //                          0       1           2           3       4               5       6       7       8                       9       10             11        12          13
                    dt = db.GetTable("Select d.Id,d.ProductName,d.CompanyName,d.Unit,d.PurchaseUnit,d.Quantity,d.Code,m.Id as MId,d.HasMandatoryCodeSeries,d.batchno,d.ExpireDate,d.Barcode,d.SaleRate,d.StaticBarcode,d.AssemblyChildStatus from PurchaseDetail d inner join PurchaseMaster m on m.Id=d.PorderNo where m.Suppliername='First stock' and m.TAXType is null and m.StockIn=" + cmbGodownName.SelectedValue + " and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId=" + CommonMethod.CompId + "");
                else
                    //                          0       1               2         3          4            5       6            7                8               9                               
                    dt = db.GetTable("Select d.Id,d.ProductName,d.CompanyName,d.Unit,d.PurchaseUnit,d.Quantity,d.Code,m.Id as MId,d.HasMandatoryCodeSeries,d.batchno,"+
                    //          10         11        12             13                14
                        "d.ExpireDate,d.Barcode,d.SaleRate,d.StaticBarcode,d.AssemblyChildStatus from PurchaseDetail d inner join PurchaseMaster m on m.Id=d.PorderNo where m.Suppliername='First stock' and m.TAXType is null and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId=" + CommonMethod.CompId + "");

                //----------------------- 0109 modified code----- if code series not given then above code doesn't work for motor category item--/
                if (dt.Rows.Count > 0)
                {
                    progressBar1.Maximum = dt.Rows.Count;
                    int j = 0;
                    GvProductInfo.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        progressBar1.Value = i + 1;
                        PurchaseDetailRepository PurDRepo = new PurchaseDetailRepository();
                        if (Convert.ToString(dt.Rows[i][14].ToString()) == "N" || Convert.ToString(dt.Rows[i][14].ToString()) == null || Convert.ToString(dt.Rows[i][14].ToString()) == "")
                        {
                            GvProductInfo.Rows.Add();
                            GvProductInfo.Rows[j].Cells["Id"].Value = dt.Rows[i]["Id"].ToString();
                            GvProductInfo.Rows[j].Cells["ProductNameg"].Value = dt.Rows[i]["ProductName"].ToString();
                            GvProductInfo.Rows[j].Cells["Company"].Value = dt.Rows[i]["CompanyName"].ToString();
                            GvProductInfo.Rows[j].Cells["Unit"].Value = dt.Rows[i]["Unit"].ToString();
                            if (dt.Rows[i]["PurchaseUnit"].ToString() != "")
                                GvProductInfo.Rows[j].Cells["PurUnit"].Value = dt.Rows[i]["PurchaseUnit"].ToString();
                            else
                                GvProductInfo.Rows[j].Cells["PurUnit"].Value = dt.Rows[i]["Unit"].ToString();
                            if (dt.Rows[i]["Unit"].ToString() != dt.Rows[i]["PurchaseUnit"].ToString() && dt.Rows[i]["PurchaseUnit"].ToString() != "")
                            {
                                productrepo1 = new ProductRepository();
                                productdata = new ProductInformation();
                                if (CommonMethod.commProduct == true)
                                    productdata = productrepo1.GetAll().Where(t => t.ProductName == dt.Rows[i]["ProductName"].ToString() && t.ManufactureCompany == dt.Rows[i]["CompanyName"].ToString() && t.Uniti == dt.Rows[i]["Unit"].ToString()).FirstOrDefault();
                                else
                                    productdata = productrepo1.GetAll().Where(t => t.ProductName == dt.Rows[i]["ProductName"].ToString() && t.ManufactureCompany == dt.Rows[i]["CompanyName"].ToString() && t.Uniti == dt.Rows[i]["Unit"].ToString() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                decimal qty = 0;
                                try
                                {
                                    qty = Convert.ToDecimal(dt.Rows[i]["Quantity"].ToString()) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                }
                                catch (Exception)
                                { }
                                GvProductInfo.Rows[i].Cells["Quantity"].Value = qty;
                            }
                            else
                                GvProductInfo.Rows[j].Cells["Quantity"].Value = dt.Rows[i]["Quantity"].ToString();
                            GvProductInfo.Rows[j].Cells["Code"].Value = dt.Rows[i]["Code"].ToString();
                            GvProductInfo.Rows[j].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                            GvProductInfo.Rows[j].Cells["MandatoryCodeSeries"].Value = dt.Rows[i]["HasMandatoryCodeSeries"].ToString();
                            GvProductInfo.Rows[j].Cells["IsModified"].Value = "N";   //used on modification/update
                            GvProductInfo.Rows[j].Cells["BatchNo"].Value = dt.Rows[i]["batchno"].ToString();
                            GvProductInfo.Rows[j].Cells["ExpiryDate"].Value = dt.Rows[i]["ExpireDate"].ToString();
                            GvProductInfo.Rows[j].Cells["Barcode"].Value = dt.Rows[i]["Barcode"].ToString();
                            GvProductInfo.Rows[j].Cells["Salerate"].Value = dt.Rows[i]["SaleRate"].ToString();
                            GvProductInfo.Rows[j].Cells["StaticBarcode"].Value = dt.Rows[i]["StaticBarcode"].ToString();
                            if (dt.Rows[i]["Barcode"].ToString() != null || dt.Rows[i]["StaticBarcode"].ToString() != null)
                                GvProductInfo.Rows[j].Cells["ChooseBarcodeProduct"].Value = true;
                            GvProductInfo.Rows[j].Cells["PrintBarcode"].Value = AIOInventorySystem.Desk.Properties.Resources.button_print_barcode;
                            j++;
                        }
                    }
                    progressBar1.Visible = false;
                    if (GvProductInfo.RowCount > 0)
                        btnSave.Text = "Update";
                    else
                        MessageBox.Show("Record not found.", "Warning");
                }
                //------------------------------------------------------------------------------------
                progressBar1.Visible = false;
            }
            catch (Exception)
            { progressBar1.Visible = false; }
        }

        private void rbtnaddproduct_Click(object sender, EventArgs e)
        {
            try
            {
                frmProductInformation pro = new frmProductInformation();
                pro.ShowDialog();
                frmAddStock_Load(sender, EventArgs.Empty);
                this.ActiveControl = txtpname;
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
                            MessageBox.Show("Import Excel file sucessfully", "Success");
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
            bool poflag = true;
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

                            string a = Convert.ToString(dtexcel.Rows[i][0].ToString());
                            string b = Convert.ToString(dtexcel.Rows[i][1].ToString());
                            string c = Convert.ToString(dtexcel.Rows[i][2].ToString());
                            decimal d = Convert.ToDecimal(dtexcel.Rows[i][3]);
                            decimal salerate = 0;
                            if (Convert.ToString(dtexcel.Rows[i][5].ToString()) == null || Convert.ToString(dtexcel.Rows[i][5].ToString()) == "")
                                salerate = 0;
                            else
                                salerate = Convert.ToDecimal(dtexcel.Rows[i][5]);
                            if (!string.IsNullOrEmpty(Convert.ToString(dtexcel.Rows[i][0].ToString())) && !string.IsNullOrEmpty(Convert.ToString(dtexcel.Rows[i][1].ToString())) && !string.IsNullOrEmpty(Convert.ToString(dtexcel.Rows[i][2].ToString())) && !string.IsNullOrEmpty(Convert.ToString(dtexcel.Rows[i][3].ToString())))
                            {
                                productrepo1 = new ProductRepository();
                                ProductInformation BillId = new ProductInformation();
                                if (CommonMethod.commProduct == true)
                                    BillId = productrepo1.GetAll().Where(t => t.ProductName.Trim().ToLower() == a.Trim().ToLower() && t.ManufactureCompany.ToLower() == b.Trim().ToLower() && t.Uniti.ToLower() == c.Trim().ToLower()).FirstOrDefault();
                                else
                                    BillId = productrepo1.GetAll().Where(t => t.ProductName.Trim().ToLower() == a.Trim().ToLower() && t.ManufactureCompany.ToLower() == b.Trim().ToLower() && t.Uniti.ToLower() == c.Trim().ToLower() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                if (BillId != null)
                                {
                                    barcodestatus = BillId.BarcodeStatus;
                                    if (BillId.WithBatch == "Y")
                                        batch = "OnlyBatch";
                                    else if (BillId.WithBatchAndExpiry == "Y")
                                        batch = "BatchAndExpire";
                                    PurchaseDetailRepository pdetailrepo1 = new PurchaseDetailRepository();
                                    var StockId = pdetailrepo1.GetAll().Where(t => t.ProductName.Trim().ToLower() == a.Trim().ToLower() && t.CompanyName.Trim().ToLower() == b.Trim().ToLower() && t.Unit.Trim().ToLower() == c.Trim().ToLower() && t.PurchaseMaster.Suppliername == "First stock" && (t.PurchaseMaster.TAXType == null || t.PurchaseMaster.TAXType == "") && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.StockIn == Convert.ToInt32(cmbGodownName.SelectedValue)).FirstOrDefault();
                                    if (barcodestatus == "Static Barcode" || barcodestatus == "Our Static")
                                    {
                                        if (Convert.ToString(dtexcel.Rows[i][4].ToString()) == null && Convert.ToString(dtexcel.Rows[i][4].ToString()) == "")
                                            goto a;
                                        else
                                            goto b;
                                    }
                                    if (batch == "OnlyBatch" || batch == "BatchAndExpire")
                                    {
                                        if (batch == "OnlyBatch")
                                        {
                                            if (Convert.ToString(dtexcel.Rows[i][6].ToString()) == null && Convert.ToString(dtexcel.Rows[i][6].ToString()) == "")
                                                goto a;
                                            else
                                                goto b;
                                        }
                                        else if (batch == "BatchAndExpire")
                                        {
                                            if (dtexcel.Rows[i][6].ToString() == null || Convert.ToString(dtexcel.Rows[i][6].ToString()) == "" || dtexcel.Rows[i][7].ToString() == null || Convert.ToString(dtexcel.Rows[i][7].ToString()) == "")
                                                goto a;
                                            else
                                                goto b;
                                        }
                                    }
                                b:
                                    {
                                        if (StockId != null)
                                        {
                                            try
                                            {
                                                db.connect();
                                                SqlCommand command = new SqlCommand("SPPOnBillSaveUpdate", db.Connection);
                                                command.CommandType = CommandType.StoredProcedure;

                                                command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'A';
                                                command.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(StockId.Id);
                                                command.Parameters.Add("@event", SqlDbType.Int).Value = 2;
                                                command.Parameters.Add("@billtype", SqlDbType.Int).Value = 0;
                                                command.Parameters.Add("@BillNo", SqlDbType.Int).Value = 0;
                                                command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(dtexcel.Rows[i][0].ToString());
                                                command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(dtexcel.Rows[i][1].ToString());
                                                command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(dtexcel.Rows[i][2].ToString());
                                                command.Parameters.Add("@AlternateUnit", SqlDbType.NVarChar).Value = Convert.ToString(dtexcel.Rows[i][2].ToString());
                                                command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(d);
                                                command.Parameters.Add("@FreeQty", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                                command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = "";
                                                command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                                command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                                command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                                command.Parameters.Add("@Vat", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                                command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                                command.Parameters.Add("@StaticBarcode", SqlDbType.NVarChar).Value = Convert.ToString(dtexcel.Rows[i][4].ToString());

                                                if (Convert.ToString(dtexcel.Rows[i][5].ToString()) == "" || Convert.ToString(dtexcel.Rows[i][5].ToString()) == null)
                                                    command.Parameters.Add("@SaleRate", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                                else
                                                    command.Parameters.Add("@SaleRate", SqlDbType.Decimal).Value = Convert.ToDecimal(salerate);
                                                if (barcodestatus == "Our Barcode")
                                                    command.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = generateBarcode();
                                                if (batch == "OnlyBatch")
                                                {
                                                    command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = Convert.ToString(dtexcel.Rows[i][6].ToString());
                                                    command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = null;
                                                }
                                                else if (batch == "BatchAndExpire")
                                                {
                                                    command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = Convert.ToString(dtexcel.Rows[i][6].ToString());
                                                    try
                                                    {
                                                        command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = Convert.ToDateTime(dtexcel.Rows[i][7].ToString());
                                                    }
                                                    catch (Exception)
                                                    { }
                                                }
                                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                                                command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                                                command.Parameters.Add("@HasMandatoryCodeSeries", SqlDbType.Char).Value = "";
                                                command.ExecuteNonQuery();
                                                db.CloseConnection();
                                                command.Dispose();
                                            }
                                            catch (Exception)
                                            { db.CloseConnection(); }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                if (poflag == true)
                                                {
                                                    PurchaseMasterRepository pmasterRepo = new PurchaseMasterRepository();
                                                    PurchaseMaster pmaster = new PurchaseMaster();
                                                    pmaster.PorderNo = Convert.ToInt32(ponumber);
                                                    pmaster.PorderDate = dtpPorderdate.Value;
                                                    pmaster.Suppliername = "First stock";
                                                    pmaster.Vat = Convert.ToDecimal(txtvat.Text);
                                                    pmaster.TransactionYear = CommonMethod.TransactionYear;
                                                    pmaster.CompId = CommonMethod.CompId;
                                                    pmaster.StockIn = Convert.ToInt32(cmbGodownName.SelectedValue);
                                                    pmasterRepo.Add(pmaster);
                                                    pmasterRepo.Save();
                                                    POID = pmaster.Id;
                                                    poflag = false;
                                                }
                                            }
                                            catch (Exception)
                                            { }
                                            try
                                            {
                                                db.connect();
                                                SqlCommand command = new SqlCommand("SPPOnBillSaveUpdate", db.Connection);
                                                command.CommandType = CommandType.StoredProcedure;

                                                command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'P';
                                                command.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                                command.Parameters.Add("@event", SqlDbType.Int).Value = 1;
                                                command.Parameters.Add("@billtype", SqlDbType.Int).Value = 0;
                                                command.Parameters.Add("@BillNo", SqlDbType.Int).Value = POID;
                                                command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(dtexcel.Rows[i][0].ToString());
                                                command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(dtexcel.Rows[i][1].ToString());
                                                command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(dtexcel.Rows[i][2].ToString());
                                                command.Parameters.Add("@AlternateUnit", SqlDbType.NVarChar).Value = Convert.ToString(dtexcel.Rows[i][2].ToString());
                                                command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(d);
                                                command.Parameters.Add("@FreeQty", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                                command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = "";
                                                command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                                command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                                command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                                command.Parameters.Add("@Vat", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                                command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                                command.Parameters.Add("@StaticBarcode", SqlDbType.NVarChar).Value = Convert.ToString(dtexcel.Rows[i][4].ToString());
                                                if (Convert.ToString(dtexcel.Rows[i][5].ToString()) == "" || Convert.ToString(dtexcel.Rows[i][5].ToString()) == null)
                                                    command.Parameters.Add("@SaleRate", SqlDbType.Decimal).Value = Convert.ToDecimal(0);
                                                else
                                                    command.Parameters.Add("@SaleRate", SqlDbType.Decimal).Value = Convert.ToDecimal(salerate);
                                                if (barcodestatus == "Our Barcode")
                                                    command.Parameters.Add("@Barcode", SqlDbType.NVarChar).Value = generateBarcode();
                                                if (batch == "OnlyBatch")
                                                {
                                                    command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = Convert.ToString(dtexcel.Rows[i][6].ToString());
                                                    command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = null;
                                                }
                                                else if (batch == "BatchAndExpire")
                                                {
                                                    command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = Convert.ToString(dtexcel.Rows[i][6].ToString());
                                                    try
                                                    {
                                                        command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = Convert.ToDateTime(dtexcel.Rows[i][7].ToString());
                                                    }
                                                    catch (Exception)
                                                    { }
                                                }
                                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                                                command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                                                command.Parameters.Add("@HasMandatoryCodeSeries", SqlDbType.Char).Value = "";
                                                command.ExecuteNonQuery();
                                                db.CloseConnection();
                                                command.Dispose();
                                            }
                                            catch (Exception)
                                            { db.CloseConnection(); }
                                        }
                                    }
                                a: { }
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

        private void dtpPorderdate_KeyDown(object sender, KeyEventArgs e)
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
                    this.ActiveControl = txtpname;
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

        private void txtpname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbUnit;
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

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (txtBatchNo.Visible == true)
                        this.ActiveControl = txtBatchNo;
                    else
                        this.ActiveControl = btnAdd;
                }
            }
            catch (Exception)
            { }
        }

        private void btnAdd_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnGetAll;
            }
            catch (Exception)
            { }
        }

        private void btnGetAll_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
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
                    this.ActiveControl = dtpPorderdate;
            }
            catch (Exception)
            { }
        }

        private void txtQuantity_Enter(object sender, EventArgs e)
        {
            refreshFlag = false;
        }

        private void getProductType(string proname, string company, string unit)
        {
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

        private void txtBatchNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (dtpExpiry.Visible == true)
                    this.ActiveControl = dtpExpiry;
            }
        }

        private void dtpExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                this.ActiveControl = btnAdd;
        }

        public string GenerateandCheckNewBarcode()
        {
            string stringBarcode = "";
            try
            {
                RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
                Random random = new Random();
                var byteArray = new byte[4];
                provider.GetBytes(byteArray);
                var randomInteger = BitConverter.ToUInt32(byteArray, 0);
                stringBarcode = Convert.ToString(randomInteger) + Convert.ToString(random.Next(01, 99));
            a:
                {
                    if (stringBarcode.Length != 12)
                    {
                        if (stringBarcode.Length > 12)
                            stringBarcode = stringBarcode.Substring(0, 12);
                        else
                            stringBarcode = stringBarcode + Convert.ToString(random.Next(01, 99));
                    }
                }
                if (stringBarcode.Length != 12)
                    goto a;
                return stringBarcode;
            }
            catch (Exception)
            { return stringBarcode; }
        }

        private string generateBarcode()
        {
            string str_barCode = "";
            try
            {
                if (str_barCode == "" && barcodestatus == "Our Barcode")
                {
                    str_barCode = GenerateandCheckNewBarcode();
                a:
                    {
                        if (GvProductInfo.RowCount > 0)
                        {
                            for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                            {
                                if (GvProductInfo.Rows[i].Cells["Barcode"].Value.ToString() == str_barCode.ToString())
                                {
                                    str_barCode = string.Empty;
                                    str_barCode = GenerateandCheckNewBarcode();
                                }
                            }
                        }
                    }
                    PurchaseDetailRepository pDetailRepo = new PurchaseDetailRepository();
                    var data = pDetailRepo.GetAll().Where(t => t.Barcode == str_barCode).FirstOrDefault();

                    RepackDetailRepository RDetailRepo = new RepackDetailRepository();
                    var Rdata = RDetailRepo.GetAll().Where(t => t.Barcode == str_barCode).FirstOrDefault();
                    pDetailRepo.Dispose();
                    RDetailRepo.Dispose();
                    if (data != null || Rdata != null)
                    {
                        goto a;
                    }
                }
                return str_barCode;
            }
            catch (Exception)
            { return str_barCode; }
        }

        //private string generateBarcode()
        //{
        //    string str_barCode = "";
        //    try
        //    {
        //        if (str_barCode == "" && barcodestatus == "Our Barcode")
        //        {
        //            //string small_alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ", numbers = "1234567890", characters = numbers + small_alphabets;
        //            string otp = string.Empty, otp1 = string.Empty;

        //            Random random = new Random();
        //            for (int q = 0; q < 3; q++)
        //            {
        //                otp += Convert.ToString(random.Next(10, 20));
        //            }

        //            for (int q = 0; q < 3; q++)
        //            {
        //                otp1 += Convert.ToString(random.Next(34, 56));
        //            }

        //            str_barCode = otp + otp1;
        //            PurchaseDetailRepository pDetailRepo = new PurchaseDetailRepository();
        //            var data = pDetailRepo.GetAll().Where(t => t.Barcode == str_barCode).FirstOrDefault();

        //            RepackDetailRepository RDetailRepo = new RepackDetailRepository();
        //            var Rdata = RDetailRepo.GetAll().Where(t => t.Barcode == str_barCode).FirstOrDefault();

        //            pDetailRepo.Dispose();
        //            RDetailRepo.Dispose();
        //            if (data != null || Rdata != null)
        //            {
        //                otp = string.Empty;
        //                otp1 = string.Empty;
        //                str_barCode = string.Empty;
        //                MessageBox.Show("This Barcode no are duplicate.", "Warning");
        //                for (int q = 0; q < 3; q++)
        //                {
        //                    otp += Convert.ToString(random.Next(24, 67));
        //                }

        //                for (int q = 0; q < 3; q++)
        //                {
        //                    otp1 += Convert.ToString(random.Next(89, 01));
        //                }

        //                str_barCode = otp + otp1;

        //                PurchaseDetailRepository pDetailReponew = new PurchaseDetailRepository();
        //                var purdata = pDetailReponew.GetAll().Where(t => t.Barcode == str_barCode).FirstOrDefault();
        //                RepackDetailRepository RDetailReponew = new RepackDetailRepository();
        //                var Redata = RDetailReponew.GetAll().Where(t => t.Barcode == str_barCode).FirstOrDefault();
        //                RDetailReponew.Dispose();
        //                pDetailReponew.Dispose();
        //                if (data != null || Redata != null)
        //                    str_barCode = "";
        //            }
        //        }
        //        return str_barCode;
        //    }
        //    catch (Exception)
        //    {
        //        return str_barCode;
        //    }
        //}

        private void txtsalerate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtsalerate.Text == "")
                {
                    txtsalerate.Text = "0";
                    this.ActiveControl = txtsalerate;
                }
            }
            catch (Exception)
            { }
        }

        private void btnPrintBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                billList.Clear();
                for (int c = 0; c < GvProductInfo.Rows.Count; c++)
                {
                    if (Convert.ToBoolean(GvProductInfo.Rows[c].Cells["ChooseBarcodeProduct"].Value) == true)
                    {
                        int id = Convert.ToInt32(GvProductInfo.Rows[c].Cells["Id"].Value);
                        billList.Add(id);
                    }
                }
                if (billList.Count != 0)
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
                        MessageBox.Show("You have selected more labels to print than available labels,\n Please select another order.", "Printing Barcode");
                    else
                    {
                        RptBarcode barrpt = new RptBarcode(billList, 0, rownum, copyno, colno, "Purchase");
                        barrpt.ShowDialog();
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
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
                adp = new SqlDataAdapter("select ProductName,CompanyName,Unit,Quantity,StaticBarcode,Barcode as 'Our Barcode',SaleRate,BatchNo,Convert(Varchar(24),ExpireDate,103) as 'Expire Date' from PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo where m.Suppliername='First stock' and m.TAXType is null and m.CompId='" + CommonMethod.CompId + "' and m.TransactionYear='" + CommonMethod.TransactionYear + "' ", db.Connection);
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

                excelSheet.Cells[1, 1] = "Product Name";
                excelSheet.Cells[1, 2] = "Company Name";
                excelSheet.Cells[1, 3] = "Unit";
                excelSheet.Cells[1, 4] = "Quanity";
                excelSheet.Cells[1, 5] = "StaticBarcode";
                excelSheet.Cells[1, 6] = "OurBarcode";
                excelSheet.Cells[1, 7] = "SaleRate";
                excelSheet.Cells[1, 8] = "BatchNo";
                excelSheet.Cells[1, 9] = "Expire Date";

                excelSheet.Columns.AutoFit();

                excelworkBook.SaveAs("AddStock.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                excelworkBook.Close(true, misValue, misValue);
                excel.Quit();
                releaseObject(excelSheet);
                releaseObject(excelworkBook);
                releaseObject(excel);
                MessageBox.Show("Excel file created , you can find the file C:\\Users\\Documents\\AddStock.xls");
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

        private void GvProductInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 7)
                {
                    if ((MessageBox.Show("Are you sure to Remove this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                    {
                        deleteid.Add(Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value));
                        GvProductInfo.Rows.Remove(GvProductInfo.Rows[e.RowIndex]);
                    }
                }
                else if (e.ColumnIndex == 15)
                {
                    PurchaseDetailRepository pdetailRepo = new PurchaseDetailRepository();
                    var pdata = pdetailRepo.GetAll().Where(t => t.Id == Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value) && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                    if (pdata != null)
                    {
                        int PMasterId = Convert.ToInt32(pdata.PorderNo);
                        decimal qty = Convert.ToDecimal(GvProductInfo.Rows[e.RowIndex].Cells["Quantity"].Value);
                        totalqty = Convert.ToInt32(qty);
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
                                if (PMasterId != 0)
                                    ponumber = Convert.ToDecimal(PMasterId);
                                RptBarcode barrpt = new RptBarcode(ponumber, rownum, copyno, colno, "Purchase");
                                barrpt.ShowDialog();
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
                else if (e.ColumnIndex == 16)
                { }
                else
                {
                    txtpname.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) + "," + GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value + "," + GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value;
                    txtQuantity.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Quantity"].Value);
                    cmbUnit.Items.Clear();
                    cmbUnit.DisplayMember = "Text";
                    cmbUnit.ValueMember = "Value";
                    cmbUnit.Items.Add(new { Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value), Value = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value) });
                    if (Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["PurUnit"].Value) != Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value))
                        cmbUnit.Items.Add(new { Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["PurUnit"].Value), Value = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["PurUnit"].Value) });
                    cmbUnit.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["PurUnit"].Value);
                    colIndex = e.RowIndex;
                    HasMandatoryseries = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["MandatoryCodeSeries"].Value);
                    if (HasMandatoryseries == "Y")
                    {
                        isaddmandatory = true;
                    }
                    ProductRepository prodrepo = new ProductRepository();
                    ProductInformation proddata = new ProductInformation();
                    if (CommonMethod.commProduct == true)
                        proddata = prodrepo.GetAll().Where(t => t.ProductName == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) && t.ManufactureCompany == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value) && t.Uniti == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value)).FirstOrDefault();
                    else
                        proddata = prodrepo.GetAll().Where(t => t.ProductName == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) && t.ManufactureCompany == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value) && t.Uniti == Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value) && t.CompId == CommonMethod.CompId).FirstOrDefault();

                    if (proddata != null)
                        ProductCategory = proddata.Category;
                    try
                    {
                        if (GvProductInfo.Rows[e.RowIndex].Cells["BatchNo"].Value != null && GvProductInfo.Rows[e.RowIndex].Cells["BatchNo"].Value.ToString() != "")
                            txtBatchNo.Text = GvProductInfo.Rows[e.RowIndex].Cells["BatchNo"].Value.ToString();
                    }
                    catch (Exception)
                    { }

                    try
                    {
                        if (GvProductInfo.Rows[e.RowIndex].Cells["ExpiryDate"].Value != null && GvProductInfo.Rows[e.RowIndex].Cells["ExpiryDate"].Value.ToString() != "")
                            dtpExpiry.Value = Convert.ToDateTime(GvProductInfo.Rows[e.RowIndex].Cells["ExpiryDate"].Value);
                    }
                    catch (Exception)
                    { }

                    updateBarcode = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Barcode"].Value);
                    txtsalerate.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Salerate"].Value);
                    txtStaticBarcode.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["StaticBarcode"].Value);
                    if (proddata.BarcodeStatus == "Static Barcode" || proddata.BarcodeStatus == "Our Barcode")
                    {
                        if (proddata.BarcodeStatus == "Static Barcode")
                        {
                            txtsalerate.Visible = true;
                            txtStaticBarcode.Visible = true;
                            label7.Visible = true;
                            label8.Visible = true;
                        }
                        else if (proddata.BarcodeStatus == "Our Barcode")
                        {
                            txtsalerate.Visible = true;
                            label7.Visible = true;
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }
    }
}