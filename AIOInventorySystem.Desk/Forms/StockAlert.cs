using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class StockAlert : Form
    {
        DbClass db = new DbClass();

        public StockAlert()
        {
            InitializeComponent();
            fillUnit();
            fillMfgCompany();
        }

        public StockAlert(string a)
        {
            InitializeComponent();
            fillUnit();
            fillMfgCompany();
            getall();
        }

        public void fillUnit()
        {
            try
            {
                DataTable dt = null;
                if (CommonMethod.commProduct == true)
                    dt = db.Gettable("Select Distinct(Uniti) from ProductInformation order by Uniti");
                else
                    dt = db.Gettable("Select Distinct(Uniti) from ProductInformation where compid=" + CommonMethod.CompId + " order by Uniti");
                DataRow dr = dt.NewRow(); //Create New Row
                dr["Uniti"] = "Select";
                dt.Rows.InsertAt(dr, 0);
                cmbunitname.DataSource = dt;
                cmbunitname.ValueMember = "Uniti";
                cmbunitname.DisplayMember = "Uniti";
                cmbunitname.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void fillMfgCompany()
        {
            try
            {
                DataTable dt = null;
                if (CommonMethod.commProduct == true)
                    dt = db.Gettable("Select Distinct(ManufactureCompany) from ProductInformation order by ManufactureCompany");
                else
                    dt = db.Gettable("Select Distinct(ManufactureCompany) from ProductInformation where compid=" + CommonMethod.CompId + " order by ManufactureCompany");
                DataRow dr = dt.NewRow(); //Create New Row
                dr["ManufactureCompany"] = "Select";
                dt.Rows.InsertAt(dr, 0);
                cmbCompanyName.DataSource = dt;
                cmbCompanyName.ValueMember = "ManufactureCompany";
                cmbCompanyName.DisplayMember = "ManufactureCompany";
                cmbCompanyName.SelectedIndex = 0;
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
            catch (Exception)
            { }
        }

        private void cmbunitname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbunitname.Text != "Select")
                {
                    txtpname.Text = "";
                    fillMfgCompany();
                }
                if (cmbunitname.Text != "" || cmbunitname.Text != "Select" || cmbunitname.Text != string.Empty)
                {
                    try
                    {
                        db.connect();
                        SqlCommand command = new SqlCommand("SPStockAlert", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@UnitName", SqlDbType.VarChar).Value = cmbunitname.Text;
                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        DataTable dt = new DataTable();
                        dt.Load(command.ExecuteReader());
                        GvPorderInfo.DataSource = dt;
                        db.CloseConnection();
                        GvPorderInfo.Columns[0].Width = 60;
                        GvPorderInfo.Columns[1].Width = 150;
                        GvPorderInfo.Columns[2].Width = 150;
                        GvPorderInfo.Columns[3].Width = 70;
                        GvPorderInfo.Columns[4].Width = 80;
                        lblTotaCount.Text = Convert.ToString(dt.Rows.Count);
                    }
                    catch (Exception)
                    { db.CloseConnection(); }
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
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void getall()
        {
            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPStockAlert", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@UnitName", SqlDbType.VarChar).Value = '0';
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                GvPorderInfo.DataSource = dt;
                db.CloseConnection();
                GvPorderInfo.Columns[0].Width = 60;
                GvPorderInfo.Columns[1].Width = 150;
                GvPorderInfo.Columns[2].Width = 150;
                GvPorderInfo.Columns[3].Width = 70;
                GvPorderInfo.Columns[4].Width = 80;
                lblTotaCount.Text = Convert.ToString(dt.Rows.Count);
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void btngetall_Click(object sender, EventArgs e)
        {
            getall();
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
                            fillUnit();
                            fillMfgCompany();
                            try
                            {
                                db.connect();
                                SqlCommand command = new SqlCommand("SPStockAlert", db.Connection);
                                command.CommandType = CommandType.StoredProcedure;
                                command.Parameters.Add("@ProductName", SqlDbType.VarChar).Value = proname;
                                command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = company;
                                command.Parameters.Add("@UnitName", SqlDbType.VarChar).Value = unit;
                                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                                DataTable dt = new DataTable();
                                dt.Load(command.ExecuteReader());
                                GvPorderInfo.DataSource = dt;
                                db.CloseConnection();
                                GvPorderInfo.Columns[0].Width = 60;
                                GvPorderInfo.Columns[1].Width = 150;
                                GvPorderInfo.Columns[2].Width = 150;
                                GvPorderInfo.Columns[3].Width = 70;
                                GvPorderInfo.Columns[4].Width = 80;
                                lblTotaCount.Text = Convert.ToString(dt.Rows.Count);
                            }
                            catch (Exception)
                            { db.CloseConnection(); }
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

        private void cmbCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbCompanyName.Text != "Select")
                {
                    txtpname.Text = "";
                    fillUnit();
                }
                if (cmbCompanyName.Text != "" || cmbCompanyName.Text != "Select" || cmbCompanyName.Text != string.Empty)
                {
                    try
                    {
                        db.connect();
                        SqlCommand command = new SqlCommand("SPStockAlert", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = cmbCompanyName.Text;
                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        DataTable dt = new DataTable();
                        dt.Load(command.ExecuteReader());
                        GvPorderInfo.DataSource = dt;
                        db.CloseConnection();
                        GvPorderInfo.Columns[0].Width = 60;
                        GvPorderInfo.Columns[1].Width = 150;
                        GvPorderInfo.Columns[2].Width = 150;
                        GvPorderInfo.Columns[3].Width = 70;
                        GvPorderInfo.Columns[4].Width = 80;
                        lblTotaCount.Text = Convert.ToString(dt.Rows.Count);
                    }
                    catch (Exception)
                    { db.CloseConnection(); }
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
                    this.ActiveControl = cmbCompanyName;
            }
            catch (Exception)
            { }
        }

        private void cmbCompanyName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbunitname;
            }
            catch (Exception)
            { }
        }

        private void cmbCompanyName_Leave(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = cmbunitname;
            }
            catch (Exception) { }
        }

        private void cmbunitname_Leave(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = btnClose;
            }
            catch (Exception) { }
        }

        private void cmbunitname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnClose;
            }
            catch (Exception) { }
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