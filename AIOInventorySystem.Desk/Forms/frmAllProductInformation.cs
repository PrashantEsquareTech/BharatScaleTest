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
using Excel1 = Microsoft.Office.Interop.Excel;

namespace AIOInventorySystem.Desk.Forms
{
    public partial class frmAllProductInformation : Form
    {
        public static frmProductInformation productinfo;
        DbClass db = new DbClass();
        CommonMethod CM = new CommonMethod();
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        public bool updateAllGSTProduct = false;
        DataTable ProductList = new DataTable();

        public frmAllProductInformation()
        {
            InitializeComponent();
            BindProductinfo();
            fillcombo();
            fillgst();
            fillDestinationName();
            fillGroupname();
        }

        public void fillcombo()
        {
            try
            {
                DataTable dt = null;
                if (CommonMethod.commProduct == false)
                    dt = db.Gettable("Select Distinct(ManufactureCompany) from ProductInformation where compid=" + CommonMethod.CompId + " order by ManufactureCompany");
                else
                    dt = db.Gettable("Select Distinct(ManufactureCompany) from ProductInformation order by ManufactureCompany");
                cmbcomanyname.DataSource = dt;
                cmbcomanyname.ValueMember = "ManufactureCompany";
                cmbcomanyname.DisplayMember = "ManufactureCompany";
                cmbcomanyname.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void fillGroupname()
        {
            try
            {
                GroupMasterRepository unitrepo = new GroupMasterRepository();
                List<GroupMaster> unitdata = new List<GroupMaster>();
                unitdata.Clear();
                if (CommonMethod.commProduct == true)
                    unitdata = unitrepo.GetAll().OrderByDescending(t => t.GroupMasterName).ToList();
                else
                    unitdata = unitrepo.GetAll().Where(t => t.CompId == CommonMethod.CompId).OrderByDescending(t => t.GroupMasterName).ToList();
                unitdata.Add(new GroupMaster { GroupMasterName = "Select", GroupMasterId = 0 });
                unitdata.Reverse();
                cmbgroupMaster.DataSource = unitdata;
                cmbgroupMaster.ValueMember = "GroupMasterId";
                cmbgroupMaster.DisplayMember = "GroupMasterName";
                unitrepo.Dispose();
                cmbgroupMaster.SelectedIndex = 0;
            }
            catch (Exception)
            { }
        }

        public void BindProductinfo()
        {
            try
            {
                SqlDataAdapter da = null;
                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("SELECT [ID],GroupName = (SELECT GroupMasterName FROM GroupMaster where GroupMasterId=p.GroupMasterId), [ProductName] As 'Product Name',[ManufactureCompany] as 'Mfg. Company',[Uniti] as Unit,[Rate] as 'Ratail Rate',DealerRate as 'Dealer Rate' ,RackNo,SGstPercent as 'SGST',CGstPercent as 'CGST',ProductType,Prefix,BarcodeStatus  FROM [ProductInformation] p order by ProductName", db.Connection);
                else
                    da = new SqlDataAdapter("SELECT [ID],GroupName = (SELECT GroupMasterName FROM GroupMaster where GroupMasterId=p.GroupMasterId), [ProductName] As 'Product Name',[ManufactureCompany] as 'Mfg. Company',[Uniti] as Unit,[Rate] as 'Ratail Rate',DealerRate as 'Dealer Rate' ,RackNo,SGstPercent as 'SGST',CGstPercent as 'CGST',ProductType,Prefix,BarcodeStatus  FROM [ProductInformation] p where compid=" + CommonMethod.CompId + " order by ProductName", db.Connection);
                ProductList.Clear();
                da.Fill(ProductList);
                GvproductInfo.DataSource = ProductList;
                GvproductInfo.Refresh();
                GvproductInfo.Columns["ID"].Visible = true;
                GvproductInfo.Columns["Updateg"].Width = 70;
                GvproductInfo.Columns["Remove"].Width = 70;
                GvproductInfo.Columns["ID"].Width = 60;
                GvproductInfo.Columns["GroupName"].Width = 100;
                GvproductInfo.Columns["Product Name"].Width = 120;
                GvproductInfo.Columns["Mfg. Company"].Width = 100;
                GvproductInfo.Columns["Unit"].Width = 65;
                GvproductInfo.Columns["Ratail Rate"].Width = 60;
                GvproductInfo.Columns["Dealer Rate"].Width = 60;
                GvproductInfo.Columns["RackNo"].Width = 50;
                GvproductInfo.Columns["SGST"].Width = 55;
                GvproductInfo.Columns["CGST"].Width = 55;
                GvproductInfo.Columns["ProductType"].Visible = false;
                GvproductInfo.Columns["Prefix"].Visible = false;
                GvproductInfo.Columns["BarcodeStatus"].Visible = false;
                da.Dispose();
            }
            catch (Exception)
            { }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkpname.Checked == true && txtpname.Text != "")
                {
                    RptProductInformation productrpt = new RptProductInformation(null, txtpname.Text);
                    productrpt.ShowDialog();
                }
                else if (chkcompanyname.Checked == true && cmbcomanyname.Text != "Select")
                {
                    RptProductInformation productrpt = new RptProductInformation(cmbcomanyname.Text, null);
                    productrpt.ShowDialog();
                }
                else if (chkgroupname.Checked == true && cmbgroupMaster.Text != "Select")
                {
                    int GroupId = Convert.ToInt32(cmbgroupMaster.SelectedValue);
                    RptProductInformation productrpt = new RptProductInformation(GroupId);
                    productrpt.ShowDialog();
                }
                else
                {
                    RptProductInformation productrpt = new RptProductInformation();
                    productrpt.ShowDialog();
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

        private void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                if (chkpname.Checked)
                {
                    if (txtpname.Text.Trim() != "")
                    {
                        string newpname = "";
                        if ((txtpname.Text).Contains("'"))
                            newpname = txtpname.Text.Replace("'", "''");
                        else
                            newpname = txtpname.Text.Trim();
                        string Proname = newpname.Split(',')[0];
                        string company = newpname.Split(',')[1];
                        string unit = newpname.Split(',')[2];
                        dt = ProductList.AsEnumerable()
                           .Where(r => r.Field<String>("Product Name") == Proname && r.Field<String>("Mfg. Company") == company && r.Field<String>("Unit") == unit)
                           .OrderBy(r => r.Field<String>("Product Name"))
                           .CopyToDataTable();
                        
                        //if (CommonMethod.commProduct == false)
                        //    da = new SqlDataAdapter("SELECT [ID],GroupName = (SELECT GroupMasterName FROM GroupMaster where GroupMasterId=p.GroupMasterId),[ProductName] As 'Product Name',[ManufactureCompany] as 'Mfg. Company',[Uniti] as Unit,[Rate] as 'Ratail Rate',DealerRate as 'Dealer Rate' ,RackNo ,SGstPercent as 'SGST',CGstPercent as 'CGST'  FROM [ProductInformation] p Where ProductName='" + Proname + "' and ManufactureCompany='" + company + "' and Uniti='" + unit + "' and compid=" + CommonMethod.CompId + " order by ProductName", db.Connection);
                        //else
                        //    da = new SqlDataAdapter("SELECT [ID],GroupName = (SELECT GroupMasterName FROM GroupMaster where GroupMasterId=p.GroupMasterId),[ProductName] As 'Product Name',[ManufactureCompany] as 'Mfg. Company',[Uniti] as Unit,[Rate] as 'Ratail Rate',DealerRate as 'Dealer Rate' ,RackNo ,SGstPercent as 'SGST',CGstPercent as 'CGST'  FROM [ProductInformation] p Where ProductName='" + Proname + "' and ManufactureCompany='" + company + "' and Uniti='" + unit + "' order by ProductName", db.Connection);
                        //da.Fill(dt);
                    }
                    else
                        MessageBox.Show("Enter Product Name.", "Warning");
                    this.ActiveControl = txtpname;
                }
                else if (chkcompanyname.Checked)
                {
                    if (cmbcomanyname.Text != "Select")
                    {
                        dt = ProductList.AsEnumerable()
                           .Where(r => r.Field<String>("Mfg. Company") == cmbcomanyname.Text)
                           .OrderBy(r => r.Field<String>("Product Name"))
                           .CopyToDataTable();
                        //if (CommonMethod.commProduct == false)
                        //    da = new SqlDataAdapter("SELECT [ID],GroupName = (SELECT GroupMasterName FROM GroupMaster where GroupMasterId=p.GroupMasterId and compid=p.compid),[ProductName] As 'Product Name',[ManufactureCompany] as 'Mfg. Company',[Uniti] as Unit,[Rate] as 'Ratail Rate',DealerRate as 'Dealer Rate' ,RackNo,SGstPercent as 'SGST',CGstPercent as 'CGST' FROM [ProductInformation] p Where ManufactureCompany='" + cmbcomanyname.Text + "' and p.compid=" + CommonMethod.CompId + " order by ProductName", db.Connection);
                        //else
                        //    da = new SqlDataAdapter("SELECT [ID],GroupName = (SELECT GroupMasterName FROM GroupMaster where GroupMasterId=p.GroupMasterId and compid=p.compid),[ProductName] As 'Product Name',[ManufactureCompany] as 'Mfg. Company',[Uniti] as Unit,[Rate] as 'Ratail Rate',DealerRate as 'Dealer Rate' ,RackNo,SGstPercent as 'SGST',CGstPercent as 'CGST' FROM [ProductInformation] p Where ManufactureCompany='" + cmbcomanyname.Text + "' order by ProductName", db.Connection);
                        //da.Fill(dt);
                    }
                    else
                        MessageBox.Show("Select Company Name.", "Warning");
                    this.ActiveControl = cmbcomanyname;
                }
                else if (chkgroupname.Checked)
                {
                    if (cmbgroupMaster.Text != "Select")
                    {
                        dt = ProductList.AsEnumerable()
                              .Where(r => r.Field<String>("GroupName") == cmbgroupMaster.Text)
                              .OrderBy(r => r.Field<String>("Product Name"))
                              .CopyToDataTable();
                        //if (CommonMethod.commProduct == false)
                        //    da = new SqlDataAdapter("SELECT [ID],GroupName = (SELECT GroupMasterName FROM GroupMaster where GroupMasterId=p.GroupMasterId and GroupMaster.compid=p.compid),[ProductName] As 'Product Name',[ManufactureCompany] as 'Mfg. Company',[Uniti] as Unit,[Rate] as 'Ratail Rate',DealerRate as 'Dealer Rate' ,RackNo,SGstPercent as 'SGST',CGstPercent as 'CGST' FROM [ProductInformation] p Where GroupMasterId='" + cmbgroupMaster.SelectedValue + "' and p.compid=" + CommonMethod.CompId + " order by ProductName", db.Connection);
                        //else
                        //    da = new SqlDataAdapter("SELECT [ID],GroupName = (SELECT GroupMasterName FROM GroupMaster where GroupMasterId=p.GroupMasterId and GroupMaster.compid=p.compid),[ProductName] As 'Product Name',[ManufactureCompany] as 'Mfg. Company',[Uniti] as Unit,[Rate] as 'Ratail Rate',DealerRate as 'Dealer Rate' ,RackNo,SGstPercent as 'SGST',CGstPercent as 'CGST' FROM [ProductInformation] p Where GroupMasterId='" + cmbgroupMaster.SelectedValue + "' order by ProductName", db.Connection);
                        //da.Fill(dt);
                    }
                    else
                        MessageBox.Show("Select Group Name.", "Warning");
                    this.ActiveControl = cmbgroupMaster;
                }
                else if (chkPrefix.Checked == true && cmbPrefix.Text != "")
                {
                    dt = ProductList.AsEnumerable()
                              .Where(r => r.Field<String>("Prefix") == cmbPrefix.Text)
                              .OrderBy(r => r.Field<String>("Product Name"))
                              .CopyToDataTable();
                    //if (CommonMethod.commProduct == false)
                    //    da = new SqlDataAdapter("SELECT [ID],GroupName = (SELECT GroupMasterName FROM GroupMaster g where g.GroupMasterId=p.GroupMasterId and g.compid=p.compid),[ProductName] As 'Product Name',[ManufactureCompany] as 'Mfg. Company',[Uniti] as Unit,[Rate] as 'Ratail Rate',DealerRate as 'Dealer Rate' ,RackNo,SGstPercent as 'SGST',CGstPercent as 'CGST' FROM [ProductInformation] p Where Prefix='" + cmbPrefix.Text + "' and p.compid=" + CommonMethod.CompId + " order by ProductName", db.Connection);
                    //else
                    //    da = new SqlDataAdapter("SELECT [ID],GroupName = (SELECT GroupMasterName FROM GroupMaster g where g.GroupMasterId=p.GroupMasterId and g.compid=p.compid),[ProductName] As 'Product Name',[ManufactureCompany] as 'Mfg. Company',[Uniti] as Unit,[Rate] as 'Ratail Rate',DealerRate as 'Dealer Rate' ,RackNo,SGstPercent as 'SGST',CGstPercent as 'CGST' FROM [ProductInformation] p Where Prefix='" + cmbPrefix.Text + "' order by ProductName", db.Connection);
                    //da.Fill(dt);
                }
                else if (chkBracodeStatus.Checked == true)
                {
                    if (cmbBarcodeStatus.Text != "")
                    {
                        dt = ProductList.AsEnumerable()
                              .Where(r => r.Field<String>("BarcodeStatus") == cmbBarcodeStatus.Text)
                              .OrderBy(r => r.Field<String>("Product Name"))
                              .CopyToDataTable();
                        //if (CommonMethod.commProduct == false)
                        //    da = new SqlDataAdapter("SELECT [ID],GroupName = (SELECT GroupMasterName FROM GroupMaster where GroupMasterId=p.GroupMasterId and GroupMaster.compid=p.compid),[ProductName] As 'Product Name',[ManufactureCompany] as 'Mfg. Company',[Uniti] as Unit,[Rate] as 'Ratail Rate',DealerRate as 'Dealer Rate' ,RackNo,SGstPercent as 'SGST',CGstPercent as 'CGST' FROM [ProductInformation] p Where p.BarcodeStatus='" + cmbBarcodeStatus.Text + "' and p.compid=" + CommonMethod.CompId + " order by ProductName", db.Connection);
                        //else
                        //    da = new SqlDataAdapter("SELECT [ID],GroupName = (SELECT GroupMasterName FROM GroupMaster where GroupMasterId=p.GroupMasterId and GroupMaster.compid=p.compid),[ProductName] As 'Product Name',[ManufactureCompany] as 'Mfg. Company',[Uniti] as Unit,[Rate] as 'Ratail Rate',DealerRate as 'Dealer Rate' ,RackNo,SGstPercent as 'SGST',CGstPercent as 'CGST' FROM [ProductInformation] p Where p.BarcodeStatus='" + cmbBarcodeStatus.Text + "' order by ProductName", db.Connection);
                        //da.Fill(dt);
                    }
                    else
                    {
                        MessageBox.Show("Select Barcode Status.", "Warning");
                        this.ActiveControl = cmbBarcodeStatus;
                    }
                }
                else if (chkProductType.Checked == true && cmbProductType.Text != "")
                {
                    dt = ProductList.AsEnumerable()
                              .Where(r => r.Field<int>("ProductType") ==Convert.ToInt32( cmbProductType.SelectedIndex))
                              .OrderBy(r => r.Field<String>("Product Name"))
                              .CopyToDataTable();
                    //if (CommonMethod.commProduct == false)
                    //    da = new SqlDataAdapter("SELECT [ID],GroupName = (SELECT GroupMasterName FROM GroupMaster g where g.GroupMasterId=p.GroupMasterId and g.compid=p.compid),[ProductName] As 'Product Name',[ManufactureCompany] as 'Mfg. Company',[Uniti] as Unit,[Rate] as 'Ratail Rate',DealerRate as 'Dealer Rate' ,RackNo,SGstPercent as 'SGST',CGstPercent as 'CGST' FROM [ProductInformation] p Where Prefix='" + cmbPrefix.Text + "' and p.compid=" + CommonMethod.CompId + " order by ProductName", db.Connection);
                    //else
                    //    da = new SqlDataAdapter("SELECT [ID],GroupName = (SELECT GroupMasterName FROM GroupMaster g where g.GroupMasterId=p.GroupMasterId and g.compid=p.compid),[ProductName] As 'Product Name',[ManufactureCompany] as 'Mfg. Company',[Uniti] as Unit,[Rate] as 'Ratail Rate',DealerRate as 'Dealer Rate' ,RackNo,SGstPercent as 'SGST',CGstPercent as 'CGST' FROM [ProductInformation] p Where Prefix='" + cmbPrefix.Text + "' order by ProductName", db.Connection);
                    //da.Fill(dt);
                }
                else
                {
                    MessageBox.Show("Check Checkbox First.", "Warning");
                    if (txtpname.Text == "")
                        this.ActiveControl = chkcompanyname;
                    else
                        this.ActiveControl = chkpname;
                }
                if (dt.Rows.Count > 0)
                {
                    GvproductInfo.DataSource = dt;
                    GvproductInfo.Refresh();
                    if (GvproductInfo.Rows.Count != 0)
                    {
                        GvproductInfo.Columns["ID"].Visible = true;
                        GvproductInfo.Columns["Updateg"].Width = 70;
                        GvproductInfo.Columns["Remove"].Width = 70;
                        GvproductInfo.Columns["ID"].Width = 60;
                        GvproductInfo.Columns["GroupName"].Width = 100;
                        GvproductInfo.Columns["Product Name"].Width = 120;
                        GvproductInfo.Columns["Mfg. Company"].Width = 100;
                        GvproductInfo.Columns["Unit"].Width = 65;
                        GvproductInfo.Columns["Ratail Rate"].Width = 60;
                        GvproductInfo.Columns["Dealer Rate"].Width = 60;
                        GvproductInfo.Columns["RackNo"].Width = 50;
                        GvproductInfo.Columns["SGST"].Width = 55;
                        GvproductInfo.Columns["CGST"].Width = 55;
                        GvproductInfo.Columns["ProductType"].Visible = false;
                        GvproductInfo.Columns["Prefix"].Visible = false;
                        GvproductInfo.Columns["BarcodeStatus"].Visible = false;
                    }
                    else
                        MessageBox.Show("Record Not found.", "Warning");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Product not Present.", "Warning");
                txtpname.Text = "";
            }
        }

        private void frmAllProductInformation_Load(object sender, EventArgs e)
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
                    txtpname.AutoCompleteCustomSource = MyCollection;
                    con.Close();
                    reader.Dispose();
                }
            }
            catch (Exception)
            { }
        }

