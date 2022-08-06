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
    public partial class frmPurchaseOrderPlace : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        public static frmPurchaseOrderPlace purorderinfo;
        public int PurchaeId, printId, colIndex = -1, flag = 0, purmasterid;
        List<int> deleteid = new List<int>();
        string currentdate = Convert.ToString(DateTime.Now.ToString("M/d/yyyy"));
        ToolTip tooltipbtn = new ToolTip();

        public frmPurchaseOrderPlace()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            fillsupplier();
            fillmfgcomp();
            maxpurchaseorderid();
            btnUpdate.Enabled = false;
            deleteid.Clear();
            this.ActiveControl = txtsupplierpono;
        }

        public frmPurchaseOrderPlace(int PorderId)
        {
            PurchaeId = PorderId;
            InitializeComponent();
            fillsupplier();
            fillmfgcomp();
            deleteid.Clear();
            maxpurchaseorderid();
            cm.changedatetimepickerrange(this);
            LoadData(PurchaeId);
            this.ActiveControl = txtsupplierpono;
        }

        public void LoadData(int PorderId)
        {
            try
            {
                PurchaseOrderPlaceMasterRepository pmasterrepo = new PurchaseOrderPlaceMasterRepository();
                PurchaeId = PorderId;
                var PurchaeData = pmasterrepo.GetById(PorderId);
                txtporderno.Text = Convert.ToString(PurchaeData.PorderNo);
                txtsupplierpono.Text = Convert.ToString(PurchaeData.SupplierpoNo);
                dtpPorderdate.Text = PurchaeData.PorderDate.ToString();
                cmbsuppliername.Text = PurchaeData.Suppliername;
                printId = Convert.ToInt32(PurchaeData.PorderNo);
                PurchaseOrderPlaceDetailRepository pdetailrepo = new PurchaseOrderPlaceDetailRepository();
                List<PurchaseOrderPlaceDetail> PurchaseDetailData = new List<PurchaseOrderPlaceDetail>();
                PurchaseDetailData = pdetailrepo.GetAll().Where(t => t.PorderNo == PurchaeData.Id && t.PurchaseOrderPlaceMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseOrderPlaceMaster.CompId == CommonMethod.CompId).ToList();

                int i = 0;
                foreach (var item in PurchaseDetailData)
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
                        GvProductInfo.Rows[i].Cells["Quantity"].Value = Convert.ToDecimal(item.Quantity) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                    }
                    else
                        GvProductInfo.Rows[i].Cells["Quantity"].Value = Convert.ToDecimal(item.Quantity);
                    GvProductInfo.Rows[i].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                    i++;
                }
                btnprint.Enabled = true;
                btnSave.Enabled = false;
                btnUpdate.Enabled = true;
                if (btnUpdate.Enabled == true)
                {
                    SettingRepository settRepo = new SettingRepository();
                    Setting sdata = settRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (sdata.DeleteSaleBill == "True")
                        btnDelete.Enabled = true;
                    else
                        btnDelete.Enabled = false;
                }
                pmasterrepo.Dispose();
                pdetailrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void maxpurchaseorderid()
        {
            try
            {
                PurchaseOrderPlaceMasterRepository pmasterrepo = new PurchaseOrderPlaceMasterRepository();
                var mid = pmasterrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.PorderNo);
                if (mid == null)
                    txtporderno.Text = "1";
                else
                    txtporderno.Text = Convert.ToString(Convert.ToInt32(mid) + 1);
                pmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillsupplier()
        {
            try
            {
                SupplierRepository supplierrepo = new SupplierRepository();
                List<SupplierInformation> sdata = new List<SupplierInformation>();
                sdata.Clear();
                if (CommonMethod.commProduct == true)
                    sdata = supplierrepo.GetAll().OrderByDescending(t=>t.SupplierName).ToList();
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

        public void fillmfgcomp()
        {
            DataTable dt = null;
            if (CommonMethod.commProduct == true)
                dt = db.Gettable("Select Distinct(ManufactureCompany) from ProductInformation order by ManufactureCompany");
            else
                dt = db.Gettable("Select Distinct(ManufactureCompany) from ProductInformation where compid=" + CommonMethod.CompId + " order by ManufactureCompany");
            cmbcomanyname.DataSource = dt;
            cmbcomanyname.ValueMember = "ManufactureCompany";
            cmbcomanyname.DisplayMember = "ManufactureCompany";
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
                if (cmbsuppliername.Text != "Select" && txtpname.Text.Trim() != "" && txtQuantity.Text != "0")
                {
                    if (colIndex != -1)
                    {
                        string Proname = txtpname.Text.Trim().Split(',')[0];
                        string company = txtpname.Text.Trim().Split(',')[1];
                        string unit = txtpname.Text.Trim().Split(',')[2];

                        int i = GvProductInfo.RowCount;
                        GvProductInfo.Rows[colIndex].Cells["ProductNameg"].Value = Proname;
                        GvProductInfo.Rows[colIndex].Cells["Company"].Value = company;
                        GvProductInfo.Rows[colIndex].Cells["Unit"].Value = unit;
                        GvProductInfo.Rows[colIndex].Cells["PurUnit"].Value = cmbUnit.Text;
                        GvProductInfo.Rows[colIndex].Cells["Quantity"].Value = txtQuantity.Text;
                        GvProductInfo.Rows[colIndex].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                        txtpname.Text = "";
                        cmbUnit.Items.Clear();
                        txtQuantity.Text = "0";
                        colIndex = -1;
                    }
                    else
                    {
                        string Proname = txtpname.Text.Trim().Split(',')[0];
                        string company = txtpname.Text.Trim().Split(',')[1];
                        string unit = txtpname.Text.Trim().Split(',')[2];
                        GvProductInfo.Rows.Add();
                        int i = GvProductInfo.RowCount;
                        GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                        GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                        GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                        GvProductInfo.Rows[i - 1].Cells["PurUnit"].Value = cmbUnit.Text;
                        GvProductInfo.Rows[i - 1].Cells["Quantity"].Value = txtQuantity.Text;
                        GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                        txtpname.Text = "";
                        cmbUnit.Items.Clear();
                        txtQuantity.Text = "0";
                        this.ActiveControl = txtpname;
                    }
                }
                else
                {
                    if (cmbsuppliername.Text == "Select")
                    {
                        MessageBox.Show("Select Supplier Name.", "Warning");
                        this.ActiveControl = cmbsuppliername;
                    }
                    else if (txtpname.Text.Trim() == "")
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            clear();
            this.ActiveControl = txtporderno;
        }

        public void clear()
        {
            chkcompanyname.Checked = false;
            maxpurchaseorderid();
            fillsupplier();
            txtpname.Text = "";
            cmbUnit.Items.Clear();
            txtQuantity.Text = "0";
            colIndex = -1;
            GvProductInfo.Rows.Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            fillmfgcomp();
            txtsupplierpono.Text = "";
            gvProductRemStock.Visible = false;
            try
            { dtpPorderdate.Value = DateTime.Now.Date; }
            catch (Exception)
            { }
        }

        public void savepomaster()
        {
            try
            {
                PurchaseOrderPlaceMasterRepository pmasterrepo = new PurchaseOrderPlaceMasterRepository();
                PurchaseOrderPlaceMaster purchasedata = new PurchaseOrderPlaceMaster();
                maxpurchaseorderid();
                purchasedata.PorderNo = Convert.ToInt32(txtporderno.Text);
                purchasedata.PorderDate = dtpPorderdate.Value;
                purchasedata.SupplierpoNo = Convert.ToString(txtsupplierpono.Text.Trim());
                purchasedata.SupplierId = Convert.ToInt32(cmbsuppliername.SelectedValue);
                purchasedata.Suppliername = cmbsuppliername.Text;
                purchasedata.TransactionYear = CommonMethod.TransactionYear;
                purchasedata.CompId = CommonMethod.CompId;
                pmasterrepo.Add(purchasedata);
                pmasterrepo.Save();
                printId = Convert.ToInt32(purchasedata.PorderNo);
                purmasterid = Convert.ToInt32(purchasedata.Id);
                pmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                PurchaseOrderPlaceMasterRepository pmasterrepo = new PurchaseOrderPlaceMasterRepository();
                var BillId = pmasterrepo.GetAll().Where(t => t.PorderNo == Convert.ToInt32(txtporderno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (BillId != null)
                {
                    MessageBox.Show("Already Saved This Record.", "Warning");
                    this.ActiveControl = btnNew;
                }
                else
                {
                    if (cmbsuppliername.Text != "Select" && GvProductInfo.Rows.Count != 0)
                    {
                        //CustomerMaster table entry code
                        savepomaster();
                        for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                        {
                            try
                            {
                                db.connect();
                                SqlCommand command = new SqlCommand("SPPOPSaveUpdate", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                command.Parameters.Add("@event", SqlDbType.Int).Value = 1;
                                command.Parameters.Add("@BillNo", SqlDbType.Int).Value = purmasterid;
                                command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                command.Parameters.Add("@PurchaseUnit", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["PurUnit"].Value);
                                command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value);
                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                                command.ExecuteNonQuery();
                                db.CloseConnection();
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
                        }
                        GvProductInfo.Rows.Clear();
                        MessageBox.Show("Purchase Order Place saved.", "Success");
                        clear();
                        fillsupplier();
                        this.ActiveControl = btnprint;
                    }
                    else
                    {
                        if (cmbsuppliername.Text == "Select")
                        {
                            MessageBox.Show("Enter Supplier Name.", "Warning");
                            this.ActiveControl = cmbsuppliername;
                        }
                        else
                        {
                            MessageBox.Show("Please Add Record first.", "Warning");
                            this.ActiveControl = btnAdd;
                        }
                    }
                }
                pmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void fmPurchaseOrder_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                tooltipbtn.SetToolTip(this.rbtnaddproduct, "Click To Add Product");
                tooltipbtn.SetToolTip(this.btnSuppForm, "Click To Add Supplier");
                getProductList();
            }
            catch (Exception)
            { }
        }

        public void getProductList()
        {
            try
            {
                if (chkcompanyname.Checked == true)
                {
                    txtpname.Text = "";
                    string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(cnString1))
                    {
                        SqlCommand cmd = null;
                        if (CommonMethod.commProduct == true)
                            cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where ManufactureCompany='" + cmbcomanyname.Text + "'", con);
                        else
                            cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where ManufactureCompany='" + cmbcomanyname.Text + "' and compid=" + CommonMethod.CompId, con);
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
                        reader.Dispose();
                    }
                }
            }
            catch (Exception) { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                RptPurchaseorderPlace porderrpt = new RptPurchaseorderPlace(printId);
                porderrpt.ShowDialog();
            }
            catch (Exception)
            { }
        }

        public void updatemaster()
        {
            try
            {
                PurchaseOrderPlaceMasterRepository pmasterrepo = new PurchaseOrderPlaceMasterRepository();
                PurchaseOrderPlaceMaster purchasedata = new PurchaseOrderPlaceMaster();
                purchasedata.PorderNo = Convert.ToInt32(txtporderno.Text);
                purchasedata.PorderDate = dtpPorderdate.Value;
                purchasedata.SupplierpoNo = Convert.ToString(txtsupplierpono.Text);
                purchasedata.SupplierId = Convert.ToInt32(cmbsuppliername.SelectedValue);
                purchasedata.Suppliername = cmbsuppliername.Text;
                purchasedata.TransactionYear = CommonMethod.TransactionYear;
                purchasedata.CompId = CommonMethod.CompId;
                purchasedata.Id = PurchaeId;
                pmasterrepo.Edit(purchasedata);
                pmasterrepo.Save();
                pmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void chkcompanyname_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkcompanyname.Checked == true)
                {
                    txtpname.Text = "";
                    string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(cnString1))
                    {
                        SqlCommand cmd = null;
                        if (CommonMethod.commProduct == true)
                            cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where ManufactureCompany='" + cmbcomanyname.Text + "'", con);
                        else
                            cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where ManufactureCompany='" + cmbcomanyname.Text + "' and compid=" + CommonMethod.CompId, con);
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
                        reader.Dispose();
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbcomanyname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkcompanyname.Checked == true)
                {
                    txtpname.Text = "";
                    string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(cnString1))
                    {
                        SqlCommand cmd = null;
                        if (CommonMethod.commProduct == true)
                            cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where ManufactureCompany='" + cmbcomanyname.Text + "'", con);
                        else
                            cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where ManufactureCompany='" + cmbcomanyname.Text + "' and compid=" + CommonMethod.CompId, con);
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
                        reader.Dispose();
                    }
                }
            }
            catch (Exception)
            { }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
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
                {
                    btnSave_Click(null, null);
                    return true;
                }
                else if (btnUpdate.Enabled == true)
                {
                    btnUpdate_Click(null, null);
                    return true;
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private void txtpname_Leave(object sender, EventArgs e)
        {
            try
            {
                gvProductRemStock.Visible = false;
                if (flag == 1)
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
                            if (productdata == null)
                            {
                                MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                                txtpname.Text = "";
                                this.ActiveControl = txtpname;
                            }
                            else
                            {
                                if (colIndex != -1)
                                {
                                    if (txtpname.Text != Convert.ToString(GvProductInfo.Rows[colIndex].Cells["ProductNameg"].Value) + "," + GvProductInfo.Rows[colIndex].Cells["Company"].Value + "," + GvProductInfo.Rows[colIndex].Cells["Unit"].Value)
                                    {
                                        cmbUnit.Items.Clear();
                                        cmbUnit.DisplayMember = "Text";
                                        cmbUnit.ValueMember = "Value";
                                        cmbUnit.Items.Add(new { Text = productdata.Uniti, Value = productdata.Uniti });
                                        if (productdata.AlternateUnit != null && productdata.AlternateUnit != "")
                                            cmbUnit.Items.Add(new { Text = productdata.AlternateUnit, Value = productdata.AlternateUnit });
                                        cmbUnit.SelectedIndex = 0;
                                    }
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
            }
            catch (Exception)
            { }
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            try
            {
                frmPurchaseOrderList compinfo = new frmPurchaseOrderList();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void btnorderlist_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                purorderinfo = this;
                frmPurchaseOrderPlace p = new frmPurchaseOrderPlace();
                p.Visible = false;
                frmPurchaseOrderPlaceList allpro = new frmPurchaseOrderPlaceList();
                allpro.ShowDialog();
            }
            catch (Exception)
            { }
        }

        private void rbtnaddproduct_Click(object sender, EventArgs e)
        {
            try
            {
                frmProductInformation pro = new frmProductInformation();
                pro.ShowDialog();
                fmPurchaseOrder_Load(sender, EventArgs.Empty);
                this.ActiveControl = txtpname;
            }
            catch (Exception)
            { }
        }

        private void txtpname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtpname.Text != "")
                {
                    flag = 1;
                    gvProductRemStock.Visible = true;
                    gvProductRemStock.Rows.Clear();
                    string proname = "", company = "", unit = "";
                    if ((txtpname.Text).Contains(","))
                    {
                        try
                        {
                            proname = txtpname.Text.Trim().Split(',')[0];
                            company = txtpname.Text.Trim().Split(',')[1];
                            unit = txtpname.Text.Trim().Split(',')[2];

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
                                SqlDataReader reader = command.ExecuteReader();
                                
                                while (reader.Read())
                                {
                                    gvProductRemStock.Rows.Add();
                                    gvProductRemStock.Rows[0].Cells[0].Value = reader.GetString(1);
                                    gvProductRemStock.Rows[0].Cells[1].Value = reader.GetSqlDecimal(11).ToString();
                                }
                                db.CloseConnection();
                                reader.Dispose();
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
                        getProductList();
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbsuppliername_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcomanyname;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmSupplierInformation supplier = new frmSupplierInformation();
                        supplier.ShowDialog();
                        fillsupplier();
                        this.ActiveControl = cmbsuppliername;
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
                    this.ActiveControl = btnAdd;
            }
            catch (Exception)
            { }
        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                int porderno = printId;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
                saveFileDialog.Filter = "Document (*.pdf)|*.PDF";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = "Export PDF File To";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    db.connect();
                    int purchaseid = 0, suppcode = 0;
                    string custadd = "", mobileno = "", custname = "";
                    SqlDataAdapter da;
                    DataTable dt = db.GetTable("Select * from PurchaseOrderPlaceMaster Where PorderNo='" + porderno + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId='" + CommonMethod.CompId + "'");
                    if (dt.Rows.Count > 0)
                    {
                        purchaseid = Convert.ToInt32(dt.Rows[0][0].ToString());
                        suppcode = Convert.ToInt32(dt.Rows[0][3].ToString());
                    }

                    DataTable dt1 = new DataTable();
                    if (CommonMethod.commProduct == true)
                        dt1 = db.GetTable("Select * from SupplierInformation Where SupplierCode=" + suppcode + "  ");
                    else
                        dt1 = db.GetTable("Select * from SupplierInformation Where SupplierCode=" + suppcode + "  and CompId='" + CommonMethod.CompId + "' ");
                    if (dt1.Rows.Count > 0)
                    {
                        custname = dt1.Rows[0][2].ToString();
                        custadd = dt1.Rows[0][3].ToString();
                        mobileno = dt1.Rows[0][4].ToString();
                    }

                    da = new SqlDataAdapter("Select * from PurchaseOrderPlaceMaster Where PorderNo='" + porderno + "' and TransactionYear='" + CommonMethod.TransactionYear + "'  and CompId='" + CommonMethod.CompId + "'", db.Connection);
                    DSPurchaseOrderPlace ds = new DSPurchaseOrderPlace();
                    da.Fill(ds, "PurchaseOrderPlaceMaster");
                    da = new SqlDataAdapter("Select * from PurchaseOrderPlaceDetail d inner join PurchaseOrderPlaceMaster m on m.Id=d.PorderNo  Where d.PorderNo=" + purchaseid + " and m.CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.Fill(ds, "PurchaseOrderPlaceDetail");
                    da = new SqlDataAdapter("Select * from CompanyInformation where CompId='" + CommonMethod.CompId + "'", db.Connection);
                    da.Fill(ds, "CompanyInformation");
                    if (CommonMethod.commProduct == true)
                        da = new SqlDataAdapter("Select * from ProductInformation", db.Connection);
                    else
                        da = new SqlDataAdapter("Select * from ProductInformation where compid=" + CommonMethod.CompId, db.Connection);
                    da.Fill(ds, "ProductInformation");
                    PurchaseOrderPlaceCrystalReport poprpt = new PurchaseOrderPlaceCrystalReport();
                    poprpt.SetDataSource(ds);
                    poprpt.SetParameterValue("Suppliername", custname);
                    poprpt.SetParameterValue("Supplieraddress", custadd);
                    poprpt.SetParameterValue("mobileno", mobileno);
                    db.CloseConnection();
                    ds.Dispose();
                    da.Dispose();
                    dt.Dispose();
                    dt1.Dispose();
                    poprpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, saveFileDialog.FileName);
                    try
                    {
                        frmSendMail SendMail = new frmSendMail(saveFileDialog.FileName);
                        SendMail.WindowState = System.Windows.Forms.FormWindowState.Normal;
                        SendMail.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                        SendMail.ShowDialog();
                    }
                    catch (Exception)
                    { }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurred while CreatinG PDF File.");
                db.CloseConnection();
            }
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
                    fillsupplier();
                    this.ActiveControl = cmbsuppliername;
                }
            }
        }

        private void cmbcomanyname_Leave(object sender, EventArgs e)
        {
            try
            {
                if (cmbcomanyname.Text != "Select" && cmbcomanyname.Text != "")
                {
                    ManufactureCInfoRepository unitrepo = new ManufactureCInfoRepository();
                    ManuCompanyInfo unitdat = new ManuCompanyInfo();
                    if (CommonMethod.commProduct == true)
                        unitdat = unitrepo.GetAll().Where(t => t.CompanyName == cmbcomanyname.Text).FirstOrDefault();
                    else
                        unitdat = unitrepo.GetAll().Where(t => t.CompanyName == cmbcomanyname.Text && t.CompId == CommonMethod.CompId).FirstOrDefault();
                    if (unitdat == null)
                    {
                        MessageBox.Show("Invalid Company Name.", "Warning");
                        fillmfgcomp();
                        this.ActiveControl = cmbcomanyname;
                    }
                    unitrepo.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void txtsupplierpono_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpPorderdate;
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
                        fillmfgcomp();
                        this.ActiveControl = cmbcomanyname;
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
                        this.ActiveControl = cmbUnit;
                    }
                }
            }
            catch (Exception) { }
        }

        private void dtpPorderdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbsuppliername;
            }
            catch (Exception) { }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbsuppliername.Text != "Select" && GvProductInfo.Rows.Count != 0)
                {
                    updatemaster();
                    if (deleteid.Count != 0)
                    {
                        for (int x = 0; x < deleteid.Count; x++)
                        {
                            try
                            {
                                PurchaseOrderPlaceDetailRepository pdetailrepo2 = new PurchaseOrderPlaceDetailRepository();
                                PurchaseOrderPlaceDetail POPD = pdetailrepo2.GetById(deleteid[x]);
                                pdetailrepo2.Remove(POPD);
                                pdetailrepo2.Save();
                            }
                            catch (Exception)
                            { }
                        }
                    }
                    for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                    {
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPPOPSaveUpdate", db.Connection);
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
                            command.Parameters.Add("@BillNo", SqlDbType.Int).Value = PurchaeId;
                            command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                            command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                            command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                            command.Parameters.Add("@PurchaseUnit", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["PurUnit"].Value);
                            command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value);
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                            command.ExecuteNonQuery();
                            db.CloseConnection();
                        }
                        catch (Exception)
                        { db.CloseConnection(); }
                    }
                    clear();
                    MessageBox.Show("Purchase Order Place Updated.", "Success");
                    this.ActiveControl = btnprint;
                    deleteid.Clear();
                }
                else
                {
                    if (GvProductInfo.Rows.Count == 0)
                    {
                        MessageBox.Show("Please Add Record First.", "Warning");
                        this.ActiveControl = btnAdd;
                    }
                    else
                    {
                        MessageBox.Show("Enter Supplier Name.", "Warning");
                        this.ActiveControl = cmbsuppliername;
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
                if ((MessageBox.Show("Are you sure to Delete this Purchase Order Place ?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    PurchaseOrderPlaceMasterRepository pmasterrepo = new PurchaseOrderPlaceMasterRepository();
                    var POPlaceData = pmasterrepo.GetAll().Where(t => t.Id == PurchaeId && t.SupplierId == Convert.ToInt32(cmbsuppliername.SelectedValue) && t.CompId == CommonMethod.CompId && t.TransactionYear == t.TransactionYear).FirstOrDefault();
                    if (POPlaceData != null)
                    {
                        // add details for history table
                        try
                        {
                            DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                            DeleteHistoryTable historydata = new DeleteHistoryTable();
                            historydata.DeleteType = "Purchase Order Place";
                            historydata.BillNo = Convert.ToString(POPlaceData.PorderNo);
                            historydata.SupplierOrCustomerName = Convert.ToString(POPlaceData.Suppliername);
                            historydata.Amount = Convert.ToDecimal(0);
                            historydata.CurrentDate = DateTime.Now.Date;
                            historydata.BillDate = Convert.ToDateTime(POPlaceData.PorderDate);
                            historydata.CompId = CommonMethod.CompId;
                            historydata.TransactionYear = CommonMethod.TransactionYear;
                            historyRepo.Add(historydata);
                            historyRepo.Save();
                        }
                        catch (Exception) { }
                        db.connect();
                        DataTable dt1 = db.GetTable("delete from PurchaseOrderPlaceDetail where PorderNo='" + PurchaeId + "' ");
                        DataTable dt2 = db.GetTable("delete from PurchaseOrderPlaceMaster where Id='" + PurchaeId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                        MessageBox.Show("Record Deleted Successfully");
                        dt1.Dispose();
                        dt2.Dispose();
                        db.CloseConnection();
                        GvProductInfo.Rows.Clear();
                        btnDelete.Enabled = false;
                        clear();
                        this.ActiveControl = txtsupplierpono;
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
                //if (e.ColumnIndex == 6)
                {
                    if (GvProductInfo.Rows.Count != 1 && btnSave.Enabled == false)
                    {
                        string pname1 = GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value.ToString();
                        string cname1 = GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value.ToString();
                        string uname1 = GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value.ToString();

                        if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        {
                            deleteid.Add(Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value));
                            GvProductInfo.Rows.Remove(GvProductInfo.Rows[e.RowIndex]);
                            colIndex = -1;
                            txtpname.Text = "";
                            txtQuantity.Text = "0";
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
                        }
                    }
                    else
                        MessageBox.Show("This Invoice has only 1 product." + Environment.NewLine + "You have to delete full Order.", "Warning");
                }
                else
                {
                    colIndex = e.RowIndex;
                    txtpname.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) + "," + GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value + "," + GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value;
                    cmbUnit.Items.Clear();
                    cmbUnit.DisplayMember = "Text";
                    cmbUnit.ValueMember = "Value";
                    cmbUnit.Items.Add(new { Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value), Value = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value) });
                    if (Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["PurUnit"].Value) != Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value))
                        cmbUnit.Items.Add(new { Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["PurUnit"].Value), Value = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["PurUnit"].Value) });
                    cmbUnit.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["PurUnit"].Value);
                    txtQuantity.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Quantity"].Value);
                    this.ActiveControl = txtpname;
                }
            }
            catch (Exception)
            { }
        }

        private void btnSuppForm_Click(object sender, EventArgs e)
        {
            try
            {
                frmSupplierInformation compinfo = new frmSupplierInformation();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
                fillsupplier();
                this.ActiveControl = cmbsuppliername;
            }
            catch (Exception)
            { }
        }
    }
}