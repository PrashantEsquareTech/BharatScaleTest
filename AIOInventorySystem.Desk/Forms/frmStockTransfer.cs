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
    public partial class frmStockTransfer : Form
    {
        DbClass db = new DbClass();
        CommonMethod cm = new CommonMethod();
        ToolTip tooltipbtn = new ToolTip();
        public string  ProductCategory;
        public int colIndex = -1, codeflag = 0;
        public bool Tax = true;

        public frmStockTransfer()
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            fillSourceName(); fillDestinationName();
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
            {
                btnTransfer.Enabled = false;
                btnNew.Enabled = false;
            }
        }

        public frmStockTransfer(bool vat)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            Tax = vat;
            fillSourceName(); fillDestinationName();
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
            {
                btnTransfer.Enabled = false;
                btnNew.Enabled = false;
            }
        }

        public frmStockTransfer(int Stockno)
        {
            InitializeComponent();
            cm.changedatetimepickerrange(this);
            fillSourceName(); fillDestinationName();
            if (cm.CheckForUpdation(CommonMethod.NextTransactionYear) == true)
            {
                btnTransfer.Enabled = false;
                btnNew.Enabled = false;
            }
        }

        public void fillSourceName()
        {
            try
            {
                SqlDataAdapter da;
                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select Cast('C'+Cast(CompanyId as varchar(10)) as varchar(15)) as Id,CompanyName as Name from CompanyInformation union Select Cast('G'+Cast(ID as varchar(10)) as varchar(15)) as Id,GodownName as Name from GodownMaster", db.Connection);
                else
                    da = new SqlDataAdapter("Select Cast('C'+Cast(CompanyId as varchar(10)) as varchar(15)) as Id,CompanyName as Name from CompanyInformation union Select Cast('G'+Cast(ID as varchar(10)) as varchar(15)) as Id,GodownName as Name from GodownMaster where CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable GDData = new DataTable();
                da.Fill(GDData);
                cmbSourceNames.DataSource = GDData;
                cmbSourceNames.ValueMember = "Id";
                cmbSourceNames.DisplayMember = "Name";
            }
            catch (Exception)
            { }
        }

        public void fillDestinationName()
        {
            try
            {
                SqlDataAdapter da;
                if (CommonMethod.commProduct == true)
                    da = new SqlDataAdapter("Select Cast('C'+Cast(CompanyId as varchar(10)) as varchar(15)) as Id,CompanyName as Name from CompanyInformation union Select Cast('G'+Cast(ID as varchar(10)) as varchar(15)) as Id,GodownName as Name from GodownMaster", db.Connection);
                else
                    da = new SqlDataAdapter("Select Cast('C'+Cast(CompanyId as varchar(10)) as varchar(15)) as Id,CompanyName as Name from CompanyInformation union Select Cast('G'+Cast(ID as varchar(10)) as varchar(15)) as Id,GodownName as Name from GodownMaster where CompId='" + CommonMethod.CompId + "'", db.Connection);
                DataTable GDData = new DataTable();
                da.Fill(GDData);
                cmbDestinationNames.DataSource = GDData;
                cmbDestinationNames.ValueMember = "Id";
                cmbDestinationNames.DisplayMember = "Name";
            }
            catch (Exception)
            { }
        }

        private void btnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbSourceNames.Text != cmbDestinationNames.Text)
                {
                    if (btnTransfer.Text == "Transfer")
                    {
                        if (GvProductInfo.Rows.Count != 0)
                        {
                            for (int i = 0; i < GvProductInfo.Rows.Count; i++)
                            {
                                string vat = "";
                                if (Tax == true)
                                    vat = "True";
                                else
                                    vat = "False";
                                try
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPTransferStock", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.Add("@event", SqlDbType.VarChar).Value = "Transfer";
                                    command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductName"].Value);
                                    command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                    command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                    command.Parameters.Add("@TransferFrom", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["SourceId"].Value);
                                    command.Parameters.Add("@TransferTo", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["DestinationId"].Value);
                                    command.Parameters.Add("@TransferDate", SqlDbType.VarChar).Value = Convert.ToString(cm.ValidFromDate(dtpPorderdate.Value.Date));
                                    command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantityg"].Value);
                                    command.Parameters.Add("@Vat", SqlDbType.VarChar).Value = Convert.ToString(vat);
                                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                                    command.ExecuteNonQuery();
                                    db.CloseConnection();
                                    command.Dispose();
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                                if (Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value) != "")
                                {
                                    TransferStockRepository TransferStockRepo = new TransferStockRepository();
                                    TransferStock TransferStockData = new TransferStock();
                                    TransferStockData = TransferStockRepo.GetAll().LastOrDefault();
                                    string[] code = Convert.ToString(GvProductInfo.Rows[i].Cells["Code"].Value).Split(',');
                                    for (int k = 0; k < code.Length; k++)
                                    {
                                        TransferStockDetailRepository TSDRepo = new TransferStockDetailRepository();
                                        TransferStockDetail TSDData = new TransferStockDetail();
                                        TSDData.TransferId = TransferStockData.Id;
                                        TSDData.Code = code[k].ToString();
                                        TSDRepo.Add(TSDData);
                                        TSDRepo.Save();
                                    }
                                }
                            }
                            MessageBox.Show("Stock Transfered Successfully.", "Success");
                            clear();
                        }
                        else
                            MessageBox.Show("Add Product First.", "Warning");
                        this.ActiveControl = txtpname;
                    }
                    else
                    {
                        if (GvProductInfo.RowCount > 0)
                        {
                            for (int i = 0; i < GvProductInfo.RowCount; i++)
                            {
                                string vat = "";
                                if (Tax == true)
                                    vat = "True";
                                else
                                    vat = "False";
                                try
                                {
                                    db.connect();
                                    SqlCommand command = new SqlCommand("SPTransferStock", db.Connection);
                                    command.CommandType = CommandType.StoredProcedure;

                                    command.Parameters.Add("@event", SqlDbType.VarChar).Value = "Transfer";
                                    command.Parameters.Add("@ProductId", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["ProductName"].Value);
                                    command.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Company"].Value);
                                    command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["Unit"].Value);
                                    command.Parameters.Add("@TransferFrom", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["SourceId"].Value);
                                    command.Parameters.Add("@TransferTo", SqlDbType.VarChar).Value = Convert.ToString(GvProductInfo.Rows[i].Cells["DestinationId"].Value);
                                    command.Parameters.Add("@TransferDate", SqlDbType.VarChar).Value = Convert.ToString(cm.ValidFromDate(dtpPorderdate.Value.Date));
                                    command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = Convert.ToDecimal(GvProductInfo.Rows[i].Cells["Quantityg"].Value);
                                    command.Parameters.Add("@Vat", SqlDbType.VarChar).Value = Convert.ToString(vat);
                                    command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = Convert.ToString(CommonMethod.TransactionYear);
                                    command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(CommonMethod.CompId);
                                    command.ExecuteNonQuery();
                                    db.CloseConnection();
                                    command.Dispose();
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                            }
                            MessageBox.Show("Stock Updated Successfully.", "Success");
                            clear();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Select Source and Destination Name Different.", "Warning");
                    this.ActiveControl = cmbSourceNames;
                }
            }
            catch (Exception)
            { }
        }

        public void clear()
        {
            btnTransfer.Text = "Transfer";
            txtQuantity.Text = "";
            txtpname.Text = "";
            GvProductInfo.Rows.Clear();
            btnTransfer.Enabled = true;
            fillSourceName();
            fillDestinationName();
        }

        public void autocompletedata(string strId)
        {
            try
            {
                txtpname.Text = "";
                string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                using (SqlConnection con = new SqlConnection(cnString1))
                {
                    SqlCommand cmd = new SqlCommand();
                    if (strId.StartsWith("C"))
                    {
                        string[] strArr = null;
                        strArr = strId.Split('C');
                        int Id = Convert.ToInt32(strArr[1]);
                        cmd = new SqlCommand("Select ProductName,MfgCompany,Unit from Stock where CompId=" + Id + "", con);
                    }
                    else if (strId.StartsWith("G"))
                    {
                        string[] strArr = null;
                        strArr = strId.Split('G');
                        int Id = Convert.ToInt32(strArr[1]);
                        if (Tax == true)
                        {
                            if (CommonMethod.commProduct == true)
                                cmd = new SqlCommand("Select distinct ProductName,MfgCompany,Unit from GodownStock where GoDownId=" + Id + " and Vat='True'", con);
                            else
                                cmd = new SqlCommand("Select distinct ProductName,MfgCompany,Unit from GodownStock where GoDownId=" + Id + " and Vat='True' and CompId='" + CommonMethod.CompId + "'", con);
                        }
                        else
                        {
                            if (CommonMethod.commProduct == true)
                                cmd = new SqlCommand("Select distinct ProductName,MfgCompany,Unit from GodownStock where GoDownId=" + Id + " and Vat='False'", con);
                            else
                                cmd = new SqlCommand("Select distinct ProductName,MfgCompany,Unit from GodownStock where GoDownId=" + Id + " and Vat='False' and CompId='" + CommonMethod.CompId + "'", con);
                        }
                    }
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
                if (txtpname.Text != "" && (txtQuantity.Text != "" && Convert.ToDecimal(txtQuantity.Text) != 0))
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
                    string Proname = txtpname.Text.Trim().Split(',')[0];
                    string company = txtpname.Text.Trim().Split(',')[1];
                    string unit = txtpname.Text.Trim().Split(',')[2];
                    if (colIndex != -1)
                    {
                        GvProductInfo.Rows[colIndex].Cells["ProductName"].Value = Proname;
                        GvProductInfo.Rows[colIndex].Cells["Company"].Value = company;
                        GvProductInfo.Rows[colIndex].Cells["Unit"].Value = unit;
                        GvProductInfo.Rows[colIndex].Cells["Quantityg"].Value = txtQuantity.Text;
                        if (ProductCategory == "Motor")
                            GvProductInfo.Rows[colIndex].Cells["Code"].Value = dtgvcode.Rows[0].Cells[0].Value.ToString();
                        GvProductInfo.Rows[colIndex].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                        GvProductInfo.Rows[colIndex].Cells["SourceId"].Value = cmbSourceNames.SelectedValue.ToString();
                        GvProductInfo.Rows[colIndex].Cells["DestinationId"].Value = cmbDestinationNames.SelectedValue.ToString();
                        colIndex = -1;
                        txtpname.Text = "";
                        txtQuantity.Text = "";
                        this.ActiveControl = txtpname;
                    }
                    else
                    {
                        GvProductInfo.Rows.Add();
                        int i = GvProductInfo.RowCount;
                        GvProductInfo.Rows[i - 1].Cells["ProductName"].Value = Proname;
                        GvProductInfo.Rows[i - 1].Cells["Company"].Value = company;
                        GvProductInfo.Rows[i - 1].Cells["Unit"].Value = unit;
                        GvProductInfo.Rows[i - 1].Cells["Quantityg"].Value = txtQuantity.Text;
                        string codearray = "";
                        if (ProductCategory == "Motor")
                        {
                            for (int l = 0; l < dtgvcode.RowCount; l++)
                            {
                                if (l == 0)
                                    codearray = dtgvcode.Rows[l].Cells[0].Value.ToString();
                                else
                                    codearray = codearray + "," + dtgvcode.Rows[l].Cells[0].Value.ToString();
                            }
                            GvProductInfo.Rows[i - 1].Cells["Code"].Value = codearray;
                        }
                        GvProductInfo.Rows[i - 1].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                        GvProductInfo.Rows[i - 1].Cells["SourceId"].Value = cmbSourceNames.SelectedValue.ToString();
                        GvProductInfo.Rows[i - 1].Cells["DestinationId"].Value = cmbDestinationNames.SelectedValue.ToString();
                        txtpname.Text = "";
                        txtQuantity.Text = "";
                        this.ActiveControl = txtpname;
                    }
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
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("Are you sure to Close this Form?", "Close", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clear();
            this.ActiveControl = txtpname;
        }

        private void frmStockTransfer_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    this.Height = Screen.PrimaryScreen.WorkingArea.Height;
                if (this.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    this.Width = Screen.PrimaryScreen.WorkingArea.Width;
            }
            catch (Exception)
            { }
        }

        private void txtpname_Leave(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Visible = false;
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
                            ProductCategory = productdata.Category;
                            string strId = cmbSourceNames.SelectedValue.ToString();
                            lblAvailableStock.Text = "0";
                            if (strId.StartsWith("C"))
                            {
                                string[] strArr = null;
                                strArr = strId.Split('C');
                                int Id = Convert.ToInt32(strArr[1]);
                                try
                                {
                                    string vat = "y";
                                    if (Tax == true)
                                        vat = "y";
                                    else
                                        vat = "n";
                                    try
                                    {
                                        db.connect();
                                        SqlCommand command = new SqlCommand("SPVatnWithoutVatStock", db.Connection);
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = proname;
                                        command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = company;
                                        command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = unit;
                                        command.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'P';
                                        command.Parameters.Add("@vat", SqlDbType.VarChar).Value = vat;
                                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = Convert.ToInt32(Id);
                                        command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                                        SqlDataReader reader = command.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            lblAvailableStock.Text = reader.GetSqlDecimal(11).ToString();
                                        }
                                        db.CloseConnection();
                                    }
                                    catch (Exception)
                                    { db.CloseConnection(); }
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
                            }
                            else if (strId.StartsWith("G"))
                            {
                                string[] strArr = null;
                                strArr = strId.Split('G');
                                int Id = Convert.ToInt32(strArr[1]);
                                try
                                {                                    
                                    string vat = "y";
                                    if (Tax == true)
                                        vat = "y";
                                    else
                                        vat = "n";
                                    try
                                    {
                                        db.connect();
                                        SqlCommand command = new SqlCommand("SPVatnWithoutVatGodownStock", db.Connection);
                                        command.CommandType = CommandType.StoredProcedure;
                                        command.Parameters.Add("@PrePName", SqlDbType.VarChar).Value = proname;
                                        command.Parameters.Add("@PreCName", SqlDbType.VarChar).Value = company;
                                        command.Parameters.Add("@PreUName", SqlDbType.VarChar).Value = unit;
                                        command.Parameters.Add("@stock", SqlDbType.VarChar).Value = 'P';
                                        command.Parameters.Add("@vat", SqlDbType.VarChar).Value = vat;
                                        command.Parameters.Add("@TransactionYear", SqlDbType.NVarChar).Value = CommonMethod.TransactionYear;
                                        command.Parameters.Add("@CompId", SqlDbType.Int).Value = 0;
                                        command.Parameters.Add("@StockIn", SqlDbType.Int).Value = Convert.ToInt32(Id);
                                        command.Parameters.Add("@FDate", SqlDbType.VarChar).Value = '0';
                                        command.CommandTimeout = 300;
                                        SqlDataReader reader = command.ExecuteReader();
                                        while (reader.Read())
                                        {
                                            lblAvailableStock.Text = reader.GetSqlDecimal(7).ToString();
                                        }
                                        db.CloseConnection();
                                    }
                                    catch (Exception)
                                    { db.CloseConnection(); }
                                }
                                catch (Exception)
                                { db.CloseConnection(); }
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
            catch (Exception)
            { }
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtQuantity.Text != "")
                {
                    if (Convert.ToDecimal(txtQuantity.Text) <= Convert.ToDecimal(lblAvailableStock.Text))
                    {
                        if (colIndex == -1)
                        {
                            if (ProductCategory == "Motor")
                                goto t;
                            else
                                goto s;
                        }
                        else
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
                    s: { }
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Quantity less than available quantity.", "Warning");
                        this.ActiveControl = txtQuantity;
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Quantity.", "Warning");
                    this.ActiveControl = txtQuantity;
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
                if (txtpname.Text != "")
                {
                    string Proname = txtpname.Text.Trim().Split(',')[0];
                    string company = txtpname.Text.Trim().Split(',')[1];
                    string unit = txtpname.Text.Trim().Split(',')[2];
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
                btnTransfer_Click(null, null);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Visible = false;
                this.ActiveControl = txtpname;
            }
            catch (Exception)
            { }
        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {
            try
            {
                progressBar1.Visible = true;
                progressBar1.Minimum = 0;
                progressBar1.Maximum = 100;
                DataTable dt = db.GetTable("Select d.Id,d.ProductName,d.CompanyName,d.Unit,d.PurchaseUnit,d.Quantity,d.Code,m.Id from PurchaseDetail d inner join PurchaseMaster m on m.Id=d.PorderNo where m.Suppliername='First stock' and m.StockIn=" + cmbSourceNames.SelectedValue + " and m.TransactionYear='" + CommonMethod.TransactionYear + "' and m.CompId=" + CommonMethod.CompId + "");
                if (dt.Rows.Count > 0)
                {
                    progressBar1.Maximum = dt.Rows.Count;
                    int j = 0;
                    GvProductInfo.Rows.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        progressBar1.Value = i + 1;
                        PurchaseDetailRepository PurDRepo = new PurchaseDetailRepository();
                        int id = Convert.ToInt32(PurDRepo.GetAll().Where(t => t.ProductName == dt.Rows[i][1].ToString() && t.CompanyName == dt.Rows[i][2].ToString() && t.Unit == dt.Rows[i][3].ToString() && t.PurchaseMaster.TransactionYear == CommonMethod.TransactionYear && t.PurchaseMaster.CompId == CommonMethod.CompId).LastOrDefault().Id);
                        if (id == Convert.ToInt32(dt.Rows[i][0].ToString()))
                        {
                            if (dt.Rows[i][6].ToString() != "")
                            {
                                for (int k = i; k > 0; k--)
                                {
                                    if (dt.Rows[i][7].ToString() == dt.Rows[k][7].ToString() && dt.Rows[k][6].ToString() != "")
                                    {
                                        GvProductInfo.Rows.Add();
                                        GvProductInfo.Rows[j].Cells["Id"].Value = dt.Rows[k][0].ToString();
                                        GvProductInfo.Rows[j].Cells["ProductName"].Value = dt.Rows[k][1].ToString();
                                        GvProductInfo.Rows[j].Cells["Company"].Value = dt.Rows[k][2].ToString();
                                        GvProductInfo.Rows[j].Cells["Unit"].Value = dt.Rows[k][3].ToString();
                                        if (dt.Rows[k][4].ToString() != "")
                                            GvProductInfo.Rows[j].Cells["PurUnit"].Value = dt.Rows[k][4].ToString();
                                        else
                                            GvProductInfo.Rows[j].Cells["PurUnit"].Value = dt.Rows[k][3].ToString();
                                        if (dt.Rows[k][3].ToString() != dt.Rows[k][4].ToString() && dt.Rows[k][4].ToString() != "")
                                        {
                                            ProductRepository productrepo = new ProductRepository();
                                            ProductInformation productdata = new ProductInformation();
                                            if (CommonMethod.commProduct == true)
                                                productdata = productrepo.GetAll().Where(t => t.ProductName == dt.Rows[k][1].ToString() && t.ManufactureCompany == dt.Rows[k][2].ToString() && t.Uniti == dt.Rows[k][3].ToString()).FirstOrDefault();
                                            else
                                                productdata = productrepo.GetAll().Where(t => t.ProductName == dt.Rows[k][1].ToString() && t.ManufactureCompany == dt.Rows[k][2].ToString() && t.Uniti == dt.Rows[k][3].ToString() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                            decimal qty;
                                            qty = Convert.ToDecimal(dt.Rows[k][5].ToString()) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                            GvProductInfo.Rows[i].Cells["Quantityg"].Value = qty;
                                        }
                                        else
                                            GvProductInfo.Rows[j].Cells["Quantityg"].Value = dt.Rows[k][5].ToString();
                                        GvProductInfo.Rows[j].Cells["Code"].Value = dt.Rows[k][6].ToString();
                                        GvProductInfo.Rows[j].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                        j++;
                                    }
                                }
                            }
                            else
                            {
                                GvProductInfo.Rows.Add();
                                GvProductInfo.Rows[j].Cells["Id"].Value = dt.Rows[i][0].ToString();
                                GvProductInfo.Rows[j].Cells["ProductName"].Value = dt.Rows[i][1].ToString();
                                GvProductInfo.Rows[j].Cells["Company"].Value = dt.Rows[i][2].ToString();
                                GvProductInfo.Rows[j].Cells["Unit"].Value = dt.Rows[i][3].ToString();
                                if (dt.Rows[i][4].ToString() != "")
                                    GvProductInfo.Rows[j].Cells["PurUnit"].Value = dt.Rows[i][4].ToString();
                                else
                                    GvProductInfo.Rows[j].Cells["PurUnit"].Value = dt.Rows[i][4].ToString();
                                if (dt.Rows[i][3].ToString() != dt.Rows[i][4].ToString() && dt.Rows[i][4].ToString() != "")
                                {
                                    ProductRepository productrepo = new ProductRepository();
                                    ProductInformation productdata = new ProductInformation();
                                    if (CommonMethod.commProduct == true)
                                        productdata = productrepo.GetAll().Where(t => t.ProductName == dt.Rows[i][1].ToString() && t.ManufactureCompany == dt.Rows[i][2].ToString() && t.Uniti == dt.Rows[i][3].ToString()).FirstOrDefault();
                                    else
                                        productdata = productrepo.GetAll().Where(t => t.ProductName == dt.Rows[i][1].ToString() && t.ManufactureCompany == dt.Rows[i][2].ToString() && t.Uniti == dt.Rows[i][3].ToString() && t.CompId == CommonMethod.CompId).FirstOrDefault();
                                    decimal qty;
                                    qty = Convert.ToDecimal(dt.Rows[i][5].ToString()) / (Convert.ToDecimal(productdata.UnitQty) / Convert.ToDecimal(productdata.AlternateUnitQty));
                                    GvProductInfo.Rows[j].Cells["Quantityg"].Value = qty;
                                }
                                else
                                    GvProductInfo.Rows[j].Cells["Quantityg"].Value = dt.Rows[i][5].ToString();
                                GvProductInfo.Rows[j].Cells["Code"].Value = dt.Rows[i][6].ToString();
                                GvProductInfo.Rows[j].Cells["Remove"].Value = AIOInventorySystem.Desk.Properties.Resources.Remove;
                                j++;
                            }
                        }
                    }
                    progressBar1.Visible = false;
                    if (GvProductInfo.RowCount > 0)
                        btnTransfer.Text = "Update";
                    else
                        MessageBox.Show("Record not found.", "Warning");
                }
                progressBar1.Visible = false;
            }
            catch (Exception)
            { progressBar1.Visible = false; }
        }

        private void cmbSourceNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                autocompletedata(cmbSourceNames.SelectedValue.ToString());
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

        private void dtgvcode_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (txtpname.Text != "" && dtgvcode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
                {
                    codeflag = 0;
                    string Proname = txtpname.Text.Trim().Split(',')[0];
                    string company = txtpname.Text.Trim().Split(',')[1];
                    string unit = txtpname.Text.Trim().Split(',')[2];
                    string strId = cmbSourceNames.SelectedValue.ToString();
                    SqlDataAdapter da = new SqlDataAdapter();
                    if (strId.StartsWith("C"))
                    {
                        string[] strArr = null;
                        strArr = strId.Split('C');
                        int Id = Convert.ToInt32(strArr[1]);
                        da = new SqlDataAdapter("(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + Id + "'as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + Id + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + Proname + "' AND gd.CompanyName='" + company + "' AND gd.Unit='" + unit + "' and gm.CompId=" + Id + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + Id + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in( Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + Id + "'as varchar(10)) as varchar(15)))) union (Select  d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + Id + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where ProductName='" + Proname + "' and MfgCompany='" + company + "' and Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + Id + "'as varchar(10)) as varchar(15))  order by Id DESC ) and m.Id>(Select top 1 m.Id from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + Id + "'as varchar(10)) as varchar(15)) order by Id DESC ) )  union SELECT d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + Id + " and (m.StockIn=0 or m.StockIn is null) and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + Id + " and m.CompId=pm.CompId and (pm.StockIn=0 or pm.StockIn is null)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + Id + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + Proname + "' AND gd.CompanyName='" + company + "' AND gd.Unit='" + unit + "' and gm.CompId=" + Id + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + Id + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + Id + "'as varchar(10)) as varchar(15)))", db.Connection);
                    }
                    else if (strId.StartsWith("G"))
                    {
                        string[] strArr = null;
                        strArr = strId.Split('G');
                        int Id = Convert.ToInt32(strArr[1]);
                        da = new SqlDataAdapter("(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and TransferTo=Cast('G'+Cast('" + Id + "' as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.StockIn=" + Id + ") and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and  m.FromCompId=" + CommonMethod.CompId + " and TransferFrom=Cast('G'+Cast('" + Id + "' as varchar(10)) as varchar(15)))) union ( Select  d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and TransferTo=Cast('G'+Cast('" + Id + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where ProductName='" + Proname + "' and MfgCompany='" + company + "' and Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and TransferTo=Cast('G'+Cast('" + Id + "'as varchar(10)) as varchar(15))  order by Id DESC)) union SELECT d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.StockIn=" + Id + " and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=pm.CompId and pm.StockIn=" + Id + ") and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.StockIn=" + Id + ") and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and  m.FromCompId=" + CommonMethod.CompId + " and TransferFrom=Cast('G'+Cast('" + Id + "' as varchar(10)) as varchar(15)))", db.Connection);
                    }
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
                        codeflag = 1;
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
                    dtgvcode.Visible = true;
                    dtgvcode.CurrentCell.Selected = true;
                }
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
                    string Proname = txtpname.Text.Trim().Split(',')[0];
                    string company = txtpname.Text.Trim().Split(',')[1];
                    string unit = txtpname.Text.Trim().Split(',')[2];
                    string cnString1 = ConfigurationManager.ConnectionStrings["CrystalReportConnection"].ConnectionString;
                    SqlDataReader dreader;
                    SqlConnection conn = new SqlConnection(cnString1);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    AutoCompleteStringCollection acBusIDSorce = new AutoCompleteStringCollection();

                    string strId = cmbSourceNames.SelectedValue.ToString();

                    if (strId.StartsWith("C"))
                    {
                        int Id = Convert.ToInt32(strId.Split('C')[1]);
                        cmd.CommandText = "(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + Id + "'as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + Id + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + Proname + "' AND gd.CompanyName='" + company + "' AND gd.Unit='" + unit + "' and gm.CompId=" + Id + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + Id + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in( Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + Id + "'as varchar(10)) as varchar(15)))) union (Select  d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + Id + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where ProductName='" + Proname + "' and MfgCompany='" + company + "' and Unit='" + unit + "' and TransferTo=Cast('C'+Cast('" + Id + "'as varchar(10)) as varchar(15))  order by Id DESC ) and m.Id>(Select top 1 m.Id from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + Id + "'as varchar(10)) as varchar(15)) order by Id DESC ) )  union SELECT d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + Id + " and (m.StockIn=0 or m.StockIn is null) and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + Id + " and m.CompId=pm.CompId and (pm.StockIn=0 or pm.StockIn is null)) and d.code not in(SELECT d.Code FROM CustomerBillDetail as d inner join CustomerBillMaster as m on m.Id=d.BillNo Where ProductId='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + Id + " and d.Id not in(SELECT gd.BNID FROM GoodReturnMaster as gm inner join GoodReturnDetail as gd on gm.Id=gd.GoodReturnNo Where gd.ProductId='" + Proname + "' AND gd.CompanyName='" + company + "' AND gd.Unit='" + unit + "' and gm.CompId=" + Id + ")) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=" + Id + " and (m.StockIn=0 or m.StockIn is null)) and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and TransferFrom=Cast('C'+Cast('" + Id + "'as varchar(10)) as varchar(15)))";
                    }
                    else if (strId.StartsWith("G"))
                    {
                        int Id = Convert.ToInt32(strId.Split('G')[1]);
                        cmd.CommandText = "(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and TransferTo=Cast('G'+Cast('" + Id + "' as varchar(10)) as varchar(15)) and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.StockIn=" + Id + ") and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and  m.FromCompId=" + CommonMethod.CompId + " and TransferFrom=Cast('G'+Cast('" + Id + "' as varchar(10)) as varchar(15)))) union ( Select  d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and TransferTo=Cast('G'+Cast('" + Id + "'as varchar(10)) as varchar(15)) AND M.Id=(sELECT TOP 1 Id from TransferStock where ProductName='" + Proname + "' and MfgCompany='" + company + "' and Unit='" + unit + "' and m.CompId=" + CommonMethod.CompId + " and TransferTo=Cast('G'+Cast('" + Id + "'as varchar(10)) as varchar(15))  order by Id DESC)) union SELECT d.Code FROM  PurchaseDetail as d inner join PurchaseMaster as m on m.Id=d.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.StockIn=" + Id + " and d.code not in(SELECT d.Code FROM PurchaseReturnDetail as d inner join PurchaseReturnMaster as m on m.Id=d.PurchaseReturnNo inner join PurchaseMaster pm on pm.PorderNo=m.PorderNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.CompId=pm.CompId and pm.StockIn=" + Id + ") and d.code not in(SELECT d.Code FROM DeadProductDetail as d inner join DeadProductMaster as m on m.Id=d.DeadNo Where ProductName='" + Proname + "' AND CompanyName='" + company + "' AND Unit='" + unit + "' and m.StockIn=" + Id + ") and d.code not in(Select d.Code from TransferStockDetail d inner join TransferStock m on m.Id=d.TransferId where m.ProductName='" + Proname + "' and m.MfgCompany='" + company + "' and m.Unit='" + unit + "' and  m.FromCompId=" + CommonMethod.CompId + " and TransferFrom=Cast('G'+Cast('" + Id + "' as varchar(10)) as varchar(15)))";
                    }
                    conn.Open();
                    dreader = cmd.ExecuteReader();
                    if (dreader.HasRows == true)
                    {
                        while (dreader.Read())
                            acBusIDSorce.Add(dreader["Code"].ToString());
                    }
                    else
                    { MessageBox.Show("Data not Found"); }
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

        private void dtpPorderdate_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbSourceNames;
            }
            catch (Exception)
            { }
        }

        private void cmbSourceNames_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = cmbDestinationNames;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmCompanyInformation godown = new frmCompanyInformation();
                        godown.ShowDialog();
                        fillSourceName();
                        this.ActiveControl = cmbSourceNames;
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void cmbDestinationNames_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = txtpname;
                if (Control.ModifierKeys == Keys.Alt)
                {
                    if (e.KeyCode == Keys.N)
                    {
                        frmCompanyInformation godown = new frmCompanyInformation();
                        godown.ShowDialog();
                        fillDestinationName();
                        this.ActiveControl = cmbDestinationNames;
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
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void txtQuantity_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                    this.ActiveControl = btnAdd;
            }
            catch (Exception)
            { }
        }

        private void GvProductInfo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 9)
                {
                    if ((MessageBox.Show("Are you sure to Remove this record?", "Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK))
                    {
                        if (Convert.ToInt32(GvProductInfo.Rows[e.RowIndex].Cells["Id"].Value) != 0)
                            GvProductInfo.Rows.Remove(GvProductInfo.Rows[ e.RowIndex]);
                        else
                            GvProductInfo.Rows.Remove(GvProductInfo.Rows[ e.RowIndex]);
                    }
                }
                else
                {
                    txtpname.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["ProductName"].Value) + "," + GvProductInfo.Rows[e.RowIndex].Cells["Company"].Value + "," + GvProductInfo.Rows[e.RowIndex].Cells["Unit"].Value;
                    txtQuantity.Text = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["Quantityg"].Value);
                    cmbSourceNames.SelectedValue = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["SourceId"].Value);
                    cmbDestinationNames.SelectedValue = Convert.ToString(GvProductInfo.Rows[e.RowIndex].Cells["DestinationId"].Value);
                    colIndex = e.RowIndex;
                }
            }
            catch (Exception)
            { }
        }
    }
}