        private void chkpname_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkpname.Checked == true)
            {
                chkProductType.Checked = false;
                chkcompanyname.Checked = false;
                cmbcomanyname.Text = "Select";
                chkgroupname.Checked = false;
                fillGroupname();
            }
        }

        private void chkcompanyname_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkcompanyname.Checked == true)
            {
                chkProductType.Checked = false;
                chkpname.Checked = false;
                txtpname.Text = "";
                chkgroupname.Checked = false;
                fillGroupname();
            }
        }

        private void GvproductInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GvproductInfo.Columns[e.ColumnIndex].HeaderText == "Update")
                {
                    int productId = Convert.ToInt32(GvproductInfo.Rows[e.RowIndex].Cells["ID"].Value);
                    if (frmProductInformation.productinfo == null)
                    {
                        frmProductInformation prodinfo = new frmProductInformation(productId);
                        prodinfo.ShowDialog();
                        BindProductinfo();
                    }
                    else
                    {
                        this.Close();
                        frmProductInformation.productinfo.loadData(productId);
                        frmProductInformation.productinfo.Show();
                        frmProductInformation.productinfo = null;
                        BindProductinfo();
                    }
                }
                if (GvproductInfo.Columns[e.ColumnIndex].HeaderText == "Remove")
                {
                    string strcheck = "F";
                    int productId = Convert.ToInt32(GvproductInfo.Rows[e.RowIndex].Cells["ID"].Value);
                    if ((MessageBox.Show("Are you sure to delete this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                    {
                        try
                        {
                            db.connect();
                            SqlCommand command = new SqlCommand("SPCheckforIdUsedorNot", db.Connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.Add("@Flag", SqlDbType.VarChar).Value = "ProductD";
                            command.Parameters.Add("@Id", SqlDbType.VarChar).Value = productId;
                            command.Parameters.Add("@CompId", SqlDbType.Int).Value = CommonMethod.CompId;
                            command.Parameters.Add("@Check", SqlDbType.NVarChar);
                            command.Parameters["@Check"].Direction = ParameterDirection.Output;
                            command.Parameters["@Check"].Size = 5;
                            command.ExecuteReader();
                            strcheck = command.Parameters["@Check"].Value.ToString();
                            db.CloseConnection();
                        }
                        catch (Exception)
                        { db.CloseConnection(); }
                        if (strcheck == "T")
                            MessageBox.Show("This Product is Used. You Can Not Delete.", "Warning");
                        else
                            MessageBox.Show("Record Deleted Sucessfully.", "Success");
                        BindProductinfo();
                        btnsearch.PerformClick();
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void btngetall_Click(object sender, EventArgs e)
        {
            try
            {
                chkProductType.Checked = false;
                chkcompanyname.Checked = false;
                chkpname.Checked = false;
                txtpname.Text = "";
                cmbcomanyname.Text = "Select";
                chkgroupname.Checked = false;
                fillGroupname();
                BindProductinfo();
                updateAllGSTProduct = true;
            }
            catch (Exception)
            { }
        }

        private void GvproductInfo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (Char)Keys.Enter)
                {
                    if (GvproductInfo.CurrentCellAddress.X == 0)
                    {
                        int productId = Convert.ToInt32(GvproductInfo.Rows[GvproductInfo.CurrentCellAddress.Y].Cells["ID"].Value);
                        if (frmProductInformation.productinfo == null)
                        {
                            frmProductInformation prodinfo = new frmProductInformation(productId);
                            prodinfo.ShowDialog();
                            frmProductInformation.productinfo = null;
                            BindProductinfo();
                        }
                        else
                        {
                            frmProductInformation.productinfo.loadData(productId);
                            frmProductInformation.productinfo.Show();
                            frmProductInformation.productinfo = null;
                            BindProductinfo();
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtpname_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtpname.Text.Trim() != "")
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

        private void frmAllProductInformation_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                frmProductInformation.productinfo = null;
            }
            catch (Exception)
            { }
        }

        private void txtpname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void cmbcomanyname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chkgroupname_CheckedChanged(object sender, EventArgs e)
        {
            if (chkgroupname.Checked == true)
            {
                chkProductType.Checked = false;
                chkcompanyname.Checked = false;
                cmbcomanyname.Text = "Select";
                chkpname.Checked = false;
                txtpname.Text = "";
            }
        }

        private void cmbgroupMaster_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        public void PIConstCP(string pname, string company)
        {
            try
            {
                ProductInfoCrystalReport pinforpt;
                if (company != null && pname == null)
                {
                    try
                    {
                        SqlDataAdapter da;
                        ProductinfoDS ds = new ProductinfoDS();
                        if (CommonMethod.commProduct == true)
                            da = new SqlDataAdapter("Select * from ProductInformation where ManufactureCompany='" + company + "'", db.Connection);
                        else
                            da = new SqlDataAdapter("Select * from ProductInformation where ManufactureCompany='" + company + "' and compid=" + CommonMethod.CompId, db.Connection);
                        da.Fill(ds, "ProductInformation");
                        da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId='" + CommonMethod.CompId + "'", db.Connection);
                        da.Fill(ds, "CompanyInformation");
                        pinforpt = new ProductInfoCrystalReport();
                        pinforpt.SetDataSource(ds);
                        db.CloseConnection();
                        da.Dispose();
                        ds.Dispose();
                        pinforpt.Dispose();
                    }
                    catch (Exception)
                    { db.CloseConnection(); }
                }
                else
                {
                    string newpname = "";
                    if ((pname).Contains("'"))
                        newpname = pname.Replace("'", "''");
                    else
                        newpname = pname.Trim();
                    string Proname = newpname.Split(',')[0];
                    string compan = newpname.Split(',')[1];
                    string unit = newpname.Split(',')[2];
                    try
                    {
                        SqlDataAdapter da;
                        ProductinfoDS ds = new ProductinfoDS();
                        if (CommonMethod.commProduct == true)
                            da = new SqlDataAdapter("Select * from ProductInformation where ProductName='" + Proname + "' and  ManufactureCompany='" + compan + "' and uniti='" + unit + "'", db.Connection);
                        else
                            da = new SqlDataAdapter("Select * from ProductInformation where ProductName='" + Proname + "' and  ManufactureCompany='" + compan + "' and uniti='" + unit + "' and compid=" + CommonMethod.CompId, db.Connection);
                        da.Fill(ds, "ProductInformation");
                        da = new SqlDataAdapter("Select * from CompanyInformation where CompanyId=" + CommonMethod.CompId, db.Connection);
                        da.Fill(ds, "CompanyInformation");
                        pinforpt = new ProductInfoCrystalReport();
                        pinforpt.SetDataSource(ds);
                        db.CloseConnection();
                        da.Dispose();
                        ds.Dispose();
                        pinforpt.Dispose();
                    }
                    catch (Exception)
                    { db.CloseConnection(); }
                }
            }
            catch (Exception)
            { MessageBox.Show("Error occurred while exporting file."); }
        }

        void exportToexcel(ProductinfoDS ds, string filename)
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

                progressBar1.Minimum = 0;
                progressBar1.Maximum = ds.Tables["ProductInformation"].Rows.Count;
                progressBar1.Visible = true;
                int m = 1, k = 0, i = 0;

                for (i = 0; i <= ds.Tables["CompanyInformation"].Rows.Count - 1; i++)
                {
                    System.Windows.Forms.Application.DoEvents();
                    progressBar1.Value = i;

                    int l = 0;
                    data = ds.Tables["CompanyInformation"].Rows[i].ItemArray[2].ToString();
                    excelSheet.Cells[m++, l + 1] = data;
                    data = ds.Tables["CompanyInformation"].Rows[i].ItemArray[3].ToString();
                    excelSheet.Cells[m++, l + 1] = data;
                    data = ds.Tables["CompanyInformation"].Rows[i].ItemArray[4].ToString();
                    excelSheet.Cells[m++, l + 1] = data;
                    data = ds.Tables["CompanyInformation"].Rows[i].ItemArray[5].ToString();
                    excelSheet.Cells[m++, l + 1] = data;
                    m++;
                }
                k = i;
                excelSheet.get_Range("A1", "AH1").Font.Bold = true;
                excelSheet.get_Range("A1", "AH1").Font.Color = System.Drawing.Color.Red;

                //excelSheet.Cells[m, 1] = "Product Name";
                excelSheet.Cells[m, 1] = "Product Name";
                excelSheet.Cells[m, 2] = "Unit";
                excelSheet.Cells[m, 3] = "HSN";
                excelSheet.Cells[m, 4] = "Retail";
                excelSheet.Cells[m, 5] = "Delear Rate";
                excelSheet.Cells[m, 6] = "SGST";
                excelSheet.Cells[m, 7] = "CGST";
                excelSheet.Cells[m, 8] = "IGST";
                m++;

                for (i = 0; i <= ds.Tables["ProductInformation"].Rows.Count - 1; i++)
                {
                    //System.Windows.Forms.Application.DoEvents();
                    progressBar1.Value = k;

                    int l = 0;
                    data = ds.Tables["ProductInformation"].Rows[i].ItemArray[2].ToString();
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["ProductInformation"].Rows[i].ItemArray[3].ToString();
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["ProductInformation"].Rows[i].ItemArray[4].ToString();
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["ProductInformation"].Rows[i].ItemArray[5].ToString();
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["ProductInformation"].Rows[i].ItemArray[8].ToString();
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["ProductInformation"].Rows[i].ItemArray[22].ToString();
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["ProductInformation"].Rows[i].ItemArray[23].ToString();
                    excelSheet.Cells[m, ++l] = data;
                    data = ds.Tables["ProductInformation"].Rows[i].ItemArray[24].ToString();
                    excelSheet.Cells[m, ++l] = data;
                    m++;
                    k++;
                }
                excelSheet.Columns.AutoFit();
                excelworkBook.SaveAs(filename, Excel1.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel1.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                excelworkBook.Close(true, misValue, misValue);
                excel.Quit();
                releaseObject(excelSheet);
                releaseObject(excelworkBook);
                releaseObject(excel);
                MessageBox.Show("Excel file created , you can find the file C:\\Users\\Documents\\" + filename);
                progressBar1.Visible = false;
            }
            catch (Exception)
            { }
        }

        public void fillPrefix()
        {
            try
            {
                ProductRepository custrepo = new ProductRepository();
                List<ProductInformation> customerdata = new List<ProductInformation>();
                customerdata.Clear();
                if (CommonMethod.commProduct==true)
                    customerdata = custrepo.GetAll().Where(t => t.Prefix != null).GroupBy(t => t.Prefix).Select(t => t.FirstOrDefault()).ToList();
                else
                    customerdata = custrepo.GetAll().Where(t => t.Prefix != null && t.CompId == CommonMethod.CompId).GroupBy(t => t.Prefix).Select(t => t.FirstOrDefault()).ToList();
                customerdata.Add(new ProductInformation { Prefix = "Select", ProductCode = 0 });
                customerdata.Reverse();
                cmbPrefix.DataSource = customerdata;
                cmbPrefix.ValueMember = "ProductCode";
                cmbPrefix.DisplayMember = "Prefix";
                custrepo.Dispose();
            }
            catch (Exception)
            { }
        }

        private void chkPrefix_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPrefix.Checked == true)
            {
                fillPrefix();
                this.ActiveControl = cmbPrefix;
            }
            else
                cmbPrefix.Text = "";
        }

        public void fillDestinationName()
        {
            try
            {
                CompanyInformationRepository compRepo = new CompanyInformationRepository();
                List<CompanyInformation> compdata = new List<CompanyInformation>();
                compdata.Clear();
                compdata = compRepo.GetAll().Where(t => t.CompanyId != CommonMethod.CompId).ToList();
                if (compdata.Count > 0)
                {
                    cmbdestination.Enabled = true;
                    compdata.Add(new CompanyInformation { CompanyName = "Select", CompanyId = 0 });
                    compdata.Reverse();
                    cmbdestination.DataSource = compdata;
                    cmbdestination.ValueMember = "CompanyId";
                    cmbdestination.DisplayMember = "CompanyName";
                    compRepo.Dispose();
                }
                else
                {
                    cmbdestination.DataSource = null;
                    cmbdestination.Items.Clear();
                    cmbdestination.DisplayMember = "Text";
                    cmbdestination.ValueMember = "Value";
                    cmbdestination.Items.Add(new { Text = "Shop", Value = 0 });
                    cmbdestination.SelectedIndex = 0;
                    cmbdestination.Enabled = false;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbdestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbdestination.SelectedIndex > 0)
                    btncopyProducts.Visible = true;
                else
                    btncopyProducts.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void cmbPrefix_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void cmbdestination_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnsearch;
            }
            catch (Exception)
            { }
        }

        private void chkpname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtpname;
            }
            catch (Exception) { }
        }

        private void chkcompanyname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbcomanyname;
            }
            catch (Exception) { }
        }

        private void chkPrefix_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbPrefix;
            }
            catch (Exception) { }
        }

        private void chkgroupname_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbgroupMaster;
            }
            catch (Exception) { }
        }

        private void cmbPSgst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPSgst.SelectedIndex > 0)
                {
                    cmbPIgst.Enabled = false;
                    cmbPCgst.SelectedValue = cmbPSgst.SelectedValue;
                }
                else
                {
                    cmbPCgst.SelectedValue = cmbPSgst.SelectedValue;
                    cmbPIgst.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbsgst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbsgst.SelectedIndex > 0)
                {
                    cmbigst.Enabled = false;
                    cmbcgst.SelectedValue = cmbsgst.SelectedValue;
                }
                else
                {
                    cmbcgst.SelectedValue = cmbsgst.SelectedValue;
                    cmbigst.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbPCgst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPCgst.SelectedIndex > 0)
                {
                    cmbPIgst.Enabled = false;
                    cmbPSgst.SelectedValue = cmbPCgst.SelectedValue;
                }
                else
                {
                    cmbPSgst.SelectedValue = cmbPCgst.SelectedValue;
                    cmbPIgst.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbcgst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbcgst.SelectedIndex > 0)
                {
                    cmbigst.Enabled = false;
                    cmbsgst.SelectedValue = cmbcgst.SelectedValue;
                }
                else
                {
                    cmbsgst.SelectedValue = cmbcgst.SelectedValue;
                    cmbigst.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void cmbPIgst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPIgst.SelectedIndex > 0)
                {
                    cmbPSgst.SelectedIndex = 0;
                    cmbPCgst.SelectedIndex = 0;
                    cmbPSgst.Enabled = false;
                    cmbPCgst.Enabled = false;
                }
                else
                {
                    cmbPSgst.Enabled = true;
                    cmbPCgst.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        public void fillgst()
        {
            try
            {
                DataTable dtsgst = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforSnCGST='Y'");
                if (dtsgst.Rows.Count > 0)
                {
                    cmbsgst.ValueMember = "GSTNo";
                    cmbsgst.DisplayMember = "GSTPercent";
                    cmbsgst.DataSource = dtsgst;
                }
                DataTable dtcgst = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforSnCGST='Y'");
                if (dtcgst.Rows.Count > 0)
                {
                    cmbcgst.ValueMember = "GSTNo";
                    cmbcgst.DisplayMember = "GSTPercent";
                    cmbcgst.DataSource = dtcgst;
                }
                DataTable dtigst = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforIGST='Y'");
                if (dtigst.Rows.Count > 0)
                {
                    cmbigst.ValueMember = "GSTNo";
                    cmbigst.DisplayMember = "GSTPercent";
                    cmbigst.DataSource = dtigst;
                }
                DataTable dtsgst1 = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforSnCGST='Y'");
                if (dtsgst1.Rows.Count > 0)
                {
                    cmbPSgst.ValueMember = "GSTNo";
                    cmbPSgst.DisplayMember = "GSTPercent";
                    cmbPSgst.DataSource = dtsgst1;
                }
                DataTable dtcgst1 = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforSnCGST='Y'");
                if (dtcgst1.Rows.Count > 0)
                {
                    cmbPCgst.ValueMember = "GSTNo";
                    cmbPCgst.DisplayMember = "GSTPercent";
                    cmbPCgst.DataSource = dtcgst1;
                }
                DataTable dtigst1 = db.Gettable("select 0 as GSTNo,0 as GSTPercent from GSTMaster union select Id,cast(GSTPercent as decimal(18,2)) from GSTMaster where ApplyforIGST='Y'");
                if (dtigst1.Rows.Count > 0)
                {
                    cmbPIgst.ValueMember = "GSTNo";
                    cmbPIgst.DisplayMember = "GSTPercent";
                    cmbPIgst.DataSource = dtigst1;
                }
            }
            catch (Exception) { }
        }

        private void cmbigst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbigst.SelectedIndex > 0)
                {
                    cmbsgst.SelectedIndex = 0;
                    cmbcgst.SelectedIndex = 0;
                    cmbsgst.Enabled = false;
                    cmbcgst.Enabled = false;
                }
                else
                {
                    cmbsgst.Enabled = true;
                    cmbcgst.Enabled = true;
                }
            }
            catch (Exception)
            { }
        }

        private void btnUpdateGST_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkcompanyname.Checked == true)
                {
                    if (cmbcomanyname.Text != "")
                        goto a;
                    else
                    {
                        MessageBox.Show("Select CompanyName.", "Warning");
                        goto b;
                    }
                }
                else if (chkgroupname.Checked == true)
                {
                    if (cmbgroupMaster.SelectedIndex > 0)
                        goto a;
                    else
                    {
                        MessageBox.Show("Select Group Name.", "Warning");
                        progressBar1.Visible = false;
                        goto b;
                    }
                }
                else if (chkpname.Checked == true)
                {
                    if (txtpname.Text != "")
                        goto a;
                    else
                    {
                        MessageBox.Show("Select Group Name.", "Warning");
                        progressBar1.Visible = false;
                        goto b;
                    }
                }
                else if (updateAllGSTProduct == true)
                    goto a;
                else
                    goto b;
            a:
                {
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = GvproductInfo.Rows.Count;
                    progressBar1.Visible = true;
                    if (GvproductInfo.Rows.Count > 0)
                    {
                        for (int i = 0; i < GvproductInfo.Rows.Count; i++)
                        {
                            progressBar1.Value = i;
                            int productId = Convert.ToInt32(GvproductInfo.Rows[i].Cells["ID"].Value);
                            ProductRepository productrepo1 = new ProductRepository();
                            ProductInformation pinfo1 = productrepo1.GetById(productId);
                            pinfo1.SGstPercent = Convert.ToDecimal(cmbsgst.Text);
                            pinfo1.CGstPercent = Convert.ToDecimal(cmbcgst.Text);
                            pinfo1.IGstPercent = Convert.ToDecimal(cmbigst.Text);
                            pinfo1.Pcgstpercent = Convert.ToDecimal(cmbPCgst.Text);
                            pinfo1.Psgstpercent = Convert.ToDecimal(cmbPSgst.Text);
                            pinfo1.Pigstpercent = Convert.ToDecimal(cmbPIgst.Text);
                            productrepo1.Edit(pinfo1);
                            productrepo1.Save();
                        }
                    }
                    MessageBox.Show("Product Information Updated Successfully.", "Success");
                    btnsearch_Click(null, null);
                    fillgst();
                    progressBar1.Visible = false;
                }
            b: { }
                pnlUpdateGST.Visible = false;
                progressBar1.Visible = false;
            }
            catch (Exception)
            { }
        }

        private void lnkQuickUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                pnlUpdateGST.Visible = true;
                this.ActiveControl = tblltUpdateGST;
                this.ActiveControl = cmbPCgst;
            }
            catch (Exception)
            { }
        }

        private void btnexportExcel_Click(object sender, EventArgs e)
        {
            int GroupMasterId = -1;
            try
            {
                RptProductInformation productrpt;
                if (chkpname.Checked == true && txtpname.Text != "")
                {
                    productrpt = new RptProductInformation(null, GroupMasterId, txtpname.Text);
                    string Filename = "ALLProductInfoByProductName.xls";
                    exportexcel(RptProductInformation.ds, Filename);
                }
                else if (chkcompanyname.Checked == true && cmbcomanyname.Text != "Select")
                {
                    productrpt = new RptProductInformation(cmbcomanyname.Text, GroupMasterId, null);
                    string Filename = "ALLProductInfoByCompanyName.xls";
                    exportexcel(RptProductInformation.ds, Filename);
                }
                else if (chkgroupname.Checked == true && cmbgroupMaster.Text != "Select")
                {
                    GroupMasterId = Convert.ToInt32(cmbgroupMaster.SelectedValue);
                    productrpt = new RptProductInformation(null, GroupMasterId, null);
                    string Filename = "ALLProductInfoByGroupName.xls";
                    exportexcel(RptProductInformation.ds, Filename);
                }
                else
                {
                    productrpt = new RptProductInformation();
                    string Filename = "ALLProductInfo.xls";
                    exportexcel(RptProductInformation.ds, Filename);
                    productrpt.Dispose();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error occurred while converting into Exel file.");
            }
        }

        void exportexcel(DataSet ds, string filename)
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
                excelSheet.Cells[1, 1] = "Group Name";//"GROUP_NAME";
                excelSheet.Cells[1, 2] = "Prefix";
                excelSheet.Cells[1, 3] = "Product Type";
                excelSheet.Cells[1, 4] = "Product Name";//"NAME";
                excelSheet.Cells[1, 5] = "Marathi Name";//"MARATHI";
                excelSheet.Cells[1, 6] = "MFG. Company";//"MFG";
                excelSheet.Cells[1, 7] = "Simple Unit";//"UNIT1";
                excelSheet.Cells[1, 8] = "Simple Unit Value";//"U_VALUE1";
                excelSheet.Cells[1, 9] = "Purchase Rate For Simple Unit";//"P_RATE1";
                excelSheet.Cells[1, 10] = "Wholesale Rate For Simple Unit";// "WSRATE1";
                excelSheet.Cells[1, 11] = "Sale Rate For Simple Unit";// "SRATE1";
                excelSheet.Cells[1, 12] = "Alternate Unit";//"UNIT2";
                excelSheet.Cells[1, 13] = "Alternate Unit Value";// "U_VALUE2";
                excelSheet.Cells[1, 14] = "Purchase Rate For Alternate Unit";// "P_RATE2";
                excelSheet.Cells[1, 15] = "Wholesale Rate For Alternate Unit";// "WSRATE2";
                excelSheet.Cells[1, 16] = "Sale Rate For Alternate Unit";// "SRATE2";
                excelSheet.Cells[1, 17] = "Sale SGST %";
                excelSheet.Cells[1, 18] = "Sale CGST %";
                excelSheet.Cells[1, 19] = "Sale IGST %";
                excelSheet.Cells[1, 20] = "Sale Disc. %";
                excelSheet.Cells[1, 21] = "Purchase SGST %";
                excelSheet.Cells[1, 22] = "Purchase CGST %";
                excelSheet.Cells[1, 23] = "Purchase IGST %";
                excelSheet.Cells[1, 24] = "Purchase Disc. %";
                excelSheet.Cells[1, 25] = "Cess";
                excelSheet.Cells[1, 26] = "Tax Inclusive";
                excelSheet.Cells[1, 27] = "HSN Code";
                excelSheet.Cells[1, 28] = "Barcode Status";
                excelSheet.Cells[1, 29] = "With Batch";
                excelSheet.Cells[1, 30] = "With Batch And Expiry";
                excelSheet.Cells[1, 31] = "Category";
                excelSheet.Cells[1, 32] = "Stock Alert";
                excelSheet.Cells[1, 33] = "Rack No";
                excelSheet.Cells[1, 34] = "Description";
                excelSheet.Columns.AutoFit();
                excelworkBook.SaveAs(filename, Excel1.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel1.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                excelworkBook.Close(true, misValue, misValue);
                excel.Quit();
                releaseObject(excelSheet);
                releaseObject(excelworkBook);
                releaseObject(excel);
                MessageBox.Show("Excel file created , you can find the file C:\\Users\\Documents\\" + filename);
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

        private void btnGSTClose_Click(object sender, EventArgs e)
        {
            try
            {
                pnlUpdateGST.Visible = false;
                fillgst();
            }
            catch (Exception)
            { }
        }

        private void chkProductType_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProductType.Checked == true)
            {
                chkpname.Checked = false;
                chkcompanyname.Checked = false;
                cmbcomanyname.Text = "Select";
                chkgroupname.Checked = false;
                fillGroupname();
            }
        }
    }
}