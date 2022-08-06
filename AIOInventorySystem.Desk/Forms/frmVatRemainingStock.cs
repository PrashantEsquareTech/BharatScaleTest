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
    public partial class frmVatRemainingStock : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        public int flag = 1, productentrytype = 0;

        public frmVatRemainingStock()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            fillcombo();
            fillGroupName();
            FillGodownName();
        }

        public void fillcombo()
        {
            try
            {
                DataTable dt = null;
                if (CommonMethod.commProduct == true)
                    dt = db.Gettable("Select Distinct(ManufactureCompany) from ProductInformation  order by ManufactureCompany");
                else
                    dt = db.Gettable("Select Distinct(ManufactureCompany) from ProductInformation where compid=" + CommonMethod.CompId + "  order by ManufactureCompany");
                DataRow dr = dt.NewRow(); //Create New Row
                dr["ManufactureCompany"] = "Select";              // Set Column Value
                dt.Rows.InsertAt(dr, 0); // Ins
                cmbcomanyname.DataSource = dt;
                cmbcomanyname.ValueMember = "ManufactureCompany";
                cmbcomanyname.DisplayMember = "ManufactureCompany";
            }
            catch (Exception)
            { }
        }

        public void fillGroupName()
        {
            try
            {
                GroupMasterRepository GMRepo = new GroupMasterRepository();
                List<GroupMaster> GMData = new List<GroupMaster>();
                GMData.Clear();
                if (CommonMethod.commProduct == true)
                    GMData = GMRepo.GetAll().OrderByDescending(t => t.GroupMasterName).ToList();
                else
                    GMData = GMRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.GroupMasterName).ToList();
                cmbGodownName.Enabled = true;
                GMData.Add(new GroupMaster { GroupMasterName = "Select", GroupMasterId = 0 });
                GMData.Reverse();
                cmbGroupName.DataSource = GMData;
                cmbGroupName.ValueMember = "GroupMasterId";
                cmbGroupName.DisplayMember = "GroupMasterName";
                cmbGroupName.Enabled = true;
                GMRepo.Dispose();
            }
            catch (Exception)
            { }
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
                else if (productdata.ProductType == 6) //Work type
                    productentrytype = 9;
                else
                    productentrytype = 0;
            }
        }

        public void FillGodownName()
        {
            try
            {
                flag = 0;
                GodownMasterRepository GDRepo = new GodownMasterRepository();
                List<GodownMaster> GDData = new List<GodownMaster>();
                GDData.Clear();
                if (CommonMethod.commProduct == true)
                    GDData = GDRepo.GetAll().OrderByDescending(t => t.GodownName).ToList();
                else
                    GDData = GDRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.GodownName).ToList();
                if (GDData.Count > 0)
                {
                    cmbGodownName.Enabled = true;
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
                    cmbGodownName.Enabled = false;
                    cmbGodownName.SelectedIndex = 0;
                }
                flag = 1;
            }
            catch (Exception)
            { }
        }

        private void Stock_Load(object sender, EventArgs e)
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
                }
            }
            catch (Exception)
            { }
        }

        private void btncheck_Click(object sender, EventArgs e)
        {
            try
            {
                object AmtSum, QtySum;
                if (chkDate.Checked)
                {
                    if (txtProductname.Text != "")
                    {
                        DataTable dt = new DataTable();
                        string Proname1 = txtProductname.Text.Trim().Split(',')[0];
                        string company = txtProductname.Text.Trim().Split(',')[1];
                        string unit = txtProductname.Text.Trim().Split(',')[2];
                        getProductType(Proname1, company, unit);

                        GvPorderInfo.DataSource = null;
                        for (int i = 0; i < GvPorderInfo.Columns.Count; i++)
                        {
                            GvPorderInfo.Columns.RemoveAt(i);
                        }
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            flag = 0;

                            string d = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                            if (productentrytype != 9 && productentrytype != 6)
                            {
                                db.connect();
                                SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = Proname1;
                                command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = company;
                                command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = unit;
                                command.Parameters.Add("@stock", SqlDbType.VarChar).Value = "DP";
                                command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'n';
                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = d;
                                command.CommandTimeout = 300;
                                dt.Load(command.ExecuteReader());
                                GvPorderInfo.DataSource = dt;
                                db.CloseConnection();
                            }
                        }
                        else
                        {
                            string d = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                            if (productentrytype != 9)
                            {
                                db.connect();
                                SqlCommand command = new SqlCommand("SPVatnWithoutVatGodownStock", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = Proname1;
                                command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = company;
                                command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = unit;
                                command.Parameters.Add("@stock", SqlDbType.VarChar).Value = "DP";
                                command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'n';
                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                                command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = d;
                                command.CommandTimeout = 300;
                                dt.Load(command.ExecuteReader());
                                GvPorderInfo.DataSource = dt;
                                db.CloseConnection();
                            }
                        }
                        GvPorderInfo.Refresh();
                        AmtSum = dt.Compute("Sum(Amount)", "RemQty>=0");
                        QtySum = dt.Compute("Sum(RemQty)", "RemQty>=0");
                        lblTotalAmount.Text = Convert.ToString(AmtSum);
                        lbltotalqty.Text = Convert.ToString(QtySum);
                        GvPorderInfo.Refresh();
                        GvPorderInfo.Columns["ID"].Visible = false;
                        GvPorderInfo.Columns["ProductName"].Width = 140;
                        GvPorderInfo.Columns["MfgCompany"].Width = 120;
                        GvPorderInfo.Columns["Unit"].Width = 60;
                        GvPorderInfo.Columns["OpeningQty"].Width = 75;
                        GvPorderInfo.Columns["PurchaseQty"].Width = 75;
                        GvPorderInfo.Columns["PurReturnQty"].Width = 75;
                        GvPorderInfo.Columns["SaleQty"].Width = 75;
                        GvPorderInfo.Columns["SaleReturnQty"].Width = 75;
                        GvPorderInfo.Columns["DeadQty"].Width = 75;
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            GvPorderInfo.Columns["RemQty"].Width = 75;
                            GvPorderInfo.Columns["ProductRate"].Width = 75;
                            GvPorderInfo.Columns["Amount"].Width = 75;
                        }
                        DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                        bcol.HeaderText = "Show Detail";
                        bcol.Text = "Show Detail";
                        bcol.Name = "btnshowdetail";
                        bcol.FlatStyle = FlatStyle.System;
                        bcol.Width = 80;
                        bcol.UseColumnTextForButtonValue = true;
                        GvPorderInfo.Columns.Add(bcol);
                        this.ActiveControl = txtProductname;
                        dt.Dispose();
                    }
                    else if (cmbcomanyname.SelectedIndex != 0)
                    {
                        DataTable dt = new DataTable();
                        progressBar1.Visible = true;
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = 100;
                        progressBar1.Value = 20;
                        GvPorderInfo.DataSource = null;
                        for (int i = 0; i < GvPorderInfo.Columns.Count; i++)
                        {
                            GvPorderInfo.Columns.RemoveAt(i);
                        }
                        progressBar1.Value = 40;
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            flag = 0;
                            progressBar1.Value = 60;

                            db.connect();
                            SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = cmbcomanyname.Text;
                            command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@stock", SqlDbType.VarChar).Value = "DC";
                            command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'n';
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                            command.CommandTimeout = 300;
                            dt.Load(command.ExecuteReader());
                            GvPorderInfo.DataSource = dt;
                            db.CloseConnection();
                        }
                        else
                        {
                            progressBar1.Value = 60;
                            db.connect();
                            SqlCommand command = new SqlCommand("SPVatnWithoutVatGodownStock", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = cmbcomanyname.Text;
                            command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@stock", SqlDbType.VarChar).Value = "DC";
                            command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'n';
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                            command.CommandTimeout = 300;
                            dt.Load(command.ExecuteReader());
                            GvPorderInfo.DataSource = dt;
                            db.CloseConnection();
                        }

                        progressBar1.Value = 80;
                        AmtSum = dt.Compute("Sum(Amount)", "RemQty>0");
                        QtySum = dt.Compute("Sum(RemQty)", "RemQty>0");
                        lblTotalAmount.Text = Convert.ToString(AmtSum);
                        lbltotalqty.Text = Convert.ToString(QtySum);
                        GvPorderInfo.Refresh();
                        GvPorderInfo.Columns["ID"].Visible = false;
                        GvPorderInfo.Columns["ProductName"].Width = 140;
                        GvPorderInfo.Columns["MfgCompany"].Width = 120;
                        GvPorderInfo.Columns["Unit"].Width = 60;
                        GvPorderInfo.Columns["OpeningQty"].Width = 75;
                        GvPorderInfo.Columns["PurchaseQty"].Width = 75;
                        GvPorderInfo.Columns["PurReturnQty"].Width = 75;
                        GvPorderInfo.Columns["SaleQty"].Width = 75;
                        GvPorderInfo.Columns["SaleReturnQty"].Width = 75;
                        GvPorderInfo.Columns["DeadQty"].Width = 75;
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            GvPorderInfo.Columns["RemQty"].Width = 75;
                            GvPorderInfo.Columns["ProductRate"].Width = 75;
                            GvPorderInfo.Columns["Amount"].Width = 75;
                        }
                        DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                        bcol.HeaderText = "Show Detail";
                        bcol.Text = "Show Detail";
                        bcol.Name = "btnshowdetail";
                        bcol.FlatStyle = FlatStyle.System;
                        bcol.Width = 80;
                        bcol.UseColumnTextForButtonValue = true;
                        GvPorderInfo.Columns.Add(bcol);
                        progressBar1.Visible = false;
                        this.ActiveControl = cmbcomanyname;
                        dt.Dispose();
                    }
                    else if (cmbGroupName.SelectedIndex != 0)
                    {
                        DataTable dt = new DataTable();
                        progressBar1.Visible = true;
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = 100;
                        progressBar1.Value = 20;
                        GvPorderInfo.DataSource = null;
                        for (int i = 0; i < GvPorderInfo.Columns.Count; i++)
                        {
                            GvPorderInfo.Columns.RemoveAt(i);
                        }
                        progressBar1.Value = 40;
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            flag = 0;
                            progressBar1.Value = 60;

                            db.connect();
                            SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = cmbGroupName.SelectedValue.ToString();
                            command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@stock", SqlDbType.VarChar).Value = "DG";
                            command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'n';
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                            command.CommandTimeout = 300;
                            dt.Load(command.ExecuteReader());
                            GvPorderInfo.DataSource = dt;
                            db.CloseConnection();
                        }
                        else
                        {
                            progressBar1.Value = 60;
                            db.connect();
                            SqlCommand command = new SqlCommand("SPVatnWithoutVatGodownStock", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = cmbGroupName.SelectedValue.ToString();
                            command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@stock", SqlDbType.VarChar).Value = "DG";
                            command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'n';
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                            command.CommandTimeout = 300;
                            dt.Load(command.ExecuteReader());
                            GvPorderInfo.DataSource = dt;
                            db.CloseConnection();
                        }

                        progressBar1.Value = 80;
                        AmtSum = dt.Compute("Sum(Amount)", "RemQty>0");
                        QtySum = dt.Compute("Sum(RemQty)", "RemQty>0");
                        lblTotalAmount.Text = Convert.ToString(AmtSum);
                        lbltotalqty.Text = Convert.ToString(QtySum);
                        GvPorderInfo.Refresh();
                        GvPorderInfo.Columns["ID"].Visible = false;
                        GvPorderInfo.Columns["ProductName"].Width = 140;
                        GvPorderInfo.Columns["MfgCompany"].Width = 120;
                        GvPorderInfo.Columns["Unit"].Width = 60;
                        GvPorderInfo.Columns["OpeningQty"].Width = 75;
                        GvPorderInfo.Columns["PurchaseQty"].Width = 75;
                        GvPorderInfo.Columns["PurReturnQty"].Width = 75;
                        GvPorderInfo.Columns["SaleQty"].Width = 75;
                        GvPorderInfo.Columns["SaleReturnQty"].Width = 75;
                        GvPorderInfo.Columns["DeadQty"].Width = 75;
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            GvPorderInfo.Columns["RemQty"].Width = 75;
                            GvPorderInfo.Columns["ProductRate"].Width = 75;
                            GvPorderInfo.Columns["Amount"].Width = 75;
                        }
                        DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                        bcol.HeaderText = "Show Detail";
                        bcol.Text = "Show Detail";
                        bcol.Name = "btnshowdetail";
                        bcol.FlatStyle = FlatStyle.System;
                        bcol.Width = 80;
                        bcol.UseColumnTextForButtonValue = true;
                        GvPorderInfo.Columns.Add(bcol);
                        progressBar1.Visible = false;
                        this.ActiveControl = cmbcomanyname;
                        dt.Dispose();
                    }
                    else
                    {
                        DataTable dt = new DataTable();
                        progressBar1.Visible = true;
                        progressBar1.Minimum = 0;
                        progressBar1.Maximum = 100;
                        progressBar1.Value = 20;
                        GvPorderInfo.DataSource = null;
                        for (int i = 0; i < GvPorderInfo.Columns.Count; i++)
                        {
                            GvPorderInfo.Columns.RemoveAt(i);
                        }
                        progressBar1.Value = 40;
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            flag = 0;
                            progressBar1.Value = 60;

                            db.connect();
                            SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@stock", SqlDbType.VarChar).Value = "D";
                            command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'n';
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                            command.CommandTimeout = 300;
                            dt.Load(command.ExecuteReader());
                            GvPorderInfo.DataSource = dt;
                            db.CloseConnection();
                        }
                        else
                        {
                            progressBar1.Value = 60;
                            db.connect();
                            SqlCommand command = new SqlCommand("SPVatnWithoutVatGodownStock", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'D';
                            command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'n';
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                            command.CommandTimeout = 300;
                            dt.Load(command.ExecuteReader());
                            GvPorderInfo.DataSource = dt;
                            db.CloseConnection();
                        }
                        progressBar1.Value = 80;
                        AmtSum = dt.Compute("Sum(Amount)", "RemQty>0");
                        QtySum = dt.Compute("Sum(RemQty)", "RemQty>0");
                        lblTotalAmount.Text = Convert.ToString(AmtSum);
                        lbltotalqty.Text = Convert.ToString(QtySum);
                        GvPorderInfo.Refresh();
                        GvPorderInfo.Columns["ID"].Visible = false;
                        GvPorderInfo.Columns["ProductName"].Width = 140;
                        GvPorderInfo.Columns["MfgCompany"].Width = 120;
                        GvPorderInfo.Columns["Unit"].Width = 60;
                        GvPorderInfo.Columns["OpeningQty"].Width = 75;
                        GvPorderInfo.Columns["PurchaseQty"].Width = 75;
                        GvPorderInfo.Columns["PurReturnQty"].Width = 75;
                        GvPorderInfo.Columns["SaleQty"].Width = 75;
                        GvPorderInfo.Columns["SaleReturnQty"].Width = 75;
                        GvPorderInfo.Columns["DeadQty"].Width = 75;
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            GvPorderInfo.Columns["RemQty"].Width = 75;
                            GvPorderInfo.Columns["ProductRate"].Width = 75;
                            GvPorderInfo.Columns["Amount"].Width = 75;
                        }
                        DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                        bcol.HeaderText = "Show Detail";
                        bcol.Text = "Show Detail";
                        bcol.Name = "btnshowdetail";
                        bcol.FlatStyle = FlatStyle.System;
                        bcol.Width = 80;
                        bcol.UseColumnTextForButtonValue = true;
                        GvPorderInfo.Columns.Add(bcol);
                        progressBar1.Visible = false;
                        this.ActiveControl = dtpfromdate;
                        dt.Dispose();
                    }
                }
                else if (ChkProductType.Checked == true)
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        progressBar1.Value = 60;
                        string sprate = "";
                        SettingRepository settingrepo = new SettingRepository();
                        var settingdata = settingrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).LastOrDefault();
                        if (settingdata.StockOn == "Purchase Rate")
                            sprate = "p";
                        else if (settingdata.StockOn == "Sale Rate")
                            sprate = "s";
                        string d = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                        db.connect();
                        SqlCommand command = new SqlCommand("SPAssemblyProductRemData", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'n';
                        command.Parameters.Add("@SPRate", SqlDbType.VarChar).Value = sprate;
                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        command.CommandTimeout = 300;
                        dt.Load(command.ExecuteReader());
                        GvPorderInfo.DataSource = null;
                        GvPorderInfo.DataSource = dt;
                        AmtSum = dt.Compute("Sum(Amount)", "");
                        QtySum = dt.Compute("Sum(AvailableQty)", "");
                        db.CloseConnection();
                        progressBar1.Value = 80;
                        lblTotalAmount.Text = Convert.ToString(AmtSum);
                        lbltotalqty.Text = Convert.ToString(QtySum);
                        GvPorderInfo.Refresh();
                        GvPorderInfo.Columns["ID"].Visible = false;
                        GvPorderInfo.Columns["ProductName"].Width = 140;
                        GvPorderInfo.Columns["MfgCompany"].Width = 120;
                        GvPorderInfo.Columns["Unit"].Width = 60;
                        GvPorderInfo.Columns["OpeningQty"].Width = 75;
                        GvPorderInfo.Columns["PurchaseQty"].Width = 75;
                        GvPorderInfo.Columns["PurReturnQty"].Width = 75;
                        GvPorderInfo.Columns["SaleQty"].Width = 75;
                        GvPorderInfo.Columns["SaleReturnQty"].Width = 75;
                        GvPorderInfo.Columns["DeadQty"].Width = 75;
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            GvPorderInfo.Columns["RemQty"].Width = 75;
                            GvPorderInfo.Columns["ProductRate"].Width = 75;
                            GvPorderInfo.Columns["Amount"].Width = 75;
                        }
                        if (productentrytype != 0)
                        {
                            DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                            bcol.HeaderText = "Show Detail";
                            bcol.Text = "Show Detail";
                            bcol.Name = "btnshowdetail";
                            bcol.FlatStyle = FlatStyle.System;
                            bcol.UseColumnTextForButtonValue = true;
                            bcol.Width = 120;
                            GvPorderInfo.Columns.Add(bcol);
                        }
                    }
                    catch (Exception)
                    {
                        db.CloseConnection();
                    }
                }
                else
                {
                    if (txtProductname.Text != "")
                    {
                        cmbcomanyname.Text = "Select";
                        string Proname1 = txtProductname.Text.Trim().Split(',')[0];
                        string company = txtProductname.Text.Trim().Split(',')[1];
                        string unit = txtProductname.Text.Trim().Split(',')[2];
                        getProductType(Proname1, company, unit);

                        try
                        {
                            DataTable dt = new DataTable();

                            if (cmbGodownName.SelectedIndex == 0)
                            {
                                if (productentrytype != 9 && productentrytype != 6)
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;

                                    command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = Proname1;
                                    command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = company;
                                    command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = unit;
                                    command.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'P';
                                    command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'n';
                                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                                    command.CommandTimeout = 300;
                                    dt.Load(command.ExecuteReader());
                                }
                                else if (productentrytype == 6)
                                {
                                    try
                                    {
                                        db.connect();
                                        SqlCommand command = new SqlCommand("SPGetAssemblyProductRemQty", db.Connection);
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.Add("@proname", SqlDbType.VarChar).Value = Proname1;
                                        command.Parameters.Add("@cname", SqlDbType.VarChar).Value = company;
                                        command.Parameters.Add("@uname", SqlDbType.VarChar).Value = unit;
                                        command.Parameters.Add("@status", SqlDbType.VarChar).Value = "P";
                                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                                        command.Parameters.Add("@vat", SqlDbType.VarChar).Value = "n";
                                        command.CommandTimeout = 300;
                                        dt.Load(command.ExecuteReader());
                                    }
                                    catch (Exception) { db.CloseConnection(); }
                                }
                            }
                            else
                            {
                                if (productentrytype != 9)
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPVatnWithoutVatGodownStock", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;

                                    command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = Proname1;
                                    command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = company;
                                    command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = unit;
                                    command.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'P';
                                    command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'n';
                                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                    command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                                    command.CommandTimeout = 300;
                                    dt.Load(command.ExecuteReader());
                                }
                            }
                            GvPorderInfo.DataSource = null;
                            for (int i = 0; i < GvPorderInfo.Columns.Count; i++)
                            {
                                GvPorderInfo.Columns.RemoveAt(i);
                            }
                            GvPorderInfo.DataSource = dt;
                            GvPorderInfo.Columns["Id"].Visible = false;
                            GvPorderInfo.Columns["ProductName"].Width = 140;
                            GvPorderInfo.Columns["MfgCompany"].Width = 120;
                            GvPorderInfo.Columns["Unit"].Width = 60;
                            GvPorderInfo.Columns["OpeningQty"].Width = 75;
                            GvPorderInfo.Columns["PurchaseQty"].Width = 75;
                            GvPorderInfo.Columns["PurReturnQty"].Width = 75;
                            GvPorderInfo.Columns["SaleQty"].Width = 75;
                            GvPorderInfo.Columns["SaleReturnQty"].Width = 75;
                            GvPorderInfo.Columns["DeadQty"].Width = 75;
                            if (cmbGodownName.SelectedIndex == 0)
                            {
                                GvPorderInfo.Columns["RemQty"].Width = 75;
                                GvPorderInfo.Columns["ProductRate"].Width = 75;
                                GvPorderInfo.Columns["Amount"].Width = 75;
                            }
                            if (productentrytype != 6)
                            {
                                DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                                bcol.HeaderText = "Show Detail";
                                bcol.Text = "Show Detail";
                                bcol.Name = "btnshowdetail";
                                bcol.FlatStyle = FlatStyle.System;
                                bcol.UseColumnTextForButtonValue = true;
                                bcol.Width = 120;
                                GvPorderInfo.Columns.Add(bcol);
                            }
                            lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(Amount)", "RemQty>=0"));
                            lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(RemQty)", "RemQty>=0"));
                            db.CloseConnection();
                            this.ActiveControl = txtProductname;
                        }
                        catch (Exception)
                        { db.CloseConnection(); }
                    }
                    else
                    {
                        MessageBox.Show("Enter Product Name.", "Warning");
                        txtProductname.Text = "";
                        this.ActiveControl = txtProductname;
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
                txtProductname.Text = "";
                try
                {
                    progressBar1.Visible = true;
                    int minval = 0, maxval = 100;
                    progressBar1.Minimum = minval;
                    progressBar1.Maximum = maxval;
                    progressBar1.Value = minval + 20;
                    DataTable dt = new DataTable();
                    SettingRepository settingrepo = new SettingRepository();
                    progressBar1.Value = minval + 30;
                    progressBar1.Value = minval + 40;
                    if (cmbGodownName.SelectedIndex == 0)
                    {
                        db.connect();
                        progressBar1.Value = minval + 15;
                        SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        progressBar1.Value = minval + 20;
                        command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                        command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = cmbcomanyname.Text;
                        command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                        progressBar1.Value = minval + 25;
                        command.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'C';
                        command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'n';
                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                        progressBar1.Value = minval + 30;
                        command.CommandTimeout = 300;
                        dt.Load(command.ExecuteReader());
                        progressBar1.Value = minval + 35;
                    }
                    else
                    {
                        db.connect();
                        progressBar1.Value = minval + 15;
                        SqlCommand command = new SqlCommand("SPVatnWithoutVatGodownStock", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        progressBar1.Value = minval + 20;
                        command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                        command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = cmbcomanyname.Text;
                        command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                        progressBar1.Value = minval + 25;
                        command.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'C';
                        command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'n';
                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                        command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                        progressBar1.Value = minval + 30;
                        command.CommandTimeout = 300;
                        dt.Load(command.ExecuteReader());
                        progressBar1.Value = minval + 35;
                    }
                    GvPorderInfo.DataSource = null;
                    for (int i = 0; i < GvPorderInfo.Columns.Count; i++)
                    {
                        GvPorderInfo.Columns.RemoveAt(i);
                    }
                    progressBar1.Value = minval + 40;
                    GvPorderInfo.DataSource = dt;
                    GvPorderInfo.Columns["Id"].Visible = false;
                    GvPorderInfo.Columns["ProductName"].Width = 140;
                    GvPorderInfo.Columns["MfgCompany"].Width = 120;
                    GvPorderInfo.Columns["Unit"].Width = 60;
                    progressBar1.Value = minval + 50;
                    GvPorderInfo.Columns["OpeningQty"].Width = 75;
                    GvPorderInfo.Columns["PurchaseQty"].Width = 75;
                    GvPorderInfo.Columns["PurReturnQty"].Width = 75;
                    GvPorderInfo.Columns["SaleQty"].Width = 75;
                    GvPorderInfo.Columns["SaleReturnQty"].Width = 75;
                    GvPorderInfo.Columns["DeadQty"].Width = 75;
                    progressBar1.Value = minval + 60;
                    if (cmbGodownName.SelectedIndex == 0)
                    {
                        GvPorderInfo.Columns["RemQty"].Width = 75;
                        GvPorderInfo.Columns["ProductRate"].Width = 75;
                        GvPorderInfo.Columns["Amount"].Width = 75;
                    }
                    progressBar1.Value = minval + 70;
                    DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                    bcol.HeaderText = "Show Detail";
                    bcol.Text = "Show Detail";
                    bcol.Name = "btnshowdetail";
                    bcol.FlatStyle = FlatStyle.System;
                    bcol.UseColumnTextForButtonValue = true;
                    bcol.Width = 120;
                    GvPorderInfo.Columns.Add(bcol);
                    progressBar1.Value = minval + 80;
                    lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(Amount)", "RemQty>=0"));
                    lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(RemQty)", "RemQty>=0"));
                    db.CloseConnection();
                    this.ActiveControl = cmbcomanyname;
                    progressBar1.Value = minval + 100;
                    progressBar1.Visible = false;
                }
                catch (Exception)
                { db.CloseConnection(); progressBar1.Visible = false; }
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
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtProductname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cmbcomanyname.Text = "Select";
            }
            catch (Exception)
            { }
        }

        private void txtProductname_Leave(object sender, EventArgs e)
        {
            try
            {
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

        private void GvPorderInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GvPorderInfo.Columns[e.ColumnIndex].Name == "btnshowdetail")
                {
                    frmshowdetailofstock showdetailstock = new frmshowdetailofstock(GvPorderInfo.Rows[e.RowIndex].Cells["ProductName"].Value.ToString(), GvPorderInfo.Rows[e.RowIndex].Cells["MfgCompany"].Value.ToString(), GvPorderInfo.Rows[e.RowIndex].Cells["Unit"].Value.ToString(), "n", Convert.ToInt32(cmbGodownName.SelectedValue));
                    showdetailstock.ShowDialog();
                }
            }
            catch (Exception)
            { }
        }

        private void btnCompoundUnitStock_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                if (cmbGodownName.SelectedIndex == 0)
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPCompoundUnitStock", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Stock", SqlDbType.NVarChar).Value = 'n';
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                    command.CommandTimeout = 300;
                    dt.Load(command.ExecuteReader());
                }
                else
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPCompoundUnitGodownStock", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Stock", SqlDbType.NVarChar).Value = 'n';
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                    command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                    command.CommandTimeout = 300;
                    dt.Load(command.ExecuteReader());
                }

                GvPorderInfo.DataSource = null;
                for (int i = 0; i < GvPorderInfo.Columns.Count; i++)
                {
                    GvPorderInfo.Columns.RemoveAt(i);
                }
                GvPorderInfo.DataSource = dt;
                GvPorderInfo.Refresh();
                GvPorderInfo.Columns["ID"].Visible = false;
                GvPorderInfo.Columns[1].Width = 140;
                GvPorderInfo.Columns[2].Width = 140;
                GvPorderInfo.Columns[3].Width = 60;
                GvPorderInfo.Columns[4].Width = 60;
                GvPorderInfo.Columns[5].Width = 85;
                GvPorderInfo.Columns[6].Width = 85;
                GvPorderInfo.Columns[7].Width = 85;
                GvPorderInfo.Columns[8].Width = 85;
                GvPorderInfo.Columns[9].Width = 85;
                GvPorderInfo.Columns[10].Width = 85;
                if (cmbGodownName.SelectedIndex == 0)
                {
                    GvPorderInfo.Columns[11].Width = 85;
                    GvPorderInfo.Columns[12].Width = 85;
                    GvPorderInfo.Columns[13].Width = 85;
                }
                DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                bcol.HeaderText = "Show Detail";
                bcol.Text = "Show Detail";
                bcol.Name = "btnshowdetail";
                bcol.FlatStyle = FlatStyle.System;
                bcol.UseColumnTextForButtonValue = true;
                bcol.Width = 120;
                GvPorderInfo.Columns.Add(bcol);
                this.ActiveControl = txtProductname;
                dt.Dispose();
                lblTotalAmount.Text = "";
                lbltotalqty.Text = "";
                db.CloseConnection();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void cmbGodownName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtProductname.Text = "";
            try
            {
                if (flag == 1)
                {
                    progressBar1.Visible = true;
                    int minval = 0, maxval = 100;
                    progressBar1.Minimum = minval;
                    progressBar1.Maximum = maxval;
                    progressBar1.Value = minval + 20;
                    DataTable dt = new DataTable();
                    progressBar1.Value = minval + 30;
                    progressBar1.Value = minval + 40;
                    if (cmbGodownName.SelectedIndex == 0 && cmbGodownName.Enabled == true)
                    {
                        db.connect();
                        progressBar1.Value = minval + 15;
                        SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        progressBar1.Value = minval + 20;
                        command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                        command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                        command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                        progressBar1.Value = minval + 25;
                        command.Parameters.Add("@stock", SqlDbType.VarChar).Value = '0';
                        command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'n';
                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                        progressBar1.Value = minval + 30;
                        command.CommandTimeout = 300;
                        dt.Load(command.ExecuteReader());
                        progressBar1.Value = minval + 35;
                    }
                    else
                    {
                        db.connect();
                        progressBar1.Value = minval + 15;
                        SqlCommand command = new SqlCommand("SPVatnWithoutVatGodownStock", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        progressBar1.Value = minval + 20;
                        command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                        command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                        command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                        progressBar1.Value = minval + 25;
                        command.Parameters.Add("@stock", SqlDbType.VarChar).Value = '0';
                        command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'n';
                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                        command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                        progressBar1.Value = minval + 30;
                        command.CommandTimeout = 300;
                        dt.Load(command.ExecuteReader());
                        progressBar1.Value = minval + 35;
                    }
                    GvPorderInfo.DataSource = null;
                    for (int i = 0; i < GvPorderInfo.Columns.Count; i++)
                    {
                        GvPorderInfo.Columns.RemoveAt(i);
                    }
                    progressBar1.Value = minval + 40;
                    GvPorderInfo.DataSource = dt;
                    GvPorderInfo.Columns["Id"].Visible = false;
                    GvPorderInfo.Columns["ProductName"].Width = 140;
                    GvPorderInfo.Columns["MfgCompany"].Width = 120;
                    GvPorderInfo.Columns["Unit"].Width = 60;
                    progressBar1.Value = minval + 50;
                    GvPorderInfo.Columns["OpeningQty"].Width = 75;
                    GvPorderInfo.Columns["PurchaseQty"].Width = 75;
                    GvPorderInfo.Columns["PurReturnQty"].Width = 75;
                    GvPorderInfo.Columns["SaleQty"].Width = 75;
                    GvPorderInfo.Columns["SaleReturnQty"].Width = 75;
                    GvPorderInfo.Columns["DeadQty"].Width = 75;
                    progressBar1.Value = minval + 60;
                    if (cmbGodownName.SelectedIndex == 0)
                    {
                        GvPorderInfo.Columns["RemQty"].Width = 75;
                        GvPorderInfo.Columns["ProductRate"].Width = 75;
                        GvPorderInfo.Columns["Amount"].Width = 75;
                    }
                    progressBar1.Value = minval + 70;
                    DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                    bcol.HeaderText = "Show Detail";
                    bcol.Text = "Show Detail";
                    bcol.Name = "btnshowdetail";
                    bcol.FlatStyle = FlatStyle.System;
                    bcol.UseColumnTextForButtonValue = true;
                    bcol.Width = 120;
                    GvPorderInfo.Columns.Add(bcol);
                    progressBar1.Value = minval + 80;
                    lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(Amount)", "RemQty>=0"));
                    lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(RemQty)", "RemQty>=0"));
                    db.CloseConnection();
                    this.ActiveControl = txtProductname;
                    progressBar1.Value = minval + 100;
                    progressBar1.Visible = false;
                }
            }
            catch (Exception)
            { db.CloseConnection(); progressBar1.Visible = false; }
        }

        private void cmbGroupName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtProductname.Text = "";
                if (cmbGroupName.SelectedIndex != 0)
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = cmbGroupName.SelectedValue.ToString();
                            command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'G';
                            command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'n';
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                            command.CommandTimeout = 300;
                            dt.Load(command.ExecuteReader());
                        }
                        else
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPVatnWithoutVatGodownStock", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = cmbGroupName.SelectedValue.ToString();
                            command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'G';
                            command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'n';
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                            command.CommandTimeout = 300;
                            dt.Load(command.ExecuteReader());
                        }
                        GvPorderInfo.DataSource = null;
                        for (int i = 0; i < GvPorderInfo.Columns.Count; i++)
                        {
                            GvPorderInfo.Columns.RemoveAt(i);
                        }
                        GvPorderInfo.DataSource = dt;
                        GvPorderInfo.Columns["Id"].Visible = false;
                        GvPorderInfo.Columns["ProductName"].Width = 140;
                        GvPorderInfo.Columns["MfgCompany"].Width = 120;
                        GvPorderInfo.Columns["Unit"].Width = 60;
                        GvPorderInfo.Columns["OpeningQty"].Width = 75;
                        GvPorderInfo.Columns["PurchaseQty"].Width = 75;
                        GvPorderInfo.Columns["PurReturnQty"].Width = 75;
                        GvPorderInfo.Columns["SaleQty"].Width = 75;
                        GvPorderInfo.Columns["SaleReturnQty"].Width = 75;
                        GvPorderInfo.Columns["DeadQty"].Width = 75;
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            GvPorderInfo.Columns["RemQty"].Width = 75;
                            GvPorderInfo.Columns["ProductRate"].Width = 75;
                            GvPorderInfo.Columns["Amount"].Width = 75;
                        }
                        DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                        bcol.HeaderText = "Show Detail";
                        bcol.Text = "Show Detail";
                        bcol.Name = "btnshowdetail";
                        bcol.FlatStyle = FlatStyle.System;
                        bcol.UseColumnTextForButtonValue = true;
                        bcol.Width = 120;
                        GvPorderInfo.Columns.Add(bcol);
                        lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(Amount)", "RemQty>=0"));
                        lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(RemQty)", "RemQty>=0"));
                        db.CloseConnection();
                    }
                    catch (Exception)
                    { db.CloseConnection(); }
                    this.ActiveControl = cmbGroupName;
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
                    this.ActiveControl = btncheck;
            }
            catch (Exception) { }
        }

        private void cmbcomanyname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (chkDate.Checked == true)
                        this.ActiveControl = btncheck;
                }
            }
            catch (Exception) { }
        }

        private void cmbGroupName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (chkDate.Checked == true)
                        this.ActiveControl = btncheck;
                }
            }
            catch (Exception) { }
        }

        private void cmbGodownName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    if (chkDate.Checked == true)
                        this.ActiveControl = btncheck;
                }
            }
            catch (Exception) { }
        }

        private void chkDate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpfromdate;
            }
            catch (Exception) { }
        }

        private void dtpfromdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btncheck;
            }
            catch (Exception) { }
        }

        private void ChkProductType_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChkProductType.Checked == true)
                {
                    chkDate.Checked = false;
                    dtpfromdate.Value = DateTime.Now.Date;
                    txtProductname.Text = "";
                    FillGodownName();
                    fillcombo();
                    fillGroupName();
                }
            }
            catch (Exception) { }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            txtProductname.Text = "";
            try
            {
                progressBar1.Visible = true;
                int minval = 0, maxval = 100;
                progressBar1.Minimum = minval;
                progressBar1.Maximum = maxval;
                progressBar1.Value = minval + 20;
                DataTable dt = new DataTable();
                SettingRepository settingrepo = new SettingRepository();
                progressBar1.Value = minval + 30;
                progressBar1.Value = minval + 40;
                if (cmbGodownName.SelectedIndex == 0)
                {
                    db.connect();
                    progressBar1.Value = minval + 15;
                    SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    progressBar1.Value = minval + 20;
                    command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                    command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                    command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                    progressBar1.Value = minval + 25;
                    command.Parameters.Add("@stock", SqlDbType.VarChar).Value = '0';
                    command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'n';
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                    progressBar1.Value = minval + 30;
                    command.CommandTimeout = 300;
                    dt.Load(command.ExecuteReader());
                    progressBar1.Value = minval + 35;
                }
                else
                {
                    db.connect();
                    progressBar1.Value = minval + 15;
                    SqlCommand command = new SqlCommand("SPVatnWithoutVatGodownStock", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    progressBar1.Value = minval + 20;
                    command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                    command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                    command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                    progressBar1.Value = minval + 25;
                    command.Parameters.Add("@stock", SqlDbType.VarChar).Value = '0';
                    command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'n';
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                    command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                    progressBar1.Value = minval + 30;
                    command.CommandTimeout = 300;
                    dt.Load(command.ExecuteReader());
                    progressBar1.Value = minval + 35;
                }
                GvPorderInfo.DataSource = null;
                for (int i = 0; i < GvPorderInfo.Columns.Count; i++)
                {
                    GvPorderInfo.Columns.RemoveAt(i);
                }
                progressBar1.Value = minval + 40;
                GvPorderInfo.DataSource = dt;
                GvPorderInfo.Columns["Id"].Visible = false;
                GvPorderInfo.Columns["ProductName"].Width = 140;
                GvPorderInfo.Columns["MfgCompany"].Width = 120;
                GvPorderInfo.Columns["Unit"].Width = 60;
                progressBar1.Value = minval + 50;
                GvPorderInfo.Columns["OpeningQty"].Width = 75;
                GvPorderInfo.Columns["PurchaseQty"].Width = 75;
                GvPorderInfo.Columns["PurReturnQty"].Width = 75;
                GvPorderInfo.Columns["SaleQty"].Width = 75;
                GvPorderInfo.Columns["SaleReturnQty"].Width = 75;
                GvPorderInfo.Columns["DeadQty"].Width = 75;
                progressBar1.Value = minval + 60;
                if (cmbGodownName.SelectedIndex == 0)
                {
                    GvPorderInfo.Columns["RemQty"].Width = 75;
                    GvPorderInfo.Columns["ProductRate"].Width = 75;
                    GvPorderInfo.Columns["Amount"].Width = 75;
                }
                progressBar1.Value = minval + 70;
                DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                bcol.HeaderText = "Show Detail";
                bcol.Text = "Show Detail";
                bcol.Name = "btnshowdetail";
                bcol.FlatStyle = FlatStyle.System;
                bcol.UseColumnTextForButtonValue = true;
                bcol.Width = 120;
                GvPorderInfo.Columns.Add(bcol);
                progressBar1.Value = minval + 80;
                lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(Amount)", "RemQty>=0"));
                lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(RemQty)", "RemQty>=0"));
                db.CloseConnection();
                this.ActiveControl = txtProductname;
                progressBar1.Value = minval + 100;
                progressBar1.Visible = false;
            }
            catch (Exception)
            { db.CloseConnection(); progressBar1.Visible = false; }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                    this.Close();
            }
            catch (Exception)
            { }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProductname.Text != "")
                {
                    RptRemainingtock remainingqty = new RptRemainingtock(null, txtProductname.Text, 'n');
                    remainingqty.ShowDialog();
                    remainingqty.Dispose();
                }
                else if (cmbcomanyname.Text != "Select" && cmbcomanyname.SelectedIndex > 0)
                {
                    RptRemainingtock remainingqty = new RptRemainingtock(cmbcomanyname.Text, null, 'n');
                    remainingqty.ShowDialog();
                    remainingqty.Dispose();
                }
                else if (chkDate.Checked)
                {
                    string d = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                    RptRemainingtock remainingqty = new RptRemainingtock(d, 'n');
                    remainingqty.ShowDialog();
                    remainingqty.Dispose();
                }
                else
                {
                    RptRemainingtock remainingqty = new RptRemainingtock('n');
                    remainingqty.ShowDialog();
                    remainingqty.Dispose();
                }
            }
            catch (Exception)
            { }
        }
    }
}