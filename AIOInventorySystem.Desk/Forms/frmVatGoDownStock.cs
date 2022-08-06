using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AIOInventorySystem.Data.Model;
using AIOInventorySystem.Data.Repository;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmVatGoDownStock : Form
    {
        DbClass db = new DbClass();

        public frmVatGoDownStock()
        {
            InitializeComponent();
            fillcombo();
            FillGodownName();
        }

        public void fillcombo()
        {
            try
            {
                DataTable dt = new DataTable();
                if (CommonMethod.commProduct == true)
                    dt = db.Gettable("Select Distinct(ManufactureCompany) from ProductInformation order by ManufactureCompany");
                else
                    dt = db.Gettable("Select Distinct(ManufactureCompany) from ProductInformation where CompId='" + CommonMethod.CompId + "' order by ManufactureCompany");
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
                    GDData = GDRepo.GetAll().OrderBy(t => t.GodownName).ToList();
                else
                    GDData = GDRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderBy(t=>t.GodownName).ToList();
                if (GDData.Count > 0)
                {
                    cmbGodownName.DataSource = GDData;
                    cmbGodownName.ValueMember = "ID";
                    cmbGodownName.DisplayMember = "GodownName";
                    cmbGodownName.Enabled = true;
                    GDRepo.Dispose();
                }
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
                    SqlCommand cmd = new SqlCommand();
                    if (CommonMethod.commProduct == true)
                        cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation", con);
                    else
                        cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where CompId='" + CommonMethod.CompId + "'", con);
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
                if (txtProductname.Text != "")
                {
                    cmbcomanyname.Text = "Select";
                    string Proname1 = txtProductname.Text.Trim().Split(',')[0];
                    string company = txtProductname.Text.Trim().Split(',')[1];
                    string unit = txtProductname.Text.Trim().Split(',')[2];

                    try
                    {
                        db.connect();
                        SqlCommand command = new SqlCommand("SPVatnWithoutVatGodownStock", db.Connection);
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = Proname1;
                        command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = company;
                        command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = unit;
                        command.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'P';
                        command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'y';
                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                        command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                        command.CommandTimeout = 300;
                        DataTable dt = new DataTable();
                        dt.Load(command.ExecuteReader());
                        GvPorderInfo.DataSource = null;
                        for (int i = 0; i < GvPorderInfo.Columns.Count; i++)
                        {
                            GvPorderInfo.Columns.RemoveAt(i);
                        }
                        GvPorderInfo.DataSource = dt;
                        if (GvPorderInfo.Columns.Contains("btnshowdetail"))
                        {
                            GvPorderInfo.Columns[1].Width = 60;
                            GvPorderInfo.Columns[4].Width = 80;
                            GvPorderInfo.Columns[5].Width = 80;
                            GvPorderInfo.Columns[6].Width = 80;
                            GvPorderInfo.Columns[7].Width = 80;
                            GvPorderInfo.Columns[8].Width = 80;
                            GvPorderInfo.Columns[9].Width = 80;
                            GvPorderInfo.Columns[10].Width = 80;
                            GvPorderInfo.Columns[0].Width = 80;
                        }
                        else
                        {
                            GvPorderInfo.Columns[0].Width = 60;
                            GvPorderInfo.Columns[4].Width = 80;
                            GvPorderInfo.Columns[5].Width = 80;
                            GvPorderInfo.Columns[6].Width = 80;
                            GvPorderInfo.Columns[7].Width = 80;
                            GvPorderInfo.Columns[8].Width = 80;
                            GvPorderInfo.Columns[9].Width = 80;
                            GvPorderInfo.Columns[10].Width = 80;
                            DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                            bcol.HeaderText = "Show Detail";
                            bcol.Text = "Show Detail";
                            bcol.Name = "btnshowdetail";
                            bcol.FlatStyle = FlatStyle.System;
                            bcol.UseColumnTextForButtonValue = true;
                            bcol.Width = 120;
                            GvPorderInfo.Columns.Add(bcol);
                        }
                        try
                        {
                            SqlCommand cmd = new SqlCommand("Select Cast(Sum(Amount) AS varchar) as totalremamt,Cast(Sum(RemQty) AS varchar) as remqty  from StockVat", db.Connection);
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                lblTotalAmount.Text = Convert.ToString(reader.GetString(0).ToString());
                                lbltotalqty.Text = Convert.ToString(reader.GetString(1).ToString());
                            }
                            db.CloseConnection();
                        }
                        catch (Exception)
                        {
                            lblTotalAmount.Text = "0.00";
                            lbltotalqty.Text = "0.00";
                            db.CloseConnection();
                        }
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
                    db.connect();
                    SqlCommand command = new SqlCommand("SPVatnWithoutVatGodownStock", db.Connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                    command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = cmbcomanyname.Text;
                    command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                    command.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'C';
                    command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'y';
                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                    command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                    command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                    command.CommandTimeout = 300;
                    DataTable dt = new DataTable();
                    dt.Load(command.ExecuteReader());
                    GvPorderInfo.DataSource = null;
                    for (int i = 0; i < GvPorderInfo.Columns.Count; i++)
                    {
                        GvPorderInfo.Columns.RemoveAt(i);
                    }
                    GvPorderInfo.DataSource = dt;
                    if (GvPorderInfo.Columns.Contains("btnshowdetail"))
                    {
                        GvPorderInfo.Columns[1].Width = 60;
                        GvPorderInfo.Columns[4].Width = 80;
                        GvPorderInfo.Columns[5].Width = 80;
                        GvPorderInfo.Columns[6].Width = 80;
                        GvPorderInfo.Columns[7].Width = 80;
                        GvPorderInfo.Columns[8].Width = 80;
                        GvPorderInfo.Columns[9].Width = 80;
                        GvPorderInfo.Columns[10].Width = 80;
                        GvPorderInfo.Columns[0].Width = 80;
                    }
                    else
                    {
                        GvPorderInfo.Columns[0].Width = 60;
                        GvPorderInfo.Columns[4].Width = 80;
                        GvPorderInfo.Columns[5].Width = 80;
                        GvPorderInfo.Columns[6].Width = 80;
                        GvPorderInfo.Columns[7].Width = 80;
                        GvPorderInfo.Columns[8].Width = 80;
                        GvPorderInfo.Columns[9].Width = 80;
                        GvPorderInfo.Columns[10].Width = 80;
                        DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                        bcol.HeaderText = "Show Detail";
                        bcol.Text = "Show Detail";
                        bcol.Name = "btnshowdetail";
                        bcol.FlatStyle = FlatStyle.System;
                        bcol.UseColumnTextForButtonValue = true;
                        bcol.Width = 120;
                        GvPorderInfo.Columns.Add(bcol);
                    }
                    try
                    {
                        SqlCommand cmd = new SqlCommand("Select Cast(Sum(Amount) AS varchar) as totalremamt,Cast(Sum(RemQty) AS varchar) as remqty  from StockVat", db.Connection);
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            lblTotalAmount.Text = Convert.ToString(reader.GetString(0).ToString());
                            lbltotalqty.Text = Convert.ToString(reader.GetString(1).ToString());
                        }
                        db.CloseConnection();
                    }
                    catch (Exception)
                    {
                        lblTotalAmount.Text = "0.00";
                        lbltotalqty.Text = "0.00";
                        db.CloseConnection();
                    }
                }
                catch (Exception)
                { db.CloseConnection(); }
                this.ActiveControl = cmbcomanyname;
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
                    frmshowdetailofstock showdetailstock = new frmshowdetailofstock(GvPorderInfo.Rows[e.RowIndex].Cells["ProductName"].Value.ToString(), GvPorderInfo.Rows[e.RowIndex].Cells["MfgCompany"].Value.ToString(), GvPorderInfo.Rows[e.RowIndex].Cells["Unit"].Value.ToString(), "y", Convert.ToInt32(cmbGodownName.SelectedValue));
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
                db.connect();
                SqlCommand command = new SqlCommand("SPCompoundUnitGodownStock", db.Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@Stock", SqlDbType.NVarChar).Value = 'y';
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                command.CommandTimeout = 300;
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                GvPorderInfo.DataSource = null;
                for (int i = 0; i < GvPorderInfo.Columns.Count; i++)
                {
                    GvPorderInfo.Columns.RemoveAt(i);
                }
                GvPorderInfo.DataSource = dt;
                GvPorderInfo.Refresh();
                if (GvPorderInfo.Columns.Contains("btnshowdetail"))
                {
                    GvPorderInfo.Columns["ID"].Visible = false;
                    GvPorderInfo.Columns[2].Width = 140;
                    GvPorderInfo.Columns[3].Width = 140;
                    GvPorderInfo.Columns[4].Width = 60;
                    GvPorderInfo.Columns[5].Width = 60;
                    GvPorderInfo.Columns[6].Width = 85;
                    GvPorderInfo.Columns[7].Width = 85;
                    GvPorderInfo.Columns[8].Width = 85;
                    GvPorderInfo.Columns[9].Width = 85;
                    GvPorderInfo.Columns[10].Width = 85;
                    GvPorderInfo.Columns[11].Width = 85;
                }
                else
                {
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
                    DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                    bcol.HeaderText = "Show Detail";
                    bcol.Text = "Show Detail";
                    bcol.Name = "btnshowdetail";
                    bcol.FlatStyle = FlatStyle.System;
                    bcol.UseColumnTextForButtonValue = true;
                    bcol.Width = 120;
                    GvPorderInfo.Columns.Add(bcol);
                }
                this.ActiveControl = txtProductname;
                dt.Dispose();
                lblTotalAmount.Text = "";
                lbltotalqty.Text = "";
                db.CloseConnection();
            }
            catch (Exception)
            { db.CloseConnection(); }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            txtProductname.Text = "";
            try
            {
                db.connect();
                SqlCommand command = new SqlCommand("SPVatnWithoutVatGodownStock", db.Connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = '0';
                command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = '0';
                command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = '0';
                command.Parameters.Add("@stock", SqlDbType.VarChar).Value = '0';
                command.Parameters.Add("@vat", SqlDbType.VarChar).Value = 'y';
                command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(cmbGodownName.SelectedValue);
                command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                command.CommandTimeout = 300;
                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                GvPorderInfo.DataSource = null;
                for (int i = 0; i < GvPorderInfo.Columns.Count; i++)
                {
                    GvPorderInfo.Columns.RemoveAt(i);
                }
                GvPorderInfo.DataSource = dt;
                if (GvPorderInfo.Columns.Contains("btnshowdetail"))
                {
                    GvPorderInfo.Columns[1].Width = 60;
                    GvPorderInfo.Columns[4].Width = 80;
                    GvPorderInfo.Columns[5].Width = 80;
                    GvPorderInfo.Columns[6].Width = 80;
                    GvPorderInfo.Columns[7].Width = 80;
                    GvPorderInfo.Columns[8].Width = 80;
                    GvPorderInfo.Columns[9].Width = 80;
                    GvPorderInfo.Columns[10].Width = 80;
                    GvPorderInfo.Columns[0].Width = 80;
                }
                else
                {
                    GvPorderInfo.Columns[0].Width = 60;
                    GvPorderInfo.Columns[4].Width = 80;
                    GvPorderInfo.Columns[5].Width = 80;
                    GvPorderInfo.Columns[6].Width = 80;
                    GvPorderInfo.Columns[7].Width = 80;
                    GvPorderInfo.Columns[8].Width = 80;
                    GvPorderInfo.Columns[9].Width = 80;
                    GvPorderInfo.Columns[10].Width = 80;
                    DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();
                    bcol.HeaderText = "Show Detail";
                    bcol.Text = "Show Detail";
                    bcol.Name = "btnshowdetail";
                    bcol.FlatStyle = FlatStyle.System;
                    bcol.UseColumnTextForButtonValue = true;
                    bcol.Width = 120;
                    GvPorderInfo.Columns.Add(bcol);
                }
                try
                {
                    SqlCommand cmd = new SqlCommand("Select Cast(Sum(Amount) AS varchar) as totalremamt,Cast(Sum(RemQty) AS varchar) as remqty  from StockVat", db.Connection);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        lblTotalAmount.Text = Convert.ToString(reader.GetString(0).ToString());
                        lbltotalqty.Text = Convert.ToString(reader.GetString(1).ToString());
                    }
                    db.CloseConnection();
                }
                catch (Exception)
                {
                    lblTotalAmount.Text = "0.00";
                    lbltotalqty.Text = "0.00";
                    db.CloseConnection();
                }
                this.ActiveControl = txtProductname;
            }
            catch (Exception)
            { db.CloseConnection(); }
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