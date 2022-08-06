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
    public partial class frmRecieptChallan : Form
    {
        public static frmRecieptChallan delichalleninfo;
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        List<int> deleteid = new List<int>();
        List<int> deletePoid = new List<int>();
        public int ChallanId, colIndex = -1, dcno, codeflag = 0, id = 0, pdid, productentrytype = 0;
        public string ProductCategory, maxpoid;

        public frmRecieptChallan()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxchallanid();
            fillcompany();
            customerdata();
            fillGodownName();
            deleteid.Clear();
            deletePoid.Clear();
            btnsave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        public frmRecieptChallan(int ChallanNo)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            fillcompany();
            customerdata();
            fillGodownName();
            deleteid.Clear();
            deletePoid.Clear();
            GetChallanData(ChallanNo);
            this.ActiveControl = cmbcustomername;
        }

        public void fillGodownName()
        {
            try
            {
                GodownMasterRepository GDRepo = new GodownMasterRepository();
                List<GodownMaster> GDData = new List<GodownMaster>();
                GDData.Clear();
                if (CommonMethod.commProduct == true)
                    GDData = GDRepo.GetAll().OrderByDescending(t => t.GodownName).ToList();
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

        public void GetChallanData(int challanno)
        {
            try
            {
                RecieptChallanMasterRepository challanmasterrepo = new RecieptChallanMasterRepository();
                var challanmasterData = challanmasterrepo.GetById(challanno);
                int dcstatus = Convert.ToInt32(challanmasterData.Status);
                if (dcstatus == 0)
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
                maxpoid = challanmasterData.POrderNo.ToString();
                PurchaseDetailRepository pdrr = new PurchaseDetailRepository();
                try
                {
                    id = pdrr.GetAll().Where(t => t.PorderNo == t.PurchaseMaster.Id && t.PurchaseMaster.PorderNo == Convert.ToInt32(maxpoid) && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId).FirstOrDefault().Id;
                }
                catch (Exception)
                {
                    id = 0;
                }
                txtchallanno.Text = Convert.ToString(challanmasterData.RecieptChallanNo);
                dtpchallandate.Value = challanmasterData.RecieptChallanDate.Value;
                cmbcustomername.Text = challanmasterData.SupplierName;
                PurchaseMasterRepository PurMRepo = new PurchaseMasterRepository();
                PurchaseMaster PurMData = new PurchaseMaster();
                PurMData = PurMRepo.GetAll().Where(t => t.PorderNo == Convert.ToInt32(maxpoid) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (PurMData != null)
                {
                    if (PurMData.StockIn != null)
                    {
                        cmbGodownName.SelectedValue = PurMData.StockIn;
                        cmbGodownName.Enabled = false;
                    }
                }
                ChallanId = challanmasterData.Id;
                dcno = Convert.ToInt32(txtchallanno.Text);
                var challanDetailData = challanmasterData.RecieptChalanDetails;
                int i = 0;
                foreach (var item in challanDetailData)
                {
                    GvProductInfo.Rows.Add();
                    GvProductInfo.Rows[i].Cells["Idg"].Value = item.Id;
                    GvProductInfo.Rows[i].Cells["ProductNameg"].Value = item.ProductName;
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
                            productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit).FirstOrDefault();
                        else
                            productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit && t.CompId == CommonMethod.CompId).FirstOrDefault();
                        GvProductInfo.Rows[i].Cells["Quantityg"].Value = Convert.ToDecimal(item.Quantity) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                    }
                    else
                        GvProductInfo.Rows[i].Cells["Quantityg"].Value = Convert.ToDouble(item.Quantity);
                    GvProductInfo.Rows[i].Cells["Code"].Value = item.Code;
                    GvProductInfo.Rows[i].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                    GvProductInfo.Rows[i].Cells["PDIDg"].Value = item.PDetailID;
                    if (!string.IsNullOrEmpty(Convert.ToString(item.BatchNo)))
                        GvProductInfo.Rows[i].Cells["BatchNo"].Value = item.BatchNo;
                    if (!string.IsNullOrEmpty(Convert.ToString(item.ExpireDate)))
                        GvProductInfo.Rows[i].Cells["ExpiryDate"].Value = item.ExpireDate;
                    i++;
                }
                challanmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void maxchallanid()
        {
            try
            {
                RecieptChallanMasterRepository challanmasterrepo = new RecieptChallanMasterRepository();
                var maxid = challanmasterrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.RecieptChallanNo);
                if (maxid == null)
                    txtchallanno.Text = "1";
                else
                    txtchallanno.Text = Convert.ToString(Convert.ToInt32(maxid) + 1);
                challanmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillcompany()
        {
            try
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
            catch (Exception)
            { }
        }

        private void frmRecieptChallan_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
                if (chkcompanyname.Checked == true)
                {
                    txtProductname.Text = "";
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
                        txtProductname.AutoCompleteCustomSource = MyCollection;
                        con.Close();
                        reader.Dispose();
                    }
                }
                else
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
                }
            }
            catch (Exception)
            { }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbcustomername.SelectedIndex > 0)
                {
                    if (txtProductname.Text.Trim() != "" && txtQuantity.Text != "" && cmbcomanyname.Text != "")
                    {
                        if (productentrytype == 1) //krushi project with batch and expiry 
                        {
                            if (txtBatchNo.Text.Trim() == "")
                            {
                                MessageBox.Show("Enter Batch Number.", "Warning");
                                pnlIntegratedDtls.Visible = true;
                                this.ActiveControl = txtBatchNo;
                                goto a;
                            }
                            if (dtpExpiry.Value.Date < DateTime.Now || dtpExpiry.Value.ToString().Trim() == "")
                            {
                                MessageBox.Show("Expiry Date must be greater than Todays Date.", "Warning");
                                pnlIntegratedDtls.Visible = true;
                                this.ActiveControl = dtpExpiry;
                                goto a;
                            }
                        }
                        else if (productentrytype == 2) //krushi project with batch
                        {
                            if (txtBatchNo.Text.Trim() == "")
                            {
                                MessageBox.Show("Enter Batch Number.", "Warning");
                                pnlIntegratedDtls.Visible = true;
                                this.ActiveControl = txtBatchNo;
                                goto a;
                            }
                        }
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
                        if (colIndex != -1)
                        {
                            string Proname = txtProductname.Text.Trim().Split(',')[0];
                            string company = txtProductname.Text.Trim().Split(',')[1];
                            string unit = txtProductname.Text.Trim().Split(',')[2];

                            GvProductInfo.Rows[colIndex].Cells["ProductNameg"].Value = Proname;
                            GvProductInfo.Rows[colIndex].Cells["Company"].Value = company;
                            GvProductInfo.Rows[colIndex].Cells["Unit"].Value = unit;
                            GvProductInfo.Rows[colIndex].Cells["SaleUnit"].Value = cmbUnit.Text;
                            if (txtQuantity.Text == "")
                                GvProductInfo.Rows[colIndex].Cells["Quantityg"].Value = 0;
                            else
                                GvProductInfo.Rows[colIndex].Cells["Quantityg"].Value = txtQuantity.Text;
                            if (ProductCategory == "Motor")
                                GvProductInfo.Rows[colIndex].Cells["Code"].Value = dtgvcode.Rows[0].Cells[0].Value;
                            else
                                GvProductInfo.Rows[colIndex].Cells["Code"].Value = GvProductInfo.Rows[colIndex].Cells["Code"].Value;
                            GvProductInfo.Rows[colIndex].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                            if (productentrytype == 1 || productentrytype == 2)
                            {
                                GvProductInfo.Rows[colIndex].Cells["BatchNo"].Value = txtBatchNo.Text;
                                if (productentrytype != 2) //only batch
                                    GvProductInfo.Rows[colIndex].Cells["ExpiryDate"].Value = dtpExpiry.Value;
                            }
                            txtProductname.Text = "";
                            cmbUnit.Items.Clear();
                            txtQuantity.Text = "";
                            if (productentrytype == 1 || productentrytype == 2)
                            {
                                txtBatchNo.Text = "";
                                dtpExpiry.Value = DateTime.Now.Date;
                            }
                            this.ActiveControl = txtProductname;
                            colIndex = -1;
                        }
                        else
                        {
                            string Proname = txtProductname.Text.Trim().Split(',')[0];
                            string company = txtProductname.Text.Trim().Split(',')[1];
                            string unit = txtProductname.Text.Trim().Split(',')[2];
                            if (ProductCategory == "Motor")
                            {
                                for (int l = 0; l < dtgvcode.RowCount; l++)
                                {
                                    GvProductInfo.Rows.Add();
                                    int i = GvProductInfo.RowCount;
                                    GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                                    GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                                    GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                                    GvProductInfo.Rows[i - 1].Cells["SaleUnit"].Value = cmbUnit.Text;
                                    if (txtQuantity.Text == "")
                                        GvProductInfo.Rows[i - 1].Cells["Quantityg"].Value = 0;
                                    else
                                        GvProductInfo.Rows[i - 1].Cells["Quantityg"].Value = 1;
                                    GvProductInfo.Rows[i - 1].Cells["Code"].Value = dtgvcode.Rows[l].Cells[0].Value;
                                    GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                }
                            }
                            else
                            {
                                GvProductInfo.Rows.Add();
                                int i = GvProductInfo.RowCount;
                                GvProductInfo.Rows[i - 1].Cells["ProductNameg"].Value = Proname;
                                GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                                GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                                GvProductInfo.Rows[i - 1].Cells["SaleUnit"].Value = cmbUnit.Text;
                                if (txtQuantity.Text == "")
                                    GvProductInfo.Rows[i - 1].Cells["Quantityg"].Value = 0;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["Quantityg"].Value = txtQuantity.Text;
                                GvProductInfo.Rows[i - 1].Cells["Code"].Value = "";
                                GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                if (productentrytype == 1 || productentrytype == 2)  //nilophar 10032018
                                {
                                    GvProductInfo.Rows[i - 1].Cells["BatchNo"].Value = txtBatchNo.Text;
                                    if (productentrytype != 2) //only batch
                                        GvProductInfo.Rows[i - 1].Cells["ExpiryDate"].Value = dtpExpiry.Value;
                                }
                            }
                            txtProductname.Text = "";
                            cmbUnit.Items.Clear();
                            txtQuantity.Text = "";
                            if (productentrytype == 1 || productentrytype == 2)
                            {
                                txtBatchNo.Text = "";
                                dtpExpiry.Value = DateTime.Now.Date;
                            }
                            this.ActiveControl = txtProductname;
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
                        else if (txtQuantity.Text == "")
                        {
                            MessageBox.Show("Enter Quantity.", "Warning");
                            this.ActiveControl = txtQuantity;
                        }
                        else
                        {
                            MessageBox.Show("Enter Company Name.", "Warning");
                            this.ActiveControl = cmbcomanyname;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Choose Supplier Name.", "Warning");
                    this.ActiveControl = cmbcustomername;
                }
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            colIndex = -1;
            txtchallanno.Text = "";
            GvProductInfo.Rows.Clear();
            fillcompany();
            deleteid.Clear();
            deletePoid.Clear();
            txtProductname.Text = "";
            cmbUnit.Items.Clear();
            txtQuantity.Text = "";
            this.ActiveControl = txtProductname;
            customerdata();
            fillGodownName();
            maxchallanid();
            txtBatchNo.Text = "";
            dtpExpiry.Value = DateTime.Now.Date;
            btnsave.Enabled = true;
            btnUpdate.Enabled = false;
            try
            { dtpchallandate.Value = DateTime.Now.Date; }
            catch (Exception)
            { }
        }

        private void btnnew_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        public void maxpurchaseorderid()
        {
            try
            {
                PurchaseMasterRepository pmasterrepo = new PurchaseMasterRepository();
                var mid = pmasterrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.PorderNo);
                if (mid == null)
                    maxpoid = "1";
                else
                    maxpoid = Convert.ToString(Convert.ToInt32(mid) + 1);
                pmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                RecieptChallanMasterRepository challanmasterrepo = new RecieptChallanMasterRepository();
                RecieptChallanMaster challanmaster = new RecieptChallanMaster();
                var challanno = challanmasterrepo.GetAll().Where(t => t.RecieptChallanNo == Convert.ToInt32(txtchallanno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
                if (challanno != null)
                {
                    MessageBox.Show("Already Saved This Record.", "Warning");
                    this.ActiveControl = btnnew;
                }
                else
                {
                    if (cmbcustomername.SelectedIndex > 0)
                    {
                        if (GvProductInfo.Rows.Count != 0)
                        {
                            maxpurchaseorderid();
                            PurchaseMasterRepository pmr = new PurchaseMasterRepository();
                            PurchaseMaster pmdata = new PurchaseMaster();
                            pmdata.PorderNo = Convert.ToInt32(maxpoid);
                            pmdata.PorderDate = dtpchallandate.Value;
                            pmdata.Suppliername = cmbcustomername.Text;
                            pmdata.SupplierId = Convert.ToInt32(cmbcustomername.SelectedValue);
                            pmdata.Vat = Convert.ToDecimal(0);
                            pmdata.TransactionYear = CommonMethod.TransactionYear;
                            pmdata.CompId = CommonMethod.CompId;
                            pmdata.StockIn = Convert.ToInt32(cmbGodownName.SelectedValue);
                            pmr.Add(pmdata);
                            pmr.Save();

                            int POId = pmdata.Id;
                            maxchallanid();
                            challanmaster.RecieptChallanNo = Convert.ToInt32(txtchallanno.Text);
                            challanmaster.RecieptChallanDate = Convert.ToDateTime(dtpchallandate.Value.ToShortDateString());
                            challanmaster.SupplierId = Convert.ToInt32(cmbcustomername.SelectedValue);
                            challanmaster.SupplierName = cmbcustomername.Text;
                            challanmaster.Status = 1;
                            challanmaster.TransactionYear = CommonMethod.TransactionYear;
                            challanmaster.CompId = CommonMethod.CompId;
                            challanmaster.POrderNo = Convert.ToInt32(maxpoid);
                            challanmasterrepo.Add(challanmaster);
                            challanmasterrepo.Save();
                            dcno = Convert.ToInt32(txtchallanno.Text);

                            RecieptChalanDetailRepository challandetailrepo = new RecieptChalanDetailRepository();
                            RecieptChalanDetail challandetail = new RecieptChalanDetail();
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
                                    command.Parameters.Add("@billtype", SqlDbType.Int).Value = 1;
                                    command.Parameters.Add("@BillNo", SqlDbType.Int).Value = POId;
                                    command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                    command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                    command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                    command.Parameters.Add("@AlternateUnit", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["SaleUnit"].Value);
                                    command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantityg"].Value);
                                    command.Parameters.Add("@FreeQty", SqlDbType.Decimal).Value = 0;
                                    command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                                    command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = 0;
                                    command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = 0;
                                    command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = 0;
                                    command.Parameters.Add("@Vat", SqlDbType.Decimal).Value = 0;
                                    command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = 0;
                                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                    command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                                    if (productentrytype == 1 || productentrytype == 2)
                                    {
                                        command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                                        if (productentrytype != 2)
                                            command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = Convert.ToDateTime(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value);
                                    }
                                    command.ExecuteNonQuery();
                                    db.CloseConnection();
                                    PurchaseDetailRepository pdr = new PurchaseDetailRepository();
                                    pdid = pdr.GetAll().LastOrDefault().Id;
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                                try
                                {
                                    db.connect();
                                    SqlCommand command1 = new SqlCommand("SPRecieptChalanSaveUpdate", db.Connection);
                                    command1.CommandType = CommandType.StoredProcedure;
                                    command1.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                    command1.Parameters.Add("@event", SqlDbType.Int).Value = 1;
                                    command1.Parameters.Add("@BillNo", SqlDbType.Int).Value = challanmaster.Id;
                                    command1.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                    command1.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                    command1.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                    command1.Parameters.Add("@SaleUnit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["SaleUnit"].Value);
                                    command1.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantityg"].Value);
                                    command1.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                                    command1.Parameters.Add("@PDetailID", SqlDbType.Int).Value = pdid;
                                    if (productentrytype == 1 || productentrytype == 2)
                                    {
                                        command1.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                                        if (productentrytype != 2)
                                            command1.Parameters.Add("@ExpiryDate", SqlDbType.DateTime).Value = Convert.ToDateTime(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value);
                                    }
                                    command1.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                                    command1.ExecuteNonQuery();
                                    db.CloseConnection();
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                            }
                            MessageBox.Show("Receipt Challan Saved.", "Success");
                            GvProductInfo.Rows.Clear();
                            clear();
                            this.ActiveControl = btnprint;
                            challandetailrepo.Dispose();
                        }
                        else
                        {
                            MessageBox.Show("Add Products Information.", "Warning");
                            this.ActiveControl = txtProductname;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Choose Supplier Name.", "Warning");
                        this.ActiveControl = cmbcustomername;
                    }
                }
                challanmasterrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        public void customerdata()
        {
            try
            {
                SupplierRepository custrepo = new SupplierRepository();
                List<SupplierInformation> customerdata = new List<SupplierInformation>();
                customerdata.Clear();
                if (CommonMethod.commProduct == true)
                    customerdata = custrepo.GetAll().OrderByDescending(t => t.SupplierName).ToList();
                else
                    customerdata = custrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.SupplierName).ToList();
                customerdata.Add(new SupplierInformation { SupplierName = "Select", SupplierCode = 0 });
                customerdata.Reverse();
                cmbcustomername.DataSource = customerdata;
                cmbcustomername.ValueMember = "SupplierCode";
                cmbcustomername.DisplayMember = "SupplierName";
                custrepo.Dispose();
                cmbcustomername.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                RptReceiptChallen challanreport = new RptReceiptChallen(dcno);
                challanreport.ShowDialog();
                challanreport.Dispose();
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
                    txtProductname.Text = "";
                    string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(cnString1))
                    {
                        SqlCommand cmd = null;
                        if (CommonMethod.commProduct == true)
                            cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where ManufactureCompany='" + cmbcomanyname.Text, con);
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
                        txtProductname.AutoCompleteCustomSource = MyCollection;
                        con.Close();
                        reader.Dispose();
                    }
                }
                else
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
                    txtProductname.Text = "";
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
                        txtProductname.AutoCompleteCustomSource = MyCollection;
                        con.Close();
                        reader.Dispose();
                    }
                }
                else
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
                }
            }
            catch (Exception)
            { }
        }

        private void txtProductname_Leave(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Visible = false;
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
                            if (colIndex == -1)
                            {
                                cmbUnit.Items.Clear();
                                cmbUnit.DisplayMember = "Text";
                                cmbUnit.ValueMember = "Value";
                                cmbUnit.Items.Add(new { Text = productdata.Uniti, Value = productdata.Uniti });
                                if (productdata.AlternateUnit != null && productdata.AlternateUnit != "")
                                    cmbUnit.Items.Add(new { Text = productdata.AlternateUnit, Value = productdata.AlternateUnit });
                                cmbUnit.SelectedIndex = 0;
                            }
                            ProductCategory = productdata.Category;
                            GvProductInfo.Columns[9].Visible = false;
                            GvProductInfo.Columns[10].Visible = false;
                            productentrytype = Convert.ToInt32(productdata.ProductType);  // allinone standard
                            if (productdata.ProductType == 0 && productdata.WithBatchAndExpiry == "N" && productdata.WithBatch == "N")
                                productentrytype = 0;
                            else if (productdata.ProductType == 0 && productdata.WithBatchAndExpiry == "Y") //krushi with batch and expiry
                            {
                                productentrytype = 1;
                                GvProductInfo.Columns[9].Visible = true;
                                GvProductInfo.Columns[10].Visible = true;
                            }
                            else if (productdata.ProductType == 0 && productdata.WithBatch == "Y") //krushi only batch
                            {
                                productentrytype = 2;
                                GvProductInfo.Columns[9].Visible = true;
                                GvProductInfo.Columns[10].Visible = true;
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
                        if (productentrytype == 6)
                        {
                            MessageBox.Show("This Product Name Is Assembly Product " + Environment.NewLine + " they can't add in receipt challan.", "Warning");
                            txtProductname.Text = "";
                            this.ActiveControl = txtProductname;
                        }
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

        private void btndeliverychallenlist_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                delichalleninfo = this;
                frmRecieptChallan p = new frmRecieptChallan();
                p.Visible = false;
                frmRecieptChallanList delichallenlist = new frmRecieptChallanList();
                delichallenlist.ShowDialog();
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
                        frmSupplierInformation supplier = new frmSupplierInformation();
                        supplier.ShowDialog();
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
                    this.ActiveControl = txtQuantity;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmProductInformation product = new frmProductInformation();
                        product.ShowDialog();
                        frmRecieptChallan_Load(null, null);
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
                    {
                        if (productentrytype == 1 || productentrytype == 2)
                            pnlIntegratedDtls.Visible = true;
                        goto s;
                    }
                }
                if (colIndex == -1)
                {
                    if (ProductCategory == "Motor")
                        goto t;
                    else
                    {
                        if (productentrytype == 1 || productentrytype == 2)
                            pnlIntegratedDtls.Visible = true;
                        goto s;
                    }
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
                { }
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

        private void cmbcustomername_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                label6.Text = "";
                try
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPSuppRemainingPayment", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                    command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "SuppRem";
                    command.Parameters.Add("@SupplierID", SqlDbType.Int).Value = Convert.ToInt32(cmbcustomername.SelectedValue);
                    DataTable dt1 = new DataTable();
                    dt1.Load(command.ExecuteReader());
                    object sumtotAmount = new object();
                    sumtotAmount = dt1.Compute("Sum(TotalReaminingAmount)", "");
                    db.CloseConnection();
                    label6.Text = Convert.ToString(sumtotAmount);
                    dt1.Dispose();
                }
                catch (Exception)
                { db.CloseConnection(); }
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
                        if (dtgvcode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != dt.Rows[i][0].ToString())
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
                                    this.ActiveControl = dtgvcode;
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
                    codeflag = 1;
                    dtgvcode.Visible = true;
                    this.ActiveControl = dtgvcode;
                    dtgvcode.CurrentCell = dtgvcode[e.ColumnIndex, e.RowIndex];
                    dtgvcode.CurrentCell.Selected = true;
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

        private void cmbcustomername_Leave(object sender, EventArgs e)
        {
            if (cmbcustomername.Text != "" && cmbcustomername.Text != "Select")
            {
                CommonMethod cm = new CommonMethod();
                if (cm.ValidSupplier(cmbcustomername.Text) == 1)
                { }
                else
                {
                    MessageBox.Show("Invalid Supplier.", "Warning");
                    customerdata();
                    this.ActiveControl = cmbcustomername;
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
                    var unitdat = unitrepo.GetAll().Where(t => t.CompanyName == cmbcomanyname.Text).FirstOrDefault();
                    if (unitdat == null)
                    {
                        MessageBox.Show("Invalid Company Name.", "Warning");
                        fillcompany();
                        this.ActiveControl = cmbcomanyname;
                    }
                    unitrepo.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void dtpchallandate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcustomername;
            }
            catch (Exception)
            { }
        }

        private void cmbcomanyname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtProductname;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmManufactureCompanyInfo company = new frmManufactureCompanyInfo();
                        company.ShowDialog();
                        fillcompany();
                        this.ActiveControl = cmbcomanyname;
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

        private void btnPnlClose_Click(object sender, EventArgs e)
        {
            pnlIntegratedDtls.Visible = false;
        }

        private void dtpExpiry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                pnlIntegratedDtls.Visible = false;
                this.ActiveControl = btnAdd;
            }
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
                else
                    productentrytype = 0;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbcustomername.SelectedIndex > 0)
                {
                    if (GvProductInfo.Rows.Count != 0)
                    {
                        PurchaseMasterRepository pmr1 = new PurchaseMasterRepository();
                        int pmid = pmr1.GetAll().Where(t => t.PorderNo == Convert.ToInt32(maxpoid) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault().Id;
                        PurchaseMasterRepository pmr = new PurchaseMasterRepository();
                        PurchaseMaster pmdata = new PurchaseMaster();
                        pmdata.PorderNo = Convert.ToInt32(maxpoid);
                        pmdata.PorderDate = dtpchallandate.Value;
                        pmdata.Suppliername = cmbcustomername.Text;
                        pmdata.SupplierId = Convert.ToInt32(cmbcustomername.SelectedValue);
                        pmdata.Vat = Convert.ToDecimal(0);
                        pmdata.TransactionYear = CommonMethod.TransactionYear;
                        pmdata.CompId = CommonMethod.CompId;
                        pmdata.StockIn = Convert.ToInt32(cmbGodownName.SelectedValue);
                        pmdata.Id = pmid;
                        pmr.Edit(pmdata);
                        pmr.Save();

                        RecieptChallanMasterRepository challanmasterrepo = new RecieptChallanMasterRepository();
                        RecieptChallanMaster challanmasterdata = new RecieptChallanMaster();
                        challanmasterdata.RecieptChallanNo = Convert.ToInt32(txtchallanno.Text);
                        challanmasterdata.RecieptChallanDate = Convert.ToDateTime(dtpchallandate.Value.ToShortDateString());
                        challanmasterdata.SupplierId = Convert.ToInt32(cmbcustomername.SelectedValue);
                        challanmasterdata.SupplierName = cmbcustomername.Text;
                        challanmasterdata.Status = 1;
                        challanmasterdata.TransactionYear = CommonMethod.TransactionYear;
                        challanmasterdata.CompId = CommonMethod.CompId;
                        challanmasterdata.POrderNo = Convert.ToInt32(maxpoid);
                        challanmasterdata.Id = ChallanId;
                        challanmasterrepo.Edit(challanmasterdata);
                        challanmasterrepo.Save();
                        RecieptChalanDetailRepository challandetailrepo = new RecieptChalanDetailRepository();
                        RecieptChalanDetail challandetail = new RecieptChalanDetail();
                        StockRepository stockRepo = new StockRepository();
                        StockRepository updateStockRepo = new StockRepository();
                        ProductRepository productrepo = new ProductRepository();
                        AIOInventorySystem.Data.Model.Stock sData = new Data.Model.Stock();
                        if (deleteid.Count != 0)
                        {
                            for (int x = 0; x < deleteid.Count; x++)
                            {
                                try
                                {
                                    RecieptChalanDetailRepository challandetailrepo1 = new RecieptChalanDetailRepository();
                                    RecieptChalanDetail dcdetail = new RecieptChalanDetail();
                                    dcdetail = challandetailrepo1.GetById(deleteid[x]);
                                    challandetailrepo1.Remove(dcdetail);
                                    challandetailrepo1.Save();
                                    challandetailrepo1.Dispose();
                                }
                                catch (Exception)
                                { }
                            }
                        }
                        if (deletePoid.Count != 0)
                        {
                            for (int x = 0; x < deletePoid.Count; x++)
                            {
                                try
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPPOnBillRemoveDetailWithStockUpdate", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;

                                    command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'P';
                                    command.Parameters.Add("@id", SqlDbType.Int).Value = deletePoid[x];
                                    command.Parameters.Add("@billtype", SqlDbType.Int).Value = 0;
                                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                    command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);

                                    command.ExecuteNonQuery();
                                    db.CloseConnection();
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
                                SqlCommand command = new SqlCommand("SPPOnBillSaveUpdate", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'P';
                                if (Convert.ToInt32(GvProductInfo.Rows[i].Cells["Idg"].Value) != 0)
                                {
                                    command.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(GvProductInfo.Rows[i].Cells["PDIDg"].Value);
                                    command.Parameters.Add("@event", SqlDbType.Int).Value = 2;
                                }
                                else
                                {
                                    command.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                    command.Parameters.Add("@event", SqlDbType.Int).Value = 1;
                                }
                                command.Parameters.Add("@billtype", SqlDbType.Int).Value = 1;
                                command.Parameters.Add("@BillNo", SqlDbType.Int).Value = pmid;
                                command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                command.Parameters.Add("@AlternateUnit", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["SaleUnit"].Value);
                                command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantityg"].Value);
                                command.Parameters.Add("@FreeQty", SqlDbType.Decimal).Value = 0;
                                command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                                command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = 0;
                                command.Parameters.Add("@Amount", SqlDbType.Decimal).Value = 0;
                                command.Parameters.Add("@Discount", SqlDbType.Decimal).Value = 0;
                                command.Parameters.Add("@Vat", SqlDbType.Decimal).Value = 0;
                                command.Parameters.Add("@TotalAmount", SqlDbType.Decimal).Value = 0;
                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                                if (productentrytype == 1 || productentrytype == 2)
                                {
                                    command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                                    if (productentrytype != 2)
                                        command.Parameters.Add("@ExpireDate", SqlDbType.DateTime).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value);
                                }
                                command.ExecuteNonQuery();
                                db.CloseConnection();
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
                            try
                            {
                                db.connect();
                                SqlCommand command1 = new SqlCommand("SPRecieptChalanSaveUpdate", db.Connection);
                                command1.CommandType = CommandType.StoredProcedure;
                                if (Convert.ToInt32(GvProductInfo.Rows[i].Cells["Idg"].Value) != 0)
                                    command1.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(GvProductInfo.Rows[i].Cells["Idg"].Value);
                                else
                                    command1.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                command1.Parameters.Add("@event", SqlDbType.Int).Value = 2;
                                command1.Parameters.Add("@BillNo", SqlDbType.Int).Value = challanmasterdata.Id;
                                command1.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                command1.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                command1.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                command1.Parameters.Add("@SaleUnit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["SaleUnit"].Value);
                                command1.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantityg"].Value);
                                command1.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                                command1.Parameters.Add("@PDetailID", SqlDbType.Int).Value = Convert.ToInt32(GvProductInfo.Rows[i].Cells["PDIDg"].Value);
                                if (productentrytype == 1 || productentrytype == 2)
                                {
                                    command1.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["BatchNo"].Value);
                                    if (productentrytype != 2)
                                        command1.Parameters.Add("@ExpiryDate", SqlDbType.DateTime).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ExpiryDate"].Value);
                                }
                                command1.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                                command1.ExecuteNonQuery();
                                db.CloseConnection();
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
                        }
                        MessageBox.Show("Receipt Challan Updated.", "Success");
                        GvProductInfo.Rows.Clear();
                        clear();
                        this.ActiveControl = btnprint;
                        challanmasterrepo.Dispose();
                        challandetailrepo.Dispose();
                        stockRepo.Dispose();
                        updateStockRepo.Dispose();
                        productrepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Add Products Information.", "Warning");
                        this.ActiveControl = txtProductname;
                    }
                }
                else
                {
                    MessageBox.Show("Choose Supplier Name.", "Warning");
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
                if ((MessageBox.Show("Are you sure to Delete this Receipt Challan?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    AIOInventorySystem.Data.Model.Stock sdata = new Data.Model.Stock();
                    string productname = "", unit = "", company = "", Suppliername = "", DeletedBillNo = "";
                    decimal Quantity = 0, newsqty = 0, sqty = 0, pqty = 0, prqty = 0, newrqty = 0, rqty = 0, srqty = 0, dqty = 0, oqty = 0, freeqty = 0, NetAmount = 0, remRawQty = 0;
                    int cntProduct = 0, purcMId = 0, rawqty = 0;
                    string proname = "", unit1 = "", company1 = "", batchno = "", expirydate = "";

                    RecieptChallanMasterRepository challanmasterrepo = new RecieptChallanMasterRepository();
                    var receiptMData = challanmasterrepo.GetAll().Where(t => t.Id == ChallanId && t.POrderNo == Convert.ToInt32(maxpoid) && t.SupplierId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && t.Status == 1).FirstOrDefault();
                    if (receiptMData != null)
                    {
                        PurchaseMasterRepository purMRepo = new PurchaseMasterRepository();
                        var purMData = purMRepo.GetAll().Where(t => t.PorderNo == Convert.ToInt32(maxpoid) && t.SupplierId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear).FirstOrDefault();
                        if (purMData != null)
                        {
                            purcMId = Convert.ToInt32(purMData.Id);
                            PurchaseDetailRepository detailRepo1 = new PurchaseDetailRepository();
                            List<PurchaseDetail> detailData1 = detailRepo1.GetAll().Where(t => t.PorderNo == Convert.ToInt32(purcMId) && (t.BatchNo != null || t.ExpireDate != null) && t.PurchaseMaster.CompId == CommonMethod.CompId && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear).ToList();
                            if (detailData1.Count != 0)
                            {
                                foreach (var pdata in detailData1)
                                {
                                    proname = pdata.ProductName;
                                    unit1 = pdata.Unit;
                                    company1 = pdata.CompanyName;
                                    batchno = pdata.BatchNo;
                                    if (pdata.ExpireDate != null)
                                        expirydate = Convert.ToString(pdata.ExpireDate.Value.ToShortDateString());
                                    else
                                        expirydate = "";
                                    getProductType(proname, company1, unit1);
                                    if (productentrytype == 1 || productentrytype == 2)
                                    {
                                        CustomerBillDetailRepository custDetailRepo = new CustomerBillDetailRepository();
                                        List<CustomerBillDetail> custdetailData = new List<CustomerBillDetail>();
                                        if (productentrytype == 1)
                                            custdetailData = custDetailRepo.GetAll().Where(t => t.ProductId == proname && t.Unit == unit1 && t.CompanyName == company1 && (t.BatchNo == batchno && t.Expiry == expirydate)).ToList();
                                        else
                                            custdetailData = custDetailRepo.GetAll().Where(t => t.ProductId == proname && t.Unit == unit1 && t.CompanyName == company1 && (t.BatchNo == batchno)).ToList();
                                        if (custdetailData.Count != 0)
                                        {
                                            foreach (var cdata in custdetailData)
                                            {
                                                cntProduct++;
                                            }
                                        }
                                    }
                                    else if (productentrytype == 6)
                                    {
                                        CustomerBillDetailRepository custDetailRepo = new CustomerBillDetailRepository();
                                        List<CustomerBillDetail> custdetailData = new List<CustomerBillDetail>();
                                        if (productentrytype == 1)
                                            custdetailData = custDetailRepo.GetAll().Where(t => t.ProductId == proname && t.Unit == unit1 && t.CompanyName == company1).ToList();
                                        else
                                            custdetailData = custDetailRepo.GetAll().Where(t => t.ProductId == proname && t.Unit == unit1 && t.CompanyName == company1).ToList();
                                        if (custdetailData.Count != 0)
                                        {
                                            foreach (var cdata in custdetailData)
                                            {
                                                cntProduct++;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        DataTable dtAssembly = db.GetTable("Select * from tblAssemblyProductDetails where ProductName='" + proname + "' and CompanyName='" + company1 + "' and Unit='" + unit1 + "' and CompId=" + CommonMethod.CompId + "");
                                        if (dtAssembly.Rows.Count > 0)
                                        {                                            //                                                  0                   1                 2                     3                          4                   
                                            DataTable dtStock = db.GetTable("Select isnull(PurchaseQty,0), isnull(SaleQty,0),isnull(PurReturnQty,0),isnull(SaleReturnQty,0),isnull(OpeningQty,0) from Stock where ProductName='" + proname + "' and MfgCompany='" + company1 + "' and Unit='" + unit1 + "' and CompId=" + CommonMethod.CompId + "");
                                            if (dtStock.Rows.Count > 0)
                                            {
                                                remRawQty = Convert.ToDecimal(dtStock.Rows[0][4].ToString()) + Convert.ToDecimal(dtStock.Rows[0][0].ToString()) - Convert.ToDecimal(dtStock.Rows[0][1].ToString()) - Convert.ToDecimal(dtStock.Rows[0][2].ToString()) + Convert.ToDecimal(dtStock.Rows[0][3].ToString()) - Convert.ToDecimal(pdata.Quantity);
                                                if (remRawQty < 0)
                                                    rawqty = 1;
                                            }
                                        }
                                    }
                                }
                            }
                            if (cntProduct != 0 || rawqty == 1)
                            {
                                if (cntProduct != 0)
                                {
                                    MessageBox.Show("You can't delete this Receipt Challan ," + Environment.NewLine + " because this bill is used in batch product in sale bill.", "Warning");
                                    this.ActiveControl = btnUpdate;
                                }
                                else if (rawqty == 1)
                                    MessageBox.Show("You can't delete this Purchase Bill," + Environment.NewLine + " because this raw products used for " + Environment.NewLine + "assembly product in sale bill.", "Warning");
                            }
                            else
                            {
                                // add details for history table
                                try
                                {
                                    DeletedBillNo = Convert.ToString(receiptMData.RecieptChallanNo);
                                    Suppliername = Convert.ToString(receiptMData.SupplierName);
                                    NetAmount = 0;
                                    DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                                    DeleteHistoryTable historydata = new DeleteHistoryTable();
                                    historydata.DeleteType = "Receipt Challan";
                                    historydata.BillNo = DeletedBillNo;
                                    historydata.SupplierOrCustomerName = Suppliername;
                                    historydata.Amount = Convert.ToDecimal(NetAmount);
                                    historydata.BillDate = Convert.ToDateTime(receiptMData.RecieptChallanDate);
                                    historydata.CurrentDate = DateTime.Now.Date;
                                    historydata.CompId = CommonMethod.CompId;
                                    historydata.TransactionYear = CommonMethod.TransactionYear;
                                    historyRepo.Add(historydata);
                                    historyRepo.Save();
                                }
                                catch (Exception) { }

                                RecieptChalanDetailRepository RCDetailrepo = new RecieptChalanDetailRepository();
                                List<RecieptChalanDetail> RCDetailList = RCDetailrepo.GetAll().Where(t => t.RecieptChallanNo == ChallanId && t.RecieptChallanMaster.CompId == CommonMethod.CompId && t.RecieptChallanMaster.TransactionYear == CommonMethod.TransactionYear).ToList();
                                if (RCDetailList.Count != 0)
                                {
                                    foreach (var item1 in RCDetailList)
                                    {
                                        int detailId = 0; decimal rate = 0; int updateStockId = 0;
                                        productname = item1.ProductName;
                                        company = item1.CompanyName;
                                        unit = item1.Unit;
                                        detailId = item1.Id;
                                        Quantity = Convert.ToDecimal(item1.Quantity);
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
                                                newsqty = pqty - Quantity - freeqty;
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
                                    }
                                }
                                db.connect();
                                DataTable dtPurM = db.GetTable("delete from PurchaseDetail where PorderNo='" + purcMId + "' ");
                                DataTable dtPurD = db.GetTable("delete from PurchaseMaster where Id='" + purcMId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                                DataTable dt1 = db.GetTable("delete from RecieptChalanDetail where RecieptChallanNo='" + ChallanId + "'");
                                DataTable dt = db.GetTable("delete from RecieptChallanMaster where Id='" + ChallanId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                                MessageBox.Show("Record Deleted Sucessfully.", "Success");
                                dt.Dispose();
                                dt1.Dispose();
                                db.CloseConnection();
                                GvProductInfo.Rows.Clear();
                                clear();
                                dtPurM.Dispose(); dtPurD.Dispose();
                                this.ActiveControl = dtpchallandate;
                                btnUpdate.Enabled = false;
                            }
                        }
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
                {
                    if (GvProductInfo.Rows.Count != 1 && btnsave.Enabled == false)
                    {
                        if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        {
                            if (Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Idg"].Value) != 0)
                            {
                                GvProductInfo.Rows.Remove(GvProductInfo.Rows[e.RowIndex]);
                                deleteid.Add(Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Idg"].Value));
                                deletePoid.Add(Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["PDIDg"].Value));
                            }
                            else
                                GvProductInfo.Rows.Remove(GvProductInfo.Rows[e.RowIndex]);
                        }
                    }
                    else if (GvProductInfo.Rows.Count != 1 && btnsave.Enabled == true)
                    {
                        if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                            GvProductInfo.Rows.Remove(GvProductInfo.Rows[e.RowIndex]);
                    }
                    else
                    {
                        MessageBox.Show("This Receipt Challan has only 1 product." + Environment.NewLine + "You have to delete full Challan.", "Warning");
                        this.ActiveControl = btnUpdate;
                    }
                }
                else
                {
                    cmbcomanyname.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value);
                    txtProductname.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) + "," + GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value + "," + GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value;
                    txtQuantity.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Quantityg"].Value);
                    cmbUnit.Items.Clear();
                    cmbUnit.DisplayMember = "Text";
                    cmbUnit.ValueMember = "Value";
                    cmbUnit.Items.Add(new { Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value), Value = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value) });
                    if (Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleUnit"].Value) != Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value))
                        cmbUnit.Items.Add(new { Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleUnit"].Value), Value = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleUnit"].Value) });
                    cmbUnit.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleUnit"].Value);
                    if (!string.IsNullOrEmpty(Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["BatchNo"].Value)))
                    {
                        txtBatchNo.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["BatchNo"].Value);
                        if (!string.IsNullOrEmpty(Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ExpiryDate"].Value)))
                            dtpExpiry.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ExpiryDate"].Value);
                    }
                    colIndex = e.RowIndex;
                    this.ActiveControl = txtProductname;
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
                customerdata();
                this.ActiveControl = cmbcustomername;
            }
            catch (Exception)
            { }
        }

        private void txtBatchNo_Leave(object sender, EventArgs e)
        {
            try
            {
                if (dtpExpiry.Enabled == false)
                {
                    pnlIntegratedDtls.Visible = false;
                    this.ActiveControl = btnAdd;
                }
            }
            catch (Exception)
            { }
        }

        private void dtpExpiry_Leave(object sender, EventArgs e)
        {
            pnlIntegratedDtls.Visible = false;
            this.ActiveControl = btnAdd;
        }

        private void btnBatchClose_Click(object sender, EventArgs e)
        {
            try
            {
                pnlIntegratedDtls.Visible = false;
                this.ActiveControl = btnAdd;
            }
            catch (Exception)
            { }
        }

        private void pnlIntegratedDtls_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                if (pnlIntegratedDtls.Visible == true)
                {
                    this.ActiveControl = tblltBatchSize;
                    this.ActiveControl = txtBatchNo;
                }
                else
                    this.ActiveControl = btnAdd;
            }
            catch (Exception)
            { }
        }
    }
}