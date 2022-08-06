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
    public partial class frmStockRegister : Form
    {
        DbClass db = new DbClass();
        CommonMethod common = new CommonMethod();

        public frmStockRegister()
        {
            InitializeComponent();
            common.changedatetimepickerrange(this);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCategory.SelectedIndex > 0)
                    getProductData();
                else
                {
                    MessageBox.Show("Please Select Category", "Warning");
                    this.ActiveControl = cmbCategory;
                }
            }
            catch (Exception)
            { }
        }

        private void getProductData(bool showPrint = false)
        {
            try
            {
                if (chkMonthwise.Checked == false)  //datewise display
                {
                    if (txtProduct.Text != "")
                    {
                        string Proname = txtProduct.Text.Trim().Split(',')[0];
                        string company = txtProduct.Text.Trim().Split(',')[1];
                        string unit = txtProduct.Text.Trim().Split(',')[2];
                        DataTable dtstock = new DataTable();
                        db.connect();
                        SqlCommand cmd = new SqlCommand("sp_Stockregister", db.Connection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Ffromdate", SqlDbType.DateTime).Value = CommonMethod.startdate;
                        cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = Convert.ToDateTime(dtpFrom.Value.Date.ToShortDateString());
                        cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = Convert.ToDateTime(dtpTo.Value.Date.ToShortDateString());
                        cmd.Parameters.Add("@Productdesc", SqlDbType.NVarChar).Value = Proname;
                        cmd.Parameters.Add("@MfgCompany", SqlDbType.NVarChar).Value = company;
                        cmd.Parameters.Add("@Unit", SqlDbType.NVarChar).Value = unit;
                        cmd.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                        cmd.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                        cmd.ExecuteNonQuery();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dtstock);
                        dtgStock.DataSource = dtstock;

                        if (dtgStock.Rows.Count > 0)
                        {
                            dtgStock.Columns["Sr.No."].Width = 50;
                            dtgStock.Columns["Transaction Date"].Width = 130;
                            dtgStock.Columns["Opening Qty"].Width = 85;
                            dtgStock.Columns["Purchase Qty"].Width = 85;
                            dtgStock.Columns["Purchase Return Qty"].Width = 85;
                            dtgStock.Columns["Dead Qty"].Width = 85;
                            dtgStock.Columns["Sale Qty"].Width = 85;
                            dtgStock.Columns["Sale Return Qty"].Width = 85;
                            dtgStock.Columns["Closing Qty"].Width = 85;
                        }
                        else
                        {
                            MessageBox.Show("No Records Found!!", "Stock Register");
                            this.ActiveControl = txtProduct;
                        }
                        db.CloseConnection();
                    }
                    else
                    {
                        MessageBox.Show("Please select product", "Warning");
                        db.CloseConnection();
                        this.ActiveControl = txtProduct;
                    }
                }
                else  // if monthwise display
                {
                    DataTable dtstock = new DataTable();
                    db.connect();
                    SqlCommand cmd = new SqlCommand("sp_Stockregister", db.Connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Ffromdate", SqlDbType.DateTime).Value = CommonMethod.startdate;
                    cmd.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = Convert.ToDateTime(dtpFrom.Value.Date.ToShortDateString());
                    cmd.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = Convert.ToDateTime(dtpTo.Value.Date.ToShortDateString());
                    cmd.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                    cmd.Parameters.Add("@DisplayMonthwise", SqlDbType.Char).Value = "Y";
                    cmd.Parameters.Add("@CatId", SqlDbType.Int).Value = Convert.ToInt32(cmbCategory.SelectedValue);
                    cmd.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;

                    cmd.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dtstock);
                    dtgStock.DataSource = dtstock;

                    if (dtgStock.Rows.Count > 0)
                    {
                        dtgStock.Columns["Sr.No."].Width = 50;
                        dtgStock.Columns["Product"].Width = 160;
                        dtgStock.Columns["Category"].Width = 20;
                        dtgStock.Columns["Opening Qty"].Width = 85;
                        dtgStock.Columns["Purchase Qty"].Width = 85;
                        dtgStock.Columns["Purchase Return Qty"].Width = 85;
                        dtgStock.Columns["Dead Qty"].Width = 85;
                        dtgStock.Columns["Sale Qty"].Width = 85;
                        dtgStock.Columns["Sale Return Qty"].Width = 85;
                        dtgStock.Columns["Closing Qty"].Width = 85;
                        dtgStock.Columns["CategoryId"].Width = 25;
                        dtgStock.Columns["CategoryId"].Visible = false;
                        dtgStock.Columns["Category"].Visible = false;
                    }
                    else
                    {
                        MessageBox.Show("No Records Found!!", "Stock Register");
                        this.ActiveControl = txtProduct;
                    }
                    db.CloseConnection();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error on getting products stock details");
                db.CloseConnection();
                this.ActiveControl = txtProduct;
            }
        }

        private void dtpTo_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(dtpTo.Text) < Convert.ToDateTime(dtpFrom.Text))
            {
                MessageBox.Show("To date should be greater than from date", "Warning");
                dtpTo.Value = dtpFrom.Value;
                this.ActiveControl = dtpTo;
            }
        }

        private void frmStockRegister_Load(object sender, EventArgs e)
        {
            fillCategory();
        }

        private void fillProduct()
        {
            txtProduct.Text = "";
            string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cnString1))
            {
                SqlCommand cmd = null;
                if (CommonMethod.commProduct == true)
                    cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where groupmasterid=" + Convert.ToInt32(cmbCategory.SelectedValue), con);
                else
                    cmd = new SqlCommand("SELECT ProductName,ManufactureCompany,Uniti FROM ProductInformation where groupmasterid=" + Convert.ToInt32(cmbCategory.SelectedValue) + " and compid=" + CommonMethod.CompId, con);
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
                txtProduct.AutoCompleteCustomSource = MyCollection;
                con.Close();
                reader.Dispose();
            }
        }

        private void fillCategory()
        {
            try
            {
                GroupMasterRepository catRepo = new GroupMasterRepository();
                List<GroupMaster> catdata = new List<GroupMaster>();
                catdata.Clear();
                if (CommonMethod.commProduct == true)
                    catdata = catRepo.GetAll().OrderByDescending(t => t.GroupMasterName).ToList();
                else
                    catdata = catRepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.GroupMasterName).ToList();
                catdata.Add(new GroupMaster { GroupMasterName = "Select", GroupMasterId = 0 });
                catdata.Reverse();
                cmbCategory.DataSource = catdata;
                cmbCategory.ValueMember = "GroupMasterId";
                cmbCategory.DisplayMember = "GroupMasterName";
                catRepo.Dispose();
                cmbCategory.SelectedIndex = 0;
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
            return base.ProcessDialogKey(keyData);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCategory.SelectedIndex > 0)
                {
                    if (txtProduct.Text != "" && chkMonthwise.Checked == false)
                    {
                        Rpt_StockRegister stockregisterrpt = new Rpt_StockRegister(txtProduct.Text, dtpFrom.Value, dtpTo.Value, "N", 0);
                        stockregisterrpt.ShowDialog();
                        stockregisterrpt.Dispose();
                    }
                    else if (chkMonthwise.Checked == true)
                    {
                        Rpt_StockRegister stockregisterrpt = new Rpt_StockRegister("", dtpFrom.Value, dtpTo.Value, "Y", Convert.ToInt32(cmbCategory.SelectedValue));
                        stockregisterrpt.ShowDialog();
                        stockregisterrpt.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Please select product", "Warning");
                        db.CloseConnection();
                        this.ActiveControl = txtProduct;
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Category", "Warning");
                    this.ActiveControl = cmbCategory;
                }
            }
            catch (Exception)
            { }
        }

        private void chkMonthwise_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMonthwise.Checked == true)
            {
                txtProduct.Enabled = false;
                txtProduct.Text = "";
            }
            else
                txtProduct.Enabled = true;
        }

        private void txtProduct_Enter(object sender, EventArgs e)
        {
            if (cmbCategory.Text == "")
            {
                MessageBox.Show("Please select Category", "Warning");
                txtProduct.Text = "";
                this.ActiveControl = cmbCategory;
            }
        }

        private void cmbCategory_Leave(object sender, EventArgs e)
        {
            if (cmbCategory.Text != "")
                fillProduct();
        }

        private void chkMonthwise_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpFrom;
            }
            catch (Exception) { }
        }

        private void dtpFrom_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = dtpTo;
            }
            catch (Exception) { }
        }

        private void dtpTo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception) { }
        }

        private void cmbCategory_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtProduct;
            }
            catch (Exception) { }
        }

        private void txtProduct_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnSearch;
            }
            catch (Exception) { }
        }

        private void txtProduct_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtProduct.Text.Trim() != "")
                {
                    string proname = "", company = "", unit = "";
                    if ((txtProduct.Text).Contains(","))
                    {
                        try
                        {
                            proname = txtProduct.Text.Trim().Split(',')[0];
                            company = txtProduct.Text.Trim().Split(',')[1];
                            unit = txtProduct.Text.Trim().Split(',')[2];
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
                            txtProduct.Text = "";
                            this.ActiveControl = txtProduct;
                        }
                    }
                    else
                    {
                        MessageBox.Show("This Product Name Is Not Valid.", "Warning");
                        txtProduct.Text = "";
                        this.ActiveControl = txtProduct;
                    }
                }
            }
            catch (Exception)
            { }
        }
    }
}