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
    public partial class frmDelieveryChallan : Form
    {
        public static frmDelieveryChallan delichalleninfo;
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        List<int> deleteid = new List<int>();
        public int ChallanId, colIndex = -1, dcno;
        public string ProductCategory;
        private bool PrefixToBind = false, prefixFilled = false;

        public frmDelieveryChallan()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxchallanid();
            fillcompany();
            customerdata(); fillVehicle();
            deleteid.Clear();
            btnsave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        public frmDelieveryChallan(int ChallanNo)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            maxchallanid();
            fillcompany();
            customerdata(); fillVehicle();
            deleteid.Clear();
            GetChallanData(ChallanNo);
            this.ActiveControl = cmbcustomername;
        }

        public void GetChallanData(int challanno)
        {
            try
            {
                DeliveryChallanRepository challanmasterrepo = new DeliveryChallanRepository();
                var challanmasterData = challanmasterrepo.GetById(challanno);
                if (Convert.ToInt32(challanmasterData.Status) == 0)
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

                txtchallanno.Text = Convert.ToString(challanmasterData.DeliveryChallanNo);
                dtpchallandate.Value = challanmasterData.DeliveryChallanDate.Value;
                cmbcustomername.Text = challanmasterData.CustomerName;
                if (challanmasterData.VehicalNo == null)
                    txtVehicalno.Text = "Select";
                else
                    txtVehicalno.Text = challanmasterData.VehicalNo;
                ChallanId = challanmasterData.Id;
                dcno = Convert.ToInt32(txtchallanno.Text);
                var challanDetailData = challanmasterData.DeliveryChallanDetails;
                int i = 0;
                foreach (var item in challanDetailData)
                {
                    GvProductInfo.Rows.Add();
                    GvProductInfo.Rows[i].Cells["Id"].Value = item.Id;
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
                        var productdata = productrepo.GetAll().Where(t => t.ProductName == item.ProductName && t.ManufactureCompany == item.CompanyName && t.Uniti == item.Unit).FirstOrDefault();
                        GvProductInfo.Rows[i].Cells["Quantity"].Value = Convert.ToDecimal(item.Quantity) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                    }
                    else
                        GvProductInfo.Rows[i].Cells["Quantity"].Value = Convert.ToDouble(item.Quantity);
                    GvProductInfo.Rows[i].Cells["Code"].Value = item.Code;
                    GvProductInfo.Rows[i].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
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
                DeliveryChallanRepository challanmasterrepo = new DeliveryChallanRepository();
                var maxid = challanmasterrepo.GetAll().Where(t => t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).Max(t => t.DeliveryChallanNo);
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

        public void fillVehicle()
        {
            try
            {
                VehicleInformationRepository Vehiclerepo = new VehicleInformationRepository();
                List<VehicleInformation> cdata = new List<VehicleInformation>();
                cdata.Clear();
                cdata = Vehiclerepo.GetAll().OrderByDescending(t => t.VehicleNumber).ToList();
                cdata.Add(new VehicleInformation { VehicleNumber = "Select", VehicleId = 0 });
                cdata.Reverse();
                txtVehicalno.DataSource = cdata;
                txtVehicalno.ValueMember = "VehicleId";
                txtVehicalno.DisplayMember = "VehicleNumber";
                Vehiclerepo.Dispose();
                txtVehicalno.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        private void frmDelieveryChallan_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
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
                    txtProductname.AutoCompleteCustomSource = MyCollection;
                    con.Close();
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
                if (cmbcustomername.SelectedIndex > 0)
                {
                    if (txtProductname.Text.Trim() != "" && txtQuantity.Text != "")
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
                                GvProductInfo.Rows[colIndex].Cells["Quantity"].Value = 0;
                            else
                                GvProductInfo.Rows[colIndex].Cells["Quantity"].Value = txtQuantity.Text;
                            GvProductInfo.Rows[colIndex].Cells["Code"].Value = GvProductInfo.Rows[colIndex].Cells["Code"].Value;
                            GvProductInfo.Rows[colIndex].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                            txtProductname.Text = "";
                            cmbUnit.Items.Clear();
                            txtQuantity.Text = "";
                            this.ActiveControl = cmbcomanyname;
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
                                        GvProductInfo.Rows[i - 1].Cells["Quantity"].Value = 0;
                                    else
                                        GvProductInfo.Rows[i - 1].Cells["Quantity"].Value = 1;
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
                                    GvProductInfo.Rows[i - 1].Cells["Quantity"].Value = 0;
                                else
                                    GvProductInfo.Rows[i - 1].Cells["Quantity"].Value = txtQuantity.Text;
                                GvProductInfo.Rows[i - 1].Cells["Code"].Value = "";
                                GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                            }
                            txtProductname.Text = "";
                            cmbUnit.Items.Clear();
                            txtQuantity.Text = "";
                            this.ActiveControl = cmbcomanyname;
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
                    MessageBox.Show("Choose Customer Name.", "Warning");
                    this.ActiveControl = cmbcustomername;
                }
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            txtchallanno.Text = "";
            GvProductInfo.Rows.Clear();
            fillcompany();
            deleteid.Clear();
            txtProductname.Text = "";
            cmbUnit.Items.Clear();
            txtQuantity.Text = "";
            this.ActiveControl = txtProductname;
            customerdata();
            fillVehicle();
            maxchallanid();
            lblmechname.Text = "";
            btnsave.Enabled = true;
            btnUpdate.Enabled = false;
            try
            { dtpchallandate.Value = DateTime.Now.Date; }
            catch (Exception)
            { }
            btnDelete.Enabled = false;
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

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                maxchallanid();
                DeliveryChallanRepository challanmasterrepo = new DeliveryChallanRepository();
                DeliveryChallanMaster challanmaster = new DeliveryChallanMaster();
                var challanno = challanmasterrepo.GetAll().Where(t => t.DeliveryChallanNo == Convert.ToInt32(txtchallanno.Text) && t.TransactionYear == CommonMethod.TransactionYear && t.CompId == CommonMethod.CompId).FirstOrDefault();
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
                            maxchallanid();
                            challanmaster.DeliveryChallanNo = Convert.ToInt32(txtchallanno.Text);
                            challanmaster.DeliveryChallanDate = Convert.ToDateTime(dtpchallandate.Value);
                            challanmaster.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                            challanmaster.CustomerName = cmbcustomername.Text;
                            if (txtVehicalno.SelectedIndex > 0)
                                challanmaster.VehicalNo = txtVehicalno.Text;
                            else
                                challanmaster.VehicalNo = null;
                            challanmaster.Status = 1;
                            challanmaster.TransactionYear = CommonMethod.TransactionYear;
                            challanmaster.CompId = CommonMethod.CompId;
                            challanmasterrepo.Add(challanmaster);
                            challanmasterrepo.Save();
                            dcno = Convert.ToInt32(txtchallanno.Text);

                            DeliveryChallanDetailRepository challandetailrepo = new DeliveryChallanDetailRepository();
                            DeliveryChallanDetail challandetail = new DeliveryChallanDetail();
                            for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                            {
                                try
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPDCandOBSaveUpdate", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;

                                    command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'D';
                                    command.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                    command.Parameters.Add("@event", SqlDbType.Int).Value = 1;
                                    command.Parameters.Add("@BillNo", SqlDbType.Int).Value = challanmaster.Id;
                                    command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                    command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                    command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                    command.Parameters.Add("@SaleUnit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["SaleUnit"].Value);
                                    command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value);
                                    command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                                    command.ExecuteNonQuery();
                                    db.CloseConnection();
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                            }
                            MessageBox.Show("Delivery Challan Saved.", "Success");
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
                        MessageBox.Show("Choose Customer Name.", "Warning");
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

        private void cmbcustomername_TextChanged(object sender, EventArgs e)
        {
            try
            {
                label6.Text = "";
                try
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPremainingpayment", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CommonMethod.CompId;
                    command.Parameters.Add("@Flag", SqlDbType.NVarChar).Value = "CustRem";
                    command.Parameters.Add("@CustomerID", SqlDbType.Int).Value = cmbcustomername.SelectedValue;

                    DataTable dt1 = new DataTable();
                    dt1.Load(command.ExecuteReader());
                    db.CloseConnection();
                    label6.Text = Convert.ToString(dt1.Compute("Sum(TotalReaminingAmount)", ""));
                    dt1.Dispose();
                }
                catch (Exception)
                { db.CloseConnection(); }

                lblmechname.Text = "";
                CustomerRepository custrepo = new CustomerRepository();
                var Mistriname = custrepo.GetAll().Where(t => t.CustomerID == Convert.ToInt32(cmbcustomername.SelectedValue)).FirstOrDefault();
                lblmechname.Text = Mistriname.MistriName;
                custrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                RptDeliveryChallan challanreport = new RptDeliveryChallan(dcno);
                challanreport.ShowDialog();
                challanreport.Dispose();
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
                        if (CommonMethod.commProduct == null)
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

        private void txtProductname_Leave(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Visible = false;
                gvProductRemStock.Visible = false;
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
                        var productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
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

        private void btndeliverychallenlist_Click(object sender, EventArgs e)
        {
            try
            {
                clear();
                delichalleninfo = this;
                frmDelieveryChallan p = new frmDelieveryChallan();
                p.Visible = false;
                frmDeliveryChallanList delichallenlist = new frmDeliveryChallanList();
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
                    this.ActiveControl = txtVehicalno;
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
                    this.ActiveControl = txtQuantity;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmProductInformation product = new frmProductInformation();
                        product.ShowDialog();
                        frmDelieveryChallan_Load(null, null);
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
                    }
                    else
                        goto s;
                }
                else
                {
                    if (ProductCategory == "Motor")
                        goto t;
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
                { }
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

        private void txtProductname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string product_prefix = "";
                if (txtProductname.Text != "")
                {
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
                            var productdata = productrepo.GetAll().Where(t => t.ProductName == proname && t.ManufactureCompany == company && t.Uniti == unit).FirstOrDefault();
                            if (productdata != null)
                            {
                                product_prefix = productdata.Prefix;
                                PrefixToBind = true;
                                cmbcomanyname.Text = product_prefix;
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

        private void cmbcustomername_Leave(object sender, EventArgs e)
        {
            if (cmbcustomername.Text != "" && cmbcustomername.Text != "Select")
            {
                CommonMethod cm = new CommonMethod();
                if (Convert.ToInt32(cm.ValidCustomer(cmbcustomername.Text)) == 1)
                { }
                else
                {
                    MessageBox.Show("Invalid Customer.", "Warning");
                    customerdata();
                    this.ActiveControl = cmbcustomername;
                }
            }
        }

        private void cmbcomanyname_KeyDown(object sender, KeyEventArgs e)
        {
            try
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

        private void cmbcomanyname_MouseClick(object sender, MouseEventArgs e)
        {
            PrefixToBind = false;
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

        private void dtpchallandate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcustomername;
            }
            catch (Exception) { }
        }

        private void txtVehicalno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                this.ActiveControl = cmbcomanyname;
        }

        private void btnCustForm_Click(object sender, EventArgs e)
        {
            try
            {
                frmcustomer compinfo = new frmcustomer();
                compinfo.WindowState = System.Windows.Forms.FormWindowState.Normal;
                compinfo.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                compinfo.ShowDialog();
                customerdata();
                this.ActiveControl = cmbcustomername;
            }
            catch (Exception)
            { }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbcustomername.SelectedIndex > 0)
                {
                    if (GvProductInfo.Rows.Count != 0)
                    {
                        DeliveryChallanRepository challanmasterrepo = new DeliveryChallanRepository();
                        DeliveryChallanMaster challanmasterdata = new DeliveryChallanMaster();
                        challanmasterdata.DeliveryChallanNo = Convert.ToInt32(txtchallanno.Text);
                        challanmasterdata.DeliveryChallanDate = Convert.ToDateTime(dtpchallandate.Value);
                        challanmasterdata.CustomerId = Convert.ToInt32(cmbcustomername.SelectedValue);
                        challanmasterdata.CustomerName = cmbcustomername.Text;
                        if (txtVehicalno.SelectedIndex > 0)
                            challanmasterdata.VehicalNo = txtVehicalno.Text;
                        else
                            challanmasterdata.VehicalNo = null;
                        challanmasterdata.Status = 1;
                        challanmasterdata.TransactionYear = CommonMethod.TransactionYear;
                        challanmasterdata.CompId = CommonMethod.CompId;
                        challanmasterdata.Id = ChallanId;
                        challanmasterrepo.Edit(challanmasterdata);
                        challanmasterrepo.Save();
                        if (deleteid.Count != 0)
                        {
                            for (int x = 0; x < deleteid.Count; x++)
                            {
                                try
                                {
                                    DeliveryChallanDetailRepository challandetailrepo1 = new DeliveryChallanDetailRepository();
                                    DeliveryChallanDetail dcdetail = new DeliveryChallanDetail();
                                    dcdetail = challandetailrepo1.GetById(deleteid[x]);
                                    challandetailrepo1.Remove(dcdetail);
                                    challandetailrepo1.Save();
                                    challandetailrepo1.Dispose();
                                }
                                catch (Exception)
                                { }
                            }
                        }
                        for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                        {
                            int RowId = Convert.ToInt32(GvProductInfo.Rows[i].Cells["Id"].Value);
                            if (RowId != 0)
                            {
                                try
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPDCandOBSaveUpdate", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;

                                    command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'D';
                                    command.Parameters.Add("@id", SqlDbType.Int).Value = RowId;
                                    command.Parameters.Add("@event", SqlDbType.Int).Value = 2;
                                    command.Parameters.Add("@BillNo", SqlDbType.Int).Value = challanmasterdata.Id;
                                    command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                    command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                    command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                    command.Parameters.Add("@SaleUnit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["SaleUnit"].Value);
                                    command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value);
                                    command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                                    command.ExecuteNonQuery();
                                    db.CloseConnection();
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                            }
                            else
                            {
                                try
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPDCandOBSaveUpdate", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;

                                    command.Parameters.Add("@type", SqlDbType.VarChar).Value = 'D';
                                    command.Parameters.Add("@id", SqlDbType.Int).Value = 0;
                                    command.Parameters.Add("@event", SqlDbType.Int).Value = 1;
                                    command.Parameters.Add("@BillNo", SqlDbType.Int).Value = challanmasterdata.Id;
                                    command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductNameg"].Value);
                                    command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                    command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                    command.Parameters.Add("@SaleUnit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["SaleUnit"].Value);
                                    command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantity"].Value);
                                    command.Parameters.Add("@Code", SqlDbType.NVarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value);
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                                    command.ExecuteNonQuery();
                                    db.CloseConnection();
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                            }
                        }
                        MessageBox.Show("Delivery Challan Updated.", "Success");
                        clear();
                        this.ActiveControl = btnprint;
                        challanmasterrepo.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Add Products Information.", "Warning");
                        this.ActiveControl = txtProductname;
                    }
                }
                else
                {
                    MessageBox.Show("Choose Customer Name.", "Warning");
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
                if ((MessageBox.Show("Are you sure to Delete this Delivery Challan ?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                {
                    int DCMId = 0;
                    decimal NetAmount = 0; string Suppliername = "", DeletedBillNo = "";
                    DeliveryChallanRepository DCMRepo = new DeliveryChallanRepository();
                    var DCData = DCMRepo.GetAll().Where(t => t.DeliveryChallanNo == Convert.ToInt32(txtchallanno.Text) && t.CustomerId == Convert.ToInt32(cmbcustomername.SelectedValue) && t.CompId == CommonMethod.CompId && t.TransactionYear == CommonMethod.TransactionYear && t.Status == 1).FirstOrDefault();
                    if (DCData != null)
                    {
                        DCMId = Convert.ToInt32(DCData.Id);
                        // add details for history table
                        try
                        {
                            DeletedBillNo = Convert.ToString(DCData.DeliveryChallanNo);
                            Suppliername = Convert.ToString(DCData.CustomerName);
                            NetAmount = Convert.ToDecimal(0);

                            DeleteHistoryTableRepository historyRepo = new DeleteHistoryTableRepository();
                            DeleteHistoryTable historydata = new DeleteHistoryTable();
                            historydata.DeleteType = "Delivery Challan";
                            historydata.BillNo = DeletedBillNo;
                            historydata.SupplierOrCustomerName = Suppliername;
                            historydata.Amount = Convert.ToDecimal(NetAmount);
                            historydata.CurrentDate = DateTime.Now.Date;
                            historydata.BillDate = Convert.ToDateTime(DCData.DeliveryChallanDate);
                            historydata.CompId = CommonMethod.CompId;
                            historydata.TransactionYear = CommonMethod.TransactionYear;
                            historyRepo.Add(historydata);
                            historyRepo.Save();
                        }
                        catch (Exception) { }
                        db.connect();
                        DataTable dt = db.GetTable("delete from DeliveryChallanDetail where DeliveryChallanNo='" + DCMId + "'");
                        DataTable dt1 = db.GetTable("delete from DeliveryChallanMaster where Id='" + DCMId + "' and CompId='" + CommonMethod.CompId + "' and TransactionYear='" + CommonMethod.TransactionYear + "'");
                        MessageBox.Show("Record Deleted Sucessfully.", "Success");
                        dt.Dispose();
                        dt1.Dispose();
                        db.CloseConnection();
                        clear();
                        this.ActiveControl = dtpchallandate;
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
                //if (e.ColumnIndex == 7)
                {
                    if (GvProductInfo.Rows.Count != 1 && btnsave.Enabled == false)
                    {
                        if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                        {
                            if (Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value) != 0)
                            {
                                deleteid.Add(Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value));
                                GvProductInfo.Rows.Remove(GvProductInfo.Rows[e.RowIndex]);                                
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
                        MessageBox.Show("This Delivery Challan has only 1 product." + Environment.NewLine + "You have to delete full Challan.", "Warning");
                        this.ActiveControl = btnUpdate;
                    }
                }
                else
                {
                    cmbcomanyname.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value);
                    txtProductname.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductNameg"].Value) + "," + GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value + "," + GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value;
                    txtQuantity.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Quantity"].Value);
                    cmbUnit.Items.Clear();
                    cmbUnit.DisplayMember = "Text";
                    cmbUnit.ValueMember = "Value";
                    cmbUnit.Items.Add(new { Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value), Value = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value) });
                    if (Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleUnit"].Value) != Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value))
                        cmbUnit.Items.Add(new { Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleUnit"].Value), Value = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleUnit"].Value) });
                    cmbUnit.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SaleUnit"].Value);
                    colIndex = e.RowIndex;
                    this.ActiveControl = txtProductname;
                }
            }
            catch (Exception)
            { }
        }

        private void txtVehicalno_Leave(object sender, EventArgs e)
        {
            try
            {
                VehicleInformationRepository VehInfoRepo = new VehicleInformationRepository();
                VehicleInformation VehData = VehInfoRepo.GetAll().Where(t => t.VehicleNumber.ToLower() == txtVehicalno.Text.ToLower()).FirstOrDefault();
                if (VehData == null)
                {
                    MessageBox.Show("This vehicle Number is not saved. " + Environment.NewLine + "Please Add it through Vehicle Information or Select other Vehicle No.", "Warning");
                    txtVehicalno.Text = "Select";
                    this.ActiveControl = txtVehicalno;
                }
            }
            catch (Exception)
            { }
        }
    }
}