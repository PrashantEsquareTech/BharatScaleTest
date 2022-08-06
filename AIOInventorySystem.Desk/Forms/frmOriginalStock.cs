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
    public partial class frmOriginalStock : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        RptRemainingtock remainingqty = new RptRemainingtock();
        RemainingtockCrystalReport Stockorpt = new RemainingtockCrystalReport();
        string companyname = null, Proname = null;
        public int flag = 0, productentrytype = 0;

        public frmOriginalStock()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            fillcombo();
            FillGodownName();
            fillGroupName();
        }

        public void LoadData()
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT [Id], [ProductName] as 'ProductName', [MfgCompany] as 'MfgCompany', [Unit],Size, OpeningQty as 'Opening Qty', "+
                    "[PurchaseQty] as 'Purchase Qty', [PurReturnQty] as 'Pur Return Qty', [SaleQty] as 'Sale Qty', [SaleReturnQty] as 'Sale return Qty', [DeadQty] as 'Dead Qty', "+
                    "[RemQty] as 'RemQty', [ProductRate] as 'Product Rate', cast((RemQty*ProductRate) as decimal(18,2)) as 'RemStockAmt' FROM [Stock] "+
                    "where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + " ", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvPorderInfo.DataSource = null;
                for (int i = 0; i < GvPorderInfo.Columns.Count; i++)
                {
                    GvPorderInfo.Columns.RemoveAt(i);
                }
                GvPorderInfo.DataSource = dt;
                lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(RemStockAmt)", "RemQty>=0"));
                lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(RemQty)", "RemQty>=0"));
                GvPorderInfo.Refresh();
                GvPorderInfo.Columns["ID"].Visible = false;

                GvPorderInfo.Columns["ProductName"].Width = 140;
                GvPorderInfo.Columns["MfgCompany"].Width = 140;
                GvPorderInfo.Columns["Unit"].Width = 60;
                GvPorderInfo.Columns["Size"].Width = 60;
                GvPorderInfo.Columns["Opening Qty"].Width = 85;
                GvPorderInfo.Columns["Purchase Qty"].Width = 85;
                GvPorderInfo.Columns["Pur Return Qty"].Width = 85;
                GvPorderInfo.Columns["Sale Qty"].Width = 85;
                GvPorderInfo.Columns["Sale return Qty"].Width = 85;
                GvPorderInfo.Columns["Dead Qty"].Width = 85;
                GvPorderInfo.Columns["RemQty"].Width = 85;
                GvPorderInfo.Columns["Product Rate"].Width = 85;
                GvPorderInfo.Columns["RemStockAmt"].Width = 85;

                DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                bcol.HeaderText = "Show Detail";
                bcol.Text = "Show Detail";
                bcol.Name = "btnshowdetail";
                bcol.FlatStyle = FlatStyle.System;
                bcol.UseColumnTextForButtonValue = true;
                GvPorderInfo.Columns.Add(bcol);
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        public void fillcombo()
        {
            try
            {
                DataTable dt = null;
                if (CommonMethod.commProduct == true)
                    dt = db.Gettable("Select Distinct(ManufactureCompany) from ProductInformation order by ManufactureCompany");
                else
                    dt = db.Gettable("Select Distinct(ManufactureCompany) from ProductInformation where compid=" + CommonMethod.CompId + " order by ManufactureCompany");
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

        public void FillGodownName()
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
                    cmbGodownName.SelectedIndex = 0;
                    cmbGodownName.Enabled = false;
                }
                cmbGodownName.SelectedIndex = 0;
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
                cmbGroupName.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        private void frmOriginalStock_Load(object sender, EventArgs e)
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
            catch (Exception)
            { }
        }

        private void btncheck_Click(object sender, EventArgs e)
        {
            try
            {
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
                                command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'a';
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
                            if (productentrytype != 9 && productentrytype != 6)
                            {
                                db.connect();
                                SqlCommand command = new SqlCommand("SPVatnWithoutVatGodownStock", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;

                                command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = Proname1;
                                command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = company;
                                command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = unit;
                                command.Parameters.Add("@stock", SqlDbType.VarChar).Value = "DP";
                                command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'a';
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
                        try
                        {
                            //if (!string.IsNullOrEmpty(dt.Compute("Count(Size)", "Size=''").ToString()))
                            //    GvPorderInfo.Columns["Size"].Visible = false;
                            //else
                            //    GvPorderInfo.Columns["Size"].Visible = true;
                            if (string.IsNullOrEmpty(dt.Compute("Sum(Amount)", "RemQty>=0").ToString()))
                                lblTotalAmount.Text = "0";
                            else
                                lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(Amount)", "RemQty>=0"));
                            if (string.IsNullOrEmpty(dt.Compute("Sum(RemQty)", "RemQty>=0").ToString()))
                                lbltotalqty.Text = "0";
                            else
                                lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(RemQty)", "RemQty>=0"));
                        }
                        catch (Exception)
                        {
                            if (string.IsNullOrEmpty(dt.Compute("Sum(RemStockAmt)", "RemQty>=0").ToString()))
                                lblTotalAmount.Text = "0";
                            else
                                lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(RemStockAmt)", "RemQty>=0"));
                            if (string.IsNullOrEmpty(dt.Compute("Sum(RemQty)", "RemQty>=0").ToString()))
                                lbltotalqty.Text = "0";
                            else
                                lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(RemQty)", "RemQty>=0"));
                        }                        
                        GvPorderInfo.Refresh();
                        GvPorderInfo.Columns["ID"].Visible = false;
                        GvPorderInfo.Columns[1].Width = 140;
                        GvPorderInfo.Columns[2].Width = 140;
                        GvPorderInfo.Columns[3].Width = 60;
                        GvPorderInfo.Columns[4].Width = 85;
                        GvPorderInfo.Columns[5].Width = 85;
                        GvPorderInfo.Columns[6].Width = 85;
                        GvPorderInfo.Columns[7].Width = 85;
                        GvPorderInfo.Columns[8].Width = 60;
                        GvPorderInfo.Columns[9].Width = 85;
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            GvPorderInfo.Columns[10].Width = 85;
                            GvPorderInfo.Columns[11].Width = 85;
                            GvPorderInfo.Columns[12].Width = 85;
                        }
                        DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                        bcol.HeaderText = "Show Detail";
                        bcol.Text = "Show Detail";
                        bcol.Name = "btnshowdetail";
                        bcol.FlatStyle = FlatStyle.System;
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
                            string d = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");

                            db.connect();
                            SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = cmbcomanyname.Text;
                            command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@stock", SqlDbType.VarChar).Value = "DC";
                            command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'a';
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = d;
                            command.CommandTimeout = 300;
                            dt.Load(command.ExecuteReader());
                            GvPorderInfo.DataSource = dt;
                            db.CloseConnection();
                        }
                        else
                        {
                            progressBar1.Value = 60;
                            string d = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                            db.connect();
                            SqlCommand command = new SqlCommand("SPVatnWithoutVatGodownStock", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = cmbcomanyname.Text;
                            command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@stock", SqlDbType.VarChar).Value = "DC";
                            command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'a';
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = d;
                            command.CommandTimeout = 300;
                            dt.Load(command.ExecuteReader());
                            GvPorderInfo.DataSource = dt;
                            db.CloseConnection();
                        }
                        if (string.IsNullOrEmpty(dt.Compute("Sum(Amount)", "RemQty>=0").ToString()))
                            lblTotalAmount.Text = "0";
                        else
                            lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(Amount)", "RemQty>=0"));
                        if (string.IsNullOrEmpty(dt.Compute("Sum(RemQty)", "RemQty>=0").ToString()))
                            lbltotalqty.Text = "0";
                        else
                            lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(RemQty)", "RemQty>=0"));
                        progressBar1.Value = 80;
                        GvPorderInfo.Refresh();
                        GvPorderInfo.Columns["ID"].Visible = false;
                        GvPorderInfo.Columns[1].Width = 140;
                        GvPorderInfo.Columns[2].Width = 140;
                        GvPorderInfo.Columns[3].Width = 60;
                        GvPorderInfo.Columns[4].Width = 85;
                        GvPorderInfo.Columns[5].Width = 85;
                        GvPorderInfo.Columns[6].Width = 85;
                        GvPorderInfo.Columns[7].Width = 85;
                        GvPorderInfo.Columns[8].Width = 60;
                        GvPorderInfo.Columns[9].Width = 85;
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            GvPorderInfo.Columns[10].Width = 85;
                            GvPorderInfo.Columns[11].Width = 85;
                            GvPorderInfo.Columns[12].Width = 85;
                        }
                        DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                        bcol.HeaderText = "Show Detail";
                        bcol.Text = "Show Detail";
                        bcol.Name = "btnshowdetail";
                        bcol.FlatStyle = FlatStyle.System;
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
                            string d = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                            db.connect();
                            SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = cmbGroupName.SelectedValue.ToString();
                            command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@stock", SqlDbType.VarChar).Value = "DG";
                            command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'a';
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = d;
                            command.CommandTimeout = 300;
                            dt.Load(command.ExecuteReader());
                            GvPorderInfo.DataSource = dt;
                            db.CloseConnection();
                        }
                        else
                        {
                            progressBar1.Value = 60;
                            string d = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                            db.connect();
                            SqlCommand command = new SqlCommand("SPVatnWithoutVatGodownStock", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = cmbGroupName.SelectedValue.ToString();
                            command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@stock", SqlDbType.VarChar).Value = "DG";
                            command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'a';
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = d;
                            command.CommandTimeout = 300;
                            dt.Load(command.ExecuteReader());
                            GvPorderInfo.DataSource = dt;
                            db.CloseConnection();
                        }
                        if (string.IsNullOrEmpty(dt.Compute("Sum(Amount)", "RemQty>=0").ToString()))
                            lblTotalAmount.Text = "0";
                        else
                            lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(Amount)", "RemQty>=0"));
                        if (string.IsNullOrEmpty(dt.Compute("Sum(RemQty)", "RemQty>=0").ToString()))
                            lbltotalqty.Text = "0";
                        else
                            lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(RemQty)", "RemQty>=0"));
                        progressBar1.Value = 80;
                        GvPorderInfo.Refresh();
                        GvPorderInfo.Columns["ID"].Visible = false;
                        GvPorderInfo.Columns[1].Width = 140;
                        GvPorderInfo.Columns[2].Width = 140;
                        GvPorderInfo.Columns[3].Width = 60;
                        GvPorderInfo.Columns[4].Width = 85;
                        GvPorderInfo.Columns[5].Width = 85;
                        GvPorderInfo.Columns[6].Width = 85;
                        GvPorderInfo.Columns[7].Width = 85;
                        GvPorderInfo.Columns[8].Width = 60;
                        GvPorderInfo.Columns[9].Width = 85;
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            GvPorderInfo.Columns[10].Width = 85;
                            GvPorderInfo.Columns[11].Width = 85;
                            GvPorderInfo.Columns[12].Width = 85;
                        }
                        DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                        bcol.HeaderText = "Show Detail";
                        bcol.Text = "Show Detail";
                        bcol.Name = "btnshowdetail";
                        bcol.FlatStyle = FlatStyle.System;
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
                        progressBar1.Value = 50;
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            flag = 0;
                            progressBar1.Value = 60;
                            string d = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                            db.connect();
                            SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@stock", SqlDbType.VarChar).Value = "D";
                            command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'a';
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = d;
                            command.CommandTimeout = 300;
                            dt.Load(command.ExecuteReader());
                            GvPorderInfo.DataSource = dt;
                            db.CloseConnection();
                        }
                        else
                        {
                            progressBar1.Value = 60;
                            string d = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                            db.connect();
                            SqlCommand command = new SqlCommand("SPVatnWithoutVatGodownStock", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                            command.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'D';
                            command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'a';
                            command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                            command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = d;
                            command.CommandTimeout = 300;
                            dt.Load(command.ExecuteReader());
                            GvPorderInfo.DataSource = dt;
                            db.CloseConnection();
                        }
                        if (string.IsNullOrEmpty(dt.Compute("Sum(Amount)", "RemQty>=0").ToString()))
                            lblTotalAmount.Text = "0";
                        else
                            lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(Amount)", "RemQty>=0"));
                        if (string.IsNullOrEmpty(dt.Compute("Sum(RemQty)", "RemQty>=0").ToString()))
                            lbltotalqty.Text = "0";
                        else
                            lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(RemQty)", "RemQty>=0"));
                        progressBar1.Value = 80;
                        GvPorderInfo.Refresh();
                        GvPorderInfo.Columns["ID"].Visible = false;
                        GvPorderInfo.Columns[1].Width = 140;
                        GvPorderInfo.Columns[2].Width = 140;
                        GvPorderInfo.Columns[3].Width = 60;
                        GvPorderInfo.Columns[4].Width = 85;
                        GvPorderInfo.Columns[5].Width = 85;
                        GvPorderInfo.Columns[6].Width = 85;
                        GvPorderInfo.Columns[7].Width = 85;
                        GvPorderInfo.Columns[8].Width = 60;
                        GvPorderInfo.Columns[9].Width = 85;
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            GvPorderInfo.Columns[10].Width = 85;
                            GvPorderInfo.Columns[11].Width = 85;
                            GvPorderInfo.Columns[12].Width = 85;
                        }
                        DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                        bcol.HeaderText = "Show Detail";
                        bcol.Text = "Show Detail";
                        bcol.Name = "btnshowdetail";
                        bcol.FlatStyle = FlatStyle.System;
                        bcol.UseColumnTextForButtonValue = true;
                        GvPorderInfo.Columns.Add(bcol);
                        progressBar1.Visible = false;
                        this.ActiveControl = dtpfromdate;
                        dt.Dispose();
                    }
                }
                else if (chkAssemeblyProduct.Checked == true)
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
                        command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'a';
                        command.Parameters.Add("@SPRate", SqlDbType.VarChar).Value = sprate;
                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        command.CommandTimeout = 300;
                        dt.Load(command.ExecuteReader());
                        GvPorderInfo.DataSource = null;
                        GvPorderInfo.DataSource = dt;
                        if (string.IsNullOrEmpty(dt.Compute("Sum(Amount)", "").ToString()))
                            lblTotalAmount.Text = "0";
                        else
                            lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(Amount)", ""));

                        if (string.IsNullOrEmpty(dt.Compute("Sum(AvailableQty)", "").ToString()))
                            lbltotalqty.Text = "0";
                        else
                            lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(AvailableQty)", ""));
                        db.CloseConnection();

                        progressBar1.Value = 80;
                        GvPorderInfo.Refresh();
                        GvPorderInfo.Columns["ID"].Visible = false;
                        GvPorderInfo.Columns[0].Visible = false;
                        GvPorderInfo.Columns[1].Width = 140;
                        GvPorderInfo.Columns[2].Width = 140;
                        GvPorderInfo.Columns[3].Width = 60;
                        GvPorderInfo.Columns[4].Width = 85;
                        GvPorderInfo.Columns[5].Width = 85;
                        GvPorderInfo.Columns[6].Width = 85;
                        GvPorderInfo.Columns[7].Width = 85;
                        GvPorderInfo.Columns[8].Width = 60;
                        GvPorderInfo.Columns[9].Width = 85;
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
                else if (chkSize.Checked == true)
                {
                    try
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
                                if (productentrytype != 6)
                                {
                                    flag = 0;
                                    string Proname = "";
                                    if ((Proname1).Contains("'"))
                                        Proname = Proname1.Replace("'", "''");
                                    else
                                        Proname = Proname1;
                                    SqlDataAdapter da = new SqlDataAdapter("SELECT [Id],[ProductName] as 'ProductName' ,[MfgCompany] as 'MfgCompany',[Unit],Size,OpeningQty as 'Opening Qty',[PurchaseQty] as 'Purchase Qty',[PurReturnQty] as 'Pur Return Qty',[SaleQty] as 'Sale Qty',[SaleReturnQty] as 'Sale return Qty',[DeadQty] as 'Dead Qty',[RemQty] as 'RemQty',[ProductRate] as 'Product Rate',cast((RemQty*ProductRate) as decimal(18,2)) as 'RemStockAmt' FROM [Stock] Where [ProductName]='" + Proname + "' and MfgCompany='" + company + "' and  Unit='" + unit + "' and Size='" + txtSize.Text + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                                    da.Fill(dt);
                                    GvPorderInfo.DataSource = dt;
                                    db.CloseConnection();
                                }
                                else
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
                                        command.Parameters.Add("@vat", SqlDbType.VarChar).Value = "a";
                                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);

                                        command.CommandTimeout = 300;
                                        dt.Load(command.ExecuteReader());
                                        GvPorderInfo.DataSource = dt;
                                        db.CloseConnection();
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
                                    command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'a';
                                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                    command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                                    command.CommandTimeout = 300;
                                    dt.Load(command.ExecuteReader());
                                    GvPorderInfo.DataSource = dt;
                                    db.CloseConnection();
                                }
                            }
                            try
                            {
                                if (string.IsNullOrEmpty(dt.Compute("Sum(Amount)", "RemQty>=0").ToString()))
                                    lblTotalAmount.Text = "0";
                                else
                                    lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(Amount)", "RemQty>=0"));
                                if (string.IsNullOrEmpty(dt.Compute("Sum(RemQty)", "RemQty>=0").ToString()))
                                    lbltotalqty.Text = "0";
                                else
                                    lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(RemQty)", "RemQty>=0"));
                            }
                            catch (Exception)
                            {
                                if (string.IsNullOrEmpty(dt.Compute("Sum(RemStockAmt)", "RemQty>=0").ToString()))
                                    lblTotalAmount.Text = "0";
                                else
                                    lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(RemStockAmt)", "RemQty>=0"));
                                if (string.IsNullOrEmpty(dt.Compute("Sum(RemQty)", "RemQty>=0").ToString()))
                                    lbltotalqty.Text = "0";
                                else
                                    lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(RemQty)", "RemQty>=0"));
                            }
                            GvPorderInfo.Refresh();
                            GvPorderInfo.Columns["ID"].Visible = false;
                            GvPorderInfo.Columns[1].Width = 140;
                            GvPorderInfo.Columns[2].Width = 140;
                            GvPorderInfo.Columns[3].Width = 60;
                            GvPorderInfo.Columns[4].Width = 85;
                            GvPorderInfo.Columns[5].Width = 85;
                            GvPorderInfo.Columns[6].Width = 85;
                            GvPorderInfo.Columns[7].Width = 85;
                            GvPorderInfo.Columns[8].Width = 60;
                            GvPorderInfo.Columns[9].Width = 85;
                            if (cmbGodownName.SelectedIndex == 0)
                            {
                                GvPorderInfo.Columns[10].Width = 85;
                                GvPorderInfo.Columns[11].Width = 85;
                                GvPorderInfo.Columns[12].Width = 85;
                            }
                            if (productentrytype != 6)
                            {
                                DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                                bcol.HeaderText = "Show Detail";
                                bcol.Text = "Show Detail";
                                bcol.Name = "btnshowdetail";
                                bcol.FlatStyle = FlatStyle.System;
                                bcol.UseColumnTextForButtonValue = true;
                                GvPorderInfo.Columns.Add(bcol);
                            }
                            this.ActiveControl = txtProductname;
                            dt.Dispose();
                        }
                        else
                        {
                            DataTable dt = db.GetTable("Select Id,ProductName,MfgCompany,Unit,Size,OpeningQty,PurchaseQty,PurReturnQty,SaleQty,SaleReturnQty,DeadQty,RemQty,ProductRate,RemQty*ProductRate as Amount from Stock where Size='" + txtSize.Text + "'");
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
                            GvPorderInfo.Columns["Size"].Width = 60;
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
                                bcol.Width = 80;
                                bcol.UseColumnTextForButtonValue = true;
                                GvPorderInfo.Columns.Add(bcol);
                            }
                            lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(Amount)", ""));
                            lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(RemQty)", ""));
                        }
                    }
                    catch (Exception)
                    {
                        lblTotalAmount.Text = "0.00";
                        lbltotalqty.Text = "0.00";
                    }
                }
                else
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
                            if (productentrytype != 6)
                            {
                                flag = 0;
                                string Proname = "";
                                if ((Proname1).Contains("'"))
                                    Proname = Proname1.Replace("'", "''");
                                else
                                    Proname = Proname1;
                                SqlDataAdapter da = new SqlDataAdapter("SELECT [Id],[ProductName] as 'ProductName' ,[MfgCompany] as 'MfgCompany',[Unit],Size,OpeningQty as 'Opening Qty',[PurchaseQty] as 'Purchase Qty',[PurReturnQty] as 'Pur Return Qty',[SaleQty] as 'Sale Qty',[SaleReturnQty] as 'Sale return Qty',[DeadQty] as 'Dead Qty',[RemQty] as 'RemQty',[ProductRate] as 'Product Rate',cast((RemQty*ProductRate) as decimal(18,2)) as 'RemStockAmt' FROM [Stock] Where [ProductName]='" + Proname + "' and MfgCompany='" + company + "' and  Unit='" + unit + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                                da.Fill(dt);
                                GvPorderInfo.DataSource = dt;
                                db.CloseConnection();
                            }
                            else
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
                                    command.Parameters.Add("@vat", SqlDbType.VarChar).Value = "a";
                                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);

                                    command.CommandTimeout = 300;
                                    dt.Load(command.ExecuteReader());
                                    GvPorderInfo.DataSource = dt;
                                    db.CloseConnection();
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
                                command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'a';
                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                                command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                                command.CommandTimeout = 300;
                                dt.Load(command.ExecuteReader());
                                GvPorderInfo.DataSource = dt;
                                db.CloseConnection();
                            }
                        }
                        try
                        {
                            if (string.IsNullOrEmpty(dt.Compute("Sum(Amount)", "RemQty>=0").ToString()))
                                lblTotalAmount.Text = "0";
                            else
                                lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(Amount)", "RemQty>=0"));
                            if (string.IsNullOrEmpty(dt.Compute("Sum(RemQty)", "RemQty>=0").ToString()))
                                lbltotalqty.Text = "0";
                            else
                                lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(RemQty)", "RemQty>=0"));
                        }
                        catch (Exception)
                        {
                            if (string.IsNullOrEmpty(dt.Compute("Sum(RemStockAmt)", "RemQty>=0").ToString()))
                                lblTotalAmount.Text = "0";
                            else
                                lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(RemStockAmt)", "RemQty>=0"));
                            if (string.IsNullOrEmpty(dt.Compute("Sum(RemQty)", "RemQty>=0").ToString()))
                                lbltotalqty.Text = "0";
                            else
                                lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(RemQty)", "RemQty>=0"));
                        }
                        GvPorderInfo.Refresh();
                        GvPorderInfo.Columns["ID"].Visible = false;
                        GvPorderInfo.Columns[1].Width = 140;
                        GvPorderInfo.Columns[2].Width = 140;
                        GvPorderInfo.Columns[3].Width = 60;
                        GvPorderInfo.Columns[4].Width = 85;
                        GvPorderInfo.Columns[5].Width = 85;
                        GvPorderInfo.Columns[6].Width = 85;
                        GvPorderInfo.Columns[7].Width = 85;
                        GvPorderInfo.Columns[8].Width = 60;
                        GvPorderInfo.Columns[9].Width = 85;
                        if (cmbGodownName.SelectedIndex == 0)
                        {
                            GvPorderInfo.Columns[10].Width = 85;
                            GvPorderInfo.Columns[11].Width = 85;
                            GvPorderInfo.Columns[12].Width = 85;
                        }
                        if (productentrytype != 6)
                        {
                            DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                            bcol.HeaderText = "Show Detail";
                            bcol.Text = "Show Detail";
                            bcol.Name = "btnshowdetail";
                            bcol.FlatStyle = FlatStyle.System;
                            bcol.UseColumnTextForButtonValue = true;
                            GvPorderInfo.Columns.Add(bcol);
                        }
                        this.ActiveControl = txtProductname;
                        dt.Dispose();
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
            { db.CloseConnection(); progressBar1.Visible = false; }
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

        private void btnew_Click(object sender, EventArgs e)
        {
            flag = 0;
            txtProductname.Text = "";
            fillcombo();
            GvPorderInfo.DataSource = null;
            lblTotalAmount.Text = "0.00";
            lbltotalqty.Text = "0.00";
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (flag == 1)
                {
                    RptRemainingtock remainingqty = new RptRemainingtock(null, null,'a');
                    remainingqty.ShowDialog();
                    remainingqty.Dispose();
                }
                else if (txtProductname.Text != "")
                {
                    RptRemainingtock remainingqty = new RptRemainingtock(null, txtProductname.Text, 'a');
                    remainingqty.ShowDialog();
                    remainingqty.Dispose();
                }
                else if (cmbcomanyname.Text != "Select" && cmbcomanyname.SelectedIndex > 0)
                {
                    RptRemainingtock remainingqty = new RptRemainingtock(cmbcomanyname.Text, null, 'a');
                    remainingqty.ShowDialog();
                    remainingqty.Dispose();
                }
                else if (chkDate.Checked)
                {
                    string d = dtpfromdate.Value.Date.ToString("MM/dd/yyyy");
                    RptRemainingtock remainingqty = new RptRemainingtock(d, 'a');
                    remainingqty.ShowDialog();
                    remainingqty.Dispose();
                }
                else
                {
                    RptRemainingtock remainingqty = new RptRemainingtock('a');
                    remainingqty.ShowDialog();
                    remainingqty.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void cmbcomanyname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbcomanyname.SelectedIndex != 0)
                {
                    flag = 0;
                    txtProductname.Text = "";
                    DataTable dt = new DataTable();
                    GvPorderInfo.DataSource = null;
                    for (int i = 0; i < GvPorderInfo.Columns.Count; i++)
                    {
                        GvPorderInfo.Columns.RemoveAt(i);
                    }
                    if (cmbGodownName.SelectedIndex == 0)
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT [Id],[ProductName] as 'ProductName' ,[MfgCompany] as 'MfgCompany',[Unit],OpeningQty as 'Opening Qty',[PurchaseQty] as 'Purchase Qty',[PurReturnQty] as 'Pur Return Qty',[SaleQty] as 'Sale Qty',[SaleReturnQty] as 'Sale return Qty',[DeadQty] as 'Dead Qty',[RemQty] as 'RemQty',[ProductRate] as 'Product Rate',cast((RemQty*ProductRate) as decimal(18,2)) as 'RemStockAmt' FROM [Stock] Where [MfgCompany]='" + cmbcomanyname.Text + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        da.Fill(dt);
                        GvPorderInfo.DataSource = dt;
                    }
                    else
                    {
                        db.connect();
                        SqlCommand command = new SqlCommand("SPVatnWithoutVatGodownStock", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                        command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = cmbcomanyname.Text;
                        command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                        command.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'C';
                        command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'a';
                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                        command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                        command.CommandTimeout = 300;
                        dt.Load(command.ExecuteReader());
                        GvPorderInfo.DataSource = dt;
                        db.CloseConnection();
                    }
                    try
                    {
                        if (string.IsNullOrEmpty(dt.Compute("Sum(Amount)", "RemQty>=0").ToString()))
                            lblTotalAmount.Text = "0";
                        else
                            lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(Amount)", "RemQty>=0"));
                        if (string.IsNullOrEmpty(dt.Compute("Sum(RemQty)", "RemQty>=0").ToString()))
                            lbltotalqty.Text = "0";
                        else
                            lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(RemQty)", "RemQty>=0"));
                    }
                    catch (Exception)
                    {
                        if (string.IsNullOrEmpty(dt.Compute("Sum(RemStockAmt)", "RemQty>=0").ToString()))
                            lblTotalAmount.Text = "0";
                        else
                            lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(RemStockAmt)", "RemQty>=0"));
                        if (string.IsNullOrEmpty(dt.Compute("Sum(RemQty)", "RemQty>=0").ToString()))
                            lbltotalqty.Text = "0";
                        else
                            lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(RemQty)", "RemQty>=0"));
                    }
                    GvPorderInfo.Refresh();
                    GvPorderInfo.Columns["ID"].Visible = false;
                    GvPorderInfo.Columns[1].Width = 140;
                    GvPorderInfo.Columns[2].Width = 140;
                    GvPorderInfo.Columns[3].Width = 60;
                    GvPorderInfo.Columns[4].Width = 85;
                    GvPorderInfo.Columns[5].Width = 85;
                    GvPorderInfo.Columns[6].Width = 85;
                    GvPorderInfo.Columns[7].Width = 85;
                    GvPorderInfo.Columns[8].Width = 60;
                    GvPorderInfo.Columns[9].Width = 85;
                    if (cmbGodownName.SelectedIndex == 0)
                    {
                        GvPorderInfo.Columns[10].Width = 85;
                        GvPorderInfo.Columns[11].Width = 85;
                        GvPorderInfo.Columns[12].Width = 85;
                    }
                    DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                    bcol.HeaderText = "Show Detail";
                    bcol.Text = "Show Detail";
                    bcol.Name = "btnshowdetail";
                    bcol.FlatStyle = FlatStyle.System;
                    bcol.UseColumnTextForButtonValue = true;
                    GvPorderInfo.Columns.Add(bcol);

                    this.ActiveControl = cmbcomanyname;
                    dt.Dispose();
                }
            }
            catch (Exception)
            { db.CloseConnection(); }
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
            cmbcomanyname.Text = "Select";
        }

        private void txtProductname_Leave(object sender, EventArgs e)
        {
            try
            {
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

        private void GvPorderInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GvPorderInfo.Columns[e.ColumnIndex].Name == "btnshowdetail")
                {
                    string pname = Convert.ToString(GvPorderInfo.Rows[e.RowIndex].Cells["ProductName"].Value);
                    string cname = Convert.ToString(GvPorderInfo.Rows[e.RowIndex].Cells["MfgCompany"].Value);
                    string size = Convert.ToString(GvPorderInfo.Rows[e.RowIndex].Cells["Unit"].Value);
                    frmshowdetailofstock showdetailstock = new frmshowdetailofstock(pname, cname, size, Convert.ToInt32(cmbGodownName.SelectedValue));
                    showdetailstock.ShowDialog();
                }
            }
            catch (Exception)
            { }
        }

        private void btnminusstock_Click(object sender, EventArgs e)
        {
            try
            {
                FillGodownName();
                SqlDataAdapter da = new SqlDataAdapter("SELECT [Id],[ProductName] as 'ProductName' ,[MfgCompany] as 'MfgCompany',[Unit],OpeningQty as 'Opening Qty',"+
                "[PurchaseQty] as 'Purchase Qty',[PurReturnQty] as 'Pur Return Qty',[SaleQty] as 'Sale Qty',[SaleReturnQty] as 'Sale return Qty',"+
                "[DeadQty] as 'Dead Qty',[RemQty] as 'RemQty',[ProductRate] as 'Product Rate',cast((RemQty*ProductRate) as decimal(18,2)) as 'RemStockAmt' FROM [Stock] where RemQty<0.00 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                GvPorderInfo.DataSource = null;
                for (int i = 0; i < GvPorderInfo.Columns.Count; i++)
                {
                    GvPorderInfo.Columns.RemoveAt(i);
                }
                GvPorderInfo.DataSource = dt;

                if (string.IsNullOrEmpty(dt.Compute("Sum(RemStockAmt)", "RemQty>=0").ToString()))
                    lblTotalAmount.Text = "0";
                else
                    lblTotalAmount.Text = Convert.ToString(dt.Compute("Sum(RemStockAmt)", "RemQty>=0"));
                if (string.IsNullOrEmpty(dt.Compute("Sum(RemQty)", "RemQty>=0").ToString()))
                    lbltotalqty.Text = "0";
                else
                    lbltotalqty.Text = Convert.ToString(dt.Compute("Sum(RemQty)", "RemQty>=0"));
                GvPorderInfo.Refresh();
                GvPorderInfo.Columns["ID"].Visible = false;
                GvPorderInfo.Columns["ProductName"].Width = 140;
                GvPorderInfo.Columns["MfgCompany"].Width = 140;
                GvPorderInfo.Columns["Unit"].Width = 60;
                GvPorderInfo.Columns["Opening Qty"].Width = 85;
                GvPorderInfo.Columns["Purchase Qty"].Width = 85;
                GvPorderInfo.Columns["Pur Return Qty"].Width = 85;
                GvPorderInfo.Columns["Sale Qty"].Width = 85;
                GvPorderInfo.Columns["Sale return Qty"].Width = 60;
                GvPorderInfo.Columns["Dead Qty"].Width = 85;
                GvPorderInfo.Columns["RemQty"].Width = 85;
                GvPorderInfo.Columns["Product Rate"].Width = 85;
                GvPorderInfo.Columns["RemStockAmt"].Width = 85;

                DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                bcol.HeaderText = "Show Detail";
                bcol.Text = "Show Detail";
                bcol.Name = "btnshowdetail";
                bcol.FlatStyle = FlatStyle.System;
                bcol.UseColumnTextForButtonValue = true;
                GvPorderInfo.Columns.Add(bcol);

                flag = 1;
                da.Dispose();
                dt.Dispose();
            }
            catch (Exception)
            { }
        }

        public void constructorfun()
        {
            try
            {
                if (companyname != null && Proname == null)
                {
                    try
                    {
                        db.connect();
                        SqlDataAdapter da;
                        da = new SqlDataAdapter("Select * from Stock where MfgCompany='" + companyname + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        DSRemainingstock ds = new DSRemainingstock();
                        da.Fill(ds, "Stock");
                        da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                        da.Fill(ds, "CompanyInformation");
                        Stockorpt = new RemainingtockCrystalReport();
                        Stockorpt.SetDataSource(ds);
                        db.CloseConnection();
                        da.Dispose();
                        ds.Dispose();
                    }
                    catch (Exception) { db.CloseConnection(); }
                }
                else if (companyname == null && Proname != null)
                {
                    try
                    {
                        string productname1 = Proname.Split(',')[0];
                        string company = Proname.Split(',')[1];
                        string unit = Proname.Split(',')[2];
                        string productname = "";
                        if (productname1.Contains("'"))
                            productname = productname1.Replace("'", "''");
                        else
                            productname = productname1;
                        try
                        {
                            db.connect();
                            SqlDataAdapter da;
                            da = new SqlDataAdapter("Select * from Stock where ProductName='" + productname + "' and MfgCompany='" + company + "' and Unit='" + unit + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                            DSRemainingstock ds = new DSRemainingstock();
                            da.Fill(ds, "Stock");
                            da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                            da.Fill(ds, "CompanyInformation");
                            Stockorpt = new RemainingtockCrystalReport();
                            Stockorpt.SetDataSource(ds);
                            db.CloseConnection();
                            da.Dispose();
                            ds.Dispose();
                        }
                        catch (Exception)
                        { db.CloseConnection(); }
                    }
                    catch (Exception) { MessageBox.Show("Error occurred while exporting file."); }
                }
                else
                {
                    try
                    {
                        db.connect();
                        SqlDataAdapter da;
                        da = new SqlDataAdapter("Select * from Stock where RemQty<0 and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        DSRemainingstock ds = new DSRemainingstock();
                        da.Fill(ds, "Stock");
                        da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                        da.Fill(ds, "CompanyInformation");
                        Stockorpt = new RemainingtockCrystalReport();
                        Stockorpt.SetDataSource(ds);
                        db.CloseConnection();
                        da.Dispose();
                        ds.Dispose();
                    }
                    catch (Exception) { db.CloseConnection(); }
                }
            }
            catch (Exception) { }
        }

        public DSRemainingstock constructorfunForExcel()
        {
            DSRemainingstock ds = new DSRemainingstock();
            try
            {
                if (companyname != null && Proname == null)
                {
                    try
                    {
                        db.connect();
                        SqlDataAdapter da;
                        da = new SqlDataAdapter("Select * from Stock where MfgCompany='" + companyname + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        ds = new DSRemainingstock();
                        da.Fill(ds, "Stock");
                        da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                        da.Fill(ds, "CompanyInformation");
                        Stockorpt = new RemainingtockCrystalReport();
                        Stockorpt.SetDataSource(ds);
                        db.CloseConnection();
                        da.Dispose();
                        ds.Dispose();
                    }
                    catch (Exception) { db.CloseConnection(); }
                }
                else if (companyname == null && Proname != null)
                {
                    try
                    {
                        string productname1 = Proname.Split(',')[0];
                        string company = Proname.Split(',')[1];
                        string unit = Proname.Split(',')[2];
                        string productname = "";
                        if (productname1.Contains("'"))
                            productname = productname1.Replace("'", "''");
                        else
                            productname = productname1;
                        try
                        {
                            db.connect();
                            SqlDataAdapter da;
                            da = new SqlDataAdapter("Select * from Stock where ProductName='" + productname + "' and MfgCompany='" + company + "' and Unit='" + unit + "' and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                            ds = new DSRemainingstock();
                            da.Fill(ds, "Stock");
                            da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                            da.Fill(ds, "CompanyInformation");
                            Stockorpt = new RemainingtockCrystalReport();
                            Stockorpt.SetDataSource(ds);
                            db.CloseConnection();
                            da.Dispose();
                            ds.Dispose();
                        }
                        catch (Exception)
                        { db.CloseConnection(); }
                    }
                    catch (Exception) { MessageBox.Show("Error occurred while exporting file."); }
                }
                else
                {
                    try
                    {
                        db.connect();
                        SqlDataAdapter da;
                        da = new SqlDataAdapter("Select * from Stock where TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        ds = new DSRemainingstock();
                        da.Fill(ds, "Stock");
                        da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId + "", db.Connection);
                        da.Fill(ds, "CompanyInformation");
                        Stockorpt = new RemainingtockCrystalReport();
                        Stockorpt.SetDataSource(ds);
                        db.CloseConnection();
                        da.Dispose();
                        ds.Dispose();
                    }
                    catch (Exception) { db.CloseConnection(); }
                }
            }
            catch (Exception) { }
            return ds;
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

        private void btnCompoundUnit_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                if (cmbGodownName.SelectedIndex == 0)
                {
                    db.connect();
                    SqlCommand command = new SqlCommand("SPCompoundUnitStock", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@Stock", SqlDbType.NVarChar).Value = 'a';
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
                    command.Parameters.Add("@Stock", SqlDbType.NVarChar).Value = 'a';
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
            try
            {
                txtProductname.Text = "";
                if (cmbGodownName.SelectedIndex == 0)
                    LoadData();
                else
                    GodownGetAll();
            }
            catch (Exception)
            { }
        }

        public void GodownGetAll()
        {
            try
            {
                DataTable dt = new DataTable();
                db.connect();
                SqlCommand command = new SqlCommand("SPVatnWithoutVatGodownStock", db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                command.Parameters.Add("@stock", SqlDbType.VarChar).Value = '0';
                command.Parameters.Add("@vat", SqlDbType.VarChar).Value = '0';
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                command.CommandTimeout = 300;
                dt.Load(command.ExecuteReader());
                GvPorderInfo.DataSource = null;
                for (int i = 0; i < GvPorderInfo.Columns.Count; i++)
                {
                    GvPorderInfo.Columns.RemoveAt(i);
                }
                GvPorderInfo.DataSource = dt;
                GvPorderInfo.Columns[0].Width = 60;
                GvPorderInfo.Columns[1].Width = 140;
                GvPorderInfo.Columns[2].Width = 140;
                GvPorderInfo.Columns[3].Width = 60;
                GvPorderInfo.Columns[4].Width = 60;
                GvPorderInfo.Columns[5].Width = 80;
                GvPorderInfo.Columns[6].Width = 80;
                GvPorderInfo.Columns[7].Width = 80;
                GvPorderInfo.Columns[8].Width = 80;
                GvPorderInfo.Columns[9].Width = 80;
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
                this.ActiveControl = txtProductname;
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void cmbGroupName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                object AmtSum, QtySum;
                if (cmbGroupName.SelectedIndex != 0)
                {
                    flag = 0;
                    txtProductname.Text = "";
                    DataTable dt = new DataTable();
                    GvPorderInfo.DataSource = null;
                    for (int i = 0; i < GvPorderInfo.Columns.Count; i++)
                    {
                        GvPorderInfo.Columns.RemoveAt(i);
                    }
                    if (cmbGodownName.SelectedIndex == 0)
                    {
                        SqlDataAdapter da = new SqlDataAdapter("SELECT s.Id,s.ProductName as 'ProductName' ,s.MfgCompany as 'MfgCompany',s.Unit,OpeningQty as 'Opening Qty',PurchaseQty as 'Purchase Qty',PurReturnQty as 'Pur Return Qty',SaleQty as 'Sale Qty',SaleReturnQty as 'Sale return Qty',DeadQty as 'Dead Qty',RemQty as 'RemQty',ProductRate as 'Product Rate',cast((RemQty*ProductRate) as decimal(18,2)) as 'RemStockAmt' FROM Stock s, ProductInformation p Where p.GroupMasterId=" + cmbGroupName.SelectedValue + " and p.ProductName=s.ProductName and p.ManufactureCompany=s.MfgCompany and p.Uniti=s.Unit and TransactionYear='" + CommonMethod.TransactionYear + "' and CompId=" + CommonMethod.CompId + "", db.Connection);
                        da.Fill(dt);
                        GvPorderInfo.DataSource = dt;
                        AmtSum = dt.Compute("Sum(RemStockAmt)", "RemQty>=0");
                        QtySum = dt.Compute("Sum(RemQty)", "RemQty>=0");
                        if (string.IsNullOrEmpty(AmtSum.ToString()))
                            AmtSum = 0;
                        if (string.IsNullOrEmpty(QtySum.ToString()))
                            QtySum = 0;
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
                        command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'a';
                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                        command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                        command.CommandTimeout = 300;
                        dt.Load(command.ExecuteReader());
                        GvPorderInfo.DataSource = dt;
                        AmtSum = dt.Compute("Sum(Amount)", "RemQty>=0");
                        QtySum = dt.Compute("Sum(RemQty)", "RemQty>=0");
                        if (string.IsNullOrEmpty(AmtSum.ToString()))
                            AmtSum = 0;
                        if (string.IsNullOrEmpty(QtySum.ToString()))
                            QtySum = 0;
                        db.CloseConnection();
                    }
                    lblTotalAmount.Text = Convert.ToString(AmtSum);
                    lbltotalqty.Text = Convert.ToString(QtySum);
                    GvPorderInfo.Refresh();
                    GvPorderInfo.Columns["ID"].Visible = false;
                    GvPorderInfo.Columns[1].Width = 140;
                    GvPorderInfo.Columns[2].Width = 140;
                    GvPorderInfo.Columns[3].Width = 60;
                    GvPorderInfo.Columns[4].Width = 85;
                    GvPorderInfo.Columns[5].Width = 85;
                    GvPorderInfo.Columns[6].Width = 85;
                    GvPorderInfo.Columns[7].Width = 85;
                    GvPorderInfo.Columns[8].Width = 60;
                    GvPorderInfo.Columns[9].Width = 85;
                    if (cmbGodownName.SelectedIndex == 0)
                    {
                        GvPorderInfo.Columns[10].Width = 85;
                        GvPorderInfo.Columns[11].Width = 85;
                        GvPorderInfo.Columns[12].Width = 85;
                    }
                    DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                    bcol.HeaderText = "Show Detail";
                    bcol.Text = "Show Detail";
                    bcol.Name = "btnshowdetail";
                    bcol.FlatStyle = FlatStyle.System;
                    bcol.UseColumnTextForButtonValue = true;
                    GvPorderInfo.Columns.Add(bcol);
                    this.ActiveControl = cmbcomanyname;
                    dt.Dispose();
                }
            }
            catch (Exception)
            { db.CloseConnection(); }
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
                {
                    if (chkDate.Checked == true)
                        this.ActiveControl = dtpfromdate;
                }
            }
            catch (Exception) { }
        }

        private void chkAssemeblyProduct_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkAssemeblyProduct.Checked == true)
                {
                    chkDate.Checked = false;
                    dtpfromdate.Value = DateTime.Now.Date;
                    txtProductname.Text = "";
                    FillGodownName();
                    fillcombo();
                    fillGroupName();
                }
            }
            catch (Exception)
            { }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            try
            {
                txtProductname.Text = "";
                if (cmbGodownName.SelectedIndex == 0)
                    LoadData();
                else
                    GodownGetAll();
            }
            catch (Exception)
            { }
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
    }
}